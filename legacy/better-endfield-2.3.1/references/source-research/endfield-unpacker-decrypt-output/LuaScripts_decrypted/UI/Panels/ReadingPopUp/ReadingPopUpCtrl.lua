local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ReadingPopUp
local PHASE_ID = PhaseId.ReadingPopUp

ReadingPopUpCtrl = HL.Class('ReadingPopUpCtrl', uiCtrl.UICtrl)







ReadingPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ReadingPopUpCtrl.m_arg = HL.Field(HL.Table)

ReadingPopUpCtrl.m_readId = HL.Field(HL.String) << ""

ReadingPopUpCtrl.m_handle = HL.Field(HL.Any)

ReadingPopUpCtrl.m_onCloseCallback = HL.Field(HL.Any)


ReadingPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.m_arg = arg
    self.m_readId = arg.richContentId or ""
    self.m_handle = arg.handle
    self.m_onCloseCallback = arg.closeCallback
end

ReadingPopUpCtrl._ShowContent = HL.Virtual() << function(self)
    local hasCfg = Tables.richContentTable:TryGetValue(self.m_readId)
    if hasCfg then
        EventLogManagerInst:GameEvent_ReadNarrativeContent(self.m_readId)
        GameInstance.player.readingSystem:SaveReadNarrativeContentTime(self.m_readId)
        self.view.richContent:SetContentById(self.m_readId)
    else
        logger.error("图文id不存在：【" .. tostring(self.m_readId) .. "】")
    end
end

ReadingPopUpCtrl.OnShow = HL.Override() << function(self)
    self:_ShowContent()
end

ReadingPopUpCtrl.OnClose = HL.Override() << function(self)
    if not string.isEmpty(self.m_readId) then
        local hasRichContentCfg, _ = Tables.richContentTable:TryGetValue(self.m_readId)
        if hasRichContentCfg then
            local stayTime = GameInstance.player.readingSystem:GetReadNarrativeContentTime(self.m_readId)
            EventLogManagerInst:GameEvent_CloseNarrativeContent(self.m_readId, stayTime)
        end
    end
    if not string.isEmpty(self.m_arg.readingPopId) then
        GameInstance.player.readingSystem:ReqSetRichContentReadingPopFinish(self.m_arg.readingPopId)
    elseif not string.isEmpty(self.m_arg.richContentId) then
        logger.warn("ReadingPopup发送已读时遇到错误！readingPopId为空，将使用richContentId上报：" .. self.m_arg.richContentId)
        GameInstance.player.readingSystem:ReqSetRichContentReadingPopFinish(self.m_arg.richContentId)
    else
        logger.warn("ReadingPopup发送已读时遇到错误！readingPopId为空，将使用radioId上报：" .. self.m_arg.radioId)
        GameInstance.player.readingSystem:ReqSetRichContentReadingPopFinish(self.m_arg.radioId)
    end
    if self.m_onCloseCallback ~= nil then
        self.m_onCloseCallback()
    end
    if self.m_handle then
        self.m_handle:Finish()
        self.m_handle = nil
    end
end

HL.Commit(ReadingPopUpCtrl)
