local GachaPoolCellBase = require_ex('UI/Widgets/GachaPoolCellBase')

GachaPoolCellStarter = HL.Class('GachaPoolCellStarter', GachaPoolCellBase)



local activitySystem = GameInstance.player.activitySystem


local csGachaSystem = GameInstance.player.gacha



GachaPoolCellStarter.m_missionCellListCache = HL.Field(HL.Forward("UIListCache"))

GachaPoolCellStarter.m_activityInfo = HL.Field(HL.Table)

GachaPoolCellStarter.m_isMissionListExpand = HL.Field(HL.Boolean) << false

GachaPoolCellStarter.m_waitCumulateRewardMsg = HL.Field(HL.Boolean) << false



GachaPoolCellStarter._OnFirstTimeInit = HL.Override() << function(self)
    GachaPoolCellStarter.Super._OnFirstTimeInit(self)
    self:_InitUI()
    self:RegisterMessage(MessageConst.ON_ACTIVITY_GACHA_BEGINNER_STAGE_MODIFY, function()
        self:_UpdateData()
        self:_RefreshActivityNode()
    end)
    self:RegisterMessage(MessageConst.ON_GACHA_POOL_ROLE_DATA_CHANGED, function()
        if self.m_waitCumulateRewardMsg then
            GachaPoolCellStarter.Super._UpdateBaseData(self)
            self:_RefreshAllUI()
            self.m_waitCumulateRewardMsg = false
        end
    end)
end

GachaPoolCellStarter._InnerInitGachaPoolCell = HL.Override() << function(self)
    logger.info("初始化 GachaPoolCellStarter")
    self:_InitData()

    self.view.gachaTenBtn.button.onClick:RemoveAllListeners()
    self.view.gachaTenBtn.button.onClick:AddListener(function()

        local gachaCostInfos = self.m_baseInfo.gachaCostInfos
        local costInfos = gachaCostInfos.tenPullCostInfos
        local isEnough = costInfos.isEnough
        local costItems = costInfos.costItems
        local costItemCount = #costItems
        if not isEnough then
            if self.m_activityInfo.canGetRewardCount > 0 then
                Notify(MessageConst.SHOW_POP_UP, {
                    content = string.format(Language.LUA_ACTIVITY_GACHA_BEGINNER_CAN_GET_STAGE_REWARD_AND_JUMP, self.m_activityInfo.canGetRewardCount),
                    onConfirm = function()
                        self:_TryJumpToActivity()
                    end
                })
            else
                if costItemCount > 0 then
                    Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_GACHA_STARTER_GACHA_COST_NOT_ENOUGH, Tables.itemTable[costItems[1].id].name))
                end
            end
            return
        end

        self:_Gacha(true)
    end)
end

GachaPoolCellStarter._InnerUpdateGachaPoolCell = HL.Override() << function(self)
    logger.info("更新 GachaPoolCellStarter")
    self:_UpdateData()
    self:_RefreshAllUI()
end

GachaPoolCellStarter.UpdateMoneyNodeOnlyGachaTicket = HL.Override(HL.Any) << function(self, moneyNode)
    moneyNode.gachaItem1.view.gameObject:SetActiveIfNecessary(false)
    moneyNode.gachaItem2.view.gameObject:SetActiveIfNecessary(false)
    moneyNode.gachaItem3.view.gameObject:SetActiveIfNecessary(true)
    local singlePullItemId = self.m_baseInfo.gachaCostInfos.tenPullItemInfos[1].itemId
    moneyNode.gachaItem3:InitMoneyCell(singlePullItemId)
end

GachaPoolCellStarter._OnEnable = HL.Override() << function(self)
    GachaPoolCellStarter.Super._OnEnable(self)
    self:_RefreshRedDot()
end



GachaPoolCellStarter._InitData = HL.Method() << function(self)

    self.m_activityInfo = {
        stageInfos = {},
        canGetRewardCount = 0,
    }

    local activityCfg = Tables.activityLevelRewardsTable[Tables.charGachaConst.gachaBeginnerActivityId]
    for _, stageCfg in pairs(activityCfg.stageList) do
        local rewardItems = UIUtils.getRewardItems(stageCfg.rewardId)
        local rewardItem = {
            itemId = rewardItems[1].id,
            count = rewardItems[1].count,
        }

        local info = {
            stageId = stageCfg.stageId,
            rewardItemInfo = rewardItem,
        }
        table.insert(self.m_activityInfo.stageInfos, info)
    end
end

GachaPoolCellStarter._UpdateData = HL.Method() << function(self)
    local activityData = activitySystem:GetActivity(Tables.charGachaConst.gachaBeginnerActivityId)

    local canGetRewardCount = 0
    for _, stageInfo in pairs(self.m_activityInfo.stageInfos) do
        local stageId = stageInfo.stageId
        local isRewarded = true
        local isComplete = true
        if activityData ~= nil then
            isRewarded = activityData.receiveStageList:Contains(stageId)
            isComplete = activityData.completeStageList:Contains(stageId)
        end

        if isComplete and not isRewarded then
            canGetRewardCount = canGetRewardCount + stageInfo.rewardItemInfo.count
        end
    end
    self.m_activityInfo.canGetRewardCount = canGetRewardCount
end



GachaPoolCellStarter._InitUI = HL.Method() << function(self)

    self.view.activityNode.button.onClick:AddListener(function()
        self:_TryJumpToActivity()
    end)

    local cumulateRewardNode = self.view.cumulateRewardNode
    cumulateRewardNode.button.onClick:AddListener(function()
        if self.m_baseInfo.remainPullCount > 0 then
            UIManager:Open(PanelId.BattlePassWeaponCase, { itemId = self.m_baseInfo.cumulateRewardItemInfo[1].id, isPreview = true, false, subTitle = Language.LUA_GACHA_STARTER_WEAPON_CASE_SUBTITLE })
            AudioAdapter.PostEvent("Au_UI_Button_Common")
        elseif not self.m_baseInfo.cumulateRewardItemInfo[1].isGot then
            self.m_waitCumulateRewardMsg = true
            csGachaSystem:SendGetCumulativeRewardReq(self.m_poolId, { 0 })
            AudioAdapter.PostEvent("Au_UI_Button_Receive")
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_GACHA_STARTER_WEAPON_CASE_HAS_GOT)
            AudioAdapter.PostEvent("Au_UI_Button_Common")
        end
    end)
end

GachaPoolCellStarter._RefreshAllUI = HL.Method() << function(self)

    if self.m_baseInfo.remainPullCount <= 0 or self.m_baseInfo.remainSoftGuaranteeCount <= 0 then
        self.view.titleGuaranteeTxt.gameObject:SetActive(false)
    else
        self.view.titleGuaranteeTxt.gameObject:SetActive(true)
        self.view.titleGuaranteeTxt:SetAndResolveTextStyle(string.format(Language.LUA_GACHA_STARTER_GUARANTEE, self.m_baseInfo.remainSoftGuaranteeProgress))
    end
    self.view.remainPullCountTxt.text = self.m_baseInfo.remainPullCount
    self.view.cumulateRewardNode.cumulateRemainNumTxt:SetAndResolveTextStyle(string.format(Language.LUA_GACHA_CUMULATE_REWARD_STARTER_GUARANTEE, self.m_baseInfo.remainPullCount))

    local cumulateRewardInfo = self.m_baseInfo.cumulateRewardItemInfo[1]
    if self.m_baseInfo.remainPullCount > 0 then
        self.view.cumulateRewardNode.stateController:SetState("Normal")
        self.view.explainNumberStateCtrl:SetState("NotZero")
    elseif cumulateRewardInfo.isGot then
        self.view.cumulateRewardNode.stateController:SetState("Rewarded")
        self.view.explainNumberStateCtrl:SetState("Zero")
    else
        self.view.cumulateRewardNode.stateController:SetState("Complete")
        self.view.explainNumberStateCtrl:SetState("Zero")
    end
    self.view.cumulateRewardNode.itemIcon:InitItemIcon(cumulateRewardInfo.id, true)
    self.view.cumulateRewardNode.numTxt.text = cumulateRewardInfo.count

    self:_RefreshActivityNode()
    self:_RefreshRedDot()
end

GachaPoolCellStarter._RefreshActivityNode = HL.Method() << function(self)
    local canGetRewardCount = self.m_activityInfo.canGetRewardCount
    if canGetRewardCount > 0 then
        self.view.activityNode.stateController:SetState("CanGetReward")
        self.view.activityNode.titleTxt.text = string.format(Language.LUA_ACTIVITY_GACHA_BEGINNER_CAN_GET_STAGE_REWARD, canGetRewardCount)
    else
        self.view.activityNode.stateController:SetState("Normal")
    end
end

GachaPoolCellStarter._RefreshRedDot = HL.Method() << function(self)

    local gachaTenTicketId = self.m_baseInfo.gachaCostInfos.tenPullItemInfos[1].itemId
    self.view.gachaTenBtn.redDot.gameObject:SetActive(Utils.getItemCount(gachaTenTicketId) > 0)
end



GachaPoolCellStarter._TryJumpToActivity = HL.Method() << function(self)
    local isUnlock = Utils.isSystemUnlocked(GEnums.UnlockSystemType.Activity)
    if not isUnlock then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_LOCK)
        return
    end
    local canJump, msg = PhaseManager:CheckCanOpenPhase(PhaseId.ActivityCenter, { activityId = Tables.charGachaConst.gachaBeginnerActivityId, gotoCenter = true})
    if not canJump then
        Notify(MessageConst.SHOW_TOAST, msg)
        return
    end
    PhaseManager:GoToPhase(PhaseId.ActivityCenter, { activityId = Tables.charGachaConst.gachaBeginnerActivityId, gotoCenter = true})
end

GachaPoolCellStarter.GetSubPanelArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local isOpen, ctrl = UIManager:IsOpen(PanelId.BattlePassWeaponCase)
    if isOpen then
        local subPanelArg = ctrl:GetCurPhaseStateArg()
        if subPanelArg then
            subPanelArg.isBattlePassWeaponCase = true
            return subPanelArg
        end
    end
    return GachaPoolCellStarter.Super.GetSubPanelArg(self)
end

GachaPoolCellStarter.HandleSubPanelArg = HL.Override(HL.Any) << function(self, subPanelArg)
    if subPanelArg.isBattlePassWeaponCase then
        UIManager:Open(PanelId.BattlePassWeaponCase, subPanelArg)
    else
        GachaPoolCellStarter.Super.HandleSubPanelArg(self, subPanelArg)
    end
end


HL.Commit(GachaPoolCellStarter)
return GachaPoolCellStarter