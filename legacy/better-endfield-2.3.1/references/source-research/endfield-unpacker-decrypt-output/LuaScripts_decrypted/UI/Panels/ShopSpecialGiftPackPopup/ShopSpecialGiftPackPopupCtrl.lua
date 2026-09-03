local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopSpecialGiftPackPopup

ShopSpecialGiftPackPopupCtrl = HL.Class('ShopSpecialGiftPackPopupCtrl', uiCtrl.UICtrl)






ShopSpecialGiftPackPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ShopSpecialGiftPackPopupCtrl.m_packId = HL.Field(HL.String) << ""

ShopSpecialGiftPackPopupCtrl.m_info = HL.Field(HL.Table)

ShopSpecialGiftPackPopupCtrl.m_fadeCor = HL.Field(HL.Thread)




ShopSpecialGiftPackPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_packId = arg
    self:_InitUI()
    self:_InitData()
    self:_RefreshAllUI()
end

ShopSpecialGiftPackPopupCtrl.OnClose = HL.Override() << function(self)
    self.m_fadeCor = self:_ClearCoroutine(self.m_fadeCor)
end

ShopSpecialGiftPackPopupCtrl.OnShow = HL.Override() << function(self)
    self.m_fadeCor = self:_ClearCoroutine(self.m_fadeCor)
end

ShopSpecialGiftPackPopupCtrl.OnHide = HL.Override() << function(self)
    self.m_fadeCor = self:_ClearCoroutine(self.m_fadeCor)
end



ShopSpecialGiftPackPopupCtrl._InitData = HL.Method() << function(self)
    local _, cashGoodsCfg = Tables.cashShopGoodsTable:TryGetValue(self.m_packId)
    if not cashGoodsCfg then
        logger.error("【ShopSpecialGiftPackPopup】 表配置中不存在该场景礼包！id：" .. self.m_packId)
        return
    end
    self.m_info = {
        icon = cashGoodsCfg.iconId,
    }
end



ShopSpecialGiftPackPopupCtrl._InitUI = HL.Method() << function(self)
    self.view.jumpBtn.onClick:AddListener(function()
        if PhaseManager:GetTopPhaseId() == PhaseId.GachaWeaponPool then
            PhaseManager:ExitPhaseFast(PhaseId.GachaWeaponPool)
        end
        Notify(MessageConst.CASH_SHOP_CHOOSE_GIFTPACK_TAB_BY_GOODSID,
            {
                cashGoodsId = self.m_packId
            })
        self:Exit()
    end)
end

ShopSpecialGiftPackPopupCtrl._RefreshAllUI = HL.Method() << function(self)

end



ShopSpecialGiftPackPopupCtrl.Fade = HL.Method() << function(self)
    self.m_fadeCor = self:_ClearCoroutine(self.m_fadeCor)
    self.m_fadeCor = self:_StartCoroutine(function()
        coroutine.wait(self.view.config.WAIT_FADE_TIME)
        self:Exit()
    end)
end

ShopSpecialGiftPackPopupCtrl.Exit = HL.Method() << function(self)
    self.m_fadeCor = self:_ClearCoroutine(self.m_fadeCor)
    if not string.isEmpty(GameInstance.player.cashShopSystem.waitShowSpecialGiftPackId) then
        logger.info("[cashshop] waitShowSpecialGiftPackId set to empty")
        GameInstance.player.cashShopSystem.waitShowSpecialGiftPackId = ""
        self:PlayAnimationOutAndClose()
    end
end


HL.Commit(ShopSpecialGiftPackPopupCtrl)
