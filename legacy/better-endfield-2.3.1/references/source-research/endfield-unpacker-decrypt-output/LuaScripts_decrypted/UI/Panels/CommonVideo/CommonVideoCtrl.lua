
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonVideo
CommonVideoCtrl = HL.Class('CommonVideoCtrl', uiCtrl.UICtrl)

CommonVideoCtrl.m_coroutine = HL.Field(HL.Thread)

CommonVideoCtrl.m_isPlaying = HL.Field(HL.Boolean) << false






CommonVideoCtrl.s_messages = HL.StaticField(HL.Table) << {

}


CommonVideoCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.playPauseBtn.onClick:AddListener(function()
        self:_TogglePlayPause()
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

CommonVideoCtrl._OnShowVideo = HL.StaticMethod(HL.String) << function(videoPath)
    local self = UIManager:AutoOpen(PANEL_ID)

    if self.m_coroutine ~= nil then
        self:_ClearCoroutine(self.m_coroutine)
        self.m_coroutine = nil
    end
    local success, file = CS.Beyond.Gameplay.View.VideoManager.TryGetVideoPlayFullPath(videoPath)
    if success then
        self.view.videoNode.player:EasySetFile(nil, file)
        self.view.videoNode.player:EasyStart()
        self.m_isPlaying = true

        self.view.videoNode.player.applyTargetAlpha = true
        self.m_coroutine = self:_StartCoroutine(function()
            while true do
                while self.view.videoNode.player == nil do
                    coroutine.step()
                end

                local status = self.view.videoNode.player.status
                if status == CS.CriWare.CriMana.Player.Status.Dechead or status == CS.CriWare.CriMana.Player.Status.WaitPrep or status == CS.CriWare.CriMana.Player.Status.Prep then
                    coroutine.step()
                else
                    if self.m_isPlaying and status ~= CS.CriWare.CriMana.Player.Status.PlayEnd then
                        self.view.iconPlay.gameObject:SetActiveIfNecessary(false)
                        self.view.iconPause.gameObject:SetActiveIfNecessary(true)
                    else
                        self.view.iconPlay.gameObject:SetActiveIfNecessary(true)
                        self.view.iconPause.gameObject:SetActiveIfNecessary(false)
                    end
                    local totalSeconds = self.view.videoNode.player:GetVideoLength()
                    local curSeconds = self.view.videoNode.player:GetTime() / 1e6
                    self.view.timeText.text = UIUtils.getRemainingTextToMinute(curSeconds) .. "/" .. UIUtils.getRemainingTextToMinute(totalSeconds)
                    self.view.progressBar.fillAmount = curSeconds / totalSeconds
                    coroutine.step()
                end
            end
        end)
    end
end

CommonVideoCtrl.OnClose = HL.Override() << function(self)
    if self.m_coroutine ~= nil then
        self:_ClearCoroutine(self.m_coroutine)
        self.m_coroutine = nil
    end
end

CommonVideoCtrl._TogglePlayPause = HL.Method() << function(self)
    local status = self.view.videoNode.player.status
    if status == CS.CriWare.CriMana.Player.Status.PlayEnd then
        self.view.videoNode.player:EasyStart()
        return
    end

    if self.m_isPlaying then
        self.view.videoNode.player:EasyPause(true)
    else
        self.view.videoNode.player:EasyPause(false)
    end
    self.m_isPlaying = not self.m_isPlaying
end

HL.Commit(CommonVideoCtrl)
