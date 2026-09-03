
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaSelCharTicPopup

GachaSelCharTicPopupCtrl = HL.Class('GachaSelCharTicPopupCtrl', uiCtrl.UICtrl)






GachaSelCharTicPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


local MAX_SHOW_CHAR_COUNT = 4

GachaSelCharTicPopupCtrl.m_info = HL.Field(HL.Table)

GachaSelCharTicPopupCtrl.m_showCharImgList = HL.Field(HL.Table)













GachaSelCharTicPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self.m_info = arg
    self:_RefreshAllUI()
end

GachaSelCharTicPopupCtrl.OnClose = HL.Override() << function(self)
    local onComplete = self.m_info.onComplete
    self.m_info = nil
    if onComplete then
        onComplete()
    end
end



GachaSelCharTicPopupCtrl._InitUI = HL.Method() << function(self)
    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.m_showCharImgList = {}
    for i = 1, MAX_SHOW_CHAR_COUNT do
        table.insert(self.m_showCharImgList, self.view["ShowCharImg" .. i])
    end
end

GachaSelCharTicPopupCtrl._RefreshAllUI = HL.Method() << function(self)

    self.view.itemIcon:InitItemIcon(self.m_info.itemId, true)

















end





HL.Commit(GachaSelCharTicPopupCtrl)
