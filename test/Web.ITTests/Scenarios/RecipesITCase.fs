namespace Web.ITTests.Scenarios

open Domain.Recipe
open Web.ITTests.Base
open Xunit

open System.Net.Http
open System.Text
open System.Text.Json

type RecipesITCase() =
    inherit BaseITCase()
    let client = base.httpClient
    
    let givenRecipes () =
        let json = JsonSerializer.Serialize({ Id = null; Name = "Recipe1" })
        let content = new StringContent(json, Encoding.UTF8, "application/json")
        client.PostAsync("/recipes", content) |> Async.AwaitTask
        
   
    [<Fact>]
    let test1 () =
        task {
            // Given
            let! _ = givenRecipes()
            // When
            
            // Then
            let! response = client.GetAsync("/recipes") |> Async.AwaitTask
            let! body = response.Content.ReadAsStringAsync()
            Assert.True(true)
        }
        
    
    
    
    