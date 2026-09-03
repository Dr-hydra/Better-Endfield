
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityActivityDungeonActMonster

ActivityDungeonActMonsterCtrl = HL.Class('ActivityDungeonActMonsterCtrl', uiCtrl.UICtrl)

ActivityDungeonActMonsterCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivityDungeonActMonsterCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityDungeonActMonsterCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self.view.btnDetailRedDot:InitRedDot("ActivityDungeon", self.m_activityId)
end


HL.Commit(ActivityDungeonActMonsterCtrl)
