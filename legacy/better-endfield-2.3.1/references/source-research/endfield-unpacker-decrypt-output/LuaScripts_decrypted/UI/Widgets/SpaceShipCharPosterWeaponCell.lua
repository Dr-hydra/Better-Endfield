local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SpaceShipCharPosterWeaponCell = HL.Class('SpaceShipCharPosterWeaponCell', UIWidgetBase)


SpaceShipCharPosterWeaponCell._OnFirstTimeInit = HL.Override() << function(self)

end

SpaceShipCharPosterWeaponCell.InitSpaceShipCharPosterWeaponCell = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()
    local itemInfo  = args.itemInfo or {}
    local item = self.view.item
    local enableControllerHoverTips = args.enableControllerHoverTips

    local count
    if itemInfo.itemInst ~= nil then
        count = 1
    else
        count = Utils.getBagItemCount(itemInfo.id)
    end
    local instId
    if itemInfo.itemInst then
        instId = itemInfo.itemInst.instId
    end
    item:InitItem({
        id = itemInfo.itemCfg.id,
        instId = instId,
        count = count,
    }, true)

    if enableControllerHoverTips ~= true then
        item:SetEnableHoverTips(not DeviceInfo.usingController)
    end

    self.view.extra.notEquipped.gameObject:SetActive(itemInfo.itemInst.equippedCharServerId == 0)

    self.view.extra.equipment.gameObject:SetActive(itemInfo.itemInst.equippedCharServerId > 0)

    self.view.extra.potentialStar:InitWeaponPotentialStar(itemInfo.itemInst.refineLv)
    self:SetSelectIndex(args.selectIndex)
end


SpaceShipCharPosterWeaponCell.SetSelectIndex = HL.Method(HL.Opt(HL.Number)) << function(self, index)
    if index then
        self.view.extra.textNum.text = index
        self.view.extra.selectedMarkMulti.gameObject:SetActive(true)
    else
        self.view.extra.selectedMarkMulti.gameObject:SetActive(false)
    end
end

HL.Commit(SpaceShipCharPosterWeaponCell)
return SpaceShipCharPosterWeaponCell

