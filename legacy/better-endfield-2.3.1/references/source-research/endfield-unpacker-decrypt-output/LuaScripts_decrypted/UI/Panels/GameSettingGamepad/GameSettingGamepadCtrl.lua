local DeviceControllerType = CS.Beyond.DeviceInfo.ControllerType
local InputSettingLevel = CS.Beyond.Input.InputSettingLevel
local InputDeviceFlags = CS.Beyond.Input.InputDeviceFlags
local GamepadKeyCode = CS.Beyond.Input.GamepadKeyCode

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GameSettingGamepad

GameSettingGamepadCtrl = HL.Class('GameSettingGamepadCtrl', uiCtrl.UICtrl)





GameSettingGamepadCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONTROLLER_TYPE_CHANGED] = "_OnControllerTypeChanged",
}

GameSettingGamepadCtrl.m_system = HL.Field(HL.Userdata)

GameSettingGamepadCtrl.m_itemDataList = HL.Field(HL.Table)

GameSettingGamepadCtrl.m_itemDataToKeyCode = HL.Field(HL.Table)

GameSettingGamepadCtrl.m_keyCodeToItemData = HL.Field(HL.Table)

GameSettingGamepadCtrl.m_itemCells = HL.Field(HL.Forward("UIListCache"))

GameSettingGamepadCtrl.m_itemCellHeight = HL.Field(HL.Number) << 0

GameSettingGamepadCtrl.m_itemCellHeightWithoutTitle = HL.Field(HL.Number) << 0

GameSettingGamepadCtrl.m_itemControlConfigs = HL.Field(HL.Table)

GameSettingGamepadCtrl.m_dropdownListMaskOriginalPadding = HL.Field(HL.Number) << 0

GameSettingGamepadCtrl.m_contentHeight = HL.Field(HL.Number) << 0


GameSettingGamepadCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:_OnCloseBtnClick()
    end)

    self.view.resetBtn.onClick:AddListener(function()
        self:_OnResetBtnClick()
    end)
    self.view.saveBtn.onClick:AddListener(function()
        self:_OnSaveBtnClick()
    end)

    local itemCell = self.view.settingItemCell
    self.m_itemCells = UIUtils.genCellCache(itemCell)
    self.m_itemCellHeight = itemCell.transform.rect.height
    self.m_itemCellHeightWithoutTitle = self.m_itemCellHeight - itemCell.view.titleNode.rect.height

    local settingItemControls = self.view.settingItemControls
    self.m_itemControlConfigs = {
        [GEnums.SettingItemType.Dropdown] = {
            template = settingItemControls.dropDownSetting.gameObject,
            initializer = function(...)
                self:_InitSettingItemControlDropdown(...)
            end,
        },
    }
    self.m_dropdownListMaskOriginalPadding = settingItemControls.dropDownSetting.dropDownListMask.padding.w

    self.m_system = GameInstance.player.gameSettingSystem

    self.m_itemDataList = {}
    for settingId, itemData in pairs(Tables.gamepadSettingItemTable) do
        table.insert(self.m_itemDataList, itemData)
    end
    table.sort(self.m_itemDataList, function(lhs, rhs)
        return lhs.settingSortOrder < rhs.settingSortOrder
    end)

    self.m_itemDataToKeyCode = {}
    self.m_keyCodeToItemData = {}

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

GameSettingGamepadCtrl.OnClose = HL.Override() << function(self)

    local isAnyDirty = self:_IsAnySettingItemDirty()
    if isAnyDirty then
        self:_ClearPendingSettings()
    end
end

GameSettingGamepadCtrl.OnShow = HL.Override() << function(self)
    self:_RefreshView(true)
    self:_SetNaviTarget()
end

GameSettingGamepadCtrl._CloseSelf = HL.Method() << function(self)
    self:PlayAnimationOutWithCallback(function()
        self.m_phase:RemovePhasePanelItemById(PanelId.GameSettingGamepad)
    end)
end

GameSettingGamepadCtrl._BuildKeyCodeMapping = HL.Method() << function(self)
    lume.clear(self.m_itemDataToKeyCode)
    lume.clear(self.m_keyCodeToItemData)

    for settingId, itemData in pairs(Tables.gamepadSettingItemTable) do
        local keyCode = self.m_system:GetKeySettingGamepadKeyCode(settingId)
        if keyCode ~= GamepadKeyCode.None then
            self.m_itemDataToKeyCode[itemData] = {
                default = self.m_system:GetKeySettingGamepadKeyCode(settingId, InputSettingLevel.Default),
                original = keyCode,
                current = keyCode,
            }
            self.m_keyCodeToItemData[keyCode] = itemData
        end
    end
end

GameSettingGamepadCtrl._UpdateKeyCodeMapping = HL.Method(HL.Userdata, HL.Userdata) << function(self, itemData, keyCode)
    self.m_itemDataToKeyCode[itemData].current = keyCode
    self.m_keyCodeToItemData[keyCode] = itemData
end

GameSettingGamepadCtrl._GetKeyCodeBySettingItem = HL.Method(HL.Userdata).Return(HL.Userdata) << function(self, itemData)
    local keyCode = self.m_itemDataToKeyCode[itemData]
    return keyCode.current
end

GameSettingGamepadCtrl._GetSettingItemByKeyCode = HL.Method(HL.Userdata).Return(HL.Userdata) << function(self, keyCode)
    return self.m_keyCodeToItemData[keyCode]
end

GameSettingGamepadCtrl._IsSettingItemDirty = HL.Method(HL.Userdata).Return(HL.Boolean) << function(self, itemData)
    local keyCode = self.m_itemDataToKeyCode[itemData]
    return keyCode.current ~= keyCode.original
end

GameSettingGamepadCtrl._IsAnySettingItemDirty = HL.Method().Return(HL.Boolean) << function(self)
    for itemData, keyCode in pairs(self.m_itemDataToKeyCode) do
        if keyCode.current ~= keyCode.original then
            return true
        end
    end
    return false
end

GameSettingGamepadCtrl._IsAllSettingItemsDefault = HL.Method().Return(HL.Boolean) << function(self)
    for itemData, keyCode in pairs(self.m_itemDataToKeyCode) do
        if keyCode.current ~= keyCode.default then
            return false
        end
    end
    return true
end

GameSettingGamepadCtrl._ClearPendingSettings = HL.Method() << function(self)

    GameInstance.player.gameSettingSystem:ClearAllPendingKeySettings()
end

GameSettingGamepadCtrl._ResetAllSettings = HL.Method() << function(self)

    GameInstance.player.gameSettingSystem:ResetAllKeySettings(InputDeviceFlags.Gamepad)

    GameInstance.player.gameSettingSystem:SaveSetting()
    Notify(MessageConst.ON_GAME_SETTING_CHANGED, UIConst.GameSettingChangeReason.Gamepad)
end

GameSettingGamepadCtrl._RefreshView = HL.Method(HL.Boolean) << function(self, rebuildData)
    if rebuildData then
        self:_BuildKeyCodeMapping()
    end

    self.m_contentHeight = 0

    local itemCount = #self.m_itemDataList
    self.m_itemCells:Refresh(itemCount, function(itemCell, itemIndex)
        self:_RefreshSettingItemCell(itemCell, itemIndex)
    end)

    local keyScale = self.view.config.DROPDOWN_OPTION_KEY_IMAGE_TEXT_SCALE
    local tipsText = string.format(Language["ui_set_gamesetting_controller_customization_hint"],
        UIUtils.getGamepadKeyImageText(GamepadKeyCode.LB, false, keyScale),
        UIUtils.getGamepadKeyImageText(GamepadKeyCode.RB, false, keyScale),
        UIUtils.getGamepadKeyImageText(GamepadKeyCode.LT, false, keyScale))
    self.view.tipsText:SetAndResolveTextStyle(tipsText)
end

GameSettingGamepadCtrl._RefreshSettingItemCell = HL.Method(HL.Userdata, HL.Number) << function(self, itemCell, itemIndex)
    local itemData = self.m_itemDataList[itemIndex]

    itemCell:InitGameSettingItemCell(itemData, self.m_itemControlConfigs)


    local showTitle = not string.isEmpty(itemData.settingGroupTitle)
    local cellHeight = showTitle and self.m_itemCellHeight or self.m_itemCellHeightWithoutTitle
    UIUtils.setSizeDeltaY(itemCell.rectTransform, cellHeight)

    if itemIndex > 1 then

        if showTitle then
            cellHeight = cellHeight + self.view.config.SETTING_ITEM_TITLE_PADDING_TOP
        end

        self.m_contentHeight = self.m_contentHeight + self.view.config.SETTING_ITEM_VERTICAL_SPACE
    end

    self.m_contentHeight = self.m_contentHeight + cellHeight

    local position = itemCell.rectTransform.anchoredPosition
    itemCell.rectTransform.anchoredPosition = Vector2(
        position.x,
        -self.m_contentHeight
    )
end

GameSettingGamepadCtrl._InitSettingItemControlDropdown = HL.Method(HL.Userdata) << function(self, itemCell)
    local itemData = itemCell.itemData
    local itemControl = itemCell.itemControl

    local optionTextList = self:_DropdownGetOptionTextList(itemData)

    itemControl.dropdown:ClearComponent()
    itemControl.dropdown:Init(function(csIndex, option, isSelected)
        local index = LuaIndex(csIndex)
        option:SetText(optionTextList[index])
    end, function(csIndex)
        self:_DropdownOnSelectOption(itemData, LuaIndex(csIndex))
    end)

    itemControl.dropdown.onToggleOptList:AddListener(function(active)
        if active then
            self:_DropdownAdjustExpandDirection(itemCell)
            itemControl.dropdown:ScrollToSelected()
        end
    end)

    local initIndex = self:_DropdownGetOptionIndex(itemData)
    itemControl.dropdown:Refresh(#optionTextList, CSIndex(initIndex), false)

    local isDirty = self:_IsSettingItemDirty(itemData)
    itemControl.dirtyNode.gameObject:SetActive(isDirty)
end

GameSettingGamepadCtrl._DropdownAdjustExpandDirection = HL.Method(HL.Userdata) << function(self, itemCell)
    local itemControl = itemCell.itemControl


    itemCell.rectTransform:SetSiblingIndex(itemCell.rectTransform.parent.childCount - 1)


    local viewRect = self.view.scrollView.transform
    local cellRect = itemControl.rectTransform
    local listRect = itemControl.dropDownListRectTransform
    local listMask = itemControl.dropDownListMask

    local cellRectMax = cellRect.rect.max
    local cellRectMaxWorldPos = cellRect:TransformPoint(Vector3(cellRectMax.x, cellRectMax.y, 0))
    local cellRectMaxLocalPos = viewRect:InverseTransformPoint(cellRectMaxWorldPos)
    local isInUpperHalf = (cellRectMaxLocalPos.y - listRect.rect.height) >= viewRect.rect.yMin

    listRect.pivot = isInUpperHalf and Vector2(0.5, 1.0) or Vector2(0.5, 0)
    listRect.anchorMin = isInUpperHalf and Vector2(0, 1.0) or Vector2(0, 0)
    listRect.anchorMax = isInUpperHalf and Vector2(1.0, 1.0) or Vector2(1.0, 0)
    listRect.anchoredPosition = Vector2.zero

    local maskPadding = listMask.padding
    maskPadding.w = isInUpperHalf and self.m_dropdownListMaskOriginalPadding or 0
    maskPadding.y = isInUpperHalf and 0 or self.m_dropdownListMaskOriginalPadding
    listMask.padding = maskPadding
    itemControl.topMask.gameObject:SetActive(not isInUpperHalf)
    itemControl.bottomMask.gameObject:SetActive(isInUpperHalf)

    itemControl.layoutStateCtrl:SetState(isInUpperHalf and "Downward" or "Upward")
end

GameSettingGamepadCtrl._DropdownGetOptionTextList = HL.Method(HL.Userdata).Return(HL.Table) << function(self, itemData)
    local optionTextList = {}
    local keyScale = self.view.config.DROPDOWN_OPTION_KEY_IMAGE_TEXT_SCALE
    local usedKeyAlpha = self.view.config.DROPDOWN_OPTION_USED_KEY_IMAGE_TEXT_ALPHA

    local textBuilder = {}
    local options = itemData.options
    for i = 0, options.Count - 1 do
        local optionData = options[i]
        local optionActionKeys = optionData.optionActionKeys

        local checkKeyCodeAvailable = optionActionKeys.Count > 1
        for j = 0, optionActionKeys.Count - 1 do
            local keyStr = optionActionKeys[j]
            local keyCode = GamepadKeyCode[keyStr]

            local keyColor = Color.white
            if checkKeyCodeAvailable then
                local usingItemData = self:_GetSettingItemByKeyCode(keyCode)
                if usingItemData and usingItemData.actionScope ~= itemData.actionScope then
                    keyColor.a = usedKeyAlpha
                end
            end

            local keyText = UIUtils.getGamepadKeyImageText(keyCode, false, keyScale, keyColor)
            table.insert(textBuilder, keyText)
        end

        local optionText = table.concat(textBuilder, "/")
        lume.clear(textBuilder)
        table.insert(optionTextList, optionText)
    end

    return optionTextList
end

GameSettingGamepadCtrl._DropdownGetOptionIndex = HL.Method(HL.Userdata).Return(HL.Number) << function(self, itemData)
    local options = itemData.options
    local currKeyCode = self:_GetKeyCodeBySettingItem(itemData)
    for i = 0, options.Count - 1 do
        local optionData = options[i]
        local optionActionKeys = optionData.optionActionKeys
        for j = 0, optionActionKeys.Count - 1 do
            local keyStr = optionActionKeys[j]
            local keyCode = GamepadKeyCode[keyStr]
            if currKeyCode == keyCode then
                return LuaIndex(i)
            end
        end
    end

    logger.error(ELogChannel.GameSetting, "Failed to get gamepad setting option index, settingId: " .. itemData.settingId)
    return 1
end

GameSettingGamepadCtrl._DropdownOnSelectOption = HL.Method(HL.Userdata, HL.Number) << function(self, itemData, optionIndex)
    local targetKeyCode
    local targetItemData

    local optionData = itemData.options[CSIndex(optionIndex)]
    local optionActionKeys = optionData.optionActionKeys

    local checkKeyCodeUsed = optionActionKeys.Count > 1
    for i = 0, optionActionKeys.Count - 1 do
        local keyStr = optionActionKeys[i]
        local keyCode = GamepadKeyCode[keyStr]
        if checkKeyCodeUsed then
            local usingItemData = self:_GetSettingItemByKeyCode(keyCode)
            if usingItemData == nil then
                targetKeyCode = keyCode
                break
            end
            if usingItemData.actionScope == itemData.actionScope then
                targetKeyCode = keyCode
                targetItemData = usingItemData
                break
            end
        else
            targetKeyCode = keyCode
            targetItemData = self:_GetSettingItemByKeyCode(keyCode)
            break
        end
    end

    if targetKeyCode == nil then
        logger.error(ELogChannel.GameSetting, "Target key code not found, settingId: " .. tostring(itemData.settingId))
        return
    end

    if targetItemData then
        local currKeyCode = self:_GetKeyCodeBySettingItem(itemData)
        GameInstance.player.gameSettingSystem:SetKeySetting(targetItemData.settingId, currKeyCode)
        self:_UpdateKeyCodeMapping(targetItemData, currKeyCode)
    end

    GameInstance.player.gameSettingSystem:SetKeySetting(itemData.settingId, targetKeyCode)
    self:_UpdateKeyCodeMapping(itemData, targetKeyCode)

    self:_RefreshView(false)
end

GameSettingGamepadCtrl._OnCloseBtnClick = HL.Method() << function(self)
    local isAnyDirty = self:_IsAnySettingItemDirty()
    if isAnyDirty then

        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_GAME_SETTING_GAMEPAD_QUIT_POPUP_CONTENT,
            subContent = Language.LUA_GAME_SETTING_GAMEPAD_QUIT_POPUP_SUB_CONTENT,
            onConfirm = function()

                self:_ClearPendingSettings()
                self:_CloseSelf()
            end,
        })
    else

        self:_CloseSelf()
    end
end

GameSettingGamepadCtrl._OnResetBtnClick = HL.Method() << function(self)
    local isAllDefault = self:_IsAllSettingItemsDefault()
    if isAllDefault then

        local isAnyDirty = self:_IsAnySettingItemDirty()
        if isAnyDirty then

            self:_ResetAllSettings()

            self:_RefreshView(true)
        end
        Notify(MessageConst.SHOW_TOAST, Language.LUA_GAME_SETTING_GAMEPAD_RESET_SUCCESS)
    else

        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_GAME_SETTING_GAMEPAD_RESET_POPUP_CONTENT,
            subContent = Language.LUA_GAME_SETTING_GAMEPAD_RESET_POPUP_SUB_CONTENT,
            onConfirm = function()

                self:_ResetAllSettings()

                self:_RefreshView(true)
                Notify(MessageConst.SHOW_TOAST, Language.LUA_GAME_SETTING_GAMEPAD_RESET_SUCCESS)
            end,
        })
    end
end

GameSettingGamepadCtrl._OnSaveBtnClick = HL.Method() << function(self)

    local isAnyDirty = self:_IsAnySettingItemDirty()
    if isAnyDirty then

        local onConfirm = function()

            GameInstance.player.gameSettingSystem:SaveSetting()
            Notify(MessageConst.ON_GAME_SETTING_CHANGED, UIConst.GameSettingChangeReason.Gamepad)
            self:_CloseSelf()
            Notify(MessageConst.SHOW_TOAST, Language.LUA_GAME_SETTING_GAMEPAD_SAVE_SUCCESS)
        end
        local conflict, conflictInfos, affectedInfos = self.m_system:CheckKeySettingGamepadConflict()
        if conflict then

            local args = {
                conflictInfos = conflictInfos,
                affectedInfos = affectedInfos,
                onConfirm = onConfirm,
            }
            self.m_phase:CreatePhasePanelItem(PanelId.GameSettingGamepadSavePopUp, args)
        else

            Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_GAME_SETTING_GAMEPAD_SAVE_POPUP_CONTENT,
                onConfirm = onConfirm,
            })
        end
    else

        self:_CloseSelf()
    end
end

GameSettingGamepadCtrl._SetNaviTarget = HL.Method() << function(self)
    self:ClearNaviTarget()

    local itemCount = #self.m_itemDataList
    if itemCount > 0 then
        local firstCell = self.m_itemCells:Get(1)
        self:SetNaviTarget(firstCell.view.naviDecorator)
    end
end

GameSettingGamepadCtrl._OnControllerTypeChanged = HL.Method(HL.Any) << function(self, args)
    local controllerType = DeviceInfo.controllerType
    if controllerType == DeviceControllerType.None then
        return
    end

    self:_RefreshView(false)
end

HL.Commit(GameSettingGamepadCtrl)
