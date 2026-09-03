local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FriendVisit

FriendVisitCtrl = HL.Class('FriendVisitCtrl', uiCtrl.UICtrl)






FriendVisitCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FriendVisitCtrl.OpenFriendVisit = HL.StaticMethod(HL.Any) << function(msg)
    UIManager:AutoOpen(PANEL_ID, msg)
end


FriendVisitCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    self.view.cancelButton.onClick:RemoveAllListeners()
    self.view.cancelButton.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    local msg = unpack(arg)
    self.view.confirmButton.onClick:RemoveAllListeners()
    self.view.confirmButton.onClick:AddListener(function()
        GameInstance.player.spaceship:VisitFriendSpaceShip(msg.RoleId)
        self:PlayAnimationOutAndClose()
    end)


    local name, _, __ = FriendUtils.getFriendInfoByRoleId(msg.RoleId, "")
    self.view.visitFriendName.text = string.format(Language.LUA_FRIEND_VISIT_VISIT_NAME, name)
    local clueRootActive = msg.ClueEndTs > DateTimeUtils.GetCurrentTimestampBySeconds()
    self.view.clueRoot.gameObject:SetActive(clueRootActive)
    self.view.countDownText:InitCountDownText(msg.ClueEndTs)

    local boolTable = {}
    if msg.ControlCenterFlag then
        table.insert(boolTable,GEnums.SpaceshipRoomType.ControlCenter)
    end
    if msg.ManufacturingStationFlag then
        table.insert(boolTable,GEnums.SpaceshipRoomType.ManufacturingStation)
    end
    if msg.GrowCabinFlag then
        table.insert(boolTable,GEnums.SpaceshipRoomType.GrowCabin)
    end
    if msg.CommandCenterFlag then
        table.insert(boolTable,GEnums.SpaceshipRoomType.CommandCenter)
    end
    if msg.HasJoinedInfoExchange then
        table.insert(boolTable,GEnums.SpaceshipRoomType.InfoExchange)
    end

    self.view.roomRoot.gameObject:SetActive(#boolTable > 0)

    self.view.visitSubTitle.gameObject:SetActive((#boolTable > 0 or clueRootActive))

    local genRoomImageCell = UIUtils.genCellCache(self.view.roomCell)
    genRoomImageCell:Refresh(#boolTable, function(cell, cellIndex)
        local cfg = Tables.spaceshipRoomTypeTable:GetValue(boolTable[cellIndex])
        cell.Img:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, cfg.icon)
        cell.color.color = UIUtils.getColorByString(cfg.color)
    end)
end











HL.Commit(FriendVisitCtrl)
