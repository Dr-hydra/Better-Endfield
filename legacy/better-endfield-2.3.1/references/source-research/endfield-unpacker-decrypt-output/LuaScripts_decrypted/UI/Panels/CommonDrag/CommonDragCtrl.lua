local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonDrag
local CommonDropHintType = CS.Beyond.UI.CommonDropHintType
CommonDragCtrl = HL.Class('CommonDragCtrl', uiCtrl.UICtrl)





CommonDragCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_TOGGLE_ITEM_SLOT_DROP_HIGHLIGHT] = '_OnToggleItemSlotDropHighlight',
    [MessageConst.RESET_DROP_HIGHLIGHT] = "_ResetDropHighlight",
}


CommonDragCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    CS.Beyond.UI.UIDragItem.s_commonDragObjectParent = self.view.transform
end

CommonDragCtrl.m_curTarget = HL.Field(HL.Any)

CommonDragCtrl._OnToggleItemSlotDropHighlight = HL.Method(HL.Table) << function(self, args)
    local active, hintType, position, parent, target = unpack(args)
    if not active and target ~= self.m_curTarget then
        return
    end

    local hintNode
    if hintType == CommonDropHintType.Square then
        hintNode = self.view.imgLightSquare
    elseif hintType == CommonDropHintType.Circle then
        hintNode = self.view.imgLightCircle
    else
        return
    end

    hintNode.gameObject:SetActive(active)
    if active and position ~= nil then

        hintNode:SetParent(parent)
        hintNode:SetAsLastSibling()
        hintNode.position = position
    end
    if active then
        self.m_curTarget = target
    else
        hintNode:SetParent(self.view.rectTransform)
        self.m_curTarget = nil
    end
end


CommonDragCtrl._ResetDropHighlight = HL.Method() << function(self)
    self.view.imgLightSquare.gameObject:SetActive(false)
    self.view.imgLightCircle.gameObject:SetActive(false)
    self.view.imgLightSquare:SetParent(self.view.rectTransform)
    self.view.imgLightCircle:SetParent(self.view.rectTransform)
end

HL.Commit(CommonDragCtrl)
