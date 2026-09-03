local RedDotUtils = {}

function RedDotUtils.hasGemNotEquipped()
    local gemDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.WeaponGem]:GetOrFallback(Utils.getCurrentScope())
    if not gemDepot then
        return false
    end
    local gemInstDict = gemDepot.instItems
    for _, itemBundle in pairs(gemInstDict) do
        local gemInst = itemBundle.instData
        if gemInst and gemInst.weaponInstId <= 0 then
            return true
        end
    end
    return false
end

function RedDotUtils.hasBlocShopDiscountShopItem(blocId)
    local shopInfo = GameInstance.player.blocManager:GetBlocShopInfo(blocId)
    if not shopInfo then
        logger.error("RedDotUtils->hasBlocShopDiscountShopItem: shopInfo is nil, blocId", blocId)
        return false
    end

    local shopId = shopInfo.Shopid
    local discountInfoMaps = {}
    for k = 0, shopInfo.DiscountInfo.Count - 1 do
        local v = shopInfo.DiscountInfo[k]
        discountInfoMaps[v.Posid] = v.Discount
    end

    local shopItemMap = Tables.blocShopItemTable[shopId]
    if not shopItemMap then
        logger.error("RedDotUtils->hasBlocShopDiscountShopItem: shopItemMap is nil, shopId", shopId)
        return false
    end

    local blocInfo = GameInstance.player.blocManager:GetBlocInfo(blocId)
    if not blocInfo then
        logger.error("RedDotUtils->hasBlocShopDiscountShopItem: blocInfo is nil, blocId", blocId)
        return false
    end
    local blocLv = blocInfo.Level

    for lv, items in pairs(shopItemMap.map) do
        if lv <= blocLv then
            for _, v in pairs(items.list) do
                local _, soldCount = shopInfo.AlreadySellCount:TryGetValue(v.id)
                local count = v.availCount - soldCount
                local isDiscount = discountInfoMaps[v.id] ~= nil

                if isDiscount and count > 0 then
                    return true
                end
            end
        end
    end

    return false
end

function RedDotUtils.hasCheckInRewardsNotCollected()
    local activitySystem = GameInstance.player.activitySystem;

    local checkInSystem = activitySystem:GetActivity(UIConst.CHECK_IN_CONST.CBT2_CHECK_IN_ID)
    if not checkInSystem then
       return false
    end

    if checkInSystem.rewardDays >= checkInSystem.loginDays then

        return false
    end
    local maxRewardDays = #Tables.checkInRewardTable

    return checkInSystem.rewardDays < maxRewardDays
end







function RedDotUtils.hasCheckInRewardsNotCollectedInRange(args)
    if args.checkInActivity.loginDays == args.checkInActivity.rewardDays then

        return false
    end
    if args.checkInActivity.rewardDays >= args.lastDay then

        return false
    end
    if args.checkInActivity.loginDays < args.firstDay then

        return false
    end
    return true
end

function RedDotUtils.hasCraftRedDot(craftId)
    return false

end

function RedDotUtils.readRedDot(systemType, id)
    if isRead(systemType, id) then
        return
    end

    local msg = LegacyMockNet.ReadRedDot()
    msg.type = systemType
    msg.ids = {id}
    LegacyLuaNetBus.Send(msg)
end

function RedDotUtils.readRedDots(systemType, ids)
    local msg = LegacyMockNet.ReadRedDot()
    msg.type = systemType
    msg.ids = ids
    LegacyLuaNetBus.Send(msg)
end

function RedDotUtils.hasPrtsNoteRedDot(investCfg)
    for _, data in pairs(investCfg.categoryDataList) do
        for _, id in pairs(data.noteIdList) do
            if GameInstance.player.prts:IsNoteUnread(id) then
                return true
            end
        end
    end
    return false
end



function RedDotUtils.hasSettlementCanUpgradeRedDot(settlementId)
    local stlData = GameInstance.player.settlementSystem:GetUnlockSettlementData(settlementId)
    if stlData == nil then
        return false
    end

    local stlCfg = Tables.settlementBasicDataTable[settlementId]
    local stlLevelCfg = stlCfg.settlementLevelMap[stlData.level]
    local curExp = stlData.exp
    local maxExp = stlLevelCfg.levelUpExp
    local canUpgrade = maxExp ~= 0 and curExp >= maxExp
    return canUpgrade
end


function RedDotUtils.hasActivityNormalChallengeSeriesRedDot(seriesId)

    local activitySystem = GameInstance.player.activitySystem
    local isUnlock = activitySystem:IsGameEntranceSeriesUnlock(seriesId)
    if not isUnlock then
        return false
    end


    if ActivityUtils.isNewGameEntranceSeries(seriesId) then
        return true, UIConst.RED_DOT_TYPE.New
    end

    local gameMechanicsSystem = GameInstance.player.subGameSys
    local _, activityGameCfg = Tables.activityGameEntranceGameTable:TryGetValue(seriesId)
    for _, gameCfg in pairs(activityGameCfg.gameList) do
        if gameMechanicsSystem:IsGameUnlocked(gameCfg.gameId) and gameMechanicsSystem:IsGameUnread(gameCfg.gameId) then
            return true, UIConst.RED_DOT_TYPE.Normal
        end
    end
    return false
end

function RedDotUtils.hasActivityBaseMultiStageRedDot(activityId)
    local activity = GameInstance.player.activitySystem:GetActivity(activityId)
    if not activity then
        return false
    end
    if activity.completeStageList.Count ~= activity.receiveStageList.Count then
        return true,UIConst.RED_DOT_TYPE.Normal
    end

    if ActivityUtils.isNewActivity(activityId) then
        return true,UIConst.RED_DOT_TYPE.New
    end
    return false
end

function RedDotUtils.hasActivityGachaBeginnerRedDot(activityId)
    local hasRedDot, type = RedDotUtils.hasActivityBaseMultiStageRedDot(activityId)
    if hasRedDot then
        return hasRedDot, type
    end

    if RedDotUtils.hasGachaBeginnerTicketNotUesRedDot() then
        return true, UIConst.RED_DOT_TYPE.Normal
    end
    return RedDotUtils.hasGachaStarterCumulateRedDot(Tables.charGachaConst.beginnerGachaActivityPoolId)
end

function RedDotUtils.hasGachaBeginnerTicketNotUesRedDot()
    local poolId = Tables.charGachaConst.beginnerGachaActivityPoolId
    local poolCfg = Tables.gachaCharPoolTable[poolId]
    local poolTypeCfg = Tables.gachaCharPoolTypeTable[poolCfg.type]
    local gachaTenTicketId = poolTypeCfg.tenPullCostItemIds[0]
    return Utils.getItemCount(gachaTenTicketId) > 0, UIConst.RED_DOT_TYPE.Normal
end



function RedDotUtils.hasGachaRedDot()

    for poolId, poolInfo in cs_pairs(GameInstance.player.gacha.poolInfos) do
        if poolInfo.isChar then
            local has, type = RedDotUtils.hasGachaSinglePoolRedDot(poolId)
            if has then
                return has, type
            end
        end
    end
    return false
end

function RedDotUtils.hasGachaSinglePoolRedDot(poolId)

    local hasInfo, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(poolId)
    if not hasInfo or not poolInfo.isOpenValid then
        return false
    end
    if not RedDotUtils.isGachaSinglePoolRead(poolId) then
        return true, UIConst.RED_DOT_TYPE.Normal
    end

    if poolInfo.type == GEnums.CharacterGachaPoolType.Beginner then
        return RedDotUtils.hasGachaStarterRedDot(poolId)
    elseif poolInfo.type == GEnums.CharacterGachaPoolType.Special then
        return RedDotUtils.hasGachaSpecialPoolRedDot(poolId)
    elseif poolInfo.type == GEnums.CharacterGachaPoolType.Standard then
        return RedDotUtils.hasGachaStandardPoolRedDot(poolId)
    elseif poolInfo.type == GEnums.CharacterGachaPoolType.Joint then
        return RedDotUtils.hasGachaJointPoolRedDot(poolId)
    end
    return false
end

function RedDotUtils.isGachaSinglePoolRead(poolId)
    local _, isNewOpenedPoolRead, _ = ClientDataManagerInst:GetBool("IS_NEW_OPENED_POOL_READ_" .. poolId, true, false)
    return isNewOpenedPoolRead
end

function RedDotUtils.setGachaSinglePoolRead(poolId)
    if RedDotUtils.isGachaSinglePoolRead(poolId) then
        return
    end
    ClientDataManagerInst:SetBool("IS_NEW_OPENED_POOL_READ_" .. poolId, true, true)
    Notify(MessageConst.ON_GACHA_POOL_NEW_OPENED_READ, poolId)
end

function RedDotUtils.hasGachaStarterRedDot(poolId)

    local hasRedDot, type = RedDotUtils.hasGachaStarterCumulateRedDot(poolId)
    if hasRedDot then
        return hasRedDot, type
    end

    local activityCfg = Tables.activityLevelRewardsTable[Tables.charGachaConst.gachaBeginnerActivityId]
    for _, stageCfg in pairs(activityCfg.stageList) do
        local stageId = stageCfg.stageId
        hasRedDot, type = RedDotUtils.hasGachaStarterActivityStageRedDot(stageId)
        if hasRedDot then
            return hasRedDot, type
        end
    end
    return false
end

function RedDotUtils.hasGachaStarterCumulateRedDot(poolId)

    local hasInfo, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(poolId)
    if not hasInfo then
        return false
    end

    local isGet = poolInfo.roleDataMsg and poolInfo.roleDataMsg.CumulativeRewardList:Contains(0)
    if isGet then
        return false
    end
    local poolCfg = Tables.gachaCharPoolTable[poolId]
    local poolTypeCfg = Tables.gachaCharPoolTypeTable[poolCfg.type]
    if poolInfo.totalPullCountNoShare >= poolTypeCfg.cumulativeRewardsPullCount[0] then
        return true, UIConst.RED_DOT_TYPE.Normal
    end

    local gachaTenTicketId = poolTypeCfg.tenPullCostItemIds[0]
    if Utils.getItemCount(gachaTenTicketId) > 0 then
        return true, UIConst.RED_DOT_TYPE.Normal
    end
    return false
end

function RedDotUtils.hasGachaStarterActivityStageRedDot(stageId)
    local activityData = GameInstance.player.activitySystem:GetActivity(Tables.charGachaConst.gachaBeginnerActivityId)
    if not activityData then
        return false
    end
    local isRewarded = activityData.receiveStageList:Contains(stageId)
    local isComplete = activityData.completeStageList:Contains(stageId)
    if isRewarded then
        return false
    elseif isComplete then
        return true, UIConst.RED_DOT_TYPE.Normal
    end
    return false
end

function RedDotUtils.hasGachaSpecialPoolRedDot(poolId)
    local hasRedDot, redDotType = RedDotUtils.hasGachaTenLtTicketRedDot(poolId)
    if hasRedDot then
        return true, redDotType
    end
    local _, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(poolId)
    return poolInfo.freeTenPullCount > 0, UIConst.RED_DOT_TYPE.Normal
end

function RedDotUtils.hasGachaStandardPoolRedDot(poolId)
    local _, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(poolId)
    return poolInfo.choicePackCount > 0, UIConst.RED_DOT_TYPE.Normal
end

function RedDotUtils.hasGachaJointPoolRedDot(poolId)
    local hasRedDot, redDotType = RedDotUtils.hasGachaTenLtTicketRedDot(poolId)
    if hasRedDot then
        return true, redDotType
    end
    local _, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(poolId)
    return poolInfo.freeTenPullCount > 0, UIConst.RED_DOT_TYPE.Normal
end

function RedDotUtils.hasGachaTenLtTicketRedDot(poolId)
    local hasCfg, poolCfg = Tables.gachaCharPoolTable:TryGetValue(poolId)
    if not hasCfg then
        return false
    end
    if Utils.getItemCount(poolCfg.ticketGachaTenLt) > 0 then
        return true, UIConst.RED_DOT_TYPE.Normal
    end
    if Utils.getItemCount(poolCfg.ticketGachaSingleLt) >= 10 then
        return true, UIConst.RED_DOT_TYPE.Normal
    end
    return false
end



function RedDotUtils.hasGachaWeaponTenLtTicketRedDot(poolId)
    local poolCfg = Tables.GachaWeaponPoolTable[poolId]
    if string.isEmpty(poolCfg.ticketGachaTenLt) then
        return false
    end
    if Utils.getItemCount(poolCfg.ticketGachaTenLt) > 0 then
        return true, UIConst.RED_DOT_TYPE.Normal
    end
    return false
end



function RedDotUtils.hasSingleDomainDevelopmentRedDot(checkDomainId)
    local hasCfg, domainCfg = Tables.domainDataTable:TryGetValue(checkDomainId)
    if hasCfg then
        for poiType, getRedDotInfoFunc in pairs(DomainPOIUtils.GetRedDotInfoFunc) do
            local checkCanOpenFuncName = DomainPOIUtils.CheckCanOpenPOIFunc[poiType]
            if string.isEmpty(checkCanOpenFuncName) or not DomainPOIUtils[checkCanOpenFuncName] then
                logger.error("【地区发展系统红点】 checkCanOpenFunc不存在，poiType为" .. tostring(poiType))
            else
                if DomainPOIUtils[checkCanOpenFuncName](checkDomainId) then
                    local redDotInfo = DomainPOIUtils[getRedDotInfoFunc](checkDomainId)
                    local hasRedDot, redDotType = RedDotManager:GetRedDotState(redDotInfo.redDotName, redDotInfo.redDotArgs)
                    if hasRedDot then
                        return true, redDotType
                    end
                end
            end
        end


        for _, stlId in pairs(domainCfg.settlementGroup) do
            if RedDotUtils.hasSettlementCanUpgradeRedDot(stlId) then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
        end


        return GameInstance.player.domainDevelopmentSystem:HasLevelNotGerReward(checkDomainId), UIConst.RED_DOT_TYPE.Normal
    else
        logger.error("domainDataTable missing cfg, id: " .. checkDomainId)
        return false
    end
    return false
end

function RedDotUtils.hasSingleDomainGradeRedDot(checkDomainId)
    local hasCfg, domainCfg = Tables.domainDataTable:TryGetValue(checkDomainId)
    if hasCfg then

        return GameInstance.player.domainDevelopmentSystem:HasLevelNotGerReward(checkDomainId), UIConst.RED_DOT_TYPE.Normal
    else
        logger.error("domainDataTable missing cfg, id: " .. checkDomainId)
        return false
    end
    return false
end



function RedDotUtils.hasValuableDepotTabCommercialItemRedDot()


    local minExpireTs = math.maxinteger
    local needShowExpire = false
    local depotType = GEnums.ItemValuableDepotType.CommercialItem
    local mainScope = CS.Beyond.Gameplay.Scope.Create(GEnums.ScopeName.Main)

    local inventory = GameInstance.player.inventory

    local depot = inventory.valuableDepots[depotType]:GetOrFallback(mainScope)
    if depot then
        local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()

        for instId, itemBundle in pairs(depot.instItems) do
            if itemBundle.isLimitTimeItem then
                local itemCfg = Tables.itemTable[itemBundle.id]
                local ltItemCfg = Tables.lTItemTypeTable[itemCfg.type]
                local almostExpireTime = ltItemCfg.daysBeforeExpireToNotify * Const.SEC_PER_DAY
                if (itemBundle.instData.expireTs - curTime) <= almostExpireTime then

                    minExpireTs = math.min(minExpireTs, itemBundle.instData.expireTs)
                    needShowExpire = true
                end
            end
        end
    end
    if needShowExpire then
        return true, UIConst.RED_DOT_TYPE.Expire, minExpireTs
    end


    if RedDotUtils.isValuableDepotTabHasNewObtainedImportantItem(GEnums.ItemValuableDepotType.CommercialItem) then
        return true, UIConst.RED_DOT_TYPE.Normal
    end

    return false
end



function RedDotUtils.isValuableDepotTabHasNewObtainedImportantItem(type)
    if not GameInstance.player.inventory.valuableDepots:TryGetValue(type) then
        return false
    end

    local commercialItemList = GameInstance.player.inventory.valuableDepots[type]:GetOrFallback(Utils.getCurrentScope())
    for id, _ in cs_pairs(commercialItemList.normalItems) do
        local success, redDotType = RedDotManager:GetRedDotState("ValuableDepotItem", id)

        if success and redDotType == UIConst.RED_DOT_TYPE.Normal then
            return true, UIConst.RED_DOT_TYPE.Normal
        end
    end
    for instId, bundle in cs_pairs(commercialItemList.instItems) do
        local hasItemData, itemData = Tables.itemTable:TryGetValue(bundle.id)
        if hasItemData and itemData.valuableDepotRedDot and GameInstance.player.inventory:IsNewItem(bundle.id, instId) then
            return true, UIConst.RED_DOT_TYPE.Normal
        end
    end

    return false
end

local newObtainedImportantValuableDepotItemText = "NewObtainedImportantValuableDepotItem"
function RedDotUtils.isNewObtainedImportantValuableDepotItem(id)
    local suc, value = ClientDataManagerInst:GetBool(newObtainedImportantValuableDepotItemText .. id,false)
    return suc and value
end

function RedDotUtils.setNewObtainedImportantValuableDepotItem(id, value)
    ClientDataManagerInst:SetBool(newObtainedImportantValuableDepotItemText .. id, value, false, EClientDataTimeValidType.Permanent)
    Notify(MessageConst.ON_VALUABLE_DEPOT_IMPORT_ITEM_CHANGED)
end



function RedDotUtils.hasContingencyContractTagHasRedDot(activityId, stageId, tagId)
    if string.isEmpty(stageId) then
        return false
    end

    local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
    if not activityData then
        return false
    end

    local isUnlock = ActivityUtils.isStageUnlockMultiConditionStageActivity(activityData, stageId)
    if not isUnlock then
        return false
    end

    local isRead = ActivityUtils.isCcTagRead(tagId)
    if isRead then
        return false
    end

    return true, UIConst.RED_DOT_TYPE.Normal
end



local newObtainedMusicText = "NewObtainedMusic"
function RedDotUtils.checkMusicIsRead(musicId)
    if string.isEmpty(musicId) then
        return false
    end

    local _, value = ClientDataManagerInst:GetBool(newObtainedMusicText .. musicId, false, false)
    return value
end

function RedDotUtils.setMusicRead(musicId)
    ClientDataManagerInst:SetBool(newObtainedMusicText .. musicId, true, false)
end



local decoBuildingNew = "DecoBuildingNew-"
function RedDotUtils.checkDecoBuildingNewIsRead(itemId)
    local _, value = ClientDataManagerInst:GetBool(decoBuildingNew .. itemId, false, false)
    return value
end

function RedDotUtils.setDecoBuildingNewRead(itemId)
    if RedDotUtils.checkDecoBuildingNewIsRead(itemId) then
        return
    end
    ClientDataManagerInst:SetBool(decoBuildingNew .. itemId, true, false)
end

local decoBuildingFirstGet = "DecoBuildingFirstGet-"
function RedDotUtils.checkDecoBuildingFirstGetIsRead(itemId)
    local _, value = ClientDataManagerInst:GetBool(decoBuildingFirstGet .. itemId, false, false)
    return value
end

function RedDotUtils.setDecoBuildingFirstGetRead(itemId)
    if RedDotUtils.checkDecoBuildingFirstGetIsRead(itemId) then
        return
    end
    ClientDataManagerInst:SetBool(decoBuildingFirstGet .. itemId, true, false)
end



_G.RedDotUtils = RedDotUtils
return RedDotUtils
