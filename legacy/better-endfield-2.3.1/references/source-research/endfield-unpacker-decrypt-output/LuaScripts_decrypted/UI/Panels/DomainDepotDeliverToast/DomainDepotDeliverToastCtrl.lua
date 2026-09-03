local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainDepotDeliverToast

DomainDepotDeliverToastCtrl = HL.Class('DomainDepotDeliverToastCtrl', uiCtrl.UICtrl)

local DOMAIN_DEPOT_DELIVER_TOAST_MAIN_HUD_QUEUE_TYPE = "DomainDepotDeliverToast"

DomainDepotDeliverToastCtrl.m_showTimer = HL.Field(HL.Number) << -1

DomainDepotDeliverToastCtrl.s_waitShowRecvToast = HL.StaticField(HL.Boolean) << false





DomainDepotDeliverToastCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE] = 'OnInterruptMainHudActionQueue',
}


DomainDepotDeliverToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local showRecvFinish = arg.showRecvFinish
    self.view.viewController:SetState(showRecvFinish and "Recv" or "Send")
    if showRecvFinish then
        local itemType, packType = GameInstance.player.domainDepotSystem:GetDomainDepotDeliverItemAndPackType()
        local itemDesc = Tables.domainDepotDeliverItemTypeTable[itemType].deliveryDesc
        local packDesc = Tables.domainDepotDeliverPackTypeTable[packType].deliveryDesc
        self.view.titleTxt.text = string.format(Language.LUA_DOMAIN_DEPOT_DELIVER_TOAST_RECV, packDesc, itemDesc)
    else
        self.view.titleTxt.text = Language.LUA_DOMAIN_DEPOT_DELIVER_TOAST_SEND
    end
    self.m_showTimer = self:_StartTimer(self.view.config.SHOW_DURATION, function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
            Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, DOMAIN_DEPOT_DELIVER_TOAST_MAIN_HUD_QUEUE_TYPE)
        end)
    end)
end

DomainDepotDeliverToastCtrl.OnInterruptMainHudActionQueue = HL.Method() << function(self)
    self.m_showTimer = self:_ClearTimer(self.m_showTimer)
    self:Close()
end

DomainDepotDeliverToastCtrl.ShowDeliverRecvToast = HL.StaticMethod() << function()
    DomainDepotDeliverToastCtrl.s_waitShowRecvToast = true
    LuaSystemManager.mainHudActionQueue:AddRequest(DOMAIN_DEPOT_DELIVER_TOAST_MAIN_HUD_QUEUE_TYPE, function()
        DomainDepotDeliverToastCtrl.s_waitShowRecvToast = false
        UIManager:AutoOpen(PANEL_ID, { showRecvFinish = true })
    end)
end

DomainDepotDeliverToastCtrl.ShowDeliverSendToast = HL.StaticMethod(HL.Any) << function(arg)
    local isSendFinished = unpack(arg)
    if not isSendFinished then
        return
    end
    LuaSystemManager.mainHudActionQueue:AddRequest(DOMAIN_DEPOT_DELIVER_TOAST_MAIN_HUD_QUEUE_TYPE, function()
        UIManager:AutoOpen(PANEL_ID, { showRecvFinish = false })
    end)
end

DomainDepotDeliverToastCtrl.ClearDeliverToast = HL.StaticMethod() << function()
    if not DomainDepotDeliverToastCtrl.s_waitShowRecvToast then
        return
    end
    LuaSystemManager.mainHudActionQueue:RemoveActionsOfType(DOMAIN_DEPOT_DELIVER_TOAST_MAIN_HUD_QUEUE_TYPE)
    DomainDepotDeliverToastCtrl.s_waitShowRecvToast = false
end


HL.Commit(DomainDepotDeliverToastCtrl)
