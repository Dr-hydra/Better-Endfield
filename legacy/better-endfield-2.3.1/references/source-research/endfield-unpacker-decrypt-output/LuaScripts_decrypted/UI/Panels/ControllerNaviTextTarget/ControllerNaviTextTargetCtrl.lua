
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ControllerNaviTextTarget

ControllerNaviTextTargetCtrl = HL.Class('ControllerNaviTextTargetCtrl', uiCtrl.UICtrl)



ControllerNaviTextTargetCtrl.m_targetUIText = HL.Field(CS.Beyond.UI.UIText)

ControllerNaviTextTargetCtrl.m_startPos = HL.Field(Vector2)

ControllerNaviTextTargetCtrl.m_endPos = HL.Field(Vector2)

ControllerNaviTextTargetCtrl.m_startHeight = HL.Field(HL.Number) << 0

ControllerNaviTextTargetCtrl.m_endHeight = HL.Field(HL.Number) << 0








ControllerNaviTextTargetCtrl.s_messages = HL.StaticField(HL.Table) << {
}

ControllerNaviTextTargetCtrl.ShowHint = HL.StaticMethod(HL.Table) << function(arg)
    local ctrl = UIManager:AutoOpen(PANEL_ID)
    ctrl:_InnerShowHint(arg.uiText, arg.startCharIndex, arg.endCharIndex)
end

ControllerNaviTextTargetCtrl.HideHint = HL.StaticMethod() << function()
    UIManager:Hide(PANEL_ID)
end





ControllerNaviTextTargetCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

ControllerNaviTextTargetCtrl.OnHide = HL.Override() << function(self)
    self.m_targetUIText = nil
end



ControllerNaviTextTargetCtrl._RefreshHint = HL.Method() << function(self)

    self.view.hintLeftNode.anchoredPosition = self.m_startPos
    self.view.hintRightNode.anchoredPosition = self.m_endPos
    self.view.hintLeftNode:SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, self.m_startHeight)
    self.view.hintRightNode:SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, self.m_endHeight)
    self:_CalcCanvasOrder()
end




ControllerNaviTextTargetCtrl._InnerShowHint = HL.Method(CS.Beyond.UI.UIText, HL.Number, HL.Number) << function(self, uiText, startCharIndex, endCharIndex)
    if IsNull(uiText) or uiText.activeSelf == false then
        UIManager:Hide(PANEL_ID)
        return
    end

    local hasStartChar, startPos, startHeight = uiText:TryGetCharacterStartPosAndHeight(startCharIndex)
    local hasEndChar, endPos, endHeight = uiText:TryGetCharacterEndPosAndHeight(endCharIndex)
    if not hasStartChar or not hasEndChar then
        if not hasStartChar then
            logger.error("字符不存在，Index: " .. startCharIndex)
        end
        if not hasEndChar then
            logger.error("字符不存在，Index: " .. endCharIndex)
        end
        UIManager:Hide(PANEL_ID)
    end
    startPos = CameraManager.uiCamera:WorldToScreenPoint(startPos)
    endPos = CameraManager.uiCamera:WorldToScreenPoint(endPos)
    self.m_targetUIText = uiText
    self.m_startPos = UIUtils.screenPointToUI(Vector2(startPos.x, startPos.y), self.uiCamera, self.view.transform)
    self.m_endPos = UIUtils.screenPointToUI(Vector2(endPos.x, endPos.y), self.uiCamera, self.view.transform)
    self.m_startHeight = startHeight
    self.m_endHeight = endHeight

    self:_RefreshHint()
end

ControllerNaviTextTargetCtrl._CalcCanvasOrder = HL.Method() << function(self)
    if IsNull(self.m_targetUIText) then
        return
    end
    local canvas = self.m_targetUIText.transform:GetComponentInParent(typeof(Unity.Canvas), true)
    self:SetSortingOrder(canvas.sortingOrder + 1, false)
end


HL.Commit(ControllerNaviTextTargetCtrl)
