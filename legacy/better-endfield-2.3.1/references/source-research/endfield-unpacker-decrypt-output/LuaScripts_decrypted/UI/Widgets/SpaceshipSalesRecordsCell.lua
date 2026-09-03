local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SpaceshipSalesRecordsCell = HL.Class('SpaceshipSalesRecordsCell', UIWidgetBase)


SpaceshipSalesRecordsCell.m_moneyCellCache = HL.Field(HL.Forward("UIListCache"))




SpaceshipSalesRecordsCell._OnFirstTimeInit = HL.Override() << function(self)

end

SpaceshipSalesRecordsCell.InitSpaceshipSalesRecordsCell = HL.Method(HL.Boolean, HL.Number, HL.Any)
    << function(self, isEmpty, roleId, moneyIdToSoldPrice)
    self:_FirstTimeInit()
    if isEmpty then
        self.view.stateController:SetState("EmptyNode")
        return
    end

    self.view.stateController:SetState("NormalNode")
    self.view.playerHead:UpdateHideSignature(true)
    self.view.playerHead:InitCommonPlayerHeadByRoleId(roleId, function()
        FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(roleId).action()
    end)

    self.m_moneyCellCache = self.m_moneyCellCache or UIUtils.genCellCache(self.view.moneyCell)
    self.m_moneyCellCache:Refresh(moneyIdToSoldPrice.Count, function(moneyCell, subLuaIndex)
        local soldPrice = moneyIdToSoldPrice[subLuaIndex - 1]
        local succ, itemData = Tables.itemTable:TryGetValue(soldPrice.moneyIdStr)
        if succ then
            moneyCell.moneyIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, itemData.iconId)
        end
        moneyCell.moneyText.text = soldPrice.price
    end)
end

HL.Commit(SpaceshipSalesRecordsCell)
return SpaceshipSalesRecordsCell

