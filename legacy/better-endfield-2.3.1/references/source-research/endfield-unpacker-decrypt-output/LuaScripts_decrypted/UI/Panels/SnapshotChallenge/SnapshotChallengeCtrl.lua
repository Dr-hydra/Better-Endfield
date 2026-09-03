
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SnapshotChallenge
local PHASE_ID = PhaseId.SnapshotChallenge


local activitySystem = GameInstance.player.activitySystem


SnapshotChallengeCtrl = HL.Class('SnapshotChallengeCtrl', uiCtrl.UICtrl)






SnapshotChallengeCtrl.s_messages = HL.StaticField(HL.Table) << {
}



SnapshotChallengeCtrl.m_activityId = HL.Field(HL.String) << ""

SnapshotChallengeCtrl.m_defaultStageId = HL.Field(HL.String) << ""

local WAIT_FOR_ANIM_TIME = 0.5





SnapshotChallengeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitArg(arg)
    if not self:_CheckActivityExist() then
        return
    end
    local path = ActivityUtils.GetSnapshotChallengeMainNodePath(self.m_activityId)
    local prefab = self.m_phase.m_resourceLoader:LoadGameObject(path)
    local mainObj = CSUtils.CreateObject(prefab, self.view.transform)
    mainObj.name = "Main"
    local mainCell = Utils.wrapLuaNode(mainObj)
    self.view.main = mainCell
    self.view.main:InitSnapshotChallengeMainInfo(self, self.m_activityId, self.m_defaultStageId)
    self.view.notchAdapter:FindAllSideUI(true)
    self.view.notchAdapter:ApplyNotch()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    ActivityUtils.backToMainHudWhenActivityClosed(self, self.m_activityId)
    self.view.animationWrapper:PlayInAnimation()
end

SnapshotChallengeCtrl._InitArg = HL.Method(HL.Any) << function(self, arg)
    if type(arg) == "string" then
        self.m_activityId = arg
    else
        self.m_activityId = arg.activityId
        self.m_defaultStageId = arg.stageId or ""
    end
end

SnapshotChallengeCtrl._CheckActivityExist = HL.Method().Return(HL.Boolean) << function(self)

    if not activitySystem:GetActivity(self.m_activityId) then
        self:_StartCoroutine(function()
            coroutine.wait(WAIT_FOR_ANIM_TIME)
            Notify(MessageConst.SHOW_TOAST,Language.LUA_ACTIVITY_FORBIDDEN)
            PhaseManager:PopPhase(PHASE_ID)
        end)
        return false
    end
    return true
end

SnapshotChallengeCtrl.OnClose = HL.Override() << function(self)
    self.view.main:OnClose()
end


HL.Commit(SnapshotChallengeCtrl)
