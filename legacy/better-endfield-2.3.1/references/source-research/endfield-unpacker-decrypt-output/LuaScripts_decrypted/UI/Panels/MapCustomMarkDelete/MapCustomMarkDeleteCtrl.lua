
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapCustomMarkDelete

MapCustomMarkDeleteCtrl = HL.Class('MapCustomMarkDeleteCtrl', uiCtrl.UICtrl)

MapCustomMarkDeleteCtrl.m_selectDeleteMarkTable = HL.Field(HL.Table)

MapCustomMarkDeleteCtrl.m_selectDeleteMarkCount = HL.Field(HL.Number) << 0

MapCustomMarkDeleteCtrl.m_argsInstId = HL.Field(HL.String) << ""

MapCustomMarkDeleteCtrl.m_selectLevelId = HL.Field(HL.String) << ""

MapCustomMarkDeleteCtrl.m_trackMark = HL.Field(HL.Userdata)





MapCustomMarkDeleteCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CUSTOM_MARK_MULTI_DELETE_SELECT] = '_OnSelectMark',
    [MessageConst.ON_MAP_MARK_RUNTIME_DATA_CHANGED] = '_OnDataChanged',
}


MapCustomMarkDeleteCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    Notify(MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL)
    self.view.backBtn.onClick:AddListener(function()
        Notify(MessageConst.HIDE_CUSTOM_MARK_MULTI_DELETE)
    end)

    self.view.deleteBtn.onClick:AddListener(function()
        self:_OnClickDelete()
    end)
    self.view.title.text = Language.LUA_MAP_CUSTOM_MARK_DELETE_DESC
    self.m_selectDeleteMarkTable = self.m_selectDeleteMarkTable or {}
    self.m_selectLevelId = args.levelId
    if args.instId then
        local data = GameInstance.player.mapManager:GetQuickSearchCustomMarkData(args.instId)
        if data then
            if not data.isSelect then
                self:_OnSelectMark(args)
            end
            self.m_argsInstId = args.instId
        end
    end
    self:_RefreshSelectText()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

MapCustomMarkDeleteCtrl.OnClose = HL.Override() << function(self)
    self:_OnClearMultiSelect()
end

MapCustomMarkDeleteCtrl._OnSelectMark = HL.Method(HL.Table) << function(self, args)
    local instId = args.instId
    local mark = args.mark
    if args.trackMark then
        self.m_trackMark = args.trackMark
    end
    self.m_selectDeleteMarkTable = self.m_selectDeleteMarkTable or {}
    if self.m_selectDeleteMarkTable[instId] then
        mark:RefreshCustomMarkDeleteState(false)
        self.m_selectDeleteMarkTable[instId] = nil
        self.m_selectDeleteMarkCount = self.m_selectDeleteMarkCount - 1
    else
        mark:RefreshCustomMarkDeleteState(true)
        self.m_selectDeleteMarkTable[instId] = mark
        self.m_selectDeleteMarkCount = self.m_selectDeleteMarkCount + 1
    end
    if args.trackMark then
        self.m_trackMark:RefreshCustomMarkDeleteState(not self.m_trackMark:GetCustomMarkDeleteState())
    end
    self:_RefreshSelectText()
    Notify(MessageConst.ON_CUSTOM_MARK_MULTI_DELETE_SELECT_STATE_CHANGED, {
        instId = instId,
        isSelect = self.m_selectDeleteMarkTable[instId] ~= nil
    })
end

MapCustomMarkDeleteCtrl._RefreshSelectText = HL.Method() << function(self)
    local curNum = self.m_selectDeleteMarkCount
    local maxNum = GameInstance.player.mapManager:GetQuickSearchCustomMarkCountByLevel(self.m_selectLevelId)

    local data
    if self.m_argsInstId ~= "" then
        data = GameInstance.player.mapManager:GetQuickSearchCustomMarkData(self.m_argsInstId)
        if data and data.isSelect and maxNum > 0 then
            maxNum = maxNum - 1
        end
    end
    self.view.isSelectText.text = string.format("%d/%d", curNum, maxNum)
end

MapCustomMarkDeleteCtrl._OnDataChanged = HL.Method(HL.Table) << function(self, args)
    local instId, isAdd = unpack(args)
    self:_RefreshSelectText()
end

MapCustomMarkDeleteCtrl._OnClickDelete = HL.Method() << function(self)
    if not self.m_selectDeleteMarkTable then
        return
    end
    local ids = {}
    for markInstId, _ in pairs(self.m_selectDeleteMarkTable) do
        if markInstId == GameInstance.player.mapManager.trackingMarkInstId then
            GameInstance.player.mapManager:TrackMark(markInstId, false)
        end
        table.insert(ids, markInstId)
    end

    GameInstance.player.mapManager:RemoveCustomMarkToServer(ids)
    if not self.m_selectDeleteMarkTable or not next(self.m_selectDeleteMarkTable)then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_MAP_CUSTOM_MARK_DELETE_FAIL)
    end
    self.m_selectDeleteMarkTable = {}
    self.m_selectDeleteMarkCount = 0
    self:_RefreshSelectText()
end

MapCustomMarkDeleteCtrl._OnClearMultiSelect = HL.Method() << function(self)
    if not self.m_selectDeleteMarkTable or not next(self.m_selectDeleteMarkTable) then
        return
    end
    for instId, mark in pairs(self.m_selectDeleteMarkTable) do
        mark:RefreshCustomMarkDeleteState(false)
        if instId == GameInstance.player.mapManager.trackingMarkInstId then
            self.m_trackMark:RefreshCustomMarkDeleteState(false)
        end
    end
    self.m_trackMark = nil
    self.m_selectDeleteMarkTable = {}
    self.m_selectDeleteMarkCount = 0
end

MapCustomMarkDeleteCtrl.IsCustomMarkSelectedToDelete = HL.Method(HL.String).Return(HL.Boolean) << function(self, markInstId)
    return self.m_selectDeleteMarkTable[markInstId] ~= nil
end

HL.Commit(MapCustomMarkDeleteCtrl)
