namespace Web.ITTests.Base

open System.Net
open Microsoft.AspNetCore.Mvc.Testing;
open System

open Program
open Web.ITTests.Fixtures
open Xunit
type BaseITCase() =
    let db: MongoDbFixture = MongoDbFixture()
    let factory = new WebApplicationFactory<EntryPoint>()
    
    do
        let connectionString = db.start()
        Environment.SetEnvironmentVariable("connectionString", connectionString)
        
    interface System.IDisposable with
        member _.Dispose() =
            db.stop()

    member val httpClient = factory.CreateClient() with get
    
