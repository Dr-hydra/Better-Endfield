
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityPhotoTakingUniverse

ActivityPhotoTakingUniverseCtrl = HL.Class('ActivityPhotoTakingUniverseCtrl', uiCtrl.UICtrl)






ActivityPhotoTakingUniverseCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivityPhotoTakingUniverseCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityPhotoTakingUniverseCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self.view.activityCommonInfo.view.gotoNode.btnDetailRedDot:InitRedDot("ActivityConditionalMultiStage", self.m_activityId)
end


HL.Commit(ActivityPhotoTakingUniverseCtrl)
