
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CashShopToast

CashShopToastCtrl = HL.Class('CashShopToastCtrl', uiCtrl.UICtrl)

CashShopToastCtrl.m_timerId = HL.Field(HL.Number) << 0





CashShopToastCtrl.s_messages = HL.StaticField(HL.Table) << {

}

CashShopToastCtrl.OnShowToast = HL.StaticField(HL.Any) << function (arg)
    local ctrl = CashShopToastCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:ShowToast(arg)
end


CashShopToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end











CashShopToastCtrl.ShowToast = HL.Method(HL.Any) << function (self, arg)
    if self.m_timerId ~= 0 then
        self:_ClearTimer(self.m_timerId)
        self.m_timerId = 0
    end

    if arg.text then
        self.view.shopToastTxt:SetAndResolveTextStyle(arg.text)
    end

    local duration = self.view.config.SHOW_DURATION
    self.m_timerId = self:_StartTimer(duration, function()
        self.m_timerId = 0
        self:PlayAnimationOut()
    end)
end

HL.Commit(CashShopToastCtrl)
