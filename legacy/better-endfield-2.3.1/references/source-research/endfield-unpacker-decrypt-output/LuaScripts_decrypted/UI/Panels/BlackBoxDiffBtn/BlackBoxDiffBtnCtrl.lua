
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BlackBoxDiffBtn
local BlackboxCondition = CS.Beyond.Gameplay.RemoteFactory.BlackboxCondition
local GetEnumNameFunc = CS.System.Enum.GetName
local SystemActionConflictId = "BlackboxObtainReward"

local Phase = {
    Normal = 1,
    Fail = 2,
    CompleteMainGoal = 3,
    CompleteAllGoal = 4,
}

BlackBoxDiffBtnCtrl = HL.Class('BlackBoxDiffBtnCtrl', uiCtrl.UICtrl)







BlackBoxDiffBtnCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_SUB_GAME_FINISH_STATE_CHANGE] = "OnSubGameFinishStateChange",
    [MessageConst.ON_DUNGEON_WARNING_STATE_CHANGE] = "OnDungeonWarningStateChange",

    [MessageConst.ON_PHASE_LEVEL_NOT_ON_TOP] = "OnLeaveMainHud",

    [MessageConst.ON_HUD_BTN_VISIBLE_CHANGE] = "OnHudBtnVisibleChange",
    [MessageConst.ON_SHOW_BLACKBOX_RESULT] = "OnShowBlackboxResult",
    [MessageConst.ON_TOGGLE_FAC_TOP_VIEW] = "OnToggleFacTopView",
}

BlackBoxDiffBtnCtrl.OnOpenSubGameTrackings = HL.StaticMethod(HL.Any) << function(args)
    if not Utils.isInDungeonFactory() then
        return
    end

    LuaSystemManager.commonTaskTrackSystem:AddRequest("BlackboxDiff", function()

        local ctrl = BlackBoxDiffBtnCtrl.AutoOpen(PANEL_ID)
        ctrl:OnSubGameFinishStateChange(args)
        ctrl:OnDungeonWarningStateChange({})
        ctrl:_ProcessPosition()
        ctrl.m_hasObtainReward = false
    end)
end

BlackBoxDiffBtnCtrl.OnCloseSubGameTrackings = HL.StaticMethod() << function()
    local succ, ctrl = UIManager:IsOpen(PANEL_ID)
    if succ then
        ctrl:PlayAnimationOutAndClose()
    end
end

BlackBoxDiffBtnCtrl.m_curDungeonId = HL.Field(HL.String) << ""

BlackBoxDiffBtnCtrl.m_curDungeonInfo = HL.Field(HL.Any)

BlackBoxDiffBtnCtrl.m_curPhase = HL.Field(HL.Number) << Phase.Normal

BlackBoxDiffBtnCtrl.m_isWarning = HL.Field(HL.Boolean) << false

BlackBoxDiffBtnCtrl.m_warningInfo = HL.Field(HL.Table)

BlackBoxDiffBtnCtrl.m_cacheWarningState = HL.Field(HL.Table)

BlackBoxDiffBtnCtrl.m_failReason = HL.Field(HL.String) << ""

BlackBoxDiffBtnCtrl.m_updateTick = HL.Field(HL.Number) << -1

BlackBoxDiffBtnCtrl.m_taskTrackCtrl = HL.Field(HL.Forward("CommonTaskTrackHudCtrl"))

BlackBoxDiffBtnCtrl.m_hadAddPosition = HL.Field(HL.Boolean) << false

BlackBoxDiffBtnCtrl.m_hasObtainReward = HL.Field(HL.Boolean) << false


BlackBoxDiffBtnCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_warningInfo = {}
    self.m_cacheWarningState = {}

    self.view.blackboxDiffStateBtn.onClick:AddListener(function()
        self:_OnBtnDiffStateClick()
    end)

    self.view.btnReset.onClick:AddListener(function()
        self:_OnBtnResetClick()
    end)

    self.view.hintTxt:SetAndResolveTextStyle(self:_ProcessPlatformDiffText())

    local dungeonId = GameInstance.dungeonManager.curDungeonId
    local success, dungeonInfo = Tables.gameMechanicTable:TryGetValue(dungeonId or "")
    if success then
        self.m_curDungeonId = dungeonId
        self.m_curDungeonInfo = dungeonInfo
    end

    self.m_updateTick = LuaUpdate:Add("LateTick", function(deltaTime)
        self:_RefreshBtnPanelPosition()
    end)
end

BlackBoxDiffBtnCtrl.OnClose = HL.Override() << function(self)
    self.m_updateTick = LuaUpdate:Remove(self.m_updateTick)
end

BlackBoxDiffBtnCtrl.ProcessIfIsInTopView = HL.Method() << function(self)
    if FactoryUtils.isInTopView() then
        LuaSystemManager.factory:ToggleTopView(false, true)
    end
end

BlackBoxDiffBtnCtrl._ShowGoalDetailsPanel = HL.Method() << function(self)
    if string.isEmpty(self.m_curDungeonId) then
        return
    end

    UIManager:AutoOpen(PanelId.BlackBoxTargetAndReward,
                       { dungeonId = self.m_curDungeonId, warningInfo = self.m_warningInfo })
end

BlackBoxDiffBtnCtrl._CompleteWithoutExtraGoal = HL.Method() << function(self)
    local extraRewardData = Tables.rewardTable[self.m_curDungeonInfo.extraRewardId]
    local extraRewardItemBundles = {}
    for _, itemBundle in pairs(extraRewardData.itemBundles) do
        local _, itemData = Tables.itemTable:TryGetValue(itemBundle.id)
        if itemData then
            table.insert(extraRewardItemBundles,
                         { id = itemBundle.id, count = itemBundle.count, rarity = itemData.rarity, type = itemData.type:ToInt() })
        end
    end
    table.sort(extraRewardItemBundles, Utils.genSortFunction({ "rarity", "type", "id" }, false))

    local extraRewardGained = GameInstance.dungeonManager:IsDungeonExtraRewardGained(self.m_curDungeonInfo.gameMechanicsId)
    Notify(MessageConst.SHOW_POP_UP, {
        content = extraRewardGained and Language.LUA_DUNGEON_COMPLETE_WITHOUT_EXTRA_REWARDS_GAINED_CONFIRM or
                Language.LUA_DUNGEON_COMPLETE_WITHOUT_EXTRA_REWARDS_CONFIRM,
        items = extraRewardItemBundles,
        got = extraRewardGained,
        onConfirm = function()
            self:_CompleteGoal()
        end,
        onCancel = function()
        end
    })
end

BlackBoxDiffBtnCtrl._CompleteGoal = HL.Method() << function(self)
    self:ProcessIfIsInTopView()

    if self.m_hasObtainReward then
        return
    end

    GameInstance.player.subGameSys:TryObtainReward(self.m_curDungeonId)

    GameInstance.player.systemActionConflictManager:TryStartSystemAction(SystemActionConflictId)
end

BlackBoxDiffBtnCtrl._OnBtnDiffStateClick = HL.Method() << function(self)
    if self.m_isWarning and self.m_curPhase ~= Phase.Fail or self.m_curPhase == Phase.Normal then
        self:_ShowGoalDetailsPanel()
    elseif self.m_curPhase == Phase.CompleteMainGoal and
            not string.isEmpty(self.m_curDungeonInfo.extraRewardId) then

        self:_CompleteWithoutExtraGoal()
    elseif self.m_curPhase == Phase.CompleteAllGoal or
            self.m_curPhase == Phase.CompleteMainGoal and string.isEmpty(self.m_curDungeonInfo.extraRewardId) then

        self:_CompleteGoal()
    elseif self.m_curPhase == Phase.Fail then
        self:ProcessIfIsInTopView()
        local currentTimestampBySeconds = CS.Beyond.DateTimeUtils.GetCurrentTimestampBySeconds()
        Notify(MessageConst.ON_SHOW_BLACKBOX_RESULT, { self.m_curDungeonId, currentTimestampBySeconds + self.view.config.COUNTDOWN_DURATION, true, self.m_failReason })
    end
end

BlackBoxDiffBtnCtrl.OnSubGameFinishStateChange = HL.Method(HL.Any) << function(self, args)
    local subGameId, phase = unpack(args)
    if self.m_isWarning and phase ~= Phase.Fail then
        self.view.animation:SetState("Warning")
    elseif phase == Phase.Normal or phase == Phase.Fail then
        self.view.animation:SetState("Normal")
    elseif phase == Phase.CompleteMainGoal or phase == Phase.CompleteAllGoal then
        self.view.animation:SetState("Complete")
    end

    if self.m_curPhase ~= Phase.CompleteMainGoal and self.m_curPhase ~= Phase.CompleteAllGoal
        and (phase == Phase.CompleteMainGoal or phase == Phase.CompleteAllGoal) then

        AudioAdapter.PostEvent("Au_UI_Event_SimulateComplete")
    end

    local completelyComplete = not self.m_isWarning and (phase == Phase.CompleteAllGoal or phase == Phase.CompleteMainGoal
            and string.isEmpty(self.m_curDungeonInfo.extraRewardId))
    self.view.tips.gameObject:SetActive(completelyComplete)
    self.view.btnReset.gameObject:SetActive(not completelyComplete)

    if phase == Phase.Fail then
        local success, subGameData = DataManager.subGameInstDataTable:TryGetValue(subGameId)
        if success then

            self.m_failReason = subGameData.failInfo:GetText()
        end
    end

    self.m_curPhase = phase
end

BlackBoxDiffBtnCtrl.OnDungeonWarningStateChange = HL.Method(HL.Any) << function(self, args)
    if self.m_curPhase == Phase.Fail then
        return
    end

    local chapterId, conditions = unpack(args)
    if string.isEmpty(chapterId) then
        chapterId = Utils.getCurrentChapterId()
        conditions = GameInstance.dungeonManager.curDungeonLikeSubGame.needCheckConditions
    end
    local curWarningState = false
    self.m_warningInfo = {}
    for _, condition in pairs(conditions) do
        local checkResult = GameInstance.remoteFactoryManager.blackboxConditionQuery:CheckCondition(chapterId,
                                                                                                    condition)
        self.m_cacheWarningState[condition] = checkResult
    end

    for condition, result in pairs(self.m_cacheWarningState) do
        if not result then
            curWarningState = true
            local text = self:_GetWarningTextByConditionType(condition)
            if not string.isEmpty(text) then
                table.insert(self.m_warningInfo, text)
            end
        end
    end

    if self.m_isWarning ~= curWarningState then
        self.m_isWarning = curWarningState
        self:OnSubGameFinishStateChange({ self.m_curDungeonId, self.m_curPhase})
    end
end

BlackBoxDiffBtnCtrl._GetWarningTextByConditionType = HL.Method(HL.Userdata).Return(HL.String) << function(self,
                                                                                                          condition)
    local str = GetEnumNameFunc(typeof(BlackboxCondition), condition)
    return Language["LUA_BLACKBOX_WARNING_" .. str]
end

BlackBoxDiffBtnCtrl._ProcessPlatformDiffText = HL.Method().Return(HL.String) << function(self)
    local text
    if DeviceInfo.usingTouch then
        text = Language["ui_fac_blackboxdiffbtn_tips_mb"]
    else
        text = InputManager.ParseTextActionId(Language["ui_fac_blackboxdiffbtn_tips"])
    end
    return text
end

BlackBoxDiffBtnCtrl._ProcessPosition = HL.Method() << function(self)
    local height = 156

    local succ, minimapCtrl = UIManager:IsOpen(PanelId.MiniMap)
    if succ then



        UIManager:Close(PanelId.MiniMap)
    end


    if DeviceInfo.usingTouch then
        height = math.max(height - self.view.blackboxDiffStateBtn:GetComponent("RectTransform").rect.height, 0)
    end


    local taskHudSucc, commonTaskCtrl = UIManager:IsOpen(PanelId.CommonTaskTrackHud)
    if taskHudSucc then
        if not self.m_hadAddPosition then
            self.m_hadAddPosition = true

            commonTaskCtrl:AddPositionOffset(Vector2(0, height), true)
        end
        commonTaskCtrl:HideBottomNode()
    end
end

BlackBoxDiffBtnCtrl._RefreshBtnPanelPosition = HL.Method() << function(self)
    if self.m_taskTrackCtrl == nil then
        local success, taskTrackCtrl = UIManager:IsOpen(PanelId.CommonTaskTrackHud)
        if not success then
            return
        end
        self.m_taskTrackCtrl = taskTrackCtrl
    end

    local followNode = DeviceInfo.usingTouch and self.m_taskTrackCtrl:GetContentTopFollowNode() or
            self.m_taskTrackCtrl:GetContentBottomFollowNode()
    if NotNull(followNode) then
        self.view.btnGroup.position = followNode.position
    end

    if DeviceInfo.usingTouch then
        self.view.btnReset.transform.position = self.m_taskTrackCtrl:GetResetBtnRect().position
    end
end

BlackBoxDiffBtnCtrl._OnBtnResetClick = HL.Method() << function(self)
    local curStatus = GameInstance.dungeonManager.curDungeonLikeSubGame:GetCurrentCompletionStatus()
    if self.m_curPhase == Phase.Fail then
        self:_DoReset(curStatus)
    else
        self:Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_DUNGEON_RESET_BLACKBOX_CONFIRM,
            onConfirm = function()
                self:_DoReset(curStatus)
            end,
            onCancel = function()
            end
        })
    end
end

BlackBoxDiffBtnCtrl._DoReset = HL.Method(HL.Any) << function(self, status)
    if status then
        GameInstance.dungeonManager.curDungeonLikeSubGame:UserSendReStartAtStatus(status)
    else
        GameInstance.dungeonManager.curDungeonLikeSubGame:SendReStart(true)
    end
end


BlackBoxDiffBtnCtrl.OnLeaveMainHud = HL.Method() << function(self)
    UIManager:Hide(PanelId.CommonPopUp)
end

BlackBoxDiffBtnCtrl.OnHudBtnVisibleChange = HL.Method(HL.Any) << function(self, arg)
    local isOn = unpack(arg)
    self.view.panel.gameObject:SetActive(isOn)
end

BlackBoxDiffBtnCtrl.OnShowBlackboxResult = HL.Method(HL.Any) << function(self, args)
    local dungeonId, levelTimestamp, isFail, failReason = unpack(args)
    if isFail then
        return
    end

    self.m_hasObtainReward = true
end

BlackBoxDiffBtnCtrl.OnToggleFacTopView = HL.Method(HL.Boolean) << function(self, active)
    self:_UpdateDiffBtnBinding()

    if DeviceInfo.usingTouch then

        if active then
            self:ToTop()
        end
    end
end

BlackBoxDiffBtnCtrl._UpdateDiffBtnBinding = HL.Method() << function(self)
    if DeviceInfo.usingKeyboard and LuaSystemManager.factory.inTopView and GameSettingUtils.GetSettingValueBool(GameSettingConst.SETTING_ID_FAC_TOP_VIEW_ESC_EXIT) then
        self.view.blackboxDiffStateBtn.onClick:StopUseBinding()
    else
        self.view.blackboxDiffStateBtn.onClick:ChangeBindingPlayerAction("common_task_track_stop")
    end
end

BlackBoxDiffBtnCtrl.OnShow = HL.Override() << function(self)
    self:_UpdateDiffBtnBinding()
end

HL.Commit(BlackBoxDiffBtnCtrl)
