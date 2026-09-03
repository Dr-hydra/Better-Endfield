










StateBehaviour = HL.Class("StateBehaviour")




StateBehaviour.state = HL.Field(HL.Number) << PhaseConst.EPhaseState.Init

StateBehaviour.arg = HL.Field(HL.Any)

StateBehaviour.m_completeOnDestroy = HL.Field(HL.Boolean) << false






StateBehaviour.StateBehaviour = HL.Constructor(HL.Opt(HL.Any)) << function(self, arg)
    self.arg = arg
    self:_Init()
end

StateBehaviour._Init = HL.Method() << function(self)
    self.state = PhaseConst.EPhaseState.Init
    self.m_completeOnDestroy = false
    self:_OnInit()
end

StateBehaviour._OnInit = HL.Virtual() << function(self)
end






StateBehaviour.isActive = HL.Field(HL.Boolean) << false

StateBehaviour._ActivePhase = HL.Method() << function(self)
    local succ, log = xpcall(function()
        self.isActive = true
        self:_OnActivated()
    end, debug.traceback)
    if not succ then
        logger.critical(log)
    end
end

StateBehaviour._OnActivated = HL.Virtual() << function(self)
end


StateBehaviour._DeactivatePhase = HL.Method() << function(self)
    local succ, log = xpcall(function()
        self.isActive = false
        self:_OnDeActivated()
    end, debug.traceback)
    if not succ then
        logger.critical(log)
    end
end
StateBehaviour._OnDeActivated = HL.Virtual() << function(self)
end






StateBehaviour.TransitionIn = HL.Method(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.state = PhaseConst.EPhaseState.TransitionIn
    self:_DoTransitionInCoroutine(fastMode, args)
    if fastMode or self:_CheckAllTransitionDone() then
        self.state = PhaseConst.EPhaseState.Activated
        self:_ActivePhase()
    else
        self:_StartCoroutine(function()
            coroutine.waitCondition(function()
                return self:_CheckAllTransitionDone()
            end)
            self.state = PhaseConst.EPhaseState.Activated
            self:_ActivePhase()
        end)
    end
end

StateBehaviour._DoTransitionInCoroutine = HL.Virtual(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






StateBehaviour.TransitionBackToTop = HL.Method(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.state = PhaseConst.EPhaseState.TransitionBackToTop
    self:_DoTransitionBackToTopCoroutine(fastMode, args)
    if fastMode or self:_CheckAllTransitionDone() then
        self.state = PhaseConst.EPhaseState.Activated
        self:_ActivePhase()
    else
        self:_StartCoroutine(function()
            coroutine.waitCondition(function()
                return self:_CheckAllTransitionDone()
            end)
            self.state = PhaseConst.EPhaseState.Activated
            self:_ActivePhase()
        end)
    end
end

StateBehaviour._DoTransitionBackToTopCoroutine = HL.Virtual(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






StateBehaviour.TransitionBehind = HL.Method(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.state = PhaseConst.EPhaseState.TransitionBehind
    self:_DoTransitionBehindCoroutine(fastMode, args)
    if fastMode or self:_CheckAllTransitionDone() then
        self.state = PhaseConst.EPhaseState.Deactivated
        self:_DeactivatePhase()
    else
        self:_StartCoroutine(function()
            coroutine.waitCondition(function()
                return self:_CheckAllTransitionDone()
            end)
            self.state = PhaseConst.EPhaseState.Deactivated
            self:_DeactivatePhase()
        end)
    end
end

StateBehaviour._DoTransitionBehindCoroutine = HL.Virtual(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end





StateBehaviour.TransitionOut = HL.Method(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.state = PhaseConst.EPhaseState.TransitionOut
    self:_DoTransitionOutCoroutine(fastMode, args)
    if fastMode or self:_CheckAllTransitionDone() then
        self.state = PhaseConst.EPhaseState.WaitRelease
        self:_DeactivatePhase()
    else
        self:_StartCoroutine(function()
            coroutine.waitCondition(function()
                return self:_CheckAllTransitionDone()
            end)
            self.state = PhaseConst.EPhaseState.WaitRelease
            self:_DeactivatePhase()
        end)
    end
end

StateBehaviour._DoTransitionOutCoroutine = HL.Virtual(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

StateBehaviour._CheckAllTransitionDone = HL.Virtual().Return(HL.Boolean) << function(self)
    return true
end





StateBehaviour.Destroy = HL.Method() << function(self)
    local succ, log = xpcall(function()
        self:_OnDestroy()
        self.m_completeOnDestroy = true
    end, debug.traceback)
    if not succ then
        logger.critical(log)
    end

    self:_InnerDestroy()
    self:_ClearAllCoroutine()
end


StateBehaviour._InnerDestroy = HL.Virtual() << function(self)
end


StateBehaviour._OnDestroy = HL.Virtual() << function(self)
end






StateBehaviour._StartCoroutine = HL.Method(HL.Function).Return(HL.Thread) << function(self, func)
    return CoroutineManager:StartCoroutine(func, self)
end

StateBehaviour._ClearCoroutine = HL.Method(HL.Thread).Return(HL.Any) << function(self, coroutine)
    CoroutineManager:ClearCoroutine(coroutine)
    return nil
end

StateBehaviour._ClearAllCoroutine = HL.Method() << function(self)
    CoroutineManager:ClearAllCoroutine(self)
end




HL.Commit(StateBehaviour)
