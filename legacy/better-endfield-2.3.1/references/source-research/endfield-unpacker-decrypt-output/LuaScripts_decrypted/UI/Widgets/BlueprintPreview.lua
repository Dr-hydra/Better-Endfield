local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local LuaNodeCache = require_ex('Common/Utils/LuaNodeCache')
local CommonCache = require_ex('Common/Utils/CommonCache')

BlueprintPreview = HL.Class('BlueprintPreview', UIWidgetBase)


local uiSizePerUnit = 128


BlueprintPreview.m_csBP = HL.Field(CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprint)

BlueprintPreview.m_nodeCellCache = HL.Field(LuaNodeCache)

BlueprintPreview.m_conveyorCellCache = HL.Field(LuaNodeCache)

BlueprintPreview.m_showingCellDic = HL.Field(HL.Table)

BlueprintPreview.m_previewHelper = HL.Field(CS.Beyond.UI.BlueprintPreviewHelper)

BlueprintPreview.m_canEdit = HL.Field(HL.Boolean) << false

BlueprintPreview.m_updateId = HL.Field(HL.Number) << -1

BlueprintPreview.m_nextTargetId = HL.Field(HL.Number) << 1

BlueprintPreview.m_id2Cell = HL.Field(HL.Table)

BlueprintPreview.m_nodeIdToTargetId = HL.Field(HL.Table)



BlueprintPreview.m_envGenCoverInfos = HL.Field(HL.Table)


BlueprintPreview.m_pinnedUdpipeTargetId = HL.Field(HL.Any)
BlueprintPreview.m_pinnedUdpipePeerTargetId = HL.Field(HL.Any)


BlueprintPreview.m_lastHoveredUdpipeTargetId = HL.Field(HL.Any)

BlueprintPreview.m_bpAbnormalIconHelper = HL.Field(HL.Table)

BlueprintPreview.mouseShow = HL.Field(HL.Boolean) << true




BlueprintPreview.m_viewConnBindingId = HL.Field(HL.Number) << 0











local NodeType = {
    Building = 1,
    Logistic = 2,
    Belt = 3,
    Pipe = 4,
}
local Face2Vector2 = {
    [0] = Vector2(0, 1),
    [1] = Vector2(1, 0),
    [2] = Vector2(0, -1),
    [3] = Vector2(-1, 0),
}
local Face2RotZForConveyor = {
    [0] = 90,
    [1] = 0,
    [2] = -90,
    [3] = 180,
}
local Face2RotZForBuilding = {
    [0] = 360,
    [1] = 270,
    [2] = 180,
    [3] = 90,
}

local function GetPortRenderKey(isPipe, isInput)

    if isPipe then
        return isInput and "pipeIn" or "pipeOut"
    end
    return isInput and "beltIn" or "beltOut"
end





local function GetBuildingGridRect(entry, bData, extendX, extendZ)
    local ex = extendX or 0
    local ez = extendZ or 0
    local w = bData.range.width + ex * 2
    local d = bData.range.depth + ez * 2

    local swap = entry.spatial.face == 1 or entry.spatial.face == 3
    local sw = swap and d or w
    local sd = swap and w or d
    local pos = entry.worldSpatial.worldPosition
    local minX = lume.round(pos.x - sw / 2)
    local minY = lume.round(pos.z - sd / 2)
    return minX, minY, sw, sd
end



BlueprintPreview._OnFirstTimeInit = HL.Override() << function(self)
    self.m_nodeCellCache = LuaNodeCache(self.view.nodeCell, self.view.content)
    self.m_conveyorCellCache = LuaNodeCache(self.view.conveyorCell, self.view.content)
    self.m_previewHelper = CS.Beyond.UI.BlueprintPreviewHelper()
    self.view.maskBtn.onClick:AddListener(function()
        self:_OnClick()
    end)
    self:_InitChangeIconNode()



    local panelCtrl = self:GetUICtrl()
    local panelInputGroup = panelCtrl and panelCtrl.view.inputGroup
    if panelInputGroup then
        self.m_viewConnBindingId = UIUtils.bindInputPlayerAction("fac_blueprint_view_connection", function()
            self:_OnControllerViewConnection()
        end, panelInputGroup.groupId) or 0

        self:_RefreshViewConnectionHint()
    end


    local secondCellObj = CSUtils.CreateObject(self.view.hoverTipsCell.gameObject, self.view.hoverTipsNode)
    secondCellObj.name = "HoverTipsCell2"
    secondCellObj.transform:SetAsLastSibling()
    self.view.hoverTipsCell2 = Utils.wrapLuaNode(secondCellObj)
end

BlueprintPreview.m_widthScale = HL.Field(HL.Number) << 1

BlueprintPreview.m_heightScale = HL.Field(HL.Number) << 1

BlueprintPreview.InitBlueprintPreview = HL.Method(CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprint, HL.Boolean, HL.Opt(HL.Table)) << function(self, csBP, canEdit, bpAbnormalIconHelper)
    self:_FirstTimeInit()

    self:_HideChangeIconNode(true)
    if self.m_showingCellDic then
        for cell, info in pairs(self.m_showingCellDic) do
            if info.type == NodeType.Building or info.type == NodeType.Logistic then
                self.m_nodeCellCache:Cache(cell)
            elseif info.type == NodeType.Belt or info.type == NodeType.Pipe then
                self.m_conveyorCellCache:Cache(cell)
            end
        end
    end

    self.m_csBP = csBP
    self.m_showingCellDic = {}
    self.m_changedIcons = {}
    self.m_canEdit = canEdit
    self.m_bpAbnormalIconHelper = bpAbnormalIconHelper

    self.m_pinnedUdpipeTargetId = nil
    self.m_pinnedUdpipePeerTargetId = nil
    self.m_lastHoveredUdpipeTargetId = nil
    self.view.pipeConnectionLine.gameObject:SetActive(false)
    self:_CancelHover()


    local range = self.m_csBP.sourceRect
    local bpUISize = Vector2(range.width, range.height) * uiSizePerUnit
    self.view.content.transform.sizeDelta = bpUISize
    local viewSize = self.view.scrollRect.transform.rect.size - Vector2(100, 100)
    local widthRatio = viewSize.x / bpUISize.x
    local heightRatio = viewSize.y / bpUISize.y
    local scaleValue = math.min(widthRatio, heightRatio, 1)
    scaleValue = math.max(scaleValue, self.view.config.MIN_SCALE)

    self.m_widthScale = scaleValue/widthRatio - 1
    self.m_heightScale = scaleValue/heightRatio - 1

    self.view.content.transform.localScale = Vector3(scaleValue, scaleValue, 1)


    local curMaxShowingCount = self.view.scrollRect.transform.rect.size / (uiSizePerUnit * scaleValue)
    local bgWidth, bgHeight
    if curMaxShowingCount.x > range.width then
        bgWidth = math.ceil(curMaxShowingCount.x)

        bgWidth = bgWidth + (bgWidth + range.width) % 2
    else
        bgWidth = range.width + 2
    end
    if curMaxShowingCount.y > range.height then
        bgHeight = math.ceil(curMaxShowingCount.y)

        bgHeight = bgHeight + (bgHeight + range.height) % 2
    else
        bgHeight = range.height + 2
    end
    self.view.gridImg.transform.sizeDelta = Vector2(bgWidth, bgHeight) * uiSizePerUnit
    self.view.gridImg.transform:SetAsFirstSibling()

    self.m_previewHelper:SetSize(range.width, range.height)
    self.m_nextTargetId = 1
    self.m_id2Cell = {}


    for _, entry in pairs(self.m_csBP.buildingNodes) do
        self:_GenPreviewBuilding(entry)
    end



    self.m_nodeIdToTargetId = {}
    for _, info in pairs(self.m_showingCellDic) do
        if info.type == NodeType.Building or info.type == NodeType.Logistic then
            self.m_nodeIdToTargetId[info.entry.nodeId] = info.id
        end
    end





    local invScale = 1 / scaleValue
    for _, info in pairs(self.m_showingCellDic) do
        if info.type == NodeType.Building or info.type == NodeType.Logistic then
            local cell = self.m_id2Cell[info.id]
            cell.iconNode.changeHint.transform.localScale = Vector3(invScale, invScale, 1)
            if info.entry:IsUdpipe() then
                local _, isConnected = self:_TryGetUdpipeConnection(info.id)
                cell.udpipeStateStateController:SetState(isConnected and "NotSelected" or "NotConnect")
                cell.nonScaledNode.transform.localScale = Vector3(invScale, invScale, 1)
                if DeviceInfo.usingTouch then
                    cell.udpipeState:SampleToInAnimationEnd()
                else
                    cell.udpipeState:SampleToInAnimationBegin()
                end
            end
        end
    end



    self:_CollectEnvGenCoverInfos()
    self:_RefreshAllEnvReceiverIcons()


    for _, entry in pairs(self.m_csBP.conveyorNodes) do
        self:_GenPreviewConveyor(entry)
    end

    self.view.hoverHint.transform:SetAsLastSibling()
    if DeviceInfo.usingController then
        self.view.controllerMouse.gameObject:SetActive(true)
    else
        self.view.controllerMouse.gameObject:SetActive(false)
    end

    if DeviceInfo.usingController then
        InputManagerInst:SetCustomControllerMouse(self.view.controllerMouse.transform, self:GetUICtrl().uiCamera)
        self.view.changeIconNode.selectableNaviGroup.onIsFocusedChange:AddListener(function(isTarget)
            if not isTarget then
                self:_HideChangeIconNode()
            end
        end)
        self.view.controllerMouse.anchoredPosition = Vector2(self.view.scrollRectRectTransform.rect.size.x * 1/2, self.view.scrollRectRectTransform.rect.size.y * 1/2)
    end
end




BlueprintPreview._GenPreviewBuilding = HL.Method(CS.Beyond.Gameplay.RemoteFactory.BlueprintBuildingEntry) << function(self, entry)
    local cell = self.m_nodeCellCache:Get()
    local info = {
        id = self:_GetNextTargetId(),
        entry = entry,
    }
    self.m_showingCellDic[cell] = info
    self.m_id2Cell[info.id] = cell

    local templateId = entry.templateId
    local isBuilding, bData = Tables.factoryBuildingTable:TryGetValue(templateId)
    info.type = isBuilding and NodeType.Building or NodeType.Logistic

    if self.m_canEdit then
        local _, iconData = Tables.factoryBlueprintMachineIconTable:TryGetValue(templateId)
        info.canChangeIcon = iconData ~= nil and iconData.canModify
    else
        info.canChangeIcon = false
    end
    cell.selectedNode.gameObject:SetActive(false)

    cell.udpipeState.gameObject:SetActive(entry:IsUdpipe())

    cell.envNode.gameObject:SetActive(false)

    local spatial = entry.spatial
    local gridSize = Vector2(isBuilding and bData.range.width or 1, isBuilding and bData.range.depth or 1)
    local swapSize = spatial.face == 1 or spatial.face == 3
    local swappedGridSize = swapSize and Vector2(gridSize.y, gridSize.x) or gridSize
    local nodeUISize = gridSize * uiSizePerUnit
    cell.transform.sizeDelta = swappedGridSize * uiSizePerUnit
    local pos = entry.worldSpatial.worldPosition
    cell.transform.anchoredPosition = Vector2(pos.x, pos.z) * uiSizePerUnit
    cell.bg.transform.sizeDelta = nodeUISize
    cell.bg.transform.localEulerAngles = Vector3(0, 0, Face2RotZForBuilding[spatial.face])

    local minX = lume.round(pos.x - swappedGridSize.x / 2)
    local minY = lume.round(pos.z - swappedGridSize.y / 2)
    self.m_previewHelper:BatchAddGridValue(minX, minY, swappedGridSize.x, swappedGridSize.y, info.id)

    local bgPath, isDefaultBuilding
    if isBuilding then


        local spBGInfo = FacConst.BLUEPRINT_PREVIEW_SP_BUILDING_BG_BY_TEMPLATE_ID[templateId]
            or FacConst.BLUEPRINT_PREVIEW_SP_BUILDING_BG[bData.type]
        isDefaultBuilding = spBGInfo == nil
        bgPath = spBGInfo and spBGInfo[1] or FacConst.BLUEPRINT_PREVIEW_BUILDING_DEFAULT_BG
    else
        bgPath = string.format(FacConst.BLUEPRINT_PREVIEW_LOGISTIC_BG, templateId)
    end
    cell.bg:LoadSpriteWithOutFormat(bgPath)

    self:_PrepareCellImgCache(cell)

    if isDefaultBuilding then
        self:_GenDefaultBuildingBG(entry, cell, bData)
    else
        cell.waistDeco.gameObject:SetActive(false)
        cell.machineBG.gameObject:SetActive(false)
    end

    if not isBuilding then
        local _, isLiquid = FactoryUtils.getLogisticData(templateId)
        if isLiquid then
            info.isHighLayer = true
        end
    end
    if not info.isHighLayer then

        cell.transform:SetAsFirstSibling()
    end

    self:_UpdateTargetIcon(info.id)
end


BlueprintPreview._SetEdgeImgs = HL.Method(HL.Table, HL.Table, HL.String, HL.String) << function(self, edgeImgs, info, format, formatAlter)
    local count = info.count
    if count > 0 then
        local size = info.size
        if count == size then
            edgeImgs[info.edgeFace] = string.format(format, count)
        else
            edgeImgs[info.edgeFace] = string.format(formatAlter, size, count)
        end
    end
end

BlueprintPreview._GenDefaultBuildingBG = HL.Method(CS.Beyond.Gameplay.RemoteFactory.BlueprintBuildingEntry, HL.Table, HL.Any) << function(self, entry, cell, bData)
    local isBigBuilding = bData.range.width >= 3 and bData.range.depth >= 3 and (bData.range.width * bData.range.depth >= 9)
    local portGroups = self:_BuildDefaultBuildingPortGroups(entry)
    local edgeHasPort = {}

    for _, group in ipairs(portGroups) do
        edgeHasPort[group.edgeFace] = true
        local imgPath = self:_TryGetDefaultBuildingPortImgPath(group)
        if imgPath then


            local img = cell.m_imgCache:Get()
            img:LoadSpriteWithOutFormat(imgPath)
            img:SetNativeSizeIgnoreRefScale()
            img.type = CS.UnityEngine.UI.Image.Type.Simple

            local centerPos = (group.minPos + group.maxPos + 1) / 2
            if group.edgeFace == 0 then
                img.transform.anchoredPosition = Vector2(centerPos, bData.range.depth) * uiSizePerUnit
                img.transform.localEulerAngles = Vector3(0, 0, 0)
            elseif group.edgeFace == 1 then
                img.transform.anchoredPosition = Vector2(bData.range.width, centerPos) * uiSizePerUnit
                img.transform.localEulerAngles = Vector3(0, 0, 270)
            elseif group.edgeFace == 2 then
                img.transform.anchoredPosition = Vector2(centerPos, 0) * uiSizePerUnit
                img.transform.localEulerAngles = Vector3(0, 0, 180)
            elseif group.edgeFace == 3 then
                img.transform.anchoredPosition = Vector2(0, centerPos) * uiSizePerUnit
                img.transform.localEulerAngles = Vector3(0, 0, 90)
            end
            table.insert(cell.m_showingImgs, img)
        else

            self:_GenDefaultBuildingFallbackPorts(cell, group, bData.range.width, bData.range.depth)
        end
    end

    local needWaist = not edgeHasPort[1] and not edgeHasPort[3]
    for face = 0, 3 do
        if not edgeHasPort[face] and (face == 0 or face == 2) then
            local img = cell.m_imgCache:Get()
            img:LoadSpriteWithOutFormat(isBigBuilding and FacConst.BLUEPRINT_PREVIEW_BUILDING_DEFAULT_EDGE_BIG or FacConst.BLUEPRINT_PREVIEW_BUILDING_DEFAULT_EDGE_SMALL)
            img.transform.sizeDelta = Vector2(bData.range.width * uiSizePerUnit, isBigBuilding and 140 or 40)
            img.type = CS.UnityEngine.UI.Image.Type.Sliced
            if face == 0 then
                img.transform.anchoredPosition = Vector2(bData.range.width / 2, bData.range.depth) * uiSizePerUnit
                img.transform.localEulerAngles = Vector3(0, 0, 0)
            else
                img.transform.anchoredPosition = Vector2(bData.range.width / 2, 0) * uiSizePerUnit
                img.transform.localEulerAngles = Vector3(0, 0, 180)
            end
            table.insert(cell.m_showingImgs, img)
        end
    end
    cell.waistDeco.gameObject:SetActive(needWaist)
    if isBigBuilding then
        cell.machineBG:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_PANEL_ICON_BIG, bData.iconOnPanel)
        cell.machineBG.transform.localEulerAngles = Vector3(0, 0, -cell.bg.transform.localEulerAngles.z)
        cell.machineBG.gameObject:SetActive(true)
    else
        cell.machineBG.gameObject:SetActive(false)
    end
end

local function ResolveDefaultBuildingPortEdge(point, side, width, depth)
    local useZEdge = side == 0 or side == 2



    if useZEdge then
        if depth > 0 and point.z == depth - 1 then
            return 0, point.x
        end
        if point.z == 0 then
            return 2, point.x
        end
    else
        if width > 0 and point.x == width - 1 then
            return 1, point.z
        end
        if point.x == 0 then
            return 3, point.z
        end
    end
    if depth > 0 and point.z == depth - 1 then
        return 0, point.x
    end
    if width > 0 and point.x == width - 1 then
        return 1, point.z
    end
    if point.z == 0 then
        return 2, point.x
    end
    if point.x == 0 then
        return 3, point.z
    end
    return useZEdge and 0 or 1, useZEdge and point.x or point.z
end

BlueprintPreview._ResolveDefaultBuildingPorts = HL.Method(CS.Beyond.Gameplay.RemoteFactory.BlueprintBuildingEntry).Return(HL.Table) << function(self, entry)
    local result = {}
    local showPipePorts = false
    local formulaMode = entry:GetFormulaMode()
    if not formulaMode or formulaMode ~= FacConst.FAC_FORMULA_MODE_MAP.NORMAL then

        showPipePorts = true
    end

    local staticData = CS.Beyond.Gameplay.GameInstance.remoteFactoryManager and CS.Beyond.Gameplay.GameInstance.remoteFactoryManager.staticData
    if not staticData then
        return result
    end

    local buildingData = staticData:QueryBuildingData(entry.templateId)
    if not buildingData then
        return result
    end

    local width = buildingData.range.width
    local depth = buildingData.range.depth
    local function collectPorts(isInput, count)
        for i = 0, count - 1 do
            local port = isInput and buildingData:GetInputPort(i) or buildingData:GetOutputPort(i)


            local isClosed = not string.isEmpty(formulaMode) and CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryUtil.IsPortClosedOfProducerInMode(staticData, entry.templateId, formulaMode, isInput, i)
            if port and not isClosed and (showPipePorts or not port.isPipe) then


                local edgeFace, edgePos = ResolveDefaultBuildingPortEdge(port.edge.point, port.edge.side, width, depth)
                table.insert(result, {
                    isInput = isInput,
                    isPipe = port.isPipe,
                    edgeFace = edgeFace,
                    edgePos = edgePos,
                    height = port.edge.point.y,
                })
            end
        end
    end

    collectPorts(true, buildingData.inputPortCount)
    collectPorts(false, buildingData.outputPortCount)
    return result
end

BlueprintPreview._BuildDefaultBuildingPortGroups = HL.Method(CS.Beyond.Gameplay.RemoteFactory.BlueprintBuildingEntry).Return(HL.Table) << function(self, entry)
    local groupDic = {}
    local result = {}
    local ports = self:_ResolveDefaultBuildingPorts(entry)
    for _, port in ipairs(ports) do


        local key = string.format("%s_%d", GetPortRenderKey(port.isPipe, port.isInput), port.edgeFace)
        local group = groupDic[key]
        if not group then
            group = {
                isInput = port.isInput,
                isPipe = port.isPipe,
                edgeFace = port.edgeFace,
                minPos = math.maxinteger,
                maxPos = math.mininteger,
                count = 0,
                positions = {},
            }
            groupDic[key] = group
            table.insert(result, group)
        end
        group.count = group.count + 1
        group.minPos = math.min(group.minPos, port.edgePos)
        group.maxPos = math.max(group.maxPos, port.edgePos)
        table.insert(group.positions, port.edgePos)
    end

    for _, group in ipairs(result) do
        table.sort(group.positions)
        group.size = group.maxPos - group.minPos + 1

        group.isContinuous = group.count == group.size
    end
    table.sort(result, function(a, b)
        if a.edgeFace ~= b.edgeFace then
            return a.edgeFace < b.edgeFace
        end
        if a.isPipe ~= b.isPipe then
            return a.isPipe == false
        end
        return a.isInput == true and b.isInput == false
    end)
    return result
end

BlueprintPreview._TryGetDefaultBuildingPortImgPath = HL.Method(HL.Table).Return(HL.Opt(HL.String)) << function(self, group)
    if group.count <= 0 then
        return
    end

    local normalPath
    local alterPath
    if group.isPipe then
        normalPath = group.isInput and FacConst.BLUEPRINT_PREVIEW_PIPE_PORT_IN or FacConst.BLUEPRINT_PREVIEW_PIPE_PORT_OUT
        alterPath = group.isInput and FacConst.BLUEPRINT_PREVIEW_PIPE_PORT_IN_ALTER or FacConst.BLUEPRINT_PREVIEW_PIPE_PORT_OUT_ALTER
    else
        normalPath = group.isInput and FacConst.BLUEPRINT_PREVIEW_BELT_PORT_IN or FacConst.BLUEPRINT_PREVIEW_BELT_PORT_OUT
        alterPath = group.isInput and FacConst.BLUEPRINT_PREVIEW_BELT_PORT_IN_ALTER or FacConst.BLUEPRINT_PREVIEW_BELT_PORT_OUT_ALTER
    end

    if group.isContinuous then
        return string.format(normalPath, group.count)
    end



    local supportMap = FacConst.BLUEPRINT_PREVIEW_EDGE_PORT_ALT_SUPPORT[GetPortRenderKey(group.isPipe, group.isInput)]
    if supportMap and supportMap[group.size] and supportMap[group.size][group.count] then
        return string.format(alterPath, group.size, group.count)
    end
end

BlueprintPreview._GenDefaultBuildingFallbackPorts = HL.Method(HL.Table, HL.Table, HL.Number, HL.Number) << function(self, cell, group, width, depth)
    local imgPathMap = {
        beltIn = FacConst.BLUEPRINT_PREVIEW_BELT_PORT_IN_UNIT,
        beltOut = FacConst.BLUEPRINT_PREVIEW_BELT_PORT_OUT_UNIT,
        pipeIn = FacConst.BLUEPRINT_PREVIEW_PIPE_PORT_IN_UNIT,
        pipeOut = FacConst.BLUEPRINT_PREVIEW_PIPE_PORT_OUT_UNIT,
    }
    local imgPath = imgPathMap[GetPortRenderKey(group.isPipe, group.isInput)]
    if string.isEmpty(imgPath) then
        return
    end

    for _, edgePos in ipairs(group.positions) do

        local img = cell.m_imgCache:Get()
        img:LoadSpriteWithOutFormat(imgPath)
        img:SetNativeSizeIgnoreRefScale()
        img.type = CS.UnityEngine.UI.Image.Type.Simple
        if group.edgeFace == 0 then
            img.transform.anchoredPosition = Vector2(edgePos + 0.5, depth) * uiSizePerUnit
            img.transform.localEulerAngles = Vector3(0, 0, 0)
        elseif group.edgeFace == 1 then
            img.transform.anchoredPosition = Vector2(width, edgePos + 0.5) * uiSizePerUnit
            img.transform.localEulerAngles = Vector3(0, 0, 270)
        elseif group.edgeFace == 2 then
            img.transform.anchoredPosition = Vector2(edgePos + 0.5, 0) * uiSizePerUnit
            img.transform.localEulerAngles = Vector3(0, 0, 180)
        elseif group.edgeFace == 3 then
            img.transform.anchoredPosition = Vector2(0, edgePos + 0.5) * uiSizePerUnit
            img.transform.localEulerAngles = Vector3(0, 0, 90)
        end
        table.insert(cell.m_showingImgs, img)
    end
end






BlueprintPreview._GenPreviewConveyor = HL.Method(CS.Beyond.Gameplay.RemoteFactory.BlueprintConveyorEntry) << function(self, entry)


    local cell = self.m_conveyorCellCache:Get()
    local info = {
        id = self:_GetNextTargetId(),
        entry = entry,
        canChangeIcon = false,
    }
    self.m_showingCellDic[cell] = info
    self.m_id2Cell[info.id] = cell

    local templateId = entry.templateId
    local isBelt = templateId == FacConst.BELT_ID
    info.type = isBelt and NodeType.Belt or NodeType.Pipe
    info.segInfos = self:_GetConveyorSegmentInfos(entry)
    info.isHighLayer = not isBelt

    if isBelt then

        cell.transform:SetAsFirstSibling()
    end

    local imgs = isBelt and FacConst.BLUEPRINT_PREVIEW_BELT_IMGS or FacConst.BLUEPRINT_PREVIEW_PIPE_IMGS
    self:_PrepareCellImgCache(cell)
    for k, v in ipairs(info.segInfos) do

        local img = cell.m_imgCache:Get()
        if UNITY_EDITOR then
            img.gameObject.name = "Seg_" .. k
        end
        table.insert(cell.m_showingImgs, img)
        if v.length then

            img:LoadSpriteWithOutFormat(imgs.normal)
            local centerGridPos = v.startPoint + Vector2(0.5, 0.5) + (v.length - 1) / 2 * Face2Vector2[v.startFace]
            img.transform.anchoredPosition = centerGridPos * uiSizePerUnit
            img.transform.sizeDelta = Vector2(v.length, 1) * uiSizePerUnit
            img.transform.localEulerAngles = Vector3(0, 0, Face2RotZForConveyor[v.startFace])

            local endPoint = v.startPoint + (v.length - 1) * Face2Vector2[v.startFace]
            local minX = math.min(v.startPoint.x, endPoint.x)
            local minY = math.min(v.startPoint.y, endPoint.y)
            local sizeX = math.abs(v.startPoint.x - endPoint.x) + 1
            local sizeY = math.abs(v.startPoint.y - endPoint.y) + 1
            self.m_previewHelper:BatchAddGridValue(minX, minY, sizeX, sizeY, info.id)
        else

            local imgInfo = FacConst.BLUEPRINT_PREVIEW_CORNER_DIC[v.startFace][v.endFace]
            img:LoadSpriteWithOutFormat(imgInfo[1] and imgs.corner1 or imgs.corner2)
            local centerGridPos = v.startPoint + Vector2(0.5, 0.5)
            img.transform.anchoredPosition = centerGridPos * uiSizePerUnit
            img.transform.sizeDelta = Vector2(uiSizePerUnit, uiSizePerUnit)
            img.transform.localEulerAngles = Vector3(0, 0, imgInfo[2])

            self.m_previewHelper:AddGridValue(v.startPoint.x, v.startPoint.y, info.id)
        end

    end
end

BlueprintPreview._GetConveyorSegmentInfos = HL.Method(CS.Beyond.Gameplay.RemoteFactory.BlueprintConveyorEntry).Return(HL.Table) << function(self, entry)

    local spatialInfo = entry.spatial
    local gridPath = spatialInfo.gridPath
    local segInfos = {}
    local curStartPoint = Vector2(gridPath.startPoint.x, gridPath.startPoint.y)
    local curStartFace = spatialInfo.startFace
    for _, vector in pairs(gridPath.segments) do
        local length = vector.length
        if vector.face ~= curStartFace then

            local cornerInfo = {
                startPoint = curStartPoint,
                startFace = curStartFace,
                endFace = vector.face,
            }
            curStartFace = cornerInfo.endFace
            curStartPoint = curStartPoint + Face2Vector2[cornerInfo.endFace]
            table.insert(segInfos, cornerInfo)
            length = length - 1
        end

        if length > 0 then

            local straightInfo = {
                startPoint = curStartPoint,
                startFace = curStartFace,
                length = length,
            }
            table.insert(segInfos, straightInfo)
            curStartPoint = curStartPoint + straightInfo.length * Face2Vector2[curStartFace]
        end
    end
    return segInfos
end






BlueprintPreview.m_iconCells = HL.Field(HL.Forward('UIListCache'))

BlueprintPreview.m_changedIcons = HL.Field(HL.Table)

BlueprintPreview.m_curIconIndex = HL.Field(HL.Number) << -1

BlueprintPreview.m_iconInfos = HL.Field(HL.Table)

BlueprintPreview.m_curChangeIconTargetId = HL.Field(HL.Number) << -1


BlueprintPreview._InitChangeIconNode = HL.Method() << function(self)
    local node = self.view.changeIconNode
    self.m_iconCells = UIUtils.genCellCache(node.iconCell)
    node.autoCloseArea.onTriggerAutoClose:AddListener(function()
        self:_HideChangeIconNode()
    end)
    local ctrl = self:GetUICtrl()
    node.transform:SetParent(ctrl.view.transform.transform)
end

BlueprintPreview._ShowChangeIconNode = HL.Method(HL.Number) << function(self, targetId)
    if self.m_curChangeIconTargetId == targetId then
        self:_HideChangeIconNode()
        return
    end

    local node = self.view.changeIconNode
    node.gameObject:SetActive(true)
    node.animationWrapper:ClearTween(false)
    node.animationWrapper:PlayInAnimation()

    self.m_curChangeIconTargetId = targetId

    local cell = self.m_id2Cell[targetId]
    local info = self.m_showingCellDic[cell]
    local templateId = info.entry.templateId
    local bData = Tables.factoryBuildingTable[templateId]
    node.titleTxt.text = string.format(Language.LUA_FAC_BLUEPRINT_CHANGE_MACHINE_ICON_TITLE, bData.name)


    local iconMap = {
        [""] = {
            itemId = "",
            icon = bData.iconOnPanel,
            sortId1 = math.maxinteger,
        },
    }
    local _, curItemId = self:_GetTargetIconInfo(targetId)
    if not string.isEmpty(curItemId) and not FactoryUtils.isBlueprintProductIconGasEnv(curItemId) then

        local itemData = Tables.itemTable[curItemId]
        iconMap[curItemId] = {
            itemId = curItemId,
            icon = itemData.iconId,
            sortId1 = itemData.sortId1,
            sortId2 = itemData.sortId2,
            rarity = itemData.rarity,
        }
    end
    local _, formulaCpt = info.entry.info:TryGetValue(GEnums.FCComponentPos.FormulaMan)
    local currentMode
    if formulaCpt and not string.isEmpty(formulaCpt.currentMode) then
        currentMode = formulaCpt.currentMode
    end
    local craftInfos = FactoryUtils.getBuildingCrafts(templateId, nil, nil, currentMode)
    for _, cInfo in ipairs(craftInfos) do
        if cInfo.outcomes then
            for _, v in ipairs(cInfo.outcomes) do
                local itemId = v.id
                if not iconMap[itemId] then
                    local itemData = Tables.itemTable[itemId]
                    iconMap[itemId] = {
                        itemId = itemId,
                        icon = itemData.iconId,
                        sortId1 = itemData.sortId1,
                        sortId2 = itemData.sortId2,
                        rarity = itemData.rarity,
                    }
                end
            end
        end
    end
    if bData.type == GEnums.FacBuildingType.EnvGenWithActivator then
        local gasEntries = FactoryUtils.getEnvGenBlueprintGasProductIconEntries(templateId)
        for _, ge in ipairs(gasEntries) do
            if not iconMap[ge.itemId] then
                local gasSprite = FactoryUtils.blueprintProductIconGasEnvToSpriteName(ge.itemId)
                iconMap[ge.itemId] = {
                    itemId = ge.itemId,
                    icon = gasSprite,
                    sortId1 = ge.sortId1,
                    sortId2 = ge.sortId2,
                    isGasBlueprintIcon = true,
                }
            end
        end
        if not string.isEmpty(curItemId) and FactoryUtils.isBlueprintProductIconGasEnv(curItemId) and not iconMap[curItemId] then
            local gasSprite = FactoryUtils.blueprintProductIconGasEnvToSpriteName(curItemId)
            if gasSprite then
                iconMap[curItemId] = {
                    itemId = curItemId,
                    icon = gasSprite,
                    sortId1 = 0,
                    sortId2 = 0,
                    isGasBlueprintIcon = true,
                }
            end
        end
    end
    self.m_iconInfos = {}
    for _, v in pairs(iconMap) do
        table.insert(self.m_iconInfos, v)
    end
    table.sort(self.m_iconInfos, Utils.genSortFunction({ "sortId1", "sortId2", "itemId" }))

    local selectedCell = nil

    self.m_iconCells:Refresh(#self.m_iconInfos, function(iconCell, index)
        local iconInfo = self.m_iconInfos[index]
        if string.isEmpty(iconInfo.itemId) then


            iconCell.icon:InitItemIcon("item_gold")
            iconCell.icon.view.icon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_PANEL_ICON, iconInfo.icon)
            iconCell.rarityLine.gameObject:SetActive(false)
            iconCell.rarityLight.gameObject:SetActive(false)
        elseif iconInfo.isGasBlueprintIcon or FactoryUtils.isBlueprintProductIconGasEnv(iconInfo.itemId) then
            iconCell.icon:InitItemIcon("item_gold")
            iconCell.icon.view.icon:LoadSprite(UIConst.UI_SPRITE_FAC_GAS, iconInfo.icon or FactoryUtils.blueprintProductIconGasEnvToSpriteName(iconInfo.itemId))
            iconCell.rarityLine.gameObject:SetActive(false)
            iconCell.rarityLight.gameObject:SetActive(false)
        else
            iconCell.icon:InitItemIcon(iconInfo.itemId)
            iconCell.rarityLine.gameObject:SetActive(true)
            iconCell.rarityLight.gameObject:SetActive(true)
            local color = UIUtils.getItemRarityColor(iconInfo.rarity)
            iconCell.rarityLine.color = color
            iconCell.rarityLight.color = color
        end
        local abItemId = FactoryUtils.isBlueprintProductIconGasEnv(iconInfo.itemId) and "" or iconInfo.itemId
        self:_UpdateAbnormalType(iconCell, templateId, abItemId)

        iconCell.button.onClick:RemoveAllListeners()
        iconCell.button.onClick:AddListener(function()
            self:_OnClickIcon(index)
        end)
        local isSelected = iconInfo.itemId == curItemId
        if isSelected then
            self.m_curIconIndex = index
            selectedCell = iconCell
        end
        iconCell.stateController:SetState(isSelected and "Selected" or "Normal")

        iconCell.gameObject.name = "Icon_" .. iconInfo.itemId
    end)
    cell.selectedNode.gameObject:SetActive(true)

    local panelCtrl = self:GetUICtrl()
    UIUtils.updateTipsPosition(self.view.changeIconNode.transform, cell.transform, panelCtrl.view.transform, panelCtrl.uiCamera,
            UIConst.UI_TIPS_POS_TYPE.RightTop, {
                top = self.view.config.CHANGE_ICON_NODE_PADDING_VER.x,
                bottom = self.view.config.CHANGE_ICON_NODE_PADDING_VER.y,
                left = self.view.config.CHANGE_ICON_NODE_PADDING_HOR.x,
                right = self.view.config.CHANGE_ICON_NODE_PADDING_HOR.y,
            })
    node.autoCloseArea.tmpSafeArea = cell.transform

    LayoutRebuilder.ForceRebuildLayoutImmediate(node.scorllRect.content)

    if DeviceInfo.usingController then
        self.mouseShow = false
        self.view.leftBottomNode.gameObject:SetActive(false)
        self:_CancelHover()

        node.selectableNaviGroup.useDefaultTargetOnFocus = false
        node.selectableNaviGroup:SetLayerSelectedTarget(selectedCell.button, false)
        node.selectableNaviGroup:ManuallyFocus()

        node.scorllRect:AutoScrollToRectTransform(selectedCell.gameObject.transform, true)
    else
        node.scorllRect:AutoScrollToRectTransform(selectedCell.gameObject.transform, true)
    end
end


BlueprintPreview._OnClickIcon = HL.Method(HL.Number) << function(self, index)
    if index == self.m_curIconIndex then
        return
    end

    local oldCell = self.m_iconCells:Get(self.m_curIconIndex)
    oldCell.stateController:SetState("Normal")
    self.m_curIconIndex = index
    local newCell = self.m_iconCells:Get(self.m_curIconIndex)
    newCell.stateController:SetState("Selected")

    self.m_changedIcons[self.m_curChangeIconTargetId] = self.m_iconInfos[index].itemId
    self:_UpdateTargetIcon(self.m_curChangeIconTargetId)

    self:_OnEnvGenIconChanged(self.m_curChangeIconTargetId)
end

BlueprintPreview._HideChangeIconNode = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAni)
    local node = self.view.changeIconNode
    if not skipAni then
        if node.animationWrapper.curState == CS.Beyond.UI.UIConst.AnimationState.Out then

            return
        end
    end

    if skipAni then
        node.animationWrapper:ClearTween(false)
        node.gameObject:SetActive(false)
    else
        UIUtils.PlayAnimationAndToggleActive(node.animationWrapper, false)
    end
    self.mouseShow = true
    self.view.leftBottomNode.gameObject:SetActive(true)
    if self.m_curChangeIconTargetId > 0 then
        local cell = self.m_id2Cell[self.m_curChangeIconTargetId]
        self.m_curChangeIconTargetId = -1
        cell.selectedNode.gameObject:SetActive(false)
    end
end

BlueprintPreview._GetTargetIconInfo = HL.Method(HL.Number).Return(HL.Opt(HL.String, HL.String, HL.Number)) << function(self, targetId)
    local itemId = self.m_changedIcons[targetId]
    local cell = self.m_id2Cell[targetId]
    local info = self.m_showingCellDic[cell]
    if not itemId then
        itemId = info.entry.productIcon
    end
    if string.isEmpty(itemId) then

        if info.type == NodeType.Building then
            local data = Tables.factoryBuildingTable[info.entry.templateId]
            return data.iconOnPanel, ""
        end
        return
    end
    if FactoryUtils.isBlueprintProductIconGasEnv(itemId) then
        local gasSprite = FactoryUtils.blueprintProductIconGasEnvToSpriteName(itemId)
        if gasSprite then
            return gasSprite, itemId, 0
        end
        if info.type == NodeType.Building then
            local data = Tables.factoryBuildingTable[info.entry.templateId]
            return data.iconOnPanel, ""
        end
        return
    end
    local itemData = Tables.itemTable[itemId]
    return itemData.iconId, itemId, itemData.rarity
end

BlueprintPreview._UpdateTargetIcon = HL.Method(HL.Number) << function(self, targetId)
    local cell = self.m_id2Cell[targetId]
    local info = self.m_showingCellDic[cell]
    local icon, itemId, rarity = self:_GetTargetIconInfo(targetId)
    local node = cell.iconNode
    node.machineIcon.gameObject:SetActive(false)
    node.emptyNode.gameObject:SetActive(false)
    node.itemNode.gameObject:SetActive(false)
    if icon == nil then
        node.emptyNode.gameObject:SetActive(true)
    elseif FactoryUtils.isBlueprintProductIconGasEnv(itemId) then
        node.itemNode.gameObject:SetActive(true)
        node.itemIcon:InitItemIcon("item_gold")
        node.itemIcon.view.icon:LoadSprite(UIConst.UI_SPRITE_FAC_GAS, icon)
        node.rarityIcon.gameObject:SetActiveIfNecessary(false)
        self:_UpdateAbnormalType(node, info.entry.templateId, "")
    elseif string.isEmpty(itemId) then
        node.machineIcon.gameObject:SetActive(true)
        node.machineIcon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_PANEL_ICON, icon)
    else
        node.itemNode.gameObject:SetActive(true)
        node.itemIcon:InitItemIcon(itemId)
        node.rarityIcon.gameObject:SetActiveIfNecessary(true)
        local color = UIUtils.getItemRarityColor(rarity)
        node.rarityIcon.color = color
        self:_UpdateAbnormalType(node, info.entry.templateId, itemId)
    end
    node.changeHint.gameObject:SetActive(info.canChangeIcon)
end

BlueprintPreview._UpdateAbnormalType = HL.Method(HL.Any, HL.String, HL.Opt(HL.String)) << function(self, node, machineId, itemId)
    local iconAbnormalType
    if Utils.isInBlackbox() or not self.m_bpAbnormalIconHelper then
        iconAbnormalType = FacConst.FAC_BP_ABNORMAL_ICON_TYPE.Normal
    elseif string.isEmpty(itemId) then
        iconAbnormalType = FacConst.FAC_BP_ABNORMAL_ICON_TYPE.Normal
    else
        iconAbnormalType = self.m_bpAbnormalIconHelper.GetAbnormalType(machineId, itemId)
    end
    node.lockedNode.gameObject:SetActive(iconAbnormalType == FacConst.FAC_BP_ABNORMAL_ICON_TYPE.Locked)
    node.timeLimitedExpiredNode.gameObject:SetActive(iconAbnormalType == FacConst.FAC_BP_ABNORMAL_ICON_TYPE.TimeLimitedExpired)
    node.timeLimitedActiveNode.gameObject:SetActive(iconAbnormalType == FacConst.FAC_BP_ABNORMAL_ICON_TYPE.TimeLimitedActive)
    FactoryUtils.setTimeLimitedItemTagColor(node.timeLimitedColorTag, itemId)
end

BlueprintPreview.ApplyIconChanges = HL.Method() << function(self)
    for targetId, itemId in pairs(self.m_changedIcons) do
        local cell = self.m_id2Cell[targetId]
        local info = self.m_showingCellDic[cell]
        info.entry.productIcon = itemId
    end
end

BlueprintPreview.GetChangedIcons = HL.Method().Return(HL.Opt(HL.Table)) << function(self)
    if not self:HasIconChanged() then
        return
    end
    local dic = {}
    for targetId, newIcon in pairs(self.m_changedIcons) do
        local cell = self.m_id2Cell[targetId]
        local info = self.m_showingCellDic[cell]
        dic[info.entry.nodeId] = newIcon
    end
    return dic
end

BlueprintPreview.HasIconChanged = HL.Method().Return(HL.Boolean) << function(self)
    return next(self.m_changedIcons) ~= nil
end












BlueprintPreview._CollectEnvGenCoverInfos = HL.Method() << function(self)
    self.m_envGenCoverInfos = {}
    for _, info in pairs(self.m_showingCellDic) do
        if info.type == NodeType.Building then
            local templateId = info.entry.templateId
            local isBuilding, bData = Tables.factoryBuildingTable:TryGetValue(templateId)
            if isBuilding and bData.type == GEnums.FacBuildingType.EnvGenWithActivator then
                local ok, vapoData = Tables.factoryVaporizerTable:TryGetValue(templateId)
                if ok then
                    local extend = vapoData.rangeExtend
                    local minX, minY, w, d = GetBuildingGridRect(info.entry, bData, extend.x, extend.z)
                    table.insert(self.m_envGenCoverInfos, {
                        targetId = info.id,
                        minX = minX,
                        minY = minY,
                        w = w,
                        d = d,
                    })
                end
            end
        end
    end
end




BlueprintPreview._RefreshAllEnvReceiverIcons = HL.Method() << function(self)
    for _, info in pairs(self.m_showingCellDic) do
        if info.type == NodeType.Building and FactoryUtils.isEnvReceiverBuilding(info.entry.templateId) then
            self:_UpdateEnvReceiverIcon(info.id)
        end
    end
end


BlueprintPreview._UpdateEnvReceiverIcon = HL.Method(HL.Number) << function(self, targetId)
    local cell = self.m_id2Cell[targetId]
    local info = self.m_showingCellDic[cell]
    local gasSprite = self:_ResolveEnvReceiverGasSprite(info)
    if gasSprite then
        cell.envNode.gameObject:SetActive(true)
        cell.envIcon:LoadSprite(UIConst.UI_SPRITE_FAC_GAS, gasSprite)
    else
        cell.envNode.gameObject:SetActive(false)
    end
end



BlueprintPreview._ResolveEnvReceiverGasSprite = HL.Method(HL.Table).Return(HL.Opt(HL.String)) << function(self, info)
    if self.m_envGenCoverInfos == nil then
        return nil
    end
    local _, bData = Tables.factoryBuildingTable:TryGetValue(info.entry.templateId)
    if not bData then
        return nil
    end
    local devMinX, devMinY, devW, devD = GetBuildingGridRect(info.entry, bData)
    local devMaxX = devMinX + devW
    local devMaxY = devMinY + devD
    for _, cover in ipairs(self.m_envGenCoverInfos) do

        if devMinX >= cover.minX and devMaxX <= cover.minX + cover.w
            and devMinY >= cover.minY and devMaxY <= cover.minY + cover.d then
            local _, sgItemId = self:_GetTargetIconInfo(cover.targetId)
            if not string.isEmpty(sgItemId) and FactoryUtils.isBlueprintProductIconGasEnv(sgItemId) then

                return FactoryUtils.blueprintProductIconGasEnvToEffectedSpriteName(sgItemId)
            end
            return nil
        end
    end
    return nil
end



BlueprintPreview._OnEnvGenIconChanged = HL.Method(HL.Number) << function(self, targetId)
    local cell = self.m_id2Cell[targetId]
    local info = cell and self.m_showingCellDic[cell]
    if not info or info.type ~= NodeType.Building then
        return
    end
    local isBuilding, bData = Tables.factoryBuildingTable:TryGetValue(info.entry.templateId)
    if isBuilding and bData.type == GEnums.FacBuildingType.EnvGenWithActivator then
        self:_RefreshAllEnvReceiverIcons()
    end
end






BlueprintPreview.m_curHoverTargetId = HL.Field(HL.Any)

BlueprintPreview.m_curHoverTargetId2 = HL.Field(HL.Any)








BlueprintPreview._TryGetUdpipeConnection = HL.Method(HL.Number).Return(HL.Boolean, HL.Boolean, HL.Opt(HL.Number)) << function(self, targetId)
    local cell = self.m_id2Cell[targetId]
    local info = cell and self.m_showingCellDic[cell]

    if not info or (info.type ~= NodeType.Building and info.type ~= NodeType.Logistic) then
        return false, false, nil
    end

    if not info.entry:IsUdpipe() then
        return false, false, nil
    end

    local peerNodeId = info.entry:GetUdpipeConnectedNodeId()
    if peerNodeId == 0 then
        return true, false, nil
    end


    local peerTargetId = self.m_nodeIdToTargetId[peerNodeId]
    if not peerTargetId then
        return true, false, nil
    end
    return true, true, peerTargetId
end





BlueprintPreview._SetUdpipeCellSelected = HL.Method(HL.Number, HL.Boolean) << function(self, targetId, isSelected)
    local cell = self.m_id2Cell[targetId]
    if not cell then
        return
    end
    cell.udpipeStateStateController:SetState(isSelected and "Selected" or "NotSelected")
end





BlueprintPreview._UpdatePipeConnectionLine = HL.Method() << function(self)
    if not self.m_pinnedUdpipeTargetId or not self.m_pinnedUdpipePeerTargetId then
        self.view.pipeConnectionLine.gameObject:SetActive(false)
        return
    end
    local cell1 = self.m_id2Cell[self.m_pinnedUdpipeTargetId]
    local cell2 = self.m_id2Cell[self.m_pinnedUdpipePeerTargetId]
    local info1 = self.m_showingCellDic[cell1]
    local cell1IsLoader = FacConst.UDPIPE_PORT_LOAD_TYPE_MAP[info1.entry.templateId]
    local fromCell = cell1IsLoader and cell1 or cell2
    local toCell = cell1IsLoader and cell2 or cell1

    local fromCenter = fromCell.transform.anchoredPosition
    local toCenter = toCell.transform.anchoredPosition
    local delta = toCenter - fromCenter
    local len = delta.magnitude
    local pl = self.view.pipeConnectionLine.transform
    pl.gameObject:SetActive(true)

    pl.anchorMin = Vector2.zero
    pl.anchorMax = Vector2.zero
    pl.anchoredPosition = fromCenter

    pl.sizeDelta = Vector2(len, pl.sizeDelta.y)
    pl.localEulerAngles = Vector3(0, 0, math.deg(math.atan(delta.y, delta.x)))

    pl:SetAsLastSibling()
end


BlueprintPreview._PinUdpipe = HL.Method(HL.Number, HL.Number) << function(self, targetId, peerTargetId)
    if self.m_pinnedUdpipeTargetId then

        self:_SetUdpipeCellSelected(self.m_pinnedUdpipeTargetId, false)
        self:_SetUdpipeCellSelected(self.m_pinnedUdpipePeerTargetId, false)
    end
    self.m_pinnedUdpipeTargetId = targetId
    self.m_pinnedUdpipePeerTargetId = peerTargetId
    self:_SetUdpipeCellSelected(targetId, true)
    self:_SetUdpipeCellSelected(peerTargetId, true)
    self:_UpdatePipeConnectionLine()
    self:_RefreshViewConnectionHint()
end


BlueprintPreview._UnpinUdpipe = HL.Method() << function(self)
    if not self.m_pinnedUdpipeTargetId then
        return
    end
    self:_SetUdpipeCellSelected(self.m_pinnedUdpipeTargetId, false)
    self:_SetUdpipeCellSelected(self.m_pinnedUdpipePeerTargetId, false)
    self.m_pinnedUdpipeTargetId = nil
    self.m_pinnedUdpipePeerTargetId = nil
    self.view.pipeConnectionLine.gameObject:SetActive(false)
    self:_RefreshViewConnectionHint()
end






BlueprintPreview._RefreshViewConnectionHint = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    if self.m_viewConnBindingId <= 0 then
        return
    end
    local enable = false
    local targetId = self.m_curHoverTargetId
    if targetId then
        local isUdpipe, isConnected = self:_TryGetUdpipeConnection(targetId)
        enable = isUdpipe and isConnected
    end
    InputManagerInst:ToggleBinding(self.m_viewConnBindingId, enable)
    if enable then

        local isPinnedPair = (targetId == self.m_pinnedUdpipeTargetId)
            or (targetId == self.m_pinnedUdpipePeerTargetId)
        InputManagerInst:SetBindingText(self.m_viewConnBindingId,
            isPinnedPair and Language.LUA_BLUEPRINT_PREVIEW_UDPIPE_KEYHINT_HIDE
                or Language.LUA_BLUEPRINT_PREVIEW_UDPIPE_KEYHINT_SHOW)
    end
end






BlueprintPreview._SetCurrentHoveredUdpipe = HL.Method(HL.Opt(HL.Number)) << function(self, newTargetId)
    if self.m_lastHoveredUdpipeTargetId == newTargetId then
        return
    end
    if not DeviceInfo.usingTouch then
        local oldTargetId = self.m_lastHoveredUdpipeTargetId
        if oldTargetId then
            local oldCell = self.m_id2Cell[oldTargetId]
            if oldCell then oldCell.udpipeState:PlayOutAnimation() end
        end
        if newTargetId then
            local newCell = self.m_id2Cell[newTargetId]
            if newCell then newCell.udpipeState:PlayInAnimation() end
        end
    end
    self.m_lastHoveredUdpipeTargetId = newTargetId
end


BlueprintPreview._UpdateHoverPos = HL.Method() << function(self)
    if not DeviceInfo.usingController and not self.view.maskBtnArea.pointerInArea then

        if self.m_curHoverTargetId then
            self:_CancelHover()
        end
        return
    end
    if DeviceInfo.usingController then
        if not self.mouseShow then
            self.view.controllerMouse.gameObject:SetActive(false)
            return
        end
        self.view.controllerMouse.gameObject:SetActive(true)
        local stickValue = InputManagerInst:GetGamepadStickValue(true)
        local moveDelta = stickValue * self.view.config.CONTROLLER_MOVE_SPEED * Time.deltaTime
        local targetPosition = self.view.controllerMouse.anchoredPosition + moveDelta
        local targetNormalizedPosition = self.view.scrollRect.normalizedPosition
        if targetPosition.x <= self.view.config.CONTROLLER_PADDING and self.m_widthScale > 0 then
            targetNormalizedPosition.x = lume.clamp(self.view.scrollRect.normalizedPosition.x - self.view.config.CONTROLLER_ROLL_SPEED * Time.deltaTime / self.m_widthScale,0,1)
        elseif targetPosition.x >= self.view.scrollRectRectTransform.rect.size.x - self.view.config.CONTROLLER_PADDING and self.m_widthScale > 0 then
            targetNormalizedPosition.x = lume.clamp(self.view.scrollRect.normalizedPosition.x + self.view.config.CONTROLLER_ROLL_SPEED * Time.deltaTime / self.m_widthScale,0,1)
        end
        if targetPosition.y <= self.view.config.CONTROLLER_PADDING and self.m_heightScale > 0 then
            targetNormalizedPosition.y = lume.clamp(self.view.scrollRect.normalizedPosition.y - self.view.config.CONTROLLER_ROLL_SPEED * Time.deltaTime / self.m_heightScale,0,1)
        elseif targetPosition.y >= self.view.scrollRectRectTransform.rect.size.y - self.view.config.CONTROLLER_PADDING and self.m_heightScale > 0 then
            targetNormalizedPosition.y = lume.clamp(self.view.scrollRect.normalizedPosition.y + self.view.config.CONTROLLER_ROLL_SPEED * Time.deltaTime / self.m_heightScale,0,1)
        end
        self.view.scrollRect.normalizedPosition = targetNormalizedPosition

        if stickValue ~= Vector2.zero then
            targetPosition.x = lume.clamp(targetPosition.x,self.view.config.CONTROLLER_PADDING,self.view.scrollRectRectTransform.rect.size.x - self.view.config.CONTROLLER_PADDING)
            targetPosition.y = lume.clamp(targetPosition.y,self.view.config.CONTROLLER_PADDING,self.view.scrollRectRectTransform.rect.size.y - self.view.config.CONTROLLER_PADDING)
            self.view.controllerMouse.anchoredPosition = targetPosition
        end
    end


    local mousePos = InputManager.mousePosition:XY()
    local rect = CSUtils.RectTransformToScreenRect(self.view.content.transform, self:GetUICtrl().uiCamera)
    local relativePos = mousePos - rect.min
    local gridScreenSize = rect.width / self.m_csBP.sourceRect.width
    local gridPos = relativePos / gridScreenSize
    gridPos = Vector2(math.floor(gridPos.x), math.floor(gridPos.y))
    local id1, id2 = self.m_previewHelper:GetGridValue(gridPos.x, gridPos.y)
    if id1 == 0 then

        if self.m_curHoverTargetId then
            self:_CancelHover()
        end
        self.view.controllerEditBtn.interactable = false
        self:_RefreshViewConnectionHint()
        return
    end


    local cell1 = self.m_id2Cell[id1]
    local info1 = self.m_showingCellDic[cell1]
    local isConveyor = info1.type == NodeType.Belt or info1.type == NodeType.Pipe
    if not isConveyor and id1 == self.m_curHoverTargetId and id2 == self.m_curHoverTargetId2 then

        return
    end

    self.m_curHoverTargetId = id1
    if id2 == 0 then
        self.m_curHoverTargetId2 = nil
    else
        local cell2 = self.m_id2Cell[id2]
        local info2 = self.m_showingCellDic[cell2]
        if info2.isHighLayer then
            self.m_curHoverTargetId = id2
            self.m_curHoverTargetId2 = id1
            isConveyor = info2.type == NodeType.Belt or info2.type == NodeType.Pipe
            cell1, info1 = cell2, info2
        else
            self.m_curHoverTargetId2 = id2
        end
    end
    self:_UpdateHoverTips()

    self.view.hoverHint.gameObject:SetActive(true)
    self.view.hoverTipsNode.gameObject:SetActive(true)

    if isConveyor then
        self.view.hoverHint.transform.sizeDelta = Vector2(uiSizePerUnit, uiSizePerUnit)
        self.view.hoverHint.transform.anchoredPosition = Vector2(math.floor(gridPos.x) + 0.5, math.floor(gridPos.y) + 0.5) * uiSizePerUnit
    else
        self.view.hoverHint.transform.sizeDelta = cell1.transform.sizeDelta
        self.view.hoverHint.transform.anchoredPosition = cell1.transform.anchoredPosition
    end


    local isUdpipe, isConnected = self:_TryGetUdpipeConnection(self.m_curHoverTargetId)
    self:_SetCurrentHoveredUdpipe(isUdpipe and self.m_curHoverTargetId or nil)


    self.view.controllerEditBtn.interactable = info1.canChangeIcon
    self:_RefreshViewConnectionHint()
end

BlueprintPreview._CancelHover = HL.Method() << function(self)
    self.m_curHoverTargetId = nil
    self.m_curHoverTargetId2 = nil
    self.view.hoverHint.gameObject:SetActive(false)
    self.view.hoverTipsNode.gameObject:SetActive(false)

    self:_SetCurrentHoveredUdpipe(nil)

    self:_RefreshViewConnectionHint()


end

BlueprintPreview._OnClick = HL.Method() << function(self)
    self:_UpdateHoverPos()


    if not self.m_curHoverTargetId then
        return
    end

    local clickedTargetId = self.m_curHoverTargetId
    local info = self.m_showingCellDic[self.m_id2Cell[clickedTargetId]]


    if self.m_canEdit and info.canChangeIcon then
        self:_ShowChangeIconNode(clickedTargetId)
        return
    end


    self:_ToggleUdpipePin(clickedTargetId)
end



BlueprintPreview._ToggleUdpipePin = HL.Method(HL.Number) << function(self, clickedTargetId)
    local isUdpipe, isConnected, peerTargetId = self:_TryGetUdpipeConnection(clickedTargetId)
    if not (isUdpipe and isConnected) then
        return
    end
    if self.m_pinnedUdpipeTargetId == clickedTargetId or self.m_pinnedUdpipePeerTargetId == clickedTargetId then

        self:_UnpinUdpipe()
    else

        self:_PinUdpipe(clickedTargetId, peerTargetId)
    end
end



BlueprintPreview._OnControllerViewConnection = HL.Method() << function(self)
    self:_UpdateHoverPos()
    if not self.m_curHoverTargetId then
        return
    end
    self:_ToggleUdpipePin(self.m_curHoverTargetId)
end

BlueprintPreview._UpdateHoverTips = HL.Method() << function(self)
    self:_UpdateSingleHoverTips(self.view.hoverTipsCell, self.m_curHoverTargetId)
    self:_UpdateSingleHoverTips(self.view.hoverTipsCell2, self.m_curHoverTargetId2)
end

BlueprintPreview._UpdateSingleHoverTips = HL.Method(HL.Table, HL.Any) << function(self, cell, targetId)
    if not targetId then
        cell.m_curTargetId = nil
        cell.gameObject.gameObject:SetActive(false)
        return
    end
    local oriActive = cell.gameObject.activeInHierarchy
    cell.gameObject.gameObject:SetActive(true)
    if cell.m_curTargetId == targetId then
        if not oriActive then
            AudioAdapter.PostEvent("Au_UI_Popup_CommonHoverTipPanel_Open")
        end
        return
    end
    cell.m_curTargetId = targetId
    AudioAdapter.PostEvent("Au_UI_Popup_CommonHoverTipPanel_Open")

    local nodeCell = self.m_id2Cell[targetId]
    local info = self.m_showingCellDic[nodeCell]
    local templateId = info.entry.templateId
    local icon, name, itemId
    if info.type == NodeType.Building then
        local data = Tables.factoryBuildingTable[templateId]
        icon = data.iconOnPanel
        name = data.name
        itemId = FactoryUtils.getBuildingItemId(templateId)
    elseif info.type == NodeType.Logistic then
        local data = FactoryUtils.getLogisticData(templateId)
        icon = data.iconOnPanel
        name = data.name
        itemId = data.itemId
    elseif info.type == NodeType.Belt then
        local data = Tables.factoryGridBeltTable[templateId]
        icon = data.beltData.iconOnPanel
        name = data.beltData.name
        itemId = data.beltData.itemId
    elseif info.type == NodeType.Pipe then
        local data = Tables.factoryLiquidPipeTable[templateId]
        icon = data.pipeData.iconOnPanel
        name = data.pipeData.name
        itemId = data.pipeData.itemId
    end

    cell.icon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_PANEL_ICON, icon)
    cell.nameTxt.text = name
    local itemData = Tables.itemTable[itemId]
    cell.rarityLine.color = UIUtils.getItemRarityColor(itemData.rarity)


    local isUdpipe, isConnected, _ = self:_TryGetUdpipeConnection(targetId)
    cell.connectionStatusNode.gameObject:SetActive(isUdpipe)
    if isUdpipe then
        cell.connectionStatusNode:SetState(isConnected and "Connected" or "Disconnected")
    end
end







BlueprintPreview._OnEnable = HL.Override() << function(self)
    if self.m_updateId < 0 then
        self.m_updateId = LuaUpdate:Add("TailTick", function()
            self:_UpdateHoverPos()
        end)
    end
end

BlueprintPreview._OnDisable = HL.Override() << function(self)
    if self.m_updateId then
        self.m_updateId = LuaUpdate:Remove(self.m_updateId)
    end
end

BlueprintPreview._OnDestroy = HL.Override() << function(self)
    if self.m_updateId then
        self.m_updateId = LuaUpdate:Remove(self.m_updateId)
    end
end

BlueprintPreview._GetNextTargetId = HL.Method().Return(HL.Number) << function(self)
    local id = self.m_nextTargetId
    self.m_nextTargetId = self.m_nextTargetId + 1
    return id
end

BlueprintPreview._PrepareCellImgCache = HL.Method(HL.Any) << function(self, cell)
    if not cell.m_imgCache then
        cell.m_imgCache = CommonCache(function()
            local obj = CSUtils.CreateObject(cell.image.gameObject, cell.image.transform.parent)
            return obj.gameObject:GetComponent("UIImage")
        end, function(img)
            img.gameObject:SetActive(true)
        end, function(img)
            img.gameObject:SetActive(false)
        end)
        cell.m_imgCache:Cache(cell.image)
        cell.m_showingImgs = {}
    else
        for _, img in ipairs(cell.m_showingImgs) do
            cell.m_imgCache:Cache(img)
        end
        cell.m_showingImgs = {}
    end
end




HL.Commit(BlueprintPreview)
return BlueprintPreview
