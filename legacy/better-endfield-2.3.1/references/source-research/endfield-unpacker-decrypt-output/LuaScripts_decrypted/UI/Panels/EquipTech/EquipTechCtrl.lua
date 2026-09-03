
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.EquipTech
local PHASE_ID = PhaseId.EquipTech

EquipTechCtrl = HL.Class('EquipTechCtrl', uiCtrl.UICtrl)



local STATE_NAME = {
    PRODUCE = "produce",
    ENHANCE_TARGET = "enhanceTarget",
    ENHANCE_MATERIAL = "enhanceMaterial",
}

local EQUIP_SLOT_TAB_CONFIG = {
    [1] = {
        partType = nil,
    },
    [2] = {
        partType = GEnums.PartType.Body,
    },
    [3] = {
        partType = GEnums.PartType.Hand,
    },
    [4] = {
        partType = GEnums.PartType.EDC,
    },
}

local GO_TO_TEXT_KEY = {
    [GEnums.EquipFormulaUnlockType.EquipFormulaChest] = "LUA_EQUIP_FORMULA_SOURCE_CHEST",
    [GEnums.EquipFormulaUnlockType.DomainShop] = "LUA_EQUIP_FORMULA_SOURCE_SHOP",
    [GEnums.EquipFormulaUnlockType.StarShop] = "LUA_EQUIP_FORMULA_STAR_SHOP",
}

local MATERIAL_CELL_COUNT_PER_ROW = 2
local MATERIAL_CELL_ITEM_ROW_SIZE = 138
local MATERIAL_CELL_TITLE_ROW_SIZE = 84







EquipTechCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_EQUIP_PRODUCE] = '_OnEquipProduce',
    [MessageConst.ON_EQUIP_ENHANCE] = '_OnEquipEnhance',
    [MessageConst.ON_ITEM_COUNT_CHANGED] = '_OnItemChanged',
    [MessageConst.ON_WALLET_CHANGED] = '_OnWalletChanged',
    [MessageConst.GUIDE_EQUIP_PRODUCE_SCROLL_TO_ITEM] = '_OnGuideEquipProduceScrollToItem',
    [MessageConst.ON_EQUIP_FORMULA_CHAIN_CHANGED] = '_OnFormulaChainChanged',
    [MessageConst.ON_EQUIP_FORMULA_CHAIN_READ_CHANGED] = '_OnFormulaChainReadChanged',
    [MessageConst.ON_COMPLETE_GUIDE_GROUP] = '_OnCompleteGuideGroup',
    [MessageConst.ON_BUY_ITEM_SUCC] = '_OnShopBuyItem',
}

EquipTechCtrl.m_equipTechSystem = HL.Field(HL.Userdata)

EquipTechCtrl.m_arg = HL.Field(HL.Table)

EquipTechCtrl.m_fromDialog = HL.Field(HL.Boolean) << false

EquipTechCtrl.m_jumpEquipId = HL.Field(HL.String) << ""

EquipTechCtrl.m_jumpFormulaId = HL.Field(HL.String) << ""

EquipTechCtrl.m_jumpMaterialEquipId = HL.Field(HL.String) << ""

EquipTechCtrl.m_costItemIds = HL.Field(HL.Table)

EquipTechCtrl.m_jumpEquipInstId = HL.Field(HL.Number) << 0











EquipTechCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg or {}
    self.m_equipTechSystem = GameInstance.player.equipTechSystem

    self.m_fromDialog = arg ~= nil and arg.fromDialog == true

    self:_InitAction()
    self:_InitController()

    if arg ~= nil and arg.resumeState then
        self:_ApplyResumeState(arg.resumeState)
        self.m_arg.resumeState = nil
    elseif arg ~= nil and arg.isEnhance then
        self.m_jumpEquipId = string.isEmpty(arg.equipId) and "" or arg.equipId
        self.m_jumpMaterialEquipId = string.isEmpty(arg.materialEquipId) and "" or arg.materialEquipId
        self.m_jumpEquipInstId = arg.equipInstId or -1
        self:_EnterEnhanceTarget()
        self.view.commonBg.tabEnhanceNode.toggle:SetIsOnWithoutNotify(true)
    else
        self.m_isInitClickProduceEquip = true
        if arg and not string.isEmpty(arg.formulaId) then
            self.m_jumpFormulaId = arg.formulaId
        end
        self:_EnterProduce()
    end
end

EquipTechCtrl.OnClose = HL.Override() << function(self)
    self:_SendFormulaRead()
    self.m_equipTechSystem:CloseEquipTechPanel()
end

EquipTechCtrl.OnPhaseRefresh = HL.Override(HL.Opt(HL.Any)) << function(self, arg)

    if self.view.stateController.currentStateName == STATE_NAME.ENHANCE_MATERIAL then
        self:_BackToEnhanceTarget()
    end

    self:_EnterProduce()
end

EquipTechCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    self.view.rightProduceNode.bottomNode.numberSelector.view.keyHintLeft.gameObject:SetActive(active)
    self.view.rightProduceNode.bottomNode.numberSelector.view.keyHintRight.gameObject:SetActive(active)
end

EquipTechCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_arg and lume.deepCopy(self.m_arg) or {}
    arg.formulaId = nil
    arg.equipId = nil
    arg.materialEquipId = nil
    arg.isEnhance = self.view.stateController.currentStateName ~= STATE_NAME.PRODUCE
    arg.resumeState = self:_CollectResumeState()
    return arg
end

EquipTechCtrl._CollectResumeState = HL.Method().Return(HL.Table) << function(self)
    local state = {
        tabKey = self:_GetCurrentTabKey(),
    }
    if state.tabKey == "enhance" then
        state.enhanceTarget = self:_CollectEnhanceTargetResumeState()
        if self.view.stateController.currentStateName == STATE_NAME.ENHANCE_MATERIAL then
            state.enhanceMaterial = self:_CollectEnhanceMaterialResumeState()
        end
    else
        state.produce = self:_CollectProduceResumeState()
    end

    local isIntroOpen, introCtrl = UIManager:IsOpen(PanelId.CommonIntro)
    local introState = nil
    if isIntroOpen and introCtrl:IsShow() and PhaseManager:GetTopPhaseId() == PHASE_ID then
        introState = introCtrl:GetRecoverStateArg()
    end

    if introState and introState.introId == "equip_enhance" then
        state.commonIntroState = introState
    end


    if UIManager:IsOpen(PanelId.EquipFormulaChainSelect) then
        state.formulaChainSelectArg = { formulaId = self.m_selectedProduceFormulaId }
    elseif UIManager:IsOpen(PanelId.EquipBatchFormulaChainSelect) then
        state.batchFormulaChainSelectOpen = true
        local _, batchCtrl = UIManager:IsOpen(PanelId.EquipBatchFormulaChainSelect)
        if batchCtrl then
            state.batchFormulaChainSelectArg = batchCtrl:GetRecoverStateArg()
        end

        if introState and introState.introId == "equip_formula_switch" then
            state.batchCommonIntroState = introState
        end
    end
    if UIManager:IsOpen(PanelId.EquipFormulaChainAccessGuide) then
        local _, guideCtrl = UIManager:IsOpen(PanelId.EquipFormulaChainAccessGuide)
        if guideCtrl and guideCtrl.m_materialInfo then
            state.formulaChainAccessGuideArg = { chainId = guideCtrl.m_materialInfo.chainId }
        end
    end
    return state
end

EquipTechCtrl._CollectProduceResumeState = HL.Method().Return(HL.Table) << function(self)
    return {
        selectedFilterTags = self.m_selectedProduceFilterTags or {},
        sortState = self:_CollectSortState(self.view.leftBarProduce and self.view.leftBarProduce.sortNode or nil),
        selectedFormulaId = self.m_selectedProduceFormulaId,
        produceCount = self.m_produceCount,
    }
end

EquipTechCtrl._CollectEnhanceTargetResumeState = HL.Method().Return(HL.Table) << function(self)
    local commonItemList = self.view.leftBarEnhance and self.view.leftBarEnhance.commonItemList or nil
    return {
        equipSlotTabIndex = self:_GetEquipSlotTabIndexByPartType(self.m_partType),
        selectedFilterTags = commonItemList and commonItemList.m_selectedTags or {},
        sortState = self:_CollectSortState(commonItemList and commonItemList.view and commonItemList.view.sortNode or nil),
        selectedEquipInstId = self.m_selectedEnhanceEquipInstId,
    }
end

EquipTechCtrl._CollectEnhanceMaterialResumeState = HL.Method().Return(HL.Table) << function(self)
    local attrShowInfo = self.m_selectedAttrShowInfoList and self.m_selectedAttrShowInfoList[self.m_selectedAttrShowInfoIndex] or nil
    local state = {
        selectedAttrIndex = self.m_selectedAttrShowInfoIndex,
        selectedEnhancedAttrIndex = attrShowInfo and attrShowInfo.enhancedAttrIndex or 0,
        selectedMaterialInstIdList = self.m_selectedMaterialInstIdList,
        selectedMaterialInstId2Index = self.m_selectedMaterialInstId2Index,
    }
    local itemList = self.view.selectMaterials and self.view.selectMaterials.itemList
    if itemList and itemList.gameObject.activeInHierarchy then
        local scrollRect = itemList:GetComponent(typeof(CS.Beyond.UI.UIScrollRect))
        if scrollRect then
            state.materialListScroll = {
                verticalNormalizedPosition = scrollRect.verticalNormalizedPosition,
            }
        end
    end
    return state
end

EquipTechCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState then
        self:_EnterProduce()
        return
    end
    local tabKey = resumeState.tabKey
    if tabKey == "enhance" then
        local targetState = resumeState.enhanceTarget or {}
        local materialState = resumeState.enhanceMaterial
        self.m_selectedEnhanceEquipInstId = targetState.selectedEquipInstId or 0
        self:_EnterEnhanceTarget(targetState)
        self.view.commonBg.tabEnhanceNode.toggle:SetIsOnWithoutNotify(true)
        if materialState and self.m_selectedEnhanceEquipItemInfo then
            local attrIndex = materialState.selectedEnhancedAttrIndex
            if attrIndex and attrIndex > 0 then
                self.m_selectedAttrShowInfoIndex = self:_GetAttrShowInfoIndexByEnhancedAttrIndex(attrIndex)
            else
                self.m_selectedAttrShowInfoIndex = lume.clamp(materialState.selectedAttrIndex or 1, 1,
                    math.max(#self.m_selectedAttrShowInfoList, 1))
            end
            self.m_lastEnhanceAttrCell = self.m_enhanceAttrCellCache and self.m_enhanceAttrCellCache:GetItem(self.m_selectedAttrShowInfoIndex) or nil
            self:_EnterEnhanceMaterial(materialState)
        end
    else
        local produceState = resumeState.produce or {}
        self.m_selectedProduceFilterTags = produceState.selectedFilterTags or {}
        self.m_selectedProduceFormulaId = produceState.selectedFormulaId or ""
        self.m_produceCount = produceState.produceCount or 1
        if tabKey == "highLevelSuit" then
            self.view.commonBg.tabHighLevelSuitNode.toggle:SetIsOnWithoutNotify(true)
            self:_EnterHighLevelSuitProduce(true)
        elseif tabKey == "highLevelParts" then
            self.view.commonBg.tabHighLevelPartsNode.toggle:SetIsOnWithoutNotify(true)
            self:_EnterHighLevelPartsProduce(true)
        else

            self.view.commonBg.tabBasicNode.toggle:SetIsOnWithoutNotify(true)
            self:_EnterBasicProduce(true)
        end
        self:_ApplySortState(self.view.leftBarProduce and self.view.leftBarProduce.sortNode or nil, produceState.sortState)
    end

    if resumeState.commonIntroState then
        local isOpen = UIManager:IsOpen(PanelId.CommonIntro)
        if not isOpen then
            UIManager:Open(PanelId.CommonIntro, resumeState.commonIntroState)
        end
    end

    if resumeState.formulaChainSelectArg then
        UIManager:Open(PanelId.EquipFormulaChainSelect, resumeState.formulaChainSelectArg)
    elseif resumeState.batchFormulaChainSelectOpen then
        UIManager:Open(PanelId.EquipBatchFormulaChainSelect, resumeState.batchFormulaChainSelectArg)

        if resumeState.batchCommonIntroState and not UIManager:IsOpen(PanelId.CommonIntro) then
            UIManager:Open(PanelId.CommonIntro, resumeState.batchCommonIntroState)
        end
    end
    if resumeState.formulaChainAccessGuideArg then
        UIManager:Open(PanelId.EquipFormulaChainAccessGuide, resumeState.formulaChainAccessGuideArg)
    end
end

EquipTechCtrl._CollectSortState = HL.Method(HL.Any).Return(HL.Table) << function(self, sortNode)
    if not sortNode then
        return {}
    end
    return {
        optionIndex = sortNode:GetCurSelectedIndex(),
        isIncremental = sortNode.isIncremental == true,
    }
end

EquipTechCtrl._ApplySortState = HL.Method(HL.Any, HL.Opt(HL.Any)) << function(self, sortNode, sortState)
    if not sortNode or not sortState or not sortState.optionIndex then
        return
    end
    sortNode.isIncremental = sortState.isIncremental == true
    sortNode:RefreshIncremental()
    sortNode.view.mobilePCNode.dropDown:SetSelected(CSIndex(sortState.optionIndex), true, false)
    sortNode:OnSortChanged()
    sortNode:UpdateDeviceState()
end

EquipTechCtrl._ApplyCommonItemListSelectedTags = HL.Method(HL.Any, HL.Table) << function(self, commonItemList, selectedTags)
    if not commonItemList then
        return
    end
    selectedTags = selectedTags or {}
    commonItemList.m_selectedTags = selectedTags
    local filterBtn = commonItemList.view and commonItemList.view.filterBtn or nil
    local filterBtnWithText = commonItemList.view and commonItemList.view.filterBtnWithText or nil
    if filterBtn and filterBtn._UpdateState then
        filterBtn:_UpdateState(selectedTags)
    end
    if filterBtnWithText and filterBtnWithText._UpdateState then
        filterBtnWithText:_UpdateState(selectedTags)
    end
    commonItemList:Refresh({ skipGraduallyShow = true })
end

EquipTechCtrl._RestoreCommonItemListResumeState = HL.Method(HL.Any, HL.Any, HL.Any, HL.Boolean) << function(self,
    commonItemList, resumeState, selectedId, noScroll)
    if not commonItemList or not resumeState then
        return
    end
    self:_ApplyCommonItemListSelectedTags(commonItemList, resumeState.selectedFilterTags or {})
    self:_ApplySortState(commonItemList.view and commonItemList.view.sortNode or nil, resumeState.sortState)
    if selectedId and selectedId > 0 then
        commonItemList:SetSelectedId(selectedId, false, noScroll)
    end
end

EquipTechCtrl._GetCurrentTabKey = HL.Method().Return(HL.String) << function(self)
    if self.view.commonBg.tabEnhanceNode.toggle.isOn then
        return "enhance"
    end
    if self.view.commonBg.tabHighLevelSuitNode.toggle.isOn then
        return "highLevelSuit"
    end
    if self.view.commonBg.tabHighLevelPartsNode.toggle.isOn then
        return "highLevelParts"
    end
    return "basic"
end

EquipTechCtrl._GetEquipSlotTabIndexByPartType = HL.Method(HL.Any).Return(HL.Number) << function(self, partType)
    for luaIndex, config in ipairs(EQUIP_SLOT_TAB_CONFIG) do
        if config.partType == partType then
            return luaIndex
        end
    end
    return 1
end

EquipTechCtrl._GetAttrShowInfoIndexByEnhancedAttrIndex = HL.Method(HL.Number).Return(HL.Number) << function(self, enhancedAttrIndex)
    for luaIndex, attrShowInfo in ipairs(self.m_selectedAttrShowInfoList or {}) do
        if attrShowInfo.enhancedAttrIndex == enhancedAttrIndex then
            return luaIndex
        end
    end
    return 1
end





EquipTechCtrl._InitAction = HL.Method() << function(self)
    self:BindInputPlayerAction("equip_tech_close", function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.topBar.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.topBar.btnBack.onClick:AddListener(function()
        self.view.middleBar.enhanceAttrNode.animationWrapper:PlayOutAnimation()
        self.view.middleBar.bottomNode.animationWrapper:PlayOutAnimation()
        self.view.middleBar.materialContent.animationWrapper:PlayOutAnimation()
        self.view.selectMaterials.animationWrapper:PlayOutAnimation(function()
            self:_BackToEnhanceTarget()
        end)
    end)
    self.view.topBar.btnHelp.onClick:AddListener(function()
        Notify(MessageConst.SHOW_INTRO, "equip_enhance")
    end)

    self.view.commonBg.tabBasicNode.redDot:InitRedDot("EquipProducer", { isHighLevelSuit = false })
    self.view.commonBg.tabHighLevelSuitNode.redDot:InitRedDot("EquipProducer", { isHighLevelSuit = true, isSuit = true })
    self.view.commonBg.tabHighLevelPartsNode.redDot:InitRedDot("EquipProducer", { isHighLevelSuit = true, isSuit = false })

    self.view.commonBg.tabBasicNode.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self.m_playAnimProduceList = true
            self:_EnterBasicProduce()
        end
    end)
    self.view.commonBg.tabHighLevelSuitNode.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self.m_playAnimProduceList = true
            self:_EnterHighLevelSuitProduce()
        end
    end)
    self.view.commonBg.tabHighLevelPartsNode.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self.m_playAnimProduceList = true
            self:_EnterHighLevelPartsProduce()
        end
    end)
    self.view.commonBg.tabEnhanceNode.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:_EnterEnhanceTarget()
        end
    end)

    self.view.topBar.btnFormulaSwitch.onClick:AddListener(function()
        UIManager:Open(PanelId.EquipBatchFormulaChainSelect)
    end)

    self.view.middleBar.produceContent.formulaNode.switchFormulaBtnNode.onClick:AddListener(function()
        local args = {
            formulaId = self.m_selectedProduceFormulaId,
        }
        UIManager:Open(PanelId.EquipFormulaChainSelect, args)
    end)

    self.view.rightProduceNode.bottomNode.btnMake.onClick:AddListener(function()
        self:_CheckProduceConfirm()
    end)
    self.view.rightProduceNode.bottomNode.gotoNode.buttonGoto.onClick:AddListener(function()
        if not self.m_selectedProduceItemInfo or not self.m_selectedProduceItemInfo.equipFormulaData then
            logger.error("EquipTechCtrl->_OnProduceGotoClicked: No selected item info available.")
            return
        end

        local equipFormulaData = self.m_selectedProduceItemInfo.equipFormulaData
        if equipFormulaData.unlockType == GEnums.EquipFormulaUnlockType.StarShop then
            PhaseManager:OpenPhase(PhaseId.ShopStar, { targetGoodsId = equipFormulaData.unlockKey })
            return
        end

        local uniqueKey = equipFormulaData.unlockKey
        if string.isEmpty(uniqueKey) then
            logger.error("EquipTechCtrl->_OnProduceGotoClicked: Unique key is empty for formulaId: " .. equipFormulaData.formulaId)
            return
        end
        local markType
        if equipFormulaData.unlockType == GEnums.EquipFormulaUnlockType.EquipFormulaChest then
            markType = GEnums.MarkType.EquipFormulaChest
        elseif equipFormulaData.unlockType == GEnums.EquipFormulaUnlockType.DomainShop then
            markType = GEnums.MarkType.DomainShop
        end
        if markType then
            local found, instId = GameInstance.player.mapManager:GetMapMarkInstId(markType, uniqueKey)
            if found then
                MapUtils.openMap(instId)
            else
                logger.error("EquipTechCtrl->_OnProduceGotoClicked: Failed to find mark with type: " .. tostring(markType) .. " and unique key: " .. uniqueKey)
            end
        else
            logger.error("EquipTechCtrl->_OnProduceGotoClicked: Invalid unlock type: " .. tostring(equipFormulaData.unlockType))
        end
    end)

    local nextWorldLevel = self:_GetNextWorldLevel()
    if nextWorldLevel then
        self.view.middleBar.helpBtn.onClick:AddListener(function()
            PhaseManager:OpenPhaseFast(PhaseId.WorldLevelPopup, {
                childPanelArg = { targetWorldLevel = nextWorldLevel },
                isTipsMode = true,
                isFromCharUpgrade = true,
            })
        end)
    end

    self.view.commonBg.tabBasicNode.gameObject:SetActive(EquipTechUtils.hasVisibleBasicEquipPack())
    self.view.commonBg.tabHighLevelSuitNode.gameObject:SetActive(EquipTechUtils.hasVisibleHighLevelSuitEquipPack())
    self.view.commonBg.tabHighLevelPartsNode.gameObject:SetActive(EquipTechUtils.hasVisibleHighLevelPartsEquipPack())
    self.view.commonBg.tabEnhanceNode.gameObject:SetActive(Utils.isSystemUnlocked(GEnums.UnlockSystemType.EquipEnhance))
end

EquipTechCtrl._GetNextWorldLevel = HL.Method().Return(HL.Any) << function(self)
    local currentMaxWorldLevel = GameInstance.player.adventure.currentMaxWorldLevel
    local maxConfigWorldLevel = math.max(1, currentMaxWorldLevel)
    while true do
        local success = Tables.adventureWorldLevelTable:TryGetValue(maxConfigWorldLevel + 1)
        if not success then
            break
        end
        maxConfigWorldLevel = maxConfigWorldLevel + 1
    end
    local hasNextWorldLevel = currentMaxWorldLevel < maxConfigWorldLevel
    local nextTargetWorldLevel = math.min(currentMaxWorldLevel + 1, maxConfigWorldLevel)
    return hasNextWorldLevel and nextTargetWorldLevel or nil
end



EquipTechCtrl._CheckProduceConfirm = HL.Method() << function(self)
    local lockedFormulaId = self.m_selectedProduceItemInfo.equipFormulaData.formulaId
    local lockedCount = self.m_produceCount
    local worldLevel = GameInstance.player.adventure.currentMaxWorldLevel
    local highEquipMinLevel = Tables.EquipTechConst.highEquipMinWorldLevel
    local _, hideToday = ClientDataManagerInst:GetBool(
        "EQUIP_TECH_PRODUCE_CONFIRM_DAILY", false, false, "EquipTech")
    local curTab = self:_GetCurrentTabKey()
    local isHighLevelSuitTab = curTab == "highLevelSuit" or curTab == "highLevelParts"
    if hideToday or worldLevel >= highEquipMinLevel or not isHighLevelSuitTab then
        self:_DoProduce(lockedFormulaId, lockedCount)
        return
    end
    self:_ShowProduceConfirm(lockedFormulaId, lockedCount)
end


EquipTechCtrl._ShowProduceConfirm = HL.Method(HL.String, HL.Number) << function(self, formulaId, count)

    local closuresIsOn = false
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_EQUIP_FORMULA_PRODUCE_LOW_WORLD_LEVEL_MAKE_CONFIRM,
        toggle = {
            isOn = false,
            onValueChanged = function(isOn)
                closuresIsOn = isOn
            end,
            toggleText = Language.LUA_EQUIP_FORMULA_PRODUCE_DAILY_CONFIRM,
        },
        onConfirm = function()

            ClientDataManagerInst:SetBool(
                "EQUIP_TECH_PRODUCE_CONFIRM_DAILY", closuresIsOn, false,
                "EquipTech",
                EClientDataTimeValidType.CurrentDayUntil4AM)
            self:_DoProduce(formulaId, count)
        end,
        showWorldUpgradeBtn = true,
    })
end


EquipTechCtrl._DoProduce = HL.Method(HL.String, HL.Number) << function(self, formulaId, count)
    logger.info("[EquipTech] DoProduce", count, formulaId)
    if self:_IsEquipDepotFull(count) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_EQUIP_TECH_EQUIP_DEPOT_FULL)
        return
    end
    local chainId = EquipTechUtils.GetCurFormulaChainId(formulaId)
    self.m_equipTechSystem:ProduceEquip(formulaId, chainId, count)
end

EquipTechCtrl._EnterProduce = HL.Method() << function(self)
    local jumpSuccess = false
    if not string.isEmpty(self.m_jumpFormulaId) then
        local _, formulaData = Tables.equipFormulaTable:TryGetValue(self.m_jumpFormulaId)
        if formulaData then
            local _, equipPackData = Tables.equipPackTable:TryGetValue(formulaData.packId)
            if equipPackData then
                if not equipPackData.isHighLevelSuit and self.view.commonBg.tabBasicNode.gameObject.activeSelf then
                    self.view.commonBg.tabBasicNode.toggle:SetIsOnWithoutNotify(true)
                    self:_EnterBasicProduce()
                    jumpSuccess = true
                elseif equipPackData.isHighLevelSuit and equipPackData.isSuit and self.view.commonBg.tabHighLevelSuitNode.gameObject.activeSelf then
                    self.view.commonBg.tabHighLevelSuitNode.toggle:SetIsOnWithoutNotify(true)
                    self:_EnterHighLevelSuitProduce()
                    jumpSuccess = true
                elseif equipPackData.isHighLevelSuit and not equipPackData.isSuit and self.view.commonBg.tabHighLevelPartsNode.gameObject.activeSelf then
                    self.view.commonBg.tabHighLevelPartsNode.toggle:SetIsOnWithoutNotify(true)
                    self:_EnterHighLevelPartsProduce()
                    jumpSuccess = true
                end
            else
                logger.error("EquipTechCtrl->_EnterProduce: No equip pack data found for packId: " .. formulaData.packId)
            end
        else
            logger.error("EquipTechCtrl->_EnterProduce: No formula data found for formulaId: " .. self.m_jumpFormulaId)
        end
        self.m_jumpFormulaId = ""
    end

    if not jumpSuccess then
        local worldLevel = GameInstance.player.adventure.currentMaxWorldLevel
        local highEquipMinLevel = Tables.EquipTechConst.highEquipMinWorldLevel
        local preferHighLevel = worldLevel >= highEquipMinLevel

        if preferHighLevel and self.view.commonBg.tabHighLevelSuitNode.gameObject.activeSelf then
            self.view.commonBg.tabHighLevelSuitNode.toggle:SetIsOnWithoutNotify(true)
            self:_EnterHighLevelSuitProduce()
        elseif self.view.commonBg.tabBasicNode.gameObject.activeSelf then
            self.view.commonBg.tabBasicNode.toggle:SetIsOnWithoutNotify(true)
            self:_EnterBasicProduce()
        elseif self.view.commonBg.tabHighLevelSuitNode.gameObject.activeSelf then
            self.view.commonBg.tabHighLevelSuitNode.toggle:SetIsOnWithoutNotify(true)
            self:_EnterHighLevelSuitProduce()
        else
            self.view.commonBg.tabHighLevelPartsNode.toggle:SetIsOnWithoutNotify(true)
            self:_EnterHighLevelPartsProduce()
        end
    end
end

EquipTechCtrl._OnGuideEquipProduceScrollToItem = HL.Method(HL.Table) << function(self, args)
    local itemId = unpack(args)
    local _, formulaId = Tables.equipFormulaReverseTable:TryGetValue(itemId)
    if formulaId then
        self.m_jumpFormulaId = formulaId
        self:_RefreshProduceList()
        self.m_jumpFormulaId = ""
    end
end

EquipTechCtrl._IsEquipDepotFull = HL.Method(HL.Number).Return(HL.Boolean) << function(self, addCount)
    local depots = GameInstance.player.inventory.valuableDepots
    if not depots:ContainsKey(GEnums.ItemValuableDepotType.Equip) then
        return false
    end
    local equipDepot = depots[GEnums.ItemValuableDepotType.Equip]:GetOrFallback(Utils.getCurrentScope())
    return equipDepot:GetUsedGridCount() + addCount > equipDepot.gridLimit
end

EquipTechCtrl._OnFormulaChainChanged = HL.Method(HL.Any) << function(self, arg)
    local mode, chainId = unpack(arg)
    local CHAIN_MODE = CS.Proto.EQUIP_FORMULA_CHAIN_MODE
    local MODE_SINGLE = CHAIN_MODE.Single:GetHashCode()
    local MODE_BATCH = CHAIN_MODE.Batch:GetHashCode()
    local MODE_BATCH_WITH_PRESERVE = CHAIN_MODE.BatchWithPreserve:GetHashCode()
    local MODE_RESET = CHAIN_MODE.Reset:GetHashCode()

    if mode == MODE_RESET then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_EQUIP_FORMULA_CHAIN_RESET_TOAST)
    elseif mode == MODE_BATCH or mode == MODE_BATCH_WITH_PRESERVE then
        local hasValue, materialInfo = EquipTechUtils.GetMaterialInfoByChainId(chainId)
        local materialName = hasValue and materialInfo.scriptName or ""
        Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_EQUIP_FORMULA_CHAIN_BATCH_TOAST, materialName))
    elseif mode == MODE_SINGLE then
        local hasValue, materialInfo = EquipTechUtils.GetMaterialInfoByChainId(chainId)
        local materialName = hasValue and materialInfo.scriptName or ""
        Notify(MessageConst.SHOW_EQUIP_TOAST, string.format(Language.LUA_EQUIP_FORMULA_CHAIN_SINGLE_TOAST, materialName))
    end

    if self.view.stateController.currentStateName ~= STATE_NAME.PRODUCE then
        return
    end

    if mode ~= MODE_SINGLE then
        self:_RefreshProduceList()
    end

    if self.m_selectedProduceItemInfo then
        self:_RefreshProduceEquipInfo(self.m_selectedProduceItemInfo)
    end
end

EquipTechCtrl._OnFormulaChainReadChanged = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    self:_UpdateNewChainTip()
end

EquipTechCtrl._OnCompleteGuideGroup = HL.Method(HL.Any) << function(self, arg)
    local groupId = unpack(arg)
    local count =  Tables.EquipTechConst.updateHintFinishedGuideId.Count
    for i = 0, count - 1 do
        if Tables.EquipTechConst.updateHintFinishedGuideId[i] == groupId then
            EquipTechUtils.MarkChainsAsRead()
            break
        end
    end
end

EquipTechCtrl._OnShopBuyItem = HL.Method(HL.Any) << function(self, arg)
    if self.view.commonBg.tabBasicNode.toggle.isOn then
        self:_EnterBasicProduce()
    elseif self.view.commonBg.tabHighLevelSuitNode.toggle.isOn then
        self:_EnterHighLevelSuitProduce()
    elseif self.view.commonBg.tabHighLevelPartsNode.toggle.isOn then
        self:_EnterHighLevelPartsProduce()
    end
end





EquipTechCtrl.m_getEquipPackCell = HL.Field(HL.Function)

EquipTechCtrl.m_equipPackDataList = HL.Field(HL.Table)

EquipTechCtrl.m_filteredEquipPackDataList = HL.Field(HL.Table)

EquipTechCtrl.m_selectedProduceFilterTags = HL.Field(HL.Table)

EquipTechCtrl.m_selectedProduceItemInfo = HL.Field(HL.Table)

EquipTechCtrl.m_selectedProduceFormulaId = HL.Field(HL.String) << ""

EquipTechCtrl.m_selectedProduceItemCell = HL.Field(HL.Userdata)

EquipTechCtrl.m_costItemCellCache = HL.Field(HL.Forward("UIListCache"))

EquipTechCtrl.m_costItemCountUpdateFunctions = HL.Field(HL.Table)

EquipTechCtrl.m_playAnimProduceList = HL.Field(HL.Boolean) << false

EquipTechCtrl.m_isInitClickProduceEquip = HL.Field(HL.Boolean) << false

EquipTechCtrl.m_jumpFormulaCell = HL.Field(HL.Any)

EquipTechCtrl.m_produceCount = HL.Field(HL.Number) << 0

EquipTechCtrl._EnterBasicProduce = HL.Method(HL.Opt(HL.Boolean)) << function(self, preserveSelection)
    self:_RemoveEnhanceTargetNaviGroup()
    self:_SendFormulaRead()
    self.view.stateController:SetState(STATE_NAME.PRODUCE)
    self.view.commonBg.stateController:SetState("basicProduce")
    self.m_equipPackDataList = EquipTechUtils.getUnlockedEquipPackList(false, nil)
    self.m_filteredEquipPackDataList = self.m_equipPackDataList
    self:_ClearProduceEquipSelection(preserveSelection == true)
    self:_InitProduceList()
    self.m_equipTechSystem:NotifyHighLevelEquipTabChanged(CS.Beyond.Gameplay.EquipTechSystem.EHighLevelEquipTabType.Basics:GetHashCode())

    self:_UpdateNewChainTip()
    self:_UpdateWorldLevelLimitTip()
end

EquipTechCtrl._EnterHighLevelSuitProduce = HL.Method(HL.Opt(HL.Boolean)) << function(self, preserveSelection)
    self:_RemoveEnhanceTargetNaviGroup()
    self:_SendFormulaRead()
    self.view.stateController:SetState(STATE_NAME.PRODUCE)
    self.view.commonBg.stateController:SetState("highLevelProduce")
    self.m_equipPackDataList = EquipTechUtils.getUnlockedEquipPackList(true, true)
    self.m_filteredEquipPackDataList = self.m_equipPackDataList
    self:_ClearProduceEquipSelection(preserveSelection == true)
    self:_InitProduceList()

    self:_UpdateNewChainTip()
    self:_UpdateWorldLevelLimitTip()

    local notifyTabChangeFunc = function()
       local tabType = CS.Beyond.Gameplay.EquipTechSystem.EHighLevelEquipTabType.HighLevelSuit:GetHashCode()
       GameInstance.player.equipTechSystem:NotifyHighLevelEquipTabChanged(tabType)
    end
    local show = EquipTechUtils.TryShowNewGeneralGoldEquip(notifyTabChangeFunc)
    if not show then
        notifyTabChangeFunc()
    end
end

EquipTechCtrl._EnterHighLevelPartsProduce = HL.Method(HL.Opt(HL.Boolean)) << function(self, preserveSelection)
    self:_RemoveEnhanceTargetNaviGroup()
    self:_SendFormulaRead()
    self.view.stateController:SetState(STATE_NAME.PRODUCE)
    self.view.commonBg.stateController:SetState("highLevelProduce")
    self.m_equipPackDataList = EquipTechUtils.getUnlockedEquipPackList(true, false)
    self.m_filteredEquipPackDataList = self.m_equipPackDataList
    self:_ClearProduceEquipSelection(preserveSelection == true)
    self:_InitProduceList()
    self.m_equipTechSystem:NotifyHighLevelEquipTabChanged(CS.Beyond.Gameplay.EquipTechSystem.EHighLevelEquipTabType.HighLevelParts:GetHashCode())

    self:_UpdateNewChainTip()
    self:_UpdateWorldLevelLimitTip()
end

EquipTechCtrl._RefreshProducePackRedDot = HL.Method(HL.String, HL.Boolean) << function(self, packId, active)
    for _, packData in ipairs(self.m_equipPackDataList) do
        if packData.equipPackData.packId == packId then
            packData.hasRedDot = active
            break
        end
    end
end

EquipTechCtrl._InitProduceList = HL.Method() << function(self)
    if not self.m_getEquipPackCell then
        self.m_getEquipPackCell = UIUtils.genCachedCellFunction(self.view.leftBarProduce.itemList)
        self.view.leftBarProduce.itemList.onUpdateCell:AddListener(function(object, csIndex)
            local cell = self.m_getEquipPackCell(object)
            local packData = self.m_filteredEquipPackDataList[LuaIndex(csIndex)]
            self:_UpdateProducePackCell(cell, packData, csIndex)
            cell.gameObject.name = packData.equipPackData.packId
        end)
        self.view.leftBarProduce.redDotScrollRect.getRedDotStateAt = function(index)
            return self:_GetEquipPackRedDotStateAt(index)
        end


        local filterArgs = {
            tagGroups = FilterUtils.generateConfig_EQUIP_PRODUCE(),
            selectedTags = self.m_selectedProduceFilterTags,
            onConfirm = function(selectedTags)
                self.m_selectedProduceFilterTags = selectedTags
                self:_ApplyProduceFilterOption(selectedTags)
                local sortNode = self.view.leftBarProduce.sortNode
                self:_ApplyProduceSortOption(sortNode:GetCurSortData(), sortNode.isIncremental)
                self:_RefreshProduceList()
            end,
            getResultCount = function(selectedTags)
                return self:_GetProduceFilterResultCount(selectedTags)
            end,
            sortNodeWidget = self.view.leftBarProduce.sortNode,
        }
        self.view.leftBarProduce.filterBtn:InitFilterBtn(filterArgs)

        local defaultSort = EquipTechUtils.GetSortType()
        defaultSort = CSIndex(defaultSort)
        self.view.leftBarProduce.sortNode:InitSortNode(EquipTechConst.EQUIP_PRODUCE_PACK_SORT_OPTION, function(sortOption, isIncremental)
            self:_ClearProduceEquipSelection()
            self:_ApplyProduceSortOption(sortOption, isIncremental)
            self:_RefreshProduceList()
        end, defaultSort, nil, true, self.view.leftBarProduce.filterBtn)
    end

    self:_ApplyProduceFilterOption(self.m_selectedProduceFilterTags)
    local sortNode = self.view.leftBarProduce.sortNode
    self:_ApplyProduceSortOption(sortNode:GetCurSortData(), sortNode.isIncremental)
    self:_RefreshProduceList()
end

EquipTechCtrl._ApplyProduceSortOption = HL.Method(HL.Table, HL.Boolean) << function(self, sortOption, isIncremental)
    local sortFunc = Utils.genSortFunction(sortOption.keys, isIncremental)
    table.sort(self.m_filteredEquipPackDataList, sortFunc)

    local curIndex = self.view.leftBarProduce.sortNode:GetCurSelectedIndex()
    local config = EquipTechConst.EQUIP_PRODUCE_PACK_SORT_CONFIG[curIndex]
    local innerKeys = config and config.innerKeys or EquipTechConst.EQUIP_PRODUCE_PACK_SORT_CONFIG[EquipTechConst.PANL_SORT_TYPE.MATERIAL].innerKeys
    local innerSortFunc = Utils.genSortFunction(innerKeys)
    for _, packData in ipairs(self.m_filteredEquipPackDataList) do
        table.sort(packData.equipList, innerSortFunc)
    end


    EquipTechUtils.UpdateSelectedSortType(curIndex)
end

EquipTechCtrl._ApplyProduceFilterOption = HL.Method(HL.Opt(HL.Table)) << function(self, tagInfoList)
    if not tagInfoList or not next(tagInfoList) then
        self.m_filteredEquipPackDataList = self.m_equipPackDataList
    else
        self.m_filteredEquipPackDataList = {}
        for _, packData in ipairs(self.m_equipPackDataList) do
            local equipList = {}
            for _, itemInfo in ipairs(packData.equipList) do
                if FilterUtils.checkIfPassFilter(itemInfo, tagInfoList) then
                    table.insert(equipList, itemInfo)
                end
            end
            if #equipList > 0 then

                local newPackData = {
                    equipPackData = packData.equipPackData,
                    sortId = packData.equipPackData.sortId,
                    isExpanded = true,
                    equipList = equipList,
                }
                table.insert(self.m_filteredEquipPackDataList, newPackData)
            end
        end
    end
end

EquipTechCtrl._GetProduceFilterResultCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tagInfoList)
    local count = 0
    for _, packData in ipairs(self.m_equipPackDataList) do
        for _, itemInfo in ipairs(packData.equipList) do
            if FilterUtils.checkIfPassFilter(itemInfo, tagInfoList) then
                count = count + 1
            end
        end
    end
    return count
end

EquipTechCtrl._RefreshProduceList = HL.Method() << function(self)
    local count = #self.m_filteredEquipPackDataList
    local targetPackIndex
    local targetItemInfo
    if not string.isEmpty(self.m_jumpFormulaId) then
        for i, packData in pairs(self.m_filteredEquipPackDataList) do
            for j, itemInfo in pairs(packData.equipList) do
                if itemInfo.equipFormulaData.formulaId == self.m_jumpFormulaId then
                    targetPackIndex = i
                    break
                end
            end
            if targetPackIndex then
                break
            end
        end
    elseif not string.isEmpty(self.m_selectedProduceFormulaId) then
        for i, packData in pairs(self.m_filteredEquipPackDataList) do
            for j, itemInfo in pairs(packData.equipList) do
                if itemInfo.equipFormulaData.formulaId == self.m_selectedProduceFormulaId then
                    targetPackIndex = i
                    targetItemInfo = itemInfo
                    break
                end
            end
            if targetPackIndex then
                break
            end
        end
    end

    if targetPackIndex then
        self.view.leftBarProduce.itemList:UpdateCount(count, CSIndex(targetPackIndex), true, false, true)
    else
        self.view.leftBarProduce.itemList:UpdateCount(count, true, true)
    end

    self.view.leftBarProduce.emptyNode.gameObject:SetActive(count == 0)
    if self.m_playAnimProduceList then
        self.view.leftBarProduce.animationWrapper:ClearTween(false)
        self.view.leftBarProduce.animationWrapper:PlayInAnimation()
    end

    if self.m_jumpFormulaCell then
        local scrollRect = self.view.leftBarProduce.itemList:GetComponent(typeof(CS.Beyond.UI.UIScrollRect))
        scrollRect:AutoScrollToRectTransform(self.m_jumpFormulaCell.transform, true)
        if DeviceInfo.usingController then
            self:SetNaviTarget(self.m_jumpFormulaCell.view.button)
        else
            self.m_jumpFormulaCell.view.button.onClick:Invoke()
        end
        self.view.rightProduceNode.emptyNode.gameObject:SetActive(false)
        self.m_jumpFormulaCell = nil
    elseif count > 0 then
        local packIndex = targetPackIndex or 1
        local packCell = self.m_getEquipPackCell(self.view.leftBarProduce.itemList:Get(CSIndex(packIndex)))
        local targetEquipCell = nil
        if packCell then
            if targetItemInfo then
                for itemIndex, itemInfo in ipairs(self.m_filteredEquipPackDataList[packIndex].equipList) do
                    if itemInfo.equipFormulaData.formulaId == targetItemInfo.equipFormulaData.formulaId then
                        targetEquipCell = packCell.itemCache:Get(itemIndex)
                        break
                    end
                end
            end
            targetEquipCell = targetEquipCell or packCell.itemCache:Get(1)
        end
        local targetItem = targetItemInfo or (self.m_filteredEquipPackDataList[packIndex] and self.m_filteredEquipPackDataList[packIndex].equipList[1] or nil)
        if not targetEquipCell then
            if targetItem then
                self.m_selectedProduceItemInfo = targetItem
                self.m_selectedProduceFormulaId = targetItem.equipFormulaData.formulaId
                self:_RefreshProduceEquipInfo(targetItem, true)
            end
            self.view.rightProduceNode.emptyNode.gameObject:SetActive(targetItem == nil)
            self.m_playAnimProduceList = false
            return
        end
        if targetItem then
            self:_OnProduceItemClicked(targetEquipCell, targetItem, false)
            self:_RefreshProduceEquipInfo(targetItem, true)
        end
        if DeviceInfo.usingController then
            self:SetNaviTarget(targetEquipCell.view.button)
        else
            targetEquipCell.view.button.onClick:Invoke()
        end
        self.view.rightProduceNode.emptyNode.gameObject:SetActive(false)
    end

    self.m_playAnimProduceList = false
end

EquipTechCtrl._UpdateProducePackCell = HL.Method(HL.Table, HL.Table, HL.Number) << function(self, cell, packData, packCsIndex)
    cell.nameText.text = packData.equipPackData.name
    local hasIcon = not string.isEmpty(packData.equipPackData.iconId)
    cell.decoImg.gameObject:SetActive(hasIcon)
    if hasIcon then
        cell.decoImg:LoadSprite(UIConst.UI_SPRITE_EQUIPMENT_LOGO_BIG_WHITE, packData.equipPackData.iconId)
    end
    cell.toggle.onValueChanged:RemoveAllListeners()
    cell.toggle.onValueChanged:AddListener(function(isOn)
        if isOn ~= packData.isExpanded then
            packData.isExpanded = isOn
            self.view.leftBarProduce.itemList:Toggle(packCsIndex)
        end
    end)
    cell.toggle.isOn = packData.isExpanded
    self.view.leftBarProduce.itemList:ToggleByState(packCsIndex, packData.isExpanded, true)
    local packId = packData.equipPackData.packId
    cell.redDot:InitRedDot("EquipPack", packId, function(redDot, active, rdType)
        redDot.view.allNew.gameObject:SetActive(active and rdType == EquipTechConst.EQUIP_PRODUCE_PACK_RED_DOT_TYPE.AllNew)
        redDot.view.partialNew.gameObject:SetActive(active and rdType == EquipTechConst.EQUIP_PRODUCE_PACK_RED_DOT_TYPE.PartialNew)
        self:_RefreshProducePackRedDot(packId, active)
        self:UpdateGeneralEquipPackTag(redDot, packId)
    end, self.view.leftBarProduce.redDotScrollRect)

    if cell.itemCache == nil then
        cell.itemCache = UIUtils.genCellCache(cell.itemBigBlack)
    end
    cell.itemCache:Refresh(#packData.equipList, function(itemCell, index)
        local itemInfo = packData.equipList[index]
        self:_UpdateProduceEquipCell(itemInfo, itemCell)
    end)

    self:UpdateGeneralEquipPackTag(cell.redDot, packId)
end

EquipTechCtrl.UpdateGeneralEquipPackTag = HL.Method(HL.Any, HL.Any)<< function(self, redDot, packId)
    local hasRedDot, redDotType = RedDotManager:GetRedDotState("EquipPack", packId)
    if not hasRedDot and packId == Tables.EquipTechConst.generalEquipPackId then
        redDot.view.recommend.gameObject:SetActive(true)
        redDot.view.recommendText.text = Language.LUA_EQUIP_PACK_RECOMMEND
        return;
    end

    redDot.view.recommend.gameObject:SetActive(false)
end

EquipTechCtrl._UpdateProduceEquipCell = HL.Method(HL.Table, HL.Userdata) << function(self, itemInfo, itemCell)
    itemCell.view.gameObject.name = itemInfo.id
    itemCell:InitItem({ id = itemInfo.id} , function()
        self:_OnProduceItemClicked(itemCell, itemInfo, true)
    end)
    itemCell:SetExtraInfo({
        isSideTips = DeviceInfo.usingController,
    })
    if DeviceInfo.usingController then
        itemCell:SetEnableHoverTips(false)
    end
    local isSelected = self.m_selectedProduceItemInfo == itemInfo
    itemCell:SetSelected(isSelected)
    itemCell.redDot = itemCell.view.redDot
    itemCell:UpdateRedDot("EquipFormula", itemInfo.equipFormulaData.formulaId)
    itemCell.view.disableMark.gameObject:SetActive(not itemInfo.isUnlocked)
    if isSelected then
        self.m_selectedProduceItemCell = itemCell
    end

    local formulaId = itemInfo.equipFormulaData.formulaId
    if self.m_equipTechSystem:IsFormulaUnread(formulaId) then
        self.m_readFormulas = self.m_readFormulas or {}
        self.m_readFormulas[formulaId] = true
    end
    if itemInfo.equipFormulaData.isNew and not self.m_equipTechSystem:IsNewVersionFormulaRead(formulaId) then
        self.m_readNewVersionFormulas = self.m_readNewVersionFormulas or {}
        self.m_readNewVersionFormulas[formulaId] = true
    end
    if not string.isEmpty(self.m_jumpFormulaId) and self.m_jumpFormulaId == formulaId then
        self.m_jumpFormulaCell = itemCell
    end

    local curKey = self:_GetCurrentTabKey()
    local showFormulaIcon = curKey ~= "basic" and curKey ~= "enhance"
    itemCell.view.iconFormula.gameObject:SetActive(showFormulaIcon)
    if showFormulaIcon then
        local defaultCostItemId = EquipTechUtils.GetDefaultCostMaterial(formulaId)
        if defaultCostItemId then
            local _, defaultItemData = Tables.itemTable:TryGetValue(defaultCostItemId)
            if defaultItemData then
                itemCell.view.iconFormula:LoadSprite(UIConst.UI_SPRITE_ITEM, defaultItemData.iconId)
            end
        end
    end
end

EquipTechCtrl._OnProduceItemClicked = HL.Method(HL.Userdata, HL.Table, HL.Opt(HL.Boolean)) << function(self, itemCell, itemInfo, playAnim)
    if self.m_selectedProduceItemInfo == itemInfo then
        return
    end
    self.m_selectedProduceItemInfo = itemInfo
    self.m_selectedProduceFormulaId = itemInfo.equipFormulaData.formulaId

    if self.m_selectedProduceItemCell then
        self.m_selectedProduceItemCell:SetSelected(false)
    end
    self.m_selectedProduceItemCell = itemCell
    self.m_selectedProduceItemCell:SetSelected(true)

    local formulaId = itemInfo.equipFormulaData.formulaId
    if self.m_equipTechSystem:IsFormulaUnread(formulaId) then
        self.m_equipTechSystem:SetFormulaRead({ formulaId })
    end
    if itemInfo.equipFormulaData.isNew and not self.m_equipTechSystem:IsNewVersionFormulaRead(formulaId) then
        self.m_equipTechSystem:SetNewVersionFormulaRead({ formulaId })
    end

    if playAnim and not self.m_isInitClickProduceEquip then
        self.view.middleBar.produceContent.animationWrapper:ClearTween(false)
        self.view.middleBar.produceContent.animationWrapper:PlayOutAnimation(function()
            self:_RefreshProduceEquipInfo(itemInfo)
            self.view.middleBar.produceContent.animationWrapper:PlayInAnimation()
            self.view.rightProduceNode.animationWrapper:ClearTween(false)
            self.view.rightProduceNode.animationWrapper:Play("equiptech_content_right_switch")
            self.view.middleBar.centerItem.animationWrapper:ClearTween(false)
            self.view.middleBar.centerItem.animationWrapper:PlayInAnimation()
        end)
        if not self.m_playAnimProduceList then
            AudioAdapter.PostEvent("Au_UI_Toast_SelectEquipMotion")
        end
    else
        self:_RefreshProduceEquipInfo(itemInfo)
    end
    self.m_isInitClickProduceEquip = false
end

EquipTechCtrl._ClearProduceEquipSelection = HL.Method(HL.Opt(HL.Boolean)) << function(self, preserveFormulaId)
    local selectedFormulaId = preserveFormulaId and self.m_selectedProduceFormulaId or ""
    if self.m_selectedProduceItemCell then
        self.m_selectedProduceItemCell:SetSelected(false)
        self.m_selectedProduceItemCell = nil
    end
    if not preserveFormulaId then
        self.m_selectedProduceFormulaId = ""
    end
    if DeviceInfo.usingController then
        self:SetNaviTarget(nil)
    end
    self:_RefreshProduceEquipInfo(nil)
    if preserveFormulaId then
        self.m_selectedProduceFormulaId = selectedFormulaId
    end
end

EquipTechCtrl._RefreshProduceEquipInfo = HL.Method(HL.Table, HL.Opt(HL.Boolean)) << function(self, itemInfo, isCostItemCountChanged)
    self.m_selectedProduceItemInfo = itemInfo
    self.m_selectedProduceFormulaId = itemInfo and itemInfo.equipFormulaData and itemInfo.equipFormulaData.formulaId or ""
    self.m_costItemIds = {}
    self.m_costItemCountUpdateFunctions = {}
    local isEmpty = itemInfo == nil
    local curKey = self:_GetCurrentTabKey()

    self.view.middleBar.centerItem.gameObject:SetActive(not isEmpty)
    self.view.middleBar.produceContent.equipInfo.gameObject:SetActive(not isEmpty)
    self.view.middleBar.produceContent.formulaNode.gameObject:SetActive(not isEmpty)
    self.view.middleBar.centerItem.imgEquip.gameObject:SetActive(not isEmpty)
    self.view.rightProduceNode.emptyNode.gameObject:SetActive(isEmpty)
    self.view.rightProduceNode.equipDetails.gameObject:SetActive(not isEmpty)
    self.view.rightProduceNode.bottomNode.gameObject:SetActive(not isEmpty)
    local showFormulaChainBtn = curKey ~= "basic" and curKey ~= "enhance" and itemInfo and itemInfo.isUnlocked
    self.view.middleBar.produceContent.formulaNode.switchFormulaBtnNode.gameObject:SetActive(showFormulaChainBtn)
    self.view.topBar.btnFormulaSwitch.gameObject:SetActive(curKey ~= "basic" and curKey ~= "enhance")
    self.view.middleBar.produceContent.formulaNode.initialFormulaNode.gameObject:SetActive(showFormulaChainBtn)
    if isEmpty then
        return
    end

    self.view.middleBar.centerItem.imgEquip:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemInfo.data.iconId)
    self.view.middleBar.centerItem.commonStorageNodeNew:InitStorageNode((Utils.getItemCount(itemInfo.id, true, true)))
    self.view.middleBar.produceContent.equipInfo:InitEquipTechEquipInfo(itemInfo.id)
    self.view.middleBar.produceContent.formulaNode.emptyState.gameObject:SetActive(not itemInfo.isUnlocked)

    local defaultCostItemId = EquipTechUtils.GetDefaultCostMaterial(itemInfo.equipFormulaData.formulaId)
    if showFormulaChainBtn and defaultCostItemId then
        local _, defaultItemData = Tables.itemTable:TryGetValue(defaultCostItemId)
        if defaultItemData then
            self.view.middleBar.produceContent.formulaNode.iconInitialFormula:LoadSprite(UIConst.UI_SPRITE_ITEM, defaultItemData.iconId)
        end
    end

    local isCostEnough = true
    if not self.m_costItemCellCache then
        self.m_costItemCellCache = UIUtils.genCellCache(self.view.middleBar.produceContent.formulaNode.costItemCell)
    end
    local costItemCount = 0
    local maxProduceCount = Tables.equipTechConst.equipProduceMaxCount
    local costItemTable
    if itemInfo.isUnlocked then
        costItemTable = {}
        local formulaId = itemInfo.equipFormulaData.formulaId
        local chainId = EquipTechUtils.GetCurFormulaChainId(formulaId)
        local level = itemInfo.equipFormulaData.level
        local chainData = EquipTechUtils.GetLevelChainDetail(level, chainId)
        if chainData then
            if not string.isEmpty(chainData.costGoldId) and chainData.costGoldNum > 0 then
                local costInfo = EquipTechUtils.GetChainCostInfo(chainId)
                local discount = costInfo and costInfo.discount or nil
                table.insert(costItemTable, { id = chainData.costGoldId, count = chainData.costGoldNum, discount = discount })
                table.insert(self.m_costItemIds, chainData.costGoldId)
            end
            for i = 0, #chainData.costItemId - 1 do
                local costItemId = chainData.costItemId[i]
                local costItemNum = chainData.costItemNum[i]
                table.insert(costItemTable, { id = costItemId, count = costItemNum })
                table.insert(self.m_costItemIds, costItemId)
            end
        end
        costItemCount = #costItemTable
    end
    self.m_costItemCellCache:Refresh(costItemCount, function(cell, luaIndex)
        local costItemId = costItemTable[luaIndex].id
        local costItemNum = costItemTable[luaIndex].count
        local discount = costItemTable[luaIndex].discount
        cell.gameObject.name = costItemId
        cell.item:InitItem({ id = costItemId, count = costItemNum }, true)
        cell.item:SetExtraInfo({
            isSideTips = DeviceInfo.usingController,
        })
        if DeviceInfo.usingController then
            cell.item:SetEnableHoverTips(false)
        end
        local ownItemNum = Utils.getItemCount(costItemId, true, true)
        local isLack = ownItemNum < costItemNum
        cell.item:UpdateCountSimple(costItemNum, isLack)
        local updateCountFunc = function()
            local costNum = costItemNum * self.m_produceCount
            local ownNum = Utils.getItemCount(costItemId, true, true)
            cell.item:UpdateCountSimple(costNum, ownNum < costNum)
        end
        table.insert(self.m_costItemCountUpdateFunctions, updateCountFunc)
        cell.ownCountTxt.text = UIUtils.setCountColor(tostring(ownItemNum), isLack)
        if isLack then
            isCostEnough = false
        else
            maxProduceCount = math.min(maxProduceCount, math.floor(ownItemNum / costItemNum))
        end


        if discount and discount >= 0 and discount < 1 then
            cell.discountInfoNode.gameObject:SetActive(true)
            local discountPercent = math.floor((1 - (discount or 1)) * 100 + 0.5)
            local showDiscountTxt = string.format("-%d", discountPercent)

            cell.discountNumTxt.text = showDiscountTxt
            cell.discountNumShadownTxt.text = showDiscountTxt
        else
            cell.discountInfoNode.gameObject:SetActive(false)
        end
    end)
    self.view.middleBar.produceContent.formulaNode.naviGroup.enabled = itemInfo.isUnlocked
    self.view.middleBar.produceContent.formulaNode.controllerFocusHintNode.gameObject:SetActive(itemInfo.isUnlocked)

    self.view.rightProduceNode.equipDetails.weaponAttributeNode:InitEquipAttributeNodeByTemplateId(itemInfo.id)
    self.view.rightProduceNode.equipDetails.equipSuitNode:InitEquipSuitNode(itemInfo.id)

    local bottomNodeView = self.view.rightProduceNode.bottomNode
    bottomNodeView.gameObject:SetAllChildrenActiveIfNecessary(false)
    if itemInfo.isUnlocked then
        if isCostEnough then
            bottomNodeView.makeNode.gameObject:SetActive(true)
            local curProduceCount = 1
            if self.m_arg.resumeState then
                curProduceCount = self.m_arg.resumeState.produce.produceCount or 1
            elseif isCostItemCountChanged then
                curProduceCount = math.min(self.m_produceCount, maxProduceCount)
            end
            bottomNodeView.numberSelector:InitNumberSelector(curProduceCount, 1, maxProduceCount, function(count)
                self.m_produceCount = count
                for _, updateFunc in pairs(self.m_costItemCountUpdateFunctions) do
                    updateFunc()
                end
            end)
        else
            self.m_produceCount = 0
            bottomNodeView.shortageTip.gameObject:SetActive(true)
        end
    else
        self.m_produceCount = 0
        if itemInfo.equipFormulaData.unlockType == GEnums.EquipFormulaUnlockType.AdventureLevel then
            bottomNodeView.levelTip.gameObject:SetActive(true)
            bottomNodeView.levelTip.txtTargetLv.text = string.format(Language.LUA_EQUIP_PRODUCE_ADVENTURE_LEVEL_LOCKED_FORMAT,
                itemInfo.equipFormulaData.unlockValue)
            bottomNodeView.levelTip.txtCurrentLv.text = string.format(Language.LUA_EQUIP_PRODUCE_ADVENTURE_LEVEL_FORMAT,
                GameInstance.player.adventure.adventureLevelData.lv)
        elseif GO_TO_TEXT_KEY[itemInfo.equipFormulaData.unlockType] then
            bottomNodeView.gotoNode.buttonGoto.text = Language[GO_TO_TEXT_KEY[itemInfo.equipFormulaData.unlockType]]
            bottomNodeView.gotoNode.gameObject:SetActive(true)
        end
    end
end

EquipTechCtrl._UpdateWorldLevelLimitTip = HL.Method() << function(self)
    local max = GameInstance.player.adventure.currentMaxWorldLevel
    local limit = Tables.EquipTechConst.highEquipMinWorldLevel
    local curKey = self:_GetCurrentTabKey()
    local showTip = (curKey == "highLevelSuit" or curKey == "highLevelParts") and max < limit
    self.view.middleBar.unusableRemind.gameObject:SetActive(showTip)
end

EquipTechCtrl._UpdateNewChainTip = HL.Method() << function(self)
    local bHas, chainId = EquipTechUtils.HasNewChain()
    local curTabKey = self:_GetCurrentTabKey()
    local bShow = bHas and curTabKey ~= "basic" and curTabKey ~= "enhance"
    self.view.topBar.updateRemind.gameObject:SetActive(bShow)
    if not bShow then
        return
    end

    local costInfo = EquipTechUtils.GetChainCostInfo(chainId)
    local costItemId = costInfo and costInfo.costItemId or nil
    if costItemId then
        local _, ItemData = Tables.itemTable:TryGetValue(costItemId)
        if ItemData then
            self.view.topBar.iconFormula:LoadSprite(UIConst.UI_SPRITE_ITEM, ItemData.iconId)
        end
    end
end

EquipTechCtrl._OnEquipProduce = HL.Method(HL.Table) << function(self, args)
    local formulaId, equipInstIdList = unpack(args)

    local equipFormulaData = Tables.equipFormulaTable[formulaId]
    local produceCount = equipInstIdList.Count
    local items = {}
    for i = 1, produceCount do
        table.insert(items, { id = equipFormulaData.outcomeEquipId, count = 1 })
    end
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        title = Language.LUA_EQUIP_PRODUCE_SUCCESS_TITLE,
        items = items,
    })
    self:_RefreshProduceEquipInfo(self.m_selectedProduceItemInfo)
end

EquipTechCtrl._OnItemChanged = HL.Method(HL.Table) << function(self, args)
    local changedItemId2DiffCount = unpack(args)
    local needRefresh = false
    for _, itemId in pairs(self.m_costItemIds) do
        if changedItemId2DiffCount:ContainsKey(itemId) then
            needRefresh = true
            break
        end
    end
    if not needRefresh then
        return
    end
    self:_ItemCountChangRefresh()
end

EquipTechCtrl._OnWalletChanged = HL.Method(HL.Table) << function(self, args)
    local itemId = unpack(args)
    local needRefresh = false
    for _, costItemId in pairs(self.m_costItemIds) do
        if costItemId == itemId then
            needRefresh = true
            break
        end
    end
    if not needRefresh then
        return
    end
    self:_ItemCountChangRefresh()
end

EquipTechCtrl._ItemCountChangRefresh = HL.Method() << function(self)
    if self.m_selectedProduceItemInfo then
        self:_RefreshProduceEquipInfo(self.m_selectedProduceItemInfo, true)
    end
    if self.view.stateController.currentStateName == STATE_NAME.ENHANCE_MATERIAL then
        self:_RefreshEnhanceCostItem()
    end
end

EquipTechCtrl._GetEquipPackRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local luaIndex = LuaIndex(index)
    if luaIndex < 1 or luaIndex > #self.m_filteredEquipPackDataList then
        return 0
    end
    local packData = self.m_filteredEquipPackDataList[luaIndex]
    if not packData then
        return 0
    end
    local hasRedDot, redDotType = RedDotManager:GetRedDotState("EquipPack", packData.equipPackData.packId)
    if hasRedDot then
        if redDotType == UIConst.RED_DOT_TYPE.Normal then
            return redDotType
        else
            return UIConst.RED_DOT_TYPE.New
        end
    else
        return 0
    end
end





EquipTechCtrl.m_isEquipSlotTabInited = HL.Field(HL.Boolean) << false

EquipTechCtrl.m_partType = HL.Field(HL.Any)

EquipTechCtrl.m_selectedEnhanceEquipItemInfo = HL.Field(HL.Table)

EquipTechCtrl.m_selectedEnhanceEquipInstId = HL.Field(HL.Number) << 0

EquipTechCtrl.m_selectedAttrShowInfoList = HL.Field(HL.Table)

EquipTechCtrl.m_enhanceAttrCellCache = HL.Field(HL.Forward("UIListCache"))

EquipTechCtrl.m_selectedAttrShowInfoIndex = HL.Field(HL.Number) << 0

EquipTechCtrl.m_enhanceTargetTypeCellCache = HL.Field(HL.Forward("UIListCache"))

EquipTechCtrl.m_firstCanEnhancedAttrCell = HL.Field(HL.Table)

EquipTechCtrl._EnterEnhanceTarget = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    FilterUtils.updateCharInstIdIndex()
    self:_RemoveEnhanceTargetNaviGroup()

    self:_ClearProduceEquipSelection()
    self.m_selectedEnhanceEquipItemInfo = nil
    self.m_selectedEnhanceEquipInstId = resumeState and resumeState.selectedEquipInstId or 0
    self:_SendFormulaRead()
    self.view.stateController:SetState(STATE_NAME.ENHANCE_TARGET)
    self.view.middleBar.centerItem.btnExplain.gameObject:SetActive(true)

    self:_InitEquipSlotTab()
    local targetTabIndex = resumeState and resumeState.equipSlotTabIndex or 1
    local targetTabCell = self.m_enhanceTargetTypeCellCache:GetItem(targetTabIndex)
    local targetToggle = targetTabCell and targetTabCell.toggle or self.m_enhanceTargetTypeCellCache:GetItem(1).toggle
    if targetToggle.isOn then
        local config = EQUIP_SLOT_TAB_CONFIG[targetTabIndex] or EQUIP_SLOT_TAB_CONFIG[1]
        self.m_partType = config.partType
        self:_RefreshEnhanceTargetList(resumeState)
    else
        targetToggle:SetIsOnWithoutNotify(true)
        local config = EQUIP_SLOT_TAB_CONFIG[targetTabIndex] or EQUIP_SLOT_TAB_CONFIG[1]
        self.m_partType = config.partType
        self:_RefreshEnhanceTargetList(resumeState)
    end

    self:_UpdateNewChainTip()
    self:_UpdateWorldLevelLimitTip()
    self.m_equipTechSystem:NotifyHighLevelEquipTabChanged(CS.Beyond.Gameplay.EquipTechSystem.EHighLevelEquipTabType.Enhance:GetHashCode())
end

EquipTechCtrl._RemoveEnhanceTargetNaviGroup = HL.Method() << function(self)
    if DeviceInfo.usingController then
        InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.leftBarEnhance.commonItemList.view.scrollRect.naviGroup)
    end
end

EquipTechCtrl._InitEquipSlotTab = HL.Method() << function(self)
    if self.m_isEquipSlotTabInited then
        return
    end

    local tabCellCache = UIUtils.genCellCache(self.view.leftBarEnhance.typesNode.typeCell)
    tabCellCache:Refresh(#EQUIP_SLOT_TAB_CONFIG, function(cell, index)
        local config = EQUIP_SLOT_TAB_CONFIG[index]
        if config.partType then
            local iconName = UIConst.EQUIP_TYPE_TO_ICON_NAME[config.partType]
            cell.dimIcon:LoadSprite(UIConst.UI_SPRITE_EQUIP, iconName)
            cell.lightIcon:LoadSprite(UIConst.UI_SPRITE_EQUIP, iconName)
        end
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self.m_partType = config.partType
                self.m_selectedEnhanceEquipInstId = 0
                self:_RefreshEnhanceTargetList()
            end
        end)
    end)
    self.m_enhanceTargetTypeCellCache = tabCellCache
    self.m_isEquipSlotTabInited = true
end

EquipTechCtrl._RefreshEnhanceTargetList = HL.Method(HL.Opt(HL.Any, HL.Boolean)) << function(self, resumeState, skipGraduallyShow)
    local jumpEquipId = self.m_jumpEquipId
    self.m_jumpEquipId = ""
    local jumpEquipInstId = self.m_jumpEquipInstId
    self.m_jumpEquipInstId = 0

    local itemListArgs = {
        listType = UIConst.COMMON_ITEM_LIST_TYPE.EQUIP_TECH_EQUIP_ENHANCE,
        onClickItem = function(args)
            if self.m_selectedEnhanceEquipItemInfo and self.m_selectedEnhanceEquipItemInfo.instId == args.itemInfo.instId then
                return
            end
            self.m_selectedEnhanceEquipItemInfo = args.itemInfo
            if args.realClick then
                self.view.topNode.animationWrapper:ClearTween(false)
                self.view.topNode.animationWrapper:PlayOutAnimation(function()
                    self.view.topNode.animationWrapper:PlayInAnimation()
                end)
                self.view.rightBarEnhanceAttr.animationWrapper:ClearTween(false)
                self.view.rightBarEnhanceAttr.animationWrapper:PlayOutAnimation(function()
                    self:_RefreshEnhancedEquip()
                    self.view.rightBarEnhanceAttr.animationWrapper:PlayInAnimation()
                end)
            else
                self:_RefreshEnhancedEquip()
            end
        end,
        onFilterNone = function()
            self.m_selectedEnhanceEquipItemInfo = nil
            self:_RefreshEnhancedEquip()
        end,
        setItemSelected = function(cell, isSelected)
            cell.stateController:SetState(isSelected and "selected" or "normal")
        end,
        getItemBtn = function(cell)
            return cell.btn
        end,
        refreshItemAddOn = function(cell, itemInfo)
            cell.gameObject.name = "TargetCell" .. tostring(itemInfo.indexId)

            if itemInfo.id == jumpEquipId then
                cell.gameObject.name = itemInfo.id
            end
            cell.equipEnhanceLevelCellCache = cell.equipEnhanceLevelCellCache or UIUtils.genCellCache(cell.equipEnhanceLevelNode)
            cell.equipEnhanceLevelCellCache:Refresh(#itemInfo.equipData.displayAttrModifiers, function(enhanceLevelCell, index)
                local attrMod = itemInfo.equipData.displayAttrModifiers[CSIndex(index)]
                enhanceLevelCell:InitEquipEnhanceLevelNode({
                    equipInstId = itemInfo.instId,
                    attrIndex = attrMod.enhancedAttrIndex,
                })
            end)

            cell.equipItem:InitEquipItem({
                equipInstId = itemInfo.instId,
                noInitItem = true,
                itemInteractable = false,
            })
        end,
        filter_equipType = self.m_partType,
        defaultSelectedIndex = 1,
        selectedIndexId = jumpEquipInstId > 0 and jumpEquipInstId or self.m_selectedEnhanceEquipInstId,
        selectedItemId = jumpEquipId,
        skipGraduallyShow = skipGraduallyShow,


        suppressAutoNaviTarget = DeviceInfo.usingController and resumeState ~= nil,
    }
    self.view.leftBarEnhance.commonItemList:InitCommonItemList(itemListArgs)
    self:_RestoreCommonItemListResumeState(self.view.leftBarEnhance.commonItemList, resumeState,
        resumeState and resumeState.selectedEquipInstId or 0, false)
    if DeviceInfo.usingController then
        local commonItemList = self.view.leftBarEnhance.commonItemList
        local scrollRect = commonItemList.view and commonItemList.view.scrollRect or nil
        local naviGroup = scrollRect and scrollRect.naviGroup or nil
        local selectedCell = commonItemList.GetCurSelectedItemCell and commonItemList:GetCurSelectedItemCell() or nil
        local selectedBtn = selectedCell and selectedCell.btn or nil
        if naviGroup and selectedBtn then

            self:SetNaviTarget(selectedBtn)
        end
    end
end

EquipTechCtrl._RefreshEnhancedEquip = HL.Method() << function(self)
    local isEmpty = self.m_selectedEnhanceEquipItemInfo == nil
    self.m_selectedEnhanceEquipInstId = isEmpty and 0 or self.m_selectedEnhanceEquipItemInfo.instId
    self.view.middleBar.centerItem.gameObject:SetActive(true)
    self.view.middleBar.centerItem.emptyState.gameObject:SetActive(isEmpty)
    self.view.middleBar.centerItem.imgEquip.gameObject:SetActive(not isEmpty)
    local showBtnExplain = not isEmpty and (self.view.stateController.currentStateName == STATE_NAME.ENHANCE_TARGET or not DeviceInfo.usingController)
    self.view.middleBar.centerItem.btnExplain.gameObject:SetActive(showBtnExplain)
    self.view.topNode.gameObject:SetActive(not isEmpty)
    self.view.rightBarEnhanceAttr.emptyNode.gameObject:SetActive(isEmpty)
    self.view.rightBarEnhanceAttr.attrNode.gameObject:SetActive(not isEmpty)
    self.view.middleBar.targetContent.equipInfo.gameObject:SetActive(not isEmpty)
    self.view.middleBar.targetContent.txtEquip.gameObject:SetActive(not isEmpty)
    if isEmpty then
        return
    end

    local equipInstData = EquipTechUtils.getEquipInstData(self.m_selectedEnhanceEquipInstId)

    local _, itemData = Tables.itemTable:TryGetValue(self.m_selectedEnhanceEquipItemInfo.id)
    if itemData then
        self.view.middleBar.centerItem.imgEquip:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
    end
    self.view.middleBar.targetContent.equipInfo:InitEquipTechEquipInfo(self.m_selectedEnhanceEquipItemInfo.id)
    self.view.middleBar.materialContent.equipInfo:InitEquipTechEquipInfo(self.m_selectedEnhanceEquipItemInfo.id)
    self.view.middleBar.centerItem.btnExplain.onClick:RemoveAllListeners()
    self.view.middleBar.centerItem.btnExplain.onClick:AddListener(function()
        self:_SetCenterEquipSelected(true)
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = self.m_selectedEnhanceEquipItemInfo.id,
            instId = self.m_selectedEnhanceEquipInstId,
            transform = self.view.middleBar.centerItem.imgEquip.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.RightMid,
            onClose = function()
                if self.m_isClosed then
                    return
                end
                self:_SetCenterEquipSelected(false)
            end
        })
    end)

    local _, primaryAttrs, nonPrimaryAttrs = CharInfoUtils.getEquipShowAttributes(self.m_selectedEnhanceEquipInstId)
    self.m_selectedAttrShowInfoList = lume.concat(primaryAttrs, nonPrimaryAttrs)
    local attrCount = self.m_selectedAttrShowInfoList and #self.m_selectedAttrShowInfoList or 0
    self.m_firstCanEnhancedAttrCell = nil
    self.m_enhanceAttrCellCache = self.m_enhanceAttrCellCache or UIUtils.genCellCache(self.view.rightBarEnhanceAttr.enhanceAttrCell)
    self.m_enhanceAttrCellCache:Refresh(attrCount, function(cell, luaIndex)
        cell.gameObject.name = tostring(luaIndex)
        local attrShowInfo = self.m_selectedAttrShowInfoList[luaIndex]
        local isEnhanced = equipInstData:IsAttrEnhanced(attrShowInfo.enhancedAttrIndex)
        local isMaxEnhanced = equipInstData:IsAttrMaxEnhanced(attrShowInfo.enhancedAttrIndex)
        cell.stateController:SetState(isMaxEnhanced and "max" or "normal")
        cell.txtName.text = attrShowInfo.showName
        cell.txtValue.text = EquipTechUtils.getAttrShowValueText(attrShowInfo)
        local color = isEnhanced and self.view.config.COLOR_ENHANCED or self.view.config.COLOR_NORMAL
        cell.txtName.color = color
        cell.txtValue.color = color
        cell.equipEnhanceLevelNode:InitEquipEnhanceLevelNode({
            equipInstId = self.m_selectedEnhanceEquipInstId,
            attrIndex = attrShowInfo.enhancedAttrIndex,
        })
        cell.btnEnhance.onClick:RemoveAllListeners()
        if not isMaxEnhanced then
            cell.btnEnhance.onClick:AddListener(function()
                self.m_selectedAttrShowInfoIndex = luaIndex
                self.m_lastEnhanceAttrCell = cell
                self:_EnterEnhanceMaterial()
            end)
            if not self.m_firstCanEnhancedAttrCell then
                self.m_firstCanEnhancedAttrCell = cell
            end
        end
    end)

    self.view.topNode.txtCount.text = string.format("%d/%d", equipInstData:GetEnhanceLevel(),
        attrCount * Tables.equipConst.maxAttrEnhanceLevel)
end

EquipTechCtrl._SetCenterEquipSelected = HL.Method(HL.Boolean) << function(self, isSelected)
    self.view.middleBar.centerItem.btnExplain.gameObject:SetActive(not isSelected and
        (self.view.stateController.currentStateName == STATE_NAME.ENHANCE_TARGET or
            self.view.stateController.currentStateName == STATE_NAME.ENHANCE_MATERIAL))
    self.view.middleBar.centerItem.selectedBG.gameObject:SetActive(isSelected)
end





EquipTechCtrl.m_nextLevelAttrShowValue = HL.Field(HL.String) << ""

EquipTechCtrl.m_isCostItemCountEnough = HL.Field(HL.Boolean) << false

EquipTechCtrl.m_lastEnhanceAttrCell = HL.Field(HL.Table)

EquipTechCtrl.m_isEnhanceMaterialItemTipsMode = HL.Field(HL.Boolean) << false

EquipTechCtrl.m_closeEnhanceMaterialItemTipsBindingId = HL.Field(HL.Number) << 0

EquipTechCtrl.m_getMaterialGroupCell = HL.Field(HL.Function)

EquipTechCtrl.m_materialGroups = HL.Field(HL.Table)

EquipTechCtrl.m_selectedMaterialInstIdList = HL.Field(HL.Table)

EquipTechCtrl.m_selectedMaterialInstId2Index = HL.Field(HL.Table)

EquipTechCtrl.m_enhanceGuaranteeFailedCount = HL.Field(HL.Number) << 0

EquipTechCtrl.m_enhanceGuaranteeMaxFailedCount = HL.Field(HL.Number) << 0

EquipTechCtrl.m_sliderPreviewTween = HL.Field(HL.Any)

EquipTechCtrl.m_enhanceCostOwnItemCount = HL.Field(HL.Number) << 0

EquipTechCtrl.m_enhanceCostPerItemCount = HL.Field(HL.Number) << 0

EquipTechCtrl.m_isEnhanceMaterialNaviTargetSet = HL.Field(HL.Boolean) << false

EquipTechCtrl.m_materialFastScrollIndex = HL.Field(HL.Number) << 0

EquipTechCtrl._BackToEnhanceTarget = HL.Method() << function(self)
    self.view.stateController:SetState(STATE_NAME.ENHANCE_TARGET)
    self.view.leftBarEnhance.layoutElement.ignoreLayout = false
    self.view.leftBarEnhance.inputGroup.enabled = true
    InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.selectMaterials.listNodeNaviGroup)
    self.view.rightBarEnhanceAttr.naviGroup:ManuallyFocus()
    if self.m_lastEnhanceAttrCell then
        local naviTarget
        if self.m_lastEnhanceAttrCell.btnEnhance.gameObject.activeInHierarchy then
            naviTarget = self.m_lastEnhanceAttrCell.btnEnhance
        else
            naviTarget = self.m_lastEnhanceAttrCell.accomplishNode
        end
        self:SetNaviTarget(naviTarget)
        self.m_lastEnhanceAttrCell = nil
    end
end

EquipTechCtrl._EnterEnhanceMaterial = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    self.view.stateController:SetState(STATE_NAME.ENHANCE_MATERIAL)
    self:_RefreshEnhanceMaterialList(resumeState)
    self:_RefreshEnhanceInfo(true)
    self.view.middleBar.bottomNode.btnMake.onClick:RemoveAllListeners()
    self.view.middleBar.bottomNode.btnMake.onClick:AddListener(function()
        self:_OnEnhanceClicked()
    end)
    self.view.middleBar.bottomNode.clearBtn.onClick:RemoveAllListeners()
    self.view.middleBar.bottomNode.clearBtn.onClick:AddListener(function()
        self:_ClearSelectedEnhanceMaterials()
    end)
    self.view.middleBar.bottomNode.selectBtn.onClick:RemoveAllListeners()
    self.view.middleBar.bottomNode.selectBtn.onClick:AddListener(function()
        self:_AutoSelectEnhanceMaterials(true)
    end)
    self.view.middleBar.centerItem.btnExplain.gameObject:SetActive(not DeviceInfo.usingController)
    self.view.middleBar.centerBg:PlayInAnimation()
end

EquipTechCtrl._ClearSelectedEnhanceMaterials = HL.Method() << function(self)
    if not self.m_selectedMaterialInstIdList or #self.m_selectedMaterialInstIdList == 0 then
        return
    end
    self.m_selectedMaterialInstIdList = {}
    self.m_selectedMaterialInstId2Index = {}
    self:_UpdateSelectedMaterialCellsIndex()
    self:_RefreshEnhanceInfo()
end


EquipTechCtrl._GetEnhanceIngredientHeadroom = HL.Method().Return(HL.Number, HL.Number, HL.Number) << function(self)
    local list = self.m_selectedMaterialInstIdList
    local selectedCount = list and #list or 0
    local remainingByBatchLimit = Tables.equipConst.maxEnhanceIngredientCount - selectedCount
    local remainingByGuarantee = math.huge
    local maxFailed = self.m_enhanceGuaranteeMaxFailedCount
    if maxFailed > 0 then
        remainingByGuarantee = (maxFailed - self.m_enhanceGuaranteeFailedCount) - selectedCount + 1
    end
    local remainingByCostBudget = math.huge
    local costPerUse = self.m_enhanceCostPerItemCount
    if costPerUse > 0 then
        remainingByCostBudget = math.floor(self.m_enhanceCostOwnItemCount / costPerUse) - selectedCount
    end
    return remainingByBatchLimit, remainingByGuarantee, remainingByCostBudget
end

EquipTechCtrl._GetMaxAdditionalEnhanceIngredientCount = HL.Method().Return(HL.Number) << function(self)
    local remainingByBatchLimit, remainingByGuarantee, remainingByCostBudget = self:_GetEnhanceIngredientHeadroom()
    return math.max(0, math.min(remainingByBatchLimit, remainingByGuarantee, remainingByCostBudget))
end


EquipTechCtrl._GetPickableUnenhancedEnhanceMaterialCount = HL.Method().Return(HL.Number) << function(self)
    if not self.m_materialGroups then
        return 0
    end
    self.m_selectedMaterialInstId2Index = self.m_selectedMaterialInstId2Index or {}
    local n = 0
    for _, group in ipairs(self.m_materialGroups) do
        for _, itemInfo in ipairs(group.itemList) do
            local equipInstData = itemInfo.equipInstData
            if equipInstData and not equipInstData:HasAnyEnhanceRecord() and not self.m_selectedMaterialInstId2Index[itemInfo.instId] then
                n = n + 1
            end
        end
    end
    return n
end


EquipTechCtrl._AutoSelectEnhanceMaterials = HL.Method(HL.Opt(HL.Boolean)) << function(self, fromUserClick)
    if self.view.stateController.currentStateName ~= STATE_NAME.ENHANCE_MATERIAL then
        return
    end
    if not self.m_materialGroups or #self.m_materialGroups == 0 then
        return
    end
    local isUserClick = fromUserClick == true
    self.m_selectedMaterialInstIdList = self.m_selectedMaterialInstIdList or {}
    self.m_selectedMaterialInstId2Index = self.m_selectedMaterialInstId2Index or {}
    self:_RefreshEnhanceInfo()
    local pickableUnenhanced = self:_GetPickableUnenhancedEnhanceMaterialCount()
    if isUserClick and self.m_enhanceCostPerItemCount > 0 and pickableUnenhanced > 0 and self.m_enhanceCostOwnItemCount <= 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_EQUIP_ENHANCE_INGREDIENT_COST_NOT_ENOUGH_TOAST)
        return
    end
    local toAdd = self:_GetMaxAdditionalEnhanceIngredientCount()
    if toAdd <= 0 then
        return
    end
    local added = 0
    local firstFilledInstId = 0
    for _, group in ipairs(self.m_materialGroups) do
        for _, itemInfo in ipairs(group.itemList) do
            if added >= toAdd then
                break
            end
            local instId = itemInfo.instId
            local equipInstData = itemInfo.equipInstData
            local isUnEnhancedMaterial = equipInstData and not equipInstData:HasAnyEnhanceRecord()
            if isUnEnhancedMaterial and not self.m_selectedMaterialInstId2Index[instId] then
                if firstFilledInstId == 0 then
                    firstFilledInstId = instId
                end
                table.insert(self.m_selectedMaterialInstIdList, instId)
                self.m_selectedMaterialInstId2Index[instId] = #self.m_selectedMaterialInstIdList
                added = added + 1
            end
        end
        if added >= toAdd then
            break
        end
    end
    if isUserClick and added == 0 and toAdd > 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_EQUIP_ENHANCE_AUTO_SELECT_NO_UNENHANCED_TOAST)
    end
    self:_UpdateSelectedMaterialCellsIndex()
    self:_RefreshEnhanceInfo()
    if isUserClick and added > 0 and firstFilledInstId ~= 0 then
        self.m_isEnhanceMaterialNaviTargetSet = false
        local luaJump = self:_FindEnhanceMaterialJumpListLuaIndexByInstId(firstFilledInstId)
        self.m_materialFastScrollIndex = CSIndex(luaJump)
        local range = self.view.selectMaterials.itemList:GetRangeInView()
        local inView = range.x >= 0 and range.y >= 0 and
            self.m_materialFastScrollIndex >= range.x and self.m_materialFastScrollIndex <= range.y
        if not inView then
            self.view.selectMaterials.itemList:ScrollToIndex(self.m_materialFastScrollIndex, true)
        end
        if DeviceInfo.usingController then
            local itemBtn = self:_FindEnhanceMaterialItemBtnByInstId(firstFilledInstId)
            if itemBtn then
                self:SetNaviTarget(itemBtn)
                self.m_isEnhanceMaterialNaviTargetSet = true
            end
        end
    end
end

EquipTechCtrl._RefreshEnhanceMaterialList = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    local _, primaryAttrs, nonPrimaryAttrs = CharInfoUtils.getEquipTemplateShowAttributes(self.m_selectedEnhanceEquipItemInfo.id)
    local selectedTemplateAttrShowInfoList = lume.concat(primaryAttrs, nonPrimaryAttrs)

    self.m_selectedMaterialInstIdList = resumeState and resumeState.selectedMaterialInstIdList or {}
    self.m_selectedMaterialInstId2Index = resumeState and resumeState.selectedMaterialInstId2Index or {}

    self.m_materialGroups = EquipTechUtils.getEquipEnhanceMaterialsGroups(self.m_selectedEnhanceEquipItemInfo.partType,
        selectedTemplateAttrShowInfoList[self.m_selectedAttrShowInfoIndex], self.m_selectedEnhanceEquipInstId)

    local isEmpty = not self.m_materialGroups or #self.m_materialGroups == 0
    self.view.selectMaterials.emptyNode.gameObject:SetActive(isEmpty)
    self.view.selectMaterials.itemList.gameObject:SetActive(not isEmpty)
    if isEmpty then
        self.m_jumpMaterialEquipId = ""
        return
    end

    for _, group in ipairs(self.m_materialGroups) do
        group.uiCellCount = math.ceil(#group.itemList / MATERIAL_CELL_COUNT_PER_ROW) + 1
    end
    self.m_getMaterialGroupCell = self.m_getMaterialGroupCell or UIUtils.genCachedCellFunction(self.view.selectMaterials.itemList)
    self.view.selectMaterials.itemList.getCellSize = function(csIndex)
        local luaIndex = LuaIndex(csIndex)
        local cellCount = 0
        for _, group in pairs(self.m_materialGroups) do
            local nextCellCount = cellCount + group.uiCellCount
            if luaIndex == cellCount + 1 then
                return MATERIAL_CELL_TITLE_ROW_SIZE
            elseif luaIndex > cellCount and luaIndex <= nextCellCount then
                return MATERIAL_CELL_ITEM_ROW_SIZE
            end
            cellCount = nextCellCount
        end
    end
    self.view.selectMaterials.itemList.onUpdateCell:RemoveAllListeners()
    self.view.selectMaterials.itemList.onUpdateCell:AddListener(function(object, csIndex)
        local luaIndex = LuaIndex(csIndex)
        local targetGroup
        local startIndex, endIndex, cellCount = 0, 0, 0
        for _, group in ipairs(self.m_materialGroups) do
            local nextCellCount = cellCount + group.uiCellCount
            if luaIndex == cellCount + 1 then

                targetGroup = group
                break
            elseif luaIndex > cellCount and luaIndex <= nextCellCount then
                targetGroup = group
                local itemCellCountInGroup = luaIndex - cellCount - 1
                startIndex = (itemCellCountInGroup - 1) * MATERIAL_CELL_COUNT_PER_ROW + 1
                endIndex = math.min(startIndex + MATERIAL_CELL_COUNT_PER_ROW - 1, #group.itemList)
                break
            end
            cellCount = nextCellCount
        end
        local cell = self.m_getMaterialGroupCell(object)
        cell.gameObject.name = "MaterialGroupCell"
        self:_UpdateEnhanceMaterialGroupCell(targetGroup, cell, startIndex, endIndex)
    end)
    local allCellCount = 0
    for _, group in ipairs(self.m_materialGroups) do
        allCellCount = allCellCount + group.uiCellCount
    end
    self.m_materialFastScrollIndex = 0
    if not string.isEmpty(self.m_jumpMaterialEquipId) then
        self.m_materialFastScrollIndex = CSIndex(self:_FindEnhanceMaterialJumpListLuaIndex(self.m_jumpMaterialEquipId))
    end
    self.m_isEnhanceMaterialNaviTargetSet = false
    self.view.selectMaterials.itemList:UpdateCount(allCellCount, self.m_materialFastScrollIndex)
    self.m_jumpMaterialEquipId = ""
    if resumeState and resumeState.materialListScroll then
        local s = resumeState.materialListScroll
        local scrollRect = self.view.selectMaterials.itemList:GetComponent(typeof(CS.Beyond.UI.UIScrollRect))
        if scrollRect then
            scrollRect.verticalNormalizedPosition = s.verticalNormalizedPosition
        end
        resumeState.materialListScroll = nil
    end
    if resumeState and DeviceInfo.usingController then
        self:_StartCoroutine(function()

            coroutine.step()
            coroutine.step()
            self:_SetSelectMaterialsItemListNaviToFirstFullyVisibleCell()
        end)
    end
end


EquipTechCtrl._SetSelectMaterialsItemListNaviToFirstFullyVisibleCell = HL.Method() << function(self)
    if not DeviceInfo.usingController or not self.m_getMaterialGroupCell then
        return
    end
    local itemList = self.view.selectMaterials.itemList
    if not itemList then
        return
    end
    local scrollRect = itemList:GetComponent(typeof(CS.Beyond.UI.UIScrollRect))
    local viewportRt = scrollRect and scrollRect.viewport
    if not viewportRt then
        return
    end

    local range = itemList:GetRangeInView()
    if range.x < 0 or range.y < 0 then
        return
    end
    local function _isRectTransformFullyInsideViewport(cellRt, viewportRt)
        if not cellRt or not viewportRt then
            return false
        end
        local arr = CS.System.Array.CreateInstance(typeof(CS.UnityEngine.Vector3), 4)
        cellRt:GetWorldCorners(arr)
        for i = 0, 3 do
            local p = viewportRt:InverseTransformPoint(arr[i])
            if not viewportRt.rect:Contains(CS.UnityEngine.Vector2(p.x, p.y)) then
                return false
            end
        end
        return true
    end
    local function tryFocusGroupCell(groupCell, requireFull)
        if not groupCell or groupCell.isTitle or not groupCell.itemCellCache then
            return false
        end
        if requireFull and not _isRectTransformFullyInsideViewport(groupCell.rectTransform, viewportRt) then
            return false
        end
        local itemCell = groupCell.itemCellCache:Get(1)
        if itemCell and itemCell.btn then
            self:SetNaviTarget(itemCell.btn)
            self.m_isEnhanceMaterialNaviTargetSet = true
            return true
        end
        return false
    end
    for csIdx = range.x, range.y do
        local cellGo = itemList:Get(csIdx)
        if cellGo and tryFocusGroupCell(self.m_getMaterialGroupCell(cellGo), true) then
            return
        end
    end
    for csIdx = range.x, range.y do
        local cellGo = itemList:Get(csIdx)
        if cellGo and tryFocusGroupCell(self.m_getMaterialGroupCell(cellGo), false) then
            return
        end
    end
end


EquipTechCtrl._FindEnhanceMaterialJumpListLuaIndex = HL.Method(HL.String).Return(HL.Number) << function(self, templateId)
    if string.isEmpty(templateId) or not self.m_materialGroups then
        return 1
    end
    local cellCount = 0
    for _, group in ipairs(self.m_materialGroups) do
        if group.uiCellCount and group.itemList then
            for itemIdx, itemInfo in ipairs(group.itemList) do
                if itemInfo.id == templateId then
                    local rowInGroup = math.ceil(itemIdx / MATERIAL_CELL_COUNT_PER_ROW)
                    return cellCount + 1 + rowInGroup
                end
            end
            cellCount = cellCount + group.uiCellCount
        end
    end
    return 1
end


EquipTechCtrl._FindEnhanceMaterialJumpListLuaIndexByInstId = HL.Method(HL.Number).Return(HL.Number) << function(self, instId)
    if not instId or instId == 0 or not self.m_materialGroups then
        return 1
    end
    local cellCount = 0
    for _, group in ipairs(self.m_materialGroups) do
        if group.uiCellCount and group.itemList then
            for itemIdx, itemInfo in ipairs(group.itemList) do
                if itemInfo.instId == instId then
                    local rowInGroup = math.ceil(itemIdx / MATERIAL_CELL_COUNT_PER_ROW)
                    return cellCount + 1 + rowInGroup
                end
            end
            cellCount = cellCount + group.uiCellCount
        end
    end
    return 1
end


EquipTechCtrl._FindEnhanceMaterialItemBtnByInstId = HL.Method(HL.Number).Return(HL.Any) << function(self, instId)
    if not instId or instId == 0 or not self.m_getMaterialGroupCell then
        return nil
    end
    local list = self.view.selectMaterials.itemList
    if not list then
        return nil
    end
    for i = 1, list.count do
        local cellGo = list:Get(CSIndex(i))
        if cellGo then
            local groupCell = self.m_getMaterialGroupCell(cellGo)
            if groupCell and not groupCell.isTitle and groupCell.itemCellCache then
                for j = 1, groupCell.itemCellCache:GetCount() do
                    local itemCell = groupCell.itemCellCache:Get(j)
                    if itemCell and itemCell.itemInfo and itemCell.itemInfo.instId == instId then
                        return itemCell.btn
                    end
                end
            end
        end
    end
    return nil
end


EquipTechCtrl._CanAddEnhanceIngredient = HL.Method().Return(HL.Boolean) << function(self)
    local remainingByBatchLimit, remainingByGuarantee, remainingByCostBudget = self:_GetEnhanceIngredientHeadroom()
    if remainingByBatchLimit < 1 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_EQUIP_ENHANCE_INGREDIENT_BATCH_LIMIT_TOAST)
        return false
    end
    if remainingByGuarantee < 1 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_EQUIP_ENHANCE_INGREDIENT_GUARANTEED_TOAST)
        return false
    end
    if remainingByCostBudget < 1 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_EQUIP_ENHANCE_INGREDIENT_COST_NOT_ENOUGH_TOAST)
        return false
    end
    return true
end

EquipTechCtrl._UpdateEnhanceMaterialGroupCell = HL.Method(HL.Table, HL.Table, HL.Number, HL.Number) << function(self, group, cell, startIndex, endIndex)
    local isTitle = startIndex == 0 and endIndex == 0
    cell.isTitle = isTitle
    cell.titleNode.gameObject:SetActive(isTitle)
    cell.layout.gameObject:SetActive(not isTitle)
    if isTitle then
        cell.titleTxt.text = group.title
        return
    end
    local itemCount = endIndex - startIndex + 1
    cell.itemCellCache = cell.itemCellCache or UIUtils.genCellCache(cell.enhanceMaterialCell)

    cell.itemCellCache:Refresh(itemCount, function(cell, index)
        local itemInfo = group.itemList[startIndex + index - 1]

        if not string.isEmpty(self.m_jumpMaterialEquipId) and itemInfo.id == self.m_jumpMaterialEquipId then
            cell.gameObject.name = itemInfo.id
            self.m_jumpMaterialEquipId = ""
            if DeviceInfo.usingController then
                self:SetNaviTarget(cell.btn)
                self.m_isEnhanceMaterialNaviTargetSet = true
            end
        end
        cell.itemInfo = itemInfo
        cell.equipItem:InitEquipItem({
            equipInstId = itemInfo.instId,
            itemInteractable = false,
        })
        cell.btnSymbol.onClick:RemoveAllListeners()
        cell.btnSymbol.onClick:AddListener(function()
            if DeviceInfo.usingController and not self.m_isEnhanceMaterialItemTipsMode then
                self.m_isEnhanceMaterialItemTipsMode = true
                self.m_closeEnhanceMaterialItemTipsBindingId = self:BindInputPlayerAction(
                    "common_back", function()
                        self:_CloseEnhanceMaterialItemTips()
                        InputManagerInst:ToggleGroup(cell.btnSymbol.groupId, true)
                    end, self.view.selectMaterials.itemListInputGroup.groupId)
                Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
                    panelId = PANEL_ID,
                    isGroup = true,
                    id = self.view.selectMaterials.itemListInputGroup.groupId,
                    hintPlaceholder = self.view.controllerHintPlaceholder,
                    rectTransform = self.view.selectMaterials.itemListRectTransform,
                    noHighlight = true,
                })
                InputManagerInst:ToggleGroup(cell.btnSymbol.groupId, false)
            end
            self:_ShowEnhanceMaterialItemTips(itemInfo)
        end)
        cell.txtNormal.gameObject:SetActive(itemInfo.equipEnhanceSuccessProb == EquipTechConst.EEquipEnhanceSuccessProb.Normal)
        cell.txtHigh.gameObject:SetActive(itemInfo.equipEnhanceSuccessProb == EquipTechConst.EEquipEnhanceSuccessProb.High)
        cell.btn.onIsNaviTargetChanged = function(isTarget)
            InputManagerInst:ToggleGroup(cell.btnSymbol.groupId, isTarget and not self.m_isEnhanceMaterialItemTipsMode)
            if isTarget and self.m_isEnhanceMaterialItemTipsMode then
                self:_ShowEnhanceMaterialItemTips(itemInfo)
            end
        end
        cell.btn.onClick:RemoveAllListeners()
        cell.btn.onClick:AddListener(function()
            local instId = itemInfo.instId
            if self.m_selectedMaterialInstId2Index[instId] then
                self.m_selectedMaterialInstId2Index[instId] = nil
                local changeIndex
                for i, instId in ipairs(self.m_selectedMaterialInstIdList) do
                    if instId == itemInfo.instId then
                        table.remove(self.m_selectedMaterialInstIdList, i)
                        changeIndex = i
                        break
                    end
                end
                for i = changeIndex, #self.m_selectedMaterialInstIdList do
                    local instId = self.m_selectedMaterialInstIdList[i]
                    self.m_selectedMaterialInstId2Index[instId] = i
                end
                self:_UpdateMaterialCellSelection(cell)
                self:_UpdateSelectedMaterialCellsIndex()
            else
                if not self:_CanAddEnhanceIngredient() then
                    return
                end
                table.insert(self.m_selectedMaterialInstIdList, instId)
                self.m_selectedMaterialInstId2Index[instId] = #self.m_selectedMaterialInstIdList
                self:_UpdateMaterialCellSelection(cell)
            end
            self:_RefreshEnhanceInfo()
        end)
        if DeviceInfo.usingController then
            InputManagerInst:ToggleGroup(cell.btnSymbol.groupId, false)
        end
        self:_UpdateMaterialCellSelection(cell)
    end)
    if DeviceInfo.usingController and not self.m_isEnhanceMaterialNaviTargetSet and self.m_materialFastScrollIndex <= 0 then
        self:SetNaviTarget(cell.itemCellCache:Get(1).btn)
        self.m_isEnhanceMaterialNaviTargetSet = true
    end
end

EquipTechCtrl._UpdateMaterialCellSelection = HL.Method(HL.Table) << function(self, cell)
    cell.isSelected = self.m_selectedMaterialInstId2Index[cell.itemInfo.instId] ~= nil
    cell.stateController:SetState(cell.isSelected and "selected" or "normal")
    if cell.isSelected then
        cell.selectedIndex = self.m_selectedMaterialInstId2Index[cell.itemInfo.instId]
        cell.numTxt.text = tostring(cell.selectedIndex)
    end
    if DeviceInfo.usingController then
        cell.btn.customBindingViewLabelText = cell.isSelected and Language.LUA_EQUIP_ENHANCE_MATERIAL_UNSELECT or Language.LUA_EQUIP_ENHANCE_MATERIAL_SELECT
    end
end

EquipTechCtrl._UpdateSelectedMaterialCellsIndex = HL.Method() << function(self)
    for i = 1, self.view.selectMaterials.itemList.count do
        local cellGo = self.view.selectMaterials.itemList:Get(CSIndex(i))
        if cellGo then
            local cell = self.m_getMaterialGroupCell(cellGo)
            if cell and not cell.isTitle and cell.itemCellCache then
                for i = 1, cell.itemCellCache:GetCount() do
                    local itemCell = cell.itemCellCache:Get(i)
                    if itemCell then
                        local selectedIndex = self.m_selectedMaterialInstId2Index[itemCell.itemInfo.instId]
                        local isSelected = selectedIndex ~= nil
                        if itemCell.isSelected ~= isSelected then
                            self:_UpdateMaterialCellSelection(itemCell)
                        elseif itemCell.isSelected and itemCell.selectedIndex ~= selectedIndex then
                            itemCell.selectedIndex = selectedIndex
                            itemCell.numTxt.text = tostring(selectedIndex)
                        end
                    end
                end
            end
        end
    end
end

EquipTechCtrl._ShowEnhanceMaterialItemTips = HL.Method(HL.Table) << function(self, itemInfo)
    Notify(MessageConst.SHOW_ITEM_TIPS, {
        itemId = itemInfo.id,
        instId = itemInfo.instId,
        transform = self.view.selectMaterials.itemTipsPos,
        posType = UIConst.UI_TIPS_POS_TYPE.RightTop,
        isSideTips = DeviceInfo.usingController,
        onBeforeJump = function()
            self.m_isEnhanceMaterialItemTipsMode = false
            self:DeleteInputBinding(self.m_closeEnhanceMaterialItemTipsBindingId)
            Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.selectMaterials.itemListInputGroup.groupId)
        end
    })
end

EquipTechCtrl._CloseEnhanceMaterialItemTips = HL.Method() << function(self)
    Notify(MessageConst.HIDE_ITEM_TIPS)
    self.m_isEnhanceMaterialItemTipsMode = false
    self:DeleteInputBinding(self.m_closeEnhanceMaterialItemTipsBindingId)
    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.selectMaterials.itemListInputGroup.groupId)
end

EquipTechCtrl._RefreshEnhanceInfo = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAnim)
    local isEmpty = self.m_selectedMaterialInstIdList == nil or #self.m_selectedMaterialInstIdList == 0
    local attrShowInfo = self.m_selectedAttrShowInfoList[self.m_selectedAttrShowInfoIndex]
    local selectedCount = isEmpty and 0 or #self.m_selectedMaterialInstIdList

    if self.m_sliderPreviewTween then
        self.m_sliderPreviewTween:Kill()
    end

    self.view.middleBar.enhanceAttrNode.txtName.text = attrShowInfo.showName
    self.view.middleBar.enhanceAttrNode.equipEnhanceLevelNode:InitEquipEnhanceLevelNode({
        equipInstId = self.m_selectedEnhanceEquipInstId,
        attrIndex = attrShowInfo.enhancedAttrIndex,
        showNextLevel = not isEmpty,
    })
    self.view.middleBar.enhanceAttrNode.txtBefore.text = EquipTechUtils.getAttrShowValueText(attrShowInfo)

    self.view.middleBar.enhanceAttrNode.imgArrow.gameObject:SetActive(not isEmpty)
    self.view.middleBar.enhanceAttrNode.txtAfter.gameObject:SetActive(not isEmpty)
    self.m_nextLevelAttrShowValue = EquipTechUtils.getAttrShowValueText(attrShowInfo, true, self.m_selectedEnhanceEquipInstId)
    self.view.middleBar.enhanceAttrNode.txtAfter.text = self.m_nextLevelAttrShowValue
    self.view.middleBar.enhanceAttrNode.selectedNumTxt.text = tostring(selectedCount)


    local successRationNode = self.view.middleBar.enhanceAttrNode.successRationNode
    local enhanceLevel = self.m_selectedEnhanceEquipItemInfo.equipInstData:GetAttrEnhanceLevel(attrShowInfo.enhancedAttrIndex)
    local nextEnhanceLevel = enhanceLevel + 1
    local canEnhance = enhanceLevel < Tables.equipConst.maxAttrEnhanceLevel
    self.m_enhanceGuaranteeFailedCount = 0
    self.m_enhanceGuaranteeMaxFailedCount = 0
    successRationNode.animationWrapper:ClearTween(false)
    successRationNode.gameObject:SetActive(canEnhance)
    if canEnhance then
        successRationNode.txtNormal.gameObject:SetActive(not isEmpty)
        successRationNode.txtEmpty.gameObject:SetActive(isEmpty)

        local failedCount = self.m_selectedEnhanceEquipItemInfo.equipInstData:GetEnhanceFailedTimes(attrShowInfo.enhancedAttrIndex, nextEnhanceLevel)
        local maxFailedCount
        if not string.isEmpty(attrShowInfo.enhanceGuaranteeTimesRuleId) then
            local _, ruleData = Tables.equipEnhanceGuaranteeTimesRuleTable:TryGetValue(attrShowInfo.enhanceGuaranteeTimesRuleId)
            if ruleData then
                maxFailedCount = ruleData[string.format("GuaranteeTimes%d", nextEnhanceLevel)]
            end
        end
        self.m_enhanceGuaranteeFailedCount = failedCount
        self.m_enhanceGuaranteeMaxFailedCount = maxFailedCount or 0
        if maxFailedCount then
            local isMustEnhance = failedCount + selectedCount > maxFailedCount
            successRationNode.successNode.gameObject:SetActive(isMustEnhance)
            successRationNode.bgLight.gameObject:SetActive(isMustEnhance)
            successRationNode.numberTxt.text = string.format("%d/%d", failedCount, maxFailedCount)
            successRationNode.slider.value = failedCount / maxFailedCount

            local targetValue = (failedCount + selectedCount) / maxFailedCount
            if skipAnim then
                successRationNode.sliderPreview.value = targetValue
            else
                self.m_sliderPreviewTween = DOTween.To(function()
                    return successRationNode.sliderPreview.value
                end, function(value)
                    successRationNode.sliderPreview.value = value
                end, targetValue, 0.3)
            end

            if not isEmpty and isMustEnhance then
                successRationNode.animationWrapper:PlayInAnimation()
                AudioAdapter.PostEvent("Au_UI_Event_EquipForgMS")
            end
        else
            logger.error("EquipTechCtrl._RefreshEnhanceMaterial: maxFailedCount not found for ruleId: " ..
                tostring(attrShowInfo.enhanceGuaranteeTimesRuleId) .. ", nextEnhanceLevel: " .. tostring(nextEnhanceLevel))
        end
    end

    self.view.middleBar.bottomNode.btnMake.gameObject:SetActive(not isEmpty)
    if self.view.stateController.currentStateName == STATE_NAME.ENHANCE_MATERIAL then
        local hasCandidates = self.m_materialGroups and #self.m_materialGroups > 0
        self.view.middleBar.bottomNode.selectBtn.gameObject:SetActive(hasCandidates)
        self.view.middleBar.bottomNode.clearBtn.gameObject:SetActive(hasCandidates)
    else
        self.view.middleBar.bottomNode.selectBtn.gameObject:SetActive(false)
        self.view.middleBar.bottomNode.clearBtn.gameObject:SetActive(false)
    end
    self.view.middleBar.bottomNode.emptyState.gameObject:SetActive(isEmpty)
    self:_RefreshEnhanceCostItem()
end

EquipTechCtrl._RefreshEnhanceCostItem = HL.Method() << function(self)
    local isEmpty = self.m_selectedMaterialInstIdList == nil or #self.m_selectedMaterialInstIdList == 0
    local consumeNode = self.view.middleBar.enhanceAttrNode.consumeNode
    consumeNode.btnIcon.onClick:RemoveAllListeners()
    self.m_isCostItemCountEnough = false
    self.m_costItemIds = {}
    self.m_enhanceCostOwnItemCount = 0
    self.m_enhanceCostPerItemCount = 0
    local itemCount = 0
    local costItemCount = 0
    local _, costData = Tables.equipEnhanceCostTable:TryGetValue(self.m_selectedEnhanceEquipItemInfo.equipData.domainId)
    if costData then
        local _, costItemData = Tables.itemTable:TryGetValue(costData.consumeItemId)
        if costItemData then
            table.insert(self.m_costItemIds, costItemData.id)
            itemCount = Utils.getItemCount(costItemData.id)
            costItemCount = costData.consumeItemCnt
            self.m_enhanceCostOwnItemCount = itemCount
            self.m_enhanceCostPerItemCount = costItemCount
            local selectedCount = isEmpty and 0 or #self.m_selectedMaterialInstIdList
            local totalNeedCost = costItemCount * selectedCount
            self.m_isCostItemCountEnough = not isEmpty and itemCount >= totalNeedCost
            consumeNode.imgIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, costItemData.iconId)
            consumeNode.btnIcon.onClick:AddListener(function()
                Notify(MessageConst.SHOW_ITEM_TIPS, {
                    itemId = costItemData.id,
                    transform = consumeNode.itemTipsPos,
                    posType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
                    isSideTips = DeviceInfo.usingController,
                })
            end)
        else
            logger.error("EquipTechCtrl._RefreshEnhanceCostItem: costItemData not found for itemId: " .. costData.consumeItemId)
        end
    else
        logger.error("EquipTechCtrl._RefreshEnhanceCostItem: costData not found for domainId: " .. self.m_selectedEnhanceEquipItemInfo.equipData.domainId)
    end
    consumeNode.textExpend.color = isEmpty and self.view.config.COST_EMPTY_COLOR or self.view.config.COST_ENOUGH_COLOR
    consumeNode.txtCost.color = isEmpty and self.view.config.COST_EMPTY_COLOR or
        (self.m_isCostItemCountEnough and self.view.config.COST_ENOUGH_COLOR or self.view.config.COST_NOT_ENOUGH_COLOR)
    consumeNode.txtCost.text = isEmpty and
        string.format("--/%d", itemCount) or string.format("%d/%d", costItemCount * #self.m_selectedMaterialInstIdList, itemCount)
end

EquipTechCtrl._OnEnhanceClicked = HL.Method() << function(self)
    if self.m_selectedEnhanceEquipInstId == 0 or self.m_selectedMaterialInstIdList == nil or not next(self.m_selectedMaterialInstIdList) then
        return
    end

    if not self.m_isCostItemCountEnough then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_EQUIP_ENHANCE_MATERIAL_NOT_ENOUGH)
        return
    end

    local needConfirm = false
    for _, instId in pairs(self.m_selectedMaterialInstIdList) do
        local equipInstData = CharInfoUtils.getEquipByInstId(instId)
        if equipInstData and equipInstData:HasAnyEnhanceRecord() then
            needConfirm = true
            break
        end
    end
    if needConfirm then
        local items = {}
        for _, instId in pairs(self.m_selectedMaterialInstIdList) do
            local equipInstData = CharInfoUtils.getEquipByInstId(instId)
            if equipInstData and equipInstData:HasAnyEnhanceRecord() then
                table.insert(items, {
                    id = equipInstData.templateId,
                    instId = instId,
                })
            end
        end
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_EQUIP_ENHANCE_MATERIAL_POPUP_TITLE,
            items = items,
            onConfirm = function()
                self:_EnhanceEquip()
            end,
        })
    else
        self:_EnhanceEquip()
    end
end

EquipTechCtrl._EnhanceEquip = HL.Method() << function(self)
    local attrIndex = self.m_selectedAttrShowInfoList[self.m_selectedAttrShowInfoIndex].enhancedAttrIndex
    self.m_equipTechSystem:EnhanceEquip(self.m_selectedEnhanceEquipInstId, self.m_selectedMaterialInstIdList, attrIndex)
end

EquipTechCtrl._OnEquipEnhance = HL.Method(HL.Table) << function(self, args)
    local equipInstId, enhancedAttrIndex = unpack(args)
    local attrShowInfo = self.m_selectedAttrShowInfoList[self.m_selectedAttrShowInfoIndex]
    if equipInstId ~= self.m_selectedEnhanceEquipInstId then
        return
    end

    local consumedIngredientInstIds = {}
    local returnedIngredientInstIds = {}
    local sentList = self.m_selectedMaterialInstIdList
    if sentList then
        for _, instId in ipairs(sentList) do
            if CharInfoUtils.getEquipByInstId(instId) then
                table.insert(returnedIngredientInstIds, instId)
            else
                table.insert(consumedIngredientInstIds, instId)
            end
        end
    end
    self.m_selectedMaterialInstIdList = {}
    self.m_selectedMaterialInstId2Index = {}

    local enhanceFailCountDelta
    if enhancedAttrIndex <= 0 then
        enhanceFailCountDelta = #consumedIngredientInstIds
    end


    local resultArgs = {
        isSuccessful = enhancedAttrIndex > 0,
        equipInstId = equipInstId,
        attrShowInfo = attrShowInfo,
        nextLevelAttrShowValue = self.m_nextLevelAttrShowValue,
        enhanceFailCountDelta = enhanceFailCountDelta,
        consumedIngredientInstIds = consumedIngredientInstIds,
        returnedIngredientInstIds = returnedIngredientInstIds,
        closeCallback = function()
            self:_RefreshEnhancedEquip()
            self:_RefreshEnhanceTargetList(nil, true)
            self:_RefreshEnhanceMaterialList()
            self:_RefreshEnhanceInfo(true)

            local equipInstData = EquipTechUtils.getEquipInstData(equipInstId)
            if equipInstData:IsAttrMaxEnhanced(attrShowInfo.enhancedAttrIndex) then
                self:_BackToEnhanceTarget()
            end
        end
    }
    UIManager:Open(PanelId.EquipEnhanceResult, resultArgs)
end






EquipTechCtrl.m_readNewVersionFormulas = HL.Field(HL.Table)

EquipTechCtrl.m_readFormulas = HL.Field(HL.Table)

EquipTechCtrl._SendFormulaRead = HL.Method() << function(self)
    if self.m_readNewVersionFormulas then
        local readFormulaIdList = {}
        for formulaId, _ in pairs(self.m_readNewVersionFormulas) do
            table.insert(readFormulaIdList, formulaId)
        end
        if #readFormulaIdList > 0 then
            self.m_equipTechSystem:SetNewVersionFormulaRead(readFormulaIdList)
        end
        self.m_readNewVersionFormulas = nil
    end

    if self.m_readFormulas then
        local readFormulaIdList = {}
        for formulaId, _ in pairs(self.m_readFormulas) do
            table.insert(readFormulaIdList, formulaId)
        end
        if #readFormulaIdList > 0 then
            self.m_equipTechSystem:SetFormulaRead(readFormulaIdList)
        end
        self.m_readFormulas = nil
    end
end





EquipTechCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.middleBar.produceContent.formulaNode.naviGroup.onIsFocusedChange:AddListener(function(isFocused)
        self.view.middleBar.produceContent.formulaNode.controllerFocusHintNode.gameObject:SetActive(not isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
    self.view.rightBarEnhanceAttr.naviGroup.onIsFocusedChange:AddListener(function(isFocused)
        self.view.commonBg.tabInputGroup.enabled = not isFocused
        self.view.leftBarEnhance.inputGroup.enabled = not isFocused
    end)
    self.view.rightBarEnhanceAttr.naviGroup.getDefaultSelectableFunc = function()
        if self.m_firstCanEnhancedAttrCell then
            return self.m_firstCanEnhancedAttrCell.btnEnhance
        end
        return nil
    end
    self:BindInputPlayerAction("common_horizontal_focus_right", function()
        self.view.rightBarEnhanceAttr.naviGroup:ManuallyFocus()
    end, self.view.leftBarEnhance.inputGroup.groupId)
    self:BindInputPlayerAction("common_horizontal_stop_focus_left", function()
        self.view.rightBarEnhanceAttr.naviGroup:ManuallyStopFocus()
    end, self.view.rightBarEnhanceAttr.attrNodeInputGroup.groupId)
    UIUtils.bindHyperlinkPopup(self, "EquipTech", self.view.inputGroup.groupId)
end



HL.Commit(EquipTechCtrl)
