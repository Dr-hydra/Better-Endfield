local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MusicPlayerListCell = HL.Class('MusicPlayerListCell', UIWidgetBase)

MusicPlayerListCell.m_isGuestRoomMusic = HL.Field(HL.Boolean) << false

MusicPlayerListCell.m_isSelect = HL.Field(HL.Boolean) << false

MusicPlayerListCell.m_isLock = HL.Field(HL.Boolean) << false

local UIState = {
    Normal = "Normal",
    Selected = "Selected",
    Lock = "Lock",
    LockSelected = "LockSelected",
    NormalCurrent = "NormalCurrent",
    SelectedCurrent = "SelectedCurrent",
}


MusicPlayerListCell._OnFirstTimeInit = HL.Override() << function(self)

end

MusicPlayerListCell.InitMusicPlayerListCell = HL.Method() << function(self)
    self:_FirstTimeInit()


end

MusicPlayerListCell.SetState = HL.Method(HL.Boolean, HL.Boolean, HL.Boolean) << function(self, isGuestRoomMusic, isLock, isSelect)
    self:SetIsGuestRoomMusic(isGuestRoomMusic)
    self.m_isLock = isLock
    self.m_isSelect = isSelect
    if isSelect then
        self.view.nodeStateAnimationWrapper:SampleToInAnimationEnd()
    else
        self.view.nodeStateAnimationWrapper:SampleToOutAnimationEnd()
    end
    self:_RefreshState()
end

MusicPlayerListCell.SetIsGuestRoomMusic = HL.Method(HL.Boolean) << function(self, isGuestRoomMusic)
    self.m_isGuestRoomMusic = isGuestRoomMusic
    self.view.currentNode.gameObject:SetActiveIfNecessary(isGuestRoomMusic)
end

MusicPlayerListCell.SetSelectState = HL.Method(HL.Boolean) << function(self, isSelect)
    if self.m_isSelect ~= isSelect then
        if isSelect then
            self.view.nodeStateAnimationWrapper:PlayInAnimation()
        else
            self.view.nodeStateAnimationWrapper:PlayOutAnimation()
        end
    end
    self.m_isSelect = isSelect
    self:_RefreshState()
end

MusicPlayerListCell._RefreshState = HL.Method() << function(self)
    self.view.iconLock.gameObject:SetActiveIfNecessary(self.m_isLock)

    if self.m_isSelect then
        if self.m_isGuestRoomMusic then
            self:_SetState(UIState.SelectedCurrent)
        end
        self:_SetState(self.m_isLock and UIState.LockSelected or UIState.Selected)
    else
        if self.m_isGuestRoomMusic then
            self:_SetState(UIState.NormalCurrent)
        end
        self:_SetState(self.m_isLock and UIState.Lock or UIState.Normal)
    end
end

MusicPlayerListCell._SetState = HL.Method(HL.String) << function(self, state)
    self.view.nodeState:SetState(state)
end

HL.Commit(MusicPlayerListCell)
return MusicPlayerListCell

