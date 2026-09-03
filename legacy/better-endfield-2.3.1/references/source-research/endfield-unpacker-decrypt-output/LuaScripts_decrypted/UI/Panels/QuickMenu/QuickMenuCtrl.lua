local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.QuickMenu
local PhaseLevel = require_ex('Phase/Level/PhaseLevel').PhaseLevel

local QuickMenuCellState = {
    Normal = "Normal",
    Locked = "Locked",
    Forbidden = "Forbidden",
}

local CLEAR_SCREEN_EXPECTED_PANEL_LIST = {
    PANEL_ID,
    PanelId.Joystick,
    PanelId.MissionHud,
    PanelId.MiniMap,
    PanelId.WeeklyRaidTaskTrackHud,
    PanelId.CommonTaskTrackHud,
    PanelId.CommonPOIUpgradeToast,
}

local DEFAULT_DELAY_RECOVER_SCREEN_FRAME_COUNT = 1
local EXTRA_DELAY_RECOVER_SCREEN_FRAME_COUNT = 5

local QUICK_MENU_INVALID_ITEM_ID = QuickMenuConst.QUICK_MENU_ITEM_ID_GETTER.none

QuickMenuCtrl = HL.Class('QuickMenuCtrl', uiCtrl.UICtrl)

QuickMenuCtrl.m_mainHudCtrl = HL.Field(HL.Forward("MainHudCtrl"))

QuickMenuCtrl.m_centerCells = HL.Field(HL.Forward("UIListCache"))

QuickMenuCtrl.m_quickMenuCenterItemData = HL.Field(HL.Table)

QuickMenuCtrl.m_quickMenuUpdateThread = HL.Field(HL.Thread)

QuickMenuCtrl.m_currentArrowAngle = HL.Field(HL.Number) << 0

QuickMenuCtrl.m_currentSelectedCenterItemId = HL.Field(HL.String) << ""

QuickMenuCtrl.m_quickMenuLeftItemData = HL.Field(HL.Table)

QuickMenuCtrl.m_quickMenuRightItemData = HL.Field(HL.Table)

QuickMenuCtrl.m_currentStickPushed = HL.Field(HL.Boolean) << false

QuickMenuCtrl.s_clearScreenKey = HL.StaticField(HL.Number) << -1

QuickMenuCtrl.s_releaseCloseEnabled = HL.StaticField(HL.Boolean) << true

QuickMenuCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SELECT_QUICK_MENU_SYSTEM] = '_OnSelectSystem',
}


QuickMenuCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local _, mainHudCtrl = UIManager:IsOpen(PanelId.MainHud)
    self.m_mainHudCtrl = mainHudCtrl

    self:_InitQuickMenu()
    self:_InitQuickMenuCenterCells()
    self:_InitQuickMenuLeftCells()
    self:_InitQuickMenuRightCells()

    AudioManager.PostEvent("au_ui_menu_dial_open")

    QuickMenuCtrl.ActivateQuickMenu()
end

QuickMenuCtrl.OnClose = HL.Override() << function(self)
    self.m_quickMenuUpdateThread = self:_ClearCoroutine(self.m_quickMenuUpdateThread)
    AudioManager.PostEvent("au_ui_menu_dial_close")
    QuickMenuCtrl.DeactivateQuickMenu()
end

QuickMenuCtrl.OnAnimationInFinished = HL.Override() << function(self)
    GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.QuickMenuSystemActionConflictName)
end

QuickMenuCtrl.ActivateQuickMenu = HL.StaticMethod() << function()
    if not GameInstance.player.systemActionConflictManager:TryStartSystemAction(Const.QuickMenuSystemActionConflictName) then
        return
    end

    UIManager:ClearScreenWithOutAnimation(function(clearScreenKey)
        QuickMenuCtrl.s_clearScreenKey = clearScreenKey
        local isOpen = UIManager:IsOpen(PANEL_ID)
        if not isOpen then
            QuickMenuCtrl.DeactivateQuickMenu()
        end
    end, CLEAR_SCREEN_EXPECTED_PANEL_LIST)
end

QuickMenuCtrl.DeactivateQuickMenu = HL.StaticMethod() << function()
    if QuickMenuCtrl.s_clearScreenKey <= 0 then
        return
    end
    GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.QuickMenuSystemActionConflictName)
    UIManager:RecoverScreen(QuickMenuCtrl.s_clearScreenKey)
    QuickMenuCtrl.s_clearScreenKey = -1
end

QuickMenuCtrl.OnToggleReleaseClose = HL.StaticMethod(HL.Any) << function(arg)
    QuickMenuCtrl.s_releaseCloseEnabled = unpack(arg)
end

QuickMenuCtrl._OnSelectSystem = HL.Method(HL.Any) << function(self, arg)
    local systemId = unpack(arg)
    self:_SelectQuickMenuItem(self.m_quickMenuCenterItemData[systemId])
end

QuickMenuCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if not active then
        self:Close()
    end
end




QuickMenuCtrl._BuildMenuItemData = HL.Method(HL.String).Return(HL.Opt(HL.Table)) << function(self, itemId)
    local cellData = { itemId = itemId }
    local itemConfig = QuickMenuConst.QUICK_MENU_ITEM_CONFIG[itemId]
    if itemConfig == nil then
        return
    end

    local phaseId = PhaseId[itemConfig.phaseId]
    local mainHudInfo
    if itemConfig.mainHudId ~= nil then
        mainHudInfo = self.m_mainHudCtrl:GetMainHudBtnInfo(itemConfig.mainHudId)
        if mainHudInfo == nil then
            logger.error("快捷轮盘中的mainHudId配置错误，无法找到对应MainHud按钮", itemId)
            return
        end
        if mainHudInfo.phaseId ~= nil then
            phaseId = mainHudInfo.phaseId
        end
    end


    if mainHudInfo ~= nil then
        cellData.onUse = function()
            self.m_mainHudCtrl:OnMainHudBtnClick(mainHudInfo)
        end
    else
        cellData.onUse = function()
            if itemConfig.onUse ~= nil then
                itemConfig.onUse()
            else
                if phaseId ~= nil then
                    PhaseManager:OpenPhase(phaseId)
                else
                    logger.error("快捷轮盘无可用的使用回调", itemId)
                end
            end
        end
    end


    local name, icon
    if mainHudInfo ~= nil and mainHudInfo.iconSpriteGetter ~= nil then
        icon = self:LoadSprite(UIConst.UI_SPRITE_MAIN_HUD, mainHudInfo.iconSpriteGetter())
    end
    if phaseId ~= nil then
        local systemViewConfig = PhaseManager:GetPhaseSystemViewConfig(phaseId)
        if systemViewConfig ~= nil then
            name = systemViewConfig.systemName
            if icon == nil then
                icon = self:LoadSprite(UIConst.UI_SPRITE_MAIN_HUD, systemViewConfig.systemIcon)
            end
        end
    end
    if itemConfig.nameTextId ~= nil then
        if type(itemConfig.nameTextId) == "string" then
            name = Language[itemConfig.nameTextId]
        else
            local nameTextId = itemConfig.nameTextId()
            if nameTextId ~= nil then
                name = Language[nameTextId]
            end
        end
    end
    if itemConfig.iconId ~= nil then
        if type(itemConfig.iconId) == "string" then
            icon = self:LoadSprite(UIConst.UI_SPRITE_MAIN_HUD, itemConfig.iconId)
        else
            local iconId = itemConfig.iconId()
            if iconId ~= nil then
                icon = self:LoadSprite(UIConst.UI_SPRITE_MAIN_HUD, iconId)
            end
        end
    end
    if name == nil or icon == nil then
        logger.error("快捷轮盘没有配置名称或图标", itemId)
        return
    end
    cellData.name = name
    cellData.icon = icon


    cellData.redDotName = itemConfig.redDotName
    if cellData.redDotName == nil then
        if mainHudInfo ~= nil and mainHudInfo.redDotName ~= nil then
            cellData.redDotName = mainHudInfo.redDotName
        else
            if phaseId ~= nil then
                cellData.redDotName = PhaseManager:GetPhaseRedDotName(phaseId)
            else
                cellData.redDotName = itemConfig.redDotName
            end
        end
    end


    if QuickMenuConst.QUICK_MENU_AUDIO[itemId] ~= nil then
        cellData.onPressAudio = QuickMenuConst.QUICK_MENU_AUDIO[itemId]
    else
        cellData.onPressAudio = "Au_UI_Button_Common"
    end


    local isLocked, isForbidden = false, false
    if phaseId ~= nil and itemConfig.ignorePhaseOpenCheck ~= true then
        isLocked = not PhaseManager:IsPhaseUnlocked(phaseId)
        isForbidden = not PhaseManager:CheckCanOpenPhase(phaseId)
    end
    if mainHudInfo ~= nil then

        isForbidden = isForbidden or not self.m_mainHudCtrl:IsMainHudBtnVisible(mainHudInfo)
    end
    if itemConfig.getIsLocked ~= nil then
        isLocked = isLocked or itemConfig.getIsLocked()
    end
    if itemConfig.getIsForbidden ~= nil then
        isForbidden = isForbidden or itemConfig.getIsForbidden()
    end
    cellData.isLocked = isLocked
    cellData.isForbidden = isForbidden

    return cellData
end

QuickMenuCtrl._BuildMenuItemDataListAndGetValid = HL.Method(HL.Any).Return(HL.Table, HL.Table) << function(self, itemList)
    if not itemList then
        return nil, nil
    end
    local firstValidItemData, lastItemIgnoreForbiddenData
    for _, itemId in ipairs(itemList) do
        local itemData = self:_BuildMenuItemData(itemId)
        if itemData and not itemData.isLocked then
            if not itemData.isForbidden and firstValidItemData == nil then
                firstValidItemData = itemData
            end
            lastItemIgnoreForbiddenData = itemData
        end
    end
    return firstValidItemData, lastItemIgnoreForbiddenData
end

QuickMenuCtrl._InitQuickMenuCenterCells = HL.Method() << function(self)
    self.m_quickMenuCenterItemData = {}
    local itemCount = #QuickMenuConst.QUICK_MENU_CENTER_ITEM_LIST
    if itemCount == nil or itemCount <= 0 then
        return
    end

    local rotateAngle = 360 / itemCount
    self.m_centerCells:Refresh(itemCount, function(centerCell, luaIndex)
        local itemCell = centerCell.quickMenuCell
        local targetAngle = rotateAngle * (luaIndex - 1)
        local angleLeftBound, angleRightBound  = targetAngle - rotateAngle / 2, targetAngle + rotateAngle / 2
        centerCell.transform.localEulerAngles = Vector3(0, 0, -targetAngle)
        centerCell.gameObject:SetActive(true)
        itemCell.iconNode.localEulerAngles = Vector3(0, 0, targetAngle)

        local centerItemData = {
            centerCell = centerCell,
            itemCell = itemCell,
            angleLeftBound = angleLeftBound,
            angleRightBound = angleRightBound,
        }

        local itemId
        if QuickMenuConst.QUICK_MENU_CENTER_ITEM_LIST[luaIndex] == QUICK_MENU_INVALID_ITEM_ID then
            itemId = QUICK_MENU_INVALID_ITEM_ID
            centerItemData.name = QUICK_MENU_INVALID_ITEM_ID
            centerItemData.isLocked = true
            itemCell.stateController:SetState(QuickMenuCellState.Locked)
        else
            local itemData
            if type(QuickMenuConst.QUICK_MENU_CENTER_ITEM_LIST[luaIndex]) == "table" then
                local first, last = self:_BuildMenuItemDataListAndGetValid(QuickMenuConst.QUICK_MENU_CENTER_ITEM_LIST[luaIndex])
                itemData = first and first or last
            else
                itemData = self:_BuildMenuItemData(QuickMenuConst.QUICK_MENU_CENTER_ITEM_LIST[luaIndex])
            end
            if itemData then
                itemId = itemData.itemId
                itemCell.nameTxt.gameObject:SetActive(false)
                self:_RefreshQuickMenuCell(itemCell, itemData)
                if itemData.isLocked then
                    itemCell.stateController:SetState(QuickMenuCellState.Locked)
                elseif itemData.isForbidden then
                    itemCell.stateController:SetState(QuickMenuCellState.Forbidden)
                else
                    itemCell.stateController:SetState(QuickMenuCellState.Normal)
                end
                centerItemData.name = itemData.name
                centerItemData.isLocked = itemData.isLocked
                centerItemData.isForbidden = itemData.isForbidden
                centerItemData.onUse = itemData.onUse
                self:_InitQuickMenuRefreshMessages(itemId, itemCell)
            else
                itemId = QUICK_MENU_INVALID_ITEM_ID
                centerItemData.name = QUICK_MENU_INVALID_ITEM_ID
                centerItemData.isLocked = true
                itemCell.stateController:SetState(QuickMenuCellState.Locked)
            end
            itemCell.gameObject.name = itemId
        end

        centerItemData.id = itemId
        self.m_quickMenuCenterItemData[itemId] = centerItemData

        centerCell.gameObject.name = string.format("QuickMenuCenterCell_%s", itemId)
    end)
end

QuickMenuCtrl._InitQuickMenuLeftCells = HL.Method() << function(self)
    self.m_quickMenuLeftItemData = {}
    local anyValidItem = false
    for index, itemList in ipairs(QuickMenuConst.QUICK_MENU_LEFT_ITEM_CELLS_LIST) do
        local validItemData = self:_BuildMenuItemDataListAndGetValid(itemList)
        local cell = self.view.quickMenuLeftCells[string.format("quickMenuCell%d", index)]
        if validItemData ~= nil then
            self:_RefreshQuickMenuCell(cell, validItemData)
            cell.nameTxt.text = validItemData.name
            cell.nameTxt.gameObject:SetActive(true)
            cell.stateController:SetState("Normal")
            self.m_quickMenuLeftItemData[index] = {
                id = validItemData.itemId,
                onUse = validItemData.onUse,
                onPressAudio = validItemData.onPressAudio,
            }
            anyValidItem = true
            self:_InitQuickMenuRefreshMessages(validItemData.itemId, cell)
        else
            cell.nameTxt.gameObject:SetActive(false)
            cell.stateController:SetState("Locked")
        end
    end

    if not anyValidItem then
        self.view.leftNode:SetState("Invalid")
        return
    end

    self.view.leftNode:SetState("Normal")

    self:BindInputPlayerAction("quickMenu_left_up_select", function()
        self:_SelectQuickMenuItem(self.m_quickMenuLeftItemData[1], true)
    end)
    self:BindInputPlayerAction("quickMenu_left_right_select", function()
        self:_SelectQuickMenuItem(self.m_quickMenuLeftItemData[2], true)
    end)
    self:BindInputPlayerAction("quickMenu_left_down_select", function()
        self:_SelectQuickMenuItem(self.m_quickMenuLeftItemData[3], true)
    end)
    self:BindInputPlayerAction("quickMenu_left_left_select", function()
        self:_SelectQuickMenuItem(self.m_quickMenuLeftItemData[4], true)
    end)

    self:BindInputPlayerAction("quickMenu_left_up_onPress", function()
        self:_QuickMenuItemOnPress(self.m_quickMenuLeftItemData[1])
    end)
    self:BindInputPlayerAction("quickMenu_left_right_onPress", function()
        self:_QuickMenuItemOnPress(self.m_quickMenuLeftItemData[2])
    end)
    self:BindInputPlayerAction("quickMenu_left_down_onPress", function()
        self:_QuickMenuItemOnPress(self.m_quickMenuLeftItemData[3])
    end)
    self:BindInputPlayerAction("quickMenu_left_left_onPress", function()
        self:_QuickMenuItemOnPress(self.m_quickMenuLeftItemData[4])
    end)
end

QuickMenuCtrl._InitQuickMenuRightCells = HL.Method() << function(self)
    self.m_quickMenuRightItemData = {}
    local anyValidItem = false
    for index, itemList in ipairs(QuickMenuConst.QUICK_MENU_RIGHT_ITEM_CELLS_LIST) do
        local validItemData = self:_BuildMenuItemDataListAndGetValid(itemList)
        local cell = self.view.quickMenuRightCells[string.format("quickMenuCell%d", index)]
        if validItemData ~= nil then
            self:_RefreshQuickMenuCell(cell, validItemData)
            cell.nameTxt.text = validItemData.name
            cell.nameTxt.gameObject:SetActive(true)
            cell.stateController:SetState("Normal")
            self.m_quickMenuRightItemData[index] = {
                id = validItemData.itemId,
                onUse = validItemData.onUse,
                onPressAudio = validItemData.onPressAudio,
            }
            anyValidItem = true
            self:_InitQuickMenuRefreshMessages(validItemData.itemId, cell)
        else
            cell.nameTxt.gameObject:SetActive(false)
            cell.stateController:SetState("Locked")
        end
    end

    if not anyValidItem then
        self.view.rightNode:SetState("Invalid")
        return
    end

    self.view.rightNode:SetState("Normal")

    self:BindInputPlayerAction("quickMenu_right_up_select", function()
        self:_SelectQuickMenuItem(self.m_quickMenuRightItemData[1], true)
    end)
    self:BindInputPlayerAction("quickMenu_right_right_select", function()
        self:_SelectQuickMenuItem(self.m_quickMenuRightItemData[2], true)
    end)
    self:BindInputPlayerAction("quickMenu_right_down_select", function()
        self:_SelectQuickMenuItem(self.m_quickMenuRightItemData[3], true)
    end)
    self:BindInputPlayerAction("quickMenu_right_left_select", function()
        self:_SelectQuickMenuItem(self.m_quickMenuRightItemData[4], true)
    end)

    self:BindInputPlayerAction("quickMenu_right_up_onPress", function()
        self:_QuickMenuItemOnPress(self.m_quickMenuRightItemData[1])
    end)
    self:BindInputPlayerAction("quickMenu_right_right_onPress", function()
        self:_QuickMenuItemOnPress(self.m_quickMenuRightItemData[2])
    end)
    self:BindInputPlayerAction("quickMenu_right_down_onPress", function()
        self:_QuickMenuItemOnPress(self.m_quickMenuRightItemData[3])
    end)
    self:BindInputPlayerAction("quickMenu_right_left_onPress", function()
        self:_QuickMenuItemOnPress(self.m_quickMenuRightItemData[4])
    end)
end

QuickMenuCtrl._InitQuickMenuRefreshMessages = HL.Method(HL.String, HL.Table) << function(self, itemId, itemCell)
    local itemConfig = QuickMenuConst.QUICK_MENU_ITEM_CONFIG[itemId]
    if itemConfig ~= nil and itemConfig.refreshMessageList ~= nil then
        for _, message in pairs(itemConfig.refreshMessageList) do
            MessageManager:Register(message, function(msgArg)
                local itemData = self:_BuildMenuItemData(itemId)
                if itemData == nil then
                    return
                end
                self:_RefreshQuickMenuCell(itemCell, itemData)
            end, self)
        end
    end
end






QuickMenuCtrl._InitQuickMenu = HL.Method() << function(self)
    self.m_quickMenuUpdateThread = self:_StartCoroutine(function()
        while true do
            coroutine.step()
            local lastSelectedId = self.m_currentSelectedCenterItemId
            self:_UpdateSelectArrowState()
            self:_UpdateSelectItemState()
            local needUpdateSystemInfo = lastSelectedId ~= self.m_currentSelectedCenterItemId

            if not self.m_isClosed then
                if needUpdateSystemInfo then
                    self:_UpdateSystemInfo()
                end
                self:_UpdateQuickMenuState()
            end
        end
    end)

    self.m_centerCells = UIUtils.genCellCache(self.view.quickMenuCenterCell)

    self:BindInputPlayerAction("quickMenu_cancel_selected_system", function()
        if not QuickMenuCtrl.s_releaseCloseEnabled then
            return
        end
        self:PlayAnimationOutAndClose()
    end)

    self.view.tipsTxt.text = Language["LUA_QUICK_MENU_CENTER_SELECT"]

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.view.selectArrow.gameObject:SetActive(false)
end

QuickMenuCtrl._QuickMenuItemOnPress = HL.Method(HL.Opt(HL.Any)) << function(self, itemData)
    local isValid = true
    if self.m_currentSelectedCenterItemId == QUICK_MENU_INVALID_ITEM_ID then
        isValid = false
    elseif itemData == nil then
        isValid = false
    elseif itemData.isLocked or itemData.isForbidden then
        isValid = false
    end

    if isValid then
        if itemData.onPressAudio ~= nil then
            AudioAdapter.PostEvent(itemData.onPressAudio)
        else
            AudioAdapter.PostEvent("Au_UI_Button_Common")
        end
    end

end

QuickMenuCtrl._SelectQuickMenuItem = HL.Method(HL.Opt(HL.Any, HL.Boolean)) << function(self, itemData, onlyShowToastIfInvalid)
    if QuickMenuCtrl.s_clearScreenKey <= 0 then
        return
    end

    local isValid = true
    if self.m_currentSelectedCenterItemId == QUICK_MENU_INVALID_ITEM_ID then
        isValid = false
    elseif itemData == nil then
        isValid = false
    elseif itemData.isLocked or itemData.isForbidden then
        isValid = false
    end

    if not isValid then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_INVALID_SYSTEM_COMMON_DESCRIPTION)
        if onlyShowToastIfInvalid then
            return
        end
    end

    local clearScreenKey
    if isValid then

        clearScreenKey = UIManager:ClearScreen({ PANEL_ID })
        if not clearScreenKey then

            return
        end

        PhaseLevel.s_forceTransitionBehindFastMode = true
    end
    if self:IsShow() then
        self:Close()
    end
    if isValid then
        itemData.onUse()
        local delayFrameCount = self:_IsQuickMenuItemNeedExtraDelayRecoverScreen(itemData.id) and
            EXTRA_DELAY_RECOVER_SCREEN_FRAME_COUNT or
            DEFAULT_DELAY_RECOVER_SCREEN_FRAME_COUNT
        TimerManager:StartFrameTimer(delayFrameCount, function()
            UIManager:RecoverScreen(clearScreenKey)
            PhaseLevel.s_forceTransitionBehindFastMode = false
        end)
    end
end

QuickMenuCtrl._GetQuickMenuItemIsInConfig = HL.Method(HL.Table, HL.Number).Return(HL.Boolean) << function(self, configTable, phaseId)
    if configTable == nil then
        return
    end

    for _, id in ipairs(configTable) do
        if id == phaseId then
            return true
        end
    end

    return false
end

QuickMenuCtrl._IsQuickMenuItemNeedExtraDelayRecoverScreen = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    local itemConfig = QuickMenuConst.QUICK_MENU_ITEM_CONFIG[itemId]
    if itemConfig == nil then
        return false
    end
    return itemConfig.needExtraDelayRecoverScreen == true
end

QuickMenuCtrl._GetQuickMenuItemIsLocked = HL.Method(HL.Number).Return(HL.Boolean) << function(self, phaseId)
    return not PhaseManager:IsPhaseUnlocked(phaseId)
end

QuickMenuCtrl._RefreshQuickMenuCell = HL.Method(HL.Table, HL.Table) << function(self, itemCell, itemData)

    itemCell.systemIcon.sprite = itemData.icon
    itemCell.systemIconShadow.sprite = itemData.icon


    local redDot = itemData.redDotName
    if not string.isEmpty(redDot) and not itemData.isLocked and not itemData.isForbidden then
        itemCell.redDot.gameObject:SetActive(true)
        itemCell.redDot:InitRedDot(redDot)
    else
        itemCell.redDot.gameObject:SetActive(false)
    end
end






QuickMenuCtrl._UpdateQuickMenuState = HL.Method() << function(self)
    local rightStickValue = InputManagerInst:GetGamepadStickValue(false)
    local useRightStick = rightStickValue.x ~= 0 or rightStickValue.y ~= 0

    if not useRightStick and not string.isEmpty(self.m_currentSelectedCenterItemId) and QuickMenuCtrl.s_releaseCloseEnabled then
        self:_SelectQuickMenuItem(self.m_quickMenuCenterItemData[self.m_currentSelectedCenterItemId])
    end
end

QuickMenuCtrl._UpdateSelectArrowState = HL.Method() << function(self)
    if QuickMenuCtrl.s_clearScreenKey <= 0 then
        return
    end

    local stickValue = InputManagerInst:GetGamepadStickValue(false)
    if stickValue.magnitude < self.view.config.RIGHT_STICK_DEAD_ZONE_VALUE then
        self.m_currentStickPushed = false
        return
    end

    local angle = 180 * (math.acos(Vector2.Dot(stickValue.normalized, Vector2(0, 1))) / math.pi)
    if stickValue.x < 0 then
        angle = 360 - angle
    end

    self.m_currentArrowAngle = angle
    self.view.selectArrow.eulerAngles = Vector3(0, 0, -angle)
    self.view.selectArrow.gameObject:SetActive(true)
    self.m_currentStickPushed = true
end

QuickMenuCtrl._UpdateSelectItemState = HL.Method() << function(self)
    if self.m_currentArrowAngle == 0 and not self.m_currentStickPushed then
        if not self.m_currentSelectedCenterItemId == QUICK_MENU_INVALID_ITEM_ID then
            local itemData = self.m_quickMenuCenterItemData[self.m_currentSelectedCenterItemId]
            if itemData ~= nil and itemData.itemCell ~= nil then
                itemData.centerCell.selectedMark.gameObject:SetActive(false)
            end
            self.m_currentSelectedCenterItemId = QUICK_MENU_INVALID_ITEM_ID
        end
        return
    end

    local triggerItemId
    for itemId, itemData in pairs(self.m_quickMenuCenterItemData) do
        local centerCell = itemData.centerCell
        local angleLeftBound, angleRightBound = itemData.angleLeftBound, itemData.angleRightBound
        local isSelected = false
        if angleLeftBound < 0 then
            angleLeftBound = 360 + angleLeftBound
            isSelected = (self.m_currentArrowAngle >= angleLeftBound and self.m_currentArrowAngle <= 360) or
                (self.m_currentArrowAngle >= 0 and self.m_currentArrowAngle < angleRightBound)
        else
            isSelected = self.m_currentArrowAngle >= angleLeftBound and self.m_currentArrowAngle < angleRightBound
        end

        if centerCell ~= nil and centerCell.selectedMark ~= nil then
            centerCell.selectedMark.gameObject:SetActive(isSelected)
        end

        if isSelected then
            if self.m_currentSelectedCenterItemId ~= itemId then
                triggerItemId = itemId
            end
            self.m_currentSelectedCenterItemId = itemId
        end
    end

    if not string.isEmpty(triggerItemId) then
        CS.Beyond.Gameplay.Conditions.OnQuickMenuSystemHover.Trigger(triggerItemId)
    end
end

QuickMenuCtrl._UpdateSystemInfo = HL.Method() << function(self)
    if self.view == nil then
        return
    end
    local tipsTextId = string.isEmpty(self.m_currentSelectedCenterItemId) and "LUA_QUICK_MENU_CENTER_SELECT" or "LUA_QUICK_MENU_CENTER_CONFIRM"
    self.view.tipsTxt.text = Language[tipsTextId]

    local itemData = self.m_quickMenuCenterItemData[self.m_currentSelectedCenterItemId]
    if itemData == nil then
        self.view.systemInfo.gameObject:SetActive(false)
        return
    end

    local name = itemData.isLocked and Language.LUA_LOCKED_SYSTEM_TITLE or itemData.name
    self.view.systemNameTxt.text = name
    self.view.systemInfo.gameObject:SetActive(true)

    AudioManager.PostEvent("au_ui_hover_dial")
end




HL.Commit(QuickMenuCtrl)
