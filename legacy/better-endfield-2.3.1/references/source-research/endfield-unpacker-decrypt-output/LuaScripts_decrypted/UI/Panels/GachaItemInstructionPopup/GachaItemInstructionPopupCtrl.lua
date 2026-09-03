
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaItemInstructionPopup

GachaItemInstructionPopupCtrl = HL.Class('GachaItemInstructionPopupCtrl', uiCtrl.UICtrl)






GachaItemInstructionPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


GachaItemInstructionPopupCtrl.m_info = HL.Field(HL.Table)













GachaItemInstructionPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self.m_info = arg
    self:_RefreshAllUI()
end

GachaItemInstructionPopupCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return self.m_info
end



GachaItemInstructionPopupCtrl._InitUI = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.itemTipsBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = self.m_info.itemId,
            transform = self.view.itemTipsBtn.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
        })
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

GachaItemInstructionPopupCtrl._RefreshAllUI = HL.Method() << function(self)
    self.view.titleTxt.text = self.m_info.title
    self.view.descTxt:SetAndResolveTextStyle(self.m_info.desc)
    self.view.tipsTxt:SetAndResolveTextStyle(self.m_info.tips)
    self.view.itemIcon:InitItemIcon(self.m_info.itemId, true)
end


HL.Commit(GachaItemInstructionPopupCtrl)
