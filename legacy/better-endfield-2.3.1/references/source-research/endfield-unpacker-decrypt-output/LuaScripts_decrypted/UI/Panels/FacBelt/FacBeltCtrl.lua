local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacBelt
FacBeltCtrl = HL.Class('FacBeltCtrl', uiCtrl.UICtrl)

local EMPTY_ITEM_ICON_ALPHA = 0.6
local MAX_DISPLAY_WHOLE_ITEMS_COUNT = 3
local SELF_ITEM_INDEX = 0

FacBeltCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacBeltCtrl.m_nodeId = HL.Field(HL.Any)

FacBeltCtrl.m_index = HL.Field(HL.Number) << -1

FacBeltCtrl.m_buildingInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.ConveyorUIInfo)

FacBeltCtrl.m_selfItem = HL.Field(HL.String) << ""

FacBeltCtrl.m_lastSelfValidItem = HL.Field(HL.String) << ""

FacBeltCtrl.m_wholeBeltItems = HL.Field(HL.Table)

FacBeltCtrl.m_wholeBeltItemsCells = HL.Field(HL.Forward("UIListCache"))

FacBeltCtrl.m_wholeBeltLength = HL.Field(HL.Number) << -1

FacBeltCtrl.m_updateThread = HL.Field(HL.Thread)




FacBeltCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_buildingInfo = arg.uiInfo
    local nodeId = self.m_buildingInfo.nodeId
    self.m_nodeId = nodeId
    self.m_index = arg.index

    local beltData = Tables.factoryGridBeltTable:GetValue(self.m_buildingInfo.nodeHandler.templateId).beltData
    local buildingData = { nodeId = arg.uiInfo.nodeId }
    setmetatable(buildingData, { __index = beltData })

    self.view.buildingCommon:InitBuildingCommon(nil, {
        data = buildingData,
        customLeftButtonOnClicked = function()
            self:_OnDeleteBeltButtonClicked()
        end,
        customRightButtonOnClicked = function()
            self:_OnDeleteWholeBeltsButtonClicked()
        end,
    })
    self.view.buildingCommon:ChangeBuildingStateDisplay(GEnums.FacBuildingState.Normal)

    self.m_wholeBeltItems = {}
    self.m_wholeBeltItemsCells = UIUtils.genCellCache(self.view.wholeItemCell)

    self:_InitBeltButtons()
    self:_InitBeltBasicContent()
    self:_InitBeltUpdateThread()
    self:_InitBeltController()
end

FacBeltCtrl.OnClose = HL.Override() << function(self)
    self.m_updateThread = self:_ClearCoroutine(self.m_updateThread)
end

FacBeltCtrl._InitBeltBasicContent = HL.Method() << function(self)
    local success, tableData = Tables.factoryGridBeltTable:TryGetValue(self.m_buildingInfo.nodeHandler.templateId)
    if success == nil then
        return
    end

    local beltData = tableData.beltData
    local speed = beltData.msPerRound > 0 and 1000 / beltData.msPerRound or 0
    self.view.buildingCommon.view.speedText.text = string.format("%.1f", speed)
    self.view.buildingCommon.view.normalLine.gameObject:SetActive(speed > 0)
    self.view.buildingCommon.view.stopLine.gameObject:SetActive(speed <= 0)

    self.m_wholeBeltLength = CSFactoryUtil.GetConveyorLength(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId)
end




FacBeltCtrl._InitBeltUpdateThread = HL.Method() << function(self)
    self:_RefreshSelfItem(true)

    self:_UpdateBeltItems()

    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_UpdateBeltItems()

        end
    end)
end

FacBeltCtrl._UpdateBeltItems = HL.Method() << function(self)

    local success, itemList = CSFactoryUtil.GetConveyorPipeInfo(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId, self.m_index, false)
    local currentSelfItemId = ""
    if success and itemList.Count > 0 then
        currentSelfItemId = itemList[SELF_ITEM_INDEX]
    end
    if self.m_selfItem ~= currentSelfItemId then
        local isEmpty = string.isEmpty(currentSelfItemId)
        if not isEmpty then
            self.m_lastSelfValidItem = currentSelfItemId
        end
        self:_RefreshSelfItem(isEmpty)
    end
    self.m_selfItem = currentSelfItemId


    local wholeSuccess, wholeItemList = CSFactoryUtil.GetConveyorPipeInfo(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId, self.m_index, true)
    if wholeSuccess then
        self.m_wholeBeltItems = {}
        local index = 0
        for i = 0, wholeItemList.Count - 1 do
            local currentItemId = wholeItemList[i]
            if index == 0 then
                index = index + 1
                self.m_wholeBeltItems[index] = {
                    id = currentItemId,
                    count = 1,
                }
            else
                local lastItemId = self.m_wholeBeltItems[index].id
                if lastItemId == currentItemId then
                    self.m_wholeBeltItems[index].count = self.m_wholeBeltItems[index].count + 1
                else
                    index = index + 1
                    self.m_wholeBeltItems[index] = {
                        id = currentItemId,
                        count = 1,
                    }
                end
            end
        end
    else
        self.m_wholeBeltItems = {}
    end
    self:_RefreshWholeItems()
end

FacBeltCtrl._RefreshSelfItem = HL.Method(HL.Boolean) << function(self, isEmpty)
    if string.isEmpty(self.m_lastSelfValidItem) then
        self.view.selfItem:InitItem({ id = ""})
        self.view.selfItemIcon.gameObject:SetActiveIfNecessary(false)
        self.view.nameText.gameObject:SetActiveIfNecessary(false)
        if self.view.getSelfItemButton.interactable then
            self.view.getSelfItemButton.interactable = false
        end
        return
    end

    local success, itemData = Tables.itemTable:TryGetValue(self.m_lastSelfValidItem)
    if success then
        self.view.selfItemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
        local color = self.view.selfItemIcon.color
        color.a = isEmpty and EMPTY_ITEM_ICON_ALPHA or 1
        self.view.selfItemIcon.color = color
        self.view.nameText.text = itemData.name
    end

    self.view.selfItem:InitItem({
        id = self.m_lastSelfValidItem,
        count = isEmpty and 0 or 1,
    }, true)

    self.view.selfItem.gameObject:SetActiveIfNecessary(true)
    self.view.nameText.gameObject:SetActiveIfNecessary(true)
    self.view.selfItemIcon.gameObject:SetActiveIfNecessary(true)
    if not self.view.getSelfItemButton.interactable then
        self.view.getSelfItemButton.interactable = true
    end
end

FacBeltCtrl._RefreshWholeItems = HL.Method() << function(self)
    if #self.m_wholeBeltItems == 0 then
        self.view.wholeItems.gameObject:SetActiveIfNecessary(false)
        if self.view.getWholeItemsButton.interactable then
            self.view.getWholeItemsButton.interactable = false
        end
        return
    end

    self.m_wholeBeltItemsCells:Refresh(math.min(MAX_DISPLAY_WHOLE_ITEMS_COUNT, #self.m_wholeBeltItems), function(cell, index)
        cell:InitItem(self.m_wholeBeltItems[index], true)

        if DeviceInfo.usingController then
            cell:SetExtraInfo({
                tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                tipsPosTransform = self.view.wholeItems,
                isSideTips = true,
            })
            cell:SetEnableHoverTips(false)
        end
    end)

    self.view.wholeItems.gameObject:SetActiveIfNecessary(true)

    if not self.view.getWholeItemsButton.interactable then
        self.view.getWholeItemsButton.interactable = true
    end
end

FacBeltCtrl._RefreshBeltAnimState = HL.Method() << function(self)
    local isRunning = not string.isEmpty(self.m_selfItem)
    local beltAnimName = isRunning and "beltnode_loop" or "beltnode_default"
    if self.view.arrowAnim.curStateName == beltAnimName then
        return
    end
    self.view.arrowAnim:PlayWithTween(beltAnimName)
end






FacBeltCtrl._InitBeltButtons = HL.Method() << function(self)
    self.view.getSelfItemButton.onClick:AddListener(function()
        self:_OnGetSelfItemButtonClicked()
    end)

    self.view.getWholeItemsButton.onClick:AddListener(function()
        self:_OnGetWholeItemsButtonClicked()
    end)
end

FacBeltCtrl._OnGetSelfItemButtonClicked = HL.Method() << function(self)
    self.m_buildingInfo.sender:Message_OpMoveItemConveyorToBag(Utils.getCurrentChapterId(),
        self.m_buildingInfo.conveyorComponentId,
        self.m_wholeBeltLength - self.m_index - 1,
        false
    )
end

FacBeltCtrl._OnGetWholeItemsButtonClicked = HL.Method() << function(self)
    self.m_buildingInfo.sender:Message_OpMoveItemConveyorToBag(Utils.getCurrentChapterId(),
        self.m_buildingInfo.conveyorComponentId,
        1,
        true
    )
end

FacBeltCtrl._OnDeleteBeltButtonClicked = HL.Method() << function(self)
    GameInstance.remoteFactoryManager:DismantleUnitFromConveyor(Utils.getCurrentChapterId(), self.m_nodeId, self.m_index)
    PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
end

FacBeltCtrl._OnDeleteWholeBeltsButtonClicked = HL.Method() << function(self)
    GameInstance.player.remoteFactory.core:Message_OpDismantle(Utils.getCurrentChapterId(), self.m_nodeId)
    PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
end






FacBeltCtrl._InitBeltController = HL.Method() << function(self)
    self.view.wholeItemsNaviGroup.getDefaultSelectableFunc = function()
        if self.m_wholeBeltItemsCells:GetCount() <= 0 then
            return nil
        end
        local firstItem = self.m_wholeBeltItemsCells:GetItem(1)
        return firstItem.view.button
    end

    self.view.wholeItemsNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end



HL.Commit(FacBeltCtrl)
