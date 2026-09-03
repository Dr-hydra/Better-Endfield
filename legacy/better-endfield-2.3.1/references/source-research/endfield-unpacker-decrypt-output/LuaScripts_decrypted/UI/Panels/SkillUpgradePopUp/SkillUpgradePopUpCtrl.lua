local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SkillUpgradePopUp
SkillUpgradePopUpCtrl = HL.Class('SkillUpgradePopUpCtrl', uiCtrl.UICtrl)







SkillUpgradePopUpCtrl.s_messages = HL.StaticField(HL.Table) << {
}

SkillUpgradePopUpCtrl.m_cells = HL.Field(HL.Forward("UIListCache"))

SkillUpgradePopUpCtrl.m_subDescCellCache = HL.Field(HL.Forward("UIListCache"))


SkillUpgradePopUpCtrl.m_skillUpgradeNodeContainerCellCache = HL.Field(HL.Forward("UIListCache"))

SkillUpgradePopUpCtrl.m_arg = HL.Field(HL.Any)


SkillUpgradePopUpCtrl.OnSkillLevelUpgraded = HL.StaticMethod(HL.Table) << function(arg)
    local ctrl = SkillUpgradePopUpCtrl.AutoOpen(PANEL_ID, arg, true)
    local charInstId, skillGroupId, level = unpack(arg)

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)

    ctrl:ShowSkillUpgrade(charInstId, skillGroupId, level)
end

SkillUpgradePopUpCtrl.OnTalentLevelUpgraded = HL.StaticMethod(HL.Table) << function(arg)
    local charInstId, nodeId = unpack(arg)

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local talentCfg = CharInfoUtils.getTalentNodeCfg(charInst.templateId, nodeId)

    if (talentCfg.nodeType == GEnums.TalentNodeType.CharBreak) or (talentCfg.nodeType == GEnums.TalentNodeType.EquipBreak) then
        return
    end

    local ctrl = SkillUpgradePopUpCtrl.AutoOpen(PANEL_ID, arg, true)
    ctrl:ShowTalentUpgrade(charInstId, nodeId)
end


SkillUpgradePopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    local charInstId, nodeId = unpack(arg)
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    Utils.triggerVoice("chrup_skill", charInst.templateId)

    self.m_arg = arg

    self.view.btnClose.onClick:RemoveAllListeners()
    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    UIUtils.bindHyperlinkPopup(self, "SkillUpgradePopUp", self.view.inputGroup.groupId)

    self.m_skillUpgradeNodeContainerCellCache = UIUtils.genCellCache(self.view.skillUpgradeNode.container)
    self:_ResetPopUpPanel()
end

SkillUpgradePopUpCtrl.OnShow = HL.Override() << function(self)
    AudioAdapter.PostEvent("au_ui_btn_skill_levelup_popup")
end

SkillUpgradePopUpCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.ON_CLOSE_SKILL_UPGRADE_POPUP, self.m_arg)
end

SkillUpgradePopUpCtrl.ShowSkillUpgrade = HL.Method(HL.Int, HL.String, HL.Number)
    << function(self, charInstId, skillGroupId, curSkillLv)
    AudioAdapter.PostEvent("Au_UI_Popup_SkillUpgradePopUpPanel_Open")

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local skillGroupCfg = CharInfoUtils.getSkillGroupCfg(charInst.templateId, skillGroupId)
    local skillUpgradeNode = self.view.skillUpgradeNode

    skillUpgradeNode.gameObject:SetActive(true)
    skillUpgradeNode.levelBefore.text = curSkillLv - 1
    skillUpgradeNode.levelCur.text = curSkillLv
    skillUpgradeNode.polygonBefore:InitElitePolygon(curSkillLv - 1 - UIConst.CHAR_MAX_SKILL_NORMAL_LV)
    skillUpgradeNode.polygonCur:InitElitePolygon(curSkillLv - UIConst.CHAR_MAX_SKILL_NORMAL_LV)

    local isElite = curSkillLv - 1 >= UIConst.CHAR_MAX_SKILL_NORMAL_LV
    skillUpgradeNode.polygonBefore.view.gameObject:SetActive(isElite)
    skillUpgradeNode.polygonCur.view.gameObject:SetActive(isElite)
    skillUpgradeNode.levelBefore.gameObject:SetActive(not isElite)
    skillUpgradeNode.levelCur.gameObject:SetActive(not isElite)
    skillUpgradeNode.levelDeco.gameObject:SetActive(not isElite)
    skillUpgradeNode.levelDeco2.gameObject:SetActive(not isElite)

    local hasBothConditions = CharInfoUtils.hasBothSkillGroupConditions(skillGroupCfg)
    local containerCount = hasBothConditions and 2 or 1
    self.m_skillUpgradeNodeContainerCellCache:Refresh(containerCount, function(cell, index)
        local conditionIdx = hasBothConditions and index or 0
        self:_SetupUISkillUpgradeNodeContainer(cell, charInst, skillGroupId, curSkillLv, conditionIdx)
    end)


    skillUpgradeNode.skillNameTxt.gameObject:SetActive(hasBothConditions)
    skillUpgradeNode.skillNameTxt.text = skillGroupCfg.name
    self:_UpdateSkillUpgradeScrollState()
end


SkillUpgradePopUpCtrl._SetupUISkillUpgradeNodeContainer = HL.Method(HL.Any, HL.Any, HL.String, HL.Number, HL.Number) << function(self, container, charInst, skillGroupId, curSkillLv, conditionIdx)
    local skillGroupCfg = CharInfoUtils.getSkillGroupCfg(charInst.templateId, skillGroupId)

    local switchLuaRef = container.btnSkill.view.stanceSwitchSkill

    if conditionIdx == 0 then

        container.name.text = skillGroupCfg.name
        switchLuaRef.gameObject:SetActive(false)
    end
    container.btnSkill:InitCharInfoSkillButtonNew(charInst, skillGroupCfg.skillGroupType)
    container.btnSkill.view.rank.gameObject:SetActive(false)
    container.btnSkill.view.eliteNode.gameObject:SetActive(false)


    if conditionIdx == 1 or conditionIdx == 2 then
        local displayData = CharInfoUtils.getSkillGroupConditionDisplayDataByIndex(charInst.instId, skillGroupCfg, conditionIdx)
        local icon = displayData.icon
        container.btnSkill.view.skillIcon:LoadSprite(UIConst.UI_SPRITE_SKILL_ICON, icon and icon or skillGroupCfg.icon)

        container.name.text = displayData.name
        switchLuaRef.gameObject:SetActive(true)
        switchLuaRef.yangImg.gameObject:SetActive(conditionIdx == 1)
        switchLuaRef.yinImg.gameObject:SetActive(conditionIdx == 2)
    end

    if container.subDescCellCache == nil then
        container.subDescCellCache = UIUtils.genCellCache(container.cell)
    end

    local skillDescNameList, skillDescList
    if conditionIdx == 1 or conditionIdx == 2 then
        local selectedConditionId = conditionIdx == 1 and skillGroupCfg.conditionId1 or skillGroupCfg.conditionId2
        skillDescNameList, skillDescList = CharInfoUtils.getSkillGroupSubDescList(
            charInst.templateId, skillGroupId, curSkillLv, charInst.instId, selectedConditionId)
    else

        skillDescNameList, skillDescList = CharInfoUtils.getSkillGroupSubDescList(charInst.templateId, skillGroupId, curSkillLv)
    end
    container.subDescCellCache:Refresh(#skillDescNameList, function(cell, index)
        if skillDescNameList[index] == nil then
            cell.gameObject:SetActive(false)
            return
        end

        local subDescName = skillDescNameList[index]
        local subDesc = skillDescList[index]
        cell.image.enabled = index % 2 ~= 0

        cell.subDescName.text = subDescName
        cell.subDesc.text = subDesc
    end)
end

SkillUpgradePopUpCtrl._UpdateSkillUpgradeScrollState = HL.Method() << function(self)
    local scrollSkill = self.view.skillUpgradeNode.scrollSkill
    if IsNull(scrollSkill) or IsNull(scrollSkill.content) or IsNull(scrollSkill.viewport) then
        return
    end

    LayoutRebuilder.ForceRebuildLayoutImmediate(scrollSkill.content)
    local canScroll = scrollSkill.content.rect.height > scrollSkill.viewport.rect.height + 1
    scrollSkill.enabled = canScroll
    if canScroll then
        scrollSkill.verticalNormalizedPosition = 1
    end

    self.view.skillUpgradeNode.keyHint.overrideValidState = canScroll
        and CS.Beyond.UI.CustomUIStyle.OverrideValidState.None
        or CS.Beyond.UI.CustomUIStyle.OverrideValidState.ForceNotValid
end

SkillUpgradePopUpCtrl.ShowTalentUpgrade = HL.Method(HL.Number, HL.String) << function(self, charInstId, nodeId)
    AudioAdapter.PostEvent("Au_UI_Popup_TalentUpgradePopUpPanel_Open")

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local talentCfg = CharInfoUtils.getTalentNodeCfg(charInst.templateId, nodeId)

    if talentCfg.nodeType == GEnums.TalentNodeType.PassiveSkill then
        self:_ShowPassiveSkillUpgrade(charInstId, nodeId)
    elseif talentCfg.nodeType == GEnums.TalentNodeType.Attr then
        self:_ShowAttributeUpgrade(charInstId, nodeId)
    elseif talentCfg.nodeType == GEnums.TalentNodeType.FactorySkill then
        self:_ShowShipSkillUpgrade(charInstId, nodeId)
    end
end

SkillUpgradePopUpCtrl._ShowShipSkillUpgrade = HL.Method(HL.Number, HL.String) << function(self, charInstId, nodeId)
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)

    local curNodeCfg = CharInfoUtils.getTalentNodeCfg(charInst.templateId, nodeId)
    local curSkillInfo = curNodeCfg.factorySkillNodeInfo

    local beforeNodeCfg = CharInfoUtils.getShipSkillTalentNodeByIndex(charInst.templateId, curSkillInfo.index, curSkillInfo.level - 1)
    local hasSkillBefore = beforeNodeCfg ~= nil

    local skillId = CharInfoUtils.getShipSkillIdByTalentNodeId(charInst.templateId, nodeId)
    local shipSkillCfgCur = Tables.spaceshipSkillTable[skillId]

    local shipSkillNode = self.view.shipSkillNode
    shipSkillNode.gameObject:SetActive(true)
    shipSkillNode.beforeNode.gameObject:SetActive(hasSkillBefore)
    shipSkillNode.curNode.gameObject:SetActive(true)

    if hasSkillBefore then
        local shipSkillCfgBefore = Tables.spaceshipSkillTable[CharInfoUtils.getShipSkillIdByTalentNodeId(charInst.templateId, beforeNodeCfg.nodeId)]
        shipSkillNode.shipSkillPostfixBefore.text = shipSkillCfgBefore.skillNamePostfix
    end


    shipSkillNode.shipSkillPostfixCur.text = shipSkillCfgCur.skillNamePostfix

    local shipSkillId = CharInfoUtils.getShipSkillIdByTalentNodeId(charInst.templateId, nodeId)
    local curSkillCfg = CharInfoUtils.getShipSkillCfg(shipSkillId)
    shipSkillNode.name.text = curSkillCfg.talentName
    shipSkillNode.desc.text = curSkillCfg.desc
    shipSkillNode.icon:LoadSprite(UIConst.UI_SPRITE_SS_SKILL_ICON, curSkillCfg.icon)
end

SkillUpgradePopUpCtrl._ShowPassiveSkillUpgrade = HL.Method(HL.Number, HL.String) << function(self, charInstId, nodeId)
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)

    local curNodeCfg = CharInfoUtils.getTalentNodeCfg(charInst.templateId, nodeId)
    local curSkillInfo = curNodeCfg.passiveSkillNodeInfo

    local beforeNodeCfg = CharInfoUtils.getPassiveSkillTalentNodeByIndex(charInst.templateId, curSkillInfo.index, curSkillInfo.level - 1)
    local hasSkillBefore = beforeNodeCfg ~= nil

    local passiveSkillNode = self.view.passiveSkillNode
    passiveSkillNode.gameObject:SetActive(true)
    passiveSkillNode.beforeNode.gameObject:SetActive(hasSkillBefore)
    passiveSkillNode.curNode.gameObject:SetActive(true)

    if hasSkillBefore then
        local beforeSkillInfo = beforeNodeCfg.passiveSkillNodeInfo
        passiveSkillNode.stageGroupBefore:InitStageLevelCellGroup(beforeSkillInfo.level)
    end

    passiveSkillNode.stageGroupCur:InitStageLevelCellGroup(curSkillInfo.level)

    passiveSkillNode.name.text = curSkillInfo.name
    local nodeDesc = CS.Beyond.Gameplay.TalentUtil.GetTalentNodeDescription(charInst.templateId, curNodeCfg.nodeId, charInst.instId)
    passiveSkillNode.desc:SetAndResolveTextStyle(nodeDesc)
    passiveSkillNode.icon:LoadSprite(UIConst.UI_SPRITE_SKILL_ICON, curSkillInfo.iconId)
    passiveSkillNode.scrollView.verticalNormalizedPosition = 1
end

SkillUpgradePopUpCtrl._ShowAttributeUpgrade = HL.Method(HL.Number, HL.String) << function(self, charInstId, nodeId)
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local nodeCfg = CharInfoUtils.getTalentNodeCfg(charInst.templateId, nodeId)
    local attrNodeInfo = nodeCfg.attributeNodeInfo
    local attrType = CharInfoUtils.getTalentAttributeNodeDisplayAttrType(charInst.templateId, attrNodeInfo)
    local attrKey = Const.ATTRIBUTE_TYPE_2_ATTRIBUTE_DATA_KEY[attrType]

    local attributeNode = self.view.attributeNode
    attributeNode.gameObject:SetActive(true)
    attributeNode.icon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, UIConst.UI_ATTRIBUTE_ICON_PREFIX .. attrKey)
    attributeNode.name.text = attrNodeInfo.title
    attributeNode.desc.text = attrNodeInfo.desc
end


SkillUpgradePopUpCtrl._ResetPopUpPanel = HL.Method() << function(self)
    self.view.skillUpgradeNode.gameObject:SetActive(false)

    self.view.passiveSkillNode.gameObject:SetActive(false)
    self.view.attributeNode.gameObject:SetActive(false)
    self.view.shipSkillNode.gameObject:SetActive(false)
end

HL.Commit(SkillUpgradePopUpCtrl)
