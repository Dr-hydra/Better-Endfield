local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityMaterialSupply
local PHASE_ID = PhaseId.ActivityMaterialSupply
ActivityMaterialSupplyCtrl = HL.Class('ActivityMaterialSupplyCtrl', uiCtrl.UICtrl)






ActivityMaterialSupplyCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivityMaterialSupplyCtrl.m_activityId = HL.Field(HL.String) << ''


ActivityMaterialSupplyCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)

    self.view.activityCommonInfo.view.gotoNode.btnDetailRedDot:InitRedDot("ActivityGotoFoodSubmitMap", self.m_activityId)
    self.view.activityCommonInfo:UpdateGoToBtnDetailCallBack(function()
        local timeVal = ActivityUtils.GetFoodSubmitCurGoToRedDot()
        GameInstance.player.activitySystem:SetFoodSubmitGoToRedDotRecord(self.m_activityId, timeVal)
        Notify(MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE)
    end)

    self:_UpdateTabRedDot()

end

ActivityMaterialSupplyCtrl._UpdateTabRedDot = HL.Method() << function(self)
    GameInstance.player.activitySystem:SetFoodSubmitTabMissionRedDotRecord(self.m_activityId)

    for stageId, value in pairs(Tables.FoodSubmitStageIdTable) do
        if value.activityId == self.m_activityId then
            local showStage = false
            local stageState = ActivityUtils.GetFoodSubmitStageState(self.m_activityId, stageId)
            if stageState ~= GEnums.ActivityConditionalStageState.Locked then
                showStage = true
                GameInstance.player.activitySystem:SetFoodSubmitTabStageRedDotRecord(self.m_activityId, stageId)
            end
        end
    end
    Notify(MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE)
end

HL.Commit(ActivityMaterialSupplyCtrl)
