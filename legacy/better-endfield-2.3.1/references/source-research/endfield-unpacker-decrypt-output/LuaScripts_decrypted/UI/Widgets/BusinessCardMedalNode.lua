local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

BusinessCardMedalNode = HL.Class('BusinessCardMedalNode', UIWidgetBase)

BusinessCardMedalNode.m_canJumpTo = HL.Field(HL.Boolean) << false


BusinessCardMedalNode._OnFirstTimeInit = HL.Override() << function(self)
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        if not self.m_canJumpTo then
            return
        end
        PhaseManager:OpenPhase(PhaseId.AchievementMain)
    end)
end

BusinessCardMedalNode.InitBusinessCardMedalNode = HL.Method(HL.Any, HL.Boolean) << function(self, roleId, canJumpTo)
    self:_FirstTimeInit()
    self.m_canJumpTo = canJumpTo or false
    self.view.button.gameObject:SetActiveIfNecessary(self.m_canJumpTo)
    local medalMap = {}


    local success, friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
    if success and friendInfo.achievementInfo ~= nil and friendInfo.achievementInfo.Display ~= nil and friendInfo.achievementInfo.InfoList ~= nil then
        for slotIndex, achievementId in pairs(friendInfo.achievementInfo.Display) do
            if achievementId ~= 0 then
                for i = 0, friendInfo.achievementInfo.InfoList.Count - 1 do
                    local achievement = friendInfo.achievementInfo.InfoList[i]
                    if achievement.AchieveNumId == achievementId then
                        local strId = Tables.numIdStrTable["achieve_id"].dic:GetValue(achievementId)
                        medalMap[slotIndex] = {
                            achievementId = strId,
                            level = achievement.Level,
                            isPlated = achievement.IsPlated,
                            isRare = Tables.achievementTable[strId].applyRareEffect or false,
                        }
                        break
                    end
                end
            end
        end
    end

    self.view.medalNode:InitMedalGroup(medalMap, self.view.config.MAX_SLOT_COUNT)
end

HL.Commit(BusinessCardMedalNode)
return BusinessCardMedalNode

