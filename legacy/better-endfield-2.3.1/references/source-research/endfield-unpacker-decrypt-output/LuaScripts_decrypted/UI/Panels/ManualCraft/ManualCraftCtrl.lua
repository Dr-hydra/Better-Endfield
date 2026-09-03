
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ManualCraft
local PHASE_ID = PhaseId.ManualCraft
local MAX_APPEND_MANUFACTURE_COUNT_LIMIT = 5
local CraftShowingType = CS.Beyond.GEnums.CraftShowingType
local filterList = {
    [1] = {
        type = CraftShowingType.ManualCraftTonic,
    },
    [2] = {
        type = CraftShowingType.ManualCraftArmament,
    },
    [3] = {
        type = CraftShowingType.ManualCraftDish,
    },
    [4] = {
        type = CraftShowingType.ManualArableField,
    },
    [5] = {
        type = CraftShowingType.SpecialManualCraft,
    },
    [6] = {
        type = CraftShowingType.ManualCraftConvert,
    }
}

local StaminaItemId = "item_ap"

local ShowStatus = {
    Locked = 0,
    Unlocked = 1,
    Completed = 2,
    Rewarded = 3,
}


ManualCraftCtrl = HL.Class('ManualCraftCtrl', uiCtrl.UICtrl)







ManualCraftCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_STAMINA_CHANGED] = '_OnStaminaChanged',
    [MessageConst.ON_MANUAL_WORK_MODIFY] = 'OnManualWorkModify',
    [MessageConst.ON_MANUAL_WORK_CANCEL] = 'OnManualWorkCancel',
    [MessageConst.ON_OPEN_COMMON_FILTER] = '_OpenCommonFilter',
    [MessageConst.ON_CLOSE_COMMON_FILTER] = '_CloseCommonFilter',
    [MessageConst.ON_ITEM_COUNT_CHANGED] = 'OnItemCountChanged',
    [MessageConst.ON_MANUAL_CRAFT_POPUP_PANEL_CLOSE] = 'OnManualCraftPopupPanelClose',
}

ManualCraftCtrl.m_inventorySystem = HL.Field(HL.Any)

ManualCraftCtrl.m_facManualCraftSystem = HL.Field(HL.Any)

ManualCraftCtrl.m_filterTypeTabCellCache = HL.Field(HL.Forward("UIListCache"))

ManualCraftCtrl.m_TabLuaIndex2Cell = HL.Field(HL.Table)

ManualCraftCtrl.m_TabValidNum = HL.Field(HL.Number) << 0

ManualCraftCtrl.m_validTabFilterTypeList = HL.Field(HL.Table) << nil

ManualCraftCtrl.m_validTabFilterTypeDict = HL.Field(HL.Table) << nil

ManualCraftCtrl.m_sortMode = HL.Field(HL.Number) << 1

ManualCraftCtrl.m_sortIncremental = HL.Field(HL.Boolean) << true

ManualCraftCtrl.m_getCraftCellFunc = HL.Field(HL.Function)

ManualCraftCtrl.m_craftInfoList = HL.Field(HL.Table)

ManualCraftCtrl.m_allIngredientsForDisplayCraft = HL.Field(HL.Table)

ManualCraftCtrl.m_csIndex2craftItemCell = HL.Field(HL.Table)

ManualCraftCtrl.m_selectedCraftId = HL.Field(HL.String) << ""

ManualCraftCtrl.m_selectedCraftTabType = HL.Field(HL.Any) << ""

ManualCraftCtrl.m_selectedTabLuaIndex = HL.Field(HL.Number) << -1

ManualCraftCtrl.m_workshopList = HL.Field(HL.Forward("UIListCache"))

ManualCraftCtrl.m_manualCount = HL.Field(HL.Number) << 0

ManualCraftCtrl.m_manufactureListCache = HL.Field(HL.Forward("UIListCache"))

ManualCraftCtrl.m_itemDescNodeId = HL.Field(HL.Any) << ""

ManualCraftCtrl.m_readCraftIds = HL.Field(HL.Table)

ManualCraftCtrl.m_isMaking = HL.Field(HL.Boolean) << false

ManualCraftCtrl.itemNaviFlag = HL.Field(HL.Boolean) << false

ManualCraftCtrl.m_tabPlayingOutAnim = HL.Field(HL.Boolean) << false

ManualCraftCtrl.m_fabricateSoundKey = HL.Field(HL.Number) << 0

ManualCraftCtrl.m_tipActivityManualCraft = HL.Field(HL.Table)

ManualCraftCtrl.m_domainFilterOptions = HL.Field(HL.Table)

ManualCraftCtrl.m_craftConvertFilterOptions = HL.Field(HL.Table)

ManualCraftCtrl.m_curFilterMode = HL.Field(HL.String) << ""

ManualCraftCtrl.m_validDomainFilter = HL.Field(HL.Table)

ManualCraftCtrl.m_nowTabCell = HL.Field(HL.Any)

ManualCraftCtrl.m_nowCraftCell = HL.Field(HL.Any)

ManualCraftCtrl.m_filterCells = HL.Field(HL.Forward("UIListCache"))

ManualCraftCtrl.m_filterCurNaviIndex = HL.Field(HL.Number) << 0

ManualCraftCtrl.m_jumpId = HL.Field(HL.String) << ""

ManualCraftCtrl.m_initSelectCsIndex = HL.Field(HL.Number) << 0

ManualCraftCtrl.m_getTabCellFunc = HL.Field(HL.Function)

ManualCraftCtrl.m_useStamina = HL.Field(HL.Boolean) << false

ManualCraftCtrl.m_craftStaminaOpen2CloseDict = HL.Field(HL.Table) << nil

ManualCraftCtrl.m_craftStaminaClose2OpenDict = HL.Field(HL.Table) << nil

ManualCraftCtrl.m_canJumpStamina = HL.Field(HL.Boolean) << false



ManualCraftCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    MAX_APPEND_MANUFACTURE_COUNT_LIMIT = Tables.factoryConst.manualWorkCountLimit

    self.m_inventorySystem = GameInstance.player.inventory
    self.m_facManualCraftSystem = GameInstance.player.facManualCraft
    self:_InitSubmitActivityInfo()

    self.m_readCraftIds = {}

    self.m_workshopList = UIUtils.genCellCache(self.view.itemCell)
    if arg and arg.jumpId then
        self.m_jumpId = arg.jumpId
    end
    if arg and arg.showPopup then
        UIManager:Open(PanelId.ManualCraftPopups,{ itemId = arg.itemId })
    end

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.ManualCraft)
    end)
    self.m_csIndex2craftItemCell = {}
    self.view.craftContent.onUpdateCell:AddListener(function(gameObject, index)
        self:_UpdateCell(gameObject, index)
    end)
    self.view.craftContent.onSelectedCell:AddListener(function(obj, csIndex)
        self:_SelectCraft(self.m_craftInfoList[LuaIndex(csIndex)].id)
    end)

    self.view.productionManualBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.ManualCraftPopups)
    end)
    self.m_sortMode = 1
    self.m_sortIncremental = false
    self:_InitDomainFilterOptions()
    self:_InitCraftConvertFilterOptions()

    local recoverState = arg and arg.recoverState
    if recoverState then
        self.m_sortMode = recoverState.sortMode or self.m_sortMode
        self.m_sortIncremental = (recoverState.sortIncremental ~= nil) and recoverState.sortIncremental or self.m_sortIncremental
        self.m_useStamina = recoverState.useStamina or false
        if recoverState.selectedCraftId and recoverState.selectedCraftId ~= "" then
            self.m_jumpId = recoverState.selectedCraftId
        end
    end
    self.m_facManualCraftSystem.useStamina = self.m_useStamina

    self.view.openStaminaButton.gameObject:SetActive(false)
    self.view.openStaminaButton.onClick:RemoveAllListeners()
    self.view.openStaminaButton.onClick:AddListener(function()
        if self.m_canJumpStamina then
            Notify(MessageConst.HIDE_ITEM_TIPS)
            PhaseManager:OpenPhase(PhaseId.StaminaPopUp)
        end
    end)

    self:BindInputPlayerAction("jump_manual_tab_prev", function()
        self:_ClickPrevTab()
    end)
    self:BindInputPlayerAction("jump_manual_tab_next", function()
        self:_ClickNextTab()
    end)

    self:_InitTabScrollList()
    self:_InitStaminaToggle()
    self:_UpdateTabScrollList()
    self:_UpdateTabKeyHint()
    self:_UpdateClickTab()

    if recoverState and recoverState.manualCount and recoverState.manualCount > 0 then
        self.m_manualCount = recoverState.manualCount
        self:_RefreshCraftNode(false)
        self:_RefreshCraftCount()
    end

    self.view.btnCommon.onClick:AddListener(function()
        self:_StartCraft()
    end)

    self.view.settingList.gameObject:SetActive(false)
    self.view.productionManualRedDot:InitRedDot("ManualCraftRewardEntry")

    local isUnlock = Utils.isSystemUnlocked(GEnums.UnlockSystemType.ProductManual) and Utils.isInMainScope()
    self.view.productionManualBtn.gameObject:SetActive(isUnlock)

    if DeviceInfo.usingController then
        self.view.rightBottomDecorationIcon.gameObject:SetActive(false)
    else
        self.view.rightBottomDecorationIcon.gameObject:SetActive(true)
    end

    self.view.useItemNodesSelectableNaviGroup.onIsFocusedChange:AddListener(function(isFocus)
        self:_useItemNodesNaviGroupShowInfo(isFocus)

        if isFocus then
            Notify(MessageConst.HIDE_CONTROLLER_HINT, { panelId = PANEL_ID, })
            Notify(MessageConst.HIDE_CONTROLLER_HINT, { panelId = PanelId.FakeControllerSmallMenu, })
        else
            local hintArgs = self.view.controllerHintPlaceholder:GetArgs()
            Notify(MessageConst.SHOW_CONTROLLER_HINT, hintArgs)
        end
    end)

    self:Notify(MessageConst.ON_DISABLE_COMMON_TOAST)
end


ManualCraftCtrl._useItemNodesNaviGroupShowInfo = HL.Method(HL.Boolean) << function(self, isFocus)
    self.view.rightBottomDecorationIcon.gameObject:SetActive(isFocus)
    self.view.promptBox.gameObject:SetActive(not isFocus)
    self.view.openStaminaButton.gameObject:SetActive(isFocus)
    self.view.numberSelector_New:UpdateKeyHintVisible(not isFocus)
end

ManualCraftCtrl._ApplySort = HL.Method(HL.Table, HL.Boolean) << function(self, optData, isIncremental)
    self.m_sortMode = optData.sortMode
    self.m_sortIncremental = isIncremental
    self:_RefreshCraftList()
    for k,v in pairs(self.m_readCraftIds) do
        self.m_facManualCraftSystem:ReadSingleCraft(k)
    end
end

ManualCraftCtrl.PhaseRefresh = HL.Method(HL.Any) << function(self, jumpId)
    if string.isEmpty(jumpId) then
        return
    end
    self.view.useItemNodesSelectableNaviGroup:ManuallyStopFocus()
    self:_useItemNodesNaviGroupShowInfo(false)

    self.m_jumpId = jumpId
    local lastSelectedCraftId = self.m_selectedCraftTabType
    self:_UpdateClickTab()
    if lastSelectedCraftId == self.m_selectedCraftTabType then
        self:_RefreshCraftList()
    end
end


ManualCraftCtrl._InitSubmitActivityInfo = HL.Method() << function(self)
    self.m_tipActivityManualCraft = {}
    for activityId, data in pairs(Tables.ActivitySubmitTextTable) do
        local activity = GameInstance.player.activitySystem:GetActivity(activityId)
        local _, activityData = Tables.activityTable:TryGetValue(activityId)
        local finish = false
        if activityData and not string.isEmpty(activityData.introMissionQuestId) then
            local questState = GameInstance.player.mission:GetQuestState(activityData.introMissionQuestId)
            if questState == CS.Beyond.Gameplay.MissionSystem.QuestState.Completed then
                finish = true
            end
        end

        if activity and finish then
            for key, value in pairs(Tables.FoodSubmitStageIdTable) do
                if value.activityId == activityId then
                    local info = {
                        stageId = key,
                        sortId = value.sortId,
                    }
                    local showStage = false
                    local stageState = self:GetStageShowState(activityId, info.stageId)

                    if stageState ~= ShowStatus.Locked then
                        showStage = true
                    end

                    local hasUiCfg, uiCfg = Tables.ActivitySubmitFoodTable:TryGetValue(info.stageId)
                    if hasUiCfg then
                        if uiCfg.unlockShow then
                            showStage = true
                        end
                        if showStage and stageState == ShowStatus.Unlocked then
                            self.m_tipActivityManualCraft[uiCfg.tipManualCraftId] = true
                        end

                    else
                        showStage = false
                    end
                end
            end
        end
    end

end


ManualCraftCtrl.GetStageShowState = HL.Method(HL.Any, HL.Any).Return(HL.Any) << function(self, activityId, stageId)
    local showStatus = ShowStatus.Locked
    local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
    if not activityData then
        return showStatus
    end

    local stageData = activityData:GetStageData(stageId)
    local status = GEnums.ActivityConditionalStageState.Locked
    if stageData ~= nil then
        status = GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
    end

    if status == GEnums.ActivityConditionalStageState.Locked then
        showStatus = ShowStatus.Locked
    elseif status == GEnums.ActivityConditionalStageState.Unlocked then
        showStatus = ShowStatus.Unlocked
    elseif status == GEnums.ActivityConditionalStageState.Completed then
        showStatus = ShowStatus.Completed
    elseif status == GEnums.ActivityConditionalStageState.Rewarded then
        showStatus = ShowStatus.Rewarded
    end

    return showStatus
end


ManualCraftCtrl._StartCraft = HL.Method() << function(self)
    local needItems = self:_GetIngredientItems(self.m_selectedCraftId, self.m_manualCount)

    for _, item in pairs(needItems) do
        local inventoryCount = self:_GetItemCount(item.id)
        local itemName = Tables.itemTable:GetValue(item.id).name
        if inventoryCount < item.count then
            GameAction.ShowUIToast(string.format(Language.LUA_INGREDIENT_NOT_ENOUGH, itemName))
            return
        end
    end
    self.m_facManualCraftSystem:DoManualWork(Utils.getCurrentScope(), self.m_selectedCraftId, self.m_manualCount)
end

ManualCraftCtrl._InitStaminaToggle = HL.Method() << function(self)
    self.view.switchStaminaNode.gameObject:SetActive(false)
    self.view.commonToggle:InitCommonToggle(function(isOn)
        if isOn ~= self.m_useStamina then
            self.m_useStamina = isOn
            self.m_facManualCraftSystem.useStamina = isOn
            self.itemNaviFlag = true
            self.m_initSelectCsIndex = 0

            if self.m_useStamina then
                if self.m_craftStaminaClose2OpenDict[self.m_selectedCraftId] ~= nil then
                    self.m_jumpId = self.m_craftStaminaClose2OpenDict[self.m_selectedCraftId]
                end
            else
                if self.m_craftStaminaOpen2CloseDict[self.m_selectedCraftId] ~= nil  then
                    self.m_jumpId = self.m_craftStaminaOpen2CloseDict[self.m_selectedCraftId]
                end
            end
            Notify(MessageConst.ON_CRAFT_SWITCH_STAMINA)
            self:_RefreshCraftList()
            self:_RefreshConvertRedDot()
        end
    end, self.m_useStamina)
end


ManualCraftCtrl._InitTabScrollList = HL.Method() << function(self)
    self.view.tabScrollList.gameObject:SetActive(true)

    self.m_getTabCellFunc = UIUtils.genCachedCellFunction(self.view.tabScrollList)
    self.view.tabScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getTabCellFunc(obj)
        self:_OnRefreshTabCell(cell, csIndex)
    end)
end

ManualCraftCtrl._UpdateTabScrollList = HL.Method() << function(self)
    self.m_nowTabCell = nil
    self.m_TabValidNum = 0
    self.m_validTabFilterTypeList = {}
    self.m_validTabFilterTypeDict = {}
    for i = 1, #filterList do
        local l = self.m_facManualCraftSystem:GetUnlockedFormulaByType(filterList[i].type)
        if l ~= nil and l.Count > 0 then
            table.insert(self.m_validTabFilterTypeList, filterList[i].type)
            self.m_validTabFilterTypeDict[filterList[i].type] = true
        end
    end
    self.m_TabLuaIndex2Cell = {}
    self.m_TabValidNum = #self.m_validTabFilterTypeList
    self.view.tabScrollList:UpdateCount(#self.m_validTabFilterTypeList)
end


ManualCraftCtrl._OnRefreshTabCell = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)
    local luaIndex = LuaIndex(csIndex)
    local filterType = self.m_validTabFilterTypeList[luaIndex]
    self.m_TabLuaIndex2Cell[luaIndex] = cell
    cell.gameObject.name = "Tab_" .. filterType:ToString()
    cell.redDot:InitRedDot("ManualCraftType", filterType)
    local success, craftTypeInfo = Tables.factoryCraftShowingTypeTable:TryGetValue(filterType:ToInt())

    if success then
        cell.default.gameObject:SetActive(self.m_selectedCraftTabType ~= filterType)
        cell.selected.gameObject:SetActive(self.m_selectedCraftTabType == filterType)
        cell.default.text.text = craftTypeInfo.name
        cell.selected.text.text = craftTypeInfo.name
        cell.selected.icon:LoadSprite(UIConst.UI_SPRITE_MANUAL_CRAFT_TYPE_ICON, craftTypeInfo.icon)
        cell.default.icon:LoadSprite(UIConst.UI_SPRITE_MANUAL_CRAFT_TYPE_ICON, craftTypeInfo.icon)
        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            self:_ClickTab(luaIndex)
        end)
    end
end


ManualCraftCtrl._UpdateClickTab = HL.Method() << function(self)
    if not string.isEmpty(self.m_jumpId) then
        local craftData = Tables.factoryManualCraftTable:GetValue(self.m_jumpId)
        if not self.m_validTabFilterTypeDict[craftData.showingType] then
            self.m_jumpId = ""
        end
    end

    if self.m_validTabFilterTypeList == nil or #self.m_validTabFilterTypeList == 0 then
        self:_SetEmpty()
        return
    end

    if string.isEmpty(self.m_jumpId) then
        self:_ClickTab(1)
    else
        local craftData = Tables.factoryManualCraftTable:GetValue(self.m_jumpId)
        local clickTabLuaIndex = 1
        for i = 1, #self.m_validTabFilterTypeList do
            if craftData.showingType == self.m_validTabFilterTypeList[i] then
                clickTabLuaIndex = i
            end
        end
        self:_ClickTab(clickTabLuaIndex)
    end
end

ManualCraftCtrl._UpdateTabKeyHint = HL.Method() << function(self)
    if DeviceInfo.usingTouch then
        self.view.tabKeyHintNode.gameObject:SetActive(false)
    else
        if self.m_TabValidNum > 1 then
            self.view.tabKeyHintNode.gameObject:SetActive(true)
        else
            self.view.tabKeyHintNode.gameObject:SetActive(false)
        end
    end
end



ManualCraftCtrl._ClickPrevTab = HL.Method() << function(self)
    if self.m_tabPlayingOutAnim then
        return
    end

    if self.m_selectedTabLuaIndex == -1 then
        return
    end

    if self.m_TabValidNum == 1 then
        return
    end

    if self.m_selectedTabLuaIndex == 1 then
        self:_ClickTab(#self.m_validTabFilterTypeList)
    else
        self:_ClickTab(self.m_selectedTabLuaIndex - 1)
    end
end

ManualCraftCtrl._ClickNextTab = HL.Method() << function(self)
    if self.m_tabPlayingOutAnim then
        return
    end

    if self.m_TabValidNum == 1 then
        return
    end

    if self.m_selectedTabLuaIndex == -1 then
        return
    end

    if self.m_selectedTabLuaIndex == #self.m_validTabFilterTypeList then
        self:_ClickTab(1)
    else
        self:_ClickTab(self.m_selectedTabLuaIndex + 1)
    end
end


ManualCraftCtrl._ClickTab = HL.Method(HL.Any) << function(self, tabLuaIndex)
    local csIndex = CSIndex(tabLuaIndex)
    local res = self.view.tabScrollList:GetShowRange()
    if res.x <= csIndex and res.y >= csIndex then
        local checkCell = self.m_TabLuaIndex2Cell[tabLuaIndex]
        if checkCell == nil then
            return
        end
        local leftNodePos = checkCell.leftNode.gameObject.transform.position
        local leftScreenPos = self.uiCamera:WorldToScreenPoint(leftNodePos)
        local leftUiPos, leftIsInside = UIUtils.screenPointToUI(Vector2(leftScreenPos.x,leftScreenPos.y), self.uiCamera, self.view.tabScrollList.gameObject.transform)

        local rightNodePos = checkCell.rightNode.gameObject.transform.position
        local rightScreenPos = self.uiCamera:WorldToScreenPoint(rightNodePos)
        local rightUiPos, rightIsInside = UIUtils.screenPointToUI(Vector2(rightScreenPos.x,rightScreenPos.y), self.uiCamera, self.view.tabScrollList.gameObject.transform)

        if not self.view.tabScrollList.gameObject.transform.rect:Contains(leftUiPos) or not self.view.tabScrollList.gameObject.transform.rect:Contains(rightUiPos) then
            self.view.tabScrollList:ScrollToIndex(csIndex, true)
        end
    else
        self.view.tabScrollList:ScrollToIndex(csIndex, true)
    end

    if self.m_tabPlayingOutAnim then
        return
    end

    local cell = self.m_TabLuaIndex2Cell[tabLuaIndex]
    if cell == nil then
        return
    end

    self.itemNaviFlag = true
    self.m_initSelectCsIndex = 0
    self.m_selectedTabLuaIndex = tabLuaIndex

    local filterType = self.m_validTabFilterTypeList[tabLuaIndex]
    if self.m_selectedCraftTabType == filterType then
        return
    end

    if filterType == CraftShowingType.ManualCraftConvert then
        self.m_curFilterMode = "ManualCraftConvert"
    else
        self.m_curFilterMode = "Domain"
    end

    for k,v in pairs(self.m_readCraftIds) do
        self.m_facManualCraftSystem:ReadSingleCraft(k)
    end

    if self.m_nowTabCell ~= nil then
        self.m_nowTabCell.default.gameObject:SetActive(true)
        local nowTabCell = self.m_nowTabCell
        self.m_tabPlayingOutAnim = true
        if nowTabCell.selected.gameObject.activeSelf then
            nowTabCell.selectedAnimationWrapper:PlayOutAnimation(function()
                nowTabCell.selected.gameObject:SetActive(false)
                self.m_tabPlayingOutAnim = false
            end)
        end
    end

    if self.m_nowCraftCell ~= nil then
        self.m_nowCraftCell.default.gameObject:SetActive(true)
        self.m_nowCraftCell.selected.gameObject:SetActive(false)
        self.m_nowCraftCell = nil
    end
    self.view.settingList.gameObject:SetActive(false)
    cell.default.gameObject:SetActive(false)
    cell.selected.gameObject:SetActive(true)
    cell.selectedAnimationWrapper:PlayInAnimation()
    self.m_selectedCraftTabType = filterType
    self.m_facManualCraftSystem:NotifyManualCraftTabChanged(filterType:ToInt())
    self.view.switchStaminaNode.gameObject:SetActive(self.m_selectedCraftTabType == CraftShowingType.ManualCraftConvert)
    self:_RefreshCraftList()
    self:_RefreshConvertRedDot()
    self.m_nowTabCell = cell
    if DeviceInfo.usingController then
        AudioAdapter.PostEvent("Au_UI_Button_Common")
    end
    if self.m_selectedTabLuaIndex > 0 then
        local success, craftTypeInfo = Tables.factoryCraftShowingTypeTable:TryGetValue(self.m_selectedCraftTabType:ToInt())
        if success then
            local path = string.gsub(craftTypeInfo.icon, "small", "")
            self.view.typeDecoBg:LoadSprite(UIConst.UI_SPRITE_MANUAL_CRAFT_TYPE_ICON, path)
        end
    end
end


ManualCraftCtrl._SetEmpty = HL.Method() << function(self)
    self.view.emptyNode.gameObject:SetActive(true)
    self.view.middleBarNode.gameObject:SetActive(false)
    self.view.rightBar.gameObject:SetActive(false)
    self.view.topBarNode.gameObject:SetActive(false)
    self:_RefreshStartCraftBtn()
end


ManualCraftCtrl._RefreshCraftList = HL.Method() << function(self)
    if self.m_nowCraftCell ~= nil then
        self.m_nowCraftCell.default.gameObject:SetActive(true)
        self.m_nowCraftCell.selected.gameObject:SetActive(false)
        self.m_nowCraftCell = nil
    end

    if self.m_selectedCraftTabType == nil or string.isEmpty(self.m_selectedCraftTabType) then
        self:_SetEmpty()
        return
    end

    local manualCraftData = Tables.factoryManualCraftTable

    self.m_craftInfoList = {}
    self.m_validDomainFilter = {}

    local validFormulaCount = 0
    local formulaList = self.m_facManualCraftSystem:GetUnlockedFormulaByType(self.m_selectedCraftTabType)
    local noSelectFilter = self:_CheckIsNoSelectFilter()

    if formulaList ~= nil then
        local tempUseStamina  = {}
        local tempNoUseStamina = {}
        self.m_craftStaminaOpen2CloseDict = {}
        self.m_craftStaminaClose2OpenDict = {}

        for _, formulaId in pairs(formulaList) do
            local success, manualCraftInfo = manualCraftData:TryGetValue(formulaId)
            if CraftShowingType.ManualCraftConvert == self.m_selectedCraftTabType then
                local haveItemAp = false
                for useItemIndex = 1, manualCraftInfo.ingredients.Count do
                    local checkId = manualCraftInfo.ingredients[useItemIndex - 1].id
                    if checkId == StaminaItemId then
                        haveItemAp = true
                    end
                end

                if not self.m_useStamina and haveItemAp then
                    success = false
                elseif self.m_useStamina and not haveItemAp then
                    success = false
                end

                if manualCraftInfo.ingredients.Count > 0 then
                    local outId = manualCraftInfo.outcomes[0].id
                    local craftId = manualCraftInfo.id
                    if haveItemAp then
                        tempUseStamina[outId] = craftId
                    else
                        tempNoUseStamina[outId] = craftId
                    end
                end
            end

            if success then
                validFormulaCount = validFormulaCount + 1
                self.m_validDomainFilter[manualCraftInfo.domainId] = true
                if noSelectFilter then
                    table.insert(self.m_craftInfoList, manualCraftInfo)
                else
                    self:_AddCraftInfoByFilter(manualCraftInfo)
                end
            end
        end

        for outId, craftId in pairs(tempUseStamina) do
            local noStaminaCraftId = tempNoUseStamina[outId]
            if noStaminaCraftId ~= nil then
                self.m_craftStaminaOpen2CloseDict[craftId] = noStaminaCraftId
                self.m_craftStaminaClose2OpenDict[noStaminaCraftId] = craftId
            end
        end
    end

    local validDomainCount = 0
    for _ in pairs(self.m_validDomainFilter) do
        validDomainCount = validDomainCount + 1
    end

    local activeFilter = false
    if self.m_curFilterMode == "Domain" then
        activeFilter = validDomainCount > 1 or (#self.m_craftInfoList == 0 and validFormulaCount > 0)
    elseif self.m_curFilterMode == "ManualCraftConvert" then
        activeFilter = true
    end

    self:_initFilterAndSortNode(activeFilter)

    if activeFilter == false then
        self.view.settingList.gameObject:SetActive(activeFilter)
    end
    local sortFunc = Utils.genSortFunction(UIConst.ManualCraftSortOptions[self.m_sortMode].sortKeys, self.m_sortIncremental)
    local craftSortFun = function(a,b)
        if self.m_sortMode == 1 then
            local aCanDo = self:_CheckFormulaAvailable(a.id)
            local bCanDo = self:_CheckFormulaAvailable(b.id)
            if aCanDo ~= bCanDo then
                if self.m_sortIncremental then
                    return not aCanDo
                else
                    return aCanDo
                end
            end
            return sortFunc(a,b)
        else
            return sortFunc(a,b)
        end
    end
    table.sort(self.m_craftInfoList, craftSortFun)

    self.m_allIngredientsForDisplayCraft = {}

    self.m_getCraftCellFunc = self.m_getCraftCellFunc or UIUtils.genCachedCellFunction(self.view.craftContent)
    local selectIndex = 0
    if not string.isEmpty(self.m_jumpId) then
        for i = 1,#self.m_craftInfoList do
            if self.m_craftInfoList[i].id == self.m_jumpId then
                selectIndex = i - 1
                self.m_jumpId = ""
                self.m_initSelectCsIndex = selectIndex
                break
            end
        end
    end

     if #self.m_craftInfoList > 0 then
         self.view.craftContent:SetSelectedIndex(selectIndex, true, true, false)
         self.view.emptyNode.gameObject:SetActive(false)
         self.view.middleBarNode.gameObject:SetActive(true)
         self.view.rightBar.gameObject:SetActive(true)
         self.view.topBarNode.gameObject:SetActive(true)
         self.view.middleBar.gameObject:SetActive(true)
         self.view.useItemNodes.gameObject:SetActive(true)
         self.view.rightBar.gameObject:SetActive(true)
     elseif validFormulaCount > 0 then
         self.view.emptyNode.gameObject:SetActive(false)
         self.view.middleBarNode.gameObject:SetActive(true)
         self.view.middleBar.gameObject:SetActive(false)
         self.view.useItemNodes.gameObject:SetActive(false)
         self.view.rightBar.gameObject:SetActive(false)
         self.view.topBarNode.gameObject:SetActive(true)
     else
        self:_SetEmpty()
        self.m_workshopList:Refresh(0, function(cell, index)
        end)
    end

    if self.m_csIndex2craftItemCell ~= nil then
        for i, cell in pairs(self.m_csIndex2craftItemCell) do
            cell.button.onClick:RemoveAllListeners()
        end
    end

    self.m_csIndex2craftItemCell = {}
    if selectIndex == 0 then
        self.view.craftContent:UpdateCount(#self.m_craftInfoList, true)
    else
        self.view.craftContent:UpdateCount(#self.m_craftInfoList, selectIndex)
    end
end


ManualCraftCtrl._UpdateCell = HL.Method(GameObject, HL.Number) << function(self, gameObject, index)
    local luaIdx = LuaIndex(index)

    local craftInfo = self.m_craftInfoList[luaIdx]
    gameObject.name = "Craft_" .. craftInfo.id
    self.m_readCraftIds[craftInfo.id] = true
    local outcomeItemId = craftInfo.outcomes[0].id
    local craftItemCell = self.m_getCraftCellFunc(gameObject)
    self.m_csIndex2craftItemCell[index] = craftItemCell
    craftItemCell.id = craftInfo.id
    local data = Tables.itemTable:GetValue(outcomeItemId)
    craftItemCell.selected.commodityText.text = data.name
    craftItemCell.default.commodityText.text = data.name
    craftItemCell.default.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, data.iconId)
    craftItemCell.selected.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, data.iconId)
    craftItemCell.notUnlocked.gameObject:SetActive(false)
    UIUtils.setItemRarityImage(craftItemCell.default.colorLine, data.rarity)
    UIUtils.setItemRarityImage(craftItemCell.selected.colorLine, data.rarity)
    if self.view.craftContent.curSelectedIndex == index then
        craftItemCell.selected.gameObject:SetActive(true)
        craftItemCell.animationWrapper:SampleToInAnimationEnd()
        craftItemCell.default.gameObject:SetActive(false)
        if self.m_nowCraftCell == nil then
            self.m_nowCraftCell = craftItemCell
        end
        self.m_facManualCraftSystem:ReadSingleCraft(craftInfo.id)
        craftItemCell.default.redDot:InitRedDot("ManualCraftItem", craftInfo.id)
    else
        craftItemCell.selected.gameObject:SetActive(false)
        craftItemCell.default.gameObject:SetActive(true)
    end

    craftItemCell.button.onClick:RemoveAllListeners()
    craftItemCell.button.onClick:AddListener(function()
        self:_SelectCraftItem(index)
    end)

    if self.itemNaviFlag then
        if luaIdx == self.m_initSelectCsIndex + 1 then
            self.itemNaviFlag = false
            self:SetNaviTarget(craftItemCell.button)
        end
    else
        if self.m_selectedCraftId == craftInfo.id then
            self:SetNaviTarget(craftItemCell.button)
        end
    end

    craftItemCell.default.redDot:InitRedDot("ManualCraftItem", craftInfo.id)

    for i = 1, craftInfo.ingredients.Count do
        self.m_allIngredientsForDisplayCraft[craftInfo.ingredients[i-1].id] = true
    end

    if self.m_tipActivityManualCraft[craftInfo.id] then
        craftItemCell.markImage.gameObject:SetActive(true)
    else
        craftItemCell.markImage.gameObject:SetActive(false)
    end

    self:_RefreshCraftCellAvailable(craftItemCell, true)
end

ManualCraftCtrl._SelectCraftItem = HL.Method(HL.Number) << function(self, csIndex)
    local craftItemCell = self.m_csIndex2craftItemCell[csIndex]
    local luaIdx = LuaIndex(csIndex)
    local craftInfo = self.m_craftInfoList[luaIdx]

    if craftInfo == nil or craftItemCell == nil then
        return
    end

    if self.view.craftContent.curSelectedIndex ~= csIndex then
        if self.m_nowCraftCell ~= nil then
            self.m_nowCraftCell.default.gameObject:SetActive(true)
        end
        self.m_facManualCraftSystem:ReadSingleCraft(craftInfo.id)
        craftItemCell.default.redDot:InitRedDot("ManualCraftItem", craftInfo.id)
        craftItemCell.default.gameObject:SetActive(false)
        craftItemCell.selected.gameObject:SetActive(true)
        self.m_nowCraftCell = craftItemCell
        self.view.craftContent:SetSelectedIndex(csIndex)
        self.view.rightBar.gameObject:GetComponent(typeof(CS.Beyond.UI.UIAnimationWrapper)):PlayInAnimation()
    end
end

ManualCraftCtrl._RefreshCraftCellAvailable = HL.Method(HL.Any, HL.Boolean) << function(self, inCraftItemCell, clearTween)
    local craftAvailable = self:_CheckFormulaAvailable(inCraftItemCell.id)
    if craftAvailable then
        inCraftItemCell.selected.craftableText.gameObject:SetActive(true)
        inCraftItemCell.default.craftableText.gameObject:SetActive(true)
        inCraftItemCell.default.insufficientText.gameObject:SetActive(false)
        inCraftItemCell.selected.insufficientText.gameObject:SetActive(false)

        inCraftItemCell.selected.craftableText.text = Language.LUA_CRAFT_AVAILABLE
        inCraftItemCell.selected.craftableText.color = self.view.config.NORMAL_NUM_COLOR
        inCraftItemCell.default.craftableText.color = self.view.config.NORMAL_NUM_COLOR
        inCraftItemCell.default.craftableText.text = Language.LUA_CRAFT_AVAILABLE
    else
        inCraftItemCell.selected.craftableText.gameObject:SetActive(false)
        inCraftItemCell.default.craftableText.gameObject:SetActive(false)
        inCraftItemCell.default.insufficientText.gameObject:SetActive(true)
        inCraftItemCell.selected.insufficientText.gameObject:SetActive(true)

        inCraftItemCell.selected.insufficientText.text = Language.LUA_CRAFT_NOT_AVAILABLE
        inCraftItemCell.selected.insufficientText.color = self.view.config.CRAFT_NOT_AVAILABLE_TEXT_COLOR
        inCraftItemCell.default.insufficientText.color = self.view.config.CRAFT_NOT_AVAILABLE_TEXT_COLOR
        inCraftItemCell.default.insufficientText.text = Language.LUA_CRAFT_NOT_AVAILABLE
    end

    local color1 = inCraftItemCell.default.itemIcon.color
    local color2 = inCraftItemCell.selected.itemIcon.color


    if craftAvailable then
        color1.a = UIConst.ITEM_EXIST_TRANSPARENCY
        color2.a = UIConst.ITEM_EXIST_TRANSPARENCY
        inCraftItemCell.default.itemIcon.color = color1
        inCraftItemCell.selected.itemIcon.color = color2

    else
        color1.a = UIConst.ITEM_MISSING_TRANSPARENCY
        color2.a = UIConst.ITEM_MISSING_TRANSPARENCY
        inCraftItemCell.default.itemIcon.color = color1
        inCraftItemCell.selected.itemIcon.color = color2
    end
end

ManualCraftCtrl._SelectCraft = HL.Method(HL.String) << function(self, craftId)
    local lastSelectedCraftId = self.m_selectedCraftId
    self.m_selectedCraftId = craftId
    self:_PlayCraftListSelectEffect(lastSelectedCraftId)
    self:_RefreshCraftNode(true)
end


ManualCraftCtrl._OnItemClick = HL.Method(HL.Number, HL.Any) << function(self, luaIndex, itemId)
    if PhaseManager:GetTopPhaseId() ~= PhaseId.ManualCraft then
        return
    end
    local rewardCell = self.m_workshopList:Get(luaIndex)
    local rewardCell1 = self.m_workshopList:Get(1)
    local rewardCell2 = self.m_workshopList:Get(2)
    local rewardCell3 = self.m_workshopList:Get(3)
    local posInfo
    if DeviceInfo.usingController then
        if itemId == StaminaItemId then
            self.m_canJumpStamina = true
            posInfo = {
                tipsPosType = UIConst.UI_TIPS_POS_TYPE.RightMid,
                isSideTips = true,
                keyHintGroupIds = {self.view.openStaminaButton.groupId},
            }
        else
            self.m_canJumpStamina = false
            posInfo = {
                tipsPosType = UIConst.UI_TIPS_POS_TYPE.RightMid,
                isSideTips = true,
            }
        end
    end

    rewardCell.itemBigBlack:ShowTips(posInfo)
end

ManualCraftCtrl._RefreshCraftNode = HL.Method(HL.Opt(HL.Boolean)) << function(self, needResetManualCount)

    local maxFormulaWorkTimes = 0
    local success, craftInfo = Tables.factoryManualCraftTable:TryGetValue(self.m_selectedCraftId)
    if not success then
        return
    end

    self.m_workshopList:Refresh(3, function(cell, index)
        if  index <= craftInfo.ingredients.Count then
            local ingredientItem = craftInfo.ingredients[index - 1]

            cell.itemBigBlack.gameObject:SetActive(true)
            cell.itemBigBlack:InitItem({id = ingredientItem.id, count = 1}, function()
                self:_OnItemClick(index, ingredientItem.id)
            end)
            cell.itemBigBlack:SetExtraInfo({ isSideTips = DeviceInfo.usingController })

            cell.itemBigBlack.canUse = false
            cell.emptyBG.gameObject:SetActive(false)
            cell.commonStorageNodeNew.gameObject:SetActive(true)
            local inventoryCount = self:_GetItemCount(ingredientItem.id)
            if index == 1 then
                maxFormulaWorkTimes = inventoryCount // ingredientItem.count
            else
                maxFormulaWorkTimes = math.min(maxFormulaWorkTimes, inventoryCount // ingredientItem.count)
            end

            if ingredientItem.id == StaminaItemId then
                cell.commonStorageNodeNew.view.addButton.gameObject:SetActive(true)
                cell.commonStorageNodeNew.view.addButton.onClick:RemoveAllListeners()
                cell.commonStorageNodeNew.view.addButton.onClick:AddListener(function()
                    PhaseManager:OpenPhase(PhaseId.StaminaPopUp)
                end)
            else
                cell.commonStorageNodeNew.view.addButton.gameObject:SetActive(false)
            end

        else
            cell.itemBigBlack.gameObject:SetActive(false)
            cell.emptyBG.gameObject:SetActive(true)
            cell.commonStorageNodeNew.gameObject:SetActive(false)
        end
    end)

    maxFormulaWorkTimes = math.max(maxFormulaWorkTimes, 1)
    maxFormulaWorkTimes = math.min(maxFormulaWorkTimes, MAX_APPEND_MANUFACTURE_COUNT_LIMIT)

    if success and craftInfo.outcomes.Count > 0 then
        local outcomeItem = craftInfo.outcomes[0].id
        local item = Tables.itemTable:GetValue(outcomeItem)
        if item.type == GEnums.ItemType.CardExp then

        end

        self.view.currentIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, item.iconId)
        self.m_itemDescNodeId = item.id
        self.view.itemDescNode:InitItemDescNode(self.m_itemDescNodeId)
        if self.m_selectedCraftTabType == CraftShowingType.SpecialManualCraft then
            self.view.portableDeviceTagNode.gameObject:SetActive(true)
            self.view.portableDeviceTagNode:InitPortableDeviceTagNode(self.m_itemDescNodeId)
        else
            self.view.portableDeviceTagNode.gameObject:SetActive(false)
        end

        self.view.mainTitle.text = item.name
        local itemTypeName = UIUtils.getItemTypeName(outcomeItem)
        self.view.subtitleText.text = itemTypeName
    end

    local c = 1
    if not needResetManualCount then
        c = math.min(self.m_manualCount, maxFormulaWorkTimes)
    end

    self.view.numberSelector_New:InitNumberSelector(c, 1, maxFormulaWorkTimes, function(cntCount)
        self.m_manualCount = cntCount
        self:_RefreshCraftCount()
    end, false, 0)

    UIUtils.setItemRarityImage(self.view.qualityLight, Tables.itemTable:GetValue(craftInfo.outcomes[0].id).rarity)
end

ManualCraftCtrl.OnManualCraftPopupPanelClose = HL.Method() << function(self)
    if string.isEmpty(self.m_itemDescNodeId) then
        return
    end
    self.view.itemDescNode:InitItemDescNode(self.m_itemDescNodeId)
    if self.m_selectedCraftTabType == CraftShowingType.SpecialManualCraft then
        self.view.portableDeviceTagNode.gameObject:SetActive(true)
        self.view.portableDeviceTagNode:InitPortableDeviceTagNode(self.m_itemDescNodeId)
    else
        self.view.portableDeviceTagNode.gameObject:SetActive(false)
    end
end


ManualCraftCtrl.OnItemCountChanged = HL.Method(HL.Any) << function(self, args)
    if string.isEmpty(self.m_selectedCraftId) then
        return
    end
    local changedItemId2DiffCount, _ = unpack(args)
    local manualCraftData = Tables.factoryManualCraftTable
    local success, craftInfo = manualCraftData:TryGetValue(self.m_selectedCraftId)
    local needRefreshCount = false
    if success then
        for i = 1, craftInfo.ingredients.Count do
            if changedItemId2DiffCount:ContainsKey(craftInfo.ingredients[i-1].id) then
                needRefreshCount = true
                break
            end
        end
        if changedItemId2DiffCount:ContainsKey(craftInfo.outcomes[0].id) then
            needRefreshCount = true
        end
    end
    if needRefreshCount then
        self:_RefreshCraftAllNode()
    end
    self:_RefreshAllCraftCellAvailable(changedItemId2DiffCount)
end


ManualCraftCtrl._RefreshCraftAllNode = HL.Method() << function(self)
    self:_RefreshCraftCount()
    self:_RefreshCraftNode()
end

ManualCraftCtrl._RefreshAllCraftCellAvailable = HL.Method(HL.Any) << function(self, changedItemId2DiffCount)
    if self.m_allIngredientsForDisplayCraft then
        for itemId, _ in pairs(changedItemId2DiffCount) do
            if self.m_allIngredientsForDisplayCraft[itemId] then
                for i = 1, #self.m_craftInfoList do
                    local gameObject = self.view.craftContent:Get(CSIndex(i))
                    if gameObject then
                        local craftCell = self.m_getCraftCellFunc(gameObject)
                        if craftCell then
                            self:_RefreshCraftCellAvailable(craftCell, false)
                        end
                    end
                end
                break
            end
        end
    end
end

ManualCraftCtrl._RefreshCraftCount = HL.Method() << function(self)
    self:_RefreshStartCraftBtn()

    if string.isEmpty(self.m_selectedCraftId) then
        return
    end
    local manualCraftData = Tables.factoryManualCraftTable
    local success, craftInfo = manualCraftData:TryGetValue(self.m_selectedCraftId)
    if success then
        self.m_workshopList:Refresh(3, function(cell, index)
            if index <= craftInfo.ingredients.Count then
                local itemId = craftInfo.ingredients[CSIndex(index)].id
                local count = craftInfo.ingredients[CSIndex(index)].count
                local demandCount = math.floor(count * self.m_manualCount)
                local inventoryCount = self:_GetItemCount(itemId)
                cell.itemBigBlack:UpdateCountSimple(demandCount, demandCount > inventoryCount)
                local ignoreInSafeZone = true
                if itemId == StaminaItemId then
                    ignoreInSafeZone = true
                end
                UIUtils.setItemStorageCountText(cell.commonStorageNodeNew, itemId, count, ignoreInSafeZone)
            end
        end)

        if craftInfo.outcomes.Count > 0 then
            local outcomeItem = craftInfo.outcomes[0]
            local ignoreInSafeZone = true
            UIUtils.setItemStorageCountText(self.view.commonStorageNodeNew, outcomeItem.id, 1, ignoreInSafeZone)
            local outcomeCount = math.floor(outcomeItem.count * self.m_manualCount)
            self.view.curNumberText.text = outcomeCount
        end
    end
end

ManualCraftCtrl._RefreshStartCraftBtn = HL.Method() << function(self)
    local manufactureData = self.m_facManualCraftSystem.manufactureData:GetOrFallback(Utils.getCurrentScope())
    local available = not string.isEmpty(self.m_selectedCraftId) and self:_CheckFormulaAvailable(self.m_selectedCraftId)
    if available then
        self.view.btnCommon.gameObject:SetActive(true)
        if self.m_manualCount > 0 and manufactureData.queue.Count < MAX_APPEND_MANUFACTURE_COUNT_LIMIT then
            self.view.btnCommon.interactable = true
        else
            self.view.btnCommon.interactable = false
        end
        self.view.notEnoughBtn.gameObject:SetActive(false)
    else
        self.view.btnCommon.gameObject:SetActive(false)
        self.view.notEnoughBtn.gameObject:SetActive(true)
    end
end

ManualCraftCtrl._PlayCraftListSelectEffect = HL.Method(HL.String) << function(self, lastSelectedCraftId)

    for idx, craftInfo in ipairs(self.m_craftInfoList) do
        local gameObject = self.view.craftContent:Get(CSIndex(idx))
        if gameObject then
            local craftCell = self.m_getCraftCellFunc(gameObject)
            local craftAvailable = self:_CheckFormulaAvailable(craftInfo.id)
            if craftInfo.id == self.m_selectedCraftId then
                craftCell.animationWrapper:PlayInAnimation()
            else
                if craftInfo.id == lastSelectedCraftId then
                    local cell = craftCell
                    cell.default.gameObject:SetActive(true)
                    craftCell.animationWrapper:PlayOutAnimation(function()
                        if cell ~= self.m_nowCraftCell and self.m_nowCraftCell then
                            cell.selected.gameObject:SetActive(false)
                        end
                    end)
                end
            end
        end
    end
end

ManualCraftCtrl._RefreshMakingState = HL.Method() << function(self)

end

ManualCraftCtrl._ToggleFabricateSound = HL.Method(HL.Boolean) << function(self, isOn)
    if isOn then
        if self.m_fabricateSoundKey == 0 then
            self.m_fabricateSoundKey = AudioManager.PostEvent("au_ui_fac_manualcraft_fabricate")
        end
    else
        if self.m_fabricateSoundKey ~= 0 then
            AudioManager.StopSoundByPlayingId(self.m_fabricateSoundKey)
            self.m_fabricateSoundKey = 0
        end
    end
end


ManualCraftCtrl._RefreshfilterNaviSelected = HL.Method() << function(self)
    self.m_filterCells:Update(function(cell, index)
        cell.controllerSelectedHintNode.gameObject:SetActive(index == self.m_filterCurNaviIndex)
    end)
end

ManualCraftCtrl._RefreshManufactureList = HL.Method() << function(self)

end

ManualCraftCtrl._GetIngredientItems = HL.Method(HL.String, HL.Number).Return(HL.Table) << function(self, formulaId, count)
    local manualCraftTable = Tables.factoryManualCraftTable
    local success, craftInfo = manualCraftTable:TryGetValue(formulaId)
    local ret = {}
    if success then
        for i, v in pairs(craftInfo.ingredients) do
            table.insert(ret, {id = v.id, count = v.count * count})
        end
    end
    return ret
end

ManualCraftCtrl._GetOutcomeItems = HL.Method(HL.String, HL.Number).Return(HL.Table) << function(self, formulaId, count)
    local manualCraftTable = Tables.factoryManualCraftTable
    local success, craftInfo = manualCraftTable:TryGetValue(formulaId)
    local ret = {}
    if success then
        for i, v in pairs(craftInfo.outcomes) do
            table.insert(ret, {id = v.id, count = v.count * count})
        end
    end
    return ret
end

ManualCraftCtrl._GetItemCount = HL.Method(HL.String).Return(HL.Number) << function(self, itemId)
    if itemId == StaminaItemId then
        local count = GameInstance.player.inventory.curStamina
        return count
    else
        local count = Utils.getItemCount(itemId, true, true)
        return count
    end
end

ManualCraftCtrl._OpenCommonFilter = HL.Method() << function(self)
    self.view.numberSelector_New:UpdateKeyHintVisible(false)
end


ManualCraftCtrl._CloseCommonFilter = HL.Method() << function(self)
    self.view.numberSelector_New:UpdateKeyHintVisible(true)
end

ManualCraftCtrl._IsValuableItem = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    local itemData = Tables.itemTable[itemId]
    local valuableDepotType = itemData.valuableTabType
    if valuableDepotType ~= CS.Beyond.GEnums.ItemValuableDepotType.Factory then
        return true
    else
        return false
    end
end


ManualCraftCtrl._CheckFormulaAvailable = HL.Method(HL.String).Return(HL.Boolean) << function(self, formulaId)
    local manualCraftTable = Tables.factoryManualCraftTable
    local success, craftInfo = manualCraftTable:TryGetValue(formulaId)
    if success then
        local needItems = self:_GetIngredientItems(formulaId, 1)
        for _, item in pairs(needItems) do
            local inventoryCount = self:_GetItemCount(item.id)
            if inventoryCount < item.count then
                return false
            end
        end

    end
    return true
end

ManualCraftCtrl._OnStaminaChanged = HL.Method() << function(self)
    self:_RefreshCraftAllNode()
    self:_RefreshAllCraftCellAvailable({StaminaItemId=1})
end


ManualCraftCtrl.OnManualWorkModify = HL.Method(HL.Any) << function(self, arg)
    local manufactureData = self.m_facManualCraftSystem.manufactureData:GetOrFallback(Utils.getCurrentScope())
    if manufactureData.inBlock then
        GameAction.ShowUIToast(Language.LUA_BAG_FULL)
    end




    local info = {
        title = Language.LUA_FAC_CRAFT_ITEM_SUCCESS_MAKE,
        onComplete = function()
        end,
    }
    arg = arg[1]
    local manualCraftTable = Tables.factoryManualCraftTable
    local success, craftInfo = manualCraftTable:TryGetValue(arg.FormulaId)
    info.items = {}
    local outItems = self:_GetOutcomeItems(arg.FormulaId, arg.Count)
    for _, item in pairs(outItems) do
        table.insert(info.items, {
            id = item.id,
            count = item.count,
        })
    end
    local _arg = {info, craftInfo.itemId, self:_GetIngredientItems(arg.FormulaId, arg.Count)}
    UIManager:Open(PanelId.CompositeToast, _arg)

    self:_RefreshCraftNode()
    self:_RefreshManufactureList()
end

ManualCraftCtrl.OnGetNewManualFormula = HL.StaticMethod(HL.Any) << function(args)
    local newFormulaIds = unpack(args)
    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if isOpen then
        ctrl:_OnGetNewManualFormula(args)
        ctrl:_RefreshCraftNode()
        ctrl:_RefreshManufactureList()
    else
        if newFormulaIds.Count == 1 then
            local _, craftInfo = Tables.factoryManualCraftTable:TryGetValue(newFormulaIds[0])
            if craftInfo then
                Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_CRAFT_UNLOCK, craftInfo.name))
            end
        elseif newFormulaIds.Count > 1 then
            local _, craftInfo = Tables.factoryManualCraftTable:TryGetValue(newFormulaIds[0])
            if craftInfo then
                Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_MULTIPLE_CRAFT_UNLOCK, craftInfo.name, newFormulaIds.Count))
            end
        end
    end

end

ManualCraftCtrl._OnGetNewManualFormula = HL.Method(HL.Any) << function(self, args)
    self.itemNaviFlag = true
    self.m_initSelectCsIndex = 0
    local newFormulaIds = unpack(args)
    for _, formulaId in pairs(newFormulaIds) do
        local _, formulaData = Tables.factoryManualCraftTable:TryGetValue(formulaId)
        if formulaData then
            for i, k in pairs(filterList) do
                if k.type == formulaData.showingType and not self.m_validTabFilterTypeDict[k.type] then
                    self:_UpdateTabScrollList()
                    self:_UpdateTabKeyHint()
                end
            end

            self:_StartTimer(1, function()
                Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_CRAFT_UNLOCK, formulaData.name))
            end)
        end
    end

    if self.m_selectedCraftTabType == nil then
        self:_ClickTab(1)
    else
        self:_RefreshCraftList()
    end
end

ManualCraftCtrl.OnUnlockManualCraft = HL.StaticMethod(HL.Any) << function(args)
    local newItems = unpack(args)
    local info = {
        title = Language.LUA_FAC_MANUAL_CRAFT_UNLOCK,
        subTitle = Language.LUA_LOST_AND_FOUND_GET_ALL,
        onComplete = function()
        end,
    }
    info.items = {}
    for _, v in pairs(newItems) do
        local id = Tables.factoryManualCraftFormulaUnlockTable:GetValue(v).rewardItemId1
        table.insert(info.items, {
            id = id,
            count = 1,
        })
    end
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, info)
end

ManualCraftCtrl.OnManualWorkCancel = HL.Method(HL.Any) << function(self, arg)
    local backItems, breakItems = unpack(arg)
    local showItems = {}
    for itemId, itemCount in pairs(backItems) do
        table.insert(showItems, { id = itemId, count = itemCount})
    end
    if self.m_fabricateSoundKey ~= 0 then
        AudioManager.StopSoundByPlayingId(self.m_fabricateSoundKey)
        self.m_fabricateSoundKey = 0
    end
    AudioManager.PostEvent("au_ui_fac_manualcraft_terminate")
    GameAction.ShowUIToast(Language.LUA_MANUAL_WORK_HAS_BEEN_CANCELLED)
end

ManualCraftCtrl.OnShow = HL.Override() << function(self)
    self:_RefreshMakingState()
end
ManualCraftCtrl.OnHide = HL.Override() << function(self)
    self:_ToggleFabricateSound(false)

end
ManualCraftCtrl.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    return {
        selectedCraftId = self.m_selectedCraftId,
        manualCount = self.m_manualCount,
        sortMode = self.m_sortMode,
        sortIncremental = self.m_sortIncremental,
        useStamina = self.m_useStamina,
    }
end

ManualCraftCtrl.OnClose = HL.Override() << function(self)
    local craftIds = {}
    for craftId, _ in pairs(self.m_readCraftIds) do
        table.insert(craftIds, craftId)
    end
    self.m_facManualCraftSystem:ReadCrafts(craftIds)
    self:_ToggleFabricateSound(false)
    self:Notify(MessageConst.ON_ENABLE_COMMON_TOAST)
    Notify(MessageConst.ON_MANUAL_CRAFT_PANEL_CLOSE)
    self.m_facManualCraftSystem:CloseManualCraftPanel()
end

ManualCraftCtrl.OnAnimationInFinished = HL.Override() << function(self)
    local obj = self.view.craftContent:Get(0)
    if obj then
        InputManagerInst:MoveVirtualMouseTo(obj.transform, self.uiCamera)
    end
end


ManualCraftCtrl._RefreshConvertRedDot = HL.Method() << function(self)
    if self.view.switchStaminaRedDot and self.m_selectedCraftTabType == CraftShowingType.ManualCraftConvert then
        self.view.switchStaminaRedDot:InitRedDot("ManualCraftConvert")
    end
end




ManualCraftCtrl._InitCraftConvertFilterOptions = HL.Method() << function(self)
    self.m_craftConvertFilterOptions = {}

    table.insert(self.m_craftConvertFilterOptions, {
        id = CS.Beyond.GEnums.ManualCraftFilterType.CharUpgrade,
        name = Language.LUA_MANUAL_CRAFT_FILTER_CHAR_UPGRADE
    })

    table.insert(self.m_craftConvertFilterOptions, {
        id = CS.Beyond.GEnums.ManualCraftFilterType.SkillEnhancement,
        name = Language.LUA_MANUAL_CRAFT_FILTER_SKILL_ENHANCEMENT
    })

    table.insert(self.m_craftConvertFilterOptions, {
        id = CS.Beyond.GEnums.ManualCraftFilterType.WeaponBreakthrough,
        name = Language.LUA_MANUAL_CRAFT_FILTER_WEAPON_BREAKTHROUGH
    })

    for index, info in ipairs(self.m_craftConvertFilterOptions) do
        local keyName = GameInstance.player.roleId.."ManualCraft.Filter.ConvertTab." .. index
        local _, value = ClientDataManagerInst:GetInt(keyName, false, false and 1 or 0)
        info.isOn = value == 1
    end
end


ManualCraftCtrl._InitDomainFilterOptions = HL.Method() << function(self)
    self.m_domainFilterOptions = {}
    local list = self.m_facManualCraftSystem:GetAllDomainData()
    for i = 0 , list.Count - 1 do
        local domainData = list[i]
        table.insert(self.m_domainFilterOptions, {
            id = domainData.domainId,
            name = domainData.domainName
        })
    end

    for index, info in ipairs(self.m_domainFilterOptions) do
        local keyName = GameInstance.player.roleId.."ManualCraft.Filter.Tab." .. index
        local _, value = ClientDataManagerInst:GetInt(keyName, false, false and 1 or 0)
        self.m_domainFilterOptions[index].isOn = value == 1
    end
end


ManualCraftCtrl._initFilterAndSortNode = HL.Method(HL.Any) << function(self, activeFilter)
    self.view.filterBtn.gameObject:SetActive(activeFilter)

    local selectedDomainFilter = {}
    for _, v in ipairs(self.m_domainFilterOptions) do
        if v.isOn then
            table.insert(selectedDomainFilter, v)
        end
    end

    local selectedCraftConvertFilter = {}
    for _, v in ipairs(self.m_craftConvertFilterOptions) do
        if v.isOn then
            table.insert(selectedCraftConvertFilter, v)
        end
    end

    local useFilterTags = {}
    local selectedFilter = {}
    if self.m_curFilterMode == "Domain" then
        useFilterTags = self.m_domainFilterOptions
        selectedFilter = selectedDomainFilter
    elseif self.m_curFilterMode == "ManualCraftConvert" then
        useFilterTags = self.m_craftConvertFilterOptions
        selectedFilter = selectedCraftConvertFilter
    end


    if not activeFilter then
        selectedFilter = {}
    end


    self.view.filterBtn:InitFilterBtn({
        tagGroups = {{tags = useFilterTags}},
        selectedTags = selectedFilter,
        onConfirm = function(tags)
            self:_FilterBtnConfirm(tags)
            self:_ApplySort(self.view.sortNodeUp:GetCurSortData(), self.view.sortNodeUp.isIncremental)
        end,
        getResultCount = function(tags)
            return self:_FilterBtnGetResCount(tags)
        end,
        sortNodeWidget = self.view.sortNodeUp,
    })

    if activeFilter then
        self.view.sortNodeUp:InitSortNode(UIConst.ManualCraftSortOptions, function(optData, isIncremental)
            self:_ApplySort(optData, isIncremental)
        end, self.m_sortMode - 1, self.m_sortIncremental, true, self.view.filterBtn)
    else
        self.view.sortNodeUp:InitSortNode(UIConst.ManualCraftSortOptions, function(optData, isIncremental)
            self:_ApplySort(optData, isIncremental)
        end, self.m_sortMode - 1, self.m_sortIncremental, true)
    end
end


ManualCraftCtrl._FilterBtnConfirm = HL.Method(HL.Any) << function(self, tags)
    local checkOptions = {}
    local prefix = ""
    if self.m_curFilterMode == "Domain" then
        checkOptions = self.m_domainFilterOptions
        prefix = "ManualCraft.Filter.Tab."
    elseif self.m_curFilterMode == "ManualCraftConvert" then
        checkOptions = self.m_craftConvertFilterOptions
        prefix = "ManualCraft.Filter.ConvertTab."
    end

    for i = 1, #checkOptions do
        checkOptions[i].isOn = false
    end

    if tags ~= nil then
        for i = 1,#tags do
            for j = 1,#checkOptions do
                if checkOptions[j].id == tags[i].id then
                    checkOptions[j].isOn = true
                end
            end
        end
    end

    for i = 1, #checkOptions do
        local value = checkOptions[i].isOn and 1 or 0
        local keyName = GameInstance.player.roleId..prefix .. i
        ClientDataManagerInst:SetInt(keyName, value, false, EClientDataTimeValidType.Permanent)
    end

end

ManualCraftCtrl._FilterBtnGetResCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    local noSelect = #tags == 0
    local formulaList = self.m_facManualCraftSystem:GetUnlockedFormulaByType(self.m_selectedCraftTabType)

    if noSelect then
        if self.m_curFilterMode == "Domain" then
            return formulaList.Count
        elseif self.m_curFilterMode == "ManualCraftConvert" then
            return formulaList.Count//2
        else
            return 0
        end
    end
    local count = 0
    local manualCraftData = Tables.factoryManualCraftTable
    if formulaList ~= nil then
        for _, formulaId in pairs(formulaList) do
            local success, manualCraftInfo = manualCraftData:TryGetValue(formulaId)
            if success == true then
                for j = 1,#tags do
                    if self.m_curFilterMode == "Domain" then
                        if (tags[j].id == manualCraftInfo.domainId) then
                            count = count + 1
                        end
                    elseif self.m_curFilterMode == "ManualCraftConvert" then
                        if (tags[j].id == manualCraftInfo.craftFilterType) then
                            count = count + 1
                        end
                    end
                end
            end
        end
    end
    if self.m_curFilterMode == "Domain" then
        return count
    elseif self.m_curFilterMode == "ManualCraftConvert" then
        return count//2
    else
        return 0
    end
end


ManualCraftCtrl._CheckIsNoSelectFilter = HL.Method().Return(HL.Boolean) << function(self)
    local noSelectFilter = true

    if self.m_curFilterMode == "Domain" then
        for _, info in pairs(self.m_domainFilterOptions) do
            if info.isOn then
                noSelectFilter = false
                break
            end
        end
    elseif self.m_curFilterMode == "ManualCraftConvert" then
        for _, info in pairs(self.m_craftConvertFilterOptions) do
            if info.isOn then
                noSelectFilter = false
                break
            end
        end
    end
    return noSelectFilter
end


ManualCraftCtrl._AddCraftInfoByFilter = HL.Method(HL.Any) << function(self, manualCraftInfo)
    if self.m_curFilterMode == "Domain" then
        for j = 1,#self.m_domainFilterOptions do
            if not self.m_validDomainFilter[manualCraftInfo.domainId] or (self.m_domainFilterOptions[j].id == manualCraftInfo.domainId and self.m_domainFilterOptions[j].isOn) then
                table.insert(self.m_craftInfoList, manualCraftInfo)
            end
        end
    elseif self.m_curFilterMode == "ManualCraftConvert" then
        for j = 1,#self.m_craftConvertFilterOptions do
            if (self.m_craftConvertFilterOptions[j].id == manualCraftInfo.craftFilterType and self.m_craftConvertFilterOptions[j].isOn) then
                table.insert(self.m_craftInfoList, manualCraftInfo)
            end
        end
    end
end




HL.Commit(ManualCraftCtrl)
