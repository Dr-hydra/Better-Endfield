
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityBasic

ActivityBasicCtrl = HL.Class('ActivityBasicCtrl', uiCtrl.UICtrl)






ActivityBasicCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivityBasicCtrl.m_activityId = HL.Field(HL.String) << ''


ActivityBasicCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
end

HL.Commit(ActivityBasicCtrl)
