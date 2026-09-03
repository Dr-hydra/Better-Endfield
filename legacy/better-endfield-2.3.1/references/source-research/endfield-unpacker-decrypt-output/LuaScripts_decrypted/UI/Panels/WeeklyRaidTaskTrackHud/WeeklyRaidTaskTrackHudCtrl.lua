local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeeklyRaidTaskTrackHud

local OPTIONAL_TEXT_COLOR = "C7EC59"

WeeklyRaidTaskTrackHudCtrl = HL.Class('WeeklyRaidTaskTrackHudCtrl', uiCtrl.UICtrl)

WeeklyRaidTaskTrackHudCtrl.m_game = HL.Field(HL.Userdata)

WeeklyRaidTaskTrackHudCtrl.m_genGoalCells = HL.Field(HL.Forward("UIListCache"))

WeeklyRaidTaskTrackHudCtrl.m_startCountDown = HL.Field(HL.Boolean) << false

WeeklyRaidTaskTrackHudCtrl.m_isFrozen = HL.Field(HL.Boolean) << false

WeeklyRaidTaskTrackHudCtrl.m_tickKey = HL.Field(HL.Any) << 0

WeeklyRaidTaskTrackHudCtrl.m_countDownTickKey = HL.Field(HL.Any) << 0

WeeklyRaidTaskTrackHudCtrl.m_countDown = HL.Field(HL.Number) << 0

WeeklyRaidTaskTrackHudCtrl.m_targetPercent = HL.Field(HL.Number) << 0

WeeklyRaidTaskTrackHudCtrl.m_animQueue = HL.Field(HL.Forward("Queue"))

WeeklyRaidTaskTrackHudCtrl.m_currentShowQuestId = HL.Field(HL.String) << ""

WeeklyRaidTaskTrackHudCtrl.m_objectStateTable = HL.Field(HL.Table)

WeeklyRaidTaskTrackHudCtrl.m_playAnimCount = HL.Field(HL.Number) << 0

WeeklyRaidTaskTrackHudCtrl.m_isPlayCountDown = HL.Field(HL.Boolean) << false

WeeklyRaidTaskTrackHudCtrl.m_playedAnimHashTable = HL.Field(HL.Table)


WeeklyRaidTaskTrackHudCtrl.m_lastDisplaySec = HL.Field(HL.Number) << -1

WeeklyRaidTaskTrackHudCtrl.m_freezeRefCount = HL.Field(HL.Number) << 0





WeeklyRaidTaskTrackHudCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_QUEST_OBJECTIVE_UPDATE] = 'UpdateWeekRaidTaskTrackHudObjectiveInfo',
    [MessageConst.ON_WEEK_RAID_DANGER_METER_MODIFY] = 'UpdateWeekRaidTaskTrackHudDangerInfo',
    [MessageConst.ON_WEEK_RAID_MISSION_REFRESH] = 'UpdateWeekRaidTaskTrackHudObjectiveInfo',
    [MessageConst.ON_WEEK_RAID_MISSION_PIN_CHANGE] = 'UpdateWeekRaidTaskTrackHudObjectiveInfo',
    [MessageConst.ON_QUEST_STATE_CHANGE] = 'OnQuestStateChange',
    [MessageConst.ON_OPEN_PROGRESS] = '_HideMission',
    [MessageConst.TRAVEL_POLE_ENTER_TRAVEL_MODE] = '_HideMission',
    [MessageConst.ON_CLOSE_PROGRESS] = '_ShowMission',
    [MessageConst.ON_EXIT_TRAVEL_MODE] = '_ShowMission',
    [MessageConst.ON_GAME_TIME_FREEZE_STATE_CHANGED] = 'OnFreezeWorld',
}


WeeklyRaidTaskTrackHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_game = GameInstance.player.weekRaidSystem.weekRaidGame
    self.m_genGoalCells = UIUtils.genCellCache(self.view.extraGoalCell)

    self.m_objectStateTable = {}
    self.m_playedAnimHashTable = {}

    self.m_animQueue = require_ex("Common/Utils/DataStructure/Queue")()

    self.view.countdownNode.gameObject:SetActive(false)
    self.view.targetPanel.gameObject:SetActive(WeeklyRaidUtils.IsInWeeklyRaid())
    self.view.NormalNodeBgImage.gameObject:SetActive(not WeeklyRaidUtils.IsInWeeklyRaidIntro())
    self.m_tickKey = self:_StartUpdate(function()

        self.m_game:UpdateTrackInfo()

        self:_UpdateDangerValue()


        if self.m_playAnimCount > 0 or self.m_animQueue:Count() == 0 then
            return
        end

        local animInfo = self.m_animQueue:Pop()

        if animInfo == nil or animInfo.anim == nil then
            return
        end

        if #animInfo.anim == 0 then
            if animInfo.onComplete ~= nil then
                animInfo.onComplete()
            end
            return
        end

        for _, v in ipairs(animInfo.anim) do
            local cell = self.m_genGoalCells:Get(v.index)
            if cell ~= nil then
                cell.animation:Play(v.name, function()
                    self.m_playAnimCount = self.m_playAnimCount - 1
                    if self.m_playAnimCount < 0 then
                        logger.error("WeeklyRaidTaskTrackHudCtrl.OnCreate: m_playAnimCount < 0")
                        self.m_playAnimCount = 0
                    end
                    if self.m_playAnimCount == 0 and animInfo.onComplete ~= nil then
                        animInfo.onComplete()
                    end
    end)
                self.m_playAnimCount = self.m_playAnimCount + 1
            end
        end
    end)

    self:UpdateWeekRaidTaskTrackHudDangerInfo(nil)
    self:UpdateWeekRaidTaskTrackHudObjectiveInfo(nil)
end
WeeklyRaidTaskTrackHudCtrl.UpdateWeekRaidTaskTrackHudDangerInfo = HL.Method(HL.Any) << function(self, args)

    local game = self.m_game

    self.view.normalNode:SetState(string.format('%d', game.DangerMeterLevel))
    self.m_targetPercent = game.DangerMeter / game.MaxDangerMeter

    if isNAN(self.m_targetPercent) then
        self.view.slider.value = 0
        self.view.scheduleNumberTxt.text = '0%'
    elseif self.m_targetPercent > self.view.slider.value then


        local sliderWidth = self.view.slider.transform.sizeDelta.x
        local deltaPercent = math.abs(self.m_targetPercent - self.view.slider.value)
        self.view.handleNode.sizeDelta = Vector2(deltaPercent * sliderWidth, self.view.handleNode.sizeDelta.y)

        local startX = self.view.slider.value * sliderWidth
        self.view.handleNode.anchoredPosition = Vector2(startX, self.view.handleNode.anchoredPosition.y)

        self.view.scheduleNumberTxt.text = string.format('%d/%d', game.DangerMeter, game.MaxDangerMeter)

        local needTip = self.m_targetPercent >= (Tables.weekRaidConst.weekRaidDangerTip / 100)
        self.view.warningImg.gameObject:SetActiveIfNecessary(needTip)
        if needTip and (self.view.slider.value < (Tables.weekRaidConst.weekRaidDangerTip / 100) or self.view.edgeEffects.curState == CS.Beyond.UI.UIConst.AnimationState.Stop) then

            self.view.edgeEffects.gameObject:SetActiveIfNecessary(true)
            self.view.edgeEffects:PlayInAnimation(function()
                self.view.edgeEffects:PlayLoopAnimation()
            end)
        end
    else
        self.view.slider.value = self.m_targetPercent
        self.view.handleNode.sizeDelta = Vector2(0, self.view.handleNode.sizeDelta.y)
        self.view.scheduleNumberTxt.text = string.format('%d/%d', game.DangerMeter, game.MaxDangerMeter)
        self.view.warningImg.gameObject:SetActiveIfNecessary(self.m_targetPercent >= (Tables.weekRaidConst.weekRaidDangerTip / 100))
        self.view.edgeEffects.gameObject:SetActiveIfNecessary(self.m_targetPercent >= (Tables.weekRaidConst.weekRaidDangerTip / 100))
    end
    if args ~= nil then
        local countDown = unpack(args)
        if countDown > 0 then
            if (not self.m_isPlayCountDown) then
                self.view.edgeEffects:Play("weeklyraidtask_warning_loop")
                self.m_isPlayCountDown = true
            end

            self:CountDownTime(countDown)
        end
    end
end

WeeklyRaidTaskTrackHudCtrl.UpdateWeekRaidTaskTrackHudObjectiveInfo = HL.Method(HL.Opt(HL.Any)) << function(self, args)

    local pinMissionId = GameInstance.player.weekRaidSystem.currentPinMission
    self.view.content.gameObject:SetActive(not string.isEmpty(pinMissionId))
    if string.isEmpty(pinMissionId) then

        return
    end
    local cfg = Tables.weekRaidDelegateTable[pinMissionId]

    if cfg.weekRaidMissionType == GEnums.WeekRaidMissionType.MainMission then
        local missionInfo = GameInstance.player.mission:GetMissionInfo(pinMissionId)
        self.view.titleText.text = missionInfo.missionName:GetText()
    else
    self.view.titleText.text = cfg.name
    end

    local questId = ""
    if cfg.weekRaidMissionType == GEnums.WeekRaidMissionType.MainMission then
        local list = GameInstance.player.mission:GetMissionData(cfg.missionId):GetProcessingQuests()
        if list.Count == 0 then
            logger.warn("WeeklyRaidTaskTrackHudCtrl.UpdateWeekRaidTaskTrackHudObjectiveInfo 失败！因为进行中任务数量不等于1！ pinMissionId : " .. pinMissionId)
            return
        end
        questId = list[0]
    else
        questId = cfg.questId
    end

    local questInfo = GameInstance.player.mission:GetQuestInfo(questId)

    local needCompleteIndexList = {}
    self.m_animQueue:Push({
            anim = needCompleteIndexList,
            onComplete = function()
                self.m_currentShowQuestId = questId
                self:_UpdateCells(self.m_currentShowQuestId ~= questId)
            end
        })








































































end

WeeklyRaidTaskTrackHudCtrl.OnQuestStateChange = HL.Method(HL.Any) << function(self, arg)




    local pinMissionId = GameInstance.player.weekRaidSystem.currentPinMission
    local questId, questState = unpack(arg)
    local missionId = GameInstance.player.mission:GetMissionIdByQuestId(questId)

    if pinMissionId ~= missionId then
        return
    end


    self:UpdateWeekRaidTaskTrackHudObjectiveInfo()
    end

WeeklyRaidTaskTrackHudCtrl._UpdateCells = HL.Method(HL.Boolean) << function(self, allUpdate)
    local questInfo = GameInstance.player.mission:GetQuestInfo(self.m_currentShowQuestId)
    local optional = questInfo.optional
    local questCompleted = GameInstance.player.mission:GetQuestState(self.m_currentShowQuestId) == CS.Beyond.Gameplay.MissionSystem.QuestState.Completed

    local needUpdateIndexList = {}
    self.m_genGoalCells:Refresh(questInfo.objectiveList.Count, function(objectiveCell, objectiveLuaIdx)
        local objective = questInfo.objectiveList[CSIndex(objectiveLuaIdx)]
        local type = objective.condition:GetType()
        local tableCfg = WeeklyRaidUtils.DelegateObjectiveConfig[type]
        if tableCfg ~= nil then
            local itemId = tableCfg.GetItemId(objective.condition)
            objectiveCell.desc:SetAndResolveTextStyle(WeeklyRaidUtils.DelegateObjectiveConfig[objective.condition:GetType()].GetObjectiveDesc(objective.condition, true))
            objectiveCell.desc.onClickLink:RemoveAllListeners()
            objectiveCell.desc.onClickLink:AddListener(function(linkId)
                Notify(MessageConst.SHOW_ITEM_TIPS, {
                    transform = objectiveCell.desc.transform,
                    itemId = itemId,
                })
            end)
        else
            if not objective.useStrDesc then
                if optional then
                    local tempText = string.format("<color=#%s>%s</color> %s", OPTIONAL_TEXT_COLOR, Language.ui_optional_quest, objective.runtimeDescription:GetText())
                    objectiveCell.desc:SetAndResolveTextStyle(tempText)
                else
                    objectiveCell.desc:SetAndResolveTextStyle(objective.runtimeDescription:GetText())
                end
            else
                objectiveCell.desc:SetAndResolveTextStyle(objective.descStr)
            end
        end

        if objective.isShowProgress then
            objectiveCell.progress.gameObject:SetActive(true)
            if questCompleted or objective.isCompleted then
                if tableCfg ~= nil then
                    local itemId = tableCfg.GetItemId(objective.condition)
                    local count = WeeklyRaidUtils.GetWeekRaidItemCount(itemId)
                    objectiveCell.progress.text = string.format("%d/%d", count, objective.progressToCompareForShow)
                else
                    objectiveCell.progress.text = string.format("%d/%d", objective.progressToCompareForShow, objective.progressToCompareForShow)
                end
            else
                objectiveCell.progress.text = string.format("%d/%d", objective.progressForShow, objective.progressToCompareForShow)
            end
        else
            objectiveCell.progress.gameObject:SetActive(false)
        end

        local hash = self:_GetObjectiveHashCode(objective)

        if allUpdate then
            table.insert(needUpdateIndexList, {
                index = objectiveLuaIdx,
                name = objective.isCompleted and "tasktrackhud_cellfinish" or "tasktrackhud_celldefault",
            })
        else
            local animName = objective.isCompleted and "tasktrackhud_cellfinish" or "tasktrackhud_celldefault"


            if self.m_objectStateTable[objectiveLuaIdx] ~= hash then
                table.insert(needUpdateIndexList, {
                    index = objectiveLuaIdx,
                    name = animName,
                })
            end
        end

        self.m_objectStateTable[objectiveLuaIdx] = self:_GetObjectiveHashCode(objective)







        objectiveCell.stateController:SetState(objective.isCompleted and 'Complete' or 'Normal')
    end)

    self.m_animQueue:Push({
        anim = needUpdateIndexList,
        onComplete = function()

            local allComplete = true
            for i = 1, #self.m_objectStateTable do
                local hash = self.m_objectStateTable[i]

                local isCompleted = (hash & 1) == 1
                if not isCompleted then
                    allComplete = false
                    break
                end
            end
            if allComplete then
                self.view.titleFinish.gameObject:SetActiveIfNecessary(true)

end


        end
    })
end

WeeklyRaidTaskTrackHudCtrl._UpdateDangerValue = HL.Method() << function(self)
    local currentPercent = self.view.slider.value
    if currentPercent >= self.m_targetPercent then
        return
    end

    if math.abs(currentPercent - self.m_targetPercent) < 0.001 then
        self.view.slider.value = self.m_targetPercent
        self.view.handleNode.sizeDelta = Vector2(0, self.view.handleNode.sizeDelta.y)
        return
    end
    local curve = self.view.config.DANGER_METER_CHANGE_CURVE
    local speed = curve:Evaluate(math.abs(currentPercent - self.m_targetPercent)) * self.view.config.DANGER_METER_CHANGE_SPEED
    currentPercent = math.min(currentPercent + speed * Time.deltaTime, self.m_targetPercent)





    self.view.slider.value = currentPercent

    local sliderWidth = self.view.slider.transform.sizeDelta.x
    local deltaPercent = math.abs(self.m_targetPercent - self.view.slider.value)
    self.view.handleNode.sizeDelta = Vector2(deltaPercent * sliderWidth, self.view.handleNode.sizeDelta.y)

    local startX = self.view.slider.value * sliderWidth
    self.view.handleNode.anchoredPosition = Vector2(startX, self.view.handleNode.anchoredPosition.y)
end

WeeklyRaidTaskTrackHudCtrl.CountDownTime = HL.Method(HL.Number) << function(self, countDown)


    self.m_countDown = countDown
    if self.m_startCountDown then
        return
    end
    self.m_startCountDown = true

    self.view.countdownNode.gameObject:SetActive(true)
    local initSec = math.floor(math.max(self.m_countDown - DateTimeUtils.GetCurrentTimestampBySeconds(), 0))
    self.m_lastDisplaySec = initSec
    self.view.countDownText.view.text.text = tostring(initSec)
    self.view.countdownNodeAnim.gameObject:SetActiveIfNecessary(true)
    self.view.countdownNodeAnimationWrapper:Play("weeklyraidtask_changenumb")
    self.view.countdownNodeAnimationWrapper:PlayOpenAudio()
    self.m_countDownTickKey = self:_StartUpdate(function()

        if self.m_isFrozen then
            return
        end
        local dispVal = math.floor(math.max(self.m_countDown - DateTimeUtils.GetCurrentTimestampBySeconds(), 0))
        if dispVal == self.m_lastDisplaySec then
            return
        end
        self.m_lastDisplaySec = dispVal
        self.view.countDownText.view.text.text = tostring(dispVal)
        if dispVal > 0 then
            self.view.countdownNodeAnim.gameObject:SetActiveIfNecessary(true)
            self.view.countdownNodeAnimationWrapper:Play("weeklyraidtask_changenumb")
            self.view.countdownNodeAnimationWrapper:PlayOpenAudio()
        end
    end)
end

WeeklyRaidTaskTrackHudCtrl._GetObjectiveHashCode = HL.Method(HL.Any).Return(HL.Number) << function(self, objective)



    local hash = objective.isCompleted and 1 << 63 or 0
    if objective.progressForShow < 0 or objective.progressForShow >= (1 << 31) then
        error("显示范围错误，必须是0至 " .. ((1 << 31) - 1) .. ")")
    end
    hash = hash | objective.progressForShow << 32
    hash = hash | (objective:GetHashCode() & 0xFFFFFFFF)

    return hash
end

WeeklyRaidTaskTrackHudCtrl.OnFreezeWorld = HL.Method(HL.Any) << function(self, args)
    local isFrozen, reason = unpack(args)
    if isFrozen then
        self.m_freezeRefCount = self.m_freezeRefCount + 1
    else
        self.m_freezeRefCount = math.max(self.m_freezeRefCount - 1, 0)
    end
    self.m_isFrozen = self.m_freezeRefCount > 0
end

WeeklyRaidTaskTrackHudCtrl.OnShow = HL.Override() << function(self)

    self.m_freezeRefCount = 0
    self.m_isFrozen = false
    if self.m_startCountDown then
        local dispVal = math.floor(math.max(self.m_countDown - DateTimeUtils.GetCurrentTimestampBySeconds(), 0))
        self.m_lastDisplaySec = dispVal
        self.view.countDownText.view.text.text = tostring(dispVal)
    end
end
WeeklyRaidTaskTrackHudCtrl.OnClose = HL.Override() << function(self)
    self:_RemoveUpdate(self.m_tickKey)
    self:_RemoveUpdate(self.m_countDownTickKey)
end

WeeklyRaidTaskTrackHudCtrl._HideMission = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    self.view.targetPanel.gameObject:SetActive(false)
end

WeeklyRaidTaskTrackHudCtrl._ShowMission = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    if not WeeklyRaidUtils.IsInWeeklyRaidIntro() then
        self.view.targetPanel.gameObject:SetActive(true)
    end
end




HL.Commit(WeeklyRaidTaskTrackHudCtrl)
