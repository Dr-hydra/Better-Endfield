
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.GachaWeaponPreheat
PhaseGachaWeaponPreheat = HL.Class('PhaseGachaWeaponPreheat', phaseBase.PhaseBase)





PhaseGachaWeaponPreheat.s_messages = HL.StaticField(HL.Table) << {

}



PhaseGachaWeaponPreheat.m_preheatObjItem = HL.Field(HL.Forward('PhaseGameObjectItem'))

PhaseGachaWeaponPreheat.m_preheatDirector = HL.Field(CS.UnityEngine.Playables.PlayableDirector)

PhaseGachaWeaponPreheat.m_cutsceneData = HL.Field(HL.Any)




PhaseGachaWeaponPreheat._OnInit = HL.Override() << function(self)
    PhaseGachaWeaponPreheat.Super._OnInit(self)
end




PhaseGachaWeaponPreheat.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseGachaWeaponPreheat._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)

    local maxRarity = 0
    for _, v in ipairs(self.arg.weapons) do
        if v.rarity > maxRarity then
            maxRarity = v.rarity
        end
    end
    GameInstance.player.gacha.curPlayingTimelineMaxRarity = maxRarity

    local _
    _, self.m_cutsceneData = GameWorld.cutsceneManager:TryGetCutsceneData("gacha_weapon_main_ten")
    if self.m_cutsceneData then
        AudioAdapter.LoadAndPinEventsSync(self.m_cutsceneData.audioEvents)
    else
        logger.error("No Cutscene Data 【gacha_weapon_main_ten】")
    end

    self.m_preheatObjItem = self:CreatePhaseGOItem("GachaWeaponPreheat", nil, nil, "Gacha")
    self.m_preheatDirector = self.m_preheatObjItem.view.director
    self.m_preheatObjItem.go:SetLayerRecursive(UIConst.GACHA_LAYER)
    self.m_preheatObjItem.go.transform.position = Vector3(0, 0, 0)

    local panel = self:CreatePhasePanelItem(PanelId.GachaWeaponPreheat, self.arg)
    panel.uiCtrl:Start()

    LuaSystemManager.gachaSystem:UpdateGachaWeaponSettingState()
end

PhaseGachaWeaponPreheat._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.m_cutsceneData then
        for _, v in pairs(self.m_cutsceneData.audioEvents) do
            AudioAdapter.UnpinEvent(v)
        end
        self.m_cutsceneData = nil
    end
    GameInstance.player.gacha.curPlayingTimelineMaxRarity = 0
end

PhaseGachaWeaponPreheat._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGachaWeaponPreheat._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseGachaWeaponPreheat._OnActivated = HL.Override() << function(self)
    LuaSystemManager.gachaSystem:UpdateGachaWeaponSettingState()
end

PhaseGachaWeaponPreheat._OnDeActivated = HL.Override() << function(self)
    LuaSystemManager.gachaSystem:UpdateGachaWeaponSettingState()
end

PhaseGachaWeaponPreheat._OnDestroy = HL.Override() << function(self)
    PhaseGachaWeaponPreheat.Super._OnDestroy(self)
end




HL.Commit(PhaseGachaWeaponPreheat)

