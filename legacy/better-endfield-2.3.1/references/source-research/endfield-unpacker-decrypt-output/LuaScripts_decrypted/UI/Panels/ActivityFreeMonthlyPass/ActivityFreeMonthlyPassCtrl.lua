
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityFreeMonthlyPass

ActivityFreeMonthlyPassCtrl = HL.Class('ActivityFreeMonthlyPassCtrl', uiCtrl.UICtrl)

ActivityFreeMonthlyPassCtrl.m_havePlayOut = HL.Field(HL.Boolean) << false

ActivityFreeMonthlyPassCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHECK_IN_REWARD] = '_OnRewardInfo',
    [MessageConst.ON_ACTIVITY_CALENDAR_CHECK_IN] = '_OnCheckIn',
    [MessageConst.ON_ACTIVITY_CALENDAR_CHECK_IN_UPDATE] = '_OnCalendarDataUpdate',

}

ActivityFreeMonthlyPassCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityFreeMonthlyPassCtrl.m_activityData = HL.Field(CS.Beyond.Gameplay.ActivityCalendarCheckin)

ActivityFreeMonthlyPassCtrl.m_rewardData = HL.Field(HL.Table)


ActivityFreeMonthlyPassCtrl.m_currUiTodayGetReward = HL.Field(HL.Boolean) << false



ActivityFreeMonthlyPassCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.m_activityData = GameInstance.player.activitySystem:GetActivity(args.activityId)
    self.m_rewardData = {}

    self:_BindUI()

    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self:_RefreshUI()


    ActivityUtils.backToMainHudWhenActivityClosed(self, self.m_activityId)


    UIManager:Open(PanelId.ActivityFreeMonthlyPass3D, {
        activityId = self.m_activityId,
        isDailyPopup = false,
    })
end

ActivityFreeMonthlyPassCtrl.OnHide = HL.Override() << function(self)
end

ActivityFreeMonthlyPassCtrl.OnClose = HL.Override() << function(self)

    if not self.m_havePlayOut then
        UIManager:Close(PanelId.ActivityFreeMonthlyPass3D)
    end
end

ActivityFreeMonthlyPassCtrl.PlayAnimationOut = HL.Override(HL.Opt(HL.Number)) << function(self, outCompleteActionType)
    self.m_havePlayOut = true
    ActivityFreeMonthlyPassCtrl.Super.PlayAnimationOut(self, outCompleteActionType)
    local isOpen, ctrl = UIManager:IsOpen(PanelId.ActivityFreeMonthlyPass3D)
    if isOpen then
        ctrl:PlayAnimationOut()
    end
end




ActivityFreeMonthlyPassCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if DeviceInfo.usingController then
        local keyHint = self.view.activityCommonInfo.transform:Find("GotoNode/BtnReceive/Root/KeyHint")
        keyHint.gameObject:SetActive(active)
    end
end





ActivityFreeMonthlyPassCtrl._BindUI = HL.Method() << function(self)
    self.view.btnReceive.button.onClick:AddListener(function()
        self:_OnGetRewardBtnClick()
    end)

    self.view.btnReceive.btnIntroMissionlRedDot:InitRedDot("ActivityCalendarCheckin", self.m_activityId)
end

ActivityFreeMonthlyPassCtrl._RefreshUI = HL.Method() << function(self)
    self.m_currUiTodayGetReward = self.m_activityData.curDayRewarded

    local receiveStateNode = self.view.receiveStateNode
    local btnReceive = self.view.btnReceive

    local todayNumber, todayGetReward, allGetReward = ActivityUtils.CalendarCheckInGetCurDayNumber(self.m_activityId)

    receiveStateNode.receiveNumTxt.text = string.format(Language.LUA_FREE_MONTHLY_PASS_GET_REWARD_DAYS, self.m_activityData.rewardDays)
    if allGetReward then
        receiveStateNode.stateController:SetState("AllReceive")
    else
        receiveStateNode.stateController:SetState("Receive")
    end

    local btnStateCtrl = btnReceive.root
    if not todayGetReward and not allGetReward then

        btnStateCtrl:SetState("NormalState")
    else

        if allGetReward then

            btnStateCtrl:SetState("DisableAllReceiveState")
        else

            btnStateCtrl:SetState("DisableTodayReceiveState")
        end
    end
end

ActivityFreeMonthlyPassCtrl._OnGetRewardBtnClick = HL.Method() << function(self)
    local _, todayGetReward, allGetReward = ActivityUtils.CalendarCheckInGetCurDayNumber(self.m_activityId)
    if todayGetReward or allGetReward then
        return
    end
    self.m_activityData:GainReward()
end

ActivityFreeMonthlyPassCtrl._OnRewardInfo = HL.Method(HL.Table) << function(self, args)
    local rewardPack = unpack(args)
    local reward = {
        items = rewardPack.itemBundleList,
        chars = rewardPack.chars,
        onComplete = function()
        end
    }

    self.m_rewardData = reward

end

ActivityFreeMonthlyPassCtrl._OnCheckIn = HL.Method(HL.Table) << function(self, args)
    local id = unpack(args)
    if id ~= self.m_activityId then
        return
    end

    local todayNumber, todayGetReward, allGetReward = ActivityUtils.CalendarCheckInGetCurDayNumber(self.m_activityId)
    if not self.m_currUiTodayGetReward and todayGetReward then

        self:_RefreshUI()

        if allGetReward then
            self.view.receiveStateNode.animationWrapper:Play("receivestateall_in")
        else
            self.view.receiveStateNode.animationWrapper:Play("receivestate_in")
        end

        local isOpen, ctrl = UIManager:IsOpen(PanelId.ActivityFreeMonthlyPass3D)
        if isOpen then
            ctrl:PlayGotDailyReward(function()
                Notify(MessageConst.SHOW_SYSTEM_REWARDS, self.m_rewardData)
            end)
        end
    end
end

ActivityFreeMonthlyPassCtrl._OnCalendarDataUpdate = HL.Method(HL.Table) << function(self, args)
    local id = unpack(args)
    if id ~= self.m_activityId then
        return
    end

    local todayNumber, todayGetReward, allGetReward = ActivityUtils.CalendarCheckInGetCurDayNumber(self.m_activityId)
    if self.m_currUiTodayGetReward and not todayGetReward then

        self:_RefreshUI()
        local isOpen, ctrl = UIManager:IsOpen(PanelId.ActivityFreeMonthlyPass3D)
        if isOpen then
            ctrl:SampleToAnimBegin()
        end
    end
end



HL.Commit(ActivityFreeMonthlyPassCtrl)
