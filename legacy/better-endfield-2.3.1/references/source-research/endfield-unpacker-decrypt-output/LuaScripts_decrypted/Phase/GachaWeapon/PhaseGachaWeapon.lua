
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.GachaWeapon
PhaseGachaWeapon = HL.Class('PhaseGachaWeapon', phaseBase.PhaseBase)





PhaseGachaWeapon.s_messages = HL.StaticField(HL.Table) << {

}

PhaseGachaWeapon.m_displayObjItem = HL.Field(HL.Forward('PhaseGameObjectItem'))


PhaseGachaWeapon._OnInit = HL.Override() << function(self)
    PhaseGachaWeapon.Super._OnInit(self)
end




PhaseGachaWeapon.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    if transitionType == PhaseConst.EPhaseState.TransitionIn and not fastMode then
        UIManager:PreloadPanelAsset(PanelId.GachaWeapon, PHASE_ID)
    end
end

PhaseGachaWeapon._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    LuaSystemManager.gachaSystem:UpdateGachaWeaponSettingState()

    self.m_displayObjItem = self:CreatePhaseGOItem("GachaWeaponDisplay", nil, nil, "Gacha")
    self.m_displayObjItem.go:SetLayerRecursive(UIConst.GACHA_LAYER)
    self.m_displayObjItem.go.transform.position = Vector3(0, 100, 0)
    self.m_displayObjItem.view.charInfo3DUI.gameObject:SetLayerRecursive(UIConst.WORLD_UI_LAYER)

    if UNITY_EDITOR then

        local additionLightGroup = self.m_displayObjItem.view.gachaWeaponLight.gameObject:AddComponent(typeof(CS.Beyond.DevTools.AdditionalLightGroup))
        additionLightGroup.savePath = "Assets/Beyond/DynamicAssets/Gameplay/Prefabs/Gacha/"
    end

    local panel = self:CreatePhasePanelItem(PanelId.GachaWeapon, self.arg)
    panel.uiCtrl:_PlayWeaponAt(1)

    LuaSystemManager.gachaSystem:UpdateGachaWeaponSettingState()
end

PhaseGachaWeapon._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    LuaSystemManager.gachaSystem:UpdateGachaWeaponSettingState()
end

PhaseGachaWeapon._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGachaWeapon._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseGachaWeapon._OnActivated = HL.Override() << function(self)
    if self.m_displayObjItem then
        self.m_displayObjItem.view.gameObject:SetActive(true)
    end
    LuaSystemManager.gachaSystem:UpdateGachaWeaponSettingState()
end

PhaseGachaWeapon._OnDeActivated = HL.Override() << function(self)
    if self.m_displayObjItem then
        self.m_displayObjItem.view.gameObject:SetActive(false)
    end
    LuaSystemManager.gachaSystem:UpdateGachaWeaponSettingState()
end

PhaseGachaWeapon._OnDestroy = HL.Override() << function(self)
    PhaseGachaWeapon.Super._OnDestroy(self)
end




HL.Commit(PhaseGachaWeapon)
