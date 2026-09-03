local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

GemCustomizationBoxTermGroupCell = HL.Class('GemCustomizationBoxTermGroupCell', UIWidgetBase)

GemCustomizationBoxTermGroupCell.m_isPreview = HL.Field(HL.Boolean) << false

GemCustomizationBoxTermGroupCell.m_genTermCells = HL.Field(HL.Forward("UIListCache"))

GemCustomizationBoxTermGroupCell.m_test = HL.Field(HL.Number) << 1


GemCustomizationBoxTermGroupCell._OnFirstTimeInit = HL.Override() << function(self)
    if self.m_isPreview then
        self.m_genTermCells = UIUtils.genCellCache(self.view.gemCustomizationBoxTermCell)
    else
        self.m_genTermCells = UIUtils.genCellCache(self.view.gemCustomizationBoxTermCell)
    end
end

GemCustomizationBoxTermGroupCell.InitGemCustomizationBoxTermGroupCell = HL.Method(HL.Boolean) << function(self, isPreview)
    self.m_isPreview = isPreview
    self:_FirstTimeInit()
end

GemCustomizationBoxTermGroupCell.UpdateTermGroupUI = HL.Method(HL.Number, HL.Table, HL.Table)
    << function(self, luaIndex, termList, currSelectInfo)
    self:_RefreshViewTitle(luaIndex)

    local currTabCanSelectTermTypeIndexes = currSelectInfo["eachTabCanSelectTermTypeIndex"][currSelectInfo["selectTabIndex"]]
    local canSelectGroup = false
    for i, v in ipairs(currTabCanSelectTermTypeIndexes) do
        if v == luaIndex then
            canSelectGroup = true
        end
    end
    self.m_genTermCells:Refresh(#termList, function(termCell, index)
        local termId = termList[index]
        termCell:InitGemCustomizationBoxTermCell(termId)
        termCell:RefreshUI(termId, currSelectInfo, canSelectGroup)
    end)
end

GemCustomizationBoxTermGroupCell.UpdateTagGroupUI = HL.Method(HL.Number, HL.Table) << function(self, luaIndex, termList)
    self:_RefreshViewTitle(luaIndex)
    self.m_genTermCells:Refresh(#termList, function(termCell, index)
        local termId = termList[index]
        termCell:InitGemCustomizationBoxTermCell(termId)
        termCell:RefreshUIInPreviewMode(termId)
    end)
end

GemCustomizationBoxTermGroupCell.GetTermIdByTransform = HL.Method(HL.Any) << function(self, trans)
    local termCellItems = self.m_genTermCells:GetItems()
    for _, termCell in ipairs(termCellItems) do
        if termCell.transform == trans then
            local currTermId = termCell:GetTermId()
            return currTermId
        end
    end
    return ""
end

GemCustomizationBoxTermGroupCell._RefreshViewTitle = HL.Method(HL.Number) << function(self, luaIndex)
    self.view.titleTxt.text = Language["LUA_GEMCUSTOMIZATIONBOX_TAB_GROUP_ATTR_GROUP_NAME" .. luaIndex]
end

HL.Commit(GemCustomizationBoxTermGroupCell)
return GemCustomizationBoxTermGroupCell

