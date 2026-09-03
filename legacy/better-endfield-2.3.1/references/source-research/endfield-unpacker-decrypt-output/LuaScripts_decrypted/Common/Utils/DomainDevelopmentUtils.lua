local DomainDevelopmentUtils = {}

local DetailState = {
    DetailPreAndCur = 1,
    Unknown = 2,
    Ellipsis = 3,
}


local UpgradeEffectConfig = {
    ["isMineOutputUp"] = {
        sortId = 0,
        effectDesc = "LUA_DOMAIN_DEVELOPMENT_MINE_OUTPUT_UP",
    },
    ["bandwidth"] = {
        sortId = 1,
        effectDesc = "LUA_DOMAIN_DEV_BANDWIDTH_UPGRADE",
    },
    ["travelPoleLimit"] = {
        sortId = 2,
        effectDesc = "LUA_DOMAIN_DEV_TRAVEL_POLE_LIMIT_UPGRADE",
    },
    ["battleBuildingLimit"] = {
        sortId = 3,
        effectDesc = "LUA_DOMAIN_DEV_BATTLE_BUILDING_LIMIT_UPGRADE",
    },
}

local CANT_GET_COLOR = "787878"

local LOCKED_LEVEL_ID = "LOCKED"
local LOCKED_LEVEL_SORT_ID = 9999

function DomainDevelopmentUtils.updateLevelCellTitle(view, domainId, lv)
    local domainDevSys = GameInstance.player.domainDevelopmentSystem

    local domainData = domainDevSys.domainDevDataDic:get_Item(domainId)
    local isCurLv = lv == domainData.lv
    local reachedLevel = lv <= domainData.lv
    local isGet = domainDevSys:IsLevelRewarded(domainId, lv)

    local domainCfg = Tables.domainDataTable[domainId]
    local colorStr = reachedLevel and domainCfg.domainGradeTitleColor or CANT_GET_COLOR
    view.bgImage.color = UIUtils.getColorByString(colorStr)
    view.lvNumberTxt.text = lv

    view.unlockNode.gameObject:SetActive(reachedLevel)
    view.inCompleteNode.gameObject:SetActive(not reachedLevel)

    local firstLevel = lv == 1
    view.receiveBtn.gameObject:SetActive(not firstLevel and not isGet)
    view.receivedNode.gameObject:SetActive(not firstLevel and isGet)
    view.completeNode.gameObject:SetActive(firstLevel)

    view.rewardListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

function DomainDevelopmentUtils.updateLevelRewardCell(cell, rewardInfo, isGet)
    local hasItem = rewardInfo ~= nil
    cell.itemReward.gameObject:SetActive(hasItem)
    cell.emptyNode.gameObject:SetActive(not hasItem)

    if rewardInfo then
        cell.itemReward:InitItem(rewardInfo, function()
            UIUtils.showItemSideTips(cell.itemReward, UIConst.UI_TIPS_POS_TYPE.LeftDown)
        end)
        cell.itemReward:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
    end

    cell.itemReward.view.rewardedCover.gameObject:SetActive(isGet)
end

function DomainDevelopmentUtils.updateLevelEffectCell(cell, levelEffectInfoUnit, reachedLevel)

    local isSpecial = false
    if isSpecial then
        if reachedLevel then
            cell.stateCtr:SetState("AchieveSpecial")
        else
            cell.stateCtr:SetState("InCompleteSpecial")
        end
    else
        if reachedLevel then
            cell.stateCtr:SetState("AchieveNormal")
        else
            cell.stateCtr:SetState("InCompleteNormal")
        end
    end

    if levelEffectInfoUnit.firstLevel then
        cell.descTxt.text = levelEffectInfoUnit.effectDesc
    else
        local levelId = levelEffectInfoUnit.levelId
        if levelId then
            local levelName = Language.LUA_DOMAIN_DEV_LOCKED_LEVEL_DES
            if levelId ~= LOCKED_LEVEL_ID then
                levelName = Tables.LevelDescTable[levelId].showName
            end
            local effectDesc = levelEffectInfoUnit.effectDesc
            cell.descTxt.text = string.format(Language.LUA_DOMAIN_DEVELOPMENT_LEVEL_NAME_CONNECT_EFFECT, levelName, effectDesc)
        else
            cell.descTxt.text = levelEffectInfoUnit.effectDesc
        end
    end

    local detailState = levelEffectInfoUnit.detailState
    cell.promoteNode.gameObject:SetActive(detailState == DetailState.DetailPreAndCur)
    cell.unknownNode.gameObject:SetActive(detailState == DetailState.Unknown)
    cell.unlockNode.gameObject:SetActive(detailState == DetailState.Ellipsis)
    if levelEffectInfoUnit.isFinalMax and detailState ~= DetailState.Unknown then
        cell.stateCtr:SetState("IsFinalMax")
    else
        cell.stateCtr:SetState("NotFinalMax")
    end

    if detailState == DetailState.DetailPreAndCur then
        cell.preTxt.text = UIUtils.getNumString(levelEffectInfoUnit.pre)
        cell.curTxt.text = UIUtils.getNumString(levelEffectInfoUnit.cur)
    end

    cell.animationWrapper:Play("domaingradepopupcell_in")
end

function DomainDevelopmentUtils.genLevelEffectInfo(domainId, lv)
    local levelEffectInfo = {}

    if lv == 1 then
        table.insert(levelEffectInfo, {
            firstLevel = true,
            effectDesc = Language.LUA_DOMAIN_DEV_LEVEL_ONE_EFFECT_DESC,
            detailState = DetailState.Ellipsis,
        })
        return levelEffectInfo
    end



    local domainCfg = Tables.domainDataTable[domainId]
    local csIndex = CSIndex(lv)
    local domainLevelCfg = domainCfg.domainDevelopmentLevel[csIndex]
    local domainLevelEffect = domainLevelCfg.domainDevelopmentLevelEffect
    local moneyLimit = domainLevelCfg.moneyLimit

    local mapMgr = GameInstance.player.mapManager


    local preLevel = lv - 1
    local preLevelCsIndex = CSIndex(preLevel)
    local domainPreLevelCfg = domainCfg.domainDevelopmentLevel[preLevelCsIndex]



    local hasTransCfg, domainTransmissionCfg = Tables.factoryDomainItemTransmissionTable:TryGetValue(domainId)
    if hasTransCfg then
        local transferUnlockLevel = domainTransmissionCfg.unlockLevel
        local transferUnlockLosslessLevel = domainTransmissionCfg.unlockLosslessLevel
        local info

        if lv == transferUnlockLevel then

            info = {
                effectDesc = string.format(Language.LUA_DOMAIN_DEVELOPMENT_UNLOCK_TRANSFER, domainCfg.domainName),
                detailState = DetailState.Ellipsis,
                isNewUnlockSystem = true,
                isFinalMax = domainTransmissionCfg.levelToIsFinalMaxCapacity[lv],

                typeSortId = 1,
                levelSortId = -1,
                effectSortId = -1,
            }
        elseif lv > transferUnlockLevel then

            local preCapacity
            local nowCapacity
            if preLevel <= #domainTransmissionCfg.levelToCapacity then
                preCapacity = domainTransmissionCfg.levelToCapacity[preLevel]
            end
            if lv <= #domainTransmissionCfg.levelToCapacity then
                nowCapacity = domainTransmissionCfg.levelToCapacity[lv]
            end
            if preCapacity ~= nowCapacity and preCapacity ~= nil then
                info = {
                    effectDesc = string.format(Language.LUA_DOMAIN_DEVELOPMENT_UNLOCK_TRANSFER_UPGRADE, domainCfg.domainName),
                    detailState = DetailState.DetailPreAndCur,
                    pre = preCapacity,
                    cur = nowCapacity,
                    isNewUnlockSystem = true,
                    isFinalMax = domainTransmissionCfg.levelToIsFinalMaxCapacity[lv],

                    typeSortId = 2,
                    levelSortId = -1,
                    effectSortId = -1,
                }
            end
        end

        if info then
            table.insert(levelEffectInfo, info)
        end

        if lv == transferUnlockLosslessLevel then
            info = {
                effectDesc = string.format(Language.LUA_DOMAIN_DEVELOPMENT_UNLOCK_TRANSFER_LOSSLESS, domainCfg.domainName),
                detailState = DetailState.Ellipsis,
                isNewUnlockSystem = true,

                typeSortId = 1,
                levelSortId = -1,
                effectSortId = -1,
            }
            table.insert(levelEffectInfo, info)
        end
    else
        logger.error("跨区域传输表数据缺失！domainId：", domainId)
    end



    local preMoneyLimit = domainPreLevelCfg.moneyLimit
    if preMoneyLimit ~= moneyLimit then
        table.insert(levelEffectInfo, {
            effectDesc = string.format(Language.LUA_DOMAIN_DEV_LEVEL_MONEY_LIMIT_UPGRADE, domainCfg.domainName),
            detailState = DetailState.DetailPreAndCur,
            pre = preMoneyLimit,
            cur = moneyLimit,
            isFinalMax = domainLevelCfg.isFinalMaxMoneyLimit,

            typeSortId = 3,
            levelSortId = -1,
            effectSortId = -1,
        })
    end




    local lockedLevelBandwidthUpgrade = false
    local lockedLevelTravelPoleLimitUpgrade = false
    local lockedLevelBattleBuildingLimitUpgrade = false
    local lockedLevelMineOutputUp = false

    for levelId, levelEffect in pairs(domainLevelEffect) do

        local preLevelEffect = domainPreLevelCfg.domainDevelopmentLevelEffect[levelEffect.levelId]
        local isLevelUnlocked = mapMgr:IsLevelUnlocked(levelId)


        if (levelEffect.isMineOutputUp and (isLevelUnlocked or not lockedLevelMineOutputUp)) then
            table.insert(levelEffectInfo, {
                levelId = isLevelUnlocked and levelEffect.levelId or LOCKED_LEVEL_ID,
                effectDesc = Language[UpgradeEffectConfig["isMineOutputUp"].effectDesc],
                detailState = isLevelUnlocked and DetailState.Ellipsis or DetailState.Unknown,
                isFinalMax = levelEffect.isFinalMaxMineOutputUp,

                typeSortId = 5,
                levelSortId = isLevelUnlocked and levelEffect.sortId or LOCKED_LEVEL_SORT_ID,
                effectSortId = UpgradeEffectConfig["isMineOutputUp"].sortId,
            })
            if not isLevelUnlocked then
                lockedLevelMineOutputUp = true
            end
        end


        local preBandwidth = preLevelEffect.bandwidth
        if (preBandwidth ~= levelEffect.bandwidth and (isLevelUnlocked or not lockedLevelBandwidthUpgrade)) then
            table.insert(levelEffectInfo, {
                levelId = isLevelUnlocked and levelEffect.levelId or LOCKED_LEVEL_ID,
                effectDesc = Language[UpgradeEffectConfig["bandwidth"].effectDesc],
                detailState = isLevelUnlocked and DetailState.DetailPreAndCur or DetailState.Unknown,
                pre = preBandwidth,
                cur = levelEffect.bandwidth,
                isFinalMax = levelEffect.isFinalMaxBandwidth,

                typeSortId = 5,
                levelSortId = isLevelUnlocked and levelEffect.sortId or LOCKED_LEVEL_SORT_ID,
                effectSortId = UpgradeEffectConfig["bandwidth"].sortId,
            })



            if not isLevelUnlocked then
                lockedLevelBandwidthUpgrade = true
            end
        end


        local preTravelPoleLimit = preLevelEffect.travelPoleLimit
        if (preTravelPoleLimit ~= levelEffect.travelPoleLimit and (isLevelUnlocked or not lockedLevelTravelPoleLimitUpgrade)) then
            table.insert(levelEffectInfo, {
                levelId = isLevelUnlocked and levelEffect.levelId or LOCKED_LEVEL_ID,
                effectDesc = Language[UpgradeEffectConfig["travelPoleLimit"].effectDesc],
                detailState = isLevelUnlocked and DetailState.DetailPreAndCur or DetailState.Unknown,
                pre = preTravelPoleLimit,
                cur = levelEffect.travelPoleLimit,
                isFinalMax = levelEffect.isFinalMaxTravelPoleLimit,

                typeSortId = 5,
                levelSortId = isLevelUnlocked and levelEffect.sortId or LOCKED_LEVEL_SORT_ID,
                effectSortId = UpgradeEffectConfig["travelPoleLimit"].sortId,
            })
            if not isLevelUnlocked then
                lockedLevelTravelPoleLimitUpgrade = true
            end
        end


        local preBattleBuildingLimit = preLevelEffect.battleBuildingLimit
        if (preBattleBuildingLimit ~= levelEffect.battleBuildingLimit and (isLevelUnlocked or not lockedLevelBattleBuildingLimitUpgrade)) then
            table.insert(levelEffectInfo, {
                levelId = isLevelUnlocked and levelEffect.levelId or LOCKED_LEVEL_ID,
                effectDesc = Language[UpgradeEffectConfig["battleBuildingLimit"].effectDesc],
                detailState = isLevelUnlocked and DetailState.DetailPreAndCur or DetailState.Unknown,
                pre = preBattleBuildingLimit,
                cur = levelEffect.battleBuildingLimit,
                isFinalMax = levelEffect.isFinalMaxBattleBuildingLimit,

                typeSortId = 5,
                levelSortId = isLevelUnlocked and levelEffect.sortId or LOCKED_LEVEL_SORT_ID,
                effectSortId = UpgradeEffectConfig["battleBuildingLimit"].sortId,
            })
            if not isLevelUnlocked then
                lockedLevelBattleBuildingLimitUpgrade = true
            end
        end
    end



    table.sort(levelEffectInfo, Utils.genSortFunction({ "typeSortId", "levelSortId", "effectSortId" }, true))
    return levelEffectInfo
end


_G.DomainDevelopmentUtils = DomainDevelopmentUtils
return DomainDevelopmentUtils