local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local SHOW_REWARD_ITEM_COUNT = 5
local LEVEL_EFFECT_MAX_COUNT = 3

DomainGradeListCell = HL.Class('DomainGradeListCell', UIWidgetBase)

DomainGradeListCell.m_domainId = HL.Field(HL.String) << ""

DomainGradeListCell.m_lv = HL.Field(HL.Number) << -1

DomainGradeListCell.m_levelEffectCellCache = HL.Field(HL.Forward("UIListCache"))

DomainGradeListCell.m_rewardItemCellCache = HL.Field(HL.Forward("UIListCache"))


DomainGradeListCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_levelEffectCellCache = UIUtils.genCellCache(self.view.levelEffectCell)
    self.m_rewardItemCellCache = UIUtils.genCellCache(self.view.titleNode.rewardItemCell)

    self.view.titleNode.receiveBtn.onClick:AddListener(function()
        self:_OnClickReceiveBtn()
    end)

    self.view.detailBtn.onClick:AddListener(function()
        self:_OnClickDetailBtn()
    end)


    local redDot = self.view.titleNode.redDot
    local redDotRoot = redDot.content.gameObject
    local redDotNormal = redDot.normal.gameObject
    local redDotNew = redDot.new.gameObject
    self:GetUICtrl().view.redDotScrollRect:RegisterRedDot(redDotRoot, redDotNormal, redDotNew)
end

DomainGradeListCell.InitDomainGradeListCell = HL.Method(HL.String, HL.Table)
        << function(self, domainId, domainDevelopmentLevelData)
    self:_FirstTimeInit()

    self.m_domainId = domainId
    self.m_lv = domainDevelopmentLevelData.lv

    local isUnopenedLevel = domainDevelopmentLevelData.isUnopenedLevel
    self.view.unopenedNode.gameObject:SetActive(isUnopenedLevel)
    self.view.openNode.gameObject:SetActive(not isUnopenedLevel)

    if not isUnopenedLevel then
        self:_UpdateLevelDetail()
    end
end

DomainGradeListCell._UpdateLevelDetail = HL.Method() << function(self)
    local domainDevSys = GameInstance.player.domainDevelopmentSystem

    local domainData = domainDevSys.domainDevDataDic:get_Item(self.m_domainId)
    local isCurLv = self.m_lv == domainData.lv
    local reachedLevel = self.m_lv <= domainData.lv
    local isGet = domainDevSys:IsLevelRewarded(self.m_domainId, self.m_lv)


    DomainDevelopmentUtils.updateLevelCellTitle(self.view.titleNode, self.m_domainId, self.m_lv)

    local domainCfg = domainData.domainDataCfg
    local domainDevelopmentLevelCfg = domainCfg.domainDevelopmentLevel[self.m_lv - 1]
    local rewardId = domainDevelopmentLevelCfg.rewardId
    local rewards = string.isEmpty(rewardId) and {} or UIUtils.getRewardItems(rewardId)
    self.view.titleNode.rewardStateCtrl:SetState(#rewards > 0 and "HasReward" or "Empty")
    self.m_rewardItemCellCache:Refresh(SHOW_REWARD_ITEM_COUNT, function(cell, luaIndex)
        local rewardInfo = rewards[luaIndex]
        DomainDevelopmentUtils.updateLevelRewardCell(cell, rewardInfo, isGet)
    end)


    local levelEffectInfo = DomainDevelopmentUtils.genLevelEffectInfo(self.m_domainId, self.m_lv)
    local detailsCount = #levelEffectInfo
    self.m_levelEffectCellCache:Refresh(math.min(detailsCount, LEVEL_EFFECT_MAX_COUNT), function(cell, luaIndex)
        local levelEffectInfoUnit = levelEffectInfo[luaIndex]
        DomainDevelopmentUtils.updateLevelEffectCell(cell, levelEffectInfoUnit, reachedLevel)
    end)
    self.view.detailNode.gameObject:SetActive(detailsCount > LEVEL_EFFECT_MAX_COUNT)
end

DomainGradeListCell._OnClickReceiveBtn = HL.Method() << function(self)
    GameInstance.player.domainDevelopmentSystem:TakeLevelReward(self.m_domainId, self.m_lv)
end

DomainGradeListCell._OnClickDetailBtn = HL.Method() << function(self)
    UIManager:Open(PanelId.DomainGradePopup, { domainId = self.m_domainId, lv = self.m_lv })
end


HL.Commit(DomainGradeListCell)
return DomainGradeListCell