
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GemCustomizationBox
local MAX_TERM_GROUP_INDEX = 3
local MAX_SHOW_TAB_NUM = 3


local tabAniStrLeftFormat = "gemcustombox_slccell_left_%sto%s"

local tabAniStrRightFormat = "gemcustombox_slccell_right_%sto%s"

GemCustomizationBoxCtrl = HL.Class('GemCustomizationBoxCtrl', uiCtrl.UICtrl)

GemCustomizationBoxCtrl.m_boxItemId = HL.Field(HL.String) << ""

GemCustomizationBoxCtrl.m_boxNumber = HL.Field(HL.Number) << 5

GemCustomizationBoxCtrl.m_tabNum = HL.Field(HL.Number) << 3

GemCustomizationBoxCtrl.m_termGroupList = HL.Field(HL.Table)

GemCustomizationBoxCtrl.m_termGroupNum = HL.Field(HL.Number) << 3

GemCustomizationBoxCtrl.m_genTermGroupCellFunc = HL.Field(HL.Function)

GemCustomizationBoxCtrl.m_genTagGroupCellFunc = HL.Field(HL.Function)

GemCustomizationBoxCtrl.m_selectTabCells = HL.Field(HL.Forward("UIListCache"))

GemCustomizationBoxCtrl.m_currCanCustomizeTermTypeIndexList = HL.Field(HL.Table)

GemCustomizationBoxCtrl.m_currSelectInfo = HL.Field(HL.Table)

GemCustomizationBoxCtrl.m_lastRefreshUICurrTabIndex = HL.Field(HL.Number) << 0





GemCustomizationBoxCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_GEMCUSTOMIZATIONBOX_TERM_SELECT] = '_OnTermClick',
    [MessageConst.ON_SC_ITEM_BAG_USE_ITEM_GEMLOCKEDTERMBOX] = '_OnReceiveServer',
}


GemCustomizationBoxCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if arg ~= nil then
        self.m_boxItemId = arg
    end
    self:_BindBtnCallbacks()
    self:_InitData()
    self:_InitUIData()
    self:_RefreshUI()


    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

GemCustomizationBoxCtrl.OnShow = HL.Override() << function(self)

end









GemCustomizationBoxCtrl._InitData = HL.Method() << function(self)
    local data = Tables.GemCustomizationBox[self.m_boxItemId]
    self.m_tabNum = data.lockedTermCount

    self.m_termGroupList = {}
    local gemItemId = data.gemItemId
    local termPoolIdData = Tables.GemItemId2TermPoolIdDataTable[gemItemId]
    local poolId1 = termPoolIdData.termPoolId1;
    local poolId2 = termPoolIdData.termPoolId2;
    local poolId3 = termPoolIdData.termPoolId3;
    local poolIdList = {poolId1, poolId2, poolId3}
    self.m_termGroupNum = 0
    for index, poolId in ipairs(poolIdList) do
        local gemTermIdListData = Tables.TermPoolId2TermPoolIdDataTable[poolId]
        if gemTermIdListData == nil then
            self.m_termGroupList[index] = {}
        else
            local termIdList = {}
            for j = 0, #gemTermIdListData.gemTermIdList - 1 do
                local termId = gemTermIdListData.gemTermIdList[j]
                table.insert(termIdList, termId)
            end
            self.m_termGroupList[index] = termIdList
            self.m_termGroupNum = self.m_termGroupNum + 1
        end
    end

    self.m_currSelectInfo = {}
    self.m_currSelectInfo["selectTabIndex"] = 1
    self.m_currSelectInfo["selectTermIds"] = {}
    self.m_currSelectInfo["eachTabCanSelectTermTypeIndex"] = {}
    self.m_currSelectInfo["seeAllTermGroup"] = false
    self.m_currSelectInfo["haveRefreshPreviewMode"] = false
    self.m_currSelectInfo["haveSetNaviTarget"] = false
    self.m_currSelectInfo["naviSelectTermIds"] = {}
    local tmpTermTypeLists = {}
    tmpTermTypeLists[1] = data.term1Type;
    tmpTermTypeLists[2] = data.term2Type;
    tmpTermTypeLists[3] = data.term3Type;
    for i = 1, 3 do
        local tmpTermTypeList = tmpTermTypeLists[i]
        self.m_currSelectInfo["eachTabCanSelectTermTypeIndex"][i] = {}
        for j = 0, #tmpTermTypeList - 1 do
            local termType = tmpTermTypeList[j]

            local termTypeIndex = GemCustomizationBoxCtrl._TermType2LuaIndex(termType)
            local termGroup = self.m_termGroupList[termTypeIndex]
            if termGroup ~= nil and #termGroup > 0 then
                table.insert(self.m_currSelectInfo["eachTabCanSelectTermTypeIndex"][i], termTypeIndex)
            end
        end
    end

    local itemNum = Utils.getItemCount(self.m_boxItemId, true, true)
    self.m_currSelectInfo["haveBoxNumber"] = itemNum;
    local config_max_use_num = self.view.config.MAX_USE_NUM;
    self.m_currSelectInfo["useBoxMaxNumber"] = math.min(itemNum, config_max_use_num)
end

GemCustomizationBoxCtrl._InitUIData = HL.Method() << function(self)
    self.m_selectTabCells = UIUtils.genCellCache(self.view.gemCustomizationBoxSelectTabCell)
    self:_InitTermGroups()
    self:_InitUITagGroups()
    self.view.numberSelector:InitNumberSelector(1, 1, self.m_currSelectInfo["useBoxMaxNumber"])
end

GemCustomizationBoxCtrl._BindBtnCallbacks = HL.Method() << function(self)
    self.view.closeButton.onClick:AddListener(function()
        if self.m_currSelectInfo["seeAllTermGroup"] then

            self.m_currSelectInfo["seeAllTermGroup"] = false
            self:_RefreshUI()
        else
            self:PlayAnimationOut()
        end
    end)
    self.view.previewBtn.onClick:AddListener(function()
        self.m_currSelectInfo["seeAllTermGroup"] = true
        self:_RefreshUI()
    end)
    self.view.confirmBtnYes.button.onClick:AddListener(function()
        self:_OnConfirmBtnClick()
    end)

    self:BindInputPlayerAction("common_toggle_group_previous_include_pc", function()
        self:_TabMoveLeft()
    end)
    self:BindInputPlayerAction("common_toggle_group_next_include_pc", function()
        self:_TabMoveRight()
    end)
end

GemCustomizationBoxCtrl._RefreshUI = HL.Method() << function(self)
    if self.m_currSelectInfo["seeAllTermGroup"] then

        self.view.themeNode:SetState("preview")
        self:_RefreshUIPreviewMode()
    else

        self.view.themeNode:SetState("box")
        self:_RefreshUISelectMode()
    end
end

GemCustomizationBoxCtrl._RefreshUISelectMode = HL.Method() << function(self)
    local haveChangeTab = self.m_currSelectInfo["selectTabIndex"] ~= self.m_lastRefreshUICurrTabIndex
    self.m_lastRefreshUICurrTabIndex = self.m_currSelectInfo["selectTabIndex"]
    local _, itemData = Tables.itemTable:TryGetValue(self.m_boxItemId)

    local itemName = itemData.name
    self.view.titleText.text = string.format(Language.LUA_GEMCUSTOMIZATIONBOX_TITLE, itemName)

    self.view.weaponImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
    self.view.numTxt.text = tostring(self.m_currSelectInfo["haveBoxNumber"])

    self.view.titleLayout.titleTxt:SetAndResolveTextStyle(Language["LUA_GEMCUSTOMIZATIONBOX_TAB_DESC" .. self.m_tabNum])
    if self.m_tabNum > 0 then
        self.view.titleLayout.selectText:SetAndResolveTextStyle(Language.LUA_GEMCUSTOMIZATIONBOX_TAB_DESC_HAVE_SELECT1)
        local cnt = 0
        for i = 1, self.m_tabNum do
            if self.m_currSelectInfo["selectTermIds"][i] ~= nil then
                cnt = cnt + 1
            end
        end

        if cnt < self.m_tabNum then
            self.view.titleLayout.titleNumTxt:SetAndResolveTextStyle(string.format(
                Language.LUA_GEMCUSTOMIZATIONBOX_TAB_DESC_HAVE_SELECT2_NOTFILL, cnt, self.m_tabNum))
        else
            self.view.titleLayout.titleNumTxt:SetAndResolveTextStyle(string.format(
                Language.LUA_GEMCUSTOMIZATIONBOX_TAB_DESC_HAVE_SELECT2, cnt, self.m_tabNum))
        end
    else
        self.view.titleLayout.selectText.gameObject:SetActive(false)
        self.view.titleLayout.titleNumTxt.gameObject:SetActive(false)
    end

    self:_UpdateSelectTab()

    self:_CreateTermGroups(haveChangeTab)

    self:_UpdateConfirmBtnUI()

    if self.m_tabNum == 0 then
        self.view.entriesLayout.gameObject:SetActive(false)
        self.view.bgNode.gameObject:SetActive(false)
    end

    if self.m_currSelectInfo["haveSetNaviTarget"] == false and self.m_tabNum > 0 then
        self.m_currSelectInfo["haveSetNaviTarget"] = true
        local naviGroup = self.view.scrollListFilter.transform:GetComponent("UISelectableNaviGroup")

        naviGroup:NaviToThisGroup()
    end
end

GemCustomizationBoxCtrl._RefreshUIPreviewMode = HL.Method() << function(self)
    local viewListNode = self.view.viewListNode;
    if self.m_currSelectInfo["haveRefreshPreviewMode"] == false then
        self.m_currSelectInfo["haveRefreshPreviewMode"] = true
        viewListNode.scrollListFilter:UpdateCount(self.m_termGroupNum)
    end
end

GemCustomizationBoxCtrl._InitItemBoxData = HL.Method() << function(self)

end

GemCustomizationBoxCtrl._InitTermGroups = HL.Method() << function(self)
    self.m_genTermGroupCellFunc = UIUtils.genCachedCellFunction(self.view.scrollListFilter)
    self.view.scrollListFilter.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnTermGroupScrollUpdateCell(obj, csIndex)
    end)
end

GemCustomizationBoxCtrl._OnTermGroupScrollUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, obj, csIndex)
    local luaIndex = LuaIndex(csIndex)

    local emptyCnt = 0
    for i = 1, luaIndex do
        if self.m_termGroupList[i] == nil then
            emptyCnt = emptyCnt + 1
        end
    end
    luaIndex = luaIndex + emptyCnt

    if self.m_tabNum == 0 then
        local termGroupCell = self.m_genTermGroupCellFunc(obj)
        self:_UpdateTermGroupCell(termGroupCell, luaIndex)
        return
    end

    local trueIndex = 0
    local cnt = 0
    for i = 1, MAX_TERM_GROUP_INDEX do
        if self:_CheckTermGroupIndexCanSelect(i) then
            cnt = cnt + 1
            if cnt == luaIndex then
                trueIndex = i
            end
        end
    end
    local termGroupCell = self.m_genTermGroupCellFunc(obj)
    self:_UpdateTermGroupCell(termGroupCell, trueIndex)
end

GemCustomizationBoxCtrl._InitUITagGroups = HL.Method() << function(self)
    local scrollList = self.view.viewListNode.scrollListFilter
    self.m_genTagGroupCellFunc = UIUtils.genCachedCellFunction(scrollList)
    scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        local luaIndex = LuaIndex(csIndex)

        local emptyCnt = 0
        for i = 1, luaIndex do
            if self.m_termGroupList[i] == nil then
                emptyCnt = emptyCnt + 1
            end
        end
        luaIndex = luaIndex + emptyCnt

        local tagGroupCell = self.m_genTermGroupCellFunc(obj)
        self:_UpdateTagGroupCell(tagGroupCell, luaIndex)
    end)
end

GemCustomizationBoxCtrl._UpdateTermGroupCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    cell:InitGemCustomizationBoxTermGroupCell(false)
    local termIdList = self.m_termGroupList[luaIndex]
    cell:UpdateTermGroupUI(luaIndex, termIdList, self.m_currSelectInfo)
end

GemCustomizationBoxCtrl._UpdateTagGroupCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    cell:InitGemCustomizationBoxTermGroupCell(true)
    local termIdList = self.m_termGroupList[luaIndex]
    cell:UpdateTagGroupUI(luaIndex, termIdList)
end

GemCustomizationBoxCtrl._CreateTermGroups = HL.Method(HL.Boolean) << function(self, haveChangeTab)

    if self.m_tabNum == 0 then
        self.view.scrollListFilter:UpdateCount(self.m_termGroupNum)
        return
    end

    local cnt = 0
    for i = 1, MAX_TERM_GROUP_INDEX do
        if self:_CheckTermGroupIndexCanSelect(i) then
            cnt = cnt + 1
        end



    end

    if haveChangeTab then
        self.view.scrollListFilter:UpdateCount(cnt)
    else
        self.view.scrollListFilter:UpdateShowingCells(function(csIndex, obj)
            self:_OnTermGroupScrollUpdateCell(obj, csIndex)
        end)
    end
end

GemCustomizationBoxCtrl._UpdateSelectTab = HL.Method() << function(self)

    self.m_selectTabCells:Refresh(MAX_SHOW_TAB_NUM, function(cell, index)
        self:_UpdateSelectTabCell(cell, index)
    end)

    local showTabKeyHint = self.m_tabNum >= 2
    self.view.leftTabKeyHint.gameObject:SetActive(showTabKeyHint)
    self.view.rightTabKeyHint.gameObject:SetActive(showTabKeyHint)

    local canScrollByController = self.m_tabNum == 0
    self.view.scrollListFilterRect.controllerScrollEnabled = canScrollByController
end

GemCustomizationBoxCtrl._UpdateSelectTabCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    cell:InitGemCustomizationBoxSelectTabCell()
    if luaIndex > self.m_tabNum then
        cell:SetBtnClickCallback(nil)
        local tabData = {}
        tabData["cannotSelect"] = true
        cell:SetupView(tabData)
    else
        cell:SetBtnClickCallback(function(tabData)
            local tabIndex = tabData["tabIndex"]
            self:_OnTabChange(tabIndex)
        end)
        local tabData = {}
        tabData["cannotSelect"] = false
        tabData["tabIndex"] = luaIndex
        tabData["selectInfo"] = self.m_currSelectInfo
        cell:SetupView(tabData)
    end
end

GemCustomizationBoxCtrl._UpdateConfirmBtnUI = HL.Method() << function(self)
    local allSelect = true
    for i = 1, self.m_tabNum do
        if self.m_currSelectInfo["selectTermIds"][i] == nil then
            allSelect = false
        end
    end
    self.view.confirmBtnNo.gameObject:SetActive(not allSelect)
    self.view.confirmBtnYes.gameObject:SetActive(allSelect)
    self.view.confirmBtnNo.button.text = Language.LUA_GEMCUSTOMIZATIONBOX_TAB_GROUP_BTN_INACTIVE
    self.view.confirmBtnYes.button.text = Language.LUA_GEMCUSTOMIZATIONBOX_TAB_GROUP_BTN_ACTIVE
    self.view.confirmBtnNo.root:SetState("DisableState")
    self.view.confirmBtnYes.root:SetState("NormalState")
end

GemCustomizationBoxCtrl._OnTabChange = HL.Method(HL.Number, HL.Opt(HL.Boolean))
    << function(self, tabIndex, callByShortcut)
    local prevTabIndex = self.m_currSelectInfo["selectTabIndex"]
    if prevTabIndex == tabIndex then
        return
    end

    local originTabIndex = self.m_currSelectInfo["selectTabIndex"]
    local curr = CS.Beyond.Input.InputManager.instance.controllerNaviManager.curTarget
    if NotNull(curr) then

        local currWidget = curr.transform.parent:GetComponent("LuaUIWidget")
        if currWidget ~= nil and currWidget.table ~= nil then
            local termId = currWidget.table[1]:GetTermId()
            self.m_currSelectInfo["naviSelectTermIds"][originTabIndex] = termId
        end
    end

    self.m_currSelectInfo["selectTabIndex"] = tabIndex

    if callByShortcut then
        AudioAdapter.PostEvent("Au_UI_Toggle_Tag_On")
    end
    self:_RefreshUI()

    if tabIndex > prevTabIndex then
        self.view.arrowNode:Play(string.format(tabAniStrLeftFormat, prevTabIndex, tabIndex))
    else
        self.view.arrowNode:Play(string.format(tabAniStrRightFormat, prevTabIndex, tabIndex))
    end
    local tabCell = self.m_selectTabCells:Get(tabIndex)
    tabCell.view.animationWrapper:Play("gemcustombox_slccell_in")

    if self.m_currSelectInfo["naviSelectTermIds"][tabIndex] ~= nil then
        local targetTermId = self.m_currSelectInfo["naviSelectTermIds"][tabIndex]
        for i = 0, MAX_TERM_GROUP_INDEX - 1 do
            local termGroupGo = self.view.scrollListFilter:Get(i)
            if termGroupGo ~= nil then
                local childCount = termGroupGo.transform.childCount
                for j = 0, childCount-1 do
                    local term = termGroupGo.transform:GetChild(j)
                    if term.transform.name == "GemCustomizationBoxTermCell(Clone)" then
                        local termLua = term.transform:GetComponent("LuaUIWidget").table[1]
                        local termId = termLua:GetTermId()
                        if termId == targetTermId then
                            local btn = termLua.view.tagBtn
                            self:SetNaviTarget(btn)
                        end
                    end
                end
            end
        end
    else
        local firstTermGroupGo = self.view.scrollListFilter:Get(0)
        local firstTerm = firstTermGroupGo.transform:Find("GemCustomizationBoxTermCell(Clone)")
        local firstBtn = firstTerm:Find("TagBtn")
        self:SetNaviTarget(firstBtn.transform:GetComponent("UIButton"))
    end
end



GemCustomizationBoxCtrl._OnTermClick = HL.Method(HL.String) << function(self, termId)
    local currSelectInfo = self.m_currSelectInfo

    local currTabCanSelectTermTypeIndexes = currSelectInfo["eachTabCanSelectTermTypeIndex"][currSelectInfo["selectTabIndex"]]
    local canSelectGroup = false
    for _, termTypeIndex in ipairs(currTabCanSelectTermTypeIndexes) do

        for _, id in ipairs(self.m_termGroupList[termTypeIndex]) do
            if id == termId then
                canSelectGroup = true
            end
        end
    end
    if canSelectGroup == false then
        return
    end

    local currTabSelectTermId = currSelectInfo["selectTermIds"]
        and currSelectInfo["selectTermIds"][currSelectInfo["selectTabIndex"]]
        or nil
    if currTabSelectTermId ~= nil and currTabSelectTermId == termId then
        currSelectInfo["selectTermIds"][currSelectInfo["selectTabIndex"]] = nil
        self:_RefreshUI()

        local termWidget = self:_GetTermWidgetByTermId(currTabSelectTermId)
        if termWidget ~= nil then
            termWidget.view.animationWrapper:Play("gemcustombox_termcell_out")
        end
        return
    end

    for i = 1, self.m_tabNum do
        local thisTabSelectTermId = currSelectInfo["selectTermIds"] and
            currSelectInfo["selectTermIds"][i] or nil
        if i ~= currSelectInfo["selectTabIndex"] and thisTabSelectTermId == termId then

            Notify(MessageConst.SHOW_TOAST, Language["ui_GemCustomizationBoxPanel_term_not_same"])
            return
        end
    end

    currSelectInfo["selectTermIds"][currSelectInfo["selectTabIndex"]] = termId

    local rightTabIndex = nil
    for i = currSelectInfo["selectTabIndex"] + 1, currSelectInfo["selectTabIndex"] + self.m_tabNum - 1 do
        local trueI = i > self.m_tabNum and i - self.m_tabNum or i
        if currSelectInfo["selectTermIds"][trueI] == nil then
            rightTabIndex = trueI
            break
        end
    end

    if rightTabIndex ~= nil then

        self:_OnTabChange(rightTabIndex)
    else
        self:_RefreshUI()

        local termWidget = self:_GetTermWidgetByTermId(termId)
        if termWidget ~= nil then
            termWidget.view.animationWrapper:Play("gemcustombox_termcell_in")
        end
    end
end

GemCustomizationBoxCtrl._OnConfirmBtnClick = HL.Method() << function(self)
    local selectTermIdList = {}
    for i = 1, self.m_tabNum do
        local termId = self.m_currSelectInfo["selectTermIds"][i]
        table.insert(selectTermIdList, termId)
    end

    self.m_currSelectInfo["useBoxNumber"] = self.view.numberSelector.curNumber
    GameInstance.player.inventory:OpenGemCustomizationBox(
        self.m_boxItemId, self.view.numberSelector.curNumber, selectTermIdList)
end

GemCustomizationBoxCtrl._OnReceiveServer = HL.Method(HL.Table) << function(self, args)
    local serverOpenBoxNum = 0


    self:Close()












    local rewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(CS.Beyond.GEnums.RewardSourceType.ItemCase)
    local items = {}
    if rewardPack and rewardPack.rewardSourceType == CS.Beyond.GEnums.RewardSourceType.ItemCase then
        for _, itemBundle in pairs(rewardPack.itemBundleList) do
            local _, itemData = Tables.itemTable:TryGetValue(itemBundle.id)
            if itemData then
                serverOpenBoxNum = serverOpenBoxNum + 1
                local showGemPerfectIcon = false
                if itemData.type == GEnums.ItemType.WeaponGem and itemBundle.instId and itemBundle.instId > 0 then
                    showGemPerfectIcon = UIUtils.getGemWishListPerfectMatch(itemBundle.instId)
                end
                local putInside = false
                for i = 1, #items do
                    if items[i].id == itemData.id and itemBundle.instId == 0 then
                        items[i].count = items[i].count + itemBundle.count
                        putInside = true
                        break
                    end
                end

                if not putInside then
                    table.insert(items, {id = itemBundle.id,
                                         count = itemBundle.count,
                                         instData = itemBundle.instData,
                                         instId = itemBundle.instId,
                                         showGemPerfectIcon = showGemPerfectIcon,
                                         rarity = itemData.rarity,
                                         type = itemData.type:ToInt()})
                end
            end
        end
        table.sort(items, Utils.genSortFunction({"rarity", "type", "id"}, false))
    end
    local rewardPanelArgs = {}
    rewardPanelArgs.items = items
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, rewardPanelArgs)

    if serverOpenBoxNum < self.m_currSelectInfo["useBoxNumber"] then
        Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_GEMCUSTOMIZATIONBOX_OPEN_HINT, serverOpenBoxNum))
    end
end

GemCustomizationBoxCtrl._CheckTermGroupIndexCanSelect = HL.Method(HL.Number).Return(HL.Boolean) << function(self, index)
    local currTabCanSelectTermTypeIndexes = self.m_currSelectInfo["eachTabCanSelectTermTypeIndex"][self.m_currSelectInfo["selectTabIndex"]]
    for _, v in ipairs(currTabCanSelectTermTypeIndexes) do
        if v == index then
            return true
        end
    end
    return false
end

GemCustomizationBoxCtrl._TabMoveRight = HL.Method() << function(self)
    if (self.m_tabNum <= 1) then
        return
    end

    local target = self.m_currSelectInfo["selectTabIndex"] + 1
    if target > self.m_tabNum then
        target = 1
    end

    self:_OnTabChange(target, true)


end

GemCustomizationBoxCtrl._TabMoveLeft = HL.Method() << function(self)
    if (self.m_tabNum <= 1) then
        return
    end

    local target = self.m_currSelectInfo["selectTabIndex"] - 1
    if target == 0 then
        target = self.m_tabNum
    end

    self:_OnTabChange(target, true)


end

GemCustomizationBoxCtrl._GetTermWidgetByTermId = HL.Method(HL.String).Return(HL.Any) << function(self, targetTermId)
    for i = 0, MAX_TERM_GROUP_INDEX - 1 do
        local termGroupGo = self.view.scrollListFilter:Get(i)
        if termGroupGo ~= nil then
            local childCount = termGroupGo.transform.childCount
            for j = 0, childCount-1 do
                local term = termGroupGo.transform:GetChild(j)
                if term.transform.name == "GemCustomizationBoxTermCell(Clone)" then
                    local termLua = term.transform:GetComponent("LuaUIWidget").table[1]
                    local termId = termLua:GetTermId()
                    if termId == targetTermId then
                        return termLua
                    end
                end
            end
        end
    end
    return nil
end

GemCustomizationBoxCtrl._TermType2LuaIndex = HL.StaticMethod(HL.Any).Return(HL.Number) << function(termType)
    if termType == CS.Beyond.GEnums.GemTermType.PrimAttrTerm then
        return 1
    elseif termType == CS.Beyond.GEnums.GemTermType.SecAttrTerm then
        return 2
    elseif termType == CS.Beyond.GEnums.GemTermType.SkillTerm then
        return 3
    end
end


GemCustomizationBoxCtrl.CheckTermIdIsSelected = HL.StaticMethod(HL.String, HL.Table).Return(HL.Number)
    << function(termId, selectInfo)
    for i = 1, 3 do
        local selectTermId = selectInfo["selectTermIds"][i]
        if selectTermId == termId then
            return i
        end
    end
    return 0
end

HL.Commit(GemCustomizationBoxCtrl)
