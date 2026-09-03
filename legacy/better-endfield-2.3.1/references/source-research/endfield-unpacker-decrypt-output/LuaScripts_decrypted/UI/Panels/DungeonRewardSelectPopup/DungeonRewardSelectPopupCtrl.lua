
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DungeonRewardSelectPopup

DungeonRewardSelectPopupCtrl = HL.Class('DungeonRewardSelectPopupCtrl', uiCtrl.UICtrl)

DungeonRewardSelectPopupCtrl.m_dungeonId = HL.Field(HL.String) << ""

DungeonRewardSelectPopupCtrl.m_currSelectIdx = HL.Field(HL.Number) << 0

DungeonRewardSelectPopupCtrl.m_rewardIdList = HL.Field(HL.Table)





DungeonRewardSelectPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


DungeonRewardSelectPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if arg ~= nil then
        self.m_dungeonId = arg.dungeonId
    end

    self:_BindBtnCallbacks()
    self:_InitData()
    self:_RefreshUI()
    local firstCell = self.view.dungeonRewardSelectCell
    if firstCell then
        self:SetNaviTarget(firstCell.view.inputBindingGroupNaviDecorator)
    end
end





DungeonRewardSelectPopupCtrl._BindBtnCallbacks = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        self:_OnCloseBtnClick()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

DungeonRewardSelectPopupCtrl._InitData = HL.Method() << function(self)
    local haveDungeon, dungeonData = Tables.DungeonTable:TryGetValue(self.m_dungeonId)
    if not haveDungeon then
        return
    end

    self.m_rewardIdList = {}
    table.insert(self.m_rewardIdList, dungeonData.rewardId)
    table.insert(self.m_rewardIdList, dungeonData.customRewardId)

    local hasRecord, subGameRecord = GameInstance.player.subGameSys:TryGetSubGameRecord(self.m_dungeonId)
    self.m_currSelectIdx = hasRecord and LuaIndex(subGameRecord.customRewardIndex) or 1
end

DungeonRewardSelectPopupCtrl._RefreshUI = HL.Method() << function(self)
    if (#self.m_rewardIdList < 2) then
        return
    end

    self.view.dungeonRewardSelectCell:InitDungeonRewardSelectCell(
        self.m_rewardIdList[1],
        self.m_currSelectIdx == 1,
        function()
            self:_SelectRewardIndex(1)
        end)
    self.view.dungeonRewardSelectCell2:InitDungeonRewardSelectCell(
        self.m_rewardIdList[2],
        self.m_currSelectIdx == 2,
        function()
            self:_SelectRewardIndex(2)
        end)
end



DungeonRewardSelectPopupCtrl._OnCloseBtnClick = HL.Method() << function(self)
    self:PlayAnimationOutAndClose()
end

DungeonRewardSelectPopupCtrl._SelectRewardIndex = HL.Method(HL.Number) << function(self, luaIndex)
    if self.m_currSelectIdx == luaIndex then
        return
    end

    if not DungeonUtils.isDungeonUnlock(self.m_dungeonId) then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_SET_CUSTOM_REWARD_INDEX_BUT_LOCKED_HINT)
        return
    end

    GameInstance.dungeonManager:SendSetCustomRewardIndexReq(self.m_dungeonId, CSIndex(luaIndex))
    self.m_currSelectIdx = luaIndex
    self:_RefreshUI()
end



HL.Commit(DungeonRewardSelectPopupCtrl)
