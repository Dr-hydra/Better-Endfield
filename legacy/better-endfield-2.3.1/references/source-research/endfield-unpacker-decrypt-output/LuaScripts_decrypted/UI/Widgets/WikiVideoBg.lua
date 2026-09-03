local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WikiVideoBg = HL.Class('WikiVideoBg', UIWidgetBase)

local VIDEO_START_KEY = "ui_wiki_entry_start"
local VIDEO_LOOP_KEY = "ui_wiki_entry_loop"


WikiVideoBg._OnFirstTimeInit = HL.Override() << function(self)
end

WikiVideoBg.InitWikiVideoBg = HL.Method() << function(self)
    self:_FirstTimeInit()



















end

WikiVideoBg._OnEnable = HL.Override() << function(self)




















end

WikiVideoBg._OnDisable = HL.Override() << function(self)

end

HL.Commit(WikiVideoBg)
return WikiVideoBg

