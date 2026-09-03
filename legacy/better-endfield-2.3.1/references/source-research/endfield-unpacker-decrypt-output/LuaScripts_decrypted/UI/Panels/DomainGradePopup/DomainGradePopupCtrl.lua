
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainGradePopup

local SHOW_REWARD_ITEM_COUNT = 5

DomainGradePopupCtrl = HL.Class('DomainGradePopupCtrl', uiCtrl.UICtrl)

DomainGradePopupCtrl.m_domainId = HL.Field(HL.String) << ""

DomainGradePopupCtrl.m_lv = HL.Field(HL.Number) << -1

DomainGradePopupCtrl.m_genLevelEffectCellFunc = HL.Field(HL.Function)

DomainGradePopupCtrl.m_rewardItemCellCache = HL.Field(HL.Forward("UIListCache"))

DomainGradePopupCtrl.m_levelEffectInfo = HL.Field(HL.Table)

DomainGradePopupCtrl.m_isGet = HL.Field(HL.Boolean) << false

DomainGradePopupCtrl.m_isReachedLevel = HL.Field(HL.Boolean) << false





DomainGradePopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_DOMAIN_DEVELOPMENT_LEVEL_REWARD_GET] = 'OnDomainDevelopmentLevelRewardGet',
}


DomainGradePopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_domainId = arg.domainId
    self.m_lv = arg.lv

    self.m_genLevelEffectCellFunc = UIUtils.genCachedCellFunction(self.view.levelEffectScrollList)
    self.m_rewardItemCellCache = UIUtils.genCellCache(self.view.titleNode.rewardItemCell)

    self.view.levelEffectScrollList.onUpdateCell:AddListener(function(gameObject, csIndex)
        self:_UpdateLevelEffectCell(gameObject, csIndex)
    end)

    self.view.closeBtn.onClick:AddListener(function()
        self:_OnClickCloseBtn()
    end)

    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        self:_OnClickCloseBtn()
    end)

    self.view.titleNode.receiveBtn.onClick:AddListener(function()
        self:_OnClickReceiveBtn()
    end)

    self:_UpdateState()

    self:_UpdateTitleInfo()
    self:_UpdateLevelRewardsInfo()
    self:_UpdateLevelEffectInfo()
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

DomainGradePopupCtrl._UpdateState = HL.Method() << function(self)
    local domainDevelopmentSys = GameInstance.player.domainDevelopmentSystem
    local domainData = domainDevelopmentSys.domainDevDataDic:get_Item(self.m_domainId)

    self.m_isReachedLevel = self.m_lv <= domainData.lv
    self.m_isGet = domainDevelopmentSys:IsLevelRewarded(self.m_domainId, self.m_lv)
end

DomainGradePopupCtrl._UpdateTitleInfo = HL.Method() << function(self)
    DomainDevelopmentUtils.updateLevelCellTitle(self.view.titleNode, self.m_domainId, self.m_lv)
end

DomainGradePopupCtrl._UpdateLevelRewardsInfo = HL.Method() << function(self)
    local domainCfg = Tables.domainDataTable[self.m_domainId]
    local domainDevelopmentLevelCfg = domainCfg.domainDevelopmentLevel[self.m_lv - 1]
    local rewardId = domainDevelopmentLevelCfg.rewardId
    local rewards = string.isEmpty(rewardId) and {} or UIUtils.getRewardItems(rewardId)
    self.m_rewardItemCellCache:Refresh(SHOW_REWARD_ITEM_COUNT, function(cell, luaIndex)
        local rewardInfo = rewards[luaIndex]
        DomainDevelopmentUtils.updateLevelRewardCell(cell, rewardInfo, self.m_isGet)
    end)
end

DomainGradePopupCtrl._UpdateLevelEffectInfo = HL.Method() << function(self)
    self.m_levelEffectInfo = DomainDevelopmentUtils.genLevelEffectInfo(self.m_domainId, self.m_lv)
    self.view.levelEffectScrollList:UpdateCount(#self.m_levelEffectInfo)
end

DomainGradePopupCtrl._UpdateLevelEffectCell = HL.Method(GameObject, HL.Number) << function(self, gameObject, csIndex)
    local cell = self.m_genLevelEffectCellFunc(gameObject)
    local levelEffectInfoUnit = self.m_levelEffectInfo[LuaIndex(csIndex)]
    DomainDevelopmentUtils.updateLevelEffectCell(cell, levelEffectInfoUnit, self.m_isReachedLevel)
end

DomainGradePopupCtrl._OnClickCloseBtn = HL.Method() << function(self)
    self:PlayAnimationOutAndClose()
end

DomainGradePopupCtrl._OnClickReceiveBtn = HL.Method() << function(self)
    GameInstance.player.domainDevelopmentSystem:TakeLevelReward(self.m_domainId, self.m_lv)
end

DomainGradePopupCtrl.OnDomainDevelopmentLevelRewardGet = HL.Method(HL.Any) << function(self, args)
    self:_UpdateState()

    self:_UpdateTitleInfo()
    self:_UpdateLevelRewardsInfo()
    self:_UpdateLevelEffectInfo()
end

HL.Commit(DomainGradePopupCtrl)
