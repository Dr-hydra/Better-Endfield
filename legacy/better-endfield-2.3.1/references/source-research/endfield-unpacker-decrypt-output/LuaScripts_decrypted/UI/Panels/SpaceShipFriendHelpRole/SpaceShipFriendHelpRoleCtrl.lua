
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceShipFriendHelpRole

SpaceShipFriendHelpRoleCtrl = HL.Class('SpaceShipFriendHelpRoleCtrl', uiCtrl.UICtrl)






SpaceShipFriendHelpRoleCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SPACESHIP_USE_HELP_CREDIT] = 'OnUseHelpCredit',
    [MessageConst.ON_SPACESHIP_ASSIST_DATA_MODIFY] = 'OnDataModify',
    [MessageConst.ON_SPACESHIP_HEAD_NAVI_TARGET_CHANGE] = 'OnHeadCellNaviTargetChange',
}

SpaceShipFriendHelpRoleCtrl.m_getCharCell = HL.Field(HL.Function)

SpaceShipFriendHelpRoleCtrl.m_chosenCharIdList = HL.Field(HL.Table)

SpaceShipFriendHelpRoleCtrl.m_allCharInfos = HL.Field(HL.Table)

SpaceShipFriendHelpRoleCtrl.m_roomId = HL.Field(HL.String) << ""

SpaceShipFriendHelpRoleCtrl.m_maxCharNum = HL.Field(HL.Number) << 1

SpaceShipFriendHelpRoleCtrl.m_allCharInfoReverseMap = HL.Field(HL.Table)

SpaceShipFriendHelpRoleCtrl.m_nowNaviHeadCell = HL.Field(HL.Userdata)


SpaceShipFriendHelpRoleCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_roomId = Tables.spaceshipConst.controlCenterRoomId
    self.m_allCharInfoReverseMap = {}
    self.m_chosenCharIdList = {}
    self.m_allCharInfos = {}
    local spaceship = GameInstance.player.spaceship
    local index = 1
    for id, char in pairs(spaceship.characters) do
        local info = {
            id = id,
            char = char,
            staminaSort = -spaceship:GetCharCurStamina(id),
            workingSort = char.isWorking and 1 or 0,
            workingRoomSort = math.mininteger,
            stamina = spaceship:GetCharCurStamina(id)
        }
        if not string.isEmpty(char.stationedRoomId) then
            local roomTypeData = Tables.spaceshipRoomTypeTable[char.roomType]
            info.workingRoomSort = math.mininteger + roomTypeData.sortId
        end

        self.m_allCharInfos[index] = info
        index = index + 1
    end
    table.sort(self.m_allCharInfos, Utils.genSortFunction({"staminaSort", "workingRoomSort", "workingSort"}, false))
    for i, v in ipairs(self.m_allCharInfos) do
        self.m_allCharInfoReverseMap[v.id] = i
    end
    local beHelpedCreditLeft, beAssistTime = GameInstance.player.spaceship:GetCabinAssistedTime(self.m_roomId)
    self.m_maxCharNum = beHelpedCreditLeft
    self.view.choseTxt.text = string.format(Language.LUA_SPACESHIP_CHAR_CHOSE_CHAR_NUM, self.m_maxCharNum)
    self.view.terminalNode:InitSpaceShipFriendHelpTerminalNode(self.m_roomId, self)
    self.view.btnBack.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
        end)
    end)
    self.view.btnConfirm.gameObject:SetActive(false)
    self.view.btnConfirm.onClick:AddListener(function()
        GameInstance.player.spaceship:SpaceshipUseHelpRoomCreditControlCenter(self.m_roomId, self.m_chosenCharIdList)
    end)

    self:BindInputPlayerAction("ss_char_detail", function()
        if self.m_nowNaviHeadCell then
            self.m_nowNaviHeadCell:ShowTips()
        end
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

SpaceShipFriendHelpRoleCtrl.OnUseHelpCredit = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_SS_USE_HELP_CHAR_TOAST)
    self:Close()
end

SpaceShipFriendHelpRoleCtrl.OnShow = HL.Override() << function(self)
    self.m_getCharCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getCharCell(obj), LuaIndex(csIndex))
    end)
    self.view.scrollList:UpdateCount(#self.m_allCharInfos)
end

SpaceShipFriendHelpRoleCtrl.OnHide = HL.Override() << function(self)

end

SpaceShipFriendHelpRoleCtrl.OnClose = HL.Override() << function(self)

end

SpaceShipFriendHelpRoleCtrl.OnHeadCellNaviTargetChange = HL.Method(HL.Opt(HL.Userdata)) << function(self, cell)
    self.m_nowNaviHeadCell = cell
    self:_OnCellSelectedChanged(cell.m_charId, cell.view.button)
end

SpaceShipFriendHelpRoleCtrl._OnCellSelectedChanged = HL.Method(HL.String, HL.Any) << function(self, charId, target)
    local chosenIndex = lume.find(self.m_chosenCharIdList, charId)
    if chosenIndex then
        InputManagerInst:SetBindingText(target.hoverConfirmBindingId, Language.key_hint_common_unselect)
    else
        InputManagerInst:SetBindingText(target.hoverConfirmBindingId, Language.key_hint_common_select)
    end
end

SpaceShipFriendHelpRoleCtrl._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local charId = self.m_allCharInfos[index].id
    cell.gameObject.name = charId
    cell.ssCharHeadCell:InitSSCharHeadCell({
        charId = charId,
        disableFunc = function()
            return self:_CheckStaminaFullByCharIndex(index)
        end,
        onClick = function()
            self:_OnClickChar(index)
            self:_OnCellSelectedChanged(self.m_allCharInfos[index].id, cell.ssCharHeadCell.view.button)
        end,
        targetRoomId = ""
    })

    self:_UpdateCharChooseState(charId)
    if not self.m_nowNaviHeadCell and index == 1 then
        self:SetNaviTarget(cell.ssCharHeadCell.view.button)
        self:OnHeadCellNaviTargetChange(cell.ssCharHeadCell)
    end
end

SpaceShipFriendHelpRoleCtrl._CheckStaminaFullByCharIndex = HL.Method(HL.Number).Return(HL.Boolean) << function(self, index)
    return self.m_allCharInfos[index].stamina >= Tables.spaceshipConst.maxPhysicalStrength
end

SpaceShipFriendHelpRoleCtrl._OnClickChar = HL.Method(HL.Number) << function(self, index)
    local charInfo = self.m_allCharInfos[index]
    self:_ToggleChooseChar(charInfo.id, index)
end

SpaceShipFriendHelpRoleCtrl.OnDataModify = HL.Method() << function(self)
    local leftHelpCount, beHelpedCount = GameInstance.player.spaceship:GetCabinAssistedTime(self.m_roomId)
    if leftHelpCount == 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SS_USE_HELP_CHAR_TOAST)
        self:Close()
    end
end

SpaceShipFriendHelpRoleCtrl._ToggleChooseChar = HL.Method(HL.String, HL.Number) << function(self, charId, index)
    local chosenIndex = lume.find(self.m_chosenCharIdList, charId)
    if chosenIndex then

        table.remove(self.m_chosenCharIdList, chosenIndex)
        self:_UpdateCharChooseState(charId)
        for _, v in ipairs(self.m_chosenCharIdList) do
            self:_UpdateCharChooseState(v)
        end
    else

        if self:_CheckStaminaFullByCharIndex(self.m_allCharInfoReverseMap[charId]) then

            Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_CHAR_STAMINA_FULL_TOAST)
            return
        end

        local curCount = #self.m_chosenCharIdList
        if curCount >= self.m_maxCharNum then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_CHAR_CHOSE_CHAR_MAX_NUM_TOAST)
            return
        end
        table.insert(self.m_chosenCharIdList, charId)
        self:_UpdateCharChooseState(charId)
    end
    self.view.btnConfirm.gameObject:SetActive(#self.m_chosenCharIdList > 0)
end

SpaceShipFriendHelpRoleCtrl._UpdateCharChooseState = HL.Method(HL.String) << function(self, charId)
    local index = self.m_allCharInfoReverseMap[charId]
    if not index then
        return
    end
    local cell = self.m_getCharCell(index)
    if cell then
        self:_UpdateCharCellChooseState(charId, cell)
    end
end


SpaceShipFriendHelpRoleCtrl._UpdateCharCellChooseState = HL.Method(HL.String, HL.Table) << function(self, charId, cell)
    local chosenIndex = lume.find(self.m_chosenCharIdList, charId)
    if self.m_maxCharNum == 1 then
        cell.ssCharHeadCell:SetChooseState(chosenIndex ~= nil)
    else
        cell.ssCharHeadCell:SetChooseState(chosenIndex)
    end
    local preStamina = chosenIndex and Tables.spaceshipConst.centerBeHelpedRewardPhysicalStrength or 0
    cell.ssCharHeadCell:UpdateSSCharPreStamina(preStamina)
end

HL.Commit(SpaceShipFriendHelpRoleCtrl)
