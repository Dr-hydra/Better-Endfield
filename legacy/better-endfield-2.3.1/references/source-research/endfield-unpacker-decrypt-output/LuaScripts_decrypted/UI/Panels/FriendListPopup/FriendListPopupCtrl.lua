local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FriendListPopup

FriendListPopupCtrl = HL.Class('FriendListPopupCtrl', uiCtrl.UICtrl)

FriendListPopupCtrl.m_friendList = HL.Field(HL.Table)

FriendListPopupCtrl.friendSystem = HL.Field(CS.Beyond.Gameplay.FriendSystem)





FriendListPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FRIEND_INFO_SYNC] = 'OnSync',
    [MessageConst.ON_FRIEND_CELL_INFO_CHANGE] = 'OnCellChange',
}

FriendListPopupCtrl.OnSync = HL.Method() << function(self)
    self:_UpdateCache()
    self:_Refresh()
end

FriendListPopupCtrl.OnCellChange = HL.Method() << function(self)
    self:_UpdateCache()
    self.view.friendList:RefreshInfoStayPos(self.m_friendList)
end


FriendListPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.friendSystem = GameInstance.player.friendSystem
    self.friendSystem:SyncFriendSimpleInfo()

    local initArg = FriendUtils.FRIEND_CELL_INIT_CONFIG.Friend
    initArg.onSearchChange = function(str)
        self.view.clearBtn.gameObject:SetActiveIfNecessary(not (string.isEmpty(str)))
    end
    initArg.isFilter = true

    if arg ~= nil and arg.stateName ~= nil then
        initArg.stateName = arg.stateName
    end

    if arg ~= nil and arg.shareCode ~= nil then
        initArg.shareCode = arg.shareCode
    end

    self.view.friendList:InitFriendListCtrl(initArg)

    self.view.friendList.view.inputField.onFocused:AddListener(function(str)
        self.view.friendList.view.inputField.transform.sizeDelta = Vector2(self.view.config.INPUT_FIELD_FOCUS_WIDTH, self.view.friendList.view.inputField.transform.sizeDelta.y)
        self.view.inputBgImage.transform.sizeDelta = Vector2(self.view.config.INPUT_FIELD_BG_FOCUS_WIDTH, self.view.inputBgImage.transform.sizeDelta.y)
        self.view.clearBtn.transform.anchoredPosition = Vector2(self.view.config.CLEAR_BTN_FOCUS_POS, self.view.clearBtn.transform.localPosition.y, 0)
    end)
    self.view.friendList.view.inputField.onEndEdit:AddListener(function(str)
        if string.isEmpty(self.view.inputField.text) then
            self.view.friendList.view.inputField.transform.sizeDelta = Vector2(self.view.config.INPUT_FIELD_WIDTH, self.view.friendList.view.inputField.transform.sizeDelta.y)
            self.view.inputBgImage.transform.sizeDelta = Vector2(self.view.config.INPUT_FIELD_BG_WIDTH, self.view.inputBgImage.transform.sizeDelta.y)
            self.view.clearBtn.transform.anchoredPosition = Vector2(self.view.config.CLEAR_BTN_POS, self.view.clearBtn.transform.localPosition.y, 0)
        end
    end)

    self.view.inputField.onFocused:AddListener(function(str)
        self:_StartInput()
        self.view.clearBtn.gameObject:SetActiveIfNecessary(not (string.isEmpty(str)))
    end)

    self.view.inputField.onEndEdit:AddListener(function(str)
        self:_EndInput()
        self.view.clearBtn.gameObject:SetActiveIfNecessary(false)
    end)

    self.view.clearBtn.onClick:RemoveAllListeners()
    self.view.clearBtn.onClick:AddListener(function()
        self.view.inputField.text = ""
    end)

    self.view.clearBtn.gameObject:SetActiveIfNecessary(false)
    self.view.btnClose.onClick:AddListener(function()
        self:Close()
    end)

    self:Loading();
end

FriendListPopupCtrl._StartInput = HL.Method() << function(self)
    if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
        return
    end
    Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
        panelId = PANEL_ID,
        isGroup = true,
        id = self.view.textInputBindingGroup.groupId,
        hintPlaceholder = self.view.controllerHintPlaceholder,
        rectTransform = self.view.textInputBindingGroup.transform,
    })
end

FriendListPopupCtrl._EndInput = HL.Method() << function(self)
    if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
        return
    end
    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.textInputBindingGroup.groupId)
    self.view.inputField:OnDeselect(nil)
end

FriendListPopupCtrl.Loading = HL.Method() << function(self)
    self.m_friendList = {}
    self:_Refresh()
end

FriendListPopupCtrl.OnPhaseRefresh = HL.Override(HL.Any) << function(self, args)
    self:_UpdateCache()
    self:_Refresh()
end

FriendListPopupCtrl._UpdateCache = HL.Method() << function(self)
    self.m_friendList = {}
    local index = 1
    for _, friendInfo in cs_pairs(self.friendSystem.friendInfoDic) do
        self.m_friendList[index] = FriendUtils.friendInfo2SortInfo(friendInfo, self.view.friendList.SearchSort)
        index = index + 1
    end
end

FriendListPopupCtrl._Refresh = HL.Method() << function(self)
    self.view.friendList:RefreshInfo(self.m_friendList ,true)
end









HL.Commit(FriendListPopupCtrl)
