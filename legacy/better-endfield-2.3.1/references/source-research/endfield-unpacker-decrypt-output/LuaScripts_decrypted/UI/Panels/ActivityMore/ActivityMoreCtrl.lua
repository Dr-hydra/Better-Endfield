
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityMore

ActivityMoreCtrl = HL.Class('ActivityMoreCtrl', uiCtrl.UICtrl)






ActivityMoreCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivityMoreCtrl.m_activityId = HL.Field(HL.String) << ''


ActivityMoreCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
end

HL.Commit(ActivityMoreCtrl)
