local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.HyperlinkPopup

HyperlinkPopupCtrl = HL.Class('HyperlinkPopupCtrl', uiCtrl.UICtrl)






HyperlinkPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
}



HyperlinkPopupCtrl.IsRestore = HL.StaticField(HL.Boolean) << false

HyperlinkPopupCtrl.m_args = HL.Field(HL.Table)

HyperlinkPopupCtrl.m_genTermCells = HL.Field(HL.Forward("UIListCache"))

HyperlinkPopupCtrl.m_genOriginalTxtCells = HL.Field(HL.Forward("UIListCache"))

HyperlinkPopupCtrl.m_maxTermCellCount = HL.Field(HL.Number) << 0


HyperlinkPopupCtrl.m_termDataStack = HL.Field(HL.Forward("Stack"))

HyperlinkPopupCtrl.m_originalTextDataList = HL.Field(HL.Table)

HyperlinkPopupCtrl.m_targetUITextIndex = HL.Field(HL.Number) << 0

HyperlinkPopupCtrl.m_baseLinkId = HL.Field(HL.String) << ""

HyperlinkPopupCtrl.m_refreshTermCellFunc = HL.Field(HL.Function)

HyperlinkPopupCtrl.m_clickTargetTextLinkFunc = HL.Field(HL.Function)

HyperlinkPopupCtrl.m_onOriginalTxtHoverLinkChangeFunc = HL.Field(HL.Function)

HyperlinkPopupCtrl.m_clickTermTextLinkFunc = HL.Field(HL.Function)



local LayerType = {
    None = 0,
    OriginalText = 1,
    TermList = 2,
}

HyperlinkPopupCtrl.m_curFocusLayer = HL.Field(HL.Number) << LayerType.None

HyperlinkPopupCtrl.m_hyperlinkConfirmBindId = HL.Field(HL.Number) << 0

HyperlinkPopupCtrl.m_isNavi = HL.Field(HL.Boolean) << false

HyperlinkPopupCtrl.m_updateNaviKey = HL.Field(HL.Number) << 0

HyperlinkPopupCtrl.m_isTemporaryHideNavi = HL.Field(HL.Boolean) << false







HyperlinkPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self:_InitUI()
    self.m_updateNaviKey = self:_StartUpdate(function()
        if self.m_isNavi then
            self:_AutoRefreshNavi()
        end
    end)
end

HyperlinkPopupCtrl.OnShow = HL.Override() << function(self)
    if self.m_isTemporaryHideNavi then
        self.m_isTemporaryHideNavi = false
        self.m_isNavi = true
        self:_AutoRefreshNavi()
    end
end

HyperlinkPopupCtrl.OnHide = HL.Override() << function(self)
    if not self.m_isTemporaryHideNavi then
        self.m_isTemporaryHideNavi = true
        self.m_isNavi = false
        self:_ChangeNavi(nil)
    end
end

HyperlinkPopupCtrl.OnClose = HL.Override() << function(self)
    HyperlinkPopupCtrl.IsRestore = false
    self.m_originalTextDataList = nil
    self.view.originalTxt.onClickLink:RemoveListener(self.m_clickTargetTextLinkFunc)
    self.m_updateNaviKey = self:_RemoveUpdate(self.m_updateNaviKey)
    self:_ChangeNavi(nil)

    local dataCount = self.m_termDataStack:Count()
    for i = 1, dataCount do
        local data = self.m_termDataStack:Get(i)
        if data.tween then
            data.tween:Kill()
        end
    end

    Notify(MessageConst.ON_STOP_HOVER_LINK)
    AudioAdapter.PostEvent("Au_UI_Popup_DetailsPanel_Close")
end

HyperlinkPopupCtrl.OnAnimationInFinished = HL.Override() << function(self)
    if HyperlinkPopupCtrl.IsRestore then
        HyperlinkPopupCtrl.IsRestore = false
        return
    end
    local originalTextDataCount = #self.m_originalTextDataList
    for i = 1, originalTextDataCount do
        local cell = self.m_genOriginalTxtCells:Get(i)
        if cell then
            self.m_originalTextDataList[i].hyperTextData = HyperlinkPopupCtrl._WrapHyperTextData(cell.originalTxt)
        end
    end
    if DeviceInfo.usingController and #self.m_originalTextDataList > 0 then
        self.m_baseLinkId = self.m_originalTextDataList[1].hyperTextData.linkDataList[1].linkId
    end
    if not string.isEmpty(self.m_baseLinkId) then
        self:_ForceRefreshBaseLink(self.m_baseLinkId)
        self:_AutoRefreshNavi()
    end
end

HyperlinkPopupCtrl.ShowPopup = HL.Method(HL.Any) << function(self, args)
    UIManager:SetTopOrder(PANEL_ID)
    self:_InitData(args)
    self:_RefreshAllUI()
end





HyperlinkPopupCtrl.ShowHyperlinkPopupSingle = HL.StaticMethod(HL.Any) << function(args)
    local targetUIText, baseLinkId = unpack(args)
    local linkType = UIUtils.resolveLinkTypeFromId(baseLinkId)
    if linkType ~= UIConst.UI_TEXT_LINK_TYPE.Hyperlink then
        return
    end

    if UIManager:IsShow(PANEL_ID) then
        return
    end
    Notify(MessageConst.ON_STOP_HOVER_LINK)
    local isOpened = UIManager:IsOpen(PANEL_ID)
    local self = UIManager:AutoOpen(PANEL_ID)
    if isOpened then
        self:_ClearStack()
    end
    self:ShowPopup({
        targetUIText = targetUIText,
        baseLinkId = baseLinkId,
    })
end

HyperlinkPopupCtrl.ShowHyperlinkPopupByGroupId = HL.StaticMethod(HL.String) << function(arg)
    if UIManager:IsShow(PANEL_ID) then
        return
    end
    Notify(MessageConst.ON_STOP_HOVER_LINK)
    local isOpened = UIManager:IsOpen(PANEL_ID)
    local self = UIManager:AutoOpen(PANEL_ID)
    if isOpened then
        self:_ClearStack()
    end
    self:ShowPopup({
        hyperlinkUITextGroupId = arg,
    })
end





HyperlinkPopupCtrl._InitData = HL.Method(HL.Table) << function(self, args)
    self.m_args = args
    self.m_originalTextDataList = {}
    self.m_curFocusLayer = LayerType.OriginalText
    self.m_targetUITextIndex = 1

    local tplCellAniWrapper = self.view.termCell.animationWrapper
    self.m_cellAniLengthUpIn = tplCellAniWrapper:GetClipLength("hyperlinkpopup_termcell_up_in")
    self.m_cellAniLengthUpOut = tplCellAniWrapper:GetClipLength("hyperlinkpopup_termcell_up_out")
    self.m_cellAniLengthDownIn = tplCellAniWrapper:GetClipLength("hyperlinkpopup_termcell_down_in")
    self.m_cellAniLengthDownOut = tplCellAniWrapper:GetClipLength("hyperlinkpopup_termcell_down_out")
    self.m_cellVertLayoutGroupPaddingBottom = self.view.termCell.cellVertLayoutGroup.padding.bottom

    if args.targetUIText then
        local originalTextData = {
            uiText = args.targetUIText
        }
        table.insert(self.m_originalTextDataList, originalTextData)
        self.m_baseLinkId = args.baseLinkId
    elseif args.hyperlinkUITextGroupId then

        local group = CS.Beyond.UI.UIText.GetGroupDisplayableHyperlinkUIText(args.hyperlinkUITextGroupId)
        if group ~= nil then
            for _, uiText in cs_pairs(group) do
                local textData = {
                    uiText = uiText
                }
                table.insert(self.m_originalTextDataList, textData)
            end
        end
    end
end

HyperlinkPopupCtrl._WrapHyperTextData = HL.StaticMethod(CS.Beyond.UI.UIText).Return(HL.Table) << function(uiText)
    local textData = {
        uiText = uiText,

        linkDataList = {},
        curFocusIndex = 1,

        lineLinkDataListMap = {},
        lineOrderList = {},
    }
    local textInfo = uiText.textInfo
    local linkLength = textInfo.linkCount

    if linkLength > 0 then
        for luaIndex = 1, linkLength do
            local csIndex = CSIndex(luaIndex)
            local hyperlinkInfo = textInfo.linkInfo[csIndex]
            local _, linkId = uiText:TryGetLinkId(csIndex)

            local startCharIndex = hyperlinkInfo.linkTextfirstCharacterIndex
            local startCharInfo = textInfo.characterInfo[startCharIndex]
            local startLineNumber = startCharInfo.lineNumber
            local endCharIndex = startCharIndex + hyperlinkInfo.linkTextLength - 1
            local endCharInfo = textInfo.characterInfo[endCharIndex]
            local endLineNumber = endCharInfo.lineNumber

            local linkData = {
                index = luaIndex,
                linkId = linkId,
                lineOrder = 1,

                startCharIndex = startCharIndex,
                startLineNumber = startLineNumber,
                startXPos = startCharInfo.topLeft.x,

                endCharIndex = endCharIndex,
                endLineNumber = endLineNumber,
                endXPos = endCharInfo.topRight.x,
            }
            table.insert(textData.linkDataList, linkData)

            local lineLinkDataList = textData.lineLinkDataListMap[startLineNumber]
            if lineLinkDataList == nil then
                lineLinkDataList = {}
                textData.lineLinkDataListMap[startLineNumber] = lineLinkDataList
                table.insert(textData.lineOrderList, startLineNumber)
            end
            linkData.lineOrder = #textData.lineOrderList
            table.insert(lineLinkDataList, linkData)

            if startLineNumber ~= endLineNumber then
                lineLinkDataList = textData.lineLinkDataListMap[endLineNumber]
                if lineLinkDataList == nil then
                    lineLinkDataList = {}
                    textData.lineLinkDataListMap[endLineNumber] = lineLinkDataList
                    table.insert(textData.lineOrderList, endLineNumber)
                end
                table.insert(lineLinkDataList, linkData)
            end
        end
    end
    return textData
end

HyperlinkPopupCtrl._WrapTermData = HL.StaticMethod(HL.String, HL.Number).Return(HL.Table) << function(linkId, stackIndex)
    local cfg = Utils.tryGetTableCfg(Tables.hyperlinkTextTable, string.gsub(linkId, CS.Beyond.UI.UIText.HYPERLINK_ID_PREFIX, ""))
    if cfg then
        local data = {
            id = linkId,
            index = stackIndex,
            cfg = cfg,
        }
        return data
    else
        return nil
    end
end





HyperlinkPopupCtrl._InitUI = HL.Method() << function(self)
    if DeviceInfo.usingController then

        self.view.closeBtn.gameObject:SetActive(false)
    else
        self.view.closeBtn.gameObject:SetActive(true)
        self.view.closeBtn.onClick:AddListener(function()
            self:_ChangeNavi(nil)
            self:PlayAnimationOutAndClose()
            AudioManager.PostEvent("Au_Ul_Popup_DetailsPanel_Close")
        end)
    end

    self.m_genTermCells = UIUtils.genCellCache(self.view.termCell)
    self.m_genOriginalTxtCells = UIUtils.genCellCache(self.view.originalTxtCell)
    self.m_termDataStack = require_ex("Common/Utils/DataStructure/Stack")()

    self.m_refreshTermCellFunc = function(cell, luaIndex)
        cell.gameObject.name = "TermCell" .. luaIndex
        local count = self.m_termDataStack:Count()
        local maxCount = self.m_genTermCells:GetCount()
        if count > maxCount then
            self:_OnRefreshTermCell(cell, count - maxCount + luaIndex)
        else
            self:_OnRefreshTermCell(cell, luaIndex)
        end
    end

    self.m_clickTargetTextLinkFunc = function(linkId)
        self:_ForceRefreshBaseLink(linkId)
    end
    self.m_onOriginalTxtHoverLinkChangeFunc = function(linkId, isShow)
        if not isShow then
            Notify(MessageConst.ON_STOP_HOVER_LINK)
        else
            Notify(MessageConst.ON_START_HOVER_LINK, { linkId })
        end
    end

    self.m_clickTermTextLinkFunc = function(linkId)
        if self.m_remainTermAniCount <= 0 then
            self:_PushTerm(linkId)
        end
    end

    self:BindInputPlayerAction("common_navigation_4_dir_up_no_hint", function()
        self:_OnMoveNaviUp()

        local cell = self.m_genOriginalTxtCells:Get(self.m_targetUITextIndex)
        if cell then
            self.view.originalTxtList:AutoScrollToRectTransform(cell.rectTransform)
        end
    end)
    self:BindInputPlayerAction("common_navigation_4_dir_down", function()
        self:_OnMoveNaviDown()

        local cell = self.m_genOriginalTxtCells:Get(self.m_targetUITextIndex)
        if cell then
            self.view.originalTxtList:AutoScrollToRectTransform(cell.rectTransform)
        end
    end)
    self:BindInputPlayerAction("common_navigation_4_dir_left", function()
        self:_OnMoveNaviLeft()

        local cell = self.m_genOriginalTxtCells:Get(self.m_targetUITextIndex)
        if cell then
            self.view.originalTxtList:AutoScrollToRectTransform(cell.rectTransform)
        end
    end)
    self:BindInputPlayerAction("common_navigation_4_dir_right", function()
        self:_OnMoveNaviRight()

        local cell = self.m_genOriginalTxtCells:Get(self.m_targetUITextIndex)
        if cell then
            self.view.originalTxtList:AutoScrollToRectTransform(cell.rectTransform)
        end
    end)

    self.m_hyperlinkConfirmBindId = self:BindInputPlayerAction("hyperlink_confirm", function()
        if self.m_isNavi == false then
            return
        end

        if self.m_curFocusLayer == LayerType.TermList then
            local termData = self.m_termDataStack:Peek()
            local textData = termData.contentHyperTextData
            if #textData.linkDataList > 0 then
                local linkData = textData.linkDataList[textData.curFocusIndex]
                self:_PushTerm(linkData.linkId)
            end
        else
            self.m_curFocusLayer = LayerType.TermList

        end
    end)
    self:BindInputPlayerAction("hyperlink_cancel", function()
        if self.m_remainTermAniCount > 0 then
            return
        end
        if self.m_curFocusLayer == LayerType.OriginalText then
            self:_ChangeNavi(nil)
            self:PlayAnimationOutAndClose()
        else
            if self.m_termDataStack:Count() > 1 then

                self:_PopTerm(1)
            else

                self.m_curFocusLayer = LayerType.OriginalText

            end
        end
    end)
    self:BindInputPlayerAction("hyperlink_close", function()
        self:_ChangeNavi(nil)
        self:PlayAnimationOutAndClose()
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
        self.view.inputGroup.groupId,
    })
end

HyperlinkPopupCtrl._RefreshAllUI = HL.Method() << function(self)
    self.m_genOriginalTxtCells:Refresh(#self.m_originalTextDataList, function(cell, luaIndex)
        self:_OnRefreshOriginalTxtCell(cell, luaIndex)
    end)
end

HyperlinkPopupCtrl._ForceRefreshBaseLink = HL.Method(HL.String) << function(self, newBaseLink)
    self.m_baseLinkId = newBaseLink
    self:_ClearStack()
    self:_PushTerm(self.m_baseLinkId)
end

HyperlinkPopupCtrl._OnRefreshOriginalTxtCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local originalTextData = self.m_originalTextDataList[luaIndex]
    cell.originalTxt.text = originalTextData.uiText.text
    cell.originalTxt.onClickLink:RemoveListener(self.m_clickTargetTextLinkFunc)
    cell.originalTxt.onClickLink:AddListener(self.m_clickTargetTextLinkFunc)
    cell.originalTxt.onHoverLinkChange:RemoveListener(self.m_onOriginalTxtHoverLinkChangeFunc)
    cell.originalTxt.onHoverLinkChange:AddListener(self.m_onOriginalTxtHoverLinkChangeFunc)
    cell.originalTxt:ForceMeshUpdate()
    local showLine = luaIndex ~= #self.m_originalTextDataList
    cell.lineImg.gameObject:SetActive(showLine)
end

HyperlinkPopupCtrl._OnRefreshTermCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    if luaIndex < 1 or luaIndex > self.m_termDataStack:Count() then
        cell.gameObject:SetActive(false)
        cell.previousBtn.onClick:RemoveAllListeners()
        cell.jumpWikiBtn.onClick:RemoveAllListeners()
        cell.titleTxt.text = ""
        cell.contentTxt.text = ""
        return
    end
    cell.gameObject:SetActive(true)

    local termData = self.m_termDataStack:Get(luaIndex)

    local cfg = termData.cfg

    cell.titleTxt.text = cfg.name
    cell.contentTxt:SetAndResolveTextStyle(cfg.desc)

    cell.previousBtn.onClick:RemoveAllListeners()
    cell.previousBtn.onClick:AddListener(function()
        if self.m_remainTermAniCount > 0 then
            return
        end
        local popCount = self.m_termDataStack:Count() - termData.index
        if popCount <= 0 then
            return
        end
        self:_PopTerm(1)
    end)

    cell.jumpWikiBtn.onClick:RemoveAllListeners()
    local jumpWikiId = cfg.jumpWikiId
    local canJumpWiki = (not string.isEmpty(jumpWikiId))
        and (not DeviceInfo.usingController or luaIndex == self.m_termDataStack:Count())
        and Utils.isSystemUnlocked(GEnums.UnlockSystemType.Wiki)
        and WikiUtils.isWikiEntryUnlock(jumpWikiId)
        and not UIManager:IsShow(PanelId.GuideMedia)
    if canJumpWiki then
        cell.jumpWikiBtn.onClick:AddListener(function()
            if UIManager:ShouldBlockObtainWaysPhaseJump(PhaseId.Wiki) then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_OBTAIN_WAYS_JUMP_BLOCKED)
                return
            end
            self:_OnJumpToWiki(jumpWikiId)
        end)
    end
    cell.jumpWikiBtn.gameObject:SetActive(canJumpWiki)

    cell.contentTxt.onClickLink:RemoveListener(self.m_clickTermTextLinkFunc)
    cell.contentTxt.onClickLink:AddListener(self.m_clickTermTextLinkFunc)
end




HyperlinkPopupCtrl._PushTerm = HL.Method(HL.String) << function(self, linkId)
    self:_ChangeNavi(nil)

    Notify(MessageConst.ON_STOP_HOVER_LINK)

    local preLastIndex = self.m_termDataStack:Count()
    for i = 1, preLastIndex do
        local data = self.m_termDataStack:Get(i)
        if data.id == linkId then
            local popCount = preLastIndex - data.index
            if popCount > 0 then
                self:_PopTerm(popCount)
            end
            return
        end
    end

    local nowLastIndex = preLastIndex + 1
    local data = HyperlinkPopupCtrl._WrapTermData(linkId, nowLastIndex)
    if data == nil then
        logger.error("[HyperlinkPopupCtrl] linkId data is nil : " .. linkId)
        return
    end

    self.m_termDataStack:Push(data)

    local maxCount = self.view.config.MAX_TERM_CELL_COUNT
    if nowLastIndex > maxCount then

        self.m_genTermCells:Refresh(maxCount + 1, self.m_refreshTermCellFunc)
        local cellCount = self.m_genTermCells:GetCount()

        local cell = self.m_genTermCells:Get(1)
        self:_TermAniUpOut(cell, nowLastIndex - maxCount)

        cell = self.m_genTermCells:Get(cellCount - 1)
        self:_TermAniCollapse(cell, preLastIndex)

        cell = self.m_genTermCells:Get(cellCount)
        self:_TermAniUpIn(cell, nowLastIndex)
    else

        self.m_genTermCells:Refresh(nowLastIndex, self.m_refreshTermCellFunc)

        local cell = self.m_genTermCells:Get(nowLastIndex)
        self:_TermAniUpIn(cell, nowLastIndex)

        if nowLastIndex > 1 then
            cell = self.m_genTermCells:Get(preLastIndex)
            self:_TermAniCollapse(cell, preLastIndex)
        end
    end

    self.m_onAllTermAniDone = function()
        self:_EnableAllTermCellPreviousBtn()

        local termCell = self.m_genTermCells:Get(self.m_genTermCells:GetCount())
        if termCell then
            data.contentHyperTextData = HyperlinkPopupCtrl._WrapHyperTextData(termCell.contentTxt)
            self:_AutoRefreshNavi()
        end
    end
    AudioManager.PostEvent("Au_UI_Popup_WikiTipsPanel_Open")
end

HyperlinkPopupCtrl._PopTerm = HL.Method(HL.Number) << function(self, popCount)
    self:_ChangeNavi(nil)
    local preLastIndex = self.m_termDataStack:Count()
    local nowLastIndex = preLastIndex - 1
    local maxCount = self.view.config.MAX_TERM_CELL_COUNT


    if preLastIndex > maxCount then
        local cellCount = self.m_genTermCells:GetCount()

        local cell = self.m_genTermCells:Get(1)
        self:_TermAniDownIn(cell, preLastIndex - maxCount)

        cell = self.m_genTermCells:Get(cellCount - 1)
        self:_TermAniExpand(cell, nowLastIndex)

        cell = self.m_genTermCells:Get(cellCount)
        self:_TermAniDownOut(cell, preLastIndex)

        self.m_onAllTermAniDone = function()
            self.m_termDataStack:Pop()
            if nowLastIndex > maxCount then
                self.m_genTermCells:Refresh(maxCount + 1, self.m_refreshTermCellFunc)
                local nowCellCount = self.m_genTermCells:GetCount()

                local tempCell = self.m_genTermCells:Get(1)
                tempCell.gameObject:SetActive(false)

                tempCell = self.m_genTermCells:Get(nowCellCount - 1)
                tempCell.gameObject:SetActive(true)
                tempCell.cellStateCtrl:SetState("Collapse")

                tempCell = self.m_genTermCells:Get(nowCellCount)
                tempCell.gameObject:SetActive(true)
                tempCell.cellStateCtrl:SetState("Expand")
            else
                self.m_genTermCells:Refresh(nowLastIndex, self.m_refreshTermCellFunc)
            end
            if popCount <= 1 then
                self:_EnableAllTermCellPreviousBtn()
                self:_NaviTermCellWaitForRenderDone()
            else
                self:_PopTerm(popCount - 1)
            end
        end
    else
        local cellCount = self.m_genTermCells:GetCount()

        local cell = self.m_genTermCells:Get(cellCount - 1)
        self:_TermAniExpand(cell, nowLastIndex)

        cell = self.m_genTermCells:Get(cellCount)
        self:_TermAniDownOut(cell, preLastIndex)

        self.m_onAllTermAniDone = function()
            self.m_termDataStack:Pop()
            self.m_genTermCells:Refresh(nowLastIndex, self.m_refreshTermCellFunc)
            if popCount <= 1 then
                self:_EnableAllTermCellPreviousBtn()
                self:_NaviTermCellWaitForRenderDone()
            else
                self:_PopTerm(popCount - 1)
            end
        end
    end
    AudioManager.PostEvent("Au_UI_Popup_WikiTipsPanel_Open")
end

HyperlinkPopupCtrl._ClearStack = HL.Method() << function(self)
    self.m_termDataStack:Clear()
    self.m_genTermCells:Refresh(0, self.m_refreshTermCellFunc)
end

HyperlinkPopupCtrl._OnMoveNaviRight = HL.Method() << function(self)
    if self.m_isNavi == false then
        return
    end
    if self.m_curFocusLayer == LayerType.OriginalText then
        local curTextIndex = self.m_targetUITextIndex
        local textData = self.m_originalTextDataList[curTextIndex].hyperTextData
        local nextIndex = textData.curFocusIndex + 1
        if nextIndex <= #textData.linkDataList then

            textData.curFocusIndex = nextIndex
            self:_ForceRefreshBaseLink(textData.linkDataList[nextIndex].linkId)

            AudioManager.PostEvent("Au_UI_Hover_ControllerSelect")
        elseif curTextIndex < #self.m_originalTextDataList then

            curTextIndex = curTextIndex + 1
            self.m_targetUITextIndex = curTextIndex
            textData = self.m_originalTextDataList[curTextIndex].hyperTextData
            textData.curFocusIndex = 1
            self:_ForceRefreshBaseLink(textData.linkDataList[1].linkId)

            AudioManager.PostEvent("Au_UI_Hover_ControllerSelect")
        end
    else
        local termData = self.m_termDataStack:Peek()
        local textData = termData.contentHyperTextData
        local nextIndex = textData.curFocusIndex + 1
        if nextIndex <= #textData.linkDataList then
            textData.curFocusIndex = nextIndex

            AudioManager.PostEvent("Au_UI_Hover_ControllerSelect")
        end
    end
end

HyperlinkPopupCtrl._OnMoveNaviLeft = HL.Method() << function(self)
    if self.m_isNavi == false then
        return
    end
    if self.m_curFocusLayer == LayerType.OriginalText then
        local curTextIndex = self.m_targetUITextIndex
        local textData = self.m_originalTextDataList[curTextIndex].hyperTextData
        local nextIndex = textData.curFocusIndex - 1
        if nextIndex >= 1 then

            textData.curFocusIndex = nextIndex
            self:_ForceRefreshBaseLink(textData.linkDataList[nextIndex].linkId)

            AudioManager.PostEvent("Au_UI_Hover_ControllerSelect")
        elseif curTextIndex > 1 then

            curTextIndex = curTextIndex - 1
            self.m_targetUITextIndex = curTextIndex
            textData = self.m_originalTextDataList[curTextIndex].hyperTextData
            nextIndex = #textData.linkDataList
            textData.curFocusIndex = nextIndex
            self:_ForceRefreshBaseLink(textData.linkDataList[nextIndex].linkId)

            AudioManager.PostEvent("Au_UI_Hover_ControllerSelect")
        end
    else
        local termData = self.m_termDataStack:Peek()
        local textData = termData.contentHyperTextData
        local nextIndex = textData.curFocusIndex - 1
        if nextIndex >= 1 then
            textData.curFocusIndex = nextIndex

            AudioManager.PostEvent("Au_UI_Hover_ControllerSelect")
        end
    end
end

HyperlinkPopupCtrl._OnMoveNaviDown = HL.Method() << function(self)
    if self.m_isNavi == false then
        return
    end
    if self.m_curFocusLayer == LayerType.OriginalText then
        local curTextIndex = self.m_targetUITextIndex
        local textData = self.m_originalTextDataList[curTextIndex].hyperTextData
        local nextIndex = HyperlinkPopupCtrl._GetNextLinkIndexDown(textData)
        if nextIndex > 0 then
            textData.curFocusIndex = nextIndex
            self:_ForceRefreshBaseLink(textData.linkDataList[nextIndex].linkId)

            AudioManager.PostEvent("Au_UI_Hover_ControllerSelect")
        elseif curTextIndex < #self.m_originalTextDataList then
            curTextIndex = curTextIndex + 1
            self.m_targetUITextIndex = curTextIndex
            textData = self.m_originalTextDataList[curTextIndex].hyperTextData
            textData.curFocusIndex = 1
            self:_ForceRefreshBaseLink(textData.linkDataList[1].linkId)

            AudioManager.PostEvent("Au_UI_Hover_ControllerSelect")
        else
            self:_OnMoveNaviRight()
        end
    else
        local termData = self.m_termDataStack:Peek()
        local textData = termData.contentHyperTextData
        local nextIndex = HyperlinkPopupCtrl._GetNextLinkIndexDown(textData)
        if nextIndex > 0 then
            textData.curFocusIndex = nextIndex

            AudioManager.PostEvent("Au_UI_Hover_ControllerSelect")
        else
            self:_OnMoveNaviRight()
        end
    end
end

HyperlinkPopupCtrl._OnMoveNaviUp = HL.Method() << function(self)
    if self.m_isNavi == false then
        return
    end
    if self.m_curFocusLayer == LayerType.OriginalText then
        local curTextIndex = self.m_targetUITextIndex
        local textData = self.m_originalTextDataList[curTextIndex].hyperTextData
        local nextIndex = HyperlinkPopupCtrl._GetNextLinkIndexUp(textData)
        if nextIndex > 0 then
            textData.curFocusIndex = nextIndex
            self:_ForceRefreshBaseLink(textData.linkDataList[nextIndex].linkId)

            AudioManager.PostEvent("Au_UI_Hover_ControllerSelect")
        elseif curTextIndex > 1 then
            curTextIndex = curTextIndex - 1
            self.m_targetUITextIndex = curTextIndex
            textData = self.m_originalTextDataList[curTextIndex].hyperTextData
            nextIndex = #textData.linkDataList
            textData.curFocusIndex = nextIndex
            self:_ForceRefreshBaseLink(textData.linkDataList[nextIndex].linkId)

            AudioManager.PostEvent("Au_UI_Hover_ControllerSelect")
        else
            self:_OnMoveNaviLeft()
        end
    else
        local termData = self.m_termDataStack:Peek()
        local textData = termData.contentHyperTextData
        local nextIndex = HyperlinkPopupCtrl._GetNextLinkIndexUp(textData)
        if nextIndex > 0 then
            textData.curFocusIndex = nextIndex

            AudioManager.PostEvent("Au_UI_Hover_ControllerSelect")
        else
            self:_OnMoveNaviLeft()
        end
    end
end

HyperlinkPopupCtrl._GetNextLinkIndexDown = HL.StaticMethod(HL.Table).Return(HL.Number) << function(textData)
    local curFocusLinkData = textData.linkDataList[textData.curFocusIndex]
    if not curFocusLinkData then
        return -1
    end
    local nextLineOrder = curFocusLinkData.lineOrder + 1
    local nextIndex = -1

    if nextLineOrder > #textData.lineOrderList then
        return nextIndex
    end

    for order = nextLineOrder, #textData.lineOrderList do
        local lineNumber = textData.lineOrderList[order]
        local lineLinkDataList = textData.lineLinkDataListMap[lineNumber]
        local cachedPosDelta = 0
        local curPosDelta = 0
        for _, linkData in pairs(lineLinkDataList) do

            if linkData.index ~= curFocusLinkData.index then

                if lineNumber == linkData.startLineNumber then
                    curPosDelta = math.abs(linkData.startXPos - curFocusLinkData.startXPos)
                else
                    curPosDelta = math.abs(linkData.endXPos - curFocusLinkData.startXPos)
                end

                if nextIndex < 1 or curPosDelta < cachedPosDelta then
                    nextIndex = linkData.index
                    cachedPosDelta = curPosDelta
                end
            end
        end

        if nextIndex > 0 then
            break
        end
    end
    return nextIndex
end

HyperlinkPopupCtrl._GetNextLinkIndexUp = HL.StaticMethod(HL.Table).Return(HL.Number) << function(textData)
    local curFocusLinkData = textData.linkDataList[textData.curFocusIndex]
    if not curFocusLinkData then
        return -1
    end
    local nextLineOrder = curFocusLinkData.lineOrder - 1
    local nextIndex = -1

    if nextLineOrder < 1 then
        return nextIndex
    end

    for order = nextLineOrder, 1, -1 do
        local lineNumber = textData.lineOrderList[order]
        local lineLinkDataList = textData.lineLinkDataListMap[lineNumber]
        local cachedPosDelta = 0
        local curPosDelta = 0
        for _, linkData in pairs(lineLinkDataList) do

            if linkData.index ~= curFocusLinkData.index then

                if lineNumber == linkData.startLineNumber then
                    curPosDelta = math.abs(linkData.startXPos - curFocusLinkData.startXPos)
                else
                    curPosDelta = math.abs(linkData.endXPos - curFocusLinkData.startXPos)
                end

                if nextIndex < 1 or curPosDelta < cachedPosDelta then
                    nextIndex = linkData.index
                    cachedPosDelta = curPosDelta
                end
            end
        end

        if nextIndex > 0 then
            break
        end
    end
    return nextIndex
end

HyperlinkPopupCtrl._ChangeNavi = HL.Method(HL.Table) << function(self, textData)
    if self.m_isTemporaryHideNavi then
        Notify(MessageConst.HIDE_CONTROLLER_NAVI_TEXT_HINT)
        return
    end
    if not DeviceInfo.usingController then
        self.m_isNavi = false
        Notify(MessageConst.HIDE_CONTROLLER_NAVI_TEXT_HINT)
        return
    end
    if textData ~= nil then
        local focusIndex = textData.curFocusIndex
        if focusIndex > 0 and focusIndex <= #textData.linkDataList then
            local linkData = textData.linkDataList[textData.curFocusIndex]
            InputManagerInst:ToggleBinding(self.m_hyperlinkConfirmBindId, true)
            local arg = {
                uiText = textData.uiText,
                startCharIndex = linkData.startCharIndex,
                endCharIndex = linkData.endCharIndex,
            }
            self.m_isNavi = true
            Notify(MessageConst.SHOW_CONTROLLER_NAVI_TEXT_HINT, arg)
            return
        end
    end
    self.m_isNavi = false
    InputManagerInst:ToggleBinding(self.m_hyperlinkConfirmBindId, false)
    Notify(MessageConst.HIDE_CONTROLLER_NAVI_TEXT_HINT)
end

HyperlinkPopupCtrl._AutoRefreshNavi = HL.Method() << function(self)
    if self.m_isTemporaryHideNavi then
        self:_ChangeNavi(nil)
        return
    end
    if self.m_curFocusLayer == LayerType.OriginalText then
        local originalTextData = self.m_originalTextDataList[self.m_targetUITextIndex]
        self:_ChangeNavi(originalTextData.hyperTextData)
    else
        local termData = self.m_termDataStack:Peek()
        local textData = termData.contentHyperTextData
        self:_ChangeNavi(textData)
    end

    local termData = self.m_termDataStack:Peek()
    if termData and termData.contentHyperTextData then
        local textData = termData.contentHyperTextData
        InputManagerInst:ToggleBinding(self.m_hyperlinkConfirmBindId, #textData.linkDataList > 0)
    else
        InputManagerInst:ToggleBinding(self.m_hyperlinkConfirmBindId, true)
    end
end

HyperlinkPopupCtrl._OnJumpToWiki = HL.Method(HL.String) << function(self, jumpWikiId)
    if PhaseManager:CheckIsInTransition() then
        return
    end
    Notify(MessageConst.HIDE_ITEM_TIPS)
    if PhaseManager:IsOpen(PhaseId.Wiki) then


        UIManager:Close(PANEL_ID)
        PhaseManager:GoToPhase(PhaseId.Wiki, {
            wikiEntryId = jumpWikiId,
        })
    else
        UIManager:Hide(PANEL_ID)
        PhaseManager:GoToPhase(PhaseId.Wiki, {
            wikiEntryId = jumpWikiId,
            restoreHyperlinkPopupCallback = function()

                if PhaseManager:IsOpen(PhaseId.Wiki) then
                    if UIManager:IsOpen(PANEL_ID) then
                        HyperlinkPopupCtrl.IsRestore = true
                        UIManager:Show(PANEL_ID)
                    end
                else


                    UIManager:Close(PANEL_ID)
                end
            end
        })
    end
end

HyperlinkPopupCtrl._EnableAllTermCellPreviousBtn = HL.Method() << function(self)
    local cellCount = self.m_genTermCells:GetCount()
    for i = 1, cellCount do
        local cell = self.m_genTermCells:Get(i)
        if cell then
            cell.previousBtn.enabled = true
        end
    end
end





HyperlinkPopupCtrl.m_cellAniLengthUpIn = HL.Field(HL.Number) << 0

HyperlinkPopupCtrl.m_cellAniLengthUpOut = HL.Field(HL.Number) << 0

HyperlinkPopupCtrl.m_cellAniLengthDownIn = HL.Field(HL.Number) << 0

HyperlinkPopupCtrl.m_cellAniLengthDownOut = HL.Field(HL.Number) << 0

HyperlinkPopupCtrl.m_cellVertLayoutGroupPaddingBottom = HL.Field(HL.Number) << 0

HyperlinkPopupCtrl.m_remainTermAniCount = HL.Field(HL.Number) << 0

HyperlinkPopupCtrl.m_onAllTermAniDone = HL.Field(HL.Function)

HyperlinkPopupCtrl._TermAniUpOut = HL.Method(HL.Table, HL.Number) << function(self, cell, dataIndex)
    self.m_remainTermAniCount = self.m_remainTermAniCount + 1
    cell.previousBtn.enabled = false
    cell.animationWrapper:Play("hyperlinkpopup_termcell_up_out")
    cell.cellVertLayoutGroup.childControlHeight = false
    local titlePreferredHeight = CS.UnityEngine.UI.LayoutUtility.GetPreferredHeight(cell.titleNode)
    local data = self.m_termDataStack:Get(dataIndex)
    cell.cellVertLayoutGroup.padding.bottom = self.m_cellVertLayoutGroupPaddingBottom
    cell.titleNode:SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, titlePreferredHeight)
    logger.info(string.format("[HyperlinkPopup:_TermAniUpOut] index: %d; cellName: %s;", dataIndex, cell.gameObject.name))
    local tween = DOTween.To(function()
        return titlePreferredHeight
    end, function(value)
        cell.cellVertLayoutGroup.padding.bottom = math.floor(value / titlePreferredHeight * self.m_cellVertLayoutGroupPaddingBottom)
        cell.titleNode:SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value)
    end, 0, self.m_cellAniLengthUpOut)
    data.tween = tween
    tween:OnComplete(function()
        cell.gameObject:SetActive(false)
        self:_OnOneTermAniDone()
    end)
end

HyperlinkPopupCtrl._TermAniUpIn = HL.Method(HL.Table, HL.Number) << function(self, cell, dataIndex)
    self.m_remainTermAniCount = self.m_remainTermAniCount + 1
    cell.previousBtn.enabled = false
    cell.cellVertLayoutGroup.childControlHeight = true
    cell.cellVertLayoutGroup.padding.bottom = self.m_cellVertLayoutGroupPaddingBottom
    cell.animationWrapper:Play("hyperlinkpopup_termcell_up_in", function()
        self:_OnOneTermAniDone()
        local contentPreferredHeight = cell.contentTxtRect.rect.size.y
        local data = self.m_termDataStack:Get(dataIndex)
        if data == nil then
            return
        end
        data.contentPreferredHeight = contentPreferredHeight
        logger.info(string.format("[HyperlinkPopup:_TermAniUpIn] index: %d; cellName: %s; contentPreferredHeight: %.3f", dataIndex, cell.gameObject.name, contentPreferredHeight))
    end)
end

HyperlinkPopupCtrl._TermAniCollapse = HL.Method(HL.Table, HL.Number) << function(self, cell, dataIndex)
    self.m_remainTermAniCount = self.m_remainTermAniCount + 2
    cell.previousBtn.enabled = false
    cell.cellVertLayoutGroup.childControlHeight = false
    local data = self.m_termDataStack:Get(dataIndex)
    local contentPreferredHeight = data.contentPreferredHeight
    cell.contentTxtRect.gameObject:SetActive(true)
    logger.info(string.format("[HyperlinkPopup:_TermAniCollapse] index: %d; cellName: %s; contentPreferredHeight: %.3f", dataIndex, cell.gameObject.name, contentPreferredHeight))
    local tween = DOTween.To(function()
        return contentPreferredHeight
    end, function(value)
        cell.contentTxtRect:SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value)
    end, 0, self.m_cellAniLengthUpOut)
    data.tween = tween
    tween:OnComplete(function()
        cell.contentTxtRect:SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentPreferredHeight)
        cell.splitLine.gameObject:SetActive(false)
        cell.contentTxtRect.gameObject:SetActive(false)
        self:_OnOneTermAniDone()
    end)
    cell.animationWrapper:Play("hyperlinkpopup_termcell_deco_out", function()
        self:_OnOneTermAniDone()
    end)
end

HyperlinkPopupCtrl._TermAniDownIn = HL.Method(HL.Table, HL.Number) << function(self, cell, dataIndex)
    self.m_remainTermAniCount = self.m_remainTermAniCount + 1
    cell.previousBtn.enabled = false
    cell.animationWrapper:Play("hyperlinkpopup_termcell_down_in")
    cell.cellVertLayoutGroup.childControlHeight = false
    cell.gameObject:SetActive(true)
    local data = self.m_termDataStack:Get(dataIndex)
    local titlePreferredHeight = CS.UnityEngine.UI.LayoutUtility.GetPreferredHeight(cell.titleNode)
    logger.info(string.format("[HyperlinkPopup:_TermAniDownIn] index: %d; cellName: %s;", dataIndex, cell.gameObject.name))
    local tween = DOTween.To(function()
        return 0
    end, function(value)
        cell.cellVertLayoutGroup.padding.bottom = math.floor(self.m_cellVertLayoutGroupPaddingBottom * value / titlePreferredHeight)
        cell.titleNode:SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value)
    end, titlePreferredHeight, self.m_cellAniLengthDownIn)
    data.tween = tween
    tween:OnComplete(function()
        cell.cellVertLayoutGroup.padding.bottom = self.m_cellVertLayoutGroupPaddingBottom
        self:_OnOneTermAniDone()
    end)
end

HyperlinkPopupCtrl._TermAniDownOut = HL.Method(HL.Table, HL.Number) << function(self, cell, dataIndex)
    self.m_remainTermAniCount = self.m_remainTermAniCount + 1
    cell.previousBtn.enabled = false
    cell.cellVertLayoutGroup.childControlHeight = true
    cell.gameObject:SetActive(true)
    cell.splitLine.gameObject:SetActive(true)
    cell.contentTxtRect.gameObject:SetActive(true)
    logger.info(string.format("[HyperlinkPopup:_TermAniDownOut] index: %d; cellName: %s;", dataIndex, cell.gameObject.name))
    cell.animationWrapper:Play("hyperlinkpopup_termcell_down_out", function()
        self:_OnOneTermAniDone()
    end)
end

HyperlinkPopupCtrl._TermAniExpand = HL.Method(HL.Table, HL.Number) << function(self, cell, dataIndex)
    self.m_remainTermAniCount = self.m_remainTermAniCount + 2
    cell.previousBtn.enabled = false
    cell.cellVertLayoutGroup.childControlHeight = false
    local data = self.m_termDataStack:Get(dataIndex)
    local contentPreferredHeight = data.contentPreferredHeight
    logger.info(string.format("[HyperlinkPopup:_TermAniExpand] index: %d; cellName: %s; contentPreferredHeight: %.3f", dataIndex, cell.gameObject.name, contentPreferredHeight))
    cell.contentTxtRect.gameObject:SetActive(true)
    cell.splitLine.gameObject:SetActive(true)
    local tween = DOTween.To(function()
        return 0
    end, function(value)
        cell.contentTxtRect:SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value)
    end, contentPreferredHeight, self.m_cellAniLengthDownIn)
    data.tween = tween
    tween:OnComplete(function()
        cell.cellVertLayoutGroup.childControlHeight = true
        self:_OnOneTermAniDone()
    end)
    cell.animationWrapper:Play("hyperlinkpopup_termcell_deco_in", function()
        self:_OnOneTermAniDone()
    end)
end

HyperlinkPopupCtrl._NaviTermCellWaitForRenderDone = HL.Method() << function(self)
    self:_StartCoroutine(function()
        coroutine.waitForRenderDone()
        local termCell = self.m_genTermCells:Get(self.m_genTermCells:GetCount())
        if termCell then
            local topData = self.m_termDataStack:Peek()
            topData.contentHyperTextData.uiText = termCell.contentTxt
            self:_AutoRefreshNavi()
        end
    end)
end

HyperlinkPopupCtrl._OnOneTermAniDone = HL.Method() << function(self)
    self.m_remainTermAniCount = self.m_remainTermAniCount - 1
    if self.m_remainTermAniCount <= 0 then
        if self.m_onAllTermAniDone then
            self.m_onAllTermAniDone()
        end
    end
end



HL.Commit(HyperlinkPopupCtrl)
