
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.ReflowFormalReconnect
PhaseReflowFormalReconnect = HL.Class('PhaseReflowFormalReconnect', phaseBase.PhaseBase)





PhaseReflowFormalReconnect.s_messages = HL.StaticField(HL.Table) << {

}

PhaseReflowFormalReconnect.m_curPanelItem = HL.Field(HL.Forward("PhasePanelItem"))
PhaseReflowFormalReconnect.m_panelItemDic = HL.Field(HL.Table)
PhaseReflowFormalReconnect.m_reconnectPanel = HL.Field(HL.Forward("PhasePanelItem"))
PhaseReflowFormalReconnect.m_instructionShowState = HL.Field(HL.Boolean) << false
PhaseReflowFormalReconnect.m_instructionId = HL.Field(HL.String) << ''


PhaseReflowFormalReconnect._OnInit = HL.Override() << function(self)
    PhaseReflowFormalReconnect.Super._OnInit(self)
end



PhaseReflowFormalReconnect._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.m_panelItemDic = {}
    local arg = self.arg or {}
    arg.phase = self
    self.m_reconnectPanel = self:CreatePhasePanelItem(PanelId.ReflowFormalReconnect, arg)
    self:_BindControllerHintPlaceHolder()


    if arg.showInstruction then
        self:ShowInstruction(arg.instructionId)
        arg.showInstruction = false
    end
end




PhaseReflowFormalReconnect._OnDestroy = HL.Override() << function(self)
    PhaseReflowFormalReconnect.Super._OnDestroy(self)
end


PhaseReflowFormalReconnect.OnTabChange = HL.Method(HL.Table) << function(self, arg)
    if arg.panelId == nil then
        return
    end

    if self.m_curPanelItem then
        local preUiCtrl = self.m_curPanelItem.uiCtrl
        if self.m_curPanelItem.uiCtrl.panelId == arg.panelId then
            return
        else
            self.m_curPanelItem:ClearNaviTarget()
        end
        local curUiAnimationWrapper = self.m_curPanelItem.uiCtrl.view.animationWrapper
        if curUiAnimationWrapper then
            curUiAnimationWrapper:ClearTween(false)
            curUiAnimationWrapper:PlayOutAnimation(function()
                preUiCtrl:Hide()
                self:_OpenTab(arg)
                self:_BindControllerHintPlaceHolder()
            end)
        else
            preUiCtrl:Hide()
            self:_OpenTab(arg)
            self:_BindControllerHintPlaceHolder()
        end
    else
        self:_OpenTab(arg)
        self:_BindControllerHintPlaceHolder()
    end
end

PhaseReflowFormalReconnect._OpenTab = HL.Method(HL.Table) << function(self, arg)
    local panelId = arg.panelId
    local panelItem
    if self.m_panelItemDic[panelId] then
        panelItem = self.m_panelItemDic[panelId]
        panelItem.uiCtrl:Show()
    else
        local tabArg = {}
        tabArg.phase = self
        tabArg.activityId = self.arg.activityId
        tabArg.hasPopupSpec = true
        tabArg.replayScrollAnimOnShow = true
        panelItem = self:CreatePhasePanelItem(panelId, tabArg)
        self.m_panelItemDic[panelId] = panelItem
    end
    local animationWrapper = panelItem.uiCtrl.view.animationWrapper
    if animationWrapper then
        animationWrapper:ClearTween(false)
        animationWrapper:PlayInAnimation()
    end
    self.m_curPanelItem = panelItem
end

PhaseReflowFormalReconnect._BindControllerHintPlaceHolder = HL.Method() << function(self)
    if not self.m_reconnectPanel or not self.m_curPanelItem then
        return
    end
    local reconnectCtrl = self.m_reconnectPanel.uiCtrl
    if reconnectCtrl then
        if self.m_curPanelItem.uiCtrl.panelId == PanelId.ActivityReflowSignin then
            self.m_curPanelItem.uiCtrl.m_checkInWidget.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
                reconnectCtrl.view.inputGroup.groupId,
                self.m_curPanelItem.uiCtrl.view.inputGroup.groupId,
            })
        else
            self.m_curPanelItem.uiCtrl.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
                reconnectCtrl.view.inputGroup.groupId,
                self.m_curPanelItem.uiCtrl.view.inputGroup.groupId,
            })
        end
    end
end

PhaseReflowFormalReconnect.ShowInstruction = HL.Method(HL.String) << function(self, instructionId)
    self.m_instructionShowState = true
    self.m_instructionId = instructionId
    UIManager:Open(PanelId.InstructionBook, {
        id = instructionId,
        onClose = function()
            self.m_instructionShowState = false
        end
    })
end

PhaseReflowFormalReconnect.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.copy(self.arg) or {}
    arg.panelId = self.m_curPanelItem.uiCtrl.panelId
    arg.phase = nil
    arg.showInstruction = self.m_instructionShowState
    arg.instructionId = self.m_instructionId
    return arg
end

HL.Commit(PhaseReflowFormalReconnect)

