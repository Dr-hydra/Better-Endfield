
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonItemToast
local EXP_ITEM_ID = -1

CommonItemToastCtrl = HL.Class('CommonItemToastCtrl', uiCtrl.UICtrl)





CommonItemToastCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SHOW_EXP_TOAST] = 'OnShowExpToast',
    [MessageConst.ON_ADVENTURE_EXP_CHANGE_FOR_TOAST] = 'OnShowAdventureExpToast',
    [MessageConst.ON_ITEM_COUNT_CHANGED_FOR_TOAST] = 'OnItemCountChangedImm',
    [MessageConst.ON_WALLET_CHANGED] = 'OnWalletChangedImm',

    [MessageConst.TOGGLE_COMMON_ITEM_TOAST_CACHE] = 'ToggleItemNeedCache',


}


CommonItemToastCtrl.m_itemId2ItemInfo = HL.Field(HL.Table)

CommonItemToastCtrl.m_waitingToastInfoCache = HL.Field(HL.Forward("MinHeap"))

CommonItemToastCtrl.m_showingToastInfoCache = HL.Field(HL.Forward("Queue"))

CommonItemToastCtrl.m_toastTimeSchedule = HL.Field(HL.Forward("Queue"))

CommonItemToastCtrl.m_panelExitTimeMark = HL.Field(HL.Number) << -1

CommonItemToastCtrl.m_curShowCount = HL.Field(HL.Number) << 0

CommonItemToastCtrl.m_curListCount = HL.Field(HL.Number) << 0

CommonItemToastCtrl.m_lastToastTime = HL.Field(HL.Number) << 0

CommonItemToastCtrl.m_updateKey = HL.Field(HL.Number) << -1

CommonItemToastCtrl.m_cacheToasts = HL.Field(HL.Forward("Stack"))

CommonItemToastCtrl.m_maxCount = HL.Field(HL.Number) << 0

CommonItemToastCtrl.m_autoScrollCor = HL.Field(HL.Any)

CommonItemToastCtrl.m_getToastCell = HL.Field(HL.Function)


CommonItemToastCtrl.s_isActive = HL.StaticField(HL.Boolean) << true


CommonItemToastCtrl.s_isCommonToastEnable = HL.StaticField(HL.Boolean) << false


CommonItemToastCtrl.OnShowExpToast = HL.Method(HL.Any) << function(self, arg)
    if arg == nil then
        return
    end
    arg.isExp = true
    self:_AddToastRequest({ arg })
end

CommonItemToastCtrl.OnShowAdventureExpToast = HL.Method(HL.Any) << function(self, arg)
    if arg == nil then
        return
    end

    local preLv, preExp = unpack(arg)
    local adventureData = GameInstance.player.adventure.adventureLevelData
    local curExp = adventureData.exp
    local info = {
        itemId = Tables.globalConst.adventureExpItemId,
        count = curExp - preExp,
    }
    if info.count <= 0 then
        return
    end

    self:_AddToastRequest({info})
end

CommonItemToastCtrl.OnItemCountChangedImm = HL.Method(HL.Any) << function(self, arg)
    if not arg then
        return
    end

    local itemId2DiffCount = unpack(arg)
    local toastDataList = {}
    for itemId, diffCount in pairs(itemId2DiffCount) do
        if diffCount > 0 then
            local cachedCount = self.m_cachedItemChangeCounts[itemId]
            if cachedCount then
                self.m_cachedItemChangeCounts[itemId] = cachedCount + diffCount
            else
                table.insert(toastDataList, {
                    itemId = itemId,
                    count = diffCount
                })
            end
        end
    end

    self:_AddToastRequest(toastDataList)
end

local WalletChangeBlackList = {
    ["item_spaceship_tundra_gold"] = true,
    ["item_spaceship_jinlong_gold"] = true,
}

CommonItemToastCtrl.OnWalletChangedImm = HL.Method(HL.Any) << function(self, arg)
    local itemId, curCount, diffCount = unpack(arg)

    if WalletChangeBlackList[itemId] then
        return
    end

    if diffCount == nil or diffCount <= 0 then
        return
    end

    self:_AddToastRequest({
        {
            itemId = itemId,
            count = diffCount,
        }
    })
end

CommonItemToastCtrl.ToggleCommonItemToast = HL.StaticMethod(HL.Boolean) << function(active)
    CommonItemToastCtrl.s_isActive = active
    if not active then
        UIManager:Hide(PANEL_ID)
    end
end

CommonItemToastCtrl.OnDisableCommonToast = HL.StaticMethod() << function()
    CommonItemToastCtrl.s_isCommonToastEnable = false
    if UIManager:IsShow(PANEL_ID) then
        UIManager:Hide(PANEL_ID)
    end
end

CommonItemToastCtrl.OnEnableCommonToast = HL.StaticMethod() << function()
    CommonItemToastCtrl.s_isCommonToastEnable = true
end



CommonItemToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_itemId2ItemInfo = {}
    self.m_cachedItemChangeCounts = {}
    self.m_waitingToastInfoCache = require_ex("Common/Utils/DataStructure/MinHeap")()
    self.m_showingToastInfoCache = require_ex("Common/Utils/DataStructure/Queue")()
    self.m_toastTimeSchedule = require_ex("Common/Utils/DataStructure/Queue")()
    self.m_getToastCell = UIUtils.genCachedCellFunction(self.view.list)

    self.view.toastCell.gameObject:SetActive(false)
    self.view.list.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateCell(object, LuaIndex(csIndex))
    end)

    self.m_updateKey = LuaUpdate:Add("Tick", function()
        self:_Update()
    end, true)

    self:Hide()
end

CommonItemToastCtrl.OnHide = HL.Override() << function(self)
    self:_CleanUpCache()
end

CommonItemToastCtrl.OnClose = HL.Override() << function(self)
    self:_ClearRegister()
end

CommonItemToastCtrl._Update = HL.Method() << function(self)
    if not CommonItemToastCtrl.s_isCommonToastEnable then
        return
    end

    self:_ScrollExit()
    self:_ShowNewToast()

    self:_TryHidePanel()
end

CommonItemToastCtrl._TryHidePanel = HL.Method() << function(self)
    if self:IsHide() then
        return
    end

    if self:IsPlayingAnimationOut() then
        return
    end

    if self.m_toastTimeSchedule:Size() > 0 then
        return
    end
    if self.m_waitingToastInfoCache:Size() > 0 then
        return
    end

    if self.m_panelExitTimeMark < 0 then
        self.m_panelExitTimeMark = Time.realtimeSinceStartup
    end

    if Time.realtimeSinceStartup - self.m_panelExitTimeMark < self.view.config.PANEL_EXIT_WAIT_DURATION then
        return
    end

    self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Hide)
end

CommonItemToastCtrl._ScrollExit = HL.Method() << function(self)
    if self:IsHide() then
        return
    end

    if self:IsPlayingAnimationOut() then
        return
    end

    if self.m_toastTimeSchedule:Size() <= 0 then
        return
    end

    local scheduleNode = self.m_toastTimeSchedule:AtIndex(1)
    local showTime = scheduleNode.showTime
    local scrollIndex = scheduleNode.scrollIndex
    if Time.realtimeSinceStartup - showTime < self.view.config.SHOW_TOAST_TIME then
        return
    end

    self.m_toastTimeSchedule:Pop()
    self.m_curShowCount = self.m_curShowCount - 1
    self.view.list:ScrollToIndex(scrollIndex)

end

CommonItemToastCtrl._ShowNewToast = HL.Method() << function(self)
    local waitingToastCount = self.m_waitingToastInfoCache:Size()
    if waitingToastCount <= 0 then
        return
    end

    local hasEmptySlot = self.m_curShowCount < self.view.config.MAX_IN_SCREEN_TOAST_COUNT
    local isOverNextToastTime = Time.realtimeSinceStartup - self.m_lastToastTime > self.view.config.BETWEEN_TOAST_DURATION
    local canShowNextCell = hasEmptySlot and isOverNextToastTime
    if not canShowNextCell then
        return
    end

    if self:IsPlayingAnimationOut() then
        return
    end

    if self:IsHide() then
        self:Show()

    end

    local cellInfo = self.m_waitingToastInfoCache:Pop()
    if self.m_itemId2ItemInfo[cellInfo.itemId] then
        self.m_itemId2ItemInfo[cellInfo.itemId] = nil
    end

    self.m_lastToastTime = Time.realtimeSinceStartup
    self.m_showingToastInfoCache:Push(cellInfo)

    self.m_curShowCount = self.m_curShowCount + 1
    self.m_curListCount = self.m_curListCount + 1

    self.m_toastTimeSchedule:Push({
        showTime = Time.realtimeSinceStartup,
        scrollIndex = self.m_curListCount,
    })

    self.view.list:UpdateCount(self.m_curListCount, false, false, true)

end

CommonItemToastCtrl._ClearRegister = HL.Method() << function(self)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
end

CommonItemToastCtrl._CleanUpCache = HL.Method() << function(self)

    self.view.list:UpdateCount(0, true)

    local minHeap = self.m_waitingToastInfoCache
    local count = minHeap:Size()
    for k = 1, count do
        minHeap:Pop()
    end

    self.m_showingToastInfoCache:Clear()
    self.m_toastTimeSchedule:Clear()
    self.m_panelExitTimeMark = -1
    self.m_lastToastTime = 0
    self.m_curShowCount = 0
    self.m_curListCount = 0


end

CommonItemToastCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, object, index)
    local toastCell = self.m_getToastCell(object)
    local data = self.m_showingToastInfoCache:AtIndex(index)
    if data.isExp then
        self:_UpdateExpCell(toastCell, data)
    else
        self:_UpdateItemCell(toastCell, data)
    end
end

CommonItemToastCtrl._UpdateExpCell = HL.Method(HL.Any, HL.Table) << function(self, toastCell, data)
    local info = unpack(data)
    toastCell.rarity.gameObject:SetActive(false)
    toastCell.rarityLight.gameObject:SetActive(false)

    toastCell.toItemBag.gameObject:SetActive(false)
    toastCell.toValuableDepot.gameObject:SetActive(false)

    toastCell.icon.gameObject:SetActive(false)
    toastCell.expIcon.gameObject:SetActive(true)
    toastCell.label.text = Language.LUA_COMMON_ITEM_TOAST_EXP_NAME
    toastCell.num.text = string.format(" × %d", info.Exp)
end

CommonItemToastCtrl._UpdateItemCell = HL.Method(HL.Any, HL.Table) << function(self, toastCell, data)
    local itemData = Tables.itemTable:GetValue(data.itemId)
    local isShowRarityLight = itemData.rarity >= UIConst.COMMON_TOAST_SHOW_LIGHT_RARITY

    local itemType = itemData.type
    local itemTypeCfg = Tables.itemTypeTable:GetValue(itemType)

    toastCell.toItemBag.gameObject:SetActive(itemTypeCfg.storageSpace == GEnums.ItemStorageSpace.BagAndFactoryDepot)
    toastCell.toValuableDepot.gameObject:SetActive(itemTypeCfg.storageSpace == GEnums.ItemStorageSpace.ValuableDepot and itemType ~= GEnums.ItemType.DecorateBuildingItem)

    toastCell.label.text = itemData.name

    toastCell.icon.gameObject:SetActive(true)
    toastCell.expIcon.gameObject:SetActive(false)
    toastCell.icon:InitItemIcon(data.itemId)

    toastCell.num.text = string.format(" × %s", UIUtils.getNumString(data.count))

    toastCell.rarity.gameObject:SetActive(true)
    UIUtils.setItemRarityImage(toastCell.rarity, itemData.rarity)

    toastCell.rarityLight.gameObject:SetActive(isShowRarityLight)
    if isShowRarityLight then
        local rarityLightColor = UIUtils.getItemRarityColor(itemData.rarity)
        rarityLightColor.a = self.view.config.RARITY_LIGHT_COLOR_ALPHA
        toastCell.rarityLight.color = rarityLightColor
    end

    local isPickUp, _ = Tables.useItemTable:TryGetValue(data.itemId)
    toastCell.pickUpNode.gameObject:SetActive(isPickUp)
    toastCell.portableDeviceNode.gameObject:SetActive(Utils.isPortableDevice(data.itemId))
end

CommonItemToastCtrl._InitMaxCount = HL.Method() << function(self)
    local spacing = self.view.list.spacing
    local cellHeight = self.view.toastCell.rectTransform.rect.height
    local rect = self.view.list:RectTransform().rect
    local maxCount = math.floor((rect.height + spacing) / cellHeight)
    self.m_maxCount = maxCount
end







CommonItemToastCtrl._AddToastRequest = HL.Method(HL.Any) << function(self, infoList)
    if not CommonItemToastCtrl.s_isActive then
        return
    end

    LuaSystemManager.mainHudActionQueue:AddRequest("GetItemToast", function()
        for _, info in ipairs(infoList) do
            if info.isExp then
                self:_AddExpRequest(info)
            else
                self:_AddItemRequest(info)
            end
        end
    end)
end

CommonItemToastCtrl._AddExpRequest = HL.Method(HL.Any) << function(self, info)
    local waitingToastInfoCache = self.m_waitingToastInfoCache
    local itemId2ItemInfo = self.m_itemId2ItemInfo
    if (not CommonItemToastCtrl.s_isCommonToastEnable) and itemId2ItemInfo[UIConst.COMMON_TOAST_EXP_ICON_ID] then
        itemId2ItemInfo[EXP_ITEM_ID].count = itemId2ItemInfo[EXP_ITEM_ID].count + info.count
        return
    end

    waitingToastInfoCache:Add(info, Const.MAX_ITEM_RARITY)
    itemId2ItemInfo[EXP_ITEM_ID] = info
end

CommonItemToastCtrl._AddItemRequest = HL.Method(HL.Any) << function(self, info)
    if not self:_CheckIfBindingSystemUnlock(info.itemId) then
        logger.info(ELogChannel.GamePlay,"AddItemRequest->System locked itemId:[%s] ", info.itemId)
        return
    end

    self.m_panelExitTimeMark = -1

    local waitingToastInfoCache = self.m_waitingToastInfoCache
    local itemId2ItemInfo = self.m_itemId2ItemInfo
    if (not CommonItemToastCtrl.s_isCommonToastEnable) and itemId2ItemInfo[info.itemId] then
        itemId2ItemInfo[info.itemId].count = itemId2ItemInfo[info.itemId].count + info.count
        return
    end

    local _, itemData = Tables.itemTable:TryGetValue(info.itemId)
    if not itemData then
        return
    end

    local reverseRarity = math.min(Const.MAX_ITEM_RARITY - itemData.rarity, 1)
    waitingToastInfoCache:Add(info, reverseRarity)
    itemId2ItemInfo[info.itemId] = info
end

CommonItemToastCtrl._CheckIfBindingSystemUnlock = HL.Method(HL.Any).Return(HL.Boolean) << function(self, itemId)
    local _, itemCfg = Tables.itemTable:TryGetValue(itemId)
    if not itemCfg then
        return false
    end

    if itemCfg.type == GEnums.ItemType.None then
        return false
    end

    local _, itemTypeCfg = Tables.itemTypeTable:TryGetValue(itemCfg.type)
    if not itemTypeCfg then
        return false
    end

    if itemTypeCfg.unlockSystemType == GEnums.UnlockSystemType.None then
        return true
    end

    return Utils.isSystemUnlocked(itemTypeCfg.unlockSystemType)
end


CommonItemToastCtrl.m_cachedItemChangeCounts = HL.Field(HL.Table)

CommonItemToastCtrl.ToggleItemNeedCache = HL.Method(HL.Table) << function(self, arg)
    local itemId, needCache = unpack(arg)
    if needCache then
        if self.m_cachedItemChangeCounts[itemId] then
            return
        end
        self.m_cachedItemChangeCounts[itemId] = 0
    else
        if not self.m_cachedItemChangeCounts[itemId] then
            return
        end
        local curCount = self.m_cachedItemChangeCounts[itemId]
        self.m_cachedItemChangeCounts[itemId] = nil
        if curCount > 0 then
            self:_AddToastRequest({
                { itemId = itemId, count = curCount }
            })
        end
    end
end


HL.Commit(CommonItemToastCtrl)
