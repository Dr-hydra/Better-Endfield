local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ItemBagColoredSlotUniBG = HL.Class('ItemBagColoredSlotUniBG', UIWidgetBase)


ItemBagColoredSlotUniBG._OnFirstTimeInit = HL.Override() << function(self)
end

ItemBagColoredSlotUniBG.InitItemBagColoredSlotUniBG = HL.Method(CS.Beyond.UI.UIScrollList, HL.Number, HL.Opt(HL.Number)) << function(self, itemList, coloredSlotNum, extraOffsetY)
    self:_FirstTimeInit()

    if coloredSlotNum == 0 then
        self.view.gameObject:SetActive(false)
        return
    end

    local countPerLine = itemList.countPerLine
    local extraCount = coloredSlotNum % countPerLine
    local isSameLine = coloredSlotNum <= countPerLine or extraCount == 0
    local lineCount = math.ceil(coloredSlotNum / countPerLine)


    local realPadding = itemList:GetPadding(true)
    if extraOffsetY then
        self.view.rectTransform.anchoredPosition = Vector2(realPadding.left, -(realPadding.top + extraOffsetY))
    else
        self.view.rectTransform.anchoredPosition = Vector2(realPadding.left, -realPadding.top)
    end
    local width = math.min(coloredSlotNum, countPerLine) * (itemList.cellWidth + itemList.space.x) - itemList.space.x
    local height = lineCount * (itemList.cellHeight + itemList.space.y) - itemList.space.y
    self.view.rectTransform.sizeDelta = Vector2(width, height)


    if not isSameLine then
        local outlineSize = -self.view.diffLineNode.offsetMin.x
        local extraWidth = extraCount * (itemList.cellWidth + itemList.space.x) + outlineSize - itemList.space.x / 2
        local realHeight = height + outlineSize * lineCount - itemList.space.y
        self.view.leftBG.sizeDelta = Vector2(extraWidth, realHeight)
        self.view.rightBG.sizeDelta = Vector2(width + outlineSize * 2 - extraWidth, realHeight)
    end

    self.view.sameLineNode.gameObject:SetActive(isSameLine)
    self.view.diffLineNode.gameObject:SetActive(not isSameLine)
    if self.view.dropHilight then
        self.view.dropHilight.gameObject:SetActive(false)
    end
    self.view.gameObject:SetActive(true)
end

ItemBagColoredSlotUniBG.SetDropHilightActive = HL.Method(HL.Boolean) << function(self, active)
    if not self.view.dropHilight then
        return
    end
    self.view.dropHilight.gameObject:SetActive(active)
    if active then

        self.view.dropHilight.transform:SetParent(self.view.gameObject.transform.parent, true)
        self.view.dropHilight.transform:SetAsLastSibling()
    else
        self.view.dropHilight.transform:SetParent(self.view.sameLineNode.transform, true)
    end
end

HL.Commit(ItemBagColoredSlotUniBG)
return ItemBagColoredSlotUniBG
