local readingPopUpPaperCtrl = require_ex('UI/Panels/ReadingPopUpPaper/ReadingPopUpPaperCtrl')
local PANEL_ID = PanelId.ReadingPopUpElec

ReadingPopUpElecCtrl = HL.Class('ReadingPopUpElecCtrl', readingPopUpPaperCtrl.ReadingPopUpPaperCtrl)


ReadingPopUpElecCtrl.m_radioId = HL.Field(HL.String) << ""


ReadingPopUpElecCtrl.m_overrideRadioId = HL.Field(HL.String) << ""



ReadingPopUpElecCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    ReadingPopUpElecCtrl.Super.OnCreate(self, arg)
    self.m_radioId = arg.radioId or ""
    self.m_overrideRadioId = arg.overrideRadioId or ""
end


ReadingPopUpElecCtrl._ShowContent = HL.Override() << function(self)
    local isRadio = not string.isEmpty(self.m_radioId)

    self.view.richContent.gameObject:SetActive(not isRadio)
    self.view.prtsRadio.gameObject:SetActive(isRadio)

    if isRadio then
        local title = ""
        local hasCfg, readingPopCfg = Tables.readingPopUpTable:TryGetValue(self.m_arg.readingPopId)
        if hasCfg then
            title = readingPopCfg.title
        end
        self.view.prtsRadio:InitPRTSRadio(self.m_radioId, title, self.m_overrideRadioId)
        self.view.prtsRadio:SetPlayRadio(true)
        self:_RefreshIcon()
    else
        ReadingPopUpElecCtrl.Super._ShowContent(self)
    end
end

HL.Commit(ReadingPopUpElecCtrl)
