local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

GemCard = HL.Class('GemCard', UIWidgetBase)

GemCard.m_starCellCache = HL.Field(HL.Forward("UIListCache"))

GemCard.m_gemInst = HL.Field(HL.Number) << -1

GemCard.m_refreshSkillNode = HL.Field(HL.Function)


GemCard._OnFirstTimeInit = HL.Override() << function(self)
    self.m_starCellCache = UIUtils.genCellCache(self.view.starCell)
    self.view.naviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
        self.view.controllerFocusHintNode.gameObject:SetActive(not isTopLayer)
    end)
    self:RegisterMessage(MessageConst.ON_GEM_ENHANCE, function(args)

        local msg = unpack(args)
        if msg.IsSuccess and msg.GemInstId == self.m_gemInst then
            self.m_refreshSkillNode()
        end
    end)
end

GemCard.InitGemCard = HL.Method(HL.Number, HL.Opt(HL.Number)) << function(self, gemInstId, tryWeaponInstId)
    self:_FirstTimeInit()

    if not gemInstId or gemInstId <= 0 then
        if self.view.gemNode then
            self.view.gemNode.gameObject:SetActive(false)
        end
        return
    end
    self.m_gemInst = gemInstId
    self:_RefreshBasicInfo(gemInstId)
    self.m_refreshSkillNode = function()
        self.view.gemSkillNode:InitGemSkillNode(gemInstId, { weaponInstId = tryWeaponInstId })
    end
    self.m_refreshSkillNode()

    local isPerfectMatch = UIUtils.getGemWishListPerfectMatch(gemInstId)
    if self.view.gemNode then
        self.view.gemNode.gameObject:SetActive(isPerfectMatch)
    end
    if self.view.leftDeco01 then
        self.view.leftDeco01.gameObject:SetActive(not isPerfectMatch)
    end
end

GemCard._RefreshBasicInfo = HL.Method(HL.Number) << function(self, gemInstId)
    local gemInst = CharInfoUtils.getGemByInstId(gemInstId)
    local gemItemCfg = Tables.itemTable:GetValue(gemInst.templateId)

    self.view.gemName.text = UIUtils.getItemName(gemInst.templateId, gemInst.instId)
    self.view.gemItemIcon:InitItemIcon(gemInst.templateId, true, gemInst.instId)
    self.view.lockToggle:InitLockToggle(gemInst.templateId, gemInst.instId)
    self.view.trashToggle:InitTrashToggle(gemInst.templateId, gemInst.instId)
    self.view.domainNode:InitDomainTagNode(gemInst.domainId)
    UIUtils.setItemRarityImage(self.view.bgColor, gemItemCfg.rarity)
    UIUtils.setItemRarityImage(self.view.titleColor, gemItemCfg.rarity)
    self.m_starCellCache:Refresh(gemItemCfg.rarity)
end

GemCard.ActiveToggleGroup = HL.Method(HL.Boolean) << function(self, isActive)
    self.view.naviGroup.gameObject:SetActive(isActive)
end

HL.Commit(GemCard)
return GemCard
