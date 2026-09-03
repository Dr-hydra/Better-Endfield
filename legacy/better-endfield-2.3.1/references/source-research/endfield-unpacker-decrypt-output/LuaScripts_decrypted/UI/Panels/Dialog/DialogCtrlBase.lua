local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
DialogCtrlBase = HL.Class('DialogCtrlBase', uiCtrl.UICtrl)

DialogCtrlBase.m_optionCells = HL.Field(HL.Forward("UIListCache"))

DialogCtrlBase.m_optionCount = HL.Field(HL.Number) << 0

DialogCtrlBase.m_curEntryLinks = HL.Field(HL.Userdata) << nil

DialogCtrlBase.m_enableGlossaryPopUp = HL.Field(HL.Boolean) << true





DialogCtrlBase.s_messages = HL.StaticField(HL.Table) << {}


DialogCtrlBase.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_optionCells = UIUtils.genCellCache(self.view.panelOptionCell)


    self.view.buttonBack.onClick:RemoveAllListeners()
    self.view.buttonBack.onClick:AddListener(function()
        self:OnBtnBackClick()

    end)


    self.view.buttonNext.onClick:RemoveAllListeners()
    self.view.buttonNext.onClick:AddListener(function()
        self:OnBtnNextClick()
    end)


    self.view.buttonSkip.onClick:RemoveAllListeners()
    self.view.buttonSkip.onClick:AddListener(function()
        self:OnBtnSkipClick()
    end)


    self.view.buttonAuto.onClick:RemoveAllListeners()
    self.view.buttonAuto.onClick:AddListener(function()
        self:OnBtnAutoClick()
    end)


    self.view.buttonLog.onClick:RemoveAllListeners()
    self.view.buttonLog.onClick:AddListener(function()
        self:OnBtnLogClick()
    end)


    self.view.buttonStop.onClick:RemoveAllListeners()
    self.view.buttonStop.onClick:AddListener(function()
        self:OnBtnStopClick()
    end)

    self.view.textTalk.uiText.onClickLink:RemoveAllListeners()
    self.view.textTalk.uiText.onClickLink:AddListener(function(linkId)
        self:OnLinkClick(linkId)
    end)


    self:_InitDialogController()
    self:OnCreated(arg)
end

DialogCtrlBase.OnCreated = HL.Virtual(HL.Any) << function(self, arg)
end



DialogCtrlBase._InitDialogController = HL.Method() << function(self)
    self:_SwitchControllerAutoPlayHint()
    self:_RefreshGlossaryNoteKeyHint()
    self:BindInputPlayerAction("dialog_open_note", function() self:_OpenGlossaryPopUp() end)
end

DialogCtrlBase._EnableDialogControllerOption = HL.Method() << function(self)
    if not DeviceInfo.usingController or self.m_optionCount <= 0 then
        return
    end

    local optionCount = self.m_optionCells:GetCount()
    if optionCount == 0 then
        return
    end

    self.view.optionNaviGroup:ManuallyFocus()
end

DialogCtrlBase._DisableDialogControllerOption = HL.Method() << function(self)
    if not DeviceInfo.usingController or self.m_optionCount > 0 then
        return
    end

    self.view.optionNaviGroup:ManuallyStopFocus()
end

DialogCtrlBase.SetTrunkOption = HL.Virtual(HL.Userdata) << function(self, optionData)
    if self:IsHide() then
        self:Show()
        self.view.textTalkCenterNode.gameObject:SetActive(false)
        self.view.radioNode.gameObject:SetActive(false)
        self.view.bottomLayout.gameObject:SetActive(false)
    end

    local count = optionData.Count
    self.m_optionCount = count
    if count == 0 then
        self:_DisableDialogControllerOption()
        self.m_optionCells:PlayAllOut()
    else
        self.view.optionList.gameObject:SetActive(true)
        self.m_optionCells:ClearAllTween(false)
        self.m_optionCells:Refresh(count, function(cell, luaIndex)
            local option = optionData[CSIndex(luaIndex)]
            local data = {
                optionId = option.optionId,
                index = luaIndex,
                text = option.optionText or "",
                iconType = option.iconType,
                icon = option.optionIcon,
                color = option.useExOptionColor and  option.optionIconColor or nil,
                setGreyed = option.setGreyed,
            }
            cell:InitDialogOptionCell(data, function()
                self:OnOptionClick(luaIndex, option)
            end)
            cell.view.animationWrapper:PlayInAnimation()
        end)

        if self:IsShow() then
             self:_EnableDialogControllerOption()
        end
    end

    self:_RefreshCanSkip()
    local showWait = count <= 0
    self:_TrySetWaitNode(showWait)
end

DialogCtrlBase._RefreshCanSkip = HL.Virtual() << function(self)
end

DialogCtrlBase.OnDialogTextStopped = HL.Virtual() << function(self)
end

DialogCtrlBase.OnOptionClick = HL.Virtual(HL.Number, HL.Any) << function(self, index, data)
end


DialogCtrlBase._TrySetWaitNode = HL.Virtual(HL.Boolean) << function(self, active)
end

DialogCtrlBase._SwitchControllerAutoPlayHint = HL.Method() << function(self)
    local onAutoPlay = self:_GetCurrentAutoMode()

    if onAutoPlay then
        InputManagerInst:SetBindingText(self.view.buttonAuto.onClick.bindingId, Language["ui_nar_dialogue_auto"])
    else
        InputManagerInst:SetBindingText(self.view.buttonAuto.onClick.bindingId, Language["key_hint_dialog_auto_play"])
    end

    self.view.controllerHint.skipHint.gameObject:SetActiveIfNecessary(not onAutoPlay)
    self.view.controllerHint.skipHintLoop.gameObject:SetActiveIfNecessary(onAutoPlay)
end

DialogCtrlBase._RefreshGlossaryNoteKeyHint = HL.Method() << function(self)
    local enable = true

    if self.m_phase == nil or self.m_curEntryLinks == nil or self.m_curEntryLinks.Count == 0 then
        enable = false
    end

    if not self.m_enableGlossaryPopUp then
        enable = false
    end

    self.view.noteKeyHintNode.gameObject:SetActive(enable)
end

DialogCtrlBase._GetCurrentAutoMode = HL.Virtual().Return(HL.Boolean) << function(self)
    return GameWorld.dialogManager.autoMode
end




DialogCtrlBase.OnBtnBackClick = HL.Virtual() << function(self)
end

DialogCtrlBase.OnBtnNextClick = HL.Virtual() << function(self)
end

DialogCtrlBase.OnBtnSkipClick = HL.Virtual() << function(self)
end

DialogCtrlBase.OnBtnAutoClick = HL.Virtual() << function(self)
end

DialogCtrlBase.OnBtnLogClick = HL.Virtual() << function(self)
    self:Notify(MessageConst.OPEN_DIALOG_RECORD)
end

DialogCtrlBase.OnBtnStopClick = HL.Virtual() << function(self)
end



DialogCtrlBase.OnLinkClick = HL.Virtual(HL.String) << function(self, linkId)
    if UIUtils.resolveLinkTypeFromId(linkId) ~= UIConst.UI_TEXT_LINK_TYPE.Narrative then
        return
    end
    self:_OpenGlossaryPopUp()
end

DialogCtrlBase._OpenGlossaryPopUp = HL.Method() << function(self)
    if self.m_phase == nil or self.m_curEntryLinks == nil or self.m_curEntryLinks.Count == 0 then
        return
    end

    if not self.m_enableGlossaryPopUp then
        return
    end

    AudioAdapter.PostEvent("Au_UI_Button_Common")
    self.m_phase:CreatePhasePanelItem(PanelId.DialogGlossaryPopUp, { self.m_curEntryLinks })
end

DialogCtrlBase.SetCurEntryLinks = HL.Method(HL.Userdata) << function(self, links)
    self.m_curEntryLinks = links
    self:_RefreshGlossaryNoteKeyHint()
end

DialogCtrlBase.SetGlossaryPopUpEnable = HL.Method(HL.Boolean) << function(self, isEnable)
    self.m_enableGlossaryPopUp = isEnable
    self:_RefreshGlossaryNoteKeyHint()
end




DialogCtrlBase._RefreshAutoMode = HL.Virtual(HL.Boolean) << function(self, autoMode)
    self.view.textAuto.gameObject:SetActive(autoMode)
    self:_SwitchControllerAutoPlayHint()
end

DialogCtrlBase.OnRefreshAutoMode = HL.Method(HL.Table) << function (self, arg)
    self:_RefreshAutoMode(unpack(arg))
end


DialogCtrlBase.ShowDevWaterMark = HL.Method() << function(self)
    if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
        self.view.devWaterMarkGroup.gameObject:SetActive(true)
        self.view.devWaterMark_1.text = Language.LUA_DIALOG_DEV_WATER_MARK
        self.view.devWaterMark_2.text = Language.LUA_DIALOG_DEV_WATER_MARK
    end
end

DialogCtrlBase.GetCurDialogId = HL.Virtual().Return(HL.String) << function(self)
    return ""
end

DialogCtrlBase.CheckTextPlaying = HL.Method().Return(HL.Boolean) << function(self)
    if self.view.textTalk.gameObject.activeInHierarchy and self.view.textTalk.playing then
        return true
    end

    return false
end

DialogCtrlBase.OnShow = HL.Override() << function(self)
    self:OnDialogShow()
    self.view.debugNode.gameObject:SetActive(false)
    if NarrativeUtils.ShouldShowNarrativeDebugNode() then
        self.view.debugNode.gameObject:SetActive(true)
        local desc = ""
        if GameWorld.dialogManager.dialogTree then
            desc = "\nT2/T3"
            if GameWorld.dialogManager.dialogTree.dialogTreeData.qualityLevel == CS.Beyond.Gameplay.DialogEnums.DialogQualityLevel.High then
                desc = "\nTO/T1"
            end
        end
        self.view.textDialogId.text = self:GetCurDialogId() .. desc
    end
end

DialogCtrlBase.OnDialogShow = HL.Virtual() << function(self)
    if self.m_optionCount > 0 then
         self:_EnableDialogControllerOption()
    end
end




DialogCtrlBase.OnClose = HL.Override() << function(self)
    self.view.bgSprite:DOKill()
    self.view.bgBlack:DOKill()
    self.view.bottomLayout:DOKill()
    self.view.textTalkCenterNode:DOKill()

    self.view.buttonBack.onClick:RemoveAllListeners()
    self.view.buttonNext.onClick:RemoveAllListeners()
    self.view.buttonSkip.onClick:RemoveAllListeners()
    self.view.buttonAuto.onClick:RemoveAllListeners()
end




HL.Commit(DialogCtrlBase)
