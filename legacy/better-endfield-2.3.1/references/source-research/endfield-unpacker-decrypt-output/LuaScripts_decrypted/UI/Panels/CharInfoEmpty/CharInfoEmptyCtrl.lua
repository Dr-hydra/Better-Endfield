local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoEmpty
CharInfoEmptyCtrl = HL.Class('CharInfoEmptyCtrl', uiCtrl.UICtrl)







CharInfoEmptyCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.REFRESH_CHAR_CONTROLLER_HINT] = 'RefreshCharControllerHint',
    [MessageConst.REFRESH_CHAR_EMPTY_SORTING_ORDER] = 'RefreshSortingOrder',
}


CharInfoEmptyCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

CharInfoEmptyCtrl.RefreshCharControllerHint = HL.Method(HL.Table) << function(self, args)
    local groupIds, additionalHints = unpack(args)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder(groupIds, additionalHints)
end

CharInfoEmptyCtrl.SwitchCharInfoVirtualMouseType = HL.Method(HL.Any) << function(self, panelMouseMode)

    self:ChangePanelCfg("realMouseMode", panelMouseMode)
end

CharInfoEmptyCtrl.RefreshSortingOrder = HL.Method(HL.Number) << function(self, targetSortingOrder)
    self:SetSortingOrder(targetSortingOrder, false)
end

HL.Commit(CharInfoEmptyCtrl)
