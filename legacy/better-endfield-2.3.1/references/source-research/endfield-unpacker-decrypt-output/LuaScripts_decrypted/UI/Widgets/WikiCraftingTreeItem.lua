local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WikiCraftingTreeItem = HL.Class('WikiCraftingTreeItem', UIWidgetBase)

WikiCraftingTreeItem.m_args = HL.Field(HL.Table)

WikiCraftingTreeItem.m_hasWiki = HL.Field(HL.Boolean) << false

WikiCraftingTreeItem.m_isMain = HL.Field(HL.Boolean) << false


WikiCraftingTreeItem._OnFirstTimeInit = HL.Override() << function(self)
    self.view.selectNode.cutBtn.onClick:AddListener(function()
        if not self.m_hasWiki then
            return
        end
        Notify(MessageConst.CHANGE_WIKI_CRAFTING_TREE, self.m_args.itemId)
    end)
    self.view.selectNode.btnTip.onClick:AddListener(function()
        if self.m_isMain then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_WIKI_CRAFTING_CAN_NOT_SWITCH_MAIN)
        elseif not self.m_hasWiki then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_WIKI_CRAFTING_CAN_NOT_SWITCH)
        end
    end)
end








WikiCraftingTreeItem.InitWikiCraftingTreeItem = HL.Method(HL.Table) << function(self, args)
    self.m_args = args
    self:_FirstTimeInit()

    self.view.gameObject.name = args.itemId
    self.view.itemBlack:InitItem({ id = args.itemId }, function()
        if args.onClicked then
            args.onClicked(args.itemId, self)
        end
    end)
    self.view.itemBlack:SetEnableHoverTips(not DeviceInfo.usingController)
    self:SetSelected(false)
    self:SetMain(args.isShowMainIcon == true)
    self.m_hasWiki = WikiUtils.getWikiEntryIdFromItemId(args.itemId) ~= nil
    self.m_isMain = args.mainItemId == args.itemId
    self:SetJumpBtn(self.m_isMain, self.m_hasWiki)

    if args.playInAnimation == true then
        self.view.animationWrapper:PlayInAnimation()
    end

    self:_InitController()
end

WikiCraftingTreeItem.GetItemId = HL.Method().Return(HL.String) << function(self)
    return self.m_args.itemId
end

WikiCraftingTreeItem.SetMain = HL.Method(HL.Boolean) << function(self, isMain)
    self.view.mainNode.gameObject:SetActive(isMain)
    if isMain then
        if self.m_args.playInAnimation then
            self.view.mainNode:PlayInAnimation()
        else
            self.view.mainNode:PlayLoopAnimation()
        end
    end
end

WikiCraftingTreeItem.SetJumpBtn = HL.Method(HL.Boolean, HL.Boolean) << function(self, isMain, hasWiki)
    local stateName
    if isMain then
        stateName = "Main"
    elseif hasWiki then
        stateName = "Jump"
    else
        stateName = "NoJump"
    end
    self.view.selectNode.stateController:SetState(stateName)
end

WikiCraftingTreeItem.SetSelected = HL.Method(HL.Boolean) << function(self, isSelected)
    self.view.selectNode.gameObject:SetActive(isSelected)
    self.view.itemBlack.view.nonDrawingGraphic.enabled = not isSelected
end

WikiCraftingTreeItem.GetButton = HL.Method().Return(HL.Userdata) << function(self)
    return self.view.itemBlack.view.button
end

WikiCraftingTreeItem.SetLeftMountPointCount = HL.Method(HL.Number) << function(self, count)
    CSUtils.UIContainerResize(self.view.leftNode, count)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.leftNode)
end

WikiCraftingTreeItem.SetRightMountPointCount = HL.Method(HL.Number) << function(self, count)
    CSUtils.UIContainerResize(self.view.rightNode, count)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.rightNode)
end

WikiCraftingTreeItem.GetLeftMountPoint = HL.Method(Transform, HL.Number).Return(Vector2) << function(self, relativeTo, index)
    local node = self.view.leftNode.transform:GetChild(CSIndex(index))
    if node then
        local pos = relativeTo:InverseTransformPoint(node.position)
        return Vector2(pos.x, pos.y)
    end
    return Vector2.zero
end

WikiCraftingTreeItem.GetRightMountPoint = HL.Method(Transform, HL.Number).Return(Vector2) << function(self, relativeTo, index)
    local node = self.view.rightNode.transform:GetChild(CSIndex(index))
    if node then
        local pos = relativeTo:InverseTransformPoint(node.position)
        return Vector2(pos.x, pos.y)
    end
    return Vector2.zero
end

WikiCraftingTreeItem.HideExpandToggle = HL.Method() << function(self)
    self.view.expandToggle.gameObject:SetActive(false)
end

WikiCraftingTreeItem.SetExpandToggle = HL.Method(HL.Boolean, HL.Function) << function(self, isOn, callback)
    self.view.expandToggle.onValueChanged:RemoveAllListeners()
    self.view.expandToggle.gameObject:SetActive(true)
    self.view.expandToggle.isOn = isOn
    InputManagerInst:SetBindingText(self.view.expandToggle.toggleBindingId,
        isOn and Language.ui_wiki_fac_tree_item_less or Language.ui_wiki_fac_tree_item_more)
    self.view.expandToggle.onValueChanged:AddListener(function(value)
        InputManagerInst:SetBindingText(self.view.expandToggle.toggleBindingId,
            value and Language.ui_wiki_fac_tree_item_less or Language.ui_wiki_fac_tree_item_more)
        if callback then
            callback(value)
        end
    end)
end

WikiCraftingTreeItem._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    self.view.expandToggleKeyHint.gameObject:SetActive(false)
    self.view.expandInputBindingGroup.enabled = self.view.itemBlack.view.button.isNaviTarget
    self.view.expandToggleKeyHint.gameObject:SetActive(self.view.itemBlack.view.button.isNaviTarget)
    self.view.itemBlack.view.button.onIsNaviTargetChanged = function(isNaviTarget)
        self.view.expandToggleKeyHint.gameObject:SetActive(isNaviTarget)
        self.view.expandInputBindingGroup.enabled = isNaviTarget
    end
end

HL.Commit(WikiCraftingTreeItem)
return WikiCraftingTreeItem

