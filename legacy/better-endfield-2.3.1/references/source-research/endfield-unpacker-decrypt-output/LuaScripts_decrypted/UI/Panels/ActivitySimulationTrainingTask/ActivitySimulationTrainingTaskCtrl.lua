local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivitySimulationTrainingTask









ActivitySimulationTrainingTaskCtrl = HL.Class('ActivitySimulationTrainingTaskCtrl', uiCtrl.UICtrl)







ActivitySimulationTrainingTaskCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE] = '_OnTaskProgress',
    [MessageConst.ON_ACTIVITY_UPDATED] = '_OnLimitedActivityEnd',
}


ActivitySimulationTrainingTaskCtrl.m_activityId = HL.Field(HL.String) << ''


ActivitySimulationTrainingTaskCtrl.m_allTaskRewarded = HL.Field(HL.Boolean) << false





ActivitySimulationTrainingTaskCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)

    self.view.limitTaskBtn.gameObject:SetActive(true)
    self.view.limitTaskBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.SimulationTrainingTask, { enterFromActivityCenter = true })
    end)

    self.view.activityCommonInfo.view.gotoNode.btnDetailRedDot:InitRedDot("ActivitySimulationTrainingGotoDetailBtn", self.m_activityId)
    self.view.activityCommonInfo:UpdateGoToBtnDetailCallBack(function()
        ActivityUtils.SetSimulationTrainingGotoDetailRead()
    end)

    self:_UpdateTaskState()
    self.view.limitTaskRedDot:InitRedDot("ActivitySimulationTrainingLimitTaskRedDot", self.m_activityId)
    self:_UpdateLimitTaskDoneNode()

end



ActivitySimulationTrainingTaskCtrl._UpdateLimitTaskDoneNode = HL.Method() << function(self)
    self.view.limitTaskDoneNode.gameObject:SetActive(self.m_allTaskRewarded)
end

ActivitySimulationTrainingTaskCtrl._UpdateTaskState = HL.Method() << function(self)
    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if not activityData then
        self.m_allTaskRewarded = false
        self.view.limitTaskBtn.gameObject:SetActive(false)
        return
    end

    local _, taskConfig = Tables.activityConditionalMultiStageTaskConfigTable:TryGetValue(self.m_activityId)
    if not taskConfig then
        self.m_allTaskRewarded = false
        self.view.limitTaskBtn.gameObject:SetActive(false)
        return
    end

    local taskCount = 0
    for id, _ in pairs(taskConfig.TaskConfigMap) do
        local taskStatusData = activityData:GetTaskData(id)
        if taskStatusData ~= nil then
            taskCount = taskCount + 1
        end
    end

    if taskCount == 0 then
        self.m_allTaskRewarded = false
        self.view.limitTaskBtn.gameObject:SetActive(false)
        return
    end

    local allRewarded = true
    for id, _ in pairs(taskConfig.TaskConfigMap) do
        local taskStatusData = activityData:GetTaskData(id)
        if taskStatusData ~= nil then
            local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskStatusData.Status)
            if status ~= GEnums.ActivityConditionalTaskState.Rewarded then
                allRewarded = false
                break
            end
        else
            allRewarded = false
            break
        end
    end

    self.m_allTaskRewarded = allRewarded
end




ActivitySimulationTrainingTaskCtrl._OnTaskProgress = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if activityId ~= self.m_activityId then
        return
    end
    self:_UpdateTaskState()
    self:_UpdateLimitTaskDoneNode()
end




ActivitySimulationTrainingTaskCtrl._OnLimitedActivityEnd = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if activityId ~= self.m_activityId then
        return
    end

    local activity = GameInstance.player.activitySystem:GetActivity(activityId)
    if not activity then
        UIManager:Close(PANEL_ID)
    end
end

HL.Commit(ActivitySimulationTrainingTaskCtrl)
