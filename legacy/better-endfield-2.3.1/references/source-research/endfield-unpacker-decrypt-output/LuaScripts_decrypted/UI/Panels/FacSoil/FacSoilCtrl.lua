
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacSoil
FacSoilCtrl = HL.Class('FacSoilCtrl', uiCtrl.UICtrl)







FacSoilCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacSoilCtrl.m_nodeId = HL.Field(HL.Any)

FacSoilCtrl.m_soil = HL.Field(HL.Userdata)

FacSoilCtrl.m_seedItemTypeCount = HL.Field(HL.Number) << 0

FacSoilCtrl.m_seedInfoList = HL.Field(HL.Table)

FacSoilCtrl.m_getCell = HL.Field(HL.Function)

FacSoilCtrl.m_selectedSeedItem = HL.Field(HL.Table)

FacSoilCtrl.m_selectedCell = HL.Field(HL.Userdata)

FacSoilCtrl.m_curFocusCell = HL.Field(HL.Userdata)

FacSoilCtrl.m_selectSeedView = HL.Field(HL.Boolean) << false


FacSoilCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local nodeId = arg.uiInfo.nodeId
    local uiInfo = arg.uiInfo

    self.view.buildingCommon:InitBuildingCommon(uiInfo)

    self.m_soil = GameInstance.player.facSoilSystem:GetSoilNodeInCurrentRegion(nodeId)

    self.view.btnPlant.onClick:RemoveAllListeners()
    self.view.btnPlant.onClick:AddListener(function()
        self:_OnBtnPlant()
    end)

    self.view.btnCancel.onClick:RemoveAllListeners()
    self.view.btnCancel.onClick:AddListener(function()
        self:_OnBtnCancel()
    end)

    self.view.btnHarvest.onClick:RemoveAllListeners()
    self.view.btnHarvest.onClick:AddListener(function()
        self:_OnBtnHarvest()
    end)

    self.m_selectSeedView = false
    self:_UpdateStateInfo()
    self:_StartCoroutine(function()
        while true do
            coroutine.step()
            self:_UpdateStateInfo()
        end
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    InputManagerInst:SetVirtualMouseIconVisible(false)
end

FacSoilCtrl._UpdateStateInfo = HL.Method() << function(self)
    local soil = self.m_soil
    if soil.hasSeed then
        self.view.panelSelect.gameObject:SetActiveIfNecessary(false)
        self.view.panelInfo.gameObject:SetActiveIfNecessary(true)
        self.view.panelEmpty.gameObject:SetActiveIfNecessary(false)

        local seedItemId = soil.seedItemId
        self.view.itemDisplay:InitItem({id = seedItemId, count = 1}, true)

        if soil.canHarvest then
            self.view.btnCancel.gameObject:SetActiveIfNecessary(false)
            self.view.btnHarvest.gameObject:SetActiveIfNecessary(true)
            self.view.textCanHarvest.gameObject:SetActiveIfNecessary(true)
            self.view.textCountDown.gameObject:SetActiveIfNecessary(false)
            self.view.sliderCountDown.size = 1.0
        else
            local remainTime = soil.harvestRemainTime
            self.view.btnCancel.gameObject:SetActiveIfNecessary(true)
            self.view.btnHarvest.gameObject:SetActiveIfNecessary(false)
            self.view.textCanHarvest.gameObject:SetActiveIfNecessary(false)
            self.view.textCountDown.gameObject:SetActiveIfNecessary(true)
            self.view.textCountDown.text = UIUtils.getRemainingText(remainTime)
            self.view.sliderCountDown.size = soil.progressRatio
        end
    else
        if self.m_selectedSeedItem == nil then
            self.view.btnPlant.gameObject:SetActiveIfNecessary(false)
        else
            self.view.btnPlant.gameObject:SetActiveIfNecessary(true)
        end
    end

    self:_UpdateSelectView()
end

FacSoilCtrl._UpdateSelectView = HL.Method() << function(self)
    if self.m_soil.hasSeed then
        self.m_selectSeedView = false
        return
    end

    if self.m_selectSeedView then
        return
    end
    self.m_selectSeedView = true

    self.m_seedInfoList = {}
    self.m_seedItemTypeCount = 0
    for k,v in pairs(Tables.factorySeedItemTable) do
        local itemId = v.id
        local itemCount = GameInstance.player.inventory:GetItemCountInBag(Utils.getCurrentScope(), itemId)
        if itemCount > 0 then
            local seedInfo = {
                itemId = itemId,
                itemCount = itemCount,
                totalProgress = v.growTotalProgress
            }
            self.m_seedItemTypeCount = self.m_seedItemTypeCount + 1
            self.m_seedInfoList[self.m_seedItemTypeCount] = seedInfo
        end
    end

    if self.m_seedItemTypeCount > 0 then
        self.view.panelSelect.gameObject:SetActiveIfNecessary(true)
        self.view.panelInfo.gameObject:SetActiveIfNecessary(false)
        self.view.panelEmpty.gameObject:SetActiveIfNecessary(false)

        self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollListSeed)
        self.view.scrollListSeed.onUpdateCell:AddListener(function(object, index)
            self:_OnUpdateSeedItemCell(self.m_getCell(object), LuaIndex(index))
        end)
        self.view.scrollListSeed.onSelectedCell:AddListener(function(object, csIndex)
            local cell = self.m_getCell(object)
            if cell then
                self:_OnFocusSeedItemCell(cell, LuaIndex(csIndex))
            end
        end)

        self.view.scrollListSeed:UpdateCount(self.m_seedItemTypeCount)
    else
        self.view.panelSelect.gameObject:SetActiveIfNecessary(false)
        self.view.panelInfo.gameObject:SetActiveIfNecessary(false)
        self.view.panelEmpty.gameObject:SetActiveIfNecessary(true)
    end
end

FacSoilCtrl.OnAnimationInFinished = HL.Override() << function(self)
    local soil = self.m_soil
    if soil and soil.hasSeed then
        InputManagerInst:MoveVirtualMouseTo(self.view.itemDisplay.view.transform, self.uiCamera)
        InputManagerInst:SetVirtualMouseIconVisible(true)
    else
        if self.m_seedItemTypeCount > 0 then
            self.view.scrollListSeed:SetSelectedIndex(0, true, true)
            InputManagerInst:SetVirtualMouseIconVisible(false)
        end
    end
end

FacSoilCtrl._OnUpdateSeedItemCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local seedItemData = self.m_seedInfoList[luaIndex]

    cell.view.destroySelectNode.gameObject:SetActiveIfNecessary(false)
    cell:InitItem({
            id = seedItemData.itemId,
            count = seedItemData.itemCount
        },
        function()
            self:_OnFocusSeedItemCell(cell, luaIndex)
            self:_OnClickSeedItemCell(cell, seedItemData, true)
        end
    )
    cell.view.button.clickHintTextId = "key_hint_common_select"
end

FacSoilCtrl._OnFocusSeedItemCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    if self.m_curFocusCell ~= nil then
        self.m_curFocusCell:SetSelected(false)
    end

    self.view.scrollListSeed:SetSelectedIndex(CSIndex(luaIndex))
    self.m_curFocusCell = cell
    cell:SetSelected(true)
end

FacSoilCtrl._OnClickSeedItemCell = HL.Method(HL.Any, HL.Any, HL.Boolean) << function(self, cell, seedItemData)
    if self.m_selectedCell == cell then
        self.m_selectedCell.view.button.clickHintTextId = "key_hint_common_select"
        self.m_selectedCell.view.destroySelectNode.gameObject:SetActiveIfNecessary(false)
        self.m_selectedCell = nil
        self.m_selectedSeedItem = nil


        Notify(MessageConst.HIDE_ITEM_TIPS)
    else
        local posInfo = {
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            tipsPosTransform = self.view.anchorItemTips.transform,
            isSideTips = true
        }
        local timeCost = seedItemData.totalProgress

        if self.m_selectedCell ~= nil then
            self.m_selectedCell.view.destroySelectNode.gameObject:SetActiveIfNecessary(false)
            self.m_selectedCell.view.button.clickHintTextId = "key_hint_common_select"
        end
        self.m_selectedCell = cell
        cell.view.destroySelectNode.gameObject:SetActiveIfNecessary(true)
        cell.view.button.clickHintTextId = "key_hint_common_unselect"

        cell.prefixDesc = string.format(Language.LUA_FAC_SOIL_TIME_COST_TEXT, UIUtils.getRemainingText(timeCost))
        cell.hideItemObtainWays = true
        cell.hideBottomInfo = true
        cell:ShowTips(posInfo)
        self.m_selectedSeedItem = seedItemData
    end
end

FacSoilCtrl._OnBtnPlant = HL.Method() << function(self)
    local selectedSeedItem = self.m_selectedSeedItem
    if selectedSeedItem ~= nil then
        self.m_soil:PlantSeedFromBag(selectedSeedItem.itemId)
    end

    Notify(MessageConst.HIDE_ITEM_TIPS)
end

FacSoilCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.HIDE_ITEM_TIPS)
end

FacSoilCtrl._OnBtnCancel = HL.Method() << function(self)
    self:Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_FAC_SOIL_CANCEL_BUTTON_TEXT,
        onConfirm = function()
            self.m_soil:CancelSeed()
        end
    })
end

FacSoilCtrl._OnBtnHarvest = HL.Method() << function(self)
    if self.m_soil.canHarvest then
        self.m_soil:HarvestByPick()
    end
end

HL.Commit(FacSoilCtrl)
