
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BombAim
BombAimCtrl = HL.Class('BombAimCtrl', uiCtrl.UICtrl)

BombAimCtrl.m_isInitAim = HL.Field(HL.Boolean) << false

BombAimCtrl.m_isHit = HL.Field(HL.Boolean) << false

BombAimCtrl.m_controllerTriggerSettingHandlerId = HL.Field(HL.Number) << -1

BombAimCtrl.m_isControllerTriggerUsingVibration = HL.Field(HL.Boolean) << false






BombAimCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SYNC_AIM_POS] = '_OnSyncAimPos',
    [MessageConst.HIDE_BOMB_AIM] = '_OnHideBombAim',
    [MessageConst.ON_CHANGE_INPUT_DEVICE_TYPE_FINISHED] = '_OnChangeInputDeviceTypeFinished',
}


BombAimCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:BindInputPlayerAction("common_cancel_alter", function()
        self:_OnCancel()
    end)

    self:BindInputPlayerAction("battle_throw_mode_throw_show_only", function()
        if GameWorld.battle.inThrowMode and not InputManager.cursorVisible then
            GameInstance.playerController.mainCharacter.interactiveInstigatorCtrl:CastThrowSkill()
        end
    end)

    self.view.closeBtn.onClick:AddListener(function()
        self:_OnCancel()
    end)
end

BombAimCtrl.OnShow = HL.Override() << function(self)
    if DeviceInfo.usingController then
        self:_ToggleControllerTriggerSetting(true)
    end
end

BombAimCtrl.OnHide = HL.Override() << function(self)
    if DeviceInfo.usingController then
        self:_ToggleControllerTriggerSetting(false)
    end
end

BombAimCtrl._OnShowBombAim = HL.StaticMethod() << function()
    local bombAimPanel = UIManager:AutoOpen(PANEL_ID)
    bombAimPanel.m_isInitAim = false;
    bombAimPanel.view.animationWrapper:PlayWithTween("bombaimfar_in")
    Notify(MessageConst.GENERAL_ABILITY_CHANGE_KEY_BINDING, {true, "Bomb"})
    if DeviceInfo.usingController then
        bombAimPanel:_ToggleControllerTriggerSetting(true)
    end
end

BombAimCtrl._OnHideBombAim = HL.Method() << function(self)
    self:Hide()
    self.m_isHit = false
    Notify(MessageConst.GENERAL_ABILITY_CHANGE_KEY_BINDING, {false, "Bomb"})
    self:_ToggleControllerTriggerSetting(false)

    GameInstance.player.forbidSystem:SetForbid(ForbidType.ForbidMainHudTopBtns, "Bomb", false)
end

BombAimCtrl._OnSyncAimPos = HL.Method(HL.Any) << function(self, args)
    local pos, isHit = unpack(args)
    if isHit ~= self.m_isHit or self.m_isInitAim == false then
        self.m_isInitAim = true;
        self.m_isHit = isHit
        if self.m_isHit then
            self.view.animationWrapper:PlayWithTween("bombaimfar_change", function()
                self.view.animationWrapper:PlayWithTween("bombaim_loop")
            end)
        else
            self.view.animationWrapper:PlayWithTween("bombaim_change", function()
                self.view.animationWrapper:PlayWithTween("bombaimfar_loop")
            end)
        end
    end
    local uiPos = UIUtils.objectPosToUI(pos, self.uiCamera, self.view.transform)
    self.view.aimImage.anchoredPosition = uiPos
    self.view.aimImageFar.anchoredPosition = uiPos
end

BombAimCtrl._ToggleControllerTriggerSetting = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active)
    local oldHandlerId = self.m_controllerTriggerSettingHandlerId
    self.m_isControllerTriggerUsingVibration = active
    if active then
        local cmd = CS.Plugins.LibScePad.TriggerEffectCommandUnion()
        cmd.mask = CS.Plugins.LibScePad.TriggerMask.R2
        cmd.mode = CS.Plugins.LibScePad.ScePadTriggerEffectMode.SCE_PAD_TRIGGER_EFFECT_MODE_MULTIPLE_POSITION_FEEDBACK
        local vibration = CS.Plugins.LibScePad.TriggerMultiPositionFeedbackEffect()
        vibration.strength0 = 6
        vibration.strength1 = 7
        vibration.strength2 = 8
        vibration.strength3 = 6
        vibration.strength4 = 3
        vibration.strength5 = 2
        vibration.strength6 = 1
        vibration.strength7 = 1
        vibration.strength8 = 0
        vibration.strength9 = 0
        cmd.multiPositionFeedback = vibration
        self.m_controllerTriggerSettingHandlerId = GameInstance.audioManager.gamePad.scePad:SetTriggerEffect(cmd)
    else
        self.m_controllerTriggerSettingHandlerId = -1
    end

    if oldHandlerId >= 0 then
        GameInstance.audioManager.gamePad.scePad:EndTriggerEffect(oldHandlerId)
    end
end

BombAimCtrl._OnChangeInputDeviceTypeFinished = HL.Method(HL.Table) << function(self, args)
    if not self:IsShow() then
        return
    end

    local inputType = args.inputType
    Notify(MessageConst.GENERAL_ABILITY_CHANGE_KEY_BINDING, {true, "Bomb"})
    if inputType == DeviceInfo.InputType.Controller then
        self:_ToggleControllerTriggerSetting(true)
        UIManager:HideWithKey(PanelId.BattleAction, "Bomb")
        GameInstance.player.forbidSystem:SetForbid(ForbidType.ForbidMainHudTopBtns, "Bomb", true)
    else
        self:_ToggleControllerTriggerSetting(false)
        UIManager:ShowWithKey(PanelId.BattleAction, "Bomb")
        GameInstance.player.forbidSystem:SetForbid(ForbidType.ForbidMainHudTopBtns, "Bomb", false)
    end
end

BombAimCtrl._OnCancel = HL.Method() << function(self)
    if GameInstance.playerController.mainCharacter == nil then
        return
    end
    self:_ToggleControllerTriggerSetting(false)
    GameInstance.playerController.mainCharacter.interactiveInstigatorCtrl:ClearPickupItem()
end

BombAimCtrl.OnClose = HL.Override() << function(self)
    self:_ToggleControllerTriggerSetting(false)
    self:_OnCancel()

    GameInstance.player.forbidSystem:SetForbid(ForbidType.ForbidMainHudTopBtns, "Bomb", false)
end

HL.Commit(BombAimCtrl)
