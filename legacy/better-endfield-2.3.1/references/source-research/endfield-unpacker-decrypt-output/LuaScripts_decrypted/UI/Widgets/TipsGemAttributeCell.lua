local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

TipsGemAttributeCell = HL.Class('TipsGemAttributeCell', UIWidgetBase)


TipsGemAttributeCell._OnFirstTimeInit = HL.Override() << function(self)

end

TipsGemAttributeCell.InitTipsGemAttributeCell = HL.Method() << function(self)
    self:_FirstTimeInit()
end

TipsGemAttributeCell.RefreshUI = HL.Method(HL.Number, HL.Table) << function(self, index, termList)
    self.view.titleTxt.text = Language["LUA_GEMCUSTOMIZATIONBOX_TAB_GROUP_ATTR_GROUP_NAME" .. index]

    local termNameList = {}
    for _, termId in ipairs(termList) do
        local _, termCfg = Tables.gemTable:TryGetValue(termId)
        if termCfg then
            local skillNameFormat = Language.LUA_GEM_CARD_SKILL_ACTIVE
            local name = string.format(skillNameFormat, termCfg.tagName)
            table.insert(termNameList, name)
        end
    end

    local joinChar = Language.LUA_GEMCUSTOMIZATIONBOX_TERMSTRING_JOIN
    self.view.concentTxt.text = table.concat(termNameList, joinChar)
end

HL.Commit(TipsGemAttributeCell)
return TipsGemAttributeCell

