
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattlePass

BattlePassCtrl = HL.Class('BattlePassCtrl', uiCtrl.UICtrl)






BattlePassCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BATTLE_PASS_TASK_BASIC_INFO_UPDATE] = '_OnBasicInfoUpdate',
    [MessageConst.ON_BATTLE_PASS_LEVEL_UPDATE] = '_OnLevelUpdate',
    [MessageConst.ON_BATTLE_PASS_TRACK_UPDATE] = '_OnTrackUpdate',
    [MessageConst.ON_CHANGE_BATTLE_PASS_TAB] = '_OnChangeTab',
}

BattlePassCtrl.m_arg = HL.Field(HL.Table)

BattlePassCtrl.m_tabInfos = HL.Field(HL.Table)

BattlePassCtrl.m_curTabIndex = HL.Field(HL.Number) << -1

BattlePassCtrl.m_genTabCells = HL.Field(HL.Forward("UIListCache"))

BattlePassCtrl.m_seasonId = HL.Field(HL.String) << ''

BattlePassCtrl.m_curLevel = HL.Field(HL.Number) << 0

BattlePassCtrl.m_curShowLevel = HL.Field(HL.Number) << 0

BattlePassCtrl.m_maxLevel = HL.Field(HL.Number) << 0

BattlePassCtrl.m_curExp = HL.Field(HL.Number) << 0

BattlePassCtrl.m_curShowExp = HL.Field(HL.Number) << 0

BattlePassCtrl.m_openTime = HL.Field(HL.Number) << 0

BattlePassCtrl.m_endTime = HL.Field(HL.Number) << 0

BattlePassCtrl.m_buyLevelTime = HL.Field(HL.Number) << 0

BattlePassCtrl.m_expBoost = HL.Field(HL.Number) << 0

BattlePassCtrl.m_canBuyTime = HL.Field(HL.Number) << 0

BattlePassCtrl.m_expTween = HL.Field(HL.Userdata)


BattlePassCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    self:_InitViews()
    self:_InitTabIndex(arg)
    self:_InitTabs()
    self:_LoadData(true)
    self:_RenderViews()
    self:_RenderTimeRelatedPart()

    self:_ChangeTab(self.m_curTabIndex, true)
end

BattlePassCtrl.OnShow = HL.Override() << function(self)

    if CashShopUtils.NoCashShopGoods() and CashShopUtils.IsPS() then

        logger.info("[CashShop] 显示ps empty store")
        GameInstance.player.cashShopSystem:ShowPsEmptyStore()
    end
end




BattlePassCtrl.OnClose = HL.Override() << function(self)
    self:_ClearExpTween()
end

BattlePassCtrl._InitViews = HL.Method() << function(self)
    self:_InitTabInfos()

    self:BindInputPlayerAction("common_open_battle_pass", function()
        self.m_phase:CloseSelf()
    end)
    self.view.btnClose.onClick:RemoveAllListeners()
    self.view.btnClose.onClick:AddListener(function()
        self.m_phase:CloseSelf()
    end)
    self.view.helpBtn.onClick:RemoveAllListeners()
    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "battle_pass")
    end)
    self.view.btnCommon.onClick:RemoveAllListeners()
    self.view.btnCommon.onClick:AddListener(function()
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
        PhaseManager:GoToPhase(PhaseId.BattlePassBuyLevel, {
            onBuyClose = function(isBuy)
                if isBuy then
                    local tabCell = self.m_genTabCells:GetItem(1)
                    if tabCell ~= nil then
                        tabCell.toggle.isOn = true
                    end
                    self:_OnLevelUpdateImpl()
                end
            end
        })
    end)
    self.m_genTabCells = UIUtils.genCellCache(self.view.tabPlanNode)
end

BattlePassCtrl._InitTabInfos = HL.Method() << function(self)












    self.m_tabInfos = {}
    table.insert(self.m_tabInfos, {
        iconId = "icon_bp_plan",
        tabName = "ui_battlepass_tab_plan",
        panelId = PanelId.BattlePassPlan,
        redDot = "BattlePassPlan",
    })
    table.insert(self.m_tabInfos, {
        iconId = "icon_bp_task",
        tabName = "ui_battlepass_tab_task",
        panelId = PanelId.BattlePassTask,
        redDot = "BattlePassTask",
    })
end

BattlePassCtrl._InitTabIndex = HL.Method(HL.Any) << function(self, arg)
    if arg ~= nil and arg.panelId ~= nil then
        local panelId = PanelId[arg.panelId]
        local panelIndex = self:_FindTabIndexByPanelId(panelId)
        if panelIndex > 0 then
            self.m_curTabIndex = panelIndex
        end
    else
        self.m_curTabIndex = 1
    end
end

BattlePassCtrl._FindTabIndexByPanelId = HL.Method(HL.Number).Return(HL.Number) << function(self, panelId)
    for index, info in ipairs(self.m_tabInfos) do
        if info.panelId == panelId then
            return index
        end
    end
    return -1
end

BattlePassCtrl._InitTabs = HL.Method() << function(self)
    self.m_genTabCells:Refresh(#self.m_tabInfos, function(cell, luaIndex)
        local info = self.m_tabInfos[luaIndex]
        local iconPath = UIConst.UI_SPRITE_BATTLE_PASS
        cell.selectedIcon:LoadSprite(iconPath, info.iconId)
        cell.defaultIcon:LoadSprite(iconPath, info.iconId)
        cell.selectedNameTxt.text = I18nUtils.GetText(info.tabName)
        cell.defaultNameTxt.text = I18nUtils.GetText(info.tabName)
        if not string.isEmpty(info.redDot) then
            if info.redDotArg then
                cell.redDot:InitRedDot(info.redDot, info.redDotArg)
            else
                cell.redDot:InitRedDot(info.redDot)
            end
        end

        cell.toggle.isOn = luaIndex == self.m_curTabIndex
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:_ChangeTab(luaIndex)
            end
        end)
    end)
end

BattlePassCtrl._LoadData = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    local bpSystem = GameInstance.player.battlePassSystem
    self.m_seasonId = bpSystem.seasonData.seasonId
    self.m_openTime = bpSystem.seasonData.openTime
    self.m_endTime = bpSystem.seasonData.closeTime
    self.m_canBuyTime = bpSystem.seasonData.canBuyLevelTime
    self.m_curLevel = bpSystem.levelData.currLevel
    self.m_curExp = bpSystem.levelData.currExp
    if isInit then
        self.m_curShowLevel = self.m_curLevel
        self.m_curShowExp = self.m_curExp
    end
    self.m_expBoost = BattlePassUtils.GetBattlePassExpBoost()
    if string.isEmpty(self.m_seasonId) then
        return
    end
    local hasSeason, seasonData = Tables.battlePassSeasonTable:TryGetValue(self.m_seasonId)
    if not hasSeason then
        return
    end
    self.m_maxLevel = seasonData.maxLevel
end

BattlePassCtrl._RenderViews = HL.Method() << function(self)
    if not string.isEmpty(self.m_seasonId) then
        local hasSeason, seasonData = Tables.battlePassSeasonTable:TryGetValue(self.m_seasonId)
        if hasSeason then
            self.view.titleText.text = seasonData.shortName
        end
    end
    self:_ClearExpTween()
    if self.m_curLevel <= self.m_curShowLevel and self.m_curExp <= self.m_curShowExp then
        self.m_curShowLevel = self.m_curLevel
        self.m_curShowExp = self.m_curExp
        self:_RenderExp(self.m_curLevel, self.m_curExp)
    else
        self:_PlayExpTween()
    end

    self.view.percentageNode.gameObject:SetActive(self.m_expBoost > 0)
    self.view.percentageTxt.text = string.format("+%d%%", self.m_expBoost / 10)
end

BattlePassCtrl._RenderMaxLevel = HL.Method() << function(self)
    self.view.maxLevelNode:ClearTween()
    if self.m_curLevel == self.m_maxLevel then
        self.view.maxLevelNode:PlayWithTween(self.view.config.LEVEL_MAX_ANIMATION_NAME)
    else
        self.view.maxLevelNode:PlayWithTween(self.view.config.LEVEL_MAX_OVER_ANIMATION_NAME)
    end
end

BattlePassCtrl._QueryToNextExp = HL.Method(HL.Number, HL.Number).Return(HL.Number) << function(self, level, maxLevel)
    local hasSeason, seasonData = Tables.battlePassSeasonTable:TryGetValue(self.m_seasonId)
    if not hasSeason then
        return 0
    end
    local hasLevel, levelGroupData = Tables.battlePassLevelTable:TryGetValue(seasonData.levelGroupId)
    if hasLevel then
        local nextLevel = math.min(level, maxLevel) + 1
        local hasNext, levelData = levelGroupData.levelInfos:TryGetValue(nextLevel)
        if hasNext then
            return levelData.levelExp
        end
    end
end

BattlePassCtrl._RenderExp = HL.Method(HL.Number, HL.Number) << function(self, level, exp)
    local showLevel = math.min(level, self.m_maxLevel)
    local progress = 0
    local toNextExp = self:_QueryToNextExp(level, self.m_maxLevel)
    local showExp = (toNextExp > 0 and showLevel >= self.m_maxLevel) and (exp % toNextExp) or exp
    if toNextExp > 0 then
        progress = showExp / toNextExp
    end
    showExp = math.floor(showExp + 0.5)
    toNextExp = math.floor(toNextExp + 0.5)
    self.view.fill.gameObject:SetActive(showExp > 0)
    self.view.levelNumTxt.text = showLevel
    self.view.progressBar.value = progress
    self.view.progressTxt.text = string.format(Language.LUA_BATTLEPASS_PLAN_EXP_PROGRESS_FORMAT, showExp, toNextExp)
end

BattlePassCtrl._PlayExpTween = HL.Method() << function(self)
    local startPos = self.m_curShowExp
    local endPos = 0
    local toNextExpMap = {}
    for i = self.m_curShowLevel, self.m_curLevel do
        if i < self.m_curLevel then
            local toNextExp = self:_QueryToNextExp(i, self.m_maxLevel)
            toNextExpMap[i] = toNextExp

            if i < self.m_maxLevel then
                endPos = endPos + toNextExp
            end
        else
            endPos = endPos + self.m_curExp
        end
    end
    local duration = self.view.config.EXP_ANIMATION_DURATION
    self.m_expTween = DOTween.To(function()
        return startPos
    end, function(pos)
        local toNextExp = -1
        if toNextExpMap[self.m_curShowLevel] ~= nil then
            toNextExp = toNextExpMap[self.m_curShowLevel]
        end
        local curExp = pos
        for level, toNextExp in pairs(toNextExpMap) do

            if level < self.m_curShowLevel and level < self.m_maxLevel then
                curExp = curExp - toNextExp
            end
        end
        if toNextExp < 0 then

            self.m_curShowExp = curExp
        else

            if curExp >= toNextExp then

                self.m_curShowLevel = self.m_curShowLevel + 1
                self.m_curShowExp = curExp - toNextExp
                if self.m_curShowLevel <= self.m_maxLevel then
                    self:_OnExpTweenLevelUp()
                end
            else

                self.m_curShowExp = curExp
            end
        end
        self:_RenderExp(self.m_curShowLevel, self.m_curShowExp)
    end, endPos, duration):OnComplete(
        function()
            self:_OnExpTweenEnd()
        end
    ):SetEase(CS.DG.Tweening.Ease.OutCubic)
    self:_OnExpTweenStart()
end

BattlePassCtrl._ClearExpTween = HL.Method() << function(self)
    if self.m_expTween ~= nil then
        self.m_expTween:Kill(false)
        self.m_expTween = nil
        self:_OnExpTweenEnd()
    end
end

BattlePassCtrl._OnExpTweenStart = HL.Method() << function(self)
    if PhaseManager:GetTopPhaseId() == PhaseId.BattlePass then
        AudioAdapter.PostEvent("Au_UI_Event_BPLevelUp")
    end
end

BattlePassCtrl._OnExpTweenEnd = HL.Method() << function(self)
    AudioAdapter.PostEvent("Au_UI_Event_BPLevelUp_Stop")
end

BattlePassCtrl._OnExpTweenLevelUp = HL.Method() << function(self)
    if not string.isEmpty(self.view.config.LEVEL_UP_ANIMATION_NAME) then
        self.view.topNode:ClearTween()
        self.view.topNode:PlayWithTween(self.view.config.LEVEL_UP_ANIMATION_NAME)
    end
    AudioAdapter.PostEvent("Au_UI_Event_BPLevelUpgrade")
end

BattlePassCtrl._RenderTimeRelatedPart = HL.Method() << function(self)
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local leftSec = self.m_endTime - curServerTime
    leftSec = math.max(leftSec, 0)
    self.view.endTimeTxt.text = string.format(Language.LUA_BATTLEPASS_PLAN_END_TIME_HINT, UIUtils.getLeftTime(leftSec))
    self.view.buyNode:SetState(self.m_curLevel >= self.m_maxLevel and "Max" or (curServerTime >= self.m_canBuyTime and "CanBuy" or "CantBuy"))
end

BattlePassCtrl._OnChangeTab = HL.Method(HL.Any) << function(self, arg)
    if arg == nil or arg.panelId == nil then
        return
    end
    local panelId = PanelId[arg.panelId]
    local index = self:_FindTabIndexByPanelId(panelId)
    local toggleCell = self.m_genTabCells:Get(index)
    if toggleCell ~= nil then
        toggleCell.toggle.isOn = true
    end
end

BattlePassCtrl._ChangeTab = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, luaIndex, isInit)
    if self.m_curTabIndex == luaIndex and not isInit then
        return
    end
    local isRight = self.m_curTabIndex <= luaIndex
    self.m_curTabIndex = luaIndex
    local tabInfo = self.m_tabInfos[luaIndex]
    local arg = {
        baseNaviGroupId = self.view.inputGroup.groupId,
    }
    if isInit and self.m_arg ~= nil and self.m_arg.panelArgs ~= nil then
        arg.panelArgs = self.m_arg.panelArgs
        arg.popupPlanBuy = self.m_arg.popupPlanBuy
    end
    local ret = self.m_phase:ChangePanel(tabInfo.panelId, isRight, arg)
    if not ret then

        self.m_curTabIndex = 3 - luaIndex
        local cell = self.m_genTabCells:GetItem(self.m_curTabIndex)
        cell.toggle:SetIsOnWithoutNotify(true)
    else

        if luaIndex == 1 then
            self.m_phase:ShowPsStore()
        else
            self.m_phase:HidePsStore()
        end
    end
end

BattlePassCtrl.GetRecoverStateArg = HL.Method().Return(HL.Table) << function(self)
    local recoverState = {}
    if self.m_tabInfos ~= nil and self.m_curTabIndex > 0 then
        local tabInfo = self.m_tabInfos[self.m_curTabIndex]
        if tabInfo ~= nil then
            local isOpen, ctrl = UIManager:IsOpen(tabInfo.panelId)
            if isOpen and ctrl ~= nil then
                recoverState.panelId = ctrl.panelCfg.name
                if HL.TryGet(ctrl, "GetRecoverStateArg") then
                    recoverState.panelArgs = ctrl:GetRecoverStateArg()
                end
            end
        end
    end
    return recoverState
end

BattlePassCtrl._OnLevelUpdate = HL.Method() << function(self)
    if UIManager:IsOpen(PanelId.BattlePassBuyLevel) then
        return
    end
    self:_OnLevelUpdateImpl()
end

BattlePassCtrl._OnLevelUpdateImpl = HL.Method() << function(self)
    local fromLevel = self.m_curLevel
    local fromExp = self.m_curExp
    local fromRecruitTime = 0
    local toNextExp = self:_QueryToNextExp(self.m_maxLevel, self.m_maxLevel)
    if toNextExp > 0 and fromLevel >= self.m_maxLevel then
        fromRecruitTime = fromExp // toNextExp
    end
    self:_LoadData()
    self:_RenderViews()
    self:_RenderTimeRelatedPart()
    local currRecruitTime = 0
    if toNextExp > 0 and self.m_curLevel >= self.m_maxLevel then
        currRecruitTime = self.m_curExp // toNextExp
    end
    if self.m_curLevel >= self.m_maxLevel and (fromLevel < self.m_curLevel or fromRecruitTime < currRecruitTime) then
        self:_RenderMaxLevel()
        if fromRecruitTime < currRecruitTime then
            self.view.maxTextNew.text = string.format(Language.LUA_BATTLE_PASS_PLAN_MAX_LEVEL_RECRUIT_TIME_FORMAT, currRecruitTime - fromRecruitTime)
        end
        AudioAdapter.PostEvent("Au_UI_Event_BPLevelUpgrade")
    end
end

BattlePassCtrl._OnTrackUpdate = HL.Method() << function(self)
    self.m_expBoost = BattlePassUtils.GetBattlePassExpBoost()
    self.view.percentageNode.gameObject:SetActive(self.m_expBoost > 0)
    self.view.percentageTxt.text = string.format("+%d%%", self.m_expBoost / 10)
end

BattlePassCtrl._OnBasicInfoUpdate = HL.Method() << function(self)
    self:_RenderTimeRelatedPart()
end

HL.Commit(BattlePassCtrl)
