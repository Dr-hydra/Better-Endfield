local SpaceshipUtils = {}

SpaceshipUtils.RoomStateEnum = {
    Idle = 1,
    Producing = 2,
    ShutDown = 3,
}
function SpaceshipUtils.updateSSCharPreStamina(staminaNode, charId, preAddStamina)
    local spaceship = GameInstance.player.spaceship
    local staminaPercent = (spaceship:GetCharCurStamina(charId) + preAddStamina or 0) / Tables.spaceshipConst.maxPhysicalStrength
    if staminaNode.preFill then
        staminaNode.preFill.fillAmount = staminaPercent > 1 and 1 or staminaPercent
        staminaNode.preFill.gameObject:SetActive(preAddStamina ~= 0)
    end
end

function SpaceshipUtils.updateSSCharStamina(staminaNode, charId)
    local spaceship = GameInstance.player.spaceship
    if spaceship.isViewingFriend then
        staminaNode.gameObject:SetActive(false)
        return
    end
    staminaNode.gameObject:SetActive(true)
    local staminaPercent = spaceship:GetCharCurStamina(charId) / Tables.spaceshipConst.maxPhysicalStrength
    staminaNode.valueTxt.text = string.format(Language.LUA_SPACESHIP_CHAR_STAMINA_FORMAT, math.ceil(staminaPercent * 100))

    if staminaNode.fill then
        staminaNode.fill.fillAmount = staminaPercent
    end
    if staminaNode.preFill then
        staminaNode.preFill.gameObject:SetActive(false)
    end

    local sprite
    local lowFrameActive
    local fullFrameActive
    if staminaPercent >= 1 then
        sprite = "spaceship_stamina_full"
        lowFrameActive = false
        fullFrameActive = true
    elseif staminaPercent <= 0.2 then
        sprite = "spaceship_stamina_low"
        lowFrameActive = true
        fullFrameActive = false
    else
        sprite = "spaceship_stamina_normal"
        lowFrameActive = false
        fullFrameActive = false
    end

    if staminaNode.lowFrame then
        staminaNode.lowFrame.gameObject:SetActive(lowFrameActive)
    end

    if staminaNode.fullFrame then
        staminaNode.fullFrame.gameObject:SetActive(fullFrameActive)
    end

    if staminaNode.icon then
        staminaNode.icon:LoadSprite(UIConst.UI_SPRITE_SS_COMMON, sprite)
    end
end

function SpaceshipUtils.updateSSCharInfos(view, charId, targetRoomId, specialEmptyColor, disableFunc)
    local charData = Tables.characterTable[charId]
    if view.nameTxt then
        view.nameTxt.text = charData.name
    end

    if view.config and view.config:HasValue("USE_ROUND_ICON_PATH") and view.config.USE_ROUND_ICON_PATH then
        view.charIcon:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, UIConst.UI_CHAR_HEAD_PREFIX .. charId)
    else
        view.charIcon:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD_RECTANGLE, UIConst.UI_CHAR_HEAD_SQUARE_PREFIX .. charId)
    end

    if view.rarity then
        view.rarity.color = UIUtils.getCharRarityColor(charData.rarity)
    end

    local spaceship = GameInstance.player.spaceship


    local succ, char = spaceship.characters:TryGetValue(charId)
    if succ then
        view.friendshipTxt.text = string.format(Language.LUA_SPACESHIP_CHAR_FRIENDSHIP_FORMAT, math.floor(CSPlayerDataUtil.GetFriendshipPercent(char.friendship) * 100))
    end

    SpaceshipUtils.updateSSCharStamina(view.staminaNode, charId)

    local showRoom = true
    if view.roomNode then
        showRoom = view.config.USE_ROOM_NODE
        view.roomNode.gameObject:SetActive(showRoom)
    end
    if showRoom and view.roomBG and succ then
        local roomId = char.stationedRoomId
        if string.isEmpty(roomId) then
            if specialEmptyColor then
                view.roomBG.color = UIUtils.getColorByString(SpaceshipConst.NO_ROOM_SPECIAL_COLOR_STR[1])
                view.roomName.color = UIUtils.getColorByString(SpaceshipConst.NO_ROOM_SPECIAL_COLOR_STR[2])
            else
                view.roomBG.color = UIUtils.getColorByString(SpaceshipConst.NO_ROOM_COLOR_STR[1])
                view.roomName.color = UIUtils.getColorByString(SpaceshipConst.NO_ROOM_COLOR_STR[2])
            end

            view.roomName.text = Language.LUA_SPACESHIP_CHAR_NO_ROOM
        else
            local _, roomData = GameInstance.player.spaceship:TryGetRoom(roomId)
            view.roomBG.color = UIUtils.getColorByString(SpaceshipConst.ROOM_COLOR_STR[roomData.type][1])
            view.roomName.color = UIUtils.getColorByString(SpaceshipConst.ROOM_COLOR_STR[roomData.type][2])
            if roomId == targetRoomId then
                view.roomName.text = Language.LUA_SPACESHIP_IS_IN_TARGET_ROOM
            else
                view.roomName.text = SpaceshipUtils.getFormatCabinSerialNum(roomId, roomData.serialNum)
            end
        end

        if view.disableNode then
            view.disableNode.gameObject:SetActive(disableFunc and disableFunc())
        end
    end

    if view.stateNode then

        view.stateNode.gameObject:SetActive(view.config.USE_STATE_NODE and spaceship:IsCharResting(charId))
    end

    if view.friendshipNode then
        if view.config and view.config:HasValue("USE_FRIENDSHIP_NODE") then
            view.friendshipNode.gameObject:SetActive(view.config.USE_FRIENDSHIP_NODE)
        end
        if spaceship.isViewingFriend then
            view.friendshipNode.gameObject:SetActive(false)
        end
    end

    if view.workStateNode and succ then
        local isWorking = char.isWorking
        local isResting = char.isResting
        if spaceship.isViewingFriend then
            view.workStateNode.gameObject:SetActive(false)
        elseif view.config and view.config:HasValue("USE_WORK_STATE_NODE") and (isWorking or isResting) then
            view.workStateNode.gameObject:SetActive(view.config.USE_WORK_STATE_NODE)
            if isWorking or isResting then
                view.workStateNode.workingIcon.gameObject:SetActive(isWorking)
                view.workStateNode.restIcon.gameObject:SetActive(isResting)
                if view.workStateNode.stateTxt then
                    view.workStateNode.stateTxt.text = isWorking and Language.LUA_SPACESHIP_CHAR_WORKING or Language.LUA_SPACESHIP_CHAR_RESTING
                end
                if view.workStateNode.timeTxt then
                    view.workStateNode.timeTxt:InitCountDownText(spaceship:GetCharLeftTime(charId) + DateTimeUtils.GetCurrentTimestampBySeconds())
                end
            end
        else
            view.workStateNode.gameObject:SetActive(false)
        end
    end
end

function SpaceshipUtils.getRoomLvTableById(roomId)
    local type = SpaceshipUtils.getRoomTypeByRoomId(roomId)
    return SpaceshipUtils.getRoomLvTableByType(type)
end

function SpaceshipUtils.getRoomLvTableByType(roomType)
    if roomType == GEnums.SpaceshipRoomType.ControlCenter then
        return Tables.spaceshipControlCenterLvTable
    elseif roomType == GEnums.SpaceshipRoomType.ManufacturingStation then
        return Tables.spaceshipManufacturingStationLvTable
    elseif roomType == GEnums.SpaceshipRoomType.GrowCabin then
        return Tables.spaceshipGrowCabinLvTable
    elseif roomType == GEnums.SpaceshipRoomType.CommandCenter then
        return Tables.spaceshipCommandCenterLvTable
    elseif roomType == GEnums.SpaceshipRoomType.GuestRoomClueExtension then
        return Tables.spaceshipGuestRoomClueLvTable
    end
end

function SpaceshipUtils.getRoomRecipeOutcomesByLv(roomId, lv, onlyNew)
    local roomType = SpaceshipUtils.getRoomTypeByRoomId(roomId)
    local outcomeItemIds = {}
    if roomType == GEnums.SpaceshipRoomType.ManufacturingStation then
        for _, v in pairs(Tables.spaceshipManufactureFormulaTable) do
            local isValid
            if onlyNew then
                isValid = v.level == lv
            else
                isValid = v.level <= lv
            end
            if isValid then
                local itemData = Tables.itemTable[v.outcomeItemId]
                table.insert(outcomeItemIds, {
                    id = v.outcomeItemId,
                    sortId1 = itemData.sortId1,
                    sortId2 = itemData.sortId2,
                })
            end
        end
    elseif roomType == GEnums.SpaceshipRoomType.GrowCabin then
        for _, v in pairs(Tables.spaceshipGrowCabinFormulaTable) do
            local isValid
            if onlyNew then
                isValid = v.level == lv
            else
                isValid = v.level <= lv
            end
            if isValid then
                local itemData = Tables.itemTable[v.outcomeItemId]
                table.insert(outcomeItemIds, {
                    id = v.outcomeItemId,
                    sortId1 = itemData.sortId1,
                    sortId2 = itemData.sortId2,
                })
            end
        end
        for _, v in pairs(Tables.spaceshipGrowCabinSeedFormulaTable) do
            local isValid
            if onlyNew then
                isValid = v.level == lv
            else
                isValid = v.level <= lv
            end
            if isValid then
                local itemData = Tables.itemTable[v.outcomeseedItemId]
                table.insert(outcomeItemIds, {
                    id = v.outcomeseedItemId,
                    sortId1 = itemData.sortId1,
                    sortId2 = itemData.sortId2,
                })
            end
        end
    end
    table.sort(outcomeItemIds, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
    return outcomeItemIds
end

function SpaceshipUtils.getRoomTypeByRoomId(roomId)
    if not roomId then
        return
    end
    local haveRoom, roomData = GameInstance.player.spaceship:TryGetRoom(roomId)
    if haveRoom then
        return roomData.type
    end
end

function SpaceshipUtils.getUpgradeEffectInfos(roomId, newLv)
    local roomType = SpaceshipUtils.getRoomTypeByRoomId(roomId)
    local lvTable = SpaceshipUtils.getRoomLvTableByType(roomType)
    local oldLv = newLv - 1
    local oldLvData = lvTable[oldLv]
    local newLvData = lvTable[newLv]
    local effectInfos = {}
    if oldLvData.stationMaxCount ~= newLvData.stationMaxCount then

        table.insert(effectInfos, {
            icon = "icon_spaceship_room_effect_station",
            name = Language.LUA_SPACESHIP_UPGRADE_EFFECT_STATION_COUNT,
            oldValue = oldLvData.stationMaxCount,
            newValue = newLvData.stationMaxCount,
        })
    end
    if roomType == GEnums.SpaceshipRoomType.ControlCenter then
        if not string.isEmpty(newLvData.unlockArea) then

            local oldRoomCount = 0
            for k = 1, newLv - 1 do
                local data = lvTable[k]
                oldRoomCount = oldRoomCount + (string.isEmpty(data.unlockArea) and 0 or 1)
            end

            table.insert(effectInfos, {
                icon = "icon_spaceship_room_effect_new_room",
                name = string.format("[%s]", newLvData.unlockAreaText),
                newText = Language.LUA_SPACESHIP_UPGRADE_EFFECT_UNLOCK
            })

            table.insert(effectInfos, {
                icon = "icon_spaceship_room_effect_new_room_build",
                name = Language.LUA_SPACESHIP_UPGRADE_EFFECT_NEW_AREA,
                oldValue = oldRoomCount,
                newValue = oldRoomCount + 1,
            })
        end


        if newLvData.unlockRoomType ~= GEnums.SpaceshipRoomType.Invalid then
            local roomTypeData = Tables.spaceshipRoomTypeTable[newLvData.unlockRoomType]
            local roomName = roomTypeData.name
            table.insert(effectInfos, {
                icon = roomTypeData.icon,
                name = roomName,
                newText = Language.LUA_SPACESHIP_UPGRADE_EFFECT_NEW
            })
        end



        local newRoomLvs = {}
        for k, v in pairs(Tables.spaceshipRoomLvTable) do
            if v.conditionType == GEnums.ConditionType.CheckSpaceshipRoomLevel and v.progressToCompare == newLv then
                if Tables.spaceshipRoomLvHelperTable:ContainsKey(k) then
                    table.insert(newRoomLvs, {
                        id = k,
                        helperData = Tables.spaceshipRoomLvHelperTable[k],
                    })
                end
            end
        end
        for _, info in ipairs(newRoomLvs) do
            local typeData = Tables.spaceshipRoomTypeTable[info.helperData.roomType]
            table.insert(effectInfos, {
                icon = typeData.icon .. "_limit",
                name = string.format(Language.LUA_SPACESHIP_UPGRADE_EFFECT_ROOM_MAX_LV, typeData.name),
                oldValue = info.helperData.level - 1,
                newValue = info.helperData.level,
            })
        end
    elseif roomType == GEnums.SpaceshipRoomType.ManufacturingStation then
        if oldLvData.machineCapacity ~= newLvData.machineCapacity then

            table.insert(effectInfos, {
                icon = "icon_spaceship_room_effect_capacity",
                name = Language.LUA_SPACESHIP_UPGRADE_EFFECT_MACHINE_CAPACITY,
                oldValue = oldLvData.machineCapacity,
                newValue = newLvData.machineCapacity,
            })
        end
    elseif roomType == GEnums.SpaceshipRoomType.GrowCabin then
        if newLvData.unlockPlantingField.Count > 0 then

            local oldFiledCount = 0
            for k = 1, newLv - 1 do
                local data = lvTable[k]
                oldFiledCount = oldFiledCount + data.unlockPlantingField.Count
            end
            table.insert(effectInfos, {
                icon = "icon_spaceship_room_effect_field",
                name = Language.LUA_SPACESHIP_UPGRADE_EFFECT_PLANTING_FIELD,
                oldValue = oldFiledCount,
                newValue = oldFiledCount + newLvData.unlockPlantingField.Count,
            })
        end
    elseif roomType == GEnums.SpaceshipRoomType.GuestRoomClueExtension then
        if newLvData.baseCreditReward > 0 then
            table.insert(effectInfos, {
                icon = "ss_room_attr_icon_room_base_credit_award",
                name = Language.LUA_CLUE_BASE_CREDIT_REWARD,
                oldValue = oldLvData.baseCreditReward,
                newValue = newLvData.baseCreditReward
            })
        end
        if newLvData.baseInfoReward > 0 then
            table.insert(effectInfos, {
                icon = "ss_room_attr_icon_room_intelligence_exchange_award",
                name = Language.LUA_CLUE_BASE_INFO_REWARD,
                oldValue = oldLvData.baseInfoReward,
                newValue = newLvData.baseInfoReward
            })
        end
        if newLvData.maxCalcRoleCount > 0 then
            table.insert(effectInfos, {
                icon = "ss_room_attr_icon_room_clue_people_stat",
                name = Language.LUA_CLUE_FRIEND_TOTAL_COUNT,
                oldValue = oldLvData.maxCalcRoleCount,
                newValue = newLvData.maxCalcRoleCount
            })
        end
        if newLvData.extraCreditPerRole > 0 then
            table.insert(effectInfos, {
                icon = "ss_room_attr_icon_room_credit_award_extra",
                name = Language.LUA_CLUE_EXTRA_CREDIT_REWARD,
                oldValue = oldLvData.extraCreditPerRole,
                oldValueShow = string.format(Language.LUA_SPACESHIP_CLUE_EXTRA_REWARD_PER_PERSON, oldLvData.extraCreditPerRole),
                newValue = newLvData.extraCreditPerRole,
                newValueShow = string.format(Language.LUA_SPACESHIP_CLUE_EXTRA_REWARD_PER_PERSON, newLvData.extraCreditPerRole),
            })
        end
        if newLvData.extraInfoPerRole > 0 then
            table.insert(effectInfos, {
                icon = "ss_room_attr_icon_room_intelligence_exchange_extra",
                name = Language.LUA_CLUE_EXTRA_CREDIT_REWARD,
                oldValue = oldLvData.extraInfoPerRole,
                oldValueShow = string.format(Language.LUA_SPACESHIP_CLUE_EXTRA_REWARD_PER_PERSON, oldLvData.extraInfoPerRole),
                newValue = newLvData.extraInfoPerRole,
                newValueShow = string.format(Language.LUA_SPACESHIP_CLUE_EXTRA_REWARD_PER_PERSON, newLvData.extraInfoPerRole),

            })
        end
    end
    return effectInfos
end

local RoomEffectIcon = {
    station = "icon_spaceship_room_effect_station",
    newRoom = "icon_spaceship_room_effect_new_room",
    roomLv = "icon_spaceship_room_effect_room_lv",
    capacity = "icon_spaceship_room_effect_capacity",
    field = "icon_spaceship_room_effect_field",
}

function SpaceshipUtils.getMaxUpgradeEffectInfos(roomId)
    local roomType = SpaceshipUtils.getRoomTypeByRoomId(roomId)
    local lvTable = SpaceshipUtils.getRoomLvTableByType(roomType)
    local maxLv = #lvTable
    local maxLvData = lvTable[maxLv]
    local effectInfos = {}


    table.insert(effectInfos, {
        icon = RoomEffectIcon.station,
        name = Language.LUA_SPACESHIP_UPGRADE_EFFECT_STATION_COUNT,
        value = maxLvData.stationMaxCount,
    })

    if roomType == GEnums.SpaceshipRoomType.ControlCenter then

        local roomCount = 0
        for k = 1, maxLv do
            local data = lvTable[k]
            roomCount = roomCount + (string.isEmpty(data.unlockArea) and 0 or 1)
        end
        table.insert(effectInfos, {
            icon = RoomEffectIcon.newRoom,
            name = Language.LUA_SPACESHIP_UPGRADE_EFFECT_NEW_ROOM,
            value = roomCount,
        })


        local newRoomLvs = {}
        for k, v in pairs(Tables.spaceshipRoomLvTable) do
            if v.conditionType == GEnums.ConditionType.CheckSpaceshipRoomLevel then
                local success, helperData = Tables.spaceshipRoomLvHelperTable:TryGetValue(k)
                if success and helperData.level == GameInstance.player.spaceship:GetRoomMaxLvByType(helperData.roomType) then
                    table.insert(newRoomLvs, {
                        id = k,
                        helperData = helperData,
                    })
                end
            end
        end
        for _, info in ipairs(newRoomLvs) do
            local typeData = Tables.spaceshipRoomTypeTable[info.helperData.roomType]
            table.insert(effectInfos, {
                icon = typeData.icon,
                name = string.format(Language.LUA_SPACESHIP_UPGRADE_EFFECT_ROOM_MAX_LV, typeData.name),
                value = info.helperData.level,
            })
        end
    elseif roomType == GEnums.SpaceshipRoomType.ManufacturingStation then

        table.insert(effectInfos, {
            icon = RoomEffectIcon.capacity,
            name = Language.LUA_SPACESHIP_UPGRADE_EFFECT_MACHINE_CAPACITY,
            value = maxLvData.machineCapacity,
        })
    elseif roomType == GEnums.SpaceshipRoomType.GrowCabin then

        table.insert(effectInfos, {
            icon = RoomEffectIcon.field,
            name = Language.LUA_SPACESHIP_UPGRADE_EFFECT_PLANTING_FIELD,
            value = SpaceshipConst.GROW_CABIN_MAX_FILED,
        })
    elseif roomType == GEnums.SpaceshipRoomType.GuestRoomClueExtension then
        if maxLvData.baseCreditReward > 0 then
            table.insert(effectInfos, {
                icon = "ss_room_attr_icon_room_base_credit_award",
                name = Language.LUA_CLUE_BASE_CREDIT_REWARD,
                value = maxLvData.baseCreditReward
            })
        end
        if maxLvData.baseInfoReward > 0 then
            table.insert(effectInfos, {
                icon = "ss_room_attr_icon_room_intelligence_exchange_award",
                name = Language.LUA_CLUE_BASE_INFO_REWARD,
                value = maxLvData.baseInfoReward
            })
        end
        if maxLvData.maxCalcRoleCount > 0 then
            table.insert(effectInfos, {
                icon = "ss_room_attr_icon_room_clue_people_stat",
                name = Language.LUA_CLUE_FRIEND_TOTAL_COUNT,
                value = maxLvData.maxCalcRoleCount
            })
        end
        if maxLvData.extraCreditPerRole > 0 then
            table.insert(effectInfos, {
                icon = "ss_room_attr_icon_room_credit_award_extra",
                name = Language.LUA_CLUE_EXTRA_CREDIT_REWARD,
                value = string.format(Language.LUA_SPACESHIP_CLUE_EXTRA_REWARD_PER_PERSON, maxLvData.extraCreditPerRole)

            })
        end
        if maxLvData.extraInfoPerRole > 0 then
            table.insert(effectInfos, {
                icon = "ss_room_attr_icon_room_intelligence_exchange_extra",
                name = Language.LUA_CLUE_EXTRA_CREDIT_REWARD,
                value = string.format(Language.LUA_SPACESHIP_CLUE_EXTRA_REWARD_PER_PERSON, maxLvData.extraInfoPerRole)
            })
        end
    end
    return effectInfos
end

function SpaceshipUtils.getRoomColor(roomId)
    local type = SpaceshipUtils.getRoomTypeByRoomId(roomId)
    local roomTypeData = Tables.spaceshipRoomTypeTable[type]
    return UIUtils.getColorByString(roomTypeData.color)
end


local Room2AttrMap = {
    [GEnums.SpaceshipRoomType.ControlCenter] = {
        GEnums.SpaceshipRoomAttrType.PSRecoveryRate,
        GEnums.SpaceshipRoomAttrType.RoomPSCostRate,
    },
    [GEnums.SpaceshipRoomType.ManufacturingStation] = {
        GEnums.SpaceshipRoomAttrType.RoomPSCostRate,
        GEnums.SpaceshipRoomAttrType.ManufacturingStationCharExpMaterialProduceRate,
        GEnums.SpaceshipRoomAttrType.ManufacturingStationWeaponExpMaterialProduceRate,
    },
    [GEnums.SpaceshipRoomType.GrowCabin] = {
        GEnums.SpaceshipRoomAttrType.RoomPSCostRate,
        GEnums.SpaceshipRoomAttrType.GrowCabinCharMaterialProduceRate,
        GEnums.SpaceshipRoomAttrType.GrowCabinSkillMaterialProduceRate,
        GEnums.SpaceshipRoomAttrType.GrowCabinWeaponMaterialProduceRate,
    },
    [GEnums.SpaceshipRoomType.GuestRoomClueExtension] = {
        GEnums.SpaceshipRoomAttrType.RoomPSCostRate,
        GEnums.SpaceshipRoomAttrType.GuestRoomCharCollectRateIncreate,
        GEnums.SpaceshipRoomAttrType.GuestRoomClueProbabilityIncreaseShow,
    }
}
local AttrBaseValue = {
    [GEnums.SpaceshipRoomAttrType.PSRecoveryRate] = Tables.spaceshipConst.basePhysicalStrengthRecoveryRate,
    [GEnums.SpaceshipRoomAttrType.RoomPSCostRate] = Tables.spaceshipConst.basePhysicalStrengthCostRate,
    [GEnums.SpaceshipRoomAttrType.RoomProduceRate] = Tables.spaceshipConst.defaultManufacturingStationProduceRate,
    [GEnums.SpaceshipRoomAttrType.GrowCabinCharMaterialProduceRate] = Tables.spaceshipConst.defaultGrowCabinProduceRate,
    [GEnums.SpaceshipRoomAttrType.GrowCabinSkillMaterialProduceRate] = Tables.spaceshipConst.defaultGrowCabinProduceRate,
    [GEnums.SpaceshipRoomAttrType.GrowCabinWeaponMaterialProduceRate] = Tables.spaceshipConst.defaultGrowCabinProduceRate,
    [GEnums.SpaceshipRoomAttrType.ManufacturingStationCharExpMaterialProduceRate] = Tables.spaceshipConst.defaultManufacturingStationProduceRate,
    [GEnums.SpaceshipRoomAttrType.ManufacturingStationWeaponExpMaterialProduceRate] = Tables.spaceshipConst.defaultManufacturingStationProduceRate,
    [GEnums.SpaceshipRoomAttrType.GuestRoomCharCollectRateIncreate] = Tables.spaceshipConst.baseGuestRoomClueCollectProduceRate,
}
local AttrDefaultAddValue = {
    [GEnums.SpaceshipRoomAttrType.RoomProduceRate] = Tables.spaceshipConst.baseManufacturingStationProduceRate,
    [GEnums.SpaceshipRoomAttrType.ManufacturingStationCharExpMaterialProduceRate] = Tables.spaceshipConst.baseManufacturingStationProduceRate,
    [GEnums.SpaceshipRoomAttrType.ManufacturingStationWeaponExpMaterialProduceRate] = Tables.spaceshipConst.baseManufacturingStationProduceRate,
    [GEnums.SpaceshipRoomAttrType.GrowCabinCharMaterialProduceRate] = Tables.spaceshipConst.baseGrowCabinProduceRate,
    [GEnums.SpaceshipRoomAttrType.GrowCabinSkillMaterialProduceRate] = Tables.spaceshipConst.baseGrowCabinProduceRate,
    [GEnums.SpaceshipRoomAttrType.GrowCabinWeaponMaterialProduceRate] = Tables.spaceshipConst.baseGrowCabinProduceRate,
    [GEnums.SpaceshipRoomAttrType.GuestRoomCharCollectRateIncreate] = Tables.spaceshipConst.spaceshipGuestRoomCharAddPointIncrease,
}

function SpaceshipUtils.preCalcRoomAttrs(roomId, charIds)
    local roomAttrMap = {}
    local roomType = SpaceshipUtils.getRoomTypeByRoomId(roomId)

    local charCount = #charIds


    for _, t in ipairs(Room2AttrMap[roomType]) do
        local info = {
            type = t,
            typeData = Tables.spaceshipRoomAttrTypeTable:GetValue(t),
        }
        info.sortId = info.typeData.sortId
        info.attr = {
            baseValue = AttrBaseValue[t],
            addFromCharSkill = 0,
            charSkillRate = 0,
            clueSkill = {},
        }
        if AttrDefaultAddValue[t] then
            info.attr.addFromCharStation = AttrDefaultAddValue[t] * charCount
        else
            info.attr.addFromCharStation = 0
        end
        roomAttrMap[t] = info
    end

    local spaceship = GameInstance.player.spaceship
    for _, charId in ipairs(charIds) do
        local char = spaceship.characters:get_Item(charId)
        for _, skillId in pairs(char.skills) do
            local skillData = Tables.spaceshipSkillTable[skillId]
            if skillData.roomType == roomType then
                local attrType, rate
                if skillData.effectType == GEnums.SpaceshipStationEffectType.PSRecoveryRateAccByPercent then

                    attrType = GEnums.SpaceshipRoomAttrType.PSRecoveryRate
                    rate = skillData.parameters[0].valueFloatList[0]
                elseif skillData.effectType == GEnums.SpaceshipStationEffectType.RoomPSCostRateReduceByPercent then

                    attrType = GEnums.SpaceshipRoomAttrType.RoomPSCostRate
                    rate = -skillData.parameters[0].valueFloatList[0]
                elseif skillData.effectType == GEnums.SpaceshipStationEffectType.ManufactringTypeProduceRateAccByPercent then
                    local typeInt = skillData.parameters.Count > 1 and skillData.parameters[1].valueIntList[0]
                    if typeInt and typeInt == 2 then

                        attrType = GEnums.SpaceshipRoomAttrType.ManufacturingStationWeaponExpMaterialProduceRate
                    elseif typeInt and typeInt == 1 then

                        attrType = GEnums.SpaceshipRoomAttrType.ManufacturingStationCharExpMaterialProduceRate
                    else
                        attrType = GEnums.SpaceshipRoomAttrType.RoomProduceRate
                    end
                    rate = skillData.parameters[0].valueFloatList[0]
                elseif skillData.effectType == GEnums.SpaceshipStationEffectType.RoomPlantTypeProduceRateAccByPercent then

                    local typeInt = skillData.parameters[1].valueIntList[0]
                    if typeInt == 1 then

                        attrType = GEnums.SpaceshipRoomAttrType.GrowCabinCharMaterialProduceRate
                    elseif typeInt == 2 then

                        attrType = GEnums.SpaceshipRoomAttrType.GrowCabinSkillMaterialProduceRate
                    elseif typeInt == 3 then

                        attrType = GEnums.SpaceshipRoomAttrType.GrowCabinWeaponMaterialProduceRate
                    end
                    rate = skillData.parameters[0].valueFloatList[0]
                elseif skillData.effectType == GEnums.SpaceshipStationEffectType.GuestRoomCollectSpeedAccByValue then
                    attrType = GEnums.SpaceshipRoomAttrType.GuestRoomCharCollectRateIncreate
                    rate = skillData.parameters[0].valueFloatList[0]
                end
                if attrType then
                    local attr = roomAttrMap[attrType].attr
                    attr.charSkillRate = attr.charSkillRate + rate
                end

                if skillData.effectType == GEnums.SpaceshipStationEffectType.GuestRoomSpecificClueCollectProbAccByPercent then
                    attrType = GEnums.SpaceshipRoomAttrType.GuestRoomClueProbabilityIncreaseShow
                    local attr = roomAttrMap[attrType].attr
                    local clueIndex = skillData.parameters[0].valueFloatList[0]
                    local nowLevel = attr.clueSkill[clueIndex] or 0
                    local level = skillData.parameters[1].valueFloatList[0]
                    if nowLevel < level then
                        attr.clueSkill[clueIndex] = level
                    end
                end
            end
        end
    end
    local roomAttrs = {}
    for _, v in pairs(roomAttrMap) do
        v.attr.addFromCharSkill = ((v.attr.baseValue or 0) + v.attr.addFromCharStation) * v.attr.charSkillRate

        v.attr.Value = (v.attr.baseValue or 0) + v.attr.addFromCharStation + v.attr.addFromCharSkill
        table.insert(roomAttrs, v)
    end
    logger.info("preCalcRoomAttrs", roomAttrs)
    return roomAttrs
end

function SpaceshipUtils.getDeconstructDescByType(type)
    local content, subContent, roomName, roomTypeData
    roomTypeData = Tables.spaceshipRoomTypeTable[type]
    roomName = roomTypeData.name
    if type == GEnums.SpaceshipRoomType.ManufacturingStation then
        subContent = Language.LUA_SPACESHIP_DECONSTRUCT_MANUFACTURING_STATION_SUB_DESC
    elseif type == GEnums.SpaceshipRoomType.GrowCabin then
        subContent = Language.LUA_SPACESHIP_DECONSTRUCT_GROW_CABIN_SUB_DESC
    elseif type == GEnums.SpaceshipRoomType.CommandCenter then
        subContent = Language.LUA_SPACESHIP_DECONSTRUCT_COMMAND_CENTER_SUB_DESC
    end
    content = string.format(Language.LUA_SPACESHIP_DECONSTRUCT_ROOM, roomName)
    return content, subContent
end

function SpaceshipUtils.getCostItemByRoomType(roomType, curLev)
    local sumCost = {}
    local roomLevTable = SpaceshipUtils.getRoomLvTableByType(roomType)
    for i = 1, curLev do
        local typeLvData = roomLevTable[i]
        local levCostItems = Tables.SpaceshipRoomLvTable[typeLvData.id].costItems
        for i, v in pairs(levCostItems) do
            sumCost[v.id] = sumCost[v.id] or 0
            sumCost[v.id] = sumCost[v.id] + v.count
        end
    end
    return sumCost
end

function SpaceshipUtils.getSpaceshipNowAndMaxRoom(roomType)
    local centerRoomLv = GameInstance.player.spaceship:GetRoomLv(Tables.spaceshipConst.controlCenterRoomId)
    local lvData = Tables.spaceshipControlCenterLvTable[centerRoomLv]
    local roomTypeInfos = GameInstance.player.spaceship.roomTypeInfos
    local roomCount, maxCount
    roomCount = roomTypeInfos:ContainsKey(roomType) and roomTypeInfos[roomType].Count or 0
    if roomType == GEnums.SpaceshipRoomType.CommandCenter then
        maxCount = lvData.commandCenterMaxCount;
    elseif roomType == GEnums.SpaceshipRoomType.GrowCabin then
        maxCount = lvData.growCabinMaxCount;
    elseif roomType == GEnums.SpaceshipRoomType.ManufacturingStation then
        maxCount = lvData.manufacturingStationMaxCount;
    elseif roomType == GEnums.SpaceshipRoomType.GuestRoomClueExtension then
        maxCount = 1;
    elseif roomType == GEnums.SpaceshipRoomType.GuestRoom then
        maxCount = 1;
    elseif roomType == GEnums.SpaceshipRoomType.ControlCenter then
        maxCount = 1;
    end
    return roomCount, maxCount
end

function SpaceshipUtils.playSSDialog(roomID, dialogId)
    if not Utils.isInSpaceShip() then
        logger.error("不在飞船场景内playSSDialog")
        return
    end
    local succ, node = GameWorld.worldInfo.curLevel.data.cabinSlotInfoLut:TryGetValue(roomID)
    if not succ then
        GameWorld.dialogManager:PlayDialogByJsonId(dialogId)
        return
    end
    local pos = node.position;
    local rot = node.rotation;
    GameWorld.dialogManager:PlayDialogByJsonId(dialogId, true, pos, rot)
end



function SpaceshipUtils.getRoomHelpLimit(roomId)
    local hasValue, roomInfo = GameInstance.player.spaceship:TryGetRoom(roomId)
    if not hasValue  then
        logger.error("飞船舱室room id不存在")
        return 0
    end
    local roomType = roomInfo.roomType
    if roomType == GEnums.SpaceshipRoomType.ControlCenter then
        return Tables.spaceshipConst.centerBeHelpedCountLimit
    elseif roomType == GEnums.SpaceshipRoomType.ManufacturingStation then
        return Tables.spaceshipConst.manufacturingStationBeHelpedCountLimit
    elseif roomType == GEnums.SpaceshipRoomType.GrowCabin then
        return Tables.spaceshipConst.growCabinBeHelpedCountLimit
    elseif roomType == GEnums.SpaceshipRoomType.CommandCenter then
        return Tables.spaceshipConst.commandCenterBeHelpedCountLimit
    else
        return 0
    end
end


function SpaceshipUtils.getRoomSerialNum(number)
    return UIUtils.getSerialNum(number)
end


function SpaceshipUtils.getFormatCabinSerialNum(roomId, number)
    local ignoreSerialNumType =
    {
        [GEnums.SpaceshipRoomType.Any] = true,
        [GEnums.SpaceshipRoomType.GuestRoom] = true,
        [GEnums.SpaceshipRoomType.GuestRoomClueExtension] = true,
        [GEnums.SpaceshipRoomType.FlexibleTypeB] = true,
        [GEnums.SpaceshipRoomType.FlexibleTypeA] = true,
        [GEnums.SpaceshipRoomType.Invalid] = true,
        [GEnums.SpaceshipRoomType.ControlCenter] = true,
    }
    local hasValue, roomInfo = GameInstance.player.spaceship:TryGetRoom(roomId)
    if hasValue then
        local roomTypeData = Tables.spaceshipRoomTypeTable[roomInfo.type]
        if ignoreSerialNumType[roomInfo.type] then
            return roomTypeData.name
        else
            return string.format(Language.LUA_SPACESHIP_CABIN_SERIAL_NUMBER, roomTypeData.name, SpaceshipUtils.getRoomSerialNum(number))
        end
    end
    return string.format("error:%s is not exit", roomId)
end

function SpaceshipUtils.getFormatCabinSerialNumByName(roomName, number)
    return string.format(Language.LUA_SPACESHIP_CABIN_SERIAL_NUMBER, roomName, SpaceshipUtils.getRoomSerialNum(number))
end

function SpaceshipUtils.getFriendMissionTable()
    local spaceship = GameInstance.player.spaceship
    local missionTable = {
        [1] = {
            finish = function()
                local friendInfo = spaceship:GetFriendRoleInfo()
                return spaceship:CheckAllRoomCanNotHelp(friendInfo.roleId)
            end,
            showText = Language.LUA_SPACESHIP_VISIT_MISSION_HELP_FRIEND
        },
        [2] = {
            finish = function()
                local isBuild = spaceship:IsRoomBuild(Tables.spaceshipConst.guestRoomClueExtensionId)
                if not isBuild then
                    return true
                end
                local friendInfo = spaceship:GetFriendRoleInfo()
                if isBuild and friendInfo then
                    return spaceship:CheckUnableHelpGuestRoomClueExchange() or spaceship:CheckIsJoinFriendClueExchange(friendInfo.roleId)
                end
                return true
            end,
            showText = Language.LUA_SPACESHIP_VISIT_MISSION_CLUE_COLLECTION
        }
    }
    return missionTable
end

function SpaceshipUtils.PlayMoneyNodeGainAnim(moneyNode, itemId, itemCount)
    local gainNode = moneyNode.view.gainNode
    gainNode.animationWrapper:ClearTween()
    gainNode.gameObject:SetActive(true)
    gainNode.animationWrapper:PlayWithTween("spaceshipcontrolcentergain_in", function()
        gainNode.gameObject:SetActive(false)
    end)
    gainNode.creditTxt.text = string.format("+%d", itemCount)
    local limitCount
    local hasMoneyCfg, moneyCfg = Tables.moneyConfigTable:TryGetValue(itemId)
    if hasMoneyCfg then
        limitCount = moneyCfg.MoneyClearLimit
    end
    moneyNode:InitMoneyCell(itemId, true, false, true, limitCount)
end

function SpaceshipUtils.InitMoneyLimitCell(moneyNode, itemId)
    local hasMoneyCfg, moneyCfg = Tables.moneyConfigTable:TryGetValue(itemId)
    local limitCount
    if hasMoneyCfg then
        limitCount = moneyCfg.MoneyClearLimit
    end
    moneyNode:InitMoneyCell(itemId, true, false, true, limitCount)
    return limitCount or 0
end

function SpaceshipUtils.ShowClueOutcomePopup(csItems, source, creditCell, infoTokenCell)
    local itemMap = {}
    for i = 0, csItems.Count - 1 do
        local item = csItems[i]

        if itemMap[item.id or item.Id] then
            local accCount = itemMap[item.id or item.Id]
            itemMap[item.id or item.Id] = accCount + (item.Count or 0) + (item.count or 0)
        else
            itemMap[item.id or item.Id] = (item.Count or 0) + (item.count or 0)
        end
    end
    for id, count in pairs(itemMap) do
        if id == Tables.spaceshipConst.creditItemId and creditCell then
            SpaceshipUtils.PlayMoneyNodeGainAnim(creditCell, id, count)
        elseif id == Tables.spaceshipConst.infoTokenItemId and infoTokenCell then
            SpaceshipUtils.PlayMoneyNodeGainAnim(infoTokenCell, id, count)
        else
            logger.error("SpaceshipUtils.ShowClueOutcomePopup item id:", id)
        end
    end
    local items = {}
    for id, count in pairs(itemMap) do
        table.insert(items, {
            id = id,
            count = count,
        })
    end
    if source == CS.Beyond.GEnums.RewardSourceType.OpenInfoExchangeReward then
        if GameInstance.player.spaceship:IsWaitingForHandleOneKeyHarvest() then
            for i, v in ipairs(items) do
                GameInstance.player.spaceship:AddHarvestItem(v.id, v.count)
            end
            return
        end
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
            title = Language.LUA_DEFAULT_SYSTEM_REWARD_POP_UP_TITLE,
            items = items,
        })
    end
end


_G.SpaceshipUtils = SpaceshipUtils
return SpaceshipUtils
