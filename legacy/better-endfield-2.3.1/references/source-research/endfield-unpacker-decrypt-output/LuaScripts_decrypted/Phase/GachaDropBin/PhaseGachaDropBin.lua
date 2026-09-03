local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.GachaDropBin
PhaseGachaDropBin = HL.Class('PhaseGachaDropBin', phaseBase.PhaseBase)





PhaseGachaDropBin.s_messages = HL.StaticField(HL.Table) << {

}



PhaseGachaDropBin.m_outsideObjItem = HL.Field(HL.Forward('PhaseGameObjectItem'))

PhaseGachaDropBin.m_outsideDirector = HL.Field(CS.UnityEngine.Playables.PlayableDirector)

PhaseGachaDropBin.m_cutsceneData = HL.Field(HL.Any)



PhaseGachaDropBin._OnInit = HL.Override() << function(self)
    PhaseGachaDropBin.Super._OnInit(self)
end





PhaseGachaDropBin.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseGachaDropBin._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    GameInstance.player.gacha.curPlayingDropBinCount = #self.arg.chars
    local maxRarity = 0
    for _, v in ipairs(self.arg.chars) do
        if v.rarity > maxRarity then
            maxRarity = v.rarity
        end
    end
    GameInstance.player.gacha.curPlayingTimelineMaxRarity = maxRarity
    logger.info("PhaseGachaDropBin", maxRarity)

    self.m_outsideObjItem = self:CreatePhaseGOItem("GachaOutside", nil, nil, "Gacha")
    self.m_outsideDirector = self.m_outsideObjItem.view.director
    self.m_outsideObjItem.go:SetLayerRecursive(UIConst.GACHA_LAYER)
    self.m_outsideObjItem.go.transform.position = Vector3(0, 0, 0)
    local is1To1Ratio = Screen.width / Screen.height < 4/3
    self.m_outsideObjItem.view.externalCamera.gameObject:SetActive(not is1To1Ratio)
    self.m_outsideObjItem.view.externalCamera1To1Ratio.gameObject:SetActive(is1To1Ratio)


    local _
    _, self.m_cutsceneData = GameWorld.cutsceneManager:TryGetCutsceneData("gacha_main_ten")
    if self.m_cutsceneData then
        AudioAdapter.LoadAndPinEventsSync(self.m_cutsceneData.audioEvents)
    else
        logger.error("No Cutscene Data gacha_main_ten")
    end


    local panel = self:CreatePhasePanelItem(PanelId.GachaDropBin, self.arg)
    panel.uiCtrl:Start()

    LuaSystemManager.gachaSystem:UpdateGachaSettingState()
end

PhaseGachaDropBin._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.m_cutsceneData then
        for _, v in pairs(self.m_cutsceneData.audioEvents) do
            AudioAdapter.UnpinEvent(v)
        end
        self.m_cutsceneData = nil
    end
    GameInstance.player.gacha.curPlayingDropBinCount = 0
    GameInstance.player.gacha.curPlayingTimelineMaxRarity = 0
end

PhaseGachaDropBin._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGachaDropBin._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseGachaDropBin._OnActivated = HL.Override() << function(self)
    LuaSystemManager.gachaSystem:UpdateGachaSettingState()
end

PhaseGachaDropBin._OnDeActivated = HL.Override() << function(self)
    LuaSystemManager.gachaSystem:UpdateGachaSettingState()
end

PhaseGachaDropBin._OnDestroy = HL.Override() << function(self)
    PhaseGachaDropBin.Super._OnDestroy(self)
end



HL.Commit(PhaseGachaDropBin)
