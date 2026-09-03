local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ActivityCommonInfo = HL.Class('ActivityCommonInfo', UIWidgetBase)

ActivityCommonInfo.m_tagCells = HL.Field(HL.Any)

ActivityCommonInfo.m_activityId = HL.Field(HL.String) << ""

ActivityCommonInfo.m_rewardCells = HL.Field(HL.Any)

ActivityCommonInfo.m_goToBtnDetailCallBack = HL.Field(HL.Any)

ActivityCommonInfo.m_jumpBtnCallBack = HL.Field(HL.Any)

ActivityCommonInfo.m_skipReceive = HL.Field(HL.Any)

ActivityCommonInfo.m_calendarCountdownCor = HL.Field(HL.Any)


ActivityCommonInfo._OnFirstTimeInit = HL.Override() << function(self)

end

ActivityCommonInfo.InitActivityCommonInfo = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()
    self.m_activityId = args.activityId
    self.m_jumpBtnCallBack = args.jumpBtnCallBack
    self.m_skipReceive = args.skipReceive
    local activitySystem = GameInstance.player.activitySystem

    local _, activityData = Tables.activityTable:TryGetValue(self.m_activityId)
    local activity = activitySystem:GetActivity(self.m_activityId)
    if not activityData or not activity then
        logger.error('Activity not found: %s', self.m_activityId)
        self.view.gameObject:SetActive(false)
        return
    end


    self.view.infoNode.txtName.text = activityData.name
    self.view.infoNode.detailsTxt:SetAndResolveTextStyle(activityData.desc)
    if not args.skipTimeCountDown then
        if activity.endTime == 0 then
            self.view.infoNode.countDownText.text = Language.LUA_ACTIVITY_PERMANENT_TEXT
        else
            self.view.infoNode.countDownWidget:InitCountDownText(activity.endTime, args.timeOnComplete, args.timeFormatFunc)
        end
    end


    local tagIds = activityData.tagIds
    self.m_tagCells = UIUtils.genCellCache(self.view.tagCell)
    self.m_tagCells:Refresh(#tagIds, function(cell, index)
        local csIndex = CSIndex(index)
        local _,tagInfo = Tables.activityTagTable:TryGetValue(tagIds[csIndex])
        cell.tagTxt.text = tagInfo.name
    end)


    self.m_rewardCells = UIUtils.genCellCache(self.view.rewardItem)
    if self.view.config.SHOW_REWARDS then
        self:UpdateRewardInfo()
    end


    self.view.infoNode.descriptionBtn.onClick:AddListener(function()
        ActivityUtils.GameEventLogActivityVisit(self.m_activityId, "descriptionButton", "visit_description")
        local instructionId = activityData.instructionId
        UIManager:Open(PanelId.InstructionBook, {
            id = instructionId,
            onClose = function()
                Notify(MessageConst.ON_TOGGLE_ACTIVITY_INSTRUCTION, {
                    isShown = false
                })
            end
        })
        Notify(MessageConst.ON_TOGGLE_ACTIVITY_INSTRUCTION, {
            instructionId = instructionId,
            isShown = true
        })
    end)


    local state
    if not self.view.config.SHOW_BUTTONS then
        state = "None"
    elseif not activity.isUnlocked then
        state = "Reminder"
    elseif activity.status == GEnums.ActivityStatus.IntroMission then
        state = "IntroMission"
    else
        state = "Detail"
    end
    self.view.gotoNode.stateController:SetState(state)
    if state == "Reminder" then
        self.view.gotoNode.reminderJumpBtn.onClick:AddListener(function()
            ActivityUtils.GameEventLogActivityVisit(self.m_activityId, "unlockReminderButton", "visit_unlock_reminder")
            UIManager:Open(PanelId.ActivityStartReminderPopup,{
                activityId = self.m_activityId,
            })
        end)
    elseif state == "Detail" then
        if activityData.detailJumpId then
            self.view.gotoNode.btnDetail.onClick:AddListener(function()
                if self.m_goToBtnDetailCallBack then
                    self.m_goToBtnDetailCallBack()
                end
                ActivityUtils.GameEventLogActivityVisit(self.m_activityId, "gotoActivityHudButton", "visit_activity")
                if self.m_jumpBtnCallBack then
                    self.m_jumpBtnCallBack()
                    return
                end
                local normalJump = Tables.systemJumpTable:TryGetValue(activityData.detailJumpId)

                if normalJump then
                    Utils.jumpToSystem(activityData.detailJumpId)
                else

                    local webJump, webJumpInfo = Tables.activityWebTable:TryGetValue(activityData.id)
                    if webJump then
                        if webJumpInfo.disableAudio then
                            CS.Beyond.Gameplay.Audio.Utils.AudioControlUtil.Webview.SetMute(true)
                            CS.Beyond.SDK.SDKUtils.OpenHGWebPortalSDK(webJumpInfo.jumpId,"","ON_ACTIVITY_WEB_UNMUTE")
                        else
                            CS.Beyond.SDK.SDKUtils.OpenHGWebPortalSDK(webJumpInfo.jumpId,"",nil)
                        end
                    end
                end
            end)
        end
    elseif state == "IntroMission" then
        self.view.gotoNode.btnIntroMissionlRedDot:InitRedDot("ActivityIntroMission", self.m_activityId)
        self.view.gotoNode.btnIntroMission.onClick:AddListener(function()
            ActivityUtils.GameEventLogActivityVisit(self.m_activityId, "IntroMissionButton", "visit_intro_mission")
            local success = Tables.systemJumpTable:TryGetValue(activityData.introMissionJumpId)
            if success then
                Utils.jumpToSystem(activityData.introMissionJumpId)
                ActivityUtils.setFalseIntroMissionActivity(self.m_activityId)
            else
                logger.error("no such jumpId")
            end
        end)
    end

    self:_RefreshUICalendarBtn(args)
    self:_RefreshUIMedal(args)


    if DeviceInfo.usingController then
        self.view.gotoNode.scrollViewRewards.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end


    self:RegisterMessage(MessageConst.ON_ACTIVITY_UPDATED, function(updateArgs)
        local id = unpack(updateArgs)
        if id == self.m_activityId and GameInstance.player.activitySystem:GetActivity(id) then
            self:_Refresh()
        end
    end)


    self:RegisterMessage(MessageConst.ON_ACTIVITY_WEB_UNMUTE, function(_)
        CS.Beyond.Gameplay.Audio.Utils.AudioControlUtil.Webview.SetMute(false)
    end)


    self:RegisterMessage(MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE, function(stageArgs)
        local id = unpack(stageArgs)
        if id == self.m_activityId then
            self:_RefreshUICalendarBtn(args)
        end
    end)
end


ActivityCommonInfo._RefreshUICalendarBtn = HL.Method(HL.Any) << function(self, args)
    local calendar = self.view.infoNode.calendarBtn
    if calendar == nil then
        return
    end

    if self.m_calendarCountdownCor then
        self:_ClearCoroutine(self.m_calendarCountdownCor)
        self.m_calendarCountdownCor = nil
    end

    local haveCfg, multiStageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(self.m_activityId)
    if not haveCfg then
        calendar.gameObject:SetActive(false)
        return
    end

    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if not activityData then
        calendar.gameObject:SetActive(false)
        return
    end

    if not args.showCalendar then
        calendar.gameObject:SetActive(false)
        return
    end

    calendar.gameObject:SetActive(true)

    local stages = {}
    for stageId, stageCfg in pairs(multiStageCfg.stageList) do
        table.insert(stages, { stageId = stageId, cfg = stageCfg })
    end
    table.sort(stages, function(a, b) return a.cfg.sortId < b.cfg.sortId end)

    if #stages >= 1 then
        calendar.stateNode01:SetState("Nrl")
    end

    if #stages >= 2 then
        local stage2 = stages[2]
        local _, stageData = activityData.stageDataDict:TryGetValue(stage2.stageId)
        local isLocked = stageData and
            GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status) == GEnums.ActivityConditionalStageState.Locked

        calendar.timeIcon.gameObject:SetActive(isLocked)
        if isLocked then
            calendar.stateNode02:SetState("Lock")
            local openTime = stageData.OpenTimeTs
            local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
            local leftSec = openTime - curTime
            if leftSec < 0 then leftSec = 0 end
            calendar.timeTxt.text = UIUtils.getLeftTime(leftSec)

            self.m_calendarCountdownCor = self:_StartCoroutine(function()
                while true do
                    coroutine.wait(1)
                    local remaining = openTime - DateTimeUtils.GetCurrentTimestampBySeconds()
                    if remaining < 0 then remaining = 0 end
                    calendar.timeTxt.text = UIUtils.getLeftTime(remaining)
                    if remaining <= 0 then
                        break
                    end
                end
            end)
        else
            calendar.stateNode02:SetState("Nrl")
            calendar.timeTxt.text = Language.LUA_ACTIVITY_COMMON_INFO_MULTI_STAGE_CALENDAR_STAGE_LOCK
        end
    end

    calendar.button.onClick:RemoveAllListeners()
    calendar.button.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, {
            id = args.calendarInstructionId,
        })
    end)
end


ActivityCommonInfo._RefreshUIMedal = HL.Method(HL.Any) << function(self, args)
    local medal = self.view.gotoNode.bgTitle02
    if medal == nil then
        return
    end

    local hasAchievement, achievementData = Tables.activityAchievementDataTable:TryGetValue(self.m_activityId)
    if not hasAchievement or not args.showMedal then
        medal.gameObject:SetActive(false)
        return
    end

    medal.gameObject:SetActive(true)
    medal.dungeonMedalCell:InitCommonMedalNode(achievementData.achievementId)
end

ActivityCommonInfo.m_rewardId = HL.Field(HL.String) << ""
ActivityCommonInfo.UpdateGoToBtnDetailCallBack = HL.Method(HL.Any) << function(self, callback)
    self.m_goToBtnDetailCallBack = callback
end

ActivityCommonInfo._Refresh = HL.Method() << function(self)

    self:UpdateRewardInfo(self.m_rewardId)
end

ActivityCommonInfo.UpdateRewardInfo = HL.Method(HL.Opt(HL.String)) << function(self, rewardId)
    local _, activityData = Tables.activityTable:TryGetValue(self.m_activityId)
    if not rewardId then
        rewardId = activityData.rewardId
    end

    if rewardId and not string.isEmpty(rewardId) then
        self.m_rewardId = rewardId
        local _, rewardTableData = Tables.rewardTable:TryGetValue(rewardId)
        local rewardBundles = UIUtils.getRewardItems(rewardId)
        self.m_rewardCells:Refresh(#rewardBundles, function(cell, index)
            cell:InitItem(rewardBundles[index], function()
                cell:ShowTips()
            end)
            cell:SetExtraInfo({
                tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                tipsPosTransform = self.view.scrollViewRewards,
                isSideTips = true,
                onBeforeJump = function()
                    if DeviceInfo.usingController then
                        self.view.gotoNode.scrollViewRewards:ManuallyStopFocus()
                    end
                end
            })



            local isVisible = rewardTableData and rewardTableData.itemBundleVisibleList and rewardTableData.itemBundleVisibleList[CSIndex(index)] or 0
            if isVisible == 1 then
                cell.view.countNode.gameObject:SetActive(true)
            else
                cell.view.countNode.gameObject:SetActive(false)
            end
        end)
    end


    if not self.m_skipReceive then
        local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
        local receiveAll = activity and activity.receiveAllReward and not self.view.config.HIDE_RECEIVE_ALL
        self.view.gotoNode.receiveAllNode.gameObject:SetActive(receiveAll)
        self.view.gotoNode.notReceiveAllNode.gameObject:SetActive(not receiveAll)
    end
end

ActivityCommonInfo.UpdateDescTxt = HL.Method(HL.String) << function(self, desc)
    self.view.infoNode.detailsTxt:SetAndResolveTextStyle(desc)
end


HL.Commit(ActivityCommonInfo)
return ActivityCommonInfo

