local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WikiTutorialTab = HL.Class('WikiTutorialTab', UIWidgetBase)


WikiTutorialTab._OnFirstTimeInit = HL.Override() << function(self)

end

WikiTutorialTab.InitWikiTutorialTab = HL.Method(HL.Table, HL.Function) << function(self, wikiEntryShowData, onItemClicked)
    self:_FirstTimeInit()

    self.view.btn.onClick:RemoveAllListeners()
    self.view.btn.onClick:AddListener(function()
        if onItemClicked then
            onItemClicked()
        end
    end)
    self.view.titleNormalTxt.text = wikiEntryShowData.wikiEntryData.desc
    self.view.titleSelectTxt.text = wikiEntryShowData.wikiEntryData.desc
    self:SetSelected(false)
end

WikiTutorialTab.SetSelected = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isSelected, playAnim)
    self.view.normalNode.gameObject:SetActive(not isSelected)
    if playAnim then
        UIUtils.PlayAnimationAndToggleActive(self.view.selectAnimWrapper, isSelected)
    else
        self.view.selectAnimWrapper:ClearTween(false)
        self.view.selectNode.gameObject:SetActive(isSelected)
    end
end

HL.Commit(WikiTutorialTab)
return WikiTutorialTab

