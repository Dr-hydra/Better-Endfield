
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacSearchBlueprint

FacSearchBlueprintCtrl = HL.Class('FacSearchBlueprintCtrl', uiCtrl.UICtrl)



FacSearchBlueprintCtrl.m_typeCells = HL.Field(HL.Forward('UIListCache'))

FacSearchBlueprintCtrl.m_typeInfos = HL.Field(HL.Table)

FacSearchBlueprintCtrl.m_searchResults = HL.Field(HL.Table)

FacSearchBlueprintCtrl.m_selectedTypeIndex = HL.Field(HL.Number) << 0

FacSearchBlueprintCtrl.m_selectedIndex = HL.Field(HL.Number) << 0

FacSearchBlueprintCtrl.m_bpAbnormalIconHelper = HL.Field(HL.Table)

FacSearchBlueprintCtrl.m_bpCtrl = HL.Field(HL.Forward('FacBlueprintCtrl'))

FacSearchBlueprintCtrl.m_blueprintID = HL.Field(HL.Any) << 0

FacSearchBlueprintCtrl.m_gettingShareCode = HL.Field(HL.Boolean) << false

FacSearchBlueprintCtrl.m_friendSharing = HL.Field(HL.Boolean) << false

FacSearchBlueprintCtrl.m_friendRoleId = HL.Field(HL.Any)






FacSearchBlueprintCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_CHECK_SENSITIVE_SUCCESS] = '_OnCheckSensitiveSuccess',
    [MessageConst.FAC_ON_MODIFY_BLUEPRINT] = 'FacOnModifyBlueprint',
    [MessageConst.FAC_ON_REVIEW_BLUEPRINT] = 'RefreshShareState',
    [MessageConst.FAC_ON_SHARE_BLUEPRINT] = 'FacOnShareBlueprint',
}

local sourceTypeTable = {
    Mine = 1,
    Sys = 2,
    Gift = 3,
}


FacSearchBlueprintCtrl.OnCreate = HL.Override(HL.Any) << function(self, searchInfos)
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)


    local _, bpCtrl = UIManager:IsOpen(PanelId.FacBlueprint)
    self.m_bpCtrl = bpCtrl
    self.m_typeInfos = bpCtrl:GetTypeInfos()
    self.m_bpAbnormalIconHelper = bpCtrl:GetBPAbnormalIconHelper()

    self.m_typeCells = UIUtils.genCellCache(self.view.blueprintTypeCell)

    self.view.searchNode:InitSearchNode({
        searchFunc = function()
            self:_TrySearch()
        end,
    })

    self.view.bottomBtns.craftBtn.onClick:AddListener(function()
        self:_OnClickCraftBtn()
    end)
    self.view.bottomBtns.useBtn.onClick:AddListener(function()
        self:_OnClickUseBtn()
    end)

    local content = self.view.blueprintContent
    content.view.rightActions.editBtn.onClick:AddListener(function()
        self:_ShowPreview()
    end)
    content.view.rightActions.previewBtn.onClick:AddListener(function()
        self:_ShowPreview()
    end)

    if searchInfos then
        if searchInfos.setStateShare and searchInfos.friendRoleId then
            self.view.mainStateController:SetState("Share")
            self.m_friendSharing = true
            self.m_friendRoleId = searchInfos.friendRoleId
        else
            self.view.mainStateController:SetState("Normal")
        end
        if searchInfos.normalizedPosition ~= nil then
            self.m_normalizedPosition = searchInfos.normalizedPosition
        end
        local inputText = searchInfos.keyword
        if string.isEmpty(inputText) then
            inputText = searchInfos.inputText
        end
        if not string.isEmpty(inputText) then
            self.view.inputField.text = inputText
        end
        if searchInfos.keyword ~= nil then
            self.m_curKeyWord = searchInfos.keyword
            self:_RealSearch(true)
            self:_TryRecoverSelectedCell(searchInfos)
        else
            self.m_curKeyWord = ''
            self:_RealSearch(true)
        end
    else
        self.view.mainStateController:SetState("Normal")
        self.view.searchNode:Clear()
        self:_TrySearch(true)
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

FacSearchBlueprintCtrl.m_curKeyWord = HL.Field(HL.String) << ''

FacSearchBlueprintCtrl.GetRecoverStateArg = HL.Method().Return(HL.Table) << function(self)
    local searchInfos = {
        inputText = self.view.inputField.text,
        keyword = self.m_curKeyWord,
        normalizedPosition = self.view.scrollView.verticalNormalizedPosition,
    }
    if self.m_friendSharing and self.m_friendRoleId then
        searchInfos.setStateShare = true
        searchInfos.friendRoleId = self.m_friendRoleId
    end
    if self.m_selectedTypeIndex > 0 and self.m_selectedIndex > 0 then
        local searchGroup = self.m_searchResults and self.m_searchResults[self.m_selectedTypeIndex]
        local selectedInst = searchGroup and searchGroup.validInsts and searchGroup.validInsts[self.m_selectedIndex]
        if selectedInst then
            searchInfos.selectedBlueprintType = selectedInst.type:ToString()
            searchInfos.selectedBlueprintId = selectedInst.id
        end
    end
    return searchInfos
end

FacSearchBlueprintCtrl._TryRecoverSelectedCell = HL.Method(HL.Opt(HL.Any)).Return(HL.Boolean) << function(self, searchInfos)
    if not searchInfos or not self.m_searchResults or #self.m_searchResults == 0 then
        return false
    end
    local selectedBlueprintType = searchInfos.selectedBlueprintType
    local selectedBlueprintId = searchInfos.selectedBlueprintId
    local selectedBlueprintInst = searchInfos.csBPInst
    for typeIndex, searchGroup in ipairs(self.m_searchResults) do
        for index, inst in ipairs(searchGroup.validInsts) do
            if selectedBlueprintInst and inst.csInst == selectedBlueprintInst then
                self:_OnClickCell(typeIndex, index)
                return true
            end
            if selectedBlueprintId ~= nil and inst.id == selectedBlueprintId then
                if string.isEmpty(selectedBlueprintType) or searchGroup.typeInfo.type:ToString() == selectedBlueprintType then
                    self:_OnClickCell(typeIndex, index)
                    return true
                end
            end
        end
    end
    return false
end

FacSearchBlueprintCtrl._TrySearch = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAni)
    self.m_curKeyWord = string.trim(self.view.inputField.text)
    if string.isEmpty(self.m_curKeyWord) then
        self:_RealSearch(skipAni)
        return
    end
    GameInstance.player.wikiSystem:CheckSensitive(self.m_curKeyWord)
end

FacSearchBlueprintCtrl._OnCheckSensitiveSuccess = HL.Method() << function(self)
    self:_RealSearch()
end

FacSearchBlueprintCtrl._RealSearch = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAni)
    self:_UpdateSearchResultData()
    self:_UpdateSearchResultView(skipAni)
end

FacSearchBlueprintCtrl._UpdateSearchResultData = HL.Method() << function(self)
    local keyword = self.m_curKeyWord
    self.m_searchResults = {}
    if string.isEmpty(keyword) then
        self.view.emptyText.text = Language.LUA_FAC_BLUEPRINT_SEARCH_HINT
        return
    end
    self.view.emptyText.text = Language.LUA_FAC_BLUEPRINT_SEARCH_EMPTY
    local sortNode = self.m_bpCtrl.view.sortNode
    local sortKeys, isIncremental = sortNode:GetCurSortKeys(), sortNode.isIncremental
    for _, typeInfo in ipairs(self.m_typeInfos) do
        local validInsts = {}
        for _, inst in pairs(typeInfo.insts) do
            if string.find(inst.csInst.info.name, keyword) then
                table.insert(validInsts, inst)
            end
        end
        if next(validInsts) then
            table.sort(validInsts, Utils.genSortFunction(sortKeys, isIncremental))
            table.insert(self.m_searchResults, {
                typeInfo = typeInfo,
                validInsts = validInsts,
            })
        end
    end
end

FacSearchBlueprintCtrl._UpdateSearchResultView = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAni)
    TimerManager:ClearAllTimer(self)
    if not next(self.m_searchResults) then
        if skipAni then
            self.view.emptyActivateNode.gameObject:SetActive(true)
            self.view.emptyNodeAnimationWrapper:PlayInAnimation()
            self.view.contentNode.gameObject:SetActive(false)
        else
            self.animationWrapper:Play("facblueprint_searchcontent_out", function()
                self.view.emptyActivateNode.gameObject:SetActive(true)
                self.view.emptyNodeAnimationWrapper:PlayInAnimation()
                self.view.contentNode.gameObject:SetActive(false)
            end)
        end
        self.m_selectedTypeIndex = 0
        self.m_selectedIndex = 0
        return
    end
    self.m_typeCells:Refresh(#self.m_searchResults, function(typeCell, typeIndex)
        local typeInfo = self.m_searchResults[typeIndex]
        typeCell.titleTxt.text = typeInfo.typeInfo.name
        typeCell.iconImg:LoadSprite(typeInfo.typeInfo.icon)
        if not typeCell.m_cells then
            typeCell.m_cells = UIUtils.genCellCache(typeCell.blueprintCell)
            typeCell.m_cells.m_items[1] = typeCell.blueprintCell
        end
        local count = 0
        typeCell.m_cells:Refresh(#typeInfo.validInsts, function(cell, index)
            if typeIndex == 1 and index == 1 then
                self:SetNaviTarget(cell.view.button)
            end
            self:_OnUpdateCell(cell, typeIndex, index)
            cell.gameObject:SetActive(false)
            cell.view.animationWrapper:ClearTween(false)
            if count == 0 then
                cell.gameObject:SetActive(true)
                cell.view.animationWrapper:PlayInAnimation()
            else
                cell.view.animationWrapper:SampleToInAnimationBegin()
                self:_StartTimer(count * self.view.config.CELL_SHOW_DELAY, function()
                    cell.gameObject:SetActive(true)
                    cell.view.animationWrapper:PlayInAnimation()
                end)
            end
            count = count + 1
        end)
    end)
    self.view.emptyActivateNode.gameObject:SetActive(false)
    self.view.contentNode.gameObject:SetActive(true)
    if not skipAni then
        self.animationWrapper:Play("facblueprint_searchcontent_in")
    end
    self:_OnClickCell(1, 1)
end

FacSearchBlueprintCtrl._OnUpdateCell = HL.Method(HL.Forward('BlueprintCell'), HL.Number, HL.Number) << function(self, cell, typeIndex, index)
    local inst = self.m_searchResults[typeIndex].validInsts[index]
    cell:InitBlueprintCell({
        inst = inst,
        onClick = function()
            self:_OnClickCell(typeIndex, index)
        end,
        showStatus = not Utils.isInBlackbox(),
    })
    cell.view.selected.gameObject:SetActive(self.m_selectedTypeIndex == typeIndex and self.m_selectedIndex == index)
end

FacSearchBlueprintCtrl._OnClickCell = HL.Method(HL.Number, HL.Number) << function(self, typeIndex, index)
    if #self.m_searchResults < typeIndex or #(self.m_searchResults[typeIndex].validInsts) < index then
        return
    end
    if self.m_selectedTypeIndex == typeIndex and self.m_selectedIndex == index then
        return
    end
    if self.m_selectedTypeIndex > 0 then
        local oldCell = self.m_typeCells:Get(self.m_selectedTypeIndex).m_cells:Get(self.m_selectedIndex)
        oldCell.view.selected.gameObject:SetActive(false)
    end
    self.m_selectedTypeIndex = typeIndex
    self.m_selectedIndex = index
    local newCell = self.m_typeCells:Get(typeIndex).m_cells:Get(index)
    newCell.view.selected.gameObject:SetActive(true)
    self:_UpdateInfoNode()
    self:RefreshShareState()
    self.view.infoNode:ClearTween(false)
    self.view.infoNode:PlayInAnimation()
end

FacSearchBlueprintCtrl._UpdateInfoNode = HL.Method() << function(self)
    local inst = self.m_searchResults[self.m_selectedTypeIndex].validInsts[self.m_selectedIndex]
    if not inst then
        return
    end
    self.m_blueprintID = inst.id
    local content = self.view.blueprintContent
    content:InitBlueprintContent(inst.csInst, self.m_blueprintID,nil, false,false, self.m_bpAbnormalIconHelper)
    if self.m_friendSharing then
        self.view.blueprintContent:SetFriendShareState()
    end
    local canEdit = inst.csInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Mine
    content.view.rightActions.editBtn.gameObject:SetActive(canEdit)
    content.view.rightActions.previewBtn.gameObject:SetActive(not canEdit)
    self.view.bottomBtns.useBtn.gameObject:SetActive(content.canUseBP)
    self.view.bottomBtns.lackNode.gameObject:SetActive(content.haveLackTechs and not content.canUseBP)
end

FacSearchBlueprintCtrl._OnClickCraftBtn = HL.Method() << function(self)
    local deviceList = self.view.blueprintContent:GetAllDeviceIdAndCount()
    Notify(MessageConst.OPEN_FAC_BUILD_MODE_SELECT, { bluePrintData = deviceList })
end

FacSearchBlueprintCtrl._OnClickUseBtn = HL.Method() << function(self)
    if not Utils.isInFacMainRegion() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BLUEPRINT_USE_WHEN_OUTSIDE)
        return
    end

    self:_ExitOtherPhase()

    if not Utils.isInFactoryMode() then
        LuaSystemManager.factory:ClearAndSetFactoryMode(true, true)
    end

    local inst = self.m_searchResults[self.m_selectedTypeIndex].validInsts[self.m_selectedIndex]
    local clearScreenKey = UIManager:ClearScreen({ PANEL_ID, PanelId.FacBlueprint })

    if DeviceInfo.usingKeyboard then
        InputManager.SetMousePos(Vector2(Screen.width / 2, Screen.height / 2))
    end

    LuaSystemManager.factory:ToggleTopView(true, true)
    local searchInfos = {
        keyword = self.m_curKeyWord,
        csBPInst = inst.csInst,
    }
    self:Close()
    PhaseManager:ExitPhaseFast(PhaseId.FacBlueprint)
    Notify(MessageConst.FAC_ENTER_BLUEPRINT_MODE, {
        fastEnter = true,
        csBPInst = inst.csInst,
        range = inst.csInst.info.bp.sourceRect,
        searchInfos = searchInfos
    })
    UIManager:RecoverScreen(clearScreenKey)
end

FacSearchBlueprintCtrl._ExitOtherPhase = HL.Method() << function(self)

    local exitList = {}
    for k = PhaseManager.m_phaseStack:TopIndex(), PhaseManager.m_phaseStack:BottomIndex(), -1 do
        local p = PhaseManager.m_phaseStack:Get(k)
        if not(p.phaseId == PhaseId.FacBlueprint or p.phaseId == PhaseId.Level) then
            table.insert(exitList, p.phaseId)
        end
    end
    for _, v in ipairs(exitList) do
        PhaseManager:ExitPhaseFast(v)
    end
end

FacSearchBlueprintCtrl.FacOnModifyBlueprint = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    self.view.blueprintContent:Refresh()
    local cell = self.m_typeCells:Get(self.m_selectedTypeIndex).m_cells:Get(self.m_selectedIndex)
    self:_OnUpdateCell(cell, self.m_selectedTypeIndex, self.m_selectedIndex)
end

FacSearchBlueprintCtrl._SendToFriend = HL.Method() << function(self)
    local inst = self.m_searchResults[self.m_selectedTypeIndex].validInsts[self.m_selectedIndex]
    local id = inst.id
    self:_GetShareCode(inst, id)
end

FacSearchBlueprintCtrl._GetShareCode = HL.Method(HL.Any,HL.Any) << function(self, inst, id)
    self.m_gettingShareCode = true
    local type = inst.csInst.sourceType
    if type == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Mine then
        GameInstance.player.remoteFactory.blueprint:SendShareBlueprint(id)
    elseif type == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Sys then
        GameInstance.player.remoteFactory.blueprint:SendShareSysBlueprint(id)
    elseif type == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift then
        GameInstance.player.remoteFactory.blueprint:SendShareGiftBlueprint(id)
    else
        logger.error("Invalid blueprint source type: "..tostring(type))
    end
end

FacSearchBlueprintCtrl.RefreshShareState = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    local cell, index, reviewStatus
    if arg then
        local bpId, status = unpack(arg)
        reviewStatus = status

        for i, info in ipairs(self.m_searchResults[sourceTypeTable.Mine].validInsts) do
            if info.id == bpId then
                index = i
                break
            end
        end
        if not index then
            return
        end
        cell = self.m_typeCells:Get(sourceTypeTable.Mine).m_cells:Get(index)
    else

        index = self.m_selectedIndex
        reviewStatus = self.m_searchResults[self.m_selectedTypeIndex].validInsts[index].csInst.reviewStatus
        cell = self.m_typeCells:Get(self.m_selectedTypeIndex).m_cells:Get(index)
    end

    cell:RefreshCellState(reviewStatus)
    if self.m_selectedIndex == index then
        self.view.shareBtn.gameObject:SetActive(reviewStatus ~= CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintReviewStatus.InProgress)
        self.view.needAuditHintNode.gameObject:SetActive(reviewStatus == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintReviewStatus.InProgress)
        self.view.shareBtn.onClick:RemoveAllListeners()
        if reviewStatus == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintReviewStatus.Approved then
            self.view.shareBtn.onClick:AddListener(function()
                self:_SendToFriend()
            end)
        elseif reviewStatus == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintReviewStatus.Pending then
            self.view.shareBtn.onClick:AddListener(function()
                self.view.blueprintContent:_PendingShare()
            end)
        end
    end
end

FacSearchBlueprintCtrl.FacOnShareBlueprint = HL.Method(HL.Table) << function(self, args)
    if self.m_gettingShareCode then
        self.m_gettingShareCode = false
        local shareCode = unpack(args)
        GameInstance.player.friendChatSystem:SendChatBluePrint(self.m_friendRoleId ,shareCode, function()
            self:Close()
            PhaseManager:GoToPhase(PhaseId.SNS, { roleId = self.m_friendRoleId })
        end)
    end
end

FacSearchBlueprintCtrl.OnShow = HL.Override() << function(self)
    self.view.scrollView.verticalNormalizedPosition = self.m_normalizedPosition
    local isEmpty = self.m_searchResults == nil or #self.m_searchResults == 0
    self.view.emptyActivateNode.gameObject:SetActive(isEmpty)
    if DeviceInfo.usingController and not isEmpty then
        self.view.blueprintContent.view.topContainer:ManuallyStopFocus()
        self.m_typeCells:Refresh(#self.m_searchResults, function(typeCell, typeIndex)
            local typeInfo = self.m_searchResults[typeIndex]
            typeCell.m_cells:Refresh(#typeInfo.validInsts, function(cell, index)
                if typeIndex == self.m_selectedTypeIndex and index == self.m_selectedIndex then
                    self:_StartCoroutine(function()
                        coroutine.step()
                        self:SetNaviTarget(cell.view.button)
                    end)
                end
            end)
        end)
    end
end

FacSearchBlueprintCtrl.m_normalizedPosition = HL.Field(HL.Number) << 0

FacSearchBlueprintCtrl.OnHide = HL.Override() << function(self)
    self.m_normalizedPosition = self.view.scrollView.verticalNormalizedPosition
end

FacSearchBlueprintCtrl._ShowPreview = HL.Method() << function(self)
    if DeviceInfo.usingController then
        self.view.blueprintContent.view.topContainer:ManuallyStopFocus()
    end

    local inst = self.m_searchResults[self.m_selectedTypeIndex].validInsts[self.m_selectedIndex]
    UIManager:Open(PanelId.FacSaveBlueprint, {
        bpInst = inst.csInst,
    })
end

HL.Commit(FacSearchBlueprintCtrl)
