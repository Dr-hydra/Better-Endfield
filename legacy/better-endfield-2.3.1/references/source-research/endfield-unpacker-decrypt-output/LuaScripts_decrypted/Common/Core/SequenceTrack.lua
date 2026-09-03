SequenceTrack = HL.Class("SequenceTrack")

do

    SequenceTrack.m_actionQueue = HL.Field(HL.Forward("Queue"))

    SequenceTrack.m_speed = HL.Field(HL.Number) << 1

    SequenceTrack.m_curAction = HL.Field(HL.Forward("ActionBase"))

    SequenceTrack.m_leftTime = HL.Field(HL.Number) << 0

    SequenceTrack.m_tickKey = HL.Field(HL.Number) << -1
end

SequenceTrack.SequenceTrack = HL.Constructor() << function(self)
    self:_OnInit()
end

SequenceTrack._OnInit = HL.Method() << function (self)
    self.m_actionQueue  = require_ex("Common/Utils/DataStructure/Queue")()
    self.m_tickKey = LuaUpdate:Add("TailTick", function(deltaTime)
        self:TailTick(deltaTime)
    end)
end

SequenceTrack.SetSpeed = HL.Method(HL.Number) << function (self, speed)
    self.m_speed = speed
end

SequenceTrack.AddAction = HL.Method(HL.Forward("ActionBase")) << function (self, action)
    self.m_actionQueue:Push(action)
end

SequenceTrack.TailTick = HL.Method(HL.Number) << function (self, deltaTime)
    local updateNow = self:_UpdateNowAction(deltaTime)
    if not updateNow then
        self:_PlayerNextAction()
    end
end

SequenceTrack.IsEnd = HL.Method().Return(HL.Boolean) << function (self)
    return not self.m_curAction and self.m_actionQueue:Empty()
end

SequenceTrack._UpdateNowAction = HL.Method(HL.Number).Return(HL.Boolean) << function (self, deltaTime)
    if self.m_curAction then
        self.m_curAction:Tick(deltaTime)

        if not self.m_curAction.isPlaying then
            self.m_curAction:Destroy()
            self.m_curAction = nil
        else
            self.m_leftTime = self.m_leftTime - deltaTime * self.m_speed
            if self.m_leftTime <= 0 then
                self.m_curAction:Stop()
                self.m_curAction:Destroy()
                self.m_curAction = nil
                self.m_leftTime = 0
            end
        end
        return true
    end
    return false
end

SequenceTrack._PlayerNextAction = HL.Method() << function (self)
    while not self.m_curAction and self.m_actionQueue:Size() > 0 do
        local nextAction = self.m_actionQueue:Pop()
        nextAction:Play()
        local duration = nextAction.duration
        if duration <= 0 then
            nextAction:Stop()
            nextAction:Destroy()
        else
            self.m_curAction = nextAction
            self.m_leftTime = nextAction.duration
        end
    end
end

SequenceTrack.Destroy = HL.Method() << function (self)
    if self.m_curAction then
        self.m_curAction:Stop()
        self.m_curAction:Destroy()
    end
    self.m_curAction = nil
    self.m_leftTime = 0
    self.m_actionQueue:Clear()

    if self.m_tickKey > 0 then
        LuaUpdate:Remove(self.m_tickKey)
    end
    self.m_tickKey = -1
end

HL.Commit(SequenceTrack)
