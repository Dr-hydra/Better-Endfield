
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailSettlementDefenseTerminal

MapMarkDetailSettlementDefenseTerminalCtrl = HL.Class('MapMarkDetailSettlementDefenseTerminalCtrl', uiCtrl.UICtrl)






MapMarkDetailSettlementDefenseTerminalCtrl.s_messages = HL.StaticField(HL.Table) << {

}

MapMarkDetailSettlementDefenseTerminalCtrl.m_settlementId = HL.Field(HL.String) << ""

MapMarkDetailSettlementDefenseTerminalCtrl.m_markInstId = HL.Field(HL.String) << ""


MapMarkDetailSettlementDefenseTerminalCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_markInstId = arg.markInstId

    local commonArgs = {}
    commonArgs.bigBtnActive = true
    commonArgs.markInstId = self.m_markInstId
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)

    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(self.m_markInstId)

    if getRuntimeDataSuccess == false then
        logger.error("地图详情页获取实例数据失败" .. self.m_instId)
        return
    end

    self.m_settlementId = markRuntimeData.detail.settlementId
    local defenseGroupData = GameInstance.player.towerDefenseSystem:GetLastUnlockedDefenseGroup(self.m_settlementId)
    if defenseGroupData == nil then
        self.view.riskLevelCell.gameObject:SetActive(false)
        self.view.cautionNode.gameObject:SetActive(false)
        return
    end
    local _, groupTableData = Tables.towerDefenseGroupTable:TryGetValue(defenseGroupData.groupId)
    local levelCellView = self.view.riskLevelCell
    if groupTableData then
        levelCellView.nameText.text = groupTableData.name
    end
    levelCellView.lockStateController:SetState("Normal")
    levelCellView.selectionStateController:SetState("UnSelected")
    local isNormalCompleted = defenseGroupData.normalLevel and defenseGroupData.normalLevel.isCompleted
    levelCellView.manualDefenseCell:SetState(isNormalCompleted and "Completed" or "UnSelected")
    local isAutoCompleted = defenseGroupData.autoLevel and defenseGroupData.autoLevel.isCompleted
    levelCellView.defensePlanCell:SetState(isAutoCompleted and "Completed" or "UnSelected")

    self:_RefreshDefenseState()

    local settlementData = GameInstance.player.settlementSystem:GetUnlockSettlementData(self.m_settlementId)
    local isDuringBuff = settlementData and DateTimeUtils.GetCurrentTimestampBySeconds() < settlementData.tdGainEffectExpirationTs
    if isDuringBuff then
        self.view.cautionNode.countDown:InitCountDownText(settlementData.tdGainEffectExpirationTs, function()
            self:_RefreshDefenseState()
        end, UIUtils.getLeftTimeToSecond)
    end
end

MapMarkDetailSettlementDefenseTerminalCtrl._RefreshDefenseState = HL.Method() << function(self)
    local defenseState = GameInstance.player.towerDefenseSystem:GetSettlementDefenseState(self.m_settlementId)
    self.view.cautionNode.stateController:SetState(defenseState:ToString())
end

HL.Commit(MapMarkDetailSettlementDefenseTerminalCtrl)
