local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MainHudRacingEffectBtn = HL.Class('MainHudRacingEffectBtn', UIWidgetBase)

MainHudRacingEffectBtn.m_lateTickKey = HL.Field(HL.Number) << -1

MainHudRacingEffectBtn.m_remainFloatingTime = HL.Field(HL.Number) << 0


MainHudRacingEffectBtn._OnFirstTimeInit = HL.Override() << function(self)

end

MainHudRacingEffectBtn.InitMainHudRacingEffectBtn = HL.Method() << function(self)
    self:_FirstTimeInit()
end

MainHudRacingEffectBtn.OnShow = HL.Method() << function(self)
    self.view.floatingIcon.gameObject:SetActiveIfNecessary(false)
end

MainHudRacingEffectBtn.OnHide = HL.Method() << function(self)
    self.view.floatingIcon.gameObject:SetActiveIfNecessary(false)
    LuaUpdate:Remove(self.m_lateTickKey)
    self.m_lateTickKey = -1
end

local noEffectBuff = {["race_item_1"] = true, ["race_item_2"] = true}

MainHudRacingEffectBtn.CanPlayEffect = HL.Method(HL.String).Return(HL.Boolean) << function(self, buffId)
    return not noEffectBuff[buffId]
end

MainHudRacingEffectBtn._Update = HL.Method(HL.Number) << function(self, deltaTime)
    self.m_remainFloatingTime = self.m_remainFloatingTime - deltaTime

    if self.m_remainFloatingTime <= 0 then
        self.view.floatingIcon.gameObject:SetActiveIfNecessary(false)
        LuaUpdate:Remove(self.m_lateTickKey)
        self.m_lateTickKey = -1
        return
    end

    local localPosition = self.view.floatingIcon.transform.localPosition

    self.view.floatingIcon.transform.localPosition = Vector3(
        localPosition.x - localPosition.x / self.m_remainFloatingTime * deltaTime,
        localPosition.y - localPosition.y / self.m_remainFloatingTime * deltaTime,
        0)

end

HL.Commit(MainHudRacingEffectBtn)
return MainHudRacingEffectBtn

