
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityStartReminderPopup

ActivityStartReminderPopupCtrl = HL.Class('ActivityStartReminderPopupCtrl', uiCtrl.UICtrl)






ActivityStartReminderPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivityStartReminderPopupCtrl.arg = HL.Field(HL.Any)

ActivityStartReminderPopupCtrl.m_id = HL.Field(HL.String) << ""

ActivityStartReminderPopupCtrl.m_conditionCells = HL.Field(HL.Any)

ActivityStartReminderPopupCtrl.m_firstCell = HL.Field(HL.Any)

ActivityStartReminderPopupCtrl.m_activity = HL.Field(HL.Any)

ActivityStartReminderPopupCtrl.m_conditions = HL.Field(HL.Table)

ActivityStartReminderPopupCtrl.m_drawMode = HL.Field(HL.Number) << 1

ActivityStartReminderPopupCtrl.m_source = HL.Field(HL.String) << ""

local ClientActivityConditionHandleInfoTable = {
    [GEnums.ConditionType.MissionStateEqual] = {
        GetProgress = function()
            local _,desc = Utils.getCurMissionIdAndDesc("activity")
            return desc
        end,
        IsComplete = function(condition)
            return GameInstance.player.mission:GetMissionState(condition.parameters[0].valueStringList[0]) == CS.Beyond.Gameplay.MissionSystem.MissionState.Completed
        end
    },
    [GEnums.ConditionType.QuestStateEqual] = {
        GetProgress = function()
            local _,desc = Utils.getCurMissionIdAndDesc("activity")
            return desc
        end,
        IsComplete = function(condition)
            return GameInstance.player.mission:GetMissionState(condition.parameters[0].valueStringList[0]) == CS.Beyond.Gameplay.MissionSystem.MissionState.Completed
        end
    },
}


ActivityStartReminderPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.arg = arg

    self.view.btnClose.onClick:AddListener(function()
        self:_Close()
    end)
    self.view.mask.onClick:AddListener(function()
        self:_Close()
    end)


    if arg then
        if arg.activityId then
            local activitySystem = GameInstance.player.activitySystem
            local _, activityData = Tables.activityTable:TryGetValue(arg.activityId)
            self.m_activity = activitySystem:GetActivity(arg.activityId)
            self.view.reminderTitleTxt.text = string.format(Language.LUA_ACTIVITY_JOIN_REMINDER,activityData.name)
            self.view.titleText.text = Language.LUA_TASK_TITLE_POPUP_ACTIVITY
            self.m_source = "Activity"

            self.m_conditions = {}
            for i = 1,#activityData.conditions do
                table.insert(self.m_conditions,activityData.conditions[CSIndex(i)])
            end
            self.m_drawMode = ActivityConst.ACTIVITY_REMINDER_DRAW_MODE.All
        elseif arg.conditions and arg.title then
            self.view.reminderTitleTxt.text = arg.title
            self.view.titleText.text = arg.mainTitle
            self.m_conditions = arg.conditions
            self.m_drawMode = arg.drawMode or ActivityConst.ACTIVITY_REMINDER_DRAW_MODE.All
            self.m_source = "Mission"
        end
    end


    if self.m_conditions then
        self.m_conditionCells = UIUtils.genCellCache(self.view.reminderItemCell)
        self.m_conditionCells:Refresh(#self.m_conditions, function(cell, index)
            self:_OnUpdateCell(cell,index)
        end)
    end


    if DeviceInfo.usingController then
        self:SetNaviTarget(self.m_firstCell.button)
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputBindingGroupMonoTarget.groupId })
    end
end

ActivityStartReminderPopupCtrl._OnUpdateCell = HL.Method(HL.Any,HL.Number) << function(self, cell, index)
    if index == 1 then
        self.m_firstCell = cell
    end


    local condition = self.m_conditions[index]
    local isComplete
    local progress
    if self.m_source == "Activity" then

        local info = ClientActivityConditionHandleInfoTable[condition.conditionType]
        if info ~= nil then
            progress = info.GetProgress()
            isComplete = info.IsComplete(condition)
        else
            progress = self.m_activity:GetProgress(condition.conditionId)
            if not progress then
                progress = 0
            end
            isComplete = Utils.compareInt(progress,condition.progressToCompare,condition.compareOperator)
        end
        cell.contentTxt.text = string.format(condition.tips, progress)
    else
        isComplete = condition.isComplete
        cell.contentTxt.text = condition.tips
    end
    if cell.contentTxt.text == "" then
        cell.stateController:SetState("NoTips")
    end


    if isComplete and self.m_drawMode == ActivityConst.ACTIVITY_REMINDER_DRAW_MODE.NoComplete then
        cell.gameObject:SetActive(false)
    elseif isComplete then
        cell.stateController:SetState("Complete")
        cell.button:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.None)
    elseif condition.jumpId then
        cell.stateController:SetState("Goto")
        cell.button:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.PressConfirmTriggerOnClick)
        cell.btnGoto.onClick:AddListener(function()
            local succ, cfg = Tables.systemJumpTable:TryGetValue(condition.jumpId)
            if not succ then
                logger.error("no such jumpId")
                return
            end
            Utils.jumpToSystem(condition.jumpId)
            self:_Close(true)
        end)
    else
        cell.stateController:SetState("Empty")
        cell.button:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.None)
    end


    cell.titleTxt.text = condition.desc


    if DeviceInfo.usingController then
        cell.btnGoto.enabled = false
        cell.keyHint.gameObject:SetActive(false)
        cell.button.onIsNaviTargetChanged = function(active)
            cell.btnGoto.enabled = active
            cell.keyHint.gameObject:SetActive(active)
        end
    end
end

ActivityStartReminderPopupCtrl._Close = HL.Method(HL.Opt(HL.Boolean)) << function(self, fast)
    if fast then
        UIManager:Close(PANEL_ID)
    else
        self:PlayAnimationOutAndClose()
    end

end

HL.Commit(ActivityStartReminderPopupCtrl)
