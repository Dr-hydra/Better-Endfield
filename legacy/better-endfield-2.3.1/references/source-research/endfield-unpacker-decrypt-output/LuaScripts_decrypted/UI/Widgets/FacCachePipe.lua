local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FacCachePipe = HL.Class('FacCachePipe', UIWidgetBase)

local ARROW_ANIMATION_NAME_DEFAULT = "pipecell_decoarrow_defult%d"
local ARROW_ANIMATION_NAME_LOOP = "pipecell_decoarrow_loop%d"
local ITEM_CHANGED_ANIMATION_NAME = "pipe_item_changed%d"
local BLOCK_ANIMATION_NAME_IN = "pipecell_blocknode_in"
local BLOCK_ANIMATION_NAME_OUT = "pipecell_blocknode_out"

local ARROW_ANIMATION_OVERRIDE_NAME_DEFAULT = "OVERRIDE_PIPECELL_DECOARROW_DEFULT%d"
local ARROW_ANIMATION_OVERRIDE_NAME_LOOP = "OVERRIDE_PIPECELL_DECOARROW_LOOP%d"
local ITEM_CHANGED_ANIMATION_OVERRIDE_NAME = "OVERRIDE_PIPE_ITEM_CHANGED%d"

local MESSAGE_ITEM_INDEX = 0
local SINGLE_ANIM_INDEX_OFFSET = 4

local SINGLE_LIQUID_CACHE_SLOT_SPACING_Y = 114
local MULTI_LIQUID_CACHE_SLOT_SPACING_Y = 204
local SINGLE_REPO_PIPE_COLOR = "EEEEEE"
local MULTI_REPO_PIPE_COLOR_1 = "B1773D"
local MULTI_REPO_PIPE_COLOR_2 = "A29D3E"

FacCachePipe.m_buildingInfo = HL.Field(HL.Userdata)

FacCachePipe.m_buildingNodeId = HL.Field(HL.Number) << -1

FacCachePipe.m_inPipeInfoList = HL.Field(HL.Table)

FacCachePipe.m_outPipeInfoList = HL.Field(HL.Table)


FacCachePipe.m_needInversePipe = HL.Field(HL.Boolean) << false

FacCachePipe.m_inBindingPipeDataMap = HL.Field(HL.Table)

FacCachePipe.m_outBindingPipeDataMap = HL.Field(HL.Table)

FacCachePipe.m_inPipeList = HL.Field(HL.Table)

FacCachePipe.m_outPipeList = HL.Field(HL.Table)

FacCachePipe.m_isInPipeMode = HL.Field(HL.Boolean) << false

FacCachePipe.m_useSinglePipe = HL.Field(HL.Boolean) << false

FacCachePipe.m_needModeSwitch = HL.Field(HL.Boolean) << false

FacCachePipe.m_cachedSprite = HL.Field(HL.Table)

FacCachePipe.m_isInSingleState = HL.Field(HL.Boolean) << false

FacCachePipe.m_stateRefreshCallback = HL.Field(HL.Function)

FacCachePipe.m_needRefreshPortState = HL.Field(HL.Boolean) << false


FacCachePipe._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_CONVEYOR_CHANGE, function(args)
        local bindingNodeId, componentId, isIn, itemList = unpack(args)
        local itemId = (itemList ~= nil and itemList.Count > 0) and itemList[MESSAGE_ITEM_INDEX] or ""
        self:_RefreshPipeCellConveyorAnimation(isIn, bindingNodeId, componentId, itemId)
    end)

    self:RegisterMessage(MessageConst.ON_PORT_BLOCK_STATE_CHANGE, function(args)
        local buildingNodeId = unpack(args)
        self:_RefreshPipeCellBlockState(buildingNodeId)
    end)
end

FacCachePipe._OnDestroy = HL.Override() << function(self)
    self:_UnRegisterInterested()
    self.m_cachedSprite = nil
end

FacCachePipe._OnEnable = HL.Override() << function(self)
    if self.m_needRefreshPortState then
        self.m_needRefreshPortState = false
        self:RefreshPipeCellsState()
    end
end

FacCachePipe._OnDisable = HL.Override() << function(self)
    self.m_needRefreshPortState = true
end





FacCachePipe.InitFacCachePipe = HL.Method(HL.Userdata, HL.Opt(HL.Table)) << function(self, buildingInfo, customInfo)
    if buildingInfo == nil then
        return
    end
    self.m_buildingInfo = buildingInfo
    self.m_buildingNodeId = buildingInfo.nodeId
    self.m_cachedSprite = {}

    self:_ParseCustomInfo(customInfo)
    self:_RefreshCachePipe()
    self:_FirstTimeInit()
end

FacCachePipe._ParseCustomInfo = HL.Method(HL.Table) << function(self, customInfo)
    if customInfo == nil then
        return
    end

    self.m_needInversePipe = customInfo.needInversePipe or false
    self.m_useSinglePipe = customInfo.useSinglePipe or false
    self.m_needModeSwitch = customInfo.needModeSwitch or false
    self.m_stateRefreshCallback = customInfo.stateRefreshCallback or function()end
end

FacCachePipe._RefreshCachePipe = HL.Method() << function(self)
    if self.m_useSinglePipe then
        self.m_isInPipeMode = true
    else
        if self.m_needModeSwitch then

            if self.m_buildingInfo.formulaMan == nil then
                return
            end

            local isInPipeMode = self.m_buildingInfo.formulaMan.currentMode ~= FacConst.FAC_FORMULA_MODE_MAP.NORMAL
            self.m_isInPipeMode = isInPipeMode
        else
            self.m_isInPipeMode = true
        end
    end

    if self.m_isInPipeMode then
        self:_GetPipeInfoList()
        self:_InitPipeList()
        self:_RegisterInterested()

        self.view.gameObject:SetActive(true)
    else
        self.view.gameObject:SetActive(false)
    end
end

FacCachePipe._GetPipeInfoList = HL.Method() << function(self)
    self.m_inPipeInfoList, self.m_outPipeInfoList = FactoryUtils.getBuildingPortState(self.m_buildingNodeId, true)


    if self.m_needInversePipe and self.m_inPipeInfoList ~= nil and #self.m_inPipeInfoList > 1 then
        local temp = self.m_inPipeInfoList[1]
        self.m_inPipeInfoList[1] = self.m_inPipeInfoList[2]
        self.m_inPipeInfoList[2] = temp
        temp = self.m_inPipeInfoList[1].index
        self.m_inPipeInfoList[1].index = self.m_inPipeInfoList[2].index
        self.m_inPipeInfoList[2].index = temp
    end
    if self.m_needInversePipe and self.m_outPipeInfoList ~= nil and #self.m_outPipeInfoList > 1 then
        local temp = self.m_outPipeInfoList[1]
        self.m_outPipeInfoList[1] = self.m_outPipeInfoList[2]
        self.m_outPipeInfoList[2] = temp
        temp = self.m_outPipeInfoList[1].index
        self.m_outPipeInfoList[1].index = self.m_outPipeInfoList[2].index
        self.m_outPipeInfoList[2].index = temp
    end
end

FacCachePipe._GetItemSprite = HL.Method(HL.String).Return(HL.Userdata) << function(self, itemId)
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

FacCachePipe._GetIsPipeBlocked = HL.Method(HL.Number, HL.Boolean).Return(HL.Boolean) << function(self, nodeId, isIn)
    local infoList = isIn and self.m_inPipeInfoList or self.m_outPipeInfoList
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



FacCachePipe._InitPipeList = HL.Method() << function(self)
    if self.m_useSinglePipe then
        self.m_inPipeList = { self.view.singleInCell }
        self.m_outPipeList = { self.view.singleOutCell }
    else
        self.m_inPipeList = {
            self.view.pipeCell1,
            self.view.pipeCell2,
        }
        self.m_outPipeList = {
            self.view.pipeCell3,
            self.view.pipeCell4,
        }

        if self.view.expendPipeIn ~= nil then
            table.insert(self.m_inPipeList, self.view.expendPipeIn)
        end
        if self.view.expendPipeOut ~= nil then
            table.insert(self.m_inPipeList, self.view.expendPipeOut)
        end
    end

    self.m_outPipeList = {
        self.m_useSinglePipe and self.view.singleOutCell or self.view.pipeCell3
    }
    if not self.m_useSinglePipe then
        table.insert(self.m_outPipeList, self.view.pipeCell4)
    end
    self.m_inBindingPipeDataMap = {}
    self.m_outBindingPipeDataMap = {}

    local pipeList = lume.concat(self.m_inPipeList, self.m_outPipeList)

    for _, pipeCell in ipairs(pipeList) do
        pipeCell.gameObject:SetActive(false)
    end

    for index, inPipeInfo in ipairs(self.m_inPipeInfoList) do
        local inPipe
        if inPipeInfo.index ~= nil then
            inPipe = self.m_inPipeList[LuaIndex(inPipeInfo.index)]
            if inPipe == nil then
                inPipe = self.m_inPipeList[index]
            end
        end
        inPipe.gameObject:SetActive(true)
        self:_InitPipeCell(inPipe, inPipeInfo, self.m_inBindingPipeDataMap)
    end
    for index, outPipeInfo in ipairs(self.m_outPipeInfoList) do
        local outPipe
        if outPipeInfo.index ~= nil then
            outPipe = self.m_outPipeList[LuaIndex(outPipeInfo.index)]
            if outPipe == nil then
                outPipe = self.m_outPipeList[index]
            end
        end
        outPipe.gameObject:SetActive(true)
        self:_InitPipeCell(outPipe, outPipeInfo, self.m_outBindingPipeDataMap)
    end
end

FacCachePipe._InitPipeCell = HL.Method(HL.Any, HL.Table, HL.Table) << function(self, cell, info, bindingMap)
    if cell == nil or info == nil then
        return
    end

    self:_RefreshPipeCellState(cell, info)
    if not self.m_useSinglePipe then
        local pipeState = self.view.config.USE_BOLD_PIPE and "BoldState" or "NormalState"
        cell.stateController:SetState(pipeState)
    end

    if info.isBinding then
         bindingMap[info.touchNodeId] = {
             pipeCell = cell,
             touchCompId = info.touchCompId,
         }
    end
end






FacCachePipe.m_registered = HL.Field(HL.Boolean) << false

FacCachePipe._RegisterInterested = HL.Method() << function(self)
    if self.m_registered then
        self:_UnRegisterInterested()
    end

    if self.m_inBindingPipeDataMap ~= nil then
        for nodeId, _ in pairs(self.m_inBindingPipeDataMap) do
            GameInstance.remoteFactoryManager:RegisterInterestedUnitId(nodeId)
        end
    end

    if self.m_outBindingPipeDataMap ~= nil then
        for nodeId, _ in pairs(self.m_outBindingPipeDataMap) do
            GameInstance.remoteFactoryManager:RegisterInterestedUnitId(nodeId)
        end
    end

    self.m_registered = true
end

FacCachePipe._UnRegisterInterested = HL.Method() << function(self)
    if not self.m_registered then
        return
    end

    if self.m_inBindingPipeDataMap ~= nil then
        for nodeId, _ in pairs(self.m_inBindingPipeDataMap) do
            GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(nodeId)
        end
    end

    if self.m_outBindingPipeDataMap ~= nil then
        for nodeId, _ in pairs(self.m_outBindingPipeDataMap) do
            GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(nodeId)
        end
    end

    self.m_registered = false
end






FacCachePipe._RefreshPipeCellState = HL.Method(HL.Any, HL.Table) << function(self, cell, info)
    if cell == nil or info == nil then
        return
    end

    cell.emptyPipe.gameObject:SetActiveIfNecessary(not info.isBinding)
    cell.bindingNode.gameObject:SetActiveIfNecessary(info.isBinding)

    local decoAnimationName
    if self.view.config.USE_BOLD_PIPE then
        decoAnimationName = info.isBlock and cell.animationSetting.boldDecoOutAnimation or cell.animationSetting.boldDecoInAnimation
    else
        decoAnimationName = info.isBlock and cell.animationSetting.decoOutAnimation or cell.animationSetting.decoInAnimation
    end
    if decoAnimationName ~= cell.bindingNode.decoAnimation.curStateName then
        cell.bindingNode.decoAnimation:PlayWithTween(decoAnimationName)
    end

    UIUtils.PlayAnimationAndToggleActive(cell.bindingNode.blockAnimation, info.isBlock)

    if self.m_stateRefreshCallback ~= nil then
        self.m_stateRefreshCallback(info)
    end
end

FacCachePipe._RefreshPipeCellBlockState = HL.Method(HL.Number) << function(self, buildingNodeId)
    if not self.m_isInPipeMode then
        return
    end

    if self.m_buildingNodeId ~= buildingNodeId then
        return
    end

    self:_GetPipeInfoList()

    for index, inPipeInfo in ipairs(self.m_inPipeInfoList) do
        local inPipe
        if inPipeInfo.index ~= nil then
            inPipe = self.m_inPipeList[LuaIndex(inPipeInfo.index)]
            if inPipe == nil then
                inPipe = self.m_inPipeList[index]
            end
        end
        self:_RefreshPipeCellState(inPipe, inPipeInfo)
    end
    for index, outPipeInfo in ipairs(self.m_outPipeInfoList) do
        local outPipe
        if outPipeInfo.index ~= nil then
            outPipe = self.m_outPipeList[LuaIndex(outPipeInfo.index)]
            if outPipe == nil then
                outPipe = self.m_outPipeList[index]
            end
        end
        self:_RefreshPipeCellState(outPipe, outPipeInfo)
    end
end

FacCachePipe._RefreshPipeCellConveyorAnimation = HL.Method(HL.Boolean, HL.Number, HL.Number, HL.String) << function(
    self, isIn, nodeId, compId, itemId)
    if self.m_isInSingleState then
        return
    end

    local pipeMap = isIn and self.m_outBindingPipeDataMap or self.m_inBindingPipeDataMap
    local pipeData = pipeMap[nodeId]
    if pipeData == nil then
        return
    end

    if pipeData.touchCompId ~= compId then
        return
    end

    if self:_GetIsPipeBlocked(nodeId, not isIn) then
        return
    end

    local cell = pipeData.pipeCell
    if cell == nil then
        return
    end

    cell.bindingNode.itemAnimation:ClearTween()
    local itemChangedAnimationName = self.view.config.USE_BOLD_PIPE and
        cell.animationSetting.boldItemAnimation or
        cell.animationSetting.itemAnimation
    cell.bindingNode.itemAnimation:PlayWithTween(itemChangedAnimationName)

    local itemSprite = self:_GetItemSprite(itemId)
    cell.bindingNode.itemIcon.sprite = itemSprite
    cell.bindingNode.blockIcon.sprite = itemSprite
    cell.bindingNode.blockIcon.gameObject:SetActive(true)
end







FacCachePipe.RefreshCachePipe = HL.Method() << function(self)
    self:_RefreshCachePipe()
end

FacCachePipe.SetCachePipeSingleState = HL.Method(HL.Boolean) << function(self, useSingleState)


    for index, inPipeInfo in ipairs(self.m_inPipeInfoList) do
        local inPipe
        if inPipeInfo.index ~= nil then
            inPipe = self.m_inPipeList[LuaIndex(inPipeInfo.index)]
            if inPipe == nil then
                inPipe = self.m_inPipeList[index]
            end
        end
        inPipe.bindingNode.blockNode.gameObject:SetActiveIfNecessary(not useSingleState)
        inPipe.bindingNode.itemNode.gameObject:SetActiveIfNecessary(not useSingleState)
    end

    for index, outPipeInfo in ipairs(self.m_outPipeInfoList) do
        local outPipe
        if outPipeInfo.index ~= nil then
            outPipe = self.m_outPipeList[LuaIndex(outPipeInfo.index)]
            if outPipe == nil then
                outPipe = self.m_outPipeList[index]
            end
        end
        outPipe.bindingNode.blockNode.gameObject:SetActiveIfNecessary(not useSingleState)
        outPipe.bindingNode.itemNode.gameObject:SetActiveIfNecessary(not useSingleState)
    end

    self.m_isInSingleState = useSingleState
end

FacCachePipe.RefreshPipeCellsState = HL.Method() << function(self)
    if not self.m_isInPipeMode then
        return
    end

    for index, inPipeInfo in ipairs(self.m_inPipeInfoList) do
        local inPipe
        if inPipeInfo.index ~= nil then
            inPipe = self.m_inPipeList[LuaIndex(inPipeInfo.index)]
            if inPipe == nil then
                inPipe = self.m_inPipeList[index]
            end
        end
        self:_RefreshPipeCellState(inPipe, inPipeInfo)
    end
    for index, outPipeInfo in ipairs(self.m_outPipeInfoList) do
        local outPipe
        if outPipeInfo.index ~= nil then
            outPipe = self.m_outPipeList[LuaIndex(outPipeInfo.index)]
            if outPipe == nil then
                outPipe = self.m_outPipeList[index]
            end
        end
        self:_RefreshPipeCellState(outPipe, outPipeInfo)
    end
end



FacCachePipe.ChangePipeSpacingY = HL.Method(HL.Boolean, HL.Boolean) << function(self, isIn, single)
    local halfSpacing = single and SINGLE_LIQUID_CACHE_SLOT_SPACING_Y or MULTI_LIQUID_CACHE_SLOT_SPACING_Y

    if isIn then
        self.view.pipeCell1.transform.anchoredPosition = Vector2(self.view.pipeCell1.transform.anchoredPosition.x, halfSpacing)
        self.view.pipeCell2.transform.anchoredPosition = Vector2(self.view.pipeCell2.transform.anchoredPosition.x, -halfSpacing)
    else
        self.view.pipeCell3.transform.anchoredPosition = Vector2(self.view.pipeCell3.transform.anchoredPosition.x, halfSpacing)
        self.view.pipeCell4.transform.anchoredPosition = Vector2(self.view.pipeCell4.transform.anchoredPosition.x, -halfSpacing)
    end
end




FacCachePipe.ChangePipeLineColor = HL.Method(HL.Boolean, HL.Boolean) << function(self, isIn, single)
    if isIn then
        if single then
            self.view.pipeCell1.decoLine.color = UIUtils.getColorByString(SINGLE_REPO_PIPE_COLOR)
            self.view.pipeCell2.decoLine.color = UIUtils.getColorByString(SINGLE_REPO_PIPE_COLOR)
        else
            self.view.pipeCell1.decoLine.color = UIUtils.getColorByString(MULTI_REPO_PIPE_COLOR_1)
            self.view.pipeCell2.decoLine.color = UIUtils.getColorByString(MULTI_REPO_PIPE_COLOR_2)
        end
    else
        if single then
            self.view.pipeCell3.decoLine.color = UIUtils.getColorByString(SINGLE_REPO_PIPE_COLOR)
            self.view.pipeCell4.decoLine.color = UIUtils.getColorByString(SINGLE_REPO_PIPE_COLOR)
        else
            self.view.pipeCell3.decoLine.color = UIUtils.getColorByString(MULTI_REPO_PIPE_COLOR_1)
            self.view.pipeCell4.decoLine.color = UIUtils.getColorByString(MULTI_REPO_PIPE_COLOR_2)
        end
    end
end




HL.Commit(FacCachePipe)
return FacCachePipe

