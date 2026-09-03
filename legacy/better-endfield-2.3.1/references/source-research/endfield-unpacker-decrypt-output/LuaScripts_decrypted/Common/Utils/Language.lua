local text = {}


local tmpText = {


}
setmetatable(text, {
    __index = function(_, k)
        local succ, data = Tables.textTable:TryGetValue(k)
        if succ then
            return data
        end

        if tmpText[k] and tmpText[k][1] then
            if BEYOND_DEBUG_COMMAND then
                if string.sub(k,1,#"LUA") ~= "LUA"
                    and string.sub(k,1,#"DATE_FORMAT") ~= "DATE_FORMAT"
                    and string.sub(k,1,#"TIME_FORMAT") ~= "TIME_FORMAT" then
                    logger.error("Language的Key需要是LUA_开头！key:" .. k)
                    return string.format("<color=red>!!ERROR!!Wrong Head for this text key. key: %s</color>", k)
                end
                if tmpText[k][2] then
                    local version = string.match(tmpText[k][2], "v%d+d%d+%d*d?")
                    if not version then
                        logger.error("Language的文本需要有版本信息（vXdX或vXdXdX）!key:" .. k)
                        return string.format("<color=red>!!ERROR!!No version info for this text key. key: %s</color>", k)
                    end
                else
                    logger.error("Language的文本需要有版本信息（vXdX或vXdXdX）!key:" .. k)
                    return string.format("<color=red>!!ERROR!!No version info for this text key. key: %s</color>", k)
                end
            end
            return tmpText[k][1]
        end

        if BEYOND_DEBUG_COMMAND then
            local error = string.format("Error: 表格中没有配置这个TextId：<color=#ff2121>%s</color>", k)
            logger.error(error)
            return error
        else
            return k
        end
    end
})

return text
