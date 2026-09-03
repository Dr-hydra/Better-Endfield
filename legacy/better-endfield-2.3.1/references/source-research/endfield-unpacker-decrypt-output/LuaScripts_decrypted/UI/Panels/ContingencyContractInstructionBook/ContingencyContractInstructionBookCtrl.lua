local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ContingencyContractInstructionBook

ContingencyContractInstructionBookCtrl = HL.Class('ContingencyContractInstructionBookCtrl', uiCtrl.UICtrl)






ContingencyContractInstructionBookCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ContingencyContractInstructionBookCtrl.m_gameId = HL.Field(HL.String) << ""

ContingencyContractInstructionBookCtrl.m_infos = HL.Field(HL.Table)




ContingencyContractInstructionBookCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self.m_gameId = arg
    self:_InitData()
    self:_RefreshAllUI()
end



ContingencyContractInstructionBookCtrl._InitData = HL.Method() << function(self)
    self.m_infos = {}
    local _, ccLevelCfg = Tables.contingencyContractLevelTable:TryGetValue(self.m_gameId)
    for level, levelData in pairs(ccLevelCfg.levelMap) do
        local info = {
            score = level,
            rewardIcon = "",
            rewardCount = 0,
        }
        if not string.isEmpty(levelData.firstReward) then
            local item = UIUtils.getRewardFirstItem(levelData.firstReward)
            info.rewardCount = item.count
            local itemCfg = Tables.itemTable[item.id]
            info.rewardIcon = itemCfg.iconId
        end
        table.insert(self.m_infos, info)
    end
    table.sort(self.m_infos, function(a, b)
        return a.score < b.score
    end)
end



ContingencyContractInstructionBookCtrl._InitUI = HL.Method() << function(self)
    self.view.closeButton.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

ContingencyContractInstructionBookCtrl._RefreshAllUI = HL.Method() << function(self)
    for i = 1, ContingencyContractUtils.MAX_ROW_COUNT do
        local scoreTxt = self.view["scoreTxt" .. i]
        local rewardNumTxt = self.view["rewardNumTxt" .. i]
        local rewardIcon = self.view["rewardIcon" .. i]
        local info = self.m_infos[i]
        scoreTxt.text = info.score
        rewardNumTxt.text = info.rewardCount
        rewardIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, info.rewardIcon)
    end
end





HL.Commit(ContingencyContractInstructionBookCtrl)
