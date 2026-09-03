
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityGachaBeginner

ActivityGachaBeginnerCtrl = HL.Class('ActivityGachaBeginnerCtrl', uiCtrl.UICtrl)


local activitySystem = GameInstance.player.activitySystem


local csGachaSystem = GameInstance.player.gacha





ActivityGachaBeginnerCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_GACHA_BEGINNER_STAGE_MODIFY] = '_OnActivityOrGachaPoolChange',
    [MessageConst.ON_GACHA_POOL_ROLE_DATA_CHANGED] = '_OnActivityOrGachaPoolChange',
}


ActivityGachaBeginnerCtrl.m_info = HL.Field(HL.Table)

ActivityGachaBeginnerCtrl.m_weaponItemCellListCache = HL.Field(HL.Forward("UIListCache"))

ActivityGachaBeginnerCtrl.m_missionCellListCache = HL.Field(HL.Forward("UIListCache"))

ActivityGachaBeginnerCtrl.m_confirmFocusBindId = HL.Field(HL.Number) << -1

ActivityGachaBeginnerCtrl.m_cancelFocusBindId = HL.Field(HL.Number) << -1

ActivityGachaBeginnerCtrl.m_rewardList = HL.Field(HL.Table)




ActivityGachaBeginnerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.activityCommonInfo:InitActivityCommonInfo(arg)
    self:_InitUI()
    self:_InitData(arg)
    self:_UpdateData()
    self:_RefreshAllUI()
end



ActivityGachaBeginnerCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    local poolId = Tables.charGachaConst.beginnerGachaActivityPoolId
    local poolCfg = Tables.gachaCharPoolTable[poolId]
    local poolTypeCfg = Tables.gachaCharPoolTypeTable[poolCfg.type]
    local cumulateRewardIds = poolCfg.cumulativeRewardIds
    local rewardItems = UIUtils.getRewardItems(cumulateRewardIds[0])
    self.m_info = {

        cumulateRewardItemInfo = {
            id = rewardItems[1].id,
            count = rewardItems[1].count,
        },
        rewardItemInfos = {},
        maxPullCount = poolTypeCfg.maxPullCount,
        remainPullCount = 0,
        gachaTenTicketId = poolTypeCfg.tenPullCostItemIds[0],

        curMissionInfo = {},
        stageInfos = {},
        totalStageTicketRewardCount = 0,
        activityData = nil,
    }

    local _, itemChestData = Tables.usableItemChestTable:TryGetValue(self.m_info.cumulateRewardItemInfo.id)
    for _, rewardId in pairs(itemChestData.rewardIdList) do
        local weaponItemBundle = UIUtils.getRewardFirstItem(rewardId)
        table.insert(self.m_info.rewardItemInfos, {
            id = weaponItemBundle.id,
            count = weaponItemBundle.count,
            forceHidePotentialStar = true
        })
    end

    self.view.activityCommonInfo.view.gotoNode.btnDetailRedDot:InitRedDot("ActivityGachaBeginnerJumpPoolBtn", Tables.charGachaConst.gachaBeginnerActivityId)

    local activityCfg = Tables.activityLevelRewardsTable[Tables.charGachaConst.gachaBeginnerActivityId]
    for _, stageCfg in pairs(activityCfg.stageList) do
        local conditionCfg = stageCfg.conditions[0]
        local desc = conditionCfg.desc
        local missionId = conditionCfg.parameters[0].valueStringList[0]
        local stageRewardItems = UIUtils.getRewardItems(stageCfg.rewardId)
        local rewardItem = {
            id = stageRewardItems[1].id,
            count = stageRewardItems[1].count,
        }

        local info = {
            stageId = stageCfg.stageId,
            stageStrId = stageCfg.stageStrId,
            desc = desc,
            missionId = missionId,
            rewardItemInfo = rewardItem,

            state = GEnums.ActivityConditionalStageState.Unlocked,
        }
        table.insert(self.m_info.stageInfos, info)
        if rewardItem.id == self.m_info.gachaTenTicketId then
            self.m_info.totalStageTicketRewardCount = self.m_info.totalStageTicketRewardCount + rewardItem.count
        end
    end
end

ActivityGachaBeginnerCtrl._UpdateData = HL.Method() << function(self)
    self.m_rewardList = {}
    local activityData = activitySystem:GetActivity(Tables.charGachaConst.gachaBeginnerActivityId)
    self.m_info.activityData = activityData


    for _, stageInfo in pairs(self.m_info.stageInfos) do
        local stageId = stageInfo.stageId
        local isRewarded = activityData.receiveStageList:Contains(stageId)
        local isComplete = activityData.completeStageList:Contains(stageId)

        if isRewarded then
            stageInfo.state = GEnums.ActivityConditionalStageState.Rewarded
        elseif isComplete then
            stageInfo.state = GEnums.ActivityConditionalStageState.Completed
            table.insert(self.m_rewardList, stageId)
        else
            stageInfo.state = GEnums.ActivityConditionalStageState.Unlocked
        end
    end

    self.m_info.curMissionInfo.curMissionId, self.m_info.curMissionInfo.curMissionDesc = Utils.getCurMissionIdAndDesc("gacha")


    local _, poolInfo = csGachaSystem.poolInfos:TryGetValue(Tables.charGachaConst.beginnerGachaActivityPoolId)
    self.m_info.remainPullCount = self.m_info.maxPullCount - poolInfo.totalPullCountNoShare
end



ActivityGachaBeginnerCtrl._InitUI = HL.Method() << function(self)
    self.m_weaponItemCellListCache = UIUtils.genCellCache(self.view.weaponItemCell)
    self.m_missionCellListCache = UIUtils.genCellCache(self.view.missionNode.missionCell)
    self.view.missionNode.titleJumpBtn.onClick:AddListener(function()
        local info = self.m_info.curMissionInfo
        if not string.isEmpty(info.curMissionId) then
            PhaseManager:OpenPhase(PhaseId.Mission, {
                autoSelect = info.curMissionId
            })
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_GACHA_STARTER_ALL_MISSION_COMPLETE_TOAST)
        end
    end)
    self.view.missionNode.missionListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        InputManagerInst:ToggleBinding(self.m_confirmFocusBindId, not isFocused)
        InputManagerInst:ToggleBinding(self.m_cancelFocusBindId, isFocused)
        if not isFocused then
            return
        end
        local firstCell = self.m_missionCellListCache:Get(1)
        if firstCell then
            self:SetNaviTarget(firstCell.naviDeco)
        end
    end)

    self.m_confirmFocusBindId = self:BindInputPlayerAction("activity_gacha_beginner_confirm_focus", function()
        self.view.missionNode.missionListNaviGroup:ManuallyFocus()
    end)

    self.m_cancelFocusBindId = self:BindInputPlayerAction("activity_gacha_beginner_cancel_focus", function()
        self.view.missionNode.missionListNaviGroup:ManuallyStopFocus()
    end)
    InputManagerInst:ToggleBinding(self.m_cancelFocusBindId, false)
end

ActivityGachaBeginnerCtrl._RefreshAllUI = HL.Method() << function(self)
    local itemId = self.m_info.cumulateRewardItemInfo.id
    local itemCfg = Tables.itemTable[itemId]
    self.view.weaponBoxItemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemCfg.iconId)

    self.m_weaponItemCellListCache:Refresh(#self.m_info.rewardItemInfos, function(cell, luaIndex)
        cell:InitItem(self.m_info.rewardItemInfos[luaIndex], function()
            UIUtils.showItemSideTips(cell)
        end)

        cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        cell.view.simpleStateController:SetState("NoRarity")
    end)

    self:_RefreshCumulateRewardUI()
    self:_RefreshStageMissionUI()
end

ActivityGachaBeginnerCtrl._RefreshCumulateRewardUI = HL.Method() << function(self)
    if self.m_info.remainPullCount <= 0 then
        self.view.weaponBoxRewardTips:SetAndResolveTextStyle(Language.LUA_ACTIVITY_GACHA_BEGINNER_CAN_GET_WEAPON_REWARD)
    else
        self.view.weaponBoxRewardTips:SetAndResolveTextStyle(string.format(Language.LUA_ACTIVITY_GACHA_BEGINNER_WEAPON_REWARD_REMAIN_PULL_COUNT, self.m_info.remainPullCount))
    end
end

ActivityGachaBeginnerCtrl._RefreshStageMissionUI = HL.Method() << function(self)
    local missionNode = self.view.missionNode

    local missionSubTitle = string.isEmpty(self.m_info.curMissionInfo.curMissionDesc) and Language.LUA_GACHA_STARTER_ALL_MISSION_COMPLETE_TOAST or self.m_info.curMissionInfo.curMissionDesc
    missionNode.missionSubTitleTxt.text = missionSubTitle

    missionNode.titleRewardNumTxt.text = string.format(Language.LUA_COMMON_X_COUNT, self.m_info.totalStageTicketRewardCount)

    self.m_missionCellListCache:Refresh(#self.m_info.stageInfos, function(cell, luaIndex)
        local info = self.m_info.stageInfos[luaIndex]
        cell.gameObject.name = "MissionCell_" .. luaIndex
        cell.descTxt.text = info.desc

        cell.rewardItem:InitItem(info.rewardItemInfo, true)
        cell.rewardItem:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        if DeviceInfo.usingController then
            cell.rewardItem:SetEnableHoverTips(false)
        else
            cell.rewardItem:SetEnableHoverTips(true)
        end

        if info.state == GEnums.ActivityConditionalStageState.Rewarded then
            cell.stateController:SetState("Rewarded")
        elseif info.state == GEnums.ActivityConditionalStageState.Completed then
            cell.stateController:SetState("Complete")
        else
            cell.stateController:SetState("Normal")
        end

        cell.getRewardBtn.onClick:RemoveAllListeners()
        cell.getRewardBtn.onClick:AddListener(function()
            self.m_info.activityData:GainReward(self.m_rewardList)
        end)

        cell.toastBtn.onClick:RemoveAllListeners()
        cell.toastBtn.onClick:AddListener(function()
            if info.state == GEnums.ActivityConditionalStageState.Rewarded then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_GACHA_BEGINNER_HAS_GOT_REWARD)
            elseif info.state ~= GEnums.ActivityConditionalStageState.Completed then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_GACHA_BEGINNER_CAN_NOT_GET_REWARD)
            end
        end)
    end)
end



ActivityGachaBeginnerCtrl._OnActivityOrGachaPoolChange = HL.Method() << function(self)
    self:_UpdateData()
    self:_RefreshCumulateRewardUI()
    self:_RefreshStageMissionUI()
end


HL.Commit(ActivityGachaBeginnerCtrl)
