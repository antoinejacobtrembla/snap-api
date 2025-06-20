namespace App.Recipe.Handler

open System.Threading.Tasks
open Domain.Recipe.Repository
open MediatR
open App.Recipe.Queries
open Domain.Recipe

type GetRecipeByIdHandler(find: Find) =
    interface IRequestHandler<GetRecipeByIdQuery, Recipe option> with
        member this.Handle(request, cancellationToken) =
            find (Criteria.Id request.id) |> Array.tryHead |> Task.FromResult
