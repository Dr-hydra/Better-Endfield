
local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SimplePotentialStar = HL.Class('SimplePotentialStar', UIWidgetBase)


SimplePotentialStar.m_potentialCellList = HL.Field(HL.Table)



SimplePotentialStar._OnFirstTimeInit = HL.Override() << function(self)
    self.m_potentialCellList = {}
    for i = 1, UIConst.CHAR_MAX_POTENTIAL do
        local potentialCellName = 'potentialCell' .. i
        local cell = self.view[potentialCellName]
        self.m_potentialCellList[i] = cell
    end
end

SimplePotentialStar.InitCharSimplePotentialStar = HL.Method(HL.Number) << function(self, charInstId)

    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local pLv = charInfo.potentialLevel
    self:_InitStars(pLv)
end

SimplePotentialStar.InitWeaponSimplePotentialStar = HL.Method(HL.Number) << function(self, pLv)
    self:_InitStars(pLv)
end

SimplePotentialStar._InitStars = HL.Method(HL.Number) << function(self, pLv)
    self:_FirstTimeInit()

    for index, cell in ipairs(self.m_potentialCellList) do
        self:_InitPotentialCell(cell, index, pLv)
    end
    self.view.maxBG.gameObject:SetActive(pLv == UIConst.CHAR_MAX_POTENTIAL)
end


SimplePotentialStar._InitPotentialCell = HL.Method(CS.Beyond.UI.UIImage, HL.Number, HL.Number) << function(self, img, index, pLv)
    if index <= pLv then
        img.color = Color.white
        img.gameObject:SetActive(true)
    elseif index == pLv + 1 then
        img.color = UIConst.NEXT_POTENTIAL_STAR_COLOR
        img.gameObject:SetActive(true)
    else
        img.gameObject:SetActive(false)
    end
end

HL.Commit(SimplePotentialStar)
return SimplePotentialStar
