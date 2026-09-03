
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacMarkerManagePopup
local CREDIT_TEXT_FORMAT = "%s<color=#8D9194>/%s</color>"

FacMarkerManagePopupCtrl = HL.Class('FacMarkerManagePopupCtrl', uiCtrl.UICtrl)

local SIGN_BUILDINGID = "marker_1"





FacMarkerManagePopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacMarkerManagePopupCtrl.m_playerAllSignCount = HL.Field(HL.Number) << 0


FacMarkerManagePopupCtrl.m_playerAllSignInfo = HL.Field(HL.Table)

FacMarkerManagePopupCtrl.m_getCell = HL.Field(HL.Function)

FacMarkerManagePopupCtrl.m_nodeId = HL.Field(HL.Any)

FacMarkerManagePopupCtrl.m_roleId = HL.Field(HL.Any)

FacMarkerManagePopupCtrl.m_onDelBuilding = HL.Field(HL.Any)

FacMarkerManagePopupCtrl.m_sortOptions = HL.Field(HL.Table)

FacMarkerManagePopupCtrl.m_sortData = HL.Field(HL.Table)

FacMarkerManagePopupCtrl.m_sortIncremental = HL.Field(HL.Boolean) << false

FacMarkerManagePopupCtrl.m_waitingNaviFirst = HL.Field(HL.Boolean) << false


FacMarkerManagePopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local onClose
    if arg ~= nil then
        self.m_nodeId = arg.nodeId
        self.m_roleId = arg.roleId
        onClose = arg.onClose
        self.m_onDelBuilding = arg.onDelBuilding
    end
    self.view.warningTipsNode.gameObject:SetActiveIfNecessary(self.m_nodeId == nil and self.m_roleId == nil)

    self.view.closeButton.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
            if onClose then
                onClose()
            end
        end)
    end)
    self.view.bgBlack.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
            if onClose then
                onClose()
            end
        end)
    end)
    self.m_getCell = UIUtils.genCachedCellFunction(self.view.markerScrollList)
    self.view.markerScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)

    self:_InitSort()
    self:_UpdateMarkerList()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end








FacMarkerManagePopupCtrl._InitSort = HL.Method() << function(self)
    self.m_sortOptions = {
        {
            ascendingSortTitle = Language.LUA_SORT_DESCENDING_OLD_TO_NEW,
            descendingSortTitle = Language.LUA_SORT_DESCENDING_NEW_TO_OLD,
            name = Language.LUA_SIGN_MANAGE_TIME_SORT,
            keys = { "timestamp", "nodeId" },
        },
        {
            name = Language.LUA_SIGN_MANAGE_LIKE_SORT,
            keys = { "like", "nodeId" },
        },
    }
    self.view.sortNodeUp:InitSortNode(self.m_sortOptions, function(optData, isIncremental)
        self.m_sortData = optData
        self.m_sortIncremental = isIncremental
        table.sort(self.m_playerAllSignInfo, Utils.genSortFunction(optData.keys, isIncremental))
        if DeviceInfo.usingController then
            self.m_waitingNaviFirst = true
            self.view.markerScrollList:ScrollToIndex(0, true)
        end
        self.view.markerScrollList:UpdateCount(#self.m_playerAllSignInfo)
    end, 0, false, true, self.view.filterBtn)
    self.m_sortData = self.m_sortOptions[1]
    self.m_sortIncremental = self.view.sortNodeUp.isIncremental
end

FacMarkerManagePopupCtrl._UpdateMarkerList = HL.Method() << function(self)
    self.m_playerAllSignCount, self.m_playerAllSignInfo = FactoryUtils.getPlayerAllMarkerBuildingNodeInfo()
    for _, signInfo in ipairs(self.m_playerAllSignInfo) do
        local chapterIdNum = ScopeUtil.ChapterIdStr2Int(signInfo.chapter)
        signInfo.like = FactoryUtils.getBuildingComponentPayload_Social(signInfo.nodeId, chapterIdNum).like
    end
    table.sort(self.m_playerAllSignInfo, Utils.genSortFunction(self.m_sortData.keys, self.m_sortIncremental))
    if DeviceInfo.usingController then
        self.m_waitingNaviFirst = true
        self.view.markerScrollList:ScrollToIndex(0, true)
    end
    self.view.markerScrollList:UpdateCount(#self.m_playerAllSignInfo)
    self.view.markerNumTxt.text = string.format(CREDIT_TEXT_FORMAT, self.m_playerAllSignCount, Tables.factoryConst.signNodeCountLimit)
end

FacMarkerManagePopupCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local signData = self.m_playerAllSignInfo[index]
    local combineText = ""
    for i = 1, FacConst.SOCIAL_ICON_MAX_COUNT do
        local iconKey = signData.iconKey[i]
        local iconNode = cell["markerIconNode0" .. i]
        iconNode.markerIcon.gameObject:SetActiveIfNecessary(iconKey ~= nil)
        iconNode.emptyIcon.gameObject:SetActiveIfNecessary(iconKey == nil)
        if iconKey ~= nil then
            local iconData = Tables.socialBuildingSignTable:GetValue(iconKey)
            iconNode.markerIcon:LoadSprite(UIConst.UI_SPRITE_FAC_MARKER_SETTING_ICON, iconData.uiIconKey)
            if i == 1 then
                combineText = iconData.text
            else
                combineText = I18nUtils.CombineStringWithLanguageSpilt(combineText, iconData.text)
            end
        end
    end
    cell.markerTxt.text = combineText
    local chapterIdNum = ScopeUtil.ChapterIdStr2Int(signData.chapter)
    cell.maintainNumTxt.text = FactoryUtils.getBuildingComponentPayload_Social(signData.nodeId, chapterIdNum).like
    if self.m_waitingNaviFirst and index == 1 then
        self.m_waitingNaviFirst = false
        self:SetNaviTarget(cell.naviDecorator)
    end

    cell.positionBtn.gameObject:SetActiveIfNecessary(self.m_nodeId ~= nil or self.m_roleId ~= nil)
    cell.shareBtn.gameObject:SetActiveIfNecessary(self.m_nodeId ~= nil or self.m_roleId ~= nil)
    if self.m_nodeId ~= nil or self.m_roleId ~= nil then
        cell.positionBtn.onClick:RemoveAllListeners()
        cell.positionBtn.onClick:AddListener(function()
            if GameInstance.player.spaceship.isViewingFriend then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_SOCIAL_BUILDING_JUMP_SPACESHIP_TIP)
                return
            end
            local id = ScopeUtil.ChapterIdStr2Int(signData.chapter)
            local success, mapInstId = GameInstance.player.mapManager:GetFacMarkInstIdByNodeId(id, signData.nodeId)
            if success then
                MapUtils.openMap(mapInstId)
            end
        end)
        cell.shareBtn.onClick:RemoveAllListeners()
        cell.shareBtn.onClick:AddListener(function()
            if self:_CheckChapterDiffAndToast(signData.chapter) then
                return
            end

            if not FriendUtils.canShareBuilding() then
                return
            end

            if self.m_roleId ~= nil then
                GameInstance.player.friendChatSystem:SendChatSocialBuilding(self.m_roleId, signData.chapter, signData.nodeId, function()
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_SHARE_SOCIAL_BUILDING_SUCCESS)
                    self:PlayAnimationOutAndClose()
                end)
            else
                UIManager:Open(PanelId.FriendRequest, {
                    onShareClick = function(roleId)
                        GameInstance.player.friendChatSystem:SendChatSocialBuilding(roleId, signData.chapter, signData.nodeId, function()
                            Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_SHARE_SOCIAL_BUILDING_SUCCESS)
                            PhaseManager:OpenPhase(PhaseId.SNS, { roleId = roleId }, nil, true)
                        end)
                    end,
                })
            end
        end)
    end
    cell.acceptBtn.gameObject:SetActiveIfNecessary(self.m_roleId == nil)
    if self.m_roleId == nil then
        cell.acceptBtn.onClick:RemoveAllListeners()
        cell.acceptBtn.onClick:AddListener(function()
            if self:_CheckChapterDiffAndToast(signData.chapter) then
                return
            end

            if not FactoryUtils.canDelBuilding(signData.nodeId, true) then
                return
            end
            local data = Tables.factoryBuildingTable:GetValue(SIGN_BUILDINGID)
            local hintTxt
            if data ~= nil then
                hintTxt = data.delConfirmText
            end
            FactoryUtils.delBuilding(signData.nodeId, function()
                self:_UpdateMarkerList()
                if self.m_onDelBuilding ~= nil then
                    self.m_onDelBuilding(self.m_playerAllSignCount)
                end
                if self.m_nodeId == signData.nodeId then
                    PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
                else
                    self.view.warningTipsNode.gameObject:SetActiveIfNecessary(false)
                end
            end, false, hintTxt)
        end)
    end
end



local CHAPTER_ERROR_CODE = 1081

FacMarkerManagePopupCtrl.m_curChapterId = HL.Field(HL.String) << ""

FacMarkerManagePopupCtrl._CheckChapterDiffAndToast = HL.Method(HL.String).Return(HL.Boolean) << function(self, chapter)
    if string.isEmpty(self.m_curChapterId) then
        self.m_curChapterId = Utils.getCurDomainId()
    end

    if chapter ~= self.m_curChapterId then
        local error = Tables.errorCodeTable:GetValue(CHAPTER_ERROR_CODE).text
        Notify(MessageConst.SHOW_TOAST, error)
        return true
    end

    return false
end






HL.Commit(FacMarkerManagePopupCtrl)
