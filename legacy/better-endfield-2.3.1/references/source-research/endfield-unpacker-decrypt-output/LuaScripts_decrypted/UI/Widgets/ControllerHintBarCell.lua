local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ControllerHintBarCell = HL.Class('ControllerHintBarCell', UIWidgetBase)


ControllerHintBarCell.args = HL.Field(HL.Table)

ControllerHintBarCell.keyHintCells = HL.Field(HL.Forward('UIListCache'))

ControllerHintBarCell.m_autoUpdate = HL.Field(HL.Boolean) << false

ControllerHintBarCell.m_lateTickFunc = HL.Field(HL.Function)

ControllerHintBarCell.m_virtualMouseLongPressFakeBindingId = HL.Field(HL.Number) << -1

ControllerHintBarCell.m_virtualMouseHoverTarget = HL.Field(CS.UnityEngine.UI.Selectable)



ControllerHintBarCell._OnFirstTimeInit = HL.Override() << function(self)
    self.keyHintCells = UIUtils.genCellCache(self.view.keyHint)
    self.m_lateTickFunc = function()
        if self.m_autoUpdate then
            self:RefreshAll(false)
        end
    end
    self:_RegisterTick()
end









ControllerHintBarCell.InitControllerHintBarCell = HL.Method(HL.Table, HL.Opt(HL.Boolean)) << function(self, args, autoUpdate)
    self:_FirstTimeInit()

    self.args = args
    self.m_autoUpdate = autoUpdate
end

ControllerHintBarCell.Clear = HL.Method() << function(self)
    self.args = nil
    self.m_autoUpdate = false
    self.keyHintCells:Refresh(0, nil, nil, function(cell)
        self:_ClearRedDot(cell)
        cell.actionKeyHint:SetActionId(nil)
    end)
end

ControllerHintBarCell.m_hasRegisterTick = HL.Field(HL.Boolean) << false

ControllerHintBarCell._OnEnable = HL.Override() << function(self)
    self:_RegisterTick()
end

ControllerHintBarCell._OnDisable = HL.Override() << function(self)
    self:_UnRegisterTick()
end

ControllerHintBarCell._OnDestroy = HL.Override() << function(self)
    self:_UnRegisterTick()
end

ControllerHintBarCell._RegisterTick = HL.Method() << function(self)
    if self.m_lateTickFunc and not self.m_hasRegisterTick then
        InputManagerInst.onInputLateTick = InputManagerInst.onInputLateTick + self.m_lateTickFunc
        self.m_hasRegisterTick = true
    end
end

ControllerHintBarCell._UnRegisterTick = HL.Method() << function(self)
    if self.m_lateTickFunc and self.m_hasRegisterTick then
        InputManagerInst.onInputLateTick = InputManagerInst.onInputLateTick - self.m_lateTickFunc
        self.m_hasRegisterTick = false
    end
end


ControllerHintBarCell.RefreshAll = HL.Method(HL.Boolean) << function(self, needMouseHint)
    local infoList = self:GetKeyHintInfos(needMouseHint)
    local count = #infoList
    if count > 0 then

        self.keyHintCells:Refresh(count, function(cell, index)
            local info = infoList[index]
            self:_UpdateCell(cell, info)
        end, nil, function(cell)
            self:_ClearRedDot(cell)
            cell.actionKeyHint:SetActionId(nil)
        end)
        self:RefreshBarBgVisibleState()
    else

        self.keyHintCells:Refresh(0, nil, nil, function(cell)
            self:_ClearRedDot(cell)
            cell.actionKeyHint:SetActionId(nil)
        end)
        self.view.bg.gameObject:SetActive(false)
    end
end

ControllerHintBarCell.RefreshContentOnly = HL.Method() << function(self)

    self.keyHintCells:Update(function(cell)
        cell.actionKeyHint:UpdateKeyHint()
    end)
end

ControllerHintBarCell.RefreshBarBgVisibleState = HL.Method() << function(self)
    local allHidden = true
    self.keyHintCells:Update(function(cell)
        if not cell.actionKeyHint.hintHidden then
            allHidden = false
        end
    end)
    self.view.bg.gameObject:SetActive(not allHidden)
end

ControllerHintBarCell.GetKeyHintInfos = HL.Method(HL.Boolean).Return(HL.Table) << function(self, needMouseHint)
    local args = self.args
    local infoList = {}


    local clickHintInfo, clickHintText
    if needMouseHint then
        local mouseHoverTarget = InputManagerInst.virtualMouse:GetCurHoverSelectable()
        if self.m_virtualMouseHoverTarget ~= mouseHoverTarget then
            InputManagerInst:DeleteBinding(self.m_virtualMouseLongPressFakeBindingId)
            self.m_virtualMouseLongPressFakeBindingId = -1
        end
        if mouseHoverTarget then
            local succ, clickTextId, longPressTextId = mouseHoverTarget:GetMouseActionHints()
            if succ then
                if not string.isEmpty(clickTextId) then
                    clickHintInfo = {
                        actionId = InputManager.s_virtualMouseClickHintActionId,
                        textId = clickTextId,
                    }
                    clickHintText = Language[clickTextId]
                    table.insert(infoList, clickHintInfo)
                end
                if not string.isEmpty(longPressTextId) then
                    local info = {
                        actionId = InputManager.s_virtualMouseLongPressHintActionId,
                        textId = longPressTextId,
                    }
                    if self.m_virtualMouseHoverTarget ~= mouseHoverTarget then

                        self.m_virtualMouseLongPressFakeBindingId = self:BindInputPlayerAction(InputManager.s_virtualMouseLongPressHintActionId, function() end)
                    end
                    info.bindingId = self.m_virtualMouseLongPressFakeBindingId
                    table.insert(infoList, info)
                end
            end
        end
        self.m_virtualMouseHoverTarget = mouseHoverTarget
    else
        if self.m_virtualMouseHoverTarget then
            InputManagerInst:DeleteBinding(self.m_virtualMouseLongPressFakeBindingId)
            self.m_virtualMouseLongPressFakeBindingId = -1
            self.m_virtualMouseHoverTarget = nil
        end
    end


    local infoCSList = InputManagerInst:GetEmptyControllerHintInfoList()
    infoCSList:Clear()




    local ignoreRootEnabled = false
    if GameInstance.player.guide.isInForceGuide then
        local isOpen, guideCtrl = UIManager:IsOpen(PanelId.Guide)
        if isOpen then
            ignoreRootEnabled = guideCtrl:GetBlockKeyboardEvent()
        end
    end
    for _, groupId in pairs(args.groupIds) do
        InputManagerInst:GetControllerHintInfos(groupId, ignoreRootEnabled, infoCSList)
    end
    if args.optionalActionIds then
        InputManagerInst:GetControllerHintInfos(args.optionalActionIds, infoCSList)
    end
    for _, info in pairs(infoCSList) do
        local validInfo = CS.Beyond.UI.UIActionKeyHint.s_stopCheckBindingEnabledForGuide or
                info.hintView == nil or info.hintView.bindingViewState ~= CS.Beyond.Input.BindingViewState.Hide
        if validInfo then
            table.insert(infoList, info)
        end
    end

    if args.customGetKeyHintInfos then
        args.customGetKeyHintInfos(infoList)
    end

    return infoList
end

ControllerHintBarCell._UpdateCell = HL.Method(HL.Table, HL.Any) << function(self, cell, info)
    local actionId = info.actionId
    local hintView = info.hintView
    local textId = info.textId
    local bindingId = info.bind and info.bind.id or info.bindingId

    cell.gameObject.name = actionId
    if bindingId then
        cell.actionKeyHint:SetBindingId(bindingId, actionId, hintView, true)
    else
        cell.actionKeyHint:SetKeyHint(actionId, hintView, true)
    end
    if textId then
        cell.actionKeyHint:SetText(Language[textId])
    end


    if hintView and NotNull(hintView.redDotTrans) then
        if (not cell.redDotTarget) or (cell.redDotTarget.transform ~= hintView.redDotTrans) then
            self:_ClearRedDot(cell)
            local widget = hintView.redDotTrans:GetComponent("LuaUIWidget")
            if widget.table then
                cell.redDotTarget = widget.table[1]
                cell.redDotTarget:SetKeyHintTarget(cell.redDot)
            else
                logger.error("No LuaUIWidget Table", hintView.redDotTrans:PathFromRoot())
            end
        end
    else
        self:_ClearRedDot(cell)
        cell.redDot.gameObject:SetActive(false)
    end
end

ControllerHintBarCell._ClearRedDot = HL.Method(HL.Any) << function(self, cell)
    if cell.redDotTarget then
        if NotNull(cell.redDotTarget) then
            cell.redDotTarget:SetKeyHintTarget(nil)
        end
        cell.redDotTarget = nil
    end
end


HL.Commit(ControllerHintBarCell)
return ControllerHintBarCell
