local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainDepotGoodsType
local DomainDepotDeliverItemType = GEnums.DomainDepotDeliverItemType
local DeliverPackType = GEnums.DeliverPackType

DomainDepotGoodsTypeCtrl = HL.Class('DomainDepotGoodsTypeCtrl', uiCtrl.UICtrl)

local ItemTypeCellViewConfig = {
    [DomainDepotDeliverItemType.NaturalResource] = {
        viewName = "natureCell",
        viewNumber = 2,
    },
    [DomainDepotDeliverItemType.Industry] = {
        viewName = "industryCell",
        viewNumber = 3,
    },
    [DomainDepotDeliverItemType.Misc] = {
        viewName = "miscCell",
        viewNumber = 1,
    },
}

local PackTypeTabViewConfig = {
    [DeliverPackType.SmallPack] = {
        viewName = "tabSmallCell",
        viewNumber = 1,
    },
    [DeliverPackType.MediumPack] = {
        viewName = "tabMediumCell",
        viewNumber = 2,
    },
    [DeliverPackType.LargePack] = {
        viewName = "tabLargeCell",
        viewNumber = 3,
    },
}

local PACK_TYPE_NUMBER_TEXT_FORMAT = "#0%d"

local TYPE_AUDIO_KEY_MAP = {
    [DomainDepotDeliverItemType.NaturalResource] = {
        [DeliverPackType.SmallPack] = "Au_UI_Event_RegionWareBoxDrop_NatureSmall",
        [DeliverPackType.MediumPack] = "Au_UI_Event_RegionWareBoxDrop_NatureMedium",
        [DeliverPackType.LargePack] = "Au_UI_Event_RegionWareBoxDrop_NatureLarge",
    },
    [DomainDepotDeliverItemType.Industry] = {
        [DeliverPackType.SmallPack] = "Au_UI_Event_RegionWareBoxDrop_IndustrySmall",
        [DeliverPackType.MediumPack] = "Au_UI_Event_RegionWareBoxDrop_IndustryMedium",
        [DeliverPackType.LargePack] = "Au_UI_Event_RegionWareBoxDrop_IndustryLarge",
    },
    [DomainDepotDeliverItemType.Misc] = {
        [DeliverPackType.SmallPack] = "Au_UI_Event_RegionWareBoxDrop_SundriesSmall",
        [DeliverPackType.MediumPack] = "Au_UI_Event_RegionWareBoxDrop_SundriesMedium",
        [DeliverPackType.LargePack] = "Au_UI_Event_RegionWareBoxDrop_SundriesLarge",
    },
}





DomainDepotGoodsTypeCtrl.s_messages = HL.StaticField(HL.Table) << {

}

DomainDepotGoodsTypeCtrl.m_domainId = HL.Field(HL.String) << ""

DomainDepotGoodsTypeCtrl.m_depotId = HL.Field(HL.String) << ""

DomainDepotGoodsTypeCtrl.m_selectedItemType = HL.Field(DomainDepotDeliverItemType)

DomainDepotGoodsTypeCtrl.m_selectedPackType = HL.Field(DeliverPackType)

DomainDepotGoodsTypeCtrl.m_valueLimitCfg = HL.Field(HL.Table)

DomainDepotGoodsTypeCtrl.m_pack = HL.Field(HL.Forward("DomainDepotPack"))

DomainDepotGoodsTypeCtrl.m_backPanel = HL.Field(HL.Forward("DomainDepotPackBackGroundCtrl"))

DomainDepotGoodsTypeCtrl.m_incomeDomainRatio = HL.Field(HL.Number) << 1

DomainDepotGoodsTypeCtrl.m_resumeState = HL.Field(HL.Table)

DomainDepotGoodsTypeCtrl.m_curNaviItemViewNumber = HL.Field(HL.Number) << -1


DomainDepotGoodsTypeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_depotId = arg.depotId
    self.m_pack = arg.pack
    self.m_backPanel = arg.backPanel

    self.m_resumeState = arg.resumeState
    arg.resumeState = nil
    local depotTableConfig = Tables.domainDepotTable[self.m_depotId]
    self.m_domainId = depotTableConfig.domainId
    self.m_incomeDomainRatio = Tables.domainDataTable[self.m_domainId].domainDepotOfferPriceRatio

    self.view.nextBtn.onClick:AddListener(function()
        self:_OnClickNextBtn()
    end)

    self:_InitMoneyNodes()
    self:_InitPackValueLimitCfg()
    self:_InitItemTypeSelectNode()
    self:_InitPackTypeSelectNode()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end


DomainDepotGoodsTypeCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    return {
        depotId = self.m_depotId,
        resumeState = {
            selectedItemType = self.m_selectedItemType:GetHashCode(),
            selectedPackType = self.m_selectedPackType:GetHashCode(),
        }
    }
end

DomainDepotGoodsTypeCtrl._InitMoneyNodes = HL.Method() << function(self)
    DomainDepotUtils.InitTopMoneyTitle(self.view.domainTopMoneyTitle, self.m_domainId, function()
        Notify(MessageConst.ON_CLOSE_DOMAIN_DEPOT_PACK_TYPE_SELECT_PANEL)
    end)

    DomainDepotUtils.RefreshMoneyIconWithDomain(self.view.moneyIconImg, self.m_domainId)
end

DomainDepotGoodsTypeCtrl._OnClickNextBtn = HL.Method() << function(self)
    local limitCfg = self.m_valueLimitCfg[self.m_selectedItemType][self.m_selectedPackType]
    Notify(MessageConst.ON_OPEN_DOMAIN_DEPOT_PACK_ITEM_SELECT_PANEL, {
        depotId = self.m_depotId,
        domainId = self.m_domainId,
        itemType = self.m_selectedItemType,
        packType = self.m_selectedPackType,
        minLimitValue = limitCfg.minLimitValue,
        maxLimitValue = limitCfg.maxLimitValue,
    })
end

DomainDepotGoodsTypeCtrl._PlayTypeSelectAudio = HL.Method() << function(self)
    local audioKey = TYPE_AUDIO_KEY_MAP[self.m_selectedItemType][self.m_selectedPackType]
    AudioAdapter.PostEvent(audioKey)
end




DomainDepotGoodsTypeCtrl._InitItemTypeSelectNode = HL.Method() << function(self)
    local depotInfo = DomainDepotUtils.GetDepotInfo(self.m_depotId)
    local currLevelConfig = depotInfo.currLevelConfig

    local unlockedItemTypeList = {}
    for index = 0, currLevelConfig.deliverItemTypeList.Count - 1 do
        unlockedItemTypeList[currLevelConfig.deliverItemTypeList[index]] = true
    end

    local itemTypeSelectNode = self.view.itemTypeSelectNode
    local firstSelectType, firstCellCfgData, firstSelectButton
    for type, typeCfgData in pairs(ItemTypeCellViewConfig) do
        local viewCell = itemTypeSelectNode[typeCfgData.viewName]
        if unlockedItemTypeList[type] then
            viewCell.button.onClick:AddListener(function()
                self:_OnItemTypeClick(type)
                self:_PlayTypeSelectAudio()
                AudioAdapter.PostEvent("Au_UI_Toggle_Common_On")
            end)
            if firstCellCfgData == nil or firstCellCfgData.viewNumber < typeCfgData.viewNumber then
                firstSelectType = type
                firstCellCfgData = typeCfgData
                firstSelectButton = viewCell.button
            end
            viewCell.stateController:SetState("Unselected")
            viewCell.gameObject:SetActive(true)
        else
            viewCell.gameObject:SetActive(false)
        end
    end

    local targetSelectType = self:_GetResumeEnumValue(ItemTypeCellViewConfig, self.m_resumeState and self.m_resumeState.selectedItemType or nil)

    if targetSelectType == nil or not unlockedItemTypeList[targetSelectType] then
        targetSelectType = firstSelectType
    end

    if targetSelectType ~= nil then
        self.m_pack:PlayRandomItemDropAnim(targetSelectType)
        self:_OnItemTypeClick(targetSelectType)
        local targetViewCell = itemTypeSelectNode[ItemTypeCellViewConfig[targetSelectType].viewName]

        if DeviceInfo.usingController then
            self:SetNaviTarget(targetViewCell.button)
        end
    end

    itemTypeSelectNode.lockCell.gameObject:SetActive(false)
    if depotInfo.currLevel < depotInfo.maxLevel then
        if depotInfo.maxLevelConfig.deliverItemTypeList.Count > depotInfo.currLevelConfig.deliverItemTypeList.Count then
            itemTypeSelectNode.lockCell.stateController:SetState("Lock")
            itemTypeSelectNode.lockCell.gameObject:SetActive(true)
        end
    end

    itemTypeSelectNode.emptyCell.gameObject:SetActive(false)
    if depotInfo.currLevel == depotInfo.maxLevel then
        if not depotInfo.maxLevelConfig.isFinalItemTypeListLevel then
            itemTypeSelectNode.emptyCell.stateController:SetState("Empty")
            itemTypeSelectNode.emptyCell.gameObject:SetActive(true)
        end
    end
end

DomainDepotGoodsTypeCtrl._OnItemTypeClick = HL.Method(DomainDepotDeliverItemType) << function(self, itemType)
    if self.m_selectedItemType == itemType then
        return
    end

    local itemTypeSelectNode = self.view.itemTypeSelectNode

    if self.m_selectedItemType ~= nil then
        local lastCfgData = ItemTypeCellViewConfig[self.m_selectedItemType]
        local lastViewCell = itemTypeSelectNode[lastCfgData.viewName]
        lastViewCell.animationWrapper:ClearTween()
        lastViewCell.animationWrapper:PlayWithTween("domainDepot_goodscell_slcout", function()
            lastViewCell.typeNode.color = Color.white
            lastViewCell.stateController:SetState("Unselected")
        end)
        if not DeviceInfo.usingController then
            lastViewCell.button.interactable = true
        end
    end

    local currCfgData = ItemTypeCellViewConfig[itemType]
    local currViewCell = itemTypeSelectNode[currCfgData.viewName]
    currViewCell.stateController:SetState("Selected")
    currViewCell.animationWrapper:ClearTween()
    currViewCell.animationWrapper:PlayWithTween("domainDepot_goodscell_slc")
    DomainDepotUtils.SetDomainColorToDepotNodes(self.m_domainId, { currViewCell.typeNode })
    if not DeviceInfo.usingController then
        currViewCell.button.interactable = false
    end

    self.m_selectedItemType = itemType
    self:_RefreshPackValueState()
    self.m_pack:ChangePackItemType(itemType)
    self.m_backPanel:ChangePackItemType(itemType)
end






DomainDepotGoodsTypeCtrl._InitPackTypeSelectNode = HL.Method() << function(self)
    local depotInfo = DomainDepotUtils.GetDepotInfo(self.m_depotId)
    local currLevelConfig = depotInfo.currLevelConfig

    local unlockedPackTypeList = {}
    for index = 0, currLevelConfig.deliverPackTypeList.Count - 1 do
        unlockedPackTypeList[currLevelConfig.deliverPackTypeList[index]] = true
    end

    local packTypeSelectNode = self.view.packTypeSelectNode
    local firstViewTab, firstSelectType, firstTabCfgData
    local validCount = 0
    for type, typeCfgData in pairs(PackTypeTabViewConfig) do
        local viewTab = packTypeSelectNode[typeCfgData.viewName]
        if unlockedPackTypeList[type] then
            viewTab.toggle.onValueChanged:AddListener(function(isOn)
                if isOn then
                    self:_OnPackTypeToggle(type)
                    self:_PlayTypeSelectAudio()
                end
                if not DeviceInfo.usingController then
                    viewTab.toggle.interactable = not isOn
                end
            end)
            if firstTabCfgData == nil or firstTabCfgData.viewNumber < typeCfgData.viewNumber then
                firstViewTab = viewTab
                firstSelectType = type
                firstTabCfgData = typeCfgData
            end
            viewTab.gameObject:SetActive(true)
            viewTab.recommendIcon.gameObject:SetActive(false)
            validCount = validCount + 1
        else
            viewTab.gameObject:SetActive(false)
        end
    end


    local targetSelectType = self:_GetResumeEnumValue(PackTypeTabViewConfig, self.m_resumeState and self.m_resumeState.selectedPackType or nil)
    if targetSelectType == nil or not unlockedPackTypeList[targetSelectType] then
        targetSelectType = firstSelectType
    end

    if targetSelectType ~= nil then
        local targetCfgData = PackTypeTabViewConfig[targetSelectType]
        local targetViewTab = targetCfgData and packTypeSelectNode[targetCfgData.viewName] or nil
        if targetViewTab == nil then
            return
        end
        targetViewTab.toggle:SetIsOnWithoutNotify(true)
        if not DeviceInfo.usingController then
            targetViewTab.toggle.interactable = false
        end
        if validCount > 1 then
            firstViewTab.recommendIcon.gameObject:SetActive(true)
        end
        self:_OnPackTypeToggle(targetSelectType)
    end
end

DomainDepotGoodsTypeCtrl._OnPackTypeToggle = HL.Method(DeliverPackType) << function(self, packType)
    self.m_selectedPackType = packType
    self:_RefreshPackValueState()
    self.m_pack:ChangePackSize(packType)
end






DomainDepotGoodsTypeCtrl._InitPackValueLimitCfg = HL.Method() << function(self)
    self.m_valueLimitCfg = DomainDepotUtils.GetDepotPackValueLimitCfg(self.m_depotId)
end

DomainDepotGoodsTypeCtrl._RefreshPackValueState = HL.Method() << function(self)
    if self.m_selectedItemType == nil or self.m_selectedPackType == nil then
        return
    end

    local limitCfg = self.m_valueLimitCfg[self.m_selectedItemType][self.m_selectedPackType]

    self:_RefreshIncomeValueState(limitCfg.maxLimitValue)
end

DomainDepotGoodsTypeCtrl._RefreshIncomeValueState = HL.Method(HL.Number) << function(self, limitPackValue)
    local itemFactor = Tables.domainDepotDeliverItemTypeTable[self.m_selectedItemType].priceFactor
    local value = math.floor(limitPackValue * itemFactor * self.m_incomeDomainRatio)
    self.view.incomeNumTxt.text = string.format("%d", value)
end


DomainDepotGoodsTypeCtrl._GetResumeEnumValue = HL.Method(HL.Table, HL.Opt(HL.Any)).Return(HL.Any) << function(self, configMap, persistedValue)
    for enumValue, _ in pairs(configMap) do
        if enumValue:GetHashCode() == persistedValue then
            return enumValue
        end
    end
    return nil
end



HL.Commit(DomainDepotGoodsTypeCtrl)
