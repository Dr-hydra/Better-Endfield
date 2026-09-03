local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MainHudExpandNode = HL.Class('MainHudExpandNode', UIWidgetBase)


MainHudExpandNode.m_isExpanded = HL.Field(HL.Boolean) << false

MainHudExpandNode.m_autoTimerId = HL.Field(HL.Number) << -1

MainHudExpandNode.m_belongNode = HL.Field(HL.Table)


MainHudExpandNode.InitMainHudExpandNode = HL.Method(HL.Table) << function(self, belongNode)
    if self.m_belongNode then
        logger.error("已经初始化了", self.view.transform:PathFromRoot())
        return
    end

    self.m_belongNode = belongNode
    self.gameObject:SetActive(true)
    self:SetExpanded(false, true)
end

MainHudExpandNode.StartAutoCloseTimer = HL.Method() << function(self)
    self:ClearAutoCloseTimer()
    self.m_autoTimerId = self:_StartTimer(self.view.config.EXPAND_TIME, function()
        self:SetExpanded(false)
    end)
end

MainHudExpandNode.ClearAutoCloseTimer = HL.Method() << function(self)
    self.m_autoTimerId = self:_ClearTimer(self.m_autoTimerId)
end

MainHudExpandNode.SetExpanded = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, expand, skipAnimation)
    CoroutineManager:ClearAllCoroutine(self)
    local node = self.m_belongNode
    local hasContent = node.m_curInsideBtnCount and node.m_curInsideBtnCount > 0
    if expand and not hasContent then

        return
    end
    self.m_isExpanded = expand
    self.m_autoTimerId = self:_ClearTimer(self.m_autoTimerId)
    if DeviceInfo.usingController then
        self:_OnAnimationFinished(true)
        return
    end
    if skipAnimation or not hasContent then
        self:_OnAnimationFinished(true)
    else
        self.transform.localScale = Vector3.one
        self.view.closeExpandBtn.gameObject:SetActive(true)
        node.expandBtn.gameObject:SetActive(false)
        if expand then
            self.view.animationWrapper:PlayInAnimation(function()
                self:_OnAnimationFinished()
            end)
            self:_RefreshAlpha(function(canvasGroup, k, childCount)
                canvasGroup.alpha = 0
                canvasGroup:DOFade(1, self.view.config.ANI_IN_BTN_FADE_TIME):SetDelay(self.view.config.ANI_IN_BTN_FADE_DELAY * k)
            end)
        else
            self.view.animationWrapper:PlayOutAnimation(function()
                self:_OnAnimationFinished()
            end)
            self:_RefreshAlpha(function(canvasGroup, k, childCount)
                canvasGroup.alpha = 1
                canvasGroup:DOFade(0, self.view.config.ANI_OUT_BTN_FADE_TIME):SetDelay(self.view.config.ANI_OUT_BTN_FADE_DELAY * (childCount - k - 1))
            end)
        end
    end
end

MainHudExpandNode._OnAnimationFinished = HL.Method(HL.Opt(HL.Boolean)) << function(self, forceRefreshAlpha)
    local node = self.m_belongNode
    self.transform.localScale = self.m_isExpanded and Vector3.one or Vector3.zero
    self.view.closeExpandBtn.gameObject:SetActive(self.m_isExpanded)
    node.expandBtn.gameObject:SetActive(not self.m_isExpanded and node.m_curInsideBtnCount and node.m_curInsideBtnCount > 0 and (not DeviceInfo.usingController))
    if forceRefreshAlpha then
        self:_RefreshAlpha(function(canvasGroup, k, childCount)
            canvasGroup.alpha = 1
        end)
    end
end

MainHudExpandNode._RefreshAlpha = HL.Method(HL.Function) << function(self, actionOnCanvasGroup)
    local childCount = self.view.transform.childCount
    for k = 1, childCount - 1 do
        local child = self.view.transform:GetChild(k)
        if child then
            local canvasGroup = child.transform:GetComponent("CanvasGroup")
            if NotNull(canvasGroup) then
                canvasGroup:DOKill()
                actionOnCanvasGroup(canvasGroup, k, childCount)
            else
                logger.error("需要添加 CanvasGroup 组件", child.gameObject.name)
            end
        end
    end
end


HL.Commit(MainHudExpandNode)
return MainHudExpandNode
