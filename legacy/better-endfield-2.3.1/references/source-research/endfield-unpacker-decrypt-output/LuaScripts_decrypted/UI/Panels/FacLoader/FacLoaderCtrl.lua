local LogisticPortLinkageStatus = FacCoreNS.FactoryGridLogisticSystem.LogisticPortLinkageStatus
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacLoader
FacLoaderCtrl = HL.Class('FacLoaderCtrl', uiCtrl.UICtrl)

local INVALID_COUNT_TEXT = "--"







FacLoaderCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacLoaderCtrl.m_nodeId = HL.Field(HL.Any)

FacLoaderCtrl.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_BusLoader)


FacLoaderCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_uiInfo = arg.uiInfo
    local nodeId = self.m_uiInfo.nodeId
    self.m_nodeId = nodeId

    self.view.buildingCommon:InitBuildingCommon(self.m_uiInfo)
    self.view.facCacheBelt:InitFacCacheBelt(self.m_uiInfo, {
        noGroup = true,
        stateRefreshCallback = function(portInfo)
            self:_RefreshBlockState(portInfo.isBlock)
        end
    })

    local domainId = Utils.getCurDomainId()
    local succ, domainData = Tables.domainDataTable:TryGetValue(domainId)
    if succ then
        self.view.countTitle.text = domainData.storageName
    else

        self.view.countTitle.text = Language.LUA_BLACK_BOX_DEPOT_NAME
    end

    self:_UpdateTransferItem()
    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_UpdateTransferItem()
        end
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    GameInstance.player.remoteFactory.core:Message_HSFB(Utils.getCurrentChapterId(), false, {1})

    GameInstance.remoteFactoryManager:RegisterInterestedUnitId(nodeId)

    self:_NaviToLoadingInfo()
end

FacLoaderCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.remoteFactory.core:Message_HSFB(Utils.getCurrentChapterId(), true, {1})
    GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(self.m_nodeId)
end

FacLoaderCtrl._UpdateTransferItem = HL.Method() << function(self)
    local id = self.m_uiInfo.lastLoadItemId
    local itemExist = not string.isEmpty(id)

    self.view.loadingEmptyNode.gameObject:SetActive(not itemExist)
    self.view.loadingItem.gameObject:SetActive(itemExist)

    if DeviceInfo.usingController and id ~= self.view.loadingItem.id then
        self:_NaviToLoadingInfo()
    end

    if itemExist then
        local count = Utils.getDepotItemCount(id)
        if id ~= self.view.loadingItem.id then
            self.view.loadingItem:InitItem({id = id, count = 1}, true)
            local success, itemData = Tables.itemTable:TryGetValue(id)
            if success then
                local iconId = itemData.iconId
                self.view.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, iconId)
            end
        end

        local text = tostring(count)
        local itemColor = count <= 0 and self.view.config.COLOR_STORAGE_EMPTY or self.view.config.COLOR_STORAGE_NORMAL
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

FacLoaderCtrl._RefreshBlockState = HL.Method(HL.Boolean) << function(self, isBlock)
    local state = isBlock and GEnums.FacBuildingState.Blocked or GEnums.FacBuildingState.Normal
    self.view.buildingCommon:ChangeBuildingStateDisplay(state)
end

FacLoaderCtrl._RefreshItemCount = HL.Method(HL.String, HL.Any) << function(self, countText, color)
    self.view.infoShadowNode.countText.text = countText
    self.view.infoNode.countText.text = countText
    self.view.infoNode.countText.color = color
end

FacLoaderCtrl._NaviToLoadingInfo = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    local itemExist = not string.isEmpty(self.m_uiInfo.lastLoadItemId)
    local naviTarget = itemExist and self.view.loadingItem.view.button or self.view.loadingEmptyNaviDecorator
    self:SetNaviTarget(naviTarget)
end

HL.Commit(FacLoaderCtrl)
