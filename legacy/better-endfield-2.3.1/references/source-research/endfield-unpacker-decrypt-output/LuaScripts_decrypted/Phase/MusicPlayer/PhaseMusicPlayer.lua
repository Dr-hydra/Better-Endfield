
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.MusicPlayer
PhaseMusicPlayer = HL.Class('PhaseMusicPlayer', phaseBase.PhaseBase)





PhaseMusicPlayer.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.OPEN_MUSIC_PLAYER_PANEL] = { 'OpenMusicPlayerPanel', false },
}

PhaseMusicPlayer.OpenMusicPlayerPanel = HL.StaticMethod(HL.Table) << function(args)
    PhaseManager:OpenPhase(PhaseId.MusicPlayer, {isViewingFriend = unpack(args)})
end



PhaseMusicPlayer._OnInit = HL.Override() << function(self)
    PhaseMusicPlayer.Super._OnInit(self)
end

PhaseMusicPlayer.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    local panelCtrl = self.m_panel2Item[PanelId.MusicPlayer].uiCtrl
    arg.currentAlbumIndex = panelCtrl.m_currentAlbumIndex
    arg.currentSelectMusicId = panelCtrl.m_currentSelectMusicId
    arg.currentPlayedMusicId = panelCtrl.m_currentPlayedMusicId
    arg.filterTags = panelCtrl.m_filterTags
    arg.isIncremental = panelCtrl.m_isIncremental
    arg.filterSettings = panelCtrl.m_filterSettings
    arg.hasShowPopup = not UIManager:IsOpen(PanelId.MusicPlayerPopup)
    return arg
end




PhaseMusicPlayer.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseMusicPlayer._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.arg.hasShowPopup then
        return
    end

    local isViewingFriend = GameInstance.player.spaceship.isViewingFriend
    if not isViewingFriend then
        for albumId in pairs(Tables.spaceshipAlbumTable) do
            local hasRedDot = RedDotManager:GetRedDotState("SSMusicPlayerAlbum", albumId)
            if hasRedDot then
                UIManager:Open(PanelId.MusicPlayerPopup)
                break
            end
        end
    end
end

PhaseMusicPlayer._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseMusicPlayer._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseMusicPlayer._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseMusicPlayer._OnActivated = HL.Override() << function(self)
end

PhaseMusicPlayer._OnDeActivated = HL.Override() << function(self)
end

PhaseMusicPlayer._OnDestroy = HL.Override() << function(self)
    PhaseMusicPlayer.Super._OnDestroy(self)
end




HL.Commit(PhaseMusicPlayer)

