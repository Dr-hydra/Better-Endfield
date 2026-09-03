local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')

LevelWorldUISystem = HL.Class('LevelWorldUISystem', LuaSystemBase.LuaSystemBase)

LevelWorldUISystem.m_activeLevelWorldUis = HL.Field(HL.Table)

LevelWorldUISystem.LevelWorldUISystem = HL.Constructor() << function(self)
    self.m_activeLevelWorldUis = {}

    self:RegisterMessage(MessageConst.ON_LEVEL_WORLD_UIS_LOADED, function(args)

        local readyUiInfos

        local readyUiModels
        readyUiInfos, readyUiModels = unpack(args)
        for key, readyUiInfo in pairs(readyUiInfos) do
            local readyUiModel = readyUiModels[key]

            local levelWorldUi = Utils.wrapLuaNode(readyUiModel)
            self.m_activeLevelWorldUis[readyUiInfo.globalId] = levelWorldUi
            if levelWorldUi.InitLevelWorldUi then
                levelWorldUi:InitLevelWorldUi(readyUiInfo.args)
            end
        end
    end)

    self:RegisterMessage(MessageConst.ON_LEVEL_WORLD_UIS_RELEASED, function(args)

        local releasedUiIds = unpack(args)
        for _, id in pairs(releasedUiIds) do
            self:_ReleaseLevelUi(id)
        end
    end)
end

LevelWorldUISystem._ReleaseLevelUi = HL.Method(HL.Number) << function(self, levelUiId)

    local toRelease = self.m_activeLevelWorldUis[levelUiId]
    if not toRelease then
        return
    end
    if toRelease.OnLevelWorldUiReleased then
        toRelease:OnLevelWorldUiReleased()
    end
    CSUtils.ClearUIComponents(toRelease.gameObject)
    self.m_activeLevelWorldUis[levelUiId] = nil
end

LevelWorldUISystem.OnRelease = HL.Override() << function(self)

    for id, _ in pairs(self.m_activeLevelWorldUis) do
        self:_ReleaseLevelUi(id)
    end
end

HL.Commit(LevelWorldUISystem)
return LevelWorldUISystem