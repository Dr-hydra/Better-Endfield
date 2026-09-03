local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ItemTags = HL.Class('ItemTags', UIWidgetBase)

ItemTags.m_tagListCache = HL.Field(HL.Forward("UIListCache"))


ItemTags._OnFirstTimeInit = HL.Override() << function(self)
    if not self.m_tagListCache then
        self.m_tagListCache = UIUtils.genCellCache(self.view.tagCell)
    end
end

ItemTags.InitItemTags = HL.Method(HL.String) << function(self, itemId)
    self:_FirstTimeInit()

    local _, itemData = Tables.itemTable:TryGetValue(itemId)
    if not itemData then
        return
    end
    local hasTag ,tagIdList = UIUtils.tryGetTagList(itemData.id, itemData.type)
    self.m_tagListCache:Refresh(hasTag and tagIdList.Count or 0, function(cell, index)
        local tagId = tagIdList[CSIndex(index)]
        local _, tagData = Tables.factoryIngredientTagTable:TryGetValue(tagId)
        if tagData then
            cell.tagTxt.text = tagData.tagLabel
        end
    end)
end

HL.Commit(ItemTags)
return ItemTags

