
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.GachaLauncher
PhaseGachaLauncher = HL.Class('PhaseGachaLauncher', phaseBase.PhaseBase)





PhaseGachaLauncher.s_messages = HL.StaticField(HL.Table) << {

}


PhaseGachaLauncher.m_launcherObjItem = HL.Field(HL.Forward('PhaseGameObjectItem'))

PhaseGachaLauncher.m_launcherDirector = HL.Field(CS.UnityEngine.Playables.PlayableDirector)

PhaseGachaLauncher.m_cutsceneData = HL.Field(HL.Any)



PhaseGachaLauncher._OnInit = HL.Override() << function(self)
    PhaseGachaLauncher.Super._OnInit(self)
end




PhaseGachaLauncher.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)

end

PhaseGachaLauncher._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    local maxRarity = 0
    for _, v in ipairs(self.arg.chars) do
        if v.rarity > maxRarity then
            maxRarity = v.rarity
        end
    end
    local hasSurprise = GameInstance.player.gacha.hasSuperSurprise
    if maxRarity == 6 and hasSurprise then
        GameInstance.player.gacha.curPlayingTimelineMaxRarity = 5
    else
        GameInstance.player.gacha.curPlayingTimelineMaxRarity = maxRarity
    end

    self.m_launcherObjItem = self:CreatePhaseGOItem("GachaLauncher", nil, nil, "Gacha")
    self.m_launcherDirector = self.m_launcherObjItem.view.director
    self.m_launcherObjItem.go:SetLayerRecursive(UIConst.GACHA_LAYER)
    self.m_launcherObjItem.go.transform.position = Vector3(0, 0, 0)


    local panel = self:CreatePhasePanelItem(PanelId.GachaLauncher, self.arg)
    panel.uiCtrl:Start()

    LuaSystemManager.gachaSystem:UpdateGachaSettingState()
end

PhaseGachaLauncher._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    GameInstance.player.gacha.curPlayingTimelineMaxRarity = 0
end

PhaseGachaLauncher._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGachaLauncher._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseGachaLauncher._OnActivated = HL.Override() << function(self)
    LuaSystemManager.gachaSystem:UpdateGachaSettingState()
end

PhaseGachaLauncher._OnDeActivated = HL.Override() << function(self)
    LuaSystemManager.gachaSystem:UpdateGachaSettingState()
end

PhaseGachaLauncher._OnDestroy = HL.Override() << function(self)
    PhaseGachaLauncher.Super._OnDestroy(self)
end




HL.Commit(PhaseGachaLauncher)

