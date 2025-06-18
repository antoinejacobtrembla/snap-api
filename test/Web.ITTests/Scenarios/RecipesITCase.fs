namespace Web.ITTests.Scenarios

open Web.ITTests.Base
open Web.ITTests.Fixtures
open Xunit

type RecipesITCase() =
    inherit BaseITCase()

    [<Fact>]
    member _.Test1() =
            Assert.True(true)