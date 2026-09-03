local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityRanking
local PHASE_ID = PhaseId.ActivityRanking

local IdentityTypeState = {
    Self = "Self",
    Other = "Other",
    NPC = "NPC",
}

local RankingState = {
    Ranking1st = "Ranking1st",
    Ranking2nd = "Ranking2nd",
    Ranking3rd = "Ranking3rd",
    RankingHigh = "RankingHigh",
    RankingNormal = "RankingNormal",
}

local PanelState = {
    Normal = "Nrl",
    Loading = "Loading",
}

local LocateSelfAnim = "activityrankinglistcellplayer_in"

local floorRankValueConverter = function(value)
    return math.floor(value / 1000)
end

ActivityRankingCtrl = HL.Class('ActivityRankingCtrl', uiCtrl.UICtrl)

ActivityRankingCtrl.m_activityId = HL.Field(HL.String) << ""

ActivityRankingCtrl.m_rankRelatedId = HL.Field(HL.String) << ""

ActivityRankingCtrl.m_genRankListCellFunc = HL.Field(HL.Function)

ActivityRankingCtrl.m_rawRankInfoDic = HL.Field(HL.Userdata)

ActivityRankingCtrl.m_rankInfo = HL.Field(HL.Table)

ActivityRankingCtrl.m_selfRankInfo = HL.Field(HL.Table)

ActivityRankingCtrl.m_rankInfoReady = HL.Field(HL.Boolean) << false

ActivityRankingCtrl.m_roleSimpleInfoReady = HL.Field(HL.Boolean) << false

ActivityRankingCtrl.m_scrollToSelfTag = HL.Field(HL.Boolean) << false





ActivityRankingCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_REQ_RANK_LIST] = 'OnActivityReqRankList',
    [MessageConst.ON_FRIEND_INFO_SYNC] = "OnFriendInfoSync",
}


ActivityRankingCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData(arg)
    self:_InitView()
end





ActivityRankingCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.friendSystem:ClearSyncCallback()
end

ActivityRankingCtrl.OnActivityReqRankList = HL.Method(HL.Table) << function(self, args)
    local activityId, rankRelatedId, rankInfoDic = unpack(args)
    if activityId ~= self.m_activityId then
        return
    end

    if rankRelatedId ~= self.m_rankRelatedId then
        return
    end

    self.m_rawRankInfoDic = rankInfoDic
    self.m_rankInfoReady = true

    self:_TryRefresh()
end

ActivityRankingCtrl.OnFriendInfoSync = HL.Method() << function(self)
    self.m_roleSimpleInfoReady = true

    self:_TryRefresh()
end

ActivityRankingCtrl._InitUI = HL.Method() << function(self)
    self.view.btnClose.onClick:AddListener(function()
        self:_OnClickBtnClose()
    end)

    self.view.activityRankingScrollList.onUpdateCell:AddListener(function(go, csIndex)
        self:_OnUpdateCellRankingCell(go, csIndex)
    end)

    self.view.activityRankingScrollList.onScrollEnd:AddListener(function()
        self:_OnScrollEndRankingScrollList()
    end)

    self.m_genRankListCellFunc = UIUtils.genCachedCellFunction(self.view.activityRankingScrollList)

    if not DeviceInfo.usingController then
        return
    end
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

ActivityRankingCtrl._InitData = HL.Method(HL.Table) << function(self, args)
    self.m_activityId = args.activityId
    self.m_rankRelatedId = args.rankRelatedId
end

ActivityRankingCtrl._InitView = HL.Method() << function(self)

    self.view.main:SetState(PanelState.Loading)

    local activityRankInfoCfg = Tables.activityRankInfoTable[self.m_rankRelatedId]
    self.view.main:SetState(activityRankInfoCfg.rankValueStyleType:ToString())


    GameInstance.player.activitySystem:SendReqActivityFriendRank(self.m_activityId, self.m_rankRelatedId)
    GameInstance.player.friendSystem:SyncFriendSimpleInfo()
end

ActivityRankingCtrl._RefreshData = HL.Method() << function(self)
    self.m_rankInfo = {}
    self.m_selfRankInfo = {}

    local succ, activityRankInfoCfg = Tables.activityRankInfoTable:TryGetValue(self.m_rankRelatedId)
    if succ then

        for roleId, rankInfo in pairs(self.m_rawRankInfoDic) do
            local succ, friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
            if succ then
                table.insert(self.m_rankInfo, {
                    roleId = roleId,
                    rankValue = rankInfo.rankValue,
                    style = activityRankInfoCfg.rankValueStyleType,
                })
            end
        end


        for npcId, npcRankInfo in pairs(activityRankInfoCfg.npcRankInfo) do
            table.insert(self.m_rankInfo, {
                npcId = npcId,
                rankValue = npcRankInfo.rankValue,
                style = activityRankInfoCfg.rankValueStyleType,
            })
        end
    else
        logger.warn("没有活动排行榜数据：", self.m_rankRelatedId)
    end

    table.sort(self.m_rankInfo, Utils.genSortFunction({ "rankValue" }, activityRankInfoCfg.isIncremental))


    local selfInfo = GameInstance.player.friendSystem.SelfInfo
    for luaIndex, rankInfo in ipairs(self.m_rankInfo) do
        if rankInfo.roleId == selfInfo.roleId then
            self.m_selfRankInfo.rankLuaIndex = luaIndex
            self.m_selfRankInfo.rankValue = rankInfo.rankValue
        end
    end

end

ActivityRankingCtrl._RefreshView = HL.Method() << function(self)
    self.view.main:SetState(PanelState.Normal)
    self.view.activityRankingScrollList:UpdateCount(#self.m_rankInfo)
    self:_RefreshSelfInfo()
end

ActivityRankingCtrl._TryRefresh = HL.Method() << function(self)
    if not self.m_rankInfoReady then
        return
    end

    if not self.m_roleSimpleInfoReady then
        return
    end

    self:_RefreshData()
    self:_RefreshView()
    self:_EventLogRankingView()

    if not DeviceInfo.usingController then
        return
    end

    local cell = self.m_genRankListCellFunc(1)
    self:SetNaviTarget(cell.inputBindingGroupNaviDecorator)
end

ActivityRankingCtrl._OnClickBtnClose = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end

ActivityRankingCtrl._OnUpdateCellRankingCell = HL.Method(GameObject, HL.Number) << function(self, go, csIndex)
    local cell = self.m_genRankListCellFunc(go)
    local luaIndex = LuaIndex(csIndex)
    local rankInfo = self.m_rankInfo[luaIndex]

    local identityTypeState = IdentityTypeState.Other
    local rankValueState = self:_GetRankingStateByRanking(luaIndex)
    local topicId = ""
    local isPlayer = not string.isEmpty(rankInfo.roleId)
    if isPlayer then


        local _, friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(rankInfo.roleId)
        local isSelf = friendInfo.roleType == CS.Beyond.Gameplay.RoleType.Self
        if friendInfo.playerOnlineState == CS.Beyond.Gameplay.PlayerOnlineState.Online then
            cell.onlineTimeTxt.text = Language.LUA_FRIEND_ONLINE
        elseif friendInfo.lastDateTime ~= 0 then
            local passTime = DateTimeUtils.GetCurrentTimestampBySeconds() - friendInfo.lastDateTime
            cell.onlineTimeTxt.text = string.format(Language.LUA_FRIEND_LAST_ONLINE_TIME, UIUtils.getLeftTime(passTime))
        else
            cell.onlineTimeTxt.text = ""
        end

        topicId = friendInfo.businessCardTopicId
        identityTypeState = isSelf and IdentityTypeState.Self or IdentityTypeState.Other
        cell.onlineState:SetState(friendInfo.playerOnlineState:ToString())
        cell.commonPlayerHead:InitCommonPlayerHeadByRoleId(rankInfo.roleId, function()
            FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(rankInfo.roleId).action()
        end)
    else

        local npcRankInfo = Tables.activityRankInfoTable[self.m_rankRelatedId].npcRankInfo
        local npcRankInfoCfg = npcRankInfo[rankInfo.npcId]
        local avatarPath = string.format("%s/%s", UIConst.UI_SPRITE_CHAR_REMOTE_ICON, rankInfo.npcId)
        topicId = npcRankInfoCfg.topicId
        identityTypeState = IdentityTypeState.NPC
        cell.commonPlayerHead:UpdateHideLevelTxt(true)
        cell.commonPlayerHead:UpdateHidePlatformNode(true)
        cell.commonPlayerHead:InitCommonPlayerHead(avatarPath, "", false, 0, npcRankInfoCfg.name)
    end

    cell.timeTxt.text = UIUtils.getLeftTimeToSecond(floorRankValueConverter(rankInfo.rankValue or 0))
    cell.rankingTxt.text = luaIndex
    cell.stateController:SetState(identityTypeState)
    cell.stateController:SetState(rankValueState)
    cell.stateController:SetState(rankInfo.style:ToString())

    local success, topicCfg = Tables.businessCardTopicTable:TryGetValue(topicId)
    if success then
        cell.themeImg:LoadSprite(UIConst.UI_BUSINESS_CARD_ICON_PATH, topicCfg.id)

    else
        logger.error("未找到名片主题配置:", topicId)
    end
end

ActivityRankingCtrl._GetRankingStateByRanking = HL.Method(HL.Number).Return(HL.String) << function(self, ranking)
    local rankingState = RankingState.RankingNormal
    if ranking == 1 then
        rankingState = RankingState.Ranking1st
    elseif ranking == 2 then
        rankingState = RankingState.Ranking2nd
    elseif ranking == 3 then
        rankingState = RankingState.Ranking3rd
    elseif ranking < 10 then
        rankingState = RankingState.RankingHigh
    end
    return rankingState
end

ActivityRankingCtrl._RefreshSelfInfo = HL.Method() << function(self)
    local selfInfo = GameInstance.player.friendSystem.SelfInfo
    local selfNode = self.view.activityRankingSelfCell
    selfNode.commonPlayerHead:InitCommonPlayerHeadByRoleId(selfInfo.roleId, false)

    selfNode.onlineState:SetState(CS.Beyond.Gameplay.PlayerOnlineState.Online:ToString())
    selfNode.onlineTimeTxt.text = Language.LUA_FRIEND_ONLINE

    local selfRankLuaIndex = self.m_selfRankInfo.rankLuaIndex
    selfNode.rankingTxt.text = selfRankLuaIndex or "-"
    selfNode.timeTxt.text = self.m_selfRankInfo.rankValue and
            UIUtils.getLeftTimeToSecond(floorRankValueConverter(self.m_selfRankInfo.rankValue)) or "--:--"

    selfNode.stateController:SetState(IdentityTypeState.Self)
    local rankValueState = selfRankLuaIndex and self:_GetRankingStateByRanking(selfRankLuaIndex) or RankingState.RankingNormal
    selfNode.stateController:SetState(rankValueState)

    local activityRankInfoCfg = Tables.activityRankInfoTable[self.m_rankRelatedId]
    selfNode.stateController:SetState(activityRankInfoCfg.rankValueStyleType:ToString())

    local success, topicCfg = Tables.businessCardTopicTable:TryGetValue(selfInfo.businessCardTopicId)
    if success then
        selfNode.themeImg:LoadSprite(UIConst.UI_BUSINESS_CARD_ICON_PATH, topicCfg.id)

    else
        logger.error("未找到名片主题配置:", selfInfo.businessCardTopicId)
    end

    selfNode.jumpBtn.onClick:RemoveAllListeners()
    selfNode.jumpBtn.onClick:AddListener(function()
        self:_OnClickSelfJumpBtn()
    end)
end

ActivityRankingCtrl._OnClickSelfJumpBtn = HL.Method() << function(self)
    if not self.m_selfRankInfo.rankLuaIndex then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_RANKING_JUMP_TO_SELF_FAIL)
        return
    end

    local luaIndex = self.m_selfRankInfo.rankLuaIndex
    local fastMode = DeviceInfo.usingController
    self.m_scrollToSelfTag = not fastMode
    self.view.activityRankingScrollList:ScrollToIndex(CSIndex(luaIndex), fastMode)

    if fastMode then
        local cell = self.m_genRankListCellFunc(luaIndex)
        self:SetNaviTarget(cell.inputBindingGroupNaviDecorator)
        self:_PlayLocateSelfAnim()
    end
end

ActivityRankingCtrl._OnScrollEndRankingScrollList = HL.Method() << function(self)
    if not self.m_scrollToSelfTag then
        return
    end
    self.m_scrollToSelfTag = false

    self:_PlayLocateSelfAnim()
end

ActivityRankingCtrl._PlayLocateSelfAnim = HL.Method() << function(self)
    local luaIndex = self.m_selfRankInfo.rankLuaIndex
    local rankCell = self.m_genRankListCellFunc(luaIndex)
    if not rankCell then
        logger.error("ActivityRankingCtrl._PlayLocateSelfAnim cant get rankCell, rankIndex:", luaIndex)
        return
    end
    rankCell.animationWrapper:Play(LocateSelfAnim)
end

ActivityRankingCtrl._EventLogRankingView = HL.Method() << function(self)
    local eventRankInfo = {}

    for _, rankInfoUnit in ipairs(self.m_rankInfo) do
        if not string.isEmpty(rankInfoUnit.roleId) then
            table.insert(eventRankInfo, {
                roleId = rankInfoUnit.roleId,
                rankValue = rankInfoUnit.rankValue,
            })
        end
    end

    ActivityUtils.GameEventLogActivityRankView(self.m_activityId, self.m_rankRelatedId, eventRankInfo)
end

HL.Commit(ActivityRankingCtrl)
