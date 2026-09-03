local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local DialogProcessActionType = {
    UpdateCount = 1,
    ShowOption = 2,
    ShowAdditiveResult = 3,
}

local SNSDialogContentType = GEnums.SNSDialogContentType
local LoadingState = {
    None = 0,
    Bubble = 1,
    ContentIn = 2,
    Interval = 3,
    Finish = 4,
}

local sns = GameInstance.player.sns
local SNSContentLinkNodeType = CS.Beyond.Gameplay.SNSContentLinkNodeType

local SNSDialogOptionType = GEnums.SNSDialogOptionType
local SNSGroupDialogTagType = GEnums.SNSGroupDialogTagType


local OFFSET_BOTTOM_PADDING = 78
local FIRST_PLACEHOLDER_HEIGHT = 26
local TITLE_HEIGHT = 40
local ContentType2Height = {
    [SNSDialogContentType.Image] = SNSUtils.PIC_STANDARD_SIZE.y,
    [SNSDialogContentType.Sticker] = 215,
    [SNSDialogContentType.Video] = 224,
    [SNSDialogContentType.Voice] = 78,
    [SNSDialogContentType.System] = 100,
    [SNSDialogContentType.PRTS] = 332,
    [SNSDialogContentType.Task] = 160,
}
local END_LINED_HEIGHT = 120

local NaviDirection = CS.UnityEngine.UI.NaviDirection
local ContentType2CanNavi = {
    [SNSDialogContentType.Text] = false,
    [SNSDialogContentType.Image] = true,
    [SNSDialogContentType.Sticker] = false,
    [SNSDialogContentType.Video] = true,
    [SNSDialogContentType.Voice] = true,
    [SNSDialogContentType.Item] = true,
    [SNSDialogContentType.System] = false,
    [SNSDialogContentType.Card] = true,
    [SNSDialogContentType.PRTS] = true,
    [SNSDialogContentType.Vote] = false,
    [SNSDialogContentType.Task] = true,
}

SNSDialogContentCore = HL.Class('SNSDialogContentCore', UIWidgetBase)

SNSDialogContentCore.m_textOptionCellCache = HL.Field(HL.Forward("UIListCache"))

SNSDialogContentCore.m_stickerOptionCellCache = HL.Field(HL.Forward("UIListCache"))

SNSDialogContentCore.m_getDialogContentCellFunc = HL.Field(HL.Function)

SNSDialogContentCore.m_chatId = HL.Field(HL.String) << ""

SNSDialogContentCore.m_curDialogId = HL.Field(HL.String) << ""

SNSDialogContentCore.m_dialogIds = HL.Field(HL.Table)

SNSDialogContentCore.m_dialogFinishCb = HL.Field(HL.Function)


SNSDialogContentCore.m_targetUpdateCellLuaIndex = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_dialogProcessAction = HL.Field(HL.Table)

SNSDialogContentCore.m_curDialogProcessActionStep = HL.Field(HL.Number) << 0

SNSDialogContentCore.m_dialogContentInfo = HL.Field(HL.Table)

SNSDialogContentCore.m_resetClickCounterTimer = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_clickCount = HL.Field(HL.Number) << 0

SNSDialogContentCore.m_fastMode = HL.Field(HL.Boolean) << false

SNSDialogContentCore.m_accelerateTick = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_firstTimeClickTime = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_showAccelerateHintTime = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_onLongPressStartTime = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_isTopic = HL.Field(HL.Boolean) << false

SNSDialogContentCore.m_rawPaddingBottom = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_rawRectMask2DPadding = HL.Field(Vector4)

SNSDialogContentCore.m_optionsBottomNodeHeight = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_isDialogInProcess = HL.Field(HL.Boolean) << false

SNSDialogContentCore.m_contentTween = HL.Field(CS.DG.Tweening.Tween)

SNSDialogContentCore.m_cellLoadingCor = HL.Field(HL.Thread)



SNSDialogContentCore.m_startTs = HL.Field(HL.Number) << -1




SNSDialogContentCore._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_SNS_DIALOG_SET_OPTION, function(args)
        self:OnSNSDialogSetOption(args)
    end)

    self:RegisterMessage(MessageConst.ON_SNS_DIALOG_MODIFY, function(args)
        self:OnSNSDialogModify(args)
    end)

    self:RegisterMessage(MessageConst.ON_SNS_CONTENT_CORE_CELL_SIZE_CHANGED, function(args)
        self:OnSNSContentCoreCellSizeChange(args)
    end)

    self:RegisterMessage(MessageConst.ON_SNS_CONTENT_WIDGET_CLICK, function(args)
        args = args or {}
        self:_OnSNSContentWidgetClick(args)
    end)





    self.m_textOptionCellCache = UIUtils.genCellCache(self.view.textOptionCell)
    self.m_stickerOptionCellCache = UIUtils.genCellCache(self.view.stickerOptionCell)
    self.m_getDialogContentCellFunc = UIUtils.genCachedCellFunction(self.view.dialogScrollList)

    self.view.dialogScrollRect.onValueChanged:AddListener(function(normalizedPosition)
        self:_OnScrollValueChanged(normalizedPosition)
    end)

    self.view.dialogScrollList.onUpdateCell:AddListener(function(gameObject, csIndex)
        self:_OnUpdateDialogCell(gameObject, csIndex)
    end)

    self.view.accelerateBtn.onClick:AddListener(function()
        self:_OnClickAccelerateBtn()
    end)

    self.view.accelerateBtn.onPressStart:AddListener(function()
        self:_OnPressStartAccelerateBtn()
    end)

    self.view.accelerateBtn.onPressEnd:AddListener(function()
        self:_OnPressEndAccelerateBtn()
    end)

    self.view.accelerateHint.gameObject:SetActive(false)
    self.view.acceleratingTips.gameObject:SetActive(false)

    self.m_rawPaddingBottom = self.view.dialogScrollList:GetPadding().bottom
    self.m_optionsBottomNodeHeight = self.view.optionsNode:GetComponent("RectTransform").rect.height
    self.m_rawRectMask2DPadding = self.view.dialogScrollListRectMask2D.padding

    self:_InitController()

    local enableDebug = (BEYOND_DEBUG or BEYOND_DEBUG_COMMAND) and CS.Beyond.Cfg.RemoteGameCfg.instance.data.enableDebugInfo
    self.view.debugNode.gameObject:SetActive(enableDebug)
    self.view.debugTxt.text = ""
end

SNSDialogContentCore._OnCreate = HL.Override() << function(self)
    self.view.debugNode.gameObject:SetActive(false)
end

SNSDialogContentCore._OnEnable = HL.Override() << function(self)
end

SNSDialogContentCore._OnDisable = HL.Override() << function(self)
end

SNSDialogContentCore._OnDestroy = HL.Override() << function(self)
    if self.m_accelerateTick > 0 then
        self.m_accelerateTick = LuaUpdate:Remove(self.m_accelerateTick)
    end
    self:ClearAsyncHandler()

    if DeviceInfo.usingController then


        self:_TryToggleContentCoreFocus(false, self.view.dialogContentInputBindingGroupMonoTarget.groupId)
        self:_TryToggleContentCoreFocus(false, self.view.inputGroup.groupId)
    end
end

SNSDialogContentCore.InitSNSDialogContentCore = HL.Method(HL.String, HL.String, HL.Opt(HL.Function))
        << function(self, chatId, dialogId, finishCb)
    self:_FirstTimeInit()
    self:ClearAsyncHandler()


    self.view.animationWrapper:Play("sns_dialogcontent_change")

    local chatCfg = Tables.sNSChatTable[chatId]
    local dialogCfg = Tables.sNSDialogTable[dialogId]
    local missionRelated = not string.isEmpty(dialogCfg.relatedMissionId)
    local isTopic = not string.isEmpty(dialogCfg.topicId)
    local isGroup = chatCfg.chatType == GEnums.SNSChatType.Group

    self.view.official.gameObject:SetActive(isGroup and chatCfg.tagType == SNSGroupDialogTagType.Official)
    self.view.external.gameObject:SetActive(isGroup and chatCfg.tagType == SNSGroupDialogTagType.External)
    self.view.friendNode.gameObject:SetActive(not missionRelated)
    self.view.taskNode.gameObject:SetActive(missionRelated)
    self.view.nameTxt.text = chatCfg.name

    self.view.dialogScrollListRectMask2D.padding = self.m_rawRectMask2DPadding
    self.view.dialogScrollList:SetPaddingBottom(self.m_rawPaddingBottom)
    self.view.dialogScrollList:UpdateCount(0)

    self.m_isTopic = isTopic
    self.m_chatId = chatId
    self.m_dialogIds = SNSUtils.processDialogIds(chatId, dialogId, isTopic)
    self.m_dialogFinishCb = finishCb

    self.m_dialogContentInfo = {}
    self.m_targetUpdateCellLuaIndex = -1
    self.m_dialogProcessAction = {}
    self.m_curDialogProcessActionStep = 0

    self.m_isDialogInProcess = false
    self.m_contentNaviTargetCSIndexes = {}

    self:_TryStartSNS()

    self.m_accelerateTick = LuaUpdate:Remove(self.m_accelerateTick)
    self.m_accelerateTick = LuaUpdate:Add("TailTick", function(deltaTime)
        self:_AccelerateTick(deltaTime)
    end)
end

SNSDialogContentCore.OnSNSDialogSetOption = HL.Method(HL.Any) << function(self, args)


    if not self.gameObject.activeInHierarchy then
        return
    end

    if DeviceInfo.usingController then
        self:_TryToggleContentCoreFocus(false, self.view.dialogContentInputBindingGroupMonoTarget.groupId)
        InputManagerInst:ToggleGroup(self.view.dialogContentInputBindingGroupMonoTarget.groupId, false)
        self:ClearNaviTarget()

        InputManagerInst:ToggleBinding(self.m_contentNaviPreActionId, false)
        InputManagerInst:ToggleBinding(self.m_contentNaviNextActionId, false)
        InputManagerInst:ToggleBinding(self.m_focusContentActionId, false)
        InputManagerInst:ToggleBinding(self.m_stopFocusContentActionId, false)
    end

    self.view.accelerateNode.gameObject:SetActive(true)
    self.view.dialogScrollRect.controllerScrollEnabled = false
    self.view.dialogScrollListRectMask2D.padding = self.m_rawRectMask2DPadding




    local scrollToIndex = math.max(0, CSIndex(#self.m_dialogContentInfo))
    self.view.dialogScrollList:ScrollToIndex(scrollToIndex, true)

    self:_ModifySNSDialogData(args)
    self:_PostProcessDialogData()
    self:_StartDialogProcessAction(1)

    local showOptionsNode = self.view.optionsNode.gameObject.activeInHierarchy
    if showOptionsNode then
        self.view.optionsNodeAnimationWrapper:PlayOutAnimation(function()
            self.view.optionsNode.gameObject:SetActive(false)
        end)
    end
end

SNSDialogContentCore.OnSNSDialogModify = HL.Method(HL.Table) << function(self, args)
end

SNSDialogContentCore.OnSNSContentCoreCellSizeChange = HL.Method(HL.Table) << function(self, args)


    if not self.gameObject.activeInHierarchy then
        return
    end

    local csIndex, height = unpack(args)
    self.view.dialogScrollList:NotifyCellSizeChange(csIndex, height)
end

SNSDialogContentCore.OnUIPhaseExited = HL.Method(HL.String) << function(self, oldPhaseName)

    if not DeviceInfo.usingController then
        return
    end

    if PhaseManager:GetTopPhaseId() ~= PhaseId.SNS then
        return
    end

    if not self.m_isFocusingContentCore then
        return
    end

    local contentCSIndex = self.m_contentNaviTargetCSIndexes[self.m_curContentNaviTargetCSIndex]
    local cell = self.m_getDialogContentCellFunc(LuaIndex(contentCSIndex))
    self:SetNaviTarget(cell:GetNaviTarget())
end

SNSDialogContentCore._StartSNSDialog = HL.Method() << function(self)
    self.m_startTs = DateTimeUtils.GetCurrentTimestampBySeconds()
    self:_StartCoroutine(function()
        coroutine.step()
        self.m_curDialogProcessActionStep = 0
        self:_StartDialogProcessAction(1)
        self:_EventLogStartDialog()
    end)

    if DeviceInfo.usingController then
        self:_UpdateContentNodeFocusableState()

        if self:_IsSidePanel() then

            self:_ManuallyFocusContentCore()

            InputManagerInst:ToggleBinding(self.m_stopFocusContentCoreActionId, false)
            InputManagerInst:ToggleBinding(self.m_stopFocusContentCoreDirActionId, false)
        end
    end
end

SNSDialogContentCore._ModifySNSDialogData = HL.Method(HL.Any) << function(self, args)
    local _, dialogId, curContentId = unpack(args)
    local dialogInfo = sns.dialogInfoDic:get_Item(dialogId)

    local contentLink = dialogInfo.contentLink
    local preContentLinkCount = contentLink.Count
    dialogInfo:ProcessContentLink()

    local curDialogContentCellCount = #self.m_dialogContentInfo
    local accumulateCellCount = #self.m_dialogContentInfo


    for csIndex = preContentLinkCount - 1, contentLink.Count - 1 do

        local contentLinkNode = contentLink[csIndex]

        if contentLinkNode.linkNodeType == SNSContentLinkNodeType.Content then
            if contentLinkNode.additive then
                table.insert(self.m_dialogProcessAction, {
                    type = DialogProcessActionType.ShowAdditiveResult,
                    dialogId = dialogId,
                    additiveResultIndex = csIndex,
                    curDialogContentCellCount = accumulateCellCount,
                    contentSendMsg = true,
                    contentId = contentLinkNode.contentId,
                })
            else
                local latestContentInfo = self.m_dialogContentInfo[#self.m_dialogContentInfo]
                table.insert(self.m_dialogContentInfo,
                             self:_GenDialogContentInfo(self.m_chatId,
                                                        dialogId,
                                                        contentLinkNode.contentId,
                                                        latestContentInfo and latestContentInfo.contentId or 0,
                                                        false,
                                                        accumulateCellCount,
                                                        csIndex,
                                                        true))

                accumulateCellCount = accumulateCellCount + 1
                table.insert(self.m_dialogProcessAction, {
                    type = DialogProcessActionType.UpdateCount,
                    dialogId = dialogId,
                    count = accumulateCellCount,
                    targetUpdateCellLuaIndex = accumulateCellCount
                })
            end

        end

        if contentLinkNode.linkNodeType == SNSContentLinkNodeType.Option then
            if contentLinkNode.hasOptionResult then
                if contentLinkNode.additive then


                    table.insert(self.m_dialogProcessAction, {
                        type = DialogProcessActionType.ShowAdditiveResult,
                        dialogId = dialogId,
                        additiveResultIndex = csIndex,
                        curDialogContentCellCount = curDialogContentCellCount,
                    })
                end
            else
                table.insert(self.m_dialogProcessAction, {
                    type = DialogProcessActionType.ShowOption,
                    dialogId = dialogId,
                    nodeIndex = csIndex,
                    accumulateCellCount = accumulateCellCount,
                })


                break
            end
        end
    end
end

SNSDialogContentCore._InitSNSDialogData = HL.Method() << function(self)

    local dialogProcessAction = {}

    local dialogContentInfo = {}

    self.m_dialogContentInfo = dialogContentInfo
    self.m_dialogProcessAction = dialogProcessAction

    local chatId = self.m_chatId
    local dialogIds = self.m_dialogIds


    local disposableUpdateCount = 0

    for _, dialogId in ipairs(dialogIds) do
        local dialogInfo = sns.dialogInfoDic:get_Item(dialogId)
        local contentLink = dialogInfo.contentLink
        dialogInfo:ProcessContentLink()

        local dialogCfg = Tables.sNSDialogTable[dialogId]




        local flagCSIndex
        for csIndex = 0, contentLink.Count - 1 do

            local contentLinkNode = contentLink[csIndex]
            local contentInfoCount = #dialogContentInfo

            if contentLinkNode.linkNodeType == SNSContentLinkNodeType.Content then




                local isRead = contentLinkNode.hasRead or
                        not self.m_isTopic and dialogInfo.pureContent and dialogCfg.skipToFirstOption or
                        not self.m_isTopic and dialogInfo.pureContent and #dialogContentInfo == 0
                if isRead then
                    if contentLinkNode.additive then

                        dialogContentInfo[contentInfoCount].additiveCSIndex = csIndex
                    else
                        local latestContentInfo = dialogContentInfo[#dialogContentInfo]
                        table.insert(dialogContentInfo,
                                     self:_GenDialogContentInfo(chatId,
                                                                dialogId,
                                                                contentLinkNode.contentId,
                                                                latestContentInfo and latestContentInfo.contentId or 0,
                                                                isRead,
                                                                contentInfoCount,
                                                                csIndex,
                                                                false))
                    end
                else

                    flagCSIndex = csIndex
                    disposableUpdateCount = #dialogContentInfo
                    break
                end
            end

            if contentLinkNode.linkNodeType == SNSContentLinkNodeType.Option then
                if contentLinkNode.hasOptionResult then
                    if contentLinkNode.additive then

                        dialogContentInfo[contentInfoCount].additiveCSIndex = csIndex
                    end

                else

                    flagCSIndex = csIndex
                    disposableUpdateCount = #dialogContentInfo
                    break
                end
            end
        end


        flagCSIndex = flagCSIndex or contentLink.Count




        local accumulateCellCount = #dialogContentInfo
        for csIndex = flagCSIndex, contentLink.Count - 1 do

            local contentLinkNode = contentLink[csIndex]

            if contentLinkNode.linkNodeType == SNSContentLinkNodeType.Content then
                if contentLinkNode.additive then



                    table.insert(dialogProcessAction, {
                        type = DialogProcessActionType.ShowAdditiveResult,
                        dialogId = dialogId,
                        additiveResultIndex = csIndex,
                        curDialogContentCellCount = accumulateCellCount,
                        contentSendMsg = true,
                        contentId = contentLinkNode.contentId,
                    })
                else
                    local latestContentInfo = dialogContentInfo[#dialogContentInfo]
                    table.insert(dialogContentInfo,
                                 self:_GenDialogContentInfo(chatId,
                                                            dialogId,
                                                            contentLinkNode.contentId,
                                                            latestContentInfo and latestContentInfo.contentId or 0,
                                                            contentLinkNode.hasRead,
                                                            accumulateCellCount,
                                                            csIndex,
                                                            true))
                    accumulateCellCount = accumulateCellCount + 1

                    table.insert(dialogProcessAction, {
                        type = DialogProcessActionType.UpdateCount,
                        dialogId = dialogId,
                        count = accumulateCellCount,
                        targetUpdateCellLuaIndex = accumulateCellCount
                    })
                end
            end

            if contentLinkNode.linkNodeType == SNSContentLinkNodeType.Option then
                table.insert(dialogProcessAction, {
                    type = DialogProcessActionType.ShowOption,
                    dialogId = dialogId,
                    nodeIndex = csIndex,
                    accumulateCellCount = accumulateCellCount,
                })


                break
            end
        end

    end


    if disposableUpdateCount == 0 and #dialogProcessAction == 0 then
        disposableUpdateCount = #dialogContentInfo
    end

    if disposableUpdateCount > 0 then
        table.insert(dialogProcessAction, 1, {
            type = DialogProcessActionType.UpdateCount,
            dialogId = dialogIds[1],
            count = disposableUpdateCount,


            needInitContent = true,
        })
    end

end

SNSDialogContentCore._PostProcessDialogData = HL.Method() << function(self)

    if self.m_isTopic and #self.m_dialogContentInfo > 0 then
        local lastContent = self.m_dialogContentInfo[#self.m_dialogContentInfo]
        local succ, chatInfo = sns.chatInfoDic:TryGetValue(self.m_chatId)
        if lastContent.contentId < 0
                and succ and not chatInfo.hasTopicToStart
        then

            lastContent.forceSystemMsg = true
            lastContent.langKey = "LUA_SNS_TOPIC_CONTENT_ALL_DIALOG_FINISH"
        end
    end
end

SNSDialogContentCore._GenDialogContentInfo = HL.Method(HL.String, HL.String, HL.Number, HL.Number, HL.Boolean,
                                                       HL.Number, HL.Number, HL.Boolean).Return(HL.Table)
        << function(self, chatId, dialogId, contentId, preContentId, isRead, contentCellCSIndex, linkNodeCSIndex, sendMsg)


    return {
        chatId = chatId,
        dialogId = dialogId,
        contentId = contentId,
        preContentId = preContentId,
        isRead = isRead,
        isLoaded = isRead,

        contentCellCSIndex = contentCellCSIndex,
        contentLinkCSIndex = linkNodeCSIndex,
        sendMsg = sendMsg,
    }
end

SNSDialogContentCore._ShowDialogOption = HL.Method(HL.String, HL.Number, HL.Number)
        << function(self, dialogId, contentLinkNodeCSIndex, cellLuaIndex)
    local dialogInfo = sns.dialogInfoDic:get_Item(dialogId)

    local contentLink = dialogInfo.contentLink

    local node = contentLink[contentLinkNodeCSIndex]
    local contentCfg = Tables.sNSDialogTable[dialogId].dialogContentData[node.contentId]
    local optionType = contentCfg.optionType
    local showOptionsNode = optionType ~= SNSDialogOptionType.EmojiComment

    local options = {}
    for optionCSIndex = 0, contentCfg.dialogOptionIds.Count - 1 do
        local optionId = contentCfg.dialogOptionIds[optionCSIndex]
        local optionCfg = Tables.sNSDialogOptionTable[optionId]
        local option = {
            optionId = optionId,
            desc = optionCfg.optionDesc,
            path = optionCfg.optionResPath,
        }
        table.insert(options, option)
    end


    if optionType == SNSDialogOptionType.Text or
            optionType == SNSDialogOptionType.Vote then
        self.m_textOptionCellCache:Refresh(#options, function(cell, luaIndex)
            local option = options[luaIndex]
            cell.descTxt:SetAndResolveTextStyle(SNSUtils.resolveTextStyleWithPlayerName(option.desc))
            cell.button.onClick:RemoveAllListeners()
            cell.button.onClick:AddListener(function()
                sns:SelectDialogOption(node.chatId, node.dialogId, node.contentId, option.optionId)
                self:_EvenLogSetOption(option.optionId)
            end)
        end)
    elseif optionType == SNSDialogOptionType.Sticker then
        self.m_stickerOptionCellCache:Refresh(#options, function(cell, luaIndex)
            local option = options[luaIndex]
            cell.sticker:LoadSprite(UIConst.UI_SPRITE_SNS_STICKER, option.path)
            cell.button.onClick:RemoveAllListeners()
            cell.button.onClick:AddListener(function()
                sns:SelectDialogOption(node.chatId, node.dialogId, node.contentId, option.optionId)
                self:_EvenLogSetOption(option.optionId)
            end)
        end)
    elseif optionType == SNSDialogOptionType.EmojiComment then
        local contentInfo = self.m_dialogContentInfo[#self.m_dialogContentInfo]

        contentInfo.showBubble = true
        contentInfo.showBubbleArgs = {
            number = #options,
            refreshFunc = function(cell, luaIndex)
                local option = options[luaIndex]
                cell.image:LoadSprite(UIConst.UI_SPRITE_SNS_EMOJI, option.path)
                cell.button.onClick:RemoveAllListeners()
                cell.button.onClick:AddListener(function()
                    sns:SelectDialogOption(node.chatId, node.dialogId, node.contentId, option.optionId)
                    self:_EvenLogSetOption(option.optionId)
                end)
            end,
        }


        local contentCell = self.m_getDialogContentCellFunc(cellLuaIndex)
        contentCell:ShowEmojiCommentOption()
    else
        logger.error(string.format("SNSDialogContentCore._ShowDialogOption but optionType == None, dialogId:%s, contentLinkNodeCSIndex:%d",
                                   dialogId, contentLinkNodeCSIndex))
    end
    self.view.textOptions.gameObject:SetActive(optionType == SNSDialogOptionType.Text or optionType == SNSDialogOptionType.Vote)
    self.view.stickerOptions.gameObject:SetActive(optionType == SNSDialogOptionType.Sticker)

    self.view.optionsNode.gameObject:SetActive(showOptionsNode)
    self.view.accelerateNode.gameObject:SetActive(false)
    self.view.dialogScrollRect.controllerScrollEnabled = true

    self:_ResetAccelerating()

    self.view.decoNode.gameObject:SetActive(false)

    if showOptionsNode then
        AudioAdapter.PostEvent("Au_UI_Popup_SNSDialogContent_Options_Open")
        self.view.dialogScrollList:SetPaddingBottom(self.m_rawPaddingBottom + self.m_optionsBottomNodeHeight)
        self.view.dialogScrollListRectMask2D.padding = self.m_rawRectMask2DPadding +
                Vector4(0, self.m_optionsBottomNodeHeight, 0, 0)
        self:_OnBottomNodeShow()
    end

    InputManagerInst:ToggleGroup(self.view.dialogContentInputBindingGroupMonoTarget.groupId, true)
    if DeviceInfo.usingController then
        if self.m_isFocusingContentCore then
            if showOptionsNode then

                self:_TrySetOptionsNodeTarget()
            else

                self:_ManuallyFocusContent()
            end
        end

    end
end

SNSDialogContentCore._OnBottomNodeShow = HL.Method() << function(self)
    local inClipTime = self.view.optionsNodeAnimationWrapper:GetInClipLength()
    local count = self.m_targetUpdateCellLuaIndex < 0 and #self.m_dialogContentInfo or self.m_targetUpdateCellLuaIndex
    self:_ScrollToBottom(false, inClipTime, function()

        self.view.dialogScrollList:UpdateCount(count, CSIndex(count))
        self.view.dialogScrollRect.normalizedPosition = Vector2(0, 0)
    end)
end

SNSDialogContentCore._StartDialogProcessAction = HL.Method(HL.Number) << function(self, offset)
    self:_UpdateContentNodeFocusableState()

    local nextStep = self.m_curDialogProcessActionStep + offset
    if nextStep > #self.m_dialogProcessAction then
        self.m_isDialogInProcess = false
        self.m_targetUpdateCellLuaIndex = -1
        if DeviceInfo.usingController then
            self:_UpdateContentNodeFocusableState()
        end
        self:_CheckSNSDialogFinish()
        self:_UpdateNaviTargetIndexes()
        return
    end
    local debug = self.m_dialogContentInfo
    self.m_curDialogProcessActionStep = nextStep

    local nextProgressAction = self.m_dialogProcessAction[nextStep]
    self.m_isDialogInProcess = nextProgressAction.needInitContent ~= true
    self.m_curDialogId = nextProgressAction.dialogId
    self.view.debugTxt.text = nextProgressAction.dialogId


    if nextProgressAction.type == DialogProcessActionType.UpdateCount then
        local paddingBottom = self.view.dialogScrollList:GetPadding().bottom
        if nextProgressAction.targetUpdateCellLuaIndex then

            local offsetPadding = self:_GetUpdateCellHeight(nextProgressAction.targetUpdateCellLuaIndex)
            paddingBottom = math.max(self.m_rawPaddingBottom, self.view.dialogScrollList:GetPadding().bottom - offsetPadding)
        end
        self.view.dialogScrollList:SetPaddingBottom(paddingBottom)

        self.m_targetUpdateCellLuaIndex = nextProgressAction.targetUpdateCellLuaIndex or -1
        self.view.dialogScrollList:UpdateCount(nextProgressAction.count)
        self.view.dialogScrollList:ScrollToIndex(CSIndex(nextProgressAction.count), true)
        self:_ScrollToBottom(true)

        if nextProgressAction.needInitContent then
            self:_StartDialogProcessAction(1)
        else
            self:_DoCellLoadingAction()
        end
    elseif nextProgressAction.type == DialogProcessActionType.ShowAdditiveResult then



        local curContentCellLuaIndex = nextProgressAction.curDialogContentCellCount
        local additiveResultIndex = nextProgressAction.additiveResultIndex

        local cell = self.m_getDialogContentCellFunc(curContentCellLuaIndex)
        local duration = cell:ShowAdditiveResult(nextProgressAction.dialogId, additiveResultIndex, false, function(csIndex, size)
            self:_OnCellSizeChangeAndScrollToBottom(csIndex, size)
        end)

        self.m_dialogContentInfo[curContentCellLuaIndex].additiveCSIndex = additiveResultIndex

        if nextProgressAction.contentSendMsg then
            sns:ModifyDialogCurContent(self.m_chatId, nextProgressAction.dialogId, nextProgressAction.contentId)
        end

        if duration > 0 then
            self.m_cellLoadingCor = self:_ClearCoroutine(self.m_cellLoadingCor)
            self.m_cellLoadingCor = self:_StartCoroutine(function()
                coroutine.wait(duration)
                self:_StartDialogProcessAction(1)
            end)
        else
            self:_StartDialogProcessAction(1)
        end

    elseif nextProgressAction.type == DialogProcessActionType.ShowOption then

        self:_ShowDialogOption(nextProgressAction.dialogId, nextProgressAction.nodeIndex, nextProgressAction.accumulateCellCount)
        self:_StartDialogProcessAction(1)
    end

end

SNSDialogContentCore._CheckSNSDialogFinish = HL.Method() << function(self)
    local actionCount = #self.m_dialogProcessAction
    local lastAction = self.m_dialogProcessAction[actionCount]
    if lastAction.type == DialogProcessActionType.ShowOption then
        return
    end



    self.view.accelerateNode.gameObject:SetActive(false)
    self.view.dialogScrollRect.controllerScrollEnabled = true

    InputManagerInst:ToggleGroup(self.view.dialogContentInputBindingGroupMonoTarget.groupId, true)
    self:_ResetAccelerating()


    if self.m_dialogFinishCb then
        self.m_dialogFinishCb()
        self:_OnBottomNodeShow()
    else
        local count = self.m_targetUpdateCellLuaIndex < 0 and #self.m_dialogContentInfo or self.m_targetUpdateCellLuaIndex

        self.view.dialogScrollList:UpdateCount(count, CSIndex(count))
        self.view.dialogScrollRect.normalizedPosition = Vector2(0, 0)
    end

    if self.m_isTopic then
        self:_TryShowOptionsOfStartTopic()
    end

    self:_EventLogEndDialog()
end

SNSDialogContentCore._GetUpdateCellHeight = HL.Method(HL.Number).Return(HL.Number)
        << function(self, targetUpdateCellLuaIndex)
    local height = OFFSET_BOTTOM_PADDING

    local contentInfo = self.m_dialogContentInfo[targetUpdateCellLuaIndex]
    local chatId = contentInfo.chatId
    local dialogId = contentInfo.dialogId

    local chatCfg = Tables.sNSChatTable[chatId]
    local dialogCfg = Tables.sNSDialogTable[dialogId]
    local dialogContentCfg = dialogCfg.dialogContentData
    local contentCfg = dialogContentCfg[contentInfo.contentId]
    local contentType = contentCfg.contentType

    local isGroup = chatCfg.chatType == GEnums.SNSChatType.Group
    local isContentSystem = contentType == SNSDialogContentType.System or
            contentType == SNSDialogContentType.Task or
            contentCfg.isEnd
    local preContentId = contentInfo.preContentId
    local isFirst
    if not dialogContentCfg:ContainsKey(preContentId) then
        isFirst = true
    else
        isFirst = dialogContentCfg[preContentId].optionType ~= GEnums.SNSDialogOptionType.None or
                dialogContentCfg[preContentId].speaker ~= contentCfg.speaker
    end

    if isContentSystem then
        if contentCfg.isEnd and contentInfo.forceSystemMsg then
            height = ContentType2Height[SNSDialogContentType.System]
        elseif contentCfg.isEnd then
            height = END_LINED_HEIGHT
        else
            height = ContentType2Height[contentCfg.contentType]
        end
    else
        local contentHeight = ContentType2Height[contentCfg.contentType] or OFFSET_BOTTOM_PADDING
        height = contentHeight + (isFirst and FIRST_PLACEHOLDER_HEIGHT or 0) + (isGroup and TITLE_HEIGHT or 0)
    end


    height = height or OFFSET_BOTTOM_PADDING


    return height
end

SNSDialogContentCore._OnUpdateDialogCell = HL.Method(GameObject, HL.Number) << function(self, gameObject, csIndex)
    local luaIndex = LuaIndex(csIndex)
    if self.m_isDialogInProcess and self.m_targetUpdateCellLuaIndex ~= luaIndex then


        return
    end

    local contentInfo = self.m_dialogContentInfo[luaIndex]

    local cell = self.m_getDialogContentCellFunc(gameObject)



    contentInfo.maxTextWidth = self.view.config.MAX_TEXT_WIDTH

    contentInfo.taskHorPadding = self.view.config.CONTENT_TASK_HOR_PADDING

    cell:InitSNSDialogContentCoreCell(contentInfo)
    LayoutRebuilder.ForceRebuildLayoutImmediate(cell.rectTransform)


end

SNSDialogContentCore._OnScrollValueChanged = HL.Method(Vector2) << function(self, normalizedPos)
    if DeviceInfo.usingController then
        local naviTarget = self:_GetShowingCellNaviTarget()
        InputManagerInst:ToggleBinding(self.m_focusContentActionId, self.m_isFocusingContentCore and not self.m_isDialogInProcess and
                naviTarget ~= nil)
    end
end

SNSDialogContentCore._OnCellSizeChangeAndScrollToBottom = HL.Method(HL.Number, HL.Number) << function(self, csIndex, size)
    self.view.dialogScrollList:NotifyCellSizeChange(csIndex, size)
    self:_ScrollToBottom(true)
end

SNSDialogContentCore._ScrollToBottom = HL.Method(HL.Boolean, HL.Opt(HL.Number, HL.Function))
        << function(self, noTween, duration, onComplete)

    if self.m_contentTween then
        self.m_contentTween:Kill(true)
    end

    if noTween then
        self.view.dialogScrollRect.normalizedPosition = Vector2(0, 0)
    else
        self.m_contentTween = DOTween.To(function()
            return self.view.dialogScrollRect.normalizedPosition.y
        end, function(value)
            local x = self.view.dialogScrollRect.normalizedPosition.x
            self.view.dialogScrollRect.normalizedPosition = Vector2(x, value)
        end, 0, duration)

        self.m_contentTween:OnComplete(function()
            if onComplete then
                onComplete()
            end
        end)
    end
end

SNSDialogContentCore._IsSidePanel = HL.Method().Return(HL.Boolean) << function(self)
    return self:GetUICtrl():GetPanelType() == SNSUtils.PanelType.SidePanel
end



SNSDialogContentCore._TryStartSNS = HL.Method() << function(self)
    local resultDialogCount = #self.m_dialogIds
    local latestDialogId = self.m_dialogIds[resultDialogCount] or ""
    local latestDialogIsFinish = sns:DialogHasEnd(latestDialogId)

    self.view.accelerateNode.gameObject:SetActive(not latestDialogIsFinish)
    self.view.dialogScrollRect.controllerScrollEnabled = latestDialogIsFinish
    self.view.dialogScrollListRectMask2D.padding = self.m_rawRectMask2DPadding

    InputManagerInst:ToggleGroup(self.view.dialogContentInputBindingGroupMonoTarget.groupId, latestDialogIsFinish)
    self.view.optionsNode.gameObject:SetActive(false)


    if not self.m_isTopic or resultDialogCount ~= 0 then
        self:_InitSNSDialogData()
        self:_PostProcessDialogData()
        self:_StartSNSDialog()
    end


    if self.m_isTopic and (resultDialogCount == 0 or latestDialogIsFinish) then
        self:_TryShowOptionsOfStartTopic()
    end
end

SNSDialogContentCore._TryShowOptionsOfStartTopic = HL.Method() << function(self)
    local showingTopicDialogInfos = SNSUtils.getShowingTopicDialogInfos(self.m_chatId)
    local showingTopicDialogCount = #showingTopicDialogInfos
    local showOptionsNode = showingTopicDialogCount > 0
    if showOptionsNode then
        self.m_textOptionCellCache:Refresh(showingTopicDialogCount, function(cell, luaIndex)
            local topicInfo = showingTopicDialogInfos[luaIndex]
            local topicCfg = Tables.sNSDialogTopicTable[topicInfo.topicId]
            cell.descTxt:SetAndResolveTextStyle(SNSUtils.resolveTextStyleWithPlayerName(topicCfg.topicStartOptionDesc))
            cell.button.onClick:RemoveAllListeners()
            cell.button.onClick:AddListener(function()
                self:_OnClickOptionOfStartTopicCell(topicInfo.dialogId)
            end)
        end)

        self.view.decoNode.gameObject:SetActive(true)

        local topicDialogIds = {}
        for _, info in ipairs(showingTopicDialogInfos) do
            table.insert(topicDialogIds, info.dialogId)
        end
        sns:ReadDialogs(topicDialogIds)
    end
    self.view.textOptions.gameObject:SetActive(showOptionsNode)
    self.view.optionsNode.gameObject:SetActive(showOptionsNode)
    self.view.accelerateNode.gameObject:SetActive(false)
    self.view.dialogScrollRect.controllerScrollEnabled = true

    InputManagerInst:ToggleGroup(self.view.dialogContentInputBindingGroupMonoTarget.groupId, true)
    self:_UpdateContentNodeFocusableState()
    self:_ResetAccelerating()

    if showOptionsNode then
        self.view.dialogScrollList:SetPaddingBottom(self.m_rawPaddingBottom + self.m_optionsBottomNodeHeight)
        self.view.dialogScrollListRectMask2D.padding = self.m_rawRectMask2DPadding +
                Vector4(0, self.m_optionsBottomNodeHeight, 0, 0)
        self:_OnBottomNodeShow()
    end

    if DeviceInfo.usingController then
        local showOptionsNode = self.view.textOptions.gameObject.activeInHierarchy
        if showOptionsNode and self.m_isFocusingContentCore then
            self:_TrySetOptionsNodeTarget()
        end
    end
end

SNSDialogContentCore._OnClickOptionOfStartTopicCell = HL.Method(HL.String) << function(self, dialogId)
    self.view.accelerateNode.gameObject:SetActive(true)
    self.view.dialogScrollRect.controllerScrollEnabled = false
    self.view.dialogScrollListRectMask2D.padding = self.m_rawRectMask2DPadding
    InputManagerInst:ToggleGroup(self.view.dialogContentInputBindingGroupMonoTarget.groupId, false)

    table.insert(self.m_dialogIds, dialogId)

    self:_InitSNSDialogData()
    self:_PostProcessDialogData()
    self.view.optionsNodeAnimationWrapper:PlayOutAnimation(function()
        self.view.optionsNode.gameObject:SetActive(false)

        self:_StartSNSDialog()
    end)
end





SNSDialogContentCore.ClearAsyncHandler = HL.Method() << function(self)
    self.m_cellLoadingCor = self:_ClearCoroutine(self.m_cellLoadingCor)

    if self.m_contentTween then
        self.m_contentTween:Kill(true)
    end
end

SNSDialogContentCore._OnLoadingStateChange = HL.Method(HL.Number) << function(self, nextState)
    local contentInfo = self.m_dialogContentInfo[self.m_targetUpdateCellLuaIndex]
    contentInfo.loadingState = nextState
    self:_DoCellLoadingAction()
end

SNSDialogContentCore._DoCellLoadingAction = HL.Method() << function(self)
    if self.m_targetUpdateCellLuaIndex < 1 then
        return
    end

    local contentInfo = self.m_dialogContentInfo[self.m_targetUpdateCellLuaIndex]

    local cell = self.m_getDialogContentCellFunc(self.m_targetUpdateCellLuaIndex)
    if cell == nil then
        logger.error("[SNS]SNSDialogContentCore _DoCellLoadingAction Fail, dialogId:", self.m_curDialogId, ",contentId:", contentInfo.contentId, ",dialogIds:", self.m_dialogIds)
        return
    end
    local ratio = self.m_fastMode and self.view.config.ACCELERATING_RATIO or 1

    if contentInfo.loadingState == LoadingState.Bubble then
        cell:ShowLoadingNode()


        local loadingTime = cell:GetLoadingTime(ratio)
        self.m_cellLoadingCor = self:_ClearCoroutine(self.m_cellLoadingCor)
        self.m_cellLoadingCor = self:_StartCoroutine(function()
            coroutine.wait(loadingTime)
            self:_OnLoadingStateChange(LoadingState.ContentIn)
        end)
    elseif contentInfo.loadingState == LoadingState.ContentIn then
        cell:InitContent()
        LayoutRebuilder.ForceRebuildLayoutImmediate(cell.rectTransform)
        self:_OnCellSizeChangeAndScrollToBottom(contentInfo.contentCellCSIndex, cell.rectTransform.rect.height)
        Notify(MessageConst.ON_SNS_DIALOG_CONTENT_LOADING_FINISH, { contentInfo.chatId,
                                                                    contentInfo.dialogId,
                                                                    contentInfo.contentId })

        if not contentInfo.isRead then
            contentInfo.isRead = true
            sns:ModifyDialogCurContent(contentInfo.chatId, contentInfo.dialogId, contentInfo.contentId)
        end

        local contentAnimationInTime = cell:GetContentInTime(ratio)
        self.m_cellLoadingCor = self:_ClearCoroutine(self.m_cellLoadingCor)
        self.m_cellLoadingCor = self:_StartCoroutine(function()
            coroutine.step()

            coroutine.wait(contentAnimationInTime)
            self:_OnLoadingStateChange(LoadingState.Interval)
        end)
    elseif contentInfo.loadingState == LoadingState.Interval then
        local intervalTime = cell:GetIntervalTime(ratio)
        self.m_cellLoadingCor = self:_ClearCoroutine(self.m_cellLoadingCor)
        self.m_cellLoadingCor = self:_StartCoroutine(function()
            coroutine.wait(intervalTime)
            self:_OnLoadingStateChange(LoadingState.Finish)
        end)
    elseif contentInfo.loadingState == LoadingState.Finish then
        self.m_cellLoadingCor = self:_ClearCoroutine(self.m_cellLoadingCor)

        if not contentInfo.isLoaded then
            contentInfo.isLoaded = true
            self:_StartDialogProcessAction(1)

        end
    end
end





SNSDialogContentCore._AccelerateTick = HL.Method(HL.Number) << function(self, deltaTime)
    if self.m_firstTimeClickTime > 0 and
            Time.unscaledTime - self.m_firstTimeClickTime > self.view.config.RESET_CLICK_COUNT_TIME then
        self.m_clickCount = 0
        self.m_firstTimeClickTime = -1
    end

    if self.m_showAccelerateHintTime > 0 and
            Time.unscaledTime - self.m_showAccelerateHintTime > self.view.config.ACCELERATE_HINT_DURATION then
        self.view.accelerateHint:PlayOutAnimation(function()
            self.view.accelerateHint.gameObject:SetActive(false)
        end)
        self.m_showAccelerateHintTime = -1
    end

    if self.m_onLongPressStartTime > 0 and
            Time.unscaledTime - self.m_onLongPressStartTime > self.view.config.LONG_PRESS_ACCELERATING_TIME_THRESHOLD then
        self.m_fastMode = true
        self.view.acceleratingTips.gameObject:SetActive(true)
        self.view.accelerateHint:PlayOutAnimation(function()
            self.view.accelerateHint.gameObject:SetActive(false)
        end)
        self.m_onLongPressStartTime = -1
    end
end

SNSDialogContentCore._OnClickAccelerateBtn = HL.Method() << function(self)
    self:_TrySkipCurCellLoadingState()

    self.m_clickCount = self.m_clickCount + 1
    if self.m_clickCount == 1 then
        self.m_firstTimeClickTime = Time.unscaledTime
    end
    if self.m_clickCount == self.view.config.TRIGGER_LONG_PRESS_HINT_CLICK_COUNT then
        self.view.accelerateHint.gameObject:SetActive(true)
        self.m_showAccelerateHintTime = Time.unscaledTime
    end
end

SNSDialogContentCore._TrySkipCurCellLoadingState = HL.Method() << function(self)

    local cell = self.m_getDialogContentCellFunc(self.m_targetUpdateCellLuaIndex)
    local contentInfo = self.m_dialogContentInfo[self.m_targetUpdateCellLuaIndex]
    if not cell then
        logger.warn("[sns] _TrySkipCurCellLoadingState error, targetUpdateCellLuaIndex:", self.m_targetUpdateCellLuaIndex)
        return
    end

    if contentInfo.loadingState == nil or
            contentInfo.loadingState == LoadingState.ContentIn then
        return
    end

    self:_OnLoadingStateChange(contentInfo.loadingState + 1)
end

SNSDialogContentCore._OnPressStartAccelerateBtn = HL.Method() << function(self)
    self.m_onLongPressStartTime = Time.unscaledTime
end

SNSDialogContentCore._OnPressEndAccelerateBtn = HL.Method() << function(self)
    self:_ResetAccelerating()
end

SNSDialogContentCore._ResetAccelerating = HL.Method() << function(self)
    self.m_onLongPressStartTime = -1
    self.m_fastMode = false
    if not self.view.acceleratingTips.gameObject.activeSelf then
        return
    end
    self.view.acceleratingTips:PlayOutAnimation(function()
        self.view.acceleratingTips.gameObject:SetActive(false)
    end)
end





local MissionState = CS.Beyond.Gameplay.MissionSystem.MissionState
local MissionType = CS.Beyond.Gameplay.MissionSystem.MissionType

SNSDialogContentCore._OnSNSContentWidgetClick = HL.Method(HL.Table) << function(self, args)


    if not self.gameObject.activeInHierarchy then
        return
    end




    if GameInstance.player.spaceship.isViewingFriend then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_OBTAIN_WAYS_JUMP_BLOCKED)
        return
    end



    if self.m_isFocusingContentCore then
        self:_ManuallyStopFocusContent()
        self:_ManuallyStopFocusContentCore()
    end

    local missionId = args.missionId
    local prtsJumpArgs = args.prtsJumpArgs
    if not string.isEmpty(missionId) then
        self:_OnClickSNSContentTask(missionId)
    elseif not string.isEmpty(prtsJumpArgs) then
        self:_OnClickSNSContentPRTS(prtsJumpArgs)
    end
end

SNSDialogContentCore._OnClickSNSContentTask = HL.Method(HL.String) << function(self, missionId)
    local missionState = GameInstance.player.mission:GetMissionState(missionId)

    local missionRuntimeAsset = GameInstance.player.mission:GetMissionInfo(missionId)
    local otherCaseHintText = missionRuntimeAsset.missionType == MissionType.Misc and
            Language["ui_sns_toast_mission_misc_failed"] or Language["ui_mis_empty_default"]
    if missionState == MissionState.Processing then
        PhaseManager:OpenPhase(PhaseId.Mission, { autoSelect = missionId, useBlackMask = true })
    elseif isComplete then
        Notify(MessageConst.SHOW_TOAST, Language["ui_sns_toast_mission_completed"])
    else
        Notify(MessageConst.SHOW_TOAST, otherCaseHintText)
    end
end

SNSDialogContentCore._OnClickSNSContentPRTS = HL.Method(HL.Table) << function(self, prtsJumpArgs)
    local prts = GameInstance.player.prts
    local unlock = prtsJumpArgs.isFirstLvId and prts:IsFirstLvUnlock(prtsJumpArgs.id) or prts:IsPrtsUnlocked(prtsJumpArgs.id)
    if unlock then
        PhaseManager:GoToPhase(PhaseId[prtsJumpArgs.phaseId], prtsJumpArgs)
    else
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SNS_DONT_HAVE_PRTS_DATA)
    end
end





SNSDialogContentCore.m_focusContentCoreActionId = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_focusContentCoreDirActionId = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_stopFocusContentCoreActionId = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_stopFocusContentCoreDirActionId = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_focusContentActionId = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_stopFocusContentActionId = HL.Field(HL.Number) << -1


SNSDialogContentCore.m_contentNaviTargetCSIndexes = HL.Field(HL.Table)

SNSDialogContentCore.m_curContentNaviTargetCSIndex = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_contentNaviPreActionId = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_contentNaviNextActionId = HL.Field(HL.Number) << -1

SNSDialogContentCore.m_isFocusingContentCore = HL.Field(HL.Boolean) << false

SNSDialogContentCore._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    local ctrl = self:GetUICtrl()
    if not ctrl or IsNull(ctrl.view.inputGroup) then
        return
    end


    self.m_focusContentCoreActionId = InputManagerInst:CreateBindingByActionId("sns_content_core_focus_content_core", function()
        self:_ManuallyFocusContentCore()
        self:_TrySetOptionsNodeTarget()
        self:_TrySetEmojiCommentTarget()
    end, self:GetUICtrl().view.inputGroup.groupId)

    self.m_focusContentCoreDirActionId = InputManagerInst:CreateBindingByActionId("sns_content_core_focus_content_core_dir", function()
        self:_ManuallyFocusContentCore()
        self:_TrySetOptionsNodeTarget()
        self:_TrySetEmojiCommentTarget()
    end, self:GetUICtrl().view.inputGroup.groupId)

    self.m_stopFocusContentCoreActionId = InputManagerInst:CreateBindingByActionId("common_back", function()
        self:_ManuallyStopFocusContentCore()
    end, self.view.inputGroup.groupId)

    self.m_stopFocusContentCoreDirActionId = InputManagerInst:CreateBindingByActionId("sns_content_core_stop_focus_content_core_dir", function()
        self:_ManuallyStopFocusContentCore()
    end, self.view.inputGroup.groupId)

    InputManagerInst:ToggleBinding(self.m_focusContentCoreActionId, false)
    InputManagerInst:ToggleBinding(self.m_focusContentCoreDirActionId, false)
    InputManagerInst:ToggleBinding(self.m_stopFocusContentCoreActionId, false)
    InputManagerInst:ToggleBinding(self.m_stopFocusContentCoreDirActionId, false)


    self.view.dialogScrollRect.controllerScrollEnabled = true

    self.m_focusContentActionId = InputManagerInst:CreateBindingByActionId("sns_content_core_focus_content", function()
        self:_ManuallyFocusContent()
    end, self.view.inputGroup.groupId)

    self.m_stopFocusContentActionId = InputManagerInst:CreateBindingByActionId("common_back", function()
        self:_ManuallyStopFocusContent()
    end, self.view.dialogContentInputBindingGroupMonoTarget.groupId)

    InputManagerInst:ToggleBinding(self.m_focusContentActionId, false)
    InputManagerInst:ToggleBinding(self.m_stopFocusContentActionId, false)










    InputManagerInst:ToggleBinding(self.m_contentNaviPreActionId, false)
    InputManagerInst:ToggleBinding(self.m_contentNaviNextActionId, false)

    self.view.dialogContent.onDefaultNaviFailed:AddListener(function(dir)
        self:_OnContentTargetDefaultNaviFailed(dir)
    end)

    self.view.dialogContent.getDefaultSelectableFunc = function()
        local naviTarget = self:_GetShowingCellNaviTarget()
        return naviTarget
    end
end

SNSDialogContentCore._ManuallyFocusContentCore = HL.Method() << function(self)
    self.m_isFocusingContentCore = true
    self:_TryToggleContentCoreFocus(true, self.view.inputGroup.groupId)
    InputManagerInst:ToggleBinding(self.m_stopFocusContentCoreActionId, true)
    InputManagerInst:ToggleBinding(self.m_stopFocusContentCoreDirActionId, true)
    local naviTarget = self:_GetShowingCellNaviTarget()
    InputManagerInst:ToggleBinding(self.m_focusContentActionId, self.m_isFocusingContentCore and not self.m_isDialogInProcess and
            naviTarget ~= nil)
end

SNSDialogContentCore._ManuallyStopFocusContentCore = HL.Method() << function(self)
    self.m_isFocusingContentCore = false
    self:_TryToggleContentCoreFocus(false, self.view.inputGroup.groupId)
    InputManagerInst:ToggleBinding(self.m_stopFocusContentCoreActionId, false)
    InputManagerInst:ToggleBinding(self.m_stopFocusContentCoreDirActionId, false)
    InputManagerInst:ToggleBinding(self.m_stopFocusContentActionId, false)
    InputManagerInst:ToggleBinding(self.m_focusContentActionId, false)

    InputManagerInst:ToggleBinding(self.m_focusContentCoreActionId, true)
    InputManagerInst:ToggleBinding(self.m_focusContentCoreDirActionId, true)


    self:GetUICtrl():ReturnToFocusCell()
end

SNSDialogContentCore._ManuallyFocusContent = HL.Method() << function(self)
    self:_TryToggleContentCoreFocus(true, self.view.dialogContentInputBindingGroupMonoTarget.groupId)

    self.view.dialogContent:ManuallyFocus()

    InputManagerInst:ToggleBinding(self.m_stopFocusContentActionId, true)
    InputManagerInst:ToggleBinding(self.m_contentNaviPreActionId, true)
    InputManagerInst:ToggleBinding(self.m_contentNaviNextActionId, true)


    self.view.dialogScrollRect.controllerScrollEnabled = false
end

SNSDialogContentCore._ManuallyStopFocusContent = HL.Method() << function(self)
    self:_TryToggleContentCoreFocus(false, self.view.dialogContentInputBindingGroupMonoTarget.groupId)
    self.view.dialogContent:ManuallyStopFocus()

    InputManagerInst:ToggleBinding(self.m_stopFocusContentActionId, false)
    InputManagerInst:ToggleBinding(self.m_contentNaviPreActionId, false)
    InputManagerInst:ToggleBinding(self.m_contentNaviNextActionId, false)
    self:_TrySetOptionsNodeTarget()


    self.view.dialogScrollRect.controllerScrollEnabled = true
end

SNSDialogContentCore._TryToggleContentCoreFocus = HL.Method(HL.Boolean, HL.Opt(HL.Number)) << function(self, isOn,
                                                                                                       bindGroupId)
    if isOn then
        Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
            panelId = self:GetUICtrl().panelId,
            isGroup = true,
            id = bindGroupId,
            rectTransform = self.view.controllerFocusRect,
            noHighlight = self:_IsSidePanel(),
            hintPlaceholder = self:GetUICtrl().view.controllerHintPlaceholder,
        })
    else
        Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, bindGroupId)
    end
end

SNSDialogContentCore._TrySetOptionsNodeTarget = HL.Method() << function(self)
    if self.view.textOptions.gameObject.activeInHierarchy then
        local cell = self.m_textOptionCellCache:Get(1)
        self:SetNaviTarget(cell.button)
    end

    if self.view.stickerOptions.gameObject.activeInHierarchy then
        local cell = self.m_stickerOptionCellCache:Get(1)
        self:SetNaviTarget(cell.button)
    end
end

SNSDialogContentCore._TrySetEmojiCommentTarget = HL.Method() << function(self)
    if self.m_isDialogInProcess then
        return
    end


    if #self.m_dialogProcessAction == 0 then
        return
    end

    local curLastAction = self.m_dialogProcessAction[#self.m_dialogProcessAction]
    if curLastAction.type ~= DialogProcessActionType.ShowOption then
        return
    end

    local dialogId = curLastAction.dialogId
    local contentLinkNodeCSIndex = curLastAction.nodeIndex

    local dialogInfo = sns.dialogInfoDic:get_Item(dialogId)
    local contentLink = dialogInfo.contentLink

    local node = contentLink[contentLinkNodeCSIndex]
    local contentCfg = Tables.sNSDialogTable[dialogId].dialogContentData[node.contentId]
    local optionType = contentCfg.optionType
    if optionType ~= SNSDialogOptionType.EmojiComment then
        return
    end

    self:_ManuallyFocusContent()
end

SNSDialogContentCore._UpdateContentNodeFocusableState = HL.Method() << function(self)

    InputManagerInst:ToggleBinding(self.m_focusContentCoreActionId, not self.m_isFocusingContentCore)
    InputManagerInst:ToggleBinding(self.m_focusContentCoreDirActionId, not self.m_isFocusingContentCore)
    local naviTarget = self:_GetShowingCellNaviTarget()
    InputManagerInst:ToggleBinding(self.m_focusContentActionId, self.m_isFocusingContentCore and not self.m_isDialogInProcess and
            naviTarget ~= nil)
end

SNSDialogContentCore._GetShowingCellNaviTarget = HL.Method().Return(HL.Any, HL.Number) << function(self)
    local paddingBottom = self.view.dialogScrollList:GetPadding().bottom
    local range = self.view.dialogScrollList:GetShowRange(-paddingBottom)
    for i = range.y, range.x, -1 do

        local cell = self.m_getDialogContentCellFunc(LuaIndex(i))
        local naviTarget = cell and cell:GetNaviTarget()
        if naviTarget ~= nil then
            return naviTarget, i
        end
    end

    return nil, -1
end

SNSDialogContentCore._UpdateNaviTargetIndexes = HL.Method() << function(self)
    local paddingBottom = self.view.dialogScrollList:GetPadding().bottom
    local range = self.view.dialogScrollList:GetShowRange(-paddingBottom)
    for i = range.y, range.x, -1 do

        local cell = self.m_getDialogContentCellFunc(LuaIndex(i))
        local naviTarget = cell and cell:GetNaviTarget()
        if naviTarget ~= nil and lume.find(self.m_contentNaviTargetCSIndexes, i) == nil then
            table.insert(self.m_contentNaviTargetCSIndexes, i)
        end
    end

    for luaIndex, info in ipairs(self.m_dialogContentInfo) do
        local dialogId = info.dialogId
        local contentId = info.contentId
        local dialogCfg = Tables.sNSDialogTable[dialogId]
        local contentCfg = dialogCfg.dialogContentData[contentId]
        local csIndex = CSIndex(luaIndex)
        if ContentType2CanNavi[contentCfg.contentType] and
                lume.find(self.m_contentNaviTargetCSIndexes, csIndex) == nil then
            table.insert(self.m_contentNaviTargetCSIndexes, csIndex)
        end
    end

    table.sort(self.m_contentNaviTargetCSIndexes)
end

SNSDialogContentCore._ContentNaviTarget = HL.Method(HL.Number) << function(self, offset)
    local nextContentCSIndex = self.m_curContentNaviTargetCSIndex + offset
    if nextContentCSIndex > #self.m_contentNaviTargetCSIndexes then
        return
    end

    if nextContentCSIndex < 1 then
        return
    end


    local contentIndex = self.m_contentNaviTargetCSIndexes[nextContentCSIndex]
    self.view.dialogScrollList:ScrollToIndex(contentIndex, true)
    local cell = self.m_getDialogContentCellFunc(LuaIndex(contentIndex))
    local naviTarget = cell:GetNaviTarget()

    self:SetNaviTarget(naviTarget)
    naviTarget:OnInteractableChanged()


    self.m_curContentNaviTargetCSIndex = nextContentCSIndex
end

SNSDialogContentCore.OnClickSidePanelFinishBtn = HL.Method() << function(self)
    if DeviceInfo.usingController and self:_IsSidePanel() then
        self:_ManuallyStopFocusContentCore()
    end
end

SNSDialogContentCore._OnContentTargetDefaultNaviFailed = HL.Method(CS.UnityEngine.UI.NaviDirection) << function(self, dir)
    if dir == NaviDirection.Left or dir == NaviDirection.Right then
        return
    end

    local startCSIndex, endCSIndex, offset
    local paddingBottom = self.view.dialogScrollList:GetPadding().bottom
    local range = self.view.dialogScrollList:GetShowRange(-paddingBottom)
    if dir == NaviDirection.Up then
        startCSIndex = range.x - 1
        endCSIndex = 0
        offset = -1
    elseif dir == NaviDirection.Down then
        startCSIndex = range.y + 1
        endCSIndex = #self.m_dialogContentInfo - 1
        offset = 1
    end

    for i = startCSIndex, endCSIndex, offset do
        if lume.find(self.m_contentNaviTargetCSIndexes, i) ~= nil then
            self.view.dialogScrollList:ScrollToIndex(i, true)

            local cell = self.m_getDialogContentCellFunc(LuaIndex(i))
            local naviTarget = cell and cell:GetNaviTarget()
            if naviTarget ~= nil then
                self:SetNaviTarget(naviTarget)
                break
            end
        end
    end
end

SNSDialogContentCore.ToggleContentCoreFocusable = HL.Method(HL.Boolean) << function(self, isOn)
    InputManagerInst:ToggleBinding(self.m_focusContentCoreActionId, isOn)
    InputManagerInst:ToggleBinding(self.m_focusContentCoreDirActionId, isOn)
    InputManagerInst:ToggleBinding(self.m_stopFocusContentCoreActionId, isOn)
    InputManagerInst:ToggleBinding(self.m_stopFocusContentCoreDirActionId, isOn)
end





SNSDialogContentCore._EventLogStartDialog = HL.Method() << function(self)
    local succ, cfg = Tables.sNSDialogTable:TryGetValue(self.m_curDialogId)
    if succ then
        EventLogManagerInst:GameEvent_SNSDialogStart(self.m_curDialogId, self:_EventLogGetDialogType())
    end
end

SNSDialogContentCore._EvenLogSetOption = HL.Method(HL.String) << function(self, optionId)
    local succ, cfg = Tables.sNSDialogTable:TryGetValue(self.m_curDialogId)
    if succ then
        EventLogManagerInst:GameEvent_SNSDialogSetOption(self.m_curDialogId, self:_EventLogGetDialogType(), self.m_startTs, optionId)
    end
end

SNSDialogContentCore._EventLogEndDialog = HL.Method() << function(self)
    local succ, cfg = Tables.sNSDialogTable:TryGetValue(self.m_curDialogId)
    if succ then
        local dialogTime = DateTimeUtils.GetCurrentTimestampBySeconds() - self.m_startTs
        EventLogManagerInst:GameEvent_SNSDialogEnd(self.m_curDialogId, self:_EventLogGetDialogType(), self.m_startTs, dialogTime)
    end
end

SNSDialogContentCore._EventLogGetDialogType = HL.Method().Return(HL.Number) << function(self)
    local succ, cfg = Tables.sNSDialogTable:TryGetValue(self.m_curDialogId)
    if not succ then
        return 1
    end

    if not string.isEmpty(cfg.relatedMissionId) then
        return 3
    elseif not string.isEmpty(cfg.topicId) then
        return 2
    else
        return 1
    end
end



HL.Commit(SNSDialogContentCore)
return SNSDialogContentCore














