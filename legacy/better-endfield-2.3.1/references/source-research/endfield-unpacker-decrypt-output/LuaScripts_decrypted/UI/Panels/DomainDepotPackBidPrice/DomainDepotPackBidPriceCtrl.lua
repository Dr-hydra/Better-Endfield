
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainDepotPackBidPrice

DomainDepotPackBidPriceCtrl = HL.Class('DomainDepotPackBidPriceCtrl', uiCtrl.UICtrl)






DomainDepotPackBidPriceCtrl.s_messages = HL.StaticField(HL.Table) << {

}

DomainDepotPackBidPriceCtrl.m_domainDepotId = HL.Field(HL.String) << ""

DomainDepotPackBidPriceCtrl.m_trySkipBindingId = HL.Field(HL.Number) << -1

DomainDepotPackBidPriceCtrl.m_isClick = HL.Field(HL.Boolean) << false


DomainDepotPackBidPriceCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local domainDepotId = arg and arg.domainDepotId
    self.m_domainDepotId = domainDepotId

    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        self:_OnNextBtnClick()
    end)


    local pack = arg.pack
    local deliverInfo = GameInstance.player.domainDepotSystem:GetDomainDepotDeliverInfoByDepotId(domainDepotId)

    pack:GotoSellAnim(deliverInfo.deliverPackType, deliverInfo.itemType)

    local buyerInfo = DomainDepotUtils.GetBuyerInfo(domainDepotId)



    self:_StartCoroutine(function()
        coroutine.wait(lume.random(self.view.config.RANDOM_TIME1))
        self.view.peoplePanel01.priceTxt.text = buyerInfo[1].reward.count
        AudioAdapter.PostEvent("Au_UI_Toast_RegionWareQuote")
        self.view.peoplePanel01.animationWrapper:Play("domainDepot_peoplepanel03", function()
            self.view.peoplePanel01.animationWrapper:Play("domainDepot_peoplepanel03_loop")
        end)
    end)

    if #buyerInfo == 0 then
        return
    end
    self:_StartCoroutine(function()
        coroutine.wait(lume.random(self.view.config.RANDOM_TIME2))
        if #buyerInfo >= 2 then
            self.view.peoplePanel02.priceTxt.text = buyerInfo[2].reward.count
            AudioAdapter.PostEvent("Au_UI_Toast_RegionWareQuote")
            self.view.peoplePanel02.animationWrapper:Play("domainDepot_peoplepanel03", function()
                self.view.peoplePanel02.animationWrapper:Play("domainDepot_peoplepanel03_loop")
            end)
        end
    end)

    self:_StartCoroutine(function()
        coroutine.wait(lume.random(self.view.config.RANDOM_TIME2, 5))
        self:_OnNextBtnClick()
    end)

    self:_InitBidPriceController()
end


DomainDepotPackBidPriceCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    return {
        domainDepotId = self.m_domainDepotId,
    }
end

DomainDepotPackBidPriceCtrl._OnNextBtnClick = HL.Method() << function(self)
    if self.m_isClick then
        return
    end
    self.m_isClick = true
    Notify(MessageConst.ON_OPEN_DOMAIN_DEPOT_PACK_SELL_PANEL, { domainDepotId = self.m_domainDepotId })
end




DomainDepotPackBidPriceCtrl._InitBidPriceController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end



    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

DomainDepotPackBidPriceCtrl._OnControllerTrySkip = HL.Method() << function(self)
    self:BindInputPlayerAction("domain_depot_bid_price_skip", function()
        self:_OnNextBtnClick()
    end)
    InputManagerInst:ToggleBinding(self.m_trySkipBindingId, false)
end



HL.Commit(DomainDepotPackBidPriceCtrl)
