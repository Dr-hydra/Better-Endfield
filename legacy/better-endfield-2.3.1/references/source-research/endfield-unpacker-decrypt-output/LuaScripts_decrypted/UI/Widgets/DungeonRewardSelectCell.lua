local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

DungeonRewardSelectCell = HL.Class('DungeonRewardSelectCell', UIWidgetBase)

DungeonRewardSelectCell.m_index = HL.Field(HL.Number) << 0

DungeonRewardSelectCell.m_rewardId = HL.Field(HL.String) << ""

DungeonRewardSelectCell.m_rewardList = HL.Field(HL.Table)

DungeonRewardSelectCell.m_currSelected = HL.Field(HL.Boolean) << false

DungeonRewardSelectCell.m_clickCallback = HL.Field(HL.Function)

DungeonRewardSelectCell.m_rewardCells = HL.Field(HL.Forward("UIListCache"))


DungeonRewardSelectCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_rewardCells = UIUtils.genCellCache(self.view.itemBig)
    self.view.btnCommonSecondary.onClick:AddListener(function()
        self:m_clickCallback()
    end)
    self.view.rewardList.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

DungeonRewardSelectCell.InitDungeonRewardSelectCell = HL.Method(HL.String, HL.Boolean, HL.Function)
    << function(self, rewardId, isSelected, clickCallback)
    self.m_rewardId = rewardId
    self.m_currSelected = isSelected
    self.m_clickCallback = clickCallback
    self:_FirstTimeInit()

    self:_InitRewardData()
    self.m_rewardCells:Refresh(#self.m_rewardList, function(cell, index)
        local rewardInfo = self.m_rewardList[index]
        cell:InitItem(rewardInfo, function()
            UIUtils.showItemSideTips(cell)
        end)
        cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
    end)

    InputManagerInst:ToggleGroup(self.view.rewardTitleNode.groupId, not self.m_currSelected)
    local btnStateCtrl = self.view.root
    btnStateCtrl:SetState(self.m_currSelected and "DisableState" or "YellowState")
end

DungeonRewardSelectCell._InitRewardData = HL.Method() << function(self)
    local hasCfg, rewardsCfg = Tables.rewardTable:TryGetValue(self.m_rewardId)
    if not hasCfg then
        logger.error("[Reward Table] missing, id = "..self.m_rewardId)
        return
    end
    local rewards = {}
    for _, itemBundle in pairs(rewardsCfg.itemBundles) do
        local reward = rewards[itemBundle.id]
        if not reward then
            local itemCfg
            hasCfg, itemCfg = Tables.itemTable:TryGetValue(itemBundle.id)
            if hasCfg then
                reward = {
                    id = itemBundle.id,
                    count = itemBundle.count,
                    rarity = itemCfg.rarity,
                    type = itemCfg.type:ToInt(),
                }
                rewards[itemBundle.id] = reward
            else
                logger.error("[Item Table] missing, id = "..itemBundle.id)
                return
            end
        end
    end
    local rewardList = {}
    for _, v in pairs(rewards) do
        table.insert(rewardList, v)
    end
    table.sort(rewardList, Utils.genSortFunction({ "rarity", "type" }))
    self.m_rewardList = rewardList
end

HL.Commit(DungeonRewardSelectCell)
return DungeonRewardSelectCell

