local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WorldEnergyPointEntry
local PHASE_ID = PhaseId.WorldEnergyPointEntry

local WORLD_ENERGY_POINT_WEAK_INSTRUCTION_ID = "world_energy_point_weak"
local SERIALIZED_CATEGORY = "WEP"
local WEP_STAMINA_LACK_START_CONFIRM_HINT_KEY = "wep_stamina_lack_start_confirm_hint"
local WEP_GEM_CUSTOM_ITEM_LACK_CONFIRM_HINT_KEY = "wep_gem_custom_item_lack_confirm_hint"
local WEP_NOT_GEM_CUSTOM_CONFIRM_HINT_KEY = "wep_not_gem_custom_confirm_hint"

WorldEnergyPointEntryCtrl = HL.Class('WorldEnergyPointEntryCtrl', uiCtrl.UICtrl)

WorldEnergyPointEntryCtrl.m_rewardCellCache = HL.Field(HL.Forward("UIListCache"))

WorldEnergyPointEntryCtrl.m_gameGroupId = HL.Field(HL.String) << ""

WorldEnergyPointEntryCtrl.m_gameId = HL.Field(HL.String) << ""

WorldEnergyPointEntryCtrl.m_entityLid = HL.Field(HL.Number) << -1

WorldEnergyPointEntryCtrl.m_isFull = HL.Field(HL.Boolean) << false





WorldEnergyPointEntryCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_WORLD_ENERGY_POINT_SELECT_TERMS_CHANGED] = 'OnWEPSelectTermsChanged',
    [MessageConst.ON_STAMINA_CHANGED] = 'OnStaminaChanged',
}


WorldEnergyPointEntryCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:_OnClickBtnClose()
    end)

    self.view.gemOverviewBtn.onClick:AddListener(function()
        self:_OnClickGemOverviewBtn()
    end)

    self.view.btnEnemyDetails.onClick:AddListener(function()
        self:_OnClickEnemyInfoBtn()
    end)

    self.view.btnRewardDetails.onClick:AddListener(function()
        self:_OnClickBtnRewardDetails()
    end)

    self.view.gemCustomBtn.onClick:AddListener(function()
        self:_OnClickGemCustomBtn()
    end)

    self.view.btnGemWishlist.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.GemWishlist)
    end)

    self.view.weakNode.onClick:AddListener(function()
        self:_OnClickWeakInfoBtn()
    end)
    self.view.weakTipsBtn.onClick:AddListener(function()
        self:_OnClickWeakInfoBtn()
    end)

    self.view.btnGameStart.onClick:AddListener(function()
        self:_OnClickBtnGameStartBtn()
    end)

    self.m_rewardCellCache = UIUtils.genCellCache(self.view.rewardCell)

    self:_InitData(arg)
    self:_InitView()


    self:TryRecoverPopupState(arg and arg.popupState)
    if arg then
        arg.popupState = nil
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end








WorldEnergyPointEntryCtrl._InitData = HL.Method(HL.Table) << function(self, arg)
    local gameGroupId, _, entityLid = unpack(arg)
    self.m_gameGroupId = gameGroupId
    self.m_entityLid = entityLid
    self.m_gameId = GameInstance.player.worldEnergyPointSystem:GetCurSubGameId(gameGroupId)
    self.m_isFull = GameInstance.player.worldEnergyPointSystem.isFull
end

WorldEnergyPointEntryCtrl._InitView = HL.Method() << function(self)
    self:_InitBasicView()


    self:_RefreshRewards()


    self:_RefreshSelectTerms()


    self:_RefreshCostStaminaNode()
end

WorldEnergyPointEntryCtrl._InitBasicView = HL.Method() << function(self)
    local worldEnergyPointCfg = Tables.worldEnergyPointTable[self.m_gameId]
    local wepGroupCfg = Tables.worldEnergyPointGroupTable[self.m_gameGroupId]


    self.view.gemPreNode.gameObject:SetActive(self.m_isFull)


    self.view.btnGemWishlist.gameObject:SetActive(PhaseManager:IsPhaseUnlocked(PhaseId.GemWishlist))


    self.view.worldEnergyPointTitleTxt.text = worldEnergyPointCfg.gameName

    self.view.locationTxt.text = DungeonUtils.getEntryLocation(worldEnergyPointCfg.levelId, false)

    self.view.recommendLvTxt.text = string.format(Language.LUA_WEP_RECOMMEND_LV_FORMAT, worldEnergyPointCfg.recommendLv)

    self.view.descTxt.text = worldEnergyPointCfg.desc


    if self.m_isFull then
        local doubleTicket = Tables.dungeonConst.doubleStaminaTicketItemId
        local hasGot = GameInstance.player.inventory:IsItemGot(doubleTicket)

        local ids = { wepGroupCfg.gemCustomItemId, Tables.dungeonConst.staminaItemId }
        local cellPreferredWidths = {}
        cellPreferredWidths[wepGroupCfg.gemCustomItemId] = self.view.config.MONEY_CELL_PREFERRED_WIDTH

        if hasGot then
            table.insert(ids, 2, doubleTicket)
            cellPreferredWidths[doubleTicket] = self.view.config.MONEY_CELL_PREFERRED_WIDTH
        end
        self.view.walletBarPlaceholder:InitWalletBarPlaceholder(ids, false, false, false, cellPreferredWidths)
    end

    local succ, wepGroupRecord = GameInstance.player.worldEnergyPointSystem:TryGetWorldEnergyPointGroupRecord(self.m_gameGroupId)
    self.view.weakNode.gameObject:SetActive(succ and wepGroupRecord.isWeak)
end


WorldEnergyPointEntryCtrl._RefreshRewards = HL.Method() << function(self)
    local rewards = {}
    local wepGroupCfg = Tables.worldEnergyPointGroupTable[self.m_gameGroupId]
    local wepCfg = Tables.worldEnergyPointTable[self.m_gameId]


    local firstRewardGained = GameInstance.player.worldEnergyPointSystem:IsGameGroupFirstPassRewardGained(self.m_gameGroupId)
    local rewardCfg = Tables.rewardTable[wepGroupCfg.firstPassRewardId]
    for _, itemBundle in pairs(rewardCfg.itemBundles) do
        local reward = self:_GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.First, -1, -1, firstRewardGained,
                                           itemBundle.id, itemBundle.count)
        table.insert(rewards, reward)
    end

    if self.m_isFull then

        for i = 0, wepCfg.regularItemIds.Count - 1 do
            local itemId = wepCfg.regularItemIds[i]
            local itemCount = i < wepCfg.regularItemCount.Count and wepCfg.regularItemCount[i] or 0
            local reward = self:_GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.Regular, -2, -2, false, itemId, itemCount)
            table.insert(rewards, reward)
        end


        local probGemItems = wepCfg.probGemItemIds
        for i = 0, probGemItems.Count - 1 do
            local itemId = probGemItems[i]
            local reward = self:_GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.Random, -2, -3, false, itemId)
            table.insert(rewards, reward)
        end
    end

    local sortKeys = UIConst.COMMON_ITEM_SORT_KEYS
    table.insert(sortKeys, 1, "rewardTypeSortId")
    table.insert(sortKeys, 1, "gainedSortId")
    table.sort(rewards, Utils.genSortFunction(sortKeys))

    local groupId
    self.m_rewardCellCache:Refresh(#rewards, function(cell, luaIndex)
        local reward = rewards[luaIndex]
        cell.itemSmall:InitItem(reward, true)
        cell.getNode.gameObject:SetActive(reward.gained == true)
        cell.lockNode.gameObject:SetActive(reward.locked == true)
        cell.lineNode.gameObject:SetActive(reward.groupId ~= groupId)
        groupId = reward.groupId

        cell.extraTag.gameObject:SetActive(reward.gained ~= true)
        cell.extraTag:SetState(reward.tagState)
    end)
    self.view.rewardNode.gameObject:SetActive(#rewards > 0)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.container)
    self.view.rewardList.normalizedPosition = Vector2(0, 0)
end

WorldEnergyPointEntryCtrl._GenRewardInfo = HL.Method(HL.String, HL.Number, HL.Number, HL.Boolean, HL.String, HL.Opt(HL.Number)).Return(HL.Table)
        << function(self, tagState, groupId, rewardTypeSortId, gained, itemId, itemCount)
    local itemCfg = Tables.itemTable[itemId]
    return {
        id = itemId,
        count = itemCount,
        gained = gained,
        tagState = tagState,
        groupId = groupId,

        gainedSortId = gained and 0 or 1,
        rewardTypeSortId = rewardTypeSortId,
        sortId1 = itemCfg.sortId1,
        sortId2 = itemCfg.sortId2,
    }
end

WorldEnergyPointEntryCtrl._RefreshSelectTerms = HL.Method() << function(self)
    self.view.gemCustomNode.gameObject:SetActive(self.m_isFull)
    self.view.gemCustomLockNode.gameObject:SetActive(not self.m_isFull)

    if self.m_isFull then
        local succ, wepGroupRecord = GameInstance.player.worldEnergyPointSystem:TryGetWorldEnergyPointGroupRecord(self.m_gameGroupId)
        local selectTerms = succ and wepGroupRecord.selectTerms
        local hasSelectTerms = succ and selectTerms.Count > 0

        self.view.nonCustomNode.gameObject:SetActive(not hasSelectTerms)
        self.view.customResultNode.gameObject:SetActive(hasSelectTerms)

        if hasSelectTerms then
            local primAtrri = {}
            local secondPartAtrriName
            for i = 0 , selectTerms.Count - 1 do
                local selectTermId = selectTerms[i]
                local termCfg = Tables.gemTable[selectTermId]
                if termCfg.termType == GEnums.GemTermType.PrimAttrTerm then
                    table.insert(primAtrri, {
                        tagName = termCfg.tagName,
                        sortId = termCfg.sortOrder,
                    })
                else
                    secondPartAtrriName = termCfg.tagName
                end
            end
            table.sort(primAtrri, Utils.genSortFunction({ "sortId" }))

            if #primAtrri == 3 and not string.isEmpty(secondPartAtrriName) then
                self.view.attri1Txt.text = string.format(Language.LUA_WEP_GEM_CUSTOM_PRIM_ATTRI_FORMAT,
                                                         primAtrri[1].tagName,
                                                         primAtrri[2].tagName,
                                                         primAtrri[3].tagName)
                self.view.attri2Txt.text = secondPartAtrriName
            end
        end
    end
end

WorldEnergyPointEntryCtrl._RefreshCostStaminaNode = HL.Method() << function(self)

    local worldEnergyPointCfg = Tables.worldEnergyPointTable[self.m_gameId]
    local activityInfo = ActivityUtils.getStaminaReduceInfo()
    local canReduceStamina = ActivityUtils.hasStaminaReduceCount()

    UIUtils.updateStaminaNode(self.view.staminaNode, {
        costStamina = ActivityUtils.getRealStaminaCost(worldEnergyPointCfg.costStamina),
        descStamina = Language["ui_dungeon_details_ap_reuse"],
        delStamina = canReduceStamina and worldEnergyPointCfg.costStamina or nil
    })
    self.view.staminaNode.gameObject:SetActive(self.m_isFull)


    self.view.laveNumTxt.text = string.format("%d/%d", activityInfo.totalCount - activityInfo.usedCount,
                                              activityInfo.totalCount)
    self.view.staminaLaveNode.gameObject:SetActive(self.m_isFull and canReduceStamina)
end

WorldEnergyPointEntryCtrl._OnClickBtnClose = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end

WorldEnergyPointEntryCtrl._OnClickGemOverviewBtn = HL.Method() << function(self)
    UIManager:Open(PanelId.GemTermOverviewPopup, self.m_gameGroupId)
end

WorldEnergyPointEntryCtrl._OnClickEnemyInfoBtn = HL.Method(HL.Opt(HL.Number)) << function(self, initSelectEnemyLuaIndex)
    local worldEnergyPointCfg = Tables.worldEnergyPointTable[self.m_gameId]
    local enemyPopupArg = {
        title = Language.LUA_WEP_ENEMY_INFO_TITLE,
        enemyListTitle = Language["ui_dungeon_enemy_popup_info_list"],
        enemyInfoTitle = Language["ui_dungeon_enemy_popup_info_desc"],
        enemyIds = worldEnergyPointCfg.enemyIds,
        enemyLevels = worldEnergyPointCfg.enemyLevels
    }

    if initSelectEnemyLuaIndex ~= nil then
        enemyPopupArg.initSelectEnemyLuaIndex = initSelectEnemyLuaIndex
    end
    UIManager:AutoOpen(PanelId.CommonEnemyPopup, enemyPopupArg)
end

WorldEnergyPointEntryCtrl._OnClickBtnRewardDetails = HL.Method() << function(self)
    local wepGroupCfg = Tables.worldEnergyPointGroupTable[self.m_gameGroupId]
    local wepCfg = Tables.worldEnergyPointTable[self.m_gameId]
    local rewardArgs = {}


    local firstPartRewards = {}
    rewardArgs.firstPartRewardsTitle = Language.LUA_WEP_FIRST_PART_REWARD_TITLE
    rewardArgs.firstPartRewards = firstPartRewards

    local firstRewardGained = GameInstance.player.worldEnergyPointSystem:IsGameGroupFirstPassRewardGained(self.m_gameGroupId)
    local rewardCfg = Tables.rewardTable[wepGroupCfg.firstPassRewardId]
    for _, itemBundle in pairs(rewardCfg.itemBundles) do
        local reward = self:_GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.First, -1, -1, firstRewardGained,
                                           itemBundle.id, itemBundle.count)
        table.insert(firstPartRewards, reward)
    end
    table.sort(firstPartRewards, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))



    if self.m_isFull then
        local secondPartRewards = {}
        rewardArgs.secondPartRewardsTitle = Language.LUA_WEP_SECOND_PART_REWARD_TITLE
        rewardArgs.secondPartRewards = secondPartRewards

        for i = 0, wepCfg.regularItemIds.Count - 1 do
            local itemId = wepCfg.regularItemIds[i]
            local itemCount = i < wepCfg.regularItemCount.Count and wepCfg.regularItemCount[i] or 0
            local reward = self:_GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.Regular, -2, -2, false, itemId, itemCount)
            table.insert(secondPartRewards, reward)
        end

        for i = 0, wepCfg.probGemItemIds.Count - 1 do
            local itemId = wepCfg.probGemItemIds[i]
            local reward = self:_GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.Random, -2, -3, false, itemId)
            table.insert(secondPartRewards, reward)
        end
        local sortIds = UIConst.COMMON_ITEM_SORT_KEYS
        table.insert(sortIds, 1, "rewardTypeSortId")
        table.sort(secondPartRewards, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
    end








    UIManager:AutoOpen(PanelId.CommonRewardDetailsPopup, rewardArgs)
end

WorldEnergyPointEntryCtrl._OnClickGemCustomBtn = HL.Method() << function(self)
    PhaseManager:OpenPhase(PhaseId.GemCustomization, self.m_gameGroupId)
end

WorldEnergyPointEntryCtrl._OnClickWeakInfoBtn = HL.Method() << function(self)
    UIManager:Open(PanelId.InstructionBook, WORLD_ENERGY_POINT_WEAK_INSTRUCTION_ID)
end

WorldEnergyPointEntryCtrl._OnClickBtnGameStartBtn = HL.Method() << function(self)

    if self.m_isFull then
        local succ, wepGroupRecord = GameInstance.player.worldEnergyPointSystem:TryGetWorldEnergyPointGroupRecord(self.m_gameGroupId)
        local hasSelectTerms = succ and wepGroupRecord.hasSelectTerms
        if hasSelectTerms then

            local wepGroupCfg = Tables.worldEnergyPointGroupTable[self.m_gameGroupId]
            local gemCustomItemId = wepGroupCfg.gemCustomItemId
            local count = Utils.getItemCount(gemCustomItemId)
            if count > 0 then

                self:_StartStaminaCheck()
            else

                self:_ShowGemCustomItemLackPopup()
            end
        else

            self:_ShowNotGemCustomPopup()
        end
    else
        self:_TryStartWorldEnergyPoint()
    end
end

WorldEnergyPointEntryCtrl._TryStartWorldEnergyPoint = HL.Method() << function(self)
    GameInstance.player.worldEnergyPointSystem:SendReqStartWorldEnergyPoint(self.m_gameId, self.m_entityLid)
    PhaseManager:PopPhase(PHASE_ID)
end

WorldEnergyPointEntryCtrl._StartStaminaCheck = HL.Method() << function(self)
    local wepGameCfg = Tables.worldEnergyPointTable[self.m_gameId]
    local curStamina = GameInstance.player.inventory.curStamina
    local realCost = ActivityUtils.getRealStaminaCost(wepGameCfg.costStamina)
    if curStamina >= realCost then

        self:_TryStartWorldEnergyPoint()
    else
        local firstPassRewardGained = GameInstance.player.worldEnergyPointSystem:IsGameGroupFirstPassRewardGained(self.m_gameGroupId)
        local hint = firstPassRewardGained and Language.LUA_WEP_STAMINA_LACK_START_WITHOUT_FIRST_PASS_REWARD_CONFIRM_HINT or
                Language.LUA_WEP_STAMINA_LACK_START_CONFIRM_HINT
        self:_TryShowSerializedPopup(hint, WEP_STAMINA_LACK_START_CONFIRM_HINT_KEY, function()
            self:_TryStartWorldEnergyPoint()
        end)
    end
end

WorldEnergyPointEntryCtrl._ShowGemCustomItemLackPopup = HL.Method() << function(self)
    self:_TryShowSerializedPopup(Language.LUA_WEP_GEM_CUSTOM_ITEM_LACK_CONFIRM_HINT,
                                 WEP_GEM_CUSTOM_ITEM_LACK_CONFIRM_HINT_KEY, function()
        self:_StartStaminaCheck()
    end)
end

WorldEnergyPointEntryCtrl._ShowNotGemCustomPopup = HL.Method() << function(self)
    self:_TryShowSerializedPopup(Language.LUA_WEP_NOT_GEM_CUSTOM_CONFIRM_HINT,
                                 WEP_NOT_GEM_CUSTOM_CONFIRM_HINT_KEY, function()
        self:_StartStaminaCheck()
    end)
end

WorldEnergyPointEntryCtrl._TryShowSerializedPopup = HL.Method(HL.String, HL.String, HL.Function)
        << function(self, content, serializeKey, onConfirm)
    local suuc, ignoreHint = ClientDataManagerInst:GetBool(serializeKey, false, false, SERIALIZED_CATEGORY)
    if ignoreHint then
        onConfirm()
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
            content = content,
            onConfirm = function()
                ClientDataManagerInst:SetBool(serializeKey,
                                              closuresIsOn, false,
                                              SERIALIZED_CATEGORY,
                                              EClientDataTimeValidType.CurrentDay)
                onConfirm()
            end,
        })
    end
end

WorldEnergyPointEntryCtrl.GetRecoverPopupStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl.id == WORLD_ENERGY_POINT_WEAK_INSTRUCTION_ID then
        return {
            popupType = "WeakInstructionBook",
        }
    end

    local isGemOverviewOpen = UIManager:IsOpen(PanelId.GemTermOverviewPopup)
    if isGemOverviewOpen then
        return {
            popupType = "GemOverview",
        }
    end

    local isRewardDetailsOpen = UIManager:IsOpen(PanelId.CommonRewardDetailsPopup)
    if isRewardDetailsOpen then
        return {
            popupType = "RewardDetails",
        }
    end

    local isEnemyPopupOpen, enemyPopupCtrl = UIManager:IsOpen(PanelId.CommonEnemyPopup)
    if isEnemyPopupOpen then
        local popupState = {
            popupType = "EnemyDetails",
        }

        if HL.TryGet(enemyPopupCtrl, "GetCurSelectEnemyLuaIndex") then
            local selectedEnemyLuaIndex = enemyPopupCtrl:GetCurSelectEnemyLuaIndex()
            if selectedEnemyLuaIndex > 0 then
                popupState.selectedEnemyLuaIndex = selectedEnemyLuaIndex
            end
        end
        return popupState
    end
end

WorldEnergyPointEntryCtrl.TryRecoverPopupState = HL.Method(HL.Any) << function(self, popupState)
    if popupState == nil or string.isEmpty(popupState.popupType) then
        return
    end

    if popupState.popupType == "WeakInstructionBook" then
        local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
        if isOpen and instructionCtrl.id == WORLD_ENERGY_POINT_WEAK_INSTRUCTION_ID then
            return
        end
        self:_OnClickWeakInfoBtn()
        return
    end

    if popupState.popupType == "GemOverview" then
        local isOpen = UIManager:IsOpen(PanelId.GemTermOverviewPopup)
        if isOpen then
            return
        end
        self:_OnClickGemOverviewBtn()
        return
    end

    if popupState.popupType == "RewardDetails" then
        local isOpen = UIManager:IsOpen(PanelId.CommonRewardDetailsPopup)
        if isOpen then
            return
        end
        self:_OnClickBtnRewardDetails()
        return
    end

    if popupState.popupType == "EnemyDetails" then
        local isOpen = UIManager:IsOpen(PanelId.CommonEnemyPopup)
        if isOpen then
            return
        end
        self:_OnClickEnemyInfoBtn(popupState.selectedEnemyLuaIndex)
    end
end

WorldEnergyPointEntryCtrl.OnWEPSelectTermsChanged = HL.Method(HL.Table) << function(self, args)
    local gameGroupId = unpack(args)
    if self.m_gameGroupId ~= gameGroupId then
        return
    end

    self:_RefreshSelectTerms()
end

WorldEnergyPointEntryCtrl.OnStaminaChanged = HL.Method() << function(self)
    self:_RefreshCostStaminaNode()
end

HL.Commit(WorldEnergyPointEntryCtrl)
