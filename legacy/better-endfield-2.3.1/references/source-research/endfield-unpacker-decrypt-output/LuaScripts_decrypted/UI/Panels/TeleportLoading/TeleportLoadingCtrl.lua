local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.TeleportLoading

TeleportLoadingCtrl = HL.Class('TeleportLoadingCtrl', uiCtrl.UICtrl)






TeleportLoadingCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CLOSE_LOADING_PANEL] = 'CloseLoadingPanel',
    [MessageConst.START_CAMERA_RENDER_IN_LOADING] = 'StartCameraRenderInLoading',
}

TeleportLoadingCtrl.m_isClosing = HL.Field(HL.Boolean) << false


TeleportLoadingCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if UNITY_EDITOR then
        if CS.Beyond.GameApp.instance.quickStart then

            CameraManager.uiCamera.enabled = true
        end
    end
end

TeleportLoadingCtrl.OnClose = HL.Override() << function(self)

    UIManager:RemoveMainCameraTempRequest("TeleportLoading")
end

TeleportLoadingCtrl.OpenTeleportLoadingPanel = HL.StaticMethod(HL.Opt(HL.Table)) << function(args)
    local isShowing = UIManager:IsShow(PANEL_ID)
    local self = UIManager:AutoOpen(PANEL_ID)
    Notify(MessageConst.ON_TELEPORT_LOADING_PANEL_OPENED)
    self.m_isClosing = false

    self:_Init(args)
    if not isShowing then
        GameInstance.SetBurstMode(false, GameInstance.EBurstModeReason.TeleportLoadingUI)
        self:_StartTimer(0.5, function()

            if UIManager:IsShow(PANEL_ID) and not self:IsPlayingAnimationOut() and not self.m_isClosing then
                GameInstance.SetBurstMode(true, GameInstance.EBurstModeReason.TeleportLoadingUI)
            end
        end)
    end
end

TeleportLoadingCtrl._Init = HL.Method(HL.Opt(HL.Table)) << function(self, args)

    local uiType = unpack(args)
    self.view.black.gameObject:SetActive(uiType == CS.Beyond.Gameplay.TeleportUIType.Black)
    self.view.white.gameObject:SetActive(uiType == CS.Beyond.Gameplay.TeleportUIType.White)
end

TeleportLoadingCtrl.CloseLoadingPanel = HL.Method() << function(self)
    self:_TryCloseLoading()
end

TeleportLoadingCtrl._TryCloseLoading = HL.Method() << function(self)
    if self.m_isClosing then
        return
    end

    GameInstance.SetBurstMode(false, GameInstance.EBurstModeReason.TeleportLoadingUI)
    self.m_isClosing = true
    self:_StartCoroutine(function()
        coroutine.step()
        if not self.m_isClosing then
            return
        end
        if not self:IsPlayingAnimationOut() then
            self:PlayAnimationOutWithCallback(function()
                Notify(MessageConst.ON_TELEPORT_LOADING_PANEL_CLOSED)
                self:Close()
            end)
        end
    end)
end

TeleportLoadingCtrl.StartCameraRenderInLoading = HL.Method() << function(self)
    logger.info("TeleportLoadingCtrl.StartCameraRenderInLoading")
    UIManager:AddMainCameraTempRequest("TeleportLoading")
end

HL.Commit(TeleportLoadingCtrl)
