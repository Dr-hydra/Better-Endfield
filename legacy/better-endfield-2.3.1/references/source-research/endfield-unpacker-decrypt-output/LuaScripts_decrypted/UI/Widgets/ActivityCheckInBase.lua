local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ActivityCheckInBase = HL.Class('ActivityCheckInBase', UIWidgetBase)

local FIRST_CELL_POSITION_CAL_FINE_TUNE = 1

local stateTable = {
    NotComplete = 1,
    Complete = 2,
    Done = 3,
}



ActivityCheckInBase.m_animation = HL.Field(HL.Any)

ActivityCheckInBase.m_animNameList = HL.Field(HL.Any)

ActivityCheckInBase._InitAnim = HL.Method(HL.Table) << function(self, args)
    self.m_animation = args.animation
    self.m_animNameList = args.animNameList

    self.m_replayScrollAnimOnShow = args.replayScrollAnimOnShow or false
end

ActivityCheckInBase._PlayCarouselAnim = HL.Method() << function(self)
    local animName = self.m_animNameList[self.m_bigRewardIndex]
    self.m_animation:Play(animName)
end



ActivityCheckInBase.m_activityId = HL.Field(HL.String) << ""

ActivityCheckInBase.m_activity = HL.Field(HL.Any)

ActivityCheckInBase.m_totalDays = HL.Field(HL.Number) << -1

ActivityCheckInBase.m_rewards = HL.Field(HL.Userdata)

ActivityCheckInBase.m_getRewardCell = HL.Field(HL.Function)

ActivityCheckInBase.m_genCellFunc = HL.Field(HL.Function)

ActivityCheckInBase.m_isPopup = HL.Field(HL.Boolean) << false

ActivityCheckInBase.m_replayScrollAnimOnShow = HL.Field(HL.Boolean) << false

ActivityCheckInBase._InitActivityInfo = HL.Method(HL.Table) << function(self, args)

    self.m_isPopup = args.isPopup or false


    self:RegisterMessage(MessageConst.ON_ACTIVITY_CHECK_IN, function(arg)
        self:_OnActivityCheckIn(arg)
    end)

    self:RegisterMessage(MessageConst.ON_ACTIVITY_UPDATED, function(arg)
        local modifyId = unpack(arg)
        if modifyId == self.m_activityId then
            self:_OnActivityCheckIn(arg)
        end
    end)

    self:RegisterMessage(MessageConst.CHECK_IN_REWARD, function(arg)
        self:OnRewardInfo(arg)
    end)


    self.m_activityId = args.activityId
    self.m_Force2digits = Tables.checkInInfoTable[self.m_activityId].forceShowTwoDigits
    self.m_activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    self:_RefreshRewardDays()


    if self.view.descTxt then
        self.view.descTxt:SetAndResolveTextStyle(Tables.checkInInfoTable[self.m_activityId].checkinDes)
    end


    self.m_rewards = Tables.CheckInRewardTable[self.m_activityId].stageList
    self.m_totalDays = #self.m_rewards


    self.m_firstCanReceiveDay = self.m_activity.loginDays
    for index = 1,self.m_totalDays do
        if self:_GetState(index) == stateTable.Complete then
            self.m_firstCanReceiveDay = index
            break
        end
    end

    self.m_canGetReward = self.m_activity.loginDays ~= self.m_activity.rewardDays.Count
    if self.view.activityCommonInfo then
        self.view.activityCommonInfo:InitActivityCommonInfo(args)

        if self.m_isPopup then
            self.view.activityCommonInfo.view.infoNode.descriptionLayout.gameObject:SetActive(false)
        end
    end


    self.m_getRewardCell = UIUtils.genCachedCellFunction(self.m_scrollList)
    self.m_scrollList.onUpdateCell:RemoveAllListeners()
    self.m_scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getRewardCell(obj), LuaIndex(csIndex))
    end)
    self.m_scrollList:UpdateCount(self.m_totalDays)

    if self.m_isPopup then

        ActivityUtils.actionWhenActivityClosed(function()
            Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
            if args.closeCallback then
                args.closeCallback()
            end
        end, self, self.m_activityId)
    else

        ActivityUtils.backToMainHudWhenActivityClosed(self, self.m_activityId)
    end
end




ActivityCheckInBase.m_rewardCell = HL.Field(HL.Any)

ActivityCheckInBase.m_scrollList = HL.Field(HL.Any)

ActivityCheckInBase.m_scrollRect = HL.Field(HL.Any)

ActivityCheckInBase.m_scrollRectTransform = HL.Field(HL.Any)

ActivityCheckInBase.m_scrollNaviGroup = HL.Field(HL.Any)

ActivityCheckInBase.m_Force2digits = HL.Field(HL.Boolean) << false

ActivityCheckInBase._InitScrollList = HL.Method(HL.Table) << function(self, args)
    self.m_rewardCell = args.rewardCell
    self.m_scrollList = args.scrollList
    self.m_scrollRect = args.scrollList.gameObject:GetComponent("UIScrollRect")
    self.m_scrollNaviGroup = args.scrollList.gameObject:GetComponent("UISelectableNaviGroup")
    self.m_scrollRectTransform = args.scrollList.gameObject:GetComponent("RectTransform")
end




ActivityCheckInBase.m_listCells = HL.Field(HL.Any)

ActivityCheckInBase._InitTipPoints = HL.Method(HL.Table) << function(self, args)

    self.m_listCells = UIUtils.genCellCache(args.stateNode)
    self.m_listCells:Refresh(self.m_totalDays, function(cell, index)
        self:_RefreshDots(cell,index)
    end)
end




ActivityCheckInBase.m_receiveAllBtn = HL.Field(HL.Any)
ActivityCheckInBase._InitReceiveAll = HL.Method(HL.Table) << function(self,args)
    self.m_receiveAllBtn = args.receiveAllBtn
    self.m_receiveAllBtn.gameObject:SetActive(self.m_canGetReward)


    self.m_receiveAllBtn.onClick:AddListener(function()
        if self.m_canGetReward then
            self:_GainReward(self.m_allRewardDays)
        else
            Notify(MessageConst.SHOW_TOAST,Language.LUA_ACTIVITY_CHECK_IN_RECEIVE_FAIL)
        end
    end)
    if args.receiveRedDot then
        args.receiveRedDot:InitRedDot("ActivityCheckIn",self.m_activityId)
    end
end



ActivityCheckInBase.m_firstCanReceiveDay = HL.Field(HL.Number) << 1

ActivityCheckInBase._InitPosition = HL.Method() << function(self)

    local cellPixel = (self.m_rewardCell.gameObject:GetComponent("RectTransform").rect.width + self.m_scrollList.space.x)
    local leftPixel = (self.m_firstCanReceiveDay - 1) * cellPixel
    local totalPixel = self.m_scrollList:GetPadding().left + self.m_scrollList:GetPadding().right + cellPixel * self.m_totalDays - self.m_scrollList.space.x - self.m_scrollRectTransform.rect.width
    local normalizedPosition = leftPixel / totalPixel
    self.m_scrollRect.horizontalNormalizedPosition = lume.clamp(normalizedPosition, 0 ,1)
end



local SCROLL_ANIM_TIME = 0.3

ActivityCheckInBase.m_canFocus = HL.Field(HL.Boolean) << false

ActivityCheckInBase.m_focusBtn = HL.Field(HL.Any)

ActivityCheckInBase._InitFocus = HL.Method(HL.Table) << function(self,args)
    self.m_canFocus = true
    self.m_focusBtn = args.focusBtn


    self.m_focusBtn.onClick:AddListener(function()
        AudioManager.PostEvent("Au_UI_Hover_CheckInGrandPrize")

        local day = self.m_bigRewards[self.m_bigRewardIndex].day

        self:ClearNaviTarget()
        self.m_scrollList:ScrollToIndex(day)
        self.m_scrollList.onScrollEnd:RemoveAllListeners()
        self.m_scrollList.onScrollEnd:AddListener(function()

            self.m_scrollList.onScrollEnd:RemoveAllListeners()
            local cell = self:_GetCell(day)
            if cell then
                self:_SetNaviTarget(day)
                cell.animation:Play("reward_cell_focusremind")
            end
        end)
    end)
end



ActivityCheckInBase.m_focusIndex = HL.Field(HL.Number) << 0

ActivityCheckInBase.m_needDisableFirstCell = HL.Field(HL.Boolean) << true

ActivityCheckInBase.m_setNaviTargetIndex = HL.Field(HL.Number) << 0

ActivityCheckInBase._InitController = HL.Method(HL.Opt(HL.Table)) << function(self, args)
    if args and args.needDisableFirstCell then
        self.m_needDisableFirstCell = args.needDisableFirstCell
    end

    if DeviceInfo.usingController then
        if self.m_isPopup then

            if not self.luaPanel then
                self:GetLuaPanel()
            end
            self.m_setNaviTargetIndex = self.m_firstCanReceiveDay

            self.luaPanel.onAnimationInFinished:AddListener(function()
                self:_SetNaviTarget(self.m_setNaviTargetIndex)
            end)
            self.m_scrollList.onGraduallyShowFinish:AddListener(function()
                self:_SetNaviTarget(self.m_setNaviTargetIndex)
            end)
        else

            local viewBindingId = self:BindInputPlayerAction("common_view_item", function()
                self:OnActivityCenterNaviFailed()
            end)

            self.m_scrollNaviGroup.onIsTopLayerChanged:AddListener(function(active)
                if not active then
                    self.m_focusIndex = 0
                end
                InputManagerInst:ToggleBinding(viewBindingId, not active)
            end)
        end
    end
end

ActivityCheckInBase._SetNaviTarget = HL.Method(HL.Number) << function(self, index)
    if index == 0 or not DeviceInfo.usingController  then
        return
    end
    local cell = self:_GetCell(index)
    if not cell then
        self.m_scrollList:ScrollToIndex(index, true)
        cell = self:_GetCell(index)
    end
    if cell then
        local target = cell.button
        self:_ToggleCell(index, true)
        self:SetNaviTarget(target)
    else
        logger.error(string.format("SetNaviTarget Fail!Cell %d Not Found!", index))
    end
end

ActivityCheckInBase._OnEnable = HL.Override() << function(self)

    if self.m_replayScrollAnimOnShow then

        self.m_scrollList:UpdateCount(self.m_totalDays, true)
        self.m_setNaviTargetIndex = self.m_firstCanReceiveDay
    else
        if self.m_activity and DeviceInfo.usingController then
            self.m_scrollList:UpdateShowingCells(function(csIndex, obj)
                self:_OnUpdateCell(self.m_getRewardCell(obj), LuaIndex(csIndex))
            end)
            self.m_setNaviTargetIndex = self.m_focusIndex
        end
    end
end



ActivityCheckInBase.m_bigRewards = HL.Field(HL.Table)

ActivityCheckInBase.m_bigRewardIndex = HL.Field(HL.Number) << 0

ActivityCheckInBase.m_carouselCoroutine = HL.Field(HL.Any)

ActivityCheckInBase.m_dayTxt = HL.Field(HL.Any)

ActivityCheckInBase.m_nameTxt = HL.Field(HL.Any)

ActivityCheckInBase.m_tipsTxt = HL.Field(HL.Any)

ActivityCheckInBase._InitBigRewards = HL.Method(HL.Table) << function(self, args)

    self.m_dayTxt = args.dayTxt
    self.m_nameTxt = args.nameTxt
    self.m_tipsTxt = args.tipsTxt
    self.m_bigRewards = {}
    self.m_bigRewardIndex = 0
    local found = false
    for index = 1,self.m_totalDays do
        local csIndex = CSIndex(index)
        if self.m_rewards[csIndex].isKeyReward then
            table.insert(self.m_bigRewards,{
                day = index,
                reward = self.m_rewards[csIndex],
            })
            if not found and not self.m_getIfRewarded[index] then
                self.m_bigRewardIndex = #self.m_bigRewards - 1
                found = true
            end
        end
    end
    self:_LoadBigReward(true)
end

ActivityCheckInBase._InitBigRewardsCarousel = HL.Method(HL.Table) << function(self, args)

    args.leftBtn.onClick:AddListener(function()
        self:_RestartCarousel()
        self:_LoadBigReward(false)
    end)
    args.rightBtn.onClick:AddListener(function()
        self:_RestartCarousel()
        self:_LoadBigReward(true)
    end)


    self:_RestartCarousel()
end

ActivityCheckInBase._RestartCarousel = HL.Method() << function(self)
    if self.m_carouselCoroutine then
        self:_ClearCoroutine(self.m_carouselCoroutine)
    end
    self.m_carouselCoroutine = self:_StartCoroutine(function()
        while true do
            coroutine.wait(Tables.checkInInfoTable[self.m_activityId].checkInRewardChangeTime)
            self:_LoadBigReward(true)
        end
    end)
end

ActivityCheckInBase._LoadBigReward = HL.Method(HL.Boolean) << function(self, next)

    if next then
        self.m_bigRewardIndex = self.m_bigRewardIndex % #self.m_bigRewards + 1
    else
        self.m_bigRewardIndex = self.m_bigRewardIndex == 1 and #self.m_bigRewards or self.m_bigRewardIndex - 1
    end
    local index = self.m_bigRewardIndex
    local reward = self.m_bigRewards[index].reward


    self:_PlayCarouselAnim()


    self.m_dayTxt.text = self.m_bigRewards[index].day
    self.m_nameTxt.text = reward.rewardName


    local isChar = reward.charId ~= ""
    local canReceive = self:_GetState(self.m_bigRewards[index].day) ~= stateTable.Done
    if isChar and not canReceive then
        self.m_tipsTxt.text = Language.LUA_ACTIVITY_CHECK_IN_1_CHAR_RECEIVED
    elseif isChar and canReceive then
        self.m_tipsTxt.text = Language.LUA_ACTIVITY_CHECK_IN_1_CHAR_RECEIVE
    elseif not isChar and not canReceive then
        self.m_tipsTxt.text = Language.LUA_ACTIVITY_CHECK_IN_1_RECEIVED
    elseif not isChar and canReceive then
        self.m_tipsTxt.text = Language.LUA_ACTIVITY_CHECK_IN_1_RECEIVE
    end

    if self.m_canSearch then

        if reward.charId ~= "" then
            self.m_searchBtn.gameObject:SetActive(true)
            self.m_searchInfo = {
                isChar = true,
                charId = reward.charId,
            }
        elseif reward.weaponId ~= "" then
            self.m_searchBtn.gameObject:SetActive(true)
            self.m_searchInfo = {
                isChar = false,
                weaponId = reward.weaponId,
            }
        else
            self.m_searchBtn.gameObject:SetActive(false)
        end
    end
end




ActivityCheckInBase.m_getIfRewarded = HL.Field(HL.Table)

ActivityCheckInBase.m_allRewardDays = HL.Field(HL.Table)

ActivityCheckInBase.m_canGetReward = HL.Field(HL.Boolean) << false

ActivityCheckInBase._RefreshDots = HL.Method(HL.Any,HL.Number) << function(self,cell,index)

    local color
    if self.m_getIfRewarded[index] then
        color = "Black"
    elseif index <= self.m_activity.loginDays then
        color = "Yellow"
    elseif self.m_rewards[CSIndex(index)].isKeyReward then
        color = "Pink"
    else
        color = "White"
    end


    local circle
    if self.m_rewards[CSIndex(index)].isKeyReward and self.m_getIfRewarded[index] then
        circle = "BigRewardBlack"
    elseif self.m_rewards[CSIndex(index)].isKeyReward then
        circle = "BigRewardWhite"
    else
        circle = "NoBigReward"
    end


    local today
    if index == self.m_activity.loginDays then
        today = "Today"
    else
        today = "NotToday"
    end

    cell.stateController:SetState(color)
    cell.stateController:SetState(circle)
    cell.stateController:SetState(today)
end

ActivityCheckInBase._GetState = HL.Method(HL.Number).Return(HL.Number) << function(self,index)
    local state = stateTable.NotComplete
    if self.m_getIfRewarded[index] then
        state =  stateTable.Done
    elseif index <= self.m_activity.loginDays then
        state =  stateTable.Complete
    end
    return state
end




ActivityCheckInBase.m_canSearch = HL.Field(HL.Boolean) << false

ActivityCheckInBase.m_searchInfo = HL.Field(HL.Table)

ActivityCheckInBase.m_searchBtn = HL.Field(HL.Any)

ActivityCheckInBase._InitSearch = HL.Method(HL.Table) << function(self, args)
    self.m_canSearch = true
    self.m_searchBtn = args.searchBtn
    self.m_searchBtn.onClick:AddListener(function()
        self:_Search()
    end)
end

ActivityCheckInBase._Search = HL.Method() << function(self)

    local info = self.m_searchInfo
    if info.isChar then
        CharInfoUtils.openCharInfoBestWay({
            initCharInfoCreator = function(initCharTemplateId)
                local previewCharInfo = GameInstance.player.charBag:CreateClientInitialGachaPoolChar(info.charId)
                local perfectCharInfo = GameInstance.player.charBag:CreateClientPerfectGachaPoolCharInfo(info.charId)
                return {
                    instId = previewCharInfo.instId,
                    templateId = previewCharInfo.templateId,
                    charInstIdList = { previewCharInfo.instId  },
                    maxCharInstIdList = { perfectCharInfo.instId },
                    isShowPreview = true,
                }
            end,
            onClose = function()
                GameInstance.player.charBag:ClearAllClientCharAndItemData()
            end,
        },nil,true)
    else
        WikiUtils.showWeaponPreview({ weaponId = info.weaponId })
    end
end




ActivityCheckInBase.OnRewardInfo = HL.Method(HL.Table) << function(self, args)
    local rewardPack = unpack(args)
    local reward = {
        items = rewardPack.itemBundleList,
        chars = rewardPack.chars,
        onComplete = function()
            if DeviceInfo.usingController then
                self:_SetNaviTarget(self.m_focusIndex)
            end
        end
    }
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, reward)
end

ActivityCheckInBase._OnActivityCheckIn = HL.Method(HL.Any) << function(self, args)

    self:_RefreshRewardDays()
    self.m_scrollList:UpdateShowingCells(function(csIndex, obj)
        self:_OnUpdateCell(self.m_getRewardCell(obj), LuaIndex(csIndex))
    end)


    if self.m_receiveAllBtn then
        self.m_canGetReward = self.m_activity.loginDays ~= self.m_activity.rewardDays.Count
        self.m_receiveAllBtn.gameObject:SetActive(self.m_canGetReward)
    end


    if self.m_listCells then
        self.m_listCells:Refresh(self.m_totalDays, function(cell, index)
            self:_RefreshDots(cell,index)
        end)
    end


    if DeviceInfo.usingController and self.m_focusIndex > 0 then
        self:_SetNaviTarget(self.m_focusIndex)
    end
end



ActivityCheckInBase._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)

    local state = self:_GetState(index)

    if Tables.CheckInRewardTable[self.m_activityId].stageList[CSIndex(index)].dateImg ~= "" then

        local path = UIConst.UI_SPRITE_CHECK_IN_DATE_IMAGE_PATH
        local name = Tables.CheckInRewardTable[self.m_activityId].stageList[CSIndex(index)].dateImg
        cell.nameImage:LoadSprite(path, name)
    else

        if self.m_Force2digits then
            cell.levelTxt.text = string.format("%02d", index)
        else
            cell.levelTxt.text = index
        end
    end
    cell.gameObject.name = "Cell" .. tostring(index)


    local rewardId = self.m_rewards[CSIndex(index)].rewardId
    local rewardBundles = UIUtils.getRewardItems(rewardId)
    cell.rewardCellCache = cell.rewardCellCache or UIUtils.genCellCache(cell.itemBlack)
    cell.rewardCellCache:Refresh(#rewardBundles, function(innerCell, innerIndex)
        local reward = {
            id = rewardBundles[innerIndex].id,
            count = rewardBundles[innerIndex].count,
            forceHidePotentialStar = true,
        }
        innerCell:InitItem(reward, function()
            if not DeviceInfo.usingController and state == stateTable.Complete then
                self:_GainReward({index})
            else
                innerCell:ShowTips()
            end
        end)
        innerCell:SetExtraInfo({
            tipsPosTransform = innerCell.view.content,
            isSideTips = true,
        })
        innerCell.view.stateController:SetState(state == stateTable.Done and "Done" or "Nrl")
    end)


    local isKey = self.m_rewards[CSIndex(index)].isKeyReward
    if state == stateTable.NotComplete then
        cell.stateController:SetState(isKey and "Senior" or "Nrl")
    elseif state == stateTable.Complete then
        cell.stateController:SetState(isKey and "SeniorReceive" or "NrlReceive")
    elseif state == stateTable.Done then
        cell.stateController:SetState(isKey and "SeniorDone" or "NrlDone")
    end
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        if state == stateTable.Complete then
            AudioManager.PostEvent("Au_UI_Event_CheckInPanel_Receive")
            self:_GainReward({index})
        else
            AudioManager.PostEvent("Au_UI_Button_Common")
        end
    end)
    cell.redDot:InitRedDot("ActivityCheckInReward",state == "Receive")


    if DeviceInfo.usingController then
        cell.button:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.None)
        cell.getRewardKeyHint.gameObject:SetActive(false)


        self:_ToggleCell(index, false)
        cell.detailKeyHint.gameObject:SetActive(false)
        cell.button.onIsNaviTargetChanged = function(isTarget)
            self:_ToggleCell(index, isTarget)
        end
    else
        cell.inputBindingGroupMonoTarget.enabled = true
    end
end

ActivityCheckInBase._GainReward = HL.Method(HL.Table) << function(self, rewardDays)
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if activity then
        activity:GainReward(rewardDays)
    else

        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
    end
end

ActivityCheckInBase._GetCell = HL.Method(HL.Number).Return(HL.Any) << function(self, index)
    local oriCell = self.m_scrollList:Get(CSIndex(index))
    return oriCell and self.m_getRewardCell(oriCell)
end

ActivityCheckInBase._ToggleCell = HL.Method(HL.Number, HL.Boolean) << function(self, index, active)
    local cell = self:_GetCell(index)
    if cell then
        InputManagerInst:ToggleGroup(cell.inputBindingGroupMonoTarget.groupId, active)
        InputManagerInst:ToggleBinding(cell.button.onClick.bindingId, active and self:_GetState(index) == stateTable.Complete)
        cell.detailKeyHint.gameObject:SetActive(active)
        cell.getRewardKeyHint.gameObject:SetActive(active)
        if active then
            self.m_focusIndex = index
        end
    end
end




ActivityCheckInBase.OnPanelInputBlocked = HL.Method(HL.Boolean) << function(self, active)



    if not DeviceInfo.usingController then
        return
    end
    if self.m_focusIndex == 0 then
        return
    end
    local cell = self:_GetCell(self.m_focusIndex)
    if not cell then
        return
    end
    cell.getRewardKeyHint.gameObject:SetActive(active)
end


ActivityCheckInBase._RefreshRewardDays = HL.Method() << function(self)
    self.m_getIfRewarded = {}
    for i = 1,self.m_activity.rewardDays.Count do
        local rewardDay = self.m_activity.rewardDays[CSIndex(i)]
        self.m_getIfRewarded[rewardDay] = true
    end
    self.m_allRewardDays = {}
    for day = 1,self.m_activity.loginDays do
        if not self.m_getIfRewarded[day] then
            table.insert(self.m_allRewardDays,day)
        end
    end
end

ActivityCheckInBase.OnActivityCenterNaviFailed = HL.Virtual() << function(self)
    if self.view.container then
        local left = math.floor((- self.view.container.anchoredPosition.x - FIRST_CELL_POSITION_CAL_FINE_TUNE)/(self.m_rewardCell.gameObject:GetComponent("RectTransform").rect.width + self.m_scrollList.space.x)) + 1
        self:_SetNaviTarget(LuaIndex(left))
    end
end



HL.Commit(ActivityCheckInBase)
return ActivityCheckInBase