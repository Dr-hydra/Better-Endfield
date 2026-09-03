
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local EOperationType = CS.Beyond.Gameplay.EOperationType
local FertilizeIncreaseType = GEnums.FertilizeIncreaseType
local UnlockSystemType = GEnums.UnlockSystemType
local PANEL_ID = PanelId.FacCultivate
FacCultivateCtrl = HL.Class('FacCultivateCtrl', uiCtrl.UICtrl)






FacCultivateCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_CROP_STEP_CHANGE] = '_OnStepChange',
    [MessageConst.ON_CROP_OPERATION_FAILED] = '_OnOperationFailed',
    [MessageConst.ON_CROP_OPERATION_FAILED_FORCE_CLOSE] = '_OnOperationFailedForce',
    [MessageConst.ON_CROP_EXTRA_OPERATION_FINISH] = '_OnExtraOperationFinish',
    [MessageConst.On_FERTILIZE_PANEL_DO_OPERATION] = '_OnUpdateIfDoOperation'
}
FacCultivateCtrl.m_nodeId = HL.Field(HL.Any)

FacCultivateCtrl.m_soilNode = HL.Field(CS.Beyond.Gameplay.FacSoilSystem.SoilNode)

FacCultivateCtrl.m_soilShow = HL.Field(CS.Beyond.Gameplay.SoilShow)

FacCultivateCtrl.m_soilComp = HL.Field(CS.Beyond.Gameplay.Core.IntFacSoilComponent)

FacCultivateCtrl.m_soilCfg = HL.Field(CS.Beyond.Cfg.PlantingStepData)

FacCultivateCtrl.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_Soil)

FacCultivateCtrl.m_enumMap = HL.Field(HL.Table)

FacCultivateCtrl.m_enumList = HL.Field(HL.Table)

FacCultivateCtrl.m_enumReverseLookup = HL.Field(HL.Table)

FacCultivateCtrl.m_progressImgMap = HL.Field(HL.Table)

FacCultivateCtrl.m_partNodeList = HL.Field(HL.Table)

FacCultivateCtrl.m_getCell = HL.Field(HL.Function)

FacCultivateCtrl.m_showInfo = HL.Field(HL.Boolean) << false

FacCultivateCtrl.m_currentStep = HL.Field(HL.Number) << -1

FacCultivateCtrl.m_doTweenAnim = HL.Field(HL.Any)

FacCultivateCtrl.m_openedInProgress = HL.Field(HL.Boolean) << false

FacCultivateCtrl.m_waitingExitPhase = HL.Field(HL.Boolean) << false

FacCultivateCtrl.s_key = HL.StaticField(HL.Number) << -1

FacCultivateCtrl.m_inNaviState = HL.Field(HL.Boolean) << false


FacCultivateCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    self.m_uiInfo = arg.uiInfo
    local nodeId = self.m_uiInfo.nodeId
    self.m_nodeId = nodeId
    self.m_soilNode = GameInstance.player.facSoilSystem:GetSoilNodeInCurrentRegion(nodeId)
    self.m_soilShow = GameInstance.player.facSoilSystem:GetSoilShow(nodeId)
    self.m_soilCfg = self.m_soilShow:GetCurPlantConfig()
    self.m_soilComp = self.m_soilShow.facSoilComponent

    self.m_enumMap = {
        [GEnums.PlantingStepType.Reclaim] = {
            icon = "icon_fac_plant_1"
        },
        [GEnums.PlantingStepType.Water] = {
            icon = "icon_fac_plant_2"
        },
        [GEnums.PlantingStepType.Grow] = {
            icon = "icon_fac_plant_3"
        },
        [GEnums.PlantingStepType.Harvest] = {
            icon = "icon_fac_plant_4"
        },
    }

    self.m_progressImgMap = {
        [GEnums.PlantingStepType.Reclaim] = {
            icon = "deco_bg_fac_cultivate_1"
        },
        [GEnums.PlantingStepType.Water] = {
            icon = "deco_bg_fac_cultivate_2"
        },
        [GEnums.PlantingStepType.Grow] = {
            icon = "deco_bg_fac_cultivate_3"
        },
        [GEnums.PlantingStepType.Harvest] = {
            icon = "deco_bg_fac_cultivate_4"
        },
        [EOperationType.Fertilize] = {
            icon = "deco_bg_fac_cultivate_5"
        },
    }
    self.m_partNodeList = {
        [1] = "part1",
        [2] = "part2",
        [3] = "part3",
        [4] = "part4",
        [5] = "part5",
        [6] = "part6",
        [7] = "part7",
        [8] = "part8",
    }
    self.m_enumList = {
        [1] = GEnums.PlantingStepType.Reclaim,
        [2] = GEnums.PlantingStepType.Water,
        [3] = GEnums.PlantingStepType.Grow,
        [4] = GEnums.PlantingStepType.Harvest,
    }

    self.m_enumReverseLookup = {}
    for k, v in pairs(self.m_enumList) do
        self.m_enumReverseLookup[v] = k
    end

    self.view.buildingCommon:InitBuildingCommon(self.m_uiInfo, {
        onStateChanged = function(state)
        end
    })
    self:_SetInfoVisible(false)
    self.view.showStepInfoBtn.onClick:AddListener(function()
        AudioAdapter.PostEvent("Au_UI_Toast_Common_Small_Open")
        self:_SetInfoVisible(true)
    end)
    local t = self.view.nodeContainer
    local childCount = t.childCount
    local curStep = self.m_soilNode.soilStage
    local maxCnt = self.m_soilCfg.plantingSteps.Count
    for index, key in ipairs(self.m_partNodeList) do
        local i = index - 1
        local curChild = t[key]
        local visible = (i < maxCnt)
        local currentActive = (i == curStep)
        local currentHasDone = (i < curStep)
        local currentNotStart = (i > curStep)
        if curChild ~= nil then
            if i < maxCnt then
                local cfg = self.m_enumMap[self.m_soilCfg.plantingSteps[i].plantingStepType]
                if cfg ~= nil then
                    curChild.icon:LoadSprite(UIConst.UI_SPRITE_CROP, cfg.icon)
                end
            end
            curChild.gameObject:SetActiveIfNecessary(visible)
        end
    end
    local hasFound, curStepConstCfg = Tables.plantingStepConstTable:TryGetValue(self.m_soilCfg.id)
    local rewardId = self.m_soilNode.fertilizeIncreaseType == FertilizeIncreaseType.None and self.m_soilCfg.rewardId or self.m_soilCfg.newRewardId
    local rewardValid, rewardCfg = Tables.rewardSoilTable:TryGetValue(rewardId)
    self.m_openedInProgress = self.m_soilComp.isNatureResourceBusy
    self.view.mainPanel.gameObject:SetActiveIfNecessary(not self.m_soilComp.isNatureResourceBusy)
    self:_SetCancelBtn(self.m_soilComp.isNatureResourceBusy)
    self.view.bottomNode.gameObject:SetActiveIfNecessary(self.m_soilComp.isNatureResourceBusy)
    self.view.btnCommonYellow.onClick:AddListener(function()
        self:_UpdateProgressNode()
        self:_DoOperation()
    end)

    self.view.closeInfoBtn.onClick:AddListener(function()
        AudioAdapter.PostEvent("Au_UI_Toast_Common_Small_Close")
        self.view.closeInfoBtn.gameObject:SetActiveIfNecessary(false)
        self.view.stepContainer.gameObject:SetActiveIfNecessary(true)
        self.view.stepInfoContainer.gameObject:SetActiveIfNecessary(false)
    end)
    self.view.bottomNodeBtn.onClick:AddListener(function()


    end)
    self.view.itemContent.onClick:AddListener(function()
        self:_KillTween()
        self.m_soilShow:DoInterrupt()
    end)
    local showItemId = rewardCfg.itemBundles[0].id
    local showItemNum = rewardCfg.itemBundles[0].count
    self.view.item:InitItem({
        id = showItemId,
        count = showItemNum
    }, true)
    if DeviceInfo.usingController then





    end
    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateStepCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)
    self.view.scrollList:UpdateCount(#self.m_enumList)

    self:_UpdateProgressNode()

    self.view.facProgressNode:InitFacProgressNode(1, 1)
    if self.m_soilComp.isNatureResourceBusy then
        self:_PlayDoTweenAnim()
    end

    self:_UpdateShow()
    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_LARGER_UPDATE_INTERVAL)
            self:_UpdateShow()
        end
    end)

    if DeviceInfo.usingController then
        self:_InitController()
    end
end

FacCultivateCtrl._UpdateProgressNode = HL.Method() << function(self)

    local curStep = self.m_soilNode.soilStage
    local curStepCfg = self.m_soilCfg.plantingSteps[curStep]
    local hasFound, stepConstCfg = Tables.plantingStepConstTable:TryGetValue(curStepCfg.plantingStepType)
    local imgType = (curStepCfg.plantingStepType ~= GEnums.PlantingStepType.Grow) and curStepCfg.plantingStepType or EOperationType.Fertilize
    self.view.progressImg:LoadSprite(UIConst.UI_SPRITE_CROP, self.m_progressImgMap[imgType].icon)
    if curStepCfg.plantingStepType == GEnums.PlantingStepType.Grow then
        self.view.bottomProgressText.text = Language.LUA_FAC_FERTILIZATION_PROGRESS
    else
        self.view.bottomProgressText.text = stepConstCfg.progressText
    end
end










FacCultivateCtrl._RefreshBtn = HL.Method() << function(self)











    self:_SetCancelBtn(self.m_soilComp.isNatureResourceBusy)
end

FacCultivateCtrl._SetInfoVisible = HL.Method(HL.Boolean) << function(self, infoVisible)
    self.view.closeInfoBtn.gameObject:SetActiveIfNecessary(infoVisible)
    self.view.stepContainer.gameObject:SetActiveIfNecessary(not infoVisible)
    self.view.stepInfoContainer.gameObject:SetActiveIfNecessary(infoVisible)
    if DeviceInfo.usingController then
        if infoVisible then
            InputManagerInst:ToggleGroup(self.view.contentInfoStateInputBindingGroupMonoTarget.groupId,true)
            Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
                panelId = PANEL_ID,
                isGroup = true,
                id = self.view.contentInfoStateInputBindingGroupMonoTarget.groupId,
                hintPlaceholder = self.view.controllerHintPlaceholder,
                rectTransform = self.view.stepInfoContainer,
                noHighlight = true,
            })
        else
            Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.contentInfoStateInputBindingGroupMonoTarget.groupId)
        end
        self.view.controllerHintPlaceholder.gameObject:SetActive(not infoVisible)
    end
end

FacCultivateCtrl._DoOperation = HL.Method() << function(self)
    if self.m_soilComp.isNatureResourceBusy then

        self:_ExitAnyWay()
        return
    end
    local curStep = self.m_soilNode.soilStage
    local stepType = self.m_soilCfg.plantingSteps[curStep].plantingStepType
    if stepType == GEnums.PlantingStepType.Grow then
        if not self.m_soilNode:CanFertilize() then
            self:_ExitAnyWay()
            return

        end
    else
        self.view.mainPanel.gameObject:SetActiveIfNecessary(false)
        self:_SetCancelBtn(true)
        self.view.bottomNode.gameObject:SetActiveIfNecessary(true)
        self:_PlayDoTweenAnim()
    end

    self.m_soilShow:DoOperation(false)
end

FacCultivateCtrl._OnStepChange = HL.Method(HL.Table) << function(self, args)
    local nodeId = unpack(args)
    if self.m_soilComp.soilNodeId ~= nodeId then
        return
    end
    self:_KillTween()
    local curStep = self.m_soilNode.soilStage
    local curStepCfg = self.m_soilCfg.plantingSteps[curStep]

    local hasFound, stepConstCfg = Tables.plantingStepConstTable:TryGetValue(curStepCfg.plantingStepType)

    if curStepCfg.plantingStepType == GEnums.PlantingStepType.Harvest then
        return
    end

    self:_ExitAnyWay()
end



FacCultivateCtrl.ExtractHotSwitchRuntimeState = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    self:_KillTween()
    return nil
end

FacCultivateCtrl._KillTween = HL.Method() << function(self)
    if self.m_doTweenAnim then
        self.m_doTweenAnim:Kill()
    end
    self.m_doTweenAnim = nil
end

FacCultivateCtrl._SetCancelBtn = HL.Method(HL.Boolean) << function(self, showBtn)
    self.view.itemContent.gameObject:SetActiveIfNecessary(showBtn)
end

FacCultivateCtrl._OnOperationFailed = HL.Method(HL.Any) << function(self, arg)
    local nodeId = unpack(arg)
    if self.m_nodeId == nodeId then
        self:_ExitAnyWay()
    end
end

FacCultivateCtrl._OnExtraOperationFinish = HL.Method(HL.Any) << function(self, arg)
    local nodeId = unpack(arg)
    if self.m_nodeId == nodeId then
        self:_ExitAnyWay()
    end
end

FacCultivateCtrl._OnOperationFailedForce = HL.Method(HL.Any) << function(self, arg)
    self:_ExitAnyWay()
end

FacCultivateCtrl._ExitAnyWay = HL.Method() << function(self)
    if self.view.bottomNode.gameObject.activeInHierarchy then
        self.view.progressAnim:PlayOutAnimation(function()
            self:_ClosePanel()
        end)
    else
        self.view.contentAnim:PlayOutAnimation(function()
            self:_ClosePanel()
        end)
    end
end

FacCultivateCtrl._ClosePanel = HL.Method() << function(self)

    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.contentInfoStateInputBindingGroupMonoTarget.groupId)
    local isOpen, sideMenuCtrl = UIManager:IsOpen(PanelId.ControllerSideMenu)
    if isOpen then
        sideMenuCtrl:PlayAnimationOutAndClose()
    end
    if not PhaseManager:IsOpen(PhaseId.FacMachine) then
        UIManager:Close(PANEL_ID)
        return
    end
    if not PhaseManager:CheckIsInTransition() then
        PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
        return
    end
    if self.m_waitingExitPhase then
        return
    end
    self.m_waitingExitPhase = true
    self:_StartCoroutine(function()
        coroutine.waitCondition(function()
            return self.m_isClosed or not PhaseManager:CheckIsInTransition()
        end)
        self.m_waitingExitPhase = false
        if self.m_isClosed then
            return
        end
        if PhaseManager:IsOpen(PhaseId.FacMachine) then
            PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
        else
            UIManager:Close(PANEL_ID)
        end
    end)
end

FacCultivateCtrl._UpdateShow = HL.Method() << function(self)
    if self.m_nodeId == nil then
        self:_ExitAnyWay()
        return
    end
    if CSFactoryUtil.CheckMinaCharOperateNodeBlockByTeammate(self.m_nodeId) then
        self:_ExitAnyWay()
        return
    end


    if self.m_openedInProgress and not self.m_soilComp.isNatureResourceBusy then
        self:_ExitAnyWay()
        return
    end
    local t = self.view.nodeContainer
    local childCount = t.childCount
    local curStep = self.m_soilNode.soilStage
    local curStepCfg = self.m_soilCfg.plantingSteps[curStep]

    self.view.facProgressNode:UpdateProgress(1 - self.m_soilShow:GetProgress())
    if curStepCfg.plantingStepType == GEnums.PlantingStepType.Grow then
        self.view.countTimeText.text = self.m_soilShow:GetRemainTimeString()
    end
    if curStepCfg.plantingStepType ~= GEnums.PlantingStepType.Grow then
        self.view.textNode.gameObject:SetActiveIfNecessary(false)
        self.view.textNodeYellow.gameObject:SetActiveIfNecessary(true)
    else
        self.view.textNode.gameObject:SetActiveIfNecessary(true)
        self.view.textNodeYellow.gameObject:SetActiveIfNecessary(false)
    end

    if curStep == self.m_currentStep then
        return
    end

    self.m_currentStep = curStep
    local maxCnt = self.m_soilCfg.plantingSteps.Count
    for index, key in ipairs(self.m_partNodeList) do
        local i = index - 1;
        local curChild = t[key]
        local visible = (i < maxCnt)
        local currentActive = (i == curStep)
        local currentHasDone = (i < curStep)
        local currentNotStart = (i > curStep)
        if curChild ~= nil then
            if i < maxCnt then
                local cfg = self.m_enumMap[self.m_soilCfg.plantingSteps[i].plantingStepType]
                if cfg ~= nil then
                    curChild.bgLine1.gameObject:SetActiveIfNecessary(currentActive)
                    curChild.bgLine2.gameObject:SetActiveIfNecessary(currentActive)
                    curChild.bgBlock1.gameObject:SetActiveIfNecessary(currentNotStart)
                    curChild.bgBlock2.gameObject:SetActiveIfNecessary(currentHasDone)
                    curChild.line1.gameObject:SetActiveIfNecessary((currentActive) or (currentHasDone))
                    curChild.line2.gameObject:SetActiveIfNecessary(currentHasDone)
                    curChild.dotLine1.gameObject:SetActiveIfNecessary(currentNotStart)
                    curChild.dotLine2.gameObject:SetActiveIfNecessary(currentNotStart or currentActive)
                    if currentNotStart then
                        curChild.icon.color = self.view.config.COLOR_ICON_INACTIVE;
                    end
                end
            end
            curChild.gameObject:SetActiveIfNecessary(visible)
        end
    end
    local hasFound, curStepConstCfg = Tables.plantingStepConstTable:TryGetValue(curStepCfg.plantingStepType)
    local facValid, curFacCfg = Tables.factoryBuildingTable:TryGetValue(self.m_soilCfg.id)
    if curStepCfg.plantingStepType == GEnums.PlantingStepType.Grow then
        self.view.mainBtnText.text = Language.LUA_FAC_FERTILIZATION_FERTILIZE_BTN
        Notify(MessageConst.REFRESH_CONTROLLER_HINT)
    else
        self.view.mainBtnText.text = curStepConstCfg.btnText
        Notify(MessageConst.REFRESH_CONTROLLER_HINT)
    end

    local isFertilizeUnlock = self.m_soilNode.system:IsFertilizeSystemUnlock()

    local hideBtnCommonYellow = curStepCfg.plantingStepType == GEnums.PlantingStepType.Grow and
        (not self.m_soilNode:CanFertilize() or
        (not isFertilizeUnlock))
    self.view.btnCommonYellow.gameObject:SetActiveIfNecessary(not hideBtnCommonYellow)

    self.view.soilItemDescText.text = curFacCfg.desc
    local data = UIUtils.getSoilRewardFirstItem(self.m_soilCfg.rewardId)
    local itemCfg = Tables.itemTable:GetValue(data.id)
    if itemCfg ~= nil then
        self.view.soilItemNameDescText.text = itemCfg.name
    end

    if curStepCfg.plantingStepType ~= GEnums.PlantingStepType.Grow then
        self.view.curStepIconYellow:LoadSprite(UIConst.UI_SPRITE_CROP, self.m_enumMap[curStepCfg.plantingStepType].icon)
        self.view.hintTextYellow.text = curStepConstCfg.hintText
    else
        self.view.curStepIcon:LoadSprite(UIConst.UI_SPRITE_CROP, self.m_enumMap[curStepCfg.plantingStepType].icon)
        self.view.hintText.text = curStepConstCfg.hintText
    end
    if curStepCfg.plantingStepType ~= GEnums.PlantingStepType.Grow then
        self.view.countTimeText.text = "--:--:--"
    end


    self:_UpdateFertilizeState(isFertilizeUnlock, curStepCfg)

    self:_RefreshBtn()
end

FacCultivateCtrl._UpdateFertilizeState = HL.Method(HL.Boolean, HL.Any) << function(self, isFertilizeUnlock, curStepCfg)
    if isFertilizeUnlock then
        if curStepCfg.plantingStepType == GEnums.PlantingStepType.Grow then
            self:_UpdateFertilizeIfUnlock()
        else
            self.view.righNodeState:SetState("Normal")
        end
    else
        self.view.righNodeState:SetState("Normal")
    end
end

FacCultivateCtrl._UpdateFertilizeIfUnlock = HL.Method() << function(self)
    if self.m_soilNode:CanFertilize() then
        self.view.righNodeState:SetState("Fertilization")
    else
    self:_UpdateFertilizeIfCannot()
    end
end

FacCultivateCtrl._UpdateFertilizeIfCannot = HL.Method() << function(self)
    if self.m_soilNode.fertilizeCount == 0 then
        self.view.righNodeState:SetState("Normal")
    else
        if self.m_soilNode.fertilizeIncreaseType ~= FertilizeIncreaseType.None then
            self.view.righNodeState:SetState("Increase")
        elseif self.m_soilNode.fertilizeRipenCount > 0 then
            self.view.righNodeState:SetState("AccelerateGrowth")
        else
            self.view.righNodeState:SetState("Normal")
        end
    end
end

FacCultivateCtrl._OnUpdateIfDoOperation = HL.Method() << function(self)




    self:_ClosePanel()
end

FacCultivateCtrl._PlayDoTweenAnim = HL.Method() << function(self)
    local curStep = self.m_soilNode.soilStage
    local curStepCfg = self.m_soilCfg.plantingSteps[curStep]
    local progress = self.m_soilShow:GetProgress()
    self.view.barMask.sizeDelta = Vector2(-(1 - progress) * self.view.bar.rect.size.x,self.view.barMask.sizeDelta.y)
    self:_UpdateShow()

    local stepNum = CSIndex(self.m_enumReverseLookup[curStepCfg.plantingStepType])
    local totalTime = 0
    local plantingSteps = Tables.plantingDataTable[self.m_uiInfo.buildingId].plantingSteps

    if curStepCfg.plantingStepType == GEnums.PlantingStepType.Grow and Tables.fertilizeDataTable[self.m_soilNode.fertilizeId] then
        totalTime = Tables.fertilizeDataTable[self.m_soilNode.fertilizeId].fertilizeTime
    else
        totalTime = plantingSteps[stepNum].plantingStepParameter.valueIntList[0]
    end

    if self.view.barMask.gameObject.activeSelf then
        self.m_doTweenAnim = self.view.barMask:DOSizeDelta(
            Vector2(0, self.view.barMask.rect.size.y),
            totalTime * (1 - progress)
        ):SetEase(1)
    end
end

FacCultivateCtrl._OnUpdateStepCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local curEnum = self.m_enumList[index]
    local hasFound, curStepConstCfg = Tables.plantingStepConstTable:TryGetValue(curEnum)
    local showCfg = self.m_enumMap[curEnum]
    cell.icon:LoadSprite(UIConst.UI_SPRITE_CROP, showCfg.icon)
    cell.text.text = curStepConstCfg.stepDescription
    cell.blackBG.gameObject:SetActiveIfNecessary(index % 2 == 0)
end

FacCultivateCtrl._OnOpenCrop = HL.StaticMethod(HL.Any) << function(arg)
    local unpackNodeId, cb = unpack(arg)
    if CSFactoryUtil.CheckMinaCharOperateNodeBlockByTeammate(unpackNodeId) then
        return
    end
    if PhaseManager:IsOpen(PhaseId.FacMachine) then
        return
    end
    if UIManager:IsOpen(PANEL_ID) then
        return
    end

    if UIManager:IsOpen(PanelId.FacFertilization) then
        UIManager:Close(PanelId.FacFertilization)
    end

    Notify(MessageConst.FAC_OPEN_BUILDING_PANEL, {
        nodeId = unpackNodeId,
        failCb = cb,
    })
end




FacCultivateCtrl.m_naviToItemBindingId = HL.Field(HL.Number) << -1

FacCultivateCtrl.m_cancelNaviItemBindingId = HL.Field(HL.Number) << -1

FacCultivateCtrl._InitController = HL.Method() << function(self)
    self.view.closeContentInfoBtn.onClick:AddListener(function()
        AudioAdapter.PostEvent("Au_UI_Toast_Common_Small_Close")
        self:_SetInfoVisible(false)
    end)
    self.view.doubleClickCloseBtn.onClick:AddListener(function()
        AudioAdapter.PostEvent("Au_UI_Toast_Common_Small_Close")
        self:_SetInfoVisible(false)
    end)
    self.m_naviToItemBindingId = UIUtils.bindInputPlayerAction("fac_cultivate_item", function()
        self:_SetInNaviState(true)
    end, self.view.inputGroup.groupId)
    self.m_cancelNaviItemBindingId = UIUtils.bindInputPlayerAction("common_cancel", function()
        self:_SetInNaviState(false)
    end, self.view.inputGroup.groupId)
    self.view.buildingCommon.view.controllerSideMenuBtn:InitControllerSideMenuBtn({
        preOpenMenu = function()
            self:_SetInNaviState(false)
        end,
    })
    self:_SetInNaviState(false)
end

FacCultivateCtrl._SetInNaviState = HL.Method(HL.Boolean) << function(self, navi)
    self.m_inNaviState = navi
    self.view.buildingCommon.view.closeButton.enabled = not navi
    InputManagerInst:ToggleBinding(self.m_naviToItemBindingId, not navi)
    InputManagerInst:ToggleBinding(self.m_cancelNaviItemBindingId, navi)
    InputManagerInst:ToggleGroup(self.view.buttonNodeGroup.groupId, not navi)
    if navi then
        self:SetNaviTarget(self.view.item.view.button)
    else
        InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.buildingCommon.view.buttonsNaviGroup)
        InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.itemPlantNode)
    end
end



HL.Commit(FacCultivateCtrl)
