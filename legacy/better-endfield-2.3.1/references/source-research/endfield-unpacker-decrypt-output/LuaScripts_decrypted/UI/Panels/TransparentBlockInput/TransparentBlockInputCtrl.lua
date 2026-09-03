
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.TransparentBlockInput
TransparentBlockInputCtrl = HL.Class('TransparentBlockInputCtrl', uiCtrl.UICtrl)

do

    TransparentBlockInputCtrl.m_timerId = HL.Field(HL.Number) << 0
end







TransparentBlockInputCtrl.s_messages = HL.StaticField(HL.Table) << {

}

TransparentBlockInputCtrl.OnShowBlockInputPanel = HL.StaticMethod(HL.Any) << function(arg)
    local time
    if type(arg) == "table" then
        time = arg[1]
    else
        time = arg
    end
    time = lume.clamp(time, 0, CS.Beyond.Gameplay.GameplayUIUtils.MAX_BLOCK_TIME)
    local ctrl = TransparentBlockInputCtrl.AutoOpen(PANEL_ID)
    Notify(MessageConst.TOGGLE_LEVEL_CAMERA_MOVE, { "TransparentBlockInputPanel", false })
    ctrl:SetDuration(time)
end


TransparentBlockInputCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

TransparentBlockInputCtrl.SetDuration = HL.Method(HL.Number) << function(self, time)
    if self.m_timerId ~= 0 then
        self.m_timerId = self:_ClearTimer(self.m_timerId)
    end

    self.m_timerId = self:_StartTimer(time, function()
        self:Hide()
        self.m_timerId = 0
        Notify(MessageConst.TOGGLE_LEVEL_CAMERA_MOVE, { "TransparentBlockInputPanel", true })
    end)
end

HL.Commit(TransparentBlockInputCtrl)
