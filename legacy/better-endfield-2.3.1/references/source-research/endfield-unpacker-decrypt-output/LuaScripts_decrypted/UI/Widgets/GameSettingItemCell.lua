local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

GameSettingItemCell = HL.Class('GameSettingItemCell', UIWidgetBase)









GameSettingItemCell.itemData = HL.Field(HL.Any)

GameSettingItemCell.m_itemControlConfigs = HL.Field(HL.Table)

GameSettingItemCell.m_extraArgs = HL.Field(HL.Table)

GameSettingItemCell.itemControl = HL.Field(HL.Any)

GameSettingItemCell.m_itemControlCache = HL.Field(HL.Table)


GameSettingItemCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.disabledBtn.onClick:AddListener(function()
        self:_OnDisabledBtnClicked()
    end)

    self.m_itemControlCache = {}
end

GameSettingItemCell.InitGameSettingItemCell = HL.Method(HL.Any, HL.Table, HL.Opt(HL.Table))
    << function(self, itemData, itemControlConfigs, extraArgs)
    self:_FirstTimeInit()

    self.itemData = itemData
    self.m_itemControlConfigs = itemControlConfigs
    self.m_extraArgs = extraArgs or {}

    self:Refresh()
end

GameSettingItemCell.Refresh = HL.Method() << function(self)
    local itemData = self.itemData
    local settingId = itemData.settingId


    local showTitle = not string.isEmpty(itemData.settingGroupTitle)
    self.view.titleNode.gameObject:SetActive(showTitle)
    if showTitle then
        self.view.titleText.text = itemData.settingGroupTitle
        if self.view.titleKey then
            self.view.titleKey.gameObject:SetActive(itemData.settingItemType == GEnums.SettingItemType.Key)
        end
    end


    self.view.gameObject.name = string.format("GameSettingItem_%s", itemData.settingId)
    self.view.itemText.text = itemData.settingText


    local itemState = self:_GetItemState(settingId)
    self.view.stateCtrl:SetState(itemState)
    if itemState == UIConst.GameSettingItemState.Normal then
        self:_InitItemControl()
    end


    local showRedDot = not string.isEmpty(itemData.settingRedDot)
    self.view.redDot.gameObject:SetActive(showRedDot)
    if showRedDot then
        self.view.redDot:InitRedDot(itemData.settingRedDot, itemData.settingId, nil, self:GetUICtrl().view.redDotScrollRect)
    end
end

GameSettingItemCell._GetItemState = HL.Method(HL.String).Return(HL.String) << function(self, settingId)
    local itemStateGetter = self.m_extraArgs.itemStateGetter
    if itemStateGetter == nil then
        return UIConst.GameSettingItemState.Normal
    end
    local success, result = xpcall(itemStateGetter, debug.traceback, settingId)
    if not success then
        logger.error(ELogChannel.GameSetting, "Failed to get setting item state, message: " .. tostring(result))
        return UIConst.GameSettingItemState.Disabled
    end
    local itemState = UIConst.GameSettingItemState[result]
    if itemState == nil then
        logger.error(ELogChannel.GameSetting, "Invalid setting item state: " .. tostring(result))
        return UIConst.GameSettingItemState.Disabled
    end
    return itemState
end

GameSettingItemCell._InitItemControl = HL.Method() << function(self)
    local itemData = self.itemData
    local itemType = itemData.settingItemType

    local itemControlConfig = self.m_itemControlConfigs[itemType]
    if itemControlConfig == nil then
        logger.error(ELogChannel.GameSetting, "Setting item control config not found, itemType: " .. tostring(itemType))
        return
    end

    local itemControl = self.m_itemControlCache[itemType]
    if itemControl == nil then
        local itemControlObject = CSUtils.CreateObject(itemControlConfig.template, self.view.controlNode)
        itemControl = Utils.wrapLuaNode(itemControlObject)
        self.m_itemControlCache[itemType] = itemControl
    end
    self.itemControl = itemControl

    for k, v in pairs(self.m_itemControlConfigs) do
        local existing = self.m_itemControlCache[k]
        if existing then
            existing.gameObject:SetActive(k == itemType)
        end
    end
    itemControl.rectTransform.anchoredPosition = Vector2.zero

    itemControlConfig.initializer(self)
end

GameSettingItemCell._OnDisabledBtnClicked = HL.Method() << function(self)
    local onDisabledItemClicked = self.m_extraArgs.onDisabledItemClicked
    if onDisabledItemClicked == nil then
        return
    end
    local settingId = self.itemData.settingId
    local success, result = xpcall(onDisabledItemClicked, debug.traceback, settingId)
    if not success then
        logger.error(ELogChannel.GameSetting, "Failed to invoke disabled btn click callback, message: " .. tostring(result))
    end
end

HL.Commit(GameSettingItemCell)
return GameSettingItemCell
