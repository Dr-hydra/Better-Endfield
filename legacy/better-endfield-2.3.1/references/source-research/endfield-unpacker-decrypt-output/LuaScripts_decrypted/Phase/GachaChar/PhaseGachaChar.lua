
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.GachaChar
PhaseGachaChar = HL.Class('PhaseGachaChar', phaseBase.PhaseBase)





PhaseGachaChar.s_messages = HL.StaticField(HL.Table) << {

}


PhaseGachaChar.m_roomObjItem = HL.Field(HL.Forward('PhaseGameObjectItem'))

PhaseGachaChar.m_oldCamCulling = HL.Field(HL.Any)




PhaseGachaChar._OnInit = HL.Override() << function(self)
    PhaseGachaChar.Super._OnInit(self)
end






PhaseGachaChar.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    if transitionType == PhaseConst.EPhaseState.TransitionIn and not fastMode then
        UIManager:PreloadPanelAsset(PanelId.GachaChar, PHASE_ID)
    end
end

PhaseGachaChar._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    LuaSystemManager.gachaSystem:UpdateGachaSettingState()

    self.m_roomObjItem = self:CreatePhaseGOItem("GachaRoom", nil, nil, "Gacha")
    self.m_roomObjItem.go:SetLayerRecursive(UIConst.GACHA_LAYER)
    self.m_roomObjItem.go.transform.position = Vector3(0, 0, 0)
    self.m_roomObjItem.view.charInfo3DUI.gameObject:SetLayerRecursive(UIConst.WORLD_UI_LAYER)
    self.m_roomObjItem.view.sixStarUIBg.gameObject:SetLayerRecursive(UIConst.WORLD_UI_LAYER)

    local panel = self:CreatePhasePanelItem(PanelId.GachaChar, self.arg)
    panel.uiCtrl:_PlayCharacterAt(1)

    LuaSystemManager.gachaSystem:UpdateGachaSettingState()
end

PhaseGachaChar._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    LuaSystemManager.gachaSystem:UpdateGachaSettingState()
end

PhaseGachaChar._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGachaChar._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end







PhaseGachaChar._OnActivated = HL.Override() << function(self)
    if self.m_roomObjItem then
        self.m_roomObjItem.view.gameObject:SetActive(true)
    end
    LuaSystemManager.gachaSystem:UpdateGachaSettingState()
end

PhaseGachaChar._OnDeActivated = HL.Override() << function(self)
    if self.m_roomObjItem then
        self.m_roomObjItem.view.gameObject:SetActive(false)
    end
    LuaSystemManager.gachaSystem:UpdateGachaSettingState()
end

PhaseGachaChar._OnDestroy = HL.Override() << function(self)
    PhaseGachaChar.Super._OnDestroy(self)
end




HL.Commit(PhaseGachaChar)
