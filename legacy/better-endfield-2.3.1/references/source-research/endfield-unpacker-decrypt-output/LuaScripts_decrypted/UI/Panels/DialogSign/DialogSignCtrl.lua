
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DialogSign
local PHASE_ID = PhaseId.DialogSign
DialogSignCtrl = HL.Class('DialogSignCtrl', uiCtrl.UICtrl)






DialogSignCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_HIDE_DIALOG_SIGN_OPTION] = "OnHideDialogSignOption",
    [MessageConst.ON_FINISH_DIALOG_TIMELINE] = 'OnDialogTimelineFinish',

}

DialogSignCtrl.m_textContent = HL.Field(HL.String) << ''

DialogSignCtrl.m_curEntryLinks = HL.Field(HL.Userdata) << nil

DialogSignCtrl.m_enableGlossaryPopUp = HL.Field(HL.Boolean) << false

DialogSignCtrl.m_optionConfig = HL.Field(HL.Userdata)

DialogSignCtrl.m_textShowSpeed = HL.Field(HL.Number) << 0

DialogSignCtrl.m_optionDelayTime = HL.Field(HL.Number) << 0

DialogSignCtrl.m_signDelayTime = HL.Field(HL.Number) << 0.5

DialogSignCtrl.m_optionTimer = HL.Field(HL.Number) << -1

DialogSignCtrl.m_signTimer = HL.Field(HL.Number) << -1

DialogSignCtrl.m_typeWriterCor = HL.Field(HL.Thread)

DialogSignCtrl.m_textPlayCompleted = HL.Field(HL.Boolean) << false

DialogSignCtrl.m_lastMaxVisibleCharacters = HL.Field(HL.Number) << -1

DialogSignCtrl.m_lastVisibleLineNumber = HL.Field(HL.Number) << -1

DialogSignCtrl.m_hasScrolledToBottom = HL.Field(HL.Boolean) << false

DialogSignCtrl.m_dialogAutoMode = HL.Field(HL.Boolean) << false

DialogSignCtrl.OnShowDialogSignOption = HL.StaticMethod(HL.Table) << function(arg)

    PhaseManager:OpenPhase(PHASE_ID, arg, nil, true)
end


DialogSignCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_textContent, self.m_optionConfig, self.m_textShowSpeed, self.m_optionDelayTime, self.m_signDelayTime = unpack(arg)
    self.m_textContent, self.m_curEntryLinks = UIUtils.resolveNarrativeTextWithEntryLinks(self.m_textContent)
    self.m_enableGlossaryPopUp = false
    self:BindInputPlayerAction("dialog_open_note", function() self:_OpenGlossaryPopUp() end)

    self.view.content.uiText.onClickLink:AddListener(function(linkId)
        self:OnLinkClick(linkId)
    end)
end

DialogSignCtrl.OnShow = HL.Override() << function(self)
    if GameWorld.dialogTimelineManager.autoMode then
        self.m_dialogAutoMode = true
        GameWorld.dialogTimelineManager:SetAutoMode(false)
    end
    self:_RefreshGlossaryNoteKeyHint()
    self:StartAutoTypeWriter()
end

DialogSignCtrl._RefreshGlossaryNoteKeyHint = HL.Method() << function(self)
    local enable = true

    if self.m_phase == nil or self.m_curEntryLinks == nil or self.m_curEntryLinks.Count == 0 then
        enable = false
    end

    if not self.m_enableGlossaryPopUp then
        enable = false
    end

    self.view.noteKeyHintNode.gameObject:SetActive(enable)
end

DialogSignCtrl.OnLinkClick = HL.Method(HL.String) << function(self, linkId)
    if UIUtils.resolveLinkTypeFromId(linkId) ~= UIConst.UI_TEXT_LINK_TYPE.Narrative then
        return
    end
    self:_OpenGlossaryPopUp()
end

DialogSignCtrl._OpenGlossaryPopUp = HL.Method() << function(self)
    if self.m_phase == nil or self.m_curEntryLinks == nil or self.m_curEntryLinks.Count == 0 then
        return
    end

    if not self.m_enableGlossaryPopUp then
        return
    end

    self.m_phase:CreatePhasePanelItem(PanelId.DialogGlossaryPopUp, { self.m_curEntryLinks })
end

DialogSignCtrl.StartAutoTypeWriter = HL.Method() << function(self)
    self.view.scrollView.disableScroll = true
    self.view.scrollView.controllerScrollEnabled = false
    self.view.scrollViewImage.raycastTarget = false
    self.view.content.textRevealSpeed = self.m_textShowSpeed
    self.view.scrollKeyHint.gameObject:SetActive(false)


    self.m_lastMaxVisibleCharacters = -1
    self.m_lastVisibleLineNumber = -1
    self.m_hasScrolledToBottom = false

    self.view.content:SetText(self.m_textContent, false)
    self.view.content:Play()

    self.view.content.uiText.raycastTarget = false

    self.m_typeWriterCor = self:_StartCoroutine(function()
        while self.m_isClosed ~= true and self.m_textPlayCompleted ~= true do
            self:TryAutoScrollToBottomLine()


            if self.view.content.playing == false then

                self:OnTextPlayComplete()
                break
            end
            coroutine.step()
        end
    end)
end

DialogSignCtrl.TryAutoScrollToBottomLine = HL.Method() << function(self)

    if self.m_hasScrolledToBottom then
        return
    end

    local uiText = self.view.content.uiText
    local scrollView = self.view.scrollView

    if uiText == nil or scrollView == nil or not scrollView.vertical then
        return
    end


    local currentMaxVisibleChars = uiText.maxVisibleCharacters
    if currentMaxVisibleChars == self.m_lastMaxVisibleCharacters then
        return
    end

    self.m_lastMaxVisibleCharacters = currentMaxVisibleChars


    local textInfo = uiText.textInfo
    if textInfo == nil or textInfo.lineCount == 0 then
        return
    end


    local lastVisibleLine = -1
    for i = 0, textInfo.lineCount - 1 do
        local lineInfo = textInfo.lineInfo[i]
        if lineInfo.firstVisibleCharacterIndex <= currentMaxVisibleChars then
            lastVisibleLine = i
        else

            break
        end
    end


    if lastVisibleLine >= 0 and lastVisibleLine ~= self.m_lastVisibleLineNumber then
        self.m_lastVisibleLineNumber = lastVisibleLine

        if not self:_IsLineVisibleInViewport(textInfo, lastVisibleLine) then
            self:_ScrollHalfViewportOrToBottom()
        end
    end
end

DialogSignCtrl._IsLineVisibleInViewport = HL.Method(HL.Userdata, HL.Number).Return(HL.Boolean) << function(self, textInfo, lineNumber)
    if textInfo == nil or lineNumber < 0 or lineNumber >= textInfo.lineCount then
        return false
    end

    local uiText = self.view.content.uiText
    local scrollView = self.view.scrollView

    if uiText == nil or scrollView == nil or not scrollView.vertical then
        return true
    end


    local lineInfo = textInfo.lineInfo[lineNumber]


    local textRect = uiText.rectTransform
    local contentRect = scrollView.content
    local viewportRect = scrollView.viewport




    local lineBottomY = lineInfo.descender
    local lineTopY = lineInfo.ascender


    local textPosInContent = textRect.anchoredPosition


    local lineBottomInContentY = textPosInContent.y + lineBottomY
    local lineTopInContentY = textPosInContent.y + lineTopY


    local viewportHeight = viewportRect.rect.height
    local contentHeight = contentRect.rect.height


    local currentContentPosY = contentRect.anchoredPosition.y
    local contentPivot = contentRect.pivot





    local contentTopLocalY = contentHeight * (1 - contentPivot.y)




    local viewportTopInContentY = contentTopLocalY - currentContentPosY

    local viewportBottomInContentY = viewportTopInContentY - viewportHeight




    local lineHeight = lineTopY - lineBottomY
    local safetyMargin = lineHeight * 0.3
    local isLineFullyVisible = (lineTopInContentY <= viewportTopInContentY) and (lineBottomInContentY >= viewportBottomInContentY + safetyMargin)

    return isLineFullyVisible
end

DialogSignCtrl._ScrollHalfViewportOrToBottom = HL.Method() << function(self)
    local scrollView = self.view.scrollView
    if scrollView == nil or not scrollView.vertical then
        return
    end

    local contentRect = scrollView.content
    local viewportRect = scrollView.viewport

    local viewportHeight = viewportRect.rect.height
    local contentHeight = contentRect.rect.height

    if contentHeight <= viewportHeight then
        return
    end

    local currentContentPosX = contentRect.anchoredPosition.x
    local currentContentPosY = contentRect.anchoredPosition.y

    local maxScrollY = contentHeight - viewportHeight
    local halfViewportHeight = viewportHeight * 0.75
    local targetPosY = currentContentPosY + halfViewportHeight

    if targetPosY >= maxScrollY then
        targetPosY = maxScrollY
        self.m_hasScrolledToBottom = true
    end

    local newContentPos = Vector2(currentContentPosX, targetPosY)
    scrollView:ScrollTo(newContentPos, false)
end

DialogSignCtrl.OnTextPlayComplete = HL.Method() << function(self)
    self.m_textPlayCompleted = true
    self.view.scrollView.disableScroll = false
    self.view.scrollView.controllerScrollEnabled = true
    self.view.scrollViewImage.raycastTarget = true
    self.view.content.uiText.raycastTarget = true
    self.view.scrollKeyHint.gameObject:SetActive(true)

    self.m_enableGlossaryPopUp = true
    self:_RefreshGlossaryNoteKeyHint()

    if self.m_typeWriterCor ~= nil then
        self.m_typeWriterCor = self:_ClearCoroutine(self.m_typeWriterCor)
    end

    if self.m_optionDelayTime > 0 then
        self.m_optionTimer = self:_StartTimer(self.m_optionDelayTime, function()
            if self.m_isClosed then
                return
            end
            self:ShowOption()
        end)
    else
        self:ShowOption()
    end
end

DialogSignCtrl.ShowOption = HL.Method() << function(self)
    local data = {
        optionId = self.m_optionConfig.optionId,
        index = 1,
        text = self.m_optionConfig.optionText or "",
        iconType = self.m_optionConfig.iconType,
        icon = self.m_optionConfig.optionIcon,
        color = self.m_optionConfig.useExOptionColor and  self.m_optionConfig.optionIconColor or nil,
        setGreyed = self.m_optionConfig.setGreyed,
    }

    self.view.optionCell.gameObject:SetActive(true)
    self.view.optionCell.view.button.interactable = true
    self.view.optionCell:InitDialogOptionCell(data, function() self:OnOptionSelect() end)
    self.view.optionCell.view.keyHintContent.gameObject:SetActive(true)
end

DialogSignCtrl.OnOptionSelect = HL.Method() << function(self)
    if GameWorld.dialogTimelineManager.canClick then
        GameWorld.dialogTimelineManager:Next()
    end

    self.view.optionCell.view.animationWrapper:PlayOutAnimation()
    self.view.optionCell.view.button.interactable = false
    self:StartSign()
end

DialogSignCtrl.StartSign = HL.Method() << function(self)

    if self.m_signDelayTime < 0 then
        return
    end
    self.m_signTimer = self:_StartTimer(self.m_signDelayTime, function()
        if self.m_isClosed then
            return
        end
        self.view.signNode.gameObject:SetActive(true)
    end)
end

DialogSignCtrl.OnHideDialogSignOption = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end

DialogSignCtrl.OnDialogTimelineFinish = HL.Method() << function(self)
    PhaseManager:ExitPhaseFast(PHASE_ID)
end

DialogSignCtrl.OnClose = HL.Override() << function(self)
    if self.m_typeWriterCor ~= nil then
        self.m_typeWriterCor = self:_ClearCoroutine(self.m_typeWriterCor)
    end

    if self.m_optionTimer ~= -1 then
        self.m_optionTimer = self:_ClearTimer(self.m_optionTimer)
    end

    if self.m_signTimer ~= -1 then
        self.m_signTimer = self:_ClearTimer(self.m_signTimer)
    end

    if GameWorld.dialogTimelineManager.autoMode ~= self.m_dialogAutoMode then
        GameWorld.dialogTimelineManager:SetAutoMode(self.m_dialogAutoMode)
    end
    DialogSignCtrl.Super.OnClose(self)
end


HL.Commit(DialogSignCtrl)
