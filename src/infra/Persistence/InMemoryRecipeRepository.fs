namespace Infra.Persistence

open Microsoft.Extensions.DependencyInjection
open System.Collections

open Domain.Recipe
open Domain.Recipe.Repository

module InMemoryRecipeRepository =
    let find (inMemory: Hashtable) (criteria: Criteria) : Recipe[] =
        match criteria with
        | All -> inMemory.Values |> Seq.cast |> Array.ofSeq

    let save (inMemory: Hashtable) (recipe: Recipe) =
        inMemory.Add(recipe.Id, recipe) |> ignore
        recipe

    type IServiceCollection with
        member this.AddRecipeInMemory (inMemory : Hashtable) =
            this.AddSingleton<Find>(find inMemory) |> ignore
            this.AddSingleton<Save>(save inMemory) |> ignore