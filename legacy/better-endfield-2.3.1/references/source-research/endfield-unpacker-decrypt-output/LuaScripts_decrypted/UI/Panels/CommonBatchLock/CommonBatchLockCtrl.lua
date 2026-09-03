local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonBatchLock














CommonBatchLockCtrl = HL.Class('CommonBatchLockCtrl', uiCtrl.UICtrl)

CommonBatchLockCtrl.m_args = HL.Field(HL.Table)

CommonBatchLockCtrl.m_unlockActions = HL.Field(HL.Table)

CommonBatchLockCtrl.m_lockActions = HL.Field(HL.Table)

CommonBatchLockCtrl.m_selectedId = HL.Field(HL.Any)

CommonBatchLockCtrl.m_unlockTagCells = HL.Field(HL.Forward("UIListCache"))

CommonBatchLockCtrl.m_lockTagCells = HL.Field(HL.Forward("UIListCache"))

CommonBatchLockCtrl.m_hintText = HL.Field(HL.Userdata)

CommonBatchLockCtrl.m_rootNaviGroup = HL.Field(HL.Userdata)

CommonBatchLockCtrl.m_tagNaviGroup = HL.Field(HL.Userdata)





CommonBatchLockCtrl.s_messages = HL.StaticField(HL.Table) << {}


CommonBatchLockCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local recoverState = arg and arg.recoverState or nil
    self.m_args = arg or {}
    self.m_unlockActions = self.m_args.unlockActions or {}
    self.m_lockActions = self.m_args.lockActions or {}
    self.m_selectedId = nil
    self.m_rootNaviGroup = nil
    self.m_tagNaviGroup = nil

    if not self.m_args.onConfirm then
        logger.error("[CommonBatchLock] OnCreate: onConfirm 回调未提供")
    end


    self.view.closeButton.onClick:AddListener(function()
        self:_OnClickClose()
    end)
    self.view.resetBtn.onClick:AddListener(function()
        self:_OnClickReset()
    end)
    self.view.confirmBtn.onClick:AddListener(function()
        self:_OnClickConfirm()
    end)


    self.m_hintText = nil
    if self.view.bottomNode then
        local hintTransform = self.view.bottomNode.transform:Find("LockDetailTxt")
        if hintTransform then
            self.m_hintText = hintTransform:GetComponent(typeof(CS.TMPro.TMP_Text))
        end
    end
    if self.m_hintText then
        if string.isEmpty(self.m_args.unlockHintText) then
            self.m_hintText.gameObject:SetActive(false)
        else
            self.m_hintText.gameObject:SetActive(true)
            self.m_hintText.text = self.m_args.unlockHintText
        end
    end


    self.m_unlockTagCells = self:_InitSection(
        self.view.unlockTagGroupCell,
        self.m_unlockActions,
        "unlock"
    )
    self.m_lockTagCells = self:_InitSection(
        self.view.lockTagGroupCell,
        self.m_lockActions,
        "lock"
    )

    self:_InitControllerNavigation()
    if recoverState then
        self:_TryRecoverState(recoverState)
        arg.recoverState = nil
    else
        self:_Refresh()
        self:_InitNaviTarget()
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

CommonBatchLockCtrl._InitSection = HL.Method(HL.Any, HL.Opt(HL.Table, HL.String)).Return(HL.Forward("UIListCache")) << function(self, groupCell, actions, cacheKey)
    local hasActions = actions ~= nil and #actions > 0
    groupCell.gameObject:SetActive(hasActions)
    if not hasActions then
        return nil
    end

    local tagCells = UIUtils.genCellCache(groupCell.tagCell)
    tagCells:Refresh(#actions, function(cell, index)
        local action = actions[index]
        cell.gameObject.name = string.format("%sTag_%s", cacheKey, tostring(action.id))
        if cell.name then
            cell.name.text = action.labelText or ""
        end
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle:SetIsOnWithoutNotify(action.id == self.m_selectedId)
        cell.toggle.onIsNaviTargetChanged = nil
        cell.toggle.onIsNaviTargetChanged = function(active)
            self:_OnTagCellSelectedChanged(cell, cell.toggle.isOn, active)
        end
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:_OnSelectTag(action.id)
            else
                if self.m_selectedId == action.id then
                    self:_OnClickReset()
                end
            end
            self:_OnTagCellSelectedChanged(cell, isOn, true)
        end)
        self:_OnTagCellSelectedChanged(cell, cell.toggle.isOn, cell.toggle.isNaviTarget)
    end)
    return tagCells
end

CommonBatchLockCtrl._TryRecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    self.m_selectedId = recoverState.selectedId
    self:_Refresh()
    if not DeviceInfo.usingController then
        return
    end
    self:_InitNaviTarget()
end

CommonBatchLockCtrl._OnTagCellSelectedChanged = HL.Method(HL.Table, HL.Boolean, HL.Boolean) << function(self, cell, isSelect, active)
    local bindingId = cell and cell.toggle and cell.toggle.hoverConfirmBindingId
    if not bindingId then
        return
    end
    InputManagerInst:SetBindingText(bindingId, isSelect and
        (Language.key_hint_common_unselect or Language.LUA_COMMON_FILTER_CANCEL_SELECT_KEY_HINT) or
        (Language.key_hint_common_select or Language.LUA_COMMON_FILTER_SELECT_KEY_HINT))
    if not active then
        InputManagerInst:ToggleBinding(bindingId, false)
    end
end

CommonBatchLockCtrl._InitControllerNavigation = HL.Method() << function(self)
    self.m_rootNaviGroup = self.view.resetBtn and self.view.resetBtn.naviGroup or self.view.confirmBtn and self.view.confirmBtn.naviGroup or self.view.closeButton and self.view.closeButton.naviGroup
    self.m_tagNaviGroup = self.view.unlockTagGroupCell and self.view.unlockTagGroupCell.tagCell and self.view.unlockTagGroupCell.tagCell.toggle and self.view.unlockTagGroupCell.tagCell.toggle.naviGroup
    if self.m_tagNaviGroup == nil then
        self.m_tagNaviGroup = self.view.lockTagGroupCell and self.view.lockTagGroupCell.tagCell and self.view.lockTagGroupCell.tagCell.toggle and self.view.lockTagGroupCell.tagCell.toggle.naviGroup
    end

    if self.m_rootNaviGroup then
        self.m_rootNaviGroup.getDefaultSelectableFunc = function()
            return self:_GetFirstTagSelectable() or self:_GetFirstAvailableButton()
        end
    end
    if self.m_tagNaviGroup then
        self.m_tagNaviGroup.getDefaultSelectableFunc = function()
            return self:_GetFirstTagSelectable()
        end
    end
end

CommonBatchLockCtrl._InitNaviTarget = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    local targetSelectable = self:_GetFirstTagSelectable() or self:_GetFirstAvailableButton()
    if targetSelectable then
        local targetNaviGroup = targetSelectable.naviGroup or self.m_tagNaviGroup or self.m_rootNaviGroup
        if targetNaviGroup then
            self:SetNaviTarget(targetSelectable)
            return
        end
        self:SetNaviTarget(targetSelectable)
    end
end

CommonBatchLockCtrl._GetFirstAvailableButton = HL.Method().Return(HL.Userdata) << function(self)
    if self.view.resetBtn and self.view.resetBtn.gameObject.activeInHierarchy and self.view.resetBtn.interactable then
        return self.view.resetBtn
    end
    if self.view.confirmBtn and self.view.confirmBtn.gameObject.activeInHierarchy and self.view.confirmBtn.interactable then
        return self.view.confirmBtn
    end
    if self.view.closeButton and self.view.closeButton.gameObject.activeInHierarchy and self.view.closeButton.interactable then
        return self.view.closeButton
    end
    return nil
end

CommonBatchLockCtrl._GetFirstTagSelectable = HL.Method().Return(HL.Userdata) << function(self)
    local function getFirstSelectable(cache)
        local cell = cache and cache:GetItem(1)
        if cell and cell.toggle and cell.toggle.gameObject.activeInHierarchy and cell.toggle.interactable then
            return cell.toggle
        end
        return nil
    end

    return getFirstSelectable(self.m_unlockTagCells) or getFirstSelectable(self.m_lockTagCells)
end

CommonBatchLockCtrl._GetTargetTagSelectable = HL.Method().Return(HL.Userdata) << function(self)
    if self.m_selectedId == nil then
        return self:_GetFirstTagSelectable()
    end

    local function getSelectedSelectable(cache, actions)
        if cache == nil then
            return nil
        end
        for index, action in ipairs(actions or {}) do
            if action.id == self.m_selectedId then
                local cell = cache:GetItem(index)
                if cell and cell.toggle and cell.toggle.gameObject.activeInHierarchy and cell.toggle.interactable then
                    return cell.toggle
                end
                break
            end
        end
        return nil
    end

    return getSelectedSelectable(self.m_unlockTagCells, self.m_unlockActions)
        or getSelectedSelectable(self.m_lockTagCells, self.m_lockActions)
        or self:_GetFirstTagSelectable()
end

CommonBatchLockCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if not self.m_args then
        return
    end
    local arg = lume.deepCopy(self.m_args)
    arg.recoverState = {
        selectedId = self.m_selectedId,
    }
    return arg
end

CommonBatchLockCtrl._OnSelectTag = HL.Method(HL.Any) << function(self, id)
    if self.m_selectedId == id then
        return
    end
    self.m_selectedId = id
    self:_Refresh()
end

CommonBatchLockCtrl._OnClickReset = HL.Method() << function(self)
    if self.m_selectedId == nil then
        return
    end
    self.m_selectedId = nil
    self:_Refresh()
end

CommonBatchLockCtrl._OnClickConfirm = HL.Method() << function(self)
    if self.m_selectedId == nil then
        return
    end
    local selectedId = self.m_selectedId
    if self.m_args.onConfirm then
        self.m_args.onConfirm(selectedId)
    end
    UIManager:Close(PANEL_ID)
end

CommonBatchLockCtrl._OnClickClose = HL.Method() << function(self)
    if self.m_args.onCancel then
        self.m_args.onCancel()
    end
    UIManager:Close(PANEL_ID)
end

CommonBatchLockCtrl._Refresh = HL.Method() << function(self)
    local selectedId = self.m_selectedId
    local function applyIsOn(actions, cache)
        if cache == nil then
            return
        end
        cache:Update(function(cell, index)
            local action = actions[index]
            if action and cell and cell.toggle then
                cell.toggle:SetIsOnWithoutNotify(action.id == selectedId)
                self:_OnTagCellSelectedChanged(cell, cell.toggle.isOn, cell.toggle.isNaviTarget)
            end
        end)
    end
    applyIsOn(self.m_unlockActions, self.m_unlockTagCells)
    applyIsOn(self.m_lockActions, self.m_lockTagCells)


    self.view.confirmBtn.interactable = selectedId ~= nil
end

HL.Commit(CommonBatchLockCtrl)
