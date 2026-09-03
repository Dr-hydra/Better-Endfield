local LogisticPortLinkageStatus = FacCoreNS.FactoryGridLogisticSystem.LogisticPortLinkageStatus

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacUnloader
FacUnloaderCtrl = HL.Class('FacUnloaderCtrl', uiCtrl.UICtrl)

local INVALID_COUNT_TEXT = "--"

local INVALID_SUB_INDEX = -1







FacUnloaderCtrl.s_messages = HL.StaticField(HL.Table) << {}

FacUnloaderCtrl.m_nodeId = HL.Field(HL.Any)

FacUnloaderCtrl.m_isHUBPort = HL.Field(HL.Boolean) << false

FacUnloaderCtrl.m_subIndex = HL.Field(HL.Number) << 1

FacUnloaderCtrl.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo)

FacUnloaderCtrl.m_selector = HL.Field(CS.Beyond.Gameplay.RemoteFactory.FBUtil.Selector)

FacUnloaderCtrl.m_isSelectorLocked = HL.Field(HL.Boolean) << false

FacUnloaderCtrl.m_cacheShowItemTipsBindingId = HL.Field(HL.Number) << -1


FacUnloaderCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_uiInfo = arg.uiInfo
    local nodeId = self.m_uiInfo.nodeId
    self.m_nodeId = nodeId
    self.m_subIndex = arg.subIndex or INVALID_SUB_INDEX

    self:_InitUnloaderBuildingInfo()
    self:_InitUnloadingSelectNode()

    self:_UpdateTransferItem()
    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_UpdateTransferItem()
        end
    end)

    self:SetNaviTarget(self.view.unloadingInfo)

    CS.Beyond.Gameplay.Conditions.OnOpenFacUnloaderPanel.Trigger(self.m_selector.selectItemId)
    GameInstance.player.remoteFactory.core:Message_HSFB(Utils.getCurrentChapterId(), false, {1})
    GameInstance.remoteFactoryManager:RegisterInterestedUnitId(nodeId)
end

FacUnloaderCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.remoteFactory.core:Message_HSFB(Utils.getCurrentChapterId(), true, {1})
    GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(self.m_nodeId)
end

FacUnloaderCtrl._InitUnloaderBuildingInfo = HL.Method() << function(self)
    local subIndex = self.m_subIndex
    self.m_isHUBPort = subIndex ~= nil and subIndex ~= INVALID_SUB_INDEX

    if not self.m_isHUBPort then
        self.m_selector = self.m_uiInfo.selector
    else
        self.m_selector = self.m_uiInfo["selector" .. subIndex]
    end

    if DeviceInfo.usingController then

        local itemEmpty = string.isEmpty(self.m_selector.selectItemId)
        self.view.switchText.text = itemEmpty and Language["key_hint_fac_unloader_add_item"] or Language["key_hint_fac_unloader_replace_item"]
        self.view.btnIcon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_COMMON, itemEmpty and "icon_tips_add" or "icon_tips_replace")
    end

    if not self.m_isHUBPort then
        self.view.buildingCommon:InitBuildingCommon(self.m_uiInfo)
        self.view.facCacheBelt:InitFacCacheBelt(self.m_uiInfo, {
            noGroup = true,
            stateRefreshCallback = function(portInfo)
                self:_RefreshBlockState(portInfo.isBlock)
            end
        })
    else

        local unloaderData = Tables.factoryBuildingTable:GetValue("unloader_1")
        local fakeData = {
            name = I18nUtils.CombineStringWithLanguageSpilt(Language.LUA_FAC_HUB_INPUT, subIndex),
            itemId = FactoryUtils.getBuildingItemId("unloader_1"),
            nodeId = self.m_uiInfo.nodeId,
        }
        setmetatable(fakeData, { __index = unloaderData })
        self.view.buildingCommon:InitBuildingCommon(nil, { data = fakeData })
        self.view.buildingCommon.nodeId = self.m_nodeId
        self.view.buildingCommon.view.descText.text = Language["ui_fac_hub_unloader_des"]
        self.view.facCacheBelt:InitFacCacheBelt(self.m_uiInfo, {
            noGroup = true,
            outIndexList = { subIndex },
            stateRefreshCallback = function(portInfo)
                self:_RefreshBlockState(portInfo.isBlock)
            end
        })
        self.view.buildingCommon.view.wikiButton.gameObject:SetActiveIfNecessary(false)
        self.view.buildingCommon.view.controllerSideMenuBtn.gameObject:SetActive(false)
        self.view.buildingCommon.view.earlyAccessNode.gameObject:SetActiveIfNecessary(false)
    end

    self:_RefreshSelectorLockState()
end

FacUnloaderCtrl._InitUnloadingSelectNode = HL.Method() << function(self)
    self.view.unloadingEmptyNode.onClick:AddListener(function()
        self:_ShowSelectPanel()
    end)

    local itemEmpty = string.isEmpty(self.m_selector.selectItemId)
    self.view.switchButton.onClick:AddListener(function()
        self:_ShowSelectPanel()
    end)

    self.view.switchText.text = itemEmpty and Language["key_hint_fac_unloader_add_item"] or Language["key_hint_fac_unloader_replace_item"]
    self.view.btnIcon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_COMMON, itemEmpty and "icon_tips_add" or "icon_tips_replace")

    self.m_cacheShowItemTipsBindingId = InputManagerInst:CreateBindingByActionId("show_item_tips", function()
        local itemExist = not string.isEmpty(self.m_selector.selectItemId)
        if itemExist then
            self.view.unloadingItem:ShowTips()
        end
    end, self.view.unloadingInfoGroup.groupId)
    InputManagerInst:ToggleBinding(self.m_cacheShowItemTipsBindingId, not itemEmpty)
end

FacUnloaderCtrl._ShowSelectPanel = HL.Method() << function(self)
    if self.m_isSelectorLocked then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_FORBID_SELECT_ITEM)
        return
    end

    Notify(MessageConst.FAC_SHOW_UNLOADER_SELECT, {
        buildingInfo = self.m_uiInfo,
        selector = self.m_selector,
        subIndex = self.m_subIndex,
        selectCallback = function(itemId)
            self:_SelectItem(itemId)
        end,
    })
end

FacUnloaderCtrl._UpdateTransferItem = HL.Method() << function(self)
    if IsNull(self.view.gameObject) then
        return
    end
    local id = self.m_selector.selectItemId
    local itemExist = not string.isEmpty(id)

    self.view.unloadingItem.gameObject:SetActive(itemExist)
    self.view.unloadingEmptyNode.gameObject:SetActive(not itemExist)

    if itemExist then
        if id ~= self.view.unloadingItem.id then
            self.view.unloadingItem:InitItem({id = id, count = 1}, true)

            local success, itemData = Tables.itemTable:TryGetValue(id)
            if success then
                local iconId = itemData.iconId
                self.view.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, iconId)
            end
        end

        local depotCount = Utils.getDepotItemCount(id)
        local text = tostring(depotCount)
        local itemColor = depotCount <= 0 and self.view.config.COLOR_STORAGE_EMPTY or self.view.config.COLOR_STORAGE_NORMAL
        if FactoryUtils.isItemInfiniteInFactoryDepot(id) then
            text = Language.LUA_ITEM_INFINITE_COUNT
            itemColor = self.view.config.COLOR_STORAGE_NORMAL
        end
        self:_RefreshItemCount(text, itemColor)
    else
        self:_RefreshItemCount(INVALID_COUNT_TEXT, self.view.config.COLOR_STORAGE_NORMAL)
    end

    self.view.itemIcon.gameObject:SetActiveIfNecessary(itemExist)
    self.view.decoIcon.gameObject:SetActiveIfNecessary(not itemExist)
end

FacUnloaderCtrl._SelectItem = HL.Method(HL.String) << function(self, itemId)
    if itemId == self.m_selector.selectItemId then
        itemId = ""
    end

    self.m_uiInfo.sender:Message_OpSetSelectTarget(Utils.getCurrentChapterId(), self.m_selector.componentId, itemId, function()
        if not UIManager:IsOpen(PANEL_ID) then
            return
        end

        self.m_uiInfo:Update()
        self:_UpdateTransferItem()
        CS.Beyond.Gameplay.Conditions.OnFacChooseItemInUnloader.Trigger(itemId)
    end)

    local itemEmpty = string.isEmpty(itemId)
    local btnText = itemEmpty and Language["key_hint_fac_unloader_add_item"] or Language["key_hint_fac_unloader_replace_item"]
    self.view.switchText.text = btnText
    self.view.btnIcon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_COMMON, itemEmpty and "icon_tips_add" or "icon_tips_replace")
    InputManagerInst:ToggleBinding(self.m_cacheShowItemTipsBindingId, not itemEmpty)
end

FacUnloaderCtrl._RefreshBlockState = HL.Method(HL.Boolean) << function(self, isBlock)
    local state = isBlock and GEnums.FacBuildingState.Blocked or GEnums.FacBuildingState.Normal
    self.view.buildingCommon:ChangeBuildingStateDisplay(state)
end

FacUnloaderCtrl._RefreshItemCount = HL.Method(HL.String, HL.Any) << function(self, countText, color)
    self.view.infoShadowNode.countText.text = countText
    self.view.infoNode.countText.text = countText
    self.view.infoNode.countText.color = color
end

FacUnloaderCtrl._RefreshSelectorLockState = HL.Method() << function(self)
    local node = FactoryUtils.getBuildingNodeHandler(self.m_uiInfo.nodeId)
    if node == nil then
        return
    end

    local pdp = node.predefinedParam
    if pdp == nil then
        return
    end

    local selector
    if self.m_isHUBPort then
        local hub = pdp.hub
        if hub ~= nil and hub.selectors ~= nil then
            for i = 0, hub.selectors.Count - 1 do
                local v = hub.selectors[i]
                if v.index == self.m_subIndex - 1 then
                    selector = v
                end
            end
        end
    else
        selector = pdp.selector
    end

    local locked = selector and selector.lockSelectedItemId or false

    self.view.selectLockNode.gameObject:SetActive(locked)
    self.m_isSelectorLocked = locked
end


HL.Commit(FacUnloaderCtrl)
