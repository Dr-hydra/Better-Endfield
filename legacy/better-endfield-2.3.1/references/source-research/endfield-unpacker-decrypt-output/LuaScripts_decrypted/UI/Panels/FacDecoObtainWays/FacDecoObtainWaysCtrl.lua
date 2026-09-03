local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacDecoObtainWays

FacDecoObtainWaysCtrl = HL.Class('FacDecoObtainWaysCtrl', uiCtrl.UICtrl)

FacDecoObtainWaysCtrl.m_unlockObtainWays = HL.Field(HL.Table)

FacDecoObtainWaysCtrl.m_noObtainWays = HL.Field(HL.Table)

FacDecoObtainWaysCtrl.m_tasksInfo = HL.Field(HL.Table)

FacDecoObtainWaysCtrl.m_getItemCell = HL.Field(HL.Function)

FacDecoObtainWaysCtrl.m_itemId = HL.Field(HL.String) << ""

local SHIELD_OBTAIN_WAY = "item_obtain_mission_main"
local SHIELD_PHASE= "FacDecoObtainWays"
local RED_DOT_DECO_BUILDING_OBTAIN_WAY = "DecoBuildingObtainWay"

local ConditionHandleInfoTable = {
    [GEnums.ConditionType.MissionStateEqual] = {
        GetProgress = function()
            local _,desc = Utils.getCurMissionIdAndDesc("activity")
            return desc
        end,
        IsComplete = function(condition)
            return GameInstance.player.mission:GetMissionState(condition.parameters[0].valueStringList[0]) == CS.Beyond.Gameplay.MissionSystem.MissionState.Completed
        end
    },
    [GEnums.ConditionType.QuestStateEqual] = {
        GetProgress = function()
            local _,desc = Utils.getCurMissionIdAndDesc("activity")
            return desc
        end,
        IsComplete = function(condition)
            return GameInstance.player.mission:GetMissionState(condition.parameters[0].valueStringList[0]) == CS.Beyond.Gameplay.MissionSystem.MissionState.Completed
        end
    },
}





FacDecoObtainWaysCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_DECORATE_OBTAIN_MODIFY] = '_RefreshList',
}


FacDecoObtainWaysCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeButton.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.FacDecoObtainWays)
    end)

    self.m_itemId = arg.itemId
    local itemId = arg.itemId
    self:_InitBasicInfo(itemId)
    self:_InitUnlockObtainWays(itemId)
    self:_InitRedDot()
    self:_InitController()
end

FacDecoObtainWaysCtrl._InitBasicInfo = HL.Method(HL.String) << function(self, itemId)
    local _, itemData = Tables.itemTable:TryGetValue(itemId)
    self.view.titleTxt.text = itemData.name
    self.view.descTxt.text = UIUtils.getItemTypeName(itemId)
    local rarityColor = UIUtils.getItemRarityColor(itemData.rarity)
    self.view.rarity.color = rarityColor
    self.view.decorateImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
    self.view.tipsBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            transform = self.view.decorateImg.transform,
            itemId = itemId,
        })
    end)
end

FacDecoObtainWaysCtrl._InitUnlockObtainWays = HL.Method(HL.String) << function(self, itemId)
    self.m_unlockObtainWays = self:_GenerateObtainInfoList(itemId)
    self.m_noObtainWays = self:_GenerateNoObtainInfoList(itemId)
    self.m_tasksInfo = self:_GenerateTaskInfoList(itemId)
    self:_RefreshList()
end

FacDecoObtainWaysCtrl._GenerateObtainInfoList = HL.Method(HL.String).Return(HL.Table) << function(self, itemId)

    local obtainInfoList = {}


    local itemCfg = Tables.itemTable:GetValue(itemId)
    if itemCfg.obtainWayIds then
        for k, obtainWayId in pairs(itemCfg.obtainWayIds) do
            if obtainWayId == SHIELD_OBTAIN_WAY then
                goto continue
            end
            local _, obtainWayCfg = Tables.systemJumpTable:TryGetValue(obtainWayId)
            if obtainWayCfg then
                if obtainWayCfg.phaseId == SHIELD_PHASE then
                    goto continue
                end
                local isShowOrNoCondition = true
                local showSucc, showCondition = Tables.obtainWayShowCondTable:TryGetValue(obtainWayId)
                if showSucc then
                    isShowOrNoCondition = ItemObtainWaysUtils.CheckObtainWayCondition(showCondition)
                end
                local isUnlock = Utils.isSystemUnlocked(obtainWayCfg.bindSystem)
                if isShowOrNoCondition and isUnlock then
                    local phaseId = PhaseId[obtainWayCfg.phaseId]
                    local phaseArgs = Utils.buildSystemJumpPhaseArgsWithItem(obtainWayCfg, itemId)
                    local blockJumpToast = ""
                    if phaseId ~= nil and not PhaseManager:CheckCanOpenPhase(phaseId, phaseArgs) then
                        if obtainWayCfg.bindSystem == GEnums.UnlockSystemType.Map then
                            blockJumpToast = Language.LUA_OBTAIN_WAYS_MAP_JUMP_BLOCKED
                        else
                            blockJumpToast = Language.LUA_OBTAIN_WAYS_JUMP_BLOCKED
                        end
                    end
                    table.insert(obtainInfoList, {
                        name = obtainWayCfg.desc,
                        iconFolder = UIConst.UI_SPRITE_ITEM_TIPS,
                        iconId = obtainWayCfg.iconId,
                        phaseId = phaseId,
                        phaseArgs = phaseArgs,
                        blockJumpToast = blockJumpToast,
                        sortId = -k / 1000,
                        itemId = itemId,
                        obtainId = obtainWayId,
                    })
                end
            end
            :: continue ::
        end
    end

    table.sort(obtainInfoList, Utils.genSortFunction({"sortId"}))

    return obtainInfoList
end

FacDecoObtainWaysCtrl._GenerateNoObtainInfoList = HL.Method(HL.String).Return(HL.Table) << function(self, itemId)
    local noObtainInfoList = {}
    if #self.m_unlockObtainWays > 0 then
        return noObtainInfoList
    end
    local itemCfg = Tables.itemTable:GetValue(itemId)
    if itemCfg.noObtainWayId ~= nil and itemCfg.noObtainWayId.Count > 0 then
        local find, showNoObtainWayId = self:_FindShowNoObtainWayId(itemCfg)
        if find then
            local _, obtainWayCfg = Tables.systemJumpTable:TryGetValue(showNoObtainWayId)
            if obtainWayCfg then
                local isUnlock = Utils.isSystemUnlocked(obtainWayCfg.bindSystem) and not Utils.isInBlackbox()
                local phaseId = PhaseId[obtainWayCfg.phaseId]
                local phaseArgs = Utils.buildSystemJumpPhaseArgsWithItem(obtainWayCfg, itemId)
                if phaseId ~= nil and not PhaseManager:CheckCanOpenPhase(phaseId, phaseArgs) then
                    isUnlock = false
                end
                table.insert(noObtainInfoList, {
                    name = obtainWayCfg.desc,
                    iconFolder = UIConst.UI_SPRITE_ITEM_TIPS,
                    iconId = obtainWayCfg.iconId,
                    phaseId = isUnlock and phaseId or nil,
                    phaseArgs = isUnlock and phaseArgs or nil,
                })
            end
        end
    end
    return noObtainInfoList
end

FacDecoObtainWaysCtrl._FindShowNoObtainWayId = HL.Method(HL.Any).Return(HL.Boolean, HL.String) << function(self, itemCfg)
    if itemCfg == nil or itemCfg.noObtainWayId == nil or itemCfg.noObtainWayId.Count == 0 then
        return false, ""
    end

    for csIndex = 0, itemCfg.noObtainWayId.Count - 1 do
        if csIndex >= itemCfg.noObtainWayConditionId.Count then
            return true, itemCfg.noObtainWayId[csIndex]
        end
        local conditionId = itemCfg.noObtainWayConditionId[csIndex]
        local unlockTag = ItemObtainWaysUtils.CheckObtainWayCondition(conditionId)
        if not unlockTag then
            return true, itemCfg.noObtainWayId[csIndex]
        end
    end

    return false, ""
end

FacDecoObtainWaysCtrl._GenerateTaskInfoList = HL.Method(HL.String).Return(HL.Table) << function(self, itemId)
    local taskInfoList = {}
    local success, rewardInfo = Tables.FactoryDecoBuildingTable:TryGetValue(itemId)
    if not success or #rewardInfo.obtainIds == 0 then
        return taskInfoList
    end

    local index = 1
    for i = 1, #rewardInfo.obtainIds do
        table.insert(taskInfoList, {})
        local info = taskInfoList[index]
        local obtainId = rewardInfo.obtainIds[CSIndex(i)]
        local obtainInfo = Tables.DecoBuildingObtainWaysTable[obtainId]
        info.itemId = itemId
        info.obtainId = obtainId
        info.isTitle = true
        info.showGetReward = obtainInfo.showGetReward
        info.rewardId = obtainInfo.rewardId
        info.taskNum = #obtainInfo.conditionIds
        info.rewardItem = UIUtils.getRewardFirstItem(obtainInfo.rewardId)
        info.subTasks = {}
        local completeTaskNum = 0

        for j = 1, info.taskNum do
            table.insert(info.subTasks, {})
            local taskInfo = info.subTasks[j]
            local conditionId = obtainInfo.conditionIds[CSIndex(j)]
            local conditionInfo = Tables.DecoBuildingObtainConditionTable[conditionId]
            taskInfo.conditionId = conditionInfo.conditionId
            taskInfo.title = conditionInfo.title

            local taskParam = ConditionHandleInfoTable[conditionInfo.conditionType]
            if taskParam ~= nil then
                taskInfo.desc = string.format(conditionInfo.desc, taskParam.GetProgress())
                taskInfo.missionId = conditionInfo.parameters[0].valueStringList[0]
                local isComplete = taskParam.IsComplete(conditionInfo)
                taskInfo.isComplete = isComplete
                if isComplete then
                    completeTaskNum = completeTaskNum + 1
                end
            else
                taskInfo.desc = conditionInfo.desc
            end
        end

        info.completeTaskNum = completeTaskNum
        index = index + 1
    end

    return taskInfoList
end

FacDecoObtainWaysCtrl._RefreshList = HL.Method() << function(self)
    local cellCount = #self.m_unlockObtainWays + #self.m_noObtainWays + #self.m_tasksInfo
    if cellCount == 0 then
        self.view.content:SetState("Empty")
        return
    end
    self.view.content:SetState("Normal")
    local scrollList = self.view.scrollList
    if not self.m_getItemCell then
        self.m_getItemCell = UIUtils.genCachedCellFunction(scrollList)
        scrollList.onUpdateCell:AddListener(function(obj, csIndex)
            self:_OnUpdateCell(self.m_getItemCell(obj), LuaIndex(csIndex))
        end)
    end
    scrollList:UpdateCount(cellCount)
end

FacDecoObtainWaysCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local obtainWaysCount = #self.m_unlockObtainWays
    local isObtainWayCell = index <= obtainWaysCount
    if isObtainWayCell then
        self:_OnUpdateObtainWayCell(cell, index)
        return
    end

    local noWaysCount = #self.m_noObtainWays
    local isNoWayCell = index <= (obtainWaysCount + noWaysCount)
    if isNoWayCell then
        self:_OnUpdateNoWayCell(cell, index - obtainWaysCount)
        return
    end

    local taskIndex = index - obtainWaysCount - noWaysCount
    local taskInfo = self.m_tasksInfo[taskIndex]
    if taskInfo.isTitle then
        self:_OnUpdateTaskTitleCell(cell, taskIndex)
    end
end

FacDecoObtainWaysCtrl._OnUpdateObtainWayCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    cell.titleNode.gameObject:SetActiveIfNecessary(true)
    cell.stateList.gameObject:SetActiveIfNecessary(false)
    cell.titleNode:SetState("Goto")

    local info = self.m_unlockObtainWays[index]
    cell.icon:LoadSprite(info.iconFolder, info.iconId)
    cell.description.text = info.name
    cell.goBtn.onClick:RemoveAllListeners()

    if info.phaseId ~= nil and PhaseManager:CheckCanOpenPhase(info.phaseId, info.phaseArgs) then
        cell.goBtn.onClick:AddListener(function()
            local isBlocked = UIManager:ShouldBlockObtainWaysPhaseJump(info.phaseId)
            if isBlocked then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_OBTAIN_WAYS_JUMP_BLOCKED)
                return
            end
            if info.blockJumpToast ~= nil and not string.isEmpty(info.blockJumpToast) then
                Notify(MessageConst.SHOW_TOAST, info.blockJumpToast)
                return
            end
            PhaseManager:GoToPhase(info.phaseId, info.phaseArgs)
        end)
    end
    cell.redDot.gameObject:SetActiveIfNecessary(false)
end

FacDecoObtainWaysCtrl._OnUpdateNoWayCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    cell.titleNode.gameObject:SetActiveIfNecessary(true)
    cell.stateList.gameObject:SetActiveIfNecessary(false)

    local info = self.m_noObtainWays[index]
    cell.icon:LoadSprite(info.iconFolder, info.iconId)
    cell.description.text = info.name
    cell.goBtn.onClick:RemoveAllListeners()
    cell.redDot.gameObject:SetActiveIfNecessary(false)
    if info.phaseId then
        cell.titleNode:SetState("NoLock")
        cell.goBtn.onClick:AddListener(function()
            local isBlocked = UIManager:ShouldBlockObtainWaysPhaseJump(info.phaseId)
            if isBlocked then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_OBTAIN_WAYS_JUMP_BLOCKED)
                return
            end
            PhaseManager:GoToPhase(info.phaseId, info.phaseArgs)
        end)
    else
        cell.titleNode:SetState("Lock")
    end
end

FacDecoObtainWaysCtrl._OnUpdateTaskTitleCell = HL.Method(HL.Any, HL.Number) << function(self, cell, taskIndex)
    cell.titleNode.gameObject:SetActiveIfNecessary(true)
    cell.stateList.gameObject:SetActiveIfNecessary(true)
    local info = self.m_tasksInfo[taskIndex]
    if info.completeTaskNum == info.taskNum then
        local remoteFactoryCore = GameInstance.player.remoteFactory.core
        local isObtained = remoteFactoryCore:CheckDecorateObtained(info.obtainId)
        if not info.showGetReward or isObtained then
            cell.titleNode:SetState("Finish")
            cell.titleNode:SetState("Get")
            cell.redDot.gameObject:SetActiveIfNecessary(false)
        else
            cell.titleNode:SetState("Receive")
            cell.titleNode:SetState("NotGet")
            cell.receiveBtn.onClick:RemoveAllListeners()
            cell.receiveBtn.onClick:AddListener(function()
                remoteFactoryCore:Message_FactoryDecorateObtain(info.obtainId)
            end)
            cell.redDot:InitRedDot(RED_DOT_DECO_BUILDING_OBTAIN_WAY, {itemId = info.itemId, obtainId = info.obtainId})
        end
    else
        cell.titleNode:SetState("Inprogress")
        cell.titleNode:SetState("NotGet")
        cell.redDot.gameObject:SetActiveIfNecessary(false)
    end
    cell.icon:LoadSprite("Factory/Decorate", "icon_decorate_task_1")
    cell.description.text = Language.LUA_FACTORY_DECO_BUILDING_OBTAIN_TASK_TITLE
    cell.sliderTxt.text = string.format("%s/%s", info.completeTaskNum, info.taskNum)
    cell.slider.value = info.completeTaskNum / info.taskNum
    cell.itemSmallRewardBlack:InitItem({id = info.rewardItem.id, info.rewardItem.count}, true)

    local subTaskCount = #info.subTasks
    for i = 1, subTaskCount do
        local go = cell.stateList.transform:GetChild(CSIndex(i))
        if not go then
            go = GameObject.Instantiate(self.view.progressNode.cell.gameObject, self.view.progressNode.transform)
        end
        go.gameObject:SetActiveIfNecessary(true)
        local subCell = Utils.wrapLuaNode(go)
        self:_OnUpdateTaskCell(subCell, info.subTasks[i])
    end

    if subTaskCount > cell.stateList.transform.childCount then
        for i = subTaskCount + 1, cell.stateList.transform.childCount do
            local go = cell.stateList.transform:GetChild(CSIndex(i))
            go.gameObject:SetActiveIfNecessary(false)
        end
    end
end

FacDecoObtainWaysCtrl._OnUpdateTaskCell = HL.Method(HL.Any, HL.Table) << function(self, cell, info)
    cell.titleTxt.text = info.title
    cell.descTxt.text = info.desc

    if info.isComplete then
        cell.stateController:SetState("Finish")
    else
        cell.stateController:SetState("Normal")
        cell.goBtn.onClick:RemoveAllListeners()
        if info.missionId then
            cell.goBtn.onClick:AddListener(function()
                PhaseManager:OpenPhase(PhaseId.Mission, { autoSelect = info.missionId })
            end)
        else
            logger.error("不支持的跳转类型")
        end
    end
end

FacDecoObtainWaysCtrl._InitRedDot = HL.Method() << function(self)
    self.view.redDotScrollRect.getRedDotStateAt = function(csIndex)
        return self:_GetRedDotStateAt(csIndex)
    end
end

FacDecoObtainWaysCtrl._GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local luaIndex = LuaIndex(index)
    if luaIndex < 1 then
        return 0
    end

    local obtainWaysCount = #self.m_unlockObtainWays
    local noWaysCount = #self.m_noObtainWays
    local tasksInfoCount = #self.m_tasksInfo
    if obtainWaysCount == 0 and luaIndex <= noWaysCount then
        return 0
    elseif luaIndex > obtainWaysCount + noWaysCount + tasksInfoCount then
        return 0
    end

    if luaIndex <= obtainWaysCount then
        local info = self.m_unlockObtainWays[luaIndex]
        local hasRedDot, redDotType = RedDotManager:GetRedDotState("DecoBuilding", {itemId = info.itemId, obtainId = info.obtainId})
        if hasRedDot then
            return redDotType
        end
    elseif luaIndex > obtainWaysCount + noWaysCount then
        local info = self.m_tasksInfo[luaIndex - obtainWaysCount - noWaysCount]
        local hasRedDot, redDotType = RedDotManager:GetRedDotState("DecoBuilding", {itemId = info.itemId, obtainId = info.obtainId})
        if hasRedDot then
            return redDotType
        end
    end

    return 0
end

FacDecoObtainWaysCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    if self.m_getItemCell == nil then
        return
    end

    local slot = self.m_getItemCell(1)
    if slot ~= nil then
        self:SetNaviTarget(slot.titleNodeInputBindingGroupNaviDecorator)
    end
end

FacDecoObtainWaysCtrl.GetRecoverStateArg = HL.Method().Return(HL.Table) << function(self)
    return {itemId = self.m_itemId}
end

HL.Commit(FacDecoObtainWaysCtrl)
