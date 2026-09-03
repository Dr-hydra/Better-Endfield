local SNSContentBase = require_ex('UI/Widgets/SNSContentBase')

local MissionState = CS.Beyond.Gameplay.MissionSystem.MissionState
local MissionType = CS.Beyond.Gameplay.MissionSystem.MissionType

SNSContentTask = HL.Class('SNSContentTask', SNSContentBase)

SNSContentTask._OnSNSContentInit = HL.Override() << function(self)
    local missionId = self.m_contentCfg.contentParam[0]

    local missionRuntimeAsset = GameInstance.player.mission:GetMissionInfo(missionId)

    local levelTableData = Tables.levelDescTable[missionRuntimeAsset.levelId]
    local icon = UIConst.MISSION_VIEW_TYPE_CONFIG[missionRuntimeAsset.viewType].missionIcon
    self.view.mainTxt.text = missionRuntimeAsset.missionName:GetText()
    self.view.subTxt.text = levelTableData.showName
    self.view.taskIcon:LoadSprite(UIConst.UI_SPRITE_MISSION_TYPE_ICON, icon)

    local importanceValue = missionRuntimeAsset.missionImportance:GetHashCode()
    self.view.taskAll:SetState(string.format("Importance%s", tostring(importanceValue)))
    local isComplete = GameInstance.player.mission:IsMissionCompleted(missionId)
    self.view.taskAll:SetState(isComplete and "CompleteNode" or "GotoNode")

    self.view.verticalLayoutGroup.padding.left = self.m_contentInfo.taskHorPadding
    self.view.verticalLayoutGroup.padding.right = self.m_contentInfo.taskHorPadding

    self.view.btnClick.onClick:RemoveAllListeners()
    self.view.btnClick.onClick:AddListener(function()



        if GameInstance.player.spaceship.isViewingFriend then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_OBTAIN_WAYS_JUMP_BLOCKED)
            return
        end

        Notify(MessageConst.ON_SNS_CONTENT_WIDGET_CLICK)

        local missionState = GameInstance.player.mission:GetMissionState(missionId)
        local otherCaseHintText = missionRuntimeAsset.missionType == MissionType.Misc and
                Language["ui_sns_toast_mission_misc_failed"] or Language["ui_mis_empty_default"]
        if missionState == MissionState.Processing then
            PhaseManager:OpenPhase(PhaseId.Mission, { autoSelect = missionId, useBlackMask = true })
        elseif isComplete then
            Notify(MessageConst.SHOW_TOAST, Language["ui_sns_toast_mission_completed"])
        else
            Notify(MessageConst.SHOW_TOAST, otherCaseHintText)
        end
    end)
end



SNSContentTask.CanSetTarget = HL.Override().Return(HL.Boolean) << function(self)
    return true
end

SNSContentTask.GetNaviTarget = HL.Override().Return(HL.Any) << function(self)
    return self.view.btnClick
end



HL.Commit(SNSContentTask)
return SNSContentTask
