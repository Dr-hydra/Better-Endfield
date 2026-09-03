local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.Dialog
PhaseDialog = HL.Class('PhaseDialog', phaseBase.PhaseBase)

local clearPhases = {
    PhaseId.CharInfo,
    PhaseId.CharFormation,
}





PhaseDialog.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_DIALOG_START] = { 'OnDirectDialogStart', false },
    [MessageConst.IS_DIALOG_PHASE_OPENED] = { 'IsDialogPhaseOpened', false },

    [MessageConst.ON_EXIT_DIALOG] = { 'OnExitDialog', true },
    [MessageConst.ON_PLAY_DIALOG_TRUNK] = { 'OnPlayDialogTrunk', true },
    [MessageConst.ON_SHOW_DIALOG_OPTION] = { 'OnShowDialogOption', true },
    [MessageConst.DIALOG_PANEL_SHOW_FULL_BG] = { 'OnShowDialogFullBg', true },
    [MessageConst.DIALOG_PANEL_SHOW_POST_PROCESS_EFFECT] = {"OnShowPostProcessEffect", true},
    [MessageConst.DIALOG_PANEL_SHOW_LEFT_SUBTITLE] = { 'OnShowDialogLeftSubtitle', true },
    [MessageConst.DIALOG_PANEL_EXIT_LEFT_SUBTITLE] = { 'OnExitDialogLeftSubtitle', true },
    [MessageConst.ON_DIALOG_ENV_TALK_CHANGED] = { 'OnDialogEnvTalkChanged', true },
    [MessageConst.ON_COMMON_BACK_CLICKED] = { 'OnCommonBackClicked', true },
    [MessageConst.DIALOG_OPEN_UI] = { "OpenUI", true },
    [MessageConst.ON_DIALOG_PHASE_BACK_TO_TOP] = { "OnDialogPhaseTransitionBackToTop", true },

    [MessageConst.DIALOG_CHANGE_NEXT_INDEX] = { "ChangeNextIndex", true },
    [MessageConst.DIALOG_SEND_PRESENT_END] = { "OnSendPresentEnd", true },

    [MessageConst.OPEN_DIALOG_RECORD] = { '_OpenDialogRecord', true },
    [MessageConst.HIDE_DIALOG_RECORD] = { '_HideDialogRecord', true },

    [MessageConst.OPEN_DIALOG_SKIP_POP_UP] = { '_OpenDialogSkipPopUp', true },
    [MessageConst.HIDE_DIALOG_SKIP_POP_UP] = { '_HideDialogSkipPopUp', true },
    [MessageConst.SKIP_DIALOG] = { '_SkipDialog', true },
}

PhaseDialog.m_panelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseDialog.m_targetGroup = HL.Field(HL.Forward("PhaseGameObjectItem"))

PhaseDialog.m_inited = HL.Field(HL.Boolean) << false

PhaseDialog.doingOut = HL.Field(HL.Boolean) << false

PhaseDialog.m_onRightMouseButtonPress = HL.Field(HL.Function)

PhaseDialog.m_onDrag = HL.Field(HL.Function)

PhaseDialog.m_hasListened = HL.Field(HL.Boolean) << false

PhaseDialog.s_nextDialog = HL.StaticField(HL.String) << ""

PhaseDialog.s_nextIndexConfig = HL.StaticField(HL.Table)

PhaseDialog.m_tempNextIndex = HL.Field(HL.Number) << -1

PhaseDialog.m_openedPhaseId = HL.Field(HL.Number) << -1

PhaseDialog.m_pendingExitDialogArg = HL.Field(HL.Any)

PhaseDialog.m_hasPendingExitDialog = HL.Field(HL.Boolean) << false

PhaseDialog.m_deferredExitCoroutine = HL.Field(HL.Thread)

PhaseDialog.m_needStartNextDialogAfterDeferredExit = HL.Field(HL.Boolean) << false


PhaseDialog._OnInit = HL.Override() << function(self)
    PhaseDialog.Super._OnInit(self)
    UIManager:ToggleBlockObtainWaysJump("IN_CINEMATIC", true)
end

PhaseDialog._InitNextIndex = HL.StaticMethod() << function()
    PhaseDialog.s_nextIndexConfig = {}
    for phaseName, nextIndex in pairs(DialogConst.DIALOG_PHASE_2_NEXT_INDEX) do
        PhaseDialog.s_nextIndexConfig[PhaseId[phaseName]] = nextIndex
    end
end

PhaseDialog.GetOpenedPhaseId = HL.Method().Return(HL.Number) << function(self)
    return self.m_openedPhaseId
end

PhaseDialog.ChangeNextIndex = HL.Method(HL.Table) << function(self, args)
    if args.phaseId == self.m_openedPhaseId then
        self.m_tempNextIndex = args.nextIndex
    end
end

PhaseDialog.GetNextIndex = HL.StaticMethod(HL.Number).Return(HL.Number) << function(phaseId)
    local suc, phaseDialog = PhaseManager:IsOpen(PHASE_ID)
    if not suc then
        return 0
    end
    if phaseDialog.m_tempNextIndex >= 0 then
        return phaseDialog.m_tempNextIndex
    end
    if not PhaseDialog.s_nextIndexConfig then
        PhaseDialog._InitNextIndex()
    end
    return PhaseDialog.s_nextIndexConfig[phaseId] or 0
end

PhaseDialog.ClearPhasesWithCam = HL.StaticMethod(HL.Opt(HL.Any)) << function(_)
    for _, phaseId in pairs(clearPhases) do
        local isOpen, phase = PhaseManager:IsOpen(phaseId)
        if isOpen then
            PhaseManager:ExitPhaseFast(phaseId)
        end
    end
end

PhaseDialog.OnDialogStart = HL.StaticMethod(HL.Table) << function(arg)
    arg.fast = true
    local isOpen, _ = PhaseManager:IsOpen(PhaseId.Dialog)
    if not isOpen then
        PhaseDialog.AutoOpen(PHASE_ID, arg)
    else
        if phase.doingOut then
            local nextDialog = GameWorld.dialogManager.dialogId
            logger.info("Dialog already open: " .. nextDialog)
            PhaseDialog.s_nextDialog = nextDialog
        end
    end
end

PhaseDialog.OnDirectDialogStart = HL.StaticMethod(HL.Opt(HL.Table)) << function(data)
    local arg = {
        direct = true,
        fast = true,
    }

    local inWaitingQueue =  PhaseManager:IsInWaitingQueue(PhaseId.Dialog)
    if inWaitingQueue then
        return
    end

    local isOpen, phase = PhaseManager:IsOpen(PhaseId.Dialog)
    if not isOpen then
        local res = PhaseDialog.AutoOpen(PHASE_ID, arg)
        if not res then
            logger.warn("PhaseDialog Open Failed!!!!")
        end
    else
        if phase.doingOut then
            local nextDialog = GameWorld.dialogManager.dialogId
            logger.info("Dialog already open: " .. nextDialog)
            PhaseDialog.s_nextDialog = nextDialog
        end
    end
end

PhaseDialog.IsDialogPhaseOpened = HL.StaticMethod(HL.Table) << function(arg)
    local isOpenedContext = unpack(arg)
    local isOpen, phase = PhaseManager:IsOpenAndValid(PHASE_ID)
    if isOpen and not phase.doingOut then
        isOpenedContext.isPhaseOpen = true
    else
        isOpenedContext.isPhaseOpen = false
    end
end

PhaseDialog.OnShowDialogFullBg = HL.Method(HL.Table) << function(self, data)
    local actionData = unpack(data)
    self:_DoShowFullBg(actionData)
end

PhaseDialog.OnShowPostProcessEffect = HL.Method(HL.Table) << function(self, data)
    local actionData = unpack(data)
    self.m_panelItem.uiCtrl:SetPostProcessEffect(actionData)
    self.m_panelItem.uiCtrl:Show()
    self.m_inited = true
end

PhaseDialog.OnShowDialogLeftSubtitle = HL.Method(HL.Table) << function(self, data)
    local actionData = unpack(data)
    self:_DoShowLeftSubtitle(actionData)
end

PhaseDialog.OnExitDialogLeftSubtitle = HL.Method() << function(self)
    self:_DoExitLeftSubtitle()
end

PhaseDialog.OnDialogEnvTalkChanged = HL.Method(HL.Table) << function(self, arg)
    self:_GetPanelPhaseItem(PanelId.HeadLabelInDialog).uiCtrl:RefreshEnvTalk(arg)
end


PhaseDialog._InitAllPhaseItems = HL.Override() << function(self)
    PhaseDialog.Super._InitAllPhaseItems(self)
    self.m_panelItem = self:_GetPanelPhaseItem(PanelId.Dialog)
end

PhaseDialog.OnCommonBackClicked = HL.Method() << function(self)
end

PhaseDialog.OnExitDialog = HL.Method(HL.Opt(HL.Any)) << function(self, arg)


    if self.state == PhaseConst.EPhaseState.TransitionBackToTop then
        self.m_pendingExitDialogArg = arg
        self.m_hasPendingExitDialog = true
        return
    end

    local fast = false
    if arg then
        fast = unpack(arg)
    end


    UIManager:Hide(PanelId.CommonPopUp)

    if not fast then
        self.doingOut = true
        self.m_panelItem.uiCtrl:PlayAnimationOutWithCallback(function()
            self:_ExitSelfFastSafely(true)
        end)
    else
        self:_ExitSelfFastSafely(false)
    end
end

PhaseDialog._CanExitSelfFastImmediately = HL.Method().Return(HL.Boolean) << function(self)
    return PhaseManager.m_curState == Const.PhaseState.Idle and not PhaseManager:CheckIsInTransition()
end

PhaseDialog._ExitSelfFastSafely = HL.Method(HL.Boolean) << function(self, startNextDialog)
    if not PhaseManager:IsOpenAndValid(PHASE_ID) then
        if startNextDialog then
            PhaseDialog._TryStartNextDialog()
        end
        return
    end


    if not self:_CanExitSelfFastImmediately() then
        self.doingOut = true
        self.m_needStartNextDialogAfterDeferredExit = self.m_needStartNextDialogAfterDeferredExit or startNextDialog
        self:_StartDeferredExitCoroutine()
        return
    end

    self:ExitSelfFast()
    if startNextDialog then
        PhaseDialog._TryStartNextDialog()
    end
end

PhaseDialog._StartDeferredExitCoroutine = HL.Method() << function(self)
    if self.m_deferredExitCoroutine then
        return
    end

    self.m_deferredExitCoroutine = self:_StartCoroutine(function()
        coroutine.waitCondition(function()
            return self.m_destroyed
                or not PhaseManager:IsOpenAndValid(PHASE_ID)
                or self:_CanExitSelfFastImmediately()
        end)

        self.m_deferredExitCoroutine = nil
        local startNextDialog = self.m_needStartNextDialogAfterDeferredExit
        self.m_needStartNextDialogAfterDeferredExit = false

        if self.m_destroyed or not PhaseManager:IsOpenAndValid(PHASE_ID) then
            if startNextDialog or not string.isEmpty(PhaseDialog.s_nextDialog) then
                PhaseDialog._TryStartNextDialog()
            end
            return
        end

        self:ExitSelfFast()
        if startNextDialog or not string.isEmpty(PhaseDialog.s_nextDialog) then
            PhaseDialog._TryStartNextDialog()
        end
    end)
end

PhaseDialog._TryStartNextDialog = HL.StaticMethod() << function()
    if not string.isEmpty(PhaseDialog.s_nextDialog) then
        PhaseDialog.s_nextDialog = ""
        PhaseDialog.OnDirectDialogStart()
    end
end


PhaseDialog.OnPlayDialogTrunk = HL.Method(HL.Table) << function(self, data)
    local trunkNodeData, fastMode, npcId, npcGroupId = unpack(data)
    self:_DoPlayDialogTrunk(trunkNodeData, fastMode, npcId, npcGroupId)
end

PhaseDialog.OnShowDialogOption = HL.Method(HL.Table) << function(self, data)
    local options = unpack(data)
    self:_DoShowDialogOption(options)
end

PhaseDialog._DoPlayDialogTrunk = HL.Method(CS.Beyond.Gameplay.DTTrunkNodeData, HL.Opt(HL.Boolean, HL.Any, HL.Any)) <<
    function
    (self, trunkNodeData, fastMode, npcId, npcGroupId)
        self.m_panelItem.uiCtrl:Show()
        self.m_panelItem.uiCtrl:SetTrunk(trunkNodeData, fastMode, npcId, npcGroupId)
        self.m_inited = true
    end

PhaseDialog._DoShowDialogOption = HL.Method(HL.Userdata) << function
(self, options)
    self.m_panelItem.uiCtrl:SetTrunkOption(options)
    self.m_inited = true
end

PhaseDialog._DoShowFullBg = HL.Method(CS.Beyond.Gameplay.DialogFullBgActionData) << function(self, actionData)
    self.m_panelItem.uiCtrl:SetFullBg(actionData)
    self.m_panelItem.uiCtrl:Show()
    self.m_inited = true
end

PhaseDialog._DoShowLeftSubtitle = HL.Method(CS.Beyond.Gameplay.DialogLeftSubtitleActionData) << function(self, actionData)
    self.m_panelItem.uiCtrl:Show()
    self.m_panelItem.uiCtrl:SetLeftSubtitle(actionData)
    self.m_inited = true
end

PhaseDialog._DoExitLeftSubtitle = HL.Method() << function(self)
    self.m_panelItem.uiCtrl:ExitLeftSubtitle()
end

PhaseDialog._AddRegisters = HL.Method() << function(self)
    local touchPanel = self.m_panelItem.uiCtrl:GetTouchPanel()
    if not touchPanel then
        return
    end

    if not self.m_onDrag then
        self.m_onDrag = function(eventData)
            self:_MoveCamera(eventData.delta)
        end
    end

    if not self.m_hasListened then
        touchPanel.onDrag:AddListener(self.m_onDrag)

        if BEYOND_DEBUG then
            if not self.m_onRightMouseButtonPress then
                self.m_onRightMouseButtonPress = function(delta)
                    self:_MoveCamera(delta)
                end
            end
            touchPanel.onRightMouseButtonPress:AddListener(self.m_onRightMouseButtonPress)
        end
    end

    self.m_hasListened = true
end

PhaseDialog._ClearRegisters = HL.Method() << function(self)
    if not self.m_panelItem then
        return
    end

    local touchPanel = self.m_panelItem.uiCtrl:GetTouchPanel()
    if not touchPanel or not self.m_onRightMouseButtonPress then
        return
    end

    if self.m_hasListened then
        touchPanel.onDrag:RemoveListener(self.m_onDrag)

        if BEYOND_DEBUG then
            touchPanel.onRightMouseButtonPress:RemoveListener(self.m_onRightMouseButtonPress)
        end
    end
    self.m_hasListened = false
end

PhaseDialog._MoveCamera = HL.Method(HL.Userdata) << function(self, delta)
    CameraManager:OnInput(UIUtils.getNormalizedScreenX(delta.x), UIUtils.getNormalizedScreenY(delta.y))
end







PhaseDialog._OnActivated = HL.Override() << function(self)

    if self.m_hasPendingExitDialog then
        local arg = self.m_pendingExitDialogArg
        self.m_pendingExitDialogArg = nil
        self.m_hasPendingExitDialog = false
        self:OnExitDialog(arg)
        return
    end

    PhaseDialog.ClearPhasesWithCam()
    self:_TryShowTrunk()
    self:_TryShowOptions()
    self:_AddRegisters()
    self:_InitPhaseDialogController()
end


PhaseDialog._OnDeActivated = HL.Override() << function(self)
    self:_ClearRegisters()
    self:_ClearPhaseDialogController()


    self:RemovePhasePanelItemById(PanelId.DialogRecord)
end

PhaseDialog._TryShowTrunk = HL.Method() << function(self)
    local mainFlowHandle = GameWorld.dialogManager.mainFlowHandle
    if not self.m_inited and mainFlowHandle ~= nil and mainFlowHandle.trunkNodeData then
        self:_DoPlayDialogTrunk(mainFlowHandle.trunkNodeData, true, mainFlowHandle.npcId, mainFlowHandle.templateId)
    else
        self.m_panelItem.uiCtrl:RefreshTrunk()
    end

end

PhaseDialog._TryShowOptions = HL.Method() << function(self)
    local options = GameWorld.dialogManager.options
    if not self.m_inited and options.Count > 0 then
        self:_DoShowDialogOption(options)
    end
end


PhaseDialog._OnDestroy = HL.Override() << function(self)
    UIManager:ToggleBlockObtainWaysJump("IN_CINEMATIC", false)
    self.m_panelItem = nil
end

PhaseDialog.OpenUI = HL.Method(HL.Table) << function(self, arg)
    local panelIdStr, paramStr, actionData = unpack(arg)
    local phaseId = PhaseId[panelIdStr]
    local param = not string.isEmpty(paramStr) and Utils.stringJsonToTable(paramStr) or {}
    param.fromDialog = true
    param.actionData = actionData
    if param.blockWhitePhaseName ~= nil then
        local exceptIds = {}
        for _, whitePhaseName in pairs(param.blockWhitePhaseName) do
            table.insert(exceptIds, whitePhaseName)
        end
        UIManager:ToggleBlockObtainWaysJump("IN_CINEMATIC", true, exceptIds)
    end

    if not phaseId then
        logger.error(("Dialog OpenUI Failed !! PanelId Not Found !! PanelIdStr is %s, Param is %s"):format(panelIdStr, paramStr))
        self:Next()
        return
    end

    self.m_panelItem.uiCtrl:PlayAnimationOutWithCallback(function()
        if phaseId == PhaseId.ReadingPopUp then
            local closeCallback = function() end
            param = {param.id, closeCallback}
        end

        local res = PhaseManager:OpenPhase(phaseId, param, nil, true)
        if res then
            self.m_openedPhaseId = phaseId
        else
            logger.error("Dialog OpenUI fail!!!", panelIdStr)
            GameWorld.dialogManager:Next()
        end

        GameWorld.dialogManager:TryRecoverVoiceManager()
    end)
end

PhaseDialog._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self:OnDialogPhaseTransitionBackToTop()
end

PhaseDialog.OnDialogPhaseTransitionBackToTop = HL.Method() << function(self)
    if self.doingOut or self.m_openedPhaseId == -1 then
        return
    end

    local nextIndex = PhaseDialog.GetNextIndex(self.m_openedPhaseId)
    self:ChangeNextIndex({
        phaseId = self.m_openedPhaseId,
        nextIndex = -1,
    })

    self.m_openedPhaseId = -1


    GameWorld.dialogManager:TryPauseVoiceManager()



    if nextIndex then
        self:Next(nextIndex)
    end


    if self.m_destroyed then
        return
    end


    local needForceCompleteTransition = self.m_hasPendingExitDialog or PhaseManager:IsInWaitingQueue(PhaseId.DialogTimeline)

    if needForceCompleteTransition then
        for phaseItem, _ in pairs(self.m_phaseItems) do
            local uiCtrl = phaseItem.uiCtrl
            if uiCtrl and uiCtrl.animationWrapper then
                local curState = uiCtrl.animationWrapper.curState
                if curState == CS.Beyond.UI.UIConst.AnimationState.In or curState == CS.Beyond.UI.UIConst.AnimationState.Out then
                    uiCtrl.animationWrapper:ClearTween(false)
                end
            end
        end
    else
        self.m_panelItem.uiCtrl:PlayAnimationIn()
    end
end

PhaseDialog.OnSendPresentEnd = HL.Method(HL.Table) << function(self, data)
    local success = data.success
    local deltaFav = data.deltaFav
    local selectedItems = data.selectedItems
    local nextIndex = data.nextIndex
    local levelChanged = data.levelChanged
    Notify(MessageConst.DIALOG_CHANGE_NEXT_INDEX, { phaseId = PhaseId.FriendShipPresent, nextIndex = nextIndex, })
    PhaseManager:PopPhase(PhaseId.FriendShipPresent)
    if success then
        self.m_panelItem.uiCtrl:ShowPresentSuccess(levelChanged, deltaFav, selectedItems)
    end
end


PhaseDialog.Next = HL.Method(HL.Opt(HL.Number)) << function(self, num)
    num = num or -1
    GameWorld.dialogManager:Next(num)
end


PhaseDialog.SetCtrlButtonVisible = HL.Method(HL.Boolean) << function(self, visible)
    local panelItem = self:_GetPanelPhaseItem(PanelId.Dialog)
    if panelItem then
        panelItem.uiCtrl:SetCtrlButtonVisible(visible)
    end
end


PhaseDialog._OpenDialogRecord = HL.Method() << function(self)
    GameWorld.dialogManager:SetAutoMode(false)
    local panelItem = self:_GetPanelPhaseItem(PanelId.DialogRecord)
    if not panelItem then
        panelItem = self:CreatePhasePanelItem(PanelId.DialogRecord)
    end
    panelItem.uiCtrl:Show()
end

PhaseDialog._HideDialogRecord = HL.Method() << function(self)
    local panelItem = self:_GetPanelPhaseItem(PanelId.DialogRecord)
    if panelItem then
        panelItem.uiCtrl:Hide()
    end
end



PhaseDialog._OpenDialogSkipPopUp = HL.Method() << function(self)
    local summaryId = GameWorld.dialogManager.summaryId
    local curAuto = GameWorld.dialogTimelineManager.autoMode
    GameWorld.dialogManager:SetAutoMode(false)
    if string.isEmpty(summaryId) then
        local dialogId = GameWorld.dialogManager.dialogId
        if not string.isEmpty(dialogId) then
            Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_CONFIRM_SKIP_DIALOG,
                onConfirm = function()
                    if curAuto then
                        GameWorld.dialogManager:SetAutoMode(curAuto)
                    end
                    GameWorld.dialogManager:SkipDialog(dialogId)
                end,
                onCancel = function()
                    if curAuto then
                        GameWorld.dialogManager:SetAutoMode(curAuto)
                    end
                end,
            })
        end
    else
        local panelItem = self:_GetPanelPhaseItem(PanelId.DialogSkipPopUp)
        local firstCreate = false
        if not panelItem then
            panelItem = self:CreatePhasePanelItem(PanelId.DialogSkipPopUp)
            firstCreate = true
        end

        if not UIManager:IsOpen(PanelId.DialogSkipPopUp) then
            logger.error("DialogSkipPopUp Panel is not opened! FirstCreate: " .. tostring(firstCreate))
            return
        end

        panelItem.uiCtrl:Show()
        panelItem.uiCtrl:RefreshSummary(summaryId)
        if curAuto then
            panelItem.uiCtrl:SetCloseRecoverAuto()
        end
    end
end

PhaseDialog._HideDialogSkipPopUp = HL.Method() << function(self)
    local panelItem = self:_GetPanelPhaseItem(PanelId.DialogSkipPopUp)
    if panelItem then
        panelItem.uiCtrl:Hide()
    end
end

PhaseDialog._SkipDialog = HL.Method() << function(self)
    self:_HideDialogSkipPopUp()
    local dialogId = GameWorld.dialogManager.dialogId
    if not string.isEmpty(dialogId) then
        GameWorld.dialogManager:SkipDialog(dialogId)
    end
end




PhaseDialog.m_dialogControllerThread = HL.Field(HL.Thread)

PhaseDialog._InitPhaseDialogController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.m_dialogControllerThread = self:_StartCoroutine(function()
        while true do
            coroutine.step()
            self:_UpdateControllerMoveCamera()
        end
    end)
end

PhaseDialog._ClearPhaseDialogController = HL.Method() << function(self)
    if self.m_dialogControllerThread ~= nil then
        self.m_dialogControllerThread = self:_ClearCoroutine(self.m_dialogControllerThread)
    end
end

PhaseDialog._UpdateControllerMoveCamera = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    if not self:_GetIsControllerDialogCameraValid() then
        return
    end

    local stickValue = InputManagerInst:GetGamepadStickValue(false)
    if InputManager.CheckGamepadStickInDeadZone(stickValue) then
        return
    end

    self:_MoveCamera(JsonConst.CONTROLLER_DIALOG_CAMERA_MOVE_SPEED[1] * stickValue)
end

PhaseDialog._GetIsControllerDialogCameraValid = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_panelItem.uiCtrl.view.inputGroup.groupEnabled
end



HL.Commit(PhaseDialog)
