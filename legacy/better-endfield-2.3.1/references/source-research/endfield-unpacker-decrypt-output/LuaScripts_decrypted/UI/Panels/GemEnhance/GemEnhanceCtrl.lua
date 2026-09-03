
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GemEnhance

GemEnhanceCtrl = HL.Class('GemEnhanceCtrl', uiCtrl.UICtrl)

local ENHANCE_NODE_STATE_NAME = {
    GEM = "gem",
    MATERIAL = "material",
}






GemEnhanceCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_GEM_ENHANCE] = '_OnGemEnhance',
}

GemEnhanceCtrl.m_args = HL.Field(HL.Table)

GemEnhanceCtrl.m_selectedGemInstId = HL.Field(HL.Number) << -1

GemEnhanceCtrl.m_selectedMaterialGemInstId = HL.Field(HL.Number) << -1

GemEnhanceCtrl.m_selectedTermIndex = HL.Field(HL.Number) << -1

GemEnhanceCtrl.m_isSelectedTermMax = HL.Field(HL.Boolean) << false

GemEnhanceCtrl.m_TermLevelCellCache = HL.Field(HL.Forward("UIListCache"))

GemEnhanceCtrl.m_selectedTermEnhanceData = HL.Field(HL.Any)

GemEnhanceCtrl.m_isCostEnough = HL.Field(HL.Boolean) << false













GemEnhanceCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_args = args or {}
    self:_InitController()

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.GemEnhance)
    end)
    self.view.btnBack.onClick:AddListener(function()
        self:_BackToSelectionMode(true)
    end)
    self.view.enhancedNode.commonToggle:InitCommonToggle(function(isOn)
        if isOn then
            if self.view.enhancedNode.stateController.currentStateName == ENHANCE_NODE_STATE_NAME.GEM then
                return
            end
            self.view.enhancedNode.stateController:SetState(ENHANCE_NODE_STATE_NAME.GEM)
            self:_RefreshMaterialGemList(DeviceInfo.usingController)
            self:_RefreshProb()
            self:_RefreshEnhanceBtn()
        else
            if self.view.enhancedNode.stateController.currentStateName == ENHANCE_NODE_STATE_NAME.MATERIAL then
                return
            end
            self.view.enhancedNode.stateController:SetState(ENHANCE_NODE_STATE_NAME.MATERIAL)
            self:_RefreshEnhanceMaterial()
            self:_RefreshProb()
            self:_RefreshEnhanceBtn()
        end
    end, false, true)
    self:BindInputPlayerAction("weapon_gem_enhance_cost_gem", function()
        self.view.enhancedNode.commonToggle:SetValue(true)
        self.view.enhancedNode.commonToggle.toggle:PlayAudio()
    end)
    self:BindInputPlayerAction("weapon_gem_enhance_cost_material", function()
        self.view.enhancedNode.commonToggle:SetValue(false)
        self.view.enhancedNode.commonToggle.toggle:PlayAudio()
    end)
    self.view.bottomEnhancedNode.btnConfirm.onClick:AddListener(function()
        self:_OnEnhanceConfirmClicked()
    end)
    self.view.entriesPanNode.preBtn.onClick:AddListener(function()
        self:_OnPreTermClicked()
    end)
    self.view.entriesPanNode.nextBtn.onClick:AddListener(function()
        self:_OnNextTermClicked()
    end)
    self:_SetInfoGemSelected(false)
    self.view.infoNode.btnExplain.onClick:AddListener(function()
        self:_SetInfoGemSelected(true)
        local gemInst = CharInfoUtils.getGemByInstId(self.m_selectedGemInstId)
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = gemInst.templateId,
            instId = self.m_selectedGemInstId,
            transform = self.view.infoNode.itemIcon.view.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            onClose = function()
                if self.m_isClosed then
                    return
                end
                self:_SetInfoGemSelected(false)
            end
        })
    end)

    if self.m_args.resumeCurMode == "enhance" then
        self:_EnterSelectionMode(true)
        self:_EnterEnhanceMode()
    else
        self:_EnterSelectionMode()
    end
    self.m_args.resumeCurMode = nil
end





GemEnhanceCtrl._EnterSelectionMode = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipGraduallyShow)
    self.view.lockNaviGroup.enabled = true
    self.view.controllerFocusHintNode.gameObject:SetActive(true)
    self.view.stateController:SetState("selection")
    self:_RefreshLeftGemList(skipGraduallyShow)
end

GemEnhanceCtrl._RefreshLeftGemList = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipGraduallyShow)
    local selectedTermIdMap = self.m_args.selectedTermIdMap
    local selectedGemInstId = self.m_args.gemInstId or self.m_selectedGemInstId
    self.view.commonItemList:InitCommonItemList({
        listType = UIConst.COMMON_ITEM_LIST_TYPE.WEAPON_EXHIBIT_GEM,
        onClickItem = function(args)
            local itemInfo = args.itemInfo
            if not itemInfo then
                return
            end

            self:_OnSelectGem(itemInfo)
        end,
        refreshItemAddOn = function(cell, itemInfo)
            local gemInst = itemInfo.itemInst
            local attachedWeaponInstId = gemInst.weaponInstId

            cell.equipped.gameObject:SetActive(attachedWeaponInstId and attachedWeaponInstId > 0)

            cell.curEquipped.gameObject:SetActive(false)
            cell.disableMask.gameObject:SetActive(false)

            local isPerfectMatch = UIUtils.getGemWishListPerfectMatch(gemInst.instId)
            cell.item:ShowGemPerfectIcon(isPerfectMatch)
        end,
        filter_rarity = Tables.gemConst.enhanceCostGemRarity,
        defaultSelectedIndex = 1,
        selectedTagArgs = selectedTermIdMap,
        selectedIndexId = selectedGemInstId > 0 and selectedGemInstId or nil,
        skipGraduallyShow = skipGraduallyShow,
        onFilterNone = function()
            self.m_selectedGemInstId = -1
            self.view.stateController:SetState("empty")
            self:_UpdateSelectedGemSkillInfo()
        end,

        sortKeys = { "gemPerfectMatchSort", "isEquippedSort", "lockedIndex", "rarity", "sortId1", "sortId2", "id", "instId" }
    })


    self.m_args.selectedTermIdMap = nil
    self.m_args.gemInstId = nil
end

GemEnhanceCtrl._OnSelectGem = HL.Method(HL.Table) << function(self, itemInfo)
    if self.m_selectedGemInstId == itemInfo.instId then
        return
    end
    self.view.stateController:SetState("selection", false)
    self.m_selectedGemInstId = itemInfo.instId
    self:_UpdateSelectedGemBasicInfo(itemInfo)
    self:_UpdateSelectedGemSkillInfo()
end

GemEnhanceCtrl._UpdateSelectedGemBasicInfo = HL.Method(HL.Table) << function(self, itemInfo)
    local gemInst = CharInfoUtils.getGemByInstId(itemInfo.instId)
    if not gemInst then
        logger.error("GemEnhanceCtrl->Can't get gemInst by instId: " .. itemInfo.instId)
        return
    end

    self.view.infoNode.matrixName.text = UIUtils.getItemName(itemInfo.id, itemInfo.instId)
    self.view.infoNode.itemIcon:InitItemIcon(gemInst.templateId, true, gemInst.instId)
    self.view.infoNode.lockToggle:InitLockToggle(gemInst.templateId, gemInst.instId)
    self.view.infoNode.trashToggle:InitTrashToggle(gemInst.templateId, gemInst.instId)
    self.view.infoNode.domainNode:InitDomainTagNode(gemInst.domainId)
    CSUtils.UIContainerResize(self.view.infoNode.starInfoNode, itemInfo.rarity)
    UIUtils.setItemRarityImage(self.view.infoNode.imgQuality, itemInfo.rarity)

    if self.view.infoNode.gemNode then
        local isPerfectMatch = UIUtils.getGemWishListPerfectMatch(itemInfo.instId)
        self.view.infoNode.gemNode.gameObject:SetActive(isPerfectMatch)
    end
end

GemEnhanceCtrl._UpdateSelectedGemSkillInfo = HL.Method() << function(self)
    self.view.entriesGroupNode.animationWrapper:PlayInAnimation()
    local gemInstId = self.m_selectedGemInstId
    if gemInstId <= 0 then
        for i = 1, 3 do

            local termCell = self.view.entriesGroupNode[string.format("gemSkillEnhanceCell%d", i)]
            termCell:InitGemSkillEnhanceCell(nil)
        end
        return
    end

    local gemInst = CharInfoUtils.getGemByInstId(gemInstId)
    if not gemInst then
        logger.error("GemEnhanceCtrl->Can't get gemInst by instId: " .. tostring(gemInstId))
        return
    end
    for i = 0, gemInst.termList.Count - 1 do
        local term = gemInst.termList[i]

        local termCell = self.view.entriesGroupNode[string.format("gemSkillEnhanceCell%d", i+1)]
        termCell:InitGemSkillEnhanceCell({
            termId = term.termId,
            termLevel = term.cost,
            onClick = function(termId, termLevel)
                self.m_selectedTermIndex = i
                self:_EnterEnhanceModeWithAnim()
            end,
        })
    end
end

GemEnhanceCtrl._RefreshSelectedGemInfoByInstId = HL.Method() << function(self)
    local gemInstId = self.m_selectedGemInstId
    if gemInstId == nil or gemInstId <= 0 then
        return
    end
    local gemInst = CharInfoUtils.getGemByInstId(gemInstId)
    if not gemInst then
        logger.error("GemEnhanceCtrl->Can't get gemInst by instId: " .. tostring(gemInstId))
        return
    end
    local itemCfg = Tables.itemTable[gemInst.templateId]
    if not itemCfg then
        logger.error("GemEnhanceCtrl->Can't get itemCfg by templateId: " .. tostring(gemInst.templateId))
        return
    end
    self:_UpdateSelectedGemBasicInfo({
        id = gemInst.templateId,
        instId = gemInst.instId,
        rarity = itemCfg.rarity,
    })
    self:_UpdateSelectedGemSkillInfo()
end

GemEnhanceCtrl._SetInfoGemSelected = HL.Method(HL.Boolean) << function(self, isSelected)
    self.view.infoNode.btnExplain.gameObject:SetActive(not isSelected)
    self.view.infoNode.selectedBG.gameObject:SetActive(isSelected)
end





GemEnhanceCtrl._EnterEnhanceModeWithAnim = HL.Method() << function(self)
    self.view.commonItemList.view.animationWrapper:PlayOutAnimation()
    self.view.entriesGroupNode.animationWrapper:PlayOutAnimation(function()
        self:_EnterEnhanceMode()
    end)
end

GemEnhanceCtrl._EnterEnhanceMode = HL.Method() << function(self)
    self.view.lockNaviGroup.enabled = false
    self.view.controllerFocusHintNode.gameObject:SetActive(false)
    self.view.stateController:SetState("enhance")
    if self.m_args.gemInstId and self.m_args.gemInstId > 0 then
        self.m_selectedGemInstId = self.m_args.gemInstId
    end
    self:_RefreshSelectedGemInfoByInstId()
    if self.m_args.resumeSelectedTermIndex ~= nil and self.m_args.resumeSelectedTermIndex >= 0 then
        self.m_selectedTermIndex = self.m_args.resumeSelectedTermIndex
    end
    self.m_args.resumeSelectedTermIndex = nil
    local targetIsCostGem = self.m_args.resumeEnhanceIsCostGem
    if targetIsCostGem == nil then
        targetIsCostGem = true
    end
    self.m_args.resumeEnhanceIsCostGem = nil
    local isOn = self.view.enhancedNode.commonToggle.toggle.isOn
    self.view.enhancedNode.commonToggle:SetValue(targetIsCostGem)

    if isOn == targetIsCostGem then
        if targetIsCostGem then
            self.view.enhancedNode.stateController:SetState(ENHANCE_NODE_STATE_NAME.GEM)
            self:_RefreshMaterialGemList(DeviceInfo.usingController)
            self:_RefreshProb()
            self:_RefreshEnhanceBtn()
        else
            self.view.enhancedNode.stateController:SetState(ENHANCE_NODE_STATE_NAME.MATERIAL)
            self:_RefreshEnhanceMaterial()
            self:_RefreshProb()
            self:_RefreshEnhanceBtn()
        end
    end
    self:_RefreshEnhanceTerm()
end

GemEnhanceCtrl._BackToSelectionMode = HL.Method(HL.Opt(HL.Boolean)) << function(self, playOutAnim)
    if playOutAnim then
        self.view.bottomEnhancedNode.animationWrapper:PlayOutAnimation()
        self.view.entriesPanNode.animationWrapper:PlayOutAnimation()
        self.view.enhancedNode.animationWrapper:PlayOutAnimation(function()
            self:_EnterSelectionMode()
        end)
    else
        self:_EnterSelectionMode()
    end
end

GemEnhanceCtrl._RefreshEnhanceTerm = HL.Method(HL.Opt(HL.Boolean)) << function(self, playSwitchAnim)
    local gemInst = CharInfoUtils.getGemByInstId(self.m_selectedGemInstId)
    if not gemInst then
        logger.error("GemEnhanceCtrl->Can't get gemInst by instId: " .. tostring(self.m_selectedGemInstId))
        return
    end
    self.view.entriesPanNode.preBtn.gameObject:SetActive(self.m_selectedTermIndex > 0)
    self.view.entriesPanNode.nextBtn.gameObject:SetActive(self.m_selectedTermIndex < gemInst.termList.Count - 1)
    local term = gemInst.termList[self.m_selectedTermIndex]
    local _, termCfg = Tables.gemTable:TryGetValue(term.termId)
    if not termCfg then
        logger.error("GemEnhanceCtrl->Can't get termCfg by termId: " .. tostring(term.termId))
        return
    end

    local isMax = CharInfoUtils.isGemTermEnhanceMax(term.termId, term.cost)
    self.m_isSelectedTermMax = isMax
    local nextCost = isMax and term.cost or term.cost + 1
    self.view.entriesPanNode.txtAttrName:SetAndResolveTextStyle(string.format(Language.LUA_GEM_CARD_SKILL_ACTIVE, termCfg.tagName))
    self.view.entriesPanNode.nowValueTxt.text = string.format(Language.LUA_WEAPON_EXHIBIT_UPGRADE_ADD_FORMAT, term.cost)
    self.view.entriesPanNode.newValueTxt.text = string.format(Language.LUA_WEAPON_EXHIBIT_UPGRADE_ADD_FORMAT, nextCost)
    self.m_TermLevelCellCache = self.m_TermLevelCellCache or UIUtils.genCellCache(self.view.entriesPanNode.grooveCell)
    self.m_TermLevelCellCache:Refresh(nextCost, function(cell, luaIndex)
        cell.progress.gameObject:SetActive(luaIndex <= term.cost)
        cell.progressAdd.gameObject:SetActive(luaIndex > term.cost)
    end)
    self.view.entriesPanNode.stateController:SetState(isMax and "max" or "normal")

    self:_RefreshProb()
    self:_RefreshEnhanceMaterial()
    self:_RefreshEnhanceBtn()

    if playSwitchAnim then
        self.view.entriesPanNode.animationWrapper:Play("gemenhance_entriespan_switch")
    end
end

GemEnhanceCtrl._OnPreTermClicked = HL.Method() << function(self)
    self.m_selectedTermIndex = math.max(0, self.m_selectedTermIndex - 1)
    self:_RefreshEnhanceTerm(true)
end

GemEnhanceCtrl._OnNextTermClicked = HL.Method() << function(self)
    local gemInst = CharInfoUtils.getGemByInstId(self.m_selectedGemInstId)
    self.m_selectedTermIndex = math.min(gemInst.termList.Count - 1, self.m_selectedTermIndex + 1)
    self:_RefreshEnhanceTerm(true)
end

GemEnhanceCtrl._RefreshProb = HL.Method() << function(self)
    local stateName = "Empty"
    if not self.m_isSelectedTermMax then
        if self.view.enhancedNode.stateController.currentStateName == ENHANCE_NODE_STATE_NAME.MATERIAL then
            stateName = "Must"
        elseif self.m_selectedMaterialGemInstId > 0 then
            local gemEnhanceData = self:_GetCurrentGemEnhanceData()
            if gemEnhanceData then
                stateName = gemEnhanceData.probStateName
            end
        end
    end
    self.view.bottomEnhancedNode.probStateCtrl:SetState(stateName)
end

GemEnhanceCtrl._GetCurrentGemEnhanceData = HL.Method().Return(HL.Any) << function(self)
    local gemInst = CharInfoUtils.getGemByInstId(self.m_selectedGemInstId)
    local term = gemInst.termList[self.m_selectedTermIndex]
    local _, termCfg = Tables.gemTable:TryGetValue(term.termId)
    if not termCfg then
        logger.error("GemEnhanceCtrl->Can't get termCfg by termId: " .. tostring(term.termId))
        return nil
    end

    local _, gemEnhanceDataList = Tables.gemEnhanceTable:TryGetValue(termCfg.termType)
    if gemEnhanceDataList then
        for _, gemEnhanceData in pairs(gemEnhanceDataList.list) do
            if gemEnhanceData.termCost == term.cost + 1 then
                return gemEnhanceData
            end
        end
    end
    return nil
end

GemEnhanceCtrl._RefreshMaterialGemList = HL.Method(HL.Opt(HL.Boolean)) << function(self, isFirstSelected)
    local selectedMaterialGemTermIdMap = self.m_args.resumeMaterialSelectedTermIdMap
    local selectedMaterialGemInstId = self.m_args.resumeSelectedMaterialGemInstId or self.m_selectedMaterialGemInstId
    self.m_args.resumeMaterialSelectedTermIdMap = nil
    self.m_args.resumeSelectedMaterialGemInstId = nil
    self:_OnSelectMaterialGem(nil)
    self.view.bottomEnhancedNode.commonGemHorizontalList:InitCommonItemList({
        listType = UIConst.COMMON_ITEM_LIST_TYPE.WEAPON_GEM_MATERIAL,
        onClickItem = function(args)
            local itemInfo = args.itemInfo
            if not itemInfo then
                return
            end

            self:_OnSelectMaterialGem(itemInfo)
        end,
        refreshItemAddOn = function(cell, itemInfo)
            local gemInst = itemInfo.itemInst
            local attachedWeaponInstId = gemInst.weaponInstId

            cell.equipped.gameObject:SetActive(attachedWeaponInstId and attachedWeaponInstId > 0)

            cell.curEquipped.gameObject:SetActive(false)
            cell.disableMask.gameObject:SetActive(false)

            local isPerfectMatch = UIUtils.getGemWishListPerfectMatch(gemInst.instId)
            cell.item:ShowGemPerfectIcon(isPerfectMatch)
        end,
        filter_rarity = Tables.gemConst.enhanceCostGemRarity,
        selectedTagArgs = selectedMaterialGemTermIdMap,
        selectedIndexId = selectedMaterialGemInstId > 0 and selectedMaterialGemInstId or nil,
        exclusiveInstId = self.m_selectedGemInstId,
        onFilterNone = function()
            self.m_selectedMaterialGemInstId = -1
            self.view.enhancedNode.gemEmptyNode.gameObject:SetActive(true)
            self.view.enhancedNode.gemCard.gameObject:SetActive(false)
            self:_RefreshProb()
            self:_RefreshEnhanceBtn()
        end,
        defaultSelectedIndex = selectedMaterialGemInstId > 0 and nil or (isFirstSelected and 1 or nil),
        sortKeys = { "gemPerfectMatchSortReverse", "trashIndex", "lockedIndexReverse", "equippedWeaponInstIdReverse", "rarity", "sortId1", "sortId2", "id", "instId" }
    })
end

GemEnhanceCtrl._OnSelectMaterialGem = HL.Method(HL.Table) << function(self, itemInfo)
    local isEmpty = itemInfo == nil
    local instId = isEmpty and -1 or itemInfo.instId
    if self.m_selectedMaterialGemInstId == instId then
        return
    end
    self.m_selectedMaterialGemInstId = instId
    self.view.enhancedNode.gemNodeAnim:PlayInAnimation()
    self.view.enhancedNode.gemEmptyNode.gameObject:SetActive(isEmpty)
    self.view.enhancedNode.gemCard.gameObject:SetActive(not isEmpty)
    if isEmpty then
        return
    end
    self.view.enhancedNode.gemCard:InitGemCard(itemInfo.instId)
    self:_RefreshProb()
    self:_RefreshEnhanceBtn()
end

GemEnhanceCtrl._RefreshEnhanceMaterial = HL.Method() << function(self)
    local gemEnhanceData = self:_GetCurrentGemEnhanceData()
    local itemId = Tables.gemConst.gemEnhancementItemId
    local costItemCount = gemEnhanceData and gemEnhanceData.costEnhancementItem or 0
    self.view.enhancedNode.itemMaterial:InitItem({
        id = itemId,
        count = costItemCount,
    }, true)

    local itemCount = Utils.getItemCount(itemId, true, true)
    self.view.enhancedNode.commonStorageNode:InitStorageNode(itemCount, costItemCount, true)
    self.m_isCostEnough = itemCount >= costItemCount
end

GemEnhanceCtrl._RefreshEnhanceBtn = HL.Method() << function(self)
    local isMaterialMode = self.view.enhancedNode.stateController.currentStateName == ENHANCE_NODE_STATE_NAME.MATERIAL
    local isAvailable = true
    local unavailableTips = ''
    if self.m_isSelectedTermMax then
        isAvailable = false
        unavailableTips = Language.LUA_GEM_ENHANCE_MAX_LEVEL
    else
        if isMaterialMode then
            if not self.m_isCostEnough then
                isAvailable = false
                unavailableTips = Language.LUA_GEM_ENHANCE_COST_NOT_ENOUGH
            end
        else
            if self.m_selectedMaterialGemInstId <= 0 then
                isAvailable = false
                unavailableTips = Language.LUA_GEM_ENHANCE_MATERIAL_GEM_NOT_SELECTED
            end
        end
    end
    if not isAvailable then
        self.view.bottomEnhancedNode.txtUnavaliable.text = unavailableTips
    end

    self.view.bottomEnhancedNode.btnConfirm.gameObject:SetActive(isAvailable)
    self.view.bottomEnhancedNode.limitReachedNode.gameObject:SetActive(not isAvailable)
end

GemEnhanceCtrl._OnEnhanceConfirmClicked = HL.Method() << function(self)
    local isMaterial = self.view.enhancedNode.stateController.currentStateName == ENHANCE_NODE_STATE_NAME.MATERIAL
    local gemInst = CharInfoUtils.getGemByInstId(self.m_selectedGemInstId, true)
    if not gemInst then
        return
    end
    local termId = gemInst.termList[self.m_selectedTermIndex].termId
    if isMaterial then
        GameInstance.player.inventory:EnhanceGem(self.m_selectedGemInstId, termId, 0, true)
    else
        local materialGemInst = CharInfoUtils.getGemByInstId(self.m_selectedMaterialGemInstId, true)
        if not gemInst then
            return
        end
        if materialGemInst.weaponInstId > 0 then
            self:Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_GEM_ENHANCE_MATERIAL_GEM_EQUIP_CONFIRM,
                onConfirm = function()
                    GameInstance.player.inventory:EnhanceGem(self.m_selectedGemInstId, termId, self.m_selectedMaterialGemInstId, false)
                end,
                weaponInstId = materialGemInst.weaponInstId,
            })
        else
            GameInstance.player.inventory:EnhanceGem(self.m_selectedGemInstId, termId, self.m_selectedMaterialGemInstId, false)
        end
    end
end

GemEnhanceCtrl._OnGemEnhance = HL.Method(HL.Table) << function(self, args)

    local msg = unpack(args)
    UIManager:Open(PanelId.GemEnhanceResult, {
        isSuccess = msg.IsSuccess,
        gemInstId = msg.GemInstId,
        termIndex = self.m_selectedTermIndex,
        refundItems = msg.refundItems,
        onConfirm = function()
            if self.view.enhancedNode.stateController.currentStateName == ENHANCE_NODE_STATE_NAME.MATERIAL then
                self:_RefreshEnhanceMaterial()
            else
                self:_RefreshMaterialGemList(true)
            end
            self:_RefreshEnhanceTerm()
            self:_UpdateSelectedGemSkillInfo()
            local gemInst = CharInfoUtils.getGemByInstId(self.m_selectedGemInstId)
            local term = gemInst.termList[self.m_selectedTermIndex]
            local isMax = CharInfoUtils.isGemTermEnhanceMax(term.termId, term.cost)
            if isMax then
                self:_EnterSelectionMode()
                self:_UpdateSelectedGemSkillInfo()
            end
        end
    })
end





GemEnhanceCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.lockNaviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
        self.view.controllerFocusHintNode.gameObject:SetActive(not isTopLayer)
    end)
    self.view.leftGemNaviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
        if not DeviceInfo.usingController then
            return
        end
        local selectedCell = self.view.commonItemList:GetCurSelectedItemCell()
        if selectedCell then
            self.view.commonItemList:SetSelectedAppearance(selectedCell, not isTopLayer)
        end
    end)
    self.view.enhancedNode.gemCard.view.naviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if UIManager:IsShow(PanelId.CommonFilter) then
            self.view.enhancedNode.gemCard.view.naviGroup:ManuallyStopFocus()
        end
    end)
end



GemEnhanceCtrl._GetSelectedTagArgMap = HL.Method(HL.Any).Return(HL.Opt(HL.Any)) << function(self, commonItemList)
    if not commonItemList or commonItemList.m_selectedTags == nil then
        return nil
    end
    local selectedTagArgMap = {}
    for _, tagInfo in ipairs(commonItemList.m_selectedTags) do
        if tagInfo.param ~= nil then
            selectedTagArgMap[tagInfo.param] = true
        end
    end
    return selectedTagArgMap
end

GemEnhanceCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = lume.deepCopy(self.m_args)
    local selectedTermIdMap = self:_GetSelectedTagArgMap(self.view.commonItemList)
    if selectedTermIdMap then
        arg.selectedTermIdMap = selectedTermIdMap
    end
    if self.m_selectedGemInstId > 0 then
        arg.gemInstId = self.m_selectedGemInstId
    end
    arg.resumeCurMode = self.view.stateController.currentStateName
    if arg.resumeCurMode == "enhance" then
        arg.resumeSelectedTermIndex = self.m_selectedTermIndex
        arg.resumeEnhanceIsCostGem = self.view.enhancedNode.commonToggle.toggle.isOn
        arg.resumeSelectedMaterialGemInstId = self.m_selectedMaterialGemInstId > 0 and self.m_selectedMaterialGemInstId or nil
        local materialSelectedTermIdMap = self:_GetSelectedTagArgMap(self.view.bottomEnhancedNode.commonGemHorizontalList)
        if materialSelectedTermIdMap then
            arg.resumeMaterialSelectedTermIdMap = materialSelectedTermIdMap
        end
    end
    return arg
end

HL.Commit(GemEnhanceCtrl)
