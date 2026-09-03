local CommonPopUpCtrl = require_ex('UI/Panels/CommonPopUp/CommonPopUpCtrl')
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SimulationTraining
local PHASE_ID = PhaseId.SimulationTraining






































































SimulationTrainingCtrl = HL.Class('SimulationTrainingCtrl', uiCtrl.UICtrl)







SimulationTrainingCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SIMULATION_TRAINING_DRAW_FIRST] = '_HandleFirstDrawCard',
    [MessageConst.ON_SIMULATION_TRAINING_DRAW_NORMAL] = '_HandleNormalDrawCard',
    [MessageConst.ON_SIMULATION_TRAINING_DOUBLE] = '_HandleDouble',
    [MessageConst.ON_SIMULATION_TRAINING_DAILY_REFRESH] = '_OnSimulationTrainingDailyRefresh',
    [MessageConst.ON_SIMULATION_TRAINING_GIVE_UP] = '_OnSimulationTrainingGiveUp',
    [MessageConst.ON_ACTIVITY_UPDATED] = '_OnLimitedActivityEnd',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE] = '_OnLimitTaskProgress',
}


SimulationTrainingCtrl.m_enemyCells = HL.Field(HL.Forward("UIListCache"))


SimulationTrainingCtrl.m_remainCardCells = HL.Field(HL.Forward("UIListCache"))


SimulationTrainingCtrl.m_rewardPointCells = HL.Field(HL.Forward("UIListCache"))


SimulationTrainingCtrl.m_system = HL.Field(HL.Any)


SimulationTrainingCtrl.m_enemyIds = HL.Field(HL.Table)


SimulationTrainingCtrl.m_enemyLevels = HL.Field(HL.Table)


SimulationTrainingCtrl.m_openDouble = HL.Field(HL.Boolean) << false


SimulationTrainingCtrl.m_poolAllCardPoint2Num = HL.Field(HL.Table)


SimulationTrainingCtrl.m_remainCardPoint2Num = HL.Field(HL.Table)


SimulationTrainingCtrl.m_lastPointNumber = HL.Field(HL.Number) << 0


SimulationTrainingCtrl.m_curPointNumber = HL.Field(HL.Number) << 0


SimulationTrainingCtrl.m_infoTickHandle = HL.Field(HL.Number) << -1


SimulationTrainingCtrl.m_curShowPoolName = HL.Field(HL.String) << ""


SimulationTrainingCtrl.m_entityLid = HL.Field(HL.Number) << 0


SimulationTrainingCtrl.m_dailyPlayCnt = HL.Field(HL.Number) << 0


SimulationTrainingCtrl.m_unlimitedMode = HL.Field(HL.Boolean) << false


SimulationTrainingCtrl.m_commonToggleInitialed = HL.Field(HL.Boolean) << false


SimulationTrainingCtrl.m_floorState = HL.Field(HL.Any) << nil


SimulationTrainingCtrl.m_lastMainState = HL.Field(HL.Any) << nil


SimulationTrainingCtrl.m_curMainState = HL.Field(HL.Any) << nil


SimulationTrainingCtrl.m_playDrawCardVxIng = HL.Field(HL.Boolean) << false


SimulationTrainingCtrl.m_playRewardSlideVxIng = HL.Field(HL.Boolean) << false


SimulationTrainingCtrl.m_canDrawCard = HL.Field(HL.Boolean) << false


SimulationTrainingCtrl.m_luaIndex2rewardPointCell = HL.Field(HL.Table) << nil


SimulationTrainingCtrl.m_luaIndex2rewardCellX = HL.Field(HL.Table) << nil


SimulationTrainingCtrl.m_luaIndex2rewardNum = HL.Field(HL.Table) << nil


SimulationTrainingCtrl.m_lastRewardPointLuaIndex = HL.Field(HL.Number) << 1


SimulationTrainingCtrl.m_currentRewardPointLuaIndex = HL.Field(HL.Number) << 1


SimulationTrainingCtrl.m_tween = HL.Field(HL.Any) << nil


SimulationTrainingCtrl.m_inited = HL.Field(HL.Boolean) << false


SimulationTrainingCtrl.m_openSwitchInVsPost = HL.Field(HL.Boolean) << false


SimulationTrainingCtrl.m_limitTaskEndTime = HL.Field(HL.Number) << 0


SimulationTrainingCtrl.m_limitTaskActivityId = HL.Field(HL.String) << ''


SimulationTrainingCtrl.m_allTaskRewarded = HL.Field(HL.Boolean) << false


local PanelState = {
    Standard = "Standard",
    Infinity = "Infinity",
}

local MainState = {
    Normal = "Normal",
    Double = "Double",
    Point10 = "Point10",
    Overflow = "Overflow",
}

local RightState = {
    Extractable = "Extractable",
    NoExtractable = "NoExtractable",
}

local BottomState = {
    DoubleSwitchActive = "DoubleSwitchActive",
    DoubleSwitchDeActive = "DoubleSwitchDeActive",
    ActiveAllBtn = "ActiveAllBtn",
    DeActiveAllBtn = "DeActiveAllBtn",
}

local EnemyCellPointImg = {
    [1] = "icon_simulation_training_big_1",
    [2] = "icon_simulation_training_big_2",
    [3] = "icon_simulation_training_big_3",
    [4] = "icon_simulation_training_big_4",
    [5] = "icon_simulation_training_big_5",
}

local PointNumberImg = {
    [1] = "icon_simulation_training_number_1",
    [2] = "icon_simulation_training_number_2",
    [3] = "icon_simulation_training_number_3",
    [4] = "icon_simulation_training_number_4",
    [5] = "icon_simulation_training_number_5",
}

local RemainCellPointImg = {
    [1] = "icon_simulation_training_1",
    [2] = "icon_simulation_training_2",
    [3] = "icon_simulation_training_3",
    [4] = "icon_simulation_training_4",
    [5] = "icon_simulation_training_5",
}

local MAX_POINT_NUMBER = 10
local PUNISH_POINT_11 = 11
local PUNISH_POINT_22 = 22
local MAX_CARD_NUMBER = 5
local TOGGLE_NEVER_TIP_CLOSE_FLAG = "TOGGLE_NEVER_TIP_CLOSE_FLAG"
local TOGGLE_NEVER_TIP_DRAW_2_FLAG = "TOGGLE_NEVER_TIP_DRAW_2_FLAG"
local IMG_FOLDER = "SimulationTraining"

local SIMULATION_TRAINING_SUB_GAME_ID = "world_poi_simulation_training_01"



SimulationTrainingCtrl.ShowSimulationTraining = HL.StaticMethod(HL.Opt(HL.Table)) << function(args)
    PhaseManager:OpenPhase(PHASE_ID, args)
end





SimulationTrainingCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    AudioAdapter.PostEvent("Au_UI_Menu_GamblePOI_Open")
    self.m_system = GameInstance.player.simulationTrainingSystem
    self.m_entityLid = self.m_system.lastEntityLid

    self.m_dailyPlayCnt = self.m_system.dailyPlayCnt
    self.m_unlimitedMode = self.m_system.unlimitedMode

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.m_openDouble = self.m_system.doublePrize

    self.view.helpBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_INTRO, "simulation_training")
    end)

    self.view.btnClose.onClick:AddListener(function()
        self:_OnClickBtnClose()
    end)

    self.view.enemyInformationBtn.onClick:AddListener(function()

        self:_OpenEnemyInfoPanel(1)
    end)

    self.view.exitBtn.onClick:AddListener(function()
        if DeviceInfo.usingController then
            AudioAdapter.PostEvent("Au_UI_Button_Cancel")
        end
        self:_OnClickGiveUpBtn()
    end)

    self.view.exitBtn.onPressStart:AddListener(function()
        AudioAdapter.PostEvent("Au_UI_Button_Cancel")
    end)

    self.view.challengeBtn.onClick:AddListener(function()
        self:_OnClickChallengeBtn()
    end)

    self.view.drawCardBtn.onClick:AddListener(function()
        self:_OnClickDrawCard()
    end)

    self.view.limitTaskBtn.onClick:AddListener(function()
        self:_OnClickLimitTaskBtn()
    end)
    self:_UpdateLimitTaskBtnVisible()

    self.m_enemyCells = UIUtils.genCellCache(self.view.simulationTrainingCardCell)
    self.m_remainCardCells = UIUtils.genCellCache(self.view.remainCardCell)
    self.m_rewardPointCells = UIUtils.genCellCache(self.view.rewardPointCell)

    self:_UpdateInfoTick(0)
    self.m_infoTickHandle = LuaUpdate:Add("Tick", function(deltaTime)
        self:_UpdateInfoTick(deltaTime)
    end)

    self.view.commonToggle:InitCommonToggle(function(isOn)
        if self.m_commonToggleInitialed then
            if self.m_system.curLevel <= 1  then
                self.view.commonToggle:SetValue(self.m_openDouble, true)
                Notify(MessageConst.SHOW_TOAST, Language.LUA_SIMULATION_TRAINING_DOUBLE_LEVEL_1_TOAST)
                return
            end
            if self.m_system.dailyDoubleCnt == 0 or self.m_system.curHandCards.Count == 0 or self.m_system.curHandCards.Count > 2 then
                if self.m_system.dailyDoubleCnt == 0 then
                    self.view.commonToggle:SetValue(self.m_openDouble, true)
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_SIMULATION_TRAINING_DOUBLE_CNT_ZERO_TOAST)
                    return
                end
                self.view.commonToggle:SetValue(self.m_openDouble, true)
                if self.m_system.curHandCards.Count == 0 then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_SIMULATION_TRAINING_CLICK_DOUBLE_NO_CARD_TOAST)
                else
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_SIMULATION_TRAINING_CLICK_DOUBLE_TOAST)
                end
                return
            end
        end

        if isOn ~= self.m_openDouble then
            self.m_openDouble = isOn
            if self.m_system.curHandCards.Count > 0 and self.m_system.curHandCards.Count <= 2 then
                self.m_system:SimulationTrainingDouble(self.m_openDouble)
            end
        end

        local showDoubleText = string.format(Language.LUA_SIMULATION_TRAINING_NUMBER_OF_DOUBLE_LEFT, self.m_system.dailyDoubleCnt)
        self.view.doubleLeftTxt.text = showDoubleText
        self.view.doubleLeftRewardText.text = I18nUtils.CombineStringWithLanguageSpilt("", Language.ui_simulationtraining_main_double_reward)
        self:_UpdateOpenDoubleShow()
    end, self.m_openDouble)
    self.m_commonToggleInitialed = true
    self.view.rewardPointVxCell.gameObject:SetActive(false)
    self:_InitTopMiddleTitle()
    self:_HandleDrawCardVxPre()
    self:_HandleDrawCardVxPost()
    self:_InitTipsPopup()
    self.view.decoVxNode.gameObject:SetActive(false)
    self.view.cardDecoVxCell.gameObject:SetActive(false)
    self.view.limitTaskRedDot:InitRedDot("ActivitySimulationTrainingLimitTaskRedDot", "activity_simulation_training_1")
    self:_UpdateLimitTaskState()
    self:_UpdateLimitTaskDoneNode()
    self.m_inited = true
end



SimulationTrainingCtrl._HandleFirstDrawCard = HL.Method() << function(self)
    self:_InitTopMiddleTitle()
    self.m_playDrawCardVxIng = true
    self.m_playRewardSlideVxIng = true
    self:_HandleDrawCardVxPre()
    self.view.decoVxNode.gameObject:SetActive(true)
    self.view.cardDecoVxCell.gameObject:SetActive(true)
    self.view.middleAnimationWrapper:PlayWithTween("simulationtraining_draw1", function()
        AudioAdapter.PostEvent("Au_UI_Event_GambleInlay")
        self:_HandleDrawCardVxPost()
        self.view.decoVxNode.gameObject:SetActive(false)
        self.view.cardDecoVxCell.gameObject:SetActive(false)
    end)
    self:_HandleCardDecoVxCell()
end



SimulationTrainingCtrl._HandleCardDecoVxCell = HL.Method() << function(self)
    if self.m_system.curHandCards.Count == 0 then
        return
    end
    local cell = self.view.cardDecoVxCell
    cell.stateController:SetState("Normal")
    local cardName = self.m_system.curHandCards[self.m_system.curHandCards.Count - 1]
    local hasCard, cardData = Tables.SimulationTrainingCardTable:TryGetValue(cardName)
    if hasCard then
        local cardPoint = cardData.cardPoint
        if cardData.enemyIdList.Count > 0 then
            local enemyId = cardData.enemyIdList[0]
            local enemyCount = cardData.enemyCountList[0]
            local enemyInfo = UIUtils.getEnemyInfoByIdAndLevel(enemyId, cardData.enemyLevel[0])
            cell.nameTxt.text = string.format(Language.LUA_SIMULATION_TRAINING_ENEMY_SHOW_NAME_AND_COUNT, enemyInfo.name, enemyCount)
            cell.lvTxt.text = enemyInfo.level + self.m_system.deBuffMonsterLevel
            cell.iconImg:LoadSprite(UIConst.UI_SPRITE_MONSTER_ICON_BIG, enemyInfo.templateId)
            cell.numberImg:LoadSprite(IMG_FOLDER, PointNumberImg[cardPoint])
            cell.numberImg.gameObject:SetActive(true)
            cell.numberImgVx.gameObject:SetActive(false)
            cell.pointsImg:LoadSprite(IMG_FOLDER, EnemyCellPointImg[cardPoint])


        end
    end
end




SimulationTrainingCtrl._HandleNormalDrawCard = HL.Method() << function(self)
    local vxName = "simulationtraining_draw3"
    if self.m_system.curHandCards.Count == 2 then
        vxName = "simulationtraining_draw2"
    elseif self.m_system.curHandCards.Count == 3 then
        vxName = "simulationtraining_draw3"
    elseif self.m_system.curHandCards.Count == 4 then
        vxName = "simulationtraining_draw4"
    elseif self.m_system.curHandCards.Count == 5 then
        vxName = "simulationtraining_draw5"
    end

    self.m_playDrawCardVxIng = true
    self.m_playRewardSlideVxIng = true
    self:_HandleDrawCardVxPre()
    self.view.decoVxNode.gameObject:SetActive(true)
    self.view.cardDecoVxCell.gameObject:SetActive(true)
    self.view.middleAnimationWrapper:PlayWithTween(vxName, function()
        AudioAdapter.PostEvent("Au_UI_Event_GambleInlay")
        self:_HandleDrawCardVxPost()
        self.view.decoVxNode.gameObject:SetActive(false)
        self.view.cardDecoVxCell.gameObject:SetActive(false)
    end)

    self:_HandleCardDecoVxCell()
end



SimulationTrainingCtrl._HandleDouble = HL.Method() << function(self)
    self:_HandleDrawCardVxPre()
    self:_HandleDrawCardVxPost()
end



SimulationTrainingCtrl._OnSimulationTrainingGiveUp = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end



SimulationTrainingCtrl._OnSimulationTrainingDailyRefresh = HL.Method() << function(self)

end



SimulationTrainingCtrl.OnShow = HL.Override() << function(self)
    local curPoolId = self.m_system:GetValidCardPoolId()
    if self.m_curShowPoolName ~= "" and curPoolId ~= self.m_curShowPoolName then
        self:_UpdateRemainCardCells()
    end
end


SimulationTrainingCtrl.OnHide = HL.Override() << function(self)

end


SimulationTrainingCtrl.OnClose = HL.Override() << function(self)
    AudioAdapter.PostEvent("Au_UI_Menu_GamblePOI_Close")

    if self.m_infoTickHandle > 0 then
        self.m_infoTickHandle = LuaUpdate:Remove(self.m_infoTickHandle)
    end

    for i = 1, self.m_enemyCells:GetCount() do
        local cell = self.m_enemyCells:GetItem(i)
        if cell then
            cell.infoButton.onClick:RemoveAllListeners()
        end
    end
end




SimulationTrainingCtrl._UpdateInfoTick = HL.Method(HL.Number) << function(self, deltaTime)
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()

    local rotationInterval = Tables.simulationTrainingConst.rotationInterval
    local nextRefreshTs = self.m_system.lastRefreshTs + rotationInterval * 24 * 3600
    local disTime = nextRefreshTs - curTime
    local shortLeftTime = UIUtils.getShortLeftTime(disTime)
    self.view.poolTimeTxt.text = string.format(Language.LUA_SIMULATION_TRAINING_POOL_LEFT_TIME, shortLeftTime)

    if self.m_limitTaskEndTime and self.m_limitTaskEndTime > 0 then
        local remainSec = self.m_limitTaskEndTime - curTime
        if remainSec > 0 then
            self.view.limitTaskTimeTxt.text = UIUtils.getLeftTime(remainSec)
        else
            self.m_limitTaskEndTime = 0
            if self.view.limitTaskBtn.gameObject.activeSelf then
                self.view.limitTaskBtn.gameObject:SetActive(false)
                Notify(MessageConst.SHOW_POP_UP, {
                    content = Language.LUA_SIMULATION_TRAINING_LIMITED_ACTIVITY_END_POP_UP,
                    hideCancel = true,
                })
            end
        end
    end
end

SimulationTrainingCtrl._OnLimitedActivityEnd = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if activityId ~= self.m_limitTaskActivityId then
        return
    end
    self:_UpdateLimitTaskBtnVisible()
end



SimulationTrainingCtrl._OnClickBtnClose = HL.Method() << function(self)
    if self.m_system.curHandCards.Count == 0 then
        PhaseManager:PopPhase(PHASE_ID)
        return
    end
    local succ, neverTip = ClientDataManagerInst:GetBool(GameInstance.player.roleId..TOGGLE_NEVER_TIP_CLOSE_FLAG, true)
    if succ and neverTip then
        PhaseManager:PopPhase(PHASE_ID)
        return
    end

    local toggleNeverTip = false
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_SIMULATION_TRAINING_CLOSE_TIP_POPUP_TITLE,
        subContent = "",
        onConfirm = function()
            if toggleNeverTip then
                ClientDataManagerInst:SetBool(GameInstance.player.roleId..TOGGLE_NEVER_TIP_CLOSE_FLAG, true, true)
            end
            PhaseManager:PopPhase(PHASE_ID)
        end,
        onCancel = nil,
        confirmText = Language.LUA_SIMULATION_TRAINING_POPUP_CONFIRM_TEXT,
        cancelText = Language.LUA_SIMULATION_TRAINING_POPUP_CANCEL_TEXT,
        toggle = {
            isOn = false,
            onValueChanged = function(isOn)
                toggleNeverTip = isOn
            end,
            toggleText = Language.LUA_SIMULATION_TRAINING_NEVER_TIP_TEXT,
            styleType = CommonPopUpCtrl.EToggleStyle.Square,
            onHintTextId = "LUA_SIMULATION_TRAINING_NEVER_TIP_CONTROLLER_TEXT",
            offHintTextId = "LUA_SIMULATION_TRAINING_NEVER_TIP_CONTROLLER_TEXT",
        },
    })
end


SimulationTrainingCtrl._UpdateLimitTaskBtnVisible = HL.Method() << function(self)
    local activitySystem = GameInstance.player.activitySystem
    local activityId = "activity_simulation_training_1"
    local activityData = activitySystem:GetActivity(activityId)
    if activityData and activityData.endTime > 0 then
        local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
        if activityData.endTime - curTime > 0 then
            self.m_limitTaskActivityId = activityId
            self.m_limitTaskEndTime = activityData.endTime
            self.view.limitTaskBtn.gameObject:SetActive(true)
            return
        end
    end
    self.m_limitTaskEndTime = 0
    self.view.limitTaskBtn.gameObject:SetActive(false)
end

SimulationTrainingCtrl._OnClickLimitTaskBtn = HL.Method() << function(self)
    PhaseManager:OpenPhase(PhaseId.SimulationTrainingTask)
end

SimulationTrainingCtrl._UpdateLimitTaskState = HL.Method() << function(self)
    if self.m_limitTaskActivityId == '' then
        self.m_allTaskRewarded = false
        return
    end

    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_limitTaskActivityId)
    if not activityData then
        self.m_allTaskRewarded = false
        return
    end

    local _, taskConfig = Tables.activityConditionalMultiStageTaskConfigTable:TryGetValue(self.m_limitTaskActivityId)
    if not taskConfig then
        self.m_allTaskRewarded = false
        return
    end

    local allRewarded = true
    for id, _ in pairs(taskConfig.TaskConfigMap) do
        local taskStatusData = activityData:GetTaskData(id)
        if taskStatusData ~= nil then
            local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskStatusData.Status)
            if status ~= GEnums.ActivityConditionalTaskState.Rewarded then
                allRewarded = false
                break
            end
        else
            allRewarded = false
            break
        end
    end

    self.m_allTaskRewarded = allRewarded
end

SimulationTrainingCtrl._UpdateLimitTaskDoneNode = HL.Method() << function(self)
    self.view.limitTaskDoneNode.gameObject:SetActive(self.m_allTaskRewarded)
end




SimulationTrainingCtrl._OnLimitTaskProgress = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if activityId ~= self.m_limitTaskActivityId then
        return
    end
    self:_UpdateLimitTaskState()
    self:_UpdateLimitTaskDoneNode()
end

SimulationTrainingCtrl._OnClickDrawCard = HL.Method() << function(self)
    if self.m_system.curHandCards.Count == 5 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SIMULATION_TRAINING_DRAW_5_TOAST)
        return
    end

    if self.m_playDrawCardVxIng then
        return
    end
    if self.m_playRewardSlideVxIng then
        return
    end

    if not self.m_canDrawCard then
        return
    end

    if self.m_system.curHandCards.Count == 2 and self.m_system.curLevel > 1 and not self.m_unlimitedMode then
        local toggleNeverTip = false
        local succ, neverTip = ClientDataManagerInst:GetBool(GameInstance.player.roleId..TOGGLE_NEVER_TIP_DRAW_2_FLAG, true)
        if succ and neverTip then
            toggleNeverTip = true
        end
        if toggleNeverTip or self.m_unlimitedMode then
            AudioAdapter.PostEvent("Au_UI_Button_GambleRoll")
            self.m_system:DrawCardNormal()
        else
            Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_SIMULATION_TRAINING_DOUBLE_TIP_POPUP_TITLE,
                subContent = "",
                onConfirm = function()
                    if toggleNeverTip then
                        ClientDataManagerInst:SetBool(GameInstance.player.roleId..TOGGLE_NEVER_TIP_DRAW_2_FLAG, true, true)
                    end
                    AudioAdapter.PostEvent("Au_UI_Button_GambleRoll")
                    self.m_system:DrawCardNormal()
                end,
                onCancel = nil,
                confirmText = Language.LUA_SIMULATION_TRAINING_POPUP_CONFIRM_TEXT,
                cancelText = Language.LUA_SIMULATION_TRAINING_POPUP_CANCEL_TEXT,
                toggle = {
                    isOn = false,
                    onValueChanged = function(isOn)
                        toggleNeverTip = isOn
                    end,
                    toggleText = Language.LUA_SIMULATION_TRAINING_NEVER_TIP_TEXT,
                    styleType = CommonPopUpCtrl.EToggleStyle.Square,
                    onHintTextId = "LUA_SIMULATION_TRAINING_NEVER_TIP_CONTROLLER_TEXT",
                    offHintTextId = "LUA_SIMULATION_TRAINING_NEVER_TIP_CONTROLLER_TEXT",
                },
            })
        end
        return
    end

    if self.m_system.curHandCards.Count == 0 then
        if self.m_curShowPoolName ~= self.m_system.serverCardPoolId then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SIMULATION_TRAINING_CHANGE_POOL_TOAST)
            PhaseManager:PopPhase(PHASE_ID)
            return
        end
        AudioAdapter.PostEvent("Au_UI_Button_GambleRoll")
        self.m_system:DrawCardFirst()
    else
        AudioAdapter.PostEvent("Au_UI_Button_GambleRoll")
        self.m_system:DrawCardNormal()
    end
end



SimulationTrainingCtrl._OnClickChallengeBtn = HL.Method() << function(self)
    PhaseManager:GoToPhase(PhaseId.CharFormation, {
        customTitle = Language.LUA_SIMULATION_TRAINING_CHAR_FORMATION_TITLE,
        closeInfoBtn = true,
        startBtnCallback = function()
            GameInstance.player.worldEnergyPointSystem:SendReqStartWorldEnergyPoint(SIMULATION_TRAINING_SUB_GAME_ID, self.m_entityLid)
            PhaseManager:ExitPhaseFast(PhaseId.CharFormation)
            PhaseManager:PopPhase(PHASE_ID)
            self.m_system:SimulationTrainingFloorChange(self.m_floorState)

            local maskData = CS.Beyond.Gameplay.UICommonMaskData()
            maskData.fadeType = UIConst.UI_COMMON_MASK_FADE_TYPE.FadeIn
            maskData.fadeInTime = 0.1
            maskData.fadeWaitTime = 30
            Notify(MessageConst.ON_COMMON_MASK_HIGH_START, {maskData})
        end,
        startBtnText = Language.LUA_SIMULATION_TRAINING_CHAR_FORMATION_CHALLENGE_BTN,
    })
end



SimulationTrainingCtrl._OnClickGiveUpBtn = HL.Method() << function(self)
    if self.m_unlimitedMode then

        self.m_system:SimulationTrainingGiveUp()
        return
    end

    if self.m_system.dailyFoldCnt > 0 then
        Notify(MessageConst.SHOW_POP_UP, {
            content = string.format(
                Language.LUA_SIMULATION_TRAINING_GIVE_UP_POPUP_HAVE_EXIT, self.m_system.dailyFoldCnt
            ),
            subContent = "",
            onConfirm = function()
                self.m_system:SimulationTrainingGiveUp()
            end,
            onCancel = nil,
            confirmText = Language.LUA_SIMULATION_TRAINING_POPUP_CONFIRM_TEXT,
            cancelText = Language.LUA_SIMULATION_TRAINING_POPUP_CANCEL_TEXT,
        })
    else
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_SIMULATION_TRAINING_GIVE_UP_POPUP_NO_EXIT,
            subContent = "",
            onConfirm = function()
                self.m_system:SimulationTrainingGiveUp()
            end,
            onCancel = nil,
            confirmText = Language.LUA_SIMULATION_TRAINING_POPUP_CONFIRM_TEXT,
            cancelText = Language.LUA_SIMULATION_TRAINING_POPUP_CANCEL_TEXT,
        })
    end
end



SimulationTrainingCtrl._InitAllPoolPoint2Num = HL.Method() << function(self)
    self.m_poolAllCardPoint2Num = {}
    self.m_curShowPoolName = self.m_system:GetValidCardPoolId()
    local hasPool, poolData = Tables.SimulationTrainingCardPoolTable:TryGetValue(self.m_curShowPoolName)
    if hasPool then
        for i = 1, poolData.list.Count do
            local card = poolData.list[i - 1]
            local cardName = card.enemyGroupId
            local cardNum = card.cardNum
            local hasCard, cardData = Tables.SimulationTrainingCardTable:TryGetValue(cardName)
            if hasCard then
                local cardPoint = cardData.cardPoint
                if self.m_poolAllCardPoint2Num[cardPoint] == nil then
                    self.m_poolAllCardPoint2Num[cardPoint] = 0
                end
                self.m_poolAllCardPoint2Num[cardPoint] = self.m_poolAllCardPoint2Num[cardPoint] + cardNum
            end
        end
    end
end



SimulationTrainingCtrl._InitTipsPopup = HL.Method() << function(self)
    self.view.tipsPopupNode.gameObject:SetActive(false)
    self.view.tipsPopupNode.tipsPopupCloseBtn.onClick:AddListener(function()
        self.view.tipsPopupNodeAnimationWrapper:PlayOutAnimation(function()
            self.view.tipsPopupNode.gameObject:SetActive(false)
            self.view.tipsPopupNode.selectableNaviGroup:ManuallyStopFocus()
        end)
    end)

    self.view.tipsBtn.onClick:AddListener(function()
        if self.view.tipsPopupNode.gameObject.activeSelf then
            self.view.tipsPopupNodeAnimationWrapper:PlayOutAnimation(function()
                self.view.tipsPopupNode.gameObject:SetActive(false)
                self.view.tipsPopupNode.selectableNaviGroup:ManuallyStopFocus()
            end)
        else
            self.view.tipsPopupNode.gameObject:SetActive(true)
            self.view.tipsPopupNodeAnimationWrapper:PlayInAnimation()
            self.view.tipsPopupNode.selectableNaviGroup:ManuallyFocus()
        end
    end)
end



SimulationTrainingCtrl._UpdateTipsPopupInfo = HL.Method() << function(self)
    if self.m_openDouble then
        self.view.buffTipsTxt.text = Language.LUA_SIMULATION_TRAINING_MID_TIP_DOUBLE
    end

    if self.m_curPointNumber == 10 then
        self.view.buffTipsTxt.text = Language.LUA_SIMULATION_TRAINING_MID_TIP_10_POINT
    end

    if self.m_curPointNumber >= PUNISH_POINT_22 then
        self.view.buffTipsTxt.text = Language.LUA_SIMULATION_TRAINING_PUNISHMENT_20_BTN_TEXT
        self.view.tipsPopupNode.tipsPopupTitleText.text = Language.LUA_SIMULATION_TRAINING_PUNISHMENT_20_POPUP_TITLE
        if self.m_unlimitedMode then
            self.view.tipsPopupNode.tipsPopupHintText.text = Language.LUA_SIMULATION_TRAINING_PUNISHMENT_20_POPUP_UNLIMITED_CONTENT
        else
            self.view.tipsPopupNode.tipsPopupHintText.text = Language.LUA_SIMULATION_TRAINING_PUNISHMENT_20_POPUP_CONTENT
        end
    elseif self.m_curPointNumber >= PUNISH_POINT_11 then
        self.view.tipsPopupNode.tipsPopupTitleText.text = Language.LUA_SIMULATION_TRAINING_PUNISHMENT_10_POPUP_TITLE
        if self.m_unlimitedMode then
            self.view.tipsPopupNode.tipsPopupHintText.text = Language.LUA_SIMULATION_TRAINING_PUNISHMENT_10_POPUP_UNLIMITED_CONTENT
        else
            self.view.tipsPopupNode.tipsPopupHintText.text = Language.LUA_SIMULATION_TRAINING_PUNISHMENT_10_POPUP_CONTENT
        end
        self.view.buffTipsTxt.text = Language.LUA_SIMULATION_TRAINING_PUNISHMENT_10_BTN_TEXT
    end
end




SimulationTrainingCtrl._InitTopMiddleTitle = HL.Method() << function(self)
    self.view.topMiddleTitleNode.topMiddleNumber1Txt.text = string.format(
        Language.LUA_SIMULATION_TRAINING_NUMBER_OF_GAMES_LEFT, self.m_system.dailyPlayCnt)
    self.view.topMiddleTitleNode.topMiddleNumber2Txt.text = string.format(
        Language.LUA_SIMULATION_TRAINING_NUMBER_OF_GAMES_LEFT, self.m_system.dailyPlayCnt)
end



SimulationTrainingCtrl._UpdateEnemyCells = HL.Method() << function(self)
    self.m_enemyIds = {}
    self.m_enemyLevels = {}

    for i = 1, self.m_system.curHandCards.Count do
        local cardName = self.m_system.curHandCards[i - 1]
        local hasCard, cardData = Tables.SimulationTrainingCardTable:TryGetValue(cardName)
        if hasCard then
            local enemyId = cardData.enemyIdList[0]
            local enemyLevel = cardData.enemyLevel[0]
            table.insert(self.m_enemyIds, enemyId)
            table.insert(self.m_enemyLevels, enemyLevel)
        end
    end

    local enemyAllCount = 0
    self.m_enemyCells:Refresh(5, function(cell, luaIndex)
        local csIndex = CSIndex(luaIndex)
        cell.gameObject.name = "SimulationTrainingCardCell_" .. luaIndex
        cell.infoButton.onClick:RemoveAllListeners()
        if luaIndex > self.m_system.curHandCards.Count then
            cell.stateController:SetState("Empty")
        else
            local cardName = self.m_system.curHandCards[csIndex]
            local hasCard, cardData = Tables.SimulationTrainingCardTable:TryGetValue(cardName)
            if hasCard then
                local cardPoint = cardData.cardPoint
                if cardData.enemyIdList.Count > 0 then
                    local enemyId = cardData.enemyIdList[0]
                    local enemyCount = cardData.enemyCountList[0]
                    enemyAllCount = enemyAllCount + enemyCount
                    if cardData.isBonusCard then
                        cell.stateController:SetState("Special")
                    else
                        cell.stateController:SetState("Normal")
                    end
                    local enemyInfo = UIUtils.getEnemyInfoByIdAndLevel(enemyId, cardData.enemyLevel[0])
                    cell.nameTxt.text = string.format(Language.LUA_SIMULATION_TRAINING_ENEMY_SHOW_NAME_AND_COUNT, enemyInfo.name, enemyCount)
                    cell.lvTxt.text = enemyInfo.level + self.m_system.deBuffMonsterLevel
                    cell.iconImg:LoadSprite(UIConst.UI_SPRITE_MONSTER_ICON_BIG, enemyInfo.templateId)
                    cell.numberImg:LoadSprite(IMG_FOLDER, PointNumberImg[cardPoint])
                    cell.numberImgVx:LoadSprite(IMG_FOLDER, PointNumberImg[cardPoint])
                    cell.numberImg.gameObject:SetActive(true)
                    cell.numberImgVx.gameObject:SetActive(false)
                    cell.pointsImg:LoadSprite(IMG_FOLDER, EnemyCellPointImg[cardPoint])
                    cell.infoButton.onClick:AddListener(function()
                        self:_OpenEnemyInfoPanel(luaIndex)
                    end)
                end
            end
            if luaIndex == self.m_system.curHandCards.Count and self.m_playDrawCardVxIng then
                cell.numberImg.gameObject:SetActive(false)
                cell.numberImgVx.gameObject:SetActive(true)
                cell.animationWrapper:PlayWithTween("simulationtraininglist_light",function()
                    cell.numberImg.gameObject:SetActive(true)
                    cell.numberImgVx.gameObject:SetActive(false)
                    self.m_playDrawCardVxIng = false
                end)
            end
        end
    end)
    self.m_system.enemyAllCount = enemyAllCount
end



SimulationTrainingCtrl._UpdateRemainCardCells = HL.Method() << function(self)
    self:_InitAllPoolPoint2Num()
    self.m_remainCardPoint2Num = {}

    for pointNum, remainNumber in pairs(self.m_poolAllCardPoint2Num) do
        self.m_remainCardPoint2Num[pointNum] = remainNumber
        local showInfo = {
            pointNum = pointNum,
            remainNumber = remainNumber,
        }
    end

    for i = 1, self.m_system.curHandCards.Count do
        local cardName = self.m_system.curHandCards[i - 1]
        local hasCard, cardData = Tables.SimulationTrainingCardTable:TryGetValue(cardName)
        if hasCard then
            local cardPoint = cardData.cardPoint
            if self.m_remainCardPoint2Num[cardPoint] ~= nil then
                self.m_remainCardPoint2Num[cardPoint] = self.m_remainCardPoint2Num[cardPoint] - 1
            end
        end
    end

    local showRemainTable = {}
    for pointNum, remainNumber in ipairs(self.m_remainCardPoint2Num) do
        if remainNumber >= 0 then
            local showInfo = {
                pointNum = pointNum,
                remainNumber = remainNumber,
            }
            table.insert(showRemainTable, showInfo)
        end
    end

    table.sort(showRemainTable, Utils.genSortFunction({ "pointNum" }, true))

    self.m_remainCardCells:Refresh(#showRemainTable, function(cell, luaIndex)
        local showInfo = showRemainTable[luaIndex]
        cell.remainCardIcon:LoadSprite(IMG_FOLDER, RemainCellPointImg[luaIndex])
        cell.remainCardTxt.text = showInfo.remainNumber
    end)
end



SimulationTrainingCtrl._UpdateOpenDoubleShow = HL.Method() << function(self)
    self:_CalRewardPointNumber()
    self:_UpdateRewardPointCells()
end



SimulationTrainingCtrl._CalRewardPointNumber = HL.Method() << function(self)
    self.m_lastPointNumber = self.m_curPointNumber
    local pointNumber = 0
    for i = 1, self.m_system.curHandCards.Count do
        local cardName = self.m_system.curHandCards[i - 1]
        local hasCard, cardData = Tables.SimulationTrainingCardTable:TryGetValue(cardName)
        if hasCard then
            local cardPoint = cardData.cardPoint
            pointNumber = pointNumber + cardPoint
        end
    end

    self.m_curPointNumber = pointNumber

    self.m_floorState = CS.Beyond.Gameplay.SimulationTrainingSystem.SimulationTrainingFloorState.Normal
    if self.m_curPointNumber >= PUNISH_POINT_22 then
        self.m_floorState = CS.Beyond.Gameplay.SimulationTrainingSystem.SimulationTrainingFloorState.Punish22
    elseif self.m_curPointNumber >= PUNISH_POINT_11 then
        self.m_floorState = CS.Beyond.Gameplay.SimulationTrainingSystem.SimulationTrainingFloorState.Punish11
    end

    if self.m_curPointNumber >= PUNISH_POINT_22 then
        self.m_system.deBuffMonsterLevel = Tables.simulationTrainingConst.secondDebuffMonsterLv
        self.m_system.deBuffTime = Tables.simulationTrainingConst.secondDebuffTime
    elseif self.m_curPointNumber >= PUNISH_POINT_11 then
        self.m_system.deBuffMonsterLevel = Tables.simulationTrainingConst.firstDebuffMonsterLv
        self.m_system.deBuffTime = Tables.simulationTrainingConst.firstDebuffTime
    end
end




SimulationTrainingCtrl._UpdateRewardPointCells = HL.Method() << function(self)
    local curLevel = self.m_system.curLevel

    local hasCfg, curLevelData = Tables.simulationTrainingLevelTable:TryGetValue(curLevel)
    if not hasCfg then
        logger.error("无等级配置数据", curLevel)
        return
    end
    self.m_luaIndex2rewardPointCell = {}
    self.m_luaIndex2rewardCellX = {}
    self.m_luaIndex2rewardNum = {}
    self.m_lastRewardPointLuaIndex = self.m_currentRewardPointLuaIndex
    self.m_rewardPointCells:Refresh(MAX_POINT_NUMBER + 1, function(cell, luaIndex)
        self.m_luaIndex2rewardPointCell[luaIndex] = cell
        local rewardNum = curLevelData.pointAward[luaIndex - 1]
        if self.m_openDouble then
            rewardNum = rewardNum + rewardNum
        end
        local pointNum = luaIndex - 1
        cell.pointNumberTxt.text = pointNum
        cell.rewardNumberTxt.text = rewardNum
        self.m_luaIndex2rewardNum[luaIndex] = rewardNum
        if pointNum == self.m_curPointNumber % 11 then
            GameInstance.player.simulationTrainingSystem.rewardScoreNumber = rewardNum
            self.m_currentRewardPointLuaIndex = luaIndex
            if not self.m_inited then
                cell.stateController:SetState("Select")
            end
        else
            cell.stateController:SetState("Normal")
        end

        if self.m_unlimitedMode then
            GameInstance.player.simulationTrainingSystem.rewardScoreNumber = 0
            cell.stateController:SetState("InfinityCell")
        else
            cell.stateController:SetState("StandardCell")
        end
    end)

    if self.m_inited and self.m_playRewardSlideVxIng then
        self:_StartTimer(0, function()
            for luaIndex, cell in ipairs(self.m_luaIndex2rewardPointCell) do
                self.m_luaIndex2rewardCellX[luaIndex] = cell.rectTransform.anchoredPosition.x + self.view.rewardLayoutNode.offsetMin.x
            end
            self:_UpdateRewardPointVx()
        end)
    end
end



SimulationTrainingCtrl._UpdateRewardPointVx = HL.Method() << function(self)
    if not self.m_inited then
        return
    end

    if not self.m_luaIndex2rewardPointCell then
        return
    end

    if self.m_unlimitedMode then
        self.view.rewardPointVxCell.stateController:SetState("InfinityCell")
    else
        self.view.rewardPointVxCell.stateController:SetState("StandardCell")
    end

    local lastCell = self.m_luaIndex2rewardPointCell[self.m_lastRewardPointLuaIndex]
    local currentCell = self.m_luaIndex2rewardPointCell[self.m_currentRewardPointLuaIndex]
    local startX = self.m_luaIndex2rewardCellX[self.m_lastRewardPointLuaIndex]
    local endX = self.m_luaIndex2rewardCellX[self.m_currentRewardPointLuaIndex]
    local posY = currentCell.rectTransform.anchoredPosition.y

    if startX < endX then
        AudioAdapter.PostEvent("Au_UI_Event_GamblePointUp")
    else
        AudioAdapter.PostEvent("Au_UI_Event_GamblePointDown")
    end

    local config = self.view.config
    self.m_tween = DOTween.To(function()
        self.view.rewardPointVxCell.gameObject:SetActive(true)
        lastCell.stateController:SetState("Normal")
        return startX
    end, function(value)
        self.view.rewardPointVxCell.rectTransform.anchoredPosition = Vector2(value, posY)

        local showLuaIndex = 1
        for luaIndex, x in ipairs(self.m_luaIndex2rewardCellX) do
            if value >= x then
                showLuaIndex = luaIndex
            end
        end

        self.view.rewardPointVxCell.pointNumberTxt.text = showLuaIndex - 1
        self.view.rewardPointVxCell.rewardNumberTxt.text = self.m_luaIndex2rewardNum[showLuaIndex]
    end, endX, 0.2):SetEase(config.SLIDER_TWEEN_CURV):OnComplete(function()
        currentCell.stateController:SetState("Select")
        self.view.rewardPointVxCell.gameObject:SetActive(false)
        if self.m_openSwitchInVsPost then
            self.m_openSwitchInVsPost = false
            if self.m_system.dailyDoubleCnt > 0 and self.m_system.curHandCards.Count > 0 and self.m_system.curHandCards.Count <= 2 then
                InputManagerInst:ForceBindingKeyhintToGray(self.view.commonToggleToggle.toggleBindingId, false)
                self.view.bottomNode:SetState(BottomState.DoubleSwitchActive)
            end
        end
        self.m_playRewardSlideVxIng = false
    end)
end





SimulationTrainingCtrl._OpenEnemyInfoPanel = HL.Method(HL.Number) << function(self, luaIndex)
    if #self.m_enemyIds == 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SIMULATION_TRAINING_NO_ENEMY_TOAST)
        return
    end

    UIManager:AutoOpen(
        PanelId.CommonEnemyPopup,
        {
            title = Language.LUA_SIMULATION_TRAINING_ENEMY_POPUP_TITLE,
            enemyListTitle = Language["ui_dungeon_enemy_popup_info_list"],
            enemyInfoTitle = Language["ui_dungeon_enemy_popup_info_desc"],
            enemyIds = self.m_enemyIds,
            enemyLevels = self.m_enemyLevels,
            initSelectEnemyLuaIndex = luaIndex,
            hideLevelTextNode = true,
        })

end



SimulationTrainingCtrl._UpdateMainState = HL.Method() << function(self)
    if self.m_unlimitedMode then
        self.view.panelStateController:SetState(PanelState.Infinity)
    else
        self.view.panelStateController:SetState(PanelState.Standard)
    end
    if self.m_openDouble then
        if self.m_curPointNumber == 10 then
            self.view.mainState:SetState(MainState.Point10)
            self.m_curMainState = MainState.Point10
        elseif self.m_curPointNumber >= PUNISH_POINT_11 then
            self.view.mainState:SetState(MainState.Overflow)
            self.m_curMainState = MainState.Overflow
        else
            self.view.mainState:SetState(MainState.Double)
            self.m_curMainState = MainState.Double
        end
    else
        if self.m_curPointNumber < 10 then
            self.view.mainState:SetState(MainState.Normal)
            self.m_curMainState = MainState.Normal
        elseif self.m_curPointNumber == 10 then
            self.view.mainState:SetState(MainState.Point10)
            self.m_curMainState = MainState.Point10

        elseif self.m_curPointNumber >= PUNISH_POINT_11 then
            self.view.mainState:SetState(MainState.Overflow)
            self.m_curMainState = MainState.Overflow
        else
            self.view.mainState:SetState(MainState.Normal)
            self.m_curMainState = MainState.Normal
        end
    end

    if self.m_lastMainState ~= self.m_curMainState then
        self.view.mainAnimationWrapper:ClearTween(true)
        self.view.mainAnimationWrapper:PlayWithTween("simulationtrainingcolor_in")
        self.m_lastMainState = self.m_curMainState
        if self.m_curMainState == MainState.Overflow then
            AudioAdapter.PostEvent("Au_UI_Event_GambleRed")
        elseif self.m_curMainState == MainState.Point10 then
            AudioAdapter.PostEvent("Au_UI_Event_GambleGreen")
        elseif self.m_curMainState == MainState.Double then
            AudioAdapter.PostEvent("Au_UI_Event_GambleYellow")
        end
    end

    self:_UpdateTipsPopupInfo()
end



SimulationTrainingCtrl._UpdateRightState = HL.Method() << function(self)
    if self.m_unlimitedMode then
        if self.m_system.curHandCards.Count < 5 then
            self.view.rightNode:SetState(RightState.Extractable)
            self.m_canDrawCard = true
        else
            self.view.rightNode:SetState(RightState.NoExtractable)
            self.m_canDrawCard = false
        end
    else
        if self.m_dailyPlayCnt == 0 and self.m_system.curHandCards.Count == 0 then
            self.view.rightNode:SetState(RightState.NoExtractable)
            self.m_canDrawCard = false
        else
            if self.m_system.curHandCards.Count < 5 then
                self.view.rightNode:SetState(RightState.Extractable)
                self.m_canDrawCard = true
            else
                self.view.rightNode:SetState(RightState.NoExtractable)
                self.m_canDrawCard = false
            end
        end
    end
end



SimulationTrainingCtrl._UpdateBottomState = HL.Method() << function(self)
    if self.m_system.curHandCards.Count == 0 then
        InputManagerInst:ForceBindingKeyhintToGray(self.view.commonToggleToggle.toggleBindingId, true)
        self.view.bottomNode:SetState(BottomState.DeActiveAllBtn)
    else
        InputManagerInst:ForceBindingKeyhintToGray(self.view.commonToggleToggle.toggleBindingId, false)
        self.view.bottomNode:SetState(BottomState.ActiveAllBtn)
    end

    if self.m_system.dailyDoubleCnt > 0 then
        if self.m_system.curHandCards.Count > 0 and self.m_system.curHandCards.Count <= 2 then
            if self.m_inited then
                self.m_openSwitchInVsPost = true
            else
                InputManagerInst:ForceBindingKeyhintToGray(self.view.commonToggleToggle.toggleBindingId, false)
                self.view.bottomNode:SetState(BottomState.DoubleSwitchActive)
            end
        else
            InputManagerInst:ForceBindingKeyhintToGray(self.view.commonToggleToggle.toggleBindingId, true)
            self.view.bottomNode:SetState(BottomState.DoubleSwitchDeActive)
        end
    else
        InputManagerInst:ForceBindingKeyhintToGray(self.view.commonToggleToggle.toggleBindingId, true)
        self.view.bottomNode:SetState(BottomState.DoubleSwitchDeActive)
    end
end




SimulationTrainingCtrl._HandleDrawCardVxPre = HL.Method() << function(self)
    self:_CalRewardPointNumber()
    self:_UpdateBottomState()
end



SimulationTrainingCtrl._HandleDrawCardVxPost = HL.Method() << function(self)
    self:_UpdateRewardPointCells()
    self:_UpdateEnemyCells()
    self:_UpdateRemainCardCells()
    self:_UpdateMainState()
    self:_UpdateRightState()
end

HL.Commit(SimulationTrainingCtrl)
