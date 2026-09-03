
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceShipFriendHelpList

SpaceShipFriendHelpListCtrl = HL.Class('SpaceShipFriendHelpListCtrl', uiCtrl.UICtrl)

SpaceShipFriendHelpListCtrl.m_roomId = HL.Field(HL.String) << ""

SpaceShipFriendHelpListCtrl.m_showFriendBusinessCardBindingId = HL.Field(HL.Number) << -1

SpaceShipFriendHelpListCtrl.m_nowNaviFriendCell = HL.Field(HL.Userdata)





SpaceShipFriendHelpListCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FRIEND_INFO_SYNC] = 'OnRecvQueryInfo',
    [MessageConst.ON_SPACESHIP_USE_HELP_CREDIT] = 'OnUseHelpCredit',
    [MessageConst.ON_SPACESHIP_ASSIST_DATA_MODIFY] = 'OnUseHelpCredit',
}


SpaceShipFriendHelpListCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if arg and arg.roomId then
        self.m_roomId = arg.roomId
    else
        logger.error("[SpaceShipFriendHelpList]: arg no roomId")
    end
    self.view.closeButton.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
        end)
    end)

    self.view.btnInviteFriend.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.Friend, { panelId = PanelId.FriendList })
    end)

    self.view.btnUseHelp.onClick:AddListener(function()
        local hasValue, roomInfo = GameInstance.player.spaceship:TryGetRoom(self.m_roomId)
        if not hasValue then
            return
        end
        if roomInfo.type == GEnums.SpaceshipRoomType.ControlCenter  then
            UIManager:AutoOpen(PanelId.SpaceShipFriendHelpRole)
        elseif roomInfo.type == GEnums.SpaceshipRoomType.ManufacturingStation then
            local formulaId = GameInstance.player.spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId)
            if string.isEmpty(formulaId) then
                Notify(MessageConst.SHOW_TOAST, I18nUtils.GetText("ui_spaceship_friendhelproom_no_formula_toast"))
            else
                UIManager:AutoOpen(PanelId.SpaceShipFriendHelpRoom, { roomId = self.m_roomId, formulaId = formulaId})
            end
        end
    end)
    self.m_showFriendBusinessCardBindingId = self:BindInputPlayerAction("ss_show_friend_business_card", function()
        if self.m_nowNaviFriendCell then
            self.m_nowNaviFriendCell:OnClick()
        end
    end, self.view.assistListNodeInputBindingGroupMonoTarget.groupId)
    InputManagerInst:ToggleBinding(self.m_showFriendBusinessCardBindingId, false)
    self.view.assistListNode.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            self.m_nowNaviFriendCell = nil
            InputManagerInst:ToggleBinding(self.m_showFriendBusinessCardBindingId, false)
        end
    end)
    self:_InitData()
    GameInstance.player.friendSystem:SyncFriendSimpleInfo()
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

SpaceShipFriendHelpListCtrl.OnShow = HL.Override() << function(self)

end

SpaceShipFriendHelpListCtrl.OnHide = HL.Override() << function(self)

end

SpaceShipFriendHelpListCtrl.OnClose = HL.Override() << function(self)

end

SpaceShipFriendHelpListCtrl.OnRecvQueryInfo = HL.Method() << function(self)
    self:_RefreshData()
end


SpaceShipFriendHelpListCtrl.OnUseHelpCredit = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    if GameInstance.player.spaceship.isViewingFriend then
        return
    end
    local roomId = arg and arg[1] or self.m_roomId
    self:_RefreshData(roomId)
end

SpaceShipFriendHelpListCtrl._InitData = HL.Method(HL.Opt(HL.String)) << function(self, roomId)
    roomId = roomId or self.m_roomId
    local helpedRoleIds = GameInstance.player.spaceship:GetBeHelpedRecordFriendRoleIdsByRoomId(roomId)
    local beHelpedCreditLeft, beAssistTime = GameInstance.player.spaceship:GetCabinAssistedTime(roomId)
    self.view.terminalNode:InitSpaceShipFriendHelpTerminalNode(roomId, self)
    local helpLimit = SpaceshipUtils.getRoomHelpLimit(roomId)
    local _, roomInfo = GameInstance.player.spaceship:TryGetRoom(roomId)
    local isGrowCabin = roomInfo.type == GEnums.SpaceshipRoomType.GrowCabin
    local isCC = roomInfo.type == GEnums.SpaceshipRoomType.ControlCenter
    local isManufacturing = roomInfo.type == GEnums.SpaceshipRoomType.ManufacturingStation
    local hasCreditLeft = beHelpedCreditLeft > 0

    self.view.btnUseHelp.gameObject:SetActive(hasCreditLeft and (isCC or isManufacturing))
    self.view.btnInviteFriend.gameObject:SetActive(helpedRoleIds.Count < helpLimit)

    self.view.hintNode.gameObject:SetActive(isGrowCabin and beAssistTime < beHelpedCreditLeft)
end


SpaceShipFriendHelpListCtrl._RefreshData = HL.Method(HL.Opt(HL.String)) << function(self, roomId)
    roomId = roomId or self.m_roomId
    self:_InitData(roomId)
    local helpedRoleIds = GameInstance.player.spaceship:GetBeHelpedRecordFriendRoleIdsByRoomId(roomId)
    local beHelpedCreditLeft, beAssistTime = GameInstance.player.spaceship:GetCabinAssistedTime(roomId)
    self.view.terminalNode:InitSpaceShipFriendHelpTerminalNode(roomId, self)
    local helpLimit = SpaceshipUtils.getRoomHelpLimit(roomId)
    for index = 1, helpLimit do
        local helpNode = self.view["helpTypeNode".. index]
        if not helpNode then
            break
        end
        if index <= helpedRoleIds.Count then
            local beHelpedFriendRoleId = helpedRoleIds[CSIndex(index)]
            if not GameInstance.player.friendSystem.friendInfoDic:ContainsKey(beHelpedFriendRoleId) then
                helpNode.stateController:SetState("NoData")
            else
                helpNode.stateController:SetState("Friend")
                local friendDicIndex = FriendUtils.FRIEND_CELL_INIT_CONFIG.Friend.infoDicIndex
                local haveFriend, info = GameInstance.player.friendSystem.friendInfoDic:TryGetValue(beHelpedFriendRoleId)
                local nameStr, avatarPath, avatarFramePath = FriendUtils.getFriendInfoByRoleId(beHelpedFriendRoleId)
                helpNode.nameTxt.text = nameStr
                helpNode.commonPlayerHead:InitCommonPlayerHeadByRoleId(beHelpedFriendRoleId, function()
                    Notify(MessageConst.ON_OPEN_BUSINESS_CARD_PREVIEW, { roleId = beHelpedFriendRoleId, isPhase = true })
                end)
                helpNode.themeBg.gameObject:SetActive(info.businessCardTopicId ~= nil)
                if info.businessCardTopicId then
                    local success, topicCfg = Tables.businessCardTopicTable:TryGetValue(info.businessCardTopicId)
                    if success then
                        helpNode.themeImg:LoadSprite(UIConst.UI_BUSINESS_CARD_ICON_PATH, topicCfg.id)
                    end
                end
                helpNode.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:RemoveAllListeners()
                helpNode.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:AddListener(function(select)
                    if select then
                        self.m_nowNaviFriendCell = helpNode.commonPlayerHead
                        InputManagerInst:ToggleBinding(self.m_showFriendBusinessCardBindingId, true)
                    end
                end)
            end
        else
            helpNode.stateController:SetState("Wait")
            helpNode.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:RemoveAllListeners()
            helpNode.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:AddListener(function(select)
                if select then
                    self.m_nowNaviFriendCell = nil
                    InputManagerInst:ToggleBinding(self.m_showFriendBusinessCardBindingId, false)
                end
            end)
        end
    end

    local _, roomInfo = GameInstance.player.spaceship:TryGetRoom(roomId)
    local isGrowCabin = roomInfo.type == GEnums.SpaceshipRoomType.GrowCabin
    local isCC = roomInfo.type == GEnums.SpaceshipRoomType.ControlCenter
    local isManufacturing = roomInfo.type == GEnums.SpaceshipRoomType.ManufacturingStation
    local hasCreditLeft = beHelpedCreditLeft > 0

    self.view.btnUseHelp.gameObject:SetActive(hasCreditLeft and (isCC or isManufacturing))
    self.view.btnInviteFriend.gameObject:SetActive(helpedRoleIds.Count < helpLimit)

    self.view.hintNode.gameObject:SetActive(isGrowCabin and beAssistTime < beHelpedCreditLeft)
end

HL.Commit(SpaceShipFriendHelpListCtrl)
