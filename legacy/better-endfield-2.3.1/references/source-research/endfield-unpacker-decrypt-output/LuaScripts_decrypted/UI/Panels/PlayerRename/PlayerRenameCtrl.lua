local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.PlayerRename
local PHASE_ID = PhaseId.PlayerRename
local RENAME_PS_DEBUG = false
local State = {
    Default = 0,
    SecondCheck = 1,
}
local Type = {
    SetName = 0,
    ResetName = 1,
}

local function UsingVirtualKeyboard()
    return DeviceInfo.isMobile or CS.Beyond.CloudGame.enabled
end
PlayerRenameCtrl = HL.Class('PlayerRenameCtrl', uiCtrl.UICtrl)





PlayerRenameCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CHECK_PLAYER_NAME_SUCCESS] = '_OnNameCheckSuccess',
    [MessageConst.ON_CHECK_PLAYER_NAME_FAILED] = '_OnNameCheckFailed',
    [MessageConst.ON_SET_PLAYER_NAME] = '_OnNameSetSuccess',
}

PlayerRenameCtrl.m_input = HL.Field(HL.String) << ""

PlayerRenameCtrl.m_isValid = HL.Field(HL.Boolean) << true

PlayerRenameCtrl.m_checked = HL.Field(HL.Boolean) << false

PlayerRenameCtrl.m_state = HL.Field(HL.Number) << 0

PlayerRenameCtrl.m_type = HL.Field(HL.Any) << Type.SetName

PlayerRenameCtrl.m_select = HL.Field(HL.Boolean) << true

PlayerRenameCtrl.m_onFinish = HL.Field(HL.Any)

PlayerRenameCtrl.m_inited = HL.Field(HL.Boolean) << false

PlayerRenameCtrl.m_caret = HL.Field(HL.Any)

PlayerRenameCtrl.m_tailTickId = HL.Field(HL.Number) << -1

PlayerRenameCtrl.m_itemId = HL.Field(HL.String) << ""

PlayerRenameCtrl.m_instId = HL.Field(HL.Number) << 0

PlayerRenameCtrl.m_updateThread = HL.Field(HL.Thread)

PlayerRenameCtrl.OnSetPlayerNameStart = HL.StaticMethod(HL.Table) << function(arg)
    local onFinish = unpack(arg)
    local openArg = {}
    openArg.onFinish = onFinish
    openArg.type = Type.SetName
    PhaseManager:OpenPhaseFast(PHASE_ID, openArg)
end

PlayerRenameCtrl.OnResetPlayerNameStart = HL.StaticMethod(HL.Table) << function(arg)
    local onFinish, itemId, instId = unpack(arg)
    local openArg = {}
    openArg.onFinish = onFinish
    openArg.type = Type.ResetName
    openArg.itemId = itemId
    openArg.instId = instId
    PhaseManager:OpenPhaseFast(PHASE_ID, openArg)
end


PlayerRenameCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local onFinish = arg.onFinish
    self.m_onFinish = onFinish
    self.m_type = arg.type
    self.m_itemId = arg.itemId or ""
    self.m_instId = arg.instId or 0
    self.m_caret = nil

    self.view.userRoleInputField.characterLimit = UIConst.INPUT_FIELD_PLAYER_NAME_CHARACTER_LIMIT
    self.view.userRoleInputField.onValidateCharacterLimit = I18nUtils.GetRealTextByLengthLimit
    self.view.userRoleInputField.onGetTextLength = I18nUtils.GetTextRealLength
    self.view.userRoleInputField.onValueChanged:AddListener(function(text)
        self:_OnValueChanged(text)
    end)

    self.view.userRoleInputField.caretWidth = 0

    self.view.userRoleInputField.onSelect:AddListener(function(_)
        self.m_select = true
        self:_RefreshInputField()
    end)

    if DeviceInfo.usingTouch then

        self.view.userRoleInputField.onDeselect:AddListener(function(_)
            self.m_select = false
            self:_RefreshInputField()
        end)
    end

    if self.view.renameTipsTextKeyBoard ~= nil then
        local isPCController = DeviceInfo.usingController and DeviceInfo.isPCUser
        self.view.renameTipsTextKeyBoard.gameObject:SetActive(isPCController)
    end

    self.view.sureBtn.onClick:AddListener(function()
        self:_OnSureBtnClicked()
    end)

    self.view.cancelBtn.onClick:AddListener(function()
        self:_OnCancelBtnClicked()
    end)

    self.view.againBtn.onClick:AddListener(function()
        if UNITY_PS4 or UNITY_PS5 or RENAME_PS_DEBUG or (UsingVirtualKeyboard() and DeviceInfo.usingController) then
            self:_SwitchState(State.Default)
            self:_DeselectInputField()
            self:_ToggleSelectBtn(true)
            self:_RefreshInputField()
        else
            self:_SwitchState(State.Default)
            self:_SelectToEnd(true)
            self:_RefreshInputField()
        end
    end)

    if UNITY_PS4 or UNITY_PS5 or RENAME_PS_DEBUG or (UsingVirtualKeyboard() and DeviceInfo.usingController) then
        self.view.userRoleInputField.onEndEdit:AddListener(function()
            self.view.keyHintPS.gameObject:SetActive(true)
            self.view.invisibleSelectBtn.gameObject:SetActive(false)
            self.view.invisibleCancelBtn.gameObject:SetActive(false)
            self.view.controllerHintPlaceholder.gameObject:SetActive(false)

            self:_DeselectInputField()
        end)
    end

    self.view.sureSecondBtn.onClick:AddListener(function()
        if self.m_type == Type.ResetName then
            GameInstance.player.inventory:UseRenameCard(
                self.m_itemId, self.m_instId, self.m_input)
        else
            GameInstance.player.playerInfoSystem:SetPlayerName(self.m_input)
        end
    end)

    if UNITY_PS4 or UNITY_PS5 or RENAME_PS_DEBUG or (UsingVirtualKeyboard() and DeviceInfo.usingController) then
        self.view.invisibleCancelBtn.onClick:AddListener(function()
            self:_DeselectInputField()
        end)

        self.view.invisibleSelectBtn.onClick:AddListener(function()
            self:_SelectToEnd()
            self:_RefreshInputField()
        end)
    end

    self.view.touchBtn.onClick:AddListener(function()
        self:_SelectToEnd()
        self:_RefreshInputField()
    end)




    self.view.mobileRenameBtn.onClick:AddListener(function()
        self:_SelectToEnd(true)
        self:_RefreshInputField()
    end)

    self.m_tailTickId = LuaUpdate:Add("TailTick", function(deltaTime)
        self:TailTick(deltaTime)
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

PlayerRenameCtrl._ToggleSelectBtn = HL.Method(HL.Boolean) << function(self, canSelect)
    if UNITY_PS4 or UNITY_PS5 or RENAME_PS_DEBUG or (UsingVirtualKeyboard() and DeviceInfo.usingController) then
        self.view.keyHintPS.gameObject:SetActive(canSelect)
        self.view.invisibleSelectBtn.gameObject:SetActive(canSelect)
        self.view.invisibleCancelBtn.gameObject:SetActive(not canSelect)
        self.view.controllerHintPlaceholder.gameObject:SetActive(not canSelect)
    end
end

PlayerRenameCtrl._SelectToEnd = HL.Method(HL.Opt(HL.Boolean)) << function(self, force)
    self.m_select = true
    self.view.userRoleInputField.enabled = true
    self.view.userRoleInputField:Select()
    self.view.userRoleInputField:ActivateInputField()
    self.view.userRoleInputField.caretPosition = UIUtils.getStringLength(self.view.userRoleInputField.text)
    self.view.userRoleInputField.selectionAnchorPosition = self.view.userRoleInputField.caretPosition
    self.view.userRoleInputField.selectionFocusPosition = UIUtils.getStringLength(self.view.userRoleInputField.text)
    self:_ToggleSelectBtn(false)
    self:_RefreshInputField()
end

PlayerRenameCtrl._DeselectInputField = HL.Method() << function(self)
    self.m_select = false
    self.view.userRoleInputField:DeactivateInputField(true)
    self.view.userRoleInputField.enabled = false

    self:_ToggleSelectBtn(true)
    self:_RefreshInputField()
end

PlayerRenameCtrl.TailTick = HL.Method(HL.Number) << function(self, deltaTime)
    self:_RefreshCaret()
end

PlayerRenameCtrl.OnShow = HL.Override() << function(self)
    self.view.keyHintPS.gameObject:SetActive(false)
    self.view.invisibleSelectBtn.gameObject:SetActive(false)
    self.view.invisibleCancelBtn.gameObject:SetActive(false)
    self.view.controllerHintPlaceholder.gameObject:SetActive(false)

    self.m_select = false
    if DeviceInfo.inputType == DeviceInfo.InputType.Keyboard then
        if UsingVirtualKeyboard() then
            self:_ToggleSelectBtn(true)
            self:_DeselectInputField()
        else
            self:_SelectToEnd(true)
        end
    elseif DeviceInfo.inputType == DeviceInfo.InputType.Controller then
        if UNITY_PS4 or UNITY_PS5 or RENAME_PS_DEBUG or (UsingVirtualKeyboard() and DeviceInfo.usingController) then

            self:_ToggleSelectBtn(true)
            self:_DeselectInputField()
        else
            self:_SelectToEnd(true)
        end
    end

    self:_SwitchState(State.Default)
    self:_RefreshInputField()

    self.view.idPlayerTxt.text = string.format("UID: %s", GameInstance.player.playerInfoSystem.roleId)
    self:_RefreshHint()

    if not UsingVirtualKeyboard() then
        self.m_updateThread = self:_StartCoroutine(function()
            while true do
                coroutine.wait(UIConst.UI_PLAYER_RENAME_UPDATE_INTERVAL)
                if self.m_select then
                    self:_SelectToEnd()
                else
                    self:_RefreshInputField()
                end
            end
        end)
    else
        self.view.userRoleInputField.onDeselect:AddListener(function(_)
            self.m_select = false
            self:_RefreshInputField()
        end)

    end
end

PlayerRenameCtrl.OnHide = HL.Override() << function(self)
    if self.m_updateThread then
        self.m_updateThread = self:_ClearCoroutine(self.m_updateThread)
    end
end

PlayerRenameCtrl.OnClose = HL.Override() << function(self)
    self.m_tailTickId = LuaUpdate:Remove(self.m_tailTickId)
end

PlayerRenameCtrl._RefreshInputField = HL.Method() << function(self)
    local default = self.m_state == State.Default
    self.view.blockNode.gameObject:SetActive(not default)
    self.view.caretLightImage.gameObject:SetActive(default and self.m_select)

    local needActive = string.isEmpty(self.m_input) and not self.m_select
    self.view.mobileRenameText.gameObject:SetActive(needActive)
end

PlayerRenameCtrl._OnSureBtnClicked = HL.Method() << function(self)
    local count = UIUtils.getStringLength(self.m_input)
    if self.m_isValid and count > 0 then
        GameInstance.player.playerInfoSystem:CheckPlayerName(self.m_input, self.m_type == Type.ResetName)
    end
end

PlayerRenameCtrl._OnCancelBtnClicked = HL.Method() << function(self)
    self:_ExitAndNotifyRenameResult(Language.LUA_RENAME_CANCEL_TOAST)
end

PlayerRenameCtrl._ExitAndNotifyRenameResult = HL.Method(HL.String) << function(self, toastText)
    AudioAdapter.PostEvent("Au_UI_Event_PlayerRename_End")
    self:PlayAnimationOutWithCallback(function()

        PhaseManager:ExitPhaseFast(PHASE_ID)
        local onFinish = self.m_onFinish
        if onFinish then
            onFinish()
        end
        if self.m_type == Type.ResetName and toastText then
            Notify(MessageConst.SHOW_TOAST, toastText)
        end
    end)
end

PlayerRenameCtrl._RefreshHint = HL.Method() << function(self)
    self.m_isValid = UIUtils.checkInputValid(self.m_input)
    local count = UIUtils.getStringLength(self.m_input)
    local gray = not self.m_isValid or count <= 0
    self.view.normalSureImage.gameObject:SetActive(gray)
    self.view.selectSureImg.gameObject:SetActive(not gray)

    UIUtils.PlayAnimationAndToggleActive(self.view.warnNode, not self.m_isValid)
    self.view.renameTipsTxt.gameObject:SetActive(self.m_isValid)
end

PlayerRenameCtrl._RefreshCaret = HL.Method() << function(self)

    if not self.m_caret then
        self.m_caret = self.view.userRoleInputField.transform:FindRecursive("Caret")
    end

    if DeviceInfo.usingKeyboard then
        if not self.m_inited then
            if self.m_caret and self.m_caret.gameObject.activeSelf then
                self.m_caret.gameObject:SetActive(false)
                self.m_inited = true
            end
        end
    end
end

PlayerRenameCtrl._RefreshMobileView = HL.Method() << function(self)

end

PlayerRenameCtrl._SwitchState = HL.Method(HL.Number) << function(self, state)
    local default = state == State.Default
    self.m_state = state
    self.view.renameTipsTxt.gameObject:SetActive(default)
    self.view.selectTipsTxt.gameObject:SetActive(not default)
    self.view.sureBtn.gameObject:SetActive(default)
    self.view.sureSecondBtn.gameObject:SetActive(not default)
    self.view.againBtn.gameObject:SetActive(not default)
    self.view.touchBtn.gameObject:SetActive(default)
    self.view.bgNode.gameObject:SetActive(default)

    self.view.cancelBtn.gameObject:SetActive(self.m_type == Type.ResetName and default)
end

PlayerRenameCtrl._OnValueChanged = HL.Method(HL.String) << function(self, input)
    if self.m_state ~= State.Default then
        return
    end

    local realInput = string.gsub(input, " ", "")
    if string.len(realInput) > string.len(self.m_input) then
        AudioAdapter.PostEvent("Au_UI_Event_Type")
    end
    self.m_input = realInput
    self.view.userRoleInputField.text = realInput
    self:_RefreshHint()
    self.m_checked = false
end

PlayerRenameCtrl._OnNameCheckSuccess = HL.Method() << function(self)
    self.m_checked = true
    self.m_select = false

    self:_SwitchState(State.SecondCheck)
    self:_DeselectInputField()

    if UNITY_PS4 or UNITY_PS5 or RENAME_PS_DEBUG or (UsingVirtualKeyboard() and DeviceInfo.usingController) then
        self.view.keyHintPS.gameObject:SetActive(false)
        self.view.invisibleSelectBtn.gameObject:SetActive(false)
        self.view.invisibleCancelBtn.gameObject:SetActive(false)
        self.view.controllerHintPlaceholder.gameObject:SetActive(false)
    end
end

PlayerRenameCtrl._OnNameCheckFailed = HL.Method() << function(self)
    self.view.userRoleInputField:Select()
end

PlayerRenameCtrl._OnNameSetSuccess = HL.Method() << function(self)
    self:_ExitAndNotifyRenameResult(Language.LUA_RENAME_SUCC_TOAST)
end

HL.Commit(PlayerRenameCtrl)
