local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacPipeConnector
local ActionOnSetNaviTarget = CS.Beyond.Input.ActionOnSetNaviTarget
FacPipeConnectorCtrl = HL.Class('FacPipeConnectorCtrl', uiCtrl.UICtrl)

local CONNECTOR_START_PORT_INDEX = 1
local CONNECTOR_MAX_PORTS_COUNT = 4
local SINGLE_CONNECTOR_ITEM_INDEX = 0

FacPipeConnectorCtrl.m_buildingInfo = HL.Field(HL.Userdata)

FacPipeConnectorCtrl.m_updateThread = HL.Field(HL.Thread)

FacPipeConnectorCtrl.m_connectorItems = HL.Field(HL.Table)

FacPipeConnectorCtrl.m_lastValidConnectorItems = HL.Field(HL.Table)

FacPipeConnectorCtrl.m_skipIndexMap = HL.Field(HL.Table)

FacPipeConnectorCtrl.m_inPipeInfoList = HL.Field(HL.Table)

FacPipeConnectorCtrl.m_outPipeInfoList = HL.Field(HL.Table)

FacPipeConnectorCtrl.m_inBindingAnimMap = HL.Field(HL.Table)

FacPipeConnectorCtrl.m_outBindingAnimMap = HL.Field(HL.Table)

FacPipeConnectorCtrl.m_inItemAnimMap = HL.Field(HL.Table)

FacPipeConnectorCtrl.m_outItemAnimMap = HL.Field(HL.Table)

FacPipeConnectorCtrl.m_itemSpriteCache = HL.Field(HL.Table)






FacPipeConnectorCtrl.s_messages = HL.StaticField(HL.Table) << {

}


FacPipeConnectorCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_buildingInfo = arg.uiInfo

    self.m_itemSpriteCache = {}
    local logisticData = FactoryUtils.getLogisticData(self.m_buildingInfo.nodeHandler.templateId)
    local buildingData = { nodeId = arg.uiInfo.nodeId }
    setmetatable(buildingData, { __index = logisticData })
    self.view.buildingCommon:InitBuildingCommon(nil, {
        data = buildingData,
        customRightButtonOnClicked = function()
            self:_OnDeleteConnectorButtonClicked()
        end,
    })
    self.view.buildingCommon:ChangeBuildingStateDisplay(GEnums.FacBuildingState.Normal)
    local speed = logisticData.msPerRound > 0 and 1000 / logisticData.msPerRound or 0
    self.view.buildingCommon.view.speedText.text = string.format("%d", math.floor(speed))
    self.view.buildingCommon.view.stopLine.gameObject:SetActive(speed <= 0)
    self.view.buildingCommon.view.normalLine.gameObject:SetActive(speed > 0)

    self:_InitConnectorUpdateItemsThread()
    self:_InitConveyorEvent()
    self:_InitAnimDataMap()
    self:_InitConveyorBindingAnim()

    if DeviceInfo.usingController then

        local naviBridgeBtn
        if self.view.buildingCommon.view.delButton.gameObject.activeSelf then
            naviBridgeBtn = self.view.buildingCommon.view.delButton
        else
            naviBridgeBtn = self.view.buildingCommon.view.forbiddenDelButton
        end
        self.view.liquidItemLogistics1.view.button:SetExplicitSelectOnRight(naviBridgeBtn)
        self.view.liquidItemLogistics4.view.button:SetExplicitSelectOnLeft(naviBridgeBtn)
        self.view.liquidItemLogistics1:SetAsNaviTarget()
    end
end

FacPipeConnectorCtrl.OnClose = HL.Override() << function(self)
    self:_ClearConveyorEvent()
    self.m_updateThread = self:_ClearCoroutine(self.m_updateThread)
end

FacPipeConnectorCtrl._InitConveyorEvent = HL.Method() << function(self)
    self.m_inPipeInfoList, self.m_outPipeInfoList = FactoryUtils.getBuildingPortState(self.m_buildingInfo.nodeId, true)

    if self.m_inPipeInfoList ~= nil then
        for _, inPipeInfo in pairs(self.m_inPipeInfoList) do
            if inPipeInfo.isBinding then
                GameInstance.remoteFactoryManager:RegisterInterestedUnitId(inPipeInfo.touchNodeId)
            end
        end
    end
    if self.m_outPipeInfoList ~= nil then
        for _, outPipeInfo in pairs(self.m_outPipeInfoList) do
            if outPipeInfo.isBinding then
                GameInstance.remoteFactoryManager:RegisterInterestedUnitId(outPipeInfo.touchNodeId)
            end
        end
    end

    MessageManager:Register(MessageConst.ON_CONVEYOR_CHANGE, function(args)
        self:_OnConveyorChanged(args)
    end, self)
end

FacPipeConnectorCtrl._ClearConveyorEvent = HL.Method() << function(self)
    if self.m_inPipeInfoList ~= nil then
        for _, inPipeInfo in pairs(self.m_inPipeInfoList) do
            if inPipeInfo.isBinding then
                GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(inPipeInfo.touchNodeId)
            end
        end
    end
    if self.m_outPipeInfoList ~= nil then
        for _, outPipeInfo in pairs(self.m_outPipeInfoList) do
            if outPipeInfo.isBinding then
                GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(outPipeInfo.touchNodeId)
            end
        end
    end

    MessageManager:UnregisterAll(self)
end

FacPipeConnectorCtrl._InitConnectorUpdateItemsThread = HL.Method() << function(self)
    self.m_connectorItems = {}
    self.m_lastValidConnectorItems = {}
    self:_UpdateConnectorItems()
    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_UpdateConnectorItems()
        end
    end)
end

FacPipeConnectorCtrl._UpdateConnectorItems = HL.Method() << function(self)
    self.m_skipIndexMap = {}
    for index = CONNECTOR_START_PORT_INDEX, CONNECTOR_MAX_PORTS_COUNT do
        if not self.m_skipIndexMap[index] then

            local connectToMachine, fcType = CSFactoryUtil.GetPortConnectionNodeType(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId, true, CSIndex(index))
            local inItemId = ""
            if connectToMachine and fcType ~= GEnums.FCNodeType.FluidConveyor then
                local bridge = self.m_buildingInfo["fluidBridge" .. index]
                if bridge ~= nil and not string.isEmpty(bridge.lastItemIn) then
                    inItemId = bridge.lastItemIn
                    self:_UpdatePortIndexSkipMap(index)
                end
            else
                local inSuccess, inItemList = CSFactoryUtil.GetLogisticInfo(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId, true, CSIndex(index), false)
                if inSuccess and inItemList.Count > 0 then
                    inItemId = inItemList[inItemList.Count - 1]
                    self:_UpdatePortIndexSkipMap(index)
                end
            end
            local inIndex = self:_GetViewIndexByConnectorPortIndex(index, true)
            self:_RefreshConnectorItemState(inIndex, inItemId)


            connectToMachine, fcType = CSFactoryUtil.GetPortConnectionNodeType(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId, false, CSIndex(index))
            local outItemId = ""
            if connectToMachine and fcType ~= GEnums.FCNodeType.FluidConveyor then
                local bridge = self.m_buildingInfo["fluidBridge" .. index]
                if bridge ~= nil and not string.isEmpty(bridge.lastItemOut) then
                    outItemId = bridge.lastItemOut
                    self:_UpdatePortIndexSkipMap(index)
                end
            else
                local outSuccess, outItemList = CSFactoryUtil.GetLogisticInfo(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId, false, CSIndex(index), false)
                if outSuccess and outItemList.Count > 0 then
                    outItemId = outItemList[outItemList.Count - 1]
                    self:_UpdatePortIndexSkipMap(index)
                end
            end
            local outIndex = self:_GetViewIndexByConnectorPortIndex(index, false)
            self:_RefreshConnectorItemState(outIndex, outItemId)
        end
    end
end

FacPipeConnectorCtrl._UpdatePortIndexSkipMap = HL.Method(HL.Number) << function(self, portLuaIndex)

    if portLuaIndex % 2 == 0 then
        self.m_skipIndexMap[portLuaIndex - 1] = true
    else
        self.m_skipIndexMap[portLuaIndex + 1] = true
    end
end

FacPipeConnectorCtrl._GetViewIndexByConnectorPortIndex = HL.Method(HL.Number, HL.Boolean).Return(HL.Number) <<
    function(self, portLuaIndex, isIn)
        if isIn then
            return portLuaIndex % 2 == 0 and portLuaIndex - 1 or portLuaIndex
        else
            return portLuaIndex % 2 == 0 and portLuaIndex or portLuaIndex + 1
        end
    end

FacPipeConnectorCtrl._RefreshConnectorItemState = HL.Method(HL.Number, HL.String) << function(self, index, itemId)
    local viewItemName = string.format("liquidItemLogistics%d", index)
    local viewItem = self.view[viewItemName]
    local lastItemId = self.m_connectorItems[index]
    if lastItemId == nil and string.isEmpty(itemId) then

        viewItem:InitItem({
            id = "",
            count = 0,
        })
        viewItem.view.button:ChangeActionOnSetNaviTarget(ActionOnSetNaviTarget.None)
        self.m_connectorItems[index] = ""
        return
    end

    local lastValidItemId = self.m_lastValidConnectorItems[index]
    if itemId ~= lastItemId then
        if string.isEmpty(itemId) then
            if not string.isEmpty(lastValidItemId) then
                viewItem:UpdateCountSimple(0)
            end
        else
            viewItem:InitItem({
                id = itemId,
                count = 1,
                isInfinite = FactoryUtils.isItemInfiniteInFactoryDepot(itemId),
            }, true)
            self.m_lastValidConnectorItems[index] = itemId
        end
        self.m_connectorItems[index] = itemId
    end
end

FacPipeConnectorCtrl._OnDeleteConnectorButtonClicked = HL.Method() << function(self)
    if not FactoryUtils.canDelBuilding(self.m_buildingInfo.nodeId, true) then
        return
    end
    PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
    GameInstance.player.remoteFactory.core:Message_OpDismantle(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId)
end




FacPipeConnectorCtrl._InitAnimDataMap = HL.Method() << function(self)
    self.m_inBindingAnimMap = {
        {
            decoAnimWrapper = self.view.arrowDecoAnimationIn1,
            decoAnimName = "facconnector_arrow02",
            conveyorAnimWrapper = self.view.conveyorAnimationIn1,
            conveyorAnimName = "facpipeconnector_arrow",
        },
        {
            decoAnimWrapper = self.view.arrowDecoAnimationIn2,
            decoAnimName = "facconnector_arrow",
            conveyorAnimWrapper = self.view.conveyorAnimationIn2,
            conveyorAnimName = "facpipeconnector_arrow",
        },
    }
    self.m_outBindingAnimMap = {
        {
            decoAnimWrapper = self.view.arrowDecoAnimationOut1,
            decoAnimName = "facconnector_arrow03",
            conveyorAnimWrapper = self.view.conveyorAnimationOut1,
            conveyorAnimName = "facpipeconnector_arrow02",
        },
        {
            decoAnimWrapper = self.view.arrowDecoAnimationOut2,
            decoAnimName = "facconnector_arrow01",
            conveyorAnimWrapper = self.view.conveyorAnimationOut2,
            conveyorAnimName = "facpipeconnector_arrow02",
        },
    }

    self.m_inItemAnimMap = {
        {
            animationNode = self.view.itemAnimationIn1,
            animationName = "connector_facpipe_changed",
        },
        {
            animationNode = self.view.itemAnimationIn2,
            animationName = "connector_facpipe_changed_3",
        }
    }
    self.m_outItemAnimMap = {
        {
            animationNode = self.view.itemAnimationOut1,
            animationName = "connector_facpipe_changed_1",
        },
        {
            animationNode = self.view.itemAnimationOut2,
            animationName = "connector_facpipe_changed_2",
        },
    }
end

FacPipeConnectorCtrl._GetAnimIndexFromPipeInfoIndex = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    return math.ceil(index / 2.0)
end

FacPipeConnectorCtrl._InitConveyorBindingAnim = HL.Method() << function(self)

    for inIndex, inPipeInfo in ipairs(self.m_inPipeInfoList) do
        local animInfo = self.m_inBindingAnimMap[self:_GetAnimIndexFromPipeInfoIndex(inIndex)]
        if animInfo ~= nil then
            animInfo.decoAnimWrapper:PlayWithTween(animInfo.decoAnimName)
            animInfo.conveyorAnimWrapper:PlayWithTween(animInfo.conveyorAnimName)
        end
    end
    for outIndex, outPipeInfo in ipairs(self.m_outPipeInfoList) do
        local animInfo = self.m_outBindingAnimMap[self:_GetAnimIndexFromPipeInfoIndex(outIndex)]
        if animInfo ~= nil then
            animInfo.decoAnimWrapper:PlayWithTween(animInfo.decoAnimName)
            animInfo.conveyorAnimWrapper:PlayWithTween(animInfo.conveyorAnimName)
        end
    end
end

FacPipeConnectorCtrl._OnConveyorChanged = HL.Method(HL.Any) << function(self, args)
    local bindingNodeId, componentId, isIn, itemList = unpack(args)
    local infoList = isIn and self.m_outPipeInfoList or self.m_inPipeInfoList
    local animMap = isIn and self.m_outItemAnimMap or self.m_inItemAnimMap
    for index, info in ipairs(infoList) do
        if info.touchNodeId == bindingNodeId then
            local animInfo = animMap[self:_GetAnimIndexFromPipeInfoIndex(index)]
            if animInfo ~= nil then
                animInfo.animationNode.animationWrapper:ClearTween()
                animInfo.animationNode.animationWrapper:PlayWithTween(animInfo.animationName)

                local itemId = itemList[SINGLE_CONNECTOR_ITEM_INDEX]
                if self.m_itemSpriteCache[itemId] == nil then
                    local success, itemData = Tables.itemTable:TryGetValue(itemId)
                    if success then
                        self.m_itemSpriteCache[itemId] = self:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
                    end
                end
                animInfo.animationNode.image.sprite = self.m_itemSpriteCache[itemId]
            end
        end
    end
end




HL.Commit(FacPipeConnectorCtrl)
