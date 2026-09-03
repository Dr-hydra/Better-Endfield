

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')

ActivityRewardRegistrationPopupCtrl = HL.Class('ActivityRewardRegistrationPopupCtrl', uiCtrl.UICtrl)

ActivityRewardRegistrationPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_UPDATED] = 'OnActivityUpdate',
}

ActivityRewardRegistrationPopupCtrl.m_closeCallback = HL.Field(HL.Function)


ActivityRewardRegistrationPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_closeCallback = args.closeCallback or function()
        self:Close()
    end
    self.view.btnClose.onClick:AddListener(function()
        self:_Close()
    end)
    self.view.autoCloseButtonUp.onClick:AddListener(function()
        self:_Close()
    end)
    self.view.autoCloseButtonDown.onClick:AddListener(function()
        self:_Close()
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder( { self.view.inputGroup.groupId } )


    local initArg = {
        activityId = self.view.config.ACTIVITY_ID,
        isPopup = true,
        animation = self.view.rewardStateNode,
        animNameList = { "bannerchange_page_out", "bannerchange_page_in" },
    }
    self.view.activityRewardRegistrationInfo:Init(initArg)
end

ActivityRewardRegistrationPopupCtrl.OnActivityUpdate = HL.Method(HL.Table) << function(self, args)
    local id = unpack(args)
    if id == self.view.config.ACTIVITY_ID and not GameInstance.player.activitySystem:GetActivity(id) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
        self:_Close()
    end
end

ActivityRewardRegistrationPopupCtrl.m_waitingForClose = HL.Field(HL.Boolean) << false

ActivityRewardRegistrationPopupCtrl._Close = HL.Method() << function(self)
    if self.view.animationWrapper.curState == UIConst.UI_ANIMATION_WRAPPER_STATE.Out then
        return
    end
    if not self:IsShow() then
        self.m_waitingForClose = true
        return
    end
    if UIManager:IsOpen(PanelId.RewardsPopUpForSystem) then
        UIManager:Close(PanelId.RewardsPopUpForSystem)
    end
    self.view.animationWrapper:PlayOutAnimation(function()
        self.m_closeCallback()
    end)
end

ActivityRewardRegistrationPopupCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)

    if self.view.activityRewardRegistrationInfo then
        self.view.activityRewardRegistrationInfo:OnPanelInputBlocked(active)
    end
end

ActivityRewardRegistrationPopupCtrl.OnShow = HL.Override() << function(self)
    if self.m_waitingForClose then
        self.m_waitingForClose = false
        self:_Close()
    end
end


HL.Commit(ActivityRewardRegistrationPopupCtrl)