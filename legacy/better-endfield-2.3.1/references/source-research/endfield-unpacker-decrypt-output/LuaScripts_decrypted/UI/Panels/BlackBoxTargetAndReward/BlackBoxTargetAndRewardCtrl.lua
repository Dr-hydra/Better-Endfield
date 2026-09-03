
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BlackBoxTargetAndReward
BlackBoxTargetAndRewardCtrl = HL.Class('BlackBoxTargetAndRewardCtrl', uiCtrl.UICtrl)







BlackBoxTargetAndRewardCtrl.s_messages = HL.StaticField(HL.Table) << {

}

BlackBoxTargetAndRewardCtrl.m_warningCellCache = HL.Field(HL.Forward("UIListCache"))

BlackBoxTargetAndRewardCtrl.m_mainGoalCellCache = HL.Field(HL.Forward("UIListCache"))

BlackBoxTargetAndRewardCtrl.m_extraGoalCellCache = HL.Field(HL.Forward("UIListCache"))

BlackBoxTargetAndRewardCtrl.m_mainRewardCellsCache = HL.Field(HL.Forward("UIListCache"))

BlackBoxTargetAndRewardCtrl.m_extraRewardCellCache = HL.Field(HL.Forward("UIListCache"))


BlackBoxTargetAndRewardCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    self.view.btnExit.onClick:AddListener(function()
        self:_OnBtnExitClick()
    end)

    self:_Refresh(arg)
    self:_InitController()
end

BlackBoxTargetAndRewardCtrl._Refresh = HL.Method(HL.Table) << function(self, args)
    local dungeonId = args.dungeonId
    local dungeonCfg = Tables.dungeonTable[dungeonId]


    self.view.titleTxt:SetAndResolveTextStyle(dungeonCfg.dungeonName)


    self:_RefreshWarnings(args.warningInfo)

    local trackingMgr = GameWorld.levelScriptTaskTrackingManager
    local mainTask = trackingMgr.mainTask
    local mainGoalCount = mainTask ~= nil and mainTask.objectives.Length or 0
    self.view.mainGoalRoot.gameObject:SetActive(mainGoalCount > 0)
    self.m_mainGoalCellCache = self.m_mainGoalCellCache or UIUtils.genCellCache(self.view.mainGoalCell)
    self.m_mainGoalCellCache:Refresh(mainGoalCount, function(cell, index)
        cell:InitBlackBoxTaskCell(index, CS.Beyond.Gameplay.LevelScriptTaskType.Main)
    end)

    local extraTask = trackingMgr.extraTask
    local extraGoalCount = extraTask ~= nil and extraTask.objectives.Length or 0
    self.view.extraGoalRoot.gameObject:SetActive(extraGoalCount > 0)
    self.m_extraGoalCellCache = self.m_extraGoalCellCache or UIUtils.genCellCache(self.view.extraGoalCell)
    self.m_extraGoalCellCache:Refresh(extraGoalCount, function(cell, index)
        cell:InitBlackBoxTaskCell(index, CS.Beyond.Gameplay.LevelScriptTaskType.Extra)
    end)

    local mainGoalFinish = GameInstance.dungeonManager:IsDungeonFirstPassRewardGained(dungeonId)
    local extraGoalFinish = GameInstance.dungeonManager:IsDungeonExtraRewardGained(dungeonId)

    self.view.mainRewardFinished.gameObject:SetActive(mainGoalFinish)
    self.view.mainRewardUnfinished.gameObject:SetActive(not mainGoalFinish)
    local findMainReward, mainRewardData = Tables.rewardTable:TryGetValue(dungeonCfg.firstPassRewardId or "")
    local mainRewardItemBundles = {}
    if findMainReward then
        for _,itemBundle in pairs(mainRewardData.itemBundles) do
            local _, itemData = Tables.itemTable:TryGetValue(itemBundle.id)
            if itemData then
                table.insert(mainRewardItemBundles, { id = itemBundle.id, count = itemBundle.count, rarity = itemData.rarity, type = itemData.type:ToInt() })
            end
        end
    end
    table.sort(mainRewardItemBundles, Utils.genSortFunction({ "rarity", "type", "id"}, false))
    self.view.mainRewardRoot.gameObject:SetActive(#mainRewardItemBundles > 0)
    self.m_mainRewardCellsCache = self.m_mainRewardCellsCache or UIUtils.genCellCache(self.view.mainRewardCell)
    self.m_mainRewardCellsCache:Refresh(#mainRewardItemBundles, function(cell, index)
        cell.item:InitItem(mainRewardItemBundles[index], true)
        cell.item:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        cell.getNode.gameObject:SetActive(mainGoalFinish)
    end)

    self.view.extraRewardFinished.gameObject:SetActive(extraGoalFinish)
    self.view.extraRewardUnfinished.gameObject:SetActive(not extraGoalFinish)
    local findExtraReward, extraRewardData = Tables.rewardTable:TryGetValue(dungeonCfg.extraRewardId or "")
    local extraRewardItemBundles = {}
    if findExtraReward then
        for _, itemBundle in pairs(extraRewardData.itemBundles) do
            local _, itemData = Tables.itemTable:TryGetValue(itemBundle.id)
            if itemData then
                table.insert(extraRewardItemBundles, { id = itemBundle.id, count = itemBundle.count, rarity = itemData.rarity, type = itemData.type:ToInt() })
            end
        end
    end
    table.sort(extraRewardItemBundles, Utils.genSortFunction({ "rarity", "type", "id"}, false))
    self.view.extraRewardRoot.gameObject:SetActive(#extraRewardItemBundles > 0)
    self.m_extraRewardCellCache = self.m_extraRewardCellCache or UIUtils.genCellCache(self.view.extraRewardCell)
    self.m_extraRewardCellCache:Refresh(#extraRewardItemBundles, function(cell, index)
        cell.item:InitItem(extraRewardItemBundles[index], true)
        cell.item:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        cell.getNode.gameObject:SetActive(extraGoalFinish)
    end)
end

BlackBoxTargetAndRewardCtrl._RefreshWarnings = HL.Method(HL.Table) << function(self, warningInfo)
    self.view.warningRoot.gameObject:SetActive(#warningInfo > 0)
    if #warningInfo > 0 then
        self.m_warningCellCache = self.m_warningCellCache or UIUtils.genCellCache(self.view.warningCell)
        self.m_warningCellCache = self.m_warningCellCache:Refresh(#warningInfo, function(cell, index)
            cell.text:SetAndResolveTextStyle(warningInfo[index])
        end)
    end
end

BlackBoxTargetAndRewardCtrl._OnBtnExitClick = HL.Method() << function(self)
    self:Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_EXIT_CURRENT_BLACKBOX_CONFIRM,
        onConfirm = function()
            self:_OnBtnCloseClick()
            GameInstance.dungeonManager:LeaveDungeon()
        end,
        onCancel = function()
        end
    })

end

BlackBoxTargetAndRewardCtrl._OnBtnCloseClick = HL.Method() << function(self)
    self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close)
end



BlackBoxTargetAndRewardCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.right.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end



HL.Commit(BlackBoxTargetAndRewardCtrl)
