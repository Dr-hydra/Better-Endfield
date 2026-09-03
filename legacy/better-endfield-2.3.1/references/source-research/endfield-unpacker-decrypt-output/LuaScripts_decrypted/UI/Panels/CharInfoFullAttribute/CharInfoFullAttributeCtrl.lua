
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoFullAttribute
CharInfoFullAttributeCtrl = HL.Class('CharInfoFullAttributeCtrl', uiCtrl.UICtrl)







CharInfoFullAttributeCtrl.s_messages = HL.StaticField(HL.Table) << {
}

do
    CharInfoFullAttributeCtrl.m_charInfo = HL.Field(HL.Table)

    CharInfoFullAttributeCtrl.m_fcAttrCellCache = HL.Field(HL.Forward("UIListCache"))

    CharInfoFullAttributeCtrl.m_scMainAttrCellCache = HL.Field(HL.Forward("UIListCache"))

    CharInfoFullAttributeCtrl.m_scSubAttrCellCache = HL.Field(HL.Forward("UIListCache"))

    CharInfoFullAttributeCtrl.m_singleFcAttrHintShowing = HL.Field(HL.Boolean) << false

    CharInfoFullAttributeCtrl.m_onClose = HL.Field(HL.Function)
end



CharInfoFullAttributeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local charInfo = arg.charInfo or CharInfoUtils.getLeaderCharInfo()
    local onClose = arg.onClose
    local charCfg = Tables.characterTable[charInfo.templateId]

    self.m_onClose = onClose
    self.m_charInfo = charInfo
    self.m_fcAttrCellCache = UIUtils.genCellCache(self.view.fcAttributeCell)
    self.m_scMainAttrCellCache = UIUtils.genCellCache(self.view.scMainAttributeCell)
    self.m_scSubAttrCellCache = UIUtils.genCellCache(self.view.scSubAttributeCell)

    self.view.titleTxt.text = string.format(Language.LUA_CHAR_INFO_FULL_ATTR_TITLE_FORMAT, charCfg.name)

    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.emptyBtn.onClick:AddListener(function()
        UIUtils.PlayAnimationAndToggleActive(self.view.scAttributeHintNode.animationWrapper, false)
        self.view.emptyBtn.gameObject:SetActive(false)
        if DeviceInfo.usingController then
            self.view.detailInputGroup.enabled = true
        end
    end)

    self.view.scAttributeHintNode.gameObject:SetActive(false)
    self:_RefreshCharBasicInfo(charInfo)
    self:_RefreshCharAttributeInfo(charInfo)
    self:_InitCharInfoFullAttributeController()
    UIUtils.bindHyperlinkPopup(self, "charInfoFullAttr", self.view.inputGroup.groupId)
end

CharInfoFullAttributeCtrl.OnClose = HL.Override() << function(self)
    if self.m_onClose then
        self.m_onClose()
    end
end

CharInfoFullAttributeCtrl._RefreshCharBasicInfo = HL.Method(HL.Table) << function(self, charInfo)
    local templateId = charInfo.templateId
    local charCfg = Tables.characterTable[templateId]

    self.view.nameText.text = charCfg.name
    self.view.charBg:LoadSprite(UIConst.UI_SPRITE_CHAR_INFO, UIConst.UI_CHAR_INFO_CHAR_BG_PREFIX .. templateId)

    local professionCfg = Tables.charProfessionTable[charCfg.profession]
    self.view.professionText.text = professionCfg.name
    self.view.professionIcon:LoadSprite(UIConst.UI_SPRITE_CHAR_PROFESSION, CharInfoUtils.getCharProfessionIconName(charCfg.profession))
end

CharInfoFullAttributeCtrl._RefreshCharAttributeInfo = HL.Method(HL.Table) << function(self, charInfo)
    local charInstId = charInfo.instId
    local charCfg = Tables.characterTable[charInfo.templateId]
    local fcAttrShowList, scMainAttrShowList, scSubAttrShowList = CharInfoUtils.generateCharInfoBasicAttrShowInfo(charInstId)

    self.m_fcAttrCellCache:Refresh(#fcAttrShowList, function(cell, index)
        local showInfo = fcAttrShowList[index]
        local isMainAttrType = AttributeUtils.CheckIsMainAttr(showInfo.attributeType, charInfo.templateId)
        local isSubAttrType = AttributeUtils.CheckIsSubAttr(showInfo.attributeType, charInfo.templateId)

        self:_RefreshFCAttrCell(cell, index, showInfo, isMainAttrType, isSubAttrType)
    end)

    self.m_scMainAttrCellCache:Refresh(#scMainAttrShowList, function(cell, index)
        local showInfo = scMainAttrShowList[index]

        self:_RefreshSCMainAttrCell(cell, showInfo, charInfo)
    end)

    self.m_scSubAttrCellCache:Refresh(#scSubAttrShowList, function(cell, index)
        local showInfo = scSubAttrShowList[index]

        self:_RefreshSCSubAttrCell(#scSubAttrShowList, index, cell, showInfo, charInfo)
    end)
end

CharInfoFullAttributeCtrl._RefreshFCAttrCell = HL.Method(HL.Any, HL.Number, HL.Table, HL.Boolean, HL.Boolean) << function(self, cell, index, showInfo, isMainAttrType, isSubAttrType)
    cell.fcMainAttr.gameObject:SetActive(isMainAttrType)
    cell.fcSubAttr.gameObject:SetActive(isSubAttrType)
    cell.fcExtraAttr.gameObject:SetActive(not isMainAttrType and not isSubAttrType)

    local realAttrCell = isMainAttrType and cell.fcMainAttr or isSubAttrType and cell.fcSubAttr or cell.fcExtraAttr
    realAttrCell.mainAttributeHint.gameObject:SetActive(isMainAttrType)
    realAttrCell.subAttributeHint.gameObject:SetActive(isSubAttrType)
    realAttrCell.attributeText.text = showInfo.showName
    realAttrCell.valueText.text = showInfo.showValue
    realAttrCell.attributeIconBig:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, showInfo.bigIconName)
    realAttrCell.attributeIcon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, showInfo.iconName)
    realAttrCell.button.customBindingViewLabelText = Language["ui_char_info_full_attribute_tips_btn"]
    realAttrCell.button.onClick:RemoveAllListeners()
    realAttrCell.button.onClick:AddListener(function()
        if DeviceInfo.usingController then
            if self.m_singleFcAttrHintShowing then
                return
            end
            self.view.middleInputGroup.enabled = false
            self.view.controllerCloseBtn.gameObject:SetActive(true)
        end
        self.m_singleFcAttrHintShowing = true
        Notify(MessageConst.CHAR_INFO_SHOW_FC_ATTR_HINT, {
            key = index,
            transform = realAttrCell.transform,
            attributeInfo = showInfo,
            charTemplateId = self.m_charInfo.templateId,
            charInstId = self.m_charInfo.instId,
            onHintClose = function()
                if DeviceInfo.usingController then
                    self.m_singleFcAttrHintShowing = false
                    self.view.middleInputGroup.enabled = true
                    self.view.controllerCloseBtn.gameObject:SetActive(false)
                end
            end
        })
    end)

    if DeviceInfo.usingController and index == 1 then
        self:SetNaviTarget(realAttrCell.button)
    end
end

CharInfoFullAttributeCtrl._RefreshSCMainAttrCell = HL.Method(HL.Any, HL.Table, HL.Table) << function(self, cell, showInfo, charInfo)
    local hasIconName = not string.isEmpty(showInfo.iconName)
    cell.attributeIcon.gameObject:SetActive(hasIconName)
    if hasIconName then
        cell.attributeIcon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, showInfo.iconName)
    end
    cell.attributeText.text = showInfo.showName
    cell.valueText.text = showInfo.showValue
    cell.detailGroup.gameObject:SetActive(false)
    cell.expandMarker.gameObject:SetActive(true)
    cell.shrinkMarker.gameObject:SetActive(false)
    cell.defExtra.gameObject:SetActive(false)

    if cell.detailGroupCache == nil then
        cell.detailGroupCache = UIUtils.genCellCache(cell.detailGroup)
    end

    cell.attrCell.onClick:RemoveAllListeners()
    cell.attrCell.onClick:AddListener(function()
        if DeviceInfo.usingController then
            return
        end
        self:_RefreshAttrDetailExpandState(cell, showInfo, charInfo)
    end)

    cell.detailNode.gameObject:SetActive(false)

    if DeviceInfo.usingController then
        self:_RefreshAttrDetailExpandState(cell, showInfo, charInfo)
    end
end

CharInfoFullAttributeCtrl._RefreshAttrDetailExpandState = HL.Method(HL.Any, HL.Table, HL.Table) << function(self, cell, showInfo, charInfo)
    if cell.detailNode.gameObject.activeSelf then
        cell.detailNode.gameObject:SetActive(false)
        cell.expandMarker.gameObject:SetActive(true)
        cell.shrinkMarker.gameObject:SetActive(false)
        cell.defExtra.gameObject:SetActive(false)
    else
        cell.detailNode.gameObject:SetActive(true)
        cell.expandMarker.gameObject:SetActive(false)
        cell.shrinkMarker.gameObject:SetActive(true)

        local detailCfgList = UIConst.CHAR_INFO_ATTR_TYPE_2_DETAIL_GROUP[showInfo.attributeType]
        if showInfo.attributeType == GEnums.AttributeType.Def then
            local defExtraHint = CharInfoUtils.getDefExtraHint(charInfo.instId)
            if not string.isEmpty(defExtraHint) then
                cell.defExtra.gameObject:SetActive(true)
                cell.defExtraTxt.text = defExtraHint
            end
        end

        cell.detailGroupCache:Refresh(#detailCfgList, function(group, index)
            local detailCfg = detailCfgList[index]
            local showValue, value = CharInfoUtils[detailCfg.valueFuncName](charInfo, showInfo)

            local isShowValue = true
            if detailCfg.notZero then
                if not value then
                    isShowValue = false
                end

                if value and math.abs(value) < 0.001 then
                    isShowValue = false
                end
            end
            group.gameObject:SetActive(isShowValue)
            group.groupText.text = Language[detailCfg.showNameKey]
            group.groupValue.text = showValue
            group.hintBtn.gameObject:SetActive(detailCfg.hintInfo ~= nil)
            if detailCfg.hintInfo ~= nil then
                group.hintBtn.onClick:RemoveAllListeners()
                group.hintBtn.onClick:AddListener(function()
                    local screenPosition = self.uiCamera:WorldToScreenPoint(group.rectTransform.position)
                    local showAbove = screenPosition.y <= Screen.height / 2.0
                    self:_ShowAttributeHint(group, detailCfg.hintInfo, showAbove)
                    if DeviceInfo.usingController then
                        self.view.detailInputGroup.enabled = false
                    end
                end)
            end

            local detailList = {}
            if detailCfg.detailListFuncName then
                detailList = CharInfoUtils[detailCfg.detailListFuncName](charInfo, value)
            end

            if group.detailCellCache == nil then
                group.detailCellCache = UIUtils.genCellCache(group.detailCell)
            end

            group.detailCellCache:Refresh(#detailList, function(detailCell, detailIndex)
                local detailInfo = detailList[detailIndex]
                detailCell.detailName.text = detailInfo.showName
                detailCell.valueText.text = detailInfo.showValue
                if detailCell.subDetailCellCache == nil then
                    detailCell.subDetailCellCache = UIUtils.genCellCache(detailCell.subDetailCell)
                end
                local subDetailCount = detailInfo.subDetailList and #detailInfo.subDetailList or 0
                detailCell.subDetailCellCache:Refresh(subDetailCount, function(subCell, subIndex)
                    local subDetailInfo = detailInfo.subDetailList[subIndex]
                    subCell.detailName.text = subDetailInfo.showName
                    subCell.valueText.text = subDetailInfo.showValue
                    subCell.hintBtn.gameObject:SetActive(subDetailInfo.hintInfo ~= nil)
                    if subDetailInfo.hintInfo ~= nil then
                        subCell.hintBtn.onClick:RemoveAllListeners()
                        subCell.hintBtn.onClick:AddListener(function()
                            local screenPosition = self.uiCamera:WorldToScreenPoint(subCell.rectTransform.position)
                            local showAbove = screenPosition.y <= Screen.height / 2.0
                            self:_ShowAttributeHint(subCell, subDetailInfo.hintInfo, showAbove)
                            if DeviceInfo.usingController then
                                self.view.detailInputGroup.enabled = false
                            end
                        end)
                    end
                end)
            end)
        end)
    end
end

CharInfoFullAttributeCtrl._RefreshSCSubAttrCell = HL.Method(HL.Number, HL.Number, HL.Any, HL.Table, HL.Table) << function(self, listCount, index, cell, showInfo)
    local hasIconName = not string.isEmpty(showInfo.iconName)
    cell.attributeIcon.gameObject:SetActive(hasIconName)
    if hasIconName then
        cell.attributeIcon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, showInfo.iconName)
    end
    cell.intervalLine.gameObject:SetActive(index % 2 == 0)
    cell.attributeText.text = showInfo.showName
    cell.valueText.text = showInfo.showValue
    cell.hintBtn.gameObject:SetActive(showInfo.hasHint)
    if showInfo.hasHint then
        cell.hintBtn.onClick:RemoveAllListeners()
        cell.hintBtn.onClick:AddListener(function()
            local screenPosition = self.uiCamera:WorldToScreenPoint(cell.rectTransform.position)
            local showAbove = screenPosition.y <= Screen.height / 2.0
            local hintInfo = {
                title = showInfo.showName,
                mainHint = AttributeUtils.getAttributeHint(showInfo, {
                    charTemplateId = self.m_charInfo.templateId,
                    charInstId = self.m_charInfo.instId
                }).mainHint,
            }
            self:_ShowAttributeHint(cell, hintInfo, showAbove)
            if DeviceInfo.usingController then
                self.view.detailInputGroup.enabled = false
            end
        end)
    end
end






CharInfoFullAttributeCtrl._ShowAttributeHint = HL.Method(HL.Any, HL.Table, HL.Boolean) << function(self, cell, hintInfo, showAbove)
    self.view.scAttributeHintNode.gameObject:SetActive(true)
    self.view.emptyBtn.gameObject:SetActive(true)

    local hintNode = self.view.scAttributeHintNode

    hintNode.transform:SetParent(cell.hintBtn.transform)
    hintNode.attributeText.text = not string.isEmpty(hintInfo.titleKey) and Language[hintInfo.titleKey] or hintInfo.title
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(self.m_charInfo.instId)
    local formatData = CS.Beyond.Gameplay.PhysicalAndSpellInflictionEnhanceFormatData(charInst.attributes)
    local hintText = CS.Beyond.Gameplay.FormatUtils.FormatBattleText(
        not string.isEmpty(hintInfo.mainHintKey) and Language[hintInfo.mainHintKey] or hintInfo.mainHint, formatData)
    hintNode.hintText:SetAndResolveTextStyle(hintText)

    hintNode.rectTransform.position = Vector3(hintNode.rectTransform.position.x, cell.rectTransform.position.y, hintNode.rectTransform.position.z)

    hintNode.iconArrowTop.gameObject:SetActive(not showAbove)
    hintNode.iconArrowBottom.gameObject:SetActive(showAbove)

    if showAbove then
        LayoutRebuilder.ForceRebuildLayoutImmediate(hintNode.hintText.transform)
        LayoutRebuilder.ForceRebuildLayoutImmediate(hintNode.attributeText.transform)
        LayoutRebuilder.ForceRebuildLayoutImmediate(hintNode.content)
        local contentHeight = hintNode.content.rect.height
        hintNode.rectTransform.localPosition = Vector3(self.view.config.HINT_NODE_OFFSET_X, self.view.config.HINT_NODE_OFFSET_Y + contentHeight, 0)
    else
        hintNode.rectTransform.localPosition = Vector3(self.view.config.HINT_NODE_OFFSET_X, -self.view.config.HINT_NODE_OFFSET_Y, 0)
    end

    hintNode.transform:SetParent(self.view.transform)
end




CharInfoFullAttributeCtrl._InitCharInfoFullAttributeController = HL.Method() << function(self)
    self.view.controllerCloseBtn.gameObject:SetActive(false)
    self.view.controllerCloseBtn.onClick:AddListener(function()
        Notify(MessageConst.CHAR_INFO_CLOSE_ATTR_TIP)
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.detailScroll.enabled = true
    self.view.detailInputGroup.enabled = true
end




HL.Commit(CharInfoFullAttributeCtrl)
