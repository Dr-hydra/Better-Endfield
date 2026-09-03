
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WatchBlur

WatchBlurCtrl = HL.Class('WatchBlurCtrl', uiCtrl.UICtrl)






WatchBlurCtrl.s_messages = HL.StaticField(HL.Table) << {

}


WatchBlurCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.blurBG:InitRT()
    self.view.blurBG:Register()
end





WatchBlurCtrl.OnClose = HL.Override() << function(self)
    local _, blurPanel = UIManager:IsOpen(PanelId.FullScreenSceneBlur)
    if blurPanel then
        blurPanel.view.blurBG:SetCustomBlurImg(nil)
    end
end

HL.Commit(WatchBlurCtrl)
