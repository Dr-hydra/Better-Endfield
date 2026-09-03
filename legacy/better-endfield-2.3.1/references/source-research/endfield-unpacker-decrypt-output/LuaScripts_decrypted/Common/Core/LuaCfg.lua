require("LuaUtils")

local setmetatable  = setmetatable
local rawset        = rawset
local LuaSpark      = _G.LuaUtils.LuaSpark
local UnityEngine   = CS.UnityEngine

local LuaCfg = {}


local Tables = {}
setmetatable(Tables, {
    __mode = "v",
    __index = function(t, k)
        if type(k) ~= 'string' then
            return nil
        end

        local root = LuaSpark.GetRoot(k)
        if root == nil then
            return nil
        end

        rawset(t, k, root)
        return root
    end,
})

_G.unloadSparkTable = function(name)
    LuaSpark.UnloadRoot(name)
    Tables[name] = nil
end

LuaCfg.Tables = Tables


local Types = {}
setmetatable(Types, {
    __index = function(t, k)
        if type(k) ~= 'string' then
            return nil
        end

        local type = LuaSpark.GetBeanTypeByName(k)
        if type == nil then
            return nil
        end

        rawset(t, k, type)
        rawset(t, type, k)
        return type
    end
})

LuaCfg.Types = Types



hg.curEnvLang = CS.Beyond.I18n.I18nUtils.curEnvLang:GetHashCode()
local function I18nGetText(id, text, ...)
    if UNITY_EDITOR then
        CS.Beyond.I18n.I18nUtils.RecordTextId(id)
    end

    if BEYOND_DEBUG_COMMAND then
        if CS.Beyond.I18n.I18nUtils.GetShowTextIdMode() then
            local hex = string.format("%X", id)
            return hex
        end
        local args = {...}
        local in_recursion = args[1] or false
        if CS.Beyond.I18n.I18nUtils.GetReplaceLangMode() and not in_recursion then
            local hex = string.format("#*%X*#%s", id, I18nGetText(id, text, true))
            return hex
        end
    end

    if hg.curEnvLang == 0 and text ~= nil and text ~= '' then
        if id == 0 then
            if BEYOND_DEBUG_COMMAND then
                return '*' .. text
            else
                return ''
            end
        end
        return text
    elseif id == 0 then
        return ''
    else
        local hotfixText = CS.Beyond.I18n.I18nHotfixManager.instance:GetHotFixText(id)
        if hotfixText ~= '' then
            if BEYOND_DEBUG_COMMAND then
                Notify(MessageConst.SHOW_TOAST, string.format("使用了RemoteConfig里的热更文本 id(%d)text(%s)", id, hotfixText))
            end
            return hotfixText
        end

        local foundPatch, i18NHotfixDataList = Tables.i18nHotFix:TryGetValue(id)
        if foundPatch then
            local patchText = ''
            for i = 1, #i18NHotfixDataList.list do
                local data = i18NHotfixDataList.list[CSIndex(i)]
                if data.type:GetHashCode() == hg.curEnvLang and data.platform == GEnums.I18nPlatform.DF then
                    patchText = data.text
                    break
                end
            end
            if DeviceInfo.inputType == DeviceInfo.InputType.Touch then
                for i = 1, #i18NHotfixDataList.list do
                    local data = i18NHotfixDataList.list[CSIndex(i)]
                    if data.type:GetHashCode() == hg.curEnvLang and data.platform == GEnums.I18nPlatform.MB then
                        patchText = data.text
                        break
                    end
                end
            elseif DeviceInfo.inputType == DeviceInfo.InputType.Controller then
                for i = 1, #i18NHotfixDataList.list do
                    local data = i18NHotfixDataList.list[CSIndex(i)]
                    if data.type:GetHashCode() == hg.curEnvLang and data.platform == GEnums.I18nPlatform.CT then
                        patchText = data.text
                        break
                    end
                end
            end
            if patchText ~= '' then
                if BEYOND_DEBUG_COMMAND then
                    Notify(MessageConst.SHOW_TOAST, string.format("使用了文本热更表里的热更文本 id(%d)text(%s)", id, patchText))
                end
                return patchText
            end
        end

        local found, i18nText = Tables.i18nTextTable:TryGetValue(id)
        if not found or i18nText == nil or i18nText == '' then
            if BEYOND_DEBUG_COMMAND then
                local outText = ''
                if UNITY_EDITOR then
                    local openVisualText = CS.Beyond.I18n.I18nUtils.fakeI18nVisualTextEditorOnly
                    if openVisualText then
                        outText = CS.Beyond.I18n.I18nUtils.GetI18nVisualText(id, text)
                    end
                end
                if string.isEmpty(outText) then
                    outText = string.format("<color=red>!!ERROR!!I18N_NOT_FOUND:%X</color>", id)
                end
                return outText
            else
                return ''
            end
        end

        return i18nText
    end
end
LuaSpark.SetI18nGetTextFunc(I18nGetText, Types.I18nText)

function LuaCfg.intToEnum(enumType, value)
    return CS.System.Enum.ToObject(enumType, value)
end


local function Enum2Userdata(enumVal, enumName)
    local csEnum = GEnums[enumName]
    if csEnum == nil then
        return enumVal
    else
        return csEnum.__CastFrom(enumVal)
    end
end
local function Userdata2Enum(csEnum)
    if csEnum == nil then
        return nil
    else
        return csEnum:GetHashCode()
    end
end
LuaSpark.SetExternalEnumConvertFunc(Enum2Userdata, Userdata2Enum)




function LuaCfg.GetType(object)
    local type, typeName = LuaSpark.GetBeanType(object)
    if type ~= nil then
        rawset(Types, typeName, type)
        rawset(Types, type, typeName)
        return type, typeName
    else
        return nil
    end
end


function LuaCfg.GetEntityTypeName(entity)
    return LuaSpark.GetEntityType(entity)
end


function LuaCfg.NameOfType(type)
    return rawget(Types, type)
end


function LuaCfg.IsBean(object)
    return LuaSpark.IsBean(object)
end


function LuaCfg.IsArray(object)
    return LuaSpark.IsArray(object)
end


function LuaCfg.IsMap(object)
    return LuaSpark.IsMap(object)
end


function LuaCfg.DebugString(object)
    return LuaSpark.DebugString(object)
end


function LuaCfg.ToVector3(bean)
    if bean then
        return UnityEngine.Vector3(bean.x, bean.y, bean.z)
    else
        return UnityEngine.Vector3.zero
    end
end


function LuaCfg.ToVector2(bean)
    if bean then
        return UnityEngine.Vector2(bean.x, bean.y)
    else
        return UnityEngine.Vector2.zero
    end
end


function LuaCfg.Contains(arrayOrMap, target)
    if not (LuaSpark.IsMap(arrayOrMap) or LuaSpark.IsArray(arrayOrMap)) then
        return false
    end

    for _, v in pairs(arrayOrMap) do
        if v == target then
            return true
        end
    end
    return false
end

return LuaCfg