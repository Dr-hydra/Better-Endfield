
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacUndergroundPipe

FacUndergroundPipeCtrl = HL.Class('FacUndergroundPipeCtrl', uiCtrl.UICtrl)

local UDPIPE_CONTROLLER_STATE_MAP = {
    ["udpipe_loader_1"] = "NormalEntrance",
    ["udpipe_loader_2"] = "AdvancedEntrance",
    ["udpipe_unloader_1"] = "NormalOutlet",
    ["udpipe_unloader_2"] = "AdvancedOutlet",
}

local GASLIQUID_UNLOCK_TECH_NODE_ID = "tech_jinlong_4_gas_pump_1"

local UDPIPE_NODE_STATE = {
    Disconnect = "DisconnectState",
    Normal = "InOperationState",
    Block = "BlockState",

    Limited = "FlowRateLimitState",
}

FacUndergroundPipeCtrl.m_buildingInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo)

FacUndergroundPipeCtrl.m_curConnectNode = HL.Field(HL.Table)

FacUndergroundPipeCtrl.m_curStateName = HL.Field(HL.String) << ""

FacUndergroundPipeCtrl.m_isSpeedLimited = HL.Field(HL.Boolean) << false

FacUndergroundPipeCtrl.m_maxSpeed = HL.Field(HL.Number) << 0

FacUndergroundPipeCtrl.m_templateId = HL.Field(HL.String) << ""

FacUndergroundPipeCtrl.m_isLoader = HL.Field(HL.Boolean) << false

FacUndergroundPipeCtrl.m_updateThread = HL.Field(HL.Thread)

FacUndergroundPipeCtrl.m_dropHelper = HL.Field(HL.Forward('UIDropHelper'))

FacUndergroundPipeCtrl.m_capacityCount = HL.Field(HL.Number) << 0

FacUndergroundPipeCtrl.m_lastConveyorItemId = HL.Field(HL.String) << ""

FacUndergroundPipeCtrl.m_lastItemId = HL.Field(HL.String) << ""

FacUndergroundPipeCtrl.m_itemCountZero = HL.Field(HL.Boolean) << false

FacUndergroundPipeCtrl.m_cachedSprite = HL.Field(HL.Table)

FacUndergroundPipeCtrl.m_needRefreshPortState = HL.Field(HL.Boolean) << false

FacUndergroundPipeCtrl.m_cacheType = HL.Field(HL.Number) << 0





FacUndergroundPipeCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_START_UI_DRAG] = '_OnStartUiDrag',
    [MessageConst.ON_END_UI_DRAG] = '_OnEndUiDrag',

    [MessageConst.ON_CONVEYOR_CHANGE] = '_OnConveyorChange',
}


FacUndergroundPipeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_buildingInfo = arg.uiInfo
    self.m_templateId = self.m_buildingInfo.nodeHandler.templateId
    self:_InitCfgData()

    self.m_cacheType = GameInstance.player.facTechTreeSystem:NodeIsHidden(GASLIQUID_UNLOCK_TECH_NODE_ID) and FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid or FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid
    self.view.liquidItemSlot:SetItemType(self.m_cacheType)
    self.view.liquidItemSlot.view.cacheTypeCtrl:SetState(FacConst.FAC_CACHE_SLOT_TYPE_CTRL_STATE[self.m_cacheType])
    self.view.buildingCommon:InitBuildingCommon(self.m_buildingInfo)
    self.view.mainUIController:SetState(UDPIPE_CONTROLLER_STATE_MAP[self.m_templateId])
    self.m_isLoader = FacConst.UDPIPE_PORT_LOAD_TYPE_MAP[self.m_templateId]
    self.m_curConnectNode = self.m_isLoader and self.view.exportInfo or self.view.entranceInfo
    self.m_curConnectNode.viewMapBtn.onClick:AddListener(function()
        self:_OnClickViewMapBtn()
    end)
    self.m_curConnectNode.disconnectBtn.onClick:AddListener(function()
        self:_OnClickDisconnectBtn()
    end)
    self.view.buildingCommon:InitBuildingCommon(self.m_buildingInfo, {
        onStateChanged = function(state)
            self:_UpdateUdPipeConnectNode()
        end,
    })
    self:_UpdateUdPipeConnectNode()

    self.view.facCachePipe:InitFacCachePipe(self.m_buildingInfo, {
        useSinglePipe = FacConst.UDPIPE_PORT_LAYOUT_STATE_MAP[self.m_templateId],
        needInversePipe = true,
    })
    self.view.inventoryArea:InitInventoryArea({
        customOnUpdateCell = function(cell, itemBundle)
            self:_RefreshInventoryItemCell(cell, itemBundle)
        end,
        onStateChange = function()
            self:_RefreshNaviGroupSwitcherInfos()
        end,
        hasFluidInCache = true,
        hasGasInCache = self.m_cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid,
    })
    self:_InitUndergroundPipeUpdateThread()
    self:_InitFacMachineCrafterController()

    self.view.liquidItemSlot.view.liquidNaviGroup:NaviToThisGroup()

    GameInstance.remoteFactoryManager:RegisterInterestedUnitId(self.m_buildingInfo.nodeId)
end

FacUndergroundPipeCtrl.OnClose = HL.Override() << function(self)
    GameInstance.remoteFactoryManager:UnregisterInterestedUnitId(self.m_buildingInfo.nodeId)
end

FacUndergroundPipeCtrl.OnShow = HL.Override() << function(self)
    if self.m_needRefreshPortState and self.m_buildingInfo.nodeHandler.valid then
        self.m_needRefreshPortState = false
        self.m_buildingInfo:Update()
        self:_UpdateUdPipeConnectNode()
    end
end

FacUndergroundPipeCtrl.OnHide = HL.Override() << function(self)
    self.m_needRefreshPortState = true
end

FacUndergroundPipeCtrl._OnClickViewMapBtn = HL.Method() << function(self)
    local connect = self.m_buildingInfo.udPipe.connectComponent
    if connect == nil then
        return
    end
    local success, mapInstId = GameInstance.player.mapManager:GetFacMarkInstIdByNodeId(connect.belongNode.belongChapter.chapterId, connect.belongNode.nodeId)
    if success then
        MapUtils.openMap(mapInstId)
    end
end

FacUndergroundPipeCtrl._OnClickDisconnectBtn = HL.Method() << function(self)
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_UDPIPE_DISCONNECTION_TIPS,
        onConfirm = function()
            local connect = self.m_buildingInfo.udPipe.connectComponent
            if connect == nil then
                return
            end
            local fromId = self.m_isLoader and self.m_buildingInfo.udPipe.componentId or connect.componentId
            local toId = self.m_isLoader and connect.componentId or self.m_buildingInfo.udPipe.componentId
            GameInstance.player.remoteFactory.core:Message_OpDelUpPipeConnection(self.m_buildingInfo.chapterId, fromId, toId, function()
                self.m_buildingInfo:Update()
                self:_UpdateUdPipeConnectNode()

                if DeviceInfo.usingController then
                    self.m_curConnectNode.btnNaviGroup:ManuallyStopFocus()
                end

                GameInstance.player.mapManager:OnUdPipeLineChange()
            end)
        end
    })
end

FacUndergroundPipeCtrl._OnConveyorChange = HL.Method(HL.Table) << function(self, args)
    if self.m_curConnectNode.itemAnim.curState == CS.Beyond.UI.UIConst.AnimationState.In then
        return
    end
    local bindingNodeId, componentId, isIn, itemList = unpack(args)

    if bindingNodeId == self.m_buildingInfo.nodeId and self.m_isLoader ~= isIn then
        local itemId = (itemList ~= nil and itemList.Count > 0) and itemList[0] or ""
        if not string.isEmpty(itemId) then
            self.m_lastConveyorItemId = itemId
            self.m_curConnectNode.blockIcon.sprite = self:_GetItemSprite(itemId)
            self.m_curConnectNode.blockIcon.gameObject:SetActiveIfNecessary(true)
            self.m_curConnectNode.itemIcon.sprite = self:_GetItemSprite(itemId)
            self.m_curConnectNode.itemAnim:PlayInAnimation()
        end
    end
end

FacUndergroundPipeCtrl._GetItemSprite = HL.Method(HL.String).Return(HL.Userdata) << function(self, itemId)
    if not self.m_cachedSprite then
        self.m_cachedSprite = {}
    end
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

FacUndergroundPipeCtrl._UpdateUdPipeConnectNode = HL.Method() << function(self)
    local connect = self.m_buildingInfo.udPipe.connectComponent
    if connect == nil then
        self.m_curStateName = UDPIPE_NODE_STATE.Disconnect
        self.m_curConnectNode.nodeController:SetState(self.m_curStateName)
        self.view.buildingCommon.view.disconnectStateNode.gameObject:SetActiveIfNecessary(true)
        self.view.buildingCommon.view.stateNode.gameObject:SetActiveIfNecessary(false)
        self.view.buildingCommon.view.disconnectStateText.text = self.m_isLoader and Language["ui_fac_common_noconnect_out_info"] or Language["ui_fac_common_noconnect_in_info"]
        self.view.buildingCommon.view.controllerSideMenuBtn:InitControllerSideMenuBtn()
        return
    end

    self.view.buildingCommon.view.disconnectStateNode.gameObject:SetActiveIfNecessary(false)
    self.view.buildingCommon.view.stateNode.gameObject:SetActiveIfNecessary(true)
    self.view.buildingCommon.view.controllerSideMenuBtn:InitControllerSideMenuBtn({
        extraBtnInfos = {
            {
                name = "UdpipeViewMap",
                textId = "ui_fac_udpipe_map_button",
                sprite = self.m_curConnectNode.mapIcon.sprite,
                priority = 3.1,
                action = function()
                    self:_OnClickViewMapBtn()
                end
            },
            {
                name = "UdpipeDisconnect",
                textId = "ui_fac_udpipe_disconnect_button",
                sprite = self.m_curConnectNode.disconnectIcon.sprite,
                priority = 3.2,
                action = function()
                    self:_OnClickDisconnectBtn()
                end
            },
        }
    })
    local bdata = Tables.factoryBuildingTable:GetValue(connect.belongNode.templateId)
    self.m_curConnectNode.titleText.text = bdata.name
    self.m_curConnectNode.btnNaviGroup:SetFocusBindingText(bdata.name)
    self.m_isSpeedLimited = FacConst.UDPIPE_PORT_LAYOUT_STATE_MAP[connect.belongNode.templateId] and not FacConst.UDPIPE_PORT_LAYOUT_STATE_MAP[self.m_templateId]
    local dstState = FactoryUtils.getBuildingStateType(self.m_buildingInfo.nodeId)
    local targetState
    if dstState == GEnums.FacBuildingState.Blocked then
        targetState = UDPIPE_NODE_STATE.Block
        self.m_curConnectNode.arrowAnim:PlayOutAnimation()
    else
        targetState = self.m_isSpeedLimited and UDPIPE_NODE_STATE.Limited or UDPIPE_NODE_STATE.Normal
        self.m_curConnectNode.arrowAnim:PlayLoopAnimation()
    end
    if self.m_curStateName == UDPIPE_NODE_STATE.Block and dstState ~= GEnums.FacBuildingState.Blocked then
        self.m_curConnectNode.blockAnim:PlayOutAnimation(function()
            self.m_curConnectNode.nodeController:SetState(targetState)
        end)
    else
        self.m_curConnectNode.nodeController:SetState(targetState)
    end
    self.m_curStateName = targetState
end

FacUndergroundPipeCtrl._UpdateConnectPassSpeed = HL.Method() << function(self)
    local connect = self.m_buildingInfo.udPipe.connectComponent
    if connect ~= nil then
        local curSpeed = self.m_buildingInfo.udPipe.lastRoundPassCount
        self.m_curConnectNode.speedTxt.text = tostring(curSpeed)
    end
end

FacUndergroundPipeCtrl._InitUndergroundPipeUpdateThread = HL.Method() << function(self)
    self:_RefreshUndergroundPipeContainerCount()
    self:_RefreshLiquidItemSlot(true)
    self:_RefreshLiquidBg()
    self:_UpdateConnectPassSpeed()

    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_RefreshUndergroundPipeContainerCount()
            self:_RefreshLiquidItemSlot(false)
            self:_RefreshLiquidBg()
            self:_UpdateConnectPassSpeed()
        end
    end)
end

FacUndergroundPipeCtrl._InitCfgData = HL.Method() << function(self)
    local success, storagerData = Tables.factoryUndergroundPipeTable:TryGetValue(self.m_templateId)
    if not success then
        return
    end

    self.m_capacityCount = storagerData.capacity
    self.m_maxSpeed = 1000 / storagerData.msPerRound
end

FacUndergroundPipeCtrl._RefreshUndergroundPipeContainerCount = HL.Method() << function(self)
    local itemCount = self.m_buildingInfo.fluidContainer.holdItemCount

    local isFull = itemCount == self.m_capacityCount
    local countColor = isFull and self.view.config.CONTAINER_FULL_COUNT_COLOR or self.view.config.NORMAL_COUNT_COLOR
    local itemView = self.view.liquidItemSlot.view.item.view
    itemView.count.color = countColor
end

FacUndergroundPipeCtrl._RefreshLiquidItemSlot = HL.Method(HL.Boolean) << function(self, firstInit)
    local itemId = self.m_buildingInfo.fluidContainer.holdItemId
    local itemCount = self.m_buildingInfo.fluidContainer.holdItemCount
    local itemSlot = self.view.liquidItemSlot


    if string.isEmpty(itemId) then
        itemId = self.m_buildingInfo.udPipe.lastPassItemId
    end

    local isEmpty
    if string.isEmpty(itemId) then
        if string.isEmpty(self.m_lastItemId) then
            isEmpty = true
        else
            itemId = self.m_lastItemId
            isEmpty = false
        end
    end

    if self.m_lastItemId ~= itemId or firstInit then
        if isEmpty then
            itemSlot:InitItemSlot()
        else
            itemSlot:InitItemSlot({
                id = itemId,
                count = itemCount,
            }, function()
                self:_OnClickItemSlot(itemSlot)
            end)
            itemSlot.gameObject.name = "Item_" .. itemId
            itemSlot.item.view.button.clickHintTextId = "virtual_mouse_hint_item_tips"
            itemSlot.item.actionMenuArgs = {}
            local isLiquid = Tables.liquidTable:ContainsKey(itemId)
            itemSlot.item.customChangeActionMenuFunc = function(actionMenuInfos)
                table.insert(actionMenuInfos, 1, {
                    text = isLiquid and Language.LUA_ITEM_ACTION_CACHE_SELECT_DUMP_LIQUID or Language.LUA_ITEM_ACTION_CACHE_SELECT_DUMP_GAS,
                    action = function()
                        Notify(MessageConst.ON_NAVI_INVENTORY_SELECT_FLUID, {
                            componentId = self.m_buildingInfo.fluidContainer.componentId,
                            slotIndex = 0,
                            fluidId = itemId,
                            sourceItem = itemSlot.item,
                            cacheType = isLiquid and FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid or FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas
                        })
                    end
                })
                table.insert(actionMenuInfos, 1, {
                    text = isLiquid and Language.LUA_ITEM_ACTION_CACHE_SELECT_FILL_LIQUID or Language.LUA_ITEM_ACTION_CACHE_SELECT_FILL_GAS,
                    action = function()
                        Notify(MessageConst.ON_NAVI_INVENTORY_SELECT_FLUID, {
                            componentId = self.m_buildingInfo.fluidContainer.componentId,
                            slotIndex = 0,
                            fluidId = "",
                            sourceItem = itemSlot.item,
                            cacheType = isLiquid and FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid or FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas
                        })
                    end
                })
            end
            InputManagerInst:SetBindingText(itemSlot.item.view.button.hoverConfirmBindingId, Language["key_hint_item_open_action_menu"])
        end
        itemSlot.view.dragItem.enabled = false
        itemSlot.view.emptyIcon.gameObject:SetActiveIfNecessary(isEmpty)

        self.m_dropHelper = UIUtils.initUIDropHelper(self.view.liquidItemSlot.view.dropItem, {
            acceptTypes = UIConst.FACTORY_LIQUID_STORAGER_DROP_ACCEPT_INFO,
            onDropItem = function(eventData, dragHelper)
                if self:_CanDrop(dragHelper) then
                    self:_OnDropItem(dragHelper)
                end
            end,
            isDropArea = true,
        })
    end

    local countZero = (itemCount == 0)
    if self.m_itemCountZero ~= countZero or self.m_lastItemId ~= itemId or firstInit then
        if not isEmpty then
            if not countZero then
                local isLiquid = Tables.liquidTable:ContainsKey(itemId)
                local selectFillLiquidId = itemSlot.item:AddHoverBinding("common_quick_drop", function()
                    Notify(MessageConst.ON_NAVI_INVENTORY_SELECT_FLUID, {
                        componentId = self.m_buildingInfo.fluidContainer.componentId,
                        slotIndex = 0,
                        fluidId = "",
                        sourceItem = itemSlot.item,
                        cacheType = isLiquid and FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid or FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas
                    })
                end)
                InputManagerInst:SetBindingText(selectFillLiquidId, Language.LUA_ITEM_ACTION_CACHE_SELECT_FILL_LIQUID)
            else
                local fakeGrayBinding = itemSlot.item:AddHoverBinding("common_quick_drop", function() end)
                InputManagerInst:SetBindingText(fakeGrayBinding, Language.LUA_ITEM_ACTION_CACHE_SELECT_FILL_LIQUID)
                InputManagerInst:ForceBindingKeyhintToGray(fakeGrayBinding, true)
            end
        end
        self.m_itemCountZero = countZero
    end

    if self.m_lastItemId ~= itemId or firstInit then
        self.m_lastItemId = itemId
        itemSlot.item.view.button.onIsNaviTargetChanged = function(active)
            if active then
                self:_TryDisableHoverBindingOnEmptyItem()
            end
        end
        self:_TryDisableHoverBindingOnEmptyItem()
    end

    itemSlot.item:UpdateCountSimple(itemCount)
end

FacUndergroundPipeCtrl._OnClickItemSlot = HL.Method(HL.Forward('ItemSlot')) << function(self, itemSlot)
    if DeviceInfo.usingController then
        itemSlot.item:ShowActionMenu()
        return
    end

    itemSlot.item:SetSelected(true)
    itemSlot.item:ShowTips(nil, function()
        itemSlot.item:SetSelected(false)
    end)
end

FacUndergroundPipeCtrl._TryDisableHoverBindingOnEmptyItem = HL.Method() << function(self)
    if string.isEmpty(self.m_lastItemId) then
        InputManagerInst:ToggleBinding(self.view.liquidItemSlot.item.view.button.hoverConfirmBindingId, false)
    end
end

FacUndergroundPipeCtrl._RefreshLiquidBg = HL.Method() << function(self)
    local itemSlot = self.view.liquidItemSlot

    local count = 0
    local height = 0
    if not string.isEmpty(self.m_buildingInfo.fluidContainer.holdItemId) then
        count = self.m_buildingInfo.fluidContainer.holdItemCount
        local maxCount = self.m_capacityCount
        if maxCount > 0 then
            height = count / maxCount
        end
    end

    itemSlot:RefreshLiquidHeight(height)
end

FacUndergroundPipeCtrl._RefreshInventoryItemCell = HL.Method(HL.Userdata, HL.Any) << function(self, cell, itemBundle)
    if cell == nil or itemBundle == nil then
        return
    end


    local itemId = itemBundle.id
    local isEmptyBottle, isFullBottle = self:_IsEmptyBottleDrop(itemId), self:_IsFullBottleDrop(itemId)
    local isBottle = isEmptyBottle or isFullBottle
    local isEmpty = string.isEmpty(itemBundle.id)

    if not isBottle and not isEmpty then
        cell.view.forbiddenMask.gameObject:SetActiveIfNecessary(true)
        cell.view.dropItem.enabled = false
    end
    if not isBottle then
        cell.view.dragItem.enabled = false
    end

    if isBottle then
        local isGasJar = FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas) or FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
        cell.item.customChangeActionMenuFunc = function(actionMenuInfos)
            local dropAction = {}
            if isEmptyBottle then
                dropAction.text = isGasJar and Language.LUA_ITEM_ACTION_FILL_GAS or Language.LUA_ITEM_ACTION_FILL_LIQUID
            else
                dropAction.text = isGasJar and Language.LUA_ITEM_ACTION_DUMP_GAS or Language.LUA_ITEM_ACTION_DUMP_LIQUID
            end
            dropAction.action = function()
                local dragHelper = cell.item.actionMenuArgs.dragHelper
                if self:_CanDrop(dragHelper) then
                    self:_OnDropItem(dragHelper)
                end
            end
            table.insert(actionMenuInfos, 1, dropAction)
        end
    end
end

FacUndergroundPipeCtrl._IsEmptyBottleDrop = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    return FactoryUtils.isEmptyBottleOrJarItem(itemId, self.m_cacheType)
end

FacUndergroundPipeCtrl._IsFullBottleDrop = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    return FactoryUtils.isFullBottleOrJarItem(itemId, self.m_cacheType)
end

FacUndergroundPipeCtrl._ShouldAcceptDrop = HL.Method(HL.Forward('UIDragHelper')).Return(HL.Boolean) << function(self, dragHelper)
    if not self.m_dropHelper:Accept(dragHelper) then
        return false
    end

    local itemId = dragHelper.info.itemId
    local isEmptyBottle, isFullBottle = self:_IsEmptyBottleDrop(itemId), self:_IsFullBottleDrop(itemId)
    if not isEmptyBottle and not isFullBottle then
        return false
    end


    if isEmptyBottle and string.isEmpty(self.m_buildingInfo.fluidContainer.holdItemId) then
        return false
    end

    return true
end

FacUndergroundPipeCtrl._CanDrop = HL.Method(HL.Forward('UIDragHelper')).Return(HL.Boolean) << function(self, dragHelper)
    if not self:_ShouldAcceptDrop(dragHelper) then
        return false
    end

    local itemId = dragHelper.info.itemId

    if Tables.liquidTable:ContainsKey(self.m_buildingInfo.fluidContainer.holdItemId) then
        if FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas) or FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas) then
            return false
        end
    end
    if Tables.gasTable:ContainsKey(self.m_buildingInfo.fluidContainer.holdItemId) then
        if FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid) or FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid) then
            return false
        end
    end

    return true
end

FacUndergroundPipeCtrl._OnDropItem = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    local source = dragHelper.source
    local dragInfo = dragHelper.info
    local core = GameInstance.player.remoteFactory.core
    local componentId = self.m_buildingInfo.fluidContainer.componentId
    if source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then
        core:Message_OpFillingFluidComWithBag(Utils.getCurrentChapterId(), componentId, dragInfo.csIndex, 0)
        FactoryUtils.playAudioWhenFillingItem(dragInfo.itemId, self.m_buildingInfo.fluidContainer.holdItemId, self.m_buildingInfo.fluidContainer.holdItemCount)
    elseif source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot then
        core:Message_OpFillingFluidComWithDepot(Utils.getCurrentChapterId(), componentId, dragInfo.itemId, 0)
        FactoryUtils.playAudioWhenFillingItem(dragInfo.itemId, self.m_buildingInfo.fluidContainer.holdItemId, self.m_buildingInfo.fluidContainer.holdItemCount)
    end
end

FacUndergroundPipeCtrl._OnStartUiDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if dragHelper == nil then
        return
    end

    if self:_ShouldAcceptDrop(dragHelper) then
        self.view.liquidItemSlot:SetAcceptDrop(true, dragHelper.info.itemId, self.m_cacheType)
    else
        self.view.liquidItemSlot:SetAcceptDrop(false)
    end
end

FacUndergroundPipeCtrl._OnEndUiDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if dragHelper == nil then
        return
    end

    self.view.liquidItemSlot:SetAcceptDrop(false)
    if self:_ShouldAcceptDrop(dragHelper) then
        self.view.liquidItemSlot:SetHintActive(false)
    end
end



FacUndergroundPipeCtrl.m_naviGroupSwitcher = HL.Field(HL.Forward('NaviGroupSwitcher'))

FacUndergroundPipeCtrl._InitFacMachineCrafterController = HL.Method() << function(self)
    local NaviGroupSwitcher = require_ex("Common/Utils/UI/NaviGroupSwitcher").NaviGroupSwitcher
    self.m_naviGroupSwitcher = NaviGroupSwitcher(self.view.inputGroup.groupId, nil, true)

    self:_RefreshNaviGroupSwitcherInfos()
end

FacUndergroundPipeCtrl._RefreshNaviGroupSwitcherInfos = HL.Method() << function(self)
    if self.m_naviGroupSwitcher == nil then
        return
    end

    local naviGroupInfos = {
        {
            naviGroup = self.view.liquidItemSlot.view.liquidNaviGroup,
            text = Language.LUA_INV_NAVI_SWITCH_TO_MACHINE
        }
    }
    self.view.inventoryArea:AddNaviGroupSwitchInfo(naviGroupInfos)
    self.m_naviGroupSwitcher:ChangeGroupInfos(naviGroupInfos)
end



HL.Commit(FacUndergroundPipeCtrl)
