AsyncActionHelper = HL.Class('AsyncActionHelper')

AsyncActionHelper.isParallel = HL.Field(HL.Boolean) << false

AsyncActionHelper.m_actions = HL.Field(HL.Table)

AsyncActionHelper.m_actionCount = HL.Field(HL.Number) << 0

AsyncActionHelper.m_curFinishedCount = HL.Field(HL.Number) << 0

AsyncActionHelper.m_curExecutingActionsKey = HL.Field(HL.Number) << 0

AsyncActionHelper.m_onFinished = HL.Field(HL.Function)

AsyncActionHelper.m_executing = HL.Field(HL.Boolean) << false

AsyncActionHelper.AsyncActionHelper = HL.Constructor(HL.Boolean) << function(self, isParallel)
    self.m_actions = {}
    self.isParallel = isParallel
end

AsyncActionHelper.AddAction = HL.Method(HL.Function) << function(self, action)
    if self.m_executing then
        logger.error("AsyncActionHelper.AddAction 失败！因为正在执行事件！")
        return
    end
    table.insert(self.m_actions, action)
    self.m_actionCount = self.m_actionCount + 1
end

AsyncActionHelper.SetOnFinished = HL.Method(HL.Function) << function(self, action)
    self.m_onFinished = action
end

AsyncActionHelper.Clear = HL.Method() << function(self)
    if self.m_executing then
        logger.error("AsyncActionHelper.Clear 失败！因为正在执行事件！")
        return
    end
    self.m_actions = {}
    self.m_actionCount = 0
    self.m_onFinished = nil
end

AsyncActionHelper.ForceClear = HL.Method() << function(self)
    if self.m_executing then
        self.m_executing = false
        self.m_curExecutingActionsKey = self.m_curExecutingActionsKey + 1
    end
    self:Clear()
end

AsyncActionHelper.Start = HL.Method() << function(self)
    self.m_curFinishedCount = 0
    local curExecutingActionsKey = self.m_curExecutingActionsKey + 1
    self.m_curExecutingActionsKey = curExecutingActionsKey

    if self.m_executing then
        logger.error("AsyncActionHelper.Start 失败！因为正在执行事件！")
        return
    end

    if self.m_actionCount == 0 then
        self:_OnFinished()
        return
    end

    self.m_executing = true
    if self.isParallel then

        local onFinished = function()
            if self.m_curExecutingActionsKey ~= curExecutingActionsKey then

                return
            end
            self.m_curFinishedCount = self.m_curFinishedCount + 1

            if self.m_curFinishedCount == self.m_actionCount then
                self:_OnFinished()
            end
        end
        for _, act in ipairs(self.m_actions) do
            act(onFinished)
        end
    else

        self:_StartNextAct(curExecutingActionsKey)
    end
end

AsyncActionHelper._StartNextAct = HL.Method(HL.Number) << function(self, curExecutingActionsKey)
    if self.m_curExecutingActionsKey ~= curExecutingActionsKey then

        return
    end

    if self.m_curFinishedCount == self.m_curFinishedCount then
        self:m_onFinished()
        return
    end

    local act = self.m_actions[self.m_curFinishedCount + 1]
    act(function()
        self.m_curFinishedCount = self.m_curFinishedCount + 1
        self:_StartNextAct(curExecutingActionsKey)
    end)
end

AsyncActionHelper._OnFinished = HL.Method() << function(self)
    self.m_executing = false
    self.m_curFinishedCount = 0
    self.m_curExecutingActionsKey = self.m_curExecutingActionsKey + 1

    if self.m_onFinished then
        self.m_onFinished()
    end
end

AsyncActionHelper.IsExecuting = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_executing
end


HL.Commit(AsyncActionHelper)
return AsyncActionHelper
