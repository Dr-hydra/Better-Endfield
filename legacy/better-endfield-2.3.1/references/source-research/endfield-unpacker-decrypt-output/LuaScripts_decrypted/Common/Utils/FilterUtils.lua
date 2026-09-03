local FilterUtils = {}












local CALC_TYPE = {
    AND = "AND",
    OR = "OR",
}





FilterUtils.EQUIP_PART_FILTER_TYPE = {
    GEnums.CraftShowingType.EquipBody,
    GEnums.CraftShowingType.EquipHead,
    GEnums.CraftShowingType.EquipRing
}



























































function FilterUtils.processItemDefault(itemId, instId)
    local _, itemData = Tables.itemTable:TryGetValue(itemId)
    if not itemData then
        logger.error("itemData is nil, templateId: " .. itemId)
        return nil
    end

    local indexId = instId
    if indexId == nil or indexId == 0 then
        indexId = itemId
    end

    local info = {
        id = itemId,
        instId = instId,
        indexId = indexId,
        data = itemData,
        rarity = itemData.rarity,
        sortId1 = itemData.sortId1,
        sortId2 = itemData.sortId2,
    }
    local isNew = instId and GameInstance.player.inventory:IsNewItem(itemId, instId) or GameInstance.player.inventory:IsNewItem(itemId)
    info.newOrder = isNew and 1 or 0
    info.realId = instId and (itemId .. instId) or itemId
    return info
end

function FilterUtils.processWeaponUpgradeIngredient(templateId, instId)
    local infoDefault
    local isWeapon = instId ~= nil and instId > 0
    if isWeapon then
        infoDefault = FilterUtils.processWeapon(templateId, instId)
        infoDefault.forceSortKey = 0
        infoDefault.forceSortKeyReverse = 0
    else
        infoDefault = FilterUtils.processItemDefault(templateId, instId)

        infoDefault.forceSortKey = infoDefault.rarity
        infoDefault.forceSortKeyReverse = -infoDefault.rarity
    end

    if not infoDefault then
        return nil
    end

    infoDefault.inventoryCount = isWeapon and 1 or Utils.getItemCount(templateId)
    infoDefault.ingredientIndex = infoDefault.rarity
    infoDefault.isWeapon = isWeapon

    return infoDefault
end

function FilterUtils.processWeapon(templateId, instId)
    local infoDefault = FilterUtils.processItemDefault(templateId, instId)
    if not infoDefault then
        return nil
    end

    local weaponInst = CharInfoUtils.getWeaponByInstId(instId)
    if not weaponInst then
        return nil
    end

    local _, weaponCfg = Tables.weaponBasicTable:TryGetValue(templateId)
    if not weaponCfg then
        return nil
    end

    infoDefault.weaponLv = weaponInst.weaponLv
    infoDefault.weaponType = weaponCfg.weaponType
    infoDefault.isWeapon = true

    return infoDefault
end

function FilterUtils.processWeaponPotential(templateId, instId)
    local _, itemData = Tables.itemTable:TryGetValue(templateId)
    if itemData.type == GEnums.ItemType.Weapon then
        local infoWeapon = FilterUtils.processWeapon(templateId, instId)
        local weaponInst = CharInfoUtils.getWeaponByInstId(instId)
        infoWeapon.isItemMarker = 0
        infoWeapon.noGemAttached = weaponInst.attachedGemInstId <= 0 and 1 or 0
        infoWeapon.inventoryCount = 1
        return infoWeapon
    else
        local infoItem = FilterUtils.processItemDefault(templateId, instId)
        infoItem.isItemMarker = 1
        infoItem.inventoryCount = Utils.getItemCount(templateId)
        return infoItem
    end
end

function FilterUtils.processWeaponGem(templateId, instId, extraArgs)
    local infoDefault = FilterUtils.processItemDefault(templateId, instId)
    if not infoDefault then
        return nil
    end

    if not instId then
        return infoDefault
    end

    local gemInst = CharInfoUtils.getGemByInstId(instId)
    if not gemInst then
        return infoDefault
    end
    infoDefault.trashIndex = gemInst.isTrash and 1 or 0
    infoDefault.trashIndexReverse = -infoDefault.trashIndex
    infoDefault.lockedIndex = gemInst.isLocked and 1 or 0
    infoDefault.lockedIndexReverse = -infoDefault.lockedIndex
    infoDefault.equippedWeaponInstId = gemInst.weaponInstId
    infoDefault.equippedWeaponInstIdReverse = -infoDefault.equippedWeaponInstId
    infoDefault.isEquippedSort = gemInst.weaponInstId > 0 and 1 or 0
    infoDefault.isEquippedSortReverse = -infoDefault.isEquippedSort


    infoDefault.enableOnWeapon = false
    infoDefault.matchWeaponSkillCount = 0
    infoDefault.matchWeaponSkillIndex = 0
    local skillMap = {}
    for _, skillTerm in pairs(gemInst.termList) do
        skillMap[skillTerm.termId] = true
        local weaponSkillList = extraArgs and extraArgs.weaponSkillList
        if weaponSkillList then
            local termCfg = Tables.gemTable:GetValue(skillTerm.termId)
            for _, weaponSkill in pairs(weaponSkillList) do
                local skillCfg = CharInfoUtils.getSkillCfg(weaponSkill.skillId, weaponSkill.level)
                if skillCfg.tagId == termCfg.tagId then
                    infoDefault.enableOnWeapon = true
                    infoDefault.matchWeaponSkillCount = infoDefault.matchWeaponSkillCount + 1
                    infoDefault.matchWeaponSkillIndex = infoDefault.matchWeaponSkillIndex + skillTerm.cost
                end
            end
        end
    end

    infoDefault.skillMap = skillMap

    local isPerfectMatch = UIUtils.getGemWishListPerfectMatch(instId)
    infoDefault.gemPerfectMatchSort = isPerfectMatch and 1 or 0
    infoDefault.gemPerfectMatchSortReverse = -infoDefault.gemPerfectMatchSort

    return infoDefault
end





function FilterUtils.processEquip(templateId, instId, extraArgs)
    local infoDefault = FilterUtils.processItemDefault(templateId, instId)
    if not infoDefault then
        return nil
    end

    local _, equipTemplate = Tables.equipTable:TryGetValue(templateId)
    if equipTemplate then
        infoDefault.minWearLv = equipTemplate.minWearLv
        infoDefault.partType = equipTemplate.partType
        infoDefault.suitId = equipTemplate.suitID
        infoDefault.equipData = equipTemplate
    end

    if instId then
        local equipInst = CharInfoUtils.getEquipByInstId(instId)
        infoDefault.num_canEquip = 0
        if equipInst then
            local maxWearLimit = extraArgs and extraArgs.maxWearLimit or 0
            local canEquip = maxWearLimit == nil or infoDefault.rarity <= maxWearLimit
            infoDefault.num_canEquip = canEquip and 1 or 0
            infoDefault.equipEnhanceLevel = equipInst:GetEnhanceLevel()
            infoDefault.equippedCharInstId = equipInst.equippedCharServerId
        end
    end

    return infoDefault
end




function FilterUtils.processEquipProduce(equipFormulaData)
    local infoDefault = FilterUtils.processEquip(equipFormulaData.outcomeEquipId)
    if not infoDefault then
        return nil
    end
    infoDefault.isUnlocked = FactoryUtils.isEquipFormulaUnlocked(equipFormulaData.formulaId)
    infoDefault.equipFormulaData = equipFormulaData

    local costItemId = EquipTechUtils.GetDefaultCostMaterial(equipFormulaData.formulaId)
    local hasValue, curMaterialInfo = Tables.EquipCostMaterialTable:TryGetValue(costItemId)
    if curMaterialInfo then
        infoDefault.costItemSortId = curMaterialInfo.sortId
    else
        infoDefault.costItemSortId = 0
    end

    return infoDefault
end





function FilterUtils.processEquipEnhance(templateId, instId)
    local infoDefault = FilterUtils.processEquip(templateId, instId)
    if not infoDefault then
        return nil
    end
    infoDefault.equipInstData = CharInfoUtils.getEquipByInstId(instId)
    infoDefault.canEnhance = infoDefault.equipInstData:IsMaxEnhanced() and 0 or 1
    infoDefault.equipEnhanceLevel = infoDefault.equipInstData:IsMaxEnhanced() and -1 or infoDefault.equipInstData:GetEnhanceLevel()
    infoDefault.equipEnhanceTotalFailedTimes = infoDefault.equipInstData:GetTotalFailedTimes()
    if infoDefault.equippedCharInstId and infoDefault.equippedCharInstId > 0 and FilterUtils.charInstId2Index then
        local index = FilterUtils.charInstId2Index[infoDefault.equippedCharInstId]
        infoDefault.equippedCharIndex = index and -index or -math.huge
        infoDefault.partTypeReverseNum = -infoDefault.partType:ToInt()
    end
    return infoDefault
end






function FilterUtils.processEquipEnhanceMaterial(templateId, instId, extraArgs)
    local infoDefault = FilterUtils.processEquip(templateId, instId)
    if not infoDefault then
        return nil
    end
    infoDefault.equipInstData = CharInfoUtils.getEquipByInstId(instId)
    infoDefault.equipEnhanceSuccessProb = EquipTechUtils.getEquipEnhanceSuccessProbability(infoDefault.equipInstData, extraArgs.attrShowInfo)
    infoDefault.equipEnhanceLevelReverse = -infoDefault.equipInstData:GetEnhanceLevel()
    infoDefault.equipEnhanceTotalFailedTimesReverse = -infoDefault.equipInstData:GetTotalFailedTimes()
    infoDefault.equippedCharInstIdReverse = -infoDefault.equippedCharInstId
    return infoDefault
end






function FilterUtils.generateConfig_EQUIP_ENHANCE()
    local filterConfig = {}
    table.insert(filterConfig, FilterUtils._generateEquipMainAttrFilterGroup())
    table.insert(filterConfig, FilterUtils._generateEquipExtraAttrFilterGroup())
    local suitFilterConfigs = FilterUtils._generateEquipSuitFilterGroup()
    table.insert(filterConfig, suitFilterConfigs)
    table.insert(filterConfig, FilterUtils._generateDomainFilterGroup())
    return filterConfig
end


function FilterUtils.generateConfig_EQUIP_ENHANCE_MATERIALS()
    local filterConfig = {}
    table.insert(filterConfig, FilterUtils._generateEquipMainAttrFilterGroup())
    table.insert(filterConfig, FilterUtils._generateEquipExtraAttrFilterGroup())
    table.insert(filterConfig, FilterUtils._generateDomainFilterGroup())
    return filterConfig
end

function FilterUtils._generateCharRelationLevelFilterGroup()
    local tags = {}
    for level, cfg in pairs(Tables.spaceshipCharRelationLevelTable) do
        local tag = {
            groupType = "RelationLevel",
            name = cfg.favorDesc,
            funcName = "_filterByRelation",
            param = cfg.relationLevel,
        }
        table.insert(tags, tag)
    end
    table.sort(tags, Utils.genSortFunction({ "param" }, true))
    local suitFilterConfigs = {
        title = Language["ui_char_info_controller_friendship"],
        tags = tags,
    }
    return suitFilterConfigs
end

function FilterUtils._filterByRelation(info, level)
    return (CSPlayerDataUtil.GetFriendshipLevelByChar(info.templateId) or 0) == level
end

function FilterUtils._filterByPosterRarity(info, rarity)
    return info.charRarity == rarity
end


function FilterUtils._filterByPosterPotentialLevel(info, photoLevel)
    return info.photoLevel == photoLevel
end

function FilterUtils.generateConfig_POSTER_PICTURE()
    local POSTER_PICTURE = {}
    local RARITY = {
        title = Language.LUA_ITEM_FILTER_GROUP_TITLE_RARITY,
        tags = {
            {
                groupType = "CharRarity",
                name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 4),
                funcName = "_filterByPosterRarity",
                param = 4,
            },
            {
                groupType = "CharRarity",
                name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 5),
                funcName = "_filterByPosterRarity",
                param = 5,
            },
            {
                groupType = "CharRarity",
                name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 6),
                funcName = "_filterByPosterRarity",
                param = 6,
            },
        }
    }
    local LEVEL = {
        title = Language.LUA_POTENTIAL_LEVEL,
        tags = {
            {
                groupType = "PotentialLevel",
                name = string.format(Language.LUA_FILTER_OPTION_POTENTIAL_LEVEL, 1),
                funcName = "_filterByPosterPotentialLevel",
                param = 1,
            },
            {
                groupType = "PotentialLevel",
                name = string.format(Language.LUA_FILTER_OPTION_POTENTIAL_LEVEL, 3),
                funcName = "_filterByPosterPotentialLevel",
                param = 3,
            },
            {
                groupType = "PotentialLevel",
                name = string.format(Language.LUA_FILTER_OPTION_POTENTIAL_LEVEL, 5),
                funcName = "_filterByPosterPotentialLevel",
                param = 5,
            },

        }
    }
    table.insert(POSTER_PICTURE, RARITY)
    table.insert(POSTER_PICTURE, LEVEL)
    return POSTER_PICTURE
end

function FilterUtils.generateConfig_DEPOT_CHAR()
    local DEPOT_CHAR = {}
    table.insert(DEPOT_CHAR, FilterUtils.generateConfigCharRarity())
    table.insert(DEPOT_CHAR, FilterUtils._generateCharRelationLevelFilterGroup())
    return DEPOT_CHAR
end

function FilterUtils.generateConfigCharRarity()
    local RARITY = {
        title = Language.LUA_ITEM_FILTER_GROUP_TITLE_RARITY,
        tags = {
            {
                groupType = "CharRarity",
                name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 4),
                funcName = "_filterByRarity",
                param = 4,
            },
            {
                groupType = "CharRarity",
                name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 5),
                funcName = "_filterByRarity",
                param = 5,
            },
            {
                groupType = "CharRarity",
                name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 6),
                funcName = "_filterByRarity",
                param = 6,
            },
        }
    }
    return RARITY
end

function FilterUtils.generateConfigNpcHasGift()
    local dailyRemain = {
        title = Language.LUA_SPACESHIP_SUMMON_GIFT_TITLE,
        tags = {
            {
                groupType = "NpcGiftRemain",
                name = Language.LUA_SPACESHIP_SUMMON_FILTER_HAVE_GIFT,
                funcName = "_filterNpcGiftRemain",
                param = 1,
            },
        }
    }
    return dailyRemain
end

function FilterUtils.generateConfigCharSummon()
    local DEPOT_CHAR = {}
    table.insert(DEPOT_CHAR, FilterUtils.generateConfigCharRarity())
    table.insert(DEPOT_CHAR, FilterUtils._generateCharRelationLevelFilterGroup())
    table.insert(DEPOT_CHAR, FilterUtils.generateConfigNpcHasGift())
    return DEPOT_CHAR
end

function FilterUtils.generateConfig_DEPOT_WEAPON()
    local DEPOT_WEAPON = {
        {
            title = Language.LUA_WIKI_FILTER_GROUP_NAME_WEAPON_TYPE,
            tags = {
                {
                    groupType = "WeaponType",
                    name = Language[string.format("LUA_WEAPON_TYPE_%d", GEnums.WeaponType.Sword:ToInt())],
                    funcName = "_filterByWeaponType",
                    param = GEnums.WeaponType.Sword,
                },
                {
                    groupType = "WeaponType",
                    name = Language[string.format("LUA_WEAPON_TYPE_%d", GEnums.WeaponType.Wand:ToInt())],
                    funcName = "_filterByWeaponType",
                    param = GEnums.WeaponType.Wand,
                },
                {
                    groupType = "WeaponType",
                    name = Language[string.format("LUA_WEAPON_TYPE_%d", GEnums.WeaponType.Claymores:ToInt())],
                    funcName = "_filterByWeaponType",
                    param = GEnums.WeaponType.Claymores,
                },
                {
                    groupType = "WeaponType",
                    name = Language[string.format("LUA_WEAPON_TYPE_%d", GEnums.WeaponType.Lance:ToInt())],
                    funcName = "_filterByWeaponType",
                    param = GEnums.WeaponType.Lance,
                },
                {
                    groupType = "WeaponType",
                    name = Language[string.format("LUA_WEAPON_TYPE_%d", GEnums.WeaponType.Pistol:ToInt())],
                    funcName = "_filterByWeaponType",
                    param = GEnums.WeaponType.Pistol,
                },
            }
        },
        {
            title = Language.LUA_ITEM_FILTER_GROUP_TITLE_RARITY,
            tags = {
                {
                    groupType = "WeaponRarity",
                    name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 3),
                    funcName = "_filterByRarity",
                    param = 3,
                },
                {
                    groupType = "WeaponRarity",
                    name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 4),
                    funcName = "_filterByRarity",
                    param = 4,
                },
                {
                    groupType = "WeaponRarity",
                    name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 5),
                    funcName = "_filterByRarity",
                    param = 5,
                },
                {
                    groupType = "WeaponRarity",
                    name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 6),
                    funcName = "_filterByRarity",
                    param = 6,
                },
            }
        },
    }
    return DEPOT_WEAPON
end

function FilterUtils.RemoveFilterTag(tagGroups, groupType, param)
    for _, group in ipairs(tagGroups or {}) do
        for index = #(group.tags or {}), 1, -1 do
            local tag = group.tags[index]
            if tag.groupType == groupType and tag.param == param then
                table.remove(group.tags, index)
            end
        end
    end
end

function FilterUtils.generateConfig_DEPOT_WEAPON_DESTROY()
    local filterConfigs = FilterUtils.generateConfig_DEPOT_WEAPON()

    FilterUtils.RemoveFilterTag(filterConfigs, "WeaponRarity", 6)
    return filterConfigs
end

function FilterUtils.generateConfig_CHAR_INFO_WEAPON()
    local FILTER_CHAR_INFO_WEAPON = {
        {
            title = Language.LUA_ITEM_FILTER_GROUP_TITLE_RARITY,
            tags = {
                {
                    groupType = "WeaponRarity",
                    name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 3),
                    funcName = "_filterByRarity",
                    param = 3,
                },
                {
                    groupType = "WeaponRarity",
                    name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 4),
                    funcName = "_filterByRarity",
                    param = 4,
                },
                {
                    groupType = "WeaponRarity",
                    name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 5),
                    funcName = "_filterByRarity",
                    param = 5,
                },
                {
                    groupType = "WeaponRarity",
                    name = string.format(Language.LUA_DEPOT_FILTER_OPTION_RARITY, 6),
                    funcName = "_filterByRarity",
                    param = 6,
                },
            }
        }
    }
    return FILTER_CHAR_INFO_WEAPON
end

function FilterUtils.generateConfig_DEPOT_GEM()
    return FilterUtils._generateGemSkillFilterGroups()
end



function FilterUtils.generateConfig_DEPOT_EQUIP()

    local filterConfig = {}
    table.insert(filterConfig, FilterUtils._generateEquipMainAttrFilterGroup())
    table.insert(filterConfig, FilterUtils._generateEquipExtraAttrFilterGroup())
    table.insert(filterConfig, FilterUtils._generateEquipPartTypeFilterGroup())
    table.insert(filterConfig, FilterUtils._generateEquipSuitFilterGroup())
    table.insert(filterConfig, FilterUtils._generateDomainFilterGroup())
    return filterConfig
end

function FilterUtils.generateConfig_WEAPON_EXHIBIT_GEM(selectedTermIdMap)
    return FilterUtils._generateGemSkillFilterGroups(selectedTermIdMap)
end

function FilterUtils.generateConfig_DEPOT_EQUIP_DESTROY()
    local filterConfig = {}
    table.insert(filterConfig, FilterUtils._generateEquipRarityFilterGroup(1, 5))
    local unlockFilterGroup =
    {
        title = Language.LUA_ITEM_FILTER_GROUP_UNLOCK,
        tags = {
            {
                groupType = "EquipUnlock",
                name = Language.LUA_DEPOT_FILTER_OPTION_UNLOCK,
                funcName = "_filterByUnlock",
                param = true,
            },
        }
    }
    table.insert(filterConfig, unlockFilterGroup)
    return filterConfig
end

function FilterUtils.generateConfig_DEPOT_GEM_DESTROY()
    local DEPOT_GEM_DESTROY = {
        {
            title = Language.LUA_ITEM_FILTER_GROUP_TITLE_RARITY,
            tags = {
                {
                    groupType = "Rarity",
                    name = Tables.itemTable["item_gem_rarity_2"].name,
                    funcName = "_filterByRarity",
                    param = 2,
                },
                {
                    groupType = "Rarity",
                    name = Tables.itemTable["item_gem_rarity_3"].name,
                    funcName = "_filterByRarity",
                    param = 3,
                },
                {
                    groupType = "Rarity",
                    name = Tables.itemTable["item_gem_rarity_4"].name,
                    funcName = "_filterByRarity",
                    param = 4,
                },
            }
        },
        {
            title = Language.LUA_ITEM_FILTER_GROUP_UNLOCK,
            tags = {
                {
                    groupType = "GemUnlock",
                    name = Language.LUA_DEPOT_FILTER_OPTION_UNLOCK,
                    funcName = "_filterByUnlock",
                    param = true,
                },
                {
                    groupType = "GemUnlock",
                    name = Language.LUA_DEPOT_FILTER_OPTION_TRASH,
                    funcName = "_filterByTrash",
                    param = true,
                },
            }
        },
    }
    return DEPOT_GEM_DESTROY
end


function FilterUtils.generateConfig_EQUIP_PRODUCE()
    local filterConfig = {}
    table.insert(filterConfig, FilterUtils._generateEquipMainAttrFilterGroup())
    table.insert(filterConfig, FilterUtils._generateEquipExtraAttrFilterGroup())
    table.insert(filterConfig, FilterUtils._generateEquipSuitFilterGroup())
    table.insert(filterConfig, FilterUtils._generateEquipPartTypeFilterGroup())
    table.insert(filterConfig, FilterUtils._generateEquipCurFormulaFilterGroup())
    table.insert(filterConfig, FilterUtils._generateEquipDefaultFormulaFilterGroup())

    return filterConfig
end







function FilterUtils._generateEquipRarityFilterGroup(minRarity, maxRarity)

    local tags = {}
    for i = minRarity, maxRarity do

        local tag = {
            groupType = "ItemRarity",
            name = Language[string.format("LUA_EQUIP_FILTER_NAME_RARITY_%d", i)],
            funcName = "_filterByRarity",
            param = i,
        }
        table.insert(tags, tag)
    end


    local tagGroup = {
        title = Language.LUA_EQUIP_FILTER_GROUP_TITLE_RARITY,
        tags = tags,
    }

    return tagGroup
end

function FilterUtils._generateDomainFilterGroup()
    local tags = {}
    for _, domainCfg in pairs(Tables.domainDataTable) do

        local tag = {
            groupType = "Domain",
            name = domainCfg.domainName,
            funcName = "_filterByDomain",
            param = domainCfg.domainId,
        }
        table.insert(tags, tag)
    end


    local tagGroup = {
        title = Language.LUA_FILTER_GROUP_TITLE_DOMAIN,
        tags = tags,
    }

    return tagGroup
end



function FilterUtils._generateEquipCurFormulaFilterGroup()
    local tbHighLevelMaterialList = EquipTechUtils.GetCostMaterialList()
    local group = {
        title = Language.LUA_EQUIP_PRODUCE_FILTER_GROUP_CUR_FORMULA,
        tags = {},
    }

    for _, materialInfo in ipairs(tbHighLevelMaterialList) do
        table.insert(group.tags, {
            groupType = "EquipCurFormula",
            name = materialInfo.name,
            funcName = "_filterByEquipCurFormula",
            param = materialInfo.itemId,
        })
    end

    return group
end



function FilterUtils._generateEquipDefaultFormulaFilterGroup()
    local tbHighLevelMaterialList = EquipTechUtils.GetCostMaterialList()
    local group = {
        title = Language.LUA_EQUIP_PRODUCE_FILTER_GROUP_DEFAULT_FORMULA,
        tags = {},
    }

    for _, materialInfo in ipairs(tbHighLevelMaterialList) do
        table.insert(group.tags, {
            groupType = "EquipDefaultFormula",
            name = materialInfo.name,
            funcName = "_filterByEquipDefaultFormula",
            param = materialInfo.itemId,
        })
    end

    return group
end



function FilterUtils._generateEquipPartTypeFilterGroup()
    local group = {
        title = Language.LUA_DEPOT_FILTER_GROUP_TITLE_EQUIP_TYPE,
        tags = {
            {
                groupType = "EquipType",
                name = Language.LUA_WIKI_FILTER_NAME_EQUIP_PART_BODY,
                funcName = "_filterByPartType",
                param = GEnums.PartType.Body,
            },
            {
                groupType = "EquipType",
                name = Language.LUA_WIKI_FILTER_NAME_EQUIP_PART_HAND,
                funcName = "_filterByPartType",
                param = GEnums.PartType.Hand,
            },
            {
                groupType = "EquipType",
                name = Language.LUA_WIKI_FILTER_NAME_EQUIP_PART_EDC,
                funcName = "_filterByPartType",
                param = GEnums.PartType.EDC,
            },
        }
    }
    return group
end



function FilterUtils._generateEquipMainAttrFilterGroup()
    return FilterUtils._generateAttrTypeFilterGroup(
        "EquipMainAttrType",
        FilterUtils.getEquipMainAttrFilterList(), nil, CALC_TYPE.AND)
end



function FilterUtils._generateEquipExtraAttrFilterGroup()
    return FilterUtils._generateAttrTypeFilterGroup(
        "EquipExtraAttrType",
        FilterUtils.getEquipExtraAttrFilterList(),
        Language.LUA_DEPOT_FILTER_GROUP_TITLE_EQUIP_EXTRA_ATTR_TYPE)
end












function FilterUtils._generateAttrTypeFilterGroup(groupType, attrFilterList, tagGroupTitle, calcType)

    local tags = {}
    for _, attrFilterData in pairs(attrFilterList) do
        local isCompositeAttr = type(attrFilterData.attrKey) == "string"
        local extraCfg = { attrModifier = attrFilterData.modifier, ignoreAttrModifier = attrFilterData.ignoreModifier}

        local attrShowCfg = isCompositeAttr and
            AttributeUtils.getCompositeAttributeShowCfg(attrFilterData.attrKey, extraCfg) or
            AttributeUtils.getAttributeShowCfg(attrFilterData.attrKey, extraCfg)
        if attrShowCfg then

            local tag = {
                groupType = groupType,
                name = attrShowCfg.name,
                funcName = "_filterByAttrType",
                param = attrFilterData,
                calcType = calcType
            }
            table.insert(tags, tag)
        end
    end

    tagGroupTitle = tagGroupTitle or Language.LUA_DEPOT_FILTER_GROUP_TITLE_EQUIP_ATTR_TYPE


    local tagGroup = {
        title = tagGroupTitle,
        tags = tags,
    }

    return tagGroup
end



function FilterUtils._generateEquipSuitFilterGroup()
    local tags = {}
    for _, suitCfg in pairs(Tables.equipSuitTable) do
        local list = suitCfg.list
        local suitPieceCfg = list[CSIndex(1)]
        if suitPieceCfg then
            local tag = {
                groupType = "Suit",
                name = suitPieceCfg.suitName,
                funcName = "_filterByEquipSuit",
                param = suitPieceCfg.suitID,
            }
            table.insert(tags, tag)
        end
    end
    table.insert(tags, {
        groupType = "Suit",
        name = Language.LUA_DEPOT_FILTER_OPTION_NO_SUIT,
        funcName = "_filterByEquipSuit",
        param = "",
    })
    table.sort(tags, Utils.genSortFunction({ "tag" }))

    local suitFilterConfigs = {
        title = Language.LUA_DEPOT_FILTER_GROUP_TITLE_EQUIP_SUIT,
        tags = tags,
    }

    return suitFilterConfigs
end




function FilterUtils._generateGemSkillFilterGroups(selectedTermIdMap)

    local GemTermType = CS.Beyond.GEnums.GemTermType

    local termType2TagGroup = {
        [GemTermType.PrimAttrTerm] = {
            title = Language.LUA_FILTER_GROUP_TITLE_GEM_PRIM_ATTR,
            tags = {},
        },
        [GemTermType.SecAttrTerm] = {
            title = Language.LUA_FILTER_GROUP_TITLE_GEM_SEC_ATTR,
            tags = {},
        },
        [GemTermType.SkillTerm] = {
            title = Language.LUA_FILTER_GROUP_TITLE_GEM_SKILL,
            tags = {},
        },
    }
    local selectedFilterTags = {}
    for _, gemTermCfg in pairs(Tables.gemTable) do
        local tag = {
            groupType = gemTermCfg.termType:ToString(),
            name = gemTermCfg.tagName,
            sortOrder = gemTermCfg.sortOrder,
            funcName = "_filterByGemSkill",
            param = gemTermCfg.gemTermId,
            calcType = CALC_TYPE.OR,
        }
        local tagGroup = termType2TagGroup[gemTermCfg.termType]
        if tagGroup then
            table.insert(tagGroup.tags, tag)

            if selectedTermIdMap and selectedTermIdMap[gemTermCfg.gemTermId] then
                table.insert(selectedFilterTags, tag)
            end
        end
    end

    local filterConfigs = {}
    local termTypeOrder = {
        [1] = GemTermType.PrimAttrTerm,
        [2] = GemTermType.SecAttrTerm,
        [3] = GemTermType.SkillTerm,
    }
    for _, gemTermType in ipairs(termTypeOrder) do
        local tagGroup = termType2TagGroup[gemTermType]
        if tagGroup and #tagGroup.tags > 0 then
            table.sort(tagGroup.tags, function(a, b)
                return a.sortOrder < b.sortOrder
            end)
            table.insert(filterConfigs, tagGroup)
        end
    end
    return filterConfigs, selectedFilterTags
end











function FilterUtils.isSameTagInfo(leftTag, rightTag)
    local function isSameValue(left, right)
        if left == nil and right ~= nil then
            return false
        end
        if left ~= nil and right == nil then
            return false
        end
        if type(left) ~= type(right) then
            return false
        end
        if type(left) ~= "table" then
            return left == right
        end
        for k, v in pairs(left) do
            if not isSameValue(v, right[k]) then
                return false
            end
        end
        for k, v in pairs(right) do
            if not isSameValue(v, left[k]) then
                return false
            end
        end
        return true
    end

    return isSameValue(leftTag, rightTag)
end














function FilterUtils.checkIfPassFilter(itemInfo, filterConfigs)
    local filterGroups = {}

    for _, filterConfig in pairs(filterConfigs) do
        local groupType = filterConfig.groupType or "Default"
        local calcType = filterConfig.calcType or CALC_TYPE.OR
        if not filterGroups[groupType] then
            filterGroups[groupType] = {
                isPass = false,
                calcType = calcType,
                filters = {},
            }
        end
        table.insert(filterGroups[groupType].filters, filterConfig)
    end

    for _, filterGroup in pairs(filterGroups) do
        filterGroup.isPass = FilterUtils._checkIfGroupPassFilter(itemInfo, filterGroup)
    end


    for _, filterGroup in pairs(filterGroups) do
        if not filterGroup.isPass then
            return false
        end
    end

    return true
end





function FilterUtils._checkIfGroupPassFilter(itemInfo, filterGroup)
    local calcType = filterGroup.calcType
    for _, filterConfig in pairs(filterGroup.filters) do
        local funcName = filterConfig.funcName
        local filterFunc = FilterUtils[funcName]
        local isPass = filterFunc(itemInfo, filterConfig.param)

        if calcType == CALC_TYPE.AND then
            if not isPass then
                return false
            end
        else
            if isPass then
                return true
            end
        end
    end

    if calcType == CALC_TYPE.AND then
        return true
    else
        return false
    end
end









function FilterUtils._filterByDomain(info, domainId)
    return info.equipData.domainId == domainId
end





function FilterUtils._filterByAttrType(info, attrFilterData)
    local isCompositeAttr = type(attrFilterData.attrKey) == "string"
    local maxAttrIndex = attrFilterData.maxAttrIndex
    local minAttrIndex = attrFilterData.minAttrIndex
    local attrModifiers = info.equipData.displayAttrModifiers
    for i = 0, attrModifiers.Count - 1 do
        local attrModifier = attrModifiers[i]
        if (maxAttrIndex == nil or attrModifier.attrIndex <= maxAttrIndex) and
           (minAttrIndex == nil or attrModifier.attrIndex >= minAttrIndex) then
            local isModifierMatch = attrFilterData.modifier == nil or attrModifier.modifierType == attrFilterData.modifier
            local isAttrKeyMatch = (isCompositeAttr and attrModifier.compositeAttr == attrFilterData.attrKey) or
                                    (not isCompositeAttr and attrModifier.attrType == attrFilterData.attrKey)
            if isModifierMatch and isAttrKeyMatch then
                return true
            end
        end
    end
    return false
end

function FilterUtils._filterByWeaponType(info, weaponType)
    if info.weaponType == GEnums.WeaponType.All then
        return true
    end

    return info.weaponType == weaponType
end

function FilterUtils._filterByRarity(info, rarity)
    return info.rarity == rarity
end

function FilterUtils._filterNpcGiftRemain(info, GiftValue)
    return info.hasGift == GiftValue
end

function FilterUtils._filterByEnhanceEquip(info, wantEnhanceEquip)
    return false
end

function FilterUtils._filterByUnlock(info, unlock)
    if not info.instId or info.instId <= 0 then
        return true
    end

    return unlock == not GameInstance.player.inventory:IsItemLocked(Utils.getCurrentScope(), info.id, info.instId)
end

function FilterUtils._filterByTrash(info, trash)
    if not info.instId or info.instId <= 0 then
        return false
    end

    return trash == GameInstance.player.inventory:IsItemTrash(Utils.getCurrentScope(), info.id, info.instId)
end

function FilterUtils._filterByPartType(info, equipType)
    return info.partType == equipType
end

function FilterUtils._filterByEquipSuit(info, suitId)
    return info.suitId == suitId
end

function FilterUtils._filterByEquipCurFormula(info, materialItemId)
    local formulaData = info.equipFormulaData
    local formulaId = formulaData and formulaData.formulaId or nil
    if not formulaId then
        return false
    end

    local itemId = EquipTechUtils.GetCurCostMaterial(formulaId)
    return itemId == materialItemId
end

function FilterUtils._filterByEquipDefaultFormula(info, materialItemId)
    local formulaData = info.equipFormulaData
    local formulaId = formulaData and formulaData.formulaId or nil
    if not formulaId then
        return false
    end

    local itemId = EquipTechUtils.GetDefaultCostMaterial(formulaId)
    return itemId == materialItemId
end

function FilterUtils._filterByGemSkill(info, gemTermId)
    local skillMap = info.skillMap
    if not skillMap then
        return false
    end

    for termId, _ in pairs(skillMap) do
        if termId == gemTermId then
            return true
        end
    end

    return false
end

function FilterUtils._filterByGemEnableOnWeapon(info, param)
    if param == nil then
        param = false
    end
    return info.enableOnWeapon == param
end





function FilterUtils.generateConfig_TACTICAL_ITEM()
    local TACTICAL_ITEM_FILTER_INFO = {
        {
            title = Language.LUA_TACTICAL_ITEM_FILTER_GROUP_NAME_EFFECT,
            tags = {
                {
                    name = Language.LUA_TACTICAL_ITEM_FILTER_NAME_EFFECT_HEAL,
                    groupType = "TacticalItemEffect",
                    funcName = "_filterByTacticalItemEffect",
                    param = GEnums.ItemUseEffectType.Heal,
                },
                {
                    name = Language.LUA_TACTICAL_ITEM_FILTER_NAME_EFFECT_REVIVE,
                    groupType = "TacticalItemEffect",
                    funcName = "_filterByTacticalItemEffect",
                    param = GEnums.ItemUseEffectType.Revive,
                },
                {
                    name = Language.LUA_TACTICAL_ITEM_FILTER_NAME_EFFECT_BUFF,
                    groupType = "TacticalItemEffect",
                    funcName = "_filterByTacticalItemEffect",
                    param = GEnums.ItemUseEffectType.Buff,
                },
            }
        },
        {
            title = Language.LUA_TACTICAL_ITEM_FILTER_GROUP_NAME_TARGET_NUM,
            tags = {
                {
                    name = Language.LUA_TACTICAL_ITEM_FILTER_NAME_TARGET_NUM_SINGLE,
                    groupType = "TacticalItemTargetNum",
                    funcName = "_filterByTacticalItemTargetNum",
                    param = GEnums.ItemUseTargetNumType.Single,
                },
                {
                    name = Language.LUA_TACTICAL_ITEM_FILTER_NAME_TARGET_NUM_ALL,
                    groupType = "TacticalItemTargetNum",
                    funcName = "_filterByTacticalItemTargetNum",
                    param = GEnums.ItemUseTargetNumType.All,
                },
            }
        },
    }
    return TACTICAL_ITEM_FILTER_INFO
end

function FilterUtils.processTacticalItem(itemId, instId)
    local info = FilterUtils.processItemDefault(itemId, instId)
    local useItemData = Tables.useItemTable:GetValue(itemId)
    info.effectType = useItemData.effectType
    info.targetNumType = useItemData.targetNumType
    info.curCount = Utils.getBagItemCount(itemId)
    return info
end

function FilterUtils._filterByTacticalItemEffect(info, effectType)
    return info.effectType == effectType
end

function FilterUtils._filterByTacticalItemTargetNum(info, targetNumType)
    return info.targetNumType == targetNumType
end




function FilterUtils.generateConfig_ACHIEVEMENT_MEDAL()
    local filterConfigs = {}
    local levelNameConfigs = {
        [1] = 'ui_achv_edit_filtertag_level1',
        [2] = 'ui_achv_edit_filtertag_level2',
        [3] = 'ui_achv_edit_filtertag_level3',
    }
    local levelTags = {}
    for level, nameConfig in ipairs(levelNameConfigs) do
        table.insert(levelTags, {
            groupType = "AchievementLevel",
            name = I18nUtils.GetText(nameConfig),
            funcName = '_filterByAchievementLevel',
            param = level,
        })
    end
    local plateTags = {
        {
            groupType = "AchievementPlate",
            name = I18nUtils.GetText('ui_achv_edit_filtertag_plating'),
            funcName = '_filterByAchievementPlate',
            param = true,
        },
        {
            groupType = "AchievementPlate",
            name = I18nUtils.GetText('ui_achv_edit_filtertag_noplating'),
            funcName = '_filterByAchievementPlate',
            param = false,
        }
    }
    local rareTags = {
        {
            groupType = "AchievementRare",
            name = I18nUtils.GetText('ui_achv_edit_filtertag_special'),
            funcName = '_filterByAchievementRare',
            param = true,
        },
        {
            groupType = "AchievementRare",
            name = I18nUtils.GetText('ui_achv_edit_filtertag_nospecial'),
            funcName = '_filterByAchievementRare',
            param = false,
        }
    }
    table.insert(filterConfigs, {
        title = I18nUtils.GetText('ui_achv_edit_filtertag_title_level'),
        tags = levelTags,
    })
    table.insert(filterConfigs, {
        title = I18nUtils.GetText('ui_achv_edit_filtertag_plating_title'),
        tags = plateTags,
    })
    table.insert(filterConfigs, {
        title = I18nUtils.GetText('ui_achv_edit_filtertag_special_title'),
        tags = rareTags,
    })
    return filterConfigs
end

function FilterUtils._filterByAchievementLevel(info, param)
    local playerInfo = info.achievementPlayerInfo
    local playerLevel = (playerInfo == nil) and 0 or playerInfo.level
    return playerLevel == param
end

function FilterUtils._filterByAchievementPlate(info, param)
    local playerInfo = info.achievementPlayerInfo
    local playerPlated = (playerInfo ~= nil) and playerInfo.isPlated or false
    return playerPlated == param
end

function FilterUtils._filterByAchievementRare(info, param)
    local playerInfo = info.achievementPlayerInfo
    local playerLevel = (playerInfo == nil) and 0 or playerInfo.level
    local achievementData = info.achievementData
    local isRare = achievementData.applyRareEffect and playerLevel >= Tables.achievementConst.levelDisplayEffect
    return isRare == param
end





local equipExtraAttrFilterList = nil



function FilterUtils.getEquipExtraAttrFilterList()
    if equipExtraAttrFilterList then
        return equipExtraAttrFilterList
    end
    equipExtraAttrFilterList = {}
    local attrKeyTable = {}
    local _, attrFilterList = Tables.attributeFilterTable:TryGetValue("equipExtraAttr")
    if attrFilterList then
        for i = 0, attrFilterList.list.Count - 1 do
            local filterData = attrFilterList.list[i]
            local attrKey = not string.isEmpty(filterData.compositeAttr) and filterData.compositeAttr or filterData.attributeType
            if not attrKeyTable[attrKey] then
                attrKeyTable[attrKey] = true
                table.insert(equipExtraAttrFilterList, { attrKey = attrKey, ignoreModifier = true, minAttrIndex = 3 })
            end
        end
    end
    return equipExtraAttrFilterList
end


local equipMainAttrFilterList = nil



function FilterUtils.getEquipMainAttrFilterList()
    if not equipMainAttrFilterList then
        equipMainAttrFilterList = {
            { attrKey = GEnums.AttributeType.Str, ignoreModifier = true, maxAttrIndex = 2 },
            { attrKey = GEnums.AttributeType.Agi, ignoreModifier = true, maxAttrIndex = 2 },
            { attrKey = GEnums.AttributeType.Wisd, ignoreModifier = true, maxAttrIndex = 2 },
            { attrKey = GEnums.AttributeType.Will, ignoreModifier = true, maxAttrIndex = 2 },
            { attrKey = "Main", ignoreModifier = true, maxAttrIndex = 2 },
            { attrKey = "Sub", ignoreModifier = true, maxAttrIndex = 2 },
        }
    end
    return equipMainAttrFilterList
end





FilterUtils.charInstId2Index = nil
FilterUtils.isCharInstIdIndexDirty = true

function FilterUtils.updateCharInstIdIndex()
    if not FilterUtils.isCharInstIdIndexDirty then
        return
    end
    FilterUtils.charInstId2Index = {}
    local charInfoList = CharInfoUtils.getCharInfoList()
    table.sort(charInfoList, Utils.genSortFunction(UIConst.CHAR_FORMATION_LIST_SORT_OPTION[1].reverseKeys))
    for index, charInfo in ipairs(charInfoList) do
        FilterUtils.charInstId2Index[charInfo.instId] = index
    end
    FilterUtils.isCharInstIdIndexDirty = false
end

function FilterUtils.setCharInstIdIndexDirty()
    FilterUtils.isCharInstIdIndexDirty = true
end




_G.FilterUtils = FilterUtils
return FilterUtils