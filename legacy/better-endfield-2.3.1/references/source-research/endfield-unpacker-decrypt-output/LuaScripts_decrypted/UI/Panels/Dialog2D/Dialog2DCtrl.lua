
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Dialog2D
local PHASE_ID = PhaseId.Dialog2D
Dialog2DCtrl = HL.Class('Dialog2DCtrl', uiCtrl.UICtrl)

Dialog2DCtrl.m_trunkNodeData = HL.Field(CS.Beyond.Gameplay.DTTrunkNodeData)

Dialog2DCtrl.m_optionCells = HL.Field(HL.Forward("UIListCache"))

Dialog2DCtrl.m_optionCount = HL.Field(HL.Number) << 0

Dialog2DCtrl.m_textContent = HL.Field(HL.String) << ''

Dialog2DCtrl.m_typeWriterCor = HL.Field(HL.Thread)

Dialog2DCtrl.m_autoNext = HL.Field(HL.Boolean) << false





Dialog2DCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_PLAY_DIALOG_TRUNK] = 'OnPlayDialogTrunk',
    [MessageConst.ON_SHOW_DIALOG_OPTION] = 'OnShowDialogOption',
    [MessageConst.DIALOG_OPEN_UI] = 'OpenUI',
    [MessageConst.ON_PLAY_DIALOG_2D_CONTENT] = 'OnPlayDialog2DContent',
    [MessageConst.ON_EXIT_DIALOG] = 'OnExitDialog',
}

Dialog2DCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_optionCells = UIUtils.genCellCache(self.view.panelOptionCell)
    self.m_textContent = ''
end



Dialog2DCtrl.OnDialogStart2D = HL.StaticMethod() << function()
    local inWaitingQueue = PhaseManager:IsInWaitingQueue(PHASE_ID)
    if inWaitingQueue then
        return
    end

    local isOpen, _ = PhaseManager:IsOpen(PHASE_ID)
    if not isOpen then
        PhaseManager:OpenPhaseFast(PHASE_ID)
    end
end

Dialog2DCtrl.OnExitDialog = HL.Method(HL.Opt(HL.Table)) << function(self)
    PhaseManager:ExitPhaseFast(PHASE_ID)
end

Dialog2DCtrl.OnPlayDialogTrunk = HL.Method(HL.Table) << function(self, data)
    local trunkNodeData, fastMode, npcId, npcGroupId = unpack(data)
    self:SetTrunk(trunkNodeData, fastMode, npcId, npcGroupId)
end

Dialog2DCtrl.OnShowDialogOption = HL.Method(HL.Table) << function(self, data)
    local options = unpack(data)
    self:SetTrunkOption(options)
end

Dialog2DCtrl.OnPlayDialog2DContent = HL.Method(HL.Table) << function(self, data)
    local trunkId, completeCallback = unpack(data)
    self:Set2DContentText(trunkId, completeCallback)
end

Dialog2DCtrl.OpenUI = HL.Method(HL.Table) << function(self, arg)
    local panelIdStr, paramStr, _ = unpack(arg)
    local panelId = PanelId[panelIdStr]
    local phaseId = PhaseId[panelIdStr]
    local param = not string.isEmpty(paramStr) and Utils.stringJsonToTable(paramStr) or {}
    param.fromDialog = true
    if param.blockWhitePhaseName ~= nil then
        local exceptIds = {}
        for _, whitePhaseName in pairs(param.blockWhitePhaseName) do
            table.insert(exceptIds, whitePhaseName)
        end
        UIManager:ToggleBlockObtainWaysJump("IN_CINEMATIC", true, exceptIds)
    end

    if not panelId or not phaseId then
        logger.error(("Dialog2D OpenUI Failed !! PanelId Not Found !! PanelIdStr is %s, Param is %s"):format(panelIdStr, paramStr))
        GameWorld.dialogManager:Next()
        return
    end

    if phaseId == PhaseId.ReadingPopUp then
        local closeCallback = function()
            GameWorld.dialogManager:Next(0)
        end
        param = {param.id, closeCallback}
    end

    local res = PhaseManager:OpenPhase(phaseId, param)
    if not res then
        logger.error("Dialog2D OpenUI fail!!!", panelIdStr)
        GameWorld.dialogManager:Next()
    end

    GameWorld.dialogManager:TryRecoverVoiceManager()
end

Dialog2DCtrl.CloseUI = HL.Method(HL.Table) << function(self, arg)

    local panelId, phaseId, nextIndex, notFastMode = unpack(arg)

    GameWorld.dialogManager:TryPauseVoiceManager()
    if nextIndex then
        GameWorld.dialogManager:Next(nextIndex)
    end
end



Dialog2DCtrl.OnClose = HL.Override() << function(self)
    if self.m_typeWriterCor ~= nil then
        self.m_typeWriterCor = self:_ClearCoroutine(self.m_typeWriterCor)
    end
    Dialog2DCtrl.Super.OnClose(self)
end

Dialog2DCtrl.SetTrunk = HL.Method(HL.Userdata, HL.Opt(HL.Boolean)) << function(self, trunkNodeData, fastMode)
    self.m_trunkNodeData = trunkNodeData
    local trunkId = trunkNodeData.overrideTrunkId
    if string.isEmpty(trunkId) then
        trunkId = trunkNodeData.trunkId
    end

    local name = ""
    local dialogText = ""
    local res, trunkTbData = Tables.dialogTextTable:TryGetValue(trunkId)
    if res then
        if not string.isEmpty(trunkTbData.actorName) then
            name = trunkTbData.actorName
        end
        dialogText = trunkTbData.dialogText
    end

    local text = UIUtils.resolveTextCinematic(dialogText)
    local singleTrunk = string.isEmpty(name)
    self.view.bottomLayout:DOKill()
    self.view.bottomLayout.alpha = 1
    self.view.textTalkCenterNode.alpha = 1

    self.view.textTalkCenterNode.gameObject:SetActive(singleTrunk)
    self.view.bottomLayout.gameObject:SetActive(not singleTrunk)

    if singleTrunk then
        self.view.textTalkCenter:SetText(text)
        if fastMode then
            self.view.textTalkCenter:SeekToEnd()
        else
            self.view.textTalkCenter:Play()
        end
    else
        local richName = UIUtils.resolveTextCinematic(name)
        richName = UIUtils.removePattern(richName, UIConst.NARRATIVE_ANONYMITY_PATTERN)
        self.view.textName:SetAndResolveTextStyle(richName)
        self.view.textName.gameObject:SetActive(true)

        self.view.textTalk:SetText(text)
        if fastMode then
            self.view.textTalk:SeekToEnd()
        else
            self.view.textTalk:Play()
        end
    end
end

Dialog2DCtrl.SetTrunkOption = HL.Method(HL.Userdata) << function(self, optionData)
    if self:IsHide() then
        self:Show()
        self.view.textTalkCenterNode.gameObject:SetActive(false)
        self.view.bottomLayout.gameObject:SetActive(false)
    end

    local count = optionData.Count
    self.m_optionCount = count
    if count == 0 then
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
    end
end

Dialog2DCtrl.OnOptionClick = HL.Method(HL.Number, HL.Any) << function(self, index, data)
    GameWorld.dialogManager:SelectIndex(CSIndex(index))
end

Dialog2DCtrl.Set2DContentText = HL.Method(HL.String, HL.Boolean) << function(self, trunkId, autoNext)
    self.m_autoNext = autoNext


    local dialogText = ""
    local res, trunkTbData = Tables.dialogTextTable:TryGetValue(trunkId)
    if res then
        dialogText = trunkTbData.dialogText
    end


    local text = UIUtils.resolveTextCinematic(dialogText)


    if not string.isEmpty(text) then
        self.m_textContent = self.m_textContent .. text .. "\n\n"
    end


    self.view.content:SetText(self.m_textContent, false)
    self.view.content:Play()


    if self.m_typeWriterCor == nil then
        self.view.scrollView.disableScroll = true
        self.view.scrollView.controllerScrollEnabled = false
        if self.view.scrollViewImage then
            self.view.scrollViewImage.raycastTarget = false
        end

        self.m_typeWriterCor = self:_StartCoroutine(function()
            while self.m_isClosed ~= true do

                local verticalNormalizedPosition = self.view.scrollView.verticalNormalizedPosition
                if verticalNormalizedPosition > 0.001 then

                    verticalNormalizedPosition = verticalNormalizedPosition - verticalNormalizedPosition * Time.deltaTime * 10
                    if verticalNormalizedPosition < 0.001 then
                        verticalNormalizedPosition = 0
                    end
                    self.view.scrollView.verticalNormalizedPosition = verticalNormalizedPosition
                end


                if self.view.content.playing == false then

                    self:OnTextPlayComplete()
                    break
                end
                coroutine.step()
            end
        end)
    end
end

Dialog2DCtrl.OnTextPlayComplete = HL.Method() << function(self)
    self.view.scrollView.disableScroll = false
    self.view.scrollView.controllerScrollEnabled = true
    if self.view.scrollViewImage then
        self.view.scrollViewImage.raycastTarget = true
    end

    if self.m_typeWriterCor ~= nil then
        self.m_typeWriterCor = self:_ClearCoroutine(self.m_typeWriterCor)
    end

    if self.m_autoNext then
        GameWorld.dialogManager:Next()
    end
end

HL.Commit(Dialog2DCtrl)
