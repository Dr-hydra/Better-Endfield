local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapCustomMarkDetail
MapCustomMarkDetailCtrl = HL.Class('MapCustomMarkDetailCtrl', uiCtrl.UICtrl)

MapCustomMarkDetailCtrl.m_customMarkTemplateCells = HL.Field(HL.Forward('UIListCache'))

MapCustomMarkDetailCtrl.m_selectMarkTempId = HL.Field(HL.String) << ""

MapCustomMarkDetailCtrl.m_selectMarkTempCell = HL.Field(HL.Table)

MapCustomMarkDetailCtrl.m_tempTypeList = HL.Field(HL.Userdata)

MapCustomMarkDetailCtrl.m_markInstId = HL.Field(HL.String) << ""

MapCustomMarkDetailCtrl.m_markInstRuntimeData = HL.Field(HL.Userdata)

MapCustomMarkDetailCtrl.m_noteIsChange = HL.Field(HL.Boolean) << false

MapCustomMarkDetailCtrl.m_markTypeIsChange = HL.Field(HL.Boolean) << false





MapCustomMarkDetailCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_MAP_MARK_RUNTIME_DATA_CHANGED] = '_OnDataChanged',
}


MapCustomMarkDetailCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local insId = arg.markInstId
    if not insId then
        self.m_markInstId = GameInstance.player.mapManager:AddSelectCustomMark(arg.mapId, arg.levelId, arg.pos)
    else
        self.m_markInstId = insId
    end
    self.m_markInstRuntimeData = GameInstance.player.mapManager:GetQuickSearchCustomMarkData(self.m_markInstId)
    if self.m_markInstRuntimeData == nil then
        Notify(MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL)
        return
    end

    self.m_tempTypeList = Tables.MapMarkTypeTempTable:GetValue(GEnums.MarkType.CustomMark).typeList
    self:_InitCustomMark()
end

MapCustomMarkDetailCtrl.OnShow = HL.Override() << function(self)

end
MapCustomMarkDetailCtrl.OnHide = HL.Override() << function(self)

end
MapCustomMarkDetailCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.mapManager:RemoveSelectCustomMark()
    if not self.m_markInstRuntimeData.isSelect and (self.m_noteIsChange or self.m_markTypeIsChange) then
        local isValid = UIUtils.checkInputValid(self.view.reNameInputField.text)
        if not isValid then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_MAP_CUSTOM_MARK_ILLEGAL_CHARACTERS)
            return
        end
        if self.m_markTypeIsChange then
            GameInstance.player.mapManager:ModifyCustomMarkTemplateIdToServer(self.m_markInstId, self.m_selectMarkTempId)
        end
        if self.m_noteIsChange then
            GameInstance.player.mapManager:ModifyCustomNoteToServer(self.m_markInstId, self.view.reNameInputField.text)
        end
    end
end

MapCustomMarkDetailCtrl._InitCustomMark = HL.Method() << function(self)
    self.view.detailCommon.gameObject:SetActive(true)
    self:_InitMapMarkDetailCommon()
    if self.m_markInstRuntimeData.isSelect then
        self.m_selectMarkTempId = self.m_tempTypeList[CSIndex(1)].markInfoId
        GameInstance.player.mapManager:ModifySelectCustomMarkTemplateId(self.m_selectMarkTempId)
    else
        self.m_selectMarkTempId = self.m_markInstRuntimeData.templateId
    end
    self.m_customMarkTemplateCells = UIUtils.genCellCache(self.view.markCell)
    self.m_customMarkTemplateCells:Refresh(self.m_tempTypeList.Count,function(cell, index)
        cell.img:LoadSprite(UIConst.UI_SPRITE_MAP_MARK_ICON_CUSTOM, self.m_tempTypeList[CSIndex(index)].activeIcon)
        cell.button.onClick:AddListener(function()
            self.m_selectMarkTempId = self.m_tempTypeList[CSIndex(index)].markInfoId
            self.m_markTypeIsChange = self.m_markTypeIsChange or self.m_selectMarkTempId ~= self.m_markInstRuntimeData.templateId
            if self.m_selectMarkTempCell then
                self.m_selectMarkTempCell.select.gameObject:SetActive(false)
            end
            self.m_selectMarkTempCell = cell
            cell.select.gameObject:SetActive(true)
            GameInstance.player.mapManager:ModifyCustomMarkTemplateId(self.m_markInstId, self.m_selectMarkTempId)
            GameInstance.player.mapManager:ModifySelectCustomMarkTemplateId(self.m_selectMarkTempId)
        end)

        if self.m_selectMarkTempId == self.m_tempTypeList[CSIndex(index)].markInfoId then
            self.m_selectMarkTempCell = cell
            cell.select.gameObject:SetActive(true)
            if DeviceInfo.usingController then
                self:SetNaviTarget(cell.button)
            end
        else
            cell.select.gameObject:SetActive(false)
        end
    end)
    self.view.reNameInputField.characterLimit = Tables.GlobalConst.maxCustomMapMarkNameLen
    self.view.reNameInputField.onValidateCharacterLimit = I18nUtils.GetRealTextByLengthLimit
    self.view.reNameInputField.onGetTextLength = I18nUtils.GetTextRealLength
    self.view.reNameInputField.onSelect:AddListener(function()
        AudioAdapter.PostEvent("Au_UI_Button_Common")
    end)

    self.view.reNameInputField.onValueChanged:AddListener(function(changedName)
        local realInput = string.gsub(changedName, " ", "")
        local isValid = UIUtils.checkInputValid(realInput)
        self.view.textWarning.gameObject:SetActive(not isValid)
        self.view.reNameInputField.text = realInput
        self.m_noteIsChange = true
    end)

    self.view.deleteBtn.button.onClick:AddListener(function()
        local args = {instId = self.m_markInstId, levelId = self.m_markInstRuntimeData.levelId}
        Notify(MessageConst.SHOW_CUSTOM_MARK_MULTI_DELETE, args)
    end)
    local curNum = GameInstance.player.mapManager:GetQuickSearchCustomMarkCountByLevel(self.m_markInstRuntimeData.levelId)
    local maxNum = Tables.GlobalConst.maxSceneCustomMapMarkNumber
    self.view.markNowNumTxt.text = curNum
    self.view.markFullNumTxt.text = string.format("/%d", maxNum)
end

MapCustomMarkDetailCtrl._InitMapMarkDetailCommon = HL.Method() << function(self)
    local commonArgs = {}

    if self.m_markInstRuntimeData.templateId ~= MapConst.CUSTOM_MARK_SELECT_TEMPLATE  then
        commonArgs.leftBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.REMOVE_TRACE
        commonArgs.leftBtnActive = true
        commonArgs.leftBtnText = Language.LUA_MAP_CUSTOM_MARK_DELETE
        commonArgs.leftBtnCallback = function()
            if self.m_markInstId == GameInstance.player.mapManager.trackingMarkInstId then
                GameInstance.player.mapManager:TrackMark(self.m_markInstId, false)
            end
            GameInstance.player.mapManager:RemoveCustomMarkToServer({self.m_markInstId})
            Notify(MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL)
            AudioAdapter.PostEvent("Au_UI_Button_Delete")
        end
        self.view.detailCommon.view.leftBtn.onClick:ChangeBindingPlayerAction("map_custom_mark_delete")

        if self.m_markInstId == GameInstance.player.mapManager.trackingMarkInstId then
            commonArgs.rightBtnText = Language.LUA_MAP_TOAST_CANCEL
        else
            commonArgs.rightBtnText = Language.LUA_MAP_TOAST_TRACK
        end
        commonArgs.rightBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.FAST_ENTER
        commonArgs.rightBtnActive = true
        commonArgs.rightBtnCallback = function()
            self.view.detailCommon:_SwitchTracerState()
        end
        if self.m_markInstRuntimeData.note == "" then
            local id = GameInstance.player.mapManager:GetCustomMarkIdByInstId(self.m_markInstRuntimeData.instId)
            local defaultName = string.format("%s%s", Language.LUA_MAP_CUSTOM_MARK_EDIT_NAME, id)
            commonArgs.titleText = defaultName
            self.view.reNameInputField.text = defaultName
        else
            commonArgs.titleText = self.m_markInstRuntimeData.note
            self.view.reNameInputField.text = self.m_markInstRuntimeData.note
        end
        self.view.yellowBtn.gameObject:SetActive(false)
    else

        commonArgs.leftBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.REMOVE_TRACE
        commonArgs.leftBtnActive = true
        commonArgs.leftBtnText = Language.LUA_MAP_CUSTOM_MARK_CANCEL
        commonArgs.leftBtnCallback = function()
            Notify(MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL)
            AudioAdapter.PostEvent("Au_UI_Button_Cancel")
        end
        self.view.detailCommon.view.leftBtn.onClick:ChangeBindingPlayerAction("common_cancel")
        self.view.yellowBtn.gameObject:SetActive(true)
        self.view.yellowBtn.onClick:AddListener(function()
            self:_OnConfirmMark()
        end)
        self.view.yellowBtnText.text = Language.LUA_MAP_CUSTOM_MARK_CREATE
        self.view.yellowBtnIcon:LoadSprite(UIConst.UI_SPRITE_MAP_DETAIL_BTN_ICON, UIConst.MAP_DETAIL_BTN_ICON_NAME.CONFIRM)
        self.view.reNameInputField.text = ""
        local curNum = GameInstance.player.mapManager:GetQuickSearchCustomMarkCountByLevel(self.m_markInstRuntimeData.levelId)
        self.view.reNameInputField.placeholder.text = string.format("%s%d", Language.LUA_MAP_CUSTOM_MARK_EDIT_NAME, curNum)
    end
    commonArgs.markInstId = self.m_markInstId
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)
end

MapCustomMarkDetailCtrl._OnDataChanged = HL.Method(HL.Table) << function(self, args)
    local instId, isAdd = unpack(args)
    if isAdd then
        self.m_markInstRuntimeData = GameInstance.player.mapManager:GetQuickSearchCustomMarkData(instId)
    end
end

MapCustomMarkDetailCtrl._OnConfirmMark = HL.Method() << function(self)
    if self.m_selectMarkTempId == "" then
        self.m_selectMarkTempId = self.m_tempTypeList[CSIndex(1)]
    end
    local isValid = UIUtils.checkInputValid(self.view.reNameInputField.text)
    if isValid then
        local data = GameInstance.player.mapManager:GetQuickSearchCustomMarkData(self.m_markInstId)
        local name = self.view.reNameInputField.text ~= "" and self.view.reNameInputField.text or self.view.reNameInputField.placeholder.text
        GameInstance.player.mapManager:AddCustomMarkToServer(data.levelId, data.position, name, data.templateId, data.tierId)
    else
        Notify(MessageConst.SHOW_TOAST, Language.LUA_MAP_CUSTOM_MARK_ILLEGAL_CHARACTERS)
    end
    Notify(MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL)
end

HL.Commit(MapCustomMarkDetailCtrl)
