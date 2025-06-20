namespace App.Recipe

open MediatR
open Domain.Recipe

module Queries =
    type GetRecipesQuery(criteria: Repository.Criteria) =
        interface IRequest<Recipe[]>
        member this.criteria = criteria

    type GetRecipeByIdQuery(id: string) =
        interface IRequest<Recipe option>
        member this.id = id
