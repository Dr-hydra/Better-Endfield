local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacDestroyMode
local FAC_DESTROY_MODE_STATE_KEY = "FacDestroyModeCtrl"
FacDestroyModeCtrl = HL.Class('FacDestroyModeCtrl', uiCtrl.UICtrl)







FacDestroyModeCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.FAC_ON_DRAG_BEGIN_IN_BATCH_MODE] = 'OnDragBeginInBathMode',
    [MessageConst.FAC_ON_DRAG_END_IN_BATCH_MODE] = 'OnDragEndInBathMode',

    [MessageConst.FAC_ON_TOGGLE_BATCH_TARGET] = 'OnToggleBatchTarget',
}

FacDestroyModeCtrl.m_hideKey = HL.Field(HL.Number) << -1

FacDestroyModeCtrl.m_exitBindingId = HL.Field(HL.Number) << -1

FacDestroyModeCtrl.m_exitSecondaryBindingId = HL.Field(HL.Number) << -1


FacDestroyModeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.exitButton.onClick:AddListener(function()
        FacDestroyModeCtrl.ExitMode(false, true)
    end)

    self:_InitBatchNode()

    self.view.hidePipeToggle.toggle.onValueChanged:AddListener(function(isOn)
        self:_OnChangeHideToggle(isOn)
    end)
    self.view.reverseToggle.onValueChanged:AddListener(function(isOn)
        self:_OnChangeReverseToggle(isOn)
    end)

    self:BindInputPlayerAction("fac_disable_mouse1_sprint", function()

    end)

    self:_InitKeyHint()
end

FacDestroyModeCtrl.OnShow = HL.Override() << function(self)
    self.view.reverseToggle:SetIsOnWithoutNotify(false)
    self.view.reverseToggle.gameObject:SetActive(LuaSystemManager.factory.inTopView and DeviceInfo.usingTouch)
end

FacDestroyModeCtrl._ToggleExitBinding = HL.Method(HL.Boolean) << function(self, active)
    InputManagerInst:ToggleBinding(self.m_exitBindingId, active)
    if self.m_exitSecondaryBindingId > 0 then
        InputManagerInst:ToggleBinding(self.m_exitSecondaryBindingId, active)
    end
end

FacDestroyModeCtrl.OnClose = HL.Override() << function(self)
    if LuaSystemManager.factory.inDestroyMode then
        self:_RealExitMode()
    else
        self:_ClearOnExit()
    end
end

FacDestroyModeCtrl.ExtractHotSwitchRuntimeState = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if self.m_keyHintCells == nil then
        return nil
    end

    local state = {
        keyHintCells = self.m_keyHintCells,
    }
    self.m_keyHintCells = nil
    return state
end

FacDestroyModeCtrl.RestoreHotSwitchRuntimeState = HL.Override(HL.Opt(HL.Any)) << function(self, state)
    if not state then
        return
    end
    self.m_keyHintCells = state.keyHintCells
end

FacDestroyModeCtrl.EnterMode = HL.StaticMethod(HL.Opt(HL.Table)) << function(args)
    if Utils.isCurSquadAllDead() then
        return
    end


    if not GameInstance.player.systemActionConflictManager:TryStartSystemAction(Const.FacDestroySystemActionConflictName) then
        return
    end


    FacDestroyModeCtrl.s_radioTagHandle = GameInstance.player.globalTagsSystem:AddGlobalTag(CS.Beyond.Gameplay.GlobalTagDefine.notStopRadioTags)

    Notify(MessageConst.BEFORE_ENTER_DESTROY_MODE)
    PhaseManager:ExitPhaseFastTo(PhaseId.Level, true)

    args = args or {}

    local curChapter = GameInstance.remoteFactoryManager.currentChapterInfo
    local batchMoveMapping = GameInstance.remoteFactoryManager.interact.batchMoveConveyorMoveMapping
    local targets = args.batchSelectTargets
    if curChapter and batchMoveMapping and targets and type(targets) == "table" then
        local toRemoveKeys = {}
        local toReassignSelections = {}
        for k, v in pairs(targets) do
            local targetInst = curChapter:GetNodeIncludingPending(k)
            if not targetInst then
                toRemoveKeys[#toRemoveKeys + 1] = k
                local queryResult = batchMoveMapping:QueryLastBatchConveyorMoveMapping(k)
                if queryResult then
                    for i = 0, queryResult.Count - 1 do
                        local newId = queryResult[i].Item1
                        local newUnits = queryResult[i].Item2
                        if newUnits then
                            local newTargetUnits = {}
                            for j = 0, newUnits.Count - 1 do
                                newTargetUnits[newUnits[j]] = true
                            end
                            toReassignSelections[newId] = newTargetUnits
                        else
                            toReassignSelections[newId] = true
                        end
                    end
                end
            else
                local instBelongSlot = targetInst.belongSlot
                if instBelongSlot then
                    local pendingNodesInSlot = CSFactoryUtil.GetAllPendingNodeIdInSlot(instBelongSlot)
                    for i = 0, pendingNodesInSlot.Count - 1 do
                        local pendingNodeId = pendingNodesInSlot[i]
                        if not toReassignSelections[pendingNodeId] then
                            toReassignSelections[pendingNodeId] = true
                        end
                    end
                end
            end
        end
        for _, v in ipairs(toRemoveKeys) do
            targets[v] = nil
        end
        for k, v in pairs(toReassignSelections) do
            targets[k] = v
        end
    end

    local onClearScreenFinishAct = function(key)
        if GameInstance.player.systemActionConflictManager.curProcessingSystemAction ~= Const.FacDestroySystemActionConflictName then

            UIManager:RecoverScreen(key)
            Notify(MessageConst.BEFORE_EXIT_DESTROY_MODE)
            Notify(MessageConst.ON_FAC_DESTROY_MODE_CHANGE, false)

            if FacDestroyModeCtrl.s_radioTagHandle then
                FacDestroyModeCtrl.s_radioTagHandle:RemoveTag()
                FacDestroyModeCtrl.s_radioTagHandle = nil
            end
            return
        end

        local self = UIManager:AutoOpen(PANEL_ID)
        self.m_hideKey = key
        self:_OnEnterMode(args)
    end
    if args.fastEnter then
        local key = UIManager:ClearScreen(Const.RESERVE_PANEL_IDS_FOR_FAC_DESTROY_MODE)
        onClearScreenFinishAct(key)
    else
        UIManager:ClearScreenWithOutAnimation(onClearScreenFinishAct, Const.RESERVE_PANEL_IDS_FOR_FAC_DESTROY_MODE)
    end
end

FacDestroyModeCtrl.m_args = HL.Field(HL.Table)

FacDestroyModeCtrl.s_radioTagHandle = HL.StaticField(HL.Any)

FacDestroyModeCtrl._OnEnterMode = HL.Method(HL.Table) << function(self, args)
    LuaSystemManager.factory.inDestroyMode = true

    Notify(MessageConst.TOGGLE_IN_MAIN_HUD_STATE, { FAC_DESTROY_MODE_STATE_KEY, false })
    Notify(MessageConst.TOGGLE_FORBID_ATTACK, { FAC_DESTROY_MODE_STATE_KEY, true })

    self.m_updateKey = LuaUpdate:Add("Tick", function()
        self:_Update()
    end, true)

    self.m_args = args

    local inTopView = LuaSystemManager.factory.inTopView
    local showHidePipe = inTopView and FactoryUtils.canShowPipe()
    self.view.hidePipeToggle.gameObject:SetActive(showHidePipe)
    if args.isFromChangeInputDevice then
        local rMode = LuaSystemManager.factory:GetSimpleFigureModeFromRenderer()
        local isHideToggleOn = rMode == FacConst.SIMPLE_FIGURE_MODE.SimplePipeFigure
        self.view.hidePipeToggle.toggle:SetIsOnWithoutNotify(isHideToggleOn)
        LuaSystemManager.factory:SetSimpleFigureMode(rMode, true)
    else
        local fac = LuaSystemManager.factory
        local isHideToggleOn = fac.simpleFigureMode == FacConst.SIMPLE_FIGURE_MODE.SimplePipeFigure
        self.view.hidePipeToggle.toggle:SetIsOnWithoutNotify(isHideToggleOn)
        self:_OnChangeHideToggle(isHideToggleOn)
    end

    self.view.errorHint.gameObject:SetActive(false)
    self.view.warningHint.gameObject:SetActive(false)

    self.view.createBPHint.gameObject:SetActive(args.showCreateHint)

    self.view.batchNode.gameObject:SetActive(false)
    if inTopView then
        self.view.stateController:SetState("Batch")
        self.view.batchModeName.text = args.showCreateHint and Language.LUA_FAC_BATCH_MODE_TITLE_NAME_FROM_BP or Language.LUA_FAC_BATCH_MODE_TITLE_NAME
    else
        self.view.stateController:SetState("Destroy")
    end

    if self.m_exitBindingId > 0 then
        self.m_exitBindingId = self:DeleteInputBinding(self.m_exitBindingId)
    end
    local actId = inTopView and "fac_exit_batch_select_mode" or "fac_exit_dismantle_device"

    self.m_exitBindingId = self:BindInputPlayerAction(actId, function()
        FacDestroyModeCtrl.ExitMode(false, true)
    end)


    if self.m_exitSecondaryBindingId > 0 then
        self.m_exitSecondaryBindingId = self:DeleteInputBinding(self.m_exitSecondaryBindingId)
    end
    if DeviceInfo.usingKeyboard or DeviceInfo.usingTouch then

        self.m_exitSecondaryBindingId = self:BindInputPlayerAction("common_cancel", function()
            FacDestroyModeCtrl.ExitMode(false, true)
        end)
    end

    Notify(MessageConst.ON_FAC_DESTROY_MODE_CHANGE, true)

    if inTopView and args.batchSelectTargets then


        local _, facInteract = UIManager:IsOpen(PanelId.FacBuildingInteract)
        for nodeId, info in pairs(args.batchSelectTargets) do
            if info == true then
                facInteract:_SelectBatchTarget(nodeId, true)
            else
                for index, _ in pairs(info) do
                    facInteract:_SelectBatchTarget(nodeId, true, index)
                end
            end
        end
    end

    self:_UpdateKeyHintStates()

    if self.m_args.isFromChangeInputDevice and self.m_args.openSaveBP then
        self:_SaveBlueprint()
    end
end

FacDestroyModeCtrl.ExitModeForCS = HL.StaticMethod(HL.Opt(HL.Any)) << function(arg)
    FacDestroyModeCtrl.ExitMode(true)
end

FacDestroyModeCtrl.ExitMode = HL.StaticMethod(HL.Opt(HL.Boolean, HL.Boolean)) << function(skipAnim, fromBtn)
    if not LuaSystemManager.factory.inDestroyMode then

        GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.FacDestroySystemActionConflictName)
        return
    end

    local _, self = UIManager:IsOpen(PANEL_ID)

    Notify(MessageConst.BEFORE_EXIT_DESTROY_MODE)

    local needOpenBP = fromBtn and self.m_args.showCreateHint
    if not skipAnim then
        self:PlayAnimationOutWithCallback(function()
            self:_RealExitMode()
            if needOpenBP then
                PhaseManager:OpenPhaseFast(PhaseId.FacBlueprint)
            end
        end)
    else
        self:_RealExitMode()
        if needOpenBP then
            PhaseManager:OpenPhaseFast(PhaseId.FacBlueprint)
        end
    end
end

FacDestroyModeCtrl._RealExitMode = HL.Method() << function(self)
    self:Hide()
    self.m_keyHintCells:Refresh(0)
    self.m_keyHintName = ""

    if not InputManagerInst.inChangingInputDevice then
        if LuaSystemManager.factory.inTopView then
            LuaSystemManager.factory:ApplySimpleFigureToRenderer()
        else
            LuaSystemManager.factory:ClearSimpleFigureEcsOnly()
        end
    end


    LuaSystemManager.factory.inDestroyMode = false
    self:_ClearOnExit()

    Notify(MessageConst.TOGGLE_IN_MAIN_HUD_STATE, { FAC_DESTROY_MODE_STATE_KEY, true })
    if FacDestroyModeCtrl.s_radioTagHandle then

        FacDestroyModeCtrl.s_radioTagHandle:RemoveTag()
        FacDestroyModeCtrl.s_radioTagHandle = nil
    end

    Notify(MessageConst.ON_FAC_DESTROY_MODE_CHANGE, false)
    Notify(MessageConst.TOGGLE_FORBID_ATTACK, { FAC_DESTROY_MODE_STATE_KEY, false })
end

FacDestroyModeCtrl._ClearOnExit = HL.Method() << function(self)
    self.m_hideKey = UIManager:RecoverScreen(self.m_hideKey)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
    GameInstance.remoteFactoryManager.visual:HideRangeGrid()

    GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.FacDestroySystemActionConflictName)
end

FacDestroyModeCtrl._OnChangeHideToggle = HL.Method(HL.Boolean) << function(self, isOn)
    local mode = isOn and FacConst.SIMPLE_FIGURE_MODE.SimplePipeFigure or FacConst.SIMPLE_FIGURE_MODE.None
    LuaSystemManager.factory:SetSimpleFigureMode(mode)
end

FacDestroyModeCtrl._OnChangeReverseToggle = HL.Method(HL.Boolean) << function(self, isOn)
    LuaSystemManager.factory:ChangeIsReverseSelect(isOn)
end

FacDestroyModeCtrl.OnDragBeginInBathMode = HL.Method() << function(self)
    self.view.keyHintNode.gameObject:SetActive(false)
    self.view.batchNode.transform.localScale = Vector3.zero
    self:_ToggleExitBinding(false)
end

FacDestroyModeCtrl.OnDragEndInBathMode = HL.Method() << function(self)
    self:_ToggleExitBinding(true)
    self.view.batchNode.transform.localScale = Vector3.one
    self.view.keyHintNode.gameObject:SetActive(true)
end

FacDestroyModeCtrl._ConfirmBatchDel = HL.Method() << function(self)
    if GameInstance.remoteFactoryManager.batchSelect.hasHub then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BATCH_ACTION_WARNING_HUB)
        return
    end
    local targets = LuaSystemManager.factory.batchSelectTargets
    if not next(targets) then
        return
    end
    local nodeList = {}
    local Dictionary_UInt_ListInt = CS.System.Collections.Generic.Dictionary(CS.System.UInt32, CS.System.Collections.Generic["List`1[System.Int32]"])
    local beltInfos = Dictionary_UInt_ListInt()
    local pendingSlotIdMap = {}
    local count = 0
    for id, info in pairs(targets) do
        if info == true then
            local slotId = FactoryUtils.getPendingBuildingNodeSlotId(id)
            if slotId then
                pendingSlotIdMap[slotId] = true
            else
                table.insert(nodeList, id)
            end
        else
            local list = {}
            for k, _ in pairs(info) do
                table.insert(list, k)
            end
            beltInfos[id] = list
        end
        count = count + 1
    end

    local pendingSlotIdList = {}
    for k, _ in pairs(pendingSlotIdMap) do
        table.insert(pendingSlotIdList, k)
    end
    if #pendingSlotIdList > 0 then
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_FAC_BLUEPRINT_CONFIRM_CANCEL_PENDING,
            warningContent = Language.LUA_FAC_BLUEPRINT_CONFIRM_CANCEL_PENDING_HINT,
            onConfirm = function()
                GameInstance.player.remoteFactory.core:Message_OpDismantleBatch(Utils.getCurrentChapterId(), nodeList, beltInfos, pendingSlotIdList)
            end
        })
        return
    end

    if count >= FacConst.BATCH_DEL_HINT_COUNT then
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_FAC_CONFIRM_BATCH_DEL_A_LOT,
            onConfirm = function()
                GameInstance.player.remoteFactory.core:Message_OpDismantleBatch(Utils.getCurrentChapterId(), nodeList, beltInfos, pendingSlotIdList)
            end
        })
    else
        GameInstance.player.remoteFactory.core:Message_OpDismantleBatch(Utils.getCurrentChapterId(), nodeList, beltInfos, pendingSlotIdList)
    end
end

FacDestroyModeCtrl.m_updateKey = HL.Field(HL.Number) << -1

FacDestroyModeCtrl._Update = HL.Method() << function(self)
    self:_UpdateKeyHintStates()
end



local KeyHints = {
    normal = {
        "fac_exit_dismantle_device",
    },
    batch_empty = {
        "fac_batch_select",
        "fac_batch_drag_select",
        "fac_batch_drag_unselect",
        "fac_exit_batch_select_mode",
    },
    batch_not_empty = {
        "fac_batch_select",
        "fac_batch_drag_select",
        "fac_batch_drag_unselect",
        "fac_exit_batch_select_mode",
    },
}

local ControllerKeyHints = {
    normal = {
        "fac_exit_dismantle_device",
    },
    batch_empty = {
        "fac_top_view_ct_move",
        "fac_top_view_ct_move_cam",
        "fac_top_view_ct_scale_cam",

        "fac_top_view_batch_drag_ct",
        "fac_top_view_batch_drag_reverse_ct",
        "fac_top_view_hide_pipe",
        "fac_exit_batch_select_mode",
    },
    batch_not_empty = {
        "fac_top_view_ct_move",
        "fac_top_view_ct_move_cam",
        "fac_top_view_ct_scale_cam",

        "fac_top_view_batch_drag_ct",
        "fac_top_view_batch_drag_reverse_ct",
        "fac_top_view_hide_pipe",
        "fac_exit_batch_select_mode",
    },
}

FacDestroyModeCtrl.m_keyHintCells = HL.Field(HL.Forward('UIListCache'))

FacDestroyModeCtrl.m_keyHintName = HL.Field(HL.String) << ''

FacDestroyModeCtrl._InitKeyHint = HL.Method() << function(self)
    self.m_keyHintCells = self.m_keyHintCells or UIUtils.genCellCache(self.view.keyHintCell)
end

FacDestroyModeCtrl._RefreshKeyHint = HL.Method(HL.Opt(HL.String)) << function(self, name)
    self.m_keyHintName = name
    local keyHint
    if DeviceInfo.usingKeyboard then
        keyHint = KeyHints[name]
    elseif DeviceInfo.usingController then
        keyHint = ControllerKeyHints[name]
    end
    if not keyHint then
        self.m_keyHintCells:Refresh(0)
        return
    end
    local count = #keyHint
    self.m_keyHintCells:Refresh(count, function(cell, index)
        local actionId = keyHint[index]
        cell.actionKeyHint:SetActionId(actionId)
        cell.gameObject.name = "KeyHint-" .. actionId


        if DeviceInfo.usingKeyboard and actionId == "fac_exit_dismantle_device" then
            cell.actionKeyHint:SetSecondActionId("common_cancel", true)
        end
    end)
end

FacDestroyModeCtrl.m_needUpdateActionInteract = HL.Field(HL.Boolean) << false

FacDestroyModeCtrl.OnToggleBatchTarget = HL.Method() << function(self)
    self.m_needUpdateActionInteract = true
end

FacDestroyModeCtrl._UpdateKeyHintStates = HL.Method() << function(self)
    local name = "normal"
    local inTopView = LuaSystemManager.factory.inTopView
    if inTopView then
        local hasTarget = false
        if next(LuaSystemManager.factory.batchSelectTargets) then
            hasTarget = true
            name = "batch_not_empty"
        else
            name = "batch_empty"
        end

        self.view.batchNode.gameObject:SetActive(hasTarget)
        if hasTarget then
            self.view.createBPHint.gameObject:SetActive(false)
        end
        if hasTarget then
            if not GameInstance.remoteFactoryManager.batchSelect.needUpdateRange then
                local range = GameInstance.remoteFactoryManager.batchSelect.selectedRange
                local needReverse = lume.round(LuaSystemManager.factory.topViewCamTarget.eulerAngles.y) % 180 ~= 0
                local width = lume.round(needReverse and range.height or range.width)
                local height = lume.round(needReverse and range.width or range.height)
                local isWidthOverSize = width > Tables.facBlueprintConst.BluePrintXLenMax
                local isHeightOverSize = height > Tables.facBlueprintConst.BluePrintZLenMax
                width = string.format(isWidthOverSize and Language.LUA_FAC_BLUEPRINT_RANGE_OVER_MAX or "%d", width)
                height = string.format(isHeightOverSize and Language.LUA_FAC_BLUEPRINT_RANGE_OVER_MAX or "%d", height)
                local newStr = string.format(Language.LUA_FAC_BLUEPRINT_RANGE_FORMAT, width, height)
                if self.view.sizeTxt.text ~= newStr then
                    self.view.sizeTxt.text = newStr
                end
            end
        else
            local newStr = string.format(Language.LUA_FAC_BLUEPRINT_RANGE_FORMAT, "0", "0")
            if self.view.sizeTxt.text ~= newStr then
                self.view.sizeTxt.text = newStr
            end
        end
    end

    if name ~= self.m_keyHintName then
        self:_RefreshKeyHint(name)
        self.m_needUpdateActionInteract = true
    end
    if inTopView and self.m_needUpdateActionInteract and not GameInstance.remoteFactoryManager.batchSelect.needUpdateRange then
        self:_UpdateBatchActionInteractable()
        self.m_needUpdateActionInteract = false
    end
end



FacDestroyModeCtrl._OnClickDel = HL.Method(HL.Boolean) << function(self, isAll)
    if LuaSystemManager.factory.inBatchSelectMode then
        Notify(MessageConst.FAC_STOP_DRAG_IN_BATCH_MODE)
        self:_ConfirmBatchDel()
    else
        local _, interactCtrl = UIManager:IsOpen(PanelId.FacBuildingInteract)
        interactCtrl:_OnClickFakeInteractOption(isAll)
    end
end





FacDestroyModeCtrl._InitBatchNode = HL.Method() << function(self)
    local node = self.view.batchNode

    if Utils.isInBlackbox() then
        node.saveBtn.gameObject:SetActive(false)
    else
        node.saveBtn.onClick:AddListener(function()
            self:_SaveBlueprint()
        end)
    end
    node.moveBtn.onClick:AddListener(function()
        self:TryStartBatchMove()
    end)
    node.copyBtn.onClick:AddListener(function()
        if self:CheckBatchActionValid() then
            self:_EnterBPMode(false)
        end
    end)
    node.delBtn.onClick:AddListener(function()
        self:_OnClickDel(false)
    end)
    node.togglePowerBtn.onClick:AddListener(function()
        self:_OnClickTogglePower()
    end)
end

FacDestroyModeCtrl.TryStartBatchMove = HL.Method() << function(self)
    if not self.view.inputGroup.groupEnabled then
        return
    end
    if self:CheckBatchActionValid(true) then
        self:_EnterBPMode(true)
    end
end


FacDestroyModeCtrl._SaveBlueprint = HL.Method() << function(self)
    if not self:CheckBatchActionValid() then
        return
    end

    local maxCount = Tables.facBlueprintConst.BlueprintNodeCountLimit
    if LuaSystemManager.factory:GetCurBatchSelectTargetCount() > maxCount then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BATCH_MODE_ERROR_TOO_MUCH_NODE)
        return
    end

    Notify(MessageConst.FAC_STOP_DRAG_IN_BATCH_MODE)

    UIManager:Open(PanelId.FacSaveBlueprint)
end

FacDestroyModeCtrl.CheckBatchActionValid = HL.Method(HL.Opt(HL.Boolean, HL.Boolean)).Return(HL.Boolean) << function(self, isMoveAct, noToast)
    local rst, toast = self:_CheckBatchActionValid(isMoveAct)
    if toast and not noToast then
        Notify(MessageConst.SHOW_TOAST, toast)
    end
    return rst
end

FacDestroyModeCtrl._CheckBatchActionValid = HL.Method(HL.Opt(HL.Boolean)).Return(HL.Boolean, HL.Opt(HL.String)) << function(self, isMoveAct)
    local range = GameInstance.remoteFactoryManager.batchSelect.selectedRange
    if range.width > Tables.facBlueprintConst.BluePrintXLenMax or range.height > Tables.facBlueprintConst.BluePrintZLenMax then
        return false, Language.LUA_FAC_BLUEPRINT_RANGE_OVER_MAX_HINT
    end
    if not isMoveAct then
        if GameInstance.remoteFactoryManager.batchSelect.hasHub then
            return false, Language.LUA_FAC_BATCH_ACTION_WARNING_HUB
        end
        if GameInstance.remoteFactoryManager.batchSelect.hasDecoTargets then
            return false, Language.LUA_FAC_BATCH_ACTION_WARNING_DECO
        end
        if GameInstance.remoteFactoryManager.batchSelect.hasPendingTargets then
            return false, Language.LUA_FAC_BATCH_ACTION_WARNING_PENDING
        end
    end
    if GameInstance.remoteFactoryManager.batchSelect.hasSocialTargets then
        return false, Language.LUA_FAC_BLUEPRINT_HAS_SOCIAL_HINT
    end
    if not GameInstance.remoteFactoryManager.batchSelect.allTargetsInSamePosY then
        return false, Language.LUA_FAC_BATCH_MODE_ERROR_NOT_IN_SAME_HEIGHT
    end
    if GameInstance.remoteFactoryManager.batchSelect.autoAdjustPipeAndFluidValveFailed then
        return false, Language.LUA_BLUEPRINT_FAILED_TO_ADJUST_PIPE_SLOPE
    end
    if isMoveAct and GameInstance.remoteFactoryManager.batchSelect.hasAdjustedPipe then
        return false, Language.LUA_BLUEPRINT_CANNOT_BATCH_MOVE_SINCE_ADJUST_PIPE_SLOPE_ERROR
    end
    return true
end

FacDestroyModeCtrl._UpdateBatchActionInteractable = HL.Method() << function(self)
    local batch = GameInstance.remoteFactoryManager.batchSelect
    local noPendingNode = not batch.hasPendingTargets
    local allNodesInSameHeight = batch.allTargetsInSamePosY
    local hasAdjustedPipe = batch.hasAdjustedPipe
    local hasDecoTargets = batch.hasDecoTargets
    local hasHub = batch.hasHub
    local autoAdjustPipeAndFluidValveFailed = batch.autoAdjustPipeAndFluidValveFailed
    local range = batch.selectedRange
    local notOverSize = range.width <= Tables.facBlueprintConst.BluePrintXLenMax and range.height <= Tables.facBlueprintConst.BluePrintZLenMax
    local bpValid = noPendingNode and allNodesInSameHeight and notOverSize and not batch.hasSocialTargets and not hasDecoTargets and not hasHub

    self.view.batchNode.saveBtnStateController:SetState(bpValid and "Valid" or "NotValid")
    self.view.batchNode.copyBtnStateController:SetState(bpValid and "Valid" or "NotValid")
    self.view.batchNode.moveBtnStateController:SetState((allNodesInSameHeight and not batch.hasSocialTargets and not hasAdjustedPipe and notOverSize) and "Valid" or "NotValid")
    self.view.batchNode.delBtnStateController:SetState(hasHub and "NotValid" or "Valid")

    local hasError, warningHintTxt
    if not allNodesInSameHeight then
        self.view.errorHintText.text = Language.LUA_FAC_BATCH_MODE_ERROR_NOT_IN_SAME_HEIGHT
        hasError = true
    elseif autoAdjustPipeAndFluidValveFailed then
        self.view.errorHintText.text = Language.LUA_BLUEPRINT_FAILED_TO_ADJUST_PIPE_SLOPE
        hasError = true
    elseif not notOverSize then
        self.view.errorHintText.text = Language.LUA_FAC_BATCH_MODE_ERROR_OVER_SIZE
        hasError = true
    elseif batch.hasSocialTargets then
        self.view.errorHintText.text = Language.LUA_FAC_BLUEPRINT_HAS_SOCIAL_HINT
        hasError = true
    elseif hasHub then
        warningHintTxt = Language.LUA_FAC_BATCH_ACTION_WARNING_HUB
    elseif hasDecoTargets then
        warningHintTxt = Language.LUA_FAC_BATCH_ACTION_WARNING_DECO
    elseif not noPendingNode then
        warningHintTxt = Language.LUA_FAC_BATCH_ACTION_WARNING_PENDING
    end
    self.view.errorHint.gameObject:SetActive(hasError == true)
    self.view.warningHint.gameObject:SetActive(warningHintTxt ~= nil)
    if warningHintTxt then
        self.view.warningHintTxt.text = warningHintTxt
    end
end

FacDestroyModeCtrl._EnterBPMode = HL.Method(HL.Boolean) << function(self, isMove)
    local range = GameInstance.remoteFactoryManager.batchSelect.selectedRange

    if not isMove then
        if range.width > Tables.facBlueprintConst.BluePrintXLenMax or range.height > Tables.facBlueprintConst.BluePrintZLenMax then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BLUEPRINT_RANGE_OVER_MAX_HINT)
            return
        end
        local deviceMap = {}
        for nodeId, v in pairs(LuaSystemManager.factory.batchSelectTargets) do
            if v == true then
                local node = FactoryUtils.getBuildingNodeHandler(nodeId)
                local buildingId = node.templateId
                if Tables.factoryBuildingTable:ContainsKey(buildingId) then
                    if deviceMap[buildingId] then
                        deviceMap[buildingId] = deviceMap[buildingId] + 1
                    else
                        deviceMap[buildingId] = 1
                    end
                end
            end
        end
    end

    local bp = LuaSystemManager.factory:GetBlueprintFromBatchSelectTargets()
    local targets = LuaSystemManager.factory.batchSelectTargets
    local showCreateHint = self.m_args.showCreateHint
    FacDestroyModeCtrl.ExitMode(true, false)
    local initGridPos
    if DeviceInfo.usingTouch then

        local posY = GameUtil.playerPos.y
        local lbPos = CameraManager.mainCamera:WorldToScreenPoint(Vector3(range.xMin, posY, range.yMin)):XY()
        local rtPos = CameraManager.mainCamera:WorldToScreenPoint(Vector3(range.xMax, posY, range.yMax)):XY()
        local padding = 150
        local rect = Unity.Rect(padding, 0, Screen.width - padding * 2, Screen.height)
        if rect:Contains(lbPos) and rect:Contains(rtPos) then
            initGridPos = Vector3(range.center.x, posY, range.center.y)
        end
    end
    Notify(MessageConst.FAC_ENTER_BLUEPRINT_MODE, {
        fastEnter = true,
        blueprint = bp,
        isMove = isMove,
        batchSelectTargets = targets,
        range = range,
        showCreateHint = showCreateHint,
        initGridPos = initGridPos,
    })
end



FacDestroyModeCtrl._OnClickTogglePower = HL.Method() << function(self)
    local targets = LuaSystemManager.factory.batchSelectTargets
    local count = 0
    local nodeIds = {}
    for id, _ in pairs(targets) do
        if not FactoryUtils.isPendingBuildingNode(id) then
            local node = FactoryUtils.getBuildingNodeHandler(id)
            local buildingId = node.templateId
            local succ, bData = Tables.factoryBuildingTable:TryGetValue(buildingId)
            if succ and bData.canBatchModeTogglePower then
                table.insert(nodeIds, id)
                count = count + 1
            end
        end
    end
    if count == 0 then
        return
    end
    if count > Tables.facBlueprintConst.BlueprintNodeCountLimit then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BATCH_MODE_TOGGLE_POWER_OVER_MAX_COUNT)
        return
    end
    GameInstance.player.remoteFactory.core:Message_OpEnableNodes(Utils.getCurrentChapterId(), nodeIds, function(op, opRet)
        if opRet.RetCode == CS.Proto.FACTORY_OP_RET_CODE.Ok then
            if opRet.EnableNode.ToEnable then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BATCH_MODE_TOGGLE_POWER_ON)
                AudioAdapter.PostEvent("Au_UI_Toggle_FacPower_Switch_On")
            else
                Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BATCH_MODE_TOGGLE_POWER_OFF)
                AudioAdapter.PostEvent("Au_UI_Toggle_FacPower_Switch_Off")
            end
        end
    end)
end

HL.Commit(FacDestroyModeCtrl)
