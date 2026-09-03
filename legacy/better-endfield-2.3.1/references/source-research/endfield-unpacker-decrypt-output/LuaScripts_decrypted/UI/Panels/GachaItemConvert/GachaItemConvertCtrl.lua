
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaItemConvert

GachaItemConvertCtrl = HL.Class('GachaItemConvertCtrl', uiCtrl.UICtrl)






GachaItemConvertCtrl.s_messages = HL.StaticField(HL.Table) << {

}


GachaItemConvertCtrl.m_info = HL.Field(HL.Table)















GachaItemConvertCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self.m_info = arg
    self:_RefreshAllUI()
end

GachaItemConvertCtrl.OnClose = HL.Override() << function(self)
    local onComplete = self.m_info.onComplete
    self.m_info = nil
    if onComplete then
        onComplete()
    end
end



GachaItemConvertCtrl._InitUI = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.confirmBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.originalItem.tipsBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = self.m_info.originalItemId,
            isSideTips = true,
            transform = self.view.originalItem.tipsBtn.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
        })
    end)
    self.view.convertItem.tipsBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = self.m_info.convertItemBundle.id,
            isSideTips = true,
            transform = self.view.convertItem.tipsBtn.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
        })
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.itemNodeNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        self.view.controllerFocusHintNode.gameObject:SetActive(not isFocused)
    end)
end

GachaItemConvertCtrl._RefreshAllUI = HL.Method() << function(self)
    self.view.originalItem.itemIcon:InitItemIcon(self.m_info.originalItemId, true)
    self.view.convertItem.itemIcon:InitItemIcon(self.m_info.convertItemBundle.id, true)
    self.view.convertItem.itemNumTxt.text = string.format(Language.LUA_COMMON_X_COUNT, self.m_info.convertItemBundle.count)
    self.view.titleTxt.text = self.m_info.title
    self.view.tipsTxt.text = self.m_info.tipsText
end





HL.Commit(GachaItemConvertCtrl)
