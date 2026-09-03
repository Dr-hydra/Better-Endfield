
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattlePassBuyLevel
local PHASE_ID = PhaseId.BattlePassBuyLevel

BattlePassBuyLevelCtrl = HL.Class('BattlePassBuyLevelCtrl', uiCtrl.UICtrl)

local MIN_REFRESH_TIME = 0.1





BattlePassBuyLevelCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BATTLE_PASS_BUY_LEVEL] = '_OnBuyLevel',
}

BattlePassBuyLevelCtrl.m_bpSystem = HL.Field(HL.Any)

BattlePassBuyLevelCtrl.m_originiumEnough = HL.Field(HL.Boolean) << true

BattlePassBuyLevelCtrl.m_maxLevel = HL.Field(HL.Number) << 1

BattlePassBuyLevelCtrl.m_curLevel = HL.Field(HL.Number) << 1

BattlePassBuyLevelCtrl.m_targetLevel = HL.Field(HL.Number) << 1

BattlePassBuyLevelCtrl.m_buyLevel = HL.Field(HL.Number) << 0

BattlePassBuyLevelCtrl.m_currExp = HL.Field(HL.Number) << 0

BattlePassBuyLevelCtrl.m_seasonData = HL.Field(HL.Any)

BattlePassBuyLevelCtrl.m_rewards = HL.Field(HL.Any)

BattlePassBuyLevelCtrl.m_getCell = HL.Field(HL.Function)

BattlePassBuyLevelCtrl.m_buyOriTrack = HL.Field(HL.Boolean) << false

BattlePassBuyLevelCtrl.m_buyProtocalTrack = HL.Field(HL.Boolean) << false

BattlePassBuyLevelCtrl.m_onBuyClose = HL.Field(HL.Function)

BattlePassBuyLevelCtrl.m_updateKey = HL.Field(HL.Number) << -1


BattlePassBuyLevelCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    self.view.closeButton.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID, function()
            if self.m_onBuyClose ~= nil then
                self.m_onBuyClose(false)
            end
        end)
    end)


    self.m_bpSystem = GameInstance.player.battlePassSystem
    self.m_seasonData = BattlePassUtils.GetSeasonData()
    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.m_maxLevel = self.m_seasonData.maxLevel
    self.m_curLevel = self.m_bpSystem.levelData.currLevel
    self.m_buyOriTrack = BattlePassUtils.CheckOriginiumTrackActive()
    self.m_buyProtocalTrack = BattlePassUtils.CheckPayTrackActive()
    self.m_currExp = self.m_bpSystem.levelData.currExp
    self.m_onBuyClose = arg ~= nil and arg.onBuyClose or nil


    self:_InitViews()


    self:_InitNumberSelector()


    self.view.scrollList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateCell(object, LuaIndex(csIndex))
    end)
    self.view.scrollList:UpdateCount(#self.m_rewards)

    self.m_updateKey = LuaUpdate:Add("Tick", function()
        local isEnabled = self.view.inputGroup.groupEnabled
        self.view.reduceKeyHint.gameObject:SetActive(isEnabled)
        self.view.addKeyHint.gameObject:SetActive(isEnabled)
    end, true)
end

BattlePassBuyLevelCtrl._InitNumberSelector = HL.Method() << function(self)
    local maxBuyLevel = math.max(self.m_maxLevel - self.m_curLevel, 1)
    self.view.numberSelector:InitNumberSelector(maxBuyLevel, 1, maxBuyLevel, function(curNumber)
        self:_RefreshLevelInfos(math.floor(curNumber))
    end)
end

BattlePassBuyLevelCtrl._OnBuyLevel = HL.Method(HL.Table) << function(self, arg)
    local curLevel = unpack(arg)
    local expGap = BattlePassUtils.GetExpGap(self.m_curLevel,self.m_currExp,curLevel,0)
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        title = Language.LUA_BATTLEPASS_BUY_LEVEL_TIPS,
        items = {
            { id = Tables.battlePassConst.bpExpItem , count = expGap }
        },
        onComplete = function()
            if self.m_onBuyClose ~= nil then
                self.m_onBuyClose(true)
            end
        end
    })
    PhaseManager:PopPhase(PHASE_ID)
end

BattlePassBuyLevelCtrl._RefreshLevelInfos = HL.Method(HL.Opt(HL.Number)) << function(self, curNumber)

    self.m_curLevel = self.m_bpSystem.levelData.currLevel
    self.m_buyLevel = curNumber or self.m_buyLevel
    self.m_targetLevel = self.m_buyLevel + self.m_curLevel


    self.view.curLevelText.text = self.m_curLevel
    self.view.buyLevelText.text = self.m_buyLevel
    self.view.targetLevelText.text = self.m_targetLevel
    self.view.costTotalTxt.text = self.m_buyLevel * Tables.battlePassConst.buyLevelMoneyCnt


    self.m_originiumEnough = BattlePassUtils.CheckOriginiumEnough(self.m_buyLevel)
    self.view.mainStateController:SetState(self.m_originiumEnough and "Buy" or "NoMoney")


    self:_RefreshRewards(self.m_curLevel + 1, self.m_targetLevel)
end

BattlePassBuyLevelCtrl._InitViews = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.walletBarPlaceholder:InitWalletBarPlaceholder({ Tables.battlePassConst.buyLevelMoneyID })


    self.view.originiumNode:SetState(self.m_buyOriTrack and "Active" or "NoActive")
    self.view.protocolNode:SetState(self.m_buyProtocalTrack and "Active" or "NoActive")


    self.view.buyBtn.onClick:AddListener(function()
        if not BattlePassUtils.CheckBattlePassSeasonValid() then
            logger.info("BattlePassBuyLevelCtrl: not valid, close.")
            PhaseManager:PopPhase(PHASE_ID, function()
                if self.m_onBuyClose ~= nil then
                    self.m_onBuyClose(false)
                end
            end)
        end
        if self.m_buyOriTrack and self.m_buyProtocalTrack then
            self.m_bpSystem:SendBuyLevel(self.m_targetLevel)
        else
            Notify(MessageConst.SHOW_POP_UP,{
                content = string.format(Language.LUA_BATTLEPASS_BUY_TRACK_TIPS, self.m_buyOriTrack and BattlePassUtils.GetPayTrackInfo().name or BattlePassUtils.GetOriginiumTrackInfo().name),
                onConfirm = function()
                    UIManager:Close(PanelId.CommonPopUp)
                    self.m_bpSystem:SendBuyLevel(self.m_targetLevel)
                end
            })
        end
    end)


    self.view.noMoneyBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_POP_UP,{
            content = Language.LUA_BATTLEPASS_ORIGINIUM_NOT_ENOUGH_TIPS,
            onConfirm = function()
                UIManager:Close(PanelId.CommonPopUp)
                CashShopUtils.GotoCashShopRechargeTab()
            end
        })
    end)
end

BattlePassBuyLevelCtrl._RefreshRewards = HL.Method(HL.Number,HL.Number) << function(self, startLevel, endLevel)
    local levelGroupId = self.m_seasonData.levelGroupId
    local overrideLevelGroupId = self.m_seasonData.ovrLvRewardGroupId
    self.m_rewards = {}
    for i = startLevel,endLevel do
        local overrideLevelData = BattlePassUtils.GetBattlePassOverrideLevelData(i, overrideLevelGroupId)


        local freeRewardId = Tables.battlePassLevelTable[levelGroupId].levelInfos[i].freeRewardId
        self.m_rewards = UIUtils.getRewardItemsMergeSameId(freeRewardId, self.m_rewards)


        if self.m_buyOriTrack then
            local originiumRewardId = Tables.battlePassLevelTable[levelGroupId].levelInfos[i].originiumRewardId
            if overrideLevelData ~= nil then
                originiumRewardId = overrideLevelData.originiumRewardId
            end
            self.m_rewards = UIUtils.getRewardItemsMergeSameId(originiumRewardId, self.m_rewards)
        end


        if self.m_buyProtocalTrack then
            local payRewardId = Tables.battlePassLevelTable[levelGroupId].levelInfos[i].payRewardId
            if overrideLevelData ~= nil then
                payRewardId = overrideLevelData.payRewardId
            end
            self.m_rewards = UIUtils.getRewardItemsMergeSameId(payRewardId, self.m_rewards)
        end
    end
    for i = 1, #self.m_rewards do
        self.m_rewards[i].rarity = Tables.itemTable[self.m_rewards[i].id].rarity
    end
    table.sort(self.m_rewards, Utils.genSortFunction({"rarity"}, false))
    self.view.scrollList:UpdateCount(#self.m_rewards, 1, false, false, true)
end

BattlePassBuyLevelCtrl._OnUpdateCell = HL.Method(HL.Any,HL.Number) << function(self, obj, index)
    local cell = self.m_getCell(obj)
    cell:InitItem(self.m_rewards[index], true)
    if DeviceInfo.usingController then
        cell:SetExtraInfo({
            isSideTips = true,
        })
    end
end

BattlePassBuyLevelCtrl.m_init = HL.Field(HL.Boolean) << false

BattlePassBuyLevelCtrl.OnShow = HL.Override() << function(self)
    if self.m_init then
        self:_RefreshLevelInfos()
    else
        self.m_init = true
    end
end



BattlePassBuyLevelCtrl.OnClose = HL.Override() << function(self)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
end


HL.Commit(BattlePassBuyLevelCtrl)
