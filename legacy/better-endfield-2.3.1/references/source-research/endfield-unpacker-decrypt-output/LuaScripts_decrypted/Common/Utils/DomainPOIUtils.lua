local DomainPOIUtils = {}






DomainPOIUtils.GetRedDotInfoFunc = {
    [GEnums.DomainPoiType.DomainDepot] = "getRedDotInfoDomainDepot",
    [GEnums.DomainPoiType.KiteStation] = "getKiteStationRedDotInfo",
}



function DomainPOIUtils.getRedDotInfoDomainDepot(domainId)
    return {
        redDotName = "DomainDepot",
        redDotArgs = domainId,
    }
end

function DomainPOIUtils.getKiteStationRedDotInfo(domainId)

    return {
        redDotName = "KiteStationCollectionReward",
        redDotArgs = "",
    }
end




DomainPOIUtils.CheckCanOpenPOIFunc = {
    [GEnums.DomainPoiType.DomainShop] = "checkCanOpenDomainShop",
    [GEnums.DomainPoiType.DomainDepot] = "checkCanOpenDomainDepot",
    [GEnums.DomainPoiType.KiteStation] = "checkCanOpenKiteStation",
}



function DomainPOIUtils.checkCanOpenDomainShop(domainId)
    local _, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
    local shopGroupId = domainCfg.domainShopGroupId
    local shopGroupData = GameInstance.player.shopSystem:GetShopGroupData(shopGroupId)
    return shopGroupData.domainChannelData and shopGroupData.domainChannelData.channelLevelMap.Count > 0
end



function DomainPOIUtils.checkCanOpenDomainDepot(domainId)
    for domainDepotId, domainDepotCfg in pairs(Tables.domainDepotTable) do
        if domainDepotCfg.domainId == domainId then
            local domainDepotData = GameInstance.player.domainDepotSystem:GetDomainDepotDataById(domainDepotId)
            if domainDepotData and domainDepotData.level > 0 then
                return true
            end
        end
    end
    return false
end



function DomainPOIUtils.checkCanOpenKiteStation(domainId)
    for kiteStationId, kiteStationCfg in pairs(Tables.kiteStationLevelTable) do
        if kiteStationCfg.domainId == domainId then
            local kiteStationData = GameInstance.player.kiteStationSystem:GetKiteStationDataByInstId(kiteStationId)
            if kiteStationData and kiteStationData.level > 0 then
                return true
            end
        end
    end
    return false
end


























function DomainPOIUtils.getUpgradeCtrlArgsTemplate()

    return {

        domainId = "",
        levelId = "",
        titleName = "",
        descList = {},

        upgradeQuestId = "",
        upgradeQuestDesc = "",
        upgradeCostMoney = 0,
        jumpTaskToast = "",

        curLevel = 0,
        targetLevel = 0,
        maxLevel = 0,
        isFinalMaxLevel = false,

        contentInfoList = {}
    }
end




DomainPOIUtils.GetPoiUpgradeCtrlInfo = {
    [GEnums.DomainPoiType.DomainShop] = function(channelId, needContentInfo)
        local info = DomainPOIUtils.getUpgradeCtrlArgsTemplate()

        local _, shopChannelCfg = Tables.shopChannelDevelopmentTable:TryGetValue(channelId)
        local shopGroupId = shopChannelCfg.shopGroupId
        local _, shopGroupDomainCfg = Tables.shopGroupDomainTable:TryGetValue(shopGroupId)
        local domainId = shopGroupDomainCfg.domainId
        local _, domainCfg = Tables.domainDataTable:TryGetValue(domainId)

        local _, shopGroupCfg = Tables.shopGroupTable:TryGetValue(shopGroupId)
        local commonShopId, randomShopId
        for _, shopId in pairs(shopGroupCfg.shopIds) do
            local _, shopCfg = Tables.shopTable:TryGetValue(shopId)
            if shopCfg.shopRefreshType == GEnums.ShopRefreshType.None then
                commonShopId = shopId
            elseif shopCfg.shopRefreshType == GEnums.ShopRefreshType.RefreshRandom then
                randomShopId = shopId
            end
        end
        local _, commonShopCfg = Tables.shopTable:TryGetValue(commonShopId)
        local _, randomShopCfg = Tables.shopTable:TryGetValue(randomShopId)

        local shopSystem = GameInstance.player.shopSystem
        local shopGroupData = shopSystem:GetShopGroupData(shopGroupId)
        local channelData = shopGroupData.domainChannelData
        local hasValue, curLv = channelData.channelLevelMap:TryGetValue(channelId)
        if not hasValue then
            curLv = 0
        end
        local nextLv = curLv + 1
        local maxLv = DomainPOIUtils.GetDomainShopChannelMaxLevel(channelId)

        local _, nextLvChannelCfg = shopChannelCfg.channelLevelMap:TryGetValue(nextLv)
        local costMoney = 0
        if nextLv <= maxLv and nextLvChannelCfg then
            for i = 0, #nextLvChannelCfg.costItemIdList - 1 do
                if nextLvChannelCfg.costItemIdList[i] == domainCfg.domainGoldItemId then
                    costMoney = nextLvChannelCfg.costItemNumList[i]
                    break
                end
            end
        end

        info.domainId = domainId
        info.levelId = shopChannelCfg.levelId
        info.titleName = shopChannelCfg.channelName
        local _, curLvChannelCfg = shopChannelCfg.channelLevelMap:TryGetValue(curLv)
        if curLvChannelCfg and not string.isEmpty(curLvChannelCfg.channelDesc) then
            table.insert(info.descList, curLvChannelCfg.channelDesc)
        end
        if nextLvChannelCfg and not string.isEmpty(nextLvChannelCfg.upgradeDesc) and curLv < maxLv then
            table.insert(info.descList, nextLvChannelCfg.upgradeDesc)
        end

        local canSkipPreconditions = DomainPOIUtils._CanSkipDomainShopChannelPreconditions(shopChannelCfg)
        info.upgradeQuestId = canSkipPreconditions and "" or Tables.shopDomainConst.domainShopUnlockQuestId
        info.upgradeQuestDesc = Language.LUA_DOMAIN_SHOP_UNLOCK_QUEST_DESC
        info.upgradeCostMoney = costMoney

        info.curLevel = curLv
        info.targetLevel = nextLv
        info.maxLevel = maxLv
        if curLvChannelCfg then
            info.isFinalMaxLevel = curLvChannelCfg.isFinalMaxLevel
        else
            info.isFinalMaxLevel = false
        end

        info.domainShopChannelId = channelId
        info.domainShopGroupId = shopGroupId
        if not needContentInfo then
            return info
        end


        if info.curLevel >= info.maxLevel then
            return info
        end

        local randomGoodsUnlockLv = shopSystem:GetChannelUnlockRandomGoodsLevel(randomShopId, channelId)

        local newlyCommonGoods = {}
        local newlyRandomGoods = {}
        for i = 0, #nextLvChannelCfg.newGoodsList - 1 do
            local newGoodsId = nextLvChannelCfg.newGoodsList[i]

            local _, goodsCfg = Tables.shopGoodsTable:TryGetValue(newGoodsId)
            local itemBundleCfg = UIUtils.getRewardFirstItem(goodsCfg.rewardId)
            local itemBundle = {
                id = itemBundleCfg.id,
            }
            if lume.find(commonShopCfg.shopGoodsIds, newGoodsId) then
                table.insert(newlyCommonGoods, itemBundle)
            else
                table.insert(newlyRandomGoods, itemBundle)
            end
        end

        if #newlyCommonGoods > 0 then
            DomainPOIUtils.insertContentCommonTitle(
                info,
                UIConst.UI_SPRITE_SHOP_TRADE_MARKET_ICON_SMALL .. "/" .. commonShopCfg.iconId,
                commonShopCfg.shopName
            )
            DomainPOIUtils.insertContentItemList(info, nil, newlyCommonGoods)
        end


        local needShowNewLimit = false
        if #newlyRandomGoods > 0 then
            local randomShopData = shopSystem:GetShopData(randomShopId)
            local curRandomShopGoodsCount = randomShopData.goodList.Count
            local curDomainRandomShopIsUnlock = shopSystem:CheckShopUnlocked(randomShopId) and curRandomShopGoodsCount > 0
            if not curDomainRandomShopIsUnlock and nextLv == randomGoodsUnlockLv then


                DomainPOIUtils.insertContentCommonTitle(
                    info,
                    UIConst.UI_SPRITE_SHOP_TRADE_MARKET_ICON_SMALL .. "/" .. randomShopCfg.iconId,
                    randomShopCfg.shopName
                )
                DomainPOIUtils.insertContentTextImgText(info, Language.LUA_DOMAIN_SHOP_UPGRADE_RANDOM_SHOP_OPEN_TITLE, {
                    {
                        text2 = Language.LUA_DOMAIN_SHOP_UPGRADE_RANDOM_SHOP_OPEN_DESC,
                        fontSizeLevel = 2,
                    }
                }, 2)
                DomainPOIUtils.insertContentItemList(info, nil, newlyRandomGoods)
            elseif curDomainRandomShopIsUnlock then

                DomainPOIUtils.insertContentCommonTitle(
                    info,
                    UIConst.UI_SPRITE_SHOP_TRADE_MARKET_ICON_SMALL .. "/" .. randomShopCfg.iconId,
                    randomShopCfg.shopName
                )
                DomainPOIUtils.insertContentItemList(info, nil, newlyRandomGoods)
                needShowNewLimit = true
            end
        else

            needShowNewLimit = true
            local isAddLimit = nextLvChannelCfg.randGoodsBaseLimitUp > 0
            local isAddLimitUp = nextLvChannelCfg.randGoodsDailyAddLimitUp > 0
            if isAddLimit or isAddLimitUp then
                DomainPOIUtils.insertContentCommonTitle(
                    info,
                    UIConst.UI_SPRITE_SHOP_TRADE_MARKET_ICON_SMALL .. "/" .. randomShopCfg.iconId,
                    randomShopCfg.shopName
                )
            end
        end

        if needShowNewLimit then
            local curBaseLimit = channelData.buyRandomGoodsLimitCount
            local curDailyAddLimit = channelData.buyRandomGoodsLimitUpCount
            local iconPath = UIConst.UI_SPRITE_COMMON .. "/deco_common_arrow"
            local isAddLimit = nextLvChannelCfg.randGoodsBaseLimitUp > 0
            local isAddLimitUp = nextLvChannelCfg.randGoodsDailyAddLimitUp > 0
            if isAddLimit then
                local newlyBaseLimit = nextLvChannelCfg.randGoodsBaseLimitUp + curBaseLimit
                DomainPOIUtils.insertContentTextImgText(info, Language.LUA_DOMAIN_SHOP_UPGRADE_BASE_LIMIT, {
                    {
                        text1 = curBaseLimit,
                        icon = iconPath,
                        text2 = newlyBaseLimit,
                    }
                }, 2)
            end
            if isAddLimitUp then
                local newlyDailyAddLimit = nextLvChannelCfg.randGoodsDailyAddLimitUp + curDailyAddLimit
                DomainPOIUtils.insertContentTextImgText(info, Language.LUA_DOMAIN_SHOP_UPGRADE_ADD_LIMIT, {
                    {
                        text1 = curDailyAddLimit,
                        icon = iconPath,
                        text2 = newlyDailyAddLimit,
                    }
                }, 2)
            end
        end


        return info
    end,
}



DomainPOIUtils.contentTypeEnum = {
    CommonTitle = 1,
    ItemList = 2,
    TextImgText = 3,
    RewardList = 4,
    TitleWithText = 5,
    SewageTreatInfo = 6,
}

function DomainPOIUtils.insertContentCommonTitle(upgradeCtrlArgs, titleIcon, titleName, isNew)
    local info = {
        contentType = DomainPOIUtils.contentTypeEnum.CommonTitle,
        icon = titleIcon,
        titleName = titleName,
        isNew = isNew,
    }
    table.insert(upgradeCtrlArgs.contentInfoList, info)
end

function DomainPOIUtils.insertContentItemList(upgradeCtrlArgs, title, itemBundleList)
    local info = {
        contentType = DomainPOIUtils.contentTypeEnum.ItemList,
        title = title,
        itemBundleList = itemBundleList,
    }
    if itemBundleList ~= nil and #itemBundleList > 0 then
        info.useNaviGroup = true
    end
    table.insert(upgradeCtrlArgs.contentInfoList, info)
end



function DomainPOIUtils.insertContentTextImgText(upgradeCtrlArgs, title, contentList, indentLevel, noDeco)
    local info = {
        contentType = DomainPOIUtils.contentTypeEnum.TextImgText,
        title = title,
        contentList = contentList,
        indentLevel = indentLevel,
        noDeco = noDeco,
    }
    table.insert(upgradeCtrlArgs.contentInfoList, info)
end

function DomainPOIUtils.insertContentRewardList(upgradeCtrlArgs, itemBundleList, tagStateName, hasArrow)
    local info = {
        contentType = DomainPOIUtils.contentTypeEnum.RewardList,
        itemBundleList = itemBundleList,
        tagStateName = tagStateName,
        hasArrow = hasArrow,
    }
    if itemBundleList ~= nil and #itemBundleList > 0 then
        info.useNaviGroup = true
    end
    table.insert(upgradeCtrlArgs.contentInfoList, info)
end

function DomainPOIUtils.insertContentTitleWithText(upgradeCtrlArgs, title, contentText)
    local info = {
        contentType = DomainPOIUtils.contentTypeEnum.TitleWithText,
        title = title,
        contentText = contentText,
    }
    table.insert(upgradeCtrlArgs.contentInfoList, info)
end

function DomainPOIUtils.insertContentSewageTreatInfo(upgradeCtrlArgs, isImporter, isMaxLv, currCount, nextCount)
    local info = {
        contentType = DomainPOIUtils.contentTypeEnum.SewageTreatInfo,
        isImporter = isImporter,
        isMaxLv = isMaxLv,
        currCount = currCount,
        nextCount = nextCount,
    }
    table.insert(upgradeCtrlArgs.contentInfoList, info)
end






function DomainPOIUtils.GetDomainMaxLevel(domainId)
    local hasCfg, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
    if not hasCfg then
        logger.error("domain表数据缺失！domain id：", domainId)
        return 0
    end
    return DomainPOIUtils._CommonGetListMaxLevelFunc(domainCfg.domainDevelopmentLevel)
end

function DomainPOIUtils.GetKiteStationMaxLevel(kiteStationId)
    return GameInstance.player.kiteStationSystem:GetKiteStationMaxLevel(kiteStationId)
end

function DomainPOIUtils.GetSettlementMaxLevel(stlId)
    local _, stlCfg = Tables.settlementBasicDataTable:TryGetValue(stlId)
    if not stlCfg then
        logger.error("据点表数据缺失！据点id：", stlId)
        return 0
    end
    return DomainPOIUtils._CommonGetMapMaxLevelFunc(stlCfg.settlementLevelMap)
end

function DomainPOIUtils.GetDomainShopChannelMaxLevel(channelId)
    local _, shopChannelCfg = Tables.shopChannelDevelopmentTable:TryGetValue(channelId)
    if not shopChannelCfg then
        logger.error("地区商店渠道表数据缺失！渠道id：", channelId)
        return 0
    end
    return DomainPOIUtils._CommonGetMapMaxLevelFunc(shopChannelCfg.channelLevelMap)
end

function DomainPOIUtils.GetDomainDepotMaxLevel(depotId)
    local success, levelCfg = Tables.domainDepotLevelTable:TryGetValue(depotId)
    if not success then
        logger.error("地区仓库等级表数据缺失！仓库id：", depotId)
        return 0
    end
    return DomainPOIUtils._CommonGetMapMaxLevelFunc(levelCfg.levelList)
end



function DomainPOIUtils._CommonGetListMaxLevelFunc(levelCfgList)
    local nowMaxLv = 0
    local listLength = levelCfgList.Count
    for i = listLength, 1, -1 do
        local levelCfg = levelCfgList[CSIndex(i)]
        if Utils.isCurTimeInTimeIdRange(levelCfg.timeId, true) then
            nowMaxLv = listLength
            break
        end
    end
    return nowMaxLv
end


function DomainPOIUtils._CommonGetMapMaxLevelFunc(levelCfgMap)
    local maxLv = 0
    for level, levelCfg in pairs(levelCfgMap) do
        if Utils.isCurTimeInTimeIdRange(levelCfg.timeId, true) then
            maxLv = math.max(maxLv, level)
        end
    end
    return maxLv
end












DomainPOIUtils.GetPOILevelNewVersionInfoFunc = {
    [GEnums.DomainPoiType.Settlement] = "getPOILevelNewVersionInfo_Settlement",
    [GEnums.DomainPoiType.DomainShop] = "getPOILevelNewVersionInfo_DomainShop",
    [GEnums.DomainPoiType.KiteStation] = "getPOILevelNewVersionInfo_KiteStation",
    [GEnums.DomainPoiType.DomainDepot] = "getPOILevelNewVersionInfo_DomainDepot",
}














function DomainPOIUtils.getPOILevelNewVersionInfo_Settlement(levelId, startVersionData, endVersionData)
    local _, stlMapCfg = Tables.settlementLevelPOIMapTable:TryGetValue(levelId)
    if not stlMapCfg then
        return nil
    end
    local stlId = stlMapCfg.settlementId

    local settlementSystem = GameInstance.player.settlementSystem

    local stlData = settlementSystem:GetUnlockSettlementData(stlId)
    if not stlData then
        return nil
    end
    local _, stlCfg = Tables.settlementBasicDataTable:TryGetValue(stlId)
    if not stlCfg then
        logger.error("据点数据缺失，据点id：", stlId)
    end

    local curMaxLv = DomainPOIUtils.GetSettlementMaxLevel(stlId)
    if curMaxLv <= 0 then
        return nil
    end
    local hasMaxLvDiff = false
    for checkLv = curMaxLv, 1, -1 do
        local checkVersion = stlCfg.settlementLevelMap[checkLv].versionStart
        local checkVersionData = DomainPOIUtils._GetVersionData(checkVersion)
        if checkVersionData.versionValue < startVersionData.versionValue then
            break
        elseif checkVersionData.versionValue <= endVersionData.versionValue then
            curMaxLv = checkLv
            hasMaxLvDiff = true
            break
        end
    end

    local rewardList = {}
    local itemIdSet = {}
    local curStlLv = stlData.level
    for checkLv = 1, curStlLv do
        local itemMap = stlCfg.settlementLevelMap[checkLv].settlementTradeItemMap
        for _, stlItemCfg in pairs(itemMap) do
            if string.isEmpty(stlItemCfg.activityId) then
                local checkVersionData = DomainPOIUtils._GetVersionData(stlItemCfg.versionStart)
                local canShow = startVersionData.versionValue <= checkVersionData.versionValue and checkVersionData.versionValue <= endVersionData.versionValue
                if canShow then
                    local itemId = stlItemCfg.itemId
                    local _, itemCfg = Tables.itemTable:TryGetValue(itemId)
                    if not itemIdSet[itemId] and itemCfg then
                        itemIdSet[itemId] = true
                        table.insert(rewardList, {
                            id = itemId,
                            count = 1,

                            rarity = itemCfg.rarity,
                            sortId1 = itemCfg.sortId1,
                            sortId2 = itemCfg.sortId2,
                        })
                    end
                end
            end
        end
    end
    table.sort(rewardList, Utils.genSortFunction({ "rarity", "sortId1", "sortId2", "id" }))

    if #rewardList <= 0 and not hasMaxLvDiff then
        return nil
    end

    local info = {
        levelPoiName = stlCfg.settlementName,
        poiCurVersionMaxLv = hasMaxLvDiff and curMaxLv or -1,
        rewardList = rewardList
    }
    return info
end

function DomainPOIUtils.getPOILevelNewVersionInfo_DomainShop(levelId, startVersionData, endVersionData)
    local _, channelMapCfg = Tables.shopChannelLevelPOIMapTable:TryGetValue(levelId)
    if not channelMapCfg then
        return nil
    end
    local channelId = channelMapCfg.channelPartner
    local _, channelCfg = Tables.shopChannelDevelopmentTable:TryGetValue(channelId)
    if not channelCfg then
        return nil
    end
    local shopGroupId = channelCfg.shopGroupId

    local shopGroupData = GameInstance.player.shopSystem:GetShopGroupData(shopGroupId)
    if not shopGroupData.domainChannelData then
        return nil
    end
    local hasValue, curLevel = shopGroupData.domainChannelData.channelLevelMap:TryGetValue(channelId)
    if not hasValue or curLevel <= 0 then
        return nil
    end

    local curMaxLv = DomainPOIUtils.GetDomainShopChannelMaxLevel(channelId)
    local hasMaxLvDiff = false
    for checkLv = curMaxLv, 1, -1 do
        local checkVersion = channelCfg.channelLevelMap[checkLv].versionStart
        local checkVersionData = DomainPOIUtils._GetVersionData(checkVersion)
        if checkVersionData.versionValue < startVersionData.versionValue then
            break
        elseif checkVersionData.versionValue <= endVersionData.versionValue then
            curMaxLv = checkLv
            hasMaxLvDiff = true
            break
        end
    end
    if not hasMaxLvDiff then
        return nil
    end

    local info = {
        levelPoiName = channelCfg.channelName,
        poiCurVersionMaxLv = curMaxLv,
    }
    return info
end

function DomainPOIUtils.getPOILevelNewVersionInfo_KiteStation(levelId, startVersionData, endVersionData)
    local kiteStationLevelCfg
    for _, cfg in pairs(Tables.kiteStationLevelTable) do
        if cfg.levelId == levelId then
            kiteStationLevelCfg = cfg
            break
        end
    end
    if not kiteStationLevelCfg then
        return nil
    end
    if GameInstance.player.kiteStationSystem:IsKiteStationUnlocked(kiteStationLevelCfg.kiteStation) == false then
        return nil
    end

    local curMaxLv = DomainPOIUtils.GetKiteStationMaxLevel(kiteStationLevelCfg.kiteStation)
    local hasMaxLvDiff = false
    for checkLv = curMaxLv, 1, -1 do
        local checkVersion = kiteStationLevelCfg.list[checkLv].versionStart
        local checkVersionData = DomainPOIUtils._GetVersionData(checkVersion)
        if checkVersionData.versionValue < startVersionData.versionValue then
            break
        elseif checkVersionData.versionValue <= endVersionData.versionValue then
            curMaxLv = checkLv
            hasMaxLvDiff = true
            break
        end
    end
    if not hasMaxLvDiff then
        return nil
    end

    local info = {
        levelPoiName = kiteStationLevelCfg.list[curMaxLv].name,
        poiCurVersionMaxLv = curMaxLv,
    }
    return info
end

function DomainPOIUtils.getPOILevelNewVersionInfo_DomainDepot(levelId, startVersionData, endVersionData)
    local levelDepotId, levelDepotName
    for depotId, depotCfg in pairs(Tables.domainDepotTable) do
        if depotCfg.refLevelId == levelId then
            levelDepotId = depotId
            levelDepotName = depotCfg.depotName
            break
        end
    end
    if not levelDepotId then
        return nil
    end
    local levelSuccess, domainDepotLevelList = Tables.domainDepotLevelTable:TryGetValue(levelDepotId)
    if not levelSuccess then
        return nil
    end
    local depotInfo = DomainDepotUtils.GetDepotInfo(levelDepotId)
    if depotInfo == nil or depotInfo.currLevel == 0 then
        return nil
    end
    domainDepotLevelList = domainDepotLevelList.levelList
    local curMaxLv = DomainPOIUtils.GetDomainDepotMaxLevel(levelDepotId)
    local hasMaxLvDiff = false
    for checkLv = curMaxLv, 1, -1 do
        local checkVersion = domainDepotLevelList[checkLv].versionStart
        local checkVersionData = DomainPOIUtils._GetVersionData(checkVersion)
        if checkVersionData.versionValue < startVersionData.versionValue then
            break
        elseif checkVersionData.versionValue <= endVersionData.versionValue then
            curMaxLv = checkLv
            hasMaxLvDiff = true
            break
        end
    end
    if not hasMaxLvDiff then
        return nil
    end
    return {
        levelPoiName = levelDepotName,
        poiCurVersionMaxLv = curMaxLv,
    }
end







function DomainPOIUtils.DomainMaxLevelHasVersionDiff(domainId, gmForceRecordVersion, gmForceNowVersion)

    local hasVersion, recordVersion = GameInstance.player.domainDevelopmentSystem:TryReadVersion(
        domainId,
        CS.Proto.DOMAIN_DEVELOPMENT_READ_VERSION_TYPE.DomainDevSystem
    )

    if gmForceRecordVersion then
        hasVersion = true
        recordVersion = gmForceRecordVersion
    end

    if not hasVersion then
        return false
    end
    local recordVersionData = DomainPOIUtils._GetVersionData(recordVersion)

    local hasCfg, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
    if not hasCfg then
        logger.error("domain表数据缺失！domain id：", domainId)
        return false
    end
    local nowMaxLv = DomainPOIUtils.GetDomainMaxLevel(domainId)
    local nowMaxLvCfg = domainCfg.domainDevelopmentLevel[CSIndex(nowMaxLv)]
    local endVersion = nowMaxLvCfg.versionStart

    if gmForceNowVersion then
        endVersion = gmForceNowVersion
    end

    local endVersionData = DomainPOIUtils._GetVersionData(endVersion)

    if not gmForceNowVersion then
        if endVersionData.major ~= CS.Beyond.GlobalOptions.instance.branchVersionMajor or
            endVersionData.minor ~= CS.Beyond.GlobalOptions.instance.branchVersionMinor
        then
            return false
        end
    end



    if recordVersionData.versionValue >= endVersionData.versionValue then
        return false
    end

    local startVersionData = recordVersionData
    if startVersionData.major ~= endVersionData.major or startVersionData.minor ~= endVersionData.minor then

        startVersionData.major = endVersionData.major
        startVersionData.minor = endVersionData.minor
        startVersionData.phase = 0
    else

        startVersionData.phase = startVersionData.phase + 1
    end
    DomainPOIUtils._CalculateVersionValue(startVersionData)

    return startVersionData, endVersionData

end



function DomainPOIUtils._GetVersionData(versionStr)
    local versionData = {
        isValid = false,
        major = 1,
        minor = 0,
        phase = 0,
        versionValue = 0,
    }
    if type(versionStr) == "string" and not string.isEmpty(versionStr) then
        local major, minor, phase = string.match(versionStr, "^v(%d+)d(%d+)d(%d+)$")
        if major == nil then
            major, minor = string.match(versionStr, "^v(%d+)d(%d+)$")
            phase = "0"
        end
        if major ~= nil and minor ~= nil and phase ~= nil then
            versionData.major = tonumber(major) or versionData.major
            versionData.minor = tonumber(minor) or versionData.minor
            versionData.phase = tonumber(phase) or versionData.phase
            versionData.isValid = true
        end
    end
    DomainPOIUtils._CalculateVersionValue(versionData)
    return versionData
end

function DomainPOIUtils._CalculateVersionValue(versionData)
    versionData.versionValue = versionData.major * 10000 + versionData.minor * 100 + versionData.phase
end

function DomainPOIUtils.tryGetDomainNewVersionInfo(domainId, startVersionData, endVersionData)
    local hasCfg, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
    if not hasCfg then
        logger.error("domain表数据缺失！domain id：", domainId)
        return nil
    end
    local curMaxLv = DomainPOIUtils.GetDomainMaxLevel(domainId)
    for checkLv = curMaxLv, 1, -1 do
        local checkVersion = domainCfg.domainDevelopmentLevel[CSIndex(checkLv)].versionStart
        local checkVersionData = DomainPOIUtils._GetVersionData(checkVersion)
        if checkVersionData.versionValue < startVersionData.versionValue then
            break
        elseif checkVersionData.versionValue <= endVersionData.versionValue then
            curMaxLv = checkLv
            break
        end
    end

    local domainVersionDiffInfo = {
        domainId = domainId,
        domainIcon = domainCfg.domainIcon,
        domainName = domainCfg.domainName,

        startVersionData = startVersionData,
        endVersionData = endVersionData,
        domainCurMaxLv = curMaxLv,
        poiVersionInfoList = {},
    }

    local poiVersionInfoList = domainVersionDiffInfo.poiVersionInfoList

    local poiVersionInfo = DomainPOIUtils.tryGetPOINewVersionInfo(GEnums.DomainPoiType.Settlement, domainCfg, startVersionData, endVersionData)
    if poiVersionInfo then
        table.insert(poiVersionInfoList, poiVersionInfo)
    end

    for _, poiType in pairs(domainCfg.domainPoiTypeGroup) do
        poiVersionInfo = DomainPOIUtils.tryGetPOINewVersionInfo(poiType, domainCfg, startVersionData, endVersionData)
        if poiVersionInfo then
            table.insert(poiVersionInfoList, poiVersionInfo)
        end
    end
    return domainVersionDiffInfo
end

function DomainPOIUtils.tryGetPOINewVersionInfo(poiType, domainCfg, startVersionData, endVersionData)

    local hasCfg, poiCfg = Tables.domainPoiTable:TryGetValue(poiType)
    if not hasCfg then
        return nil
    end

    local levelVersionInfoList = {}
    for _, levelId in pairs(domainCfg.levelGroup) do
        local levelName = Tables.levelDescTable[levelId].showName
        local funcName = DomainPOIUtils.GetPOILevelNewVersionInfoFunc[poiType]
        if not string.isEmpty(funcName) then
            local levelVersionInfo = DomainPOIUtils[funcName](levelId, startVersionData, endVersionData)
            if levelVersionInfo then
                levelVersionInfo.levelName = levelName
                table.insert(levelVersionInfoList, levelVersionInfo)
            end
        end
    end

    if #levelVersionInfoList > 0 then

        local poiVersionInfo = {
            poiType = poiType,
            poiName = poiCfg.name,
            poiIcon = poiCfg.noBackgroundIcon,
            levelVersionInfoList = levelVersionInfoList,
        }
        return poiVersionInfo
    else
        return nil
    end
end







DomainPOIUtils.GetPOIRemindInfoFunc = {
    [GEnums.DomainPoiType.Settlement] = "getPOIRemindInfo_Settlement",
    [GEnums.DomainPoiType.DomainShop] = "getPOIRemindInfo_DomainShop",
    [GEnums.DomainPoiType.RecycleBin] = "getPOIRemindInfo_RecycleBin",
    [GEnums.DomainPoiType.SimulationTraining] = "getPOIRemindInfo_SimulationTraining",
    [GEnums.DomainPoiType.DomainDepot] = "getPOIRemindInfo_DomainDepot",
    [GEnums.DomainPoiType.SewageTreatPlant] = "getPOIRemindInfo_SewageTreatPlant",
    [GEnums.DomainPoiType.KiteStation] = "getPOIRemindInfo_KiteStation",
}

function DomainPOIUtils.getRemindInfoTemplate()

    return {

        poiId = "",
        poiName = "",
        poiSerial = 0,
        curLevel = 0,
        maxLevel = 1,
        isFinalMaxLv = false,
        mapMarkType = nil,
        jumpPhaseId = nil,

        upgradeCostMoney = 0,
        upgradeMissionId = "",
        upgradeQuestId = "",
        isBlockUpgrade = false,
        blockUpgradeDesc = "",
        needWarning = false,
        stateDesc = "",
    }
end

function DomainPOIUtils.getPOIRemindInfo_Settlement(levelId)
    local _, stlMapCfg = Tables.settlementLevelPOIMapTable:TryGetValue(levelId)
    if not stlMapCfg then
        return nil
    end

    local stlId = stlMapCfg.settlementId

    local stlData = GameInstance.player.settlementSystem:GetUnlockSettlementData(stlId)
    if not stlData or stlData.level <= 0 then
        return nil
    end

    local info = DomainPOIUtils.getRemindInfoTemplate()
    local stlCfg = Tables.settlementBasicDataTable[stlId]
    local maxLv = DomainPOIUtils.GetSettlementMaxLevel(stlId)
    local stlMaxLvCfg = stlCfg.settlementLevelMap[maxLv]
    local stlCurLvCfg = stlCfg.settlementLevelMap[stlData.level]
    info.poiId = stlId
    info.poiName = stlCfg.settlementName
    info.curLevel = stlData.level
    info.maxLevel = maxLv
    info.isFinalMaxLv = stlMaxLvCfg.isFinalMaxLevel
    info.mapMarkType = GEnums.MarkType.Settlement
    info.jumpPhaseId = PhaseId.SettlementMain

    info.upgradeMissionId = stlCurLvCfg.upgradeMissionId
    if stlData.exp < stlData.maxExp then
        info.isBlockUpgrade = true
        info.blockUpgradeDesc = Language.LUA_DOMAIN_POI_OVERVIEW_SETTLEMENT_WAIT_EXP
    end
    if stlData.remainMoney >= stlCurLvCfg.moneyMax then
        info.needWarning = true
        info.stateDesc = Language.LUA_DOMAIN_POI_OVERVIEW_SETTLEMENT_MAX_MONEY
    else
        info.stateDesc = Language.LUA_DOMAIN_POI_OVERVIEW_SETTLEMENT_WAIT_MONEY
    end

    return { info }
end

function DomainPOIUtils.getPOIRemindInfo_DomainShop(levelId)
    local _, channelMapCfg = Tables.shopChannelLevelPOIMapTable:TryGetValue(levelId)
    if not channelMapCfg then
        return nil
    end


    local shopSys = GameInstance.player.shopSystem
    local channelId = channelMapCfg.channelPartner
    local _, shopChannelCfg = Tables.shopChannelDevelopmentTable:TryGetValue(channelId)
    local _, shopGroupCfg = Tables.shopGroupTable:TryGetValue(shopChannelCfg.shopGroupId)
    local shopGroupData = shopSys:GetShopGroupData(shopChannelCfg.shopGroupId)
    local randomShopId = ""
    for _, shopId in pairs(shopGroupCfg.shopIds) do
        local _, shopCfg = Tables.shopTable:TryGetValue(shopId)
        if shopCfg.shopRefreshType == GEnums.ShopRefreshType.RefreshRandom then
            randomShopId = shopId
        end
    end
    local channelData = shopGroupData.domainChannelData
    local hasValue, curLv = channelData.channelLevelMap:TryGetValue(channelId)
    if not hasValue then
        curLv = 0
    end
    if string.isEmpty(randomShopId) then
        return nil
    end

    local remainCount = shopSys:GetRemainLimitCountByShopId(randomShopId)
    local limitUpCount = shopGroupData.domainChannelData.buyRandomGoodsLimitUpCount
    local maxLimitCount = shopGroupData.domainChannelData.buyRandomGoodsLimitCount
    local willOverflow = (remainCount + limitUpCount) > maxLimitCount
    local isUnlockDomainShop = shopSys:CheckShopUnlocked(randomShopId)

    local info = DomainPOIUtils.getRemindInfoTemplate()
    info.poiId = channelId
    info.curLevel = curLv
    info.maxLevel = DomainPOIUtils.GetDomainShopChannelMaxLevel(channelId)
    info.upgradeQuestId = Tables.shopDomainConst.domainShopUnlockQuestId
    local _, curLvChannelCfg = shopChannelCfg.channelLevelMap:TryGetValue(curLv)
    if curLvChannelCfg then
        info.isFinalMaxLv = curLvChannelCfg.isFinalMaxLevel
    end
    local _, nextLvChannelCfg = shopChannelCfg.channelLevelMap:TryGetValue(curLv + 1)
    if nextLvChannelCfg then
        info.upgradeCostMoney = nextLvChannelCfg.costItemNumList[0]
    end
    info.mapMarkType = GEnums.MarkType.DomainShop
    info.jumpPhaseId = PhaseId.ShopTrade
    if isUnlockDomainShop then
        if willOverflow then
            info.needWarning = true
            info.stateDesc = Language.LUA_DOMAIN_POI_OVERVIEW_DOMAIN_SHOP_OVERFLOW
        else
            info.stateDesc = Language.LUA_DOMAIN_POI_OVERVIEW_DOMAIN_SHOP_NORMAL
        end
    elseif curLv > 0 then
        info.stateDesc = Language.LUA_DOMAIN_POI_OVERVIEW_DOMAIN_SHOP_NO_RANDOM
    end

    return { info }
end

function DomainPOIUtils.getPOIRemindInfo_RecycleBin(levelId)
    local succ, recycleBinsCfg = Tables.levelId2RecycleBinsTable:TryGetValue(levelId)
    if not succ then
        return {}
    end

    local infos = {}
    for _, recycleBinId in pairs(recycleBinsCfg.recycleBinIds) do

        local info = DomainPOIUtils.getRemindInfoTemplate()
        local recycleBinCfg = Tables.recycleBinTable[recycleBinId]
        local isUnlock, recycleBinData = GameInstance.player.recycleBinSystem.recycleBins:TryGetValue(recycleBinId)
        info.poiId = recycleBinId
        info.poiSerial = recycleBinCfg.serialId
        info.curLevel = isUnlock and recycleBinData.lv or 0
        info.maxLevel = isUnlock and recycleBinData.maxLv or 5
        local levelData = recycleBinCfg.levelData
        info.upgradeCostMoney = isUnlock and levelData[recycleBinData.lv].lvUpCost or recycleBinCfg.unlockCost
        info.isFinalMaxLv = true
        info.mapMarkType = GEnums.MarkType.Recycler

        local canPick = isUnlock and recycleBinData.isCanPickUp
        info.needWarning = canPick
        info.stateDesc = canPick and Language.LUA_DOMAIN_POI_OVERVIEW_RECYCLE_BIN_CAN_PICK
                or Language.LUA_DOMAIN_POI_OVERVIEW_RECYCLE_BIN_CANT_PICK

        table.insert(infos, info)
    end

    return infos
end

function DomainPOIUtils.getPOIRemindInfo_SimulationTraining(levelId)
    if levelId ~= Tables.simulationTrainingConst.simulationTrainingRefLevelId then
        return nil
    end

    local system = GameInstance.player.simulationTrainingSystem

    local info = DomainPOIUtils.getRemindInfoTemplate()
    info.poiId = system:GetInstId()
    info.curLevel = system.curLevel
    info.maxLevel = system.maxLevel
    info.isFinalMaxLv = not system:IsSimulationTrainingCanLevelUp()
    info.isLvMax = not system:IsSimulationTrainingCanLevelUp()
    info.mapMarkType = GEnums.MarkType.SimulationTraining

    if not info.isFinalMaxLv then
        local hasTargetLevelCfg, targetLevelData = Tables.simulationTrainingLevelTable:TryGetValue(info.curLevel + 1)
        if hasTargetLevelCfg then
            info.upgradeCostMoney = targetLevelData.costDomainMoney
        end
    end

    if system.dailyPlayCnt > 0 then
        info.needWarning = true
        info.stateDesc = string.format(Language.LUA_SIMULATION_TRAINING_POI_OVERVIEW_PLAY_DESC, system.dailyPlayCnt)
    else
        if info.curLevel == 0 then
            info.upgradeQuestId = Tables.simulationTrainingConst.unlockQuestId
        end
        info.stateDesc = Language.LUA_SIMULATION_TRAINING_POI_OVERVIEW_DESC
    end

    return { info }
end

function DomainPOIUtils.getPOIRemindInfo_DomainDepot(levelId)
    local levelDepotList = {}
    for depotId, domainDepotCfg in pairs(Tables.domainDepotTable) do
        if domainDepotCfg.refLevelId == levelId then
            table.insert(levelDepotList, depotId)
        end
    end
    if #levelDepotList == 0 then
        return nil
    end

    local remindInfoList = {}
    for index, depotId in ipairs(levelDepotList) do
        local depotInfo = DomainDepotUtils.GetDepotInfo(depotId)
        local remindInfo = DomainPOIUtils.getRemindInfoTemplate()

        remindInfo.poiId = depotId
        if #levelDepotList > 1 then
            remindInfo.poiSerial = index
        end
        remindInfo.curLevel = depotInfo.currLevel
        remindInfo.maxLevel = depotInfo.maxLevel
        remindInfo.isFinalMaxLv = depotInfo.isFinalMaxLevel
        remindInfo.mapMarkType = GEnums.MarkType.DomainDepot
        remindInfo.jumpPhaseId = PhaseId.DomainDepotPackage

        if depotInfo.currLevel < depotInfo.maxLevel then
            local nextLevel = depotInfo.currLevel + 1
            local nextLevelCfg = depotInfo.depotLevelList[nextLevel]
            remindInfo.upgradeCostMoney = nextLevelCfg.costDomainMoney
        end

        if depotInfo.currLevel > 0 then
            if depotInfo.currLevelConfig.deliverItemTypeList.Count <= 0 then
                remindInfo.stateDesc = Language.LUA_DOMAIN_DEPOT_REMIND_DELIVER_LOCKED
            else
                remindInfo.stateDesc = depotInfo.depotRuntimeData.canPack and
                    Language.LUA_DOMAIN_DEPOT_REMIND_DELIVER_AVAILABLE or
                    Language.LUA_DOMAIN_DEPOT_REMIND_DELIVER_PREPARING
                remindInfo.needWarning = depotInfo.depotRuntimeData.canPack
            end
        end

        table.insert(remindInfoList, remindInfo)
    end

    return remindInfoList
end

function DomainPOIUtils.getPOIRemindInfo_SewageTreatPlant(levelId)
    local plantId = FactoryUtils.getSewageTreatPlantIdByLevelId(levelId)
    if plantId == nil then
        return nil
    end

    local plantData = FactoryUtils.getSewageTreatPlantData(plantId)
    if plantData == nil then
        return nil
    end

    if plantData.currLevel == 0 then
        return nil
    end

    local remindInfo = DomainPOIUtils.getRemindInfoTemplate()
    remindInfo.poiId = plantId
    remindInfo.curLevel = plantData.currLevel
    remindInfo.maxLevel = plantData.maxLevel
    remindInfo.isFinalMaxLv = plantData.isFinalMaxLevel
    remindInfo.mapMarkType = GEnums.MarkType.SewageTreatPlant

    if plantData.currLevel < plantData.maxLevel then
        remindInfo.upgradeCostMoney = plantData.levelCost
    end

    remindInfo.stateDesc = Language.LUA_FAC_SEWAGE_TREAT_POI_REMIND_DEFAULT_TEXT

    return { remindInfo }
end

function DomainPOIUtils.getPOIRemindInfo_KiteStation(levelId)

    local kiteStationList = {}
    for kiteStationId, kiteStationCfg in pairs(Tables.kiteStationLevelTable) do
        if kiteStationCfg.levelId == levelId then
            table.insert(kiteStationList, {k = kiteStationId, v = kiteStationCfg})
        end
    end

    if #kiteStationList == 0 then
        return nil
    end

    local remindInfoList = {}
    for index, kv in ipairs(kiteStationList) do
        local kiteStationId = kv.k
        local kiteStationCfg = kv.v
        local data = GameInstance.player.kiteStationSystem:GetKiteStationDataByInstId(kiteStationId)
        local remindInfo = DomainPOIUtils.getRemindInfoTemplate()
        remindInfo.poiId = kiteStationId
        if #kiteStationList > 1 then
            remindInfo.poiSerial = index
        end
        remindInfo.curLevel = data.level
        remindInfo.maxLevel = DomainPOIUtils.GetKiteStationMaxLevel(kiteStationId)
        remindInfo.mapMarkType = GEnums.MarkType.KiteStation
        remindInfo.jumpPhaseId = PhaseId.KiteStation
        if data.level > 0 then
            remindInfo.isFinalMaxLv = kiteStationCfg.list[data.level].isFinalMaxLevel
        end

        if data.level < remindInfo.maxLevel then
            local nextLevel = data.level + 1
            if nextLevel > 0 then
                local nextLevelCfg = kiteStationCfg.list[nextLevel]
                remindInfo.upgradeCostMoney = nextLevelCfg.costItemCount[0]
                remindInfo.upgradeQuestId = nextLevelCfg.upgradeQuestId
            end
        end

        local entrustIdx = GameInstance.player.kiteStationSystem:GetEntrustIdx(kiteStationId)

        local unCompletedCount = 0
        local completedCount = 0
        for i = 0, entrustIdx.Count - 1 do
            local state = GameInstance.player.kiteStationSystem:GetEntrustState(kiteStationId, entrustIdx[i])
            if state ~= CS.Proto.KITE_STATION_ENTRUST_TASK_STATUS.Completed then
                unCompletedCount = unCompletedCount + 1
            else
                completedCount = completedCount + 1
            end
        end
        if data.level > 0 then
            local entrustSlotCnt = kiteStationCfg.list[data.level].entrustSlotCnt
            remindInfo.needWarning = unCompletedCount > 0
            if unCompletedCount == 0 then

                remindInfo.stateDesc = Language.LUA_DOMAIN_POI_OVERVIEW_KITE_STATION_ENTRUST_ALL_COMPLETED
            elseif unCompletedCount == entrustSlotCnt then

                remindInfo.stateDesc = Language.LUA_DOMAIN_POI_OVERVIEW_KITE_STATION_ENTRUST_FULL
            else

                remindInfo.stateDesc = string.format(Language.LUA_DOMAIN_POI_OVERVIEW_KITE_STATION_ENTRUST_AVAILABLE, unCompletedCount)
            end
        end
        table.insert(remindInfoList, remindInfo)
    end
    return remindInfoList
end



DomainPOIUtils.MarkTypeMap = {
    [GEnums.DomainPoiType.KiteStation] = GEnums.MarkType.KiteStation,
    [GEnums.DomainPoiType.DomainShop] = GEnums.MarkType.DomainShop,
    [GEnums.DomainPoiType.SimulationTraining] = GEnums.MarkType.SimulationTraining,
    [GEnums.DomainPoiType.DomainDepot] = GEnums.MarkType.DomainDepot,
    [GEnums.DomainPoiType.RecycleBin] = GEnums.MarkType.Recycler,
    [GEnums.DomainPoiType.SewageTreatPlant] = GEnums.MarkType.SewageTreatPlant,
}

DomainPOIUtils.POICanUnlock = {
    [GEnums.DomainPoiType.KiteStation] = function(levelId)
        return GameInstance.player.kiteStationSystem:GetAllCanUnlockKiteStationIdsByLevelId(levelId)
    end,
    [GEnums.DomainPoiType.RecycleBin] = function(levelId)
        return GameInstance.player.recycleBinSystem:GetAllCanUnlockRecycleBinIdsByLevelId(levelId)
    end,
    [GEnums.DomainPoiType.SimulationTraining] = function(levelId)
        return GameInstance.player.simulationTrainingSystem:GetMapSimulationTrainingUnlockId()
    end,
    [GEnums.DomainPoiType.DomainDepot] = function(levelId)
        return GameInstance.player.domainDepotSystem:GetAllCanUnlockDomainDepotIdsByLevelId(levelId)
    end,
    [GEnums.DomainPoiType.DomainShop] = function(levelId)
        return DomainPOIUtils.GetAllCanUpDomainShopChannelIds(levelId, true)
    end,
}

DomainPOIUtils.POICanUpgrade = {
    [GEnums.DomainPoiType.KiteStation] = function(levelId)
        return GameInstance.player.kiteStationSystem:GetAllCanLevelUpKiteStationIdsByLevelId(levelId)
    end,
    [GEnums.DomainPoiType.RecycleBin] = function(levelId)
        return GameInstance.player.recycleBinSystem:GetAllCanLevelUpRecycleBinIdsByLevelId(levelId)
    end,
    [GEnums.DomainPoiType.SimulationTraining] = function(levelId)
        return GameInstance.player.simulationTrainingSystem:GetMapSimulationTrainingLevelId()
    end,
    [GEnums.DomainPoiType.DomainDepot] = function(levelId)
        return GameInstance.player.domainDepotSystem:GetAllCanUpgradeDomainDepotIdsByLevelId(levelId)
    end,
    [GEnums.DomainPoiType.DomainShop] = function(levelId)
        return DomainPOIUtils.GetAllCanUpDomainShopChannelIds(levelId, false)
    end,
    [GEnums.DomainPoiType.SewageTreatPlant] = function(levelId)
        return FactoryUtils.getSewageTreatPlantCanLevelUpIdListInTargetLevel(levelId)
    end,
}

function DomainPOIUtils.GetAllCanUpDomainShopChannelIds(levelId, isCheckUnlock)
    local channelIds = {}

    local hasCfg, cfg = Tables.shopChannelLevelPOIMapTable:TryGetValue(levelId)
    if not hasCfg then
        return channelIds
    end
    local channelId = cfg.channelPartner
    local _, shopChannelCfg = Tables.shopChannelDevelopmentTable:TryGetValue(channelId)
    local canSkipPreconditions = DomainPOIUtils._CanSkipDomainShopChannelPreconditions(shopChannelCfg)

    local level001Id = "map01_lv001"
    if not canSkipPreconditions and levelId ~= level001Id then
        local hasCfg, cfg = Tables.shopChannelLevelPOIMapTable:TryGetValue(level001Id)
        if not hasCfg then
            return channelIds
        end
        local channelId = cfg.channelPartner
        local _, shopChannelCfg = Tables.shopChannelDevelopmentTable:TryGetValue(channelId)
        local shopGroupData = GameInstance.player.shopSystem:GetShopGroupData(shopChannelCfg.shopGroupId)
        local hasValue, curLv = shopGroupData.domainChannelData.channelLevelMap:TryGetValue(channelId)
        if not hasValue or curLv < 2 then
            return channelIds
        end
    end

    local info = DomainPOIUtils.GetPoiUpgradeCtrlInfo[GEnums.DomainPoiType.DomainShop](channelId, false)

    if (isCheckUnlock and info.curLevel > 0)
        or (not isCheckUnlock and info.curLevel <= 0)
    then
        return channelIds
    end

    if canSkipPreconditions then
        info.questState = CS.Beyond.Gameplay.MissionSystem.QuestState.Completed
    elseif string.isEmpty(info.upgradeQuestId) then
        info.questState = CS.Beyond.Gameplay.MissionSystem.QuestState.Completed
    else
        info.questState = GameInstance.player.mission:GetQuestState(info.upgradeQuestId)
    end
    if info.questState ~= CS.Beyond.Gameplay.MissionSystem.QuestState.Completed then
        return channelIds
    end

    if info.curLevel >= info.maxLevel then
        return channelIds
    end

    local _, domainCfg = Tables.domainDataTable:TryGetValue(info.domainId)
    local moneyId = domainCfg.domainGoldItemId
    local curMoneyCount = Utils.getItemCount(moneyId)
    local isMoneyEnough = curMoneyCount >= info.upgradeCostMoney
    if isMoneyEnough then
        table.insert(channelIds, channelId)
    end
    return channelIds
end




DomainPOIUtils.POIOverviewCellFoldState = {}


DomainPOIUtils.POIOverviewCellPreHasRemind = {}




function DomainPOIUtils.GetPOIOverviewCellIsFold(domainId, poiType)
    if DeviceInfo.usingController then
        return false
    end

    local domainInfo = DomainPOIUtils.POIOverviewCellFoldState[domainId]
    if not domainInfo then
        domainInfo = {}
        DomainPOIUtils.POIOverviewCellFoldState[domainId] = domainInfo
    end
    local poiTypeInt = poiType:GetHashCode()
    if domainInfo[poiTypeInt] == nil then
        domainInfo[poiTypeInt] = false
    end
    return domainInfo[poiTypeInt]
end



function DomainPOIUtils.SetPOIOverviewCellIsFold(domainId, poiType, isFold)
    if DeviceInfo.usingController then
        return false
    end

    local domainInfo = DomainPOIUtils.POIOverviewCellFoldState[domainId]
    if not domainInfo then
        domainInfo = {}
        DomainPOIUtils.POIOverviewCellFoldState[domainId] = domainInfo
    end
    local poiTypeInt = poiType:GetHashCode()
    domainInfo[poiTypeInt] = isFold
end




function DomainPOIUtils.GetPOIOverviewCellPreHasRemind(domainId, poiType)
    local domainInfo = DomainPOIUtils.POIOverviewCellPreHasRemind[domainId]
    if not domainInfo then
        domainInfo = {}
        DomainPOIUtils.POIOverviewCellPreHasRemind[domainId] = domainInfo
    end
    local poiTypeInt = poiType:GetHashCode()
    return domainInfo[poiTypeInt]
end



function DomainPOIUtils.SetPOIOverviewCellPreHasRemind(domainId, poiType, hasRemind)
    local domainInfo = DomainPOIUtils.POIOverviewCellPreHasRemind[domainId]
    if not domainInfo then
        domainInfo = {}
        DomainPOIUtils.POIOverviewCellPreHasRemind[domainId] = domainInfo
    end
    local poiTypeInt = poiType:GetHashCode()
    domainInfo[poiTypeInt] = hasRemind
end

function DomainPOIUtils.HasDomainPOIPreviewRedDot(domainId)













    return false
end

function DomainPOIUtils.HasSinglePOIPreviewRedDot(domainId, poiType)






















    return false
end

function DomainPOIUtils.CalculateSinglePOIOverviewContentInfo(domainId, contentInfo, justRedDot)
    local isLvMax = contentInfo.curLevel >= contentInfo.maxLevel
    local canUpgrade = not (isLvMax or contentInfo.blockUpgrade)
    local _, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
    local domainMoneyCount = Utils.getItemCount(domainCfg.domainGoldItemId)
    local enoughMoney = contentInfo.upgradeCostMoney == 0 or contentInfo.upgradeCostMoney <= domainMoneyCount
    local hasQuest = false
    if not string.isEmpty(contentInfo.upgradeQuestId) then
        local questState = GameInstance.player.mission:GetQuestState(contentInfo.upgradeQuestId)
        if questState ~= CS.Beyond.Gameplay.MissionSystem.QuestState.Completed then
            contentInfo.upgradeMissionId = GameInstance.player.mission:GetMissionIdByQuestId(contentInfo.upgradeQuestId)
        end
    end
    if not string.isEmpty(contentInfo.upgradeMissionId) then
        hasQuest = true
    end

    local remindUpgrade = canUpgrade and (hasQuest or enoughMoney) and not contentInfo.isBlockUpgrade
    local hasRemind = contentInfo.needWarning or remindUpgrade

    contentInfo.hasRemind = hasRemind
    if not justRedDot then
        local levelId = contentInfo.levelId
        local _, levelCfg = Tables.levelDescTable:TryGetValue(levelId)
        local levelName = levelCfg.showName

        contentInfo.levelId = levelId
        contentInfo.levelName = levelName

        contentInfo.isLvMax = isLvMax
        contentInfo.canUpgrade = canUpgrade
        contentInfo.hasQuest = hasQuest
        contentInfo.enoughMoney = enoughMoney
        contentInfo.remindUpgrade = remindUpgrade
    end
end



function DomainPOIUtils.GetPOIMapMarkInstId(mapMarkType, poiId)
    if mapMarkType == GEnums.MarkType.Settlement then

        return GameInstance.player.mapManager:GetSettlementMarkInstIdBySettlementId(poiId)
    else
        return GameInstance.player.mapManager:GetMapMarkInstId(mapMarkType, poiId)
    end
end





function DomainPOIUtils.resolveOpenSettlementArgs(args)
    local domainId
    local defaultStlId
    if args and type(args) == "string" then

        local hasCfg, levelCfg = DataManager.levelBasicInfoTable:TryGetValue(args)
        if hasCfg then
            domainId = levelCfg.domainName
            local _, cfg = Tables.settlementLevelPOIMapTable:TryGetValue(args)
            if cfg then
                defaultStlId = cfg.settlementId
            end
        else
            domainId = args
        end
    elseif type(args) == "table" and not string.isEmpty(args.domainId) then
        domainId = args.domainId
        defaultStlId = args.poiId
    else
        domainId = ScopeUtil.GetCurrentChapterIdAsStr()
    end

    return domainId, defaultStlId
end

function DomainPOIUtils.resolveOpenGradeArgs(arg)
    local tableArg
    local domainId
    if type(arg) == "string" then
        tableArg = { domainId = arg }
    elseif type(arg) == "table" then
        tableArg = arg
    else
        tableArg = { domainId = Utils.getCurDomainId() }
    end
    if not string.isEmpty(tableArg.domainId) then
        domainId = tableArg.domainId
    else
        domainId = Utils.getCurDomainId()
        tableArg.domainId = domainId
    end
    return tableArg, domainId
end


function DomainPOIUtils._CanSkipDomainShopChannelPreconditions(shopChannelCfg)
    if shopChannelCfg == nil or shopChannelCfg.skipToMissionChapter == nil then
        return false
    end
    local skipToMissionChapter = shopChannelCfg.skipToMissionChapter
    if (skipToMissionChapter:GetHashCode() or 0) <= 0 then
        return false
    end
    local earlyAcceptChapterMask = GameInstance.player.mission.earlyAcceptChapterMask
    return earlyAcceptChapterMask:HasFlag(skipToMissionChapter)
end





function DomainPOIUtils.getSettlementTradeActivityInfo()

    local activitySys = GameInstance.player.activitySystem
    local info = {

        hasActivity = false,
        activityId = "",
        openTime = 0,
        closeTime = 0,
        activityMoneyId = "",
        activityColor = Color.white,

        domainActivityInfos = {},
    }


    local idList = activitySys:GetActivityOfCertainType(GEnums.ActivityType.LimitedFormula)
    if idList.Count <= 0 then
        return info
    end

    local activityId = idList[0]
    info.activityId = activityId
    local _, activityCfg = Tables.activityTable:TryGetValue(activityId)


    local activityData = activitySys:GetActivity(activityId)
    if activityData.status ~= GEnums.ActivityStatus.InProgress and
        activityData.status ~= GEnums.ActivityStatus.Completed then
        return info
    end

    local _, limitedFormulaCfg = Tables.activityLimitedFormulaTable:TryGetValue(activityId)
    local finalStageId = limitedFormulaCfg.endStageId
    local isFinalStage = ActivityUtils.isFinalStageMultiConditionStageActivity(activityData, finalStageId)
    if isFinalStage then
        return info
    end



    info.hasActivity = true
    info.activityColor = UIUtils.getColorByString(activityCfg.themeColor)
    info.openTime = activityData.startTime
    info.closeTime = activityData.endTime
    info.activityMoneyId = limitedFormulaCfg.moneyId




    local _, limitedFormulaStlCfg = Tables.activityLimitedFormulaSettlementTable:TryGetValue(activityId)
    for stlId, tradeItemDict in pairs(limitedFormulaStlCfg.settlementList) do
        local stlData = GameInstance.player.settlementSystem:GetUnlockSettlementData(stlId)
        if stlData then
            local _, stlCfg = Tables.settlementBasicDataTable:TryGetValue(stlId)
            local hasCanCraftItem = false

            local domainId = stlCfg.domainId
            local actDomainInfo = info.domainActivityInfos[domainId]
            if not actDomainInfo then
                actDomainInfo = {}
            end

            local actStlInfo = actDomainInfo[stlId]
            if not actStlInfo then
                actStlInfo = {}
            end

            for itemId, tradeItemInfo in pairs(tradeItemDict.tradeList) do

                local isHide = not GameInstance.player.inventory:IsItemFound(itemId)
                if not isHide then
                    hasCanCraftItem = true
                    actStlInfo[itemId] = tradeItemInfo.moneyCount
                end
            end

            if hasCanCraftItem then
                info.domainActivityInfos[domainId] = actDomainInfo
                actDomainInfo[stlId] = actStlInfo
            end
        end
    end


    return info
end



function DomainPOIUtils.getSettlementMoneyActivityEffectInfo(stlId)
    local totalEffect = 0
    local activityId = ""

    local globalEffectSys = GameInstance.player.globalEffectSystem
    local effects = globalEffectSys:GetGlobalEffectByType(GEnums.GlobalEffectType.AddSettlementActivityBuff)
    if effects then

        for effectId, effectInfo in cs_pairs(effects) do
            local _, effectCfg = Tables.globalEffectTable:TryGetValue(effectId)
            if effectCfg then
                if effectCfg.dps[0].valueStringList == nil or effectCfg.dps[0].valueStringList.Count <= 0 then
                    logger.error("effectCfg.dps[0].valueStringList为空！effectId：", effectId)
                else
                    if effectCfg.dps[0].valueStringList[0] == stlId then
                        if effectCfg.dps[1].valueFloatList == nil or effectCfg.dps[1].valueFloatList.Count <= 0 then
                            logger.error("effectCfg.dps[1].valueFloatList为空！effectId：", effectId)
                        elseif effectCfg.dps[2].valueStringList == nil or effectCfg.dps[2].valueStringList.Count <= 0 then
                            logger.error("effectCfg.dps[2].valueStringList为空！effectId：", effectId)
                        else
                            totalEffect = totalEffect + effectCfg.dps[1].valueFloatList[0]
                            activityId = effectCfg.dps[2].valueStringList[0]
                        end
                    end
                end
            end
        end
    end
    return math.floor(totalEffect * 100), activityId
end




_G.DomainPOIUtils = DomainPOIUtils
return DomainPOIUtils
