
local ItemObtainWaysUtils = {}




function ItemObtainWaysUtils.CheckObtainWayCondition(conditionId)
    local succ, conditionCfg = Tables.noObtainWayCondTable:TryGetValue(conditionId)
    if not succ then
        return false
    end

    local unlockTag = false
    if ItemObtainWaysUtils[conditionCfg.checkFunction] ~= nil then
        local args
        if not string.isEmpty(conditionCfg.checkArgs) then
            args = Json.decode(conditionCfg.checkArgs)
        end
        unlockTag = ItemObtainWaysUtils[conditionCfg.checkFunction](args)
    end

    return unlockTag
end




function ItemObtainWaysUtils._CheckFacTechTreeUnlocked(args)
    if args == nil or args.techId == nil or string.isEmpty(args.techId) then
        return false
    end
    return not GameInstance.player.facTechTreeSystem:NodeIsLocked(args.techId)
end

function ItemObtainWaysUtils._CheckWikiUnlocked(args)
    if args == nil or args.wikiId == nil or string.isEmpty(args.wikiId) then
        return false
    end
    return WikiUtils.isWikiEntryUnlock(args.wikiId)
end

function ItemObtainWaysUtils._CheckPlayerOwnItem(args)
    if args == nil or args.itemId == nil or string.isEmpty(args.itemId) then
        return false
    end
    local count = Utils.getItemCount(args.itemId)
    return count > 0
end

function ItemObtainWaysUtils._CheckPlayerDungeonUnlocked(args)
    if args == nil or args.dungeonId == nil or string.isEmpty(args.dungeonId) then
        return false
    end
    return GameInstance.dungeonManager:IsDungeonUnlocked(args.dungeonId)
end

function ItemObtainWaysUtils._CheckDungeonEntryTouched(args)
    if args == nil or args.dungeonId == nil or string.isEmpty(args.dungeonId) then
        return false
    end
    return GameInstance.dungeonManager:IsDungeonInteractiveActive(args.dungeonId)
end

function ItemObtainWaysUtils._CheckDungeonEntryNotTouched(args)
    if args == nil or args.dungeonId == nil or string.isEmpty(args.dungeonId) then
        return false
    end
    return not GameInstance.dungeonManager:IsDungeonInteractiveActive(args.dungeonId)
end

function ItemObtainWaysUtils._CheckDomainShopTargetLevel(args)
    if args == nil or args.channelId == nil or string.isEmpty(args.channelId) or args.targetLevel == nil then
        return false
    end

    local shopSys = GameInstance.player.shopSystem
    local _, shopChannelCfg = Tables.shopChannelDevelopmentTable:TryGetValue(args.channelId)
    local shopGroupData = shopSys:GetShopGroupData(shopChannelCfg.shopGroupId)
    local channelData = shopGroupData.domainChannelData
    local hasValue, curLv = channelData.channelLevelMap:TryGetValue(args.channelId)
    if not hasValue then
        curLv = 0
    end

    return curLv >= args.targetLevel
end

function ItemObtainWaysUtils._CheckPlayerSkipChapter(args)
    if args == nil or args.missionChapter == nil or args.skip == nil then
        return false
    end

    local enums =  GEnums.MissionChapter.__CastFrom(args.missionChapter)
    if enums == nil then
        return false
    end

    local earlyAcceptChapterMask = GameInstance.player.mission.earlyAcceptChapterMask
    return (earlyAcceptChapterMask:HasFlag(enums) == args.skip)
end

function ItemObtainWaysUtils._CheckLevelUnlock(args)
    if args == nil or args.levelId == nil then
        return false
    end

    return MapUtils.checkIsValidLevelId(args.levelId)
end

function ItemObtainWaysUtils._CheckSpaceshipRoomTypeBuild(args)
    if args == nil or args.roomType == nil then
        return false
    end

    local enums =  GEnums.SpaceshipRoomType.__CastFrom(args.roomType)
    if enums == nil then
        return false
    end

    return GameInstance.player.spaceship:CheckRoomBuildByType(enums)
end

function ItemObtainWaysUtils._CheckMissionCompleted(args)
    if args == nil or args.missionId == nil or args.completed == nil then
        return false
    end

    local completed = GameInstance.player.mission:IsMissionCompleted(args.missionId)
    return completed == args.completed
end


function ItemObtainWaysUtils._CheckAndConditionList(args)
    if args == nil or args.conditionList == nil or type(args.conditionList) ~= "table" then
        return false
    end

    for _, conditionId in ipairs(args.conditionList) do
        local succ, conditionCfg = Tables.noObtainWayCondTable:TryGetValue(conditionId)
        if not succ then
            return false
        end
        if conditionCfg.checkFunction == "_CheckAndConditionList" or conditionCfg.checkFunction == "_CheckOrConditionList" then
            return false
        end
        if ItemObtainWaysUtils[conditionCfg.checkFunction] == nil then
            return false
        end
        local subArgs
        if not string.isEmpty(conditionCfg.checkArgs) then
            subArgs = Json.decode(conditionCfg.checkArgs)
        end
        if not ItemObtainWaysUtils[conditionCfg.checkFunction](subArgs) then
            return false
        end
    end

    return true
end


function ItemObtainWaysUtils._CheckOrConditionList(args)
    if args == nil or args.conditionList == nil or type(args.conditionList) ~= "table" then
        return false
    end

    for _, conditionId in ipairs(args.conditionList) do
        local succ, conditionCfg = Tables.noObtainWayCondTable:TryGetValue(conditionId)
        if not succ then
            return false
        end
        if conditionCfg.checkFunction == "_CheckAndConditionList" or conditionCfg.checkFunction == "_CheckOrConditionList" then
            return false
        end
        if ItemObtainWaysUtils[conditionCfg.checkFunction] ~= nil then
            local subArgs
            if not string.isEmpty(conditionCfg.checkArgs) then
                subArgs = Json.decode(conditionCfg.checkArgs)
            end
            if ItemObtainWaysUtils[conditionCfg.checkFunction](subArgs) then
                return true
            end
        end
    end

    return false
end





_G.ItemObtainWaysUtils = ItemObtainWaysUtils
return ItemObtainWaysUtils
