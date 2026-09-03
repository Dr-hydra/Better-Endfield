
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SocializeVisitTips

SocializeVisitTipsCtrl = HL.Class('SocializeVisitTipsCtrl', uiCtrl.UICtrl)

SocializeVisitTipsCtrl.m_isInit = HL.Field(HL.Boolean) << false





SocializeVisitTipsCtrl.s_messages = HL.StaticField(HL.Table) << {
}


SocializeVisitTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

SocializeVisitTipsCtrl.OnCellChange = HL.Method() << function(self)
    local friendInfo = GameInstance.player.spaceship:GetFriendRoleInfo()
    if friendInfo and friendInfo.roleId ~= 0 then
        self.view.socializeFriendName:InitSocializeFriendName(friendInfo.roleId)
    end
end

SocializeVisitTipsCtrl.OnShow = HL.Override() << function(self)
    self:OnCellChange()
    self.view.animationWrapper:ClearTween()
    if self.m_isInit then
        self.view.animationWrapper:PlayWithTween("socializevisittips_in_part_2")
        AudioManager.PostEvent("Au_UI_Toast_FriendVisitSide_Open")
    else
        self.view.animationWrapper:PlayWithTween("socializevisittips_in_part_1", function()
            self.m_isInit = true
        end)
        AudioManager.PostEvent("Au_UI_Toast_FriendVisitMain_Open")
    end
end
HL.Commit(SocializeVisitTipsCtrl)
