local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.BattlePass
PhaseBattlePass = HL.Class('PhaseBattlePass', phaseBase.PhaseBase)





PhaseBattlePass.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_START_WEB_APPLICATION] = { '_OnStartPayment', true },
    [MessageConst.ON_CLOSE_WEB_APPLICATION] = { '_OnClosePayment', true },
}


PhaseBattlePass.m_panelItemDic = HL.Field(HL.Table)

PhaseBattlePass.m_basePanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseBattlePass.m_curPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseBattlePass.m_transCoroutine = HL.Field(HL.Thread)

PhaseBattlePass.m_isChanging = HL.Field(HL.Boolean) << false

PhaseBattlePass.m_bpEndTimer = HL.Field(HL.Number) << 0

PhaseBattlePass.m_haveShowPsStoreLogo = HL.Field(HL.Boolean) << false

PhaseBattlePass.m_storeShowPsStoreLogo = HL.Field(HL.Boolean) << false


PhaseBattlePass._OnInit = HL.Override() << function(self)
    PhaseBattlePass.Super._OnInit(self)
end




PhaseBattlePass.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseBattlePass._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.m_panelItemDic = {}
    if self:_TryOpenBattlePassDisplay() then
        return
    end
    self.m_basePanel = self:CreatePhasePanelItem(PanelId.BattlePass, self.arg)
    self:_TryPopPanel()
    self:_TryRecoverInstructionBook()
end

PhaseBattlePass._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
end

PhaseBattlePass._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.m_storeShowPsStoreLogo = self.m_haveShowPsStoreLogo
    self:HidePsStore()
end

PhaseBattlePass._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.m_storeShowPsStoreLogo then
        self.m_storeShowPsStoreLogo = false
        self:ShowPsStore()
    end
end






PhaseBattlePass._OnActivated = HL.Override() << function(self)
    local timeToEnd = BattlePassUtils.GetSeasonLeftTime()
    self.m_bpEndTimer = TimerManager:StartTimer(timeToEnd, function()
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_BATTLE_PASS_END_POPUP,
            onConfirm = function()
                self:CloseSelf()
            end,
            hideCancel = true,
        })
        TimerManager:ClearTimer(self.m_bpEndTimer)
        self.m_bpEndTimer = 0
    end)
end

PhaseBattlePass._OnDeActivated = HL.Override() << function(self)
    if self.m_bpEndTimer > 0 then
        TimerManager:ClearTimer(self.m_bpEndTimer)
        self.m_bpEndTimer = 0
    end
end

PhaseBattlePass.CloseSelf = HL.Override() << function(self)
    if self.arg and self.arg.fromPhase == PhaseId.CashShop then
        PhaseManager:OpenPhaseFast(PhaseId.CashShop)
    end
    PhaseBattlePass.Super.CloseSelf(self)
end

PhaseBattlePass._OnDestroy = HL.Override() << function(self)
    self:HidePsStore()
    PhaseBattlePass.Super._OnDestroy(self)
end

PhaseBattlePass._OnRefresh = HL.Override() << function(self)
    if not self.m_basePanel then
        return
    end
    if self.arg and self.arg.panelId and self.m_curPanel.uiCtrl.panelId ~= self.arg.panelId then
        Notify(MessageConst.ON_CHANGE_BATTLE_PASS_TAB, self.arg)
    elseif self.m_curPanel then
        self.m_curPanel.uiCtrl:OnPhaseRefresh(self.arg)
    end
end

PhaseBattlePass.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    if self.m_basePanel == nil or self.m_basePanel.uiCtrl == nil then
        return arg
    end
    local recoverState = self.m_basePanel.uiCtrl:GetRecoverStateArg()
    if recoverState ~= nil then
        arg.panelId = recoverState.panelId
        arg.panelArgs = recoverState.panelArgs
    end
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() and instructionCtrl.id == "battle_pass" and PhaseManager:GetTopPhaseId() == PHASE_ID then
        arg.instructionBookArg = {
            id = instructionCtrl.id,
        }
    else
        arg.instructionBookArg = nil
    end
    return arg
end


PhaseBattlePass._TryRecoverInstructionBook = HL.Method() << function(self)
    if self.arg == nil or self.arg.instructionBookArg == nil then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() then
        return
    end
    UIManager:Open(PanelId.InstructionBook, self.arg.instructionBookArg)
    self.arg.instructionBookArg = nil
end

PhaseBattlePass._TryOpenBattlePassDisplay = HL.Method().Return(HL.Boolean) << function(self)


    local bpSystem = GameInstance.player.battlePassSystem
    if bpSystem:IsSeasonUnread(GameInstance.player.battlePassSystem.seasonData.seasonId) then
        bpSystem:ResetAllReadTasks()
        local showingTaskIds = BattlePassUtils.GetShowingTaskIds()
        if #showingTaskIds > 0 then
            bpSystem:ReadTasks(showingTaskIds)
        end
        bpSystem:ReadSeason(GameInstance.player.battlePassSystem.seasonData.seasonId)
        UIManager:Open(PanelId.BattlePassSeasonDisplay, {
            onClose = function()

                if self.m_destroyed or self.m_panel2Item == nil or self.state == PhaseConst.EPhaseState.TransitionOut or self.state == PhaseConst.EPhaseState.WaitRelease then
                    return
                end
                local arg = nil
                if self.arg ~= nil then
                    arg = self.arg
                else
                    arg = {}
                end
                arg.popupPlanBuy = not BattlePassUtils.CheckBattlePassPurchaseBlock()
                self.m_basePanel = self:CreatePhasePanelItem(PanelId.BattlePass, arg)
                self:_TryPopPanel()
                if BattlePassUtils.CheckBattlePassPurchaseBlock() then
                    if UIManager:IsOpen(PanelId.BattlePassSeasonDisplay) then
                        UIManager:SetTopOrder(PanelId.BattlePassSeasonDisplay)
                    end
                    return
                end
                local popupPanelId = nil
                if arg ~= nil and arg.popupPanelId ~= nil then
                    popupPanelId = arg.popupPanelId
                end

                if popupPanelId ~= 'BattlePassAdvancedPlanBuy' then
                    PhaseManager:OpenPhaseFast(PhaseId.BattlePassAdvancedPlanBuy)
                end
                if UIManager:IsOpen(PanelId.BattlePassSeasonDisplay) then
                    UIManager:SetTopOrder(PanelId.BattlePassSeasonDisplay)
                end
            end,
        })
        return true
    end
    return false
end

PhaseBattlePass._TryPopPanel = HL.Method() << function(self)
    local popupPanelId = nil
    local popupPhase = false
    if self.arg ~= nil then
        if self.arg.popupPanelId ~= nil then
            popupPanelId = self.arg.popupPanelId
        end
        popupPhase = self.arg.popupPhase == true
    end
    if BattlePassUtils.CheckBattlePassPurchaseBlock() and popupPanelId == 'BattlePassAdvancedPlanBuy' then
        return
    end
    if popupPanelId ~= nil then
        if popupPhase then
            local phaseId = PhaseId[popupPanelId]
            PhaseManager:OpenPhaseFast(phaseId)
        else
            local panelId = PanelId[popupPanelId]
            UIManager:Open(panelId)
        end
    end
end

PhaseBattlePass.ChangePanel = HL.Method(HL.Number, HL.Boolean, HL.Opt(HL.Any)).Return(HL.Boolean) << function(self, panelId, isRight, arg)
    if self.m_isChanging then
        return false
    end
    self:_OpenPanel(panelId, isRight, arg)
    return true
end

PhaseBattlePass._OpenPanel = HL.Method(HL.Number, HL.Boolean, HL.Opt(HL.Any)) << function(self, panelId, isRight, arg)
    self:_ClearCoroutine(self.m_transCoroutine)
    self.m_transCoroutine = nil
    self.m_isChanging = false
    if self.m_curPanel ~= nil then
        self:_HidePanelImpl(self.m_curPanel, isRight,function()
            if self.m_curPanel.uiCtrl.panelId ~= panelId then
                self.m_curPanel:ClearNaviTarget()
            end
            self.m_curPanel = self:_OpenPanelImpl(panelId, isRight, arg)
        end)
    else
        self.m_curPanel = self:_OpenPanelImpl(panelId, isRight, arg, true)
    end
end

PhaseBattlePass._HidePanelImpl = HL.Method(HL.Forward("PhasePanelItem"), HL.Boolean, HL.Function)
    << function(self, panelItem, isRight, onPanelHide)
    if panelItem == nil and onPanelHide ~= nil then
        onPanelHide()
        return
    end
    local uiCtrl = panelItem.uiCtrl
    InputManagerInst.controllerNaviManager:TryRemoveLayer(uiCtrl.naviGroup)
    local outAniName = isRight and uiCtrl.view.config.ANI_OUT_RIGHT or uiCtrl.view.config.ANI_OUT_LEFT
    uiCtrl.animationWrapper:ClearTween(false)
    uiCtrl.animationWrapper:Play(outAniName, function()
        uiCtrl:Hide()
    end)
    self.m_isChanging = true
    self.m_transCoroutine = self:_StartCoroutine(function()
        coroutine.wait(self.m_basePanel.uiCtrl.view.config.TRANSITION_DELAY_TIME)
        onPanelHide()
        self.m_transCoroutine = nil
        self.m_isChanging = false
    end)
end

PhaseBattlePass._OpenPanelImpl = HL.Method(HL.Number, HL.Boolean, HL.Opt(HL.Any, HL.Boolean)).Return(HL.Forward("PhasePanelItem"))
    << function(self, panelId, isRight, arg, isFirstInit)
    isFirstInit = isFirstInit == true
    local panelItem
    if self.m_panelItemDic[panelId] then
        panelItem = self.m_panelItemDic[panelId]
        panelItem.uiCtrl:Show()
    else
        panelItem = self:CreatePhasePanelItem(panelId, arg)
        if panelItem == nil then
            return
        end
        self.m_panelItemDic[panelId] = panelItem
    end
    local uiCtrl = panelItem.uiCtrl
    uiCtrl.animationWrapper:ClearTween(false)
    if isFirstInit then
        uiCtrl.animationWrapper:PlayInAnimation()
    else
        uiCtrl.animationWrapper:SampleToInAnimationEnd()
        local inAniName = isRight and uiCtrl.view.config.ANI_IN_RIGHT or uiCtrl.view.config.ANI_IN_LEFT
        uiCtrl.animationWrapper:Play(inAniName)
    end
    return panelItem
end

PhaseBattlePass.ShowPsStore = HL.Method() << function(self)
    if BattlePassUtils.CheckBattlePassPurchaseBlock() then
        return
    end
    if self.m_haveShowPsStoreLogo then
        return
    end
    self.m_haveShowPsStoreLogo = true
    CashShopUtils.ShowPsStore()
end

PhaseBattlePass.HidePsStore = HL.Method() << function(self)
    if BattlePassUtils.CheckBattlePassPurchaseBlock() then
        return
    end
    if not self.m_haveShowPsStoreLogo then
        return
    end
    self.m_haveShowPsStoreLogo = false
    CashShopUtils.HidePsStore()
end

PhaseBattlePass._OnStartPayment = HL.Method(HL.Table) << function(self, arg)
    local key = unpack(arg)
    if key ~= CS.Beyond.SDK.PaymentEasyAccess.MASK_KEY_PAYMENT then
        return
    end

    self.m_storeShowPsStoreLogo = self.m_haveShowPsStoreLogo
    self:HidePsStore()
end

PhaseBattlePass._OnClosePayment = HL.Method(HL.Table) << function(self, arg)
    local key = unpack(arg)
    if key ~= CS.Beyond.SDK.PaymentEasyAccess.MASK_KEY_PAYMENT then
        return
    end
    if self.m_storeShowPsStoreLogo then
        self.m_storeShowPsStoreLogo = false
        self:ShowPsStore()
    end
end

HL.Commit(PhaseBattlePass)
