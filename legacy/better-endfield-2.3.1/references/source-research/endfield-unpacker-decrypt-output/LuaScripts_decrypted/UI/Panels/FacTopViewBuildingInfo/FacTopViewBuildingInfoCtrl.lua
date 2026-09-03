local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacTopViewBuildingInfo
local LuaNodeCache = require_ex("Common/Utils/LuaNodeCache")

FacTopViewBuildingInfoCtrl = HL.Class('FacTopViewBuildingInfoCtrl', uiCtrl.UICtrl)






FacTopViewBuildingInfoCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.FAC_TOGGLE_TOP_VIEW_BUILDING_INFO] = 'ToggleTopViewBuildingInfo',
    [MessageConst.FAC_ON_BUILDING_MOVED] = 'OnBuildingMoved',
    [MessageConst.FAC_ON_NODE_REMOVED] = 'OnNodeRemoved',
    [MessageConst.FAC_ON_PENDING_SLOTS_REMOVED] = 'OnPendingSlotsRemoved',
    [MessageConst.ON_FAC_TOP_VIEW_CAM_ZOOM] = 'OnFacTopViewCamZoom',
    [MessageConst.FAC_UPDATE_TOP_VIEW_BUILDING_INFOS] = 'FacUpdateTopViewBuildingInfos',
    [MessageConst.FAC_TOP_VIEW_SET_BLUEPRINT_ICONS] = 'SetBlueprintIcons',
    [MessageConst.FAC_TOP_VIEW_SET_BLUEPRINT_ICON_POS] = 'SetBlueprintIconPos',
}


FacTopViewBuildingInfoCtrl.m_isShowing = HL.Field(HL.Boolean) << false

FacTopViewBuildingInfoCtrl.m_updateCor = HL.Field(HL.Thread)

FacTopViewBuildingInfoCtrl.m_cellCache = HL.Field(LuaNodeCache)

FacTopViewBuildingInfoCtrl.m_cells = HL.Field(HL.Table)

FacTopViewBuildingInfoCtrl.m_onAddFunc = HL.Field(HL.Function)

FacTopViewBuildingInfoCtrl.m_onRemoveFunc = HL.Field(HL.Function)

FacTopViewBuildingInfoCtrl.m_onUpdateFunc = HL.Field(HL.Function)

FacTopViewBuildingInfoCtrl.m_padding = HL.Field(HL.Any)

FacTopViewBuildingInfoCtrl.m_iconCache = HL.Field(LuaNodeCache)



FacTopViewBuildingInfoCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_cellCache = LuaNodeCache(self.view.buildingInfoCell, self.view.main)
    self.m_cells = {}
    self.m_onAddFunc = function(info)
        self:_OnAddInfo(info)
    end
    self.m_onRemoveFunc = function(nodeId)
        self:_OnRemoveInfo(nodeId)
    end
    self.m_onUpdateFunc = function(info)
        self:_OnUpdateInfo(info)
    end

    self.m_iconCache = LuaNodeCache(self.view.productCell, self.view.main)

    self.m_padding = CSFactoryUtil.Padding(self.view.config.PADDING_TOP, self.view.config.PADDING_LEFT,
            self.view.config.PADDING_RIGHT, self.view.config.PADDING_BOTTOM)
end

FacTopViewBuildingInfoCtrl.OnShow = HL.Override() << function(self)
    if UIManager:IsOpen(PanelId.GeneralTracker) then
        UIManager:SetTopOrder(PanelId.GeneralTracker)
    end
    if self.m_isShowing then
        self:_UpdateInfos()
    end
    self.m_updateCor = self:_StartCoroutine(function()
        coroutine.step()
        self.view.main.gameObject:SetActive(true)

        while true do
            coroutine.step()
            if self.m_isShowing then
                self:_UpdateInfos()
            end
        end
    end)
    if LuaSystemManager.factory.m_topViewCamCtrl ~= nil then


        self:OnFacTopViewCamZoom(LuaSystemManager.factory.m_topViewCamCtrl.curZoomPercent)
    end
end

FacTopViewBuildingInfoCtrl.OnHide = HL.Override() << function(self)
    self.view.main.gameObject:SetActive(false)
    self.m_updateCor = self:_ClearCoroutine(self.m_updateCor)
end

FacTopViewBuildingInfoCtrl.OnClose = HL.Override() << function(self)
    self.m_updateCor = self:_ClearCoroutine(self.m_updateCor)
    self:_ClearCache()
end

FacTopViewBuildingInfoCtrl._ClearCache = HL.Method() << function(self)
    for _, cell in pairs(self.m_cells) do
        if cell.m_isIcon then
            self.m_iconCache:Cache(cell)
        else
            self.m_cellCache:Cache(cell)
        end
    end
    self.m_cells = {}
end

FacTopViewBuildingInfoCtrl.ToggleTopViewBuildingInfo = HL.Method(HL.Boolean) << function(self, active)
    CSFactoryUtil.ClearTopViewBuildingInfos()
    if active then
        self:_ClearCache()
        self:Show()
        self.m_isShowing = true
        self:_UpdateAllInfos()
    else
        self.m_isShowing = false
        self:Hide()
        self:_ClearCache()
    end
end

FacTopViewBuildingInfoCtrl.OnBuildingMoved = HL.Method(HL.Table) << function(self, arg)
    self:OnNodeRemoved(arg)
end

FacTopViewBuildingInfoCtrl.OnPendingSlotsRemoved = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    local toRemoveNodeIds = arg[1]
    for _, nodeId in pairs(toRemoveNodeIds) do
        self:_OnRemoveInfo(nodeId)
        CSFactoryUtil.s_topViewBuildingInfos:Remove(nodeId)
    end
end

FacTopViewBuildingInfoCtrl.FacUpdateTopViewBuildingInfos = HL.Method(HL.Table) << function(self, batchSelectTargets)
    for nodeId, info in pairs(batchSelectTargets) do
        if info == true then
            self:_OnRemoveInfo(nodeId)
            CSFactoryUtil.s_topViewBuildingInfos:Remove(nodeId)
        end
    end
    self:_UpdateInfos()
end

FacTopViewBuildingInfoCtrl.OnNodeRemoved = HL.Method(HL.Table) << function(self, arg)
    local nodeId = unpack(arg)
    self:_OnRemoveInfo(nodeId)
    CSFactoryUtil.s_topViewBuildingInfos:Remove(nodeId)
end

FacTopViewBuildingInfoCtrl._UpdateAllInfos = HL.Method() << function(self)

    CSFactoryUtil.UpdateTopViewBuildingInfos(self.m_padding, nil, nil, nil)
    for _, info in pairs(CSFactoryUtil.s_topViewBuildingInfos) do
        self:_OnAddInfo(info)
    end
end

FacTopViewBuildingInfoCtrl._UpdateInfos = HL.Method() << function(self)
    if lume.round(LuaSystemManager.factory.topViewCamTarget.transform.eulerAngles.y) % 90 ~= 0 then

        return
    end
    CSFactoryUtil.UpdateTopViewBuildingInfos(self.m_padding, self.m_onAddFunc, self.m_onRemoveFunc, self.m_onUpdateFunc)
end

FacTopViewBuildingInfoCtrl._OnAddInfo = HL.Method(CS.Beyond.Gameplay.Factory.FactoryUtil.TopViewBuildingInfo) << function(self, info)
    local nodeId = info.nodeId
    local isBuilding = info.dataId ~= nil

    logger.info("FacTopViewBuildingInfoCtrl._OnAddInfo", nodeId, isBuilding, info.dataId)

    if FactoryUtils.isDecoBuilding(info.dataId) then
        return
    end

    local cell
    if isBuilding then
        cell = self.m_cellCache:Get()
        cell.m_isIcon = false
        local isStateOnly = FacConst.FAC_TOP_VIEW_STATE_ONLY_BUILDING_IDS[info.dataId]
        if isStateOnly then
            cell.stateController:SetState("StateOnly")
            cell.ignoreState = false
        else
            local data = Tables.factoryBuildingTable[info.dataId]
            cell.icon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_PANEL_ICON, data.iconOnPanel)
            cell.iconShadow:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_PANEL_ICON, data.iconOnPanel)
            cell.name.text = data.name
            cell.nameNodeContentSizeFitter.horizontalFit = self.m_useMinNameSize and Unity.UI.ContentSizeFitter.FitMode.MinSize or Unity.UI.ContentSizeFitter.FitMode.PreferredSize
            cell.ignoreState = FacConst.FAC_TOP_VIEW_IGNORE_STATE_BUILDING_IDS[info.dataId] or FacConst.FAC_TOP_VIEW_IGNORE_STATE_BUILDING_TYPES[data.type]
            if not cell.ignoreState then
                cell.stateController:SetState("Normal")
            end
        end
    else
        cell = self.m_iconCache:Get()
        cell.m_isIcon = true
    end
    cell.elementFollower.followPosition = info.worldPos

    self:_OnUpdateInfo(info, cell)
    self.m_cells[nodeId] = cell
end

FacTopViewBuildingInfoCtrl._OnRemoveInfo = HL.Method(HL.Number) << function(self, nodeId)
    logger.info("FacTopViewBuildingInfoCtrl._OnRemoveInfo", nodeId)
    local cell = self.m_cells[nodeId]
    if cell then
        if cell.m_isIcon then
            self.m_iconCache:Cache(cell)
        else
            self.m_cellCache:Cache(cell)
        end
        self.m_cells[nodeId] = nil
    end
end

FacTopViewBuildingInfoCtrl._OnUpdateInfo = HL.Method(CS.Beyond.Gameplay.Factory.FactoryUtil.TopViewBuildingInfo, HL.Opt(HL.Any)) << function(self, info, cell)
    local nodeId = info.nodeId

    if FactoryUtils.isDecoBuilding(info.dataId) then
        return
    end

    if not cell then
        cell = self.m_cells[nodeId]
    end

    if not cell then
        logger.error("No Cell", nodeId, info.dataId)
        return
    end

    local isBuilding = info.dataId ~= nil
    if isBuilding then

        if not cell.ignoreState then
            local state = GEnums.FacBuildingState.__CastFrom(info.lastState)
            local spriteName = FacConst.FAC_TOP_VIEW_BUILDING_STATE_TO_SPRITE[state]
            if spriteName then
                cell.stateNode.gameObject:SetActive(true)
                cell.stateIcon:LoadSprite(UIConst.UI_SPRITE_FAC_TOP_VIEW, spriteName)
            else
                cell.stateNode.gameObject:SetActive(false)
            end
        end


        local hasItem = info.itemCount > 0
        if hasItem or (info.isEnvGenerator and info.envType ~= GEnums.FacEnvGenEnvType.None) then
            if cell.ignoreState then
                cell.stateController:SetState("NoStateWithItem")
            else
                cell.productNode.gameObject:SetActive(true)
            end
            if not cell.productCells then
                cell.productCells = UIUtils.genCellCache(cell.productCell)
                cell.productCells.m_items[1] = cell.productCell
            end

            local count = 1
            cell.productCells:Refresh(count, function(productCell, luaIndex)
                if hasItem then
                    local itemId = info["item" .. CSIndex(luaIndex)]
                    local itemData = Tables.itemTable[itemId]
                    productCell.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
                    UIUtils.setItemRarityImage(productCell.rarityLine, itemData.rarity)
                    productCell.rarityLine.gameObject:SetActive(true)
                    self:_UpdateLiquidIcon(productCell, itemId)
                else
                    local iconName = string.format("icon_gas_env_%s", string.lower(info.envType:ToString()))
                    productCell.icon:LoadSprite(UIConst.UI_SPRITE_FAC_GAS, iconName)
                    productCell.rarityLine.gameObject:SetActive(false)
                    if productCell.liquidIcon then
                        productCell.liquidIcon.gameObject:SetActive(false)
                    end
                end
            end)
        else
            if cell.ignoreState then
                cell.stateController:SetState("NoStateWithoutItem")
            else
                cell.productNode.gameObject:SetActive(false)
            end
        end

        if not info.isEnvGenerator and info.envType ~= GEnums.FacEnvGenEnvType.None then
            cell.envBg:SetState(info.envType:ToString())
        else
            cell.envBg:SetState("None")
        end
    else

        if info.itemCount > 0 then
            cell.gameObject:SetActive(true)
            local itemData = Tables.itemTable[info.item0]
            cell.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
            UIUtils.setItemRarityImage(cell.rarityLine, itemData.rarity)
            self:_UpdateLiquidIcon(cell, info.item0)
        else
            cell.gameObject:SetActive(false)
        end
    end
end

FacTopViewBuildingInfoCtrl.OnFacTopViewCamZoom = HL.Method(HL.Number) << function(self, zoomPercent)
    local scale = self.view.config.SIZE_ANIM_CURVE:Evaluate(zoomPercent)
    self.view.main.transform.localScale = Vector3(scale, scale, scale)
    local shouldUseMin = zoomPercent <= self.view.config.NAME_MIN_SIZE_ZOOM_PERCENT
    if shouldUseMin ~= self.m_useMinNameSize then
        self.m_useMinNameSize = shouldUseMin
        self:_UpdateNameSize()
    end
end


FacTopViewBuildingInfoCtrl.m_useMinNameSize = HL.Field(HL.Boolean) << false

FacTopViewBuildingInfoCtrl._UpdateNameSize = HL.Method() << function(self)
    local mode = self.m_useMinNameSize and Unity.UI.ContentSizeFitter.FitMode.MinSize or Unity.UI.ContentSizeFitter.FitMode.PreferredSize
    for _, cell in pairs(self.m_cells) do
        if not cell.m_isIcon then
            cell.nameNodeContentSizeFitter.horizontalFit = mode
        end
    end
end




FacTopViewBuildingInfoCtrl.m_curBPIconCells = HL.Field(HL.Table)

FacTopViewBuildingInfoCtrl.SetBlueprintIcons = HL.Method(HL.Opt(CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprint)) << function(self, bp)
    if self.m_curBPIconCells then
        for _, cell in ipairs(self.m_curBPIconCells) do
            cell.transform:SetParent(self.view.main.transform)
            self.m_iconCache:Cache(cell)
        end
        self.m_curBPIconCells = nil
    end
    if not bp then
        return
    end
    self.m_curBPIconCells = {}
    for _, entry in pairs(bp.buildingNodes) do
        if FacConst.FAC_VALVE_NODE_IDS[entry.templateId] and not string.isEmpty(entry.productIcon)
                and not FactoryUtils.isBlueprintProductIconGasEnv(entry.productIcon)
                and GameInstance.player.inventory:IsItemFound(entry.productIcon) then
            local cell = self.m_iconCache:Get()
            cell.m_isIcon = true
            local itemData = Tables.itemTable[entry.productIcon]
            cell.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
            cell.bpOffset = entry.worldSpatial.worldPosition
            UIUtils.setItemRarityImage(cell.rarityLine, itemData.rarity)
            self:_UpdateLiquidIcon(cell, entry.productIcon)
            table.insert(self.m_curBPIconCells, cell)
        end
    end
end

FacTopViewBuildingInfoCtrl.SetBlueprintIconPos = HL.Method(HL.Table) << function(self, args)
    local bpOriWorldPos, dir = unpack(args)
    for _, cell in ipairs(self.m_curBPIconCells) do
        if dir == 0 then
            cell.elementFollower.followPosition = bpOriWorldPos + cell.bpOffset
        elseif dir == 1 then
            cell.elementFollower.followPosition = bpOriWorldPos + Vector3(cell.bpOffset.z, cell.bpOffset.y, -cell.bpOffset.x)
        elseif dir == 2 then
            cell.elementFollower.followPosition = bpOriWorldPos + Vector3(-cell.bpOffset.x, cell.bpOffset.y, -cell.bpOffset.z)
        elseif dir == 3 then
            cell.elementFollower.followPosition = bpOriWorldPos + Vector3(-cell.bpOffset.z, cell.bpOffset.y, cell.bpOffset.x)
        end
    end
end



FacTopViewBuildingInfoCtrl._UpdateLiquidIcon = HL.Method(HL.Table, HL.String) << function(self, cell, itemId)

    local liquidIcon
    local isFullBottle, fullBottleData = Tables.fullBottleTable:TryGetValue(itemId)
    if isFullBottle then
        local liquidData = Tables.itemTable[fullBottleData.liquidId]
        liquidIcon = liquidData.iconId
    else
        isFullBottle, fullBottleData = Tables.fullGasJarTable:TryGetValue(itemId)
        if isFullBottle then
            local gasData = Tables.itemTable[fullBottleData.gasId]
            liquidIcon = gasData.iconId
        end
    end
    if not cell.liquidIcon then
        if not liquidIcon then
            return
        end
        local obj = CSUtils.CreateObject(LuaSystemManager.itemPrefabSystem.liquidIconPrefab, cell.transform)
        obj.name = "LiquidIcon"
        obj.transform.localScale = Vector3.one
        local center = Vector2.one / 2
        obj.transform.pivot = center
        obj.transform.anchorMin = center
        obj.transform.anchorMax = center
        obj.transform.anchoredPosition = Vector2.zero

        local size = 80 * cell.transform.rect.width / 180
        obj.transform.sizeDelta = Vector2(size, size)
        cell.liquidIcon = obj:GetComponent("UIImage")
    end
    if liquidIcon then
        cell.liquidIcon.gameObject:SetActive(true)
        cell.liquidIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, liquidIcon)
    else
        cell.liquidIcon.gameObject:SetActive(false)
    end
end


HL.Commit(FacTopViewBuildingInfoCtrl)
