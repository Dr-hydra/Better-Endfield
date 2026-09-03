local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.DialogTimeline
PhaseDialogTimeline = HL.Class('PhaseDialogTimeline', phaseBase.PhaseBase)





PhaseDialogTimeline.m_panelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseDialogTimeline.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_PLAY_DIALOG_TIMELINE] = { 'OnPlayDialogTimeline', false },
    [MessageConst.ON_FINISH_DIALOG_TIMELINE] = { 'OnFinishDialogTimeline', true },
    [MessageConst.ON_SHOW_DIALOG_TIMELINE_OPTION] = { 'OnShowDialogOption', true },
    [MessageConst.OPEN_DIALOG_TIMELINE_RECORD] = { '_OpenDialogRecord', true },
    [MessageConst.HIDE_DIALOG_RECORD] = { '_HideDialogRecord', true },

    [MessageConst.OPEN_DIALOG_TIMELINE_SKIP_POP_UP] = { '_OpenDialogSkipPopUp', true },
    [MessageConst.HIDE_DIALOG_TIMELINE_SKIP_POP_UP] = { '_HideDialogSkipPopUp', true },
    [MessageConst.SKIP_DIALOG_TIMELINE] = { '_SkipDialogTimeline', true },
}

PhaseDialogTimeline.OnPlayDialogTimeline = HL.StaticMethod(HL.Table) << function(arg)
    arg.fast = true
    local isOpen, phase = PhaseManager:IsOpen(PHASE_ID)
    if isOpen then
        phase:ClearOut()
    end
    PhaseDialogTimeline.AutoOpen(PHASE_ID, arg)

    Notify(MessageConst.ON_LOAD_NEW_DLG_TIMELINE, arg)
end



PhaseDialogTimeline._OnInit = HL.Override() << function(self)
    PhaseDialogTimeline.Super._OnInit(self)
end

PhaseDialogTimeline.ClearOut = HL.Method() << function(self)
    if self.m_panelItem then
        self.m_panelItem.uiCtrl.animationWrapper:ClearTween(true)
    end
end


PhaseDialogTimeline._InitAllPhaseItems = HL.Override() << function(self)
    PhaseDialogTimeline.Super._InitAllPhaseItems(self)
    self.m_panelItem = self:_GetPanelPhaseItem(PanelId.DialogTimeline)
end



PhaseDialogTimeline.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseDialogTimeline._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDialogTimeline._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDialogTimeline._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDialogTimeline._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    Notify(MessageConst.ON_DIALOG_PHASE_BACK_TO_TOP)
end






PhaseDialogTimeline._OnActivated = HL.Override() << function(self)

    UIManager:Hide(PanelId.DialogMask)
end

PhaseDialogTimeline._OnDeActivated = HL.Override() << function(self)
    UIManager:Show(PanelId.DialogMask)
end

PhaseDialogTimeline._OnDestroy = HL.Override() << function(self)
    PhaseDialogTimeline.Super._OnDestroy(self)
    self.m_panelItem = nil
end



PhaseDialogTimeline.OnFinishDialogTimeline = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    local fast = false
    if arg then
        fast = unpack(arg)
    end


    UIManager:Hide(PanelId.CommonPopUp)

    if not fast then

        self.m_panelItem.uiCtrl:PlayAnimationOutWithCallback(function()
            if PhaseManager:IsOpen(PHASE_ID) then
                self:ExitSelfFast()
            end




        end)
    else
        self:ClearOut()
        if PhaseManager:IsOpen(PHASE_ID) then
            self:ExitSelfFast()
        end
    end
end

PhaseDialogTimeline.OnShowDialogOption = HL.Method(HL.Table) << function(self, data)
    local options = unpack(data)
    self:_DoShowDialogOption(options)
end

PhaseDialogTimeline._DoShowDialogOption = HL.Method(HL.Userdata) << function
(self, options)
    self.m_panelItem.uiCtrl:SetTrunkOption(options)
end



PhaseDialogTimeline._OpenDialogSkipPopUp = HL.Method() << function(self)
    local summaryId = GameWorld.dialogTimelineManager.summaryId
    local curAuto = GameWorld.dialogTimelineManager.autoMode
    GameWorld.dialogTimelineManager:SetAutoMode(false)
    if summaryId == nil or string.isEmpty(summaryId) then
        summaryId = GameWorld.dialogManager.summaryId
    end

    if summaryId == nil or string.isEmpty(summaryId) then
        local dialogId = GameWorld.dialogTimelineManager.dialogId
        if not string.isEmpty(dialogId) then
            Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_CONFIRM_SKIP_DIALOG,
                onConfirm = function()
                    if curAuto then
                        GameWorld.dialogManager:SetAutoMode(curAuto)
                    end
                    GameWorld.dialogTimelineManager:SkipDialog()
                end,
                onCancel = function()
                    if curAuto then
                        GameWorld.dialogManager:SetAutoMode(curAuto)
                    end
                end,
            })
        end
    else
        local panelItem = self:_GetPanelPhaseItem(PanelId.DialogSkipPopUp)
        if not panelItem then
            panelItem = self:CreatePhasePanelItem(PanelId.DialogSkipPopUp, {
                confirmMessage = MessageConst.SKIP_DIALOG_TIMELINE,
                cancelMessage = MessageConst.HIDE_DIALOG_TIMELINE_SKIP_POP_UP
            })
        end
        panelItem.uiCtrl:Show()
        panelItem.uiCtrl:RefreshSummary(summaryId)
        if curAuto then
            panelItem.uiCtrl:SetCloseRecoverAuto()
        end
    end
end

PhaseDialogTimeline._HideDialogSkipPopUp = HL.Method() << function(self)
    local panelItem = self:_GetPanelPhaseItem(PanelId.DialogSkipPopUp)
    if panelItem then
        panelItem.uiCtrl:Hide()
    end
end

PhaseDialogTimeline._SkipDialogTimeline = HL.Method() << function(self)
    self:_HideDialogSkipPopUp()
    GameWorld.dialogTimelineManager:SkipDialog()
end



PhaseDialogTimeline._OpenDialogRecord = HL.Method() << function(self)
    GameWorld.dialogTimelineManager:SetAutoMode(false)
    local panelItem = self:_GetPanelPhaseItem(PanelId.DialogRecord)
    if not panelItem then
        panelItem = self:CreatePhasePanelItem(PanelId.DialogRecord)
    end
    panelItem.uiCtrl:Show()
end

PhaseDialogTimeline._HideDialogRecord = HL.Method() << function(self)
    local panelItem = self:_GetPanelPhaseItem(PanelId.DialogRecord)
    if panelItem then
        panelItem.uiCtrl:Hide()
    end
end


HL.Commit(PhaseDialogTimeline)

