local minHeapClass = require_ex("Common/Utils/DataStructure/MinHeap")

TimerManager = HL.Class('TimerManager')

TimerManager.m_heap = HL.Field(HL.Forward("MinHeap"))

TimerManager.m_unscaledHeap = HL.Field(HL.Forward("MinHeap"))

TimerManager.m_frameHeap = HL.Field(HL.Forward("MinHeap"))

TimerManager.m_removedTimerKeys = HL.Field(HL.Table)

TimerManager.m_groupTimerKeys = HL.Field(HL.Table)

TimerManager.m_nextTimerKey = HL.Field(HL.Number) << 1

TimerManager.m_frameCount = HL.Field(HL.Number) << 0

TimerManager.m_requestCache = HL.Field(HL.Forward("Stack"))

TimerManager.m_timerStackTraces = HL.Field(HL.Table)

TimerManager.m_reachedTimers = HL.Field(HL.Table)


TimerManager.TimerManager = HL.Constructor() << function(self)
    self.m_heap = minHeapClass()
    self.m_unscaledHeap = minHeapClass()
    self.m_frameHeap = minHeapClass()
    self.m_requestCache = require_ex("Common/Utils/DataStructure/Stack")()
    self.m_removedTimerKeys = {}
    self.m_groupTimerKeys = setmetatable({}, { __mode = "k" })
    self.m_timerStackTraces = {}
    self.m_reachedTimers = {}
    self.m_nextTimerKey = 1
    self.m_frameCount = 0
    LuaUpdate:Add("Tick", function(deltaTime)
        self:Tick(deltaTime)
    end)
end

TimerManager.Tick = HL.Method(HL.Number) << function(self, deltaTime)
    self.m_frameCount = self.m_frameCount + 1
    self:_UpdateTimers(self.m_heap, Time.time)
    self:_UpdateTimers(self.m_unscaledHeap, Time.unscaledTime)
    self:_UpdateTimers(self.m_frameHeap, self.m_frameCount)
end

TimerManager._UpdateTimers = HL.Method(HL.Forward("MinHeap"), HL.Number) << function(self, heap, time)
    local reachedTimers = self.m_reachedTimers
    local reachedCount = 0
    while heap:Size() > 0 do
        local request, triggerTime = heap:Min()
        if self.m_removedTimerKeys[request.key] then
            self.m_removedTimerKeys[request.key] = nil
            heap:Pop()
            if ENABLE_PROFILER then
                self.m_timerStackTraces[request.key] = nil
            end
            self:_CacheRequest(request)
        else
            if time >= triggerTime then
                heap:Pop()
                reachedCount = reachedCount + 1
                reachedTimers[reachedCount] = request
            else
                break
            end
        end
    end
    for i = 1, reachedCount do
        self:_ExecuteTimer(reachedTimers[i])
        reachedTimers[i] = nil
    end
end

TimerManager._ExecuteTimer = HL.Method(HL.Table) << function(self, request)
    if ENABLE_PROFILER then
        local label = self.m_timerStackTraces[request.key]
        if label and unity_sample and unity_sample.begin_unity_sample then
            unity_sample.begin_unity_sample(label)
        end
    end
    local succ, log = xpcall(request.action, debug.traceback)
    if not succ then
        logger.critical("Timer Action Fail\n", log)
    end
    if ENABLE_PROFILER then
        if unity_sample and unity_sample.end_unity_sample then
            unity_sample.end_unity_sample()
        end
        self.m_timerStackTraces[request.key] = nil
    end
    self:_CacheRequest(request)
end

TimerManager.StartFrameTimer = HL.Method(HL.Number, HL.Function, HL.Opt(HL.Any)).Return(HL.Number)
<< function(self, frameNum, action, groupKey)
    local triggerTime = self.m_frameCount + frameNum
    local heap = self.m_frameHeap
    return self:_StartTimer(heap, triggerTime, action, groupKey)
end

TimerManager.StartTimer = HL.Method(HL.Number, HL.Function, HL.Opt(HL.Boolean, HL.Any)).Return(HL.Number)
<< function(self, time, action, unscaled, groupKey)
    local triggerTime = (unscaled and Time.unscaledTime or Time.time) + time
    local heap = unscaled and self.m_unscaledHeap or self.m_heap
    return self:_StartTimer(heap, triggerTime, action, groupKey)
end

TimerManager.GetTimerTriggerTime = HL.Method(HL.Number).Return(HL.Number) << function(self, timerId)
    for key, _ in self.m_heap:NodeIter() do
        local request = key.key
        if request.key == timerId then
            return request.triggerTime
        end
    end

    for key, _ in self.m_unscaledHeap:NodeIter() do
        local request = key.key
        if request.key == timerId then
            return request.triggerTime
        end
    end

    return -1
end

TimerManager._StartTimer = HL.Method(HL.Forward("MinHeap"), HL.Number, HL.Function, HL.Opt(HL.Any)).Return(HL.Number)
<< function(self, heap, triggerTime, action, groupKey)
    local key = self.m_nextTimerKey
    self.m_nextTimerKey = self.m_nextTimerKey + 1
    local request = self:_GetRequest()
    request.key = key
    request.action = action
    request.triggerTime = triggerTime
    request.groupKey = groupKey

    if ENABLE_PROFILER then
        self.m_timerStackTraces[key] = self:_GetCallerStackLabel()
    end

    heap:Add(request, triggerTime)

    if groupKey ~= nil then
        local keys = self.m_groupTimerKeys[groupKey]
        if not keys then
            keys = {}
            self.m_groupTimerKeys[groupKey] = keys
        end
        table.insert(keys, key)
    end

    return key
end

TimerManager.ClearTimer = HL.Method(HL.Number) << function(self, key)
    if key then
        self.m_removedTimerKeys[key] = true
        if ENABLE_PROFILER then
            self.m_timerStackTraces[key] = nil
        end
    end
end

TimerManager.ClearAllTimer = HL.Method(HL.Any) << function(self, groupKey)
    local keys = self.m_groupTimerKeys[groupKey]
    if keys then
        for _, k in pairs(keys) do
            self:ClearTimer(k)
        end
        self.m_groupTimerKeys[groupKey] = nil
    end
end

TimerManager._GetRequest = HL.Method().Return(HL.Table) << function(self)
    if self.m_requestCache:Empty() then
        return {}
    else
        return self.m_requestCache:Pop()
    end
end

TimerManager._CacheRequest = HL.Method(HL.Table) << function(self, request)
    request.action = nil
    request.groupKey = nil
    self.m_requestCache:Push(request)
end

TimerManager._GetCallerStackLabel = HL.Method().Return(HL.String) << function(self)
    local thisSource

    for level = 4, 12 do
        local info = debug.getinfo(level, "Sl")
        if info and info.what ~= "C" and info.source then
            local src = info.source
            if thisSource == nil or src ~= thisSource then
                local line = info.currentline or 0
                if src:sub(1,1) == "@" then src = src:sub(2) end
                local last = src:match("[^/\\]+$") or src
                return string.format("Timer:%s:%d", last, line)
            end
        end
    end
    return "Timer:Unknown"
end

HL.Commit(TimerManager)
return TimerManager