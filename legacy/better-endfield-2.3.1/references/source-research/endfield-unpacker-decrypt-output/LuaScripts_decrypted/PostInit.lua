
require_ex("Const/PostConst")

UIManager:OpenInitPanels()

if (BEYOND_DEBUG_COMMAND or BEYOND_DEBUG) and HIDE_DEV_LOG_CONSOLE then
    Unity.Debug.developerConsoleEnabled = false
end

Register(MessageConst.ON_SWITCH_LANGUAGE_ENUM, function(arg)
    hg.curEnvLang = CS.Beyond.I18n.I18nUtils.curEnvLang:GetHashCode()
end)

Register(MessageConst.ON_SWITCH_LANGUAGE, function(arg)
    LoadConst(true)

    UIManager:Open(PanelId.UIDPanel)
    UIManager:Open(PanelId.CommonTips)
end)

if BEYOND_DEBUG_COMMAND then
    local bindingId = InputManagerInst:CreateBindingByActionId("debug_reset_ui_panels", function()
        Notify(MessageConst.EXIT_ALL_PHASE, { CS.Beyond.EventSystemConst.ExitAllPhaseReason.DEBUG })
        Notify(MessageConst.OPEN_LEVEL_PHASE)
    end, -1)
    Register(MessageConst.ON_DISPOSE_LUA_ENV, function(arg)
        InputManagerInst:DeleteBinding(bindingId)
    end)
end
