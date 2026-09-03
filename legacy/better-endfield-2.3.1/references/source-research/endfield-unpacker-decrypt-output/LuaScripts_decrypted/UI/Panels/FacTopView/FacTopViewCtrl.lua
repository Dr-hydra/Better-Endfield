
local QuickBarItemType = FacConst.QuickBarItemType

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacTopView
FacTopViewCtrl = HL.Class('FacTopViewCtrl', uiCtrl.UICtrl)





FacTopViewCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BUILD_MODE_CHANGE] = 'OnBuildModeChange',
    [MessageConst.BEFORE_ENTER_BUILD_MODE] = 'BeforeEnterBuildMode',
    [MessageConst.ON_FAC_DESTROY_MODE_CHANGE] = 'OnFacDestroyModeChange',
    [MessageConst.BEFORE_ENTER_DESTROY_MODE] = 'BeforeEnterDestroyMode',

    [MessageConst.ON_TOGGLE_QUICK_BAR_CONTROLLER] = 'OnToggleQuickBarController',
    [MessageConst.ON_ITEM_COUNT_CHANGED] = 'OnItemCountChanged',
    [MessageConst.TOGGLE_HIDE_FAC_TOP_VIEW_RIGHT_SIDE_UI] = 'ToggleHideFacTopViewRightSideUi',

    [MessageConst.ON_QUICK_BAR_CHANGED] = 'OnQuickBarChanged',

    [MessageConst.ON_FAC_SIMPLE_FIGURE_MODE_CHANGE] = 'OnFacSimpleFigureModeChange',

    [MessageConst.FAC_TOPVIEW_SHOW_MULTI_TARGET_MENU] = '_ShowMultiTargetMenu',
    [MessageConst.FAC_TOPVIEW_HIDE_MULTI_TARGET_MENU] = '_HideMultiTargetMenu',
}


FacTopViewCtrl.m_onDrag = HL.Field(HL.Function)

FacTopViewCtrl.m_typeCells = HL.Field(HL.Forward('UIListCache'))

FacTopViewCtrl.m_getItemCell = HL.Field(HL.Function)

FacTopViewCtrl.m_isCollapsed = HL.Field(HL.Boolean) << false

FacTopViewCtrl.m_escExitBindingId = HL.Field(HL.Number) << -1

FacTopViewCtrl.m_keyHintCells = HL.Field(HL.Forward('UIListCache'))

FacTopViewCtrl.m_multiTargetCells = HL.Field(HL.Forward('UIListCache'))

FacTopViewCtrl.m_menuHoverEffect = HL.Field(HL.Table)

FacTopViewCtrl.m_multiTargetMenuGridPos = HL.Field(HL.Any)



FacTopViewCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_onDrag = function(eventData)
        self:_OnDrag(eventData)
    end

    self.m_typeCells = self.m_typeCells or UIUtils.genCellCache(self.view.typeCell)

    self.view.rotBtn.onClick:AddListener(function()
        LuaSystemManager.factory:RotateTopViewCam()
    end)
    self.view.collapseBtn.onClick:AddListener(function()
        self:_ToggleContent(false)
    end)
    self.view.expandBtn.onClick:AddListener(function()
        self:_ToggleContent(true)
    end)

    self.view.beltNode.item:InitItem({ id = FacConst.BELT_ITEM_ID }, function()
        self:_OnClickBelt()
    end)
    self.view.beltNode.item:OpenLongPressTips()
    self.view.pipeNode.item:InitItem({ id = FacConst.PIPE_ITEM_ID }, function()
        self:_OnClickPipe()
    end)
    self.view.pipeNode.item:OpenLongPressTips()

    if self.view.hidePipeToggle then
        self.view.hidePipeToggle.toggle.onValueChanged:AddListener(function(isOn)
            local fac = LuaSystemManager.factory
            if isOn then
                fac:SetSimpleFigureMode(FacConst.SIMPLE_FIGURE_MODE.SimplePipeFigure)
            elseif fac.simpleFigureMode == FacConst.SIMPLE_FIGURE_MODE.SimplePipeFigure then
                fac:SetSimpleFigureMode(FacConst.SIMPLE_FIGURE_MODE.None)
            end
        end)
    end

    self.m_getItemCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getItemCell(obj), LuaIndex(csIndex))
    end)

    self.view.scrollListScrollRect.onOverScrollEffect:AddListener(function(isNext)
        self:_OnOverScrollList(isNext)
    end)

    self:BindInputPlayerAction("fac_top_view_rot_cam", function()
        LuaSystemManager.factory:RotateTopViewCam()
    end, self.view.main.groupId)
    if DeviceInfo.usingKeyboard then
        self:BindInputPlayerAction("fac_open_devices_list", function()
            Notify(MessageConst.OPEN_FAC_BUILD_MODE_SELECT, {showLastType = true})
        end, self.view.main.groupId)
        self:BindInputPlayerAction("fac_open_blueprint", function()
            PhaseManager:OpenPhase(PhaseId.FacBlueprint)
        end, self.view.main.groupId)
        self.m_escExitBindingId = self:BindInputPlayerAction("fac_exit_top_view_mode_pc_esc", function()

            if GameSettingUtils.GetSettingValueBool(GameSettingConst.SETTING_ID_FAC_TOP_VIEW_ESC_EXIT) and InputManagerInst:IsBindingEnabled(self.view.topViewToggle.toggleBindingId) then
                Notify(MessageConst.FAC_TOGGLE_TOP_VIEW, false)
            end
        end, self.view.main.groupId)
    elseif DeviceInfo.usingController then
        self:BindInputPlayerAction("fac_top_view_enter_batch_mode_ct", function()
            Notify(MessageConst.FAC_ENTER_DESTROY_MODE)
        end, self.view.main.groupId)
        self:BindInputPlayerAction("fac_top_view_ct_scale_cam", function()
            self:_OnControllerZoomCamera()
        end)
        self.view.startBuildBtn.onClick:AddListener(function()
            if self.m_isCollapsed then
                self:_ToggleContent(true)
            end
        end)
        self:BindInputPlayerAction("common_cancel", function()
            if not self.m_isCollapsed then
                self:_ToggleContent(false)
            end
        end, self.view.buildNode.groupId)

        self:BindInputPlayerAction("fac_top_view_open_building_menu", function()
            self:_ControllerOpenCurBuildingMenu()
        end, self.view.main.groupId)
        self:BindInputPlayerAction("fac_top_view_open_building_panel", function()
            self:_ControllerOpenCurBuildingPanel()
        end, self.view.main.groupId)
        self:BindInputPlayerAction("fac_top_view_move_building", function()
            self:_ControllerMoveCurBuilding()
        end, self.view.main.groupId)
    end




    self.view.topViewToggle.isOn = true
    self.view.topViewToggle.checkIsValueValid = function(isOn)
        if not isOn then
            Notify(MessageConst.FAC_TOGGLE_TOP_VIEW, false)
            return false
        end
        return true
    end

    self:_InitFilters()
    self:_InitKeyHints()
    self.view.facQuickBarClearDropZone:InitFacQuickBarClearDropZone()

    self.m_multiTargetCells = self.m_multiTargetCells or UIUtils.genCellCache(self.view.multiTargetMenuNode.btnCell)
    self.view.multiTargetMenuNode.content.onTriggerAutoClose:AddListener(function()
        self:_HideMultiTargetMenu()
    end)
    self.view.multiTargetMenuNode.gameObject:SetActive(false)

    if not self.m_menuHoverEffect then
        local prefab = self.loader:LoadGameObject(FacConst.BUILDING_INTERACT_HOVER_INDICATOR_PATH)
        self.m_menuHoverEffect = Utils.wrapLuaNode(self:_CreateWorldGameObject(prefab))
        self.m_menuHoverEffect.gameObject.name = "MenuHoverEffect"
        self.m_menuHoverEffect.gameObject:SetActive(false)
    end
end

FacTopViewCtrl.OnShow = HL.Override() << function(self)
    self:_AddRegister()

    self.view.mouseHoverHint.gameObject:SetActive(false)
    self.view.controllerMouseHoverHint.gameObject:SetActive(false)

    if DeviceInfo.usingController then
        self.m_isCollapsed = false
        self:_RefreshTypes()
        self:_ToggleContent(false, true)
        InputManagerInst:SetCustomControllerMouse(self.view.controllerMouse.transform, self.uiCamera)
        self.view.controllerMouse.gameObject:SetActive(true)
    else
        self:_RefreshTypes()
        self:_ToggleContent(true, true)
        self.view.controllerMouse.gameObject:SetActive(false)
    end
    self.view.facQuickBarClearDropZone.gameObject:SetActive(false)
    self:_SyncHidePipeToggleFromFacSystem()
end

FacTopViewCtrl.OnFacSimpleFigureModeChange = HL.Method(HL.Opt(HL.Number)) << function(self, mode)
    self:_SyncHidePipeToggleFromFacSystem()
end

FacTopViewCtrl._SyncHidePipeToggleFromFacSystem = HL.Method() << function(self)
    if not self.view.hidePipeToggle then
        return
    end
    local show = FactoryUtils.canShowPipe()
    self.view.hidePipeToggle.gameObject:SetActive(show)
    if not show then
        return
    end
    local on = LuaSystemManager.factory.simpleFigureMode == FacConst.SIMPLE_FIGURE_MODE.SimplePipeFigure
    self.view.hidePipeToggle.toggle:SetIsOnWithoutNotify(on)
end

FacTopViewCtrl.OnHide = HL.Override() << function(self)
    self:_ClearRegister()
    self:_HideMultiTargetMenu()
    InputManagerInst:SetCustomControllerMouse(nil, nil)
    if not self.m_isCollapsed then
        self:_ToggleContent(false, true)
    end
end

FacTopViewCtrl.OnClose = HL.Override() << function(self)
    self:_ClearRegister()
    if LuaSystemManager.factory.inTopView then
        LuaSystemManager.factory:ToggleTopView(false, true)
    end
    self.m_clearScreenKeyForControllerExpandBuildNode = UIManager:RecoverScreen(self.m_clearScreenKeyForControllerExpandBuildNode)
end

FacTopViewCtrl.ExtractHotSwitchRuntimeState = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if not self.m_typeCells and
        not self.m_multiTargetCells and
        not self.m_keyHintCells and
        not self.m_filterCells and
        not self.m_controllerMouseHoverHintCells and
        not self.m_menuHoverEffect then
        return nil
    end

    local state = {
        typeCells = self.m_typeCells,
        multiTargetCells = self.m_multiTargetCells,
        keyHintCells = self.m_keyHintCells,
        filterCells = self.m_filterCells,
        controllerMouseHoverHintCells = self.m_controllerMouseHoverHintCells,
        menuHoverEffect = self.m_menuHoverEffect,
    }

    self.m_typeCells = nil
    self.m_multiTargetCells = nil
    self.m_keyHintCells = nil
    self.m_filterCells = nil
    self.m_controllerMouseHoverHintCells = nil
    self.m_menuHoverEffect = nil

    return state
end

FacTopViewCtrl.RestoreHotSwitchRuntimeState = HL.Override(HL.Opt(HL.Any)) << function(self, state)
    if not state then
        return
    end

    self.m_typeCells = state.typeCells
    self.m_multiTargetCells = state.multiTargetCells
    self.m_keyHintCells = state.keyHintCells
    self.m_filterCells = state.filterCells
    self.m_controllerMouseHoverHintCells = state.controllerMouseHoverHintCells
    self.m_menuHoverEffect = state.menuHoverEffect
end

FacTopViewCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if not DeviceInfo.usingTouch then

        self.view.keyHintNode.gameObject:SetActive(active)
    end
end

FacTopViewCtrl.OnQuickBarChanged = HL.Method() << function(self)
    if self:IsHide() then
        return
    end
    self.m_typeInfos[1] = self:_GenCustomTypeInfo()
    self:_RefreshItemList()
end


FacTopViewCtrl.m_controllerNaviInfo = HL.Field(HL.Table)

FacTopViewCtrl._RecordControllerNaviInfo = HL.Method() << function(self)
    self.m_controllerNaviInfo = nil
    if not DeviceInfo.usingController or self.m_isCollapsed then
        return
    end
    local tInfo = self.m_typeInfos[self.m_selectedTypeIndex]
    self.m_controllerNaviInfo = {
        typeIndex = self.m_selectedTypeIndex,
    }

    local target = self.view.container.LayerSelectedTarget
    if target then
        for k, _ in pairs(tInfo.showingItems) do
            local cell = self.m_getItemCell(k)
            if cell and cell.button == target then
                self.m_controllerNaviInfo.itemIndex = k
                break
            end
        end
    end
end

FacTopViewCtrl._TryRecoverNaviInfo = HL.Method(HL.Boolean) << function(self, isRecover)
    if not DeviceInfo.usingController then
        return
    end
    if not isRecover then
        self:_ToggleContent(false, true)
        return
    end
    if not self.m_controllerNaviInfo then
        return
    end
    self.m_selectedTypeIndex = self.m_controllerNaviInfo.typeIndex
    self:_RefreshTypes()

    self.view.scrollList:ScrollToIndex(CSIndex(self.m_controllerNaviInfo.itemIndex), true)
    self:SetNaviTarget(self.m_getItemCell(self.m_controllerNaviInfo.itemIndex).button)
    self.m_controllerNaviInfo = nil
end




FacTopViewCtrl.OnToggleFacTopView = HL.StaticMethod(HL.Boolean) << function(active)
    if active then
        local self = UIManager:AutoOpen(PANEL_ID)
        self:_OnToggleFacTopView(true)
    else
        local _, self = UIManager:IsOpen(PANEL_ID)
        if self then
            self:_OnToggleFacTopView(false)
        end
    end
end

FacTopViewCtrl._OnToggleFacTopView = HL.Method(HL.Boolean) << function(self, active)
    if active then
        self:_ClearScreen()
        UIManager:AutoOpen(PanelId.FacTopViewBuildingInfo)
        self.view.controllerMouse.anchoredPosition = Vector2.zero
        Notify(MessageConst.FAC_TOGGLE_TOP_VIEW_BUILDING_INFO, true)
    else
        if self.m_hideKey ~= -1 then
            self:Hide()
            self:_RecoverScreen()
            Notify(MessageConst.FAC_TOGGLE_TOP_VIEW_BUILDING_INFO, false)
        end
    end

    self.view.topViewToggle:SetIsOnWithoutNotify(true)
    self:_ResetFilters()
end

FacTopViewCtrl.OnFacDestroyModeChange = HL.Method(HL.Boolean) << function(self, inDestroyMode)
    self.view.main.gameObject:SetActive(not inDestroyMode)
    self:PlayAnimationIn()
    self:_TryRecoverNaviInfo(not inDestroyMode)
end

FacTopViewCtrl.OnBuildModeChange = HL.Method(HL.Number) << function(self, mode)
    local inBuild = mode ~= FacConst.FAC_BUILD_MODE.Normal
    self.view.main.gameObject:SetActive(not inBuild)
    if (mode == FacConst.FAC_BUILD_MODE.Building or mode == FacConst.FAC_BUILD_MODE.Blueprint) and not FactoryUtils.isMovingBuilding() then
        self.view.controllerMouse.anchoredPosition = Vector2.zero
    end
    if self:IsHide() then
        return
    end
    self:PlayAnimationIn()
    self:_TryRecoverNaviInfo(not inBuild)
end

FacTopViewCtrl.RecoverControllerMouseOnChangeDevice = HL.Method(Vector3) << function(self, mouseWorldPos)
    if not DeviceInfo.usingController then
        return
    end
    self:_SetControllerMouseWorldPos(mouseWorldPos)
end

FacTopViewCtrl._SetControllerMouseWorldPos = HL.Method(Vector3) << function(self, mouseWorldPos)
    local curScreenWorldRect = CSFactoryUtil.GetCurScreenWorldRect(FacConst.FAC_TOP_VIEW_CONTROLLER_MOUSE_PADDING)
    mouseWorldPos.x = lume.clamp(mouseWorldPos.x, curScreenWorldRect.xMin, curScreenWorldRect.xMax)
    mouseWorldPos.z = lume.clamp(mouseWorldPos.z, curScreenWorldRect.yMin, curScreenWorldRect.yMax)
    LuaSystemManager.factory.topViewControllerMouseMoveTarget.position = mouseWorldPos
    LuaSystemManager.factory.topViewControllerMouseMoveTargetChanged = true

    local screenPos = CameraManager.mainCamera:WorldToScreenPoint(mouseWorldPos):XY()
    local screenSize = Vector2(Screen.width, Screen.height)
    local newPos = (screenPos - screenSize / 2) / Screen.width * self.view.rectTransform.rect.width
    self.view.controllerMouse.anchoredPosition = newPos
end

FacTopViewCtrl.BeforeEnterBuildMode = HL.Method(HL.Boolean) << function(self, skipMainHudAnim)
    self:_HideMultiTargetMenu()
    self:_RecordControllerNaviInfo()
    self:PlayAnimationOutWithCallback()



    self.view.controllerMouseHoverHint.gameObject:SetActive(false)
end

FacTopViewCtrl.BeforeEnterDestroyMode = HL.Method() << function(self)
    self:_HideMultiTargetMenu()
    self:_RecordControllerNaviInfo()
    self:PlayAnimationOutWithCallback()
end

FacTopViewCtrl.OnToggleQuickBarController = HL.Method(HL.Boolean) << function(self, active)
    self:ChangePanelCfg("virtualMouseMode", active and Types.EPanelMouseMode.ForceHide or Types.EPanelMouseMode.NeedShow)
end

FacTopViewCtrl.OnItemCountChanged = HL.Method(HL.Table) << function(self, args)
    if self:IsHide() or not self.m_typeInfos then
        return
    end
    local itemId2DiffCount = unpack(args)
    local showingItems = self.m_typeInfos[self.m_selectedTypeIndex].showingItems
    self.view.scrollList:UpdateShowingCells(function(csIndex, obj)
        local info = showingItems[LuaIndex(csIndex)]
        if itemId2DiffCount:ContainsKey(info.itemId) then
            local cell = self.m_getItemCell(obj)
            local count = Utils.getItemCount(info.itemId)
            cell.item:UpdateCount(count)
        end
    end)
end






FacTopViewCtrl.m_hideKey = HL.Field(HL.Number) << -1

FacTopViewCtrl._ClearScreen = HL.Method() << function(self)
    if self.m_hideKey ~= -1 then
        return
    end
    local exceptedPanels = {
        PANEL_ID,
        PanelId.MainHud,
        PanelId.FacMain,
        PanelId.LevelCamera,
        PanelId.FacMiniPowerHud,
        PanelId.FacHudBottomMask,
        PanelId.FacPowerPoleTravelHint,
        PanelId.FacPowerPoleAutoConnectHint,
        PanelId.FacBuildMode,
        PanelId.FacDestroyMode,
        PanelId.FacBuildingInteract,
        PanelId.CommonItemToast,
        PanelId.CommonNewToast,
        PanelId.CommonHudToast,
        PanelId.GeneralTracker,
        PanelId.Radio,
        PanelId.MiniMap,
        PanelId.MissionHud,
        PanelId.MissionHudMini,
        PanelId.SNSHud,
        PanelId.CommonTaskTrackHud,
        PanelId.BlackBoxDiffBtn,
        PanelId.FacTopViewBuildingInfo,
        PanelId.FacMainRight,
    }
    if not DeviceInfo.usingTouch then
        table.insert(exceptedPanels, PanelId.Joystick)
    end
    self.m_hideKey = UIManager:ClearScreen(exceptedPanels)
end

FacTopViewCtrl._RecoverScreen = HL.Method() << function(self)
    self.m_hideKey = UIManager:RecoverScreen(self.m_hideKey)
end

FacTopViewCtrl._AddRegister = HL.Method() << function(self)
    local touchPanel = UIManager.commonTouchPanel
    touchPanel.onDrag:AddListener(self.m_onDrag)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
    self.m_updateKey = LuaUpdate:Add("TailTick", function()
        self:_TailUpdate()
    end)
end

FacTopViewCtrl._ClearRegister = HL.Method() << function(self)
    local touchPanel = UIManager.commonTouchPanel
    touchPanel.onDrag:RemoveListener(self.m_onDrag)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
end

FacTopViewCtrl._OnDrag = HL.Method(HL.Userdata) << function(self, eventData)
    if InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse1) then

        return
    end
    if LuaSystemManager.factory.inDragSelectBatchMode and not InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse2) then

        return
    end
    if DeviceInfo.usingKeyboard then
        local isOpen, ctrl = UIManager:IsOpen(PanelId.FacBuildMode)
        if isOpen then
            if ctrl.m_buildingNodeId and not InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse2) then

                return
            end
        end
    end

    self:_Move(eventData.delta * -self.view.config.MOVE_SPD_ON_DRAG)
end

FacTopViewCtrl._Move = HL.Method(Vector2) << function(self, dir)
    LuaSystemManager.factory:MoveTopViewCamTarget(dir)
end

FacTopViewCtrl._MoveMouse = HL.Method(Vector2) << function(self, dir)

    local cam = CameraManager.mainCamera
    local curMousePos = InputManager.mousePosition
    local camRay = cam:ScreenPointToRay(curMousePos)
    local _, worldPos = CSFactoryUtil.SampleLevelRegionPointWithRay(camRay)
    local camTrans = cam.transform
    local realDir = dir.x * camTrans.right + dir.y * camTrans.up
    realDir.y = 0
    worldPos = worldPos + realDir.normalized * dir.magnitude
    local targetScreenPos = cam:WorldToScreenPoint(worldPos)
    InputManager.SetMousePos(targetScreenPos:XY())
end

FacTopViewCtrl.m_updateKey = HL.Field(HL.Number) << -1

FacTopViewCtrl._TailUpdate = HL.Method() << function(self)


    if IsNull(self.view.transform) then
        return
    end
    if not CS.UnityEngine.Application.isFocused then return end

    if DeviceInfo.usingKeyboard then
        self:_UpdateMouseHintStates()
    elseif DeviceInfo.usingController then
        if not self.m_isCollapsed then
            return
        end
        if LuaSystemManager.factory.topViewControllerMouseMoveTargetChanged then
            LuaSystemManager.factory.topViewControllerMouseMoveTargetChanged = false
            local mouseWorldPos = LuaSystemManager.factory.topViewControllerMouseMoveTarget.position


            local curScreenWorldRect = CSFactoryUtil.GetCurScreenWorldRect(FacConst.FAC_TOP_VIEW_CONTROLLER_MOUSE_PADDING)
            mouseWorldPos.x = lume.clamp(mouseWorldPos.x, curScreenWorldRect.xMin, curScreenWorldRect.xMax)
            mouseWorldPos.z = lume.clamp(mouseWorldPos.z, curScreenWorldRect.yMin, curScreenWorldRect.yMax)
            LuaSystemManager.factory.topViewControllerMouseMoveTarget.position = mouseWorldPos

            local screenPos = CameraManager.mainCamera:WorldToScreenPoint(mouseWorldPos):XY()
            local screenSize = Vector2(Screen.width, Screen.height)
            local newPos = (screenPos - screenSize / 2) / Screen.width * self.view.rectTransform.rect.width
            self.view.controllerMouse.anchoredPosition = newPos
        end
        self:_UpdateControllerMouseHintStates()
    end
end






FacTopViewCtrl.m_typeInfos = HL.Field(HL.Table)

FacTopViewCtrl.m_selectedTypeIndex = HL.Field(HL.Number) << 1

FacTopViewCtrl._InitInfos = HL.Method() << function(self)
    local typeInfos = {}
    self.view.beltNode.gameObject:SetActive(GameInstance.remoteFactoryManager.unlockSystem.systemUnlockedBelt)
    self.view.pipeNode.gameObject:SetActive(FactoryUtils.canShowPipe())

    table.insert(typeInfos, self:_GenCustomTypeInfo())

    local inventory = GameInstance.player.inventory
    local curDomainId = FactoryUtils.getCurAndAutoTransferBlackBoxToDomainId()
    local tInfosDic = {
        ["logistic"] = self:_GetLogisticInfos()
    }
    for id, data in pairs(Tables.factoryBuildingTable) do
        local typeId = data.quickBarType
        if not string.isEmpty(typeId) then
            local itemData = FactoryUtils.getBuildingItemData(id)
            if inventory:IsItemFound(itemData.id) and not FactoryUtils.isSkipBuildingInvalidInDomain(id, curDomainId) then
                local tInfo = tInfosDic[typeId]
                if not tInfo then
                    local typeData = Tables.factoryQuickBarTypeTable:GetValue(typeId)
                    tInfo = {
                        data = typeData,
                        priority = typeData.priority,
                        allItems = {},
                        showingItems = {},
                    }
                    tInfosDic[typeId] = tInfo
                end
                if tInfo then
                    local info = {
                        id = id,
                        itemId = itemData.id,
                        rarity = itemData.rarity,
                        sortId1 = itemData.sortId1,
                        sortId2 = itemData.sortId2,
                        type = QuickBarItemType.Building,
                    }
                    FactoryUtils.addBuildingDomainSortFilterInfo(info, data, curDomainId)
                    table.insert(tInfo.allItems, info)
                end
            end
        end
    end
    for _, info in pairs(tInfosDic) do
        table.insert(typeInfos, info)
    end

    table.sort(typeInfos, Utils.genSortFunction({ "priority" }))
    self.m_typeInfos = typeInfos
end

FacTopViewCtrl._GenCustomTypeInfo = HL.Method().Return(HL.Table) << function(self)
    local fcType = GEnums.FCQuickBarType.Inner
    local curChapterInfo = GameInstance.player.remoteFactory.core:GetCurrentChapterInfo()
    local quickBarList = curChapterInfo:GetQuickBar(fcType)

    local typeData = Tables.factoryQuickBarTypeTable:GetValue("custom")
    local typeInfo = {
        data = typeData,
        priority = typeData.priority,
        noFilter = true,
        showingItems = {},
    }
    for _, id in pairs(quickBarList) do
        local info = {
            itemId = id,
            isCustomQuickBarItem = true,
        }
        if not string.isEmpty(id) then
            local buildingData = FactoryUtils.getItemBuildingData(id)
            if buildingData then
                info.type = QuickBarItemType.Building
            else
                info.type = QuickBarItemType.Logistic
            end
        end
        table.insert(typeInfo.showingItems, info)
    end
    return typeInfo
end


FacTopViewCtrl._GetLogisticInfos = HL.Method().Return(HL.Opt(HL.Table)) << function(self)
    local typeData = Tables.factoryQuickBarTypeTable:GetValue("logistic")
    local curDomainId = FactoryUtils.getCurAndAutoTransferBlackBoxToDomainId()

    local typeInfo = {
        data = typeData,
        priority = typeData.priority,
        allItems = {},
        showingItems = {},
    }
    for id, data in pairs(Tables.factoryGridBeltTable) do
        if id ~= FacConst.BELT_ID and FactoryUtils.isLogisticUnlocked(id, curDomainId) then
            local item = {
                id = id,
                itemId = data.beltData.itemId,
                type = QuickBarItemType.Belt,
                data = data.beltData,
                conveySpeed = 1000000 / data.beltData.msPerRound,
            }
            table.insert(typeInfo.allItems, item)
        end
    end
    for id, data in pairs(Tables.FactoryBoxValveTable) do
        if FactoryUtils.isLogisticUnlocked(id, curDomainId) then
            local item = {
                id = id,
                itemId = data.gridUnitData.itemId,
                type = QuickBarItemType.Logistic,
                data = data.gridUnitData,
                conveySpeed = 1000000 / data.gridUnitData.msPerRound,
            }
            table.insert(typeInfo.allItems, item)
        end
    end
    for id, data in pairs(Tables.factoryGridConnecterTable) do
        if FactoryUtils.isLogisticUnlocked(id, curDomainId) then
            local item = {
                id = id,
                itemId = data.gridUnitData.itemId,
                type = QuickBarItemType.Logistic,
                data = data.gridUnitData,
                conveySpeed = 1000000 / data.gridUnitData.msPerRound,
            }
            table.insert(typeInfo.allItems, item)
        end
    end
    for id, data in pairs(Tables.factoryGridRouterTable) do
        if FactoryUtils.isLogisticUnlocked(id, curDomainId) then
            local item = {
                id = id,
                itemId = data.gridUnitData.itemId,
                type = QuickBarItemType.Logistic,
                data = data.gridUnitData,
                conveySpeed = 1000000 / data.gridUnitData.msPerRound,
            }
            table.insert(typeInfo.allItems, item)
        end
    end


    if FactoryUtils.isDomainSupportPipe() then
        for id, data in pairs(Tables.factoryLiquidPipeTable) do
            if id ~= FacConst.PIPE_ID and FactoryUtils.isLogisticUnlocked(id, curDomainId) then
                local item = {
                    id = id,
                    itemId = data.pipeData.itemId,
                    type = QuickBarItemType.Belt,
                    data = data.pipeData,
                    conveySpeed = 1000000 / data.pipeData.msPerRound,
                    recommendDomains = FactoryUtils.GetAllowPipeDoaminList(),
                }
                table.insert(typeInfo.allItems, item)
            end
        end
        for id, data in pairs(Tables.factoryFluidValveTable) do
            if FactoryUtils.isLogisticUnlocked(id, curDomainId) then
                local item = {
                    id = id,
                    itemId = data.liquidUnitData.itemId,
                    type = QuickBarItemType.Logistic,
                    data = data.liquidUnitData,
                    conveySpeed = 1000000 / data.liquidUnitData.msPerRound,
                    recommendDomains = FactoryUtils.GetAllowPipeDoaminList(),
                }
                table.insert(typeInfo.allItems, item)
            end
        end
        for id, data in pairs(Tables.factoryLiquidConnectorTable) do
            if FactoryUtils.isLogisticUnlocked(id, curDomainId) then
                local item = {
                    id = id,
                    liquidUnitId = id,
                    itemId = data.liquidUnitData.itemId,
                    type = QuickBarItemType.Logistic,
                    data = data.liquidUnitData,
                    conveySpeed = 1000000 / data.liquidUnitData.msPerRound,
                    recommendDomains = FactoryUtils.GetAllowPipeDoaminList(),
                }
                table.insert(typeInfo.allItems, item)
            end
        end
        for id, data in pairs(Tables.factoryLiquidRouterTable) do
            if FactoryUtils.isLogisticUnlocked(id, curDomainId) then
                local item = {
                    id = id,
                    liquidUnitId = id,
                    itemId = data.liquidUnitData.itemId,
                    type = QuickBarItemType.Logistic,
                    data = data.liquidUnitData,
                    conveySpeed = 1000000 / data.liquidUnitData.msPerRound,
                    recommendDomains = FactoryUtils.GetAllowPipeDoaminList(),
                }
                table.insert(typeInfo.allItems, item)
            end
        end
    end


    local filteredItems = {}
    for _, v in ipairs(typeInfo.allItems) do
        if not FactoryUtils.isSkipBuildingInvalidInDomain(v.id, curDomainId) then
            table.insert(filteredItems, v)
        end
    end
    typeInfo.allItems = filteredItems

    local hasItem
    for _, v in ipairs(typeInfo.allItems) do
        hasItem = true
        local itemData = Tables.itemTable[v.itemId]
        v.rarity = itemData.rarity
        v.sortId1 = itemData.sortId1
        v.sortId2 = itemData.sortId2
    end
    if not hasItem then
        return nil
    end
    return typeInfo
end

FacTopViewCtrl._RefreshTypes = HL.Method() << function(self)
    self:_InitInfos()
    local count = #self.m_typeInfos
    self.m_selectedTypeIndex = math.min(math.max(self.m_selectedTypeIndex, 1), count)
    self.m_typeCells:Refresh(count - 1, function(cell, cellIndex)
        self:_UpdateTypeCell(cell, cellIndex + 1)
    end)
    self:_UpdateTypeCell(self.view.customTypeCell, 1)

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.typeList.transform)
    local bgWidth = self.view.typeList.transform.rect.width - self.view.customTypeCell.transform.rect.width
    self.view.typeNodeBG.transform.sizeDelta = Vector2(bgWidth, self.view.typeNodeBG.transform.sizeDelta.y)

    self:_OnClickType(self.m_selectedTypeIndex)
end

FacTopViewCtrl._UpdateTypeCell = HL.Method(HL.Table, HL.Number) << function(self, cell, tabIndex)
    local info = self.m_typeInfos[tabIndex]
    cell.icon:LoadSprite(info.data.icon)
    cell.iconShadow:LoadSprite(string.format("%s_shadow", info.data.icon))
    cell.text.text = info.data.name
    cell.gameObject.name = "TypeTabCell_" .. info.data.id

    cell.toggle.onValueChanged:RemoveAllListeners()
    cell.toggle.isOn = tabIndex == self.m_selectedTypeIndex
    cell.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:_OnClickType(tabIndex)
            if DeviceInfo.usingController then
                self:SetNaviTarget(self.m_getItemCell(1).button)
            end
        end
    end)
end


FacTopViewCtrl._OnClickType = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, index, noAutoExpand)
    self.m_selectedTypeIndex = index
    local tInfo = self.m_typeInfos[self.m_selectedTypeIndex]
    self:_ApplyFilter()
    self:_RefreshItemList(true)
    self.view.filterNode.gameObject:SetActive(not tInfo.noFilter)
    if not noAutoExpand and self.m_isCollapsed then
        self:_ToggleContent(true)
    end


    local countInCache = self.m_typeCells:GetCount()
    self.m_typeCells:Update(function(cell, cellIndex)
        local tabIndex = cellIndex + 1
        cell.rightLine.gameObject:SetActive((tabIndex < index - 1) or (tabIndex > index and cellIndex ~= countInCache))
    end)

    self.view.scrollListLeftArrow.transform.localScale = index > 1 and Vector3.one or Vector3.zero
    self.view.scrollListRightArrow.transform.localScale = index < (countInCache + 1) and Vector3.one or Vector3.zero
end

FacTopViewCtrl._OnOverScrollList = HL.Method(HL.Boolean) << function(self, isNext)
    if not UIUtils.isScreenPosInRectTransform(InputManager.mousePosition, self.view.scrollList.transform, self.uiCamera) then
        return
    end

    if isNext and self.m_selectedTypeIndex == #self.m_typeInfos then
        return
    end
    if not isNext and self.m_selectedTypeIndex == 1 then
        return
    end
    local newIndex = self.m_selectedTypeIndex + (isNext and 1 or -1)
    if newIndex == 1 then
        self.view.customTypeCell.toggle.isOn = true
    else
        self.m_typeCells:Get(newIndex - 1).toggle.isOn = true
    end
    GameInstance.mobileMotionManager:PostEventCommonShort()
end

FacTopViewCtrl._RefreshItemList = HL.Method(HL.Opt(HL.Boolean)) << function(self, toTop)
    local tInfo = self.m_typeInfos[self.m_selectedTypeIndex]
    local count = tInfo and #tInfo.showingItems or 0
    self.view.scrollList:UpdateCount(count, toTop == true)
    self.view.scrollListEmptyNode.gameObject:SetActive(count == 0)
end

FacTopViewCtrl._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local tInfo = self.m_typeInfos[self.m_selectedTypeIndex]
    local info = tInfo.showingItems[index]
    local itemId = info.itemId
    local isEmpty = string.isEmpty(itemId)

    cell.gameObject.name = "Item_" .. (isEmpty and index or itemId)

    cell.button.onClick:RemoveAllListeners()
    cell.button.onLongPress:RemoveAllListeners()
    cell.dragItem.enabled = not isEmpty
    cell.dragItem:ClearEvents()
    cell.content.gameObject:SetActive(not isEmpty)
    cell.emptyNode.gameObject:SetActive(isEmpty)

    if info.isCustomQuickBarItem then
        local actionId = "fac_use_quick_item_" .. index
        cell.button.onClick:ChangeBindingPlayerAction(actionId)
    else
        cell.button.onClick:StopUseBinding()
    end

    cell.button.onDoubleClick:RemoveAllListeners()
    if isEmpty then
        cell.button.onClick:RemoveAllListeners()
        cell.button.onLongPress:RemoveAllListeners()
        InputManagerInst:DeleteInGroup(cell.button.hoverBindingGroupId)
        if DeviceInfo.usingController then
            cell.controllerKeyHint:SetActionId("")
            cell.controllerKeyHint.gameObject:SetActive(cell.button.isNaviTarget)
        end
        return
    end

    local count
    if info.type == QuickBarItemType.Building then
        count = isEmpty and 0 or Utils.getItemCount(itemId)
    end

    cell.item:InitItem({ id = itemId, count = count })
    cell.typeIcon:LoadSprite(tInfo.data.icon)

    InputManagerInst:DeleteInGroup(cell.button.hoverBindingGroupId)
    InputManagerInst:CreateBindingByActionId("fac_quick_bar_controller_build_top_view", function()
        self:_OnClickItemCell(index)
    end, cell.button.hoverBindingGroupId)
    InputManagerInst:CreateBindingByActionId("show_item_tips", function()
        cell.item:ShowTips()
    end, cell.button.hoverBindingGroupId)

    cell.button.onClick:AddListener(function()
        if not isEmpty then
            self:_OnClickItemCell(index)
        end
    end)
    cell.button.onLongPress:AddListener(function()
        if not isEmpty then
            cell.item:ShowTips()
        end
    end)

    local isSkipBuilding = FactoryUtils.isSkipUnlockedBuildingByItemId(itemId)
    local hasCraft = Tables.FactoryItemAsHubCraftOutcomeTable:TryGetValue(itemId)
    if isSkipBuilding then

        if DeviceInfo.usingController then
            InputManagerInst:CreateBindingByActionId("fac_quick_bar_controller_craft", function()
                if Utils.getItemCount(itemId) == 0 then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_QUICK_BAR_SKIP_BUILDING_CANNOT_CRAFT_IN_DOMAIN)
                end
            end, cell.button.hoverBindingGroupId)
        else
            cell.button.onDoubleClick:AddListener(function()
                if Utils.getItemCount(itemId) == 0 then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_QUICK_BAR_SKIP_BUILDING_CANNOT_CRAFT_IN_DOMAIN)
                end
            end)
        end
    elseif hasCraft then
        if DeviceInfo.usingController then
            InputManagerInst:CreateBindingByActionId("fac_quick_bar_controller_craft", function()
                Notify(MessageConst.OPEN_FAC_BUILD_MODE_SELECT, { selectedId = itemId })
            end, cell.button.hoverBindingGroupId)
        else
            cell.button.onDoubleClick:AddListener(function()
                if Utils.getItemCount(itemId) == 0 then
                    Notify(MessageConst.OPEN_FAC_BUILD_MODE_SELECT, { selectedId = itemId })
                end
            end)
        end
    elseif FactoryUtils.isDecoBuildingItem(itemId) then
        InputManagerInst:CreateBindingByActionId("fac_quick_bar_controller_craft", function()
            if Utils.getItemCount(itemId) == 0 then
                Notify(MessageConst.OPEN_FAC_BUILD_MODE_SELECT, { selectedId = itemId })
            end
        end, cell.button.hoverBindingGroupId)
        cell.button.onDoubleClick:AddListener(function()
            if Utils.getItemCount(itemId) == 0 then
                Notify(MessageConst.OPEN_FAC_BUILD_MODE_SELECT, { selectedId = itemId })
            end
        end)
    end
    if DeviceInfo.usingController then
        cell.controllerKeyHint:SetActionId((not isSkipBuilding and hasCraft and count == 0) and "fac_quick_bar_controller_craft" or "fac_quick_bar_controller_build_top_view")
        cell.controllerKeyHint.gameObject:SetActive(cell.button.isNaviTarget)
    end

    local itemData = Tables.itemTable:GetValue(itemId)
    cell.name.text = itemData.name
    UIUtils.initUIDragHelper(cell.dragItem, {
        source = info.isCustomQuickBarItem and UIConst.UI_DRAG_DROP_SOURCE_TYPE.QuickBar or UIConst.UI_DRAG_DROP_SOURCE_TYPE.BuildModeSelect,
        type = itemData.type,
        itemId = itemId,
        csIndex = CSIndex(index),
        onBeginDrag = function(enterObj, enterDropHelper)
            self:_OnQuickBarBeginDrag(index, enterObj, enterDropHelper)
        end,
        onDrag = function(eventData)
            self:_OnQuickBarDrag(eventData)
        end,
        onEndDrag = function(enterObj, enterDrop, eventData)
            self:_OnQuickBarEndDrag(index, enterObj, enterDrop, eventData)
        end,
        onDropTargetChanged = function(enterObj, dropHelper)
            if not info.isCustomQuickBarItem then
                return
            end
            local dragObj = cell.dragItem.curDragObj
            if not dragObj then
                return
            end
            local dragItem = dragObj:GetComponent("LuaUIWidget").table[1]
            if not dropHelper or not dropHelper.info.isQuickBarClearDropZone then
                dragItem.view.clearNode.gameObject:SetActive(false)
                return
            end
            dragItem.view.clearNode.gameObject:SetActive(true)
        end,
    })
    cell.dragItem.onUpdateDragObject:AddListener(function(dragObj)
        local dragItem = UIWidgetManager:Wrap(dragObj)
        dragItem:InitItem({ id = itemId })
    end)
end

FacTopViewCtrl._OnQuickBarBeginDrag = HL.Method(HL.Number, HL.Opt(HL.Userdata, HL.Forward('UIDropHelper'))) << function(self, index, enterObj, enterDropHelper)
    self.view.scrollList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.m_getItemCell(obj)
        cell.button.animator:SetBool("IgnoreHighlight", true)
    end)
    self.view.clearDropZoneRoot.gameObject:SetActive(false)
end

FacTopViewCtrl._OnQuickBarDrag = HL.Method(CS.UnityEngine.EventSystems.PointerEventData) << function(self, eventData)
    local mousePos = eventData.position
    local inScrollRect = CS.Beyond.UI.UIUtils.IsScreenPosInRectTransform(mousePos, self.view.scrollList.transform, self.uiCamera)
    self.view.clearDropZoneRoot.gameObject:SetActive(not inScrollRect)
end

FacTopViewCtrl._OnQuickBarEndDrag = HL.Method(HL.Number, HL.Opt(HL.Userdata, HL.Forward('UIDropHelper'), HL.Any)) << function(self, index, enterObj, enterDrop, eventData)
    self.view.scrollList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.m_getItemCell(obj)
        cell.button.animator:SetBool("IgnoreHighlight", false)
    end)
    if not eventData then
        return
    end
    if enterDrop then
        return
    end
    if enterObj ~= UIManager.commonTouchPanel.gameObject then
        return
    end

    self:_OnClickItemCell(index, eventData.position)
end

FacTopViewCtrl._ToggleContent = HL.Method(HL.Boolean, HL.Opt(HL.Boolean, HL.Boolean)) << function(self, active, fastMode, isToBuildOrDesMode)
    self.m_isCollapsed = not active
    local ani = self.view.bottomNode
    if fastMode then
        if active then
            ani:SampleToInAnimationEnd()
        else
            ani:SampleToOutAnimationEnd()
        end
    else
        if active then
            ani:PlayInAnimation()
        else
            ani:PlayOutAnimation()
        end
    end
    if DeviceInfo.usingController then
        self.view.controllerMouse.gameObject:SetActive(not active)
        self.view.controllerMouseHoverHint.gameObject:SetActive(not active and self.m_lastMouseHintContent ~= nil)
        self.view.topViewToggle.gameObject:SetActive(not active)
        self.view.hidePipeToggle.gameObject:SetActive(FactoryUtils.canShowPipe() and not active)
        if active then

            Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
                panelId = PANEL_ID,
                isGroup = true,
                id = self.view.bottomNodeInputBindingGroupMonoTarget.groupId,
                rectTransform = self.view.buildNode.transform,
                noHighlight = true,
            })

            self.view.scrollList:SetTop(false)
            self:SetNaviTarget(self.m_getItemCell(1).button)
            self.m_clearScreenKeyForControllerExpandBuildNode = UIManager:ClearScreen({
                PANEL_ID, PanelId.FacTopViewBuildingInfo, PanelId.FacTopViewLowerCfg,
                PanelId.FacBuildMode, PanelId.FacDestroyMode, PanelId.CommonTaskTrackHud,
                PanelId.FacPowerPoleLinkingLabel, PanelId.FacPowerPoleTravelHint,
                PanelId.FacPowerPoleAutoConnectHint,
            })
        else
            Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.bottomNodeInputBindingGroupMonoTarget.groupId)
            InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.container)
            if isToBuildOrDesMode then

                self:_StartTimer(0, function()
                    self.m_clearScreenKeyForControllerExpandBuildNode = UIManager:RecoverScreen(self.m_clearScreenKeyForControllerExpandBuildNode)
                end)
            else
                self.m_clearScreenKeyForControllerExpandBuildNode = UIManager:RecoverScreen(self.m_clearScreenKeyForControllerExpandBuildNode)
            end
        end
        if fastMode then
            self.view.startBuildBtn.gameObject:SetActive(not active)
        else
            UIUtils.PlayAnimationAndToggleActive(self.view.startBuildBtnAnimationWrapper, not active)
        end
    end
    self.view.buildNode.enabled = active or not DeviceInfo.usingController
end

FacTopViewCtrl.m_clearScreenKeyForControllerExpandBuildNode = HL.Field(HL.Number) << -1

FacTopViewCtrl._OnClickItemCell = HL.Method(HL.Number, HL.Opt(Vector2)) << function(self, index, mousePosition)
    local info = self.m_typeInfos[self.m_selectedTypeIndex].showingItems[index]
    if info.type == QuickBarItemType.Building then
        local itemId = info.itemId
        local count, backpackCount = Utils.getItemCount(itemId)
        if count == 0 then
            if FactoryUtils.isSkipUnlockedBuildingByItemId(itemId) then

                Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_QUICK_BAR_COUNT_ZERO_NO_JUMP)
            else
                local hasCraft = Tables.FactoryItemAsHubCraftOutcomeTable:TryGetValue(itemId)
                if hasCraft then
                    if DeviceInfo.usingController then
                        Notify(MessageConst.SHOW_TOAST, InputManager.ParseTextActionId(Language.LUA_FAC_QUICK_BAR_COUNT_ZERO_CT))
                    elseif DeviceInfo.usingTouch then
                        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_QUICK_BAR_COUNT_ZERO_TOUCH)
                    else
                        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_QUICK_BAR_COUNT_ZERO)
                    end
                elseif FactoryUtils.isDecoBuildingItem(itemId) then
                    if DeviceInfo.usingController then
                        Notify(MessageConst.SHOW_TOAST, InputManager.ParseTextActionId(Language.LUA_FAC_QUICK_BAR_DECO_COUNT_ZERO_CT))
                    elseif DeviceInfo.usingTouch then
                        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_QUICK_BAR_DECO_COUNT_ZERO_TOUCH)
                    else
                        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_QUICK_BAR_DECO_COUNT_ZERO)
                    end
                else
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_QUICK_BAR_COUNT_ZERO_NO_JUMP)
                end
            end
            return
        end
        Notify(MessageConst.FAC_ENTER_BUILDING_MODE, {
            itemId = itemId,
            initMousePos = mousePosition,
        })
    elseif info.type == QuickBarItemType.Belt then
        Notify(MessageConst.FAC_ENTER_BELT_MODE, {
            beltId = info.id,
            initMousePos = mousePosition,
        })
    elseif info.type == QuickBarItemType.Logistic then
        Notify(MessageConst.FAC_ENTER_LOGISTIC_MODE, {
            itemId = info.itemId,
            initMousePos = mousePosition,
        })
    end
end

FacTopViewCtrl._OnClickPipe = HL.Method() << function(self)
    Notify(MessageConst.FAC_ENTER_BELT_MODE, { beltId = FacConst.PIPE_ID })
end

FacTopViewCtrl._OnClickBelt = HL.Method() << function(self)
    Notify(MessageConst.FAC_ENTER_BELT_MODE, { beltId = FacConst.BELT_ID })
end






FacTopViewCtrl.m_filterInfos = HL.Field(HL.Table)

FacTopViewCtrl.m_selectedFilters = HL.Field(HL.Table)

FacTopViewCtrl.m_filterCells = HL.Field(HL.Forward('UIListCache'))

FacTopViewCtrl._InitFilters = HL.Method() << function(self)
    self.m_filterInfos = {}
    self.m_selectedFilters = {}

    local mapManager = GameInstance.player.mapManager
    for _, domainData in pairs(Tables.domainDataTable) do
        local isDomainUnlocked = true
        for _, levelId in pairs(domainData.levelGroup) do
            if mapManager:IsLevelUnlocked(levelId) then
                isDomainUnlocked = true
                break
            end
        end
        if isDomainUnlocked then
            table.insert(self.m_filterInfos, {
                id = domainData.domainId,
                name = domainData.domainName,
                sortId = domainData.sortId,
                icon = domainData.domainIcon
            })
        end
    end
    table.sort(self.m_filterInfos, Utils.genSortFunction({ "sortId" }, true))

    local node = self.view.filterNode
    node.filterBtn.onClick:AddListener(function()
        self:_ToggleFilterList(true)
    end)
    node.filteredBtn.onClick:AddListener(function()
        self:_ToggleFilterList(true)
    end)
    node.confirmBtn.onClick:AddListener(function()
        self:_ToggleFilterList(false)
    end)
    node.list.onTriggerAutoClose:AddListener(function()
        self:_ToggleFilterList(false)
    end)
    self:_ToggleFilterList(false, true)
    node.controllerHintPlaceholder:InitControllerHintPlaceholder({node.listInputBindingGroupMonoTarget.groupId})

    self.m_filterCells = self.m_filterCells or UIUtils.genCellCache(node.optionCell)
    self.m_filterCells:Refresh(#self.m_filterInfos, function(cell, index)
        local info = self.m_filterInfos[index]
        cell.name.text = info.name
        cell.icon:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT, info.icon)
        cell.toggle.isOn = false
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self.m_selectedFilters[info.id] = true
            else
                self.m_selectedFilters[info.id] = nil
            end
            self:_ApplyFilter()
            self:_RefreshItemList(true)
        end)
    end)
end

FacTopViewCtrl._ResetFilters = HL.Method() << function(self)
    self.m_selectedFilters = {}
    self:_UpdateFilterCellStates()
end

FacTopViewCtrl._UpdateFilterCellStates = HL.Method() << function(self)
    self.m_filterCells:Update(function(cell, index)
        local info = self.m_filterInfos[index]
        cell.toggle:SetIsOnWithoutNotify(self.m_selectedFilters[info.id] == true)
    end)
    self:_UpdateFilterIcon()
end


FacTopViewCtrl._ToggleFilterList = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, skipAni)
    local node = self.view.filterNode
    if skipAni then
        node.list.gameObject:SetActive(active)
    else
        UIUtils.PlayAnimationAndToggleActive(node.listAnimationWrapper, active)
    end
    self:_UpdateFilterIcon(active)
    if DeviceInfo.usingController then
        if active then
            node.listSelectableNaviGroup:ManuallyFocus()
        else

            local cell = self.m_getItemCell(1)
            if cell then
                self:SetNaviTarget(cell.button)
            end
            node.listSelectableNaviGroup:ManuallyStopFocus()
        end
    end
end

FacTopViewCtrl._UpdateFilterIcon = HL.Method(HL.Opt(HL.Boolean)) << function(self, active)
    local node = self.view.filterNode
    if active == nil then
        active = node.list.gameObject.activeInHierarchy
    end
    node.confirmBtn.gameObject:SetActive(active)
    node.filterBtn.gameObject:SetActive(not active and not next(self.m_selectedFilters))
    node.filteredBtn.gameObject:SetActive(not active and next(self.m_selectedFilters) ~= nil)
end

FacTopViewCtrl._ApplyFilter = HL.Method() << function(self)
    local tInfo = self.m_typeInfos[self.m_selectedTypeIndex]
    if tInfo.noFilter then
        return
    end
    local hasFilter = next(self.m_selectedFilters) ~= nil
    tInfo.showingItems = {}
    for _, v in ipairs(tInfo.allItems) do
        if hasFilter then
            if v.recommendDomains and next(v.recommendDomains) then
                for _, domainId in ipairs(v.recommendDomains) do
                    if self.m_selectedFilters[domainId] then
                        table.insert(tInfo.showingItems, v)
                        break
                    end
                end
            else
                table.insert(tInfo.showingItems, v)
            end
        else
            table.insert(tInfo.showingItems, v)
        end
    end
    table.sort(tInfo.showingItems, Utils.genSortFunction({ "domainReverseSort", "sortId1", "sortId2", "rarity" }, true))
end






local MouseHints = {
    building = {
        normal = "FAC_TOP_VIEW_MOUSE_HOVER_HINT_BUILDING",
        des = "FAC_TOP_VIEW_MOUSE_HOVER_HINT_BUILDING_DES",
    },
    pending = {
        normal = "LUA_FAC_TOP_VIEW_MOUSE_HOVER_HINT_PENDING",
        des = "FAC_TOP_VIEW_MOUSE_HOVER_HINT_BUILDING_DES",
    },
    belt = {
        normal = "FAC_TOP_VIEW_MOUSE_HOVER_HINT_LOGISTIC",
        des = "FAC_TOP_VIEW_MOUSE_HOVER_HINT_BELT_DES",
    },
    logistic = {
        normal = "FAC_TOP_VIEW_MOUSE_HOVER_HINT_LOGISTIC",
        des = "FAC_TOP_VIEW_MOUSE_HOVER_HINT_BUILDING_DES",
    },
    pipe = {
        normal = "FAC_TOP_VIEW_MOUSE_HOVER_HINT_LOGISTIC",
        des = "LUA_FAC_TOP_VIEW_MOUSE_HOVER_HINT_PIPE_DES",
    },
    multiTarget = {
        normal = "LUA_FAC_TOP_VIEW_MOUSE_HOVER_HINT_MULTI_TARGET",
    },
}

local ControllerMouseHints = {
    normal = { "fac_top_view_open_building_menu", "fac_top_view_move_building", "fac_top_view_open_building_panel" },
    normalNoMove = { "fac_top_view_open_building_menu", "fac_top_view_open_building_panel" },


    batchSelect = { "fac_batch_select", },
    batchSelectBeltOrPipe = { "fac_batch_select", "fac_batch_select_single_grid" },


    newBuilding = { "fac_build_confirm_in_top_view", "fac_build_continuous_confirm", "fac_rotate_device", "fac_build_cancel", },
    newBuildingCantRotate = { "fac_build_confirm_in_top_view", "fac_build_continuous_confirm", "fac_build_cancel", },
    oldBuilding = { "fac_build_confirm_in_top_view", "fac_rotate_device", "fac_build_mode_delete", "fac_build_cancel", },
    oldBuildingCantDes = { "fac_build_confirm_in_top_view", "fac_rotate_device", "fac_build_cancel", },
    beltStart = { "fac_build_confirm_belt_start_in_top_view", "fac_build_rotate_belt", "fac_build_cancel", },
    beltEnd = { "fac_build_confirm_belt_end_in_top_view", "fac_build_rotate_belt", "fac_build_cancel", },
    pipeStart = { "fac_build_confirm_belt_start_in_top_view", "fac_build_rotate_pipe", "fac_build_cancel", },
    pipeEnd = { "fac_build_confirm_belt_end_in_top_view", "fac_build_rotate_pipe", "fac_build_cancel", },
    blueprint = { "fac_build_confirm_in_top_view", "fac_rotate_device", "fac_build_cancel", },
}

FacTopViewCtrl.m_lastMouseHintContent = HL.Field(HL.Any)

FacTopViewCtrl.m_controllerMouseHoverHintCells = HL.Field(HL.Forward('UIListCache'))

FacTopViewCtrl._UpdateMouseHintStates = HL.Method() << function(self)

    local ctrl = LuaSystemManager.factory.interactPanelCtrl
    local content
    if self:_IsHoveringMultiTargetMenuGrid() then
        content = nil
    elseif UIManager.commonTouchPanel.isPointerEntered and not ctrl:IsDraggingInBatchMode() then
        local targetCount = 0
        if ctrl.m_interactPipeNodeId then
            targetCount = targetCount + 1
            if FactoryUtils.isPendingBuildingNode(ctrl.m_interactPipeNodeId) then
                content = MouseHints.pending
            else
                content = MouseHints.pipe
            end
        end
        if ctrl.m_interactFacNodeId then
            targetCount = targetCount + 1
            if FactoryUtils.isPendingBuildingNode(ctrl.m_interactFacNodeId) or not FactoryUtils.canMoveBuilding(ctrl.m_interactFacNodeId) then
                content = content or MouseHints.pending
            else
                if ctrl.m_interactFacNodeIdIsBuilding then
                    content = content or MouseHints.building
                else
                    content = content or MouseHints.logistic
                end
            end
        end
        if ctrl.m_interactFacOverlapNodeIds then

            targetCount = targetCount + #ctrl.m_interactFacOverlapNodeIds
        end
        if ctrl.m_interactLogisticPos then
            local succ, nodeId, unitIndex = GameInstance.remoteFactoryManager:TrySampleConveyor(ctrl.m_interactLogisticPos)
            if succ and nodeId then
                targetCount = targetCount + 1
                if FactoryUtils.isPendingBuildingNode(nodeId) then
                    content = content or MouseHints.pending
                else
                    content = content or MouseHints.belt
                end
            end
        end
        if targetCount > 1 and not LuaSystemManager.factory.inDestroyMode then
            content = MouseHints.multiTarget
        end
        if content then
            content = LuaSystemManager.factory.inDestroyMode and content.des or content.normal
        end
    end
    if content ~= self.m_lastMouseHintContent then
        self.m_lastMouseHintContent = content
        if content then
            self.view.mouseHoverHint.gameObject:SetActiveIfNecessary(true)
            self.view.mouseHoverHint.text.text = Language[content]
        else
            self.view.mouseHoverHint.gameObject:SetActiveIfNecessary(false)
        end
    end
end

FacTopViewCtrl._UpdateControllerMouseHintStates = HL.Method() << function(self)

    local ctrl = LuaSystemManager.factory.interactPanelCtrl
    if self:_IsMultiTargetMenuShowing() then
        self:_RefreshControllerMouseHints(nil)
        return
    end
    local actionIds
    if LuaSystemManager.factory.inDestroyMode then

        if not ctrl:IsDraggingInBatchMode() then
            if ctrl.m_interactLogisticPos or ctrl.m_interactPipeNodeId then
                local nodeId, _
                if ctrl.m_interactLogisticPos then
                    _, nodeId, _ = GameInstance.remoteFactoryManager:TrySampleConveyor(ctrl.m_interactLogisticPos)
                else
                    nodeId = ctrl.m_interactPipeNodeId
                end
                if FactoryUtils.isPendingBuildingNode(nodeId) then
                    actionIds = ControllerMouseHints.batchSelect
                else
                    actionIds = ControllerMouseHints.batchSelectBeltOrPipe
                end
            elseif ctrl.m_interactFacNodeId then
                actionIds = ControllerMouseHints.batchSelect
            end
        end
    else
        local _, buildModeCtrl = UIManager:IsOpen(PanelId.FacBuildMode)
        if buildModeCtrl and buildModeCtrl.m_mode ~= FacConst.FAC_BUILD_MODE.Normal then

            local mode = buildModeCtrl.m_mode
            if mode == FacConst.FAC_BUILD_MODE.Building then
                if buildModeCtrl.m_buildingNodeId then
                    actionIds = FactoryUtils.canDelBuilding(buildModeCtrl.m_buildingNodeId) and ControllerMouseHints.oldBuilding or ControllerMouseHints.oldBuildingCantDes
                else
                    actionIds = ControllerMouseHints.newBuilding
                end
            elseif mode == FacConst.FAC_BUILD_MODE.Logistic then
                if buildModeCtrl:_CanRotate() then
                    actionIds = ControllerMouseHints.newBuilding
                else
                    actionIds = ControllerMouseHints.newBuildingCantRotate
                end
            elseif mode == FacConst.FAC_BUILD_MODE.Belt then
                local isPipe = buildModeCtrl:_IsPipe()
                local hasStart = GameInstance.remoteFactoryManager.interact.currentConveyorMode.hasStart
                if isPipe then
                    actionIds = hasStart and ControllerMouseHints.pipeEnd or ControllerMouseHints.pipeStart
                else
                    actionIds = hasStart and ControllerMouseHints.beltEnd or ControllerMouseHints.beltStart
                end
            elseif mode == FacConst.FAC_BUILD_MODE.Blueprint then
                actionIds = ControllerMouseHints.blueprint
            end
        else

            if ctrl.m_interactFacNodeId or ctrl.m_interactLogisticPos or ctrl.m_interactPipeNodeId then
                if ctrl.m_interactFacNodeIdIsBuilding and FactoryUtils.canMoveBuilding(ctrl.m_interactFacNodeId) then
                    actionIds = ControllerMouseHints.normal
                else
                    actionIds = ControllerMouseHints.normalNoMove
                end
            end
        end
    end
    self:_RefreshControllerMouseHints(actionIds)
    if actionIds then
        self:_UpdateControllerOpenDetailHint()
    end
end




FacTopViewCtrl._UpdateControllerOpenDetailHint = HL.Method() << function(self)
    local cells = self.m_controllerMouseHoverHintCells
    if not cells then
        return
    end
    local count = cells:GetCount()
    if count == 0 then
        return
    end

    local cell = cells:GetItem(count)
    if not cell or cell.gameObject.name ~= "KeyHint-fac_top_view_open_building_panel" then
        return
    end

    local ctrl = LuaSystemManager.factory.interactPanelCtrl
    local liveCount = 0
    if ctrl.m_interactPipeNodeId then liveCount = liveCount + 1 end
    if ctrl.m_interactFacNodeId then liveCount = liveCount + 1 end
    if ctrl.m_interactFacOverlapNodeIds then liveCount = liveCount + #ctrl.m_interactFacOverlapNodeIds end
    if ctrl.m_interactLogisticPos then liveCount = liveCount + 1 end
    if liveCount > 1 then
        local targets = self:_CollectControllerTargets()
        if #targets > 1 then
            cell.actionKeyHint:SetText(string.format(Language.LUA_FAC_TOP_VIEW_CONTROLLER_OPEN_DETAIL_FORMAT, targets[1].name))
            return
        end
    end
    cell.actionKeyHint:SetText(cell.actionKeyHint:GetTextStr())
end

FacTopViewCtrl._RefreshControllerMouseHints = HL.Method(HL.Opt(HL.Table)) << function(self, actionIds)
    if actionIds == self.m_lastMouseHintContent then
        return
    end
    self.m_lastMouseHintContent = actionIds
    if not actionIds then
        self.view.controllerMouseHoverHint.gameObject:SetActiveIfNecessary(false)
        return
    end
    self.view.controllerMouseHoverHint.gameObject:SetActiveIfNecessary(true)
    if not self.m_controllerMouseHoverHintCells then
        self.m_controllerMouseHoverHintCells = UIUtils.genCellCache(self.view.controllerMouseHoverHint.keyHint)
    end
    self.m_controllerMouseHoverHintCells:Refresh(#actionIds, function(cell, index)
        local id = actionIds[index]
        cell.actionKeyHint:SetActionId(id)
        cell.gameObject.name = "KeyHint-" .. id
    end)
end






FacTopViewCtrl._InitKeyHints = HL.Method() << function(self)
    if DeviceInfo.usingTouch then
        self.view.keyHintNode.gameObject:SetActive(false)
        return
    end
    local actionNames
    if DeviceInfo.usingController then
        actionNames = {
            "fac_top_view_ct_move",
            "fac_top_view_ct_move_cam",
            "fac_top_view_ct_scale_cam",
            "fac_top_view_rot_cam",
            "fac_top_view_enter_batch_mode_ct",
        }
    elseif DeviceInfo.usingKeyboard then
        actionNames = {
            "fac_top_view_rot_cam",
        }
    end
    self.m_keyHintCells = self.m_keyHintCells or UIUtils.genCellCache(self.view.keyHintCell)
    self.m_keyHintCells:Refresh(#actionNames, function(cell, index)
        local actionId = actionNames[index]
        cell.actionKeyHint:SetActionId(actionId)
        cell.gameObject.name = "KeyHint-" .. actionId
    end)
    self.view.keyHintNode.gameObject:SetActive(true)
end






FacTopViewCtrl._OnControllerZoomCamera = HL.Method() << function(self)
    local delta = InputManagerInst:GetGamepadStickValue(false).y * self.view.config.CONTROLLER_ZOOM_CAMERA_SPD * -Time.deltaTime
    Notify(MessageConst.ZOOM_LEVEL_CAMERA, delta)
end

FacTopViewCtrl._ControllerOpenCurBuildingPanel = HL.Method() << function(self)


    local _, facInteract = UIManager:IsOpen(PanelId.FacBuildingInteract)
    facInteract:_OnClickScreen(nil, true)
end



FacTopViewCtrl._CollectControllerTargets = HL.Method().Return(HL.Table) << function(self)

    local _, facInteract = UIManager:IsOpen(PanelId.FacBuildingInteract)

    local targets = {}


    local chapterId = Utils.getCurrentChapterId()
    local seenPendingSlotIds = {}

    if facInteract.m_interactPipeNodeId then
        local pipeNodeId = facInteract.m_interactPipeNodeId
        local pipePendingSlotId = CSFactoryUtil.GetBlueprintSlotId(chapterId, pipeNodeId)
        local isPendingPipe = pipePendingSlotId > 0
        if not isPendingPipe or not seenPendingSlotIds[pipePendingSlotId] then
            if isPendingPipe then
                seenPendingSlotIds[pipePendingSlotId] = true
            end
            table.insert(targets, {
                type = "pipe",
                sortBase = 100,
                name = isPendingPipe
                    and FactoryUtils.getPendingSlotName(pipePendingSlotId)
                    or Language.LUA_FAC_PIPE_INTERACT_OPTION,
                nodeId = pipeNodeId,
                unitIndex = facInteract.m_interactPipeUnitIndex,
            })
        end
    end
    if facInteract.m_interactFacNodeId then
        local nodeId = facInteract.m_interactFacNodeId
        local buildingPendingSlotId = CSFactoryUtil.GetBlueprintSlotId(chapterId, nodeId)
        local isPendingBuilding = buildingPendingSlotId > 0
        if isPendingBuilding then

            if not seenPendingSlotIds[buildingPendingSlotId] then
                seenPendingSlotIds[buildingPendingSlotId] = true
                table.insert(targets, {
                    type = "building",
                    sortBase = 200,
                    name = FactoryUtils.getPendingSlotName(buildingPendingSlotId),
                    nodeId = nodeId,
                    isBuilding = true,
                    subBuildingIndex = -1,
                })
            end
        else
            local isBuilding = facInteract.m_interactFacNodeIdIsBuilding
            local subIndex = facInteract.m_interactSubBuildingIndex
            local handler = FactoryUtils.getBuildingNodeHandler(nodeId)
            local targetName
            if isBuilding then
                if subIndex >= 0 then

                    targetName = Language.LUA_FAC_HUB_INPUT .. subIndex
                else
                    targetName = Tables.factoryBuildingTable[handler.templateId].name
                end
            else
                targetName = FactoryUtils.getLogisticData(handler.templateId).name
            end
            table.insert(targets, {
                type = "building",
                sortBase = 200,
                name = targetName,
                nodeId = nodeId,
                isBuilding = isBuilding,
                subBuildingIndex = subIndex,
            })
        end
    end
    if facInteract.m_interactFacOverlapNodeIds then



        for i, overlapNodeId in ipairs(facInteract.m_interactFacOverlapNodeIds) do
            local overlapPendingSlotId = CSFactoryUtil.GetBlueprintSlotId(chapterId, overlapNodeId)
            local isOverlapPending = overlapPendingSlotId > 0
            if isOverlapPending then
                if not seenPendingSlotIds[overlapPendingSlotId] then
                    seenPendingSlotIds[overlapPendingSlotId] = true
                    table.insert(targets, {
                        type = "building",
                        sortBase = 150 + 10 * i,
                        name = FactoryUtils.getPendingSlotName(overlapPendingSlotId),
                        nodeId = overlapNodeId,
                        isBuilding = true,
                        subBuildingIndex = -1,
                    })
                end
            else
                local handler = FactoryUtils.getBuildingNodeHandler(overlapNodeId)
                if handler then
                    local _, buildingData = Tables.factoryBuildingTable:TryGetValue(handler.templateId)
                    local overlapIsBuilding = buildingData ~= nil
                    local overlapName
                    if overlapIsBuilding then
                        overlapName = buildingData.name
                    else
                        local unitData = FactoryUtils.getLogisticData(handler.templateId)
                        overlapName = unitData and unitData.name
                    end
                    if overlapName then
                        table.insert(targets, {
                            type = "building",
                            sortBase = 150 + 10 * i,
                            name = overlapName,
                            nodeId = overlapNodeId,
                            isBuilding = overlapIsBuilding,
                        })
                    end
                end
            end
        end
    end
    if facInteract.m_interactLogisticPos then
        local logisticPos = facInteract.m_interactLogisticPos
        local succ, beltNodeId, beltUnitIndex = GameInstance.remoteFactoryManager:TrySampleConveyor(logisticPos)
        if succ and beltNodeId then
            local beltPendingSlotId = CSFactoryUtil.GetBlueprintSlotId(chapterId, beltNodeId)
            local isPendingBelt = beltPendingSlotId > 0
            if not isPendingBelt or not seenPendingSlotIds[beltPendingSlotId] then
                if isPendingBelt then
                    seenPendingSlotIds[beltPendingSlotId] = true
                end
                local beltName
                if isPendingBelt then
                    beltName = FactoryUtils.getPendingSlotName(beltPendingSlotId)
                else
                    local chapterInfo = FactoryUtils.getCurChapterInfo()
                    local handler = chapterInfo:GetNode(beltNodeId)
                    beltName = Tables.factoryGridBeltTable:GetValue(handler.templateId).beltData.name
                end
                table.insert(targets, {
                    type = "belt",
                    sortBase = 300,
                    name = beltName,
                    nodeId = beltNodeId,
                    unitIndex = beltUnitIndex,
                    logisticPos = logisticPos,
                })
            end
        end
    end
    table.sort(targets, function(a, b) return a.sortBase < b.sortBase end)
    return targets
end

FacTopViewCtrl._ControllerOpenCurBuildingMenu = HL.Method() << function(self)

    local _, facInteract = UIManager:IsOpen(PanelId.FacBuildingInteract)

    local targets = self:_CollectControllerTargets()
    if #targets == 0 then
        return
    end

    local isMulti = #targets > 1
    local actions = {}
    for _, target in ipairs(targets) do
        self:_AddControllerMenuActionsForTarget(actions, target, isMulti)
    end
    table.sort(actions, function(a, b) return a.priority < b.priority end)

    local effect = facInteract.m_hoverInteractHighlightEffect
    local posList = {
        effect.corner1.transform.position,
        effect.corner2.transform.position,
        effect.corner3.transform.position,
        effect.corner4.transform.position,
    }

    local min = effect.corner1.transform.position
    local max = effect.corner1.transform.position
    for _, v in ipairs(posList) do
        min.x = math.min(min.x, v.x)
        min.y = math.min(min.y, v.y)
        min.z = math.min(min.z, v.z)

        max.x = math.max(max.x, v.x)
        max.y = math.max(max.y, v.y)
        max.z = math.max(max.z, v.z)
    end
    min = CameraManager.mainCamera:WorldToScreenPoint(min)
    max = CameraManager.mainCamera:WorldToScreenPoint(max)

    local size = max - min
    size.x = math.abs(size.x)
    size.y = math.abs(size.y)
    min.x = math.min(min.x, max.x)
    min.y = math.min(min.y, max.y)
    max = min + size
    local targetScreenRect = Unity.Rect(min.x, Screen.height - (min.y + size.y), size.x, size.y)

    Notify(MessageConst.SHOW_NAVI_TARGET_ACTION_MENU, {
        targetScreenRect = targetScreenRect,
        actions = actions,


        noMask = false,

    })
end

FacTopViewCtrl._AddControllerMenuActionsForTarget = HL.Method(HL.Table, HL.Table, HL.Boolean) << function(self, actions, target, isMulti)
    local _, facInteract = UIManager:IsOpen(PanelId.FacBuildingInteract)
    local fmt = Language.LUA_FAC_TOP_VIEW_CONTROLLER_MULTI_TARGET_MENU_FORMAT
    local function fmtText(text)
        return isMulti and string.format(fmt, target.name, text) or text
    end

    local PRIORITY_OTHER_OFFSET = 1000

    table.insert(actions, {
        objName = "Open_" .. target.type,
        text = fmtText(Language.LUA_FAC_TOP_VIEW_CONTROLLER_MENU_OPEN),
        priority = target.sortBase,
        action = function()
            if target.type == "building" then
                if target.isBuilding then
                    if target.subBuildingIndex and target.subBuildingIndex >= 0 then

                        facInteract:_OnInteractFactory({
                            buildingNodeId = target.nodeId,
                            subBuildingIndex = target.subBuildingIndex,
                        })
                    else
                        facInteract:_OnInteractFactory({ buildingNodeId = target.nodeId })
                    end
                else
                    facInteract:_OnInteractFactory({ nodeId = target.nodeId })
                end
            elseif target.type == "pipe" then
                facInteract:_OnInteractFactory({ nodeId = target.nodeId, unitIndex = target.unitIndex })
            elseif target.type == "belt" then
                facInteract:_OnInteractFactory({ nodeId = target.nodeId, unitIndex = target.unitIndex, logisticPos = target.logisticPos })
            end
            facInteract:_RemoveInteractOption()
        end,
    })

    if target.type == "building" then
        if target.isBuilding and FactoryUtils.canMoveBuilding(target.nodeId) then
            table.insert(actions, {
                objName = "Move_Building",
                text = fmtText(Language.LUA_FAC_TOP_VIEW_CONTROLLER_MENU_MOVE),
                priority = target.sortBase + PRIORITY_OTHER_OFFSET,
                action = function()
                    Notify(MessageConst.FAC_ENTER_BUILDING_MODE, { nodeId = target.nodeId })
                end,
            })
        end
        if FactoryUtils.canDelBuilding(target.nodeId) then
            table.insert(actions, {
                objName = "Del_Building",
                text = fmtText(Language.LUA_FAC_TOP_VIEW_CONTROLLER_MENU_DEL),
                priority = target.sortBase + PRIORITY_OTHER_OFFSET + 1,
                action = function()
                    FactoryUtils.delBuilding(target.nodeId)
                end,
            })
        end
    elseif target.type == "pipe" or target.type == "belt" then
        if FactoryUtils.canDelBuilding(target.nodeId) then
            table.insert(actions, {
                objName = "DelWhole_" .. target.type,
                text = fmtText(Language.LUA_FAC_TOP_VIEW_CONTROLLER_MENU_DEL_WHOLE),
                priority = target.sortBase + PRIORITY_OTHER_OFFSET,
                action = function()
                    GameInstance.player.remoteFactory.core:Message_OpDismantle(Utils.getCurrentChapterId(), target.nodeId)
                end,
            })
            table.insert(actions, {
                objName = "DelOneGrid_" .. target.type,
                text = fmtText(Language.LUA_FAC_TOP_VIEW_CONTROLLER_MENU_DEL_ONE_GRID),
                priority = target.sortBase + PRIORITY_OTHER_OFFSET + 1,
                action = function()
                    GameInstance.remoteFactoryManager:DismantleUnitFromConveyor(Utils.getCurrentChapterId(), target.nodeId, target.unitIndex)
                end,
            })
        end
    end
end

FacTopViewCtrl._ControllerMoveCurBuilding = HL.Method() << function(self)
    local _, facInteract = UIManager:IsOpen(PanelId.FacBuildingInteract)
    if not facInteract.m_interactFacNodeIdIsBuilding or not FactoryUtils.canMoveBuilding(facInteract.m_interactFacNodeId, true) then
        return
    end
    Notify(MessageConst.FAC_ENTER_BUILDING_MODE, { nodeId = facInteract.m_interactFacNodeId })
end

FacTopViewCtrl.ToggleHideFacTopViewRightSideUi = HL.Method(HL.Boolean) << function(self, isHide)
    self.view.rightSideNode.gameObject:SetActive(not isHide)
    if DeviceInfo.usingController then
        self.view.controllerMouse.gameObject:SetActive(not isHide)
        self.view.controllerMouseHoverHint.gameObject:SetActive(not isHide and self.m_lastMouseHintContent ~= nil)
    end
end






FacTopViewCtrl._IsMultiTargetMenuShowing = HL.Method().Return(HL.Boolean) << function(self)
    return self.view.multiTargetMenuNode.gameObject.activeSelf
end

FacTopViewCtrl._IsHoveringMultiTargetMenuGrid = HL.Method().Return(HL.Boolean) << function(self)
    if not self:_IsMultiTargetMenuShowing() or not self.m_multiTargetMenuGridPos then
        return false
    end
    local ctrl = LuaSystemManager.factory.interactPanelCtrl
    local detectPos = ctrl and ctrl.m_lastDetectWorldPos
    if not detectPos then
        return false
    end
    local gridPos = self.m_multiTargetMenuGridPos
    return math.floor(detectPos.x) == gridPos.x and math.floor(detectPos.z) == gridPos.z
end

FacTopViewCtrl._ClearHoverHintsForMultiTargetMenu = HL.Method() << function(self)
    self.m_lastMouseHintContent = nil
    self.view.mouseHoverHint.gameObject:SetActiveIfNecessary(false)
    self.view.controllerMouseHoverHint.gameObject:SetActiveIfNecessary(false)
end

FacTopViewCtrl._ShowMultiTargetMenu = HL.Method(HL.Table) << function(self, args)
    local targets = args.targets
    local screenRect = args.screenRect
    local ctrl = LuaSystemManager.factory.interactPanelCtrl

    self.m_multiTargetCells:Refresh(#targets, function(cell, index)
        local info = targets[index]
        cell.gameObject.name = "MultiTarget_" .. index
        cell.actionNode.onClick:RemoveAllListeners()
        cell.text.text = info.name
        cell.actionNode.onClick:AddListener(function()
            self:_HideMultiTargetMenu()
            info.action()
        end)
        if index == 1 then
            self:SetNaviTarget(cell.actionNode)
        end
    end)

    self.view.multiTargetMenuNode.gameObject:SetActive(true)
    local detectPos = ctrl and ctrl.m_lastDetectWorldPos
    self.m_multiTargetMenuGridPos = detectPos and { x = math.floor(detectPos.x), z = math.floor(detectPos.z) } or nil
    self:_ClearHoverHintsForMultiTargetMenu()
    local contentRect = self.view.multiTargetMenuNode.content.transform
    LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect)

    UIUtils.updateTipsPositionWithScreenRect(
        contentRect,
        screenRect,
        self.view.rectTransform,
        self.uiCamera,
        UIConst.UI_TIPS_POS_TYPE.RightTop,
        { bottom = 100 }
    )

    local firstEffectInfo = targets[1] and targets[1].effectInfo
    if firstEffectInfo and firstEffectInfo.worldPos then
        self:_SetMenuEffect(self.m_menuHoverEffect, firstEffectInfo.worldPos, 0)
    end
    for _, t in ipairs(targets) do
        if t.type == "pipe" and t.effectInfo then
            LuaSystemManager.factory:RequestPipeHighlight("menu", t.effectInfo.nodeId, t.effectInfo.unitIndex)
            break
        end
    end

    AudioAdapter.PostEvent("Au_UI_Menu_FacOverlay_Open")
end

FacTopViewCtrl._HideMultiTargetMenu = HL.Method() << function(self)
    if self.view.multiTargetMenuNode.gameObject.activeSelf then
        self.view.multiTargetMenuNode.gameObject:SetActive(false)
    end
    self.m_multiTargetMenuGridPos = nil
    if self.m_menuHoverEffect then
        self.m_menuHoverEffect.gameObject:SetActive(false)
    end
    LuaSystemManager.factory:ReleasePipeHighlight("menu")
end

FacTopViewCtrl._SetMenuEffect = HL.Method(HL.Table, CS.UnityEngine.Vector3, HL.Number) << function(self, effect, pos, offsetY)
    pos.y = pos.y + offsetY
    effect.transform.position = pos
    effect.transform.eulerAngles = Vector3.zero
    effect.transform.localScale = Vector3.one
    for k = 1, 4 do
        effect["corner" .. k].transform.localScale = Vector3.one
    end
    if not effect.gameObject.activeSelf then
        effect.gameObject:SetActive(true)
        for k = 1, 4 do
            effect["effect" .. k]:Update(0)
        end
    end
end




FacTopViewCtrl.RecoverStateOnChangeDevice = HL.Method(HL.Number, HL.Table) << function(self, selectedTypeIndex, selectedFilters)
    self.m_selectedFilters = selectedFilters
    self:_UpdateFilterCellStates()

    local tabCell
    if selectedTypeIndex == 1 then
        tabCell = self.view.customTypeCell
    else
        tabCell = self.m_typeCells:Get(selectedTypeIndex - 1)
    end
    tabCell.toggle:SetIsOnWithoutNotify(true)
    self:_OnClickType(selectedTypeIndex, true)
end


HL.Commit(FacTopViewCtrl)
