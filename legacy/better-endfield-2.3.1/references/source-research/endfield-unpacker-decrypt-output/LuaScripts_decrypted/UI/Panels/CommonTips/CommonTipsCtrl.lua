
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonTips

CommonTipsCtrl = HL.Class('CommonTipsCtrl', uiCtrl.UICtrl)






CommonTipsCtrl.s_messages = HL.StaticField(HL.Table) << {
}


CommonTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.main.gameObject:SetActive(false)
    self.view.main.onTriggerAutoClose:AddListener(function()
        self.view.controllerHintPlaceholder.gameObject:SetActive(false)
        self:ChangeCurPanelBlockSetting(false)
    end)
    self.view.controllerHintPlaceholder.gameObject:SetActive(false)
end

CommonTipsCtrl.ShowCommonTips = HL.StaticMethod(HL.Table) << function(args)
    local self = UIManager:AutoOpen(PANEL_ID)
    self:ShowTips(args)
end








CommonTipsCtrl.ShowTips = HL.Method(HL.Table) << function(self, args)
    self:ChangeCurPanelBlockSetting(true)
    UIManager:SetTopOrder(PANEL_ID)
    self.view.main.gameObject:SetActive(true)
    self.view.text.text = args.text
    UIUtils.updateTipsPosition(self.view.main.transform, args.transform, self.view.rectTransform, self.uiCamera, args.posType)
    self.view.controllerHintPlaceholder.gameObject:SetActive(true)
end

HL.Commit(CommonTipsCtrl)
