local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailCoin

MapMarkDetailCoinCtrl = HL.Class('MapMarkDetailCoinCtrl', uiCtrl.UICtrl)







MapMarkDetailCoinCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailCoinCtrl.m_countdownCor = HL.Field(HL.Any)


MapMarkDetailCoinCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local markInstId = arg.markInstId
    local _, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    local activityId = markRuntimeData.detail.activityId
    local success, cfg = Tables.activityTable:TryGetValue(activityId)
    if not success then
        logger.error("[MapMarkDetailCoin] Invalid activityId " .. tostring(activityId))
        return
    end
    local commonArgs = {}
    commonArgs.markInstId = markInstId
    commonArgs.bigBtnActive = true
    commonArgs.titleText = cfg.name
    commonArgs.descText = cfg.desc
    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)

    self:_RefreshCountdown(activityId)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end


MapMarkDetailCoinCtrl._RefreshCountdown = HL.Method(HL.String) << function(self, activityId)
    if self.m_countdownCor then
        self:_ClearCoroutine(self.m_countdownCor)
        self.m_countdownCor = nil
    end

    local activity = GameInstance.player.activitySystem:GetActivity(activityId)
    if not activity then
        logger.error("[MapMarkDetailCoin] Activity not found: " .. tostring(activityId))
        self.view.coinTimeNode.gameObject:SetActive(false)
        return
    end

    self.view.coinTimeNode.gameObject:SetActive(true)

    if activity.endTime == 0 then
        self.view.coinTimeNode.coinTimeNumTxt.text = Language.LUA_ACTIVITY_PERMANENT_TEXT
        return
    end

    local endTime = activity.endTime
    local function refresh()
        local leftSec = endTime - DateTimeUtils.GetCurrentTimestampBySeconds()
        if leftSec < 0 then leftSec = 0 end
        self.view.coinTimeNode.coinTimeNumTxt.text = UIUtils.getLeftTime(leftSec)
    end
    refresh()

    self.m_countdownCor = self:_StartCoroutine(function()
        while true do
            coroutine.wait(60)
            refresh()
            if endTime - DateTimeUtils.GetCurrentTimestampBySeconds() <= 0 then
                break
            end
        end
    end)
end





MapMarkDetailCoinCtrl.OnClose = HL.Override() << function(self)
    if self.m_countdownCor then
        self:_ClearCoroutine(self.m_countdownCor)
        self.m_countdownCor = nil
    end
end




HL.Commit(MapMarkDetailCoinCtrl)
