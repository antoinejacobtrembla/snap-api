namespace App.Recipe

open MediatR
open Domain.Recipe

module Queries =
    type GetRecipesQuery(criteria: Repository.Criteria) =
        interface IRequest<Recipe[]>
        member this.criteria = criteria