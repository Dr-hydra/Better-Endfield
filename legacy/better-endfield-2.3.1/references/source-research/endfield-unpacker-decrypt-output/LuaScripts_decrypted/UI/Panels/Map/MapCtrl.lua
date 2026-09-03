local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local MapSpaceshipNode = require_ex('UI/Widgets/MapSpaceshipNode')
local MapMarkDetailCommon = require_ex('UI/Widgets/MapMarkDetailCommon')
local MapPanelNodeType = MapConst.MapPanelNodeType
local PANEL_ID = PanelId.Map
local PHASE_ID = PhaseId.Map
local STANDARD_SCREEN_WIDTH = CS.Beyond.UI.UIConst.STANDARD_HORIZONTAL_RESOLUTION
local STANDARD_SCREEN_HEIGHT = CS.Beyond.UI.UIConst.STANDARD_VERTICAL_RESOLUTION
MapCtrl = HL.Class('MapCtrl', uiCtrl.UICtrl)

local COLLECTIONS_CONFIG = {
    ["chest"] = {
        viewName = "chest",
        mergeId = "item_cate_chest",
        hoverTextId = "ui_mappanel_collection_trchest",
    },
    ["coin"] = {
        viewName = "coin",
        mergeId = "int_collection_coin",
        hoverTextId = "ui_mappanel_collection_coin",
    },
    ["piece"] = {
        viewName = "piece",
        mergeId = "int_collection_piece",
        hoverTextId = "ui_mappanel_collection_puzzle",
    },
    ["blackbox"] = {
        viewName = "blackbox",
        mergeId = "int_blackbox_entry",
        hoverTextId = "ui_mappanel_collection_blackbox",
    },
    ["equipFormula"] = {
        viewName = "equipFormula",
        mergeId = "int_trchest_equip",
        hoverTextId = "ui_mappanel_collection_equip_formula",
    },
    ["trStar"] = {
        viewName = "trStar",
        mergeId = "int_collection_trstar",
        hoverTextId = "ui_mappanel_collection_trstar",
    },
}

local BUILDING_INFOS_CONFIG = {
    ["bandwidth"] = {
        viewName = "bandwidth",
        getter = function(sceneInfo)
            return sceneInfo.bandwidth.current, sceneInfo.bandwidth.max
        end,
        hoverTextId = "ui_mappanel_collection_bandwidth",
    },
    ["travelPole"] = {
        viewName = "travelPole",
        getter = function(sceneInfo)
            return sceneInfo.bandwidth.travelPoleCurrent, sceneInfo.bandwidth.travelPoleMax
        end,
        hoverTextId = "ui_mappanel_collection_pole",
    },
    ["battleBuilding"] = {
        viewName = "battleBuilding",
        getter = function(sceneInfo)
            return sceneInfo.bandwidth.battleCurrent, sceneInfo.bandwidth.battleMax
        end,
        hoverTextId = "ui_mappanel_collection_battle",
    }
}

local MAP_BLOCK_ORDER_OFFSET = 10

local INITIAL_SELECT_OPTION_INDEX = 1

local OPTION_MARK_HIGHLIGHT_DELAY_TIME = 0.05

local TIER_CHECK_TOLERANCE_RADIUS = 3

MapCtrl.s_initialSelectMarkInstId = HL.StaticField(HL.String) << ""

MapCtrl.m_initialLevelId = HL.Field(HL.String) << ""

MapCtrl.m_currLevelId = HL.Field(HL.String) << ""

MapCtrl.m_currMapId = HL.Field(HL.String) << ""

MapCtrl.m_ignoreOpenFocus = HL.Field(HL.Boolean) << false

MapCtrl.m_selectMarkRectTransform = HL.Field(Unity.RectTransform)

MapCtrl.m_isMarkDetailShowing = HL.Field(HL.Boolean) << false

MapCtrl.m_addZoomTick = HL.Field(HL.Number) << -1

MapCtrl.m_reduceZoomTick = HL.Field(HL.Number) << -1

MapCtrl.m_zoomVisibleLayer = HL.Field(HL.Number) << -1

MapCtrl.m_controllerRect = HL.Field(Unity.RectTransform)

MapCtrl.m_controllerSizeTick = HL.Field(HL.Number) << -1

MapCtrl.m_selectOptionCells = HL.Field(HL.Forward('UIListCache'))

MapCtrl.m_selectNodeShowTimer = HL.Field(HL.Number) << -1

MapCtrl.m_optionCloseCDTimer = HL.Field(HL.Number) << -1

MapCtrl.m_optionCloseCD = HL.Field(HL.Boolean) << false

MapCtrl.m_selectNodeTick = HL.Field(HL.Number) << -1

MapCtrl.m_doingSelectNodeHide = HL.Field(HL.Boolean) << false

MapCtrl.m_waitInitShowDetailMarkInstId = HL.Field(HL.String) << ""

MapCtrl.m_selectOptionMarkIdList = HL.Field(HL.Table)

MapCtrl.m_selectOptionMarkList = HL.Field(HL.Table)

MapCtrl.m_nextOptionMainAnimState = HL.Field(HL.Boolean) << true

MapCtrl.m_optionMainAnimState = HL.Field(HL.Boolean) << true

MapCtrl.m_optionAnimPlayThread = HL.Field(HL.Thread)

MapCtrl.m_optionMarkHighlightThread = HL.Field(HL.Thread)

MapCtrl.m_currHighlightOption = HL.Field(HL.Number) << -1

MapCtrl.m_buildingInfo = HL.Field(HL.Table)

MapCtrl.m_collectionInfo = HL.Field(HL.Table)

MapCtrl.m_multiDeletePanelShow = HL.Field(HL.Boolean) << false

MapCtrl.m_selectMarkInstId = HL.Field(HL.String) << ""

MapCtrl.m_tierCheckTick = HL.Field(HL.Number) << -1

MapCtrl.m_currTierContainerId = HL.Field(HL.Number) << -1

MapCtrl.m_currTierIdList = HL.Field(HL.Table)

MapCtrl.m_currTierIndex = HL.Field(HL.Number) << -1

MapCtrl.m_tierSwitcherCells = HL.Field(HL.Forward('UIListCache'))

MapCtrl.m_tierSwitchersShowing = HL.Field(HL.Boolean) << false

MapCtrl.m_waitRecoverTierId = HL.Field(HL.Number) << -1

MapCtrl.m_controllerHoverMark = HL.Field(HL.Boolean) << false

MapCtrl.m_controllerTierBindingGroup = HL.Field(HL.Number) << -1

MapCtrl.m_needPlayMistsAnimation = HL.Field(HL.Boolean) << false

MapCtrl.m_initZoomScalePercentage = HL.Field(HL.Number) << 1

MapCtrl.m_nodeOnInitFocused = HL.Field(HL.Boolean) << false

MapCtrl.m_trySwitchTierOnFocusMark = HL.Field(HL.Any)

MapCtrl.m_waitAutoSwitchTierId = HL.Field(HL.Number) << -1

MapCtrl.m_markClickLockThread = HL.Field(HL.Thread)

MapCtrl.m_isResettingToTargetLevel = HL.Field(HL.Boolean) << false

MapCtrl.m_isResettingToPlayer = HL.Field(HL.Boolean) << false

MapCtrl.m_remindTipInfo = HL.Field(HL.Any)

MapCtrl.m_isRemindTipShowing = HL.Field(HL.Boolean) << false

MapCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_LEVEL_MAP_MARK_CLICKED] = '_OnLevelMapMarkClicked',
    [MessageConst.ON_JUMP_TO_TP_RELATED_MARK] = '_OnJumpToTpMark',
    [MessageConst.ON_TRACKING_MAP_MARK] = '_OnTrackingMapMarkChanged',

    [MessageConst.ON_TELEPORT_FINISH] = '_OnTeleportFinish',
    [MessageConst.START_CAMERA_RENDER_IN_LOADING] = '_OnTeleportFinish',

    [MessageConst.ON_MAP_FILTER_STATE_CHANGED] = '_RefreshFilterBtnState',
    [MessageConst.ON_SYSTEM_UNLOCK_CHANGED] = '_OnSystemUnlock',
    [MessageConst.FORCE_SET_OPENED_MAP_LEVEL] = '_ForceSetMapStateToTargetLevel',
    [MessageConst.ON_MAP_MARK_RUNTIME_DATA_CHANGED] = '_OnDataChanged',
    [MessageConst.ON_LEVEL_MAP_SWITCH_BTN_CLICKED] = '_OnLevelSwitchBtnClicked',

    [MessageConst.ON_SCREEN_SIZE_CHANGED] = '_OnScreenSizeChanged',

    [MessageConst.FAC_ON_NODE_REMOVED] = '_OnFacNodeChanged',


    [MessageConst.SHOW_CUSTOM_MARK_MULTI_DELETE] = '_OnShowMarkMultiDelete',
    [MessageConst.HIDE_CUSTOM_MARK_MULTI_DELETE] = '_OnHideMarkMultiDelete',
    [MessageConst.TOGGLE_CUSTOM_MARK_MULTI_DELETE_STATE] = '_OnToggleCustomMarkMultiDeleteState',
    [MessageConst.ON_CUSTOM_MARK_MULTI_DELETE_SELECT_STATE_CHANGED] = '_OnCustomMarkMultiDeleteSelectStateChanged',
}

MapCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    args = args or {}
    self.view.blockMask.gameObject:SetActive(false)
    self.view.controllerFocusAnim.gameObject:SetActive(DeviceInfo.usingController)
    self.m_controllerRect = self.view.levelMapController.view.rectTransform
    self.m_waitRecoverTierId = args.tierId or -1
    self:_ParseCustomArgs(args.customArgs)
    self:_RefreshMapRectMask()

    if self.m_onMapPanelOpen ~= nil then
        self.m_onMapPanelOpen()
    end

    self:_InitCloseButton()
    self:_InitFilterButton()
    self:_InitBuildingAndCollectionHoverButton()


    local markInstId, levelId = args.instId, args.levelId
    if not string.isEmpty(MapCtrl.s_initialSelectMarkInstId) then
        markInstId = MapCtrl.s_initialSelectMarkInstId
        MapCtrl.s_initialSelectMarkInstId = ""
    end
    local needShowDetail = not string.isEmpty(markInstId)
    if needShowDetail then
        levelId = GameInstance.player.mapManager:GetMarkInstRuntimeDataLevelId(markInstId)
        self.m_waitInitShowDetailMarkInstId = markInstId
    end

    self.m_initialLevelId = not string.isEmpty(levelId) and levelId or GameWorld.worldInfo.curLevelId
    if BEYOND_DEBUG_COMMAND then
        if not GameInstance.player.mapManager:IsLevelLoaderDataExists(self.m_initialLevelId) then
            self:_InitDebugMode()
            self:_InitTierSwitcherNode()
            self:_RefreshTitle(levelId)
            self:_HideNotExpectedNodes()
            self.view.zoomNode.gameObject:SetActive(false)
            return
        end
    end

    self:_InitLevelMapController()
    self:_CheckAndRefreshNeedPlayMistUnlockedAnimationState()
    self:_InitBigRectHelper()
    self:_InitZoomNode()
    self:_InitRegionMapButton()
    self:_InitSelectOptionList()
    self:_InitWalletBar()

    self:_RefreshLevelMapContent()
    self:_TryPlayMapMaskAnimation()

    self:_InitMapRemindTip()

    self:_InitTierSwitcherNode()


    self:_SetDetectorState(markInstId, true, DataManager.uiLevelMapConfig.detectorZoomSliderPercent, args.isRecover)

    if needShowDetail and not self.m_forceDoNotShowDetail then
        self:_ShowMarkDetail(markInstId, true, false, true)
    end

    self:_InitPlayerIcon()

    self.m_waitInitShowDetailMarkInstId = ""

    if args.needTransit == nil then
        args.needTransit = false
    end
    self.view.transitBlack.gameObject:SetActive(args.needTransit)

    self:_InitMapController()

    self:_TryPlayMistUnlockedAnimation()
    self:_InitCustomMark()

    self:_TryRecoverBigRectState(args)

    if BEYOND_DEBUG_COMMAND then
        self:_InitDebugTeleport()
    end
end

MapCtrl.OnClose = HL.Override() << function(self)
    if self.m_onMapPanelClose then
        self.m_onMapPanelClose()
    end

    self.m_selectNodeShowTimer = self:_ClearTimer(self.m_selectNodeShowTimer)
    self.m_selectNodeTick = LuaUpdate:Remove(self.m_selectNodeTick)
    self.m_addZoomTick = LuaUpdate:Remove(self.m_addZoomTick)
    self.m_reduceZoomTick = LuaUpdate:Remove(self.m_reduceZoomTick)
    self.m_tierCheckTick = LuaUpdate:Remove(self.m_tierCheckTick)
    self.m_controllerSizeTick = LuaUpdate:Remove(self.m_controllerSizeTick)
    self:_StopCheckSwitchTierOnFocus()
    self:_StopPlayerIconLimit()
    self:_ResetFilterState()
    MapMarkDetailCommon.s_forbidAllBtn = false
    MapSpaceshipNode.ClearStaticFromData()
    GameInstance.player.mapManager.forceShowFacMarkInRegionList:Clear()
    MapCtrl.s_initialSelectMarkInstId = ""
end

MapCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)

    local zoomNode = self.view.zoomNode
    zoomNode.addKeyHint.gameObject:SetActive(active)
    zoomNode.reduceKeyHint.gameObject:SetActive(active)
end















MapCtrl.m_onMapPanelOpen = HL.Field(HL.Function)

MapCtrl.m_onMapPanelClose = HL.Field(HL.Function)

MapCtrl.m_expectedPanelNodes = HL.Field(HL.Table)

MapCtrl.m_expectedStaticElementTypes = HL.Field(HL.Table)

MapCtrl.m_expectedMarks = HL.Field(HL.Table)

MapCtrl.m_topOrderMarks = HL.Field(HL.Table)

MapCtrl.m_noGeneralTracking = HL.Field(HL.Boolean) << false

MapCtrl.m_noMissionTracking = HL.Field(HL.Boolean) << false

MapCtrl.m_noCustomMark = HL.Field(HL.Boolean) << false

MapCtrl.m_forceDoNotShowDetail = HL.Field(HL.Boolean) << false

MapCtrl._ParseCustomArgs = HL.Method(HL.Any) << function(self, customArgs)
    if customArgs == nil then
        local stateArgs = MapConst.GAMEPLAY_STATE_CUSTOM_GETTER()
        customArgs = stateArgs and stateArgs or {}
    end

    if customArgs.onMapPanelOpen then
        self.m_onMapPanelOpen = customArgs.onMapPanelOpen
    end
    if customArgs.onMapPanelClose then
        self.m_onMapPanelClose = customArgs.onMapPanelClose
    end
    self.m_expectedPanelNodes = customArgs.expectedPanelNodes or {}
    self.m_expectedStaticElementTypes = customArgs.expectedStaticElementTypes or {}
    self.m_expectedMarks = customArgs.expectedMarks or {}
    self.m_topOrderMarks = customArgs.topOrderMarks or {}
    self.m_noGeneralTracking = customArgs.noGeneralTracking == true
    self.m_noMissionTracking = customArgs.noMissionTracking == true
    self.m_noCustomMark = customArgs.noCustomMark == true
    self.m_forceDoNotShowDetail = customArgs.forceDoNotShowDetail == true
    self.m_ignoreOpenFocus = customArgs.ignoreOpenFocus == true
    if customArgs.forbidDetailBtn then
        MapMarkDetailCommon.s_forbidAllBtn = true
    end
end

MapCtrl._HideNotExpectedNodes = HL.Method(HL.Opt(HL.Table)) << function(self, overrideExpectedPanelNodes)
    local typesToViewNodes = {
        [MapPanelNodeType.Remind] = self.view.transactionReminderNode,
        [MapPanelNodeType.Tracking] = self.view.mapTrackingInfo,
        [MapPanelNodeType.Zoom] = self.view.zoomNode,
        [MapPanelNodeType.DomainSwitch] = self.view.regionMapBtn,
        [MapPanelNodeType.LevelInfo] = self.view.infoNode,
        [MapPanelNodeType.Filter] = self.view.filterBtn,
        [MapPanelNodeType.TierSwitch] = self.view.tierSwitcherNode,
        [MapPanelNodeType.SpaceshipJump] = self.view.mapSpaceshipNode,
        [MapPanelNodeType.WalletBar] = self.view.walletBarPlaceholder,
    }

    local expectedPanelNodes
    if overrideExpectedPanelNodes ~= nil then

        expectedPanelNodes = overrideExpectedPanelNodes
    else

        expectedPanelNodes = self.m_expectedPanelNodes
    end
    if next(expectedPanelNodes) == nil then

        expectedPanelNodes = MapConst.LEVEL_EXPECTED_PANEL_NODES_GETTER[self.m_currLevelId]
    end

    local allVisible = false
    if expectedPanelNodes == nil or not next(expectedPanelNodes) then
        allVisible = true
    end

    for type, node in pairs(typesToViewNodes) do
        node.gameObject:SetActive(allVisible or expectedPanelNodes[type] == true)
    end
end






MapCtrl.OnSelectMark = HL.StaticMethod(HL.Any) << function(arg)
    local markInstId = arg
    if type(arg) == "table" then
        markInstId = unpack(arg)
    end
    if string.isEmpty(markInstId) then
        MapCtrl.s_initialSelectMarkInstId = ""
        return
    end

    local isOpen, self = UIManager:IsOpen(PANEL_ID)
    if isOpen then
        if self.m_isResettingToTargetLevel then
            self.m_waitInitShowDetailMarkInstId = markInstId
        else
            self:ResetMapStateToTargetLevel({ instId = markInstId })
        end
    else
        MapCtrl.s_initialSelectMarkInstId = markInstId
    end
end

MapCtrl._OnTeleportFinish = HL.Method() << function(self)
    MapSpaceshipNode.ClearStaticFromData()
    Notify(MessageConst.RECOVER_PHASE_LEVEL)
end

MapCtrl._OnLevelSwitchBtnClicked = HL.Method(HL.Any) << function(self, args)
    if self:IsPlayingAnimationIn() then
        return
    end

    if self.m_isResettingToTargetLevel then
        return
    end

    local targetLevelId = args
    if type(args) == "table" then
        targetLevelId = unpack(args)
    end

    local currConfigSuccess, currLevelConfig = GameInstance.player.mapManager:GetLoaderLevelBasicInfoByLevelId(self.m_currLevelId)
    if not currConfigSuccess then
        return
    end

    local targetConfigSuccess, targetLevelConfig = GameInstance.player.mapManager:GetLoaderLevelBasicInfoByLevelId(targetLevelId)
    if not targetConfigSuccess then
        return
    end
    GameInstance.player.mapManager:RemoveSelectCustomMark()
    if currLevelConfig.isSingleLevel or targetLevelConfig.isSingleLevel then
        self:ResetMapStateToTargetLevel({ levelId = targetLevelId })
    else
        self.view.levelMapController:SwitchToTargetLevel(targetLevelId)
    end
end

MapCtrl._OnDataChanged = HL.Method(HL.Table) << function(self, args)
    local instId, isAdd = unpack(args)
    if not isAdd then
        self:_HideSelectIcon(instId, true)
        if self.m_isMarkDetailShowing and self.m_selectMarkInstId == instId then
            Notify(MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL, true)
        end
    end
    local mapController = self.view.levelMapController
    mapController:_ResetLoaderMarksVisibleStateByFilter()
    mapController:RefreshLoaderMarksVisibleStateByLayer(mapController.m_currentLayer)
    local currLevelId = self.view.levelMapController:GetControllerCurrentLevelId()
    self:_RefreshRemindTipInfo(currLevelId)
    self:_RefreshHintTipsNode()
end

MapCtrl._OnScreenSizeChanged = HL.Method() << function(self)
    self:_RefreshMapRectMask()
end

MapCtrl._OnFacNodeChanged = HL.Method(HL.Any) << function(self, args)
    self:_StartCoroutine(function()

        coroutine.step()
        self:_RefreshBuildingInfos(self.m_currLevelId)
    end)
end






MapCtrl._RefreshTitle = HL.Method(HL.String) << function(self, levelId)
    local levelBasicSuccess, levelBasicInfo = DataManager.levelBasicInfoTable:TryGetValue(levelId)
    if not levelBasicSuccess then
        return
    end

    local success, levelDesc = Tables.levelDescTable:TryGetValue(levelId)
    if not success then
        return
    end

    self.view.titleNode.levelTitleTxt.text = levelDesc.showName
    self.view.titleNode.domainTitleTxt.gameObject:SetActive(false)

    local configSuccess, levelConfig = GameInstance.player.mapManager:GetLoaderLevelBasicInfoByLevelId(levelId)
    if not configSuccess then
        return
    end

    local domainId = levelBasicInfo.domainName
    local domainSuccess, domainData = Tables.domainDataTable:TryGetValue(domainId)
    if domainSuccess and not levelConfig.isSingleLevel then
        self.view.titleNode.domainTitleTxt.text = domainData.domainName
        self.view.titleNode.domainTitleTxt.gameObject:SetActive(true)
    end
end

MapCtrl._RefreshMapRectMask = HL.Method() << function(self)
    local isStandardScreenRatio = Screen.width * STANDARD_SCREEN_HEIGHT == STANDARD_SCREEN_WIDTH * Screen.height
    self.view.mapRectMask.enabled = not isStandardScreenRatio
end






MapCtrl._InitLevelMapController = HL.Method() << function(self)
    self.view.levelMapController:InitLevelMapController(MapConst.LEVEL_MAP_CONTROLLER_MODE.LEVEL_SWITCH, {
        onLevelSwitch = function(targetInfo)
            self:_OnLevelSwitch(targetInfo)
        end,
        onLevelSwitchStart = function(targetInfo)
            self:_OnLevelSwitchStart(targetInfo)
        end,
        onLevelSwitchFinish = function()
            self:_OnLevelSwitchFinish()
        end,
        onTrackingMarkClicked = function(instId, trackingMark, relatedMark)
            self:_OnTrackingMarkClicked(instId, trackingMark, relatedMark)
        end,
        onMarkHover = function(instId, isHover)
            self:_OnControllerMarkHover(instId, isHover)
        end,
        gridsMaskFollowTarget = self.view.mapMask,
        initialLevelId = self.m_initialLevelId,
        visibleRect = self.view.markVisibleRect,
        expectedMarks = self.m_expectedMarks,
        expectedStaticElements = self.m_expectedStaticElementTypes,
        topOrderMarks = self.m_topOrderMarks,
        noGeneralTracking = self.m_noGeneralTracking,
        noMissionTracking = self.m_noMissionTracking,
    })

    self.m_controllerSizeTick = LuaUpdate:Add("LateTick", function()
        self.m_controllerRect.sizeDelta = self.view.levelMapController.view.levelMapLoader.view.viewRect.sizeDelta
    end)
end

MapCtrl._OnLevelSwitch = HL.Method(HL.Table) << function(self, switchTargetInfo)
    self:_SetMapRectByTargetLevelInfo(switchTargetInfo)
end

MapCtrl._OnLevelSwitchStart = HL.Method(HL.Table) << function(self, switchTargetInfo)
    self.view.fullScreenMask.gameObject:SetActive(true)
    self.view.bigRectHelper:ClearAllTween()
    self.view.bigRectHelper.enabled = false
    self.view.touchPanel.enabled = false
    self.m_controllerRect:SetParent(self.view.mapMask)
    self:_SetMapRectByTargetLevelInfo(switchTargetInfo)
    self:_StopPlayerIconLimit()

    self:_PlayAndSetMainNodeVisibleState(false)

    if self.m_isMarkDetailShowing then
        Notify(MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL, true)
    end

    self:_ToggleControllerMoveAndZoom(false)
end

MapCtrl._OnLevelSwitchFinish = HL.Method() << function(self)
    self.view.fullScreenMask.gameObject:SetActive(false)
    self.view.touchPanel.enabled = true
    self.m_controllerRect:SetParent(self.view.mapRect)
    self:_RefreshLevelMapContent()
    self:_ResetBigRectHelper()
    self:_ResetZoomSliderValue(false)
    self:_RefreshPlayerIconNeedLimit()

    self:_PlayAndSetMainNodeVisibleState(true)

    self:_ToggleControllerMoveAndZoom(true)

    self:_CheckAndRefreshNeedPlayMistUnlockedAnimationState()
    self:_TryPlayMistUnlockedAnimation()
end

MapCtrl._SetMapRectByTargetLevelInfo = HL.Method(HL.Table) << function(self, targetInfo)
    self.view.mapRect.pivot = Vector2(0.5, 1.0)
    local resetPos = self:_GetTargetRelativeCenterPosition(self.view.mapRect, {
        scale = targetInfo.scale,
        width = targetInfo.size.x,
        height = targetInfo.size.y,
    })
    self.view.mapRect.sizeDelta = targetInfo.size
    self.view.mapRect.localScale = Vector3.one * targetInfo.scale
    self.view.mapRect.anchoredPosition = resetPos + targetInfo.initialOffset
end

MapCtrl._RefreshLevelMapContent = HL.Method() << function(self)
    local currLevelId = self.view.levelMapController:GetControllerCurrentLevelId()
    self:_RefreshTitle(currLevelId)
    self:_RefreshBuildingInfos(currLevelId)
    self:_RefreshCollectionsInfo(currLevelId)
    self:_RefreshTrackingInfo(currLevelId)
    self:_RefreshSpaceshipNode(currLevelId)
    self:_RefreshSpaceshipLevelNode(currLevelId)
    self:_RefreshRemindTipInfo(currLevelId)

    self.m_currLevelId = currLevelId

    local success, levelConfig = Utils.getLevelConfig(self.m_currLevelId)
    if success then
        self.m_currMapId = levelConfig.mapIdStr
    end

    UIManager:Close(PanelId.MapCustomMarkDetail)
    CS.Beyond.Gameplay.Conditions.OnUILevelMapEnterLevel.Trigger(currLevelId)

    self:_HideNotExpectedNodes()
    self:_RefreshHintTipsNode()
end

MapCtrl._GetTargetRelativeCenterPosition = HL.Method(Unity.RectTransform, HL.Table).Return(Vector2) << function(self, rectTransform, targetInfo)
    local parentRectTransform = rectTransform.parent:GetComponent("RectTransform")
    if parentRectTransform == nil then
        return
    end




    local anchorOffset = Vector2(
        parentRectTransform.rect.width * (0.5 - rectTransform.anchorMin.x),
        parentRectTransform.rect.height * (0.5 - rectTransform.anchorMin.y)
    )

    local pivotOffset = Vector2(
        targetInfo.scale * targetInfo.width * (rectTransform.pivot.x - 0.5),
        targetInfo.scale * targetInfo.height * (rectTransform.pivot.y - 0.5)
    );

    return anchorOffset + pivotOffset
end






MapCtrl._LockMarkClick = HL.Method() << function(self)
    if self.m_markClickLockThread ~= nil then
        return
    end
    self.m_markClickLockThread = self:_StartCoroutine(function()
        coroutine.step()
        self.m_markClickLockThread = self:_ClearCoroutine(self.m_markClickLockThread)
    end)
end

MapCtrl._IsMarkClickLocked = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_markClickLockThread ~= nil
end

MapCtrl._OnLevelMapMarkClicked = HL.Method(HL.String) << function(self, markInstId)
    if self:_IsMarkClickLocked() then
        return
    end

    local nearbyDistance = DeviceInfo.usingController and
        self.view.config.CONTROLLER_NEARBY_MARK_DISTANCE or
        self.view.config.NEARBY_MARK_DISTANCE
    local nearbyMarkList = self.view.levelMapController:GetControllerNearbyMarkList(
        markInstId,
        nearbyDistance,
        self.view.zoomNode.zoomSlider.value,
        self.m_multiDeletePanelShow and GEnums.MarkType.CustomMark
    )

    if #nearbyMarkList <= 1 then
        self:_ShowMarkDetail(markInstId, true, true)
    else
        self:_RefreshSelectOptionList(markInstId, nearbyMarkList)
    end

    local isInCustomMarkDeleteMode = UIManager:IsOpen(PanelId.MapCustomMarkDelete)
    if not isInCustomMarkDeleteMode then
        self:_ToggleControllerMoveAndZoom(false)
    end
end

MapCtrl._OnJumpToTpMark = HL.Method(HL.String) << function(self, tpMarkInstId)
    if GameInstance.player.mapManager:IsTrackingRelatedMark(tpMarkInstId) then
        local trackingMark, relatedMark = self.view.levelMapController:GetControllerTrackingMark(tpMarkInstId)
        self:_OnTrackingMarkClicked(tpMarkInstId, trackingMark, relatedMark)
    else
        local tpMark = self.view.levelMapController:GetControllerMarkByInstId(tpMarkInstId, true)
        if tpMark and tpMark.gameObject.activeSelf then
            self:_ShowMarkDetail(tpMarkInstId, true, true)
        else
            self:ResetMapStateToTargetLevel({ instId = tpMarkInstId })
        end
    end
end

MapCtrl._ShowSelectIcon = HL.Method(HL.String) << function(self, markInstId)
    self.m_selectMarkRectTransform = self.view.levelMapController:GetControllerMarkRectTransform(markInstId)
    self:_ToggleSelectIconSyncTick(true)

    self.view.selectIconAnim:ClearTween(true)
    UIUtils.PlayAnimationAndToggleActive(self.view.selectIconAnim, true)
end

MapCtrl._HideSelectIcon = HL.Method(HL.String, HL.Opt(HL.Boolean, HL.Boolean)) << function(self, markInstId, force, onlyHideNode)
    if onlyHideNode then
        UIUtils.PlayAnimationAndToggleActive(self.view.selectIconAnim, false)
        return
    end

    if markInstId ~= self.m_selectMarkInstId then
        return
    end

    local rectTransform = self.view.levelMapController:GetControllerMarkRectTransform(markInstId)
    UIUtils.PlayAnimationAndToggleActive(self.view.selectIconAnim, false)
    if self.m_multiDeletePanelShow and rectTransform and not force then
        return
    end
    self:_ToggleSelectIconSyncTick(false)
end

MapCtrl._ToggleSelectIconSyncTick = HL.Method(HL.Boolean) << function(self, active)
    if self.m_selectNodeTick > 0 then
        self.m_selectNodeTick = LuaUpdate:Remove(self.m_selectNodeTick)
    end
    if active then
        self.m_selectNodeTick = LuaUpdate:Add("LateTick", function(deltaTime)
            self:_SyncSelectIconPosition()
        end)
    end
end

MapCtrl._SyncSelectIconPosition = HL.Method() << function(self)
    if IsNull(self.m_selectMarkRectTransform) then
        return
    end
    self.view.selectIconNode.position = self.m_selectMarkRectTransform.position
end

MapCtrl._IsWaitingInitShowMarkDetail = HL.Method().Return(HL.Boolean) << function(self)
    return not string.isEmpty(self.m_waitInitShowDetailMarkInstId)
end

MapCtrl._ShowMarkDetail = HL.Method(HL.String, HL.Opt(HL.Boolean, HL.Boolean, HL.Boolean)) << function(self, markInstId, needFocus, needFocusTween, isInit)
    local mark = self.view.levelMapController:GetControllerMarkByInstId(markInstId, true)
    if not mark then
        return
    end
    local markRectTransform = mark.rectTransform
    if not markRectTransform then
        return
    end
    if not GameInstance.player.mapManager:IsTrackingRelatedMark(markInstId) then
        mark:ToggleForceShowMark("ShowDetail", true)
    end

    needFocusTween = needFocusTween == true


    if self.m_multiDeletePanelShow then
        local trackingMark
        if markInstId == GameInstance.player.mapManager.trackingMarkInstId then
            trackingMark = self.view.levelMapController.view.levelMapLoader:GetGeneralTrackingMark()
        end
        local args = {
            mark = mark,
            instId = markInstId,
            trackMark = trackingMark
        }
        Notify(MessageConst.ON_CUSTOM_MARK_MULTI_DELETE_SELECT, args)
    else
        self.view.levelMapController:SetSingleMarkToTopOrder(markInstId)
        self:_ToggleControllerMoveAndZoom(false)
        Notify(MessageConst.SHOW_LEVEL_MAP_MARK_DETAIL, {
            markInstId = markInstId,
            onClosedCallback = function()
                self.m_selectNodeShowTimer = self:_ClearTimer(self.m_selectNodeShowTimer)
                self.view.levelMapController:ResetSingleMarkToOriginalOrder(markInstId)
                self:_HideSelectIcon(markInstId)
                self:_SetDetectorState(markInstId, false)
                self.m_selectMarkInstId = ""
                self.m_isMarkDetailShowing = false
                mark:ToggleForceShowMark("ShowDetail", false)
                self:_ToggleControllerMoveAndZoom(true)
            end
        })
        self:_LockMarkClick()
        self.m_isMarkDetailShowing = true
    end


    self.m_selectNodeShowTimer = self:_ClearTimer(self.m_selectNodeShowTimer)
    if not string.isEmpty(self.m_selectMarkInstId) then
        self:_HideSelectIcon(self.m_selectMarkInstId, true, true)
    end

    if not self.m_multiDeletePanelShow then
        if needFocus then
            self.m_selectNodeShowTimer = self:_StartTimer(self.view.config.SELECT_ICON_SHOW_DELAY, function()
                self:_ShowSelectIcon(markInstId)
                self.m_selectNodeShowTimer = self:_ClearTimer(self.m_selectNodeShowTimer)
            end)
            if isInit then
                self:_BigRectFocusNodeOnInit(markRectTransform, needFocusTween, function()
                    self.view.focusArrowNode.position = markRectTransform.position
                    self:_StopCheckSwitchTierOnFocus()
                end)
            else
                self.view.bigRectHelper:FocusNode(markRectTransform, needFocusTween, function()
                    self.view.focusArrowNode.position = markRectTransform.position
                    self:_StopCheckSwitchTierOnFocus()
                end)
            end

            if needFocusTween then
                self:_StartCheckSwitchTierOnFocus(markInstId)
            else
                self:_TrySwitchTierOnFocus(mark)
            end
        else
            self:_SyncSelectIconPosition()
            self:_ShowSelectIcon(markInstId)
            self:_TrySwitchTierOnFocus(mark)
        end
    else
        self:_SyncSelectIconPosition()
    end

    self.m_selectMarkInstId = markInstId
end

MapCtrl._SetDetectorState = HL.Method(HL.Any, HL.Boolean, HL.Opt(HL.Number, HL.Boolean)) << function(self, instId, focus, percent, skipAnim)
    if not instId then
        return
    end
    local isDetector = false
    local runtimeSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(instId)
    if not runtimeSuccess then
        return
    end
    local templateSuccess, templateData = Tables.mapMarkTempTable:TryGetValue(markRuntimeData.templateId)
    if not templateSuccess then
        return
    end
    local markType = templateData.markType
    if MapUtils.isDetectorMarkType(markType) then
        isDetector = true
    end
    local mark = self.view.levelMapController:GetControllerMarkByInstId(instId, true)
    if not mark then
        return
    end
    local loader = self.view.levelMapController.view.levelMapLoader
    if isDetector and not skipAnim then
        loader.view.detectorNode.gameObject:SetActive(focus)
        mark:RefreshDetectorNodeState(focus)
        if focus then
            loader.view.detectorNode.anchoredPosition = GameInstance.player.mapManager.characterRectPosition
            self:_HideSelectIcon(instId)
            if percent then
                local zoomSlider = self.view.zoomNode.zoomSlider
                local zoomValue = zoomSlider.maxValue - zoomSlider.minValue
                zoomSlider.value = zoomSlider.minValue + zoomValue * percent
                self:_RefreshZoomVisibleLayer(zoomValue, true)
            end
            local markRectTransform = self.view.levelMapController:GetControllerMarkRectTransform(instId)
            self:_BigRectFocusNodeOnInit(markRectTransform, true, function()
                self.view.focusArrowNode.position = markRectTransform.position
            end)
        end
        return
    end
    loader.view.detectorNode.gameObject:SetActive(false)
    mark:RefreshDetectorNodeState(false)
end






MapCtrl._OnShowMarkMultiDelete = HL.Method(HL.Table) << function(self, args)
    self.m_multiDeletePanelShow = true
    local loader = self.view.levelMapController.view.levelMapLoader
    local trackingMarkId = GameInstance.player.mapManager.trackingMarkInstId

    local showTracking = false
    local runtimeSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(trackingMarkId)
    if runtimeSuccess then
        local templateSuccess, templateData = Tables.mapMarkTempTable:TryGetValue(markRuntimeData.templateId)
        if templateSuccess then
            local markType = templateData.markType
            if markType == GEnums.MarkType.CustomMark then
                showTracking = true
            end
            loader:ToggleLoaderGeneralTrackingVisibleState(showTracking)
        end
    end
    loader:ToggleLoaderMissionTrackingVisibleState(false)
    loader:ToggleLoaderSwitchMaskVisibleState(false)
    loader:ToggleLoaderSwitchMaskVisibleState(false)
    loader:ToggleLoaderLineRootVisibleState(false)
    loader:ToggleLoaderGamePlayAreaVisibleState(false)
    loader:ToggleLoaderMissionAreaVisibleState(false)
    self.view.closeBtn.gameObject:SetActive(false)
    self:_HideNotExpectedNodes(MapConst.DELETE_MODE_MAP_EXPECTED_NODES)
end

MapCtrl._OnHideMarkMultiDelete = HL.Method() << function(self)
    self.m_multiDeletePanelShow = false
    local loader = self.view.levelMapController.view.levelMapLoader
    loader:ToggleLoaderGeneralTrackingVisibleState(not self.m_noGeneralTracking)
    loader:ToggleLoaderMissionTrackingVisibleState(not self.m_noMissionTracking)
    loader:ToggleLoaderSwitchMaskVisibleState(true)
    loader:ToggleLoaderLineRootVisibleState(true)
    loader:ToggleLoaderGamePlayAreaVisibleState(true)
    loader:ToggleLoaderMissionAreaVisibleState(true)
    self.m_selectMarkInstId = ""

    self.view.closeBtn.gameObject:SetActive(true)
    self:_HideNotExpectedNodes()
end

MapCtrl._OnToggleCustomMarkMultiDeleteState = HL.Method(HL.Table) << function(self, args)
    if DeviceInfo.usingController then
        self:_OnControllerCustomMarkMultiDeleteStateChange(args.isShow)
    end
end

MapCtrl._OnCustomMarkMultiDeleteSelectStateChanged = HL.Method(HL.Table) << function(self, args)
    if DeviceInfo.usingController then
        local bindingText = args.isSelect and Language.LUA_MAP_CUSTOM_MARK_MULTI_SELECT_CANCEL or Language.LUA_MAP_CUSTOM_MARK_MULTI_SELECT
        InputManagerInst:SetBindingText(self.view.bigRectHelper.clickBindingId, bindingText)
    end
end

MapCtrl._InitCustomMark = HL.Method() << function(self)
    if self.m_noCustomMark then
        return
    end

    local mapManager = GameInstance.player.mapManager
    local ids = {}
    local levelInst = mapManager:GetQuickSearchCustomMarksByLevel(self.m_initialLevelId)
    if levelInst then
        for markInstId, mapMarkRuntimeData in pairs(levelInst) do
            local pos = mapMarkRuntimeData.rectPosition
            local viewRectSize = self.view.levelMapController.view.levelMapLoader.view.viewRect.sizeDelta
            local viewRectPos = self.view.levelMapController.view.levelMapLoader.view.viewRect.anchoredPosition

            if viewRectSize.x / 2 - (math.abs(pos.x - viewRectPos.x)) < 0
                or viewRectSize.y / 2 - (math.abs(pos.y - viewRectPos.y)) < 0 then
                if markInstId == GameInstance.player.mapManager.trackingMarkInstId then
                    mapManager:TrackMark(markInstId, false)
                end
                table.insert(ids, markInstId)
            end
        end
    end

    if #ids > 0 then
        mapManager:RemoveCustomMarkToServer(ids)
    end

    self.view.touchPanel.onClick:RemoveAllListeners()
    self.view.touchPanel.onClick:AddListener(function(eventData)
        if self:_IsMarkClickLocked() then
            return
        end

        if BEYOND_DEBUG_COMMAND then
            if self.view.debugToggle.isOn then
                return
            end
        end

        if self.m_optionCloseCD or not eventData then
            return
        end

        local curNum = GameInstance.player.mapManager:GetQuickSearchCustomMarkCountByLevel(self.m_currLevelId)
        local maxNum = Tables.GlobalConst.maxSceneCustomMapMarkNumber

        if curNum + 1 > maxNum then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_MAP_CUSTOM_MARK_MAX_NUM_TOAST)
            return
        end


        if not UIUtils.isScreenPosInRectTransform(eventData.position, self.view.levelMapController.gameObject.transform, self.uiCamera) then
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_CUSTOM_MARK_OUT_EDGE_TOAST)
            return
        end

        if self.m_multiDeletePanelShow then
            return
        end
        local rectPos = UIUtils.screenPointToUI(
            eventData.position,
            self.uiCamera,
            self.view.levelMapController.view.levelMapLoader.view.rectTransform
        )
        local tempPos = Vector2.zero
        local success, levelConfig = GameInstance.player.mapManager:GetLoaderLevelBasicInfoByLevelId(self.m_currLevelId)
        if not success then
            return
        end
        if levelConfig.needInverseXZ then
            tempPos.x = -rectPos.y
            tempPos.y = rectPos.x
        else
            tempPos = rectPos
        end
        local worldPos = self.view.levelMapController.view.levelMapLoader:GetWorldPositionByRectPosition(tempPos)
        success, levelConfig = Utils.getLevelConfig(self.m_currLevelId)
        if not success then
            return
        end
        if not string.isEmpty(self.m_selectMarkInstId) then
            return
        end
        if not string.isEmpty(self.m_phase.m_detailPanelShownId)then
            return
        end
        AudioAdapter.PostEvent("Au_UI_Button_MapIcon")
        local mapId = levelConfig.mapIdStr
        local markInstId = GameInstance.player.mapManager:AddSelectCustomMark(mapId, self.m_currLevelId, worldPos, self.m_currTierIdList[self.m_currTierIndex])
        if not markInstId then
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_CUSTOM_MARK_OUT_EDGE_TOAST)
            return
        end
        self:_ShowMarkDetail(markInstId)
        self:_ToggleControllerMoveAndZoom(false)
    end)
end






MapCtrl._InitMapRemindTip = HL.Method() << function(self)

    self.view.mapRemindBtn.onClick:RemoveAllListeners()
    self.view.mapRemindBtn.onClick:AddListener(function()
        self:_OnRemindTipBtnClick()
    end)

    self:_RefreshHintTipsNode()
end

MapCtrl._RefreshHintTipsNode = HL.Method() << function(self)
    if Utils.isInSpaceShip() then
        self.view.hintTipsNode.gameObject:SetActiveIfNecessary(false)
        return
    end

    local chapterId = ScopeUtil.GetChapterId(self.m_currLevelId)
    if chapterId ~= Utils.getCurrentChapterId() then
        self.view.hintTipsNode.gameObject:SetActiveIfNecessary(false)
        return
    end
    local hasInvalidPlacedBuilding = GameInstance.player.remoteFactory:HasInvalidPlacedBuilding(chapterId)
    self.view.hintTipsNode.gameObject:SetActiveIfNecessary(hasInvalidPlacedBuilding)
    self.view.hintTipsNodeButton.onClick:RemoveAllListeners()
    if hasInvalidPlacedBuilding then
        self.view.hintTipsNodeButton.onClick:AddListener(function()
            UIManager:Open(PanelId.MapWaitDeleteBuildingList, chapterId)
        end)
    end
end

MapCtrl._RefreshRemindTipInfo = HL.Method(HL.String) << function(self, levelId)
    self.m_remindTipInfo = MapUtils.getMapRemindTipInfo(levelId)
end

MapCtrl._OnRemindTipBtnClick = HL.Method(HL.Opt(HL.Number)) << function(self, initIndex)
    self.view.mapRemindBtn.gameObject:SetActiveIfNecessary(false)
    self.view.mapTrackingInfo.gameObject:SetActiveIfNecessary(false)
    self.view.mapTransactionReminderPopUp.gameObject:SetActiveIfNecessary(true)
    self.m_isRemindTipShowing = true

    if DeviceInfo.usingController then
        self:_ToggleControllerMoveAndZoom(false)
        Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
            panelId = PANEL_ID,
            isGroup = true,
            id = self.view.mapTransactionReminderPopUp.view.inputGroup.groupId,
            hintPlaceholder = self.view.controllerHintPlaceholder,
            rectTransform = self.view.mapTransactionReminderPopUp.view.rectTransform,
            panelOffset = MAP_BLOCK_ORDER_OFFSET,
            noHighlight = true,
        })
    end

    self.view.mapTransactionReminderPopUp:InitMapRemind(self.m_currLevelId, self.m_remindTipInfo, function()
        if DeviceInfo.usingController then
            self:_ToggleControllerMoveAndZoom(true)
            Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.mapTransactionReminderPopUp.view.inputGroup.groupId)
        end

        self.view.mapTransactionReminderPopUp.view.wrapper:PlayOutAnimation(function()
            self.view.mapRemindBtn.gameObject:SetActiveIfNecessary(true)
            self.view.mapTrackingInfo.gameObject:SetActiveIfNecessary(true)
            self.view.mapTransactionReminderPopUp.gameObject:SetActiveIfNecessary(false)
            self:_RefreshHintTipsNode()
        end)

        self.m_isRemindTipShowing = false
    end, initIndex)

    self.view.hintTipsNode.gameObject:SetActiveIfNecessary(false)
end

MapCtrl.GetRemindTipState = HL.Method().Return(HL.Table) << function(self)
    local tabIndex
    if self.m_isRemindTipShowing then
        tabIndex = self.view.mapTransactionReminderPopUp:GetCurTabIndex()
    end
    return {
        isShowing = self.m_isRemindTipShowing,
        tabIndex = tabIndex,
    }
end

MapCtrl.RecoverRemindTip = HL.Method(HL.Number) << function(self, initIndex)
    self:_OnRemindTipBtnClick(initIndex)
end






MapCtrl._InitSelectOptionList = HL.Method() << function(self)
    self.m_selectOptionCells = UIUtils.genCellCache(self.view.selectOptionNode.selectOptionCell)

    self.view.selectOptionNode.button.onClick:AddListener(function()
        if self.m_optionCloseCDTimer > 0 then
            self.m_optionCloseCDTimer = self:_ClearTimer(self.m_optionCloseCDTimer)
        end
        self.m_optionCloseCD = true
        self.m_optionCloseCDTimer = self:_StartTimer(0.1, function()
            self.m_optionCloseCD = false
        end)
        self:_RefreshSelectOptionListShownState(false)
        self:_ToggleControllerMoveAndZoom(true)
    end)
    self.view.selectOptionNode.gameObject:SetActive(false)
end

MapCtrl._RefreshSelectOptionList = HL.Method(HL.String, HL.Table) << function(self, markInstId, nearbyMarkList)
    local markRectTransform = self.view.levelMapController:GetControllerMarkRectTransform(markInstId)
    if markRectTransform == nil then
        return
    end

    self.m_selectOptionMarkIdList = nearbyMarkList
    self.m_selectOptionMarkList = {}

    self.m_selectOptionCells:Refresh(#nearbyMarkList, function(cell, index)
        local nearbyInstId = nearbyMarkList[index]
        local runtimeSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(nearbyInstId)
        if not runtimeSuccess then
            return
        end

        local templateId = markRuntimeData.templateId
        local templateSuccess, templateData = Tables.mapMarkTempTable:TryGetValue(templateId)
        if not templateSuccess then
            return
        end

        local icon = markRuntimeData.isActive and templateData.activeIcon or templateData.inActiveIcon
        cell.icon:LoadSprite(UIConst.UI_SPRITE_MAP_MARK_ICON_SMALL, icon)

        cell.icon.color = Color.white


        if templateData.markType == GEnums.MarkType.CustomMark then
            cell.name.text = markRuntimeData.note
        elseif templateData.markType == GEnums.MarkType.SnapshotActivity then
            cell.name.text = MapUtils.getActivitySnapShotMarkTitle(markRuntimeData)
        else
            cell.name.text = templateData.name
        end

        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            self:_OnSelectOptionClick(index)
        end)
        cell.button.onHoverChange:RemoveAllListeners()
        cell.button.onHoverChange:AddListener(function(isHover)
            if index == self.m_currHighlightOption then
                return
            end
            if isHover then
                local lastCell = self.m_selectOptionCells:GetItem(self.m_currHighlightOption)
                self:_RefreshSelectOptionHighlightState(lastCell, self.m_currHighlightOption, false)
                self:_RefreshSelectOptionHighlightState(cell, index, true)
                self.m_currHighlightOption = index
            end
        end)

        self.m_selectOptionMarkList[index] = self.view.levelMapController:GetControllerMarkByInstId(nearbyInstId)

        if index == INITIAL_SELECT_OPTION_INDEX then
            self:_RefreshSelectOptionHighlightState(cell, index, true, true)
            self.m_currHighlightOption = index
            self.view.selectOptionNode.selectOptionList:AutoScrollToRectTransform(cell.rectTransform)
        else
            self:_RefreshSelectOptionHighlightState(cell, index, false)
        end

        cell.gameObject.name = "SelectOption_" .. nearbyInstId
    end)

    self.view.bigSelectIcon.position = markRectTransform.position
    self:_RefreshSelectOptionListShownState(true)
    self:_LockMarkClick()
end

MapCtrl._OnSelectOptionClick = HL.Method(HL.Number) << function(self, index)
    local instId = self.m_selectOptionMarkIdList[index]
    self:_ShowMarkDetail(instId, true, true)
    self:_RefreshSelectOptionListShownState(false)
end

MapCtrl._RefreshSelectOptionListShownState = HL.Method(HL.Boolean) << function(self, isShown)
    self.view.bigSelectIcon.gameObject:SetActive(isShown)
    self.view.selectOptionNode.gameObject:SetActive(isShown)

    self:_RefreshSelectOptionMainAnimState(not isShown)

    if not isShown and self.m_selectOptionMarkList ~= nil then
        for _, mark in pairs(self.m_selectOptionMarkList) do
            mark:ToggleMarkHighlightState(false)
        end
    end

    if DeviceInfo.usingController then
        if isShown then
            Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
                panelId = PANEL_ID,
                isGroup = true,
                id = self.view.selectOptionNode.inputGroup.groupId,
                hintPlaceholder = self.view.controllerHintPlaceholder,
                rectTransform = self.view.selectOptionNode.selectOptionList.transform,
                panelOffset = MAP_BLOCK_ORDER_OFFSET,
                noHighlight = true,
            })
        else
            Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.selectOptionNode.inputGroup.groupId)
        end
    end

    if DeviceInfo.usingTouch then
        self.view.touchPanel.enabled = not isShown
    end
end

MapCtrl._RefreshSelectOptionMainAnimState = HL.Method(HL.Boolean) << function(self, showMain)
    self.m_nextOptionMainAnimState = showMain

    if self.m_optionAnimPlayThread == nil then
        self.m_optionAnimPlayThread = self:_StartCoroutine(function()
            coroutine.step()
            if self.m_nextOptionMainAnimState ~= self.m_optionMainAnimState then
                self:_PlayAndSetMainNodeVisibleState(self.m_nextOptionMainAnimState, function(isIn)
                    if isIn ~= self.m_nextOptionMainAnimState then
                        self:_PlayAndSetMainNodeVisibleState(self.m_nextOptionMainAnimState)
                    end
                    self.m_optionMainAnimState = self.m_nextOptionMainAnimState
                end)
            end
            self.m_optionAnimPlayThread = self:_ClearCoroutine(self.m_optionAnimPlayThread)
        end)
    end
end

MapCtrl._RefreshSelectOptionHighlightState = HL.Method(HL.Any, HL.Number, HL.Boolean, HL.Opt(HL.Boolean)) << function(self, cell, index, isHighlight, delayRefreshMarkHighlight)
    local animName = isHighlight and "select_item_selected" or "select_item_normal"
    cell.contentAnimation:PlayWithTween(animName)

    local mark = self.m_selectOptionMarkList[index]
    if mark ~= nil then
        if delayRefreshMarkHighlight then

            self.m_optionMarkHighlightThread = self:_ClearCoroutine(self.m_optionMarkHighlightThread)
            self.m_optionMarkHighlightThread = self:_StartCoroutine(function()
                coroutine.wait(OPTION_MARK_HIGHLIGHT_DELAY_TIME)
                mark:ToggleMarkHighlightState(isHighlight)
            end)
        else
            mark:ToggleMarkHighlightState(isHighlight)
        end
        self.view.bigSelectIcon.position = mark.rectTransform.position
    end

    if DeviceInfo.usingController then
        cell.keyHint.gameObject:SetActive(isHighlight)
    end
end






MapCtrl._InitBigRectHelper = HL.Method() << function(self)
    self.view.bigRectHelper:SetZoomRangeMax(self.view.levelMapController:GetControllerCurrentMaxScale())
    self.view.bigRectHelper:OverrideZoomRangeMin(self.view.levelMapController:GetControllerCurrentMinScale())
    self.view.bigRectHelper:Init()

    self.view.touchPanel.onZoom:AddListener(function(zoomVal)
        self:_RefreshZoomValue()
    end)

    self.view.bigRectHelper.onControllerFocusEnterSelectable:AddListener(function()
        self.m_controllerHoverMark = true
        self.view.controllerFocusAnim:PlayWithTween("map_focus_hit")
        self:_RefreshControllerClickBindingState()
        self:_RefreshControllerClickHoverText()
    end)
    self.view.bigRectHelper.onControllerFocusExitSelectable:AddListener(function()
        self.m_controllerHoverMark = false
        self.view.controllerFocusAnim:PlayWithTween("map_focus_default")
        self:_RefreshControllerClickBindingState()
        self:_RefreshControllerClickHoverText()
    end)
    self.view.bigRectHelper.onControllerZoom:AddListener(function(zoomVal)
        AudioAdapter.PostEvent("Au_UI_Slider_Common")
        self:_RefreshZoomValue()
    end)
end

MapCtrl._ResetBigRectHelper = HL.Method() << function(self)
    self.view.bigRectHelper.enabled = true
    self.view.bigRectHelper:SetZoomRangeMax(self.view.levelMapController:GetControllerCurrentMaxScale())
    self.view.bigRectHelper:OverrideZoomRangeMin(self.view.levelMapController:GetControllerCurrentMinScale())
    self.view.bigRectHelper:Init()
end

MapCtrl._BigRectFocusNodeOnInit = HL.Method(RectTransform, HL.Boolean, HL.Opt(HL.Function)) << function(self, focusNode, needTween, callback)
    if self.m_ignoreOpenFocus then
        return
    end
    if self.m_nodeOnInitFocused then
        return
    end
    self.view.bigRectHelper:FocusNode(focusNode, needTween, function()
        if callback ~= nil then
            callback()
        end
    end)
    self.m_nodeOnInitFocused = true
end






MapCtrl._InitZoomNode = HL.Method() << function(self)
    local zoomNode = self.view.zoomNode

    zoomNode.zoomSlider.onValueChanged:AddListener(function(value)
        self:_OnZoomValueChanged(value)
    end)

    zoomNode.addButton.onPressStart:AddListener(function()
        self:_StartTickChangZoomValue(true)
    end)
    zoomNode.reduceButton.onPressStart:AddListener(function()
        self:_StartTickChangZoomValue(false)
    end)
    zoomNode.addButton.onPressEnd:AddListener(function()
        self:_StopTickChangZoomValue(true)
    end)
    zoomNode.reduceButton.onPressEnd:AddListener(function()
        self:_StopTickChangZoomValue(false)
    end)
    self:_ResetZoomSliderValue(false)

    if not self.m_ignoreOpenFocus then
        local initZoomScaleRatio = 0
        if self:_IsWaitingInitShowMarkDetail() then
            initZoomScaleRatio = 1
            local mark = self.view.levelMapController:GetControllerMarkByInstId(self.m_waitInitShowDetailMarkInstId, true)
            if mark ~= nil then
                local _, targetTierId = mark:GetMarkTierState()
                if targetTierId > 0 then
                    self.m_waitAutoSwitchTierId = targetTierId
                end
            end
        else
            if self.m_initialLevelId == GameWorld.worldInfo.curLevelId then
                local playerNode = self.view.levelMapController.view.levelMapLoader.view.element.player
                if playerNode.gameObject.activeSelf then
                    local currTierId = GameWorld.mapRegionManager:GetCurrentCharInTierId()
                    if currTierId > 0 and not GameInstance.player.guide.isInGuide then

                        initZoomScaleRatio = DataManager.uiLevelMapConfig.tierZoomSliderPercent
                        self.m_waitAutoSwitchTierId = currTierId
                    end
                end
            end
        end

        if self.m_waitAutoSwitchTierId < 0 and self.m_waitRecoverTierId > 0 then
            self.m_waitAutoSwitchTierId = self.m_waitRecoverTierId
        end
        self.m_waitRecoverTierId = -1

        local zoomSlider = zoomNode.zoomSlider
        zoomSlider.value = zoomSlider.minValue + initZoomScaleRatio * (zoomSlider.maxValue - zoomSlider.minValue)
    end
end

MapCtrl._ResetZoomSliderValue = HL.Method(HL.Boolean) << function(self, useMaxValue)
    local zoomSlider = self.view.zoomNode.zoomSlider
    zoomSlider.minValue = self.view.levelMapController:GetControllerCurrentMinScale()
    zoomSlider.maxValue = self.view.levelMapController:GetControllerCurrentMaxScale()
    local initValue = useMaxValue and zoomSlider.maxValue or zoomSlider.minValue
    if zoomSlider.value == initValue then
        self:_OnZoomValueChanged(initValue)
    else
        zoomSlider.value = initValue
    end
    self:_RefreshZoomVisibleLayer(initValue, true)
end

MapCtrl._RefreshZoomValue = HL.Method() << function(self)

    local current = self.view.bigRectHelper:GetCurrentZoomValue()
    self.view.zoomNode.zoomSlider:SetValueWithoutNotify(current)
    self:_RefreshZoomButtonInteractableState()
    self:_RefreshZoomVisibleLayer(current)
end

MapCtrl._ChangeZoomValue = HL.Method(HL.Boolean, HL.Number) << function(self, isAdd, deltaPercent)
    if self.m_isResettingToPlayer then

        return
    end
    local zoomSlider = self.view.zoomNode.zoomSlider
    local current = zoomSlider.value
    local min, max = zoomSlider.minValue, zoomSlider.maxValue
    local changeValue = (max - min) * deltaPercent / 100.0
    local targetValue = isAdd and current + changeValue or current - changeValue
    targetValue = lume.clamp(targetValue, min, max)
    if targetValue == current then
        return
    end

    zoomSlider.value = targetValue
end

MapCtrl._StartTickChangZoomValue = HL.Method(HL.Boolean) << function(self, isAdd)
    if isAdd then
        self.m_addZoomTick = LuaUpdate:Remove(self.m_addZoomTick)
        self.m_addZoomTick = LuaUpdate:Add("Tick", function(deltaTime)
            self:_ChangeZoomValue(true, self.view.config.PRESS_DELTA)
        end)
    else
        self.m_reduceZoomTick = LuaUpdate:Remove(self.m_reduceZoomTick)
        self.m_reduceZoomTick = LuaUpdate:Add("Tick", function(deltaTime)
            self:_ChangeZoomValue(false, self.view.config.PRESS_DELTA)
        end)
    end
end

MapCtrl._StopTickChangZoomValue = HL.Method(HL.Boolean) << function(self, isAdd)
    if isAdd then
        self.m_addZoomTick = LuaUpdate:Remove(self.m_addZoomTick)
    else
        self.m_reduceZoomTick = LuaUpdate:Remove(self.m_reduceZoomTick)
    end
end

MapCtrl._OnZoomValueChanged = HL.Method(HL.Number) << function(self, value)
    self.view.bigRectHelper:ResetPivotPositionToScreenCenter()
    local isPlayAnimationIn = self:IsPlayingAnimationIn()
    self.view.bigRectHelper:SyncZoomValue(value, not self:_IsWaitingInitShowMarkDetail() and not isPlayAnimationIn)
    self:_RefreshZoomButtonInteractableState()
    self:_RefreshZoomVisibleLayer(value)
end

MapCtrl._RefreshZoomButtonInteractableState = HL.Method() << function(self)
    local zoomNode = self.view.zoomNode
    local zoomSlider = zoomNode.zoomSlider
    local value = zoomSlider.value
    local min, max = zoomSlider.minValue, zoomSlider.maxValue
    if value <= min then
        if zoomNode.reduceButton.interactable then
            zoomNode.reduceButton.interactable = false
            self:_StopTickChangZoomValue(false)
        end
    else
        if not zoomNode.reduceButton.interactable then
            zoomNode.reduceButton.interactable = true
        end
    end
    if value >= max then
        if zoomNode.addButton.interactable then
            zoomNode.addButton.interactable = false
            self:_StopTickChangZoomValue(true)
        end
    else
        if not zoomNode.addButton.interactable then
            zoomNode.addButton.interactable = true
        end
    end
end

MapCtrl._RefreshZoomVisibleLayer = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, scaleValue, forceRefresh)
    local layer = DataManager.uiLevelMapConfig:GetVisibleLayerByScale(scaleValue)
    if layer == self.m_zoomVisibleLayer and not forceRefresh then
        return
    end

    self.view.levelMapController:RefreshLoaderMarksVisibleStateByLayer(layer)
    self.m_zoomVisibleLayer = layer
end

MapCtrl._TryRecoverBigRectState = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    if args == nil or args.bigRectState == nil then
        return
    end
    self.view.bigRectHelper:RecoverContainerState(args.bigRectState)
    self:_RefreshZoomValue()
    args.bigRectState = nil
end






MapCtrl._InitBuildingAndCollectionHoverButton = HL.Method() << function(self)
    for _, buildingInfo in pairs(BUILDING_INFOS_CONFIG) do
        local viewNode = self.view.infoNode[buildingInfo.viewName]
        if viewNode ~= nil then
            viewNode.button.onHoverChange:AddListener(function(isHover)
                if isHover then
                    Notify(MessageConst.SHOW_COMMON_HOVER_TIP, {
                        mainText = Language[buildingInfo.hoverTextId],
                        delay = self.view.config.BOTTOM_TIP_HOVER_DELAY,
                    })
                else
                    Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
                end
            end)

            viewNode.button.onClick:AddListener(function()
                self:_OnInfoPopupBtnClick()
            end)
        end
    end

    for _, collectionInfo in pairs(COLLECTIONS_CONFIG) do
        local viewNode = self.view.infoNode[collectionInfo.viewName]
        if viewNode ~= nil then
            viewNode.button.onHoverChange:RemoveAllListeners()
            viewNode.button.onHoverChange:AddListener(function(isHover)
                if isHover then
                    Notify(MessageConst.SHOW_COMMON_HOVER_TIP, {
                        mainText = Language[collectionInfo.hoverTextId],
                        delay = self.view.config.BOTTOM_TIP_HOVER_DELAY,
                    })
                else
                    Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
                end
            end)

            viewNode.button.onClick:RemoveAllListeners()
            viewNode.button.onClick:AddListener(function()
                self:_OnInfoPopupBtnClick()
            end)
        end
    end

    self.view.infoNode.infoPopupBtn.onClick:AddListener(function()
        self:_OnInfoPopupBtnClick()
    end)
end

MapCtrl._OnInfoPopupBtnClick = HL.Method() << function(self)
    UIManager:Open(PanelId.MapInfoPopup, self:GetPopupInfoState())
end

MapCtrl._RefreshBuildingInfos = HL.Method(HL.String) << function(self, levelId)
    self.m_buildingInfo = {}

    local sceneInfo = GameInstance.remoteFactoryManager.system.core:GetSceneInfoByName(levelId)
    for buildingCfgId, buildingInfo in pairs(BUILDING_INFOS_CONFIG) do
        local viewNode = self.view.infoNode[buildingInfo.viewName]
        if viewNode ~= nil then
            local curr, total = 0, 0
            if sceneInfo ~= nil then
                curr, total = buildingInfo.getter(sceneInfo)
            end

            local viewNodeData = {}
            viewNodeData.total = total
            viewNodeData.curr = curr
            self.m_buildingInfo[buildingCfgId] = viewNodeData

            MapUtils.updateMapInfoViewNode(viewNode, viewNodeData, true)
        end
    end
end

MapCtrl._RefreshCollectionsInfo = HL.Method(HL.String) << function(self, levelId)
    self.m_collectionInfo = {}

    local collectionManager = GameInstance.player.collectionManager
    for collectionCfgId, collectionInfo in pairs(COLLECTIONS_CONFIG) do
        local viewNode = self.view.infoNode[collectionInfo.viewName]
        if viewNode ~= nil then
            local total, curr = collectionManager:GetMergeItemCnt(collectionInfo.mergeId, levelId)
            local viewNodeData = {}
            viewNodeData.total = total
            viewNodeData.curr = curr
            self.m_collectionInfo[collectionCfgId] = viewNodeData

            MapUtils.updateMapInfoViewNode(viewNode, viewNodeData, false)
        end
    end
end






MapCtrl._InitRegionMapButton = HL.Method() << function(self)
    self.view.regionMapBtn.onClick:AddListener(function()
        MapUtils.switchFromLevelMapToRegionMap(self.m_currLevelId)
    end)
end






MapCtrl._RefreshTrackingInfo = HL.Method(HL.String) << function(self, levelId)
    self.view.mapTrackingInfo:InitMapTrackingInfo({ levelId = levelId })
end

MapCtrl._OnTrackingMapMarkChanged = HL.Method(HL.Any) << function(self)
    if not self.m_isMarkDetailShowing then
        return
    end
    Notify(MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL)
end

MapCtrl._OnTrackingMarkClicked = HL.Method(HL.String, HL.Any, HL.Any) << function(self, instId, trackingMark, relatedMark)
    if trackingMark.view.levelMapLimitInRect.isLimitedInRect then
        local levelId = GameInstance.player.mapManager:GetMarkInstRuntimeDataLevelId(instId)
        if levelId == self.m_currLevelId and relatedMark ~= nil then
            self.view.bigRectHelper:FocusNode(relatedMark.rectTransform, true, function()
                self:_ShowMarkDetail(instId)
                self:_StopCheckSwitchTierOnFocus()
            end)
            self:_StartCheckSwitchTierOnFocus(instId)
        else
            self:ResetMapStateToTargetLevel({ instId = instId, levelId = levelId })
        end
    else
        self:_OnLevelMapMarkClicked(instId)
    end
end







MapCtrl._InitPlayerIcon = HL.Method() << function(self)
    local playerNode = self.view.levelMapController.view.levelMapLoader.view.element.player

    if self.m_initialLevelId == GameWorld.worldInfo.curLevelId then
        self:_StartPlayerIconLimit()
        if playerNode.gameObject.activeSelf then
            self:_BigRectFocusNodeOnInit(playerNode.rectTransform, false)
        end
    end

    playerNode.playerBtn.onClick:AddListener(function()
        if playerNode.levelMapLimitInRect.isLimitedInRect then
            self.view.bigRectHelper:FocusNode(playerNode.rectTransform, true)
        end
    end)
end

MapCtrl._RefreshPlayerIconNeedLimit = HL.Method() << function(self)
    if self.m_currLevelId == GameWorld.worldInfo.curLevelId then
        self:_StartPlayerIconLimit()
    else
        self:_StopPlayerIconLimit()
    end
end

MapCtrl._StartPlayerIconLimit = HL.Method() << function(self)
    local playerNode = self.view.levelMapController.view.levelMapLoader.view.element.player
    playerNode.levelMapLimitInRect:StartLimitMarkInRect()
end

MapCtrl._StopPlayerIconLimit = HL.Method() << function(self)
    local playerNode = self.view.levelMapController.view.levelMapLoader.view.element.player
    playerNode.levelMapLimitInRect:StopLimitMarkInRect()
end







MapCtrl._RefreshSpaceshipNode = HL.Method(HL.String) << function(self, levelId)
    self.view.mapSpaceshipNode:InitMapSpaceshipNode({ levelId = levelId })
end






MapCtrl._RefreshSpaceshipLevelNode = HL.Method(HL.String) << function(self, levelId)
    local inSpaceship = levelId == Tables.spaceshipConst.baseSceneName or GameInstance.player.spaceship.isViewingFriend
    self.view.lvDotNode.gameObject:SetActive(inSpaceship)
    if inSpaceship then
        local _, roomInfo = GameInstance.player.spaceship:TryGetRoom(Tables.spaceshipConst.controlCenterRoomId)
        local roomTypeData = Tables.spaceshipRoomTypeTable[roomInfo.roomType]
        self.view.lvDotNode:InitLvDotNode(roomInfo.lv, roomInfo.maxLv, UIUtils.getColorByString(roomTypeData.color))
    end
end






MapCtrl._InitCloseButton = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        self:_CloseMap()
    end)

    self:BindInputPlayerAction("map_close", function()
        self:_CloseMap()
    end)
end

MapCtrl._CloseMap = HL.Method() << function(self)
    Notify(MessageConst.ON_CLICK_MAP_CLOSE_BTN)
end






MapCtrl._InitFilterButton = HL.Method() << function(self)
    self.view.filterBtn.button.onClick:AddListener(function()
        self:_OnFilterBtnClick()
    end)
    self:_RefreshFilterBtnState()
end

MapCtrl._OnFilterBtnClick = HL.Method() << function(self)
    self:_ToggleControllerMoveAndZoom(false)
    Notify(MessageConst.SHOW_LEVEL_MAP_FILTER, {
        onCloseCallback = function()
            self:_ToggleControllerMoveAndZoom(true)
        end
    })
end

MapCtrl._RefreshFilterBtnState = HL.Method() << function(self)
    local isFilterValid = GameInstance.player.mapManager:IsFilterValid()
    self.view.filterBtn.existNode.gameObject:SetActive(isFilterValid)
    self.view.filterBtn.normalNode.gameObject:SetActive(not isFilterValid)
end

MapCtrl._ResetFilterState = HL.Method() << function(self)
    local useServerFilterState = GameInstance.player.mapManager.useServerFilterState

    if not InputManagerInst.inChangingInputDevice and not useServerFilterState then
        GameInstance.player.mapManager:ResetFilterState()
    end
end






MapCtrl._InitWalletBar = HL.Method() << function(self)
    self.view.walletBarPlaceholder:InitWalletBarPlaceholder(UIConst.REGION_MAP_STAMINA_IDS)
    self:_RefreshWalletNodeVisibleState()
end

MapCtrl._OnSystemUnlock = HL.Method(HL.Table) << function(self, args)
    local systemIndex = unpack(args)
    if systemIndex == GEnums.UnlockSystemType.Dungeon:GetHashCode() then
        self:_RefreshWalletNodeVisibleState()
    end
end

MapCtrl._RefreshWalletNodeVisibleState = HL.Method() << function(self)
    self.view.walletBarPlaceholder.gameObject:SetActive(Utils.isSystemUnlocked(GEnums.UnlockSystemType.Dungeon))
end






MapCtrl._InitTierSwitcherNode = HL.Method() << function(self)
    self.m_currTierIdList = {}
    self.m_trySwitchTierOnFocusMark = nil
    self.view.tierFocusNode.gameObject:SetActive(not DeviceInfo.usingController)
    self:_RefreshTierFocusNode()

    self.m_tierSwitcherCells = UIUtils.genCellCache(self.view.tierSwitcherNode.tierSwitcherCell)
    self:_RefreshTierSwitcherNode(false, true)

    self.m_tierCheckTick = LuaUpdate:Add("LateTick", function(deltaTime)
        self:_CheckNeedShowTierSwitcherList()

        if self.m_trySwitchTierOnFocusMark ~= nil then
            if self:_TrySwitchTierOnFocus(self.m_trySwitchTierOnFocusMark) then
                self:_StopCheckSwitchTierOnFocus()
            end
        end
    end)
end

MapCtrl._CheckNeedShowTierSwitcherList = HL.Method() << function(self)
    local screenPos
    if DeviceInfo.usingController then
        screenPos = Unity.RectTransformUtility.WorldToScreenPoint(self.uiCamera, self.view.focusArrowNode.position)
    else
        screenPos = Vector2(Screen.width / 2, Screen.height / 2)
    end
    local rectPos = UIUtils.screenPointToUI(
        screenPos,
        self.uiCamera,
        self.view.levelMapController.view.levelMapLoader.view.rectTransform
    )
    local worldPos = self.view.levelMapController.view.levelMapLoader:GetWorldPositionByRectPosition(rectPos)
    local isDirty = false
    local success, regionId = GameWorld.mapRegionManager:CheckPosIsInTierContainerMap(
        self.m_currMapId,
        Vector2(worldPos.x, worldPos.z),
        TIER_CHECK_TOLERANCE_RADIUS
    )
    if success and GameWorld.mapRegionManager:GetRegionLevelId(regionId) ~= self.m_currLevelId then
        success = false
    end
    if success and not GameWorld.mapRegionManager:CheckRegionHideInMist(regionId) then
        if self.m_currTierContainerId ~= regionId then
            self.m_currTierContainerId = regionId
            isDirty = true
        end
    else
        if self.m_currTierContainerId ~= MapConst.BASE_TIER_CONTAINER_ID then
            self.m_currTierContainerId = MapConst.BASE_TIER_CONTAINER_ID
            isDirty = true
        end
    end

    if isDirty then
        self:_RefreshTierInfo()
    end
end

MapCtrl._RefreshTierInfo = HL.Method() << function(self)
    if self.m_currTierContainerId == MapConst.BASE_TIER_CONTAINER_ID then
        self.m_currTierIdList = {}
        self:_RefreshTierSwitcherNode(false)
        return
    end


    local currTierIdSortList = {}
    local tierIdList = GameWorld.mapRegionManager:GetTierListInContainer(self.m_currTierContainerId)
    if tierIdList ~= nil then
        for index = 0, tierIdList.Count - 1 do
            local tierId = tierIdList[index]
            local tierIndex = GameWorld.mapRegionManager:GetTierIndex(tierId)

            if tierIndex == 0 or not GameWorld.mapRegionManager:CheckRegionHideInMist(tierId) then
                table.insert(currTierIdSortList, {
                    sortIndex = tierIndex,
                    tierId = tierId,
                })
            end
        end
    end
    if #currTierIdSortList <= 1 then
        self:_RefreshTierSwitcherNode(false)
        return
    end

    table.sort(currTierIdSortList, Utils.genSortFunction({ "sortIndex" }, false))
    local currTierIdList = {}
    for index, tierSortData in ipairs(currTierIdSortList) do
        currTierIdList[index] = tierSortData.tierId
    end
    self.m_currTierIdList = currTierIdList

    local forceRefresh = self.m_trySwitchTierOnFocusMark ~= nil
    self:_RefreshTierSwitcherNode(true, forceRefresh)
end

MapCtrl._RefreshTierSwitcherNode = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, needShow, forceRefresh)
    local isShownStateDirty = self.m_tierSwitchersShowing ~= needShow
    local isPlayAnimationIn = self:IsPlayingAnimationIn()

    self.m_tierSwitchersShowing = needShow
    if forceRefresh or isPlayAnimationIn or not isShownStateDirty then
        self:_OnRefreshTierSwitcherNode(needShow)
    else
        self.view.tierSwitcherNode.nodeAnim:ClearTween(false)
        self.view.tierSwitcherNode.nodeAnim:PlayWithTween("map_tierswitcher_out", function()
            self:_OnRefreshTierSwitcherNode(self.m_tierSwitchersShowing)
            self.view.tierSwitcherNode.nodeAnim:PlayWithTween("map_tierswitcher_in")
        end)
    end

    if isShownStateDirty then
        local audioKey = needShow and "Au_UI_Toast_LayeredMap_Open" or "Au_UI_Toast_LayeredMap_Close"
        AudioAdapter.PostEvent(audioKey)
    end

    self:_RefreshTierFocusNode()
end

MapCtrl._OnRefreshTierSwitcherNode = HL.Method(HL.Boolean) << function(self, needShow)
    self:_RefreshTierSwitcherCells()
    InputManagerInst:ToggleGroup(self.m_controllerTierBindingGroup, needShow)
    self.view.tierSwitcherNode.topNode.gameObject:SetActive(needShow)
    self.view.tierSwitcherNode.bottomNode.gameObject:SetActive(needShow)
end

MapCtrl._RefreshTierSwitcherCells = HL.Method() << function(self)
    if self.m_tierSwitchersShowing then
        local _, levelConfig = GameInstance.player.mapManager:GetLoaderLevelDataByLevelId(self.m_currLevelId)
        self.m_currTierIndex = 0


        local currTierId = GameWorld.mapRegionManager:GetCurrentCharInTierId()
        local currContainerId = GameWorld.mapRegionManager:GetCurrentCharInTierContainerId()
        local currTierInContainer = currContainerId == self.m_currTierContainerId
        for _, tierId in pairs(self.m_currTierIdList) do
            if tierId == currTierId then
                currTierInContainer = true
                break
            end
        end

        local waitAutoSwitchTierInContainer = false
        for _, tierId in pairs(self.m_currTierIdList) do
            if tierId == self.m_waitAutoSwitchTierId then
                waitAutoSwitchTierInContainer = true
                break
            end
        end

        self.view.levelMapController:SetLoaderTierStateWithNeedShowMarkTier(true)
        local firstSelectIndex = 0
        self.m_tierSwitcherCells:Refresh(#self.m_currTierIdList, function(cell, index)
            local tierId = self.m_currTierIdList[index]
            local tierIndex = GameWorld.mapRegionManager:GetTierIndex(tierId)
            local isBase = tierIndex == MapConst.BASE_TIER_INDEX
            cell.baseStateController:SetState(isBase and "Base" or "Tier")
            local isSelected
            local isCurrTier
            if currTierId > 0 then
                isCurrTier = currTierId == tierId
            else
                isCurrTier = isBase
            end
            if self.m_waitAutoSwitchTierId > 0 and waitAutoSwitchTierInContainer then

                isSelected = self.m_waitAutoSwitchTierId == tierId
            else
                isSelected = isBase
            end
            cell.selectAnim.gameObject:SetActive(isSelected)
            cell.currentStateController:SetState((isCurrTier and currTierInContainer) and "Current" or "Other")

            cell.activeStateController:SetState("Active")
            cell.selectBtn.onClick:RemoveAllListeners()
            cell.selectBtn.onClick:AddListener(function()
                self:_OnSelectTier(index)
            end)
            cell.selectBtn.onHoverChange:RemoveAllListeners()
            cell.selectBtn.onHoverChange:AddListener(function(isHover)
                if index == self.m_currTierIndex then
                    return
                end
                cell.tierNameTxt.gameObject:SetActive(isHover)
            end)
            cell.selectBtn.interactable = not isSelected

            local nameSuccess, nameTextId = levelConfig.loadConfig.tierNames:TryGetValue(tierId)
            cell.tierNameTxt.gameObject:SetActive(false)
            if isBase then
                cell.tierNameTxt.text = Tables.levelDescTable[self.m_currLevelId].showName
            else
                if nameSuccess then
                    cell.tierNameTxt.text = Language[nameTextId]
                end
            end

            if isSelected then
                firstSelectIndex = index
            end
        end)

        self:_OnSelectTier(firstSelectIndex)

        self.m_waitAutoSwitchTierId = -1
    else
        self.view.levelMapController:SetLoaderTierStateWithNeedShowMarkTier(false)


        self.m_tierSwitcherCells:Refresh(1, function(cell, index)
            cell.baseStateController:SetState("Base")
            cell.activeStateController:SetState("Inactive")
            cell.currentStateController:SetState("Other")
            cell.selectAnim.gameObject:SetActive(false)
            cell.selectBtn.onHoverChange:RemoveAllListeners()
            cell.selectBtn.interactable = false
            cell.tierNameTxt.gameObject:SetActive(false)
        end)

        self.view.levelMapController:SetLoaderTierStateWithTierId(MapConst.BASE_TIER_ID)
    end
end

MapCtrl._RefreshTierFocusNode = HL.Method() << function(self)
    if DeviceInfo.usingController then
        return
    end
    self.view.tierFocusNode.focusImg.color = self.m_tierSwitchersShowing and Color.white or self.view.config.TIER_FOCUS_NORMAL_COLOR
end

MapCtrl._SelectTierByTierId = HL.Method(HL.Number).Return(HL.Boolean) << function(self, targetTierId)
    if not next(self.m_currTierIdList) then
        return false
    end

    for index, tierId in ipairs(self.m_currTierIdList) do
        if targetTierId == tierId then
            self:_OnSelectTier(index)
            return true
        end
    end

    local targetTierIndex = GameWorld.mapRegionManager:GetTierIndex(targetTierId)


    if targetTierIndex == MapConst.BASE_TIER_INDEX then
        for index, tierId in ipairs(self.m_currTierIdList) do
            local tierIndex = GameWorld.mapRegionManager:GetTierIndex(tierId)
            if tierIndex == targetTierIndex then
                self:_OnSelectTier(index)
                return true
            end
        end
    end

    return false
end

MapCtrl._OnSelectTier = HL.Method(HL.Number) << function(self, index)
    if self.m_currTierIndex > 0 then
        local lastCell = self.m_tierSwitcherCells:GetItem(self.m_currTierIndex)
        if lastCell ~= nil then
            UIUtils.PlayAnimationAndToggleActive(lastCell.selectAnim, false)
            lastCell.selectBtn.interactable = true
            lastCell.tierNameTxt.gameObject:SetActive(false)
        end
    end

    local currCell = self.m_tierSwitcherCells:GetItem(index)
    if currCell ~= nil then
        UIUtils.PlayAnimationAndToggleActive(currCell.selectAnim, true)
        currCell.selectBtn.interactable = false
        currCell.tierNameTxt.gameObject:SetActive(true)
    end
    self.m_currTierIndex = index

    self:_SwitchToTargetTier(self.m_currTierIdList[index])
end

MapCtrl._SwitchToTargetTier = HL.Method(HL.Number) << function(self, tierId)
    self.view.levelMapController:SetLoaderTierStateWithTierId(tierId, true)
end

MapCtrl._StartCheckSwitchTierOnFocus = HL.Method(HL.String) << function(self, markInstId)
    self:_StopCheckSwitchTierOnFocus()
    self.m_trySwitchTierOnFocusMark = self.view.levelMapController:GetControllerMarkByInstId(markInstId, true)
end

MapCtrl._StopCheckSwitchTierOnFocus = HL.Method() << function(self)
    self.m_trySwitchTierOnFocusMark = nil
end

MapCtrl._TrySwitchTierOnFocus = HL.Method(HL.Any).Return(HL.Boolean) << function(self, mark)
    if mark == nil then
        return false
    end

    local _, targetTierId = mark:GetMarkTierState()
    return self:_SelectTierByTierId(targetTierId)
end






MapCtrl._CheckAndRefreshNeedPlayMistUnlockedAnimationState = HL.Method() << function(self)
    self:_RefreshNeedPlayMistUnlockedAnimationState(self.view.levelMapController:NeedPlayMistsUnlockedAnimation())
end

MapCtrl._RefreshNeedPlayMistUnlockedAnimationState = HL.Method(HL.Boolean) << function(self, needPlay)
    self.m_needPlayMistsAnimation = needPlay
    self.view.blockMask.gameObject:SetActive(needPlay)
    InputManagerInst:ToggleGroup(self.view.mainInputGroup.groupId, not needPlay)
end

MapCtrl._TryPlayMistUnlockedAnimation = HL.Method() << function(self)
    if not self.m_needPlayMistsAnimation then
        return
    end
    self.view.levelMapController:TryPlayMistUnlockedAnimation(function()
        self:_RefreshNeedPlayMistUnlockedAnimationState(false)
    end)
    AudioAdapter.PostEvent("Au_UI_Toast_MapMistDissipate")
end






MapCtrl._TryPlayMapMaskAnimation = HL.Method() << function(self)
    if self:_IsWaitingInitShowMarkDetail() then
        return
    end
    self.view.mapMaskAnimWrapper:PlayWithTween("map_masklevelmapcontroller_in")
end

MapCtrl._PlayRightZoomNodeAnimation = HL.Method(HL.Boolean) << function(self, isIn)
    local animName = isIn and "map_mainui_rightzoom_in" or "map_mainui_rightzoom_out"
    self.view.rightAnim:PlayWithTween(animName)
end

MapCtrl._PlayRightSpaceshipNodeAnimation = HL.Method(HL.Boolean) << function(self, isIn)
    local animName = isIn and "map_mainui_rightssnode_in" or "map_mainui_rightssnode_out"
    self.view.rightAnim:PlayWithTween(animName)
end

MapCtrl._PlayTopNodeAnimation = HL.Method(HL.Boolean) << function(self, isIn)
    local animName = isIn and "map_mainui_topnode_in" or "map_mainui_topnode_out"
    self.view.topAnim:ClearTween()
    self.view.topAnim:PlayWithTween(animName, function()
        self.view.topAnim.gameObject:SetActive(isIn)
    end)
end

MapCtrl._PlayLeftNodeAnimation = HL.Method(HL.Boolean) << function(self, isIn)
    local animName = isIn and "map_trackinginfo_in" or "map_trackinginfo_out"
    self.view.leftAnim:PlayWithTween(animName)
end

MapCtrl._PlayRightNodeAnimation = HL.Method(HL.Boolean) << function(self, isIn)
    local animName = isIn and "map_mainui_rightssnode_in" or "map_mainui_rightssnode_out"
    self.view.topAnim:PlayWithTween(animName)
end

MapCtrl._PlayMapResetAnimation = HL.Method(HL.Boolean, HL.Opt(HL.Function)) << function(self, isIn, callback)
    local animName = isIn and "map_mask_switch_in" or "map_mask_switch_out"
    self.view.mapMaskAnimWrapper:PlayWithTween(animName, callback)
end

MapCtrl._PlayTrackingNodeAnimation = HL.Method(HL.Boolean, HL.Opt(HL.Function)) << function(self, isIn, callback)
    local animName = isIn and "map_trackinginfo_in" or "map_trackinginfo_out"
    self.view.leftAnim:PlayWithTween(animName, callback)
end

MapCtrl._PlayFilterBtnAnimation = HL.Method(HL.Boolean, HL.Opt(HL.Function)) << function(self, isIn, callback)
    local animName = isIn and "map_mainui_filterbtn_in" or "map_mainui_filterbtn_out"
    self.view.leftAnim:PlayWithTween(animName, callback)
end

MapCtrl._PlayAndSetMainNodeVisibleState = HL.Method(HL.Boolean, HL.Opt(HL.Function)) << function(self, isIn, onComplete)
    UIUtils.PlayAnimationAndToggleActive(self.view.topAnim, isIn, function()
        if onComplete ~= nil then
            onComplete(isIn)
        end
    end)
    UIUtils.PlayAnimationAndToggleActive(self.view.bottomAnim, isIn)
    UIUtils.PlayAnimationAndToggleActive(self.view.leftAnim, isIn)
    UIUtils.PlayAnimationAndToggleActive(self.view.rightAnim, isIn)

    if isIn then

        self.view.walletBarPlaceholder.gameObject:SetActive(true)
    else
        self.view.walletBarPlaceholder.gameObject:SetActive(false)
    end
end






MapCtrl._ForceSetMapStateToTargetLevel = HL.Method(HL.Table) << function(self, args)

    if self.m_isMarkDetailShowing then
        Notify(MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL)
    end

    local levelId = unpack(args)
    if self.m_currLevelId ~= levelId then
        self.view.levelMapController:ResetSwitchModeToTargetLevelState(levelId)
        self:_ResetBigRectHelper()
        self:_RefreshLevelMapContent()
    end
    self:_ResetZoomSliderValue(false)
end

MapCtrl.GetBigRectRecoverStateArg = HL.Method().Return(HL.Any) << function(self)
    return self.view.bigRectHelper:GetRecoverState()
end

MapCtrl.GetTierRecoverId = HL.Method().Return(HL.Number) << function(self)
    if self.m_currTierIdList == nil or next(self.m_currTierIdList) == nil then
        return -1
    end
    return self.m_currTierIdList[self.m_currTierIndex]
end

MapCtrl.GetPopupInfoState = HL.Method().Return(HL.Table) << function(self)
    return {
        self.m_buildingInfo,
        self.m_collectionInfo
    }
end

MapCtrl.GetCurrState = HL.Method().Return(HL.Table) << function(self)
    return {
        currLevelId = self.m_currLevelId,
        currMapId = self.m_currMapId,
        selectedMarkInstId = self.m_selectMarkInstId,
    }
end

MapCtrl.OpenFilterPanel = HL.Method() << function(self)
    self:_OnFilterBtnClick()
end

MapCtrl.ResetMapStateToTargetLevel = HL.Method(HL.Table) << function(self, args)
    if self.m_isResettingToTargetLevel then
        return
    end

    local instId, levelId = args.instId, args.levelId
    local needShowDetail = not string.isEmpty(instId)
    if needShowDetail then
        levelId = GameInstance.player.mapManager:GetMarkInstRuntimeDataLevelId(instId)
        self.m_waitInitShowDetailMarkInstId = instId
    end

    if self.m_isMarkDetailShowing then
        Notify(MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL)
    end

    self.view.bigRectHelper:ClearAllTween()
    self.view.bigRectHelper.enabled = false
    self.view.touchPanel.enabled = false
    self.view.fullScreenMask.gameObject:SetActive(true)

    self:_PlayAndSetMainNodeVisibleState(false)

    self:_ToggleControllerMoveAndZoom(false)

    self.m_isResettingToTargetLevel = true
    self:_PlayMapResetAnimation(false, function()
        self.view.fullScreenMask.gameObject:SetActive(false)
        self.view.levelMapController:ResetSwitchModeToTargetLevelState(levelId)
        self:_ResetBigRectHelper()
        self.view.touchPanel.enabled = true
        self:_RefreshLevelMapContent()
        self:_ResetZoomSliderValue(needShowDetail)

        if not string.isEmpty(self.m_waitInitShowDetailMarkInstId) then
            self:_ShowMarkDetail(self.m_waitInitShowDetailMarkInstId, true)
            self:_StartCheckSwitchTierOnFocus(self.m_waitInitShowDetailMarkInstId)
            self.m_waitInitShowDetailMarkInstId = ""
        end

        self:_PlayMapResetAnimation(true)
        self:_PlayAndSetMainNodeVisibleState(true, function(isIn)
            self:_StopCheckSwitchTierOnFocus()
            self.m_isResettingToTargetLevel = false
        end)

        self:_ToggleControllerMoveAndZoom(not needShowDetail)

        if args.onComplete then
            args.onComplete()
        end

        local configSuccess, levelConfig = GameInstance.player.mapManager:GetLoaderLevelBasicInfoByLevelId(levelId)
        if configSuccess then
            local audioKey = levelConfig.isSingleLevel and "Au_UI_Menu_MapPanel_Open" or "Au_UI_Menu_MapPanel_Close"
            AudioAdapter.PostEvent(audioKey)
        end

        self:_CheckAndRefreshNeedPlayMistUnlockedAnimationState()
        self:_TryPlayMistUnlockedAnimation()
    end)
end






MapCtrl._InitMapController = HL.Method() << function(self)

    local optionGroup = self.view.selectOptionNode.inputGroup
    self:BindInputPlayerAction("map_select_option_before", function()
        self:_OnControllerSelectOption(false)
    end, optionGroup.groupId)
    self:BindInputPlayerAction("map_select_option_next", function()
        self:_OnControllerSelectOption(true)
    end, optionGroup.groupId)
    self:BindInputPlayerAction("map_select_option_click", function()
        AudioAdapter.PostEvent("Au_UI_Button_Common")
        self:_OnSelectOptionClick(self.m_currHighlightOption)
    end, optionGroup.groupId)


    local openInfoBinding = self:BindInputPlayerAction("map_open_collection_info", function()
        self:_OnInfoPopupBtnClick()
    end, self.view.infoNode.inputGroup.groupId)
    self.view.infoNode.keyHint:SetBindingId(openInfoBinding)


    self:BindInputPlayerAction("map_reset_to_player", function()
        self:_ControllerResetToPlayer()
    end)
    self.view.controllerFocusAnim:PlayWithTween("map_focus_default")


    self.m_controllerTierBindingGroup = InputManagerInst:CreateGroup(self.view.mainInputGroup.groupId)
    local tierPrevBindingId = self:BindInputPlayerAction("map_switch_tier_prev", function()
        self:_OnControllerSelectTier(false)
    end, self.m_controllerTierBindingGroup)
    local tierNextBindingId = self:BindInputPlayerAction("map_switch_tier_next", function()
        self:_OnControllerSelectTier(true)
    end, self.m_controllerTierBindingGroup)
    self.view.tierSwitcherNode.prevKeyHint:SetBindingId(tierPrevBindingId)
    self.view.tierSwitcherNode.nextKeyHint:SetBindingId(tierNextBindingId)


    self.view.mapTrackingInfo.view.listNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        self:_ToggleControllerMoveAndZoom(not isFocused)
    end)
    self.view.mapTrackingInfo.view.listNaviGroup.focusPanelSortingOrder = self:GetSortingOrder() + MAP_BLOCK_ORDER_OFFSET

    self:_RefreshControllerClickHoverText()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end






MapCtrl._ControllerResetToPlayer = HL.Method() << function(self)
    if self.m_isResettingToPlayer then
        return
    end
    self.m_isResettingToPlayer = true

    local playerNode = self.view.levelMapController.view.levelMapLoader.view.element.player


    local function freezeInput()
        self:_StopTickChangZoomValue(true)
        self:_StopTickChangZoomValue(false)
        self.view.bigRectHelper:ClearAllTween()
        self.view.bigRectHelper.controllerMoveEnabled = false
        self.view.bigRectHelper.controllerZoomEnabled = false
    end


    local function unfreezeAndFinish()
        self.view.bigRectHelper.controllerMoveEnabled = true
        self.view.bigRectHelper.controllerZoomEnabled = true
        self.m_isResettingToPlayer = false
    end


    local function onFocusComplete()
        UIUtils.PlayAnimationAndToggleActive(self.view.controllerFocusAnim, true)
        self.view.focusArrowNode.position = playerNode.rectTransform.position
        unfreezeAndFinish()
    end

    freezeInput()

    UIUtils.PlayAnimationAndToggleActive(self.view.controllerFocusAnim, false, function()
        if GameWorld.worldInfo.curLevelId == self.m_currLevelId then

            self.view.bigRectHelper:FocusNode(playerNode.rectTransform, true, onFocusComplete)
        else

            self:ResetMapStateToTargetLevel({
                levelId = GameWorld.worldInfo.curLevelId,
                onComplete = function()
                    self.view.bigRectHelper:FocusNode(playerNode.rectTransform, false)
                    onFocusComplete()
                end
            })
        end
    end)
end

MapCtrl._OnControllerSelectOption = HL.Method(HL.Boolean) << function(self, next)
    local nextIndex = next and self.m_currHighlightOption + 1 or self.m_currHighlightOption - 1
    nextIndex = lume.clamp(nextIndex, 1, self.m_selectOptionCells:GetCount())
    if nextIndex == self.m_currHighlightOption then
        return
    end
    local lastCell = self.m_selectOptionCells:GetItem(self.m_currHighlightOption)
    local cell = self.m_selectOptionCells:GetItem(nextIndex)
    self:_RefreshSelectOptionHighlightState(lastCell, self.m_currHighlightOption, false)
    self:_RefreshSelectOptionHighlightState(cell, nextIndex, true)
    self.m_currHighlightOption = nextIndex
    self.view.selectOptionNode.selectOptionList:AutoScrollToRectTransform(cell.rectTransform)
    AudioAdapter.PostEvent("Au_UI_Hover_ControllerSelect")
end

MapCtrl._ToggleControllerMoveAndZoom = HL.Method(HL.Boolean) << function(self, enabled)
    if not DeviceInfo.usingController then
        return
    end
    self.view.controllerFocusAnim.gameObject:SetActive(enabled)
    self.view.bigRectHelper.controllerMoveEnabled = enabled
    self.view.bigRectHelper.controllerZoomEnabled = enabled
    self:_RefreshControllerClickBindingState()
end

MapCtrl._RefreshControllerClickBindingState = HL.Method() << function(self)
    InputManagerInst:ToggleBinding(self.view.bigRectHelper.clickBindingId, self.view.bigRectHelper.controllerMoveEnabled)
end

MapCtrl._OnControllerSelectTier = HL.Method(HL.Boolean) << function(self, next)
    local index = next and self.m_currTierIndex + 1 or self.m_currTierIndex - 1
    index = lume.clamp(index, 1, #self.m_currTierIdList)
    self:_OnSelectTier(index)
    AudioAdapter.PostEvent("Au_UI_Toggle_MapLayerSelect")
end

MapCtrl._OnControllerCustomMarkMultiDeleteStateChange = HL.Method(HL.Boolean) << function(self, isShow)
    self:_RefreshControllerClickBindingState()
    local bindingText = isShow and Language.LUA_MAP_CUSTOM_MARK_MULTI_SELECT_CANCEL or Language.LUA_MAP_CUSTOM_MARK_ADD
    InputManagerInst:SetBindingText(self.view.bigRectHelper.clickBindingId, bindingText)


    local _, multiController = UIManager:IsOpen(PanelId.MapCustomMarkDelete)
    if isShow then
        InputManagerInst:ChangeParent(false, self.view.bigRectHelper.clickBindingId, multiController.view.inputGroup.groupId)
    else
        InputManagerInst:ChangeParent(false, self.view.bigRectHelper.clickBindingId, self.view.inputGroup.groupId)
    end
end

MapCtrl._RefreshControllerClickHoverText = HL.Method() << function(self)
    if self.m_multiDeletePanelShow then
        return
    end
    local bindingText = (not self.m_controllerHoverMark and not self.m_noCustomMark) and Language.LUA_MAP_CUSTOM_MARK_ADD or ""
    InputManagerInst:SetBindingText(self.view.bigRectHelper.clickBindingId, bindingText)
end

MapCtrl._OnControllerMarkHover = HL.Method(HL.String, HL.Boolean) << function(self, markInstId, isHover)
    if self.m_multiDeletePanelShow then
        local isOpen, multiController = UIManager:IsOpen(PanelId.MapCustomMarkDelete)
        if isOpen then
            local isSelected = multiController:IsCustomMarkSelectedToDelete(markInstId)
            local bindingText = Language.LUA_MAP_CUSTOM_MARK_MULTI_SELECT
            if isHover and isSelected then
                bindingText = Language.LUA_MAP_CUSTOM_MARK_MULTI_SELECT_CANCEL
            end
            InputManagerInst:SetBindingText(self.view.bigRectHelper.clickBindingId, bindingText)
        end
    end
end






if BEYOND_DEBUG_COMMAND or BEYOND_DEBUG then
    MapCtrl.m_isDebugMode = HL.Field(HL.Boolean) << false

    MapCtrl._InitDebugMode = HL.Method() << function(self)
        self.view.levelMapController:InitLevelMapController(MapConst.LEVEL_MAP_CONTROLLER_MODE.DEBUG)
        self.view.infoNode.gameObject:SetActive(false)
        self.view.filterBtn.gameObject:SetActive(false)
        self.view.zoomNode.gameObject:SetActive(false)

        self.view.bigRectHelper:SetZoomRangeMax(2)
        self.view.bigRectHelper:OverrideZoomRangeMin(0.25)
        self.view.bigRectHelper:SyncZoomValue(0.25, false)
        self.view.bigRectHelper:Init()
        self.view.bigRectHelper.enabled = false
        local playerNode = self.view.levelMapController.view.levelMapLoader.view.player
        self.view.bigRectHelper:FocusNode(playerNode.rectTransform, false)

        self.m_currLevelId = self.m_initialLevelId

        self:_InitDebugTeleport()
    end

    MapCtrl._InitDebugTeleport = HL.Method() << function(self)
        self.view.touchPanel.onClick:AddListener(function(eventData)
            if not self.view.debugToggle.isOn then
                return
            end
            self:_DebugTeleport(eventData.position)
        end)
        self.view.touchPanel.onRightClick:AddListener(function(eventData)
            if not InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.LeftAlt) then
                return
            end
            self:_DebugTeleport(eventData.position)
        end)
        self.view.debugToggle.gameObject:SetActive(true)
    end

    MapCtrl._DebugTeleport = HL.Method(HL.Any) << function(self, position)
        local rectPos = UIUtils.screenPointToUI(
            position,
            self.uiCamera,
            self.view.levelMapController.view.levelMapLoader.view.rectTransform
        )
        local worldPos = self.view.levelMapController.view.levelMapLoader:GetWorldPositionByRectPosition(rectPos)
        if self.m_currLevelId == MapConst.LEVEL_MAP_ID_GETTER.BASE01_LV001 or self.m_currLevelId == MapConst.LEVEL_MAP_ID_GETTER.BASE01_LV003 then
            worldPos = CS.Beyond.Gameplay.UILevelMapUtils.GetDebugTeleportWorldPosition(Vector2(worldPos.x, worldPos.z), 100)
        else
            worldPos = CS.Beyond.Gameplay.UILevelMapUtils.GetDebugTeleportWorldPosition(Vector2(worldPos.x, worldPos.z))
        end
        local configSuccess, levelConfig = GameInstance.player.mapManager:GetLoaderLevelBasicInfoByLevelId(self.m_currLevelId)
        if configSuccess then
            if levelConfig.needInverseXZ then
                local temp = worldPos.x
                worldPos.x = -worldPos.z
                worldPos.z = temp
            end
        end
        Utils.teleportToPosition(self.m_currLevelId, worldPos)
    end
end




HL.Commit(MapCtrl)
