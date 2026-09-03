local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FacBuildListDecoBuildingInfoNode = HL.Class('FacBuildListDecoBuildingInfoNode', UIWidgetBase)

FacBuildListDecoBuildingInfoNode.m_decoBuilldingInfos = HL.Field(HL.Table)

FacBuildListDecoBuildingInfoNode.m_filteredDecos = HL.Field(HL.Table)

FacBuildListDecoBuildingInfoNode.m_typeInfos = HL.Field(HL.Table)

FacBuildListDecoBuildingInfoNode.m_readItems = HL.Field(HL.Table)

FacBuildListDecoBuildingInfoNode.m_getItemCell = HL.Field(HL.Function)

FacBuildListDecoBuildingInfoNode.m_getTabCell = HL.Field(HL.Function)

FacBuildListDecoBuildingInfoNode.m_selectedTypeIndex = HL.Field(HL.Number) << 0

FacBuildListDecoBuildingInfoNode.m_isIncremental = HL.Field(HL.Boolean) << true

FacBuildListDecoBuildingInfoNode.m_isShow = HL.Field(HL.Boolean) << false

FacBuildListDecoBuildingInfoNode.m_isGettingDecorate = HL.Field(HL.Boolean) << false

FacBuildListDecoBuildingInfoNode.m_inputGroupId = HL.Field(HL.Number) << -1

FacBuildListDecoBuildingInfoNode.m_setQuickBarBindingId = HL.Field(HL.Number) << -1

FacBuildListDecoBuildingInfoNode.s_lastSelectId = HL.StaticField(HL.String) << ""


FacBuildListDecoBuildingInfoNode._OnFirstTimeInit = HL.Override() << function(self)
    self:_PrepareDecoBuildingData()
    self:_PrepareTypeData()
    self:_InitBtn()
    self:_InitRedDot()
    MessageManager:Register(MessageConst.ON_DECORATE_OBTAIN_MODIFY, function()
        self:_OnGetDecorate()
    end, self)
end

FacBuildListDecoBuildingInfoNode.InitFacBuildListDecoBuildingInfoNode = HL.Method(HL.Number, HL.Opt(HL.String)) << function(self, groupId, selectedId)
    FacBuildListDecoBuildingInfoNode.s_lastSelectId = selectedId or FacBuildListDecoBuildingInfoNode.s_lastSelectId
    self.m_inputGroupId = groupId
    self.m_readItems = {}
    self:_FirstTimeInit()
    self:_InitSortAndFilterNode()
    self:_InitController()
    self:_RefreshTabItemList()
end

FacBuildListDecoBuildingInfoNode.OnShow = HL.Method() << function(self)
    self.m_isShow = true
    self:_NaviToDecoList()
    if #self.m_filteredDecos > 0 then
        local info = self.m_filteredDecos[LuaIndex(self.view.scrollList.curSelectedIndex)]
        local buildingInfo = Tables.FactoryDecoBuildingTable[info.id]
        local buildingId = buildingInfo.buildingId
        local _, gotCnt, placedCnt = GameInstance.player.remoteFactory.core:GetDecoBuildingCount(buildingId)
        InputManagerInst:ToggleBinding(self.m_setQuickBarBindingId, gotCnt + placedCnt > 0)
    end
end

FacBuildListDecoBuildingInfoNode.OnHide = HL.Method() << function(self)
    if self.m_isShow then
        self:SetItemRead()
        if self.m_setQuickBarBindingId > 0 then
            InputManagerInst:ToggleBinding(self.m_setQuickBarBindingId, false)
        end
    end
    self.m_isShow = false
end

FacBuildListDecoBuildingInfoNode.OnClose = HL.Method() << function(self)
    if self.m_isShow then
        self:SetItemRead()
    end
    self.m_isShow = false
    MessageManager:UnregisterAll(self)
end

FacBuildListDecoBuildingInfoNode._PrepareDecoBuildingData = HL.Method() << function(self)
    self.m_decoBuilldingInfos = {}
    for id, _ in pairs(Tables.FactoryDecoBuildingTable) do
        if FactoryUtils.isDecoBuildingVisible(id) then
            local itemInfo = Tables.itemTable[id]
            local buildingId = Tables.factoryDecoBuildingTable[id].buildingId
            local result = GameInstance.player.remoteFactory.core:GetDecoBuildingCount(buildingId)
            table.insert(self.m_decoBuilldingInfos, {
                id = id,
                sortId1= itemInfo.sortId1,
                sortId2 = itemInfo.sortId2,
                rarity = itemInfo.rarity,
                unlock = result,
            })
        end
    end
end

FacBuildListDecoBuildingInfoNode._PrepareTypeData = HL.Method() << function(self)

    self.m_typeInfos = {{
        data = {
            id = "all",
            name = Language.LUA_FAC_ALL,
            icon = "Factory/WorkshopCraftTypeIcon/icon_type_all",
        },
        priority = math.maxinteger,
    }}
end

FacBuildListDecoBuildingInfoNode._RefreshItemList = HL.Method() << function(self)
    local count = #self.m_filteredDecos
    if count == 0 then
        self.view.listAndInfo:SetState("Empty")
        return
    end
    self.view.listAndInfo:SetState("Normal")
    local scrollList = self.view.scrollList
    if not self.m_getItemCell then
        self.m_getItemCell = UIUtils.genCachedCellFunction(scrollList)
        scrollList.onUpdateCell:AddListener(function(obj, csIndex)
            self:_OnUpdateCell(self.m_getItemCell(obj), LuaIndex(csIndex))
        end)
        self.view.scrollList.onSelectedCell:AddListener(function(obj, csIndex)
            self:_OnClickItem(LuaIndex(csIndex))
        end)

        if not DeviceInfo.usingController then
            self.view.scrollList.onCellSelectedChanged:AddListener(function(obj, csIndex, isSelected)
                local cell = self.m_getItemCell(obj)
                if cell then
                    cell.item:SetSelected(isSelected)
                end
            end)
        end
    end

    local selectedIndex = 0
    if not string.isEmpty(FacBuildListDecoBuildingInfoNode.s_lastSelectId) then
        for k, v in ipairs(self.m_filteredDecos) do
            if v.id == FacBuildListDecoBuildingInfoNode.s_lastSelectId then
                selectedIndex = CSIndex(k)
                break
            end
        end
    end

    scrollList:UpdateCount(count)
    scrollList:ScrollToIndex(selectedIndex, true)
    scrollList:SetSelectedIndex(selectedIndex, true, true, false)
    if DeviceInfo.usingController then
        self:_NaviToDecoList()
    end
end

FacBuildListDecoBuildingInfoNode._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local id = self.m_filteredDecos[index].id
    cell:InitItemSlot({
        id = id,
    }, function()
        self.view.scrollList:SetSelectedIndex(CSIndex(index))
    end)

    local buildingId = Tables.factoryDecoBuildingTable[id].buildingId
    local remoteFactoryCore = GameInstance.player.remoteFactory.core
    local result, gotCnt, placedCnt = remoteFactoryCore:GetDecoBuildingCount(buildingId)
    if not result then
        cell.view.stateController:SetState("Lock")
    elseif gotCnt == 0 or Utils.isInBlackbox() then
        cell.view.stateController:SetState("Empty")
    else
        cell.view.stateController:SetState("Nrl")
    end
    cell.view.dragItem.enabled = true
    local data = Tables.itemTable:GetValue(id)
    local num = gotCnt + placedCnt
    UIUtils.initUIDragHelper(cell.view.dragItem, {
        source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.BuildModeSelect,
        type = data.type,
        itemId = id,
        cantDrop = num == 0,
        cantDropText = Language.LUA_FACTORY_DECO_BUILDING_QUICKBAR_FORBID
    })
    cell:InitPressDrag()
    cell.item.view.button.longPressHintTextId = "virtual_mouse_hint_drag"
    cell.view.redDot:InitRedDot("DecoBuilding", id, nil, self.view.redDotScrollRect)

    self.m_readItems[id] = true

    if DeviceInfo.usingController then
        cell.item:SetEnableHoverTips(false)
        cell.view.inputNaviDecorator.onIsNaviTargetChanged = function(active)
            if active then
                self.view.scrollList:SetSelectedIndex(CSIndex(index))
            end
        end
    end
end

FacBuildListDecoBuildingInfoNode._OnClickItem = HL.Method(HL.Number) << function(self, index)
    self:_RefreshSelectedInfo()
    local info = self.m_filteredDecos[index]
    RedDotUtils.setDecoBuildingFirstGetRead(info.id)
    RedDotUtils.setDecoBuildingNewRead(info.id)
    Notify(MessageConst.ON_FAC_DECO_BUILDING_RED_DOT_REFRESH)
    self.view.infoNode.animationWrapper:PlayInAnimation()
end

FacBuildListDecoBuildingInfoNode._RefreshSelectedInfo = HL.Method() << function(self)
    local info = self.m_filteredDecos[LuaIndex(self.view.scrollList.curSelectedIndex)]
    FacBuildListDecoBuildingInfoNode.s_lastSelectId = info.id
    local itemInfo = Tables.itemTable[info.id]
    local buildingInfo = Tables.FactoryDecoBuildingTable[info.id]
    local buildingId = buildingInfo.buildingId
    self.view.infoNode.nameTxt.text  = itemInfo.name
    self.view.infoNode.icon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_IMAGE, string.format("image_%s", buildingId))
    UIUtils.setItemRarityImage(self.view.infoNode.rarityLight, itemInfo.rarity)
    UIUtils.setItemRarityImage(self.view.infoNode.rarityIcon, itemInfo.rarity)


    local isInSpaceShip = Utils.isInSpaceShip()
    local isInBlackBox = Utils.isInBlackbox()
    local depotNode = self.view.infoNode.depotNode
    if isInSpaceShip then
        depotNode.stateController:SetState("Warn")
        depotNode.mapImg01.color = Color.white
        depotNode.mapImg02.color = Color.white
        depotNode.delSocialButton.gameObject:SetActiveIfNecessary(false)
    elseif isInBlackBox then
        self:_ShowDefaultSelectedInfo()
    else
        local currentChapterId = ScopeUtil.GetCurrentChapterIdAsStr()
        local success, domainInfo = Tables.domainDataTable:TryGetValue(currentChapterId)
        if success then
            depotNode.stateController:SetState("Nrl")
            depotNode.countIcon:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT, domainInfo.domainIcon)
            local color = UIUtils.getColorByString(domainInfo.domainGradeTitleColor)
            depotNode.mapImg01.color = color
            depotNode.mapImg02.color = color
            local curNum = GameInstance.remoteFactoryManager:GetBuildingCountInCurMap(buildingId)
            depotNode.countTxt.text = curNum
            depotNode.delSocialButton.gameObject:SetActiveIfNecessary(curNum > 0)
        else
            self:_ShowDefaultSelectedInfo()
        end
    end

    local remoteFactoryCore = GameInstance.player.remoteFactory.core
    local result, gotCnt, placedCnt = remoteFactoryCore:GetDecoBuildingCount(buildingId)
    if isInBlackBox then
        gotCnt = 0
        placedCnt = 0
        InputManagerInst:ToggleBinding(self.m_setQuickBarBindingId, false)
        self.view.infoNode.redDot.gameObject:SetActiveIfNecessary(false)
    else
        if gotCnt > 0 then
            self.view.infoNode.confirmBtn.gameObject:SetActiveIfNecessary(true)
            self.view.infoNode.shortageNode.gameObject:SetActiveIfNecessary(false)
        else
            self.view.infoNode.confirmBtn.gameObject:SetActiveIfNecessary(false)
            self.view.infoNode.shortageNode.gameObject:SetActiveIfNecessary(true)
        end

        local totalNum = buildingInfo.totalNum
        local curNum = gotCnt + placedCnt
        self.view.infoNode.warnNode.gameObject:SetActiveIfNecessary(curNum < totalNum)
        self.view.infoNode.redDot:InitRedDot("DecoBuildingAllObtainWay", info.id)
        if self.m_isShow then
            InputManagerInst:ToggleBinding(self.m_setQuickBarBindingId, curNum > 0)
        end
    end
    self.view.infoNode.bagNode.countTxt.text = tostring(gotCnt)
    self.view.infoNode.bagNode.countTxt.color = gotCnt == 0 and self.view.config.COUNT_EMPTY_COLOR or self.view.config.COUNT_ENOUGH_COLOR
end

FacBuildListDecoBuildingInfoNode._ShowDefaultSelectedInfo = HL.Method() << function(self)
    local depotNode = self.view.infoNode.depotNode
    depotNode.stateController:SetState("Warn")
    depotNode.mapImg01.color = Color.white
    depotNode.mapImg02.color = Color.white
    depotNode.delSocialButton.gameObject:SetActiveIfNecessary(false)
    self.view.infoNode.wayBtn.gameObject:SetActiveIfNecessary(false)
    self.view.infoNode.confirmBtn.gameObject:SetActiveIfNecessary(false)
    self.view.infoNode.shortageNode.gameObject:SetActiveIfNecessary(false)
end

FacBuildListDecoBuildingInfoNode._RefreshTabItemList = HL.Method() << function(self)
    if not self.m_getTabCell then
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
    end
    self.view.tabScrollList:UpdateCount(#self.m_typeInfos)
    self.view.tabScrollList:ScrollToIndex(0)
    self:_OnClickType(LuaIndex(self.m_selectedTypeIndex))
end

FacBuildListDecoBuildingInfoNode._OnUpdateTypeCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local info = self.m_typeInfos[index]

    cell.default.icon:LoadSprite(info.data.icon)
    cell.selected.icon:LoadSprite(info.data.icon)
    cell.default.gameObject:SetActiveIfNecessary(self.m_selectedTypeIndex ~= index)
    cell.selected.gameObject:SetActiveIfNecessary(self.m_selectedTypeIndex == index)

    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_OnClickType(index)
    end)

    if not Utils.isInBlackbox() and info.redDot then
        cell.redDot:InitRedDot(info.redDot)
        cell.redDot.gameObject:SetActiveIfNecessary(true)
    else
        cell.redDot:Stop()
        cell.redDot.gameObject:SetActiveIfNecessary(false)
    end
end

FacBuildListDecoBuildingInfoNode._OnClickType = HL.Method(HL.Number) << function(self, index)
    self.m_selectedTypeIndex = index
    self.view.tabText.text = self.m_typeInfos[index].data.name
    self.view.tabScrollList:SetSelectedIndex(CSIndex(index), true, true)

    self:_RefreshItemList()
end

FacBuildListDecoBuildingInfoNode._InitBtn = HL.Method() << function(self)
    self.view.infoNode.outcomeWikiBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "decorate_building")
    end)
    self.view.infoNode.wayBtn.onClick:AddListener(function()
        local info = self.m_filteredDecos[LuaIndex(self.view.scrollList.curSelectedIndex)]
        PhaseManager:OpenPhase(PhaseId.FacDecoObtainWays, {itemId = info.id})
    end)
    self.view.infoNode.confirmBtn.onClick:AddListener(function()
        self:_EnterBuildingMode()
    end)
    self.view.infoNode.depotNode.delSocialButton.onClick:AddListener(function()
        local info = self.m_filteredDecos[LuaIndex(self.view.scrollList.curSelectedIndex)]
        local itemId = info.id
        local currentChapterId = ScopeUtil.GetCurrentChapterIdAsStr()
        local _, domainInfo = Tables.domainDataTable:TryGetValue(currentChapterId)
        local itemInfo = Tables.itemTable[itemId]
        local hintText = string.format(Language.LUA_FACTORY_DECO_POPUP_REMOVE_ALL, domainInfo.domainName, itemInfo.name)
        Notify(MessageConst.SHOW_POP_UP, {
            content = hintText,
            hideBlur = true,
            onConfirm = function()
                local buildingId = Tables.FactoryDecoBuildingTable[itemId].buildingId
                local nodeList = GameInstance.remoteFactoryManager:GetBuildingNodeIdsInCurMap(buildingId)
                local chapterId = ScopeUtil.GetCurrentChapterId()
                GameInstance.player.remoteFactory.core:Message_OpDismantleBatch(chapterId, nodeList, {}, {}, true, function()
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_STORAGE_ALL_DECO_BUILDING_TOAST)
                    self:_RefreshSelectedInfo()
                end)
            end,
        })
    end)
end

FacBuildListDecoBuildingInfoNode._InitSortAndFilterNode = HL.Method(HL.Opt(HL.Any)) << function(self)
    self.view.filterBtn.gameObject:SetActiveIfNecessary(false)

    local sortOptions = {
        {
            name = Language.LUA_FAC_CRAFT_SORT_1,
            keys = {"unlockSortId", "sortId1", "sortId2", "rarity", "id" },
            reverseKeys = {"unlockSortId", "sortId1", "sortId2", "rarity", "id" },
        },
        {
            name = Language.LUA_FAC_CRAFT_SORT_2,
            keys = {"unlockSortId", "rarity", "sortId1", "sortId2", "id" },
            reverseKeys = {"unlockSortId", "sortId1", "sortId2", "rarity", "id" },
        },
    }

    self.view.sortNodeUp:InitSortNode(sortOptions, function(optData, isIncremental)
        self.m_isIncremental = isIncremental
        self:_UpdateShowListInfos(optData, isIncremental)
        self:_RefreshItemList()
    end, 0, self.m_isIncremental, true)
    self:_UpdateShowListInfos(self.view.sortNodeUp:GetCurSortData(), self.view.sortNodeUp.isIncremental)
end

FacBuildListDecoBuildingInfoNode._UpdateShowListInfos = HL.Method(HL.Table, HL.Boolean) << function(self, optData, isIncremental)
    self.m_filteredDecos = self.m_decoBuilldingInfos
    local sortKey = isIncremental and optData.reverseKeys or optData.keys

    local beforeIndex = 0
    local beforeId = ""
    if self.m_isGettingDecorate then
        beforeIndex = LuaIndex(self.view.scrollList.curSelectedIndex)
        local info = self.m_filteredDecos[beforeIndex]
        beforeId = info.id
    end
    for _, info in ipairs(self.m_filteredDecos) do
        if isIncremental then
            info.unlockSortId = info.unlock and 0 or 1
        else
            info.unlockSortId = info.unlock and 1 or 0
        end
    end
    table.sort(self.m_filteredDecos, Utils.genSortFunction(sortKey, isIncremental))
    if beforeIndex > 0 then
        local currentIndex = 0
        for i, info in ipairs(self.m_filteredDecos) do
            if info.id == beforeId then
                currentIndex = i
                break
            end
        end

        local currentInfo = self.m_filteredDecos[currentIndex]
        table.remove(self.m_filteredDecos, currentIndex)
        table.insert(self.m_filteredDecos,beforeIndex, currentInfo)
    end
end

FacBuildListDecoBuildingInfoNode._EnterBuildingMode = HL.Method() << function(self)
    local isInSpaceShip = Utils.isInSpaceShip()
    if isInSpaceShip then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_DECO_BUILDING_CANNOT_BUILD_TOAST)
        return
    end
    local info = self.m_filteredDecos[LuaIndex(self.view.scrollList.curSelectedIndex)]
    local itemId = info.id
    local count = Utils.getItemCount(itemId, true, true)
    if count == 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_QUICK_BAR_COUNT_ZERO_NO_JUMP)
        return
    end
    Notify(MessageConst.FAC_ENTER_BUILDING_MODE, {
        itemId = itemId,
        fastEnter = true,
        skipMainHudAnim = true,
    })
end

FacBuildListDecoBuildingInfoNode._NaviToDecoList = HL.Method() << function(self)
    if not self.m_isShow or self.m_getItemCell == nil then
        return
    end
    local slot = self.m_getItemCell(LuaIndex(self.view.scrollList.curSelectedIndex))
    if slot ~= nil then
        self:SetNaviTarget(slot.view.inputNaviDecorator)
    end
end

FacBuildListDecoBuildingInfoNode.SetItemRead = HL.Method() << function(self)
    for itemId in pairs(self.m_readItems) do
        RedDotUtils.setDecoBuildingNewRead(itemId)
    end
    Notify(MessageConst.ON_FAC_DECO_BUILDING_RED_DOT_REFRESH)
end

FacBuildListDecoBuildingInfoNode._InitController = HL.Method() << function(self)
    self.m_setQuickBarBindingId = UIUtils.bindInputPlayerAction("fac_build_list_deco_set_quick_bar", function()
        local info = self.m_filteredDecos[LuaIndex(self.view.scrollList.curSelectedIndex)]
        if info ~= nil then
            Notify(MessageConst.START_SET_BUILDING_ON_FAC_QUICK_BAR, {
                itemId = info.id,
            })
        end
    end, self.m_inputGroupId)
    if not self.m_isShow then
        InputManagerInst:ToggleBinding(self.m_setQuickBarBindingId, false)
    end
end

FacBuildListDecoBuildingInfoNode._InitRedDot = HL.Method() << function(self)
    self.view.redDotScrollRect.getRedDotStateAt = function(csIndex)
        return self:_GetRedDotStateAt(csIndex)
    end
end

FacBuildListDecoBuildingInfoNode._GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local info = self.m_filteredDecos
    if info == nil or #info == 0 then
        return 0
    end

    local luaIndex = LuaIndex(index)
    if luaIndex < 1 or luaIndex > #info then
        return 0
    end

    local item = info[luaIndex]
    local hasRedDot, redDotType = RedDotManager:GetRedDotState("DecoBuilding", item.id)
    if hasRedDot then
        return redDotType
    end

    return 0
end

FacBuildListDecoBuildingInfoNode._OnGetDecorate = HL.Method() << function(self)
    self.m_isGettingDecorate = true
    local beforeIndex = LuaIndex(self.view.scrollList.curSelectedIndex)
    local info = self.m_filteredDecos[beforeIndex]
    local buildingId = Tables.factoryDecoBuildingTable[info.id].buildingId
    local result = GameInstance.player.remoteFactory.core:GetDecoBuildingCount(buildingId)
    self.m_filteredDecos[beforeIndex].unlock = result

    self:_UpdateShowListInfos(self.view.sortNodeUp:GetCurSortData(), self.view.sortNodeUp.isIncremental)
    self:_RefreshItemList()
    self.m_isGettingDecorate = false
end

HL.Commit(FacBuildListDecoBuildingInfoNode)
return FacBuildListDecoBuildingInfoNode

