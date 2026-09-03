
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivitySkipChapter1Confirm

ActivitySkipChapter1ConfirmCtrl = HL.Class('ActivitySkipChapter1ConfirmCtrl', uiCtrl.UICtrl)

ActivitySkipChapter1ConfirmCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SKIP_CHAPTER_SUCCESS] = '_OnSkipChapterSuccess',
}

ActivitySkipChapter1ConfirmCtrl.m_activityId = HL.Field(HL.String) << ''

ActivitySkipChapter1ConfirmCtrl.m_pressCor = HL.Field(HL.Any)
ActivitySkipChapter1ConfirmCtrl.m_pressCompleted = HL.Field(HL.Boolean) << false
ActivitySkipChapter1ConfirmCtrl.m_skipRequested = HL.Field(HL.Boolean) << false

ActivitySkipChapter1ConfirmCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.pressImage.fillAmount = 0

    self.view.forceConfirmButton.onPressStart:AddListener(function()
        self:_StartPressFill()
    end)

    self.view.forceConfirmButton.onPressEnd:AddListener(function()
        self:_StopPressFill()
    end)

    self.view.forceConfirmButton.onLongPress:AddListener(function()

        if self.m_skipRequested then
            return
        end
        self.m_skipRequested = true
        self:_FinishPressFill()
        local _, skipChapterData = Tables.activitySkipChapterTable:TryGetValue(self.m_activityId)
        if skipChapterData then
            GameInstance.player.activitySystem:SendDoSkipChapter(skipChapterData.skipChapterConfigId)
        end
    end)

    self.view.confirmButton.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
end

ActivitySkipChapter1ConfirmCtrl._StartPressFill = HL.Method() << function(self)
    self:_StopPressFill()
    local duration = self.view.forceConfirmButton.longPressTime
    self.m_pressCompleted = false
    self.view.pressImage.fillAmount = 0
    self.m_pressCor = self:_StartCoroutine(function()
        local startTime = Time.unscaledTime
        while true do
            coroutine.step()
            local elapsed = Time.unscaledTime - startTime
            local progress = math.min(elapsed / duration, 1)
            self.view.pressImage.fillAmount = progress
            if progress >= 1 then
                break
            end
        end
    end)
end

ActivitySkipChapter1ConfirmCtrl._FinishPressFill = HL.Method() << function(self)
    if self.m_pressCor then
        self.m_pressCor = self:_ClearCoroutine(self.m_pressCor)
    end
    self.m_pressCompleted = true
    self.view.pressImage.fillAmount = 1
end

ActivitySkipChapter1ConfirmCtrl._StopPressFill = HL.Method() << function(self)
    if self.m_pressCor then
        self.m_pressCor = self:_ClearCoroutine(self.m_pressCor)
    end
    if not self.m_pressCompleted then
        self.view.pressImage.fillAmount = 0
    end
end

ActivitySkipChapter1ConfirmCtrl._OnSkipChapterSuccess = HL.Method(HL.Any) << function(self, arg)
    local skipChapterConfigId = arg and arg[1] or nil
    local bindDlgId = nil
    if skipChapterConfigId ~= nil and skipChapterConfigId ~= "" then
        local hasData, skipChapterData = Tables.skipChapterTable:TryGetValue(skipChapterConfigId)
        if hasData and skipChapterData then
            bindDlgId = skipChapterData.bindDlgId
        end
    end

    self:Close()
    PhaseManager:ExitPhaseFastTo(PhaseId.Level, true)

    if bindDlgId ~= nil and bindDlgId ~= "" then
        GameAction.StartDialog(bindDlgId)
    end
end

HL.Commit(ActivitySkipChapter1ConfirmCtrl)
