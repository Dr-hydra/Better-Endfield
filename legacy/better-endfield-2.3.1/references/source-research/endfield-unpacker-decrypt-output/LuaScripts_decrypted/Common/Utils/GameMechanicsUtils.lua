local GameMechanicsUtils = {}


function GameMechanicsUtils.isCurGameCanSwitchLanguage()
    local gameIdValid, gameMechanicData = Tables.gameMechanicTable:TryGetValue(GameWorld.worldInfo.curSubGameId)
    if not gameIdValid then
        return true
    end

    local gameCategoryValid, gameMechanicCategoryData = Tables.gameMechanicCategoryTable:TryGetValue(gameMechanicData.gameCategory)
    if not gameCategoryValid then
        return true
    end


    return gameMechanicCategoryData.canSwitchLanguage
end


_G.GameMechanicsUtils = GameMechanicsUtils
return GameMechanicsUtils