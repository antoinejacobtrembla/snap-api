namespace App.Recipe.Handler

open System.Threading.Tasks

open Domain.Recipe.Repository

open MediatR
open App.Recipe.Queries
open Domain.Recipe

type GetRecipesHandler(find: Find) =        
    interface IRequestHandler<GetRecipesQuery, Recipe[]> with
        member this.Handle(request, cancellationToken) =
            find request.criteria |> Task.FromResult
