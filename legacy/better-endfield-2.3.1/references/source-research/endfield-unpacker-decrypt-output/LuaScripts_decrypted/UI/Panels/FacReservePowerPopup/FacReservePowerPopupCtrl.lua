local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacReservePowerPopup
local PHASE_ID = PhaseId.FacReservePowerPopup

FacReservePowerPopupCtrl = HL.Class('FacReservePowerPopupCtrl', uiCtrl.UICtrl)

local E_BACKUP_POWER_STATE = {
    InUse = 1,
    UnderRecovery = 2,
    Available = 3,
}

local eventLogActionType = {
    OpenPopup = "1",
    Use = "2",
    Cancel = "3",
    ToggleOn = "4",
    ToggleOff = "5",
}

local USE_DURATION = Tables.factoryConst.facBackUpPowerDuration
local RECOVERY_DURATION = Tables.factoryConst.facBackUpPowerCooldownTime





FacReservePowerPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacReservePowerPopupCtrl.m_backupPowerState = HL.Field(HL.Any)

FacReservePowerPopupCtrl.m_chapterId = HL.Field(HL.Number) << -1

FacReservePowerPopupCtrl.m_domainId = HL.Field(HL.String) << ""

FacReservePowerPopupCtrl.m_useEndTime = HL.Field(HL.Number) << -1

FacReservePowerPopupCtrl.m_recoveryEndTime = HL.Field(HL.Number) << -1

FacReservePowerPopupCtrl.m_realPowerEnough = HL.Field(HL.Boolean) << false

FacReservePowerPopupCtrl.m_isAutoUse = HL.Field(HL.Boolean) << false

FacReservePowerPopupCtrl.m_recoverOpen = HL.Field(HL.Boolean) << false

FacReservePowerPopupCtrl.m_cor = HL.Field(HL.Thread)


FacReservePowerPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitData(arg)
    self:_InitUI()
    self:_RefreshAllUIs()
end

FacReservePowerPopupCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_chapterId = arg.chapterId
    self.m_useEndTime = arg.useEndTime
    self.m_recoveryEndTime = arg.recoveryEndTime
    self.m_realPowerEnough = arg.isRealPowerEnough
    self.m_isAutoUse = arg.isAutoUse
    self.m_domainId = ScopeUtil.ChapterIdInt2Str(self.m_chapterId)
    local currentTimeTs = DateTimeUtils.GetCurrentTimestampBySeconds()

    if currentTimeTs < self.m_useEndTime then
        self.m_backupPowerState = E_BACKUP_POWER_STATE.InUse
    elseif currentTimeTs < self.m_recoveryEndTime then
        self.m_backupPowerState = E_BACKUP_POWER_STATE.UnderRecovery
    else
        self.m_backupPowerState = E_BACKUP_POWER_STATE.Available
        LuaSystemManager.factory:SetBackupPowerViewed(self.m_chapterId)
    end

    self.m_recoverOpen = PhaseManager.isRecovering
    if not self.m_recoverOpen then
        EventLogManagerInst:GameEvent_BackupPowerSupply(eventLogActionType.OpenPopup, self.m_domainId)
    end
    CS.Beyond.Gameplay.Conditions.OnFacOpenBackupPowerPopup.Trigger(self.m_backupPowerState)

    self.m_cor = self:_ClearCoroutine(self.m_cor)
    self.m_cor = self:_StartCoroutine(function()
        while true do
            self:_UpdateData()
            self:_UpdateRemainPowerNum()
            self:_RefreshAllUIs()
            coroutine.wait(1)
        end
    end)
end

FacReservePowerPopupCtrl._InitUI = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        self:_OnCancelUse()
    end)
    self.view.downState.cancelBtn.onClick:AddListener(function()
        self:_OnCancelUse()
    end)
    self.view.downState.confirmBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.downState.useBtn.onClick:AddListener(function()
        self:_OnConfirmUse()
    end)
    self.view.rightState.toggle.isOn = self.m_isAutoUse

    if DeviceInfo.usingController then
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    end


    self:BindInputPlayerAction("fac_open_backup_power_popup", function()
        self:_OnCancelUse()
    end)
end

FacReservePowerPopupCtrl._OnCancelUse = HL.Method() << function(self)
    if self.m_backupPowerState == E_BACKUP_POWER_STATE.Available then
        if self.view.rightState.toggle.isOn then
            Notify(MessageConst.SHOW_POP_UP,{
                    content = Language.LUA_FAC_BACKUP_POWER_AUTO_USE_SECOND_CONFIRM_HINT,
                    onConfirm = function()
                        self:_OnConfirmUse()
                    end
                })
        else
            EventLogManagerInst:GameEvent_BackupPowerSupply(eventLogActionType.Cancel, self.m_domainId)
            PhaseManager:PopPhase(PHASE_ID)
        end
    else
        PhaseManager:PopPhase(PHASE_ID)
    end
end

FacReservePowerPopupCtrl._OnConfirmUse = HL.Method() << function(self)
    GameInstance.player.remoteFactory.core:Message_UseBackupPower(self.m_chapterId)
    EventLogManagerInst:GameEvent_BackupPowerSupply(eventLogActionType.Use, self.m_domainId)
    PhaseManager:PopPhase(PHASE_ID)
end

FacReservePowerPopupCtrl._UpdateData = HL.Method() << function(self)

    local powerData = FactoryUtils.getRegionPowerInfoByChapterId(self.m_chapterId)
    if powerData then
        self.m_realPowerEnough = powerData.powerGen >= powerData.powerCost
        self.m_useEndTime = powerData.backupLastStartTs + USE_DURATION
        self.m_recoveryEndTime = self.m_useEndTime + RECOVERY_DURATION

        if self.m_realPowerEnough and self.m_backupPowerState ~= E_BACKUP_POWER_STATE.InUse then
            PhaseManager:PopPhase(PHASE_ID)
            return
        end
    end


    local currentTimeTs = DateTimeUtils.GetCurrentTimestampBySeconds()
    if currentTimeTs < self.m_useEndTime then
        self.m_backupPowerState = E_BACKUP_POWER_STATE.InUse
    elseif currentTimeTs < self.m_recoveryEndTime then
        self.m_backupPowerState = E_BACKUP_POWER_STATE.UnderRecovery
    else
        self.m_backupPowerState = E_BACKUP_POWER_STATE.Available
    end
end

FacReservePowerPopupCtrl._UpdateRemainPowerNum = HL.Method() << function(self)

    if self.m_backupPowerState == E_BACKUP_POWER_STATE.Available then
        return
    end

    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local leftTime
    if self.m_backupPowerState == E_BACKUP_POWER_STATE.InUse then
        leftTime = self.m_useEndTime - curTime
    else
        leftTime = self.m_recoveryEndTime - curTime
    end

    self.view.rightState.timeNumText:SetAndResolveTextStyle(UIUtils.getLeftTimeToSecond(leftTime, true))
    local remainPowerPercent
    if self.m_backupPowerState == E_BACKUP_POWER_STATE.InUse then
        remainPowerPercent = math.ceil(leftTime / USE_DURATION  * 100)
    else
        remainPowerPercent = math.floor((RECOVERY_DURATION - leftTime) / RECOVERY_DURATION  * 100)
    end
    remainPowerPercent = lume.clamp(remainPowerPercent, 0, 100)
    self.view.leftState.remainPowerTxt.gameObject:SetActive(true)
    self.view.leftState.remainPowerTxt.text = string.format("%d%%", remainPowerPercent)
    self.view.leftState.remainPowerBar.fillAmount = remainPowerPercent / 100
end

FacReservePowerPopupCtrl._RefreshAllUIs = HL.Method() << function(self)
    if self.m_backupPowerState == E_BACKUP_POWER_STATE.InUse then
        self.view.leftState.stateController:SetState("Use")
        self.view.rightState.stateController:SetState("InUse")

        self.view.rightState.hintTxt.text = string.format(self.m_realPowerEnough and Language.LUA_FAC_BACKUP_POWER_ENOUGH_REAL_POWER_HINT or Language.LUA_FAC_BACKUP_POWER_NO_REAL_POWER_HINT)

        self.view.rightState.stateController:SetState(self.m_realPowerEnough and "Grey" or "Red")
        self.view.downState.stateController:SetState("Confirm")
    elseif self.m_backupPowerState == E_BACKUP_POWER_STATE.UnderRecovery then
        self.view.leftState.stateController:SetState("Recover")
        self.view.rightState.stateController:SetState("UnderRecovery")
        self.view.downState.stateController:SetState("Confirm")
    else
        self.view.leftState.stateController:SetState("Use")
        self.view.leftState.remainPowerTxt.gameObject:SetActive(false)
        self.view.leftState.remainPowerBar.fillAmount = 1
        self.view.rightState.stateController:SetState("Available")
        self.view.rightState.stateController:SetState("Grey")
        self.view.rightState.descTxt.text = string.format(Language.LUA_FAC_BACKUP_POWER_USE_HINT, UIUtils.getShortLeftTime(USE_DURATION))
        self.view.rightState.hintTxt.text = string.format(Language.LUA_FAC_BACKUP_POWER_RECOVER_CONSUME_HINT, UIUtils.getShortLeftTime(RECOVERY_DURATION))
        self.view.downState.stateController:SetState("Use")
    end
    if DeviceInfo.usingController then
        InputManagerInst:ToggleGroup(self.view.closeBtnInputGroup.groupId, self.m_backupPowerState ~= E_BACKUP_POWER_STATE.Available)
    end
end

FacReservePowerPopupCtrl.OnClose = HL.Override() << function(self)
    self.m_cor = self:_ClearCoroutine(self.m_cor)
    if InputManagerInst.inChangingInputDevice then
        return
    end

    local finalToggleState = self.view.rightState.toggle.isOn
    if finalToggleState ~= self.m_isAutoUse or self.m_recoverOpen then
        GameInstance.player.remoteFactory.core:Message_SetBackupPowerScopeOption(ScopeUtil.GetCurrentScope():GetHashCode(), finalToggleState)
        EventLogManagerInst:GameEvent_BackupPowerSupply(finalToggleState and eventLogActionType.ToggleOn or eventLogActionType.ToggleOff, self.m_domainId)
    end
end

FacReservePowerPopupCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Any) << function(self)
    return {
        chapterId = self.m_chapterId,
        useEndTime = self.m_useEndTime,
        recoveryEndTime = self.m_recoveryEndTime,
        isRealPowerEnough = self.m_realPowerEnough,
        isAutoUse = self.view.rightState.toggle.isOn,
    }
end

HL.Commit(FacReservePowerPopupCtrl)
