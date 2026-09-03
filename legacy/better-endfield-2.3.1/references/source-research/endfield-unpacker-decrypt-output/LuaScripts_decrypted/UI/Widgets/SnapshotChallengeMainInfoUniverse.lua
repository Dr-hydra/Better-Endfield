local SnapshotChallengeMainInfoCommon = require_ex('UI/Widgets/SnapshotChallengeMainInfoCommon')


SnapshotChallengeMainInfoUniverse = HL.Class('SnapshotChallengeMainInfoUniverse', SnapshotChallengeMainInfoCommon)

SnapshotChallengeMainInfoUniverse._RefreshContentUI = HL.Override(HL.Number) << function(self, stageIndex)
    SnapshotChallengeMainInfoUniverse.Super._RefreshContentUI(self, stageIndex)
    local stageInfo = self.m_info.stageInfoList[stageIndex]
    local viewNode = self.view
    local isComplete = stageInfo.state == self.StageStateEnum.Complete
    local inProgress = stageInfo.state == self.StageStateEnum.InProgress
    if inProgress then
        viewNode.stateNode:SetState("InProgress")
    elseif isComplete then
        viewNode.stateNode:SetState("Complete")
    else
        viewNode.stateNode:SetState("Received")
    end
end

SnapshotChallengeMainInfoUniverse._ChangeSelectStage = HL.Override(HL.Number) << function(self, stageIndex)
    local oldIndex = self.m_info.curSelectStage
    local oldStageNode = self.m_stageNodeList[oldIndex]
    oldStageNode.animationWrapper:PlayOutAnimation()
    local curStageNode = self.m_stageNodeList[stageIndex]
    curStageNode.animationWrapper:PlayInAnimation()
    local curStageInfo = self.m_info.stageInfoList[stageIndex]
    SnapshotChallengeMainInfoUniverse.Super._ChangeSelectStage(self, stageIndex)
end

SnapshotChallengeMainInfoUniverse._RefreshStageCell = HL.Override() << function(self)
    SnapshotChallengeMainInfoUniverse.Super._RefreshStageCell(self)
    local infoCount = #self.m_info.stageInfoList
    local maxUICount = self.view.config.MAX_STAGE_NUM
    for i = 1, maxUICount do
        local isShow = i <= infoCount
        if isShow then
            self.m_stageNodeList[i].animationWrapper:PlayLoopAnimation()
        end
    end
end


HL.Commit(SnapshotChallengeMainInfoUniverse)
return SnapshotChallengeMainInfoUniverse

