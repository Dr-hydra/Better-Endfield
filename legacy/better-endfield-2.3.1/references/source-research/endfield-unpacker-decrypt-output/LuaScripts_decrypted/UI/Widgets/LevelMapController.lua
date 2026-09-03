local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local ControllerMode = MapConst.LEVEL_MAP_CONTROLLER_MODE
local ChunkLODType = CS.Beyond.Gameplay.ChunkLoadConfigInfo.ChunkLODType

LevelMapController = HL.Class('LevelMapController', UIWidgetBase)

local EXTRA_VIEW_VALUE = 1
local LEVEL_UPDATE_THREAD_INTERVAL = 0.5
local LEVEL_SWITCH_LOADER_UPDATE_THREAD_INTERVAL = 0.015
local FOLLOW_CHARACTER_LOADER_UPDATE_THREAD_INTERVAL = 0.25
local INVOKE_LEVEL_SWITCH_FINISH_DELAY = 0.2

local NEED_UPDATE_LOADER_CHARACTER_SQR_MAGNITUDE = 0.002

LevelMapController.m_mode = HL.Field(HL.Number) << -1

LevelMapController.m_levelMapConfig = HL.Field(CS.Beyond.Gameplay.UILevelMapConfig)

LevelMapController.m_currentLevelId = HL.Field(HL.String) << ""

LevelMapController.m_currentIsSingleLevel = HL.Field(HL.Boolean) << false



LevelMapController.m_fixedMarkInstId = HL.Field(HL.String) << ""

LevelMapController.m_customRefreshMark = HL.Field(HL.Function)





LevelMapController.m_onLevelSwitch = HL.Field(HL.Function)

LevelMapController.m_onLevelSwitchStart = HL.Field(HL.Function)

LevelMapController.m_onLevelSwitchFinish = HL.Field(HL.Function)

LevelMapController.m_onTrackingMarkClicked = HL.Field(HL.Function)

LevelMapController.m_switchTweenList = HL.Field(HL.Table)

LevelMapController.m_delayInvokeTimer = HL.Field(HL.Number) << -1

LevelMapController.m_currentMaxScale = HL.Field(HL.Number) << -1

LevelMapController.m_currentMinScale = HL.Field(HL.Number) << -1

LevelMapController.m_visibleMarks = HL.Field(HL.Table)

LevelMapController.m_filteredMarks = HL.Field(HL.Table)

LevelMapController.m_layeredMarks = HL.Field(HL.Table)

LevelMapController.m_currentLayer = HL.Field(HL.Number) << -1

LevelMapController.m_currFollowModeTrackingMarkId = HL.Field(HL.String) << ""

LevelMapController.m_markVisibleRect = HL.Field(RectTransform)

LevelMapController.m_expectedMarks = HL.Field(HL.Table)

LevelMapController.m_topOrderMarks = HL.Field(HL.Table)

LevelMapController.m_noGeneralTracking = HL.Field(HL.Boolean) << true

LevelMapController.m_noMissionTracking = HL.Field(HL.Boolean) << true

LevelMapController.m_currTopOrderMarkLastOrder = HL.Field(HL.Number) << -1

LevelMapController.m_waitPlayUnlockedAnimMistList = HL.Field(HL.Table)





LevelMapController.m_waitPlayUnlockedAnimMistQueue = HL.Field(HL.Forward("Queue"))

LevelMapController.m_isPlayingUnlockedAnimation = HL.Field(HL.Boolean) << false

LevelMapController.m_lastPosChanged = HL.Field(HL.Boolean) << false





LevelMapController.m_currTrackingMarkData = HL.Field(HL.Table)

LevelMapController.m_lastTrackingMarkId = HL.Field(HL.String) << ""

LevelMapController.m_trackingMissionMarkIdList = HL.Field(HL.Table)




LevelMapController._OnFirstTimeInit = HL.Override() << function(self)
    if self.m_mode == ControllerMode.LEVEL_SWITCH then
        self:RegisterMessage(MessageConst.ON_MAP_FILTER_STATE_CHANGED, function(args)
            self:_RefreshLoaderMarksVisibleStateByFilter()
        end)
    elseif self.m_mode == ControllerMode.FOLLOW_CHARACTER then
        self:RegisterMessage(MessageConst.ON_MIST_MAP_UNLOCK, function(args)
            self:_OnMistStateChangeInFollowMode(args)
        end)
        self:RegisterMessage(MessageConst.ON_TELEPORT_FINISH, function(args)
            self:_OnTeleportFinish()
        end)

        self:RegisterMessage(MessageConst.ON_ENTER_TIER_REGION, function(args)
            local tierId = unpack(args)
            self:_OnCurrTierRegionStateChangeInFollowMode(tierId, true)
        end)
        self:RegisterMessage(MessageConst.ON_LEAVE_TIER_REGION, function(args)
            local tierId = unpack(args)
            self:_OnCurrTierRegionStateChangeInFollowMode(tierId, false)
        end)
        self:RegisterMessage(MessageConst.ON_ENTER_TIER_CONTAINER_REGION, function(args)
            self:SetLoaderTierStateWithNeedShowMarkTier(true)
        end)
        self:RegisterMessage(MessageConst.ON_LEAVE_TIER_CONTAINER_REGION, function(args)
            self:SetLoaderTierStateWithNeedShowMarkTier(false)
        end)

        self:RegisterMessage(MessageConst.SHOW_MISSION_TRACKER, function(args)
            self:_OnShowMissionMarks()
        end)
    end

    self:RegisterMessage(MessageConst.ON_TRACKING_MAP_MARK, function(args)
        self:_OnTrackingStateChanged()
    end)
    self:RegisterMessage(MessageConst.ON_TRACK_MARK_CHANGE, function(args)
        self:_OnTrackingStateChanged()
    end)
    self:RegisterMessage(MessageConst.ON_MAP_TRACKING_MISSION_DATA_CHANGED, function(args)
        self:_RefreshMissionTrackingMarks()
    end)
    self:RegisterMessage(MessageConst.ON_MAP_MARK_RUNTIME_DATA_MODIFY, function(args)
        local instId = unpack(args)
        self:_RefreshGeneralTrackingMarkOnMarkDataModify(instId)
    end)
end

LevelMapController._OnDestroy = HL.Override() << function(self)
    if self.m_mode == ControllerMode.FOLLOW_CHARACTER then
        GameInstance.player.mapManager.characterFollowerInstance = nil
    elseif self.m_mode == ControllerMode.LEVEL_SWITCH then
        if self.m_switchTweenList ~= nil then
            for _, tween in pairs(self.m_switchTweenList) do
                if tween ~= nil then
                    tween:Kill(false)
                end
            end
            self.m_switchTweenList = nil
        end
    end
end


























LevelMapController.InitLevelMapController = HL.Method(HL.Number, HL.Opt(HL.Table)) << function(self, mode, customInfo)
    self.m_levelMapConfig = DataManager.uiLevelMapConfig


    customInfo = customInfo or {}
    local initialLevelId = string.isEmpty(customInfo.initialLevelId) and
        GameWorld.worldInfo.curLevelId or
        customInfo.initialLevelId
    local loaderCustomInfo = {}
    if mode == ControllerMode.FIXED then
        if not string.isEmpty(customInfo.fixedMarkInstId) then
            initialLevelId = GameInstance.player.mapManager:GetMarkInstRuntimeDataLevelId(customInfo.fixedMarkInstId)
            self.m_fixedMarkInstId = customInfo.fixedMarkInstId
        end
        loaderCustomInfo = {
            needUpdate = false,
        }
    elseif mode == ControllerMode.LEVEL_SWITCH then
        loaderCustomInfo = {
            extraView = EXTRA_VIEW_VALUE,
            needOptimizePerformance = false,
            hideOtherLevels = true,
            needUpdate = false,
            needShowOtherLevelTracking = false,
            needInteractableMark = true,
            useChunkGODrawer = true,
            retainTextureAfterDraw = true,
            onMarkInstDataChangedCallback = function()
                self:_ResetLoaderMarksVisibleStateByFilter()
                self:_RefreshLoaderMarksVisibleStateByLayer(self.m_currentLayer)
            end,
            onMarkHover = function(markInstId, isHover)
                customInfo.onMarkHover(markInstId, isHover)
            end,
            onGridsLoadedStateChange = function()
                self:_RefreshAnimationMistsInSwitchMode()
            end
        }
    elseif mode == ControllerMode.FOLLOW_CHARACTER then
        loaderCustomInfo = {
            needOptimizePerformance = true,
            needExtraLowScaleRT = true,
            needShowOtherLevelTracking = true,
            needListenMarkStateChange = true,
        }
    end
    loaderCustomInfo.expectedStaticElements = customInfo.expectedStaticElements
    loaderCustomInfo.gridsMaskFollowTarget = customInfo.gridsMaskFollowTarget
    if not GameInstance.player.mapManager:IsLevelLoaderDataExists(initialLevelId) then
        if BEYOND_DEBUG_COMMAND and mode == ControllerMode.DEBUG then
            self.view.levelMapLoader:InitLevelMapLoader(initialLevelId, { isDebugMode = true })
        end
        return
    end

    self.view.levelMapLoader:InitLevelMapLoader(initialLevelId, loaderCustomInfo)
    self:_SetCurrentLevelId(initialLevelId)

    self.m_customRefreshMark = customInfo.customRefreshMark or function()end

    self.m_onLevelSwitch = customInfo.onLevelSwitch or function()end
    self.m_onLevelSwitchStart = customInfo.onLevelSwitchStart or function()end
    self.m_onLevelSwitchFinish = customInfo.onLevelSwitchFinish or function()end
    self.m_onTrackingMarkClicked = customInfo.onTrackingMarkClicked or function()end
    self.m_markVisibleRect = customInfo.visibleRect
    self.m_expectedMarks = customInfo.expectedMarks or {}
    self.m_topOrderMarks = customInfo.topOrderMarks or {}
    self.m_noGeneralTracking = customInfo.noGeneralTracking == true
    self.m_noMissionTracking = customInfo.noMissionTracking == true
    self.m_visibleMarks = {}

    self.m_mode = mode

    if mode == ControllerMode.FIXED then
        self:_InitControllerFixedMode()
    elseif mode == ControllerMode.LEVEL_SWITCH then
        self:_InitControllerSwitchMode()
    elseif mode == ControllerMode.FOLLOW_CHARACTER then
        self:_InitControllerFollowMode()
    end

    self:_FirstTimeInit()
end

LevelMapController._SetCurrentLevelId = HL.Method(HL.String) << function(self, levelId)
    self.m_currentLevelId = levelId
    local configSuccess, levelConfig = GameInstance.player.mapManager:GetLoaderLevelBasicInfoByLevelId(levelId)
    if configSuccess then
        self.m_currentIsSingleLevel = levelConfig.isSingleLevel
    end
end




LevelMapController._InitControllerFixedMode = HL.Method() << function(self)
    if not string.isEmpty(self.m_fixedMarkInstId) then
        self.view.levelMapLoader:SetLoaderWithMarkPosition(self.m_fixedMarkInstId)
    end
    self.view.levelMapLoader:UpdateAndRefreshAll()
    self.view.levelMapLoader:RefreshCharacterPosition()
    self.view.levelMapLoader:SetLoaderPlayerVisibleState(false)
    self:_CustomRefreshFixedModeMarks()
end

LevelMapController._CustomRefreshFixedModeMarks = HL.Method() << function(self)
    local loadedMarkViewDataMap = self.view.levelMapLoader:GetLoadedMarkViewDataMap()
    if loadedMarkViewDataMap == nil or next(loadedMarkViewDataMap) == nil then
        return
    end

    for _, markViewData in pairs(loadedMarkViewDataMap) do
        if self.m_customRefreshMark ~= nil then
            self.m_customRefreshMark(markViewData)
        end
    end
end






LevelMapController._InitControllerSwitchMode = HL.Method() << function(self)
    self.view.markTopOrder:SetAsLastSibling()
    self.view.levelMapLoader:SetLoaderDataUpdateInterval(LEVEL_SWITCH_LOADER_UPDATE_THREAD_INTERVAL)
    self:_RefreshLoaderStateByLevel(self.m_currentLevelId, false)
    self:_RefreshMissionTrackingMarks()

    self.view.levelMapLoader:ToggleLoaderGeneralTrackingVisibleState(not self.m_noGeneralTracking)
    self.view.levelMapLoader:ToggleLoaderMissionTrackingVisibleState(not self.m_noMissionTracking)
    self.view.levelMapLoader:RefreshCharacterPosition()
    self.view.levelMapLoader:SetNeedOptimizePerformance(false)
end

LevelMapController._RefreshLoaderStateByLevel = HL.Method(HL.String, HL.Boolean) << function(self, levelId, moveNeedTween)
    local basicInfoSuccess, configInfo = GameInstance.player.mapManager:GetLoaderLevelBasicInfoByLevelId(levelId)
    if not basicInfoSuccess then
        return
    end

    local levelConfigSuccess, levelConfig = Utils.getLevelConfig(levelId)
    if not levelConfigSuccess then
        return
    end

    local gridRectLength = self.m_levelMapConfig.gridRectLength
    local tweenDuration = self.view.config.SWITCH_TWEEN_DURATION
    local tweenEase = self.view.config.SWITCH_TWEEN_CURVE
    local tweenData = moveNeedTween and {
        duration = tweenDuration,
        ease = tweenEase,
    } or nil
    self.m_switchTweenList = {}

    local levelMapLoader = self.view.levelMapLoader
    levelMapLoader:SetLoaderLevel(levelId)
    levelMapLoader:RefreshLevelSwitchMaskState(levelId)
    local inCurrentMap = GameWorld.worldInfo.curMapIdStr == levelConfig.mapIdStr

    if moveNeedTween then
        levelMapLoader:DisposeChunkResourcesByLODTypes({ChunkLODType.Medium, ChunkLODType.High})
        levelMapLoader:SetLoaderElementsShownState(false)
    end


    local minScale = configInfo.minScale
    local offset = Vector2(
        configInfo.horizontalInitOffsetGridsValue * gridRectLength * minScale,
        configInfo.verticalInitOffsetGridsValue * gridRectLength * minScale
    )
    local loaderPosTween = levelMapLoader:SetLoaderWithLevelCenterPosition(levelId, offset, tweenData)
    local loaderSizeTween = levelMapLoader:SetLoaderViewSizeByGridsCount(
        configInfo.horizontalViewGridsCount,
        configInfo.verticalViewGridsCount,
        tweenData
    )
    local loaderMoveSize = Vector2(
        configInfo.horizontalMoveGridsValue * gridRectLength,
        configInfo.verticalMoveGridsValue * gridRectLength
    )
    local initialOffset = Vector2(
        configInfo.horizontalAllInitOffsetGridsValue * gridRectLength,
        configInfo.verticalAllInitOffsetGridsValue * gridRectLength
    )
    local targetInfo = {
        size = loaderMoveSize,
        scale = minScale,
        initialOffset = initialOffset,
    }

    self:_SetCurrentLevelId(levelId)
    self.m_currentMaxScale = DataManager.uiLevelMapConfig.globalLevelMaxScale
    self.m_currentMinScale = configInfo.minScale

    local onMoveFinish = function()
        self:_RefreshGeneralTrackingMarkInSwitchMode()
        self:_RefreshMissionTrackingMarks()
        levelMapLoader:SetLoaderPlayerVisibleState(inCurrentMap)
    end


    if moveNeedTween then
        table.insert(self.m_switchTweenList, loaderPosTween)
        table.insert(self.m_switchTweenList, loaderSizeTween)
        self.m_onLevelSwitchStart(targetInfo)

        local scaleTween = self.view.rectTransform:DOScale(configInfo.minScale, tweenDuration):SetEase(tweenEase)
        local posTween = self.view.rectTransform:DOAnchorPos(initialOffset, tweenDuration):SetEase(tweenEase)
        table.insert(self.m_switchTweenList, scaleTween)
        table.insert(self.m_switchTweenList, posTween)



        for index, tween in ipairs(self.m_switchTweenList) do
            tween:OnComplete(function()
                self.m_switchTweenList[index] = nil
                if not next(self.m_switchTweenList) then

                    self:_ResetLoaderMarksVisibleStateByFilter()

                    self.m_delayInvokeTimer = self:_StartTimer(INVOKE_LEVEL_SWITCH_FINISH_DELAY, function()
                        self.m_onLevelSwitchFinish()
                        self:_ClearTimer(self.m_delayInvokeTimer)
                        levelMapLoader:RefreshElementsHiddenStateInOtherLevel()
                        levelMapLoader:SetLoaderElementsShownState(true)
                        onMoveFinish()
                    end)
                end
            end)
        end
    else
        levelMapLoader:UpdateAndRefreshAll()
        levelMapLoader:RefreshElementsHiddenStateInOtherLevel()
        self:_ResetLoaderMarksVisibleStateByFilter()
        self.m_onLevelSwitch(targetInfo)

        self.view.rectTransform.anchoredPosition = Vector2.zero
        onMoveFinish()
    end
end

LevelMapController._ResetLoaderMarksVisibleStateByFilter = HL.Method() << function(self)
    self:_UpdateLoaderMarksVisibleData()
    self:_RefreshLoaderMarksVisibleStateByFilter()
end

LevelMapController._UpdateLoaderMarksVisibleData = HL.Method() << function(self)
    local loadedMarkViewDataMap = self.view.levelMapLoader:GetLoadedMarkViewDataMap()
    if loadedMarkViewDataMap == nil or next(loadedMarkViewDataMap) == nil then
        return
    end

    self.m_visibleMarks = {}
    for _, markViewData in pairs(loadedMarkViewDataMap) do
        self.m_visibleMarks[markViewData.instId] = markViewData
    end

    self:_SetMarksToTopOrder()
end

LevelMapController._RefreshLoaderMarksVisibleStateByFilter = HL.Method() << function(self)
    self.m_filteredMarks = {}
    for instId, markViewData in pairs(self.m_visibleMarks) do
        local filterType = markViewData.filterType
        self.m_filteredMarks[instId] = not GameInstance.player.mapManager:HasFilterFlag(filterType)
        self:_RefreshLoaderMarkVisibleState(instId)
    end

    self:_RefreshLoaderLineVisibleState()
end

LevelMapController._RefreshLoaderMarksVisibleStateByLayer = HL.Method(HL.Number) << function(self, layer)
    self.m_layeredMarks = {}
    for instId, markViewData in pairs(self.m_visibleMarks) do
        local visible = markViewData.visibleLayer <= layer
        self.m_layeredMarks[instId] = visible
        self:_RefreshLoaderMarkVisibleState(instId)
    end
    self.m_currentLayer = layer

    self:_RefreshLoaderLineVisibleState()
end

LevelMapController._RefreshLoaderMarkVisibleState = HL.Method(HL.String) << function(self, markInstId)
    local markViewData = self.m_visibleMarks[markInstId]
    if markViewData == nil then
        return
    end
    local isVisible = true
    if not self.m_currentIsSingleLevel then
        isVisible = self:_GetIsMarkRealVisible(markInstId)
    end

    markViewData.markObj:ToggleMarkHiddenState("PanelControl", not isVisible)
end

LevelMapController._RefreshLoaderLineVisibleState = HL.Method() << function(self)


    self.view.levelMapLoader:SetLoaderLineVisibleState(true)
    local invisibleLineList = {}

    if next(self.m_expectedMarks) then
        for combinedTemplateId, lineType in pairs(MapConst.COMBINED_TEMPLATE_ID_TO_LINE_TYPE) do
            local isVisible = self.m_expectedMarks[combinedTemplateId]
            if not isVisible then
                invisibleLineList[lineType] = true
            end
        end
    end

    for lineType, fieldName in pairs(MapConst.LINE_TYPE_TO_VISIBLE_LAYER_FIELD_NAME) do
        local isVisible = self.m_currentLayer >= self.m_levelMapConfig[fieldName]
        if not isVisible then
            invisibleLineList[lineType] = true
        end
    end

    for filterType, lineType in pairs(MapConst.FILTER_TYPE_TO_LINE_TYPE) do
        if GameInstance.player.mapManager:HasFilterFlag(filterType) then
            invisibleLineList[lineType] = true
        end
    end

    for lineType, _ in pairs(invisibleLineList) do
        self.view.levelMapLoader:SetLoaderLineVisibleStateByType(lineType, false)
    end
end

LevelMapController._SetTrackingMissionMarksOnClickedCallbackInSwitchMode = HL.Method() << function(self)
    local trackingMissionMarks = self.view.levelMapLoader:GetMissionTrackingMarks()
    if trackingMissionMarks == nil then
        return
    end

    for instId, trackingMark in pairs(trackingMissionMarks) do
        trackingMark:OverrideMarkOnClickCallback(function()
            local relatedMark = self.view.levelMapLoader:GetLoadedMarkByInstId(instId)
            self.m_onTrackingMarkClicked(instId, trackingMark, relatedMark)
        end)
    end
end

LevelMapController._GetIsMarkRealVisible = HL.Method(HL.String).Return(HL.Boolean) << function(self, markInstId)
    local markViewData = self.m_visibleMarks[markInstId]
    if markViewData == nil then
        return false
    end
    if next(self.m_expectedMarks) and not self.m_expectedMarks[markViewData.runtimeData.templateId] then
        if markViewData.runtimeData.isPowerRelated then
            if not self.m_expectedMarks[MapConst.POWER_RELATED_COMBINED_TEMPLATE_ID] then
                return false
            end
        elseif markViewData.runtimeData.isTravelRelated then
            if not self.m_expectedMarks[MapConst.TRAVEL_RELATED_COMBINED_TEMPLATE_ID] then
                return false
            end
        else
            return false
        end
    end
    if self.m_filteredMarks ~= nil and not self.m_filteredMarks[markInstId] then
        return false
    end
    if self.m_layeredMarks ~= nil and not self.m_layeredMarks[markInstId] then
        return false
    end
    return true
end

LevelMapController._SetMarksToTopOrder = HL.Method() << function(self)
    if not next(self.m_topOrderMarks) then
        return
    end

    local loadedMarkViewDataMap = self.view.levelMapLoader:GetLoadedMarkViewDataMap()
    if loadedMarkViewDataMap == nil or next(loadedMarkViewDataMap) == nil then
        return
    end

    for _, markViewData in pairs(loadedMarkViewDataMap) do
        if self.m_topOrderMarks[markViewData.runtimeData.templateId] then
            markViewData.mark.rectTransform:SetParent(self.view.markTopOrder)
        end
    end
end

LevelMapController._RefreshAnimationMistsInSwitchMode = HL.Method() << function(self)
    local showList = {}
    self.m_waitPlayUnlockedAnimMistList = {}

    local mistMapSystem = GameInstance.player.mistMapSystem
    for loaderData in cs_pairs(self.view.levelMapLoader.view.loader.hitChunks) do
        if loaderData.needLoadMists then
            for mistId, _ in pairs(loaderData.mists) do
                if not showList[mistId] and mistMapSystem:IsMistWaitingPlayUnlockedAnimation(mistId) then
                    if showList[mistId] == nil then
                        showList[mistId] = true
                        table.insert(self.m_waitPlayUnlockedAnimMistList, mistId)
                    end
                end
            end
        end
    end

    self.view.levelMapLoader:RefreshAnimationMistState(showList)

    if next(showList) then
        self.view.levelMapLoader:ToggleAnimationMistNodeVisibleState(true)
    end
end

LevelMapController.SetSingleMarkToTopOrder = HL.Method(HL.String) << function(self, markInstId)
    local markViewData = self.view.levelMapLoader:GetLoadedMarkViewDataByInstId(markInstId)
    if markViewData == nil then
        return
    end
    local markRectTransform = markViewData.mark.rectTransform
    self.m_currTopOrderMarkLastOrder = markRectTransform:GetSiblingIndex()
    markRectTransform:SetParent(self.view.markTopOrder)
end

LevelMapController.ResetSingleMarkToOriginalOrder = HL.Method(HL.String) << function(self, markInstId)
    local markViewData = self.view.levelMapLoader:GetLoadedMarkViewDataByInstId(markInstId)
    if markViewData == nil then
        return
    end
    local markRectTransform = markViewData.mark.rectTransform
    markRectTransform:SetParent(self.view.levelMapLoader:GetMarkOrderRoot(markViewData.sortOrder))
    markRectTransform:SetSiblingIndex(self.m_currTopOrderMarkLastOrder)
end






LevelMapController._InitControllerFollowMode = HL.Method() << function(self)
    self.view.levelMapLoader:SetLoaderDataUpdateInterval(FOLLOW_CHARACTER_LOADER_UPDATE_THREAD_INTERVAL)
    self:_RefreshMissionTrackingMarks()

    self:_InitTierStateInFollowMode()
    self.view.levelMapLoader:SetNeedOptimizePerformance(false)
    GameInstance.player.mapManager.characterFollowerInstance = self.view.characterFollower
    self.view.levelMapLoader:SetNeedOptimizePerformance(true)

    self:_RefreshGeneralTrackingMarkInFollowMode()
end

LevelMapController._OnTeleportFinish = HL.Method() << function(self)
    self.view.levelMapLoader:SetNeedOptimizePerformance(false)
    GameInstance.player.mapManager:ForceUpdateAndRefreshCharacterFollowerState(false)
    self.view.levelMapLoader:SetNeedOptimizePerformance(true)
end

LevelMapController._InitTierStateInFollowMode = HL.Method() << function(self)
    local needShowTier = GameWorld.mapRegionManager:GetCurrentCharInTierContainerId() > 0
    self:SetLoaderTierStateWithNeedShowMarkTier(needShowTier)
    self:SetLoaderTierStateWithTierId(GameWorld.mapRegionManager:GetCurrentCharInTierId())
end

LevelMapController._OnMistStateChangeInFollowMode = HL.Method(HL.Table) << function(self, args)
    if self.m_waitPlayUnlockedAnimMistQueue == nil then
        self.m_waitPlayUnlockedAnimMistQueue = require_ex("Common/Utils/DataStructure/Queue")()
    end
    self.m_waitPlayUnlockedAnimMistQueue:Push(unpack(args))
    self:_PlayMistUnlockedAnimationWithQueueInFollowMode()
end

LevelMapController._PlayMistUnlockedAnimationWithQueueInFollowMode = HL.Method() << function(self)
    if self.m_waitPlayUnlockedAnimMistQueue:Empty() or self.m_isPlayingUnlockedAnimation then
        return
    end

    self.m_isPlayingUnlockedAnimation = true
    local unlockMistId = self.m_waitPlayUnlockedAnimMistQueue:Pop()
    local levelMapLoader = self.view.levelMapLoader
    levelMapLoader:ToggleAnimationMistNodeVisibleState(false)
    levelMapLoader:ToggleForbidMistRefreshAfterGridChange(true)
    levelMapLoader:RefreshAnimationMistState({ [unlockMistId] = true }, function()
        levelMapLoader:RefreshMistState(function()
            levelMapLoader:ToggleAnimationMistNodeVisibleState(true)
            levelMapLoader:PlayMistsUnlockedAnimation(function()
                levelMapLoader:ToggleForbidMistRefreshAfterGridChange(false)
                levelMapLoader:ToggleAnimationMistNodeVisibleState(false)

                self.m_isPlayingUnlockedAnimation = false
                self:_PlayMistUnlockedAnimationWithQueueInFollowMode()
            end)
            levelMapLoader:RefreshMarkStateAfterMistUnlocked()
            AudioAdapter.PostEvent("Au_UI_Toast_SmallMapMistDissipate")
        end)
    end)
end

LevelMapController._OnCurrTierRegionStateChangeInFollowMode = HL.Method(HL.Number, HL.Boolean) << function(self, tierId, isIn)
    local tierIndex = GameWorld.mapRegionManager:GetTierIndex(tierId)
    if tierIndex == MapConst.BASE_TIER_INDEX then
        return
    end
    self:SetLoaderTierStateWithTierId(isIn and tierId or MapConst.BASE_TIER_ID)
end






LevelMapController._OnTrackingStateChanged = HL.Method() << function(self)
    if self.m_mode == ControllerMode.LEVEL_SWITCH then
        self:_RefreshGeneralTrackingMarkInSwitchMode()
    elseif self.m_mode == ControllerMode.FOLLOW_CHARACTER then
        self:_RefreshGeneralTrackingMarkInFollowMode()
    end
end

LevelMapController._RefreshGeneralTrackingMarkOnMarkDataModify = HL.Method(HL.String) << function(self, markInstId)
    if markInstId ~= GameInstance.player.mapManager.trackingMarkInstId then
        return
    end
    self:_OnTrackingStateChanged()
end

LevelMapController._RefreshGeneralTrackingMarkInSwitchMode = HL.Method() << function(self)
    local mapManager = GameInstance.player.mapManager
    local trackingMarkInstId

    if not string.isEmpty(mapManager.trackingMarkInstId) and
        mapManager:GetMarkInstRuntimeDataLevelId(mapManager.trackingMarkInstId) ~= self.m_currentLevelId then
        trackingMarkInstId = ""
    else
        trackingMarkInstId = mapManager.trackingMarkInstId
    end

    self:_RefreshGeneralRelatedMarkVisibleState(self.m_lastTrackingMarkId, trackingMarkInstId)
    self.view.levelMapLoader:SetGeneralTrackingMarkState(trackingMarkInstId)

    local trackingMark = self.view.levelMapLoader:GetGeneralTrackingMark()
    if not string.isEmpty(trackingMarkInstId) then
        trackingMark:OverrideMarkOnClickCallback(function()
            local relatedMark = self.view.levelMapLoader:GetLoadedMarkByInstId(trackingMarkInstId)
            self.m_onTrackingMarkClicked(trackingMarkInstId, trackingMark, relatedMark)
        end)
    end

    self.m_lastTrackingMarkId = trackingMarkInstId
end

LevelMapController._RefreshGeneralRelatedMarkVisibleState = HL.Method(HL.String, HL.String) << function(self, lastId, currId)
    local lastMark = self.view.levelMapLoader:GetLoadedMarkByInstId(lastId)
    local currMark = self.view.levelMapLoader:GetLoadedMarkByInstId(currId)
    if lastMark ~= nil then
        lastMark:ToggleMarkHiddenState("TrackingRelated", lastId == currId)
    end
    if currMark ~= nil then
        currMark:ToggleMarkHiddenState("TrackingRelated", true)
    end
end

LevelMapController._RefreshGeneralTrackingMarkInFollowMode = HL.Method() << function(self)
    local trackingMarkInstId = GameInstance.player.mapManager.trackingMarkInstId
    self:_RefreshGeneralRelatedMarkVisibleState(self.m_lastTrackingMarkId, trackingMarkInstId)
    self.view.levelMapLoader:SetGeneralTrackingMarkState(trackingMarkInstId)
    self.m_lastTrackingMarkId = trackingMarkInstId
    self.m_currFollowModeTrackingMarkId = trackingMarkInstId
end

LevelMapController._RefreshMissionTrackingMarks = HL.Method() << function(self)
    self.m_trackingMissionMarkIdList = {}
    for _,markId in cs_pairs(GameInstance.player.mapManager.trackingMissionMarkList) do
        table.insert(self.m_trackingMissionMarkIdList, markId)
    end
    self.view.levelMapLoader:SetMissionTrackingMarkState(self.m_trackingMissionMarkIdList)
    self:_RefreshMissionRelatedMarksVisibleState()

    if self.m_mode == ControllerMode.LEVEL_SWITCH then
        self:_SetTrackingMissionMarksOnClickedCallbackInSwitchMode()
    end
end

LevelMapController._RefreshMissionRelatedMarksVisibleState = HL.Method() << function(self)
    if self.m_trackingMissionMarkIdList == nil then
        return
    end
    for _, id in pairs(self.m_trackingMissionMarkIdList) do
        local mark = self.view.levelMapLoader:GetLoadedMarkByInstId(id)
        if mark ~= nil then
            mark:ToggleMarkHiddenState("MissionTrackingRelated", true)
        end
    end
end

LevelMapController._OnShowMissionMarks = HL.Method() << function(self)
    local trackingMissionMarks = self.view.levelMapLoader:GetMissionTrackingMarks()
    for _, missionTrackingMarkObj in pairs(trackingMissionMarks) do
        missionTrackingMarkObj.view.trackingImgAnim:ClearTween(false)
        missionTrackingMarkObj.view.trackingImgAnim:PlayInAnimation()
    end
end






LevelMapController.GetControllerCurrentMaxScale = HL.Method().Return(HL.Number) << function(self)
    return self.m_currentMaxScale
end

LevelMapController.GetControllerCurrentMinScale = HL.Method().Return(HL.Number) << function(self)
    return self.m_currentMinScale
end

LevelMapController.GetControllerCurrentLevelId = HL.Method().Return(HL.String) << function(self)
    return self.m_currentLevelId
end

LevelMapController.GetControllerMarkRectTransform = HL.Method(HL.String, HL.Opt(HL.Boolean)).Return(Unity.RectTransform) << function(self, markInstId, ignoreTracking)
    local mark = self:GetControllerMarkByInstId(markInstId, ignoreTracking)
    if mark == nil then
        return nil
    end
    return mark.rectTransform
end

LevelMapController.GetControllerNearbyMarkList = HL.Method(HL.String, HL.Number, HL.Number, HL.Opt(GEnums.MarkType)).Return(HL.Table) << function(self, targetInstId, length, scale, markTypeFilter)
    length = length / scale / 2.0
    local targetViewData = self.m_visibleMarks[targetInstId]
    if targetViewData == nil then
        return {}
    end
    local targetMark = targetViewData.mark
    local targetPos = targetMark.rectTransform.anchoredPosition

    local tempResult = {}
    for instId, markViewData in pairs(self.m_visibleMarks) do
        if instId ~= targetInstId and self:_GetIsMarkRealVisible(instId) then
            local isValidMark = self.view.levelMapLoader:GetLoadedMarkByInstId(instId) ~= nil
            if isValidMark then
                local mark = markViewData.mark
                local pos = mark.rectTransform.anchoredPosition
                if math.abs(pos.x - targetPos.x) <= length and math.abs(pos.y - targetPos.y) <= length then
                    local validNearby = true

                    local markSuccess, runtimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markViewData.instId)
                    if markSuccess then
                        local templateSuccess, templateData = Tables.mapMarkTempTable:TryGetValue(runtimeData.templateId)
                        if templateSuccess then
                            if markTypeFilter and templateData.markType ~= markTypeFilter then
                                validNearby = false
                            end

                            if markViewData.instId == GameInstance.player.mapManager:GetNowSelectCustomMarkInsId() then
                                validNearby = false
                            end

                            if instId == GameInstance.player.mapManager.trackingMarkInstId then
                                local trackingMark = self.view.levelMapLoader:GetGeneralTrackingMark()
                                if trackingMark.view.levelMapLimitInRect ~= nil and trackingMark.view.levelMapLimitInRect.isLimitedInRect then
                                    validNearby = false
                                end
                            end

                            if self.m_trackingMissionMarkIdList ~= nil then
                                for _, missionInstId in ipairs(self.m_trackingMissionMarkIdList) do
                                    if instId == missionInstId then
                                        local trackingMarks = self.view.levelMapLoader:GetMissionTrackingMarks()
                                        local trackingMark = trackingMarks[instId]
                                        if trackingMark ~= nil and trackingMark.view.levelMapLimitInRect ~= nil and trackingMark.view.levelMapLimitInRect.isLimitedInRect then
                                            validNearby = false
                                        end
                                    end
                                end
                            end

                            if not mark.gameObject.activeInHierarchy and not runtimeData.isTracking and not runtimeData.isMissionTracking then
                                validNearby = false
                            end

                            if validNearby then
                                table.insert(tempResult, markViewData)
                            end
                        end
                    end
                end
            end
        end
    end
    table.sort(tempResult, Utils.genSortFunction({ "sortOrder", "instId" }, false))

    local result = {}
    if targetInstId ~= GameInstance.player.mapManager:GetNowSelectCustomMarkInsId() then
        table.insert(result, targetInstId)
    end
    for _, markViewData in ipairs(tempResult) do
        table.insert(result, markViewData.instId)
    end

    return result
end

LevelMapController.GetControllerMarkByInstId = HL.Method(HL.String, HL.Opt(HL.Boolean)).Return(HL.Any) << function(self, instId, ignoreTracking)
    local originalMark = self.view.levelMapLoader:GetLoadedMarkByInstId(instId)
    if ignoreTracking then
        return originalMark
    end

    local mapManager = GameInstance.player.mapManager
    local trackingMarkInstId = mapManager.trackingMarkInstId
    if instId == trackingMarkInstId then
        local mark = self.view.levelMapLoader:GetGeneralTrackingMark()
        if mark ~= nil and mark.gameObject.activeInHierarchy then
            return mark
        end
    end

    local trackingMissionMarks = self.view.levelMapLoader:GetMissionTrackingMarks()
    for trackingMissionMarkInstId, trackingMissionMark in pairs(trackingMissionMarks) do
        if instId == trackingMissionMarkInstId and trackingMissionMark ~= nil and trackingMissionMark.gameObject.activeInHierarchy then
            return trackingMissionMark
        end
    end

    return originalMark
end

LevelMapController.GetControllerTrackingMark = HL.Method(HL.String).Return(HL.Any, HL.Any) << function(self, markInstId)
    if GameInstance.player.mapManager.trackingMarkInstId == markInstId then
        local trackingMark = self.view.levelMapLoader:GetGeneralTrackingMark()
        local relatedMark = self.view.levelMapLoader:GetLoadedMarkByInstId(markInstId)
        return trackingMark, relatedMark
    else
        local trackingMissionMarks = self.view.levelMapLoader:GetMissionTrackingMarks()
        if trackingMissionMarks ~= nil then
            for missionTrackingMarkInstId, trackingMark in pairs(trackingMissionMarks) do
                if missionTrackingMarkInstId == markInstId then
                    local relatedMark = self.view.levelMapLoader:GetLoadedMarkByInstId(missionTrackingMarkInstId)
                    return trackingMark, relatedMark
                end
            end
        end
    end
    return nil, nil
end

LevelMapController.NeedPlayMistsUnlockedAnimation = HL.Method().Return(HL.Boolean) << function(self)
    return next(self.m_waitPlayUnlockedAnimMistList) ~= nil
end

LevelMapController.TryPlayMistUnlockedAnimation = HL.Method(HL.Function) << function(self, onComplete)
    self.view.levelMapLoader:PlayMistsUnlockedAnimation(onComplete)
    GameInstance.player.mistMapSystem:AddMistUnlockedAnimationPlayed(self.m_waitPlayUnlockedAnimMistList)
    self.m_waitPlayUnlockedAnimMistList = {}
end

LevelMapController.RefreshLoaderMarksVisibleStateByLayer = HL.Method(HL.Number) << function(self, layer)
    self:_RefreshLoaderMarksVisibleStateByLayer(layer)
end

LevelMapController.ResetSwitchModeToTargetLevelState = HL.Method(HL.String) << function(self, levelId)
    local targetSuccess, targetLevelConfig = Utils.getLevelConfig(levelId)
    if not targetSuccess then
        return
    end

    local currSuccess, currLevelConfig = Utils.getLevelConfig(self.m_currentLevelId)
    if not currSuccess then
        return
    end

    if targetLevelConfig.mapIdStr ~= currLevelConfig.mapIdStr then

        self.view.levelMapLoader:ResetToTargetMapAndLevel(levelId)
    else

        self.view.levelMapLoader:DisposeChunkResourcesByLODTypes({ChunkLODType.Medium, ChunkLODType.High})
    end
    self:_RefreshLoaderStateByLevel(levelId, false)
end

LevelMapController.SetLoaderTierStateWithTierId = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, tierId, needAnim)
    self.view.levelMapLoader:SetLoaderTierId(tierId, needAnim)
end

LevelMapController.SetLoaderTierStateWithNeedShowMarkTier = HL.Method(HL.Boolean) << function(self, needShow)
    self.view.levelMapLoader:ToggleLoaderNeedShowMarkTier(needShow)
end

LevelMapController.SwitchToTargetLevel = HL.Method(HL.String) << function(self, levelId)
    self:_RefreshLoaderStateByLevel(levelId, true)
end

LevelMapController.RecoverTrackingMarkAnimation = HL.Method() << function(self)
    local trackingMark = self.view.levelMapLoader:GetGeneralTrackingMark()
    if trackingMark ~= nil and trackingMark.gameObject.activeInHierarchy then
        trackingMark:TryPlayTrackingMarkAnimation()
    end

    local trackingMissionMarks = self.view.levelMapLoader:GetMissionTrackingMarks()
    for _, trackingMissionMark in pairs(trackingMissionMarks) do
        if trackingMissionMark ~= nil and trackingMissionMark.gameObject.activeInHierarchy then
            trackingMissionMark:TryPlayTrackingMarkAnimation()
        end
    end
end




HL.Commit(LevelMapController)
return LevelMapController
