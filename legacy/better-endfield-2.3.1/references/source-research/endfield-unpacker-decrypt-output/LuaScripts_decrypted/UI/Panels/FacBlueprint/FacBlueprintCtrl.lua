local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacBlueprint
local PHASE_ID = PhaseId.FacBlueprint

local lastTabIndexSaveKey = "FAC_BP_LAST_TAB_INDEX"
local FAC_BLUEPRINT_INSTRUCTION_BOOK_ID = "fac_blueprint_statement"

FacBlueprintCtrl = HL.Class('FacBlueprintCtrl', uiCtrl.UICtrl)






FacBlueprintCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.FAC_ON_DELETE_BLUEPRINT] = 'FacOnDeleteBlueprint',
    [MessageConst.FAC_ON_MODIFY_BLUEPRINT] = 'FacOnModifyBlueprint',
    [MessageConst.FAC_ON_QUERY_SHARED_BLUEPRINT] = 'FacOnQuerySharedBlueprint',
    [MessageConst.FAC_ON_FETCH_BLUEPRINT] = 'GetGiftBlueprint',
    [MessageConst.FAC_ON_GET_GIFT_BLUEPRINT] = 'FacOnGetGiftBlueprint',
    [MessageConst.FAC_ON_MODIFY_BLUEPRINT] = 'FacOnModifyBlueprint',
    [MessageConst.FAC_ON_REVIEW_BLUEPRINT] = 'RefreshShareState',
    [MessageConst.FAC_ON_SHARE_BLUEPRINT] = 'FacOnShareBlueprint',
    [MessageConst.ON_ITEM_COUNT_CHANGED] = 'OnItemCountChanged',
    [MessageConst.FAC_ON_REFRESH_TECH_TREE_UI] = 'OnRefreshTechTree',
    [MessageConst.FAC_QUERY_BLUEPRINT_TIME_OUT] = 'OnQueryFailed',
    [MessageConst.FAC_ON_UPDATE_FRIEND_CHAT_STATE] = 'OnUpdateFriendChatState',
    [MessageConst.FAC_RESET_BLUEPRINT_FILTER] = 'ResetFilter',
}

FacBlueprintCtrl.m_getCell = HL.Field(HL.Function)

FacBlueprintCtrl.m_typeCells = HL.Field(HL.Forward('UIListCache'))

FacBlueprintCtrl.m_typeInfos = HL.Field(HL.Table)

FacBlueprintCtrl.m_showingInsts = HL.Field(HL.Table)

FacBlueprintCtrl.m_selectedTypeIndex = HL.Field(HL.Number) << 1

FacBlueprintCtrl.m_selectedIndex = HL.Field(HL.Number) << 1

FacBlueprintCtrl.m_inShareMode = HL.Field(HL.Boolean) << false

FacBlueprintCtrl.m_bpAbnormalIconHelper = HL.Field(HL.Table)

FacBlueprintCtrl.m_giftBlueprintLoaded = HL.Field(HL.Boolean) << false

FacBlueprintCtrl.m_waitingForSearching = HL.Field(HL.Boolean) << false

FacBlueprintCtrl.m_friendSharing = HL.Field(HL.Boolean) << false

FacBlueprintCtrl.m_friendChatState = HL.Field(HL.Boolean) << false

FacBlueprintCtrl.m_fetchingGift = HL.Field(HL.Boolean) << false

FacBlueprintCtrl.m_shareCode = HL.Field(HL.Any)

FacBlueprintCtrl.m_friendRoleId = HL.Field(HL.Any)

FacBlueprintCtrl.m_gettingShareCode = HL.Field(HL.Boolean) << false

local sourceTypeTable = {
    Mine = 1,
    Sys = 2,
    Gift = 3,
}



FacBlueprintCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    local facSaveBlueprintState = arg and arg.facSaveBlueprintState or nil
    local needRecoverInstructionBook = arg ~= nil and arg.needRecoverInstructionBook == true
    local commonPopUpArg = arg and arg.commonPopUpArg or nil
    local recoverState = arg and arg.recoverState or nil
    self:_InitArg(arg)

    self.view.shareStateController:SetState(self.m_friendSharing and "Share" or "Normal")
    self.view.mainStateController:SetState("Normal")
    self.m_readBPIds = {}
    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)
    self.m_typeCells = UIUtils.genCellCache(self.view.typeCell)
    self.m_bpAbnormalIconHelper = FactoryUtils.createBPAbnormalIconHelper()

    self:_InitButtons()
    self:_InitInfos()
    self:_InitSortAndFilter(recoverState)
    self:_RefreshTypeCells(arg)
    self:_TryRecoverDeleteState(recoverState)
    self:_InitShare()
    self:_InitBlackBox()
    self:_InitController()
    self:_ChooseEnterMode()
    self:_TryRecoverSaveBlueprintPanel(facSaveBlueprintState)
    self:_TryRecoverInstructionBook(needRecoverInstructionBook)
    self:_TryRecoverCommonPopUp(commonPopUpArg)
    self:_InitDebug()
end

FacBlueprintCtrl._InitArg = HL.Method(HL.Table) << function(self, arg)
    if not arg then
        return
    end

    if arg.friendSharing and arg.roleId then
        self.m_friendSharing = arg.friendSharing
        self.m_friendRoleId = arg.roleId
    end

    if arg.fetchingGift and arg.shareCode then
        self.m_fetchingGift = arg.fetchingGift
        self.m_shareCode = arg.shareCode
    end

    if arg.bpSearchInfos then
        self.m_bpSearchInfos = arg.bpSearchInfos
    end
end

FacBlueprintCtrl._InitButtons = HL.Method() << function(self)
    self.view.helpBtn.onClick:AddListener(function()
        self:_OpenInstructionBook()
    end)
    self.view.searchBtn.onClick:AddListener(function()
        self:_Search()
    end)

    self.view.bottomBtns.craftBtn.onClick:AddListener(function()
        self:_OnClickCraftBtn()
    end)
    self.view.bottomBtns.useBtn.onClick:AddListener(function()
        self:_OnClickUseBtn()
    end)

    self.view.bottomBtns.lackNode.onClick:AddListener(function()
        self.view.blueprintContent:LocateFirstLackTech()
    end)

    self.view.delBtn.onClick:AddListener(function()
        self:_OnClickDeleteBtn()
    end)
    self.view.delNode.confirmDelBtn.onClick:AddListener(function()
        self:_OnClickConfirmDelBtn()
    end)
    self.view.delNode.cancelDelBtn.onClick:AddListener(function()
        self:_OnClickCancelDelBtn()
    end)

    self.view.createBtn.onClick:AddListener(function()
        self:_OnClickCreate()
    end)

    local content = self.view.blueprintContent
    content.view.rightActions.editBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.FacSaveBlueprint, {
            bpInst = self.m_showingInsts[self.m_selectedIndex].csInst,
        })
    end)
    content.view.rightActions.previewBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.FacSaveBlueprint, {
            bpInst = self.m_showingInsts[self.m_selectedIndex].csInst,
        })
    end)
end

FacBlueprintCtrl._InitDebug = HL.Method() << function(self)
    if BEYOND_DEBUG_COMMAND then
        UIUtils.bindInputEvent(CS.Beyond.Input.KeyboardKeyCode.C, function()
            local inst = self.m_showingInsts[self.m_selectedIndex]
            Unity.GUIUtility.systemCopyBuffer = tostring(inst.id)
            Notify(MessageConst.SHOW_TOAST, string.format("DEBUG: 已复制ID %s", tostring(inst.id)))
        end, nil, nil, self.view.inputGroup.groupId)
        UIUtils.bindInputEvent(CS.Beyond.Input.KeyboardKeyCode.S, function()
            local inst = self.m_showingInsts[self.m_selectedIndex]
            local msg = CS.Proto.CS_GM_COMMAND()
            msg.Command = "FactorySetBluePrintReviewStatus " .. tostring(inst.id) .. " 2"
            CS.Beyond.Network.NetBus.instance.defaultSender:Send(msg)
            Notify(MessageConst.SHOW_TOAST, string.format("DEBUG: 使用GM使蓝图通过审核", tostring(inst.id)))
        end, nil, nil, self.view.inputGroup.groupId)
    end
end

FacBlueprintCtrl._ChooseEnterMode = HL.Method() << function(self)

    if self.m_fetchingGift and self.m_shareCode then
        GameInstance.player.remoteFactory.blueprint:SendQuerySharedBlueprint(self.m_shareCode)
    end


    if self.m_bpSearchInfos then
        self:_Search()
    end
end

FacBlueprintCtrl._TryRecoverSaveBlueprintPanel = HL.Method(HL.Opt(HL.Any)) << function(self, facSaveBlueprintState)
    if not facSaveBlueprintState then
        return
    end
    UIManager:Open(PanelId.FacSaveBlueprint, facSaveBlueprintState)
end

FacBlueprintCtrl._OpenInstructionBook = HL.Method() << function(self)
    UIManager:Open(PanelId.InstructionBook, {
        id = FAC_BLUEPRINT_INSTRUCTION_BOOK_ID,
        onClose = function()
            if self.m_isClosed then
                return
            end
            local topContainer = self.view and self.view.blueprintContent and self.view.blueprintContent.view and self.view.blueprintContent.view.topContainer
            if topContainer and topContainer.ManuallyStopFocus then
                topContainer:ManuallyStopFocus()
            end
        end,
    })
end

FacBlueprintCtrl._NeedRecoverInstructionBook = HL.Method().Return(HL.Boolean) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return false
    end
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    return isOpen and instructionCtrl:IsShow() and instructionCtrl.id == FAC_BLUEPRINT_INSTRUCTION_BOOK_ID
end

FacBlueprintCtrl._TryRecoverInstructionBook = HL.Method(HL.Boolean) << function(self, needRecoverInstructionBook)
    if not needRecoverInstructionBook then
        return
    end
    self:_OpenInstructionBook()
end

FacBlueprintCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = {}
    if self.m_friendSharing and self.m_friendRoleId then
        arg.friendSharing = true
        arg.roleId = self.m_friendRoleId
    end
    if self.m_fetchingGift and self.m_shareCode then
        arg.fetchingGift = true
        arg.shareCode = self.m_shareCode
    end
    local isSearchOpen, searchCtrl = UIManager:IsOpen(PanelId.FacSearchBlueprint)
    if isSearchOpen and searchCtrl and searchCtrl:IsShow() and searchCtrl.GetRecoverStateArg then
        arg.bpSearchInfos = searchCtrl:GetRecoverStateArg()
    elseif self.m_bpSearchInfos then
        arg.bpSearchInfos = lume.deepCopy(self.m_bpSearchInfos)
    end
    local isSaveOpen, saveCtrl = UIManager:IsOpen(PanelId.FacSaveBlueprint)
    if isSaveOpen and saveCtrl and saveCtrl:IsShow() and saveCtrl.GetRecoverStateArg then
        arg.facSaveBlueprintState = saveCtrl:GetRecoverStateArg()
    end
    arg.needRecoverInstructionBook = self:_NeedRecoverInstructionBook()
    local commonPopUpArg = self:_GetCommonPopUpRecoverState()
    if commonPopUpArg then
        arg.commonPopUpArg = commonPopUpArg
    end

    local recoverState = {}
    local curTypeInfo = self.m_typeInfos and self.m_typeInfos[self.m_selectedTypeIndex]
    if curTypeInfo then
        recoverState.selectedType = curTypeInfo.type:ToString()
    end
    local selectedInst = self.m_showingInsts and self.m_showingInsts[self.m_selectedIndex]
    if selectedInst then
        recoverState.selectedBlueprintId = selectedInst.id
    end
    if self.view and self.view.sortNode then
        recoverState.sortSelectedIndex = self.view.sortNode:GetCurSelectedIndex()
        recoverState.sortIsIncremental = self.view.sortNode.isIncremental
    end
    local selectedTags = self.view and self.view.filterBtn and self.view.filterBtn.GetSelectedTags and self.view.filterBtn:GetSelectedTags()
    if selectedTags and #selectedTags > 0 then
        recoverState.filterTags = {}
        for _, tag in ipairs(selectedTags) do
            table.insert(recoverState.filterTags, {
                type = tag.type,
                id = tag.id,
            })
        end
        table.sort(recoverState.filterTags, Utils.genSortFunction({ "type", "id" }, true))
    end
    local deleteState = self:_BuildDeleteRecoverState()
    if deleteState.inDeleteMode then
        recoverState.inDeleteMode = true
        recoverState.selectedDeleteBlueprintIds = deleteState.selectedDeleteBlueprintIds
    end
    arg.recoverState = recoverState
    return arg
end





FacBlueprintCtrl.GetTypeInfos = HL.Method().Return(HL.Table) << function(self)
    return self.m_typeInfos or {}
end

FacBlueprintCtrl.GetBPAbnormalIconHelper = HL.Method().Return(HL.Table) << function(self)
    return self.m_bpAbnormalIconHelper
end

FacBlueprintCtrl._GetCommonPopUpRecoverState = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local isOpen, commonPopUpCtrl = UIManager:IsOpen(PanelId.CommonPopUp)
    if not isOpen or not commonPopUpCtrl:IsShow() then
        return
    end
    local recoverArg = commonPopUpCtrl:GetCurPhaseStateArg()
    if not self:_IsFacBlueprintCommonPopUpArg(recoverArg) then
        return
    end
    return recoverArg
end

FacBlueprintCtrl._IsFacBlueprintCommonPopUpArg = HL.Method(HL.Opt(HL.Any)).Return(HL.Boolean) << function(self, arg)
    if arg == nil or type(arg) ~= "table" or string.isEmpty(arg.content) then
        return false
    end
    return arg.content == Language.LUA_FAC_BLUEPRINT_DEL_HINT
        or arg.content == Language.LUA_FAC_BLUEPRINT_IMPORT_POPUP
end

FacBlueprintCtrl._TryRecoverCommonPopUp = HL.Method(HL.Opt(HL.Any)) << function(self, commonPopUpArg)
    if commonPopUpArg == nil then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    if not self:_IsFacBlueprintCommonPopUpArg(commonPopUpArg) then
        return
    end
    local isOpen, commonPopUpCtrl = UIManager:IsOpen(PanelId.CommonPopUp)
    if isOpen and commonPopUpCtrl:IsShow() then
        return
    end
    commonPopUpArg.onConfirm = function(shareCode)
        self:_OnClickImportConfirm(shareCode)
    end
    commonPopUpArg.onCancel = function()
        UIManager:Close(PanelId.BlueprintShareBlackScreen)
    end
    self:Notify(MessageConst.SHOW_POP_UP, commonPopUpArg)
end

FacBlueprintCtrl._OnClickImportConfirm = HL.Method(HL.String) << function(self, shareCode)
    self.m_shareCode = shareCode
    GameInstance.player.remoteFactory.blueprint:SendQuerySharedBlueprint(shareCode)
end

FacBlueprintCtrl.m_filterTags = HL.Field(HL.Table)

FacBlueprintCtrl._GetRecoverFilterTags = HL.Method(HL.Table, HL.Opt(HL.Any)).Return(HL.Table) << function(self, filterTagGroups, recoverState)
    if not recoverState or not recoverState.filterTags or #recoverState.filterTags == 0 then
        return {}
    end
    local recoverTagMap = {}
    for _, tagInfo in ipairs(recoverState.filterTags) do
        recoverTagMap[tagInfo.type] = recoverTagMap[tagInfo.type] or {}
        recoverTagMap[tagInfo.type][tagInfo.id] = true
    end
    local recoverFilterTags = {}
    for _, groupInfo in ipairs(filterTagGroups) do
        for _, tagInfo in ipairs(groupInfo.tags) do
            if recoverTagMap[tagInfo.type] and recoverTagMap[tagInfo.type][tagInfo.id] then
                table.insert(recoverFilterTags, tagInfo)
            end
        end
    end
    return recoverFilterTags
end

FacBlueprintCtrl._InitSortAndFilter = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    local inited = false

    local filterTagGroups = FactoryUtils.getBlueprintTagGroupInfos()
    local recoverFilterTags = self:_GetRecoverFilterTags(filterTagGroups, recoverState)
    if #recoverFilterTags > 0 then
        self.m_filterTags = {}
        for _, tagInfo in ipairs(recoverFilterTags) do
            self.m_filterTags[tagInfo.type] = self.m_filterTags[tagInfo.type] or {}
            table.insert(self.m_filterTags[tagInfo.type], tagInfo)
        end
    end
    self.view.filterBtn:InitFilterBtn({
        tagGroups = filterTagGroups,
        selectedTags = recoverFilterTags,
        onConfirm = function(tags)
            if not inited then
                return
            end
            if tags then
                self.m_filterTags = {}
                for _, v in ipairs(tags) do
                    if not self.m_filterTags[v.type] then
                        self.m_filterTags[v.type] = {}
                    end
                    table.insert(self.m_filterTags[v.type], v)
                end
            else
                self.m_filterTags = nil
            end
            self:_ApplyFilter()
            self:_RefreshList()
        end,
        getResultCount = function(tags)
            return self:_GetContentFilterResultCount(tags)
        end,
        sortNodeWidget = self.view.sortNode,
    })
    local sortOptions = {
        {
            name = Language.LUA_FAC_BLUEPRINT_SORT_TIME,
            keys = { "fetchTime", "useCount" },
        },
        {
            name = Language.LUA_FAC_BLUEPRINT_SORT_USE_COUNT,
            keys = { "useCount", "fetchTime" },
        },
    }
    local sortSelectedCsIndex
    if recoverState and type(recoverState.sortSelectedIndex) == "number" then
        sortSelectedCsIndex = CSIndex(lume.clamp(recoverState.sortSelectedIndex, 1, #sortOptions))
    end
    local sortIsIncremental = recoverState and recoverState.sortIsIncremental or nil
    self.view.sortNode:InitSortNode(sortOptions, function(optData, isIncremental)
        if not inited then
            return
        end
        self:_SortData(optData, isIncremental)
        self:_RefreshList()
    end, sortSelectedCsIndex, sortIsIncremental, true, self.view.filterBtn)

    inited = true
end

FacBlueprintCtrl._ApplyFilter = HL.Method() << function(self)
    local allInsts = self.m_typeInfos[self.m_selectedTypeIndex].insts
    if not self.m_filterTags or not next(self.m_filterTags) then
        self.m_showingInsts = {}
        for _, v in pairs(allInsts) do
            table.insert(self.m_showingInsts, v)
        end
    else
        self.m_showingInsts = {}
        for _, v in pairs(allInsts) do
            if self:_CheckPassFilter(self.m_filterTags, v) then
                table.insert(self.m_showingInsts, v)
            end
        end
    end

    local sort = self.view.sortNode

    local sortData = sort:GetCurSortData()
    self:_SortData(sortData, sort.isIncremental)
end

FacBlueprintCtrl._CheckPassFilter = HL.Method(HL.Table, HL.Table).Return(HL.Boolean) << function(self, filterTags, inst)
    for _, tagList in pairs(filterTags) do
        local foundTag
        for _, tag in ipairs(tagList) do
            if inst.csInst.info.tags:Contains(tag.id) then
                foundTag = true
                break
            end
        end
        if not foundTag then
            return false
        end
    end
    return true
end

FacBlueprintCtrl._GetContentFilterResultCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    local tagGroups = {}
    if tags then
        tagGroups = {}
        for _, v in ipairs(tags) do
            if not tagGroups[v.type] then
                tagGroups[v.type] = {}
            end
            table.insert(tagGroups[v.type], v)
        end
    else
        tagGroups = nil
    end

    local typeInfo = self.m_typeInfos[self.m_selectedTypeIndex]
    if not tagGroups or not next(tagGroups) then
        return typeInfo.count
    else
        local count = 0
        for _, v in pairs(typeInfo.insts) do
            if self:_CheckPassFilter(tagGroups, v) then
                count = count + 1
            end
        end
        return count
    end
end

FacBlueprintCtrl.ResetFilter = HL.Method() << function(self)
    if not self.m_typeInfos or not self.m_typeInfos[self.m_selectedTypeIndex] then
        return
    end

    self.m_filterTags = nil
    self.view.filterBtn:_UpdateState({})
    self:_ApplyFilter()
    self:_RefreshList()
end

FacBlueprintCtrl._SortData = HL.Method(HL.Table, HL.Boolean) << function(self, sortData, isIncremental)

    local keys = lume.copy(sortData.keys)
    if Utils.isInBlackbox() or self.m_selectedTypeIndex == sourceTypeTable.Sys then
        keys[#keys + 1] = "idForSort"
    end
    table.sort(self.m_showingInsts, Utils.genSortFunction(sortData.keys, isIncremental))
end





FacBlueprintCtrl._InitInfos = HL.Method() << function(self)
    local csBPSys = GameInstance.player.remoteFactory.blueprint
    self.m_giftBlueprintLoaded = false

    if Utils.isInBlackbox() then
        local sysInfo = {
            type = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Sys,
            name = Utils.isInBlackbox() and Language.LUA_FAC_BLUEPRINT_TYPE_NAME_PRESET or Language.LUA_FAC_BLUEPRINT_TYPE_NAME_SYS,
            icon = "FacBlueprint/icon_fac_blueprint_btn_system",
            canDelete = false,
        }
        sysInfo.insts, sysInfo.count = self:_ConvertCS2LuaBPMap(csBPSys.presetBlueprints)
        self.m_typeInfos = { sysInfo }
    else
        local mineInfo = {
            type = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Mine,
            name = Language.LUA_FAC_BLUEPRINT_TYPE_NAME_MINE,
            icon = "FacBlueprint/icon_fac_blueprint_btn_mine",
            canDelete = true,
            maxCount = Tables.facBlueprintConst.MyBluePrintNumMax,
        }
        mineInfo.insts, mineInfo.count = self:_ConvertCS2LuaBPMap(csBPSys.myBlueprints)

        local sysInfo = {
            type = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Sys,
            name = Utils.isInBlackbox() and Language.LUA_FAC_BLUEPRINT_TYPE_NAME_PRESET or Language.LUA_FAC_BLUEPRINT_TYPE_NAME_SYS,
            icon = "FacBlueprint/icon_fac_blueprint_btn_system",
            canDelete = false,
        }

        sysInfo.insts, sysInfo.count = self:_ConvertCS2LuaBPMap(csBPSys.builtinBlueprints)

        local shareInfo = {
            type = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift,
            name = Language.LUA_FAC_BLUEPRINT_TYPE_NAME_SHARE,
            icon = "FacBlueprint/icon_fac_blueprint_btn_shared",
            canDelete = true,
            maxCount = Tables.facBlueprintConst.GiftBluePrintNumMax,
            insts = {},
            count = 0,
        }


        if csBPSys:AllGiftBlueprintLoaded() then
            shareInfo.insts, shareInfo.count = self:_ConvertCS2LuaBPMap(csBPSys.giftBlueprintHandles, true)
            self.m_giftBlueprintLoaded = true
        end

        self.m_typeInfos = { mineInfo, sysInfo, shareInfo }
    end
end

FacBlueprintCtrl._ConvertCS2LuaBPMap = HL.Method(HL.Any, HL.Opt(HL.Boolean)).Return(HL.Table, HL.Number) << function(self, csBPs, isGift)
    if not csBPs then
        return {}, 0
    end
    local count = 0
    local insts = {}
    for id, v in pairs(csBPs) do
        local csInst = isGift and v.loadedBlueprintInstance or v
        insts[id] = {
            id = id,
            idForSort = tostring(id),
            csInst = csInst,
            type = csInst.sourceType,
            useCount = csInst.useCount,
            isNew = csInst.isNew,
            fetchTime = csInst.fetchTime,
        }
        count = count + 1
    end
    return insts, count
end






FacBlueprintCtrl._GetRecoverTypeIndex = HL.Method(HL.Opt(HL.Table)).Return(HL.Number) << function(self, initArg)
    local recoverState = initArg and initArg.recoverState
    local recoverType = recoverState and recoverState.selectedType or nil
    if string.isEmpty(recoverType) then
        return 0
    end
    for index, info in ipairs(self.m_typeInfos) do
        if info.type:ToString() == recoverType then
            return index
        end
    end
    return 0
end

FacBlueprintCtrl._RefreshTypeCells = HL.Method(HL.Opt(HL.Table)) << function(self, initArg)

    local recoverTypeIndex = self:_GetRecoverTypeIndex(initArg)
    if recoverTypeIndex > 0 then
        self.m_selectedTypeIndex = recoverTypeIndex
    elseif initArg then
        if initArg.csBPInst then
            local found
            for k, v in ipairs(self.m_typeInfos) do
                for _, vv in pairs(v.insts) do
                    if vv.csInst == initArg.csBPInst then
                        self.m_selectedTypeIndex = k
                        found = true
                        break
                    end
                end
                if found then
                    break
                end
            end
        elseif initArg.blueprintType and sourceTypeTable[initArg.blueprintType] then
            self.m_selectedTypeIndex = sourceTypeTable[initArg.blueprintType]
        end
    elseif not Utils.isInBlackbox() and not self.m_fetchingGift and not self.m_friendSharing then

        local hasValue, value = ClientDataManagerInst:GetInt(lastTabIndexSaveKey, false)
        if hasValue then
            self.m_selectedTypeIndex = value
        else
            self.m_selectedTypeIndex = 2
        end
    end
    self.m_typeCells:Refresh(#self.m_typeInfos, function(cell, index)
        local info = self.m_typeInfos[index]
        cell.redDot.gameObject:SetActive(false)
        cell.icon:LoadSprite(info.icon)
        cell.nameTxt.text = info.name
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.isOn = index == self.m_selectedTypeIndex
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:_ReadCurShowingBPs()
                self:_OnClickTypeCell(index)
            end
        end)
        cell.gameObject.name = "Cell_" .. index
    end)
    self:_OnClickTypeCell(self.m_selectedTypeIndex, true, initArg)
end

FacBlueprintCtrl._RefreshCreateAndImportBtn = HL.Method() << function(self)
    self.view.createBtn.gameObject:SetActive(self.m_selectedTypeIndex == sourceTypeTable.Mine and not self.m_inShareMode and not Utils.isInBlackbox() and not self.m_isDeleting and not self.m_friendSharing)
    self.view.importBtn.gameObject:SetActive(self.m_selectedTypeIndex == sourceTypeTable.Gift and not self.m_inShareMode and not Utils.isInBlackbox() and not self.m_isDeleting and not self.m_friendSharing)
end
FacBlueprintCtrl._OnClickTypeCell = HL.Method(HL.Number, HL.Opt(HL.Boolean, HL.Table)) << function(self, index, forceUpdate, initArg)
    if self.m_selectedTypeIndex == index and not forceUpdate then
        return
    end
    self.m_selectedTypeIndex = index
    local info = self.m_typeInfos[index]
    self:_RefreshDelBtn()


    if info and info.type then
        CS.Beyond.Gameplay.Conditions.OnFacBlueprintTabOpen.Trigger(info.type)
    end

    if info.maxCount then
        self.view.listTitleNode.gameObject:SetActive(true)
        self.view.bpCountTxt.text = string.format(Language.LUA_FAC_BLUEPRINT_BP_COUNT_FORMAT, UIUtils.setCountColor(info.count, info.count >= info.maxCount), info.maxCount)
    else
        self.view.listTitleNode.gameObject:SetActive(false)
    end

    self:_RefreshCreateAndImportBtn()

    if not Utils.isInBlackbox() and not self.m_fetchingGift and not self.m_friendSharing then

        ClientDataManagerInst:SetInt(lastTabIndexSaveKey, index, false)
    end


    if info.type == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift and (not self.m_giftBlueprintLoaded or forceUpdate) and (not initArg or not initArg.csBPInst) then
        if not self.m_giftBlueprintLoaded then

            self.m_showingInsts = {}
            self:_RefreshList()
        end
        self:GetGiftBlueprint(nil)
        return
    end

    self:_ApplyFilter()
    self:_RefreshList(initArg)
end

FacBlueprintCtrl._RefreshList = HL.Method(HL.Opt(HL.Table)) << function(self, initArg)
    local count = #self.m_showingInsts
    local scrollList = self.view.scrollList
    local recoverState = initArg and initArg.recoverState
    local recoverBlueprintId
    if (not initArg or (initArg.csBPInst == nil and initArg.blueprintId == nil)) and recoverState then
        recoverBlueprintId = recoverState.selectedBlueprintId
    end

    local isEmpty = count == 0
    if not isEmpty then
        local index = 1
        local isInit = false
        if (initArg and (initArg.csBPInst ~= nil or initArg.blueprintId ~= nil)) or recoverBlueprintId ~= nil then
            isInit = true
        end

        if initArg and initArg.csBPInst then
            for k, v in ipairs(self.m_showingInsts) do
                if v.csInst == initArg.csBPInst then
                    index = k
                    break
                end
            end
        elseif initArg and initArg.blueprintId then
            for k, v in ipairs(self.m_showingInsts) do
                if v.id == initArg.blueprintId then
                    index = k
                    break
                end
            end
        elseif recoverBlueprintId ~= nil then
            for k, v in ipairs(self.m_showingInsts) do
                if v.id == recoverBlueprintId then
                    index = k
                    break
                end
            end
        end
        scrollList:UpdateCount(count, index, false, false, isInit)
        if DeviceInfo.usingController and not self.m_friendChatState then



            if initArg and (initArg.csBPInst ~= nil or initArg.blueprintId ~= nil) then
                self.m_cellOnHide = nil
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
            local cell = self.m_getCell(index)
            self:SetNaviTarget(cell.view.button)
        end
        self:_OnClickCell(index, true, true)
        local countPerLine = scrollList.countPerLine
        local bgWidth = countPerLine * (scrollList.cellWidth + scrollList.space.x)
        local lineCount = math.max(math.ceil(count / countPerLine), scrollList.maxShowingCellCount / countPerLine)
        local bgHeight = lineCount * (scrollList.cellHeight + scrollList.space.y)
        self.view.scrollListBG.transform.sizeDelta = Vector2(bgWidth, bgHeight)
    else
        self.m_selectedIndex = 0
    end
    self.view.infoContentNode.gameObject:SetActive(not isEmpty)
    self.view.infoEmptyNode.gameObject:SetActive(isEmpty)
    self.view.listContentNode.gameObject:SetActive(not isEmpty)
    self.view.listEmptyNode.gameObject:SetActive(isEmpty)
    if isEmpty then

        scrollList:UpdateCount(0)
        if self.m_filterTags and next(self.m_filterTags) then

            self.view.listEmptyTxt.text = Language.LUA_FAC_BLUEPRINT_FILTER_RESULT_IS_EMPTY
        else
            local info = self.m_typeInfos[self.m_selectedTypeIndex]
            if info.type == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Mine then
                self.view.listEmptyTxt.text = Language.LUA_FAC_BLUEPRINT_MINE_LIST_EMPTY
            elseif info.type == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift then
                self.view.listEmptyTxt.text = Language.LUA_FAC_BLUEPRINT_GIFT_LIST_EMPTY
            else
                self.view.listEmptyTxt.text = Language.LUA_FAC_BLUEPRINT_SYS_LIST_EMPTY
            end
        end
    end
end

FacBlueprintCtrl._OnUpdateCell = HL.Method(HL.Forward('BlueprintCell'), HL.Number, HL.Opt(HL.Table)) << function(self, cell, index, initArg)
    local ActionOnSetNaviTarget = CS.Beyond.Input.ActionOnSetNaviTarget
    local action = (not self.m_isDeleting) and ActionOnSetNaviTarget.AutoTriggerOnClick or ActionOnSetNaviTarget.PressConfirmTriggerOnClick
    cell.view.button:ChangeActionOnSetNaviTarget(action)

    local inst = self.m_showingInsts[index]
    cell:InitBlueprintCell({
        inst = inst,
        onClick = function()
            self:_OnClickCell(index, false, false)
        end,
        showStatus = not Utils.isInBlackbox(),
    })
    cell.view.button.onIsNaviTargetChanged = function(isTarget)
        if isTarget and self.m_isDeleting then
            self:_OnClickCell(index, true, false)
        end
    end

    cell.view.selected.gameObject:SetActive(index == self.m_selectedIndex)
    self.m_readBPIds[inst.id] = true
end

FacBlueprintCtrl._OnClickCell = HL.Method(HL.Number, HL.Boolean, HL.Opt(HL.Boolean)) << function(self, index, onlyShow, isInit)
    if self.m_showingInsts[index] == nil then
        return
    end

    if self.m_isDeleting then
        local inst = self.m_showingInsts[index]
        if not onlyShow then
            inst.isDel = not inst.isDel
        end
        local cell = self.m_getCell(index)
        if cell then
            cell.view.button.hintTextId = inst.isDel and "key_hint_common_unselect" or "key_hint_common_select"
            Notify(MessageConst.REFRESH_CONTROLLER_HINT)
            cell.view.delMark.gameObject:SetActive(inst.isDel)
        end
        self:_UpdateDelCount()
    end


    if not isInit and not onlyShow and not (index == self.m_selectedIndex and not self.m_isDeleting) then
        local oldInst = self.m_showingInsts[self.m_selectedIndex]
        local newInst = self.m_showingInsts[index]
        local ids = {}
        if oldInst then
            table.insert(ids, oldInst.id)
        end
        if newInst then
            table.insert(ids, newInst.id)
        end
        self:_ReadBPs(ids)
    end

    if not isInit and index == self.m_selectedIndex then
        return
    end

    self.view.infoEmptyNode.gameObject:SetActive(false)
    self.view.infoContentNode.gameObject:SetActive(true)
    self.view.infoNode:ClearTween(false)
    self.view.infoNode:PlayInAnimation()

    local oldCell = self.m_getCell(self.m_selectedIndex)
    if oldCell then
        oldCell.view.selected.gameObject:SetActive(false)
    end

    local newCell = self.m_getCell(index)
    if newCell then
        newCell.view.selected.gameObject:SetActive(true)
    end

    self.m_selectedIndex = index
    self:_UpdateInfoNode()

    if self.m_isDeleting then
        self.view.bottomBtns.craftBtn.gameObject:SetActive(false)
        self.view.bottomBtns.useBtn.gameObject:SetActive(false)
        self.view.bottomBtns.lackNode.gameObject:SetActive(false)
    end

    if self.m_friendSharing then
        self:RefreshShareState()
    end
end

FacBlueprintCtrl._UpdateInfoNode = HL.Method() << function(self)
    local inst = self.m_showingInsts[self.m_selectedIndex]
    if not inst then
        return
    end
    local content = self.view.blueprintContent
    content:InitBlueprintContent(inst.csInst, inst.id, nil, false, false, self.m_bpAbnormalIconHelper)
    InputManagerInst:ToggleGroup(content.view.topBinding.groupId, false)
    InputManagerInst:ToggleGroup(content.view.inputBindingGroupMonoTarget.groupId, not self.m_isDeleting)
    self.view.blueprintContent.view.rightActions.gameObject:SetActive(not self.m_isDeleting)
    if self.m_isDeleting then
        self.view.bottomBtns.craftBtn.gameObject:SetActive(false)
        self.view.bottomBtns.useBtn.gameObject:SetActive(false)
        self.view.bottomBtns.lackNode.gameObject:SetActive(false)
    end
    if self.m_friendSharing then
        self.view.blueprintContent:SetFriendShareState()
    end
    local canEdit = inst.csInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Mine
    content.view.rightActions.editBtn.gameObject:SetActive(canEdit)
    content.view.rightActions.previewBtn.gameObject:SetActive(not canEdit)
    self.view.bottomBtns.useBtn.gameObject:SetActive(content.canUseBP)
    self.view.bottomBtns.lackNode.gameObject:SetActive(content.haveLackTechs and not content.canUseBP)
end

FacBlueprintCtrl._OnClickCreate = HL.Method() << function(self)
    if not Utils.isInFacMainRegion() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BLUEPRINT_CREATE_WHEN_OUTSIDE)
        return
    end

    self:_ExitOtherPhase()

    if not Utils.isInFactoryMode() then
        LuaSystemManager.factory:ClearAndSetFactoryMode(true, true)
    end

    local clearScreenKey = UIManager:ClearScreen({ PANEL_ID })
    LuaSystemManager.factory:ToggleTopView(true, true)
    PhaseManager:ExitPhaseFast(PHASE_ID)
    Notify(MessageConst.FAC_ENTER_DESTROY_MODE, {
        fastEnter = true,
        showCreateHint = true,
    })
    UIManager:RecoverScreen(clearScreenKey)
end

FacBlueprintCtrl._OnClickCraftBtn = HL.Method() << function(self)
    if GameInstance.player.systemActionConflictManager.curProcessingSystemAction then
        return
    end
    local deviceList = self.view.blueprintContent:GetAllDeviceIdAndCount()
    Notify(MessageConst.OPEN_FAC_BUILD_MODE_SELECT, { bluePrintData = deviceList })
end

FacBlueprintCtrl._ExitOtherPhase = HL.Method() << function(self)

    local exitList = {}
    for k = PhaseManager.m_phaseStack:TopIndex(), PhaseManager.m_phaseStack:BottomIndex(), -1 do
        local p = PhaseManager.m_phaseStack:Get(k)
        if not(p.phaseId == PHASE_ID or p.phaseId == PhaseId.Level) then
            table.insert(exitList, p.phaseId)
        end
    end
    for _, v in ipairs(exitList) do
        PhaseManager:ExitPhaseFast(v)
    end
end

FacBlueprintCtrl._OnClickUseBtn = HL.Method() << function(self)
    if not Utils.isInFacMainRegion() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BLUEPRINT_USE_WHEN_OUTSIDE)
        return
    end

    if GameInstance.player.systemActionConflictManager.curProcessingSystemAction then
        return
    end

    self:_ExitOtherPhase()

    if not Utils.isInFactoryMode() then
        LuaSystemManager.factory:ClearAndSetFactoryMode(true, true)
    end

    local inst = self.m_showingInsts[self.m_selectedIndex]
    local clearScreenKey = UIManager:ClearScreen({ PANEL_ID })

    if DeviceInfo.usingKeyboard then
        InputManager.SetMousePos(Vector2(Screen.width / 2, Screen.height / 2))
    end

    LuaSystemManager.factory:ToggleTopView(true, true)
    PhaseManager:ExitPhaseFast(PHASE_ID)
    Notify(MessageConst.FAC_ENTER_BLUEPRINT_MODE, {
        fastEnter = true,
        csBPInst = inst.csInst,
        range = inst.csInst.info.bp.sourceRect,
    })
    UIManager:RecoverScreen(clearScreenKey)
end






FacBlueprintCtrl.FacOnModifyBlueprint = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    UIManager:Hide(PanelId.CommonPopUp)
    self.view.blueprintContent:Refresh()
    self.view.blueprintContent:_RefreshShareState()

    self.view.scrollList:UpdateShowingCells(function(csIndex, obj)
        self:_OnUpdateCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)
end






FacBlueprintCtrl.m_isDeleting = HL.Field(HL.Boolean) << false

FacBlueprintCtrl._BuildDeleteRecoverState = HL.Method().Return(HL.Table) << function(self)
    if not self.m_isDeleting then
        return {}
    end
    local deleteState = {
        inDeleteMode = true,
        selectedDeleteBlueprintIds = {},
    }
    for _, inst in ipairs(self.m_showingInsts or {}) do
        if inst.isDel then
            table.insert(deleteState.selectedDeleteBlueprintIds, inst.id)
        end
    end
    return deleteState
end

FacBlueprintCtrl._TryRecoverDeleteState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if not recoverState or recoverState.inDeleteMode ~= true then
        return
    end
    self:_ToggleDelete(true, true)
    local selectedIds = recoverState.selectedDeleteBlueprintIds
    if not selectedIds or #selectedIds == 0 then
        return
    end
    local selectedIdMap = {}
    for _, id in ipairs(selectedIds) do
        selectedIdMap[id] = true
    end
    local typeInfo = self.m_typeInfos and self.m_typeInfos[self.m_selectedTypeIndex]
    if not typeInfo or not typeInfo.insts then
        return
    end
    for _, inst in pairs(typeInfo.insts) do
        inst.isDel = selectedIdMap[inst.id] == true
    end
    self.view.scrollList:UpdateShowingCells(function(csIndex, obj)
        self:_OnUpdateCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)
    local selectedBlueprintId = recoverState.selectedBlueprintId
    if selectedBlueprintId ~= nil then
        for index, inst in ipairs(self.m_showingInsts or {}) do
            if inst.id == selectedBlueprintId then
                self:_OnClickCell(index, true, true)
                break
            end
        end
    end
    self:_UpdateDelCount()
end

FacBlueprintCtrl._ToggleDelete = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isDeleting, skipAni)
    self.m_isDeleting = isDeleting
    self.view.blueprintContent.view.rightActions.shareNode.gameObject:SetActive(not isDeleting)

    self.view.delNode.animationWrapper:ClearTween(false)
    if skipAni or isDeleting then
        self.view.mainStateController:SetState(isDeleting and "Delete" or "Normal")
        self:_RefreshDelBtn()
    else
        self.view.delNode.gameObject:SetActive(true)
        self.view.delNode.animationWrapper:PlayOutAnimation(function()
            self.view.mainStateController:SetState("Normal")
            self:_RefreshDelBtn()
        end)
    end

    self:_RefreshDelBtn()
    self.view.closeBtn.interactable = not isDeleting
    self.view.helpBtn.interactable = not isDeleting
    self.view.searchBtn.interactable = not isDeleting

    self.m_typeCells:Update(function(cell, index)
        cell.toggle.interactable = not isDeleting or index == self.m_selectedTypeIndex
    end)
    InputManagerInst:ToggleBinding(self.view.typeNode.moveToNextBindingId, not isDeleting)
    InputManagerInst:ToggleBinding(self.view.typeNode.moveToPreviousBindingId, not isDeleting)
    self.view.bottomBtns.craftBtn.gameObject:SetActive(not isDeleting)
    self.view.bottomBtns.useBtn.gameObject:SetActive(not isDeleting)
    self.view.bottomBtns.lackNode.gameObject:SetActive(not isDeleting)
    self.view.controllerSideMenuBtn.gameObject:SetActive(not isDeleting)
    self.view.searchKeyHint.gameObject:SetActive(not isDeleting)

    for _, inst in pairs(self.m_typeInfos[self.m_selectedTypeIndex].insts) do
        inst.isDel = false
    end
    if isDeleting then
        self:_UpdateDelCount()

        self.m_selectedIndex = 0
        self.view.infoEmptyNode.gameObject:SetActive(true)
        self.view.infoContentNode.gameObject:SetActive(false)
    else
        if self.m_selectedIndex == 0 then
            self.m_selectedIndex = 1
        end
        self:_RefreshList()
    end
    self.view.scrollList:UpdateShowingCells(function(csIndex, obj)
        self:_OnUpdateCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)
    self:_RefreshCreateAndImportBtn()
end

FacBlueprintCtrl._OnClickDeleteBtn = HL.Method() << function(self)
    self:_ToggleDelete(true)
end

FacBlueprintCtrl._OnClickConfirmDelBtn = HL.Method() << function(self)
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_FAC_BLUEPRINT_DEL_HINT,
        onConfirm = function()
            local ids = {}
            for _, inst in ipairs(self.m_showingInsts) do
                if inst.isDel then
                    if inst.csInst.param.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Mine then
                        table.insert(ids, inst.csInst.param.myBpUid)
                    elseif inst.csInst.param.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift then
                        table.insert(ids, inst.id)
                    end
                end
            end
            if not next(ids) then
                return
            end
            if self.m_typeInfos[self.m_selectedTypeIndex].type == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Mine then
                GameInstance.player.remoteFactory.blueprint:SendDeleteBlueprints(ids)
            elseif self.m_typeInfos[self.m_selectedTypeIndex].type == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift then
                GameInstance.player.remoteFactory.blueprint:SendDeleteGiftBlueprints(ids)
            end
            UIManager:Close(PanelId.CommonPopUp)
        end,
    })
end

FacBlueprintCtrl._OnClickCancelDelBtn = HL.Method() << function(self)
    self:_ToggleDelete(false)
end

FacBlueprintCtrl.FacOnDeleteBlueprint = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    self:_ToggleDelete(false)
    self:_InitInfos()
    self:_OnClickTypeCell(self.m_selectedTypeIndex, true)
end

FacBlueprintCtrl._UpdateDelCount = HL.Method() << function(self)
    local count = 0
    for _, v in ipairs(self.m_showingInsts) do
        if v.isDel then
            count = count + 1
        end
    end
    self.view.delNode.delCountTxt.text = count
    self.view.delNode.noSelectHint.gameObject:SetActive(count == 0)

    if DeviceInfo.usingController then
        self.view.delNode.confirmDelBtn.gameObject:SetActive(true)
        self.view.delNode.root.gameObject:SetActive(count > 0)
        self.view.delNode.confirmDelBtn.interactable = count > 0
    else
        self.view.delNode.confirmDelBtn.gameObject:SetActive(count > 0)
    end
end






FacBlueprintCtrl.m_readBPIds = HL.Field(HL.Table)

FacBlueprintCtrl._ReadCurShowingBPs = HL.Method() << function(self)
    if not self.m_readBPIds or not next(self.m_readBPIds) then
        return
    end
    local ids = {}
    for k, _ in pairs(self.m_readBPIds) do
        table.insert(ids, k)
    end
    self.m_readBPIds = {}
    self:_ReadBPs(ids)
end

FacBlueprintCtrl._ReadBPs = HL.Method(HL.Table) << function(self, ids)
    local t = self.m_typeInfos[self.m_selectedTypeIndex].type
    if t == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Mine then
        GameInstance.player.remoteFactory.blueprint:SendReadMyBlueprint(ids)
    elseif t == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Sys then
        GameInstance.player.remoteFactory.blueprint:SendReadSysBlueprint(ids)
    elseif t == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift then
        GameInstance.player.remoteFactory.blueprint:SendReadGiftBlueprint(ids)
    end
end





FacBlueprintCtrl._InitShare = HL.Method() << function(self)
    self.view.importBtn.onClick:AddListener(function()

        if GameInstance.player.friendSystem.isCommunicationRestricted or GameInstance.player.remoteFactory.blueprint.forbidFetch then
            Notify(MessageConst.SHOW_TOAST,Language.LUA_BLUEPRINT_BAN_IMPORT)
            return
        end
        Notify(MessageConst.SHOW_POP_UP, {
            characterLimit = FacConst.FAC_BLUEPRINT_IMPORT_INPUTFIELD_MAX_LENGTH,
            content = Language.LUA_FAC_BLUEPRINT_IMPORT_POPUP,
            input = true,
            inputPaste = true,
            pasteFunc = FactoryUtils.getMatchingBlueprintShareCode,
            onConfirm = function(shareCode)
                self:_OnClickImportConfirm(shareCode)
            end,
            closeOnConfirm = false,
            onCancel = function()
                UIManager:Close(PanelId.BlueprintShareBlackScreen)
            end
        })
    end)
end

FacBlueprintCtrl.FacOnQuerySharedBlueprint = HL.Method(HL.Any) << function(self, arg)

    if not self:IsShow() then
        return
    end
    UIManager:Close(PanelId.CommonPopUp)
    self.m_importingInst = unpack(arg)
    self.m_selectedTypeIndex = sourceTypeTable.Gift
    self:_OnClickTypeCell(self.m_selectedTypeIndex, true)
end

FacBlueprintCtrl.m_queryFailed = HL.Field(HL.Boolean) << false

FacBlueprintCtrl.OnQueryFailed = HL.Method() << function(self)







end

FacBlueprintCtrl.m_importingInst = HL.Field(HL.Any)

FacBlueprintCtrl._ImportBlueprint = HL.Method() << function(self)
    UIManager:Open(PanelId.FacSaveBlueprint, {
        bpInst = self.m_importingInst,
        isSharing = false,
        isEditing = false,
        isImporting = true,
        id = -1,
        shareCode = self.m_shareCode,
        fromFriend = self.m_fetchingGift,
    })
    self.m_importingInst = nil
end

FacBlueprintCtrl.OnUpdateFriendChatState = HL.Method(HL.Any) << function(self, state)
    self.m_friendChatState = state
end

FacBlueprintCtrl.GetGiftBlueprint = HL.Method(HL.Any) << function(self, arg)
    self.view.loadingNode.gameObject:SetActive(true)
    local csBPSys = GameInstance.player.remoteFactory.blueprint
    csBPSys:GetGiftBlueprint()
end

FacBlueprintCtrl.FacOnGetGiftBlueprint = HL.Method() << function(self)






    self.m_giftBlueprintLoaded = true
    local info = self.m_typeInfos[sourceTypeTable.Gift]
    local csBPSys = GameInstance.player.remoteFactory.blueprint
    local shareInfo = self.m_typeInfos[sourceTypeTable.Gift]
    shareInfo.insts, shareInfo.count = self:_ConvertCS2LuaBPMap(csBPSys.giftBlueprintHandles, true)
    self.m_typeInfos[sourceTypeTable.Gift] = shareInfo


    self.view.loadingNode.gameObject:SetActive(false)


    if self.m_waitingForSearching then
        self.m_waitingForSearching = false
        self:_OpenSearch()
    elseif self.m_importingInst then
        self:_ImportBlueprint()
    else
        self.m_selectedTypeIndex = sourceTypeTable.Gift
        self.m_typeCells:Refresh(#self.m_typeInfos, function(cell, index)
            if index == self.m_selectedTypeIndex then
                cell.toggle.isOn = true
            end
        end)
        self:_ApplyFilter()
        self:_RefreshList()
        self.view.bpCountTxt.text = string.format(Language.LUA_FAC_BLUEPRINT_BP_COUNT_FORMAT, UIUtils.setCountColor(info.count, info.count >= info.maxCount), info.maxCount)
        self.view.mainStateController:SetState("Normal")
        self:_RefreshCreateAndImportBtn()
    end


    self:_RefreshDelBtn()
end

FacBlueprintCtrl._SendToFriend = HL.Method() << function(self)
    local inst = self.m_showingInsts[self.m_selectedIndex].csInst
    local id = self.m_showingInsts[self.m_selectedIndex].id
    self:_GetShareCode(inst, id)
end

FacBlueprintCtrl.RefreshShareState = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    local cell, index, reviewStatus
    if arg then
        local bpId, status = unpack(arg)
        reviewStatus = status

        for i, info in ipairs(self.m_showingInsts) do
            if info.id == bpId then
                index = i
                break
            end
        end
        if not index then
            return
        end
    else

        index = self.m_selectedIndex
        reviewStatus = self.m_showingInsts[index].csInst.reviewStatus
    end

    cell = self.m_getCell(index)


    if cell then
        cell:RefreshCellState(reviewStatus)
    end
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

FacBlueprintCtrl._GetShareCode = HL.Method(HL.Any, HL.Any) << function(self, inst, id)
    self.m_gettingShareCode = true
    if inst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Mine then
        GameInstance.player.remoteFactory.blueprint:SendShareBlueprint(id)
    elseif inst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Sys then
        GameInstance.player.remoteFactory.blueprint:SendShareSysBlueprint(id)
    else
        GameInstance.player.remoteFactory.blueprint:SendShareGiftBlueprint(id)
    end
end

FacBlueprintCtrl.FacOnShareBlueprint = HL.Method(HL.Table) << function(self, args)
    if self.m_gettingShareCode then
        self.m_gettingShareCode = false
        local shareCode = unpack(args)
        GameInstance.player.friendChatSystem:SendChatBluePrint(self.m_friendRoleId, shareCode, function()
            PhaseManager:PopPhase(PhaseId.FacBlueprint)
        end)
    end
end




FacBlueprintCtrl._InitBlackBox = HL.Method() << function(self)
    local inBlackbox = Utils.isInBlackbox()
    self.view.controllerSideMenuBtn.gameObject:SetActive(not inBlackbox)
    self.view.leftKeyHint.gameObject:SetActive(not inBlackbox)
    self.view.rightKeyHint.gameObject:SetActive(not inBlackbox)
    self.view.sortAndFilterNode.gameObject:SetActive(not inBlackbox)
    self.view.searchBtn.gameObject:SetActive(not inBlackbox)
    self.view.helpBtn.gameObject:SetActive(not inBlackbox)
    if inBlackbox then
        local curSceneInfo = GameInstance.remoteFactoryManager.currentSceneInfo
        local blackboxEnableBuildingCraft = curSceneInfo.data.blackbox.buildingCraft ~= nil
        self.view.bottomBtns.craftBtn.interactable = blackboxEnableBuildingCraft
    end
end





FacBlueprintCtrl.m_cellOnHide = HL.Field(HL.Any)

FacBlueprintCtrl.OnHide = HL.Override() << function(self)
    if DeviceInfo.usingController and self.view.containerSelectableNaviGroup.IsTopLayer then
        self.m_cellOnHide = self.view.containerSelectableNaviGroup.LayerSelectedTarget
    end
end

FacBlueprintCtrl.OnShow = HL.Override() << function(self)
    if DeviceInfo.usingController then
        if self.m_cellOnHide then
            self:SetNaviTarget(self.m_cellOnHide)
            self.m_cellOnHide = nil
        end
        if Utils.isInBlackbox() then
            InputManagerInst:ToggleGroup(self.view.blueprintContent.view.topBinding.groupId, false)
            self:BindInputPlayerAction("fac_blueprint_edit", function()
                UIManager:Open(PanelId.FacSaveBlueprint, {
                    bpInst = self.m_showingInsts[self.m_selectedIndex].csInst,
                })
            end)
        end
    end
end

FacBlueprintCtrl.OnClose = HL.Override() << function(self)
    self:_ReadCurShowingBPs()
end

FacBlueprintCtrl.OnItemCountChanged = HL.Method(HL.Table) << function(self, args)
    if self.m_selectedIndex > 0 then
        self.view.blueprintContent:UpdateCount()
    end
end

FacBlueprintCtrl.OnRefreshTechTree = HL.Method() << function(self)
    if self:IsShow() then
        self:_UpdateInfoNode()
    end
    if DeviceInfo.usingController and #self.view.blueprintContent.m_lackTechIdInfos == 0 then
        local cell = self.m_getCell(self.m_selectedIndex)
        if cell then
            self:SetNaviTarget(cell.view.button)
        end
    end
end

FacBlueprintCtrl._RefreshDelBtn = HL.Method() << function(self)
    local bpCanDelete = self.m_typeInfos[self.m_selectedTypeIndex].canDelete
    self.view.delBtn.gameObject:SetActive(bpCanDelete and not self.m_friendSharing and not self.m_isDeleting)
end

FacBlueprintCtrl.OnPhaseRefresh = HL.Override(HL.Any) << function(self, arg)
    if not arg then
        return
    end

    if arg.friendSharing and arg.roleId then
        self.m_friendSharing = arg.friendSharing
        self.m_friendRoleId = arg.roleId
        self.view.shareStateController:SetState(self.m_friendSharing and "Share" or "Normal")
        self:_RefreshCreateAndImportBtn()
        self:_UpdateInfoNode()
    end

    if arg.fetchingGift and arg.shareCode then
        self.m_fetchingGift = arg.fetchingGift
        self.m_shareCode = arg.shareCode
        GameInstance.player.remoteFactory.blueprint:SendQuerySharedBlueprint(self.m_shareCode)
    end

    if arg.csBPInst or arg.blueprintType then
        self:_RefreshTypeCells(arg)
    end
end




FacBlueprintCtrl.m_bpSearchInfos = HL.Field(HL.Any)

FacBlueprintCtrl._Search = HL.Method() << function(self)

    local info = self.m_typeInfos[sourceTypeTable.Gift]
    if info.type == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift and (not self.m_giftBlueprintLoaded) and (not Utils.isInBlackbox()) then
        self.m_waitingForSearching = true
        self:GetGiftBlueprint(nil)
    else
        self:_OpenSearch()
    end
end

FacBlueprintCtrl._OpenSearch = HL.Method() << function(self)
    local searchInfos = self.m_bpSearchInfos
    if self.m_friendSharing then
        searchInfos = searchInfos or {}
        searchInfos.setStateShare = true
        searchInfos.friendRoleId = self.m_friendRoleId
    end
    UIManager:Open(PanelId.FacSearchBlueprint, searchInfos)
    self.m_bpSearchInfos = nil
end




FacBlueprintCtrl._InitController = HL.Method() << function(self)

    if not DeviceInfo.usingController then
        return
    end


    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })


    if Utils.isInBlackbox() then
        return
    end


    local extraBtnInfos = {}
    table.insert(extraBtnInfos, {
        name = "BlueprintInstructionBook",
        action = function()
            self:_OpenInstructionBook()
        end,
        textId = "key_hint_fac_blueprint_instruction_book",
    })
    self.view.controllerSideMenuBtn:InitControllerSideMenuBtn({
        extraBtnInfos = extraBtnInfos
    })
end



HL.Commit(FacBlueprintCtrl)
