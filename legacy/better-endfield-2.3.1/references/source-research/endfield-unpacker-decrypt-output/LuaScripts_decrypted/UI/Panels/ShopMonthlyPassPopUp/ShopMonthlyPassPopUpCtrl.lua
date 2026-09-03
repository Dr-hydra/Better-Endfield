
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopMonthlyPassPopUp

ShopMonthlyPassPopUpCtrl = HL.Class('ShopMonthlyPassPopUpCtrl', uiCtrl.UICtrl)






ShopMonthlyPassPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ShopMonthlyPassPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_BindUICallback()


    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder(
        { self.view.inputGroup.groupId })

end













ShopMonthlyPassPopUpCtrl._BindUICallback = HL.Method() << function(self)
    self.view.emptyClick.onClick:AddListener(function()
        self:_OnBgClick()
    end)
end

ShopMonthlyPassPopUpCtrl._OnBgClick = HL.Method() << function(self)
    self.m_phase:OnClickBg()
end



ShopMonthlyPassPopUpCtrl.RefreshUI = HL.Method() << function(self)
    if self.m_phase.m_haveGotReward == false then
        self.view.contentState:SetState("AcquireBefore")
        self.view.emptyClick.interactable = true
    else
        self.view.contentState:SetState("AcquireAfter")
        self.view.emptyClick.interactable = false
    end
end

HL.Commit(ShopMonthlyPassPopUpCtrl)
