local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityScratchOffLotteryPopup

ActivityScratchOffLotteryPopupCtrl = HL.Class('ActivityScratchOffLotteryPopupCtrl', uiCtrl.UICtrl)

local DELTA_TIME_TO_STICK_VALUE_RATIO = 100.0

ActivityScratchOffLotteryPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivityScratchOffLotteryPopupCtrl.m_activityId = HL.Field(HL.String) << ""

ActivityScratchOffLotteryPopupCtrl.m_activity = HL.Field(HL.Userdata)

ActivityScratchOffLotteryPopupCtrl.m_scratchCursorTick = HL.Field(HL.Number) << -1

ActivityScratchOffLotteryPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local activitySystem = GameInstance.player.activitySystem

    self.view.closeBtn.onClick:AddListener(function()
        self:_OnCloseBtnClicked()
    end)

    self:BindInputPlayerAction("activity_scratch_off_lottery_quick_scratch", function()
        self:_SetScratchCursorVisible(false)
        self.view.lotteryInfo:QuickScratch()
    end, self.view.inputGroup.groupId)

    self.m_activityId = args.activityId
    self.m_activity = activitySystem:GetActivity(args.activityId)

    local lotteryInfoArgs = {
        activityId = args.activityId,
        canScratch = true,
        onScratchCompletedLocally = function()
            if self.m_isClosed then
                return
            end
            self:_SetScratchCursorVisible(false)
        end,
        onScratchCompleted = function()
            if self.m_isClosed then
                return
            end
            self:_CloseSelf()
        end,
    }
    self.view.lotteryInfo:InitActivityScratchOffLotteryInfo(lotteryInfoArgs)

    if DeviceInfo.usingController then
        if self.m_activity.isScratchCompleted then
            self:_SetScratchCursorVisible(false)
        else
            self:_SetScratchCursorVisible(true)

            local screenPosition = self:_GetScratchCursorScreenPosition()
            self.view.lotteryInfo:BeginScratch(screenPosition)

            self.m_scratchCursorTick = LuaUpdate:Add("Tick", function(deltaTime)
                self:_UpdateScratchCursor(deltaTime)
            end)
        end
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

ActivityScratchOffLotteryPopupCtrl.OnShow = HL.Override() << function(self)
    self:_UpdateView()
end

ActivityScratchOffLotteryPopupCtrl.OnClose = HL.Override() << function(self)
    local screenPosition = self:_GetScratchCursorScreenPosition()
    self.view.lotteryInfo:EndScratch(screenPosition)

    self.m_scratchCursorTick = LuaUpdate:Remove(self.m_scratchCursorTick)
end

ActivityScratchOffLotteryPopupCtrl._UpdateView = HL.Method() << function(self)
    self.view.lotteryInfo:Refresh()
end

ActivityScratchOffLotteryPopupCtrl._SetScratchCursorVisible = HL.Method(HL.Boolean) << function(self, visible)
    self.view.scratchCursor.gameObject:SetActive(visible)
    if not visible then
        self.m_scratchCursorTick = LuaUpdate:Remove(self.m_scratchCursorTick)
    end
end

ActivityScratchOffLotteryPopupCtrl._GetScratchCursorScreenPosition = HL.Method().Return(HL.Userdata) << function(self)
    local worldPosition = self.view.scratchCursor.position
    local screenPosition = Unity.RectTransformUtility.WorldToScreenPoint(self.uiCamera, worldPosition)
    return screenPosition
end

ActivityScratchOffLotteryPopupCtrl._UpdateScratchCursor = HL.Method(HL.Number) << function(self, deltaTime)
    local stickValue = InputManagerInst:GetGamepadStickValue(false)
    if stickValue == Vector2.zero then
        return
    end

    self.view.scratchHint.gameObject:SetActive(false)

    stickValue = DELTA_TIME_TO_STICK_VALUE_RATIO * deltaTime * stickValue

    local deltaPosition = stickValue * self.view.config.SCRATCH_CURSOR_MOVE_SPEED
    local prevAnchoredPosition = self.view.scratchCursor.anchoredPosition
    local currAnchoredPosition = prevAnchoredPosition + deltaPosition

    local sourceRect = self.view.scratchCursor.parent
    local targetRect = self.view.lotteryInfo.view.scratchArea
    currAnchoredPosition = CSUtils.ClampPointInRectTransform(sourceRect, targetRect, currAnchoredPosition)
    self.view.scratchCursor.anchoredPosition = currAnchoredPosition

    local screenPosition = self:_GetScratchCursorScreenPosition()
    self.view.lotteryInfo:ApplyScratch(screenPosition)
end

ActivityScratchOffLotteryPopupCtrl._CloseSelf = HL.Method() << function(self)
    self:PlayAnimationOutWithCallback(function()
        self.m_phase:RemovePhasePanelItemById(PanelId.ActivityScratchOffLotteryPopup)
    end)
end

ActivityScratchOffLotteryPopupCtrl._OnCloseBtnClicked = HL.Method() << function(self)
    if self.m_activity.isScratchCompleted then
        return
    end


    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_ACTIVITY_SCRATCH_OFF_LOTTERY_INTERRUPT_POP_UP_CONTENT,
        onConfirm = function()
            self:_CloseSelf()
        end
    })
end

HL.Commit(ActivityScratchOffLotteryPopupCtrl)
