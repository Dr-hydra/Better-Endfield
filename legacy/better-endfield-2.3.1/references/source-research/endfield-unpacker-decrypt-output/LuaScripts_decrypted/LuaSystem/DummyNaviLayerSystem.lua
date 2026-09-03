local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')

DummyNaviLayerSystem = HL.Class('DummyNaviLayerSystem', LuaSystemBase.LuaSystemBase)

DummyNaviLayerSystem.m_dummyNaviLayers = HL.Field(HL.Table)

DummyNaviLayerSystem.DummyNaviLayerSystem = HL.Constructor() << function(self)
    self.m_dummyNaviLayers = {}
    self:RegisterMessage(MessageConst.ATTACH_DUMMY_NAVI_LAYER, function(key)
        self:_AttachNaviDummyLayer(key)
    end)
    self:RegisterMessage(MessageConst.DETACH_DUMMY_NAVI_LAYER, function(key)
        self:_DetachNaviDummyLayer(key)
    end)
end

DummyNaviLayerSystem._AttachNaviDummyLayer = HL.Method(HL.String) << function(self, key)
    if self.m_dummyNaviLayers[key] ~= nil then
        local dummyLayer = self.m_dummyNaviLayers[key]
        dummyLayer:NaviToThisGroup()
        return
    end
    local dummyLayerObj = UIManager:CreateNaviDummLayerObj()
    local dummyLayer = dummyLayerObj.transform:GetComponent("UISelectableNaviGroup")
    self.m_dummyNaviLayers[key] = dummyLayer
    dummyLayerObj.name = key
    dummyLayer.isDummy = true
    dummyLayer:NaviToThisGroup()
end

DummyNaviLayerSystem._DetachNaviDummyLayer = HL.Method(HL.String) << function(self, key)
    if self.m_dummyNaviLayers[key] == nil then
        return
    end
    local dummyLayer = self.m_dummyNaviLayers[key]
    GameObject.DestroyImmediate(dummyLayer.gameObject)
    self.m_dummyNaviLayers[key] = nil
end

DummyNaviLayerSystem.GetDummyNaviLayerByKey = HL.Method(HL.String).Return(HL.Userdata) << function(self, key)
    return self.m_dummyNaviLayers[key]
end

HL.Commit(DummyNaviLayerSystem)
return DummyNaviLayerSystem