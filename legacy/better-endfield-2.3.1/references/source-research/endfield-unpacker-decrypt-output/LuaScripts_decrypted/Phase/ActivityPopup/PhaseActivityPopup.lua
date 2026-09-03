
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.ActivityPopup

PhaseActivityPopup = HL.Class('PhaseActivityPopup', phaseBase.PhaseBase)

PhaseActivityPopup.m_popupIds = HL.Field(HL.Table)





PhaseActivityPopup.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ACTIVITY_MANUALLY_POP_UP] = { "ManuallyPopup", false }
}




PhaseActivityPopup._OnInit = HL.Override() << function(self)
    PhaseActivityPopup.Super._OnInit(self)
    UIManager:ToggleBlockObtainWaysJump("PhaseActivityPopup", true, {})
end

PhaseActivityPopup._ShowPopUp = HL.Method(HL.Number) << function(self, index)
    local id = self.m_popupIds[index]
    if not GameInstance.player.activitySystem:GetActivity(id) then
        if index == #self.m_popupIds then
            PhaseManager:ExitPhaseFast(PHASE_ID)
        else
            self:_ShowPopUp(index + 1)
        end
        return
    end
    local panelId = Tables.activityTable[id].popUpPanelId
    ActivityUtils.recordPopup(id)
    self:CreatePhasePanelItem(PanelId[panelId], {
        activityId = id,
        closeCallback = function()
            self:RemovePhasePanelItemById(PanelId[panelId])
            if index == #self.m_popupIds then
                PhaseManager:ExitPhaseFast(PHASE_ID)
            else
                self:_ShowPopUp(index + 1)
            end
        end
    })
end

PhaseActivityPopup._OnDestroy = HL.Override() << function(self)
    PhaseActivityPopup.Super._OnDestroy(self)
    if self.arg and self.arg.closeCallback then
        self.arg.closeCallback()
    end
    UIManager:ToggleBlockObtainWaysJump("PhaseActivityPopup", false)
end

PhaseActivityPopup.ManuallyPopup = HL.StaticMethod(HL.Table) << function(args)

    if not BEYOND_DEBUG_COMMAND then
        return
    end


    local activityIds = {}
    local str = unpack(args)
    for id in string.gmatch(str, "([^,]+)") do
        if GameInstance.player.activitySystem:GetActivity(id) then
            table.insert(activityIds, id)
        else
            logger.error("PhaseActivityPopup.ManuallyPopup 不存在该活动id，跳过该拍脸：", id)
        end
    end
    if #activityIds == 0 then
        PhaseManager:ExitPhaseFast(PHASE_ID)
        return
    end


    PhaseManager:OpenPhaseFast(PHASE_ID, {
        manuallyPopup = true,
        popupIds = activityIds,
    })
end



PhaseActivityPopup._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)

    if BEYOND_DEBUG_COMMAND and self.arg and self.arg.manuallyPopup then
        self.m_popupIds = self.arg.popupIds
    else
        self.m_popupIds = ActivityUtils.getPopUpIds()
    end
    self:_ShowPopUp(1)
end


HL.Commit(PhaseActivityPopup)
