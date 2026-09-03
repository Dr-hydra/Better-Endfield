local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SimulationTrainingSettlementPopup

























SimulationTrainingSettlementPopupCtrl = HL.Class('SimulationTrainingSettlementPopupCtrl', uiCtrl.UICtrl)







SimulationTrainingSettlementPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ALL_CHARACTER_DEAD] = 'OnAllCharacterDead',
}


SimulationTrainingSettlementPopupCtrl.m_getRewardItemCellFunc = HL.Field(HL.Function)


SimulationTrainingSettlementPopupCtrl.m_items = HL.Field(HL.Table)


SimulationTrainingSettlementPopupCtrl.m_leaveTick = HL.Field(HL.Number) << -1


SimulationTrainingSettlementPopupCtrl.m_leaveTime = HL.Field(HL.Number) << 0


SimulationTrainingSettlementPopupCtrl.m_isClose = HL.Field(HL.Boolean) << false


SimulationTrainingSettlementPopupCtrl.m_rewardPoint = HL.Field(HL.Number) << 0

local exitTime = 60




SimulationTrainingSettlementPopupCtrl.OnShowSimulationTrainingResult = HL.StaticMethod(HL.Opt(HL.Any)) << function(args)
    if GameInstance.player.simulationTrainingSystem.deadInGame then
        return
    end

    LuaSystemManager.commonTaskTrackSystem:AddRequest("DungeonSettlement", function()
        if GameInstance.player.simulationTrainingSystem.deadInGame then
            return
        end
        local ctrl = UIManager:AutoOpen(PANEL_ID)
        if ctrl == nil then
            return
        end
        ctrl:StartSettlement(args)
    end, function()
        if UIManager:IsShow(PANEL_ID) then
            UIManager:Close(PANEL_ID)
        end
    end)
end






SimulationTrainingSettlementPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnLeaveDungeon.onClick:AddListener(function()
        self:_OnBtnLeaveClick()
    end)

    self.view.btnRestartDungeon.onClick:AddListener(function()
        self:_OnBtnRestartClick()
    end)

    self.m_getRewardItemCellFunc = UIUtils.genCachedCellFunction(self.view.rewardsScrollList)
    self.view.rewardsScrollList.onUpdateCell:AddListener(function(gameObject, csIndex)
        self:_OnUpdateCell(gameObject, csIndex)
    end)

    self.view.rewardsScrollList.onGraduallyShowFinish:AddListener(function()
        self:_OnGraduallyShowFinish()
    end)

    self:_InitController()
end



SimulationTrainingSettlementPopupCtrl.OnShow = HL.Override() << function(self)
    Notify(MessageConst.TOGGLE_COMMON_ITEM_TOAST, false)
end



SimulationTrainingSettlementPopupCtrl.OnHide = HL.Override() << function(self)
    Notify(MessageConst.TOGGLE_COMMON_ITEM_TOAST, true)
end



SimulationTrainingSettlementPopupCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.simulationTrainingSystem:SetShowInteractOptions(true)
    GameInstance.player.simulationTrainingSystem:SetBlockAllPlayerAction(false)
    Notify(MessageConst.HIDE_ITEM_TIPS)
    Notify(MessageConst.TOGGLE_COMMON_ITEM_TOAST, true)
    if self.m_leaveTick then
        self.m_leaveTick = LuaUpdate:Remove(self.m_leaveTick)
    end
end



SimulationTrainingSettlementPopupCtrl.OnAnimationInFinished = HL.Override() << function(self)
    local obj = self.view.rewardsScrollList:Get(0)
    if obj then
        local cell = self.m_getRewardItemCellFunc(obj)
        if cell then
            InputManagerInst:MoveVirtualMouseTo(cell.transform, self.uiCamera)
        end
    end
end



SimulationTrainingSettlementPopupCtrl._OnGraduallyShowFinish = HL.Method() << function(self)
    if DeviceInfo.usingController then
        self.view.controllerHintPlaceholder.gameObject:SetActive(true)
        self.view.focusItemKeyHint.gameObject:SetActive(true)
        local firstItemGo = self.view.rewardsScrollList:Get(0)
        if firstItemGo then
            self.view.focusItemKeyHint.transform.position = firstItemGo.transform.position
            local keyHintPos = self.view.focusItemKeyHint.transform.localPosition
            keyHintPos = keyHintPos + self.view.config.FOCUS_REWARDS_OFFSET
            self.view.focusItemKeyHint.transform.localPosition = keyHintPos
        end
    end
end

SimulationTrainingSettlementPopupCtrl.OnAllCharacterDead = HL.Method() << function(self)
    self:_OnBtnCloseClick()
end



SimulationTrainingSettlementPopupCtrl._OnBtnCloseClick = HL.Method() << function(self)
    if self.m_isClose then
       return
    end
    self.m_isClose = true
    self:PlayAnimationOutWithCallback(function()
        self:Close()
    end)
end



SimulationTrainingSettlementPopupCtrl._UpdateRewardsState = HL.Method() << function(self)
    self.m_items = self:_GetRewardItems()


    self.view.btnRestartDungeon.gameObject:SetActiveIfNecessary(true)

    local rewardsCount = #self.m_items
    self.view.rewardsList.gameObject:SetActiveIfNecessary(rewardsCount > 0)
    self.view.emptyRewardNode.gameObject:SetActiveIfNecessary(rewardsCount == 0)
    if rewardsCount == 0 then
        local system = GameInstance.player.simulationTrainingSystem
        if system.unlimitedMode then
            self.view.emptyRewardNodeStateController:SetState("Infinity")
        else
            self.view.emptyRewardNodeStateController:SetState("Standard")
        end
    end
    self:_StartTimer(1, function()
        self.view.rewardsScrollList:UpdateCount(rewardsCount)
    end)

    self.view.btnNode.gameObject:SetActiveIfNecessary(true)
    self.view.rewardsNode.gameObject:SetActiveIfNecessary(true)
    self.view.titleTxt.text = Language.LUA_DUNGEON_SETTLEMENT_REWARDS_TITLE

end



SimulationTrainingSettlementPopupCtrl._OnBtnRestartClick = HL.Method() << function(self)
    local system = GameInstance.player.simulationTrainingSystem
    if system.unlimitedRestartTips then
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_SIMULATION_TRAINING_TIP_UNLIMITED_POPUP_TITLE,
            subContent = "",
            onConfirm = function()
                if self.m_isClose then
                    Notify(MessageConst.ON_OPEN_SIMULATION_TRAINING_DRAW_PANEL)
                else
                    self.m_isClose = true
                    self:PlayAnimationOutWithCallback(function()
                      self:Close()
                      Notify(MessageConst.ON_OPEN_SIMULATION_TRAINING_DRAW_PANEL)
                    end)
                end
            end,
            onCancel = nil,
            confirmText = Language.LUA_SIMULATION_TRAINING_TIP_UNLIMITED_POPUP_CONFIRM_TEXT,
            cancelText = Language.LUA_SIMULATION_TRAINING_TIP_UNLIMITED_POPUP_CANCEL_TEXT,
        })
    else
        self.m_isClose = true
        self:PlayAnimationOutWithCallback(function()
            self:Close()

            if system.unlimitedMode then
                Notify(MessageConst.ON_OPEN_SIMULATION_TRAINING_DRAW_PANEL)
            else
                if system.dailyPlayCnt == 0 then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_SIMULATION_TRAINING_PLAY_CNT_EXHAUSTED_TOAST)
                    return
                end
                Notify(MessageConst.ON_OPEN_SIMULATION_TRAINING_DRAW_PANEL)
            end
        end)
    end
end



SimulationTrainingSettlementPopupCtrl._OnBtnLeaveClick = HL.Method() << function(self)
    self:Notify(MessageConst.HIDE_ITEM_TIPS)
    self:_OnBtnCloseClick()
end



SimulationTrainingSettlementPopupCtrl._OnClickSkipGraduallyShow = HL.Method() << function(self)
    self.view.luaPanel.animationWrapper:SkipInAnimation()
    self.view.rewardsScrollList:SkipGraduallyShow()
end





SimulationTrainingSettlementPopupCtrl._OnUpdateCell = HL.Method(GameObject, HL.Number) << function(self, go, csIndex)
    local cell = self.m_getRewardItemCellFunc(go)
    local index = LuaIndex(csIndex)
    local itemBundle = self.m_items[index]
    cell:InitItem(itemBundle, true)
    UIUtils.setRewardItemRarityGlow(cell, UIUtils.getItemRarity(itemBundle.id))
    cell:SetExtraInfo({
        isSideTips = DeviceInfo.usingController,
    })
end



SimulationTrainingSettlementPopupCtrl._GetRewardItems = HL.Method().Return(HL.Table) << function(self)
    local items = {}
    if self.m_rewardPoint > 0 then
        table.insert(items, {id="item_domain_jinlong_coupon", count=self.m_rewardPoint})
    end
    return items
end




SimulationTrainingSettlementPopupCtrl.StartSettlement = HL.Method(HL.Any) << function(self, args)
    local rewardPoint = unpack(args)
    self.m_rewardPoint = rewardPoint

    self:_UpdateRewardsState()


    self.m_leaveTime = 0
    self.m_leaveTick = LuaUpdate:Add("Tick", function(deltaTime)
        self.m_leaveTime = self.m_leaveTime + deltaTime
        local leftTime = math.ceil(exitTime - self.m_leaveTime)
        if leftTime <= 0 then
            leftTime = 0

            self:_OnBtnCloseClick()
            return
        end
        self.view.leaveTxt.text = tostring(leftTime) .. Language.LUA_LEAVE_DUNGEON_TEXT
    end)
end



SimulationTrainingSettlementPopupCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.focusItemKeyHint.gameObject:SetActive(false)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    local rewardNaviGroup = self.view.rewardsScrollList.gameObject:GetComponent("UISelectableNaviGroup")
    rewardNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

HL.Commit(SimulationTrainingSettlementPopupCtrl)