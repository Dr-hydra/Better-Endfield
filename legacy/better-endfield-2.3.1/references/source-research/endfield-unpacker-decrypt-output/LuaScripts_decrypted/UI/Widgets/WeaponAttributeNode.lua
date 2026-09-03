local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WeaponAttributeNode = HL.Class('WeaponAttributeNode', UIWidgetBase)

WeaponAttributeNode.m_mainAttributeCellCache = HL.Field(HL.Forward("UIListCache"))

WeaponAttributeNode.m_subAttributeCellCache = HL.Field(HL.Forward("UIListCache"))

WeaponAttributeNode.m_extraAttributeCellCache = HL.Field(HL.Forward("UIListCache"))

WeaponAttributeNode.m_effectCor = HL.Field(HL.Thread)

WeaponAttributeNode.m_normalSubAttrTextColor = HL.Field(Color)

WeaponAttributeNode.m_normalSubAttrValueColor = HL.Field(Color)

WeaponAttributeNode.m_normalExtraAttrTextColor = HL.Field(Color)

WeaponAttributeNode.m_normalExtraAttrValueColor = HL.Field(Color)



WeaponAttributeNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_mainAttributeCellCache = UIUtils.genCellCache(self.view.mainAttributeCell)
    self.m_subAttributeCellCache = UIUtils.genCellCache(self.view.subAttributeCell)
    self.m_extraAttributeCellCache = UIUtils.genCellCache(self.view.extraAttributeCell)

    self.m_normalSubAttrTextColor = self.view.subAttributeCell.attributeText.color
    self.m_normalSubAttrValueColor = self.view.subAttributeCell.fromValue.color
    self.m_normalExtraAttrTextColor = self.view.extraAttributeCell.attributeText.color
    self.m_normalExtraAttrValueColor = self.view.extraAttributeCell.fromValue.color
end

WeaponAttributeNode.InitWeaponAttributeNode = HL.Method(HL.Number, HL.Opt(HL.Number)) << function(self, weaponInstId, gemInstId)
    self:_FirstTimeInit()

    local weaponInst = CharInfoUtils.getWeaponByInstId(weaponInstId)
    if gemInstId == nil then
        gemInstId = weaponInst.attachedGemInstId
    end
    self.m_subAttributeCellCache:Refresh(0, nil)
    self.m_extraAttributeCellCache:Refresh(0, nil)
    local mainAttributeList, subAttributeList = CharInfoUtils.getWeaponShowAttributes(weaponInstId)

    self.m_mainAttributeCellCache:Refresh(#mainAttributeList, function(cell, index)
        local attributeInfo = mainAttributeList[index]
        self:_RefreshAttributeCell(cell, attributeInfo)
    end)
end

WeaponAttributeNode.InitWeaponAttributeNodeByTemplateId = HL.Method(HL.String, HL.Opt(HL.Boolean)) << function(self, weaponTemplateId, isMaxLevel)
    self:_FirstTimeInit()

    self.m_subAttributeCellCache:Refresh(0, nil)
    self.m_extraAttributeCellCache:Refresh(0, nil)

    local mainAttributeList = isMaxLevel and
        CharInfoUtils.getWeaponShowAttributesByTemplateIdWithMaxLevel(weaponTemplateId) or
        CharInfoUtils.getWeaponShowAttributesByTemplateIdWithBasicLevel(weaponTemplateId)

    self.m_mainAttributeCellCache:Refresh(#mainAttributeList, function(cell, index)
        local attributeInfo = mainAttributeList[index]
        self:_RefreshAttributeCell(cell, attributeInfo)
    end)
end

WeaponAttributeNode.InitCharAttributeNode = HL.Method(HL.Number, HL.Number, HL.Number, HL.Opt(HL.Table)) << function(self, charInstId, targetLevel, targetStage, extraArg)
    self:_FirstTimeInit()

    local charInstInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local baseAttributes = CharInfoUtils.getCharPaperAttributes(charInstInfo.templateId, charInstInfo.level, charInstInfo.breakStage)
    local targetAttributes = CharInfoUtils.getCharPaperAttributes(charInstInfo.templateId, targetLevel, targetStage)

    local showBaseAttrs = CharInfoUtils.generateUpgradeAttributeShowInfoList(baseAttributes)
    local showTargetAttrs = CharInfoUtils.generateUpgradeAttributeShowInfoList(targetAttributes)

    self:_ClearCoroutine(self.m_effectCor)

    local forceDiff = true
    if extraArg and extraArg.forceDiff ~= nil then
        forceDiff = extraArg.forceDiff
    end
    if extraArg and extraArg.showAttrTransition == true then
        self.m_effectCor = self:_StartCoroutine(function()
            self.m_mainAttributeCellCache:Refresh(#showTargetAttrs, function(cell, index)
                local baseAttributeInfo = showBaseAttrs[index]
                local attributeInfo = showTargetAttrs[index]
                cell.animationWrapper:PlayInAnimation()
                self:_RefreshUpgradeAttributeCell(cell, baseAttributeInfo, attributeInfo, forceDiff)
                coroutine.wait(self.view.config.ATTR_TRANSITION_DURATION)
            end)
        end)
    else
        self.m_mainAttributeCellCache:Refresh(#showTargetAttrs, function(cell, index)
            local baseAttributeInfo = showBaseAttrs[index]
            local attributeInfo = showTargetAttrs[index]
            self:_RefreshUpgradeAttributeCell(cell, baseAttributeInfo, attributeInfo, forceDiff)
        end)
    end
end

WeaponAttributeNode.InitEquipAttributeFullNode = HL.Method(HL.Number) << function(self, charInstId)
    self:_FirstTimeInit()

    local defAttr, mainAttrShowInfos, extraAttrShowInfos = CharInfoUtils.getCharAllEquipAttributeShowInfos(charInstId)



    self.m_mainAttributeCellCache:Refresh(1, function(cell, index)
        self:_RefreshAttributeCell(cell, defAttr, true)
    end)

    self.m_subAttributeCellCache:Refresh(#mainAttrShowInfos, function(cell, index)
        local attrDiff = mainAttrShowInfos[index]
        cell.gameObject:SetActive(math.abs(attrDiff.attributeValue) > 0.00001)
        self:_RefreshAttributeCell(cell, attrDiff, true)
    end)

    self.m_extraAttributeCellCache:Refresh(#extraAttrShowInfos, function(cell, index)
        local attrDiff = extraAttrShowInfos[index]
        cell.gameObject:SetActive(math.abs(attrDiff.attributeValue) > 0.00001)
        self:_RefreshAttributeCell(cell, attrDiff, true)
    end)
end


WeaponAttributeNode.InitEquipAttributeNode = HL.Method(HL.Number, HL.Opt(HL.String)) << function(self, equipInstId, charTemplateId)
    self:_FirstTimeInit()

    local defShowInfo, mainAttrShowInfoList, extraAttrShowInfoList = CharInfoUtils.getEquipShowAttributes(equipInstId)
    self:_Init(mainAttrShowInfoList, extraAttrShowInfoList, defShowInfo, charTemplateId, equipInstId)
end

WeaponAttributeNode.InitEquipAttributeNodeByTemplateId = HL.Method(HL.String, HL.Opt(HL.String)) << function(self, equipTemplateId, charTemplateId)
    self:_FirstTimeInit()

    local defShowInfo, mainAttrShowInfoList, extraAttrShowInfoList = CharInfoUtils.getEquipTemplateShowAttributes(equipTemplateId)
    self:_Init(mainAttrShowInfoList, extraAttrShowInfoList, defShowInfo, charTemplateId)
end

WeaponAttributeNode._Init = HL.Method(HL.Table, HL.Table, HL.Table, HL.Opt(HL.String, HL.Number)) << function(
    self, fcAttrShowList, extraAttrShowList, defShowInfo, charTemplateId, equipInstId)


    local siblingIndex = 1

    if defShowInfo == nil then
        self.m_mainAttributeCellCache:Refresh(0, nil)
    else
        self.m_mainAttributeCellCache:Refresh(1, function(cell, index)
            cell.transform:SetSiblingIndex(siblingIndex)
            siblingIndex = siblingIndex + 1
            self:_RefreshAttributeCell(cell, defShowInfo, true)
        end)
    end


    self.m_subAttributeCellCache:Refresh(#fcAttrShowList, function(cell, index)
        local attrInfo = fcAttrShowList[index]
        cell.equipEnhanceLevelNode:InitEquipEnhanceLevelNode({
            equipInstId = equipInstId,
            attrIndex = attrInfo.enhancedAttrIndex,
        })

        cell.transform:SetSiblingIndex(siblingIndex)
        siblingIndex = siblingIndex + 1

        self:_RefreshAttributeCell(cell, attrInfo, true, cell.equipEnhanceLevelNode.enabled)
        cell.attributeText.color = cell.equipEnhanceLevelNode.isEnhanced and
            self.view.config.ATTR_ENHANCED_COLOR or self.m_normalSubAttrTextColor
        cell.fromValue.color = cell.equipEnhanceLevelNode.isEnhanced and
            self.view.config.ATTR_ENHANCED_COLOR or self.m_normalSubAttrValueColor
    end)

    self.m_extraAttributeCellCache:Refresh(#extraAttrShowList, function(cell, index)
        local attrInfo = extraAttrShowList[index]
        cell.equipEnhanceLevelNode:InitEquipEnhanceLevelNode({
            equipInstId = equipInstId,
            attrIndex = attrInfo.enhancedAttrIndex,
        })

        cell.transform:SetSiblingIndex(siblingIndex)
        siblingIndex = siblingIndex + 1
        local isEnhanced = cell.equipEnhanceLevelNode.enabled
        self:_RefreshAttributeCell(cell, attrInfo, true, isEnhanced)
        cell.attributeText.color = cell.equipEnhanceLevelNode.isEnhanced and
            self.view.config.ATTR_ENHANCED_COLOR or self.m_normalExtraAttrTextColor
        cell.fromValue.color = cell.equipEnhanceLevelNode.isEnhanced and
            self.view.config.ATTR_ENHANCED_COLOR or self.m_normalExtraAttrValueColor
    end)
end


WeaponAttributeNode._RefreshAttributeCell = HL.Method(HL.Table, HL.Table, HL.Opt(HL.Boolean, HL.Boolean)) << function(
    self, cell, attributeInfo, showPlus, showEnhance)
    cell.attributeText.text = attributeInfo.showName
    if attributeInfo.attrShowCfg and attributeInfo.attrShowCfg.isReduce then
        showPlus = false
    end
    if showPlus then

        if attributeInfo.modifiedValue > 0 then
            cell.fromValue.text = string.format(Language.LUA_WEAPON_EXHIBIT_UPGRADE_ADD_FORMAT, attributeInfo.showValue)
        else
            cell.fromValue.text = attributeInfo.showValue
        end
    else
        cell.fromValue.text = attributeInfo.showValue
    end


    local hasAttrIcon = not string.isEmpty(attributeInfo.iconName)
    cell.attributeIcon.enabled = hasAttrIcon

    if cell.circleNode then
        cell.circleNode.gameObject:SetActive(not hasAttrIcon and not showEnhance)
    end
    cell.attributeIcon.gameObject:SetActive(hasAttrIcon and not showEnhance)
    if hasAttrIcon and not showEnhance then
        cell.attributeIcon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, attributeInfo.iconName)
    end
end

WeaponAttributeNode.InitWeaponUpgradeAttributeNode = HL.Method(HL.Table, HL.Opt(HL.Table)) << function(self, arg, extraArg)
    self:_FirstTimeInit()

    local fromLv = arg.fromLv
    local fromBreakthroughLv = arg.fromBreakthroughLv
    local toLv = arg.toLv
    local toBreakthroughLv = arg.toBreakthroughLv
    local weaponInstId = arg.weaponInstId
    local gemInstId = arg.gemInstId or 0

    local mainAttributeList, subAttributeList = CharInfoUtils.getWeaponShowAttributes(weaponInstId, fromLv)
    local targetMainAttributeList, targetSubAttributeList = CharInfoUtils.getWeaponShowAttributes(weaponInstId, toLv)

    if extraArg and extraArg.showAttrTransition == true then
        self.m_effectCor = self:_StartCoroutine(function()
            self.m_mainAttributeCellCache:Refresh(#mainAttributeList, function(cell, index)
                local attributeInfo = mainAttributeList[index]
                local targetAttributeInfo = targetMainAttributeList[index]
                cell.animationWrapper:PlayInAnimation()
                coroutine.wait(0.1)
                self:_RefreshUpgradeAttributeCell(cell, attributeInfo, targetAttributeInfo)
                coroutine.wait(self.view.config.ATTR_TRANSITION_DURATION)
            end)
        end)
    else
        self.m_mainAttributeCellCache:Refresh(#mainAttributeList, function(cell, index)
            local attributeInfo = mainAttributeList[index]
            local targetAttributeInfo = targetMainAttributeList[index]
            self:_RefreshUpgradeAttributeCell(cell, attributeInfo, targetAttributeInfo)
        end)
    end
end

WeaponAttributeNode._RefreshUpgradeAttributeCell = HL.Method(HL.Table, HL.Table, HL.Table, HL.Opt(HL.Boolean)) << function(self, cell, attributeInfo, targetAttributeInfo, forceDiff)
    self:_RefreshAttributeCell(cell, attributeInfo)

    local hasDiff = math.abs(attributeInfo.attributeValue - targetAttributeInfo.attributeValue) > 0.00001



    local showDiff = hasDiff or forceDiff
    cell.toValueDeco.gameObject:SetActive(showDiff)
    cell.toValue.gameObject:SetActive(showDiff)

    cell.toValue.color = hasDiff and self.view.config.COLOR_TO_HAS_DIFF or self.view.config.COLOR_TO_NORMAL


    cell.toValue.text = targetAttributeInfo.showValue
end

HL.Commit(WeaponAttributeNode)
return WeaponAttributeNode

