local error              = logger.error
local pairs              = pairs
local ipairs             = ipairs
local select             = select
local concat             = table.concat
local format             = string.format
local getinfo            = debug.getinfo
local setmetatable       = setmetatable
local warn               = warn or print
local assert             = assert

local HyperLua           = require("HyperLua")
local Log                = HyperLua.Log
local IsObject           = HyperLua.IsObject
local IsObjectType       = HyperLua.IsObjectType
local IsDerivedFrom      = HyperLua.IsDerivedFrom
local IsInstance         = HyperLua.IsInstance
local NewObjectType      = HyperLua.NewObjectType
local RenewObjectType    = HyperLua.RenewObjectType
local ReloadObjectType   = HyperLua.ReloadObjectType
local IsReloadingType    = HyperLua.IsReloadingType
local CommitObjectType   = HyperLua.CommitObjectType
local IsCommitedType     = HyperLua.IsCommitedType
local HasField           = HyperLua.HasField
local AddField           = HyperLua.AddField
local AddMethod          = HyperLua.AddMethod
local ReplaceMethod      = HyperLua.ReplaceMethod
local HotAddMethod       = HyperLua.HotAddMethod
local HotReplaceMethod   = HyperLua.HotReplaceMethod
local SetObjectAsConst   = HyperLua.SetObjectAsConst
local IsConstObject      = HyperLua.IsConstObject
local GetObjectID        = HyperLua.GetObjectID
local GetObjectSize      = HyperLua.GetObjectSize
local GetObjectCount     = HyperLua.GetObjectCount
local GetMethodInfo      = HyperLua.GetMethodInfo
local GetConstructor     = HyperLua.GetConstructor
local GetTypeName        = HyperLua.GetTypeName
local GetBaseTypeName    = HyperLua.GetBaseTypeName
local TryGet             = HyperLua.TryGet
local TrySet             = HyperLua.TrySet
local LuaType            = HyperLua.LuaType
local LuaPairs           = HyperLua.Pairs
local LuaObjects         = HyperLua.Objects
local LuaToString        = HyperLua.ToString
local DumpEnv            = HyperLua.DumpEnv

local AddExternalTypeSys = HyperLua.AddExternalTypeSystem
local IsExternalType     = HyperLua.IsExternalType
local IsExternalInstance = HyperLua.IsExternalInstance

local LogLevel_Info      = 0
local LogLevel_Warn      = 1
local LogLevel_Error     = 2


local ProfilerWrapper    = _G.g_ProfilerWrapper

local ASSETS_PATH        = _G.g_AssetsPath or "."

local IS_BUILD_SHIPPING  = _G.g_IsBuildShipping

local PLACEHOLDER        = "placeholder"

local OPTIONAL           = "optional"

local EMPTY_TABLE        = {}


local HL                 = {}


HL.Boolean               = "boolean"
HL.Number                = "number"
HL.Int                   = "int"
HL.String                = "string"
HL.Table                 = "table"
HL.Userdata              = "userdata"
HL.Pointer               = "pointer"
HL.Function              = "function"
HL.Varlist               = "varlist"
HL.Thread                = "thread"
HL.Any                   = "any"



HL.Opt = function(...)
    return OPTIONAL, ...
end



local HyperLuaObject

local HyperLuaReloading  = true

local HyperLuaClasses    = {}

local HyperLuaFwdClasses = {}

local HyperLuaStrCache   = {}

local AnonymousTypeID    = 1

local MT_CTOR            = 1
local MT_STATIC          = 2
local MT_INSTANCE        = 3
local MT_VIRTUAL         = 4
local MT_OVERRIDE        = 5
local MT_FINAL           = 6


local HyperLuaBuiltinTypes = {
    ["boolean"]  = true,
    ["number"]   = true,
    ["int"]      = true,
    ["pointer"]  = true,
    ["string"]   = true,
    ["table"]    = true,
    ["userdata"] = true,
    ["function"] = true,
    ["thread"]   = true,
    ["any"]      = true,
}


local HyperLuaInitValueTypes = {
    ["boolean"]  = {
        ["boolean"]  = true,
    },
    ["number"]   = {
        ["number"]   = true,
    },
    ["int"]      = {
        ["number"]   = true,
    },
    ["pointer"]  = {
        ["nil"]      = true,
        ["pointer"]  = true,
    },
    ["string"]   = {
        ["string"]   = true,
    },
    ["table"]    = {
        ["nil"]      = true,
        ["function"] = true,
    },
    ["userdata"] = {
        ["nil"]      = true,
        ["function"] = true,
    },
    ["function"] = {
        ["nil"]      = true,
        ["function"] = true,
    },
    ["thread"]   = {
        ["nil"]      = true,
        ["function"] = true,
    },
}

local HyperLuaParamValueTypes = {
    ["boolean"]   = {
        ["boolean"]  = true,
    },
    ["number"]   = {
        ["number"]   = true,
    },
    ["int"]      = {
        ["number"]   = true,
    },
    ["pointer"]  = {
        ["nil"]      = true,
        ["pointer"]  = true,
    },
    ["string"]   = {
        ["string"]   = true,
    },
    ["table"]    = {
        ["nil"]      = true,
        ["table"]    = true,
    },
    ["userdata"] = {
        ["nil"]      = true,
        ["userdata"] = true,
    },
    ["function"] = {
        ["nil"]      = true,
        ["function"] = true,
    },
    ["thread"]   = {
        ["nil"]      = true,
        ["thread"]   = true,
    },
}

local function SaveOption(name, value)
    value = tostring(value)

    local path = string.format("%s/%s.flag", ASSETS_PATH, name)
    local file = io.open(path, "wb")
    if file then
        file:write(value)
        file:close()
    end
    return value
end

local function LoadOption(name, default)
    local path = string.format("%s/%s.flag", ASSETS_PATH, name)
    local file = io.open(path, "rb")
    if file then
        local ret = file:read("*a")
        file:close()
        return ret
    end
    return default
end


local RuntimeTypeCheckLevel_None  = 0
local RuntimeTypeCheckLevel_Log   = 1
local RuntimeTypeCheckLevel_Error = 2
local RuntimeTypeCheckLevel       = RuntimeTypeCheckLevel_Error
local RuntimeTypeCheck            = true
HL.RuntimeTypeCheckLevel_None     = RuntimeTypeCheckLevel_None
HL.RuntimeTypeCheckLevel_Log      = RuntimeTypeCheckLevel_Log
HL.RuntimeTypeCheckLevel_Error    = RuntimeTypeCheckLevel_Error
do


    function HL.SetRuntimeTypeCheckLevel(level)
        assert(level >= RuntimeTypeCheckLevel_None and level <= RuntimeTypeCheckLevel_Error)
        SaveOption("rttc", level)
        return level
    end


    function HL.GetRuntimeTypeCheckLevel()
        return RuntimeTypeCheckLevel
    end


    local DefaultRuntimeTypeCheckLevel, RuntimeTypeCheckFlag
    if IS_BUILD_SHIPPING then
        DefaultRuntimeTypeCheckLevel = RuntimeTypeCheckLevel_None
        RuntimeTypeCheckFlag = RuntimeTypeCheckLevel_None
    else
        DefaultRuntimeTypeCheckLevel = _G.g_OverrideDefaultTypeCheckFlag or RuntimeTypeCheckLevel_Error
        RuntimeTypeCheckFlag = _G.g_OverrideTypeCheckFlag or LoadOption("rttc", DefaultRuntimeTypeCheckLevel)
    end

    RuntimeTypeCheckFlag  = tonumber(RuntimeTypeCheckFlag) or DefaultRuntimeTypeCheckLevel
    RuntimeTypeCheckLevel = RuntimeTypeCheckFlag
    RuntimeTypeCheck      = RuntimeTypeCheckLevel ~= RuntimeTypeCheckLevel_None


    HyperLua.SetRuntimeTypeCheckLevel(RuntimeTypeCheckLevel)
end


local RuntimeObjectTrack = false
do

    function HL.SetRuntimeObjectTrackEnabled(enabled)
        if enabled == nil then enabled = not RuntimeObjectTrack end
        SaveOption("rtot", enabled and "1" or "0")
        return enabled
    end


    function HL.GetRuntimeObjectTrackEnabled()
        return RuntimeObjectTrack
    end


    local RuntimeObjectTrackFlag = LoadOption("rtot", "0")
    RuntimeObjectTrack = RuntimeObjectTrackFlag == "1"

    HyperLua.SetRuntimeObjectTrack(RuntimeObjectTrack)
end

local function TypeCheckError(str, ...)
    local msg = select('#', ...) > 0 and format(str, ...) or str
    if RuntimeTypeCheckLevel == RuntimeTypeCheckLevel_Error then
        error(msg)
    else
        Log(LogLevel_Error, msg)
    end
end


local function Error(str, ...)
    local msg = select('#', ...) > 0 and format(str, ...) or str
    error(msg)
end


local function GetType(object)
    local typeName = GetTypeName(object)
    return typeName and HyperLuaClasses[typeName] or nil
end


local function GetBaseType(objectOrType)
    local baseTypeName = GetBaseTypeName(objectOrType)
    return baseTypeName and HyperLuaClasses[baseTypeName] or nil
end


local function IsBuiltinType(typeName)
    return HyperLuaBuiltinTypes[typeName] ~= nil
end


local function CheckFieldType(fieldType)
    if not IsBuiltinType(fieldType) and not IsObjectType(fieldType) and not IsExternalType(fieldType) then
        Error("invalid field type '%s'", tostring(fieldType))
    end
end


local function CheckFieldName(objectType, fieldName)
    if HasField(objectType, fieldName) then
        Error("member with name '%s' already exists in '%s'", fieldName, GetTypeName(objectType))
    end
end


local function CheckFieldInitValue(objectType, fieldName, fieldType, storeInObject, initValue)
    if fieldType == "any" then
        return
    end

    local initValueType = LuaType(initValue)
    local expectedTypes = HyperLuaInitValueTypes[fieldType]
    if not expectedTypes then
        if storeInObject then
            if initValueType ~= "function" and initValueType ~= "nil" then
                Error("%s @ %s: '%s' or '%s' expected, got '%s'", fieldName, GetTypeName(objectType), "function", "nil", initValueType)
            end
        else
            if initValueType ~= "userdata" and initValueType ~= "function" and initValueType ~= "nil" then
                Error("%s @ %s: '%s' or '%s' or '%s' expected, got '%s'", fieldName, GetTypeName(objectType), "userdata", "function", "nil", initValueType)
            end

            if initValueType == "userdata" then
                if not IsInstance(initValue, fieldType) then
                    Error("%s @ %s: '%s' expected", fieldName, GetTypeName(objectType), GetTypeName(fieldType))
                end
            end
        end
    else
        if not expectedTypes[initValueType] then
            if storeInObject then
                local types = {}
                for k, _ in pairs(expectedTypes) do
                    types[#types + 1] = "'" .. k .. "'"
                end
                Error("%s @ %s: %s expected, got '%s'", fieldName, GetTypeName(objectType), concat(types, " or "), initValueType)
            else
                if fieldType ~= initValueType then
                    local types = { fieldType }
                    for k, _ in pairs(expectedTypes) do
                        types[#types + 1] = "'" .. k .. "'"
                    end
                    Error("%s @ %s: %s expected, got '%s'", fieldName, GetTypeName(objectType), concat(types, " or "), initValueType)
                end
            end
        end
    end
end


local function CheckMethodInitValue(methodBody)
    local methodBodyType = LuaType(methodBody)
    if methodBodyType  ~= "function" then
        Error("'%s' expected, got '%s'", "function", methodBodyType)
    end
end


local function CheckMethodParamTypes(paramTypes, baseParamTypes, isReturn)
    local name = isReturn and "return value" or "param"
    if paramTypes and baseParamTypes then
        if #paramTypes ~= #baseParamTypes then
            TypeCheckError("%s type count mismatch ('%d' expected, got '%d')", name, #baseParamTypes, #paramTypes)
            return false
        end

        for ith, paramType in ipairs(paramTypes) do
            local baseParamType = baseParamTypes[ith]
            if paramType ~= baseParamType then
                if IsBuiltinType(paramType) or IsBuiltinType(baseParamType) or not IsDerivedFrom(paramType, baseParamType) then
                    local paramTypeName = IsBuiltinType(paramType) and paramType or GetTypeName(paramType)
                    local baseParamTypeName = IsBuiltinType(baseParamType) and baseParamType or GetTypeName(baseParamType)
                    TypeCheckError("bad %s type #%d ('%s' expected, got '%s')", name, ith, baseParamTypeName, paramTypeName)
                    return false
                end
            end
        end
    elseif paramTypes then
        TypeCheckError("%s type count mismatch ('%d' expected, got '%d')", name, 0, #paramTypes)
        return false
    elseif baseParamTypes then
        TypeCheckError("%s type count mismatch ('%d' expected, got '%d')", name, #baseParamTypes, 0)
        return false
    end
    return true
end


local function CheckOverrideMethod(objectType, baseObjectType, methodName, inParams, retParams)
    local methodInfo = GetMethodInfo(baseObjectType, methodName)
    if not methodInfo then
        TypeCheckError("'%s' override method '%s' not exists in '%s'", GetTypeName(objectType), methodName, GetTypeName(baseObjectType))
        return false
    end

    local baseMethodType, baseInParams, baseRetParams = methodInfo[1], methodInfo[2], methodInfo[3]
    if baseMethodType == MT_VIRTUAL or baseMethodType == MT_OVERRIDE then
        CheckMethodParamTypes(inParams, baseInParams)
        CheckMethodParamTypes(retParams, baseRetParams)
    elseif baseMethodType == MT_FINAL then
        TypeCheckError("'%s' can not override final method '%s' in '%s'", GetTypeName(objectType), methodName, GetTypeName(baseObjectType))
        return false
    elseif baseMethodType == MT_STATIC then
        TypeCheckError("'%s' can not override static method '%s' in '%s'", GetTypeName(objectType), methodName, GetTypeName(baseObjectType))
        return false
    else
        TypeCheckError("'%s' can not override non-virtual method '%s' in '%s'", GetTypeName(objectType), methodName, GetTypeName(baseObjectType))
        return false
    end
    return true
end


local function CreateMethodParams(isReturn, ...)
    local hasVarList = false
    local curParams  = {}
    local requireCnt = nil
    for _, paramType in ipairs({ ... }) do
        if hasVarList then
            if isReturn then
                TypeCheckError("'varlist' must be the last return value type")
            else
                TypeCheckError("'varlist' must be the last param type")
            end
        else
            if paramType == "varlist" then
                hasVarList = true
                curParams[#curParams + 1] = paramType
            elseif paramType == PLACEHOLDER then
                curParams[#curParams + 1] = paramType
            elseif paramType == OPTIONAL then
                if requireCnt ~= nil then
                    TypeCheckError("'Opt' should be the last group or parameters.")
                end
                requireCnt = #curParams
            elseif IsBuiltinType(paramType) then
                curParams[#curParams + 1] = paramType
            elseif IsObjectType(paramType) then
                curParams[#curParams + 1] = paramType
            elseif IsExternalType(paramType) then
                curParams[#curParams + 1] = paramType
            else
                if isReturn then
                    TypeCheckError("invalid return value type")
                else
                    TypeCheckError("invalid param type")
                end
            end
        end
    end

    if isReturn and #curParams == 0 then
        TypeCheckError("no return value type found.")
    end

    if requireCnt == nil then
        requireCnt = #curParams
    end

    return #curParams > 0 and curParams or nil, requireCnt
end


local function CheckMethodParamValues(debugInfo, paramTypes, requireCnt, isReturn, ...)
    if requireCnt == nil then
        requireCnt = 0
    end

    local name = isReturn and "return value" or "argument"
    for ith, paramType in ipairs(paramTypes) do
        if paramType == "varlist" then
            return select(1, ...)
        elseif paramType ~= "any" then
            local paramValue = select(ith, ...)
            local gotType = LuaType(paramValue)
            local expectedTypes = HyperLuaParamValueTypes[paramType]
            if expectedTypes then
                if not expectedTypes[gotType] then
                    if ith <= requireCnt or
                       gotType ~= "nil" then
                        local types = {}
                        for k, _ in pairs(expectedTypes) do
                            types[#types + 1] = "'" .. k .. "'"
                        end
                        TypeCheckError("%s: bad %s #%d (%s expected, got '%s')", debugInfo, name, ith, concat(types, ' or '), gotType)
                    end
                end
            elseif paramValue then
                local ok, errParam = IsInstance(paramValue, paramType)
                if not ok then
                    local externalTypeName = IsExternalType(paramType)
                    if externalTypeName then
                        if not IsExternalInstance(paramValue, paramType) then
                            TypeCheckError("%s: bad %s #%d (external '%s' expected, got '%s')", debugInfo, name, ith, externalTypeName, LuaType(paramValue))
                        end
                    else
                        if errParam == 1 then
                            TypeCheckError("%s: bad %s #%d (HL 'object' expected, got '%s')", debugInfo, name, ith, gotType)
                        elseif errParam == 2 then
                            TypeCheckError("%s: bad %s #%d (HL 'class' expected)", debugInfo, name, ith)
                        else
                            TypeCheckError("%s: bad %s #%d ('%s' expected, got '%s)", debugInfo, name, ith, GetTypeName(paramType), GetTypeName(paramValue))
                        end
                    end
                end
            end
        end
    end


    local expectedMinCount = requireCnt
    local expectedMaxCount = #paramTypes
    local gotCount = select('#', ...)
    if expectedMinCount == expectedMaxCount then
        if expectedMinCount ~= gotCount then
            TypeCheckError("%s: %s count mismatch (expected '%d', got '%d')", debugInfo, name, expectedMinCount, gotCount)
        end
    else
        if gotCount < expectedMinCount then
            TypeCheckError("%s: %s count mismatch (expected '%d' at least, got '%d')", debugInfo, name, expectedMinCount, gotCount)
        elseif gotCount > expectedMaxCount then
            TypeCheckError("%s: %s count mismatch (expected '%d' at most, got '%d')", debugInfo, name, expectedMaxCount, gotCount)
        end
    end
    return select(1, ...)
end


local function CreateMethodWrapper(methodName, methodBody, inParams, inRequireCnt, retParams, retRequireCnt)
    local info = getinfo(methodBody, "S")
    local debugInfo = format("%s:%d:%s", info.source, info.linedefined, methodName)
    if inParams and retParams then
        return function(...)
            return CheckMethodParamValues(debugInfo, retParams, retRequireCnt, true, methodBody(CheckMethodParamValues(debugInfo, inParams, inRequireCnt, false, ...)))
        end
    elseif inParams then
        return function(...)
            return methodBody(CheckMethodParamValues(debugInfo, inParams, inRequireCnt, false, ...))
        end
    elseif retParams then
        return function(...)
            return CheckMethodParamValues(debugInfo, retParams, retRequireCnt, true, methodBody(...))
        end
    else
        return methodBody
    end
end


local function CreateProxy(metatable)
    return setmetatable({}, metatable)
end


local function AddStringToCache(str)
    HyperLuaStrCache[str] = true
end


local function FieldInternal(objectType, fieldType, storeInObject, constant, fieldName, initValue)
    if IsReloadingType(objectType) then

        return
    end


    CheckFieldType(fieldType)


    CheckFieldName(objectType, fieldName)
    CheckFieldInitValue(objectType, fieldName, fieldType, storeInObject, initValue)


    local internalFieldType
    if IsObjectType(fieldType) then
        internalFieldType = "object"
    elseif IsExternalType(fieldType) then
        internalFieldType = "external"
    else
        internalFieldType = fieldType
    end
    if not AddField(objectType, fieldName, internalFieldType, storeInObject, constant, initValue, fieldType) then
        Error("add '%s' field to '%s' failed", fieldName, GetTypeName(objectType))
    end


    AddStringToCache(fieldName)
end


local function MethodInternal(objectType, methodType, inParams, inRequireCnt, retParams, retRequireCnt, methodName, methodBody)
    local methodInfo

    if RuntimeTypeCheck then
        methodInfo = { methodType, inParams, retParams }
    end

    local typeName = GetTypeName(objectType)
    local isOverrideOrFinal = methodType == MT_OVERRIDE or methodType == MT_FINAL
    local methodWrapper = methodBody

    if RuntimeTypeCheck then
        local isConstructor = methodType == MT_CTOR
        if isConstructor then
            if retParams then
                TypeCheckError("return value is not allowed in constructor '%s'", methodName)
            end
        end


        CheckMethodInitValue(methodBody)

        methodWrapper = CreateMethodWrapper(methodName, methodBody, inParams, inRequireCnt, retParams, retRequireCnt)
    end


    if ProfilerWrapper then
        local info = getinfo(methodBody, "S")
        local funcName = format("%s.%s %s:%d", typeName, methodName, info.source, info.linedefined)
        methodWrapper = ProfilerWrapper(funcName, methodWrapper)
    end

    if IsReloadingType(objectType) then
        local bHasMethodField = HasField(objectType, methodName)



        local subTypes = {}
        do
            local insert = table.insert
            for _, t in pairs(HyperLuaClasses) do
                if t ~= objectType and IsDerivedFrom(t, objectType) then
                    local bValidInSubType = true
                    if RuntimeTypeCheck and false then
                        if (not bHasMethodField) and HasField(t, methodName) then
                            if not CheckOverrideMethod(objectType, GetBaseType(objectType) or HyperLuaObject, methodName, inParams, retParams) then
                                Log(LogLevel_Error, "hot replace method '%s' in '%s' failed, method with the same name already exists in sub-type '%s'", methodName, GetTypeName(objectType), GetTypeName(t))
                                bValidInSubType = false
                            end
                        end
                    end
                    if bValidInSubType then
                        insert(subTypes, t)
                    end
                end
            end
        end

        if bHasMethodField then
            if isOverrideOrFinal then
                if RuntimeTypeCheck then
                    CheckOverrideMethod(objectType, GetBaseType(objectType) or HyperLuaObject, methodName, inParams, retParams)
                end
            end

            if not HotReplaceMethod(objectType, methodName, methodType, methodWrapper, methodInfo, subTypes) then
                Error("hot replace method '%s' in '%s' failed", methodName, GetTypeName(objectType))
            end
        else

            if not HotAddMethod(objectType, methodName, methodType, methodWrapper, methodInfo, subTypes) then
                Error("hot add new method '%s' in '%s' failed", methodName, GetTypeName(objectType))
            end
        end
    else
        if isOverrideOrFinal then

            if RuntimeTypeCheck then
                CheckOverrideMethod(objectType, GetBaseType(objectType) or HyperLuaObject, methodName, inParams, retParams)
            end


            if not ReplaceMethod(objectType, methodName, methodType, methodWrapper, methodInfo) then
                Error("replace method '%s' in '%s' failed", methodName, GetTypeName(objectType))
            end
        else

            CheckFieldName(objectType, methodName)


            if not AddMethod(objectType, methodName, methodType, methodWrapper, methodInfo) then
                Error("add method '%s' to '%s' failed", methodName, GetTypeName(objectType))
            end
        end
    end


    AddStringToCache(methodName)
end




function HL.Class(classNameOrNil, baseObjectType)

    local className = classNameOrNil
    if not className then
        className = format("HL.Anonymous.%d", AnonymousTypeID)
        AnonymousTypeID = AnonymousTypeID + 1
    end


    local objectType = HyperLuaClasses[className]
    local bReloading = false
    if objectType then
        if not HyperLuaReloading then
            Error("class with name '%s' already exists", className)
        end


        bReloading = true
    end


    if baseObjectType and not IsCommitedType(baseObjectType) then
        Error("base type '%s' is not commited", GetTypeName(baseObjectType))
    end

    baseObjectType = baseObjectType or HyperLuaObject

    if bReloading then
        if GetBaseType(objectType) ~= baseObjectType then
            Error("bad base type, '%s' expected, got '%s'", GetTypeName(GetBaseType(objectType)), GetTypeName(baseObjectType))
        end

        ReloadObjectType(objectType)
        return objectType
    end



    local fwdObjectType = HyperLuaFwdClasses[className]
    if fwdObjectType then
        HyperLuaFwdClasses[className] = nil
        objectType = RenewObjectType(fwdObjectType, baseObjectType)
    else
        objectType = NewObjectType(className, baseObjectType)
    end

    HyperLuaClasses[className] = objectType
    return objectType
end


do
    local fieldStoreInObject = false
    local fieldIsConstant    = false
    local fieldValueType
    local fieldInitValue

    local fieldProxy = nil
    fieldProxy = CreateProxy({
        __shl = function(_, defaultValue)
            fieldInitValue = defaultValue
            return fieldProxy
        end,
    })




    local function MakeFieldProxy(storeInObject, isConstant)
        return function(valueType, initValue)
            fieldStoreInObject = storeInObject
            fieldIsConstant    = isConstant
            fieldValueType     = valueType
            fieldInitValue     = initValue
            return fieldProxy
        end
    end


    HL.Field = MakeFieldProxy(true, false)


    HL.Const = MakeFieldProxy(false, true)


    HL.StaticField = MakeFieldProxy(false, false)

    local methodType
    local methodInParams, methodInRequireCnt
    local methodRetParams, methodRetRequireCnt
    local methodBody

    local methodProxy = nil
    methodProxy = CreateProxy({
        __shl = function(_, func)
            methodBody = func
            return methodProxy
        end,
        __index = {
            Return = function(...)
                if RuntimeTypeCheck then
                    methodRetParams, methodRetRequireCnt = CreateMethodParams(true, ...)
                end
                return methodProxy
            end,
            Assign = function(func)
                methodBody = func
                return methodBody
            end,
        },
    })



    local function MakeMethodProxy(inMethodType)
        return function(...)
            if RuntimeTypeCheck then
                methodType          = nil
                methodInParams      = nil
                methodInRequireCnt  = nil
                methodRetParams     = nil
                methodRetRequireCnt = nil
                methodBody          = nil
            end

            methodType = inMethodType
            if methodType ~= MT_STATIC then
                if RuntimeTypeCheck then
                    methodInParams, methodInRequireCnt = CreateMethodParams(false, PLACEHOLDER, ...)
                end
            else
                if RuntimeTypeCheck then
                    methodInParams, methodInRequireCnt = CreateMethodParams(false, ...)
                end
            end
            return methodProxy
        end
    end

    HL.Constructor  = MakeMethodProxy(MT_CTOR)
    HL.Method       = MakeMethodProxy(MT_INSTANCE)
    HL.Virtual      = MakeMethodProxy(MT_VIRTUAL)
    HL.Override     = MakeMethodProxy(MT_OVERRIDE)
    HL.Final        = MakeMethodProxy(MT_FINAL)
    HL.StaticMethod = MakeMethodProxy(MT_STATIC)

    local function DeclareMemberToObjectType(objectType, memberName, memberProxy)
        if memberProxy == methodProxy then
            if RuntimeTypeCheck then
                if methodType ~= MT_STATIC then
                    methodInParams[1] = objectType
                end
                if methodType == MT_CTOR then
                    assert(methodRetParams == nil)
                elseif methodRetParams == nil then
                    methodRetParams = EMPTY_TABLE
                end
            end
            MethodInternal(objectType, methodType, methodInParams, methodInRequireCnt, methodRetParams, methodRetRequireCnt, memberName, methodBody)

            if RuntimeTypeCheck then
                methodType          = nil
                methodInParams      = nil
                methodInRequireCnt  = nil
                methodRetParams     = nil
                methodRetRequireCnt = nil
                methodBody          = nil
            end

            return
        end

        if memberProxy == fieldProxy then
            FieldInternal(objectType, fieldValueType, fieldStoreInObject, fieldIsConstant, memberName, fieldInitValue)

            fieldValueType     = nil
            fieldStoreInObject = false
            fieldIsConstant    = false
            fieldInitValue     = nil

            return
        end

        Error("unknown declaration '%s' with member name '%s' to object type '%s'", tostring(memberProxy), memberName, GetTypeName(objectType))
    end

    HyperLua.SetObjectTypeNewIndexBeforeCommit(DeclareMemberToObjectType)
end

function HL.Commit(objectType)
    local isCommited = IsCommitedType(objectType)
    if isCommited then
        Error("repeated Commit to class '%s'", GetTypeName(objectType))
    end
    CommitObjectType(objectType)
    return objectType
end


function HL.Forward(className)
    local objectType = HyperLuaClasses[className] or HyperLuaFwdClasses[className]
    if not objectType then
        objectType = NewObjectType(className)
        HyperLuaFwdClasses[className] = objectType
    end
    return objectType
end


do
    local ObjectTypeFallbackAccessors = {
        Super = function(objectType)
            return HL.GetBaseType(objectType)
        end,
        SuperConstructor = function(objectType)
            return HL.Super(objectType)
        end,
    }

    local function AccessFallbackFieldOfObjectType(objectType, key)
        local f = ObjectTypeFallbackAccessors[key]
        return f and f(objectType, key)
    end

    HyperLua.SetObjectTypeIndexFallback(AccessFallbackFieldOfObjectType)
end


function HL.Super(objectType)
    local baseType = GetBaseType(objectType)
    return baseType and GetConstructor(baseType) or nil
end


local function DumpObject(obj, onlyHyperLua, usePrint)
    if type(obj) ~= "userdata" or not HL.Is(obj, HL.Object) then
        Error("bad argument #1, instance of HyperLua Object expected, got '%s'", type(obj))
    end

    local lines  = {}
    local insert = table.insert
    insert(lines, format("%s = {", LuaToString(obj)))
    for k, v in LuaPairs(obj) do
        if IsObject(v) then
            insert(lines, format("    %s = %s", k, LuaToString(v)))
        else
            if not onlyHyperLua then
                insert(lines, format("    %s = %s", k, v))
            end
        end
    end
    insert(lines, "}")

    local ret = table.concat(lines, "\n")
    if usePrint == nil or usePrint == true then
        print(ret)
        return
    end

    return ret
end


local function DumpObjects(onlyHyperLua)

    collectgarbage("stop")
    for obj, t in LuaObjects() do
        DumpObject(obj, onlyHyperLua)
    end

    collectgarbage("restart")
end


local function DumpObjectCount(outputFunc)
    if outputFunc == nil then
        outputFunc = warn
    end

    local nameLength = 0

    local countTotal = 0
    local countArray = {}
    for typeName, objectType in pairs(HyperLuaClasses) do
        local objectCount = GetObjectCount(objectType)
        if objectCount > 0 then
            countTotal = countTotal + objectCount
            countArray[#countArray + 1] = { typeName, objectCount }

            if #typeName > nameLength then
                nameLength = #typeName
            end
        end
    end


    table.sort(countArray, function(e1, e2)
        return e1[2] > e2[2]
    end)

    local dumpFmt = format("%%%ds: %%d", nameLength)

    outputFunc(format("HL object count: %d", countTotal))
    for k, v in pairs(countArray) do
        outputFunc(format(dumpFmt, v[1], v[2]))
    end
end

HL.DumpObject      = DumpObject
HL.DumpObjects     = DumpObjects
HL.DumpObjectCount = DumpObjectCount


HL.DumpUncommited  = function()
    warn("Uncommitted HL Classes:")
    for objectType, v in pairs(HyperLuaClasses) do
        if not IsCommitedType(objectType) then
            warn(GetTypeName(objectType))
        end
    end
end


HyperLuaObject = HL.Class("HyperLua.Object")
do


































    HL.Commit(HyperLuaObject)
end

HL.Object          = HyperLuaObject

HL.GetType         = GetType
HL.GetTypeName     = GetTypeName
HL.GetBaseType     = GetBaseType
HL.GetBaseTypeName = GetBaseTypeName

HL.GetObjectID     = GetObjectID
HL.GetObjectSize   = GetObjectSize
HL.GetObjectCount  = GetObjectCount

HL.SetObjectAsConst= SetObjectAsConst
HL.IsConstObject   = IsConstObject

HL.As              = function(object, objectType)
    return IsInstance(object, objectType) and object or nil
end

HL.Is              = IsInstance
HL.TryGet          = TryGet
HL.TrySet          = TrySet

HL.DumpEnv         = function(objectType, reason)
    DumpEnv(objectType, reason or "unknown")
end

HL.SetReloading    = function(bReload)
    HyperLuaReloading = bReload == true
end

HL.IsReloading     = function()
    return HyperLuaReloading
end










local HLExternalTypeSystem = HL.Class("HLExternalTypeSystem")
do

    HLExternalTypeSystem.IsExternalType = HL.Virtual(HL.Any).Return(HL.Opt(HL.String)) << function(self, typeToTest)
        return nil
    end

    HLExternalTypeSystem.IsExternalInstance = HL.Virtual(HL.Userdata, HL.Any).Return(HL.Boolean) << function(self, instanceToTest, type)
        return false
    end

    HLExternalTypeSystem.Register = HL.Method() << function(self)
        AddExternalTypeSys(self)
    end

    HL.Commit(HLExternalTypeSystem)
end

HL.ExternalTypeSystem = HLExternalTypeSystem

return HL
