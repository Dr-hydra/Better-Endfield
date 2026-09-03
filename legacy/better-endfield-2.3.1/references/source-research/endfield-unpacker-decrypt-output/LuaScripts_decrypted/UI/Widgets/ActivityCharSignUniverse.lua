local ActivityCharSignCommon = require_ex('UI/Widgets/ActivityCharSignCommon')

ActivityCharSignUniverse = HL.Class('ActivityCharSignUniverse', ActivityCharSignCommon)

ActivityCharSignUniverse.Init = HL.Override(HL.Table) << function(self, args)
    ActivityCharSignUniverse.Super.Init(self, args)
    local nameStr, avatarPath, avatarFramePath = FriendUtils.getFriendInfoByRoleId(GameInstance.player.roleId)
    self.view.nameTxt.text = nameStr
    self.view.bgImage.onHoverChange:RemoveAllListeners()
    self.view.bgImage.onHoverChange:AddListener(function(isHover)
        if isHover then
            AudioManager.PostEvent("Au_UI_Event_SpaceCheckinName")
        end
    end)
end

HL.Commit(ActivityCharSignUniverse)
return ActivityCharSignUniverse