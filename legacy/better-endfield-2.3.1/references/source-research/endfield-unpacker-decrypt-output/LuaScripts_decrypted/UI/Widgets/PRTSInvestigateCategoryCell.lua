local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

PRTSInvestigateCategoryCell = HL.Class('PRTSInvestigateCategoryCell', UIWidgetBase)



PRTSInvestigateCategoryCell.m_genCollCell = HL.Field(HL.Forward("UIListCache"))




PRTSInvestigateCategoryCell.m_infoBundle = HL.Field(HL.Table)

PRTSInvestigateCategoryCell.m_isNoteShown = HL.Field(HL.Boolean) << false

PRTSInvestigateCategoryCell.m_clientHasReadTrick = HL.Field(HL.Boolean) << false

PRTSInvestigateCategoryCell.m_onCollFocusChanged = HL.Field(HL.Function)



PRTSInvestigateCategoryCell._OnFirstTimeInit = HL.Override() << function(self)

    self.m_forbidTitleInputGroupRecord = {}
    self.m_genCollCell = UIUtils.genCellCache(self.view.collCell)
    self.view.noteBtn.onClick:RemoveAllListeners()
    self.view.noteBtn.onClick:AddListener(function()

        Notify(MessageConst.PRTS_CHANGE_INVESTIGATE_GALLERY_NOTE_VISIBLE, { isShow = true, index = self.m_infoBundle.index })
    end)
    if DeviceInfo.usingController then
        self:_ForbidTitleInputGroupController("Navi", true)
    end
end

PRTSInvestigateCategoryCell.InitPRTSInvestigateCategoryCell = HL.Method(HL.Table, HL.String, HL.Opt(HL.Function)) << function(self, infoBundle, investId, onCollFocusChanged)
    self:_FirstTimeInit()


    self.m_infoBundle = infoBundle
    self.m_infoBundle.investId = investId
    self.m_onCollFocusChanged = onCollFocusChanged
    local viewRef = self.view


    viewRef.titleTxt.text = infoBundle.title
    local noteCnt = #infoBundle.noteInfos
    local noteCountText = string.format(Language.LUA_PRTS_NOTE_COUNT_FORMAT, noteCnt)
    viewRef.noteCountTxt1.text = noteCountText
    viewRef.noteCountTxt2.text = noteCountText
    viewRef.noteCountTxt3.text = noteCountText

    self:RefreshUINoteShowState(infoBundle.showNote, infoBundle.isPlayNoteAni)

    self.m_genCollCell:Refresh(#infoBundle.collInfos, function(cell, luaIndex)
        self:_OnRefreshCollCell(cell, luaIndex)
    end)

    if not self.m_infoBundle.showNote and self:_HasNoteUnread() and not self.m_clientHasReadTrick then
        self.view.animationNode.gameObject:SetActiveIfNecessary(true)
        self.view.animationNode:PlayInAnimation()
    else
        self.view.animationNode.gameObject:SetActiveIfNecessary(false)
    end

    if noteCnt == 0 then
        self.view.noteBtnState:SetState("NoNote")
    end
    self.view.noteBtn.interactable = noteCnt ~= 0
    self:_ForbidTitleInputGroupController("ShowNote", infoBundle.showNote or noteCnt == 0)
end

PRTSInvestigateCategoryCell.ForceRefreshNoteCell = HL.Method(HL.Table) << function(self, noteInfos)
    self.m_infoBundle.noteInfos = noteInfos
    self:RefreshUINoteShowState(self.m_infoBundle.showNote, false)
end

PRTSInvestigateCategoryCell._OnRefreshCollCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    cell.gameObject.name = "CollCell" .. luaIndex
    local info = self.m_infoBundle.collInfos[luaIndex]
    cell.iconImg:LoadSprite(UIConst.UI_SPRITE_PRTS_ICON, info.imgPath)
    cell.nameTxt:SetAndResolveTextStyle(UIUtils.resolveTextCinematic(info.name or ""))
    cell.gotoBtn.onClick:RemoveAllListeners()
    cell.gotoBtn.onClick:AddListener(function()
        if self.m_onCollFocusChanged then
            self.m_onCollFocusChanged(info.collId)
        end
        local ids = {}
        for _, collInfo in pairs(self.m_infoBundle.collInfos) do
            table.insert(ids, collInfo.collId)
        end

        if PhaseManager:IsOpen(PhaseId.PRTSStoryCollDetail) then
            PhaseManager:ExitPhaseFast(PhaseId.PRTSStoryCollDetail)
        end
        PhaseManager:GoToPhase(PhaseId.PRTSStoryCollDetail, {
            isFirstLvId = false,
            idList = ids,
            initShowIndex = luaIndex,
            showGotoBtn = false,
            researchId = self.m_infoBundle.investId,
        })
    end)
    if DeviceInfo.usingController then
        cell.gotoBtn.onIsNaviTargetChanged = function(isTarget)
            self:_ForbidTitleInputGroupController("Navi", not isTarget)
            if isTarget and self.m_onCollFocusChanged then
                self.m_onCollFocusChanged(info.collId)
            end
        end
    end
    info.hasRead = true
    cell.redDot:InitRedDot("PRTSItem", info.collId)
end

PRTSInvestigateCategoryCell._OnRefreshNoteCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    cell.gameObject.name = "NoteCell" .. luaIndex
    local info = self.m_infoBundle.noteInfos[luaIndex]
    cell.indexTxt.text = string.format(Language.LUA_PRTS_NOTE_INDEX_FORMAT, info.index + 1)
    cell.descTxt:SetAndResolveTextStyle(UIUtils.resolveTextCinematic(info.desc))
    cell.redDot:InitRedDot("PRTSNote", info.noteId)
    info.hasRead = true
end

PRTSInvestigateCategoryCell.RefreshUINoteShowState = HL.Method(HL.Boolean, HL.Boolean) << function(self, isShow, playAni)
    self.m_isNoteShown = isShow

    if isShow then


        self.m_clientHasReadTrick = true
    end
    self:_ForbidTitleInputGroupController("ShowNote", isShow)

    if isShow then
        self.view.noteBtnState:SetState("NoteShown")
    elseif self:_HasNoteUnread() and not self.m_clientHasReadTrick then
        self.view.noteBtnState:SetState("HasNew")
    else
        self.view.noteBtnState:SetState("Normal")
    end
end

PRTSInvestigateCategoryCell._HasNoteUnread = HL.Method().Return(HL.Boolean) << function(self)
    for _, info in pairs(self.m_infoBundle.noteInfos) do
        if GameInstance.player.prts:IsNoteUnread(info.noteId) then
            return true
        end
    end
    return false
end

PRTSInvestigateCategoryCell.m_forbidTitleInputGroupRecord = HL.Field(HL.Table)

PRTSInvestigateCategoryCell._ForbidTitleInputGroupController = HL.Method(HL.String, HL.Boolean) << function(self, key, isForbid)
    if not DeviceInfo.usingController then
        return
    end
    if isForbid then
        self.m_forbidTitleInputGroupRecord[key] = true
    else
        self.m_forbidTitleInputGroupRecord[key] = nil
    end
    if next(self.m_forbidTitleInputGroupRecord) then
        InputManagerInst:ToggleGroup(self.view.titleInputGroup.groupId, false)
    else
        InputManagerInst:ToggleGroup(self.view.titleInputGroup.groupId, true)
    end
end

HL.Commit(PRTSInvestigateCategoryCell)
return PRTSInvestigateCategoryCell

