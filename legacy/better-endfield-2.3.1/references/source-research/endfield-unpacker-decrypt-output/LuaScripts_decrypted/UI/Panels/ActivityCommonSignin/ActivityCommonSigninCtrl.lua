local ActivityCheckInBase = require_ex('UI/Widgets/ActivityCheckInBase')

ActivityRewardRegistrationInfo = HL.Class('ActivityRewardRegistrationInfo', ActivityCheckInBase)

ActivityCommonSigninCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityCommonSigninCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
end


HL.Commit(ActivityCommonSigninCtrl)
