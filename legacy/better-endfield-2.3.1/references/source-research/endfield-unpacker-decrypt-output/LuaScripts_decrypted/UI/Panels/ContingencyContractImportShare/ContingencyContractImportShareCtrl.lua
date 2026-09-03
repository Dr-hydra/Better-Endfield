local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ContingencyContractImportShare

ContingencyContractImportShareCtrl = HL.Class('ContingencyContractImportShareCtrl', uiCtrl.UICtrl)






ContingencyContractImportShareCtrl.s_messages = HL.StaticField(HL.Table) << {

}



ContingencyContractImportShareCtrl.m_gameId = HL.Field(HL.String) << ""

ContingencyContractImportShareCtrl.m_shareTagInfos = HL.Field(HL.Table)

ContingencyContractImportShareCtrl.m_importTagInfos = HL.Field(HL.Table)

ContingencyContractImportShareCtrl.m_importCallback = HL.Field(HL.Function)




ContingencyContractImportShareCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData(arg)
    self:_RefreshAllUI()
    self:_TryRecoverInputState(arg and arg.recoverState)
    if arg then
        arg.recoverState = nil
    end
    self:SetNaviTarget(self.view.genCodeNode.naviDeco)
end



ContingencyContractImportShareCtrl._InitData = HL.Method(HL.Table) << function(self, arg)
    self.m_importCallback = arg.importCallback
    self.m_gameId = arg.gameId
    local joinedTagIds = arg.joinedTagIds
    local _, shareCode = ContingencyContractUtils.GenShareCode(self.m_gameId, joinedTagIds)
    local shareText = ContingencyContractUtils.WrapShareText(shareCode, arg.totalScore, ContingencyContractUtils.GenShareCodeSourceType.Normal)
    self.m_shareTagInfos = {
        joinedTagIds = joinedTagIds,
        shareCode = shareCode,
        shareText = shareText,
        totalScore = arg.totalScore,
    }

    self.m_importTagInfos = {
        shareText = "",
        shareTagIds = {},
        totalScore = 0,
        isErrorCode = true,
    }
end



ContingencyContractImportShareCtrl._InitUI = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.genCodeNode.copyBtn.onClick:AddListener(function()
        local shareText = self.m_shareTagInfos.shareText
        if string.isEmpty(shareText) then
            logger.error("危机合约词条分享界面，shareCode为空！")
        else
            Unity.GUIUtility.systemCopyBuffer = shareText
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SHARE_COPY_TIP)
        end

        EventLogManagerInst:GameEvent_ContingencyContractGenShareCode(
            self.m_shareTagInfos.shareCode
        )
    end)

    self.view.analyzeCodeNode.inputClearBtn.onClick:AddListener(function()
        self.view.analyzeCodeNode.inputField.text = ""
        self:_RefreshAnalyzeCodeNode()
        self.view.analyzeCodeNode.inputField:ActivateInputField()
    end)

    self.view.analyzeCodeNode.pasteBtn.onClick:AddListener(function()
        local shareText = Unity.GUIUtility.systemCopyBuffer
        shareText = string.gsub(shareText, "\n", "")
        if string.isEmpty(shareText) then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SHARE_NEED_COPY_TIP)
        else
            local inputField = self.view.analyzeCodeNode.inputField
            inputField.text = shareText
            inputField:ForceLabelUpdate()
            inputField.textComponent:ForceMeshUpdate()
            local textRect = inputField.textComponent.rectTransform
            local viewport = inputField.textViewport
            local overflowWidth = inputField.textComponent.preferredWidth - viewport.rect.width
            textRect.anchoredPosition = CS.UnityEngine.Vector2(overflowWidth > 0 and -overflowWidth or 0, textRect.anchoredPosition.y)
        end
    end)

    self.view.analyzeCodeNode.importBtn.onClick:AddListener(function()
        if string.isEmpty(self.m_importTagInfos.shareText) then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SHARE_NEED_INPUT_TIP)
            return
        end
        self:_AnalyzeShareText()
        if self.m_importTagInfos.isErrorCode then
            self.view.analyzeCodeNode.inputField.text = ""
            self:_RefreshAnalyzeCodeNode()
            return
        end

        local titleText = string.format(Language.LUA_CONTINGENCY_CONTRACT_SHARE_CONFIRM_POP_UP_TITLE, self.m_importTagInfos.totalScore)
        Notify(MessageConst.SHOW_POP_UP, {
            content = titleText,
            warningContent = Language.LUA_CONTINGENCY_CONTRACT_SHARE_CONFIRM_POP_UP_WARN_TITLE,
            onConfirm = function()
                if self.m_importCallback then
                    self.m_importCallback(self.m_importTagInfos.shareTagIds)
                end
                self:PlayAnimationOutAndClose()
                EventLogManagerInst:GameEvent_ContingencyContractImportShareCode(
                    self.m_importTagInfos.shareText
                )
            end
        })
    end)

    self.view.analyzeCodeNode.inputField.onValueChanged:AddListener(function()
        local inputField = self.view.analyzeCodeNode.inputField
        local shareText = inputField.text
        shareText = string.gsub(shareText, "\n", "")
        inputField:SetTextWithoutNotify(shareText)
        self.m_importTagInfos.shareText = shareText
        self:_RefreshAnalyzeCodeNode()
    end)


    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.genCodeNode.naviDeco.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self.view.stateController:SetState("NaviGenCodeNode")
        end
    end
    self.view.analyzeCodeNode.naviDeco.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self.view.stateController:SetState("NaviAnalyzeCodeNode")
        end
    end
end

ContingencyContractImportShareCtrl._RefreshAllUI = HL.Method() << function(self)
    local genCodeNode = self.view.genCodeNode
    genCodeNode.shareCodeTxt.text = self.m_shareTagInfos.shareCode
    genCodeNode.totalScoreTxt.text = self.m_shareTagInfos.totalScore
    self:_RefreshAnalyzeCodeNode()
end

ContingencyContractImportShareCtrl._RefreshAnalyzeCodeNode = HL.Method() << function(self)
    local analyzeCodeNode = self.view.analyzeCodeNode
    if string.isEmpty(analyzeCodeNode.inputField.text) then
        analyzeCodeNode.stateController:SetState("NoInput")
    else
        analyzeCodeNode.stateController:SetState("HasInput")
    end
end

ContingencyContractImportShareCtrl._TryRecoverInputState = HL.Method(HL.Any) << function(self, recoverState)
    if recoverState == nil or recoverState.inputShareText == nil then
        return
    end

    self.view.analyzeCodeNode.inputField.text = recoverState.inputShareText
end



ContingencyContractImportShareCtrl._AnalyzeShareText = HL.Method() << function(self)
    local info = self.m_importTagInfos
    local shareText = info.shareText
    if string.isEmpty(shareText) then
        info.isErrorCode = true
        info.shareTagIds = {}
        info.totalScore = 0
        return
    end

    local suc, tagIds, totalScore = ContingencyContractUtils.AnalyzeShareText(self.m_gameId, shareText)
    info.isErrorCode = not suc
    info.shareTagIds = tagIds
    info.totalScore = totalScore
end

ContingencyContractImportShareCtrl.GetRecoverStateArg = HL.Method().Return(HL.Table) << function(self)
    return {
        inputShareText = self.m_importTagInfos and self.m_importTagInfos.shareText or "",
    }
end


HL.Commit(ContingencyContractImportShareCtrl)
