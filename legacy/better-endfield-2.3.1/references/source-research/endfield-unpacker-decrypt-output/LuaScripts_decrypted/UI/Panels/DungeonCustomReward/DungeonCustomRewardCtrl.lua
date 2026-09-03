
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DungeonCustomReward
local PHASE_ID = PhaseId.DungeonCustomReward
DungeonCustomRewardCtrl = HL.Class('DungeonCustomRewardCtrl', uiCtrl.UICtrl)

DungeonCustomRewardCtrl.m_dungeonId = HL.Field(HL.String) << ""

DungeonCustomRewardCtrl.m_costStamina = HL.Field(HL.Number) << -1

DungeonCustomRewardCtrl.m_curSelectRadio = HL.Field(HL.Number) << -1





DungeonCustomRewardCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_STAMINA_CHANGED] = 'OnStaminaChanged',
}

DungeonCustomRewardCtrl.TryStartSettlement = HL.StaticMethod() << function()
    local dungeonMgr = GameInstance.dungeonManager
    local dungeonId = dungeonMgr.curDungeonId
    local isCostStamina, costStamina = DungeonUtils.isDungeonCostStamina(dungeonId)

    local success = Tables.activityCharTrial:ContainsKey(dungeonId)
    if success then
        dungeonMgr:LeaveDungeon()
        return
    end

    if isCostStamina then
        local hasBpDoubleRewardEver = GameInstance.player.inventory:IsItemGot(Tables.dungeonConst.doubleStaminaTicketItemId)
        if hasBpDoubleRewardEver then

            PhaseManager:OpenPhase(PHASE_ID, {
                dungeonId = dungeonId,
                costStamina = costStamina,
            })
        else
            local dungeonCfg = Tables.dungeonTable[dungeonId]
            local hasFirstPassReward = not string.isEmpty(dungeonCfg.firstPassRewardId)
            local firstPassRewardGained = dungeonMgr:IsDungeonFirstPassRewardGained(dungeonId)
            local canGetFirstPassReward = hasFirstPassReward and not firstPassRewardGained
            local isHunterMode = DungeonUtils.isDungeonHasHunterMode(dungeonId)

            local realCost = ActivityUtils.getRealStaminaCost(costStamina)
            Notify(MessageConst.SHOW_POP_UP, {
                staminaInfo = {
                    descStamina = isHunterMode and Language.LUA_DUNGEON_OBTAIN_REWARD_COST_STAMINA_HUNTER_MODE_DESC or Language.LUA_DUNGEON_OBTAIN_REWARD_COST_STAMINA_RESOURCE_DESC,
                    costStamina = realCost,
                    delStamina = ActivityUtils.hasStaminaReduceCount() and costStamina or nil
                },
                content = canGetFirstPassReward and
                        string.format("%s\n%s", Language.LUA_DUNGEON_OBTAIN_REWARD_COST_STAMINA_CONFIRM_HINT, Language.LUA_DUNGEON_OBTAIN_REWARD_COST_STAMINA_CONFIRM_ADDITIVE_HINT) or
                        Language.LUA_DUNGEON_OBTAIN_REWARD_COST_STAMINA_CONFIRM_HINT,
                onConfirm = function()

                    if ActivityUtils.getRealStaminaCost(costStamina) > GameInstance.player.inventory.curStamina then
                        PhaseManager:OpenPhase(PhaseId.StaminaPopUp)
                    else
                        dungeonMgr:TryObtainReward(dungeonId, false, ActivityUtils.hasStaminaReduceCount(), 1)
                    end
                end,
                onCancel = function()
                    if canGetFirstPassReward then
                        dungeonMgr:TryObtainReward(dungeonId, true)
                    end
                end
            })
        end
    else
        dungeonMgr:TryObtainReward(dungeonId)
    end
end


DungeonCustomRewardCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnAward.onClick:AddListener(function()
        self:_OnClickBtnAward()
    end)

    self.view.btnCancel.onClick:AddListener(function()
        self:_OnClickBtnCancel()
    end)

    self:_InitData(arg)
    self:_InitView()
    self:_InitController()
end

DungeonCustomRewardCtrl.OnAnimationInFinished = HL.Override() << function(self)
    if ActivityUtils.hasStaminaReduceCount() then
        return
    end
    self.view.customRewardRadioComp:SetDefaultTarget()
end

DungeonCustomRewardCtrl.OnStaminaChanged = HL.Method() << function(self)
    self:_RefreshState()
end








DungeonCustomRewardCtrl._InitData = HL.Method(HL.Table) << function(self, args)
    self.m_dungeonId = args.dungeonId
    self.m_costStamina = args.costStamina

    local hasBpDoubleRewardItemEver = GameInstance.player.inventory:IsItemGot(Tables.dungeonConst.doubleStaminaTicketItemId)

    if not hasBpDoubleRewardItemEver or ActivityUtils.hasStaminaReduceCount() then
        self.m_curSelectRadio = 1
    end
end

DungeonCustomRewardCtrl._InitView = HL.Method() << function(self)
    local ids = { Tables.dungeonConst.staminaItemId }
    local doubleTicket = Tables.dungeonConst.doubleStaminaTicketItemId
    local hasGotDoubleTicket = GameInstance.player.inventory:IsItemGot(doubleTicket)
    if hasGotDoubleTicket then
        table.insert(ids, 1, doubleTicket)
    end
    self.view.walletBarPlaceholder:InitWalletBarPlaceholder(ids)

    local valid = self.m_costStamina > 0
    if not valid then
        logger.error("DungeonCustomRewardCtrl._InitView failed, costStamina invalid", self.m_costStamina, self.m_dungeonId)
        return
    end
    self.view.customRewardRadioComp:InitCustomRewardRadioComp(self.m_costStamina, function(radioIndex)
        self:_OnRewardRadioChanged(radioIndex)
    end)

    local dungeonCfg = Tables.dungeonTable[self.m_dungeonId]
    local hasFirstPassReward = not string.isEmpty(dungeonCfg.firstPassRewardId)
    local firstPassRewardGained = GameInstance.dungeonManager:IsDungeonFirstPassRewardGained(self.m_dungeonId)
    local canGetFirstPassReward = hasFirstPassReward and not firstPassRewardGained
    self.view.cancelTips.gameObject:SetActive(canGetFirstPassReward)

    self:_RefreshState()
end

DungeonCustomRewardCtrl._OnClickBtnAward = HL.Method() << function(self)
    local realCostStamina = ActivityUtils.getRealStaminaCost(self.m_curSelectRadio * self.m_costStamina)
    if GameInstance.player.inventory.curStamina >= realCostStamina then
        PhaseManager:ExitPhaseFast(PHASE_ID)
        GameInstance.dungeonManager:TryObtainReward(self.m_dungeonId, false, ActivityUtils.hasStaminaReduceCount(), self.m_curSelectRadio)
    else

        PhaseManager:OpenPhase(PhaseId.StaminaPopUp)
    end
end

DungeonCustomRewardCtrl._OnClickBtnCancel = HL.Method() << function(self)
    PhaseManager:ExitPhaseFast(PHASE_ID)

    local dungeonId = self.m_dungeonId
    local dungeonCfg = Tables.dungeonTable[dungeonId]
    local hasFirstPassReward = not string.isEmpty(dungeonCfg.firstPassRewardId)
    local firstPassRewardGained = GameInstance.dungeonManager:IsDungeonFirstPassRewardGained(dungeonId)
    local canGetFirstPassReward = hasFirstPassReward and not firstPassRewardGained

    if canGetFirstPassReward then
        GameInstance.dungeonManager:TryObtainReward(self.m_dungeonId, true)
    end
end

DungeonCustomRewardCtrl._OnRewardRadioChanged = HL.Method(HL.Number) << function(self, radioIndex)
    self.m_curSelectRadio = radioIndex
    self:_RefreshState()
end

DungeonCustomRewardCtrl._RefreshState = HL.Method() <<function(self)
    local hasSelectRadio = self.m_curSelectRadio > 0
    local showConsume = hasSelectRadio

    self.view.consumeItemNode.gameObject:SetActive(showConsume)
    self.view.btnAward.gameObject:SetActive(showConsume)
    self.view.unselectedNode.gameObject:SetActive(not showConsume)
    self.view.selectRewardTips.gameObject:SetActive(not showConsume)

    local activityInfo = ActivityUtils.getStaminaReduceInfo()
    local isStaminaActivityOn = activityInfo.activityUsable
    local gameCostStamina = self.m_costStamina



    self.view.multiplesCouponNumberTxt.text = math.ceil(gameCostStamina / Tables.dungeonConst.staminaPerDoubleStaminaTicket) *
            (self.m_curSelectRadio - 1)

    self.view.breakNumberTxt.gameObject:SetActive(isStaminaActivityOn)

    self.view.breakNumberTxt.text = gameCostStamina


    local costStamina = gameCostStamina * self.m_curSelectRadio
    local realCost = isStaminaActivityOn and math.max(0, costStamina - activityInfo.disCount) or costStamina
    self.view.strengthNumberTxt.text = UIUtils.setCountColor(realCost, realCost > GameInstance.player.inventory.curStamina)

    self.view.consumeMultiplesCoupon.gameObject:SetActive(self.m_curSelectRadio > 1)
end

DungeonCustomRewardCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

HL.Commit(DungeonCustomRewardCtrl)
