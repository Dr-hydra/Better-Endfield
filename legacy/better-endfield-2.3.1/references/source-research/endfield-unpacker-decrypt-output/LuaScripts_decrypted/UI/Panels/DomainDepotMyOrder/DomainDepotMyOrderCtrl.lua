local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainDepotMyOrder

DomainDepotMyOrderCtrl = HL.Class('DomainDepotMyOrderCtrl', uiCtrl.UICtrl)

DomainDepotMyOrderCtrl.m_getCellFunc = HL.Field(HL.Function)





DomainDepotMyOrderCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_COLLECT_DELEGATE_REWARD] = 'OnSync',
    [MessageConst.ON_DOMAIN_DEPOT_DELIVERY_REWARD] = 'OnReward',
}


DomainDepotMyOrderCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:RemoveAllListeners()
    self.view.scrollList.onUpdateCell:AddListener(function(object, index)
        local cell = self.m_getCellFunc(object)
        cell:InitSelfDomainDepotDeliveryCell(GameInstance.player.domainDepotSystem.myDelegateDeliverList[index], nil)
    end)

    self.view.orderNode:SetState(GameInstance.player.domainDepotSystem.myDelegateDeliverList.Count == 0 and 'empty' or 'normal')

    self.view.tipsButton.onClick:RemoveAllListeners()
    self.view.tipsButton.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "domain_depot_delivery")
    end)

    self.view.getBtn.onClick:RemoveAllListeners()
    self.view.getBtn.onClick:AddListener(function()
        local rewardIDList = {}
        for i = 0, GameInstance.player.domainDepotSystem.myDelegateDeliverList.Count - 1 do

            local info = GameInstance.player.domainDepotSystem.myDelegateDeliverList[i]
            if info.packageProgress == GEnums.DomainDepotPackageProgress.SendPackageTimeout or info.packageProgress == GEnums.DomainDepotPackageProgress.WaitingRecvFinalPayment then
                table.insert(rewardIDList, info.insId)
            end
        end

        GameInstance.player.domainDepotSystem:SendDomainDepotCollectDelegateRewardReq(rewardIDList)
    end)

    self:_UpdateGetAllBtn()

    self.view.scrollList:UpdateCount(GameInstance.player.domainDepotSystem.myDelegateDeliverList.Count)
end

DomainDepotMyOrderCtrl.OnSync = HL.Method() << function(self)
    self.view.scrollList:UpdateCount(GameInstance.player.domainDepotSystem.myDelegateDeliverList.Count)
    self.view.selectableNaviGroup:NaviToThisGroup()
    self.view.emptyNode.gameObject:SetActiveIfNecessary(GameInstance.player.domainDepotSystem.myDelegateDeliverList.Count == 0)
    self.view.orderNode:SetState(GameInstance.player.domainDepotSystem.myDelegateDeliverList.Count == 0 and 'empty' or 'normal')
    self:_UpdateGetAllBtn()
end

DomainDepotMyOrderCtrl._UpdateGetAllBtn = HL.Method() << function(self)
    local canReceive = false
    for i = 0, GameInstance.player.domainDepotSystem.myDelegateDeliverList.Count - 1 do

        local info = GameInstance.player.domainDepotSystem.myDelegateDeliverList[i]
        if info.packageProgress == GEnums.DomainDepotPackageProgress.SendPackageTimeout or info.packageProgress == GEnums.DomainDepotPackageProgress.WaitingRecvFinalPayment then
            canReceive = true
            break
        end
    end

    self.view.getBtn.gameObject:SetActiveIfNecessary(canReceive)
end

DomainDepotMyOrderCtrl.OnReward = HL.Method(HL.Any) << function(self, map)
    local kv = unpack(map)
    local rewardMoneyList = {}
    for id,count in cs_pairs(kv) do
        table.insert(rewardMoneyList, {
            id = id,
            count = count
        })
    end

    if #rewardMoneyList > 0 then
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
            items = rewardMoneyList
        })
    end
end

DomainDepotMyOrderCtrl.OnShow = HL.Override() << function(self)
    self.view.selectableNaviGroup:NaviToThisGroup()
    self.view.scrollList:UpdateCount(GameInstance.player.domainDepotSystem.myDelegateDeliverList.Count)
end
DomainDepotMyOrderCtrl.OnHide = HL.Override() << function(self)
    self.view.scrollList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.m_getCellFunc(obj)
        cell.view.animationWrapper:PlayOutAnimation()
    end)
end






HL.Commit(DomainDepotMyOrderCtrl)
