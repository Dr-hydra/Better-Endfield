local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharPotential = HL.Class('CharPotential', UIWidgetBase)


CharPotential._OnFirstTimeInit = HL.Override() << function(self)

end

CharPotential.InitCharPotential = HL.Method(HL.Number) << function(self, potentialLevel)
    self:_FirstTimeInit()

    for i = 1, 5 do
        local imageLine = self.view[string.format("line%02d", i)]
        if imageLine then
            local lineActive = false
            local lineColor = self.view.config.DEFAULT_COLOR
            if i < potentialLevel + 1 then
                lineActive = true
                lineColor = self.view.config.DEFAULT_COLOR
            elseif i == potentialLevel + 1 then
                lineActive = true
                lineColor = self.view.config.CURRENT_COLOR
            else
                lineActive = false
            end
            imageLine.gameObject:SetActive(lineActive)
            imageLine.color = lineColor
        end
    end
end

HL.Commit(CharPotential)
return CharPotential

