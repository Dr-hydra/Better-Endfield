
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoAttributeHint

CharInfoAttributeHintCtrl = HL.Class('CharInfoAttributeHintCtrl', uiCtrl.UICtrl)






CharInfoAttributeHintCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHAR_INFO_CLOSE_ATTR_TIP] = '_CloseAttrTips',
}

CharInfoAttributeHintCtrl.isShowingHint = HL.Field(HL.Boolean) << false


CharInfoAttributeHintCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.fcAttributeHintNode.gameObject:SetActive(false)
    self.view.scAttributeHintNode.gameObject:SetActive(false)

    self.view.fcAttributeHintNode.autoCloseArea.onTriggerAutoClose:AddListener(function()
        if self.m_args and self.m_isFC then
            self.m_args = nil
        end
        self:_CloseAttrTips()
    end)
    self.view.scAttributeHintNode.autoCloseArea.onTriggerAutoClose:AddListener(function()
        if self.m_args and not self.m_isFC then
            self.m_args = nil
        end
        self:_CloseAttrTips()
    end)
end


CharInfoAttributeHintCtrl.m_args = HL.Field(HL.Table)

CharInfoAttributeHintCtrl.m_isFC = HL.Field(HL.Boolean) << false

CharInfoAttributeHintCtrl.CharInfoShowFCAttrHint = HL.StaticMethod(HL.Table) << function(args)










    CharInfoAttributeHintCtrl._TryShowHint(args, true)
end

CharInfoAttributeHintCtrl.CharInfoShowSCAttrHint = HL.StaticMethod(HL.Table) << function(args)













    CharInfoAttributeHintCtrl._TryShowHint(args, false)
end

CharInfoAttributeHintCtrl._TryShowHint = HL.StaticMethod(HL.Table, HL.Boolean) << function(args, isFirstClass)
    if args.key == nil then
        args.key = args.transform
    end

    local self = UIManager:AutoOpen(PANEL_ID)
    if DeviceInfo.usingController then

        UIUtils.PlayAnimationAndToggleActive(self.view.fcAttributeHintNode.animationWrapper, false)
        UIUtils.PlayAnimationAndToggleActive(self.view.scAttributeHintNode.animationWrapper, false)
    else
        self.view.fcAttributeHintNode.gameObject:SetActive(false)
        self.view.scAttributeHintNode.gameObject:SetActive(false)
    end


    local hintNode = isFirstClass and self.view.fcAttributeHintNode or self.view.scAttributeHintNode
    if self.m_args and self.m_args.key == args.key then

        hintNode.autoCloseArea:CloseSelf()
        return
    end

    UIManager:SetTopOrder(PANEL_ID)
    Notify(MessageConst.CHAR_INFO_CLOSE_SKILL_TIP)

    hintNode.gameObject:SetActive(false)
    hintNode.gameObject:SetActive(true)
    self.m_args = args
    self.m_isFC = isFirstClass
    if isFirstClass then
        self:_ShowFCAttrHintNode()
    else
        self:_ShowSCMainAttrHintNode()
    end
    LayoutRebuilder.ForceRebuildLayoutImmediate(hintNode.content.transform)
    local tipsNode = args.tipsPos or args.transform
    local targetScreenRect = UIUtils.getTransformScreenRect(tipsNode, self.uiCamera)
    local scale = self.view.transform.rect.width / Screen.width
    local pos = Vector2(targetScreenRect.xMax, -targetScreenRect.yMin) * scale

    pos.y = lume.clamp(pos.y, hintNode.content.transform.rect.height - self.view.transform.rect.height, 0)
    hintNode.transform.anchoredPosition = pos
    hintNode.autoCloseArea.tmpSafeArea = args.transform
    hintNode.autoCloseArea:ChangeEnableCloseActionOnController(args.enableCloseActionOnController == true)
    self.isShowingHint = true

    Notify(MessageConst.ON_CHAR_INFO_SHOW_ATTR_TIP)
end

CharInfoAttributeHintCtrl._ShowFCAttrHintNode = HL.Method() << function(self)
    local hintNode = self.view.fcAttributeHintNode
    local hintInfo = AttributeUtils.getAttributeHint(self.m_args.attributeInfo, {
        charTemplateId = self.m_args.charTemplateId,
        charInstId = self.m_args.charInstId
    })
    self:_RefreshAttributeHint(hintNode, self.m_args.attributeInfo, hintInfo, true)
end

CharInfoAttributeHintCtrl._ShowSCMainAttrHintNode = HL.Method() << function(self)
    local hintNode = self.view.scAttributeHintNode
    local charInfo = self.m_args.charInfo
    local attributeInfo = self.m_args.attributeInfo

    if hintNode.defExtra ~= nil then
        local isDef = attributeInfo.attributeType == GEnums.AttributeType.Def
        local defExtraHint = CharInfoUtils.getDefExtraHint(charInfo.instId)
        hintNode.defExtra.gameObject:SetActive(isDef and defExtraHint ~= nil)
        if defExtraHint then
            hintNode.defExtraTxt.text = defExtraHint
        end
    end

    hintNode.attributeText.text = attributeInfo.showName
    hintNode.valueText.text = attributeInfo.showValue
    hintNode.attributeIcon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, attributeInfo.iconName)

    if hintNode.detailGroupCache == nil then
        hintNode.detailGroupCache = UIUtils.genCellCache(hintNode.detailGroup)
    end

    local detailCfgList = UIConst.CHAR_INFO_ATTR_TYPE_2_DETAIL_GROUP[attributeInfo.attributeType]

    hintNode.detailGroupCache:Refresh(#detailCfgList, function(group, index)
        local detailCfg = detailCfgList[index]
        local showValue, value = CharInfoUtils[detailCfg.valueFuncName](charInfo, attributeInfo)

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
        end)
    end)
end

CharInfoAttributeHintCtrl._RefreshAttributeHint = HL.Method(HL.Any, HL.Table, HL.Table, HL.Boolean) << function(self, cell, attributeInfo, hintInfo, isFCAttr)
    cell.attributeText.text = attributeInfo.showName
    cell.hintText.text = hintInfo.mainHint

    if isFCAttr then
        if not cell.m_fcSubHintCellCache then
            cell.m_fcSubHintCellCache = UIUtils.genCellCache(cell.attributesCell)
        end

        local subHintCount = hintInfo.subHintList and #hintInfo.subHintList or 0
        cell.m_fcSubHintCellCache:Refresh(subHintCount, function(subCell, index)
            subCell.text:SetAndResolveTextStyle(hintInfo.subHintList[index])
        end)

        local extraHintCount = hintInfo.extraHintList and #hintInfo.extraHintList or 0
        cell.boundary.gameObject:SetActive(extraHintCount > 0)
        if not cell.m_fcExtraHintCellCache then
            cell.m_fcExtraHintCellCache = UIUtils.genCellCache(cell.extraHint)
        end
        cell.m_fcExtraHintCellCache:Refresh(extraHintCount, function(extraCell, index)
            extraCell.text:SetAndResolveTextStyle(hintInfo.extraHintList[index])
        end)
    end
end

CharInfoAttributeHintCtrl._CloseAttrTips = HL.Method() << function(self)
    if self.m_args ~= nil and self.m_args.onHintClose ~= nil then
        self.m_args.onHintClose()
    end
    local isRealClose = self.isShowingHint
    self.isShowingHint = false
    self.m_args = nil
    if isRealClose then
        self:_OnCloseAttrTips()
    end
    self:PlayAnimationOutAndClose()
end

CharInfoAttributeHintCtrl._OnCloseAttrTips = HL.Method() << function(self)
    Notify(MessageConst.ON_CHAR_INFO_CLOSE_ATTR_TIP)
end

HL.Commit(CharInfoAttributeHintCtrl)
