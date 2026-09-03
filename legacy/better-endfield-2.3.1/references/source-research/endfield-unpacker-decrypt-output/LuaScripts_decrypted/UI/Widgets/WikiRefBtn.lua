local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')


WikiRefBtn = HL.Class('WikiRefBtn', UIWidgetBase)


WikiRefBtn._OnFirstTimeInit = HL.Override() << function(self)

end

WikiRefBtn.m_showingItemTips = HL.Field(HL.Boolean) << false

WikiRefBtn.m_showingWikiTips = HL.Field(HL.Boolean) << false

WikiRefBtn.InitWikiRefBtn = HL.Method(HL.String) << function(self, wikiEntryId)
    self:_FirstTimeInit()

    if self.m_showingWikiTips then
        Notify(MessageConst.HIDE_WIKI_REF_TIPS)

        self.m_showingWikiTips = false
    end
    if self.m_showingItemTips then
        Notify(MessageConst.HIDE_ITEM_TIPS)

        self.m_showingItemTips = false
    end


    self:SetSelected(false)


    local hasValue, wikiEntryData = Tables.wikiEntryDataTable:TryGetValue(wikiEntryId)

    if not hasValue then

        self.view.text.text = wikiEntryId
        return
    end

    if wikiEntryData.refItemId and not string.isEmpty(wikiEntryData.refItemId) then

        local hasValue, itemInfo = Tables.itemTable:TryGetValue(wikiEntryData.refItemId)
        self.view.text.text = itemInfo.name
    elseif wikiEntryData.refMonsterTemplateId and not string.isEmpty(wikiEntryData.refMonsterTemplateId) then

        local hasValue, monsterInfo = Tables.enemyTemplateDisplayInfoTable:TryGetValue(wikiEntryData.refMonsterTemplateId)
        self.view.text.text = monsterInfo.name
    else
        self.view.text.text = wikiEntryData.desc
    end
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()

        self:SetSelected(true)

        if wikiEntryData.refItemId and not string.isEmpty(wikiEntryData.refItemId) then
            if self.m_showingItemTips then
                return
            end
            Notify(MessageConst.HIDE_WIKI_REF_TIPS)
            self.m_showingItemTips = true
            Notify(MessageConst.SHOW_ITEM_TIPS, {
                transform = self.transform,

                hideItemObtainWays = false,
                hideBottomInfo = false,
                prefixDesc = "",

                itemId = wikiEntryData.refItemId,
                fromDepot = false,

                canPlace = false,
                canSplit = false,
                canUse = false,

                isSideTips = DeviceInfo.usingController,

                onClose = function()
                    self:OnTipClosed()
                end
            })
        else

            Notify(MessageConst.HIDE_ITEM_TIPS)
            self.m_showingWikiTips = true
            Notify(MessageConst.SHOW_WIKI_REF_TIPS, {
                transform = self.transform,
                wikiEntryId = wikiEntryId,
                onClose = function()
                    self:OnTipClosed()
                end
            })
        end
    end)
end


WikiRefBtn.OnTipClosed = HL.Method() << function(self)
    if NotNull(self.view.gameObject) then
        self:SetSelected(false)
    end
    self.m_showingItemTips = false
    self.m_showingWikiTips = false
end


WikiRefBtn.SetSelected = HL.Method(HL.Boolean) << function(self, isSelected)
    self.view.stateController:SetState(isSelected and 'select' or 'unselect')
end

HL.Commit(WikiRefBtn)
return WikiRefBtn