
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.GenderSelect
PhaseGenderSelect = HL.Class('PhaseGenderSelect', phaseBase.PhaseBase)
local PHASE_GENDER_SELECT_OBJECT = "GameplayGenderSelect"
local EnterCutsceneId = "Cutscene_e0m0_1"
local DECO_NORMAL_LOOP_ANIM = "gender_deco_normal_loop"
local AVAILABLE_SELECTION = {
    None = "none",
    Male = "male",
    Female = "female",
}

local GENDER_HOVER_FIRST_DELAY = 1.2
local GENDER_HOVER_LOOP_ANIM = "gender_deco_hover_loop"
local TITLE_TEXT_MAT_PATH = "Assets/Beyond/DynamicAssets/Gameplay/Prefabs/GenderSelect/Materials/M_ui_cutscene_e0m0_1_tittletext_01.mat"





PhaseGenderSelect.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.GENDER_SELECT_START] = { 'OnGenderSelectStart', false },
    [MessageConst.ON_CONFIRM_GENDER] = { 'OnConfirmGender', true },
    [MessageConst.ON_GENDER_HOVER_CHANGE] = { 'OnGenderHoverChange', true },
    [MessageConst.ON_GENDER_HOVER_ANIM] = { 'GenderHoverPlayAnim', true },
}

PhaseGenderSelect.m_sceneObject = HL.Field(HL.Forward("PhaseGameObjectItem"))

PhaseGenderSelect.m_genderSelectPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseGenderSelect.m_genderConfirmPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseGenderSelect.m_firstActive = HL.Field(HL.Boolean) << true

PhaseGenderSelect.m_curSelection = HL.Field(HL.String) << AVAILABLE_SELECTION.None

PhaseGenderSelect.m_onFinishGenderSelect = HL.Field(HL.Any)

PhaseGenderSelect.m_effectCor = HL.Field(HL.Thread)

PhaseGenderSelect.m_genderHoverTimerId = HL.Field(HL.Number) << 0

PhaseGenderSelect.m_hadSelected = HL.Field(HL.Boolean) << false

PhaseGenderSelect.m_genderConfirmCheckUpdateKey = HL.Field(HL.Number) << -1

PhaseGenderSelect.m_timelineHandle = HL.Field(HL.Any)

PhaseGenderSelect.OnGenderSelectStart = HL.StaticMethod(HL.Any) << function(onFinishGenderSelect)
    logger.info(ELogChannel.GamePlay, "PhaseGenderSelect.OnGenderSelectStart")
    PhaseManager:OpenPhaseFast(PhaseId.GenderSelect, onFinishGenderSelect)
end


PhaseGenderSelect._OnInit = HL.Override() << function(self)
    PhaseGenderSelect.Super._OnInit(self)
    self.m_sceneObject = nil
end



PhaseGenderSelect._OnActivated = HL.Override() << function(self)

    UIManager:Hide(PanelId.Touch)

    logger.info(ELogChannel.GamePlay, "PhaseGenderSelect OnActivated")
    local arg = self.arg or {}

    if self.m_firstActive then
        self.m_onFinishGenderSelect = unpack(arg)

        self:_InitSceneObject()

        self.m_curSelection = AVAILABLE_SELECTION.None

        GameWorld.playerCenterUpdater:SetProxyRef(self.m_sceneObject.view.actor)


        logger.info(ELogChannel.GamePlay, "PhaseGenderSelect PlayEnterCutscene")
        self.m_timelineHandle = GameAction.PlayCutsceneAndGetHandle(EnterCutsceneId, function()
            self:OnNotificationActive()
        end)
    end

    self.m_firstActive = false
end

PhaseGenderSelect._InitSceneObject = HL.Method() << function(self)
    self.m_sceneObject = self:CreatePhaseGOItem(PHASE_GENDER_SELECT_OBJECT, PhaseManager.m_cacheRoot)
    local sceneObject = self.m_sceneObject
    local view = sceneObject.view

    self:_ToggleGenderSelectGameplay(false)

    local titleMaterial = self.m_resourceLoader:LoadI18NAsset(TITLE_TEXT_MAT_PATH, typeof(CS.UnityEngine.Material))
    view.titleTextRender.sharedMaterial = titleMaterial
    view.canvasMale.worldCamera = CameraManager.mainCamera
    view.canvasFemale.worldCamera = CameraManager.mainCamera
    view.hoverImageMale.gameObject:SetActive(false)
    view.hoverImageFemale.gameObject:SetActive(false)
end

PhaseGenderSelect.OnNotificationActive = HL.Method() << function(self)
    logger.info(ELogChannel.GamePlay, "PhaseGenderSelect OnNotificationActive")


    local sceneObject = self.m_sceneObject
    if not sceneObject then
        return
    end
    if not sceneObject.view then
        return
    end
    if not sceneObject.view.actor then
        return
    end

    AudioManager.PostEvent(AudioDataContainer.audioGlobalConfig.specialGameplayGenderSelectIn)
    AudioManager.PostEvent("au_music_lv000_elevator")
    self:_ToggleGenderSelectGameplay(true)



    self.m_genderSelectPanel = self:CreatePhasePanelItem(PanelId.GenderSelectController,{
        phase = self
    })
    self.m_genderConfirmPanel = self:CreatePhasePanelItem(PanelId.GenderSelectConfirmHolder,{
        phase = self
    })
    self.m_genderConfirmPanel.uiCtrl:Hide()
end

PhaseGenderSelect._ToggleGenderSelectGameplay = HL.Method(HL.Boolean) << function(self, isOn)
    local sceneObject = self.m_sceneObject
    if not sceneObject then
        return
    end
    if not sceneObject.view then
        return
    end

    sceneObject.view.actor.gameObject:SetActive(isOn)
    sceneObject.view.camera.gameObject:SetActive(isOn)
    sceneObject.view.others.gameObject:SetActive(isOn)
    sceneObject.view.light.gameObject:SetActive(isOn)
end

PhaseGenderSelect.ChooseFemale = HL.Method() << function(self)
    logger.info(ELogChannel.GamePlay, "PhaseGenderSelect ChooseFemale")
    AudioAdapter.PostEvent("Au_UI_Button_Player_Girl")
    self:_InnerChooseGender(AVAILABLE_SELECTION.Female)
end

PhaseGenderSelect.ChooseMale = HL.Method() << function(self)
    logger.info(ELogChannel.GamePlay, "PhaseGenderSelect ChooseMale")
    AudioAdapter.PostEvent("Au_UI_Button_Player_Boy")

    self:_InnerChooseGender(AVAILABLE_SELECTION.Male)
end

PhaseGenderSelect.ChooseNone = HL.Method() << function(self)
    logger.info(ELogChannel.GamePlay, "PhaseGenderSelect ChooseNone")

    self:_InnerChooseGender(AVAILABLE_SELECTION.None)
end

PhaseGenderSelect.OnGenderHoverChange = HL.Method(HL.Table) << function(self, arg)
    local view = self.m_sceneObject.view
    local isMale, isHover = unpack(arg)
    local wrapper = isMale and view.hoverImageMale or view.hoverImageFemale
    local wrapperStateController = isMale and view.hoverImageMaleStateController or view.hoverImageFemaleStateController
    if wrapperStateController == nil then
        return
    end
    if isHover then
        AudioAdapter.PostEvent("Au_UI_Hover_CharSelect")
        wrapperStateController:SetState("Choose")
        if wrapper:GetClipLength(GENDER_HOVER_LOOP_ANIM) > 0 then
            wrapper:PlayWithTween(GENDER_HOVER_LOOP_ANIM)
        end
    else
        wrapperStateController:SetState("NoChoose")
        if wrapper.curState ~= CS.Beyond.UI.UIConst.AnimationState.Out then
            wrapper:PlayWithTween(DECO_NORMAL_LOOP_ANIM)
        end
    end
end

PhaseGenderSelect._ClearGenderHoverTimer = HL.Method() << function(self)
    if self.m_genderHoverTimerId > 0 then
        TimerManager:ClearTimer(self.m_genderHoverTimerId)
    end

    self.m_genderHoverTimerId = 0
end

PhaseGenderSelect.GenderHoverPlayAnim = HL.Method(HL.Table) << function(self, arg)
    local isIn, delay = unpack(arg)
    local view = self.m_sceneObject.view
    local maleWrapper = view.hoverImageMale
    local femaleWrapper = view.hoverImageFemale
    local maleWrapperStateController = view.hoverImageMaleStateController
    local femaleWrapperStateController = view.hoverImageFemaleStateController
    maleWrapperStateController:SetState("NoChoose")
    femaleWrapperStateController:SetState("NoChoose")

    self:_ClearGenderHoverTimer()
    if isIn then
        if delay then
            self.m_genderHoverTimerId = TimerManager:StartTimer(GENDER_HOVER_FIRST_DELAY, function()
                maleWrapper.gameObject:SetActive(true)
                femaleWrapper.gameObject:SetActive(true)
                maleWrapper:PlayInAnimation(function()
                    maleWrapper:PlayWithTween(DECO_NORMAL_LOOP_ANIM)
                end)
                femaleWrapper:PlayInAnimation(function()
                    femaleWrapper:PlayWithTween(DECO_NORMAL_LOOP_ANIM)
                end)
                self:_ClearGenderHoverTimer()
            end)
        else
            maleWrapper.gameObject:SetActive(true)
            femaleWrapper.gameObject:SetActive(true)
            maleWrapper:PlayInAnimation(function()
                maleWrapper:PlayWithTween(DECO_NORMAL_LOOP_ANIM)
            end)
            femaleWrapper:PlayInAnimation(function()
                femaleWrapper:PlayWithTween(DECO_NORMAL_LOOP_ANIM)
            end)
        end
        maleWrapperStateController:SetState("NoChoose")
    else
        if delay then
            self.m_genderHoverTimerId = TimerManager:StartTimer(GENDER_HOVER_FIRST_DELAY, function()
                maleWrapper:PlayOutAnimation(function()
                    maleWrapper:PlayWithTween(DECO_NORMAL_LOOP_ANIM)
                end)
                femaleWrapper:PlayOutAnimation(function()
                    femaleWrapper:PlayWithTween(DECO_NORMAL_LOOP_ANIM)
                end)
                self:_ClearGenderHoverTimer()
            end)
        else
            maleWrapper:PlayOutAnimation(function()
                maleWrapper:PlayWithTween(DECO_NORMAL_LOOP_ANIM)
            end)
            femaleWrapper:PlayOutAnimation(function()
                femaleWrapper:PlayWithTween(DECO_NORMAL_LOOP_ANIM)
            end)
        end
    end

end

PhaseGenderSelect.ConfirmSelection = HL.Method() << function(self)

    if self.m_hadSelected then
        return
    end

    self.m_hadSelected = true
    local playerInfoSystem = GameInstance.player.playerInfoSystem
    local sceneObject = self.m_sceneObject

    local serverGender = CS.Proto.GENDER.GenFemale
    if self.m_curSelection == AVAILABLE_SELECTION.Female then
        sceneObject.view.ac_endminm.gameObject:SetActive(false)
        serverGender = CS.Proto.GENDER.GenFemale
        AudioManager.PostEvent("au_sfx_levelscript_elevatorfoley_f")
    elseif self.m_curSelection == AVAILABLE_SELECTION.Male then
        sceneObject.view.ac_endminf.gameObject:SetActive(false)
        serverGender = CS.Proto.GENDER.GenMale
        AudioManager.PostEvent("au_sfx_levelscript_elevatorfoley_m")
    end

    UIManager:Hide(PanelId.GenderSelectConfirmHolder)

    sceneObject.view.ac_camera:SetTrigger(string.format("CONFIRM_%s", self.m_curSelection))
    sceneObject.view.ac_endminf:SetTrigger(string.format("CONFIRM_%s", self.m_curSelection))
    sceneObject.view.ac_endminm:SetTrigger(string.format("CONFIRM_%s", self.m_curSelection))

    sceneObject.view.ac_camera:Update(0)
    sceneObject.view.ac_endminf:Update(0)
    sceneObject.view.ac_endminm:Update(0)

    sceneObject.view.hoverImageMale.gameObject:SetActive(false)
    sceneObject.view.hoverImageFemale.gameObject:SetActive(false)

    playerInfoSystem:SetGender(serverGender, true)
end

PhaseGenderSelect._InnerChooseGender = HL.Method(HL.String, HL.Opt(HL.Boolean)) << function(self, gender, isInit)
    local sceneObject = self.m_sceneObject

    sceneObject.view.ac_camera:SetTrigger(string.format("IN_%s", gender))
    sceneObject.view.ac_camera:Update(0)
    local realConfirmPanelView = self.m_genderConfirmPanel.uiCtrl:GetRealPanelView()

    if gender == AVAILABLE_SELECTION.Female then
        realConfirmPanelView.transform:SetParent(sceneObject.view.femaleConfirmPopupPos)
    elseif gender == AVAILABLE_SELECTION.Male then
        realConfirmPanelView.transform:SetParent(sceneObject.view.maleConfirmPopupPos)
    end

    realConfirmPanelView.transform:Reset()

    self.m_curSelection = gender
    self.m_effectCor = PhaseManager:_StartCoroutine(function()
        if gender == AVAILABLE_SELECTION.None then
            self.m_genderConfirmPanel.uiCtrl:Hide()
            coroutine.wait(sceneObject.view.config.SHOW_CONTROLLER_PANEL_DURATION)
            UIUtils.PlayAnimationAndToggleActive(sceneObject.view.hoverImageFemale, true)
            UIUtils.PlayAnimationAndToggleActive(sceneObject.view.hoverImageMale, true)
            self.m_genderSelectPanel.uiCtrl:Show()
        else
            self.m_genderSelectPanel.uiCtrl:PlayAnimationOutAndHide()
            UIUtils.PlayAnimationAndToggleActive(sceneObject.view.hoverImageFemale, false)
            UIUtils.PlayAnimationAndToggleActive(sceneObject.view.hoverImageMale, false)
            coroutine.wait(sceneObject.view.config.SHOW_CONFIRM_PANEL_DURATION)
            self.m_genderConfirmPanel.uiCtrl:Show()
        end
    end)
end

PhaseGenderSelect.OnConfirmGender = HL.Method() << function(self)

    logger.info(ELogChannel.GamePlay, "PhaseGenderSelect on gender confirm, start check loop")



    local checkLoopTime = 0
    self.m_genderConfirmCheckUpdateKey = LuaUpdate:Add("Tick", function(deltaTime)
        checkLoopTime = checkLoopTime + deltaTime


        if checkLoopTime > 15 then
            logger.error(ELogChannel.GamePlay, "PhaseGenderSelect check loop timeout")
            self:LeaveGenderSelect()
            return
        end
        local csLeaderIndex = GameInstance.player.squadManager.curSquad.leaderIndex
        local squadSlots = GameInstance.player.squadManager.curSquad.slots
        local leaderSlot = squadSlots[csLeaderIndex]
        if leaderSlot == nil then
            return
        end

        local camCanExit = false
        local sceneObject = self.m_sceneObject
        local ac_camera = sceneObject.view.ac_camera
        local curStateInfo = ac_camera:GetCurrentAnimatorStateInfo(0)
        if curStateInfo:IsName(string.format("CONFIRM_STATE_%s", self.m_curSelection)) and curStateInfo.normalizedTime > 0.95 then
            camCanExit = true
        end

        local leader = leaderSlot.character
        if leader.modelCom and leader.modelCom.isModelLoaded and camCanExit then
            logger.info(ELogChannel.GamePlay, "PhaseGenderSelect all model loaded, leave")

            self:LeaveGenderSelect()
        end
    end)
end

PhaseGenderSelect.LeaveGenderSelect = HL.Method() << function(self)
    AudioManager.PostEvent(AudioDataContainer.audioGlobalConfig.specialGameplayGenderSelectOut)

    LuaUpdate:Remove(self.m_genderConfirmCheckUpdateKey)
    local mainCharRoot = GameInstance.playerController.mainCharacter.rootCom
    if mainCharRoot then
        GameWorld.playerCenterUpdater:SetProxyRef(mainCharRoot.transform);
    end

    PhaseManager:ExitPhaseFast(PHASE_ID)
    if self.m_onFinishGenderSelect then
        self.m_onFinishGenderSelect()
    end
end

PhaseGenderSelect._OnDestroy = HL.Override() << function(self)
    PhaseGenderSelect.Super._OnDestroy(self)


    if self.m_timelineHandle then
        self.m_timelineHandle.afterFinish = nil
    end

    LuaUpdate:Remove(self.m_genderConfirmCheckUpdateKey)
    UIManager:Show(PanelId.Touch)
end




HL.Commit(PhaseGenderSelect)

