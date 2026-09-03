
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.TemporaryEmptyFreezeWorld

TemporaryEmptyFreezeWorldCtrl = HL.Class('TemporaryEmptyFreezeWorldCtrl', uiCtrl.UICtrl)






TemporaryEmptyFreezeWorldCtrl.s_messages = HL.StaticField(HL.Table) << {

}


TemporaryEmptyFreezeWorldCtrl.s_usages = HL.StaticField(HL.Table) << {}

TemporaryEmptyFreezeWorldCtrl.s_usageCount = HL.StaticField(HL.Number) << 0



TemporaryEmptyFreezeWorldCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

TemporaryEmptyFreezeWorldCtrl.OpenFreezeWorldPanel = HL.StaticMethod(HL.String) << function(source)
    local freezeWorld = TemporaryEmptyFreezeWorldCtrl.s_usages[source]
    if freezeWorld then
        return
    end

    TemporaryEmptyFreezeWorldCtrl.s_usages[source] = true
    local usageCount = TemporaryEmptyFreezeWorldCtrl.s_usageCount + 1
    TemporaryEmptyFreezeWorldCtrl.s_usageCount = usageCount
    if usageCount == 1 then
        UIWorldFreezeManager:_OnPanelActivate("TemporaryEmptyFreezeWorld")
    end
end

TemporaryEmptyFreezeWorldCtrl.CloseFreezeWorldPanel = HL.StaticMethod(HL.String) << function(source)
    local freezeWorld = TemporaryEmptyFreezeWorldCtrl.s_usages[source]
    if not freezeWorld then
        return
    end

    TemporaryEmptyFreezeWorldCtrl.s_usages[source] = nil
    local usageCount = TemporaryEmptyFreezeWorldCtrl.s_usageCount - 1
    TemporaryEmptyFreezeWorldCtrl.s_usageCount = usageCount
    if usageCount == 0 then
        UIWorldFreezeManager:_OnPanelDeActivate("TemporaryEmptyFreezeWorld")
    end
end

TemporaryEmptyFreezeWorldCtrl.OnApplicationPause = HL.StaticMethod(HL.Any) << function(arg)

    if not DeviceInfo.isAndroid then
        return
    end

    local pauseStatus = arg[1]
    if pauseStatus then
        Notify(MessageConst.OPEN_FREEZE_WORLD_PANEL, "OnApplicationPause")
    else
        Notify(MessageConst.CLOSE_FREEZE_WORLD_PANEL, "OnApplicationPause")
    end
end

HL.Commit(TemporaryEmptyFreezeWorldCtrl)
