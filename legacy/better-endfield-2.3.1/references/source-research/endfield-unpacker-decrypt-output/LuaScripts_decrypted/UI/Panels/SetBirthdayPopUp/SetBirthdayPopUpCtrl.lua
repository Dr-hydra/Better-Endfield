
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SetBirthdayPopUp

SetBirthdayPopUpCtrl = HL.Class('SetBirthdayPopUpCtrl', uiCtrl.UICtrl)

SetBirthdayPopUpCtrl.m_getMonthItemCell = HL.Field(HL.Function)

SetBirthdayPopUpCtrl.m_getDayItemCell = HL.Field(HL.Function)

SetBirthdayPopUpCtrl.m_currentMonth = HL.Field(HL.Number) << 1

SetBirthdayPopUpCtrl.m_currentDay = HL.Field(HL.Number) << 1

local MONTH_NUM = 12





SetBirthdayPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {

}


SetBirthdayPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if arg ~= nil then
        self.m_currentMonth = arg.month
        self.m_currentDay = arg.day
    end
    self:_InitBtn()
    self:_InitBasicInfo()
    self:_InitMonth()
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

SetBirthdayPopUpCtrl._InitMonth = HL.Method() << function(self)
    local scrollView = self.view.monthNode
    if not self.m_getMonthItemCell then
        self.m_getMonthItemCell = UIUtils.genCachedCellFunction(scrollView)
        scrollView.onUpdateCell:AddListener(function(obj, csIndex)
            self:_OnUpdateCell(self.m_getMonthItemCell(obj), LuaIndex(csIndex))
        end)
        scrollView.onScrollEnd:AddListener(function(csIndex)
            local index = LuaIndex(csIndex)
            self.m_currentMonth = index
            self:_RefreshDayNode()
        end)
    end

    scrollView:UpdateCount(MONTH_NUM)
    scrollView:ScrollToIndex(CSIndex(self.m_currentMonth), true)
end

SetBirthdayPopUpCtrl._RefreshDayNode = HL.Method() << function(self)
    local scrollView = self.view.dayNode
    if not self.m_getDayItemCell then
        self.m_getDayItemCell = UIUtils.genCachedCellFunction(scrollView)
        scrollView.onUpdateCell:AddListener(function(obj, csIndex)
            self:_OnUpdateCell(self.m_getDayItemCell(obj), LuaIndex(csIndex))
        end)
        scrollView.onScrollEnd:AddListener(function(csIndex)
            self.m_currentDay = LuaIndex(csIndex)
        end)
    end
    local dayCount = SetBirthdayPopUpCtrl._GetDayCount(self.m_currentMonth)
    scrollView:UpdateCount(dayCount)
    if self.m_currentDay > dayCount then
        self.m_currentDay = dayCount
    end
    scrollView:ScrollToIndex(CSIndex(self.m_currentDay), true)
end

SetBirthdayPopUpCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    cell.text.text = tostring(index)
end

SetBirthdayPopUpCtrl._InitBasicInfo = HL.Method() << function(self)
    local roleId = GameInstance.player.roleId
    local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
    if not success then
        logger.error("获取玩家信息失败，roleId: " .. roleId)
        return
    end
    self.view.playerNameTxt.text = string.format(Language.LUA_FRIEND_SET_BIRTHDAY_TITLE, playerInfo.name)
end

SetBirthdayPopUpCtrl._InitBtn = HL.Method() << function(self)
    self.view.closeButton.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.btnCancel.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.btnSave.onClick:AddListener(function()
        self:_ShowMessageBox()
    end)
end

SetBirthdayPopUpCtrl._GetDayCount = HL.StaticMethod(HL.Number).Return(HL.Number) << function(month)
    if month == 2 then
        return 29
    elseif month == 4 or month == 6 or month == 9 or month == 11 then
        return 30
    else
        return 31
    end
end

SetBirthdayPopUpCtrl._ShowMessageBox = HL.Method() << function(self)
    local message = string.format(Language.LUA_FRIEND_SET_BIRTHDAY_MESSAGE_BOX, self.m_currentMonth, self.m_currentDay)
    if self.m_currentMonth == 2 and self.m_currentDay == 29 then
        message = string.format(Language.LUA_FRIEND_SET_BIRTHDAY_MESSAGE_BOX_LEAPMONTH_SUFFIX, message)
    end
    Notify(MessageConst.SHOW_POP_UP, {
        content = message,
        warningContent = Language.LUA_FRIEND_SET_BIRTHDAY_MESSAGE_BOX_WARNING,
        onConfirm = function()
            GameInstance.player.friendSystem:SetBirthday(self.m_currentMonth, self.m_currentDay)
            self:PlayAnimationOutAndClose()
        end,
        onCancel = function()

        end
    })
end

SetBirthdayPopUpCtrl.GetCurStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    arg.month = self.m_currentMonth
    arg.day = self.m_currentDay
    return arg
end











HL.Commit(SetBirthdayPopUpCtrl)
