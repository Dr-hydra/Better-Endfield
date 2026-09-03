local logger = {}


local DLogger = CS.Beyond.DLogger
local Log = DLogger.Log
local LogWarning = DLogger.LogWarning
local LogError = DLogger.LogError
local LogCritical = DLogger.LogCritical
local LogImportant = DLogger.LogImportant
local defaultChannel = CS.Beyond.ELogChannel.UI
local channelType = typeof(CS.Beyond.ELogChannel)
_G.__LogError__ = LogError

local isInfoOrWarnEnabled = DEVELOPMENT_BUILD or UNITY_EDITOR

local getStr = function(...)
    local count = select('#', ...)
    local args = {...}
    local newArgs = {}
    for k = 1, count do
        v = args[k]
        if type(v) == "table" then
            v = realInspect(v)
        end
        newArgs[k] = v == nil and "nil" or tostring(v)
    end
    return table.concat(newArgs, "\t")
end

function getLogChannelAndStr(channel, ...)
    if channel and type(channel) == "userdata" and channel:GetType() == channelType then
        return channel, getStr(...)
    else
        return defaultChannel, getStr(channel, ...)
    end
end

function logger.editorInfo(channel, ...)

    if UNITY_EDITOR then
        local logChannel, str = getLogChannelAndStr(channel, ...)
        Log(logChannel, str)
    end
end

function logger.info(channel, ...)
    if isInfoOrWarnEnabled then
        local logChannel, str = getLogChannelAndStr(channel, ...)
        Log(logChannel, str)
    end
end

function logger.important(enableLogType, channel, ...)
    local logChannel, str = getLogChannelAndStr(channel, ...)
    LogImportant(enableLogType, logChannel, str)
end

function logger.warn(channel, ...)
    if isInfoOrWarnEnabled then
        local logChannel, str = getLogChannelAndStr(channel, ...)
        LogWarning(logChannel, str)
    end
end

function logger.error(channel, ...)
    local logChannel, str = getLogChannelAndStr(channel, ...)
    str = string.format("%s\n%s", str, debug.traceback(nil, 2))
    LogError(logChannel, str)
end

function logger.critical(channel, ...)
    local logChannel, str = getLogChannelAndStr(channel, ...)
    str = string.format("%s\n%s", str, debug.traceback(nil, 2))
    LogCritical(logChannel, str)
end

return logger
