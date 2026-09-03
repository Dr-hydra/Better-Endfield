local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharInfoBasicNode = HL.Class('CharInfoBasicNode', UIWidgetBase)

local ActionOnSetNaviTarget = CS.Beyond.Input.ActionOnSetNaviTarget

do
    CharInfoBasicNode.m_starCellCache = HL.Field(HL.Forward("UIListCache"))

    CharInfoBasicNode.m_fcAttrCellCache = HL.Field(HL.Forward("UIListCache"))

    CharInfoBasicNode.m_scAttrCellCache = HL.Field(HL.Forward("UIListCache"))

    CharInfoBasicNode.m_imageSelectCache = HL.Field(HL.Table)

    CharInfoBasicNode.m_charInstId = HL.Field(HL.Number) << -1

    CharInfoBasicNode.m_fcHintDataList = HL.Field(HL.Table)

    CharInfoBasicNode.m_scHintDataList = HL.Field(HL.Table)

    CharInfoBasicNode.m_battleTagCellCache = HL.Field(HL.Forward("UIListCache"))
end


CharInfoBasicNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_starCellCache = UIUtils.genCellCache(self.view.starCell)
    self.m_fcAttrCellCache = UIUtils.genCellCache(self.view.firstClassAttributeCell)
    self.m_scAttrCellCache = UIUtils.genCellCache(self.view.secondClassAttributeCell)
    self.m_battleTagCellCache = UIUtils.genCellCache(self.view.tagCell)
    self.m_imageSelectCache = {}

    self:RegisterMessage(MessageConst.CHAR_INFO_SHOW_FC_ATTR_HINT, function(args)
        self:_RefreshImageSelect(args.cell)
    end)
    self:RegisterMessage(MessageConst.CHAR_INFO_SHOW_SC_ATTR_HINT, function(args)
        self:_RefreshImageSelect(args.cell)
    end)
    self:RegisterMessage(MessageConst.SHOW_CHAR_SKILL_TIP, function(args)
        self:_RefreshImageSelect(args.cell)
    end)
    self:RegisterMessage(MessageConst.ON_CHAR_INFO_CLOSE_ATTR_TIP, function(args)
        self:_RefreshImageSelect()
        self:_ActiveControllerAutoClick(false)
    end)
    self:RegisterMessage(MessageConst.ON_CHAR_INFO_CLOSE_SKILL_TIP, function(args)
        self:_RefreshImageSelect()
        self:_ActiveControllerAutoClick(false)
    end)
    self:RegisterMessage(MessageConst.CHAR_INFO_SHOW_FULL_ATTRIBUTE, function()
        self:_OnShowCurrentCharFullAttribute()
    end)
    self:RegisterMessage(MessageConst.ON_CHAR_INFO_SHOW_ATTR_TIP, function()
        self:_ActiveControllerAutoClick(true)
    end)
    self:RegisterMessage(MessageConst.ON_CHAR_INFO_SHOW_SKILL_TIP, function()
        self:_ActiveControllerAutoClick(true)
    end)

    self:_InitCharInfoBasicNodeController()
end

CharInfoBasicNode.InitCharInfoBasicNode = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, charInstId, hideWhenOpenDetail)
    self:_FirstTimeInit()

    if charInstId == nil then
        return
    end
    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    self.m_charInstId = charInstId
    local templateId = charInfo.templateId
    local charCfg = Tables.characterTable[templateId]

    self.view.charNameText:SetPhoneticText(GEnums.PhoneticType.CharNamePhonetic, templateId)
    self.view.professionIcon:LoadSprite(UIConst.UI_SPRITE_CHAR_PROFESSION, CharInfoUtils.getCharProfessionIconName(charCfg.profession))
    self.view.charElementIcon:InitCharTypeIcon(charCfg.charTypeId)

    self.m_starCellCache:Refresh(UIConst.CHAR_MAX_RARITY, function(cell, index)

        cell.gameObject:SetActive(index <= charCfg.rarity)
    end)

    local fcAttrShowList, scAttrShowList = CharInfoUtils.generateCharInfoBasicAttrShowInfo(charInstId)
    self.m_fcHintDataList = {}
    self.m_fcAttrCellCache:Refresh(#fcAttrShowList, function(cell, index)
        local showInfo = fcAttrShowList[index]

        local isMainAttrType = showInfo.attributeType == charCfg.mainAttrType
        local isSubAttrType = showInfo.attributeType == charCfg.subAttrType

        cell.bgImage.gameObject:SetActive(isMainAttrType or isSubAttrType)
        if isMainAttrType or isSubAttrType then
            cell.bgImage.color = isMainAttrType and self.view.config.ATTR_BG_COLOR_MAIN or self.view.config.ATTR_BG_COLOR_SUB
        end

        cell.valueText.text = showInfo.showValue
        cell.nameText.text = showInfo.showName
        cell.icon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, UIConst.UI_ATTRIBUTE_ICON_PREFIX .. showInfo.attributeKey)
        cell.icon.color = (isMainAttrType or isSubAttrType) and self.view.config.ATTR_ICON_COLOR_MAIN or self.view.config.ATTR_ICON_COLOR_OTHER

        cell.iconShadow:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, UIConst.UI_ATTRIBUTE_ICON_PREFIX .. showInfo.attributeKey)
        cell.iconShadow.color = (isMainAttrType or isSubAttrType) and self.view.config.ATTR_SHADOW_COLOR_MAIN or self.view.config.ATTR_SHADOW_COLOR_SUB

        local hintData = {
            key = string.format("fc_%d", index),
            transform = cell.transform,
            attributeInfo = showInfo,
            charTemplateId = templateId,
            charInstId = charInstId,
            tipsPos = self.view.hintTransform,


            cell = cell,
            enableCloseActionOnController = true,
        }
        self.m_fcHintDataList[index] = hintData

        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            Notify(MessageConst.CHAR_INFO_SHOW_FC_ATTR_HINT, hintData)
        end)
        cell.line.gameObject:SetActive(index ~= #fcAttrShowList)
    end)

    self.m_scHintDataList = {}
    self.m_scAttrCellCache:Refresh(#scAttrShowList, function(cell, index)
        local showInfo = scAttrShowList[index]

        cell.valueText.text = showInfo.showValue
        cell.icon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, UIConst.UI_ATTRIBUTE_ICON_PREFIX .. showInfo.attributeKey)

        local hintData = {
            key = string.format("sc_%d", index),
            transform = cell.transform,
            attributeInfo = showInfo,
            charInfo = charInfo,
            tipsPos = self.view.hintTransform,


            cell = cell,
            enableCloseActionOnController = true,
        }
        self.m_scHintDataList[index] = hintData

        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            Notify(MessageConst.CHAR_INFO_SHOW_SC_ATTR_HINT, hintData)
        end)
    end)

    self.view.charSkillNodeNew:InitCharSkillNodeNew({
        charInstId = charInstId,
        isSingleChar = false,
        hideBtnUpgrade = false,
        tipsNode = self.view.hintTransform,
        enableCloseActionOnController = true,
    })
    self.view.charPassiveSkillNode:InitCharPassiveSkillNode({
        charInstId = charInstId,
        isSingleChar = false,
        hideBtnUpgrade = false,
        tipsNode = self.view.hintTransform,
        enableCloseActionOnController = true,
    })

    if self.view.professionIconButton then
        self.view.professionIconButton.onClick:RemoveAllListeners()
        self.view.professionIconButton.onClick:AddListener(function()
            UIManager:Open(PanelId.CharInfoProAndElement)
        end)
    end

    self.view.attributeTitle.detailButton.onClick:RemoveAllListeners()
    self.view.attributeTitle.detailButton.onClick:AddListener(function()
        if hideWhenOpenDetail then
            UIUtils.PlayAnimationAndToggleActive(self.view.animationWrapper, false, function()
                self:_OpenCharInfoFullAttribute(charInstId, templateId, function()
                    UIUtils.PlayAnimationAndToggleActive(self.view.animationWrapper, true)
                end)
            end)
        else
            self:_OpenCharInfoFullAttribute(charInstId, templateId)
        end
    end)
    self.view.charLevelNode:InitCharLevelNode(charInstId)




    self.m_imageSelectCache = {}
    local fcItemCells = self.m_fcAttrCellCache:GetItems()
    local scItemCells = self.m_scAttrCellCache:GetItems()
    local skillCells = self.view.charSkillNodeNew.m_skillCells:GetItems()
    local passiveSkillCells = self.view.charPassiveSkillNode.m_passiveSkillCellCache:GetItems()

    for i, cell in pairs(fcItemCells) do
        self.m_imageSelectCache[cell] = cell.imageSelect
    end
    for i, cell in pairs(scItemCells) do
        self.m_imageSelectCache[cell] = cell.imageSelect
    end
    for i, cell in pairs(skillCells) do
        self.m_imageSelectCache[cell] = cell.view.imageSelect
    end
    for i, cell in pairs(passiveSkillCells) do
        self.m_imageSelectCache[cell] = cell.imageSelect
    end


    local tagCount = #charCfg.charBattleTagIds
    self.m_battleTagCellCache:Refresh(tagCount, function(cell, index)
        local _, tagName = Tables.charBattleTagTable:TryGetValue(charCfg.charBattleTagIds[CSIndex(index)])
        if tagName then
            cell.tagTxt.text = tagName
        end
    end)
    self.view.tagDeco.gameObject:SetActive(tagCount % 2 == 0)
end

CharInfoBasicNode._RefreshImageSelect = HL.Method(HL.Opt(HL.Any)) << function(self, showCell)
    for parentCell, cell in pairs(self.m_imageSelectCache) do
        cell.gameObject:SetActive(parentCell == showCell and not DeviceInfo.usingController)
    end
end

CharInfoBasicNode._OnShowCurrentCharFullAttribute = HL.Method() << function(self)
    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(self.m_charInstId)
    if charInfo == nil then
        return
    end
    self:_OpenCharInfoFullAttribute(self.m_charInstId, charInfo.templateId)
end

CharInfoBasicNode._OpenCharInfoFullAttribute = HL.Method(HL.Number, HL.String, HL.Opt(HL.Function)) << function(self, instId, templateId, onClose)
    UIManager:Open(PanelId.CharInfoFullAttribute,{
        charInfo = {
            instId = instId,
            templateId = templateId,
        },
        onClose = onClose,
    })
end




CharInfoBasicNode._InitCharInfoBasicNodeController = HL.Method() << function(self)
    local function closeTips()
        Notify(MessageConst.CHAR_INFO_CLOSE_ATTR_TIP)
        Notify(MessageConst.CHAR_INFO_CLOSE_SKILL_TIP)
    end
    self.view.mainNaviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
        if not isTopLayer then
            closeTips()
        end
    end)
    self.view.professionIconButton.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            closeTips()
        end
    end
    self.view.attributeTitle.detailButton.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            closeTips()
        end
    end
end

CharInfoBasicNode._ActiveControllerAutoClick = HL.Method(HL.Boolean) << function(self, isActive)
    local actionOnSetNaviTarget = isActive and ActionOnSetNaviTarget.AutoTriggerOnClick or ActionOnSetNaviTarget.PressConfirmTriggerOnClick
    for i = 1, self.m_fcAttrCellCache:GetCount() do
        local cell = self.m_fcAttrCellCache:GetItem(i)
        if cell.button then
            cell.button:ChangeActionOnSetNaviTarget(actionOnSetNaviTarget)
        end
    end
    for i = 1, self.m_scAttrCellCache:GetCount() do
        local cell = self.m_scAttrCellCache:GetItem(i)
        if cell.button then
            cell.button:ChangeActionOnSetNaviTarget(actionOnSetNaviTarget)
        end
    end
    local skillCellCache = self.view.charSkillNodeNew.m_skillCells
    for i = 1, skillCellCache:GetCount() do
        local cell = skillCellCache:GetItem(i)
        if cell.view.button then
            cell.view.button:ChangeActionOnSetNaviTarget(actionOnSetNaviTarget)
        end
    end

    local talentCellCache = self.view.charPassiveSkillNode.m_passiveSkillCellCache
    for i = 1, talentCellCache:GetCount() do
        local cell = talentCellCache:GetItem(i)
        if cell.button then
            cell.button:ChangeActionOnSetNaviTarget(actionOnSetNaviTarget)
        end
    end
end




HL.Commit(CharInfoBasicNode)
return CharInfoBasicNode
