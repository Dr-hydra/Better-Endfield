local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')
local LuaResourceLoader = require_ex('Common/Utils/LuaResourceLoader')
MapResourceSystem = HL.Class('MapResourceSystem', LuaSystemBase.LuaSystemBase)

MapResourceSystem.m_resourceLoader = HL.Field(HL.Forward("LuaResourceLoader"))

MapResourceSystem.m_singleMarkIconCache = HL.Field(HL.Table)

MapResourceSystem.m_markDynamicNodePrefabCache = HL.Field(HL.Table)

MapResourceSystem.m_markObjPrefabCache = HL.Field(HL.Table)

MapResourceSystem.m_markObjCache = HL.Field(HL.Table)

MapResourceSystem.MapResourceSystem = HL.Constructor() << function(self)
end

MapResourceSystem.OnInit = HL.Override() << function(self)
    self.m_resourceLoader = LuaResourceLoader.LuaResourceLoader()
    self.m_singleMarkIconCache = {}
    self.m_markDynamicNodePrefabCache = {}
    self.m_markObjPrefabCache = {}
    self.m_markObjCache = {}
    self:_LoadMarkDynamicNodePrefabs()
end

MapResourceSystem.OnRelease = HL.Override() << function(self)
    self.m_resourceLoader:DisposeAllHandles()
end

MapResourceSystem._LoadMarkDynamicNodePrefabs = HL.Method() << function(self)
    local prefabRootPath = MapConst.MARK_DYNAMIC_NODE_PREFAB_ROOT_PATH
    for prefabKey, prefabPath in pairs(MapConst.MARK_DYNAMIC_NODE_PREFAB_PATH_CONFIG) do
        local fullPath = string.format("%s%s.prefab", prefabRootPath, prefabPath)
        self.m_markDynamicNodePrefabCache[prefabKey] = self.m_resourceLoader:LoadGameObject(fullPath)
    end
end



MapResourceSystem.GetSingleMarkIconSprite = HL.Method(HL.String).Return(HL.Userdata) << function(self, iconName)
    if self.m_singleMarkIconCache[iconName] == nil then
        local sprite = self.m_resourceLoader:LoadSprite(UIUtils.getSpritePath(UIConst.UI_SPRITE_MAP_MARK_ICON, iconName))
        self.m_singleMarkIconCache[iconName] = sprite
    end
    return self.m_singleMarkIconCache[iconName]
end

MapResourceSystem.GetMarkDynamicNodePrefab = HL.Method(HL.String).Return(HL.Userdata) << function(self, prefabKey)
    return self.m_markDynamicNodePrefabCache[prefabKey]
end




HL.Commit(MapResourceSystem)
return MapResourceSystem
