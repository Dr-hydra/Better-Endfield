
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipClueHelp

SpaceshipClueHelpCtrl = HL.Class('SpaceshipClueHelpCtrl', uiCtrl.UICtrl)






SpaceshipClueHelpCtrl.s_messages = HL.StaticField(HL.Table) << {

}


SpaceshipClueHelpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeButton.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self:_InitData()
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

SpaceshipClueHelpCtrl.OnShow = HL.Override() << function(self)

end

SpaceshipClueHelpCtrl.OnHide = HL.Override() << function(self)
end

SpaceshipClueHelpCtrl.OnClose = HL.Override() << function(self)
end

SpaceshipClueHelpCtrl._InitData = HL.Method() << function(self)
    local hasValue, roomInfo = GameInstance.player.spaceship:TryGetRoom(Tables.spaceshipConst.guestRoomClueExtensionId)
    if hasValue and roomInfo then
        local roomLevel = roomInfo.lv
        local levelTable = SpaceshipUtils.getRoomLvTableByType(roomInfo.type)
        self.view.maxPeopleCount.text = levelTable[roomLevel].maxCalcRoleCount
        self.view.extraIntelligenceReward.text = string.format(Language.LUA_SPACESHIP_CLUE_EXTRA_REWARD_PER_PERSON, levelTable[roomLevel].extraInfoPerRole)
        self.view.extraCreditReward.text = string.format(Language.LUA_SPACESHIP_CLUE_EXTRA_REWARD_PER_PERSON, levelTable[roomLevel].extraCreditPerRole)
    end
end

HL.Commit(SpaceshipClueHelpCtrl)
