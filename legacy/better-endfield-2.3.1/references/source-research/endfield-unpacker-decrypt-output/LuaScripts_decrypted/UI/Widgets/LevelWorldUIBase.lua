local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

LevelWorldUiBase = HL.Class('LevelWorldUiBase', UIWidgetBase)


LevelWorldUiBase.InitLevelWorldUi = HL.Virtual(HL.Any) << function(self, args)
end


LevelWorldUiBase.OnLevelWorldUiReleased = HL.Virtual() << function(self)
end

HL.Commit(LevelWorldUiBase)
return LevelWorldUiBase