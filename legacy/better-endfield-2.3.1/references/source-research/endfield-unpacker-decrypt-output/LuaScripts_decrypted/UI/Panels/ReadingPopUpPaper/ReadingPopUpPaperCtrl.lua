local readingPopUpCtrl = require_ex('UI/Panels/ReadingPopUp/ReadingPopUpCtrl')
local PANEL_ID = PanelId.ReadingPopUpPaper

ReadingPopUpPaperCtrl = HL.Class('ReadingPopUpPaperCtrl', readingPopUpCtrl.ReadingPopUpCtrl)

ReadingPopUpPaperCtrl._ShowContent = HL.Override() << function(self)
    ReadingPopUpPaperCtrl.Super._ShowContent(self)
    self:_RefreshIcon()
end

ReadingPopUpPaperCtrl._RefreshIcon = HL.Virtual() << function(self)
    local iconType = self.m_arg.iconType
    if iconType == GEnums.ReadingPopBlocType.None then
        self.view.logoImg.gameObject:SetActive(false)
        return
    end

    self.view.logoImg.gameObject:SetActive(true)
    local bgType = self.m_arg.bgType
    local hasCfg, rpIconCfg = Tables.readingPopUpIconTable:TryGetValue(iconType)
    if not hasCfg then
        return
    end
    local _, iconCfg = rpIconCfg.iconMap:TryGetValue(bgType)
    self.view.logoImg:LoadSprite(UIConst.UI_READING_POPUP_LOGO, iconCfg.icon)
end

HL.Commit(ReadingPopUpPaperCtrl)
