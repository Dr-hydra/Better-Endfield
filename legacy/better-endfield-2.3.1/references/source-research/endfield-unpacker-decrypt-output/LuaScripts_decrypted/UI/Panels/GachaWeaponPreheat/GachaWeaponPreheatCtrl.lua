
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaWeaponPreheat

GachaWeaponPreheatCtrl = HL.Class('GachaWeaponPreheatCtrl', uiCtrl.UICtrl)






GachaWeaponPreheatCtrl.s_messages = HL.StaticField(HL.Table) << {

}



GachaWeaponPreheatCtrl.m_sortedRarityList = HL.Field(HL.Table)




GachaWeaponPreheatCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.skipBtn.onClick:AddListener(function()
        self:_OnClickSkip()
    end)
end

GachaWeaponPreheatCtrl.Start = HL.Method() << function(self)
    self:_UpdateData()

    local gachaBoxEffectRoot = self.m_phase.m_preheatObjItem.view.gachaBoxEffectRoot
    local cameraEffectRoot = self.m_phase.m_preheatObjItem.view.cameraEffect
    local count = #self.m_sortedRarityList

    for i = 1, count do
        local rarity = self.m_sortedRarityList[i]
        local boxRoot = gachaBoxEffectRoot:Find(string.format("GachaBox%d/transform", CSIndex(i)))
        boxRoot:Find("Rarity6Effect").gameObject:SetActive(rarity >= 6)
        boxRoot:Find("Rarity5Effect").gameObject:SetActive(rarity == 5)
        boxRoot:Find("Rarity4Effect").gameObject:SetActive(rarity == 4)
    end
    local maxRarity = GameInstance.player.gacha.curPlayingTimelineMaxRarity
    cameraEffectRoot:Find("Rarity6Effect").gameObject:SetActive(maxRarity == 6)
    cameraEffectRoot:Find("Rarity5Effect").gameObject:SetActive(maxRarity == 5)

    self.m_phase.m_preheatObjItem.go:SetActive(true)
    self.m_phase.m_preheatDirector.time = 0
    self.m_phase.m_preheatDirector:Evaluate()
    self.m_phase.m_preheatDirector:Play()

    logger.info("Gacha Weapon Preheat Duration", self.m_phase.m_preheatDirector.duration)
    self:_StartTimer(self.m_phase.m_preheatDirector.duration, function()
        self:_OnClickSkip()
    end)
end

GachaWeaponPreheatCtrl._UpdateData = HL.Method() << function(self)
    self.m_sortedRarityList = {}
    local arg = self.m_phase.arg
    local weapons
    if arg and arg.weapons then
        weapons = self.m_phase.arg.weapons
    else
        weapons = {}
    end
    for _, weapon in ipairs(weapons) do
        table.insert(self.m_sortedRarityList, weapon.rarity)
    end
    table.sort(self.m_sortedRarityList, function(a, b) return a > b end)
    logger.info("sortedRarityList", self.m_sortedRarityList)
end

GachaWeaponPreheatCtrl._OnClickSkip = HL.Method() << function(self)
    local arg = self.m_phase.arg
    if arg and arg.onComplete then
        arg.onComplete()
    end

    PhaseManager:ExitPhaseFast(PhaseId.GachaWeaponPreheat)
end

HL.Commit(GachaWeaponPreheatCtrl)
