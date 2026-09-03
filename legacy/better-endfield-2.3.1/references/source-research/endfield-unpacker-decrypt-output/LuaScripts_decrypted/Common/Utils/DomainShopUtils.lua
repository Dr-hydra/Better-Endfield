local DomainShopUtils = {}



function DomainShopUtils.curServeAreaIsRedForGain()
    local serverIdType = Utils.getServerAreaType()
    return lume.find(Tables.shopDomainConst.redForGainServerAreaList, serverIdType) ~= nil
end



function DomainShopUtils.getProfitArrowStateName(profitRatio)
    local isRedGain = DomainShopUtils.curServeAreaIsRedForGain()
    local stateName
    if profitRatio >= 0 then
        if isRedGain then
            stateName = "RedGain"
        else
            stateName = "GreenGain"
        end
    else
        if isRedGain then
            stateName = "GreenLoss"
        else
            stateName = "RedLoss"
        end
    end
    return stateName
end


function DomainShopUtils.refreshTotalMyPositionDetail(view, info)
    view.moneyIconImg:LoadSprite(UIConst.UI_SPRITE_WALLET, info.moneyIcon)
    view.moneyNumTxt.text = info.moneyCount
    view.profitTxt.text = math.abs(info.profit)
    view.profitRatioTxt.text = info.profitRatio > 0 and '+' .. info.profitRatio or info.profitRatio
    view.profitArrowStateCtrl:SetState(DomainShopUtils.getProfitArrowStateName(info.profitRatio))
end



function DomainShopUtils.getNextServerRefreshTimeLeftSecByType(type)
    local next = nil

    if type == GEnums.ShopFrequencyLimitType.Daily then
        next = Utils.getNextCommonServerRefreshTime()
    end
    if type == GEnums.ShopFrequencyLimitType.Weekly then
        next = Utils.getNextWeeklyServerRefreshTime()
    end
    if type == GEnums.ShopFrequencyLimitType.Monthly then
        next = Utils.getNextMonthlyServerRefreshTime()
    end

    if next then
        local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
        return next - curTime
    else
        return -1
    end
end


function DomainShopUtils.getAllLocalUnlockRandomShopIds()
    local randomShopIds = {}
    for _, domainCfg in pairs(Tables.domainDataTable) do
        local shopGroupId = domainCfg.domainShopGroupId
        local hasCfg, shopGroupCfg = Tables.shopGroupTable:TryGetValue(shopGroupId)
        if hasCfg then
            for _, shopId in pairs(shopGroupCfg.shopIds) do
                local _, shopCfg = Tables.shopTable:TryGetValue(shopId)
                if shopCfg.shopRefreshType == GEnums.ShopRefreshType.RefreshRandom
                    and GameInstance.player.shopSystem:CheckShopUnlocked(shopId)
                then
                    table.insert(randomShopIds, shopId)
                    break
                end
            end
        end
    end
    return randomShopIds
end


function DomainShopUtils.openDomainFriendShop(friendRoleId)
    GameInstance.player.shopSystem:SendQueryFriendShop(friendRoleId, DomainShopUtils.getAllLocalUnlockRandomShopIds())
    UIUtils.waitMsgExecute(MessageConst.ON_FRIEND_SHOP_INFO_SYNC, nil, function(msgArg)
        local shopData = GameInstance.player.shopSystem:GetFriendShopData(friendRoleId)
        if shopData == nil then
            logger.error("打开好友地区商店发生错误！收到服务端消息但客户端找不到对应friendRoleId商店数据：" .. friendRoleId)
            return
        end
        if PhaseManager:IsOpen(PhaseId.ShopTrade) then
            return
        end
        PhaseManager:OpenPhase(PhaseId.ShopTrade, {
            friendRoleId = friendRoleId
        })
    end)
end

function DomainShopUtils.getDomainIdByDomainShopId(domainShopId)
    local shopGroupId = GameInstance.player.shopSystem:GetShopGroupIdByShopId(domainShopId)
    local hasCfg, shopGroupDomainCfg = Tables.shopGroupDomainTable:TryGetValue(shopGroupId)
    if hasCfg then
        return shopGroupDomainCfg.domainId
    else
        return ""
    end
end

function DomainShopUtils.GetPhotoAnimCharacterIdByItemId(itemId)
    if string.isEmpty(itemId) then
        return ""
    end
    local snapshotSystem = GameInstance.player.snapshotSystem
    snapshotSystem:InitActionConfig()
    local entry = snapshotSystem:GetActionEntryByItemId(itemId)
    if not entry then
        return ""
    end
    local characterIds = snapshotSystem:GetCharacterIdsByActionId(entry.actionId)
    if not characterIds then
        return ""
    end
    for i = 0, characterIds.Count - 1 do
        local characterId = characterIds[i]
        if Tables.characterTable:ContainsKey(characterId) then
            return characterId
        end
    end
    return ""
end

function DomainShopUtils.IsPhotoAnimActionStatic(itemId)
    if string.isEmpty(itemId) then
        return true
    end
    local snapshotSystem = GameInstance.player.snapshotSystem
    snapshotSystem:InitActionConfig()
    local entry = snapshotSystem:GetActionEntryByItemId(itemId)
    if not entry then
        return true
    end
    return entry.isStatic
end


_G.DomainShopUtils = DomainShopUtils
return DomainShopUtils