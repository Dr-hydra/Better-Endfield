local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ItemSimple = HL.Class('ItemSimple', UIWidgetBase)


ItemSimple._OnFirstTimeInit = HL.Override() << function(self)
end

ItemSimple.InitItemSimple = HL.Method(HL.String, HL.Number) << function(self, itemId, itemCount)
    self:_FirstTimeInit()
    local data = Tables.itemTable[itemId]
    self.view.icon:InitItemIcon(data.id)
    local rarityColor = UIUtils.getItemRarityColor(data.rarity)
    self.view.rarityLine.color = rarityColor
    self.view.count.text = UIUtils.getNumString(itemCount)
end

HL.Commit(ItemSimple)
return ItemSimple
