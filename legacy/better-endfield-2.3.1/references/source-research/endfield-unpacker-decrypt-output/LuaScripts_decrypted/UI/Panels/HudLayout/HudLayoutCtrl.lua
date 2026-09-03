
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.HudLayout

HudLayoutCtrl = HL.Class('HudLayoutCtrl', uiCtrl.UICtrl)

local D_PAD_CONFIG = {
    ["btnUp"] = Vector2.up,
    ["btnDown"] = Vector2.down,
    ["btnLeft"] = Vector2.left,
    ["btnRight"] = Vector2.right,
}


local LayoutType = CS.Beyond.UI.UICustomLayoutElement.LayoutType
local COMBO_SKILL_LAYOUT_ELEMENT_KEY = "layout_battle_combo_skill"
local comboSkillLayoutConfig = {
    {
        getName = function()
            return Language.ui_set_layout_combo_skill_right_to_left
        end,
        layoutType = LayoutType.None,
    },
    {
        getName = function()
            return Language.ui_set_layout_combo_skill_left_to_right
        end,
        layoutType = LayoutType.LeftToRight,
    },
}





HudLayoutCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_HUD_LAYOUT_CHANGED] = "_OnHudLayoutChanged",
}

HudLayoutCtrl.m_layoutHandleList = HL.Field(HL.Userdata)

HudLayoutCtrl.m_selectedLayoutHandle = HL.Field(HL.Userdata)


HudLayoutCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_layoutHandleList = self.view.layout.layoutHandles
    self.view.selectedNode.gameObject:SetActive(false)
    self:_InitAction()
    self:_RefreshOperationNode()
    self.m_oriScreenRatioInt = DeviceInfo:GetCurRatioInt()

    PhaseManager:ExitPhaseFastTo(PhaseId.Level)
end

HudLayoutCtrl._InitAction = HL.Method() << function(self)
    self.view.operationNode.btnBack.onClick:AddListener(function()
        if self.view.layout:IsDirty() then
            Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_CHANGE_NOT_SAVED_CONFIRM,
                onConfirm = function()
                    self:_CloseToMainHud()
                end,
            })

        else
           self:_CloseToMainHud()
        end
    end)

    for i = 0, self.m_layoutHandleList.Count - 1 do
        local handle = self.m_layoutHandleList[i]
        if handle then
            handle.onClick:AddListener(function()
                self:_OnLayoutHandleClicked(handle)
            end)
            handle.onBeginDrag:AddListener(function()
                self:_OnLayoutHandleClicked(handle)
            end)
            if handle.Element.elementKey == COMBO_SKILL_LAYOUT_ELEMENT_KEY then
                handle.Element.onLayoutDataLoaded:AddListener(function()
                    self:_RefreshComboSkillLayoutType(handle.layoutType)
                end)
            end
        end
    end

    self.view.operationNode.sliderAlpha.minValue = self.view.config.ALPHA_RANGE.x
    self.view.operationNode.sliderAlpha.maxValue = self.view.config.ALPHA_RANGE.y
    self.view.operationNode.sliderAlpha.onValueChanged:AddListener(function(value)
        if self.m_selectedLayoutHandle then
            self.m_selectedLayoutHandle.alpha = value
        end
        self.view.operationNode.txtAlphaNumber.text = string.format("%d%%", math.floor(value * 100))
    end)

    self.view.operationNode.sliderScale.minValue = self.view.config.SCALE_RANGE.x
    self.view.operationNode.sliderScale.maxValue = self.view.config.SCALE_RANGE.y
    self.view.operationNode.sliderScale.onValueChanged:AddListener(function(value)
        if self.m_selectedLayoutHandle then
            self.m_selectedLayoutHandle.scale = value
        end
        self.view.operationNode.txtScaleNumber.text = string.format("%d%%", math.floor(value * 100))
    end)

    for btnName, dir in pairs(D_PAD_CONFIG) do
        self.view.operationNode[btnName].onClick:AddListener(function()
            if self.m_selectedLayoutHandle then
                local pos = self.m_selectedLayoutHandle.position
                pos = pos + dir * self.view.config.MOVE_STEP
                self.m_selectedLayoutHandle.position = pos
            end
        end)
        self.view.operationNode[btnName].onLongPress:AddListener(function()
            if self.m_selectedLayoutHandle then
                self:_StartMoveTick(dir * self.view.config.MOVE_SPEED)
            end
        end)
        self.view.operationNode[btnName].onPressEnd:AddListener(function()
            self:_StopMoveTick()
        end)
    end

    self.view.operationNode.btnRest.onClick:AddListener(function()
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_HUD_LAYOUT_RESET_CONFIRM,
            onConfirm = function()
                self.view.layout:ResetToDefault(true)
                self:_RefreshOperationNode()
                self:_SetCanSave(false)
            end,
        })
    end)
    self.view.operationNode.btnSave.onClick:AddListener(function()
        self.view.layout:Save()
        Notify(MessageConst.SHOW_TOAST, Language.LUA_HUD_LAYOUT_SAVED)
        self:_SetCanSave(false)
    end)

    self.view.operationNode.hideBtn.onClick:AddListener(function()
        self.view.operationAnim:ClearTween()
        self.view.operationAnim:PlayOutAnimation(function()
            self.view.operationNode.gameObject:SetActive(false)
        end)
        self.view.expandBtn.transform.localPosition = self.view.operationNode.transform.localPosition
        self.view.expandBtn.gameObject:SetActive(true)
    end)

    self.view.expandBtn.gameObject:SetActive(false)
    self.view.expandBtn.onClick:AddListener(function()
        self.view.expandBtn.gameObject:SetActive(false)
        self.view.operationAnim:ClearTween()
        self.view.operationNode.gameObject:SetActive(true)
        self.view.operationAnim:PlayInAnimation()
        self.view.operationNode.transform.localPosition = self.view.expandBtn.transform.localPosition
        self.view.operationNode.drag:ApplyDragArea()
    end)
    self.view.expandBtn.gameObject:SetActive(false)
    self.view.operationNode.gameObject:SetActive(true)
    self:_SetCanSave(false)

    local isFacMode = Utils.isInFactoryMode()
    self.view.operationNode.commonToggle:InitCommonToggle(function(isOn)
        self:_ActiveFacMode(not isOn)
    end, not isFacMode, true)
    self:_ActiveFacMode(isFacMode)
end

HudLayoutCtrl._CloseToMainHud = HL.Method() << function(self)
    self.view.selectedNode.gameObject:SetActive(false)
    self:PlayAnimationOutAndClose()
    PhaseManager:ExitPhaseFastTo(PhaseId.Level)
    Notify(MessageConst.TRY_SWITCH_FAC_MODE, self.view.switchModeNode.isOn)
end

HudLayoutCtrl._OnLayoutHandleClicked = HL.Method(HL.Userdata) << function(self, layoutHandle)
    if self.m_selectedLayoutHandle == layoutHandle then
        return
    end
    self.m_selectedLayoutHandle = layoutHandle
    if self.m_selectedLayoutHandle then
        self.view.selectedNode.gameObject:SetActive(true)
        self.view.selectedNodeAnim:PlayInAnimation()
        self.view.selectedNode:SetParent(self.m_selectedLayoutHandle.Element.highlightRect, false)
        self.view.selectedNode.anchoredPosition = Vector2.zero
        self.view.selectedNode.sizeDelta = Vector2.zero
    else
        self.view.selectedNode.gameObject:SetActive(false)
    end

    self:_RefreshOperationNode()
end

HudLayoutCtrl._RefreshOperationNode = HL.Method() << function(self)
    local isSelected = self.m_selectedLayoutHandle ~= nil
    local view = self.view.operationNode
    view.controlNode.gameObject:SetActive(isSelected)
    view.emptyNode.gameObject:SetActive(not isSelected)
    if not isSelected then
        return
    end
    local hintText = ''
    if not string.isEmpty(self.m_selectedLayoutHandle.hintTextId) then
        hintText = Language[self.m_selectedLayoutHandle.hintTextId]
    end
    view.currentSelectTxt.text = hintText
    view.sliderAlpha:SetValueWithoutNotify(self.m_selectedLayoutHandle.alpha)
    view.sliderScale:SetValueWithoutNotify(self.m_selectedLayoutHandle.scale)
    view.txtAlphaNumber.text = string.format("%d%%", math.floor(self.m_selectedLayoutHandle.alpha * 100))
    view.txtScaleNumber.text = string.format("%d%%", math.floor(self.m_selectedLayoutHandle.scale * 100))
    if self.m_selectedLayoutHandle.Element.elementKey == COMBO_SKILL_LAYOUT_ELEMENT_KEY then
        view.styleNode.gameObject:SetActive(true)
        view.dropDown:ClearComponent()
        view.dropDown:Init(
            function(csIndex, option, _)
                option:SetText(comboSkillLayoutConfig[LuaIndex(csIndex)].getName())
            end,
            function(csIndex)
                local layoutType = comboSkillLayoutConfig[LuaIndex(csIndex)].layoutType
                self:_RefreshComboSkillLayoutType(layoutType)
                LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.comboSkillStateController.transform)
                self.m_selectedLayoutHandle.layoutType = layoutType
            end
        )
        local selectedIndex = 1
        for i, config in pairs(comboSkillLayoutConfig) do
            if config.layoutType == self.m_selectedLayoutHandle.Element.layoutType then
                selectedIndex = i
                break
            end
        end
        view.dropDown:Refresh(#comboSkillLayoutConfig, CSIndex(selectedIndex), false)
    else
        view.styleNode.gameObject:SetActive(false)
    end
end

HudLayoutCtrl.m_moveTickKey = HL.Field(HL.Number) << -1

HudLayoutCtrl._StartMoveTick = HL.Method(Vector2) << function(self, velocity)
    self:_StopMoveTick()
    self.m_moveTickKey = LuaUpdate:Add("Tick", function(deltaTime)
        if self.m_selectedLayoutHandle then
            local pos = self.m_selectedLayoutHandle.position
            pos = pos + velocity * deltaTime
            self.m_selectedLayoutHandle.position = pos
        end
    end)
end

HudLayoutCtrl._StopMoveTick = HL.Method() << function(self)
    LuaUpdate:Remove(self.m_moveTickKey)
end

HudLayoutCtrl._OnHudLayoutChanged = HL.Method() << function(self)
    self:_SetCanSave(true)
end

HudLayoutCtrl._SetCanSave = HL.Method(HL.Boolean) << function(self, canSave)
    self.view.operationNode.btnSave.gameObject:SetActive(canSave)
    self.view.operationNode.btnSaveDisable.gameObject:SetActive(not canSave)
end

HudLayoutCtrl._ActiveFacMode = HL.Method(HL.Boolean) << function(self, isFactory)
    self.view.factory.gameObject:SetActive(isFactory)
    self.view.battle.gameObject:SetActive(not isFactory)
    if self.m_selectedLayoutHandle and not self.m_selectedLayoutHandle.gameObject.activeInHierarchy then
        self:_OnLayoutHandleClicked(nil)
    end
    self.view.switchModeNode.isOn = isFactory
end

HudLayoutCtrl._RefreshComboSkillLayoutType = HL.Method(HL.Userdata) << function(self, layoutType)
    local stateName = "RightToLeft"
    if layoutType == CS.Beyond.UI.UICustomLayoutElement.LayoutType.LeftToRight then
        stateName = "LeftToRight"
    end
    self.view.comboSkillStateController:SetState(stateName)
end


HudLayoutCtrl.m_oriScreenRatioInt = HL.Field(HL.Number) << -1

HudLayoutCtrl.OnScreenSizeChanged = HL.StaticMethod() << function()
    local isOpen, self = UIManager:IsOpen(PANEL_ID)
    if isOpen then

        self:_OnScreenSizeChanged()
    else

        CS.Beyond.UI.UICustomLayoutElement.RefreshAll()
    end
end

HudLayoutCtrl._OnScreenSizeChanged = HL.Method() << function(self)
    local curRatio = DeviceInfo:GetCurRatioInt()
    if curRatio == self.m_oriScreenRatioInt or not self.view.operationNode.btnSave.gameObject.activeInHierarchy then

        Notify(MessageConst.HIDE_POP_UP)
        self.m_oriScreenRatioInt = DeviceInfo:GetCurRatioInt()
        CS.Beyond.UI.UICustomLayoutElement.RefreshAll()
        return
    end

    local refreshAct = function()
        CS.Beyond.UI.UICustomLayoutElement.RefreshAll()
        self:_RefreshOperationNode()
        self:_SetCanSave(false)
        self.m_oriScreenRatioInt = DeviceInfo:GetCurRatioInt()
    end

    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_CUSTOM_HUD_LAYOUT_SCREEN_SIZE_CHANGE_HINT,
        warningContent = Language.LUA_CUSTOM_HUD_LAYOUT_SCREEN_SIZE_CHANGE_WARNING_HINT,
        confirmText = Language.LUA_CUSTOM_HUD_LAYOUT_SCREEN_SIZE_CHANGE_CONFIRM,
        onConfirm = function()
            self.view.layout:Save(self.m_oriScreenRatioInt)
            Notify(MessageConst.SHOW_TOAST, Language.LUA_HUD_LAYOUT_SAVED)
            refreshAct()
        end,
        onCancel = function()
            refreshAct()
        end,
    })
end


HL.Commit(HudLayoutCtrl)
