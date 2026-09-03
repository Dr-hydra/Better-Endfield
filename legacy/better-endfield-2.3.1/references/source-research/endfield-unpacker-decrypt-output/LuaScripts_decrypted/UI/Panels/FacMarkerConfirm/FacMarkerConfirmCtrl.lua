
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacMarkerConfirm

FacMarkerConfirmCtrl = HL.Class('FacMarkerConfirmCtrl', uiCtrl.UICtrl)

FacMarkerConfirmCtrl.s_lastSelectIconKey = HL.StaticField(HL.Table)

FacMarkerConfirmCtrl.m_curTypeIndex = HL.Field(HL.Number) << -1

FacMarkerConfirmCtrl.m_iconTabInfo = HL.Field(HL.Table)

FacMarkerConfirmCtrl.m_typeCells = HL.Field(HL.Forward('UIListCache'))

FacMarkerConfirmCtrl.m_showIconList = HL.Field(HL.Table)

FacMarkerConfirmCtrl.m_selectedIconKey = HL.Field(HL.Table)

FacMarkerConfirmCtrl.m_selectedIconNode = HL.Field(HL.Table)

FacMarkerConfirmCtrl.m_getCell = HL.Field(HL.Function)

FacMarkerConfirmCtrl.m_waitingNaviFirst = HL.Field(HL.Boolean) << false

FacMarkerConfirmCtrl.m_hideKey = HL.Field(HL.Number) << -1





FacMarkerConfirmCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SQUAD_INFIGHT_CHANGED] = 'OnSquadInfightChanged',
    [MessageConst.ON_PREPARE_NARRATIVE] = 'ExitPanelForCS',
    [MessageConst.ON_SCENE_LOAD_START] = 'ExitPanelForCS',
    [MessageConst.ALL_CHARACTER_DEAD] = 'ExitPanelForCS',
    [MessageConst.ON_TELEPORT_SQUAD] = 'ExitPanelForCS',
    [MessageConst.PLAY_CG] = 'ExitPanelForCS',
    [MessageConst.ON_PLAY_CUTSCENE] = 'ExitPanelForCS',
    [MessageConst.ON_DIALOG_START] = 'ExitPanelForCS',
    [MessageConst.ON_REPATRIATE] = 'ExitPanelForCS',
}


FacMarkerConfirmCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local onConfirm = arg.onConfirm
    local onClose = arg.onClose
    local reset = arg.reset
    self.view.closeButton.onClick:AddListener(function()
        if reset and FacMarkerConfirmCtrl.s_lastSelectIconKey ~= nil then
            local icon1 = FacMarkerConfirmCtrl.s_lastSelectIconKey[1] or 0
            local icon2 = FacMarkerConfirmCtrl.s_lastSelectIconKey[2] or 0
            local icon3 = FacMarkerConfirmCtrl.s_lastSelectIconKey[3] or 0
            GameInstance.remoteFactoryManager:SetPreviewSignBuildingIcon(icon1, icon2, icon3)
        end
        self:_SafePlayOut(function()
            self:Close()
            if onClose then
                onClose()
            end
        end)
    end)
    self.view.btnCommon.onClick:AddListener(function()
        if #self.m_selectedIconKey == 0 then
            return
        end
        FacMarkerConfirmCtrl.s_lastSelectIconKey = lume.clone(self.m_selectedIconKey)
        GameInstance.remoteFactoryManager.interact:SyncBuildSignIconParam(self.m_selectedIconKey)
        self:_SafePlayOut(function()
            self:Close()
            if onConfirm then
                onConfirm()
            end
        end)
    end)
    self:_ClearSpecificPanel()

    self.m_selectedIconNode = {}
    for i = 1, FacConst.SOCIAL_ICON_MAX_COUNT do
        self.m_selectedIconNode[i] = self.view["selected" .. i]
    end
    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)

    if reset and FacMarkerConfirmCtrl.s_lastSelectIconKey ~= nil then
        self.m_selectedIconKey = lume.clone(FacMarkerConfirmCtrl.s_lastSelectIconKey)
    else
        self.m_selectedIconKey = {}
    end
    self:_UpdateSelectedIcon()

    self:_InitTypeData()
    self:_InitTypeList()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end








FacMarkerConfirmCtrl._SafePlayOut = HL.Method(HL.Function) << function(self, callback)
    if self:IsPlayingAnimationOut() then
        return
    end
    if self.m_hideKey ~= -1 then
        self:_RecoverSpecificPanel()
    end
    self:PlayAnimationOutWithCallback(callback)
end

FacMarkerConfirmCtrl.OnSquadInfightChanged = HL.Method(HL.Opt(HL.Any)) << function(self)
    local inFight = Utils.isInFight()
    if inFight then
        self:_SafePlayOut(function()
            self:Close()
            Notify(MessageConst.FAC_BUILD_EXIT_CUR_MODE, true)
        end)
    end
end

FacMarkerConfirmCtrl.ExitPanelForCS = HL.Method(HL.Opt(HL.Any)) << function(self)
    self:_SafePlayOut(function()
        self:Close()
        Notify(MessageConst.FAC_BUILD_EXIT_CUR_MODE, true)
    end)
end

FacMarkerConfirmCtrl._ClearSpecificPanel = HL.Method() << function(self)
    if self.m_hideKey ~= -1 then
        return
    end
    local exceptedPanels = {
        PANEL_ID,
        PanelId.FacBuildMode,
    }
    if not DeviceInfo.usingTouch then
        table.insert(exceptedPanels, PanelId.Joystick)
    end
    self.m_hideKey = UIManager:ClearScreen(exceptedPanels)
end

FacMarkerConfirmCtrl._RecoverSpecificPanel = HL.Method() << function(self)
    self.m_hideKey = UIManager:RecoverScreen(self.m_hideKey)
end

FacMarkerConfirmCtrl._InitTypeData = HL.Method() << function(self)
    self.m_curTypeIndex = 1
    self.m_iconTabInfo = {
        {
            name = Language.LUA_FAC_ALL,
            icon = "Factory/WorkshopCraftTypeIcon/icon_type_all",
            items = {},
            priority = math.maxinteger,
        }
    }
    local tabInfos = {}
    for id, data in pairs(Tables.socialBuildingSignTable) do
        if tabInfos[data.tab] == nil then
            local tabData = Tables.socialBuildingSignTabTable:GetValue(data.tab)
            tabInfos[data.tab] = {
                name = tabData.name,
                icon = tabData.icon,
                items = {},
                priority = tabData.priority,
            }
        end
        table.insert(tabInfos[data.tab].items, {
            id = data.signId,
            name = data.text,
            icon = data.uiIconKey,
            sortId = data.sortId,
        })
    end
    for _, tab in pairs(tabInfos) do
        table.insert(self.m_iconTabInfo, tab)
    end
    table.sort(self.m_iconTabInfo, Utils.genSortFunction({ "priority" }))
    local sortFunc = Utils.genSortFunction({ "sortId" })
    for k = 2, #self.m_iconTabInfo do
        local tabInfo = self.m_iconTabInfo[k]
        table.sort(tabInfo.items, sortFunc)
        for _, v in ipairs(tabInfo.items) do
            table.insert(self.m_iconTabInfo[1].items, v)
        end
    end
end

FacMarkerConfirmCtrl._InitTypeList = HL.Method() << function(self)
    self.m_typeCells = UIUtils.genCellCache(self.view.typeCell)
    self.m_typeCells:Refresh(#self.m_iconTabInfo, function(cell, index)
        local info = self.m_iconTabInfo[index]


        if index == 1 then
            cell.dimIcon:LoadSprite(info.icon)
            cell.lightIcon:LoadSprite(info.icon)
        else
            cell.dimIcon:LoadSprite(UIConst.UI_SPRITE_FAC_MARKER_SETTING_ICON, info.icon)
            cell.lightIcon:LoadSprite(UIConst.UI_SPRITE_FAC_MARKER_SETTING_ICON, info.icon)
        end
        cell.name.text = info.name

        local isSelected = index == self.m_curTypeIndex
        cell.decoLine.gameObject:SetActive(not (isSelected or index == 1 or index == (self.m_curTypeIndex + 1)))
        cell.dimIcon.gameObject:SetActive(not isSelected)
        cell.lightNode.gameObject:SetActive(isSelected)

        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.isOn = isSelected
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn and self.m_curTypeIndex ~= index then
                self:_OnClickShowingType(index)
            end
        end)

        cell.gameObject.name = "TabCell_" .. index
    end)
    self:_OnClickShowingType(self.m_curTypeIndex)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.typesNode.transform)
end

FacMarkerConfirmCtrl._OnClickShowingType = HL.Method(HL.Number) << function(self, index)
    self.m_curTypeIndex = index
    self.m_showIconList = self.m_iconTabInfo[index].items
    self.m_typeCells:Update(function(cell, k)
        cell.decoLine.gameObject:SetActive(not (k == self.m_curTypeIndex or k == 1 or k == (self.m_curTypeIndex + 1)))
        cell.dimIcon.gameObject:SetActive(k ~= index)
        cell.lightNode.gameObject:SetActive(k == index)
    end)
    if DeviceInfo.usingController then
        self.m_waitingNaviFirst = true
        self.view.scrollList:UpdateCount(#self.m_showIconList)
        self.view.scrollList:ScrollToIndex(0, true)
    else
        self.view.scrollList:UpdateCount(#self.m_showIconList)
    end
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.typesNode.transform)
end

FacMarkerConfirmCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local iconData = self.m_showIconList[index]
    local iconKey = iconData.id
    cell.icon:LoadSprite(UIConst.UI_SPRITE_FAC_MARKER_SETTING_ICON, iconData.icon)
    cell.name.text = iconData.name
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_OnCellClick(iconKey)
        self.view.scrollList:UpdateShowingCells(function(csIndex, obj)
            local curIconKey = self.m_showIconList[LuaIndex(csIndex)].id
            self:_UpdateSelectedCell(self.m_getCell(obj), curIconKey)
        end)
    end)
    self:_UpdateSelectedCell(cell, iconKey)
    if self.m_waitingNaviFirst and index == 1 then
        self.m_waitingNaviFirst = false
        self:SetNaviTarget(cell.button)
    end
end

FacMarkerConfirmCtrl._UpdateSelectedCell = HL.Method(HL.Any, HL.Number) << function(self, cell, iconKey)
    local selectedIndex = lume.find(self.m_selectedIconKey, iconKey)
    cell.select.gameObject:SetActiveIfNecessary(selectedIndex ~= nil)
    cell.numBg.gameObject:SetActiveIfNecessary(selectedIndex ~= nil)
    if selectedIndex ~= nil then
        cell.numTxt.text = tostring(selectedIndex)
        cell.icon.color = self.view.config.SELECT_ICON_COLOR
    else
        cell.icon.color = self.view.config.NORMAL_ICON_COLOR
    end
    if DeviceInfo.usingController then
        local confirmText = selectedIndex and Language.LUA_SIGN_KEYHINT_UNSELECT_ICON or Language.LUA_SIGN_KEYHINT_SELECT_ICON
        InputManagerInst:SetBindingText(cell.button.hoverConfirmBindingId, confirmText)
    end
end

FacMarkerConfirmCtrl._OnCellClick = HL.Method(HL.Number) << function(self, iconKey)
    local index = lume.find(self.m_selectedIconKey, iconKey)
    if index ~= nil then
        table.remove(self.m_selectedIconKey, index)
    elseif #self.m_selectedIconKey < FacConst.SOCIAL_ICON_MAX_COUNT then
        table.insert(self.m_selectedIconKey, iconKey)
    else
        Notify(MessageConst.SHOW_TOAST, Language.LUA_MARKER_SELECT_ICON_MAX)
        return
    end
    local icon1 = self.m_selectedIconKey[1] or 0
    local icon2 = self.m_selectedIconKey[2] or 0
    local icon3 = self.m_selectedIconKey[3] or 0
    GameInstance.remoteFactoryManager:SetPreviewSignBuildingIcon(icon1, icon2, icon3)
    self:_UpdateSelectedIcon()
end

FacMarkerConfirmCtrl._UpdateSelectedIcon = HL.Method() << function(self)
    local combineText = ""
    for i = 1, FacConst.SOCIAL_ICON_MAX_COUNT do
        local iconKey = self.m_selectedIconKey[i]
        self.m_selectedIconNode[i].icon.gameObject:SetActiveIfNecessary(iconKey ~= nil)
        self.m_selectedIconNode[i].empty.gameObject:SetActiveIfNecessary(iconKey == nil)
        if iconKey ~= nil then
            local iconData = Tables.socialBuildingSignTable:GetValue(iconKey)
            self.m_selectedIconNode[i].icon:LoadSprite(UIConst.UI_SPRITE_FAC_MARKER_SETTING_ICON, iconData.uiIconKey)
            if i == 1 then
                combineText = iconData.text
            else
                combineText = I18nUtils.CombineStringWithLanguageSpilt(combineText, iconData.text)
            end
        end
    end
    self.view.selectTxt.text = combineText
    self.view.btnController:SetState(#self.m_selectedIconKey > 0 and "NormalState" or "DisableState")
    self.view.bottomTips.gameObject:SetActiveIfNecessary(#self.m_selectedIconKey == 0)
end




HL.Commit(FacMarkerConfirmCtrl)
