namespace Web.ITTests.Fixtures

open Mongo2Go

type MongoDbFixture() =
    member val _runner:MongoDbRunner = null with get,set
    
    member this.start ()=
        this._runner <- MongoDbRunner.Start()
        this._runner.ConnectionString
           
    member this.stop() =
        this._runner.Dispose()