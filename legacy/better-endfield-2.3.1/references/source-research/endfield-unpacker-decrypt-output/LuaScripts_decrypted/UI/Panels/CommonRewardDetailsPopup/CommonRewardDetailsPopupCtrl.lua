
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonRewardDetailsPopup

CommonRewardDetailsPopupCtrl = HL.Class('CommonRewardDetailsPopupCtrl', uiCtrl.UICtrl)

CommonRewardDetailsPopupCtrl.m_args = HL.Field(HL.Table)

CommonRewardDetailsPopupCtrl.m_firstPartRewardCellCache = HL.Field(HL.Forward("UIListCache"))

CommonRewardDetailsPopupCtrl.m_secondPartRewardCellCache = HL.Field(HL.Forward("UIListCache"))





CommonRewardDetailsPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}










CommonRewardDetailsPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        self:_OnCloseBtnClick()
    end)

    self.view.mask.onClick:AddListener(function()
        self:_OnCloseBtnClick()
    end)

    self.m_args = arg
    self.m_firstPartRewardCellCache = UIUtils.genCellCache(self.view.firstPartRewardCell)
    self.m_secondPartRewardCellCache = UIUtils.genCellCache(self.view.secondPartRewardCell)

    self:_InitRewardDetails()
    self:_InitController()
end








CommonRewardDetailsPopupCtrl._OnCloseBtnClick = HL.Method() << function(self)
    if UIManager:IsShow(PanelId.ItemTips) then
        self:Notify(MessageConst.HIDE_ITEM_TIPS)
    else
        self:PlayAnimationOutAndClose()
    end
end

CommonRewardDetailsPopupCtrl._InitRewardDetails = HL.Method() << function(self)
    self:_InitFirstPartRewards()
    self:_InitSecondPartRewards()
end


CommonRewardDetailsPopupCtrl._InitFirstPartRewards = HL.Method() << function(self)

    local firstPartRewards = self.m_args.firstPartRewards
    local titleTxt = self.m_args.firstPartRewardsTitle or Language["ui_dungeon_reward_detail_complete"]
    self.view.firstPartTitleTxt.text = titleTxt
    self.m_firstPartRewardCellCache:Refresh(#firstPartRewards, function(cell, luaIndex)
        local reward = firstPartRewards[luaIndex]
        cell.getNode.gameObject:SetActive(reward.gained)
        cell.item:InitItem(reward, true)
    end)
end

CommonRewardDetailsPopupCtrl._InitSecondPartRewards = HL.Method() << function(self)

    local secondPartRewards = self.m_args.secondPartRewards or {}
    local titleTxt = self.m_args.secondPartRewardsTitle or Language["ui_dungeon_reward_detail_extra"]
    local secondRowRewardsCount = #secondPartRewards
    if secondRowRewardsCount > 0 then
        self.view.secondPartTitleTxt.text = titleTxt
        self.m_secondPartRewardCellCache:Refresh(secondRowRewardsCount, function(cell, luaIndex)
            local reward = secondPartRewards[luaIndex]
            cell.getNode.gameObject:SetActive(reward.gained == true)
            cell.item:InitItem(reward, true)
        end)
    end
    self.view.secondRewardsNode.gameObject:SetActive(secondRowRewardsCount > 0)
end

CommonRewardDetailsPopupCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.m_firstPartRewardCellCache:Update(function(cell, index)
        cell.item:SetExtraInfo({ isSideTips = true })
    end)

    self.m_secondPartRewardCellCache:Update(function(cell, index)
        cell.item:SetExtraInfo({ isSideTips = true })
    end)

    self.view.scrollView.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

HL.Commit(CommonRewardDetailsPopupCtrl)
