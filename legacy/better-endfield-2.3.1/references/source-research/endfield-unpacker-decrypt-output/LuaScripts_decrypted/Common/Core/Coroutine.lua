local create = coroutine.create
local running = coroutine.running
local resume = coroutine.resume
local yield = coroutine.yield
local getinfo = debug.getinfo
local corTimers = {}
local corUpdates = {}
local corInRequest = {}
local stoppedCor = {}
local waitStepCors = {}
local waitStepCorCount = 0

local corToDebugInfo = {}
setmetatable(corToDebugInfo, {
    __mode = "kv"
})

setmetatable(stoppedCor, {
    __mode = "k"
})

local emptyFun = function() end
local begin_unity_sample = emptyFun
local end_unity_sample = emptyFun
local getFileInfo = emptyFun

if unity_sample and unity_sample.begin_unity_sample then

    if DEVELOPMENT_BUILD or UNITY_EDITOR then
        getFileInfo = function (co)
            local info = getinfo(4,"Sl")
            corToDebugInfo[co] = (info.short_src..":"..info.currentline):match("([^\\/]+)$")
        end

        begin_unity_sample = unity_sample.begin_unity_sample
        end_unity_sample = unity_sample.end_unity_sample
    end
end
function coroutine.start(f)
    local co = create(f)
    getFileInfo(co)
    if running() == nil then

        begin_unity_sample(corToDebugInfo[co])
        local flag, msg = resume(co)
        end_unity_sample()
        if not flag then
            logger.error(debug.traceback(co, msg), corToDebugInfo[co])
        end
    else

        local action = function()
            if not corTimers[co] then

                return
            end
            corTimers[co] = nil
            begin_unity_sample(corToDebugInfo[co])
            local flag, msg = resume(co)
            end_unity_sample()
            if not flag then
                logger.error(debug.traceback(co, msg), corToDebugInfo[co])
            end
        end

        local timer = TimerManager:StartFrameTimer(0, action)
        corTimers[co] = timer
    end

    return co
end

function coroutine.wait(t, co)
    co = co or running()
    if stoppedCor[co] then
        return yield()
    end

    local action = function()
        if not corTimers[co] then

            return
        end
        corTimers[co] = nil
        begin_unity_sample(corToDebugInfo[co])
        local flag, msg = resume(co)
        end_unity_sample()
        if not flag then
            logger.error(debug.traceback(co, msg), corToDebugInfo[co])
            return
        end
    end

    local timer = TimerManager:StartTimer(t, action)
    corTimers[co] = timer
    return yield()
end

local stepUpdateKey

function coroutine.step(co)
    co = co or running()
    if stoppedCor[co] then
        return yield()
    end

    if not stepUpdateKey then
        stepUpdateKey = LuaUpdate:Add("Tick", coroutine._stepUpdate)
    end

    waitStepCorCount = waitStepCorCount + 1
    waitStepCors[waitStepCorCount] = co
    return yield()
end

function coroutine.waitForRenderDone(co)




    co = co or running()
    if stoppedCor[co] then
        return yield()
    end

    local key = LuaUpdate:Add("RenderDone", function()
        LuaUpdate:Remove(corUpdates[co])
        corUpdates[co] = nil

        begin_unity_sample(corToDebugInfo[co])
        local flag, msg = resume(co)
        end_unity_sample()
        if not flag then
            logger.error(debug.traceback(co, msg), corToDebugInfo[co])
        end

        return true
    end)
    corUpdates[co] = key

    return yield()
end

function coroutine._stepUpdate()

    local count = waitStepCorCount
    if count == 0 then
        return
    end
    waitStepCorCount = 0
    local cors = waitStepCors
    waitStepCors = {}
    for k = 1, count do
        local stepCo = cors[k]
        if stepCo and not stoppedCor[stepCo] then
            begin_unity_sample(corToDebugInfo[stepCo])
            local flag, msg = resume(stepCo)
            end_unity_sample()
            if not flag then
                logger.error(debug.traceback(stepCo, msg), corToDebugInfo[stepCo])
            end
        end
    end
end

function coroutine.stop(co)
    stoppedCor[co] = true
    local timer = corTimers[co]
    if timer ~= nil then
        TimerManager:ClearTimer(timer)
        corTimers[co] = nil
    end
    corInRequest[co] = nil
    local updateKey = corUpdates[co]
    if updateKey then
        LuaUpdate:Remove(updateKey)
    end
    for k = 1, waitStepCorCount do
        if waitStepCors[k] == co then
            waitStepCors[k] = nil
        end
    end
end


function coroutine.waitAsyncRequest(request, co)
    co = co or running()
    if stoppedCor[co] then
        return yield()
    end

    corInRequest[co] = true

    local action = function()
        if not corInRequest[co] then

            return
        end

        corInRequest[co] = nil
        corTimers[co] = nil

        begin_unity_sample(corToDebugInfo[co])
        local flag, msg = resume(co)
        end_unity_sample()

        if not flag then
            logger.error(debug.traceback(co, msg), corToDebugInfo[co])
            return
        end
    end

    local timer = TimerManager:StartFrameTimer(0, function()
        request(action)
    end)
    corTimers[co] = timer
    return yield()
end

coroutine.Tick = -1
coroutine.LateTick = -2
coroutine.TailTick = -3

function coroutine.waitCondition(condition, checkInterval, co)
    co = co or running()
    if stoppedCor[co] then
        return yield()
    end

    checkInterval = checkInterval or coroutine.Tick
    local tickName
    if checkInterval == coroutine.Tick then
        tickName = "Tick"
    elseif checkInterval == coroutine.LateTick then
        tickName = "LateTick"
    elseif checkInterval == coroutine.TailTick then
        tickName = "TailTick"
    end

    local key = LuaUpdate:Add(tickName, function()
        if condition() then
            LuaUpdate:Remove(corUpdates[co])
            corUpdates[co] = nil

            begin_unity_sample(corToDebugInfo[co])
            local flag, msg = resume(co)
            end_unity_sample()
            if not flag then
                logger.error(debug.traceback(co, msg), corToDebugInfo[co])
            end

            return true
        end
    end)
    corUpdates[co] = key

    return yield()
end
