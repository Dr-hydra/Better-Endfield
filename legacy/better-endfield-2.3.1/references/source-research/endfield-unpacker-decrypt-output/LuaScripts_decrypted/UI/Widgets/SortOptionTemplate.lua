local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local TextColorNormal = CS.UnityEngine.Color(105 / 255, 105 / 255, 103 / 255, 1)
local TextColorSelected = CS.UnityEngine.Color(232 / 255, 233 / 255, 232 / 255, 1)

SortOptionTemplate = HL.Class('SortOptionTemplate', UIWidgetBase)


SortOptionTemplate._OnFirstTimeInit = HL.Override() << function(self)

end

SortOptionTemplate.InitSortOptionTemplate = HL.Method(HL.String, HL.Opt(HL.Boolean)) << function(self, text, isSelected)
    self:_FirstTimeInit()

    self.view.txtOption.text = text
    self:SetSelectState(isSelected)
end

SortOptionTemplate.SetSelectState = HL.Method(HL.Opt(HL.Boolean)) << function(self, isSelected)
    if isSelected then
        self.view.txtOption.color = TextColorSelected
    else
        self.view.txtOption.color = TextColorNormal
    end
    self.view.normalNode.gameObject:SetActiveIfNecessary(not isSelected)
    self.view.selectedNode.gameObject:SetActiveIfNecessary(isSelected)
end

HL.Commit(SortOptionTemplate)
return SortOptionTemplate