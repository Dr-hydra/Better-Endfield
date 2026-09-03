local phaseBase = require_ex('Phase/Core/PhaseBase')
local FormulaCtrl = require_ex('UI/Panels/Formula/FormulaCtrl')
local PHASE_ID = PhaseId.FacMachine
PhaseFacMachine = HL.Class('PhaseFacMachine', phaseBase.PhaseBase)




PhaseFacMachine.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.FAC_OPEN_BUILDING_PANEL] = {'OpenBuildingPanel', false},
    [MessageConst.FAC_OPEN_BUILDING_PANEL_ALTER] = {'OpenBuildingPanelAlter', false},
    [MessageConst.FAC_OPEN_LOGISTIC_PANEL] = {'OpenLogisticPanel', false},
    [MessageConst.FAC_OPEN_NEAREST_BUILDING_PANEL] = {'OpenNearestBuildingPanel', false},
}




local ReservePanelIds = {
    PanelId.Joystick,
    PanelId.LevelCamera,
}

local NeedClosePanelIds = {
    PanelId.Formula,
    PanelId.FacMarkerManagePopup,
}

local QuestState = CS.Beyond.Gameplay.MissionSystem.QuestState

PhaseFacMachine.curPanelId = HL.Field(HL.Number) << -1

PhaseFacMachine.curPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseFacMachine.curNodeId = HL.Field(HL.Any)

PhaseFacMachine.m_panelArg = HL.Field(HL.Any)

PhaseFacMachine.m_panelBuildingDataId = HL.Field(HL.Any)

PhaseFacMachine.m_hidePanelKey = HL.Field(HL.Number) << -1

PhaseFacMachine.m_isDoingOut = HL.Field(HL.Boolean) << false

PhaseFacMachine.curUIInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.NodeUIInfo)

PhaseFacMachine.m_curBuildingPos = HL.Field(Vector3)

PhaseFacMachine.m_curBuildingRadius = HL.Field(HL.Number) << 0

PhaseFacMachine.m_isHalfPanel = HL.Field(HL.Boolean) << false

PhaseFacMachine.m_updateKey = HL.Field(HL.Number) << -1

PhaseFacMachine.m_tickInitialized = HL.Field(HL.Boolean) << false






PhaseFacMachine.OpenBuildingPanel = HL.StaticMethod(HL.Table) << function(args)
    local isForbid, forbidReason = Utils.isForbiddenWithReason(ForbidType.ForbidInteractFacBuilding)
    if isForbid then
        Notify(MessageConst.SHOW_RADIO, { GameAction.RadioRuntimeData(forbidReason.radioId, true, 0) })
        if (args.failCb ~= nil) then
            args.failCb()
        end
        return
    end

    local nodeId = args.nodeId
    local buildingSyncNode = FactoryUtils.getBuildingNodeHandler(nodeId)


    local isInvalidBuilding = FactoryUtils.isInvalidBuilding(nodeId)
    if isInvalidBuilding then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_INVALID_BUILDING_OP_DISABLE_TOAST)
        if (args.failCb ~= nil) then
            args.failCb()
        end
        return
    end


    if buildingSyncNode and not GameInstance.player.guide.skipAllGuide then
        if PhaseFacMachine.CheckPanelUnlockedState(buildingSyncNode.templateId, buildingSyncNode.instKey, args.customArg) == false then
            if (args.failCb ~= nil) then
                args.failCb()
            end
            return
        end
    end

    args.isBuilding = true
    Notify(MessageConst.FAC_BUILD_EXIT_CUR_MODE, true)
    if args.skipAnim then
        local ret = PhaseManager:OpenPhaseFast(PhaseId.FacMachine, args)
        if ret ~= true then
            if (args.failCb ~= nil) then
                args.failCb()
            end
        end
    else
        local ret = PhaseManager:OpenPhase(PhaseId.FacMachine, args)
        if ret ~= true then
            if (args.failCb ~= nil) then
                args.failCb()
            end
        end
    end
end

PhaseFacMachine.OpenBuildingPanelAlter = HL.StaticMethod(HL.Table) << function(args)
    local nodeId, customArg, buildingId = unpack(args)

    local buildingSyncNode = FactoryUtils.getBuildingNodeHandler(nodeId)

    if buildingSyncNode and not GameInstance.player.guide.skipAllGuide then
        if PhaseFacMachine.CheckPanelUnlockedState(buildingSyncNode.templateId, buildingSyncNode.instKey, customArg) == false then
            return
        end
    end

    PhaseFacMachine.OpenBuildingPanel({
        nodeId = nodeId,
        customArg = customArg,
        panelBuildingDataId = buildingId,
    })
end

PhaseFacMachine.OpenLogisticPanel = HL.StaticMethod(HL.Table) << function(args)
    local buildingSyncNode = FactoryUtils.getBuildingNodeHandler(args.nodeId)


    local isInvalidBuilding = FactoryUtils.isInvalidBuilding(args.nodeId)
    if isInvalidBuilding then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_INVALID_BUILDING_OP_DISABLE_TOAST)
        return
    end


    if buildingSyncNode and not GameInstance.player.guide.skipAllGuide then
        if PhaseFacMachine.CheckPanelUnlockedState(buildingSyncNode.templateId, buildingSyncNode.instKey, args.customArg) == false then
            return
        end
    end

    if buildingSyncNode then
        local templateId = buildingSyncNode.templateId
        local panelId = FacConst.FACTORY_NON_BUILDING_UI_MAP[templateId]
        if panelId == nil then
            logger.error("OpenLogisticPanel Failed, no panelId", templateId)
            return
        end
    end

    args.isBuilding = false
    PhaseManager:OpenPhase(PhaseId.FacMachine, args)
end

PhaseFacMachine.OpenNearestBuildingPanel = HL.StaticMethod(HL.Table) << function(arg)
    local buildingId, ignoreCull = unpack(arg)
    local isOpen, phase = PhaseManager:IsOpen(PhaseId.FacMachine)
    if isOpen then
        local buildingData = Tables.factoryBuildingTable[buildingId]
        local buildingType = buildingData.type
        local panelName = unpack(FacConst.FACTORY_BUILDING_UI_MAP[buildingType])
        local panelId = PanelId[panelName]
        if phase.curPanelId == panelId then
            return
        end
        PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
    end

    local succ, targetNodeId = FactoryUtils.findNearestBuilding(buildingId, ignoreCull)
    if not succ then
        logger.error("OpenNearestBuildingPanel 失败，找不到建筑", buildingId)
        return
    end

    PhaseFacMachine.OpenBuildingPanel({
        nodeId = targetNodeId,
        skipAnim = true,
        ignoreDist = true,
    })
end






PhaseFacMachine._OnInit = HL.Override() << function(self)
    PhaseFacMachine.Super._OnInit(self)

    local nodeId = self.arg.nodeId
    self.curNodeId = nodeId


    local chapterId = Utils.getCurrentChapterId()
    local slotId = CSFactoryUtil.GetBlueprintSlotId(chapterId, nodeId)
    if slotId > 0 then
        self.curPanelId = PanelId.FacPendingBuilding
        self.m_isHalfPanel = false
        self.m_panelArg = { slotId = CSFactoryUtil.GetBlueprintSlotId(chapterId, nodeId) }
        return
    end

    if self.arg.isBuilding then
        local panelBuildingDataId = self.arg.panelBuildingDataId
        local customArg = self.arg.customArg

        local buildingSyncNode = FactoryUtils.getBuildingNodeHandler(nodeId)
        local buildingId = buildingSyncNode.templateId
        local buildingData = Tables.factoryBuildingTable[buildingId]
        local buildingType
        if buildingSyncNode.nodeType == GEnums.FCNodeType.Special:GetHashCode() then
            buildingType = buildingData.type
        else
            _, buildingType = CSFactoryUtil.GetFacBuildingType(buildingSyncNode.nodeType)
        end

        if nodeId then

            local needRepairItems = nil
            local predefinedParam = buildingSyncNode.predefinedParam
            if  predefinedParam then
                local common = predefinedParam.common
                if common then
                    if common.needRepair then
                        needRepairItems = common.repairNeedItem
                    end
                end
            end
            if needRepairItems then
                local unRepair = buildingSyncNode.isNeedRepair
                if unRepair then
                    self.curPanelId = PanelId.FacRepairBuilding
                    self.m_isHalfPanel = true
                    self.m_panelArg = { needRepairItems = needRepairItems, nodeId = nodeId }
                    return
                end
            end
        end

        local panelBuildingType
        if panelBuildingDataId then

            panelBuildingType = Tables.factoryBuildingTable[panelBuildingDataId or buildingId].type
        else
            panelBuildingDataId = buildingId
            panelBuildingType = buildingType
        end
        local panelName, isHalfPanel = unpack(FacConst.FACTORY_BUILDING_UI_MAP[panelBuildingType])
        local panelId = PanelId[panelName]
        self.curPanelId = panelId
        self.m_isHalfPanel = isHalfPanel


        self.curNodeId = nodeId
        self.curUIInfo = GameInstance.remoteFactoryManager.uiInfoProvider:GetBuildingUIInfo(buildingType)
        if self.curUIInfo then
            if not self.arg.ignoreDist then
                self.m_curBuildingPos = CSFactoryUtil.GetBuildingModelPosition(buildingSyncNode)
            end

            local width = buildingData.range.width
            local depth = buildingData.range.depth
            self.m_curBuildingRadius = math.sqrt(width * width + depth * depth) / 2

            if customArg and customArg.subIndex then
                self.curUIInfo:SetupChapterIdAndNodeId(Utils.getCurrentChapterId(), nodeId, customArg.subIndex)
            else
                self.curUIInfo:SetupChapterIdAndNodeId(Utils.getCurrentChapterId(), nodeId)
            end
            if not customArg then
                customArg = { uiInfo = self.curUIInfo }
            else
                customArg.uiInfo = self.curUIInfo
            end
        else

            if not customArg then
                customArg = { nodeId = nodeId }
            else
                customArg.nodeId = nodeId
            end
        end

        self.m_panelBuildingDataId = panelBuildingDataId
        self.m_panelArg = customArg
    else
        self.m_panelArg = self:_ParseNonBuildingPanelArgs(self.arg)
    end

    if self.curPanelId == PanelId.FacMixPool and self.arg and self.arg.facMixPoolRecoverState ~= nil then
        self.m_panelArg = self.m_panelArg or {}
        self.m_panelArg.recoverState = self.arg.facMixPoolRecoverState
    end

    self:_StartFbAndTickUpdate()
end

PhaseFacMachine._ParseNonBuildingPanelArgs = HL.Method(HL.Table).Return(HL.Table) << function(self, args)
    local result = {}
    if args == nil then
        return result
    end

    local nodeId = args.nodeId
    local nodeHandler = FactoryUtils.getBuildingNodeHandler(nodeId)
    if nodeHandler == nil then
        return result
    end

    local templateId = nodeHandler.templateId
    local panelId = FacConst.FACTORY_NON_BUILDING_UI_MAP[templateId]
    if panelId == nil then
        return result
    end

    local nodeType = nodeHandler.nodeType
    if nodeType == GEnums.FCNodeType.BoxConveyor:GetHashCode() then

        self.curUIInfo = GameInstance.remoteFactoryManager.uiInfoProvider:GetConveyorUIInfo()
        self.curUIInfo:SetupChapterIdAndNodeId(Utils.getCurrentChapterId(), nodeId, args.index)
    elseif nodeType == GEnums.FCNodeType.BoxRouterM1:GetHashCode() or nodeType == GEnums.FCNodeType.BoxBridge:GetHashCode() then

        self.curUIInfo = GameInstance.remoteFactoryManager.uiInfoProvider:GetLogisticUIInfo(templateId)
        self.curUIInfo:SetupChapterIdAndNodeId(Utils.getCurrentChapterId(), nodeId)
    elseif nodeType == GEnums.FCNodeType.FluidConveyor:GetHashCode() then

        self.curUIInfo = GameInstance.remoteFactoryManager.uiInfoProvider:GetPipeUIInfo()
        self.curUIInfo:SetupChapterIdAndNodeId(Utils.getCurrentChapterId(), nodeId)
    elseif nodeType == GEnums.FCNodeType.FluidRouterM1:GetHashCode() or nodeType == GEnums.FCNodeType.FluidBridge:GetHashCode() then

        self.curUIInfo = GameInstance.remoteFactoryManager.uiInfoProvider:GetFluidUnitUIInfo(templateId)
        self.curUIInfo:SetupChapterIdAndNodeId(Utils.getCurrentChapterId(), nodeId)
    elseif nodeType == GEnums.FCNodeType.BoxValve:GetHashCode() then

        self.curUIInfo = GameInstance.remoteFactoryManager.uiInfoProvider:GetBoxValveUIInfo()
        self.curUIInfo:SetupChapterIdAndNodeId(Utils.getCurrentChapterId(), nodeId)
    elseif nodeType == GEnums.FCNodeType.FluidValve:GetHashCode() then

        self.curUIInfo = GameInstance.remoteFactoryManager.uiInfoProvider:GetFluidValveUIInfo()
        self.curUIInfo:SetupChapterIdAndNodeId(Utils.getCurrentChapterId(), nodeId)
    end

    self.curPanelId = PanelId[panelId]
    self.m_curBuildingPos = GameUtil.playerPos
    self.m_curBuildingRadius = 1
    self.m_isHalfPanel = false

    result.uiInfo = self.curUIInfo
    result.index = args.index
    return result
end

PhaseFacMachine._StartFbAndTickUpdate = HL.Method() << function(self)
    if self.curUIInfo and not self.m_tickInitialized and not self.m_isDoingOut then
        local nodeValid = self.curUIInfo.nodeHandler and self.curUIInfo.nodeHandler.valid
        if nodeValid then

            self.curUIInfo.sender:Message_HSFB(Utils.getCurrentChapterId(), false, { self.curNodeId })
            self.curUIInfo:Update(true)
        else
            self:_StartCoroutine(function()
                coroutine.waitCondition(function()
                    return PhaseManager.m_curState == Const.PhaseState.Idle or PhaseManager:GetTopPhaseId() ~= PHASE_ID
                end)
                if PhaseManager:GetTopPhaseId() == PHASE_ID then
                    self.m_curBuildingPos = nil
                    PhaseManager:PopPhase(PHASE_ID)
                    Notify(MessageConst.FAC_UPDATE_INTERACT_OPTION, true)
                end
            end)
            return
        end
        self.m_updateKey = LuaUpdate:Add("Tick", function()
            self:_Update()
        end)
        self.m_tickInitialized = true
    end
end

PhaseFacMachine._StopFbAndTickUpdate = HL.Method() << function(self)
    if self.curUIInfo and self.m_tickInitialized then
        self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)

        self.curUIInfo.sender:Message_HSFB(Utils.getCurrentChapterId(), true, { self.curNodeId })
        self.m_tickInitialized = false
    end
end






PhaseFacMachine.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    if transitionType == PhaseConst.EPhaseState.TransitionIn then
        if self.curPanelId and not fastMode and self.curPanelId ~= PanelId.FacMachineCrafter then

            UIManager:PreloadPanelAsset(self.curPanelId, PHASE_ID)
        end
        if self.m_isHalfPanel and anotherPhaseId == PhaseId.Level then
            Notify(MessageConst.SET_PHASE_LEVEL_TRANSITION_RESERVE_PANELS, ReservePanelIds)
        end
    end
end

PhaseFacMachine._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.m_panelArg.uiInfo ~= nil and string.isEmpty(self.m_panelArg.uiInfo.nodeHandler.templateId) then
        PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
        return
    end

    self.m_isDoingOut = false
    local reservePanelIds = self.m_isHalfPanel and ReservePanelIds or nil
    self.m_hidePanelKey = UIManager:ClearScreen(reservePanelIds)

    CS.Beyond.Gameplay.Audio.AudioRemoteFactoryBridge.OnUnitUiOpened(self.curNodeId, self.m_panelBuildingDataId)
    self.curPanelItem = self:CreatePhasePanelItem(self.curPanelId, self.m_panelArg)
    self:_TryRecoverInventoryArea()
    self:_TryRecoverFacSourceDetails()
    self:_TryRecoverFriendRequest()
    self:_TryRecoverFormula()
    self:_TryRecoverInstructionBook()

    if self.m_isHalfPanel then
        Notify(MessageConst.ENTER_LEVEL_HALF_SCREEN_PANEL_MODE)
    end

    if self.m_panelBuildingDataId then
        CS.Beyond.Gameplay.Conditions.OnBuildingPanelOpen.Trigger(self.m_panelBuildingDataId, false)
    end
end

PhaseFacMachine._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.m_isDoingOut = true
    Notify(MessageConst.HIDE_ITEM_TIPS)
    Notify(MessageConst.EXIT_LEVEL_HALF_SCREEN_PANEL_MODE)
    Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
    Notify(MessageConst.RESET_DROP_HIGHLIGHT)
    for _, panelId in pairs(NeedClosePanelIds) do
        if UIManager:IsOpen(panelId) then
            UIManager:Close(panelId)
        end
    end
    CS.Beyond.Gameplay.Audio.AudioRemoteFactoryBridge.OnUnitUiClosed(self.curNodeId, self.m_panelBuildingDataId)
end

PhaseFacMachine._DoPhaseTransitionOutAfterItems = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.m_hidePanelKey = UIManager:RecoverScreen(self.m_hidePanelKey)
end

PhaseFacMachine._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if args.anotherPhaseId == PhaseId.FacBuildListSelect and self.curPanelItem ~= nil and self.curPanelId ~= PanelId.FacHUB then
        self.curPanelItem.uiCtrl:Hide()
    end
end

PhaseFacMachine._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if args.anotherPhaseId == PhaseId.FacBuildListSelect and self.curPanelItem ~= nil and self.curPanelId ~= PanelId.FacHUB then
        self.curPanelItem.uiCtrl:Show()
    end
    if self.curPanelId == PanelId.FacPendingBuilding then

        self.curPanelItem.uiCtrl:RefreshList(true)
    end
end

PhaseFacMachine.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    arg.inventoryAreaState = self:_GetInventoryAreaRecoverStateArg()
    arg.facMixPoolRecoverState = self:_GetFacMixPoolRecoverStateArg()
    arg.needRecoverFriendRequest = self:_ShouldRecoverFriendRequest()
    arg.needRecoverFacSourceDetails = self:_ShouldRecoverFacSourceDetails()
    arg.formulaArg = self:_GetFormulaRecoverArg()
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if self.curPanelId == PanelId.FacHUB and isOpen and instructionCtrl:IsShow() and instructionCtrl.id == "fac_hub_offline" and PhaseManager:GetTopPhaseId() == PHASE_ID then
        arg.instructionBookArg = {
            id = instructionCtrl.id,
        }
    else
        arg.instructionBookArg = nil
    end
    return arg
end

PhaseFacMachine._GetFormulaRecoverArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return nil
    end
    local isOpen, formulaCtrl = UIManager:IsOpen(PanelId.Formula)
    if not isOpen or not formulaCtrl:IsShow() then
        return nil
    end
    return formulaCtrl:GetRecoverStateArg(self.curNodeId, self.m_panelBuildingDataId)
end

PhaseFacMachine._GetBuildingCommon = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local uiCtrl = self.curPanelItem and self.curPanelItem.uiCtrl
    local buildingCommon = uiCtrl and uiCtrl.view and uiCtrl.view.buildingCommon
    if buildingCommon == nil or buildingCommon.nodeId ~= self.curNodeId then
        return nil
    end
    return buildingCommon
end

PhaseFacMachine._ShouldRecoverFriendRequest = HL.Method().Return(HL.Boolean) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return false
    end
    local buildingCommon = self:_GetBuildingCommon()
    if buildingCommon == nil then
        return false
    end
    local isOpen, friendRequestCtrl = UIManager:IsOpen(PanelId.FriendRequest)
    if not isOpen or not friendRequestCtrl:IsShow() then
        return false
    end
    return friendRequestCtrl:IsShareMode()
end

PhaseFacMachine._TryRecoverFriendRequest = HL.Method() << function(self)
    if self.arg == nil or self.arg.needRecoverFriendRequest ~= true then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local isOpen, friendRequestCtrl = UIManager:IsOpen(PanelId.FriendRequest)
    if isOpen and friendRequestCtrl:IsShow() then
        self.arg.needRecoverFriendRequest = nil
        return
    end
    local buildingCommon = self:_GetBuildingCommon()
    if buildingCommon ~= nil then
        buildingCommon:ShareBuilding()
    end
    self.arg.needRecoverFriendRequest = nil
end

PhaseFacMachine._ShouldRecoverFacSourceDetails = HL.Method().Return(HL.Boolean) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return false
    end
    local buildingCommon = self:_GetBuildingCommon()
    if buildingCommon == nil then
        return false
    end
    local isOpen, facSourceDetailsCtrl = UIManager:IsOpen(PanelId.FacSourceDetails)
    if not isOpen or not facSourceDetailsCtrl:IsShow() then
        return false
    end
    return facSourceDetailsCtrl:GetTargetNodeId() == self.curNodeId
end

PhaseFacMachine._TryRecoverFacSourceDetails = HL.Method() << function(self)
    if self.arg == nil or self.arg.needRecoverFacSourceDetails ~= true then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local isOpen, facSourceDetailsCtrl = UIManager:IsOpen(PanelId.FacSourceDetails)
    if isOpen and facSourceDetailsCtrl:IsShow() and facSourceDetailsCtrl:GetTargetNodeId() == self.curNodeId then
        self.arg.needRecoverFacSourceDetails = nil
        return
    end
    local buildingCommon = self:_GetBuildingCommon()
    if buildingCommon ~= nil then
        buildingCommon:ShowBuildingSource()
    end
    self.arg.needRecoverFacSourceDetails = nil
end

PhaseFacMachine._GetInventoryAreaRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local uiCtrl = self.curPanelItem and self.curPanelItem.uiCtrl
    local inventoryArea = uiCtrl and uiCtrl.view and uiCtrl.view.inventoryArea
    if inventoryArea == nil then
        return nil
    end
    return inventoryArea:GetRecoverStateArg()
end

PhaseFacMachine._GetFacMixPoolRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if self.curPanelId ~= PanelId.FacMixPool then
        return nil
    end
    local uiCtrl = self.curPanelItem and self.curPanelItem.uiCtrl
    if uiCtrl == nil or not HL.TryGet(uiCtrl, "GetRecoverStateArg") then
        return nil
    end
    return uiCtrl:GetRecoverStateArg()
end

PhaseFacMachine._TryRecoverInventoryArea = HL.Method() << function(self)
    local recoverState = self.arg and self.arg.inventoryAreaState
    if recoverState == nil then
        return
    end
    local uiCtrl = self.curPanelItem and self.curPanelItem.uiCtrl
    local inventoryArea = uiCtrl and uiCtrl.view and uiCtrl.view.inventoryArea
    if inventoryArea == nil then
        self.arg.inventoryAreaState = nil
        return
    end
    inventoryArea:TryRecoverState(recoverState)
    self.arg.inventoryAreaState = nil
end

PhaseFacMachine._TryRecoverFormula = HL.Method() << function(self)
    local formulaArg = self.arg and self.arg.formulaArg
    if formulaArg == nil then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local isOpen, formulaCtrl = UIManager:IsOpen(PanelId.Formula)
    if isOpen and formulaCtrl:IsShow() then
        self.arg.formulaArg = nil
        return
    end
    local uiCtrl = self.curPanelItem and self.curPanelItem.uiCtrl
    if uiCtrl and uiCtrl.view then
        formulaArg.belongingCanvasGroup = uiCtrl.view.canvasGroup
    end
    Notify(MessageConst.FAC_SHOW_FORMULA, formulaArg)
    self.arg.formulaArg = nil
end

PhaseFacMachine._TryRecoverInstructionBook = HL.Method() << function(self)
    if self.curPanelId ~= PanelId.FacHUB or self.arg == nil or self.arg.instructionBookArg == nil then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() then
        return
    end
    UIManager:Open(PanelId.InstructionBook, self.arg.instructionBookArg)
    self.arg.instructionBookArg = nil
end






PhaseFacMachine._OnActivated = HL.Override() << function(self)
    self:_StartFbAndTickUpdate()
    if self.m_panelBuildingDataId and not self.m_isDoingOut then
        CS.Beyond.Gameplay.Conditions.OnBuildingPanelOpen.Trigger(self.m_panelBuildingDataId, true)
    end
    if self.curNodeId and not self.m_isDoingOut then
        local isOthers, isPreset = FactoryUtils.getBuildingSocialSourceInfo(self.curNodeId)
        if isOthers and not isPreset then
            CS.Beyond.Gameplay.Conditions.OnOtherPlayerSocialBuildingPanelOpen.Trigger(self.m_panelBuildingDataId)
        end
    end
end

PhaseFacMachine._OnDeActivated = HL.Override() << function(self)
    self:_StopFbAndTickUpdate()
end

PhaseFacMachine._OnDestroy = HL.Override() << function(self)
    self:_StopFbAndTickUpdate()
    if self.curPanelItem ~= nil then
        self:RemovePhasePanelItem(self.curPanelItem)
    end
end

PhaseFacMachine._Update = HL.Method() << function(self)
    local nodeValid = false
    if self.curUIInfo then
        nodeValid = self.curUIInfo.nodeHandler and self.curUIInfo.nodeHandler.valid
        if nodeValid then
            self.curUIInfo:Update()
        end
    end

    local needExit = not nodeValid

    if not needExit then
        if self.m_curBuildingPos and not LuaSystemManager.factory.inTopView then
            local playerPos = GameUtil.playerPos
            local dist = (playerPos - self.m_curBuildingPos).magnitude
            if dist > self.m_curBuildingRadius + FacConst.BUILDING_PANEL_AUTO_CLOSE_RANGE then
                needExit = true
            end
        end
    end

    if needExit then
        if PhaseManager.m_curState == Const.PhaseState.Idle and PhaseManager:GetTopPhaseId() == PhaseId.FacMachine then
            self.m_curBuildingPos = nil
            PhaseManager:PopPhase(PhaseId.FacMachine)
            Notify(MessageConst.FAC_UPDATE_INTERACT_OPTION, true)
        end
    end
end






PhaseFacMachine.CheckPanelUnlockedState = HL.StaticMethod(HL.String, HL.String, HL.Opt(HL.Table)).Return(HL.Boolean) << function(buildingId, instKey, args)
    local subIndex = args and args.subIndex or -1
    return not CSFactoryUtil.CheckIsBuildingInteractLocked(buildingId, instKey, true, subIndex)
end




HL.Commit(PhaseFacMachine)
