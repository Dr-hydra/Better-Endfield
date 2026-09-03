local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacTechTree
local PHASE_ID = PhaseId.FacTechTree
local FAC_TECH_POINT_LACK_COLOR = "D25F69"

local SidebarType = {
    NodeDetails = 1,
    BlackboxList = 2,
}

local UnhiddenClipName = {
    CategoryTab = "factechtree_categorytab_unhidden",
    CategoryBg = "factechtree_unhiddencategorybg_unhidden",
    TechNode = "factechtree_treenode_unhidden",
    TechLine = "factechtree_line_unhidden",
}

FacTechTreeCtrl = HL.Class('FacTechTreeCtrl', uiCtrl.UICtrl)







FacTechTreeCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.FAC_ON_REFRESH_TECH_TREE_UI] = 'OnRefreshUI',
    [MessageConst.FOCUS_TECH_TREE_NODE] = 'FocusTechTreeNode',
    [MessageConst.FOCUS_TECH_TREE_CATEGORY] = 'FocusTechTreeCategory',
    [MessageConst.FOCUS_TECH_TREE_LAYER] = 'FocusTechTreeLayer',
    [MessageConst.ZOOM_TO_FULL_TECH_TREE] = 'ZoomToFullTechTree',

    [MessageConst.FAC_ON_UNLOCK_TECH_TREE_UI] = 'OnUnlockNode',
    [MessageConst.FAC_ON_UNLOCK_TECH_TIER_UI] = 'OnUnlockTier',

    [MessageConst.FAC_ON_UNLOCK_STT_LAYERS_BEGIN] = 'OnUnlockSttLayersBegin',
    [MessageConst.FAC_ON_UNLOCK_STT_LAYERS_END] = 'OnUnlockSttLayersEnd',

    [MessageConst.ON_SUB_GAME_ACTIVE] = "OnSubGameActive",
}

local facSTTGroupTable = Tables.facSTTGroupTable
local facSTTLayerTable = Tables.facSTTLayerTable
local facSTTNodeTable = Tables.facSTTNodeTable
local facSTTCategoryTable = Tables.facSTTCategoryTable

FacTechTreeCtrl.m_nodeCells = HL.Field(HL.Forward("UIListCache"))

FacTechTreeCtrl.m_lineCells = HL.Field(HL.Forward("UIListCache"))

FacTechTreeCtrl.m_layerCells = HL.Field(HL.Forward("UIListCache"))

FacTechTreeCtrl.m_categoryTabCells = HL.Field(HL.Forward("UIListCache"))

FacTechTreeCtrl.m_categoryLineCells = HL.Field(HL.Forward("UIListCache"))

FacTechTreeCtrl.m_targetCells = HL.Field(HL.Forward("UIListCache"))

FacTechTreeCtrl.m_unhiddenCategoryBgCells = HL.Field(HL.Forward("UIListCache"))

FacTechTreeCtrl.m_normalLayerCutLineCells = HL.Field(HL.Forward("UIListCache"))

FacTechTreeCtrl.m_getRewardCell = HL.Field(HL.Function)

FacTechTreeCtrl.m_curSelectNode = HL.Field(HL.Any)

FacTechTreeCtrl.m_lineList = HL.Field(HL.Table)

FacTechTreeCtrl.m_rewardList = HL.Field(HL.Table)

FacTechTreeCtrl.m_recommendTechId = HL.Field(HL.String) << ""

FacTechTreeCtrl.m_popupArgs = HL.Field(HL.Table)

FacTechTreeCtrl.m_popupUIState = HL.Field(HL.Number) << 0

FacTechTreeCtrl.m_showSidebar = HL.Field(HL.Boolean) << false

FacTechTreeCtrl.m_isFocus = HL.Field(HL.Boolean) << false

FacTechTreeCtrl.m_packageId = HL.Field(HL.String) << ""

FacTechTreeCtrl.m_followTick = HL.Field(HL.Number) << -1

FacTechTreeCtrl.m_lastScale = HL.Field(HL.Number) << -1

FacTechTreeCtrl.m_getConsumeItemCell = HL.Field(HL.Function)

FacTechTreeCtrl.m_consumeItems = HL.Field(HL.Table)

FacTechTreeCtrl.m_blackboxCellCache = HL.Field(HL.Forward("UIListCache"))

FacTechTreeCtrl.m_getBlackboxCellFunc = HL.Field(HL.Function)

FacTechTreeCtrl.m_allBlackboxIds = HL.Field(HL.Table)

FacTechTreeCtrl.m_showBlackboxAvailableTimer = HL.Field(HL.Number) << -1

FacTechTreeCtrl.m_showUnhiddenCategoryQueue = HL.Field(HL.Table)

FacTechTreeCtrl.m_showUnhiddenCategoryIndex = HL.Field(HL.Number) << 0

FacTechTreeCtrl.m_showUnhiddenTechQueue = HL.Field(HL.Table)

FacTechTreeCtrl.m_showUnhiddenTechIndex = HL.Field(HL.Number) << 0

FacTechTreeCtrl.m_curSelectTechId = HL.Field(HL.String) << ""

FacTechTreeCtrl.m_isAllOpenProgressFinished = HL.Field(HL.Boolean) << false

FacTechTreeCtrl.m_techId2CellLuaIndex = HL.Field(HL.Table)

FacTechTreeCtrl.m_techId2LineCellLuaIndex = HL.Field(HL.Table)

FacTechTreeCtrl.m_unhiddenShowCor = HL.Field(HL.Thread)

FacTechTreeCtrl.m_openUnlockTierBindingKey = HL.Field(HL.Number) << -1

FacTechTreeCtrl.m_lockedLayerIds = HL.Field(HL.Table)

FacTechTreeCtrl.m_curFocusNode = HL.Field(HL.Any)

FacTechTreeCtrl.m_needOpenLayerId = HL.Field(HL.String) << ""

FacTechTreeCtrl.m_externalFocusNode = HL.Field(HL.Boolean) << false

FacTechTreeCtrl.m_externalFocusId = HL.Field(HL.String) << ""

FacTechTreeCtrl.m_activeGamesWhenUnlockLayer = HL.Field(HL.Table)

FacTechTreeCtrl.m_isRelativeBlackboxNodeOpened = HL.Field(HL.Boolean) << false

FacTechTreeCtrl.m_needRecoverRelativeBlackboxNode = HL.Field(HL.Boolean) << false


FacTechTreeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:BindInputPlayerAction("fac_open_tech_tree", function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.m_curSelectTechId = arg.techId or ""
    self.m_needOpenLayerId = arg.layerId or ""
    self.m_packageId = arg.packageId
    self.m_needRecoverRelativeBlackboxNode = arg.recoverRelativeBlackboxNode == true

    self.m_nodeCells = UIUtils.genCellCache(self.view.nodeCell)
    self.m_lineCells = UIUtils.genCellCache(self.view.lineCell)
    self.m_layerCells = UIUtils.genCellCache(self.view.layerCell)
    self.m_categoryTabCells = UIUtils.genCellCache(self.view.categoryTabCell)
    self.m_categoryLineCells = UIUtils.genCellCache(self.view.facTechTreeCategoryLineCell)
    self.m_unhiddenCategoryBgCells = UIUtils.genCellCache(self.view.unhiddenCategoryBgCell)
    self.m_normalLayerCutLineCells = UIUtils.genCellCache(self.view.normalCutLineCell)

    self.m_showUnhiddenCategoryQueue = {}
    self.m_showUnhiddenTechQueue = {}
    self.m_techId2CellLuaIndex = {}
    self.m_techId2LineCellLuaIndex = {}


    local unhiddenPackageCount = GameInstance.player.facTechTreeSystem:GetUnhiddenPackageCount()
    self.view.btnScene.gameObject:SetActiveIfNecessary(unhiddenPackageCount > 1)
    self.view.btnScene.onClick:AddListener(function()
        self:Notify(MessageConst.FAC_TECH_TREE_OPEN_PACKAGE_PANEL, {self.m_packageId})
    end)

    self.view.btnBlackbox.onClick:AddListener(function()
        self:_OnBtnBlackboxClick()
    end)

    self.view.mask.gameObject:SetActiveIfNecessary(true)
    self.view.bigRectHelper.OnOpenTweenFinished:AddListener(function()
        self:_OnOpenTweenFinished()
    end)

    self:_InitInfo()


    self:_UpdateRecommendNode()
    self:_BuildPanel()
    self:_UpdateUnlockLayerIds()

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.containerNode)
    self.view.bigRectHelper:Init()

    self.view.touchPanelBtn.onClick:AddListener(function()
        self:_CloseSidebar()
    end)

    self.view.sidebar.gameObject:SetActiveIfNecessary(false)
    self.view.blackboxRedDot:InitRedDot("BlackboxEntry", self.m_packageId)


    local packageCfg = Tables.facSTTGroupTable[self.m_packageId]
    local detailNode = self.view.sidebar.facTechNodeDetail
    detailNode.nodeDetailReturnBtn.onClick:AddListener(function()
        self:_CloseSidebar()
    end)
    detailNode.relativeBtn.onClick:AddListener(function()
        self:_OnRelativeBtnClick()
    end)
    detailNode.packUpBtn.onClick:AddListener(function()
        self:_OnPackUpBtnClick()
    end)

    self.m_targetCells = UIUtils.genCellCache(detailNode.targetCell)
    self.m_getRewardCell = UIUtils.genCachedCellFunction(detailNode.rewardList)
    self.m_getConsumeItemCell = UIUtils.genCachedCellFunction(detailNode.consumeList)
    self.m_blackboxCellCache = UIUtils.genCellCache(detailNode.blackboxCell)
    detailNode.rewardList.onUpdateCell:AddListener(function(object, csIndex)
        local cell = self.m_getRewardCell(object)
        self:_OnUpdateRewardsCell(cell, LuaIndex(csIndex))
    end)
    detailNode.consumeList.onUpdateCell:AddListener(function(go, csIndex)
        local cell = self.m_getConsumeItemCell(go)
        self:_OnUpdateConsumeCell(cell, LuaIndex(csIndex))
    end)
    detailNode.techPointBtn.onClick:AddListener(function()
        self:_OnCostPointClick()
    end)
    detailNode.costPointBg.onClick:AddListener(function()
        self:_OnCostPointClick()
    end)
    FactoryUtils.updateFacTechTreeTechPointNode(detailNode.resourceNode, self.m_packageId)
    FactoryUtils.updateFacTechTreeTechPointNode(self.view.resourceNode, self.m_packageId)


    local blackboxOverview = self.view.sidebar.blackboxOverview

    self.m_allBlackboxIds = FactoryUtils.getBlackboxInfoTbl(packageCfg.blackboxIds, false)
    self.m_getBlackboxCellFunc = UIUtils.genCachedCellFunction(blackboxOverview.blackboxScrollList)

    blackboxOverview.blackboxOverviewReturnBtn.onClick:AddListener(function()
        self:_CloseSidebar()
    end)
    blackboxOverview.blackboxScrollList.onUpdateCell:AddListener(function(gameObject, csIndex)
        local cell = self.m_getBlackboxCellFunc(gameObject)
        local info = self.m_allBlackboxIds[LuaIndex(csIndex)]
        self:_OnUpdateBlackboxCell(cell, info.blackboxId)
    end)
    blackboxOverview.filterBtn.onClick:AddListener(function()
        local args = self:_GenFilterArgs()
        self:Notify(MessageConst.SHOW_COMMON_FILTER, args)
    end)

    FactoryUtils.updateFacTechTreeTechPointNode(blackboxOverview.resourceNode, self.m_packageId)

    self:_InitController()
end

FacTechTreeCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.CLOSE_TECH_TREE_POP_UP)

    if self.m_followTick > 0 then
        LuaUpdate:Remove(self.m_followTick)
        self.m_followTick = -1
    end

    if self.m_showBlackboxAvailableTimer > 0 then
        self.m_showBlackboxAvailableTimer = self:_ClearTimer(self.m_showBlackboxAvailableTimer)
    end

    if self.m_unhiddenShowCor then
        self.m_unhiddenShowCor = self:_ClearCoroutine(self.m_unhiddenShowCor)
    end
end

FacTechTreeCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    self.view.screenZoomNode.gameObject:SetActive(active)
end

FacTechTreeCtrl.OnRefreshUI = HL.Method() << function(self)
    if not self.m_showSidebar then
        return
    end

    self:_RefreshNodeDetail()
end

FacTechTreeCtrl.OnSubGameActive = HL.Method(HL.Table) << function(self, args)


    if self.m_activeGamesWhenUnlockLayer then
        local gameId = unpack(args)
        table.insert(self.m_activeGamesWhenUnlockLayer, gameId)
    end
end

FacTechTreeCtrl.OnUnlockTier = HL.Method(HL.Table) << function(self, args)
    self:_UpdateUnlockLayerIds()

    AudioAdapter.PostEvent("Au_UI_Event_FacTechTree_Unlock")
    local layerId = unpack(args)

    self.m_layerCells:Update(function(cell, _)
        cell:Refresh()
    end)

    self.m_categoryLineCells:Update(function(cell, _)
        cell:Refresh()
    end)

    self:_RefreshLine(false)

    local length = self.m_layerCells:Get(1):GetUnlockClipLength()
    self:_UpdateRecommendNode()
    self:_StartTimer(length, function()
        self.m_nodeCells:Update(function(cell, _)
            cell:Refresh(cell.techId == self.m_recommendTechId)
        end)
    end)
end

FacTechTreeCtrl.OnUnlockSttLayersBegin = HL.Method() << function(self)
    self.m_activeGamesWhenUnlockLayer = {}
end

FacTechTreeCtrl.OnUnlockSttLayersEnd = HL.Method() << function(self)

    if #self.m_activeGamesWhenUnlockLayer > 0 then
        self:_ShowBlackboxAvailable()
    end

    self.m_activeGamesWhenUnlockLayer = nil
end

FacTechTreeCtrl._ShowBlackboxAvailable = HL.Method() << function(self)
    self.view.blackBoxAddNode.gameObject:SetActiveIfNecessary(true)
    self.m_showBlackboxAvailableTimer = self:_StartTimer(self.view.config.SHOW_BLACKBOX_AVAILABLE_TIME, function()
        self.view.blackBoxAddNode.gameObject:SetActiveIfNecessary(false)
    end)
end

FacTechTreeCtrl.OnUnlockNode = HL.Method(HL.Table) << function(self, args)
    local techTreeSystem = GameInstance.player.facTechTreeSystem
    local curTechId = unpack(args)

    self:_UpdateRecommendNode()
    self:_RefreshLine(false)
    self:_CloseSidebar()

    FactoryUtils.updateFacTechTreeTechPointCount(self.view.resourceNode, self.m_packageId)
    FactoryUtils.updateFacTechTreeTechPointCount(self.view.sidebar.facTechNodeDetail.resourceNode, self.m_packageId)

    local unlockItems = {}
    local rewardsItems = {}
    local buildingInfo = {}
    local techId = curTechId
    local techData = facSTTNodeTable:GetValue(techId)
    for _, rewardData in pairs(techData.unlockReward) do
        if rewardData.count <= 0 then
            table.insert(unlockItems, rewardData.itemId)
        else
            table.insert(rewardsItems, { id = rewardData.itemId, count = rewardData.count })
        end
    end
    buildingInfo.buildingId = techTreeSystem:GetBuildingName(techId)
    buildingInfo.level = techTreeSystem:GetBuildingLevel(techId)
    local args = {
        techId = techId,
        unlockItems = unlockItems,
        rewardsItems = rewardsItems,
        buildingInfo = buildingInfo,
        onHideCb = function()
            self.m_nodeCells:Update(function(cell, _)
                cell:Refresh(cell.techId == self.m_recommendTechId)
            end)
        end,
    }
    self.m_popupArgs = args
    self.m_popupUIState = 0
    self:_ShowUnlock()

    CS.Beyond.Gameplay.Audio.AudioRemoteFactoryAnnouncement.Announcement("au_fac_announcement_techtree_unlock")
end

FacTechTreeCtrl.OnRefreshNodeName = HL.Method(HL.Boolean) << function(self, show)
    self.m_nodeCells:Update(function(cell, _)
        cell:OnShowNameStateChange(show)
    end)
end

FacTechTreeCtrl.FocusTechTreeNode = HL.Method(HL.Table) << function(self, args)
    local techId = unpack(args)
    local succ, techCfg = Tables.facSTTNodeTable:TryGetValue(techId)
    if not succ then
        return
    end

    if not self.m_isAllOpenProgressFinished then
        self.m_externalFocusId = techId
        return
    end

    local luaIndex = self.m_techId2CellLuaIndex[techId]
    local nodeCell = self.m_nodeCells:Get(luaIndex)
    self:_OnClickNode(nodeCell, false)
end

FacTechTreeCtrl.FocusTechTreeCategory = HL.Method(HL.Table) << function(self, args)
    local categoryId = unpack(args)
    local succ, categoryCfg = Tables.facSTTCategoryTable:TryGetValue(categoryId)
    if not succ then
        return
    end

    if not self.m_isAllOpenProgressFinished then
        self.m_externalFocusId = categoryId
        return
    end

    local categoryTab = self.m_categoryTabCells:Get(categoryCfg.order)



    self.view.bigRectHelper:FocusNode(categoryTab.rectTransform)
end

FacTechTreeCtrl.FocusTechTreeLayer = HL.Method(HL.Table) << function(self, args)
    local layerId = unpack(args)
    local succ, layerCfg = Tables.facSTTLayerTable:TryGetValue(layerId)
    if not succ then
        return
    end

    if not self.m_isAllOpenProgressFinished then
        self.m_externalFocusId = layerId
        return
    end

    local layerCell = self.m_layerCells:Get(layerCfg.order)



    self.view.bigRectHelper:FocusNode(layerCell.view.craft)
end

FacTechTreeCtrl.ZoomToFullTechTree = HL.Method() << function(self)
    self.view.bigRectHelper:ZoomToFullRect(function()  end)
end

FacTechTreeCtrl.AutoSelect = HL.Method(HL.Opt(HL.String)) << function(self, techId)
    if string.isEmpty(techId) then
        return
    end

    local luaIndex = self.m_techId2CellLuaIndex[techId]
    local nodeCell = self.m_nodeCells:Get(luaIndex)
    self:_OnClickNode(nodeCell)
end

FacTechTreeCtrl.GetCurSelectNode = HL.Method().Return(HL.Any) << function(self)
    return self.m_curSelectNode
end

FacTechTreeCtrl.GetCurPackageId = HL.Method().Return(HL.Any) << function(self)
    return self.m_packageId
end

FacTechTreeCtrl.GetIsRelativeBlackboxNodeOpened = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_isRelativeBlackboxNodeOpened
end

FacTechTreeCtrl._InitInfo = HL.Method() << function(self)
    local packageCfg = Tables.facSTTGroupTable[self.m_packageId]
    local domainId = packageCfg.domainId
    local domainCfg = Tables.domainDataTable[domainId]

    self.view.domainIcon:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT, domainCfg.domainIcon)
    self.view.nameText:SetAndResolveTextStyle(packageCfg.groupName)
end

FacTechTreeCtrl._UpdateUnlockLayerIds = HL.Method() << function(self)
    self.m_lockedLayerIds = {}
    local packageCfg = Tables.facSTTGroupTable[self.m_packageId]
    local system = GameInstance.player.facTechTreeSystem
    for i = 0, packageCfg.layerIds.Count - 1 do
        local layerId = packageCfg.layerIds[i]
        local layerCfg = Tables.facSTTLayerTable[layerId]
        if not layerCfg.isTBD and system:LayerIsLocked(layerId) then
            table.insert(self.m_lockedLayerIds, layerId)
        end
    end

    if DeviceInfo.usingController then
        if self.m_openUnlockTierBindingKey ~= -1 and #self.m_lockedLayerIds == 0 then
            self.m_openUnlockTierBindingKey = self:DeleteInputBinding(self.m_openUnlockTierBindingKey)
        elseif #self.m_lockedLayerIds > 0 and self.m_openUnlockTierBindingKey == -1 then
            self.m_openUnlockTierBindingKey = self:BindInputPlayerAction("tech_tree_open_unlock_tier", function()
                self:_OnClickLayer(self.m_lockedLayerIds[1])
            end, self.view.titleNode.groupId)
        end
    end
end

FacTechTreeCtrl._BuildPanel = HL.Method() << function(self)
    local system = GameInstance.player.facTechTreeSystem

    local marginSize = self.view.notchAdapter.selfMarginSize
    local marginX = marginSize.x

    local cellWidth = self.view.nodeCell.rectTransform.rect.width
    local cellHeight = self.view.nodeCell.rectTransform.rect.height

    local packageCfg = facSTTGroupTable[self.m_packageId]

    local categoryPadding = packageCfg.categoryPadding

    local layerPadding = packageCfg.layerPadding

    local X_DIS = cellWidth * (1 + packageCfg.internalSpacingX)

    local Y_DIS = cellHeight * (1 + packageCfg.internalSpacingY)
    local X_ORI = self.view.config.X_ORI
    local Y_ORI = self.view.config.Y_ORI
    local LINE_WEIGHT = self.view.config.LINE_WIDTH


    local nodeList = {}
    local lineList = {}
    local layerList = {}

    local categoryList = {}

    local categoryLineList = {}
    local unhiddenCategoryList = {}
    self.m_lineList = lineList





    local order2CategoryVOLuaIndex = {}
    local maxX = 0
    for _, categoryId in pairs(packageCfg.categoryIds) do
        local categoryCfg = facSTTCategoryTable[categoryId]

        if not system:CategoryIsHidden(categoryId) then
            local maxPosX = math.mininteger
            for _, techId in pairs(categoryCfg.techIds) do

                if not system:NodeIsHidden(techId) then
                    local techCfg = facSTTNodeTable[techId]
                    maxPosX = math.max(maxPosX, techCfg.uiPos[0])
                end
            end
            local containsXCount = maxPosX - categoryCfg.startPosX + 1
            local categoryCfgSizeX = (containsXCount - 1) * X_DIS + (categoryPadding * 2 + 1) * cellWidth
            local isUnhiddenCategoryRead = system:IsUnhiddenCategoryRead(categoryId)
            local isFirstUnhidden = categoryCfg.defaultHidden and not isUnhiddenCategoryRead
            table.insert(categoryList, {
                categoryId = categoryId,
                name = categoryCfg.name,
                order = categoryCfg.order,
                containsXCount = containsXCount,
                sizeX = categoryCfgSizeX,
                isFirstUnhidden = isFirstUnhidden,
            })

            local luaIndex = #categoryList
            if categoryCfg.defaultHidden then
                table.insert(unhiddenCategoryList, {
                    categoryId = categoryId,
                    posX = marginX + X_ORI + maxX,
                    sizeX = categoryCfgSizeX,
                    isFirstUnhidden = isFirstUnhidden,
                    unhiddenBgColor = categoryCfg.unhiddenBgColor,
                })

                if not isUnhiddenCategoryRead then

                    table.insert(self.m_showUnhiddenCategoryQueue, {
                        categoryId = categoryId,
                        relativeUnhiddenCategoryListIndex = #unhiddenCategoryList,
                        order = luaIndex,
                    })
                end
            end

            order2CategoryVOLuaIndex[categoryCfg.order] = luaIndex
            maxX = maxX + categoryCfgSizeX
        end
    end
    table.sort(categoryList, Utils.genSortFunction({ "order" }, true))


    self.m_categoryTabCells:Refresh(#categoryList, function(cell, index)
        local categoryVO = categoryList[index]
        categoryVO.categoryTabCell = cell
        cell.gameObject.name = "CategoryTab-"..categoryVO.categoryId
        cell.categoryText.text = categoryVO.name
        cell.rectTransform.sizeDelta = Vector2(categoryVO.sizeX, cell.rectTransform.sizeDelta.y)
        cell.gameObject:SetActiveIfNecessary(not categoryVO.isFirstUnhidden)
    end)

    local unhiddenBgHeight = 0

    for _, layerId in pairs(packageCfg.layerIds) do
        local layerCfg = facSTTLayerTable[layerId]
        local maxPosY = math.mininteger
        local hasTechIds = layerCfg.techIds.Count > 0
        for _, techId in pairs(layerCfg.techIds) do
            local techCfg = facSTTNodeTable[techId]

            if not system:NodeIsHidden(techId) then
                maxPosY = math.max(maxPosY, techCfg.uiPos[1])
            end
        end
        local containsYCount = hasTechIds and maxPosY - layerCfg.startPosY + 1 or layerCfg.containsTechCountIfTBD
        local sizeY = (containsYCount - 1) * Y_DIS + (layerPadding * 2 + 1) * cellHeight
        if not layerCfg.isTBD then
            unhiddenBgHeight = unhiddenBgHeight + sizeY
        end
        table.insert(layerList, {
            layerId = layerId,
            name = layerCfg.name,
            order = layerCfg.order,
            containsYCount = containsYCount,
            sizeY = sizeY,
            isTBD = layerCfg.isTBD,
        })
    end
    table.sort(layerList, Utils.genSortFunction({ "order" }, true))

    self.m_layerCells:Refresh(#layerList, function(cell, index)
        local layerVO = layerList[index]
        layerVO.layerCell = cell
        cell:InitFacTechTreeLayerCell(layerVO.layerId, maxX + X_ORI, layerVO.sizeY, marginX,
                                      function()
                                          self:_OnClickLayer(layerList[index].layerId)
                                      end)
    end)


    local x = self.view.normalCutLineCell.rectTransform.anchoredPosition.x
    local y = 0
    self.m_normalLayerCutLineCells:Refresh(#layerList, function(cell, index)
        local layerVO = layerList[index]
        cell.gameObject.name = "NormalCutLineCell-" .. layerVO.layerId
        cell.rectTransform.anchoredPosition = Vector2(x, y)
        y = y - layerVO.sizeY
    end)


    self.m_unhiddenCategoryBgCells:Refresh(#unhiddenCategoryList, function(cell, index)
        local unhiddenCategoryVO = unhiddenCategoryList[index]
        cell.gameObject.name = "UnhiddenCategoryBg-"..unhiddenCategoryVO.categoryId
        cell.rectTransform.anchoredPosition = Vector2(unhiddenCategoryVO.posX, Y_ORI)
        cell.rectTransform.sizeDelta = Vector2(unhiddenCategoryVO.sizeX, unhiddenBgHeight)
        cell.image.color = UIUtils.getColorByString(unhiddenCategoryVO.unhiddenBgColor)
        cell.gameObject:SetActiveIfNecessary(not unhiddenCategoryVO.isFirstUnhidden)
    end)
    self.view.unhiddenCategoryBgNode.gameObject:SetActiveIfNecessary(#unhiddenCategoryList > 0)


    local currentYOri = self.view.config.Y_ORI
    local currentXOri = self.view.config.X_ORI + marginX

    local accumulateHeight = 0
    for _, layer in ipairs(layerList) do

        local calcPosY = currentYOri - layer.sizeY / 2

        local accumulateWidth = 0
        for _, category in ipairs(categoryList) do
            local calcPosX = currentXOri + category.sizeX
            table.insert(categoryLineList, {
                layerId = layer.layerId,
                width = self.view.config.CATEGORY_LINE_WIDTH,
                height = layer.containsYCount * Y_DIS,
                posX = calcPosX,
                posY = calcPosY,
            })
            currentXOri = calcPosX

            category.accumulateWidth = accumulateWidth
            accumulateWidth = accumulateWidth + category.sizeX
        end
        currentXOri = self.view.config.X_ORI + marginX
        currentYOri = currentYOri - layer.sizeY

        layer.accumulateHeight = accumulateHeight
        accumulateHeight = accumulateHeight + layer.sizeY
    end


    self.m_categoryLineCells:Refresh(#categoryLineList, function(cell, index)
        local categoryLineVO = categoryLineList[index]
        categoryLineVO.categoryLineCell = cell
        cell:InitFacTechTreeCategoryLineCell(categoryLineVO)
    end)


    local calc = function(techCfg, isVertical)
        if isVertical then
            local layerCfg = facSTTLayerTable[techCfg.layer]
            local layer = layerList[layerCfg.order]
            return Y_ORI - layer.accumulateHeight - (techCfg.uiPos[1] - layerCfg.startPosY) * Y_DIS -
                    (layerPadding + 0.5) * cellHeight
        else
            local categoryCfg = facSTTCategoryTable[techCfg.category]
            local category = categoryList[order2CategoryVOLuaIndex[categoryCfg.order]]
            return marginX + X_ORI + category.accumulateWidth + (techCfg.uiPos[0] - categoryCfg.startPosX) * X_DIS +
                    (categoryPadding + 0.5) * cellWidth
        end
    end

    for _, techId in pairs(packageCfg.techIds) do

        if not system:NodeIsHidden(techId) then
            local techCfg = facSTTNodeTable[techId]

            local x = calc(techCfg, false)
            local y = calc(techCfg, true)











            local isFirstUnhidden = techCfg.defaultHidden and
                    not system:IsUnhiddenTechRead(techCfg.techId)
            if isFirstUnhidden then
                table.insert(self.m_showUnhiddenTechQueue, {
                    techId = techId,
                    x = x,
                    y = y,
                })
            end


            for _, preNodeId in pairs(techCfg.preNode) do
                if not string.isEmpty(preNodeId) then
                    local preNodeData = facSTTNodeTable:GetValue(preNodeId)
                    local upX = calc(preNodeData, false)
                    local upY = calc(preNodeData, true)
                    table.insert(lineList, {
                        techId = techId,
                        preNodeLayer = preNodeData.layer,
                        upX = upX,
                        upY = upY,
                        downX = x,
                        downY = y,
                        lineWeight = LINE_WEIGHT,
                        yDis = Y_DIS,
                        isFirstUnhidden = isFirstUnhidden,
                    })
                end
            end

            table.insert(nodeList, {
                techId = techId,
                layer = techCfg.layer,
                x = x,
                y = y,
                sortY = -y,
                isFirstUnhidden = isFirstUnhidden,
            })
        end
    end


    self:_RefreshLine(true)

    table.sort(nodeList, Utils.genSortFunction({"x", "sortY"}, true))


    self.m_nodeCells:Refresh(#nodeList, function(cell, index)
        local nodeVO = nodeList[index]
        nodeVO.nodeCell = cell
        self.m_techId2CellLuaIndex[nodeVO.techId] = index
        cell:InitFacTechTreeNode(nodeVO, nodeVO.techId == self.m_recommendTechId,
                                 function()
                                     self:_OnClickNode(cell, true)
                                 end,
                                 function(isTarget)
                                     self:_OnIsNaviTargetChangedNode(cell, isTarget)
                                 end)
        cell:OnSelect(false)
        cell.gameObject:SetActiveIfNecessary(not nodeVO.isFirstUnhidden)
    end)

    self.view.bigRectHelper.zoomEvent:AddListener(function(csIndex, isLarger)
        if csIndex == 0 then
            self:OnRefreshNodeName(isLarger)
        end
    end)

    self.m_followTick = LuaUpdate:Add("TailTick", function()

        local targetPos = self.view.categoryPivot.position
        local followerPos = self.view.directory.position
        self.view.directory.position = Vector3(targetPos.x, followerPos.y, followerPos.z)


        local scale = self.view.containerNode.localScale.x
        if math.abs(self.m_lastScale - scale) > 0.001 then

            local newPaddingLeft = lume.round((self.view.config.X_ORI + marginX) * scale)
            self.view.directoryHorizontalLayoutGroup.padding.left = newPaddingLeft
            for _, category in ipairs(categoryList) do
                local sizeDelta = category.categoryTabCell.rectTransform.sizeDelta
                category.categoryTabCell.rectTransform.sizeDelta = Vector2(category.sizeX * scale, sizeDelta.y)
            end
            LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.directory)

            self.m_lastScale = scale
        end
    end)
end

FacTechTreeCtrl._UpdateRecommendNode = HL.Method() << function(self)
    local recommendTechId = ""
    local minSort = math.maxinteger
    local techTreeSystem = GameInstance.player.facTechTreeSystem

    local packageCfg = facSTTGroupTable[self.m_packageId]
    for _, techId in pairs(packageCfg.techIds) do
        if techTreeSystem:NodeIsHidden(techId) then
            goto continue
        end

        if not techTreeSystem:NodeIsLocked(techId) then
            goto continue
        end

        local techCfg = facSTTNodeTable[techId]
        if techTreeSystem:LayerIsLocked(techCfg.layer) then
            goto continue
        end

        if techTreeSystem:CategoryIsHidden(techCfg.category) then
            goto continue
        end

        if techCfg.preNode.Count > 0 and techTreeSystem:PreNodeIsLocked(techId) then
            goto continue
        end

        if techCfg.sortId > minSort then
            goto continue
        end

        minSort = techCfg.sortId
        recommendTechId = techId

        ::continue::
    end

    self.m_recommendTechId = recommendTechId
end

FacTechTreeCtrl._RefreshLine = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    local lineList = self.m_lineList
    local techTreeSystem = GameInstance.player.facTechTreeSystem
    for _, line in ipairs(lineList) do
        if techTreeSystem:NodeIsLocked(line.techId) then
            line.lineOrder = 0
        else
            line.lineOrder = 1
        end
    end


    table.sort(lineList, Utils.genSortFunction({ "lineOrder" }, true))


    self.m_lineCells:Refresh(#lineList, function(cell, index)
        local lineVO = lineList[index]
        self.m_techId2LineCellLuaIndex[lineVO.techId] = index
        cell:InitFacTechTreeLineCell(lineVO)
        if isInit then
            cell.gameObject:SetActiveIfNecessary(not lineVO.isFirstUnhidden)
        end
    end)
end

FacTechTreeCtrl._OnClickLayer = HL.Method(HL.String) << function(self, layerId)
    local isLocked = GameInstance.player.facTechTreeSystem:LayerIsLocked(layerId)
    if not isLocked or string.isEmpty(Tables.facSTTLayerTable[layerId].preLayer) then
        return
    end

    UIManager:Open(PanelId.FacTechTreeUnlockTierPopup, { layerId = layerId, lockedLayerIds = self.m_lockedLayerIds })
end

FacTechTreeCtrl._OnClickNode = HL.Method(HL.Any, HL.Opt(HL.Boolean)) << function(self, node, playSound)
    self:_ToggleSideBarForController(true)

    local lastNode = self.m_curSelectNode
    if lastNode ~= nil and lastNode ~= node then
        lastNode:OnSelect(false)
    end
    self.m_curSelectNode = node
    node:OnSelect(true)
    self:_OpenNodeDetail()
    self:_RefreshNodeDetail()
    self:_FocusTechNode(self.m_curSelectNode.transform, true)
    if playSound then
        AudioManager.PostEvent("au_ui_btn_techtree")
    end
end

FacTechTreeCtrl._RefreshNodeDetail = HL.Method() << function(self)
    local detailNode = self.view.sidebar.facTechNodeDetail
    local techId = self.m_curSelectNode.techId
    local nodeData = facSTTNodeTable:GetValue(techId)


    detailNode.techNameTxt.text = nodeData.name
    detailNode.techIcon:LoadSprite(UIConst.UI_SPRITE_FAC_TECH_ICON, nodeData.icon)
    detailNode.desc:SetAndResolveTextStyle(nodeData.desc)


    local techTreeSystem = GameInstance.player.facTechTreeSystem
    local isLocked = techTreeSystem:NodeIsLocked(techId)
    local conditions = nodeData.conditions
    local showConditionNode = conditions.Count > 0 and isLocked
    detailNode.conditionNode.gameObject:SetActiveIfNecessary(showConditionNode)
    if showConditionNode then
        self.m_targetCells:Refresh(nodeData.conditions.Count, function(item, index)
            self:_RefreshConditionCell(item, index)
        end)
    end


    local costItems = nodeData.costItems
    local hasCost = costItems.Count > 0
    local consumeList = {}
    detailNode.consumeNode.gameObject:SetActiveIfNecessary(hasCost)
    for _, itemBundle in pairs(costItems) do
        local consumeItem = {}
        consumeItem.id = itemBundle.id
        consumeItem.count = itemBundle.count
        consumeItem.ownCount = Utils.getItemCount(itemBundle.id)
        table.insert(consumeList, consumeItem)
    end
    self.m_consumeItems = consumeList
    detailNode.consumeList:UpdateCount(#consumeList)


    local rewardList = {}
    for i = 0, nodeData.unlockReward.Count - 1 do
        local rewardData = nodeData.unlockReward[i]
        local itemId = rewardData.itemId
        local count = rewardData.count
        if not string.isEmpty(itemId) then
            table.insert(rewardList, { itemId = itemId, count = count })
        end
    end
    self.m_rewardList = rewardList
    detailNode.rewardList:UpdateCount(#rewardList)



    detailNode.unlockInfoNode.gameObject:SetActiveIfNecessary(isLocked)
    if isLocked then
        local packageCfg = facSTTGroupTable[self.m_packageId]
        local costCount = nodeData.costPointCount
        local ownCount = Utils.getItemCount(packageCfg.costPointType)
        local techPointItemCfg = Tables.itemTable[packageCfg.costPointType]
        local countStr = string.format("%s/%s", ownCount, costCount)
        if costCount > ownCount then
            countStr = string.format(UIConst.COLOR_STRING_FORMAT, FAC_TECH_POINT_LACK_COLOR, countStr)
        end
        detailNode.unlockInfoTxt.text = countStr
        detailNode.techPointIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, techPointItemCfg.iconId)



    end


    self:_RefreshUnlockButton()


end

FacTechTreeCtrl._RefreshUnlockButton = HL.Method() << function(self)
    local detailNode = self.view.sidebar.facTechNodeDetail
    local techId = self.m_curSelectNode.techId
    local techTreeSystem = GameInstance.player.facTechTreeSystem
    local nodeData = facSTTNodeTable:GetValue(techId)
    local groupData = facSTTGroupTable:GetValue(nodeData.groupId)
    local techPointItemCfg = Tables.itemTable[groupData.costPointType]

    local locked = techTreeSystem:NodeIsLocked(techId)
    detailNode.finishNode.gameObject:SetActiveIfNecessary(not locked)
    detailNode.unlockBtn.gameObject:SetActiveIfNecessary(locked)
    detailNode.relativeBtn.gameObject:SetActiveIfNecessary(nodeData.blackboxIds.Count > 0)


    if locked then
        local isMatchCondition = true
        if nodeData.conditions.Count > 0 then
            for i = 1, nodeData.conditions.Count do
                if not techTreeSystem:GetConditionIsCompleted(techId, nodeData.conditions[CSIndex(i)].conditionId) then
                    isMatchCondition = false
                    break
                end
            end
        end

        local costItemEnough = true
        if nodeData.conditions.Count > 0 then
            for i = 1, nodeData.costItems.Count do
                local costItemBundle = nodeData.costItems[CSIndex(i)]
                if Utils.getItemCount(costItemBundle.id) < costItemBundle.count then
                    costItemEnough = false
                    break
                end
            end
        end

        local pointEnough = Utils.getItemCount(groupData.costPointType) >= nodeData.costPointCount

        detailNode.unlockBtn.onClick:RemoveAllListeners()
        detailNode.unlockBtn.onClick:AddListener(function()
            AudioManager.PostEvent("au_ui_fac_techtree_node_unlock")

            local canUnlock = false
            local conditionText
            if techTreeSystem:LayerIsLocked(nodeData.layer) then
                local layerData = facSTTLayerTable:GetValue(nodeData.layer)
                conditionText = string.format(Language.LUA_FAC_TECHTREE_FAILED_TOAST_3, layerData.name)
            elseif techTreeSystem:PreNodeIsLocked(techId) then
                conditionText = Language.LUA_FAC_TECHTREE_FAILED_TOAST_4
            elseif not isMatchCondition then
                conditionText = Language.LUA_FAC_TECHTREE_FAILED_TOAST_1
            elseif not pointEnough then

                conditionText = string.format(Language.LUA_FAC_TECHTREE_FAILED_TOAST_5, techPointItemCfg.name)
            elseif not costItemEnough then

                conditionText = Language.LUA_FAC_TECHTREE_FAILED_TOAST_2
            else
                canUnlock = true
            end

            if not canUnlock then
                self:Notify(MessageConst.SHOW_TOAST, conditionText)
            else
                if self.m_isFocus then
                    return
                end
                local techId = self.m_curSelectNode.techId
                GameInstance.player.facTechTreeSystem:SendUnlockNodeMsg(techId)
                Utils.reportPlacementEvent(GEnums.ClientPlacementEventType.TechFactoryUnlockedFirst)
            end
        end)
    end
end

FacTechTreeCtrl._OnUpdateRewardsCell = HL.Method(HL.Forward("Item"), HL.Number) << function(self, cell, luaIndex)
    local techId = self.m_curSelectNode.techId
    local techTreeSystem = GameInstance.player.facTechTreeSystem
    local rewardData = self.m_rewardList[luaIndex]
    local count = rewardData.count
    local itemId = rewardData.itemId
    if count > 0 then
        cell:InitItem({ id = itemId, count = count }, true)
    else
        cell:InitItem({ id = itemId }, true)
    end
    cell:SetExtraInfo({
                          tipsPosTransform = self.view.sidebar.facTechNodeDetail.transform,
                          tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                          isSideTips = DeviceInfo.usingController,
                      })
    cell.view.rewardedCover.gameObject:SetActiveIfNecessary(not techTreeSystem:NodeIsLocked(techId))
end

FacTechTreeCtrl._OnUpdateConsumeCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local consumeItemData = self.m_consumeItems[luaIndex]
    cell.ownTxt.text = consumeItemData.ownCount
    cell.item:InitItem({ id = consumeItemData.id, count = consumeItemData.count }, function()
        self:_OnClickShowRewardItemTips(consumeItemData.id)
    end)
end

FacTechTreeCtrl._OnCostPointClick = HL.Method() << function(self)
    local packageCfg = Tables.facSTTGroupTable[self.m_packageId]
    self:Notify(MessageConst.SHOW_ITEM_TIPS, {
        itemId = packageCfg.costPointType,
        transform = self.view.sidebar.facTechNodeDetail.techPointBtn.transform,
        posType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
        notPenetrate = true,
    })
end

FacTechTreeCtrl._OnClickShowRewardItemTips = HL.Method(HL.String) << function(self, itemId)
    self:Notify(MessageConst.SHOW_ITEM_TIPS, {
        itemId = itemId,
        transform = self.view.sidebar.facTechNodeDetail.transform,
        posType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
        notPenetrate = true,
        isSideTips = DeviceInfo.usingController,
    })
end

FacTechTreeCtrl._RefreshConditionCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local techId = self.m_curSelectNode.techId
    local nodeData = facSTTNodeTable:GetValue(techId)
    local techTreeSystem = GameInstance.player.facTechTreeSystem
    local conditions = nodeData.conditions
    local condition = conditions[CSIndex(index)]

    local isMissionCell = condition.conditionType == GEnums.ConditionType.MissionStateEqual
            or condition.conditionType == GEnums.ConditionType.QuestStateEqual
    local isQuestId = condition.conditionType == GEnums.ConditionType.QuestStateEqual

    local isComplete = techTreeSystem:GetConditionIsCompleted(techId, condition.conditionId)
    local missionStyle = isMissionCell and not isComplete
    cell.stateController:SetState(missionStyle and "GotoMission" or "Normal")
    if missionStyle then
        local id = condition.parameters[0].valueStringList[0]
        local missionId = isQuestId and GameInstance.player.mission:GetMissionIdByQuestId(id) or id
        cell.gotoMissionBtn.onClick:RemoveAllListeners()
        cell.gotoMissionBtn.onClick:AddListener(function()
            if GameInstance.player.mission:GetMissionState(missionId) ==
                    CS.Beyond.Gameplay.MissionSystem.MissionState.Processing then
                PhaseManager:OpenPhase(PhaseId.Mission, { autoSelect = missionId })
            else
                Notify(MessageConst.SHOW_TOAST, Language.LUA_TECH_TREE_TECH_MISSION_CONDITION_NOT_PROCESSING)
            end
        end)
        cell.missionDescTxt.text = condition.desc
    else
        local progress = techTreeSystem:GetConditionProgress(techId, condition.conditionId)
        local total = techTreeSystem:GetConditionTotalProgress(condition.conditionId)
        local progress = string.format("(%1$d/%2$d)", progress, total)
        cell.normalNode:SetState(isComplete and "Complete" or "Normal")
        cell.descTxt.text = condition.desc .. " " .. progress
    end
end

FacTechTreeCtrl._OnRelativeBtnClick = HL.Method() << function(self)
    local techId = self.m_curSelectNode.techId
    local nodeData = facSTTNodeTable:GetValue(techId)

    local relativeBlackboxes = FactoryUtils.getBlackboxInfoTbl(nodeData.blackboxIds, false)
    local showToast = false
    for _, relativeBlackbox in ipairs(relativeBlackboxes) do
        if not DungeonUtils.isDungeonActive(relativeBlackbox.blackboxId) then
            showToast = true
            break
        end
    end

    if showToast then
        local layerId = nodeData.layer
        local layerCfg = Tables.facSTTLayerTable[layerId]
        local hint = string.format(Language.LUA_TECH_TREE_JUMP_BLACKBOX_ENTRY_FAIL_FORMAT, layerCfg.name)
        Notify(MessageConst.SHOW_TOAST, hint)
        return
    end

    if #relativeBlackboxes > 0 then
        self.m_blackboxCellCache:Refresh(#relativeBlackboxes, function(cell, luaIndex)
            local info = relativeBlackboxes[luaIndex]
            self:_OnUpdateBlackboxCell(cell, info.blackboxId)
        end)
        self:_ToggleTechNodeRelativeBlackboxPanel(true)
    else
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_TECH_TREE_NO_RELATIVE_BLACKBOX_TOAST_DESC)
    end
end

FacTechTreeCtrl._ToggleTechNodeRelativeBlackboxPanel = HL.Method(HL.Boolean) << function(self, isOn)
    self.m_isRelativeBlackboxNodeOpened = isOn
    local detailNode = self.view.sidebar.facTechNodeDetail
    if isOn then
        detailNode.animationWrapper:PlayInAnimation()
    else
        detailNode.animationWrapper:PlayOutAnimation()
    end

    if DeviceInfo.usingController then
        if isOn then
            self.view.sidebar.facTechNodeDetail.relativeBlackboxNode:ManuallyFocus()
        else
            self.view.sidebar.facTechNodeDetail.relativeBlackboxNode:ManuallyStopFocus()
        end
    end
end

FacTechTreeCtrl._OnUpdateBlackboxCell = HL.Method(HL.Any, HL.String) << function(self, cell, blackboxId)
    FactoryUtils.updateBlackboxCell(cell, blackboxId, function()
        local isUnlock = DungeonUtils.isDungeonUnlock(blackboxId)

        if isUnlock then
            self:_ToggleTechNodeRelativeBlackboxPanel(false)
            PhaseManager:OpenPhase(PhaseId.BlackboxEntry, { packageId = self.m_packageId, blackboxId = blackboxId })
        end
    end, true)
end

FacTechTreeCtrl._OnPackUpBtnClick = HL.Method() << function(self)
    self:_ToggleTechNodeRelativeBlackboxPanel(false)
end

FacTechTreeCtrl._OpenNodeDetail = HL.Method() << function(self)
    self:_ShowSideBar(SidebarType.NodeDetails)
    self:_ToggleTechNodeRelativeBlackboxPanel(false)

    self.view.sidebar.facTechNodeDetail.animationWrapper:SampleToOutAnimationEnd()
end

FacTechTreeCtrl._OpenBlackboxOverview = HL.Method() << function(self)
    self:_ShowSideBar(SidebarType.BlackboxList)

    local blackboxOverview = self.view.sidebar.blackboxOverview
    local count = #self.m_allBlackboxIds
    blackboxOverview.blackboxScrollList:UpdateCount(count)

    blackboxOverview.contentNode.gameObject:SetActiveIfNecessary(count > 0)
    blackboxOverview.emptyNode.gameObject:SetActiveIfNecessary(count == 0)
end

FacTechTreeCtrl._ShowSideBar = HL.Method(HL.Number) << function(self, sidebarType)
    self.m_showSidebar = true
    self.view.bigRectHelper:ChangePaddingRight(math.floor(self.view.sidebar.rectTransform.sizeDelta.x))

    self.view.btnBlackbox.gameObject:SetActiveIfNecessary(false)

    self.view.sidebar.gameObject:SetActiveIfNecessary(true)
    self.view.sidebar.facTechNodeDetail.gameObject:SetActiveIfNecessary(sidebarType == SidebarType.NodeDetails)
    self.view.sidebar.blackboxOverview.gameObject:SetActiveIfNecessary(sidebarType == SidebarType.BlackboxList)
    InputManagerInst:ToggleGroup(self.view.sidebar.inputBindingGroupMonoTarget.groupId, true)
end

FacTechTreeCtrl._CloseSidebar = HL.Method(HL.Opt(HL.Function)) << function(self, onFinish)
    if self.m_showSidebar == false then
        return
    end
    self:_ToggleSideBarForController(false)

    self.m_showSidebar = false
    self.view.bigRectHelper:ChangePaddingRight(0)

    self.view.btnBlackbox.gameObject:SetActiveIfNecessary(true)


    if self.m_curSelectNode then
        self.m_curSelectNode:OnSelect(false)
        self.m_curSelectNode = nil
    end


    InputManagerInst:ToggleGroup(self.view.sidebar.inputBindingGroupMonoTarget.groupId, false)

    self.view.sidebar.animationWrapper:PlayOutAnimation(function()
        self.view.sidebar.gameObject:SetActiveIfNecessary(false)
        if onFinish then
            onFinish()
        end
    end)
end

FacTechTreeCtrl._GenFilterArgs = HL.Method().Return(HL.Table) << function(self)
    return FactoryUtils.genFilterBlackboxArgs(self.m_packageId, function(selectedTags)
        self:_OnFilterConfirm(selectedTags)
    end)
end

FacTechTreeCtrl._OnFilterConfirm = HL.Method(HL.Table) << function(self, selectedTags)
    selectedTags = selectedTags or {}
    local blackboxOverview = self.view.sidebar.blackboxOverview
    local ids = FactoryUtils.getFilterBlackboxIds(self.m_packageId, selectedTags)
    self.m_allBlackboxIds = FactoryUtils.getBlackboxInfoTbl(ids, false)

    local hasFilterResult = #ids > 0
    blackboxOverview.contentNode.gameObject:SetActiveIfNecessary(hasFilterResult)
    blackboxOverview.emptyNode.gameObject:SetActiveIfNecessary(not hasFilterResult)
    blackboxOverview.hasFilter.gameObject:SetActiveIfNecessary(#selectedTags > 0)
    if hasFilterResult then
        blackboxOverview.blackboxScrollList:UpdateCount(#ids)
    end
end

FacTechTreeCtrl._OnBtnBlackboxClick = HL.Method() << function(self)
    PhaseManager:OpenPhase(PhaseId.BlackboxEntry, { packageId = self.m_packageId })
end

FacTechTreeCtrl._ShowUnlock = HL.Method() << function(self)
    local args = self.m_popupArgs
    if #args.unlockItems > 0 then
        self.m_popupUIState = self.m_popupUIState + 1
        Notify(MessageConst.SHOW_TECH_TREE_POP_UP, {
            techId = args.techId,
            unlockItems = args.unlockItems,
            state = self.m_popupUIState,
            onStageFinishCb = function()
                self:_ShowLevelUp()
            end
        })
    else
        self:_ShowLevelUp()
    end
end

FacTechTreeCtrl._ShowLevelUp = HL.Method() << function(self)
    local args = self.m_popupArgs
    if not string.isEmpty(args.buildingInfo.buildingId) then
        self.m_popupUIState = self.m_popupUIState + 1
        Notify(MessageConst.SHOW_TECH_TREE_POP_UP, {
            techId = args.techId,
            buildingInfo = args.buildingInfo,
            state = self.m_popupUIState,
            onStageFinishCb = function()
                self:_ShowRewards()
            end
        })
    else
        self:_ShowRewards()
    end
end

FacTechTreeCtrl._ShowRewards = HL.Method() << function(self, args)
    local args = self.m_popupArgs
    if #args.rewardsItems > 0 then
        self.m_popupUIState = self.m_popupUIState + 1
        Notify(MessageConst.SHOW_TECH_TREE_POP_UP, {
            techId = args.techId,
            state = self.m_popupUIState,
            rewardsItems = args.rewardsItems,
            onStageFinishCb = function()
                self:_HidePopup()
            end
        })
    else
        self:_HidePopup()
    end
end

FacTechTreeCtrl._HidePopup = HL.Method() << function(self)
    if self.m_popupUIState > 0 then
        Notify(MessageConst.HIDE_TECH_TREE_POP_UP, { onHide = function()
            if self.m_popupArgs.onHideCb then
                self.m_popupArgs.onHideCb()
            end
        end })
    end
    self.m_popupUIState = 0
end

FacTechTreeCtrl._OnOpenTweenFinished = HL.Method() << function(self)
    self:_StartUnhiddenCategoryShow()



    local succ, _ = Tables.facSTTNodeTable:TryGetValue(self.m_externalFocusId)
    if DeviceInfo.usingController and not succ then
        local focusTechId
        if not string.isEmpty(self.m_curSelectTechId) then
            focusTechId = self.m_curSelectTechId
        elseif not string.isEmpty(self.m_recommendTechId) then
            focusTechId = self.m_recommendTechId
        end

        if string.isEmpty(focusTechId) then

            local cells = self.m_nodeCells:GetItems()

            for _, cell in ipairs(cells) do
                if GameInstance.player.facTechTreeSystem:NodeIsLocked(cell.techId) then
                    focusTechId = cell.techId
                    break
                end
            end
        end

        local focusTechCellIndex = string.isEmpty(focusTechId) and 1 or self.m_techId2CellLuaIndex[focusTechId]

        local defaultTargetCell = self.m_nodeCells:Get(focusTechCellIndex)
        self:SetNaviTarget(defaultTargetCell.view.itemBtn)
    end

    if self.m_needRecoverRelativeBlackboxNode and self.m_curSelectNode then
        self.m_needRecoverRelativeBlackboxNode = false
        self:_OnRelativeBtnClick()
    end
end

FacTechTreeCtrl._AllOpenProgressFinished = HL.Method() << function(self)
    self:AutoSelect(self.m_curSelectTechId)
    self.view.mask.gameObject:SetActiveIfNecessary(false)

    if not string.isEmpty(self.m_needOpenLayerId) and
            GameInstance.player.facTechTreeSystem:LayerIsLocked(self.m_needOpenLayerId) then
        UIManager:Open(PanelId.FacTechTreeUnlockTierPopup, { layerId = self.m_needOpenLayerId,
                                                             lockedLayerIds = self.m_lockedLayerIds })
    end
    self.m_needOpenLayerId = ""
    self.m_isAllOpenProgressFinished = true
    self:_ProcessCacheAfterAllOpenProgress()
    CS.Beyond.Gameplay.Conditions.OnSTTAllOpenProgressFinished.Trigger()

    self:_StartCoroutine(function()
        coroutine.wait(0.3)
        InputManagerInst:ToggleBinding(self.m_zoomOutActionId, true)
        InputManagerInst:ToggleBinding(self.m_zoomActionId, true)
    end)
end

FacTechTreeCtrl._FocusTechNode = HL.Method(Transform, HL.Opt(HL.Boolean, HL.Function)) << function(self, trans, needTween, cb)
    self.m_isFocus = true
    self.view.bigRectHelper:FocusNode(trans, needTween, function()
        if cb then
            cb()
        end
        self.m_isFocus = false
    end)
end

FacTechTreeCtrl._ProcessCacheAfterAllOpenProgress = HL.Method() << function(self)
    if string.isEmpty(self.m_externalFocusId) then
        return
    end

    local id = self.m_externalFocusId
    self.m_curSelectTechId = id
    self.m_externalFocusId = ""
    local isTech, techCfg = Tables.facSTTNodeTable:TryGetValue(id)
    local isLayer, layerCfg = Tables.facSTTLayerTable:TryGetValue(id)
    local isCategory, categoryCfg = Tables.facSTTCategoryTable:TryGetValue(id)
    if isTech then
        self:FocusTechTreeNode({ id })
    elseif isCategory then
        self:FocusTechTreeCategory({ id })
    elseif isLayer then
        self:FocusTechTreeLayer({ id })
    end
end




FacTechTreeCtrl._StartUnhiddenCategoryShow = HL.Method() << function(self)
    if #self.m_showUnhiddenCategoryQueue > 0 then
        self.m_showUnhiddenCategoryIndex = 0
        table.sort(self.m_showUnhiddenCategoryQueue, Utils.genSortFunction({"order"}))
        self:_NextUnhiddenCategoryShow()
    else
        self:_StartUnhiddenTechShow()
    end
end

FacTechTreeCtrl._NextUnhiddenCategoryShow = HL.Method() << function(self)
    self.m_showUnhiddenCategoryIndex = self.m_showUnhiddenCategoryIndex + 1
    if self.m_showUnhiddenCategoryIndex > #self.m_showUnhiddenCategoryQueue then

        self:_StartUnhiddenTechShow()
    else

        local actionInfo = self.m_showUnhiddenCategoryQueue[self.m_showUnhiddenCategoryIndex]

        local categoryTabCell = self.m_categoryTabCells:Get(actionInfo.order)
        local unhiddenCategoryBgCell = self.m_unhiddenCategoryBgCells:Get(actionInfo.relativeUnhiddenCategoryListIndex)
        self.m_unhiddenShowCor = self:_ClearCoroutine(self.m_unhiddenShowCor)
        self.m_unhiddenShowCor = self:_StartCoroutine(function()
            self.view.bigRectHelper:FocusNode(unhiddenCategoryBgCell.rectTransform)

            coroutine.wait(0.5)

            local tabClipLength = categoryTabCell.animationWrapper:GetClipLength(UnhiddenClipName.CategoryTab)
            local bgClipLength = unhiddenCategoryBgCell.animationWrapper:GetClipLength(UnhiddenClipName.CategoryBg)
            categoryTabCell.gameObject:SetActiveIfNecessary(true)
            categoryTabCell.animationWrapper:Play(UnhiddenClipName.CategoryTab)
            unhiddenCategoryBgCell.gameObject:SetActiveIfNecessary(true)
            unhiddenCategoryBgCell.animationWrapper:Play(UnhiddenClipName.CategoryBg)
            AudioAdapter.PostEvent("Au_UI_Event_FacTechTree_NewTypeUnlock")
            coroutine.wait(math.max(tabClipLength, bgClipLength))
            GameInstance.player.facTechTreeSystem:ReadUnhiddenCategory({ actionInfo.categoryId })

            self:_NextUnhiddenCategoryShow()
        end)
    end
end

FacTechTreeCtrl._StartUnhiddenTechShow = HL.Method() << function(self)
    if #self.m_showUnhiddenTechQueue > 0 then
        self.m_showUnhiddenTechIndex = 0
        table.sort(self.m_showUnhiddenTechQueue, Utils.genSortFunction({"y", "x"}))
        self:_NextUnhiddenTechShow()
    else
        self:_AllOpenProgressFinished()
    end
end

FacTechTreeCtrl._NextUnhiddenTechShow = HL.Method() << function(self)
    self.m_showUnhiddenTechIndex = self.m_showUnhiddenTechIndex + 1
    if self.m_showUnhiddenTechIndex > #self.m_showUnhiddenTechQueue then


        self:_AllOpenProgressFinished()
    else

        local actionInfo = self.m_showUnhiddenTechQueue[self.m_showUnhiddenTechIndex]
        local techId = actionInfo.techId
        local nodeCell = self.m_nodeCells:Get(self.m_techId2CellLuaIndex[techId])

        local lineCellIndex = self.m_techId2LineCellLuaIndex[techId]
        local lineCell
        if lineCellIndex then
            lineCell = self.m_lineCells:Get(lineCellIndex)
        end

        local unhiddenTechStartShowInterval = self.view.config.UNHIDDEN_TECH_START_SHOW_INTERVAL
        local isFullInScreen = self:_IsElementFullInScreen(nodeCell.rectTransform)
        self.m_unhiddenShowCor = self:_ClearCoroutine(self.m_unhiddenShowCor)
        self.m_unhiddenShowCor = self:_StartCoroutine(function()

            if not isFullInScreen then

                self.view.bigRectHelper:FocusNode(nodeCell.rectTransform)
                coroutine.wait(0.5)
            end

            local lineClipLength = 0
            if lineCell then
                lineClipLength = lineCell.view.animationWrapper:GetClipLength(UnhiddenClipName.TechLine)
                lineCell.view.gameObject:SetActiveIfNecessary(true)
                lineCell.view.animationWrapper:Play(UnhiddenClipName.TechLine)
            end

            nodeCell.view.gameObject:SetActiveIfNecessary(true)
            nodeCell.view.animationWrapper:Play(UnhiddenClipName.TechNode)


            local showTime = unhiddenTechStartShowInterval

            AudioAdapter.PostEvent("Au_UI_Popup_NewTechnodeAppear")
            coroutine.wait(showTime)
            GameInstance.player.facTechTreeSystem:ReadUnhiddenTech({ actionInfo.techId })

            self:_NextUnhiddenTechShow()
        end)
    end
end

FacTechTreeCtrl._IsElementFullInScreen = HL.Method(RectTransform).Return(HL.Boolean) << function(self, trans)
    local vectorArray = CS.System.Array.CreateInstance(typeof(Vector3), 4)
    trans:GetWorldCorners(vectorArray)
    for i = 0, vectorArray.Length - 1 do
        local vec = vectorArray[i]
        if not UIUtils.isPosInScreen(vec, self.uiCamera) then
            return false
        end
    end
    return true
end





local ActionOnSetNaviTarget = CS.Beyond.Input.ActionOnSetNaviTarget

local TOGGLE_NAVI_KEY = "TOGGLE_NAVI_KEY"

FacTechTreeCtrl.m_zoomOutActionId = HL.Field(HL.Number) << -1

FacTechTreeCtrl.m_zoomActionId = HL.Field(HL.Number) << -1

FacTechTreeCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.view.sidebar.facTechNodeDetail.obtainNode.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
        self.view.sidebar.facTechNodeDetail.controllerFocusHintNode.gameObject:SetActive(not isFocused)
    end)


    self.m_zoomOutActionId = self:BindInputPlayerAction("tech_tree_panel_zoom_out", function()
        if self.m_isFocus then
            return
        end
        self.view.bigRectHelper:ManuallyZoom(self.view.config.CONTROLLER_ZOOM_VALUE, true)


    end, self.view.inputGroup.groupId)


    self.m_zoomActionId = self:BindInputPlayerAction("tech_tree_panel_zoom", function()
        if self.m_isFocus then
            return
        end
        self.view.bigRectHelper:ManuallyZoom(-self.view.config.CONTROLLER_ZOOM_VALUE, true)


    end, self.view.inputGroup.groupId)

    InputManagerInst:ToggleBinding(self.m_zoomOutActionId, false)
    InputManagerInst:ToggleBinding(self.m_zoomActionId, false)
end

FacTechTreeCtrl._ToggleUnlockTier = HL.Method(HL.Boolean) << function(self, isEnable)
    local layerCells = self.m_layerCells:GetItems()
    for _, cell in pairs(layerCells) do
        cell:OnLayerInputEnableChange(isEnable)
    end
end

FacTechTreeCtrl._ToggleNodeCellFocusAsClick = HL.Method(HL.Boolean) << function(self, isOn)
    if not DeviceInfo.usingController then
        return
    end

    local type = isOn and ActionOnSetNaviTarget.AutoTriggerOnClick or ActionOnSetNaviTarget.PressConfirmTriggerOnClick

    self.m_nodeCells:Update(function(cell, _)
        cell.view.itemBtn:ChangeActionOnSetNaviTarget(type)
    end)
end

FacTechTreeCtrl._ToggleSideBarForController = HL.Method(HL.Boolean) << function(self, isOn)
    if not DeviceInfo.usingController then
        return
    end

    self.view.titleNode.enabled = not isOn
    self:_ToggleNodeCellFocusAsClick(isOn)
    self:_ToggleUnlockTier(not isOn)
end

FacTechTreeCtrl._OnIsNaviTargetChangedNode = HL.Method(HL.Forward("FacTechTreeNode"), HL.Boolean) << function(self, cell, isTarget)
    if isTarget then
        self.view.bigRectHelper:ChangePivotPositionToTarget(cell.transform)
        self:_FocusTechNode(cell.transform, true)
        self.m_curFocusNode = cell
    end
end



HL.Commit(FacTechTreeCtrl)
