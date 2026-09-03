local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaDropBin

GachaDropBinCtrl = HL.Class('GachaDropBinCtrl', uiCtrl.UICtrl)






GachaDropBinCtrl.s_messages = HL.StaticField(HL.Table) << {

}

GachaDropBinCtrl.m_sortedRarityList = HL.Field(HL.Table)


GachaDropBinCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.skipBtn.onClick:AddListener(function()
        self:_OnClickSkip()
    end)
    local canJump = not GameInstance.player.gacha.hasSuperSurprise or self.view.config.CAN_JUMP_WHEN_SURPRISE
    self.view.skipBtn.gameObject:SetActive(canJump)
end

local DropBinIndexList = { 0, 9, 7, 8, 6, 5, 4, 3, 2, 1 }
local RarityEffectName = {
    [4] = "RarityEffect4",
    [5] = "RarityEffect5",
    [6] = "RarityEffect6",
}

GachaDropBinCtrl.Start = HL.Method() << function(self)
    self.m_sortedRarityList = {}
    local chars = self.m_phase.arg.chars
    for _, v in ipairs(chars) do
        table.insert(self.m_sortedRarityList, v.rarity)
    end
    table.sort(self.m_sortedRarityList, function(a, b)
        return a > b
    end)
    logger.info("sortedRarityList", self.m_sortedRarityList)

    local timelineRoot = self.m_phase.m_outsideObjItem.view.timelineRoot.transform


    local count = #self.m_sortedRarityList
    for k = 1, 10 do
        local dropBin = timelineRoot:Find("Actor/DropBin" .. CSIndex(k))
        local active = k <= count
        dropBin.gameObject:SetActive(active)
    end


    local effectOneRoot = self.m_phase.m_outsideObjItem.view.one

    local effectTenRoot = self.m_phase.m_outsideObjItem.view.ten
    if count == 1 then

        effectOneRoot.gameObject:SetActive(true)
        effectTenRoot.gameObject:SetActive(false)
        local rarity = self.m_sortedRarityList[1]
        GachaDropBinCtrl._SetRarity(effectOneRoot, rarity)
    else

        effectOneRoot.gameObject:SetActive(false)
        effectTenRoot.gameObject:SetActive(true)
        for listIndex, dropBinIndex in ipairs(DropBinIndexList) do
            local rarity = self.m_sortedRarityList[listIndex]
            local dropBinRoot = effectTenRoot:Find("DropBin" .. dropBinIndex)
            GachaDropBinCtrl._SetRarity(dropBinRoot, rarity)
        end
    end

    self.m_phase.m_outsideObjItem.go:SetActive(true)

    local dir = self.m_phase.m_outsideDirector
    dir:Stop()
    dir.time = 0
    dir:Evaluate()
    local duration = self.m_phase.m_outsideDirector.duration
    self.view.fullScreenBlackMask.gameObject:SetActive(true)
    logger.info("Gacha Drop Bin Duration", duration)
    self:_StartCoroutine(function()
        coroutine.step()
        dir:Play()
        self.view.fullScreenBlackMask.gameObject:SetActive(false)
        while true do
            coroutine.step()
            if self.m_phase.m_outsideDirector.time >= duration then
                self:_OnClickSkip()
            end
        end
    end)
end

GachaDropBinCtrl._OnClickSkip = HL.Method() << function(self)
    local onComplete = self.m_phase.arg.onComplete
    onComplete()
    PhaseManager:ExitPhaseFast(PhaseId.GachaDropBin)
end

GachaDropBinCtrl._SetRarity = HL.StaticMethod(Transform, HL.Number) << function(effectRoot, rarity)
    local cut3RarityName = RarityEffectName[rarity]
    local cut2RarityName = cut3RarityName


    local cut = effectRoot:Find("Cam02")
    for i = 0, cut.childCount - 1 do
        local effect = cut:GetChild(i)
        effect.gameObject:SetActive(effect.name == cut2RarityName)
    end

    cut = effectRoot:Find("Cam03")
    for i = 0, cut.childCount - 1 do
        local effect = cut:GetChild(i)
        effect.gameObject:SetActive(effect.name == cut3RarityName)
    end
end

HL.Commit(GachaDropBinCtrl)
