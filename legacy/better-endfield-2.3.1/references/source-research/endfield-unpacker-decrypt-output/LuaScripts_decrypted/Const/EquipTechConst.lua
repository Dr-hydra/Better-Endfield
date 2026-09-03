local SORT_TYPE = {
    MATERIAL = CS.Beyond.Gameplay.EquipTechSystem.EPanelSortType.Material:GetHashCode(),
    EQUIP_PART = CS.Beyond.Gameplay.EquipTechSystem.EPanelSortType.EquipPart:GetHashCode(),
    EARLY_ACCEPT = CS.Beyond.Gameplay.EquipTechSystem.EPanelSortType.EarlyAccept:GetHashCode(),
}

local EquipTechConst = {


    EEquipEnhanceSuccessProb = {
        None = 1,
        Normal = 2,
        High = 3,
    },

    PANL_SORT_TYPE = SORT_TYPE,

    EQUIP_PRODUCE_PACK_SORT_CONFIG = {
        [SORT_TYPE.MATERIAL] = {
            name = Language.LUA_EQUIP_PRODUCE_PACK_SORT_OPTION_MATERIAL,
            keys = { "costMatSortId", "sortId" },
            innerKeys = { "costItemSortId", "minWearLv", "rarity", "sortId1", "sortId2", "id" },
        },
        [SORT_TYPE.EQUIP_PART] = {
            name = Language.LUA_EQUIP_PRODUCE_PACK_SORT_OPTION_EQUIP_PART,
            keys = { "costMatSortId", "sortId" },
            innerKeys = { "minWearLv", "rarity", "sortId1", "sortId2", "id" },
        },
        [SORT_TYPE.EARLY_ACCEPT] = {
            name = Language.LUA_EQUIP_PRODUCE_PACK_SORT_OPTION_EARLY_ACCEPT,
            keys = { "generalPackSortId", "costMatSortId", "sortId" },
            innerKeys = { "costItemSortId", "minWearLv", "rarity", "sortId1", "sortId2", "id" },
        }
    },

    EQUIP_ENHANCE_SORT_OPTION = {
        {
            name = Language.LUA_EQUIP_ENHANCE_SORT_OPTION_CHAR,
            keys = { "canEnhance", "equippedCharIndex", "partTypeReverseNum", "equipEnhanceLevel", "equipEnhanceTotalFailedTimes", "sortId1", "sortId2", "id", "instId" }
        },
        {
            name = Language.LUA_DEPOT_SORT_OPTION_DEFAULT,
            keys = { "equipEnhanceLevel", "equipEnhanceTotalFailedTimes", "equippedCharInstId", "sortId1", "sortId2", "id", "instId" }
        },
    },

    EQUIP_ENHANCE_MATERIALS_SORT_OPTION = {
        {
            name = Language.LUA_DEPOT_SORT_OPTION_DEFAULT,
            keys = { "equipEnhanceSuccessProb", "equipEnhanceLevelReverse", "equipEnhanceTotalFailedTimesReverse", "equippedCharInstIdReverse", "sortId1", "sortId2", "id", "instId" }
        },
    },

    EQUIP_PRODUCE_PACK_RED_DOT_TYPE = {
        AllNew = 103,
        PartialNew = 104,
    }
}

EquipTechConst.EQUIP_PRODUCE_PACK_SORT_OPTION = (function()
    local options = {}
    for _, cfg in ipairs(EquipTechConst.EQUIP_PRODUCE_PACK_SORT_CONFIG) do
        options[#options + 1] = { name = cfg.name, keys = cfg.keys }
    end
    return options
end)()


_G.EquipTechConst = EquipTechConst
return EquipTechConst
