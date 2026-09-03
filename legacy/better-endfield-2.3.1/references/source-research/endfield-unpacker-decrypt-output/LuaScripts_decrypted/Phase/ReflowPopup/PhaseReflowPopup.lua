
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.ReflowPopup

PhaseReflowPopup = HL.Class('PhaseReflowPopup', phaseBase.PhaseBase)





PhaseReflowPopup.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SHOW_REFLOW_ONE_TIME_REWARD] = {'_ShowReward', true},
    [MessageConst.ON_ACTIVITY_UPDATED] = {'_OnActivityUpdated',true},
}




PhaseReflowPopup._OnInit = HL.Override() << function(self)
    PhaseReflowPopup.Super._OnInit(self)
    UIManager:ToggleBlockObtainWaysJump("PhaseReflowPopup", true, {})
end

PhaseReflowPopup._ShowPopUp = HL.Method() << function(self)
    local id = self.arg.activityId

    self:CreatePhasePanelItem(PanelId.ReflowFormalDialogue, {
        activityId = id,
        closeCallback = function()

            local activity = GameInstance.player.activitySystem:GetActivity(id)
            if activity and not activity.oneTimeRewardReceived then
                GameInstance.player.activitySystem:SendGainReflowOneTimeReward(id)
            else
                PhaseManager:ExitPhaseFast(PHASE_ID)
            end
        end
    })
end

PhaseReflowPopup._OnDestroy = HL.Override() << function(self)
    PhaseReflowPopup.Super._OnDestroy(self)
    if self.arg and self.arg.closeCallback then
        self.arg.closeCallback()
    end
    UIManager:ToggleBlockObtainWaysJump("PhaseReflowPopup", false)
end

PhaseReflowPopup._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self:_ShowPopUp()
end

PhaseReflowPopup._ShowReward = HL.Method(HL.Any) << function(self, arg)
    local _, items, chars = unpack(arg)
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        items = items,
        chars = chars,
        showHint = true,
        onComplete = function()
            PhaseManager:ExitPhaseFast(PHASE_ID)
        end,
    })
end

PhaseReflowPopup._OnActivityUpdated = HL.Method(HL.Table) << function(self, arg)
    local activityId = unpack(arg)
    if activityId ~= self.arg.activityId then
        return
    end

    local activity = GameInstance.player.activitySystem:GetActivity(self.arg.activityId)
    if not activity then

        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
        if UIManager:IsOpen(PanelId.RewardsPopUpForSystem) then
            UIManager:Close(PanelId.RewardsPopUpForSystem)
        end
        Notify(MessageConst.HIDE_ITEM_TIPS)
        PhaseManager:ExitPhaseFast(PHASE_ID)
    end
end

HL.Commit(PhaseReflowPopup)

