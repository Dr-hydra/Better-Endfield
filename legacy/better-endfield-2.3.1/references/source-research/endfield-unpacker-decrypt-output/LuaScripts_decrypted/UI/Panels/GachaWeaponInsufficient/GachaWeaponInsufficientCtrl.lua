
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaWeaponInsufficient

GachaWeaponInsufficientCtrl = HL.Class('GachaWeaponInsufficientCtrl', uiCtrl.UICtrl)






GachaWeaponInsufficientCtrl.s_messages = HL.StaticField(HL.Table) << {

}


GachaWeaponInsufficientCtrl.m_onClickOriginiumExchange = HL.Field(HL.Function)



GachaWeaponInsufficientCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    if arg then
        self.m_onClickOriginiumExchange = arg.onClickOriginiumExchange
    end

    local hasGiftPack = CashShopUtils.HasGachaWeaponTokenGiftGoodsCanBuy()
    if hasGiftPack then
        self.view.giftPackEntranceBtn.gameObject:SetActive(true)
        self.view.originiumExchangeBtn.gameObject:SetActive(false)
    else
        self.view.giftPackEntranceBtn.gameObject:SetActive(false)
        self.view.originiumExchangeBtn.gameObject:SetActive(self.m_onClickOriginiumExchange ~= nil)
    end
end

GachaWeaponInsufficientCtrl._InitUI = HL.Method() << function(self)
    self.view.mask.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.confirmButton.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.exploreEntranceBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
        PhaseManager:GoToPhase(PhaseId.GachaPool)
    end)
    self.view.battlePassEntranceBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
        PhaseManager:GoToPhase(PhaseId.BattlePass)
    end)
    self.view.giftPackEntranceBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
        PhaseManager:GoToPhase(PhaseId.CashShop, {
            shopGroupId = CashShopConst.CashShopCategoryType.Pack,
            cashShopId = Tables.CashShopConst.WeaponCashShopId,
        })
    end)
    self.view.originiumExchangeBtn.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            if self.m_onClickOriginiumExchange then
                self.m_onClickOriginiumExchange()
            end
            self:Close()
        end)
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
        self.view.inputGroup.groupId,
    })
end

HL.Commit(GachaWeaponInsufficientCtrl)
