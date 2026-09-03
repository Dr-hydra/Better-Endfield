
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RecycleBinNoticeToast

RecycleBinNoticeToastCtrl = HL.Class('RecycleBinNoticeToastCtrl', uiCtrl.UICtrl)






RecycleBinNoticeToastCtrl.s_messages = HL.StaticField(HL.Table) << {

}

RecycleBinNoticeToastCtrl.OnRecycleBinUpgradeAndCollected = HL.StaticMethod(HL.Table) << function(args)
    UIManager:AutoOpen(PANEL_ID, args)
end

RecycleBinNoticeToastCtrl.m_durationCor = HL.Field(HL.Thread)


RecycleBinNoticeToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_durationCor = self:_StartCoroutine(function()
        coroutine.wait(self.view.config.SHOW_DURATION)
        self:PlayAnimationOutAndClose()
    end)
end





RecycleBinNoticeToastCtrl.OnClose = HL.Override() << function(self)
    self.m_durationCor = self:_ClearCoroutine(self.m_durationCor)
end




HL.Commit(RecycleBinNoticeToastCtrl)
