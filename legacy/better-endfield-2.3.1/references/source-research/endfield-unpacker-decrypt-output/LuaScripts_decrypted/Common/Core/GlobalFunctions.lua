
hg = {
    loadedModules = {},
    loadedModuleNameList = {}
}

local ModuleType = {
    NoReturn = 1,
    ReturnTable = 2,
    ReturnUserData = 3,
}

function string.isEmpty(str)
    return str == nil or str == ''
end

function string.upperFirst(str)
    if not str or type(str) ~= "string" then
        return str
    end
    return (str:gsub("^%l", string.upper))
end

function string.lowerFirst(str)
    if not str or type(str) ~= "string" then
        return str
    end
    return (str:gsub("^%u", string.lower))
end

function string.startWith(str, prefix)
    if not str or not prefix then
        return str
    end
    return string.find(str, prefix, 1, true) == 1
end

function string.endWith(str, suffix)
    if not str or not suffix then
        return str
    end
    return suffix == "" or string.sub(str, -#suffix) == suffix
end


function string.toSnakeCase(str)
    if not str or type(str) ~= "string" then
        return str
    end
    local newStr =
        string.gsub(
        str:sub(2),
        "([%u%d]%l*)",
        function(ss)
            ss = "_" .. ss:lower()
            return ss
        end)
    return str:sub(1, 1):lower() .. newStr
end

function string.split(str, delimiter)
    if delimiter == "" then
        return false
    end

    local pos, arr = 0, {}

    for st, sp in function()
        return string.find(str, delimiter, pos, true)
    end do
        table.insert(arr, string.sub(str, pos, st - 1))
        pos = sp + 1
    end
    table.insert(arr, string.sub(str, pos))
    return arr
end

function string.splitByLine(str)
    local lines = {}
    for line in string.gmatch(str, "[^\r\n]+") do
        table.insert(lines, line)
    end
    return lines
end

function string.trim(str)
    str = string.gsub(str, "^[ \t\n\r]+", "")
    return string.gsub(str, "[ \t\n\r]+$", "")
end

function string.utf8len(s)
    local len = 0
    local i = 1
    local byte = string.byte
    while i <= #s do
        local c = byte(s, i)
        if c < 128 then
            i = i + 1
            len = len + 1
        elseif c < 192 then
            i = i + 1
        elseif c < 224 then
            i = i + 2
            len = len + 1
        elseif c < 240 then
            i = i + 3
            len = len + 1
        elseif c < 248 then
            i = i + 4
            len = len + 1
        elseif c < 252 then
            i = i + 5
            len = len + 1
        elseif c < 254 then
            i = i + 6
            len = len + 1
        end
    end
    return len
end

local function charSize(c)
    if c < 128 then
        return 1
    elseif c < 192 then
        return 1
    elseif c < 224 then
        return 2
    elseif c < 240 then
        return 3
    elseif c < 248 then
        return 4
    elseif c < 252 then
        return 5
    elseif c < 254 then
        return 6
    end
    logger.error("charSize error: " .. c)
    return 0
end

function string.utf8sub(str, startChar, numChars)
    local startIndex = 1
    while startChar > 1 do
        local char = string.byte(str, startIndex)
        startIndex = startIndex + charSize(char)
        startChar = startChar - 1
    end
    local currentIndex = startIndex

    while numChars > 0 and currentIndex <= #str do
        local char = string.byte(str, currentIndex)
        currentIndex = currentIndex + charSize(char)
        numChars = numChars - 1
    end
    return str:sub(startIndex, currentIndex - 1)
end

local RequireFailReason = {
    NoFile = 1,
    ReturnNotTable = 2,
    Else = 3,
}






local function real_require_ex(moduleName, isReload, globalEnv)
    globalEnv = globalEnv or _G

    local module = hg.loadedModules[moduleName]
    if not isReload and module then
        return module.result or module.env
    end

    local ret, msg
    if USING_VFS then
        local content = LuaManagerInst:LoadLua(moduleName)
        if content == nil then
            return nil, RequireFailReason.NoFile, "No Lua File"
        end
        ret, msg = loadstring(content, "@" .. moduleName)
    elseif not UNITY_EDITOR and USING_BUNDLE then
        local content = LuaManagerInst:LoadLua(moduleName)
        if content == nil then
            return nil, RequireFailReason.NoFile, "No Lua File"
        end
        ret, msg = loadstring(content, "@" .. moduleName)
    else
        ret, msg = loadfile(LuaManagerInst:GetLuaFileRealPath(moduleName))
    end

    if ret == nil then
        if string.find(msg, "No such file or directory") then
            return nil, RequireFailReason.NoFile, msg
        end
        return nil, RequireFailReason.Else, msg
    end

    if not module then

        local moduleEnv = setmetatable({}, {__index = globalEnv})
        local retFunc = setfenv(ret, moduleEnv)
        local succ, result = xpcall(retFunc, debug.traceback)
        if not succ then
            logger.critical(result)
            return
        end

        module = {
            name = moduleName,
            env = moduleEnv
        }
        if result then
            local typeStr = type(result)
            if typeStr == "table" then
                module.result = result
                module.type = ModuleType.ReturnTable
            elseif typeStr == "userdata" then
                module.result = result
                module.type = ModuleType.ReturnUserData
            else
                return nil, RequireFailReason.ReturnNotTable, "file can only return table or no return"
            end
        else
            module.type = ModuleType.NoReturn
        end
        hg.loadedModules[moduleName] = module
        hg.loadedModuleNameList[#hg.loadedModuleNameList + 1] = moduleName
    else

        local result = setfenv(ret, module.env)()
        if module.type ~= ModuleType.NoReturn then
            module.result = result
        end
    end

    return module.result or module.env
end

local function parsePath(path)
    if not path then
        return
    end


    path = path:gsub("/+", "/")


    while true do
        local count
        path, count = path:gsub("/[^/]-/%.%./", "/")
        if count == 0 then
            break
        end
    end

    return path
end

local function getLocalModuleName(moduleName, isFromImport)
    local info = debug.getinfo(isFromImport and 3 or 2)
    local curPath = info.short_src
    local pattern = "LuaScripts~/(.*/).*%.lua"
    if USING_VFS then
        pattern = "TextAsset/LuaScripts/(.*/).*%.lua"
    elseif not UNITY_EDITOR and USING_BUNDLE then
        pattern = "TextAsset/LuaScripts/(.*/).*%.lua"
    end
    local localModuleName
    _, _, localModuleName = curPath:find(pattern)
    if not localModuleName then
        _, _, localModuleName = curPath:find("(.*/).*")
    end

    if localModuleName then
        localModuleName = parsePath(localModuleName .. moduleName)
    end

    return localModuleName
end

function require_ex(moduleName, isReload, globalEnv, isFromImport)
    moduleName = parsePath(moduleName)
    local localModuleName
    local result, reason, msg = real_require_ex(moduleName, isReload, globalEnv)
    if result == nil and reason == RequireFailReason.NoFile then
        logger.info("module not exist:", moduleName, msg, "\ntry require using localModuleName", localModuleName)
        localModuleName = getLocalModuleName(moduleName, isFromImport)
        if localModuleName then
            result, reason, msg = real_require_ex(localModuleName, isReload, globalEnv)
        end
    end

    if result == nil then
        logger.error("require_ex fail", moduleName, isReload, reason, "\nerror message:", msg)
        return
    end

    return result
end

function require_check(moduleName)
    moduleName = parsePath(moduleName)
    if LuaManagerInst:IsLuaFileExist(moduleName) then
        return true
    end

    local localModuleName = getLocalModuleName(moduleName)
    return LuaManagerInst:IsLuaFileExist(localModuleName)
end

function addMetaIndex(t, index)
    local mt = getmetatable(t)
    if not mt then
        setmetatable(t, {__index = index})
        return
    end
    local oldIndex = mt.__index
    local oldIsTable = type(oldIndex) == "table"
    local newIsTable = type(index) == "table"
    mt.__index = function(tt, k)
        local value
        if oldIndex ~= nil then
            if oldIsTable then
                value = oldIndex[k]
            else
                value = oldIndex(tt, k)
            end
        end

        if value == nil then
            if newIsTable then
                value = index[k]
            else
                value = index(tt, k)
            end
        end
        return value
    end
end

function addMetaNewIndex(t, index)
    local mt = getmetatable(t)
    if not mt then
        setmetatable(t, {__newindex = index})
        return
    end
    local oldIndex = mt.__newindex
    local newIsTable = type(index) == "table"
    mt.__newindex = function(tt, k, value)
        if newIsTable then
            local action = index[k]
            if action then
                action(tt, k, value)
                return
            end
        else
            index(tt, k, value)
            return
        end

        if oldIndex then
            oldIndex(tt, k, value)
        else
            rawset(tt, k, value)
        end
    end
end

function import(moduleName)
    local module = require_ex(moduleName, nil, nil, true)
    local env = getfenv(2)
    addMetaIndex(env, module)
end

function refreshScripts()
    logger.info("refresh scripts .....")
    hg.isReloading = true
    HL.SetReloading(true)
    for _, k in ipairs(hg.loadedModuleNameList) do
        local succ, log = xpcall(require_ex, debug.traceback, k, true)
        if not succ then
            logger.critical(log)
        end
    end
    HL.SetReloading(false)
    hg.isReloading = false
end

function setmetatableindex(t, index)
    local mt = getmetatable(t)
    if not mt then
        mt = {}
    end
    if not mt.__index then
        mt.__index = index
        setmetatable(t, mt)
    elseif mt.__index ~= index then
        setmetatableindex(mt, index)
    end
end

function LuaIndex(index)
    if not index then
        return
    end
    return index + 1
end

function CSIndex(index)
    if not index then
        return
    end
    return index - 1
end

function isINF(value)
  return value == math.huge or value == -math.huge
end

function isNAN(value)
  return value ~= value
end

function isInvalidNumber(value)
  return value == nil or isINF(value) or isNAN(value)
end
