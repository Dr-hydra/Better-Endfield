
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityWEBFool

ActivityWEBFoolCtrl = HL.Class('ActivityWEBFoolCtrl', uiCtrl.UICtrl)

ActivityWEBFoolCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivityWEBFoolCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityWEBFoolCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    local redDotName = ActivityUtils.getActivityRedDotName(self.m_activityId) or "ActivityBasic"
    self.view.activityCommonInfoLuaReference.gotoNode.btnDetailRedDot:InitRedDot(redDotName, self.m_activityId)
end


HL.Commit(ActivityWEBFoolCtrl)
