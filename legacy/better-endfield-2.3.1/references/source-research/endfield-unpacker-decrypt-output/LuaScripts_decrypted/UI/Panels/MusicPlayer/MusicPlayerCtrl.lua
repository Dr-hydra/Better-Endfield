
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MusicPlayer

MusicPlayerCtrl = HL.Class('MusicPlayerCtrl', uiCtrl.UICtrl)

MusicPlayerCtrl.m_currentGuestRoomMusicId = HL.Field(HL.String) << ""

MusicPlayerCtrl.m_currentSelectMusicId = HL.Field(HL.String) << ""

MusicPlayerCtrl.m_currentPlayedMusicId = HL.Field(HL.String) << ""

MusicPlayerCtrl.m_currentGuestRoomAlbumId = HL.Field(HL.String) << ""

MusicPlayerCtrl.m_currentAlbumIndex = HL.Field(HL.Number) << -1

MusicPlayerCtrl.m_unlockMusicNumList = HL.Field(HL.Table)

MusicPlayerCtrl.m_unlockMusicMap = HL.Field(HL.Table)

MusicPlayerCtrl.m_albumIdList = HL.Field(HL.Table)

MusicPlayerCtrl.m_currentMusicList = HL.Field(HL.Table)

MusicPlayerCtrl.m_filteredMusicList = HL.Field(HL.Table)

MusicPlayerCtrl.m_filterTags = HL.Field(HL.Table)

MusicPlayerCtrl.m_filterSettings = HL.Field(HL.Table)

MusicPlayerCtrl.m_getAlbumItemCell = HL.Field(HL.Function)

MusicPlayerCtrl.m_getItemCell = HL.Field(HL.Function)

MusicPlayerCtrl.m_curSelectCell = HL.Field(HL.Any)

MusicPlayerCtrl.m_sortIncremental = HL.Field(HL.Boolean) << false

MusicPlayerCtrl.m_isViewingFriend = HL.Field(HL.Boolean) << false

MusicPlayerCtrl.m_isChooseMusicController = HL.Field(HL.Boolean) << false

MusicPlayerCtrl.m_isChangeInputDeviceType = HL.Field(HL.Boolean) << false

MusicPlayerCtrl.m_isIncremental = HL.Field(HL.Boolean) << false

MusicPlayerCtrl.m_timeMs = HL.Field(HL.Number) << 0

MusicPlayerCtrl.m_confirmAlbumBindingId = HL.Field(HL.Number) << -1

MusicPlayerCtrl.m_timeUpdateThread = HL.Field(HL.Thread)

local REFRESH_TIME_INTERVAL = 0.1
local SYNC_TIME_INTERVAL = 3

local BottomState = {
    Lock = "Lock",
    Unlock = "Unlock",
}

local ButtonState = {
    Normal = "NormalState",
    Lock = "NotObtainedState",
    Current = "CurrentState",
}





MusicPlayerCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_MUSIC_PLAYER_PLAY] = '_OnMusicPlayerPlay',
    [MessageConst.ON_SET_GUEST_ROOM_MUSIC_SUCCESS] = '_OnSetGuestRoomMusicSuccess',
    [MessageConst.ON_CONFIRM_CHANGE_INPUT_DEVICE_TYPE] = '_OnChangeInputDeviceType',
}


MusicPlayerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_isViewingFriend = arg.isViewingFriend
    self.m_filterTags = arg.filterTags
    self.m_isIncremental = arg.isIncremental or false
    self.m_filterSettings = arg.filterSettings
    self:_InitBtn()
    self:_InitFilterNode()
    self:_InitSortNode()
    self:_InitMusicData(arg.currentSelectMusicId, arg.currentPlayedMusicId)
    self:_PrepareAlbumData()
    self:_RefreshAlbumList(arg.currentAlbumIndex)
    self:_RefreshCurrentMusicInfo()
    self:_InitRedDot()
    self:_InitController()
    GameInstance.audioManager.music:EnableVisualize(true, CS.Beyond.Gameplay.Audio.AudioMusicSystem.EVisualizePresetType.DIJIANG_PLAYER)
end

MusicPlayerCtrl.OnClose = HL.Override() << function(self)
    self.m_timeUpdateThread = self:_ClearCoroutine(self.m_timeUpdateThread)
    self.m_curSelectCell = nil

    if not self.m_isChangeInputDeviceType then
        if self.m_currentPlayedMusicId ~= self.m_currentGuestRoomMusicId then
            local musicData = Tables.spaceshipMusicTable[self.m_currentGuestRoomMusicId]
            GameInstance.player.spaceship:PlayMusic(musicData.musicEvent)
        end

        if not self.m_isViewingFriend then
            for musicId in pairs(self.m_unlockMusicMap) do
                RedDotUtils.setMusicRead(musicId)
            end
        end

        local currentGuestRoomMusicData = Tables.spaceshipMusicTable[self.m_currentGuestRoomMusicId]
        if self.m_currentGuestRoomAlbumId ~= currentGuestRoomMusicData.albumId then
            GameInstance.player.spaceship:ChangeAlbum(currentGuestRoomMusicData.albumId)
        end
    end
    GameInstance.audioManager.music:EnableVisualize(false)
end

MusicPlayerCtrl._InitBtn = HL.Method() << function(self)
    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.MusicPlayer)
    end)

    self.view.downState.setasBtn.onClick:AddListener(function()
        self:_SetGuestRoomMusic()
    end)
end

MusicPlayerCtrl._SetGuestRoomMusic = HL.Method() << function(self)
    GameInstance.player.spaceship:SetGuestRoomMusic(self.m_currentSelectMusicId)
end

MusicPlayerCtrl._InitMusicData = HL.Method(HL.Opt(HL.String, HL.String)) << function(self, currentSelectMusicId, currentPlayedMusicId)
    self.m_unlockMusicNumList = {}
    self.m_unlockMusicMap = {}
    local musicId = GameInstance.player.spaceship:GetCurrentMusicId()
    self.m_currentGuestRoomMusicId = (musicId == nil or musicId == "") and Tables.spaceshipConst.musicPlayerDefaultMusic or musicId
    if currentSelectMusicId ~= nil then
        self.m_currentSelectMusicId = currentSelectMusicId
        self.m_currentPlayedMusicId = currentPlayedMusicId
    else
        self.m_currentSelectMusicId = self.m_currentGuestRoomMusicId
        self.m_currentPlayedMusicId = self.m_currentGuestRoomMusicId
        local musicData = Tables.spaceshipMusicTable[self.m_currentGuestRoomMusicId]
        self.m_currentGuestRoomAlbumId = musicData.albumId
    end

    local unlockList = GameInstance.player.spaceship:GetUnlockMusicIds()
    if unlockList ~= nil then
        for i = 1, unlockList.Count do
            local musicId = unlockList[CSIndex(i)]
            local musicData = Tables.spaceshipMusicTable[musicId]
            local unlockNum = self.m_unlockMusicNumList[musicData.albumId]
            if unlockNum == nil then
                self.m_unlockMusicNumList[musicData.albumId] = 1
            else
                self.m_unlockMusicNumList[musicData.albumId] = unlockNum + 1
            end
            self.m_unlockMusicMap[musicId] = true
        end
    end
end

MusicPlayerCtrl._RefreshAlbumList = HL.Method(HL.Opt(HL.Number)) << function(self, currentAlbumIndex)
    if currentAlbumIndex == nil then
        local musicData = Tables.spaceshipMusicTable[self.m_currentSelectMusicId]
        local currentAlbumId = musicData.albumId
        for i, info in ipairs(self.m_albumIdList) do
            if info.id == currentAlbumId then
                self.m_currentAlbumIndex = i
                break
            end
        end
    else
        self.m_currentAlbumIndex = currentAlbumIndex
    end

    local scrollView = self.view.leftNode.recordScrollView
    if not self.m_getAlbumItemCell then
        self.m_getAlbumItemCell = UIUtils.genCachedCellFunction(scrollView)
        scrollView.onUpdateCell:AddListener(function(obj, csIndex)
            self:_OnUpdateAlbumCell(self.m_getAlbumItemCell(obj), LuaIndex(csIndex))
        end)
        scrollView.onScrollEnd:AddListener(function(csIndex)
            local index = LuaIndex(csIndex)
            self.m_currentAlbumIndex = index
            self:_RefreshMusicList()
            self:_RefreshAlbumInfo(index)
            self.view.leftNode.animL:SkipInAnimation()
            self.view.leftNode.animL:PlayInAnimation()
        end)
    end

    local index = self.m_currentAlbumIndex
    self:_RefreshAlbumInfo(index)
    scrollView:UpdateCount(#self.m_albumIdList)
    scrollView:ScrollToIndex(CSIndex(index), true)
    local currentObj = scrollView:GetCurrentCell()
    local currentCell = self.m_getItemCell(currentObj)
    self:SetNaviTarget(currentCell.inputBindingGroupNaviDecorator)
    self:_RefreshMusicList()

    if currentAlbumIndex == nil then
        for i, info in ipairs(self.m_filteredMusicList) do
            if info.id == self.m_currentSelectMusicId then
                self.view.rightNode.musicListScrollList:ScrollToIndex(CSIndex(i), true, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)
                break
            end
        end
    end
end

MusicPlayerCtrl._RefreshAlbumInfo = HL.Method(HL.Number) << function(self, index)
    local albumId = self.m_albumIdList[index].id
    local albumInfo = Tables.spaceshipAlbumTable[albumId]

    self.view.leftNode.titleTxt.text = albumInfo.albumName

    local unlockNum = self.m_unlockMusicNumList[albumId]
    if unlockNum == nil then
        unlockNum = 0
    end
    self.view.leftNode.collectTxt.text = tostring(unlockNum)
end

MusicPlayerCtrl._PrepareAlbumData = HL.Method() << function(self)
    self.m_albumIdList = {}
    for id, albumData in pairs(Tables.spaceshipAlbumTable) do
        table.insert(self.m_albumIdList, {
            id = id,
            sortId = albumData.order
        })
    end

    table.sort(self.m_albumIdList, Utils.genSortFunction({"sortId"}, true))
end

MusicPlayerCtrl._OnUpdateAlbumCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local id = self.m_albumIdList[index].id
    local info = Tables.spaceshipAlbumTable[id]
    cell.recordImg:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_MUSIC_PLAYER, info.icon)
    cell.recordImgButton.onClick:RemoveAllListeners()
    cell.recordImgButton.onClick:AddListener(function()
        self.view.leftNode.recordScrollView:ScrollToObject(cell.gameObject)
    end)

    local musicData = Tables.spaceshipMusicTable[self.m_currentGuestRoomMusicId]
    cell.sureTabs.gameObject:SetActiveIfNecessary(musicData.albumId == id)

    if not self.m_isViewingFriend then
        cell.redDot:InitRedDot("SSMusicPlayerAlbum", id)
    else
        cell.redDot.gameObject:SetActiveIfNecessary(false)
    end
end

MusicPlayerCtrl._RefreshMusicList = HL.Method() << function(self)
    self:_PrepareMusicData()

    local scrollView = self.view.rightNode.musicListScrollList
    if not self.m_getItemCell then
        self.m_getItemCell = UIUtils.genCachedCellFunction(scrollView)
        scrollView.onUpdateCell:AddListener(function(obj, csIndex)
            self:_OnUpdateMusicCell(self.m_getItemCell(obj), LuaIndex(csIndex))
        end)
    end

    self:_UpdateMusicList()
end

MusicPlayerCtrl._UpdateMusicList = HL.Method() << function(self)
    local scrollView = self.view.rightNode.musicListScrollList
    local count = #self.m_filteredMusicList
    if count == 0 then
        self.view.rightNode.emptyNode.gameObject:SetActiveIfNecessary(true)
        scrollView.gameObject:SetActiveIfNecessary(false)
    else
        self.view.rightNode.emptyNode.gameObject:SetActiveIfNecessary(false)
        scrollView.gameObject:SetActiveIfNecessary(true)
        scrollView:UpdateCount(count)
    end
end

MusicPlayerCtrl._PrepareMusicData = HL.Method() << function(self)
    self.m_currentMusicList = {}
    local currentAlbumId = self.m_albumIdList[self.m_currentAlbumIndex].id

    local success, albumInfo = Tables.spaceshipAlbumMusicTable:TryGetValue(currentAlbumId)
    if success then
        for _, musicId in pairs(albumInfo.musicList) do
            local musicData = Tables.spaceshipMusicTable[musicId]
            local isUnlock = self:_IsUnlock(musicId) and 1 or 0
            if isUnlock == 0 and musicData.musicShowMissionId then
                if not GameInstance.player.mission:IsMissionCompleted(musicData.musicShowMissionId) then
                    goto continue
                end
            end
            if self.m_isViewingFriend and isUnlock == 0 then
                goto continue
            end

            table.insert(self.m_currentMusicList, {
                id = musicId,
                sortId = musicData.order,
                isUnlock = isUnlock,
                isLock = 1 - isUnlock,
            })
            ::continue::
        end
    end

    self:_FilterCurrentMusicList()
    self:_SortMusicList(self.view.rightNode.sortNodeDown:GetCurSortData(), self.view.rightNode.sortNodeDown.isIncremental)
end

MusicPlayerCtrl._FilterCurrentMusicList = HL.Method() << function(self)
    if self.m_filterTags == nil or #self.m_filterTags == 2 then
        self.m_filteredMusicList = self.m_currentMusicList
    else
        self.m_filteredMusicList = {}
        local unlock = self.m_filterTags[1].isUnlock
        for _, v in ipairs(self.m_currentMusicList) do
            if (v.isUnlock == 1) == unlock then
                table.insert(self.m_filteredMusicList, v)
            end
        end
    end
end

MusicPlayerCtrl._OnUpdateMusicCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local id = self.m_filteredMusicList[index].id
    local isSelected = (id == self.m_currentSelectMusicId)
    local isGuestRoomMusic = (id == self.m_currentGuestRoomMusicId)
    local isLock = not self:_IsUnlock(id)
    cell:SetState(isGuestRoomMusic, isLock, isSelected)
    cell.view.button.onClick:RemoveAllListeners()
    cell.view.button.onClick:AddListener(function()
        if self.m_curSelectCell ~= nil then
            self.m_curSelectCell:SetSelectState(false)
        end
        self.m_curSelectCell = cell
        cell:SetSelectState(true)


        if self.m_currentSelectMusicId ~= id then
            self.m_currentSelectMusicId = id
            self:_RefreshCurrentMusicInfo()
            if self:_IsUnlock(id) and not self.m_isViewingFriend then
                RedDotUtils.setMusicRead(id)
                Notify(MessageConst.ON_SET_MUSIC_READ)
            end
        end
    end)

    if self.m_currentSelectMusicId == id then
        self.m_curSelectCell = cell
    end

    local hasValue, itemCfg = Tables.itemTable:TryGetValue(id)
    if not hasValue then
        logger.error("物品表里没有这个音乐", id)
        return
    end

    cell.view.nameText.text = itemCfg.name
    if isLock or self.m_isViewingFriend then
        cell.view.redDot.gameObject:SetActiveIfNecessary(false)
    else
        if isSelected then
            RedDotUtils.setMusicRead(id)
            Notify(MessageConst.ON_SET_MUSIC_READ)
        end
        cell.view.redDot:InitRedDot("SSMusicPlayerSingleMusic", id, nil, self.view.rightNode.redDotScrollRect)
    end
end

MusicPlayerCtrl._RefreshCurrentMusicInfo = HL.Method() << function(self)
    local node = self.view.downState
    local id = self.m_currentSelectMusicId
    local isUnlock = self:_IsUnlock(id)
    node.stateController:SetState(isUnlock and BottomState.Unlock or BottomState.Lock)

    if id == self.m_currentGuestRoomMusicId then
        node.root:SetState(ButtonState.Current)
    else
        node.root:SetState(isUnlock and ButtonState.Normal or ButtonState.Lock)
        if not DeviceInfo.usingController then
            self.view.downState.setasBtn.interactable = isUnlock
        end
    end
    self.view.downState.inputBindingGroupMonoTarget.enabled = isUnlock and id ~= self.m_currentGuestRoomMusicId

    local hasValue, itemCfg = Tables.itemTable:TryGetValue(id)
    if not hasValue then
        return
    end

    node.nameTxt.text = itemCfg.name

    if isUnlock then
        local musicData = Tables.spaceshipMusicTable[id]
        node.descTxt.text = itemCfg.decoDesc
        node.timeTxt2.text = UIUtils.getLeftTimeToSecond(musicData.duration)
        if id ~= self.m_currentPlayedMusicId then
            self.m_currentPlayedMusicId = id
            if MusicPlayerCtrl._IsDefaultMusic(id) then
                GameInstance.player.spaceship:PlayMusic(musicData.musicEvent)
                self:_SyncCurrentMusicTime()
                self:_RefreshCurrentTime()
            else
                self.m_timeUpdateThread = self:_ClearCoroutine(self.m_timeUpdateThread)
                GameInstance.player.spaceship:PlayMusic(musicData.musicEventSample)
            end
        else
            if self.m_timeUpdateThread == nil then
                local timeMs = GameInstance.player.spaceship:GetCurrentMusicPlayingPosition(MusicPlayerCtrl._IsDefaultMusic(id))
                self:_OnMusicPlayerPlay({timeMs})
            else
                self:_RefreshCurrentTime()
            end
        end
    else

        if itemCfg.obtainWayIds ~= nil then
            for _, obtainWayId in pairs(itemCfg.obtainWayIds) do
                local _, obtainWayCfg = Tables.systemJumpTable:TryGetValue(obtainWayId)
                if obtainWayCfg then
                    node.descTxt.text = string.format(Language.LUA_MUSIC_PLAYER_LOCK_DES, obtainWayCfg.desc)
                end
            end
        else
            node.descTxt.text = ""
        end
    end
end

MusicPlayerCtrl._IsUnlock = HL.Method(HL.String).Return(HL.Boolean) << function(self, musicId)
    return self.m_unlockMusicMap[musicId] == true
end

MusicPlayerCtrl._InitSortNode = HL.Method() << function(self)
    local SORT_OPTION = {
        {
            name = Language.LUA_CHAR_SORT_1,
            keys = { "isUnlock", "sortId"},
            reverseKeys = { "isLock", "sortId"},
        }
    }
    self.view.rightNode.sortNodeDown:InitSortNode(SORT_OPTION, function(optData, isIncremental)
        self.m_isIncremental = isIncremental
        self:_SortMusicList(optData, isIncremental)
        self:_UpdateMusicList()
    end, 0, self.m_isIncremental, true, self.view.rightNode.filterBtn)
end

MusicPlayerCtrl._SortMusicList = HL.Method(HL.Table, HL.Boolean) << function(self, optData, isIncremental)
    self.m_sortIncremental = isIncremental
    local keys = isIncremental and optData.reverseKeys or optData.keys
    table.sort(self.m_filteredMusicList, Utils.genSortFunction(keys, isIncremental))
end

MusicPlayerCtrl._InitFilterNode = HL.Method() << function(self)
    if self.m_filterSettings == nil then
        self.m_filterSettings = {
            {
                name = Language.LUA_MUSIC_PLAYER_FILTER_UNLOCK,
                isUnlock = true,
                isOn = false,
            },
            {
                name = Language.LUA_MUSIC_PLAYER_FILTER_LOCK,
                isUnlock = false,
                isOn = false,
            }
        }
        if self.m_isViewingFriend then
            self.m_filterSettings[2] = nil
        end
    end

    local filterArgs = {
        tagGroups = {{tags = self.m_filterSettings}},
        onConfirm = function(tags)
            self:_OnFilterConfirm(tags)
        end,
        selectedTags = self.m_filterTags,
        getResultCount = function(tags)
            return self:_FilterBtnGetResCount(tags)
        end,
        sortNodeWidget = self.view.rightNode.sortNodeDown,
    }
    self.view.rightNode.filterBtn:InitFilterBtn(filterArgs)
end

MusicPlayerCtrl._OnFilterConfirm = HL.Method(HL.Any) << function(self, tags)
    self.m_filterTags = tags
    self:_FilterCurrentMusicList()
    self:_SortMusicList(self.view.rightNode.sortNodeDown:GetCurSortData(), self.view.rightNode.sortNodeDown.isIncremental)
    self:_UpdateMusicList()

    if DeviceInfo.usingController and #self.m_filteredMusicList == 0 and self.m_isChooseMusicController then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_MUSIC_NO_MUSIC_TOAST_DESC)
        self:_HighlightAlbumController()
    end
end

MusicPlayerCtrl._FilterBtnGetResCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    if tags == nil or #tags == 0 then
        return 0
    end

    local count = 0
    for _, tag in ipairs(tags) do
        for _, musicInfo in ipairs(self.m_currentMusicList) do
            if tag.isUnlock == (musicInfo.isUnlock == 1) then
                count = count + 1
            end
        end
    end
    return count
end

MusicPlayerCtrl._OnMusicPlayerPlay = HL.Method(HL.Table) << function(self, timeMs)
    self.m_timeUpdateThread = self:_ClearCoroutine(self.m_timeUpdateThread)
    if MusicPlayerCtrl._IsDefaultMusic(self.m_currentPlayedMusicId) then
        self.m_timeMs = unpack(timeMs)
    else
        self:_SyncCurrentMusicTime()
    end

    self:_RefreshCurrentTime()
    self.m_timeUpdateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(REFRESH_TIME_INTERVAL)
            local msBefore = self.m_timeMs
            self.m_timeMs = self.m_timeMs + REFRESH_TIME_INTERVAL * 1000
            local timeS = math.floor(self.m_timeMs / 1000 + 0.5)
            if timeS % SYNC_TIME_INTERVAL == 0 then
                self:_SyncCurrentMusicTime()
                timeS = math.floor(self.m_timeMs / 1000 + 0.5)
            end

            if self.m_currentSelectMusicId == self.m_currentPlayedMusicId then
                local musicData = Tables.spaceshipMusicTable[self.m_currentPlayedMusicId]
                if msBefore / 1000 >= musicData.duration then
                    self:_SyncCurrentMusicTime()
                    timeS = math.floor(self.m_timeMs / 1000 + 0.5)
                end

                if timeS > musicData.duration then
                    self.m_timeMs = musicData.duration * 1000
                end
                self:_RefreshCurrentTime()
            end
        end
    end)
end

MusicPlayerCtrl._SyncCurrentMusicTime = HL.Method() << function(self)
    local timeMs = GameInstance.player.spaceship:GetCurrentMusicPlayingPosition(MusicPlayerCtrl._IsDefaultMusic(self.m_currentPlayedMusicId))
    self.m_timeMs = timeMs
end

MusicPlayerCtrl._RefreshCurrentTime = HL.Method() << function(self)
    if self.m_currentSelectMusicId ~= self.m_currentPlayedMusicId then
        return
    end

    local second = self.m_timeMs / 1000
    self.view.downState.timeTxt1.text = UIUtils.getLeftTimeToSecond(second)
end

MusicPlayerCtrl._OnSetGuestRoomMusicSuccess = HL.Method(HL.Table) << function(self, musicId)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_MUSIC_TOAST_SET_GUEST_ROOM_MUSIC)
    local lastIndex = self:_GetCurrentGuestRoomMusicIndex()
    if lastIndex > 0 then
        local cellObj = self.view.rightNode.musicListScrollList:Get(CSIndex(lastIndex))
        if cellObj then
            local cell = self.m_getItemCell(cellObj)
            cell:SetIsGuestRoomMusic(false)
        end
    end
    self.m_currentGuestRoomMusicId = unpack(musicId)
    if self.m_currentGuestRoomMusicId == self.m_currentSelectMusicId then
        local node = self.view.downState
        node.root:SetState(ButtonState.Current)
        self.view.downState.inputBindingGroupMonoTarget.enabled = isUnlock and id ~= self.m_currentGuestRoomMusicId
    end
    local currentIndex = self:_GetCurrentGuestRoomMusicIndex()
    if currentIndex > 0 then
        local cellObj = self.view.rightNode.musicListScrollList:Get(CSIndex(currentIndex))
        if cellObj then
            local cell = self.m_getItemCell(cellObj)
            cell:SetState(true, false, true)
        end
    end
    self.view.leftNode.recordScrollView:UpdateCount(#self.m_albumIdList)
    self:_HighlightMusicController(true)

    EventLogManagerInst:GameEvent_SpaceshipMusicPlayerSetGuestRoomMusic(self.m_currentGuestRoomMusicId, self.m_isViewingFriend and "friend" or "own")
end

MusicPlayerCtrl._GetCurrentGuestRoomMusicIndex = HL.Method().Return(HL.Number) << function(self)
    for i = 1, #self.m_filteredMusicList do
        if self.m_filteredMusicList[i].id == self.m_currentGuestRoomMusicId then
            return i
        end
    end
    return -1
end

MusicPlayerCtrl._IsDefaultMusic = HL.StaticMethod(HL.String).Return(HL.Boolean) << function(musicId)
    return musicId == Tables.spaceshipConst.musicPlayerDefaultMusic
end

MusicPlayerCtrl._InitRedDot = HL.Method() << function(self)
    if self.m_isViewingFriend then
        return
    end

    self.view.rightNode.redDotScrollRect.getRedDotStateAt = function(csIndex)
        return self:_GetRedDotStateAt(csIndex)
    end
end

MusicPlayerCtrl._GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local luaIndex = LuaIndex(index)
    if luaIndex < 1 or luaIndex > #self.m_filteredMusicList then
        return 0
    end

    local hasRedDot, redDotType = RedDotManager:GetRedDotState("SSMusicPlayerSingleMusic", self.m_filteredMusicList[luaIndex].id)
    if hasRedDot then
        return redDotType
    end

    return 0
end

MusicPlayerCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self:_HighlightAlbumController()
    self:_BindingActions()
end

MusicPlayerCtrl._BindingActions = HL.Method() << function(self)
    self.m_confirmAlbumBindingId = self:BindInputPlayerAction("music_player_confirm", function()
        self:_HighlightMusicController(false)
    end)
    self:BindInputPlayerAction("music_player_back", function()
        if self.m_isChooseMusicController then
            self:_HighlightAlbumController()
        else
            PhaseManager:PopPhase(PhaseId.MusicPlayer)
        end
    end)
    self:BindInputPlayerAction("music_player_left", function()
        if self.m_isChooseMusicController then
            self:_HighlightAlbumController()
        end
    end)
    self:BindInputPlayerAction("music_player_right", function()
        self:_HighlightMusicController(false)
    end)
end

MusicPlayerCtrl._HighlightMusicController = HL.Method(HL.Boolean) << function(self, silence)
    if #self.m_filteredMusicList == 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_MUSIC_NO_MUSIC_TOAST_DESC)
        return
    end
    self.m_isChooseMusicController = true
    if not silence then
        self.view.rightNode.musicListScrollList:ScrollToIndex(0, true, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)
    end
    self.view.rightNode.musicListScrollListSelectableNaviGroup:NaviToThisGroup()
    if not silence then
        local firstObj = self.view.rightNode.musicListScrollList:Get(0)
        local firstCell = self.m_getItemCell(firstObj)
        self:SetNaviTarget(firstCell.view.button)
    end
    InputManagerInst:ToggleBinding(self.m_confirmAlbumBindingId, false)
end

MusicPlayerCtrl._HighlightAlbumController = HL.Method() << function(self)
    if self.m_confirmAlbumBindingId ~= -1 then
        InputManagerInst:ToggleBinding(self.m_confirmAlbumBindingId, true)
    end
    self.view.leftNode.recordScrollViewSelectableNaviGroup:NaviToThisGroup()
    self.m_isChooseMusicController = false
end

MusicPlayerCtrl._OnChangeInputDeviceType = HL.Method(HL.Any) << function(self, args)
    self.m_isChangeInputDeviceType = true
end

HL.Commit(MusicPlayerCtrl)
