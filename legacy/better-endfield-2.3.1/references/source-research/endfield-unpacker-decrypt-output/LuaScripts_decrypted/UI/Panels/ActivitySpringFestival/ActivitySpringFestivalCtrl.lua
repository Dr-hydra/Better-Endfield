
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivitySpringFestival

ActivitySpringFestivalCtrl = HL.Class('ActivitySpringFestivalCtrl', uiCtrl.UICtrl)

ActivitySpringFestivalCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivitySpringFestivalCtrl.m_activityId = HL.Field(HL.String) << ''

ActivitySpringFestivalCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self.view.activityCommonInfo.view.gotoNode.btnDetailRedDot:InitRedDot("ActivitySpringFest",args.activityId)
    self.view.activityCommonInfo.view.gotoNode.btnDetail.onClick:AddListener(function()
        ActivityUtils.setActivityDayAsRead(self.m_activityId)
    end)
end


HL.Commit(ActivitySpringFestivalCtrl)
