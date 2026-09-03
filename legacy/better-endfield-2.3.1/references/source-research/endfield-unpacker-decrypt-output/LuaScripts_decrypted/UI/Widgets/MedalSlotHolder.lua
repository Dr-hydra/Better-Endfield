local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MedalSlotHolder = HL.Class('MedalSlotHolder', UIWidgetBase)

MedalSlotHolder._OnFirstTimeInit = HL.Override() << function(self)

end

MedalSlotHolder.InitMedalSlotHolder = HL.Method(HL.Opt(HL.Any, HL.Any, HL.Boolean, HL.Boolean, HL.Any, HL.Number, HL.Boolean)) <<
    function(self, medalBundle, slotPos, isValid, useSlot, dragOptions, slotIndex, isNaviDrag)
    self:_FirstTimeInit()




    self.view.gameObject.name = slotIndex == nil and "EditMedalSlot" or "EditMedalSlot_" .. slotIndex
    self:_SetMedalHolderPos(slotPos)
    local isValid = isValid == true
    if useSlot then
        self.view.medalSlot:InitMedalSlot(medalBundle, dragOptions, slotIndex, isNaviDrag)
    else
        self.view.medal:InitMedal(medalBundle)
    end
    self.view.medalHolder.gameObject:SetActive(isValid)
end

MedalSlotHolder.SetDragState = HL.Method(HL.Boolean) << function(self, isDrag)
    if self.view.medalSlot ~= nil then
        self.view.medalSlot:SetDragState(isDrag)
    end
end

MedalSlotHolder._SetMedalHolderPos = HL.Method(HL.Opt(HL.Any)) << function(self, slotPos)

    local isOddRow = (slotPos.posV % 2) == 0
    self.view.medalHolder.transform.anchoredPosition = isOddRow and self.view.config.HOLDER_POS_ODD or self.view.config.HOLDER_POS_EVEN
end

HL.Commit(MedalSlotHolder)
return MedalSlotHolder