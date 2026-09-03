local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CraftCell = HL.Class('CraftCell', UIWidgetBase)

CraftCell.incomeCache = HL.Field(HL.Forward('UIListCache'))

CraftCell.outcomeItemsCache = HL.Field(HL.Forward('UIListCache'))

CraftCell.inAddCache = HL.Field(HL.Forward("UIListCache"))

CraftCell.outAddCache = HL.Field(HL.Forward("UIListCache"))

CraftCell.craftInfo = HL.Field(HL.Table)

CraftCell.m_itemTipsPosInfo = HL.Field(HL.Table)

CraftCell.m_onClickItem = HL.Field(HL.Function)


CraftCell._OnFirstTimeInit = HL.Override() << function(self)
    self.incomeCache = UIUtils.genCellCache(self.view.incomeItem)
    self.outcomeItemsCache = UIUtils.genCellCache(self.view.outcomeItem)

    if self.view.inAddImg then
        self.inAddCache = UIUtils.genCellCache(self.view.inAddImg)
    end
    if self.view.outAddImg then
        self.outAddCache = UIUtils.genCellCache(self.view.outAddImg)
    end

    if self.config.ALWAYS_SHOW_ARROW then
        self.view.arrow.gameObject:SetActive(true)
    end

    self:RegisterMessage(MessageConst.ON_PIN_CRAFT_RESP, function()
        self:RefreshPin()
    end)
end

CraftCell.InitCraftCell = HL.Method(HL.Any, HL.Opt(HL.Table, HL.Function)) << function(self, craftInfo, itemTipsPosInfo, onClickItem)
    self:_FirstTimeInit()

    if craftInfo == nil then
        return
    end

    self.m_itemTipsPosInfo = itemTipsPosInfo
    self.m_onClickItem = onClickItem
    self.gameObject.name = "CraftCell" .. craftInfo.craftId
    self.craftInfo = craftInfo
    self:_RefreshCraftCell()
end

CraftCell._RefreshCraftCell = HL.Method() << function(self)
    local craftInfo = self.craftInfo


    if craftInfo.incomes then
        self.incomeCache:Refresh(#craftInfo.incomes, function(cell, index)
            local bundle = craftInfo.incomes[index]
            if self.m_onClickItem then
                cell:InitItem(bundle, function()
                    self.m_onClickItem(cell)
                end)
            else
                cell:InitItem(bundle, true)
            end

            if self.m_itemTipsPosInfo then
                cell:SetExtraInfo(self.m_itemTipsPosInfo)
            end
            cell.gameObject.name = "Income_" .. bundle.id
        end)
    else
        self.incomeCache:Refresh(0)
    end
    if craftInfo.incomeText then
        self.view.incomeText.gameObject:SetActive(true)
        self.view.incomeText.text = craftInfo.incomeText
    else
        self.view.incomeText.gameObject:SetActive(false)
    end
    if self.inAddCache then
        local incomesCount = craftInfo.incomes and #craftInfo.incomes or 0
        if incomesCount > 1 then
            self.inAddCache:Refresh(incomesCount - 1, function(cell, index)
                cell.transform:SetSiblingIndex(index * 2 + 3)
            end)
        else
            self.inAddCache:Refresh(0)
        end
    end


    if craftInfo.time then
        self.view.time.gameObject:SetActive(true)
        if not self.config.ALWAYS_SHOW_ARROW then
            self.view.arrow.gameObject:SetActive(false)
        end
        self.view.time.text = string.format(Language["LUA_CRAFT_CELL_STANDARD_TIME"], FactoryUtils.getCraftTimeStr(craftInfo.time, true))
    else
        self.view.time.gameObject:SetActive(false)
        if not self.config.ALWAYS_SHOW_ARROW then
            self.view.arrow.gameObject:SetActive(true)
        end
    end
    if craftInfo.buildingId then
        self.view.buildingItem.gameObject:SetActive(true)
        local itemId = FactoryUtils.getBuildingItemData(craftInfo.buildingId).id
        self.view.buildingItem:InitItem({ id = itemId }, true)
        if self.m_itemTipsPosInfo then
            self.view.buildingItem:SetExtraInfo(self.m_itemTipsPosInfo)
        end
    else
        self.view.buildingItem.gameObject:SetActive(false)
    end




    if craftInfo.outcomes then
        self.view.outcomeItems.gameObject:SetActive(true)
        self.outcomeItemsCache:Refresh(#craftInfo.outcomes, function(cell, index)
            local bundle = craftInfo.outcomes[index]
            if self.m_onClickItem then
                cell:InitItem(bundle, function()
                    self.m_onClickItem(cell)
                end)
            else
                cell:InitItem(bundle, true)
            end
            if self.m_itemTipsPosInfo then
                cell:SetExtraInfo(self.m_itemTipsPosInfo)
            end
            cell.gameObject.name = "Outcome_" .. bundle.id
        end)
    else
        self.view.outcomeItems.gameObject:SetActive(false)
    end
    if craftInfo.outcomeText then
        self.view.outcomePower.gameObject:SetActive(true)
        self.view.powerText.text = craftInfo.outcomeText
    else
        self.view.outcomePower.gameObject:SetActive(false)
    end
    self.view.outcomeFinish.gameObject:SetActive(craftInfo.useFinish)
    if self.outAddCache then
        local outcomesCount = craftInfo.outcomes and #craftInfo.outcomes or 0
        if outcomesCount > 1 then
            self.outAddCache:Refresh(outcomesCount - 1, function(cell, index)
                cell.transform:SetSiblingIndex(index * 2 + 2)
            end)
        else
            self.outAddCache:Refresh(0)
        end
    end


    local showPin = self.config.SHOW_PIN_BTN and not string.isEmpty(craftInfo.craftId) and
        Tables.factoryMachineCraftTable:ContainsKey(craftInfo.craftId)
    self.view.pinBtn.gameObject:SetActive(showPin)
    if showPin then
        self.view.pinBtn:InitPinBtn(craftInfo.craftId, GEnums.FCPinPosition.Formula:GetHashCode())
    end


    if self.config.SHOW_RED_DOT then
        local hasRedDot = RedDotUtils.hasCraftRedDot(craftInfo.craftId)
        self.view.redDot.gameObject:SetActive(hasRedDot)
    else
        self.view.redDot.gameObject:SetActive(false)
    end


    local success, formulaTableData = Tables.factoryMachineCraftTable:TryGetValue(craftInfo.craftId)
    if self.view.config.SHOW_DESC and success then
        self.view.craftDescTxt.text = formulaTableData.formulaDesc
        self.view.craftDescNode.gameObject:SetActive(true)
    else
        self.view.craftDescNode.gameObject:SetActive(false)
    end


    self.view.sewageNode.gameObject:SetActive(craftInfo.isSewageCraft == true)
    self.view.timeNode.gameObject:SetActive(craftInfo.isSewageCraft ~= true)


    if craftInfo.env and craftInfo.env ~= GEnums.FacEnvGenEnvType.None then
        self.view.envTitleNode.gameObject:SetActive(true)
        self.view.envTitleStateController:SetState(craftInfo.env:ToString())
    else
        self.view.envTitleNode.gameObject:SetActive(false)
    end


    if craftInfo.genEnv and craftInfo.genEnv ~= GEnums.FacEnvGenEnvType.None then
        self.view.outcomeEnv.gameObject:SetActive(true)
        self.view.envGenBg.gameObject:SetActive(true)
        self.view.outcomeEnv:SetState(craftInfo.genEnv:ToString())
    else
        self.view.outcomeEnv.gameObject:SetActive(false)
        self.view.envGenBg.gameObject:SetActive(false)
    end
    if craftInfo.consumeRate then
        self.view.middleNode.gameObject:SetActive(false)
        self.view.envTipsNode.gameObject:SetActive(true)
        self.view.genEnvTimeTxt.text = I18nUtils.CombineStringWithLanguageSpilt(craftInfo.consumeRate, Language["ui_fac_common_minute_speed"])
    else
        self.view.middleNode.gameObject:SetActive(true)
        self.view.envTipsNode.gameObject:SetActive(false)
    end
end

CraftCell.SetSelectedState = HL.Method(HL.Boolean, HL.Boolean) << function(self, isSelected, isBlocked)
    self.view.normalNode.gameObject:SetActive(not isSelected)
    self.view.selectNode.gameObject:SetActive(isSelected)

    if isSelected then
        self.view.selectTitleNode.gameObject:SetActive(not isBlocked)
        self.view.blockTitleNode.gameObject:SetActive(isBlocked)
    end

    local color = isSelected and self.config.COLOR_HIGHLIGHT_MIDDLE or self.config.COLOR_NORMAL_MIDDLE
    self.view.arrow.color = color
    self.view.time.color = color
end

HL.Commit(CraftCell)
return CraftCell
