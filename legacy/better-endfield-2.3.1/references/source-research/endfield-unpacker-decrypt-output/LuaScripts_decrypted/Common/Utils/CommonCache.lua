CommonCache = HL.Class('CommonCache')

CommonCache.m_cacheStack = HL.Field(HL.Forward("Stack"))

CommonCache.m_createFunc = HL.Field(HL.Function)

CommonCache.m_onUse = HL.Field(HL.Function)

CommonCache.m_onCache = HL.Field(HL.Function)

CommonCache.CommonCache = HL.Constructor(HL.Function, HL.Opt(HL.Function, HL.Function))
<< function(self, createFunc, onUse, onCache)
    self.m_cacheStack = require_ex("Common/Utils/DataStructure/Stack")()
    self.m_createFunc = createFunc
    self.m_onUse = onUse
    self.m_onCache = onCache
end

CommonCache.Get = HL.Method().Return(HL.Any) << function(self)
    local cell
    if self.m_cacheStack:Count() > 0 then
        cell = self.m_cacheStack:Pop()
    else
        cell = self.m_createFunc()
    end
    if self.m_onUse then
        self.m_onUse(cell)
    end
    return cell
end

CommonCache.Cache = HL.Method(HL.Any) << function (self, cell)
    if self.m_onCache then
        self.m_onCache(cell)
    end
    self.m_cacheStack:Push(cell)
end

HL.Commit(CommonCache)
return CommonCache
