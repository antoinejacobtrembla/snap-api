namespace Web.Http

open Giraffe
open MediatR
open Microsoft.AspNetCore.Http
open System

open App.Recipe.Queries
open App.Recipe

open Domain.Recipe
open Domain.Recipe.Repository

module RecipeHttp =
    let handlers: HttpFunc -> HttpContext -> HttpFuncResult =
        choose [
            GET >=> route "/recipes" >=>
                fun next context ->
                    task {
                    let criteria =
                        match context.TryGetQueryStringValue "id" with
                        | None -> Criteria.All
                        | Some name -> Criteria.Id name
                    let mediator = context.GetService<IMediator>()
                    let! recipes = mediator.Send(GetRecipesQuery(criteria))
                    return! json recipes next context
                    }

            POST >=> route "/recipes" >=>
                fun next context ->
                    task {
                        let save = context.GetService<Save>()
                        let! recipe = context.BindJsonAsync<Recipe>()
                        let recipe = { recipe with Id = ShortGuid.fromGuid(Guid.NewGuid()) }
                        return! json (save recipe) next context
                    }
        ]
