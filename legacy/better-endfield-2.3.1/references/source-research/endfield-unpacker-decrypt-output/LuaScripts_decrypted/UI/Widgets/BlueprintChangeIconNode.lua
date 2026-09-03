local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

BlueprintChangeIconNode = HL.Class('BlueprintChangeIconNode', UIWidgetBase)


BlueprintChangeIconNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_getGroupCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateGroupCell(self.m_getGroupCell(obj), csIndex)
    end)

    local countPerLine = self.view.config.COUNT_PER_LINE
    local titleHeight = self.view.config.TITLE_HEIGHT
    local cellHeight = self.view.config.CELL_HEIGHT
    self.view.scrollList.getCellSize = function(csIndex)
        local count = 0
        for _, v in ipairs(self.m_iconGroupInfos) do
            if count == csIndex then

                return v.name and titleHeight or 0
            end
            count = count + 1
            if not v.isFold then
                count = count + math.ceil(#v.list / countPerLine)
                if csIndex < count then

                    return cellHeight
                end
            end
        end
        return 0
    end

    self:_InitColorNode()

    self.view.colorBtn.onClick:AddListener(function()
        self:_ChangeState(false, true)
    end)
    self.view.iconBtn.onClick:AddListener(function()
        self:_ChangeState(true, true)
    end)
end


BlueprintChangeIconNode.m_getGroupCell = HL.Field(HL.Function)

BlueprintChangeIconNode.m_iconGroupInfos = HL.Field(HL.Table)

BlueprintChangeIconNode.m_icon2PosInfo = HL.Field(HL.Table)

BlueprintChangeIconNode.curIconId = HL.Field(HL.String) << ''

BlueprintChangeIconNode.onChangeIconOrColor = HL.Field(HL.Function)

BlueprintChangeIconNode.m_csBP = HL.Field(CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprint)

BlueprintChangeIconNode.m_selectedIconCell = HL.Field(HL.Any)

BlueprintChangeIconNode.m_selectedColorCell = HL.Field(HL.Any)




BlueprintChangeIconNode.InitBlueprintChangeIconNode = HL.Method(CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprint, HL.String, HL.Number, HL.Function) << function(self, csBP, icon, colorId, onChangeIconOrColor)
    self:_FirstTimeInit()

    self.m_csBP = csBP
    self.curIconId = icon
    self.onChangeIconOrColor = onChangeIconOrColor
    self.m_selectedIconCell = nil

    self:_InitIconGroupInfos()
    self:_RefreshColorNode(colorId)
    self.view.scrollList:UpdateCount(self:_GetScrollListCellCount())
    self:_ChangeState(true)
end

BlueprintChangeIconNode.ScrollToIcon = HL.Method(HL.String) << function(self, iconId)








    local csIndex = self:_GetGroupCellIndexOfIcon(iconId)
    if not csIndex then
        return
    end
    self.view.scrollList:UpdateCount(self:_GetScrollListCellCount(), math.max(0, csIndex - 2))
end






BlueprintChangeIconNode._InitIconGroupInfos = HL.Method() << function(self)
    local canUseIconGroupInfo = {
        {

            name = Language.LUA_FAC_BLUEPRINT_ICON_GROUP_TITLE_DEFAULT,
            list = {
                { id = FacConst.FAC_BLUEPRINT_DEFAULT_ICON, icon = FacConst.FAC_BLUEPRINT_DEFAULT_ICON, }
            },
            isFold = false,
        },
        {

            name = Language.LUA_FAC_BLUEPRINT_ICON_GROUP_TITLE_RELATED,
            list = self:_GenRelatedIconInfoList(),
            isFold = false,
            icon = "icon_fac_blueprint_change_related_icon"
        },





























    }

    self.m_iconGroupInfos = {}
    self.m_icon2PosInfo = {}
    for _, groupInfo in ipairs(canUseIconGroupInfo) do
        if groupInfo.showingTypes then
            for _, t in ipairs(groupInfo.showingTypes) do
                local idList = Tables.itemListByShowingTypeTable[t].list
                for _, id in pairs(idList) do
                    local hasWiki, wikiId = Tables.wikiEntryDataReverseTable:TryGetValue(id)
                    if hasWiki and WikiUtils.isWikiEntryUnlock(wikiId) then
                        table.insert(groupInfo.list, self:_GetItemIconInfo(id))
                    end
                end
            end
        end
        if next(groupInfo.list) then
            table.sort(groupInfo.list, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
            table.insert(self.m_iconGroupInfos, groupInfo)
            local groupIndex = #self.m_iconGroupInfos
            for k, v in ipairs(groupInfo.list) do
                self.m_icon2PosInfo[v.id] = { groupIndex, k }
            end
        end
    end
end

BlueprintChangeIconNode._GenRelatedIconInfoList = HL.Method().Return(HL.Table) << function(self)
    local addedItemIds = {}

    for _, entry in pairs(self.m_csBP.buildingNodes) do
        local templateId = entry.templateId
        local isBuilding, bData = Tables.factoryBuildingTable:TryGetValue(templateId)

        if isBuilding then
            addedItemIds[FactoryUtils.getBuildingItemId(templateId)] = true
        else
            local lData = FactoryUtils.getLogisticData(templateId)
            addedItemIds[lData.itemId] = true
        end

        if not string.isEmpty(entry.productIcon) then
            addedItemIds[entry.productIcon] = true
        end

        if isBuilding then
            local craftInfos = FactoryUtils.getBuildingCrafts(templateId)
            for _, cInfo in ipairs(craftInfos) do
                if cInfo.outcomes then
                    for _, v in ipairs(cInfo.outcomes) do
                        addedItemIds[v.id] = true
                    end
                end
            end
            if bData.type == GEnums.FacBuildingType.EnvGenWithActivator then
                local gasList = FactoryUtils.getEnvGenBlueprintGasProductIconEntries(templateId)
                for _, ge in ipairs(gasList) do
                    addedItemIds[ge.itemId] = true
                end
            end
        end
    end


    for _, entry in pairs(self.m_csBP.conveyorNodes) do
        local templateId = entry.templateId

        if templateId == FacConst.BELT_ID then
            addedItemIds[FacConst.BELT_ITEM_ID] = true
        elseif templateId == FacConst.PIPE_ID then
            addedItemIds[FacConst.PIPE_ITEM_ID] = true
        else
            local lData = FactoryUtils.getLogisticData(templateId)
            if lData then
                addedItemIds[lData.itemId] = true
            end
        end
    end

    local iconInfos = {}
    for k, _ in pairs(addedItemIds) do
        table.insert(iconInfos, self:_GetItemIconInfo(k))
    end

    return iconInfos
end

BlueprintChangeIconNode._GetItemIconInfo = HL.Method(HL.String).Return(HL.Table) << function(self, itemId)
    if FactoryUtils.isBlueprintProductIconGasEnv(itemId) then
        local spriteName = FactoryUtils.blueprintProductIconGasEnvToSpriteName(itemId)
        return {
            id = itemId,
            icon = spriteName,
            rarity = 0,
            sortId1 = 0,
            sortId2 = 0,
            isGasBlueprintIcon = true,
        }
    end
    local itemData = Tables.itemTable[itemId]
    return {
        id = itemId,
        icon = itemData.iconId,
        rarity = itemData.rarity,
        sortId1 = itemData.sortId1,
        sortId2 = itemData.sortId2,
    }
end






BlueprintChangeIconNode._OnUpdateGroupCell = HL.Method(HL.Table, HL.Number) << function(self, groupCell, csIndex)
    local nextGroupStartIndex = 0
    local groupInfo, groupIndex, startIndexInGroup, endIndexInGroup
    local countPerLine = self.view.config.COUNT_PER_LINE

    for k, v in ipairs(self.m_iconGroupInfos) do
        if nextGroupStartIndex == csIndex then

            groupInfo = v
            groupIndex = k
            break
        end
        if v.isFold then

            nextGroupStartIndex = nextGroupStartIndex + 1
        else
            local count = #v.list
            local lineCount = math.ceil(count / countPerLine)
            if csIndex <= nextGroupStartIndex + lineCount then

                groupInfo = v
                groupIndex = k
                startIndexInGroup = (csIndex - nextGroupStartIndex - 1) * countPerLine + 1
                endIndexInGroup = math.min(count, startIndexInGroup + countPerLine)
                break
            else
                nextGroupStartIndex = nextGroupStartIndex + lineCount + 1
            end
        end
    end

    groupCell.gameObject.name = "GroupCell_" .. csIndex



    local isTitle = not startIndexInGroup
    groupCell.titleBtn.gameObject:SetActive(isTitle)
    groupCell.iconNode.gameObject:SetActive(not isTitle)
    if isTitle then
        if groupInfo.name then
            groupCell.titleTxt.text = groupInfo.name
            if groupInfo.icon then
                groupCell.titleIcon:LoadSprite(UIConst.UI_SPRITE_BLUEPRINT, groupInfo.icon)
                groupCell.titleIcon.gameObject:SetActive(true)
            else
                groupCell.titleIcon.gameObject:SetActive(false)
            end
            groupCell.titleBtn.onClick:RemoveAllListeners()
            groupCell.titleBtn.onClick:AddListener(function()


            end)
        else
            groupCell.titleBtn.gameObject:SetActive(false)
        end
    else
        for k = 1, countPerLine do
            local iconCell = groupCell["iconCell" .. k]
            local info = groupInfo.list[startIndexInGroup + k - 1]
            if not info then
                iconCell.gameObject:SetActive(false)
            else
                iconCell.gameObject:SetActive(true)
                self:_UpdateIconCell(iconCell, info)
            end
        end
    end
end

BlueprintChangeIconNode._UpdateIconCell = HL.Method(HL.Table, HL.Table) << function(self, iconCell, info)
    iconCell.m_id = info.id
    if info.isGasBlueprintIcon then
        iconCell.icon:InitItemIcon("item_gold")
        iconCell.icon.view.icon:LoadSprite(UIConst.UI_SPRITE_FAC_GAS, info.icon)
    elseif info.id == FacConst.FAC_BLUEPRINT_DEFAULT_ICON then


        iconCell.icon:InitItemIcon("item_gold")
        iconCell.icon.view.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, info.icon)
    else
        iconCell.icon:InitItemIcon(info.id)
    end
    iconCell.button.onClick:RemoveAllListeners()
    iconCell.button.onClick:AddListener(function()
        self:_OnClickIcon(info.id)
    end)
    self:_UpdateIconCellSelected(iconCell)
end


BlueprintChangeIconNode._UpdateIconCellSelected = HL.Method(HL.Table) << function(self, iconCell)
    local isSelected = self.curIconId == iconCell.m_id
    iconCell.stateController:SetState(isSelected and "Selected" or "Normal")
    if DeviceInfo.usingController and isSelected then
        self.m_selectedIconCell = iconCell
    end
end






BlueprintChangeIconNode._OnClickIcon = HL.Method(HL.String) << function(self, iconId)
    if iconId == self.curIconId then
        return
    end

    self.curIconId = iconId

    local countPerLine = self.view.config.COUNT_PER_LINE
    self.view.scrollList:UpdateShowingCells(function(csIndex, obj)
        local groupCell = self.m_getGroupCell(obj)
        for k = 1, countPerLine do
            local iconCell = groupCell["iconCell" .. k]
            if iconCell.gameObject.activeInHierarchy then
                self:_UpdateIconCellSelected(iconCell)
            end
        end
    end)

    self.onChangeIconOrColor(self.curIconId, self.m_colorInfos[self.curColorIndex].id)
end

BlueprintChangeIconNode._OnClickGroupTitle = HL.Method(HL.Number) << function(self, groupIndex)
    local groupInfo = self.m_iconGroupInfos[groupIndex]
    groupInfo.isFold = not groupInfo.isFold
    local count, groupTitleCSIndex = self:_GetScrollListCellCount(groupIndex)
    self.view.scrollList:UpdateCount(count, -1, true)
    if not groupInfo.isFold then
        self.view.scrollList:ScrollToIndex(groupTitleCSIndex)
    end
end






BlueprintChangeIconNode._GetScrollListCellCount = HL.Method(HL.Opt(HL.Number)).Return(HL.Number, HL.Number) << function(self, targetGroupIndex)
    local count = 0
    local groupTitleCSIndex = 0
    local countPerLine = self.view.config.COUNT_PER_LINE
    for k, v in ipairs(self.m_iconGroupInfos) do
        if k == targetGroupIndex then
            groupTitleCSIndex = count
        end
        count = count + 1
        if not v.isFold then
            count = count + math.ceil(#v.list / countPerLine)
        end
    end
    return count, groupTitleCSIndex
end


BlueprintChangeIconNode._GetCellAndInfoOfIcon = HL.Method(HL.String).Return(HL.Opt(HL.Any, HL.Any)) << function(self, iconId)
    local posInfo = self.m_icon2PosInfo[iconId]
    if not posInfo then
        return
    end
    local groupIndex = posInfo[1]
    local indexInGroup = posInfo[2]

    local groupInfo = self.m_iconGroupInfos[groupIndex]
    if groupInfo.isFold then
        return
    end

    local csIndex = self:_GetGroupCellIndexOfIcon(iconId)
    local groupCell = self.m_getGroupCell(LuaIndex(csIndex))
    if not groupCell then
        return
    end

    local indexInGroupCell = CSIndex(indexInGroup) % self.view.config.COUNT_PER_LINE + 1
    return groupCell["iconCell" .. indexInGroupCell], groupInfo.list[indexInGroup]
end

BlueprintChangeIconNode._GetGroupCellIndexOfIcon = HL.Method(HL.String).Return(HL.Opt(HL.Number)) << function(self, iconId)
    local posInfo = self.m_icon2PosInfo[iconId]
    if not posInfo then
        return
    end
    local groupIndex = posInfo[1]
    local indexInGroup = posInfo[2]

    local groupInfo = self.m_iconGroupInfos[groupIndex]
    if groupInfo.isFold then
        return
    end

    local csIndex = 0
    local countPerLine = self.view.config.COUNT_PER_LINE
    for k, v in ipairs(self.m_iconGroupInfos) do
        if k == groupIndex then
            csIndex = csIndex + math.ceil(indexInGroup / countPerLine)
            break
        else
            if v.isFold then
                csIndex = csIndex + 1
            else
                csIndex = csIndex + math.ceil(#v.list / countPerLine) + 1
            end
        end
    end

    return csIndex
end







BlueprintChangeIconNode.curColorIndex = HL.Field(HL.Number) << -1

BlueprintChangeIconNode.m_colorCells = HL.Field(HL.Forward('UIListCache'))

BlueprintChangeIconNode.m_colorInfos = HL.Field(HL.Table)

BlueprintChangeIconNode._InitColorNode = HL.Method() << function(self)
    self.m_colorCells = UIUtils.genCellCache(self.view.colorCell)
    self.m_colorInfos = {}
    for id, data in pairs(Tables.factoryBlueprintIconBGColorTable) do
        if data.playerCanUse then
            table.insert(self.m_colorInfos, {
                id = id,
                data = data,
                sortId = data.sortId
            })
        end
    end
    table.sort(self.m_colorInfos, Utils.genSortFunction({ "sortId", "id" }))
end

BlueprintChangeIconNode._RefreshColorNode = HL.Method(HL.Number) << function(self, colorId)
    self.m_colorCells:Refresh(#self.m_colorInfos, function(cell, index)
        local info = self.m_colorInfos[index]
        cell.button.onClick:AddListener(function()
            self:_OnClickColor(index)
        end)
        cell.colorImg.color = UIUtils.getColorByString(info.data.color)
        cell.colorTxt.text = string.format("#%s", info.data.color)
        if info.id == colorId then
            cell.selectNode.gameObject:SetActive(true)
            self.curColorIndex = index
            if DeviceInfo.usingController then
                self.m_selectedColorCell = cell
            end
        else
            cell.selectNode.gameObject:SetActive(false)
        end
    end)
end

BlueprintChangeIconNode._OnClickColor = HL.Method(HL.Number) << function(self, index)
    if index == self.curColorIndex then
        return
    end
    local oldCell = self.m_colorCells:Get(self.curColorIndex)
    if oldCell then
        oldCell.selectNode.gameObject:SetActive(false)
    end
    self.curColorIndex = index
    local newCell = self.m_colorCells:Get(self.curColorIndex)
    newCell.selectNode.gameObject:SetActive(true)
    self.onChangeIconOrColor(self.curIconId, self.m_colorInfos[self.curColorIndex].id)
    self.m_selectedColorCell = newCell
end



BlueprintChangeIconNode.m_isIcon = HL.Field(HL.Boolean) << true

BlueprintChangeIconNode.IsIconTabSelected = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_isIcon
end

BlueprintChangeIconNode.SetTabState = HL.Method(HL.Boolean) << function(self, isIcon)
    self:_ChangeState(isIcon, true)
end

BlueprintChangeIconNode._ChangeState = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isIcon, resetNavi)
    if self.m_isIcon == isIcon then
        return
    end
    self.m_isIcon = isIcon
    self.view.changeIconNodeMainStateController:SetState(isIcon and "Icon" or "Color")
    if isIcon then
        self.view.iconBtnAnimationWrapper:PlayInAnimation()
        self.view.colorBtnAnimationWrapper:PlayOutAnimation()
    else
        self.view.colorBtnAnimationWrapper:PlayInAnimation()
        self.view.iconBtnAnimationWrapper:PlayOutAnimation()
    end

    if DeviceInfo.usingController and resetNavi then
        local target = isIcon and self.m_selectedIconCell.button or self.m_selectedColorCell.button
        self:SetNaviTarget(target)
    end
end

BlueprintChangeIconNode.RefreshController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    self.view.changeIconNodeMain:ManuallyFocus(true)
    self:_ChangeState(true)
    local targetIconId = self.curIconId
    if self.m_isIcon and self.m_selectedIconCell and self.m_selectedIconCell.m_id == self.curIconId and self.m_selectedIconCell.gameObject.activeInHierarchy then
        self:SetNaviTarget(self.m_selectedIconCell.button)
        Notify(MessageConst.HIDE_ITEM_TIPS)
        return
    end

    self:ScrollToIcon(targetIconId)

    local targetCell = self:_GetCellAndInfoOfIcon(targetIconId)
    if targetCell then
        self.m_selectedIconCell = targetCell
        self:SetNaviTarget(targetCell.button)
    end
    Notify(MessageConst.HIDE_ITEM_TIPS)
end

HL.Commit(BlueprintChangeIconNode)
return BlueprintChangeIconNode
