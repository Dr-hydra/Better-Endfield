local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityFreeMonthlyPassPopUp

ActivityFreeMonthlyPassPopUpCtrl = HL.Class('ActivityFreeMonthlyPassPopUpCtrl', uiCtrl.UICtrl)

ActivityFreeMonthlyPassPopUpCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityFreeMonthlyPassPopUpCtrl.m_activityData = HL.Field(CS.Beyond.Gameplay.ActivityCalendarCheckin)

ActivityFreeMonthlyPassPopUpCtrl.m_closeCallback = HL.Field(HL.Function)

ActivityFreeMonthlyPassPopUpCtrl.m_rewardData = HL.Field(HL.Table)

ActivityFreeMonthlyPassPopUpCtrl.m_haveGotReward = HL.Field(HL.Boolean) << false


ActivityFreeMonthlyPassPopUpCtrl.m_fallbackTimerId = HL.Field(HL.Number) << -1





ActivityFreeMonthlyPassPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHECK_IN_REWARD] = '_OnRewardInfo',
    [MessageConst.ON_ACTIVITY_CALENDAR_CHECK_IN] = '_OnCheckIn',
}


ActivityFreeMonthlyPassPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_activityId = arg.activityId
    self.m_activityData = GameInstance.player.activitySystem:GetActivity(arg.activityId)
    self.m_closeCallback = arg.closeCallback or function()
        self:Close()
    end
    self.m_rewardData = {}

    self:_BindUICallback()


    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder(
        { self.view.inputGroup.groupId })


    UIManager:Open(PanelId.ActivityFreeMonthlyPass3D, {
        activityId = self.m_activityId,
        isDailyPopup = true,
    })

    local _, serverTodayGot, _ = ActivityUtils.CalendarCheckInGetCurDayNumber(self.m_activityId)
    if serverTodayGot then
        self.view.contentState:SetState("AcquireAfter")
    end

    self.view.activityCommonInfo:InitActivityCommonInfo(arg)
    self:_RefreshUI()


    ActivityUtils.actionWhenActivityClosed(function()
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
        self.m_closeCallback()
    end, self, self.m_activityId)
end

ActivityFreeMonthlyPassPopUpCtrl.OnClose = HL.Override() << function(self)
    UIManager:Close(PanelId.ActivityFreeMonthlyPass3D)
end



ActivityFreeMonthlyPassPopUpCtrl._BindUICallback = HL.Method() << function(self)
    self.view.emptyClick.onClick:AddListener(function()
        self:_OnBgClick()
    end)
end

ActivityFreeMonthlyPassPopUpCtrl._OnBgClick = HL.Method() << function(self)
    local _, serverTodayGot, _ = ActivityUtils.CalendarCheckInGetCurDayNumber(self.m_activityId)
    if serverTodayGot then
        self.view.emptyClick.interactable = false
        self.m_closeCallback()
        return
    end
    if self.m_haveGotReward then
        return
    end
    self.m_haveGotReward = true
    self.m_activityData:GainReward()
    self.m_fallbackTimerId = self:_StartTimer(10, function()
        logger.error("ActivityFreeMonthlyPassPopUp: GainReward 10s timeout, force close. activityId: ", self.m_activityId)
        self.m_closeCallback()
    end)
end





ActivityFreeMonthlyPassPopUpCtrl._RefreshUI = HL.Method() << function(self)
    local receiveStateNode = self.view.receiveStateNode

    local todayNumber, todayGetReward, allGetReward = ActivityUtils.CalendarCheckInGetCurDayNumber(self.m_activityId)

    receiveStateNode.receiveNumTxt.text = string.format(Language.LUA_FREE_MONTHLY_PASS_GET_REWARD_DAYS, self.m_activityData.rewardDays)
    if allGetReward then
        receiveStateNode.stateController:SetState("AllReceive")
    else
        receiveStateNode.stateController:SetState("Receive")
    end
end





ActivityFreeMonthlyPassPopUpCtrl._OnRewardInfo = HL.Method(HL.Table) << function(self, args)
    local rewardPack = unpack(args)
    self.m_rewardData = {
        items = rewardPack.itemBundleList,
        chars = rewardPack.chars,
        onComplete = function()
            if self.m_closeCallback then
                self.m_closeCallback()
            end
        end
    }
end

ActivityFreeMonthlyPassPopUpCtrl._OnCheckIn = HL.Method(HL.Table) << function(self, args)
    local id = unpack(args)
    if id ~= self.m_activityId then
        return
    end
    self.m_fallbackTimerId = self:_ClearTimer(self.m_fallbackTimerId)

    self.view.contentState:SetState("AcquireAfter")
    self.view.emptyClick.interactable = false
    self:_RefreshUI()


    local _, _, allGetReward = ActivityUtils.CalendarCheckInGetCurDayNumber(self.m_activityId)
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
    else
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, self.m_rewardData)
    end
end



HL.Commit(ActivityFreeMonthlyPassPopUpCtrl)
