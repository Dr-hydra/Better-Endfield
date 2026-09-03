local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

TeamCell = HL.Class('TeamCell', UIWidgetBase)

TeamCell.index = HL.Field(HL.Number) << -1

TeamCell.data = HL.Field(HL.Table)




TeamCell._OnFirstTimeInit = HL.Override() << function(self)
end

TeamCell.InitTeamCell = HL.Method(HL.Any, HL.Opt(HL.Function)) << function(self, data, onClickItem)
    self:_FirstTimeInit()
    self.index = data.index
    self.data = data
    self:SetSelect(false)
    if self.view.btn then
        self.view.btn.onClick:RemoveAllListeners()
        self.view.btn.onClick:AddListener(function()
            if onClickItem then
                onClickItem()
            end
        end)
    end
    self.view.textNum.text = string.format("%02d", self.index)
end

TeamCell.SetSelect = HL.Method(HL.Boolean) << function(self, isSelected)
    self.view.stateController:SetState(isSelected and 'Selected' or 'Normal')
end

HL.Commit(TeamCell)
return TeamCell
