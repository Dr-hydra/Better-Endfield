

local ActivityCheckInBase = require_ex('UI/Widgets/ActivityCheckInBase')

ActivityCharSignCommon = HL.Class('ActivityCharSignCommon', ActivityCheckInBase)

ActivityCharSignCommon.Init = HL.Virtual(HL.Table) << function(self, args)
    self.view.cell.gameObject:SetActive(false)
    self:_InitAnim({
        replayScrollAnimOnShow = args.replayScrollAnimOnShow,
    })
    self:_InitScrollList({
        scrollList = self.view.signScrollList,
        rewardCell = self.view.cell,
    })
    self:_InitActivityInfo({
        activityId = args.activityId,
        isPopup = args.isPopup,
    })
    self:_InitReceiveAll({
        receiveAllBtn = self.view.allReceiveBtn,
        receiveRedDot = self.view.receiveRedDot,
    })
    self:_InitPosition()
    self:_InitController({})
end

HL.Commit(ActivityCharSignCommon)
return ActivityCharSignCommon