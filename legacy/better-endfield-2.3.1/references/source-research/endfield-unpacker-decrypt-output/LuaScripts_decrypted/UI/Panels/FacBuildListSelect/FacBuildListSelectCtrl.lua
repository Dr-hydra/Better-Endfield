
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacBuildListSelect
local QuickBarItemType = FacConst.QuickBarItemType

local MainState = {
    Fac = "Fac",
    Hub = "Hub",
    Blackbox = "Blackbox",
}
local MAXIMUM_MAKE_COUNT = 999

FacBuildListSelectCtrl = HL.Class('FacBuildListSelectCtrl', uiCtrl.UICtrl)






FacBuildListSelectCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ITEM_COUNT_CHANGED] = 'OnItemCountChanged',
    [MessageConst.ON_HUB_CRAFT_SUCC] = 'OnHubCraftSucc',
    [MessageConst.FAC_SCROLL_TO_BUILDLIST_TARGET_ITEM] = "OnActionScrollToTarget",
}

FacBuildListSelectCtrl.s_lastSelectInfo = HL.StaticField(HL.Table)

FacBuildListSelectCtrl.m_typeInfos = HL.Field(HL.Table)

FacBuildListSelectCtrl.m_showListInfos = HL.Field(HL.Table)

FacBuildListSelectCtrl.m_selectedTypeIndex = HL.Field(HL.Number) << 1

FacBuildListSelectCtrl.m_getCell = HL.Field(HL.Function)

FacBuildListSelectCtrl.m_getTabCell = HL.Field(HL.Function)

FacBuildListSelectCtrl.m_curCraftModeState = HL.Field(HL.Boolean) << false

FacBuildListSelectCtrl.m_curDecoBuildingState = HL.Field(HL.Boolean) << false

FacBuildListSelectCtrl.m_lastPlaceModeSelectId = HL.Field(HL.String) << ""

FacBuildListSelectCtrl.m_lastCraftModeSelectId = HL.Field(HL.String) << ""

FacBuildListSelectCtrl.m_sortOptions = HL.Field(HL.Table)

FacBuildListSelectCtrl.m_sortData = HL.Field(HL.Table)

FacBuildListSelectCtrl.m_sortIncremental = HL.Field(HL.Boolean) << false

FacBuildListSelectCtrl.m_onlyCraftNode = HL.Field(HL.Boolean) << false

FacBuildListSelectCtrl.m_bluePrintMode = HL.Field(HL.Boolean) << false

FacBuildListSelectCtrl.m_filterTags = HL.Field(HL.Any)

FacBuildListSelectCtrl.m_bluePrintData = HL.Field(HL.Table)

FacBuildListSelectCtrl.m_waitingForCraftData = HL.Field(HL.Table)


FacBuildListSelectCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_waitingForCraftData = {}
    self.m_bluePrintData = {}
    self.m_bluePrintMode = false
    if arg then
        if arg.onlyCraftNode then
            self.m_onlyCraftNode = true
        end
        if arg.bluePrintData ~= nil then
            self.m_onlyCraftNode = true
            self.m_bluePrintMode = true
            FacBuildListSelectCtrl.s_lastSelectInfo = { "all" }
            for _, data in ipairs(arg.bluePrintData) do
                self.m_bluePrintData[data.id] = data.count
            end
        end
        if arg.selectedId ~= nil then
            if not FactoryUtils.isDecoBuildingItem(arg.selectedId) then
                FacBuildListSelectCtrl.s_lastSelectInfo = { "all", arg.selectedId }


                self.m_curCraftModeState = not Utils.isInBlackbox() and Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacHub)
            end
        end
    end
    local recoverState = self:_ProcessRecoverStateArg(arg and arg.recoverState)
    local isInBlackbox = Utils.isInBlackbox()
    local isInDungeon = Utils.isInDungeon()
    self.view.mainController:SetState(self.m_onlyCraftNode and MainState.Hub or
        ((isInBlackbox or isInDungeon) and MainState.Blackbox or MainState.Fac))
    if DeviceInfo.usingController or self.m_onlyCraftNode then
        self.view.dragTips.gameObject:SetActiveIfNecessary(false)
    end

    self.view.buildingCommon.view.socialNode.gameObject:SetActive(false)
    self:_InitBuildListController()


    if not self.m_onlyCraftNode then
        self.view.placeNode.confirmBtn.onClick:AddListener(function()
            self:_OnClickConfirm()
        end)
        self.view.placeNode.emptyBtn.onClick:AddListener(function()
            self.view.commonToggle:Toggle()
        end)
        self.view.commonToggle:InitCommonToggle(function(isOn)
            self:_OnChangeModeToggle(isOn)
        end, not self.m_curCraftModeState, true)
        self.view.commonToggle:SetCustomAnimation("common_toggle_to_left02", "common_toggle_to_right02")
        if isInBlackbox or not Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacHub) then
            self.view.commonToggle:ToggleInteractable(false)
            self.view.canCraftImg.gameObject:SetActiveIfNecessary(false)
            self.view.disCraftImg.gameObject:SetActiveIfNecessary(true)
        end
        self.view.facQuickBarPlaceholder:InitFacQuickBarPlaceHolder({
            controllerSwitchArgs = {
                hintPlaceholder = self.view.controllerHintPlaceholder
            }
        })

        if isInBlackbox or isInDungeon then
            self.view.contentStateController:SetState("Industry")
            self.view.typeToggle.gameObject:SetActiveIfNecessary(false)
            self.view.decoBuildingRoot.gameObject:SetActiveIfNecessary(false)
        else
            self.view.decoBuildingRoot:InitFacBuildListDecoBuildingInfoNode(self.view.inputGroup.groupId, arg.selectedId)
            self.view.typeToggle.gameObject:SetActiveIfNecessary(true)
            local isShowDecoBuilding = (arg and arg.showLastType) and LuaSystemManager.factory:FacBuildListSelectIsShowDecoBuilding() or false
            if arg.selectedId ~= nil and FactoryUtils.isDecoBuildingItem(arg.selectedId) then
                isShowDecoBuilding = true
            end
            self.view.typeToggle:InitCommonToggle(function(isOn)
                self:_OnChangeTypeToggle(isOn)
            end, not isShowDecoBuilding, true)
            self:_OnChangeTypeToggle(not isShowDecoBuilding)

            self.view.typeToggle:SetCustomAnimation("common_toggle_to_left04", "common_toggle_to_right04")
            self.view.typeToggle.view.industryRedDot:InitRedDot("FacBuildModeIndustry")
            self.view.typeToggle.view.decoRedDot:InitRedDot("FacBuildModeDecorate")
        end
    else
        self.view.contentStateController:SetState("Industry")
        self.view.typeToggle.gameObject:SetActiveIfNecessary(false)
        self.view.decoBuildingRoot.gameObject:SetActiveIfNecessary(false)
    end

    self.m_curCraftModeState = self.m_curCraftModeState or self.m_onlyCraftNode
    self.view.placeNode.gameObject:SetActiveIfNecessary(not self.m_curCraftModeState)
    self.view.craftNode.gameObject:SetActiveIfNecessary(self.m_curCraftModeState)
    self.view.commonToggle.gameObject:SetActiveIfNecessary(not self.m_onlyCraftNode)

    self.view.topNode.gameObject:SetActiveIfNecessary(self.m_bluePrintMode)
    local depotKey = self.m_bluePrintMode and "ui_common_item_tips_own" or "ui_fac_common_storage_have_local"
    self.view.craftNode.depotCountTitle.text = Language[depotKey]
    if self.m_bluePrintMode then
        self.view.toppingToggle.onValueChanged:RemoveAllListeners()
        if recoverState ~= nil and recoverState.toppingToggleIsOn ~= nil then
            self.view.toppingToggle.isOn = recoverState.toppingToggleIsOn
        else
            self.view.toppingToggle.isOn = true
        end
        self.view.toppingToggle.onValueChanged:AddListener(function(isOn)
            self:_UpdateShowListInfos()
            self:_RefreshItemList()
        end)
    end

    self.view.craftNode.outcomeWikiBtn.onClick:AddListener(function()
        self:_OnClickWiki()
    end)
    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.FacBuildListSelect)
    end)
    self.view.craftNode.buildBtn.onClick:AddListener(function()
        self:_OnClickBuild()
    end)

    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)
    self.view.scrollList.onSelectedCell:AddListener(function(obj, csIndex)
        self:_OnClickItem(LuaIndex(csIndex))
    end)
    if not DeviceInfo.usingController then
        self.view.scrollList.onCellSelectedChanged:AddListener(function(obj, csIndex, isSelected)
            local cell = self.m_getCell(obj)
            if cell then
                cell.item:SetSelected(isSelected)
            end
        end)
    end

    self.m_getTabCell = UIUtils.genCachedCellFunction(self.view.tabScrollList)
    self.view.tabScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateTypeCell(self.m_getTabCell(obj), LuaIndex(csIndex))
    end)
    self.view.tabScrollList.onCellSelectedChanged:AddListener(function(obj, csIndex, isSelected)
        local cell = self.m_getTabCell(obj)
        if cell ~= nil then
            cell.default.gameObject:SetActive(not isSelected)
            cell.selected.gameObject:SetActive(isSelected)
            if isSelected then
                cell.animationWrapper:PlayInAnimation()
            else
                cell.animationWrapper:PlayOutAnimation()
            end
        end
    end)

    if UNITY_EDITOR or DEVELOPMENT_BUILD then
        self:BindInputEvent(CS.Beyond.Input.KeyboardKeyCode.A, function()
            local msg = CS.Proto.CS_GM_COMMAND()
            local item = self.m_showListInfos[self.m_selectedTypeIndex][LuaIndex(self.view.scrollList.curSelectedIndex)]
            msg.Command = "AddItemToItemBagSystem " .. item.itemId ..  " 50"
            CS.Beyond.Network.NetBus.instance.defaultSender:Send(msg)
            Notify(MessageConst.SHOW_TOAST, "DEBUG: 已添加道具50个")
        end)
    end

    self:_InitSortAndFilterNode(recoverState)
    self:_InitTypeData()
    self:_RefreshTypeList()

    self:_TryRecoverExtraState(recoverState)
    self:_InitRedDot()
end

FacBuildListSelectCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, isActive)
    if DeviceInfo.usingController then
        self.view.numberReduceKeyhint.gameObject:SetActive(isActive)
        self.view.numberAddKeyhint.gameObject:SetActive(isActive)
    end
end

FacBuildListSelectCtrl.OnPhaseRefresh = HL.Override(HL.Any) << function(self, args)

    if DeviceInfo.usingController then
        self.view.craftNode.incomeNode:ManuallyStopFocus()
    end
end




FacBuildListSelectCtrl.OnHide = HL.Override() << function(self)
    self:_SaveSelectInfo()
    self.view.decoBuildingRoot:OnHide()
end

FacBuildListSelectCtrl.OnClose = HL.Override() << function(self)
    self:_SaveSelectInfo()
    self.view.decoBuildingRoot:OnClose()
end

FacBuildListSelectCtrl._SaveSelectInfo = HL.Method() << function(self)
    if not self.m_typeInfos then
        return
    end
    local curTab = self.m_typeInfos[self.m_selectedTypeIndex]
    local tabId = curTab.data.id
    local item = self.m_showListInfos[self.m_selectedTypeIndex][LuaIndex(self.view.scrollList.curSelectedIndex)]
    if item == nil then
        FacBuildListSelectCtrl.s_lastSelectInfo = { tabId }
    else
        local id = item.itemId
        FacBuildListSelectCtrl.s_lastSelectInfo = { tabId, id }
    end
end

FacBuildListSelectCtrl.GetRecoverStateArg = HL.Method().Return(HL.Table) << function(self)
    local recoverState = {}
    local curTab = self.m_typeInfos and self.m_typeInfos[self.m_selectedTypeIndex]
    if curTab and curTab.data then
        recoverState.tabId = curTab.data.id
    end
    local curList = self.m_showListInfos and self.m_showListInfos[self.m_selectedTypeIndex]
    local item = curList and curList[LuaIndex(self.view.scrollList.curSelectedIndex)]
    if item then
        recoverState.selectedItemId = item.itemId
    end
    if self.m_filterTags then
        recoverState.filterDomainIds = {}
        for domainId, _ in pairs(self.m_filterTags) do
            table.insert(recoverState.filterDomainIds, domainId)
        end
        table.sort(recoverState.filterDomainIds)
    end
    if self.view.sortNode then
        recoverState.sortSelectedIndex = self.view.sortNode:GetCurSelectedIndex()
        recoverState.sortIsIncremental = self.view.sortNode.isIncremental
    end
    if not self.m_onlyCraftNode then
        recoverState.toggleIsOn = self.view.commonToggle.toggle.isOn
    end
    if self.m_bluePrintMode then
        recoverState.toppingToggleIsOn = self.view.toppingToggle.isOn
    end
    if self.m_curCraftModeState and self.view.craftNode.numberSelector.gameObject.activeSelf then
        recoverState.craftCount = self.view.craftNode.numberSelector.curNumber
    end
    return recoverState
end

FacBuildListSelectCtrl._ProcessRecoverStateArg = HL.Method(HL.Opt(HL.Any)).Return(HL.Opt(HL.Any)) << function(self, arg)
    if not arg then
        return nil
    end
    if not string.isEmpty(arg.tabId) then
        if not string.isEmpty(arg.selectedItemId) then
            FacBuildListSelectCtrl.s_lastSelectInfo = { arg.tabId, arg.selectedItemId }
        else
            FacBuildListSelectCtrl.s_lastSelectInfo = { arg.tabId }
        end
    end
    if not self.m_onlyCraftNode and arg.toggleIsOn ~= nil then
        self.m_curCraftModeState = not arg.toggleIsOn
    end
    return arg
end

FacBuildListSelectCtrl._GetRecoverFilterTags = HL.Method(HL.Table, HL.Opt(HL.Any)).Return(HL.Table) << function(self, filterTagGroups, recoverState)
    if not recoverState or not recoverState.filterDomainIds or #recoverState.filterDomainIds == 0 then
        return {}
    end
    local recoverDomainIdMap = {}
    for _, domainId in ipairs(recoverState.filterDomainIds) do
        recoverDomainIdMap[domainId] = true
    end
    local recoverFilterTags = {}
    for _, tagInfo in ipairs(filterTagGroups) do
        if recoverDomainIdMap[tagInfo.domainId] then
            table.insert(recoverFilterTags, tagInfo)
        end
    end
    return recoverFilterTags
end

FacBuildListSelectCtrl._OnChangeModeToggle = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isOn, skipAnimation)
    self.m_curCraftModeState = not isOn
    self.view.placeNode.gameObject:SetActiveIfNecessary(isOn)
    self.view.craftNode.gameObject:SetActiveIfNecessary(not isOn)
    self.view.scrollList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.m_getCell(obj)
        local item = self.m_showListInfos[self.m_selectedTypeIndex][LuaIndex(csIndex)]
        local disNodeShow = (self.m_curCraftModeState and item.type ~= QuickBarItemType.Building)
            or (not self.m_curCraftModeState and item.domainSortGroup < FacConst.DOMAIN_SORT_GROUP.Unsuitable)
        cell.view.disNode.gameObject:SetActiveIfNecessary(disNodeShow)
    end)
    if self.m_curCraftModeState then
        self:_RefreshSelectedCraftNode()
        if not skipAnimation then
            self.view.craftNode.animationWrapper:PlayInAnimation()
        end
    else
        self:_RefreshSelectedPlaceNode()
        if not skipAnimation then
            self.view.placeNode.animationWrapper:PlayInAnimation()
        end
    end
end

FacBuildListSelectCtrl._TryRecoverExtraState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if not recoverState then
        return
    end
    if not self.m_onlyCraftNode and recoverState.toggleIsOn ~= nil then
        self.view.commonToggle:SetValue(recoverState.toggleIsOn, true)
        self:_OnChangeModeToggle(recoverState.toggleIsOn, true)
    end
    if recoverState.craftCount ~= nil and self.m_curCraftModeState then
        local curList = self.m_showListInfos and self.m_showListInfos[self.m_selectedTypeIndex]
        local item = curList and curList[LuaIndex(self.view.scrollList.curSelectedIndex)]
        if item and item.type == QuickBarItemType.Building then
            self.view.craftNode.numberSelector:RefreshNumber(recoverState.craftCount)
        end
    end
end

FacBuildListSelectCtrl._InitSortAndFilterNode = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    local filterTagGroups = {}
    local mapManager = GameInstance.player.mapManager
    for id, domainData in pairs(Tables.domainDataTable) do
        local isDomainUnlocked = false
        for _, levelId in pairs(domainData.levelGroup) do
            if mapManager:IsLevelUnlocked(levelId) then
                isDomainUnlocked = true
                break
            end
        end
        if isDomainUnlocked then
            table.insert(filterTagGroups, {
                name = domainData.domainName,
                domainId = id,
                sort = domainData.sortId
            })
        end
    end
    table.sort(filterTagGroups, Utils.genSortFunction({ "sort" }));

    local recoverFilterTags = self:_GetRecoverFilterTags(filterTagGroups, recoverState)
    if #recoverFilterTags > 0 then
        self.m_filterTags = {}
        for _, tagInfo in ipairs(recoverFilterTags) do
            self.m_filterTags[tagInfo.domainId] = true
        end
    end
    self.view.filterBtn:InitFilterBtn({
        tagGroups = {
            { tags = filterTagGroups }
        },
        selectedTags = recoverFilterTags,
        onConfirm = function(tags)
            if tags then
                self.m_filterTags = {}
                for _, v in ipairs(tags) do
                    self.m_filterTags[v.domainId] = true
                end
            else
                self.m_filterTags = nil
            end
            self:_UpdateShowListInfos()
            self:_RefreshItemList()
        end,
        getResultCount = function(tags)
            return self:_GetContentFilterResultCount(tags)
        end,
        sortNodeWidget = self.view.sortNode
    })

    self.m_sortOptions = {
        {
            name = Language.LUA_FAC_CRAFT_SORT_1,
            keys = { "domainSortGroup", "sortId1", "sortId2", "rarity", "id" },
            reverseKeys = { "domainReverseSort", "sortId1", "sortId2", "rarity", "id" },
        },
        {
            name = Language.LUA_FAC_CRAFT_SORT_2,
            keys = { "domainSortGroup", "rarity", "sortId1", "sortId2", "id" },
            reverseKeys = { "domainReverseSort", "sortId1", "sortId2", "rarity", "id" },
        },
    }

    local sortSelectedIndex = recoverState and recoverState.sortSelectedIndex or 1
    sortSelectedIndex = lume.clamp(sortSelectedIndex, 1, #self.m_sortOptions)
    local sortIsIncremental = recoverState and recoverState.sortIsIncremental
    if sortIsIncremental == nil then
        sortIsIncremental = true
    end
    self.view.sortNode:InitSortNode(self.m_sortOptions, function(optData, isIncremental)
        self.m_sortData = optData
        self.m_sortIncremental = isIncremental
        self:_UpdateShowListInfos()
        self:_RefreshItemList()
    end, CSIndex(sortSelectedIndex), sortIsIncremental, true, self.view.filterBtn)
    self.m_sortData = self.m_sortOptions[sortSelectedIndex]
    self.m_sortIncremental = self.view.sortNode.isIncremental
end

FacBuildListSelectCtrl._GetContentFilterResultCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    local domainMap
    if tags then
        domainMap = {}
        for _, v in ipairs(tags) do
            domainMap[v.domainId] = true
        end
    else
        return 0
    end
    local count = 0
    local info = self.m_showListInfos[self.m_selectedTypeIndex]
    for itemIndex, itemInfo in ipairs(info) do
        if #itemInfo.recommendDomains == 0 then
            count = count + 1
        else
            for domainIndex, domainId in ipairs(itemInfo.recommendDomains) do
                if domainMap[domainId] then
                    count = count + 1
                    break
                end
            end
        end
    end
    return count
end

FacBuildListSelectCtrl._InitTypeData = HL.Method() << function(self)
    local typeInfos = {}
    local isInMainRegion = Utils.isInFacMainRegion()

    local allItems = {}
    local allTypeInfo = {
        data = {
            id = "all",
            name = Language.LUA_FAC_ALL,
            icon = "Factory/WorkshopCraftTypeIcon/icon_type_all",
        },
        priority = math.maxinteger,
        items = allItems,
    }
    table.insert(typeInfos, allTypeInfo)

    local domainId = FactoryUtils.getCurAndAutoTransferBlackBoxToDomainId()
    local logisticTypeInfo
    do
        local typeData = Tables.factoryQuickBarTypeTable:GetValue("logistic")
        logisticTypeInfo = {
            data = typeData,
            priority = typeData.priority,
            items = {},
            redDot = "FacBuildModeMenuLogisticTab",
        }
        if isInMainRegion then
            for id, data in pairs(Tables.factoryGridBeltTable) do
                if FactoryUtils.isLogisticUnlocked(id, domainId) then
                    local item = {
                        id = id,
                        itemId = data.beltData.itemId,
                        type = QuickBarItemType.Belt,
                        data = data.beltData,
                        conveySpeed = 1000000 / data.beltData.msPerRound,
                        recommendDomains = {},
                        domainSortGroup = FacConst.DOMAIN_SORT_GROUP.Normal,
                        domainReverseSort = -FacConst.DOMAIN_SORT_GROUP.Normal,
                    }
                    table.insert(logisticTypeInfo.items, item)
                end
            end
            for id, data in pairs(Tables.FactoryBoxValveTable) do
                if FactoryUtils.isLogisticUnlocked(id, domainId) then
                    local item = {
                        id = id,
                        itemId = data.gridUnitData.itemId,
                        type = QuickBarItemType.Logistic,
                        data = data.gridUnitData,
                        conveySpeed = 1000000 / data.gridUnitData.msPerRound,
                        recommendDomains = {},
                        domainSortGroup = FacConst.DOMAIN_SORT_GROUP.Normal,
                        domainReverseSort = -FacConst.DOMAIN_SORT_GROUP.Normal,
                    }
                    table.insert(logisticTypeInfo.items, item)
                end
            end
            for id, data in pairs(Tables.factoryGridConnecterTable) do
                if FactoryUtils.isLogisticUnlocked(id, domainId) then
                    local item = {
                        id = id,
                        itemId = data.gridUnitData.itemId,
                        type = QuickBarItemType.Logistic,
                        data = data.gridUnitData,
                        conveySpeed = 1000000 / data.gridUnitData.msPerRound,
                        hasRedDot = true,
                        recommendDomains = {},
                        domainSortGroup = FacConst.DOMAIN_SORT_GROUP.Normal,
                        domainReverseSort = -FacConst.DOMAIN_SORT_GROUP.Normal,
                    }
                    table.insert(logisticTypeInfo.items, item)
                end
            end
            for id, data in pairs(Tables.factoryGridRouterTable) do
                if FactoryUtils.isLogisticUnlocked(id, domainId) then
                    local item = {
                        id = id,
                        itemId = data.gridUnitData.itemId,
                        type = QuickBarItemType.Logistic,
                        data = data.gridUnitData,
                        conveySpeed = 1000000 / data.gridUnitData.msPerRound,
                        hasRedDot = true,
                        recommendDomains = {},
                        domainSortGroup = FacConst.DOMAIN_SORT_GROUP.Normal,
                        domainReverseSort = -FacConst.DOMAIN_SORT_GROUP.Normal,
                    }
                    table.insert(logisticTypeInfo.items, item)
                end
            end
        end

        if FactoryUtils.isDomainSupportPipe() then
            for id, data in pairs(Tables.factoryLiquidPipeTable) do
                if FactoryUtils.isLogisticUnlocked(id, domainId) then
                    local item = {
                        id = id,
                        itemId = data.pipeData.itemId,
                        type = QuickBarItemType.Belt,
                        data = data.pipeData,
                        conveySpeed = 1000000 / data.pipeData.msPerRound,
                        hasRedDot = false,
                        recommendDomains = FactoryUtils.GetAllowPipeDoaminList(),
                        domainSortGroup = FacConst.DOMAIN_SORT_GROUP.Normal,
                        domainReverseSort = -FacConst.DOMAIN_SORT_GROUP.Normal,
                    }
                    table.insert(logisticTypeInfo.items, item)
                end
            end
            for id, data in pairs(Tables.factoryFluidValveTable) do
                if FactoryUtils.isLogisticUnlocked(id, domainId) then
                    local item = {
                        id = id,
                        itemId = data.liquidUnitData.itemId,
                        type = QuickBarItemType.Logistic,
                        data = data.liquidUnitData,
                        conveySpeed = 1000000 / data.liquidUnitData.msPerRound,
                        recommendDomains = FactoryUtils.GetAllowPipeDoaminList(),
                        domainSortGroup = FacConst.DOMAIN_SORT_GROUP.Normal,
                        domainReverseSort = -FacConst.DOMAIN_SORT_GROUP.Normal,
                    }
                    table.insert(logisticTypeInfo.items, item)
                end
            end
            for id, data in pairs(Tables.factoryLiquidConnectorTable) do
                if FactoryUtils.isLogisticUnlocked(id, domainId) then
                    local item = {
                        id = id,
                        liquidUnitId = data.liquidUnitData.itemId,
                        itemId = data.liquidUnitData.itemId,
                        type = QuickBarItemType.Logistic,
                        data = data.liquidUnitData,
                        conveySpeed = 1000000 / data.liquidUnitData.msPerRound,
                        hasRedDot = false,
                        recommendDomains = FactoryUtils.GetAllowPipeDoaminList(),
                        domainSortGroup = FacConst.DOMAIN_SORT_GROUP.Normal,
                        domainReverseSort = -FacConst.DOMAIN_SORT_GROUP.Normal,
                    }
                    table.insert(logisticTypeInfo.items, item)
                end
            end
            for id, data in pairs(Tables.factoryLiquidRouterTable) do
                if FactoryUtils.isLogisticUnlocked(id, domainId) then
                    local item = {
                        id = id,
                        liquidUnitId = data.liquidUnitData.itemId,
                        itemId = data.liquidUnitData.itemId,
                        type = QuickBarItemType.Logistic,
                        data = data.liquidUnitData,
                        conveySpeed = 1000000 / data.liquidUnitData.msPerRound,
                        hasRedDot = false,
                        recommendDomains = FactoryUtils.GetAllowPipeDoaminList(),
                        domainSortGroup = FacConst.DOMAIN_SORT_GROUP.Normal,
                        domainReverseSort = -FacConst.DOMAIN_SORT_GROUP.Normal,
                    }
                    table.insert(logisticTypeInfo.items, item)
                end
            end
        end

        local filteredItems = {}
        for _, v in ipairs(logisticTypeInfo.items) do
            local itemData = Tables.itemTable[v.itemId]
            v.rarity = itemData.rarity
            v.sortId1 = itemData.sortId1
            v.sortId2 = itemData.sortId2
            if not FactoryUtils.isSkipBuildingInvalidInDomain(v.id, domainId) then
                table.insert(filteredItems, v)
            end
        end
        logisticTypeInfo.items = filteredItems
    end

    do
        local infos = {}
        if not self.m_onlyCraftNode then
            infos.logistic = logisticTypeInfo
        end
        for id, data in pairs(Tables.factoryBuildingTable) do
            local typeId = data.quickBarType
            if not string.isEmpty(typeId) and (not data.onlyShowOnMain or isInMainRegion) then
                local itemData = FactoryUtils.getBuildingItemData(id)
                if FactoryUtils.isSpMachineFormulaUnlocked(id) and not FactoryUtils.isSkipBuildingInvalidInDomain(id, domainId) then
                    local info = infos[typeId]
                    if not info then
                        local typeData = Tables.factoryQuickBarTypeTable:GetValue(typeId)
                        info = {
                            data = typeData,
                            priority = typeData.priority,
                            items = {},
                        }
                        infos[typeId] = info
                    end
                    if info then
                        local item = {
                            id = id,
                            itemId = itemData.id,
                            rarity = itemData.rarity,
                            sortId1 = itemData.sortId1,
                            sortId2 = itemData.sortId2,
                            type = QuickBarItemType.Building,
                        }
                        FactoryUtils.addBuildingDomainSortFilterInfo(item, data, domainId)
                        table.insert(info.items, item)
                    end
                end
            end
        end
        for _, info in pairs(infos) do
            table.insert(typeInfos, info)
        end
    end

    table.sort(typeInfos, Utils.genSortFunction({ "priority" }))
    for k = 2, #typeInfos do
        local typeInfo = typeInfos[k]
        for _, v in ipairs(typeInfo.items) do

            v.bpSort = self.m_bluePrintData[v.id] and 1 or 0
            v.bpReverseSort = -v.bpSort
            v.groupSort = typeInfo.priority
            v.groupReverseSort = -v.groupSort
            table.insert(allItems, v)
        end
    end

    self.m_typeInfos = typeInfos
    self:_UpdateShowListInfos()
end

FacBuildListSelectCtrl._UpdateShowListInfos = HL.Method() << function(self)
    local bpSort = self.view.toppingToggle.isOn
    self.m_showListInfos = {}
    for typeIndex = 1, #self.m_typeInfos do
        local typeInfo = self.m_typeInfos[typeIndex]
        self.m_showListInfos[typeIndex] = {}
        for itemIndex, itemInfo in ipairs(typeInfo.items) do
            local filterAllowed = (self.m_filterTags == nil or #itemInfo.recommendDomains == 0)
            if self.m_filterTags ~= nil and #itemInfo.recommendDomains > 0 then
                for domainIndex, domainId in ipairs(itemInfo.recommendDomains) do
                    if self.m_filterTags[domainId] then
                        filterAllowed = true
                        break
                    end
                end
            end
            if filterAllowed then
                table.insert(self.m_showListInfos[typeIndex], itemInfo)
            end
        end

        local sortKey = self.m_sortIncremental and self.m_sortData.reverseKeys or self.m_sortData.keys
        local keyList = {}
        for _, v in ipairs(sortKey) do
            table.insert(keyList, v)
        end
        if typeIndex == 1 then


            table.insert(keyList, 2, self.m_sortIncremental and "groupReverseSort" or "groupSort")
        end
        if bpSort then


            table.insert(keyList, 1, self.m_sortIncremental and "bpReverseSort" or "bpSort")
        end

        local sortFunction = Utils.genSortFunction(keyList, self.m_sortIncremental)
        table.sort(self.m_showListInfos[typeIndex], sortFunction)
    end
end

FacBuildListSelectCtrl._RefreshTypeList = HL.Method() << function(self)
    local count = #self.m_typeInfos
    self.m_selectedTypeIndex = math.min(math.max(self.m_selectedTypeIndex, 1), count)
    if FacBuildListSelectCtrl.s_lastSelectInfo then
        local tabId = FacBuildListSelectCtrl.s_lastSelectInfo[1]
        for k, v in ipairs(self.m_typeInfos) do
            if v.data.id == tabId then
                self.m_selectedTypeIndex = k
                break
            end
        end
    end
    self.view.tabScrollList:UpdateCount(count)
    self.view.tabScrollList:ScrollToIndex(self.m_selectedTypeIndex)
    self:_OnClickType(self.m_selectedTypeIndex, true)
end

FacBuildListSelectCtrl._OnUpdateTypeCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local info = self.m_typeInfos[index]

    cell.default.icon:LoadSprite(info.data.icon)
    cell.selected.icon:LoadSprite(info.data.icon)
    cell.default.gameObject:SetActiveIfNecessary(self.m_selectedTypeIndex ~= index)
    cell.selected.gameObject:SetActiveIfNecessary(self.m_selectedTypeIndex == index)

    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_OnClickType(index)
    end)
    cell.gameObject.name = "TypeTabCell_" .. info.data.id

    if not Utils.isInBlackbox() and info.redDot then
        cell.redDot:InitRedDot(info.redDot)
        cell.redDot.gameObject:SetActiveIfNecessary(true)
    else
        cell.redDot:Stop()
        cell.redDot.gameObject:SetActiveIfNecessary(false)
    end
end

FacBuildListSelectCtrl._OnClickType = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, index, firstOpen)
    self.m_selectedTypeIndex = index
    self.view.tabText.text = self.m_typeInfos[index].data.name
    self.view.tabScrollList:SetSelectedIndex(CSIndex(self.m_selectedTypeIndex), true, true)

    self:_RefreshItemList(firstOpen)
end

FacBuildListSelectCtrl._RefreshItemList = HL.Method(HL.Opt(HL.Boolean)) << function(self, firstOpen)
    local info = self.m_showListInfos[self.m_selectedTypeIndex]
    if info == nil or #info == 0 then
        self.view.scrollList:UpdateCount(0)
        self.view.listController:SetState("Empty")
    else
        self.view.listController:SetState("Normal")
        self.view.scrollList:UpdateCount(#info)
        local targetIndex = 0
        if firstOpen and FacBuildListSelectCtrl.s_lastSelectInfo and FacBuildListSelectCtrl.s_lastSelectInfo[2] then
            local targetId = FacBuildListSelectCtrl.s_lastSelectInfo[2]
            for k, v in ipairs(info) do
                if v.itemId == targetId then
                    targetIndex = CSIndex(k)
                    break
                end
            end
        end
        self.view.scrollList:ScrollToIndex(targetIndex, true)
        self.view.scrollList:SetSelectedIndex(targetIndex, true, true, false)
        if DeviceInfo.usingController then
            self:_NaviToBuildList()
        end
    end
end

FacBuildListSelectCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local item = self.m_showListInfos[self.m_selectedTypeIndex][index]
    local itemId = item.itemId
    local count
    local isBuilding = item.type == QuickBarItemType.Building
    if isBuilding then
        count = Utils.getItemCount(itemId)
    end
    cell:InitItemSlot({
        id = itemId,
        count = count,
    }, function()
        self.view.scrollList:SetSelectedIndex(CSIndex(index))
    end)
    local disNodeShow = (self.m_curCraftModeState and item.type ~= QuickBarItemType.Building)
        or (not self.m_curCraftModeState and item.domainSortGroup < FacConst.DOMAIN_SORT_GROUP.Unsuitable)
    cell.view.disNode.gameObject:SetActiveIfNecessary(disNodeShow)
    cell.view.bpNode.gameObject:SetActiveIfNecessary(self.m_bluePrintData[item.id] ~= nil)
    if self.m_bluePrintData[item.id] ~= nil then
        local isSkip = FactoryUtils.isSkipUnlockedBuilding(item.id)
        cell.view.bpNode:SetState(isSkip and "Skip" or "Normal")
    end
    cell.item.view.button.longPressHintTextId = nil
    if DeviceInfo.usingController then
        cell.item:SetEnableHoverTips(false)
        cell.view.inputNaviDecorator.onIsNaviTargetChanged = function(active)
            if active then
                self.view.scrollList:SetSelectedIndex(CSIndex(index))
            end
        end
    end

    cell.gameObject.name = "CELL_" .. itemId
    if not DeviceInfo.usingController then
        local isSelected = index == LuaIndex(self.view.scrollList.curSelectedIndex)
        cell.item:SetSelected(isSelected)
    end



    if not Utils.isInBlackbox() and item.hasRedDot then
        cell.view.redDot:InitRedDot("FacBuildModeMenuItem", item.id, nil, self.view.redDotScrollRect)
    else
        cell.view.redDot:Stop()
    end

    local canDrag = item.type ~= QuickBarItemType.Belt and not self.m_onlyCraftNode
    cell.view.dragItem.enabled = canDrag
    if canDrag then
        local data = Tables.itemTable:GetValue(itemId)
        UIUtils.initUIDragHelper(cell.view.dragItem, {
            source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.BuildModeSelect,
            type = data.type,
            itemId = itemId,
            cantDrop = item.domainSortGroup < FacConst.DOMAIN_SORT_GROUP.Unsuitable,
            cantDropText = Language.LUA_FACTORY_INVENTORY_BUILDING_QUICKBAR_FORBID
        })
        cell:InitPressDrag()
        cell.item.view.button.longPressHintTextId = "virtual_mouse_hint_drag"
    end
end

FacBuildListSelectCtrl._OnClickItem = HL.Method(HL.Number) << function(self, index)
    self:_RefreshSelectedInfo()
    self.view.infoAnimationWrapper:PlayWithTween("facbuildmodinfonode_in")
end

FacBuildListSelectCtrl._RefreshSelectedInfo = HL.Method(HL.Opt(HL.Boolean)) << function(self, forceUpdate)
    local item = self.m_showListInfos[self.m_selectedTypeIndex][LuaIndex(self.view.scrollList.curSelectedIndex)]
    if item == nil then
        if self.view.earlyAccessCtrl ~= nil then
            self.view.earlyAccessCtrl:SetState("Normal")
        end
        return
    end

    if self.view.earlyAccessCtrl ~= nil then
        local earlyAccessState = "Normal"
        if not string.isEmpty(item.id) and FactoryUtils.isSkipUnlockedBuilding(item.id) then
            earlyAccessState = "EarlyAccess"
        end
        self.view.earlyAccessCtrl:SetState(earlyAccessState)
    end

    if self.m_curCraftModeState then
        self:_RefreshSelectedCraftNode(forceUpdate)
    else
        self:_RefreshSelectedPlaceNode(forceUpdate)
    end

    if not self.m_onlyCraftNode and not self.m_curDecoBuildingState then
        InputManagerInst:ToggleBinding(self.m_setQuickBarBindingId, item.type ~= QuickBarItemType.Belt)
    end

    if item.hasRedDot then
        local _, hasSaved = ClientDataManagerInst:GetBool(FacConst.FAC_BUILD_LIST_REDDOT_DATA_CATEGORY .. item.id, false, false, FacConst.FAC_BUILD_LIST_REDDOT_DATA_CATEGORY)
        if not hasSaved then
            ClientDataManagerInst:SetBool(FacConst.FAC_BUILD_LIST_REDDOT_DATA_CATEGORY .. item.id, true, false, FacConst.FAC_BUILD_LIST_REDDOT_DATA_CATEGORY)
            RedDotManager:TriggerUpdate("FacBuildModeMenuItem")
        end
    end
end

FacBuildListSelectCtrl._RefreshSelectedPlaceNode = HL.Method(HL.Opt(HL.Boolean)) << function(self, forceUpdate)
    local item = self.m_showListInfos[self.m_selectedTypeIndex][LuaIndex(self.view.scrollList.curSelectedIndex)]
    if item == nil then
        return
    end
    if self.m_lastPlaceModeSelectId == item.itemId and not forceUpdate then
        return
    end
    self.m_lastPlaceModeSelectId = item.itemId

    local id = item.itemId
    local data = Tables.itemTable[id]

    self.view.placeNode.machineIcon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_IMAGE, string.format("image_%s", item.id))
    UIUtils.setItemRarityImage(self.view.placeNode.rarityLight, data.rarity)
    UIUtils.setItemRarityImage(self.view.placeNode.rarityIcon, data.rarity)
    self.view.placeNode.nameTxt.text = data.name
    self.view.placeNode.descTxt:SetAndResolveTextStyle(data.desc)

    local hasTag, tagIds = UIUtils.tryGetTagList(id, data.type)
    self.view.placeNode.tagNode.gameObject:SetActiveIfNecessary(hasTag)
    if hasTag then
        local tagId = tagIds[0]
        local tagData = Tables.factoryIngredientTagTable:GetValue(tagId)
        self.view.placeNode.tagTxt.text = tagData.tagLabel
    end

    local isBuilding = item.type == QuickBarItemType.Building
    self.view.placeNode.countNode.gameObject:SetActiveIfNecessary(isBuilding)
    self.view.placeNode.powerNode.gameObject:SetActiveIfNecessary(isBuilding)
    self.view.placeNode.confirmBtn.gameObject:SetActiveIfNecessary(true)
    self.view.placeNode.emptyBtn.gameObject:SetActiveIfNecessary(false)
    if isBuilding then
        local bagCount = Utils.getBagItemCount(id)
        local depotCount = Utils.getDepotItemCount(id, Utils.getCurrentScope(), Utils.getCurDomainId())
        self.view.placeNode.bagCountTxt.text = tostring(bagCount)
        self.view.placeNode.depotCountTxt.text = tostring(depotCount)
        self.view.placeNode.bagCountTxt.color = bagCount == 0 and self.view.config.COUNT_EMPTY_COLOR or self.view.config.COUNT_ENOUGH_COLOR
        self.view.placeNode.depotCountTxt.color = depotCount == 0 and self.view.config.COUNT_EMPTY_COLOR or self.view.config.COUNT_ENOUGH_COLOR

        local buildingData = FactoryUtils.getItemBuildingData(id)
        self.view.placeNode.powerTxt.text = GameInstance.remoteFactoryManager.system.core.progressStatus:IsBuildingPowerCostNoNeed(item.id) and 0 or buildingData.powerConsume

        local lack
        if Utils.isInFacMainRegion() then
            lack = bagCount == 0 and depotCount == 0
        else
            lack = bagCount == 0
        end
        if lack then
            self.view.placeNode.confirmBtn.gameObject:SetActiveIfNecessary(false)
            if not Utils.isInBlackbox() and Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacHub) then
                self.view.placeNode.emptyBtn.gameObject:SetActiveIfNecessary(true)
            end
        end
    end
end

FacBuildListSelectCtrl._RefreshSelectedCraftNode = HL.Method(HL.Opt(HL.Boolean)) << function(self, forceUpdate)
    local item = self.m_showListInfos[self.m_selectedTypeIndex][LuaIndex(self.view.scrollList.curSelectedIndex)]
    if item == nil then
        return
    end
    if self.m_lastCraftModeSelectId == item.itemId and not forceUpdate then
        return
    end
    self.m_lastCraftModeSelectId = item.itemId

    local id = item.itemId
    local data = Tables.itemTable[id]

    self.view.craftNode.machineIcon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_IMAGE, string.format("image_%s", item.id))
    UIUtils.setItemRarityImage(self.view.craftNode.rarityLight, data.rarity)
    UIUtils.setItemRarityImage(self.view.craftNode.rarityIcon, data.rarity)
    self.view.craftNode.nameTxt.text = data.name

    local isBuilding = item.type == QuickBarItemType.Building
    self.view.craftNode.countNode.gameObject:SetActiveIfNecessary(isBuilding)
    self.view.craftNode.numberSelector.gameObject:SetActiveIfNecessary(isBuilding)
    self.view.craftNode.incomeNode.gameObject:SetActiveIfNecessary(isBuilding)
    self.view.craftNode.lineNode.gameObject:SetActiveIfNecessary(isBuilding)
    self.view.craftNode.buildBtn.gameObject:SetActiveIfNecessary(false)
    self.view.craftNode.materialLackNode.gameObject:SetActiveIfNecessary(false)
    self.view.craftNode.bpNode.gameObject:SetActiveIfNecessary(false)
    self.view.craftNode.CannotNode.gameObject:SetActiveIfNecessary(not isBuilding)
    if isBuilding then
        local bagCount = Utils.getBagItemCount(id)
        local depotCount
        if self.m_bluePrintMode then
            depotCount = Utils.getItemCount(id, true, true)
        else
            depotCount = Utils.getDepotItemCount(id, Utils.getCurrentScope(), Utils.getCurDomainId())
        end
        self.view.craftNode.bagCountTxt.text = tostring(bagCount)
        self.view.craftNode.depotCountTxt.text = tostring(depotCount)
        self.view.craftNode.bagCountTxt.color = bagCount == 0 and self.view.config.COUNT_EMPTY_COLOR or self.view.config.COUNT_ENOUGH_COLOR
        self.view.craftNode.depotCountTxt.color = depotCount == 0 and self.view.config.COUNT_EMPTY_COLOR or self.view.config.COUNT_ENOUGH_COLOR

        local craftData = Tables.factoryHubCraftTable:GetValue(item.id)
        local maxMakeCount = MAXIMUM_MAKE_COUNT
        for index = 1, FacConst.FAC_HUB_CRAFT_MAX_INCOME_NUM do
            if craftData.ingredients.length >= index then
                local itemBundle = craftData.ingredients[CSIndex(index)]
                local count = Utils.getItemCount(itemBundle.id, true, true)
                maxMakeCount = math.min(maxMakeCount, math.floor(count / itemBundle.count))
            end
        end
        local numSelector = self.view.craftNode.numberSelector
        numSelector:InitNumberSelector(numSelector.curNumber, 1, math.max(maxMakeCount, 1), function()
            self:_OnCurCountChange()
        end)
        self.view.craftNode.buildBtn.gameObject:SetActiveIfNecessary(maxMakeCount > 0)
        self.view.craftNode.materialLackNode.gameObject:SetActiveIfNecessary(maxMakeCount <= 0)

        for index = 1, FacConst.FAC_HUB_CRAFT_MAX_INCOME_NUM do
            local cell = self.view.craftNode["incomeCell" .. index]
            if craftData.ingredients.length >= index then
                cell.emptyBG.gameObject:SetActive(false)
                cell.content.gameObject:SetActive(true)
                local itemBundle = craftData.ingredients[CSIndex(index)]
                cell.item:InitItem(itemBundle, true)
                local count = Utils.getItemCount(itemBundle.id, true, true)
                local costCount = math.max(itemBundle.count, itemBundle.count * self.view.craftNode.numberSelector.curNumber)
                local isEnough = count >= costCount
                cell.item:UpdateCountSimple(costCount, not isEnough)
                if DeviceInfo.usingController then
                    cell.item:SetExtraInfo({
                        tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
                        tipsPosTransform = self.view.craftNode.incomeNode.transform,
                        isSideTips = true,
                    })
                end
            else
                cell.emptyBG.gameObject:SetActive(true)
                cell.content.gameObject:SetActive(false)
            end
        end
        local showBpCount = self.m_bluePrintData[item.id] and self.m_bluePrintData[item.id] > 0
        self.view.craftNode.bpNode.gameObject:SetActiveIfNecessary(showBpCount)
        if showBpCount then
            local bpCount = math.max(self.m_bluePrintData[item.id] - depotCount, 0)
            self.view.craftNode.bpCountTxt.text = bpCount
            numSelector:RefreshNumber(bpCount)
        end
    end
end

FacBuildListSelectCtrl._OnCurCountChange = HL.Method() << function(self)
    local item = self.m_showListInfos[self.m_selectedTypeIndex][LuaIndex(self.view.scrollList.curSelectedIndex)]
    if item == nil then
        return
    end
    local id = item.itemId
    local craftData = Tables.factoryHubCraftTable:GetValue(item.id)
    for index = 1, FacConst.FAC_HUB_CRAFT_MAX_INCOME_NUM do
        if craftData.ingredients.length >= index then
            local itemBundle = craftData.ingredients[CSIndex(index)]
            local count = Utils.getItemCount(itemBundle.id, true, true)
            local cell = self.view.craftNode["incomeCell" .. index]
            local costCount = math.max(itemBundle.count, itemBundle.count * self.view.craftNode.numberSelector.curNumber)
            local isEnough = count >= costCount
            cell.item:UpdateCountSimple(costCount, not isEnough)
            UIUtils.setItemStorageCountText(cell.storageNode, itemBundle.id, costCount, true)
        end
    end
end

FacBuildListSelectCtrl._OnClickBuild = HL.Method() << function(self)
    local item = self.m_showListInfos[self.m_selectedTypeIndex][LuaIndex(self.view.scrollList.curSelectedIndex)]
    if item == nil then
        return
    end
    local id = item.itemId
    local count = self.view.craftNode.numberSelector.curNumber
    if string.isEmpty(id) or count == 0 then
        return
    end
    self.m_waitingForCraftData.id = item.id
    self.m_waitingForCraftData.count = count
    GameInstance.player.facSpMachineSystem:StartHubCraft(item.id, count)
end

FacBuildListSelectCtrl._OnClickConfirm = HL.Method() << function(self)
    local item = self.m_showListInfos[self.m_selectedTypeIndex][LuaIndex(self.view.scrollList.curSelectedIndex)]

    if item.type == QuickBarItemType.Building then
        local itemId = item.itemId
        local count, backpackCount = Utils.getItemCount(itemId)
        if count == 0 then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_QUICK_BAR_COUNT_ZERO_NO_JUMP)
            return
        end
        Notify(MessageConst.FAC_ENTER_BUILDING_MODE, {
            itemId = itemId,
            fastEnter = true,
            skipMainHudAnim = true,
        })
    elseif item.type == QuickBarItemType.Belt then
        Notify(MessageConst.FAC_ENTER_BELT_MODE, {
            beltId = item.id,
            fastEnter = true,
            skipMainHudAnim = true,
        })
    elseif item.type == QuickBarItemType.Logistic then
        Notify(MessageConst.FAC_ENTER_LOGISTIC_MODE, {
            itemId = item.itemId,
            fastEnter = true,
            skipMainHudAnim = true,
        })
    end
end

FacBuildListSelectCtrl._OnClickWiki = HL.Method() << function(self)
    local item = self.m_showListInfos[self.m_selectedTypeIndex][LuaIndex(self.view.scrollList.curSelectedIndex)]
    if item == nil then
        return
    end

    if DeviceInfo.usingController then
        local slot = self.m_getCell(LuaIndex(self.view.scrollList.curSelectedIndex))
        if slot ~= nil then
            slot.item:SetSelected(true)
        end
    end

    local id = item.itemId
    Notify(MessageConst.SHOW_ITEM_TIPS, {
        transform = self.view.craftNode.outcomeWikiBtn.transform,
        itemId = id,
        onClose = function()
            if DeviceInfo.usingController then
                local slot = self.m_getCell(LuaIndex(self.view.scrollList.curSelectedIndex))
                if slot ~= nil then
                    slot.item:SetSelected(false)
                end
            end
        end
    })
end

FacBuildListSelectCtrl.OnItemCountChanged = HL.Method(HL.Table) << function(self, args)
    local itemId2DiffCount = unpack(args)
    local item = self.m_showListInfos[self.m_selectedTypeIndex][LuaIndex(self.view.scrollList.curSelectedIndex)]
    if item ~= nil and itemId2DiffCount:TryGetValue(item.itemId) then
        self:_RefreshSelectedInfo(true)
        self:_TryUpdateCellByItemId(item.itemId)
    end
end

FacBuildListSelectCtrl._TryUpdateCellByItemId = HL.Method(HL.String) << function(self, targetItemId)
    local info = self.m_typeInfos[self.m_selectedTypeIndex]
    info = self.m_showListInfos[self.m_selectedTypeIndex]
    if info ~= nil and #info > 0 then
        for index, item in ipairs(info) do
            local itemId = item.itemId
            if itemId == targetItemId then
                local obj = self.m_getCell(index)
                if obj ~= nil then
                    self:_OnUpdateCell(obj, index)
                end
                break
            end
        end
    end
end

FacBuildListSelectCtrl.OnHubCraftSucc = HL.Method() << function(self)
    if self.m_waitingForCraftData == nil or self.m_waitingForCraftData.id == nil then
        return
    end
    local id = self.m_waitingForCraftData.id
    local count = self.m_waitingForCraftData.count or 1

    self.m_lastPlaceModeSelectId = ""
    local info = {
        title = Language.LUA_FAC_WORKSHOP_REWARD_POP_TITLE,
        subTitle = Language.LUA_FAC_WORKSHOP_REWARD_POP_SUB_TITLE,
        items = {}
    }
    local craftData = Tables.factoryHubCraftTable:GetValue(id)
    for _, v in pairs(craftData.outcomes) do
        table.insert(info.items, {
            id = v.id,
            count = v.count * count
        })
    end
    Notify(MessageConst.SHOW_CRAFT_REWARDS, info)
end

FacBuildListSelectCtrl.OnActionScrollToTarget = HL.Method(HL.Any) << function(self, args)
    local targetBuildingId = unpack(args)
    local mainTab = 1
    self.view.tabScrollList:ScrollToIndex(CSIndex(mainTab), true)
    self:_OnClickType(mainTab)

    local targetIndex
    for index, info in ipairs(self.m_showListInfos[self.m_selectedTypeIndex]) do
        if info.id == targetBuildingId then
            targetIndex = index
            break
        end
    end
    if targetIndex ~= nil then
        self.view.scrollList:ScrollToIndex(CSIndex(targetIndex), true)
        if DeviceInfo.usingController then
            local cell = self.m_getCell(targetIndex)
            if cell ~= nil then
                self:SetNaviTarget(cell.view.inputNaviDecorator)
            end
        else
            self.view.scrollList:SetSelectedIndex(CSIndex(targetIndex))
        end
    end
end

FacBuildListSelectCtrl._OnChangeTypeToggle = HL.Method(HL.Boolean) << function(self, isOn)
    if isOn then
        self.m_curDecoBuildingState = false
        self.view.contentStateController:SetState("Industry")
        self.view.decoBuildingRoot:OnHide()
        if self.m_selectedTypeIndex > 0 then
            self.view.tabScrollList:ScrollToIndex(self.m_selectedTypeIndex, true)
        end
        if self.m_tabListNextBindingId > 0 then
            InputManagerInst:ToggleBinding(self.m_tabListNextBindingId, true)
            InputManagerInst:ToggleBinding(self.m_tabListPrevBindingId, true)
        end
        self:_NaviToBuildList()
        self.view.infoAnimationWrapper:PlayInAnimation()
        if not self.m_onlyCraftNode and self.m_showListInfos ~= nil then
            local item = self.m_showListInfos[self.m_selectedTypeIndex][LuaIndex(self.view.scrollList.curSelectedIndex)]
            if item ~= nil then
                InputManagerInst:ToggleBinding(self.m_setQuickBarBindingId, item.type ~= QuickBarItemType.Belt)
            end
        end
    else
        self.m_curDecoBuildingState = true
        if self.m_setQuickBarBindingId > 0 then
            InputManagerInst:ToggleBinding(self.m_setQuickBarBindingId, false)
        end
        if self.m_tabListNextBindingId > 0 then
            InputManagerInst:ToggleBinding(self.m_tabListNextBindingId, false)
            InputManagerInst:ToggleBinding(self.m_tabListPrevBindingId, false)
        end
        self.view.contentStateController:SetState("Decorate")
        self.view.decoBuildingRoot:OnShow()
    end
    LuaSystemManager.factory:SetFacBuildListSelectDecoBuilding(not isOn)
end



FacBuildListSelectCtrl.m_tabListNextBindingId = HL.Field(HL.Number) << -1

FacBuildListSelectCtrl.m_tabListPrevBindingId = HL.Field(HL.Number) << -1

FacBuildListSelectCtrl.m_setQuickBarBindingId = HL.Field(HL.Number) << -1

FacBuildListSelectCtrl._InitBuildListController = HL.Method() << function(self)
    self.m_tabListNextBindingId = UIUtils.bindInputPlayerAction("fac_build_list_tab_switch_next", function()
        local count = #self.m_typeInfos
        local targetIndex = self.m_selectedTypeIndex % count + 1
        self.view.tabScrollList:ScrollToIndex(targetIndex)
        self:_OnClickType(targetIndex)
        AudioAdapter.PostEvent("Au_UI_Toggle_Tag_On")
    end, self.view.inputGroup.groupId)
    self.m_tabListPrevBindingId = UIUtils.bindInputPlayerAction("fac_build_list_tab_switch_previous", function()
        local count = #self.m_typeInfos
        local targetIndex = self.m_selectedTypeIndex > 1 and self.m_selectedTypeIndex - 1 or count
        self.view.tabScrollList:ScrollToIndex(targetIndex)
        self:_OnClickType(targetIndex)
        AudioAdapter.PostEvent("Au_UI_Toggle_Tag_On")
    end, self.view.inputGroup.groupId)
    if not self.m_onlyCraftNode then
        self.m_setQuickBarBindingId = UIUtils.bindInputPlayerAction("fac_build_list_set_quick_bar", function()
            local item = self.m_showListInfos[self.m_selectedTypeIndex][LuaIndex(self.view.scrollList.curSelectedIndex)]
            if item ~= nil then
                Notify(MessageConst.START_SET_BUILDING_ON_FAC_QUICK_BAR, {
                    itemId = item.itemId,
                })
            end
        end, self.view.inputGroup.groupId)
    end

    self.view.craftNode.incomeNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        self.view.craftNode.focusDecoNode.gameObject:SetActiveIfNecessary(not isFocused)
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

FacBuildListSelectCtrl._NaviToBuildList = HL.Method() << function(self)
    if self.m_curDecoBuildingState or self.m_getCell == nil then
        return
    end
    local slot = self.m_getCell(LuaIndex(self.view.scrollList.curSelectedIndex))
    if slot ~= nil then
        self:SetNaviTarget(slot.view.inputNaviDecorator)
    end
end

FacBuildListSelectCtrl._InitRedDot = HL.Method() << function(self)
    self.view.redDotScrollRect.getRedDotStateAt = function(csIndex)
        return self:_GetRedDotStateAt(csIndex)
    end
end

FacBuildListSelectCtrl._GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    if Utils.isInBlackbox() then
        return 0
    end

    local info = self.m_showListInfos[self.m_selectedTypeIndex]
    if info == nil or #info == 0 then
        return 0
    end

    local luaIndex = LuaIndex(index)
    if luaIndex < 1 or luaIndex > #info then
        return 0
    end

    local item = info[luaIndex]
    if not item.hasRedDot then
        return 0
    end

    local hasRedDot, redDotType = RedDotManager:GetRedDotState("FacBuildModeMenuItem", item.id)
    if hasRedDot then
        return redDotType
    end

    return 0
end






HL.Commit(FacBuildListSelectCtrl)
