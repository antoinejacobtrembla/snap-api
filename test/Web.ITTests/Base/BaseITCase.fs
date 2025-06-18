namespace Web.ITTests.Base

open System

open Web.ITTests.Fixtures
type BaseITCase() =
    let db: MongoDbFixture = MongoDbFixture()
    do
        let connectionString = db.start()
        Environment.SetEnvironmentVariable("connectionString", connectionString)
        async {
            Program.main [||] |> ignore
        } |> ignore
        
    interface System.IDisposable with
        member _.Dispose() =
            db.stop()

    
