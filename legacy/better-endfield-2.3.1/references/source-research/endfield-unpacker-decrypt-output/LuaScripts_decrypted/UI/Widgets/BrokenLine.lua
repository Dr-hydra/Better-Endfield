local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

BrokenLine = HL.Class('BrokenLine', UIWidgetBase)


BrokenLine._OnFirstTimeInit = HL.Override() << function(self)

end

BrokenLine.InitBrokenLine = HL.Method(HL.Opt(HL.Table, HL.Number)) << function(self, points, count)
    self:_FirstTimeInit()

    if points then
        if count then
            self.view.brokenLine:SetYValue(points, count)
        else
            self.view.brokenLine:SetYValue(points, #points)
        end
    else
        self.view.brokenLine:SetYValue({0, 0}, 2)
    end
end

HL.Commit(BrokenLine)
return BrokenLine
