local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local ActionOnSetNaviTarget = CS.Beyond.Input.ActionOnSetNaviTarget

FacRouterCtrl = HL.Class('FacRouterCtrl', uiCtrl.UICtrl)

local MAX_ROUTER_PORT_COUNT = 4
local SINGLE_ROUTER_PORT_INDEX = 1
local NON_SINGLE_ROUTER_PORT_INDEX_OFFSET = -1
local SINGLE_ROUTER_ITEM_INDEX = 0

FacRouterCtrl.m_buildingInfo = HL.Field(HL.Userdata)

FacRouterCtrl.m_isSinglePortIn = HL.Field(HL.Boolean) << false

FacRouterCtrl.m_routerItems = HL.Field(HL.Table)

FacRouterCtrl.m_lastValidRouterItems = HL.Field(HL.Table)

FacRouterCtrl.m_updateThread = HL.Field(HL.Thread)

FacRouterCtrl.m_inBeltInfoList = HL.Field(HL.Table)

FacRouterCtrl.m_outBeltInfoList = HL.Field(HL.Table)

FacRouterCtrl.m_inBindingAnimMap = HL.Field(HL.Table)

FacRouterCtrl.m_outBindingAnimMap = HL.Field(HL.Table)

FacRouterCtrl.m_inItemAnimMap = HL.Field(HL.Table)

FacRouterCtrl.m_outItemAnimMap = HL.Field(HL.Table)

FacRouterCtrl.m_itemSpriteCache = HL.Field(HL.Table)

FacRouterCtrl.m_initialNaviTarget = HL.Field(HL.Forward("Item"))

FacRouterCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
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
    self.view.buildingCommon.view.speedText.text = string.format("%.1f", speed)
    self.view.buildingCommon.view.stopLine.gameObject:SetActive(speed <= 0)
    self.view.buildingCommon.view.normalLine.gameObject:SetActive(speed > 0)

    self.m_routerItems = {}
    self.m_lastValidRouterItems = {}
    self.m_itemSpriteCache = {}
    self:_InitConveyorEvent()
    self:_InitRouterPortData()
    self:_InitRouterUpdateThread()
    self:_InitConveyorBindingAnim()

    if DeviceInfo.usingController and self.m_initialNaviTarget ~= nil then
        self.m_initialNaviTarget:SetAsNaviTarget()
    end
end

FacRouterCtrl.OnClose = HL.Override() << function(self)
    self:_ClearConveyorEvent()
    self.m_updateThread = self:_ClearCoroutine(self.m_updateThread)
    self.m_itemSpriteCache = nil
end

FacRouterCtrl._InitRouterPortData = HL.Virtual() << function(self)
end

FacRouterCtrl._InitConveyorEvent = HL.Method() << function(self)
    self.m_inBeltInfoList, self.m_outBeltInfoList = FactoryUtils.getBuildingPortState(self.m_buildingInfo.nodeId, false)

    if self.m_inBeltInfoList ~= nil then
        for _, inBeltInfo in pairs(self.m_inBeltInfoList) do
            if inBeltInfo.isBinding then
                GameInstance.remoteFactoryManager:RegisterInterestedUnitId(inBeltInfo.touchNodeId)
            end
        end
    end
    if self.m_outBeltInfoList ~= nil then
        for _, outBeltInfo in pairs(self.m_outBeltInfoList) do
            if outBeltInfo.isBinding then
                GameInstance.remoteFactoryManager:RegisterInterestedUnitId(outBeltInfo.touchNodeId)
            end
        end
    end

    MessageManager:Register(MessageConst.ON_CONVEYOR_CHANGE, function(args)
        self:_OnConveyorChanged(args)
    end, self)
end

FacRouterCtrl._ClearConveyorEvent = HL.Method() << function(self)
    if self.m_inBeltInfoList ~= nil then
        for _, inBeltInfo in pairs(self.m_inBeltInfoList) do
            if inBeltInfo.isBinding then
                GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(inBeltInfo.touchNodeId)
            end
        end
    end
    if self.m_outBeltInfoList ~= nil then
        for _, outBeltInfo in pairs(self.m_outBeltInfoList) do
            if outBeltInfo.isBinding then
                GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(outBeltInfo.touchNodeId)
            end
        end
    end

    MessageManager:UnregisterAll(self)
end

FacRouterCtrl._InitRouterUpdateThread = HL.Method() << function(self)

    self:_UpdateRouterItems()
    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_UpdateRouterItems()
        end
    end)
end








FacRouterCtrl._UpdateRouterItems = HL.Method() << function(self)
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
        if connectToMachine and fcType ~= GEnums.FCNodeType.BoxConveyor then
            local router = self.m_buildingInfo.boxRouterM1
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

FacRouterCtrl._RefreshRouterItemState = HL.Method(HL.Number, HL.String) << function(self, index, itemId)
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

    local lastValidItemId = self.m_lastValidRouterItems[index]
    local scope, domain = Utils.getCurrentScope(), Utils.getCurDomainId()
    local count = string.isEmpty(itemId) and 0 or Utils.getDepotItemCount(itemId, scope, domain)
    if string.isEmpty(itemId) and not string.isEmpty(lastValidItemId) then
        count = Utils.getDepotItemCount(lastValidItemId, scope, domain)
    end

    if itemId ~= lastItemId then
        if string.isEmpty(itemId) and not string.isEmpty(lastItemId) then
            if not string.isEmpty(lastValidItemId) then
                local lastValidCount = Utils.getDepotItemCount(lastValidItemId, scope, domain)
                if lastValidCount > 0 then
                    viewItem.view.contentCanvasGroup.alpha = UIConst.ITEM_MISSING_TRANSPARENCY
                end
            end
        else
            viewItem:InitItem({
                id = itemId,
                count = count,
                isInfinite = FactoryUtils.isItemInfiniteInFactoryDepot(itemId),
            }, true)
            viewItem.view.contentCanvasGroup.alpha = UIConst.ITEM_EXIST_TRANSPARENCY
            self.m_lastValidRouterItems[index] = itemId
        end
        self.m_routerItems[index] = itemId
    else
        viewItem:UpdateCountSimple(count)
    end
end

FacRouterCtrl._OnDeleteRouterButtonClicked = HL.Method() << function(self)
    if not FactoryUtils.canDelBuilding(self.m_buildingInfo.nodeId, true) then
        return
    end
    PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
    GameInstance.player.remoteFactory.core:Message_OpDismantle(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId)
end

FacRouterCtrl._GetRouterItemSprite = HL.Method(HL.String).Return(HL.Userdata) << function(self, itemId)
    if self.m_itemSpriteCache[itemId] == nil then
        local success, itemData = Tables.itemTable:TryGetValue(itemId)
        if success then
            self.m_itemSpriteCache[itemId] = self:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
        end
    end
    return self.m_itemSpriteCache[itemId]
end




FacRouterCtrl._InitConveyorBindingAnim = HL.Method() << function(self)

    for inIndex, inBeltInfo in ipairs(self.m_inBeltInfoList) do
        local animInfo = self.m_inBindingAnimMap[inIndex]
        if animInfo ~= nil then
            animInfo.decoAnimWrapper:PlayWithTween(animInfo.decoAnimName)
            animInfo.conveyorAnimWrapper:PlayWithTween(animInfo.conveyorAnimName)
        end
    end
    for outIndex, outBeltInfo in ipairs(self.m_outBeltInfoList) do
        local animInfo = self.m_outBindingAnimMap[outIndex]
        if animInfo ~= nil then
            animInfo.decoAnimWrapper:PlayWithTween(animInfo.decoAnimName)
            animInfo.conveyorAnimWrapper:PlayWithTween(animInfo.conveyorAnimName)
        end
    end
end

FacRouterCtrl._OnConveyorChanged = HL.Method(HL.Any) << function(self, args)
    local bindingNodeId, componentId, isIn, itemList = unpack(args)
    local infoList = isIn and self.m_outBeltInfoList or self.m_inBeltInfoList
    local animMap = isIn and self.m_outItemAnimMap or self.m_inItemAnimMap
    for index, info in ipairs(infoList) do
        if info.touchNodeId == bindingNodeId then
            local animInfo = animMap[index]
            if animInfo ~= nil then
                animInfo.animationNode.animationWrapper:ClearTween()
                animInfo.animationNode.animationWrapper:PlayWithTween(animInfo.animationName)

                local itemId = itemList[SINGLE_ROUTER_ITEM_INDEX]
                animInfo.animationNode.image.sprite = self:_GetRouterItemSprite(itemId)

                local fullSuccess, fullBottleData = Tables.fullBottleTable:TryGetValue(itemId)
                if fullSuccess then
                    local liquidItemId = fullBottleData.liquidId
                    animInfo.animationNode.liquidIcon.sprite = self:_GetRouterItemSprite(liquidItemId)
                    animInfo.animationNode.liquidIcon.gameObject:SetActive(true)
                else
                    animInfo.animationNode.liquidIcon.gameObject:SetActive(false)
                end
            end
        end
    end
end




HL.Commit(FacRouterCtrl)