local DungeonUtils = {}

function DungeonUtils.checkCanOpenPhase(args)

    local dungeonId = args.dungeonId or ""
    local dungeonSeriesId = args.dungeonSeriesId or ""

    local dungeonIdValid = Tables.dungeonTable:ContainsKey(dungeonId)
    local dungeonSeriesIdValid = Tables.dungeonSeriesTable:ContainsKey(dungeonSeriesId)

    if not dungeonIdValid and not dungeonSeriesIdValid then
        logger.error("open failed: both dungeonId and dungeonSeriesId invalid")
        return false
    end

    local activityId = args.activityId or ""
    if not string.isEmpty(activityId) and not GameInstance.player.activitySystem:GetActivity(activityId) then
        return false, Language.LUA_ACTIVITY_FORBIDDEN
    end

    return true
end

function DungeonUtils.isDungeonUnlock(dungeonId)
    return GameInstance.dungeonManager:IsDungeonUnlocked(dungeonId)
end

function DungeonUtils.isDungeonPassed(dungeonId)
    return GameInstance.dungeonManager:IsDungeonPassed(dungeonId)
end

function DungeonUtils.isDungeonExtraRewardGained(dungeonId)
    return GameInstance.dungeonManager:IsDungeonExtraRewardGained(dungeonId)
end


function DungeonUtils.isDungeonActive(dungeonId)
    return GameInstance.dungeonManager:IsDungeonActive(dungeonId)
end

function DungeonUtils.isDungeonHasHunterMode(dungeonId)
    return not string.isEmpty(Tables.dungeonTable[dungeonId].hunterModeRewardId)
end

function DungeonUtils.isHunterModeUnlocked()
    return GameInstance.dungeonManager:IsHunterModeUnlocked()
end

function DungeonUtils.isDungeonCostStamina(dungeonId)
    local dungeonCfg = Tables.dungeonTable[dungeonId]
    local hasHunterMode = DungeonUtils.isDungeonHasHunterMode(dungeonId)
    local hunterModeOpen = DungeonUtils.isHunterModeUnlocked()

    if hasHunterMode and hunterModeOpen and dungeonCfg.hunterModeCostStamina > 0 then
        return true, dungeonCfg.hunterModeCostStamina
    end

    if not hasHunterMode and dungeonCfg.costStamina > 0 then
        return true, dungeonCfg.costStamina
    end

    return false, 0
end

function DungeonUtils.diffActionByConditionId(conditionId)
    local conditionCfg = Tables.gameMechanicConditionTable[conditionId]
    local conditionType = conditionCfg.conditionType
    local param = conditionCfg.parameter[0]
    if conditionType == GEnums.ConditionType.CheckPassGameMechanicsId then
        local preDungeonId = param.valueStringList[0]
        local dungeonTypeCfg = Tables.dungeonTypeTable[Tables.dungeonTable[preDungeonId].dungeonCategory]
        local _, instId = GameInstance.player.mapManager:GetMapMarkInstId(dungeonTypeCfg.mapMarkType, Tables.dungeonTable[preDungeonId].dungeonSeriesId)
        MapUtils.openMap(instId)
    elseif conditionType == GEnums.ConditionType.CheckSceneGrade then
        local levelId = param.valueStringList[0]
        MapUtils.openMap(nil, levelId)
    elseif conditionType == GEnums.ConditionType.QuestStateEqual then
        local questId = param.valueStringList[0]
        local missionId = GameInstance.player.mission:GetMissionIdByQuestId(questId)
        PhaseManager:OpenPhase(PhaseId.Mission, {autoSelect = missionId, useBlackMask = true})
    elseif conditionType == GEnums.ConditionType.MissionStateEqual then
        local missionId = param.valueStringList[0]
        PhaseManager:OpenPhase(PhaseId.Mission, {autoSelect = missionId, useBlackMask = true})
    else
        Notify(MessageConst.SHOW_TOAST, "Error")
    end
end

function DungeonUtils.getConditionCanJump(dungeonId, conditionId)
    local conditionCfg = Tables.gameMechanicConditionTable[conditionId]
    local conditionType = conditionCfg.conditionType
    if conditionCfg.parameter.length == 0 then
        return false
    end
    local param = conditionCfg.parameter[0]
    if conditionType == GEnums.ConditionType.CheckPassGameMechanicsId then
        local preDungeonId = param.valueStringList[0]
        local dungeonCfg = Tables.dungeonTable[dungeonId]
        local preDungeonCfg = Tables.dungeonTable[preDungeonId]
        return dungeonCfg.dungeonSeriesId ~= preDungeonCfg.dungeonSeriesId
    end
    return true
end

function DungeonUtils.getUncompletedConditionIds(dungeonId)
    local uncompletedConditionIds = {}
    local _, gameUnlockCondition = GameInstance.player.subGameSys:TryGetSubGameUnlockCondition(dungeonId)
    for conditionId, completed in pairs(gameUnlockCondition.unlockConditionFlags) do
        if not completed then
            table.insert(uncompletedConditionIds, conditionId)
        end
    end

    return uncompletedConditionIds
end

function DungeonUtils.groupDungeonsByCondition(dungeonIds)

    local rootDungeonIds = {}
    for _, v in ipairs(dungeonIds) do
        if Tables.GameMechanicGroupByConditionTable:ContainsKey(v) then
            table.insert(rootDungeonIds, v)
        end
    end

    local groupDungeonIds = {}
    for _, rootDungeonId in ipairs(rootDungeonIds) do
        local group = {}
        table.insert(group, rootDungeonId)
        local _, data = Tables.GameMechanicGroupByConditionTable:TryGetValue(rootDungeonId)
        for _, childDungeonId in pairs(data.childGameMechanicsId) do
            local exist = false
            for _, v in ipairs(dungeonIds) do
                if v == childDungeonId then
                    exist = true
                end
            end
            if exist then
                table.insert(group, childDungeonId)
            end
        end
        table.insert(groupDungeonIds, group)
    end
    return #rootDungeonIds > 0, groupDungeonIds
end

function DungeonUtils.getEntryLocation(levelId, ignoreDomain)
    if string.isEmpty(levelId) then
        return ""
    end

    local domainId = DataManager.levelBasicInfoTable:get_Item(levelId).domainName
    local levelName = Tables.levelDescTable[levelId].showName

    if ignoreDomain then
        return levelName
    else
        local succ, domainDataCfg = Tables.domainDataTable:TryGetValue(domainId)
        if succ then
            return domainDataCfg.domainName.."-"..levelName
        else

            return levelName
        end
    end
end

function DungeonUtils.getListByStr(str)
    return string.isEmpty(str) and {} or string.split(str, "\n")
end

function DungeonUtils.getEntryText(dungeonId)
    local dungeonCfg = Tables.dungeonTable[dungeonId]
    local succ, dungeonTypeCfg = Tables.dungeonTypeTable:TryGetValue(dungeonCfg.dungeonCategory)
    local entryText = succ and dungeonTypeCfg.entryText or dungeonCfg.dungeonCategory
    return entryText
end


function DungeonUtils.getDungeonChestCount(sceneId)

    local collectionManager = GameInstance.player.collectionManager

    local sceneCollectionData = collectionManager:GetSceneData(sceneId)
    if not sceneCollectionData then
        return 0, 0
    end

    local chestTag = Tables.dungeonConst.dungeonChestCollectionTag
    local _, chestIdList = Tables.collectionLabelTable:TryGetValue(chestTag)
    local gainedCount = 0
    local maxCount = 0
    for _, idCfg in pairs(chestIdList.list) do
        local gained = sceneCollectionData:GetItemCurCnt(idCfg.prefabId)
        local total = sceneCollectionData:GetItemTotalCnt(idCfg.prefabId)
        gainedCount = gainedCount + gained
        maxCount = maxCount + total
    end

    return gainedCount, maxCount
end


function DungeonUtils.TryShowDungeonInsufficientStaminaPopup(dungeonId, confirmCallback)

    local dungeonCfg = Tables.dungeonTable[dungeonId]
    local serializedHintKey = string.format(DungeonConst.IGNORE_STAMINA_SHORT_HINT_FORMAT, dungeonCfg.dungeonSeriesId)
    local succ, ignoreHint = ClientDataManagerInst:GetBool(serializedHintKey, false, false, DungeonConst.SERIALIZED_CATEGORY)

    if ignoreHint then
        confirmCallback()
    else
        local hasHunterMode = DungeonUtils.isDungeonHasHunterMode(dungeonId)
        local hintContent
        if hasHunterMode then
            if GameInstance.dungeonManager:IsDungeonFirstPassRewardGained(dungeonId) then
                hintContent = Language.LUA_DUNGEON_HUNTER_MODE_STAMINA_SHORT_CONFIRM_HINT
            else
                hintContent = Language.LUA_DUNGEON_HUNTER_MODE_STAMINA_SHORT_WITH_REWARD_CONFIRM_HINT
            end
        else
            hintContent = Language.LUA_DUNGEON_STAMINA_SHORT_CONFIRM_HINT
        end

        local closuresIsOn = false
        Notify(MessageConst.SHOW_POP_UP, {
            toggle = {
                onValueChanged = function(isOn)
                    closuresIsOn = isOn
                end,
                toggleText = Language.LUA_DUNGEON_TODAY_IGNORE_SHORT_STAMINA_HINT,
                isOn = false,
            },
            content = hintContent,
            onConfirm = function()
                ClientDataManagerInst:SetBool(serializedHintKey, closuresIsOn, false, DungeonConst.SERIALIZED_CATEGORY, EClientDataTimeValidType.CurrentDay)
                confirmCallback()
            end,
            onCancel = function()
            end
        })
    end
end

function DungeonUtils.isDungeonPerfectComplete(dungeonId)

    local dungeonManager = GameInstance.dungeonManager
    local isComplete = dungeonManager:IsDungeonPassed(dungeonId)
    local _, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
    local _, rewardCfg = Tables.rewardTable:TryGetValue(dungeonCfg.extraRewardId)
    local hasExtraReward = rewardCfg ~= nil
    local collectChestNum, maxChestNum = DungeonUtils.getDungeonChestCount(dungeonCfg.sceneId)
    local isPerfectComplete = isComplete and
        (not hasExtraReward or dungeonManager:IsDungeonExtraRewardGained(dungeonId)) and
        (maxChestNum < 1 or collectChestNum >= maxChestNum)
    return isPerfectComplete
end




function DungeonUtils.genFirstPartRewardsInfo(dungeonId)
    local firstRowRewards = {}

    local dungeonMgr = GameInstance.dungeonManager
    local dungeonCfg = Tables.dungeonTable[dungeonId]

    local gained = dungeonMgr:IsDungeonFirstPassRewardGained(dungeonId)
    local rewardId = dungeonCfg.firstPassRewardId
    if not string.isEmpty(rewardId) then
        local rewardCfg = Tables.rewardTable[rewardId]
        for _, itemBundle in pairs(rewardCfg.itemBundles) do
            local itemCfg = Tables.itemTable[itemBundle.id]
            table.insert(firstRowRewards, {
                id = itemBundle.id,
                count = itemBundle.count,
                gained = gained,
                sortId1 = itemCfg.sortId1,
                sortId2 = itemCfg.sortId2,
            })
        end
    end


    local hasRecycleReward = not string.isEmpty(dungeonCfg.rewardId)
    if hasRecycleReward then
        local rewardCfg = Tables.rewardTable[dungeonCfg.rewardId]
        for _, itemBundle in pairs(rewardCfg.itemBundles) do
            local itemCfg = Tables.itemTable[itemBundle.id]
            table.insert(firstRowRewards, {
                id = itemBundle.id,
                count = itemBundle.count,
                sortId1 = itemCfg.sortId1,
                sortId2 = itemCfg.sortId2,
            })
        end
    end

    table.sort(firstRowRewards, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))

    return firstRowRewards
end


function DungeonUtils.genSecondPartRewardsInfo(dungeonId)
    local secondRowRewards = {}

    local dungeonMgr = GameInstance.dungeonManager
    local dungeonCfg = Tables.dungeonTable[dungeonId]

    local rewardId = dungeonCfg.extraRewardId

    local hasExtraReward = not string.isEmpty(rewardId)
    local hunterModeRewardId = dungeonCfg.hunterModeRewardId
    local hasHunterModeReward = not string.isEmpty(hunterModeRewardId)
    if hasExtraReward then

        local gained = dungeonMgr:IsDungeonExtraRewardGained(dungeonId)
        local rewardCfg = Tables.rewardTable[rewardId]
        for _, itemBundle in pairs(rewardCfg.itemBundles) do
            local itemCfg = Tables.itemTable[itemBundle.id]
            table.insert(secondRowRewards, {
                id = itemBundle.id,
                count = itemBundle.count,
                gained = gained,
                typeTag = DungeonConst.DUNGEON_REWARD_TAG_STATE.Extra,
                sortId1 = itemCfg.sortId1,
                sortId2 = itemCfg.sortId2,
            })
        end
        table.sort(secondRowRewards, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
    elseif hasHunterModeReward then
        local rewardCfg = Tables.rewardTable[hunterModeRewardId]


        for _, itemBundle in pairs(rewardCfg.itemBundles) do
            local itemCfg = Tables.itemTable[itemBundle.id]
            table.insert(secondRowRewards, {
                id = itemCfg.id,
                typeSortId = 0,
                typeTag = DungeonConst.DUNGEON_REWARD_TAG_STATE.Regular,
                sortId1 = itemCfg.sortId1,
                sortId2 = itemCfg.sortId2,
            })
        end

        for _, itemBundle in pairs(rewardCfg.probItemBundles) do
            local itemCfg = Tables.itemTable[itemBundle.id]
            table.insert(secondRowRewards, {
                id = itemCfg.id,
                typeSortId = -1,
                typeTag = DungeonConst.DUNGEON_REWARD_TAG_STATE.Random,
                sortId1 = itemCfg.sortId1,
                sortId2 = itemCfg.sortId2,
            })
        end

        local sortKeys = UIConst.COMMON_ITEM_SORT_KEYS
        table.insert(sortKeys, 1, "typeSortId")
        table.sort(secondRowRewards, Utils.genSortFunction(sortKeys))
    end

    return secondRowRewards
end

function DungeonUtils.getRewardsDetailFirstRowTitle(dungeonId)
    local dungeonCfg = Tables.dungeonTable[dungeonId]
    local langKey = DungeonConst.DUNGEON_FIRST_ROW_REWARDS_TITLE[dungeonCfg.dungeonCategory]
    if langKey == nil then
        return
    end
    return Language[langKey]
end

function DungeonUtils.getRewardsDetailSecondRowTitle(dungeonId)
    local dungeonCfg = Tables.dungeonTable[dungeonId]
    local extraRewardId = dungeonCfg.extraRewardId
    local hunterModeId = dungeonCfg.hunterModeRewardId
    if not string.isEmpty(extraRewardId) then
        return Language.LUA_DUNGEON_REWARD_SHOW_EXTRAREWARD
    elseif not string.isEmpty(hunterModeId) then
        return Language.LUA_DUNGEON_REWARD_SHOW_HUNTERMODE
    else
        return "TBD"
    end
end

function DungeonUtils.startSubGameLeaveTick(action)
    local tickId = LuaUpdate:Add("LateTick", function(deltaTime)
        local game = GameInstance.dungeonManager.curDungeonLikeSubGame
        local leftTime = 0
        if game ~= nil then
            leftTime = game:GetRealLeaveTimestampMsForLua() - DateTimeUtils.GetCurrentTimestampByMilliseconds()
        end
        if leftTime >= 0 and action ~= nil then
            action(math.floor(leftTime / 1000))
        end
    end)
    return tickId
end












local DUNGEON_POPUP_HANDLERS = {
    [GEnums.DungeonPopupType.Default] = {
        tryAutoShow = function(dungeonId)
            if not DungeonUtils.isDungeonHasFeatureInfo(dungeonId) then
                return
            end
            LuaSystemManager.commonTaskTrackSystem:AddRequest("DungeonInfo", function()
                UIManager:AutoOpen(PanelId.DungeonInfoPopup, {
                    dungeonId = dungeonId,
                    closeCb = function()
                        Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, "DungeonInfo")
                    end,
                })
            end, function()
                UIManager:Close(PanelId.DungeonInfoPopup)
            end)
        end,
        show = function(dungeonId, arg)
            UIManager:AutoOpen(PanelId.DungeonInfoPopup, { dungeonId = dungeonId, needBindAction = arg and arg.needBindAction })
        end,
        checkVisibility = function(dungeonId)
            return DungeonUtils.isDungeonHasFeatureInfo(dungeonId)
        end,
    },
    [GEnums.DungeonPopupType.BountyEnemy] = {
        tryAutoShow = function(dungeonId)
            local bountyEnemies = GameWorld.battle.bountyEnemies
            if not bountyEnemies or bountyEnemies.Count <= 0 then
                return
            end
            LuaSystemManager.commonTaskTrackSystem:AddRequest("SeasonTowerEnemyBuffPopup", function()
                UIManager:AutoOpen(PanelId.SeasonTowerEnemyBuffPopup, {
                    dungeonId = dungeonId,
                    closeCb = function()
                        Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, "SeasonTowerEnemyBuffPopup")
                    end,
                })
            end, function()
                UIManager:Close(PanelId.SeasonTowerEnemyBuffPopup)
            end)
        end,
        show = function(dungeonId, arg)
            local bountyEnemies = GameWorld.battle.bountyEnemies
            if bountyEnemies and bountyEnemies.Count > 0 then
                UIManager:AutoOpen(PanelId.SeasonTowerEnemyBuffPopup, { dungeonId = dungeonId })
                return
            end
        end,
        checkVisibility = function(dungeonId)
            return true
        end,
    },
    [GEnums.DungeonPopupType.SeasonTower] = {
        tryAutoShow = function(dungeonId)
            LuaSystemManager.commonTaskTrackSystem:AddRequest("SeasonTowerDungeonInfoPopup", function()
                UIManager:AutoOpen(PanelId.SeasonTowerDungeonInfoPopup, {
                    dungeonId = dungeonId,
                    closeCb = function()
                        Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, "SeasonTowerDungeonInfoPopup")
                    end,
                })
            end, function()
                UIManager:Close(PanelId.SeasonTowerDungeonInfoPopup)
            end)
        end,
        show = function(dungeonId, arg)
            UIManager:AutoOpen(PanelId.SeasonTowerDungeonInfoPopup, { dungeonId = dungeonId, needBindAction = arg and arg.needBindAction })
        end,
        checkVisibility = function(dungeonId)
            return true
        end,
    },
}




function DungeonUtils.tryAutoShowDungeonPopup(dungeonId)
    if not DungeonUtils.checkCanAutoPopupInfoPanel(dungeonId) then
        return
    end
    local succ, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
    if not succ then
        return
    end
    local handler = DUNGEON_POPUP_HANDLERS[dungeonCfg.popupType]
    if handler and handler.tryAutoShow then
        handler.tryAutoShow(dungeonId)
    end
end



function DungeonUtils.showDungeonPopupByEvent()
    local dungeonId = GameInstance.dungeonManager.curDungeonId
    if string.isEmpty(dungeonId) then
        return
    end
    if not DungeonUtils.checkCanShowDungeonPopup(dungeonId) then
        return
    end
    local succ, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
    if not succ then
        return
    end
    local handler = DUNGEON_POPUP_HANDLERS[dungeonCfg.popupType]
    if handler and handler.show then
        handler.show(dungeonId)
    end
end


local FUNC_ON_CLICK_DUNGEON_INFO_BY_CATEGORY = {
    [DungeonConst.DUNGEON_CATEGORY.CharTutorial] = "onClickDungeonInfoBtnCharTutorial",
    [DungeonConst.DUNGEON_CATEGORY.CharTrial] = "onClickDungeonInfoBtnCharTrial",
    [DungeonConst.DUNGEON_CATEGORY.ContingencyContract] = "onClickDungeonInfoBtnContingencyContract",
}

function DungeonUtils.onClickDungeonInfoBtn()
    local dungeonId = GameInstance.dungeonManager.curDungeonId
    local succ, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
    if not succ then
        logger.error("DungeonUtils.onClickDungeonInfoBtn invalid dungeonId", dungeonId)
        return
    end

    local funcName = FUNC_ON_CLICK_DUNGEON_INFO_BY_CATEGORY[dungeonCfg.dungeonCategory]
    if funcName then
        DungeonUtils[funcName](dungeonId)
        return
    end

    local handler = DUNGEON_POPUP_HANDLERS[dungeonCfg.popupType]
    if handler and handler.show then
        handler.show(dungeonId, { needBindAction = true })
        return
    end

    UIManager:AutoOpen(PanelId.DungeonInfoPopup, { dungeonId = dungeonId, needBindAction = true })
end

function DungeonUtils.onClickDungeonInfoBtnCharTutorial(dungeonId)
    local curStage = GameInstance.dungeonManager.curDungeonLikeSubGame.stage
    local charTutorialCfg = Tables.dungeonCharTutorialTable[dungeonId]
    local stageCfg = charTutorialCfg.tutorialStageData[CSIndex(curStage)]

    GameAction.ManuallyStartGuideGroup(stageCfg.guideGroupId)
end

function DungeonUtils.onClickDungeonInfoBtnCharTrial(dungeonId)
    local charTrialCfg = Tables.dungeonCharTrialTable[dungeonId]
    GameAction.ManuallyStartGuideGroup(charTrialCfg.guideGroupId)
end

function DungeonUtils.onClickDungeonInfoBtnContingencyContract(dungeonId)

    local ccSystem = GameInstance.player.contingencyContractSystem
    local _, gameData = ccSystem.ccDataDict:TryGetValue(dungeonId)
    local tagIds = {}
    if gameData then
        for _, tagId in pairs(gameData.curSelectTagList) do
            table.insert(tagIds, tagId)
        end
    end

    PhaseManager:OpenPhase(PhaseId.ContingencyContractDetailsPopup, {
        gameId = dungeonId,
        tagIds = tagIds,
    })
end


local FUNC_CHECK_VISIBILITY_DUNGEON_INFO_BTN = {
    [DungeonConst.DUNGEON_CATEGORY.CharTutorial] = function(dungeonId)
        local game = GameInstance.dungeonManager.curDungeonLikeSubGame
        if not game then
            return false
        end



        local stage = game.stage
        local charTutorialCfg = Tables.dungeonCharTutorialTable[dungeonId]
        local tutorialStageCfg = charTutorialCfg.tutorialStageData[CSIndex(stage)]
        return not string.isEmpty(tutorialStageCfg.guideGroupId)
    end,
    [DungeonConst.DUNGEON_CATEGORY.ContingencyContract] = function(dungeonId)


        return DeviceInfo.usingController
    end,
    [DungeonConst.DUNGEON_CATEGORY.CharTrial] = function(dungeonId)
        return not string.isEmpty(Tables.dungeonCharTrialTable[dungeonId].guideGroupId)
    end,
}

function DungeonUtils.checkVisibilityDungeonInfoBtn()
    if not Utils.isInDungeon() then
        return false
    end

    local dungeonId = GameInstance.dungeonManager.curDungeonId
    local succ, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
    if not succ then
        logger.error("DungeonUtils.checkVisibilityDungeonInfoBtn invalid dungeonId", dungeonId)
        return false
    end

    local checkVisibilityFunc = FUNC_CHECK_VISIBILITY_DUNGEON_INFO_BTN[dungeonCfg.dungeonCategory]
    if checkVisibilityFunc then
        return checkVisibilityFunc(dungeonId)
    end

    local handler = DUNGEON_POPUP_HANDLERS[dungeonCfg.popupType]
    if handler and handler.checkVisibility then
        return handler.checkVisibility(dungeonId)
    end

    return DungeonUtils.isDungeonHasFeatureInfo(dungeonId)
end

function DungeonUtils.isDungeonHasFeatureInfo(dungeonId)
    if string.isEmpty(dungeonId) then
        return false
    end

    local succ, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
    return succ and not string.isEmpty(dungeonCfg.featureDesc)
end



function DungeonUtils.checkCanShowDungeonPopup(dungeonId)
    if DungeonUtils.isDungeonCharTutorial(dungeonId) then
        return false
    end

    if GameInstance.dungeonManager:IsDungeonPassed(dungeonId) then
        return false
    end

    return true
end



function DungeonUtils.checkCanAutoPopupInfoPanel(dungeonId)
    if not DungeonUtils.checkCanShowDungeonPopup(dungeonId) then
        return false
    end

    local succ, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
    if succ and dungeonCfg.forceIgnoreFeaturePopup then
        return false
    end

    return true
end




function DungeonUtils.showExitDungeonBtn()


    return Utils.isInDungeon() and not Utils.isInDungeonFactory()
end

function DungeonUtils.onClickExitDungeonBtn()
    local dungeonId = GameInstance.dungeonManager.curDungeonId
    if string.isEmpty(dungeonId) then
        return
    end


    if GameInstance.dungeonManager.curDungeonLikeSubGame == nil then
        return
    end

    if WeeklyRaidUtils.IsInWeeklyRaid() then
        Notify(MessageConst.SHOW_WEEK_RAID_LEAVE_CONFIRM)
        return
    end

    if GameInstance.mode.modeType == GEnums.GameModeType.Racing then
        Notify(MessageConst.SHOW_RACING_DUNGEON_EXIT_PANEL)
        return
    end

    if not string.isEmpty(GameInstance.player.systemActionConflictManager.curProcessingSystemAction) then
        logger.warn("DungeonUtils.onClickExitDungeonBtn systemConflict:",
                    GameInstance.player.systemActionConflictManager:GetCurProcessingSystemActionInfo())
        return
    end

    if LuaSystemManager.commonTaskTrackSystem:HasRequest() then
        return
    end

    local dungeonCfg = Tables.dungeonTable[dungeonId]
    local confirmHint
    local succ, dungeonTypeCfg = Tables.dungeonTypeTable:TryGetValue(dungeonCfg.dungeonCategory)
    if succ then
        confirmHint = GameInstance.dungeonManager.curDungeonLikeSubGame.isPass and dungeonTypeCfg.afterSuccStopConfirmText or
                dungeonTypeCfg.beforeSuccStopConfirmText
    else
        confirmHint = "副本类型表中没有配置：" .. dungeonCfg.dungeonCategory
    end
    local arg = {
        content = confirmHint,
        onConfirm = function()
            GameInstance.dungeonManager:LeaveDungeon()
        end,
        freezeWorld = true,
        pauseGame = true,
        showGameSettingBtn = true,
        interrupt = {
            interruptMessage = { MessageConst.SHOW_DEATH_INFO },
        },
    }

    if succ and dungeonTypeCfg.dungeonType == "dungeon_weeklyraid" then
        AudioAdapter.PostEvent("Au_UI_Menu_StripMenuPauseTick_Open")
        arg.onCancel = function()
            AudioAdapter.PostEvent("Au_UI_Menu_StripMenuPauseTick_Close")
        end
    end

    Notify(MessageConst.SHOW_POP_UP, arg)
end








function DungeonUtils.dungeonTypeValidate(dungeonId, dungeonCategory)
    local succ, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
    return succ and dungeonCfg.dungeonCategory == dungeonCategory
end

function DungeonUtils.isDungeonTrain(dungeonId)
    return DungeonUtils.dungeonTypeValidate(dungeonId, DungeonConst.DUNGEON_CATEGORY.Train)
end

function DungeonUtils.isDungeonCharTutorial(dungeonId)
    return DungeonUtils.dungeonTypeValidate(dungeonId, DungeonConst.DUNGEON_CATEGORY.CharTutorial)
end

function DungeonUtils.isDungeonChar(dungeonId)
    return DungeonUtils.dungeonTypeValidate(dungeonId, DungeonConst.DUNGEON_CATEGORY.Char)
end

function DungeonUtils.isDungeonChallenge(dungeonId)
    return DungeonUtils.dungeonTypeValidate(dungeonId, DungeonConst.DUNGEON_CATEGORY.Challenge)
end

function DungeonUtils.isDungeonContract(dungeonId)
    return DungeonUtils.dungeonTypeValidate(dungeonId, DungeonConst.DUNGEON_CATEGORY.ContingencyContract)
end

function DungeonUtils.isDungeonRace(dungeonId)
    return DungeonUtils.dungeonTypeValidate(dungeonId, DungeonConst.DUNGEON_CATEGORY.Race)
end

function DungeonUtils.isDungeonSeasonTower(dungeonId)
    return DungeonUtils.dungeonTypeValidate(dungeonId, DungeonConst.DUNGEON_CATEGORY.SeasonTower)
end

function DungeonUtils.isDungeonRacingDungeon(dungeonId)
    if string.isEmpty(dungeonId) then
        return false
    end

    for _, cfg in pairs(Tables.activityRacingDungeonTable) do
        if cfg.gameId == dungeonId then
            return true
        end
    end
    return false
end




function DungeonUtils.TryGetDungeonCfg(dungeonId)
    return Tables.dungeonTable:TryGetValue(dungeonId)
end

function DungeonUtils.TryGetDungeonCategoryCfg(dungeonId)
    local instSucc, dungeonCfg = DungeonUtils.TryGetDungeonCfg(dungeonId)
    if not instSucc then
        return false, nil
    end
    return Tables.dungeonTypeTable:TryGetValue(dungeonCfg.dungeonCategory)
end






function DungeonUtils.getSpecialTeamType(dungeonId)
    if dungeonId == nil then
        return nil
    end

    local __, gameMechanicData = Tables.gameMechanicTable:TryGetValue(dungeonId)
    if not gameMechanicData then
        return nil
    end

    local gameCategoryId = gameMechanicData.gameCategory

    local _, SpecialTeamData = Tables.GameMechanicCategorySpecialTeamTable:TryGetValue(gameCategoryId)
    if SpecialTeamData then
        return SpecialTeamData.specialTeamType
    end

    return nil
end




_G.DungeonUtils = DungeonUtils
return DungeonUtils