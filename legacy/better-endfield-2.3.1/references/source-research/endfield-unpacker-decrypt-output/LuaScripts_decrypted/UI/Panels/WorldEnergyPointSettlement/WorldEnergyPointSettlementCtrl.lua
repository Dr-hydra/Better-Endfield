
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WorldEnergyPointSettlement
local PHASE_ID = PhaseId.WorldEnergyPointSettlement

local SERIALIZED_CATEGORY = "WEP"
local WEP_STAMINA_LACK_START_CONFIRM_HINT_KEY = "wep_stamina_lack_start_confirm_hint"
local WEP_GEM_CUSTOM_ITEM_LACK_CONFIRM_HINT_KEY = "wep_gem_custom_item_lack_confirm_hint"

WorldEnergyPointSettlementCtrl = HL.Class('WorldEnergyPointSettlementCtrl', uiCtrl.UICtrl)

WorldEnergyPointSettlementCtrl.m_curLevelGameId = HL.Field(HL.String) << ""

WorldEnergyPointSettlementCtrl.m_gameGroupId = HL.Field(HL.String) << ""

WorldEnergyPointSettlementCtrl.m_entityLid = HL.Field(HL.Number) << -1

WorldEnergyPointSettlementCtrl.m_rewardInfos = HL.Field(HL.Table)

WorldEnergyPointSettlementCtrl.m_genRewardCellFunc = HL.Field(HL.Function)






WorldEnergyPointSettlementCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_STAMINA_CHANGED] = 'OnStaminaChanged',
}
WorldEnergyPointSettlementCtrl.OnShowWorldEnergyPointResult = HL.StaticMethod(HL.Table) << function(args)
    local rewardMultiplier, useStaminaReduce, curLevelGameId, entityLid = unpack(args)
    PhaseManager:OpenPhase(PHASE_ID, args)

    if useStaminaReduce then

        ActivityUtils.showStaminaReduceProgress()
    end
end


WorldEnergyPointSettlementCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_genRewardCellFunc = UIUtils.genCachedCellFunction(self.view.rewardsScrollList)

    self.view.rewardsScrollList.onUpdateCell:AddListener(function(go, csIndex)
        self:_OnUpdateCell(go, csIndex)
    end)

    self.view.rewardsScrollList.onGraduallyShowFinish:AddListener(function()
        self:_OnGraduallyShowFinish()
    end)

    self.view.btnRestart.onClick:AddListener(function()
        self:_OnClickBtnRestart()
    end)

    self.view.btnEnd.onClick:AddListener(function()
        self:_OnClickBtnEnd()
    end)

    self:_InitData(arg)
    self:_InitView()

    self:_InitController()
end








WorldEnergyPointSettlementCtrl.OnStaminaChanged = HL.Method() << function(self)
    self:_RefreshCostStamina()
end

WorldEnergyPointSettlementCtrl._OnUpdateCell = HL.Method(GameObject, HL.Number) << function(self, go, csIndex)
    local rewardInfo = self.m_rewardInfos[LuaIndex(csIndex)]

    local cell = self.m_genRewardCellFunc(go)
    cell:InitItem(rewardInfo, true)
    cell:SetExtraInfo({isSideTips = DeviceInfo.usingController})

    local _, itemData = Tables.itemTable:TryGetValue(rewardInfo.id)
    local isGem = itemData and itemData.type == GEnums.ItemType.WeaponGem
    if isGem and rewardInfo.instId and rewardInfo.instId > 0 then
        local isPerfectMatch = UIUtils.getGemWishListPerfectMatch(rewardInfo.instId)
        cell:ShowGemPerfectIcon(isPerfectMatch)
    end

    go.name = rewardInfo.id
end

WorldEnergyPointSettlementCtrl._OnGraduallyShowFinish = HL.Method() << function(self)
    if DeviceInfo.usingController then
        self.view.controllerHintPlaceholder.gameObject:SetActive(true)
        self.view.focusItemKeyHint.gameObject:SetActive(true)
        local firstItemGo = self.view.rewardsScrollList:Get(0)
        if firstItemGo then
            self.view.focusItemKeyHint.transform.position = firstItemGo.transform.position
            local keyHintPos = self.view.focusItemKeyHint.transform.localPosition
            keyHintPos = keyHintPos + self.view.config.FOCUS_REWARDS_OFFSET
            self.view.focusItemKeyHint.transform.localPosition = keyHintPos
        end
    end
end

WorldEnergyPointSettlementCtrl._OnClickBtnRestart = HL.Method() << function(self)
    local curIsFull = GameInstance.player.worldEnergyPointSystem.isFull
    if curIsFull then

        local wepGameCfg = Tables.worldEnergyPointTable[self.m_curLevelGameId]
        local curStamina = GameInstance.player.inventory.curStamina
        if curStamina >= ActivityUtils.getRealStaminaCost(wepGameCfg.costStamina) then

            local succ, groupRecord = GameInstance.player.worldEnergyPointSystem:TryGetWorldEnergyPointGroupRecord(self.m_gameGroupId)
            local groupCfg = Tables.worldEnergyPointGroupTable[self.m_gameGroupId]
            local hasGemCustomItem = Utils.getItemCount(groupCfg.gemCustomItemId) > 0
            local hasSelectTerms = groupRecord.hasSelectTerms
            if hasSelectTerms and not hasGemCustomItem then
                local succ, ignoreHint = ClientDataManagerInst:GetBool(WEP_GEM_CUSTOM_ITEM_LACK_CONFIRM_HINT_KEY, false, false, SERIALIZED_CATEGORY)
                if ignoreHint then
                    self:_TryStartWorldEnergyPoint()
                else
                    local closuresIsOn = false
                    Notify(MessageConst.SHOW_POP_UP, {
                        toggle = {
                            onValueChanged = function(isOn)
                                closuresIsOn = isOn
                            end,
                            toggleText = Language.LUA_WEP_NO_HINT_TODAY_HINT,
                            isOn = false,
                        },
                        content = Language.LUA_WEP_ONCE_AGAIN_BUT_CUSTOM_ON_WITHOUT_CUSTOM_ITEM_HINT,
                        onConfirm = function()
                            ClientDataManagerInst:SetBool(WEP_GEM_CUSTOM_ITEM_LACK_CONFIRM_HINT_KEY, closuresIsOn, false,
                                                          SERIALIZED_CATEGORY,
                                                          EClientDataTimeValidType.CurrentDay)
                            self:_TryStartWorldEnergyPoint()
                        end,
                    })
                end
            else
                self:_TryStartWorldEnergyPoint()
            end
        else

            local succ, ignoreHint = ClientDataManagerInst:GetBool(WEP_STAMINA_LACK_START_CONFIRM_HINT_KEY, false, false, SERIALIZED_CATEGORY)
            if ignoreHint then
                self:_TryStartWorldEnergyPoint()
            else
                local closuresIsOn = false
                Notify(MessageConst.SHOW_POP_UP, {
                    toggle = {
                        onValueChanged = function(isOn)
                            closuresIsOn = isOn
                        end,
                        toggleText = Language.LUA_WEP_NO_HINT_TODAY_HINT,
                        isOn = false,
                    },
                    content = Language.LUA_WEP_ONCE_AGAIN_BUT_NOT_ENOUGH_STAMINA_HINT,
                    onConfirm = function()
                        ClientDataManagerInst:SetBool(WEP_STAMINA_LACK_START_CONFIRM_HINT_KEY, closuresIsOn, false,
                                                      SERIALIZED_CATEGORY,
                                                      EClientDataTimeValidType.CurrentDay)
                        self:_TryStartWorldEnergyPoint()
                    end,
                })
            end
        end
    else
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_WEP_ONCE_AGAIN_BUT_NOT_FULL_HINT,
            onConfirm = function()
                self:_TryStartWorldEnergyPoint()
            end,
        })
    end
end

WorldEnergyPointSettlementCtrl._TryStartWorldEnergyPoint = HL.Method() << function(self)
    GameInstance.player.worldEnergyPointSystem:SendReqStartWorldEnergyPoint(self.m_curLevelGameId, self.m_entityLid)
    PhaseManager:PopPhase(PHASE_ID)
end

WorldEnergyPointSettlementCtrl._OnClickBtnEnd = HL.Method() <<function(self)
    PhaseManager:PopPhase(PHASE_ID)
end

WorldEnergyPointSettlementCtrl._InitData = HL.Method(HL.Table) << function(self, args)
    local rewardMultiplier, useStaminaReduce, curLevelGameId, entityLid = unpack(args)
    self.m_curLevelGameId = curLevelGameId
    self.m_entityLid = entityLid
    local gameCfg = Tables.worldEnergyPointTable[curLevelGameId]
    self.m_gameGroupId = gameCfg.gameGroupId
end

WorldEnergyPointSettlementCtrl._InitView = HL.Method() << function(self)
    local sourceType = CS.Beyond.GEnums.RewardSourceType.EnergyPoint
    local rewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(sourceType)
    if rewardPack and rewardPack.rewardSourceType == sourceType then
        local rewardInfos = {}
        local count = 0
        for _, itemBundle in pairs(rewardPack.itemBundleList) do
            local _, itemData = Tables.itemTable:TryGetValue(itemBundle.id)
            if itemData then
                table.insert(rewardInfos, { id = itemBundle.id,
                                            count = itemBundle.count,
                                            instId = itemBundle.instId,
                                            sortId1 = itemData.sortId1,
                                            sortId2 = itemData.sortId2 })
            end
        end
        table.sort(rewardInfos, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
        count = #rewardInfos
        self.m_rewardInfos = rewardInfos
        self.view.rewardsScrollList:UpdateCount(count, true)
    end

    local ids = { Tables.dungeonConst.staminaItemId}
    local cellPreferredWidths = {}
    local doubleTicket = Tables.dungeonConst.doubleStaminaTicketItemId
    local hasGotDoubleTicket = GameInstance.player.inventory:IsItemGot(doubleTicket)
    if hasGotDoubleTicket then
        table.insert(ids, 1, doubleTicket)
        cellPreferredWidths[doubleTicket] = self.view.config.MONEY_CELL_PREFERRED_WIDTH
    end

    if GameInstance.player.worldEnergyPointSystem:IsGameGroupHasSelectTerms(self.m_gameGroupId) then
        local gemCustomItemId = Tables.worldEnergyPointGroupTable[self.m_gameGroupId].gemCustomItemId
        table.insert(ids, 1, gemCustomItemId)
        cellPreferredWidths[gemCustomItemId] = self.view.config.MONEY_CELL_PREFERRED_WIDTH
    end
    self.view.walletBarPlaceholder:InitWalletBarPlaceholder(ids, false, false, false, cellPreferredWidths)

    local showStaminaNode = GameInstance.player.worldEnergyPointSystem.isFull
    if showStaminaNode then
        self:_RefreshCostStamina()
    end
    self.view.staminaNode.gameObject:SetActiveIfNecessary(showStaminaNode)
end

WorldEnergyPointSettlementCtrl._RefreshCostStamina = HL.Method() << function(self)
    local costStamina = Tables.worldEnergyPointTable[self.m_curLevelGameId].costStamina
    UIUtils.updateStaminaNode(self.view.staminaNode, {
        costStamina = ActivityUtils.getRealStaminaCost(costStamina),
        descStamina = Language["ui_dungeon_details_ap_refresh"],
        delStamina = ActivityUtils.hasStaminaReduceCount() and costStamina or nil
    })
end

WorldEnergyPointSettlementCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.view.focusItemKeyHint.gameObject:SetActive(false)
    self.view.controllerHintPlaceholder.gameObject:SetActive(false)
end

HL.Commit(WorldEnergyPointSettlementCtrl)
