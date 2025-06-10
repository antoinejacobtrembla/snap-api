namespace Web.Http

open Giraffe
open Microsoft.AspNetCore.Http

module RecipeHttp =
    let handlers: HttpFunc -> HttpContext -> HttpFuncResult =
        choose [
            GET >=> route "/recipe" >=>
                fun next context ->
                    text "Recipe" next context
        ]
