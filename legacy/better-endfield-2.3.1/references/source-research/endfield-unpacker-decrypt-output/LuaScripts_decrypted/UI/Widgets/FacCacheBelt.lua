local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FacCacheBelt = HL.Class('FacCacheBelt', UIWidgetBase)

local MAX_VIEW_PORT_COUNT = 6
local LUT_COLOR_IN_START_ID = 0
local LUT_COLOR_OUT_START_ID = 3
local MESSAGE_ITEM_INDEX = 0

FacCacheBelt.m_buildingNodeId = HL.Field(HL.Number) << -1

FacCacheBelt.m_buildingNode = HL.Field(HL.Any)

FacCacheBelt.m_buildingId = HL.Field(HL.String) << ""

FacCacheBelt.m_inBeltList = HL.Field(HL.Forward('UIListCache'))

FacCacheBelt.m_outBeltList = HL.Field(HL.Forward('UIListCache'))

FacCacheBelt.m_inBindingBeltDataMap = HL.Field(HL.Table)

FacCacheBelt.m_outBindingBeltDataMap = HL.Field(HL.Table)

FacCacheBelt.m_inBeltInfoList = HL.Field(HL.Table)

FacCacheBelt.m_outBeltInfoList = HL.Field(HL.Table)

FacCacheBelt.m_cachedSprite = HL.Field(HL.Table)

FacCacheBelt.m_isInSingleState = HL.Field(HL.Boolean) << false

FacCacheBelt.m_onInitializeFinished = HL.Field(HL.Function)

FacCacheBelt.m_needRefreshPortState = HL.Field(HL.Boolean) << false


FacCacheBelt._OnFirstTimeInit = HL.Override() << function(self)
    self.m_inBeltList = UIUtils.genCellCache(self.view.inBeltCell)
    self.m_outBeltList = UIUtils.genCellCache(self.view.outBeltCell)

    self:RegisterMessage(MessageConst.ON_CONVEYOR_CHANGE, function(args)
        local bindingNodeId, componentId, isIn, itemList = unpack(args)
        local itemId = (itemList ~= nil and itemList.Count > 0) and itemList[MESSAGE_ITEM_INDEX] or ""
        self:_RefreshBeltCellConveyorAnimation(isIn, bindingNodeId, componentId, itemId)
    end)

    self:RegisterMessage(MessageConst.ON_PORT_BLOCK_STATE_CHANGE, function(args)
        local buildingNodeId = unpack(args)
        self:_RefreshBeltCellBlockState(buildingNodeId)
    end)
end

FacCacheBelt._OnDestroy = HL.Override() << function(self)
    self:_UnRegisterInterested()
    self.m_cachedSprite = nil
end

FacCacheBelt._OnEnable = HL.Override() << function(self)
    if self.m_needRefreshPortState then
        self.m_needRefreshPortState = false
        self:RefreshBeltCellsState()
    end
end

FacCacheBelt._OnDisable = HL.Override() << function(self)
    self.m_needRefreshPortState = true
end










FacCacheBelt.InitFacCacheBelt = HL.Method(HL.Userdata, HL.Table) << function(self, buildingInfo, customInfo)
    self:_FirstTimeInit()

    if buildingInfo == nil then
        return
    end
    self.m_buildingNodeId = buildingInfo.nodeId
    self.m_buildingNode = buildingInfo.nodeHandler
    self.m_buildingId = buildingInfo.buildingId
    self.m_cachedSprite = {}
    self.m_inBindingBeltDataMap = {}
    self.m_outBindingBeltDataMap = {}
    self:_ParseCustomInfo(customInfo)

    self:_RefreshCacheBelt(true)
    if self.m_onInitializeFinished ~= nil then
        self.m_onInitializeFinished()
    end
end

FacCacheBelt._RefreshCacheBelt = HL.Method(HL.Opt(HL.Boolean)) << function(self, needDelayRefresh)
    self:_GetBeltInfoList()

    if needDelayRefresh then
        self:_StartCoroutine(function()
            coroutine.step()
        end)
    end
    self:_InitInBeltList()
    self:_InitOutBeltList()
    self:_RefreshBeltShownState()
    self:_RegisterInterested()
end

FacCacheBelt._GetBeltInfoList = HL.Method() << function(self)
    self.m_inBeltInfoList, self.m_outBeltInfoList = FactoryUtils.getBuildingPortState(self.m_buildingNodeId, false)


    local inIndexList, outIndexList = self.m_inIndexList, self.m_outIndexList
    if #inIndexList > 0 or #outIndexList > 0 then
        self.m_inBeltInfoList = self:_FilterBeltInfoList(self.m_inBeltInfoList, inIndexList)
        self.m_outBeltInfoList = self:_FilterBeltInfoList(self.m_outBeltInfoList, outIndexList)
    end
end

FacCacheBelt._GetItemSprite = HL.Method(HL.String).Return(HL.Userdata) << function(self, itemId)
    local itemSprite = self.m_cachedSprite[itemId]
    if itemSprite == nil then
        local success, itemData = Tables.itemTable:TryGetValue(itemId)
        if success then
            itemSprite = self:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
            self.m_cachedSprite[itemId] = itemSprite
        end
    end
    return itemSprite
end

FacCacheBelt._GetIsBeltBlocked = HL.Method(HL.Number, HL.Boolean).Return(HL.Boolean) << function(self, nodeId, isIn)
    local infoList = isIn and self.m_inBeltInfoList or self.m_outBeltInfoList
    if infoList == nil then
        return false
    end
    for _, info in pairs(infoList) do
        if info ~= nil and info.touchNodeId == nodeId then
            return info.isBlock
        end
    end
    return false
end

FacCacheBelt._GetBeltHeightAndSpaceByPortCount = HL.Method(HL.Number).Return(HL.Number, HL.Number) << function(self, portCount)
    local config = self.view.config
    if portCount == 2 then
        return config.TWO_PORTS_BELT_HEIGHT, config.TWO_PORTS_BELT_SPACING
    elseif portCount == 3 then
        return config.THREE_PORTS_BELT_HEIGHT, config.THREE_PORTS_BELT_SPACING
    elseif portCount == 4 then
        return config.FOUR_PORTS_BELT_HEIGHT, config.FOUR_PORTS_BELT_SPACING
    elseif portCount == 5 then
        return config.FIVE_PORTS_BELT_HEIGHT, config.FIVE_PORTS_BELT_SPACING
    elseif portCount == 6 then
        return config.SIX_PORTS_BELT_HEIGHT, config.SIX_PORTS_BELT_SPACING
    else
        return config.SINGLE_PORT_BELT_HEIGHT, 0
    end
end

FacCacheBelt._FilterBeltInfoList = HL.Method(HL.Table, HL.Table).Return(HL.Table) << function(self, resource, filter)
    filter = lume.invert(filter)
    local result = {}
    for _, info in ipairs(resource) do
        if filter[LuaIndex(info.index)] ~= nil then
            table.insert(result, info)
        end
    end

    return result
end




FacCacheBelt._InitInBeltList = HL.Method() << function(self)
    local inPortCount = #self.m_inBeltInfoList
    self.m_inBindingBeltDataMap = {}
    self.m_inEndSlotGroup = self.m_inEndSlotGroupGetter() or {}

    if inPortCount == 0 then
        self.view.inBeltGroup.gameObject:SetActiveIfNecessary(false)
        return
    else
        self.view.inBeltGroup.gameObject:SetActiveIfNecessary(true)
    end

    local inBeltHeight, inBeltSpacing = self:_GetBeltHeightAndSpaceByPortCount(inPortCount)
    self.view.inBeltLayoutGroup.spacing = inBeltSpacing

    self.m_inBeltList:Refresh(inPortCount, function(cell, index)
        local info = self.m_inBeltInfoList[index]
        self:_RefreshBeltCellState(cell, info)
        UIUtils.setSizeDeltaY(cell.rectTransform, inBeltHeight)
        cell.gameObject.name = "Belt_"..info.index

        if info.isBinding then
            self.m_inBindingBeltDataMap[info.touchNodeId] = {
                beltCell = cell,
                beltInfo = info,
                touchCompId = info.touchCompId,
            }
        end
    end)

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.inBeltGroup)

    self:_InitBeltGroupLine(true)
end

FacCacheBelt._InitOutBeltList = HL.Method() << function(self)
    local outPortCount = #self.m_outBeltInfoList
    self.m_outBindingBeltDataMap = {}
    self.m_outEndSlotGroup = self.m_outEndSlotGroupGetter() or {}

    if outPortCount == 0 then
        self.view.outBeltGroup.gameObject:SetActiveIfNecessary(false)
        return
    else
        self.view.outBeltGroup.gameObject:SetActiveIfNecessary(true)
    end

    local outBeltHeight, outBeltSpacing = self:_GetBeltHeightAndSpaceByPortCount(outPortCount)
    self.view.outBeltLayoutGroup.spacing = outBeltSpacing

    self.m_outBeltList:Refresh(outPortCount, function(cell, index)
        local info = self.m_outBeltInfoList[index]
        self:_RefreshBeltCellState(cell, info)
        UIUtils.setSizeDeltaY(cell.rectTransform, outBeltHeight)
        cell.gameObject.name = "Belt_"..info.index

        if info.isBinding then
            self.m_outBindingBeltDataMap[info.touchNodeId] = {
                beltCell = cell,
                beltInfo = info,
                touchCompId = info.touchCompId,
            }
        end
    end)

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.outBeltGroup)

    self:_InitBeltGroupLine(false)
end

FacCacheBelt._InitBeltGroupLine = HL.Method(HL.Boolean) << function(self, isIn)
    local endLineGroup = self:_GetEndLineGroup(isIn)
    local colorStartId = isIn and LUT_COLOR_IN_START_ID or LUT_COLOR_OUT_START_ID
    local beltList = isIn and self.m_inBeltList or self.m_outBeltList
    local infoList = isIn and self.m_inBeltInfoList or self.m_outBeltInfoList
    local bindingMap = isIn and self.m_inBindingBeltDataMap or self.m_outBindingBeltDataMap
    local drawer = isIn and self.view.inFacLineDrawer or self.view.outFacLineDrawer
    if self.m_noGroup then

        local portColor = CSFactoryUtil.GetPedestalLUTColor(colorStartId)
        for index, beltInfo in ipairs(infoList) do
            local beltCell = beltList:GetItem(index)
            local bindingData = bindingMap[beltInfo.touchNodeId]
            self:_RefreshBeltCellColor(beltCell, portColor)
            self:_DrawBeltGroupLine({
                drawer = drawer,
                startLine = beltCell.facLineCell,
                endLineList = endLineGroup[1],
                color = portColor,
                bindingData = bindingData,
            })
        end
        return
    end

    local layoutData = FactoryUtils.getMachineCraftCacheLayoutData(self.m_buildingNodeId)
    if layoutData == nil then
        return
    end

    local caches = isIn and layoutData.normalIncomeCaches or layoutData.normalOutcomeCaches
    if caches == nil then
        return
    end

    local slotGroup = isIn and self.m_inEndSlotGroup or self.m_outEndSlotGroup
    for index, cache in ipairs(caches) do
        local portColor = CSFactoryUtil.GetPedestalLUTColor(colorStartId + index - 1)
        if cache.ports.Count == #infoList then
            for i = 0, cache.ports.Count - 1 do
                local beltCell = beltList:GetItem(LuaIndex(i))
                local beltInfo = infoList[LuaIndex(i)]
                local bindingData = bindingMap[beltInfo.touchNodeId]
                self:_RefreshBeltCellColor(beltCell, portColor)
                self:_DrawBeltGroupLine({
                    drawer = drawer,
                    startLine = beltCell.facLineCell,
                    endLineList = endLineGroup[index],
                    color = portColor,
                    bindingData = bindingData,
                })
                if bindingData ~= nil then
                    bindingData.slotList = slotGroup[index]
                end
            end
        end
    end
end









FacCacheBelt._DrawBeltGroupLine = HL.Method(HL.Table) << function(self, drawInfo)
    local drawer = drawInfo.drawer
    local startLine = drawInfo.startLine
    local endLineList = drawInfo.endLineList
    local color = drawInfo.color
    local bindingData = drawInfo.bindingData
    local beltLineInfo = {}
    local lineIdList = {}

    beltLineInfo.drawer = drawer
    for _, endLine in ipairs(endLineList) do
        local lineId = drawer:DrawLine(startLine, endLine)
        table.insert(lineIdList, lineId)
        drawer:PlayLineAnimation(lineId, "fac_cache_line_default")
        drawer:ChangeLineColor(lineId, color)
        startLine.gameObject:SetActive(false)
        endLine.gameObject:SetActive(false)
    end
    beltLineInfo.lineIdList = lineIdList


    if bindingData ~= nil then
        bindingData.beltLineInfo = beltLineInfo
    end
end

FacCacheBelt._RefreshBeltShownState = HL.Method() << function(self)

    local needShow = GameInstance.remoteFactoryManager.unlockSystem.systemUnlockedBelt and
        GameInstance.remoteFactoryManager:IsFacNodeInMainRegion(
            self.m_buildingNode.belongChapter.chapterId,
            self.m_buildingNodeId
        )

    self.view.gameObject:SetActiveIfNecessary(needShow)
end






local DELAY_ANIM_SLOT_PLAY_DURATION = 0.35
local DELAY_ANIM_LINE_PLAY_DURATION = 0.45
local DELAY_ANIM_ITEM_PUT_PLAY_DURATION = 0.55

local DELAY_ANIM_SLOT_PLAY_TIMER = "timerItemPut"
local DELAY_ANIM_LINE_PLAY_TIMER = "timerItemPut"
local DELAY_ANIM_ITEM_PUT_PLAY_TIMER = "timerItemPut"

FacCacheBelt.m_noGroup = HL.Field(HL.Boolean) << false

FacCacheBelt.m_inEndSlotGroup = HL.Field(HL.Table)

FacCacheBelt.m_inEndSlotGroupGetter = HL.Field(HL.Function)

FacCacheBelt.m_outEndSlotGroup = HL.Field(HL.Table)

FacCacheBelt.m_outEndSlotGroupGetter = HL.Field(HL.Function)

FacCacheBelt.m_inIndexList = HL.Field(HL.Table)

FacCacheBelt.m_outIndexList = HL.Field(HL.Table)

FacCacheBelt.m_stateRefreshCallback = HL.Field(HL.Function)

FacCacheBelt._ParseCustomInfo = HL.Method(HL.Table) << function(self, customInfo)
    if customInfo == nil then
        return
    end

    self.m_noGroup = customInfo.noGroup
    self.m_inEndSlotGroupGetter = customInfo.inEndSlotGroupGetter or function()end
    self.m_outEndSlotGroupGetter = customInfo.outEndSlotGroupGetter or function()end
    self.m_inIndexList = customInfo.inIndexList or {}
    self.m_outIndexList = customInfo.outIndexList or {}
    self.m_stateRefreshCallback = customInfo.stateRefreshCallback or function()end
    self.m_onInitializeFinished = customInfo.onInitializeFinished or function()end
end

FacCacheBelt._GetEndLineGroup = HL.Method(HL.Boolean).Return(HL.Table) << function(self, isIn)
    if self.m_noGroup then
        local singleLineCell = isIn and self.view.inEndLineCell or self.view.outEndLineCell
        return { {singleLineCell} }
    else
        local slotGroup = isIn and self.m_inEndSlotGroup or self.m_outEndSlotGroup
        local lineGroup = {}
        for _, slotList in ipairs(slotGroup) do
            local lineList = {}
            for _, slot in ipairs(slotList) do
                table.insert(lineList, slot:GetNormalSlotLine())
            end
            table.insert(lineGroup, lineList)
        end
        return lineGroup
    end
end






FacCacheBelt.m_registered = HL.Field(HL.Boolean) << false

FacCacheBelt._RegisterInterested = HL.Method() << function(self)
    if self.m_registered then
        self:_UnRegisterInterested()
    end

    if self.m_inBindingBeltDataMap ~= nil then
        for nodeId, _ in pairs(self.m_inBindingBeltDataMap) do
            GameInstance.remoteFactoryManager:RegisterInterestedUnitId(nodeId)
        end
    end

    if self.m_outBindingBeltDataMap ~= nil then
        for nodeId, _ in pairs(self.m_outBindingBeltDataMap) do
            GameInstance.remoteFactoryManager:RegisterInterestedUnitId(nodeId)
        end
    end

    self.m_registered = true
end

FacCacheBelt._UnRegisterInterested = HL.Method() << function(self)
    if not self.m_registered then
        return
    end

    if self.m_inBindingBeltDataMap ~= nil then
        for nodeId, _ in pairs(self.m_inBindingBeltDataMap) do
            GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(nodeId)
        end
    end

    if self.m_outBindingBeltDataMap ~= nil then
        for nodeId, _ in pairs(self.m_outBindingBeltDataMap) do
            GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(nodeId)
        end
    end

    self.m_registered = false
end






FacCacheBelt._RefreshBeltCellColor = HL.Method(HL.Any, HL.Any) << function(self, cell, color)
    if cell == nil or color == nil then
        return
    end

    cell.decoLine.color = color
end

FacCacheBelt._RefreshBeltCellState = HL.Method(HL.Any, HL.Table) << function(self, cell, info)
    if cell == nil or info == nil then
        return
    end

    cell.normalBG.gameObject:SetActiveIfNecessary(not info.isBlock)
    cell.blockItemNode.gameObject:SetActiveIfNecessary(info.isBlock)

    local animName = info.isBlock and "inbeltcell_default" or "inbeltcell_loop"
    cell.animationWrapper:PlayWithTween(animName)

    cell.bg.gameObject:SetActiveIfNecessary(info.isBinding)


    if self.m_stateRefreshCallback ~= nil then
        self.m_stateRefreshCallback(info)
    end
end

FacCacheBelt._RefreshBeltCellConveyorAnimation = HL.Method(HL.Boolean, HL.Number, HL.Number, HL.String) << function(
    self, isIn, nodeId, compId, itemId)
    if self.m_isInSingleState then
        return
    end

    local beltMap = isIn and self.m_outBindingBeltDataMap or self.m_inBindingBeltDataMap
    if beltMap == nil then
        return
    end

    local beltData = beltMap[nodeId]
    if beltData == nil then
        return
    end

    if beltData.touchCompId ~= compId then
        return
    end

    if self:_GetIsBeltBlocked(nodeId, not isIn) then
        return
    end

    local cell, info = beltData.beltCell, beltData.beltLineInfo
    if cell == nil or info == nil then
        return
    end

    local animName = isIn and "conveyorbelt_itemright_changed" or "conveyorbelt_item_changed"

    local itemSprite = self:_GetItemSprite(itemId)
    cell.putInItemImage.sprite = itemSprite
    cell.blockItemIcon.sprite = itemSprite
    cell.blockItemIcon.gameObject:SetActiveIfNecessary(true)

    local fullSuccess, fullBottleData = Tables.fullBottleTable:TryGetValue(itemId)
    if fullSuccess then
        local liquidItemId = fullBottleData.liquidId
        local liquidSprite = self:_GetItemSprite(liquidItemId)
        cell.liquidIcon.sprite = liquidSprite
        cell.liquidIcon.gameObject:SetActive(true)
    else
        local jarSuccess, gasJarData = Tables.fullGasJarTable:TryGetValue(itemId)
        if jarSuccess then
            local gasItemId = gasJarData.gasId
            local gasSprite = self:_GetItemSprite(gasItemId)
            cell.liquidIcon.sprite = gasSprite
            cell.liquidIcon.gameObject:SetActive(true)
        else
            cell.liquidIcon.gameObject:SetActive(false)
        end
    end

    local targetLineIndex = 1
    if self.m_noGroup then
        if not isIn then

            cell.putInItemAnimation:PlayWithTween(animName)
            info[DELAY_ANIM_SLOT_PLAY_TIMER] = self:_StartTimer(DELAY_ANIM_ITEM_PUT_PLAY_DURATION, function()
                info.drawer:PlayLineAnimation(info.lineIdList[targetLineIndex], "fac_cache_line_in")
                info[DELAY_ANIM_SLOT_PLAY_TIMER] = self:_ClearTimer(info[DELAY_ANIM_SLOT_PLAY_TIMER])
            end)
        else
            info.drawer:PlayLineAnimation(info.lineIdList[targetLineIndex], "fac_cache_line_in")
            info[DELAY_ANIM_LINE_PLAY_TIMER] = self:_StartTimer(DELAY_ANIM_LINE_PLAY_DURATION, function()
                cell.putInItemAnimation:PlayWithTween(animName)
                info[DELAY_ANIM_LINE_PLAY_TIMER] = self:_ClearTimer(info[DELAY_ANIM_LINE_PLAY_TIMER])
            end)
        end
    else

        local slotList = beltData.slotList
        for index, slot in ipairs(slotList) do
            if slot:GetCurrentNormalSlotItemId() == itemId then
                targetLineIndex = index
                break
            end
        end

        local targetSlot
        targetSlot = slotList[targetLineIndex]
        if not isIn then

            cell.putInItemAnimation:PlayWithTween(animName)
            info[DELAY_ANIM_ITEM_PUT_PLAY_TIMER] = self:_StartTimer(DELAY_ANIM_ITEM_PUT_PLAY_DURATION, function()
                info.drawer:PlayLineAnimation(info.lineIdList[targetLineIndex], "fac_cache_line_in")
                info[DELAY_ANIM_ITEM_PUT_PLAY_TIMER] = self:_ClearTimer(info[DELAY_ANIM_ITEM_PUT_PLAY_TIMER])
                info[DELAY_ANIM_LINE_PLAY_TIMER] = self:_StartTimer(DELAY_ANIM_LINE_PLAY_DURATION, function()
                    info.drawer:PlayPortLineDecoAnimation(info.lineIdList[targetLineIndex], "itemslot_fac_deco_in", false)
                    info[DELAY_ANIM_LINE_PLAY_TIMER] = self:_ClearTimer(info[DELAY_ANIM_LINE_PLAY_TIMER])
                end)
            end)
        else

            info.drawer:PlayPortLineDecoAnimation(info.lineIdList[targetLineIndex], "itemslot_fac_deco_in", false)
            info[DELAY_ANIM_SLOT_PLAY_TIMER] = self:_StartTimer(DELAY_ANIM_SLOT_PLAY_DURATION, function()
                info.drawer:PlayLineAnimation(info.lineIdList[targetLineIndex], "fac_cache_line_in")
                info[DELAY_ANIM_SLOT_PLAY_TIMER] = self:_ClearTimer(info[DELAY_ANIM_SLOT_PLAY_TIMER])
                info[DELAY_ANIM_LINE_PLAY_TIMER] = self:_StartTimer(DELAY_ANIM_LINE_PLAY_DURATION, function()
                    cell.putInItemAnimation:PlayWithTween(animName)
                    info[DELAY_ANIM_LINE_PLAY_TIMER] = self:_ClearTimer(info[DELAY_ANIM_LINE_PLAY_TIMER])
                end)
            end)
        end
    end
end

FacCacheBelt._RefreshBeltCellBlockState = HL.Method(HL.Number) << function(self, buildingNodeId)
    if self.m_buildingNodeId ~= buildingNodeId then
        return
    end

    self:_GetBeltInfoList()

    local inBeltCount = self.m_inBeltList:GetCount()
    for index = 1, inBeltCount do
        local cell = self.m_inBeltList:GetItem(index)
        self:_RefreshBeltCellState(cell, self.m_inBeltInfoList[index])
    end

    local outBeltCount = self.m_outBeltList:GetCount()
    for index = 1, outBeltCount do
        local cell = self.m_outBeltList:GetItem(index)
        self:_RefreshBeltCellState(cell, self.m_outBeltInfoList[index])
    end
end






FacCacheBelt.RefreshCacheBelt = HL.Method() << function(self)
    self.view.inFacLineDrawer:ClearDrawer()
    self.view.outFacLineDrawer:ClearDrawer()
    self:_RefreshCacheBelt()
end

FacCacheBelt.SetCacheBeltSingleState = HL.Method(HL.Boolean) << function(self, useSingleState)


    local inBeltCount = self.m_inBeltList:GetCount()
    for index = 1, inBeltCount do
        local cell = self.m_inBeltList:GetItem(index)
        self:_RefreshBeltCellState(cell, self.m_inBeltInfoList[index])
        cell.blockNode.gameObject:SetActiveIfNecessary(not useSingleState)
        cell.itemNode.gameObject:SetActiveIfNecessary(not useSingleState)
    end

    local outBeltCount = self.m_outBeltList:GetCount()
    for index = 1, outBeltCount do
        local cell = self.m_outBeltList:GetItem(index)
        self:_RefreshBeltCellState(cell, self.m_outBeltInfoList[index])
        cell.blockNode.gameObject:SetActiveIfNecessary(not useSingleState)
        cell.itemNode.gameObject:SetActiveIfNecessary(not useSingleState)
    end

    self.m_isInSingleState = useSingleState
end


FacCacheBelt.RefreshBeltCellsState = HL.Method() << function(self)
    local inBeltCount = self.m_inBeltList:GetCount()
    for index = 1, inBeltCount do
        local cell = self.m_inBeltList:GetItem(index)
        self:_RefreshBeltCellState(cell, self.m_inBeltInfoList[index])
    end

    local outBeltCount = self.m_outBeltList:GetCount()
    for index = 1, outBeltCount do
        local cell = self.m_outBeltList:GetItem(index)
        self:_RefreshBeltCellState(cell, self.m_outBeltInfoList[index])
    end
end




HL.Commit(FacCacheBelt)
return FacCacheBelt

