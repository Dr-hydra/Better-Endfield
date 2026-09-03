local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.BattlePassAdvancedPlanBuy

PhaseBattlePassAdvancedPlanBuy = HL.Class('PhaseBattlePassAdvancedPlanBuy', phaseBase.PhaseBase)

PhaseBattlePassAdvancedPlanBuy.m_haveShowPsStoreLogo = HL.Field(HL.Boolean) << false

PhaseBattlePassAdvancedPlanBuy.m_storeShowPsStoreLogo = HL.Field(HL.Boolean) << false

PhaseBattlePassAdvancedPlanBuy.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_START_WEB_APPLICATION] = { '_OnStartPayment', true },
    [MessageConst.ON_CLOSE_WEB_APPLICATION] = { '_OnClosePayment', true },
}

PhaseBattlePassAdvancedPlanBuy._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self:ShowPsStore()
    local weaponCaseArg = self.arg and self.arg.weaponCaseArg
    if weaponCaseArg then
        UIManager:Open(PanelId.BattlePassWeaponCase, weaponCaseArg)
        self.arg.weaponCaseArg = nil
    end
end

PhaseBattlePassAdvancedPlanBuy._OnDestroy = HL.Override() << function(self)
    self:HidePsStore()
    PhaseBattlePassAdvancedPlanBuy.Super._OnDestroy(self)
end

PhaseBattlePassAdvancedPlanBuy._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.m_storeShowPsStoreLogo = self.m_haveShowPsStoreLogo
    self:HidePsStore()
end

PhaseBattlePassAdvancedPlanBuy._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.m_storeShowPsStoreLogo then
        self.m_storeShowPsStoreLogo = false
        self:ShowPsStore()
    end
end

PhaseBattlePassAdvancedPlanBuy.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or nil
    if arg then
        arg.weaponCaseArg = nil
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return arg
    end
    local isOpen, ctrl = UIManager:IsOpen(PanelId.BattlePassWeaponCase)
    if isOpen and ctrl ~= nil and ctrl:IsShow(false) then
        arg = arg or {}
        arg.weaponCaseArg = ctrl:GetCurPhaseStateArg()
    end
    return arg
end

PhaseBattlePassAdvancedPlanBuy.ShowPsStore = HL.Method() << function(self)
    if BattlePassUtils.CheckBattlePassPurchaseBlock() then
        return
    end
    if self.m_haveShowPsStoreLogo then
        return
    end
    self.m_haveShowPsStoreLogo = true
    CashShopUtils.ShowPsStore()
end

PhaseBattlePassAdvancedPlanBuy.HidePsStore = HL.Method() << function(self)
    if BattlePassUtils.CheckBattlePassPurchaseBlock() then
        return
    end
    if not self.m_haveShowPsStoreLogo then
        return
    end
    self.m_haveShowPsStoreLogo = false
    CashShopUtils.HidePsStore()
end

PhaseBattlePassAdvancedPlanBuy._OnStartPayment = HL.Method(HL.Table) << function(self, arg)
    local key = unpack(arg)
    if key ~= CS.Beyond.SDK.PaymentEasyAccess.MASK_KEY_PAYMENT then
        return
    end

    self.m_storeShowPsStoreLogo = self.m_haveShowPsStoreLogo
    self:HidePsStore()
end

PhaseBattlePassAdvancedPlanBuy._OnClosePayment = HL.Method(HL.Table) << function(self, arg)
    local key = unpack(arg)
    if key ~= CS.Beyond.SDK.PaymentEasyAccess.MASK_KEY_PAYMENT then
        return
    end
    if self.m_storeShowPsStoreLogo then
        self.m_storeShowPsStoreLogo = false
        self:ShowPsStore()
    end
end

HL.Commit(PhaseBattlePassAdvancedPlanBuy)
