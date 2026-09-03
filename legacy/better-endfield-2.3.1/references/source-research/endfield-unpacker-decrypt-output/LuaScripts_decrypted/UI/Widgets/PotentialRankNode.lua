local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

PotentialRankNode = HL.Class('PotentialRankNode', UIWidgetBase)


PotentialRankNode._OnFirstTimeInit = HL.Override() << function(self)
end

PotentialRankNode.InitPotentialRankNode = HL.Method(HL.Number) << function(self, charInstId)
    self:_FirstTimeInit()


    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local curPotential = charInfo.potentialLevel

    self.view.potentialStar:InitCharPotentialStar(charInstId)
    self.view.curNum.text = tostring(curPotential)
    self.view.maxNum.text = curPotential == UIConst.CHAR_MAX_POTENTIAL and "MAX" or tostring(UIConst.CHAR_MAX_POTENTIAL)
end

HL.Commit(PotentialRankNode)
return PotentialRankNode

