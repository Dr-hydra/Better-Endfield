local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CoinActivityHud

local BUFF_TYPE = {
    MAGNET = 1,
    INCREASE = 2,
}


local COMMON_TASK_END_TOAST_TYPE = "TrackEndToast"
local COIN_ACTIVITY_INTRO_REQUEST_TYPE = "CoinActivityIntro"
local STATUS_REPORT_INTERVAL_MS = 1000

CoinActivityHudCtrl = HL.Class('CoinActivityHudCtrl', uiCtrl.UICtrl)

CoinActivityHudCtrl.m_updateKey = HL.Field(HL.Any)


CoinActivityHudCtrl.m_buffIncreaseStartTime = HL.Field(HL.Number) << -1


CoinActivityHudCtrl.m_buffIncreaseEndTime = HL.Field(HL.Number) << -1


CoinActivityHudCtrl.m_buffMagnetStartTime = HL.Field(HL.Number) << -1


CoinActivityHudCtrl.m_buffMagnetEndTime = HL.Field(HL.Number) << -1


CoinActivityHudCtrl.m_lastSendCoinNum = HL.Field(HL.Number) << 0


CoinActivityHudCtrl.m_lastReportCoinNum = HL.Field(HL.Number) << 0


CoinActivityHudCtrl.m_lastReportTimeMs = HL.Field(HL.Number) << 0


CoinActivityHudCtrl.m_pendingReportTimerId = HL.Field(HL.Number) << -1


CoinActivityHudCtrl.m_finishedTaskIds = HL.Field(HL.Table)


CoinActivityHudCtrl.m_buffShown = HL.Field(HL.Table)





CoinActivityHudCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ONE_COMMON_TASK_PANEL_START] = "OnEndToastStart",
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE] = "_OnConditionalMultiStageTaskProgressChange",
    [MessageConst.ON_COIN_ACTIVITY_HUD_REFRESH_UI] = "_RefreshUI",
    [MessageConst.ON_INTERACTIVE_GOLD_COIN_GET] = "_OnInteractiveGoldCoinGet",
    [MessageConst.ON_INTERACTIVE_GOLD_COIN_MAGNENT] = "_OnInteractiveGoldCoinMagnent",
    [MessageConst.ON_INTERACTIVE_GOLD_COIN_INCREASE] = "_OnInteractiveGoldCoinIncrease",
    [MessageConst.ON_INTERACTIVE_GOLD_COIN_BUFF_REFRESH] = "_OnInteractiveGoldCoinBuffRefresh",
    [MessageConst.SHOW_RACING_DUNGEON_BATTLE_ENTRY_PANEL] = "_ShowBattleEntryPanel",
    [MessageConst.TRIGGER_RACING_DUNGEON_BATTLE_END] = "_OnTriggerRacingDungeonBattleEnd",
    [MessageConst.SHOW_RACING_DUNGEON_EXIT_PANEL] = "_ShowExitPanel",
    [MessageConst.ON_RACING_DUNGEON_SELECT_ROOM] = "_OnLevelSelectRoom",

}


CoinActivityHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_finishedTaskIds = {}
    self.m_buffShown = { [BUFF_TYPE.MAGNET] = false, [BUFF_TYPE.INCREASE] = false }

    self.view.buffEntryMagnet.gameObject:SetActive(false)
    self.view.buffEntryIncrease.gameObject:SetActive(false)
    self:_InitFinishedTaskIds()
    self:_RefreshUI()


    self.m_updateKey = self:_StartUpdate(function()
        self:_OnTick()
    end)

    self:_TryCreatePopup()
end






CoinActivityHudCtrl.OnClose = HL.Override() << function(self)
    self:_RemoveUpdate(self.m_updateKey)
    self:_ClearPendingReportTimer()
end



CoinActivityHudCtrl.OnDungeonGameInit = HL.StaticMethod(HL.Table) << function(args)
    CoinActivityHudCtrl._TryCreateHud()

end

CoinActivityHudCtrl.OnSubGameReset = HL.StaticMethod() << function()
    CoinActivityHudCtrl._TryCreateHud()
end

CoinActivityHudCtrl.OnOpenSubGameTrackings = HL.StaticMethod(HL.Table) << function(args)
    local dungeonId = unpack(args)
    if not DungeonUtils.isDungeonRace(dungeonId) then
        return
    end


    local ctrl = CoinActivityHudCtrl.AutoOpen(PANEL_ID)
    ctrl:OnSubGameStageChange()
end

CoinActivityHudCtrl.OnCloseSubGameTrackings = HL.StaticMethod() << function()
    local succ, ctrl = UIManager:IsOpen(PANEL_ID)
    if succ then



        ctrl:Close()
    end
end


CoinActivityHudCtrl.OnBattleRoomEnterIntro = HL.StaticMethod() << function()
    logger.info("[CoinActivity][RacingDungeon] OnBattleRoomEnterIntro")


    GameInstance.player.racingDungeonSystem.IsInBattleRoom = true


    local activity = GameInstance.player.racingDungeonSystem:GetActivityInfo()
    local gameId = ActivityUtils.RacingDungeonGetGameId(activity.id)


    local succ, roomCfg = Tables.activityRacingDungeonRoomTable:TryGetValue(gameId)
    if not succ then
        logger.error("no cfg in activityRacingDungeonRoomTable")
        return
    end
    local foundRoomData = nil
    for _, roomData in pairs(roomCfg.roomDataList) do
        if roomData.roomId == GameInstance.player.racingDungeonSystem.currRoomId then
            foundRoomData = roomData
        end
    end
    if foundRoomData == nil then
        return
    end
    local guideGroupId = foundRoomData.introId


    local hasValue, value = GameInstance.player.globalVar:TryGetClientVar("racing_dungeon_" .. guideGroupId)
    if value ~= 0 then
        logger.info("[CoinActivity][RacingDungeon] OnBattleRoomEnterIntro. guideGroupId: " .. guideGroupId .. " has been triggered")
        return
    end

    GameInstance.player.globalVar:SetClientVar("racing_dungeon_" .. guideGroupId, 1)

    logger.info("[CoinActivity][RacingDungeon] OnBattleRoomEnterIntro. guideGroupId: " .. guideGroupId)
    GameAction.ManuallyStartGuideGroup(guideGroupId)
end

CoinActivityHudCtrl.OnSubGameStageChange = HL.Method() << function(self)

end

CoinActivityHudCtrl.OnEndToastStart = HL.Method(HL.String) << function(self, type)

end





CoinActivityHudCtrl._TryCreateHud = HL.StaticMethod() << function()
    local dungeonId = GameInstance.dungeonManager.curDungeonId
    if not DungeonUtils.isDungeonRace(dungeonId) then
        return
    end


    local ctrl = CoinActivityHudCtrl.AutoOpen(PANEL_ID)
end

CoinActivityHudCtrl._ToggleTopMainHud = HL.Method(HL.Boolean) << function(self, isOn)










end

CoinActivityHudCtrl._OnInteractiveGoldCoinGet = HL.Method() << function(self)



    self:_RefreshGoldNum()
    if GameInstance.player.racingDungeonSystem.IsInBattleRoom then
        return
    end
    self:_TryReportGoldStatus()
end

CoinActivityHudCtrl._OnInteractiveGoldCoinMagnent = HL.Method() << function(self)
    logger.info("[CoinActivity][RacingDungeon] _OnInteractiveGoldCoinMagnent")
    self:_GetBuff(BUFF_TYPE.MAGNET)
    local title = Language.LUA_DUNGEON_RACING_BUFF_TOAST_MAGNET_TITLE
    local content = string.format(Language.LUA_DUNGEON_RACING_BUFF_TOAST_MAGNET_CONTENT, self:_GetCurrBuffMagnetValue())
    Notify(MessageConst.SHOW_DUNGEON_RACING_BUFF_TOAST, { BUFF_TYPE.MAGNET, title, content })
end

CoinActivityHudCtrl._OnInteractiveGoldCoinIncrease = HL.Method() << function(self)
    logger.info("[CoinActivity][RacingDungeon] _OnInteractiveGoldCoinIncrease")
    self:_GetBuff(BUFF_TYPE.INCREASE)
    local title = Language.LUA_DUNGEON_RACING_BUFF_TOAST_INCREASE_TITLE
    local content = string.format(Language.LUA_DUNGEON_RACING_BUFF_TOAST_INCREASE_CONTENT, self:_GetCurrBuffIncreaseValue())
    Notify(MessageConst.SHOW_DUNGEON_RACING_BUFF_TOAST, { BUFF_TYPE.INCREASE, title, content })
end

CoinActivityHudCtrl._OnInteractiveGoldCoinBuffRefresh = HL.Method() << function(self)
    logger.info("[CoinActivity][RacingDungeon] _OnInteractiveGoldCoinBuffRefresh")
end

CoinActivityHudCtrl._GetCurrGold = HL.Method().Return(HL.Number) << function(self)
    local val = GameInstance.player.racingDungeonSystem:GetCalibratedCoin()
    return val
end

CoinActivityHudCtrl._GetCurrBuffMagnetValue = HL.Method().Return(HL.Number) << function(self)
    local coinBrain = GameWorld.gameMechManager.interactiveGoldCoinBrain
    if coinBrain then
        local num = coinBrain:GetCurDistRate()
        return num
    end
    return 1.5
end

CoinActivityHudCtrl._GetCurrBuffIncreaseValue = HL.Method().Return(HL.Number) << function(self)
    local coinBrain = GameWorld.gameMechManager.interactiveGoldCoinBrain
    if coinBrain then
        local num = coinBrain:GetCurCoinRate()
        return num
    end
    return 2
end

CoinActivityHudCtrl._RefreshUI = HL.Method() << function(self)
    self:_RefreshGoldNum()
end

CoinActivityHudCtrl._InitFinishedTaskIds = HL.Method() << function(self)
    for activityId, _ in pairs(Tables.activityRacingDungeonTable) do
        local taskInfos = ActivityUtils.GetTaskInfos(activityId)
        for _, taskInfo in ipairs(taskInfos) do
            if taskInfo.status == GEnums.ActivityConditionalTaskState.Completed
                or taskInfo.status == GEnums.ActivityConditionalTaskState.Rewarded then
                self.m_finishedTaskIds[activityId .. "_" .. taskInfo.taskId] = true
            end
        end
    end
end

CoinActivityHudCtrl._OnConditionalMultiStageTaskProgressChange = HL.Method(HL.Any) << function(self, args)
    local activityId = unpack(args)
    local isRacingDungeonActivity, _ = Tables.activityRacingDungeonTable:TryGetValue(activityId)
    if not isRacingDungeonActivity then
        return
    end

    local taskInfos = ActivityUtils.GetTaskInfos(activityId)
    table.sort(taskInfos, Utils.genSortFunction({ "sortId" }, true))
    for _, taskInfo in ipairs(taskInfos) do
        local taskKey = activityId .. "_" .. taskInfo.taskId
        if taskInfo.status == GEnums.ActivityConditionalTaskState.Completed and not self.m_finishedTaskIds[taskKey] then
            self.m_finishedTaskIds[taskKey] = true
            local taskName = taskInfo.desc
            if not taskName or taskName == "" then
                taskName = taskInfo.taskId
            end
            Notify(MessageConst.SHOW_DUNGEON_RACING_TASK_FINISH_TOAST, { taskName = taskName })
        elseif taskInfo.status == GEnums.ActivityConditionalTaskState.Rewarded then
            self.m_finishedTaskIds[taskKey] = true
        end
    end
end

CoinActivityHudCtrl._RefreshGoldNum = HL.Method() << function(self)
    local num = self:_GetCurrGold()
    self.view.integralNumTxt.text = num
end

CoinActivityHudCtrl._GetNowMs = HL.Method().Return(HL.Number) << function(self)
    return math.floor(Time.realtimeSinceStartup * 1000)
end

CoinActivityHudCtrl._ClearPendingReportTimer = HL.Method() << function(self)
    if self.m_pendingReportTimerId ~= -1 then
        self.m_pendingReportTimerId = self:_ClearTimer(self.m_pendingReportTimerId)
    end
end

CoinActivityHudCtrl._TryReportGoldStatus = HL.Method() << function(self)
    local currCoin = self:_GetCurrGold()
    if currCoin <= self.m_lastReportCoinNum then
        return
    end

    local nowMs = self:_GetNowMs()
    local elapsedMs = nowMs - self.m_lastReportTimeMs
    if self.m_lastReportTimeMs <= 0 or elapsedMs >= STATUS_REPORT_INTERVAL_MS then
        self:_ClearPendingReportTimer()
        self:_SendStatusReport(currCoin)
        return
    end

    if self.m_pendingReportTimerId ~= -1 then
        return
    end

    local delaySec = (STATUS_REPORT_INTERVAL_MS - elapsedMs) / 1000
    self.m_pendingReportTimerId = self:_StartTimer(delaySec, function()
        self.m_pendingReportTimerId = -1
        self:_SendPendingStatusReport()
    end, true)
end

CoinActivityHudCtrl._SendPendingStatusReport = HL.Method() << function(self)
    local currCoin = self:_GetCurrGold()
    if currCoin <= self.m_lastReportCoinNum then
        return
    end
    self:_SendStatusReport(currCoin)
end


CoinActivityHudCtrl._SendStatusReport = HL.Method(HL.Number) << function(self, currCoin)
    local diff = currCoin - self.m_lastReportCoinNum
    if diff <= 0 then
        return
    end

    local racingDungeonSystem = GameInstance.player.racingDungeonSystem
    logger.info(string.format("[RacingDungeon] _SendStatusReport: %s, %s, %s; time: %s",
        currCoin, diff, racingDungeonSystem.currRoomId, Time.unscaledTime))
    racingDungeonSystem:SendStatusReport(currCoin, diff, racingDungeonSystem.currRoomId)
    self.m_lastReportCoinNum = currCoin
    self.m_lastReportTimeMs = self:_GetNowMs()
end

CoinActivityHudCtrl._GetBuff = HL.Method(HL.Any) << function(self, buffType)
    local coinBrain = GameWorld.gameMechManager.interactiveGoldCoinBrain
    if not coinBrain then
        return
    end

    local curTime = CS.Beyond.TimeManager.time
    if buffType == BUFF_TYPE.INCREASE then
        self.m_buffIncreaseStartTime = curTime
        local coinRateTime = coinBrain:GetCoinRateTime()
        self.m_buffIncreaseEndTime = coinRateTime
    elseif buffType == BUFF_TYPE.MAGNET then
        self.m_buffMagnetStartTime = curTime
        local distRateTime = coinBrain:GetDistRateTime()
        self.m_buffMagnetEndTime = distRateTime
    end

end





CoinActivityHudCtrl._RefreshBuffEntry = HL.Method(HL.Any, HL.Any, HL.Boolean, HL.Number, HL.Number)
    << function(self, buffType, node, shouldShow, value, normalizedValue)
    local wrapper = node.animationWrapper
    if shouldShow then
        if not self.m_buffShown[buffType] then

            node.gameObject:SetActive(true)
            wrapper:ClearTween(false)
            wrapper:PlayInAnimation()
            self.m_buffShown[buffType] = true
        end
        node.buffTxt2.text = "×" .. value
        node.buffEntryBar.fillAmount = normalizedValue
        node.buffEntryBar.color = normalizedValue > self.view.config.BUFF_RED_VALUE
            and Color.white or Color.red
    else
        if self.m_buffShown[buffType] then

            self.m_buffShown[buffType] = false
            wrapper:ClearTween(false)
            wrapper:PlayOutAnimation(function()
                node.gameObject:SetActive(false)
            end)
        end
    end
end

CoinActivityHudCtrl._OnTick = HL.Method() << function(self)
    local curTime = CS.Beyond.TimeManager.time

    local magnetShow = curTime >= self.m_buffMagnetStartTime and curTime <= self.m_buffMagnetEndTime
    local magnetValue, magnetNormalized = 0, 0
    if magnetShow then
        magnetValue = self:_GetCurrBuffMagnetValue()
        magnetNormalized = (self.m_buffMagnetEndTime - curTime) / (self.m_buffMagnetEndTime - self.m_buffMagnetStartTime)
    end
    self:_RefreshBuffEntry(BUFF_TYPE.MAGNET, self.view.buffEntryMagnet, magnetShow, magnetValue, magnetNormalized)

    local increaseShow = curTime >= self.m_buffIncreaseStartTime and curTime <= self.m_buffIncreaseEndTime
    local increaseValue, increaseNormalized = 0, 0
    if increaseShow then
        increaseValue = self:_GetCurrBuffIncreaseValue()
        increaseNormalized = (self.m_buffIncreaseEndTime - curTime) / (self.m_buffIncreaseEndTime - self.m_buffIncreaseStartTime)
    end
    self:_RefreshBuffEntry(BUFF_TYPE.INCREASE, self.view.buffEntryIncrease, increaseShow, increaseValue, increaseNormalized)
end

CoinActivityHudCtrl._ShowBattleEntryPanel = HL.Method(HL.Any) << function(self, arg)
    local pos, rot, tpId, id = unpack(arg)
    logger.info("[CoinActivity][RacingDungeon] _ShowBattleEntryPanel. id: " .. id)
    local _, tableCfg = Tables.activityRacingDungeonBattleEntryTable:TryGetValue(id)
    if tableCfg then
        local roomId = tableCfg.roomId


        if GameInstance.player.racingDungeonSystem.currRoomId ~= roomId then
            logger.error(string.format("[RacingDungeon] _ShowBattleEntryPanel, %s ~= %s, 请检查配置",
                GameInstance.player.racingDungeonSystem.currRoomId, roomId))
            GameInstance.player.racingDungeonSystem.currRoomId = roomId
        end

        local entryButtonText = Language.LUA_ACTIVITY_RACING_DUNGEON_BATTLE_ENTRY_PANEL_BUTTON_TEXT
        local racingDungeonSystem = GameInstance.player.racingDungeonSystem
        local canJumpRoom = racingDungeonSystem:HasHistoryCompleteRoom(roomId)
        logger.info("[CoinActivity][RacingDungeon] _ShowBattleEntryPanel. roomId: " .. roomId)
        local enterRoom = function(skipRoom)
            logger.info("[CoinActivity][RacingDungeon] onEntryClick")
            racingDungeonSystem.currRoomId = roomId
            logger.info("[CoinActivity][RacingDungeon] currRoomId set: " .. roomId)
            self:_SendModifyGame(roomId, 1, skipRoom == true)
            PhaseManager:PopPhase(PhaseId.DungeonCustomEntry)

        end
        PhaseManager:OpenPhase(PhaseId.DungeonCustomEntry, {
            dungeonCustomInfoArg = {
                title = tableCfg.dungeonName,
                desc = tableCfg.dungeonDesc,
                isUnlock = true,
                featureDesc = tableCfg.featureDesc,
                goalComplete = false,
                isTrain = false,
                showEnemy = true,
                entryButtonText = entryButtonText,
                showEntryButton = false,

                onEntryClick = function()
                    enterRoom(false)
                end,
                onEnemyDetailsClick = function()
                    logger.info("[CoinActivity][RacingDungeon] onEnemyDetailsClick")
                    UIManager:AutoOpen(PanelId.CommonEnemyPopup, { title = tableCfg.enemyInfoTitle,
                                                                   enemyListTitle = Language["ui_dungeon_enemy_popup_info_list"],
                                                                   enemyInfoTitle = Language["ui_dungeon_enemy_popup_info_desc"],
                                                                   enemyIds = tableCfg.enemyIds,
                                                                   enemyLevels = tableCfg.enemyLevels })
                end,
            },
            goButtonText = entryButtonText,
            showJumpButton = canJumpRoom,
            onGoClick = function()
                enterRoom(false)
            end,
            onJumpClick = function()
                enterRoom(true)
            end,
        })
    else
        logger.error("[CoinActivity][RacingDungeon] 缺失表格数据:" .. id)



    end
end

CoinActivityHudCtrl._OnTriggerRacingDungeonBattleEnd = HL.Method(HL.Any) << function(self, arg)
    logger.info("[CoinActivity][RacingDungeon] _OnTriggerRacingDungeonBattleEnd")

    self:_SendModifyGame(GameInstance.player.racingDungeonSystem.currRoomId, 3)
end

CoinActivityHudCtrl._ShowExitPanel = HL.Method() << function(self)
    UIManager:Open(PanelId.CoinActivityPopup, {
        score = self:_GetCurrGold(),
        onConfirm = function()


            self:_SendLeave()
        end,
    })
end

CoinActivityHudCtrl._SendLeave = HL.Method() << function(self)
    self:_ClearPendingReportTimer()

    local currCoin = self:_GetCurrGold()
    local diff = currCoin - self.m_lastReportCoinNum
    if diff < 0 then
        logger.error(string.format("[CoinActivity][RacingDungeon] 状态上报金币变化为 %s -> %s", self.m_lastReportCoinNum, currCoin))
    end

    local racingDungeonSystem = GameInstance.player.racingDungeonSystem
    racingDungeonSystem:SendLeave(currCoin, diff, racingDungeonSystem.currRoomId)
    self.m_lastReportCoinNum = currCoin
    self.m_lastReportTimeMs = self:_GetNowMs()
end


CoinActivityHudCtrl._SendModifyGame = HL.Method(HL.Any, HL.Any, HL.Opt(HL.Boolean)) << function(self, roomId, source, skipRoom)
    logger.info(string.format("[CoinActivity][RacingDungeon] _SendModifyGame. roomId:%s, source:%s, skipRoom:%s", roomId, source, skipRoom == true))
    local currCoin = self:_GetCurrGold()
    local diff = currCoin - self.m_lastSendCoinNum
    if diff < 0 then
        logger.error(string.format("[CoinActivity][RacingDungeon] 金币变化为 %s -> %s", self.m_lastSendCoinNum, currCoin))
    end
    GameInstance.player.racingDungeonSystem:SendModifyGame(currCoin, diff, roomId, source, skipRoom == true)
    self.m_lastSendCoinNum = currCoin
end

CoinActivityHudCtrl._TryCreatePopup = HL.Method() << function(self)
    local activityInfo = GameInstance.player.racingDungeonSystem:GetActivityInfo()
    if not activityInfo then
        return
    end
    local activityId = activityInfo.id
    local havePlayGame = ActivityUtils.RacingDungeonHavePlayGame(activityId)
    local isStageTwoNew = ActivityUtils.RacingDungeonStageTwoIsNew(activityId)
    if havePlayGame and not isStageTwoNew then
        return
    end

    local interrupted = false
    local hasStarted = false
    local finish = function()
        if interrupted then
            return
        end
        Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, COIN_ACTIVITY_INTRO_REQUEST_TYPE)
    end
    local openIntro = function(introId, onCloseCallback)
        if interrupted then
            return
        end
        UIManager:AutoOpen(PanelId.CommonIntro, {
            introId = introId,
            onCloseCallback = function()
                if interrupted then
                    return
                end
                onCloseCallback()
            end,
        })
    end
    local openStageTwoIntro = function()
        openIntro("dungeon_race_stage_two", finish)
    end

    LuaSystemManager.commonTaskTrackSystem:AddRequest(COIN_ACTIVITY_INTRO_REQUEST_TYPE, function()
        hasStarted = true
        if not havePlayGame then
            openIntro("dungeon_race_first", function()
                if isStageTwoNew then
                    openStageTwoIntro()
                else
                    finish()
                end
            end)
        elseif isStageTwoNew then
            openStageTwoIntro()
        end
    end, function()
        interrupted = true
        if hasStarted then
            UIManager:Close(PanelId.CommonIntro)
        end
    end)
end

CoinActivityHudCtrl._OnLevelSelectRoom = HL.Method(HL.Any) << function(self, arg)
    local roomId = unpack(arg)
    logger.info("[RacingDungeon] _OnLevelSelectRoom: " .. roomId)
    GameInstance.player.racingDungeonSystem.currRoomId = roomId


    GameInstance.player.racingDungeonSystem.IsInBattleRoom = false
end





CoinActivityHudCtrl.TestAddBuffIncrease = HL.Method() << function(self)
    local coinBrain = GameWorld.gameMechManager.interactiveGoldCoinBrain
    if coinBrain then
        coinBrain:SetCoinRate(2, 5.0)
    end
end

CoinActivityHudCtrl.TestAddBuffMagnet = HL.Method() << function(self)
    local coinBrain = GameWorld.gameMechManager.interactiveGoldCoinBrain
    if coinBrain then
        coinBrain:SetDistanceRate(3, 6.0)
    end
end

CoinActivityHudCtrl.TestBattleEntryPanel = HL.Method() << function(self)
    PhaseManager:OpenPhase(PhaseId.DungeonCustomEntry, {
        dungeonCustomInfoArg = {
            title = "深渊挑战",
            desc = "挑战最强的敌人...",


            isUnlock = true,


            featureDesc = "敌人攻击力提升50%",

            goalComplete = false,
            isTrain = false,
            showEnemy = true,
            entryButtonText = "开始挑战",

            onEntryClick = function()
                logger.info("[CoinActivity][RacingDungeon] onEntryClick")

            end,
            onEnemyDetailsClick = function()
                logger.info("[CoinActivity][RacingDungeon] onEnemyDetailsClick")

            end,
        },
        hideBg = true,
        title = nil,
    })
end





HL.Commit(CoinActivityHudCtrl)
