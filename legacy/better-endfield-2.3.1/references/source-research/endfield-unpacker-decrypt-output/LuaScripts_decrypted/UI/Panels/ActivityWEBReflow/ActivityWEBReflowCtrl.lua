
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityWEBReflow

ActivityWEBReflowCtrl = HL.Class('ActivityWEBReflowCtrl', uiCtrl.UICtrl)

ActivityWEBReflowCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivityWEBReflowCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityWEBReflowCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self.view.activityCommonInfo.view.gotoNode.btnDetailRedDot:InitRedDot("ActivityDaily",args.activityId)
    self.view.activityCommonInfo.view.gotoNode.btnDetail.onClick:AddListener(function()
        local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
        if activity then
            ActivityUtils.setActivityDayAsRead(self.m_activityId)
        end
    end)
end


HL.Commit(ActivityWEBReflowCtrl)
