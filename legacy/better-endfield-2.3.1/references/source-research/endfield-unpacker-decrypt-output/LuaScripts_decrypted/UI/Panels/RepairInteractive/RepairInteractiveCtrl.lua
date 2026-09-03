local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RepairInteractive

RepairInteractiveCtrl = HL.Class('RepairInteractiveCtrl', uiCtrl.UICtrl)






RepairInteractiveCtrl.s_messages = HL.StaticField(HL.Table) << {
}

local STATE_NAME = {
    SUBMIT_NO_COST = "submit_non_cost",
    SUBMIT_WITH_COST = "submit_with_cost",
    MINIGAME_NO_DESC = "minigame_no_desc",
    MINIGAME_WITH_DESC = "minigame_with_desc",
}






RepairInteractiveCtrl.m_submitItems = HL.Field(HL.Table)






RepairInteractiveCtrl.m_info = HL.Field(HL.Table)

RepairInteractiveCtrl.m_unlockType = HL.Field(HL.Any)

RepairInteractiveCtrl.m_onComplete = HL.Field(HL.Function)

RepairInteractiveCtrl.m_costItemCache = HL.Field(HL.Forward("UIListCache"))

RepairInteractiveCtrl.m_isClosing = HL.Field(HL.Boolean) << false



RepairInteractiveCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_isClosing = false

    self.m_unlockType = args.unlockType
    if self.m_unlockType == GEnums.InteractiveUnlockType.Submit then
        self:_OnCreateSubmit(args)
    elseif self.m_unlockType == GEnums.InteractiveUnlockType.MiniGame then
        self:_OnCreateMinigame(args)
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

RepairInteractiveCtrl.OnShow = HL.Override() << function(self)
    if not GameWorld.gameMechManager.mainCharFixBrain.isPanelExpectedShowing then
        RepairInteractiveCtrl.ForceCloseRepairInteractive()

        self:PlayAnimationOutAndClose()
    end
end

RepairInteractiveCtrl._ParsePanelInfoFromLockData = HL.Method(HL.Userdata).Return(HL.Table) << function(self, lockData)

    local panelInfo = {}

    if not string.isEmpty(lockData.panelIconSpritePath) then
        panelInfo.machineIconImage = lockData.panelIconSpritePath
    end

    if not lockData.panelDesc.isEmpty then
        panelInfo.descText = lockData.panelDesc:GetText()
    end

    if not lockData.panelTitle.isEmpty then
        panelInfo.machineNameText = lockData.panelTitle:GetText()
    end

    if not lockData.panelDescTitle.isEmpty then
        panelInfo.machineTitle = lockData.panelDescTitle:GetText()
    end

    if not lockData.panelBtnTxt.isEmpty then
        panelInfo.repairBtnText = lockData.panelBtnTxt:GetText()
    end

    if not lockData.panelDecoTxt.isEmpty then
        panelInfo.decoText = lockData.panelDecoTxt:GetText()
    end

    return panelInfo
end

RepairInteractiveCtrl._SwitchPanelDisplay = HL.Method(HL.Boolean, HL.Boolean) << function(self, hasDesc, hasCost)
    if self.m_unlockType == GEnums.InteractiveUnlockType.Submit then
        self.view.stateController:SetState(hasCost and STATE_NAME.SUBMIT_WITH_COST or STATE_NAME.SUBMIT_NO_COST)
    elseif self.m_unlockType == GEnums.InteractiveUnlockType.MiniGame then
        self.view.stateController:SetState(hasDesc and STATE_NAME.MINIGAME_WITH_DESC or STATE_NAME.MINIGAME_NO_DESC)
    end
end

RepairInteractiveCtrl._OnCreateMinigame = HL.Method(HL.Table) << function(self, args)
    self.view.closeButton.onClick:AddListener(function()
        self:_CloseRepairInteractive()
    end)
    self.view.repairBtn.onClick:AddListener(function()
        self:_OnClickRepair()
    end)

    self.m_info = args
    local lockData = self.m_info.lockData
    local hasDesc = false
    if lockData then
        local panelInfo = self:_ParsePanelInfoFromLockData(lockData)

        if not panelInfo.machineNameText then
            panelInfo.machineNameText = Language.LUA_UNLOCK_MINIGAME_TITLE
        end

        hasDesc = panelInfo.descText ~= nil

        self:_FillPanelInfo(panelInfo)
    else
        self:_FillPanelInfo({
            machineNameText =  Language.LUA_UNLOCK_MINIGAME_TITLE
        })
    end

    self:_SwitchPanelDisplay(hasDesc, false)
end

RepairInteractiveCtrl._OnCreateSubmit = HL.Method(HL.Table) << function(self, args)
    self.view.closeButton.onClick:AddListener(function()
        self:_CloseRepairInteractive()
    end)
    self.view.repairBtn.onClick:AddListener(function()
        self:_OnClickRepair()
    end)

    local lockData = args.lockData
    local submitId = lockData.submitId
    self.m_info = args

    local data = Tables.submitItem[submitId]

    if lockData.panelTextConfigured then
        local panelInfo = self:_ParsePanelInfoFromLockData(lockData)
        self:_FillPanelInfo(panelInfo)
    else
        local hasDesc = false
        if data ~= nil then
            hasDesc = data.desc and not data.desc.isEmpty
            self:_FillPanelInfo({
                machineNameText = data.name,
                descText = data.desc,
                machineIconImage = data.icon,
            })
        end
    end

    self.m_submitItems = {}
    for _, v in pairs(data.paramData) do
        if v.type == GEnums.SubmitTermType.Common then
            table.insert(self.m_submitItems, {
                id = v.paramList[0].valueStringList[0],
                count = v.paramList[1].valueIntList[0],
            })
        end
    end

    local items = self.m_submitItems
    local hasCost = #items ~= 0

    self:_SwitchPanelDisplay(true, hasCost)
    self:UpdateCount(true)
    self:_StartCoroutine(function()

        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_LARGER_UPDATE_INTERVAL)
            self:UpdateCount(false)
        end
    end)
end










RepairInteractiveCtrl._FillPanelInfo = HL.Method(HL.Table) << function(self, args)
    self.view.machineNameText.text = args.machineNameText
    self.view.descText.text = args.descText
    if args.machineIconImage then
        self.view.machineIconImage:LoadSprite(args.machineIconImage)
    end
    if args.machineTitle then
        self.view.machineTitleTxt.text = args.machineTitle
    end
    if args.repairBtnText then
        self.view.repairBtn.text = args.repairBtnText
    end
    if args.decoText then
        self.view.bgDecoTxt.text = args.decoText
        self.view.bottomDecoTxt.text = args.decoText
    end
end

RepairInteractiveCtrl._OnClickRepair = HL.Method() << function(self)
    if self.m_unlockType == GEnums.InteractiveUnlockType.Submit then
        if self.m_info ~= nil and self.m_info.callback ~= nil then
            self.m_info.callback(false)
            self.m_info = nil
            self:_CloseRepairInteractive()
        end
    elseif self.m_unlockType == GEnums.InteractiveUnlockType.MiniGame then

        if PhaseManager:CheckIsInTransition() then
            return
        end

        local finalArgs = {}
        finalArgs.callback = self.m_info.callback
        finalArgs.seamlessBlendToPerf = self.m_info.lockData.seamlessBlendToPerf
        local title = self.m_info.lockData.panelTitle
        local hasTitle = title and not title.isEmpty
        if hasTitle then
            finalArgs.title = title:GetText()
        end
        self.m_info = nil
        PhaseManager:ExitPhaseFast(PhaseId.RepairInteractive)
        PhaseManager:OpenPhaseFast(PhaseId.Puzzle, finalArgs)
    end
end

RepairInteractiveCtrl.UpdateCount = HL.Method(HL.Boolean) << function(self, isInit)
    local items = self.m_submitItems
    if isInit then
        self.m_costItemCache = UIUtils.genCellCache(self.view.costItem)
        self.m_costItemCache:Refresh(#items, function(cell, index)
            cell.item:InitItem(items[index], true)
            if DeviceInfo.usingController then
                cell.item:SetExtraInfo({
                    tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
                    tipsPosTransform = self.view.costItemList.transform,
                    isSideTips = true,
                })
            end
        end)
    end


    local isEnough = true
    self.m_costItemCache:Update(function(cell, index)
        local bundle = items[index]
        local count = Utils.getItemCount(bundle.id)
        local isLack = count < bundle.count
        cell.item:UpdateCountSimple(bundle.count, isLack)
        UIUtils.setItemStorageCountText(cell.storageNode, bundle.id, bundle.count)
        if isLack then
            isEnough = false
        end
    end)
    self.view.repairBtn.gameObject:SetActive(isEnough)
    self.view.notEnoughHint.gameObject:SetActive(not isEnough)
end

RepairInteractiveCtrl.ShowRepairInteractive = HL.StaticMethod(HL.Table) << function(args)
    local lockData, callback = unpack(args)

    local finalArgs = {}
    finalArgs.unlockType = GEnums.InteractiveUnlockType.Submit
    finalArgs.lockData = lockData
    finalArgs.callback = callback

    local openSuccess = PhaseManager:OpenPhase(PhaseId.RepairInteractive, finalArgs)
    if openSuccess == false then
        callback(true)
    end
end

RepairInteractiveCtrl.ShowRepairInteractiveByMinigame = HL.StaticMethod(HL.Table) << function(args)
    local callback, lockData = unpack(args)

    local finalArgs = {}
    finalArgs.unlockType = GEnums.InteractiveUnlockType.MiniGame
    finalArgs.callback = callback
    finalArgs.lockData = lockData

    local openSuccess = PhaseManager:OpenPhase(PhaseId.RepairInteractive, finalArgs)
    if openSuccess == false then
        callback(false)
    end
end

RepairInteractiveCtrl._CloseRepairInteractive = HL.Method() << function(self)
    if self.m_isClosing == true then
        return
    end
    self.m_isClosing = true
    local inTransition = PhaseManager:CheckIsInTransition()
    if inTransition then
        self:_StartCoroutine(function()
            coroutine.waitCondition(function()
                return self.m_isClosed or not PhaseManager:CheckIsInTransition()
            end)
            if self.m_isClosed then
                return
            end
            PhaseManager:PopPhase(PhaseId.RepairInteractive)
        end)
    else
        PhaseManager:PopPhase(PhaseId.RepairInteractive)
    end
end

RepairInteractiveCtrl.ForceCloseRepairInteractive = HL.StaticMethod() << function()
    if PhaseManager:GetTopPhaseId() == PhaseId.RepairInteractive then
        if UIManager:IsShow(PANEL_ID) then
            PhaseManager:ExitPhaseFast(PhaseId.RepairInteractive)
        end
    end
end

RepairInteractiveCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if self.m_info == nil then
        return nil
    end
    return {
        unlockType = self.m_unlockType,
        lockData = self.m_info.lockData,
        callback = self.m_info.callback,
    }
end

RepairInteractiveCtrl.OnClose = HL.Override() << function(self)
    if InputManagerInst.inChangingInputDevice then
        return
    end
    if self.m_unlockType == GEnums.InteractiveUnlockType.Submit then
        if self.m_info ~= nil and self.m_info.callback ~= nil then

            self.m_info.callback(true)
            self.m_info = nil
        end
    elseif self.m_unlockType == GEnums.InteractiveUnlockType.MiniGame then
        if self.m_info ~= nil and self.m_info.callback ~= nil then

            self.m_info.callback(false)
            self.m_info = nil
        end
    end
end



RepairInteractiveCtrl.OnHide = HL.Override() << function(self)
    self:_StartCoroutine(function()
        Notify(MessageConst.EXIT_LEVEL_HALF_SCREEN_PANEL_MODE)
        PhaseManager:ExitPhaseFast(PhaseId.RepairInteractive)
    end)
end

HL.Commit(RepairInteractiveCtrl)