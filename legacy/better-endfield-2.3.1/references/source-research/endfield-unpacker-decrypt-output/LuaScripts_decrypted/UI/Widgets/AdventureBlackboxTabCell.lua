local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

AdventureBlackboxTabCell = HL.Class('AdventureBlackboxTabCell', UIWidgetBase)


AdventureBlackboxTabCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_genRewardCells = UIUtils.genCellCache(self.view.rewardCell)
    self.view.goToBtn.onClick:RemoveAllListeners()
    self.view.goToBtn.onClick:AddListener(function()
        if PhaseManager:IsOpen(PhaseId.BlackboxEntry) then
            PhaseManager:ExitPhaseFast(PhaseId.BlackboxEntry)
        end
        PhaseManager:OpenPhase(PhaseId.BlackboxEntry, { packageId = self.m_info.packageId })
    end)
    self.view.rewardListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

AdventureBlackboxTabCell.m_genRewardCells = HL.Field(HL.Forward("UIListCache"))

AdventureBlackboxTabCell.m_info = HL.Field(HL.Table)

AdventureBlackboxTabCell.InitAdventureBlackboxTabCell = HL.Method(HL.Table) << function(self, info)
    self:_FirstTimeInit()

    if info.isLock then
        self.view.tabState:SetState("Empty")
        return
    end

    if info.isNextVersion then
        self.view.tabState:SetState("StayTuned")
        return
    end

    self.view.tabState:SetState("Normal")
    self.m_info = info
    local node = self.view
    node.iconImg:LoadSprite(UIConst.UI_SPRITE_ADVENTURE, info.icon)
    node.bgImg:LoadSprite(UIConst.UI_SPRITE_ADVENTURE, info.bg)
    node.curCountTxt.text = info.curCount
    node.curCount01Txt.text = info.curCount
    node.targetCountTxt.text = info.targetCount
    node.titleTxt.text = info.title
    local domainColor = UIUtils.getColorByString(info.color)
    local numColor = UIUtils.getColorByString(info.numColor)
    node.colorGroup.color = domainColor
    node.targetCountTextBg.color = numColor
    self.m_genRewardCells:Refresh(#self.m_info.rewardList, function(cell, luaIndex)
        local rewardInfo = self.m_info.rewardList[luaIndex]
        cell:InitItem(rewardInfo, function()
            UIUtils.showItemSideTips(cell)
        end)
        cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
    end)
    node.redDot:InitRedDot("AdventureBlackboxCell", info.blackboxIds)
end

AdventureBlackboxTabCell.InitEmptyState = HL.Method() << function(self)
    self:_FirstTimeInit()
    self.view.tabState:SetState("Empty")
end

HL.Commit(AdventureBlackboxTabCell)
return AdventureBlackboxTabCell

