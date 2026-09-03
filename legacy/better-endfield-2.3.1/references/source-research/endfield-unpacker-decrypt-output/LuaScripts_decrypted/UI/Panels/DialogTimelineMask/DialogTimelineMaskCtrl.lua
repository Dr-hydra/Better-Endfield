
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DialogTimelineMask

DialogTimelineMaskCtrl = HL.Class('DialogTimelineMaskCtrl', uiCtrl.UICtrl)






DialogTimelineMaskCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_START_CAMERA_CROSS_FADE] = 'OnStartCameraCrossFade',
    [MessageConst.ON_STOP_CAMERA_CROSS_FADE] = 'OnStopCameraCrossFade',
}

DialogTimelineMaskCtrl.m_setCaptureCor = HL.Field(HL.Thread)

DialogTimelineMaskCtrl.m_captureRtHandle = HL.Field(HL.Userdata)

DialogTimelineMaskCtrl.m_timelineHandle = HL.Field(HL.Userdata)


DialogTimelineMaskCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_timelineHandle = unpack(arg)
end

DialogTimelineMaskCtrl._InitBorderMask = HL.Method() << function(self, arg)
        local screenWidth = Screen.width
        local screenHeight = Screen.height

        local maxScreenWidth = UIConst.MAX_DIALOG_ASPECT_RATIO * screenHeight
        local borderSize = (screenWidth - maxScreenWidth) / 2
        local ratio = self.view.transform.rect.width / Screen.width

        self.view.leftBorder.gameObject:SetActive(true)
        self.view.rightBorder.gameObject:SetActive(true)


end

DialogTimelineMaskCtrl.OnShow = HL.Override() << function(self)
    self:_InitBorderMask()
    GameWorld.dialogTimelineManager:BindCameraCrossFadeMask(self.m_timelineHandle, self.view.cameraCrossFade)
end


DialogTimelineMaskCtrl.OnStartCameraCrossFade = HL.Method() << function(self)
    self:_ReleaseCaptureRt()
    self.m_captureRtHandle = ScreenCaptureUtils.GetScreenCaptureWithoutUI();
    self.m_setCaptureCor = self:_StartCoroutine(function() return self:_SetScreenCapture() end)
end

DialogTimelineMaskCtrl._SetScreenCapture = HL.Method() << function(self)
    coroutine.waitForRenderDone()
    if self.m_captureRtHandle then
        self.view.cameraCrossFade.texture = self.m_captureRtHandle.rt
        self.view.cameraCrossFade.gameObject:SetActive(true)
    end
end

DialogTimelineMaskCtrl._ReleaseCaptureRt = HL.Method() << function(self)
    if self.m_captureRtHandle then
        self.m_captureRtHandle:Release()
        self.m_captureRtHandle = nil
    end
end

DialogTimelineMaskCtrl.OnStopCameraCrossFade = HL.Method() << function(self)
    if self.m_setCaptureCor then
        self:_ClearCoroutine(self.m_setCaptureCor)
        self.m_setCaptureCor = nil
    end

    self:_ReleaseCaptureRt()
    self.view.cameraCrossFade.gameObject:SetActive(false)
    self.view.cameraCrossFade.texture = nil
end



DialogTimelineMaskCtrl.OnClose = HL.Override() << function(self)
    if self.m_setCaptureCor then
        self:_ClearCoroutine(self.m_setCaptureCor)
        self.m_setCaptureCor = nil
    end

    self:_ReleaseCaptureRt()
end

HL.Commit(DialogTimelineMaskCtrl)
