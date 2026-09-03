

local ActivityCheckInBase = require_ex('UI/Widgets/ActivityCheckInBase')

ActivityRewardRegistrationInfo = HL.Class('ActivityRewardRegistrationInfo', ActivityCheckInBase)

ActivityRewardRegistrationInfo.Init = HL.Method(HL.Table) << function(self, args)
    self:_InitAnim({
        startAnimTime = 0.6,
        animation = args.animation,
        animNameList = args.animNameList,
    })
    self:_InitScrollList({
        scrollList = self.view.rewardScrollList,
        rewardCell = self.view.cell,
    })
    self:_InitActivityInfo({
        activityId = args.activityId,
        isPopup = args.isPopup,
    })
    self:_InitTipPoints({
        stateNode = self.view.stateNode,
    })
    self:_InitReceiveAll({
        receiveAllBtn = self.view.receiveAllBtn,
        receiveRedDot = self.view.receiveRedDot,
    })
    self:_InitSearch({
        searchBtn = self.view.searchBtn,
    })
    self:_InitBigRewards({
        dayTxt = self.view.dayTxt,
        nameTxt = self.view.nameTxt,
        tipsTxt = self.view.tipsText,
    })
    self:_InitBigRewardsCarousel({
        leftBtn = self.view.leftBtn,
        rightBtn = self.view.rightBtn,
        searchBtn = self.view.searchBtn,
    })
    self:_InitFocus({
        focusBtn = self.view.rewardFocusBtn,
    })
    self:_InitPosition()
    self:_InitController()
end

HL.Commit(ActivityRewardRegistrationInfo)
return ActivityRewardRegistrationInfo