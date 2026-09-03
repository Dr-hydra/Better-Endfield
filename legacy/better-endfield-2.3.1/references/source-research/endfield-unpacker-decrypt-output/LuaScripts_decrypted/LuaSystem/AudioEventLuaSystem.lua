local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')
AudioEventLuaSystem = HL.Class('AudioEventLuaSystem', LuaSystemBase.LuaSystemBase)

AudioEventLuaSystem.AudioEventLuaSystem = HL.Constructor() << function(self)
    self:RegisterMessage(MessageConst.ON_FAC_MODE_CHANGE, function(toFactoryMode)
        CS.Beyond.Gameplay.Audio.Utils.AudioGameStateMonitor.Set(GEnums.AudioGameState.Factory, toFactoryMode)
    end)
end

AudioEventLuaSystem.OnInit = HL.Override() << function(self)
end

AudioEventLuaSystem.OnRelease = HL.Override() << function(self)
end

HL.Commit(AudioEventLuaSystem)
return AudioEventLuaSystem