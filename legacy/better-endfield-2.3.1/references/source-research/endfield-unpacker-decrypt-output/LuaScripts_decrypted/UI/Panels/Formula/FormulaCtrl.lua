local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Formula
FormulaCtrl = HL.Class('FormulaCtrl', uiCtrl.UICtrl)

local NONE_CRAFTER_MODE_MAP_KEY = "normal"

local CURR_FORMULA_MODE_SORT_ID = -1

local MAX_SHOW_ITEM_COUNT = 2

FormulaCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SET_FORMULA_VALID_PIN_BTN] = '_OnSetFormulaValidPinBtn',
}

FormulaCtrl.m_getCell = HL.Field(HL.Function)

FormulaCtrl.m_modeFormulaInfoList = HL.Field(HL.Table)

FormulaCtrl.m_highlightFormulaIdList = HL.Field(HL.Table)

FormulaCtrl.m_blockFormulaIdList = HL.Field(HL.Table)

FormulaCtrl.m_readFormulaIds = HL.Field(HL.Table)

FormulaCtrl.m_isMachineCrafterFormula = HL.Field(HL.Boolean) << false

FormulaCtrl.m_belongingCanvasGroup = HL.Field(HL.Userdata)

FormulaCtrl.m_extraFormulaSpeed = HL.Field(HL.Number) << 1

FormulaCtrl.m_currFocusCellGroup = HL.Field(HL.Userdata)

FormulaCtrl.m_currFormulaMode = HL.Field(HL.String) << ""

FormulaCtrl.m_buildingId = HL.Field(HL.String) << ""


FormulaCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateModeCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)
    self.view.scrollList.getCellSize = function(csIndex)
        return self:_GetModeCellSize(LuaIndex(csIndex))
    end
    self.view.scrollList.getCellName = function(csIndex)
        return "ModeGroupCell" .. LuaIndex(csIndex)
    end
    self.view.closeBtn.onClick:AddListener(function()
        self:_CloseSelf()
    end)
    self.view.closeFullBtn.onClick:AddListener(function()
        self:_CloseSelf()
    end)
end

FormulaCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if not active then
        if self.m_currFocusCellGroup ~= nil then
            self.m_currFocusCellGroup:ManuallyStopFocus()
        end
    end
end

FormulaCtrl.ShowFormula = HL.StaticMethod(HL.Table) << function(args)
    if args == nil then
        return
    end

    local buildingId = args.buildingId
    local nodeId = args.nodeId
    local extraSpeed = args.extraSpeed or 1
    local self = UIManager:AutoOpen(PANEL_ID)
    UIManager:SetTopOrder(PANEL_ID)
    self.m_buildingId = buildingId
    CS.Beyond.Gameplay.Conditions.OnFormulaPanelOpen.Trigger(self.m_buildingId, false)
    self.m_belongingCanvasGroup = args.belongingCanvasGroup
    self.m_highlightFormulaIdList = args.highlightFormulaIdList or {}
    self.m_blockFormulaIdList = args.blockFormulaIdList or {}
    self.m_isMachineCrafterFormula = args.isMachineCrafterFormula
    self.m_currFormulaMode = args.machineCrafterFormulaMode or ""
    self.m_extraFormulaSpeed = extraSpeed
    self:_InitFormula(buildingId, nodeId)
end

FormulaCtrl.OnAnimationInFinished = HL.Override() << function(self)
    if string.isEmpty(self.m_buildingId) then
        return
    end
    CS.Beyond.Gameplay.Conditions.OnFormulaPanelOpen.Trigger(self.m_buildingId, true)
end

FormulaCtrl._InitFormula = HL.Method(HL.String, HL.Number) << function(self, buildingId, nodeId)
    self.m_readFormulaIds = {}
    self.m_modeFormulaInfoList = {}

    local success, buildingTableData = Tables.factoryBuildingTable:TryGetValue(buildingId)
    local isEnvGen = false
    if success then
        self.view.buildingNameTxt.text = buildingTableData.name
        isEnvGen = buildingTableData.type == GEnums.FacBuildingType.EnvGenWithActivator
    end
    self.view.titleText.text = isEnvGen and Language.LUA_FACTORY_ENV_GEN_FORMULA_PANEL_TITLE or Language["ui_fac_common_formula_mainpanel_title"]

    self:_InitFormulaInfo(nodeId)
    self:_ShowFormula()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

FormulaCtrl._InitFormulaInfo = HL.Method(HL.Number) << function(self, nodeId)
    local crafts = FactoryUtils.getBuildingCraftsWithNodeId(nodeId, false, false, true)
    if not crafts or not next(crafts) then
        return
    end

    local needCoverModeList = {}
    if self.m_isMachineCrafterFormula then
        local node = FactoryUtils.getBuildingNodeHandler(nodeId)
        local domain = ScopeUtil.ChapterIdInt2Str(node.belongChapter.chapterId)
        local domainSuccess, domainCfg = Tables.domainDataTable:TryGetValue(domain)

        local isModeValid = function(mode)
            if domainSuccess then
                for index = 0, domainCfg.machineModeTypeGroup.Count - 1 do
                    local domainMode = domainCfg.machineModeTypeGroup[index]
                    if domainMode == mode then
                        return true
                    end
                end
                return false
            else
                return true
            end
        end

        local modesTempList = {}
        for _, craftInfo in ipairs(crafts) do
            local mode = craftInfo.formulaMode
            if isModeValid(mode) then
                if modesTempList[mode] == nil then
                    local modeCfg = Tables.factoryMachineCraftModeTable[mode]
                    local coverSuccess, coverData = Tables.factoryMachineCraftModeCoverTable:TryGetValue(mode)
                    if coverSuccess then
                        for index = 0, coverData.list.Count - 1 do
                            needCoverModeList[coverData.list[index].machineModeCoverType] = true
                        end
                    end
                    modesTempList[mode] = {
                        mode = mode,
                        sortId = mode == self.m_currFormulaMode and CURR_FORMULA_MODE_SORT_ID or modeCfg.sortId,
                        iconId = modeCfg.iconId,
                        name = modeCfg.machineModeTypeName,
                        isEnvRelated = FactoryUtils.checkIsBuildingModeEnvRelated(node.templateId, mode),
                        formulaInfoList = {},
                    }
                end
                table.insert(modesTempList[craftInfo.formulaMode].formulaInfoList, craftInfo)
            end
        end

        for needCoverMode, _ in pairs(needCoverModeList) do
            modesTempList[needCoverMode] = nil
        end

        for _, modeFormulaData in pairs(modesTempList) do
            modeFormulaData.formulaInfoList = self:_ParseSingleModeFormulaInfoList(modeFormulaData.formulaInfoList)
            table.insert(self.m_modeFormulaInfoList, modeFormulaData)
        end
        table.sort(self.m_modeFormulaInfoList, Utils.genSortFunction({ "sortId" }, true))
    else

        self.m_modeFormulaInfoList = {
            {
                mode = NONE_CRAFTER_MODE_MAP_KEY,
                formulaInfoList = self:_ParseSingleModeFormulaInfoList(crafts),
            }
        }
    end
end

FormulaCtrl._ParseSingleModeFormulaInfoList = HL.Method(HL.Table).Return(HL.Table) << function(self, crafts)

    local formulaInfoList = {}
    local blockCrafts = {}
    local normalCrafts = {}
    for _, craftInfo in ipairs(crafts) do
        local isHighlighted = self:_IsFormulaHighlighted(craftInfo.craftId)
        if isHighlighted then
            table.insert(formulaInfoList, craftInfo)
        else
            local isBlocked = self:_IsFormulaBlocked(craftInfo.craftId)
            if isBlocked then
                table.insert(blockCrafts, craftInfo)
            else
                table.insert(normalCrafts, craftInfo)
            end
        end
    end
    for _, craftInfo in ipairs(blockCrafts) do
        table.insert(formulaInfoList, craftInfo)
    end
    for _, craftInfo in ipairs(normalCrafts) do
        table.insert(formulaInfoList, craftInfo)
    end

    return formulaInfoList
end

FormulaCtrl._ShowFormula = HL.Method() << function(self)
    self.view.scrollList:UpdateCount(#self.m_modeFormulaInfoList)
end

FormulaCtrl._IsFormulaHighlighted = HL.Method(HL.String).Return(HL.Boolean) << function(self, formulaId)
    for _, highlightId in pairs(self.m_highlightFormulaIdList) do
        if formulaId == highlightId then
            return true
        end
    end
    return false
end

FormulaCtrl._IsFormulaBlocked = HL.Method(HL.String).Return(HL.Boolean) << function(self, formulaId)
    for _, blockId in pairs(self.m_blockFormulaIdList) do
        if formulaId == blockId then
            return true
        end
    end
    return false
end

FormulaCtrl._OnUpdateModeCell = HL.Method(HL.Table, HL.Number) << function(self, modeGroupCell, modeIndex)
    local modeFormulaInfo = self.m_modeFormulaInfoList[modeIndex]
    modeFormulaInfo.modeIndex = modeIndex

    if self.m_isMachineCrafterFormula and #self.m_modeFormulaInfoList > 1 then
        modeGroupCell.titleNode.gameObject:SetActive(true)
        modeGroupCell.iconImg:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_COMMON, modeFormulaInfo.iconId)
        modeGroupCell.nameTxt.text = modeFormulaInfo.name
        modeGroupCell.currentModeNode.gameObject:SetActive(modeFormulaInfo.mode == self.m_currFormulaMode)
    else
        modeGroupCell.titleNode.gameObject:SetActive(false)
    end
    modeGroupCell.modeCellsCtrl:SetState(modeFormulaInfo.isEnvRelated and "Title" or "Normal")

    if modeGroupCell.formulaCells == nil then
        modeGroupCell.formulaCells = UIUtils.genCellCache(modeGroupCell.modeCell)
        modeGroupCell.formulaCells:Refresh(#modeFormulaInfo.formulaInfoList, function(cell, index)
            self:_OnUpdateFormulaCell(cell, index, modeFormulaInfo.formulaInfoList[index], modeFormulaInfo)
        end)
    end
end

FormulaCtrl._GetModeCellSize = HL.Method(HL.Number).Return(HL.Number) << function(self, modeIndex)
    local modeFormulaInfo = self.m_modeFormulaInfoList[modeIndex]
    local cellsCount = #modeFormulaInfo.formulaInfoList
    local modeCell = self.view.formulaModeCell
    modeCell.modeCellsCtrl:SetState(modeFormulaInfo.isEnvRelated and "Title" or "Normal")
    local formulaCellLayout = modeCell.modeCellsGridLayout
    local basicHeight = modeCell.verticalLayoutGroup.spacing + modeCell.verticalLayoutGroup.padding.vertical
    local cellHeight = formulaCellLayout.spacing.y + formulaCellLayout.cellSize.y + formulaCellLayout.padding.vertical
    return basicHeight + modeCell.titleNodeLayoutElement.preferredHeight + cellHeight * math.ceil(cellsCount / 2)
end

FormulaCtrl._OnUpdateFormulaCell = HL.Method(HL.Table, HL.Number, HL.Table, HL.Table) << function(self, cell, index, formulaInfo, modeInfo)
    local formulaCell = cell.formulaCell
    local isSpecialFormula = string.isEmpty(formulaInfo.craftId) or not Tables.factoryMachineCraftTable:ContainsKey(formulaInfo.craftId)


    if formulaInfo.time ~= nil then
        formulaInfo.time = formulaInfo.time * self.m_extraFormulaSpeed
        formulaCell.time.text = string.format(Language["LUA_CRAFT_CELL_STANDARD_TIME"], FactoryUtils.getCraftTimeStr(formulaInfo.time, true))
        formulaCell.timeNode.gameObject:SetActive(true)
    else
        formulaCell.timeNode.gameObject:SetActive(false)
    end


    local descSuccess, formulaTableData = Tables.factoryMachineCraftTable:TryGetValue(formulaInfo.craftId)
    if descSuccess then
        formulaCell.craftDescTxt.text = formulaTableData.formulaDesc
    end
    formulaCell.craftDescTxt.gameObject:SetActive(descSuccess)
    formulaCell.titleIcon.gameObject:SetActive(descSuccess)


    if formulaCell.timeLimitFormulaNode then
        if formulaInfo.craftId ~= nil then
            formulaCell.timeLimitFormulaNode.gameObject:SetActive(FactoryUtils.isTimeLimitedFormula(formulaInfo.craftId))
            FactoryUtils.setTimeLimitedFormulaTagColor(formulaCell.timeLimitedColorTag1, formulaInfo.craftId)
            FactoryUtils.setTimeLimitedFormulaTagColor(formulaCell.timeLimitedColorTag2, formulaInfo.craftId)
        else
            formulaCell.timeLimitFormulaNode.gameObject:SetActive(false)
        end
    end


    local isHighlighted = self:_IsFormulaHighlighted(formulaInfo.craftId)
    local isBlocked = self:_IsFormulaBlocked(formulaInfo.craftId)
    formulaCell.selectStateController:SetState(isHighlighted and "Selected" or "Normal")
    formulaCell.pinStateController:SetState(isSpecialFormula and "NoPin" or "Normal")
    if isHighlighted then
        formulaCell.selectTitleNode.gameObject:SetActive(not isBlocked)
        formulaCell.blockTitleNode.gameObject:SetActive(isBlocked)
    else
        formulaCell.selectTitleNode.gameObject:SetActive(false)
        formulaCell.blockTitleNode.gameObject:SetActive(false)
    end
    local color = isHighlighted and self.view.config.HIGHLIGHT_CELL_TIME_COLOR or self.view.config.NORMAL_CELL_TIME_COLOR
    formulaCell.timeNode.color = color


    if cell.incomeCells == nil then
        cell.incomeCells = UIUtils.genCellCache(formulaCell.incomeItem)
    end
    local incomeCells = cell.incomeCells

    local minIncomesCount = (formulaInfo.genEnv == nil) and MAX_SHOW_ITEM_COUNT or 1
    incomeCells:Refresh(math.max(#formulaInfo.incomes, minIncomesCount), function(incomeCell, incomeIndex)
        local showItem = incomeIndex <= #formulaInfo.incomes
        if showItem then
            local bundle = formulaInfo.incomes[incomeIndex]
            incomeCell.item:InitItem(bundle, true)
            incomeCell.item:SetEnableHoverTips(not DeviceInfo.usingController)
            incomeCell.item.gameObject.name = "Item_" .. bundle.id
            incomeCell.gameObject.name = "Income_" .. bundle.id
            if DeviceInfo.usingController then
                local tipsPosType = index % 2 == 0 and UIConst.UI_TIPS_POS_TYPE.LeftMid or UIConst.UI_TIPS_POS_TYPE.RightMid
                incomeCell.item:SetExtraInfo({
                    tipsPosType = tipsPosType,
                    tipsPosTransform = formulaCell.controllerHintNode,
                    isSideTips = true,
                })
            end
        end
        incomeCell.item.gameObject:SetActive(showItem)
        incomeCell.emptyNode.gameObject:SetActive(not showItem)
        incomeCell.emptyNode.color = isHighlighted and self.view.config.HIGHLIGHT_CELL_EMPTY_COLOR or self.view.config.NORMAL_CELL_EMPTY_COLOR
    end)


    if formulaInfo.outcomes then
        if cell.outcomeCells == nil then
            cell.outcomeCells = UIUtils.genCellCache(formulaCell.outcomeItem)
        end
        local outcomeCells = cell.outcomeCells
        outcomeCells:Refresh(math.max(#formulaInfo.outcomes, MAX_SHOW_ITEM_COUNT), function(outcomeCell, outcomeIndex)
            local showItem = outcomeIndex <= #formulaInfo.outcomes
            if showItem then
                local bundle = formulaInfo.outcomes[outcomeIndex]
                outcomeCell.item:InitItem(bundle, true)
                outcomeCell.item:SetEnableHoverTips(not DeviceInfo.usingController)
                outcomeCell.item.gameObject.name = "Item_" .. bundle.id
                outcomeCell.gameObject.name = "Outcome_" .. bundle.id
                if DeviceInfo.usingController then
                    local tipsPosType = index % 2 == 0 and UIConst.UI_TIPS_POS_TYPE.LeftMid or UIConst.UI_TIPS_POS_TYPE.RightMid
                    outcomeCell.item:SetExtraInfo({
                        tipsPosType = tipsPosType,
                        tipsPosTransform = formulaCell.controllerHintNode,
                        isSideTips = true,
                    })
                end
            end
            outcomeCell.item.gameObject:SetActive(showItem)
            outcomeCell.emptyNode.gameObject:SetActive(not showItem)
            outcomeCell.emptyNode.color = isHighlighted and self.view.config.HIGHLIGHT_CELL_EMPTY_COLOR or self.view.config.NORMAL_CELL_EMPTY_COLOR
        end)
        formulaCell.outcomeItems.gameObject:SetActive(true)
    else
        formulaCell.outcomeItems.gameObject:SetActive(false)
    end
    if formulaInfo.outcomeText then
        formulaCell.outcomePower.gameObject:SetActive(true)
        formulaCell.powerText.text = formulaInfo.outcomeText
    else
        formulaCell.outcomePower.gameObject:SetActive(false)
    end
    formulaCell.outcomeFinish.gameObject:SetActive(formulaInfo.useFinish)


    if not isSpecialFormula then
        formulaCell.pinBtn:InitPinBtn(formulaInfo.craftId, GEnums.FCPinPosition.Formula:GetHashCode())
    end
    formulaCell.pinBtn.view.gameObject:SetActive(not isSpecialFormula)
    formulaCell.invalidPinBtn.gameObject:SetActive(isSpecialFormula)


    self.m_readFormulaIds[formulaInfo.craftId] = true
    if Utils.isInBlackbox() then
        cell.redDot.gameObject:SetActive(false)
    else
        cell.redDot:InitRedDot("Formula", formulaInfo.craftId)
        local hasRedDot = RedDotUtils.hasCraftRedDot(formulaInfo.craftId)
        cell.redDot.gameObject:SetActive(hasRedDot)
    end


    formulaCell.sewageNode.gameObject:SetActive(formulaInfo.isSewageCraft == true)


    if modeInfo.isEnvRelated then
        formulaCell.envTitleStateControlelr:SetState(formulaInfo.env:ToString())
    else
        formulaCell.envTitleStateControlelr:SetState("Empty")
    end


    if formulaInfo.genEnv and formulaInfo.genEnv ~= GEnums.FacEnvGenEnvType.None then
        formulaCell.outcomeEnv.gameObject:SetActive(true)
        formulaCell.outcomeEnv:SetState(formulaInfo.genEnv:ToString())

        local envDisplayConfig = FacConst.FAC_ENV_DISPLAY_CONFIG[formulaInfo.genEnv]
        formulaCell.craftDescTxt.text = Language[envDisplayConfig.textKey]
        formulaCell.craftDescTxt.gameObject:SetActive(true)
        formulaCell.titleIcon.gameObject:SetActive(true)

        formulaCell.pinStateController:SetState("EnvGen")
    else
        formulaCell.outcomeEnv.gameObject:SetActive(false)
    end
    if formulaInfo.consumeRate then
        formulaCell.envTipsNode.gameObject:SetActive(true)
        formulaCell.middleNode.gameObject:SetActive(false)
        formulaCell.genEnvTimeTxt.text = I18nUtils.CombineStringWithLanguageSpilt(formulaInfo.consumeRate, Language["ui_fac_common_minute_speed"])
    else
        formulaCell.middleNode.gameObject:SetActive(true)
        formulaCell.envTipsNode.gameObject:SetActive(false)
    end

    cell.gameObject.name = formulaInfo.craftId


    if DeviceInfo.usingController then
        formulaCell.contentNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
            if isFocused then
                self.m_currFocusCellGroup = formulaCell.contentNaviGroup
            else
                self.m_currFocusCellGroup = nil
            end
        end)
        if modeInfo.modeIndex == 1 and index == 1 then
            self:SetNaviTarget(formulaCell.inputGroupDecorator)
        end
    end
end

FormulaCtrl._ReadFormulas = HL.Method() << function(self)
    if not self.m_isMachineCrafterFormula then
        return
    end

    if not next(self.m_readFormulaIds) then
        return
    end

    local formulaIds = {}
    for k, _ in pairs(self.m_readFormulaIds) do
        table.insert(formulaIds, k)
    end
    self.m_readFormulaIds = {}

    GameInstance.player.remoteFactory.core:ReadFormula(formulaIds)
end

FormulaCtrl._OnSetFormulaValidPinBtn = HL.Method(HL.Any) << function(self, args)
    local formulaId = unpack(args)

    for modeIndex, modeFormulaInfo in ipairs(self.m_modeFormulaInfoList) do
        local modeGroupCell = self.m_getCell(modeIndex)
        if modeGroupCell and modeGroupCell.formulaCells then
            local formulaCells = modeGroupCell.formulaCells
            for cellIndex = 1, formulaCells:GetCount() do
                local cell = formulaCells:GetItem(cellIndex)
                if cell then
                    local formulaInfo = modeFormulaInfo.formulaInfoList[cellIndex]
                    if formulaInfo and formulaInfo.craftId ~= formulaId then
                        local formulaCell = cell.formulaCell
                        if formulaCell and formulaCell.pinBtn then
                            InputManagerInst:ToggleBinding(formulaCell.pinBtn.view.pinToggle.toggleBindingId, false)
                        end
                    end
                end
            end
        end
    end
end

FormulaCtrl.GetRecoverStateArg = HL.Method(HL.Number, HL.String).Return(HL.Table) << function(self, nodeId, buildingId)
    return {
        nodeId = nodeId,
        buildingId = buildingId,
        extraSpeed = self.m_extraFormulaSpeed,
        isMachineCrafterFormula = self.m_isMachineCrafterFormula,
        machineCrafterFormulaMode = self.m_currFormulaMode,
        highlightFormulaIdList = self.m_highlightFormulaIdList and lume.deepCopy(self.m_highlightFormulaIdList) or {},
        blockFormulaIdList = self.m_blockFormulaIdList and lume.deepCopy(self.m_blockFormulaIdList) or {},
    }
end

FormulaCtrl._CloseSelf = HL.Method() << function(self)
    self:_ReadFormulas()
    self:PlayAnimationOutWithCallback(function()
        self:Close()
    end)
end

HL.Commit(FormulaCtrl)
