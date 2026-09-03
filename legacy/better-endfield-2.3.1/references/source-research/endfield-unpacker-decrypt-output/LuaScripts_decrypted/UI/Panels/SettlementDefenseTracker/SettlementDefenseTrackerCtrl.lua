local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementDefenseTracker
local LuaNodeCache = require_ex('Common/Utils/LuaNodeCache')
local STANDARD_HORIZONTAL_RESOLUTION = CS.Beyond.UI.UIConst.CUR_STANDARD_HORIZONTAL_RESOLUTION
local STANDARD_VERTICAL_RESOLUTION = CS.Beyond.UI.UIConst.CUR_STANDARD_VERTICAL_RESOLUTION

SettlementDefenseTrackerCtrl = HL.Class('SettlementDefenseTrackerCtrl', uiCtrl.UICtrl)

local TRACKER_CORE_ATTACKED_IN_ANIMATION_NAME = "defense_tracker_core_attacked_in"

SettlementDefenseTrackerCtrl.m_towerDefenseGame = HL.Field(HL.Userdata)

SettlementDefenseTrackerCtrl.m_spawnerTrackerCache = HL.Field(LuaNodeCache)

SettlementDefenseTrackerCtrl.m_coreTrackerCache = HL.Field(LuaNodeCache)

SettlementDefenseTrackerCtrl.m_spawnerTrackerDataList = HL.Field(HL.Table)

SettlementDefenseTrackerCtrl.m_coreTrackerDataList = HL.Field(HL.Table)

SettlementDefenseTrackerCtrl.m_xEdgeRadius = HL.Field(HL.Number) << -1

SettlementDefenseTrackerCtrl.m_yEdgeRadius = HL.Field(HL.Number) << -1

SettlementDefenseTrackerCtrl.m_updateThread = HL.Field(HL.Thread)

SettlementDefenseTrackerCtrl.m_hpChangeCallbackList = HL.Field(HL.Table)





SettlementDefenseTrackerCtrl.s_messages = HL.StaticField(HL.Table) << {
}


SettlementDefenseTrackerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_towerDefenseGame = GameInstance.player.towerDefenseSystem.towerDefenseGame
    self.m_spawnerTrackerCache = LuaNodeCache(self.view.spawnerTracker, self.view.spawnerTrackersRoot)
    self.m_coreTrackerCache = LuaNodeCache(self.view.coreTracker, self.view.coreTrackersRoot)
    self.m_spawnerTrackerDataList = {}
    self.m_coreTrackerDataList = {}
    self.m_hpChangeCallbackList = {}

    self.m_xEdgeRadius = self.view.config.TRACKER_EDGE_RADIUS_X * (
        self.view.main.rect.width / STANDARD_HORIZONTAL_RESOLUTION
    )
    self.m_yEdgeRadius = self.view.config.TRACKER_EDGE_RADIUS_Y * (
        self.view.main.rect.height / STANDARD_VERTICAL_RESOLUTION
    )

    self:_InitTrackerTargets()
end

SettlementDefenseTrackerCtrl.OnClose = HL.Override() << function(self)
    self.m_updateThread = self:_ClearCoroutine(self.m_updateThread)

    if self.m_towerDefenseGame ~= nil then
        local coreAbilitySystems = self.m_towerDefenseGame.tdCoreAbilitySystems
        for coreAbilityIndex = 0, coreAbilitySystems.Count - 1 do
            local coreAbilitySystem = coreAbilitySystems[coreAbilityIndex]
            coreAbilitySystem.onHpChange:Remove(self.m_hpChangeCallbackList[coreAbilityIndex])
        end
    end
end

SettlementDefenseTrackerCtrl._BuildTrackerData = HL.Method(Vector3, HL.Any).Return(HL.Table) << function(self, worldPos, tracker)
    return {
        worldPos = worldPos,
        tracker = tracker,
        isInBound = false,
        visible = true,
    }
end

SettlementDefenseTrackerCtrl._InitTrackerTargets = HL.Method() << function(self)
    local towerDefenseGame = GameInstance.player.towerDefenseSystem.towerDefenseGame
    local coreAbilitySystems = towerDefenseGame.tdCoreAbilitySystems
    local corePositions = towerDefenseGame.tdCorePositions
    local spawners = towerDefenseGame.gameData.portalEffectPosRot


    if coreAbilitySystems.Count > 0 then
        for coreAbilityIndex = 0, coreAbilitySystems.Count - 1 do
            local coreAbilitySystem = coreAbilitySystems[coreAbilityIndex]
            local coreWorldPos = coreAbilitySystem.entity.position + self.view.config.CORE_TRACKER_OFFSET
            local coreTracker = self.m_coreTrackerCache:Get()
            table.insert(self.m_coreTrackerDataList, self:_BuildTrackerData(coreWorldPos, coreTracker))

            local callback = function(entity, changedHp, isMaxHpChanged)
                if changedHp < 0 then
                    coreTracker.animationWrapper:ClearTween()
                    coreTracker.animationWrapper:PlayWithTween(TRACKER_CORE_ATTACKED_IN_ANIMATION_NAME)
                end
            end
            self.m_hpChangeCallbackList[coreAbilityIndex] = callback
            coreAbilitySystem.onHpChange:Add(callback)
        end
    else
        for coreIndex = 0, corePositions.Count - 1 do
            local coreWorldPos = corePositions[coreIndex] + self.view.config.CORE_TRACKER_OFFSET
            local coreTracker = self.m_coreTrackerCache:Get()
            table.insert(self.m_coreTrackerDataList, self:_BuildTrackerData(coreWorldPos, coreTracker))
        end
    end



    local spawnerCount = spawners ~= nil and spawners.Count or 0
    if spawnerCount == 0 then
        logger.error("【据点防守】出怪点特效位置 配置数量为0")
    end
    for spawnerIndex = 0, spawnerCount - 1 do
        local spawnerWorldPos = spawners[spawnerIndex].position + self.view.config.SPAWNER_TRACKER_OFFSET
        local spawnerTracker = self.m_spawnerTrackerCache:Get()
        table.insert(self.m_spawnerTrackerDataList, self:_BuildTrackerData(spawnerWorldPos, spawnerTracker))
    end

    self:_UpdateTrackersThread()
    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            self:_UpdateTrackersThread()
            coroutine.step()
        end
    end)
end

SettlementDefenseTrackerCtrl._UpdateTrackersThread = HL.Method() << function(self)
    for _, coreData in ipairs(self.m_coreTrackerDataList) do
        self:_UpdateTrackerScreenPosition(coreData)
        self:_RefreshTrackerState(coreData)
    end

    for _, spawnerData in ipairs(self.m_spawnerTrackerDataList) do
        self:_UpdateTrackerScreenPosition(spawnerData)
        self:_RefreshTrackerState(spawnerData)
    end
end

SettlementDefenseTrackerCtrl._UpdateTrackerScreenPosition = HL.Method(HL.Table) << function(self, data)
    if data == nil or data.worldPos == nil then
        return
    end

    local screenPos = CameraManager.mainCamera:WorldToScreenPoint(data.worldPos)
    if screenPos.z < 0 then
        screenPos.x = Screen.width-screenPos.x
        screenPos.y = -screenPos.y
    end
    local screenPos = UIUtils.screenPointToUI(Vector2(screenPos.x, screenPos.y), self.uiCamera)


    local uiPos, uiAngle, isOutBound = UIUtils.mapScreenPosToEllipseEdge(screenPos, self.m_xEdgeRadius / 2.0, self.m_yEdgeRadius / 2.0)

    data.position = uiPos
    data.angle = uiAngle
    data.isInBound = not isOutBound
end

SettlementDefenseTrackerCtrl._RefreshTrackerState = HL.Method(HL.Table) << function(self, data)
    if not data.visible then
        data.tracker.gameObject:SetActiveIfNecessary(false)
        return
    end

    local tracker = data.tracker
    tracker.rectTransform.anchoredPosition = data.position
    tracker.arrowRotator.localRotation = Quaternion.Euler(0, 0, data.angle);
    tracker.arrowRotator.gameObject:SetActiveIfNecessary(not data.isInBound)
    tracker.gameObject:SetActiveIfNecessary(true)
end

HL.Commit(SettlementDefenseTrackerCtrl)
