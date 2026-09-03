
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailContingencyContract

MapMarkDetailContingencyContractCtrl = HL.Class('MapMarkDetailContingencyContractCtrl', uiCtrl.UICtrl)

MapMarkDetailContingencyContractCtrl.m_activityId = HL.Field(HL.String) << ""





MapMarkDetailContingencyContractCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = '_OnStageUpdate',
}


MapMarkDetailContingencyContractCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    local commonArgs = {
        markInstId = arg.markInstId,
        bigBtnActive = true,
    }
    local _, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(commonArgs.markInstId)
    self.m_activityId = markRuntimeData.detail.activityId
    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)


    local gameId = Tables.activityContingencyContractTable[self.m_activityId].gameId
    local gameData = GameInstance.player.contingencyContractSystem:GetCcGameData(gameId)
    local score
    if gameData then
        score = gameData.historyBestScore
    end






    self.view.mapMarkDetailCommon.view.recordNumberTxt.text = score


    self:_StageUpdate()


    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

end

MapMarkDetailContingencyContractCtrl._OnStageUpdate = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if activityId ~= self.m_activityId then
        return
    end
    self:_StageUpdate()
end

MapMarkDetailContingencyContractCtrl._StageUpdate = HL.Method() << function(self)

    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local gameplayEndTime = activityData.gameplayEndTime
    local shopEndTime = activityData.endTime
    local currentTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local isGameplayEnd = gameplayEndTime - currentTime < 0
    if isGameplayEnd then
        self.view.mapMarkDetailCommon.view.timeTitleText.text = Language.LUA_ACTIVITY_CONTINGENCY_CONTRACT_MAP_SHOP_REMAIN_TIME_TEXT
        self.view.mapMarkDetailCommon.view.countDownWidget:InitCountDownText(shopEndTime)
    else
        self.view.mapMarkDetailCommon.view.timeTitleText.text = Language.LUA_ACTIVITY_CONTINGENCY_CONTRACT_MAP_PLAY_REMAIN_TIME_TEXT
        self.view.mapMarkDetailCommon.view.countDownWidget:InitCountDownText(gameplayEndTime)
    end
end


HL.Commit(MapMarkDetailContingencyContractCtrl)
