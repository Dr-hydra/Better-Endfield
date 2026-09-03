local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local LuaNodeCache = require_ex('Common/Utils/LuaNodeCache')
local PANEL_ID = PanelId.ControllerNaviTarget
ControllerNaviTargetCtrl = HL.Class('ControllerNaviTargetCtrl', uiCtrl.UICtrl)






ControllerNaviTargetCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONTROLLER_NAVI_TARGET_CHANGED] = 'OnControllerNaviTargetChanged',
    [MessageConst.ON_PANEL_ORDER_RECALCULATED] = 'OnPanelOrderRecalculated',
}

ControllerNaviTargetCtrl.m_lateTickKey = HL.Field(HL.Number) << -1

ControllerNaviTargetCtrl.m_currHintRect = HL.Field(HL.Any)

ControllerNaviTargetCtrl.m_hintRectCache = HL.Field(LuaNodeCache)

ControllerNaviTargetCtrl.m_lastSetParentPath = HL.Field(HL.String) << ""


ControllerNaviTargetCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.hintRect.gameObject:SetActive(false)
    self.m_hintRectCache = LuaNodeCache(self.view.hintRect, self.view)
end

ControllerNaviTargetCtrl.OnShow = HL.Override() << function(self)
    self:StartTick()
end

ControllerNaviTargetCtrl.OnHide = HL.Override() << function(self)
    self:StopTick()
end

ControllerNaviTargetCtrl.OnClose = HL.Override() << function(self)
    self:StopTick()
end

ControllerNaviTargetCtrl.OnInputDeviceTypeChanged = HL.StaticMethod(HL.Opt(HL.Any)) << function(arg)
    if DeviceInfo.usingController then
        UIManager:AutoOpen(PANEL_ID)
    else
        local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
        if isOpen then
            ctrl:RecoverHintParent()
        end
        UIManager:Hide(PANEL_ID)
    end
end

ControllerNaviTargetCtrl.RecoverHintParent = HL.Method() << function(self)
    local hintRectNode = self:_GetHintRectNode()
    hintRectNode.rectTransform:SetParent(self.view.rectTransform)
end

ControllerNaviTargetCtrl.StartTick = HL.Method() << function(self)
    self:_SyncHintRect()
    self.m_lateTickKey = LuaUpdate:Add("TailTick", function(deltaTime)
        self:_SyncHintRect()
    end)
end

ControllerNaviTargetCtrl.StopTick = HL.Method() << function(self)
    self.m_lateTickKey = LuaUpdate:Remove(self.m_lateTickKey)
end

ControllerNaviTargetCtrl.OnPanelOrderRecalculated = HL.Method() << function(self)
    self:_CalcCanvasOrder()
end

ControllerNaviTargetCtrl.OnControllerNaviTargetChanged = HL.Method() << function(self)
    self:_CalcCanvasOrder()
    self:_SyncHintRectTransformParent()
    self:_SyncHintRect()
    self:_PlayHintRectAnimation()
end

ControllerNaviTargetCtrl._CalcCanvasOrder = HL.Method() << function(self)
    local target = InputManagerInst.controllerNaviManager.curTarget
    if IsNull(target) then
        return
    end
    local canvas = target.transform:GetComponentInParent(typeof(Unity.Canvas), true)
    self:SetSortingOrder(canvas.sortingOrder + 1, false)
end




ControllerNaviTargetCtrl._GetHintRectNode = HL.Method().Return(HL.Any) << function(self)
    if NotNull(self.m_currHintRect) and NotNull(self.m_currHintRect.rectTransform) then
        return self.m_currHintRect
    end

    if not string.isEmpty(self.m_lastSetParentPath) then
        logger.critical("手柄聚集框被意外销毁，上一次SetParent对象路径：", self.m_lastSetParentPath)
    end
    self.m_currHintRect = self.m_hintRectCache:Get()
    return self.m_currHintRect
end

ControllerNaviTargetCtrl._SyncHintRect = HL.Method() << function(self)
    local hintRectNode = self:_GetHintRectNode()
    local hint = hintRectNode.rectTransform
    if not DeviceInfo.usingController then
        hint.gameObject:SetActive(false)
        return
    end

    local target = InputManagerInst.controllerNaviManager.curTarget
    local selfIsActive = hint.gameObject.activeSelf
    if IsNull(target) then
        if selfIsActive then
            hint.gameObject:SetActive(false)
        end
        return
    end

    local active = target.gameObject.activeInHierarchy and
        InputManagerInst.controllerNaviManager:IsNavigationBindingEnabled() and
        not target.hideNaviHint
    if selfIsActive ~= active then
        hint.gameObject:SetActive(active)
    end
    if not active then
        return
    end

    local targetTransform = NotNull(target.overrideNaviHintRectTransform) and target.overrideNaviHintRectTransform or target.transform
    if target.changeNaviHintParent then
        hint.pivot = targetTransform.pivot
        hint.anchorMin = targetTransform.anchorMin
        hint.anchorMax = targetTransform.anchorMax
        hint.position = targetTransform.position
        hint.sizeDelta = targetTransform.sizeDelta
        hint.rotation = targetTransform.rotation
        hintRectNode.animationNodeTrans.localScale = targetTransform.localScale * target.overrideNaviHintRectScale
    else
        local targetScreenRect = UIUtils.getTransformScreenRect(targetTransform, self.uiCamera)
        local canvasSize = self.view.transform.rect.size
        local targetScreenSize = targetScreenRect.size
        hint.position = UIUtils.getRectTransformCenterPosition(targetTransform)
        hint.sizeDelta = Vector2(targetScreenSize.x / Screen.width * canvasSize.x, targetScreenSize.y / Screen.height * canvasSize.y)
    end
end

ControllerNaviTargetCtrl._PlayHintRectAnimation = HL.Method() << function(self)
    local hintRectNode = self:_GetHintRectNode()
    if not hintRectNode.rectTransform.gameObject.activeSelf then
        return
    end
    hintRectNode.animationWrapper:ClearTween()
    hintRectNode.animationWrapper:PlayInAnimation()
end

ControllerNaviTargetCtrl._SyncHintRectTransformParent = HL.Method() << function(self)
    local hintRectNode = self:_GetHintRectNode()
    local hint = hintRectNode.rectTransform
    local target = InputManagerInst.controllerNaviManager.curTarget
    local targetValid = NotNull(target)
    if targetValid and target.changeNaviHintParent then
        local targetTransform = NotNull(target.overrideNaviHintRectTransform) and target.overrideNaviHintRectTransform or target.transform
        hint:SetParent(targetTransform.parent)
        hint:SetAsLastSibling()
        LayoutRebuilder.ForceRebuildLayoutImmediate(targetTransform.parent)
        self.m_lastSetParentPath = targetTransform:PathFromRoot()
    else
        hint:SetParent(self.view.rectTransform)
        hint.rotation = Quaternion.identity
        hint.pivot = Vector2(0.5, 0.5)
        hint.anchorMin = Vector2(0.5, 0.5)
        hint.anchorMax = Vector2(0.5, 0.5)
        if targetValid then
            hintRectNode.animationNodeTrans.localScale = Vector3.one * target.overrideNaviHintRectScale
        else
            hintRectNode.animationNodeTrans.localScale = Vector3.one
        end
    end
    hint.localScale = Vector3.one
    if targetValid then
        hintRectNode.confirmKeyHint.gameObject:SetActive(target.needNaviConfirmKeyHint)
    end
    hintRectNode.confirmKeyHint.transform.localScale = Vector3.one / hint.localScale.x
end






ControllerNaviTargetCtrl._SyncHintArrow = HL.Method() << function(self)
    local hint = self.view.hintArrow
    if not DeviceInfo.usingController then
        hint.gameObject:SetActiveIfNecessary(false)
        return
    end
    local target = InputManagerInst.controllerNaviManager.curTarget
    local selfIsActive = hint.gameObject.activeInHierarchy
    if IsNull(target) then
        if selfIsActive then
            hint.gameObject:SetActive(false)
        end
        return
    end
    local active = target.gameObject.activeInHierarchy and InputManagerInst.controllerNaviManager:IsNavigationBindingEnabled()
    if selfIsActive ~= active then
        hint.gameObject:SetActive(active)
    end
    if not active then
        return
    end
    local targetScreenRect = UIUtils.getTransformScreenRect(target.transform, self.uiCamera)
    local canvasSize = self.view.transform.rect.size
    hint.anchoredPosition = Vector2(targetScreenRect.xMin / Screen.width * canvasSize.x, - targetScreenRect.yMin / Screen.height * canvasSize.y)
end




HL.Commit(ControllerNaviTargetCtrl)
