local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FriendListCell = HL.Class('FriendListCell', UIWidgetBase)

FriendListCell.info = HL.Field(HL.Userdata)

FriendListCell.id = HL.Field(HL.Number) << 0

FriendListCell.charInfos = HL.Field(HL.Table)

FriendListCell.charInstanceIdList = HL.Field(HL.Table)

FriendListCell.charCellList = HL.Field(HL.Table)

FriendListCell.arg = HL.Field(HL.Table)

FriendListCell.m_clueCells = HL.Field(HL.Forward("UIListCache"))

FriendListCell.showCharInfoCallBack = HL.Field(HL.Function)

FriendListCell.searchKey = HL.Field(HL.String) << ""

FriendListCell.m_buildNodeId = HL.Field(HL.Number) << 0


FriendListCell._OnFirstTimeInit = HL.Override() << function(self)
    self.showCharInfoCallBack = function()


        self:_OnCharInfoClick(1)
    end

    self.view.headBtn.onClick:RemoveAllListeners()
    self.view.headBtn.onClick:AddListener(function()
        logger.info("点击好友头像")
        if self.arg.onPlayerClick then
            self.arg.onPlayerClick(self.view.headRectTransform, self.info.roleId, self.showCharInfoCallBack)
        end
    end)
    self.view.shipBtn.onClick:RemoveAllListeners()
    self.view.shipBtn.onClick:AddListener(function()
        logger.info("点击好友飞船")
        if self.arg.onShipClick then
            self.arg.onShipClick(self.info.roleId)
        end
    end)
    self.view.msgBtn.onClick:RemoveAllListeners()
    self.view.msgBtn.onClick:AddListener(function()
        logger.info("点击好友消息")
        if self.arg.onMessageClick then
            self.arg.onMessageClick(self.info.roleId)
        end
    end)
    self.view.acceptFriendBtn.onClick:RemoveAllListeners()
    self.view.acceptFriendBtn.onClick:AddListener(function()
        logger.info("点击acceptFriend")
        if self.arg.onAcceptClick then
            self.arg.onAcceptClick(self.info.roleId)
        end
    end)
    self.view.notAcceptFriendBtn.onClick:RemoveAllListeners()
    self.view.notAcceptFriendBtn.onClick:AddListener(function()
        logger.info("点击notAccept消息")
        if self.arg.onNotAcceptClick then
            self.arg.onNotAcceptClick(self.info.roleId)
        end
    end)
    self.view.removeBlackListBtn.onClick:RemoveAllListeners()
    self.view.removeBlackListBtn.onClick:AddListener(function()
        logger.info("点击Remove消息")
        if self.arg.onRemoveClick then
            self.arg.onRemoveClick(self.info.roleId)
        end
    end)
    self.view.addFriendBtn.onClick:RemoveAllListeners()
    self.view.addFriendBtn.onClick:AddListener(function()
        logger.info("点击Add消息")
        FriendUtils.FRIEND_CELL_INIT_FUNC.onAddClick(self.info.roleId, self.m_buildNodeId)
    end)

    self.view.chatBtn.onClick:RemoveAllListeners()
    self.view.chatBtn.onClick:AddListener(function()
        logger.info("点击 chatBtn")
        if self.arg.onChatClick then
            self.arg.onChatClick(self.info.roleId)
        end
    end)

    self.view.giftBtn.onClick:RemoveAllListeners()
    self.view.giftBtn.onClick:AddListener(function()
        logger.info("点击 giftBtn")
        if self.arg.onGiftBtnClick then
            self.arg.onGiftBtnClick(self.info.roleId, function()
                self:_UpdateClueCells()
            end)
        end
    end)

    self.view.spreadBtn.onClick:RemoveAllListeners()
    self.view.spreadBtn.onClick:AddListener(function()
        logger.info("点击 spreadBtn")
        if self.arg.onSpaceshipVisitorClick then
            self.arg.onSpaceshipVisitorClick(self.info.roleId, self.view.spreadIconRect)
        end
    end)

    self.view.psnInfoBtnForBlackUser.onClick:RemoveAllListeners()
    self.view.psnInfoBtnForBlackUser.onClick:AddListener(function()
        logger.info("点击 psnInfoBtn")
        if self.arg.onPsnInfoClick then
            self.arg.onPsnInfoClick(self.info.psnData.AccountId)
        end
    end)
    self.view.psnInfoBtnForNoGameUser.onClick:RemoveAllListeners()
    self.view.psnInfoBtnForNoGameUser.onClick:AddListener(function()
        logger.info("点击 psnInfoBtn")
        if self.arg.onPsnInfoClick then
            self.arg.onPsnInfoClick(self.info.psnData.AccountId)
        end
    end)
    self:RegisterMessage(MessageConst.ON_SPACESHIP_CLUE_INFO_CHANGE, function()
        self:_RefreshFriendCellInfo()
    end)
end

FriendListCell.SetLoadingState = HL.Method() << function(self)
    self.view.emptyStateCtrl:SetState("Loading")
end

FriendListCell.RefreshFriendListCell = HL.Method(HL.Number, HL.Table, HL.String) << function(self, id, arg, searchKey)
    self.view.psnInfoBtnForBlackUser.gameObject:SetActiveIfNecessary(false)
    self.id = id
    self.arg = arg
    self.searchKey = searchKey
    self:_FirstTimeInit()

    local stateName = arg.stateName
    self.view.emptyStateCtrl:SetState("Normal")
    if stateName == "Stranger" then
        if GameInstance.player.friendSystem.friendInfoDic:ContainsKey(id) then
            self.view.stateController:SetState(stateName .. "_Friend")
        elseif GameInstance.player.friendSystem:ContainWaitAcceptRequestID(id) then
            self.view.stateController:SetState(stateName .. "_Wait")
        else
            self.view.stateController:SetState(stateName)
        end
    else
        self.view.stateController:SetState(stateName)
    end

    local showInfo = nil
    if stateName == "SpaceshipVisitor" then
        local success, info = GameInstance.player.friendSystem:TryGetFriendInfo(self.id)
        if success and info.init then
            showInfo = info
        else
            logger.info("未找到好友数据 " .. self.id)
            self.view.emptyStateCtrl:SetState("NoData")
            return
        end
    else
        local success, info = GameInstance.player.friendSystem:GetDictInfo(arg.infoDicIndex):TryGetValue(self.id)
        if not success then
            logger.info("未找到好友数据 " .. self.id)
            local friendInfo = GameInstance.player.spaceship:GetFriendRoleInfo()
            if GameInstance.player.spaceship.isViewingFriend and friendInfo and friendInfo.roleId == self.id then
                self.view.emptyStateCtrl:SetState("NoData_Spaceship")
            else
                self.view.emptyStateCtrl:SetState("NoData")
            end
            return
        end
        showInfo = info
    end

    if showInfo.init == false then
        logger.error("好友数据未初始化 " .. self.id)
    end

    self.info = showInfo
    self:_RefreshFriendCellInfo()

    if FriendUtils.isPsnPlatform() and self.info.lastLoginType ~= CS.Proto.HG_THIRD_ACCOUNT_TYPE.AccountTypePsn then
        self.view.commonPlayerHead.view.pcNode.gameObject:SetActiveIfNecessary(true)
        self.view.commonPlayerHead.view.psRoot.gameObject:SetActiveIfNecessary(false)
    end
    if self.view.msgBtn.gameObject.activeSelf then
        self.view.msgBtn.gameObject:SetActive(GameInstance.player.friendSystem:PlayerInBlackList(self.info.roleId) == false)
    end
end

FriendListCell.RefreshFriendListCellByPsnId = HL.Method(HL.String, HL.Table, HL.String) << function(self, id, arg, searchKey)
    self.arg = arg
    self.searchKey = searchKey

    local stateName = arg.stateName
    self.view.emptyStateCtrl:SetState("Normal")

    local success, info = GameInstance.player.friendSystem:GetPsnDictByIndex(self.arg.infoDicIndex):TryGetValue(id)

    if not success then
        logger.error(CS.Beyond.ELogChannel.Friend, "未找到好友数据 " .. id)
        self.view.emptyStateCtrl:SetState("NoData")
        return
    end
    self.view.addFriendBtn.onClick:ChangeBindingPlayerAction("friend_add_psn")
    self.id = info.roleId
    self:_FirstTimeInit()

    if stateName == "Normal" then

        if info.roleId == 0 then
            self:_UpdateUnRegisterPlayer(info)
            return
        end


        if GameInstance.player.friendSystem.blackListInfoDic:ContainsKey(info.roleId) then
            stateName = "BlackList"
        elseif GameInstance.player.friendSystem:ContainWaitAcceptRequestID(info.roleId) then
            stateName = "Stranger_Wait"
        elseif not GameInstance.player.friendSystem.friendInfoDic:ContainsKey(info.roleId) then
            stateName = "Stranger"
        end
    elseif stateName == "BlackList" and info.roleId == 0 then
        self:_UpdateUnRegisterPlayer(info)
        self.view.playerHeadBtnForNoGameUser.enabled = false
        self.view.psnInfoBtnForBlackUser.gameObject:SetActiveIfNecessary(false)
        self.view.psnInfoBtnForNoGameUser.gameObject:SetActiveIfNecessary(false)
        return
    end
    self.view.psnInfoBtnForBlackUser.gameObject:SetActiveIfNecessary(false)
    self.view.stateController:SetState(stateName)
    self.info = info
    self:_RefreshFriendCellInfo()
    if stateName == "BlackList" then

        self.view.removeBlackListBtn.gameObject:SetActiveIfNecessary(false)
    end
    if self.view.msgBtn.gameObject.activeSelf then
        self.view.msgBtn.gameObject:SetActive(not GameInstance.player.spaceship.isViewingFriend)
    end
end

FriendListCell._UpdateUnRegisterPlayer = HL.Method(HL.Any) << function(self, info)

    self.view.emptyStateCtrl:SetState("NonGameUser")
    self.view.sonyNameTxt.text = info.psName
    self.info = info

    self.view.commonPlayerHead.view.levelTag.gameObject:SetActiveIfNecessary(false)
    self.view.commonPlayerHead.view.seasonTag.gameObject:SetActiveIfNecessary(false)
    self.view.commonPlayerHead.view.lineImage.gameObject:SetActiveIfNecessary(false)
    self.view.playerHeadBtnForNoGameUser.enabled = true
    self.view.playerHeadBtnForNoGameUser.onClick:RemoveAllListeners()
    self.view.playerHeadBtnForNoGameUser.onClick:AddListener(function()

        if GameInstance.player.friendSystem.psnBlackListFriendList:ContainsKey(self.info.psnData.AccountId) then
            return
        end

        local args = {
            transform = self.view.headRectTransform,
            cellHeight = FriendUtils.CELL_HEIGHT,
            actions = {
                [1] = {
                    text = Language.LUA_FRIEND_TIP_PSN_INFO,
                    action = function()
                        FriendUtils.FRIEND_CELL_INIT_FUNC.onPsnInfoClick(self.info.psnData.AccountId)
                    end
                }
            }
        }
        Notify(MessageConst.SHOW_NAVI_TARGET_ACTION_MENU, args)
    end)
end

FriendListCell._RefreshFriendCellInfo = HL.Method() << function(self)
    if not self.info then
        return
    end
    self.charInfos = {}

    self.view.commonPlayerHead:UpdateHideSignature(self.arg.hideSignature)

    local hasValue, _ = GameInstance.player.spaceship:TryGetRoom(Tables.spaceshipConst.guestRoomId)
    local showShipBtn = hasValue and self.view.shipBtn.gameObject.activeSelf
    self.view.shipBtn.gameObject:SetActiveIfNecessary(showShipBtn)

    local onPlayerHeadClick = self.arg.onPlayerClick and function()
        if self.arg.stateName == "SpaceshipClueGift" then
            if self.arg.onPlayerClick then
                self.arg.onPlayerClick(self.info.roleId)
            end
            return
        end


        local isBlack = GameInstance.player.friendSystem.blackListInfoDic:ContainsKey(self.info.roleId)
        if isBlack then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_IN_BLACK_LIST)
            return
        end
        local isStranger = not GameInstance.player.friendSystem.friendInfoDic:ContainsKey(self.info.roleId)

        if isStranger then
            FriendUtils.FRIEND_CELL_INIT_FUNC.onStrangerPlayerClick(self.view.headRectTransform, self.info.roleId, self.showCharInfoCallBack)
        else
            local friendInfo = GameInstance.player.spaceship:GetFriendRoleInfo()
            local lshowShipBtn = friendInfo ~= nil and friendInfo.roleId ~= self.info.roleId and showShipBtn and self.info.guestRoomUnlock
            FriendUtils.FRIEND_CELL_INIT_FUNC.onFriendPlayerClick(self.view.headRectTransform, self.info.roleId, self.showCharInfoCallBack, lshowShipBtn)
        end
    end or false

    self.view.commonPlayerHead:InitCommonPlayerHeadByRoleId(self.info.roleId, onPlayerHeadClick, self.searchKey, false)

    if self.arg.showVisitorTimeText then
        self.view.visitorState.gameObject:SetActive(true)
        self.view.onlineState.gameObject:SetActive(false)
        self.view.visitorTxt.text = self.arg.showVisitorTimeText
    else
        self.view.visitorState.gameObject:SetActive(false)
        self.view.onlineState.gameObject:SetActive(true)
    end

    self.view.onlineState:SetState(self.info.playerOnlineState:ToString())
    if self.info.playerOnlineState == CS.Beyond.Gameplay.PlayerOnlineState.Online then
        self.view.onlineTimeTxt.text = Language.LUA_FRIEND_ONLINE
    elseif self.info.lastDateTime ~= 0 then
        self.view.onlineTimeTxt.text = string.format(Language.LUA_FRIEND_LAST_ONLINE_TIME, UIUtils.getLeftTime(DateTimeUtils.GetCurrentTimestampBySeconds() - self.info.lastDateTime))
    else
        self.view.onlineTimeTxt.text = ""
    end

    self.charCellList = {}
    self.charInstanceIdList = {}
    for i = 1, FriendUtils.FriendShowCharsCount do
        local state = self.view.charInfoGroup["charCellRoot" .. i]
        if i <= self.info.charInfos.Count and self.info.charInfos[CSIndex(i)] then
            local item = self.info.charInfos[CSIndex(i)]
            local charConfig = Tables.characterTable:GetValue(item.templateId)
            self.charInfos[i] = {
                templateId = item.templateId,
                instId = item.instId,
                level = item.level,
                ownTime = 0,
                rarity = charConfig.rarity,
                potentialLevel = item.potentialLevel,
                noHpBar = true,

                singleSelect = false,
                selectIndex = -1,
            }
            self.charInstanceIdList[i] = item.instId
            local cell = self.view.charInfoGroup["charHeadCell" .. i]
            state:SetState("Normal")
            cell:InitCharFormationHeadCell(self.charInfos[i], function()
                self:_OnCharInfoClick(i)
            end)
        else
            self.charInfos[i] = {}
            state:SetState("NoChar")
        end
    end

    local chatInfo = GameInstance.player.friendChatSystem:GetChatInfo(self.info.roleId)
    if chatInfo then
        if chatInfo.unReadNum > 0 then
            self.view.messageNumNode.gameObject:SetActive(true)
            self.view.msgRedDotNumber.gameObject:SetActive(true)

            self.view.messageNumText.text = chatInfo.unReadNum > 99 and "99+" or tostring(chatInfo.unReadNum)
        else
            self.view.messageNumNode.gameObject:SetActive(false)
            self.view.msgRedDotNumber.gameObject:SetActive(false)
        end
    else
        self.view.messageNumNode.gameObject:SetActive(false)
        self.view.msgRedDotNumber.gameObject:SetActive(false)
    end


    if self.view.shipBtn.gameObject.activeSelf then
        local friendInfo = GameInstance.player.spaceship:GetFriendRoleInfo()
        if friendInfo and friendInfo.roleId == self.info.roleId then
            self.view.shipBtn.enabled = false
            self.view.shipBtnStateController:SetState("CurState")
            self.view.lockMask.gameObject:SetActive(false)
        elseif not self.info.guestRoomUnlock then
            self.view.shipBtn.enabled = false
            self.view.lockMask.gameObject:SetActive(true)
            self.view.shipBtnStateController:SetState("LockState")
        else
            self.view.shipBtn.enabled = true
            self.view.shipBtnStateController:SetState("NormalState")
            self.view.lockMask.gameObject:SetActive(false)
        end
        local isCurrent = self.info.helpFlag ~= CS.Proto.FRIEND_SPACESHIP_HELP_STATUS.CanHelp and "CompleteState" or "IncompleteState"
        self.view.shipHelp.gameObject:SetActiveIfNecessary(self.info.helpFlag ~= CS.Proto.FRIEND_SPACESHIP_HELP_STATUS.Invalid)
        self.view.shipHelp:SetState(isCurrent)

        local isJoin = GameInstance.player.spaceship:CheckIsJoinFriendClueExchange(self.info.roleId)
        isCurrent = isJoin and "CompleteState" or "IncompleteState"
        self.view.clueExchange.gameObject:SetActiveIfNecessary(self.info.clueFlag or isJoin)
        self.view.clueExchange:SetState(isCurrent)
    end

    if self.info.businessCardTopicId ~= nil then
        local success, topicCfg = Tables.businessCardTopicTable:TryGetValue(self.info.businessCardTopicId)
        if success then
            self.view.themeImg:LoadSprite(UIConst.UI_BUSINESS_CARD_ICON_PATH, topicCfg.id)
            self.view.themeImagePair.color = UIUtils.getColorByString(topicCfg.color)
        else
            logger.error("未找到名片主题配置 " .. self.info.businessCardTopicId)
        end
    else
        logger.error("好友名片主题ID为空 " .. self.info.roleId)
    end

    if self.arg.stateName == "SpaceshipClueGift" then
        self:_UpdateClueCells()
    end


    if self.arg.showContingencyContractActivityState then
        self.view.commonPlayerHead:UpdateContingencyContractActivityState()
    end
    self.view.commonPlayerHead:UpdateSeasonTowerActivityState()
end

FriendListCell._UpdateClueCells = HL.Method() << function(self)
    self.m_clueCells = self.m_clueCells or UIUtils.genCellCache(self.view.clueNode)

    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local clueDict = {}
    for key, value in pairs(self.info.hostClueStatus) do
        if value == 0 or value > curTime then
            clueDict[key] = true
        end
    end

    self.m_clueCells:Refresh(7, function(cell, luaIndex)
        if self.arg.selectedClueId == luaIndex then
            if clueDict[luaIndex] == true then

                cell.stateController:SetState("Select01")
            else
                cell.stateController:SetState("Select02")
            end
        else
            if clueDict[luaIndex] == true then
                cell.stateController:SetState("NotNeed")
            else
                cell.stateController:SetState("Need")
            end
        end

        cell.clueNumTxt.text = luaIndex
    end)
end

FriendListCell._OnCharInfoClick = HL.Method(HL.Number) << function(self, cellIndex)
    if GameInstance.player.friendSystem:PlayerInBlackList(self.info.roleId) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_IN_BLACK_LIST)
        return
    end
    local templateIdList = {}
    for _, v in pairs(self.charInfos) do
        table.insert(templateIdList, v.templateId)
    end
    FriendUtils.openFriendCharInfo(self.info.roleId, self.info.charInfos[CSIndex(cellIndex)].templateId, templateIdList)
end

FriendListCell._OnEnable = HL.Override() << function(self)
    if not self.info then
        return
    end
    local chatInfo = GameInstance.player.friendChatSystem:GetChatInfo(self.info.roleId)
    if chatInfo then
        if chatInfo.unReadNum > 0 then
            self.view.messageNumNode.gameObject:SetActive(true)
            self.view.msgRedDotNumber.gameObject:SetActive(true)

            self.view.messageNumText.text = chatInfo.unReadNum > 99 and "99+" or tostring(chatInfo.unReadNum)
        else
            self.view.messageNumNode.gameObject:SetActive(false)
            self.view.msgRedDotNumber.gameObject:SetActive(false)
        end
    else
        self.view.messageNumNode.gameObject:SetActive(false)
        self.view.msgRedDotNumber.gameObject:SetActive(false)
    end
end

HL.Commit(FriendListCell)
return FriendListCell

