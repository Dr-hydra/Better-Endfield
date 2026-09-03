local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacInventoryStation
local FacBuildingState = GEnums.FacBuildingState

FacInventoryStationCtrl = HL.Class('FacInventoryStationCtrl', uiCtrl.UICtrl)






FacInventoryStationCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacInventoryStationCtrl.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo)

FacInventoryStationCtrl.m_nodeId = HL.Field(HL.Number) << -1

FacInventoryStationCtrl.m_waitInitNaviTarget = HL.Field(HL.Boolean) << true

FacInventoryStationCtrl.m_naviGroupSwitcher = HL.Field(HL.Forward('NaviGroupSwitcher'))


FacInventoryStationCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_uiInfo = arg.uiInfo
    self.m_nodeId = arg.uiInfo.nodeId

    local NaviGroupSwitcher = require_ex("Common/Utils/UI/NaviGroupSwitcher").NaviGroupSwitcher
    self.m_naviGroupSwitcher = NaviGroupSwitcher(self.view.inputGroup.groupId, nil, true)

    self.view.buildingCommon:InitBuildingCommon(self.m_uiInfo, {
        onStateChanged = function(state)
            self:_OnBuildingStateChanged(state)
        end
    })

    local itemMoveCheckFunc = function()
        return self:_CanMoveItem()
    end
    self.view.inventoryArea:InitInventoryArea({
        layoutStyle = UIConst.INVENTORY_AREA_LAYOUT_STYLE.SPLIT,
        itemMoveType = UIConst.INVENTORY_AREA_ITEM_MOVE_TYPE.BAG_TO_DEPOT,
        itemMoveCheckFunc = itemMoveCheckFunc,
        customCheckItemValid = function(...)
            return self:_CheckItemValid(...)
        end,
        ignoreInSafeZone = true,
        itemBagQuickDropBindingText = Language.LUA_FAC_INVENTORY_STATION_ITEM_BAG_QUICK_DROP,
        depotQuickDropBindingText = Language.LUA_FAC_INVENTORY_STATION_DEPOT_QUICK_DROP,
        customOnUpdateCell = function(cell, itemBundle, luaIndex, isItemBag)
            self:_UpdateInventoryItemCell(cell, itemBundle, luaIndex, isItemBag)
        end,
        customSetActionMenuArgs = function(actionMenuArgs)
            actionMenuArgs.ignoreInSafeZone = true
            actionMenuArgs.itemMoveCheckFunc = itemMoveCheckFunc
        end,
        adaptForceQuickDropKeyhintToGray = true,
    })
    local itemBagNaviGroup = self.view.inventoryArea:GetNaviGroup(true)
    itemBagNaviGroup.onDefaultNaviFailed:AddListener(function(dir)
        if dir == CS.UnityEngine.UI.NaviDirection.Right then
            self.view.buildingCommon.view.buttonsNaviGroup:NaviToThisGroup()
        end
    end)
    local canMoveItem = itemMoveCheckFunc()
    self.view.inventoryArea:ToggleAllQuickDropBindings(canMoveItem)

    self:_InitController()


    if FactoryUtils.isOthersSocialBuilding(self.m_nodeId) then
        EventLogManagerInst.itemBagDepotTransferPath = 3
    else
        EventLogManagerInst.itemBagDepotTransferPath = 2
    end
end

FacInventoryStationCtrl.OnShow = HL.Override() << function(self)
    self:_UpdateView()
end

FacInventoryStationCtrl.OnClose = HL.Override() << function(self)
    EventLogManagerInst.itemBagDepotTransferPath = 1
end

FacInventoryStationCtrl._UpdateView = HL.Method() << function(self)

end

FacInventoryStationCtrl._OnBuildingStateChanged = HL.Method(GEnums.FacBuildingState) << function(self, state)
    local linkStateName, depotStateName = "Normal", "Normal"
    local disableTipsTextId
    if state == FacBuildingState.Closed then
        linkStateName, depotStateName = "Stopped", "Disabled"
        disableTipsTextId = "LUA_FAC_INVENTORY_STATION_CLOSED"
    elseif state == FacBuildingState.NoPower then
        linkStateName, depotStateName = "PowerNotEnough", "Disabled"
        disableTipsTextId = "LUA_FAC_INVENTORY_STATION_NO_POWER"
    elseif state == FacBuildingState.NotInPowerNet then
        linkStateName, depotStateName = "NotLinked", "Disabled"
        disableTipsTextId = "LUA_FAC_INVENTORY_STATION_NOT_IN_POWER_NET"
    end
    self.view.linkNodeStateCtrl:SetState(linkStateName)
    self.view.depotNodeStateCtrl:SetState(depotStateName)
    if disableTipsTextId then
        local domainName = Utils.getCurDomainName()
        self.view.disabledTipsText.text = string.format(Language[disableTipsTextId], tostring(domainName))
    end


    local isDepotDisabled = depotStateName == "Disabled"

    if isDepotDisabled then
        if not self.m_waitInitNaviTarget then
            if not self.view.inventoryArea:IsNaviGroupTopLayer(true) then
                self.view.inventoryArea:NaviToPart(true, true)
            end
        end
    end

    self.view.inventoryArea:ToggleAllQuickDropBindings(not isDepotDisabled)

    self.view.moveItemMouseHintNode.moveGridHint.gameObject:SetActive(not isDepotDisabled)
    self.view.moveItemMouseHintNode.moveAllHint.gameObject:SetActive(not isDepotDisabled)

    self.m_naviGroupSwitcher:ToggleActive(not isDepotDisabled)
end

FacInventoryStationCtrl._UpdateInventoryItemCell = HL.Method(HL.Userdata, HL.Any, HL.Number, HL.Boolean)
    << function(self, cell, itemBundle, luaIndex, isItemBag)
    if cell == nil or itemBundle == nil then
        return
    end

    if DeviceInfo.usingController then
        if self.m_waitInitNaviTarget then
            local canMoveItem = self:_CanMoveItem()
            if canMoveItem and isItemBag then

            else
                self.m_waitInitNaviTarget = false
                self.view.inventoryArea:NaviToPart(isItemBag, false)
            end
        end
    end
end

FacInventoryStationCtrl._CanMoveItem = HL.Method().Return(HL.Boolean) << function(self)
    local buildingState = self.view.buildingCommon.lastState
    return buildingState == FacBuildingState.Normal
end

FacInventoryStationCtrl._CheckItemValid = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    return true
end

FacInventoryStationCtrl._InitController = HL.Method() << function(self)
    self:_RefreshNaviGroupSwitcherInfos()
end

FacInventoryStationCtrl._RefreshNaviGroupSwitcherInfos = HL.Method() << function(self)
    if self.m_naviGroupSwitcher == nil then
        return
    end

    local naviGroupInfos = {
        self.view.inventoryArea:GetItemBagNaviGroupSwitchInfo(),
        self.view.inventoryArea:GetDepotNaviGroupSwitchInfo(),
    }
    self.m_naviGroupSwitcher:ChangeGroupInfos(naviGroupInfos)
end

HL.Commit(FacInventoryStationCtrl)
