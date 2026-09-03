local DomainDepotUtils = {}

function DomainDepotUtils.GetBuyerInfo(id)
    local buyerInfo = {}

    local list = GameInstance.player.domainDepotSystem:GetDomainDepotDeliverBuyerList(id)

    if list == nil or list.Count == 0 then
        logger.error("DomainDepotUtils.GetBuyerInfo failed, no deliver info found for id: " .. tostring(id))
        return buyerInfo
    end

    local rewardId = DomainDepotUtils.GetMoneyId(id)

    for i = 0, list.Count - 1 do
        local info = list[i]
        local buyerCfg = Tables.domainDepotBuyerTable[info.buyerId]
        table.insert(buyerInfo, {
            id = info.buyerId,
            headIcon = buyerCfg.headIcon,
            name = buyerCfg.buyerName,
            desc = buyerCfg.desc,
            targetId = info.targetId,
            isCritical = info.isCritical,
            reward = {
                id = rewardId,
                count = info.rewardValue,
            }
        })
    end

    table.sort(buyerInfo, function(a, b)
        return a.reward.count > b.reward.count
    end)

    return buyerInfo
end

function DomainDepotUtils.GetMoneyId(id)
    local success, cfg = Tables.domainDepotTable:TryGetValue(id)
    if success and cfg then
        local dataSuccess, domainDevData = GameInstance.player.domainDevelopmentSystem.domainDevDataDic:TryGetValue(cfg.domainId)
        if not dataSuccess then
            logger.error("DomainDepotUtils.GetMoneyId failed, no domainDevData found for domainId: " .. tostring(cfg.domainId))
            return ""
        end
        return domainDevData.domainDataCfg.domainGoldItemId, domainDevData.curLevelData.moneyLimit
    end
    logger.error("DomainDepotUtils.GetMoneyId failed, id: " .. tostring(id))
    return ""
end

function DomainDepotUtils.GetDepotInfo(depotId)
    local depotRuntimeData = GameInstance.player.domainDepotSystem:GetDomainDepotDataById(depotId)
    local depotTableConfig = Tables.domainDepotTable[depotId]
    local depotLevelList = Tables.domainDepotLevelTable[depotId].levelList
    local currLevel, maxLevel = depotRuntimeData.level, DomainPOIUtils.GetDomainDepotMaxLevel(depotId)
    local currLevelConfig, maxLevelConfig
    if currLevel > 0 then
        currLevelConfig = depotLevelList[currLevel]
    end
    if maxLevel > 0 then
        maxLevelConfig = depotLevelList[maxLevel]
    end

    return {
        depotRuntimeData = depotRuntimeData,
        depotTableConfig = depotTableConfig,
        depotLevelList = depotLevelList,
        currLevel = currLevel,
        maxLevel = maxLevel,
        currLevelConfig = currLevelConfig,
        maxLevelConfig = maxLevelConfig,
        isFinalMaxLevel = maxLevelConfig.isFinalMaxLevel
    }
end

function DomainDepotUtils.InitTopMoneyTitle(topMoneyTitle, domainId, onClose)
    local success, domainDevData = GameInstance.player.domainDevelopmentSystem.domainDevDataDic:TryGetValue(domainId)
    if not success then
        return
    end

    topMoneyTitle:InitDomainTopMoneyTitle(domainId)
    topMoneyTitle.view.closeBtn.onClick:AddListener(function()
        onClose()
    end)
end

function DomainDepotUtils.GetDepotPackValueLimitCfg(depotId)
    local packValueLimitCfg = {}
    local success, domainValueCfg = Tables.domainDepotPackValueTable:TryGetValue(depotId)
    if not success then
        return packValueLimitCfg
    end

    for index = 0, domainValueCfg.packValueList.Count - 1 do
        local cfgData = domainValueCfg.packValueList[index]
        if packValueLimitCfg[cfgData.deliverItemType] == nil then
            packValueLimitCfg[cfgData.deliverItemType] = {}
        end
        packValueLimitCfg[cfgData.deliverItemType][cfgData.deliverPackType] = {
            minLimitValue = cfgData.minLimit,
            maxLimitValue = cfgData.maxLimit,
        }
    end

    return packValueLimitCfg
end

function DomainDepotUtils.GetDepotPackIntegrityReduceTypeList(packItemType)
    local result = {}
    for reduceType, typeCfg in pairs(Tables.domainDepotPackageIntegrityReduceTypeTable) do
        for index = 0, typeCfg.effectiveType.Count - 1 do
            if typeCfg.effectiveType[index] == packItemType then
                table.insert(result, reduceType)
                break
            end
        end
    end
    return result
end

function DomainDepotUtils.GetDomainDepotDeposit(deliverInstId)
    local deliverInfo = GameInstance.player.domainDepotSystem:GetDomainDepotDeliverInfoByInstId(deliverInstId)
    return math.floor(deliverInfo.buyerInfo.rewardValue * Tables.domainDepotConst.depositRatio / 100)
end

function DomainDepotUtils.UpdateReduceView(packageDamageReasonView,packItemType)
    local reduceTypeList = DomainDepotUtils.GetDepotPackIntegrityReduceTypeList(packItemType)
    local needShowTypeList = {}
    for _, reduceType in ipairs(reduceTypeList) do
        needShowTypeList[reduceType] = true
    end
    packageDamageReasonView.Hurt.gameObject:SetActiveIfNecessary(needShowTypeList[GEnums.ReducePackageCompletenessReason.Hurt:GetHashCode()] == true)
    packageDamageReasonView.Teleport.gameObject:SetActiveIfNecessary(needShowTypeList[GEnums.ReducePackageCompletenessReason.Teleport:GetHashCode()] == true)
    packageDamageReasonView.Jump.gameObject:SetActiveIfNecessary(needShowTypeList[GEnums.ReducePackageCompletenessReason.Jump:GetHashCode()] == true)
end

function DomainDepotUtils.ShowDepotTargetMapPreview(domainDepotId, targetId)
    local success, targetCfg = Tables.domainDepotDeliverTargetTable:TryGetValue(targetId)
    if not success then
        return
    end

    local targetLevelId = targetCfg.level
    MapUtils.openMap(nil, targetLevelId, {
        onMapPanelOpen = function()
            GameInstance.player.mapManager:AddDomainDepotDeliverMarks(domainDepotId, targetId)
        end,
        onMapPanelClose = function()
            GameInstance.player.mapManager:RemoveDomainDepotDeliverMarks()
        end,
        expectedPanelNodes = MapConst.DOMAIN_DEPOT_MAP_EXPECTED_PANEL_NODES,
        expectedStaticElementTypes = MapConst.DOMAIN_DEPOT_MAP_EXPECTED_STATIC_ELEMENT_TYPES,
        expectedMarks = MapConst.DOMAIN_DEPOT_MAP_EXPECTED_PANEL_MARKS,
        topOrderMarks = MapConst.DOMAIN_DEPOT_MAP_TOP_ORDER_PANEL_MARKS,
        noGeneralTracking = true,
        noMissionTracking = true,
        noCustomMark = true,
        forbidDetailBtn = true,
        ignoreOpenFocus = true,
    })
end

function DomainDepotUtils.DelegateCurrentDeliver()
    GameInstance.player.domainDepotSystem:SendDomainDepotDelegateReq()
end

function DomainDepotUtils.IsDeliverUnlocked(domainId)
    local domainDepotSystem = GameInstance.player.domainDepotSystem
    local allDepotIdList = domainDepotSystem:GetDomainDepotIdListByDomainId(domainId)
    for index = 0, allDepotIdList.Count - 1 do
        local depotId = allDepotIdList[index]
        local depotInfo = DomainDepotUtils.GetDepotInfo(depotId)
        if depotInfo.depotRuntimeData.level > 0 then
            if depotInfo.currLevelConfig.deliverItemTypeList.Count > 0 then
                return true
            end
        end
    end
    return false
end

function DomainDepotUtils.GetDomainIdByDepotId(depotId)
    local success, domainDepotCfg = Tables.domainDepotTable:TryGetValue(depotId)
    if not success then
        return ""
    end
    return domainDepotCfg.domainId
end

function DomainDepotUtils.SetDomainColorToDepotNodes(domainId, nodeList)
    local success, domainData = Tables.domainDataTable:TryGetValue(domainId)
    if not success then
        return
    end

    local domainColor = UIUtils.getColorByString(domainData.domainColor)
    for _, node in pairs(nodeList) do
        node.color = domainColor
    end
end

function DomainDepotUtils.IsDomainDepotDeliverInTradingState(depotId)
    local depotInfo = DomainDepotUtils.GetDepotInfo(depotId)
    if depotInfo.currLevel <= 0 then
        return false
    end

    local currLevelConfig = depotInfo.currLevelConfig
    if currLevelConfig.deliverItemTypeList.Count <= 0 then
        return false
    end

    local deliverInfo = GameInstance.player.domainDepotSystem:GetDomainDepotDeliverInfoByDepotId(depotId)
    if deliverInfo == nil or deliverInfo.delegateToOther then
        return false
    end

    return deliverInfo.packageProgress == GEnums.DomainDepotPackageProgress.WaitingSelectBuyer
end

function DomainDepotUtils.RefreshMoneyIconWithDomain(iconImage, domainId)
    local itemData = Tables.itemTable:GetValue(Tables.domainDataTable[domainId].domainGoldItemId)
    iconImage:LoadSprite(UIConst.UI_SPRITE_WALLET, itemData.iconId)
end


_G.DomainDepotUtils = DomainDepotUtils
return DomainDepotUtils
