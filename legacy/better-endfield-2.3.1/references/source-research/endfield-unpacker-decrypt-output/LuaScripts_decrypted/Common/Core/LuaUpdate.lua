

local LuaUpdateGroupClass = require_ex('Common/Core/LuaUpdateGroup')

LuaUpdate = HL.Class('LuaUpdate')


LuaUpdate.m_groups = HL.Field(HL.Table)

LuaUpdate.m_keyToUpdateName = HL.Field(HL.Table)

LuaUpdate.m_nextKey = HL.Field(HL.Number) << 1


LuaUpdate.LuaUpdate = HL.Constructor() << function(self)
    local updateNames = {
        "Tick",
        "LateTick",
        "TailTick",
        "RenderDone",
    }

    self.m_groups = {}
    self.m_nextKey = 1
    self.m_keyToUpdateName = {}
    for _, name in ipairs(updateNames) do
        self.m_groups[name] = LuaUpdateGroupClass(name)
        local csName = "action" .. name
        LuaManagerInst[csName]:AddListener(function(deltaTime)
            self:_ExecActions(name, deltaTime)
        end)
    end
end

LuaUpdate._GetGroup = HL.Method(HL.String).Return(LuaUpdateGroupClass) << function(self, updateName)

    return self.m_groups[updateName]
end

LuaUpdate._ExecActions = HL.Method(HL.String, HL.Number) << function(self, updateName, deltaTime)
    local group = self:_GetGroup(updateName)
    group:_ExecActions(deltaTime)
end

LuaUpdate.Add = HL.Method(HL.String, HL.Function, HL.Opt(HL.Boolean)).Return(HL.Opt(HL.Number)) << function(self, updateName, action, useTimeSlice)
    local group = self:_GetGroup(updateName)
    local key = self.m_nextKey
    self.m_nextKey = self.m_nextKey + 1
    group:Add(key, action, useTimeSlice)
    self.m_keyToUpdateName[key] = updateName
    return key
end

LuaUpdate.Remove = HL.Method(HL.Opt(HL.Number)).Return(HL.Number) << function(self, key)
    if not key or key == -1 then
        return -1
    end

    local updateName = self.m_keyToUpdateName[key]
    if not updateName then

        return -1
    end

    self.m_keyToUpdateName[key] = nil
    local group = self:_GetGroup(updateName)
    group:Remove(key)
    return -1
end


LuaUpdate.GetDebugInfo = HL.Method().Return(HL.String) << function(self)
    local infos = {}
    for k, v in pairs(self.m_groups) do
        table.insert(infos, string.format("%s\t\tm_bindingActions: %d\tm_bindingTimeSliceActions: %d", k, lume.count(v.m_bindingActions) ,#v.m_bindingTimeSliceActions))
    end
    return table.concat(infos, "\n")
end


HL.Commit(LuaUpdate)
return LuaUpdate
