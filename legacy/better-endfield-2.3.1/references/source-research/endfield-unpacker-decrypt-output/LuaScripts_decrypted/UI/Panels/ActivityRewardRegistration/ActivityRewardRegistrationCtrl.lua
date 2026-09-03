

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')

ActivityRewardRegistrationCtrl = HL.Class('ActivityRewardRegistrationCtrl', uiCtrl.UICtrl)

ActivityRewardRegistrationCtrl.s_messages = HL.StaticField(HL.Table) << {
}


ActivityRewardRegistrationCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)

    local initArg = {
        activityId = args.activityId,
        isPopup = false,
        animation = self.view.rewardStateNode,
        animNameList = { "activityrewardregistration_state_changepage_in", "activityrewardregistration_state_changepage_out" },
    }
    self.view.activityRewardRegistrationInfo:Init(initArg)
end

ActivityRewardRegistrationCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)

    if self.view.activityRewardRegistrationInfo then
        self.view.activityRewardRegistrationInfo:OnPanelInputBlocked(active)
    end
end

ActivityRewardRegistrationCtrl.OnActivityCenterNaviFailed = HL.Method() << function(self)
    self.view.activityRewardRegistrationInfo:OnActivityCenterNaviFailed()
end

HL.Commit(ActivityRewardRegistrationCtrl)