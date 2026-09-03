
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainItemTransfer
local PHASE_ID = PhaseId.DomainItemTransfer


local INDEX_CONST = 1

local RouteStatus = GEnums.DomainTransportRouteStatusType
local UPPER_PLAT_DOMAIN = "domain_1"
local LOWER_PLAT_DOMAIN = "domain_2"
local SEC_PER_HOUR = 3600
local MIN_PER_HOUR = 60
local SEC_PER_MIN = 60

local NORMAL_TRANSMISSION_STATE = "WarehouseTransfer"
local LOSSLESS_TRANSMISSION_STATE = "NoConsumptionTransfer"

DomainItemTransferCtrl = HL.Class('DomainItemTransferCtrl', uiCtrl.UICtrl)






DomainItemTransferCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FAC_TRANS_ROUTE_CHANGE] = 'OnNotifyRouteInfoChange',
}

DomainItemTransferCtrl.m_domainInfoList = HL.Field(HL.Table)

DomainItemTransferCtrl.m_transmissionCellCache = HL.Field(HL.Forward("UIListCache"))



DomainItemTransferCtrl.m_curFocusCellLuaIndex = HL.Field(HL.Number) << 1

DomainItemTransferCtrl.m_controllerInit = HL.Field(HL.Boolean) << false




DomainItemTransferCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.m_domainInfoList = {}
    self.m_transmissionCellCache = UIUtils.genCellCache(self.view.transmissionCell)

    self:_BuildDomainList()
    self:_SetPlatformInfo()
    self:_BuildRoutes()
    self:_TryRecoverDomainItemTransferSelect(arg and arg.domainItemTransferSelectState)


    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_RefreshAllCell()
        end
    end)
end

DomainItemTransferCtrl.OnAnimationInFinished = HL.Override() << function(self)
    if DeviceInfo.usingController and not self.m_controllerInit then
        self:_InitControllerAbility()
        self:_SetFocusTargetByIndex(self.m_curFocusCellLuaIndex)
    end
end

DomainItemTransferCtrl.OnClose = HL.Override() << function(self)
    if UIManager:IsOpen(PanelId.DomainItemTransferSelect) then
        UIManager:Close(PanelId.DomainItemTransferSelect)
    end
end

DomainItemTransferCtrl.OnNotifyRouteInfoChange = HL.Method() << function(self)
    self:_RebuildAll()
end

DomainItemTransferCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return {
        domainItemTransferSelectState = self:_GetDomainItemTransferSelectRecoverState(),
    }
end

DomainItemTransferCtrl._RebuildAll = HL.Method() << function(self)
    self.m_domainInfoList = {}

    self:_BuildDomainList()
    self:_SetPlatformInfo()
    self:_BuildRoutes()
end

DomainItemTransferCtrl._SetPlatformInfo = HL.Method() << function(self)


    local upperText = Tables.domainDataTable[UPPER_PLAT_DOMAIN].domainName
    self.view.platformTopCell.destinationTxt.text = upperText
    local lowerText = Tables.domainDataTable[LOWER_PLAT_DOMAIN].domainName
    self.view.platformBottomCell.destinationTxt.text = lowerText
end

DomainItemTransferCtrl._BuildDomainList = HL.Method() << function(self)

    self.m_domainInfoList = {}
    for _, domainInfo in pairs(Tables.domainDataTable) do
        table.insert(self.m_domainInfoList, domainInfo)
    end
    table.sort(self.m_domainInfoList, Utils.genSortFunction({"sortId"}, true))
end

DomainItemTransferCtrl._GetDomainRouteInfo = HL.Method().Return(HL.Table) << function(self)

    local ret = {}
    for _, domainInfo in ipairs(self.m_domainInfoList) do
        local domainId = domainInfo.domainId
        table.insert(ret, self:_GetRouteInfo(domainId))
    end
    return ret
end

DomainItemTransferCtrl._BuildRoutes = HL.Method() << function(self)
    local domainRouteInfo = self:_GetDomainRouteInfo()
    local buildCellTable = {}

    for i = 1, 2 do
        local routeInfo = domainRouteInfo[i]

        if i == 1 then
            self:_BuildTrackView(self.view.rightTrack, routeInfo)
        else
            self:_BuildTrackView(self.view.leftTrack, routeInfo)
        end
        if routeInfo ~= nil then
            table.insert(buildCellTable, routeInfo)
        end
    end
    self.m_transmissionCellCache:Refresh(#buildCellTable, function(cell, index)
        self:_BuildRouteTransmissionCellView(cell, buildCellTable[index])

        cell.domainItemTransferInfoCell.onGroupSetAsNaviTarget:RemoveAllListeners()
        cell.domainItemTransferInfoCell.onGroupSetAsNaviTarget:AddListener(function()
            self.m_curFocusCellLuaIndex = index
        end)
    end)
    self:_RefreshAllCell()
end

DomainItemTransferCtrl._BuildRouteTransmissionCellView = HL.Method(HL.Any, HL.Any) << function(self, cell, info)

    cell.info = info

    local isIdle = info.status == RouteStatus.idle and not string.isEmpty(info.toDomain)
    local isTransmitting = info.status == RouteStatus.working or info.status == RouteStatus.notFill
    local isBlocked = info.status == RouteStatus.blocked
    local isRetry = info.status == RouteStatus.retry
    local isNoTarget = string.isEmpty(info.toDomain)

    cell.idleRoot.gameObject:SetActive(isIdle)
    cell.transmittingRoot.gameObject:SetActive(isTransmitting)
    cell.blockRoot.gameObject:SetActive(isBlocked)
    cell.retryRoot.gameObject:SetActive(isRetry)
    cell.noTargetRoot.gameObject:SetActive(isNoTarget)
    cell.notEnoughItemRoot.gameObject:SetActive(false)


    if info.lossless then
        cell.transmittingRoot.stateController:SetState(LOSSLESS_TRANSMISSION_STATE)
    else
        cell.transmittingRoot.stateController:SetState(NORMAL_TRANSMISSION_STATE)
    end

    self:_SetSideCell(cell.leftSideNode, info.fromDomain)
    self:_SetSideCell(cell.rightSideNode, info.toDomain)

    cell.normalItem.gameObject:SetActive(isTransmitting or isBlocked)
    cell.retryItem.gameObject:SetActive(isRetry)
    cell.idleItem.gameObject:SetActive(isIdle)

    cell.focusItemNode.gameObject:SetActive(isTransmitting or isBlocked or isRetry)

    cell.restartBtn.onClick:RemoveAllListeners()
    cell.restartBtn.onClick:AddListener(function()
        self:_ReqRestartRoute(info)
    end)

    cell.editBtn.onClick:RemoveAllListeners()
    cell.editBtn.onClick:AddListener(function()
        self:_OpenEditPanel(info)
    end)

    cell.restartBtn.gameObject:SetActive(isBlocked)
    cell.editBtn.gameObject:SetActive(true)

    if isRetry then
        local itemInfoPack = {
            id = info.itemId,
            count = info.itemNumMax,
        }
        cell.retryItem:InitItem(itemInfoPack, true)
        cell.retryItem:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        return
    end

    if isTransmitting or isBlocked then
        local itemInfoPack = {
            id = info.itemId,
            count = info.itemNum
        }
        cell.normalItem:InitItem(itemInfoPack, true)
        cell.normalItem:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        if info.itemNum ~= info.itemNumMax then
            cell.normalItem.view.count.text = tostring(info.itemNum) .. "/" .. tostring(info.itemNumMax)
            cell.notEnoughItemRoot.gameObject:SetActive(true)
        end
        return
    end
end

DomainItemTransferCtrl._RefreshAllCell = HL.Method() << function(self)
    self.m_transmissionCellCache:Update(function(cell, index)
        self:_RefreshCellTimeText(cell)
        self:_RefreshCellBlockState(cell)
        self:_RefreshItemCount(cell)
    end)
end

DomainItemTransferCtrl._RefreshCellTimeText = HL.Method(HL.Any) << function(self, cell)
    local info = cell.info
    cell.transmittingRoot.timeText.text = self:_GetTimeTextV2(info)
    cell.retryRoot.retryText.text = self:_GetTimeTextV2(info)
end

DomainItemTransferCtrl._RefreshCellBlockState = HL.Method(HL.Any) << function(self, cell)
    local info = cell.info
    if info.status ~= RouteStatus.blocked then
        return
    end
    local itemId = info.itemId
    local itemCount = info.itemNum

    local factoryDepot = GameInstance.player.inventory.factoryDepot
    local depotInChapter = factoryDepot:GetOrFallback(Utils.getCurrentScope())
    local actualDepot = depotInChapter[ScopeUtil.ChapterIdStr2Int(info.toDomain)]
    local curCount = actualDepot:GetCount(itemId)
    local canPut = curCount + itemCount <= Utils.getDepotItemStackLimitCount(info.toDomain)
    cell.blockRoot.pleaseRestartNode.gameObject:SetActive(canPut)
    cell.blockRoot.spaceIsNotEnoughNode.gameObject:SetActive(not canPut)

    cell.restartBtn.gameObject:SetActive(canPut)
end

DomainItemTransferCtrl._RefreshItemCount = HL.Method(HL.Any) << function(self, cell)
    local info = cell.info
    local isTransmitting = info.status == RouteStatus.working or info.status == RouteStatus.notFill
    local isBlocked = info.status == RouteStatus.blocked
    if isTransmitting or isBlocked then
        local itemInfoPack = {
            id = info.itemId,
            count = info.itemNum
        }
        cell.normalItem:InitItem(itemInfoPack, true)
        cell.normalItem:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        if info.itemNum ~= info.itemNumMax then
            cell.normalItem.view.count.text = tostring(info.itemNum) .. "/" .. tostring(info.itemNumMax)
            cell.notEnoughItemRoot.gameObject:SetActive(true)
        end
        return
    end
end

DomainItemTransferCtrl._SetSideCell = HL.Method(HL.Any, HL.Any) << function(self, sideCell, domainId)
    local selected = not string.isEmpty(domainId)
    sideCell.selectedRoot.gameObject:SetActive(selected)
    sideCell.nonSelectedRoot.gameObject:SetActive(not selected)
    if selected then
        local domainInfo = Tables.domainDataTable[domainId]
        sideCell.selectedText.text = domainInfo.domainName
        sideCell.selectedIcon:LoadSprite(UIConst.UI_SPRITE_FAC_TRANS,
            UIConst.FAC_TRANS_DOMAIN_ICONS[domainId])
    end
end

DomainItemTransferCtrl._BuildTrackView = HL.Method(HL.Any, CS.Beyond.Gameplay.RemoteFactorySystem.FactoryHubTransportRouteData)
        << function(self, track, routeInfo)


    local notUnlocked = routeInfo == nil
    local stuck = false
    local idle = false
    local working = false
    local noTarget = false

    if not notUnlocked then
        stuck = routeInfo.status == RouteStatus.blocked
        idle = routeInfo.status == RouteStatus.idle or routeInfo.status == RouteStatus.retry
        working = routeInfo.status == RouteStatus.working or routeInfo.status == RouteStatus.notFill
        if string.isEmpty(routeInfo.toDomain) then
            noTarget = true
            idle = false
        end


        if routeInfo.lossless then
            track.normalRoot:SetState(LOSSLESS_TRANSMISSION_STATE)
        else
            track.normalRoot:SetState(NORMAL_TRANSMISSION_STATE)
        end
    end

    track.notUnlockedRoot.gameObject:SetActive(notUnlocked or noTarget)
    track.stuckRoot.gameObject:SetActive(stuck)
    track.normalRoot.gameObject:SetActive(working)
    track.idleRoot.gameObject:SetActive(idle)
    track.noTargetRoot.gameObject:SetActive(false)
end

DomainItemTransferCtrl._GetRouteInfo = HL.Method(HL.String).Return(HL.Any) << function(self, domainId)
    return GameInstance.player.remoteFactory:GetFacHubTransData(domainId, INDEX_CONST)
end

DomainItemTransferCtrl._GetTimeTextV2 = HL.Method(HL.Userdata).Return(HL.String) << function(self, info)
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local lastTryTime = info.timeStamp - info.progress
    local curProgress = curTime - lastTryTime
    local needTime = Tables.factoryConst.domainTransportIntervalTime
    local curNeedTimeSec = needTime - curProgress
    return UIUtils.getLeftTimeToSecond(curNeedTimeSec)
end

DomainItemTransferCtrl._GetTimeText = HL.Method(HL.Any).Return(HL.String) << function(self, info)
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local lastTryTime = info.timeStamp - info.progress
    local curProgress = curTime - lastTryTime
    local needTime = Tables.factoryConst.domainTransportIntervalTime
    local curNeedTimeSec = needTime - curProgress
    local curNeedHour = curNeedTimeSec // SEC_PER_HOUR
    local restSec = curNeedTimeSec % SEC_PER_HOUR
    local curNeedMin = restSec // SEC_PER_MIN
    restSec = restSec % SEC_PER_MIN
    if restSec % SEC_PER_MIN > 0 then
        curNeedMin = curNeedMin + 1
    end

    if curNeedMin >= MIN_PER_HOUR then
        curNeedMin = curNeedMin - 60
        curNeedHour = curNeedHour + 1
    end
    local hourText = ""
    if curNeedHour > 0 then
        hourText = string.format(Language.LUA_TIME_HOUR, curNeedHour)
    end
    local minuteText = ""
    if curNeedMin > 0 then
        minuteText = string.format(Language.LUA_TIME_MIN, curNeedMin)
    end
    local text = hourText .. minuteText
    return text
end

DomainItemTransferCtrl._ReqRestartRoute = HL.Method(HL.Any) << function(self, routeInfo)
    GameInstance.player.remoteFactory:SendReqRestartHubTransRoute(routeInfo.fromDomain, routeInfo.index)
end

DomainItemTransferCtrl._OpenEditPanel = HL.Method(HL.Any) << function(self, routeInfo)
    local args = {
        info = routeInfo
    }
    UIManager:Open(PanelId.DomainItemTransferSelect, args)
end

DomainItemTransferCtrl._GetDomainItemTransferSelectRecoverState = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local isOpen, ctrl = UIManager:IsOpen(PanelId.DomainItemTransferSelect)
    if not isOpen or not ctrl:IsShow() then
        return
    end
    return ctrl:GetRecoverStateArg()
end

DomainItemTransferCtrl._TryRecoverDomainItemTransferSelect = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil or string.isEmpty(recoverState.routeFromDomain) then
        return
    end
    local isOpen = UIManager:IsOpen(PanelId.DomainItemTransferSelect)
    if isOpen then
        return
    end
    local routeInfo = self:_GetRouteInfo(recoverState.routeFromDomain)
    if routeInfo == nil then
        return
    end
    UIManager:Open(PanelId.DomainItemTransferSelect, {
        info = routeInfo,
        recoverState = recoverState,
    })
end




DomainItemTransferCtrl._InitControllerAbility = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.m_controllerInit = true







end















DomainItemTransferCtrl._SetFocusTargetByIndex = HL.Method(HL.Number) << function(self, index)
    self.m_curFocusCellLuaIndex = index

    local firstCell = self.m_transmissionCellCache:Get(index)
    self:SetNaviTarget(firstCell.domainItemTransferInfoCell)
end


HL.Commit(DomainItemTransferCtrl)
