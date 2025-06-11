namespace Domain.Recipe

module Repository =
    type Criteria =
        | All

    type Save = Recipe -> Recipe
    type Find = Criteria -> Recipe[]