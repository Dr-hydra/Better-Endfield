local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharFormationSkillTips
local SCREEN_SAFE_PADDING = 20
local CONTROLLER_BOTTOM_PADDING = 60
local MIN_DESC_SCROLL_HEIGHT = 1

CharFormationSkillTipsCtrl = HL.Class('CharFormationSkillTipsCtrl', uiCtrl.UICtrl)


CharFormationSkillTipsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHAR_INFO_CLOSE_SKILL_TIP] = '_CloseSkillTips',
    [MessageConst.ON_CHAR_DECK_ATTR_CHANGED] = '_OnCharDeckAttrChanged',
}

CharFormationSkillTipsCtrl.m_curSkillId = HL.Field(HL.String) << ""

CharFormationSkillTipsCtrl.m_cachedArgs = HL.Field(HL.Table)

CharFormationSkillTipsCtrl.m_hasCachedTipsPositionArgs = HL.Field(HL.Boolean) << false
CharFormationSkillTipsCtrl.m_cachedTipsTransform = HL.Field(HL.Userdata)
CharFormationSkillTipsCtrl.m_cachedTipsPosType = HL.Field(HL.Any)

CharFormationSkillTipsCtrl.m_extraInfoCellCache = HL.Field(HL.Forward("UIListCache"))




CharFormationSkillTipsCtrl.m_curConditionIdx = HL.Field(HL.Number) << 0





CharFormationSkillTipsCtrl.m_cachedSkillGroupCfg = HL.Field(HL.Any)
CharFormationSkillTipsCtrl.m_cachedCharInstId = HL.Field(HL.Number) << 0

CharFormationSkillTipsCtrl.m_cachedSkillLevel = HL.Field(HL.Number) << 0

CharFormationSkillTipsCtrl.ShowCharSkillTip = HL.StaticMethod(HL.Table) << function(args)
    local isShowing = UIManager:IsShow(PANEL_ID)
    local self = UIManager:AutoOpen(PANEL_ID)
    if isShowing then
        if not self:IsPlayingAnimationOut() then
            self:PlayAnimationOutWithCallback(function()
                self:PlayAnimationIn()
                self:_ShowTips(args)
            end)
        else
            self.m_cachedArgs = args
        end
    else
        self:_ShowTips(args)
    end
end

CharFormationSkillTipsCtrl._ShowTips = HL.Method(HL.Table) << function(self, args)
    Notify(MessageConst.CHAR_INFO_CLOSE_ATTR_TIP)
    self:_InitActionEvent()


    self.m_curConditionIdx = 0
    self.m_cachedSkillGroupCfg = nil
    self.m_cachedCharInstId = 0
    self.m_cachedSkillLevel = 0
    self.view.stanceSwitchSkillNode.gameObject:SetActive(false)
    self.view.stanceSwitchSkillDesc.gameObject:SetActive(false)

    self.view.skillInfoNode.gameObject:SetActive(not args.isPassiveSkill)
    self.view.passiveSkillInfoNode.gameObject:SetActive(args.isPassiveSkill)
    if args.isPassiveSkill then
        self:_RefreshPassiveSkillTip(args)
    else
        self:_RefreshSkillGroupTip(args)
    end

    self.view.skillTipsNode.gameObject:SetActive(true)
    self.view.autoCloseArea:ChangeEnableCloseActionOnController(args.enableCloseActionOnController == true)

    self:_CacheTipsPositionArgs(args)
    self:_UpdateDescScrollLayoutAndPosition()
    self:_StartCoroutine(function()
        coroutine.step()
        self:_UpdateDescScrollLayoutAndPosition()
    end)
    Notify(MessageConst.ON_CHAR_INFO_SHOW_SKILL_TIP)
end

CharFormationSkillTipsCtrl._CacheTipsPositionArgs = HL.Method(HL.Table) << function(self, args)
    self.m_hasCachedTipsPositionArgs = true
    self.m_cachedTipsTransform = args.transform
    self.m_cachedTipsPosType = args.tipPosType
end

CharFormationSkillTipsCtrl._UpdateTipsPosition = HL.Method() << function(self)
    if not self.m_hasCachedTipsPositionArgs then
        return
    end
    UIUtils.updateTipsPosition(self.view.content, self.m_cachedTipsTransform, self.view.rectTransform, self.uiCamera,
        self.m_cachedTipsPosType, DeviceInfo.usingController and { bottom = CONTROLLER_BOTTOM_PADDING } or nil)
    self:_ConvertContentPositionFromCanvasToParent()
end



CharFormationSkillTipsCtrl._ConvertContentPositionFromCanvasToParent = HL.Method() << function(self)
    local content = self.view.content
    if IsNull(content) or IsNull(content.parent) then
        return
    end

    local canvasPos = content.anchoredPosition
    local worldPos = self.view.rectTransform:TransformPoint(Vector3(canvasPos.x, canvasPos.y, 0))
    local parentLocalPos = content.parent:InverseTransformPoint(worldPos)
    content.localPosition = Vector3(parentLocalPos.x, parentLocalPos.y, content.localPosition.z)
end

CharFormationSkillTipsCtrl._UpdateDescScrollLayoutAndPosition = HL.Method() << function(self)
    self:_UpdateDescScrollLayout()
    self:_UpdateTipsPosition()
end

CharFormationSkillTipsCtrl._RefreshSkillGroupTip = HL.Method(HL.Table) << function(self, args)
    local charInstId = args.charInstId
    local skillGroupType = args.skillGroupType
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local skillGroupCfg = CharInfoUtils.getCharSkillGroupCfgByType(charInst.templateId, skillGroupType)
    local skillInfo = CharInfoUtils.getCharSkillLevelInfoByType(charInst, skillGroupType)

    self.view.skillName.text = skillGroupCfg.name
    self.view.skillTypeName.text = UIConst.CHAR_INFO_SKILL_GROUP_TYPE_TO_TYPE_NAME[skillGroupType]


    local hasBothConditions = CharInfoUtils.hasBothSkillGroupConditions(skillGroupCfg)
    self.view.stanceSwitchSkillNode.gameObject:SetActive(hasBothConditions)
    if hasBothConditions then
        self.m_curConditionIdx = CharInfoUtils.getActiveSkillGroupConditionIdx(charInst.instId, skillGroupCfg) or 1
        self:_RefreshStanceSwitchSkillNode(skillGroupCfg, charInst.instId)

        self.m_cachedSkillGroupCfg = skillGroupCfg
        self.m_cachedCharInstId = charInst.instId
        self.m_cachedSkillLevel = skillInfo.level
    end
    self:_RefreshSkillGroupMainInfo(charInst, skillGroupCfg, skillInfo.level)
    self:_RefreshStanceSwitchSkillUI(charInst.instId, skillGroupCfg)

    local canUpgrade = skillInfo.level < UIConst.CHAR_MAX_SKILL_LV and not args.hideBtnUpgrade
    self.view.btnUpgrade.gameObject:SetActive(canUpgrade)
    self.view.btnUpgrade.onClick:RemoveAllListeners()
    self.view.btnUpgrade.onClick:AddListener(function()
        self:_CloseSkillTips()
        local isOpen, openedPhase = PhaseManager:IsOpen(PhaseId.CharInfo)
        if isOpen then
            Notify(MessageConst.CHAR_INFO_PAGE_CHANGE, {
                pageType = UIConst.CHAR_INFO_PAGE_TYPE.TALENT,
                isFast = true,
                showGlitch = true,
                extraArg = {
                    showSkillGroupType = skillGroupType,
                }
            })
        else
            CharInfoUtils.openCharInfoBestWay({
                initCharInfo = {
                    instId = charInstId,
                    templateId = charInst.templateId,
                    isSingleChar = args.isSingleChar == true,
                },
                pageType = UIConst.CHAR_INFO_PAGE_TYPE.TALENT,
                isFast = true,
                showGlitch = true,
                extraArg = {
                    showSkillGroupType = skillGroupType,
                }
            })
        end
    end)

    local curSkillLv = skillInfo.level
    local isElite = curSkillLv >= UIConst.CHAR_MAX_SKILL_NORMAL_LV
    local showSkillLv = lume.clamp(curSkillLv, 1, UIConst.CHAR_MAX_SKILL_NORMAL_LV)
    local eliteLv = isElite and curSkillLv - UIConst.CHAR_MAX_SKILL_NORMAL_LV or 0

    self.view.rank.text = string.format(Language.LUA_CHAR_INFO_TALENT_SKILL_LEVEL_PREFIX, showSkillLv)
    self.view.elitepolygon.gameObject:SetActive(isElite)
    self.view.elitepolygon:InitElitePolygon(eliteLv)


    self:_RefreshSkillExtraInfo(charInstId, skillGroupCfg, skillInfo.level)
end

CharFormationSkillTipsCtrl._RefreshPassiveSkillTip = HL.Method(HL.Table) << function(self, args)
    local isLock = args.isLock
    local charInstId = args.charInstId
    local nodeIndex = args.skillId
    local nodeLevel = args.skillLevel
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local nextNodeLevel = isLock and nodeLevel or (nodeLevel + 1)
    local talentUpgradeCfg = CharInfoUtils.getPassiveSkillTalentNodeByIndex(charInst.templateId, nodeIndex, nextNodeLevel)
    local talentNodeCfg = CharInfoUtils.getPassiveSkillTalentNodeByIndex(charInst.templateId, nodeIndex, nodeLevel)

    self.m_curSkillId = talentNodeCfg.nodeId

    self.view.skillName.text = talentNodeCfg.passiveSkillNodeInfo.name
    self.view.passiveSkillInfoNode.locked.gameObject:SetActive(isLock)
    self.view.passiveSkillInfoNode.stageLevelCellGroup.view.gameObject:SetActive(not isLock)
    self.view.passiveSkillInfoNode.rank.gameObject:SetActive(not isLock)

    local foundNodeList = CharInfoUtils.getAllPassiveSkillTalentNodeByIndex(charInst.templateId, nodeIndex)
    self.view.passiveSkillInfoNode.stageLevelCellGroup:InitStageLevelCellGroupByPassiveNodeList(charInst.instId, foundNodeList)

    local nodeDesc = CS.Beyond.Gameplay.TalentUtil.GetTalentNodeDescription(charInst.templateId, talentNodeCfg.nodeId, charInst.instId)
    self.view.desc:SetAndResolveTextStyle(nodeDesc)
    self.view.skillTypeName.text = Language.LUA_CHAR_INFO_TALENT_SKILL_NAME
    self.view.elitepolygon.gameObject:SetActive(false)

    local canUpgrade = talentUpgradeCfg ~= nil and not args.hideBtnUpgrade
    self.view.btnUpgrade.gameObject:SetActive(canUpgrade)
    self.view.btnUpgrade.onClick:RemoveAllListeners()
    self.view.btnUpgrade.onClick:AddListener(function()
        self:_CloseSkillTips()

        if PhaseManager:IsOpen(PhaseId.CharInfo) then
            Notify(MessageConst.CHAR_INFO_PAGE_CHANGE, {
                pageType = UIConst.CHAR_INFO_PAGE_TYPE.TALENT,
                isFast = true,
                showGlitch = true,
                extraArg = {
                    showPassiveSkillId = talentNodeCfg.nodeId,
                }
            })
        else
            CharInfoUtils.openCharInfoBestWay({
                initCharInfo = {
                    instId = charInstId,
                    templateId = charInst.templateId,
                    isSingleChar = args.isSingleChar == true,
                },
                pageType = UIConst.CHAR_INFO_PAGE_TYPE.TALENT,
                isFast = true,
                showGlitch = true,
                extraArg = {
                    showPassiveSkillId = talentNodeCfg.nodeId,
                }
            })
        end
    end)
    self.m_extraInfoCellCache:Refresh(0)
end

CharFormationSkillTipsCtrl._RefreshSkillGroupMainInfo = HL.Method(HL.Any, HL.Userdata, HL.Number) << function(self, charInst, skillGroupCfg, curSkillLv)

    local postDesc = CharInfoUtils.generateSkillGroupConditionPostDescText(
        charInst.instId, skillGroupCfg, self.m_curConditionIdx)

    local desc = Utils.SkillUtil.GetSkillGroupDescription(charInst.templateId, skillGroupCfg.skillGroupId, curSkillLv, charInst.instId)
    local combinedDesc = table.concat({desc, postDesc}, "\n"):gsub("^\n+", ""):gsub("\n+$", "")
    self.view.desc:SetAndResolveTextStyle(combinedDesc)
end


CharFormationSkillTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_extraInfoCellCache = UIUtils.genCellCache(self.view.extraInfo)
    if DeviceInfo.usingController then
        self.view.controllerHintBarCell:InitControllerHintBarCell({ groupIds = {self.view.inputGroup.groupId}, }, true)
    end
    UIUtils.bindHyperlinkPopup(self, "charFormationSkillTips", self.view.inputGroup.groupId)
end

CharFormationSkillTipsCtrl._GetMaxPanelHeight = HL.Method().Return(HL.Number) << function(self)
    local maxHeight = self.view.config.MAX_FIT_HEIGHT
    local canvasHeight = self.view.rectTransform.rect.height
    local safePadding = SCREEN_SAFE_PADDING + (DeviceInfo.usingController and CONTROLLER_BOTTOM_PADDING or 0)
    return math.max(MIN_DESC_SCROLL_HEIGHT, math.min(maxHeight, canvasHeight - safePadding))
end

CharFormationSkillTipsCtrl._UpdateDescScrollLayout = HL.Method() << function(self)
    if IsNull(self.view.scrollRect) or IsNull(self.view.scrollRectLayoutElement) then
        return
    end

    local descContent = self.view.scrollRect.content
    if IsNull(descContent) then
        return
    end

    LayoutRebuilder.ForceRebuildLayoutImmediate(descContent)
    local descContentHeight = descContent.rect.height
    self.view.scrollRectLayoutElement.preferredHeight = descContentHeight

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.scrollContent)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.content)

    local fixedHeight = self.view.content.rect.height - descContentHeight
    local descMaxHeight = math.max(MIN_DESC_SCROLL_HEIGHT, self:_GetMaxPanelHeight() - fixedHeight)
    local descViewHeight = math.min(descContentHeight, descMaxHeight)
    local canScroll = descContentHeight > descViewHeight + 1

    self.view.scrollRectLayoutElement.preferredHeight = descViewHeight
    self.view.scrollRect.enabled = canScroll
    self.view.keyHint.overrideValidState = canScroll
        and CS.Beyond.UI.CustomUIStyle.OverrideValidState.None
        or CS.Beyond.UI.CustomUIStyle.OverrideValidState.ForceNotValid
    self.view.scrollRect.verticalNormalizedPosition = 1


    if canScroll then
        self.view.viewport.hgSoftness = Vector4(0, 40, 0, 0)
        self.view.viewportStateController:SetState("canScroll")
    else
        self.view.viewport.hgSoftness = Vector4(0, 0, 0, 0)
        self.view.viewportStateController:SetState("cannotScroll")
    end

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.scrollContent)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.content)
end

CharFormationSkillTipsCtrl._InitActionEvent = HL.Method() << function(self)
    self.view.autoCloseArea.onTriggerAutoClose:RemoveAllListeners()
    self.view.autoCloseArea.onTriggerAutoClose:AddListener(function()
        self:_CloseSkillTips()
    end)
end



CharFormationSkillTipsCtrl._RefreshStanceSwitchSkillNode = HL.Method(HL.Userdata, HL.Number) << function(self, skillGroupCfg, charInstId)
    local node = self.view.stanceSwitchSkillNode
    node.tab1Toggle.titleTxt:SetAndResolveTextStyle(skillGroupCfg.conditionName1)
    node.tab2Toggle.titleTxt:SetAndResolveTextStyle(skillGroupCfg.conditionName2)
    local tab1Icon = CharInfoUtils.generateSkillGroupConditionIcon(charInstId, skillGroupCfg, 1)
    local tab2Icon = CharInfoUtils.generateSkillGroupConditionIcon(charInstId, skillGroupCfg, 2)
    node.tab1Toggle.buttonSkillNew.view.skillIcon:LoadSprite(UIConst.UI_SPRITE_SKILL_ICON, tab1Icon and tab1Icon or skillGroupCfg.icon)
    node.tab2Toggle.buttonSkillNew.view.skillIcon:LoadSprite(UIConst.UI_SPRITE_SKILL_ICON, tab2Icon and tab2Icon or skillGroupCfg.icon)
    local bgColor = CharInfoUtils.getCharInfoSkillGroupBgColor(skillGroupCfg)
    node.tab1Toggle.buttonSkillNew.view.bgSkillColor2.color = bgColor
    node.tab2Toggle.buttonSkillNew.view.bgSkillColor2.color = bgColor


    node.tab1Toggle.toggle.onValueChanged:RemoveAllListeners()
    node.tab2Toggle.toggle.onValueChanged:RemoveAllListeners()

    node.tab1Toggle.toggle.isOn = self.m_curConditionIdx == 1
    node.tab2Toggle.toggle.isOn = self.m_curConditionIdx == 2

    node.tab1Toggle.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:_OnStanceTabChanged(1, charInstId, skillGroupCfg)
        end
    end)
    node.tab2Toggle.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:_OnStanceTabChanged(2, charInstId, skillGroupCfg)
        end
    end)


    local activeIndex = CharInfoUtils.getActiveSkillGroupConditionIdx(charInstId, skillGroupCfg)
    node.tab1Toggle.nowNode.gameObject:SetActive(activeIndex == 1)
    node.tab2Toggle.nowNode.gameObject:SetActive(activeIndex == 2)
end




CharFormationSkillTipsCtrl._OnStanceTabChanged = HL.Method(HL.Number, HL.Number, HL.Userdata) << function(self, conditionIdx, charInstId, skillGroupCfg)
    self.m_curConditionIdx = conditionIdx
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    if charInst ~= nil then
        local skillInfo = CharInfoUtils.getCharSkillLevelInfoByType(charInst, skillGroupCfg.skillGroupType)
        if skillInfo ~= nil then
            self:_RefreshSkillGroupMainInfo(charInst, skillGroupCfg, skillInfo.level)
        end
    end
    self:_RefreshStanceSwitchSkillUI(charInstId, skillGroupCfg)
    self:_RefreshSkillExtraInfo(charInstId, skillGroupCfg, self.m_cachedSkillLevel)
    self:_UpdateDescScrollLayoutAndPosition()

    self.animationWrapper:Play("charformationskilltips_refresh")
end






CharFormationSkillTipsCtrl._RefreshSkillExtraInfo = HL.Method(HL.Number, HL.Userdata, HL.Number) << function(self, charInstId, skillGroupCfg, skillLevel)
    local extraInfoList = {}
    if skillGroupCfg.skillGroupType ~= GEnums.SkillGroupType.NormalAttack then
        local skillId = skillGroupCfg.skillIdList[0]
        local selectedConditionId
        if self.m_curConditionIdx == 1 then
            selectedConditionId = skillGroupCfg.conditionId1
        elseif self.m_curConditionIdx == 2 then
            selectedConditionId = skillGroupCfg.conditionId2
        end
        extraInfoList = CharInfoUtils.getSkillExtraInfoList(
            skillId, skillLevel, charInstId, selectedConditionId)
    end
    self.m_extraInfoCellCache:Refresh(#extraInfoList, function(cell, index)
        local info = extraInfoList[index]
        cell.title.text = info.name
        cell.num.text = info.value
    end)
end



CharFormationSkillTipsCtrl._RefreshStanceSwitchSkillUI = HL.Method(HL.Number, HL.Userdata) << function(self, charInstId, skillGroupCfg)
    local desc, isActive, originText = CharInfoUtils.generateSkillGroupConditionDescText(charInstId, skillGroupCfg, self.m_curConditionIdx)
    if desc then
        self.view.stanceSwitchSkillDesc.text:SetAndResolveTextStyle(isActive and originText or desc)
        self.view.stanceSwitchSkillDesc.stateController:SetState(isActive and "Active" or "Inactive")
        self.view.stanceSwitchSkillDesc.gameObject:SetActive(true)
    else
        self.view.stanceSwitchSkillDesc.gameObject:SetActive(false)
    end
end

CharFormationSkillTipsCtrl._CloseSkillTips = HL.Method() << function(self)
    if UIManager:IsShow(PANEL_ID) then
        Notify(MessageConst.ON_CHAR_INFO_CLOSE_SKILL_TIP)
        self:PlayAnimationOutAndClose()
    end
end





CharFormationSkillTipsCtrl._OnCharDeckAttrChanged = HL.Method(HL.Table) << function(self, arg)
    local instId = unpack(arg)
    if instId ~= self.m_cachedCharInstId or not self.m_cachedSkillGroupCfg then
        return
    end
    self:_RefreshStanceSwitchSkillNode(self.m_cachedSkillGroupCfg, instId)
    self:_RefreshStanceSwitchSkillUI(instId, self.m_cachedSkillGroupCfg)
    self:_UpdateDescScrollLayoutAndPosition()
end

HL.Commit(CharFormationSkillTipsCtrl)
