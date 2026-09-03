local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GemCustomization
local PHASE_ID = PhaseId.GemCustomization
local INSTRUCTION_BOOK_ID = "energy_point_target"
GemCustomizationCtrl = HL.Class('GemCustomizationCtrl', uiCtrl.UICtrl)






GemCustomizationCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_WORLD_ENERGY_POINT_SELECT_TERMS_CHANGED] = 'OnWorldEnergyPointSelectTermsChanged',
}


GemCustomizationCtrl.m_info = HL.Field(HL.Table)

GemCustomizationCtrl.m_uiRelate = HL.Field(HL.Table)

GemCustomizationCtrl.m_term1CellListCache = HL.Field(HL.Forward("UIListCache"))

GemCustomizationCtrl.m_term2CellListCache = HL.Field(HL.Forward("UIListCache"))

GemCustomizationCtrl.m_term3CellListCache = HL.Field(HL.Forward("UIListCache"))




GemCustomizationCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData(self:_GetGameGroupId(arg))
    self:_RefreshAllUI()
    self:_ApplyRecoverState(self:_GetRecoverState(arg))
end



GemCustomizationCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    local gameGroupId = arg
    local cfg = Tables.worldEnergyPointGroupTable:GetValue(gameGroupId)
    self.m_info = {
        gameGroupId = arg,
        gemCustomItemId = cfg.gemCustomItemId,

        term1Infos = {},
        term2Infos = {},
        term3Infos = {},

        multiTermGroup1MaxCount = 3,
        multiTermGroup1SelectTermIndexList = {},

        multiTermGroup2MaxCount = 1,
        multiTermGroup2SelectTermData = {
            groupIndex = -1,
            termIndex = -1,
        },
    }


    local succ, groupData = GameInstance.player.worldEnergyPointSystem:TryGetWorldEnergyPointGroupRecord(gameGroupId)
    local selectTerms = (succ and groupData.hasSelectTerms) and groupData.selectTerms or {}
    for csIndex, termId in pairs(cfg.primAttrTermIds) do
        local hasCfg, termCfg = Tables.gemTable:TryGetValue(termId)
        if not hasCfg then
            logger.error("词条id配置不存在，id: " .. termId)
        else
            local selected = lume.find(selectTerms, termId) ~= nil
            if selected then
                table.insert(self.m_info.multiTermGroup1SelectTermIndexList, LuaIndex(csIndex))
            end
            table.insert(self.m_info.term1Infos, {
                termId = termId,
                termName = termCfg.tagName,
                isSelect = selected,
            })
        end
    end
    for csIndex, termId in pairs(cfg.secAttrTermIds) do
        local hasCfg, termCfg = Tables.gemTable:TryGetValue(termId)
        if not hasCfg then
            logger.error("词条id配置不存在，id: " .. termId)
        else
            local selected = lume.find(selectTerms, termId) ~= nil
            if selected then
                self.m_info.multiTermGroup2SelectTermData.groupIndex = 1
                self.m_info.multiTermGroup2SelectTermData.termIndex = LuaIndex(csIndex)
            end
            table.insert(self.m_info.term2Infos, {
                termId = termId,
                termName = termCfg.tagName,
                isSelect = selected,
            })
        end
    end
    for csIndex, termId in pairs(cfg.skillTermIds) do
        local hasCfg, termCfg = Tables.gemTable:TryGetValue(termId)
        if not hasCfg then
            logger.error("词条id配置不存在，id: " .. termId)
        else
            local selected = lume.find(selectTerms, termId) ~= nil
            if selected then
                self.m_info.multiTermGroup2SelectTermData.groupIndex = 2
                self.m_info.multiTermGroup2SelectTermData.termIndex = LuaIndex(csIndex)
            end
            table.insert(self.m_info.term3Infos, {
                termId = termId,
                termName = termCfg.tagName,
                isSelect = selected,
            })
        end
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

GemCustomizationCtrl._GetGameGroupId = HL.Method(HL.Any).Return(HL.Any) << function(self, arg)
    if type(arg) == "table" then
        return arg.gameGroupId
    end
    return arg
end

GemCustomizationCtrl._GetRecoverState = HL.Method(HL.Any).Return(HL.Opt(HL.Any)) << function(self, arg)
    if type(arg) ~= "table" then
        return nil
    end
    return arg.recoverState
end

GemCustomizationCtrl._CollectRecoverState = HL.Method().Return(HL.Table) << function(self)
    local recoverState = {

        multiTermGroup1SelectTermIndexList = lume.clone(self.m_info.multiTermGroup1SelectTermIndexList),
        multiTermGroup2SelectTermData = lume.deepCopy(self.m_info.multiTermGroup2SelectTermData),

        selectedMultiTermGroupIndex = DeviceInfo.usingController and self:_GetSelectedMultiTermGroupIndex() or nil,
    }
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() and instructionCtrl.id == INSTRUCTION_BOOK_ID and PhaseManager:GetTopPhaseId() == PHASE_ID then

        recoverState.instructionBookArg = {
            id = instructionCtrl.id,
        }
    end
    return recoverState
end



GemCustomizationCtrl._InitUI = HL.Method() << function(self)

    self.view.btnClose.onClick:AddListener(function()
        self:_OnClickBtnClose()
    end)
    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, INSTRUCTION_BOOK_ID)
    end)
    self.view.gemResultNode.confirmBtn.onClick:AddListener(function()
        self:_OnClickConfirmBtn()
    end)

    self.m_uiRelate = {
        multiTermGroupToTermResult = {},


        preSelectMultiTermGroup = nil,
    }
    for i = 1, 2 do
        local group = self.view["multiTermGroup" .. i]
        local termResult = self.view.gemResultNode["termResult" .. i]
        self.m_uiRelate.multiTermGroupToTermResult[group] = termResult

        group.btn.onClick:AddListener(function()
            self:_SetSelectMultiTermGroup(group, true)
        end)
    end

    self.m_term1CellListCache = UIUtils.genCellCache(self.view.multiTermGroup1.termGroupCell.termCell)
    self.m_term2CellListCache = UIUtils.genCellCache(self.view.multiTermGroup2.termGroupCell.termCell)
    self.m_term3CellListCache = UIUtils.genCellCache(self.view.multiTermGroup2.skillTermGroupCell.termCell)
end

GemCustomizationCtrl._RefreshAllUI = HL.Method() << function(self)
    self:_RefreshMultiTermGroup1UI()
    self:_RefreshMultiTermGroup2UI()
    self:_RefreshTermResult1UI()
    self:_RefreshTermResult2UI()
    self:_RefreshGemResultState()
    self.view.moneyCell:InitMoneyCell(self.m_info.gemCustomItemId)
    self.view.walletBarPlaceholder:InitWalletBarPlaceholder({self.m_info.gemCustomItemId})
    local itemCfg = Tables.itemTable:GetValue(self.m_info.gemCustomItemId)
    self.view.gemResultNode.gemItemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemCfg.iconId)

    local firstCell = self.m_term1CellListCache:Get(1)
    if firstCell then
        self:SetNaviTarget(firstCell.termBtn)
    end
end

GemCustomizationCtrl._RefreshMultiTermGroup1UI = HL.Method() << function(self)
    local count = #self.m_info.term1Infos
    self.m_term1CellListCache:Refresh(count, function(cell, luaIndex)
        self:_RefreshTermCell(cell, luaIndex, self.m_info.term1Infos, function()
            local info = self.m_info.term1Infos[luaIndex]
            self:_SetSelectMultiTermGroup(self.view.multiTermGroup1, true)

            local listIndex = lume.find(self.m_info.multiTermGroup1SelectTermIndexList, luaIndex)
            if listIndex then

                info.isSelect = false
                table.remove(self.m_info.multiTermGroup1SelectTermIndexList, listIndex)
            else

                local selectCount = #self.m_info.multiTermGroup1SelectTermIndexList
                if selectCount >= self.m_info.multiTermGroup1MaxCount then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_GEM_CUSTOM_SELECT_OVER_MAX_TOAST)
                    return
                end
                info.isSelect = true
                table.insert(self.m_info.multiTermGroup1SelectTermIndexList, luaIndex)
            end

            self:_RefreshMultiTermGroup1CompleteState()
            self:_RefreshTermResult1UI()
            self:_RefreshGemResultState()
        end, function(isTarget, isGroupChanged)
            if isTarget then
                self:_SetSelectMultiTermGroup(self.view.multiTermGroup1, true)
            end
        end)
    end)

    self:_SetSelectMultiTermGroup(self.view.multiTermGroup1, false)
    self:_RefreshMultiTermGroup1CompleteState()
end

GemCustomizationCtrl._RefreshMultiTermGroup2UI = HL.Method() << function(self)
    local count = #self.m_info.term2Infos
    self.m_term2CellListCache:Refresh(count, function(cell, luaIndex)
        self:_RefreshTermCell(cell, luaIndex, self.m_info.term2Infos, function()
            self:_SetSelectMultiTermGroup(self.view.multiTermGroup2, true)

            local selectGroupIndex = self.m_info.multiTermGroup2SelectTermData.groupIndex
            local selectTermIndex = self.m_info.multiTermGroup2SelectTermData.termIndex
            if selectGroupIndex == 1 then
                self.m_info.term2Infos[selectTermIndex].isSelect = false
            elseif selectGroupIndex == 2 then
                self.m_info.term3Infos[selectTermIndex].isSelect = false
            end

            if selectGroupIndex == 1 and selectTermIndex == luaIndex then
                self.m_info.multiTermGroup2SelectTermData.groupIndex = -1
                self.m_info.multiTermGroup2SelectTermData.termIndex = -1
            else
                self.m_info.multiTermGroup2SelectTermData.groupIndex = 1
                self.m_info.multiTermGroup2SelectTermData.termIndex = luaIndex
                self.m_info.term2Infos[luaIndex].isSelect = true
            end

            self:_RefreshMultiTermGroup2CompleteState()
            self:_RefreshTermResult2UI()
            self:_RefreshGemResultState()
        end, function(isTarget, isGroupChanged)
            if isTarget then
                self:_SetSelectMultiTermGroup(self.view.multiTermGroup2, true)
            end
        end)
    end)

    count = #self.m_info.term3Infos
    self.m_term3CellListCache:Refresh(count, function(cell, luaIndex)
        self:_RefreshTermCell(cell, luaIndex, self.m_info.term3Infos, function()
            self:_SetSelectMultiTermGroup(self.view.multiTermGroup2, true)

            local selectGroupIndex = self.m_info.multiTermGroup2SelectTermData.groupIndex
            local selectTermIndex = self.m_info.multiTermGroup2SelectTermData.termIndex
            if selectGroupIndex == 1 then
                self.m_info.term2Infos[selectTermIndex].isSelect = false
            elseif selectGroupIndex == 2 then
                self.m_info.term3Infos[selectTermIndex].isSelect = false
            end

            if selectGroupIndex == 2 and selectTermIndex == luaIndex then
                self.m_info.multiTermGroup2SelectTermData.groupIndex = -1
                self.m_info.multiTermGroup2SelectTermData.termIndex = -1
            else
                self.m_info.multiTermGroup2SelectTermData.groupIndex = 2
                self.m_info.multiTermGroup2SelectTermData.termIndex = luaIndex
                self.m_info.term3Infos[luaIndex].isSelect = true
            end

            self:_RefreshMultiTermGroup2CompleteState()
            self:_RefreshTermResult2UI()
            self:_RefreshGemResultState()
        end, function(isTarget, isGroupChanged)
            if isTarget then
                self:_SetSelectMultiTermGroup(self.view.multiTermGroup2, true)
            end
        end)
    end)

    self:_SetSelectMultiTermGroup(self.view.multiTermGroup2, false)
    self:_RefreshMultiTermGroup2CompleteState()
end

GemCustomizationCtrl._RefreshTermCell = HL.Method(HL.Any, HL.Number, HL.Table, HL.Function, HL.Function) << function(self, cell, luaIndex, infos, onClick, onIsNaviTargetChanged)
    local info = infos[luaIndex]
    cell.stateCtrl:SetState("Unselect")
    cell.termNameTxt.text = info.termName
    cell.termBtn.onClick:RemoveAllListeners()
    cell.termBtn.onClick:AddListener(onClick)
    cell.termBtn.onIsNaviTargetChanged = onIsNaviTargetChanged
    InputManagerInst:SetBindingText(cell.termBtn.hoverConfirmBindingId, Language.LUA_GEM_CUSTOM_CONFIRM_TERM)
end

GemCustomizationCtrl._RefreshMultiTermGroup1CompleteState = HL.Method() << function(self)
    local multiGroup1 = self.view.multiTermGroup1
    local count = #self.m_info.multiTermGroup1SelectTermIndexList
    multiGroup1.curCountTxt.text = count
    multiGroup1.maxCountTxt.text = '/' .. self.m_info.multiTermGroup1MaxCount
    local isComplete = count >= self.m_info.multiTermGroup1MaxCount
    multiGroup1.stateCtrl:SetState(isComplete and "Complete" or "Normal")

    for i = 1, #self.m_info.term1Infos do
        local termInfo = self.m_info.term1Infos[i]
        local cell = self.m_term1CellListCache:Get(i)
        if termInfo.isSelect then
            InputManagerInst:SetBindingText(cell.termBtn.hoverConfirmBindingId, Language.LUA_GEM_CUSTOM_CANCEL_TERM)
            cell.stateCtrl:SetState("Select")
        else
            if isComplete then
                InputManagerInst:SetBindingText(cell.termBtn.hoverConfirmBindingId, Language.LUA_GEM_CUSTOM_CONFIRM_TERM)
                cell.stateCtrl:SetState("Disable")
            else
                InputManagerInst:SetBindingText(cell.termBtn.hoverConfirmBindingId, Language.LUA_GEM_CUSTOM_CONFIRM_TERM)
                cell.stateCtrl:SetState("Unselect")
            end
        end
    end
end

GemCustomizationCtrl._RefreshMultiTermGroup2CompleteState = HL.Method() << function(self)
    local multiGroup2 = self.view.multiTermGroup2
    local count = self.m_info.multiTermGroup2SelectTermData.termIndex > 0 and 1 or 0
    multiGroup2.curCountTxt.text = count
    multiGroup2.maxCountTxt.text = '/' .. self.m_info.multiTermGroup2MaxCount
    multiGroup2.stateCtrl:SetState(count < self.m_info.multiTermGroup2MaxCount and "Normal" or "Complete")


    self.m_term2CellListCache:Update(function(cell, luaIndex)
        local info = self.m_info.term2Infos[luaIndex]
        cell.stateCtrl:SetState(info.isSelect and "Select" or "Unselect")
        InputManagerInst:SetBindingText(cell.termBtn.hoverConfirmBindingId, info.isSelect and
                Language.LUA_GEM_CUSTOM_CANCEL_TERM or
                Language.LUA_GEM_CUSTOM_CONFIRM_TERM)
    end)

    self.m_term3CellListCache:Update(function(cell, luaIndex)
        local info = self.m_info.term3Infos[luaIndex]
        cell.stateCtrl:SetState(info.isSelect and "Select" or "Unselect")
        InputManagerInst:SetBindingText(cell.termBtn.hoverConfirmBindingId, info.isSelect and
                Language.LUA_GEM_CUSTOM_CANCEL_TERM or
                Language.LUA_GEM_CUSTOM_CONFIRM_TERM)
    end)
end

GemCustomizationCtrl._RefreshTermResult1UI = HL.Method() << function(self)
    local termResult1 = self.view.gemResultNode.termResult1
    for i = 1, self.m_info.multiTermGroup1MaxCount do
        local termIndex = self.m_info.multiTermGroup1SelectTermIndexList[i]
        if termIndex ~= nil then
            local termInfo = self.m_info.term1Infos[termIndex]
            termResult1["termTxt" .. i].text = termInfo.termName
            termResult1["termTxt" .. i].color = self.view.config.TERM_DECIDED_COLOR
        else
            termResult1["termTxt" .. i].text = Language.LUA_GEM_CUSTOM_UNDECIDED_TEXT
            termResult1["termTxt" .. i].color = self.view.config.TERM_UNDECIDED_COLOR
        end
    end
end

GemCustomizationCtrl._RefreshTermResult2UI = HL.Method() << function(self)
    local termResult2 = self.view.gemResultNode.termResult2
    local groupIndex = self.m_info.multiTermGroup2SelectTermData.groupIndex
    if groupIndex > 0 then
        local termInfo
        if groupIndex == 1 then
            termInfo = self.m_info.term2Infos[self.m_info.multiTermGroup2SelectTermData.termIndex]
        elseif groupIndex == 2 then
            termInfo = self.m_info.term3Infos[self.m_info.multiTermGroup2SelectTermData.termIndex]
        end
        termResult2.termTxt1.text = termInfo.termName
        termResult2.termTxt1.color = self.view.config.TERM_DECIDED_COLOR
    else
        termResult2.termTxt1.text = Language.LUA_GEM_CUSTOM_UNDECIDED_TEXT
        termResult2.termTxt1.color = self.view.config.TERM_UNDECIDED_COLOR
    end
end

GemCustomizationCtrl._RefreshGemResultState = HL.Method() << function(self)
    local termResult1Complete = #self.m_info.multiTermGroup1SelectTermIndexList >= self.m_info.multiTermGroup1MaxCount
    local termResult2Complete = self.m_info.multiTermGroup2SelectTermData.groupIndex > 0

    if not termResult1Complete then
        self.view.gemResultNode.stateCtrl:SetState("NotCompleteTerm1")
    elseif not termResult2Complete then
        self.view.gemResultNode.stateCtrl:SetState("NotCompleteTerm2")
    else
        self.view.gemResultNode.stateCtrl:SetState("AllowConfirm")
    end
end

GemCustomizationCtrl._ApplyRecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    self:_ApplyRecoverTermSelectState(recoverState)
    self:_TryRecoverInstructionBook(recoverState)
end

GemCustomizationCtrl._ApplyRecoverTermSelectState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end


    for _, info in ipairs(self.m_info.term1Infos) do
        info.isSelect = false
    end
    self.m_info.multiTermGroup1SelectTermIndexList = {}
    local group1SelectTermIndexList = recoverState.multiTermGroup1SelectTermIndexList or {}
    for _, luaIndex in ipairs(group1SelectTermIndexList) do
        local info = self.m_info.term1Infos[luaIndex]
        if info ~= nil then
            info.isSelect = true
            table.insert(self.m_info.multiTermGroup1SelectTermIndexList, luaIndex)
        end
    end

    for _, info in ipairs(self.m_info.term2Infos) do
        info.isSelect = false
    end
    for _, info in ipairs(self.m_info.term3Infos) do
        info.isSelect = false
    end
    local recoverGroup2SelectTermData = recoverState.multiTermGroup2SelectTermData or {}
    local recoverGroupIndex = recoverGroup2SelectTermData.groupIndex or -1
    local recoverTermIndex = recoverGroup2SelectTermData.termIndex or -1
    local recoverInfo
    if recoverGroupIndex == 1 then
        recoverInfo = self.m_info.term2Infos[recoverTermIndex]
    elseif recoverGroupIndex == 2 then
        recoverInfo = self.m_info.term3Infos[recoverTermIndex]
    end
    if recoverInfo ~= nil then
        recoverInfo.isSelect = true
        self.m_info.multiTermGroup2SelectTermData.groupIndex = recoverGroupIndex
        self.m_info.multiTermGroup2SelectTermData.termIndex = recoverTermIndex
    else
        self.m_info.multiTermGroup2SelectTermData.groupIndex = -1
        self.m_info.multiTermGroup2SelectTermData.termIndex = -1
    end

    self:_RefreshMultiTermGroup1CompleteState()
    self:_RefreshMultiTermGroup2CompleteState()
    self:_RefreshTermResult1UI()
    self:_RefreshTermResult2UI()
    self:_RefreshGemResultState()

    local selectedMultiTermGroupIndex = recoverState.selectedMultiTermGroupIndex
    if selectedMultiTermGroupIndex == 1 or selectedMultiTermGroupIndex == 2 then

        self:_SetSelectMultiTermGroup(self.view["multiTermGroup" .. selectedMultiTermGroupIndex], true)
    end
end

GemCustomizationCtrl._TryRecoverInstructionBook = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil or recoverState.instructionBookArg == nil then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() then
        return
    end
    UIManager:Open(PanelId.InstructionBook, recoverState.instructionBookArg)
    recoverState.instructionBookArg = nil
end



GemCustomizationCtrl._GetSelectedMultiTermGroupIndex = HL.Method().Return(HL.Number) << function(self)
    local preSelectMultiTermGroup = self.m_uiRelate.preSelectMultiTermGroup
    if preSelectMultiTermGroup == self.view.multiTermGroup1 then
        return 1
    end
    if preSelectMultiTermGroup == self.view.multiTermGroup2 then
        return 2
    end
    return -1
end

GemCustomizationCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return {

        gameGroupId = self.m_info.gameGroupId,
        recoverState = self:_CollectRecoverState(),
    }
end

GemCustomizationCtrl._SetSelectMultiTermGroup = HL.Method(HL.Any, HL.Boolean) << function(self, multiTermGroup, isSelect)
    if self.m_uiRelate.preSelectMultiTermGroup == multiTermGroup then
        return
    end

    local termResult = self.m_uiRelate.multiTermGroupToTermResult[multiTermGroup]
    local preMultiGroup = self.m_uiRelate.preSelectMultiTermGroup
    if isSelect then
        if preMultiGroup then
            preMultiGroup.stateCtrl:SetState("Unselect")

            local preTermResult = self.m_uiRelate.multiTermGroupToTermResult[preMultiGroup]
            preTermResult.stateCtrl:SetState("Unselect")
        end

        self.m_uiRelate.preSelectMultiTermGroup = multiTermGroup
        multiTermGroup.stateCtrl:SetState("Select")
        termResult.stateCtrl:SetState("Select")
    else
        preMultiGroup = nil

        multiTermGroup.stateCtrl:SetState("Unselect")
        termResult.stateCtrl:SetState("Unselect")
    end
end



GemCustomizationCtrl._OnClickBtnClose = HL.Method() << function(self)

    local diff = false

    local termResult1Complete = #self.m_info.multiTermGroup1SelectTermIndexList >= self.m_info.multiTermGroup1MaxCount
    local termResult2Complete = self.m_info.multiTermGroup2SelectTermData.groupIndex > 0

    if termResult1Complete and termResult2Complete then
        local succ, groupData = GameInstance.player.worldEnergyPointSystem:TryGetWorldEnergyPointGroupRecord(self.m_info.gameGroupId)
        local preSelectTermsCount = (succ and groupData.hasSelectTerms) and groupData.selectTerms.Count or 0
        if preSelectTermsCount == 0 then
            diff = true
        else
            local preSelectTerms = (succ and groupData.hasSelectTerms) and groupData.selectTerms or {}
            local curSelectTerms = self:_GetCurSelectTerms()
            for _, termId in pairs(preSelectTerms) do
                if not lume.find(curSelectTerms, termId) then
                    diff = true
                    break
                end
            end
        end
    end

    if diff then
        self:Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_WEP_CLOSE_GEM_CUSTOMIZATION_WITH_DIFF_CONFIRM_HINT,
            onConfirm = function()
                PhaseManager:PopPhase(PHASE_ID)
            end
        })
    else
        PhaseManager:PopPhase(PHASE_ID)
    end
end

GemCustomizationCtrl._OnClickConfirmBtn = HL.Method() << function(self)
    local selectTerms = self:_GetCurSelectTerms()
    GameInstance.player.worldEnergyPointSystem:SendReqSetTerms(self.m_info.gameGroupId, selectTerms)
end

GemCustomizationCtrl._GetCurSelectTerms = HL.Method().Return(HL.Table) << function(self)
    local selectTerms = {}

    for _, selectIndex in pairs(self.m_info.multiTermGroup1SelectTermIndexList) do
        local termInfo = self.m_info.term1Infos[selectIndex]
        local id = termInfo.termId
        table.insert(selectTerms, id)
    end

    local groupIndex = self.m_info.multiTermGroup2SelectTermData.groupIndex
    if groupIndex == 1 then
        local termInfo = self.m_info.term2Infos[self.m_info.multiTermGroup2SelectTermData.termIndex]
        local id = termInfo.termId
        table.insert(selectTerms, id)
    elseif groupIndex == 2 then
        local termInfo = self.m_info.term3Infos[self.m_info.multiTermGroup2SelectTermData.termIndex]
        local id = termInfo.termId
        table.insert(selectTerms, id)
    end
    return selectTerms
end

GemCustomizationCtrl.OnWorldEnergyPointSelectTermsChanged = HL.Method(HL.Table) << function(self, args)
    local gameGroupId = unpack(args)
    if self.m_info.gameGroupId ~= gameGroupId then
        return
    end

    self:_OnClickBtnClose()
end

HL.Commit(GemCustomizationCtrl)
