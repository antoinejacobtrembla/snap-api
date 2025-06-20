namespace Web.Http

open System
open App.Recipe.Queries
open Domain.Recipe.Repository
open Domain.Recipe
open Giraffe
open MediatR
open Microsoft.AspNetCore.Http



module RecipeHttp =

    let handlers: HttpFunc -> HttpContext -> HttpFuncResult =
        choose
            [ GET
              >=> routef "/recipes/%s" (fun id ->
                  fun next context ->
                      task {
                          let mediator = context.GetService<IMediator>()
                          let! maybeRecipe = mediator.Send(GetRecipeByIdQuery(id))

                          return!
                              match maybeRecipe with
                              | Some recipe -> json recipe next context
                              | None -> RequestErrors.NOT_FOUND "Recipe not found" next context
                      })

              POST
              >=> route "/recipes"
              >=> fun next context ->
                  task {
                      let save = context.GetService<Save>()
                      let! recipe = context.BindJsonAsync<Recipe>()

                      let recipe =
                          { recipe with
                              Id = ShortGuid.fromGuid (Guid.NewGuid()) }

                      return! json (save recipe) next context
                  } ]
