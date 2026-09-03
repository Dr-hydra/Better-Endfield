local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

LimitTimeMarkNode = HL.Class('LimitTimeMarkNode', UIWidgetBase)


local TICK_TIME_INTERVAL = 2


LimitTimeMarkNode.m_isValid = HL.Field(HL.Boolean) << false

LimitTimeMarkNode.m_expireTime = HL.Field(HL.Number) << 0

LimitTimeMarkNode.m_almostExpireTime = HL.Field(HL.Number) << 0

LimitTimeMarkNode.m_isExpire = HL.Field(HL.Boolean) << false

LimitTimeMarkNode.m_updateGroupKey = HL.Field(HL.Number) << -1

LimitTimeMarkNode.m_onExpire = HL.Field(HL.Function)





LimitTimeMarkNode._OnFirstTimeInit = HL.Override() << function(self)

end

LimitTimeMarkNode._OnDestroy = HL.Override() << function(self)
    self:EndTickLimitTime()
end

LimitTimeMarkNode._OnEnable = HL.Override() << function(self)
    if self.m_isValid and self.m_updateGroupKey <= 0 then
        self.m_updateGroupKey = LimitTimeMarkNode._RegisterUpdate(self)
    end
end

LimitTimeMarkNode._OnDisable = HL.Override() << function(self)
    self.m_updateGroupKey = LimitTimeMarkNode._UnregisterUpdate(self.m_updateGroupKey)
end

LimitTimeMarkNode._OnDestroy = HL.Override() << function(self)
    self.m_updateGroupKey = LimitTimeMarkNode._UnregisterUpdate(self.m_updateGroupKey)
    self.m_isValid = false
end






LimitTimeMarkNode.StartTickLimitTime = HL.Method(HL.Number, HL.Any, HL.Opt(HL.Function)) <<
    function(self, expireTime, almostExpireTime, onExpireFunc)
    self:_FirstTimeInit()

    self.m_expireTime = expireTime
    if type(almostExpireTime) == "number" then
        self.m_almostExpireTime = almostExpireTime
    elseif type(almostExpireTime) == "boolean" then
        self.m_almostExpireTime = almostExpireTime and math.maxinteger or -1
    end
    self.m_isExpire = false

    self.m_updateGroupKey = LimitTimeMarkNode._UnregisterUpdate(self.m_updateGroupKey)
    self.m_updateGroupKey = LimitTimeMarkNode._RegisterUpdate(self)
    self.m_onExpire = onExpireFunc
    self.m_isValid = true
end

LimitTimeMarkNode.EndTickLimitTime = HL.Method() << function(self)
    self.m_updateGroupKey = LimitTimeMarkNode._UnregisterUpdate(self.m_updateGroupKey)
    self.m_isValid = false
end

LimitTimeMarkNode._RefreshLimitedTime = HL.Method() << function(self)
    local markUI = self.view
    if IsNull(markUI) or IsNull(markUI.gameObject) then
        if UNITY_EDITOR then
            local str = string.format("[限时道具刷新器Refresh] UI为空了但依然尝试刷新！ groupKey：%d", self.m_updateGroupKey)
            logger.error(str)
        end
        LimitTimeMarkNode._tempDeleteKeyMap[self.m_updateGroupKey] = self
        return
    end
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local leftTime = self.m_expireTime - curTime
    if leftTime <= 0 and not self.m_isExpire then
        if self.m_onExpire then
            self.m_onExpire()
        end
        self.m_isExpire = true
    end

    local isAlmostExpire = leftTime <= self.m_almostExpireTime
    if self.m_isExpireWarningState ~= isAlmostExpire then
        self.m_isExpireWarningState = true
        markUI.stateController:SetState("Warning")
    end

    if self.m_isExpire then
        markUI.timeTxt.text = Language.LUA_ITEM_TIPS_LIMIT_TIME_ITEM_IS_EXPIRE
        if LimitTimeMarkNode._tempDeleteKeyMap then
            LimitTimeMarkNode._tempDeleteKeyMap[self.m_updateGroupKey] = self
        end
    else
        markUI.timeTxt.text = UIUtils.getShortLeftTime(leftTime)
    end
end



LimitTimeMarkNode._updateKey = HL.StaticField(HL.Number) << 0

LimitTimeMarkNode._updateDeltaTime = HL.StaticField(HL.Number) << 0

LimitTimeMarkNode._updateGroup = HL.StaticField(HL.Table)

LimitTimeMarkNode._tempDeleteKeyMap = HL.StaticField(HL.Table)

LimitTimeMarkNode._updateGroupNextKey = HL.StaticField(HL.Number) << 0

LimitTimeMarkNode.m_isExpireWarningState = HL.Field(HL.Boolean) << false


LimitTimeMarkNode._RegisterUpdate = HL.StaticMethod(LimitTimeMarkNode).Return(HL.Number) << function(widget)
    if LimitTimeMarkNode._updateGroup == nil then
        LimitTimeMarkNode._updateGroup = {}
    end

    LimitTimeMarkNode._updateGroupNextKey = LimitTimeMarkNode._updateGroupNextKey + 1
    local curKey = LimitTimeMarkNode._updateGroupNextKey
    LimitTimeMarkNode._updateGroup[curKey] = widget
    widget.m_isExpireWarningState = false
    widget.view.stateController:SetState("Normal")
    widget:_RefreshLimitedTime()
    local needUpdate = next(LimitTimeMarkNode._updateGroup) ~= nil
    if (LimitTimeMarkNode._updateKey > 0) == needUpdate then
        return curKey
    end

    LimitTimeMarkNode._updateKey = LuaUpdate:Add("LateTick", function(deltaTime)
        LimitTimeMarkNode._updateDeltaTime = LimitTimeMarkNode._updateDeltaTime + deltaTime
        if LimitTimeMarkNode._updateDeltaTime >= TICK_TIME_INTERVAL then
            LimitTimeMarkNode._updateDeltaTime = 0
            LimitTimeMarkNode._tempDeleteKeyMap = {}
            for groupKey, singleWidget in pairs(LimitTimeMarkNode._updateGroup) do
                singleWidget:_RefreshLimitedTime()
            end
            for groupKey, singleWidget in pairs(LimitTimeMarkNode._tempDeleteKeyMap) do
                if singleWidget then
                    singleWidget.m_updateGroupKey = LimitTimeMarkNode._UnregisterUpdate(singleWidget.m_updateGroupKey)
                end
            end
        end
    end)
    return curKey
end

LimitTimeMarkNode._UnregisterUpdate = HL.StaticMethod(HL.Number).Return(HL.Number) << function(key)
    if LimitTimeMarkNode._updateGroup == nil or key <= 0 then
        return -1
    end

    LimitTimeMarkNode._updateGroup[key] = nil
    local noOneUpdate = next(LimitTimeMarkNode._updateGroup) == nil
    if noOneUpdate then
        LimitTimeMarkNode._updateKey = LuaUpdate:Remove(LimitTimeMarkNode._updateKey)
    end
    return -1
end
HL.Commit(LimitTimeMarkNode)
return LimitTimeMarkNode

