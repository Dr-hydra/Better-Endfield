local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementMain
local PHASE_ID = PhaseId.SettlementMain

local settlementSystem = GameInstance.player.settlementSystem

local missionSystem = GameInstance.player.mission

SettlementMainCtrl = HL.Class('SettlementMainCtrl', uiCtrl.UICtrl)






SettlementMainCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_SETTLEMENT_MODIFY] = '_OnSettlementModify',

    [MessageConst.ON_SETTLEMENT_OFFICER_CHANGE] = '_OnUpdateOfficer',

    [MessageConst.ON_SETTLEMENT_REMAIN_MONEY_MODIFY] = '_OnUpdateTickMoney',

    [MessageConst.ON_SETTLEMENT_TRADE_SUCCESS] = '_OnTradeSuccess',

    [MessageConst.ON_FACTORY_DEPOT_CHANGED] = '_TryUpdateItemDepot',

    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = '_OnActivityStageUpdate',
}



local StlStateFlag = {
    None = 0,
    IsSelect = 1 << 0,
    CanUpgrade = 1 << 1,
    HasOfficer = 1 << 2,
    IsMoneyEmpty = 1 << 3,
    IsMoneyFilled = 1 << 4,
}


local StlCellStateNameMap = {
    basic = {
        checkFlag = StlStateFlag.IsSelect,
        [StlStateFlag.None] = "UnselectState",
        [StlStateFlag.IsSelect] = "SelectState",
    },
    expProgress = {
        checkFlag = StlStateFlag.CanUpgrade,
        [StlStateFlag.None] = "NormalState",
        [StlStateFlag.CanUpgrade] = "UpgradeState",
    },
    officer = {
        checkFlag = StlStateFlag.HasOfficer,
        [StlStateFlag.None] = "EmptyState",
        [StlStateFlag.HasOfficer] = "StationedState",
    },
    moneyProgress = {
        checkFlag = StlStateFlag.IsMoneyEmpty | StlStateFlag.IsMoneyFilled,
        [StlStateFlag.None] = "ProgressState",
        [StlStateFlag.IsMoneyEmpty] = "EmptyState",
        [StlStateFlag.IsMoneyFilled] = "FilledState",
    },
}

local TradeIconAniStage = {
    None = 0,
    In = 1,
    Loop = 2,
    Done = 3,
    Out = 4,
}

SettlementMainCtrl.m_genStlCellFunc = HL.Field(HL.Function)

SettlementMainCtrl.m_getTagCells = HL.Field(HL.Forward("UIListCache"))

SettlementMainCtrl.m_domainInfo = HL.Field(HL.Table)

SettlementMainCtrl.m_stlInfoList = HL.Field(HL.Table)

SettlementMainCtrl.m_curSelectStlIndex = HL.Field(HL.Number) << 0

SettlementMainCtrl.m_onSelectorNumberChanged = HL.Field(HL.Function)



SettlementMainCtrl.m_moneyStoreCellCache = HL.Field(HL.Forward("UIListCache"))

SettlementMainCtrl.m_itemStoreCellCache = HL.Field(HL.Forward("UIListCache"))

SettlementMainCtrl.m_waitTradeComplete = HL.Field(HL.Boolean) << false

SettlementMainCtrl.m_hasActivityUpdateMsgWaitTradeComplete = HL.Field(HL.Boolean) << false


SettlementMainCtrl.m_moneyStoreCellAniInterval = HL.Field(HL.Thread)




SettlementMainCtrl.m_activityInfo = HL.Field(HL.Table)

SettlementMainCtrl.m_remindExcessTrade = HL.Field(HL.Boolean) << false






SettlementMainCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    local initSuccess = self:_InitData(arg)
    if not initSuccess then
        return
    end
    self:_UpdateData(true)
    self:_RefreshAllUI()
    if arg and type(arg) == "table" and arg.resumeOpenPanel then
        for _, panelInfo in pairs(arg.resumeOpenPanel) do
            UIManager:Open(panelInfo.panelId, panelInfo.arg)
        end
    end
end

SettlementMainCtrl.OnShow = HL.Override() << function(self)
    settlementSystem:AddSettlementSyncRequest(self.view.transform.name)
end

SettlementMainCtrl.OnClose = HL.Override() << function(self)
    settlementSystem:RemoveSettlementSyncRequest(self.view.transform.name)
    self.m_moneyStoreCellAniInterval = self:_ClearCoroutine(self.m_moneyStoreCellAniInterval)
end

SettlementMainCtrl.OnPhaseRefresh = HL.Override(HL.Opt(HL.Any)) << function(self, arg)
    local initSuccess = self:_InitData(arg)
    if not initSuccess then
        return
    end
    self:_UpdateData(true)
    self:_RefreshAllUI()

    local isOpen, walletBarCtrl = UIManager:IsOpen(PanelId.WalletBar)
    if isOpen then
        walletBarCtrl.view.contentNaviGroup:ManuallyStopFocus()
    end
	self.view.tradeNode.centerNaviGroup:ManuallyStopFocus()
end




SettlementMainCtrl._InitData = HL.Method(HL.Any).Return(HL.Boolean) << function(self, arg)
    local domainId, defaultStlId = DomainPOIUtils.resolveOpenSettlementArgs(arg)
    local initSelectItemCount = 0
    if type(arg) == "table" and arg.resumeState then
        domainId = arg.resumeState.domainId
        defaultStlId = arg.resumeState.defaultStlId
        initSelectItemCount = arg.resumeState.selectItemCount
    end
    local hasCfg, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
    if not hasCfg then
        logger.error("domainDataTable missing cfg, id: " .. domainId)
        return false
    end

    self.m_domainInfo = {
        id = domainId,
        name = domainCfg.domainName,
        icon = domainCfg.domainIcon,
        stlIdList = domainCfg.settlementGroup,
        moneyId = domainCfg.domainGoldItemId,
        itemStoreLimitCount = Utils.getDepotItemStackLimitCount(domainId),
        defaultStlId = defaultStlId,
        initSelectItemCount = initSelectItemCount,
    }

    self.m_tradeIconAniInfo = {
        stage = TradeIconAniStage.None,
        onlyActivityAni = false,
        curIsAdd = false,

        lastUpdateAniTime = 0,
        curIsPlayActivityAni = false,
        curPlayNormalAni = 0,
    }

    return true
end

SettlementMainCtrl._UpdateData = HL.Method(HL.Boolean) << function(self, isInit)

    self.m_activityInfo = DomainPOIUtils.getSettlementTradeActivityInfo()


    self.m_stlInfoList = {}
    local defaultIndex = 1
    for index, stlId in pairs(self.m_domainInfo.stlIdList) do
        if stlId == self.m_domainInfo.defaultStlId then
            defaultIndex = LuaIndex(index)
        end


        local stlData = settlementSystem:GetUnlockSettlementData(stlId)
        if stlData then
            local stlCfg = Tables.settlementBasicDataTable[stlId]
            local domainLevelCfg = Tables.levelDescTable[stlCfg.domainLevelId]
            local moneyId = self.m_domainInfo.moneyId
            local moneyIcon = ""
            local moneyItemCfg = Utils.tryGetTableCfg(Tables.itemTable, moneyId)
            if moneyItemCfg then
                moneyIcon = moneyItemCfg.iconId
            end

            local tagInfos = {}
            for _, stlTagId in pairs(stlCfg.wantTagIdGroup) do
                local tagCfg = Tables.settlementTagTable[stlTagId]
                local tagInfo = {
                    stlTagId = stlTagId,
                    name = tagCfg.settlementTagName,

                    enhanceMoneyProduceSpeedRate = tagCfg.enhanceMoneyProduceSpeedRate,
                    enhanceMoneyProfitRate = tagCfg.enhanceMoneyProfitRate,
                    enhanceExpProfitRate = tagCfg.enhanceExpProfitRate,
                }
                table.insert(tagInfos, tagInfo)
            end

            local stlInfo = {

                stlId = stlId,
                stlName = stlCfg.settlementName,
                levelName = domainLevelCfg.showName,
                maxLevel = DomainPOIUtils.GetSettlementMaxLevel(stlId),
                stlColor = UIUtils.getColorByString(stlCfg.settlementColor),
                moneyId = moneyId,
                moneyIcon = moneyIcon,
                tagInfos = tagInfos,

                upgradeMissionInfo = {},
                officerInfo = {},
                sellItemInfo = {},
                tradeInfo = {},
            }
            table.insert(self.m_stlInfoList, stlInfo)
            self:_UpdateStlRuntimeInfo(stlInfo)
        end
    end

    if isInit then
        self.m_curSelectStlIndex = defaultIndex
    end
    if #self.m_stlInfoList <= 0 then
        local msg = string.format("据点stlInfoList为空！domainId: %s; defaultStlId: %s", self.m_domainInfo.id, self.m_domainInfo.defaultStlId or "Non")
        logger.error(msg)
        self.m_curSelectStlIndex = 0
    end
end

SettlementMainCtrl._UpdateStlRuntimeInfo = HL.Method(HL.Table) << function(self, stlInfo)
    local stlId = stlInfo.stlId

    local stlData = settlementSystem:GetUnlockSettlementData(stlId)
    local stlCfg = Tables.settlementBasicDataTable[stlId]
    local stlLevelCfg = stlCfg.settlementLevelMap[stlData.level]

    stlInfo.curLevel = stlData.level
    stlInfo.stlPic = stlLevelCfg.settlementPicId
    stlInfo.stlDesc = stlLevelCfg.desc

    local curMoney = stlData.remainMoney
    local maxMoney = stlLevelCfg.moneyMax
    stlInfo.curMoney = curMoney
    stlInfo.maxMoney = maxMoney

    local curExp = stlData.exp
    local maxExp = stlLevelCfg.levelUpExp
    stlInfo.curExp = curExp
    stlInfo.maxExp = maxExp
    stlInfo.expProgress = maxExp ~= 0 and curExp / maxExp or 1
    stlInfo.canUpgrade = maxExp ~= 0 and curExp >= maxExp

    local missionId = stlLevelCfg.upgradeMissionId
    self:_UpdateMissionInfo(stlInfo, missionId)

    local officerId = stlData.officerCharId
    self:_UpdateOfficerInfo(stlInfo, officerId)

    local sellItemId = settlementSystem:GetCurSellItem(stlId)

    local isActivityItem = not string.isEmpty(sellItemId) and
        not string.isEmpty(stlLevelCfg.settlementTradeItemMap[sellItemId].activityId)
    local hasActivity = self.m_activityInfo.hasActivity and
        self.m_activityInfo.domainActivityInfos[self.m_domainInfo.id] ~= nil and
        self.m_activityInfo.domainActivityInfos[self.m_domainInfo.id][stlId] ~= nil
    local isHide = false
    if isActivityItem then
        isHide = not hasActivity or not GameInstance.player.inventory:IsItemFound(sellItemId)
    end
    if isHide then
        sellItemId = ""
        settlementSystem:SetCurSellItem(stlId, sellItemId)
    end
    self:_UpdateSellItemInfo(stlInfo, sellItemId)
end

SettlementMainCtrl._UpdateMissionInfo = HL.Method(HL.Table, HL.String) << function(self, stlInfo, missionId)
    local upgradeMissionInfo = stlInfo.upgradeMissionInfo
    if upgradeMissionInfo.missionId == missionId then
        return
    end
    upgradeMissionInfo.missionId = missionId
    if not string.isEmpty(missionId) then
        upgradeMissionInfo.upgradeMissionTips = Language.LUA_SETTLEMENT_UPGRADE_MISSION_TIPS
        upgradeMissionInfo.isProcessing = missionSystem:GetMissionState(missionId) == CS.Beyond.Gameplay.MissionSystem.MissionState.Processing
    else
        upgradeMissionInfo.isProcessing = false
    end
end

SettlementMainCtrl._UpdateOfficerInfo = HL.Method(HL.Table, HL.String) << function(self, stlInfo, charId)
    local officerInfo = stlInfo.officerInfo
    if officerInfo.charId == charId then
        return
    end
    local hasCfg, charCfg = Tables.characterTable:TryGetValue(charId)
    if hasCfg then
        officerInfo.charId = charId
        officerInfo.iconName = UIConst.UI_CHAR_HEAD_PREFIX .. charId
        officerInfo.charName = charCfg.name

        officerInfo.enhanceMoneyProduceSpeedRate = 0
        officerInfo.enhanceMoneyProfitRate = 0
        officerInfo.enhanceExpProfitRate = 0
        for _, stlTagInfo in pairs(stlInfo.tagInfos) do
            if settlementSystem:IsCharMatchSettlementTag(charId, stlTagInfo.stlTagId) then
                officerInfo.enhanceMoneyProduceSpeedRate = officerInfo.enhanceMoneyProduceSpeedRate + stlTagInfo.enhanceMoneyProduceSpeedRate
                officerInfo.enhanceMoneyProfitRate = officerInfo.enhanceMoneyProfitRate + stlTagInfo.enhanceMoneyProfitRate
                officerInfo.enhanceExpProfitRate = officerInfo.enhanceExpProfitRate + stlTagInfo.enhanceExpProfitRate
            end
        end

        local effectText = UIUtils.getSettlementEnhanceEffectDesc(officerInfo.enhanceMoneyProduceSpeedRate,
            officerInfo.enhanceMoneyProfitRate,
            officerInfo.enhanceExpProfitRate)
        if string.isEmpty(effectText) then
            officerInfo.effectText = Language.LUA_SETTLEMENT_CHARACTER_NO_EFFECT
        else
            officerInfo.effectText = effectText
        end
    else
        officerInfo.charId = ""
    end
end

SettlementMainCtrl._UpdateSellItemInfo = HL.Method(HL.Table, HL.String) << function(self, stlInfo, itemId)
    local sellItemInfo = stlInfo.sellItemInfo
    if not string.isEmpty(itemId) then
        local stlCfg = Tables.settlementBasicDataTable[stlInfo.stlId]
        local stlLevelCfg = stlCfg.settlementLevelMap[stlInfo.curLevel]
        local tradeItemCfg = stlLevelCfg.settlementTradeItemMap[itemId]
        sellItemInfo.itemId = itemId
        sellItemInfo.localCount = Utils.getDepotItemCount(itemId, Utils.getCurrentScope(), self.m_domainInfo.id)
        sellItemInfo.rewardMoneyCount = tradeItemCfg.rewardMoneyCount
        sellItemInfo.rewardExpCount = tradeItemCfg.stmExp


        local domainId = self.m_domainInfo.id
        local stlId = stlInfo.stlId
        local hasActivity = self.m_activityInfo.hasActivity and
            self.m_activityInfo.domainActivityInfos[domainId] ~= nil and
            self.m_activityInfo.domainActivityInfos[domainId][stlId] ~= nil
        local isActivityItem = hasActivity and self.m_activityInfo.domainActivityInfos[domainId][stlId][itemId] ~= nil
        local activityItemRewardMoneyCount = isActivityItem and (self.m_activityInfo.domainActivityInfos[domainId][stlId][itemId]) or 0
        sellItemInfo.isActivityItem = isActivityItem
        sellItemInfo.rewardActivityMoneyCount = activityItemRewardMoneyCount

    else
        sellItemInfo.itemId = ""
    end
    if self.m_domainInfo.initSelectItemCount > 0 and self.m_domainInfo.defaultStlId == stlInfo.stlId then
        self:_UpdateTradeInfo(stlInfo, self.m_domainInfo.initSelectItemCount)
        self.m_domainInfo.initSelectItemCount = 0
    else
        self:_UpdateTradeInfo(stlInfo, 1)
    end
end

SettlementMainCtrl._UpdateTradeInfo = HL.Method(HL.Table, HL.Opt(HL.Number)) << function(self, stlInfo, curCount)
    local tradeInfo = stlInfo.tradeInfo
    local sellItemInfo = stlInfo.sellItemInfo
    if not string.isEmpty(sellItemInfo.itemId) then
        local maxSelectCountBaseStlMoney = math.min(sellItemInfo.localCount, math.floor(stlInfo.curMoney / sellItemInfo.rewardMoneyCount))
        local maxSelectCount = maxSelectCountBaseStlMoney


        local isTradeActivityItem = sellItemInfo.isActivityItem
        if isTradeActivityItem then
            maxSelectCount = sellItemInfo.localCount
        end


        local minCount = math.min(1, maxSelectCount)
        if not curCount then
            curCount = tradeInfo.selectCount
        end
        curCount = lume.clamp(curCount, minCount, maxSelectCount)
        tradeInfo.selectCount = curCount
        tradeInfo.maxSelectCount = maxSelectCount
        tradeInfo.maxSelectCountBaseStlMoney = maxSelectCountBaseStlMoney
        tradeInfo.curCountBaseStlMoney = math.min(curCount, maxSelectCountBaseStlMoney)
        tradeInfo.totalRewardMoney = tradeInfo.curCountBaseStlMoney * sellItemInfo.rewardMoneyCount
        tradeInfo.totalRewardExp = tradeInfo.curCountBaseStlMoney * sellItemInfo.rewardExpCount


        tradeInfo.totalRewardActivityMoney = curCount * sellItemInfo.rewardActivityMoneyCount

    else
        tradeInfo.selectCount = 0
        tradeInfo.maxSelectCount = 0
        tradeInfo.maxSelectCountBaseStlMoney = 0
        tradeInfo.curCountBaseStlMoney = 0
        tradeInfo.totalRewardMoney = 0
        tradeInfo.totalRewardExp = 0


        tradeInfo.totalRewardActivityMoney = 0

    end
end





SettlementMainCtrl._InitUI = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.domainTopMoneyTitle.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.officerNode.changeOfficerBtn.onClick:AddListener(function()
        local stlInfo = self.m_stlInfoList[self.m_curSelectStlIndex]
        if not stlInfo then
            return
        end
        PhaseManager:OpenPhase(PhaseId.SettlementChar, stlInfo.stlId)
    end)

    self.view.tradeNode.playerDepotStore.changeItemBtn.onClick:AddListener(function()
        local stlInfo = self.m_stlInfoList[self.m_curSelectStlIndex]
        if not stlInfo then
            return
        end
        PhaseManager:OpenPhase(PhaseId.SettlementCommodity, {
            settlementId = stlInfo.stlId,
            settlementLevel = stlInfo.curLevel,
            curSellItem = stlInfo.sellItemInfo.itemId,
            onConfirmChanged = function(itemId)
                local isOpen, settleMainCtrl = UIManager:IsOpen(PanelId.SettlementMain)
                if not isOpen then
                    return
                end
                local self = settleMainCtrl
                local curInfo = self.m_stlInfoList[self.m_curSelectStlIndex]
                GameInstance.player.settlementSystem:SetCurSellItem(curInfo.stlId, itemId)
                local preItemIsActivityItem = curInfo.sellItemInfo.isActivityItem or false
                self:_UpdateSellItemInfo(curInfo, itemId)
                local nowItemIsActivityItem = curInfo.sellItemInfo.isActivityItem or false
                self:_RefreshTradeNodeUI()

                if preItemIsActivityItem ~= nowItemIsActivityItem then
                    if nowItemIsActivityItem then
                        self.view.tradeNode.stlStore.activityMoneyAniWrapper:PlayInAnimation()
                    else
                        self.view.tradeNode.stlStore.activityMoneyAniWrapper:PlayOutAnimation()
                    end
                end
            end
        })
    end)

    self.view.jumpMissionBtn.onClick:AddListener(function()
        local stlInfo = self.m_stlInfoList[self.m_curSelectStlIndex]
        if not stlInfo then
            return
        end
        local missionId = stlInfo.upgradeMissionInfo.missionId
        PhaseManager:OpenPhase(PhaseId.Mission, {
            autoSelect = missionId
        })
    end)


    self.m_genStlCellFunc = UIUtils.genCachedCellFunction(self.view.settlementList)
    self.view.settlementList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnRefreshStlCell(self.m_genStlCellFunc(obj), LuaIndex(csIndex))
    end)

    local preActionId = self.view.keyHintLeft.actionId
    local nextActionId = self.view.keyHintRight.actionId
    self:BindInputPlayerAction(preActionId, function()
        local count = #self.m_stlInfoList
        local newIndex = (self.m_curSelectStlIndex + count - 2) % count + 1
        if newIndex ~= self.m_curSelectStlIndex then
            self:_OnChangeSelectStl(newIndex)
            self.view.settlementList:ScrollToIndex(CSIndex(newIndex))

            AudioManager.PostEvent("Au_UI_Toggle_Tab_On")
        end
    end)
    self:BindInputPlayerAction(nextActionId, function()
        local count = #self.m_stlInfoList
        local newIndex = self.m_curSelectStlIndex % count + 1
        if newIndex ~= self.m_curSelectStlIndex then
            self:_OnChangeSelectStl(newIndex)
            self.view.settlementList:ScrollToIndex(CSIndex(newIndex))

            AudioManager.PostEvent("Au_UI_Toggle_Tab_On")
        end
    end)

    self.m_getTagCells = UIUtils.genCellCache(self.view.tagCell)



    self.m_moneyStoreCellCache = UIUtils.genCellCache(self.view.tradeNode.stlStore.moneyStoreCell)
    self.m_itemStoreCellCache = UIUtils.genCellCache(self.view.tradeNode.playerDepotStore.itemStoreCell)
    self.m_moneyStoreCellCache:Refresh(self.view.config.STORE_CELL_COUNT)
    self.m_itemStoreCellCache:Refresh(self.view.config.STORE_CELL_COUNT)

    self.m_onSelectorNumberChanged = function(curNumber, isChangeByBtn)
        if not self.m_stlInfoList then
            return
        end
        local stlInfo = self.m_stlInfoList[self.m_curSelectStlIndex]
        if not stlInfo then
            return
        end
        local tradeInfo = stlInfo.tradeInfo
        local preCount = tradeInfo.selectCount
        local preCountBaseStlMoney = tradeInfo.curCountBaseStlMoney
        self:_UpdateTradeInfo(stlInfo, curNumber)
        self:_RefreshTradeNodeUI()

        if not self.m_domainInfo.isChangingStl then
            local curCount = tradeInfo.selectCount
            local curCountBaseStlMoney = tradeInfo.curCountBaseStlMoney
            if preCountBaseStlMoney ~= curCountBaseStlMoney then
                self:_OnSelectItemCountPlayAni(preCountBaseStlMoney < curCountBaseStlMoney, false)
            elseif preCount ~= curCount then
                self:_OnSelectItemCountPlayAni(preCount < curCount, true)
            end
        end
    end
    self.view.tradeNode.numberSelector:InitNumberSelector(1, 1, 1, self.m_onSelectorNumberChanged)

    self.view.tradeNode.stlStore.moneyTipsBtn.onClick:AddListener(function()
        local stlInfo = self.m_stlInfoList[self.m_curSelectStlIndex]
        if not stlInfo then
            return
        end
        UIManager:Open(PanelId.SettlementTokenInstruction, stlInfo.stlId)
    end)

    self.view.tradeNode.tradeBtn.onClick:AddListener(function()
        self:_OnSellItem()
    end)

    self.view.tradeNode.centerNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
        local tradeNode = self.view.tradeNode
        tradeNode.playerDepotStore.changeItemBtn.gameObject:SetActive(not isFocused)
        tradeNode.stlStore.moneyTipsBtn.interactable = not isFocused
        tradeNode.reduceBtnKeyHint.gameObject:SetActive(not isFocused)
        tradeNode.addBtnKeyHint.gameObject:SetActive(not isFocused)
    end)
    self.view.tradeNode.centerNaviGroup.getDefaultSelectableFunc = function()
        return self.view.tradeNode.stlStore.moneyItemTipsBtn
    end

end

SettlementMainCtrl._RefreshAllUI = HL.Method() << function(self)
    self.view.settlementList:UpdateCount(#self.m_stlInfoList, true)
    self.view.keyHintContent.gameObject:SetActive(#self.m_stlInfoList > 1)
    self:_RefreshDomainUI()
    self:_RefreshCurSettlementUI()



    local hasActivity = self:_CurStlHasActivity()
    if hasActivity then
        local tradeNode = self.view.tradeNode
        local activityColor = self.m_activityInfo.activityColor
        local moneyItemName = UIUtils.getItemName(self.m_activityInfo.activityMoneyId)
        tradeNode.aniNode.activityWalletIconImage.color = activityColor
        tradeNode.stlStore.activityBgImg.color = activityColor
        tradeNode.stlStore.activityIconImg.color = activityColor
        tradeNode.stlStore.activityTipsTxt.text = string.format(Language.LUA_SETTLEMENT_TRADE_ACTIVITY_ITEM_TIPS, moneyItemName)
        tradeNode.playerDepotStore.activityBgImg.color = activityColor
        tradeNode.playerDepotStore.activityIconImg.color = activityColor
    end

end

SettlementMainCtrl._OnRefreshStlCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local stlInfo = self.m_stlInfoList[luaIndex]
    cell.gameObject.name = "StlCell_" .. luaIndex
    cell.settlementNameTxt.text = stlInfo.stlName
    cell.settlementLvTxt.text = stlInfo.curLevel

    local progressTxt
    if stlInfo.curLevel >= stlInfo.maxLevel then
        progressTxt = "-/-"
    else
        progressTxt = stlInfo.curExp .. "/" .. stlInfo.maxExp
    end

    cell.moneyProgressBar.fillAmount = stlInfo.curMoney / stlInfo.maxMoney

    local officerInfo = stlInfo.officerInfo
    if not string.isEmpty(officerInfo.charId) then
        cell.officerIconImg.spriteName = officerInfo.iconName
    end

    local flag = 0
    if self.m_curSelectStlIndex == luaIndex then
        flag = flag | StlStateFlag.IsSelect
    end
    if stlInfo.canUpgrade then
        flag = flag | StlStateFlag.CanUpgrade
    end
    if not string.isEmpty(stlInfo.officerInfo.charId) then
        flag = flag | StlStateFlag.HasOfficer
    end
    if stlInfo.curMoney == 0 then
        flag = flag | StlStateFlag.IsMoneyEmpty
    elseif stlInfo.curMoney == stlInfo.maxMoney then
        flag = flag | StlStateFlag.IsMoneyFilled
    end
    cell.expProgressStateCtrl:SetState(StlCellStateNameMap.expProgress[flag & StlCellStateNameMap.expProgress.checkFlag])
    cell.officerStateCtrl:SetState(StlCellStateNameMap.officer[flag & StlCellStateNameMap.officer.checkFlag])
    cell.moneyProgressStateCtrl:SetState(StlCellStateNameMap.moneyProgress[flag & StlCellStateNameMap.moneyProgress.checkFlag])
    cell.animationWrapper:SampleClipAtPercent(self.m_curSelectStlIndex == luaIndex and "settlementmainscrollcell_selected" or "settlementmainscrollcell_normal", 1)

    cell.cellBtn.interactable = self.m_curSelectStlIndex ~= luaIndex
    cell.cellBtn.onClick:RemoveAllListeners()
    cell.cellBtn.onClick:AddListener(function()
        if self.m_curSelectStlIndex ~= luaIndex then
            self:_OnChangeSelectStl(luaIndex)
        end
    end)

    cell.redDot:InitRedDot("SettlementMainTab", stlInfo.stlId)
end

SettlementMainCtrl._RefreshDomainUI = HL.Method() << function(self)
    local domainInfo = self.m_domainInfo
    self.view.domainIconImg:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT_ICON_BIG, domainInfo.icon)
end

SettlementMainCtrl._RefreshTitleMoneyUI = HL.Method() << function(self)
    local hasActivity = self:_CurStlHasActivity()
    if hasActivity then
        self.view.domainTopMoneyTitle:InitDomainTopMoneyTitle(self.m_domainInfo.id, { self.m_activityInfo.activityMoneyId })
    else
        self.view.domainTopMoneyTitle:InitDomainTopMoneyTitle(self.m_domainInfo.id)
    end
end

SettlementMainCtrl._RefreshCurSettlementUI = HL.Method() << function(self)

    local stlInfo = self.m_stlInfoList[self.m_curSelectStlIndex]
    if not stlInfo then
        local msg = string.format("stlInfo为空！curSelectStlIndex：%s；domainId：%s；defaultStlId：%s", self.m_curSelectStlIndex, self.m_domainInfo.id, self.m_domainInfo.defaultStlId or "Non")
        logger.error(msg)
        return
    end
    self.view.settlementLvTxt.text = stlInfo.curLevel
    self.view.settlementNameTxt.text = stlInfo.stlName
    self.view.levelNameTxt.text = stlInfo.levelName
    if stlInfo.curLevel >= stlInfo.maxLevel then
        self.view.expProgressTxt.text = "-/-"
        self.view.tradeNode.rewardExpTextStateCtrl:SetState("ExpMax")
    else
        self.view.expProgressTxt.text = stlInfo.curExp .. "/" .. stlInfo.maxExp
        self.view.tradeNode.rewardExpTextStateCtrl:SetState(stlInfo.curExp < stlInfo.maxExp and "ExpNotMax" or "ExpMax")
    end
    self.view.expProgressBar.value = stlInfo.expProgress
    self.view.settlementDescTxt.text = stlInfo.stlDesc
    self.view.settlementPicImg:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT_DETAIL_LEVEL, stlInfo.stlPic)
    self.view.domainIconBgImg.color = Color(stlInfo.stlColor.r, stlInfo.stlColor.g, stlInfo.stlColor.b, self.view.domainIconBgImg.color.a)
    self.view.bottomMaskImg.color = Color(stlInfo.stlColor.r, stlInfo.stlColor.g, stlInfo.stlColor.b, self.view.bottomMaskImg.color.a)
    self.view.colorBgImage.color = Color(stlInfo.stlColor.r, stlInfo.stlColor.g, stlInfo.stlColor.b, self.view.colorBgImage.color.a)
    self:_RefreshTitleMoneyUI()

    local missionInfo = stlInfo.upgradeMissionInfo
    self.view.missionTipsTxt.text = missionInfo.upgradeMissionTips
    if stlInfo.canUpgrade and not string.isEmpty(missionInfo.missionId) and missionInfo.isProcessing then
        self.view.levelUpStateCtrl:SetState("CanUpgradeState")
    else
        self.view.levelUpStateCtrl:SetState("NormalState")
    end

    self:_RefreshOfficerUI()

    self:_RefreshTradeNodeUI()


    local hasActivity = self:_CurStlHasActivity()
    if hasActivity then
        self.view.mainStateCtrl:SetState("Activity")
        local stlStore = self.view.tradeNode.stlStore
        local activityMoneyId = self.m_activityInfo.activityMoneyId
        stlStore.activityMoneyBigIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, activityMoneyId)
        stlStore.activityMoneySmallIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, activityMoneyId)
        stlStore.activityMoneyTipsBtn.onClick:RemoveAllListeners()
        stlStore.activityMoneyTipsBtn.onClick:AddListener(function()
            Notify(MessageConst.SHOW_ITEM_TIPS, {
                itemId = activityMoneyId,

                transform = stlStore.activityMoneyBigIcon.transform,
                posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
                isSideTips = DeviceInfo.usingController,
            })
        end)
    else
        self.view.mainStateCtrl:SetState("Normal")
    end

end

SettlementMainCtrl._RefreshOfficerUI = HL.Method() << function(self)
    local stlInfo = self.m_stlInfoList[self.m_curSelectStlIndex]
    if not stlInfo then
        return
    end
    self.m_getTagCells:Refresh(#stlInfo.tagInfos, function(cell, luaIndex)
        self:_OnRefreshTagCell(cell, luaIndex)
    end)
    local officerNode = self.view.officerNode
    local officerInfo = stlInfo.officerInfo
    if not string.isEmpty(officerInfo.charId) then
        officerNode.officerStateCtrl:SetState("NormalState")
        officerNode.officerNameTxt.text = officerInfo.charName
        officerNode.officerIconImg.spriteName = officerInfo.iconName
        officerNode.officerEffectTxt:SetAndResolveTextStyle(officerInfo.effectText)
    else
        officerNode.officerStateCtrl:SetState("EmptyState")
    end
end

SettlementMainCtrl._OnRefreshTagCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local stlInfo = self.m_stlInfoList[self.m_curSelectStlIndex]
    if not stlInfo then
        return
    end
    local tagInfo = stlInfo.tagInfos[luaIndex]
    local officerInfo = stlInfo.officerInfo
    local isMatch = false
    if not string.isEmpty(officerInfo.charId) then
        isMatch = settlementSystem:IsCharMatchSettlementTag(officerInfo.charId, tagInfo.stlTagId)
    end
    cell.tagNameTxt.text = tagInfo.name
    cell.matchStateCtrl:SetState(isMatch and "MatchState" or "MismatchState")
end



SettlementMainCtrl._RefreshTradeNodeUI = HL.Method(HL.Opt(HL.Boolean)) << function(self, onlyUpdateCount)

    local tradeNode = self.view.tradeNode
    local stlStore = tradeNode.stlStore
    local playerStore = tradeNode.playerDepotStore
    local stlInfo = self.m_stlInfoList[self.m_curSelectStlIndex]
    if not stlInfo then
        return
    end
    local tradeInfo = stlInfo.tradeInfo
    local sellItemInfo = stlInfo.sellItemInfo

    if not string.isEmpty(sellItemInfo.itemId) then

        if sellItemInfo.isActivityItem or stlInfo.curMoney >= sellItemInfo.rewardMoneyCount then
            tradeNode.tradeStateCtrl:SetState("CanTradeState")
        else
            tradeNode.tradeStateCtrl:SetState("OutOfMoneyState")
        end
        if not onlyUpdateCount then
            local hasCfg, itemCfg = Tables.itemTable:TryGetValue(sellItemInfo.itemId)
            if hasCfg then
                playerStore.tradeItemImg:LoadSprite(UIConst.UI_SPRITE_ITEM, itemCfg.iconId)
            else
                logger.error("Item表配置不存在！id：", sellItemInfo.itemId)
            end
            playerStore.itemTipsBtn.onClick:RemoveAllListeners()
            playerStore.itemTipsBtn.onClick:AddListener(function()
                Notify(MessageConst.SHOW_ITEM_TIPS, {
                    itemId = sellItemInfo.itemId,
                    itemCount = sellItemInfo.localCount,

                    transform = playerStore.tradeItemImg.transform,
                    posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
                    isSideTips = DeviceInfo.usingController,
                })
            end)
        end
        playerStore.depotNumTxt.text = sellItemInfo.localCount
        playerStore.depotNumTxt.color = sellItemInfo.localCount <= 0 and self.view.config.NUM_COLOR_NOT_ENOUGH or self.view.config.NUM_COLOR_ENOUGH
        playerStore.tradeItemNumTxt.text = string.format("-%d", tradeInfo.selectCount)

        local maxCount = self.m_domainInfo.itemStoreLimitCount
        local curStoreItemCellCount = math.ceil(sellItemInfo.localCount / maxCount * self.view.config.STORE_CELL_COUNT)
        local totalTradeItemCellCount = math.ceil(tradeInfo.selectCount / maxCount * self.view.config.STORE_CELL_COUNT)
        local remainItemCellCount = curStoreItemCellCount - totalTradeItemCellCount
        self.m_itemStoreCellCache:Update(function(cell, luaIndex)
            self:_RefreshStoreCell(cell, luaIndex, remainItemCellCount, curStoreItemCellCount)
        end)
    else
        tradeNode.tradeStateCtrl:SetState("NoItemState")
        playerStore.depotNumTxt.text = "-"
    end
    playerStore.depotTitleTxt.text = string.format(Language.LUA_SETTLEMENT_DEPOT_TITLE, self.m_domainInfo.name)



    if not onlyUpdateCount then
        stlStore.moneyBigIconImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, stlInfo.moneyIcon)
        stlStore.moneyIconImg:LoadSprite(UIConst.UI_SPRITE_WALLET, stlInfo.moneyIcon)
        stlStore.moneyItemTipsBtn.onClick:RemoveAllListeners()
        stlStore.moneyItemTipsBtn.onClick:AddListener(function()
            Notify(MessageConst.SHOW_ITEM_TIPS, {
                itemId = self.m_domainInfo.moneyId,
                itemCount = stlInfo.curMoney,
                transform = stlStore.moneyBigIconImg.transform,

                posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
                isSideTips = DeviceInfo.usingController,
            })
        end)
    end
    stlStore.curMoneyNumTxt.text = stlInfo.curMoney
    stlStore.tradeMoneyNumTxt.text = string.format("+%d", tradeInfo.totalRewardMoney)
    tradeNode.expNumTxt.text = math.tointeger(tradeInfo.totalRewardExp)
    if stlInfo.curLevel < stlInfo.maxLevel and stlInfo.curExp < stlInfo.maxExp then
        if stlInfo.curExp + tradeInfo.totalRewardExp < stlInfo.maxExp then
            tradeNode.rewardExpTextStateCtrl:SetState("ExpNotMax")
        else
            tradeNode.rewardExpTextStateCtrl:SetState("ExpWillMax")
        end
    else
        tradeNode.rewardExpTextStateCtrl:SetState("ExpMax")
    end

    local curStoreMoneyCellCount = math.ceil(stlInfo.curMoney / stlInfo.maxMoney * self.view.config.STORE_CELL_COUNT)
    local totalRewardMoneyCellCount = math.ceil(tradeInfo.totalRewardMoney / stlInfo.maxMoney * self.view.config.STORE_CELL_COUNT)
    local remainMoneyCellCount = curStoreMoneyCellCount - totalRewardMoneyCellCount
    self.m_moneyStoreCellCache:Update(function(cell, luaIndex)
        self:_RefreshStoreCell(cell, luaIndex, remainMoneyCellCount, curStoreMoneyCellCount)
    end)



    local isTradeActivityItem = self.m_activityInfo.hasActivity and sellItemInfo.isActivityItem
    if isTradeActivityItem then
        tradeNode.tradeStateCtrl:SetState("TradeActivityItem")
        self.view.tradeNode.stlStore.activityMoneyAniWrapper:SampleToInAnimationEnd()
        stlStore.activityTradeMoneyNumTxt.text = string.format("+%d", tradeInfo.totalRewardActivityMoney)
    else
        tradeNode.tradeStateCtrl:SetState("NotTradeActivityItem")
        self.view.tradeNode.stlStore.activityMoneyAniWrapper:SampleToOutAnimationEnd()
    end



    tradeNode.numberSelector:RefreshNumber(
        tradeInfo.selectCount,
        math.min(1, tradeInfo.maxSelectCount),
        tradeInfo.maxSelectCount
    )


    Notify(MessageConst.REFRESH_CONTROLLER_HINT)
end

SettlementMainCtrl._RefreshStoreCell = HL.Method(HL.Any, HL.Number, HL.Number, HL.Number) << function(self, cell, luaIndex, remainCount, curCount)
    if luaIndex <= remainCount then
        cell.imgStateCtrl:SetState("Normal")
    elseif luaIndex <= curCount then
        cell.imgStateCtrl:SetState("Red")
    else
        cell.imgStateCtrl:SetState("Empty")
    end
end







SettlementMainCtrl._OnChangeSelectStl = HL.Method(HL.Number) << function(self, newLuaIndex)
    self.m_domainInfo.isChangingStl = true
    local oldIndex = self.m_curSelectStlIndex
    self.m_curSelectStlIndex = newLuaIndex
    if newLuaIndex <= 0 or newLuaIndex > #self.m_stlInfoList then
        logger.error("select settlement index out of range: " .. newLuaIndex)
        return
    end
    self:_ResetTradeIconAni()
    local cell = self.m_genStlCellFunc(oldIndex)
    if cell then
        cell.animationWrapper:Play("settlementmainscrollcell_normal")
        cell.cellBtn.interactable = true
    end
    cell = self.m_genStlCellFunc(newLuaIndex)
    if cell then
        cell.animationWrapper:Play("settlementmainscrollcell_selected")
        cell.cellBtn.interactable = false
    end
    self:_RefreshCurSettlementUI()

    self.view.stlNodeAniWrapper:Play(newLuaIndex > oldIndex and "settlementmainnew_change" or "settlementmainnew_changeleft")
    self.m_domainInfo.isChangingStl = false
end

SettlementMainCtrl._OnUpdateOfficer = HL.Method(HL.Any) << function(self, arg)
    local stlId, officerId = unpack(arg)
    local targetIndex = 0
    local targetStlInfo

    for index, stlInfo in pairs(self.m_stlInfoList) do
        if stlInfo.stlId == stlId then
            targetIndex = index
            targetStlInfo = stlInfo
            break
        end
    end

    if not targetStlInfo then
        return
    end
    self:_UpdateOfficerInfo(targetStlInfo, officerId)

    local cell = self.m_genStlCellFunc(targetIndex)
    if cell then
        self:_OnRefreshStlCell(cell, targetIndex)
    end
    if self.m_curSelectStlIndex == targetIndex then
        self:_RefreshOfficerUI()
    end
end

SettlementMainCtrl._OnUpdateTickMoney = HL.Method(HL.Any) << function(self, arg)
    local stlId, curMoney = unpack(arg)
    local targetIndex = 0
    local targetStlInfo

    for index, stlInfo in pairs(self.m_stlInfoList) do
        if stlInfo.stlId == stlId then
            targetIndex = index
            targetStlInfo = stlInfo
            break
        end
    end

    if not targetStlInfo then
        return
    end
    targetStlInfo.curMoney = curMoney
    self:_UpdateTradeInfo(targetStlInfo)

    local cell = self.m_genStlCellFunc(targetIndex)
    if cell then
        self:_OnRefreshStlCell(cell, targetIndex)
    end
    if self.m_curSelectStlIndex == targetIndex then
        self:_RefreshTradeNodeUI()
    end

    if targetStlInfo.curMoney < targetStlInfo.maxMoney then
        local moneyStoreCellIndex = math.ceil(targetStlInfo.curMoney / targetStlInfo.maxMoney * self.view.config.STORE_CELL_COUNT) + 1

        self.m_moneyStoreCellAniInterval = self:_ClearCoroutine(self.m_moneyStoreCellAniInterval)
        self.m_moneyStoreCellAniInterval = self:_StartCoroutine(function()
            for i = moneyStoreCellIndex, self.view.config.STORE_CELL_COUNT do
                local moneyStoreCell = self.m_moneyStoreCellCache:Get(i)
                if moneyStoreCell then
                    moneyStoreCell.animationWrapper:Play("storecelllight_in")
                    coroutine.wait(moneyStoreCell.config.CELL_LIGHT_ANI_INTERVAL)
                end
            end
        end)
    end
end

SettlementMainCtrl._OnSettlementModify = HL.Method(HL.Any) << function(self, arg)
    local stlId = unpack(arg)
    local targetIndex = 0
    local targetStlInfo

    for index, stlInfo in pairs(self.m_stlInfoList) do
        if stlInfo.stlId == stlId then
            targetIndex = index
            targetStlInfo = stlInfo
            break
        end
    end

    if not targetStlInfo then
        return
    end
    local isCurSelectStl = self.m_curSelectStlIndex == targetIndex
    self:_UpdateStlRuntimeInfo(targetStlInfo)

    self:_RefreshTitleMoneyUI()
    local cell = self.m_genStlCellFunc(targetIndex)
    if cell then
        self:_OnRefreshStlCell(cell, targetIndex)
    end
    if isCurSelectStl then
        self:_RefreshCurSettlementUI()
    end
end

SettlementMainCtrl._TryUpdateItemDepot = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    for _, stlInfo in pairs(self.m_stlInfoList) do
        local sellItemId = stlInfo.sellItemInfo.itemId
        if not string.isEmpty(sellItemId) then
            local sellItemInfo = stlInfo.sellItemInfo
            local nowCount = Utils.getDepotItemCount(sellItemId, Utils.getCurrentScope(), self.m_domainInfo.id)
            if nowCount ~= sellItemInfo.localCount then
                sellItemInfo.localCount = nowCount
                self:_UpdateTradeInfo(stlInfo)
            end
        end
    end
    self:_RefreshTradeNodeUI(true)
end

SettlementMainCtrl._OnSellItem = HL.Method() << function(self)
    local stlInfo = self.m_stlInfoList[self.m_curSelectStlIndex]
    if not stlInfo then
        return
    end
    if stlInfo.tradeInfo.selectCount <= 0 or stlInfo.sellItemInfo.localCount <= 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SETTLEMENT_SELL_ITEM_NOT_ENOUGH)
        return
    end

    self.m_remindExcessTrade = GameInstance.player.commonBitsetSystem:IsBitEnable(GEnums.BitsetType.CltDailyCommon, GEnums.CltDailyCommonTypeInBitset.IgnoreStlExcessTradeTips:GetHashCode())
    if not self.m_remindExcessTrade and stlInfo.tradeInfo.selectCount > stlInfo.tradeInfo.maxSelectCountBaseStlMoney then
        local toggleArg = {
            toggleText = Language.LUA_WEP_NO_HINT_TODAY_HINT,
            isOn = false,
            onValueChanged = function(value)
                self.m_remindExcessTrade = value
            end,
        }
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_SETTLEMENT_EXCESS_TRADE_CONFIRM_TITLE,
            toggle = toggleArg,
            onConfirm = function()
                if self.m_remindExcessTrade then
                    GameInstance.player.commonBitsetSystem:EnableBit(GEnums.BitsetType.CltDailyCommon, GEnums.CltDailyCommonTypeInBitset.IgnoreStlExcessTradeTips:GetHashCode())
                end
                self:_ExecuteSellItem(stlInfo)
            end,
        })
    else
        self:_ExecuteSellItem(stlInfo)
    end
end

SettlementMainCtrl._ExecuteSellItem = HL.Method(HL.Table) << function(self, stlInfo)
    settlementSystem:SendSellItem(stlInfo.stlId, stlInfo.sellItemInfo.itemId, stlInfo.tradeInfo.selectCount)
    InputManagerInst:ToggleGroup(self.view.inputGroup.groupId, false)
    self.view.tradeNode.numberSelector.view.slider.interactable = false
    self.m_waitTradeComplete = true
    self:_ResetTradeIconAni()
end

SettlementMainCtrl._OnTradeSuccess = HL.Method(HL.Any) << function(self, rawMsg)
    AudioManager.PostEvent("Au_UI_Event_Animate_SettlementTrade")

    local msg = unpack(rawMsg)
    self.m_waitTradeComplete = false

    if msg.RealSellCount == 0 then
        InputManagerInst:ToggleGroup(self.view.inputGroup.groupId, true)
        self.view.tradeNode.numberSelector.view.slider.interactable = true
        local stlInfo = self.m_stlInfoList[self.m_curSelectStlIndex]
        if not stlInfo then
            return
        end
        stlInfo.sellItemInfo.localCount = 0
        self:_UpdateTradeInfo(stlInfo)
        self:_RefreshTradeNodeUI(true)
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SETTLEMENT_TRADE_NOT_ENOUGH_ZERO)
        if self.m_hasActivityUpdateMsgWaitTradeComplete then
            self:_OnActivityStageUpdate({ self.m_activityInfo.activityI })
        end
        return
    end

    local tradeAni = "tradenodenewpc_done"
    local tradeAniReset = "tradenodenewpc_default"
    local isMobileAni = DeviceInfo.isMobile and ((Screen.width / Screen.height) >= (16/9))
    if isMobileAni then
        tradeAni = "tradenodenewmobile_done"
        tradeAniReset = "tradenodenewmobile_default"
    end
    self.view.tradeNode.animationWrapper:Play(tradeAni, function()
        self.view.tradeNode.animationWrapper:Play(tradeAniReset)
        self:_ShowTradeReward(rawMsg)
        if self.m_hasActivityUpdateMsgWaitTradeComplete then
            self:_OnActivityStageUpdate({ self.m_activityInfo.activityI })
        end
    end)
end

SettlementMainCtrl._ShowTradeReward = HL.Method(HL.Any) << function(self, rawMsg)

    local msg = unpack(rawMsg)

    local baseRewardMap = msg.RewardBase
    local bonusRewardMap = msg.RewardBonus
    local showItemInfos = {}

    for itemId, itemCount in cs_pairs(baseRewardMap) do
        if not string.isEmpty(itemId) and itemCount > 0 then
            local itemInfo = {
                id = itemId,
                count = itemCount,
                customSortId = 2,
            }
            table.insert(showItemInfos, itemInfo)
        end
    end

    for itemId, itemCount in cs_pairs(bonusRewardMap) do
        if not string.isEmpty(itemId) and itemCount > 0 then
            local itemInfo = {
                id = itemId,
                count = itemCount,
                customSortId = 1,
                isExtra = true,
            }
            table.insert(showItemInfos, itemInfo)
        end
    end

    local args = {
        title = Language.LUA_SETTLEMENT_SELL_ITEM_REWARD_TOAST_TITLE,
        items = showItemInfos,
        icon = self.view.config.SELL_ITEM_REWARD_TOAST_ICON,
        onComplete = function()
            self.view.tradeNode.numberSelector.view.slider.interactable = true
            if not GameInstance.player.guide.isInGuide then
                InputManagerInst:ToggleGroup(self.view.inputGroup.groupId, true)
            end
        end
    }
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, args)

    if msg.ExpectSellCount ~= msg.RealSellCount then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SETTLEMENT_TRADE_NOT_ENOUGH_AUTO_ADAPTIVE_COUNT)
    end
end

SettlementMainCtrl._CurStlHasActivity = HL.Method().Return(HL.Boolean) << function(self)
    local info = self.m_activityInfo
    if not info.hasActivity then
        return false
    end

    local domainActivityInfo = info.domainActivityInfos[self.m_domainInfo.id]
    if domainActivityInfo == nil then
        return false
    end

    return domainActivityInfo[self.m_stlInfoList[self.m_curSelectStlIndex].stlId] ~= nil
end

SettlementMainCtrl._OnActivityStageUpdate = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if self.m_activityInfo == nil or self.m_activityInfo.activityId ~= activityId then
        return
    end
    if self.m_waitTradeComplete then
        self.m_hasActivityUpdateMsgWaitTradeComplete = true
        return
    end
    self:_UpdateData(false)
    self:_RefreshAllUI()
    self.m_hasActivityUpdateMsgWaitTradeComplete = false
end

SettlementMainCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local resumeOpenPanel = {}
    if PhaseManager:GetTopPhaseId() == PHASE_ID then
        if UIManager:IsOpen(PanelId.SettlementTokenInstruction) then
            table.insert(resumeOpenPanel, {
                panelId = PanelId.SettlementTokenInstruction,
                arg = self.m_stlInfoList[self.m_curSelectStlIndex].stlId
            })
        end
    end
    local arg = {
        resumeState = self:_CollectResumeState(),
        resumeOpenPanel = resumeOpenPanel,
    }
    return arg
end

SettlementMainCtrl._CollectResumeState = HL.Method().Return(HL.Table) << function(self)
    local stlInfo = self.m_stlInfoList[self.m_curSelectStlIndex]
    local tradeInfo = stlInfo.tradeInfo
    return {
        domainId = self.m_domainInfo.id,
        defaultStlId = stlInfo.stlId,
        selectItemCount = tradeInfo.selectCount,
    }
end




SettlementMainCtrl.m_tradeIconAniInfo = HL.Field(HL.Table)

SettlementMainCtrl._OnSelectItemCountPlayAni = HL.Method(HL.Boolean, HL.Boolean) << function(self, isAdd, onlyActivity)
    if self.m_waitTradeComplete then
        return
    end
    local info = self.m_tradeIconAniInfo
    info.curIsAdd = isAdd
    AudioManager.PostEvent(isAdd and "Au_UI_Event_Animate_SettlementAdd" or "Au_UI_Event_Animate_SettlementReduce")
    local curStage = info.stage
    info.onlyActivityAni = onlyActivity
    if curStage == TradeIconAniStage.None then
        self:_PlayTradeIconAni(isAdd)
    end

    info.lastUpdateAniTime = Time.time
end

SettlementMainCtrl._ResetTradeIconAni = HL.Method() << function(self)
    local info = self.m_tradeIconAniInfo
    if not info then
        return
    end
    local aniNode = self.view.tradeNode.aniNode
    info.stage = TradeIconAniStage.None
    info.curIsAdd = false
    info.lastUpdateAniTime = 0

    aniNode.stlIconInAniWrapper:ClearTween(false)
    aniNode.depotIconInAniWrapper:ClearTween(false)
    aniNode.stlIconOutAniWrapper:ClearTween(false)
    aniNode.depotIconOutAniWrapper:ClearTween(false)
    aniNode.stlIconHideAniWrapper:ClearTween(false)
    aniNode.depotIconHideAniWrapper:ClearTween(false)
    aniNode.activityWalletAniWrapper:ClearTween(false)

    aniNode.stlIconInAniWrapper:SampleClipAtPercent("tradenodenewiconleft_in", 0)
    aniNode.depotIconInAniWrapper:SampleClipAtPercent("tradenodenewiconright_in", 0)
    aniNode.stlIconOutAniWrapper:SampleClipAtPercent("tradenodenewiconleft_out", 0)
    aniNode.depotIconOutAniWrapper:SampleClipAtPercent("tradenodenewiconright_out", 0)
    aniNode.activityWalletAniWrapper:SampleToInAnimationBegin()
end

SettlementMainCtrl._PlayTradeIconAni = HL.Method(HL.Boolean) << function(self, isAdd)
    local aniNode = self.view.tradeNode.aniNode

    local info = self.m_tradeIconAniInfo
    local continueIntervalTime = self.view.config.TRADE_ICON_ANI_CONTINUE_INTERVAL_TIME
    local curInterval = Time.time - info.lastUpdateAniTime
    local curStage = info.stage
    local onlyActivity = info.onlyActivityAni
    if onlyActivity and info.curPlayNormalAni ~= 0 then
        if info.curPlayNormalAni == 1 then
            aniNode.stlIconHideAniWrapper:Play("tradenodeiconleftup_out")
        else
            aniNode.stlIconHideAniWrapper:Play("tradenodeiconleftdown_out")
        end
        info.curPlayNormalAni = 0
    end
    if curStage == TradeIconAniStage.None then

        info.stage = TradeIconAniStage.In
        if isAdd then
            if not onlyActivity then
                info.curPlayNormalAni = 1
                aniNode.stlIconInAniWrapper:Play("tradenodenewiconleft_in")
            end
            aniNode.depotIconInAniWrapper:Play("tradenodenewiconright_in", function()
                self:_PlayTradeIconAni(isAdd)
            end)
        else
            if not onlyActivity then
                info.curPlayNormalAni = -1
                aniNode.stlIconOutAniWrapper:Play("tradenodenewiconleft_out")
            end
            aniNode.depotIconOutAniWrapper:Play("tradenodenewiconright_out", function()
                self:_PlayTradeIconAni(isAdd)
            end)
        end
    elseif isAdd ~= info.curIsAdd then

        aniNode.stlIconHideAniWrapper:ClearTween(true)
        aniNode.depotIconHideAniWrapper:ClearTween(false)
        if isAdd then
            if not onlyActivity then
                aniNode.stlIconHideAniWrapper:Play("tradenodeiconleftup_out")
            end
            aniNode.depotIconHideAniWrapper:Play("tradenodeiconrightup_out", function()
                aniNode.stlIconInAniWrapper:ClearTween(false)
                aniNode.depotIconInAniWrapper:ClearTween(false)
            end)
        else
            if not onlyActivity then
                aniNode.stlIconHideAniWrapper:Play("tradenodeiconleftdown_out")
            end
            aniNode.depotIconHideAniWrapper:Play("tradenodeiconrightdown_out", function()
                aniNode.stlIconOutAniWrapper:ClearTween(false)
                aniNode.depotIconOutAniWrapper:ClearTween(false)
            end)
        end
        info.stage = TradeIconAniStage.None
        self:_PlayTradeIconAni(info.curIsAdd)
    else

        if curStage == TradeIconAniStage.In then
            info.stage = TradeIconAniStage.Loop
            if isAdd then
                if not onlyActivity then
                    info.curPlayNormalAni = 1
                    aniNode.stlIconInAniWrapper:Play("tradenodenewiconleft_loop")
                else
                    if not info.curIsPlayActivityAni then
                        info.curIsPlayActivityAni = true
                        aniNode.activityWalletAniWrapper:ClearTween(false)
                        aniNode.activityWalletAniWrapper:PlayInAnimation(function()
                            self:_PlayActivityIconAni()
                        end)
                    end
                end
                aniNode.depotIconInAniWrapper:Play("tradenodenewiconright_loop", function()
                    self:_PlayTradeIconAni(isAdd)
                end)
            else
                if not onlyActivity then
                    info.curPlayNormalAni = -1
                    aniNode.stlIconOutAniWrapper:Play("tradenodenewiconleft_outloop")
                end
                aniNode.depotIconOutAniWrapper:Play("tradenodenewiconright_outloop", function()
                    self:_PlayTradeIconAni(isAdd)
                end)
            end
        elseif curStage == TradeIconAniStage.Loop then
            if curInterval <= continueIntervalTime then
                info.stage = TradeIconAniStage.In
                self:_PlayTradeIconAni(isAdd)
            else
                info.stage = TradeIconAniStage.Done
                if isAdd then
                    if not onlyActivity then
                        aniNode.stlIconInAniWrapper:Play("tradenodenewiconleft_done")
                    end
                    aniNode.depotIconInAniWrapper:Play("tradenodenewiconright_done", function()
                        self:_PlayTradeIconAni(isAdd)
                    end)
                else
                    if not onlyActivity then
                        aniNode.stlIconOutAniWrapper:Play("tradenodenewiconleft_outdone")
                    end
                    aniNode.depotIconOutAniWrapper:Play("tradenodenewiconright_outdone", function()
                        self:_PlayTradeIconAni(isAdd)
                    end)
                end
            end
        elseif curStage == TradeIconAniStage.Done then
            info.stage = TradeIconAniStage.None
            if curInterval <= continueIntervalTime then
                self:_PlayTradeIconAni(isAdd)
            end
        end
    end

end

SettlementMainCtrl._PlayActivityIconAni = HL.Method() << function(self)
    local nowAniInfo = self.m_tradeIconAniInfo
    nowAniInfo.curIsPlayActivityAni = false
    if self.m_waitTradeComplete then
        return
    end
    local aniNode = self.view.tradeNode.aniNode
    if not nowAniInfo.onlyActivityAni or not nowAniInfo.curIsAdd then
        return
    end
    if nowAniInfo.stage ~= TradeIconAniStage.In and nowAniInfo.stage ~= TradeIconAniStage.Loop then
        return
    end
    nowAniInfo.curIsPlayActivityAni = true
    aniNode.activityWalletAniWrapper:PlayInAnimation(function()
        self:_PlayActivityIconAni()
    end)
end


HL.Commit(SettlementMainCtrl)
