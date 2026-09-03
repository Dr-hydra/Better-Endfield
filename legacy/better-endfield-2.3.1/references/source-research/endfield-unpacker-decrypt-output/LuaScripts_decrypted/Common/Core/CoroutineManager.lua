

CoroutineManager = HL.Class('CoroutineManager')

CoroutineManager.m_defaultGroupKey = HL.Field(HL.Table)

CoroutineManager.m_coroutineGroups = HL.Field(HL.Table)

CoroutineManager.m_coroutineKeyMap = HL.Field(HL.Table)

CoroutineManager.m_parentsInfo = HL.Field(HL.Table)

CoroutineManager.CoroutineManager = HL.Constructor() << function(self)
    self.m_defaultGroupKey = {}
    self.m_coroutineGroups = setmetatable({}, { __mode = "k" })
    self.m_coroutineKeyMap = setmetatable({}, { __mode = "kv" })
    self.m_parentsInfo = setmetatable({}, { __mode = "k" })
end

CoroutineManager.StartCoroutine = HL.Method(HL.Function, HL.Opt(HL.Any)).Return(HL.Thread) << function(self, action, groupKey)
    local co = coroutine.start(action)

    groupKey = groupKey or self.m_defaultGroupKey
    local map = self.m_coroutineGroups[groupKey]
    if not map then
        map = setmetatable({}, { __mode = "k" })
        self.m_coroutineGroups[groupKey] = map
    end
    self.m_coroutineKeyMap[co] = groupKey
    map[co] = true

    return co
end

CoroutineManager.ClearCoroutine = HL.Method(HL.Opt(HL.Thread)) << function(self, co)
    if co == nil then
        return
    end

    local groupKey = self.m_coroutineKeyMap[co]
    self.m_coroutineKeyMap[co] = nil
    if groupKey then
        local map = self.m_coroutineGroups[groupKey]
        if map then
            map[co] = nil
        end
    end
    coroutine.stop(co)
end

CoroutineManager.IsCorCleared = HL.Method(HL.Thread).Return(HL.Boolean) << function(self, co)

    if not co then
        return true
    end

    return not self.m_coroutineKeyMap[co]
end

CoroutineManager.ClearAllCoroutine = HL.Method(HL.Any, HL.Opt(HL.Table)) << function(self, groupKey, checkedKeys)
    checkedKeys = checkedKeys or {}
    if checkedKeys[groupKey] then
        return
    end

    local map = self.m_coroutineGroups[groupKey]
    if map then
        for co, _ in pairs(map) do
            self.m_coroutineKeyMap[co] = nil
            coroutine.stop(co)
        end
        self.m_coroutineGroups[groupKey] = nil
    end
    checkedKeys[groupKey] = true

    local sonMap = self.m_parentsInfo[groupKey]
    if sonMap then
        for k, _ in pairs(sonMap) do
            self:ClearAllCoroutine(k, checkedKeys)
        end
    end
end

CoroutineManager.RegisterParent = HL.Method(HL.Any, HL.Any) << function(self, sonGroupKey, parentGroupKey)
    if sonGroupKey == nil or parentGroupKey == nil then
        return
    end

    local map = self.m_parentsInfo[parentGroupKey]
    if not map then
        map = setmetatable({}, { __mode = "k" })
        self.m_parentsInfo[parentGroupKey] = map
    end
    map[sonGroupKey] = true
end

CoroutineManager.UnregisterParent = HL.Method(HL.Any, HL.Any) << function(self, sonGroupKey, parentGroupKey)
    if sonGroupKey == nil or parentGroupKey == nil then
        return
    end

    local map = self.m_parentsInfo[parentGroupKey]
    if not map then
        return
    end
    map[sonGroupKey] = nil
end

HL.Commit(CoroutineManager)
return CoroutineManager
