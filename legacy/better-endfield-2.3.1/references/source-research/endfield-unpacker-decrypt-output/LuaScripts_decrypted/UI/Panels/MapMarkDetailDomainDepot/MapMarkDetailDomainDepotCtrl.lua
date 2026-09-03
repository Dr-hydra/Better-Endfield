local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailDomainDepot

MapMarkDetailDomainDepotCtrl = HL.Class('MapMarkDetailDomainDepotCtrl', uiCtrl.UICtrl)






MapMarkDetailDomainDepotCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailDomainDepotCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local commonArgs = {
        markInstId = arg.markInstId,
    }

    local _, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(commonArgs.markInstId)
    local domainDepotId = markRuntimeData.detail.systemInstId

    local unlockAvailable = GameInstance.player.domainDepotSystem:IsDomainDepotUnlockAvailable(domainDepotId)
    local domainDepotInfo = DomainDepotUtils.GetDepotInfo(domainDepotId)
    if domainDepotInfo.currLevel > 0 then
        local isMaxLevel = domainDepotInfo.currLevel == domainDepotInfo.maxLevel and domainDepotInfo.isFinalMaxLevel
        self.view.levelStateNode.stateController:SetState(isMaxLevel and "Max" or "Normal")
        self.view.levelStateNode.levelTxt.text = string.format("%d", domainDepotInfo.currLevel)
        self.view.levelStateNode.gameObject:SetActive(true)
    else
        self.view.levelStateNode.gameObject:SetActive(false)
    end

    if unlockAvailable then
        commonArgs.bigBtnActive = true
    else
        local unlockMissionId, unlockMissionState = GameInstance.player.domainDepotSystem:GetDomainDepotUnlockMissionState(domainDepotId)
        local domainDepotCfg = Tables.domainDepotTable[domainDepotId]
        if unlockMissionState == CS.Beyond.Gameplay.MissionSystem.MissionState.Processing then
            commonArgs.jumpInfo = {
                onJump = function()
                    PhaseManager:OpenPhase(PhaseId.Mission, {autoSelect = unlockMissionId, useBlackMask = true})
                end,
                jumpText = domainDepotCfg.unlockQuestDesc,
            }
        elseif unlockMissionState ~= CS.Beyond.Gameplay.MissionSystem.MissionState.Completed then
            commonArgs.hintInfo = {
                hintText = Language.LUA_DOMAIN_DEPOT_MAP_MARK_DETAIL_HINT_TEXT,
                importantHint = true,
            }
        end
    end

    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)
end

HL.Commit(MapMarkDetailDomainDepotCtrl)
