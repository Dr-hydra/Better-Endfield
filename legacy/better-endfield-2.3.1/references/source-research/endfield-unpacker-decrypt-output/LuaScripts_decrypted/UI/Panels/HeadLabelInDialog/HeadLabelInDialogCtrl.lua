
local headLabelCtrl = require_ex('UI/Panels/HeadLabel/HeadLabelCtrl')
local PANEL_ID = PanelId.HeadLabelInDialog
HeadLabelInDialogCtrl = HL.Class('HeadLabelInDialogCtrl', headLabelCtrl.HeadLabelCtrl)







HeadLabelInDialogCtrl.s_overrideMessages = HL.StaticField(HL.Table) << {

}

HeadLabelInDialogCtrl.RefreshEnvTalk = HL.Method(HL.Any) << function(self, args)
    self:ShowEnvTalk(args)
end











HL.Commit(HeadLabelInDialogCtrl)
