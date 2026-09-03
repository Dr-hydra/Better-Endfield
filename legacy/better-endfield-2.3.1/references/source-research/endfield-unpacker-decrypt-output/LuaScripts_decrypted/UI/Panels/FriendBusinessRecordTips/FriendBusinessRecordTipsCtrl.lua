local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FriendBusinessRecordTips

FriendBusinessRecordTipsCtrl = HL.Class('FriendBusinessRecordTipsCtrl', uiCtrl.UICtrl)

FriendBusinessRecordTipsCtrl.s_messages = HL.StaticField(HL.Table) << {}

FriendBusinessRecordTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local roleId = arg.roleId
    self.view.cantPenetrateBtn.onClick:AddListener(function() self:Close() end)

    local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
    if not success then
        return
    end

    local showCC = self:_UpdateContingencyContract(playerInfo)
    local showST = self:_UpdateSeasonTower(playerInfo)
    self.view.boundary.gameObject:SetActiveIfNecessary(showCC and showST)

    if arg.transform then
        local posType = arg.posType or UIConst.UI_TIPS_POS_TYPE.MidBottom
        UIUtils.updateTipsPosition(self.view.content.transform, arg.transform, self.view.rectTransform, self.uiCamera, posType)
    end
end

FriendBusinessRecordTipsCtrl._UpdateContingencyContract = HL.Method(HL.Any).Return(HL.Boolean) << function(self, playerInfo)
    local ccRecord = playerInfo.contingencyContractBestRecord
    if ccRecord.Item1 ~= nil and ccRecord.Item2 > 1 then
        local activityData = GameInstance.player.activitySystem:GetActivity(ccRecord.Item1)
        if activityData ~= nil and activityData.gameplayEndTime - DateTimeUtils.GetCurrentTimestampBySeconds() > 0 then
            local cfg = Tables.activityTable:GetValue(ccRecord.Item1)
            self.view.levelTagNode.gameObject:SetActiveIfNecessary(true)
            self.view.contingencyContractTxt.text = cfg.name
            self.view.levelTxt.text = tostring(ccRecord.Item2)
            self.view.contingencyContractTagNumTxt.text = string.format(Language.LUA_FRIEND_BUSINESS_CARD_CC_SCORE, ccRecord.Item2)

            local rangeArray = Tables.activityContingencyContractTable:GetValue(ccRecord.Item1).rangeArray
            self.view.levelTagNode:SetState(tostring(1))
            for i = 1, #rangeArray do
                local range = rangeArray[CSIndex(i)]
                if ccRecord.Item2 >= range then
                    self.view.levelTagNode:SetState(tostring(i + 1))
                end
            end
            return true
        end
    end
    self.view.contingencyContractNode.gameObject:SetActiveIfNecessary(false)
    return false
end

FriendBusinessRecordTipsCtrl._UpdateSeasonTower = HL.Method(HL.Any).Return(HL.Boolean) << function(self, playerInfo)
    local displayRecord = FriendUtils.getSeasonTowerDisplayRecord(playerInfo)
    local rank = displayRecord and displayRecord.rank or 0
    if displayRecord and FriendUtils.SEASON_TOWER_RANK_NAMES[rank] then
        self.view.seasonTowerTag.gameObject:SetActiveIfNecessary(true)
        self.view.seasonTxt.text = Tables.seasonTowerTable:GetValue(displayRecord.seasonId).name
        self.view.seasonTowerTag:SetState(FriendUtils.SEASON_TOWER_RANK_NAMES[rank])
        local res, rankData = Tables.seasonTowerRankTable:TryGetValue(rank)
        local rankName = res and rankData.rankName or FriendUtils.SEASON_TOWER_RANK_NAMES[rank]
        self.view.seasonTagTxt.text = string.format(Language.LUA_FRIEND_BUSINESS_CARD_SEASON_TOWER_RANK, rankName)
        return true
    end
    self.view.seasonTowerNode.gameObject:SetActiveIfNecessary(false)
    return false
end

HL.Commit(FriendBusinessRecordTipsCtrl)
