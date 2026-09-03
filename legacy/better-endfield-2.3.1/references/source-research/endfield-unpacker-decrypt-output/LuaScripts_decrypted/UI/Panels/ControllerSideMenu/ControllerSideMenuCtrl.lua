
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ControllerSideMenu
ControllerSideMenuCtrl = HL.Class('ControllerSideMenuCtrl', uiCtrl.UICtrl)





ControllerSideMenuCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ControllerSideMenuCtrl.m_cells = HL.Field(HL.Forward('UIListCache'))

ControllerSideMenuCtrl.m_menuListCpt = HL.Field(CS.Beyond.UI.ControllerSideMenuItemList)

ControllerSideMenuCtrl.m_infos = HL.Field(HL.Table)



ControllerSideMenuCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:BindInputPlayerAction("common_close_controller_side_menu", function()
        AudioAdapter.PostEvent("Au_UI_Button_Back")
        self:PlayAnimationOutAndClose()
    end)

    self.m_cells = UIUtils.genCellCache(self.view.btnCell)
    self.m_menuListCpt = arg.menuBtnList

    self.view.titleText.text = arg.title or Language[self.m_menuListCpt.title]
    self.view.titleIcon.sprite = arg.icon

    self.m_infos = arg.btnInfos
    self.m_cells:Refresh(#self.m_infos, function(cell, index)
        self:_RefreshCell(cell, index)
        if index == 1 then
            self:SetNaviTarget(cell.button)
        end
    end)
    local targetScreenRect = UIUtils.getTransformScreenRect(self.m_menuListCpt.contentPosTrans, self.uiCamera)
    local pos = UIUtils.screenPointToUI(targetScreenRect.center, self.uiCamera, self.view.transform)
    pos.y = -pos.y
    self.view.listContent.anchoredPosition = pos
    self.view.listContent.sizeDelta = self.m_menuListCpt.contentPosTrans.rect.size
    self.view.listStateController:SetState(self.m_menuListCpt.isFullScreen and "FullScreen" or "Window")

    self:_ShowControllerHint(arg.hintPlaceholder)
end

ControllerSideMenuCtrl._ShowControllerHint = HL.Method(HL.Forward('ControllerHintPlaceholder')) << function(self, hintPlaceholder)
    local hintArgs = hintPlaceholder:GetArgs()
    hintArgs.panelId = PANEL_ID
    hintArgs.groupIds = { self.view.inputGroup.groupId }
    hintArgs.optionalActionIds = nil
    hintArgs.offset = 1
    Notify(MessageConst.SHOW_CONTROLLER_HINT, hintArgs)
end


ControllerSideMenuCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.HIDE_CONTROLLER_HINT, { panelId = PANEL_ID, })
end

ControllerSideMenuCtrl._RefreshCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_OnClickBtn(index)
    end)
    local info = self.m_infos[index]
    local cellName
    if type(info) == "table" then
        cell.icon.sprite = info.sprite
        if info.getText ~= nil then
            cell.text.text = info.getText()
        else
            cell.text.text = Language[info.textId]
        end
        if info.name then
            cellName = info.name
        elseif info.button then
            cellName = info.button.gameObject.name
        else
            logger.error("[ControllerSideMenu] 第 %d 个 extra item 缺少 'name' 字段，请为该 item 指定一个稳定的 name", index)
        end
    else
        cell.icon.sprite = info:GetSprite()
        cell.text.text = info:GetText()
        cellName = info:GetItemName()
    end
    cell.icon.gameObject:SetActive(cell.icon.sprite ~= nil)
    cell.gameObject.name = cellName
end

ControllerSideMenuCtrl._OnClickBtn = HL.Method(HL.Number) << function(self, index)
    self:PlayAnimationOutWithCallback(function()
        local info = self.m_infos[index]
        self:Close()
        if type(info) == "table" then
            if info.action then
                info.action()
            elseif info.button then
                info.button.onClick:Invoke(nil)
            end
        else
            info:Execute()
        end
    end)
end

HL.Commit(ControllerSideMenuCtrl)
