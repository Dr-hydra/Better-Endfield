local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MachineToggle = HL.Class('MachineToggle', UIWidgetBase)

local ANIM_LEFT = "common_toggle_to_left03"
local ANIM_RIGHT = "common_toggle_to_right03"

MachineToggle.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.NodeUIInfo)

MachineToggle.m_preSwitchMode = HL.Field(HL.Function)

MachineToggle.m_postSwitchMode = HL.Field(HL.Function)

MachineToggle.m_baseModeName = HL.Field(HL.String) << ""

MachineToggle.m_switchModeName = HL.Field(HL.String) << ""


MachineToggle._OnFirstTimeInit = HL.Override() << function(self)
    self.view.toggle.onValueChanged:AddListener(function(isOn)

        self.view.toggle:SetIsOnWithoutNotify(not isOn)
        self:_OnModeSwitchButtonClicked()
    end)
end

MachineToggle.InitMachineToggle = HL.Method(CS.Beyond.Gameplay.RemoteFactory.NodeUIInfo, HL.Table) << function(self, uiInfo, args)
    self:_FirstTimeInit()

    self.m_uiInfo = uiInfo
    self.m_preSwitchMode = args.preSwitchMode
    self.m_postSwitchMode = args.postSwitchMode

    local nodePredefinedParam = self.m_uiInfo.nodeHandler.predefinedParam
    local needModeNode = true
    if nodePredefinedParam ~= nil and nodePredefinedParam.producer ~= nil then
        needModeNode = nodePredefinedParam.producer.enableModeSwitch
    end
    if needModeNode then
        if nodePredefinedParam ~= nil and
            nodePredefinedParam.producer ~= nil and
            nodePredefinedParam.producer.modeUseCustom then
            self.m_baseModeName = nodePredefinedParam.producer.modeCustom0 or ""
            self.m_switchModeName = nodePredefinedParam.producer.modeCustom1 or ""
            needModeNode = not self.m_baseModeName:isEmpty() and not self.m_switchModeName:isEmpty()
        else
            needModeNode, self.m_baseModeName, self.m_switchModeName = FactoryUtils.checkBuildingHasModeSwitch(self.m_uiInfo.nodeHandler.templateId, Utils.isInBlackbox())
        end
    end

    if needModeNode then
        self.gameObject:SetActiveIfNecessary(true)

        local formulaMan = self.m_uiInfo.formulaMan
        if formulaMan ~= nil then

            if formulaMan.currentMode ~= self.m_baseModeName and formulaMan.currentMode ~= self.m_switchModeName then
                if self.m_baseModeName == FacConst.FAC_FORMULA_MODE_MAP.GASLIQUID then
                    self.m_baseModeName = formulaMan.currentMode
                end
                if self.m_switchModeName == FacConst.FAC_FORMULA_MODE_MAP.GASLIQUID then
                    self.m_switchModeName = formulaMan.currentMode
                end
            end
            self.view.toggle:SetIsOnWithoutNotify(formulaMan.currentMode == self.m_switchModeName)
            InputManagerInst:SetBindingText(self.view.toggle.hoverConfirmBindingId, self:GetControllerSideMenuText())
            self.view.animation:SampleClipAtPercent(formulaMan.currentMode == self.m_switchModeName and ANIM_LEFT or ANIM_RIGHT, 1)
        end

        local baseModeData = Tables.factoryMachineCraftModeTable:GetValue(self.m_baseModeName)
        self.view.baseModeText.text = baseModeData.machineModeTypeName
        self.view.baseModeIcon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_COMMON, baseModeData.iconId)
        local switchModeData = Tables.factoryMachineCraftModeTable:GetValue(self.m_switchModeName)
        self.view.switchModeText.text = switchModeData.machineModeTypeName
        self.view.switchModeIcon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_COMMON, switchModeData.iconId)
    else
        self.gameObject:SetActiveIfNecessary(false)
    end
end

MachineToggle._OnModeSwitchButtonClicked = HL.Method() << function(self)
    local targetMode = self.m_uiInfo.formulaMan.currentMode == self.m_baseModeName and self.m_switchModeName or self.m_baseModeName

    local success = FactoryUtils.checkSwitchBuildingMode(self.m_uiInfo.buildingId, self.m_uiInfo.formulaMan.currentMode, self.m_baseModeName, self.m_switchModeName, function()
        self:_SwitchMode(targetMode)
    end)
    if success then
        self:_SwitchMode(targetMode)
    end
end

MachineToggle._SwitchMode = HL.Method(HL.String) << function(self, targetMode)
    local lockId = FactoryUtils.getMachineCraftLockFormulaId(self.m_uiInfo.nodeId)
    if not string.isEmpty(lockId) and not FactoryUtils.isMachineCraftInMode(self.m_uiInfo.buildingId, targetMode, lockId) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_FORBID_SWITCH_MODE_LOCKED_FORMULA)
        return
    end
    if self.m_preSwitchMode ~= nil then
        self.m_preSwitchMode()
    end
    GameInstance.player.remoteFactory.core:Message_OpChangeProducerMode(Utils.getCurrentChapterId(), self.m_uiInfo.nodeId, targetMode, function(message, result)
        if self.m_isDestroyed then
            return
        end
        if self.m_postSwitchMode ~= nil then
            self.m_postSwitchMode()
        end
        self.view.toggle:SetIsOnWithoutNotify(self.m_uiInfo.formulaMan.currentMode == self.m_switchModeName)
        InputManagerInst:SetBindingText(self.view.toggle.hoverConfirmBindingId, self:GetControllerSideMenuText())
        self.view.animation:Play(self.m_uiInfo.formulaMan.currentMode == self.m_switchModeName and ANIM_LEFT or ANIM_RIGHT)
        local toggleAudio = FacConst.FAC_FORMULA_MODE_TOGGLE_AUDIO[self.m_uiInfo.formulaMan.currentMode]
        if toggleAudio then
            AudioAdapter.PostEvent(toggleAudio)
        end
    end)
end




MachineToggle.ControllerSideMenuClick = HL.Method() << function(self)
    self:_OnModeSwitchButtonClicked()
end

MachineToggle.GetControllerSideMenuText = HL.Method().Return(HL.String) << function(self)
    if self.m_uiInfo.formulaMan.currentMode == self.m_baseModeName then
        return string.format(Language.LUA_MACHINE_TOGGLE_CONTROLLER_SIDE_NAME, self.view.switchModeText.text)
    end
    return string.format(Language.LUA_MACHINE_TOGGLE_CONTROLLER_SIDE_NAME, self.view.baseModeText.text)
end

MachineToggle.GetControllerSideMenuSprite = HL.Method().Return(HL.Userdata) << function(self)
    return self.view.controllerSideImage.sprite
end



HL.Commit(MachineToggle)
return MachineToggle
