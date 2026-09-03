
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharExpandList

CharExpandListCtrl = HL.Class('CharExpandListCtrl', uiCtrl.UICtrl)





CharExpandListCtrl.s_messages = HL.StaticField(HL.Table) << {

}

CharExpandListCtrl.m_getCharHeadCell = HL.Field(HL.Function)

CharExpandListCtrl.m_charInfoList = HL.Field(HL.Table)

CharExpandListCtrl.m_onCharListChanged = HL.Field(HL.Function)

CharExpandListCtrl.m_charInfo = HL.Field(HL.Table)

CharExpandListCtrl.m_skipGraduallyShow = HL.Field(HL.Boolean) << false

CharExpandListCtrl.m_args = HL.Field(HL.Table)

CharExpandListCtrl.m_naviTargetInitialized = HL.Field(HL.Boolean) << false








CharExpandListCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_args = arg
    self.m_charInfo = self.m_args.charInfo
    self.m_charInfoList = self.m_args.charInfoList
    self.view.emptyCloseBtn.onClick:AddListener(function()
        self:Notify(MessageConst.CHAR_INFO_EMPTY_BUTTON_CLICK)
    end)

    self.m_getCharHeadCell = UIUtils.genCachedCellFunction(self.view.charHeadCell)

    local stateArg = self.m_args.stateArg
    local csSortIndex
    if stateArg and stateArg.sortSelectedIndex then
        csSortIndex = CSIndex(stateArg.sortSelectedIndex)
    end
    local isIncremental = false
    if stateArg and stateArg.sortIsIncremental ~= nil then
        isIncremental = stateArg.sortIsIncremental
    end
    self.view.sortNode:InitSortNode(UIConst.CHAR_FORMATION_LIST_SORT_OPTION, function(optData, isIncremental)
        self:_OnSortChanged(optData, isIncremental)
    end, csSortIndex, isIncremental)

    self.view.charScrollList.onUpdateCell:AddListener(function(object, csIndex)
        self:_UpdateCharScrollListCell(object, csIndex)
        if self.m_args.refreshAddon then
            local info = self.m_charInfoList[LuaIndex(csIndex)]

        end
    end)
    self.view.charScrollList.onSelectedCell:AddListener(function(obj, csIndex)
        self:_OnClickCell(csIndex)
    end)
    self.view.charScrollList.getCurSelectedIndex = function()
        if self.m_charInfo then
            for k, info in ipairs(self.m_charInfoList) do
                if info.templateId == self.m_charInfo.templateId then
                    return CSIndex(k)
                end
            end
        end
        return -1
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

CharExpandListCtrl.OnShow = HL.Override() << function(self)
    self.m_naviTargetInitialized = false
    self:RefreshCharExpandList(self.m_charInfo, self.m_charInfoList)
end



CharExpandListCtrl.RefreshCharExpandList = HL.Method(HL.Opt(HL.Table, HL.Table, HL.Boolean)) << function(self, charInfo, charInfoList, skipGraduallyShow)
    self.m_charInfo = charInfo
    self.m_charInfoList = charInfoList
    self.m_skipGraduallyShow = skipGraduallyShow or false

    self.view.sortNode:SortCurData()
end

CharExpandListCtrl._RefreshCharList = HL.Method() << function(self)
    if self.m_skipGraduallyShow then
        self.view.charScrollList:UpdateCount(#self.m_charInfoList, false, false, false, self.m_skipGraduallyShow)
    else
        local fastScrollToIndex = -1
        if self.m_charInfo then
            for k, info in ipairs(self.m_charInfoList) do
                if info.instId == self.m_charInfo.instId then
                    fastScrollToIndex = CSIndex(k)
                    break
                end
            end
        end
        if DeviceInfo.usingController then
            self.naviGroup:SetLayerSelectedTarget(nil, true)
            self.m_naviTargetInitialized = false
        end
        self.view.charScrollList:UpdateCount(#self.m_charInfoList, fastScrollToIndex, false, false, self.m_skipGraduallyShow)
    end
    self.m_skipGraduallyShow = false
end

CharExpandListCtrl._OnSortChanged = HL.Method(HL.Table, HL.Boolean) << function(self, optData, isIncremental)
    if self.m_charInfoList then
        local keys = isIncremental and optData.keys or optData.reverseKeys
        self:_SortData(keys, isIncremental)
    end
end

CharExpandListCtrl._SortData = HL.Method(HL.Table, HL.Boolean) << function(self, keys, isIncremental)
    if self.m_charInfoList then
        table.sort(self.m_charInfoList, Utils.genSortFunction(keys, isIncremental))
        self:_RefreshCharList()
    end
end

CharExpandListCtrl._UpdateCharScrollListCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, csIndex)
    local info = self.m_charInfoList[LuaIndex(csIndex)]
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(info.instId)
    local templateId = info.templateId
    local charCfg = Tables.characterTable[templateId]
    local cell = self.m_getCharHeadCell(object)

    cell:InitCharFormationHeadCell({
        instId = charInst.instId,
        level = charInst.level,
        ownTime = charInst.ownTime,
        rarity = charCfg.rarity,
        templateId = templateId,
        noHpBar = true,
        isSingleSelect = info.isSingleSelect,
        slotIndex = info.slotIndex,
    }, function()
        self:_OnClickCell(csIndex)
    end)
    cell:SetSingleModeSelected(true)
    cell.view.redDot:InitRedDot("CharInfo", charInst.instId)
    cell.view.tryoutTips.gameObject:SetActive(info.isShowTrail)
    cell.view.fixedTips.gameObject:SetActive(info.isShowFixed)

    if self.m_args.refreshAddon then
        self.m_args.refreshAddon(cell, info)
    end

    if DeviceInfo.usingController and not self.m_naviTargetInitialized and charInst.instId == self.m_charInfo.instId then
        self:SetNaviTarget(cell.view.button)
        self.m_naviTargetInitialized = true
    end
end

CharExpandListCtrl._OnClickCell = HL.Method(HL.Number) << function(self, csIndex)
    local info = self.m_charInfoList[LuaIndex(csIndex)]
    if self.m_args.onClickCell then
        self.m_args.onClickCell(info)
    end
end

CharExpandListCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    local arg = {}
    arg.sortSelectedIndex = self.view.sortNode:GetCurSelectedIndex()
    arg.sortIsIncremental = self.view.sortNode.isIncremental
    return arg
end

HL.Commit(CharExpandListCtrl)
