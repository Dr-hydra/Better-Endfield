
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DebugMarketingCamera

DebugMarketingCameraCtrl = HL.Class('DebugMarketingCameraCtrl', uiCtrl.UICtrl)






DebugMarketingCameraCtrl.s_messages = HL.StaticField(HL.Table) << {

}


DebugMarketingCameraCtrl.m_camController = HL.Field(CS.Beyond.Gameplay.View.MarketingCameraController)



DebugMarketingCameraCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    InputManagerInst.enableController = false
    InputManagerInst.enableMarketingCamera = true
    InputManagerInst:CheckUsingController()

    CameraUtils:OpenMarketingCamera()

    self:_StartUpdate(function()
        self:_Update()
    end)

    self:BindInputPlayerAction("marketing_cam_offset_down", function()
        logger.info("MarketingCam: marketing_cam_offset_down")
        self.m_camController:SetOffsetInputY(-1)
    end)
    self:BindInputPlayerAction("marketing_cam_offset_up", function()
        logger.info("MarketingCam: marketing_cam_offset_up")
        self.m_camController:SetOffsetInputY(1)
    end)
    self:BindInputPlayerAction("marketing_cam_offset_left", function()
        logger.info("MarketingCam: marketing_cam_offset_left")
        self.m_camController:SetOffsetInputX(-1)
    end)
    self:BindInputPlayerAction("marketing_cam_offset_right", function()
        logger.info("MarketingCam: marketing_cam_offset_right")
        self.m_camController:SetOffsetInputX(1)
    end)
    self:BindInputPlayerAction("marketing_cam_rot_spd_up", function()
        logger.info("MarketingCam: marketing_cam_rot_spd_up")
        self.m_camController:ChangeRotationSpeed(1)
    end)
    self:BindInputPlayerAction("marketing_cam_rot_spd_down", function()
        logger.info("MarketingCam: marketing_cam_rot_spd_down")
        self.m_camController:ChangeRotationSpeed(-1)
    end)
    self:BindInputPlayerAction("marketing_cam_trans_spd_up", function()
        logger.info("MarketingCam: marketing_cam_trans_spd_up")
        self.m_camController:ChangeMoveSpeed(1)
    end)
    self:BindInputPlayerAction("marketing_cam_trans_spd_down", function()
        logger.info("MarketingCam: marketing_cam_trans_spd_down")
        self.m_camController:ChangeMoveSpeed(-1)
    end)

    self:BindInputPlayerAction("marketing_cam_attach_to_obj", function()
        logger.info("MarketingCam: marketing_cam_attach_to_obj")
        self.m_camController:Attach()
    end)
    self:BindInputPlayerAction("marketing_cam_detach_to_obj", function()
        logger.info("MarketingCam: marketing_cam_detach_to_obj")
        self.m_camController:Detach()
    end)
    self:BindInputPlayerAction("marketing_cam_toggle_attach_mode", function()
        logger.info("MarketingCam: marketing_cam_toggle_attach_mode")
        self.m_camController:ToggleAttachMode()
    end)
    self:BindInputPlayerAction("marketing_cam_attach_to_next", function()
        logger.info("MarketingCam: marketing_cam_attach_to_next")
        self.m_camController:AttachToNextTarget()
    end)
    self:BindInputPlayerAction("marketing_cam_attach_to_prev", function()
        logger.info("MarketingCam: marketing_cam_attach_to_prev")
        self.m_camController:AttachToPrevTarget()
    end)

    self:BindInputPlayerAction("marketing_cam_toggle_space_mode", function()
        logger.info("MarketingCam: marketing_cam_toggle_space_mode")
        self.m_camController:ToggleMoveSpaceMode()
    end)

    self:BindInputPlayerAction("marketing_cam_fov_up", function()
        logger.info("MarketingCam: marketing_cam_fov_up")
        self.m_camController:ChangeFov(1)
    end)
    self:BindInputPlayerAction("marketing_cam_fov_down", function()
        logger.info("MarketingCam: marketing_cam_fov_down")
        self.m_camController:ChangeFov(-1)
    end)

    self:BindInputPlayerAction("marketing_cam_enter_roll_mode", function()
        self.m_camController:EnterRollMode()
    end)

    self:BindInputPlayerAction("marketing_cam_leave_roll_mode", function()
        self.m_camController:LeaveRollMode()
    end)

    self:BindInputPlayerAction("marketing_cam_attach_rot_left", function()
        logger.info("MarketingCam: marketing_cam_attach_rot_left")
        self.m_camController:SetAttachRotateInputX(-1)
    end)
    self:BindInputPlayerAction("marketing_cam_attach_rot_right", function()
        logger.info("MarketingCam: marketing_cam_attach_rot_right")
        self.m_camController:SetAttachRotateInputX(1)
    end)

    self:BindInputPlayerAction("marketing_cam_height_up", function()
        logger.info("MarketingCam: marketing_cam_height_up")
        self.m_camController:ChangeHeight(InputManagerInst:GetGamepadTriggerValue(false))
    end)
    self:BindInputPlayerAction("marketing_cam_height_down", function()
        logger.info("MarketingCam: marketing_cam_height_down")
        self.m_camController:ChangeHeight(-InputManagerInst:GetGamepadTriggerValue(true))
    end)
end






DebugMarketingCameraCtrl.OnClose = HL.Override() << function(self)
    CameraUtils:CloseMarketingCamera()
    InputManagerInst.enableMarketingCamera = false
end

DebugMarketingCameraCtrl._Update = HL.Method() << function(self)


    if not self.m_camController then
        self.m_camController = GameInstance.cameraManager:GetMainMarketingCameraController()
    end

    if not self.m_camController then
        return
    end


    local moveX = InputManagerInst:GetAxis("HorizontalController")
    local moveY = InputManagerInst:GetAxis("VerticalController")
    if moveX ~= 0 or moveY ~= 0 then
        logger.info("MarketingCam: Move", moveX, moveY)
        self.m_camController:SetMoveInput(moveX, moveY)
    end


    local viewX = InputManagerInst:GetAxis("View X")
    local viewY = InputManagerInst:GetAxis("View Y")
    if viewX ~= 0 or viewY ~= 0 then
        logger.info("MarketingCam: View", viewX, viewY)
        self.m_camController:SetRotateInput(viewX, viewY)
    end
end

HL.Commit(DebugMarketingCameraCtrl)
