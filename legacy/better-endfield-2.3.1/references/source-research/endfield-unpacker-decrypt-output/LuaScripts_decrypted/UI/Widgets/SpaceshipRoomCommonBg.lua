local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SpaceshipRoomCommonBg = HL.Class('SpaceshipRoomCommonBg', UIWidgetBase)

SpaceshipRoomCommonBg.m_roomId = HL.Field(HL.String) << ""

SpaceshipRoomCommonBg.m_closeFunc = HL.Field(HL.Function)

SpaceshipRoomCommonBg.m_returnFunc = HL.Field(HL.Function)

SpaceshipRoomCommonBg.m_state = HL.Field(HL.Number) << -1


SpaceshipRoomCommonBg._OnFirstTimeInit = HL.Override() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        if self.m_closeFunc then
            self.m_closeFunc()
        end
    end)

    self.view.returnBtn.onClick:AddListener(function()
        if self.m_returnFunc then
            self.m_returnFunc()
        end
    end)
end

SpaceshipRoomCommonBg.InitSpaceshipRoomCommonBg = HL.Method(HL.String, HL.Function, HL.Function)
        << function(self, roomId, returnFunc, closeFunc)
    self:_FirstTimeInit()

    self.m_roomId = roomId
    self.m_returnFunc = returnFunc
    self.m_closeFunc = closeFunc

    local type = SpaceshipUtils.getRoomTypeByRoomId(roomId)
    local roomTypeData = Tables.spaceshipRoomTypeTable[type]

    local themeColor = UIUtils.getColorByString(roomTypeData.color)
    self.view.stateIngCanvasGroup.color = themeColor
    self.view.deco.color = themeColor
    self.view.bigIcon:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, roomTypeData.bg)
    self.view.smallIcon:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, roomTypeData.icon)
    self.view.titleTxt.text = string.format(Language.LUA_SPACESHIP_ROOM_PANEL_NAME_FORMAT, roomTypeData.name)
    self.view.infoNode.gameObject:SetActiveIfNecessary(type == GEnums.SpaceshipRoomType.ManufacturingStation)

    if type == GEnums.SpaceshipRoomType.ManufacturingStation then
        self.view.ingTxt.text = Language.LUA_SPACESHIP_MANUFACTURING_STATION_IS_PRODUCING
    elseif type == GEnums.SpaceshipRoomType.GrowCabin then
        self.view.ingTxt.text = Language.LUA_SPACESHIP_ROOM_GROW_CABIN_IS_PRODUCING_DESC
    end
    self.view.friendAssistNode:InitFriendAssistNode(self.m_roomId)
    self:RegisterMessage(MessageConst.ON_SPACESHIP_USE_HELP_CREDIT, function()
        self.view.friendAssistNode:InitFriendAssistNode(self.m_roomId)
    end)
end

SpaceshipRoomCommonBg.SetState = HL.Method(HL.Number) << function(self, state)
    local StateEnum = SpaceshipUtils.RoomStateEnum
    local preState = self.m_state
    self.m_state = state
    if state == StateEnum.Producing then
        self.view.decoAnim:PlayInAnimation()
        self.view.deco.gameObject:SetActiveIfNecessary(state == StateEnum.Producing)
    elseif preState == StateEnum.Producing then
        self.view.decoAnim:PlayOutAnimation(function()
            self.view.deco.gameObject:SetActiveIfNecessary(state == StateEnum.Producing)
        end)
    else
        self.view.deco.gameObject:SetActiveIfNecessary(state == StateEnum.Producing)
    end

    self.view.stateIng.gameObject:SetActiveIfNecessary(state == StateEnum.Producing)
    self.view.stateIdle.gameObject:SetActiveIfNecessary(state == StateEnum.Idle)
    self.view.stateShutDown.gameObject:SetActiveIfNecessary(state == StateEnum.ShutDown)
end

SpaceshipRoomCommonBg.SetSubTitle = HL.Method(HL.Opt(HL.String)) << function(self, subTitle)
    local isEmpty = string.isEmpty(subTitle)

    self.view.titleTxt2.gameObject:SetActiveIfNecessary(not isEmpty)
    if not isEmpty then
        self.view.titleTxt2.text = subTitle
    end
end

SpaceshipRoomCommonBg.ToggleReturnBtnOn = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.returnBtn.gameObject:SetActiveIfNecessary(isOn)
    self.view.closeBtn.gameObject:SetActiveIfNecessary(not isOn)
end

SpaceshipRoomCommonBg.SetTopInfoNodeState = HL.Method(HL.Boolean) << function(self, active)

    self.view.infoNode.gameObject:SetActiveIfNecessary(active)
end

SpaceshipRoomCommonBg.RefreshTopInfoNode = HL.Method(HL.String, HL.String) << function(self, efficacyName, efficacyTxt)
    self.view.efficacyName.text = efficacyName
    self.view.efficacyTxt.text = efficacyTxt
end

SpaceshipRoomCommonBg.SetFriendAssistNode = HL.Method(HL.Boolean) << function(self, active)
    self.view.friendAssistNode.gameObject:SetActiveIfNecessary(active)
end

HL.Commit(SpaceshipRoomCommonBg)
return SpaceshipRoomCommonBg
