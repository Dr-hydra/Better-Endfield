
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeekraidHud

WeekraidHudCtrl = HL.Class('WeekraidHudCtrl', uiCtrl.UICtrl)






WeekraidHudCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTION_POINT_UPDATE] = 'OnActionPointUpdate',
    [MessageConst.ON_CLOSE_WEEKRAID_HUD] = 'OnCloseWeekRaidHud',
}

WeekraidHudCtrl.OnWeekraidDataInit = HL.StaticMethod() << function()

end


WeekraidHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:OnActionPointUpdate()
end

WeekraidHudCtrl.OnActionPointUpdate = HL.Method() << function(self)
    local weekraid = GameInstance.player.weekraidActionPointSystem
    self.view.scheduleText.text = string.format("%d/%d", math.floor(weekraid.curActionPoint), math.floor(weekraid.maxActionPoint))
end

WeekraidHudCtrl.OnCloseWeekRaidHud = HL.Method() << function(self)
    self:PlayAnimationOutAndClose()
end

HL.Commit(WeekraidHudCtrl)
