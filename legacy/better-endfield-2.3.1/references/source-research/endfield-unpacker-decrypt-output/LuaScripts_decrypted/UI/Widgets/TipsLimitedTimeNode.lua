local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

TipsLimitedTimeNode = HL.Class('TipsLimitedTimeNode', UIWidgetBase)


local TICK_TIME_INTERVAL = 2


TipsLimitedTimeNode.m_itemId = HL.Field(HL.String) << ""

TipsLimitedTimeNode.m_instId = HL.Field(HL.Number) << 0

TipsLimitedTimeNode.m_info = HL.Field(HL.Table)

TipsLimitedTimeNode.m_onTimeOut = HL.Field(HL.Function)

TipsLimitedTimeNode.m_luaUpdateKey = HL.Field(HL.Number) << 0

TipsLimitedTimeNode.m_deltaTime = HL.Field(HL.Number) << 0

TipsLimitedTimeNode.m_isExpireWarningState = HL.Field(HL.Boolean) << false




TipsLimitedTimeNode._OnFirstTimeInit = HL.Override() << function(self)
end

TipsLimitedTimeNode.InitTipsLimitedTimeNode = HL.Method(HL.String, HL.Number, HL.Opt(HL.Function)) << function(self, itemId, instId, onTimeOut)
    self:_FirstTimeInit()

    self.m_itemId = itemId
    self.m_instId = instId
    self.m_info = Utils.getLTItemExpireInfo(itemId, instId)
    self.m_onTimeOut = onTimeOut
    self:_ToggleTickTime(false)
    self:_ToggleTickTime(true)
end

TipsLimitedTimeNode._OnEnable = HL.Override() << function(self)
    self:_ToggleTickTime(true)
end

TipsLimitedTimeNode._OnDisable = HL.Override() << function(self)
    self:_ToggleTickTime(false)
end

TipsLimitedTimeNode._OnDestroy = HL.Override() << function(self)
    self:_ToggleTickTime(false)
end

TipsLimitedTimeNode._ToggleTickTime = HL.Method(HL.Boolean) << function(self, isOn)
    if isOn then
        if not self.m_info.isLTItem then
            self.view.gameObject:SetActive(false)
            return
        end
        if self.m_luaUpdateKey > 0 then
            return
        end
        self.view.gameObject:SetActive(true)

        self.m_isExpireWarningState = false
        self.view.stateController:SetState("Normal")
        self:_RefreshTime()

        if self.m_luaUpdateKey <= 0 then
            self.m_luaUpdateKey = LuaUpdate:Add("LateTick", function(deltaTime)
                self.m_deltaTime = self.m_deltaTime + deltaTime
                if self.m_deltaTime >= TICK_TIME_INTERVAL then
                    self.m_deltaTime = 0
                    self:_RefreshTime()
                end
            end)
        end
    else
        self.m_luaUpdateKey = LuaUpdate:Remove(self.m_luaUpdateKey)
        self.view.gameObject:SetActive(false)
    end
end

TipsLimitedTimeNode._RefreshTime = HL.Method() << function(self)
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local leftTime = self.m_info.expireTime - curTime
    if leftTime <= 0 then
        self.m_info.isExpire = true
    end

    local isAlmostExpire = leftTime <= self.m_info.almostExpireTime
    if self.m_isExpireWarningState ~= isAlmostExpire then
        self.m_isExpireWarningState = true
        self.view.stateController:SetState("Warning")
    end

    if self.m_info.isExpire then
        self.view.timeTxt.text = Language.LUA_ITEM_TIPS_LIMIT_TIME_ITEM_IS_EXPIRE
        self:_ToggleTickTime(false)
        if self.m_onTimeOut then
            self.m_onTimeOut()
        end
    else
        self.view.timeTxt.text = UIUtils.getLeftTime(leftTime)
    end
end


HL.Commit(TipsLimitedTimeNode)
return TipsLimitedTimeNode

