
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaImportantRewardPopup

GachaImportantRewardPopupCtrl = HL.Class('GachaImportantRewardPopupCtrl', uiCtrl.UICtrl)






GachaImportantRewardPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


GachaImportantRewardPopupCtrl.m_info = HL.Field(HL.Table)














GachaImportantRewardPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self.m_info = arg
    self:_RefreshAllUI()
end

GachaImportantRewardPopupCtrl.OnClose = HL.Override() << function(self)
    local onComplete = self.m_info.onComplete
    self.m_info = nil
    if onComplete then
        onComplete()
    end
end



GachaImportantRewardPopupCtrl._InitUI = HL.Method() << function(self)
    self.view.maskBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

GachaImportantRewardPopupCtrl._RefreshAllUI = HL.Method() << function(self)
    local itemId = self.m_info.itemId
    self.view.itemIcon:InitItemIcon(itemId, true)

    local itemData = Tables.itemTable[itemId]
    local itemName = itemData.name
    if self.m_info.itemCount then
        itemName = string.format(Language.LUA_COMMON_NAME_X_COUNT, itemData.name, self.m_info.itemCount)
    end
    self.view.itemNameTxt.text = itemName

    if self.m_info.desc then
        self.view.descTxt:SetAndResolveTextStyle(self.m_info.desc)
    else
        self.view.descTxt:SetAndResolveTextStyle(Language.LUA_GACHA_GOT_TESTIMONIAL_IMPORTANT_DESC)
    end

    self.view.tipsBtn.onClick:RemoveAllListeners()
    self.view.tipsBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            transform = self.view.tipsBtn.transform,
            itemId = itemId,
        })
    end)
end





HL.Commit(GachaImportantRewardPopupCtrl)
