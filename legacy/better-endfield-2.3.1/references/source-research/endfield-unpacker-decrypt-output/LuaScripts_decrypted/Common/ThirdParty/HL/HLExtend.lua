
local ExternalTypeSys = HL.Class("ExternalTypeSys", HL.ExternalTypeSystem)
ExternalTypeSys.IsExternalType = HL.Override(HL.Any).Return(HL.Opt(HL.String)) << function(self, typeToTest)
    local luaType = type(typeToTest)

    local csType = luaType == 'table' and typeof(typeToTest)
    if csType then
        return csType.Name
    end

    local tblType = luaType == 'userdata' and Cfg.NameOfType(typeToTest)
    if tblType then
        return tblType
    end

    return nil
end

ExternalTypeSys.IsExternalInstance = HL.Override(HL.Userdata, HL.Any).Return(HL.Boolean) << function(self, instanceToTest, typeToTest)
    local luaType = type(typeToTest)

    local csType = luaType == 'table' and typeof(typeToTest)
    if csType then
        local instanceType = instanceToTest:GetType()
        return instanceType == csType or instanceType:IsSubclassOf(csType)
    end

    local tblType = luaType == 'userdata' and Cfg.GetType(instanceToTest)
    if tblType then
        return tblType == typeToTest
    end

    return false
end

HL.Commit(ExternalTypeSys)

if hg.isReloading then
    return
end

ExternalTypeSys():Register()
