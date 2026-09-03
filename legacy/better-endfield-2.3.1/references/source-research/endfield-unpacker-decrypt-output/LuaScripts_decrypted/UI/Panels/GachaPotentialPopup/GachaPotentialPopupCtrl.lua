local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaPotentialPopup

GachaPotentialPopupCtrl = HL.Class('GachaPotentialPopupCtrl', uiCtrl.UICtrl)






GachaPotentialPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


GachaPotentialPopupCtrl.m_info = HL.Field(HL.Table)












GachaPotentialPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self.m_info = arg
    self:_RefreshAllUI()
end

GachaPotentialPopupCtrl.OnClose = HL.Override() << function(self)
    local onComplete = self.m_info.onComplete
    self.m_info = nil
    if onComplete then
        onComplete()
    end
end



GachaPotentialPopupCtrl._InitUI = HL.Method() << function(self)
    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

GachaPotentialPopupCtrl._RefreshAllUI = HL.Method() << function(self)
    self.view.itemIcon:InitItemIcon(self.m_info.potentialItemId, true)
    if self.m_info.isPotentialBox then
        self.view.stateController:SetState("PotentialBox")
    else
        self.view.stateController:SetState("Normal")
        local _, gachaCharInfoCfg = Tables.gachaCharInfoTable:TryGetValue(self.m_info.charId)
        self.view.roleImg:LoadSprite(UIConst.UI_SPRITE_GACHA_POOL, gachaCharInfoCfg.potentialPopupCharImg)
        local color = UIUtils.getColorByString(gachaCharInfoCfg.potentialPopupColor)
        self.view.colorImg1.color = color
        self.view.colorImg2.color = color
    end
end





HL.Commit(GachaPotentialPopupCtrl)
