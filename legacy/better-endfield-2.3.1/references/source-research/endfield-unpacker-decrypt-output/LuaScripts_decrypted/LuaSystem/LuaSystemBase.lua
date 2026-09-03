LuaSystemBase = HL.Class('LuaSystemBase')

LuaSystemBase.LuaSystemBase = HL.Constructor() << function(self)
end

LuaSystemBase.OnInit = HL.Virtual() << function(self)
end

LuaSystemBase.OnRelease = HL.Virtual() << function(self)
end

LuaSystemBase.Clear = HL.Method() << function(self)
    TimerManager:ClearAllTimer(self)
    CoroutineManager:ClearAllCoroutine(self)
    MessageManager:UnregisterAll(self)
end



LuaSystemBase._StartTimer = HL.Method(HL.Number, HL.Function, HL.Opt(HL.Boolean)).Return(HL.Number)
        << function(self, duration, func, unscaled)
    return TimerManager:StartTimer(duration, func, unscaled, self)
end

LuaSystemBase._ClearTimer = HL.Method(HL.Number).Return(HL.Number) << function(self, timer)
    TimerManager:ClearTimer(timer)
    return -1
end

LuaSystemBase._StartCoroutine = HL.Method(HL.Function).Return(HL.Thread) << function(self, func)
    return CoroutineManager:StartCoroutine(func, self)
end

LuaSystemBase._ClearCoroutine = HL.Method(HL.Thread).Return(HL.Any) << function(self, coroutine)
    CoroutineManager:ClearCoroutine(coroutine)
    return nil
end



LuaSystemBase.RegisterMessage = HL.Method(HL.Number, HL.Function) << function(self, msg, action)
    MessageManager:Register(msg, function(msgArg)
        action(msgArg)
    end, self)
end


HL.Commit(LuaSystemBase)
