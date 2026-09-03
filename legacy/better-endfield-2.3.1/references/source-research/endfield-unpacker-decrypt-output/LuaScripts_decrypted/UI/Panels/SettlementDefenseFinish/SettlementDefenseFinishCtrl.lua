local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementDefenseFinish
local PHASE_ID = PhaseId.SettlementDefenseFinish
SettlementDefenseFinishCtrl = HL.Class('SettlementDefenseFinishCtrl', uiCtrl.UICtrl)

local ZERO_HP_TOLERANCE = 0.01
local DefenseState = CS.Beyond.Gameplay.TowerDefenseSystem.DefenseState

SettlementDefenseFinishCtrl.m_coreHpCells = HL.Field(HL.Forward("UIListCache"))

SettlementDefenseFinishCtrl.m_itemCells = HL.Field(HL.Forward("UIListCache"))

SettlementDefenseFinishCtrl.m_isConfirmed = HL.Field(HL.Boolean) << false

SettlementDefenseFinishCtrl.s_cachedPhaseArgs = HL.StaticField(HL.Table)

SettlementDefenseFinishCtrl.s_lastUnlockedTdId = HL.StaticField(HL.String) << ""





SettlementDefenseFinishCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_LOADING_PANEL_CLOSED] = '_OnLoadingPanelClosed',
}


SettlementDefenseFinishCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local SettlementDefenseTerminalCtrl = require_ex('UI/Panels/SettlementDefenseTerminal/SettlementDefenseTerminalCtrl')
    SettlementDefenseTerminalCtrl.SettlementDefenseTerminalCtrl.s_lastFailedTdId = ''

    self:_InitController()
    GameInstance.player.towerDefenseSystem:RefreshDangerSettlementIds()

    self.m_coreHpCells = UIUtils.genCellCache(self.view.coreHpCell)
    self.m_itemCells = UIUtils.genCellCache(self.view.itemCell)

    self.m_isConfirmed = false
    self.view.confirmButton.onClick:AddListener(function()
        if self:IsPlayingAnimationIn() then
            return
        end
        if GameInstance.player.squadManager:IsAllDead() and not self.m_isConfirmed then
            GameInstance.gameplayNetwork:SendRevive(true)
            self.m_isConfirmed = true
        end
        PhaseManager:PopPhase(PhaseId.SettlementDefenseFinish, function()
            SettlementDefenseFinishCtrl.s_cachedPhaseArgs = nil
            GameInstance.player.towerDefenseSystem.systemInDefense = false
            Notify(MessageConst.ON_TOWER_DEFENSE_LEVEL_REWARDS_FINISHED)
        end)
    end)

    local rewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(CS.Beyond.GEnums.RewardSourceType.TD)
    local isPassed = args.finishType == CS.Beyond.Gameplay.Core.TowerDefenseGame.FinishType.Complete
    local isFirstPassed = rewardPack ~= nil and isPassed
    self:_RefreshColorAndText(args.tdId)
    self:_RefreshCoreHps(args.coreHpInfoList)
    self:_RefreshItemRewards(rewardPack)
    self.view.rewardList.gameObject:SetActive(isFirstPassed or not isPassed)
    local stateName = ''
    local isAuto = false
    local _, groupCfg = Tables.towerDefenseTable:TryGetValue(args.tdId)
    local inAnimationName
    if isPassed then
        if groupCfg.tdType == GEnums.TowerDefenseLevelType.Auto then
            stateName = 'Auto'
            inAnimationName = 'defensetransit_finish_in_blue'
            isAuto = true
        else
            stateName = 'Normal'
            inAnimationName = 'defensetransit_finish_in'
        end
    else
        stateName = 'Failed'
        inAnimationName = 'defensetransit_finish_in_red'
    end
    self.view.stateController:SetState(stateName)
    self.animationWrapper:Play(inAnimationName)
    AudioAdapter.PostEvent("Au_UI_Menu_SettlementDefenseFinishPanel_Open")
    if isPassed then
        self:_RefreshTips(args.tdId, isFirstPassed, isAuto)
    end
end

SettlementDefenseFinishCtrl.OnClose = HL.Override() << function(self)
    SettlementDefenseFinishCtrl.s_lastUnlockedTdId = ""
end

SettlementDefenseFinishCtrl.OnTowerDefenseLevelFinished = HL.StaticMethod(HL.Any) << function(args)
    if not Utils.isInSettlementDefenseDefending() then
        return
    end

    if SettlementDefenseFinishCtrl.s_cachedPhaseArgs ~= nil then
        return
    end

    local tdId, finishType = unpack(args)
    local coreHpInfoList = {}
    local towerDefenseGame = GameInstance.player.towerDefenseSystem.towerDefenseGame
    if towerDefenseGame ~= nil then
        local tdCoreAbilitySystems = towerDefenseGame.tdCoreAbilitySystems
        if tdCoreAbilitySystems ~= nil and tdCoreAbilitySystems.Count > 0 then
            for index = 0, tdCoreAbilitySystems.Count - 1 do
                local coreAbilitySystem = tdCoreAbilitySystems[index]
                if coreAbilitySystem ~= nil then
                    table.insert(coreHpInfoList, {
                        hp = coreAbilitySystem.hp,
                        maxHp = coreAbilitySystem.maxHp,
                    })
                end
            end
        end
    end

    SettlementDefenseFinishCtrl.s_cachedPhaseArgs = {
        tdId = tdId,
        finishType = finishType,
        coreHpInfoList = coreHpInfoList,
    }
end

SettlementDefenseFinishCtrl.OnTowerDefenseLevelCleared = HL.StaticMethod() << function()
    if SettlementDefenseFinishCtrl.s_cachedPhaseArgs == nil or
        SettlementDefenseFinishCtrl.s_cachedPhaseArgs.finishType ~= CS.Beyond.Gameplay.Core.TowerDefenseGame.FinishType.Complete then
        SettlementDefenseFinishCtrl.s_cachedPhaseArgs = nil
        return
    end
    PhaseManager:OpenPhase(PhaseId.SettlementDefenseFinish, SettlementDefenseFinishCtrl.s_cachedPhaseArgs)
end

SettlementDefenseFinishCtrl.OnTowerDefenseLevelUnlocked = HL.StaticMethod(HL.Any) << function(args)
    local tdId = unpack(args)
    SettlementDefenseFinishCtrl.s_lastUnlockedTdId = tdId
end

SettlementDefenseFinishCtrl._OnLoadingPanelClosed = HL.Method() << function(self)
    if PhaseManager:IsOpen(PhaseId.SettlementDefenseFinish) then
        PhaseManager:ExitPhaseFast(PhaseId.SettlementDefenseFinish)
    end
end

SettlementDefenseFinishCtrl._RefreshColorAndText = HL.Method(HL.String) << function(self, tdId)
    local name = ''
    local _, tdCfg = Tables.towerDefenseTable:TryGetValue(tdId)
    if tdCfg then
        local _, groupCfg = Tables.towerDefenseGroupTable:TryGetValue(tdCfg.tdGroup)
        if groupCfg then
            name = groupCfg.name
        end
    end
    self.view.nameText.text = name
end

SettlementDefenseFinishCtrl._RefreshCoreHps = HL.Method(HL.Table) << function(self, coreHpInfoList)
    local count = coreHpInfoList == nil and 0 or #coreHpInfoList
    self.m_coreHpCells:Refresh(count, function(cell, index)
        local hpInfo = coreHpInfoList[index]
        local amount = hpInfo.hp / hpInfo.maxHp
        cell.indexText.text = string.format("%d", index)
        cell.hpSlider.value = amount
        cell.hpText.text = string.format("%d%%", math.floor(amount * 100))
        local stateName = 'Normal'
        if hpInfo.hp - 0.0 <= ZERO_HP_TOLERANCE then
            stateName = 'Zero'
        elseif amount < self.view.config.HP_RED_THRESHOLD / 100 then
            stateName = 'Low'
        end
        cell.stateController:SetState(stateName)
    end)
end

SettlementDefenseFinishCtrl._RefreshItemRewards = HL.Method(HL.Userdata) << function(self, rewardPack)
    local isEmpty = rewardPack == nil
    self.m_itemCells:Refresh(0)
    if isEmpty then
        return
    end

    self:_StartCoroutine(function()
        coroutine.wait(0.64)
        local rewardItems = rewardPack.itemBundleList
        local rewardItemDataList = UIUtils.convertRewardItemBundlesToDataList(rewardItems, false)
        self.m_itemCells:GraduallyRefresh(rewardItems.Count, 0.02, function(cell, luaIndex)
            local itemData = rewardItemDataList[luaIndex]
            cell:InitItem({
                id = itemData.id,
                count = itemData.count,
            }, true)
            if DeviceInfo.usingController then
                cell:SetExtraInfo({
                    tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                    tipsPosTransform = self.view.rewardList.transform,
                    isSideTips = true,
                })
            end
            AudioAdapter.PostEvent("Au_UI_Popup_RewardsItem_Open")
        end)
    end)
end

SettlementDefenseFinishCtrl._RefreshTips = HL.Method(HL.String, HL.Boolean, HL.Boolean) << function(self, tdId, isFirstPass, isAuto)
    local _, tdCfg = Tables.towerDefenseTable:TryGetValue(tdId)
    if tdCfg == nil then
        return
    end

    local settlementData = GameInstance.player.settlementSystem:GetUnlockSettlementData(tdCfg.settlementId)
    self.view.tipsLayout.autoDefenseNode.gameObject:SetActive(isFirstPass and not isAuto)
    self.view.tipsLayout.safeNode.gameObject:SetActive(isFirstPass)
    if isAuto then
        local showGainBuff = isFirstPass and tdId == settlementData.tdGainEffectByTdId
        self.view.tipsLayout.gainNode.gameObject:SetActive(showGainBuff)
        if showGainBuff then
            local gainEffect = math.floor(settlementData.tdGainEffect)
            self.view.tipsLayout.gainSpeedTxt.text = string.format(Language.LUA_TD_FINISH_GAIN_EFFECT_TIPS_FORMAT, math.floor(gainEffect))
        end
        local defenseState = GameInstance.player.towerDefenseSystem:GetSettlementDefenseState(tdCfg.settlementId)
        self.view.tipsLayout.safeNode.gameObject:SetActive(defenseState == DefenseState.LongSafety)
    else
        local hasGotEffect = settlementData.timeLimitTdGainEffectByTdId == tdId
        self.view.tipsLayout.gainNode.gameObject:SetActive(hasGotEffect)
        self.view.tipsLayout.gainCountDown.view.gameObject:SetActive(hasGotEffect)
        self.view.tipsLayout.safeNode.gameObject:SetActive(hasGotEffect)
        self.view.tipsLayout.safeCountDown.view.gameObject:SetActive(hasGotEffect)
        if hasGotEffect then
            self.view.tipsLayout.gainCountDown:InitCountDownText(settlementData.tdGainEffectExpirationTs, nil, UIUtils.getLeftTimeToSecond)
            self.view.tipsLayout.safeCountDown:InitCountDownText(settlementData.tdGainEffectExpirationTs, nil, UIUtils.getLeftTimeToSecond)
            self.view.tipsLayout.gainSpeedTxt.text = string.format(Language.LUA_TD_FINISH_GAIN_EFFECT_TIPS_FORMAT, math.floor(settlementData.timeLimitTdGainEffect))
        end
    end

    self.view.tipsLayout.unlockNode.gameObject:SetActive(not string.isEmpty(SettlementDefenseFinishCtrl.s_lastUnlockedTdId))
end



SettlementDefenseFinishCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.naviGroup.onIsFocusedChange:AddListener(UIUtils.hideItemTipsOnLoseFocus)
end



HL.Commit(SettlementDefenseFinishCtrl)