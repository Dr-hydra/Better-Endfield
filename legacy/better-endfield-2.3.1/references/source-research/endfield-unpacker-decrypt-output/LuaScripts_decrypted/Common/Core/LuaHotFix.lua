local LuaHotFix = {}

local hotFixIndex = 0

local function nextChunkName()
    hotFixIndex = hotFixIndex + 1
    return string.format("@LuaHotFix_%d", hotFixIndex)
end

local function setReloading(hgReloading, hlReloading)
    if hg then
        hg.isReloading = hgReloading
    end
    if HL and HL.SetReloading then
        HL.SetReloading(hlReloading)
    end
end

function LuaHotFix.Apply(luaCode, chunkName)
    if type(luaCode) ~= "string" or luaCode == "" then
        local err = "LuaHotFix.Apply: empty lua code"
        logger.error(err)
        return false, err
    end

    local chunk, loadErr = loadstring(luaCode, chunkName or nextChunkName())
    if not chunk then
        logger.error("LuaHotFix load failed", loadErr)
        return false, loadErr
    end

    setfenv(chunk, _G)

    local oldReloading = hg and hg.isReloading or false
    local oldHLReloading = HL and HL.IsReloading and HL.IsReloading() or false

    setReloading(true, true)
    local ok, result = xpcall(chunk, debug.traceback)
    setReloading(oldReloading, oldHLReloading)

    if not ok then
        logger.error("LuaHotFix execute failed", result)
        return false, result
    end

    logger.info("LuaHotFix execute success", chunkName or hotFixIndex)
    return true, result
end

return LuaHotFix
