
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoTalentUpgrade
CharInfoTalentUpgradeCtrl = HL.Class('CharInfoTalentUpgradeCtrl', uiCtrl.UICtrl)







CharInfoTalentUpgradeCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHAR_TALENT_SHOW_SKILL] = 'ShowSkillUpgrade',
    [MessageConst.CHAR_TALENT_SHOW_PASSIVE_SKILL] = 'ShowPassiveSkillUpgrade',
    [MessageConst.CHAR_TALENT_SHOW_ATTRIBUTE] = 'ShowAttributeUpgrade',
    [MessageConst.CHAR_TALENT_SHOW_SHIP_SKILL] = 'ShowShipSkillUpgrade',
    [MessageConst.CHAR_TALENT_SHOW_CHAR_BREAK] = 'ShowCharBreak',
    [MessageConst.CHAR_TALENT_SHOW_EQUIP_BREAK] = 'ShowEquipBreak',
    [MessageConst.CHAR_INFO_TALENT_EXIT_EXPAND_NODE] = '_ExternalExitExpandNode',
    [MessageConst.ON_CHAR_INFO_TALENT_INIT_CONTROLLER] = '_InitControllerPlaceHolder',
    [MessageConst.ON_ITEM_COUNT_CHANGED] = '_RefreshCost',
    [MessageConst.ON_WALLET_CHANGED] = '_RefreshCost',
    [MessageConst.ON_CHAR_DECK_ATTR_CHANGED] = '_OnCharDeckAttrChanged',
}

CharInfoTalentUpgradeCtrl.m_isSkillExpanding = HL.Field(HL.Boolean) << false

CharInfoTalentUpgradeCtrl.m_curNodeId = HL.Field(HL.String) << ''

CharInfoTalentUpgradeCtrl.m_curSkillId = HL.Field(HL.String) << ''

CharInfoTalentUpgradeCtrl.m_refreshCostFunc = HL.Field(HL.Function)




CharInfoTalentUpgradeCtrl.m_curConditionIdx = HL.Field(HL.Number) << 0






CharInfoTalentUpgradeCtrl.m_cachedSkillGroupCfg = HL.Field(HL.Any)
CharInfoTalentUpgradeCtrl.m_cachedCharInstId = HL.Field(HL.Number) << 0



CharInfoTalentUpgradeCtrl.m_cachedCurSkillLv = HL.Field(HL.Number) << 0
CharInfoTalentUpgradeCtrl.m_cachedIsMaxLv = HL.Field(HL.Boolean) << false


CharInfoTalentUpgradeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitActionEvent()
    self:_ResetUpgradePanel()

    local wrapper = self.animationWrapper
    wrapper:ClearTween()
    wrapper:SampleToInAnimationBegin()
    UIUtils.bindHyperlinkPopup(self, "CharInfoTalentUpgrade", self.view.inputGroup.groupId)
end

CharInfoTalentUpgradeCtrl.PhaseCharInfoPanelShowFinal = HL.Method(HL.Any) << function(self, arg)
    self:Show()

    self.view.rightNode.gameObject:SetActive(false)

    local charInfo = arg.initCharInfo
    if charInfo then
        local charCfg = Tables.characterTable[charInfo.templateId]
        self.view.textTitle.text = string.format(Language.LUA_CHAR_INFO_TALENT_TITLE_FORMAT, charCfg.name)
    end

    local wrapper = self.animationWrapper
    wrapper:PlayInAnimation()
end

CharInfoTalentUpgradeCtrl._RefreshNodeCommon = HL.Method(HL.Table, HL.Number, HL.Boolean, HL.Boolean, HL.Any, HL.Opt(HL.Function, HL.Function))
    << function(self, node, charInstId, isActive, isLock, costList, conditionShowFunc, conditionCheckFunc)
    local isTrailCard = not CharInfoUtils.isCharDevAvailable(charInstId)
    local stateCtrl = node.stateController
    if stateCtrl then
        stateCtrl:SetState((isTrailCard or isActive) and "Unlocked" or "Locked")
    end
    if isTrailCard then
        node.btnUpgrade.gameObject:SetActive(false)
        node.btnLock.gameObject:SetActive(false)
        node.btnActivated.gameObject:SetActive(false)
        node.commonTitle.gameObject:SetActive(false)
        node.itemList.gameObject:SetActive(false)
        if node.scrollView then
            node.scrollView.gameObject:SetActive(false)
        end
        if node.activationConditions then
            node.activationConditions.gameObject:SetActive(false)
        end
        return
    end

    node.btnUpgrade.gameObject:SetActive(false)
    node.btnLock.gameObject:SetActive(false)
    node.btnActivated.gameObject:SetActive(false)

    node.btnLock.gameObject:SetActive(isLock)
    node.btnUpgrade.gameObject:SetActive(not isLock and not isActive)
    node.btnActivated.gameObject:SetActive(isActive)
    node.commonTitle.gameObject:SetActive(not isActive)
    node.itemList.gameObject:SetActive(not isActive)
    if node.scrollView then
        node.scrollView.gameObject:SetActive(not isActive)
    end

    if node.upgradeItemCellCache == nil then
        node.upgradeItemCellCache = UIUtils.genCellCache(node.itemCell)
    end

    self.m_refreshCostFunc = function()
        if type(costList) == "table" then
            node.upgradeItemCellCache:Refresh(#costList, function(cell, index)
                local itemCfg = costList[index]
                self:_RefreshItemCell(cell, itemCfg.id, itemCfg.count)
            end)
        elseif type(costList) == "userdata" then
            node.upgradeItemCellCache:Refresh(#costList, function(cell, index)
                local itemCfg = costList[CSIndex(index)]
                self:_RefreshItemCell(cell, itemCfg.id, itemCfg.count)
            end)
        end
    end
    self.m_refreshCostFunc()

    if node.activationConditions then
        local isShowCondition = conditionShowFunc and conditionShowFunc()
        node.activationConditions.gameObject:SetActive((not isActive) and isShowCondition)

        local isPass = true
        if conditionCheckFunc then
            isPass = conditionCheckFunc()
        end
        node.activeIcon.gameObject:SetActive(isPass)
        node.defaultIcon.gameObject:SetActive(not isPass)
    end
end


local SKILL_GROUP_TYPE
local SKILL_NODE_REFRESH_ANIM = "charInfoskillnodenew_refresh"
CharInfoTalentUpgradeCtrl.ShowSkillUpgrade = HL.Method(HL.Table) << function(self, arg)
    self.m_curNodeId = ''
    if self.m_curSkillId == arg.skillGroupId and not arg.forceUpdate then
        return
    else
        self.m_curSkillId = arg.skillGroupId
    end

    self:_ResetUpgradePanel()
    self:_ToggleExpandNode(true)

    self.view.skillUpgrade.gameObject:SetActive(true)
    self.view.btnExpand.gameObject:SetActive(true)

    local charInstId = arg.charInstId
    local skillGroupId = arg.skillGroupId
    local skillGroupType = arg.skillGroupType
    local curSkillLv = arg.curSkillLv
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local skillGroupCfg = CharInfoUtils.getSkillGroupCfg(charInst.templateId, skillGroupId)

    local skillUpgradeNode = self.view.skillUpgrade


    local skillInfo = CharInfoUtils.getCharSkillLevelInfoByType(charInst, skillGroupType)
    local canUpgradeLv = skillInfo.maxLevel
    local isMaxLv = curSkillLv == canUpgradeLv and curSkillLv == UIConst.CHAR_MAX_SKILL_LV
    local isElite = curSkillLv >= UIConst.CHAR_MAX_SKILL_NORMAL_LV


    local hasBothConditions = CharInfoUtils.hasBothSkillGroupConditions(skillGroupCfg)
    if hasBothConditions then
        self.m_curConditionIdx = (arg.curConditionIdx == 1 or arg.curConditionIdx == 2)
            and arg.curConditionIdx or CharInfoUtils.getActiveSkillGroupConditionIdx(charInst.instId, skillGroupCfg) or 1

        self.m_cachedSkillGroupCfg = skillGroupCfg
        self.m_cachedCharInstId = charInst.instId
        self.m_cachedCurSkillLv = curSkillLv
        self.m_cachedIsMaxLv = isMaxLv
    else
        self.m_curConditionIdx = 0
    end

    skillUpgradeNode.stanceSwitchSkillNode.gameObject:SetActive(hasBothConditions)
    if hasBothConditions then
        self:_RefreshStanceSwitchSkillNode(skillGroupCfg, charInst.instId)
    end

    self:_RefreshMainSkillUpgradeNode(skillUpgradeNode, charInst, skillGroupId, curSkillLv)
    if isMaxLv then
        self.view.btnExpand.gameObject:SetActive(false)
    else
        self:_RefreshMainSkillUpgradeNode(self.view.skillUpgradeAfter, charInst, skillGroupId, curSkillLv + 1)
    end
    local isActive = isMaxLv
    local isLock = (not isMaxLv) and curSkillLv >= canUpgradeLv
    local costList = {}
    local skillUpgradeCfg = CharInfoUtils.getSkillTalentNodeBySkillId(charInst.templateId, skillGroupId, curSkillLv + 1)
    if skillUpgradeCfg ~= nil then
        local upgradeItemBundle = skillUpgradeCfg.itemBundle
        local goldCost = skillUpgradeCfg.goldCost
        for i = 1, #upgradeItemBundle do
            local itemCfg = upgradeItemBundle[CSIndex(i)]
            table.insert(costList, itemCfg)
        end
        if goldCost > 0 then
            table.insert(costList, {
                id = UIConst.INVENTORY_MONEY_IDS[1],
                count = goldCost,
            })
        end
    end
    self:_RefreshNodeCommon(skillUpgradeNode, charInstId, isActive, isLock, costList)

    skillUpgradeNode.text.text = UIConst.CHAR_INFO_SKILL_GROUP_TYPE_TO_TYPE_NAME[skillGroupType]
    skillUpgradeNode.btnUpgrade.text = isElite and Language.LUA_CHAR_INFO_TALENT_UPGRADE_ELITE or Language.LUA_CHAR_INFO_TALENT_UPGRADE_NORMAL
    skillUpgradeNode.lockText.text = string.format(Language.LUA_CHAR_INFO_TALENT_UPGRADE_BREAK_LOCK_HINT, charInst.breakStage + 1)

    local onClickUpgradeFunc = function()
        local isTrail = charInst.charType == GEnums.CharType.Trial
        if isTrail then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_TALENT_UPGRADE_FORBID)
            return
        end

        for i, costInfo in pairs(costList) do
            local needCount = costInfo.count
            local itemId = costInfo.id

            if needCount > Utils.getItemCount(itemId, true) then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_TALENT_UPGRADE_ITEM_NOT_ENOUGH)
                return
            end
        end
        GameInstance.player.charBag:SkillLevelUpgrade(charInst.instId, skillGroupId, skillGroupType)
    end


    skillUpgradeNode.btnUpgrade.onClick:RemoveAllListeners()
    skillUpgradeNode.btnUpgrade.onClick:AddListener(onClickUpgradeFunc)

    skillUpgradeNode.btnBreak.gameObject:SetActive(false)
    skillUpgradeNode.btnBreak.onClick:RemoveAllListeners()
    skillUpgradeNode.btnBreak.onClick:AddListener(onClickUpgradeFunc)


    if skillUpgradeNode.btnUpgrade.gameObject.activeSelf and isElite then
        skillUpgradeNode.btnUpgrade.gameObject:SetActive(false)
        skillUpgradeNode.btnBreak.gameObject:SetActive(true)
    end
end

CharInfoTalentUpgradeCtrl._RefreshMainSkillUpgradeNode = HL.Method(HL.Any, HL.Any, HL.String, HL.Number) << function(self, skillUpgradeNode, charInst, skillGroupId, curSkillLv)
    local skillGroupCfg = CharInfoUtils.getSkillGroupCfg(charInst.templateId, skillGroupId, true)
    if skillGroupCfg == nil then
        return
    end
    skillUpgradeNode.skillName.text = skillGroupCfg.name


    local icon = CharInfoUtils.generateSkillGroupConditionIcon(
            charInst.instId, skillGroupCfg, self.m_curConditionIdx)
    skillUpgradeNode.skillIcon:LoadSprite(UIConst.UI_SPRITE_SKILL_ICON, icon and icon or skillGroupCfg.icon)

    local bgColor = CharInfoUtils.getCharInfoSkillGroupBgColor(skillGroupCfg)
    skillUpgradeNode.bgSkillColor2.color = bgColor


    local postDesc = CharInfoUtils.generateSkillGroupConditionPostDescText(
        charInst.instId, skillGroupCfg, self.m_curConditionIdx)

    local desc = Utils.SkillUtil.GetSkillGroupDescription(charInst.templateId, skillGroupId, curSkillLv, charInst.instId)
    local combinedDesc = table.concat({desc, postDesc}, "\n"):gsub("^\n+", ""):gsub("\n+$", "")
    skillUpgradeNode.desc:SetAndResolveTextStyle(combinedDesc)


    self:_RefreshStanceSwitchSkillDesc(skillUpgradeNode, charInst.instId, skillGroupCfg)

    local isElite = curSkillLv >= UIConst.CHAR_MAX_SKILL_NORMAL_LV
    local showSkillLv = lume.clamp(curSkillLv, 1, UIConst.CHAR_MAX_SKILL_NORMAL_LV)
    local eliteLv = isElite and curSkillLv - UIConst.CHAR_MAX_SKILL_NORMAL_LV or 0

    skillUpgradeNode.rank.text = string.format(Language.LUA_CHAR_INFO_TALENT_SKILL_LEVEL_PREFIX, showSkillLv)
    skillUpgradeNode.eliteNode.gameObject:SetActive(isElite)
    skillUpgradeNode.elitePolygon.gameObject:SetActive(isElite)
    skillUpgradeNode.elitePolygon:InitElitePolygon(eliteLv)

    skillUpgradeNode.previewText.text = isElite and Language.LUA_CHAR_INFO_SKILL_UPGRADE_PREVIEW_ELITE or Language.LUA_CHAR_INFO_SKILL_UPGRADE_PREVIEW_NORMAL

    if skillUpgradeNode.subDescCellCache == nil then
        skillUpgradeNode.subDescCellCache = UIUtils.genCellCache(skillUpgradeNode.subDescCell)
    end

    local skillDescNameList, skillDescList = self:_generateSkillGroupSubDescList(charInst.templateId, skillGroupId, curSkillLv, charInst.instId)
    skillUpgradeNode.subDescCellCache:Refresh(#skillDescNameList, function(cell, index)
        if skillDescNameList[index] == nil then
            cell.gameObject:SetActive(false)
            return
        end

        local subDescName = skillDescNameList[index]
        local subDesc = skillDescList[index]
        cell.bg.gameObject:SetActive(index % 2 == 0)
        cell.subDescName.text = subDescName
        cell.subDesc.text = subDesc
    end)

end





CharInfoTalentUpgradeCtrl._generateSkillGroupSubDescList = HL.Method(HL.String, HL.String, HL.Number, HL.Number).Return(HL.Table, HL.Table) << function(self, charTemplateId, skillGroupId, skillGroupLv, charInstId)
    local selectedConditionId
    if self.m_curConditionIdx == 1 or self.m_curConditionIdx == 2 then
        local skillGroupCfg = CharInfoUtils.getSkillGroupCfg(charTemplateId, skillGroupId)
        if skillGroupCfg ~= nil then
            selectedConditionId = self.m_curConditionIdx == 1 and skillGroupCfg.conditionId1 or skillGroupCfg.conditionId2
        end
    end
    return CharInfoUtils.getSkillGroupSubDescList(charTemplateId, skillGroupId, skillGroupLv, charInstId, selectedConditionId)
end

CharInfoTalentUpgradeCtrl.ShowPassiveSkillUpgrade = HL.Method(HL.Table) << function(self, arg)
    local charInstId = arg.charInstId
    local nodeIndex = arg.nodeIndex
    local selectNodeLv = arg.selectNodeLv

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local selectedNodeCfg = CharInfoUtils.getPassiveSkillTalentNodeByIndex(charInst.templateId, nodeIndex, selectNodeLv)

    self.m_curSkillId = ''
    if self.m_curNodeId == selectedNodeCfg.nodeId and not arg.forceUpdate then
        return
    else
        self.m_curNodeId = selectedNodeCfg.nodeId
    end

    self:_ResetUpgradePanel()
    self:_ToggleExpandNode(true)
    local passiveSkillNode = self.view.passiveSkillNode
    passiveSkillNode.gameObject:SetActive(true)

    local isActive, isLock, lockText = CharInfoUtils.getPassiveSkillNodeStatus(charInst.instId, selectedNodeCfg.nodeId)
    self:_RefreshNodeCommon(passiveSkillNode, charInstId, isActive, isLock, selectedNodeCfg.requiredItem)

    local nodeDataList = CharInfoUtils.getAllPassiveSkillTalentNodeByIndex(charInst.templateId, nodeIndex)
    passiveSkillNode.title.text = nodeDataList[selectNodeLv].passiveSkillNodeInfo.name
    if passiveSkillNode.skillStageCellCache == nil then
        passiveSkillNode.skillStageCellCache = UIUtils.genCellCache(passiveSkillNode.stageCell)
    end

    local cellBefore = nil
    local selectedCell = nil
    passiveSkillNode.skillStageCellCache:Refresh(#nodeDataList, function(cell, index)
        local nodeCfg = nodeDataList[index]
        local passiveSkillNodeData = nodeCfg.passiveSkillNodeInfo
        local isSelectedNodeLv = passiveSkillNodeData.level == selectNodeLv
        if isSelectedNodeLv then
            selectedCell = cell
        end
        local selectedNodeCfg = CharInfoUtils.getPassiveSkillTalentNodeByIndex(charInst.templateId, nodeIndex, passiveSkillNodeData.level)

        local isActive, isLock = CharInfoUtils.getPassiveSkillNodeStatus(charInst.instId, selectedNodeCfg.nodeId)
        local nodeDesc = CS.Beyond.Gameplay.TalentUtil.GetTalentNodeDescription(charInst.templateId, nodeCfg.nodeId, charInst.instId)
        cell.desc:SetAndResolveTextStyle(nodeDesc)

        cell.stageLevelCellGroup:InitStageLevelCellGroup(index, isLock, isActive)
        cell.selected.gameObject:SetActive(isSelectedNodeLv)

        local isLastCell = #nodeDataList == index
        cell.defaultLine.gameObject:SetActive(not isLastCell)
        cell.activationLine.gameObject:SetActive(false)

        cell.stateController:SetState(self:_GetStageTitleStateName(isActive, isLock))
        if cellBefore ~= nil and isActive then
            cellBefore.stateController:SetState("Replaced")
            cellBefore.activationLine.gameObject:SetActive(true)
            cellBefore.stageLevelCellGroup:InitStageLevelCellGroup(index - 1, isLock)
        end

        cellBefore = cell
    end)

    if selectedCell then
        LayoutRebuilder.ForceRebuildLayoutImmediate(passiveSkillNode.passiveSkillScrollView.transform)
        passiveSkillNode.passiveSkillScrollView:AutoScrollToRectTransform(selectedCell.transform, true)
    end

    passiveSkillNode.icon:LoadSprite(UIConst.UI_SPRITE_SKILL_ICON, selectedNodeCfg.passiveSkillNodeInfo.iconId)
    passiveSkillNode.lockText.text = lockText

    passiveSkillNode.btnUpgrade.onClick:RemoveAllListeners()
    passiveSkillNode.btnUpgrade.onClick:AddListener(function()
        self:_TryUnlockTalentNode(charInstId, selectedNodeCfg.nodeId)
    end)
end

CharInfoTalentUpgradeCtrl.ShowAttributeUpgrade = HL.Method(HL.Table) << function(self, arg)
    self.m_curSkillId = ''
    if self.m_curNodeId == arg.talentCfg.nodeId and not arg.forceUpdate then
        return
    else
        self.m_curNodeId = arg.talentCfg.nodeId
    end

    self:_ResetUpgradePanel()
    self:_ToggleExpandNode(true)

    self.view.attributeNode.gameObject:SetActive(true)

    local charInstId = arg.charInstId
    local talentCfg = arg.talentCfg
    local attrCfg = arg.talentCfg.attributeNodeInfo
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)

    local attributeNode = self.view.attributeNode
    attributeNode.title.text = attrCfg.title
    attributeNode.desc.text = attrCfg.desc

    local isActive, isLock, lockText = CharInfoUtils.getAttributeNodeStatus(charInst.instId, talentCfg.nodeId)

    attributeNode.lockText.text = lockText

    local friendshipValue = CSPlayerDataUtil.GetCharFriendshipByInstId(charInst.instId)

    local needFavorability = attrCfg.favorability ~= nil and attrCfg.favorability  or 0
    local pass = friendshipValue >= needFavorability
    local conditionFormat = pass and Language.LUA_CHAR_INFO_TALENT_UPGRADE_FRIENDSHIP_CONDITION_PASS or Language.LUA_CHAR_INFO_TALENT_UPGRADE_FRIENDSHIP_CONDITION_NOT_PASS
    local needValueText = string.format("%.0f%%", CharInfoUtils.getCharRelationShowValue(attrCfg.favorability))
    local curValueText = string.format("%.0f%%", CharInfoUtils.getCharRelationShowValue(friendshipValue))
    local conditionText = string.format(conditionFormat, needValueText, curValueText)


    local iconName = attrCfg.customIcon or ""
    if string.isEmpty(iconName) then
        local attrType = CharInfoUtils.getTalentAttributeNodeDisplayAttrType(charInst.templateId, attrCfg)
        local attrKey = Const.ATTRIBUTE_TYPE_2_ATTRIBUTE_DATA_KEY[attrType]
        iconName = UIConst.UI_ATTRIBUTE_ICON_PREFIX .. attrKey
    end
    attributeNode.icon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, iconName)
    attributeNode.activeIcon.gameObject:SetActive(pass)
    attributeNode.defaultIcon.gameObject:SetActive(not pass)
    attributeNode.condition:SetAndResolveTextStyle(conditionText)

    self:_RefreshNodeCommon(attributeNode, charInstId, isActive, isLock, talentCfg.requiredItem, function()
        return needFavorability > 0
    end, function()
        return friendshipValue >= needFavorability
    end)


    attributeNode.btnUpgrade.onClick:RemoveAllListeners()
    attributeNode.btnUpgrade.onClick:AddListener(function()
        self:_TryUnlockTalentNode(charInstId, talentCfg.nodeId)
    end)
end

CharInfoTalentUpgradeCtrl.ShowShipSkillUpgrade = HL.Method(HL.Table) << function(self, arg)
    self.m_curNodeId = ''
    if self.m_curSkillId == arg.skillId and not arg.forceUpdate then
        return
    else
        self.m_curSkillId = arg.skillId
    end

    self:_ResetUpgradePanel()
    self:_ToggleExpandNode(true)

    local charInstId = arg.charInstId
    local shipSkillId = arg.skillId
    local skillIndex = arg.skillIndex

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)

    local factorySkillNode = self.view.factorySkillNode
    local spaceShipSkillCfg = Tables.spaceshipSkillTable[shipSkillId]
    factorySkillNode.gameObject:SetActive(true)
    factorySkillNode.title.text = spaceShipSkillCfg.name
    factorySkillNode.icon:LoadSprite(UIConst.UI_SPRITE_SS_SKILL_ICON, spaceShipSkillCfg.icon)

    local skillUpgradeCfg = CharInfoUtils.getShipSkillTalentNodeBySkillId(charInst.templateId, shipSkillId)

    if factorySkillNode.stageCellCache == nil then
        factorySkillNode.stageCellCache = UIUtils.genCellCache(factorySkillNode.stageCell)
    end


    local shipSkillUpgradeList = CharInfoUtils.getCharSpaceshipSkillUpgradeList(charInst.templateId)
    local innerUpgradeList = shipSkillUpgradeList[skillIndex]
    local cellBefore = nil
    factorySkillNode.stageCellCache:Refresh(#innerUpgradeList, function(cell, index)
        local charSkillCfg = innerUpgradeList[index].charSkillCfg
        local skillCfg = innerUpgradeList[index].skillCfg

        local isSelected = shipSkillId == skillCfg.id
        local shipSkillNodeCfg = CharInfoUtils.getShipSkillTalentNodeBySkillId(charInst.templateId, charSkillCfg.skillId)
        local isActive, isLock, lockText = CharInfoUtils.getShipSkillNodeStatus(charInst.instId, shipSkillNodeCfg.nodeId)
        local shipSkillCfg = Tables.spaceshipSkillTable[charSkillCfg.skillId]

        cell.title.text = skillCfg.name
        cell.desc.text = skillCfg.desc

        local isLastCell = #innerUpgradeList == index
        cell.defaultLine.gameObject:SetActive(not isLastCell)
        cell.activationLine.gameObject:SetActive(false)

        cell.selected.gameObject:SetActive(isSelected)
        cell.stageLevelStateController:SetState(shipSkillCfg.skillNamePostfix)
        cell.stateController:SetState(self:_GetStageTitleStateName(isActive, isLock))

        if cellBefore ~= nil and isActive then
            cellBefore.activationLine.gameObject:SetActive(true)
            cellBefore.stateController:SetState("Replaced")
        end
        cellBefore = cell
    end)

    local isActive, isLock, lockText = CharInfoUtils.getShipSkillNodeStatus(charInst.instId, skillUpgradeCfg.nodeId)
    self:_RefreshNodeCommon(factorySkillNode, charInstId, isActive, isLock, skillUpgradeCfg.requiredItem)

    local isTrailCard = not CharInfoUtils.isCharDevAvailable(charInstId)
    factorySkillNode.btnActivated.gameObject:SetActive(isActive and not isTrailCard)
    factorySkillNode.lockText.text = lockText

    factorySkillNode.btnUpgrade.onClick:RemoveAllListeners()
    factorySkillNode.btnUpgrade.onClick:AddListener(function()
        self:_TryUnlockTalentNode(charInstId, skillUpgradeCfg.nodeId)
    end)
end

CharInfoTalentUpgradeCtrl.ShowCharBreak = HL.Method(HL.Table) << function(self, arg)
    self.m_curSkillId = ''
    if self.m_curNodeId == arg.nodeId and not arg.forceUpdate then
        return
    else
        self.m_curNodeId = arg.nodeId
    end

    self:_ResetUpgradePanel()
    self:_ToggleExpandNode(true)

    local charBreakNode = self.view.charBreakNode
    charBreakNode.gameObject:SetActive(true)

    local nodeId = arg.nodeId
    local charInstId = arg.charInstId

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local charTemplateId = charInst.templateId

    local charGrowthDict = CharInfoUtils.getCharGrowthData(charTemplateId)
    local charBreakNodeCfg = charGrowthDict.charBreakCostMap[nodeId]

    charBreakNode.name.text = charBreakNodeCfg.name
    charBreakNode.charEliteMarker:InitCharEliteMarkerByBreakStage(charBreakNodeCfg.breakStage)
    charBreakNode.desc.text = charBreakNodeCfg.description

    local breakStageCfg = Tables.charBreakStageTable[charBreakNodeCfg.breakStage - 1]
    local pass = charInst.level >= breakStageCfg.maxCharLevel
    local conditionFormat = pass and Language.LUA_CHAR_INFO_TALENT_UPGRADE_BREAK_CONDITION_PASS or Language.LUA_CHAR_INFO_TALENT_UPGRADE_BREAK_CONDITION_NOT_PASS
    local conditionText = string.format(conditionFormat, breakStageCfg.maxCharLevel, charInst.level)
    local isActive, isLock, lockDetail = CharInfoUtils.getCharBreakNodeStatus(charInst.instId, nodeId)

    self:_RefreshNodeCommon(charBreakNode, charInstId, isActive, isLock, charBreakNodeCfg.requiredItem, function()
        return true
    end, function()
        return charInst.level >= breakStageCfg.maxCharLevel
    end)

    if isLock then
        local breakStageCfg = Tables.charBreakStageTable[charBreakNodeCfg.breakStage]
        if charInst.level < breakStageCfg.minCharLevel then
            charBreakNode.lockText.text = string.format(Language.LUA_CHAR_INFO_TALENT_UPGRADE_LEVEL_LOCK_HINT, breakStageCfg.minCharLevel)
        elseif charInst.equipTierLimit < charBreakNodeCfg.equipTierLimit then
            local equipBreakNodeId = CharInfoUtils.getCharBreakNodeFromStageAndEquipTier(charInst.breakStage, charBreakNodeCfg.equipTierLimit)
            if equipBreakNodeId then
                local breakDetail = charGrowthDict.charBreakCostMap[equipBreakNodeId]

                charBreakNode.lockText.text = string.format(Language.LUA_CHAR_INFO_TALENT_UPGRADE_EQUIP_LOCK_HINT, breakDetail.name)
            else
                logger.error(string.format("角色养成->找不到对应突破阶段, 角色突破[%s], 装备稀有度[%s]", charInst.breakStage, charBreakNodeCfg.equipTierLimit))
            end
        end
    end

    local isTrailCard = not CharInfoUtils.isCharDevAvailable(charInstId)
    charBreakNode.btnLock.gameObject:SetActive(isLock and (not lockDetail.isLockByLv))
    charBreakNode.btnJump.gameObject:SetActive(isLock and lockDetail.isLockByLv and not isTrailCard)

    if isLock and lockDetail.isLockByLv then
        charBreakNode.btnJump.text = Language.LUA_CHAR_INFO_JUMP_TO_UPGRADE
        charBreakNode.btnJump.onClick:RemoveAllListeners()
        charBreakNode.btnJump.onClick:AddListener(function()
            self.view.btnClose.onClick:Invoke()
            self:Notify(MessageConst.CHAR_INFO_PAGE_CHANGE, {
                pageType = UIConst.CHAR_INFO_PAGE_TYPE.UPGRADE,
                isFast = true,
                showGlitch = true,
            })
        end)
    end


    charBreakNode.condition:SetAndResolveTextStyle(conditionText)

    charBreakNode.btnUpgrade.onClick:RemoveAllListeners()
    charBreakNode.btnUpgrade.onClick:AddListener(function()
        self:_TryUnlockTalentNode(charInstId, nodeId)
    end)

end

CharInfoTalentUpgradeCtrl.ShowEquipBreak = HL.Method(HL.Table) << function(self, arg)
    self.m_curSkillId = ''
    if self.m_curNodeId == arg.nodeId and not arg.forceUpdate then
        return
    else
        self.m_curNodeId = arg.nodeId
    end

    self:_ResetUpgradePanel()
    self:_ToggleExpandNode(true)

    local equipBreakNode = self.view.equipBreakNode

    equipBreakNode.gameObject:SetActive(true)

    local nodeId = arg.nodeId
    local charInstId = arg.charInstId

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local charTemplateId = charInst.templateId

    local charGrowthCfg = CharInfoUtils.getCharGrowthData(charTemplateId)
    local charBreakDetail = charGrowthCfg.charBreakCostMap[nodeId]

    equipBreakNode.name.text = charBreakDetail.name
    equipBreakNode.desc.text = charBreakDetail.description

    local isActive, isLock = CharInfoUtils.getEquipBreakNodeStatus(charInst.instId, nodeId)
    self:_RefreshNodeCommon(equipBreakNode, charInstId, isActive, isLock, charBreakDetail.requiredItem)
    equipBreakNode.btnLock.gameObject:SetActive(isLock)

    equipBreakNode.stageLevelCellGroup:InitStageLevelCellGroup(charBreakDetail.breakStage, isLock)
    equipBreakNode.btnUpgrade.onClick:RemoveAllListeners()
    equipBreakNode.btnUpgrade.onClick:AddListener(function()
        self:_TryUnlockTalentNode(charInstId, nodeId)
    end)

    equipBreakNode.lockText.text = string.format(Language.LUA_CHAR_INFO_TALENT_UPGRADE_BREAK_LOCK_HINT, charBreakDetail.breakStage)
end

CharInfoTalentUpgradeCtrl._GetTextColor = HL.Method(HL.Boolean, HL.Boolean).Return(HL.Any) << function(self, isLock, hadUpgraded)
    local textColor = self.view.config.TEXT_COLOR_DEFAULT
    if hadUpgraded then
        textColor = self.view.config.TEXT_COLOR_ACTIVE
    elseif isLock then
        textColor = self.view.config.TEXT_COLOR_LOCK
    end
    return textColor
end

CharInfoTalentUpgradeCtrl._GetStageTitleStateName = HL.Method(HL.Boolean, HL.Boolean).Return(HL.String) << function(self, isActive, isLocked)
    if isActive then
        return "Active"
    elseif isLocked then
        return "Locked"
    else
        return "Normal"
    end
end

CharInfoTalentUpgradeCtrl._InitActionEvent = HL.Method() << function(self)
    self.view.btnClose.onClick:AddListener(function()
        self:Notify(MessageConst.TRY_CLOSE_CHAR_TALENT)
        self:_ToggleExpandNode(false)
    end)



    self.view.btnExpand.onClick:AddListener(function()
        self:_ToggleSkillNextInfo(true)
    end)
    self.view.btnShrink.onClick:AddListener(function()
        self:_ToggleSkillNextInfo(false)
    end)
end

CharInfoTalentUpgradeCtrl._ToggleSkillNextInfo = HL.Method(HL.Boolean) << function(self, isOn)
    self.m_isSkillExpanding = isOn
    self.view.skillUpgradeAfter.gameObject:SetActive(isOn)
    self.view.btnExpand.gameObject:SetActive(not isOn)
    self.view.btnShrink.gameObject:SetActive(isOn)
    if DeviceInfo.usingController then
        InputManagerInst:ToggleGroup(self.view.skillUpgrade.skillDescScrollInputBindingGroupMonoTarget.groupId, not isOn)
    end
    self.view.skillUpgrade.scrollEnableNode.gameObject:SetActive(not isOn)
    Notify(MessageConst.ON_CHAR_INFO_TALENT_SKILL_NEXT_EXPAND, isOn)
end






CharInfoTalentUpgradeCtrl._RefreshStanceSwitchSkillNode = HL.Method(HL.Userdata, HL.Number) << function(self, skillGroupCfg, charInstId)
    local stanceSwitchSkillNode = self.view.skillUpgrade.stanceSwitchSkillNode
    stanceSwitchSkillNode.tab1Toggle.titleTxt:SetAndResolveTextStyle(skillGroupCfg.conditionName1)
    stanceSwitchSkillNode.tab2Toggle.titleTxt:SetAndResolveTextStyle(skillGroupCfg.conditionName2)


    local displayData1 = CharInfoUtils.getSkillGroupConditionDisplayDataByIndex(charInstId, skillGroupCfg, 1)
    local displayData2 = CharInfoUtils.getSkillGroupConditionDisplayDataByIndex(charInstId, skillGroupCfg, 2)
    stanceSwitchSkillNode.tab1Toggle.nowNode.gameObject:SetActive(displayData1.isActive)
    stanceSwitchSkillNode.tab2Toggle.nowNode.gameObject:SetActive(displayData2.isActive)


    stanceSwitchSkillNode.tab1Toggle.toggle.onValueChanged:RemoveAllListeners()
    stanceSwitchSkillNode.tab2Toggle.toggle.onValueChanged:RemoveAllListeners()

    stanceSwitchSkillNode.tab1Toggle.toggle.isOn = self.m_curConditionIdx == 1
    stanceSwitchSkillNode.tab2Toggle.toggle.isOn = self.m_curConditionIdx == 2

    stanceSwitchSkillNode.tab1Toggle.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:_OnStanceTabChanged(1, charInstId, skillGroupCfg)
        end
    end)
    stanceSwitchSkillNode.tab2Toggle.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:_OnStanceTabChanged(2, charInstId, skillGroupCfg)
        end
    end)
end






CharInfoTalentUpgradeCtrl._OnStanceTabChanged = HL.Method(HL.Number, HL.Number, HL.Userdata) << function(self, conditionIdx, charInstId, skillGroupCfg)
    self.m_curConditionIdx = conditionIdx
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    if charInst == nil then
        return
    end
    local skillGroupId = self.m_curSkillId
    local curSkillLv = self.m_cachedCurSkillLv
    self:_RefreshMainSkillUpgradeNode(self.view.skillUpgrade, charInst, skillGroupId, curSkillLv)
    self.view.skillUpgrade.charInfoSkillNodeAnimation:Play(SKILL_NODE_REFRESH_ANIM)

    self.view.skillUpgrade.skillDescScroll.verticalNormalizedPosition = 1
    if not self.m_cachedIsMaxLv then
        self:_RefreshMainSkillUpgradeNode(self.view.skillUpgradeAfter, charInst, skillGroupId, curSkillLv + 1)
        self.view.skillUpgradeAfter.charInfoSkillNodeAnimation:Play(SKILL_NODE_REFRESH_ANIM)

        self.view.skillUpgradeAfter.skillDescScroll.verticalNormalizedPosition = 1
    end
end



CharInfoTalentUpgradeCtrl._RefreshStanceSwitchSkillDesc = HL.Method(HL.Any, HL.Number, HL.Userdata)
    << function(self, skillUpgradeNode, charInstId, skillGroupCfg)
    local desc, isActive = CharInfoUtils.generateSkillGroupConditionDescText(charInstId, skillGroupCfg, self.m_curConditionIdx)
    if desc then
        skillUpgradeNode.stanceSwitchSkillDesc:SetAndResolveTextStyle(desc)
        skillUpgradeNode.stanceSwitchSkillDescStateController:SetState(isActive
            and "Active" or "Inactive")
        skillUpgradeNode.stanceSwitchSkillDesc.gameObject:SetActive(true)
    else
        skillUpgradeNode.stanceSwitchSkillDesc.gameObject:SetActive(false)
    end
end

CharInfoTalentUpgradeCtrl._StopSkillNodeRefreshAnimations = HL.Method() << function(self)
    local skillUpgradeAnim = self.view.skillUpgrade.charInfoSkillNodeAnimation
    if skillUpgradeAnim.isPlaying then
        skillUpgradeAnim:Stop()
        skillUpgradeAnim:SeekToPercent(SKILL_NODE_REFRESH_ANIM, 1)
    end

    local skillUpgradeAfterAnim = self.view.skillUpgradeAfter.charInfoSkillNodeAnimation
    if skillUpgradeAfterAnim.isPlaying then
        skillUpgradeAfterAnim:Stop()
        skillUpgradeAfterAnim:SeekToPercent(SKILL_NODE_REFRESH_ANIM, 1)
    end
end

CharInfoTalentUpgradeCtrl._ResetUpgradePanel = HL.Method() << function(self)
    self:_StopSkillNodeRefreshAnimations()
    self:_ToggleSkillNextInfo(false)
    self.view.skillUpgradeAfter.gameObject:SetActive(false)
    self.view.skillUpgrade.gameObject:SetActive(false)
    self.view.factorySkillNode.gameObject:SetActive(false)
    self.view.attributeNode.gameObject:SetActive(false)
    self.view.charBreakNode.gameObject:SetActive(false)
    self.view.equipBreakNode.gameObject:SetActive(false)
    self.view.passiveSkillNode.gameObject:SetActive(false)
    self.view.btnShrink.gameObject:SetActive(false)
    self.view.btnExpand.gameObject:SetActive(false)
    self.m_curConditionIdx = 0
    self.m_cachedSkillGroupCfg = nil
    self.m_cachedCharInstId = 0
    self.m_cachedCurSkillLv = 0
    self.m_cachedIsMaxLv = false
end






CharInfoTalentUpgradeCtrl._OnCharDeckAttrChanged = HL.Method(HL.Table) << function(self, arg)
    local instId = unpack(arg)
    if instId ~= self.m_cachedCharInstId or not self.m_cachedSkillGroupCfg or string.isEmpty(self.m_curSkillId) then
        return
    end
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(instId)
    self:_RefreshMainSkillUpgradeNode(self.view.skillUpgrade, charInst, self.m_curSkillId, self.m_cachedCurSkillLv)
    if self.view.skillUpgradeAfter.gameObject.activeSelf then
        self:_RefreshMainSkillUpgradeNode(self.view.skillUpgradeAfter, charInst, self.m_curSkillId, self.m_cachedCurSkillLv)
    end
end

CharInfoTalentUpgradeCtrl._RefreshItemCell = HL.Method(HL.Userdata, HL.String, HL.Number) << function(self, cell, itemId, needCount)
    cell:InitItem({
        id = itemId,
        count = needCount,
    }, true)
    cell:SetExtraInfo({
        tipsPosTransform = self.view.itemTipsPos,
        isSideTips = DeviceInfo.usingController,
    })
    if DeviceInfo.usingController then
        cell:SetEnableHoverTips(false)
    end

    local storageCount = Utils.getItemCount(itemId, true)
    cell.view.storageNode:InitStorageNode(storageCount, needCount, true)
    if storageCount < needCount then
        cell:SetItemTipsJumpExtraArgs({
            expectedRewardItemId = itemId,
            expectedRewardItemCount = needCount - storageCount,
        })
    end
end

CharInfoTalentUpgradeCtrl._ToggleExpandNode = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.rightNode:ClearTween()
    UIUtils.PlayAnimationAndToggleActive(self.view.rightNode, isOn)
    self.view.btnExpand.gameObject:SetActive(not isOn)
    if not isOn then
        Notify(MessageConst.CHAR_INFO_CANCEL_TALENT_SELECT)

        self.m_isSkillExpanding = false

        self.m_curNodeId = ''
        self.m_curSkillId = ''
        self.m_refreshCostFunc = nil
        self.m_curConditionIdx = 0
        self.m_cachedSkillGroupCfg = nil
        self.m_cachedCharInstId = 0
        self.m_cachedCurSkillLv = 0
        self.m_cachedIsMaxLv = false
    end
    InputManagerInst:ToggleGroup(self.view.skillUpgrade.scrollInputGroup.groupId, not isOn)
    InputManagerInst:ToggleGroup(self.view.skillUpgradeAfter.scrollInputGroup.groupId, isOn)
end

CharInfoTalentUpgradeCtrl._ExternalExitExpandNode = HL.Method() << function(self)

    self:_StartCoroutine(function()
        coroutine.waitForRenderDone()
        coroutine.wait(0.3)
        self:_ToggleExpandNode(false)
    end)
end

CharInfoTalentUpgradeCtrl._TryUnlockTalentNode = HL.Method(HL.Number, HL.String) << function(self, charInstId, nodeId)
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)

    if not self:_CheckIfRequiredItemEnough(charInst.templateId, nodeId) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_TALENT_UPGRADE_ITEM_NOT_ENOUGH)
        return
    end

    if not self:_CheckIfGoldEnough(charInst.templateId, nodeId) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_TALENT_UPGRADE_GOLD_NOT_ENOUGH)
        return
    end

    if not self:_CheckIfFriendshipEnough(charInst, nodeId) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_TALENT_UPGRADE_FRIENDSHIP_NOT_ENOUGH)
        return
    end

    local isTrail = charInst.charType == GEnums.CharType.Trial
    if isTrail then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_TALENT_UPGRADE_FORBID)
        return
    end

    GameInstance.player.charBag:UnlockTalentNode(charInstId, nodeId)
end

CharInfoTalentUpgradeCtrl._CheckIfRequiredItemEnough = HL.Method(HL.String, HL.String).Return(HL.Boolean) << function(self, templateId, nodeId)
    local nodeCfg = CharInfoUtils.getTalentNodeCfg(templateId, nodeId)

    for _, item in pairs(nodeCfg.requiredItem) do
        local storageCount = Utils.getItemCount(item.id, true)
        if storageCount < item.count then
            return false
        end
    end

    return true
end

CharInfoTalentUpgradeCtrl._CheckIfGoldEnough = HL.Method(HL.String, HL.String).Return(HL.Boolean) << function(self, templateId, nodeId)
    local nodeCfg = CharInfoUtils.getTalentNodeCfg(templateId, nodeId)
    local goldId = UIConst.INVENTORY_MONEY_IDS[0]
    for i, v in pairs(nodeCfg.requiredItem) do
        if v.id == goldId then
            if v.count > Utils.getItemCount(goldId, true) then
                return false
            end
        end
    end
    return true
end

CharInfoTalentUpgradeCtrl._CheckIfFriendshipEnough = HL.Method(HL.Userdata, HL.String).Return(HL.Boolean) << function(self, charInst, nodeId)
    local nodeCfg = CharInfoUtils.getTalentNodeCfg(charInst.templateId, nodeId)

    if nodeCfg.nodeType == GEnums.TalentNodeType.Attr then
        local attrCfg = nodeCfg.attributeNodeInfo
        local friendshipValue = CSPlayerDataUtil.GetCharFriendshipByInstId(charInst.instId)
        if friendshipValue < attrCfg.favorability then
            return false
        end
    end
    return true
end

CharInfoTalentUpgradeCtrl._RefreshCost = HL.Method(HL.Table) << function(self, args)
    if self.m_refreshCostFunc then
        self.m_refreshCostFunc()
    end
end



CharInfoTalentUpgradeCtrl._InitControllerPlaceHolder = HL.Method(HL.Table) << function(self, args)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder(
        {self.view.inputGroup.groupId, args.inputGroupId})
end

CharInfoTalentUpgradeCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    local arg = {}
    arg.isSkillExpanding = self.m_isSkillExpanding
    arg.curConditionIdx = self.m_curConditionIdx
    return arg
end


HL.Commit(CharInfoTalentUpgradeCtrl)
