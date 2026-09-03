local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local ActionOnSetNaviTarget = CS.Beyond.Input.ActionOnSetNaviTarget

FacPipeRouterCtrl = HL.Class('FacPipeRouterCtrl', uiCtrl.UICtrl)

local MAX_ROUTER_PORT_COUNT = 4
local SINGLE_ROUTER_PORT_INDEX = 1
local NON_SINGLE_ROUTER_PORT_INDEX_OFFSET = -1
local ROUTER_PIPE_ITEM_INDEX_IN_LIST = 0
local SINGLE_ROUTER_ITEM_INDEX = 0

FacPipeRouterCtrl.m_buildingInfo = HL.Field(HL.Userdata)

FacPipeRouterCtrl.m_isSinglePortIn = HL.Field(HL.Boolean) << false

FacPipeRouterCtrl.m_routerItems = HL.Field(HL.Table)

FacPipeRouterCtrl.m_inPipeInfoList = HL.Field(HL.Table)

FacPipeRouterCtrl.m_outPipeInfoList = HL.Field(HL.Table)

FacPipeRouterCtrl.m_updateThread = HL.Field(HL.Thread)

FacPipeRouterCtrl.m_inBindingAnimMap = HL.Field(HL.Table)

FacPipeRouterCtrl.m_outBindingAnimMap = HL.Field(HL.Table)

FacPipeRouterCtrl.m_inItemAnimMap = HL.Field(HL.Table)

FacPipeRouterCtrl.m_outItemAnimMap = HL.Field(HL.Table)

FacPipeRouterCtrl.m_itemSpriteCache = HL.Field(HL.Table)

FacPipeRouterCtrl.m_initialNaviTarget = HL.Field(HL.Forward("Item"))

FacPipeRouterCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_buildingInfo = arg.uiInfo

    local logisticData = FactoryUtils.getLogisticData(self.m_buildingInfo.nodeHandler.templateId)
    local buildingData = { nodeId = arg.uiInfo.nodeId }
    setmetatable(buildingData, { __index = logisticData })
    self.view.buildingCommon:InitBuildingCommon(nil, {
        data = buildingData,
        customRightButtonOnClicked = function()
            self:_OnDeleteRouterButtonClicked()
        end,
    })
    self.view.buildingCommon:ChangeBuildingStateDisplay(GEnums.FacBuildingState.Normal)
    local speed = logisticData.msPerRound > 0 and 1000 / logisticData.msPerRound or 0
    self.view.buildingCommon.view.speedText.text = string.format("%d", math.floor(1000 / logisticData.msPerRound))
    self.view.buildingCommon.view.stopLine.gameObject:SetActive(speed <= 0)
    self.view.buildingCommon.view.normalLine.gameObject:SetActive(speed > 0)

    self.m_routerItems = {}
    self.m_itemSpriteCache = {}
    self:_InitConveyorEvent()
    self:_InitRouterPortData()
    self:_InitRouterUpdateThread()
    self:_InitConveyorBindingAnim()

    if DeviceInfo.usingController and self.m_initialNaviTarget ~= nil then
        self.m_initialNaviTarget:SetAsNaviTarget()
    end
end

FacPipeRouterCtrl.OnClose = HL.Override() << function(self)
    self:_ClearConveyorEvent()
    self.m_updateThread = self:_ClearCoroutine(self.m_updateThread)
end

FacPipeRouterCtrl._InitRouterPortData = HL.Virtual() << function(self)
end

FacPipeRouterCtrl._InitConveyorEvent = HL.Method() << function(self)
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

FacPipeRouterCtrl._ClearConveyorEvent = HL.Method() << function(self)
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

FacPipeRouterCtrl._InitRouterUpdateThread = HL.Method() << function(self)

    self:_UpdateRouterItems()
    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_UpdateRouterItems()
        end
    end)
end

FacPipeRouterCtrl._UpdateRouterItems = HL.Method() << function(self)
    for index = SINGLE_ROUTER_PORT_INDEX, MAX_ROUTER_PORT_COUNT do
        local isIn
        if index == SINGLE_ROUTER_PORT_INDEX then
            isIn = self.m_isSinglePortIn
        else
            isIn = not self.m_isSinglePortIn
        end
        local portIndex = index == SINGLE_ROUTER_PORT_INDEX and SINGLE_ROUTER_PORT_INDEX or index + NON_SINGLE_ROUTER_PORT_INDEX_OFFSET
        local connectToMachine, fcType = CSFactoryUtil.GetPortConnectionNodeType(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId, isIn, CSIndex(portIndex))
        local itemId = ""
        if connectToMachine and fcType ~= GEnums.FCNodeType.FluidConveyor then
            local router = self.m_buildingInfo.fluidRouterM1
            if router ~= nil then
                local itemInfo = isIn and router.lastItemIn or router.lastItemOut
                local succ, item = itemInfo:TryGetValue(CSIndex(portIndex))
                if succ then
                    itemId = item
                end
            end
        else
            local success, itemList = CSFactoryUtil.GetLogisticInfo(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId, isIn, CSIndex(portIndex), false)
            if success and itemList.Count > 0 then
                local itemIndex = isIn and 0 or itemList.Count - 1
                itemId = itemList[itemIndex]
            end
        end

        self:_RefreshRouterItemState(index, itemId)
    end
end








FacPipeRouterCtrl._RefreshRouterItemState = HL.Method(HL.Number, HL.String) << function(self, index, itemId)
    local viewItemName = string.format("itemLogistics%d", index)
    local viewItem = self.view[viewItemName]
    local lastItemId = self.m_routerItems[index]
    if lastItemId == nil and string.isEmpty(itemId) then

        viewItem:InitItem({
            id = "",
            count = 0,
        })
        viewItem.view.button:ChangeActionOnSetNaviTarget(ActionOnSetNaviTarget.None)
        self.m_routerItems[index] = ""
        return
    end

    if itemId ~= lastItemId then
        if string.isEmpty(itemId) and not string.isEmpty(lastItemId) then
            viewItem:UpdateCountSimple(0)
        else
            viewItem:InitItem({
                id = itemId,
                count = 1,
            }, true)
            self.m_routerItems[index] = itemId
        end
    end
end

FacPipeRouterCtrl._OnDeleteRouterButtonClicked = HL.Method() << function(self)
    if not FactoryUtils.canDelBuilding(self.m_buildingInfo.nodeId, true) then
        return
    end
    PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
    GameInstance.player.remoteFactory.core:Message_OpDismantle(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId)
end




FacPipeRouterCtrl._InitConveyorBindingAnim = HL.Method() << function(self)

    for inIndex, inPipeInfo in ipairs(self.m_inPipeInfoList) do
        local animInfo = self.m_inBindingAnimMap[inIndex]
        if animInfo ~= nil then
            animInfo.decoAnimWrapper:PlayWithTween(animInfo.decoAnimName)
            animInfo.conveyorAnimWrapper:PlayWithTween(animInfo.conveyorAnimName)
        end
    end
    for outIndex, outPipeInfo in ipairs(self.m_outPipeInfoList) do
        local animInfo = self.m_outBindingAnimMap[outIndex]
        if animInfo ~= nil then
            animInfo.decoAnimWrapper:PlayWithTween(animInfo.decoAnimName)
            animInfo.conveyorAnimWrapper:PlayWithTween(animInfo.conveyorAnimName)
        end
    end
end

FacPipeRouterCtrl._OnConveyorChanged = HL.Method(HL.Any) << function(self, args)
    local bindingNodeId, componentId, isIn, itemList = unpack(args)
    local infoList = isIn and self.m_outPipeInfoList or self.m_inPipeInfoList
    local animMap = isIn and self.m_outItemAnimMap or self.m_inItemAnimMap
    for index, info in ipairs(infoList) do
        if info.touchNodeId == bindingNodeId then
            local animInfo = animMap[index]
            if animInfo ~= nil then
                animInfo.animationNode.animationWrapper:ClearTween()
                animInfo.animationNode.animationWrapper:PlayWithTween(animInfo.animationName)

                local itemId = itemList[SINGLE_ROUTER_ITEM_INDEX]
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




HL.Commit(FacPipeRouterCtrl)