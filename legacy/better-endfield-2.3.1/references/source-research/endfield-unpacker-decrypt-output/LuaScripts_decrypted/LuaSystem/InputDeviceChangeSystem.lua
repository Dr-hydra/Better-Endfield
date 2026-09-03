local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')

InputDeviceChangeSystem = HL.Class('InputDeviceChangeSystem', LuaSystemBase.LuaSystemBase)

InputDeviceChangeSystem.m_forbidKeys = HL.Field(HL.Table)

InputDeviceChangeSystem.InputDeviceChangeSystem = HL.Constructor() << function(self)
    self.m_forbidKeys = {}
end

InputDeviceChangeSystem.OnInit = HL.Override() << function(self)
    InputManagerInst.needProcessTryChange = true
    logger.important(CS.Beyond.EnableLogType.DevOnly, "[InputDevice] 主流程关闭直接切换设备")
    Register(MessageConst.ON_TRY_CHANGE_INPUT_DEVICE_TYPE, function(arg)
        if not GameInstance.isInGameplay then
            return
        end
        local inputType = unpack(arg)
        self:_OnTryChangeInputDevice(inputType)
    end, self)
end

InputDeviceChangeSystem.OnRelease = HL.Override() << function(self)
    InputManagerInst.needProcessTryChange = false
    logger.important(CS.Beyond.EnableLogType.DevOnly, "[InputDevice] 主流程开启直接切换设备")
end

InputDeviceChangeSystem._CheckCanChangeInputDevice = HL.Method(HL.Opt(HL.Boolean)).Return(HL.Boolean) << function(self, ignoreIsChanging)
    if not ignoreIsChanging and InputManagerInst.inChangingInputDevice then
        return false
    end

    if PhaseManager:CheckIsInTransition() then
        return false
    end

    if not string.isEmpty(GameInstance.player.systemActionConflictManager.curProcessingSystemAction) then
        return false
    end

    local topPhaseId = PhaseManager:GetTopPhaseId()
    if not ignoreIsChanging and topPhaseId == PhaseId.Level then
        local _, mainHud = UIManager:IsOpen(PanelId.MainHud)
        if mainHud and not mainHud.view.inputGroup.internalEnabled then
            if not FactoryUtils.isInTopView() then
                return false
            end
        end
    end

    if next(self.m_forbidKeys) ~= nil then
        return false
    end

    if GameInstance.player.guide.isInForceGuide then
        return false
    end

    if GameWorld.gameMechManager.travelPoleBrain.inFastTravelMode then
        return false
    end

    if Utils.isInBlackbox() then
        return false
    end

    if Utils.isInNarrative() then
        return false
    end

    if FactoryUtils.isCreatingBlueprint() then
        return false
    end

    if GameInstance.player.towerDefenseSystem.hudState == CS.Beyond.Gameplay.TowerDefenseSystem.HUDState.WaitingFinished then
        return false
    end

    local _, generalAbility = UIManager:IsOpen(PanelId.GeneralAbility)
    if generalAbility ~= nil and generalAbility.startPress then
        return false
    end

    if not self:_CheckCanChangeInputDeviceInCommercial() then
        return false
    end

    for _, panelId in pairs(InputDeviceChangeConst.FORBID_INPUT_DEVICE_CHANGE_PANELS) do
        if UIManager:IsShow(PanelId[panelId]) then
            return false
        end
    end

    for _, phaseId in pairs(InputDeviceChangeConst.FORBID_INPUT_DEVICE_CHANGE_PHASES) do
        if PhaseManager:IsOpen(PhaseId[phaseId]) then
            return false
        end
    end

    if GameInstance.playerController.inUltimateCasting then
        return false
    end

    return true
end

InputDeviceChangeSystem._CheckCanChangeInputDeviceInCommercial = HL.Method().Return(HL.Boolean) << function(self)

    local isGachaOpen, gachaCtrl = UIManager:IsOpen(PanelId.GachaPool)
    if isGachaOpen and gachaCtrl:GetIsPlayingTabInAnimation() then
        return false
    end



    local isInBPSeasonDisplay = UIManager:IsOpen(PanelId.BattlePassSeasonDisplay)
    if isInBPSeasonDisplay then
        return false
    end




    if UIManager:IsShow(PanelId.ActivityPopUp) then
        return false
    end


    return true
end

InputDeviceChangeSystem._CheckChangeInputToastIgnore = HL.Method().Return(HL.Boolean) << function(self)
    return Utils.isInNarrative() and Utils.isNarrativeTopPhase()
end

InputDeviceChangeSystem._OnTryChangeInputDevice = HL.Method(HL.Userdata) << function(self, inputType)
    logger.important(CS.Beyond.EnableLogType.DevOnly, "[InputDevice] 开始切换设备", inputType)
    if not self:_CheckCanChangeInputDevice() then
        logger.important(CS.Beyond.EnableLogType.DevOnly, "[InputDevice] 当前无法切换输入设备", inputType)
        if self:_CheckChangeInputToastIgnore() then
            return
        end
        Notify(MessageConst.SHOW_TOAST, Language.LUA_INPUT_DEVICE_CHANGE_FORBIDDEN)
        return
    end

    InputManagerInst:ToggleInputDeviceChangeMode(true)
    logger.important(CS.Beyond.EnableLogType.DevOnly, "[InputDevice] 确认切换设备", inputType)
    Notify(MessageConst.HIDE_ITEM_TIPS, { skipAnim = true })
    self:_RealChangeInputDevice(inputType)
end

InputDeviceChangeSystem._OnInputDeviceTypeChangeFinish = HL.Method(HL.Userdata) << function(self, inputType)
    if not InputManagerInst.inChangingInputDevice then
        return
    end

    InputManagerInst:ToggleInputDeviceChangeMode(false)
    Notify(MessageConst.ON_CHANGE_INPUT_DEVICE_TYPE_FINISHED, { inputType = inputType })
end

InputDeviceChangeSystem._RealChangeInputDevice = HL.Method(HL.Userdata) << function(self, newInputType)
    local phaseArgs = PhaseManager:CollectCurPhaseArgs()

    Notify(MessageConst.ON_CONFIRM_CHANGE_INPUT_DEVICE_TYPE, { inputType = newInputType })

    local closeExceptPanelIds = {}
    for _, name in pairs(InputDeviceChangeConst.EXCEPT_CHANGE_DEVICE_CLOSE_PANEL) do
        local panelId = PanelId[name]
        if UIManager:_IsUsingSamePanelAssetOnInputTypeChange(panelId, DeviceInfo.inputType, newInputType) then
            table.insert(closeExceptPanelIds, panelId)
        end
    end

    UIManager.m_isHotSwitching = true
    UIManager.m_hotSwitchCache = {}

    InputManagerInst.controllerNaviManager:SetTarget(nil)
    InputManagerInst.controllerNaviManager:ResetStateForUIDispose()

    PhaseManager:_ExitAndCloseAll(closeExceptPanelIds)
    for _, name in pairs(InputDeviceChangeConst.INPUT_DEVICE_CHANGE_FORCE_CLOSE_PANELS) do
        UIManager:Close(PanelId[name])
    end

    DeviceInfo.ChangeInputType(newInputType)
    GameInstance.player.guide:OnInputDeviceChanged()

    PhaseManager:RecoverPhaseByArgs(phaseArgs)

    for panelId, _ in pairs(UIManager.m_openedPanels) do
        UIManager:RestoreShownPanelVisibilityIfNeeded(panelId)
    end

    for _, cachedInfo in pairs(UIManager.m_hotSwitchCache) do
        if cachedInfo.worldRoot then
            GameObject.DestroyImmediate(cachedInfo.worldRoot.gameObject)
        end
        if cachedInfo.worldAutoRoot then
            GameObject.DestroyImmediate(cachedInfo.worldAutoRoot.gameObject)
        end
        cachedInfo.loader:DisposeAllHandles()
        GameObject.DestroyImmediate(cachedInfo.go)
    end
    UIManager.m_hotSwitchCache = {}
    UIManager.m_isHotSwitching = false

    self:_OnInputDeviceTypeChangeFinish(newInputType)
end




InputDeviceChangeSystem.SetForbidInputDeviceChange = HL.Method(HL.String, HL.Boolean) << function(self, key, forbid)
    if forbid then
        self.m_forbidKeys[key] = true
    else
        self.m_forbidKeys[key] = nil
    end
end




HL.Commit(InputDeviceChangeSystem)
return InputDeviceChangeSystem
