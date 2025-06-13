namespace Domain.Recipe

module Repository =
    type Criteria =
        | Id of id:string
        | All

    type Save = Recipe -> Recipe
    type Find = Criteria -> Recipe[]