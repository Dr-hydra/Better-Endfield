
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityPhotoTaking

ActivityPhotoTakingCtrl = HL.Class('ActivityPhotoTakingCtrl', uiCtrl.UICtrl)

ActivityPhotoTakingCtrl.s_messages = HL.StaticField(HL.Table) << {

}
ActivityPhotoTakingCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityPhotoTakingCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self.view.activityCommonInfo.view.gotoNode.btnDetailRedDot:InitRedDot("ActivityConditionalMultiStage", self.m_activityId)
end


HL.Commit(ActivityPhotoTakingCtrl)
