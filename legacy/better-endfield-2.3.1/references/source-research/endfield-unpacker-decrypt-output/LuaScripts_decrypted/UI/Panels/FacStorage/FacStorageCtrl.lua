
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacStorage
FacStorageCtrl = HL.Class('FacStorageCtrl', uiCtrl.UICtrl)







FacStorageCtrl.s_messages = HL.StaticField(HL.Table) << {

}


FacStorageCtrl.m_nodeId = HL.Field(HL.Any)

FacStorageCtrl.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_DepositBox)

FacStorageCtrl.m_lastCDTime = HL.Field(HL.Number) << 1

FacStorageCtrl.m_lastIsDisabled = HL.Field(HL.Boolean) << false

FacStorageCtrl.m_confirmBtnImage = HL.Field(HL.Any)

FacStorageCtrl.m_confirmBtnImageOriginColor = HL.Field(HL.Any)

FacStorageCtrl.m_waitingForResponse = HL.Field(HL.Boolean) << false



FacStorageCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_uiInfo = arg.uiInfo
    local nodeId = self.m_uiInfo.nodeId
    self.m_nodeId = nodeId

    self.view.inventoryArea:InitInventoryArea({
        onStateChange = function()
            self:_RefreshNaviGroupSwitcherInfos()
        end,

        customSetActionMenuArgs = function(actionMenuArgs)
            actionMenuArgs.storage = self.view.storageContent
        end,
    })
    self.view.inventoryArea:LockInventoryArea(FactoryUtils.isBuildingInventoryLocked(nodeId))

    self.view.storageContent:InitStorageContent(self.m_uiInfo.gridBox, nil, {
        dropAcceptTypes = UIConst.FACTORY_STORAGER_DROP_ACCEPT_INFO,
    })

    self.view.facCacheBelt:InitFacCacheBelt(self.m_uiInfo,{
        noGroup = true,
    })

    self:_InitWirelessMode()

    self.view.buildingCommon:InitBuildingCommon(self.m_uiInfo, {
        onStateChanged = function(state)
            self:_OnStateChanged(state)
        end
    })

    GameInstance.remoteFactoryManager:RegisterInterestedUnitId(nodeId)

    self:_InitStorageController()
end

FacStorageCtrl.OnClose = HL.Override() << function(self)
    GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(self.m_nodeId)
end

FacStorageCtrl._OnStateChanged = HL.Method(HL.Userdata) << function(self, state)
    self.view.wirelessModeNode:RefreshPausedState(
        state ~= GEnums.FacBuildingState.Normal and state ~= GEnums.FacBuildingState.Idle and state ~= GEnums.FacBuildingState.Blocked
    )
end

FacStorageCtrl._InitWirelessMode = HL.Method() << function(self)
    self.view.wirelessModeNode:InitWirelessModeNode(self.m_uiInfo, function()
        self:_CheckCacheItemStateOnComplete(false)
    end)

    local nodeHandler = self.m_uiInfo.nodeHandler
    if nodeHandler ~= nil then
        local pdb = nodeHandler.predefinedParam
        if pdb ~= nil then
            local gridBox = pdb.gridBox
            if gridBox ~= nil then
                self.view.wirelessModeNode:RefreshSwitchValidState(gridBox.enableAutoTransfer)
            end
        end
    end

    self:_CheckCacheItemStateOnComplete(true)
end

FacStorageCtrl._CheckCacheItemStateOnComplete = HL.Method(HL.Boolean) << function(self, forceRefresh)
    local items = self.m_uiInfo.gridBox.items
    if items.Count <= 0 then
        return
    end

    local isBlocked = false
    local repoStackLimit = Utils.getDepotItemStackLimitCountInCurrentDomain()
    for _, itemPair in pairs(items) do
        local id, count = itemPair.Item1, itemPair.Item2
        if count > 0 then
            local depotCount = Utils.getDepotItemCount(id, Utils.getCurrentScope(), Utils.getCurDomainId())
            if depotCount >= repoStackLimit then
                isBlocked = true
                break
            end
        end
    end

    if isBlocked then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_WIRELESS_MODE_BLOCKED_TOAST)
    end
    self.view.wirelessModeNode:RefreshBlockedState(isBlocked, forceRefresh)
end




FacStorageCtrl.m_naviGroupSwitcher = HL.Field(HL.Forward('NaviGroupSwitcher'))

FacStorageCtrl._InitStorageController = HL.Method() << function(self)
    local NaviGroupSwitcher = require_ex("Common/Utils/UI/NaviGroupSwitcher").NaviGroupSwitcher
    self.m_naviGroupSwitcher = NaviGroupSwitcher(self.view.inputGroup.groupId, nil, true)
    self:_RefreshNaviGroupSwitcherInfos()
end

FacStorageCtrl._RefreshNaviGroupSwitcherInfos = HL.Method() << function(self)
    if self.m_naviGroupSwitcher == nil then
        return
    end

    local naviGroupInfos = {
        { naviGroup = self.view.storageContent.view.naviGroup },
    }
    self.view.inventoryArea:AddNaviGroupSwitchInfo(naviGroupInfos)
    self.m_naviGroupSwitcher:ChangeGroupInfos(naviGroupInfos)
end




HL.Commit(FacStorageCtrl)
