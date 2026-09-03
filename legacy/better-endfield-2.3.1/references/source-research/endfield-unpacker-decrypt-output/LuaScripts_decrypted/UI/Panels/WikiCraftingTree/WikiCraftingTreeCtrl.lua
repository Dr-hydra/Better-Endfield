
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WikiCraftingTree

WikiCraftingTreeCtrl = HL.Class('WikiCraftingTreeCtrl', uiCtrl.UICtrl)



local LINE_TYPE = {
    Solid = 1,
    Translucent = 2,
    Dotted = 3,
}

local ITEM_CELL_HEIGHT = 230
local ITEM_CELL_WIDTH = 175
local ITEM_CELL_GAP_WIDTH = 175 + 120 * 2
local CREATE_LINE_THRESHOLD = 5

local LINE_THICKNESS = {
    [LINE_TYPE.Solid] = 6,
    [LINE_TYPE.Translucent] = 4,
    [LINE_TYPE.Dotted] = 8,
}

local CONTENT_PADDING = Vector2(100, 100)

local MORE_CRAFT_CELL_GAP_HEIGHT = 130
local CONTROLLER_ZOOM_SPEED = 300

local START_ITEM_CRAFT_KEY = "original"

local GESTURE_TYPE = {
    NORMAL = "Normal",
    GESTURE = "gesture",
    SINGLE = "single",
}








WikiCraftingTreeCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHANGE_WIKI_CRAFTING_TREE] = '_ChangeWikiCraftingTree',
}

WikiCraftingTreeCtrl.m_wikiEntryShowData = HL.Field(HL.Table)

WikiCraftingTreeCtrl.m_forceShowBackBtn = HL.Field(HL.Any)

WikiCraftingTreeCtrl.m_zoomScale = HL.Field(HL.Number) << 1

WikiCraftingTreeCtrl.m_zoomInTickKey = HL.Field(HL.Number) << -1

WikiCraftingTreeCtrl.m_zoomOutTickKey = HL.Field(HL.Number) << -1

WikiCraftingTreeCtrl.m_zoomBindingGroupId = HL.Field(HL.Number) << -1

WikiCraftingTreeCtrl.m_zoomRatioTimerId = HL.Field(HL.Number) << -1

WikiCraftingTreeCtrl.m_originSizeDelta = HL.Field(HL.Any) << Vector2.zero


WikiCraftingTreeCtrl.m_zoomGestureState = HL.Field(HL.String) << GESTURE_TYPE.NORMAL
WikiCraftingTreeCtrl.m_zoomLastLocal = HL.Field(HL.Any)
WikiCraftingTreeCtrl.m_zoomSingleLastLocal = HL.Field(HL.Any)
WikiCraftingTreeCtrl.m_zoomGestureTickKey = HL.Field(HL.Number) << -1






WikiCraftingTreeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    local args = arg
    self.m_wikiEntryShowData = args.wikiEntryShowData
    self.m_jumpCraftId = args.craftId
    self.m_forceShowBackBtn = args.forceShowBackBtn
    self.view.scrollView.disableScroll = true

    self:_InitZoom()

    ITEM_CELL_HEIGHT = self.view.config.ITEM_CELL_HEIGHT_CT
    MORE_CRAFT_CELL_GAP_HEIGHT = self.view.config.MORE_CRAFT_CELL_GAP_HEIGHT

    self:_InitController()

    self:_InitAllCellCache()

    self:_ActivateBottom(false)
    self.view.scrollViewBtn.onClick:AddListener(function()
        self:_ActivateBottom(false, true)
    end)

    self:_InitAllCellCache()
    self:_RefreshCraft(self.m_wikiEntryShowData.wikiEntryData.refItemId)

    self:_RefreshTop()
    self.m_phase:ActiveCommonSceneItem(true)
    self:_PlayBgDecoAnim(true)
end

WikiCraftingTreeCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return {
        wikiEntryShowData = self.m_wikiEntryShowData,
        forceShowBackBtn = self.m_forceShowBackBtn,
    }
end

WikiCraftingTreeCtrl.OnShow = HL.Override() << function (self)
    if self.m_phase then
        self.m_phase:ActiveCommonSceneItem(true)
    end
    self:_PlayBgDecoAnim(true)
end

WikiCraftingTreeCtrl.OnClose = HL.Override() << function(self)
    if self.m_tweenCraftAlpha then
        self.m_tweenCraftAlpha:Kill()
        self.m_tweenCraftAlpha = nil
    end
    if self.m_tweenCraftExpand then
        self.m_tweenCraftExpand:Kill()
        self.m_tweenCraftExpand = nil
    end
    LuaUpdate:Remove(self.m_zoomInTickKey)
    LuaUpdate:Remove(self.m_zoomOutTickKey)
    LuaUpdate:Remove(self.m_zoomGestureTickKey)
    self.m_zoomGestureTickKey = -1
    self.m_zoomGestureState = GESTURE_TYPE.NORMAL
    self:_CloseZoomRatioTimer()
end

WikiCraftingTreeCtrl._OnPlayAnimationOut = HL.Override() << function(self)
    WikiCraftingTreeCtrl.Super._OnPlayAnimationOut(self)

    if self.m_phase and self.m_phase:_CheckAllTransitionDone() then
        self:_PlayBgDecoAnim(false)
    end
end





WikiCraftingTreeCtrl._RefreshTop = HL.Method() << function(self)

    local wikiTopArgs = {
        phase = self.m_phase,
        panelId = PANEL_ID,
        categoryType = self.m_wikiEntryShowData.wikiCategoryType,
        wikiEntryShowData = self.m_wikiEntryShowData,
        forceShowBackBtn = self.m_forceShowBackBtn,
    }
    self.view.top:InitWikiTop(wikiTopArgs)
end





WikiCraftingTreeCtrl.m_itemCellCache = HL.Field(HL.Forward("UIGoCache"))

WikiCraftingTreeCtrl.m_buildingCellCache = HL.Field(HL.Forward("UIGoCache"))

WikiCraftingTreeCtrl.m_moreCraftCellCache = HL.Field(HL.Forward("UIGoCache"))

WikiCraftingTreeCtrl.m_lineCellCacheTable = HL.Field(HL.Table)

WikiCraftingTreeCtrl.m_curveLeftCellCacheTable = HL.Field(HL.Table)

WikiCraftingTreeCtrl.m_curveRightCellCacheTable = HL.Field(HL.Table)

WikiCraftingTreeCtrl._RecycleAllCell = HL.Method() << function(self)
    self.m_itemCellCache:RecycleAll()
    self.m_buildingCellCache:RecycleAll()
    self.m_moreCraftCellCache:RecycleAll()
    for _, lineCellCache in pairs(self.m_lineCellCacheTable) do
        lineCellCache:RecycleAll()
    end
    for _, curveLeftCellCache in pairs(self.m_curveLeftCellCacheTable) do
        curveLeftCellCache:RecycleAll()
    end
    for _, curveRightCellCache in pairs(self.m_curveRightCellCacheTable) do
        curveRightCellCache:RecycleAll()
    end
end

WikiCraftingTreeCtrl._InitAllCellCache = HL.Method() << function(self)
    self.m_itemCellCache = UIUtils.genGoCache(self.view.itemCell, nil, self.view.rootNode)
    self.m_buildingCellCache = UIUtils.genGoCache(self.view.buildingCell, nil, self.view.rootNode)
    self.m_moreCraftCellCache = UIUtils.genGoCache(self.view.moreCraftCell, nil, self.view.rootNode)
    self.m_lineCellCacheTable = {
        [LINE_TYPE.Solid] = UIUtils.genGoCache(self.view.lineCell, Utils.bindLuaRef, self.view.lineRootNode),
        [LINE_TYPE.Translucent] = UIUtils.genGoCache(self.view.translucentLine, Utils.bindLuaRef, self.view.lineRootNode),
        [LINE_TYPE.Dotted] = UIUtils.genGoCache(self.view.dottedLine, Utils.bindLuaRef, self.view.lineRootNode),
    }
    self.m_curveLeftCellCacheTable = {
        [LINE_TYPE.Solid] = UIUtils.genGoCache(self.view.curveLeftCell, Utils.bindLuaRef, self.view.lineRootNode),
        [LINE_TYPE.Translucent] = UIUtils.genGoCache(self.view.translucentCurveLeft, Utils.bindLuaRef, self.view.lineRootNode),
        [LINE_TYPE.Dotted] = UIUtils.genGoCache(self.view.dottedCurveLeft, Utils.bindLuaRef, self.view.lineRootNode),
    }
    self.m_curveRightCellCacheTable = {
        [LINE_TYPE.Solid] = UIUtils.genGoCache(self.view.curveRightCell, Utils.bindLuaRef, self.view.lineRootNode),
        [LINE_TYPE.Translucent] = UIUtils.genGoCache(self.view.translucentCurveRight, Utils.bindLuaRef, self.view.lineRootNode),
        [LINE_TYPE.Dotted] = UIUtils.genGoCache(self.view.dottedCurveRight, Utils.bindLuaRef, self.view.lineRootNode),
    }
end





WikiCraftingTreeCtrl.m_rowCountLeft = HL.Field(HL.Number) << 0

WikiCraftingTreeCtrl.m_rowCountRight = HL.Field(HL.Number) << 0

WikiCraftingTreeCtrl.m_columnCountLeft = HL.Field(HL.Number) << 0

WikiCraftingTreeCtrl.m_columnCountRight = HL.Field(HL.Number) << 0

WikiCraftingTreeCtrl.m_craftItemIds = HL.Field(HL.Table)

WikiCraftingTreeCtrl.m_sourceItemCell = HL.Field(HL.Userdata)

WikiCraftingTreeCtrl.m_debugCounter = HL.Field(HL.Number) << 0

WikiCraftingTreeCtrl.m_selectedCell = HL.Field(HL.Any)

WikiCraftingTreeCtrl.m_tweenCraftAlpha = HL.Field(HL.Userdata)

WikiCraftingTreeCtrl.m_tweenCraftExpand = HL.Field(HL.Userdata)

WikiCraftingTreeCtrl.m_repeatedItemExpandStates = HL.Field(HL.Table)

WikiCraftingTreeCtrl.m_repeatedItemIds = HL.Field(HL.Table)

WikiCraftingTreeCtrl.m_mainItemId = HL.Field(HL.String) << ''

WikiCraftingTreeCtrl.m_pinnedCraftId = HL.Field(HL.Any)

WikiCraftingTreeCtrl.m_jumpCraftId = HL.Field(HL.Any)

WikiCraftingTreeCtrl.m_itemCraftExpandStates = HL.Field(HL.Table)

WikiCraftingTreeCtrl.m_collapsedCraftKey = HL.Field(HL.String) << ''

WikiCraftingTreeCtrl.m_collapsedBuildingCell = HL.Field(HL.Userdata)

WikiCraftingTreeCtrl.m_toggledCraftKey = HL.Field(HL.String) << ''

WikiCraftingTreeCtrl.m_toggledItemCell = HL.Field(HL.Userdata)

WikiCraftingTreeCtrl._RefreshCraft = HL.Method(HL.String) << function(self, itemId)
    self.m_mainItemId = itemId
    self.m_debugCounter = 0
    self.m_craftItemIds = {}
    self.m_repeatedItemIds = {}
    self.m_repeatedItemExpandStates = {}
    self.m_itemCraftExpandStates = {}
    self.m_rowCountLeft = 0
    self.m_rowCountRight = 0
    self.m_columnCountLeft = 0
    self.m_columnCountRight = 0
    self.m_sourceItemCell = nil
    self.m_toggledCraftKey = ''
    self.m_toggledItemCell = nil
    self.m_collapsedCraftKey = ''
    self.m_collapsedBuildingCell = nil
    if self.m_selectedCell then
        self.m_selectedCell:SetSelected(false)
    end
    local chapterInfo = FactoryUtils.getCurChapterInfo()
    if chapterInfo then
        self.m_pinnedCraftId = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryUtil.GetPinBoardStrId(chapterInfo.pinBoard, GEnums.FCPinPosition.Formula:GetHashCode())
    else
        self.m_pinnedCraftId = nil
    end
    self:_RecycleAllCell()
    self:_CreateLeftCraft(itemId, 0, Vector2.zero, LINE_TYPE.Solid, START_ITEM_CRAFT_KEY,  true)
    self:_CreateRightCraft(itemId, true)

    local viewportSize = self.view.viewport.rect.size
    local contentSize = Vector2(
        (ITEM_CELL_WIDTH + ITEM_CELL_GAP_WIDTH) * (self.m_columnCountLeft + self.m_columnCountRight) + ITEM_CELL_WIDTH,
        math.max(self.m_rowCountLeft, self.m_rowCountRight) * ITEM_CELL_HEIGHT)
    contentSize = contentSize + CONTENT_PADDING * 2
    local extraPadding = Vector2.zero
    if contentSize.x < viewportSize.x then
        extraPadding.x = (viewportSize.x - contentSize.x) / 2
    end
    if contentSize.y < viewportSize.y then
        extraPadding.y = (viewportSize.y - contentSize.y) / 2
    end
    contentSize = contentSize + extraPadding * 2
    self.view.content.sizeDelta = contentSize

    self.m_originSizeDelta = contentSize
    self:_AdjustContentSizeByZoom()

    self.view.content.localPosition = Vector3.zero
    self:_SetRootNodePosOnLayout(extraPadding)

    local viewMaskPadding = self.view.viewportMask.padding
    viewMaskPadding.z = viewportSize.x / 2
    viewMaskPadding.x = viewportSize.x / 2
    self.view.viewportMask.padding = viewMaskPadding
    if self.m_tweenCraftExpand then
        self.m_tweenCraftExpand:Kill()
    end
    self.m_tweenCraftExpand = DOTween.To(function()
        return self.view.viewportMask.padding
    end, function(value)
        self.view.viewportMask.padding = value
    end, Vector4.zero, self.view.config.EXPAND_ANIM_TIME):SetEase(self.view.config.EXPAND_ANIM_CURVE)

    self.view.center.alpha = 0
    if self.m_tweenCraftAlpha then
        self.m_tweenCraftAlpha:Kill()
    end
    self.m_tweenCraftAlpha = DOTween.To(function()
        return self.view.center.alpha
    end, function(value)
        self.view.center.alpha = value
    end, 1, self.view.config.ALPHA_ANIM_TIME):SetEase(self.view.config.ALPHA_ANIM_CURVE)

    if DeviceInfo.usingController then
        self:SetNaviTarget(self.m_sourceItemCell.view.itemBlack.view.button)
    end
end

WikiCraftingTreeCtrl._RefreshCraftAfterExpand = HL.Method() << function(self)
    self.m_debugCounter = 0
    self.m_craftItemIds = {}
    self.m_repeatedItemIds = {}
    self.m_rowCountLeft = 0
    self.m_rowCountRight = 0
    self.m_columnCountLeft = 0
    self.m_columnCountRight = 0
    self.m_sourceItemCell = nil
    self.m_toggledItemCell = nil
    self.m_collapsedBuildingCell = nil
    if self.m_selectedCell then
        self.m_selectedCell:SetSelected(false)
    end
    self:_RecycleAllCell()
    self:_CreateLeftCraft(self.m_mainItemId, 0, Vector2.zero, LINE_TYPE.Solid, START_ITEM_CRAFT_KEY, false)
    self:_CreateRightCraft(self.m_mainItemId, false)
    if DeviceInfo.usingController then
        if self.m_collapsedBuildingCell then
            self:SetNaviTarget(self.m_collapsedBuildingCell.view.button)
        end
        if self.m_toggledItemCell then
            self:SetNaviTarget(self.m_toggledItemCell.view.itemBlack.view.button)
        end
    end

    local viewportSize = self.view.viewport.rect.size
    local contentSize = Vector2(
        (ITEM_CELL_WIDTH + ITEM_CELL_GAP_WIDTH) * (self.m_columnCountLeft + self.m_columnCountRight) + ITEM_CELL_WIDTH,
        math.max(self.m_rowCountLeft, self.m_rowCountRight) * ITEM_CELL_HEIGHT)
    contentSize = contentSize + CONTENT_PADDING * 2
    local extraPadding = Vector2.zero
    if contentSize.x < viewportSize.x then
        extraPadding.x = (viewportSize.x - contentSize.x) / 2
    end
    if contentSize.y < viewportSize.y then
        extraPadding.y = (viewportSize.y - contentSize.y) / 2
    end
    contentSize = contentSize + extraPadding * 2
    self.view.content.sizeDelta = contentSize
    self.m_originSizeDelta = contentSize
    self:_AdjustContentSizeByZoom()

    self:_SetRootNodePosOnLayout(extraPadding)

    self:_ClampZoomContent()
end

WikiCraftingTreeCtrl._SetRootNodePosOnLayout = HL.Method(Vector2) << function(self, extraPadding)

    local posX = -(self.m_columnCountRight * (ITEM_CELL_WIDTH + ITEM_CELL_GAP_WIDTH) + ITEM_CELL_WIDTH / 2 + CONTENT_PADDING.x + extraPadding.x)
    local posY = -(ITEM_CELL_HEIGHT / 2 + CONTENT_PADDING.y + extraPadding.y)


    local extraX = self.view.content.sizeDelta.x - self.m_originSizeDelta.x
    local extraY = self.view.content.sizeDelta.y - self.m_originSizeDelta.y

    self.view.rootNode.localPosition = Vector3(posX - extraX / 2, posY - extraY / 2, 0)
end

WikiCraftingTreeCtrl._CreateLeftCraft = HL.Method(HL.String, HL.Number, Vector2, HL.Number, HL.String, HL.Boolean).Return(HL.Any) << function(
    self, itemId, columnCount, sourcePos, lineType, itemCraftKey, playInAnimation)

    self.m_debugCounter = self.m_debugCounter + 1
    if self.m_debugCounter > 100 then
        logger.error('WikiCraftingTreeCtrl._CreateCraft: self.m_debugCounter > 100, ' .. itemId)
        return
    end

    if columnCount > self.m_columnCountLeft then
         self.m_columnCountLeft = columnCount
    end



    local itemCell = self.m_itemCellCache:Get()
    itemCell.transform.localPosition = Vector3(
        -(ITEM_CELL_WIDTH + ITEM_CELL_GAP_WIDTH) * columnCount,
        -ITEM_CELL_HEIGHT * self.m_rowCountLeft,
        0)

    local itemArgs = {
        itemId = itemId,
        mainItemId = self.m_mainItemId,
        isShowMainIcon = self.m_sourceItemCell == nil,
        playInAnimation = playInAnimation,
        onClicked = function(id, cell)
            self:_OnCraftItemClicked(id, cell)
        end
    }
    itemCell:InitWikiCraftingTreeItem(itemArgs)
    itemCell:HideExpandToggle()
    itemCell.view.itemBlack.view.button.useExplicitNaviSelect = false

    if not self.m_sourceItemCell then
        self.m_sourceItemCell = itemCell
    end


    itemCell:SetRightMountPointCount(1)
    if sourcePos ~= Vector2.zero then
        local itemRightPoint = itemCell:GetRightMountPoint(self.view.rootNode.transform, 1)
        self:_CreateLeftLink(itemRightPoint, sourcePos, lineType)
    end


    local craftInfoList = FactoryUtils.getItemCrafts(itemId, false, true, true, true)

    local craftCount = craftInfoList and #craftInfoList or 0
    if craftCount == 0 then
        itemCell:SetLeftMountPointCount(0)
        self.m_rowCountLeft = self.m_rowCountLeft + 1
        return itemCell
    end

    if self.m_craftItemIds[itemId] then
        itemCell:SetLeftMountPointCount(0)
        self.m_rowCountLeft = self.m_rowCountLeft + 1

        return itemCell
    else
        self.m_craftItemIds[itemId] = true
    end

    itemCell:SetLeftMountPointCount(1)



    if self.m_repeatedItemIds[itemId] then
        local repeatedItemExpandState = self.m_repeatedItemExpandStates[itemId]
        if not repeatedItemExpandState then
            repeatedItemExpandState = {}
            self.m_repeatedItemExpandStates[itemId] = repeatedItemExpandState
        end
        local isExpand = repeatedItemExpandState[itemCraftKey] == true
        itemCell:SetExpandToggle(isExpand, function(isOn)
            if isOn then
                repeatedItemExpandState[itemCraftKey] = true
            else
                repeatedItemExpandState[itemCraftKey] = false
            end
            self.m_collapsedCraftKey = ''
            self.m_toggledCraftKey = itemCraftKey .. itemId
            self:_RefreshCraftAfterExpand()
        end)
        if itemCraftKey .. itemId == self.m_toggledCraftKey then
            self.m_toggledItemCell = itemCell
        end
        if not isExpand then
            self.m_rowCountLeft = self.m_rowCountLeft + 1
            self.m_craftItemIds[itemId] = false
            return itemCell
        end
    else
        self.m_repeatedItemIds[itemId] = true
    end


    local defaultCraftIndex, pinnedCraftIndex, jumpCraftIndex
    if craftCount > 1 then
        local defaultCraftId = WikiUtils.getItemDefaultCraftId(itemId)
        for i, craftInfo in ipairs(craftInfoList) do
            if not string.isEmpty(defaultCraftId) and defaultCraftId == craftInfo.craftId then
                defaultCraftIndex = i
            end

            if not string.isEmpty(self.m_pinnedCraftId) and self.m_pinnedCraftId == craftInfo.craftId then
                pinnedCraftIndex = i
            end

            if not string.isEmpty(self.m_jumpCraftId) and self.m_jumpCraftId == craftInfo.craftId then
                jumpCraftIndex = i
            end
        end
        if defaultCraftIndex == nil then

            for i, craftInfo in ipairs(craftInfoList) do
                if not Tables.factoryManualCraftTable:ContainsKey(craftInfo.craftId) then
                    defaultCraftIndex = i
                    break
                end
            end
        end

        local firstIndex = 1
        if jumpCraftIndex then
            firstIndex = jumpCraftIndex
        elseif pinnedCraftIndex then
            firstIndex = pinnedCraftIndex
        elseif defaultCraftIndex then
            firstIndex = defaultCraftIndex
        end

        local isCraftExpand = self.m_itemCraftExpandStates[itemCraftKey] == true
        if isCraftExpand then
            if firstIndex ~= 1 then
                local firstCraftInfo = table.remove(craftInfoList, firstIndex)
                table.insert(craftInfoList, 1, firstCraftInfo)
            end



            local moreCraftCell = self.m_moreCraftCellCache:Get()
            moreCraftCell.transform:SetAsFirstSibling()
            moreCraftCell.toggle.onValueChanged:RemoveAllListeners()
            moreCraftCell.toggle.isOn = true
            moreCraftCell.toggle.onValueChanged:AddListener(function(isOn)
                if isOn then
                    self.m_itemCraftExpandStates[itemCraftKey] = true
                else
                    self.m_itemCraftExpandStates[itemCraftKey] = false
                end
                self:_RefreshCraftAfterExpand()
            end)

            if DeviceInfo.usingController then
                moreCraftCell.gameObject:SetActive(false)
                moreCraftCell.btnMore.gameObject:SetActive(false)
                moreCraftCell.btnLess.gameObject:SetActive(true)
                moreCraftCell.btnLess.onClick:RemoveAllListeners()
                moreCraftCell.btnLess.onClick:AddListener(function()
                    self.m_itemCraftExpandStates[itemCraftKey] = false
                    self.m_toggledCraftKey = ''
                    self.m_collapsedCraftKey = itemCraftKey
                    moreCraftCell.transform:SetParent(self.view.rootNode, false)
                    self:_RefreshCraftAfterExpand()
                end)
            end

            for i, craftInfo in ipairs(craftInfoList) do
                local isLastCraft = i == craftCount
                if isLastCraft then
                    moreCraftCell.transform.localPosition = Vector3(
                        -(ITEM_CELL_WIDTH + ITEM_CELL_GAP_WIDTH) * columnCount - (ITEM_CELL_WIDTH + ITEM_CELL_GAP_WIDTH) / 2,
                        -ITEM_CELL_HEIGHT * self.m_rowCountLeft - MORE_CRAFT_CELL_GAP_HEIGHT,
                        0)
                end

                local rowCount = self.m_rowCountLeft

                local buildingCell = self:_CreateLeftItemOneCraft({
                    itemCell = itemCell,
                    craftInfo = craftInfo,
                    columnCount = columnCount,
                    lineType = i == 1 and LINE_TYPE.Solid or LINE_TYPE.Translucent,
                    itemCraftKey = itemCraftKey,
                    playInAnimation = playInAnimation,
                    isShowDefaultToggle = true,
                    craftIndex = i,
                    defaultCraftIndex = defaultCraftIndex,
                    moreCraftCell = moreCraftCell,
                })






                if i == 1 and self.m_collapsedCraftKey == itemCraftKey then
                    self.m_collapsedBuildingCell = buildingCell
                end
            end
        else


            local moreCraftCell = self.m_moreCraftCellCache:Get()
            moreCraftCell.transform:SetAsFirstSibling()
            moreCraftCell.transform.localPosition = Vector3(
                -(ITEM_CELL_WIDTH + ITEM_CELL_GAP_WIDTH) * columnCount - (ITEM_CELL_WIDTH + ITEM_CELL_GAP_WIDTH) / 2,
                -ITEM_CELL_HEIGHT * self.m_rowCountLeft - MORE_CRAFT_CELL_GAP_HEIGHT,
                0)
            moreCraftCell.toggle.onValueChanged:RemoveAllListeners()
            moreCraftCell.toggle.isOn = false
            moreCraftCell.toggle.onValueChanged:AddListener(function(isOn)
                if isOn then
                    self.m_itemCraftExpandStates[itemCraftKey] = true
                else
                    self.m_itemCraftExpandStates[itemCraftKey] = false
                end
                self:_RefreshCraftAfterExpand()
            end)
            if DeviceInfo.usingController then
                moreCraftCell.btnLess.gameObject:SetActive(false)
                moreCraftCell.btnMore.gameObject:SetActive(true)
                moreCraftCell.btnMore.onClick:RemoveAllListeners()
                moreCraftCell.btnMore.onClick:AddListener(function()
                    self.m_itemCraftExpandStates[itemCraftKey] = true
                    self.m_toggledCraftKey = ''
                    self.m_collapsedCraftKey = itemCraftKey
                    self:_RefreshCraftAfterExpand()
                end)
            end


            local linkRightPoint = itemCell:GetLeftMountPoint(self.view.rootNode, 1)
            local pos = self.view.rootNode:InverseTransformPoint(moreCraftCell.mountPoint.position)
            local linkLeftPoint = Vector2(pos.x, pos.y)
            self:_CreateLeftLink(linkLeftPoint, linkRightPoint, LINE_TYPE.Translucent)

            local rowCount = self.m_rowCountLeft
            local buildingCell = self:_CreateLeftItemOneCraft({
                itemCell = itemCell,
                craftInfo = craftInfoList[firstIndex],
                columnCount = columnCount,
                lineType = lineType,
                itemCraftKey = itemCraftKey,
                playInAnimation = playInAnimation,
                moreCraftCell = moreCraftCell,
            })





            if self.m_collapsedCraftKey == itemCraftKey then
                self.m_collapsedBuildingCell = buildingCell
            end
        end
    else
        for _, craftInfo in ipairs(craftInfoList) do
            self:_CreateLeftItemOneCraft({
                itemCell = itemCell,
                craftInfo = craftInfo,
                columnCount = columnCount,
                lineType = lineType,
                itemCraftKey = itemCraftKey,
                playInAnimation = playInAnimation,
            })
        end
    end

    self.m_craftItemIds[itemId] = false
    return itemCell
end













WikiCraftingTreeCtrl._CreateLeftItemOneCraft = HL.Method(HL.Table).Return(HL.Userdata) << function(self, arg)


    local buildingCell = self.m_buildingCellCache:Get()
    buildingCell.transform.localPosition = Vector3(
        -(ITEM_CELL_WIDTH + ITEM_CELL_GAP_WIDTH) * arg.columnCount - (ITEM_CELL_WIDTH + ITEM_CELL_GAP_WIDTH) / 2,
        -ITEM_CELL_HEIGHT * self.m_rowCountLeft,
        0)

    local buildingArgs = {
        itemId = arg.itemCell:GetItemId(),
        craftInfo = arg.craftInfo,
        isShowDefaultNode = arg.isShowDefaultToggle,
        craftIndex = arg.craftIndex,
        defaultCraftIndex = arg.defaultCraftIndex,
        moreCraftCell = arg.moreCraftCell,
        onClicked = function(id, cell)
            self:_OnCraftBuildingClicked(id, cell)
        end
    }
    buildingCell:InitWikiCraftingTreeBuilding(buildingArgs)



    local linkRightPoint = arg.itemCell:GetLeftMountPoint(self.view.rootNode, 1)
    local linkLeftPoint = buildingCell:GetRightMountPoint(self.view.rootNode)
    self:_CreateLeftLink(linkLeftPoint, linkRightPoint, arg.lineType)


    local buildingLeftPoint = buildingCell:GetLeftMountPoint(self.view.rootNode)
    for j, itemBundle in ipairs(arg.craftInfo.incomes) do

        local itemCell = self:_CreateLeftCraft(itemBundle.id, arg.columnCount + 1, buildingLeftPoint, arg.lineType,
            arg.itemCraftKey ..arg.craftInfo.craftId, arg.playInAnimation)
        if DeviceInfo.usingController and itemCell then
            local selectable = itemCell.view.itemBlack.view.button
            selectable.useExplicitNaviSelect = true
            selectable.banExplicitOnUp = true
            selectable.banExplicitOnDown = true
            selectable.banExplicitOnLeft = true
            selectable.banExplicitOnRight = false
            selectable:SetExplicitSelectOnRight(buildingCell.view.button)
        end
    end

    if DeviceInfo.usingController then
        local selectable = buildingCell.view.button
        selectable.useExplicitNaviSelect = true
        selectable.banExplicitOnRight = false
        selectable.banExplicitOnLeft = true
        selectable.banExplicitOnUp = true
        selectable.banExplicitOnDown = true
        selectable:SetExplicitSelectOnRight(arg.itemCell.view.itemBlack.view.button)
    end

    return buildingCell
end

WikiCraftingTreeCtrl._CreateRightCraft = HL.Method(HL.String, HL.Boolean) << function(self, itemId, playInAnimation)

    local craftInfoList = FactoryUtils.getItemAsInputRecipeIds(itemId)
    local unlockedCraftInfoList = {}
    for _, craftInfo in pairs(craftInfoList) do
        if craftInfo.isUnlock then
            local buildingItemId = FactoryUtils.getBuildingItemId(craftInfo.buildingId)
            if not WikiUtils.getWikiEntryIdFromItemId(buildingItemId) or WikiUtils.canShowWikiEntry(buildingItemId) and
                craftInfo.outcomes ~= nil then
                table.insert(unlockedCraftInfoList, craftInfo)
            end
        end
    end

    if not unlockedCraftInfoList or #unlockedCraftInfoList == 0 then
        self.m_sourceItemCell:SetRightMountPointCount(0)
        return
    end
    local craftCount = #unlockedCraftInfoList

    self.m_sourceItemCell:SetRightMountPointCount(1)
    self.m_rowCountRight = 0

    self.m_columnCountRight = 1
    for i, craftInfo in pairs(unlockedCraftInfoList) do


        local buildingCell = self.m_buildingCellCache:Get()
        buildingCell.transform.localPosition = Vector3(
            (ITEM_CELL_WIDTH + ITEM_CELL_GAP_WIDTH) / 2,
            -ITEM_CELL_HEIGHT * self.m_rowCountRight,
            0)

        local buildingArgs = {
            craftInfo = craftInfo,
            isShowExtraItemIcon = #craftInfo.incomes > 1,
            ignorePinCraft = true,
            onClicked = function(id, cell)
                self:_OnCraftBuildingClicked(id, cell)
            end
        }
        buildingCell:InitWikiCraftingTreeBuilding(buildingArgs)
        if DeviceInfo.usingController then
            local selectable = buildingCell.view.button
            selectable.useExplicitNaviSelect = true
            selectable.banExplicitOnRight = true
            selectable.banExplicitOnLeft = false
            selectable.banExplicitOnUp = true
            selectable.banExplicitOnDown = true
            selectable:SetExplicitSelectOnLeft(self.m_sourceItemCell.view.itemBlack.view.button)
        end






        local linkLeftPoint = self.m_sourceItemCell:GetRightMountPoint(self.view.rootNode, 1)
        local linkRightPoint = buildingCell:GetLeftMountPoint(self.view.rootNode)
        self:_CreateRightLink(linkLeftPoint, linkRightPoint, LINE_TYPE.Dotted)


        local buildingRightPoint = buildingCell:GetRightMountPoint(self.view.rootNode)
        for j, itemBundle in ipairs(craftInfo.outcomes) do


            local itemCell = self.m_itemCellCache:Get()
            itemCell.transform.localPosition = Vector3(
                ITEM_CELL_WIDTH + ITEM_CELL_GAP_WIDTH,
                -ITEM_CELL_HEIGHT * self.m_rowCountRight,
                0)

            local itemArgs = {
                itemId = itemBundle.id,
                playInAnimation = playInAnimation,
                onClicked = function(id, cell)
                    self:_OnCraftItemClicked(id, cell)
                end
            }
            itemCell:InitWikiCraftingTreeItem(itemArgs)
            itemCell:HideExpandToggle()
            if DeviceInfo.usingController then
                itemCell.view.itemBlack.view.button.useExplicitNaviSelect = false
            end


            itemCell:SetLeftMountPointCount(1)
            itemCell:SetRightMountPointCount(0)
            local itemLeftPoint = itemCell:GetLeftMountPoint(self.view.rootNode.transform, 1)
            self:_CreateRightLink(buildingRightPoint, itemLeftPoint, LINE_TYPE.Dotted)

            self.m_rowCountRight = self.m_rowCountRight + 1
        end
    end
end

WikiCraftingTreeCtrl._OnCraftItemClicked = HL.Method(HL.String, HL.Forward("WikiCraftingTreeItem")) << function(
    self, itemId, itemCell)
    logger.info('itemId:', itemId)
    if self.m_selectedCell then
        self.m_selectedCell:SetSelected(false)
    end
    self.m_selectedCell = itemCell
    itemCell:SetSelected(true)
    if DeviceInfo.usingController then
        self:_SetBottomRelativeInputGroup(itemCell.view.selectNode.inputBindingGroupMonoTarget)
    end
    self:_ActivateBottom(true, true)
    self:_RefreshBottom(itemId)
end

WikiCraftingTreeCtrl._OnCraftBuildingClicked = HL.Method(HL.String, HL.Forward("WikiCraftingTreeBuilding")) << function(
    self, buildingId, buildingCell)
    logger.info('buildingId:', buildingId)
    if self.m_selectedCell then
        self.m_selectedCell:SetSelected(false)
    end
    self.m_selectedCell = buildingCell
    buildingCell:SetSelected(true)
    local itemId = FactoryUtils.getBuildingItemId(buildingId)
    if DeviceInfo.usingController then
        self:_SetBottomRelativeInputGroup(buildingCell.view.selectNode.inputBindingGroupMonoTarget)
    end
    self:_ActivateBottom(true, true)
    self:_RefreshBottom(itemId)
end





WikiCraftingTreeCtrl._CreateLeftLink = HL.Method(Vector2, Vector2, HL.Number, HL.Opt(HL.Number)) << function(
    self, leftPoint, rightPoint, lineType, offset)
    if math.abs(leftPoint.y - rightPoint.y) < CREATE_LINE_THRESHOLD then
        self:_CreateLine(leftPoint, rightPoint, lineType)
    else
        self:_CreateLeftCurve(leftPoint, rightPoint, lineType, offset)
    end
end

WikiCraftingTreeCtrl._CreateRightLink = HL.Method(Vector2, Vector2, HL.Number, HL.Opt(HL.Number)) << function(
    self, leftPoint, rightPoint, lineType, offset)
    if math.abs(leftPoint.y - rightPoint.y) < CREATE_LINE_THRESHOLD then
        self:_CreateLine(leftPoint, rightPoint, lineType)
    else
        self:_CreateRightCurve(leftPoint, rightPoint, lineType, offset)
    end
end

WikiCraftingTreeCtrl._CreateLine = HL.Method(Vector2, Vector2, HL.Number) << function(self, pointStart, pointEnd, lineType)
    local lineCell = self.m_lineCellCacheTable[lineType]:Get()
    local pointMiddle = (pointStart + pointEnd) / 2
    lineCell.line.localPosition = Vector3(pointMiddle.x, pointMiddle.y, 0)
    lineCell.line.sizeDelta = Vector2(math.abs(pointEnd.x - pointStart.x), LINE_THICKNESS[lineType])
end

local CURVE_WIDTH = 47
local CURVE_HEIGHT = 47

WikiCraftingTreeCtrl._CreateLeftCurve = HL.Method(Vector2, Vector2, HL.Number, HL.Opt(HL.Number)) << function(
    self, pointLeft, pointRight, lineType, offset)
    local curveCell = self.m_curveLeftCellCacheTable[lineType]:Get()
    offset = offset or 0
    self:_SetCurveCell(curveCell, pointLeft, pointRight, pointRight.x - offset - CURVE_WIDTH,
        LINE_THICKNESS[lineType],  offset)
end

WikiCraftingTreeCtrl._CreateRightCurve = HL.Method(Vector2, Vector2, HL.Number, HL.Opt(HL.Number)) << function(
    self, pointLeft, pointRight, lineType, offset)
    local curveCell = self.m_curveRightCellCacheTable[lineType]:Get()
    offset = offset or 0
    self:_SetCurveCell(curveCell, pointLeft, pointRight, pointLeft.x + offset + CURVE_WIDTH,
        LINE_THICKNESS[lineType], offset)
end

WikiCraftingTreeCtrl._SetCurveCell = HL.Method(HL.Table, Vector2, Vector2, HL.Number, HL.Number, HL.Number) << function(
    self, curveCell, pointLeft, pointRight, posX, lineThickness, offset)
    local lineHeight = math.abs(pointRight.y - pointLeft.y) - CURVE_HEIGHT * 2 + lineThickness
    curveCell.verticalLine.sizeDelta = Vector2(lineHeight, lineThickness)
    local lineWidth = math.abs(pointRight.x - pointLeft.x) - CURVE_WIDTH * 2
    curveCell.topLine.sizeDelta = Vector2(offset, lineThickness)
    curveCell.bottomLine.sizeDelta = Vector2(lineWidth - offset, lineThickness)
    curveCell.transform.localPosition = Vector3(posX, (pointLeft.y + pointRight.y) / 2, 0)
end





WikiCraftingTreeCtrl._ActivateBottom = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, playAnim)
    local paddingBottom = 0

    if active then
        self.view.bottom.gameObject:SetActive(true)
        local bottomPreferredHeight = self.view.bottom.gameObject:GetComponent(typeof(Unity.UI.LayoutElement)).preferredHeight
        paddingBottom = bottomPreferredHeight - self.view.scrollView.transform.offsetMin.y
        if DeviceInfo.usingController then
            self.view.bottom.selectableNaviGroup:ManuallyFocus()
            self.view.controllerHintLayoutElement.ignoreLayout = true
        end
        self:_ChangeZoomInputGroup(false)
        self.view.tipInfoAniWrapper:PlayWithTween("wiki_craftingtree_tipinfo_in")
    else
        if playAnim then
            if self.view.bottom.gameObject.activeSelf then
                self.view.bottom.animWrapper:PlayOutAnimation(function()
                    if DeviceInfo.usingController then
                        self.view.bottom.selectableNaviGroup:ManuallyStopFocus()
                    end
                    self.view.bottom.gameObject:SetActive(false)
                    if self.m_selectedCell then
                        self.m_selectedCell:SetSelected(false)
                    end
                    if DeviceInfo.usingController then
                        self.view.controllerHintLayoutElement.ignoreLayout = false
                    end
                end)
            end
        else
            if DeviceInfo.usingController then
                self.view.bottom.selectableNaviGroup:ManuallyStopFocus()
            end
            self.view.bottom.gameObject:SetActive(false)
            if self.m_selectedCell then
                self.m_selectedCell:SetSelected(false)
            end
            if DeviceInfo.usingController then
                self.view.controllerHintLayoutElement.ignoreLayout = false
            end
        end
        self:_ChangeZoomInputGroup(true)
        self.view.tipInfoAniWrapper:PlayWithTween("wiki_craftingtree_tipinfo_out")
    end
    local viewSizeDelta = self.view.viewport.sizeDelta
    viewSizeDelta.y = -paddingBottom
    self.view.viewport.sizeDelta = viewSizeDelta
    if active and self.m_selectedCell then
        self.view.scrollView:ScrollToNaviTarget(self.m_selectedCell:GetButton())
    end
end

WikiCraftingTreeCtrl._RefreshBottom = HL.Method(HL.String) << function(self, itemId)
    local view = self.view.bottom
    local _, itemData = Tables.itemTable:TryGetValue(itemId)
    if not itemData then
        logger.error('WikiCraftingTreeCtrl._RefreshBottom: not itemData, ' .. itemId)
        return
    end
    view.nameTxt.text = itemData.name
    view.itemIcon:InitItemIcon(itemId, true)
    view.descTxt:SetAndResolveTextStyle(itemData.desc)
    view.itemTags:InitItemTags(itemId)
    UIUtils.setItemRarityImage(view.circleImg, itemData.rarity)
    UIUtils.setItemRarityImage(view.circleLightImg, itemData.rarity)
    local canShowWikiEntry = WikiUtils.canShowWikiEntry(itemId)
    view.detailBtn.gameObject:SetActive(canShowWikiEntry)
    view.detailBtn.onClick:RemoveAllListeners()
    view.detailBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_WIKI_ENTRY, { itemId = itemId })
    end)
    if view.earlyAccessNode then
        view.earlyAccessNode.gameObject:SetActiveIfNecessary(FactoryUtils.isSkipUnlockedBuildingByItemId(itemId))
    end
    view.btnClose.onClick:RemoveAllListeners()
    view.btnClose.onClick:AddListener(function()
        self:_ActivateBottom(false, true)
    end)
    local _, jumpData = Tables.wikiCraftJumpTable:TryGetValue(itemId)
    local hasBlackBox = false
    if jumpData and not string.isEmpty(jumpData.blackboxId) and
        Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacTechTree) and
        GameInstance.dungeonManager:IsDungeonActive(jumpData.blackboxId) and not Utils.isInDungeon() then
        local packageId = self:_GetBlackboxPackageId(jumpData.blackboxId)
        if not string.isEmpty(packageId) and not GameInstance.player.facTechTreeSystem:PackageIsLocked(packageId) and
            not GameInstance.player.facTechTreeSystem:PackageIsHidden(packageId) then
            hasBlackBox = true
        end
    end
    view.blackboxBtn.gameObject:SetActive(hasBlackBox)
    if hasBlackBox then
        view.blackboxBtn.onClick:RemoveAllListeners()
        local blackboxId = jumpData.blackboxId
        view.blackboxBtn.onClick:AddListener(function()
            self:_GotoBlackbox(blackboxId)
        end)
    end
    local hasBlueprint = false
    if Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacBlueprint) and not Utils.isInDungeon() and
        jumpData and not string.isEmpty(jumpData.blueprintId) then
        hasBlueprint = true
    end
    view.blueprintBtn.gameObject:SetActive(hasBlueprint and hasBlackBox)
    if hasBlueprint and hasBlackBox then
        view.blueprintBtn.onClick:RemoveAllListeners()
        view.blueprintBtn.onClick:AddListener(function()
            local blueprintId = jumpData.blueprintId
            if FactoryUtils.isSystemBlueprintUnlocked(blueprintId) then
                PhaseManager:GoToPhase(PhaseId.FacBlueprint, { blueprintType = "Sys", blueprintId = blueprintId })
            else
                Notify(MessageConst.SHOW_POP_UP,{
                    content = Language.LUA_WIKI_CRAFTING_BLUEPRINTS_JUMP_TIPS,
                    onConfirm = function()
                        self:_GotoBlackbox(jumpData.blackboxId)
                    end
                })
            end
        end)
    end
end

WikiCraftingTreeCtrl._ChangeWikiCraftingTree = HL.Method(HL.String) << function(self, itemId)
    self:_ActivateBottom(false, true)
    self.m_wikiEntryShowData = WikiUtils.getWikiEntryShowDataFromItemId(itemId)
    self:_RefreshTop()
    self:_RefreshCraft(itemId)
end

WikiCraftingTreeCtrl._GotoBlackbox = HL.Method(HL.String) << function(self, blackboxId)
    local packageId = self:_GetBlackboxPackageId(blackboxId)
    if string.isEmpty(packageId) then
        logger.error('WikiCraftingTreeCtrl._RefreshBottom: not found packageId for blackboxId, ' .. blackboxId)
        return
    else
        PhaseManager:OpenPhase(PhaseId.BlackboxEntry, {packageId = packageId, blackboxId = blackboxId})
    end
end

WikiCraftingTreeCtrl._GetBlackboxPackageId = HL.Method(HL.String).Return(HL.Any) << function(self, blackboxId)
    local packageId
    for _, groupData in pairs(Tables.facSTTGroupTable) do
        if lume.find(groupData.blackboxIds, blackboxId) then
            packageId = groupData.groupId
            break
        end
    end
    return packageId
end





WikiCraftingTreeCtrl._InitZoom = HL.Method() << function(self)
    self.m_zoomScale = 1
    self.m_zoomGestureState = GESTURE_TYPE.NORMAL
    self.view.scrollView.enabled = true

    self.view.scrollViewTouchPanel.onZoom:AddListener(function(delta)
        self:_OnZoom(delta)
    end)


    LuaUpdate:Remove(self.m_zoomGestureTickKey)
    self.m_zoomGestureTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
        self:_OnZoomGestureTick()
    end)

    self.view.tipTextState:SetState(DeviceInfo.usingTouch and "Mobile" or "Other")
    self.view.zoomRatioNode.gameObject:SetActive(false)
end

WikiCraftingTreeCtrl._GetZoomCenterScreenPos = HL.Method().Return(Vector2) << function(self)
    if DeviceInfo.usingController then
        local curTarget = InputManagerInst.controllerNaviManager.curTarget
        if curTarget then
            local worldPos = curTarget.transform.position
            local screenPos = self.uiCamera:WorldToScreenPoint(worldPos)
            return Vector2(screenPos.x, screenPos.y)
        end
    end
    if DeviceInfo.usingTouch and CS.UnityEngine.Input.touchCount >= 2 then
        local t0 = CS.UnityEngine.Input.GetTouch(0)
        local t1 = CS.UnityEngine.Input.GetTouch(1)
        return Vector2(
            (t0.position.x + t1.position.x) * 0.5,
            (t0.position.y + t1.position.y) * 0.5
        )
    end
    local mousePos = InputManager.mousePosition
    return Vector2(mousePos.x, mousePos.y)
end


WikiCraftingTreeCtrl._ScreenToViewportLocal = HL.Method(Vector2).Return(Vector2) << function(self, screenPos)
    local _, localPos = Unity.RectTransformUtility.ScreenPointToLocalPointInRectangle(
        self.view.viewport, screenPos, self.uiCamera)
    return localPos
end


WikiCraftingTreeCtrl._IsInZoomGesture = HL.Method().Return(HL.Boolean) << function(self)
    if not DeviceInfo.usingTouch then
        return false
    end
    local touchPanel = self.view.scrollViewTouchPanel
    if not touchPanel then
        return false
    end
    return touchPanel.isInGestureZoom == true
end

WikiCraftingTreeCtrl._ClampZoomContent = HL.Method() << function(self)
    self.view.scrollView:ClampContentToBounds()
end


WikiCraftingTreeCtrl._ApplyZoomToCenter = HL.Method(HL.Number, HL.Number) << function(self, newScale, oldScale)
    local scaleFactor = newScale / oldScale
    local centerInViewport = self:_ScreenToViewportLocal(self:_GetZoomCenterScreenPos())
    local pivotScreenPos = self.uiCamera:WorldToScreenPoint(self.view.content.position)
    local pivotInViewport = self:_ScreenToViewportLocal(Vector2(pivotScreenPos.x, pivotScreenPos.y))
    local dist = centerInViewport - pivotInViewport
    local compensation = dist * (1 - scaleFactor)
    self.m_zoomScale = newScale
    self:_AdjustContentSizeByZoom()

    self.view.content.localScale = Vector3.one * newScale
    self.view.content.anchoredPosition = self.view.content.anchoredPosition + compensation
end





WikiCraftingTreeCtrl._AdjustContentSizeByZoom = HL.Method() << function(self)
    local origin = self.m_originSizeDelta
    local scale = self.m_zoomScale
    if scale <= 0 or origin.x <= 0 or origin.y <= 0 then
        return
    end
    local viewportSize = self.view.viewport.rect.size
    self.view.content.sizeDelta = Vector2(
        math.max(origin.x, viewportSize.x / scale),
        math.max(origin.y, viewportSize.y / scale))
end

WikiCraftingTreeCtrl._OnZoom = HL.Method(HL.Number) << function(self, delta)
    local config = self.view.config
    local newScale = lume.clamp(self.m_zoomScale - delta * config.ZOOM_SENSITIVITY, config.ZOOM_MIN, config.ZOOM_MAX)
    local oldScale = self.m_zoomScale

    self:_TrySetZoomRatioInfo(newScale)

    if self:_IsInZoomGesture() then

        local centerLocal = self:_ScreenToViewportLocal(self:_GetZoomCenterScreenPos())
        if self.m_zoomGestureState ~= GESTURE_TYPE.GESTURE then
            if self.m_zoomGestureState == GESTURE_TYPE.NORMAL then
                self.view.scrollView:StopMovement()
                self.view.scrollView.enabled = false
            end
            self.m_zoomGestureState = GESTURE_TYPE.GESTURE
        elseif self.m_zoomLastLocal then
            self.view.content.anchoredPosition = self.view.content.anchoredPosition +
                Vector2(centerLocal.x - self.m_zoomLastLocal.x, centerLocal.y - self.m_zoomLastLocal.y)
        end
        self.m_zoomLastLocal = { x = centerLocal.x, y = centerLocal.y }

        if newScale ~= oldScale then
            self:_ApplyZoomToCenter(newScale, oldScale)
        end
        self:_ClampZoomContent()
    else


        if newScale ~= oldScale then
            self.view.scrollView:StopMovement()
            self:_ApplyZoomToCenter(newScale, oldScale)
            self:_ClampZoomContent()
        end
    end
end


WikiCraftingTreeCtrl._OnZoomGestureTick = HL.Method() << function(self)
    local state = self.m_zoomGestureState
    if state == GESTURE_TYPE.NORMAL then
        return
    end

    local touchCount = CS.UnityEngine.Input.touchCount

    if state == GESTURE_TYPE.GESTURE and not self:_IsInZoomGesture() then
        if touchCount >= 1 then
            self.m_zoomGestureState = GESTURE_TYPE.SINGLE
            self.m_zoomSingleLastLocal = nil
            state = GESTURE_TYPE.SINGLE
        else
            self:_EndZoomGesture()
            return
        end
    end

    if state == GESTURE_TYPE.SINGLE then
        if touchCount == 1 then
            local touch = CS.UnityEngine.Input.GetTouch(0)
            local touchLocal = self:_ScreenToViewportLocal(Vector2(touch.position.x, touch.position.y))
            if self.m_zoomSingleLastLocal then
                self.view.content.anchoredPosition = self.view.content.anchoredPosition +
                    Vector2(touchLocal.x - self.m_zoomSingleLastLocal.x, touchLocal.y - self.m_zoomSingleLastLocal.y)
                self:_ClampZoomContent()
            end
            self.m_zoomSingleLastLocal = { x = touchLocal.x, y = touchLocal.y }
        elseif touchCount == 0 then
            self:_EndZoomGesture()
        else

            self.m_zoomSingleLastLocal = nil
        end
    end
end


WikiCraftingTreeCtrl._EndZoomGesture = HL.Method() << function(self)
    self.m_zoomGestureState = GESTURE_TYPE.NORMAL
    self.m_zoomLastLocal = nil
    self.m_zoomSingleLastLocal = nil
    if self.view and self.view.scrollView then
        self.view.scrollView.enabled = true
        self.view.scrollView:StopMovement()
        self:_ClampZoomContent()
    end
end





WikiCraftingTreeCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self:_InitControllerZoom()
end

WikiCraftingTreeCtrl._InitControllerZoom = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.m_zoomBindingGroupId = InputManagerInst:CreateGroup(self.view.inputGroup.groupId)

    self:BindInputPlayerAction("wiki_craftingtree_zoom_in_press", function()
        LuaUpdate:Remove(self.m_zoomInTickKey)
        self.m_zoomInTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
            self:_OnZoom(-deltaTime * CONTROLLER_ZOOM_SPEED)
        end)
    end, self.m_zoomBindingGroupId)
    self:BindInputPlayerAction("wiki_craftingtree_zoom_in_release", function()
        LuaUpdate:Remove(self.m_zoomInTickKey)
        self.m_zoomInTickKey = -1
    end, self.m_zoomBindingGroupId)
    self:BindInputPlayerAction("wiki_craftingtree_zoom_out_press", function()
        LuaUpdate:Remove(self.m_zoomOutTickKey)
        self.m_zoomOutTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
            self:_OnZoom(deltaTime * CONTROLLER_ZOOM_SPEED)
        end)
    end, self.m_zoomBindingGroupId)
    self:BindInputPlayerAction("wiki_craftingtree_zoom_out_release", function()
        LuaUpdate:Remove(self.m_zoomOutTickKey)
        self.m_zoomOutTickKey = -1
    end, self.m_zoomBindingGroupId)
end


WikiCraftingTreeCtrl._TrySetZoomRatioInfo = HL.Method(HL.Number) << function(self, newScale)
    self:_CloseZoomRatioTimer()

    self.view.zoomRatioNode.gameObject:SetActive(true)
    self.m_zoomRatioTimerId = self:_StartTimer(self.view.config.ZOOM_RATIO_TIP_HIDE_DELAY, function()
        self.view.zoomRatioNode.gameObject:SetActive(false)
        self.m_zoomRatioTimerId = self:_ClearTimer(self.m_zoomRatioTimerId)
    end)

    self.view.zoomRatioTxt.text = string.format(Language.LUA_WIKI_ZOOM_RATIO, math.floor(newScale / self.view.config.ZOOM_MAX * 100))
end

WikiCraftingTreeCtrl._CloseZoomRatioTimer = HL.Method() << function(self)
    if self.m_zoomRatioTimerId > 0 then
        self.m_zoomRatioTimerId = self:_ClearTimer(self.m_zoomRatioTimerId)
    end
end

WikiCraftingTreeCtrl._SetBottomRelativeInputGroup = HL.Method(HL.Userdata) << function(self, inputGroup)
    InputManagerInst:ChangeParent(true, inputGroup.groupId, self.view.bottom.inputBindingGroupMonoTarget.groupId)
end

WikiCraftingTreeCtrl._ChangeZoomInputGroup = HL.Method(HL.Boolean) << function(self, bindRoot)
    if not DeviceInfo.usingController then
        return
    end

    if self.m_zoomBindingGroupId < 0 then
        return
    end

    if bindRoot then
        InputManagerInst:ChangeParent(true, self.m_zoomBindingGroupId, self.view.inputGroup.groupId)
    else
        InputManagerInst:ChangeParent(true, self.m_zoomBindingGroupId, self.view.bottom.inputBindingGroupMonoTarget.groupId)
    end
end





WikiCraftingTreeCtrl._PlayBgDecoAnim = HL.Method(HL.Boolean) << function(self, isIn)
    if self.m_phase then
        self.m_phase:PlayDecoAnim(isIn and "wiki_uideco_craft_in" or "wiki_uideco_craft_out")
        self.m_phase:PlayBgAnim(isIn and "wiki_plane_tocraft_in" or "wiki_plane_tocraft_out")
    end
end



HL.Commit(WikiCraftingTreeCtrl)
