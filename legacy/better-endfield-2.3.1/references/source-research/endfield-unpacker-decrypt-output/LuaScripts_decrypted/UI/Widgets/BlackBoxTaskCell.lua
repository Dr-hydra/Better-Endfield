local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local OBJECTIVE_PROGRESS_TEXT_FORMAT = "%d/%d"

BlackBoxTaskCell = HL.Class('BlackBoxTaskCell', UIWidgetBase)

BlackBoxTaskCell.m_index = HL.Field(HL.Number) << -1

BlackBoxTaskCell.m_taskType = HL.Field(CS.Beyond.Gameplay.LevelScriptTaskType)

BlackBoxTaskCell.m_tracking = HL.Field(CS.Beyond.Gameplay.Core.LevelScriptTaskTracking)


BlackBoxTaskCell._OnFirstTimeInit = HL.Override() << function(self)

    self:RegisterMessage(MessageConst.ON_TASK_OBJECTIVE_PROGRESS_CHANGE, function(args)
        self:OnGoalProgressChange(args)
    end)
end

BlackBoxTaskCell.InitBlackBoxTaskCell = HL.Method(HL.Number, CS.Beyond.Gameplay.LevelScriptTaskType) << function(self, index, taskType)
    self.m_index = index
    self.m_taskType = taskType
    local trackingMgr = GameWorld.levelScriptTaskTrackingManager
    self.m_tracking = trackingMgr:GetTaskByType(self.m_taskType)
    self:_FirstTimeInit()
    self:_UpdateContent()
end

BlackBoxTaskCell.OnGoalProgressChange = HL.Method(HL.Any) << function(self, args)
    local taskType, csIndex = unpack(args)
    if self.m_taskType ~= taskType or self.m_index ~= LuaIndex(csIndex) then
        return
    end

    self:_UpdateContent()
end

BlackBoxTaskCell._UpdateContent = HL.Method() << function(self)
    if not self.m_tracking then
        return
    end

    local csIndex = CSIndex(self.m_index)
    local objective = self.m_tracking.objectives[csIndex]
    if not objective then
        return
    end

    local isCompleted = objective.isCompleted
    self.view.finish.gameObject:SetActive(isCompleted)
    self.view.unfinish.gameObject:SetActive(not isCompleted)

    local label = isCompleted and self.view.finishText or self.view.unfinishText
    local number = isCompleted and self.view.finishNumber or self.view.unfinishNumber

    local objectiveEnum = CS.Beyond.Gameplay.TaskObjectiveEnum.__CastFrom(csIndex)
    local success, objectiveInfo = self.m_tracking.extraInfo.trackingInfoDict:TryGetValue(objectiveEnum)
    local descText = ""
    local progressText = ""
    if success then
        descText = objectiveInfo.description:GetText()
        if objectiveInfo.needFormatProgress then
            progressText = string.format(OBJECTIVE_PROGRESS_TEXT_FORMAT, objective.progress, objective.maxProgress)
        end
    end

    label:SetAndResolveTextStyle(descText)
    number:SetAndResolveTextStyle(progressText)
end

HL.Commit(BlackBoxTaskCell)
return BlackBoxTaskCell

