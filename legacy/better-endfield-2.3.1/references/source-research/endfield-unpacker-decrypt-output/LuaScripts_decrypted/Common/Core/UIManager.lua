local panelConfig = require_ex("UI/Panels/PanelConfig")
local rootConfig = require_ex("UI/Panels/RootConfig")
local luaLoader = require_ex('Common/Utils/LuaResourceLoader')

local PlaneDistance = CS.Beyond.UI.UIConst.SCREEN_SPACE_CAMERA_PANEL_DISTANCE

UIManager = HL.Class('UIManager')


UIManager.ids = HL.Field(HL.Table)
UIManager.cfgs = HL.Field(HL.Table)
UIManager.m_names = HL.Field(HL.Table)

UIManager.persistentInputBindingKey = HL.Field(HL.Number) << -1

UIManager.m_panelConfigs = HL.Field(HL.Table)

UIManager.m_openedPanels = HL.Field(HL.Table)
UIManager.m_hidedPanels = HL.Field(HL.Table)
UIManager.m_selfMaintainOrderPanels = HL.Field(HL.Table)

UIManager.m_nextClearScreenKey = HL.Field(HL.Number) << 1
UIManager.m_clearedPanelReverseInfos = HL.Field(HL.Table)
UIManager.m_clearedPanelInfos = HL.Field(HL.Table)
UIManager.m_autoClearScreenKeys = HL.Field(HL.Table)

UIManager.m_orderManagers = HL.Field(HL.Table)
UIManager.m_panelOrderInterval = HL.Field(HL.Number) << 20
UIManager.m_panelOrderTypeInterval = HL.Field(HL.Number) << 1000
UIManager.m_curBlockKeyboardEventPanelOrder = HL.Field(HL.Number) << -1
UIManager.m_ignoreBlockOrderUpdate = HL.Field(HL.Boolean) << false

UIManager.m_resourceLoader = HL.Field(HL.Forward("LuaResourceLoader"))
UIManager.m_panel2Handle = HL.Field(HL.Table)
UIManager.m_customPreloadInfos = HL.Field(HL.Table)

UIManager.uiCamera = HL.Field(Unity.Camera)

UIManager.m_uiCameraDefaultFov = HL.Field(HL.Number) << 15.38

UIManager.uiCanvasRect = HL.Field(Unity.RectTransform)

UIManager.m_uiCanvasScaleHelper = HL.Field(CS.Beyond.UI.UICanvasScaleHelper)

UIManager.uiNode = HL.Field(HL.Table)

UIManager.uiRoot = HL.Field(Unity.Transform)

UIManager.uiInputBindingGroupMonoTarget = HL.Field(CS.Beyond.Input.InputBindingGroupMonoTarget)

UIManager.worldUIRoot = HL.Field(Unity.RectTransform)

UIManager.gyroscopeEffect = HL.Field(CS.Beyond.UI.UIGyroscopeEffect)

UIManager.worldUICanvas = HL.Field(Unity.Canvas)

UIManager.worldObjectRoot = HL.Field(Unity.Transform)

UIManager.m_disabledPanelRoot = HL.Field(Unity.Transform)

UIManager.commonTouchPanel = HL.Field(CS.Beyond.UI.UITouchPanel)

UIManager.m_blockInputKeys = HL.Field(HL.Table)

UIManager.m_mainCameraClosedByUI = HL.Field(HL.Boolean) << false

UIManager.m_hideCameraPanelIds = HL.Field(HL.Table)

UIManager.m_mainCameraTempRequestKeys = HL.Field(HL.Table)

UIManager.m_pauseMainCameraUpdate = HL.Field(HL.Boolean) << false

UIManager.m_worldUICanvasScaleHelper = HL.Field(CS.Beyond.UI.UICanvasScaleHelper)

UIManager.m_onScreenSizeChangedCallback = HL.Field(HL.Function)

UIManager.m_pauseMobileFrameGen = HL.Field(HL.Boolean) << false

UIManager.m_currentStandardFOV = HL.Field(HL.Number) << 0

UIManager.uiDummyNaviLayerRoot = HL.Field(Unity.Transform)

UIManager.uiDummyNaviLayerAsset = HL.Field(HL.Userdata)

UIManager.m_onCanvasChangedCallback = HL.Field(HL.Function)

UIManager.m_isHotSwitching = HL.Field(HL.Boolean) << false
UIManager.m_hotSwitchCache = HL.Field(HL.Table)




UIManager.UIManager = HL.Constructor() << function(self)
    Register(MessageConst.BLOCK_LUA_UI_INPUT, function(arg)
        local shouldBlock, key = unpack(arg)
        self:_ToggleUIInputBinding(not shouldBlock, key)
    end, self)
    Register(MessageConst.CLOSE_UI_PANEL, function(arg)
        local panelName = unpack(arg)
        self:Close(PanelId[panelName])
    end, self)
    Register(MessageConst.ON_TOGGLE_UI_ACTION, function(arg)
        self:OnToggleUiAction(arg)
    end, self)
    Register(MessageConst.CLOSE_ALL_UI_FOR_SWITCH_LANGUAGE, function()
        self:_CloseAllUIForSwitchLanguage()
    end, self)
    Register(MessageConst.ON_INPUT_DEVICE_TYPE_CHANGED, function()

        if DeviceInfo.isMobile then
            self:UpdateUICanvasScaleHelpers()
        end

        self:_RefreshOpenedPanelsCustomUIStyleBindings()
    end, self)

    self.cfgs = {}
    self.m_panelConfigs = {}
    self.m_openedPanels = setmetatable({}, { __mode = "v" })
    self.m_hidedPanels = setmetatable({}, { __mode = "v" })
    self.m_selfMaintainOrderPanels = setmetatable({}, { __mode = "v" })
    self.m_clearedPanelReverseInfos = {}
    self.m_clearedPanelInfos = {}
    self.m_autoClearScreenKeys = {}
    self.m_orderManagers = {}
    self.m_panel2Handle = {}
    self.m_customPreloadInfos = {}
    self.m_blockInputKeys = {}
    self.m_hideCameraPanelIds = {}
    self.m_mainCameraTempRequestKeys = {}
    self.m_blockObtainWaysJumpKeys = {}
    self.m_recentClosedPanelIds = {}
    self.m_waitClearPanels = {}
    self:InitPanelIds()

    local UICtrl = require_ex('UI/Panels/Base/UICtrl').UICtrl
    UICtrl.s_useBlackOutPanelIds = {}
    Register(MessageConst.TOGGLE_PANEL_USE_BLACK_OUT, function(arg)
        local panelName, useBlackOut = unpack(arg)
        local panelId = PanelId[panelName]
        UICtrl.TogglePanelUseBlackOut(panelId, useBlackOut)
    end, self)

    self.persistentInputBindingKey = InputManagerInst:CreateGroup(-1)

    self.m_resourceLoader = luaLoader.LuaResourceLoader()
    self:_InitOrderManagers()


    local uiNodeAsset = self.m_resourceLoader:LoadGameObject(UIConst.UI_NODE_PREFAB_PATH)
    local uiNodeObj = CSUtils.CreateObject(uiNodeAsset)
    self.uiNode = Utils.bindLuaRef(uiNodeObj)
    self.uiNode.gameObject.name = UNITY_EDITOR and "+ UINode" or "UINode"
    GameObject.DontDestroyOnLoad(self.uiNode.gameObject)

    self.uiRoot = self.uiNode.uiRoot.transform
    self.uiInputBindingGroupMonoTarget = self.uiNode.transform:GetComponent("InputBindingGroupMonoTarget")
    self.m_uiCanvasScaleHelper = self.uiRoot:GetComponent("UICanvasScaleHelper")
    self.uiCanvasRect = self.uiNode.dummyCanvas.transform
    self.m_uiCanvasScaleHelper:AddCanvas(self.uiNode.dummyCanvas, self.uiNode.dummyCanvas:GetComponent("CanvasScaler"))
    self.worldUICanvas = self.uiNode.worldUIRoot
    self.worldUIRoot = self.uiNode.worldUIRoot.transform
    self.gyroscopeEffect = self.uiNode.worldUIRoot.transform:GetComponent("UIGyroscopeEffect")
    self.m_worldUICanvasScaleHelper = self.worldUIRoot:GetComponent("UICanvasScaleHelper")
    self.uiDummyNaviLayerRoot = self.uiNode.uiDummyNaviLayerRoot
    self.uiDummyNaviLayerAsset = self.m_resourceLoader:LoadGameObject(UIConst.UI_DUMMY_NAVI_LAYER_PREFAB_PATH)

    self.m_disabledPanelRoot = self.uiNode.disabledUIRoot
    self.m_disabledPanelRoot.gameObject:SetActive(false)

    self.worldObjectRoot = GameObject("UICreatedWorldObjects").transform
    self.worldObjectRoot.position = Vector3.zero
    GameObject.DontDestroyOnLoad(self.worldObjectRoot.gameObject)

    local uiCam = CameraManager.uiCamera
    self.m_uiCameraDefaultFov = uiCam.fieldOfView
    self.uiCamera = uiCam
    self.m_uiCanvasScaleHelper.uiCamera = uiCam
    self.m_worldUICanvasScaleHelper.uiCamera = uiCam
    self.worldUICanvas.worldCamera = uiCam
    self.uiNode.dummyCanvas.worldCamera = uiCam
    self.uiNode.dummyCanvas.planeDistance = PlaneDistance

    self.m_currentStandardFOV = self:GetUICameraFOV()
    self.m_onScreenSizeChangedCallback = function()
        self:_OnScreenSizeChanged()
    end
    self.m_worldUICanvasScaleHelper.onScreenSizeChanged:AddListener(self.m_onScreenSizeChangedCallback)

    self.m_onCanvasChangedCallback = function()
        Notify(MessageConst.ON_UI_CANVAS_SIZE_CHANGED)
    end
    self.m_uiCanvasScaleHelper.onCanvasChanged:AddListener(self.m_onCanvasChangedCallback)

    CS.TMPro.TMP_Settings.instance.dynamicFontAssetLoader = CS.Beyond.UI.DynamicFontAssetLoader()


    local uiRedDotAsset = self.m_resourceLoader:LoadGameObject(UIConst.UI_RED_DOT_ANIMATION_PATH)
    local uiRedDotObj = CSUtils.CreateObject(uiRedDotAsset, self.uiRoot)
    CS.Beyond.UI.UIRedDotAnimation.instance:Init(uiRedDotObj)


    addMetaIndex(self.cfgs, function(_, k)
        local id = self.ids[k]
        if id then
            return self.m_panelConfigs[id]
        end
    end)

    if CS.Beyond.BeyondMemoryUtility.IsLowMemoryDevice() then
        self.m_panelAssetLRUCapacity = 0
    end
end

UIManager.InitPanelIds = HL.Method() << function(self)
    self.ids = {}
    self.m_names = {}
    local nextId = 1
    for name, data in pairs(panelConfig.config) do
        local id = nextId
        nextId = nextId + 1

        self.ids[name] = id
        self.m_names[id] = name
    end
end

UIManager.InitPanelConfigs = HL.Method() << function(self)
    for name, data in pairs(panelConfig.config) do
        local id = self.ids[name]
        local cfg = {
            name = name,
            id = id
        }
        local modelPath = string.format(UIConst.UI_PANEL_MODEL_FILE_PATH, name, name)
        local modelExist = require_check(modelPath)
        if modelExist then
            cfg.modelClass = require_ex(modelPath)[name .. "Model"]
        else
            cfg.modelClass = require_ex("UI/Panels/Base/UIModel").UIModel
        end

        setmetatable(cfg, { __index = data })
        self.m_panelConfigs[id] = cfg
    end


    local backgroundMessage = require_ex(UIConst.UI_BACKGROUND_MESSAGE_PATH).BackgroundMessage
    for ctrlName, msgs in pairs(backgroundMessage.s_messages) do
        for msg, funcName in pairs(msgs) do
            Register(msg, function(msgArg)
                local id = self.ids[ctrlName]
                local cfg = self.m_panelConfigs[id]
                if not cfg.ctrlClass then
                    local path = string.format(UIConst.UI_PANEL_CTRL_FILE_PATH, ctrlName, ctrlName)
                    local ctrlClass = require_ex(path)[ctrlName .. "Ctrl"]
                    cfg.ctrlClass = ctrlClass
                end
                local ctrlClass = cfg.ctrlClass

                if HL.TryGet(ctrlClass, "s_overrideMessages") then
                    ctrlClass.s_messages = ctrlClass.s_overrideMessages
                else
                    ctrlClass.s_messages = ctrlClass.s_messages
                end

                if msgArg == nil then
                    ctrlClass[funcName]()
                else
                    ctrlClass[funcName](msgArg)
                end
            end, self)
        end
    end

    self:_InitRoots()
end

UIManager.OpenInitPanels = HL.Method() << function(self)

    Input.multiTouchEnabled = true


    self:Open(self.ids.Touch)
    self:Open(self.ids.CommonDrag)
    self.commonTouchPanel = self.cfgs.Touch.ctrl.view.uiTouchPanel
    self:Open(self.ids.MouseIconHint)
    self:Open(self.ids.CommonTips)
    self:Open(self.ids.FullScreenSceneBlur)

    Notify(MessageConst.ON_OPEN_INIT_PANELS)
end



UIManager.Open = HL.Method(HL.Number, HL.Opt(HL.Any, HL.Table, HL.Forward("PhasePanelItem"))).Return(HL.Opt(HL.Forward("UICtrl")))
    << function(self, panelId, arg, callbacks, bindPhaseItem)
    if self:IsOpen(panelId) then
        logger.error("Panel Already Opened", self.m_names[panelId])
        return
    end

    local cfg = self.m_panelConfigs[panelId]
    if not cfg then
        logger.error("No Panel Config", panelId)
        return
    end

    logger.info("UIManager.Open", self.m_names[panelId])
    LuaProfilerUtils.BeginSample("UIManager.Open." .. self.m_names[panelId])

    callbacks = callbacks or {}

    Notify(MessageConst.ON_BEFORE_UI_PANEL_OPEN, cfg.name)


    self:_TryAttachNaviDummyLayer(panelId)


    local cachedInfo = self.m_hotSwitchCache and self.m_hotSwitchCache[panelId]
    local asset, assetType, panelObj
    local isReusingHotSwitchCache = false

    if cachedInfo then
        local _, expectedAssetType = self:_GetPanelAssetPathAndType(panelId)

        if cachedInfo.assetType == expectedAssetType then

            panelObj = cachedInfo.go
            assetType = cachedInfo.assetType
            isReusingHotSwitchCache = true
            self.m_hotSwitchCache[panelId] = nil
        else

            if cachedInfo.worldRoot then
                GameObject.DestroyImmediate(cachedInfo.worldRoot.gameObject)
            end
            if cachedInfo.worldAutoRoot then
                GameObject.DestroyImmediate(cachedInfo.worldAutoRoot.gameObject)
            end
            cachedInfo.runtimeState = nil
            cachedInfo.loader:DisposeAllHandles()
            GameObject.DestroyImmediate(cachedInfo.go)
            self.m_hotSwitchCache[panelId] = nil
            cachedInfo = nil
        end
    end

    if not cachedInfo then

        asset, assetType = self:_LoadPanelAsset(panelId)
    end

    local parent = self:_GetPanelParentTransform(cfg)
    if not cachedInfo then
        if UNITY_EDITOR then


            panelObj = CSUtils.CreateObject(asset, self.m_disabledPanelRoot)
            panelObj.gameObject:SetActive(false)
            panelObj.transform:SetParent(parent)
        else
            asset.gameObject:SetActive(false)
            panelObj = CSUtils.CreateObject(asset, parent)
        end
    else
        panelObj.transform:SetParent(parent)
    end

    panelObj.name = string.format("%sPanel", cfg.name)

    local uiCamera = self.uiCamera

    local view = {
        curPanelCfg = {}
    }
    if cfg.clearScreen then
        local exceptPanelIds = {}
        if cfg.clearScreen ~= true then
            for _, v in ipairs(cfg.clearScreen) do
                table.insert(exceptPanelIds, self.ids[v])
            end
        end
        self.m_autoClearScreenKeys[panelId] = self:ClearScreen(exceptPanelIds)
    end
    if cfg.blockObtainWaysJump then
        local exceptIds = {}
        if cfg.blockObtainWaysJump ~= true then
            for _, whitePhaseName in ipairs(cfg.blockObtainWaysJump) do
                table.insert(exceptIds, whitePhaseName)
            end
        end
        self:ToggleBlockObtainWaysJump(cfg.name, true, exceptIds)
    end
    local refs = panelObj:GetComponent("LuaReference")
    refs:BindToLua(view)
    view.panelCanvas = view.transform:GetComponent("Canvas")
    view.canvasScaler = view.transform:GetComponent("CanvasScaler")
    view.luaPanel = view.transform:GetComponent("LuaPanel")
    view.raycaster = view.transform:GetComponent("GraphicRaycaster")
    view.inputGroup = view.transform:GetComponent("InputBindingGroupMonoTarget")
    view.selectableNaviGroup = view.transform:GetComponent("UISelectableNaviGroup")
    UIUtils.initLuaCustomConfig(view)

    view.panelCanvas.worldCamera = self.uiCamera
    if not cfg.isWorldUI then
        view.panelCanvas.renderMode = CS.UnityEngine.RenderMode.ScreenSpaceCamera
        view.panelCanvas.planeDistance = PlaneDistance
        self.m_uiCanvasScaleHelper:AddCanvas(view.panelCanvas, view.canvasScaler)
    else
        view.rectTransform.localScale = Vector3.one
        view.rectTransform.pivot = Vector2.one / 2
        view.rectTransform.anchorMin = Vector2.zero
        view.rectTransform.anchorMax = Vector2.one
        view.rectTransform.offsetMin = Vector2.zero
        view.rectTransform.offsetMax = Vector2.zero
        view.rectTransform.anchoredPosition3D = Vector3.zero
    end

    view.luaPanel.panelId = panelId
    view.luaPanel.panelName = cfg.name
    view.luaPanel.uiCamera = uiCamera
    view.luaPanel.planeDistance = PlaneDistance
    if cfg.orderType then
        view.luaPanel.panelLevel = UIConst.PANEL_ORDER_TO_PANEL_LEVEL[cfg.orderType]
    end
    view.luaPanel.inited = true
    CS.Beyond.UI.LuaPanel.s_openedLuaPanels:Add(cfg.name, view.luaPanel)

    if not cfg.ctrlClass then
        local name = cfg.name
        local path = string.format(UIConst.UI_PANEL_CTRL_FILE_PATH, name, name)

        local ctrlClass = require_ex(path)[name .. "Ctrl"]

        if HL.TryGet(ctrlClass, "s_overrideMessages") then
            ctrlClass.s_messages = ctrlClass.s_overrideMessages
        else
            ctrlClass.s_messages = ctrlClass.s_messages
        end

        cfg.ctrlClass = ctrlClass
    end

    local ctrl = cfg.ctrlClass()
    ctrl.panelId = panelId
    ctrl.panelCfg = cfg
    ctrl.model = cfg.modelClass()
    ctrl.view = view
    ctrl.loader = cachedInfo and cachedInfo.loader or luaLoader.LuaResourceLoader()
    ctrl.animationWrapper = view.luaPanel.animationWrapper
    ctrl.naviGroup = view.transform:GetComponent("UISelectableNaviGroup")
    ctrl.uiCamera = uiCamera
    ctrl.planeDistance = PlaneDistance
    ctrl.isControllerPanel = assetType == UIConst.PANEL_ASSET_TYPES.Controller
    ctrl.isPCPanel = assetType == UIConst.PANEL_ASSET_TYPES.PC
    ctrl.isDefaultPanel = assetType == UIConst.PANEL_ASSET_TYPES.Default
    if cachedInfo then
        ctrl.m_worldRoot = cachedInfo.worldRoot
        ctrl.m_worldAutoRoot = cachedInfo.worldAutoRoot
        ctrl:RestoreHotSwitchRuntimeState(cachedInfo.runtimeState)
    end
    ctrl.m_updateKeys = {}
    cfg.ctrl = ctrl
    self.m_openedPanels[panelId] = ctrl
    Notify(MessageConst.ON_UI_PANEL_START_OPEN, cfg.name)
    if cfg.hideCamera then
        self:_OnPanelHideCameraActivated(panelId)
    end
    local animationWrapper = view.luaPanel.animationWrapper
    view.luaPanel.onAnimationInFinished:AddListener(function()
        logger.info("OnAnimationInFinished", cfg.name)



        if cfg.hideCamera and cfg.hideCameraAfterInAnim then
            self:RemoveMainCameraTempRequest("HideCamInAnim_" .. cfg.name)
        end

        ctrl:OnAnimationInFinished()
        Notify(MessageConst.ON_UI_PANEL_OPENED, cfg.name)

        local needTrigger = animationWrapper == nil or animationWrapper.curState ~= CS.Beyond.UI.UIConst.AnimationState.Out
        if needTrigger then
            CS.Beyond.Gameplay.Conditions.OnUIPanelOpen.Trigger(cfg.name, true, PhaseManager:GetTopPhaseName())
            if GameInstance.player then
                GameInstance.player.guide:OnUIPanelOpened(cfg.name)
            end
        end

        if callbacks.onAnimationInFinished then
            callbacks.onAnimationInFinished()
        end
    end)


    if cfg.selfMaintainOrder then
        self.m_selfMaintainOrderPanels[panelId] = ctrl
    else
        self:_AutoSetPanelOrder(cfg, true)
    end


    if not isReusingHotSwitchCache then
        if HL.TryGet(ctrl, "OnBeforePanelActive") then
            ctrl:OnBeforePanelActive(arg)
        end
        panelObj.gameObject:SetActive(true)



        if cfg.hideCameraAfterInAnim then
            if cfg.hideCamera then
                self:AddMainCameraTempRequest("HideCamInAnim_" .. cfg.name)
            elseif BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
                logger.error("PanelConfig.hideCameraAfterInAnim 需配合 hideCamera = true，配置无效", cfg.name)
            end
        end
    end


    for msg, funcName in pairs(cfg.ctrlClass.s_messages) do
        Register(msg, function(msgArg)
            if ctrl.m_isClosed then
                logger.info("UIManager 触发面板监听消息，但是面板已经关闭了，所以无视掉", ctrl.panelCfg.name, MessageConst.getMsgName(msg))
                return
            end

            if msgArg == nil then
                ctrl[funcName](ctrl)
            else
                ctrl[funcName](ctrl, msgArg)
            end
        end, ctrl)
    end


    if bindPhaseItem ~= nil then
        bindPhaseItem:BindUICtrl(ctrl)
    end


    local succ, log = xpcall(function()
        ctrl.model:InitModel()
        ctrl:OnCreate(arg)
        ctrl:OnShow()
    end, debug.traceback)
    if not succ then
        logger.critical(log)
    end
    ctrl.isFinishedCreation = true

    if cfg.selfMaintainOrder then
        self:CalcOtherSystemPropertyByPanelOrder()
    end

    self:_SendEventUISwitch(cfg, true)
    CS.Beyond.Gameplay.Conditions.OnUIPanelOpen.Trigger(cfg.name, false, PhaseManager:GetTopPhaseName())

    LuaProfilerUtils.EndSample()
    return ctrl
end

UIManager.PreloadPanelAsset = HL.Method(HL.Number, HL.Any, HL.Opt(HL.Function)) << function(self, panelId, source, onPreloadComplete)
    if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
        logger.info("UIManager.PreloadPanelAsset.", self.m_names[panelId])
    end

    local info = self.m_customPreloadInfos[source]
    if not info then
        info = {}
        self.m_customPreloadInfos[source] = info
    end
    info[panelId] = true

    LuaProfilerUtils.BeginSample("UIManager.PreloadPanelAsset." .. self.m_names[panelId])
    self:_LoadPanelAsset(panelId, true, onPreloadComplete)
    LuaProfilerUtils.EndSample()
end


UIManager.PreloadPersistentPanelAsset = HL.Method(HL.Number, HL.Opt(HL.Function)) << function(self, panelId, onPreloadComplete)
    self:PreloadPanelAsset(panelId, "Persistent", onPreloadComplete)
end

UIManager.ClearPreloadAsset = HL.Method(HL.Any) << function(self, source)
    local info = self.m_customPreloadInfos[source]
    if not info then
        return
    end
    logger.info("UIManager.ClearPreloadAsset", source)
    for panelId, _ in pairs(info) do
        if not self:IsOpen(panelId) then
            local assetInfo = self.m_panel2Handle[panelId]
            if assetInfo then
                self.m_panel2Handle[panelId] = nil
                self.m_resourceLoader:DisposeHandleByKey(assetInfo[1])
                logger.info("UIManager.ClearPreloadAsset DisposeHandleByKey", panelId, self.m_names[panelId])
            end
        end
    end
end

UIManager.CheckPanelAssetHadLoaded = HL.Method(HL.Number).Return(HL.Boolean) << function(self, panelId)
    local assetInfo = self.m_panel2Handle[panelId]
    return assetInfo ~= nil
end

UIManager._GetPanelAssetPathAndTypeByInputType = HL.Method(HL.Number, HL.Userdata).Return(HL.String, HL.Number) << function(self, panelId, inputType)
    local cfg = self.m_panelConfigs[panelId]
    local path
    local assetType = UIConst.PANEL_ASSET_TYPES.Default
    local usingController = inputType == DeviceInfo.InputType.Controller
    local usingKeyboardOrController = inputType == DeviceInfo.InputType.Keyboard or usingController

    if usingController then
        local ctPath = string.format(UIConst.UI_CONTROLLER_PANEL_PREFAB_PATH, cfg.folder, cfg.name)
        if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
            if not ResourceManager.CheckExists(ctPath) then
                ctPath = string.format(UIConst.UI_CONTROLLER_PANEL_PREFAB_DEV_PATH, cfg.folder, cfg.name)
            end
        end
        if ResourceManager.CheckExists(ctPath) then
            assetType = UIConst.PANEL_ASSET_TYPES.Controller
            path = ctPath
        end
    end

    if not path and usingKeyboardOrController then
        local pcPath = string.format(UIConst.UI_PC_PANEL_PREFAB_PATH, cfg.folder, cfg.name)
        if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
            if not ResourceManager.CheckExists(pcPath) then
                pcPath = string.format(UIConst.UI_PC_PANEL_PREFAB_DEV_PATH, cfg.folder, cfg.name)
            end
        end
        if ResourceManager.CheckExists(pcPath) then
            assetType = UIConst.PANEL_ASSET_TYPES.PC
            path = pcPath
        end
    end

    if not path then
        path = string.format(UIConst.UI_PANEL_PREFAB_PATH, cfg.folder, cfg.name)
        if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
            if not ResourceManager.CheckExists(path) then
                path = string.format(UIConst.UI_PANEL_PREFAB_DEV_PATH, cfg.folder, cfg.name)
            end
        end
    end

    return path, assetType
end

UIManager._GetPanelAssetPathAndType = HL.Method(HL.Number).Return(HL.String, HL.Number) << function(self, panelId)
    return self:_GetPanelAssetPathAndTypeByInputType(panelId, DeviceInfo.inputType)
end

UIManager._IsUsingSamePanelAssetOnInputTypeChange = HL.Method(HL.Number, HL.Userdata, HL.Userdata).Return(HL.Boolean) << function(self, panelId, oldInputType, newInputType)
    local oldPath, oldAssetType = self:_GetPanelAssetPathAndTypeByInputType(panelId, oldInputType)
    local newPath, newAssetType = self:_GetPanelAssetPathAndTypeByInputType(panelId, newInputType)
    return oldAssetType == newAssetType and oldPath == newPath
end

UIManager._LoadPanelAsset = HL.Method(HL.Number, HL.Opt(HL.Boolean, HL.Function)).Return(HL.Opt(GameObject, HL.Number)) << function(self, panelId, isPreload, onPreloadComplete)
    logger.info("UIManager._LoadPanelAsset", panelId, self.m_names[panelId], isPreload)
    self:_UpdatePanelAssetLRU(panelId, true)

    local asset
    local path
    local assetType
    path, assetType = self:_GetPanelAssetPathAndType(panelId)

    local assetInfo = self.m_panel2Handle[panelId]
    if assetInfo and assetInfo[2] ~= assetType then
        self.m_panel2Handle[panelId] = nil
        self.m_resourceLoader:DisposeHandleByKey(assetInfo[1])
        logger.info("UIManager._LoadPanelAsset DisposeHandleByKey due to assetType changed", panelId, self.m_names[panelId], assetInfo[2], assetType)
        assetInfo = nil
    end
    if not assetInfo then
        if not isPreload then
            local assetKey

            if not self.m_panel2Handle[panelId] then
                asset, assetKey = self.m_resourceLoader:LoadI18NAsset(path, typeof(CS.UnityEngine.GameObject))
                self.m_panel2Handle[panelId] = { assetKey, assetType }
            end
        else
            local assetKey
            assetKey = self.m_resourceLoader:LoadGameObjectAsync(path, function(loadedAsset)
                if not assetKey then
                    logger.error("预加载面板时异步调用同步返回了", path)
                    return
                end
                if self.m_panel2Handle[panelId] then

                    self.m_resourceLoader:DisposeHandleByKey(assetKey)
                else
                    self.m_panel2Handle[panelId] = { assetKey, assetType }
                end
                if onPreloadComplete then
                    onPreloadComplete()
                end
            end)
            return
        end
    else

        if isPreload then
            if onPreloadComplete then
                onPreloadComplete()
            end
            return
        end
        local assetKey = assetInfo[1]
        assetType = assetInfo[2]
        asset = self.m_resourceLoader:GetGameObjectByKey(assetKey)
    end
    return asset, assetType
end

UIManager.PauseMainCameraUpdate = HL.Method() << function(self)
    logger.info("UIManager.PauseMainCameraUpdate")
    self.m_pauseMainCameraUpdate = true
end

UIManager.ResumeMainCameraUpdate = HL.Method() << function(self)
    logger.info("UIManager.ResumeMainCameraUpdate")
    self.m_pauseMainCameraUpdate = false
    self:_UpdateMainCamera()
end

UIManager.AddMainCameraTempRequest = HL.Method(HL.String) << function(self, key)
    logger.info("UIManager.AddMainCameraTempRequest", key)
    self.m_mainCameraTempRequestKeys[key] = true
    self:_UpdateMainCamera()
end

UIManager.RemoveMainCameraTempRequest = HL.Method(HL.String) << function(self, key)
    logger.info("UIManager.RemoveMainCameraTempRequest", key)
    self.m_mainCameraTempRequestKeys[key] = nil
    self:_UpdateMainCamera()
end

UIManager.GetHideCameraPanelCount = HL.Method().Return(HL.Number) << function(self)
    local count = 0
    for _ in pairs(self.m_hideCameraPanelIds) do
        count = count + 1
    end
    return count
end




UIManager.WasSceneVisibleBeforeThisHideCameraPanel = HL.Method().Return(HL.Boolean) << function(self)
    if self.m_pauseMainCameraUpdate then

        return not self.m_mainCameraClosedByUI
    end


    return self:GetHideCameraPanelCount() <= 1
end

UIManager._OnPanelHideCameraActivated = HL.Method(HL.Number) << function(self, panelId)
    self.m_hideCameraPanelIds[panelId] = true
    self:_UpdateMainCamera()
end

UIManager._OnPanelHideCameraDeactivated = HL.Method(HL.Number) << function(self, panelId)
    self.m_hideCameraPanelIds[panelId] = nil
    local cfg = self.m_panelConfigs[panelId]
    if cfg and cfg.hideCameraAfterInAnim then

        self:RemoveMainCameraTempRequest("HideCamInAnim_" .. cfg.name)
    end
    self:_UpdateMainCamera()
end
















UIManager._NeedCameraForHideCamPanelOut = HL.Method().Return(HL.Boolean) << function(self)
    local hasOutPlaying = false
    for panelId, _ in pairs(self.m_hideCameraPanelIds) do
        local ctrl = self.m_openedPanels[panelId]
        if ctrl then
            if ctrl:IsPlayingAnimationOut() then
                hasOutPlaying = true
            else

                return false
            end
        end
    end
    return hasOutPlaying
end

UIManager._UpdateMainCamera = HL.Method() << function(self)








    local needCamForOut = self:_NeedCameraForHideCamPanelOut()


    if self.m_pauseMainCameraUpdate and next(self.m_mainCameraTempRequestKeys) == nil then
        return
    end

    local tryCloseCamera = next(self.m_hideCameraPanelIds) ~= nil
            and next(self.m_mainCameraTempRequestKeys) == nil
            and not needCamForOut

    if self.m_mainCameraClosedByUI == tryCloseCamera then
        return
    end

    logger.info("UIManager._UpdateMainCamera.tryCloseCamera", tryCloseCamera, self.m_hideCameraPanelIds, self.m_mainCameraTempRequestKeys, "needCamForOut=", needCamForOut)
    if tryCloseCamera then
        CameraManager:AddMainCamCullingMaskConfig("UIManager", UIConst.LAYERS.Nothing)
    else
        CameraManager:RemoveMainCamCullingMaskConfig("UIManager")
    end
    self.m_mainCameraClosedByUI = tryCloseCamera
end





UIManager.DumpMainCameraInfo = HL.Method().Return(HL.String) << function(self)
    local mainCam = CameraManager.mainCamera
    local maskValue = mainCam and mainCam.cullingMask or -1
    local panelNames = {}
    local outAnimPanelNames = {}
    for pid, _ in pairs(self.m_hideCameraPanelIds) do
        local name = self.m_names[pid] or tostring(pid)
        table.insert(panelNames, name)
        local ctrl = self.m_openedPanels[pid]
        if ctrl and ctrl:IsPlayingAnimationOut() then
            table.insert(outAnimPanelNames, name)
        end
    end
    local reqKeys = {}
    for k, _ in pairs(self.m_mainCameraTempRequestKeys) do
        table.insert(reqKeys, k)
    end


    local info = string.format(
        "[SceneCam] mask=%d closed=%s paused=%s\npanels: %s\ntempReq: %s\nplayingOut: %s needCamForOut=%s",
        maskValue,
        tostring(self.m_mainCameraClosedByUI),
        tostring(self.m_pauseMainCameraUpdate),
        #panelNames > 0 and table.concat(panelNames, ", ") or "none",
        #reqKeys > 0 and table.concat(reqKeys, ", ") or "none",
        #outAnimPanelNames > 0 and table.concat(outAnimPanelNames, ", ") or "none",
        tostring(self:_NeedCameraForHideCamPanelOut())
    )
    logger.info(info)
    return info
end

local defaultNoEventOrderTypes = {
    [Types.EPanelOrderTypes.Hud] = true,
    [Types.EPanelOrderTypes.LowerHud] = true,
    [Types.EPanelOrderTypes.BottomScreenEffect] = true,
    [Types.EPanelOrderTypes.TopScreenEffect] = true,
    [Types.EPanelOrderTypes.Toast] = true,
    [Types.EPanelOrderTypes.Debug] = true,
}
UIManager._SendEventUISwitch = HL.Method(HL.Table, HL.Boolean) << function(self, cfg, isEnter)
    return




end

UIManager.AutoOpen = HL.Method(HL.Number, HL.Opt(HL.Any, HL.Boolean, HL.Function)).Return(HL.Opt(HL.Forward("UICtrl")))
    << function(self, panelId, arg, forceShow, onShowCallback)
    local isOpen, ctrl = self:IsOpen(panelId)
    if not isOpen then
        return self:Open(panelId, arg)
    else
        if forceShow or self:IsHide(panelId) then
            self:Show(panelId)
        end
        if onShowCallback then
            onShowCallback(ctrl)
        end
        return ctrl
    end
end

UIManager.AutoOpenWithPhaseItem = HL.Method(HL.Number, HL.Forward("PhasePanelItem"), HL.Opt(HL.Any)).Return(HL.Opt(HL.Forward("UICtrl")))
    << function(self, panelId, phaseItem, arg)
    local isOpen, ctrl = self:IsOpen(panelId)
    if not isOpen then
        return self:Open(panelId, arg, nil, phaseItem)
    else
        phaseItem:BindUICtrl(ctrl)
        if self:IsHide(panelId) then
            self:Show(panelId)
        end
        return ctrl
    end
end

UIManager.IsOpen = HL.Method(HL.Number).Return(HL.Boolean, HL.Opt(HL.Forward("UICtrl"))) << function(self, panelId)
    local ctrl = self.m_openedPanels[panelId]
    return ctrl ~= nil and not ctrl.m_isClosed, ctrl
end

UIManager.Close = HL.Method(HL.Number) << function(self, panelId)
    if not self:IsOpen(panelId) then
        return
    end

    local cfg = self.m_panelConfigs[panelId]
    local ctrl = cfg.ctrl

    CS.Beyond.UI.LuaPanel.s_openedLuaPanels:Remove(cfg.name)


    cfg.ctrl = nil
    self.m_openedPanels[panelId] = nil
    self.m_hidedPanels[panelId] = nil
    self.m_waitClearPanels[panelId] = nil
    local hideByOthers = self.m_clearedPanelReverseInfos[panelId]
    if hideByOthers then
        for k, _ in pairs(hideByOthers) do
            local panels = self.m_clearedPanelInfos[k]
            if panels then
                panels[panelId] = nil
            end
        end
        self.m_clearedPanelReverseInfos[panelId] = nil
    end
    if cfg.selfMaintainOrder then
        self.m_selfMaintainOrderPanels[panelId] = nil
    else
        self:_RemoveFromOrderStack(panelId)
    end

    logger.info("UIManager.Close", self.m_names[panelId])
    LuaProfilerUtils.BeginSample("UIManager.Close." .. cfg.name)

    local clearScreenKey = self.m_autoClearScreenKeys[panelId]
    self.m_autoClearScreenKeys[panelId] = nil

    if not cfg.isWorldUI then
        self.m_uiCanvasScaleHelper:RemoveCanvas(ctrl.view.panelCanvas, ctrl.view.canvasScaler)
    end


    MessageManager:UnregisterAll(ctrl)

    local shouldCacheGoOnInputChange = self.m_isHotSwitching and InputDeviceChangeConst.SUPPORT_REUSE_GO_ON_INPUT_CHANGE_PANELS[cfg.name]
    local cachedInfo
    local succ, log = xpcall(function()
        ctrl:OnClose()
        if shouldCacheGoOnInputChange then
            cachedInfo = {
                go = ctrl.view.gameObject,
                loader = ctrl.loader,
                worldRoot = ctrl.m_worldRoot,
                worldAutoRoot = ctrl.m_worldAutoRoot,
                runtimeState = ctrl:ExtractHotSwitchRuntimeState(),
            }


            ctrl:_CleanupInputForHotSwitch()


            ctrl.m_worldRoot = nil
            ctrl.m_worldAutoRoot = nil

            if ctrl.isControllerPanel then
                cachedInfo.assetType = UIConst.PANEL_ASSET_TYPES.Controller
            elseif ctrl.isPCPanel then
                cachedInfo.assetType = UIConst.PANEL_ASSET_TYPES.PC
            else
                cachedInfo.assetType = UIConst.PANEL_ASSET_TYPES.Default
            end
        end
        ctrl:Clear()
        ctrl.model:OnClose()
    end, debug.traceback)
    if not succ then
        logger.critical(log)
    end

    local gameObject = ctrl.view.gameObject
    local loader = ctrl.loader
    CSUtils.ClearUIComponents(gameObject)
    ctrl.view.luaPanel.onAnimationInFinished:RemoveAllListeners()

    if cachedInfo then

        self.m_hotSwitchCache[panelId] = cachedInfo
    else

        ctrl.view.inputGroup:DeleteGroup()
        GameObject.DestroyImmediate(gameObject)
        if ENABLE_LUA_LEAK_CHECK then
            LuaObjectMemoryLeakChecker:AddDetectLuaObject(ctrl)
        end
        loader:DisposeAllHandles()
    end



    if not cfg.isResidentPanel then
        self:_UpdatePanelAssetLRU(panelId, false)
    end

    if clearScreenKey then
        self:RecoverScreen(clearScreenKey)
    end
    self:CalcOtherSystemPropertyByPanelOrder()
    if cfg.blockObtainWaysJump then
        self:ToggleBlockObtainWaysJump(cfg.name, false)
    end

    Notify(MessageConst.ON_UI_PANEL_CLOSED, cfg.name)
    CS.Beyond.Gameplay.Conditions.OnUIPanelClose.Trigger(cfg.name)

    self:_TryDetachNaviDummyLayer(panelId)

    self:_SendEventUISwitch(cfg, false)
    if cfg.hideCamera then
        self:_OnPanelHideCameraDeactivated(panelId)
    end

    LuaProfilerUtils.EndSample()
end

UIManager.Show = HL.Method(HL.Number) << function(self, panelId)
    if not self:IsOpen(panelId) then
        logger.error("Panel Not Open", panelId, self.m_names[panelId])
        return
    end

    if not self.m_hidedPanels[panelId] then
        return
    end

    LuaProfilerUtils.BeginSample("UIManager.Show." .. self.m_names[panelId])
    local hideByOthers = self.m_clearedPanelReverseInfos[panelId] ~= nil
    self.m_hidedPanels[panelId] = nil
    if not hideByOthers then

        self:_TryAttachNaviDummyLayer(panelId)
        self:_InternalShow(panelId)
    end
    LuaProfilerUtils.EndSample()
end

UIManager._RestorePanelVisibleComponents = HL.Method(HL.Forward("UICtrl"), HL.Boolean) << function(self, ctrl, playAnimation)
    if ctrl.panelCfg.useCanvasHide then
        ctrl.view.gameObject:SetLayerRecursive(UIConst.UI_LAYER)
        ctrl.view.panelCanvas.enabled = true
        ctrl.view.luaPanel.enabled = true
        if ctrl.animationWrapper then
            ctrl.animationWrapper.enabled = true
            if playAnimation and ctrl.animationWrapper.autoPlay then
                ctrl.animationWrapper:PlayInAnimation()
            end
        end
    else
        ctrl.view.gameObject:SetActive(true)
    end
    ctrl:SetGameObjectVisible(true)
end

UIManager._InternalShow = HL.Method(HL.Number) << function(self, panelId)
    local ctrl = self.m_openedPanels[panelId]
    if ctrl == nil then
        return
    end

    if ctrl.panelCfg.hideCamera then
        self:_OnPanelHideCameraActivated(panelId)
    end

    self:_RestorePanelVisibleComponents(ctrl, true)
    if IsNull(ctrl.view.gameObject) then
        logger.error("UIManager._InternalShow 面板GO已被错误销毁", ctrl.panelCfg.name)
        return
    end





    self:CalcOtherSystemPropertyByPanelOrder()

    if ctrl.panelCfg.blockObtainWaysJump then
        local exceptIds = {}
        if ctrl.panelCfg.blockObtainWaysJump ~= true then
            for _, whitePhaseName in ipairs(ctrl.panelCfg.blockObtainWaysJump) do
                table.insert(exceptIds, whitePhaseName)
            end
        end
        self:ToggleBlockObtainWaysJump(ctrl.panelCfg.name, true, exceptIds)
    end

    local luaPanel = ctrl.view.luaPanel


    if not luaPanel.blockWhileAnim or luaPanel.animationInFinished then
        luaPanel:RecoverAllInput()
    end

    ctrl:OnShow()

    Notify(MessageConst.ON_UI_PANEL_SHOW, ctrl.panelCfg.name)

    self:_SendEventUISwitch(ctrl.panelCfg, true)
    CS.Beyond.Gameplay.Conditions.OnUIPanelOpen.Trigger(ctrl.panelCfg.name, false, PhaseManager:GetTopPhaseName())
end

UIManager.IsShow = HL.Method(HL.Number, HL.Opt(HL.Boolean)).Return(HL.Boolean) << function(self, panelId, ignoreClear)
    return self:IsOpen(panelId) and not self.m_hidedPanels[panelId] and (ignoreClear or not self.m_clearedPanelReverseInfos[panelId])
end

UIManager.Hide = HL.Method(HL.Number) << function(self, panelId)
    if not self:IsOpen(panelId) then
        return
    end

    if self.m_hidedPanels[panelId] then
        return
    end
    self.m_hidedPanels[panelId] = true

    if not self.m_clearedPanelReverseInfos[panelId] then

        self:_TryDetachNaviDummyLayer(panelId)
        self:_InternalHide(panelId)
    end
end



UIManager.HideWithKey = HL.Method(HL.Number, HL.String) << function(self, panelId, key)
    if not self:IsOpen(panelId) then
        return
    end

    local isShow = self:IsShow(panelId)

    local clearedPanelIds = self.m_clearedPanelInfos[key]
    if not clearedPanelIds then
        clearedPanelIds = {}
        self.m_clearedPanelInfos[key] = clearedPanelIds
    end
    local clearedByOthers = self.m_clearedPanelReverseInfos[panelId]
    if not clearedByOthers then
        clearedByOthers = {}
        self.m_clearedPanelReverseInfos[panelId] = clearedByOthers
    end
    clearedPanelIds[panelId] = true
    clearedByOthers[key] = true

    if isShow then
        self:_InternalHide(panelId)
    end
end


UIManager.ShowWithKey = HL.Method(HL.Number, HL.String) << function(self, panelId, key)
    if not self:IsOpen(panelId) or self:IsShow(panelId) then
        return
    end

    local clearedPanelIds = self.m_clearedPanelInfos[key]
    if not clearedPanelIds then
        return
    end
    clearedPanelIds[panelId] = nil
    if not next(clearedPanelIds) then
        self.m_clearedPanelInfos[key] = nil
    end

    local clearedByOthers = self.m_clearedPanelReverseInfos[panelId]
    if clearedByOthers then
        clearedByOthers[key] = nil
        if not next(clearedByOthers) then
            self.m_clearedPanelReverseInfos[panelId] = nil
        end
    end

    if self:IsShow(panelId) then
        self:_InternalShow(panelId)
    end
end

UIManager._InternalHide = HL.Method(HL.Number) << function(self, panelId)
    local ctrl = self.m_openedPanels[panelId]
    if ctrl == nil then
        return
    end

    local cfg = ctrl.panelCfg
    ctrl:OnHide()

    Notify(MessageConst.ON_UI_PANEL_HIDE, ctrl.panelCfg.name)
    ctrl:SetGameObjectVisible(false)

    if cfg.useCanvasHide then
        ctrl.view.gameObject:SetLayerRecursive(UIConst.HIDE_LAYER)
        ctrl.view.panelCanvas.enabled = false
        ctrl.view.luaPanel:BlockAllInput()
        ctrl.view.luaPanel.enabled = false
        if ctrl.animationWrapper then
            ctrl.animationWrapper.enabled = false
        end
    else
        ctrl.view.gameObject:SetActive(false)
    end
    if IsNull(ctrl.view.gameObject) then
        logger.error("UIManager._InternalHide 面板GO已被错误销毁", ctrl.panelCfg.name)
        return
    end






    self:CalcOtherSystemPropertyByPanelOrder()
    if cfg.blockObtainWaysJump then
        self:ToggleBlockObtainWaysJump(cfg.name, false)
    end
    self:_SendEventUISwitch(ctrl.panelCfg, false)
    if ctrl.panelCfg.hideCamera then
        self:_OnPanelHideCameraDeactivated(panelId)
    end
    CS.Beyond.Gameplay.Conditions.OnUIPanelClose.Trigger(cfg.name)
end

UIManager.IsHide = HL.Method(HL.Number).Return(HL.Boolean) << function(self, panelId)
    return self:IsOpen(panelId) and not self:IsShow(panelId)
end

UIManager.IsInternalHidden = HL.Method(HL.Number).Return(HL.Boolean) << function(self, panelId)
    local ctrl = self.m_openedPanels[panelId]
    if ctrl == nil then
        return false
    end

    local cfg = ctrl.panelCfg
    if cfg.useCanvasHide then
        return not ctrl.view.panelCanvas.enabled
    else
        return not ctrl.view.gameObject.activeSelf
    end
end




UIManager.ClearScreen = HL.Method(HL.Opt(HL.Table)).Return(HL.Opt(HL.Number)) << function(self, exceptPanelIds)
    if self.m_inClearScreen then
        logger.error("清屏中，不能额外操作")
        return -1
    end

    local clearScreenKey, panelIds = self:_GetClearScreenTargets(exceptPanelIds)

    self.m_ignoreBlockOrderUpdate = true
    for _, id in ipairs(panelIds) do
        self:_InternalHide(id)
    end
    self.m_ignoreBlockOrderUpdate = false
    self:CalcOtherSystemPropertyByPanelOrder()

    logger.info("UIManager.ClearScreen", clearScreenKey)
    return clearScreenKey
end

UIManager._GetClearScreenTargets = HL.Method(HL.Opt(HL.Table)).Return(HL.Number, HL.Table) << function(self, exceptPanelIds)
    local clearedPanels = {}
    local clearScreenKey = self.m_nextClearScreenKey
    self.m_nextClearScreenKey = self.m_nextClearScreenKey + 1
    self.m_clearedPanelInfos[clearScreenKey] = clearedPanels

    local panelIds = {}
    for id, ctrl in pairs(self.m_openedPanels) do
        if not exceptPanelIds or not lume.find(exceptPanelIds, id) then
            if ctrl:GetCurPanelCfg("clearedPanel") then
                local isShowing = self:IsShow(id)
                clearedPanels[id] = true
                local clearedByOthers = self.m_clearedPanelReverseInfos[id]
                if not clearedByOthers then
                    clearedByOthers = {}
                    self.m_clearedPanelReverseInfos[id] = clearedByOthers
                end
                clearedByOthers[clearScreenKey] = true
                if isShowing then
                    table.insert(panelIds, id)
                end
            end
        end
    end

    return clearScreenKey, panelIds
end

UIManager.m_inClearScreen = HL.Field(HL.Boolean) << false

UIManager.m_curClearScreenKey = HL.Field(HL.Number) << -1

UIManager.m_waitClearPanels = HL.Field(HL.Table)

UIManager.ClearScreenWithOutAnimation = HL.Method(HL.Function, HL.Opt(HL.Table)) << function(self, callback, exceptPanelIds)
    if self.m_inClearScreen then
        logger.error("清屏中，不能额外操作")
        callback(-1)
        return
    end

    self.m_inClearScreen = true
    local clearScreenKey, panelIds = self:_GetClearScreenTargets(exceptPanelIds)
    self.m_curClearScreenKey = clearScreenKey

    logger.info("UIManager.ClearScreenWithOutAnimation", clearScreenKey)

    self.m_ignoreBlockOrderUpdate = true
    local count = #panelIds
    if count == 0 then

        TimerManager:StartTimer(0, function()
            self.m_ignoreBlockOrderUpdate = false
            self:CalcOtherSystemPropertyByPanelOrder()
            self.m_inClearScreen = false
            self.m_curClearScreenKey = -1
            callback(clearScreenKey)
        end, true, self)
        return
    end

    self.m_waitClearPanels = {}
    for _, v in ipairs(panelIds) do
        local panelId = v
        local uiCtrl = self.m_openedPanels[panelId]
        if uiCtrl ~= nil and not uiCtrl:IsPlayingAnimationOut() then









            self.m_waitClearPanels[panelId] = true
            uiCtrl:PlayAnimationOutWithCallback(function()
                self:_InternalHide(panelId)
                self.m_waitClearPanels[panelId] = nil
            end)
        end
    end

    CoroutineManager:StartCoroutine(function()
        while true do
            coroutine.step()
            local allDone = true
            for _, v in ipairs(panelIds) do
                local panelId = v
                local ctrl = self.m_openedPanels[panelId]


                if ctrl ~= nil and ctrl.view.gameObject.activeSelf and not self:IsInternalHidden(panelId) and self.m_waitClearPanels[panelId] then
                    allDone = false
                    break
                end
            end
            if allDone then
                self.m_ignoreBlockOrderUpdate = false
                self:CalcOtherSystemPropertyByPanelOrder()
                self.m_inClearScreen = false
                self.m_curClearScreenKey = -1
                callback(clearScreenKey)
                break
            end
        end
    end, self)
end

UIManager.RecoverScreen = HL.Method(HL.Number).Return(HL.Number) << function(self, clearScreenKey)
    if self.m_inClearScreen then
        if clearScreenKey == self.m_curClearScreenKey then
            logger.error("清屏中，不能RecoverScreen自己，应该是时序有问题")
            return -1
        end
    end

    if self.m_cachedRecoverScreenKeys then
        table.insert(self.m_cachedRecoverScreenKeys, clearScreenKey)
        return -1
    end

    local clearedPanels = self.m_clearedPanelInfos[clearScreenKey]
    if not clearedPanels then
        return -1
    end
    self.m_clearedPanelInfos[clearScreenKey] = nil

    self.m_ignoreBlockOrderUpdate = true
    for id, _ in pairs(clearedPanels) do
        local clearedByOthers = self.m_clearedPanelReverseInfos[id]
        if clearedByOthers then
            clearedByOthers[clearScreenKey] = nil
            if not next(clearedByOthers) then
                self.m_clearedPanelReverseInfos[id] = nil
            end
            if self:IsShow(id) then
                self:_InternalShow(id)
            end
        end
    end
    self.m_ignoreBlockOrderUpdate = false
    self:CalcOtherSystemPropertyByPanelOrder()

    logger.info("UIManager.RecoverScreen", clearScreenKey)
    return -1
end



UIManager._InitOrderManagers = HL.Method() << function(self)
    local stackClass = require_ex("Common/Utils/DataStructure/Stack")
    for _, v in pairs(Types.EPanelOrderTypes) do
        local manager = {}
        manager.stack = stackClass()
        manager.initOrder = v * self.m_panelOrderTypeInterval
        manager.maxOrder = manager.initOrder + self.m_panelOrderTypeInterval - self.m_panelOrderInterval
        manager.nextPanelOrder = manager.initOrder
        self.m_orderManagers[v] = manager
    end
end

UIManager.GetBaseOrder = HL.Method(HL.Number).Return(HL.Number) << function(self, orderType)
    return orderType * self.m_panelOrderTypeInterval
end

UIManager._AutoSetPanelOrder = HL.Method(HL.Table, HL.Boolean) << function(self, cfg, isInit)
    isInit = isInit == true
    local orderType = cfg.orderType
    local manager = self.m_orderManagers[orderType]
    if not manager then
        logger.error("No Order Manager", orderType, inspect(cfg))
        return
    end
    local stack = manager.stack

    local recalculated = false
    if manager.nextPanelOrder > manager.maxOrder then
        logger.warn("动态面板层级超出区间，开始重新计算层级", orderType, manager.nextPanelOrder, manager.maxOrder)
        recalculated = true
        manager.nextPanelOrder = manager.initOrder
        if stack:Count() > 0 then
            for i = stack:BottomIndex(), stack:TopIndex() do
                local id = stack:Get(i)
                self.m_openedPanels[id]:SetSortingOrder(manager.nextPanelOrder, isInit)
                manager.nextPanelOrder = manager.nextPanelOrder + self.m_panelOrderInterval
            end
        end
    end

    if manager.nextPanelOrder > manager.maxOrder then
        logger.error("动态面板层级超出区间", orderType, manager.nextPanelOrder, manager.maxOrder, inspect(stack))
    end

    cfg.ctrl:SetSortingOrder(manager.nextPanelOrder, isInit)
    manager.nextPanelOrder = manager.nextPanelOrder + self.m_panelOrderInterval
    stack:Push(cfg.id)
    self:CalcOtherSystemPropertyByPanelOrder()

    if recalculated then
        Notify(MessageConst.ON_PANEL_ORDER_RECALCULATED)
    end
end

UIManager._RemoveFromOrderStack = HL.Method(HL.Number) << function(self, panelId)
    local cfg = self.m_panelConfigs[panelId]
    if cfg.selfMaintainOrder then
        return
    end
    local manager = self.m_orderManagers[cfg.orderType]
    local stack = manager.stack
    if stack:Peek() == panelId then
        manager.nextPanelOrder = manager.nextPanelOrder - self.m_panelOrderInterval
    end
    stack:Delete(panelId)
end

UIManager.SetTopOrder = HL.Method(HL.Number) << function(self, panelId)
    if not self:IsOpen(panelId) then
        logger.error("Panel Not Open", panelId)
        return
    end

    local cfg = self.m_panelConfigs[panelId]
    if cfg.selfMaintainOrder then
        logger.error("Panel is selfMaintainOrder, can't SetTopOrder")
    end

    self:_RemoveFromOrderStack(panelId)
    self:_AutoSetPanelOrder(cfg, false)
end





UIManager.CalcOtherSystemPropertyByPanelOrder = HL.Method() << function(self)
    if self.m_ignoreBlockOrderUpdate or self.m_isClosingAll then
        return
    end


    self.m_curBlockKeyboardEventPanelOrder = self:_FindTopPanelProperty(function(cfg, ctrl)
        if ctrl:GetBlockKeyboardEvent() then
            return ctrl:GetSortingOrder()
        end
    end) or -1
    for _, type in pairs(Types.EPanelOrderTypes) do
        local manager = self.m_orderManagers[type]
        local stack = manager.stack
        if stack:Count() > 0 then
            local topIndex, bottomIndex = stack:TopIndex(), stack:BottomIndex()

            local updatePanelList = {}
            for i = bottomIndex, topIndex do
                updatePanelList[i] = stack:Get(i)
            end
            for _, panelId in ipairs(updatePanelList) do
                local ctrl = self.m_openedPanels[panelId]
                ctrl:UpdateInputGroupState()
            end
        end
    end
    for _, ctrl in pairs(self.m_selfMaintainOrderPanels) do
        ctrl:UpdateInputGroupState()
    end


    local multiTouchType, topPanelOrder, targetCtrl = self:_FindTopPanelProperty(function(cfg, ctrl)
        local t = ctrl:GetMultiTouchType()
        if t and t ~= Types.EPanelMultiTouchTypes.Both then
            return t
        end
    end) or Types.EPanelMultiTouchTypes.Enable

    if multiTouchType ~= Types.EPanelMultiTouchTypes.Both then
        local enable = multiTouchType == Types.EPanelMultiTouchTypes.Enable
        if InputManager.multiTouchEnabled ~= enable then
            InputManager.multiTouchEnabled = enable
        end
    end


    local cursorCfgName = DeviceInfo.usingController and "virtualMouseMode" or "realMouseMode"
    local cursorMode = self:_FindTopPanelProperty(function(cfg, ctrl)
        local mode = ctrl:GetCurPanelCfg(cursorCfgName)
        if mode ~= Types.EPanelMouseMode.NotNeedShow then
            return mode
        end
    end) or Types.EPanelMouseMode.NotNeedShow
    InputManagerInst:SetCursorShowRequest("blocked", cursorMode == Types.EPanelMouseMode.NeedShow)
    self:_ToggleAutoShowCursor(cursorMode == Types.EPanelMouseMode.AutoShow)
    if InputManagerInst.virtualMouse then
        if cursorMode == Types.EPanelMouseMode.NotNeedShow then
            InputManagerInst.virtualMouse.keepMousePosOnEnable = false
        elseif cursorMode == Types.EPanelMouseMode.ForceHide then
            InputManagerInst.virtualMouse.keepMousePosOnEnable = true
        end
    end


    local gyroscopeEffectType = self:_FindTopPanelProperty(function(cfg, ctrl)
        local t = ctrl:GetCurPanelCfg("gyroscopeEffect")
        local isWorldUI = ctrl:GetCurPanelCfg("isWorldUI")
        if isWorldUI and t and t ~= Types.EPanelGyroscopeEffect.Both then
            return t
        end
    end) or Types.EPanelGyroscopeEffect.Disable
    if gyroscopeEffectType ~= Types.EPanelGyroscopeEffect.Both then
        local enable = gyroscopeEffectType == Types.EPanelGyroscopeEffect.Enable
        self.gyroscopeEffect.enableDetect = enable
    end


    local pauseMobileFrameGen = self:_FindTopPanelProperty(function(cfg, ctrl)
        if ctrl:GetCurPanelCfg("pauseMobileFrameGen") then
            return true
        end
    end) or false
    if pauseMobileFrameGen ~= self.m_pauseMobileFrameGen then
        self.m_pauseMobileFrameGen = pauseMobileFrameGen
        HGRenderBridgeStatics.PauseMobileFrameGenTemporarily(pauseMobileFrameGen)
    end

    Notify(MessageConst.ON_BLOCK_KEYBOARD_EVENT_PANEL_ORDER_CHANGED)
end

UIManager.CurBlockKeyboardEventPanelOrder = HL.Method().Return(HL.Number) << function(self)
    return self.m_curBlockKeyboardEventPanelOrder
end

UIManager.GetPanelOrder = HL.Method(HL.Number).Return(HL.Opt(HL.Number)) << function(self, panelId)
    local ctrl = self.m_openedPanels[panelId]
    if not ctrl then
        return
    end
    return ctrl:GetSortingOrder()
end

UIManager._FindTopPanelProperty = HL.Method(HL.Function).Return(HL.Opt(HL.Any, HL.Number, HL.Forward("UICtrl"))) << function(self, checkFunc)
    local topPanelOrder, result, targetCtrl = -1, nil, nil

    for type = Types.MaxPanelOrderType, 1, -1 do
        local manager = self.m_orderManagers[type]
        local stack = manager.stack
        if stack:Count() > 0 then
            local topIndex, bottomIndex = stack:TopIndex(), stack:BottomIndex()
            for i = topIndex, bottomIndex, -1 do
                local id = stack:Get(i)
                if self:IsShow(id) then
                    local cfg = self.m_panelConfigs[id]
                    local ctrl = cfg.ctrl
                    local value = checkFunc(cfg, ctrl)
                    if value ~= nil then
                        topPanelOrder = ctrl:GetSortingOrder()
                        result = value
                        targetCtrl = ctrl
                        break
                    end
                end
            end
        end
        if result then
            break
        end
    end



    for id, ctrl in pairs(self.m_selfMaintainOrderPanels) do
        if self:IsShow(id) then
            local cfg = self.m_panelConfigs[id]
            local value = checkFunc(cfg, ctrl)
            local order = ctrl:GetSortingOrder()
            if value ~= nil and order > topPanelOrder then
                topPanelOrder = order
                result = value
                targetCtrl = ctrl
            end
        end
    end

    return result, topPanelOrder, targetCtrl
end






UIManager.m_isClosingAll = HL.Field(HL.Boolean) << false

UIManager.m_cachedRecoverScreenKeys = HL.Field(HL.Table)






UIManager.StartCacheRecoverScreen = HL.Method() << function(self)
    if self.m_cachedRecoverScreenKeys then
        return
    end
    logger.info("UIManager.StartCacheRecoverScreen")
    self.m_cachedRecoverScreenKeys = {}
end

UIManager.EndCacheRecoverScreen = HL.Method() << function(self)
    if not self.m_cachedRecoverScreenKeys then
        return
    end
    local keys = self.m_cachedRecoverScreenKeys
    self.m_cachedRecoverScreenKeys = nil
    logger.info("UIManager.EndCacheRecoverScreen", keys)
    for _, key in ipairs(keys) do
        self:RecoverScreen(key)
    end
end

UIManager._CloseAllUI = HL.Method(HL.Boolean, HL.Opt(HL.Table)) << function(self, isChangeScene, exceptPanelIds)
    local exceptPanelIdMap = {}
    if exceptPanelIds then
        for _, id in ipairs(exceptPanelIds) do
            exceptPanelIdMap[id] = true
        end
    end
    self:_RealCloseAllUI(function(k, v)
        return self:IsOpen(k) and not exceptPanelIdMap[k] and (not isChangeScene or v.closeWhenChangeScene)
    end)
end

UIManager._RealCloseAllUI = HL.Method(HL.Function) << function(self, checkCanClose)
    self.m_isClosingAll = true
    self:StartCacheRecoverScreen()

    for k, v in pairs(self.m_panelConfigs) do
        if checkCanClose(k, v) then
            local succ, log = xpcall(self.Close, debug.traceback, self, k)
            if not succ then
                logger.critical("_CloseAllUI Fail", v.name, log)
            end

        end
    end

    self.m_isClosingAll = false
    self:CalcOtherSystemPropertyByPanelOrder()
    self:EndCacheRecoverScreen()


    if not self.m_isHotSwitching then
        self:ReleaseCachedPanelAsset()
    end
end


UIManager._CloseAllUIForSwitchLanguage = HL.Method() << function(self)
    self:_RealCloseAllUI(function(k, v)
        return self:IsOpen(k) and not v.preserveWhenChangeLanguage
    end)
end

UIManager.CloseAllUI = HL.Method(HL.Boolean, HL.Opt(HL.Table)) << function(self, isChangeScene, exceptPanelIds)
    self:_CloseAllUI(isChangeScene, exceptPanelIds)
end

UIManager.SetUICameraFOV = HL.Method(HL.Number) << function(self, fov)
    if self.uiCamera then
        self.m_currentStandardFOV = fov

        local referenceAspect = CS.Beyond.UI.UIConst.STANDARD_RATIO
        local currentAspect = Screen.width / Screen.height;
        if currentAspect < referenceAspect then
            local hFOV = Unity.Camera.VerticalToHorizontalFieldOfView(fov, referenceAspect);
            local vFOV = Unity.Camera.HorizontalToVerticalFieldOfView(hFOV, currentAspect);
            fov = vFOV;
        end

        self.uiCamera.fieldOfView = fov

        self.m_worldUICanvasScaleHelper:UpdateCanvas()
    end
end

UIManager.ResetUICameraFOV = HL.Method() << function(self)
    if self.uiCamera then
        self:SetUICameraFOV(self.m_uiCameraDefaultFov)
    end
end

UIManager.GetUICameraFOV = HL.Method().Return(HL.Number) << function(self)
    if self.uiCamera then
        return self.uiCamera.fieldOfView
    else
        return -1
    end
end

UIManager.RestoreShownPanelVisibilityIfNeeded = HL.Method(HL.Number) << function(self, panelId)
    local ctrl = self.m_openedPanels[panelId]
    if ctrl == nil or not self:IsShow(panelId) then
        return
    end

    local cfg = ctrl.panelCfg
    local needRestore = false
    if cfg.useCanvasHide then
        needRestore = not ctrl.view.gameObject.activeSelf or
            not ctrl.view.panelCanvas.enabled or
            not ctrl.view.luaPanel.enabled
    else
        needRestore = not ctrl.view.gameObject.activeSelf
    end

    if not needRestore then
        return
    end

    self:_RestorePanelVisibleComponents(ctrl, false)

    local luaPanel = ctrl.view.luaPanel
    if not luaPanel.blockWhileAnim or luaPanel.animationInFinished then
        luaPanel:RecoverAllInput()
    end
end




UIManager.m_rootInfos = HL.Field(HL.Table)

UIManager.m_panel2RootDic = HL.Field(HL.Table)

UIManager._InitRoots = HL.Method() << function(self)
    self.m_rootInfos = {}
    self.m_panel2RootDic = {}
    for name, cfg in pairs(rootConfig.config) do
        local info = {
            name = name,
            cfg = cfg,
        }

        local assetPath = string.format(UIConst.UI_ROOT_PREFAB_PATH, cfg.folder, name)
        local asset = self.m_resourceLoader:LoadGameObject(assetPath)
        local rootObj = CSUtils.CreateObject(asset, self.uiRoot)
        rootObj.name = string.format("%sRoot", name)

        info.gameObject = rootObj
        info.transform = rootObj.transform
        info.luaUIRoot = rootObj:GetComponent("LuaUIRoot")
        info.rectTransform = rootObj:GetComponent("RectTransform")

        info.rectTransform.localScale = Vector3.one
        info.rectTransform.pivot = Vector2.one / 2
        info.rectTransform.anchorMin = Vector2.zero
        info.rectTransform.anchorMax = Vector2.one
        info.rectTransform.offsetMin = Vector2.zero
        info.rectTransform.offsetMax = Vector2.zero

        self.m_rootInfos[name] = info

        for panelName, _ in pairs(info.luaUIRoot.nodeDic.data) do
            if self.m_panel2RootDic[panelName] then
                logger.error("面板已经被其他Root使用", panelName, self.m_panel2RootDic[panelName].name)
            else
                self.m_panel2RootDic[panelName] = info
            end
        end
    end
end

UIManager._GetPanelParentTransform = HL.Method(HL.Table).Return(Transform) << function(self, panelCfg)
    if panelCfg.isWorldUI then
        return self.worldUIRoot
    end

    local name = panelCfg.name
    local rootInfo = self.m_panel2RootDic[name]
    if rootInfo then
        return rootInfo.luaUIRoot.nodeDic:get_Item(name).transform
    end

    return self.uiRoot
end



UIManager.Dispose = HL.Method() << function(self)
    CS.Beyond.UI.LuaPanel.s_openedLuaPanels:Clear()
    self:ResetUICameraFOV()
    self.m_worldUICanvasScaleHelper.onScreenSizeChanged:RemoveListener(self.m_onScreenSizeChangedCallback)
    self.m_uiCanvasScaleHelper.onCanvasChanged:RemoveListener(self.m_onCanvasChangedCallback)
    TimerManager:ClearAllTimer(self)
    self:_CloseAllUI(false)
    self:OnToggleUiAction({ true })
    InputManagerInst:DeleteGroup(self.persistentInputBindingKey)


    InputManagerInst.controllerNaviManager:ResetStateForUIDispose()
    if self.uiNode ~= nil then
        GameObject.DestroyImmediate(self.uiNode.gameObject)
        self.uiNode = nil
    end
    if self.worldObjectRoot ~= nil then
        GameObject.DestroyImmediate(self.worldObjectRoot.gameObject)
        self.worldObjectRoot = nil
    end
    CS.Beyond.UI.UIRedDotAnimation.instance:UnInit()
    self.m_resourceLoader:DisposeAllHandles()
    if CS.TMPro.TMP_Settings.instance.dynamicFontAssetLoader ~= nil then
        CS.TMPro.TMP_Settings.instance.dynamicFontAssetLoader:Dispose()
        CS.TMPro.TMP_Settings.instance.dynamicFontAssetLoader = nil
    end
    CS.Beyond.UI.LuaPanel.s_openedLuaPanels:Clear()
end

UIManager._ToggleUIInputBinding = HL.Method(HL.Boolean, HL.String) << function(self, active, key)
    if active then
        self.m_blockInputKeys[key] = nil
    else
        self.m_blockInputKeys[key] = true
    end
    local rst = next(self.m_blockInputKeys) == nil
    InputManagerInst:ToggleGroup(self.uiInputBindingGroupMonoTarget.groupId, rst)
    logger.info("UIManager._ToggleUIInputBinding", active, key, "CUR RESULT", rst)
end




UIManager.m_autoShowCursorCor = HL.Field(HL.Thread)

UIManager._ToggleAutoShowCursor = HL.Method(HL.Boolean) << function(self, active)
    if not active then
        if self.m_autoShowCursorCor then
            logger.info("UIManager._ToggleAutoShowCursor AutoShowCursor", false)
            InputManagerInst:SetCursorShowRequest("AutoShowCursor", false)
            CoroutineManager:ClearCoroutine(self.m_autoShowCursorCor)
            self.m_autoShowCursorCor = nil
        end
        return
    end
    if self.m_autoShowCursorCor then
        return
    end

    local nextHideTime
    self.m_autoShowCursorCor = CoroutineManager:StartCoroutine(function()
        while true do
            coroutine.step()
            local needShow = Input.anyKeyDown
            if needShow then
                logger.info("UIManager._ToggleAutoShowCursor AutoShowCursor", true)
                InputManagerInst:SetCursorShowRequest("AutoShowCursor", true)
                nextHideTime = Time.unscaledTime + 5
            else
                if nextHideTime and Time.unscaledTime >= nextHideTime then
                    logger.info("UIManager._ToggleAutoShowCursor AutoShowCursor", false)
                    InputManagerInst:SetCursorShowRequest("AutoShowCursor", false)
                    nextHideTime = nil
                end
            end
        end
    end, self)
end




UIManager.m_blockObtainWaysJumpKeys = HL.Field(HL.Table)

UIManager.ToggleBlockObtainWaysJump = HL.Method(HL.String, HL.Boolean, HL.Opt(HL.Table)) << function(self, name, shouldBlock, blockPhaseWhiteMap)
    if shouldBlock then
        if blockPhaseWhiteMap == nil then
            blockPhaseWhiteMap = {}
        end
        self.m_blockObtainWaysJumpKeys[name] = blockPhaseWhiteMap
    else
        self.m_blockObtainWaysJumpKeys[name] = nil
    end
end

UIManager.ShouldBlockObtainWaysPhaseJump = HL.Method(HL.Any).Return(HL.Boolean) << function(self, phaseId)
    if phaseId == nil then
        phaseId = -1
    end

    for _, whiteMap in pairs(self.m_blockObtainWaysJumpKeys) do
        local subRes = true
        for _, whitePhaseName in pairs(whiteMap) do
            if PhaseManager:GetPhaseIdByName(whitePhaseName) == phaseId then
                subRes = false
            end
        end
        if subRes then
            return true
        end
    end
    return false
end

UIManager.ShouldBlockObtainWaysJump = HL.Method().Return(HL.Boolean) << function(self)
    return next(self.m_blockObtainWaysJumpKeys) ~= nil
end

UIManager.ShouldBlockAllObtainWaysJump = HL.Method().Return(HL.Boolean) << function(self)
    for _, v in pairs(self.m_blockObtainWaysJumpKeys) do
        if v ~= nil and #v == 0 then
            return true
        end
    end
    return false
end

UIManager.GetOpenedPanels = HL.Method().Return(HL.Number) << function(self)
    local count = 0
    for _, __ in pairs(self.m_openedPanels) do
        count = count + 1
    end
    return count
end

UIManager.GetHidedPanels = HL.Method().Return(HL.Number) << function(self)
    local count = 0
    for _, __ in pairs(self.m_hidedPanels) do
        count = count + 1
    end
    return count
end


UIManager.IsInFullScreenUI = HL.Method().Return(HL.Boolean) << function(self)
    for id, ctrl in pairs(self.m_openedPanels) do
        if self:IsShow(id) then
            if ctrl:GetCurPanelCfg("hideCamera") then
                return true
            end
        end
    end
    return false
end




UIManager.m_recentClosedPanelIds = HL.Field(HL.Table)

UIManager.m_panelAssetLRUCapacity = HL.Field(HL.Number) << 5

UIManager._UpdatePanelAssetLRU = HL.Method(HL.Number, HL.Boolean) << function(self, panelId, isOpen)
    if self.m_isClosingAll then
        return
    end
    if isOpen then

        self:_RemoveFromPanelAssetLRU(panelId)
        return
    end

    local count = #self.m_recentClosedPanelIds
    if self.m_recentClosedPanelIds[count] == panelId then

        return
    end
    local index
    for k, v in ipairs(self.m_recentClosedPanelIds) do
        if v == panelId then
            index = k
            break
        end
    end
    if not index then

        if count >= self.m_panelAssetLRUCapacity then

            local removedCount = count - self.m_panelAssetLRUCapacity + 1
            for k = 1, count do
                if k <= removedCount then
                    local removedPanelId = self.m_recentClosedPanelIds[k]
                    local assetInfo = self.m_panel2Handle[removedPanelId]
                    if assetInfo then
                        self.m_panel2Handle[removedPanelId] = nil
                        self.m_resourceLoader:DisposeHandleByKey(assetInfo[1])
                        logger.info("UIManager._UpdatePanelAssetLRU DisposeHandleByKey", removedPanelId, self.m_names[removedPanelId])
                    end
                end
                self.m_recentClosedPanelIds[k] = self.m_recentClosedPanelIds[k + removedCount]
            end
        end
        if self.m_panelAssetLRUCapacity > 0 then

            table.insert(self.m_recentClosedPanelIds, panelId)
        else
            local assetInfo = self.m_panel2Handle[panelId]
            if assetInfo then
                self.m_panel2Handle[panelId] = nil
                self.m_resourceLoader:DisposeHandleByKey(assetInfo[1])
                logger.info("UIManager._UpdatePanelAssetLRU DisposeHandleByKey", panelId, self.m_names[panelId])
            end
        end
    else

        for k = index, count - 1 do
            self.m_recentClosedPanelIds[k] = self.m_recentClosedPanelIds[k + 1]
        end
        self.m_recentClosedPanelIds[count] = panelId
    end








end

UIManager.ReleaseCachedPanelAsset = HL.Method() << function(self)

    for panelId, assetInfo in pairs(self.m_panel2Handle) do
        if not self:IsOpen(panelId) then
            self.m_panel2Handle[panelId] = nil
            local assetKey = assetInfo[1]
            self.m_resourceLoader:DisposeHandleByKey(assetKey)
        end
    end
    self.m_recentClosedPanelIds = {}
end

UIManager._RemoveFromPanelAssetLRU = HL.Method(HL.Number) << function(self, panelId)
    local index
    for k, v in ipairs(self.m_recentClosedPanelIds) do
        if v == panelId then
            index = k
            break
        end
    end
    if index then
        table.remove(self.m_recentClosedPanelIds, index)
    end
end

UIManager._OnScreenSizeChanged = HL.Method() << function(self)
    self:SetUICameraFOV(self.m_currentStandardFOV)
end




UIManager.OnToggleUiAction = HL.Method(HL.Table) << function(self, arg)
    local isShow = unpack(arg)
    if isShow then
        CameraManager:RemoveUICamCullingMaskConfig("UIManager")
        self:_ToggleUIInputBinding(true, "TOGGLE_UI_ACTION")
    else
        CameraManager:AddUICamCullingMaskConfig("UIManager", UIConst.LAYERS.Nothing)
        self:_ToggleUIInputBinding(false, "TOGGLE_UI_ACTION")
    end

    Notify(MessageConst.AFTER_TOGGLE_UI_ACTION, arg)
end





UIManager.Dump = HL.Method().Return(HL.String) << function(self)
    return self:GetCurPanelDebugInfo()
end

UIManager.GetCurPanelDebugInfo = HL.Method(HL.Opt(HL.Table)).Return(HL.String) << function(self, extraFieldInfo)
    local extraFieldTitle = extraFieldInfo == nil and "" or string.format("%s\t", extraFieldInfo.extraFieldTitle)
    local infos = { "\nPanelId\tIsShow\tSortingOrder\t" .. extraFieldTitle .. "PanelName\n" }
    for type = Types.MaxPanelOrderType, 1, -1 do
        local manager = self.m_orderManagers[type]
        local stack = manager.stack
        if stack:Count() > 0 then
            table.insert(infos, "----------------Layer " .. type .. "----------------")
            local topIndex, bottomIndex = stack:TopIndex(), stack:BottomIndex()
            for i = topIndex, bottomIndex, -1 do
                local id = stack:Get(i)
                local cfg = self.m_panelConfigs[id]
                local ctrl = cfg.ctrl
                local extraField = ""
                if extraFieldInfo ~= nil then
                    extraField = extraFieldInfo.extraFieldGetFunc(id, ctrl) .. "\t"
                end

                table.insert(infos, string.format("%d\t%s\t%d\t%s\t%s",
                    id,
                    self:IsShow(id),
                    ctrl:GetSortingOrder(),
                    extraField,
                    cfg.name
                ))
            end
        end
    end
    return table.concat(infos, "\n")
end

UIManager.GetCurPanelDebugInfoForBlockOtherInput = HL.Method().Return(HL.String) << function(self)
    return self:GetCurPanelDebugInfo({
        extraFieldTitle = "<color=white>BlockOtherInput</color>",
        extraFieldGetFunc = function(id, ctrl)
            local isShow = self:IsShow(id)
            local warningFormat = "<color=yellow>%s</color>"
            local normalFormat = "<color=white>%s</color>"
            local blockKeyboardEvent = ctrl:GetBlockKeyboardEvent()
            return (blockKeyboardEvent and isShow) and
                string.format(warningFormat, blockKeyboardEvent) or
                string.format(normalFormat, blockKeyboardEvent)
        end
    })
end

UIManager.GetCurPanelDebugInfoForCursorVisible = HL.Method().Return(HL.String) << function(self)
    return self:GetCurPanelDebugInfo({
        extraFieldTitle = "<color=white>CursorVisible</color>",
        extraFieldGetFunc = function(id, ctrl)
            local isShow = self:IsShow(id)
            local cursorCfgName = DeviceInfo.usingController and "virtualMouseMode" or "realMouseMode"
            local mode = ctrl:GetCurPanelCfg(cursorCfgName)
            if mode == Types.EPanelMouseMode.NeedShow then
                return isShow and "<color=yellow>ForceShow</color>" or "<color=white>ForceShow</color>"
            elseif mode == Types.EPanelMouseMode.ForceHide then
                return isShow and "<color=green>ForceHide</color>" or "<color=white>ForceHide</color>"
            else
                return "<color=white>Optional</color>"
            end
        end
    })
end

UIManager.GetCurPanelDebugInfoForMultiTouch = HL.Method().Return(HL.String) << function(self)
    return self:GetCurPanelDebugInfo({
        extraFieldTitle = "<color=white>MultiTouch</color>",
        extraFieldGetFunc = function(id, ctrl)
            local isShow = self:IsShow(id)
            local type = ctrl:GetMultiTouchType()
            if type == Types.EPanelMultiTouchTypes.Enable then
                return isShow and "<color=yellow>ForceEnable</color>" or "<color=white>ForceEnable</color>"
            elseif type == Types.EPanelMultiTouchTypes.Disable then
                return isShow and "<color=green>ForceDisable</color>" or "<color=white>ForceDisable</color>"
            else
                return "<color=white>Optional</color>"
            end
        end
    })
end

UIManager.GetCurPanelDebugInfoForCustomField = HL.Method(HL.String).Return(HL.String) << function(self, fieldName)
    return self:GetCurPanelDebugInfo({
        extraFieldTitle = "<color=white>" .. fieldName .. "</color>",
        extraFieldGetFunc = function(id, ctrl)
            local fieldValue = ctrl:GetCurPanelCfg(fieldName)
            return string.format("<color=white>%s</color>", fieldValue)
        end
    })
end

UIManager.UpdateUICanvasScaleHelpers = HL.Method() << function(self)
    logger.info("UIManager.UpdateUICanvasScaleHelpers")
    for _, scaleHelper in ipairs({ self.m_uiCanvasScaleHelper, self.m_worldUICanvasScaleHelper }) do
        local res = scaleHelper:GetProperCanvasResolution()
        for _, canvasScaler in pairs(scaleHelper.canvasScalerList) do
            canvasScaler.referenceResolution = res
        end
        scaleHelper:UpdateCanvas()
    end
end

UIManager.CreateNaviDummLayerObj = HL.Method().Return(GameObject) << function(self)
    return CSUtils.CreateObject(self.uiDummyNaviLayerAsset, self.uiDummyNaviLayerRoot)
end

UIManager._TryAttachNaviDummyLayer = HL.Method(HL.Number) << function(self, panelId)
    local cfg = self.m_panelConfigs[panelId]
    if cfg.needNaviDummyLayer ~= true then
        return
    end
    Notify(MessageConst.ATTACH_DUMMY_NAVI_LAYER, cfg.name)
end

UIManager._TryDetachNaviDummyLayer = HL.Method(HL.Number) << function(self, panelId)
    local panelName = self.m_names[panelId]
    Notify(MessageConst.DETACH_DUMMY_NAVI_LAYER, panelName)
end


UIManager._RefreshOpenedPanelsCustomUIStyleBindings = HL.Method() << function(self)
    for _, name in pairs(InputDeviceChangeConst.EXCEPT_CHANGE_DEVICE_CLOSE_PANEL) do
        local panelId = self.ids[name]
        local ctrl = panelId and self.m_openedPanels[panelId]
        if ctrl then
            local luaRef = ctrl.view.gameObject:GetComponent("LuaReference")
            if luaRef and luaRef:RefreshCustomUIStyleBindings() then
                ctrl:OnCustomUIStyleRefChanged()
            end
        end
    end
end

HL.Commit(UIManager)
return UIManager
