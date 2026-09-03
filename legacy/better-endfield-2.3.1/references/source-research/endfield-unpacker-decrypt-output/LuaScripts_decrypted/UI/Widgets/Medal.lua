local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

Medal = HL.Class('Medal', UIWidgetBase)


Medal._OnFirstTimeInit = HL.Override() << function(self)

end

Medal.id = HL.Field(HL.String) << ''


Medal.InitMedal = HL.Method(HL.Opt(HL.Any))
    << function(self, medalBundle)



    self:_FirstTimeInit()
    local isEmpty = medalBundle == nil or string.isEmpty(medalBundle.achievementId)
    if isEmpty then
        self.view.stateCtrl:SetState("Empty")
        return
    end
    self:_RenderIcon(medalBundle)
end

Medal._RenderIcon = HL.Method(HL.Any) << function(self, medalBundle)
    local iconPath = (self.view.config.USE_ICON_BIG == true) and UIConst.UI_SPRITE_MEDAL_ICON_BIG or UIConst.UI_SPRITE_MEDAL_ICON
    local iconIdFormat = medalBundle.isPlated and UIConst.UI_SPRITE_MEDAL_ICON_PLATE_FORMAT or UIConst.UI_SPRITE_MEDAL_ICON_FORMAT
    local iconId = string.format(iconIdFormat, medalBundle.achievementId, medalBundle.level)
    local showRare = medalBundle.isRare and medalBundle.level >= Tables.achievementConst.levelDisplayEffect
    self.view.icon:LoadSprite(iconPath, iconId)
    self.view.stateCtrl:SetState(showRare and "Rare" or "Normal")
end

HL.Commit(Medal)
return Medal