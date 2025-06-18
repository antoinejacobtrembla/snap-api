open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Giraffe
open System
open System.Collections

open MediatR
open App.Recipe

open Infra.Persistence.InMemoryRecipeRepository
open Microsoft.Extensions.Hosting
open Web.Http

let routes =
    choose [
        RecipeHttp.handlers
    ]

let configureMediaR (cfg: MediatRServiceConfiguration ) =
    cfg.RegisterServicesFromAssemblyContaining<Queries.GetRecipesQuery>() |> ignore
    
let configureServices (builder : WebApplicationBuilder) =
    let connectionString = Environment.GetEnvironmentVariable "connectionString"
    Console.WriteLine $"Connection String: %A{connectionString}"
    builder.Services.AddGiraffe() |> ignore
    builder.Services.AddRecipeInMemory(Hashtable())
    builder.Services.AddMediatR(configureMediaR) |> ignore
    builder
    
let build (builder : WebApplicationBuilder) =
    builder.Build()

let run (app: WebApplication) =
    app.UseGiraffe routes
    app.Run()

[<EntryPoint>]
let main _ =
    WebApplication.CreateBuilder()
        |> configureServices
        |> build
        |> run
    0