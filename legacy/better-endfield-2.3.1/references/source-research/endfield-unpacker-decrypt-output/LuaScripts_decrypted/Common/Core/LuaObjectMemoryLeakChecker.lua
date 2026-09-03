

LuaObjectMemoryLeakChecker = HL.Class('LuaObjectMemoryLeakChecker')

local GCMeta = {}

LuaObjectMemoryLeakChecker.destoryObjects = HL.Field(HL.Table)

LuaObjectMemoryLeakChecker.isGcEnd = HL.Field(HL.Boolean) << false

LuaObjectMemoryLeakChecker.coCheker = HL.Field(HL.Thread)

LuaObjectMemoryLeakChecker.LuaObjectMemoryLeakChecker = HL.Constructor() << function(self)
    self.destoryObjects = {}
    setmetatable(self.destoryObjects, {__mode = "k"})
end


LuaObjectMemoryLeakChecker.checkRegisterGC = HL.Method() << function(self)
    while true do
        coroutine.wait(2,self.coCheker)
        if self.isGcEnd then
            self:_DetectLuaObject()
            self:_ReStartChecker()
        end
    end
end

LuaObjectMemoryLeakChecker._ReStartChecker = HL.Method()<< function(self)
    if self.coCheker then
        CoroutineManager:ClearCoroutine(self.coCheker)
        self.coCheker = nil
    end
    self:_createGCListenerTable()
end

LuaObjectMemoryLeakChecker._createGCListenerTable = HL.Method()<< function(self)
    if self.coCheker ~= nil then
        return
    end

    local coCheker = CoroutineManager:StartCoroutine(function()
        LuaObjectMemoryLeakChecker.checkRegisterGC(self)
    end)

    local instance = {}
    self.isGcEnd = false
    GCMeta.__gc =  function()
        if(coCheker == self.coCheker) then
            self.isGcEnd  = true
        end
    end
    setmetatable(instance,GCMeta)
    self.coCheker = coCheker
end

LuaObjectMemoryLeakChecker.AddDetectLuaObject = HL.Method(HL.Any) << function(self, object)
    local GetUserdataAddr = nil
    local address = GetUserdataAddr ~= nil and tostring(GetUserdataAddr(object)) or tostring(object)
    local typeinfo = type(object) == "userdata" and HyperLua.GetTypeName(object) or type(object)
    local time = DateTimeUtils.GetCurrentTimestampByMilliseconds()
    local info = "memory leak  address:"..address.." type ："..typeinfo.." start time: "..time
    self.destoryObjects[object] =
    {
        [1] = time,
        [2] = info,
        [3] = address
    }
    self:_ReStartChecker()
end

LuaObjectMemoryLeakChecker.RemoveDetectLuaObject = HL.Method(HL.Any) << function(self, obj)
    self.destoryObjects[obj] = nil
end


LuaObjectMemoryLeakChecker._DetectLuaObject = HL.Method() << function(self)
    local curTime = DateTimeUtils.GetCurrentTimestampByMilliseconds()
    logger.warn("LuaObjectMemoryLeakChecker finish")
    for object, infos in pairs(self.destoryObjects) do
        if object ~= nil then
            local time = curTime - infos[1]
            logger.error(ELogChannel.UI, infos[2].." duration time:"..time)
            local mri = require ("Debug/MemoryReferenceInfo")

            mri.m_cConfig.m_bAllMemoryRefFileAddTime = false
            mri.m_cConfig.m_bSingleMemoryRefFileAddTime = false
            mri.m_cConfig.m_bComparedMemoryRefFileAddTime = false
        end
    end
end


LuaObjectMemoryLeakChecker.DumpAllLeakObject = HL.Method() << function(self)
    collectgarbage("collect")
    collectgarbage("collect")
    local curTime = DateTimeUtils.GetCurrentTimestampByMilliseconds()
    local fireName = CS.Beyond.VFS.UnityFileLoaderHelper.persistentDataPath.."/".."lua_memory_leak_dump_file_"..curTime..".txt"
    logger.error(ELogChannel.UI, "dump lua file "..fireName)
    local mri = require ("Debug/MemoryReferenceInfo")
    mri.m_cConfig.m_bAllMemoryRefFileAddTime = false
    mri.m_cConfig.m_bSingleMemoryRefFileAddTime = false
    mri.m_cConfig.m_bComparedMemoryRefFileAddTime = false
    for object, infos in pairs(self.destoryObjects) do
        if object ~= nil then
            mri.m_cMethods.DumpMemorySnapshotSingleObject(fireName,"", -1, infos[3], object)
        end
    end
end


HL.Commit(LuaObjectMemoryLeakChecker)
return LuaObjectMemoryLeakChecker



