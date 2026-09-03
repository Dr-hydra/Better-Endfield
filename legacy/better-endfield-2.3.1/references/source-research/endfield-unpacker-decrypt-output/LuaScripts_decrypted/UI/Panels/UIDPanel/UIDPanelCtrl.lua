
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.UIDPanel

local NetClientInst = GameInstance.netClientManager
UIDPanelCtrl = HL.Class('UIDPanelCtrl', uiCtrl.UICtrl)

UIDPanelCtrl.m_updatePingValueCor = HL.Field(HL.Thread)

UIDPanelCtrl.m_pingThresholdValueTbl = HL.Field(HL.Table)

UIDPanelCtrl.m_pingColorStrTbl = HL.Field(HL.Table)

UIDPanelCtrl.m_pingInProgress = HL.Field(HL.Boolean) << false







UIDPanelCtrl.s_messages = HL.StaticField(HL.Table) << {
}


UIDPanelCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if BEYOND_INNER_DEBUG then
        self.view.text.text = string.format("UID: %s VER:%s", GameInstance.player.playerInfoSystem.roleId, CS.Beyond.GlobalOptions.instance.lastCL)
    else
        self.view.text.text = string.format("UID: %s", GameInstance.player.playerInfoSystem.roleId)
    end

    pcall(function()
        local channel = CS.Beyond.Cfg.RemoteNetworkCfg.instance.data.channel
        if string.find(channel, "inner") then
            self.view.text01.text = Language.LUA_TALPHA_INNER_ALERT
        end
    end)

    local enablePing = self:_EnablePing()
    self.view.pingCon.gameObject:SetActive(enablePing)
    if enablePing then
        self:_ProcessPingCfg()
        self.m_updatePingValueCor = self:_StartCoroutine(function()
            while true do
                self:_UpdatePingInfo()
                coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            end
        end)
    end
end

UIDPanelCtrl.OnClose = HL.Override() << function(self)
    if self.m_updatePingValueCor then
        self.m_updatePingValueCor = self:_ClearCoroutine(self.m_updatePingValueCor)
    end
end

UIDPanelCtrl._EnablePing = HL.Method().Return(HL.Boolean) << function(self)
    if CS.Beyond.CloudGame.enabled then
        return false
    end
    return true
end

UIDPanelCtrl._ProcessPingCfg = HL.Method() << function(self)
    local splitChar = ","
    local valueStrTbl = string.split(self.view.config.PING_VALUE_STAGE, splitChar)
    self.m_pingThresholdValueTbl = {}
    for _, valueStr in ipairs(valueStrTbl) do
        table.insert(self.m_pingThresholdValueTbl, tonumber(valueStr))
    end
    self.m_pingColorStrTbl = string.split(self.view.config.PING_COLOR_STAGE, splitChar)
end

UIDPanelCtrl._UpdatePingInfo = HL.Method() << function(self)
    local pingValue = NetClientInst:GetPing()
    local pingWarning = pingValue < 0 or pingValue >= 999
    local clampPingValue = pingWarning and 999 or pingValue
    self.view.textNode:SetState(pingWarning and "Warning" or "Normal" )
    self.view.pingNubTxt.text = string.format("%sms", clampPingValue)
    if pingWarning then
        return
    end

    local stage = 1
    for i = #self.m_pingThresholdValueTbl, 1, -1 do
        local pingStage = self.m_pingThresholdValueTbl[i]
        if clampPingValue >= pingStage then
            stage = i
            break
        end
    end

    local color = self.m_pingColorStrTbl[stage]
    self.view.pingCon.color = UIUtils.getColorByString(color)
end

UIDPanelCtrl.OnEnterMainGame = HL.StaticMethod() << function()
    UIManager:Open(PANEL_ID)
end

HL.Commit(UIDPanelCtrl)
