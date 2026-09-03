local LuaGameConditionUtils = {}

local _conditionHandlers = {
    [GEnums.ConditionType.CheckStatisticVal] = function(params)
        local args = LuaGameConditionUtils._extractInts(params)
        return GameConditionUtils.GetStatisticValueByType(args[1], args[2] or 0)
    end,
    [GEnums.ConditionType.CheckStatisticValSum] = function(params)
        local args = LuaGameConditionUtils._extractInts(params)
        local arg2 = LuaGameConditionUtils._GetIntsAtIndex(params, 2)
        local ret = GameConditionUtils.CheckStatisticValSum(args[1], arg2)
        return ret
    end,
    [GEnums.ConditionType.CheckCharSkillLevel] = function(params)
        local args = LuaGameConditionUtils._extractStrings(params)
        return GameConditionUtils.GetCharSkillLevel(args[1] or "", args[2] or "")
    end,
    [GEnums.ConditionType.CheckDungeonTypePassNum] = function(params)
        local args = LuaGameConditionUtils._extractStrings(params)
        return GameConditionUtils.GetDungeonTypePassNum(args[1])
    end,
    [GEnums.ConditionType.CheckEtherSubmitCount] = function()
        return GameConditionUtils.GetEthSubmitCount()
    end,
    [GEnums.ConditionType.CheckFactoryBlackBoxStateNum] = function(params)
        local args = LuaGameConditionUtils._extractInts(params)
        return GameConditionUtils.GetFactoryBlackBoxStateNum(args[1])
    end,
    [GEnums.ConditionType.CheckGreaterCharLevelNum] = function(params)
        local args = LuaGameConditionUtils._extractInts(params)
        return GameConditionUtils.GetGreaterCharLevelNum(args[1])
    end,
    [GEnums.ConditionType.CheckGreaterCharStageNum] = function(params)
        local args = LuaGameConditionUtils._extractInts(params)
        return GameConditionUtils.GetGreaterCharStageNum(args[1])
    end,
    [GEnums.ConditionType.CheckGreaterSettlementLevelNum] = function(params)
        local args = LuaGameConditionUtils._extractInts(params)
        return GameConditionUtils.GetGreaterSettlementLevelNum(args[1])
    end,
    [GEnums.ConditionType.CheckGreaterWeapeonLevelNum] = function(parameters)
        local args = LuaGameConditionUtils._extractInts(parameters)
        return GameConditionUtils.GetGreaterWeaponLevelNum(args[1])
    end,
    [GEnums.ConditionType.CheckGreaterWeapeonStageNum] = function(parameters)
        local args = LuaGameConditionUtils._extractInts(parameters)
        return GameConditionUtils.GetGreaterWeaponStageNum(args[1])
    end,
    [GEnums.ConditionType.CheckPassedGameMechanicsNum] = function(params)
        local args = LuaGameConditionUtils._extractStrings(params)
        return GameConditionUtils.GetPassedGameMechanicsNum(args[1])
    end,
    [GEnums.ConditionType.CheckSettlementLevelSum] = function(params)
        local args = LuaGameConditionUtils._extractStrings(params)
        return GameConditionUtils.GetSettlementLevelSum(args[1])
    end,
    [GEnums.ConditionType.CheckUnlockGroupTechNum] = function(params)
        local args = LuaGameConditionUtils._extractStrings(params)
        return GameConditionUtils.GetUnlockGroupTechNum(args[1])
    end,
    [GEnums.ConditionType.CheckSceneCollectionNum] = function(parameters)
        local levelId = parameters[CSIndex(1)] and parameters[CSIndex(1)].valueStringList[CSIndex(1)] or ""
        local collectionList = {}
        local paramList = parameters[CSIndex(2)].valueStringList
        local labelList = {}
        for i = CSIndex(1), CSIndex(paramList.Count) do
            table.insert(labelList, parameters[CSIndex(2)].valueStringList[i])
        end
        for _, label in pairs(labelList) do
            local success, cfg = Tables.collectionLabelTable:TryGetValue(label)
            if success then
                for i = CSIndex(1), CSIndex(cfg.list.Count) do
                    table.insert(collectionList, cfg.list[i].prefabId)
                end
            end
        end
        return GameConditionUtils.GetSceneCollectionNum(levelId, collectionList)
    end,
    [GEnums.ConditionType.CheckBitSetCount] = function(params)
        local args = LuaGameConditionUtils._extractInts(params)
        local key = args[1]
        local ret = GameConditionUtils.CheckBitSetCount(key)
        return ret
    end,
    [GEnums.ConditionType.CheckWorldLevel] = function(params)
        local ret = GameConditionUtils.CheckWorldLevel()
        return ret
    end,
    [GEnums.ConditionType.CheckDomainShopChannelReachLvCnt] = function(params)
        local args = LuaGameConditionUtils._extractStrings(params)
        local arg1 = args[1]
        local arg2 = args[2]
        local ret = GameConditionUtils.CheckDomainShopChannelReachLvCnt(arg1, arg2)
        return ret
    end,
    [GEnums.ConditionType.CheckDomainDevelopmentLevel] = function(params)
        local args = LuaGameConditionUtils._extractStrings(params)
        local arg1 = args[1]
        local ret = GameConditionUtils.CheckDomainDevelopmentLevel(arg1)
        return ret
    end,
    [GEnums.ConditionType.MissionStateEqual] = function(params)
        local args = LuaGameConditionUtils._extractStrings(params)
        local arg1 = args[1]
        local arg2 = tonumber(args[2])
        local arg3 = tonumber(args[3])
        local ret = GameConditionUtils.MissionStateEqual(arg1, arg2, arg3)
        return ret
    end,
    [GEnums.ConditionType.CheckPassGameMechanicsId] = function(params)
        local args = LuaGameConditionUtils._extractStrings(params)
        local arg1 = args[1]
        local ret = GameConditionUtils.CheckPassGameMechanicsId(arg1)
        return ret
    end,
    [GEnums.ConditionType.CheckEquipTierLevelNumCharNum] = function(params)
        local arg1 = LuaGameConditionUtils._GetIntsAtIndex(params, 1)[1]
        local arg2 = LuaGameConditionUtils._GetIntsAtIndex(params, 2)[1]
        local arg3 = LuaGameConditionUtils._GetBoolsAtIndex(params, 3)[1] or false
        local ret = GameConditionUtils.CheckEquipTierLevelNumCharNum(arg1, arg2, arg3)
        return ret
    end,
    [GEnums.ConditionType.CheckDomainKiteStationUnlockCount] = function(params)
        local ret = GameConditionUtils.CheckDomainKiteStationUnlockCount()
        return ret
    end,
}

function LuaGameConditionUtils.getConditionValueByParameters(conditionType, parameters)
    if not GameConditionUtils.CheckClientConditionType(conditionType) then


        logger.info("调用了没有实现的Condition进度 :" .. tostring(conditionType))
        return false, 0
    end
    local handler = _conditionHandlers[conditionType]
    if handler then
        local ok, result = pcall(handler, parameters)
        if ok then
            return true, result or 0
        end
    end
    return false, 0
end

function LuaGameConditionUtils._extractInts(parameters)
    local results = {}
    for i = CSIndex(1), CSIndex(parameters.Count) do
        results[#results + 1] = parameters[i].valueIntList[CSIndex(1)]
    end
    return results
end

function LuaGameConditionUtils._extractStrings(parameters)
    local results = {}
    for i = CSIndex(1), CSIndex(parameters.Count) do
        results[#results + 1] = parameters[i].valueStringList[CSIndex(1)]
    end
    return results
end

function LuaGameConditionUtils._GetIntsAtIndex(parameters, luaIndex)
    local results = {}
    local csIndex = CSIndex(luaIndex)
    if csIndex >= 0 and csIndex < parameters.Count then
        local list = parameters[csIndex].valueIntList
        for i = 0, list.Count - 1 do
            table.insert(results, list[i])
        end
    end
    return results
end

function LuaGameConditionUtils._GetStringsAtIndex(parameters, luaIndex)
    local results = {}
    local csIndex = CSIndex(luaIndex)
    if csIndex >= 0 and csIndex < parameters.Count then
        local list = parameters[csIndex].valueStringList
        for i = 0, list.Count - 1 do
            table.insert(results, list[i])
        end
    end
    return results
end

function LuaGameConditionUtils._GetBoolsAtIndex(parameters, luaIndex)
    local results = {}
    local csIndex = CSIndex(luaIndex)
    if csIndex >= 0 and csIndex < parameters.Count then
        local list = parameters[csIndex].valueBoolList
        for i = 0, list.Count - 1 do
            table.insert(results, list[i])
        end
    end
    return results
end

_G.LuaGameConditionUtils = LuaGameConditionUtils
return LuaGameConditionUtils
