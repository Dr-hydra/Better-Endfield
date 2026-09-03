local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

AttackButton = HL.Class('AttackButton', UIWidgetBase)


AttackButton.root = HL.Field(HL.Userdata)

AttackButton.m_isShowing = HL.Field(HL.Boolean) << false

AttackButton.m_hasBindInput = HL.Field(HL.Boolean) << false

AttackButton.m_pressScreen = HL.Field(HL.Function)

AttackButton.m_releaseScreen = HL.Field(HL.Function)

AttackButton.m_iconCache = HL.Field(HL.Table)

AttackButton.m_forbidAttackKeys = HL.Field(HL.Table)

AttackButton.m_castingAttack = HL.Field(HL.Boolean) << false

AttackButton.m_isBreakingAttack = HL.Field(HL.Boolean) << false

AttackButton.m_isBreakingAttackPressPlaying = HL.Field(HL.Boolean) << false


AttackButton._OnFirstTimeInit = HL.Override() << function(self)
    self.root = self:GetUICtrl()
    self.m_isShowing = false
    self.m_forbidAttackKeys = {}
    self.m_iconCache = {}

    self:RegisterMessage(MessageConst.ON_CHANGE_THROW_MODE, function(args)
        self:_RefreshAttackIcon()
    end)
    self:RegisterMessage(MessageConst.TOGGLE_FORBID_ATTACK, function(args)
        self:ToggleForbidAttack(args)
    end, true)
    self:RegisterMessage(MessageConst.ON_APPLICATION_FOCUS, function(args)
        self:OnApplicationFocus(args)
    end)
    self:RegisterMessage(MessageConst.ON_SYSTEM_UNLOCK_CHANGED, function(args)
        self:OnSystemUnlock(args)
    end, true)
    self:RegisterMessage(MessageConst.ON_MAIN_CHARACTER_CHANGE, function(args)
        self:_RefreshAttackIcon()
    end)
    self:RegisterMessage(MessageConst.ON_BREAKING_TARGET_CHANGED, function(args)
        self:_RefreshAttackIcon()
    end)
    self:RegisterMessage(MessageConst.ON_NET_MASK_CHANGED, function(args)
        local showMask = unpack(args)
        if showMask then
            self:ReleaseNormalAttackBtn()
        end
    end)
    self:RegisterMessage(MessageConst.FORBID_SYSTEM_CHANGED, function(args)
        local forbidType, isForbid = unpack(args)
        if forbidType == ForbidType.ForbidAttack then
            self:ToggleForbidAttack({"ForbidSystem", isForbid})
        end
    end, true)

    if DeviceInfo.usingTouch then
        self.view.button.onPressStart:AddListener(function()
            self:StartPressAttackBtn()
        end)

        self.view.button.onPressEnd:AddListener(function()
            if Utils.isInThrowMode() then
                self:_ThrowByForceAndDir()


            else
                self:ReleaseNormalAttackBtn()
            end
        end)
    end

    self.view.finishKillAttackNode.gameObject:SetActive(false)
    self:ToggleForbidAttack({"Unlock", not Utils.isSystemUnlocked(GEnums.UnlockSystemType.NormalAttack)})
    self:ToggleForbidAttack({"GameMode", GameInstance.mode.forbidAttack})
    self:ToggleForbidAttack({"ForbidSystem", GameInstance.player.forbidSystem:IsForbidden(ForbidType.ForbidAttack)})
end

AttackButton.InitAttackButton = HL.Method() << function(self)
    self:_FirstTimeInit()
end

AttackButton.OnShow = HL.Method() << function(self)
    self:_RefreshShowing()
    self:_RefreshAttackIcon()

    self.view.anim:SampleClipAtPercent("mobile_attackbtn_pressed", 0)
    self.view.attackPressedRing:SampleClipAtPercent("mobile_attackbtn_pressedring", 0)
end

AttackButton.OnHide = HL.Method() << function(self)
    self:_RefreshShowing()
    self:ReleaseNormalAttackBtn()
    if self.m_isBreakingAttackPressPlaying then
        self.view.finishKillAttackNodeAnimationWrapper:SampleClipAtPercent("mobile_finshkillattack_pressed", 0)
        self.m_isBreakingAttackPressPlaying = false
    end
end

AttackButton._OnDestroy = HL.Override() << function(self)
    local touchPanel = UIManager.commonTouchPanel
    if self.m_pressScreen then
        touchPanel.onPress:RemoveListener(self.m_pressScreen)
    end
    if self.m_releaseScreen then
        touchPanel.onRelease:RemoveListener(self.m_releaseScreen)
    end
end

AttackButton._RefreshShowing = HL.Method() << function(self)
    local showing = self.root:IsShow() and self.view.gameObject.activeSelf
    if self.m_isShowing == showing then
        return
    end
    self.m_isShowing = showing
    if showing then

        local touchPanel = UIManager.commonTouchPanel
        self.m_pressScreen = function()
            if not DeviceInfo.usingTouch then
                if InputManagerInst.isDebugForceShow then


                    return
                end
                if not self.view.gameObject.activeSelf then
                    return
                end
                self:StartPressAttackBtn()
            end
        end
        self.m_releaseScreen = function()
            if not DeviceInfo.usingTouch then
                if InputManagerInst.isDebugForceShow then


                    return
                end
                if Utils.isInThrowMode() then
                    self:_ThrowByForceAndDir()


                else
                    if InputManagerInst.isDebugForceShow then
                        return
                    end
                    self:ReleaseNormalAttackBtn()
                end
            end
        end
        touchPanel.onPress:AddListener(self.m_pressScreen)
        touchPanel.onRelease:AddListener(self.m_releaseScreen)

        self:BindNormalAttackInputEvent()
    else
        local touchPanel = UIManager.commonTouchPanel
        if self.m_pressScreen then
            touchPanel.onPress:RemoveListener(self.m_pressScreen)
        end
        if self.m_releaseScreen then
            touchPanel.onRelease:RemoveListener(self.m_releaseScreen)
        end
    end
end

AttackButton._ThrowByForceAndDir = HL.Method() << function(self)
    self.view.anim:PlayWithTween("mobile_attackbtn_release")

    if GameWorld.battle.inThrowMode then
        GameInstance.playerController.mainCharacter.interactiveInstigatorCtrl:CastThrowSkill()
    end
end

AttackButton._ExecuteCustomAbility = HL.Method() << function(self)
    self.view.anim:PlayWithTween("mobile_attackbtn_release")
    GameInstance.playerController.mainCharacter.customAbilityCom:UseAbility()
end

AttackButton.BindNormalAttackInputEvent = HL.Method() << function(self)
    if self.m_hasBindInput then
        return
    end
    self.m_hasBindInput = true

    self.root:BindInputPlayerAction("battle_attack_start", function()
        if UNITY_EDITOR and DeviceInfo.usingTouch then
            return
        end
        if not InputManager.cursorVisible then

            self:StartPressAttackBtn()
        end
    end)

    self.root:BindInputPlayerAction("battle_attack_end", function()
        if UNITY_EDITOR and DeviceInfo.usingTouch then
            return
        end



        self:ReleaseNormalAttackBtn()
    end)

    if UNITY_EDITOR then
        self.root:BindInputPlayerAction("battle_debug_attack_start", function()
            self:StartPressAttackBtn()
        end)

        self.root:BindInputPlayerAction("battle_debug_attack_end", function()
            self:ReleaseNormalAttackBtn()
        end)
    end
end

AttackButton.StartPressAttackBtn = HL.Method() << function(self)
    if Utils.isInThrowMode() then
        self.view.anim:PlayWithTween("mobile_attackbtn_pressed")
        return
    end

    if Utils.isInCustomAbility() then
        self:_ExecuteCustomAbility();
        self.view.anim:PlayWithTween("mobile_attackbtn_pressed")
        return
    end

    if not GameInstance.playerController.canPressAttackButton then
        return
    end
    if not self.view.gameObject.activeSelf then
        return
    end
    self.view.anim:PlayWithTween("mobile_attackbtn_pressed")
    self.view.attackPressedRing:PlayWithTween("mobile_attackbtn_pressedring")
    if self.m_isBreakingAttack then
        self.m_isBreakingAttackPressPlaying = true
        self.view.finishKillAttackNodeAnimationWrapper:PlayWithTween("mobile_finshkillattack_pressed", function()
            self.m_isBreakingAttackPressPlaying = false
            self:_RefreshAttackIcon()
        end)
    end
    self.m_castingAttack = true
    GameInstance.playerController:StartCastNormalAttack()
end

AttackButton.ReleaseNormalAttackBtn = HL.Method() << function(self)
    if Utils.isInCustomAbility() then
        GameInstance.playerController.mainCharacter.customAbilityCom:StopAbility();
        self.view.anim:PlayWithTween("mobile_attackbtn_release")
        return
    end
    if self.m_castingAttack then
        GameInstance.playerController:EndCastNormalAttack()
        self.view.anim:PlayWithTween("mobile_attackbtn_release")
        self.m_castingAttack = false
    end
end

AttackButton.ToggleControllerIndicator = HL.Method(HL.Boolean) << function(self, active)
    GameInstance.playerController:ToggleIndicatorAttack(active)
end

AttackButton.OnApplicationFocus = HL.Method(HL.Table) << function(self, args)
    local hasFocus = unpack(args)
    if not hasFocus then
        self:ReleaseNormalAttackBtn()
    end
end

local weaponNumToConfigIcon = {
    "ICON_ATTACK_SWORD",
    "ICON_ATTACK_WAND",
    "ICON_ATTACK_CLAYM",
    "",
    "ICON_ATTACK_LANCE",
    "ICON_ATTACK_PISTOL",
}

AttackButton._RefreshAttackIcon = HL.Method() << function(self)
    if not DeviceInfo.usingTouch then
        return
    end
    if next(self.m_forbidAttackKeys) then
        self.view.gameObject:SetActiveIfNecessary(false)
        self:ReleaseNormalAttackBtn()
    else
        self.view.gameObject:SetActiveIfNecessary(true)
    end
    local iconName
    local inThrowMode = GameWorld.battle.inThrowMode
    self.m_isBreakingAttack = false
    if inThrowMode then
        iconName = "ICON_THROW"
        if GameInstance.player.forbidSystem:IsForbidden(ForbidType.ForbidAttack) and DeviceInfo.usingTouch then
            self.view.gameObject:SetActiveIfNecessary(true)
        end
    else
        if GameWorld.battle.lastCanBeBreakingAttackTarget ~= nil then
            iconName = "ICON_ATTACK_BREAKING"
            self.m_isBreakingAttack = true
        else
            local mainChar = GameInstance.playerController.mainCharacter
            local templateId = mainChar.templateData.id
            local charWeaponTypeNum = Tables.characterTable:GetValue(templateId).weaponType:GetHashCode()
            iconName = weaponNumToConfigIcon[charWeaponTypeNum]
        end
    end
    self:_RefreshShowing()

    local sprite = self.m_iconCache[iconName]
    if sprite == nil then
        sprite = self:LoadSprite(UIConst.UI_SPRITE_SKILL_ICON, self.config[iconName])
        self.m_iconCache[iconName] = sprite
    end
    self.view.icon.sprite = sprite
    if self.m_isBreakingAttack or self.m_isBreakingAttackPressPlaying then
        self.view.finishKillAttackNode.gameObject:SetActive(true)
        self.view.finishKillAttackNodeCanvasGroup.alpha = 1
    else
        self.view.finishKillAttackNode.gameObject:SetActive(false)
    end
end

AttackButton.OnSystemUnlock = HL.Method(HL.Table) << function(self, args)
    local system = unpack(args)
    system = GEnums.UnlockSystemType.__CastFrom(system)
    if system == GEnums.UnlockSystemType.NormalAttack then
        self:ToggleForbidAttack({"Unlock", false})
    end
end

AttackButton.ToggleForbidAttack = HL.Method(HL.Table) << function(self, args)
    local reason, forbid = unpack(args)
    if forbid then
        self.m_forbidAttackKeys[reason] = true
    else
        self.m_forbidAttackKeys[reason] = nil
    end

    if next(self.m_forbidAttackKeys) then
        self.view.gameObject:SetActive(false)
        self:ReleaseNormalAttackBtn()
    else
        self.view.gameObject:SetActive(true)
    end
    self:_RefreshShowing()
end

HL.Commit(AttackButton)
return AttackButton
