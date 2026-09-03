
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SimulationTrainingTrackHud

local Phase = {
    Normal = 1,
    Fail = 2,
    CompleteMainGoal = 3,
    CompleteAllGoal = 4,
}

local SIMULATION_TRAINING_SUB_GAME_ID = "world_poi_simulation_training_01"

local Category2CommonTaskQueueKey = {
    ["world_energy_point_small"] = "ForceClearTrackHud",
    ["world_energy_point"] = "ForceClearTrackHud",
}

local TrackingHudState = {
    None = 0,
    Custom = 1,
    SubGame = 2,
}

local PANEL_OUT_ANIM = "commontasktrackhud_out"
local CONTENT_SCROLL_FADE_ANIM = "commontasktrackhud_contentscrollfade"
local CONTENT_REFRESH_ANIM = "commontasktrackhud_contentrefresh"
local TITLE_SCROLL_FADE_ANIM = "commontasktrackhud_titlescrollfade"
local TITLE_FINISH_ANIM = "titlefinish_in"
local TITLE_FAIL_ANIM = "titlefail_in"

local STAGE_TEXT_FORMAT = "%d/%d"

SimulationTrainingTrackHudCtrl = HL.Class('SimulationTrainingTrackHudCtrl', uiCtrl.UICtrl)


SimulationTrainingTrackHudCtrl.m_curPhase = HL.Field(HL.Number) << Phase.Normal

SimulationTrainingTrackHudCtrl.m_subGameId = HL.Field(HL.String) << ""

SimulationTrainingTrackHudCtrl.m_isShowCustomTask = HL.Field(HL.Boolean) << false

SimulationTrainingTrackHudCtrl.m_resetBtnVisible = HL.Field(HL.Boolean) << false



SimulationTrainingTrackHudCtrl.m_contentShowingCor = HL.Field(HL.Thread)

SimulationTrainingTrackHudCtrl.taskGoalShowing = HL.Field(HL.Boolean) << false





SimulationTrainingTrackHudCtrl.m_canScrollContent = HL.Field(HL.Boolean) << false

SimulationTrainingTrackHudCtrl.m_showArrow = HL.Field(HL.Boolean) << true

SimulationTrainingTrackHudCtrl.m_canFold = HL.Field(HL.Boolean) << false

SimulationTrainingTrackHudCtrl.m_isFold = HL.Field(HL.Boolean) << true

SimulationTrainingTrackHudCtrl.m_scrollState = HL.Field(HL.Number) << -1

SimulationTrainingTrackHudCtrl.m_rectFoldHeight = HL.Field(HL.Number) << 0

SimulationTrainingTrackHudCtrl.m_rectUnfoldHeight = HL.Field(HL.Number) << 0

SimulationTrainingTrackHudCtrl.m_tween = HL.Field(HL.Any)



SimulationTrainingTrackHudCtrl.m_canFoldThresholdOffsetY = HL.Field(HL.Number) << -1







SimulationTrainingTrackHudCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_SUB_GAME_FINISH_STATE_CHANGE] = "OnSubGameFinishStateChange",
    [MessageConst.ON_SUB_GAME_STAGE_FINISH] = "OnSubGameStageFinish",
    [MessageConst.ON_SUB_GAME_STAGE_CHANGE] = "OnSubGameStageChange",
    [MessageConst.ON_GAME_MECHANICS_SYNC_CUSTOM_TARGET] = "OnGameMechanicsSyncCustomTarget",
}




SimulationTrainingTrackHudCtrl.s_trackHudFinishState = HL.StaticField(HL.Number) << Phase.Normal

SimulationTrainingTrackHudCtrl.OnSwitchLanguage = HL.StaticMethod() << function()
    if GameInstance.dungeonManager.curDungeonLikeSubGame == nil and GameWorld.worldInfo.subGame == nil then
        return
    end

    if SimulationTrainingTrackHudCtrl.s_trackHudFinishState ~= Phase.Normal then
        return
    end

    local trackOpened, trackHudCtrl = UIManager:IsOpen(PANEL_ID)
    if trackOpened then
        local subGameId = SIMULATION_TRAINING_SUB_GAME_ID
        SimulationTrainingTrackHudCtrl.OnOpenSubGameTrackings({ subGameId, Phase.Normal })
        return
    end
end






SimulationTrainingTrackHudCtrl.OnOpenSubGameTrackings = HL.StaticMethod(HL.Any) << function(args)
    local doAction = function()
        local trackHudCtrl = UIManager:AutoOpen(PANEL_ID)
        trackHudCtrl:InitSubGameTrack(args)
        trackHudCtrl:OnSubGameFinishStateChange(args)

        local opened, missionHudCtrl = UIManager:IsOpen(PanelId.MissionHud)
        if not opened then
            return
        end

        if UIManager:IsShow(PanelId.MissionHud) and not missionHudCtrl:IsPlayingAnimationOut() then
            missionHudCtrl:PlayAnimationOutAndClose()
        else
            missionHudCtrl:Close()
        end
    end

    doAction()


end

SimulationTrainingTrackHudCtrl.OnCloseSubGameTrack = HL.StaticMethod(HL.Table) << function(args)
    local subGameId, isReset = unpack(args)
    local action = function()

        local trackOpened, trackHudCtrl = UIManager:IsOpen(PANEL_ID)
        if trackOpened then
            trackHudCtrl:StopSubGameTrack(isReset)
        else

            SimulationTrainingTrackHudCtrl.OnDeactivateCommonTaskTrackHud({ isReset })
        end
    end

    if isReset then
        action()
    else
        action()




    end
end



SimulationTrainingTrackHudCtrl.OnDeactivateCommonTaskTrackHud = HL.StaticMethod(HL.Table) << function(args)
    local ignoreCloseAnim = unpack(args)
    local opened, commonTaskTrackCtrl = UIManager:IsOpen(PANEL_ID)
    if opened then
        if UIManager:IsShow(PANEL_ID) and not ignoreCloseAnim then
            commonTaskTrackCtrl:PlayAnimationOutAndClose()
        else
            commonTaskTrackCtrl:Close()
        end
    end

    if Utils.needMissionHud() then
        local ctrl = UIManager:AutoOpen(PanelId.MissionHud)
        ctrl:PlayAnimationIn()
    end
end




SimulationTrainingTrackHudCtrl.InitSubGameTrack = HL.Method(HL.Any) << function(self, args)
    self.m_subGameId = SIMULATION_TRAINING_SUB_GAME_ID
    self.m_isShowCustomTask = false


    self:RefreshAll()
end

SimulationTrainingTrackHudCtrl.StopSubGameTrack = HL.Method(HL.Boolean) << function(self, isReset)
    self.m_subGameId = ""
    if self.m_isShowCustomTask then
        self:RefreshAll()
    else
        SimulationTrainingTrackHudCtrl.OnDeactivateCommonTaskTrackHud({ isReset })
    end
end



SimulationTrainingTrackHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnStop.onClick:AddListener(function()
        self:_OnBtnStopClick()
    end)
end

SimulationTrainingTrackHudCtrl.OnClose = HL.Override() << function(self)
    if self.m_contentShowingCor then
        self.m_contentShowingCor = self:_ClearCoroutine(self.m_contentShowingCor)
    end
end

SimulationTrainingTrackHudCtrl.OnShow = HL.Override() << function(self)


end

SimulationTrainingTrackHudCtrl.RefreshAll = HL.Method() << function(self)
    self:_RefreshSubGameTrack()


    if self.m_contentShowingCor then
        self.m_contentShowingCor = self:_ClearCoroutine(self.m_contentShowingCor)
    end

    local wrapper = self.animationWrapper
    wrapper:SampleClip(CONTENT_REFRESH_ANIM, 1)
    wrapper:PlayInAnimation()
end


SimulationTrainingTrackHudCtrl._RefreshSubGameTrack = HL.Method() << function(self)
    local trackingMgr = GameWorld.levelScriptTaskTrackingManager
    local mainTask = trackingMgr.mainTask
    self.view.goalRoot.gameObject:SetActive(true)

    self:_RefreshMainTask()

    local success, subGameData = DataManager.subGameInstDataTable:TryGetValue(self.m_subGameId)
    self.view.btnStop.gameObject:SetActiveIfNecessary(true)
    self.view.btnStopTxt:SetAndResolveTextStyle(success and subGameData.resetBtnName:GetText() or self.m_subGameId)

    local gameMechanicData = Tables.gameMechanicTable[self.m_subGameId]
    local gameMechanicCategoryData = Tables.gameMechanicCategoryTable[gameMechanicData.gameCategory]
    self.m_resetBtnVisible = gameMechanicCategoryData.canReChallenge

    self.view.btnNode.gameObject:SetActiveIfNecessary(true)

    local title


    if string.isEmpty(title) then
        title = success and gameMechanicData.gameName
    end
    self:_ProcessTitleText(title)
    self:_ProcessTitleIcon()
end


SimulationTrainingTrackHudCtrl.OnGameMechanicsSyncCustomTarget = HL.Method(HL.Any) << function(self, args)
    local gameId, targetValue, currentValue = unpack(args)
    self.view.mainGoalCell.progressTxt.text = string.format(Language.LUA_SIMULATION_TRAINING_HUD_GOAL_NUMBER, currentValue,targetValue)
end

SimulationTrainingTrackHudCtrl._RefreshMainTask = HL.Method() << function(self)
    self.view.mainGoalCell.goalTxt.text = Language.LUA_SIMULATION_TRAINING_HUD_GOAL_TEXT
    local m_system = GameInstance.player.simulationTrainingSystem
    self.view.mainGoalCell.progressTxt.text = string.format(Language.LUA_SIMULATION_TRAINING_HUD_GOAL_NUMBER, 0, m_system.enemyAllCount)
    local rewardNum = m_system.rewardScoreNumber
    if rewardNum == 0 and not m_system.unlimitedMode and m_system.curHandCards.Count > 0 then
        local pointNumber = 0
        for i = 1, m_system.curHandCards.Count do
            local cardName = m_system.curHandCards[i - 1]
            local hasCard, cardData = Tables.SimulationTrainingCardTable:TryGetValue(cardName)
            if hasCard then
                pointNumber = pointNumber + cardData.cardPoint
            end
        end
        local hasCfg, curLevelData = Tables.simulationTrainingLevelTable:TryGetValue(m_system.curLevel)
        if hasCfg then
            rewardNum = curLevelData.pointAward[pointNumber % 11]
            if m_system.doublePrize then
                rewardNum = rewardNum + rewardNum
            end
        end
        m_system.rewardScoreNumber = rewardNum
    end
    self.view.mainGoalRewardCell.goalTxt.text = string.format(Language.LUA_SIMULATION_TRAINING_HUD_REWARD_NUMBER, rewardNum)
    self.view.mainGoalRewardCell.progressTxt.text = ""
end


SimulationTrainingTrackHudCtrl.OnSubGameStageFinish = HL.Method() << function(self)
    if not self:_ShowEndEffect() then
        return
    end

    self:_ToggleBtnVisible(false)
    local action = function()
        self.m_contentShowingCor = self:_StartCoroutine(function()
            while true do

                if not self.taskGoalShowing then
                    break
                end
                coroutine.step()
            end
            self:_SafelyPlayAnimAsync(self.animationWrapper, CONTENT_SCROLL_FADE_ANIM)
            self:_SafelyPlayAnimAsync(self.animationWrapper, PANEL_OUT_ANIM)
            self:_TrackFinish()
            Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, "TrackStateFinish")
        end)
    end
    action()



















end

SimulationTrainingTrackHudCtrl.OnSubGameStageChange = HL.Method() << function(self)
    local action = function()
        local subGameId = SIMULATION_TRAINING_SUB_GAME_ID
        local args = { subGameId, Phase.Normal}
        local trackHudCtrl = UIManager:AutoOpen(PANEL_ID)
        trackHudCtrl:InitSubGameTrack(args)
        trackHudCtrl:OnSubGameFinishStateChange(args)
        Notify(MessageConst.ON_SUB_GAME_STAGE_CHANGE_FINISH)
    end
    action()()








end

SimulationTrainingTrackHudCtrl.OnSubGameFinishStateChange = HL.Method(HL.Any) << function(self, args)
    local subGameId, phase = unpack(args)

    if phase == nil then
        phase = Phase.Normal
    end

    if phase == Phase.Normal then
        self:_ToggleTitleState(phase)
    elseif phase == Phase.Fail then
        if self:_ShowEndEffect() then
            self:_DoFailContentShowing(phase)





        else
            self:_RefreshFailInfo()
            self:_ManuSetFailState()
            self:_ToggleTitleState(phase)
            self.view.titleFail:Play(TITLE_FAIL_ANIM)
        end
    else
        if self.m_curPhase ~= Phase.CompleteMainGoal and self.m_curPhase ~= Phase.CompleteAllGoal then
            if self:_ShowEndEffect() then
                self:_DoSuccContentShowing(phase)






            else
                self:_ToggleTitleState(phase)
                self.view.titleFinish:Play(TITLE_FINISH_ANIM)
            end
        end

    end

    SimulationTrainingTrackHudCtrl.s_trackHudFinishState = phase
    self.m_curPhase = phase
end

SimulationTrainingTrackHudCtrl._ShowEndEffect = HL.Method().Return(HL.Boolean) << function(self)
    return false
end

SimulationTrainingTrackHudCtrl._DoFailContentShowing = HL.Method(HL.Number) << function(self, phase)
    self:_ToggleBtnVisible(false)
    self:_ManuSetFailState()
    AudioAdapter.PostEvent("Au_UI_Mission_Step_Fail")
    self.m_contentShowingCor = self:_StartCoroutine(function()
        while true do

            if not self.taskGoalShowing then
                break
            end
            coroutine.step()
        end
        self:_SafelyPlayAnimAsync(self.animationWrapper, CONTENT_SCROLL_FADE_ANIM)

        Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, "TrackHudShowEndEffect")
        AudioAdapter.PostEvent("Au_UI_Mission_Fail")
        self:_RefreshFailInfo()
        self:_ToggleTitleState(phase)
        self:_SafelyPlayAnimAsync(self.view.titleFail, TITLE_FAIL_ANIM)
        self:_SafelyPlayAnimAsync(self.animationWrapper, TITLE_SCROLL_FADE_ANIM)
        self:_TrackFinish()
    end)
end

SimulationTrainingTrackHudCtrl._DoSuccContentShowing = HL.Method(HL.Number) << function(self, phase)
    self:_ToggleBtnVisible(false)
    self.m_contentShowingCor = self:_StartCoroutine( function()
        while true do

            if not self.taskGoalShowing then
                break
            end
            coroutine.step()
        end

        self:_SafelyPlayAnimAsync(self.animationWrapper, CONTENT_SCROLL_FADE_ANIM)

        Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, "TrackHudShowEndEffect")
        AudioAdapter.PostEvent("Au_UI_Mission_Complete")
        self:_ToggleTitleState(phase)

        self:_SafelyPlayAnimAsync(self.view.titleFinish, TITLE_FINISH_ANIM)
        self:_SafelyPlayAnimAsync(self.animationWrapper, TITLE_SCROLL_FADE_ANIM)
        self:_TrackFinish()
    end)
end

SimulationTrainingTrackHudCtrl._SafelyPlayAnimAsync = HL.Method(HL.Any, HL.String) << function(self, animWrapper, animName)
    if self.m_isClosed then
        return
    end

    local stateTime = animWrapper:GetClipLength(animName)
    animWrapper:Play(animName)
    coroutine.wait(stateTime)
end

SimulationTrainingTrackHudCtrl._TrackFinish = HL.Method() << function(self)
    self:Hide()
end

SimulationTrainingTrackHudCtrl._ManuSetFailState = HL.Method() << function(self)

end

SimulationTrainingTrackHudCtrl._ToggleTitleState = HL.Method(HL.Number) << function(self, phase)

    if self.m_isClosed then
        return
    end
    self.view.titleDefault.gameObject:SetActive(phase == Phase.Normal)
    self.view.titleFail.gameObject:SetActive(phase == Phase.Fail)
    self.view.titleFinish.gameObject:SetActive(phase == Phase.CompleteMainGoal or phase == Phase.CompleteAllGoal)
end


SimulationTrainingTrackHudCtrl._RefreshFailInfo = HL.Method() << function(self)
    local success, subGameData = DataManager.subGameInstDataTable:TryGetValue(self.m_subGameId)
    local failInfo = success and subGameData.failInfo:GetText() or self.m_subGameId
    self.view.goalRoot.gameObject:SetActive(false)
end

SimulationTrainingTrackHudCtrl._ProcessTitleText = HL.Method(HL.String) << function(self, title)
    self.view.titleDefaultTxt:SetAndResolveTextStyle(title)
    self.view.titleFailTxt:SetAndResolveTextStyle(title)
    self.view.titleFinishTxt:SetAndResolveTextStyle(title)
end

SimulationTrainingTrackHudCtrl._ProcessTitleIcon = HL.Method() << function(self)
    local success, gameTblData = Tables.gameMechanicTable:TryGetValue(self.m_subGameId)
    local gameTypeData = success and Tables.gameMechanicCategoryTable[gameTblData.gameCategory] or {}

    local iconName = "simulation_training_icon"
    local iconBgName = gameTypeData.iconBg

    if not string.isEmpty(iconName) then
        self.view.defaultIcon:LoadSprite(UIConst.UI_SPRITE_COMMON_TASK_TRACK, iconName)
        self.view.finishIcon:LoadSprite(UIConst.UI_SPRITE_COMMON_TASK_TRACK, iconName)
        self.view.failIcon:LoadSprite(UIConst.UI_SPRITE_COMMON_TASK_TRACK, iconName)
    end

    self.view.defaultIconBg.gameObject:SetActiveIfNecessary(true)
    self.view.finishIconBg.gameObject:SetActiveIfNecessary(true)
    self.view.failIconBg.gameObject:SetActiveIfNecessary(true)
    if hasIconBgName then
        self.view.defaultIconBg:LoadSprite(UIConst.UI_SPRITE_COMMON_TASK_TRACK, iconBgName)
        self.view.finishIconBg:LoadSprite(UIConst.UI_SPRITE_COMMON_TASK_TRACK, iconBgName)
        self.view.failIconBg:LoadSprite(UIConst.UI_SPRITE_COMMON_TASK_TRACK, iconBgName)
    end
end


SimulationTrainingTrackHudCtrl._OnBtnStopClick = HL.Method() << function(self)
    self:_ShowConfirmPopup(Language.LUA_SIMULATION_TRAINING_HUD_EXIT_POPUP_TEXT, function()
        GameWorld.worldInfo.subGame:SendQuit()
    end)
end


SimulationTrainingTrackHudCtrl._ShowConfirmPopup = HL.Method(HL.String, HL.Function) << function(self, content, confirmFunc)
    if not string.isEmpty(GameInstance.player.systemActionConflictManager.curProcessingSystemAction) then
        logger.warn("SimulationTrainingTrackHudCtrl._ShowConfirmPopup systemConflict:", GameInstance.player.systemActionConflictManager:GetCurProcessingSystemActionInfo(), "SubGameId:", self.m_subGameId)
        return
    end

    self:Notify(MessageConst.SHOW_POP_UP, {
        content = content,
        onConfirm = function()
            confirmFunc()
        end,
        freezeWorld = true,
        pauseGame = true,
        interrupt = {
            interruptMessage = { MessageConst.ON_SUB_GAME_QUIT, MessageConst.SHOW_DEATH_INFO },
        }
    })
end


SimulationTrainingTrackHudCtrl._ToggleBtnVisible = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.btnStop.gameObject:SetActiveIfNecessary(isOn)
end




HL.Commit(SimulationTrainingTrackHudCtrl)
