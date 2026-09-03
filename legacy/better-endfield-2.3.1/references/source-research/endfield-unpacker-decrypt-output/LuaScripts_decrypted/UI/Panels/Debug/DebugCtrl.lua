
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Debug
local DebugManager = CS.Beyond.DebugManager.instance
DebugCtrl = HL.Class('DebugCtrl', uiCtrl.UICtrl)







DebugCtrl.s_messages = HL.StaticField(HL.Table) << {

}

DebugCtrl.OnLuaInitFinished = HL.StaticMethod() << function()
    if not (BEYOND_DEBUG_COMMAND or BEYOND_DEBUG) then
        return
    end
    Notify(MessageConst.SET_DEBUG_PANEL_BLOCK_INPUT, { false })
end

DebugCtrl.SetDebugPanelBlockInput = HL.StaticMethod(HL.Any) << function (arg)
    if not (BEYOND_DEBUG_COMMAND or BEYOND_DEBUG) then
        return
    end
    local ctrl = DebugCtrl.AutoOpen(PANEL_ID, nil, false)
    local isShown = unpack(arg)
    if isShown then
        ctrl:ChangeCurPanelBlockSetting(true, Types.EPanelMultiTouchTypes.Both)
    else
        ctrl:ChangeCurPanelBlockSetting(false, Types.EPanelMultiTouchTypes.Both)
    end
end

HL.Commit(DebugCtrl)
