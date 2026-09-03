local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacMiniPowerHud
FacMiniPowerHudCtrl = HL.Class('FacMiniPowerHudCtrl', uiCtrl.UICtrl)





FacMiniPowerHudCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_EXIT_FACTORY_MODE] = 'OnExitFactoryMode',
    [MessageConst.ON_EXIT_BUILDING_MODE] = 'OnExitBuildingMode',
    [MessageConst.ON_IN_FAC_MAIN_REGION_CHANGE] = 'OnInFacMainRegionChange',
    [MessageConst.ON_FAC_BUILDING_PREVIEW_POSITION_ROTATION_CHANGED] = 'OnFacBuildingPreviewPositionRotationChanged',
    [MessageConst.ON_FAC_CHAPTER_RESET] = 'OnFacChapterReset',
    [MessageConst.ON_BUILD_MODE_CHANGE] = 'OnBuildModeChange',
}

FacMiniPowerHudCtrl.m_miniPowerContent = HL.Field(HL.Userdata)

FacMiniPowerHudCtrl.m_baseMode = HL.Field(HL.String) << ""

FacMiniPowerHudCtrl.m_switchMode = HL.Field(HL.String) << ""

FacMiniPowerHudCtrl.m_ignoreModeSwitchPopUp = HL.Field(HL.Boolean) << false


FacMiniPowerHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_miniPowerContent = self.view.defaultNode.facMiniPowerContent
    self.m_miniPowerContent:InitFacMiniPowerContent()
    self.m_miniPowerContent.shouldShowBackupPower = true
    self.m_miniPowerContent.isMonitorPower = true
    self.m_miniPowerContent.gameObject:SetActive(false)

    self.view.defaultNode.animationWrapper:SampleToOutAnimationEnd()

    local node = self.view.defaultNode
    node.multimodeToggle:InitCommonToggle(function(isOn)
        self:_OnMultimodeToggleChanged(isOn)
    end, false, true)
    node.multimodeToggle:SetCustomAnimation("common_toggle_to_left05", "common_toggle_to_right05")
    node.multimodeToggle.toggle.checkIsValueValid = function(isOn)
        return self:_CheckIsMultimodeToggleValueValid(isOn)
    end
    self:_HideMultimodeToggle()

    UIManager:SetTopOrder(PanelId.MainHud)
end

FacMiniPowerHudCtrl.OnShow = HL.Override() << function(self)
    self.m_miniPowerContent:ToggleCoroutine(true)
end

FacMiniPowerHudCtrl.OnHide = HL.Override() << function(self)
    self.m_miniPowerContent:ToggleCoroutine(false)
end

FacMiniPowerHudCtrl.OnEnterFactoryMode = HL.StaticMethod() << function()
    if FactoryUtils.isInBuildMode() then

        return
    end
    local self = UIManager:AutoOpen(PANEL_ID)
    if self.view.defaultNode.animationWrapper.curState == CS.Beyond.UI.UIConst.AnimationState.Out then

        self.view.defaultNode.animationWrapper:SampleToOutAnimationEnd()
        if not self:IsShow() then
            self:Show()
        end
    end
    self.view.defaultNode.animationWrapper:PlayInAnimation()
    self:_OnBuildModeChange("")
end

FacMiniPowerHudCtrl.OnExitFactoryMode = HL.Method() << function(self)
    if FactoryUtils.isInBuildMode() then
        return
    end
    self.view.defaultNode.animationWrapper:PlayOutAnimation(function()
        self:Hide()
    end)
end

FacMiniPowerHudCtrl.OnEnterBuildingMode = HL.StaticMethod(HL.String) << function(itemId)
    local self = UIManager:AutoOpen(PANEL_ID)
    self.view.defaultNode.animationWrapper:SampleToInAnimationEnd()
    self.view.defaultNode.animationWrapper:PlayWithTween("fac_mini_bar_enter_fac_mode_change")
    self:_OnBuildModeChange(itemId)
end

FacMiniPowerHudCtrl.OnExitBuildingMode = HL.Method() << function(self)
    if Utils.isInFactoryMode() and self:IsShow() then
        self.view.defaultNode.animationWrapper:SampleToInAnimationEnd()
        self:_OnBuildModeChange("")
    else
        self.view.defaultNode.animationWrapper:PlayWithTween("fac_mini_bar_enter_fac_mode_changeout", function()
            self:Hide()
        end)
    end
end

FacMiniPowerHudCtrl.OnInFacMainRegionChange = HL.Method(HL.Boolean) << function(self, _)
    if not string.isEmpty(self.m_curBuildBuildingItemId) then
        self:_OnBuildModeChange(self.m_curBuildBuildingItemId)
    end
end

FacMiniPowerHudCtrl.m_curBuildBuildingItemId = HL.Field(HL.String) << ''

FacMiniPowerHudCtrl._OnBuildModeChange = HL.Method(HL.String) << function(self, buildingItemId)
    local changed = self.m_curBuildBuildingItemId ~= buildingItemId
    self.m_curBuildBuildingItemId = buildingItemId
    local data = FactoryUtils.getItemBuildingData(buildingItemId)
    local inBuildingMode = FactoryUtils.isInBuildMode()
    local node = self.view.defaultNode

    node.facMiniPowerContent:SwitchFacMiniPowerContent(buildingItemId)

    if changed then
        self.m_ignoreModeSwitchPopUp = false
    end
    self:_RefreshMultimodeToggle(buildingItemId)

    node.buildPreviewTxt.gameObject:SetActive(inBuildingMode)
    if not inBuildingMode then
        return
    end
    node.buildPreviewTxt.text = string.format(Language.LUA_BUILD_PREVIEW_TITLE, data.name)
end

FacMiniPowerHudCtrl.OnFacBuildingPreviewPositionRotationChanged = HL.Method() << function(self)
    local node = self.view.defaultNode
    if string.isEmpty(self.m_curBuildBuildingItemId) then
        return
    end
    node.facMiniPowerContent:SwitchFacMiniPowerContent(self.m_curBuildBuildingItemId)
end

FacMiniPowerHudCtrl._HideMultimodeToggle = HL.Method() << function(self)
    self.m_baseMode, self.m_switchMode = "", ""
    self.view.defaultNode.multimodeToggle.gameObject:SetActive(false)
end

FacMiniPowerHudCtrl._RefreshMultimodeToggle = HL.Method(HL.String, HL.Opt(HL.Boolean)) << function(self, buildingItemId)
    if not FactoryUtils.isInBuildMode() or string.isEmpty(buildingItemId) then
        self:_HideMultimodeToggle()
        return
    end

    local buildingMode = GameInstance.remoteFactoryManager.interact.currentBuildingMode
    if not buildingMode then
        self:_HideMultimodeToggle()
        return
    end

    local buildingId = buildingMode.buildingId
    local hasModeSwitch, baseMode, switchMode = FactoryUtils.checkBuildingHasModeSwitch(buildingId)
    if not hasModeSwitch then
        self:_HideMultimodeToggle()
        return
    end

    self.m_baseMode, self.m_switchMode = baseMode, switchMode

    local currentMode = buildingMode.nodeMode
    if currentMode ~= baseMode and currentMode ~= switchMode then
        logger.error(ELogChannel.Factory, "Invalid building mode, buildingId: " .. tostring(buildingId)
            .. ", currentMode: " .. tostring(currentMode)
            .. ", baseMode: " .. tostring(baseMode) .. ", switchMode: " .. tostring(switchMode))
        self:_HideMultimodeToggle()
        return
    end

    local node = self.view.defaultNode
    node.multimodeToggle.gameObject:SetActive(true)
    node.multimodeToggle:SetValue(currentMode == switchMode, true)


    local switchModeData = Tables.factoryMachineCraftModeTable:GetValue(switchMode)
    node.multimodeToggle.view.left.text.text = switchModeData.machineModeTypeName
    node.multimodeToggle.view.left.icon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_COMMON, switchModeData.iconId)
    local baseModeData = Tables.factoryMachineCraftModeTable:GetValue(baseMode)
    node.multimodeToggle.view.right.text.text = baseModeData.machineModeTypeName
    node.multimodeToggle.view.right.icon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_COMMON, baseModeData.iconId)

    CS.Beyond.Gameplay.Conditions.OnFacEnterBuildingModeSwitch.Trigger()
end

FacMiniPowerHudCtrl._CheckIsMultimodeToggleValueValid = HL.Method(HL.Boolean).Return(HL.Boolean) << function(self, isOn)
    if self.m_ignoreModeSwitchPopUp then
        return true
    end

    local buildingMode = GameInstance.remoteFactoryManager.interact.currentBuildingMode
    if not buildingMode then
        return false
    end

    if not buildingMode.isMoving then
        return true
    end

    local success = FactoryUtils.checkSwitchBuildingMode(buildingMode.buildingId, buildingMode.nodeMode, self.m_baseMode, self.m_switchMode, function()
        self.m_ignoreModeSwitchPopUp = true
        self:_OnMultimodeToggleChanged(isOn)
    end)
    return success
end

FacMiniPowerHudCtrl._OnMultimodeToggleChanged = HL.Method(HL.Boolean) << function(self, isOn)
    local targetMode = isOn and self.m_switchMode or self.m_baseMode
    if string.isEmpty(targetMode) then
        self:_HideMultimodeToggle()
        return
    end

    local buildingMode = GameInstance.remoteFactoryManager.interact.currentBuildingMode
    if not buildingMode then
        self:_HideMultimodeToggle()
        return
    end

    local success = buildingMode:TrySetPreviewMode(CSFactoryUtil.ToFacNodeMode(targetMode))
    if success then
        self.view.defaultNode.facMiniPowerContent:SwitchFacMiniPowerContent(self.m_curBuildBuildingItemId)
    end
    self:_RefreshMultimodeToggle(self.m_curBuildBuildingItemId)

    local toggleAudio = FacConst.FAC_FORMULA_MODE_TOGGLE_AUDIO[targetMode]
    if toggleAudio then
        AudioAdapter.PostEvent(toggleAudio)
    end
end

FacMiniPowerHudCtrl.OnBuildModeChange = HL.Method(HL.Number) << function(self, mode)

    local node = self.view.defaultNode
    local hideBackupPower = mode ~= FacConst.FAC_BUILD_MODE.Normal
    self.m_miniPowerContent:ToggleBackupPowerShow(not hideBackupPower)
end

FacMiniPowerHudCtrl.OnFacChapterReset = HL.Method() << function(self)
    local node = self.view.defaultNode
    node.facMiniPowerContent:ClearMemorizedPowerInfo()
    if string.isEmpty(self.m_curBuildBuildingItemId) then
        return
    end
    node.facMiniPowerContent:SwitchFacMiniPowerContent(self.m_curBuildBuildingItemId)
    self:_RefreshMultimodeToggle(self.m_curBuildBuildingItemId)
end

HL.Commit(FacMiniPowerHudCtrl)
