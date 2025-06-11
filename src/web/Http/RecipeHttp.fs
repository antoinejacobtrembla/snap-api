namespace Web.Http

open Giraffe
open Microsoft.AspNetCore.Http
open System

open Domain.Recipe
open Domain.Recipe.Repository

module RecipeHttp =
    let handlers: HttpFunc -> HttpContext -> HttpFuncResult =
        choose [
            GET >=> route "/recipes" >=>
                fun next context ->
                    let find = context.GetService<Find>()
                    let recipes = find Criteria.All
                    json recipes next context

            POST >=> route "/recipes" >=>
                fun next context ->
                    task {
                        let save = context.GetService<Save>()
                        let! recipe = context.BindJsonAsync<Recipe>()
                        let recipe = { recipe with Id = ShortGuid.fromGuid(Guid.NewGuid()) }
                        return! json (save recipe) next context
                    }
        ]
