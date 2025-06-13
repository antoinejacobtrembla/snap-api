open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open System.Collections

open MediatR
open App.Recipe

open Infra.Persistence.InMemoryRecipeRepository
open Web.Http

let routes =
    choose [
        RecipeHttp.handlers
    ]

let configureApp (app: IApplicationBuilder) =
    app.UseGiraffe routes

let configureMediaR (cfg: MediatRServiceConfiguration ) =
    cfg.RegisterServicesFromAssemblyContaining<Queries.GetRecipesQuery>() |> ignore
let configureServices (services : IServiceCollection) =
    services.AddGiraffe() |> ignore
    services.AddRecipeInMemory(Hashtable())
    services.AddMediatR(configureMediaR) |> ignore

[<EntryPoint>]
let main _ =
    WebHostBuilder()
        .UseKestrel()
        .Configure(configureApp)
        .ConfigureServices(configureServices)
        .Build()
        .Run()
    0