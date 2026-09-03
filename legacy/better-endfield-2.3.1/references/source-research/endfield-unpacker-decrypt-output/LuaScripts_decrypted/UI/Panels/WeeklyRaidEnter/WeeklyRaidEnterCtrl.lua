local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeeklyRaidEnter

WeeklyRaidEnterCtrl = HL.Class('WeeklyRaidEnterCtrl', uiCtrl.UICtrl)





WeeklyRaidEnterCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE] = "OnToastInterrupted",
}

WeeklyRaidEnterCtrl.m_closeOnToastInterrupted = HL.Field(HL.Boolean) << false

WeeklyRaidEnterCtrl.ShowWeeklyRaidEnter = HL.StaticMethod(HL.Any) << function(arg)
    local isReEnter = unpack(arg)
    if isReEnter then
        return
    end
    LuaSystemManager.mainHudActionQueue:AddRequest('WeeklyRaidEnter', function(isResume)
        if isResume then
            local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
            if isOpen and ctrl then
                ctrl.animationWrapper:PlayInAnimation()
                coroutine.start(function()
                    coroutine.wait(2)
                    if not UIManager:IsOpen(PANEL_ID) then
                        return
                    end
                    self:PlayAnimationOutAndClose()
                    Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "WeeklyRaidEnter")
                end)
                return
            end
        end
        UIManager:AutoOpen(PANEL_ID)
    end)

end


WeeklyRaidEnterCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.animationWrapper:PlayInAnimation()
    coroutine.start(function()
        coroutine.wait(2)
        if not UIManager:IsOpen(PANEL_ID) then
            return
        end
        if self.m_closeOnToastInterrupted then
            return
        end
        self:PlayAnimationOutAndClose()
        Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "WeeklyRaidEnter")
    end)
end

WeeklyRaidEnterCtrl.OnToastInterrupted = HL.Method() << function(self)

    self.m_closeOnToastInterrupted = true
    self.animationWrapper:ClearTween(false)
    self:Close()
end











HL.Commit(WeeklyRaidEnterCtrl)
