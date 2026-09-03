
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FakeControllerSmallMenu
FakeControllerSmallMenuCtrl = HL.Class('FakeControllerSmallMenuCtrl', uiCtrl.UICtrl)

local DEFAULT_PANEL_OFFSET = 5

FakeControllerSmallMenuCtrl.m_currMenuData = HL.Field(HL.Table)

FakeControllerSmallMenuCtrl.m_menuDataStack = HL.Field(HL.Forward("Stack"))






FakeControllerSmallMenuCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CLOSE_CONTROLLER_SMALL_MENU] = 'CloseControllerSmallMenu',
    [MessageConst.ON_INPUT_DEVICE_TYPE_CHANGED] = 'OnInputDeviceTypeChanged',
    [MessageConst.ON_PANEL_ORDER_RECALCULATED] = 'OnPanelOrderRecalculated',
}


FakeControllerSmallMenuCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    logger.info("[FakeControllerSmallMenu] OnCreate")
    self.view.highlightCell.maskNode.onClick:AddListener(function()
        self:_OnClickClose()
    end)

    self.m_menuDataStack = require_ex("Common/Utils/DataStructure/Stack")()
end

FakeControllerSmallMenuCtrl.m_lateTickKey = HL.Field(HL.Number) << -1

FakeControllerSmallMenuCtrl.OnShow = HL.Override() << function(self)
    logger.info("[FakeControllerSmallMenu] OnShow")
    self.m_lateTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
        self:_RefreshHighlight()
    end)
end
FakeControllerSmallMenuCtrl.OnHide = HL.Override() << function(self)
    logger.info("[FakeControllerSmallMenu] OnHide")
    self:_Clear()
end
FakeControllerSmallMenuCtrl.OnClose = HL.Override() << function(self)
    logger.info("[FakeControllerSmallMenu] OnClose, stackCount=" .. tostring(self.m_menuDataStack and self.m_menuDataStack:Count() or 0))








    if self.m_menuDataStack and not self.m_menuDataStack:Empty() then
        self:_Close(self.m_menuDataStack:Peek())
        self.m_menuDataStack:Clear()
    end
    self:_Clear()
end

FakeControllerSmallMenuCtrl.ShowAsControllerSmallMenu = HL.StaticMethod(HL.Table) << function(args)



    if lume.isarray(args) then

        args = FakeControllerSmallMenuCtrl.TryParseArgs(unpack(args))
    end

    logger.info("[FakeControllerSmallMenu] ShowAsControllerSmallMenu, id=" .. tostring(args and args.id) ..
        ", panelId=" .. tostring(args and args.panelId) .. ", isGroup=" .. tostring(args and args.isGroup))


    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if isOpen and ctrl:IsPlayingAnimationOut() then
        ctrl:_FinishOutAnimationAndHide()
    end


    local self = FakeControllerSmallMenuCtrl.AutoOpen(PANEL_ID, nil, true)
    self:_TryRefresh(args)
end

FakeControllerSmallMenuCtrl.TryParseArgs = HL.StaticMethod(HL.Userdata).Return(HL.Table) << function(csArgs)
    local panelId = csArgs.panelId
    local isOpen, ctrl = UIManager:IsOpen(panelId)
    if not isOpen then
        return
    end




    return {
        panelId = panelId,
        isGroup = csArgs.isGroup,
        id = csArgs.id,
        rectTransform = csArgs.rectTransform,
        noHighlight = csArgs.noHighlight,
        hintPlaceholder = ctrl.view.controllerHintPlaceholder,
        useNormalFrame = csArgs.useNormalFrame,
        useDarkFrame = csArgs.useDarkFrame,
        panelSortingOrder = csArgs.panelSortingOrder,
    }
end

FakeControllerSmallMenuCtrl.OnInputDeviceTypeChanged = HL.Method(HL.Table) << function(self, arg)
    logger.info("[FakeControllerSmallMenu] OnInputDeviceTypeChanged, force close")
    self:_ForceClose()
end

FakeControllerSmallMenuCtrl.CloseControllerSmallMenu = HL.Method(HL.Any) << function(self, groupId)
    if not self:IsShow() then
        logger.info("[FakeControllerSmallMenu] CloseControllerSmallMenu skipped (not show), groupId=" .. tostring(groupId))
        return
    end

    if lume.isarray(groupId) then
        groupId = unpack(groupId)
    end

    logger.info("[FakeControllerSmallMenu] CloseControllerSmallMenu, groupId=" .. tostring(groupId))
    self:_TryClose(groupId)
end

FakeControllerSmallMenuCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    self.view.highlightCell.gameObject:SetActive(active)
end

FakeControllerSmallMenuCtrl._Clear = HL.Method() << function(self)
    logger.info("[FakeControllerSmallMenu] _Clear")
    LuaUpdate:Remove(self.m_lateTickKey)
    self.m_lateTickKey = -1
    Notify(MessageConst.TOGGLE_LEVEL_CAMERA_MOVE, { "guide", true })
    CoroutineManager:ClearAllCoroutine(self)
end

FakeControllerSmallMenuCtrl._OnClickClose = HL.Method() << function(self)
    if self.m_currMenuData and not self.m_currMenuData.canClickClose then
        logger.info("[FakeControllerSmallMenu] _OnClickClose blocked by canClickClose, id=" .. tostring(self.m_currMenuData.id))
        return
    end
    logger.info("[FakeControllerSmallMenu] _OnClickClose, force close")
    self:_ForceClose()
end

FakeControllerSmallMenuCtrl._TryRefresh = HL.Method(HL.Table) << function(self, args)
    local id = args.id
    if id == nil then
        logger.info("[FakeControllerSmallMenu] _TryRefresh skipped, id is nil")
        return
    end

    if not self.m_menuDataStack:Empty() then
        local peekMenuData = self.m_menuDataStack:Peek()
        if peekMenuData ~= nil then
            self:_TransferParent(peekMenuData)
        end
    end
    if args and args.useNormalFrame and self.view.config:HasValue("NORMAL_FRAME_OPEN_AUDIO") then
        AudioAdapter.PostEvent(self.view.config.NORMAL_FRAME_OPEN_AUDIO)
    end
    self.m_menuDataStack:Push(args)
    logger.info("[FakeControllerSmallMenu] _TryRefresh push" .. tostring(id) ..
        ", stackCount=" .. tostring(self.m_menuDataStack:Count()))

    self:_Refresh(self.m_menuDataStack:Peek())
end

FakeControllerSmallMenuCtrl._Refresh = HL.Method(HL.Table) << function(self, menuData)
    if menuData == nil or self.m_currMenuData == menuData then
        logger.info("[FakeControllerSmallMenu] _Refresh skipped (nil or same menuData)")
        return
    end

    local id = menuData.id
    local isGroup = menuData.isGroup
    local oldGroupId = InputManagerInst:GetGroupParentId(isGroup, id)
    logger.info("[FakeControllerSmallMenu] _Refresh, id=" .. tostring(id) .. ", isGroup=" .. tostring(isGroup) ..
        ", borrow parent: " .. tostring(oldGroupId) .. " -> " .. tostring(self.view.inputGroup.groupId))
    InputManagerInst:ChangeParent(isGroup, id, self.view.inputGroup.groupId)
    local needShowType = menuData.useVirtualMouse and Types.EPanelMouseMode.NeedShow or Types.EPanelMouseMode.ForceHide
    self:ChangePanelCfg("virtualMouseMode", needShowType)
    menuData.oldGroupId = oldGroupId

    self:_ChangePanelOrder(menuData)

    if menuData.hintPlaceholder ~= nil then
        local hintArgs = menuData.hintPlaceholder:GetArgs()
        hintArgs.panelId = PANEL_ID
        hintArgs.groupIds = {self.view.inputGroup.groupId}
        hintArgs.optionalActionIds = nil
        hintArgs.offset = 1
        Notify(MessageConst.SHOW_CONTROLLER_HINT, hintArgs)
    end

    if menuData.walletPlaceholder ~= nil then
        local walletArgs = menuData.walletPlaceholder:GetArgs()
        walletArgs.panelId = PANEL_ID
        walletArgs.offset = 1
        Notify(MessageConst.SHOW_WALLET_BAR, walletArgs)
    end

    if oldGroupId == UIManager.uiInputBindingGroupMonoTarget.groupId then

        logger.error("面板根节点不能使用 FakeControllerSmallMenu", menuData.rectTransform:PathFromRoot())
    end

    self.m_currMenuData = menuData
    self:_RefreshHighlight()
end

FakeControllerSmallMenuCtrl._RefreshHighlight = HL.Method() << function(self)
    if not self.view.highlightCell.gameObject.activeSelf then
        return
    end

    local cell = self.view.highlightCell
    cell.canvasGroup.alpha = 0

    if self.m_currMenuData == nil then
        return
    end

    local target = self.m_currMenuData.rectTransform
    if not NotNull(target) then
        return
    end

    local rectTrans = cell.rectTransform
    local targetRect = UIUtils.getUIRectOfRectTransform(target, self.uiCamera)

    rectTrans.anchoredPosition = Vector2(targetRect.center.x, -targetRect.center.y)
    rectTrans.localRotation = target.rotation
    rectTrans.sizeDelta = targetRect.size


    local width = UIManager.uiCanvasRect.rect.size.x
    local height = UIManager.uiCanvasRect.rect.size.y
    local xOffset = width / 2 - targetRect.center.x
    cell.up.anchoredPosition = Vector2(xOffset, 0)
    cell.down.anchoredPosition = Vector2(xOffset, 0)
    cell.up.sizeDelta = Vector2(width, targetRect.y)
    cell.down.sizeDelta = Vector2(width, height - targetRect.yMax)
    cell.left.sizeDelta = Vector2(targetRect.x, targetRect.height)
    cell.right.sizeDelta = Vector2(width - targetRect.xMax, targetRect.height)
    cell.canvasGroup.alpha = self.m_currMenuData.noHighlight and 0 or 1


    local noMask = self.m_currMenuData.noHighlight
    local useNormalFrame = self.m_currMenuData.useNormalFrame == true
    local useDarkFrame = self.m_currMenuData.useDarkFrame == true
    cell.normalFrame.gameObject:SetActive(useNormalFrame)
    cell.darkFrame.gameObject:SetActive(useDarkFrame)
    if useNormalFrame then
        cell.normalFrame.frameBG.gameObject:SetActive(not noMask)
        cell.normalFrame.frameBGNoMask.gameObject:SetActive(noMask)
    end
    if useDarkFrame then
        cell.darkFrame.frameBG.gameObject:SetActive(not noMask)
        cell.darkFrame.frameBGNoMask.gameObject:SetActive(noMask)
    end
end

FakeControllerSmallMenuCtrl._ChangePanelOrder = HL.Method(HL.Table) << function(self, menuData)
    if menuData == nil then
        return
    end

    local selfPanelOrder
    if menuData.panelSortingOrder and menuData.panelSortingOrder > 0 then
        selfPanelOrder = menuData.panelSortingOrder
    else
        local panelId = menuData.panelId
        if panelId == nil then
            return
        end
        local _, panel = UIManager:IsOpen(panelId)
        if panel == nil then
            return
        end

        local offset = menuData.panelOffset or DEFAULT_PANEL_OFFSET
        selfPanelOrder = panel:GetSortingOrder() + offset
    end

    if selfPanelOrder then
        self:SetSortingOrder(selfPanelOrder, false)
        UIManager:CalcOtherSystemPropertyByPanelOrder()
    end
end

FakeControllerSmallMenuCtrl._FinishOutAnimationAndHide = HL.Method() << function(self)
    logger.info("[FakeControllerSmallMenu] _FinishOutAnimationAndHide (interrupt out anim)")
    if self.m_outAnimAsyncActionHelper then
        self.m_outAnimAsyncActionHelper:ForceClear()
    end
    self:Hide()
end

FakeControllerSmallMenuCtrl._ForceClose = HL.Method() << function(self)
    if self.m_menuDataStack:Empty() then
        logger.info("[FakeControllerSmallMenu] _ForceClose skipped (stack empty)")
        return
    end

    logger.info("[FakeControllerSmallMenu] _ForceClose, stackCount=" .. tostring(self.m_menuDataStack:Count()))
    self:_Close(self.m_menuDataStack:Peek())
    self.m_menuDataStack:Clear()

    self:Hide()
end

FakeControllerSmallMenuCtrl._TryClose = HL.Method(HL.Number) << function(self, groupId)
    if self.m_menuDataStack:Empty() then
        logger.info("[FakeControllerSmallMenu] _TryClose skipped (stack empty), groupId=" .. tostring(groupId))
        return
    end

    local index, closeMenuData
    for i = self.m_menuDataStack:Count(), 1, -1 do
        local menuData = self.m_menuDataStack:Get(i)
        if menuData ~= nil and menuData.id == groupId then
            index = i
            closeMenuData = menuData
            break
        end
    end
    if closeMenuData == nil then
        logger.info("[FakeControllerSmallMenu] _TryClose no match in stack, groupId=" .. tostring(groupId))
        return
    end

    logger.info("[FakeControllerSmallMenu] _TryClose match, groupId=" .. tostring(groupId) ..
        ", index=" .. tostring(index) .. ", isTop=" .. tostring(index == self.m_menuDataStack:Count()))
    if index == self.m_menuDataStack:Count() then
        self:_Close(closeMenuData)
    end
    self.m_menuDataStack:Delete(closeMenuData)

    if self.m_menuDataStack:Empty() then
        if closeMenuData.noHighlight and not (closeMenuData.useNormalFrame or closeMenuData.useDarkFrame) then

            self:Hide()
        else
            self:PlayAnimationOutAndHide()
        end

        if closeMenuData and closeMenuData.useNormalFrame and self.view.config:HasValue("NORMAL_FRAME_CLOSE_AUDIO") then
            AudioAdapter.PostEvent(self.view.config.NORMAL_FRAME_CLOSE_AUDIO)
        end
    else
        self:_Refresh(self.m_menuDataStack:Peek())
    end
end

FakeControllerSmallMenuCtrl._Close = HL.Method(HL.Table) << function(self, menuData)
    if menuData == nil then
        logger.info("[FakeControllerSmallMenu] _Close skipped (menuData nil)")
        return
    end

    if menuData.hintPlaceholder ~= nil then
        Notify(MessageConst.HIDE_CONTROLLER_HINT, { panelId = PANEL_ID, })
        menuData.hintPlaceholder = nil
    end
    if menuData.walletPlaceholder ~= nil then
        Notify(MessageConst.HIDE_WALLET_BAR, PANEL_ID)
        menuData.walletPlaceholder = nil
    end

    local id = menuData.id
    local isGroup = menuData.isGroup
    local oldGroupId = menuData.oldGroupId
    logger.info("[FakeControllerSmallMenu] _Close, id=" .. tostring(id) .. ", isGroup=" .. tostring(isGroup) ..
        ", restore parent -> " .. tostring(oldGroupId))
    InputManagerInst:ChangeParent(isGroup, id, oldGroupId)

    if menuData.onClose then
        menuData.onClose()
    end

    self.m_currMenuData = nil
end

FakeControllerSmallMenuCtrl._TransferParent = HL.Method(HL.Table) << function(self, menuData)
    if menuData == nil then
        logger.info("[FakeControllerSmallMenu] _TransferParent skipped (menuData nil)")
        return
    end

    local id = menuData.id
    local isGroup = menuData.isGroup
    local oldGroupId = menuData.oldGroupId
    logger.info("[FakeControllerSmallMenu] _TransferParent, id=" .. tostring(id) .. ", isGroup=" .. tostring(isGroup) ..
        ", return parent -> " .. tostring(oldGroupId))
    InputManagerInst:ChangeParent(isGroup, id, oldGroupId)

    if menuData.hintPlaceholder ~= nil then
        Notify(MessageConst.HIDE_CONTROLLER_HINT, { panelId = PANEL_ID, })
    end
    if menuData.walletPlaceholder ~= nil then
        Notify(MessageConst.HIDE_WALLET_BAR, PANEL_ID)
    end
end

FakeControllerSmallMenuCtrl.OnPanelOrderRecalculated = HL.Method() << function(self)
    if self.m_currMenuData == nil then
        return
    end
    self:_ChangePanelOrder(self.m_currMenuData)
end

HL.Commit(FakeControllerSmallMenuCtrl)
