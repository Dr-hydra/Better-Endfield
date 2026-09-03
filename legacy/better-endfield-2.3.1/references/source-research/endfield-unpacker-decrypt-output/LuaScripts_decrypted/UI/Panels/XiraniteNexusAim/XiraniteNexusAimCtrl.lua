
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.XiraniteNexusAim

XiraniteNexusAimCtrl = HL.Class('XiraniteNexusAimCtrl', uiCtrl.UICtrl)






XiraniteNexusAimCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CLOSE_XIRANITE_NEXUS_AIM] = '_OnCloseXiraniteNexusAim',
    [MessageConst.ON_UI_CANVAS_SIZE_CHANGED] = '_OnCanvasSizeChanged',
    [MessageConst.ON_XIRANITE_NEXUS_SCAN_STATE_CHANGE] = '_OnXiraniteNexusScanStateChange',
}

XiraniteNexusAimCtrl.m_energyLockTickKey = HL.Field(HL.Number) << -1

XiraniteNexusAimCtrl.m_xiraniteNexusBrain = HL.Field(HL.Userdata)

XiraniteNexusAimCtrl.m_blightMiasmaBrain = HL.Field(HL.Userdata)

XiraniteNexusAimCtrl.m_isFocusing = HL.Field(HL.Boolean) << false

XiraniteNexusAimCtrl.s_expandAnimPlayed = HL.StaticField(HL.Boolean) << false

XiraniteNexusAimCtrl.s_reduceAnimPlayed = HL.StaticField(HL.Boolean) << true


XiraniteNexusAimCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_xiraniteNexusBrain = GameWorld.gameMechManager.xiraniteNexusBrain
    self.m_blightMiasmaBrain = GameWorld.gameMechManager.blightMiasmaBrain
    self:_InitAction()
    self:_SetAimRadius()
    self.m_energyLockTickKey = LuaUpdate:Add("TailTick", function()
        self:_EnergyLockTick()
    end)
    self.m_isFocusing = false
    self.view.cleanNode.gameObject:SetActive(false)
    self:_UpdateScanState()
    Notify(MessageConst.GENERAL_ABILITY_ACTIVE_TEMP_ANIM_LOOP, false)
end

XiraniteNexusAimCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.GENERAL_ABILITY_CHANGE_KEY_BINDING, {false, "XiraniteNexusAim"})
    Notify(MessageConst.GENERAL_ABILITY_ACTIVE_TEMP_ANIM_LOOP, true)
    LuaUpdate:Remove(self.m_energyLockTickKey)
end

XiraniteNexusAimCtrl.OnShow = HL.Override() << function(self)
    self:_PlayAnim()
end

XiraniteNexusAimCtrl._InitAction = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        self:_ExitAimMode()
    end)
end

XiraniteNexusAimCtrl._ExitAimMode = HL.Method() << function(self)
    if GameUtil.mainCharacter and
        GameUtil.mainCharacter.customAbilityCom.curState == CS.Beyond.Gameplay.CustomAbilityType.XiraniteNexus then
        GameUtil.mainCharacter.customAbilityCom.curAbility:ExitAimMode()
    end
end

XiraniteNexusAimCtrl._OnCloseXiraniteNexusAim = HL.Method() << function(self)
    local isExpand = self.m_xiraniteNexusBrain.expandRate
    if isExpand then
        self.view.luaPanel:BlockAllInput()
        self.view.expandAnimWrapper:PlayOutAnimation(function()
            self.view.luaPanel:RecoverAllInput()
            self:Close()
        end)
    else
        self:PlayAnimationOutAndClose()
    end
end

XiraniteNexusAimCtrl._IsRadarEquipped = HL.Method().Return(HL.Boolean) << function(self)
    local itemBag = GameInstance.player.inventory.itemBag:GetOrFallback(Utils.getCurrentScope())
    for _, itemId in pairs(itemBag.curValidColoredItemIds) do
        local _, deviceData = Tables.itemPortableDeviceTable:TryGetValue(itemId)
        if deviceData and deviceData.type == GEnums.PortableDeviceType.XiraniteNexus and not deviceData.isMainDevice then
            return true
        end
    end
    return false
end

XiraniteNexusAimCtrl._OnCanvasSizeChanged = HL.Method() << function(self)
    self:_SetAimRadius()
end

XiraniteNexusAimCtrl._SetAimRadius = HL.Method() << function(self)
    local radiusScreenRate = self.m_xiraniteNexusBrain:GetScanScreenRate() * DataManager.xiraniteNexusConfig.uiRangeRate
    local isExpand = self.m_xiraniteNexusBrain.expandRate
    local stateName = isExpand and "Expand" or "Normal"

    self.view.stateController:SetState(stateName)
    local rootSize = self.view.rectTransform.sizeDelta
    local animNode = isExpand and self.view.expandAimNode.rectTransform or self.view.normalAimNode.rectTransform
    local scale = math.min(rootSize.x, rootSize.y) * radiusScreenRate * 2 / animNode.sizeDelta.x
    self.view.main.localScale = Vector3(scale, scale, 1)
end

XiraniteNexusAimCtrl._EnergyLockTick = HL.Method() << function(self)
    local isFocusing, position, progress = self.m_xiraniteNexusBrain:TryGetFocusingEnergyLockDisplayData()
    if self.m_isFocusing ~= isFocusing then
        self.m_isFocusing = isFocusing
        UIUtils.PlayAnimationAndToggleActive(self.view.cleanNodeAnimationWrapper, isFocusing)
        if isFocusing then
            AudioAdapter.PostEvent('Au_UI_Toast_Xiranitenexus_Loading')
        end
    end
    if not isFocusing then
        return
    end

    self.view.cleanProgressImg.fillAmount = progress
    self.view.cleanNode.transform.anchoredPosition = UIUtils.objectPosToUI(position, self.uiCamera, self.view.rectTransform)
    local stateName = self.m_blightMiasmaBrain.currentTolerance + self.m_blightMiasmaBrain.preTolerance <= 0 and 'Red' or 'Green'
    self.view.cleanNode:SetState(stateName, false)
end

XiraniteNexusAimCtrl._UpdateScanState = HL.Method() << function(self)
    local isScanning = self.m_xiraniteNexusBrain.isScanning
    local aimNode = self.m_xiraniteNexusBrain.expandRate and self.view.expandAimNode or self.view.normalAimNode
    UIUtils.PlayAnimationAndToggleActive(aimNode.lightSweepImage, isScanning)
    UIUtils.PlayAnimationAndToggleActive(aimNode.activeaniNode, isScanning)
end

XiraniteNexusAimCtrl._OnXiraniteNexusScanStateChange = HL.Method(HL.Table) << function(self, args)
    self:_UpdateScanState()
end

XiraniteNexusAimCtrl._PlayAnim = HL.Method() << function(self)
    local isExpand = self.m_xiraniteNexusBrain.expandRate
    if isExpand then
        XiraniteNexusAimCtrl.s_reduceAnimPlayed = false
        if XiraniteNexusAimCtrl.s_expandAnimPlayed then
            self.view.expandAnimWrapper:PlayInAnimation()
            AudioAdapter.PostEvent('Au_UI_Popup_Xiranitenexus_Open_ex')
        else
            XiraniteNexusAimCtrl.s_expandAnimPlayed = true
            self.animationWrapper:Play("xiranitenexusaim_change")
            AudioAdapter.PostEvent('Au_UI_Popup_Xiranitenexus_Open_enlarge')
        end
    else
        XiraniteNexusAimCtrl.s_expandAnimPlayed = false
        if XiraniteNexusAimCtrl.s_reduceAnimPlayed then
            self.animationWrapper:PlayInAnimation()
            AudioAdapter.PostEvent('Au_UI_Popup_Xiranitenexus_Open')
        else
            XiraniteNexusAimCtrl.s_reduceAnimPlayed = true
            self.animationWrapper:Play("xiranitenexusaim_exchange")
            AudioAdapter.PostEvent('Au_UI_Popup_Xiranitenexus_Open_narrow')
        end
    end
end



XiraniteNexusAimCtrl.OnOpenXiraniteNexusAim = HL.StaticMethod() << function()
    UIManager:AutoOpen(PANEL_ID)
    Notify(MessageConst.GENERAL_ABILITY_CHANGE_KEY_BINDING, {true, "XiraniteNexusAim"})
end

XiraniteNexusAimCtrl.TryActiveAbility = HL.StaticMethod(HL.Opt(HL.Boolean)) << function(select)
    if select == nil then
        select = true
    end
    if not XiraniteNexusAimCtrl.IsXiraniteNexusEquipped() and not XiraniteNexusAimCtrl.IsInTempActiveLevel() then
        return
    end
    GameInstance.player.generalAbilitySystem:ActivateTempAbility(GEnums.GeneralAbilityType.XiraniteNexus, select)
end

XiraniteNexusAimCtrl.DeActiveAbility = HL.StaticMethod() << function()
    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if isOpen then
        ctrl:_ExitAimMode()
    end
    if not GameInstance.player.generalAbilitySystem:IsTempAbilityActive(GEnums.GeneralAbilityType.XiraniteNexus) then
        return
    end
    GameInstance.player.generalAbilitySystem:DeactivateTempAbility(GEnums.GeneralAbilityType.XiraniteNexus)
end

XiraniteNexusAimCtrl.IsXiraniteNexusEquipped = HL.StaticMethod().Return(HL.Boolean) << function()
    local itemBag = GameInstance.player.inventory.itemBag:GetOrFallback(Utils.getCurrentScope())
    for _, itemId in pairs(itemBag.curValidColoredItemIds) do
        local _, deviceData = Tables.itemPortableDeviceTable:TryGetValue(itemId)
        if deviceData and deviceData.type == GEnums.PortableDeviceType.XiraniteNexus and deviceData.isMainDevice then
            return true
        end
    end
    return false
end

XiraniteNexusAimCtrl.IsInTempActiveLevel = HL.StaticMethod().Return(HL.Boolean) << function()
    local curLevelId = GameWorld.worldInfo.curLevelId
    for _, data in pairs(DataManager.xiraniteNexusConfig.levelSpecificTolerances) do
        if data.levelId == curLevelId then
            return true
        end
    end
    return false
end

XiraniteNexusAimCtrl.s_IsXiraniteNexusEquipped = HL.StaticField(HL.Any) << nil

XiraniteNexusAimCtrl.OnItemBagColoredItemChanged = HL.StaticMethod(HL.Table) << function(args)
    local isXiraniteNexusEquipped = XiraniteNexusAimCtrl.IsXiraniteNexusEquipped()
    if XiraniteNexusAimCtrl.s_IsXiraniteNexusEquipped ~= isXiraniteNexusEquipped then
        XiraniteNexusAimCtrl.s_IsXiraniteNexusEquipped = isXiraniteNexusEquipped
        XiraniteNexusAimCtrl._UpdateAbilityActivation()
    end

    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if isOpen then
        ctrl:_SetAimRadius()
    end
end

XiraniteNexusAimCtrl.OnCurrentLevelChanged = HL.StaticMethod() << function()
    XiraniteNexusAimCtrl.s_IsXiraniteNexusEquipped = XiraniteNexusAimCtrl.IsXiraniteNexusEquipped()
    XiraniteNexusAimCtrl._UpdateAbilityActivation(false)
end

XiraniteNexusAimCtrl.OnCloseLoadingPanel = HL.StaticMethod() << function()
    if GameWorld.worldInfo == nil then
        return
    end

    XiraniteNexusAimCtrl._UpdateAbilityActivation(false)
end

XiraniteNexusAimCtrl._UpdateAbilityActivation = HL.StaticMethod(HL.Opt(HL.Boolean)) << function(select)
    if XiraniteNexusAimCtrl.IsInTempActiveLevel() then
        XiraniteNexusAimCtrl.TryActiveAbility(select)
    elseif XiraniteNexusAimCtrl.IsXiraniteNexusEquipped() then
        local curLevelId = GameWorld.worldInfo.curLevelId
        local activationLevelIds = DataManager.xiraniteNexusConfig.activationLevelIds
        if activationLevelIds:Contains(curLevelId) then
            XiraniteNexusAimCtrl.TryActiveAbility(select)
        else
            XiraniteNexusAimCtrl.DeActiveAbility()
        end
    else
        XiraniteNexusAimCtrl.DeActiveAbility()
    end
end



HL.Commit(XiraniteNexusAimCtrl)