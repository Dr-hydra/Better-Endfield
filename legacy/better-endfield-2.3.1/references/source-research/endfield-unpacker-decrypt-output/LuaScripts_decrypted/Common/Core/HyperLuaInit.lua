
_G.g_AssetsPath = CS.UnityEngine.Application.persistentDataPath
if UNITY_EDITOR or BEYOND_DEBUG then
    _G.g_IsBuildShipping = false
    if UNITY_EDITOR then
        _G.g_OverrideDefaultTypeCheckFlag = 2
    elseif DEVELOPMENT_BUILD then
        _G.g_OverrideDefaultTypeCheckFlag = 1
    else
        _G.g_OverrideDefaultTypeCheckFlag = 0
    end
    if CUSTOM_HL_LEVEL then
        _G.g_OverrideTypeCheckFlag = CUSTOM_HL_LEVEL
    end
else
    _G.g_IsBuildShipping = true
end

HL = require("Common/ThirdParty/HL/HL")
require_ex("Common/ThirdParty/HL/HLExtend")

















do
    local function postprocessStackTrace(stackTrace)
        local lines = {}

        for line in stackTrace:gmatch("[^\r\n]+") do
            table.insert(lines, line)
        end

        local processedLines = {}
        local lastHL = nil


        for i = #lines, 1, -1 do
            local line = lines[i]

            local matchHL = line:match("^.*Common/ThirdParty/HL/HL.lua:%d*: (.*)$")
            if matchHL == nil then

                if lastHL ~= nil then
                    line = line:gsub("in upvalue 'methodBody'", lastHL)
                    lastHL = nil
                end
                table.insert(processedLines, line)
            else

                lastHL = matchHL
            end
        end


        local count = #processedLines
        for i=1, math.floor(count / 2) do
            processedLines[i], processedLines[count - i + 1] = processedLines[count - i + 1], processedLines[i]
        end

        return table.concat(processedLines, "\n")
    end

    local originTraceback = debug.traceback
    debug.traceback = function(thread, message, level)
        local stackTrace
        if type(thread) == "thread" then
            stackTrace = originTraceback(thread, message, 1 + (level or 1))
        else

            stackTrace = originTraceback(thread, 1 + (message or 1))
        end
        return postprocessStackTrace(stackTrace)
    end
end

return HL
