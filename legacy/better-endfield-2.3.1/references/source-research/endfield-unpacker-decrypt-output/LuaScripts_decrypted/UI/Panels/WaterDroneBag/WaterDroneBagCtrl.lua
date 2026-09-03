local WaterDroneSourceType = CS.Beyond.Gameplay.WaterDroneSourceType
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WaterDroneBag

WaterDroneBagCtrl = HL.Class('WaterDroneBagCtrl', uiCtrl.UICtrl)






WaterDroneBagCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.HIDE_WATER_DRONE_BAG] = '_OnCloseWaterDroneBag',
}







WaterDroneBagCtrl.m_getCell = HL.Field(HL.Function)


WaterDroneBagCtrl.m_mergedFullBottleItems = HL.Field(HL.Table)


WaterDroneBagCtrl.m_curSelectedItemCsIndex = HL.Field(HL.Number) << -1




WaterDroneBagCtrl.m_itemBag = HL.Field(CS.Beyond.Gameplay.InventorySystem.ItemBag)





WaterDroneBagCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:BindInputPlayerAction("common_cancel", function()
        self:_OnBack()
    end)
    self:BindInputPlayerAction("disable_common_open_watch", function()

    end)
    self.view.backBtn.onClick:AddListener(function()
        self:_OnBack()
    end)

    self.m_getCell = UIUtils.genCachedCellFunction(self.view.itemScrollList)
    self.view.itemScrollList.onUpdateCell:AddListener(function(gameObj, csIndex)
        self:_OnUpdateCell(gameObj, csIndex)
    end)





    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end


WaterDroneBagCtrl.Refresh = HL.Method() << function(self)
    self.m_itemBag = GameInstance.player.inventory.itemBag:GetOrFallback(Utils.getCurrentScope())

    local itemBag = self.m_itemBag
    self.m_mergedFullBottleItems = {}


    local itemDict = {}


    for index = 0, itemBag.slotCount - 1 do
        local itemBundle = itemBag.slots[index]

        if itemBundle ~= nil then

            local success, fullBottleData = Tables.fullBottleTable:TryGetValue(itemBundle.id)

            if success then
                if fullBottleData ~= nil then
                    local curLiquidId = fullBottleData.liquidId


                    local usedByWaterDrone, waterTypeData = DataManager.waterDroneConfig.waterTypeDataDict:TryGetValue(curLiquidId)

                    if usedByWaterDrone then
                        if waterTypeData ~= nil then
                            local emptyBottleId = fullBottleData.emptyBottleId
                            local emptyBottleFound, emptyBottleItemData = Tables.itemTable:TryGetValue(emptyBottleId)
                            local liquidFound, liquidItemData = Tables.itemTable:TryGetValue(curLiquidId)

                            if emptyBottleFound and liquidFound then
                                if emptyBottleItemData ~= nil and liquidItemData ~= nil then

                                    local mergedSortId = 100 * emptyBottleItemData.sortId2 + liquidItemData.sortId2

                                    if not itemDict[itemBundle.id] then
                                        itemDict[itemBundle.id] = {
                                            itemId = itemBundle.id,
                                            itemCount = 0,
                                            emptyBottleId = emptyBottleId,
                                            liquidId = curLiquidId,
                                            sortId = mergedSortId
                                        }
                                    end


                                    itemDict[itemBundle.id].itemCount = itemDict[itemBundle.id].itemCount + itemBundle.count
                                end
                            end
                        end
                    end
                end
            end
        end
    end



    for _, itemData in pairs(itemDict) do
        table.insert(self.m_mergedFullBottleItems, itemData)
    end


    table.sort(self.m_mergedFullBottleItems, Utils.genSortFunction({ "sortId" }, false))

    self.view.itemScrollList:UpdateCount(#self.m_mergedFullBottleItems, false, false, false, skipGraduallyShow == true)


    if #self.m_mergedFullBottleItems > 0 then
        self.view.contentState:SetState("ItemScrollList")

        self:RefreshToggleAllCell()
    else
        self.view.contentState:SetState("Empty")
    end
end



WaterDroneBagCtrl.RefreshToggleAllCell = HL.Method() << function(self)
    local customAbilityCom = GameUtil.mainCharacter.customAbilityCom
    local waterDroneSourceType = customAbilityCom.waterDroneSourceType
    local fromInteractiveNow = (waterDroneSourceType == WaterDroneSourceType.Interactive)

    local _showToggleCsIndex = -1
    local count = #self.m_mergedFullBottleItems
    if count >= 1 then
        for _luaIndex = 1, count do




            if fromInteractiveNow == false then

                if self.m_mergedFullBottleItems[_luaIndex].itemId == GameUtil.mainCharacter.customAbilityCom.persistFullBottleItemId then
                    _showToggleCsIndex = _luaIndex - 1
                end
            end

            local _cell = self.m_getCell(_luaIndex)
            if _cell then
                _cell.view.toggle.gameObject:SetActive(false)
            end
        end
    end


    self:RefreshToggleAfterSelectedCell(_showToggleCsIndex)
end

WaterDroneBagCtrl.RefreshToggleSingleCell = HL.Method(HL.Number) << function(self, luaIndex)
    local customAbilityCom = GameUtil.mainCharacter.customAbilityCom
    local waterDroneSourceType = customAbilityCom.waterDroneSourceType
    local fromInteractiveNow = (waterDroneSourceType == WaterDroneSourceType.Interactive)

    local _showToggleCsIndex = -1




    if fromInteractiveNow == false then

        if self.m_mergedFullBottleItems[luaIndex].itemId == GameUtil.mainCharacter.customAbilityCom.persistFullBottleItemId then
            _showToggleCsIndex = luaIndex - 1
        end
    end

    local _cell = self.m_getCell(luaIndex)
    if _cell then
        _cell.view.toggle.gameObject:SetActive(false)
    end


    if _showToggleCsIndex ~= -1 then
        self:RefreshToggleAfterSelectedCell(_showToggleCsIndex)
    end
end

WaterDroneBagCtrl.RefreshToggleAfterSelectedCell = HL.Method(HL.Number) << function(self, showToggleCsIndex)
    self.m_curSelectedItemCsIndex = showToggleCsIndex
    local selectedCell = self.m_getCell(LuaIndex(showToggleCsIndex))
    if selectedCell then
        selectedCell.view.toggle.gameObject:SetActive(true)
        self:SetNaviTarget(selectedCell.view.item.view.button)
    else
        local firstCell = self.m_getCell(1)
        if firstCell then
            self:SetNaviTarget(firstCell.view.item.view.button)
        end
    end
end

WaterDroneBagCtrl.OnShow = HL.Override() << function(self)

    self:_ToggleShowHideGeneralAbility(false)

    self:_ToggleShowHideBattleAction(false)

    GameInstance.player.forbidSystem:SetForbid(ForbidType.ForbidJump, "WaterDroneBag", true)
    self:Refresh()
end


WaterDroneBagCtrl.OnClose = HL.Override() << function(self)

    self:_ToggleShowHideGeneralAbility(true)

    self:_ToggleShowHideBattleAction(true)

    GameInstance.player.forbidSystem:SetForbid(ForbidType.ForbidJump, "WaterDroneBag", false)
end


WaterDroneBagCtrl._ToggleShowHideGeneralAbility = HL.Method(HL.Boolean) << function(self, active)
    if active then
        UIManager:ShowWithKey(PanelId.GeneralAbility, "WaterDroneBag")
    else
        UIManager:HideWithKey(PanelId.GeneralAbility, "WaterDroneBag")
    end
end

WaterDroneBagCtrl._ToggleShowHideBattleAction = HL.Method(HL.Boolean) << function(self, active)
    if active then
        UIManager:ShowWithKey(PanelId.BattleAction, "WaterDroneBag")
    else
        UIManager:HideWithKey(PanelId.BattleAction, "WaterDroneBag")
    end
end

WaterDroneBagCtrl.OnShowWaterDroneBag = HL.StaticMethod() << function()
    local waterDroneBagPanel = UIManager:AutoOpen(PANEL_ID)

end

WaterDroneBagCtrl._OnCloseWaterDroneBag = HL.Method() << function(self)


    if not self:IsPlayingAnimationOut() then
        self:PlayAnimationOutAndClose()
    end
end


WaterDroneBagCtrl._OnBack = HL.Method() << function(self)


    if not self:IsPlayingAnimationOut() then
        self:PlayAnimationOutAndClose()
    end
    Notify(MessageConst.HIDE_COMMON_HOVER_TIP, { noAnimation = true })






    local customAbilityCom = GameUtil.mainCharacter.customAbilityCom
    local waterDroneSourceType = customAbilityCom.waterDroneSourceType


    customAbilityCom:OnConfirmItem()

    if waterDroneSourceType == WaterDroneSourceType.Interactive or waterDroneSourceType == WaterDroneSourceType.InfinityTag then
        if self.m_curSelectedItemCsIndex == -1 then
            Notify(MessageConst.SHOW_WATER_DRONE_AIM)
        else



            customAbilityCom:TryEnterWaterDroneAbility_ByItem()
        end
        return
    end

    customAbilityCom:WaterDroneEnterAimModeOpenUI(WaterDroneSourceType.Item)
end

WaterDroneBagCtrl._OnUpdateCell = HL.Method(GameObject, HL.Number) << function(self, gameObject, csIndex)
    local cell = self.m_getCell(gameObject)

    local mergedFullBottleItems = self.m_mergedFullBottleItems
    local count = #mergedFullBottleItems
    local luaIndex = LuaIndex(csIndex)
    local itemCustomBundleTable = luaIndex <= count and mergedFullBottleItems[luaIndex] or nil
    if itemCustomBundleTable then
        local itemId = itemCustomBundleTable.itemId
        local itemCount = itemCustomBundleTable.itemCount
        local emptyBottleId = itemCustomBundleTable.emptyBottleId
        local liquidId = itemCustomBundleTable.liquidId
        self:_UpdateNormalSlot(cell, itemId, itemCount, emptyBottleId, liquidId, csIndex)
    end
end

WaterDroneBagCtrl._UpdateNormalSlot = HL.Method(HL.Userdata, HL.String, HL.Number, HL.String, HL.String, HL.Number) << function(self, cell, itemId, itemCount, emptyBottleId, liquidId, csIndex)
    cell:InitWaterDroneItem(itemId, itemCount, emptyBottleId, liquidId, function()
        self:_OnClickItem(csIndex)
    end)

    cell.view.item.slotIndex = csIndex


    self:RefreshToggleAllCell()

end



WaterDroneBagCtrl._OnClickItem = HL.Method(HL.Number) << function(self, csIndex)
    local count = #self.m_mergedFullBottleItems
    local luaIndex = LuaIndex(csIndex)
    local itemCustomBundleTable = luaIndex >=1 and luaIndex <= count and self.m_mergedFullBottleItems[luaIndex] or nil
    if itemCustomBundleTable == nil then

        return
    end


    local clickedItemId = itemCustomBundleTable.itemId
    local clickedLiquidId = itemCustomBundleTable.liquidId


    local liquidXiranite = Tables.globalConst.liquidXiranite
    if clickedLiquidId == liquidXiranite then
        if not GameInstance.player.systemUnlockManager:IsSystemUnlockByType(GEnums.UnlockSystemType.WaterDroneCanUseXiranite) then
            Notify(MessageConst.SHOW_TOAST, Language.ui_msc_xiranite_locked_toast)
            return
        end
    end


    GameUtil.mainCharacter.customAbilityCom:OnClickedItemChanged(clickedItemId, clickedLiquidId)


    if count >= 1 then
        for _luaIndex = 1, count do
            local _cell = self.m_getCell(_luaIndex)
            if _cell then
                _cell.view.toggle.gameObject:SetActive(false)
            end
        end
    end


    self.m_curSelectedItemCsIndex = csIndex
    local selectedCell = self.m_getCell(LuaIndex(csIndex))
    if selectedCell then
        selectedCell.view.toggle.gameObject:SetActive(true)
    end









    self:_OnBack()
end
HL.Commit(WaterDroneBagCtrl)
