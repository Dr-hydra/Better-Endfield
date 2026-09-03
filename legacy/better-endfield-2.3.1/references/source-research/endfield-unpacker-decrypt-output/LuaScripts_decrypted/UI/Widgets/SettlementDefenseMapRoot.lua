local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local LuaNodeCache = require_ex('Common/Utils/LuaNodeCache')

SettlementDefenseMapRoot = HL.Class('SettlementDefenseMapRoot', UIWidgetBase)

local ENEMY_UPDATE_THREAD_INTERVAL = 0.1
local BUILDING_UPDATE_THREAD_INTERVAL = 0.5

SettlementDefenseMapRoot.m_towerDefenseGame = HL.Field(HL.Userdata)

SettlementDefenseMapRoot.m_settlementId = HL.Field(HL.Userdata)

SettlementDefenseMapRoot.m_basicUpdateThread = HL.Field(HL.Thread)

SettlementDefenseMapRoot.m_enemyUpdateThread = HL.Field(HL.Thread)

SettlementDefenseMapRoot.m_buildingUpdateThread = HL.Field(HL.Thread)

SettlementDefenseMapRoot.m_leftBottomPos = HL.Field(Vector2)

SettlementDefenseMapRoot.m_rightUpPos = HL.Field(Vector2)

SettlementDefenseMapRoot.m_centerPos = HL.Field(Vector2)

SettlementDefenseMapRoot.m_realLeftBottomPos = HL.Field(Vector2)

SettlementDefenseMapRoot.m_realRightUpPos = HL.Field(Vector2)

SettlementDefenseMapRoot.m_mapRectWidth = HL.Field(HL.Number) << -1

SettlementDefenseMapRoot.m_mapRectHeight = HL.Field(HL.Number) << -1

SettlementDefenseMapRoot.m_viewRectWidth = HL.Field(HL.Number) << -1

SettlementDefenseMapRoot.m_viewRectHeight = HL.Field(HL.Number) << -1

SettlementDefenseMapRoot.m_mapWidth = HL.Field(HL.Number) << -1

SettlementDefenseMapRoot.m_mapHeight = HL.Field(HL.Number) << -1

SettlementDefenseMapRoot.m_mapRectOffset = HL.Field(Vector2)

SettlementDefenseMapRoot.m_playerRectOffset = HL.Field(Vector2)

SettlementDefenseMapRoot.m_playerAngle = HL.Field(HL.Number) << -1

SettlementDefenseMapRoot.m_playerViewAngle = HL.Field(HL.Number) << -1

SettlementDefenseMapRoot.m_coreIconCache = HL.Field(LuaNodeCache)

SettlementDefenseMapRoot.m_spawnerIconCache = HL.Field(LuaNodeCache)

SettlementDefenseMapRoot.m_enemyIconCache = HL.Field(LuaNodeCache)

SettlementDefenseMapRoot.m_buildingIconCache = HL.Field(LuaNodeCache)

SettlementDefenseMapRoot.m_routeIconCache = HL.Field(LuaNodeCache)

SettlementDefenseMapRoot.m_enemyDataMap = HL.Field(HL.Table)

SettlementDefenseMapRoot.m_buildingDataMap = HL.Field(HL.Table)

SettlementDefenseMapRoot.m_isTransitFinished = HL.Field(HL.Boolean) << false

SettlementDefenseMapRoot.m_hpChangeCallbackList = HL.Field(HL.Table)


SettlementDefenseMapRoot._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_TOWER_DEFENSE_DEFENDING_BATTLE_BUILDING_ADDED, function(args)
        local nodeId = unpack(args)
        self:_OnBuildingAdded(nodeId)
    end)
    self:RegisterMessage(MessageConst.ON_TOWER_DEFENSE_DEFENDING_BATTLE_BUILDING_REMOVED, function(args)
        local nodeId = unpack(args)
        self:_OnBuildingRemoved(nodeId)
    end)
    self:RegisterMessage(MessageConst.ON_ENEMY_SPAWNER_PREVIEW_ROUTE_CREATED, function(args)
        local routeId = unpack(args)
        self:_OnRouteCreated(routeId)
    end)
    self:RegisterMessage(MessageConst.ON_TOWER_DEFENSE_TRANSIT_FINISHED, function(args)
        self:_OnTransitFinished()
    end)
end

SettlementDefenseMapRoot._OnDestroy = HL.Override() << function(self)
    self.m_basicUpdateThread = self:_ClearCoroutine(self.m_basicUpdateThread)
    self.m_enemyUpdateThread = self:_ClearCoroutine(self.m_enemyUpdateThread)
    self.m_buildingUpdateThread = self:_ClearCoroutine(self.m_buildingUpdateThread)

    if self.m_towerDefenseGame ~= nil then
        local coreAbilitySystems = self.m_towerDefenseGame.tdCoreAbilitySystems
        for coreAbilityIndex = 0, coreAbilitySystems.Count - 1 do
            local coreAbilitySystem = coreAbilitySystems[coreAbilityIndex]
            coreAbilitySystem.onHpChange:Remove(self.m_hpChangeCallbackList[coreAbilityIndex])
        end
    end
end

SettlementDefenseMapRoot.InitSettlementDefenseMapRoot = HL.Method() << function(self)
    self.m_towerDefenseGame = GameInstance.player.towerDefenseSystem.towerDefenseGame
    self.m_enemyDataMap = {}
    self.m_buildingDataMap = {}
    self.m_hpChangeCallbackList = {}

    self:_InitIconCache()
    self:_InitMapRect()
    self:_InitMapCores()
    self:_InitMapSpawners()
    self:_InitMapBuildings()
    self:_InitMapUpdateThread()

    self:_FirstTimeInit()
end




SettlementDefenseMapRoot._InitIconCache = HL.Method() << function(self)
    local originalIcon, iconRoot = self.view.originalIcon, self.view.iconRoot

    self.m_coreIconCache = LuaNodeCache(originalIcon.coreIcon, iconRoot.coreRoot)
    self.m_spawnerIconCache = LuaNodeCache(originalIcon.spawnerIcon, iconRoot.spawnerRoot)
    self.m_enemyIconCache = LuaNodeCache(originalIcon.enemyIcon, iconRoot.enemyRoot)
    self.m_buildingIconCache = LuaNodeCache(originalIcon.buildingIcon, iconRoot.buildingRoot)
    self.m_routeIconCache = LuaNodeCache(originalIcon.routeIcon, iconRoot.routeRoot)
end

SettlementDefenseMapRoot._InitMapRect = HL.Method() << function(self)
    local activeTdId = GameInstance.player.towerDefenseSystem.activeTdId
    if string.isEmpty(activeTdId) then
        return
    end

    local levelSuccess, levelTableData = Tables.towerDefenseTable:TryGetValue(activeTdId)
    if not levelSuccess then
        return
    end

    local settlementId = levelTableData.settlementId
    local mapSuccess, mapTableData = Tables.towerDefenseMapTable:TryGetValue(settlementId)
    if not mapSuccess then
        return
    end

    local mapSprite = self:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT_DEFENSE_MAP, mapTableData.mapImage)
    if mapSprite ~= nil then
        self.view.map.sprite = mapSprite
        self.view.map:SetNativeSize()
    end

    self.m_leftBottomPos = Vector2(mapTableData.leftBottomPos.x, mapTableData.leftBottomPos.z)
    self.m_rightUpPos = Vector2(mapTableData.rightUpPos.x, mapTableData.rightUpPos.z)
    local direction = self.m_rightUpPos - self.m_leftBottomPos
    self.m_centerPos = direction / 2.0 + self.m_leftBottomPos
    self.m_mapWidth = math.abs(direction.x)
    self.m_mapHeight = math.abs(direction.y)

    local mapRectScale = self.view.mapRect.localScale.x
    self.m_mapRectWidth = self.view.mapRect.rect.width * mapRectScale
    self.m_mapRectHeight = self.view.mapRect.rect.height * mapRectScale

    self.m_viewRectWidth = self.view.viewRect.rect.width
    self.m_viewRectHeight = self.view.viewRect.rect.height

    local realCenterPos = self.m_centerPos + Vector2(
        self.config.REAL_AREA_OFFSET.x * self.m_mapWidth / self.m_mapRectWidth,
        self.config.REAL_AREA_OFFSET.y * self.m_mapHeight / self.m_mapRectHeight
    )
    self.m_realLeftBottomPos = realCenterPos - direction * 0.5 * self.config.REAL_AREA_SCALE
    self.m_realRightUpPos = realCenterPos + direction * 0.5 * self.config.REAL_AREA_SCALE

end

SettlementDefenseMapRoot._InitMapUpdateThread = HL.Method() << function(self)
    self:_PreUpdateAndRefreshAll()
    self.m_basicUpdateThread = self:_StartCoroutine(function()
        while true do
            local uiCtrl = self:GetUICtrl()
            if uiCtrl ~= nil and uiCtrl:IsShow() then
                self:_UpdateMapAndPlayerRectState()
                self:_UpdatePlayerAndCameraRotation()

                self:_RefreshMapRect()
                self:_RefreshPlayerRect()
            end
            coroutine.step()
        end
    end)

    self.m_enemyUpdateThread = self:_StartCoroutine(function()
        while true do
            local uiCtrl = self:GetUICtrl()
            if uiCtrl ~= nil and uiCtrl:IsShow() then
                self:_UpdateEnemyDataMap()

                self:_RefreshEnemiesRect()
            end
            coroutine.wait(ENEMY_UPDATE_THREAD_INTERVAL)
        end
    end)

    self.m_buildingUpdateThread = self:_StartCoroutine(function()
        while true do
            local uiCtrl = self:GetUICtrl()
            if uiCtrl ~= nil and uiCtrl:IsShow() then
                self:_UpdateBuildingDataMap()

                self:_RefreshBuildingsState()
            end
            coroutine.wait(BUILDING_UPDATE_THREAD_INTERVAL)
        end
    end)
end

SettlementDefenseMapRoot._InitMapCores = HL.Method() << function(self)
    local coreAbilitySystems = self.m_towerDefenseGame.tdCoreAbilitySystems
    for coreAbilityIndex = 0, coreAbilitySystems.Count - 1 do
        local coreAbilitySystem = coreAbilitySystems[coreAbilityIndex]
        local coreWorldPos = coreAbilitySystem.entity.position
        local coreIcon = self.m_coreIconCache:Get()
        self:_RefreshIconRectPos(coreWorldPos, coreIcon)
        coreIcon.indexText.text = string.format("%d", LuaIndex(coreAbilityIndex))

        local callback = function(entity, changedHp, isMaxHpChanged)
            if changedHp < 0 then
                coreIcon.animationWrapper:ClearTween()
                coreIcon.animationWrapper:PlayWithTween(coreIcon.config.ATTACKED_ANIMATION_NAME)
            end
        end
        self.m_hpChangeCallbackList[coreAbilityIndex] = callback
        coreAbilitySystem.onHpChange:Add(callback)
    end
end

SettlementDefenseMapRoot._InitMapSpawners = HL.Method() << function(self)



    local spawners = self.m_towerDefenseGame.gameData.portalEffectPosRot
    if spawners == nil then
        return
    end
    for spawnerIndex = 0, spawners.Count - 1 do
        local spawnerWorldPos = spawners[spawnerIndex].position
        local spawnerIcon = self.m_spawnerIconCache:Get()
        self:_RefreshIconRectPos(spawnerWorldPos, spawnerIcon)
    end
end

SettlementDefenseMapRoot._InitMapBuildings = HL.Method() << function(self)
    local buildings = self.m_towerDefenseGame.battleBuildings
    for buildingIndex = 0, buildings.Count - 1 do
        self:_BuildBuildingData(buildings[buildingIndex])
    end

    self:_RefreshBuildingsState()
end






SettlementDefenseMapRoot._PreUpdateAndRefreshAll = HL.Method() << function(self)
    self:_UpdateMapAndPlayerRectState()
    self:_UpdatePlayerAndCameraRotation()
    self:_RefreshMapRect()
    self:_RefreshPlayerRect()
end

SettlementDefenseMapRoot._UpdateMapAndPlayerRectState = HL.Method() << function(self)
    local character = GameInstance.playerController.mainCharacter
    if not NotNull(character.rootCom.transform) then
        return
    end
    local characterPos = Vector2(character.position.x, character.position.z)
    local worldPosOffset = characterPos - self.m_centerPos
    local rectPosOffset = Vector2(
        -worldPosOffset.x * self.m_mapRectWidth / self.m_mapWidth,
        -worldPosOffset.y * self.m_mapRectHeight / self.m_mapHeight
    )



    self.m_playerRectOffset = Vector2.zero
    local boundX = (self.m_mapRectWidth - self.m_viewRectWidth) / 2.0
    boundX = math.max(boundX, 0)
    if math.abs(rectPosOffset.x) > boundX then
        local absX = math.abs(rectPosOffset.x) - boundX
        self.m_playerRectOffset.x = (rectPosOffset.x > 0 and -1 or 1) * absX
        rectPosOffset.x = rectPosOffset.x + self.m_playerRectOffset.x
    end
    local boundY = (self.m_mapRectHeight - self.m_viewRectHeight) / 2.0
    boundY = math.max(boundY, 0)
    if math.abs(rectPosOffset.y) > boundY then
        local absY = math.abs(rectPosOffset.y) - boundY
        self.m_playerRectOffset.y = (rectPosOffset.y > 0 and -1 or 1) * absY
        rectPosOffset.y = rectPosOffset.y + self.m_playerRectOffset.y
    end

    self.m_mapRectOffset = rectPosOffset
end

SettlementDefenseMapRoot._UpdatePlayerAndCameraRotation = HL.Method() << function(self)
    local character = GameInstance.playerController.mainCharacter
    if not NotNull(character.rootCom.transform) then
        return
    end
    self.m_playerAngle = character.rootCom.transform.eulerAngles.y;
    self.m_playerViewAngle = CameraManager.mainCamera.transform.eulerAngles.y;
end

SettlementDefenseMapRoot._UpdateEnemyDataMap = HL.Method() << function(self)
    local enemies = self.m_towerDefenseGame.enemies
    for enemyIndex = 0, enemies.Count - 1 do
        local enemyEntity = enemies[enemyIndex]:Lock()
        if enemyEntity ~= nil and enemyEntity.enemy ~= nil then
            local serverId = enemyEntity.serverId
            local enemyData = self.m_enemyDataMap[serverId]
            if enemyData == nil then
                enemyData = {}
                local enemyIcon = self.m_enemyIconCache:Get()
                enemyData.icon = enemyIcon
                self.m_enemyDataMap[serverId] = enemyData


                local enemy = enemyEntity.enemy
                local rank = enemy:GetEnemyRank()
                local isElite = rank == CS.Beyond.Gameplay.EnemyRank.Elite or
                    rank == CS.Beyond.Gameplay.EnemyRank.Boss
                enemyIcon.mobImage.gameObject:SetActive(not isElite)
                enemyIcon.eliteImage.gameObject:SetActive(isElite)
            end
            enemyData.pos = enemyEntity.position
            enemyData.isUpdated = true
        end
    end

    for serverId, enemyData in pairs(self.m_enemyDataMap) do
        if not enemyData.isUpdated then

            local enemyIcon = enemyData.icon
            self.m_enemyIconCache:Cache(enemyIcon)
            self.m_enemyDataMap[serverId] = nil
        end
    end
end

SettlementDefenseMapRoot._UpdateBuildingDataMap = HL.Method() << function(self)
    for _, buildingData in pairs(self.m_buildingDataMap) do
        local nodeId = buildingData.nodeId
        local state = FactoryUtils.getBuildingStateType(nodeId)
        buildingData.isBroken = state == GEnums.FacBuildingState.Broken
    end
end






SettlementDefenseMapRoot._RefreshMapRect = HL.Method() << function(self)
    self.view.mapRect.anchoredPosition = self.m_mapRectOffset
end

SettlementDefenseMapRoot._RefreshPlayerRect = HL.Method() << function(self)
    local playerRect = self.view.playerRect
    playerRect.rectTransform.anchoredPosition = self.m_playerRectOffset
    playerRect.playerArrow.localEulerAngles = Vector3(0.0, 0.0, -self.m_playerAngle);
    playerRect.playerView.localEulerAngles = Vector3(0.0, 0.0, -self.m_playerViewAngle);
end

SettlementDefenseMapRoot._RefreshEnemiesRect = HL.Method() << function(self)
    for _, enemyData in pairs(self.m_enemyDataMap) do
        local worldPos = enemyData.pos
        local enemyIcon = enemyData.icon
        self:_RefreshIconRectPos(worldPos, enemyIcon)
        enemyData.isUpdated = false
    end
end

SettlementDefenseMapRoot._RefreshBuildingsState = HL.Method() << function(self)
    for _, buildingData in pairs(self.m_buildingDataMap) do
        buildingData.icon.brokenNode.gameObject:SetActive(buildingData.isBroken)
    end
end

SettlementDefenseMapRoot._RefreshIconRectPos = HL.Method(Vector3, HL.Any) << function(self, worldPos, icon)
    if icon == nil then
        return
    end

    worldPos = Vector2(worldPos.x, worldPos.z)
    local worldPosOffset = worldPos - self.m_centerPos
    local rectPosOffset = Vector2(
        worldPosOffset.x * self.m_mapRectWidth / self.m_mapWidth,
        worldPosOffset.y * self.m_mapRectHeight / self.m_mapHeight
    )
    icon.rectTransform.anchoredPosition = rectPosOffset
end






SettlementDefenseMapRoot._OnBuildingAdded = HL.Method(HL.Number) << function(self, nodeId)
    self:_BuildBuildingData(nodeId)
end

SettlementDefenseMapRoot._OnBuildingRemoved = HL.Method(HL.Number) << function(self, nodeId)
    local buildingData = self.m_buildingDataMap[nodeId]
    if buildingData == nil then
        return
    end
    local icon = buildingData.icon
    self.m_buildingIconCache:Cache(icon)
    self.m_buildingDataMap[nodeId] = nil
end






SettlementDefenseMapRoot._OnRouteCreated = HL.Method(HL.Number) << function(self, routeId)
    if self.view.config.NEED_WAIT_TRANSIT_COMPLETE and not self.m_isTransitFinished then
        return
    end

    self:_CreateRouteIcon(routeId)
end

SettlementDefenseMapRoot._CreateRouteIcon = HL.Method(HL.Number) << function(self, routeId)
    if routeId == nil then
        return
    end

    local success, worldPos = self.m_towerDefenseGame:GetRouteStartPositionById(routeId)
    if not success then
        return
    end

    local routeIcon = self.m_routeIconCache:Get()
    routeIcon.gameObject:SetActive(true)
    self:_RefreshIconRectPos(worldPos, routeIcon)
    routeIcon.animationWrapper:PlayInAnimation(function()
        routeIcon.gameObject:SetActive(false)
        self.m_routeIconCache:Cache(routeIcon)
    end)
end






SettlementDefenseMapRoot._OnTransitFinished = HL.Method() << function(self)
    self.m_isTransitFinished = true

    local waitList = self.m_towerDefenseGame.createdRoute
    for _, routeId in cs_pairs(waitList) do
        self:_CreateRouteIcon(routeId)
    end
end

SettlementDefenseMapRoot._BuildBuildingData = HL.Method(HL.Number) << function(self, nodeId)
    if nodeId == nil then
        return
    end

    local nodeHandler = FactoryUtils.getBuildingNodeHandler(nodeId)
    if nodeHandler == nil then
        return
    end

    local buildingWorldPos = nodeHandler.transform.worldPosition
    if buildingWorldPos.x < self.m_realLeftBottomPos.x or buildingWorldPos.x > self.m_realRightUpPos.x then
        return
    end
    if buildingWorldPos.z < self.m_realLeftBottomPos.y or buildingWorldPos.z > self.m_realRightUpPos.y then
        return
    end
    if GameInstance.remoteFactoryManager:IsWorldPositionInMainRegion(buildingWorldPos) then
        return
    end

    local buildingIcon = self.m_buildingIconCache:Get()
    local state = FactoryUtils.getBuildingStateType(nodeId)
    local isBroken = state == GEnums.FacBuildingState.Broken
    self.m_buildingDataMap[nodeId] = {
        nodeId = nodeId,
        pos = buildingWorldPos,
        icon = buildingIcon,
        isBroken = isBroken,
    }

    self:_RefreshIconRectPos(buildingWorldPos, buildingIcon)
end




HL.Commit(SettlementDefenseMapRoot)
return SettlementDefenseMapRoot

