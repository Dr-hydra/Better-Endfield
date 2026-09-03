local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonPOIUpgrade
local PHASE_ID = PhaseId.CommonPOIUpgrade
CommonPOIUpgradeCtrl = HL.Class('CommonPOIUpgradeCtrl', uiCtrl.UICtrl)






CommonPOIUpgradeCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_DOMAIN_SHOP_CHANNEL_UNLOCK] = 'OnDomainShopChannelUnlock',
    [MessageConst.ON_DOMAIN_SHOP_CHANNEL_LEVEL_UP] = 'OnDomainShopChannelLevelUp',
    [MessageConst.ON_SIMULATION_TRAINING_LEVEL_UP] = 'OnSimulationTrainingLevelChange',
    [MessageConst.ON_SEWAGE_TREAT_PLANT_LEVEL_CHANGE] = 'OnSewageTreatPlantLevelChange',
    [MessageConst.ON_SQUAD_INFIGHT_CHANGED] = 'OnSquadInFightChanged',
}




local GetDataFunc = {
    [GEnums.DomainPoiType.DomainShop] = "_GetDataDomainShop",
    [GEnums.DomainPoiType.RecycleBin] = "_GetDataRecycleBin",
    [GEnums.DomainPoiType.KiteStation] = "_GetDataKiteStation",
    [GEnums.DomainPoiType.DomainDepot] = "_GetDataDomainDepot",
    [GEnums.DomainPoiType.SewageTreatPlant] = "_GetDataSewageTreatPlant",
    [GEnums.DomainPoiType.SimulationTraining] = "_GetDataSimulationTraining",
}


local InitEventFunc = {
    [GEnums.DomainPoiType.DomainShop] = "_InitEventDomainShop",
    [GEnums.DomainPoiType.RecycleBin] = "_InitEventRecycleBin",
    [GEnums.DomainPoiType.KiteStation] = "_InitEventKiteStation",
    [GEnums.DomainPoiType.DomainDepot] = "_InitEventDomainDepot",
    [GEnums.DomainPoiType.SewageTreatPlant] = "_InitEventSewageTreatPlant",
    [GEnums.DomainPoiType.SimulationTraining] = "_InitEventSimulationTraining",
}


local RefreshContentUIFunc = {
    [DomainPOIUtils.contentTypeEnum.CommonTitle] = "_RefreshContentUICommonTitle",
    [DomainPOIUtils.contentTypeEnum.ItemList] = "_RefreshContentUIItemList",
    [DomainPOIUtils.contentTypeEnum.TextImgText] = "_RefreshContentUITextImgText",
    [DomainPOIUtils.contentTypeEnum.RewardList] = "_RefreshContentUIRewardList",
    [DomainPOIUtils.contentTypeEnum.TitleWithText] = "_RefreshContentUITitleWithText",
    [DomainPOIUtils.contentTypeEnum.SewageTreatInfo] = "_RefreshContentUISewageTreatInfo",
}



CommonPOIUpgradeCtrl.m_args = HL.Field(HL.Any)

CommonPOIUpgradeCtrl.m_instId = HL.Field(HL.String) << ""

CommonPOIUpgradeCtrl.m_domainPOIType = HL.Field(GEnums.DomainPoiType)

CommonPOIUpgradeCtrl.m_info = HL.Field(HL.Table)

CommonPOIUpgradeCtrl.m_onClickUnlockBtn = HL.Field(HL.Function)

CommonPOIUpgradeCtrl.m_onClickUpgradeBtn = HL.Field(HL.Function)


CommonPOIUpgradeCtrl.m_descTxtCellCached = HL.Field(HL.Forward("UIListCache"))







CommonPOIUpgradeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self.m_args = arg
    local domainPOIType, instId = unpack(arg)
    if type(domainPOIType) == "number" or type(domainPOIType) == "string" then
        domainPOIType = GEnums.DomainPoiType.__CastFrom(domainPOIType)
    end

    self.m_domainPOIType = domainPOIType
    self.m_instId = instId

    self:_InitController()
    self:_UpdateData()
    self:_InitEvent()
    self:_RefreshAllUI()
end



CommonPOIUpgradeCtrl._InitEvent = HL.Method() << function(self)
    local funcName = InitEventFunc[self.m_domainPOIType]
    if not funcName then
        logger.error("[CommonPOIUpgradeCtrl] InitEventFunc定义缺失，类型为：", self.m_domainPOIType)
        return
    end
    self[funcName](self)
end

CommonPOIUpgradeCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.contentParent.selectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
    self.view.contentParent.selectableNaviGroup.enabled = false
end


CommonPOIUpgradeCtrl._InitEventDomainShop = HL.Method() << function(self)
    self.m_onClickUnlockBtn = function()
        logger.info("unlock shop channel")
        GameInstance.player.shopSystem:SendShopChannelLevelUp(self.m_info.domainShopGroupId, self.m_info.domainShopChannelId, 1)

    end

    self.m_onClickUpgradeBtn = function()
        logger.info("upgrade shop channel")
        GameInstance.player.shopSystem:SendShopChannelLevelUp(
            self.m_info.domainShopGroupId,
            self.m_info.domainShopChannelId,
            self.m_info.targetLevel
        )
    end
end

CommonPOIUpgradeCtrl._InitEventRecycleBin = HL.Method() << function(self)
    self.m_onClickUnlockBtn = function()
        GameInstance.player.recycleBinSystem:RecycleBinUnlock(self.m_instId)
    end

    self.m_onClickUpgradeBtn = function()
        GameInstance.player.recycleBinSystem:RecycleBinLevelUp(self.m_instId)
    end
end

CommonPOIUpgradeCtrl._InitEventKiteStation = HL.Method() << function(self)
    self.m_onClickUnlockBtn = function()
        GameInstance.player.kiteStationSystem:SendKiteStationUnlockOrLevelUp(self.m_instId)
    end

    self.m_onClickUpgradeBtn = function()
        GameInstance.player.kiteStationSystem:SendKiteStationUnlockOrLevelUp(self.m_instId)
    end
end

CommonPOIUpgradeCtrl._InitEventDomainDepot = HL.Method() << function(self)
    self.m_onClickUnlockBtn = function()
        GameInstance.player.domainDepotSystem:UnlockDomainDepot(self.m_instId)
    end

    self.m_onClickUpgradeBtn = function()
        GameInstance.player.domainDepotSystem:UpgradeDomainDepot(self.m_instId)
    end
end

CommonPOIUpgradeCtrl._InitEventSewageTreatPlant = HL.Method() << function(self)
    self.m_onClickUnlockBtn = function()
        GameInstance.player.remoteFactory.core:Message_SewageTreatPlantLevelUp(self.m_instId, 1)
    end

    self.m_onClickUpgradeBtn = function()
        local plantData = FactoryUtils.getSewageTreatPlantData(self.m_instId)
        GameInstance.player.remoteFactory.core:Message_SewageTreatPlantLevelUp(self.m_instId, plantData.currLevel + 1)
    end
end


CommonPOIUpgradeCtrl._InitEventSimulationTraining = HL.Method() << function(self)
    self.m_onClickUnlockBtn = function()
        GameInstance.player.simulationTrainingSystem:SimulationTrainingLevelUp()
    end

    self.m_onClickUpgradeBtn = function()
        GameInstance.player.simulationTrainingSystem:SimulationTrainingLevelUp()
    end
end






CommonPOIUpgradeCtrl.OnDomainShopChannelUnlock = HL.Method() << function(self)
    PhaseManager:ExitPhaseFast(PHASE_ID)
end

CommonPOIUpgradeCtrl.OnDomainShopChannelLevelUp = HL.Method() << function(self)
    PhaseManager:ExitPhaseFast(PHASE_ID)
end

CommonPOIUpgradeCtrl.OnSewageTreatPlantLevelChange = HL.Method() << function(self)
    PhaseManager:ExitPhaseFast(PHASE_ID)
end


CommonPOIUpgradeCtrl.OnSimulationTrainingLevelChange = HL.Method() << function(self)
    PhaseManager:ExitPhaseFast(PHASE_ID)
end








CommonPOIUpgradeCtrl._UpdateData = HL.Method() << function(self)
    local funcName = GetDataFunc[self.m_domainPOIType]
    if not funcName then
        logger.error("[CommonPOIUpgradeCtrl] GetDataFunc定义缺失，类型为：", self.m_domainPOIType)
        return
    end
    self.m_info = self[funcName](self)
end


CommonPOIUpgradeCtrl._GetDataDomainShop = HL.Method().Return(HL.Any) << function(self)
    local channelId = self.m_instId
    local info = DomainPOIUtils.GetPoiUpgradeCtrlInfo[GEnums.DomainPoiType.DomainShop](channelId, true)
    return info
end

CommonPOIUpgradeCtrl._GetDataRecycleBin = HL.Method().Return(HL.Any) << function(self)
    local info = DomainPOIUtils.getUpgradeCtrlArgsTemplate()
    local recycleBinId = self.m_instId

    local recycleBinCfg = Tables.recycleBinTable[recycleBinId]
    local domainId = recycleBinCfg.domainId
    local levelId = recycleBinCfg.levelId

    local isUnlock, recycleBinData = GameInstance.player.recycleBinSystem.recycleBins:TryGetValue(recycleBinId)
    local levelData = recycleBinCfg.levelData
    local descRawText = isUnlock and levelData[recycleBinData.lv].desc or recycleBinCfg.unlockDesc

    info.domainId = domainId
    info.levelId = levelId
    info.titleName = Language["ui_recycling_upgradepanel_title"]
    info.descList = string.isEmpty(descRawText) and {} or string.split(descRawText, "\n")

    info.upgradeCostMoney = isUnlock and levelData[recycleBinData.lv].lvUpCost or recycleBinCfg.unlockCost

    info.curLevel = isUnlock and recycleBinData.lv or 0
    info.targetLevel = info.curLevel + 1
    info.maxLevel = isUnlock and recycleBinData.maxLv or 5
    info.isFinalMaxLevel = true

    local getRewardItemsByRewardId = function(rewardId)
        local rewardCfg = Tables.rewardTable[rewardId]
        local rewardItems = {}
        for i = 0, rewardCfg.itemBundles.Count - 1 do
            local itemBundle = rewardCfg.itemBundles[i]
            table.insert(rewardItems, {
                id = itemBundle.id,
                count = itemBundle.count,
            })
        end
        return rewardItems
    end


    if isUnlock then
        if recycleBinData.lv >= recycleBinData.remoteCollectLv - 1 then
            local isNew = recycleBinData.lv == recycleBinData.remoteCollectLv - 1
            DomainPOIUtils.insertContentCommonTitle(info, "POI/icon_claim_resources", Language.LUA_RECYCLE_BIN_POI_REMOTE_COLLECT_TITLE, isNew)

            local _, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
            local domainName = domainCfg.domainName
            local remoteCollectContentText = string.format(Language.LUA_RECYCLE_BIN_POI_REMOTE_COLLECT_CONTENT, domainName, recycleBinData.remoteCollectLv, domainName, recycleBinData.remoteCollectLv)
            DomainPOIUtils.insertContentTextImgText(info, remoteCollectContentText, nil, 2, true)
        end
    end


    DomainPOIUtils.insertContentCommonTitle(info, "POI/icon_recycle", Language["ui_recycling_upgradepanel_collect_mention"])
    if isUnlock then
        local isMaxLv = recycleBinData.isMaxLv
        if isMaxLv then
            local rewardId = levelData[recycleBinData.lv].rewardId

            DomainPOIUtils.insertContentRewardList(info, getRewardItemsByRewardId(rewardId), "ProduceResources", false)
        else

            local curLv = recycleBinData.lv
            local rewardIdCur = levelData[curLv].rewardId
            local rewardItemsCur = getRewardItemsByRewardId(rewardIdCur)
            local rewardItemIdsCur = {}

            DomainPOIUtils.insertContentRewardList(info, rewardItemsCur, "CurrentResources", true)
            for _, rewardItem in ipairs(rewardItemsCur) do
                table.insert(rewardItemIdsCur, rewardItem.id)
            end

            local rewardIdNext = levelData[curLv + 1].rewardId
            local rewardItemsNext = getRewardItemsByRewardId(rewardIdNext)
            for _, rewardItem in ipairs(rewardItemsNext) do
                rewardItem.isNew = lume.find(rewardItemIdsCur, rewardItem.id) == nil
            end
            DomainPOIUtils.insertContentRewardList(info, rewardItemsNext, "UpgradeResources", false)
        end
    else
        local rewardId = levelData[1].rewardId
        local rewardList = getRewardItemsByRewardId(rewardId)

        DomainPOIUtils.insertContentRewardList(info, rewardList, "ProduceResources", false)
    end
    return info
end

CommonPOIUpgradeCtrl._GetDataKiteStation = HL.Method().Return(HL.Any) << function(self)
    local info = DomainPOIUtils.getUpgradeCtrlArgsTemplate()
    local kiteStationId = self.m_instId
    local cfg = Tables.kiteStationLevelTable[kiteStationId]
    local currentLevel = GameInstance.player.kiteStationSystem:GetKiteStationLevel(kiteStationId)


    if currentLevel == -1 then
        currentLevel = 1
        local currentLevelConfig = cfg.list[currentLevel]

        info.domainId = currentLevelConfig.domainId
        info.levelId = currentLevelConfig.levelId
        info.titleName = Language.LUA_KITE_STATION_TITLE
        info.descList = string.isEmpty(Language.LUA_KITE_STATION_DESC) and {} or string.split(Language.LUA_KITE_STATION_DESC, "\n")
        info.upgradeCostMoney = currentLevelConfig.costItemCount[0]

        info.curLevel = 0
        info.targetLevel = 1
        info.maxLevel = #cfg.list
        info.isFinalMaxLevel = Tables.GlobalConst.kiteStationMaxLevel == info.maxLevel

        DomainPOIUtils.insertContentTitleWithText(info, currentLevelConfig.levelTitle, currentLevelConfig.levelDesc)

        return info
    end

    local currentLevelConfig = nil
    if currentLevel + 1 <= #cfg.list then
        local nextLevelConfig = cfg.list[currentLevel + 1]
        info.upgradeCostMoney = nextLevelConfig.costItemCount[0]
        currentLevelConfig = cfg.list[currentLevel + 1]
    else
        currentLevelConfig = cfg.list[currentLevel]
    end

    info.domainId = currentLevelConfig.domainId
    info.levelId = currentLevelConfig.levelId
    info.titleName = Language.LUA_KITE_STATION_TITLE
    info.descList = string.isEmpty(Language.LUA_KITE_STATION_DESC) and {} or string.split(Language.LUA_KITE_STATION_DESC, "\n")

    info.curLevel = currentLevel
    info.targetLevel = currentLevel + 1
    info.maxLevel = DomainPOIUtils.GetKiteStationMaxLevel(kiteStationId)
    info.isFinalMaxLevel = Tables.GlobalConst.kiteStationMaxLevel == info.maxLevel

    DomainPOIUtils.insertContentTitleWithText(info, currentLevelConfig.levelTitle, currentLevelConfig.levelDesc)
    return info
end

CommonPOIUpgradeCtrl._GetDataDomainDepot = HL.Method().Return(HL.Any) << function(self)
    local info = DomainPOIUtils.getUpgradeCtrlArgsTemplate()
    local domainDepotId = self.m_instId
    local domainDepotData = GameInstance.player.domainDepotSystem:GetDomainDepotDataById(domainDepotId)
    local depotSuccess, domainDepotCfg = Tables.domainDepotTable:TryGetValue(domainDepotId)
    local levelSuccess, domainDepotLevelList = Tables.domainDepotLevelTable:TryGetValue(domainDepotId)
    if not levelSuccess or not depotSuccess or domainDepotData == nil then
        return info
    end

    info.domainId = domainDepotCfg.domainId
    info.levelId = domainDepotCfg.refLevelId
    info.titleName = Language.LUA_DOMAIN_DEPOT_TITLE

    domainDepotLevelList = domainDepotLevelList.levelList
    local curLevel = domainDepotData.level
    local curMaxLevel = DomainPOIUtils.GetDomainDepotMaxLevel(domainDepotId)
    local targetLevel = math.min(curLevel + 1, curMaxLevel)
    local isCurMaxLevel = curLevel == curMaxLevel
    info.curLevel = curLevel
    info.targetLevel = targetLevel
    info.maxLevel = curMaxLevel
    if curLevel == 0 then
        info.isFinalMaxLevel = false
    else
        local curLevelConfig = domainDepotLevelList[curLevel]
        info.isFinalMaxLevel = curLevelConfig.isFinalMaxLevel
    end

    local arrowPath = UIConst.UI_SPRITE_COMMON .. "/deco_common_arrow"


    if curLevel == 0 then
        info.descList = string.split(domainDepotCfg.depotDesc, "\n")
    else
        local curLevelConfig = domainDepotLevelList[curLevel]
        info.descList = string.split(curLevelConfig.levelDesc, "\n")
    end

    if isCurMaxLevel then
        local curLevelConfig = domainDepotLevelList[curLevel]

        DomainPOIUtils.insertContentCommonTitle(
            info,
            UIConst.UI_SPRITE_DOMAIN_DEPOT_UPGRADE .. "/" .. "shop_market_tab_icon_small_normal",
            Language.LUA_DOMAIN_DEPOT_TITLE
        )
        DomainPOIUtils.insertContentTextImgText(info, Language.LUA_DOMAIN_DEPOT_CONTENT_TITLE_MAX_EXTRA_LIMIT, {
            {
                text1 = tostring(curLevelConfig.extraDepotLimit),
            }
        }, 2)


        DomainPOIUtils.insertContentCommonTitle(
            info,
            UIConst.UI_SPRITE_DOMAIN_DEPOT_UPGRADE .. "/" .. "shop_market_tab_icon_small_special",
            Language.LUA_DOMAIN_DEPOT_SUB_TITLE_DELIVER
        )

        local itemDescList = {}
        for index = 0, curLevelConfig.deliverItemTypeList.Count - 1 do
            local itemType = curLevelConfig.deliverItemTypeList[index]
            local itemTypeData = Tables.domainDepotDeliverItemTypeTable[itemType]
            table.insert(itemDescList, {
                text1 = nil,
                icon = UIConst.UI_SPRITE_INVENTORY .. "/" .. itemTypeData.typeIcon,
                text2 = itemTypeData.typeDesc,
                fontSizeLevel = 2,
            })
        end
        DomainPOIUtils.insertContentTextImgText(info, Language.LUA_DOMAIN_DEPOT_CONTENT_TITLE_MAX_DELIVER_ITEM, itemDescList, 2)

        local maxPackType
        for index = 0, curLevelConfig.deliverPackTypeList.Count - 1 do
            local packType = curLevelConfig.deliverPackTypeList[index]
            if maxPackType == nil or maxPackType:GetHashCode() < packType:GetHashCode() then
                maxPackType = packType
            end
        end
        local packTypeData = Tables.domainDepotDeliverPackTypeTable[maxPackType]
        DomainPOIUtils.insertContentTextImgText(info, string.format(Language.LUA_DOMAIN_DEPOT_CONTENT_TITLE_MAX_DELIVER_PACK, packTypeData.typeDesc), nil, 2)
    else

        local curExtraDepotLimit, targetExtraDepotLimit
        local curDeliverItemTypeCount, targetDeliverItemTypeCount = 0, 0
        if curLevel == 0 then
            curExtraDepotLimit = 0
        else
            local curLevelConfig = domainDepotLevelList[curLevel]
            curExtraDepotLimit = curLevelConfig.extraDepotLimit
            curDeliverItemTypeCount = curLevelConfig.deliverItemTypeList.Count
        end
        local targetLevelCfg = domainDepotLevelList[targetLevel]
        targetExtraDepotLimit = targetLevelCfg.extraDepotLimit
        targetDeliverItemTypeCount = targetLevelCfg.deliverItemTypeList.Count
        info.upgradeCostMoney = targetLevelCfg.costDomainMoney
        if curExtraDepotLimit < targetExtraDepotLimit then
            DomainPOIUtils.insertContentCommonTitle(
                info,
                UIConst.UI_SPRITE_DOMAIN_DEPOT_UPGRADE .. "/" .. "shop_market_tab_icon_small_normal",
                Language.LUA_DOMAIN_DEPOT_TITLE
            )
            DomainPOIUtils.insertContentTextImgText(info, Language.LUA_DOMAIN_DEPOT_CONTENT_TITLE_EXTRA_LIMIT, {
                {
                    text1 = tostring(curExtraDepotLimit),
                    icon = arrowPath,
                    text2 = tostring(targetExtraDepotLimit),
                }
            }, 2)
        end


        local needShowDeliverDesc = curDeliverItemTypeCount <= 0 and targetDeliverItemTypeCount > 0
        local needShowDeliverItemTypeDesc = not string.isEmpty(targetLevelCfg.upgradeDeliverItemTypeDesc)
        local needShowDeliverPackTypeDesc = not string.isEmpty(targetLevelCfg.upgradeDeliverPackTypeDesc)
        local neeShowDeliverContent = needShowDeliverDesc or needShowDeliverItemTypeDesc or needShowDeliverPackTypeDesc
        if neeShowDeliverContent then
            DomainPOIUtils.insertContentCommonTitle(
                info,
                UIConst.UI_SPRITE_DOMAIN_DEPOT_UPGRADE .. "/" .. "shop_market_tab_icon_small_special",
                Language.LUA_DOMAIN_DEPOT_SUB_TITLE_DELIVER
            )

            if needShowDeliverDesc then
                DomainPOIUtils.insertContentTextImgText(info, Language.LUA_DOMAIN_DEPOT_CONTENT_TITLE_DELIVER, {
                    {
                        text1 = Language.LUA_DOMAIN_DEPOT_CONTENT_DESC_DELIVER,
                        fontSizeLevel = 2,
                    }
                }, 2)
            end

            if needShowDeliverItemTypeDesc then
                DomainPOIUtils.insertContentTextImgText(info, targetLevelCfg.upgradeDeliverItemTypeDesc, nil, 2)
            end

            if needShowDeliverPackTypeDesc then
                DomainPOIUtils.insertContentTextImgText(info, targetLevelCfg.upgradeDeliverPackTypeDesc, nil, 2)
            end
        end
    end


    if curLevel == 0 then
        local unlockAvailable = GameInstance.player.domainDepotSystem:IsDomainDepotUnlockAvailable(domainDepotId)
        if not unlockAvailable then
            info.showJumpToTask = true
            info.upgradeQuestId = domainDepotCfg.unlockQuestId
            info.upgradeQuestDesc = domainDepotCfg.unlockQuestDesc
        end
    end

    return info
end

CommonPOIUpgradeCtrl._GetDataSewageTreatPlant = HL.Method().Return(HL.Any) << function(self)
    local info = DomainPOIUtils.getUpgradeCtrlArgsTemplate()
    local domainData = FactoryUtils.getSewageTreatPlantData(self.m_instId)

    info.domainId = domainData.domainId
    info.levelId = domainData.levelId
    info.curLevel = domainData.currLevel
    info.targetLevel = domainData.nextLevel
    info.maxLevel = domainData.maxLevel
    info.isFinalMaxLevel = domainData.isFinalMaxLevel
    info.upgradeCostMoney = domainData.levelCost
    if info.curLevel == 0 then
        info.descList = { Language.LUA_FAC_SEWAGE_TREAT_LOCK_DESC_TEXT }
    else
        info.descList = string.split(domainData.currLevelDesc, "\n")
    end

    DomainPOIUtils.insertContentCommonTitle(info, "POI/icon_recycle", domainData.nextLevelTitle)

    if domainData.currImportCount > 0 or domainData.nextImportCount > 0 then
        local isImportMax = domainData.currImportCount == domainData.maxImportCount
        DomainPOIUtils.insertContentSewageTreatInfo(info, true, isImportMax, domainData.currImportCount, domainData.nextImportCount)
    end
    if domainData.currExportCount > 0 or domainData.nextExportCount > 0 then
        local isExportMax = domainData.currExportCount == domainData.maxExportCount
        DomainPOIUtils.insertContentSewageTreatInfo(info, false, isExportMax, domainData.currExportCount, domainData.nextExportCount)
    end
    if domainData.currImportCount > 0 or domainData.nextImportCount > 0 then
        DomainPOIUtils.insertContentTitleWithText(info, domainData.importerDesc)
    end
    if domainData.currExportCount > 0 or domainData.nextExportCount > 0 then
        DomainPOIUtils.insertContentTitleWithText(info, domainData.exporterDesc)
    end

    return info
end

CommonPOIUpgradeCtrl._GetDataSimulationTraining = HL.Method().Return(HL.Any) << function(self)
    local simulationTrainingSystem = GameInstance.player.simulationTrainingSystem
    local curLevel = simulationTrainingSystem.curLevel
    local hasCfg, curLevelData
    if curLevel == 0 then
        hasCfg = true
        curLevelData = {}
    else
        hasCfg, curLevelData = Tables.simulationTrainingLevelTable:TryGetValue(curLevel)
    end

    local info = DomainPOIUtils.getUpgradeCtrlArgsTemplate()
    if not simulationTrainingSystem or not hasCfg then
        return info
    end

    info.domainId = Tables.simulationTrainingConst.domainId
    info.levelId = Tables.simulationTrainingConst.simulationTrainingRefLevelId
    info.titleName = Language.LUA_SIMULATION_TRAINING_TITLE

    local curMaxLevel = simulationTrainingSystem.maxLevel
    local targetLevel = math.min(curLevel + 1, curMaxLevel)
    local isCurMaxLevel = curLevel == curMaxLevel
    info.curLevel = curLevel
    info.targetLevel = targetLevel
    info.maxLevel = curMaxLevel
    if curLevel == 0 then
        info.isFinalMaxLevel = false
    else
        info.isFinalMaxLevel = curLevel == curMaxLevel
    end

    local arrowPath = UIConst.UI_SPRITE_COMMON .. "/deco_common_arrow"

    local curMaxPointAward = 0
    local curDoubleLimit = 0
    if curLevel == 0 then
        info.descList = string.split(Language.LUA_SIMULATION_TRAINING_ZERO_LEVEL_DESC, "\n")
    else
        curDoubleLimit = curLevelData.doubleLimit
        info.descList = string.split(curLevelData.desc, "\n")
        if curLevelData.pointAward.Count > 0 then
            curMaxPointAward = curLevelData.pointAward[curLevelData.pointAward.Count - 1]
        else
            curMaxPointAward = 0
        end

    end
    local hasTargetLevelCfg, targetLevelData = Tables.simulationTrainingLevelTable:TryGetValue(targetLevel)
    local targetMaxPointAward = 0
    local targetDoubleLimit = targetLevelData.doubleLimit
    if targetLevelData.pointAward.Count > 0 then
        targetMaxPointAward = targetLevelData.pointAward[targetLevelData.pointAward.Count - 1]
    end

    info.upgradeCostMoney = targetLevelData.costDomainMoney

    local pointAwardTitleIcon = UIConst.UI_SPRITE_DOMAIN_DEPOT_UPGRADE .. "/" .. "simulation_training_bonus_icon"
    local doubleTitleIcon = UIConst.UI_SPRITE_DOMAIN_DEPOT_UPGRADE .. "/" .. "simulation_training_num_icon"
    local unlimitedModeTitleIcon = UIConst.UI_SPRITE_DOMAIN_DEPOT_UPGRADE .. "/" .. "simulation_training_unlock_icon"
    local tipColor = "fffc00"
    local baseColor = "ffffff"

    if isCurMaxLevel then
        local rewardTitleText = Language.LUA_SIMULATION_TRAINING_TIP_REWARD_CONTENT_NORMAL
        local rewardContentList = {
            text1 = curMaxPointAward,
            text1Color = UIUtils.getColorByString(tipColor),
        }
        DomainPOIUtils.insertContentCommonTitle(
            info,
            pointAwardTitleIcon,
            Language.LUA_SIMULATION_TRAINING_LEVEL_UP_TIP_REWARD_TITLE
        )
        DomainPOIUtils.insertContentTextImgText(info, rewardTitleText, { rewardContentList }, 2)

        local doubleTitleText = Language.LUA_SIMULATION_TRAINING_TIP_DOUBLE_CONTENT_NORMAL
        local doubleContentList = {
            text1 = curDoubleLimit,
            text1Color = UIUtils.getColorByString(tipColor),
        }
        DomainPOIUtils.insertContentCommonTitle(
            info,
            doubleTitleIcon,
            Language.LUA_SIMULATION_TRAINING_LEVEL_UP_TIP_DOUBLE_TITLE
        )
        DomainPOIUtils.insertContentTextImgText(info, doubleTitleText, { doubleContentList }, 2)
    else
        local showPointAward = false
        local rewardTitleText = ""
        local rewardContentList = {}
        if curMaxPointAward == targetMaxPointAward then
            if curMaxPointAward ~= 0 then
                showPointAward = true
                rewardTitleText = Language.LUA_SIMULATION_TRAINING_TIP_REWARD_CONTENT_NORMAL
                rewardContentList = {
                    text1 = curMaxPointAward,
                    text1Color = UIUtils.getColorByString(tipColor),
                }
            end
        else
            showPointAward = true
            rewardTitleText = Language.LUA_SIMULATION_TRAINING_TIP_REWARD_CONTENT_LEVEL_UP
            rewardContentList = {
                text1 = curMaxPointAward,
                text1Color = UIUtils.getColorByString(baseColor),
                icon = arrowPath,
                iconColor = UIUtils.getColorByString(tipColor),
                text2 = targetMaxPointAward,
                text2Color = UIUtils.getColorByString(tipColor),
            }
        end

        if showPointAward then
            DomainPOIUtils.insertContentCommonTitle(
                info,
                pointAwardTitleIcon,
                Language.LUA_SIMULATION_TRAINING_LEVEL_UP_TIP_REWARD_TITLE
            )
            DomainPOIUtils.insertContentTextImgText(info, rewardTitleText, { rewardContentList }, 2)
        end

        local showDouble = false
        local doubleTitleText = ""
        local doubleContentList = {}
        if curDoubleLimit == targetDoubleLimit then
            if curDoubleLimit ~= 0 then
                showDouble = true
                doubleTitleText = Language.LUA_SIMULATION_TRAINING_TIP_DOUBLE_CONTENT_NORMAL
                doubleContentList = {
                    text1 = curDoubleLimit,
                    text1Color = UIUtils.getColorByString(tipColor),
                }
            end
        else
            showDouble = true
            doubleTitleText = Language.LUA_SIMULATION_TRAINING_TIP_DOUBLE_CONTENT_LEVEL_UP
            doubleContentList = {
                text1 = curDoubleLimit,
                text1Color = UIUtils.getColorByString(baseColor),
                icon = arrowPath,
                iconColor = UIUtils.getColorByString(tipColor),
                text2 = targetDoubleLimit,
                text2Color = UIUtils.getColorByString(tipColor),
            }
        end

        if showDouble then
            DomainPOIUtils.insertContentCommonTitle(
                info,
                doubleTitleIcon,
                Language.LUA_SIMULATION_TRAINING_LEVEL_UP_TIP_DOUBLE_TITLE
            )
            DomainPOIUtils.insertContentTextImgText(info, doubleTitleText, { doubleContentList }, 2)
        end
    end


    if curLevel == 0 then
        local questId = Tables.simulationTrainingConst.unlockQuestId
        if not GameInstance.player.mission:IsQuestCompleted(questId) then
            info.showJumpToTask = true
            info.upgradeQuestId = questId
            info.upgradeQuestDesc = Language.LUA_SIMULATION_TRAINING_QUEST_UNLOCK_DESC
            info.jumpTaskToast = Language.LUA_SIMULATION_TRAINING_QUEST_UNLOCK_TOAST
        end
    elseif curLevel == 1 then
        local questId = Tables.simulationTrainingConst.unlockLevel2QuestId
        if not GameInstance.player.mission:IsQuestCompleted(questId) then
            info.showJumpToTask = true
            info.upgradeQuestId = questId
            info.upgradeQuestDesc = Language.LUA_SIMULATION_TRAINING_UNLOCK_LEVEL_DESC_1_2
            info.jumpTaskToast = Language.LUA_SIMULATION_TRAINING_UNLOCK_LEVEL_TOAST_1_2
        end
    end

    return info

end





CommonPOIUpgradeCtrl._InitUI = HL.Method() << function(self)
    self.view.domainTopMoneyTitle.view.closeBtn.onClick:AddListener(function()
        self:_CloseSelf()
    end)

    self.m_descTxtCellCached = UIUtils.genCellCache(self.view.descTxtCell)

    local operateGroup = self.view.operateGroup
    operateGroup.unlockBtn.onClick:AddListener(function()
        if self.m_onClickUnlockBtn then
            self.m_onClickUnlockBtn()
        end
    end)
    operateGroup.upgradeBtn.onClick:AddListener(function()
        if self.m_onClickUpgradeBtn then
            self.m_onClickUpgradeBtn()
        end
    end)
    operateGroup.jumpTaskBtn.onClick:AddListener(function()
        if self.m_info.questState == CS.Beyond.Gameplay.MissionSystem.QuestState.Processing
            or self.m_info.questState == CS.Beyond.Gameplay.MissionSystem.QuestState.Paused
        then
            PhaseManager:OpenPhase(PhaseId.Mission, {
                autoSelect = self.m_info.upgradeMissionId
            })
        else
            if not string.isEmpty(self.m_info.jumpTaskToast) then
                Notify(MessageConst.SHOW_TOAST, self.m_info.jumpTaskToast)
            else
                Notify(MessageConst.SHOW_TOAST, Language.LUA_POI_UPGRADE_NEED_COMPLETE_TASK)
            end
        end
    end)

    local contentParent = self.view.contentParent
    contentParent.poiUpgradeContentCommonTitle.gameObject:SetActive(false)
    contentParent.poiUpgradeContentItemList.gameObject:SetActive(false)
    contentParent.poiUpgradeContentRewardList.gameObject:SetActive(false)
    contentParent.poiUpgradeContentTextImgText.gameObject:SetActive(false)
    contentParent.poiUpgradeContentTitleWithText.gameObject:SetActive(false)
end

CommonPOIUpgradeCtrl._RefreshAllUI = HL.Method() << function(self)

    local info = self.m_info
    if info == nil then
        return
    end
    self:_RefreshBasicUI()
    self:_RefreshContentUI()
end

CommonPOIUpgradeCtrl._RefreshBasicUI = HL.Method() << function(self)

    local info = self.m_info
    local _, domainCfg = Tables.domainDataTable:TryGetValue(info.domainId)
    local moneyId = domainCfg.domainGoldItemId

    local titleNode = self.view.titleNode
    titleNode.mapNameTxt.text = Tables.levelDescTable[info.levelId].showName
    if string.isEmpty(info.titleName) then
        info.titleName = Tables.domainPoiTable[self.m_domainPOIType].name
    end
    titleNode.titleTxt.text = info.titleName
    titleNode.curLvTxt.text = info.curLevel
    titleNode.maxLvTxt.text = info.maxLevel
    if info.curLevel >= info.maxLevel then
        titleNode.stateCtrl:SetState(info.isFinalMaxLevel and "Max" or "CurVersionMax")
    elseif info.curLevel == 0 then
        titleNode.stateCtrl:SetState("Lock")
    else
        titleNode.stateCtrl:SetState("Upgrade")
    end


    self.m_descTxtCellCached:Refresh(#info.descList, function(cell, luaIndex)
        local text = info.descList[luaIndex]
        cell.txt.text = text
    end)

    local operateGroup = self.view.operateGroup
    operateGroup.moneyConsumeTxt.text = info.upgradeCostMoney
    local itemData = Tables.itemTable:GetValue(moneyId)
    operateGroup.consumeMoneyImg:LoadSprite(UIConst.UI_SPRITE_WALLET, itemData.iconId)
    if string.isEmpty(info.upgradeQuestId) then
        info.questState = CS.Beyond.Gameplay.MissionSystem.QuestState.Completed
    else
        info.questState = GameInstance.player.mission:GetQuestState(info.upgradeQuestId)
    end
    if info.questState ~= CS.Beyond.Gameplay.MissionSystem.QuestState.Completed then
        operateGroup.stateCtrl:SetState("JumpTask")
        operateGroup.jumpTaskTxt.text = info.upgradeQuestDesc
        info.upgradeMissionId = GameInstance.player.mission:GetMissionIdByQuestId(info.upgradeQuestId)
    else
        if info.curLevel >= info.maxLevel then

            operateGroup.stateCtrl:SetState(info.isFinalMaxLevel and "Empty" or "CurVersionMax")
        else

            local curMoneyCount = Utils.getItemCount(moneyId)
            local isMoneyNotEnough = curMoneyCount < info.upgradeCostMoney
            if isMoneyNotEnough then

                operateGroup.stateCtrl:SetState("MoneyNotEnough")
                local hasCfg, itemCfg = Tables.itemTable:TryGetValue(moneyId)
                if hasCfg then
                    operateGroup.promptTxt.text = string.format(Language.LUA_POI_UPGRADE_MONEY_NOT_ENOUGH, itemCfg.name)
                end
            elseif info.curLevel == 0 then

                operateGroup.stateCtrl:SetState("CanUnlock")
            else

                operateGroup.stateCtrl:SetState("CanUpgrade")
            end
        end
    end

    self.view.domainTopMoneyTitle:InitDomainTopMoneyTitle(info.domainId)
end

CommonPOIUpgradeCtrl._RefreshContentUI = HL.Method() << function(self)

    local info = self.m_info
    local count = #info.contentInfoList
    if count <= 0 then
        self.view.contentStateCtrl:SetState("NoContent")
        return
    end
    self.view.contentStateCtrl:SetState("HasContent")

    self.view.contentTitleNode.curLvTxt.text = info.curLevel
    self.view.contentTitleNode.targetLvTxt.text = info.targetLevel

    for _, contentInfo in pairs(info.contentInfoList) do
        local funcName = RefreshContentUIFunc[contentInfo.contentType]
        if funcName then
            self[funcName](self, contentInfo)
        else
            logger.error("[CommonPOIUpgradeCtrl] RefreshContentUIFunc定义缺失，类型为：", contentInfo.contentType)
        end
    end
end


CommonPOIUpgradeCtrl._RefreshContentUICommonTitle = HL.Method(HL.Table) << function(self, info)

    local cell = CommonPOIUpgradeCtrl._GenCacheContent(self.view.contentParent.poiUpgradeContentCommonTitle.gameObject, self.view.contentParent.gameObject)
    cell.titleIconImg:LoadSprite(info.icon)
    cell.titleTxt.text = info.titleName
    cell.stateController:SetState(info.isNew and "New" or "Normal")
end

CommonPOIUpgradeCtrl._RefreshContentUIItemList = HL.Method(HL.Table) << function(self, info)

    local cell = CommonPOIUpgradeCtrl._GenCacheContent(self.view.contentParent.poiUpgradeContentItemList.gameObject, self.view.contentParent.gameObject)
    if info.title ~= nil then
        cell.itemListTitleTxt.text = info.title
    end
    local genCellFunc = UIUtils.genCachedCellFunction(cell.itemScrollList)
    cell.itemScrollList.onUpdateCell:AddListener(function(obj, csIndex)

        local itemCell = genCellFunc(obj)
        local luaIndex = LuaIndex(csIndex)
        itemCell:InitItem(info.itemBundleList[luaIndex], true)
        itemCell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
    end)
    cell.itemScrollList:UpdateCount(#info.itemBundleList)
    if info.useNaviGroup then
        self.view.contentParent.selectableNaviGroup.enabled = true
    end
end

CommonPOIUpgradeCtrl._RefreshContentUITextImgText = HL.Method(HL.Table) << function(self, info)

    local cell = CommonPOIUpgradeCtrl._GenCacheContent(self.view.contentParent.poiUpgradeContentTextImgText.gameObject, self.view.contentParent.gameObject)
    cell.titleTxt:SetAndResolveTextStyle(info.title)
    if not info.indentLevel or info.indentLevel < 1 then
        cell.typesetStateCtrl:SetState("Normal")
    else
        cell.typesetStateCtrl:SetState("IndentLevel1")
    end
    cell.typesetStateCtrl:SetState(info.noDeco and "NoDeco" or "Deco")


    local cellCached = UIUtils.genCellCache(cell.contentCell)

    if info.contentList == nil then
        return
    end
    cellCached:Refresh(#info.contentList, function(contentCell, luaIndex)
        local contentInfo = info.contentList[luaIndex]
        if not contentInfo.fontSizeLevel or contentInfo.fontSizeLevel <= 1 then
            contentCell.txtFontSizeStateCtrl:SetState("FontLevel1")
        else
            contentCell.txtFontSizeStateCtrl:SetState("FontLevel2")
        end

        if string.isEmpty(contentInfo.text1) then
            contentCell.txt1.gameObject:SetActive(false)
        else
            contentCell.txt1.gameObject:SetActive(true)
            contentCell.txt1.text = contentInfo.text1
            if contentInfo.text1Color ~= nil and not string.isEmpty(contentInfo.text1Color) then
                contentCell.txt1.color = contentInfo.text1Color
            end
        end
        if string.isEmpty(contentInfo.icon) then
            contentCell.iconImg.gameObject:SetActive(false)
        else
            contentCell.iconImg.gameObject:SetActive(true)
            contentCell.iconImg:LoadSprite(contentInfo.icon)
            if contentInfo.iconColor ~= nil and not string.isEmpty(contentInfo.iconColor) then
                contentCell.iconImg.color = contentInfo.iconColor
            end
        end
        if string.isEmpty(contentInfo.text2) then
            contentCell.txt2.gameObject:SetActive(false)
        else
            contentCell.txt2.gameObject:SetActive(true)
            contentCell.txt2.text = contentInfo.text2
            if contentInfo.text2Color ~= nil and not string.isEmpty(contentInfo.text2Color) then
                contentCell.txt2.color = contentInfo.text2Color
            end
        end
    end)
end

CommonPOIUpgradeCtrl._RefreshContentUIRewardList = HL.Method(HL.Table) << function(self, info)

    local cell = CommonPOIUpgradeCtrl._GenCacheContent(self.view.contentParent.poiUpgradeContentRewardList.gameObject, self.view.contentParent.gameObject)
    cell.arrowStateCtrl:SetState(info.hasArrow and "Arrow" or "NoArrow")
    cell.tagStateCtrl:SetState(info.tagStateName)
    local genCellFunc = UIUtils.genCachedCellFunction(cell.itemScrollList)
    cell.itemScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        local rewardCell = genCellFunc(obj)
        local luaIndex = LuaIndex(csIndex)
        local itemInfo = info.itemBundleList[luaIndex]
        rewardCell.newTag.gameObject:SetActive(itemInfo.isNew == true)

        local item = rewardCell.itemSmall
        item:InitItem(itemInfo, true)
        item:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
    end)
    cell.itemScrollList:UpdateCount(#info.itemBundleList)

    if info.useNaviGroup then
        self.view.contentParent.selectableNaviGroup.enabled = true
    end
end

CommonPOIUpgradeCtrl._RefreshContentUITitleWithText = HL.Method(HL.Table) << function(self, info)

    local cell = CommonPOIUpgradeCtrl._GenCacheContent(self.view.contentParent.poiUpgradeContentTitleWithText.gameObject, self.view.contentParent.gameObject)
    cell.titleTxt.text = info.title
    if string.isEmpty(info.contentText) then
        cell.contentTxt.gameObject:SetActive(false)
    else
        cell.contentTxt.gameObject:SetActive(true)
        cell.contentTxt.text = info.contentText
    end
end

CommonPOIUpgradeCtrl._RefreshContentUISewageTreatInfo = HL.Method(HL.Table) << function(self, info)
    local cell = CommonPOIUpgradeCtrl._GenCacheContent(self.view.contentParent.poiUpgradeSewageTreatLevelInfo.gameObject, self.view.contentParent.gameObject)
    cell.nodeTypeCtrl:SetState(info.isImporter and "Import" or "Export")
    cell.maxStateCtrl:SetState(info.isMaxLv and "Max" or "Normal")
    cell.currLevelNumTxt.text = string.format("%d", info.currCount)
    if not info.isMaxLv then
        cell.upLevelNumTxt.text = string.format("%d", info.nextCount)
    end
end






CommonPOIUpgradeCtrl._GenCacheContent = HL.StaticMethod(GameObject, GameObject).Return(HL.Table) << function(templateObj, parent)
    local child = UIUtils.addChild(parent, templateObj, true)
    child.gameObject:SetActive(true)
    return Utils.wrapLuaNode(child)
end

CommonPOIUpgradeCtrl.OnSquadInFightChanged = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    local inFight = unpack(args)
    if inFight then
        self:_CloseSelf(true)
    end
end

CommonPOIUpgradeCtrl._CloseSelf = HL.Method(HL.Opt(HL.Boolean)) << function(self, isFast)
    AudioManager.PostEvent("Au_UI_Popup_DetailsPanel_Close")
    Notify(MessageConst.DIALOG_CHANGE_NEXT_INDEX, { phaseId = PHASE_ID, nextIndex = 0, })
    if isFast then
        Notify(MessageConst.HIDE_ITEM_TIPS)
        PhaseManager:ExitPhaseFast(PHASE_ID)
    else
        PhaseManager:PopPhase(PHASE_ID)
    end
end


HL.Commit(CommonPOIUpgradeCtrl)
