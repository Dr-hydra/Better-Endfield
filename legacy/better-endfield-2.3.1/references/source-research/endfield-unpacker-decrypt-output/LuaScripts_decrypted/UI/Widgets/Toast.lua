local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

Toast = HL.Class('Toast', UIWidgetBase)

Toast.m_isInMainHud = HL.Field(HL.Boolean) << false

Toast.m_isToastShowReady = HL.Field(HL.Boolean) << false

Toast.m_isToastShow = HL.Field(HL.Boolean) << false

Toast.m_toastTimerId = HL.Field(HL.Number) << -1

Toast.m_args = HL.Field(HL.Table)

Toast._OnDisable = HL.Override() << function(self)
    self:_HideToast()
end

Toast._OnDestroy = HL.Override() << function(self)
    self:_HideToast()
end


Toast._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_PHASE_LEVEL_ON_TOP, function()
        self:OnEnterMainHud()
    end)

    self:RegisterMessage(MessageConst.ON_PHASE_LEVEL_NOT_ON_TOP, function()
        self:OnLeaveMainHud()
    end)
end

Toast.InitToast = HL.Method(HL.Opt(HL.Table)) << function(self, args)
    self:_FirstTimeInit()

    self.m_args = args
    self.m_isInMainHud = PhaseManager:GetTopPhaseId() == PhaseId.Level

    self.view.rectTransform.gameObject:SetActive(false)
end

Toast.OnEnterMainHud = HL.Method() << function(self)
    self.m_isInMainHud = true

    if self.m_isToastShowReady then
        self:ShowToast()
    end
end

Toast.OnLeaveMainHud = HL.Method() << function(self)
    self.m_isInMainHud = false
end

Toast.ShowToast = HL.Method() << function(self)
    if self.view.config.IS_MAIN_HUD_TOAST and not self.m_isInMainHud then
        self.m_isToastShowReady = true
        return
    end

    if not self.m_isToastShow then
        self.view.rectTransform.gameObject:SetActive(true)

        self.m_toastTimerId = self:_StartTimer(self.view.config.SHOW_TOAST_DURATION, function()
            self:HideToast()
        end)
    end

    if self.m_isToastShow and self.view.config.IS_NEW_TOAST_REFRESH_TIMER then
        self.m_toastTimerId = self:_ClearTimer(self.m_toastTimerId)

        self.m_toastTimerId = self:_StartTimer(self.view.config.SHOW_TOAST_DURATION, function()
            self:HideToast()
        end)
    end

    self.m_isToastShow = true

    if self.view.animation ~= nil then
        self.view.animation:PlayInAnimation()
    end

    if self.m_isToastShowReady then
        self.m_isToastShowReady = false
    end
end

Toast.HideToast = HL.Method() << function(self)
    local finishFunc
    if self.m_args then
        finishFunc = self.m_args.finishFunc
    end

    if self.view.animation == nil then
        self:_HideToast()
        if finishFunc then
            finishFunc()
        end
    else
        self.view.animation:PlayOutAnimation(function()
            self:_HideToast()
            if finishFunc then
                finishFunc()
            end
        end)
    end
end

Toast.IsToastShow = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_isToastShow
end

Toast._HideToast = HL.Method() << function(self)
    if not self.m_isToastShow then
        return
    end

    self.m_isToastShow = false
    self.m_toastTimerId = self:_ClearTimer(self.m_toastTimerId)
    self.view.rectTransform.gameObject:SetActiveIfNecessary(false)
end

HL.Commit(Toast)
return Toast
