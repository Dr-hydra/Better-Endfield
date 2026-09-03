local LevelWorldUIBase = require_ex('UI/Widgets/LevelWorldUIBase')

SSRoomCharInfoPanel = HL.Class('SSRoomCharInfoPanel', LevelWorldUIBase)

local SS_ROOM_CHAR_INFO_CONST = {
    MAX_CHAR_SLOT_CNT = 3,
    ROLE_NODE_STATES = {
        LOCKED = "Locked",
        EMPTY = "Empty",
        STATIONED = "Stationed"
    }
}

SSRoomCharInfoPanel.m_roomId = HL.Field(HL.String) << ""

SSRoomCharInfoPanel.m_charListCache = HL.Field(HL.Forward("UIListCache"))

SSRoomCharInfoPanel.m_showing = HL.Field(HL.Boolean) << false


SSRoomCharInfoPanel._OnFirstTimeInit = HL.Override() << function(self)
    self.m_charListCache = UIUtils.genCellCache(self.view.roleNode)
end

SSRoomCharInfoPanel.InitLevelWorldUi = HL.Override(HL.Any) << function(self, args)
    self:_FirstTimeInit()
    self.m_roomId = args[CSIndex(1)]
    self.view.animation.alpha = 0
    local args = {}
    args.roomId = self.m_roomId
    args.charInfoPanel = self
    Notify(MessageConst.SS_REGISTER_CHAR_INFO_PANEL, args)
end

SSRoomCharInfoPanel.OnLevelWorldUiReleased = HL.Override() << function(self)
    local cacheListItems = self.m_charListCache:GetItems()
    self.m_charListCache:Refresh(0, nil, false, function(item, index)
        cacheListItems[index] = nil
        GameObject.Destroy(item.gameObject)
    end)
    Notify(MessageConst.SS_UNREGISTER_CHAR_INFO_PANEL, self.m_roomId)
end

SSRoomCharInfoPanel._InitCharInfo = HL.Method(GEnums.SpaceshipRoomType) << function(self, roomType)
    local isManufacturing = roomType == CS.Beyond.GEnums.SpaceshipRoomType.ManufacturingStation
    local isGrowCabin = roomType == CS.Beyond.GEnums.SpaceshipRoomType.GrowCabin
    self.view.iconM.gameObject:SetActive(isManufacturing)
    self.view.iconGC.gameObject:SetActive(isGrowCabin)
end

SSRoomCharInfoPanel.UpdateCharInfo = HL.Method(HL.Number, HL.Any) << function(self, maxCharCount, charList)
    local charNumText = string.format("%d/%d", charList and charList.Count or 0, maxCharCount)
    self.view.charNumText.text = charNumText
    self.view.charNumProjText.text = charNumText
    self.m_charListCache:Refresh(SS_ROOM_CHAR_INFO_CONST.MAX_CHAR_SLOT_CNT, function(cell, luaIndex)

        local stateController = cell.simpleStateController

        local charHeadCell = cell.ssCharHeadCell
        if not charList or luaIndex > charList.Count then
            if luaIndex > maxCharCount then
                stateController:SetState(SS_ROOM_CHAR_INFO_CONST.ROLE_NODE_STATES.LOCKED)
            else
                stateController:SetState(SS_ROOM_CHAR_INFO_CONST.ROLE_NODE_STATES.EMPTY)
            end
            return
        end
        stateController:SetState(SS_ROOM_CHAR_INFO_CONST.ROLE_NODE_STATES.STATIONED)
        local charId = charList[CSIndex(luaIndex)]
        local args = {}
        args.charId = charId
        args.targetRoomId = self.m_roomId
        charHeadCell:InitSSCharHeadCell(args)
    end)
    local newShowing = maxCharCount > 0
    if newShowing ~= self.m_showing then
        if newShowing then


            local spaceship = GameInstance.player.spaceship

            local _, roomInfo = spaceship:TryGetRoom(self.m_roomId)
            local roomType = roomInfo.roomType
            self:_InitCharInfo(roomType)
        end

        self.m_showing = newShowing
        if newShowing then
            self.view.content.gameObject:SetActive(true)
            self.view.content:PlayInAnimation()
        else
            self.view.content:PlayOutAnimation(function()
                self.view.content.gameObject:SetActive(false)
            end)
        end
    end
end

HL.Commit(SSRoomCharInfoPanel)
return SSRoomCharInfoPanel

