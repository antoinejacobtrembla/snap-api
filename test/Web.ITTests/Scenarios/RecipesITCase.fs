namespace Web.ITTests.Scenarios

open System.Net
open System.Net.Http.Json
open Domain.Recipe
open Web.ITTests.Base
open Xunit

open System.Net.Http
open System.Text
open System.Text.Json

type RecipesITCase() =
    inherit BaseITCase()
    let client = base.httpClient

    let createRecipe (recipe: Recipe) =
        let json = JsonSerializer.Serialize(recipe)
        let content = new StringContent(json, Encoding.UTF8, "application/json")

        let response =
            client.PostAsync("/recipes", content)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        response.Content.ReadFromJsonAsync<Recipe>()
        |> Async.AwaitTask
        |> Async.RunSynchronously




    [<Fact>]
    let ``Given valid recipe id, When getting by id, Then return the expected recipe`` () =
        task {
            // Given
            let newRecipe = createRecipe { Id = null; Name = "recipe" }

            // When
            let! response = client.GetAsync("/recipes/" + newRecipe.Id) |> Async.AwaitTask

            // Then
            let! recipe = response.Content.ReadFromJsonAsync<Recipe>() |> Async.AwaitTask
            Assert.Equal(newRecipe.Id, recipe.Id)
        }

    [<Fact>]
    let ``Given invalid recipe id, When getting by id, Then return not found`` () =
        task {
            // Given
            let id = "invalidId"

            // When
            let! response = client.GetAsync("/recipes/" + id) |> Async.AwaitTask

            // Then
            Assert.False(response.IsSuccessStatusCode)
            Assert.Equal(response.StatusCode, HttpStatusCode.NotFound)
        }

    [<Fact>]
    let ``Given null recipe id, When getting by id, Then return not found`` () =
        task {
            // Given

            // When
            let! response = client.GetAsync("/recipes/" + null) |> Async.AwaitTask

            // Then
            Assert.False(response.IsSuccessStatusCode)
            Assert.Equal(response.StatusCode, HttpStatusCode.NotFound)
        }

    [<Fact>]
    let ``Given new recipe, When posting it, Then new recipe is created properly`` () =
        task {
            // Given
            let newRecipe: Recipe = { Id = null; Name = "recipe" }

            // When
            let recipe = createRecipe (newRecipe)

            // Then
            Assert.Equal(newRecipe.Name, recipe.Name)
        }

    [<Fact>]
    let ``Given multiple new recipe with same name, When posting them, Then new recipes are created with different Ids``
        ()
        =
        task {
            let recipes: Recipe seq =
                [ yield { Id = null; Name = "recipe1" }
                  yield { Id = null; Name = "recipe2" }
                  yield { Id = null; Name = "recipe3" } ]

            let uniqueIds =
                recipes |> Seq.map createRecipe |> Seq.map (fun r -> r.Id) |> Seq.distinct

            Assert.Equal(Seq.length recipes, Seq.length uniqueIds)
        }
