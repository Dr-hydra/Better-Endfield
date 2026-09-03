local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityScratchOffLottery

ActivityScratchOffLotteryCtrl = HL.Class('ActivityScratchOffLotteryCtrl', uiCtrl.UICtrl)






ActivityScratchOffLotteryCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_UPDATED] = "_OnActivityUpdated",
}

ActivityScratchOffLotteryCtrl.m_activityId = HL.Field(HL.String) << ""

ActivityScratchOffLotteryCtrl.m_activityData = HL.Field(HL.Userdata)

ActivityScratchOffLotteryCtrl.m_activity = HL.Field(HL.Userdata)

ActivityScratchOffLotteryCtrl.m_lotteryInstanceId = HL.Field(HL.Number) << 0


ActivityScratchOffLotteryCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local activitySystem = GameInstance.player.activitySystem

    self.m_activityId = args.activityId
    self.m_activityData = Tables.activityTable:GetValue(args.activityId)
    self.m_activity = activitySystem:GetActivity(args.activityId)
    self.m_lotteryInstanceId = self.m_activity.lotteryInstanceId

    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self.view.activityCommonInfo.view.gotoNode.btnDetail.onClick:AddListener(function()
        self:_GotoScratch()
    end)

    local lotteryInfoArgs = {
        activityId = args.activityId,
    }
    self.view.lotteryInfo:InitActivityScratchOffLotteryInfo(lotteryInfoArgs)
    self.view.lotteryInfo.view.scratchAreaBtn.onClick:AddListener(function()
        self:_GotoScratch()
    end)
end

ActivityScratchOffLotteryCtrl.OnShow = HL.Override() << function(self)
    self:_UpdateView()
end

ActivityScratchOffLotteryCtrl.OnAnimationInFinished = HL.Override() << function(self)
    self.view.lotteryInfo:PlayStampAnimationIn()
end

ActivityScratchOffLotteryCtrl._UpdateView = HL.Method() << function(self)
    self.view.lotteryInfo:Refresh()
    self:_UpdateGotoNode()
end

ActivityScratchOffLotteryCtrl._UpdateGotoNode = HL.Method() << function(self)
    local activity = self.m_activity
    local node = self.view.activityCommonInfo.view.gotoNode

    local stateName
    if activity.isScratchCompleted then

        stateName = "ScratchCompleted"
        if activity.isCompleted then

            node.scratchTimeText:StopCountDown()
            node.scratchTimeText.view.text.text = Language.LUA_ACTIVITY_SCRATCH_OFF_LOTTERY_ALL_COMPLETED
        else

            local refreshTime = Utils.getNextCommonServerRefreshTime()
            node.scratchTimeText:InitCountDownText(refreshTime, nil, function(leftTime)
                return string.format(Language.LUA_ACTIVITY_SCRATCH_OFF_LOTTERY_COMPLETED_TODAY, UIUtils.getShortLeftTime(leftTime))
            end)
        end
    else

        stateName = "Detail"
    end
    node.stateController:SetState(stateName)
end

ActivityScratchOffLotteryCtrl._OnActivityUpdated = HL.Method(HL.Any) << function(self, args)
    local activityId = unpack(args)
    if activityId ~= self.m_activityId then
        return
    end


    if self.m_lotteryInstanceId ~= self.m_activity.lotteryInstanceId then
        self.m_phase:RemovePhasePanelItemById(PanelId.ActivityScratchOffLotteryPopup)
    end

    self:_UpdateView()
end

ActivityScratchOffLotteryCtrl._GotoScratch = HL.Method() << function(self)
    if self.m_activity.isScratchCompleted then
        return
    end
    self.m_phase:CreatePhasePanelItem(PanelId.ActivityScratchOffLotteryPopup, {
        activityId = self.m_activityId,
    })
end

HL.Commit(ActivityScratchOffLotteryCtrl)
