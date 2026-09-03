
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacMainRight
FacMainRightCtrl = HL.Class('FacMainRightCtrl', uiCtrl.UICtrl)





FacMainRightCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ENTER_BUILDING_MODE] = 'OnEnterBuildingMode',
    [MessageConst.ON_ENTER_LOGISTIC_MODE] = 'OnEnterLogisticMode',
    [MessageConst.ON_EXIT_FACTORY_MODE] = 'OnExitFactoryMode',
    [MessageConst.ON_SYSTEM_UNLOCK_CHANGED] = 'OnSystemUnlock',
    [MessageConst.ON_TOGGLE_FAC_TOP_VIEW] = 'OnToggleFacTopView',
    [MessageConst.ON_RESET_BLACKBOX] = 'OnResetBlackbox',
    [MessageConst.FAC_MAIN_HUD_CLOSE_MOBILE_BOX] = '_CloseBtnBox',
    [MessageConst.FAC_MAIN_HUD_RIGHT_STOP_FOCUS] = 'StopFocus',
}


FacMainRightCtrl.m_isBuilding = HL.Field(HL.Boolean) << false

FacMainRightCtrl.m_lastBuildItemId = HL.Field(HL.String) << ''

FacMainRightCtrl.m_zoomCamGroupId = HL.Field(HL.Number) << 0



FacMainRightCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.blueprintBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.FacBlueprint)
    end)

    self:_UpdateBlueprintBtn()

    self.view.buildBtn.onClick:AddListener(function()
        Notify(MessageConst.OPEN_FAC_BUILD_MODE_SELECT, {showLastType = true})
    end)

    self.view.destroyBtn.onClick:AddListener(function()
        Notify(MessageConst.FAC_ENTER_DESTROY_MODE)
    end)

    self.view.batchSelectBtn.onClick:AddListener(function()
        Notify(MessageConst.FAC_ENTER_DESTROY_MODE)
    end)

    self.view.equipBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.EquipTech)
    end)
    if self.view.equipBtnDragHandler then
        self.view.equipBtnDragHandler.onDrag:AddListener(function(eventData)
            Notify(MessageConst.MOVE_LEVEL_CAMERA, eventData.delta)
        end)
    end

    if self.view.toolBoxToggleDragHandler then
        self.view.toolBoxToggleDragHandler.onDrag:AddListener(function(eventData)
            Notify(MessageConst.MOVE_LEVEL_CAMERA, eventData.delta)
        end)
    end

    if self.view.machineIconBtn then
        self.view.machineIconBtn.button.onClick:AddListener(function()
            local showOutput = not GameInstance.remoteFactoryManager:GetBuildingOutputVisible()
            GameInstance.remoteFactoryManager:SwitchBuildingOutputVisible(showOutput)
            self:UpdateMachineIcon()
            local toastTextId = showOutput and "LUA_BUILDING_TARGET_ICON_SHOW_TOAST" or "LUA_BUILDING_TARGET_ICON_HIDE_TOAST"
            Notify(MessageConst.SHOW_TOAST, Language[toastTextId])
        end)
    end

    if self.view.lastBuildNode then
        self.view.lastBuildNode.button.onClick:AddListener(function()
            self:_OnCLickLastBuild()
        end)
        self:_UpdateLastBuildNode()
    end

    if self.isControllerPanel then
        self.m_zoomCamGroupId = InputManagerInst:CreateGroup(self.view.content.groupId)
        UIUtils.bindControllerCamZoom(self.m_zoomCamGroupId)
        InputManagerInst:ToggleGroup(self.m_zoomCamGroupId, false)

        self.view.animationSelectableNaviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
            self:_OnIsTopLayerChanged(isTopLayer)
        end)
        self.view.animationSelectableNaviGroup.focusPanelSortingOrder = UIManager:GetBaseOrder(Types.EPanelOrderTypes.PopUp) - 1
    end

    self:_CloseBtnBox()
    self:_InitBtnRedDot()
end

FacMainRightCtrl.OnShow = HL.Override() << function(self)
    self:_UpdateLastBuildNode()
    self:UpdateMachineIcon()
    self:_UpdateTopViewState()
    self.view.equipBtn.gameObject:SetActive(PhaseManager:CheckCanOpenPhase(PhaseId.EquipTech))
end

FacMainRightCtrl.OnHide = HL.Override() << function(self)
    self:StopFocus()
    LuaSystemManager.inputDeviceChangeSystem:SetForbidInputDeviceChange("FacMainRight.Focus", false)
end

FacMainRightCtrl.OnClose = HL.Override() << function(self)
    LuaSystemManager.inputDeviceChangeSystem:SetForbidInputDeviceChange("FacMainRight.Focus", false)
end

FacMainRightCtrl.StopFocus = HL.Method() << function(self)
    if self.isControllerPanel then
        self.view.animationSelectableNaviGroup:ManuallyStopFocus()
    end
end

FacMainRightCtrl.UpdateMachineIcon = HL.Method() << function(self)
    if not self.view.machineIconBtn then
        return
    end
    local isOn = GameInstance.remoteFactoryManager:GetBuildingOutputVisible()
    self.view.machineIconBtn.iconOn.gameObject:SetActive(not isOn)
    self.view.machineIconBtn.iconOff.gameObject:SetActive(isOn)
    self.view.machineIconBtn.text.text = isOn and Language.LUA_FAC_MAIN_RIGHT_MACHINE_ICON_OFF or Language.LUA_FAC_MAIN_RIGHT_MACHINE_ICON_ON
end


FacMainRightCtrl.OnToggleFacTopView = HL.Method(HL.Boolean) << function(self, isTopView)
    if self.view.toolBoxToggle then
        if isTopView then
            self.m_btnBoxOpened = self.view.toolBoxToggle.isOn
            self.view.toolBoxToggle.isOn = self.m_btnBoxOpenedInTopView
        else
            self.m_btnBoxOpenedInTopView = self.view.toolBoxToggle.isOn
            self.view.toolBoxToggle.isOn = self.m_btnBoxOpened
        end
    end
    self:_UpdateTopViewState()
    if DeviceInfo.usingController then
        self.view.animationSelectableNaviGroup:ClearLastFocusNaviTarget()
    end
end

FacMainRightCtrl.OnSystemUnlock = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    self:_UpdateBlueprintBtn()
end

FacMainRightCtrl.OnExitFactoryMode = HL.Method(HL.Opt(HL.Any)) << function(self)
    self.m_lastBuildItemId = ""
    self:_UpdateLastBuildNode()
    self:_CloseBtnBox()
    self.m_btnBoxOpened = false
    self.m_btnBoxOpenedInTopView = false
end

FacMainRightCtrl.OnEnterBuildingMode = HL.Method(HL.String) << function(self, id)
    local count = Utils.getItemCount(id)
    if count == 0 then
        return
    end
    self.m_lastBuildItemId = id
    self.m_isBuilding = true
    self:_UpdateLastBuildNode()
end

FacMainRightCtrl.OnEnterLogisticMode = HL.Method(HL.String) << function(self, id)
    self.m_lastBuildItemId = id
    self.m_isBuilding = false
    self:_UpdateLastBuildNode()
end

FacMainRightCtrl._InitBtnRedDot = HL.Method() << function(self)
    if Utils.isInBlackbox() then

        self.view.buildBtnRedDot.gameObject:SetActive(false)
        return
    end

    self.view.buildBtnRedDot:InitRedDot("FacBuildMode")
    self.view.equipBtnRedDot:InitRedDot("EquipTech")
    if self.view.toolBoxRedDot then
        self.view.toolBoxRedDot:InitRedDot("ToolBox")
    end
end

FacMainRightCtrl._UpdateLastBuildNode = HL.Method() << function(self)
    local node = self.view.lastBuildNode
    if not node then
        return
    end

    if string.isEmpty(self.m_lastBuildItemId) or (self.m_isBuilding and Utils.getItemCount(self.m_lastBuildItemId) == 0) then
        node.gameObject:SetActive(false)
        return
    end
    node.gameObject:SetActive(true)
    local data = Tables.itemTable[self.m_lastBuildItemId]
    node.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, data.iconId)
    node.itemIconShadow:LoadSprite(UIConst.UI_SPRITE_ITEM, data.iconId)
end

FacMainRightCtrl._OnCLickLastBuild = HL.Method() << function(self)
    local itemId = self.m_lastBuildItemId
    if self.m_isBuilding then
        local count, backpackCount = Utils.getItemCount(itemId)
        if count == 0 then
            return
        end
    end

    if self.m_isBuilding then
        Notify(MessageConst.FAC_ENTER_BUILDING_MODE, {
            itemId = itemId,
        })
    else
        Notify(MessageConst.FAC_ENTER_LOGISTIC_MODE, {
            itemId = itemId,
        })
    end
end

FacMainRightCtrl._OnIsTopLayerChanged = HL.Method(HL.Boolean) << function(self, isTopLayer)
    if isTopLayer then


        local keyCode = InputManagerInst:GetActionGamepadKeyCode("fac_main_right_active", false, true);
        if not InputManagerInst:GetKey(keyCode) then
            self:_StartTimer(0, function()
                self.view.animationSelectableNaviGroup:ManuallyStopFocus()
            end)
            return
        end
    end

    self.view.animation.isActive = isTopLayer
    LuaSystemManager.inputDeviceChangeSystem:SetForbidInputDeviceChange("FacMainRight.Focus", isTopLayer)
    if isTopLayer then
        UIManager:HideWithKey(PanelId.GeneralAbility, "FacMainRightPanel")
    else
        UIManager:ShowWithKey(PanelId.GeneralAbility, "FacMainRightPanel")
    end




    local isOpen, mainCtrl = UIManager:IsOpen(PanelId.FacMain)
    if isOpen then
        mainCtrl:OnFacMainRightActiveChange(isTopLayer)
        local pinGroupId = mainCtrl.view.pinFormulaNode.inputBindingGroupMonoTarget.groupId
        if isTopLayer then


            InputManagerInst:ChangeParent(true, pinGroupId, self.view.animationInputBindingGroupMonoTarget.groupId)
        else
            InputManagerInst:ChangeParent(true, pinGroupId, mainCtrl.view.inputGroup.groupId)
        end
    end

    local _, jsPanelCtrl = UIManager:IsOpen(PanelId.Joystick)
    if jsPanelCtrl then

        local jsGroupId = jsPanelCtrl.view.joystick.groupId
        if isTopLayer then
            InputManagerInst:ChangeParent(true, jsGroupId, self.view.animationInputBindingGroupMonoTarget.groupId)
        else
            InputManagerInst:ChangeParent(true, jsGroupId, jsPanelCtrl.view.inputGroup.groupId)
        end
    end

    Notify(MessageConst.TOGGLE_HIDE_INTERACT_OPTION_LIST, { "FacMainRight", isTopLayer })
    Notify(MessageConst.TOGGLE_HIDE_FAC_TOP_VIEW_RIGHT_SIDE_UI, isTopLayer)
end

FacMainRightCtrl._CloseBtnBox = HL.Method() << function(self)
    if self.view.toolBoxToggle then
        self.view.toolBoxToggle.isOn = false
    end
end

FacMainRightCtrl._UpdateTopViewState = HL.Method() << function(self)
    local inTopView = LuaSystemManager.factory.inTopView
    self.view.stateController:SetState(inTopView and "TopView" or "Normal")
    self.view.destroyBtn.gameObject:SetActive(not inTopView)
    self.view.batchSelectBtn.gameObject:SetActive(inTopView and not DeviceInfo.usingController)
    if self.view.machineIconBtn then
        self.view.machineIconBtn.gameObject:SetActive(not inTopView)
    end
end

FacMainRightCtrl.m_btnBoxOpened = HL.Field(HL.Boolean) << false

FacMainRightCtrl.m_btnBoxOpenedInTopView = HL.Field(HL.Boolean) << false

FacMainRightCtrl.OnResetBlackbox = HL.Method() << function(self)
    self:_CloseBtnBox()
end

FacMainRightCtrl._UpdateBlueprintBtn = HL.Method() << function(self)
    local isSystemUnlocked = Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacBlueprint)
    if Utils.isInBlackbox() then
        self.view.blueprintBtn.gameObject:SetActive(isSystemUnlocked and GameInstance.player.remoteFactory.blueprint.presetBlueprints.Count > 0)
    else
        self.view.blueprintBtn.gameObject:SetActive(isSystemUnlocked)
    end
end

HL.Commit(FacMainRightCtrl)
