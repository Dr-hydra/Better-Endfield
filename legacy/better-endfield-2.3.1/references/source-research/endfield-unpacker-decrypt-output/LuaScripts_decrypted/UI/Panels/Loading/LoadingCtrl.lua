local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Loading
LoadingCtrl = HL.Class('LoadingCtrl', uiCtrl.UICtrl)







LoadingCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CLOSE_LOADING_PANEL] = 'CloseLoadingPanel',
    [MessageConst.ADD_LOADING_SYSTEM] = 'AddLoadingSystem',
    [MessageConst.REMOVE_LOADING_SYSTEM] = 'RemoveLoadingSystem',
    [MessageConst.START_CAMERA_RENDER_IN_LOADING] = 'StartCameraRenderInLoading',
}


LoadingCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local enableDebug = (BEYOND_DEBUG or BEYOND_DEBUG_COMMAND) and CS.Beyond.Cfg.RemoteGameCfg.instance.data.enableDebugInfo
    self.view.debugNode.gameObject:SetActiveIfNecessary(enableDebug)
    self.view.logoImage:LoadSprite("Loading/deco_loading_txtlo")

    if UNITY_EDITOR then
        if CS.Beyond.GameApp.instance.quickStart then

            CameraManager.uiCamera.enabled = true
        end
    end
end

LoadingCtrl.OnClose = HL.Override() << function(self)

    UIManager:RemoveMainCameraTempRequest("Loading")
    self.view.bgImg:ReleaseSprite()
end

LoadingCtrl.OpenLoadingPanel = HL.StaticMethod(HL.Opt(HL.Table)) << function(args)
    local isShowing = UIManager:IsShow(PANEL_ID)

    local self = UIManager:AutoOpen(PANEL_ID)
    Notify(MessageConst.ON_LOADING_PANEL_OPENED)
    self.m_isClosing = false

    self:_Init(args)
    if not isShowing then
        GameInstance.SetBurstMode(false, GameInstance.EBurstModeReason.LoadingUI)
        self:_StartTimer(0.5, function()

            if UIManager:IsShow(PANEL_ID) and not self:IsPlayingAnimationOut() and not self.m_isClosing then
                GameInstance.SetBurstMode(true, GameInstance.EBurstModeReason.LoadingUI)
            end
        end)
    end
end

LoadingCtrl.m_extraLoadingSystems = HL.Field(HL.Table)

LoadingCtrl._Init = HL.Method(HL.Opt(HL.Table)) << function(self, args)
    self.m_extraLoadingSystems = {}

    local tipKey, bgName = unpack(args)
    local succ, tipCfg = Tables.loadingTipsTable:TryGetValue(tipKey)
    local tipsText = succ and tipCfg.text or tipKey
    local titleText = succ and tipCfg.tipsTitle or tipKey
    self.view.debugTxt.text = tipKey

    self.view.titleTxt.text = titleText
    self.view.tipsTxt:SetAndResolveTextStyle(tipsText)
    self.view.bgImg:LoadSprite(UIConst.UI_LOADING_BG, bgName)
    self.view.maskImg:LoadSprite(UIConst.UI_LOADING_BG, bgName .. "_bg")

    self.view.progressBar.value = 0
    self:_StartCoroutine(function()
        while true do
            coroutine.step()
            self:_Update()
        end
    end)

end

LoadingCtrl.CloseLoadingPanel = HL.Method() << function(self)
    self:_TryCloseLoading()
end

LoadingCtrl.AddLoadingSystem = HL.Method(HL.Table) << function(self, args)
    local sysName = unpack(args)
    self.m_extraLoadingSystems[sysName] = true
end

LoadingCtrl.RemoveLoadingSystem = HL.Method(HL.Table) << function(self, args)
    local sysName = unpack(args)
    self.m_extraLoadingSystems[sysName] = nil
    self:_TryCloseLoading()
end

LoadingCtrl.m_isClosing = HL.Field(HL.Boolean) << false

LoadingCtrl._TryCloseLoading = HL.Method() << function(self)
    if next(self.m_extraLoadingSystems) or self.m_isClosing then
        return
    end
    GameInstance.SetBurstMode(false, GameInstance.EBurstModeReason.LoadingUI)
    self.m_isClosing = true
    self:_StartCoroutine(function()
        coroutine.step()
        if not self.m_isClosing then
            return
        end
        if not self:IsPlayingAnimationOut() then
            self:PlayAnimationOutWithCallback(function()
                Notify(MessageConst.ON_LOADING_PANEL_CLOSED)
                self:Close()
                logger.info("LoadingCtrl._TryCloseLoading Closed")
            end)
        end
    end)
end

LoadingCtrl._Update = HL.Method() << function(self)
    self.view.progressBar.value = GameWorld.levelLoader.progress
end

LoadingCtrl.StartCameraRenderInLoading = HL.Method() << function(self)
    logger.info("LoadingCtrl.StartCameraRenderInLoading")
    UIManager:AddMainCameraTempRequest("Loading")
end

HL.Commit(LoadingCtrl)
