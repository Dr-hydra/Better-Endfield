local EquipTechUtils = {}


local defaultFormulaChainMap = nil

local chainCostMap = nil

local appendGeneralGoldEquipSet = nil

local hasShowAppendGeneralGoldEquip = false




function EquipTechUtils.hasEquipSuit(equipTemplateId)
    local hasValue, equipTemplate = Tables.equipTable:TryGetValue(equipTemplateId)
    if not hasValue then
        return false
    end
    local suitId = equipTemplate.suitID
    local hasSuit, _ = Tables.equipSuitTable:TryGetValue(suitId)
    return hasSuit
end




function EquipTechUtils.canEquipEnhance(templateId)
    local _, itemData = Tables.itemTable:TryGetValue(templateId)
    if itemData and itemData.rarity == Tables.equipConst.enhanceEquipRarity then
        return true
    end
    return false
end




function EquipTechUtils.getEquipEnhanceItemList(partType)

    local itemList = {}

    local equipDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.Equip]:GetOrFallback(Utils.getCurrentScope())
    if equipDepot then
        for _, itemBundle in cs_pairs(equipDepot.instItems) do
            local equipInst = itemBundle.instData
            local templateId = equipInst.templateId

            local _, equipData = Tables.equipTable:TryGetValue(templateId)

            if equipData and (not partType or equipData.partType == partType) and EquipTechUtils.canEquipEnhance(templateId) then
                table.insert(itemList, itemBundle)
            end
        end
    end
    return itemList
end






function EquipTechUtils.getEquipEnhanceMaterialsItemList(partType, attrShowInfo, equipInstId)

    local itemList = {}


    local equipDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.Equip]:GetOrFallback(Utils.getCurrentScope())
    if equipDepot then
        for _, itemBundle in cs_pairs(equipDepot.instItems) do
            local equipInstData = itemBundle.instData
            local templateId = equipInstData.templateId
            local _, equipData = Tables.equipTable:TryGetValue(templateId)


            if not equipInstData.isLocked and equipInstData.instId ~= equipInstId and equipData and
                equipData.partType == partType and EquipTechUtils.canEquipEnhance(templateId) then
                if EquipTechUtils.getEquipEnhanceSuccessProbability(equipInstData, attrShowInfo) >
                    EquipTechConst.EEquipEnhanceSuccessProb.None then
                    table.insert(itemList, itemBundle)
                end
            end
        end
    end

    return itemList
end










function EquipTechUtils.getEquipEnhanceMaterialsGroups(partType, attrShowInfo, equipInstId)
    local group = {
        title = Language.LUA_EQUIP_ENHANCE_MATERIALS_TITLE,
        itemList = {},
    }
    local enhancedGroup = {
        title = Language.LUA_EQUIP_ENHANCE_MATERIALS_ENHANCED_TITLE,
        itemList = {},
    }
    local extraArg = {
        attrShowInfo = attrShowInfo,
    }

    local equipDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.Equip]:GetOrFallback(Utils.getCurrentScope())
    if equipDepot then
        for _, itemBundle in cs_pairs(equipDepot.instItems) do
            local equipInstData = itemBundle.instData
            local templateId = equipInstData.templateId
            local instId = equipInstData.instId
            local _, equipData = Tables.equipTable:TryGetValue(templateId)


            if not equipInstData.isLocked and instId ~= equipInstId and equipInstData.equippedCharServerId == 0 and
                equipData and equipData.partType == partType and EquipTechUtils.canEquipEnhance(templateId) and
                EquipTechUtils.getEquipEnhanceSuccessProbability(equipInstData, attrShowInfo) > EquipTechConst.EEquipEnhanceSuccessProb.None then
                local itemInfo = FilterUtils.processEquipEnhanceMaterial(templateId, instId, extraArg)
                if equipInstData:HasAnyEnhanceRecord() then
                    table.insert(enhancedGroup.itemList, itemInfo)
                else
                    table.insert(group.itemList, itemInfo)
                end
            end
        end
    end
    local sortFunc = Utils.genSortFunction(EquipTechConst.EQUIP_ENHANCE_MATERIALS_SORT_OPTION[1].keys, false)
    table.sort(group.itemList, sortFunc)
    table.sort(enhancedGroup.itemList, sortFunc)
    local groups = {}
    if #group.itemList > 0 then
        table.insert(groups, group)
    end
    if #enhancedGroup.itemList > 0 then
        table.insert(groups, enhancedGroup)
    end
    return groups
end





function EquipTechUtils.getEquipEnhanceSuccessProbability(equipInstData, attrShowInfo)
    if not equipInstData then
        return EquipTechConst.EEquipEnhanceSuccessProb.None
    end
    local _, equipData = Tables.equipTable:TryGetValue(equipInstData.templateId)
    if not equipData then
        return EquipTechConst.EEquipEnhanceSuccessProb.None
    end
    for _, attrModifier in pairs(equipData.displayAttrModifiers) do
        if attrModifier.modifierType == attrShowInfo.attrModifier and
            ((attrShowInfo.isCompositeAttr and attrShowInfo.attributeType == attrModifier.compositeAttr) or
                (not attrShowInfo.isCompositeAttr and attrShowInfo.attributeType == attrModifier.attrType)) then
            local attrValue = AttributeUtils.modifyAttributeValue(attrModifier.attrType, attrModifier.attrValue,
                attrShowInfo.attrShowCfg.showPercent, attrShowInfo.attrShowCfg.showDiffFromDefault)
            if attrValue > attrShowInfo.modifiedValue then
                return EquipTechConst.EEquipEnhanceSuccessProb.High
            elseif attrValue == attrShowInfo.modifiedValue then
                return EquipTechConst.EEquipEnhanceSuccessProb.Normal
            else
                return EquipTechConst.EEquipEnhanceSuccessProb.None
            end
        end
    end
    return EquipTechConst.EEquipEnhanceSuccessProb.None
end






function EquipTechUtils.getEnhancedAttrValue(attrInfo, equipInstData, isNextLevel)
    local enhancedLevel = equipInstData:GetAttrEnhanceLevel(attrInfo.enhancedAttrIndex)
    if isNextLevel then
        enhancedLevel = enhancedLevel + 1
    end
    if enhancedLevel > 0 and enhancedLevel <= #attrInfo.enhancedAttrValues then
        return attrInfo.enhancedAttrValues[enhancedLevel - 1]
    end
    return attrInfo.attrValue
end




function EquipTechUtils.getEquipInstData(equipInstId)
    local _, equipInstData = CS.Beyond.Gameplay.EquipUtil.TryGetEquipInstData(Utils.getCurrentScope(), equipInstId)
    return equipInstData
end






function EquipTechUtils.getAttrShowValueText(attrShowInfo, isNextLevel, equipInstId)
    local showValueText = attrShowInfo.showValue
    if isNextLevel then
        local equipInstData = EquipTechUtils.getEquipInstData(equipInstId)
        if equipInstData then
            local targetAttrModifier = nil
            local equipData = Tables.equipTable[equipInstData.templateId]
            for _, attrModifier in pairs(equipData.displayAttrModifiers) do
                if attrModifier.enhancedAttrIndex == attrShowInfo.enhancedAttrIndex then
                    targetAttrModifier = attrModifier
                    break
                end
            end
            local attrValue = EquipTechUtils.getEnhancedAttrValue(targetAttrModifier, equipInstData, true)
            local attrShowCfg = attrShowInfo.attrShowCfg
            local modifiedValue = AttributeUtils.modifyAttributeValue(attrShowInfo.attributeType, attrValue, attrShowCfg.showPercent, attrShowCfg.showDiffFromDefault)
            if string.isEmpty(attrShowInfo.attrShowCfg.valueFormat) then
                showValueText = AttributeUtils.generateShowValue(modifiedValue, attrShowCfg.showPercent)
            else
                showValueText = AttributeUtils.generateShowValueByValueFormat(attrValue,
                    attrShowCfg.valueFormat, attrShowCfg.showPercent)
            end
        end
    end
    return string.format(Language.LUA_WEAPON_EXHIBIT_UPGRADE_ADD_FORMAT, showValueText)
end

function EquipTechUtils.setEquipBaseInfo(view, loader, equipTemplateId)
    local itemData = Tables.itemTable[equipTemplateId]
    local equipCfg = Tables.equipTable[equipTemplateId]

    if view.equipName then
        view.equipName.text = itemData.name
    end
    view.levelNum.text = equipCfg.minWearLv
    local equipType = equipCfg.partType
    local equipTypeName = Language[UIConst.CHAR_INFO_EQUIP_TYPE_TILE_PREFIX .. LuaIndex(equipType:ToInt())]
    local equipTypeSpriteName = UIConst.EQUIP_TYPE_TO_ICON_NAME[equipType]
    view.equipTypeName.text = equipTypeName
    view.equipTypeIcon:LoadSprite(UIConst.UI_SPRITE_EQUIP_PART_ICON, equipTypeSpriteName)
    if view.rarityLightImg then
        UIUtils.setItemRarityImage(view.rarityLightImg, itemData.rarity)
    end
    if view.rarityImg then
        UIUtils.setItemRarityImage(view.rarityImg, itemData.rarity)
    end
end

function EquipTechUtils.canShowEquipEnhanceNode(equipInstId)
    if not equipInstId then
        return false
    end
    local equipInstData = EquipTechUtils.getEquipInstData(equipInstId)
    if not equipInstData then
        return false
    end
    if equipInstData:IsEnhanced() then
        return true
    end
    return Utils.isSystemUnlocked(GEnums.UnlockSystemType.EquipEnhance) and
        EquipTechUtils.canEquipEnhance(equipInstData.templateId)
end











function EquipTechUtils.getUnlockedEquipPackList(isHighLevelSuit, isSuit)

    local equipPackDataList = {}
    local sortFunc = Utils.genSortFunction(EquipTechConst.EQUIP_PRODUCE_PACK_SORT_CONFIG[EquipTechConst.PANL_SORT_TYPE.MATERIAL].innerKeys, false)
    for packId, packFormulaDataList in pairs(Tables.equipPackFormulaTable) do
        local _, equipPackData = Tables.equipPackTable:TryGetValue(packId)
        if equipPackData and isHighLevelSuit == equipPackData.isHighLevelSuit
            and (isSuit == nil or isSuit == equipPackData.isSuit) then
            local equipList = {}
            local costMatSortId = 0
            local generalPackSortId = packId == Tables.EquipTechConst.generalEquipPackId and 9999 or 1
            for _, packFormulaData in pairs(packFormulaDataList.itemList) do
                if EquipTechUtils.isEquipFormulaVisible(packFormulaData.formulaId) then
                    local _, formulaData = Tables.equipFormulaTable:TryGetValue(packFormulaData.formulaId)
                    if formulaData then
                        local costMatId = EquipTechUtils.GetDefaultCostMaterial(formulaData.formulaId)
                        if costMatId then
                            local _, costMatData = Tables.itemTable:TryGetValue(costMatId)
                            if costMatData then
                                costMatSortId = math.max(costMatSortId, costMatData.sortId2)
                            end
                        end
                        table.insert(equipList, FilterUtils.processEquipProduce(formulaData))
                    end
                end
            end
            if #equipList > 0 then

                local packData = {
                    equipPackData = equipPackData,
                    sortId = equipPackData.sortId,
                    isExpanded = true,
                    equipList = equipList,
                    costMatSortId = costMatSortId,
                    generalPackSortId = generalPackSortId
                }
                table.sort(packData.equipList, sortFunc)
                table.insert(equipPackDataList, packData)
            end

        end
    end
    return equipPackDataList
end

function EquipTechUtils.isEquipFormulaVisible(formulaId)
    local _, formulaData = Tables.equipFormulaTable:TryGetValue(formulaId)
    if not formulaData then
        return false
    end

    local equipTechSystem = GameInstance.player.equipTechSystem

    local mapManager = GameInstance.player.mapManager
    local curLv = GameInstance.player.adventure.adventureLevelData.lv
    local visible = true
    if not equipTechSystem:IsFormulaUnlock(formulaData.formulaId) then
        if formulaData.unlockType == GEnums.EquipFormulaUnlockType.AdventureLevel then
            visible = formulaData.unlockValue - curLv <= Tables.equipTechConst.visibleFormulaMaxDeltaLevel
        elseif formulaData.unlockType == GEnums.EquipFormulaUnlockType.EquipFormulaChest then
            local found, instId = mapManager:GetMapMarkInstId(GEnums.MarkType.EquipFormulaChest, formulaData.unlockKey)
            visible = found and MapUtils.checkIsValidMarkInstId(instId)
        elseif formulaData.unlockType == GEnums.EquipFormulaUnlockType.DomainShop then
            local found, instId = mapManager:GetMapMarkInstId(GEnums.MarkType.DomainShop, formulaData.unlockKey)
            visible = found and MapUtils.isMarkVisible(instId)
        elseif formulaData.unlockType == GEnums.EquipFormulaUnlockType.StarShop then
            visible = PhaseManager:IsPhaseUnlocked(PhaseId.ShopStar)
        else
            visible = false
        end
    end
    return visible
end





function EquipTechUtils.hasVisibleEquipPack(isHighLevelSuit, isSuit)
    for packId, packFormulaDataList in pairs(Tables.equipPackFormulaTable) do
        local _, equipPackData = Tables.equipPackTable:TryGetValue(packId)
        if equipPackData and isHighLevelSuit == equipPackData.isHighLevelSuit
            and (isSuit == nil or isSuit == equipPackData.isSuit) then
            for _, packFormulaData in pairs(packFormulaDataList.itemList) do
                if EquipTechUtils.isEquipFormulaVisible(packFormulaData.formulaId) then
                    return true
                end
            end
        end
    end
    return false
end

function EquipTechUtils.hasVisibleBasicEquipPack()
    return EquipTechUtils.hasVisibleEquipPack(false, nil)
end

function EquipTechUtils.hasVisibleHighLevelSuitEquipPack()
    return EquipTechUtils.hasVisibleEquipPack(true, true)
end

function EquipTechUtils.hasVisibleHighLevelPartsEquipPack()
    return EquipTechUtils.hasVisibleEquipPack(true, false)
end



function EquipTechUtils.GetCostMaterialList()
    local MaterialList = {}
    for _, materialInfo in pairs(Tables.EquipCostMaterialTable) do
        table.insert(MaterialList, {
            itemId = materialInfo.scriptItemId,
            sortId = materialInfo.sortId,
            name = materialInfo.scriptName,
            isRecommended = materialInfo.isRecommended,
            chainId = materialInfo.chainId,
        })
    end
    table.sort(MaterialList, function(a, b) return a.sortId > b.sortId end)
    return MaterialList
end


function EquipTechUtils.GetDefaultCostMaterial(formulaId)
    local defaultChain = EquipTechUtils.GetDefaultFormulaChain(formulaId)
    if defaultChain then
        local chainId = defaultChain.chainId
        local chainCostInfo = EquipTechUtils.GetChainCostInfo(chainId)
        if chainCostInfo and chainCostInfo.costItemId then
            return chainCostInfo.costItemId
        end
    end

    return nil
end


function EquipTechUtils.GetCurCostMaterial(formulaId)
    local chainId = EquipTechUtils.GetCurFormulaChainId(formulaId)
    local chainCostInfo = EquipTechUtils.GetChainCostInfo(chainId)
    if chainCostInfo and chainCostInfo.costItemId then
        return chainCostInfo.costItemId
    end
end

function EquipTechUtils.GetDefaultFormulaChain(formulaId)
    if not defaultFormulaChainMap then
        defaultFormulaChainMap = {}
    end

    if not defaultFormulaChainMap[formulaId] then
        local _, formulaData = Tables.equipFormulaTable:TryGetValue(formulaId)
        if not formulaData then
            return nil
        end

        local level = formulaData.level
        local _, chainData = Tables.equipFormulaChainTable:TryGetValue(level)
        if not chainData then
            return nil
        end

        for _, chainInfo in pairs(chainData.chainList) do
            if chainInfo.isDefault then
                defaultFormulaChainMap[formulaId] = chainInfo
                break
            end
        end
    end

    return defaultFormulaChainMap[formulaId]
end

function EquipTechUtils.GetCurFormulaChainId(formulaId)
    local equipTechSystem = GameInstance.player.equipTechSystem
    local chainId = equipTechSystem:GetFormulaChainId(formulaId)

    if chainId == 0 then
        local defaultChain = EquipTechUtils.GetDefaultFormulaChain(formulaId)
        chainId = defaultChain and defaultChain.chainId or 0
    end

    return chainId
end




function EquipTechUtils.GetChainCostMap()
    if chainCostMap then
        return chainCostMap
    end
    chainCostMap = {}
    for _, groupData in pairs(Tables.equipFormulaChainTable) do
        for _, chainInfo in pairs(groupData.chainList) do
            if not chainCostMap[chainInfo.chainId] then
                chainCostMap[chainInfo.chainId] = {
                    costGoldId = chainInfo.costGoldId,
                    costItemId = (#chainInfo.costItemId > 0) and chainInfo.costItemId[0] or nil,
                    discount = chainInfo.cnDiscount,
                }
            end
        end
    end
    return chainCostMap
end


function EquipTechUtils.GetChainCostInfo(chainId)
    return EquipTechUtils.GetChainCostMap()[chainId]
end



function EquipTechUtils.GetAppendGeneralGoldEquipSet()
    if appendGeneralGoldEquipSet then
        return appendGeneralGoldEquipSet
    end
    appendGeneralGoldEquipSet = {}
    for _, data in pairs(Tables.equipAppendRewardTable) do
        local strList = data.parameter1.valueStringList
        if strList and #strList > 0 then
            local rewardId = strList[0]
            local hasReward, rewardData = Tables.rewardTable:TryGetValue(rewardId)
            if hasReward then
                local itemBundles = rewardData.itemBundles
                for i = 0, #itemBundles - 1 do
                    appendGeneralGoldEquipSet[itemBundles[i].id] = true
                end
            end
        end
    end
    return appendGeneralGoldEquipSet
end


function EquipTechUtils.TryShowNewGeneralGoldEquip(onConfirm)


    if hasShowAppendGeneralGoldEquip then
        return false
    end

    local allEquip = EquipTechUtils.GetAppendGeneralGoldEquipSet()
    local showEquipList = {}
    local items = {}
    for formulaId, _ in pairs(allEquip) do
        local read = GameInstance.player.equipTechSystem:IsReadUniversalGoldEquip(formulaId)
        if not read then
            table.insert(showEquipList, formulaId)
            table.insert(items, { id = formulaId })
        end
    end

    if #showEquipList > 0 then

        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_EQUIP_APPEND_GENERAL_GOLD_EQUIP_TIP,
            items = items,
            hideCancel = true,
            onConfirm = function()
                if onConfirm then
                   onConfirm()
                end
            end,
        })
    end


    GameInstance.player.equipTechSystem:UpdateReadUniversalGoldEquip(showEquipList)

    hasShowAppendGeneralGoldEquip = true
    return #showEquipList > 0
end


function EquipTechUtils.GetLevelChainDetail(level, chainId)
    local _, chainGroupData = Tables.equipFormulaChainTable:TryGetValue(level)
    if chainGroupData then
        for _, chain in pairs(chainGroupData.chainList) do
            if chain.chainId == chainId then
                return chain
            end
        end
    end

    return nil
end





function EquipTechUtils.GetMaterialCompatibility(itemId)
    local compatibleList = {}
    local incompatibleList = {}

    local hasValue, curMaterialInfo = Tables.EquipCostMaterialTable:TryGetValue(itemId)
    if not hasValue then
        return compatibleList, incompatibleList
    end

    local availableSet = {}
    for i = 0, #curMaterialInfo.availableScript - 1 do
        availableSet[curMaterialInfo.availableScript[i]] = true
    end

    for _, materialInfo in pairs(Tables.EquipCostMaterialTable) do
        if materialInfo.scriptItemId ~= itemId then
            local entry = {
                itemId = materialInfo.scriptItemId,
                sortId = materialInfo.sortId,
                name = materialInfo.scriptName,
                isRecommended = materialInfo.isRecommended,
            }
            if availableSet[materialInfo.scriptItemId] then
                table.insert(compatibleList, entry)
            else
                table.insert(incompatibleList, entry)
            end
        end
    end

    local sortFunc = function(a, b) return a.sortId < b.sortId end
    table.sort(compatibleList, sortFunc)
    table.sort(incompatibleList, sortFunc)

    return compatibleList, incompatibleList
end





function EquipTechUtils.GetScriptUnlockObtainWayTexts(scriptItemId, onlyLocked)
    local texts = {}
    local ok, materialInfo = Tables.EquipCostMaterialTable:TryGetValue(scriptItemId)
    if not ok or materialInfo.unlockScriptObtainWay == nil or materialInfo.unlockScriptObtainWay.Count == 0 then
        return texts
    end
    for csIndex = 0, materialInfo.unlockScriptObtainWay.Count - 1 do
        local wayId = materialInfo.unlockScriptObtainWay[csIndex]
        local succ, wayCfg = Tables.systemJumpTable:TryGetValue(wayId)
        if succ and wayCfg ~= nil then
            if not onlyLocked or not EquipTechUtils.CheckObtainWayCondition(wayId) then
                table.insert(texts, wayCfg.desc)
            end
        else
            logger.error(string.format("[EquipTech] scriptItemId=%s 配的 unlockScriptObtainWay=%s 在 systemJumpTable 里查不到", scriptItemId, wayId))
        end
    end
    return texts
end





function EquipTechUtils.GetMaterialInfoByChainId(chainId)
    for _, materialInfo in pairs(Tables.EquipCostMaterialTable) do
        if materialInfo.chainId == chainId then
            return true, materialInfo
        end
    end
    return false, nil
end






function EquipTechUtils.IsMissionCompletedByChainId(chainId)
    local hasValue, materialInfo = EquipTechUtils.GetMaterialInfoByChainId(chainId)
    if not hasValue then
        return false
    end
    if string.isEmpty(materialInfo.unlockScriptMission) then
        return true
    end
    local state = GameInstance.player.mission:GetMissionState(materialInfo.unlockScriptMission)
    return state == CS.Beyond.Gameplay.MissionSystem.MissionState.Completed
end







function EquipTechUtils.IsAllObtainWaysCompletedByChainId(chainId)
    local hasValue, materialInfo = EquipTechUtils.GetMaterialInfoByChainId(chainId)
    if not hasValue then
        return false
    end
    local wayList = materialInfo.unlockScriptObtainWay
    if wayList == nil or wayList.Count == 0 then
        return true
    end

    for csIndex = 0, wayList.Count - 1 do
        local wayId = wayList[csIndex]
        if not EquipTechUtils.CheckObtainWayCondition(wayId) then
            return false
        end
    end
    return true
end

function EquipTechUtils.CheckObtainWayCondition(wayId)
    return ItemObtainWaysUtils.CheckObtainWayCondition(string.format("condition_%s", wayId))
end




function EquipTechUtils.IsFormulaChainUnlock(chainId)
    return EquipTechUtils.IsMissionCompletedByChainId(chainId)
        and EquipTechUtils.IsAllObtainWaysCompletedByChainId(chainId)
end




function EquipTechUtils.HasNewChain()
    if not EquipTechUtils.HasCompleteBatchGuide() then
        return false
    end

    local materialList = EquipTechUtils.GetCostMaterialList()
    local newest = materialList[1]
    if newest and EquipTechUtils.IsChainNew(newest.chainId) then
        return true, newest.chainId
    end

    return false
end




function EquipTechUtils.IsChainNew(chainId)
    if not EquipTechUtils.HasCompleteBatchGuide() then
        return false
    end

    local hasValue, readChainId = GameInstance.player.globalVar:TryGetClientVar("read_newest_equip_formula_chain_id")
    if not hasValue then
        return false
    end

    local __, materialInfo = EquipTechUtils.GetMaterialInfoByChainId(chainId)
    local __, readMaterialInfo = EquipTechUtils.GetMaterialInfoByChainId(readChainId)
    if materialInfo and readMaterialInfo then
        return materialInfo.sortId > readMaterialInfo.sortId
    end

    return false
end


function EquipTechUtils.MarkChainsAsRead()
    local materialList = EquipTechUtils.GetCostMaterialList()
    local newest = materialList[1]
    if newest then
        GameInstance.player.globalVar:SetClientVar("read_newest_equip_formula_chain_id", newest.chainId)
    end

    Notify(MessageConst.ON_EQUIP_FORMULA_CHAIN_READ_CHANGED)
end


function EquipTechUtils.GetSortType()
    local hasJump = GameInstance.player.mission.hasEarlyAcceptChapterMask
    local hasValue, sortType = GameInstance.player.globalVar:TryGetClientVar("equip_panel_sort_type")
    if hasValue and 0 < sortType and sortType <= #EquipTechConst.EQUIP_PRODUCE_PACK_SORT_CONFIG then
        return sortType
    end

    local defaultSortType = hasJump and EquipTechConst.PANL_SORT_TYPE.EARLY_ACCEPT or EquipTechConst.PANL_SORT_TYPE.MATERIAL
    return defaultSortType
end

function EquipTechUtils.UpdateSelectedSortType(sortType)
    if sortType and 0 < sortType and sortType <= #EquipTechConst.EQUIP_PRODUCE_PACK_SORT_CONFIG then
        GameInstance.player.globalVar:SetClientVar("equip_panel_sort_type", sortType)
    end
end


function EquipTechUtils.HasCompleteBatchGuide()
    local count =  Tables.EquipTechConst.updateHintFinishedGuideId.Count
    for i = 0, count - 1 do
        local guideId = Tables.EquipTechConst.updateHintFinishedGuideId[i]
        if GameInstance.player.guide:IsGuideCompleted(guideId) then
            return true
        end
    end
    return false
end



_G.EquipTechUtils = EquipTechUtils
return EquipTechUtils