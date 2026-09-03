
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityCleaning

ActivityCleaningCtrl = HL.Class('ActivityCleaningCtrl', uiCtrl.UICtrl)

ActivityCleaningCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_PREPARE_TRANSITION_BACK_TO_TOP] = '_OnBackToTop',
}

ActivityCleaningCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityCleaningCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    args.jumpBtnCallBack = function()
        local _, activityData = Tables.activityTable:TryGetValue(self.m_activityId)
        local jumpId = activityData.detailJumpId

        self.view.main:PlayWithTween("activitycleaning_transition", function()
            Utils.jumpToSystem(jumpId)
        end)
        self.view.audioNode.enabled = false
    end
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    local stageIds = {}
    local _, multiStageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(self.m_activityId)
    for stageId, _ in pairs(multiStageCfg.stageList) do
        table.insert(stageIds, stageId)
    end
    local redDot = self.view.activityCommonInfo.view.gotoNode.btnDetailRedDot
    redDot:InitRedDot("ActivityGraffitiCleaning", self.m_activityId)
end

ActivityCleaningCtrl._OnBackToTop = HL.Method() << function(self)
    self.view.main:SampleClipAtPercent("activitycleaning_transition", 0)
    self.animationWrapper:PlayInAnimation()
    self.view.audioNode.enabled = true
end

HL.Commit(ActivityCleaningCtrl)
