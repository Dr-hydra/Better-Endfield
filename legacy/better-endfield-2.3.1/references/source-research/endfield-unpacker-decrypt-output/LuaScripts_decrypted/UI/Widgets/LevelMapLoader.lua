local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local LuaNodeCache = require_ex('Common/Utils/LuaNodeCache')
local UILevelMapUtils = CS.Beyond.Gameplay.UILevelMapUtils
local ElementType = CS.Beyond.Gameplay.UILevelMapStaticElementType
local LineType = CS.Beyond.Gameplay.MarkLineType
local Rect = CS.UnityEngine.Rect
local CustomDrawRTManager = CS.HG.Rendering.Runtime.CustomDrawRTManager
local PosValueState = CS.Beyond.Gameplay.PosValueState
local ChunkLODType = CS.Beyond.Gameplay.ChunkLoadConfigInfo.ChunkLODType
local MistLoadType = MapConst.MapMistLoadType
local Stack = require_ex("Common/Utils/DataStructure/Stack")
local RootLevel = CS.Beyond.Gameplay.StaticElementRootLevel
local CommonPhaseStateType = CS.Beyond.Gameplay.CommonElementPhaseStateType

LevelMapLoader = HL.Class('LevelMapLoader', UIWidgetBase)

local LOADER_DEFAULT_UPDATE_INTERVAL = 0.2

local COMMON_DELAY_TIME = 3
local DISPOSE_DELAY_TIME = 3
local DISPOSE_COROUTINE_INTERVAL = 0.3

local CHUNK_DRAWER_KEY_FORMAT = "chunk_%d"
local MIST_DRAWER_KEY_FORMAT = "mist_%d"

local EXTRA_LOW_RT_DRAW_SCALE_MOBILE = 0.3
local EXTRA_LOW_RT_DRAW_SCALE_DEFAULT = 0.5
local EXTRA_RT_SIZE_SCALE_DEFAULT = 1

local MIN_MARK_ORDER = 1
local MAX_MARK_ORDER = 6
local CUSTOM_MARK_ORDER = 6
local MARK_ORDER_VIEW_NAME_FORMAT = "order%d"

local CHUNK_DRAW_MATERIAL_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Materials/M_ui_mapchunk_rendertexture_painter_override.mat"
local MIST_DRAW_MATERIAL_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Materials/M_ui_mapmist_rendertexture_painter.mat"

local TEXTURE_LOAD_PATH_FORMAT = "%s/%s.png"


LevelMapLoader.m_initialized = HL.Field(HL.Boolean) << false

LevelMapLoader.m_mapManager = HL.Field(CS.Beyond.Gameplay.MapManager)

LevelMapLoader.m_levelMapConfig = HL.Field(CS.Beyond.Gameplay.UILevelMapConfig)

LevelMapLoader.m_mistSystem = HL.Field(CS.Beyond.Gameplay.MistMapSystem)

LevelMapLoader.m_regionManager = HL.Field(CS.Beyond.Gameplay.MapRegionManager)

LevelMapLoader.m_dataUpdateInterval = HL.Field(HL.Number) << LOADER_DEFAULT_UPDATE_INTERVAL

LevelMapLoader.m_baseUpdateTick = HL.Field(HL.Number) << -1

LevelMapLoader.m_gridMaskUpdateTick = HL.Field(HL.Number) << -1

LevelMapLoader.m_animMistShowTimer = HL.Field(HL.Number) << -1

LevelMapLoader.m_mapId = HL.Field(HL.String) << ""

LevelMapLoader.m_levelId = HL.Field(HL.String) << ""

LevelMapLoader.m_markCache = HL.Field(Stack)

LevelMapLoader.m_customMarkCache = HL.Field(Stack)

LevelMapLoader.m_trackingMarkCache = HL.Field(Stack)

LevelMapLoader.m_tierCache = HL.Field(LuaNodeCache)

LevelMapLoader.m_posTween = HL.Field(HL.Userdata)

LevelMapLoader.m_sizeTween = HL.Field(HL.Userdata)

LevelMapLoader.m_onMarkInstDataChangedCallback = HL.Field(HL.Function)

LevelMapLoader.m_markStaticDataMap = HL.Field(HL.Table)

LevelMapLoader.m_delayActionList = HL.Field(HL.Table)

LevelMapLoader.m_gridsMaskFollowTarget = HL.Field(RectTransform)

LevelMapLoader.m_isLowMemoryDevice = HL.Field(HL.Boolean) << false



LevelMapLoader.m_needUpdate = HL.Field(HL.Boolean) << true

LevelMapLoader.m_needOptimizePerformance = HL.Field(HL.Boolean) << false

LevelMapLoader.m_needExtraLowScaleRT = HL.Field(HL.Boolean) << false

LevelMapLoader.m_extraView = HL.Field(HL.Number) << 0

LevelMapLoader.m_needShowOtherLevelTracking = HL.Field(HL.Boolean) << false

LevelMapLoader.m_onMarkHover = HL.Field(HL.Function)

LevelMapLoader.m_onGridsLoadedStateChange = HL.Field(HL.Function)

LevelMapLoader.m_expectedStaticElementTypes = HL.Field(HL.Table)

LevelMapLoader.m_needListenMarkStateChange = HL.Field(HL.Boolean) << false

LevelMapLoader.m_needInteractableMark = HL.Field(HL.Boolean) << false

LevelMapLoader.m_retainTextureAfterDraw = HL.Field(HL.Boolean) << false

LevelMapLoader.m_useChunkGODrawer = HL.Field(HL.Boolean) << false

LevelMapLoader.m_initialMarkScale = HL.Field(Vector3)





LevelMapLoader.m_rtDrawerDataPool = HL.Field(HL.Table)

LevelMapLoader.m_chunkRTDrawers = HL.Field(HL.Table)

LevelMapLoader.m_chunkGODrawers = HL.Field(HL.Table)

LevelMapLoader.m_loadedChunkViewDataMap = HL.Field(HL.Table)

LevelMapLoader.m_chunkResourceLoadDataPool = HL.Field(HL.Table)

LevelMapLoader.m_lateHideChunkLODType = HL.Field(HL.Any)

LevelMapLoader.m_delayDisposeResourceThread = HL.Field(HL.Thread)

LevelMapLoader.m_chunkResourcePathCache = HL.Field(HL.Table)

LevelMapLoader.m_tierResourcePathCache = HL.Field(HL.Table)

LevelMapLoader.m_mistResourcePathCache = HL.Field(HL.Table)

LevelMapLoader.m_lodScaleCache = HL.Field(HL.Table)

LevelMapLoader.m_mistLODScaleCache = HL.Field(HL.Table)

LevelMapLoader.m_loadedTierViewDataMap = HL.Field(HL.Table)

LevelMapLoader.m_mistDrawers = HL.Field(HL.Table)

LevelMapLoader.m_loadedGridViewDataMap = HL.Field(HL.Table)

LevelMapLoader.m_gridMistSingleTextureLength = HL.Field(HL.Number) << 0

LevelMapLoader.m_forbidMistRefreshAfterGridChange = HL.Field(HL.Boolean) << false

LevelMapLoader.m_waitMistRefreshAfterGridChange = HL.Field(HL.Boolean) << false

LevelMapLoader.m_tierId = HL.Field(HL.Number) << 0

LevelMapLoader.m_tierIndex = HL.Field(HL.Number) << 0

LevelMapLoader.m_needShowMarkTier = HL.Field(HL.Boolean) << false





LevelMapLoader.m_staticElementInitializer = HL.Field(HL.Table)

LevelMapLoader.m_lineCaches = HL.Field(HL.Table)

LevelMapLoader.m_lineRoots = HL.Field(HL.Table)

LevelMapLoader.m_loadedLineViewDataMap = HL.Field(HL.Table)

LevelMapLoader.m_loadedPowerLineCount = HL.Field(HL.Number)  << 0

LevelMapLoader.m_buildElementsLevels = HL.Field(HL.Table)

LevelMapLoader.m_loadedStaticElementViewDataMap = HL.Field(HL.Table)

LevelMapLoader.m_loadedMarkViewDataMap = HL.Field(HL.Table)

LevelMapLoader.m_missionTrackingAreaCache = HL.Field(LuaNodeCache)

LevelMapLoader.m_loadedMissionTrackingMarks = HL.Field(HL.Table)

LevelMapLoader.m_loadedMissionTrackingAreas = HL.Field(HL.Table)

LevelMapLoader.m_loadedGeneralTrackingMarkId = HL.Field(HL.String) << ""

LevelMapLoader.m_gameplayAreaCache = HL.Field(LuaNodeCache)

LevelMapLoader.m_loadedGameplayAreas = HL.Field(HL.Table)

LevelMapLoader.m_tryLoadElementsList = HL.Field(HL.Table)

LevelMapLoader.m_switchMaskCells = HL.Field(HL.Forward("UIListCache"))

LevelMapLoader.m_waitVisibleInMistMarks = HL.Field(HL.Table)

LevelMapLoader.m_connectNodeLineGetter = HL.Field(HL.Table)



LevelMapLoader.m_gridRectLength = HL.Field(HL.Number) << -1

LevelMapLoader.m_gridWorldLength = HL.Field(HL.Number) << -1


LevelMapLoader._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_BACKUP_STATE_CHANGED, function(args)
        self:_OnMarkLineInstDataChanged(LineType.Power)
    end)
    self:RegisterMessage(MessageConst.ON_MAP_POWER_LINE_CHANGED, function()
        self:_OnMarkLineInstDataChanged(LineType.Power)
    end)
    self:RegisterMessage(MessageConst.ON_MAP_TRAVEL_LINE_CHANGED, function()
        self:_OnMarkLineInstDataChanged(LineType.Travel)
    end)
    self:RegisterMessage(MessageConst.ON_MAP_UD_PIPE_LINE_CHANGED, function()
        self:_OnMarkLineInstDataChanged(LineType.UdPipe)
    end)
    self:RegisterMessage(MessageConst.ON_MAP_MARK_RUNTIME_DATA_CHANGED, function(args)
        local instId, isAdd = unpack(args)
        self:_OnMarkInstDataChanged(instId, isAdd)
    end)
    self:RegisterMessage(MessageConst.ON_MAP_MARK_RUNTIME_DATA_MODIFY, function(args)
        local instId = unpack(args)
        self:_OnMarkInstDataModified(instId)
    end)
    self:RegisterMessage(MessageConst.ON_MAP_GAMEPLAY_AREA_ADDED, function(args)
        local areaId = unpack(args)
        self:_RefreshGameplayArea(areaId, true)
    end)
    self:RegisterMessage(MessageConst.ON_MAP_GAMEPLAY_AREA_REMOVED, function(args)
        local areaId = unpack(args)
        self:_RefreshGameplayArea(areaId, false)
    end)
    self:RegisterMessage(MessageConst.SHOW_CUSTOM_MARK_MULTI_DELETE, function(args)
        self:_RefreshCustomMarkOrderActiveState(false)
    end)
    self:RegisterMessage(MessageConst.HIDE_CUSTOM_MARK_MULTI_DELETE, function(args)
        self:_RefreshCustomMarkOrderActiveState(true)
    end)
    self:RegisterMessage(MessageConst.ON_MAP_TRACKING_DIFF_LEVEL_STATE_CHANGED, function(args)
        self:_RefreshGeneralTrackingMarkLevelState()
        self:_RefreshMissionTrackingMarksLevelState()
    end)
    self:RegisterMessage(MessageConst.ON_MAP_TRACKING_DIFF_LEVEL_PORT_CHANGED, function(args)
        self:_RefreshCustomMarkOrderActiveState(true)
    end)
    self:RegisterMessage(MessageConst.ON_COMMON_STATIC_ELEMENT_STATE_CHANGED, function(args)
        local elementId, isVisible = unpack(args)
        self:_OnCommonStaticElementStateChanged(elementId, isVisible)
    end)
    self:RegisterMessage(MessageConst.ON_NARRATIVE_TEXT_STATE_CHANGED, function(args)
        local elementId, textId = unpack(args)
        self:_OnNarrativeTextStateChanged(elementId, textId)
    end)
end

LevelMapLoader._OnDestroy = HL.Override() << function(self)
    if not self.m_initialized then
        return
    end
    self.m_baseUpdateTick = LuaUpdate:Remove(self.m_baseUpdateTick)
    self.m_gridMaskUpdateTick = LuaUpdate:Remove(self.m_gridMaskUpdateTick)
    self.m_animMistShowTimer = self:_ClearTimer(self.m_animMistShowTimer)
    self:_StopDelayDisposeChunkResourceThread()
    self:_RemoveAllDelayActions()
    self:_DestroyChunkGODrawers()
    self:_DisposeAllChunkResources()
    self:_ClearAllDrawersRT()
end

LevelMapLoader._DestroyChunkGODrawers = HL.Method() << function(self)
    if not self.m_useChunkGODrawer or self.m_chunkGODrawers == nil then
        return
    end
    self:_RecycleAllGOChunkCells()
    for _, drawer in pairs(self.m_chunkGODrawers) do
        if drawer.stencilMaterial ~= nil then
            Unity.Object.Destroy(drawer.stencilMaterial)
            drawer.stencilMaterial = nil
        end
    end
end

LevelMapLoader.InitLevelMapLoader = HL.Method(HL.String, HL.Opt(HL.Table)) << function(self, levelId, customInfo)
    self.m_initialized = false

    if string.isEmpty(levelId) then
        return
    end

    self.m_levelMapConfig = DataManager.uiLevelMapConfig
    self.m_gridRectLength = self.m_levelMapConfig.gridRectLength
    self.m_gridWorldLength = self.m_levelMapConfig.gridWorldLength
    self.m_mapManager = GameInstance.player.mapManager
    self.m_mistSystem = GameInstance.player.mistMapSystem
    self.m_regionManager = GameWorld.mapRegionManager
    self.m_isLowMemoryDevice = CS.Beyond.BeyondMemoryUtility.IsLowMemoryDevice()
    EXTRA_RT_SIZE_SCALE_DEFAULT = self.m_isLowMemoryDevice and 2 or 1;
    local success, levelConfig = Utils.getLevelConfig(levelId)
    self.m_mapId = success and levelConfig.mapIdStr or ""
    self.m_levelId = levelId
    self.m_tierId = MapConst.BASE_TIER_ID
    self.m_forbidMistRefreshAfterGridChange = false
    self:_InitTableFields()
    self:_InitCustomInfo(customInfo)
    self:_InitLoaderCache()
    self:_InitChunkDrawer()
    self:_InitMistDrawer()

    if BEYOND_DEBUG_COMMAND and customInfo.isDebugMode then
        self:_InitDebugMode(levelId)
        return
    end

    self:_InitGridsMask()
    self:_InitMarkStaticDataMap()
    self:_InitStaticElementInitializer()
    self:_InitLoaderComponent()

    self:_InitLoaderUpdateThread()
    self:_InitPermanentElementsInCurrentMap()

    self:_FirstTimeInit()

    self.m_initialized = true
end




LevelMapLoader._InitLoaderComponent = HL.Method() << function(self)
    local loader = self.view.loader
    loader:InitLoader(self.m_levelId)
    loader.needTickCheckHit = self.m_needUpdate

    loader.onHitStateChange:AddListener(function(hitStateChangeEventData)
        self:_OnLoadStateChanged(hitStateChangeEventData)
    end)
    loader.onCullingLODChange:AddListener(function(lastLODType, currLODType)
        self:_RefreshChunksCullingLOD(lastLODType, currLODType)
    end)
    loader.onCullingStateChange:AddListener(function(cullingStateChangeEventData)
        self:_RefreshChunksCullingState()
    end)
    loader.onPermanentStaticElementsHitStateChange:AddListener(function()
        self:_RefreshPermanentStaticElementsLoadedState()
    end)
end

LevelMapLoader._InitCustomInfo = HL.Method(HL.Table) << function(self, customInfo)
    customInfo = customInfo or {}
    self.m_extraView = customInfo.extraView or 0
    self.m_needOptimizePerformance = customInfo.needOptimizePerformance or false
    self.m_needExtraLowScaleRT = customInfo.needExtraLowScaleRT or false
    self.m_needShowOtherLevelTracking = customInfo.needShowOtherLevelTracking or false
    self.m_onMarkInstDataChangedCallback = customInfo.onMarkInstDataChangedCallback or nil
    self.m_expectedStaticElementTypes = customInfo.expectedStaticElements or {}
    self.m_onMarkHover = customInfo.onMarkHover or nil
    self.m_gridsMaskFollowTarget = customInfo.gridsMaskFollowTarget or nil
    self.m_needInteractableMark = customInfo.needInteractableMark or false
    self.m_needListenMarkStateChange = customInfo.needListenMarkStateChange or false
    self.m_onGridsLoadedStateChange = customInfo.onGridsLoadedStateChange or nil
    self.m_retainTextureAfterDraw = customInfo.retainTextureAfterDraw or false
    self.m_useChunkGODrawer = customInfo.useChunkGODrawer or false
    if customInfo.needUpdate ~= nil then
        self.m_needUpdate = customInfo.needUpdate
    end

    local panelRectTransform = self:GetUICtrl().view.rectTransform
    local markRectTransform = self.view.source.marks.levelMapMark.view.rectTransform
    self.m_initialMarkScale = CS.Beyond.UI.UIUtils.GetNodeScaleOffset(panelRectTransform, markRectTransform)
    local configScale = self.view.config.INITIAL_MARK_SCALE
    self.m_initialMarkScale = Vector3(
        configScale / self.m_initialMarkScale.x,
        configScale / self.m_initialMarkScale.y,
        configScale / self.m_initialMarkScale.z
    )
end

LevelMapLoader._InitTableFields = HL.Method(HL.Opt(HL.Boolean)) << function(self, ignoreGlobal)
    self.m_buildElementsLevels = {}
    self.m_delayActionList = {}
    self.m_tryLoadElementsList = {}

    if not ignoreGlobal then
        self.m_rtDrawerDataPool = {}
        self.m_chunkResourceLoadDataPool = {}

        self.m_loadedGridViewDataMap = {}
        self.m_loadedChunkViewDataMap = {}
        self.m_loadedTierViewDataMap = {}

        self.m_loadedStaticElementViewDataMap = {}
        self.m_loadedLineViewDataMap = {}
        self.m_loadedMarkViewDataMap = {}
        self.m_loadedMissionTrackingMarks = {}
        self.m_loadedMissionTrackingAreas = {}
        self.m_loadedGameplayAreas = {}

        self.m_chunkResourcePathCache = {}
        self.m_tierResourcePathCache = {}
        self.m_mistResourcePathCache = {}
        self.m_markStaticDataMap = {}
        self.m_lodScaleCache = {}
        self.m_mistLODScaleCache = {}

        self.m_waitVisibleInMistMarks = {}

        self.m_connectNodeLineGetter = {}
    end
end

LevelMapLoader._InitMarkStaticDataMap = HL.Method() << function(self)
    self.m_markStaticDataMap = {}
    for templateId, templateData in pairs(Tables.mapMarkTempTable) do
        self.m_markStaticDataMap[templateId] = {
            templateId = templateId,
            visibleLayer = templateData.visibleLayer,
            sortOrder = templateData.sortOrder,
            filterType = templateData.markInfoType:GetHashCode(),
            filterTypeEnum = templateData.markInfoType,
        }
    end
end

LevelMapLoader._InitLoaderCache = HL.Method() << function(self)
    local source = self.view.source
    local element = self.view.element


    source.gameObject:SetActive(false)
    source.marks.levelMapMark.gameObject:SetActive(true)
    source.marks.levelMapInteractableMark.gameObject:SetActive(true)
    source.marks.levelMapCustomMark.gameObject:SetActive(true)

    self.m_tierCache = LuaNodeCache(source.tier, element.loadedTiers)

    self.m_missionTrackingAreaCache = LuaNodeCache(
        source.mission.missionTrackingArea,
        element.missionArea
    )

    self.m_gameplayAreaCache = LuaNodeCache(
        source.gameplay.gameplayArea,
        element.gameplayArea
    )

    local lineRoot = element.lineRoot
    local frontLineRoot = element.frontLineRoot
    local lines = source.lines
    self.m_lineCaches = {
        [LineType.Power] = LuaNodeCache(lines.powerLine, lineRoot.powerLine),
        [LineType.Travel] = LuaNodeCache(lines.travelLine, lineRoot.travelLine),
        [LineType.DomainDepotDeliver] = LuaNodeCache(lines.deliverLine, lineRoot.domainDepotDeliverLine),
        [LineType.UdPipe] = LuaNodeCache(lines.udPipeLine, frontLineRoot.udPipeLine),
    }
    self.m_lineRoots = {
        [LineType.Power] = lineRoot.powerLine,
        [LineType.Travel] = lineRoot.travelLine,
        [LineType.DomainDepotDeliver] = lineRoot.domainDepotDeliverLine,
        [LineType.UdPipe] = frontLineRoot.udPipeLine,
    }
    self.m_loadedPowerLineCount = 0

    self:_InitLoaderMarkCache()
end

LevelMapLoader._InitLoaderMarkCache = HL.Method() << function(self)
    if self.m_needInteractableMark then
        self.m_customMarkCache = Stack()
    end
    self.m_markCache = Stack()
    self.m_trackingMarkCache = Stack()
end

LevelMapLoader._InitStaticElementInitializer = HL.Method() << function(self)

    local source = self.view.source
    local element = self.view.element
    local frontRoot = element.staticElementFrontRoot
    local backRoot = element.staticElementBackRoot
    local bottomRoot = element.staticElementBottomRoot
    local gridRoot = element.staticElementGridRoot

    local staticElements = source.staticElements



    self.m_staticElementInitializer = {
        [ElementType.SwitchButton] = {
            cache = LuaNodeCache(staticElements.levelSwitchButton, backRoot.switchButton),
            initializer = function(staticElement, settlementElementData)
                staticElement.levelMapSwitchBtn:InitSwitchButton(
                    settlementElementData.targetLevelId, settlementElementData.directionAngle
                )
            end,
            componentGetter = function(staticElement)
                return staticElement.levelMapSwitchBtn
            end,
            needHideInTier = true,
        },
        [ElementType.NarrativeAreaText] = {
            cache = LuaNodeCache(staticElements.narrativeAreaText, backRoot.narrativeAreaText),
            initializer = function(staticElement, elementData)
                staticElement.narrativeAreaText:InitNarrativeAreaText(elementData)
            end,
            componentGetter = function(staticElement)
                return staticElement.narrativeAreaText
            end,
            needHideInTier = true,
        },
        [ElementType.FacMainRegion] = {
            cache = LuaNodeCache(staticElements.facMainRegion, bottomRoot.facMainRegion),
            initializer = function(staticElement, settlementElementData)
                staticElement.facMainRegion:InitMainRegion(settlementElementData.regionLevelId, settlementElementData.regionPanelIndex)
            end,
            componentGetter = function(staticElement)
                return staticElement.facMainRegion
            end,
            refreshWithTier = function(staticElement, tierIndex, color)
                staticElement.image.color = color
            end
        },
        [ElementType.SettlementRegion] = {
            cache = LuaNodeCache(staticElements.settlementRegion, bottomRoot.settlementRegion),
            initializer = function(staticElement, settlementElementData)
                local rectCenterPos = staticElement.rectTransform.anchoredPosition
                local worldCenterPos = UILevelMapUtils.ConvertUILevelMapRectPosToWorldPos(
                    rectCenterPos, self.m_gridWorldLength, self.m_gridRectLength
                )
                staticElement.settlementRegion:InitSettlementRegion(settlementElementData.settlementId, worldCenterPos)
            end,
            componentGetter = function(staticElement)
                return staticElement.settlementRegion
            end,
            refreshWithTier = function(staticElement, tierIndex, color)
                if staticElement.settlementRegion:GetNeedRefreshSettlementRegionTier() then
                    staticElement.image.color = Color.white
                    staticElement.settlementRegion:RefreshSettlementRegionWithTier(tierIndex)
                else
                    staticElement.image.color = color
                end
            end
        },
        [ElementType.Crane] = {
            cache = LuaNodeCache(staticElements.crane, gridRoot.crane),
            initializer = function(staticElement, settlementElementData)
                staticElement.crane:InitCrane()
            end,
            componentGetter = function(staticElement)
                return staticElement.crane
            end,
            needHideInTier = true,
        },
        [ElementType.Misty] = {
            cache = LuaNodeCache(staticElements.misty, gridRoot.misty),
            initializer = function(staticElement, settlementElementData)
                staticElement.misty:InitMisty()
            end,
            componentGetter = function(staticElement)
                return staticElement.misty
            end,
            isVisible = function(settlementElementData)
                return CS.Beyond.UI.UILevelMapMisty.IsMistyVisible()
            end,
        },
        [ElementType.Common] = {
            cacheMap = {
                [RootLevel.GridRoot] = LuaNodeCache(staticElements.commonStaticElement, gridRoot.common),
                [RootLevel.BottomRoot] = LuaNodeCache(staticElements.commonStaticElement, bottomRoot.common),
                [RootLevel.BackRoot] = LuaNodeCache(staticElements.commonStaticElement, backRoot.common),
                [RootLevel.FrontRoot] = LuaNodeCache(staticElements.commonStaticElement, frontRoot.common),
            },
            getCache = function(elementData)
                local cacheMap = self.m_staticElementInitializer[ElementType.Common].cacheMap
                return cacheMap[elementData.rootLevel] or cacheMap[RootLevel.BackRoot]
            end,
            initializer = function(staticElement, elementData)
                staticElement.commonStaticElement:InitCommonStaticElement(elementData)
            end,
            componentGetter = function(staticElement)
                return staticElement.commonStaticElement
            end,
            isVisible = function(elementData)
                return true
            end,
            needHideInTier = true,
        },
    }
end

LevelMapLoader._InitPermanentElementsInCurrentMap = HL.Method() << function(self)
    self:_InitGameplayAreasInCurrentMap()
end

LevelMapLoader._InitGameplayAreasInCurrentMap = HL.Method() << function(self)
    local success, areaDataDict = self.m_mapManager:GetGameplayAreaDataDictByMapId(self.m_mapId)
    if not success then
        return
    end

    for areaId, _ in cs_pairs(areaDataDict) do
        self:_RefreshGameplayArea(areaId, true)
    end
end

LevelMapLoader._InitLoaderUpdateThread = HL.Method() << function(self)
    local uiCtrl = self:GetUICtrl()

    self.m_baseUpdateTick = LuaUpdate:Add("Tick", function(deltaTime)
        if not uiCtrl.m_isClosed and uiCtrl:IsShow() then
            self:_RefreshMissionTrackingMarksPosition()
        end
    end)
end

LevelMapLoader._InitGridsMask = HL.Method() << function(self)
    if not NotNull(self.m_gridsMaskFollowTarget) then
        self.view.element.gridsMask.gameObject:SetActive(false)
        return
    end
    self.view.element.gridsMask.gameObject:SetActive(true)
    self:_RefreshGridsMask()
    self.m_gridMaskUpdateTick = LuaUpdate:Add("LateTick", function(deltaTime)
        if self:GetUICtrl():IsShow() then


            self:_RefreshGridsMask()
        end
    end)
end

LevelMapLoader._IsExpectedStaticElementType = HL.Method(HL.Userdata).Return(HL.Boolean) << function(self, type)
    if not next(self.m_expectedStaticElementTypes) then
        return true
    end
    return self.m_expectedStaticElementTypes[type] == true
end






LevelMapLoader._OnLoadStateChanged = HL.Method(HL.Userdata) << function(self, hitStateChangeEventData)


    self:_RefreshChunksLoadedState(hitStateChangeEventData.addChunks, hitStateChangeEventData.removeChunks)

    self:_RefreshElementsLoadedState(hitStateChangeEventData.addGrids, hitStateChangeEventData.removeGrids)
end

LevelMapLoader._OnMarkInstDataModified = HL.Method(HL.String) << function(self, markInstId)
    local success, markRuntimeData = self.m_mapManager:GetMarkInstRuntimeData(markInstId)
    if not success then
        return
    end

    local belongGridLoaderData = markRuntimeData.belongGrid
    if belongGridLoaderData == nil or not self.view.loader:IsGridInHitList(belongGridLoaderData.gridId) then
        return
    end
    self:_RefreshGridMark(markInstId, markRuntimeData, markRuntimeData.isVisible)
end

LevelMapLoader._OnMarkInstDataChanged = HL.Method(HL.String, HL.Boolean) << function(self, markInstId, isAdd)
    local success, markRuntimeData = self.m_mapManager:GetMarkInstRuntimeData(markInstId)
    if not success then
        return
    end

    local belongGridLoaderData = markRuntimeData.belongGrid
    if belongGridLoaderData == nil or not self.view.loader:IsGridInHitList(belongGridLoaderData.gridId) then
        return
    end
    self:_RefreshGridMark(markInstId, markRuntimeData, isAdd)

    if self.m_onMarkInstDataChangedCallback ~= nil then
        self.m_onMarkInstDataChangedCallback()
    end
end

LevelMapLoader._OnMarkLineInstDataChanged = HL.Method(LineType) << function(self, lineType)
    local needRefreshLines = {}
    for _, gridData in pairs(self.m_loadedGridViewDataMap) do
        local lines = gridData.loaderData.lines
        if lines ~= nil and lines.Count > 0 then
            for lineId, lineData in pairs(lines) do
                if lineData.lineType == lineType then
                    if needRefreshLines[lineId] == nil then
                        needRefreshLines[lineId] = {
                            lineData = lineData,
                            loadCount = 1
                        }
                    else
                        needRefreshLines[lineId].loadCount = needRefreshLines[lineId].loadCount + 1
                    end
                end
            end
        end
    end


    local showFunc = function(showLineData, showCount, needShow)
        for _ = 1, showCount do
            self:_ShowGridLine(showLineData)
        end
    end
    local hideFunc = function(hideLineId, hideLineType, hideCount)
        for _ = 1, hideCount do
            self:_HideGridLine(hideLineId, hideLineType)
        end
    end


    local needRemoveLines = {}
    for lineId, lineViewData in pairs(self.m_loadedLineViewDataMap) do
        if needRefreshLines[lineId] == nil and lineViewData.lineData.lineType == lineType then
            needRemoveLines[lineId] = lineViewData
        end
    end
    for _, lineViewData in pairs(needRemoveLines) do
        hideFunc(lineViewData.lineId, lineViewData.lineType, lineViewData.loadCount)
    end


    for _, refreshLineData in pairs(needRefreshLines) do
        hideFunc(refreshLineData.lineData.lineId, refreshLineData.lineData.lineType, refreshLineData.loadCount)
        showFunc(refreshLineData.lineData, refreshLineData.loadCount)
    end
end

LevelMapLoader._OnCommonStaticElementStateChanged = HL.Method(HL.String, HL.Boolean) << function(self, elementId, isVisible)
    local viewData = self.m_loadedStaticElementViewDataMap[elementId]
    if viewData == nil then
        return
    end
    self:_RefreshStaticElementVisibleState(viewData, "Condition", isVisible)
end

LevelMapLoader._OnNarrativeTextStateChanged = HL.Method(HL.String, HL.String) << function(self, elementId, textId)
    local viewData = self.m_loadedStaticElementViewDataMap[elementId]
    if viewData == nil then
        return
    end
    local shouldShow = textId ~= nil and textId ~= ""
    self:_RefreshStaticElementVisibleState(viewData, "Condition", shouldShow)
end






LevelMapLoader._RefreshGridStaticElements = HL.Method(HL.Userdata, HL.Boolean) << function(self, loaderData, needShow)
    local staticElements = loaderData.staticElements
    if staticElements == nil or staticElements.Count == 0 then
        return
    end
    for staticElementId, staticElementData in pairs(staticElements) do
        if staticElementData ~= nil and self:_IsExpectedStaticElementType(staticElementData.type) then
            if needShow then
                local initializer = self.m_staticElementInitializer[staticElementData.type]
                if initializer ~= nil then
                    local isVisible = initializer.isVisible == nil or initializer.isVisible(staticElementData)
                    if isVisible then
                        if self.m_loadedStaticElementViewDataMap[staticElementId] == nil then
                            local cache = initializer.getCache and initializer.getCache(staticElementData) or initializer.cache
                            local staticElement = cache:Get()
                            staticElement.gameObject:SetActive(true)
                            staticElement.rectTransform.anchoredPosition = self:_GetRectPosByWorldPos(staticElementData.position)
                            local viewData = {
                                elementObj = staticElement,
                                initializer = initializer,
                                cache = cache,
                                displayTierId = staticElementData.displayTierId,
                            }
                            self.m_loadedStaticElementViewDataMap[staticElementId] = viewData
                            initializer.initializer(staticElement, staticElementData)
                        else
                            local existingViewData = self.m_loadedStaticElementViewDataMap[staticElementId]
                            self:_RefreshStaticElementVisibleState(existingViewData, "GridRefresh", true)
                            if initializer.initializer ~= nil then
                                initializer.initializer(existingViewData.elementObj, staticElementData)
                            end
                        end
                        self:_RefreshLoadedStaticElementStateWithTier(self.m_loadedStaticElementViewDataMap[staticElementId])
                    end
                end
            else
                local elementViewData = self.m_loadedStaticElementViewDataMap[staticElementId]
                if elementViewData ~= nil then
                    if elementViewData.initializer.componentGetter ~= nil then
                        local component = elementViewData.initializer.componentGetter(elementViewData.elementObj)
                        if component ~= nil and component.ClearComponent ~= nil then
                            component:ClearComponent()
                        end
                    end
                    self:_RefreshStaticElementVisibleState(elementViewData, "GridRefresh", false)
                end
            end
        end
    end
end

LevelMapLoader._RefreshStaticElementVisibleState = HL.Method(HL.Table, HL.String, HL.Boolean) << function(self, elementViewData, key, isVisible)
    if elementViewData == nil then
        return
    end
    if elementViewData.hideKeyList == nil then
        elementViewData.hideKeyList = {}
    end
    if isVisible then
        elementViewData.hideKeyList[key] = nil
    else
        elementViewData.hideKeyList[key] = true
    end
    elementViewData.elementObj.gameObject:SetActive(not next(elementViewData.hideKeyList))
end

LevelMapLoader._RefreshGridLines = HL.Method(HL.Userdata, HL.Boolean) << function(self, loaderData, needShow)
    local lines = loaderData.lines
    if lines == nil or lines.Count == 0 then
        return
    end

    for _, lineData in pairs(lines) do
        if needShow then
            self:_ShowGridLine(lineData)
        else
            self:_HideGridLine(lineData.lineId, lineData.lineType)
        end
    end
end

LevelMapLoader._ShowGridLine = HL.Method(HL.Userdata) << function(self, lineData)
    local lineType = lineData.lineType
    local lineId = lineData.lineId
    local lineCache = self.m_lineCaches[lineType]

    local notInView = self.m_loadedLineViewDataMap[lineId] == nil
    local needAdd = notInView
    if lineType == LineType.Power then
        needAdd = needAdd and self.m_loadedPowerLineCount < MapConst.LOADER_POWER_LINE_MAX_COUNT
    end
    if needAdd then
        local line = lineCache:Get()
        line.gameObject:SetActive(true)
        self:_RefreshLineBasicTransform(line, lineData)


        if lineType == LineType.Power then
            line.image.color = (lineData.hasPower or FactoryUtils.getIsInBackupPower()) and
                self.view.config.POWER_LINE_VALID_COLOR or
                self.view.config.POWER_LINE_INVALID_COLOR
            self.m_loadedPowerLineCount = self.m_loadedPowerLineCount + 1
        end

        self.m_loadedLineViewDataMap[lineId] = {
            lineId = lineId,
            lineType = lineData.lineType,
            lineData = lineData,
            lineObj = line,
            loadCount = 1,
        }
        line.gameObject.name = string.format("Line_%s", lineId)
    else
        if not notInView then
            self.m_loadedLineViewDataMap[lineId].loadCount = self.m_loadedLineViewDataMap[lineId].loadCount + 1
        end
    end
end

LevelMapLoader._HideGridLine = HL.Method(HL.String, LineType) << function(self, lineId, lineType)
    if self.m_loadedLineViewDataMap[lineId] == nil then
        return
    end

    local lineCache = self.m_lineCaches[lineType]
    local viewData = self.m_loadedLineViewDataMap[lineId]
    viewData.loadCount = math.max(0, viewData.loadCount - 1)
    local needRemove = viewData.loadCount == 0
    if needRemove then
        local loadedLine = viewData.lineObj
        loadedLine.gameObject:SetActive(false)
        if loadedLine.levelMapLine ~= nil then
            loadedLine.levelMapLine:ClearComponent()
        end
        loadedLine.gameObject.name = "CachedLine"
        lineCache:Cache(loadedLine)
        self.m_loadedLineViewDataMap[lineId] = nil

        if lineType == LineType.Power then
            self.m_loadedPowerLineCount = self.m_loadedPowerLineCount - 1
        end
    end
end

LevelMapLoader._RefreshLineBasicTransform = HL.Method(HL.Any, HL.Userdata) << function(self, line, lineData)
    if line == nil or lineData == nil then
        return
    end
    local startRectPos = self:_GetRectPosByWorldPos(lineData.startPosition)
    local endRectPos = self:_GetRectPosByWorldPos(lineData.endPosition)
    local direction = endRectPos - startRectPos
    local length = direction.magnitude
    line.rectTransform.sizeDelta = Vector2(length, line.rectTransform.rect.height)
    line.rectTransform.anchoredPosition = (startRectPos + endRectPos) / 2
    local angle = math.acos(Vector2.Dot(direction.normalized, Vector2.right)) * (180 / math.pi)
    if direction.y < 0 then
        angle = 360 - angle
    end
    line.rectTransform.localRotation = Quaternion.Euler(0, 0, angle)
    if line.levelMapLine ~= nil then
        line.levelMapLine:Init(length)
    end
end

LevelMapLoader._RefreshGameplayArea = HL.Method(HL.String, HL.Boolean) << function(self, areaId, needShow)
    if needShow then
        if self.m_loadedGameplayAreas[areaId] == nil then
            local success, areaData = self.m_mapManager:GetGameplayAreaInstRuntimeData(areaId)
            if success and areaData.mapId == self.m_mapId then

                local gameplayArea = self.m_gameplayAreaCache:Get()
                gameplayArea.rectTransform.anchoredPosition = self:_GetRectPosByWorldPos(areaData.position)
                gameplayArea.gameObject:SetActive(true)
                gameplayArea.levelMapGameplayArea:Init(areaData)
                self.m_loadedGameplayAreas[areaId] = gameplayArea
            end
        end
    else
        if self.m_loadedGameplayAreas[areaId] ~= nil then
            local gameplayArea = self.m_loadedGameplayAreas[areaId]
            gameplayArea.levelMapGameplayArea:ClearComponent()
            gameplayArea.gameObject:SetActive(false)
            self.m_gameplayAreaCache:Cache(gameplayArea)
            self.m_loadedGameplayAreas[areaId] = nil
        end
    end
end

LevelMapLoader._RefreshLoadedStaticElementStateWithTier = HL.Method(HL.Table) << function(self, elementViewData)
    local isInTier = self:_GetIsInTier()
    local color = isInTier and self.m_levelMapConfig.gridBaseTierColor or Color.white
    local elementObj = elementViewData.elementObj
    local initializer = elementViewData.initializer

    local displayTierId = elementViewData.displayTierId
    if displayTierId ~= nil and displayTierId > 0 then
        local shouldShow = isInTier and self.m_tierId == displayTierId
        self:_RefreshStaticElementVisibleState(elementViewData, "Tier", shouldShow)
        return
    end

    if initializer.needHideInTier then
        self:_RefreshStaticElementVisibleState(elementViewData, "Tier", not isInTier)
    elseif initializer.refreshWithTier ~= nil then
        initializer.refreshWithTier(elementObj, self.m_tierIndex, color)
    end
end

LevelMapLoader._RefreshLoadedStaticElementsStateWithTier = HL.Method() << function(self)
    for _, elementViewData in pairs(self.m_loadedStaticElementViewDataMap) do
        self:_RefreshLoadedStaticElementStateWithTier(elementViewData)
    end

    local isInTier = self:_GetIsInTier()
    self.view.element.staticElementBottomRoot.switchMask.gameObject:SetActive(not isInTier)
end

LevelMapLoader._RefreshLoadedMarksWithTier = HL.Method() << function(self)
    for _, markViewData in pairs(self.m_loadedMarkViewDataMap) do
        local markObj = markViewData.markObj
        self:_RefreshGridMarkTierState(markObj)
    end


    self:_RefreshMissionTrackingMarksWithTier()
    self:_RefreshGeneralTrackingMarkWithTier()
end

LevelMapLoader._RefreshMissionTrackingMarksWithTier = HL.Method() << function(self)
    for _, missionTrackingMarkObj in pairs(self.m_loadedMissionTrackingMarks) do
        local missionMarkObj = missionTrackingMarkObj
        self:_RefreshGridMarkTierState(missionMarkObj)
    end
end

LevelMapLoader._RefreshGeneralTrackingMarkWithTier = HL.Method() << function(self)
    self:_GetGeneralTrackingMarkIfNeed()
    if string.isEmpty(self.m_loadedGeneralTrackingMarkId) then
        return
    end
    self:_RefreshGridMarkTierState(self.view.element.trackingMarkRoot.generalTrackingMark)
end

LevelMapLoader._RefreshGridsMask = HL.Method() << function(self)
    if not self.view.element.gridsMask.gameObject.activeSelf then
        return
    end
    local targetRect = self.m_gridsMaskFollowTarget.rect
    self.view.element.gridsMask.sizeDelta = Vector2(targetRect.width, targetRect.height)
    self.view.element.gridsMask.position = self.m_gridsMaskFollowTarget.position
end

LevelMapLoader._RefreshPermanentStaticElementsLoadedState = HL.Method() << function(self)
    for elementLoaderData in cs_pairs(self.view.loader.permanentStaticElements) do
        if self.view.loader:IsPermanentStaticElementInHitList(elementLoaderData.elementId) then
            if self.m_loadedStaticElementViewDataMap[elementLoaderData.elementId] == nil then
                self:_LoadPermanentStaticElement(elementLoaderData)
            else
                local viewData = self.m_loadedStaticElementViewDataMap[elementLoaderData.elementId]
                if viewData.delayUnloadKey ~= nil then
                    self:_RemoveMapDelayAction(viewData.delayUnloadKey)
                end
            end
        else
            if self.m_loadedStaticElementViewDataMap[elementLoaderData.elementId] ~= nil then
                local viewData = self.m_loadedStaticElementViewDataMap[elementLoaderData.elementId]
                if self.m_needOptimizePerformance then
                    local key = self:_AddMapDelayAction(
                        elementLoaderData.elementId,
                        function()
                            viewData.delayUnloadKey = nil
                            self:_UnloadPermanentStaticElement(elementLoaderData.elementId)
                        end
                    )
                    viewData.delayUnloadKey = key
                else
                    self:_UnloadPermanentStaticElement(elementLoaderData.elementId)
                end
            end
        end
    end
end








LevelMapLoader._GetChunkResourcePath = HL.Method(HL.Userdata).Return(HL.String) << function(self, loaderData)
    local chunkId = loaderData.chunkId
    local levelId = loaderData.levelId
    if self.m_chunkResourcePathCache[levelId] == nil then
        self.m_chunkResourcePathCache[levelId] = UILevelMapUtils.GetUILevelMapChunksFolderByLevelId(
            levelId, false
        )
    end
    if self.m_chunkResourcePathCache[chunkId] == nil then
        local folderPath = self.m_chunkResourcePathCache[levelId]
        self.m_chunkResourcePathCache[chunkId] = UIUtils.getSpritePath(folderPath, chunkId)
    end
    return self.m_chunkResourcePathCache[chunkId]
end

LevelMapLoader._GetTierResourcePath = HL.Method(HL.Userdata).Return(HL.String) << function(self, loaderData)
    local tierLoadId = loaderData.tierLoadId
    local levelId = loaderData.levelId
    if self.m_tierResourcePathCache[levelId] == nil then
        self.m_tierResourcePathCache[levelId] = UILevelMapUtils.GetUILevelMapTiersFolderByLevelId(
            levelId, false
        )
    end
    if self.m_tierResourcePathCache[tierLoadId] == nil then
        local folderPath = self.m_tierResourcePathCache[levelId]
        self.m_tierResourcePathCache[tierLoadId] = UIUtils.getSpritePath(folderPath, tierLoadId)
    end
    return self.m_tierResourcePathCache[tierLoadId]
end

LevelMapLoader._GetMistResourcePath = HL.Method(HL.Userdata).Return(HL.String) << function(self, loaderData)
    local mistLoadId = loaderData.mistLoadId
    local levelId = loaderData.levelId
    if self.m_mistResourcePathCache[levelId] == nil then
        self.m_mistResourcePathCache[levelId] = UILevelMapUtils.GetUILevelMapMistsFolderByLevelId(
            levelId, true
        )
    end
    if self.m_mistResourcePathCache[mistLoadId] == nil then
        local folderPath = self.m_mistResourcePathCache[levelId]
        self.m_mistResourcePathCache[mistLoadId] = string.format(TEXTURE_LOAD_PATH_FORMAT, folderPath, mistLoadId)
    end
    return self.m_mistResourcePathCache[mistLoadId]
end

LevelMapLoader._GetChunkResourceLoadDataFromPool = HL.Method(HL.Any, HL.Function).Return(HL.Table) << function(self, loadKey, onComplete)
    if self.m_chunkResourceLoadDataPool[loadKey] == nil then
        local resourceLoadData = {
            loadKey = loadKey,
            isFinished = false,
            isDisposed = false,
            isTexture = false,
            initialDisposeCount = DISPOSE_DELAY_TIME / DISPOSE_COROUTINE_INTERVAL,
            resource = nil,
            handlerKey = nil,
        }
        self.m_chunkResourceLoadDataPool[loadKey] = resourceLoadData
    end
    local resourceLoadData = self.m_chunkResourceLoadDataPool[loadKey]
    resourceLoadData.onComplete = function()
        if not resourceLoadData.inUse then

            return
        end
        onComplete(resourceLoadData.resource)
    end
    return resourceLoadData
end

LevelMapLoader._GetChunkResourceByLoadKey = HL.Method(HL.Any).Return(HL.Userdata) << function(self, loadKey)
    if self.m_chunkResourceLoadDataPool[loadKey] == nil then
        return nil
    end
    return self.m_chunkResourceLoadDataPool[loadKey].resource
end

LevelMapLoader._LoadChunkResource = HL.Method(HL.Any, HL.String, HL.Boolean, HL.Function, HL.Opt(HL.Boolean)) << function(
    self, loadKey, path, isTexture, onComplete, forceSync
)
    local resourceLoadData = self:_GetChunkResourceLoadDataFromPool(loadKey, onComplete)
    resourceLoadData.inUse = true
    resourceLoadData.isTexture = true

    if resourceLoadData.isFinished then
        resourceLoadData.onComplete(resourceLoadData.resource)
        return
    end

    local asyncLoadFunc = isTexture and self.loader.LoadTextureAsync or self.loader.LoadSpriteAsync
    local syncLoadFunc = isTexture and self.loader.LoadTexture or self.loader.LoadSprite
    if self.m_needOptimizePerformance and not forceSync then
        if resourceLoadData.handlerKey == nil then
            resourceLoadData.handlerKey = asyncLoadFunc(self.loader, path, function(resource)
                if resourceLoadData.isDisposed then
                    return
                end
                resourceLoadData.isFinished = true
                resourceLoadData.resource = resource
                resourceLoadData.onComplete(resource)
            end)
        end
    else
        if resourceLoadData.handlerKey ~= nil then
            logger.error("资源加载已完成，但是handlerKey为nil", loadKey)
        end
        local resource, handlerKey = syncLoadFunc(self.loader, path)
        resourceLoadData.handlerKey = handlerKey
        resourceLoadData.isFinished = true
        resourceLoadData.resource = resource
        resourceLoadData.onComplete(resource)
    end
end

LevelMapLoader._DisposeChunkResource = HL.Method(HL.Any, HL.Opt(HL.Boolean)) << function(self, loadKey, disposeImmediately)
    if self.m_chunkResourceLoadDataPool[loadKey] == nil then
        return
    end

    local resourceLoadData = self.m_chunkResourceLoadDataPool[loadKey]
    resourceLoadData.inUse = false
    if not self.m_needOptimizePerformance or disposeImmediately then
        self:_DoDisposeChunkResource(loadKey)
        return
    end

    resourceLoadData.disposeCount = resourceLoadData.initialDisposeCount

    if self.m_delayDisposeResourceThread == nil then
        self:_StartDelayDisposeChunkResourceThread()
    end
end

LevelMapLoader._DoDisposeChunkResource = HL.Method(HL.Any) << function(self, loadKey)
    if self.m_chunkResourceLoadDataPool[loadKey] == nil then
        logger.error("卸载的资源不在池中", loadKey)
        return
    end
    local resourceLoadData = self.m_chunkResourceLoadDataPool[loadKey]
    resourceLoadData.resource = nil
    resourceLoadData.isDisposed = true
    self.loader:DisposeHandleByKey(resourceLoadData.handlerKey)
    resourceLoadData = nil
    self.m_chunkResourceLoadDataPool[loadKey] = nil
end

LevelMapLoader._DisposeAllChunkResources = HL.Method() << function(self)
    local disposeKeys = lume.keys(self.m_chunkResourceLoadDataPool)
    for _, loadKey in ipairs(disposeKeys) do
        self:_DisposeChunkResource(loadKey, true)
    end
end

LevelMapLoader._StartDelayDisposeChunkResourceThread = HL.Method() << function(self)
    self.m_delayDisposeResourceThread = self:_StartCoroutine(function()
        while true do
            local needDoDisposeKeyList = {}
            local allDone = true
            for loadKey, resourceLoadData in pairs(self.m_chunkResourceLoadDataPool) do
                if not resourceLoadData.inUse then
                    resourceLoadData.disposeCount = resourceLoadData.disposeCount - 1
                    if resourceLoadData.disposeCount <= 0 then
                        table.insert(needDoDisposeKeyList, loadKey)
                    end
                    allDone = false
                end
            end
            for _, loadKey in ipairs(needDoDisposeKeyList) do
                self:_DoDisposeChunkResource(loadKey)
            end
            if allDone then
                self:_StopDelayDisposeChunkResourceThread()
            end
            coroutine.wait(DISPOSE_COROUTINE_INTERVAL)
        end
    end)
end

LevelMapLoader._StopDelayDisposeChunkResourceThread = HL.Method() << function(self)
    self.m_delayDisposeResourceThread = self:_ClearCoroutine(self.m_delayDisposeResourceThread)
end















LevelMapLoader._GetRTDrawerDataFromPool = HL.Method(HL.Any, HL.Table).Return(HL.Table) << function(
    self, drawerKey, drawerInfo
)
    if self.m_rtDrawerDataPool[drawerKey] == nil then
        local data = {
            key = drawerKey,


            drawNode = drawerInfo.drawNode,
            needSetActiveNodeAfterDraw = drawerInfo.needSetActiveNodeAfterDraw,
            drawMaterial = self.loader:LoadMaterial(drawerInfo.drawMaterialPath),
            drawResourcePathGetFunc = drawerInfo.drawResourcePathGetFunc,
            drawResourceSortFunc = drawerInfo.drawResourceSortFunc,
            extraScale = drawerInfo.extraDrawScale,
            isTextureResource = drawerInfo.isTextureResource ~= false,


            lodType = nil,
            nativeScale = Vector3.one,
            drawList = {},


            isDrawing = false,
            lastIsActive = true,
            waitList = {},
            transformData = {},
        }
        self.m_rtDrawerDataPool[drawerKey] = data
    end
    return self.m_rtDrawerDataPool[drawerKey]
end

LevelMapLoader._GetRTDrawerData = HL.Method(HL.Any).Return(HL.Table) << function(self, drawerKey)
    if self.m_rtDrawerDataPool[drawerKey] == nil then
        logger.error("需要先从Pool中获取一次DrawerData")
    end
    return self.drawerPool[drawerKey]
end

LevelMapLoader._ClearDrawerLoadState = HL.Method(HL.Table) << function(self, drawerData)
    if next(drawerData.drawList) ~= nil and next(drawerData.waitList) ~= nil then
        for loadId, _ in pairs(drawerData.drawList) do
            self:_DisposeChunkResource(loadId, true)
        end
    end
    drawerData.drawList = {}
    drawerData.waitList = {}
end

LevelMapLoader._StartDrawRT = HL.Method(HL.Table, HL.Opt(HL.Function)) << function(self, drawerData, onRefreshFinish)
    local drawList = drawerData.drawList
    local waitList = drawerData.waitList
    local drawResourcePathGetFunc = drawerData.drawResourcePathGetFunc
    drawerData.isDrawing = true
    drawerData.lastIsActive = true

    if next(drawList) then

        for loadId, _ in pairs(drawList) do
            waitList[loadId] = true
        end

        for loadId, loaderData in pairs(drawList) do
            local texturePath = drawResourcePathGetFunc(loaderData)
            self:_LoadChunkResource(loadId, texturePath, drawerData.isTextureResource, function()
                waitList[loadId] = nil
                if next(waitList) == nil then
                    self:_DrawRT(drawerData)
                    self:_FinishDrawRT(drawerData, true, onRefreshFinish)
                end
            end)
        end
    else
        self:_FinishDrawRT(drawerData, false, onRefreshFinish)
    end
end

LevelMapLoader._FinishDrawRT = HL.Method(HL.Table, HL.Boolean, HL.Opt(HL.Function)) << function(self, drawerData, drawValid, onRefreshFinish)
    drawerData.isDrawing = false
    if drawerData.needSetActiveNodeAfterDraw then
        drawerData.drawNode.gameObject:SetActive(drawerData.lastIsActive and drawValid)
    end
    if onRefreshFinish ~= nil then
        onRefreshFinish()
    end
end

LevelMapLoader._DrawRT = HL.Method(HL.Table) << function(self, drawerData)
    local drawList = drawerData.drawList


    local left, right, bottom, top
    for _, loaderData in pairs(drawList) do
        if left == nil then
            left = loaderData.rectLeftBottom.x
            right = loaderData.rectRightTop.x
            bottom = loaderData.rectLeftBottom.y
            top = loaderData.rectRightTop.y
        else
            left = loaderData.rectLeftBottom.x < left and loaderData.rectLeftBottom.x or left
            right = loaderData.rectRightTop.x > right and loaderData.rectRightTop.x or right
            bottom = loaderData.rectLeftBottom.y < bottom and loaderData.rectLeftBottom.y or bottom
            top = loaderData.rectRightTop.y > top and loaderData.rectRightTop.y or top
        end
    end
    local center = Vector2((left + right) / 2, (top + bottom) / 2)
    local scale = drawerData.nativeScale / drawerData.extraScale
    local size = Vector2((right - left) / scale.x, (top - bottom) / scale.x)
    drawerData.transformData = {
        left = left,
        right = right,
        bottom = bottom,
        top = top,
        size = size,
        scale = scale,
        center = center,
    }


    self:_RefreshRTBasicState(drawerData)


    self:_ClearRTDrawState(drawerData)


    local sortedDrawList = {}
    if drawerData.drawResourceSortFunc == nil then
        for loadId, _ in pairs(drawList) do
            table.insert(sortedDrawList, loadId)
        end
        table.sort(sortedDrawList)
    else
        sortedDrawList = drawerData.drawResourceSortFunc(drawList)
    end

    for _, loadId in ipairs(sortedDrawList) do
        self:_RefreshRTDrawState(loadId, drawerData)
    end
end

LevelMapLoader._RefreshRTBasicState = HL.Method(HL.Table) << function(self, drawerData)
    if drawerData == nil then
        return
    end

    local rt = drawerData.rt
    local transformData = drawerData.transformData
    local drawNode = drawerData.drawNode
    local width, height = transformData.size.x, transformData.size.y
    width = math.ceil(width)
    height = math.ceil(height)

    local needRefreshState = rt == nil or rt.width * EXTRA_RT_SIZE_SCALE_DEFAULT < width or rt.height * EXTRA_RT_SIZE_SCALE_DEFAULT < height

    if needRefreshState then
        if not IsNull(rt) then

            self:_ReleaseRT(rt)
        end
        rt = CustomDrawRTManager.Instance:AllocateRenderTexture(math.ceil(width / EXTRA_RT_SIZE_SCALE_DEFAULT), math.ceil(height / EXTRA_RT_SIZE_SCALE_DEFAULT))
        rt.name = drawerData.key.."_"..rt.width.."_"..rt.height
        drawNode.rectTransform.sizeDelta = transformData.size
        drawNode.rectTransform.localScale = transformData.scale
        drawNode.image.texture = rt
        drawerData.rt = rt
    end

    local left = transformData.left
    local top = transformData.top
    local nodeSize = drawNode.rectTransform.sizeDelta
    local scaleRatio = transformData.scale.x
    drawNode.rectTransform.anchoredPosition = Vector2(left + nodeSize.x * scaleRatio / 2, top - nodeSize.y * scaleRatio / 2)
end

LevelMapLoader._ClearRTDrawState = HL.Method(HL.Table) << function(self, drawerData)
    if drawerData == nil then
        return
    end

    local rt = drawerData.rt
    if rt == nil then
        return
    end

    CustomDrawRTManager.Instance:ClearTexture(rt)
end

LevelMapLoader._RefreshRTDrawState = HL.Method(HL.String, HL.Table) << function(self, loadId, drawerData)
    if drawerData == nil then
        return
    end

    local transformData = drawerData.transformData
    local loaderData = drawerData.drawList[loadId]
    local resource = self:_GetChunkResourceByLoadKey(loadId)
    local texture = drawerData.isTextureResource and resource or resource.texture
    local totalScaleRatio = 1 / transformData.scale.x
    local extraScaleRatio = drawerData.extraScale
    local targetRect = Rect(
        totalScaleRatio * (loaderData.rectLeftBottom.x - transformData.left),
        totalScaleRatio * (transformData.top - loaderData.rectRightTop.y),
        extraScaleRatio * texture.width,
        extraScaleRatio * texture.height
    )


    if targetRect.x + targetRect.width > drawerData.rt.width * EXTRA_RT_SIZE_SCALE_DEFAULT then
        targetRect.width = drawerData.rt.width * EXTRA_RT_SIZE_SCALE_DEFAULT - targetRect.x
    end
    if targetRect.y + targetRect.height > drawerData.rt.height * EXTRA_RT_SIZE_SCALE_DEFAULT then
        targetRect.height = drawerData.rt.height * EXTRA_RT_SIZE_SCALE_DEFAULT - targetRect.y
    end

    targetRect = Rect(targetRect.x / EXTRA_RT_SIZE_SCALE_DEFAULT,targetRect.y / EXTRA_RT_SIZE_SCALE_DEFAULT,targetRect.width / EXTRA_RT_SIZE_SCALE_DEFAULT,targetRect.height / EXTRA_RT_SIZE_SCALE_DEFAULT)

    CustomDrawRTManager.Instance:DrawTexture(drawerData.rt, targetRect, texture, drawerData.drawMaterial)

    if not self.m_retainTextureAfterDraw then
        self:_DisposeChunkResource(loadId, true)
    end
end

LevelMapLoader._ClearAllDrawersRT = HL.Method() << function(self)
    for _, drawerData in pairs(self.m_rtDrawerDataPool) do
        self:_ReleaseRT(drawerData.rt)
        drawerData.rt = nil
        drawerData.drawNode.gameObject:SetActive(false)
    end
end

LevelMapLoader._ReleaseRT = HL.Method(HL.Userdata) << function(self, rt)
    if IsNull(rt) then
        return
    end
    local rtManager = CustomDrawRTManager.Instance
    if rtManager == nil then
        return
    end
    rtManager:ReleaseRenderTexture(rt)
end

LevelMapLoader._SetDrawNodeActiveState = HL.Method(HL.Table, HL.Boolean) << function(self, drawerData, active)
    if drawerData.isDrawing then
        drawerData.lastIsActive = active
    else
        drawerData.drawNode.gameObject:SetActive(active)
    end
end







LevelMapLoader._RefreshGOChunkCells = HL.Method(HL.Table, HL.Table, ChunkLODType, HL.Boolean) << function(self, drawer, loadEntries, lodType, activateParentOnComplete)
    local batchWaitList = {}
    local batchCells = {}

    for chunkId, loaderData in pairs(loadEntries) do
        drawer.chunkSortMap[chunkId] = { levelId = loaderData.levelId, levelNumId = loaderData.levelNumId }

        local chunkCell = drawer.nodeCache:Get()
        chunkCell.image.material = drawer.stencilMaterial
        chunkCell.gameObject:SetActive(false)

        local spritePath = drawer.resourcePathGetFunc(loaderData)
        batchWaitList[chunkId] = true
        batchCells[chunkId] = {
            chunkCell = chunkCell,
            rectPos = loaderData.rectPos,
        }

        drawer.chunkCellMap[chunkId] = chunkCell

        self:_LoadChunkResource(chunkId, spritePath, false, function(sprite)
            if batchWaitList[chunkId] == nil then
                return
            end
            batchWaitList[chunkId] = nil

            local cellInfo = batchCells[chunkId]
            if drawer.chunkCellMap[chunkId] == cellInfo.chunkCell then
                cellInfo.sprite = sprite
            end


            for key, _ in pairs(batchWaitList) do
                if drawer.chunkCellMap[key] ~= batchCells[key].chunkCell then
                    batchWaitList[key] = nil
                end
            end

            if next(batchWaitList) == nil then
                for key, info in pairs(batchCells) do
                    if drawer.chunkCellMap[key] == info.chunkCell and info.sprite ~= nil then
                        info.chunkCell.image.sprite = info.sprite
                        info.chunkCell.image:SetNativeSize()
                        info.chunkCell.rectTransform.localScale = self:_GetNativeScaleByLODType(lodType)
                        info.chunkCell.rectTransform.anchoredPosition = info.rectPos
                        info.chunkCell.gameObject:SetActive(true)
                    end
                end
                self:_ReorderGOChunkCells(drawer)
                if activateParentOnComplete then
                    drawer.parentNode.gameObject:SetActive(true)
                end
            end
        end)
    end
end


LevelMapLoader._RecycleGOChunkCell = HL.Method(HL.Table, HL.String, HL.Boolean) << function(self, drawer, chunkId, disposeResource)
    local chunkCell = drawer.chunkCellMap[chunkId]
    if chunkCell ~= nil then
        if disposeResource then
            self:_DisposeChunkResource(chunkId)
        end
        chunkCell.image.sprite = nil
        drawer.nodeCache:Cache(chunkCell)
    end
    drawer.chunkCellMap[chunkId] = nil
    drawer.chunkSortMap[chunkId] = nil
end


LevelMapLoader._ClearGOChunkDrawer = HL.Method(HL.Table, HL.Boolean) << function(self, drawer, disposeResource)
    for chunkId, chunkCell in pairs(drawer.chunkCellMap) do
        if disposeResource then
            self:_DisposeChunkResource(chunkId, true)
        end
        chunkCell.image.sprite = nil
        drawer.nodeCache:Cache(chunkCell)
    end
    drawer.chunkCellMap = {}
    drawer.chunkSortMap = {}
    drawer.overlapEntryPool = {}
    drawer.parentNode.gameObject:SetActive(false)
end


LevelMapLoader._CacheGOChunkCells = HL.Method(HL.Table, HL.Table) << function(self, drawer, visibleChunkIds)
    local recycleList = {}
    for chunkId, _ in pairs(drawer.chunkCellMap) do
        if visibleChunkIds[chunkId] ~= true then
            table.insert(recycleList, chunkId)
        end
    end
    for _, chunkId in ipairs(recycleList) do
        self:_RecycleGOChunkCell(drawer, chunkId, false)
    end
end



LevelMapLoader._ReorderGOChunkCells = HL.Method(HL.Table) << function(self, drawer)
    local sortList = {}
    for chunkId, _ in pairs(drawer.chunkCellMap) do
        local info = drawer.chunkSortMap[chunkId]
        local sortKey = info and (info.levelId == self.m_levelId and -1 or info.levelNumId) or 0
        table.insert(sortList, {
            chunkId = chunkId,
            sortKey = sortKey,
        })
    end
    table.sort(sortList, function(a, b)
        if a.sortKey ~= b.sortKey then
            return a.sortKey < b.sortKey
        end
        return a.chunkId < b.chunkId
    end)

    for _, entry in ipairs(sortList) do
        local cell = drawer.chunkCellMap[entry.chunkId]
        if cell ~= nil then
            cell.rectTransform:SetAsLastSibling()
            cell.gameObject.name = entry.chunkId
        end
    end
end

LevelMapLoader._ReorderAllGOChunkDrawers = HL.Method() << function(self)
    if not self.m_useChunkGODrawer or self.m_chunkGODrawers == nil then
        return
    end
    for _, drawer in pairs(self.m_chunkGODrawers) do
        self:_ReorderGOChunkCells(drawer)
    end
end

LevelMapLoader._ProcessChunksWithCurrentLevelPriority = HL.Method(HL.Userdata, HL.Function) << function(self, chunks, action)

    for loaderData in cs_pairs(chunks) do
        if loaderData.levelId == self.m_levelId then
            action(loaderData)
        end
    end

    for loaderData in cs_pairs(chunks) do
        if loaderData.levelId ~= self.m_levelId then
            action(loaderData)
        end
    end
end








LevelMapLoader._ProcessAddChunks = HL.Method(HL.Userdata, ChunkLODType) << function(self, addChunks, lodType)
    local drawer = self.m_chunkGODrawers[lodType]
    if drawer == nil then
        return
    end

    local isHighLOD = lodType == ChunkLODType.High
    local loadEntries = {}
    local hasEntries = false

    if isHighLOD then

        self:_ProcessChunksWithCurrentLevelPriority(addChunks, function(loaderData)
            local globalId = loaderData.globalId
            if globalId ~= 0 then
                local overlapEntry = drawer.overlapEntryPool[globalId]
                if overlapEntry ~= nil then
                    overlapEntry.refCount = overlapEntry.refCount + 1
                else
                    drawer.overlapEntryPool[globalId] = { refCount = 1, ownerChunkId = loaderData.chunkId }
                    loadEntries[loaderData.chunkId] = loaderData
                    hasEntries = true
                end
            else
                loadEntries[loaderData.chunkId] = loaderData
                hasEntries = true
            end
        end)
    else
        for loaderData in cs_pairs(addChunks) do
            loadEntries[loaderData.chunkId] = loaderData
            hasEntries = true
        end
    end

    if hasEntries then
        self:_RefreshGOChunkCells(drawer, loadEntries, lodType, true)
    else
        drawer.parentNode.gameObject:SetActive(true)
    end
end





LevelMapLoader._ProcessRemoveChunks = HL.Method(HL.Userdata, ChunkLODType) << function(self, removeChunks, lodType)
    local drawer = self.m_chunkGODrawers[lodType]
    if drawer == nil then
        return
    end

    local isHighLOD = lodType == ChunkLODType.High

    for loaderData in cs_pairs(removeChunks) do
        if isHighLOD then
            local globalId = loaderData.globalId
            if globalId ~= 0 then
                local overlapEntry = drawer.overlapEntryPool[globalId]
                if overlapEntry ~= nil then
                    overlapEntry.refCount = overlapEntry.refCount - 1
                    if overlapEntry.refCount <= 0 then

                        local ownerChunkId = overlapEntry.ownerChunkId
                        self:_RecycleGOChunkCell(drawer, ownerChunkId, true)
                        drawer.overlapEntryPool[globalId] = nil
                    end

                end
            else

                local chunkId = loaderData.chunkId
                self:_RecycleGOChunkCell(drawer, chunkId, true)
            end
        else

            local chunkId = loaderData.chunkId
            self:_RecycleGOChunkCell(drawer, chunkId, true)
        end
    end
end



LevelMapLoader._RecycleAllGOChunkCells = HL.Method() << function(self)
    if not self.m_useChunkGODrawer or self.m_chunkGODrawers == nil then
        return
    end
    for _, drawer in pairs(self.m_chunkGODrawers) do
        self:_ClearGOChunkDrawer(drawer, false)
    end
end





LevelMapLoader._RefreshChunksLoadedState = HL.Method(HL.Userdata, HL.Userdata) << function(self, addChunks, removeChunks)
    local chunks, lodType = self.view.loader.hitChunks, self.view.loader.checkLODType


    if self.m_useChunkGODrawer then
        self:_ProcessRemoveChunks(removeChunks, lodType)
        self:_ProcessAddChunks(addChunks, lodType)
    else
        self:_RefreshLoadedChunks(chunks, lodType)
    end


    self:_RefreshLoadedChunksTiers()
    self:_RefreshLoaderChunksTierColorState(self:_GetIsInTier())


    if self.m_forbidMistRefreshAfterGridChange then
        self.m_waitMistRefreshAfterGridChange = true
    else
        self:_RefreshLoadedChunksMists(MistLoadType.Normal)
        self.m_waitMistRefreshAfterGridChange = false
    end
end

LevelMapLoader._RefreshElementsLoadedState = HL.Method(HL.Userdata, HL.Userdata) << function(self, addGrids, removeGrids)



    for loaderData in cs_pairs(removeGrids) do
        self:_RefreshGridsElements(loaderData, false)
    end


    for loaderData in cs_pairs(addGrids) do
        self:_RefreshGridsElements(loaderData, true)
    end

    if self.m_onGridsLoadedStateChange ~= nil then
        self.m_onGridsLoadedStateChange()
    end
end

LevelMapLoader._RefreshGridsElements = HL.Method(HL.Userdata, HL.Boolean) << function(self, loaderData, isAdd)
    if isAdd then
        self:_RefreshGrid(loaderData, true)
        self:_RefreshGridMarks(loaderData, true)
        self:_RefreshGridStaticElements(loaderData, true)
        self:_RefreshGridLines(loaderData, true)
    else
        self:_RefreshGrid(loaderData, false)
        self:_RefreshGridMarks(loaderData, false)
        self:_RefreshGridStaticElements(loaderData, false)
        self:_RefreshGridLines(loaderData, false)
    end
end

LevelMapLoader._GetNativeScaleByLODType = HL.Method(ChunkLODType, HL.Opt(HL.Boolean)).Return(Vector3) << function(self, lodType, isMist)
    local cache = isMist and self.m_mistLODScaleCache or self.m_lodScaleCache
    if cache[lodType] == nil then
        local scale = 1 / self.m_levelMapConfig:GetChunkGridsCountByLODType(lodType)
        if isMist then
            scale = scale * self.m_levelMapConfig.mistExtraLODRatio
        end
        cache[lodType] = Vector3.one / scale
    end
    return cache[lodType]
end

LevelMapLoader._GetExtraLowScale = HL.Method().Return(HL.Number) << function(self)
    if self.m_needExtraLowScaleRT then
        return DeviceInfo.isMobile and EXTRA_LOW_RT_DRAW_SCALE_MOBILE or EXTRA_LOW_RT_DRAW_SCALE_DEFAULT
    else
        return 1
    end
end

LevelMapLoader._InitChunkDrawer = HL.Method() << function(self)
    local loadedChunks = self.view.element.loadedChunks
    local chunkSources = self.view.source.chunks
    local lodParentNodes = {
        [ChunkLODType.Low] = loadedChunks.lowLODChunks,
        [ChunkLODType.Medium] = loadedChunks.mediumLODChunks,
        [ChunkLODType.High] = loadedChunks.highLODChunks,
    }

    if self.m_useChunkGODrawer then
        local goChunkSource = chunkSources.goChunk

        self.m_chunkGODrawers = {}
        for lodType, parentNode in pairs(lodParentNodes) do
            local stencilMat = self:_CreateChunkStencilMaterial(goChunkSource.image.material, lodType)
            self.m_chunkGODrawers[lodType] = {
                parentNode = parentNode,
                nodeCache = LuaNodeCache(goChunkSource, parentNode),
                overlapEntryPool = {},
                chunkCellMap = {},
                chunkSortMap = {},
                resourcePathGetFunc = function(loaderData)
                    return self:_GetChunkResourcePath(loaderData)
                end,
                stencilMaterial = stencilMat,
            }
            parentNode.gameObject:SetActive(false)
        end
        self.m_chunkRTDrawers = {}
    else
        local rtChunkSource = chunkSources.rtChunk

        self.m_chunkRTDrawers = {}
        for lodType, parentNode in pairs(lodParentNodes) do

            local rtNodeCache = LuaNodeCache(rtChunkSource, parentNode)
            local rtChunkCell = rtNodeCache:Get()

            self.m_chunkRTDrawers[lodType] = self:_GetRTDrawerDataFromPool(
                string.format(CHUNK_DRAWER_KEY_FORMAT, lodType:GetHashCode()),
                {
                    drawNode = rtChunkCell,
                    needSetActiveNodeAfterDraw = true,
                    drawMaterialPath = CHUNK_DRAW_MATERIAL_PATH,
                    isTextureResource = false,
                    drawResourcePathGetFunc = function(loaderData)
                        return self:_GetChunkResourcePath(loaderData)
                    end,
                    drawResourceSortFunc = function(drawList)
                        local sortTempList = {}
                        for loadId, loaderData in pairs(drawList) do
                            table.insert(sortTempList, {
                                loadId = loadId,
                                sortId = loaderData.levelId == self.m_levelId and -1 or loaderData.levelNumId
                            })
                        end
                        table.sort(sortTempList, Utils.genSortFunction({ "sortId" }, false))
                        local sortedDrawList = {}
                        for _, sortTempData in ipairs(sortTempList) do
                            table.insert(sortedDrawList, sortTempData.loadId)
                        end
                        return sortedDrawList
                    end,
                    extraDrawScale = self:_GetExtraLowScale()
                }
            )

            rtChunkCell.gameObject:SetActive(false)
        end
    end
end


local LOD_STENCIL_BITS = {
    [ChunkLODType.Low] = 1,
    [ChunkLODType.Medium] = 2,
    [ChunkLODType.High] = 4,
}

LevelMapLoader._CreateChunkStencilMaterial = HL.Method(HL.Userdata, ChunkLODType).Return(HL.Userdata) << function(self, sourceMaterial, lodType)
    local stencilBit = LOD_STENCIL_BITS[lodType]
    local mat = Unity.Material(sourceMaterial)
    mat:SetFloat("_Stencil", stencilBit)
    mat:SetFloat("_StencilComp", 6)
    mat:SetFloat("_StencilOp", 2)
    mat:SetFloat("_StencilReadMask", stencilBit)
    mat:SetFloat("_StencilWriteMask", stencilBit)
    return mat
end

LevelMapLoader._GetChunkNodeByLODType = HL.Method(ChunkLODType).Return(HL.Any) << function(self, lodType)
    if self.m_useChunkGODrawer then
        return self.m_chunkGODrawers[lodType].parentNode
    end
    return self.m_chunkRTDrawers[lodType].drawNode
end




LevelMapLoader._RefreshLoadedChunks = HL.Method(HL.Userdata, ChunkLODType, HL.Opt(HL.Function)) << function(self, chunks, lodType, onRefreshFinish)
    local drawerData = self.m_chunkRTDrawers[lodType]
    if drawerData == nil then
        return
    end
    self:_ClearDrawerLoadState(drawerData)
    drawerData.lodType = lodType
    drawerData.nativeScale = self:_GetNativeScaleByLODType(lodType)


    local isHighLOD = lodType == ChunkLODType.High
    local dedupGlobalIds = isHighLOD and {} or nil
    for loaderData in cs_pairs(chunks) do
        if isHighLOD then
            local globalId = loaderData.globalId
            if globalId ~= 0 then
                if dedupGlobalIds[globalId] then

                else
                    dedupGlobalIds[globalId] = true
                    drawerData.drawList[loaderData.chunkId] = loaderData
                end
            else
                drawerData.drawList[loaderData.chunkId] = loaderData
            end
        else
            drawerData.drawList[loaderData.chunkId] = loaderData
        end
    end
    self:_StartDrawRT(drawerData, onRefreshFinish)
end

LevelMapLoader._RefreshGrid = HL.Method(HL.Userdata, HL.Boolean) << function(self, loaderData, needShow)
    if needShow then
        self.m_loadedGridViewDataMap[loaderData.gridId] = {
            loaderData = loaderData,
        }
    else
        self.m_loadedGridViewDataMap[loaderData.gridId] = nil
    end
end





LevelMapLoader._RefreshLoadedChunksTiers = HL.Method() << function(self)
    local chunks, lodType
    if self.view.loader.needCullChunks then
        chunks, lodType = self.view.loader.cullingVisibleChunks, self.view.loader.cullingLODType
    else
        chunks, lodType = self.view.loader.hitChunks, self.view.loader.checkLODType
    end

    local isInTier = self:_GetIsInTier()

    local refreshTiers = {}
    if isInTier then
        for chunkLoaderData in cs_pairs(chunks) do
            if chunkLoaderData.needLoadTiers then
                for tierId, tierLoaderData in pairs(chunkLoaderData.tiers) do
                    if tierId == self.m_tierId then
                        refreshTiers[tierLoaderData.tierLoadId] = tierLoaderData
                    end
                end
            end
        end
    end

    local unusedTiers = {}
    for tierLoadId, _ in pairs(self.m_loadedTierViewDataMap) do
        if refreshTiers[tierLoadId] == nil then
            unusedTiers[tierLoadId] = true
        end
    end
    for tierLoadId, _ in pairs(unusedTiers) do
        local tierCell = self.m_loadedTierViewDataMap[tierLoadId].cell
        tierCell.image.sprite = nil
        self.m_tierCache:Cache(tierCell)
        self:_DisposeChunkResource(tierLoadId, true)
        self.m_loadedTierViewDataMap[tierLoadId] = nil
    end

    for tierLoadId, tierLoaderData in pairs(refreshTiers) do
        if self.m_loadedTierViewDataMap[tierLoadId] == nil then
            local tierCell = self.m_tierCache:Get()

            tierCell.rectTransform.anchoredPosition = tierLoaderData.rectCenter

            local tierSpritePath = self:_GetTierResourcePath(tierLoaderData)
            self:_LoadChunkResource(tierLoadId, tierSpritePath, false, function(tierSprite)
                tierCell.image.sprite = tierSprite
                tierCell.image:SetNativeSize()
                tierCell.rectTransform.localScale = self:_GetNativeScaleByLODType(lodType)
            end, true)

            self.m_loadedTierViewDataMap[tierLoadId] = {
                loaderData = tierLoaderData,
                cell = tierCell,
            }
        end
    end
end

LevelMapLoader._RefreshLoaderChunksTierColorState = HL.Method(HL.Boolean) << function(self, inTier)
    local color = inTier and self.m_levelMapConfig.gridBaseTierColor or Color.white
    if self.m_useChunkGODrawer then
        if self.m_chunkGODrawers == nil then
            return
        end
        for _, drawer in pairs(self.m_chunkGODrawers) do
            local canvasGroup = drawer.parentNode.canvasGroup
            if canvasGroup ~= nil then
                UIUtils.changeColorExceptAlpha(canvasGroup, color)
            end
        end
    else
        UIUtils.changeColorExceptAlpha(self:_GetChunkNodeByLODType(ChunkLODType.Low).image, color)
        UIUtils.changeColorExceptAlpha(self:_GetChunkNodeByLODType(ChunkLODType.Medium).image, color)
        UIUtils.changeColorExceptAlpha(self:_GetChunkNodeByLODType(ChunkLODType.High).image, color)
    end
end





LevelMapLoader._InitMistDrawer = HL.Method() << function(self)
    local element = self.view.element
    local drawNodes = {
        [MistLoadType.Normal] = element.mist,
        [MistLoadType.Animation] = element.animMist,
        [MistLoadType.LOD] = element.lodMist,
    }

    self.m_mistDrawers = {}
    for mistLoadType, drawNode in pairs(drawNodes) do
        self.m_mistDrawers[mistLoadType] = self:_GetRTDrawerDataFromPool(
            string.format(MIST_DRAWER_KEY_FORMAT, mistLoadType),
            {
                drawNode = drawNode,
                needSetActiveNodeAfterDraw = mistLoadType ~= MistLoadType.Animation,
                drawMaterialPath = MIST_DRAW_MATERIAL_PATH,
                drawResourcePathGetFunc = function(loaderData)
                    return self:_GetMistResourcePath(loaderData)
                end,
                extraDrawScale = self:_GetExtraLowScale()
            }
        )
        drawNode.gameObject:SetActive(false)
    end
end

LevelMapLoader._RefreshLoadedChunksMists = HL.Method(HL.Number, HL.Opt(HL.Table, HL.Function)) << function(
    self, mistLoadType, customRefreshInfo, onRefreshFinish
)
    local drawerData = self.m_mistDrawers[mistLoadType]
    self:_ClearDrawerLoadState(drawerData)
    drawerData.lodType = nil

    local chunks = self.view.loader.hitChunks
    local overrideMistIdList
    if customRefreshInfo ~= nil and customRefreshInfo.overrideMistIdList ~= nil then
        overrideMistIdList = customRefreshInfo.overrideMistIdList
    end
    local refreshLODType
    if customRefreshInfo ~= nil and customRefreshInfo.refreshLODType ~= nil then
        refreshLODType = customRefreshInfo.refreshLODType
        chunks = self.view.loader.cullingVisibleChunks
    end

    for chunkLoaderData in cs_pairs(chunks) do
        if chunkLoaderData.needLoadMists then
            for mistId, mistLoaderData in pairs(chunkLoaderData.mists) do
                local needLoad = false
                if overrideMistIdList then
                    if overrideMistIdList[mistId] then
                        needLoad = true
                    end
                else
                    needLoad = true
                    if self.m_mistSystem:IsUnlockedMistMap(mistId) then
                        needLoad = false
                    elseif refreshLODType ~= nil and refreshLODType ~= chunkLoaderData.lodType then
                        needLoad = false
                    end
                end
                if needLoad then
                    local mistLoadId = mistLoaderData.mistLoadId
                    drawerData.drawList[mistLoadId] = mistLoaderData
                    if drawerData.lodType == nil then
                        drawerData.lodType = chunkLoaderData.lodType
                        drawerData.nativeScale = self:_GetNativeScaleByLODType(drawerData.lodType, true)
                    end
                end
            end
        end
    end

    self:_StartDrawRT(drawerData, onRefreshFinish)
end





LevelMapLoader._RefreshChunksCullingLOD = HL.Method(ChunkLODType, ChunkLODType) << function(self, lastLODType, currLODType)
    if self.m_useChunkGODrawer then
        if lastLODType ~= ChunkLODType.Low then
            self.m_chunkGODrawers[lastLODType].parentNode.gameObject:SetActive(false)
        end
        if currLODType ~= ChunkLODType.Low then
            self.m_chunkGODrawers[currLODType].parentNode.gameObject:SetActive(true)
        end
        self.m_lateHideChunkLODType = nil
        return
    end

    if lastLODType == ChunkLODType.Medium and currLODType == ChunkLODType.High and self.m_chunkRTDrawers[currLODType].isDrawing then
        self.m_lateHideChunkLODType = ChunkLODType.Medium
        return
    end

    if lastLODType ~= ChunkLODType.Low then
        self:_SetDrawNodeActiveState(self.m_chunkRTDrawers[lastLODType], false)
    end
    if currLODType ~= ChunkLODType.Low then
        self:_SetDrawNodeActiveState(self.m_chunkRTDrawers[currLODType], true)
    end

    self.m_lateHideChunkLODType = nil
end






LevelMapLoader._RefreshGODrawerCullingChunks = HL.Method(HL.Userdata, ChunkLODType) << function(self, chunks, lodType)
    local drawer = self.m_chunkGODrawers[lodType]
    if drawer == nil then
        return
    end

    local isHighLOD = lodType == ChunkLODType.High
    local visibleChunkIds = {}
    local newEntries = {}

    if isHighLOD then

        local dedupGlobalIds = {}
        self:_ProcessChunksWithCurrentLevelPriority(chunks, function(loaderData)
            local chunkId = loaderData.chunkId
            local globalId = loaderData.globalId
            local shouldProcess = true
            if globalId ~= 0 then
                if dedupGlobalIds[globalId] then
                    shouldProcess = false
                else
                    dedupGlobalIds[globalId] = true
                end
            end
            if shouldProcess then
                visibleChunkIds[chunkId] = true
                if drawer.chunkCellMap[chunkId] == nil then
                    newEntries[chunkId] = loaderData
                end
            end
        end)
    else
        for loaderData in cs_pairs(chunks) do
            local chunkId = loaderData.chunkId
            visibleChunkIds[chunkId] = true
            if drawer.chunkCellMap[chunkId] == nil then
                newEntries[chunkId] = loaderData
            end
        end
    end


    self:_CacheGOChunkCells(drawer, visibleChunkIds)

    if next(newEntries) ~= nil then
        self:_RefreshGOChunkCells(drawer, newEntries, lodType, false)
    end
end


LevelMapLoader._RefreshCullingChunks = HL.Method(HL.Userdata, ChunkLODType) << function(self, chunks, lodType)
    if self.m_useChunkGODrawer then
        self:_RefreshGODrawerCullingChunks(chunks, lodType)
        if self.m_lateHideChunkLODType ~= nil then
            self.m_chunkGODrawers[self.m_lateHideChunkLODType].parentNode.gameObject:SetActive(false)
            self.m_lateHideChunkLODType = nil
        end
    else
        self:_RefreshLoadedChunks(chunks, lodType, function()
            if self.m_lateHideChunkLODType ~= nil then
                self:_SetDrawNodeActiveState(self.m_chunkRTDrawers[self.m_lateHideChunkLODType], false)
                self.m_lateHideChunkLODType = nil
            end
        end)
    end
end

LevelMapLoader._RefreshChunksCullingState = HL.Method() << function(self)
    local chunks, lodType = self.view.loader.cullingVisibleChunks, self.view.loader.cullingLODType

    local lastNeedOptimizePerformance = self.m_needOptimizePerformance
    self.m_needOptimizePerformance = true

    if lodType ~= ChunkLODType.Low then
        self:_RefreshCullingChunks(chunks, lodType)
    end

    self:_RefreshLoadedChunksTiers()
    self:_RefreshLoaderChunksTierColorState(self:_GetIsInTier())

    self.m_needOptimizePerformance = lastNeedOptimizePerformance
end








LevelMapLoader._GetMarkCache = HL.Method(HL.Table).Return(Stack) << function(self, markViewData)
    local cache
    if self.m_needInteractableMark then
        local isCustom = markViewData.sortOrder == CUSTOM_MARK_ORDER
        cache = isCustom and self.m_customMarkCache or self.m_markCache
    else
        cache = self.m_markCache
    end
    return cache
end

LevelMapLoader._GetIsCustomMarkByViewData = HL.Method(HL.Table).Return(HL.Boolean) << function(self, markViewData)
    if markViewData == nil then
        return false
    end
    return markViewData.sortOrder == CUSTOM_MARK_ORDER
end

LevelMapLoader._GetMarkSourceObj = HL.Method(HL.Table).Return(HL.Any) << function(self, markViewData)
    local sourceObj
    local markSource = self.view.source.marks
    if self.m_needInteractableMark then
        local isCustom = self:_GetIsCustomMarkByViewData(markViewData)
        sourceObj = isCustom and markSource.levelMapCustomMark or markSource.levelMapInteractableMark
    else
        sourceObj = markSource.levelMapMark
    end
    return sourceObj
end

LevelMapLoader._GetMarkFromCache = HL.Method(HL.Table).Return(HL.Any) << function(self, markViewData)
    local orderRoot = self:_GetMarkRootByOrder(markViewData.sortOrder)
    local markCache, markSourceObj = self:_GetMarkCache(markViewData), self:_GetMarkSourceObj(markViewData)
    local markObj
    if markCache:Count() > 0 then
        markObj = markCache:Pop()
    else
        markObj = Utils.wrapLuaNode(CSUtils.CreateObject(markSourceObj.gameObject, orderRoot))
    end
    if markObj.transform.parent ~= orderRoot then
        markObj.transform:SetParent(orderRoot)
    end
    markObj.gameObject:SetActive(true)

    if self.m_needInteractableMark then
        markObj.needTryLoadResourceStateNode = not self:_GetIsCustomMarkByViewData(markViewData)
    end

    return markObj
end

LevelMapLoader._CacheMark = HL.Method(HL.Table, HL.Any) << function(self, markViewData, markObj)
    local markCache = self:_GetMarkCache(markViewData)
    markObj:ClearLevelMapMark()
    markObj.gameObject:SetActive(false)
    markCache:Push(markObj)
end

LevelMapLoader._GetTrackingMarkFromCache = HL.Method(HL.Boolean).Return(HL.Any) << function(self, isMissionTracking)
    local trackingRoot = self.view.element.trackingMarkRoot
    local root = isMissionTracking and trackingRoot.mission or trackingRoot.general
    local sourceObj = self.view.source.marks.levelMapTrackingMark
    local trackingMarkObj
    if self.m_trackingMarkCache:Count() > 0 then
        trackingMarkObj = self.m_trackingMarkCache:Pop()
    else
        trackingMarkObj = Utils.wrapLuaNode(CSUtils.CreateObject(sourceObj.gameObject, root))
    end
    if trackingMarkObj.transform.parent ~= root then
        trackingMarkObj.transform:SetParent(root)
    end
    trackingMarkObj.gameObject:SetActive(true)

    if not isMissionTracking and self.m_needInteractableMark then
        trackingMarkObj.needTryLoadResourceStateNode = true
    end

    return trackingMarkObj
end

LevelMapLoader._CacheTrackingMark = HL.Method(HL.Any) << function(self, trackingMarkObj)
    trackingMarkObj:ClearLevelMapMark()
    trackingMarkObj.gameObject:SetActive(false)
    self.m_trackingMarkCache:Push(trackingMarkObj)
end

LevelMapLoader._RefreshGridMarks = HL.Method(HL.Userdata, HL.Boolean) << function(self, loaderData, needShow)
    local marks = loaderData.marks
    if marks == nil or marks.Count == 0 then
        return
    end
    for markInstId, markRuntimeData in pairs(marks) do
        self:_RefreshGridMark(markInstId, markRuntimeData, needShow)
    end
end

LevelMapLoader._RefreshGridMark = HL.Method(HL.String, HL.Userdata, HL.Boolean) << function(self, markInstId, markRuntimeData, needShow)
    local invisibleInMist = not markRuntimeData.visibleInMist and markRuntimeData:IsInMist()
    if invisibleInMist then
        if needShow then
            self.m_waitVisibleInMistMarks[markInstId] = true
        else
            self.m_waitVisibleInMistMarks[markInstId] = nil
        end
        return
    end

    if not markRuntimeData.isConstantState and self.m_needListenMarkStateChange then
        if needShow then
            markRuntimeData:AddStateChangeCallback("LoadedMark")
        else
            markRuntimeData:RemoveStateChangeCallback("LoadedMark")
        end
    end

    if needShow and markRuntimeData.isVisible then
        local templateId = markRuntimeData.templateId
        local templateData = self.m_markStaticDataMap[templateId]
        local order = templateData.sortOrder

        local markViewData, markObj
        local getFromCache
        if self.m_loadedMarkViewDataMap[markInstId] then
            markViewData = self.m_loadedMarkViewDataMap[markInstId]
            markObj = markViewData.markObj
            getFromCache = false
        else
            markViewData = {
                instId = markInstId,
                runtimeData = markRuntimeData,
                sortOrder = order,
                visibleLayer = templateData.visibleLayer,
                filterType = templateData.filterType,
                filterTypeEnum = templateData.filterTypeEnum,
                isPowerRelated = false,
                isTravelRelated = false,
            }
            markObj = self:_GetMarkFromCache(markViewData)
            self.m_loadedMarkViewDataMap[markInstId] = markViewData
            getFromCache = true
        end

        markViewData.markObj = markObj
        markViewData.mark = markObj.view

        markObj:InitLevelMapMark(markRuntimeData.rectPosition, markRuntimeData, self.m_needOptimizePerformance, not getFromCache)


        if markRuntimeData.isPowerRelated ~= nil then
            markViewData.isPowerRelated = markRuntimeData.isPowerRelated
        end
        if markRuntimeData.isTravelRelated ~= nil then
            markViewData.isTravelRelated = markRuntimeData.isTravelRelated
        end


        self:_RefreshGridMarkTierState(markObj)


        if self.m_onMarkHover ~= nil then
            markObj:SetMarkOnHoverCallback(function(isHover)
                self.m_onMarkHover(markInstId, isHover)
            end)
        end


        if self.view.config.INITIAL_MARK_SCALE ~= 1 then
            markObj.view.rectTransform.localScale = self.m_initialMarkScale
        end


        if markRuntimeData.isTracking or markRuntimeData.isMissionTracking then
            local hiddenKey = markRuntimeData.isMissionTracking and "MissionTrackingRelated" or "TrackingRelated"
            markObj:ToggleMarkHiddenState(hiddenKey, true)
        end
    else
        local markViewData = self.m_loadedMarkViewDataMap[markInstId]
        if markViewData ~= nil and markViewData.markObj ~= nil then
            local markObj = markViewData.markObj
            self:_CacheMark(markViewData, markObj)
            self.m_loadedMarkViewDataMap[markInstId] = nil
        end
    end
end

LevelMapLoader._RefreshNonConstantStateMark = HL.Method(HL.String) << function(self, markInstId)
    if self.m_loadedMarkViewDataMap[markInstId] then
        local markObj = self.m_loadedMarkViewDataMap[markInstId].markObj
        markObj:ResetMarkIcon()
    else
        self:_OnMarkInstDataChanged(markInstId, true)
    end
end

LevelMapLoader._RefreshGridMarkTierState = HL.Method(HL.Any) << function(self, mark)
    if mark == nil then
        return
    end
    mark:RefreshMarkTierState(self.m_tierIndex, not self.m_needShowMarkTier)
end

LevelMapLoader._GetGeneralTrackingMarkIfNeed = HL.Method() << function(self)
    if self.view.element.trackingMarkRoot.generalTrackingMark ~= nil then
        return
    end
    self.view.element.trackingMarkRoot.generalTrackingMark = self:_GetTrackingMarkFromCache(false)
end


LevelMapLoader._RefreshMissionTrackingMarksLevelState = HL.Method() << function(self)
    if self.m_loadedMissionTrackingMarks == nil then
        return
    end

    for instId, missionTrackingMarkObj in pairs(self.m_loadedMissionTrackingMarks) do
        local success, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(instId)
        if success then
            local levelMapMark = missionTrackingMarkObj
            local mark = missionTrackingMarkObj
            if markRuntimeData.trackingInfo.posValueState ~= PosValueState.DiffLevel then
                levelMapMark:ResetMarkIcon()
                mark:ResetMarkRectPosition()
                levelMapMark:ToggleMarkHiddenState("TrackingOtherLevel", false)
            else
                if self.m_needShowOtherLevelTracking then
                    local missionImportance = markRuntimeData.missionInfo.missionImportance
                    local iconName
                    if missionImportance == GEnums.MissionImportance.High then
                        iconName = MapConst.MISSION_HIGH_IMPORTANCE_TRACK_OTHER_LEVEL_ICON_NAME
                    elseif missionImportance == GEnums.MissionImportance.Mid then
                        iconName = MapConst.MISSION_MID_IMPORTANCE_TRACK_OTHER_LEVEL_ICON_NAME
                    else
                        iconName = MapConst.MISSION_LOW_IMPORTANCE_TRACK_OTHER_LEVEL_ICON_NAME
                    end
                    local trackPosition = markRuntimeData.trackingInfo.showPos
                    local trackRectPosition = self:_GetRectPosByWorldPos(trackPosition)
                    levelMapMark:OverrideMarkIcon(iconName, true)
                    mark:OverrideMarkRectPosition(trackRectPosition)
                else
                    levelMapMark:ToggleMarkHiddenState("TrackingOtherLevel", markRuntimeData.levelId ~= self.m_levelId)
                end
            end
        end
    end

    if self.m_needShowOtherLevelTracking then
        self:_RefreshMissionTrackingMarksOffset()
    end
end

LevelMapLoader._RefreshGeneralTrackingMarkLevelState = HL.Method() << function(self)
    self:_GetGeneralTrackingMarkIfNeed()

    if string.isEmpty(self.m_loadedGeneralTrackingMarkId) then
        return
    end

    if self.m_mapManager.trackingMarkPointInfo == nil then
        return
    end

    local levelMapMark = self.view.element.trackingMarkRoot.generalTrackingMark
    local mark = self.view.element.trackingMarkRoot.generalTrackingMark
    if self.m_mapManager.trackingMarkPointInfo.posValueState ~= PosValueState.DiffLevel then
        levelMapMark:ResetMarkIcon()
        mark:ResetMarkRectPosition()
        levelMapMark:ToggleMarkHiddenState("TrackingOtherLevel", false)
    else
        if self.m_needShowOtherLevelTracking then
            levelMapMark:OverrideMarkIcon(MapConst.GENERAL_TRACK_OTHER_LEVEL_ICON_NAME, true)
            local trackPosition = self.m_mapManager.trackingMarkPointInfo.showPos
            local trackRectPosition = self:_GetRectPosByWorldPos(trackPosition)
            mark:OverrideMarkRectPosition(trackRectPosition)
        else
            levelMapMark:ToggleMarkHiddenState("TrackingOtherLevel", mark.markRuntimeData.levelId ~= self.m_levelId)
        end
    end
end

LevelMapLoader._RefreshMissionTrackingMarksOffset = HL.Method() << function(self)
    local offsetPosition = Vector2(
        self.view.config.MISSION_TRACKING_MARK_OFFSET_X,
        self.view.config.MISSION_TRACKING_MARK_OFFSET_Y
    )
    for instId, missionTrackingMarkObj in pairs(self.m_loadedMissionTrackingMarks) do
        local success, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(instId)
        if success then
            local needOffset = markRuntimeData.trackingInfo.isMapOverlap or markRuntimeData.trackingInfo.isOverlap
            missionTrackingMarkObj.view.content.anchoredPosition = needOffset and offsetPosition or Vector2.zero
        end
    end
end

LevelMapLoader._RefreshMissionTrackingMarksPosition = HL.Method() << function(self)
    if not self.m_mapManager.needRefreshMissionTrackingMarkPos then
        return
    end
    if self.m_loadedMissionTrackingMarks == nil or not next(self.m_loadedMissionTrackingMarks) then
        return
    end
    for _, missionTrackingMarkObj in pairs(self.m_loadedMissionTrackingMarks) do
        if missionTrackingMarkObj.markRuntimeData.isPositionChanged and
            missionTrackingMarkObj.markRuntimeData.trackingInfo.posValueState == PosValueState.SameLevel then
            missionTrackingMarkObj:OverrideMarkRectPosition(missionTrackingMarkObj.markRuntimeData.rectPosition)
        end
    end
end

LevelMapLoader._RefreshWaitVisibleInMistMarksState = HL.Method() << function(self)
    if self.m_waitVisibleInMistMarks == nil or next(self.m_waitVisibleInMistMarks) == nil then
        return
    end
    local visibleMarks = {}
    for markInstId, _ in pairs(self.m_waitVisibleInMistMarks) do
        local success, markRuntimeData = self.m_mapManager:GetMarkInstRuntimeData(markInstId)
        if success then
            self:_RefreshGridMark(markInstId, markRuntimeData, true)
            if self.m_loadedMarkViewDataMap[markInstId] ~= nil then
                table.insert(visibleMarks, markInstId)
            end
        end
    end
    for _, markInstId in ipairs(visibleMarks) do
        self.m_waitVisibleInMistMarks[markInstId] = nil
    end
end

LevelMapLoader._RefreshCustomMarkOrderActiveState = HL.Method(HL.Boolean) << function(self, state)
    for order = 1, CUSTOM_MARK_ORDER - 1 do
        self:SetMarkOrderState(order, state)
    end
end






LevelMapLoader._GetMarkRootByOrder = HL.Method(HL.Number).Return(RectTransform) << function(self, order)
    return self.view.element.markRoot[string.format(MARK_ORDER_VIEW_NAME_FORMAT, order)]
end

LevelMapLoader._GetIsInTier = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_tierId ~= MapConst.BASE_TIER_ID and self.m_tierIndex ~= MapConst.BASE_TIER_INDEX
end

LevelMapLoader._GetGridSpriteIdWithTier = HL.Method(HL.String).Return(HL.String, HL.Boolean) << function(self, gridId)
    local gridData = self.m_loadedGridViewDataMap[gridId]
    if gridData == nil then
        return gridId, false
    end

    if not self:_GetIsInTier() then
        return gridId, false
    end

    if not gridData.loaderData.needLoadTiers then
        return gridId, false
    end

    local success, gridTierId = gridData.loaderData.tiers:TryGetValue(self.m_tierId)
    if not success then
        return gridId, false
    else
        return gridTierId, true
    end
end

LevelMapLoader._GetRectPosByWorldPos = HL.Method(HL.Any, HL.Opt(HL.Boolean)).Return(Vector2) << function(self, worldPos, ignoreInverse)
    local rectPos = UILevelMapUtils.ConvertUILevelMapWorldPosToRectPos(worldPos, self.m_gridWorldLength, self.m_gridRectLength)
    if not ignoreInverse then
        local needInverse = self.m_mapManager:IsLevelNeedInverse(self.m_levelId)
        if needInverse then
            rectPos = Vector2(rectPos.y, -rectPos.x)
        end
    end
    return rectPos
end






LevelMapLoader._ClearLoaderCache = HL.Method(HL.Table, LuaNodeCache) << function(self, nodeTable, cache)
    if nodeTable == nil then
        return
    end

    for _, node in pairs(nodeTable) do
        cache:Cache(node)
    end

    nodeTable = {}
end

LevelMapLoader._ClearLoaderMarkCache = HL.Method() << function(self)
    for _, markViewData in pairs(self.m_loadedMarkViewDataMap) do
        self:_CacheMark(markViewData, markViewData.markObj)
    end

    self.m_loadedMarkViewDataMap = {}
end

LevelMapLoader._ClearLoaderStaticElementCache = HL.Method() << function(self)
    for _, staticElementViewData in pairs(self.m_loadedStaticElementViewDataMap) do
        local initializer = staticElementViewData.initializer
        if initializer.componentGetter ~= nil then
            local component = initializer.componentGetter(staticElementViewData.elementObj)
            if component ~= nil then
                component:ClearComponent()
            end
        end
        local cache = staticElementViewData.cache or initializer.cache
        cache:Cache(staticElementViewData.elementObj)
    end

    self.m_loadedStaticElementViewDataMap = {}
end

LevelMapLoader._ClearLoaderGameplayAreaCache = HL.Method() << function(self)
    if self.m_loadedGameplayAreas == nil then
        return
    end

    for _, node in pairs(self.m_loadedGameplayAreas) do
        node.levelMapGameplayArea:ClearComponent()
        self.m_gameplayAreaCache:Cache(node)
    end

    self.m_loadedGameplayAreas = {}
end

LevelMapLoader._ClearLoaderLinesCache = HL.Method() << function(self)
    for _, lineViewData in pairs(self.m_loadedLineViewDataMap) do
        local lineCache = self.m_lineCaches[lineViewData.lineData.lineType]
        local lineObj = lineViewData.lineObj
        if lineObj.levelMapLine ~= nil then
            lineObj.levelMapLine:ClearComponent()
        end
        lineCache:Cache(lineObj)
    end

    self.m_loadedPowerLineCount = 0
    self.m_loadedLineViewDataMap = {}
end

LevelMapLoader._ClearLoaderCachesState = HL.Method() << function(self)
    self:_ClearLoaderStaticElementCache()
    self:_ClearLoaderMarkCache()
    self:_ClearLoaderGameplayAreaCache()
    self:_ClearLoaderLinesCache()

    if self.m_useChunkGODrawer then
        self:_RecycleAllGOChunkCells()
    end


end






LevelMapLoader._AddMapDelayAction = HL.Method(HL.String, HL.Function).Return(HL.Any) << function(self, key, callback)
    if self.m_delayActionList[key] ~= nil then
        local action = self.m_delayActionList[key]
        action.timer = self:_ClearTimer(action.timer)
        action.timer = self:_StartTimer(COMMON_DELAY_TIME, function()
            self:_InvokeMapDelayAction(key)
        end)
        return key
    end

    self.m_delayActionList[key] = {
        timer = self:_StartTimer(COMMON_DELAY_TIME, function()
            self:_InvokeMapDelayAction(key)
        end),
        callback = callback
    }
    return key
end

LevelMapLoader._RemoveMapDelayAction = HL.Method(HL.String) << function(self, key)
    local action = self.m_delayActionList[key]
    if action == nil then
        return
    end
    action.timer = self:_ClearTimer(action.timer)
    self.m_delayActionList[key] = nil
end

LevelMapLoader._RemoveAllDelayActions = HL.Method() << function(self)
    for _, action in pairs(self.m_delayActionList) do
        action.timer = self:_ClearTimer(action.timer)
    end
    self.m_delayActionList = {}
end

LevelMapLoader._InvokeMapDelayAction = HL.Method(HL.String) << function(self, key)
    local action = self.m_delayActionList[key]
    if action == nil then
        return
    end
    action.callback()
    self:_RemoveMapDelayAction(key)
end

LevelMapLoader._LoadPermanentStaticElement = HL.Method(HL.Userdata) << function(self, permanentElementData)
    if permanentElementData == nil then
        return
    end

    local staticElementData = permanentElementData.elementData
    if staticElementData == nil or not self:_IsExpectedStaticElementType(staticElementData.type) then
        return
    end

    local initializer = self.m_staticElementInitializer[staticElementData.type]
    if initializer == nil then
        return
    end

    if initializer.isVisible ~= nil and not initializer.isVisible(staticElementData) then
        return
    end

    local staticElementId = staticElementData.id
    local cache = initializer.getCache and initializer.getCache(staticElementData) or initializer.cache
    local staticElement = cache:Get()
    staticElement.gameObject:SetActive(true)
    staticElement.rectTransform.anchoredPosition = permanentElementData.rectPos
    local viewData = {
        elementObj = staticElement,
        initializer = initializer,
        cache = cache,
        displayTierId = staticElementData.displayTierId,
    }
    self.m_loadedStaticElementViewDataMap[staticElementId] = viewData
    initializer.initializer(staticElement, staticElementData)
    self:_RefreshLoadedStaticElementStateWithTier(viewData)
end

LevelMapLoader._UnloadPermanentStaticElement = HL.Method(HL.String) << function(self, staticElementId)
    local elementViewData = self.m_loadedStaticElementViewDataMap[staticElementId]
    if elementViewData == nil then
        return
    end

    if elementViewData.initializer.componentGetter ~= nil then
        local component = elementViewData.initializer.componentGetter(elementViewData.elementObj)
        if component ~= nil and component.ClearComponent ~= nil then
            component:ClearComponent()
        end
    end
    GameObject.Destroy(elementViewData.elementObj.gameObject)
    self.m_loadedStaticElementViewDataMap[staticElementId] = nil
end

LevelMapLoader._AddDelayLoadPermanentStaticElement = HL.Method(HL.Userdata) << function(self, staticElementData)
    if staticElementData == nil then
        return
    end

    local staticElementId = staticElementData.id
    local loadDistance = staticElementData.loadDistance
    self.m_tryLoadElementsList[staticElementId] = {
        staticElementData = staticElementData,
        loadDistance = loadDistance,
        targetPosX = staticElementData.position.x,
        targetPosY = staticElementData.position.z,
        isLoaded = false,
    }
end






LevelMapLoader.SetLoaderLevel = HL.Method(HL.String) << function(self, levelId)
    self.m_levelId = levelId

    self:_ReorderAllGOChunkDrawers()
end

LevelMapLoader.SetLoaderWithMarkPosition = HL.Method(HL.String) << function(self, markInstId)
    local success, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    if not success then
        return
    end
    self.view.viewRect.anchoredPosition = self:_GetRectPosByWorldPos(markRuntimeData.position)
end

LevelMapLoader.SetLoaderWithLevelCenterPosition = HL.Method(HL.String, Vector2, HL.Opt(HL.Table)).Return(HL.Userdata) << function(
    self, levelId, offset, tweenInfo)
    local success, levelLoaderData = self.m_mapManager:GetLoaderLevelDataByLevelId(levelId)
    if not success then
        return nil
    end

    local center = (levelLoaderData.rectLeftBottom + levelLoaderData.rectRightTop) / 2.0 + offset
    if tweenInfo then
        self.m_posTween = self.view.viewRect:DOAnchorPos(center, tweenInfo.duration):SetEase(tweenInfo.ease):OnUpdate(function()
            self.view.loader:DoLoaderHitCheck(true)
            self:_RefreshGridsMask()
        end)
        return self.m_posTween
    else
        self.view.viewRect.anchoredPosition = center
        return nil
    end
end

LevelMapLoader.SetLoaderViewSizeByGridsCount = HL.Method(HL.Number, HL.Number, HL.Opt(HL.Table)).Return(HL.Userdata) << function(
    self, horizontalCount, verticalCount, tweenInfo)
    local size = Vector2(
        horizontalCount * self.m_gridRectLength,
        verticalCount * self.m_gridRectLength
    )
    if tweenInfo ~= nil then
        self.m_sizeTween = self.view.viewRect:DOSizeDelta(size, tweenInfo.duration):SetEase(tweenInfo.ease)
        return self.m_sizeTween
    else
        self.view.viewRect.sizeDelta = size
        return nil
    end
end

LevelMapLoader.SetLoaderDataUpdateInterval = HL.Method(HL.Number) << function(self, interval)
    self.m_dataUpdateInterval = interval
end

LevelMapLoader.SetLoaderElementsShownState = HL.Method(HL.Boolean) << function(self, isShown)
    local element = self.view.element
    element.staticElementBackRoot.gameObject:SetActive(isShown)
    element.staticElementFrontRoot.gameObject:SetActive(isShown)
    element.lineRoot.gameObject:SetActive(isShown)
    element.frontLineRoot.gameObject:SetActive(isShown)
    element.markRoot.gameObject:SetActive(isShown)
    element.trackingMarkRoot.gameObject:SetActive(isShown)
    element.staticElementBottomRoot.switchMask.gameObject:SetActive(isShown)
    element.player.gameObject:SetActive(isShown)
end

LevelMapLoader.SetMarkOrderState = HL.Method(HL.Number, HL.Boolean) << function(self, orderNum, state)
    self:_GetMarkRootByOrder(orderNum).gameObject:SetActive(state)
end

LevelMapLoader.SetGeneralTrackingMarkState = HL.Method(HL.String) << function(self, markInstId)
    self:_GetGeneralTrackingMarkIfNeed()
    local generalTrackingMarkObj = self.view.element.trackingMarkRoot.generalTrackingMark
    self.m_loadedGeneralTrackingMarkId = markInstId

    local internalToggleTracking = function(isTracking)
        if not isTracking then
            generalTrackingMarkObj:ClearLevelMapMark()
        end
        generalTrackingMarkObj.gameObject:SetActive(isTracking)
        generalTrackingMarkObj:RefreshTrackingMarkState(isTracking)
        if not isTracking then
            self.m_loadedGeneralTrackingMarkId = ""
        end
    end

    if string.isEmpty(markInstId) then
        internalToggleTracking(false)
        return
    end

    local success, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    if not success then
        internalToggleTracking(false)
        return
    end

    if markRuntimeData.mapId ~= self.m_mapId then
        internalToggleTracking(false)
        return
    end

    if markRuntimeData.levelId ~= self.m_levelId and not self.m_needShowOtherLevelTracking then
        internalToggleTracking(false)
        return
    end

    generalTrackingMarkObj:InitLevelMapMark(self:_GetRectPosByWorldPos(markRuntimeData.position), markRuntimeData)
    internalToggleTracking(true)

    self:_RefreshGeneralTrackingMarkLevelState()
    self:_RefreshGeneralTrackingMarkWithTier()
end

LevelMapLoader.SetMissionTrackingMarkState = HL.Method(HL.Table) << function(self, markInstIdList)
    for _, missionTrackingMarkObj in pairs(self.m_loadedMissionTrackingMarks) do
        missionTrackingMarkObj.gameObject:SetActive(false)
        self:_CacheTrackingMark(missionTrackingMarkObj)
    end
    self.m_loadedMissionTrackingMarks = {}

    for _, missionTrackingArea in pairs(self.m_loadedMissionTrackingAreas) do
        missionTrackingArea.levelMapMissionArea:ClearComponent()
        missionTrackingArea.gameObject:SetActive(false)
        self.m_missionTrackingAreaCache:Cache(missionTrackingArea)
    end
    self.m_loadedMissionTrackingAreas = {}

    if markInstIdList == nil then
        return
    end

    for _, markInstId in pairs(markInstIdList) do
        local success, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
        if success and markRuntimeData.mapId == self.m_mapId and (markRuntimeData.levelId == self.m_levelId or self.m_needShowOtherLevelTracking) then

            local missionTrackingMarkObj = self:_GetTrackingMarkFromCache(true)
            local rectPos = self:_GetRectPosByWorldPos(markRuntimeData.position)
            missionTrackingMarkObj:InitLevelMapMark(rectPos, markRuntimeData)
            missionTrackingMarkObj:RefreshTrackingMarkState(true)
            self.m_loadedMissionTrackingMarks[markInstId] = missionTrackingMarkObj


            if markRuntimeData.guideArea > 0 then
                local missionTrackingArea = self.m_missionTrackingAreaCache:Get()
                missionTrackingArea.rectTransform.anchoredPosition = rectPos
                missionTrackingArea.gameObject:SetActive(true)
                missionTrackingArea.levelMapMissionArea:Init(markRuntimeData, missionTrackingMarkObj.gameObject)
                self.m_loadedMissionTrackingAreas[markInstId] = missionTrackingArea

                if missionTrackingArea.levelMapMissionArea.needUseCenterPosition then
                    local centerPos = self:_GetRectPosByWorldPos(markRuntimeData.position)
                    missionTrackingArea.rectTransform.anchoredPosition = centerPos
                end

                if not missionTrackingArea.levelMapMissionArea.coexistWithMissionTrack then
                    if missionTrackingArea.levelMapMissionArea.notNeedCheckCoexistBecauseOfRadius then
                        missionTrackingMarkObj:ToggleMarkHiddenState("EnterMissionArea", not markRuntimeData.trackingInfo.showPoint)
                    end
                end
            end
        end
    end

    self:_RefreshMissionTrackingMarksLevelState()
    self:_RefreshMissionTrackingMarksWithTier()


    for _, missionTrackingArea in pairs(self.m_loadedMissionTrackingAreas) do
        if not missionTrackingArea.levelMapMissionArea.coexistWithMissionTrack and
            not missionTrackingArea.levelMapMissionArea.notNeedCheckCoexistBecauseOfRadius then
            missionTrackingArea.levelMapMissionArea:ForceRefreshDisappearIfOutRect()
        end
    end
end

LevelMapLoader.SetLoaderLineVisibleStateByType = HL.Method(HL.Userdata, HL.Boolean) << function(self, lineType, isVisible)
    local root = self.m_lineRoots[lineType]
    if root == nil then
        return
    end
    root.gameObject:SetActive(isVisible)
end

LevelMapLoader.SetLoaderLineVisibleState = HL.Method(HL.Boolean) << function(self, isVisible)
    for _, root in pairs(self.m_lineRoots) do
        root.gameObject:SetActive(isVisible)
    end
end

LevelMapLoader.SetLoaderPlayerVisibleState = HL.Method(HL.Boolean) << function(self, isVisible)
    self.view.element.player.gameObject:SetActive(isVisible)
end

LevelMapLoader.SetLoaderTierId = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, tierId, needAnim)
    local lastTierIndex = self.m_tierIndex
    self.m_tierId = tierId
    self.m_tierIndex = self.m_regionManager:GetTierIndex(tierId)
    self:_RefreshLoadedMarksWithTier()
    self:_RefreshLoadedStaticElementsStateWithTier()

    if needAnim then
        local tierAnim = self.view.element.tierAnim
        if lastTierIndex ~= self.m_tierIndex and lastTierIndex ~= MapConst.BASE_TIER_INDEX and self.m_tierIndex ~= MapConst.BASE_TIER_INDEX then

            tierAnim:ClearTween(false)
            tierAnim:PlayOutAnimation(function()
                self:_RefreshLoadedChunksTiers()
                if self.m_tierIndex ~= MapConst.BASE_TIER_INDEX then
                    tierAnim:PlayInAnimation()
                end
            end)
        else
            self:_RefreshLoadedChunksTiers()
        end
        self:_RefreshLoaderChunksTierColorState(self:_GetIsInTier())
    else
        self:_RefreshLoadedChunksTiers()
        self:_RefreshLoaderChunksTierColorState(self:_GetIsInTier())
    end
end

LevelMapLoader.SetNeedOptimizePerformance = HL.Method(HL.Boolean) << function(self, active)
    self.m_needOptimizePerformance = active
end

LevelMapLoader.ToggleLoaderNeedShowMarkTier = HL.Method(HL.Boolean) << function(self, needShowTier)
    self.m_needShowMarkTier = needShowTier
    self:_RefreshLoadedMarksWithTier()
end

LevelMapLoader.ToggleLoaderGeneralTrackingVisibleState = HL.Method(HL.Boolean) << function(self, visible)
    self.view.element.trackingMarkRoot.general.gameObject:SetActive(visible)

    if not string.isEmpty(self.m_loadedGeneralTrackingMarkId) then
        local markViewData = self.m_loadedMarkViewDataMap[self.m_loadedGeneralTrackingMarkId]
        if markViewData ~= nil then
            markViewData.markObj:ToggleMarkHiddenState("TrackingRelated", visible)
        end
    end
end

LevelMapLoader.ToggleLoaderMissionTrackingVisibleState = HL.Method(HL.Boolean) << function(self, visible)
    self.view.element.trackingMarkRoot.mission.gameObject:SetActive(visible)
end

LevelMapLoader.ToggleLoaderSwitchMaskVisibleState = HL.Method(HL.Boolean) << function(self, visible)
    self.view.element.staticElementBottomRoot.switchMask.gameObject:SetActive(visible)
    self.view.element.staticElementBackRoot.switchButton.gameObject:SetActive(visible)
end

LevelMapLoader.ToggleLoaderLineRootVisibleState = HL.Method(HL.Boolean) << function(self, visible)
    self.view.element.lineRoot.gameObject:SetActive(visible)
    self.view.element.frontLineRoot.gameObject:SetActive(visible)
end

LevelMapLoader.ToggleLoaderGamePlayAreaVisibleState = HL.Method(HL.Boolean) << function(self, visible)
    self.view.element.gameplayArea.gameObject:SetActive(visible)
end


LevelMapLoader.ToggleLoaderMissionAreaVisibleState = HL.Method(HL.Boolean) << function(self, visible)
    self.view.element.missionArea.gameObject:SetActive(visible)
end

LevelMapLoader.GetLoaderViewRectWidthAndHeight = HL.Method(HL.Boolean).Return(HL.Number, HL.Number) << function(self, getTarget)
    if getTarget and self.m_sizeTween ~= nil and self.m_sizeTween:IsPlaying() then
        local size = self.m_sizeTween.endValue
        return size.x, size.y
    else
        local rect = self.view.viewRect.rect
        return rect.width, rect.height
    end
end

LevelMapLoader.GetWorldPositionByRectPosition = HL.Method(Vector2).Return(Vector3) << function(self, rectPos)
    return UILevelMapUtils.ConvertUILevelMapRectPosToWorldPos(rectPos, self.m_gridWorldLength, self.m_gridRectLength)
end

LevelMapLoader.GetMarkRectTransformByInstId = HL.Method(HL.String).Return(Unity.RectTransform) << function(self, instId)
    local markViewData = self.m_loadedMarkViewDataMap[instId]
    if markViewData == nil or markViewData.mark == nil then
        return nil
    end
    return markViewData.mark.rectTransform
end

LevelMapLoader.GetMarkOrderRoot = HL.Method(HL.Number).Return(RectTransform) << function(self, order)
    return self:_GetMarkRootByOrder(order)
end

LevelMapLoader.GetLoadedMarkViewDataMap = HL.Method().Return(HL.Table) << function(self)
    return self.m_loadedMarkViewDataMap
end

LevelMapLoader.GetLoadedMarkViewDataByInstId = HL.Method(HL.String).Return(HL.Table) << function(self, instId)
    if not self.m_loadedMarkViewDataMap or not next(self.m_loadedMarkViewDataMap) then
        return
    end
    return self.m_loadedMarkViewDataMap[instId]
end

LevelMapLoader.GetLoadedMarkByInstId = HL.Method(HL.String).Return(HL.Any) << function(self, instId)
    local markViewData = self.m_loadedMarkViewDataMap[instId]
    if markViewData == nil or markViewData.markObj == nil then
        return nil
    end
    return markViewData.markObj
end

LevelMapLoader.GetGeneralTrackingMark = HL.Method().Return(HL.Any) << function(self)

    return self.view.element.trackingMarkRoot.generalTrackingMark
end

LevelMapLoader.GetMissionTrackingMarks = HL.Method().Return(HL.Any) << function(self)
    local marks = {}
    if self.m_loadedMissionTrackingMarks == nil then
        return marks
    end

    for instId, loadedMark in pairs(self.m_loadedMissionTrackingMarks) do

        marks[instId] = loadedMark
    end
    return marks
end

LevelMapLoader.UpdateAndRefreshAll = HL.Method() << function(self)
    self.view.loader:DoLoaderHitCheck(true)
end

LevelMapLoader.RefreshMarkStateAfterMistUnlocked = HL.Method() << function(self)
    self:_RefreshWaitVisibleInMistMarksState()

    if self.m_onMarkInstDataChangedCallback ~= nil then

        self.m_onMarkInstDataChangedCallback()
    end
end

LevelMapLoader.RefreshCharacterPosition = HL.Method() << function(self)
    local playerNode = self.view.element.player
    playerNode.rectTransform.anchoredPosition = self.m_mapManager.characterRectPosition
    playerNode.playerArrow.localEulerAngles = Vector3(0.0, 0.0, -self.m_mapManager.characterForwardAngle)
    playerNode.playerView.localEulerAngles = Vector3(0.0, 0.0, -self.m_mapManager.characterViewForwardAngle)
end

LevelMapLoader.RefreshElementsHiddenStateInOtherLevel = HL.Method() << function(self)
    for loaderData in cs_pairs(self.view.loader.hitGrids) do
        local staticElements = loaderData.staticElements
        local marks = loaderData.marks
        local lines = loaderData.lines
        local gridInCurrLevel = loaderData.levelId == self.m_levelId

        if staticElements ~= nil and staticElements.Count > 0 then
            for staticElementId, _ in pairs(staticElements) do
                local elementViewData = self.m_loadedStaticElementViewDataMap[staticElementId]
                if elementViewData ~= nil then
                    self:_RefreshStaticElementVisibleState(elementViewData, "LoaderOtherLevelHide", gridInCurrLevel)
                end
            end
        end

        if marks ~= nil and marks.Count > 0 then
            for markInstId, _ in pairs(marks) do
                local markViewData = self.m_loadedMarkViewDataMap[markInstId]
                if markViewData ~= nil then
                    local markObj = markViewData.markObj
                    local needHide = not gridInCurrLevel
                    local markRuntimeData = markViewData.runtimeData
                    if markRuntimeData.connectFromNodeIdList ~= nil then
                        if needHide then
                            local otherLevelConnectRefreshFunc = function(connectNodeIdList)
                                for connectNodeId in cs_pairs(connectNodeIdList) do
                                    local _, connectMarkInstId = self.m_mapManager:GetFacMarkInstIdByNodeId(
                                        markRuntimeData.chapterId,
                                        connectNodeId
                                    )
                                    local connectMarkViewData = self.m_loadedMarkViewDataMap[connectMarkInstId]
                                    if connectMarkViewData ~= nil then
                                        local inCurrentLevel = markRuntimeData.levelId == self.m_levelId or
                                            connectMarkViewData.runtimeData.levelId == self.m_levelId
                                        if inCurrentLevel then
                                            needHide = false
                                        end
                                    end
                                end
                            end

                            if markRuntimeData.connectFromNodeIdList.Count > 0 then
                                otherLevelConnectRefreshFunc(markRuntimeData.connectFromNodeIdList)
                            end
                            if markRuntimeData.connectToNodeIdList.Count > 0 then
                                otherLevelConnectRefreshFunc(markRuntimeData.connectToNodeIdList)
                            end
                        end
                    end

                    markObj:ToggleMarkHiddenState("LoaderOtherLevelHide", needHide)
                    if markRuntimeData.isTracking then


                        if not needHide and not gridInCurrLevel then
                            markObj:ToggleMarkHiddenState("TrackingRelated", false)
                        else
                            markObj:ToggleMarkHiddenState("TrackingRelated", true)
                        end
                    end
                end
            end
        end

        for lineId, _ in pairs(lines) do
            local lineViewData = self.m_loadedLineViewDataMap[lineId]
            if lineViewData ~= nil then
                local needHide = not gridInCurrLevel
                if needHide then
                    local lineData = lineViewData.lineData
                    if lineData.startBelongGrid.gridId ~= lineData.endBelongGrid.gridId then
                        needHide = lineData.startBelongGrid.levelId ~= self.m_levelId and lineData.endBelongGrid.levelId ~= self.m_levelId
                    end
                end
                lineViewData.lineObj.gameObject:SetActive(not needHide)
            end
        end
    end
end

LevelMapLoader.RefreshLevelSwitchMaskState = HL.Method(HL.String) << function(self, levelId)
    local success, cfg = self.m_mapManager:GetLoaderLevelDataByLevelId(levelId)
    if not success then
        return
    end

    if self.m_switchMaskCells == nil then
        self.m_switchMaskCells = UIUtils.genCellCache(self.view.element.staticElementBottomRoot.switchMask.maskCell)
    end

    local maskDataList = {}
    for _, staticElementData in pairs(cfg.loadConfig.staticElements) do
        if staticElementData.type == ElementType.SwitchMask then
            table.insert(maskDataList, {
                position = staticElementData.position,
                targetLevelId = staticElementData.targetLevelId,
                targetLevelSpriteName = staticElementData.targetLevelSpriteName,
                isUnlocked = GameInstance.player.mapManager:IsLevelUnlocked(staticElementData.targetLevelId)
            })
        end
    end
    if #maskDataList == nil then
        return
    end

    self.m_switchMaskCells:Refresh(#maskDataList, function(cell, index)
        local maskData = maskDataList[index]

        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            if maskData.isUnlocked then
                Notify(MessageConst.ON_LEVEL_MAP_SWITCH_BTN_CLICKED, maskData.targetLevelId);
            else
                Notify(MessageConst.SHOW_TOAST, Language.LUA_OPEN_MAP_LEVEL_LOCKED)
            end
        end)

        cell.image:LoadSprite(MapConst.UI_MAP_SWITCH_MASK_PATH, maskData.targetLevelSpriteName)
        cell.image:SetNativeSize()
        cell.image.alphaHitTestMinimumThreshold = 0.01
        cell.image.color = maskData.isUnlocked and Color.white or self.view.config.LOCKED_SWITCH_MASK_COLOR

        cell.rectTransform.anchoredPosition = self:_GetRectPosByWorldPos(maskData.position)
    end)
end

LevelMapLoader.ToggleForbidMistRefreshAfterGridChange = HL.Method(HL.Boolean) << function(self, forbid)
    self.m_forbidMistRefreshAfterGridChange = forbid
    if forbid then
        self:_ClearDrawerLoadState(self.m_mistDrawers[MistLoadType.Normal])
    else
        if self.m_waitMistRefreshAfterGridChange then
            self:_RefreshLoadedChunksMists(MistLoadType.Normal)
        end
    end
    self.m_waitMistRefreshAfterGridChange = false
end

LevelMapLoader.RefreshMistState = HL.Method(HL.Opt(HL.Function)) << function(self, onRefreshFinish)
    self:_RefreshLoadedChunksMists(MistLoadType.Normal, nil, function()
        if onRefreshFinish ~= nil then
            onRefreshFinish()
        end
    end)
end

LevelMapLoader.RefreshAnimationMistState = HL.Method(HL.Table, HL.Opt(HL.Function)) << function(self, mistList, onRefreshFinish)
    self:_RefreshLoadedChunksMists(MistLoadType.Animation, {
        overrideMistIdList = mistList
    }, onRefreshFinish)
end

LevelMapLoader.ToggleAnimationMistNodeVisibleState = HL.Method(HL.Boolean) << function(self, isVisible)
    self.view.element.animMist.gameObject:SetActive(isVisible)
end

LevelMapLoader.PlayMistsUnlockedAnimation = HL.Method(HL.Opt(HL.Function)) << function(self, onComplete)
    local animMist = self.view.element.animMist

    local recoverAnimState = function(onRecoverFinish)
        animMist.animationWrapper:PlayWithTween("levelmaploader_mist_unlock_in", function()
            if onRecoverFinish ~= nil then
                onRecoverFinish()
            end
        end)
        animMist.materialAnimation:ForceUpdate()
    end

    recoverAnimState(function()
        self.m_animMistShowTimer = self:_ClearTimer(self.m_animMistShowTimer)
        self.m_animMistShowTimer = self:_StartTimer(self.view.config.ANIM_MIST_SHOW_DURATION, function()
            animMist.animationWrapper:PlayWithTween("levelmaploader_mist_unlock_out", function()
                self:_ClearRTDrawState(self.m_mistDrawers[MistLoadType.Animation])
                recoverAnimState()
                if onComplete ~= nil then
                    onComplete()
                end
            end)
            self.m_animMistShowTimer = self:_ClearTimer(self.m_animMistShowTimer)
        end)
    end)
end

LevelMapLoader.ForceDisposeAllTextureResources = HL.Method() << function(self)
    local needDisposeKeys = {}
    for loadKey, loadData in pairs(self.m_chunkResourceLoadDataPool) do
        if loadData.isTexture then
            table.insert(needDisposeKeys, loadKey)
        end
    end
    for _, loadKey in ipairs(needDisposeKeys) do
        self:_DisposeChunkResource(loadKey, true)
    end
end



LevelMapLoader.DisposeChunkResourcesByLODTypes = HL.Method(HL.Table) << function(self, disposeChunkLODTypes)
    if self.m_useChunkGODrawer then

        for _, lodType in ipairs(disposeChunkLODTypes) do
            local drawer = self.m_chunkGODrawers[lodType]
            if drawer ~= nil then
                self:_ClearGOChunkDrawer(drawer, true)
            end
        end
    else

        for _, lodType in ipairs(disposeChunkLODTypes) do
            local drawerData = self.m_chunkRTDrawers[lodType]
            if drawerData ~= nil then
                for loadId, _ in pairs(drawerData.drawList) do
                    self:_DisposeChunkResource(loadId, true)
                end
            end
        end
    end
end

LevelMapLoader.ResetToTargetMapAndLevel = HL.Method(HL.String) << function(self, levelId)
    local success, levelConfig = Utils.getLevelConfig(levelId)
    if not success then
        return
    end


    if self.m_posTween ~= nil then
        self.m_posTween:Kill(false)
        self.m_posTween = nil
    end
    if self.m_sizeTween ~= nil then
        self.m_sizeTween:Kill(false)
        self.m_sizeTween = nil
    end

    self.m_mapId = levelConfig.mapIdStr
    self.m_levelId = levelId
    self:SetLoaderTierId(MapConst.BASE_TIER_ID)
    self:_RefreshLoaderChunksTierColorState(false)
    self:_RemoveAllDelayActions()
    self:_ClearLoaderCachesState()
    self:_DisposeAllChunkResources()
    self:_ClearAllDrawersRT()
    self:_InitTableFields(true)


    self.view.loader:ClearLoaderCheckState()
    self.view.loader:ChangeLoaderCheckLevels(self.m_levelId)


    self:_InitPermanentElementsInCurrentMap()
end






if BEYOND_DEBUG_COMMAND then
    LevelMapLoader._InitDebugMode = HL.Method(HL.String) << function(self, levelId)
        local success, levelConfig = Utils.getLevelConfig(levelId)
        if not success then
            return
        end

        local worldLeftBottom = levelConfig.rectLeftBottom
        local worldRightTop = levelConfig.rectRightTop
        local rectLeftBottom = self:_GetRectPosByWorldPos(Vector3(worldLeftBottom.x, 0, worldLeftBottom.y), true)
        local rectRightTop = self:_GetRectPosByWorldPos(Vector3(worldRightTop.x, 0, worldRightTop.y), true)
        local horizontalCount = (rectRightTop.x - rectLeftBottom.x) / self.m_gridRectLength
        local verticalCount = (rectRightTop.y - rectLeftBottom.y) / self.m_gridRectLength

        self.m_gridCache = LuaNodeCache(self.view.source.grid, self.view.element.loadedGrids)
        for i = 0, horizontalCount - 1 do
            for j = 0, verticalCount - 1 do
                local gridCell = self.m_gridCache:Get()
                local gridRectPosX = rectLeftBottom.x + (i + 0.5) * self.m_gridRectLength
                local gridRectPosY = rectLeftBottom.y + (j + 0.5) * self.m_gridRectLength
                gridCell.rectTransform.anchoredPosition = Vector2(gridRectPosX, gridRectPosY)
            end
        end
    end
end




HL.Commit(LevelMapLoader)
return LevelMapLoader