local SocialBuildingSource = CS.Beyond.Gameplay.Factory.SocialBuildingSource
local GeneralAbilityType = GEnums.GeneralAbilityType
local AbilityState = CS.Beyond.Gameplay.GeneralAbilitySystem.AbilityState

local FactoryUtils = {}

function FactoryUtils.curPowerIsEnough()
    return not FactoryUtils.getCurRegionPowerInfo().isStopByPower
end

function FactoryUtils.getBuildingStateType(nodeId)

    local spBuilding = GameInstance.player.facSpMachineSystem:GetNode(nodeId)
    if spBuilding and spBuilding:IsIdle() then
        return GEnums.FacBuildingState.Idle
    end

    return GameInstance.remoteFactoryManager:QueryBuildingState(Utils.getCurrentChapterId(), nodeId, false)
end


function FactoryUtils.getFormulaGroupId(craftId)
    local craftData = Tables.factoryMachineCraftTable:GetValue(craftId)
    local machineModeList = FactoryUtils.getBuildingModeListCovered(craftData.machineId)
    for index, mapData in ipairs(machineModeList) do
        local success, groupData = Tables.factoryMachineCraftGroupTable:TryGetValue(mapData.groupName)
        if success then
            for _, id in pairs(groupData.craftList) do
                if id == craftId then
                    return mapData.groupName
                end
            end
        end
    end
    return nil
end

function FactoryUtils.getCraftNeedTime(craftData, formulaGroupId)
    if formulaGroupId == nil then
        formulaGroupId = FactoryUtils.getFormulaGroupId(craftData.id)
    end
    local machineCraftGroupData = Tables.factoryMachineCraftGroupTable:GetValue(formulaGroupId)
    return craftData.progressRound * machineCraftGroupData.msPerRound * 0.001
end

function FactoryUtils.getCurHubNodeId()
    local id = GameInstance.player.facSpMachineSystem:GetCurHubNodId()
    return id > 0 and id or nil
end

function FactoryUtils.getPowerText(power, isEnergy)
    local unit = isEnergy and Language.LUA_FAC_POWER_UNIT or Language.LUA_FAC_MACHINE_CONSUME_POWER_UNIT
    return UIUtils.getNumString(power) .. unit
end

function FactoryUtils.getBuildingStateIconName(nodeId, state)
    state = state or FactoryUtils.getBuildingStateType(nodeId)
    return UIConst.UI_SPRITE_FAC_BUILDING_COMMON, FacConst.FAC_BUILDING_STATE_TO_SPRITE[state]
end

function FactoryUtils.getItemProductivityPerMinus(itemId)
    return 0



end

function FactoryUtils.isBuilding(itemId)
    if string.isEmpty(itemId) then
        return false
    end

    local valid, data = Tables.factoryBuildingItemTable:TryGetValue(itemId)
    return valid, valid and data.buildingId or nil
end

function FactoryUtils.isLogistic(itemId)
    if string.isEmpty(itemId) then
        return false
    end

    local valid, data = Tables.factoryItem2LogisticIdTable:TryGetValue(itemId)
    return valid, valid and data.logisticId or nil
end


function FactoryUtils.isInBuildMode()
    local opened, ctrl = UIManager:IsOpen(PanelId.FacBuildMode)
    if opened then
        return ctrl.m_mode ~= FacConst.FAC_BUILD_MODE.Normal, ctrl.m_mode
    else
        return false
    end
end

function FactoryUtils.isMovingBuilding()
    local opened, ctrl = UIManager:IsOpen(PanelId.FacBuildMode)
    if opened then
        return ctrl.m_buildingNodeId ~= nil
    else
        return false
    end
end


function FactoryUtils.getBuildingNodeHandler(nodeId, chapterId)
    chapterId = chapterId or Utils.getCurrentChapterId()
    return CSFactoryUtil.GetNodeHandlerByNodeId(nodeId, chapterId)
end

function FactoryUtils.isPendingBuildingNode(nodeId, chapterId)
    chapterId = chapterId or Utils.getCurrentChapterId()
    local slotId = CSFactoryUtil.GetBlueprintSlotId(chapterId, nodeId)
    return slotId > 0
end

function FactoryUtils.getPendingBuildingNodeSlotId(nodeId, chapterId)
    chapterId = chapterId or Utils.getCurrentChapterId()
    local slotId = CSFactoryUtil.GetBlueprintSlotId(chapterId, nodeId)
    if slotId > 0 then
        return slotId
    else
        return
    end
end

function FactoryUtils.getPendingSlotName(slotId)
    return Language["LUA_FAC_BLUEPRINT_PENDING_NAME_" .. slotId]
end


function FactoryUtils.getBuildingComponentHandler(componentId)
    return CSFactoryUtil.GetComponentHandlerByComponentId(componentId)
end

function FactoryUtils.getBuildingComponentHandlerAtPos(syncNode, cptPos)
    local cpt = syncNode:GetComponentInPosition(cptPos:GetHashCode())
    if cpt then
        return FactoryUtils.getBuildingComponentHandler(cpt.componentId)
    end
    return nil
end



function FactoryUtils.getBuildingComponentPayload_Social(nodeId, chapterId)
    chapterId = chapterId or Utils.getCurrentChapterId()
    return CSFactoryUtil.GetBuildingComponentPayload_Social(nodeId, chapterId)
end


function FactoryUtils.isSocialBuilding(nodeId, chapterId)
    chapterId = chapterId or Utils.getCurrentChapterId()
    return CSFactoryUtil.IsSocialBuilding(nodeId, chapterId)
end


function FactoryUtils.isOthersSocialBuilding(nodeId, chapterId)
    chapterId = chapterId or Utils.getCurrentChapterId()
    return CSFactoryUtil.IsOthersSocialBuilding(nodeId, chapterId)
end


function FactoryUtils.getSocialBuildingSource(nodeId, chapterId)
    chapterId = chapterId or Utils.getCurrentChapterId()
    return CSFactoryUtil.GetSocialBuildingSource(nodeId, chapterId)
end



function FactoryUtils.getSocialBuildingDetails(nodeId, chapterId)
    chapterId = chapterId or Utils.getCurrentChapterId()
    local social, source = CSFactoryUtil.GetSocialBuildingDetails(nodeId, chapterId)
    return social, source
end


function FactoryUtils.getBuildingSocialSourceInfo(nodeId, chapterId)
    chapterId = chapterId or Utils.getCurrentChapterId()
    local social, source = CSFactoryUtil.GetSocialBuildingDetails(nodeId, chapterId)
    local isOthers = source == SocialBuildingSource.Others
    local iePreset = false
    if social then
        iePreset = social.preset
    end
    return isOthers, iePreset
end


function FactoryUtils.getSocialBuildingStability(nodeId, chapterId)
    chapterId = chapterId or Utils.getCurrentChapterId()
    return CSFactoryUtil.GetSocialBuildingStability(nodeId, chapterId)
end


function FactoryUtils.isSocialBuildingTechLayerUnlocked(buildingId)
    local buildingData = Tables.factoryBuildingTable:GetValue(buildingId)
    if buildingData.type ~= GEnums.FacBuildingType.Battle then
        return nil, true
    end
    local success, techId = CSFactoryUtil.TryGetTechId(buildingId)
    if not success then
        logger.error("[Factory] Get building tech id failed, buildingId: " .. tostring(buildingId))
        return nil, false
    end

    local techData = Tables.facSTTNodeTable:GetValue(techId)
    local techLayerId = techData.layer
    local isLocked = GameInstance.player.facTechTreeSystem:LayerIsLocked(techLayerId)
    return techLayerId, not isLocked
end

function FactoryUtils.canMoveBuilding(nodeId, needToast)
    local node = FactoryUtils.getBuildingNodeHandler(nodeId)
    if not node then
        return false
    end
    local isMoveLocked = CSFactoryUtil.CheckIsBuildingMoveAndDelLocked(node.templateId, node.instKey, needToast == true)
    if isMoveLocked then
        return false
    end

    local isInvalidBuilding = FactoryUtils.isInvalidBuilding(nodeId)
    if isInvalidBuilding then
        if needToast then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_INVALID_BUILDING_OP_DISABLE_TOAST)
        end
        return false
    end

    local isOthersSocialBuilding = FactoryUtils.isOthersSocialBuilding(nodeId)
    if isOthersSocialBuilding then
        return false
    end

    local _, bData = Tables.factoryBuildingTable:TryGetValue(node.templateId)
    if bData and not bData.allowPlayerMove then

        if needToast then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_BUILDING_MOVE_NOT_ALLOWED)
        end
        return false
    end

    local pdp = node.predefinedParam
    if not pdp then
        return true
    end
    if not pdp.common then
        return true
    end
    if pdp.common.forbidMove then
        if needToast then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_BUILDING_MOVE_NOT_ALLOWED)
        end
        return false
    end
    return true
end

function FactoryUtils.canDelBuilding(nodeId, needToast, chapterId)
    if Utils.isInSpaceShip() then

        if needToast then
            Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_FAC_DEL_BUILDING_OTHER_CHAPTER, Language.LUA_SPACESHIP_NAME))
        end
        return false
    end

    local currChapterId = Utils.getCurrentChapterId()
    if chapterId == nil then
        chapterId = currChapterId
    elseif chapterId ~= currChapterId then

        if needToast then
            local success, domainData = Tables.domainDataTable:TryGetValue(ScopeUtil.ChapterIdInt2Str(currChapterId))
            local chapterName = success and domainData.domainName or ""
            Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_FAC_DEL_BUILDING_OTHER_CHAPTER, chapterName))
        end
        return false
    end

    if FactoryUtils.isPendingBuildingNode(nodeId, chapterId) then
        return
    end

    local node = FactoryUtils.getBuildingNodeHandler(nodeId, chapterId)
    if not node then
        return false
    end

    local _, bData = Tables.factoryBuildingTable:TryGetValue(node.templateId)
    if bData and not bData.canDelete then
        if needToast then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_BUILDING_DELETE_NOT_ALLOWED)
        end
        return false
    end
    local isDelLocked = CSFactoryUtil.CheckIsBuildingMoveAndDelLocked(node.templateId, node.instKey, needToast == true)
    if isDelLocked then
        return false
    end

    local pdp = node.predefinedParam
    if not pdp then
        return true
    end
    if not pdp.common then
        return true
    end
    if pdp.common.forbidDelete then
        if needToast then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_BUILDING_DELETE_NOT_ALLOWED)
        end
        return false
    end
    return true
end

function FactoryUtils.delBuilding(nodeId, onComplete, noConfirm, hintText, chapterId, backToDepot)
    chapterId = chapterId or Utils.getCurrentChapterId()

    local clearAct

    local canDelete = FactoryUtils.canDelBuilding(nodeId, true, chapterId)
    if not canDelete then
        return
    end



















    local delBuildingAct = function()
        if clearAct then
            clearAct()
        end
        GameInstance.player.remoteFactory.core:Message_OpDismantle(chapterId, nodeId, backToDepot and true or false, function()
            if onComplete then
                onComplete()
            end
        end)
    end

    if noConfirm then
        delBuildingAct()
    else
        if hintText == nil or hintText == "" then
            hintText = Language.LUA_FAC_ASK_DELETE_BUILDING
        end
        Notify(MessageConst.SHOW_POP_UP, {
            content = hintText,
            hideBlur = true,
            onCancel = clearAct,
            onConfirm = delBuildingAct,
        })
    end
end


function FactoryUtils.canShareBuilding(nodeId)
    if Utils.isInBlackbox() then
        return false
    end

    if FactoryUtils.isPendingBuildingNode(nodeId) then
        return false
    end

    local node = FactoryUtils.getBuildingNodeHandler(nodeId)
    if not node then
        return false
    end

    local isSocialBuilding = FactoryUtils.isSocialBuilding(nodeId)
    if not isSocialBuilding then
        return false
    end

    return true
end


function FactoryUtils.canReportSocialBuilding(nodeId)
    local social, source = FactoryUtils.getSocialBuildingDetails(nodeId)
    local isValidSource = source == SocialBuildingSource.Others and not social.preset
    if not isValidSource then
        return false
    end

    local node = FactoryUtils.getBuildingNodeHandler(nodeId)
    local buildingId = node.templateId
    local socialBuildingData = FactoryUtils.getSocialBuildingData(buildingId)
    if not socialBuildingData then
        return false
    end

    return socialBuildingData.canReport
end


function FactoryUtils.reportSocialBuilding(nodeId, fnValidateOnCallback)
    if not FactoryUtils.canReportSocialBuilding(nodeId) then
        return
    end

    local chapterId = Utils.getCurrentChapterId()
    local social, source = FactoryUtils.getSocialBuildingDetails(nodeId, chapterId)
    local ownerId = social.ownerId
    GameInstance.player.friendSystem:SyncSocialFriendInfo({ ownerId }, function()
        if fnValidateOnCallback and not fnValidateOnCallback() then
            return
        end
        local success, ownerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(ownerId)
        if not success then

            logger.info(ELogChannel.Factory, "ReportSocialBuilding: Owner info not found, roleId: " .. tostring(ownerId))
        end

        UIManager:Open(PanelId.ReportPlayer, {
            reportType = FriendUtils.ReportGroupType.SocialBuilding,
            roleId = ownerId,
            socialBuildingParam = {
                chapterId = Utils.getCurrentChapterId(),
                nodeId = nodeId,
            },
        })
    end)
end


function FactoryUtils.canLikeSocialBuilding(nodeId, needToast)
    local isOthersSocialBuilding = FactoryUtils.isOthersSocialBuilding(nodeId)
    if not isOthersSocialBuilding then
        return false
    end

    return not FactoryUtils.isLikedSocialBuilding(nodeId, needToast)
end


function FactoryUtils.isLikedSocialBuilding(nodeId, needToast)
    local social = FactoryUtils.getBuildingComponentPayload_Social(nodeId)
    local lastSetLikeTs = social.lastSetLikeTs
    local currentRefreshTs = Utils.getCurrentCommonServerRefreshTime()
    local isLiked = lastSetLikeTs >= currentRefreshTs
    if needToast and isLiked then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_LIKE_SOCIAL_BUILDING_ALREADY_DONE)
    end
    return isLiked
end


function FactoryUtils.likeSocialBuilding(nodeId, callback)
    if not FactoryUtils.canLikeSocialBuilding(nodeId, true) then
        return
    end

    local chapterId = Utils.getCurrentChapterId()
    GameInstance.player.remoteFactory.core:Message_SetSocialLike(chapterId, nodeId, callback)
end


function FactoryUtils.updateBuildingLikeAbilityState(nodeId)
    local abilityState = FactoryUtils.canLikeSocialBuilding(nodeId) and AbilityState.Idle or AbilityState.ForbiddenUse
    GameInstance.player.generalAbilitySystem:SwitchAbilityStateByType(GeneralAbilityType.BuildingLike, abilityState)
end

function FactoryUtils.getItemBuildingData(itemId)
    local succ, buildingItemData = Tables.factoryBuildingItemTable:TryGetValue(itemId)
    if not succ then
        return
    end
    local buildingData = Tables.factoryBuildingTable:GetValue(buildingItemData.buildingId)
    return buildingData
end

function FactoryUtils.getItemBuildingId(itemId)
    local succ, buildingItemData = Tables.factoryBuildingItemTable:TryGetValue(itemId)
    if not succ then
        return
    end
    return buildingItemData.buildingId
end

function FactoryUtils.getBuildingItemData(buildingId, noError)
    local succ, buildingItemData = Tables.factoryBuildingItemReverseTable:TryGetValue(buildingId)
    if not succ then
        if not noError then
            logger.error("策划配错了，建筑没有对应道具", buildingId)
        end
        return
    end
    local itemData = Tables.itemTable:GetValue(buildingItemData.itemId)
    return itemData
end

function FactoryUtils.getBuildingItemId(buildingId)
    if not buildingId then
        return nil
    end
    local succ, buildingItemData = Tables.factoryBuildingItemReverseTable:TryGetValue(buildingId)
    if succ then
        return buildingItemData.itemId
    end
end


function FactoryUtils.getSocialBuildingData(buildingId)
    local success, buildingData = Tables.factoryBuildingTable:TryGetValue(buildingId)
    if not success then
        return
    end
    local nodeType
    success, nodeType = CSFactoryUtil.GetFCNodeType(buildingData.type)
    if not success then
        return
    end
    local socialBuildingData
    success, socialBuildingData = Tables.factorySocialBuildingTable:TryGetValue(nodeType)
    return socialBuildingData
end


function FactoryUtils.isItemSocialBuilding(itemId)
    local result = false
    local buildingId = FactoryUtils.getItemBuildingId(itemId)
    if buildingId then
        local socialBuildingData = FactoryUtils.getSocialBuildingData(buildingId)
        if socialBuildingData then
            result = socialBuildingData.isSocialBuilding
        end
    end
    return result
end

function FactoryUtils.getCurBuildingConsumePower(nodeId)
    local node = FactoryUtils.getBuildingNodeHandler(nodeId)
    local powerCost = FactoryUtils.getBuildingConsumePower(node.templateId)
    local powerObj = node.power
    if powerObj then
        if node.power.powerCost then
            powerCost = node.power.powerCost
        end
    end
    return powerCost
end

function FactoryUtils.getBuildingConsumePower(buildingId)
    local data = Tables.factoryBuildingTable:GetValue(buildingId)
    return data.powerConsume
end


function FactoryUtils.getItemOutputItemIds(itemId, ignoreUnlock)
    local outcomeIds = {}
    local facCore = GameInstance.player.remoteFactory.core

    do

        local hasCraft, craftIds = Tables.factoryItemAsMachineCrafterIncomeTable:TryGetValue(itemId)
        if hasCraft then
            for _, craftId in pairs(craftIds.list) do
                if ignoreUnlock or facCore:IsFormulaVisible(craftId) then
                    local craftData = Tables.factoryMachineCraftTable:GetValue(craftId)
                    local itemBundleGroupList = craftData.outcomes
                    for _, group in pairs(itemBundleGroupList) do
                        for _, bundle in pairs(group.group) do
                            outcomeIds[bundle.id] = true
                        end
                    end
                end
            end
        end
    end

    do

        local hasCraft, craftIds = Tables.FactoryItemAsHubCraftIncomeTable:TryGetValue(itemId)
        if hasCraft then
            local sys = GameInstance.player.facSpMachineSystem
            for _, craftId in pairs(craftIds.list) do
                if ignoreUnlock or sys:IsCraftUnlocked(craftId) then
                    local craftData = Tables.factoryHubCraftTable:GetValue(craftId)
                    for _, bundle in pairs(craftData.outcomes) do
                        outcomeIds[bundle.id] = true
                    end
                end
            end
        end
    end

    if not next(outcomeIds) then
        return
    end

    local outcomeSortList = {}
    for id, _ in pairs(outcomeIds) do
        local _, itemData = Tables.itemTable:TryGetValue(id)
        if itemData then
            table.insert(outcomeSortList, {
                sortId1 = itemData.sortId1,
                sortId2 = itemData.sortId2,
                itemId = id,
            })
        end
    end
    table.sort(outcomeSortList, Utils.genSortFunction({"sortId1", "sortId2"}, true))

    local outcomeIdList = {}
    for sortIdx, sortData in pairs(outcomeSortList) do
        table.insert(outcomeIdList, sortData.itemId)
    end
    return outcomeIdList
end





function FactoryUtils.getItemAsInputRecipeIds(itemId, ignoreUnlock)
    local recipeIds = {}
    local canCraft = false

    do

        local _, fuelData = Tables.factoryFuelItemTable:TryGetValue(itemId)
        if fuelData then
            local buildingId, powerStationData
            for id, data in pairs(Tables.factoryPowerStationTable) do
                local buildingItemId = FactoryUtils.getBuildingItemId(id)
                if WikiUtils.canShowWikiEntry(buildingItemId) then
                    buildingId = id
                    powerStationData = data
                    break
                end
            end
            if buildingId and powerStationData then
                local info = {
                    incomes = { { id = itemId, count = 1 } },
                    time = powerStationData.msPerRound * fuelData.progressRound * 0.001,
                    outcomeText = string.format(Language.FUEL_OUTCOME_TEXT_FORMAT, fuelData.powerProvide),
                    buildingId = buildingId,
                    craftId = fuelData.id,
                    isUnlock = true,
                }
                table.insert(recipeIds, info)
                canCraft = true
            end

        end
    end

    do

        local hasCraft, craftIds = Tables.factoryItemAsMachineCrafterIncomeTable:TryGetValue(itemId)
        if hasCraft then
            canCraft = true
            for _, craftId in pairs(craftIds.list) do
                table.insert(recipeIds, FactoryUtils.parseMachineCraftData(craftId))
            end
        end
    end

    do

        local hasCraft, craftIds = Tables.FactoryItemAsHubCraftIncomeTable:TryGetValue(itemId)
        if hasCraft then
            canCraft = true
            for _, craftId in pairs(craftIds.list) do
                if ignoreUnlock or FactoryUtils.isSpMachineFormulaUnlocked(craftId) then
                    table.insert(recipeIds, FactoryUtils.parseHubCraftData(craftId, true))
                end
            end
        end
    end

    do

        local manualCraftIdList = {}

        for craftId, v in pairs(Tables.factoryManualCraftTable) do
            for i = 0, v.ingredients.Count - 1 do
                if v.ingredients[i].id == itemId then
                    table.insert(manualCraftIdList, {
                        sortId = v.sortId,
                        craftId = craftId,
                    })
                    break
                end
            end
        end
        if #manualCraftIdList > 0 then
            table.sort(manualCraftIdList, Utils.genSortFunction({"sortId"}, true))
            local manualCraft = GameInstance.player.facManualCraft
            canCraft = true
            for _, craftData in ipairs(manualCraftIdList) do
                if ignoreUnlock or manualCraft:IsCraftUnlocked(craftData.craftId) then
                    table.insert(recipeIds, FactoryUtils.parseManualCraftData(craftData.craftId, true))
                end
            end
        end
    end

    do

        local _, fluidConsumeItemData = Tables.factoryFluidConsumeItemTable:TryGetValue(itemId)
        if fluidConsumeItemData then
            for _, buildingId in pairs(fluidConsumeItemData.buildingIds) do
                local buildingItemId = FactoryUtils.getBuildingItemId(buildingId)
                local isBuildingUnlocked = WikiUtils.canShowWikiEntry(buildingItemId)
                if isBuildingUnlocked or ignoreUnlock then
                    local _, consumeData = Tables.factoryFluidConsumeTable:TryGetValue(buildingId)
                    if consumeData then
                        local info = {
                            time = consumeData.msPerRound * 0.001,
                            incomes = { { id = itemId, count = 1 } },
                            buildingId = buildingId,
                            craftId = itemId,
                            useFinish = true,
                            isUnlock = isBuildingUnlocked,
                        }
                        table.insert(recipeIds, info)
                    end
                end
            end
        end
    end

    do

        local buildingId = "vaporizer_1"
        local envGenSuccess, envGenData = Tables.factoryVaporizerTable:TryGetValue(buildingId)
        if envGenSuccess then
            for _, groupData in pairs(envGenData.groups) do
                if groupData.consumeItem == itemId then
                    local buildingItemId = FactoryUtils.getBuildingItemId(buildingId)
                    local isBuildingUnlocked = WikiUtils.canShowWikiEntry(buildingItemId)
                    local info = {
                        incomes = { { id = itemId } },
                        buildingId = buildingId,
                        consumeRate = groupData.consumeRate,
                        genEnv = groupData.genEnv,
                        craftId = buildingId .. groupData.genEnv:ToString(),
                        isUnlock = isBuildingUnlocked,
                    }
                    table.insert(recipeIds, info)
                end
            end
        end
    end

    return recipeIds, canCraft
end







function FactoryUtils.getBuildingCrafts(buildingId, ignoreUnlock, justId, producerMode, allModes)
    local bData = Tables.factoryBuildingTable:GetValue(buildingId)
    local bType = bData.type
    local crafts = {}
    local facCore = GameInstance.player.remoteFactory.core
    local inventory = GameInstance.player.inventory

    if bType == GEnums.FacBuildingType.PowerStation then
        local powerStationData = Tables.factoryPowerStationTable:GetValue(buildingId)
        for fuelId, fuelData in pairs(Tables.factoryFuelItemTable) do
            if ignoreUnlock or inventory:IsItemFound(fuelId) then
                if justId then
                    table.insert(crafts, fuelId)
                else
                    local info = {
                        incomes = { { id = fuelId, count = 1 } },
                        time = powerStationData.msPerRound * fuelData.progressRound * 0.001,
                        outcomeText = string.format(Language.FUEL_OUTCOME_TEXT_FORMAT, fuelData.powerProvide),
                        buildingId = buildingId,
                        craftId = fuelId,
                        sort = fuelData.powerProvide,
                    }
                    table.insert(crafts, info)
                end
            end
        end

        table.sort(crafts, Utils.genSortFunction({"sort"}, true))
    elseif bType == GEnums.FacBuildingType.Hub or bType == GEnums.FacBuildingType.SubHub then
        local sys = GameInstance.player.facSpMachineSystem
        for craftId, data in pairs(Tables.factoryHubCraftTable) do
            if ignoreUnlock or sys:IsCraftUnlocked(craftId) then
                if justId then
                    table.insert(crafts, craftId)
                else
                    local info = FactoryUtils.parseHubCraftData(craftId)
                    info.buildingId = buildingId
                    table.insert(crafts, info)
                end
            end
        end
    elseif bType == GEnums.FacBuildingType.Miner then
        if ignoreUnlock or inventory:IsItemFound(FactoryUtils.getBuildingItemId(buildingId)) then
            local minerData = Tables.factoryMinerTable:GetValue(buildingId)
            for _, mineable in pairs(minerData.mineable) do
                local mineId = mineable.miningItemId
                if justId then
                    table.insert(crafts, mineId)
                else
                    table.insert(crafts, FactoryUtils.parseMinerCraftData(buildingId, mineable))
                end
            end
        end
    elseif bType == GEnums.FacBuildingType.MachineCrafter
        or bType == GEnums.FacBuildingType.FluidReaction
        or bType == GEnums.FacBuildingType.MachineWithActivator then
        local machineCrafterData = Tables.factoryMachineCrafterTable:GetValue(buildingId)
        for i = 0, machineCrafterData.modeMap.Count - 1 do
            local curModeItem = machineCrafterData.modeMap[i]
            local isModeUnlocked = GameInstance.player.remoteFactory.core:IsBuildingModeUnlocked(curModeItem.modeName, buildingId)
            local addOtherMode = allModes and isModeUnlocked
            if not producerMode or curModeItem.modeName == producerMode or addOtherMode then
                local machineCrafterGroupData = Tables.factoryMachineCraftGroupTable:GetValue(curModeItem.groupName)
                for _, craftId in pairs(machineCrafterGroupData.craftList) do
                    if ignoreUnlock or facCore:IsFormulaVisible(craftId) then
                        if justId then
                            table.insert(crafts, craftId)
                        else
                            table.insert(crafts, FactoryUtils.parseMachineCraftData(craftId, curModeItem.groupName))
                        end
                    end
                end
                if producerMode and not allModes then
                    break
                end
            end
        end
    elseif bType == GEnums.FacBuildingType.FluidPumpIn then
        local fluidPumpInDataSuccess, fluidPumpInData = Tables.factoryFluidPumpInTable:TryGetValue(buildingId)
        if fluidPumpInDataSuccess then
            local time = fluidPumpInData.msPerRound * 0.001
            for i = 0, fluidPumpInData.enableLiquidIds.Count - 1 do
                local liquidItemId = fluidPumpInData.enableLiquidIds[i]
                local liquidPreFix = "liquid"
                local liquidItemSubString = string.sub(liquidItemId, string.find(liquidItemId, liquidPreFix) + #liquidPreFix)
                local liquidPointItemId = string.format("item_liquidpoint%s", liquidItemSubString)
                local liquidPointSuccess, liquidPointItemData = Tables.itemTable:TryGetValue(liquidPointItemId)
                if liquidPointSuccess then
                    if justId then
                        table.insert(crafts, liquidItemId)
                    else
                        local incomesId = liquidPointItemId
                        local info = {
                            time = time,
                            incomes = { { id = incomesId, count = 1 } },
                            outcomes = { { id = liquidItemId, count = 1 } },
                            buildingId = buildingId,
                            craftId = liquidItemId,
                        }
                        table.insert(crafts, info)
                    end
                end
            end
        end
    elseif bType == GEnums.FacBuildingType.FluidConsume then
        local consumeSuccess, consumeData = Tables.factoryFluidConsumeTable:TryGetValue(buildingId)
        if consumeSuccess then
            local time = consumeData.msPerRound * 0.001
            for index = 0, consumeData.liquidable.Count - 1 do
                local liquidItemId = consumeData.liquidable[index]
                if justId then
                    table.insert(crafts, liquidItemId)
                else
                    local incomesId = liquidItemId
                    local info = {
                        time = time,
                        incomes = { { id = incomesId, count = 1 } },
                        buildingId = buildingId,
                        craftId = liquidItemId,
                        useFinish = true,
                    }
                    table.insert(crafts, info)
                end
            end
        end
    elseif bType == GEnums.FacBuildingType.SewageTreatPlantImport then
        local consumeSuccess, consumeData = Tables.factorySewageTreatImportTable:TryGetValue(buildingId)
        if consumeSuccess then
            local time = consumeData.msPerRound * 0.001
            for index = 0, consumeData.liquidable.Count - 1 do
                local liquidItemId = consumeData.liquidable[index]
                if justId then
                    table.insert(crafts, liquidItemId)
                else
                    local count = 1
                    if time < 1 then
                        count = math.ceil(1 / time)
                        time = 1
                    end
                    local incomesId = liquidItemId
                    local info = {
                        time = time,
                        incomes = { { id = incomesId, count = count } },
                        buildingId = buildingId,
                        craftId = liquidItemId,
                        useFinish = true,
                    }
                    table.insert(crafts, info)
                end
            end
        end
    elseif bType == GEnums.FacBuildingType.SewageTreatPlantExport then
        local consumeSuccess, consumeData = Tables.factorySewageTreatImportTable:TryGetValue(FacConst.FAC_SEWAGE_TREAT_IMPORTER_BUILDING_ID)
        local produceSuccess, produceData = Tables.factorySewageTreatExportTable:TryGetValue(buildingId)
        if consumeSuccess and produceSuccess then
            local consumeItemId = consumeData.liquidable[0]
            local productItemId = produceData.productItemId
            table.insert(crafts, {
                incomes = { { id = consumeItemId, count = produceData.countCost } },
                outcomes = { { id = productItemId, count = produceData.countProduce } },
                buildingId = buildingId,
                craftId = productItemId,
                isSewageCraft = true,
            })
        end
    elseif bType == GEnums.FacBuildingType.EnvGenWithActivator then
        local envGenSuccess, envGenData = Tables.factoryVaporizerTable:TryGetValue(buildingId)
        if envGenSuccess then
            for _, groupData in pairs(envGenData.groups) do
                table.insert(crafts, {
                    incomes = { { id = groupData.consumeItem } },
                    consumeRate = groupData.consumeRate,
                    genEnv = groupData.genEnv,
                    craftId = buildingId .. groupData.genEnv:ToString()
                })
        end
    end
    elseif bType == GEnums.FacBuildingType.GasMiner then
        local success, gasMinerData = Tables.factoryGasMinerTable:TryGetValue(buildingId)
        if success then
            local time = gasMinerData.msPerRound * 0.001
            for i = 0, gasMinerData.mineable.Count - 1 do
                local gasItemId = gasMinerData.mineable[i].miningItemId
                local gasPreFix = "gas"
                local gasItemSubString = string.sub(gasItemId, string.find(gasItemId, gasPreFix) + #gasPreFix)
                local gasPointItemId = string.format("item_gaspoint%s", gasItemSubString)
                local gasPointSuccess, gasPointItemData = Tables.itemTable:TryGetValue(gasPointItemId)
                if gasPointSuccess then
                    if justId then
                        table.insert(crafts, gasPointItemId)
                    else
                        local incomesId = gasPointItemId
                        local info = {
                            time = time,
                            incomes = { { id = incomesId, count = 1 } },
                            outcomes = { { id = gasItemId, count = 1 } },
                            buildingId = buildingId,
                            craftId = gasItemId,
                        }
                        table.insert(crafts, info)
                    end
                end
            end
        end
    end
    return crafts, bType
end

function FactoryUtils.getBuildingCraftsWithNodeId(nodeId, ignoreUnlock, justId, allModes)
    local node = FactoryUtils.getBuildingNodeHandler(nodeId)
    local buildingId = node.templateId
    local formulaManComponentPosition = GEnums.FCComponentPos.FormulaMan:GetHashCode()
    local formulaManComponent = node:GetComponentInPosition(formulaManComponentPosition)
    local currentMode = formulaManComponent ~= nil and formulaManComponent.formulaMan.currentMode or nil

    local result
    local pdp = node.predefinedParam
    if pdp then
        local limitedResult = {}
        local unlockIdList
        if pdp.producer and pdp.producer.limitedFormulaIds.Count > 0 then
            unlockIdList = pdp.producer.limitedFormulaIds
        elseif pdp.fluidReaction and pdp.fluidReaction.visibleFormulas.Count > 0 then
            unlockIdList = pdp.fluidReaction.visibleFormulas
        end
        if unlockIdList then
            result = FactoryUtils.getBuildingCrafts(buildingId, true, justId, currentMode)
            for _, v in ipairs(result) do
                local curId = justId and v or v.craftId
                local found = false
                for i = 0, unlockIdList.Count - 1 do
                    if unlockIdList[i] == curId then
                        found = true
                        break
                    end
                end
                if found then
                    table.insert(limitedResult, v)
                end
            end
            result = limitedResult
        end
    end
    if not result then
        result = FactoryUtils.getBuildingCrafts(buildingId, ignoreUnlock, justId, currentMode, allModes)
    end

    return result
end

function FactoryUtils.checkBuildingHasModeSwitch(buildingId, skipModeUnlockChecking)

    if not FactoryUtils.isDomainSupportPipe() then
        return false, "", ""
    end


    local hasCrafterData, crafterData = Tables.factoryMachineCrafterTable:TryGetValue(buildingId)
    if not hasCrafterData or crafterData.modeMap.Count <= 1 then
        return false, "", ""
    end


    local baseMode, switchMode = "", ""
    local modeMap = {}
    local modeArray = {}
    for index = 0, crafterData.modeMap.Count - 1 do
        local mapData = crafterData.modeMap[index]
        if crafterData.modeUnlockDefaultMap[mapData.modeName] then
            modeMap[mapData.modeName] = index
        else
            local unlockMode = true
            if not skipModeUnlockChecking then
                unlockMode = GameInstance.player.remoteFactory.core:IsBuildingModeUnlocked(mapData.modeName, buildingId)
            end
            if unlockMode then
                modeMap[mapData.modeName] = index
            end
        end
    end
    for modeName, index in pairs(modeMap) do
        if index ~= -1 then
            local success, coverModeData = Tables.factoryMachineCraftModeCoverTable:TryGetValue(modeName)
            if success then
                for i = 0, coverModeData.list.Count - 1 do
                    local coverMode = coverModeData.list[i]
                    if modeMap[coverMode.machineModeCoverType] then
                        modeMap[coverMode.machineModeCoverType] = -1
                    end
                end
            end
        end
    end
    for modeName, index in pairs(modeMap) do
        if index ~= -1 then
            table.insert(modeArray, {
                name = modeName,
                sort = index,
            })
        end
    end
    table.sort(modeArray, Utils.genSortFunction({"sort"}, true))
    if modeArray[1] ~= nil then
        baseMode = modeArray[1].name
    end
    if modeArray[2] ~= nil then
        switchMode = modeArray[2].name
    end

    if string.isEmpty(baseMode) then
        logger.error("错误！在建筑模式对应关系表里，" .. buildingId .. "这个建筑没有默认解锁的模式")
        return false, baseMode, switchMode
    end

    if string.isEmpty(switchMode) then
        return false, baseMode, switchMode
    end

    return true, baseMode, switchMode
end

local SWITCH_LIQUID_MODE_POPUP_TITLE_TEXT_ID = "ui_fac_pipe_mode_close_info_title"
local SWITCH_LIQUID_MODE_POPUP_DESC_TEXT_ID = "ui_fac_pipe_mode_close_info_des"
local SWITCH_LIQUID_MODE_POPUP_TOGGLE_TEXT_ID = "ui_fac_pipe_mode_close_info_choose"
local SWITCH_LIQUID_MODE_POPUP_LOCAL_DATA_KEY = "hide_fac_machine_crafter_mode_switch_pop_up"

function FactoryUtils.checkSwitchBuildingMode(buildingId, currentModeName, baseModeName, switchModeName, onConfirm, onCancel)
    local _, ignorePopUp = ClientDataManagerInst:GetBool(SWITCH_LIQUID_MODE_POPUP_LOCAL_DATA_KEY, false)
    if ignorePopUp or Utils.isInBlackbox() then
        return true
    end

    local showPopup = FactoryUtils.checkSwitchBuildingModePopup(buildingId, currentModeName, baseModeName, switchModeName)
    if not showPopup then
        return true
    end

    local suppressModeSwitchPopUp = false
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language[SWITCH_LIQUID_MODE_POPUP_TITLE_TEXT_ID],
        subContent = string.format(UIConst.COLOR_STRING_FORMAT,
            UIConst.COUNT_RED_COLOR_STR,
            Language[SWITCH_LIQUID_MODE_POPUP_DESC_TEXT_ID]),
        onConfirm = function()
            if suppressModeSwitchPopUp then
                ClientDataManagerInst:SetBool(SWITCH_LIQUID_MODE_POPUP_LOCAL_DATA_KEY, true, false)
            end
            onConfirm()
        end,
        onCancel = onCancel,
        toggle = {
            onValueChanged = function(isOn)
                suppressModeSwitchPopUp = isOn
            end,
            toggleText = Language[SWITCH_LIQUID_MODE_POPUP_TOGGLE_TEXT_ID],
            isOn = false,
        },
    })
    return false
end

function FactoryUtils.checkSwitchBuildingModePopup(buildingId, currentModeName, baseModeName, switchModeName)
    local success, machineCrafterData = Tables.factoryMachineCrafterTable:TryGetValue(buildingId)
    if not success then
        return false
    end


    local baseCountData = { 0, 0, 0, 0 }
    local switchCountData = { 0, 0, 0, 0 }
    for i = 0, machineCrafterData.modeMap.Count - 1 do
        local curModeItem = machineCrafterData.modeMap[i]
        local countData
        if curModeItem.modeName == baseModeName then
            countData = baseCountData
        elseif curModeItem.modeName == switchModeName then
            countData = switchCountData
        end
        if countData then
            local groupData = Tables.factoryMachineCraftGroupTable:GetValue(curModeItem.groupName)
            if groupData.craftList.Count > 0 then
                local craftData = Tables.factoryMachineCraftTable:GetValue(groupData.craftList[0])
                for _, itemBundleGroup in pairs(craftData.ingredients) do
                    for _, itemBundle in pairs(itemBundleGroup.group) do
                        local cacheType = FactoryUtils.getFactoryItemCacheState(itemBundle.id)
                        if cacheType == GEnums.FCItemCacheType.Liquid then
                            countData[1] = countData[1] + 1
                        elseif cacheType == GEnums.FCItemCacheType.Gas then
                            countData[2] = countData[2] + 1
                        end
                    end
                end
                for _, itemBundleGroup in pairs(craftData.outcomes) do
                    for _, itemBundle in pairs(itemBundleGroup.group) do
                        local cacheType = FactoryUtils.getFactoryItemCacheState(itemBundle.id)
                        if cacheType == GEnums.FCItemCacheType.Liquid then
                            countData[3] = countData[3] + 1
                        elseif cacheType == GEnums.FCItemCacheType.Gas then
                            countData[4] = countData[4] + 1
                        end
                    end
                end
            end
        end
    end

    for i = 1, 4 do
        if baseCountData[i] > switchCountData[i] and currentModeName == baseModeName then
            return true
        elseif baseCountData[i] < switchCountData[i] and currentModeName == switchModeName then
            return true
        end
    end
    return false
end

function FactoryUtils.checkIsBuildingModeEnvRelated(buildingId, modeName)
    local crafterData = Tables.factoryMachineCrafterTable:GetValue(buildingId)
    for i = 0, crafterData.modeMap.Count -1 do
        local modeMapItem = crafterData.modeMap[i]
        if modeMapItem.modeName == modeName then
            return modeMapItem.isEnvMode
        end
    end
    return false
end



function FactoryUtils.getBuildingCurrentEnvironment(nodeHandler)
    if nodeHandler == nil then
        return 0, false
    end

    local envComponent = nodeHandler:GetComponentInPosition(GEnums.FCComponentPos.EnvReceiver:GetHashCode())
    if envComponent == nil then
        return 0, false
    end

    return envComponent.envReceiver.currentEnv, true
end

function FactoryUtils.getMachineCraftGroupData(buildingId, modeName)
    local crafterData = Tables.factoryMachineCrafterTable:GetValue(buildingId)
    for i = 0, crafterData.modeMap.Count - 1 do
        local modeMapItem = crafterData.modeMap[i]
        if modeMapItem.modeName == modeName then
            return Tables.factoryMachineCraftGroupTable:GetValue(modeMapItem.groupName)
        end
    end
end





function FactoryUtils.isMachineCraftInMode(buildingId, modeName, craftId)
    if craftId == nil or string.isEmpty(craftId) then
        return true
    end
    local groupData = FactoryUtils.getMachineCraftGroupData(buildingId, modeName)
    if groupData == nil then
        logger.error(string.format(
            "[Factory] isMachineCraftInMode: no craft group buildingId=%s modeName=%s craftId=%s",
            tostring(buildingId), tostring(modeName), tostring(craftId)))
        return false
    end
    for _, id in pairs(groupData.craftList) do
        if id == craftId then
            return true
        end
    end
    return false
end

function FactoryUtils.getMachineCraftGroupDataFromNodeHandler(nodeHandler)
    local buildingId = nodeHandler.templateId
    local formulaManComponentPosition = GEnums.FCComponentPos.FormulaMan:GetHashCode()
    local formulaManComponent = nodeHandler:GetComponentInPosition(formulaManComponentPosition)
    local currentMode = formulaManComponent.formulaMan.currentMode
    return FactoryUtils.getMachineCraftGroupData(buildingId, currentMode)
end







function FactoryUtils.getItemCrafts(itemId, ignoreUnlock, includeMiner, includeFluidPumpIn, includeGas)
    local crafts = {}
    local canCraft = false

    local facCore = GameInstance.player.remoteFactory.core
    local inventory = GameInstance.player.inventory

    local buildingSortFunc = Utils.genSortFunction({"sortId1", "sortId2"}, true)

    do

        local hasCraft, craftIds = Tables.factoryItemAsMachineCrafterOutcomeTable:TryGetValue(itemId)
        if hasCraft then
            canCraft = true
            local sortList = {}
            for _, craftId in pairs(craftIds.list) do
                if ignoreUnlock or facCore:IsFormulaVisible(craftId) then
                    local suc, craftCfgData = Tables.factoryMachineCraftTable:TryGetValue(craftId)
                    if suc then
                        table.insert(sortList, {id = craftId, sortId1 = craftCfgData.sortId})
                    end
                end
            end
            table.sort(sortList, buildingSortFunc)
            for sortIdx, sortData in ipairs(sortList) do
                table.insert(crafts, FactoryUtils.parseMachineCraftData(sortData.id))
            end
        end
    end

    do

        local hasCraft, craftIds = Tables.factoryItemAsHubCraftOutcomeTable:TryGetValue(itemId)
        if hasCraft then
            canCraft = true
            local sys = GameInstance.player.facSpMachineSystem
            for _, craftId in pairs(craftIds.list) do
                if ignoreUnlock or sys:IsCraftUnlocked(craftId) then
                    table.insert(crafts, FactoryUtils.parseHubCraftData(craftId, true))
                end
            end
        end
    end

    do

        local hasCraft, craftIds = Tables.factoryItemAsManualCraftOutcomeTable:TryGetValue(itemId)
        if hasCraft then
            canCraft = true
            local sys = GameInstance.player.facManualCraft
            for _, craftId in pairs(craftIds.list) do
                if ignoreUnlock or sys:IsCraftUnlocked(craftId) then
                    table.insert(crafts, FactoryUtils.parseManualCraftData(craftId, true))
                end
            end
        end
    end

    do

        if includeMiner then
            local minerSortData = {}
            for buildingId, minerData in pairs(Tables.factoryMinerTable) do
                local buildingItemId = FactoryUtils.getBuildingItemId(buildingId)
                local isUnlock = inventory:IsItemFound(buildingItemId)
                local _, itemData = Tables.itemTable:TryGetValue(buildingItemId)
                if itemData then
                    for idx = 0, minerData.mineable.Count - 1 do
                        local mineable = minerData.mineable[idx]
                        if mineable.miningItemId == itemId then
                            if ignoreUnlock or isUnlock then
                                canCraft = true
                                table.insert(minerSortData, {
                                    sortId1 = itemData.sortId1,
                                    sortId2 = itemData.sortId2,
                                    data = FactoryUtils.parseMinerCraftData(buildingId, mineable),
                                })
                                break
                            end
                        end
                    end
                end
            end
            table.sort(minerSortData, buildingSortFunc)
            for sortIdx, sortData in ipairs(minerSortData) do
                table.insert(crafts, sortData.data)
            end
        end
    end

    do

        if includeGas then
            local gasSortData = {}
            for buildingId, gasMinerData in pairs(Tables.factoryGasMinerTable) do
                local buildingItemId = FactoryUtils.getBuildingItemId(buildingId)
                local isUnlock = inventory:IsItemFound(buildingItemId)
                local _, itemData = Tables.itemTable:TryGetValue(buildingItemId)
                if itemData then
                    for idx = 0, gasMinerData.mineable.Count - 1 do
                        local mineable = gasMinerData.mineable[idx]
                        if mineable.miningItemId == itemId then
                            if ignoreUnlock or isUnlock then
                                local info = FactoryUtils.parseGasMinerCraftData(buildingId, itemId)
                                if info then
                                    canCraft = true
                                    table.insert(gasSortData, {
                                        sortId1 = itemData.sortId1,
                                        sortId2 = itemData.sortId2,
                                        data = info,
                                    })
                                end
                                break
                            end
                        end
                    end
                end
            end
            table.sort(gasSortData, buildingSortFunc)
            for sortIdx, sortData in ipairs(gasSortData) do
                table.insert(crafts, sortData.data)
            end
        end
    end

    do

        if includeFluidPumpIn then
            local _, liquidData = Tables.liquidTable:TryGetValue(itemId)
            if liquidData then
                local pumpInSortData = {}
                for buildingId, fluidPumpInData in pairs(Tables.factoryFluidPumpInTable) do
                    local buildingItemId = FactoryUtils.getBuildingItemId(buildingId)
                    local _, itemData = Tables.itemTable:TryGetValue(buildingItemId)
                    if itemData and lume.find(fluidPumpInData.enableLiquidIds, itemId) then
                        local info = FactoryUtils.parseLiquidCraftData(buildingId, itemId)
                        if info then
                            canCraft = true
                            table.insert(pumpInSortData, {
                                sortId1 = itemData.sortId1,
                                sortId2 = itemData.sortId2,
                                data = info,
                            })
                        end
                    end
                end
                table.sort(pumpInSortData, buildingSortFunc)
                for sortIdx, sortData in ipairs(pumpInSortData) do
                    table.insert(crafts, sortData.data)
                end
            end
        end
    end

    return crafts, canCraft
end


function FactoryUtils.getItemProductItemList(itemId, skipItemTable)
    local itemMap = {}
    local itemList = {}
    local itemData = Tables.itemTable[itemId]
    local itemType = itemData.type
    local inv = GameInstance.player.inventory
    local ignoreUnlock = itemType == GEnums.ItemType.Blueprint

    if not skipItemTable then
        for _, id in pairs(itemData.outcomeItemIds) do
            if ignoreUnlock or inv:IsItemFound(id) then
                itemMap[id] = true
                table.insert(itemList, id)
            end
        end
    end



    local skipFindFormula = not skipItemTable and itemData.outcomeItemIds.Count > 0
    if not skipFindFormula then
        local extraItemIds, buildingId
        if itemType == GEnums.ItemType.Material then
            extraItemIds = FactoryUtils.getItemOutputItemIds(itemId, false)
        elseif itemType == GEnums.ItemType.NormalBuilding or itemType == GEnums.ItemType.FuncBuilding then
            buildingId = FactoryUtils.getItemBuildingId(itemId)
        elseif itemType == GEnums.ItemType.Blueprint then
            local succ, d = Tables.machineBlueprint2MachineItemTable:TryGetValue(itemId)
            if succ then
                buildingId = FactoryUtils.getItemBuildingId(d.itemId)
            end
        end

        if buildingId then
            local crafts, bType = FactoryUtils.getBuildingCrafts(buildingId, ignoreUnlock, true, nil)
            if bType == GEnums.FacBuildingType.Miner then
                extraItemIds = crafts
            elseif bType == GEnums.FacBuildingType.MachineCrafter then
                extraItemIds = {}
                for _, craftId in ipairs(crafts) do
                    local craftData = Tables.factoryMachineCraftTable:GetValue(craftId)
                    for _, itemBundleGroup in pairs(craftData.outcomes) do
                        for _, itemBundle in pairs(itemBundleGroup.group) do
                            table.insert(extraItemIds, itemBundle.id)
                        end
                    end
                end
            end
        end

        if extraItemIds then
            for _, id in ipairs(extraItemIds) do
                if not itemMap[id] then
                    itemMap[id] = true
                    table.insert(itemList, id)
                end
            end
        end
    end

    if next(itemList) then
        return itemList
    else
        return nil
    end
end












function FactoryUtils.parseMachineCraftData(craftId, formulaGroupId)
    local craftData = Tables.factoryMachineCraftTable:GetValue(craftId)
    if formulaGroupId == nil then
        formulaGroupId = FactoryUtils.getFormulaGroupId(craftId)
    end
    if formulaGroupId == nil and not string.isEmpty(craftData.formulaGroupId) then
        formulaGroupId = craftData.formulaGroupId
    end
    local machineCraftGroupData = Tables.factoryMachineCraftGroupTable:GetValue(formulaGroupId)
    local machineCraftData = Tables.factoryMachineCrafterTable:GetValue(craftData.machineId)
    local formulaMode = FacConst.FAC_FORMULA_MODE_MAP.NORMAL
    for index = 0, machineCraftData.modeMap.Count - 1 do
        local mapData = machineCraftData.modeMap[index]
        if mapData ~= nil and mapData.groupName == formulaGroupId then
            formulaMode = mapData.modeName
            break
        end
    end

    local info = {
        incomes = {},
        time = craftData.progressRound * machineCraftGroupData.msPerRound * 0.001,
        formulaMode = formulaMode,
        outcomes = {},
        buildingId = craftData.machineId,
        craftId = craftId,
        isUnlock = GameInstance.player.remoteFactory.core:IsFormulaVisible(craftId),
        env = craftData.gasEnv,
    }
    for _, itemBundleGroup in pairs(craftData.ingredients) do
        for _, itemBundle in pairs(itemBundleGroup.group) do
            table.insert(info.incomes, { id = itemBundle.id, count = itemBundle.count, buffer = craftData.buffers:GetValue(itemBundle.id) })
        end
    end
    for _, itemBundleGroup in pairs(craftData.outcomes) do
        for _, itemBundle in pairs(itemBundleGroup.group) do
            table.insert(info.outcomes, { id = itemBundle.id, count = itemBundle.count, buffer = craftData.buffers:GetValue(itemBundle.id) })
        end
    end

    return info
end


function FactoryUtils.parseHubCraftData(craftId, findBuilding)
    local craftData = Tables.factoryHubCraftTable:GetValue(craftId)
    local info = {
        incomes = {},
        outcomes = {},
        craftId = craftId,
        isUnlock = GameInstance.player.facSpMachineSystem:IsCraftUnlocked(craftId),
    }
    for _, itemBundle in pairs(craftData.ingredients) do
        table.insert(info.incomes, { id = itemBundle.id, count = itemBundle.count })
    end
    for _, itemBundle in pairs(craftData.outcomes) do
        table.insert(info.outcomes, { id = itemBundle.id, count = itemBundle.count })
    end
    if findBuilding then
        info.buildingId = FacConst.HUB_DATA_ID
    end
    return info
end


function FactoryUtils.parseManualCraftData(craftId, findBuilding)
    local craftData = Tables.factoryManualCraftTable:GetValue(craftId)
    local info = {
        incomes = {},
        outcomes = {},
        craftId = craftId,
        isUnlock = GameInstance.player.facManualCraft:IsCraftUnlocked(craftId)
    }
    for _, itemBundle in pairs(craftData.ingredients) do
        table.insert(info.incomes, { id = itemBundle.id, count = itemBundle.count })
    end
    for _, itemBundle in pairs(craftData.outcomes) do
        table.insert(info.outcomes, { id = itemBundle.id, count = itemBundle.count })
    end
    return info
end





function FactoryUtils.parseMinerCraftData(buildingId, mineable)
    local minerData = Tables.factoryMinerTable:GetValue(buildingId)
    local mineId = mineable.miningItemId
    local incomesId = "item_minepoint"..string.sub(mineId, string.find(mineId, "_"), -1)
    local minerTime = minerData.msPerRound / mineable.produceRate * 0.001
    local newIncomes = {}
    local consumeItemId = mineable.consumeItem.id
    local consumeItemCount = mineable.consumeItem.count
    if not consumeItemId:isEmpty() and consumeItemCount > 0 then
        table.insert(newIncomes, { id = consumeItemId, count = consumeItemCount })
    end
    table.insert(newIncomes, { id = incomesId, count = 1 })
    local info = {
        time = minerTime,
        incomes = newIncomes,
        outcomes = { { id = mineId, count = 1 } },
        buildingId = buildingId,
        craftId = string.format("%s_%s", mineId, buildingId),
    }
    return info
end





function FactoryUtils.parseLiquidCraftData(buildingId, liquidItemId)
    local _, fluidPumpInData = Tables.factoryFluidPumpInTable:TryGetValue(buildingId)
    local _, liquidData = Tables.liquidTable:TryGetValue(liquidItemId)
    if not fluidPumpInData or not liquidData then
        return nil
    end
    local liquidPreFix = "liquid"
    local liquidItemSubString = string.sub(liquidItemId, string.find(liquidItemId, liquidPreFix) + #liquidPreFix)
    local liquidPointItemId = string.format("item_liquidpoint%s", liquidItemSubString)
    local _, liquidPointItemData = Tables.itemTable:TryGetValue(liquidPointItemId)
    if not liquidPointItemData then
        return nil
    end
    local info = {
        time = fluidPumpInData.msPerRound * 0.001,
        incomes = { { id = liquidPointItemId, count = 1 } },
        outcomes = { { id = liquidItemId, count = 1 } },
        buildingId = buildingId,
        craftId = string.format("%s_%s", liquidItemId, buildingId),
    }
    return info
end





function FactoryUtils.parseGasMinerCraftData(buildingId, gasItemId)
    local _, gasMinerData = Tables.factoryGasMinerTable:TryGetValue(buildingId)
    if not gasMinerData then
        return nil
    end
    local gasPreFix = "gas"
    local gasItemSubString = string.sub(gasItemId, string.find(gasItemId, gasPreFix) + #gasPreFix)
    local gasPointItemId = string.format("item_gaspoint%s", gasItemSubString)
    local hasPoint = Tables.itemTable:TryGetValue(gasPointItemId)
    if not hasPoint then
        return nil
    end
    local info = {
        time = gasMinerData.msPerRound * 0.001,
        incomes = { { id = gasPointItemId, count = 1 } },
        outcomes = { { id = gasItemId, count = 1 } },
        buildingId = buildingId,
        craftId = string.format("%s_%s", gasItemId, buildingId),
    }
    return info
end

function FactoryUtils.isSpecialBuilding(buildingId)
    local buildingData = Tables.factoryBuildingTable:GetValue(buildingId)
    local isSpBuilding = lume.find(FacConst.SP_BUILDING_TYPES, buildingData.type) ~= nil
    return isSpBuilding
end

function FactoryUtils.isInTopView()
    return LuaSystemManager.factory.inTopView
end

function FactoryUtils.isMachineTargetShown()
    local ctrl = UIManager.cfgs.FacMainLeft.ctrl
    return ctrl and ctrl.showMachineTarget or false
end

function FactoryUtils.canPlaceBuildingOnCurRegion(buildingId)
    if not Utils.isCurrentMapHasFactoryGrid() then
        return false
    end
    local isInMainRegion = GameInstance.remoteFactoryManager:IsPlayerPositionInMainRegion()
    if isInMainRegion then
        return true
    end
    local buildingData = Tables.factoryBuildingTable:GetValue(buildingId)
    if buildingData.type == GEnums.FacBuildingType.SubHub then
        return true
    end
    return not buildingData.onlyShowOnMain
end


function FactoryUtils.getCurRegionInfo()
    return GameInstance.remoteFactoryManager.system.core.currentScope
end


function FactoryUtils.getCurChapterInfo()
    return GameInstance.player.remoteFactory.core:GetChapterInfoById(Utils.getCurrentChapterId())
end


function FactoryUtils.getCurRegionPowerInfo()
    local chapterInfo = FactoryUtils.getCurChapterInfo()
    if chapterInfo then
        return chapterInfo.blackboard.power
    end
end

function FactoryUtils.getIsInBackupPower(chapterId)
    local powerInfo
    if chapterId then
        powerInfo = FactoryUtils.getRegionPowerInfoByChapterId(chapterId)
    else
        powerInfo = FactoryUtils.getCurRegionPowerInfo()
    end
    if powerInfo == nil then
        return false
    end
    return DateTimeUtils.GetCurrentTimestampBySeconds() < powerInfo.backupLastStartTs + Tables.factoryConst.facBackUpPowerDuration
end

function FactoryUtils.getIsBackupPowerUnderRecovery(chapterId)
    local powerInfo
    if chapterId then
        powerInfo = FactoryUtils.getRegionPowerInfoByChapterId(chapterId)
    else
        powerInfo = FactoryUtils.getCurRegionPowerInfo()
    end
    if powerInfo == nil then
        return false
    end
    return DateTimeUtils.GetCurrentTimestampBySeconds() < powerInfo.backupLastStopTs + Tables.factoryConst.facBackUpPowerCooldownTime
end

function FactoryUtils.getRegionPowerInfoByChapterId(chapterId)
    local chapterInfo = GameInstance.remoteFactoryManager.system.core:GetChapterInfoById(chapterId)
    if chapterInfo == nil then
        return nil
    end
    return chapterInfo.blackboard.power
end

function FactoryUtils.getMedicProgress(nodeId)
    return GameInstance.remoteFactoryManager.medicalTowerManager:GetCurrentProgress(nodeId)
end


function FactoryUtils.findNearestBuilding(buildingId, ignoreCull)

    local playerPos = GameInstance.playerController.mainCharacter.position
    return CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryManager.FindNearestBuilding(buildingId, playerPos, ignoreCull == true)
end



function FactoryUtils.queryVoxelRangeHeightAdjust(posX, posY, posZ)
    return CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryUtil.VoxelRangeHeightAdjust(
        CS.UnityEngine.RectInt(posX, posZ, 1, 1), posY)
end


function FactoryUtils.getCurSceneHandler()
    return CSFactoryUtil.GetSceneHandler()
end

function FactoryUtils.isPlayerOutOfRangeManual()
    local level = PhaseManager.m_openedPhaseSet[PhaseId.Level]
    if not level then
        return true
    end
    return level.isPlayerOutOfRangeManual
end

function FactoryUtils.canPlayerEnterFacMode()
    local level = PhaseManager.m_openedPhaseSet[PhaseId.Level]
    if not level then
        return false
    end
    return not (level.isPlayerOutOfRangeManual or GameWorld.battle.isSquadInFight)
end

function FactoryUtils.clampTopViewCamTargetPosition(worldPos, curWorldPos)
    local level = PhaseManager.m_openedPhaseSet[PhaseId.Level]
    if not level then
        return curWorldPos, false
    end
    if level.m_lastLevelIdNum ~= GameWorld.worldInfo.curLevelIdNum then
        logger.critical("FactoryUtils.clampTopViewCamTargetPosition m_lastLevelIdNum ~= curLevelIdNum", level.m_lastLevelIdNum, GameWorld.worldInfo.curLevelIdNum)
        return curWorldPos, false
    end
    local rect
    if level.customFacTopViewRangeInWorld then
        rect = level.customFacTopViewRangeInWorld
    else
        rect = level.mainRegionLocalRectWithMovePadding
        if rect and (rect.width == 0 or rect.width == 0) then
            logger.critical("FactoryUtils.clampTopViewCamTargetPosition rect IS ZERO", GameWorld.worldInfo.curMapIdStr, GameWorld.worldInfo.curLevelId)
            local _, panelIndex = Utils.isInFacMainRegionAndGetIndex()
            level:_UpdateCurMainRegionInfo(panelIndex)
            return curWorldPos, false
        end
    end
    if not rect then
        return curWorldPos, false
    end



    local mainCamera = CameraManager.mainCamera
    local dist = (mainCamera.transform.position - LuaSystemManager.factory.topViewCamTarget.position).y
    local yPadding = math.min(dist * math.tan(mainCamera.fieldOfView / 2 * math.pi / 180), rect.height / 2)
    local xPadding = math.min(yPadding / Screen.height * Screen.width, rect.width / 2)
    if lume.round(LuaSystemManager.factory.topViewCamTarget.transform.eulerAngles.y / 90) % 2 ~= 0 then
        local tmp = xPadding
        xPadding = yPadding
        yPadding = tmp
    end






    rect = Unity.Rect(rect.x + xPadding, rect.y + yPadding, math.max(0, rect.width - xPadding * 2), math.max(0, rect.height - yPadding * 2))

    local regionTransform, localPos, curLocalPos
    if not level.customFacTopViewRangeInWorld then
        regionTransform = GameInstance.remoteFactoryManager.gameWorldAgent:GetRegionRootTransform()
        localPos = regionTransform:InverseTransformPoint(worldPos)
        curLocalPos = regionTransform:InverseTransformPoint(curWorldPos)
    else
        localPos = worldPos
        curLocalPos = curWorldPos
    end
    if rect:Contains(localPos:XZ()) then
        return worldPos, false
    else
        local xMin, xMax, yMin, yMax
        if curWorldPos then
            xMin = math.min(rect.xMin, curLocalPos.x)
            xMax = math.max(rect.xMax, curLocalPos.x)
            yMin = math.min(rect.yMin, curLocalPos.z)
            yMax = math.max(rect.yMax, curLocalPos.z)
        else
            xMin = rect.xMin
            xMax = rect.xMax
            yMin = rect.yMin
            yMax = rect.yMax
        end
        localPos.x = lume.clamp(localPos.x, xMin, xMax)
        localPos.z = lume.clamp(localPos.z, yMin, yMax)
        if regionTransform then
            return regionTransform:TransformPoint(localPos), true
        else
            return localPos, false
        end
    end
end

function FactoryUtils.gameEventFactoryItemPush(nodeId, itemId, count, curItems)
    local buildingNode = FactoryUtils.getBuildingNodeHandler(nodeId)
    local worldPos = GameInstance.remoteFactoryManager.visual:BuildingGridToWorld(
            Vector2(buildingNode.transform.position.x, buildingNode.transform.position.z))
    EventLogManagerInst:GameEvent_FactoryItemPush(buildingNode.nodeId, buildingNode.templateId,
            GameInstance.remoteFactoryManager.currentSceneName, worldPos,
            itemId, count, curItems)
end

function FactoryUtils.getBuildingPortState(nodeId, isPipePort)
    if nodeId <= 0 then
        return
    end

    local facManager = GameInstance.remoteFactoryManager
    local success, complexPortFragment = facManager:TrySamplePortInfo(Utils.getCurrentChapterId(), nodeId)
    if not success then
        return
    end

    local inPortInfoList, outPortInfoList = {}, {}

    for index = 0, complexPortFragment.ports.length - 1 do
        local portData = complexPortFragment.ports:GetValue(index)
        if portData.isUsable and portData.isPipe == isPipePort then
            local infoList = portData.isInput and inPortInfoList or outPortInfoList
            table.insert(infoList, {
                index = portData.idx,
                touchCompId = portData.touchComId,
                touchNodeId = portData.touchNodeId,
                isBinding = portData.touchNodeId > 0,
                isBlock = portData.isBlock,
            })
        end
    end

    local sortFunc = Utils.genSortFunction({"index"}, true)
    table.sort(inPortInfoList, sortFunc)
    table.sort(outPortInfoList, sortFunc)

    return inPortInfoList, outPortInfoList
end

function FactoryUtils.getBuildingTypeByBuildingId(buildingId)
    local success, buildingData = Tables.factoryBuildingTable:TryGetValue(buildingId)
    if not success then
        return GEnums.FacBuildingType.Empty
    end

    return buildingData.type
end

function FactoryUtils.getBuildingProcessingCraft(buildingInfo)
    if buildingInfo == nil then
        return nil
    end

    local buildingType = FactoryUtils.getBuildingTypeByBuildingId(buildingInfo.buildingId)
    local crafts = FactoryUtils.getBuildingCraftsWithNodeId(buildingInfo.nodeId, true)
    if crafts == nil then
        return nil
    end

    if buildingType == GEnums.FacBuildingType.PowerStation then
        for _, craftInfo in pairs(crafts) do
            if craftInfo.incomes ~= nil and craftInfo.incomes[1].id == buildingInfo.burningItemId then
                return craftInfo
            end
        end
    elseif buildingType == GEnums.FacBuildingType.Miner then
        local collectItemId = buildingInfo.collectingItemId
        if string.isEmpty(collectItemId) and buildingInfo.mineData ~= nil then
            collectItemId = buildingInfo.mineData.itemId
        end
        for _, craftInfo in pairs(crafts) do
            if craftInfo.outcomes ~= nil and craftInfo.outcomes[1].id == collectItemId then
                return craftInfo
            end
        end
    elseif buildingType == GEnums.FacBuildingType.FluidPumpIn then
        for _, craftInfo in pairs(crafts) do
            if craftInfo.outcomes ~= nil and craftInfo.outcomes[1].id == buildingInfo.collectingItemId then
                return craftInfo
            end
        end
    elseif buildingType == GEnums.FacBuildingType.FluidConsume then
        local consumeId = buildingInfo.consumeItemId
        for _, craftInfo in pairs(crafts) do
            if craftInfo.incomes ~= nil and craftInfo.incomes[1].id == consumeId then
                return craftInfo
            end
        end
    elseif buildingType == GEnums.FacBuildingType.SewageTreatPlantImport then
        for _, craftInfo in pairs(crafts) do
            if craftInfo.incomes ~= nil then
                return craftInfo
            end
        end
    elseif buildingType == GEnums.FacBuildingType.SewageTreatPlantExport then
        for _, craftInfo in pairs(crafts) do
            if craftInfo.outcomes ~= nil then
                return craftInfo
            end
        end
    elseif buildingType == GEnums.FacBuildingType.GasMiner then
        for _, craftInfo in pairs(crafts) do
            if craftInfo.outcomes ~= nil and craftInfo.outcomes[1].id == buildingInfo.collectingItemId then
                return craftInfo
            end
        end
    elseif buildingType == GEnums.FacBuildingType.EnvGenWithActivator then
        for _, craftInfo in pairs(crafts) do
            if craftInfo.incomes[1].id == buildingInfo.activatorCost.currentItemId then
                return craftInfo
            end
        end
    else
        for _, craftInfo in pairs(crafts) do
            if craftInfo.craftId == buildingInfo.formulaId or craftInfo.craftId == buildingInfo.lastFormulaId then
                return craftInfo
            end
        end
    end

    return nil
end

function FactoryUtils.getMachineCraftLockFormulaId(nodeId)
    local node = FactoryUtils.getBuildingNodeHandler(nodeId)
    if node == nil then
        return ""
    end

    local pdp = node.predefinedParam
    if pdp == nil then
        return ""
    end

    local producer = pdp.producer
    if producer == nil then
        return ""
    end

    return producer.lockFormulaId
end

function FactoryUtils.isEquipFormulaUnlocked(formulaId)
    return GameInstance.player.equipTechSystem:IsFormulaUnlock(formulaId)
end

function FactoryUtils.isSpMachineFormulaUnlocked(formulaId)
    return GameInstance.player.facSpMachineSystem:IsCraftUnlocked(formulaId)
end

function FactoryUtils.isItemInfiniteInFactoryDepot(itemId)
    local factoryDepot = GameInstance.player.inventory.factoryDepot
    if factoryDepot == nil then
        return false
    end

    local depotInChapter = factoryDepot:GetOrFallback(Utils.getCurrentScope())
    if depotInChapter == nil then
        return false
    end

    local actualDepot = depotInChapter[Utils.getCurrentChapterId()]
    if actualDepot == nil then
        return false
    end

    local success, isInfinite = actualDepot.infiniteItemIds:TryGetValue(itemId)
    if success == false then
        return false
    end

    return isInfinite
end

function FactoryUtils.isBuildingInventoryLocked(nodeId)
    local node = FactoryUtils.getBuildingNodeHandler(nodeId)
    if node == nil then
        return false
    end

    local pdp = node.predefinedParam
    if pdp == nil then
        return false
    end

    local cache, gridBox = pdp.cache, pdp.gridBox
    if cache ~= nil then
        return cache.lockManualInOut
    end
    if gridBox ~= nil then
        return gridBox.lockManualInOut
    end

    return false
end


function FactoryUtils.getLogisticData(templateId)
    local _, data
    do
        _, data = Tables.factoryGridConnecterTable:TryGetValue(templateId)
        if not data then
            _, data = Tables.factoryGridRouterTable:TryGetValue(templateId)
        end
        if not data then
            _, data = Tables.factoryBoxValveTable:TryGetValue(templateId)
        end
        if data then
            return data.gridUnitData, false
        end
    end
    do
        _, data = Tables.factoryLiquidRouterTable:TryGetValue(templateId)
        if not data then
            _, data = Tables.factoryLiquidConnectorTable:TryGetValue(templateId)
        end
        if not data then
            _, data = Tables.factoryFluidValveTable:TryGetValue(templateId)
        end
        if data then
            return data.liquidUnitData, true
        end
    end
    logger.error("No LogisticData", templateId)
end



function FactoryUtils.getFactoryItemCacheState(itemId)
    local success, factoryItemData = Tables.factoryItemTable:TryGetValue(itemId)
    if success == false then
        return nil
    end

    return factoryItemData.phaseType
end


function FactoryUtils.isFactoryItemNormal(itemId)
    local state = FactoryUtils.getFactoryItemCacheState(itemId)
    return state == GEnums.FCItemCacheType.Normal
end

function FactoryUtils.isFactoryItemFluid(itemId)
    local state = FactoryUtils.getFactoryItemCacheState(itemId)
    return state == GEnums.FCItemCacheType.Liquid
end












function FactoryUtils.getMachineCraftCacheLayoutData(nodeId)
    local nodeHandler = FactoryUtils.getBuildingNodeHandler(nodeId)
    if nodeHandler == nil then
        return nil
    end

    local groupData = FactoryUtils.getMachineCraftGroupDataFromNodeHandler(nodeHandler)
    local crafts = FactoryUtils.getBuildingCraftsWithNodeId(nodeId, true, false)
    if groupData == nil or crafts == nil or #crafts == 0 then
        return nil
    end

    local layoutData = {}
    layoutData.normalIncomeCaches = {}
    layoutData.liquidIncomeCaches = {}
    layoutData.normalOutcomeCaches = {}
    layoutData.liquidOutcomeCaches = {}

    local firstCraft = crafts[1]
    for _, income in ipairs(firstCraft.incomes) do
        local itemId = income.id
        local cacheData
        local cacheType = FactoryUtils.getFactoryItemCacheState(itemId)
        if cacheType == GEnums.FCItemCacheType.Normal then
            cacheData = layoutData.normalIncomeCaches
        else
            cacheData = layoutData.liquidIncomeCaches
        end
        local bufferId = LuaIndex(income.buffer)
        if cacheData[bufferId] == nil then
            local data = {
                slotCount = 1,
            }
            cacheData[bufferId] = data
        else
            local slotCount = cacheData[bufferId].slotCount
            cacheData[bufferId].slotCount = slotCount + 1
        end
    end
    for _, outcome in ipairs(firstCraft.outcomes) do
        local itemId = outcome.id
        local cacheData
        local cacheType = FactoryUtils.getFactoryItemCacheState(itemId)
        if cacheType == GEnums.FCItemCacheType.Normal then
            cacheData = layoutData.normalOutcomeCaches
        else
            cacheData = layoutData.liquidOutcomeCaches
        end
        local bufferId = LuaIndex(outcome.buffer)
        if cacheData[bufferId] == nil then
            local data = {
                slotCount = 1,
            }
            cacheData[bufferId] = data
        else
            local slotCount = cacheData[bufferId].slotCount
            cacheData[bufferId].slotCount = slotCount + 1
        end
    end

    local bindingCollector = function(bindingDataList, caches, cacheTypeSlotCount)
        if caches == nil or #caches == 0 then
            return
        end

        local cacheIndex = 1
        for index = 0, bindingDataList.Count - 1 do
            local cacheData = caches[cacheIndex]
            if cacheData == nil then
                logger.error("配方道具数据与建筑数据不匹配")
                return
            end
            local bindingData = bindingDataList[index]
            cacheData.portCount = bindingData.bindingPortIndices.Count
            cacheData.ports = bindingData.bindingPortIndices
            if bindingData.pipePortPhaseType.Count > 0 then
                cacheData.cacheType = 0
                for i = 0, bindingData.pipePortPhaseType.Count - 1 do
                    cacheData.cacheType = cacheData.cacheType + bindingData.pipePortPhaseType[i]:GetHashCode()
                end
            else
                cacheData.cacheType = FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal
            end
            cacheTypeSlotCount[cacheData.cacheType] = cacheTypeSlotCount[cacheData.cacheType] + cacheData.slotCount
            cacheIndex = cacheIndex + 1
        end
    end

    local inSlotCount = {
        [FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal] = 0,
        [FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid] = 0,
        [FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas] = 0,
        [FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid] = 0,
    }
    local outSlotCount = {
        [FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal] = 0,
        [FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid] = 0,
        [FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas] = 0,
        [FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid] = 0,
    }
    bindingCollector(groupData.ingredientBufferBinding, layoutData.normalIncomeCaches, inSlotCount)
    bindingCollector(groupData.outcomeBufferBinding, layoutData.normalOutcomeCaches, outSlotCount)
    bindingCollector(groupData.pipeIngredientBufferBinding, layoutData.liquidIncomeCaches, inSlotCount)
    bindingCollector(groupData.pipeOutcomeBufferBinding, layoutData.liquidOutcomeCaches, outSlotCount)
    layoutData.inSlotCountByType = inSlotCount
    layoutData.outSlotCountByType = outSlotCount

    return layoutData
end

function FactoryUtils.getNodeWorldPos(nodeId)
    local buildingNode = FactoryUtils.getBuildingNodeHandler(nodeId)
    local worldPos = CSFactoryUtil.GetBuildingModelPosition(buildingNode)
    return worldPos
end



function FactoryUtils.updateFacTechTreeTechPointNode(view, facTechPackageId)
    local packageData = Tables.facSTTGroupTable[facTechPackageId]
    local costPointCfg = Tables.itemTable[packageData.costPointType]
    view.textResourceName.text = costPointCfg.name
    view.textResourceNumber.text = Utils.getItemCount(packageData.costPointType)

    local showTips = function()
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = packageData.costPointType,
            transform = view.imgIcon.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            isSideTips = DeviceInfo.usingController,
        })
    end

    view.imgIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, packageData.costPointType)
    view.imgIconButton.onClick:AddListener(function()
        showTips()
    end)

    if view.imgBg then
        view.imgBg.onClick:AddListener(function()
            showTips()
        end)
    end
end

function FactoryUtils.updateFacTechTreeTechPointCount(view, facTechPackageId)
    local packageData = Tables.facSTTGroupTable[facTechPackageId]
    view.textResourceNumber.text = Utils.getItemCount(packageData.costPointType)
end

function FactoryUtils.updateBlackboxCell(view, blackboxId, onClickFunc, fromTechTree)
    local BlackboxCellState = {
        Complete = "complete",
        ManuallyComplete = "manullycomplete",
        Lock = "lock",
        Normal = "normal",

        Active = "active",
        Inactive = "inactive",
    }

    local blackboxCfg = Tables.dungeonTable[blackboxId]
    local blackboxName = blackboxCfg.dungeonName

    local isComplete = DungeonUtils.isDungeonPassed(blackboxId)
    local isUnlock = DungeonUtils.isDungeonUnlock(blackboxId)
    local isActive = DungeonUtils.isDungeonActive(blackboxId)
    local manuallyComplete = GameInstance.dungeonManager:IsDungeonManuallyPassed(blackboxId)

    view.nameTxtS.text = blackboxName
    view.nameTxtN.text = blackboxName

    local state1
    if isActive then
        state1 = BlackboxCellState.Active
    else
        state1 = BlackboxCellState.Inactive
    end
    view.stateController:SetState(state1)

    local state2
    if manuallyComplete and not fromTechTree then
        state2 = BlackboxCellState.ManuallyComplete
    elseif isComplete then
        state2 = BlackboxCellState.Complete
    elseif isActive and not isUnlock then
        state2 = BlackboxCellState.Lock
    else
        state2 = BlackboxCellState.Normal
    end
    view.stateController:SetState(state2)

    if onClickFunc then
        view.button.onClick:RemoveAllListeners()
        view.button.onClick:AddListener(function()
            onClickFunc()
        end)
    end
end

function FactoryUtils.getBlackboxInfoTbl(blackboxIds, ignoreInactiveAndLocked)
    local relativeBlackboxes = {}

    for _, blackboxId in pairs(blackboxIds) do
        local isComplete = DungeonUtils.isDungeonPassed(blackboxId)
        local isUnlock = DungeonUtils.isDungeonUnlock(blackboxId)
        local isActive = DungeonUtils.isDungeonActive(blackboxId)
        if not ignoreInactiveAndLocked or ignoreInactiveAndLocked and isActive and isUnlock then
            local blackboxCfg = Tables.dungeonTable[blackboxId]
            local blackboxInfo = {}
            blackboxInfo.blackboxId = blackboxId

            blackboxInfo.completeSortId = isComplete and 1 or 0
            blackboxInfo.activeSortId = isActive and 0 or 1
            blackboxInfo.unlockSortId = isUnlock and 0 or 1
            blackboxInfo.sortId = blackboxCfg.sortId

            table.insert(relativeBlackboxes, blackboxInfo)
        end
    end
    table.sort(relativeBlackboxes, Utils.genSortFunction({ "completeSortId", "activeSortId", "unlockSortId", "sortId" }, true))

    return relativeBlackboxes
end

function FactoryUtils.genFilterBlackboxArgs(packageName, onFilterConfirmFunc)
    local filter = {}
    filter.tagGroups = {}

    local layerFilter = {}
    layerFilter.title = Language.LUA_FAC_TECH_TREE_BLACKBOX_LIST_FILTER_LAYER_DESC
    layerFilter.tags = {}
    local packageCfg = Tables.facSTTGroupTable[packageName]
    for _, layerId in pairs(packageCfg.layerIds) do
        local layerCfg = Tables.facSTTLayerTable[layerId]
        if not layerCfg.isTBD then
            table.insert(layerFilter.tags, {
                layerId = layerId,
                name = layerCfg.name,
                order = layerCfg.order,
            })
        end
    end
    table.sort(layerFilter.tags, Utils.genSortFunction({ "order" }, true))
    table.insert(filter.tagGroups, layerFilter)

    local categoryFilter = {}
    categoryFilter.title = Language.LUA_FAC_TECH_TREE_BLACKBOX_LIST_FILTER_CATEGORY_DESC
    categoryFilter.tags = {}
    for _, categoryId in pairs(packageCfg.categoryIds) do
        local categoryCfg = Tables.facSTTCategoryTable[categoryId]
        if not GameInstance.player.facTechTreeSystem:CategoryIsHidden(categoryId) then
            table.insert(categoryFilter.tags, {
                categoryId = categoryId,
                name = categoryCfg.name,
                order = categoryCfg.order,
            })
        end
    end
    table.sort(categoryFilter.tags, Utils.genSortFunction({ "order" }, true))
    table.insert(filter.tagGroups, categoryFilter)

    local completeFilter = {}
    completeFilter.title = Language.LUA_FAC_TECH_TREE_BLACKBOX_LIST_FILTER_IS_COMPLETE_DESC
    completeFilter.tags = { { name = Language.LUA_FAC_TECH_TREE_BLACKBOX_LIST_FILTER_STATE_UN_DESC, completeState = false },
                            { name = Language.LUA_FAC_TECH_TREE_BLACKBOX_LIST_FILTER_STATE_COMPLETE_DESC, completeState = true } }
    table.insert(filter.tagGroups, completeFilter)

    filter.onConfirm = function(selectedTags)
        onFilterConfirmFunc(selectedTags)
    end

    filter.getResultCount = function(selectedTags)
        local ids = FactoryUtils.getFilterBlackboxIds(packageName, selectedTags)
        return #ids
    end

    return filter
end

function FactoryUtils.getFilterBlackboxIds(packageName, selectedTags)
    local blackboxIds = {}
    local packageCfg = Tables.facSTTGroupTable[packageName]

    for _, techId in pairs(packageCfg.techIds) do
        local nodeData = Tables.facSTTNodeTable[techId]
        for _, blackboxId in pairs(nodeData.blackboxIds) do

            local isUnlock = DungeonUtils.isDungeonUnlock(blackboxId)
            local isActive = DungeonUtils.isDungeonActive(blackboxId)
            if not isUnlock or not isActive then
                goto continue
            end

            local layerMatch = false
            local hasLayerTag = false
            local categoryMatch = false
            local hasCategoryTag = false
            local completeMatch = false
            local hasCompleteTag = false
            for _, tag in ipairs(selectedTags) do
                if tag.layerId ~= nil then
                    hasLayerTag = true
                end
                if hasLayerTag then
                    layerMatch = layerMatch or nodeData.layer == tag.layerId
                end

                if tag.categoryId ~= nil then
                    hasCategoryTag = true
                end
                if hasCategoryTag then
                    categoryMatch = categoryMatch or nodeData.category == tag.categoryId
                end

                if tag.completeState ~= nil then
                    hasCompleteTag = true
                end
                if hasCompleteTag then
                    completeMatch = completeMatch or GameInstance.dungeonManager:IsDungeonPassed(blackboxId) == tag.completeState
                end
            end

            if not hasLayerTag then
                layerMatch = true
            end

            if not hasCategoryTag then
                categoryMatch = true
            end

            if not hasCompleteTag then
                completeMatch = true
            end

            if layerMatch and categoryMatch and completeMatch and

                    not lume.find(blackboxIds, blackboxId) then
                table.insert(blackboxIds, blackboxId)
            end

            ::continue::
        end
    end

    return blackboxIds
end

function FactoryUtils.checkCanOpenPhaseFacTechTree(arg)
    local facTechTreeSystem = GameInstance.player.facTechTreeSystem
    if arg == nil or next(arg) == nil then

        return true
    end
    if arg.inPackage then

        return true
    end

    local techId = arg.techId
    if not string.isEmpty(techId) then
        local packageId = Tables.facSTTNodeTable[techId].groupId
        local techIsHidden = facTechTreeSystem:NodeIsHidden(techId)
        local packageIsLocked = facTechTreeSystem:PackageIsLocked(packageId)
        if not techIsHidden and not packageIsLocked then

            return true
        else
            return false, Language.LUA_FAC_TECH_TREE_JUMP_FAIL_DESC
        end
    end

    local layerId = arg.layerId
    if not string.isEmpty(layerId) then
        return true
    end

    local packageId = arg.packageId
    if not string.isEmpty(packageId) then
        local hidden = facTechTreeSystem:PackageIsHidden(packageId)
        local locked = facTechTreeSystem:PackageIsLocked(packageId)
        if not hidden and not locked then

            return true
        else
            return false, Language.LUA_FAC_TECH_TREE_JUMP_FAIL_DESC
        end
    end

    logger.error("invalid params, plz check")
    return false
end

function FactoryUtils.getPackageInvestigateProgress(packageName)
    local curProgress, totalProgress = 0, 0
    local packageCfg = Tables.facSTTGroupTable[packageName]
    local fac = GameInstance.player.facTechTreeSystem
    for _, techId in pairs(packageCfg.techIds) do
        if not fac:NodeIsHidden(techId) then
            totalProgress = totalProgress + 1

            if not fac:NodeIsLocked(techId) then
                curProgress = curProgress + 1
            end

        end
    end

    return curProgress, totalProgress
end



function FactoryUtils.enterFacCamera(stateName)
    return GameAction.AddCameraControlState(stateName)
end

function FactoryUtils.exitFacCamera(state)
    GameAction.RemoveCameraControlState(state)
end

function FactoryUtils.getCurOpenedBuildingId()
    local machine = PhaseManager.m_openedPhaseSet[PhaseId.FacMachine]
    if not machine then
        return true
    end
    return machine.m_panelBuildingDataId
end

function FactoryUtils.canShowPipe()
    return GameInstance.remoteFactoryManager.unlockSystem.systemUnlockedPipe and FactoryUtils.isDomainSupportPipe()
end

function FactoryUtils.isDomainSupportPipe()
    return CSFactoryUtil.IsCurDomainSupportPipe()
end

function FactoryUtils.getCraftTimeStr(time, forceFloor)
    if time == nil then
        return ""
    end

    local floorTime = math.floor(time)
    if floorTime == time or forceFloor then
        return tostring(floorTime)
    else
        return string.format("%.1f", time)
    end
end

function FactoryUtils.getMatchedFormulaIdByItemList(buildingId, mode, itemList, env)
    if itemList == nil or #itemList == 0 then
        return ""
    end

    local success, groupData = Tables.factoryMachineCrafterTable:TryGetValue(buildingId)
    if not success then
        return ""
    end

    local groupId = ""
    for index = 0, groupData.modeMap.Count - 1 do
        local modeData = groupData.modeMap[index]
        if modeData.modeName == mode then
            groupId = modeData.groupName
            break
        end
    end
    if string.isEmpty(groupId) then
        return ""
    end

    local craftGroupData = Tables.factoryMachineCraftGroupTable:GetValue(groupId)
    for _, formulaId in pairs(craftGroupData.craftList) do
        if GameInstance.player.remoteFactory.core:IsFormulaVisible(formulaId) then
            local formulaData = Tables.factoryMachineCraftTable:GetValue(formulaId)
            local searchMap = {}
            local totalSearchCount = 0
            for _, itemId in ipairs(itemList) do
                if searchMap[itemId] == nil then
                    searchMap[itemId] = 0
                end
                searchMap[itemId] = searchMap[itemId] + 1
                totalSearchCount = totalSearchCount + 1
            end

            local totalDataCount = 0
            for bundleGroupIndex = 0, formulaData.ingredients.Count - 1 do
                local bundleGroup = formulaData.ingredients[bundleGroupIndex].group
                for itemIndex = 0, bundleGroup.Count - 1 do
                    local itemBundle = bundleGroup[itemIndex]
                    local itemId = itemBundle.id
                    if searchMap[itemId] then
                        searchMap[itemId] = searchMap[itemId] - 1
                        if searchMap[itemId] == 0 then
                            searchMap[itemId] = nil
                        end
                    end
                    totalDataCount = totalDataCount + 1
                end
            end

            local envMatched = true
            if env ~= nil then
                local envEnum = Utils.intToEnum(typeof(GEnums.FacEnvGenEnvType), env)
                envMatched = envEnum == formulaData.gasEnv
            end

            if totalDataCount == totalSearchCount and not next(searchMap) and envMatched then
                return formulaId
            end
        end
    end

    return ""
end

function FactoryUtils.getActiveChapterIdList()
    local csList = GameInstance.player.remoteFactory.curActiveChapterIds
    local idList = {}
    for chapterId, _ in cs_pairs(csList) do
        table.insert(idList, chapterId)
    end
    return idList
end




function FactoryUtils.getPlayerAllMarkerBuildingNodeInfo()
    local csList = GameInstance.player.remoteFactory.curSignBuildingMsgList
    local infoList = {}
    local count = 0
    for index, data in cs_pairs(csList) do
        count = count + 1
        local chapterId = ScopeUtil.ChapterIdStr2Int(data.ChapterId)
        local slotId = CSFactoryUtil.GetBlueprintSlotId(chapterId, data.NodeId)
        if slotId <= 0 then
            local info = {
                nodeId = data.NodeId,
                chapter = data.ChapterId,
                timestamp = index,
                iconKey = {}
            }
            for i = 0, data.SignId.Count - 1 do
                table.insert(info.iconKey, data.SignId[i])
            end
            table.insert(infoList, info)
        end
    end
    return count, infoList
end




function FactoryUtils.getLiquidCanBeDischarge(itemId)
    local _, itemData = Tables.factoryItemTable:TryGetValue(itemId)
    if itemData == nil or itemData.dischargeType == nil then
        return false
    end
    return itemData.dischargeType
end

function FactoryUtils.getBlueprintTagGroupInfos()
    local tagGroupDic = {}
    for k, v in pairs(Tables.factoryBlueprintTagTable) do
        local tagGroup = tagGroupDic[v.type]
        if not tagGroup then
            local typeData = Tables.factoryBlueprintTagTypeTable[v.type]
            tagGroup = {
                title = typeData.name,
                sortId = typeData.sortId,
                tags = {}
            }
            tagGroupDic[v.type] = tagGroup
        end
        if string.isEmpty(v.formulaId) or GameInstance.player.remoteFactory.core:IsFormulaVisible(v.formulaId) then
            table.insert(tagGroup.tags, {
                id = k,
                type = v.type,
                name = v.name,
                sortId = v.sortId,
            })
        end
    end
    local tagGroupList = {}
    for _, v in pairs(tagGroupDic) do
        table.insert(tagGroupList, v)
        table.sort(v.tags, Utils.genSortFunction({ "sortId", "id" }))
    end
    table.sort(tagGroupList, Utils.genSortFunction({ "sortId", "id" }))
    return tagGroupList
end



function FactoryUtils.createBPAbnormalIconHelper()
    local helper = {










        cachedResults = {},
    }

    helper.GetAbnormalType = function(machineId, itemId, needCraftId)
        if not GameInstance.player.inventory:IsItemFound(itemId) then
            return FacConst.FAC_BP_ABNORMAL_ICON_TYPE.Locked
        end
        if not Tables.factoryBuildingTable:ContainsKey(machineId) then

            return FacConst.FAC_BP_ABNORMAL_ICON_TYPE.Normal
        end
        local canProduceItems = helper.cachedResults[machineId]
        if not canProduceItems then
            local craftInfos = FactoryUtils.getBuildingCrafts(machineId)
            if not craftInfos or not next(craftInfos) then

                canProduceItems = true
            else
                canProduceItems = {}
                for _, cInfo in ipairs(craftInfos) do
                    if cInfo.outcomes then
                        local isTimeLimited = FactoryUtils.isTimeLimitedFormula(cInfo.craftId)
                        local t = isTimeLimited and FacConst.FAC_BP_ABNORMAL_ICON_TYPE.TimeLimitedActive or FacConst.FAC_BP_ABNORMAL_ICON_TYPE.Normal
                        for _, v in ipairs(cInfo.outcomes) do
                            canProduceItems[v.id] = { t, cInfo.craftId }
                        end
                    end
                end
            end
            helper.cachedResults[machineId] = canProduceItems
        end
        if canProduceItems == true then
            if FactoryUtils.isTimeLimitedItem(itemId) then

                local hasCraft, craftIds = Tables.factoryItemAsMachineCrafterOutcomeTable:TryGetValue(itemId)
                if hasCraft then
                    for _, craftId in pairs(craftIds.list) do
                        if FactoryUtils.isExpiredTimeLimitedFormula(craftId) then
                            return FacConst.FAC_BP_ABNORMAL_ICON_TYPE.TimeLimitedExpired
                        end
                    end
                end
                return FacConst.FAC_BP_ABNORMAL_ICON_TYPE.TimeLimitedActive
            else
                return FacConst.FAC_BP_ABNORMAL_ICON_TYPE.Normal
            end
        else
            local abnormalInfo = canProduceItems[itemId]
            if not abnormalInfo then


                if FactoryUtils.isTimeLimitedItem(itemId) then

                    local hasCraft, craftIds = Tables.factoryItemAsMachineCrafterOutcomeTable:TryGetValue(itemId)
                    if hasCraft then
                        for _, craftId in pairs(craftIds.list) do
                            if FactoryUtils.isExpiredTimeLimitedFormula(craftId) then
                                abnormalInfo = { FacConst.FAC_BP_ABNORMAL_ICON_TYPE.TimeLimitedExpired, craftId }
                            end
                        end
                    end
                end

                if not abnormalInfo then
                    abnormalInfo = { FacConst.FAC_BP_ABNORMAL_ICON_TYPE.Locked }
                end
                canProduceItems[itemId] = abnormalInfo
            end
            if needCraftId then
                return abnormalInfo[1], abnormalInfo[2]
            else
                return abnormalInfo[1]
            end
        end
    end
    return helper
end

function FactoryUtils.clearQuickBarSlot(csIndex)
    local remoteFactoryCore = GameInstance.player.remoteFactory.core
    if remoteFactoryCore.isTempQuickBarActive then
        remoteFactoryCore:MoveItemToTempQuickBar("", csIndex)
    else
        GameInstance.player.remoteFactory:SendSetQuickBar(GEnums.FCQuickBarType.Inner, 0, csIndex, "")
    end
end


function FactoryUtils.getFreeBusLimitsInfo(regionId, index)
    local bus, source = GameInstance.remoteFactoryManager:GetFreeBusLimitsInfo(regionId, index)
    return {
        ["log_hongs_bus"] = bus,
        ["log_hongs_bus_source"] = source,
    }
end

local domainAllowModes = {}

function FactoryUtils.addBuildingDomainSortFilterInfo(info, data, domainId)
    info.recommendDomains = {}
    info.domainSortGroup = FacConst.DOMAIN_SORT_GROUP.Normal


    local allowModes = domainAllowModes[domainId]
    if not allowModes then
        allowModes = {}
        local _, domainData = Tables.domainDataTable:TryGetValue(domainId)
        if domainData then
            for _, v in pairs(domainData.machineModeTypeGroup) do
                allowModes[v] = true
            end
        end
        domainAllowModes[domainId] = allowModes
    end

    for _, filterDomainId in pairs(data.recommendDomains) do
        table.insert(info.recommendDomains, filterDomainId)
    end
    if #data.placeDomains > 0 and lume.find(data.placeDomains, domainId) == nil then
        info.domainSortGroup = FacConst.DOMAIN_SORT_GROUP.Unsupported
    end
    if info.domainSortGroup > FacConst.DOMAIN_SORT_GROUP.ModeUnsupported and next(allowModes) then
        local succ, crafterData = Tables.factoryMachineCrafterTable:TryGetValue(data.id)
        if succ then
            local allow = false
            for index = 0, crafterData.modeMap.Count - 1 do
                local mapData = crafterData.modeMap[index]
                if mapData ~= nil and allowModes[mapData.modeName]then
                    allow = true
                    break
                end
            end
            if not allow then
                info.domainSortGroup = FacConst.DOMAIN_SORT_GROUP.ModeUnsupported
            end
        end
    end
    if info.domainSortGroup > FacConst.DOMAIN_SORT_GROUP.Unsuitable then
        if #data.recommendDomains > 0 and lume.find(data.recommendDomains, domainId) == nil then
            info.domainSortGroup = FacConst.DOMAIN_SORT_GROUP.Unsuitable
        end
    end
    info.domainReverseSort = -info.domainSortGroup
end

function FactoryUtils.checkBuildingDomainSupportByItemId(itemId)
    local data = FactoryUtils.getItemBuildingData(itemId)
    if data == nil then
        return false
    end

    local domainId = Utils.getCurDomainId()

    local allowModes = domainAllowModes[domainId]
    if not allowModes then
        allowModes = {}
        local _, domainData = Tables.domainDataTable:TryGetValue(domainId)
        if domainData then
            for _, v in pairs(domainData.machineModeTypeGroup) do
                allowModes[v] = true
            end
        end
        domainAllowModes[domainId] = allowModes
    end

    if #data.placeDomains > 0 and lume.find(data.placeDomains, domainId) == nil then
        return true
    end
    if next(allowModes) then
        local succ, crafterData = Tables.factoryMachineCrafterTable:TryGetValue(data.id)
        if succ then
            local allow = false
            for index = 0, crafterData.modeMap.Count - 1 do
                local mapData = crafterData.modeMap[index]
                if mapData ~= nil and allowModes[mapData.modeName]then
                    allow = true
                    break
                end
            end
            if not allow then
                return true
            end
        end
    end
    return false
end

local allowPipeDoamins
function FactoryUtils.GetAllowPipeDoaminList()
    if allowPipeDoamins == nil then
        allowPipeDoamins = {}
        for _, cfg in pairs(Tables.domainDataTable) do
            for _, v in pairs(cfg.machineModeTypeGroup) do
                if v == FacConst.FAC_FORMULA_MODE_MAP.LIQUID then
                    table.insert(allowPipeDoamins, cfg.domainId)
                    break
                end
            end
        end
    end
    return allowPipeDoamins
end

function FactoryUtils.SetCreatorName(blueprintContent, isCreating, bpInst)
    local ShowPSCreator = UNITY_PS5
    local creatorName
    local roleId

    if isCreating then
        roleId = GameInstance.player.playerInfoSystem.roleId
        creatorName = string.format(Language.LUA_FAC_BLUEPRINT_CREATOR_FORMAT_USER, roleId)
    elseif FactoryUtils.isPlayerBP(bpInst) then
        roleId = bpInst.creatorUserId
        creatorName = string.format(Language.LUA_FAC_BLUEPRINT_CREATOR_FORMAT_USER, roleId)
    else
        creatorName = string.format(Language.LUA_FAC_BLUEPRINT_CREATOR_FORMAT_SYS, bpInst.info.creatorName)
        ShowPSCreator = false
    end

    if not ShowPSCreator then
        blueprintContent.view.creatorNameTxt.text = creatorName
        blueprintContent.view.creatorNode.gameObject:SetActive(creatorName ~= nil)
        blueprintContent.view.psNameIcon.gameObject:SetActive(false)
        return
    end


    if roleId == GameInstance.player.playerInfoSystem.roleId then
        creatorName = string.format(Language.LUA_FAC_BLUEPRINT_CREATOR_FORMAT_USER, GameInstance.player.friendSystem.SelfInfo.psName)
        blueprintContent.view.creatorNameTxt.text = creatorName
        blueprintContent.view.psNameIcon.gameObject:SetActive(true)
        blueprintContent.view.creatorNode.gameObject:SetActive(true)
        return
    end


    blueprintContent.view.creatorNode.gameObject:SetActive(false)
    GameInstance.player.friendSystem:SyncFriendInfoById(bpInst.creatorRoleId, function()
        local success, friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(bpInst.creatorRoleId)
        if success and friendInfo.psnData then

            creatorName = string.format(Language.LUA_FAC_BLUEPRINT_CREATOR_FORMAT_USER, friendInfo.psName)
            blueprintContent.view.psNameIcon.gameObject:SetActive(true)
        else

            creatorName = string.format(Language.LUA_FAC_BLUEPRINT_CREATOR_FORMAT_USER, roleId)
            blueprintContent.view.psNameIcon.gameObject:SetActive(false)
        end
        blueprintContent.view.creatorNameTxt.text = creatorName
        blueprintContent.view.creatorNode.gameObject:SetActive(true)
    end)
end

function FactoryUtils.isPlayerBP(bpInst)
    if bpInst == nil then
        return false
    end
    local isMine = bpInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Mine
    local isOther = (bpInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift and bpInst.param.shareIdx ~= 0)
    return isMine or isOther
end

function FactoryUtils.isOtherPeopleGiftBlueprint(bpInst)
    if bpInst == nil then
        return false
    end
    local isGift = bpInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift
    return isGift and bpInst.param.shareIdx ~= 0 and bpInst.creatorUserId ~= GameInstance.player.playerInfoSystem.roleId
end

function FactoryUtils.getItemCraft(itemId)
    local craftInfos, hasCraft = FactoryUtils.getItemCrafts(itemId, false)
    local defaultCraftId = WikiUtils.getItemDefaultCraftId(itemId)
    if craftInfos ~= nil and #craftInfos > 0 then
        local firstFactoryCraftInfo
        for i, craftInfo in ipairs(craftInfos) do
            if craftInfo.craftId == defaultCraftId then
                return craftInfo
            end
            if not firstFactoryCraftInfo and craftInfo.buildingId ~= nil then
                firstFactoryCraftInfo = craftInfo
            end
        end

        if firstFactoryCraftInfo then
            return firstFactoryCraftInfo
        end
        return craftInfos[1]
    end
    return nil
end

function FactoryUtils.isSystemBlueprintUnlocked(id)
    if Utils.isInBlackbox() or not Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacBlueprint) then
        return false
    end
    return GameInstance.player.remoteFactory.blueprint.builtinBlueprints:TryGetValue(id)
end

function FactoryUtils.getMatchingBlueprintShareCode(text)

    local patterns = Tables.facBlueprintConst.BlueprintShareCodePrefix
    local start_pos = 10000
    local end_pos = -1
    for index = 1, #patterns do
        local pattern = patterns[CSIndex(index)]
        local i, j = string.find(text, pattern, 1, true)
        if i and i <= start_pos and j >= end_pos then

            start_pos = math.min(start_pos, i)
            end_pos = math.max(end_pos, j)
        end
    end


    if end_pos <= 0 then
        return text
    end


    local charset = Tables.facBlueprintConst.BlueprintCharSet
    while end_pos < #text and charset:find(text:sub(end_pos+1, end_pos+1), 1, true) do
        end_pos = end_pos + 1
    end
    return text:sub(start_pos, end_pos)
end


function FactoryUtils.exitFactoryRelatedMode()
    Notify(MessageConst.FAC_BUILD_EXIT_CUR_MODE, true)
    Notify(MessageConst.FAC_EXIT_DESTROY_MODE, true)
    LuaSystemManager.factory:ToggleTopView(false, true)
end


function FactoryUtils.getCurAndAutoTransferBlackBoxToDomainId()
    local domainId
    if Utils.isInBlackbox() then
        local succ, blackboxCfg = Tables.dungeonTable:TryGetValue(GameInstance.dungeonManager.curDungeonId)
        if succ then
            domainId = blackboxCfg.domainId
        else
            domainId = Utils.getCurDomainId()
        end
    else
        domainId = Utils.getCurDomainId()
    end
    return domainId
end


function FactoryUtils.isTimeLimitedFormula(formulaId)
    return Tables.limitedFormulaCraftIdReverseTable:ContainsKey(formulaId)
end


function FactoryUtils.isExpiredTimeLimitedFormula(formulaId)
    return GameInstance.player.remoteFactory.core:IsLimitedFormulaUnlockedBefore(formulaId)
end


function FactoryUtils.isTimeLimitedItem(itemId)
    return Tables.limitedFormulaItemIdReverseTable:ContainsKey(itemId)
end


function FactoryUtils.setTimeLimitedFormulaTagColor(image, formulaId)
    if image ~= nil and Tables.limitedFormulaCraftIdReverseTable:ContainsKey(formulaId) then
        local activityId = Tables.limitedFormulaCraftIdReverseTable:GetValue(formulaId)
        local activityCfg = Tables.activityTable:GetValue(activityId)
        if not string.isEmpty(activityCfg.themeColor) then
            image.color = UIUtils.getColorByString(activityCfg.themeColor)
        end
    end
end


function FactoryUtils.setTimeLimitedItemTagColor(image, itemId)
    if image ~= nil and Tables.limitedFormulaItemIdReverseTable:ContainsKey(itemId) then
        local activityId = Tables.limitedFormulaItemIdReverseTable:GetValue(itemId)
        local activityCfg = Tables.activityTable:GetValue(activityId)
        if not string.isEmpty(activityCfg.themeColor) then
            image.color = UIUtils.getColorByString(activityCfg.themeColor)
        end
    end
end


function FactoryUtils.getActivityIdByBluePrintId(blueprintId)
    local blueprintSystem = GameInstance.player.remoteFactory.blueprint
    local blueprintInst = blueprintSystem:GetMyBlueprint(blueprintId);
    if blueprintInst == nil then
        return ""
    end

    local blueprint = blueprintInst.info.bp;
    if blueprint == nil or blueprint.timeLimitedFormulas == nil or blueprint.timeLimitedFormulas.Count == 0 then
        return ""
    end


    local formulaId = blueprint.timeLimitedFormulas[0]
    local formulaIdStr = CS.Beyond.Cfg.Tables.formulaIdToStr:GetValue(formulaId)
    if Tables.limitedFormulaCraftIdReverseTable:ContainsKey(formulaIdStr) then
        local activityId = Tables.limitedFormulaCraftIdReverseTable:GetValue(formulaIdStr)
        return activityId
    end

    return ""
end


function FactoryUtils.refreshStateNodeByState(stateNode, progressNode, state, postProcessText)
    local stateText
    if state == GEnums.FacBuildingState.NoPower then
        stateText = Language.LUA_FAC_CRAFTER_STATE_NOPOWER_TIPS
    elseif state == GEnums.FacBuildingState.NotInPowerNet then
        stateText = Language.LUA_FAC_CRAFTER_STATE_NOTINPOWERNET_TIPS
    elseif state == GEnums.FacBuildingState.Closed then
        stateText = Language.LUA_FAC_CRAFTER_STATE_CLOSE_TIPS
    end

    if stateText == nil and postProcessText ~= nil then
        stateText = postProcessText
    end

    progressNode.gameObject:SetActiveIfNecessary(stateText == nil)

    if stateText == nil then
        stateNode.animationWrapper:PlayOutAnimation(function()
            stateNode.gameObject:SetActiveIfNecessary(false)
        end)
    else
        stateNode.gameObject:SetActiveIfNecessary(true)
        stateNode.stateTxt.text = stateText
    end

    return stateText ~= nil
end

function FactoryUtils.refreshEnvIcon(env, envIconController)
    if env and env ~= GEnums.FacEnvGenEnvType.None then
        envIconController.gameObject:SetActive(true)
        envIconController:SetState(env:ToString())
    else
        envIconController.gameObject:SetActive(false)
    end
end




local s_envReceiverBuildingSet = nil




function FactoryUtils.isEnvReceiverBuilding(buildingId)
    if string.isEmpty(buildingId) then
        return false
    end
    if s_envReceiverBuildingSet == nil then
        s_envReceiverBuildingSet = {}
        for _, craftData in pairs(Tables.factoryMachineCraftTable) do
            if craftData.gasEnv ~= GEnums.FacEnvGenEnvType.None then
                s_envReceiverBuildingSet[craftData.machineId] = true
            end
        end
    end
    return s_envReceiverBuildingSet[buildingId] == true
end




function FactoryUtils.isBlueprintProductIconGasEnv(productIcon)
    if string.isEmpty(productIcon) then
        return false
    end
    local prefix = FacConst.FAC_BLUEPRINT_PRODUCT_ICON_GAS_PREFIX
    return string.sub(productIcon, 1, #prefix) == prefix
end



function FactoryUtils.blueprintProductIconGasEnvToSpriteName(productIcon)
    if not FactoryUtils.isBlueprintProductIconGasEnv(productIcon) then
        return nil
    end
    local prefix = FacConst.FAC_BLUEPRINT_PRODUCT_ICON_GAS_PREFIX
    local envName = string.sub(productIcon, #prefix + 1)
    if string.isEmpty(envName) then
        return nil
    end
    return string.format("icon_gas_env_%s", string.lower(envName))
end





function FactoryUtils.blueprintProductIconGasEnvToEffectedSpriteName(productIcon)
    if not FactoryUtils.isBlueprintProductIconGasEnv(productIcon) then
        return nil
    end
    local prefix = FacConst.FAC_BLUEPRINT_PRODUCT_ICON_GAS_PREFIX
    local envName = string.sub(productIcon, #prefix + 1)
    if string.isEmpty(envName) then
        return nil
    end
    return string.format("icon_gas_env_effected_%s", string.lower(envName))
end




function FactoryUtils.getEnvGenBlueprintGasProductIconEntries(buildingId)
    local list = {}
    local ok, data = Tables.factoryVaporizerTable:TryGetValue(buildingId)
    if not ok then
        return list
    end
    local seen = {}
    for _, groupData in pairs(data.groups) do
        local genEnv = groupData.genEnv
        if genEnv and genEnv ~= GEnums.FacEnvGenEnvType.None then
            local itemId = FacConst.FAC_BLUEPRINT_PRODUCT_ICON_GAS_PREFIX .. genEnv:ToString()
            if not seen[itemId] then
                seen[itemId] = true
                local sortId1 = enum_to_int(genEnv) or 0
                table.insert(list, {
                    itemId = itemId,
                    sortId1 = sortId1,
                    sortId2 = 0,
                })
            end
        end
    end
    table.sort(list, Utils.genSortFunction({ "sortId1", "sortId2", "itemId" }))
    return list
end

function FactoryUtils.isEmptyBottleOrJarItem(itemId, targetCacheType)
    local isEmpty = false
    if targetCacheType == nil or targetCacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid then
        isEmpty = Tables.emptyBottleTable:ContainsKey(itemId) or Tables.emptyGasJarTable:ContainsKey(itemId)
    elseif targetCacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid then
        isEmpty = Tables.emptyBottleTable:ContainsKey(itemId)
    elseif targetCacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas then
        isEmpty = Tables.emptyGasJarTable:ContainsKey(itemId)
    end
    return isEmpty
end

function FactoryUtils.isFullBottleOrJarItem(itemId, targetCacheType)
    local isFull = false
    if targetCacheType == nil or targetCacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid then
        isFull = Tables.fullBottleTable:ContainsKey(itemId) or Tables.fullGasJarTable:ContainsKey(itemId)
    elseif targetCacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid then
        isFull = Tables.fullBottleTable:ContainsKey(itemId)
    elseif targetCacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas then
        isFull = Tables.fullGasJarTable:ContainsKey(itemId)
    end
    return isFull
end

function FactoryUtils.isCacheTypeAcceptItemType(cacheType, itemType)
    if type(itemType) == "number" then
        return (cacheType & itemType) > 0
    end
    return (cacheType & itemType:GetHashCode()) > 0
end



function FactoryUtils.getSewageTreatPlantLevel(plantId)
    local success, plantCfg = Tables.factorySewageTreatPlantStoreTable:TryGetValue(plantId)
    if not success then
        return 0
    end
    local chapterId = ScopeUtil.GetChapterId(plantCfg.levelId)
    return GameInstance.player.remoteFactory.core.progressStatus:QuerySewageTreatPlantLevel(chapterId)
end

function FactoryUtils.getSewageTreatPlantIdByLevelId(levelId)
    for plantId, plantCfg in pairs(Tables.factorySewageTreatPlantStoreTable) do
        if plantCfg.levelId == levelId then
            return plantId
        end
    end
    return nil
end

function FactoryUtils.getSewageTreatPlantCanLevelUpIdListInTargetLevel(levelId)
    local targetPlantId = FactoryUtils.getSewageTreatPlantIdByLevelId(levelId)
    if targetPlantId == nil then
        return {}
    end

    local plantData = FactoryUtils.getSewageTreatPlantData(targetPlantId)
    if plantData.currLevel == 0 or plantData.isMaxLevel then
        return {}
    end

    local cost = plantData.levelCost
    local _, domainCfg = Tables.domainDataTable:TryGetValue(plantData.domainId)
    local moneyId = domainCfg.domainGoldItemId
    local money = Utils.getItemCount(moneyId)
    if money < cost then
        return {}
    end

    return { targetPlantId }
end

function FactoryUtils.getSewageTreatPlantData(plantId)
    local plantCfgSuccess, plantCfg = Tables.factorySewageTreatPlantStoreTable:TryGetValue(plantId)
    if not plantCfgSuccess then
        return
    end

    local domainId = plantCfg.domainId
    local chapterId = ScopeUtil.GetChapterId(plantCfg.levelId)
    local currLevel = GameInstance.player.remoteFactory.core.progressStatus:QuerySewageTreatPlantLevel(chapterId)
    local nextLevel = currLevel + 1

    local chapterInfo = GameInstance.remoteFactoryManager.system.core:GetChapterInfoById(chapterId)
    if chapterInfo == nil then
        return
    end

    local plantData = {}
    plantData.domainId = domainId
    plantData.levelId = plantCfg.levelId
    plantData.currLevel = currLevel
    plantData.nextLevel = nextLevel
    local currImportCount, currExportCount = 0, 0
    local nextImportCount, nextExportCount = 0, 0
    local maxImportCount, maxExportCount = 0, 0

    local maxLevel = plantCfg.levelList.Count
    local isMaxLevel = maxLevel == currLevel
    plantData.maxLevel = maxLevel
    plantData.isMaxLevel = isMaxLevel
    plantData.isFinalMaxLevel = isMaxLevel

    for levelIndex = 0, plantCfg.levelList.Count - 1 do
        local levelData = plantCfg.levelList[levelIndex]
        local cfgLevel = levelData.level

        if currLevel == cfgLevel then
            plantData.currLevelDesc = levelData.levelDesc
        end

        if nextLevel == cfgLevel or (nextLevel > maxLevel and maxLevel == cfgLevel) then
            plantData.nextLevelTitle = levelData.levelTitle
            plantData.levelCost = levelData.cost
            plantData.nextLevelBuildingInstKey = levelData.actionParams[1]
        end

        local isLevelImport = levelData.isImportLevel
        if cfgLevel <= nextLevel then
            if cfgLevel <= currLevel then
                if isLevelImport then
                    currImportCount = currImportCount + 1
                else
                    currExportCount = currExportCount + 1
                end
            end

            if isLevelImport then
                nextImportCount = nextImportCount + 1
            else
                nextExportCount = nextExportCount + 1
            end
        end

        if isLevelImport then
            maxImportCount = maxImportCount + 1
        else
            maxExportCount = maxExportCount + 1
        end
    end

    plantData.currImportCount = currImportCount
    plantData.nextImportCount = nextImportCount
    plantData.currExportCount = currExportCount
    plantData.nextExportCount = nextExportCount
    plantData.maxImportCount = maxImportCount
    plantData.maxExportCount = maxExportCount

    local importerCfgData = Tables.factoryBuildingTable:GetValue(FacConst.FAC_SEWAGE_TREAT_IMPORTER_BUILDING_ID)
    local exporterCfgData = Tables.factoryBuildingTable:GetValue(FacConst.FAC_SEWAGE_TREAT_EXPORTER_BUILDING_ID)
    plantData.importerDesc = importerCfgData.desc
    plantData.exporterDesc = exporterCfgData.desc

    return plantData
end

function FactoryUtils.evaluateMultiBuildingLimitedPriority(templateId)
    for i, v in ipairs(FacConst.FAC_MULTI_LIMITED_BUILDING_ORDER) do
        if v == templateId then
            return #FacConst.FAC_MULTI_LIMITED_BUILDING_ORDER - i + 1
        end
    end
    return 0
end



function FactoryUtils.isDecoBuilding(templateId)
    if not templateId then
        return false
    end
    local bData = Tables.factoryBuildingTable[templateId]
    return bData.type == GEnums.FacBuildingType.Decorate
end

function FactoryUtils.isDecoBuildingItem(itemId)
    local bId = FactoryUtils.getItemBuildingId(itemId)
    if bId then
        return FactoryUtils.isDecoBuilding(bId)
    end
    return false
end

function FactoryUtils.isDecoBuildingVisible(itemId)
    local decoBuildingInfo = Tables.FactoryDecoBuildingTable[itemId]
    local _, gotCnt, placedCnt = GameInstance.player.remoteFactory.core:GetDecoBuildingCount(decoBuildingInfo.buildingId)
    if gotCnt > 0 or placedCnt > 0 then
        return true
    end

    if decoBuildingInfo.hideOnNotGet then
        if gotCnt + placedCnt == 0 then
            return false
        end
    end
    if #decoBuildingInfo.visibleConditions == 0 then
        return true
    end

    for _, conditionId in pairs(decoBuildingInfo.visibleConditions) do
        local condition = Tables.DecoBuildingObtainConditionTable[conditionId]
        local success, value = LuaGameConditionUtils.getConditionValueByParameters(
            condition.conditionType,
            condition.parameters)
        if not success or value < condition.progressToCompare then
            return false
        end
    end
    return true
end

function FactoryUtils.playAudioWhenFillingItem(fillingItemId, targetItemId, targetItemCount)
    if Tables.emptyBottleTable:ContainsKey(fillingItemId) then
        if targetItemCount > 0 then
            AudioAdapter.PostEvent("Au_UI_Event_WaterDown_Small")
        end
        return
    end

    if Tables.fullBottleTable:ContainsKey(fillingItemId) then
        if string.isEmpty(targetItemId) then
            AudioAdapter.PostEvent("Au_UI_Event_WaterUp_Small")
        else
            local success, data = Tables.factoryItemTable:TryGetValue(targetItemId)
            if success and targetItemCount < data.buildingBufferStackLimit then
                AudioAdapter.PostEvent("Au_UI_Event_WaterUp_Small")
            end
        end
        return
    end

    if Tables.emptyGasJarTable:ContainsKey(fillingItemId) then
        if targetItemCount > 0 then
            AudioAdapter.PostEvent("Au_UI_Event_GasDown_Small")
        end
        return
    end
    if Tables.fullGasJarTable:ContainsKey(fillingItemId) then
        if string.isEmpty(targetItemId) then
            AudioAdapter.PostEvent("Au_UI_Event_GasUp_Small")
        else
            local success, data = Tables.factoryItemTable:TryGetValue(targetItemId)
            if success and targetItemCount < data.buildingBufferStackLimit then
                AudioAdapter.PostEvent("Au_UI_Event_GasUp_Small")
            end
        end
        return
    end
end

function FactoryUtils.isCreatingBlueprint()
    local isOpen, ctrl = UIManager:IsOpen(PanelId.FacSaveBlueprint)
    if not isOpen then
        return false
    end
    return ctrl:GetIsCreating()
end



local SkipChapterBuildingStatus = CS.Beyond.Gameplay.Factory.SkipChapterBuildingStatus




function FactoryUtils.isSkipUnlockedBuilding(buildingId)
    return GameInstance.player.remoteFactory.core.progressStatus:IsSkipUnlockedBuilding(buildingId)
end





function FactoryUtils.getSkipChapterBuildingStatus(buildingId, domainId)
    domainId = domainId or ScopeUtil.GetCurrentChapterIdAsStr()
    return CSFactoryUtil.GetSkipChapterBuildingStatus(buildingId, domainId)
end





function FactoryUtils.isSkipBuildingInvalidInDomain(buildingId, domainId)
    local status = FactoryUtils.getSkipChapterBuildingStatus(buildingId, domainId)
    return status == SkipChapterBuildingStatus.SkipUnlockedButInvalid
end





function FactoryUtils.isSkipBuildingValidInDomain(buildingId, domainId)
    local status = FactoryUtils.getSkipChapterBuildingStatus(buildingId, domainId)
    return status == SkipChapterBuildingStatus.SkipUnlocked
end






function FactoryUtils.isLogisticUnlocked(id, domainId)
    if FactoryUtils.isSkipBuildingValidInDomain(id, domainId) then
        return true
    end
    local us = GameInstance.remoteFactoryManager.unlockSystem
    if Tables.factoryGridBeltTable:TryGetValue(id) then
        return us.systemUnlockedBelt
    end
    if Tables.FactoryBoxValveTable:TryGetValue(id) then
        return us.systemUnlockedBelt and us.systemUnlockedValve
    end
    if Tables.factoryGridConnecterTable:TryGetValue(id) then
        return us.systemUnlockedBridge
    end
    if Tables.factoryGridRouterTable:TryGetValue(id) then
        local unlockType = FacConst.LOGISTIC_UNLOCK_SYSTEM_MAP[id]
        if unlockType == GEnums.UnlockSystemType.FacMerger then return us.systemUnlockedConverger
        elseif unlockType == GEnums.UnlockSystemType.FacSplitter then return us.systemUnlockedSplitter
        elseif unlockType == GEnums.UnlockSystemType.FacValve then return us.systemUnlockedValve
        end
    end
    if Tables.factoryLiquidPipeTable:TryGetValue(id) then
        return us.systemUnlockedPipe
    end
    if Tables.factoryFluidValveTable:TryGetValue(id) then
        return us.systemUnlockedPipe and us.systemUnlockedPipeValve
    end
    if Tables.factoryLiquidConnectorTable:TryGetValue(id) then
        return us.systemUnlockedPipeConnector
    end
    if Tables.factoryLiquidRouterTable:TryGetValue(id) then
        local unlockType = FacConst.LOGISTIC_UNLOCK_SYSTEM_MAP[id]
        if unlockType == GEnums.UnlockSystemType.FacPipeConverger then return us.systemUnlockedPipeConverger
        elseif unlockType == GEnums.UnlockSystemType.FacPipeSplitter then return us.systemUnlockedPipeSplitter
        elseif unlockType == GEnums.UnlockSystemType.FacPipeValve then return us.systemUnlockedPipeValve
        end
    end
    return false
end




function FactoryUtils.isSkipUnlockedBuildingByItemId(itemId)
    local buildingId = FactoryUtils.getItemBuildingId(itemId)
    if buildingId then
        return FactoryUtils.isSkipUnlockedBuilding(buildingId)
    end
    local _, logisticId = FactoryUtils.isLogistic(itemId)
    if logisticId then
        return FactoryUtils.isSkipUnlockedBuilding(logisticId)
    end
    return false
end





function FactoryUtils.getSkipChapterBuildingStatusByItemId(itemId, domainId)
    local buildingId = FactoryUtils.getItemBuildingId(itemId)
    if buildingId then
        return FactoryUtils.getSkipChapterBuildingStatus(buildingId, domainId)
    end
    local _, logisticId = FactoryUtils.isLogistic(itemId)
    if logisticId then
        return FactoryUtils.getSkipChapterBuildingStatus(logisticId, domainId)
    end
    return SkipChapterBuildingStatus.NotUnlocked
end





function FactoryUtils.isSkipBuildingInvalidInDomainByItemId(itemId, domainId)
    local status = FactoryUtils.getSkipChapterBuildingStatusByItemId(itemId, domainId)
    return status == SkipChapterBuildingStatus.SkipUnlockedButInvalid
end





function FactoryUtils.isSkipBuildingValidInDomainByItemId(itemId, domainId)
    local status = FactoryUtils.getSkipChapterBuildingStatusByItemId(itemId, domainId)
    return status == SkipChapterBuildingStatus.SkipUnlocked
end



function FactoryUtils.getValveSpeedLimitedCount()
    local domainId = FactoryUtils.getCurAndAutoTransferBlackBoxToDomainId()
    local domainCfg = Tables.domainDataTable:GetValue(domainId)
    local allCount = GameInstance.remoteFactoryManager:GetValveSpeedLimitedCountByScene()
    return allCount, domainCfg.domainSpeedLimitCount
end

function FactoryUtils.getValveSpeedLimitedInfo()
    local allCount, valveInfo = GameInstance.remoteFactoryManager:GetValveSpeedLimitedCountByScene()
    local result = {}

    if allCount > 0 then
        local mapId = GameWorld.worldInfo.curMapIdStr
        local mapSucc, mapConfig = DataManager.mapConfigTable:TryGetValue(mapId)
        if mapSucc then
            for i = 0, mapConfig.levelStrIds.Length - 1 do
                local levelId = mapConfig.levelStrIds[i]
                local levelSucc, levelData = valveInfo:TryGetValue(levelId)
                if levelSucc then
                    if levelData[GEnums.FCNodeType.BoxValve:GetHashCode()] > 0 then
                        table.insert(result, {
                            levelId = mapConfig.levelStrIds[i],
                            nodeType = GEnums.FCNodeType.BoxValve:GetHashCode(),
                            count = levelData[GEnums.FCNodeType.BoxValve:GetHashCode()],
                        })
                    end
                    if levelData[GEnums.FCNodeType.FluidValve:GetHashCode()] > 0 then
                        table.insert(result, {
                            levelId = mapConfig.levelStrIds[i],
                            nodeType = GEnums.FCNodeType.FluidValve:GetHashCode(),
                            count = levelData[GEnums.FCNodeType.FluidValve:GetHashCode()],
                        })
                    end
                end
            end
        end
    end

    return result
end


function FactoryUtils.isInvalidBuilding(nodeId, chapterId)
    chapterId = chapterId or Utils.getCurrentChapterId()
    return CSFactoryUtil.IsInvalidBuilding(nodeId, chapterId)
end


function FactoryUtils.getBuildingModeListCovered(machineId)
    local modeList = {}
    local modeMap = {}
    local succ, machineCraftData = Tables.factoryMachineCrafterTable:TryGetValue(machineId)
    if not succ then
        return modeList
    end
    for index = 0, machineCraftData.modeMap.Count - 1 do
        local mapData = machineCraftData.modeMap[index]
        modeMap[mapData.modeName] = mapData
    end

    for modeName, modeData in pairs(modeMap) do
        if modeData ~= -1 then
            local success, coverModeData = Tables.factoryMachineCraftModeCoverTable:TryGetValue(modeName)
            if success then
                local unlockMode = GameInstance.player.remoteFactory.core:IsBuildingModeUnlocked(modeName, machineId)
                if unlockMode then
                    for i = 0, coverModeData.list.Count - 1 do
                        local coverMode = coverModeData.list[i]
                        if modeMap[coverMode.machineModeCoverType] then
                            modeMap[coverMode.machineModeCoverType] = -1
                        end
                    end
                else
                    modeMap[modeName] = -1
                end
            end
        end
    end
    for modeName, modeData in pairs(modeMap) do
        if modeData ~= -1 then
            table.insert(modeList, modeData)
        end
    end

    return modeList
end


_G.FactoryUtils = FactoryUtils
return FactoryUtils
