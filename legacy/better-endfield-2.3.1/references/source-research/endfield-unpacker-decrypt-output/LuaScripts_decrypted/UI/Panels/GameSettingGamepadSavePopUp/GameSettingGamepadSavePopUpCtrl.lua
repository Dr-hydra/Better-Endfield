local DeviceControllerType = CS.Beyond.DeviceInfo.ControllerType

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GameSettingGamepadSavePopUp

GameSettingGamepadSavePopUpCtrl = HL.Class('GameSettingGamepadSavePopUpCtrl', uiCtrl.UICtrl)











GameSettingGamepadSavePopUpCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONTROLLER_TYPE_CHANGED] = "_OnControllerTypeChanged",
}

GameSettingGamepadSavePopUpCtrl.m_args = HL.Field(HL.Table)

GameSettingGamepadSavePopUpCtrl.m_itemCells = HL.Field(HL.Forward("UIListCache"))


GameSettingGamepadSavePopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.view.confirmButton.onClick:AddListener(function()
        self:_OnClickButton(self.m_args.onConfirm)
    end)
    self.view.cancelButton.onClick:AddListener(function()
        self:_OnClickButton(self.m_args.onCancel)
    end)

    self.m_args = args
    self.m_itemCells = UIUtils.genCellCache(self.view.itemCell)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

GameSettingGamepadSavePopUpCtrl.OnShow = HL.Override() << function(self)
    self:_RefreshView()
end

GameSettingGamepadSavePopUpCtrl._RefreshView = HL.Method() << function(self)
    local args = self.m_args
    local keyScale = self.view.config.CONTENT_KEY_IMAGE_TEXT_SCALE

    local textBuilder = {}
    for i = 0, args.conflictInfos.Count - 1 do
        local conflictInfo = args.conflictInfos[i]
        local keyText = UIUtils.getGamepadKeyImageText(conflictInfo.keyCode, false, keyScale)
        table.insert(textBuilder, keyText)
    end
    local imageText = table.concat(textBuilder)
    local content = string.format(Language.LUA_GAME_SETTING_GAMEPAD_CONFLICT_CONTENT, imageText)
    self.view.contentText:SetAndResolveTextStyle(content)

    local itemCount = args.affectedInfos.Count
    self.m_itemCells:Refresh(itemCount, function(itemCell, itemIndex)
        self:_RefreshItemCell(itemCell, itemIndex)
    end)
end

GameSettingGamepadSavePopUpCtrl._RefreshItemCell = HL.Method(HL.Table, HL.Number) << function(self, itemCell, itemIndex)
    local affectedInfo = self.m_args.affectedInfos[CSIndex(itemIndex)]

    local itemData = Tables.gamepadImplicitSettingItemTable:GetValue(affectedInfo.settingId)
    itemCell.nameText.text = itemData.settingText


    if itemCell.currentKeyIcons == nil then
        itemCell.currentKeyIcons = { itemCell.currentKeyIcon1, itemCell.currentKeyIcon2 }
    end
    local currentKeyCodes = affectedInfo.currentKeyCodes
    for i, v in ipairs(itemCell.currentKeyIcons) do
        local active = i <= currentKeyCodes.Length
        v.gameObject:SetActive(active)
        if active then
            v:SetKeyIconName(currentKeyCodes[CSIndex(i)])
        end
    end

    if itemCell.targetKeyIcons == nil then
        itemCell.targetKeyIcons = { itemCell.targetKeyIcon1, itemCell.targetKeyIcon2 }
    end
    local targetKeyCodes = affectedInfo.targetKeyCodes
    for i, v in ipairs(itemCell.targetKeyIcons) do
        local active = i <= targetKeyCodes.Length
        v.gameObject:SetActive(active)
        if active then
            v:SetKeyIconName(targetKeyCodes[CSIndex(i)])
        end
    end
end

GameSettingGamepadSavePopUpCtrl._OnClickButton = HL.Method(HL.Function) << function(self, callback)
    if callback then
        callback()
    end
    self:PlayAnimationOutWithCallback(function()
        self.m_phase:RemovePhasePanelItemById(PanelId.GameSettingGamepadSavePopUp)
    end)
end

GameSettingGamepadSavePopUpCtrl._OnControllerTypeChanged = HL.Method(HL.Any) << function(self, args)
    local controllerType = DeviceInfo.controllerType
    if controllerType == DeviceControllerType.None then
        return
    end

    self:_RefreshView()
end

HL.Commit(GameSettingGamepadSavePopUpCtrl)
