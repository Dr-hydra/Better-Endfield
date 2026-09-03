local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MusicPlayerPopupCell = HL.Class('MusicPlayerPopupCell', UIWidgetBase)


MusicPlayerPopupCell._OnFirstTimeInit = HL.Override() << function(self)

end

MusicPlayerPopupCell.InitMusicPlayerPopupCell = HL.Method() << function(self)
    self:_FirstTimeInit()


end

MusicPlayerPopupCell.Init = HL.Method(HL.String, HL.Number) << function(self, name, duration)
    self.view.descTxt.text = name
    self.view.timeTxt.text = UIUtils.getLeftTimeToSecond(duration)
end

HL.Commit(MusicPlayerPopupCell)
return MusicPlayerPopupCell

