local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattleAction
local PlayerController = CS.Beyond.Gameplay.Core.PlayerController

BattleActionCtrl = HL.Class('BattleActionCtrl', uiCtrl.UICtrl)





BattleActionCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BATTLE_SQUAD_CHANGED] = 'OnBattleTeamChanged',
    [MessageConst.ON_CHARACTER_DEAD] = 'OnCharacterDead',
    [MessageConst.ON_RESET_LEVEL] = 'OnResetCharacters',
    [MessageConst.ON_SYSTEM_UNLOCK_CHANGED] = 'OnSystemUnlock',
    [MessageConst.ON_DEBUG_TOGGLE_SKILL_RECOVER_BUTTON] = 'OnDebugToggleSkillRecoverBtn',
    [MessageConst.ON_CLEAR_SKILLBTN_STATE] = 'OnClearSkillBtnState',
    [MessageConst.ON_PRESS_AND_RELEASE_SKILL_BUTTON] = 'OnPressAndReleaseSkillButton',
    [MessageConst.ON_CONTROLLER_INDICATOR_CHANGE] = 'OnToggleControllerSkillIndicator',
    [MessageConst.ON_LOCK_TARGET_CHANGED] = 'OnLockTargetChanged',
    [MessageConst.ON_SQUAD_INFIGHT_CHANGED] = 'OnInFightChanged',
    [MessageConst.AFTER_TOGGLE_UI_ACTION] = 'AfterToggleUiAction',
    [MessageConst.ON_SKILL_BUTTON_ACTIVE_CONFIG_CHANGED] = 'OnSkillButtonActiveConfigChanged',
    [MessageConst.ON_FORCE_HIDE_LOCK_AIM_UI_CHANGED] = 'OnForceHideLockAimUIChanged',
    [MessageConst.FORBID_SYSTEM_CHANGED] = 'OnForbidSystemChanged',
    [MessageConst.TOGGLE_DEBUG_SQUAD_ICON_BUFF_MODE] = 'OnToggleDebugSquadIconBuffMode',
    [MessageConst.ON_BLIGHT_MIASMA_AREA_ENTER] = 'OnBlightMiasmaAreaEnter',
    [MessageConst.ON_BLIGHT_MIASMA_AREA_EXIT] = 'OnBlightMiasmaAreaExit',
}

do
    BattleActionCtrl.m_pressScreen = HL.Field(HL.Function)

    BattleActionCtrl.m_releaseScreen = HL.Field(HL.Function)

    BattleActionCtrl.m_onLongPress = HL.Field(HL.Function)

    BattleActionCtrl.m_longPressScreen = HL.Field(HL.Boolean) << false

    BattleActionCtrl.m_selectedTarget = HL.Field(HL.Userdata)

    BattleActionCtrl.m_skillCellList = HL.Field(HL.Table)

    BattleActionCtrl.m_throwData = HL.Field(HL.Userdata)

    BattleActionCtrl.m_weakLockHint = HL.Field(HL.Table)

    BattleActionCtrl.m_enemyLockHint = HL.Field(HL.Table)

    BattleActionCtrl.m_skillIndicatorShowing = HL.Field(HL.Boolean) << false

    BattleActionCtrl.m_teamSkillUnlocked = HL.Field(HL.Boolean) << false

    BattleActionCtrl.m_onClickScreen = HL.Field(HL.Function)

    BattleActionCtrl.m_isNormalSkillUnlock = HL.Field(HL.Boolean) << false

    BattleActionCtrl.m_forbidLockTargetKeys = HL.Field(HL.Table)

    BattleActionCtrl.m_hideSkillNodeKeys = HL.Field(HL.Table)

    BattleActionCtrl.m_hideSkillShowNodeKeys = HL.Field(HL.Table)

    BattleActionCtrl.m_openedSubHudPanelId = HL.Field(HL.Table)

    BattleActionCtrl.m_gpuiBuffEnabled = HL.Field(HL.Boolean) << true
end

local SYSTEM_UNLOCK_KEY = "system_unlock"
local FORBID_SYSTEM_KEY = "forbid_system"
local THROW_MODE_KEY = "throw_mode"
local WATER_DRONE_MODE_KEY = "water_drone_mode"
local SKILL_BUTTON_INACTIVE_KEY = "skill_button_inactive"


BattleActionCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local isNormalSkillUnlock = Utils.isSystemUnlocked(GEnums.UnlockSystemType.NormalSkill)
    self.m_isNormalSkillUnlock = isNormalSkillUnlock
    if self.isDefaultPanel then
        self.m_onClickScreen = function(eventData)
            GameWorld.battle:OnClickScreen(eventData)
        end
        UIManager.commonTouchPanel.onClick:AddListener(self.m_onClickScreen)
        self.view.skillShowBtn.onClick:AddListener(function(args)
            self:OnClickSkillShowBtn(args)
        end)
    end
    self.view.aimBtn.onPressStart:AddListener(function()
        self.view.aimBtnAnim:PlayWithTween("skillbutton_aim_press")
        GameWorld.battle:ToggleLockTargetStart()
    end)
    self.view.aimBtn.onPressEnd:AddListener(function()
        GameWorld.battle:ToggleLockTargetEnd()
    end)

    self.m_teamSkillUnlocked = Utils.isSystemUnlocked(GEnums.UnlockSystemType.TeamSkill)

    self.m_skillCellList = {}
    for k = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do

        local skillCell = self.view["skillButton" .. k]
        skillCell:FirstTimeInit(CSIndex(k), self.isDefaultPanel, self.isControllerPanel)
        self.m_skillCellList[k] = skillCell
    end

    self.m_forbidLockTargetKeys = {}
    self.m_hideSkillNodeKeys = {}
    self.m_hideSkillShowNodeKeys = {}
    self.m_openedSubHudPanelId = {}

    self:_CreateWorldObjectRoot(true )
    self:_InitEnemyFootBar()
    self:_InitEnemyLockHint()
    self:RefreshSkills()

    self:OnToggleControllerSkillIndicator(false)

    self.view.atbNode:OnCreate()
    if self.view.buffIconSync ~= nil then
        self.view.hpNode:SetGPUIBuffSync(self.view.buffIconSync)
    end
    self.view.hpNode:SetGPUIBuffMode(self.m_gpuiBuffEnabled)

    if not isNormalSkillUnlock then
        self.m_hideSkillNodeKeys[SYSTEM_UNLOCK_KEY] = true
        self.m_hideSkillShowNodeKeys[SYSTEM_UNLOCK_KEY] = true
    end
    if not GameWorld.battle.skillButtonActive then
        self.m_hideSkillShowNodeKeys[SKILL_BUTTON_INACTIVE_KEY] = true
    end
    self:_RefreshSkillNodeState()
    if self.isDefaultPanel then
        self:_RefreshSkillShowNodeState()
    end
    self.view.atbNode.gameObject:SetActive(isNormalSkillUnlock)
    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.LockTarget) then
        self.m_forbidLockTargetKeys[SYSTEM_UNLOCK_KEY] = true
    end
    if Utils.isForbidden(ForbidType.ForbidLockTarget) then
        self.m_forbidLockTargetKeys[FORBID_SYSTEM_KEY] = true
    end
    self:_RefreshLockTargetState()
    self:_RefreshLockAimState()

    if BEYOND_DEBUG_COMMAND then
        self:BindInputPlayerAction("battle_debug_refresh_skill_usp", function()
            CS.Beyond.Gameplay.Core.PlayerController.DoRefreshSkill(true)
        end)
        self:BindInputPlayerAction("battle_debug_heal_all_char", function()
            CS.Beyond.Gameplay.Core.PlayerController.HealAllCharacters()
        end)
        self:BindInputPlayerAction("battle_debug_reload_all_skill", function()
            CS.Beyond.Gameplay.Core.PlayerController.ReloadBattleAssets()
        end)
        self:BindInputPlayerAction("battle_debug_kill_all_enemies", function()
            CS.Beyond.Gameplay.Core.PlayerController.KillAllEnemies(false)
        end)
        self:BindInputPlayerAction("battle_debug_kill_all_enemies_in_fight", function()
            CS.Beyond.Gameplay.Core.PlayerController.KillAllEnemies(true)
        end)
        self:BindInputPlayerAction("battle_debug_toggle_time_scale", function()
            CS.Beyond.Gameplay.Core.PlayerController.ToggleTimeScale()
        end)
    end

    if self.isControllerPanel then
        UIUtils.bindControllerCamZoom(self.view.camZoomKeyHint.groupId)
    end
end

BattleActionCtrl.OnShow = HL.Override() << function(self)
    for _, skillCell in ipairs(self.m_skillCellList) do
        skillCell.enabled = true
    end
    self:RefreshSkills()
    self.view.atbNode:CheckAtbLoopAnim()
    self:OnBattleTeamChanged()
    if CS.Beyond.UI.BattleControllerInputController.instance:GetControllerIndicatorState() then
        self:OnToggleControllerSkillIndicator(true)
    end
    if self.isDefaultPanel then
        self.view.actionsNodeFadeController:OnShow()
    end
    if self.isControllerPanel then
        self.view.hudFadeController:OnShow()
        if GameWorld.gameMechManager.blightMiasmaBrain.inBlightMiasmaArea then
            self:OnBlightMiasmaAreaEnter()
        else
            self:OnBlightMiasmaAreaExit()
        end
    end
    self.view.hpNode:OnShow()
    self:_RefreshLockAimState()


    local isInThrowMode = GameWorld.battle.inThrowMode

    local isInWaterDroneMode = GameWorld.battle.inWaterDroneMode
    if isInThrowMode then
        self.m_forbidLockTargetKeys[THROW_MODE_KEY] = true
    else
        self.m_forbidLockTargetKeys[THROW_MODE_KEY] = nil
    end
    if isInWaterDroneMode then
        self.m_forbidLockTargetKeys[WATER_DRONE_MODE_KEY] = true
    else
        self.m_forbidLockTargetKeys[WATER_DRONE_MODE_KEY] = nil
    end
    self:_RefreshLockTargetState()
    if isInThrowMode then
        self.m_hideSkillNodeKeys[THROW_MODE_KEY] = true
        self.m_hideSkillShowNodeKeys[THROW_MODE_KEY] = true
    else
        self.m_hideSkillNodeKeys[THROW_MODE_KEY] = nil
        self.m_hideSkillShowNodeKeys[THROW_MODE_KEY] = nil
    end
    if isInWaterDroneMode then
        self.m_hideSkillNodeKeys[WATER_DRONE_MODE_KEY] = true
    else
        self.m_hideSkillNodeKeys[WATER_DRONE_MODE_KEY] = nil
    end
    self:_RefreshSkillNodeState()
    if self.isDefaultPanel then
        self:_RefreshSkillShowNodeState()
    end
end

BattleActionCtrl.OnHide = HL.Override() << function(self)
    self:_ClearRegisters()
    self:_ClearAllSkillBtnClick()
    self:OnToggleControllerSkillIndicator(false)
    for _, skillCell in ipairs(self.m_skillCellList) do
        skillCell.enabled = false
    end
    self.view.hpNode:OnHide()
end


BattleActionCtrl.OnClose = HL.Override() << function(self)
    self:_ClearRegisters()
    self.view.atbNode:OnClose()

    for k, skillCell in ipairs(self.m_skillCellList) do
        skillCell:Close()
    end

    if self.m_weakLockHint then
        GameObject.Destroy(self.m_weakLockHint.gameObject)
    end
    self.m_weakLockHint = nil

    if self.m_enemyLockHint then
        GameObject.Destroy(self.m_enemyLockHint.gameObject)
    end
    self.m_enemyLockHint = nil
    if self.m_onClickScreen then
        UIManager.commonTouchPanel.onClick:RemoveListener(self.m_onClickScreen)
        self.m_onClickScreen = nil
    end
end

BattleActionCtrl.OnToggleDebugSquadIconBuffMode = HL.Method() << function(self)
    self.m_gpuiBuffEnabled = not self.m_gpuiBuffEnabled
    self.view.hpNode:SetGPUIBuffMode(self.m_gpuiBuffEnabled)
end

BattleActionCtrl._ClearRegisters = HL.Method() << function(self)
    local touchPanel = UIManager.commonTouchPanel
    if self.m_pressScreen then
        touchPanel.onPress:RemoveListener(self.m_pressScreen)
    end
    if self.m_releaseScreen then
        touchPanel.onRelease:RemoveListener(self.m_releaseScreen)
    end
end

do
    BattleActionCtrl._InitEnemyFootBar = HL.Method() << function(self)
        self.m_weakLockHint = Utils.wrapLuaNode(CSUtils.CreateObject(self.view.config.WEAK_LOCK_HINT, self.m_worldAutoRoot))
    end

    BattleActionCtrl._InitEnemyLockHint = HL.Method() << function(self)
        self.m_enemyLockHint = Utils.wrapLuaNode(CSUtils.CreateObject(self.view.config.ENEMY_LOCK_HINT, self.m_worldAutoRoot))
    end
end

do
    BattleActionCtrl.OnBattleTeamChanged = HL.Method() << function(self)
        self:RefreshSkills()
        self:OnBattleCenterChange()

        local squadSlots = GameInstance.player.squadManager.curSquad.slots
        for i = 1, squadSlots.Count do
            local entity = squadSlots[CSIndex(i)].character
            if entity ~= nil and entity.abilityCom.alive then
                local hudPanelId = entity.abilityCom.data.skillDataBundle.hudPanelName
                if not string.isEmpty(hudPanelId) then
                    local panelId = PanelId[hudPanelId]
                    if panelId and not self.m_openedSubHudPanelId[panelId] and not UIManager:IsOpen(panelId) then
                        local ctrl = UIManager:Open(panelId)
                        self.m_openedSubHudPanelId[panelId] = true
                        ctrl:Hide()
                    end
                end
            end
        end
    end

    BattleActionCtrl.OnBattleCenterChange = HL.Method() << function(self)
    end

    BattleActionCtrl.OnCharacterDead = HL.Method(HL.Table) << function(self, args)
        local csIndex = unpack(args)
        local luaIndex = LuaIndex(csIndex)
        self.m_skillCellList[luaIndex]:OnCharacterDie()
    end

    BattleActionCtrl.OnResetCharacters = HL.Method() << function(self)
        self:RefreshSkills()
    end
end

do
    BattleActionCtrl._ChangeThrowMode = HL.Method(HL.Table) << function(self, args)

        local data = unpack(args)
        GameWorld.battle:ForceResetLockTarget()
        if data.valid then
            self.m_forbidLockTargetKeys[THROW_MODE_KEY] = true
            self.m_hideSkillNodeKeys[THROW_MODE_KEY] = true
            self.m_hideSkillShowNodeKeys[THROW_MODE_KEY] = true
        else
            self.m_forbidLockTargetKeys[THROW_MODE_KEY] = nil
            self.m_hideSkillNodeKeys[THROW_MODE_KEY] = nil
            self.m_hideSkillShowNodeKeys[THROW_MODE_KEY] = nil
        end
        self:_RefreshLockTargetState()
        self:_RefreshSkillNodeState()
        if self.isDefaultPanel then
            self:_RefreshSkillShowNodeState()
        end
        self:RefreshSkills()
    end

    BattleActionCtrl.OnChangeThrowMode = HL.StaticMethod(HL.Table) << function(args)
        local isOpen, ctrl = UIManager:IsOpen(PanelId.BattleAction)
        if isOpen then

            ctrl:_ChangeThrowMode(args)
            return
        end

        local data = unpack(args)
        if data.valid then
            LuaSystemManager.factory:AddFactoryModeRequest({ false, "ThrowMode" })

            isOpen, ctrl = UIManager:IsOpen(PanelId.BattleAction)
            if not isOpen then

                return
            end
            ctrl:_ChangeThrowMode(args)
        else
            LuaSystemManager.factory:RemoveFactoryModeRequest("ThrowMode")
        end
    end

    BattleActionCtrl._ThrowByForceAndDir = HL.Method() << function(self)
        if self.m_throwData ~= nil then
            GameInstance.playerController.mainCharacter.interactiveInstigatorCtrl:CastThrowSkill()
        end
    end

    BattleActionCtrl.EnterWaterDroneMode = HL.StaticMethod() << function()
        local isOpen, ctrl = UIManager:IsOpen(PanelId.BattleAction)
        if isOpen then
            ctrl:_ChangeWaterDroneMode(true)
        end
    end

    BattleActionCtrl.ExitWaterDroneMode = HL.StaticMethod() << function()
        local isOpen, ctrl = UIManager:IsOpen(PanelId.BattleAction)
        if isOpen then
            ctrl:_ChangeWaterDroneMode(false)
        end
    end

    BattleActionCtrl._ChangeWaterDroneMode = HL.Method(HL.Boolean) << function(self, isEnter)
        GameWorld.battle:ForceResetLockTarget()
        if isEnter then
            self.m_forbidLockTargetKeys[WATER_DRONE_MODE_KEY] = true
            self.m_hideSkillNodeKeys[WATER_DRONE_MODE_KEY] = true
        else
            self.m_forbidLockTargetKeys[WATER_DRONE_MODE_KEY] = nil
            self.m_hideSkillNodeKeys[WATER_DRONE_MODE_KEY] = nil
        end
        self:_RefreshLockTargetState()
        self:_RefreshSkillNodeState()
        self:RefreshSkills()
    end
end

do
    BattleActionCtrl.RefreshSkills = HL.Method() << function(self)
        local curSquad = GameInstance.player.squadManager.curSquad
        local squadSlots = curSquad.slots
        for k, skillCell in ipairs(self.m_skillCellList) do
            if k > squadSlots.Count or (not self.m_teamSkillUnlocked and CSIndex(k) ~= curSquad.leaderIndex) then
                skillCell:SetEmpty(true)
            else
                skillCell:SetEmpty(false)
                skillCell:RefreshSkillButton()
            end
        end
        self:_ClearAllSkillBtnClick()
        if self.isControllerPanel then
            self.view.skillBgNode.gameObject:SetActive(GameWorld.battle.skillButtonActive)
        end
    end

    BattleActionCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
        if not active and self:IsShow() then
            self:_ClearAllSkillBtnClick()
            self:OnToggleControllerSkillIndicator(false)
            GameWorld.battle:ToggleLockTargetEnd()
        end
        if active and self.isControllerPanel then
            if self:IsShow() and CS.Beyond.UI.BattleControllerInputController.instance:GetControllerIndicatorState() then
                self:OnToggleControllerSkillIndicator(true)
            else

                for k, skillCell in ipairs(self.m_skillCellList) do
                    skillCell:ToggleControllerSkillIndicator(false)
                end
            end
        end
    end

    BattleActionCtrl.AfterToggleUiAction = HL.Method(HL.Table) << function(self, arg)
        self:_OnPanelInputBlocked(self.view.inputGroup.groupEnabled)
        local isShow, isUltimate = unpack(arg)
        if not isUltimate then
            return
        end
        if isShow then
            InputManagerInst:ChangeParent(true, self.view.skillInputGroup.groupId, self.view.inputGroup.groupId)
        else
            InputManagerInst:ChangeParent(true, self.view.skillInputGroup.groupId, InputManagerInst.rootGroupId)
        end
    end

    BattleActionCtrl.OnClearSkillBtnState = HL.Method() << function(self)
        self:_ClearAllSkillBtnClick()
    end

    BattleActionCtrl.OnPressAndReleaseSkillButton = HL.Method(HL.Table) << function(self, arg)
        if not self:IsShow() or not self.m_isNormalSkillUnlock then
            return
        end
        local luaIndex = LuaIndex(unpack(arg))
        local skillCell = self.m_skillCellList[luaIndex]
        if not skillCell.gameObject.activeSelf then
            return
        end
        skillCell:OnPressSkillStart()
        skillCell:OnPressSkillEnd()
    end

    BattleActionCtrl._ClearAllSkillBtnClick = HL.Method() << function(self)
        local squadSlots = GameInstance.player.squadManager.curSquad.slots
        for k, skillCell in ipairs(self.m_skillCellList) do
            if k <= squadSlots.Count then
                skillCell:ClearBtnClick()
            end
        end
    end

    BattleActionCtrl.OnDebugToggleSkillRecoverBtn = HL.Method() << function(self)

    end

    BattleActionCtrl.OnLockTargetChanged = HL.Method(HL.Table) << function(self, args)
        if self.isControllerPanel then
            return
        end
        local lockTarget = unpack(args)
        if lockTarget and not GameWorld.battle.isAutoLockTarget then
            self.view.normalAimBtnNode.gameObject:SetActive(false)
            self.view.lockAimBtnNode.gameObject:SetActive(true)
        else
            self.view.normalAimBtnNode.gameObject:SetActive(true)
            self.view.lockAimBtnNode.gameObject:SetActive(false)
        end
    end
end

BattleActionCtrl.OnInFightChanged = HL.Method(HL.Opt(HL.Table)) << function(self, args)

end

do

    BattleActionCtrl.m_blockArrowBtnsForControllerGroupId = HL.Field(HL.Number) << -1

    BattleActionCtrl.OnToggleControllerSkillIndicator = HL.Method(HL.Boolean) << function(self, active)
        if self.isControllerPanel then
            if active and not self:IsShow() then

                return
            end

            self.m_skillIndicatorShowing = active
            self.view.animator:SetBool("IndicatorActive", active)
            GameWorld.hudFadeManager:SetPreventFadeState(CS.Beyond.HudFadeType.ControllerIndicator, active)
            for k, skillCell in ipairs(self.m_skillCellList) do
                skillCell:ToggleControllerSkillIndicator(active)
            end
            if active then
                AudioManager.PostEvent("au_ui_menu_BattleSkillPanel_open")
                if self.m_isNormalSkillUnlock then
                    local charIndex = CS.Beyond.UI.BattleControllerInputController.instance:TryPressControllerIndicatorWhenSkillButtonJustPressed()
                    if charIndex >= 0 then
                        local skillCell = self.m_skillCellList[LuaIndex(charIndex)]
                        if skillCell.gameObject.activeSelf then
                            skillCell:OnPressSkillStart()
                        end
                    end
                end
            end



            if self.m_blockArrowBtnsForControllerGroupId == -1 then
                self.m_blockArrowBtnsForControllerGroupId = InputManagerInst:CreateGroup(self.view.inputGroup.groupId)
                self:BindInputPlayerAction("common_navigation_4_dir_arrow_up", function()
                end, self.m_blockArrowBtnsForControllerGroupId)
                self:BindInputPlayerAction("common_navigation_4_dir_arrow_down", function()
                end, self.m_blockArrowBtnsForControllerGroupId)
                self:BindInputPlayerAction("common_navigation_4_dir_arrow_left", function()
                end, self.m_blockArrowBtnsForControllerGroupId)
                self:BindInputPlayerAction("common_navigation_4_dir_arrow_right", function()
                end, self.m_blockArrowBtnsForControllerGroupId)
            end
            InputManagerInst:ToggleGroup(self.m_blockArrowBtnsForControllerGroupId, active)
        end
    end

    BattleActionCtrl.OnClickSkillShowBtn = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
        self.view.actionsNodeFadeController:InformShow()
    end

    BattleActionCtrl.OnForbidSystemChanged = HL.Method(HL.Table) << function(self, args)
        local forbidType, isForbidden = unpack(args)
        if forbidType == ForbidType.ForbidLockTarget then
            if isForbidden then
                self.m_forbidLockTargetKeys[FORBID_SYSTEM_KEY] = true
            else
                self.m_forbidLockTargetKeys[FORBID_SYSTEM_KEY] = nil
            end
            self:_RefreshLockTargetState()
        end
    end

    BattleActionCtrl._RefreshLockTargetState = HL.Method() << function(self)
        local isForbidden = next(self.m_forbidLockTargetKeys) ~= nil
        self.view.aimBtn.gameObject:SetActive(not isForbidden)
        if isForbidden then
            GameWorld.battle:ToggleLockTargetEnd()
        end
    end

    BattleActionCtrl._RefreshLockAimState = HL.Method() << function(self)
        if self.m_worldAutoRoot then
            self.m_worldAutoRoot.gameObject:SetActive(not GameWorld.battle.forceHideLockAimUI)
        end
    end

    BattleActionCtrl._RefreshSkillNodeState = HL.Method() << function(self)
        local isHidden = next(self.m_hideSkillNodeKeys) ~= nil
        self.view.skillNode.gameObject:SetActive(not isHidden)
    end

    BattleActionCtrl._RefreshSkillShowNodeState = HL.Method() << function(self)
        local isHidden = next(self.m_hideSkillShowNodeKeys) ~= nil
        self.view.skillShowNode.gameObject:SetActive(not isHidden)
    end

    BattleActionCtrl.OnBlightMiasmaAreaEnter = HL.Method() << function(self)
        if not self.isControllerPanel then
            return
        end
        self.view.camZoomKeyHintContent.alpha = 0
    end

    BattleActionCtrl.OnBlightMiasmaAreaExit = HL.Method() << function(self)
        if not self.isControllerPanel then
            return
        end
        self.view.camZoomKeyHintContent.alpha = 1
    end
end

do
    BattleActionCtrl.OnSystemUnlock = HL.Method(HL.Any) << function(self, arg)
        local systemIndex = unpack(arg)

        if systemIndex == GEnums.UnlockSystemType.NormalSkill:GetHashCode() then
            self.m_isNormalSkillUnlock = true
            self.m_hideSkillNodeKeys[SYSTEM_UNLOCK_KEY] = nil
            self:_RefreshSkillNodeState()
            self.view.atbNode.gameObject:SetActive(true)
            if self.isDefaultPanel then
                self.m_hideSkillShowNodeKeys[SYSTEM_UNLOCK_KEY] = nil
                self:_RefreshSkillShowNodeState()
            end
        end


        if systemIndex == GEnums.UnlockSystemType.UltimateSkill:GetHashCode() then
            self:RefreshSkills()
        end


        if systemIndex == GEnums.UnlockSystemType.TeamSkill:GetHashCode() then
            self.m_teamSkillUnlocked = true
            self:RefreshSkills()
        end


        if systemIndex == GEnums.UnlockSystemType.LockTarget:GetHashCode() then
            self.m_forbidLockTargetKeys[SYSTEM_UNLOCK_KEY] = nil
            self:_RefreshLockTargetState()
        end
    end

    BattleActionCtrl.OnSkillButtonActiveConfigChanged = HL.Method() << function(self)
        if self.isControllerPanel then
            self.view.skillBgNode.gameObject:SetActive(GameWorld.battle.skillButtonActive)
        end
        if self.isDefaultPanel then
            if GameWorld.battle.skillButtonActive then
                self.m_hideSkillShowNodeKeys[SKILL_BUTTON_INACTIVE_KEY] = nil
            else
                self.m_hideSkillShowNodeKeys[SKILL_BUTTON_INACTIVE_KEY] = true
            end
            self:_RefreshSkillShowNodeState()
        end
    end

    BattleActionCtrl.OnForceHideLockAimUIChanged = HL.Method(HL.Any) << function(self, arg)
        self:_RefreshLockAimState()
    end
end

HL.Commit(BattleActionCtrl)
