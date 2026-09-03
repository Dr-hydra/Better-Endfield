local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

StaminaTips = HL.Class('StaminaTips', UIWidgetBase)

StaminaTips.m_coroutine = HL.Field(HL.Thread)


StaminaTips._OnFirstTimeInit = HL.Override() << function(self)
    self:_RefreshTickRecover()
end

StaminaTips.InitStaminaTips = HL.Method() << function(self)
    self:_FirstTimeInit()
end

StaminaTips._OnEnable = HL.Override() << function(self)
    self:_StartTickRecover()
end

StaminaTips._OnDisable = HL.Override() << function(self)
    self:_StopTickRecover()
end

StaminaTips._StartTickRecover = HL.Method() << function(self)
    self:_StopTickRecover()

    self:_RefreshTickRecover()
    self.m_coroutine = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            self:_RefreshTickRecover()
        end
    end)
end

StaminaTips._StopTickRecover = HL.Method() << function(self)
    if self.m_coroutine then
        self:_ClearCoroutine(self.m_coroutine)
    end
end

StaminaTips._RefreshTickRecover = HL.Method() << function(self)
    local curStamina = GameInstance.player.inventory.curStamina
    local maxStamina = GameInstance.player.inventory.maxStamina
    local nextLeftTime = Utils.nextStaminaRecoverLeftTime()
    local fullLeftTime = Utils.fullStaminaRecoverLeftTime()
    self.view.nextRecoverTime.text = string.format(Language.LUA_STAMINA_NEXT_RECOVER_TIME, UIUtils.getLeftTimeToSecond(nextLeftTime))
    self.view.fullRecoverTime.text = string.format(Language.LUA_STAMINA_FULL_RECOVER_TIME, UIUtils.getLeftTimeToSecond(fullLeftTime))

    if curStamina == maxStamina then
        self.view.gameObject:SetActive(false)
        self:_StopTickRecover()
    end
end

HL.Commit(StaminaTips)
return StaminaTips

