local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ReportPlayer

ReportPlayerCtrl = HL.Class('ReportPlayerCtrl', uiCtrl.UICtrl)






ReportPlayerCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ReportPlayerCtrl.m_reportList = HL.Field(HL.Table)

ReportPlayerCtrl.m_reportStrId = HL.Field(HL.String) << ""

ReportPlayerCtrl.m_reportType = HL.Field(HL.Number) << 0

ReportPlayerCtrl.m_isReport = HL.Field(HL.Boolean) << false

ReportPlayerCtrl.m_arg = HL.Field(HL.Any)


ReportPlayerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    self.view.btnClose.onClick:RemoveAllListeners()
    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.btnCommonCancel.onClick:RemoveAllListeners()
    self.view.btnCommonCancel.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.btnCommon.onClick:RemoveAllListeners()
    self.view.btnCommon.onClick:AddListener(function()
        if string.isEmpty(self.m_reportStrId) then
            return
        end
        self.m_isReport = true
        if self.m_reportType == FriendUtils.ReportGroupType.Blueprint then
            GameInstance.player.friendSystem:ReportBluePrint(self.m_reportStrId, self.view.inputFieldReport.text, arg.blueprintParam)
        elseif self.m_reportType == FriendUtils.ReportGroupType.SocialBuilding then
            local param = arg.socialBuildingParam
            GameInstance.player.friendSystem:ReportSocialBuilding(self.m_reportStrId, self.view.inputFieldReport.text, param.chapterId, param.nodeId)
        else
            GameInstance.player.friendSystem:ReportUser(arg.roleId, self.m_reportStrId, self.view.inputFieldReport.text, self.m_reportType == FriendUtils.ReportGroupType.BusinessCard)
        end
        Notify(MessageConst.SHOW_TOAST, Language.LUA_REPORT_PLAYER_SUCCESS)
        self:PlayAnimationOutAndClose()
    end)

    UIUtils.initSearchInput(self.view.inputFieldReport, {
        onInputFocused = function()
            if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
                return
            end
            Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
                panelId = PANEL_ID,
                isGroup = true,
                id = self.view.inputFieldReportInputBindingGroupMonoTarget.groupId,
                hintPlaceholder = self.view.controllerHintPlaceholder,
                rectTransform = self.view.inputFieldReport.transform,
            })
        end,
        onInputEndEdit = function()
            if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
                return
            end
            Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.inputFieldReportInputBindingGroupMonoTarget.groupId)
            self.view.inputFieldReport:DeactivateInputField(true)
        end,
    })

    self.view.btnCommon.interactable = false

    self.m_reportType = arg and arg.reportType or FriendUtils.ReportGroupType.BusinessCard

    local roleId = arg and arg.roleId or 0
    local success, info = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
    if success then
        if info.remakeName and not string.isEmpty(info.remakeName) then
            self.view.playerNameTxt.text = string.format(Language.LUA_FRIEND_REMAKE_NAME, info.remakeName, info.name, info.shortId)
        else
            self.view.playerNameTxt.text = string.format(Language.LUA_FRIEND_NAME, info.name, info.shortId)
        end
    else
        logger.error('ReportPlayerCtrl.OnCreate: can not find friend info, roleId = ' .. tostring(roleId))
    end

    self.m_reportList = {}

    for id, text in pairs(Tables.reportTable) do
        table.insert(self.m_reportList, { id = id, text = text })
    end
    table.sort(self.m_reportList, function(a, b)
        return a.id < b.id
    end)

    local genTechCells = UIUtils.genCellCache(self.view.reportItemCell)
    genTechCells:Refresh(#self.m_reportList, function(cell, luaIndex)
        local data = self.m_reportList[luaIndex]
        cell.textPair.text = data.text
        local toggle = cell.toggle
        toggle.isOn = false
        InputManagerInst:SetBindingText(toggle.hoverConfirmBindingId, Language['key_hint_friend_report_select'])
        toggle.onValueChanged:RemoveAllListeners()
        toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self.m_reportStrId = data.id
            end
            InputManagerInst:SetBindingText(toggle.hoverConfirmBindingId, isOn and Language['key_hint_friend_report_select_cancel'] or Language['key_hint_friend_report_select'])
            local interactable = self.view.reportItemList:AnyTogglesOn()
            self.view.btnCommon.interactable = interactable
            self.view.root:SetState(interactable and 'NormalState' or 'DisableState')
        end)
    end)
    self.view.btnCommon.interactable = false
    self.view.root:SetState('DisableState')
    self.view.reportItemListSelectableNaviGroup:NaviToThisGroup()
end





ReportPlayerCtrl.OnClose = HL.Override() << function(self)
    local arg = self.m_arg
    local reportEnter
    local result = self.m_isReport and 1 or 3
    local reportTargetId
    local reportType = self.m_reportType
    if reportType == FriendUtils.ReportGroupType.Blueprint then
        reportEnter = "rm42_blueprint"
        reportTargetId = string.format("%d-%d-%d", arg.blueprintParam.GiftBpKey.BpUid, arg.blueprintParam.GiftBpKey.ShareIdx, arg.blueprintParam.GiftBpKey.TargetRoleId)
    elseif reportType == FriendUtils.ReportGroupType.SocialBuilding then
        reportEnter = "message"
        reportTargetId = tostring(arg.roleId)
    elseif reportType == FriendUtils.ReportGroupType.BusinessCard then
        reportEnter = "card"
        reportTargetId = tostring(arg.roleId)
    else
        reportEnter = "user"
        reportTargetId = tostring(arg.roleId)
    end
    EventLogManagerInst:GameEvent_Report(reportEnter, result, reportTargetId,
        tostring(reportType), GameInstance.player.playerInfoSystem.roleId, self.m_reportStrId, self.view.inputFieldReport.text)
end




HL.Commit(ReportPlayerCtrl)
