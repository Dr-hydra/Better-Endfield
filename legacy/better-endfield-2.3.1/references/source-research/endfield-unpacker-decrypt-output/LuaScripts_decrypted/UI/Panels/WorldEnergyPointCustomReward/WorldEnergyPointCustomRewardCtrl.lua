local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WorldEnergyPointCustomReward
local PHASE_ID = PhaseId.WorldEnergyPointCustomReward

WorldEnergyPointCustomRewardCtrl = HL.Class('WorldEnergyPointCustomRewardCtrl', uiCtrl.UICtrl)

WorldEnergyPointCustomRewardCtrl.m_groupId = HL.Field(HL.String) << ""

WorldEnergyPointCustomRewardCtrl.m_awardGameId = HL.Field(HL.String) << ""

WorldEnergyPointCustomRewardCtrl.m_gemCustomItemId = HL.Field(HL.String) << ""

WorldEnergyPointCustomRewardCtrl.m_hasSelectTerms = HL.Field(HL.Boolean) << false

WorldEnergyPointCustomRewardCtrl.m_hasBpDoubleRewardItemEver = HL.Field(HL.Boolean) << false

WorldEnergyPointCustomRewardCtrl.m_attrInfos = HL.Field(HL.Table)

WorldEnergyPointCustomRewardCtrl.m_curSelectRadio = HL.Field(HL.Number) << -1

WorldEnergyPointCustomRewardCtrl.m_gemCustomToggleOn = HL.Field(HL.Boolean) << false





WorldEnergyPointCustomRewardCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_STAMINA_CHANGED] = 'OnStaminaChanged',
}

WorldEnergyPointCustomRewardCtrl.TryStartSettlement = HL.StaticMethod(HL.Table) << function(args)
    local isReset, groupId, awardGameId = unpack(args)
    if isReset then

        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_WEP_RESET_GAME_GROUP_CONFIRM,
            onConfirm = function()
                GameInstance.player.worldEnergyPointSystem:SendReqAbandonGroupReward(groupId)
            end,
        })
    else

        local hasSelectTerms = GameInstance.player.worldEnergyPointSystem:IsGameGroupHasSelectTerms(groupId)
        local hasBpDoubleRewardEver = GameInstance.player.inventory:IsItemGot(Tables.dungeonConst.doubleStaminaTicketItemId)
        local costStamina = Tables.worldEnergyPointTable[awardGameId].costStamina
        local realCostStamina = ActivityUtils.getRealStaminaCost(costStamina)
        if not hasSelectTerms and not hasBpDoubleRewardEver then

            Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_WEP_AWARD_CONFIRM,
                staminaInfo = {
                    descStamina = Language.LUA_WEP_AWARD_STAMINA_DESC,
                    costStamina = realCostStamina,
                    delStamina = ActivityUtils.hasStaminaReduceCount() and costStamina or nil
                },
                onConfirm = function()
                    if GameInstance.player.inventory.curStamina >= realCostStamina then
                        GameInstance.player.worldEnergyPointSystem:SendReqObtainReward(groupId, false, ActivityUtils.hasStaminaReduceCount(), 1)
                    else

                        PhaseManager:OpenPhase(PhaseId.StaminaPopUp)
                    end

                end,
                confirmText = Language.LUA_WEP_AWARD_BTN_TEXT
            })
        else

            PhaseManager:OpenPhase(PHASE_ID, { groupId, awardGameId })
        end
    end

end


WorldEnergyPointCustomRewardCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnAward.onClick:AddListener(function()
        self:_OnClickBtnAward()
    end)

    self.view.btnCancel.onClick:AddListener(function()
        self:_OnClickBtnCancel()
    end)

    self:_InitData(arg)
    self:_InitView()
    self:_InitController()

    if ActivityUtils.hasStaminaReduceCount() then
        return
    end
    self.view.customRewardRadioComp:SetDefaultTarget()
end

WorldEnergyPointCustomRewardCtrl.OnStaminaChanged = HL.Method() << function(self)
    self:_RefreshState()
end








WorldEnergyPointCustomRewardCtrl._InitData = HL.Method(HL.Table) << function(self, arg)
    local groupId, gameId = unpack(arg)
    self.m_groupId = groupId
    self.m_awardGameId = gameId

    local succ, groupRecord = GameInstance.player.worldEnergyPointSystem:TryGetWorldEnergyPointGroupRecord(groupId)
    local hasSelectTerms = succ and groupRecord.hasSelectTerms
    self.m_hasSelectTerms = hasSelectTerms

    local gemCustomItemId = Tables.worldEnergyPointGroupTable[groupId].gemCustomItemId
    self.m_gemCustomItemId = gemCustomItemId


    local hasBpDoubleRewardItemEver = GameInstance.player.inventory:IsItemGot(Tables.dungeonConst.doubleStaminaTicketItemId)
    self.m_hasBpDoubleRewardItemEver = hasBpDoubleRewardItemEver

    if not hasBpDoubleRewardItemEver or ActivityUtils.hasStaminaReduceCount() then
        self.m_curSelectRadio = 1
    end

    local attrInfos = {}
    if hasSelectTerms then
        local selectTerms = groupRecord.selectTerms
        local primAttr = {}
        local secondPartAttrName
        for i = 0, selectTerms.Count - 1 do
            local selectTermId = selectTerms[i]
            local termCfg = Tables.gemTable[selectTermId]
            if termCfg.termType == GEnums.GemTermType.PrimAttrTerm then
                table.insert(primAttr, {
                    tagName = termCfg.tagName,
                    sortId = termCfg.sortOrder,
                })
            else
                secondPartAttrName = termCfg.tagName
            end
        end
        table.sort(primAttr, Utils.genSortFunction({ "sortId" }))

        if #primAttr == 3 and not string.isEmpty(secondPartAttrName) then
            attrInfos.primAttr = primAttr
            attrInfos.secondPartAttrName = secondPartAttrName
        end
    end
    self.m_attrInfos = attrInfos

    local hasGemCustomItem = Utils.getItemCount(gemCustomItemId) > 0
    local gemCustomizationManuallyOff = GameInstance.player.worldEnergyPointSystem:IsWorldEnergyPointGemCustomizationOff(groupId)
    self.m_gemCustomToggleOn = self.m_hasSelectTerms and hasGemCustomItem and not gemCustomizationManuallyOff
end

WorldEnergyPointCustomRewardCtrl._InitView = HL.Method() << function(self)
    if self.m_hasSelectTerms then
        local primAttr = self.m_attrInfos.primAttr
        self.view.attr1DescTxt.text = string.format(Language.LUA_WEP_GEM_CUSTOM_PRIM_ATTRI_FORMAT,
                                                    primAttr[1].tagName,
                                                    primAttr[2].tagName,
                                                    primAttr[3].tagName)
        self.view.attr2DescTxt.text = self.m_attrInfos.secondPartAttrName

        self.view.orbitToggle:InitCommonToggle(function(isOn)
            self:_OnGemCustomToggleChanged(isOn)
        end, self.m_gemCustomToggleOn, true)
        local gemCustomItemCount = Utils.getItemCount(self.m_gemCustomItemId)
        if gemCustomItemCount > 0 then
            self.view.orbitEntryState:SetState(self.m_gemCustomToggleOn and "Use" or "NonUse")
        else
            self.view.orbitEntryState:SetState("Insufficient")
        end

        local itemCfg = Tables.itemTable[self.m_gemCustomItemId]
        self.view.consumeIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, itemCfg.iconId)
        self.view.orbitIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, itemCfg.iconId)
    end
    self.view.orbitEntryState.gameObject:SetActive(self.m_hasSelectTerms)

    if self.m_hasBpDoubleRewardItemEver then
        self.view.customRewardRadioComp:InitCustomRewardRadioComp(Tables.worldEnergyPointTable[self.m_awardGameId].costStamina, function(radioIndex)
            self:_OnRewardRadioChanged(radioIndex)
        end)
    end
    self.view.customRewardRadioComp.gameObject:SetActive(self.m_hasBpDoubleRewardItemEver)

    local ids = { Tables.dungeonConst.staminaItemId}
    local cellPreferredWidths = {}
    local doubleTicket = Tables.dungeonConst.doubleStaminaTicketItemId
    local hasGotDoubleTicket = self.m_hasBpDoubleRewardItemEver
    if hasGotDoubleTicket then
        table.insert(ids, 1, doubleTicket)
        cellPreferredWidths[doubleTicket] = self.view.config.MONEY_CELL_PREFERRED_WIDTH
    end

    if self.m_hasSelectTerms then
        table.insert(ids, 1, self.m_gemCustomItemId)
        cellPreferredWidths[self.m_gemCustomItemId] = self.view.config.MONEY_CELL_PREFERRED_WIDTH
    end
    self.view.walletBarPlaceholder:InitWalletBarPlaceholder(ids, false, false, false, cellPreferredWidths)

    self:_RefreshState()
end

WorldEnergyPointCustomRewardCtrl._OnClickBtnAward = HL.Method() << function(self)
    local costStamina = self.m_curSelectRadio * Tables.worldEnergyPointTable[self.m_awardGameId].costStamina
    local realCost = ActivityUtils.getRealStaminaCost(costStamina)
    if GameInstance.player.inventory.curStamina >= realCost then
        PhaseManager:ExitPhaseFast(PHASE_ID)
        GameInstance.player.worldEnergyPointSystem:SendReqObtainReward(self.m_groupId, self.m_gemCustomToggleOn, ActivityUtils.hasStaminaReduceCount(), self.m_curSelectRadio)
    else

        PhaseManager:OpenPhase(PhaseId.StaminaPopUp)
    end
end

WorldEnergyPointCustomRewardCtrl._RefreshState = HL.Method() << function(self)
    local hasSelectRadio = self.m_curSelectRadio > 0
    local showConsume = hasSelectRadio

    self.view.btnAward.gameObject:SetActive(showConsume)
    self.view.consumeItemNode.gameObject:SetActive(showConsume)
    self.view.unselectedNode.gameObject:SetActive(not showConsume)
    self.view.selectRewardTips.gameObject:SetActive(not showConsume)

    local showConsumeOrbit = self.m_hasSelectTerms and self.m_gemCustomToggleOn


    local consumeGemCustomCount = self.m_curSelectRadio * 1
    local ownGemCustomCount = Utils.getItemCount(Tables.worldEnergyPointGroupTable[self.m_groupId].gemCustomItemId)
    local gemCustomLack = ownGemCustomCount < consumeGemCustomCount
    self.view.orbitLackTips.gameObject:SetActive(showConsumeOrbit and gemCustomLack)
    self.view.orbitNumberTxt.text = gemCustomLack and 1 or consumeGemCustomCount

    local activityInfo = ActivityUtils.getStaminaReduceInfo()
    local isStaminaActivityUsable = activityInfo.activityUsable
    local gameCostStamina = Tables.worldEnergyPointTable[self.m_awardGameId].costStamina



    self.view.multiplesCouponNumberTxt.text = math.ceil(gameCostStamina / Tables.dungeonConst.staminaPerDoubleStaminaTicket) *
            (self.m_curSelectRadio - 1)

    self.view.breakNumberTxt.gameObject:SetActive(isStaminaActivityUsable)

    self.view.breakNumberTxt.text = gameCostStamina


    local costStamina = gameCostStamina * self.m_curSelectRadio
    local realCost = isStaminaActivityUsable and math.max(0, costStamina - activityInfo.disCount) or costStamina
    self.view.strengthNumberTxt.text = UIUtils.setCountColor(realCost, realCost > GameInstance.player.inventory.curStamina)

    self.view.consumeOrbit.gameObject:SetActive(showConsumeOrbit)
    self.view.consumeMultiplesCoupon.gameObject:SetActive(self.m_curSelectRadio > 1)
    self.view.consumeStrength.gameObject:SetActive(showConsume)
end

WorldEnergyPointCustomRewardCtrl._OnClickBtnCancel = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end

WorldEnergyPointCustomRewardCtrl._OnRewardRadioChanged = HL.Method(HL.Number) << function(self, radioIndex)
    self.m_curSelectRadio = radioIndex
    self:_RefreshState()
end

WorldEnergyPointCustomRewardCtrl._OnGemCustomToggleChanged = HL.Method(HL.Boolean) << function(self, isOn)
    self.m_gemCustomToggleOn = isOn
    self.view.orbitEntryState:SetState(self.m_gemCustomToggleOn and "Use" or "NonUse")
    self:_RefreshState()

    if isOn then
        GameInstance.player.worldEnergyPointSystem:SendReqRemoveGemCustomizationOff(self.m_groupId)
    else
        GameInstance.player.worldEnergyPointSystem:SendReqAddGemCustomizationOff(self.m_groupId)
    end
end

WorldEnergyPointCustomRewardCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

HL.Commit(WorldEnergyPointCustomRewardCtrl)
