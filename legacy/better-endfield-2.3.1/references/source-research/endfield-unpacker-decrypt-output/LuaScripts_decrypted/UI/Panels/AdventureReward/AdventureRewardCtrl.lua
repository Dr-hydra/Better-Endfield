local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AdventureReward
local PHASE_ID = PhaseId.AdventureReward
AdventureRewardCtrl = HL.Class('AdventureRewardCtrl', uiCtrl.UICtrl)





AdventureRewardCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_ADVENTURE_REWARD_RECEIVE] = "OnAdventureRewardReceive",
}

AdventureRewardCtrl.m_levelListCellFunc = HL.Field(HL.Function)

AdventureRewardCtrl.m_rewardCellCache = HL.Field(HL.Forward("UIListCache"))

AdventureRewardCtrl.m_docCellCache = HL.Field(HL.Forward("UIListCache"))

AdventureRewardCtrl.m_levelRewardData = HL.Field(HL.Table)

AdventureRewardCtrl.m_currRewards = HL.Field(HL.Any) << nil

AdventureRewardCtrl.m_currIndex = HL.Field(HL.Number) << -1

AdventureRewardCtrl.m_currSelectPos = HL.Field(HL.Number) << -1

AdventureRewardCtrl.m_isDragging = HL.Field(HL.Boolean) << false

AdventureRewardCtrl.m_drawOutTween = HL.Field(HL.Userdata)

AdventureRewardCtrl.m_drawOutPos = HL.Field(HL.Number) << 1

AdventureRewardCtrl.m_selectSwitchTween = HL.Field(HL.Any) << nil


AdventureRewardCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitWidget(arg)
    self:_InitView()
    self:_RestoreStateByArg(arg)
end

AdventureRewardCtrl._InitWidget = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.m_levelListCellFunc = UIUtils.genCachedCellFunction(self.view.levelAdapter)
    self.m_rewardCellCache = UIUtils.genCellCache(self.view.rewardCell)
    self.m_docCellCache = UIUtils.genCellCache(self.view.docCell)
    local switchBuilder = CS.Beyond.UI.UIAnimationSwitchTween.Builder()
    local foldHeight = self.view.config.LEVEL_FOLD_HEIGHT
    switchBuilder.animWrapper = self.view.panelSelect
    switchBuilder.dontDisableGameObject = true
    self.m_selectSwitchTween = switchBuilder:Build()
    self.m_selectSwitchTween:Reset(false)

    self.m_levelRewardData = self:_ProcessRewardData()
    local rewardCount = #self.m_levelRewardData
    local adventureLevelData = GameInstance.player.adventure.adventureLevelData
    local targetLevel = adventureLevelData.lv
    if type(arg) == "table" and type(arg.viewingLevel) == "number" then
        local restoredIndex = self:_FindLevelIndex(self.m_levelRewardData, arg.viewingLevel)
        if restoredIndex ~= nil and restoredIndex > 0 then
            targetLevel = arg.viewingLevel
        end
    end
    local initDataIndex = self:_FindLevelIndex(self.m_levelRewardData, targetLevel)
    if initDataIndex == nil or initDataIndex < 0 then
        initDataIndex = rewardCount
    end
    self.view.levelAdapter.onUpdateCell:AddListener(function(gameObject, csIndex)
        local cell = self.m_levelListCellFunc(gameObject)
        self:_UpdateLevelScrollCell(cell, csIndex)
    end)
    self.view.levelAdapter.getCellDefaultSize = function(csIndex)
        return foldHeight
    end

    self.view.nodeLevel.onStateChanged:AddListener(function(state)
        local isStable = state == UIConst.INERTIA_VIEW_PAGER_STATE.Idle
        self:_OnUpdateDrag(not isStable)
    end)

    self.view.levelScrollRect.onValueChanged:AddListener(function(normalizedPosition)
        self:_OnLevelScrollValueChanged(normalizedPosition)
    end)

    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "adventure_reward")
    end)

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.levelAdapter:UpdateCount(rewardCount)
    self.view.nodeLevel:SetPageCount(rewardCount)

    self.view.nodeLevel.currentPage = CSIndex(initDataIndex)
    self.m_currSelectPos = initDataIndex - 0.5
    self:_UpdateCurrIndex(initDataIndex)
    self:_ResetDrawOut(false)
    if DeviceInfo.usingController then
        self.view.levelAdapter.doOnceAfterLayout:AddListener(function()
            local view = self.view.levelAdapter:GetView(initDataIndex - 1)
            if view ~= nil then
                local cell = self.m_levelListCellFunc(view)
                self:SetNaviTarget(cell.view.button)
            end
        end)
    end
end

AdventureRewardCtrl._FindLevelIndex = HL.Method(HL.Any, HL.Number).Return(HL.Number) << function(self, rewardDataList, level)
    if rewardDataList == nil then
        return -1
    end
    for i, rewardData in ipairs(rewardDataList) do
        if rewardData.level == level then
            return i
        end
    end
    return -1
end

AdventureRewardCtrl._InitView = HL.Method() << function(self)
    local adventureLevelData = GameInstance.player.adventure.adventureLevelData
    local relativeExp = adventureLevelData.relativeExp
    local relativeLevelUpExp = adventureLevelData.relativeLevelUpExp

    self.view.expProgress.fillAmount = (relativeLevelUpExp > 0) and (relativeExp / relativeLevelUpExp) or 1
    self.view.curLevelTxt.text = adventureLevelData.lv
    if relativeLevelUpExp == 0 then
        self.view.expNode:SetState("MaxLevel")
    else
        self.view.expTxt.text = string.format(Language.LUA_ADVENTURE_REWARD_EXP_PROGRESS_FORMAT, relativeExp,
            relativeLevelUpExp)
    end
end

AdventureRewardCtrl._RestoreStateByArg = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    if type(arg) ~= "table" or not arg.isAdventureRewardInstructionShown then
        return
    end
    self:_StartCoroutine(function()
        UIManager:Open(PanelId.InstructionBook, "adventure_reward")
    end)
end

AdventureRewardCtrl._ProcessRewardData = HL.Method().Return(HL.Table) << function(self)
    local rewardData = {}
    local playerAdventure = GameInstance.player.adventure.adventureLevelData

    for _, adventureLevelData in pairs(Tables.adventureLevelTable) do
        if adventureLevelData.level ~= 1 then
            local rewardDataUnit = {}
            rewardDataUnit.level = adventureLevelData.level
            rewardDataUnit.rewardId = adventureLevelData.rewardId
            rewardDataUnit.rewardType = adventureLevelData.rewardShowType
            rewardDataUnit.gainStaminaLimit = adventureLevelData.raiseMaxStamina
            rewardDataUnit.gainReward = adventureLevelData.level <= playerAdventure.lv
            rewardDataUnit.hideReward = adventureLevelData.level >
                (playerAdventure.lv + self.view.config.HIDE_REWARD_LEVEL_FORWARD)

            table.insert(rewardData, rewardDataUnit)
        end
    end

    table.sort(rewardData, Utils.genSortFunction({ "level" }))

    local prevStaminaLimit = Tables.dungeonConst.initStaminaLimit
    for i = #rewardData, 1, -1 do
        rewardData[i].fromStaminaLimit = prevStaminaLimit
        rewardData[i].toStaminaLimit = prevStaminaLimit + rewardData[i].gainStaminaLimit
        prevStaminaLimit = rewardData[i].toStaminaLimit
    end

    return rewardData
end

AdventureRewardCtrl._UpdateLevelScrollCell = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)
    local luaIndex = LuaIndex(csIndex)
    cell:InitAdventureRewardShortInfoCell(self.m_levelRewardData[luaIndex], luaIndex, function(luaIndex)
        self:_OnLevelRewardCellClick(luaIndex)
    end)
end

AdventureRewardCtrl._OnUpdateDrag = HL.Method(HL.Boolean) << function(self, isDragging)
    self.m_isDragging = isDragging
    self:_StartDrawOutTween(isDragging)
end

AdventureRewardCtrl._UpdateCurrIndex = HL.Method(HL.Number) << function(self, curIndex)
    local clampedIndex = math.max(math.min(curIndex, #self.m_levelRewardData), 1)
    local levelInfo = self.m_levelRewardData[clampedIndex]
    if levelInfo ~= nil then
        self.view.levelArrowNode:SetState(curIndex == 1 and "IsMax" or "NotMax")
        self:_RefreshRewardPanel(levelInfo)
        local docCount = self.view.config.LEVEL_DOC_EXPAND_COUNT * 2 + 1
        self.m_docCellCache:Refresh(docCount, function(cell, luaIndex)
            self:_UpdateDocCell(cell, luaIndex)
        end)
    end
end

AdventureRewardCtrl._OnLevelScrollValueChanged = HL.Method(Vector2) << function(self, val)
    local scrollIndex = self.view.nodeLevel.currentScrollIndex
    local selectIndex = scrollIndex + 0.5
    local floorDataIndex = math.floor(selectIndex)
    local currIndex = floorDataIndex + 1
    local movePercent = selectIndex - floorDataIndex
    self.m_currSelectPos = scrollIndex

    self.view.docListHolder.transform.anchoredPosition = self.view.config.LEVEL_DOC_MOVE_PER_LOOP * movePercent
    if currIndex ~= self.m_currIndex then
        self.m_currIndex = currIndex
        self:_UpdateCurrIndex(currIndex)
        AudioAdapter.PostEvent("Au_UI_Button_AdvLvSelect")
    end

    local foldHeight = self.view.config.LEVEL_FOLD_HEIGHT
    local expandHeight = self.view.config.LEVEL_EXPAND_HEIGHT
    for i = 1, #self.m_levelRewardData do
        local view = self.view.levelAdapter:GetVirtualView(i - 1)
        if view ~= nil then
            local virtualIndex = view:GetIndex()
            local effectIndex = math.abs(scrollIndex - virtualIndex)
            effectIndex = 1 - math.max(math.min(effectIndex, 1), 0)
            view.preferSize = effectIndex * (expandHeight - foldHeight) + foldHeight
            if view.isAttached then
                self:_OnLevelScrollEffect(view:GetAttachedView(), effectIndex)
            end
        end
    end
    self.view.levelAdapter:NotifyAllSizeChanged()

    for idx, cell in pairs(self.m_docCellCache:GetItems()) do
        cell:UpdateCellExpand(self.m_currSelectPos, self.m_drawOutPos)
        cell:UpdateDrawOut(self.m_currSelectPos, self.m_drawOutPos)
    end
end

AdventureRewardCtrl._OnLevelScrollEffect = HL.Method(GameObject, HL.Number) << function(self, gameObject, effectVal)
    local cell = self.m_levelListCellFunc(gameObject)
    if cell ~= nil then
        cell:SampleCellEffect(effectVal)
    end
end

AdventureRewardCtrl._OnLevelRewardCellClick = HL.Method(HL.Number) << function(self, luaIndex)
    self.view.nodeLevel:MoveToPage(CSIndex(luaIndex))
end

AdventureRewardCtrl._RefreshRewardPanel = HL.Method(HL.Any) << function(self, rewardInfo)
    local adventureLevelData = GameInstance.player.adventure.adventureLevelData
    local gained = rewardInfo.level <= adventureLevelData.lv
    local itemBundles = Tables.rewardTable[rewardInfo.rewardId].itemBundles


    local rewards = {}
    for _, itemBundle in pairs(itemBundles) do
        local itemCfg = Tables.itemTable[itemBundle.id]
        local reward = {}
        reward.id = itemBundle.id
        reward.count = itemBundle.count
        reward.gained = gained
        table.insert(rewards, reward)
    end
    self.m_currRewards = rewards
    self.m_rewardCellCache:Refresh(#rewards, function(cell, luaIndex)
        self:_UpdateRewardCell(cell, luaIndex)
    end)

    if DeviceInfo.usingController then
        self.view.rewardNavi.onIsFocusedChange:RemoveAllListeners()
        self.view.rewardNavi.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end

    local gainStaminaLimit = rewardInfo.fromStaminaLimit < rewardInfo.toStaminaLimit
    self.view.rewardState:SetState(rewardInfo.hideReward and "HideReward" or "ShowReward")
    self.view.rewardState:SetState(gainStaminaLimit and "ShowStamina" or "HideStamina")
    self.view.rewardState:SetState(gained and "GainedStamina" or "NotGainedStamina")
    self.view.fromStamina.text = rewardInfo.fromStaminaLimit
    self.view.toStamina.text = rewardInfo.toStaminaLimit
end

AdventureRewardCtrl._UpdateRewardCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local reward = self.m_currRewards[luaIndex]
    cell:InitItem({ id = reward.id, count = reward.count, gained = reward.gained }, true)
    if DeviceInfo.usingController then
        cell:SetExtraInfo({
            isSideTips = true,
        })
    end
    cell.view.rewardedCover.gameObject:SetActiveIfNecessary(reward.gained)
end

AdventureRewardCtrl._UpdateDocCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local csIndex = CSIndex(luaIndex) - self.view.config.LEVEL_DOC_EXPAND_COUNT + self.m_currIndex - 1
    local doc = self.m_levelRewardData[LuaIndex(csIndex)]
    cell:InitAdventureRewardDocCell(doc, csIndex, self.m_currSelectPos, self.m_drawOutPos, self.view.config.LEVEL_DOC_EXPAND_COUNT)
end

AdventureRewardCtrl._UpdateDrawOut = HL.Method(HL.Number) << function(self, drawOutPos)
    self.m_drawOutPos = drawOutPos
    for idx, cell in pairs(self.m_docCellCache:GetItems()) do
        cell:UpdateDrawOut(self.m_currSelectPos, drawOutPos)
    end
end

AdventureRewardCtrl._ResetDrawOut = HL.Method(HL.Boolean) << function(self, isDrag)
    self:_UpdateDrawOut(isDrag and 0 or 1)
    self.m_selectSwitchTween:Reset(not isDrag)
end

AdventureRewardCtrl._StartDrawOutTween = HL.Method(HL.Boolean) << function(self, isDrag)
    self:_StopTweenIfNeeded()
    local startPos = self.m_drawOutPos
    local endPos = isDrag and 0 or 1
    local duration = math.abs(startPos - endPos) * self.view.config.LEVEL_DOC_DRAW_OUT_DURATION
    self.m_drawOutTween = DOTween.To(function()
        return startPos
    end, function(pos)
        self:_UpdateDrawOut(pos)
    end, endPos, duration)
    self.m_selectSwitchTween.isShow = not isDrag
    if not isDrag then
        AudioAdapter.PostEvent("Au_UI_Event_AdvLvDiskOut")
    end
end

AdventureRewardCtrl._StopTweenIfNeeded = HL.Method() << function(self)
    if self.m_drawOutTween ~= nil then
        self.m_drawOutTween:Kill(false)
        self.m_drawOutTween = nil
    end
end

AdventureRewardCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    local isAdventureRewardInstructionShown = false
    if isOpen and instructionCtrl:IsShow() and instructionCtrl.id == "adventure_reward" then
        isAdventureRewardInstructionShown = true
    end
    local viewingLevel = nil
    if self.m_levelRewardData ~= nil and self.m_currIndex ~= nil and self.m_currIndex > 0 then
        local clampedIndex = math.max(math.min(self.m_currIndex, #self.m_levelRewardData), 1)
        local levelInfo = self.m_levelRewardData[clampedIndex]
        if levelInfo ~= nil then
            viewingLevel = levelInfo.level
        end
    end
    return {
        isAdventureRewardInstructionShown = isAdventureRewardInstructionShown,
        viewingLevel = viewingLevel,
    }
end

AdventureRewardCtrl.OnClose = HL.Override() << function(self)
    self:_StopTweenIfNeeded()
end

AdventureRewardCtrl.OnAdventureRewardReceive = HL.Method(HL.Any) << function(self, args)
    local rewardLevels = unpack(args)
    local rewardItemsDic = {}

    for _, rewardLevel in pairs(rewardLevels) do
        local rewardId = Tables.adventureLevelTable[rewardLevel].rewardId
        local rewardCfg = Tables.rewardTable[rewardId]

        for _, bundle in pairs(rewardCfg.itemBundles) do
            if not rewardItemsDic[bundle.id] then
                rewardItemsDic[bundle.id] = {
                    id = bundle.id,
                    count = bundle.count,
                }
            else
                local count = rewardItemsDic[bundle.id].count
                rewardItemsDic[bundle.id].count = count + bundle.count
            end
        end
    end

    local rewardList = {}
    for _, rewardItem in pairs(rewardItemsDic) do
        table.insert(rewardList, rewardItem)
    end

    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        title = Language.LUA_ADVENTURE_LEVEL_REWARD_TITLE_DESC,
        items = rewardList
    })
end

HL.Commit(AdventureRewardCtrl)
