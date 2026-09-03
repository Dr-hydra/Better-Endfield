
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RiftDetailInfo
local MissionState = CS.Beyond.Gameplay.MissionSystem.MissionState
local QuestState = CS.Beyond.Gameplay.MissionSystem.QuestState




























RiftDetailInfoCtrl = HL.Class('RiftDetailInfoCtrl', uiCtrl.UICtrl)







RiftDetailInfoCtrl.s_messages = HL.StaticField(HL.Table) << {

}


local BEFORE_MISSION_TEXT = "ui_fac_rift_ui_desc_before_mission"
local AFTER_MISSION_TEXT = "ui_fac_rift_ui_desc_after_mission"
local RIFT_STEP_3_INIT_TEXT = "ui_fac_rift_ui_step_3_status_name_init"
local RIFT_STEP_3_TEXT = "ui_fac_rift_ui_step_3_status_name"

local RIFT_LIQUID_HEIGHT_SHADER_PER = 0.78





RiftDetailInfoCtrl.m_riftId = HL.Field(HL.Number) << 0




RiftDetailInfoCtrl.m_state = HL.Field(HL.Number) << -1


RiftDetailInfoCtrl.m_amount = HL.Field(HL.Number) << -1


RiftDetailInfoCtrl.m_amountMax = HL.Field(HL.Number) << -1


RiftDetailInfoCtrl.m_missionSystem = HL.Field(HL.Any)


RiftDetailInfoCtrl.m_facTechTreeSystem = HL.Field(HL.Any)




RiftDetailInfoCtrl.m_updateThread = HL.Field(HL.Thread)


RiftDetailInfoCtrl.m_lastFillPer = HL.Field(HL.Number) << -1


RiftDetailInfoCtrl.m_hasPlayedDoneAnimation = HL.Field(HL.Boolean) << false







RiftDetailInfoCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData(arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end



RiftDetailInfoCtrl.OnShow = HL.Override() << function(self)
    self.m_lastFillPer = -1
    self.m_hasPlayedDoneAnimation = false
    self:_UpdateAndRefreshAll(true)
    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_UpdateAndRefreshAll(false)
        end
    end)
end



RiftDetailInfoCtrl.PlayDoneAnimation = HL.Method() << function(self)
    self.view.leftProcessedNode:Play("riftdetailinfo_done")
    AudioAdapter.PostEvent("Au_UI_Mission_CrackNeutralComplete")
end




RiftDetailInfoCtrl.OpenRiftDetailPanel = HL.StaticMethod(HL.Table) << function(arg)
    PhaseManager:OpenPhase(PhaseId.RiftDetailInfo, arg)
end





RiftDetailInfoCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return {
        riftId = self.m_riftId,
    }
end





RiftDetailInfoCtrl._InitUI = HL.Method() << function(self)

    self.view.closeButton.onClick:AddListener(function()
        self:_CloseSelf()
    end)
    self.view.jumpMissionBtn.onClick:AddListener(function()
        self:_JumpMission()
    end)
    self.view.jumpTechBtn.onClick:AddListener(function()
        self:_JumpTech()
    end)
    self.view.btnCommonYellow.onClick:AddListener(function()
        self:_OnConfirmClick()
    end)
end




RiftDetailInfoCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_riftId = arg.riftId

    self.m_missionSystem = GameInstance.player.mission
    self.m_facTechTreeSystem = GameInstance.player.facTechTreeSystem
end









RiftDetailInfoCtrl._UpdateAndRefreshAll = HL.Method(HL.Boolean) << function(self, forceRefresh)
    local ok, newState, newAmount, newAmountMax =
        GameWorld.waterVolumeManager:TryGetRiftVolumeStateByPtrId(self.m_riftId)
    if not ok then
        logger.error("[RiftDetailInfoCtrl] TryGetRiftVolumeStateByPtrId failed, riftId = " .. tostring(self.m_riftId))
        return
    end

    local stateChanged = newState ~= self.m_state

    local amountChanged = newAmount ~= self.m_amount or newAmountMax ~= self.m_amountMax

    self.m_state = newState
    self.m_amount = newAmount
    self.m_amountMax = newAmountMax

    if forceRefresh or stateChanged or amountChanged then
        self:_RefreshStateUI()
        self:_RefreshAmountUI()
    end
end



RiftDetailInfoCtrl._RefreshStateUI = HL.Method() << function(self)
    local stateStr = self:_GetCurState(self.m_state, false)

    local _, textVal = CS.Beyond.I18n.I18nUtils.TryGetText(AFTER_MISSION_TEXT)
    self.view.riftDetailTxt.text = textVal

    if self.m_state == 0 then
        self.view.jumpMissionBtnNode.gameObject:SetActive(false)
        self.view.jumpTechBtnNode.gameObject:SetActive(false)
    elseif self.m_state == 1 then
        local techPointId = Tables.factoryConst.facRiftJumpToTechId
        local questId = Tables.factoryConst.facRiftJumpToTechQuestId
        local missionId = Tables.factoryConst.facRiftJumpToMissionId
        local questState = self.m_missionSystem:GetQuestState(questId)
        local missionState = self.m_missionSystem:GetMissionState(missionId)

        if missionState == MissionState.Available or missionState == MissionState.None then

            local _, beforeTextVal = CS.Beyond.I18n.I18nUtils.TryGetText(BEFORE_MISSION_TEXT)
            self.view.riftDetailTxt.text = beforeTextVal
            self.view.jumpMissionBtnNode.gameObject:SetActive(false)
            self.view.jumpTechBtnNode.gameObject:SetActive(false)
        elseif missionState == MissionState.Processing and
            (questState == QuestState.None or questState == QuestState.Failed or
            questState == QuestState.Disabled or questState == QuestState.Processing) then
            self.view.jumpMissionBtnNode.gameObject:SetActive(true)
            self.view.jumpTechBtnNode.gameObject:SetActive(false)
        elseif questState == CS.Beyond.Gameplay.MissionSystem.QuestState.Completed then
            self.view.jumpMissionBtnNode.gameObject:SetActive(false)
            local techLock = GameInstance.player.facTechTreeSystem:NodeIsLocked(techPointId)
            if techLock then
                self.view.jumpTechBtnNode.gameObject:SetActive(true)
            else

                self.view.jumpTechBtnNode.gameObject:SetActive(false)
                stateStr = self:_GetCurState(self.m_state, true)
            end
        end
    elseif self.m_state == 2 then
        self.view.jumpMissionBtnNode.gameObject:SetActive(false)
        self.view.jumpTechBtnNode.gameObject:SetActive(false)
    end

    self.view.riftState:SetState(stateStr)
end



RiftDetailInfoCtrl._RefreshAmountUI = HL.Method() << function(self)
    if self.m_amountMax <= 0 then
        return
    end

    local fillPer = self.m_amount / self.m_amountMax

    if self.m_amount == 0 then
        local _, textVal = CS.Beyond.I18n.I18nUtils.TryGetText(RIFT_STEP_3_INIT_TEXT)
        self.view.processedingTxt.text = textVal
    else
        local _, textVal = CS.Beyond.I18n.I18nUtils.TryGetText(RIFT_STEP_3_TEXT)
        self.view.processedingTxt.text = textVal
    end

    self.view.liquid_1.material:SetFloat("_LiquidHeight", fillPer * RIFT_LIQUID_HEIGHT_SHADER_PER)
    self.view.liquid_2.material:SetFloat("_LiquidHeight", fillPer * RIFT_LIQUID_HEIGHT_SHADER_PER)

    if fillPer >= 1 then
        self.view.percentNumTxt.text = string.format("%d", fillPer * 100)
    else
        self.view.percentNumTxt.text = string.format("%.1f", math.floor(fillPer * 1000) / 10)
    end
    self.view.processedBarNumTxt.text = string.format("%d/%d", self.m_amount, self.m_amountMax)
    self.view.progressBar.fillAmount = fillPer

    if not self.m_hasPlayedDoneAnimation and self.m_lastFillPer >= 0 and self.m_lastFillPer < 1 and fillPer >= 1 then
        self:PlayDoneAnimation()
    end
    if fillPer >= 1 then
        self.m_hasPlayedDoneAnimation = true
    end
    self.m_lastFillPer = fillPer
end



RiftDetailInfoCtrl._CloseSelf = HL.Method() << function(self)
    PhaseManager:PopPhase(PhaseId.RiftDetailInfo)
end



RiftDetailInfoCtrl._OnConfirmClick = HL.Method() << function(self)
    GameWorld.waterVolumeManager:DispatchRiftDetailConfirm(self.m_riftId)

    PhaseManager:PopPhase(PhaseId.RiftDetailInfo)
end



RiftDetailInfoCtrl._JumpMission = HL.Method() << function(self)
    local questId = Tables.factoryConst.facRiftJumpToTechQuestId
    local missionId = self.m_missionSystem:GetMissionIdByQuestId(questId)
    if string.isEmpty(missionId) then
        return
    end
    PhaseManager:GoToPhase(PhaseId.Mission, {autoSelect = missionId})
end



RiftDetailInfoCtrl._JumpTech = HL.Method() << function(self)
    local techPointId = Tables.factoryConst.facRiftJumpToTechId
    PhaseManager:OpenPhase(PhaseId.FacTechTree, {techId = techPointId}, function()
        if PhaseManager:IsOpen(PhaseId.RiftDetailInfo) then
            PhaseManager:ExitPhaseFast(PhaseId.RiftDetailInfo)
        end
    end)
end





RiftDetailInfoCtrl._GetCurState = HL.Method(HL.Number, HL.Boolean).Return(HL.String) << function(self, stateType,  isLock)
    local curState = "SuppressState"
    if stateType == 0 then
        curState = "SuppressState"
    elseif stateType == 1 then
        if isLock then
            curState = "ProcessedState"
        else
            curState = "SuppressState"
        end
    else
        curState = "ProcessedFinishState"
    end

    return curState
end










RiftDetailInfoCtrl.ShowRiftPanel = HL.StaticMethod(HL.Table) << function(args)
    local riftId = args[1]
    PhaseManager:OpenPhase(PhaseId.RiftDetailInfo, {
        riftId = riftId,
    })
end



HL.Commit(RiftDetailInfoCtrl)
