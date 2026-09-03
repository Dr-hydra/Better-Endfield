
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacHUBData
FacHUBDataCtrl = HL.Class('FacHUBDataCtrl', uiCtrl.UICtrl)







FacHUBDataCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SYNC_POWER_DATA] = 'OnSyncPowerData',
    [MessageConst.ON_SYNC_PRODUCT_DATA] = 'OnSyncProductData',
    [MessageConst.FAC_ON_DEL_TIME_LIMITED_FORMULA] = "_OnFormulaDelete",
}


FacHUBDataCtrl.m_curTabIndex = HL.Field(HL.Number) << 0

FacHUBDataCtrl.m_tabCells = HL.Field(HL.Forward('UIListCache'))

FacHUBDataCtrl.m_tabInfos = HL.Field(HL.Table)

FacHUBDataCtrl.m_domainDropDownInfo = HL.Field(HL.Table)

FacHUBDataCtrl.m_curDomainIndex = HL.Field(HL.Number) << 1




FacHUBDataCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.FacHUBData)
    end)
    self:BindInputPlayerAction("fac_open_hub_data_panel", function()
        PhaseManager:PopPhase(PhaseId.FacHUBData)
    end)

    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "hub_data")
    end)

    self.m_domainDropDownInfo = {}
    local curDomainId = Utils.getCurDomainId()
    for domainId, domainData in pairs(Tables.domainDataTable) do
        if GameInstance.player.facSpMachineSystem.hubInfos:ContainsKey(domainId) then
            table.insert(self.m_domainDropDownInfo, {
                domainId = domainId,
                domainData = domainData,
                name = domainData.domainName,
            })
            if domainId == curDomainId then
                self.m_curDomainIndex = #self.m_domainDropDownInfo
            end
        end
    end
    local recoverState = self:_ProcessRecoverStateArg(arg)
    if recoverState and recoverState.domainIndex then
        self.m_curDomainIndex = self:_ClampRecoverIndex(recoverState.domainIndex, #self.m_domainDropDownInfo, self.m_curDomainIndex)
    end

    self:_InitProductivityNode(recoverState)
    self:_InitElectricNode(recoverState)

    local tabIndex = arg and arg.tabIndex or 1
    self:_InitTabs(tabIndex)

    self:_InitHUBDataController()
    self:_TryRecoverInstructionBook(recoverState)

    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_HUB_UPDATE_INTERVAL)
            if self.view.electricNode.gameObject.activeInHierarchy then
                self:_ReqPowerData()
            end
            if self.view.productivityNode.gameObject.activeInHierarchy then
                self:_ReqProductData()
            end
        end
    end)
end

FacHUBDataCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local recoverState = {
        tabIndex = self.m_curTabIndex,
        domainIndex = self.m_curDomainIndex,
        electricDurationIndex = LuaIndex(self.view.electricNode.durationDropDown.selectedIndex),
        productivityDurationIndex = LuaIndex(self.view.productivityNode.dropDown.selectedIndex),
        sortSelectedIndex = self.view.productivityNode.sortNode:GetCurSelectedIndex(),
        sortIsIncremental = self.view.productivityNode.sortNode.isIncremental,
    }
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() and instructionCtrl.id == "hub_data" and PhaseManager:GetTopPhaseId() == PhaseId.FacHUBData then
        recoverState.instructionBookArg = {
            id = instructionCtrl.id,
        }
    end
    if self.m_filterTags and next(self.m_filterTags) then
        recoverState.filterShowingTypes = {}
        for showingType, _ in pairs(self.m_filterTags) do
            table.insert(recoverState.filterShowingTypes, showingType)
        end
    end
    return recoverState
end

FacHUBDataCtrl._ProcessRecoverStateArg = HL.Method(HL.Opt(HL.Any)).Return(HL.Opt(HL.Any)) << function(self, arg)
    if not arg then
        return nil
    end
    return arg
end

FacHUBDataCtrl._ClampRecoverIndex = HL.Method(HL.Number, HL.Number, HL.Number).Return(HL.Number) << function(self, index, maxCount, defaultIndex)
    if maxCount <= 0 or type(index) ~= "number" then
        return defaultIndex
    end
    if index < 1 then
        return 1
    end
    if index > maxCount then
        return maxCount
    end
    return index
end

FacHUBDataCtrl._TryRecoverInstructionBook = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil or recoverState.instructionBookArg == nil then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PhaseId.FacHUBData then
        return
    end
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() then
        return
    end
    UIManager:Open(PanelId.InstructionBook, recoverState.instructionBookArg)
    recoverState.instructionBookArg = nil
end



FacHUBDataCtrl._GetCurDomainId = HL.Method().Return(HL.String) << function(self)
    return self.m_domainDropDownInfo[self.m_curDomainIndex].domainId
end





FacHUBDataCtrl._InitTabs = HL.Method(HL.Number) << function(self, tabIndex)
    self.m_tabInfos = {
        {
            name = "Productivity",
            icon = "icon_productivity",
            animations = {"fachubdata_out_electric", "fachubdata_in_productivity", "fachubdata_loop"},
            updateAct = function()
                self:_UpdateProductivityNode()
                CS.Beyond.Gameplay.Conditions.OnOpenFacHubDataProductPage.Trigger()
            end,
        },
        {
            name = "Electric",
            icon = "icon_electric",
            animations = {"fachubdata_out_productivity", "fachubdata_in_electric", "fachubdata_loop"},
            updateAct = function()
                self:_UpdateElectricNode()
            end,
        }
    }
    self.m_tabCells = UIUtils.genCellCache(self.view.tabs.tabCell)
    self.m_tabCells:Refresh(#self.m_tabInfos, function(cell, index)
        local info = self.m_tabInfos[index]
        cell.gameObject.name = "tab_" .. info.name
        UIUtils.setTabIcons(cell,UIConst.UI_SPRITE_FAC_HUB_ICON,info.icon)
        cell.toggle.isOn = index == tabIndex
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:_OnClickTab(index)
            end
        end)
    end)
    self:_OnClickTab(tabIndex, true)
end

FacHUBDataCtrl._OnClickTab = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, index, isInit)
    if not isInit and self.m_curTabIndex == index then
        return
    end
    self.m_curTabIndex = index
    local info = self.m_tabInfos[index]
    if isInit then
        self.view.main:SetState(info.name)
        info.updateAct()
    else
        logger.info("info.animations", info.animations)
        InputManagerInst:ToggleGroup(self.view.inputGroup.groupId, false)
        self:PlayAnimation(info.animations[1], function()
            self.view.main:SetState(info.name)
            info.updateAct()
            self:PlayAnimation(info.animations[2], function()
                self:PlayAnimation(info.animations[3])
                InputManagerInst:ToggleGroup(self.view.inputGroup.groupId, true)
            end)
        end)
    end
end







FacHUBDataCtrl.m_powerDurationDropDownInfo = HL.Field(HL.Table)

FacHUBDataCtrl._InitElectricNode = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    local inited = false

    local node = self.view.electricNode

    node.domainDropDown:Init(function(index, option, isSelected)
        local info = self.m_domainDropDownInfo[LuaIndex(index)]
        option:SetText(info.name)
    end, function(index)
        if inited then
            self:_OnDomainChanged(LuaIndex(index))
        end
    end)
    node.domainDropDown:Refresh(#self.m_domainDropDownInfo, CSIndex(self.m_curDomainIndex))

    self.m_powerDurationDropDownInfo = {}
    for _,typeData in pairs(Tables.factoryPowerDataTypeTable) do
        table.insert(self.m_powerDurationDropDownInfo, {
            type = typeData.type,
            name = typeData.name,
            count = typeData.count,
            sortId = typeData.sortId,
        })
    end
    table.sort(self.m_powerDurationDropDownInfo, Utils.genSortFunction({ "sortId" }, true))
    local durationIndex = recoverState and recoverState.electricDurationIndex or 1
    durationIndex = self:_ClampRecoverIndex(durationIndex, #self.m_powerDurationDropDownInfo, 1)
    node.durationDropDown:Init(function(csIndex, option, isSelected)
        option:SetText(self.m_powerDurationDropDownInfo[LuaIndex(csIndex)].name)
    end, function(csIndex)
        if inited then
            self:_ReqPowerData()
        end
    end)
    node.durationDropDown:Refresh(#self.m_powerDurationDropDownInfo, CSIndex(durationIndex))


    self:_InitPowersByLocalData()

    inited = true
end

FacHUBDataCtrl._InitPowersByLocalData = HL.Method() << function(self)
    local node = self.view.electricNode
    local domainId = self:_GetCurDomainId()
    local hubInfo = GameInstance.player.facSpMachineSystem:GetHubInfo(domainId)
    self:_UpdateElectricText(0, 0)
    if hubInfo then
        self:OnSyncPowerData({ hubInfo.powerInfo.powerDataType, hubInfo.powerInfo.powerGen, hubInfo.powerInfo.powerCost, domainId })
    else
        node.simpleStateController:SetState("Normal")
        node.dataLine.genLine:InitBrokenLine()
        node.dataLine.costLine:InitBrokenLine()
        node.dataLine.notEnoughLine:InitBrokenLine()
    end
end


FacHUBDataCtrl._OnDomainChanged = HL.Method(HL.Number) << function(self, index)
    local info = self.m_domainDropDownInfo[index]
    self.m_curDomainIndex = index
    if self.view.electricNode.gameObject.activeInHierarchy then
        self:_InitPowersByLocalData()
        self:_ReqPowerData()
        return
    end
    if self.view.productivityNode.gameObject.activeInHierarchy then

        self:_UpdateProductivityNode()
        return
    end
end

FacHUBDataCtrl._ReqPowerData = HL.Method() << function(self)
    local curInfo = self.m_powerDurationDropDownInfo[LuaIndex(self.view.electricNode.durationDropDown.selectedIndex)]
    GameInstance.player.facSpMachineSystem:ReqPowerData(curInfo.type, self:_GetCurDomainId())
end

FacHUBDataCtrl.OnSyncPowerData = HL.Method(HL.Any) << function(self, args)
    logger.info("FacHUBDataCtrl.OnSyncPowerData")

    local type, genValue, costValue, domainId = unpack(args)
    if domainId ~= self:_GetCurDomainId() then
        return
    end
    local curInfo = self.m_powerDurationDropDownInfo[LuaIndex(self.view.electricNode.durationDropDown.selectedIndex)]
    if type ~= curInfo.type:GetHashCode() then
        return
    end

    local node = self.view.electricNode
    local count = Tables.factoryPowerDataTypeTable:GetValue(type).count
    local maxValue = 1
    for _,value in pairs(genValue)do
        if value > maxValue then
            maxValue = value
        end
    end
    for _,value in pairs(costValue)do
        if value > maxValue then
            maxValue = value
        end
    end
    local genPoints = {}
    for _,value in pairs(genValue)do
        table.insert(genPoints, value / maxValue)
    end
    local costPoints = {}
    for _,value in pairs(costValue)do
        table.insert(costPoints, value / maxValue)
    end

    local curGen = genValue.Count >= count and genValue[genValue.Count - 1] or 0
    local curCost = costValue.Count >= count and costValue[costValue.Count - 1] or 0
    self:_UpdateElectricText(curGen, curCost)

    local isEnough = curGen >= curCost
    node.simpleStateController:SetState(isEnough and "Normal" or "NotEnough")

    node.dataLine.genLine:InitBrokenLine(genPoints, count)
    local costLine = isEnough and node.dataLine.costLine or node.dataLine.notEnoughLine
    costLine:InitBrokenLine(costPoints, count)
end

FacHUBDataCtrl._UpdateElectricText = HL.Method(HL.Number, HL.Number) << function(self, curGen, curCost)
    local node = self.view.electricNode
    node.curGenPowerNode.text.text = curGen
    node.curGenPowerNode.textShadow.text = curGen
    node.curCostPowerNode.text.text = curCost
    node.curCostPowerNode.textShadow.text = curCost
end

FacHUBDataCtrl._UpdateElectricNode = HL.Method() << function(self)
    local node = self.view.electricNode
    node.domainDropDown:SetSelected(CSIndex(self.m_curDomainIndex), true)
    self:_InitPowersByLocalData()
end








FacHUBDataCtrl.m_itemDurationDropDownInfo = HL.Field(HL.Table)

FacHUBDataCtrl.m_filterTags = HL.Field(HL.Table)

FacHUBDataCtrl.m_items = HL.Field(HL.Table)

FacHUBDataCtrl.m_showingItems = HL.Field(HL.Table)


FacHUBDataCtrl._InitProductivityNode = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    local node = self.view.productivityNode
    local inited = false

    node.m_getCell = UIUtils.genCachedCellFunction(node.itemList)
    node.itemList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(node.m_getCell(obj), LuaIndex(csIndex))
    end)

    local sortOptions = {
        {
            name = Language.LUA_FAC_CRAFT_SORT_1,
            keys = {"order", "sortId1", "sortId2", "id"},
            reverseKeys = {"reverseOrder", "sortId1", "sortId2", "id"},
        },






        {
            name = Language.LUA_FAC_CRAFT_SORT_2,
            keys = {"order", "rarity", "sortId1", "sortId2", "id"},
            reverseKeys = {"reverseOrder", "rarity", "sortId1", "sortId2", "id"},
        },
    }
    local sortSelectedIndex = recoverState and recoverState.sortSelectedIndex or 1
    sortSelectedIndex = self:_ClampRecoverIndex(sortSelectedIndex, #sortOptions, 1)
    local sortIsIncremental = recoverState and recoverState.sortIsIncremental
    if sortIsIncremental == nil then
        sortIsIncremental = true
    end
    node.sortNode:InitSortNode(sortOptions, function(optData, isIncremental)
        if not inited then
            return
        end
        self:_SortData(optData, isIncremental)
        self:_RefreshItemList()
    end, CSIndex(sortSelectedIndex), sortIsIncremental, true, node.filterBtn)

    local filterTagGroups = {}
    for _, v in ipairs(FacConst.HUB_ITEM_PRODUCTIVITY_SHOWING_TYPES) do
        local data = Tables.itemShowingTypeTable[v:GetHashCode()]
        local info = {
            name = data.name,
            type = data.type,
        }
        table.insert(filterTagGroups, info)
    end
    local recoverFilterTags = self:_GetRecoverFilterTags(filterTagGroups, recoverState)
    if #recoverFilterTags > 0 then
        self.m_filterTags = {}
        for _, tagInfo in ipairs(recoverFilterTags) do
            self.m_filterTags[tagInfo.type] = true
        end
    end
    node.filterBtn:InitFilterBtn({
        tagGroups = {
            { tags = filterTagGroups }
        },
        selectedTags = recoverFilterTags,
        onConfirm = function(tags)
            if not inited then
                return
            end
            if tags then
                self.m_filterTags = {}
                for _, v in ipairs(tags) do
                    self.m_filterTags[v.type] = true
                end
            else
                self.m_filterTags = nil
            end
            self:_ApplyFilter()
            self:_RefreshItemList()
        end,
        getResultCount = function(tags)
            return self:_GetContentFilterResultCount(tags)
        end,
        sortNodeWidget = node.sortNode,
    })

    self.m_itemDurationDropDownInfo = {}
    for _,typeData in pairs(Tables.factoryProductivityDataTypeTable) do
        table.insert(self.m_itemDurationDropDownInfo, {
            type = typeData.type,
            name = typeData.name,
            count = typeData.count,
            sortId = typeData.sortId,
        })
    end
    table.sort(self.m_itemDurationDropDownInfo, Utils.genSortFunction({ "sortId" }, true))
    local itemDurationIndex = recoverState and recoverState.productivityDurationIndex or 1
    itemDurationIndex = self:_ClampRecoverIndex(itemDurationIndex, #self.m_itemDurationDropDownInfo, 1)
    node.dropDown:Init(function(csIndex, option, isSelected)
        option:SetText(self.m_itemDurationDropDownInfo[LuaIndex(csIndex)].name)
    end, function(csIndex)
        if not inited then
            return
        end
        self:_ReqProductData()
    end)
    node.dropDown:Refresh(#self.m_itemDurationDropDownInfo, CSIndex(itemDurationIndex))

    node.domainDropDown:Init(function(index, option, isSelected)
        local info = self.m_domainDropDownInfo[LuaIndex(index)]
        option:SetText(info.name)
    end, function(index)
        if inited then
            self:_OnDomainChanged(LuaIndex(index))
        end
    end)
    node.domainDropDown:Refresh(#self.m_domainDropDownInfo, CSIndex(self.m_curDomainIndex))

    inited = true
end

FacHUBDataCtrl._GetRecoverFilterTags = HL.Method(HL.Table, HL.Opt(HL.Any)).Return(HL.Table) << function(self, filterTagGroups, recoverState)
    if not recoverState or not recoverState.filterShowingTypes or #recoverState.filterShowingTypes == 0 then
        return {}
    end
    local recoverTypeMap = {}
    for _, showingType in ipairs(recoverState.filterShowingTypes) do
        recoverTypeMap[showingType] = true
    end
    local recoverFilterTags = {}
    for _, tagInfo in ipairs(filterTagGroups) do
        if recoverTypeMap[tagInfo.type] then
            table.insert(recoverFilterTags, tagInfo)
        end
    end
    return recoverFilterTags
end

FacHUBDataCtrl._UpdateProductivityNode = HL.Method() << function(self)
    local node = self.view.productivityNode
    node.domainDropDown:SetSelected(CSIndex(self.m_curDomainIndex), true, false)
    self:_InitItemData()
    self:_ApplyFilter()
    self:_RefreshItemList()
    self:_ReqProductData()
end

FacHUBDataCtrl._InitItemData = HL.Method() << function(self)
    self.m_items = {}
    local domainId = self.m_domainDropDownInfo[self.m_curDomainIndex].domainId
    local showingItemsData = Tables.factoryItemShowingHubTable[domainId]
    local scope = Utils.getCurrentScope()
    local remoteFactory = GameInstance.player.remoteFactory
    for _, itemId in pairs(showingItemsData.list) do
        if GameInstance.player.inventory:IsItemFound(itemId) then
            local showItem = false
            if FactoryUtils.isTimeLimitedItem(itemId) then
                local craftInfoList, canCraft = FactoryUtils.getItemCrafts(itemId)
                showItem = next(craftInfoList) ~= nil
            else
                showItem = true
            end

            if showItem then
                local itemData = Tables.itemTable[itemId]
                local isBookmark = remoteFactory:IsBookmarkItem(scope, itemId, domainId)
                local order = isBookmark and 0 or 1
                if isBookmark then
                    order = 0
                end
                table.insert(self.m_items, {
                    itemId = itemId,
                    data = itemData,
                    isBookmark = isBookmark,
                    order = order,
                    reverseOrder = -order,
                    showingType = itemData.showingType,
                    sortId1 = -itemData.sortId1,
                    sortId2 = itemData.sortId2,
                    rarity = itemData.rarity
                })
            end
        end
    end
end

FacHUBDataCtrl._SortData = HL.Method(HL.Table, HL.Boolean) << function(self, sortData, isIncremental)
    local keys = isIncremental and sortData.keys or sortData.reverseKeys
    if sortData.isProductivity then

        local domainId = self:_GetCurDomainId()
        local curTypeInfo = self.m_itemDurationDropDownInfo[LuaIndex(self.view.productivityNode.dropDown.selectedIndex)]
        local count = curTypeInfo.count
        for _, v in ipairs(self.m_showingItems) do
            local info = GameInstance.player.facSpMachineSystem:GetItemData(v.itemId, domainId)
            if info then

            end
            local genValue = info.productGen
            local curGenValue = genValue.Count >= count and genValue[genValue.Count - 1] or 0
            v.productivity = curGenValue
        end
    end
    table.sort(self.m_showingItems, Utils.genSortFunction(keys, isIncremental))
end

FacHUBDataCtrl._ApplyFilter = HL.Method() << function(self)
    if not self.m_filterTags or not next(self.m_filterTags) then
        self.m_showingItems = self.m_items
    else
        self.m_showingItems = {}
        for _, v in ipairs(self.m_items) do
            if self.m_filterTags[v.showingType] then
                table.insert(self.m_showingItems, v)
            end
        end
    end
    local sort = self.view.productivityNode.sortNode
    local sortData = sort:GetCurSortData()
    self:_SortData(sortData, sort.isIncremental)
end

FacHUBDataCtrl._RefreshItemList = HL.Method() << function(self)
    local node = self.view.productivityNode
    node.itemList:UpdateCount(#self.m_showingItems)
    if DeviceInfo.usingController then
        local firstIndex = node.itemList:GetShowingCellsIndexRange()
        local cell = node.m_getCell(LuaIndex(firstIndex))
        if cell ~= nil then
            self:SetNaviTarget(cell.naviDecorator)
        end
    end
end

FacHUBDataCtrl._GetContentFilterResultCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    local tagDic = {}
    for _, v in ipairs(tags) do
        tagDic[v.type] = true
    end
    local count = 0
    for _, v in ipairs(self.m_items) do
        if tagDic[v.showingType] then
            count = count + 1
        end
    end
    return count
end

FacHUBDataCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local itemInfo = self.m_showingItems[index]
    local itemId = itemInfo.itemId
    local itemData = Tables.itemTable:GetValue(itemId)
    cell.nameTxt.text = itemData.name
    UIUtils.setItemRarityImage(cell.rarity, itemData.rarity)
    cell.itemIcon:InitItemIcon(itemId)

    cell.bookmarkToggle.onValueChanged:RemoveAllListeners()
    cell.bookmarkToggle.isOn = itemInfo.isBookmark
    if itemInfo.isBookmark then
        cell.animation:SampleToInAnimationEnd()
    else
        cell.animation:SampleToOutAnimationEnd()
    end
    cell.bookmarkToggle.onValueChanged:AddListener(function(isOn)
        self:_MarkProductItem(index)
        if isOn then
            cell.animation:PlayInAnimation()
        else
            cell.animation:PlayOutAnimation()
        end
    end)

    cell.bookmarkToggle.checkIsValueValid = function(isOn)
        if not isOn then

            return true
        end

        local curBookmarkCount = GameInstance.player.remoteFactory:GetBookmarkItemCount(Utils.getCurrentScope(), self:_GetCurDomainId())
        local maxCount = Tables.factoryConst.maxStatisticBookmarkNum
        if curBookmarkCount >= maxCount then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_HUB_BOOKMARK_MAX_COUNT)
            return false
        else
            return true
        end
    end

    if itemData.maxStackCount > 0 then
        local count = self:_GetItemCount(itemId)
        cell.count.text = UIUtils.setCountColor(count, count <= 0)
    else
        cell.count.text = "-"
    end

    cell.costSpdTxt.text = 0
    cell.genSpdTxt.text = 0
    cell.costTheoTxt.text = 0
    cell.genTheoTxt.text = 0
    self:_ChangeCostTheoTxtColor(cell, false)
    cell.lineNode.costLine:InitBrokenLine()
    cell.lineNode.genLine:InitBrokenLine()
    self:_ReqOneProductData(itemId)
end

FacHUBDataCtrl._ChangeCostTheoTxtColor = HL.Method(HL.Any, HL.Boolean) << function(self, cell, over)
    if over then
        cell.costTheoTxt.color = self.view.config.COST_RED_COLOR
        cell.decoNameTxt.color = self.view.config.COST_RED_COLOR
        cell.decoSpTxt.color = self.view.config.COST_RED_COLOR
        cell.decoTimeTxt.color = self.view.config.COST_RED_COLOR
    else
        cell.costTheoTxt.color = self.view.config.COST_GRAY_COLOR
        cell.decoNameTxt.color = self.view.config.COST_GRAY_COLOR
        cell.decoSpTxt.color = self.view.config.COST_GRAY_COLOR
        cell.decoTimeTxt.color = self.view.config.COST_GRAY_COLOR
    end
end

FacHUBDataCtrl._MarkProductItem = HL.Method(HL.Number) << function(self, index)
    local itemInfo = self.m_showingItems[index]
    local itemId = itemInfo.itemId
    local remoteFactory = GameInstance.player.remoteFactory
    local scope = Utils.getCurrentScope()
    local domainId = self:_GetCurDomainId()
    if itemInfo.isBookmark then
        remoteFactory:RemoveBookmarkItem(scope, domainId, itemId)
    else
        remoteFactory:AddBookmarkItem(scope, domainId, itemId)
    end
    itemInfo.isBookmark = not itemInfo.isBookmark
    itemInfo.order = itemInfo.isBookmark and 0 or 1
    itemInfo.reverseOrder = -itemInfo.order
end

FacHUBDataCtrl._ReqOneProductData = HL.Method(HL.String) << function(self, itemId)
    local curInfo = self.m_itemDurationDropDownInfo[LuaIndex(self.view.productivityNode.dropDown.selectedIndex)]
    GameInstance.player.facSpMachineSystem:ReqOneProductData(curInfo.type, itemId, self:_GetCurDomainId())
end

FacHUBDataCtrl._ReqProductData = HL.Method() << function(self)
    local node = self.view.productivityNode
    local range = node.itemList:GetShowRange()
    local itemIds = {}
    for k = range.x, range.y do
        table.insert(itemIds, self.m_showingItems[LuaIndex(k)].itemId)
    end
    local curInfo = self.m_itemDurationDropDownInfo[LuaIndex(self.view.productivityNode.dropDown.selectedIndex)]
    GameInstance.player.facSpMachineSystem:ReqProductData(curInfo.type, itemIds, self:_GetCurDomainId())
end

FacHUBDataCtrl.OnSyncProductData = HL.Method() << function(self)
    local node = self.view.productivityNode
    local domainId = self:_GetCurDomainId()
    local curTypeInfo = self.m_itemDurationDropDownInfo[LuaIndex(self.view.productivityNode.dropDown.selectedIndex)]
    local typeNum = curTypeInfo.type:GetHashCode()
    node.itemList:UpdateShowingCells(function(csIndex, obj)
        local cell = node.m_getCell(obj)
        local itemId = self.m_showingItems[LuaIndex(csIndex)].itemId
        local itemData = Tables.itemTable:GetValue(itemId)

        if itemData.maxStackCount > 0 then
            local count = self:_GetItemCount(itemId)
            cell.count.text = UIUtils.setCountColor(count, count <= 0)
        else
            cell.count.text = "-"
        end

        local info = GameInstance.player.facSpMachineSystem:GetItemData(itemId, domainId)
        if info and info.productDataType == typeNum then

            local genValue = info.productGen
            local costValue = info.productCost
            local count = curTypeInfo.count
            local maxValue = 1
            if count <= genValue.Count then
                for i = genValue.Count - count + 1, genValue.Count do
                    local value = genValue[CSIndex(i)]
                    if value > maxValue then
                        maxValue = value
                    end
                end
            else
                for i = 1, genValue.Count do
                    local value = genValue[CSIndex(i)]
                    if value > maxValue then
                        maxValue = value
                    end
                end
            end
            if count <= costValue.Count then
                for i = costValue.Count - count + 1, costValue.Count do
                    local value = costValue[CSIndex(i)]
                    if value > maxValue then
                        maxValue = value
                    end
                end
            else
                for i = 1, costValue.Count do
                    local value = costValue[CSIndex(i)]
                    if value > maxValue then
                        maxValue = value
                    end
                end
            end
            local genPoints = {}
            for _, value in pairs(genValue) do
                table.insert(genPoints, value / maxValue)
            end
            local costPoints = {}
            for _, value in pairs(costValue) do
                table.insert(costPoints, value / maxValue)
            end

            cell.lineNode.genLine:InitBrokenLine(genPoints, count)
            cell.lineNode.costLine:InitBrokenLine(costPoints, count)
            local curGenValue = genValue.Count >= count and genValue[genValue.Count - 1] or 0
            local curCostValue = costValue.Count >= count and costValue[costValue.Count - 1] or 0
            cell.genSpdTxt.text = curGenValue
            cell.costSpdTxt.text = curCostValue
            cell.costTheoTxt.text = info.theoreticalCost
            cell.genTheoTxt.text = info.theoreticalGen
            self:_ChangeCostTheoTxtColor(cell, info.theoreticalCost > info.theoreticalGen)
        else
            cell.costSpdTxt.text = 0
            cell.genSpdTxt.text = 0
            cell.costTheoTxt.text = 0
            cell.genTheoTxt.text = 0
            self:_ChangeCostTheoTxtColor(cell, false)
            cell.lineNode.costLine:InitBrokenLine()
            cell.lineNode.genLine:InitBrokenLine()
        end
    end)
end

FacHUBDataCtrl._GetItemCount = HL.Method(HL.String).Return(HL.Number) << function(self, itemId)
    local inventory = GameInstance.player.inventory
    local id = ScopeUtil.ChapterIdStr2Int(self:_GetCurDomainId())
    return inventory:GetItemCountInDepot(Utils.getCurrentScope(), id, itemId)
end

FacHUBDataCtrl._OnFormulaDelete = HL.Method(HL.Any) << function(self, args)
    if self.m_curTabIndex == 1 then
        self:_UpdateProductivityNode()
    end
end







FacHUBDataCtrl._InitHUBDataController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end




HL.Commit(FacHUBDataCtrl)
