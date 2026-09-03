local SNSContentBase = require_ex('UI/Widgets/SNSContentBase')

SNSContentItem = HL.Class('SNSContentItem', SNSContentBase)


SNSContentItem._OnSNSContentInit = HL.Override() << function(self)
    local itemId = self.m_contentCfg.contentParam[0]

    local itemTableData = Tables.itemTable[itemId]

    UIUtils.setItemRarityImage(self.view.rarity, itemTableData.rarity)

    self.view.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemTableData.iconId)
    self.view.nameTxt.text = itemTableData.name

    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = itemId,
            transform = self.view.button.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.RightTop
        })
    end)
end



SNSContentItem.CanSetTarget = HL.Override().Return(HL.Boolean) << function(self)
    return true
end

SNSContentItem.GetNaviTarget = HL.Override().Return(HL.Any) << function(self)
    return self.view.button
end



HL.Commit(SNSContentItem)
return SNSContentItem
