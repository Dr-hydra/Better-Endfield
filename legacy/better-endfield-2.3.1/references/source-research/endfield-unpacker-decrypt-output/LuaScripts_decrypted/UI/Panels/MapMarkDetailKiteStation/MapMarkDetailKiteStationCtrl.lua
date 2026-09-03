
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailKiteStation

MapMarkDetailKiteStationCtrl = HL.Class('MapMarkDetailKiteStationCtrl', uiCtrl.UICtrl)






MapMarkDetailKiteStationCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailKiteStationCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local commonArgs = {
        markInstId = arg.markInstId,
    }

    local _, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(commonArgs.markInstId)
    local kiteStationInsId = markRuntimeData.detail.systemInstId
    local kiteStationData = GameInstance.player.kiteStationSystem:GetKiteStationDataByInstId(kiteStationInsId)
    if kiteStationData.level == 0 then
        local kiteStationCfg = Tables.kiteStationLevelTable[kiteStationInsId].list[1]
        local unlockQuestState = GameInstance.player.mission:GetQuestState(kiteStationCfg.upgradeQuestId)
        local unlockMissionId = GameInstance.player.mission:GetMissionIdByQuestId(kiteStationCfg.upgradeQuestId)
        if unlockQuestState == CS.Beyond.Gameplay.MissionSystem.QuestState.Processing then
            commonArgs.jumpInfo = {
                onJump = function()
                    PhaseManager:OpenPhase(PhaseId.Mission, {autoSelect = unlockMissionId, useBlackMask = true})
                end,
                jumpText = kiteStationCfg.upgradeQuestDesc,
            }
        elseif not string.isEmpty(kiteStationCfg.upgradeQuestId) then
            commonArgs.hintInfo = {
                hintText = Language.LUA_KITE_STATION_MAP_MARK_DETAIL_HINT_TEXT,
                importantHint = true,
            }
            commonArgs.descText = ""
        else
            commonArgs.bigBtnActive = true
        end
        self.view.levelStateNode.gameObject:SetActive(false)
    else
        commonArgs.bigBtnActive = true
        local isMaxLevel = kiteStationData.level == Tables.GlobalConst.kiteStationMaxLevel
        self.view.levelStateNode:SetState(isMaxLevel and "Max" or "Nrl")
        self.view.lvText.text = kiteStationData.level
        self.view.levelStateNode.gameObject:SetActive(true)
    end

    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)
end











HL.Commit(MapMarkDetailKiteStationCtrl)
