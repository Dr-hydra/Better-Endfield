local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GameSettingKeycodePopup

local KeyboardKeyCode = CS.Beyond.Input.KeyboardKeyCode

GameSettingKeycodePopupCtrl = HL.Class('GameSettingKeycodePopupCtrl', uiCtrl.UICtrl)





GameSettingKeycodePopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}

GameSettingKeycodePopupCtrl.CheckArgs = HL.StaticMethod(HL.Table).Return(HL.Boolean) << function(args)
    return true
end

GameSettingKeycodePopupCtrl.m_settingItemData = HL.Field(HL.Userdata)

GameSettingKeycodePopupCtrl.m_isPrimary = HL.Field(HL.Boolean) << false

GameSettingKeycodePopupCtrl.m_onKeyCodeInput = HL.Field(HL.Function)

GameSettingKeycodePopupCtrl.m_actionScopes = HL.Field(HL.Table)

GameSettingKeycodePopupCtrl.m_listenInputTick = HL.Field(HL.Number) << -1


GameSettingKeycodePopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.m_settingItemData = arg.settingItemData
    self.m_isPrimary = arg.isPrimary
    self.m_onKeyCodeInput = arg.onKeyCodeInput

    local actionScopes = {}
    local configActionScopes = arg.settingItemData.keyActionScopes
    for i = 0, configActionScopes.Count - 1 do
        table.insert(actionScopes, configActionScopes[i])
    end
    self.m_actionScopes = actionScopes
end

GameSettingKeycodePopupCtrl.OnClose = HL.Override() << function(self)
    self.m_listenInputTick = LuaUpdate:Remove(self.m_listenInputTick)
end

GameSettingKeycodePopupCtrl.OnShow = HL.Override() << function(self)
    self:_UpdateView()

    self.m_listenInputTick = LuaUpdate:Add("Tick", function(deltaTime)
        self:_ListenInput()
    end)
end

GameSettingKeycodePopupCtrl._UpdateView = HL.Method() << function(self)
    local settingItemData = self.m_settingItemData
    self.view.actionNameText.text = settingItemData.settingText

    local actionPriorityTextId = self.m_isPrimary and "ui_set_gamesetting_keyhint1" or "ui_set_gamesetting_keyhint2"
    self.view.actionPriorityText.text = Language[actionPriorityTextId]
end

GameSettingKeycodePopupCtrl._ListenInput = HL.Method() << function(self)
    if not self.view.inputGroup.groupEnabled then
        return
    end

    local success, keyCode, isBlackList = InputManagerInst:AnyKeyboardKey(self.m_actionScopes)
    if not success then
        return
    end
    if isBlackList then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_GAME_SETTING_KEY_CODE_IN_BLACK_LIST)
        return
    end

    local result
    success, result = xpcall(self.m_onKeyCodeInput, debug.traceback, keyCode)
    if not success then
        logger.error("[GameSetting] OnKeyCodeInput: Failed, message: " .. tostring(result))
        self:PlayAnimationOutAndClose()
        return
    end


    if result then
        self:PlayAnimationOutAndClose()
    end
end

HL.Commit(GameSettingKeycodePopupCtrl)
