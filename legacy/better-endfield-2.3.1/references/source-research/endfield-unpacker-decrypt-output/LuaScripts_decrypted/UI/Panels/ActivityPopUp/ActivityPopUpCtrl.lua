

local commonPopUpCtrl = require_ex('UI/Panels/CommonPopUp/CommonPopUpCtrl')
local PANEL_ID = PanelId.ActivityPopUp

ActivityPopUpCtrl = HL.Class('ActivityPopUpCtrl', commonPopUpCtrl.CommonPopUpCtrl)

ActivityPopUpCtrl.ShowActivityPopUp = HL.StaticMethod(HL.Table) << function(args)
    local ctrl = ActivityPopUpCtrl.AutoOpen(PANEL_ID, nil, false)
    UIManager:SetTopOrder(PANEL_ID)
    ctrl:_ShowPopUp(args)
end

ActivityPopUpCtrl.ShowActivityPopUpCS = HL.StaticMethod(HL.Table) << function(args)
    local ctrl = ActivityPopUpCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:_ShowPopUp({
        content = args[1],
        subContent = args[2],
        onConfirm = args[3],
        onCancel = args[4]
    })
end

HL.Commit(ActivityPopUpCtrl)
