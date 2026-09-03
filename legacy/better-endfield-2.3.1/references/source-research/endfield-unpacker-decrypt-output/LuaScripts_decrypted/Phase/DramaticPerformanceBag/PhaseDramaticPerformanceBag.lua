local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.DramaticPerformanceBag
PhaseDramaticPerformanceBag = HL.Class('PhaseDramaticPerformanceBag', phaseBase.PhaseBase)





PhaseDramaticPerformanceBag.s_messages = HL.StaticField(HL.Table) << {

}

PhaseDramaticPerformanceBag.m_renderTextureHandle = HL.Field(HL.Userdata)

PhaseDramaticPerformanceBag.m_bagPanelItem = HL.Field(HL.Forward("PhasePanelItem"))


PhaseDramaticPerformanceBag._OnInit = HL.Override() << function(self)
    PhaseDramaticPerformanceBag.Super._OnInit(self)
end




PhaseDramaticPerformanceBag.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseDramaticPerformanceBag._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)


    coroutine.waitCondition(function()
        return true
    end, coroutine.TailTick)

    self.m_renderTextureHandle = ScreenCaptureUtils.GetScreenCapture(math.floor(Screen.width), math.floor(Screen.height))
    coroutine.waitForRenderDone()

    self.m_bagPanelItem = self:CreatePhasePanelItem(PanelId.DramaticPerformanceBag, self.arg)
    self.m_bagPanelItem.uiCtrl:SetScreenCaptureImg(self.m_renderTextureHandle.rt)
end

PhaseDramaticPerformanceBag._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDramaticPerformanceBag._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDramaticPerformanceBag._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseDramaticPerformanceBag._OnActivated = HL.Override() << function(self)
end

PhaseDramaticPerformanceBag._OnDeActivated = HL.Override() << function(self)
end

PhaseDramaticPerformanceBag._OnDestroy = HL.Override() << function(self)
    self:_ReleaseRT()
    PhaseDramaticPerformanceBag.Super._OnDestroy(self)
end





PhaseDramaticPerformanceBag._ReleaseRT = HL.Method() << function(self)
    if self.m_renderTextureHandle then
        self.m_bagPanelItem.uiCtrl:SetScreenCaptureImg(nil)
        self.m_renderTextureHandle:Release()
        self.m_renderTextureHandle = nil
    end
end



HL.Commit(PhaseDramaticPerformanceBag)

