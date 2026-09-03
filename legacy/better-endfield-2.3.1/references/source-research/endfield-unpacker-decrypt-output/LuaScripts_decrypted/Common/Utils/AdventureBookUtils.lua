local AdventureBookUtils = {}

AdventureBookUtils.StageTaskDisplayState = {
    InProgress = 1,
    Complete = 2,
    Rewarded = 3,
    OtherDomainRewarded = 4,
}

function AdventureBookUtils.CheckRedDotAdventureBookTabStage()

    local adventureBookData = GameInstance.player.adventure.adventureBookData
    local isComplete = adventureBookData.isCurAdventureBookStateComplete
    local curStage = adventureBookData.adventureBookStage
    local isActualStage = curStage == adventureBookData.actualBookStage
    if isActualStage and isComplete then
        return true
    end

    local hasCfg, stageTaskCfg = Tables.adventureBookStageRewardTable:TryGetValue(curStage)
    if not hasCfg then
        logger.error("[Adventure Book Stage Reward Table] missing cfg, id = "..curStage)
        return false
    end
    local slotCompleteMap = {}
    local slotRewardedMap = {}
    for _, taskId in pairs(stageTaskCfg.taskIds) do
        local taskData = Tables.adventureTaskTable[taskId]
        if taskData ~= nil then
            local slotId = string.isEmpty(taskData.stageSlotId) and taskId or taskData.stageSlotId
            if GameInstance.player.adventure:IsTaskRewarded(taskId) then
                slotRewardedMap[slotId] = true
            else
                local taskValue = AdventureBookUtils.GetTaskCurrProgress(taskData)
                local maxProgress = AdventureBookUtils.GetTaskMaxProgress(taskData)
                local taskComplete = GameInstance.player.adventure:IsTaskComplete(taskId)
                if not taskComplete then
                    taskComplete = taskValue >= maxProgress
                end
                if taskComplete then
                    slotCompleteMap[slotId] = true
                end
            end
        end
    end
    for slotId, _ in pairs(slotCompleteMap) do
        if not slotRewardedMap[slotId] then
            return true
        end
    end
    return false
end


function AdventureBookUtils.CheckRedDotAdventureBookTabDaily()

    local curDailyActivation = GameInstance.player.adventure.adventureBookData.dailyActivation

    local curDailyRewardedActivation = GameInstance.player.adventure.adventureBookData.dailyRewardedActivation

    local maxActivation = 0
    for _, cfg in pairs(Tables.dailyActivationRewardTable) do
        if cfg.activation > maxActivation then
            maxActivation = cfg.activation
        end
    end

    if curDailyRewardedActivation >= maxActivation then
        return false
    end

    local taskDic = GameInstance.player.adventure.adventureBookData.adventureTasks
    for k, v in pairs(Tables.adventureTaskTable) do
        local succ, csTask = taskDic:TryGetValue(k)
        if succ then
            if v.taskType == GEnums.AdventureTaskType.Daily and csTask.isComplete then
                return true
            end
        end
    end

    for _, cfg in pairs(Tables.dailyActivationRewardTable) do
        if cfg.activation > curDailyRewardedActivation and cfg.activation <= curDailyActivation then
            return true
        end
    end

    return false
end

function AdventureBookUtils.CheckRedDotAdventureBookTabDungeon()

    for seriesId, seriesCfg in pairs(Tables.dungeonSeriesTable) do
        local isUnlocked = seriesCfg.dungeonCategory ~= GEnums.DungeonCategoryType.None and
            seriesCfg.dungeonCategory ~= GEnums.DungeonCategoryType.Train and
                GameInstance.player.adventure:IsAdventureDungeonCategoryTypeUnlocked(seriesId, seriesCfg.dungeonCategory)
        if isUnlocked then
            for _, id in pairs(seriesCfg.includeDungeonIds) do
                if GameInstance.player.subGameSys:IsGameUnread(id) then
                    return true
                end
            end
        end
    end

    for groupId, tableData in pairs(Tables.worldEnergyPointGroupTable) do
        local groupIsActive = GameInstance.player.subGameSys:IsGameMapMarkUnlock(groupId, GEnums.MarkType.EnemySpawner) and
            AdventureBookUtils.CheckEnemySpawnerCanOpenMap(groupId)

        local succ, wepGroupCfg = Tables.worldEnergyPointGroupTable:TryGetValue(groupId)
        if succ and groupIsActive then
            local exist = false
            for level = 1, GameInstance.player.adventure.currentWorldLevel do
                local subGameId = wepGroupCfg.worldLevel2GameMechanicsIdMap[level]
                if GameInstance.player.subGameSys:IsGameUnlocked(subGameId) and
                    not GameInstance.player.subGameSys:IsGameUnread(subGameId) then
                    exist = true
                end
            end
            if not exist then
                return true
            end
        end
    end
    return false
end

function AdventureBookUtils.CheckRedDotAdventureBookTabTrain()
    for seriesId, seriesCfg in pairs(Tables.dungeonSeriesTable) do
        if seriesCfg.dungeonCategory == GEnums.DungeonCategoryType.Train then
            for _, id in pairs(seriesCfg.includeDungeonIds) do
                if DungeonUtils.isDungeonUnlock(id) and
                    GameInstance.player.subGameSys:IsGameUnread(id) then
                    return true
                end
            end
        end
    end
    return false
end

function AdventureBookUtils.CheckRedDotAdventureBookTabBlackbox()
    local dungeonMgr = GameInstance.dungeonManager
    for _, cfg in pairs(Tables.domainDataTable) do
        local canShow = true
        local hasCfg, facCfg = Tables.facSTTGroupTable:TryGetValue(cfg.facTechPackageId)
        if not hasCfg then
            logger.error("[Domain Data Table] missing, id = "..cfg.facTechPackageId)
            canShow = false
        end

        if canShow then
            local isLock = GameInstance.player.facTechTreeSystem:PackageIsLocked(cfg.facTechPackageId) or
                GameInstance.player.facTechTreeSystem:PackageIsHidden(cfg.facTechPackageId)
            if not isLock then
                local blackboxIds = facCfg.blackboxIds
                for _, blackboxId in pairs(blackboxIds) do

                    local isUnlock = DungeonUtils.isDungeonUnlock(blackboxId)
                    local isActive = DungeonUtils.isDungeonActive(blackboxId)
                    if isActive and isUnlock and not dungeonMgr:IsBlackboxRead(blackboxId) then
                        return true
                    end
                end
            end
        end
    end
    return false
end

function AdventureBookUtils.CheckRedDotAdventureBookTabActivity()
    local dataList = AdventureBookUtils.InitActivityDataList()
    for _, data in ipairs(dataList) do
        local redDotName = data.redDotName
        if not string.isEmpty(redDotName) then
            local ret, redDotType, expireTs = RedDotManager:GetRedDotState(redDotName)
            if ret then
                return ret, redDotType, expireTs
            end
        end
    end
    return false
end


function AdventureBookUtils.GetTaskCurrProgress(taskData)
    if taskData.conditionDataList.Count == 1 then
        local condition = taskData.conditionDataList[0]
        local success, value = LuaGameConditionUtils.getConditionValueByParameters(
            condition.conditionType,
            condition.parameters)
        if success then
            return value
        else
            return 0
        end
    else
        local allConditionComplete = true
        for _, condition in pairs(taskData.conditionDataList) do
            local success, value = LuaGameConditionUtils.getConditionValueByParameters(
                condition.conditionType,
                condition.parameters)
            if not success or value < condition.progressToCompare then
                allConditionComplete = false
                break
            end
        end
        return allConditionComplete and 1 or 0
    end
end


function AdventureBookUtils.GetTaskMaxProgress(taskData)
    if taskData.conditionDataList.Count == 1 then
        local condition = taskData.conditionDataList[0]
        return condition.progressToCompare
    else
        return 1
    end
end


function AdventureBookUtils.HaveDungeon()
    local typeList = {
        GEnums.DungeonCategoryType.CharResource,
        GEnums.DungeonCategoryType.BasicResource,
        GEnums.DungeonCategoryType.BossRush,
        GEnums.DungeonCategoryType.SpecialResource,
    }

    for _, type in pairs(typeList) do
        local isCategoryUnlocked = GameInstance.player.adventure:IsAdventureDungeonFirCategoryUnlock(type)

        if isCategoryUnlocked then
            return true
        end
    end

    for groupId, _ in pairs(Tables.worldEnergyPointGroupTable) do
        local id = GameInstance.player.worldEnergyPointSystem:GetCurSubGameId(groupId)
        local canShow = false
        if GameInstance.player.subGameSys:IsGameMapMarkUnlock(groupId, GEnums.MarkType.EnemySpawner) and
            GameInstance.player.subGameSys:IsGameUnlocked(id) and
            self:_CheckCanOpenMap(groupId) then
            return true
        end
    end

    return false
end

function AdventureBookUtils.CheckEnemySpawnerCanOpenMap(seriesId)
    local hasData, instId = GameInstance.player.mapManager:GetMapMarkInstId(GEnums.MarkType.EnemySpawner, seriesId)
    if not hasData then
        return false
    end

    local data = {}

    local levelId = GameInstance.player.mapManager:GetMarkInstRuntimeDataLevelId(instId)

    data.instId = instId
    data.levelId = levelId

    local ret = MapUtils.checkCanOpenMapAndParseArgs(data)
    return ret
end


function AdventureBookUtils.HaveActivityTab()
    local dataList = AdventureBookUtils.InitActivityDataList()
    for _, data in ipairs(dataList) do
        if data.willShowTab ~= false then
            return true
        end
    end
    return false
end

function AdventureBookUtils.InitActivityDataList()
    local ret = {}
    local emptyNode = nil
    for id, cfg in pairs(Tables.AdventureActivityDataTable) do
        local type = cfg.type
        local data = AdventureBookUtils.GetActivityDataByType(type)
        data.id = cfg.id
        data.type = cfg.type
        data.name = data.name or cfg.name
        data.titleImg = data.titleImg or cfg.titleImg
        data.decoImg = data.decoImg or cfg.decoImg
        data.bgImg = data.bgImg or cfg.bgImg
        data.bgNodeColor = data.bgNodeColor or cfg.bgNodeColor

        local rewardInfos = {}
        if cfg.rewardList ~= nil then
            for _, rewardId in pairs(cfg.rewardList) do
                table.insert(rewardInfos, { id = rewardId })
            end
        end
        data.rewardInfos = rewardInfos

        local isShow = data.checkShowFunc()
        if isShow then
            if cfg.type ~= "empty" then
                table.insert(ret, data)
            else
                emptyNode = data
            end
        end
    end
    table.sort(ret, function(a, b)
        return a.id < b.id
    end)
    if #ret < 3 then
        table.insert(ret, emptyNode)
    end
    return ret
end

function AdventureBookUtils.GetActivityDataByType(type)
    if type == "WeekRaid" then
        return {
            checkShowFunc = function()
                local ret = Utils.isSystemUnlocked(GEnums.UnlockSystemType.WeekRaidIntro)
                return ret
            end,
            nodeStateName = "Normal",
            setUI = true,
            ClickFunc = function()
                PhaseManager:OpenPhase(PhaseId.DungeonWeeklyRaid)
            end,
            redDotName = "WeekRaid",
        }
    elseif type == "HighDifficulty" then
        return {
            checkShowFunc = function()
                local ret = GameInstance.dungeonManager:IsDungeonCategoryUnlocked(
                    DungeonConst.DUNGEON_CATEGORY.HighDifficulty)
                if not ret then
                    return false
                end
                local ids = GameInstance.player.highDifficultySystem:GetAllUnlockSeriesIds()
                return ids.Count > 0
            end,
            nodeStateName = "Normal",
            setUI = true,
            ClickFunc = function()
                PhaseManager:OpenPhase(PhaseId.HighDifficultyMainHud,{})
            end,
            redDotName = "AdventureBookHighDifficulty",
        }
    elseif type == "SeasonTower" then
        return {
            checkShowFunc = function()
                if GameInstance.player.gameSettingSystem.forbiddenSeasonTower then
                    return false
                end
                if GameInstance.player.seasonTowerSystem.currentSeasonId <= 0 then
                    return false
                end
                local ret = Utils.isSystemUnlocked(GEnums.UnlockSystemType.SeasonTowerDungeon)
                return ret
            end,
            nodeStateName = "Normal",
            setUI = true,
            ClickFunc = function()
                PhaseManager:GoToPhase(PhaseId.SeasonTowerMainHud,{})
            end,
            redDotName = "SeasonTowerMain",
            bgImg = (Utils.getPlayerGender() == CS.Proto.GENDER.GenMale) and "bg_adventure_activity_3_a" or "bg_adventure_activity_3_b",
        }
    elseif type == "empty" then
        return {
            checkShowFunc = function()
                return true
            end,
            nodeStateName = "EmptyNode",
            setUI = false,
            willShowTab = false,
        }
    else
        return nil
    end
end


_G.AdventureBookUtils = AdventureBookUtils
return AdventureBookUtils