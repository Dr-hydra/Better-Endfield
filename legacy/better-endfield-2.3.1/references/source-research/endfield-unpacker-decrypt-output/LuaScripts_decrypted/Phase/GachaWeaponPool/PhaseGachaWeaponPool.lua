
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.GachaWeaponPool
PhaseGachaWeaponPool = HL.Class('PhaseGachaWeaponPool', phaseBase.PhaseBase)





PhaseGachaWeaponPool.s_messages = HL.StaticField(HL.Table) << {

}



PhaseGachaWeaponPool._OnInit = HL.Override() << function(self)
    PhaseGachaWeaponPool.Super._OnInit(self)
end




PhaseGachaWeaponPool.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    AudioAdapter.LoadAndPinEventsAsync({ UIConst.GACHA_MUSIC_UI })
end

PhaseGachaWeaponPool._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGachaWeaponPool._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    AudioAdapter.UnpinEvent(UIConst.GACHA_MUSIC_UI)
end

PhaseGachaWeaponPool._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGachaWeaponPool._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseGachaWeaponPool._OnActivated = HL.Override() << function(self)

    LuaSystemManager.gachaSystem:UpdateGachaMusicState()
end

PhaseGachaWeaponPool._OnDeActivated = HL.Override() << function(self)
    LuaSystemManager.gachaSystem:UpdateGachaWeaponSettingState()
end

PhaseGachaWeaponPool._OnDestroy = HL.Override() << function(self)
    PhaseGachaWeaponPool.Super._OnDestroy(self)
end



PhaseGachaWeaponPool.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}

    local panelItem = self.m_panel2Item[PanelId.GachaWeaponPool]
    if panelItem then
        arg.rewardQueueRestoreInfo = panelItem.uiCtrl:GetRewardQueueRestoreInfo()
    end
    return arg
end

HL.Commit(PhaseGachaWeaponPool)
