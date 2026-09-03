local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
ShopTabs = HL.Class('ShopTabs', UIWidgetBase)

ShopTabs.m_tabCellCache = HL.Field(HL.Forward("UIListCache"))


ShopTabs._OnFirstTimeInit = HL.Override() << function(self)
    self.m_tabCellCache = UIUtils.genCellCache(self.view.tabCell)
end

ShopTabs.InitShopTabs = HL.Method(HL.String, HL.String, HL.Function) << function(self, shopGroupId, curShopId, callBack)
    self:_FirstTimeInit()
    local shopSystem = GameInstance.player.shopSystem
    local shopGroupData = GameInstance.player.shopSystem:GetShopGroupData(shopGroupId)
    local unlockedShopSheets = {}
    for i, shopId in pairs(shopGroupData.shopIdList) do
        local isUnlocked = shopSystem:CheckShopUnlocked(shopId)

        local shopTableData = Tables.shopTable[shopId]
        if isUnlocked or shopTableData.isShowWhenLock then
            table.insert(unlockedShopSheets, shopId)
        end
    end


    self.m_tabCellCache:Refresh(#unlockedShopSheets, function(cell, index)
        local shopId = unlockedShopSheets[index]
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn and curShopId ~= shopId and callBack then
                callBack(shopId)
            end
        end)
        cell.toggle.isOn = shopId == curShopId
        cell.gameObject.name = "tab_" .. index


        local shopData = Tables.shopTable[shopId]
        UIUtils.setTabIcons(cell,UIConst.UI_SPRITE_INVENTORY,shopData.iconId)

    end)
end

ShopTabs.InitShopTabsForSwitchShopGroup = HL.Method(HL.Any, HL.String, HL.Function) << function(self, shopGroupList, curGroupId, callBack)
    self:_FirstTimeInit()
    self.m_tabCellCache:Refresh(shopGroupList.Count, function(cell, index)
        local groupId = shopGroupList[index - 1].shopGroupId
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            local isUnlock = GameInstance.player.shopSystem:GetShopGroupData(groupId)
            if isOn and curGroupId ~= groupId and callBack then
                if not isUnlock then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_TALENT_PASSIVE_SKILL_LOCK)
                end
                callBack(groupId)
            end
        end)
        cell.gameObject.name = "tab_" .. index
        local isUnlock = GameInstance.player.shopSystem:GetShopGroupData(groupId)
        if isUnlock then
            cell.toggle.isOn = groupId == curGroupId
        end



        local groupData = Tables.shopGroupTable[groupId]
        cell.defaultIcon:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, groupData.icon)
        cell.selectedIcon:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, groupData.icon)

    end)
end

HL.Commit(ShopTabs)
return ShopTabs

