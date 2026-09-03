
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.GenderChange
local GENDER_CHANGE_PANEL_ID = PanelId.GenderChange
local CUT_SCENE_ID = "cutscene_e1m10_1"
local Stage = {
    INIT = 1,
    WAIT_FOR_SELECT = 2,
    WAIT_FOR_CUTSCENE = 3,
    WAIT_FOR_GENDER_CHANGED = 4,
    WAIT_FOR_CHARACTER_LOAD = 5,
    DONE = 6,
}
PhaseGenderChange = HL.Class('PhaseGenderChange', phaseBase.PhaseBase)

PhaseGenderChange.m_Stage = HL.Field(HL.Number) << Stage.INIT

PhaseGenderChange.m_targetGender = HL.Field(HL.Userdata)

PhaseGenderChange.m_genderSelectPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseGenderChange.m_waitCharLoadTick = HL.Field(HL.Number) << -1

PhaseGenderChange.m_transportPos = HL.Field(HL.Userdata)

PhaseGenderChange.m_transportRot = HL.Field(HL.Userdata)

PhaseGenderChange.m_transportId = HL.Field(HL.String) << ""






PhaseGenderChange.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONFIRM_GENDER] = { '_OnConfirmGender', true },
}



PhaseGenderChange._OnInit = HL.Override() << function(self)
    PhaseGenderChange.Super._OnInit(self)
    self.m_transportPos = self.arg[1]
    self.m_transportRot = self.arg[2]
    self.m_transportId = self.arg[3]
end

PhaseGenderChange._InitAllPhaseItems = HL.Override() << function(self)
    PhaseGenderChange.Super._InitAllPhaseItems(self)
    self:ChangeStage(Stage.WAIT_FOR_SELECT)
end




PhaseGenderChange.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseGenderChange._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGenderChange._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGenderChange._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGenderChange._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseGenderChange._OnActivated = HL.Override() << function(self)
end

PhaseGenderChange._OnDeActivated = HL.Override() << function(self)
end

PhaseGenderChange._OnDestroy = HL.Override() << function(self)
    PhaseGenderChange.Super._OnDestroy(self)
end



PhaseGenderChange.ChangeStage = HL.Method(HL.Number) << function(self, stage)
    if stage == Stage.WAIT_FOR_SELECT then
        local onStartTab = {}
        table.insert(onStartTab, function(isChangeGender)
            self:_OnConfirmChangePlayerGender(isChangeGender)
        end)
        self.m_genderSelectPanel = self:CreatePhasePanelItem(GENDER_CHANGE_PANEL_ID, onStartTab)
    elseif stage == Stage.WAIT_FOR_GENDER_CHANGED then
        if GameInstance.player.playerInfoSystem.gender == self.m_targetGender then
            self:ChangeStage(Stage.DONE)
        end
    elseif stage == Stage.WAIT_FOR_CHARACTER_LOAD then
        if self.m_waitCharLoadTick ~= -1 then
            return
        end
        self.m_waitCharLoadTick = LuaUpdate:Add("Tick", function(deltaTime)
            if GameInstance.playerController.mainCharacter.hasStarted then
                Utils.teleportToPositionByValidationId(self.m_transportId, function()
                    GameAction.BlackScreenFadeOut(1, true, false)
                end)
                self:ChangeStage(Stage.DONE)
                self.m_waitCharLoadTick = LuaUpdate:Remove(self.m_waitCharLoadTick)
            end
        end)
    elseif stage == Stage.DONE then
        self.m_waitCharLoadTick = LuaUpdate:Remove(self.m_waitCharLoadTick)
        PhaseManager:PopPhase(PhaseId.GenderChange)
    end
end

PhaseGenderChange._OnConfirmChangePlayerGender = HL.Method(HL.Boolean) << function(self, isChangeGender)
    if isChangeGender then

        local res = GameAction.PlayCutscene(CUT_SCENE_ID, function() self:_StartGenderChange() end, nil, nil, nil, nil, true)
        if not res then
            logger.error("Play Cutscene Failed !!")
            self:ChangeStage(Stage.DONE)
            return
        end

        if self.m_genderSelectPanel then
            self.m_genderSelectPanel.uiCtrl:PlayAnimationOutWithCallback(function()
                self:RemovePhasePanelItem(self.m_genderSelectPanel)
                self.m_genderSelectPanel = nil
            end)
        end
        self:ChangeStage(Stage.WAIT_FOR_CUTSCENE)
    else

        self:ChangeStage(Stage.DONE)
    end
end

PhaseGenderChange._StartGenderChange = HL.Method() << function(self)
    self.m_targetGender = CS.Proto.GENDER.GenFemale
    if GameInstance.player.playerInfoSystem.gender == CS.Proto.GENDER.GenFemale then
        self.m_targetGender = CS.Proto.GENDER.GenMale
    end
    local maskData = CS.Beyond.Gameplay.UICommonMaskData()
    maskData.maskType = UIConst.UI_COMMON_MASK_TYPE.WhiteScreen
    maskData.fadeInTime = 0
    maskData.waitHide = true
    GameAction.ShowBlackScreen(maskData)
    GameInstance.player.playerInfoSystem:SetGender(self.m_targetGender, false)
end
PhaseGenderChange._OnConfirmGender = HL.Method() << function(self)
    self:ChangeStage(Stage.WAIT_FOR_CHARACTER_LOAD)
end

HL.Commit(PhaseGenderChange)

