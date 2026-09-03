
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DungeonCharTimeHint

DungeonCharTimeHintCtrl = HL.Class('DungeonCharTimeHintCtrl', uiCtrl.UICtrl)

DungeonCharTimeHintCtrl.m_leaveTick = HL.Field(HL.Number) << -1





DungeonCharTimeHintCtrl.s_messages = HL.StaticField(HL.Table) << {

}


DungeonCharTimeHintCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    self.m_leaveTick = DungeonUtils.startSubGameLeaveTick(function(leftTime)
        self.view.timeTxt.text = string.format(Language["ui_dungeon_settlement_popup_countdown"], leftTime)
    end)
end

DungeonCharTimeHintCtrl.OnClose = HL.Override() << function(self)
    if self.m_leaveTick then
        self.m_leaveTick = LuaUpdate:Remove(self.m_leaveTick)
    end
end

HL.Commit(DungeonCharTimeHintCtrl)
