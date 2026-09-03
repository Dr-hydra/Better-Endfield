
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FullScreenSceneBlur


local MarkerState = CS.Beyond.UI.FullScreenSceneBlurMarker.State

FullScreenSceneBlurCtrl = HL.Class('FullScreenSceneBlurCtrl', uiCtrl.UICtrl)






FullScreenSceneBlurCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.QUICK_HIDE_FULL_SCREEN_SCENE_BLUR] = 'QuickHideBlur',
}


FullScreenSceneBlurCtrl.m_activeMarkers = HL.Field(HL.Table)

FullScreenSceneBlurCtrl.m_blackBlurMarkers = HL.Field(HL.Table)

FullScreenSceneBlurCtrl.m_notUseSceneColorPSMarkers = HL.Field(HL.Table)

FullScreenSceneBlurCtrl.m_needUpdate = HL.Field(HL.Boolean) << false

FullScreenSceneBlurCtrl.m_updateKey = HL.Field(HL.Number) << -1



FullScreenSceneBlurCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_activeMarkers = {}
    self.m_blackBlurMarkers = {}
    self.m_notUseSceneColorPSMarkers = {}
    self.view.blurBG.gameObject:SetActive(false)
    self.view.blurBG.enabled = true
    CS.Beyond.UI.FullScreenSceneBlurMarker.s_onFullScreenSceneBlurMarkerStateChanged = function(id, state, useWhiteBlur, useSceneColorPS)
        self:OnFullScreenSceneBlurMarkerStateChanged(id, state, useWhiteBlur, useSceneColorPS)
    end

    self.m_updateKey = LuaUpdate:Add("TailTick", function()
        self:_Update()
    end)
end

FullScreenSceneBlurCtrl.OnClose = HL.Override() << function(self)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
    CS.Beyond.UI.FullScreenSceneBlurMarker.s_onFullScreenSceneBlurMarkerStateChanged = nil
    UIManager:RemoveMainCameraTempRequest("FullScreenSceneBlur")
    self.view.blurBGRawImage:DOKill()
end

FullScreenSceneBlurCtrl.OnFullScreenSceneBlurMarkerStateChanged = HL.Method(HL.Number, MarkerState, HL.Boolean, HL.Boolean) << function(self, id, state, useWhiteBlur, useSceneColorPS)
    if state == MarkerState.OnEnable then
        self.m_activeMarkers[id] = true
        if not useWhiteBlur then
            self.m_blackBlurMarkers[id] = true
        end
        if not useSceneColorPS then
            self.m_notUseSceneColorPSMarkers[id] = true
        end
        self:_UpdateState()
    else
        self.m_activeMarkers[id] = nil
        if not useWhiteBlur then
            self.m_blackBlurMarkers[id] = nil
        end
        if not useSceneColorPS then
            self.m_notUseSceneColorPSMarkers[id] = nil
        end
        self.m_needUpdate = true
    end
end

FullScreenSceneBlurCtrl._Update = HL.Method() << function(self)
    if self.m_needUpdate then
        self:_UpdateState()
    end
end


















FullScreenSceneBlurCtrl._UpdateState = HL.Method() << function(self)
    self.m_needUpdate = false
    local shouldShow = next(self.m_activeMarkers) ~= nil
    local curIsShowing = self.view.blurBG.gameObject.activeSelf

    if curIsShowing ~= shouldShow then
        if shouldShow then
            local shouldPlayInAnim = UIManager:WasSceneVisibleBeforeThisHideCameraPanel()
            logger.info("FullScreenSceneBlurCtrl: blur activating, shouldPlayInAnim", shouldPlayInAnim)

            UIManager:AddMainCameraTempRequest("FullScreenSceneBlur")
            TimerManager:StartFrameTimer(2, function()
                logger.info("FullScreenSceneBlurCtrl: blur RT captured, releasing camera request")
                UIManager:RemoveMainCameraTempRequest("FullScreenSceneBlur")
            end, self)

            self.view.blurBGAnimationWrapper:ClearTween(false)
            self.view.blurBG.gameObject:SetActive(true)

            if shouldPlayInAnim then
                self.view.blurBGAnimationWrapper:PlayInAnimation()
            else
                self.view.blurBGAnimationWrapper:SampleToInAnimationEnd()
            end
        else
            if self.view.blurBGAnimationWrapper.curState ~= CS.Beyond.UI.UIConst.AnimationState.Out then


                UIManager:AddMainCameraTempRequest("FullScreenSceneBlur")
                self.view.blurBGAnimationWrapper:PlayOutAnimation(function()
                    self.view.blurBG.gameObject:SetActive(false)



                    UIManager:RemoveMainCameraTempRequest("FullScreenSceneBlur")
                end)
            end
        end
    elseif curIsShowing then
        if self.view.blurBGAnimationWrapper.curState == CS.Beyond.UI.UIConst.AnimationState.Out then

            self.view.blurBGAnimationWrapper:ClearTween(false)
            self.view.blurBGAnimationWrapper:SampleToInAnimationEnd()



            UIManager:RemoveMainCameraTempRequest("FullScreenSceneBlur")
        end
    end

    self.view.blurBGRawImage:DOKill()
    if shouldShow and self.view.blurBGRawImage.texture ~= nil then
        local useBlackBlur = next(self.m_blackBlurMarkers) ~= nil
        local color = useBlackBlur and self.view.config.DEFAULT_COLOR or self.view.config.WHITE_BLUR_COLOR
        if self.view.blurBGRawImage.color ~= color then
            if curIsShowing then
                self.view.blurBGRawImage:DOColor(color, 0.2)
            else
                self.view.blurBGRawImage.color = color
            end
        end
        local useSceneColorPS = next(self.m_notUseSceneColorPSMarkers) == nil
        self.view.blurBG:SetUseSceneColorPS(useSceneColorPS)
    end
end

FullScreenSceneBlurCtrl.QuickHideBlur = HL.Method() << function(self)
    self.view.blurBGRawImage:DOKill()
    self.view.blurBG.gameObject:SetActive(false)

    UIManager:RemoveMainCameraTempRequest("FullScreenSceneBlur")
end

HL.Commit(FullScreenSceneBlurCtrl)
