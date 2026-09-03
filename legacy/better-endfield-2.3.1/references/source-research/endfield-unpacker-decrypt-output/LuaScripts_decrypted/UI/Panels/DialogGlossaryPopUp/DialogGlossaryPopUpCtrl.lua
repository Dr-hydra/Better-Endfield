
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DialogGlossaryPopUp

DialogGlossaryPopUpCtrl = HL.Class('DialogGlossaryPopUpCtrl', uiCtrl.UICtrl)


DialogGlossaryPopUpCtrl.m_noteCells = HL.Field(HL.Forward("UIListCache"))

DialogGlossaryPopUpCtrl.m_noteLinkIds = HL.Field(HL.Userdata) << nil

DialogGlossaryPopUpCtrl.m_dialogAutoMode = HL.Field(HL.Boolean) << false





DialogGlossaryPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {

}


DialogGlossaryPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_noteCells = UIUtils.genCellCache(self.view.noteCellTemplate)
    self.m_noteLinkIds = unpack(args)

    self.view.closeButton.onClick:RemoveAllListeners()
    self.view.closeButton.onClick:AddListener(function()
        self:TryCloseFromPhase()
    end)

    self.view.maskButton.onClick:RemoveAllListeners()
    self.view.maskButton.onClick:AddListener(function()
        self:TryCloseFromPhase()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

DialogGlossaryPopUpCtrl.TryCloseFromPhase = HL.Method() << function(self)
    if self:IsPlayingAnimationOut() or self.m_isClosed then
        return
    end

    self:PlayAnimationOutWithCallback(function()
        if self.m_phase and self.m_phaseItem then
            self.m_phase:RemovePhasePanelItem(self.m_phaseItem)
        else
            self:Close()
        end
    end)
end

DialogGlossaryPopUpCtrl.OnShow = HL.Override() << function(self)
    if GameWorld.dialogTimelineManager.autoMode then
        self.m_dialogAutoMode = true
        GameWorld.dialogTimelineManager:SetAutoMode(false)
    end

    self.m_noteCells:Refresh(self.m_noteLinkIds.Count, function(cell, luaIndex)
        local linkId = self.m_noteLinkIds[CSIndex(luaIndex)]
        local res, noteEntryData = Tables.narrativeNoteEntryTable:TryGetValue(linkId)
        local entryName, entryDesc
        if res then
            entryName = noteEntryData.entryName
            entryDesc = noteEntryData.entryDesc
        else
            entryName = "NoteEntry Id Not Find: " .. linkId
            entryDesc = "NoteEntry Id Not Find: " .. linkId
        end

        cell.titleTxt:SetAndResolveTextStyle(UIUtils.resolveTextCinematic(entryName))
        cell.descTxt:SetAndResolveTextStyle(UIUtils.resolveTextCinematic(entryDesc))
    end)
end

DialogGlossaryPopUpCtrl.OnClose = HL.Override() << function(self)
    if GameWorld.dialogTimelineManager.autoMode ~= self.m_dialogAutoMode then
        GameWorld.dialogTimelineManager:SetAutoMode(self.m_dialogAutoMode)
    end
end


DialogGlossaryPopUpCtrl.OnOpenDialogGlossaryPopUp = HL.StaticMethod(HL.Opt(HL.Any)) << function(arg)

    if not BEYOND_DEBUG and not BEYOND_DEBUG_COMMAND then
        return
    end

    if arg == nil then
        return
    end
    local list = unpack(arg)
    if list == nil or list.Count == nil or list.Count == 0 then
        return
    end
    if UIManager:IsOpen(PANEL_ID) then
        UIManager:Close(PANEL_ID)
    end
    UIManager:Open(PANEL_ID, { list })
end

HL.Commit(DialogGlossaryPopUpCtrl)
