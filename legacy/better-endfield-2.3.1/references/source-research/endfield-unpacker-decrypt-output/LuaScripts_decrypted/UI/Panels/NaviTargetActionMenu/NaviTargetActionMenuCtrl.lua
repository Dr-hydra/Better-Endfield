
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.NaviTargetActionMenu
NaviTargetActionMenuCtrl = HL.Class('NaviTargetActionMenuCtrl', uiCtrl.UICtrl)

local ContentState = {
    ContentWidth457 = "ContentWidth457",
    ContentWidth290 = "ContentWidth290",
}





NaviTargetActionMenuCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.HIDE_NAVI_TARGET_ACTION_MENU] = 'HideNaviTargetActionMenu',
}

NaviTargetActionMenuCtrl.m_cells = HL.Field(HL.Forward('UIListCache'))

NaviTargetActionMenuCtrl.m_args = HL.Field(HL.Table)



NaviTargetActionMenuCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeMaskBtn.onClick:AddListener(function()
        self:HideNaviTargetActionMenu()
    end)
    self.view.contentAutoCloseArea.onTriggerAutoClose:AddListener(function()
        self:HideNaviTargetActionMenu()
    end)
    self.m_cells = UIUtils.genCellCache(self.view.btnCell)
end














NaviTargetActionMenuCtrl.ShowNaviTargetActionMenu = HL.StaticMethod(HL.Table) << function(args)

    local self = UIManager:AutoOpen(PANEL_ID)
    UIManager:SetTopOrder(PANEL_ID)
    if args.useSmallContent then
        self.view.contentStateController:SetState(ContentState.ContentWidth290)
    else
        self.view.contentStateController:SetState(ContentState.ContentWidth457)
    end
    if args.useRightTitle then
        self.view.titleLeftNode.gameObject:SetActive(false)
        self.view.titleRightNode.gameObject:SetActive(true)
    else
        self.view.titleLeftNode.gameObject:SetActive(true)
        self.view.titleRightNode.gameObject:SetActive(false)
    end
    self:_RefreshContent(args)
end

NaviTargetActionMenuCtrl._RefreshContent = HL.Method(HL.Table) << function(self, args)
    self.m_args = args
    local actions = args.actions
    local useActionRightText = args.useActionRightText == true
    self.m_cells:Refresh(#actions, function(cell, index)
        local info = actions[index]

        if info.objName then
            cell.gameObject.name = info.objName
        else
            cell.gameObject.name = "Cell_" .. index
        end

        if args.cellHeight then
            cell.layoutElement.preferredHeight = args.cellHeight
        else
            cell.layoutElement.preferredHeight = self.view.config.CELL_HEIGHT
        end

        cell.actionNode.onClick:RemoveAllListeners()
        cell.actionNode.onHoverChange:RemoveAllListeners()
        local isAction = info.action ~= nil
        cell.actionNode.gameObject:SetActive(isAction)
        cell.titleNode.gameObject:SetActive(not isAction)
        cell.leftText.gameObject:SetActive(false)
        cell.rightText.gameObject:SetActive(false)
        if isAction then
            if useActionRightText then
                cell.rightText.gameObject:SetActive(true)
                cell.rightText.text = info.text
            else
                cell.leftText.gameObject:SetActive(true)
                cell.leftText.text = info.text
            end

            if info.redDot then
                cell.redDot:InitRedDot(info.redDot)
            else
                cell.redDot.gameObject:SetActiveIfNecessary(false)
            end

            cell.actionNode.onClick:AddListener(function()
                self:_OnClickCell(index)
            end)
            cell.actionNode.onHoverChange:AddListener(function(isHover)
                self:_OnHoverCell(index, isHover)
            end)
        else
            cell.titleText.text = info.text
        end
        if index == 1 then
            self:SetNaviTarget(cell.actionNode)
        end
    end)

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.content)
    local notchSize = CS.Beyond.DeviceInfoManager.NotchPaddingInCanvas(self.view.transform).x
    local padding = {
        bottom = 100,
        left = notchSize,
        right = notchSize,
    }
    local posType = args.posType ~= nil and args.posType or UIConst.UI_TIPS_POS_TYPE.RightTop
    if self.m_args.transform then
        UIUtils.updateTipsPosition(self.view.content, self.m_args.transform, self.view.rectTransform, self.uiCamera, posType, padding)
    elseif self.m_args.targetScreenRect then
        UIUtils.updateTipsPositionWithScreenRect(self.view.content, self.m_args.targetScreenRect, self.view.rectTransform, self.uiCamera, posType, padding)
    end
    self.view.closeMask.enabled = args.noMask ~= true
end

NaviTargetActionMenuCtrl._OnClickCell = HL.Method(HL.Number) << function(self, index)
    local args = self.m_args
    if args.checkActionValid ~= nil and not args.checkActionValid() then
         self:HideNaviTargetActionMenu()
         return
    end
    if args == nil or args.actions == nil then
        return
    end
    local action = args.actions[index]
    if action == nil then
        return
    end
    self:HideNaviTargetActionMenu(action.action, action.beforeCloseAction)
end

NaviTargetActionMenuCtrl._OnHoverCell = HL.Method(HL.Number, HL.Boolean) << function(self, index, isHover)
    local args = self.m_args
    if args == nil or args.actions == nil then
        return
    end
    local action = args.actions[index]
    if action == nil then
        return
    end
    local onHoverAction = action.onHoverAction
    if onHoverAction == nil then
        return
    end
    onHoverAction(isHover)
end

NaviTargetActionMenuCtrl.HideNaviTargetActionMenu = HL.Method(HL.Opt(HL.Function, HL.Function)) <<
    function(self, callback, beforeCloseAction)
    if self:IsPlayingAnimationOut() then
        return
    end



    if self.m_args == nil then
        return
    end

    local args = self.m_args
    if args ~= nil and args.actions ~= nil then
        for index = 1, #args.actions do
            self:_OnHoverCell(index, false)
        end
    end

    local onClose = args.onClose
    if onClose then
        onClose()
    end

    if beforeCloseAction then
        beforeCloseAction()
    end
    if callback then
        self:PlayAnimationOutWithCallback(function()
            self:Hide()
            callback()
        end)
    else
        self:PlayAnimationOutAndHide()
    end
end

NaviTargetActionMenuCtrl.OnHide = HL.Override() << function(self)
    self.m_args = nil
end

NaviTargetActionMenuCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if not self.m_args then
        return
    end

    local arg = lume.deepCopy(self.m_args)
    if self.m_args.transform then
        arg.targetScreenRect = UIUtils.getTransformScreenRect(self.m_args.transform, self.uiCamera)
        arg.transform = nil
    end
    return arg
end

HL.Commit(NaviTargetActionMenuCtrl)
