local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local InstCellState = {
    Ready = "Ready",
    Preparing = "Preparing",
    Trading = "Trading",
    Locked = "Locked",
    Delivering = "Delivering",
}

DomainDepotInstCell = HL.Class('DomainDepotInstCell', UIWidgetBase)

local MAX_LEVEL_FORMAT = "/%s"

DomainDepotInstCell.m_depotId = HL.Field(HL.String) << ""

DomainDepotInstCell.m_instCellState = HL.Field(HL.String) << ""

DomainDepotInstCell.m_itemTypeCell = HL.Field(HL.Forward('UIListCache'))

DomainDepotInstCell.m_nextAvailablePackTimestamp = HL.Field(HL.Number) << 0

DomainDepotInstCell.m_countdownTextFormat = HL.Field(HL.String) << ""

DomainDepotInstCell.m_preparingTimeTickThread = HL.Field(HL.Number) << -1




DomainDepotInstCell.m_onNaviTargetChanged = HL.Field(HL.Function)


DomainDepotInstCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_itemTypeCell = UIUtils.genCellCache(self.view.hintNode.deliverItemTypeCell)

    self.view.mapBtn.onClick:AddListener(function()
        self:_OnClickMapBtn()
    end)

    self.view.confirmBtn.onClick:AddListener(function()
        self:_OnClickConfirmBtn()
    end)

    self:RegisterMessage(MessageConst.ON_FINISH_DOMAIN_DEPOT_DELIVER, function(args)
        self:_RefreshInstState()
    end)
    self:RegisterMessage(MessageConst.ON_DOMAIN_DEPOT_DAILY_REFRESHED, function(args)
        self:_RefreshInstState()
    end)
    self:RegisterMessage(MessageConst.ON_PACK_ITEM_END, function(args)
        self:_RefreshInstState()
    end)
end

DomainDepotInstCell.InitDomainDepotInstCell = HL.Method(HL.String) << function(self, depotId)
    self:_FirstTimeInit()

    self.m_depotId = depotId
    self.m_countdownTextFormat = Language.LUA_DOMAIN_DEPOT_INST_CELL_COUNTDOWN
    self.view.redDot:InitRedDot("DomainDepotInstCell", depotId)

    self:_RefreshBasicInfo()
    self:_RefreshInstState()
    self:_InitCellController()
end

DomainDepotInstCell._OnDestroy = HL.Override() << function(self)
    if self.m_preparingTimeTickThread > 0 then
        self.m_preparingTimeTickThread = LuaUpdate:Remove(self.m_preparingTimeTickThread)
    end

    self.view.confirmBtn.onIsNaviTargetChanged = nil
    self.m_onNaviTargetChanged = nil
end

DomainDepotInstCell._OnClickMapBtn = HL.Method() << function(self)
    local success, markInstId = GameInstance.player.mapManager:GetMapMarkInstId(GEnums.MarkType.DomainDepot, self.m_depotId)
    if not success then
        return
    end
    MapUtils.openMap(markInstId)
end

DomainDepotInstCell._OnClickConfirmBtn = HL.Method() << function(self)
    if self.m_instCellState == InstCellState.Ready then
        Notify(MessageConst.ON_OPEN_DOMAIN_DEPOT_PACK_TYPE_SELECT_PANEL, { depotId = self.m_depotId })
    elseif self.m_instCellState == InstCellState.Trading then
        Notify(MessageConst.ON_OPEN_DOMAIN_DEPOT_PACK_SELL_PANEL, { domainDepotId = self.m_depotId, simpleOpen = true })
    elseif self.m_instCellState == InstCellState.Delivering then
        PhaseManager:GoToPhase(PhaseId.Mission, { autoSelect = Tables.domainDepotConst.depotDeliverMissionId })
    end
end

DomainDepotInstCell._RefreshBasicInfo = HL.Method() << function(self)
    local depotInfo = DomainDepotUtils.GetDepotInfo(self.m_depotId)
    local depotTableConfig = depotInfo.depotTableConfig
    local currLevel, maxLevel = depotInfo.currLevel,depotInfo.maxLevel
    local currLevelConfig = depotInfo.currLevelConfig


    self.view.nameTxt.text = depotTableConfig.depotName


    local levelNode = self.view.levelNode
    levelNode.currentTxt.text = tostring(currLevel)
    levelNode.maxTxt.text = string.format(MAX_LEVEL_FORMAT, maxLevel)
    levelNode.maxNode.gameObject:SetActive(currLevel == maxLevel and depotInfo.isFinalMaxLevel)


    local hintNode = self.view.hintNode
    hintNode.extraLimitTxt.text = tonumber(currLevelConfig.extraDepotLimit)

    if currLevelConfig.deliverItemTypeList.Count > 0 then
        self.m_itemTypeCell:Refresh(currLevelConfig.deliverItemTypeList.Count, function(cell, index)
            local itemType = currLevelConfig.deliverItemTypeList[CSIndex(index)]
            local itemTypeData = Tables.domainDepotDeliverItemTypeTable[itemType]
            cell.iconImg:LoadSprite(UIConst.UI_SPRITE_INVENTORY, itemTypeData.typeIcon)
            cell.nameTxt.text = itemTypeData.typeDesc
        end)
        hintNode.deliverHintText.gameObject:SetActive(true)
        hintNode.deliverItemHintList.gameObject:SetActive(true)
    else
        hintNode.deliverHintText.gameObject:SetActive(false)
        hintNode.deliverItemHintList.gameObject:SetActive(false)
    end


    local depotId = depotTableConfig.domainId
    DomainDepotUtils.SetDomainColorToDepotNodes(depotId, {
        self.view.dyeCircleNode,
        self.view.dyeRingNode,
        self.view.dyePrgImage,
        self.view.bottomDecoImage,
        self.view.gradImage,
        self.view.positionBgImage,
    })


    self.view.depotImg:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT_INST, depotTableConfig.depotImage)
end

DomainDepotInstCell._RefreshAndSetInstCellState = HL.Method(HL.String, HL.Boolean) << function(self, state, needRefreshProgress)
    self.view.stateController:SetState(state)
    if needRefreshProgress then
        self.view.progressController:SetState(state)
    end
    self.m_instCellState = state

    local invalidState = state == InstCellState.Locked or state == InstCellState.Preparing
    if invalidState then
        local recoverColorNodes = {
            self.view.dyeCircleNode,
            self.view.dyeRingNode,
            self.view.dyePrgImage,
            self.view.positionBgImage,
        }
        for _, node in pairs(recoverColorNodes) do
            node.color = Color.white
        end
    end

    if DeviceInfo.usingController then
        self.view.confirmBtn:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.None)
    else
        self.view.confirmBtn.interactable = not invalidState
    end

    if DeviceInfo.usingController then
        local naviAction = invalidState and CS.Beyond.Input.ActionOnSetNaviTarget.None or CS.Beyond.Input.ActionOnSetNaviTarget.PressConfirmTriggerOnClick
        self.view.confirmBtn:ChangeActionOnSetNaviTarget(naviAction)
    end
end

DomainDepotInstCell._RefreshInstState = HL.Method() << function(self)
    local depotInfo = DomainDepotUtils.GetDepotInfo(self.m_depotId)
    local depotRuntimeData = depotInfo.depotRuntimeData
    local currLevelConfig = depotInfo.currLevelConfig
    local deliverInfo = GameInstance.player.domainDepotSystem:GetDomainDepotDeliverInfoByDepotId(self.m_depotId)
    local enterPreparingState = function()
        self:_RefreshAndSetInstCellState(InstCellState.Preparing, true)
        self.m_nextAvailablePackTimestamp = Utils.getNextCommonServerRefreshTime()
        if self.m_preparingTimeTickThread > 0 then
            self.m_preparingTimeTickThread = LuaUpdate:Remove(self.m_preparingTimeTickThread)
        end
        self.m_preparingTimeTickThread = LuaUpdate:Add("Tick", function(deltaTime)
            self:_RefreshPreparingRemainingTime()
        end)
    end













    if currLevelConfig.deliverItemTypeList.Count <= 0 then
        self:_RefreshAndSetInstCellState(InstCellState.Locked, false)
        return
    end

    if depotRuntimeData.canPack then
        self:_RefreshAndSetInstCellState(InstCellState.Ready, true)
        return
    end

    if deliverInfo == nil or deliverInfo.delegateToOther or deliverInfo.delegateFromOther then
        enterPreparingState()
        return
    end

    local isTrading = deliverInfo.packageProgress == GEnums.DomainDepotPackageProgress.WaitingSelectBuyer
    if isTrading then
        self:_RefreshAndSetInstCellState(InstCellState.Trading, true)
    else
        self:_RefreshAndSetInstCellState(InstCellState.Delivering, true)
    end
end

DomainDepotInstCell._RefreshPreparingRemainingTime = HL.Method() << function(self)
    local currTimestamp = DateTimeUtils.GetCurrentTimestampBySeconds()
    local remainSeconds = math.floor(self.m_nextAvailablePackTimestamp - currTimestamp + 0.5)
    remainSeconds = math.max(remainSeconds, 0)
    local remainHours = math.floor(remainSeconds / Const.SEC_PER_HOUR)
    local remainMinutes = math.floor((remainSeconds % Const.SEC_PER_HOUR) / Const.SEC_PER_MIN)
    self.view.countdownTxt.text = string.format(self.m_countdownTextFormat, remainHours, remainMinutes)
end




DomainDepotInstCell._InitCellController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    self.view.inputGroup.enabled = false
    self.view.confirmKeyHint.gameObject:SetActive(false)
    self.view.confirmBtn.onIsNaviTargetChanged = function(isNaviTarget)
        self.view.confirmKeyHint.gameObject:SetActive(isNaviTarget)
        self.view.inputGroup.enabled = isNaviTarget
        if self.m_onNaviTargetChanged then
            self.m_onNaviTargetChanged(isNaviTarget)
        end
    end
end


DomainDepotInstCell.SetOnNaviTargetChanged = HL.Method(HL.Opt(HL.Function)) << function(self, callback)
    self.m_onNaviTargetChanged = callback
end




HL.Commit(DomainDepotInstCell)
return DomainDepotInstCell

