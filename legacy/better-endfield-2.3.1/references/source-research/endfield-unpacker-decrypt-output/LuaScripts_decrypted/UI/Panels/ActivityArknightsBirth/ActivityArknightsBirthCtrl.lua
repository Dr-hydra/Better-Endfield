
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityArknightsBirth

ActivityArknightsBirthCtrl = HL.Class('ActivityArknightsBirthCtrl', uiCtrl.UICtrl)






ActivityArknightsBirthCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = 'OnStageChange',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_PROGRESS_CHANGE] = 'OnStageChange',
}

ActivityArknightsBirthCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityArknightsBirthCtrl.m_activityData = HL.Field(CS.Beyond.Gameplay.ActivityArknightsBirth)

ActivityArknightsBirthCtrl.m_stageInfoList = HL.Field(HL.Table)

ActivityArknightsBirthCtrl.m_prefabNode = HL.Field(HL.Any)

ActivityArknightsBirthCtrl.m_prefabName = HL.Field(HL.String) << ''

ActivityArknightsBirthCtrl.m_arknightsBirthCenter = HL.Field(HL.Any)

ActivityArknightsBirthCtrl.m_timeOffset = HL.Field(HL.Int) << 0

ActivityArknightsBirthCtrl.m_rewardItemIdList = HL.Field(HL.Table)

ActivityArknightsBirthCtrl.m_rewardBundles = HL.Field(HL.Table)

ActivityArknightsBirthCtrl.m_jumpIdList = HL.Field(HL.Table)

ActivityArknightsBirthCtrl.m_curStage = HL.Field(HL.Int) << 0

ActivityArknightsBirthCtrl.m_curStagePage = HL.Field(HL.Int) << 1

ActivityArknightsBirthCtrl.m_stampClickGen = HL.Field(HL.Int) << 0


ActivityArknightsBirthCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_activityId = arg.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(arg)
    self.m_activityData = GameInstance.player.activitySystem:GetActivity(arg.activityId)

    self:RefreshInfo()
    self:RefreshAchievement()
end

ActivityArknightsBirthCtrl.RefreshInfo = HL.Method() << function(self)
    self.m_rewardItemIdList = {}
    self.m_rewardBundles = {}
    self.m_jumpIdList = {}
    self.m_curStage = 0

    local suc, multiStageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(self.m_activityId)
    if suc then

        local curSortId = 0
        for stageId, stageCfg in pairs(multiStageCfg.stageList) do

            if not Tables.activityArknightsBirthMultiStageTable[stageId].isVisible then
                goto continue
            end

            table.insert(self.m_rewardItemIdList, Tables.activityArknightsBirthMultiStageTable[stageId].rewardItemId)
            table.insert(self.m_jumpIdList, Tables.activityArknightsBirthMultiStageTable[stageId].jumpId)

            local csConditionalStageInfo = self.m_activityData:GetStageData(stageId)
            if csConditionalStageInfo ~= nil then

                local openTimeStamp = Utils.getTimeIdOpenTimeStamp(stageCfg.timeId)
                if openTimeStamp > self.m_timeOffset then
                    self.m_timeOffset = openTimeStamp
                end
                local status = GEnums.ActivityConditionalStageState.__CastFrom(csConditionalStageInfo.Status)

                if status ~= GEnums.ActivityConditionalStageState.Locked then
                    self.m_curStage = self.m_curStage + 1
                    local centerPanelId = Tables.activityArknightsBirthMultiStageTable[stageId].centerPanelId
                    if self.m_prefabName == nil or self.m_prefabName == '' then
                        self.m_prefabName = centerPanelId
                        curSortId = stageCfg.sortId
                    elseif stageCfg.sortId > curSortId then
                        self.m_prefabName = centerPanelId
                        curSortId = stageCfg.sortId
                    end
                end
            end
            :: continue ::
        end


        local path = string.format(UIConst.UI_ACTIVITY_ARKNIGHTS_BIRTH_PREFAB_PATH, self.m_prefabName)
        local prefab = self:LoadGameObject(path)
        if self.m_prefabNode then
            CSUtils.ClearUIComponents(self.m_prefabNode)
            GameObject.DestroyImmediate(self.m_prefabNode)
        end
        local parent = self.view.content.rectTransform
        self.m_prefabNode = CSUtils.CreateObject(prefab, parent)
        self.m_arknightsBirthCenter = Utils.wrapLuaNode(self.m_prefabNode)


        self.m_curStagePage = self.m_curStage
        if self.m_curStagePage == 1 then
            self.view.content.arrowImage01.gameObject:SetActive(true)
            self.view.content.arrowImage02.gameObject:SetActive(false)
        else
            self.view.content.arrowImage02.gameObject:SetActive(true)
            self.view.content.arrowImage01.gameObject:SetActive(false)
        end


        if self.m_curStage == 1 then
            self.m_arknightsBirthCenter.content.activityTab1.leftNode.rightNode.stateController:SetState("NotUnlocked")
            local endTime = self.m_activityData.startTime + self.m_timeOffset * 3600
            local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
            if endTime > curTime then
                self.m_arknightsBirthCenter.content.activityTab1.leftNode.rightNode.countDownText:InitCountDownText(endTime, function()
                    self:RefreshInfo()
                end)
            end
        else
            self.m_arknightsBirthCenter.content.activityTab1.leftNode.rightNode.stateController:SetState("Unlocked")
        end


        local step = 0
        for stageId, stageCfg in pairs(multiStageCfg.stageList) do

            if not Tables.activityArknightsBirthMultiStageTable[stageId].isVisible then
                goto continue
            end
            local csConditionalStageInfo = self.m_activityData:GetStageData(stageId)
            if csConditionalStageInfo ~= nil then
                step = step + 1
                local status = GEnums.ActivityConditionalStageState.__CastFrom(csConditionalStageInfo.Status)
                if status == GEnums.ActivityConditionalStageState.Unlocked then
                    if step == 1 then
                        self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.emailNode:SetState("InProgress")
                    elseif step == 2 then
                        self.m_arknightsBirthCenter.content.activityTab2.rightNode.detailNode.emailNode:SetState("InProgress")
                    end
                end
                if status == GEnums.ActivityConditionalStageState.Completed then
                    if step == 1 then
                        self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.emailNode:SetState("Completed")
                        self.m_arknightsBirthCenter.content.activityTab1.leftNode.cardTab.titleNode.claimedImg.gameObject:SetActive(true)
                        self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.claimedImg1.gameObject:SetActive(true)
                        self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.claimedImg2.gameObject:SetActive(true)
                        if self.m_curStage == 2 then
                            self.m_arknightsBirthCenter.content.activityTab2.rightNode.leftNode.claimedImg.gameObject:SetActive(true)
                        end
                    elseif step == 2 then
                        self.m_arknightsBirthCenter.content.activityTab2.rightNode.detailNode.emailNode:SetState("Completed")
                        self.m_arknightsBirthCenter.content.activityTab1.leftNode.rightNode.claimedImg.gameObject:SetActive(true)
                        self.m_arknightsBirthCenter.content.activityTab2.rightNode.cardTab.titleNode.claimedImg.gameObject:SetActive(true)
                        self.m_arknightsBirthCenter.content.activityTab2.rightNode.detailNode.claimedImg.gameObject:SetActive(true)
                    end
                end
            end
            :: continue ::
        end


        if self.m_curStage == 1 then
            self.m_arknightsBirthCenter.content.activityTab1.leftNode.rightNode.notUnlocked.onClick:RemoveAllListeners()
            self.m_arknightsBirthCenter.content.activityTab1.leftNode.rightNode.notUnlocked.onClick:AddListener(function()
                Notify(MessageConst.SHOW_TOAST,Language.LUA_ACTIVITY_ARKNIGHTS_STAGE_LOCKED)
            end)
        elseif self.m_curStage == 2 then
        local animWrapper_1 = self.m_arknightsBirthCenter.content.activityTab1.gameObject:GetComponent("UIAnimationWrapper")
        local animWrapper_2 = self.m_arknightsBirthCenter.content.activityTab2.gameObject:GetComponent("UIAnimationWrapper")
        self.m_arknightsBirthCenter.content.activityTab1.leftNode.rightNode.unlocked.onClick:RemoveAllListeners()
        self.m_arknightsBirthCenter.content.activityTab1.leftNode.rightNode.unlocked.onClick:AddListener(function()

            animWrapper_1:PlayOutAnimation(function()

                self.view.content.arrowImage02.gameObject:SetActive(true)
                self.view.content.arrowImage01.gameObject:SetActive(false)

                self.m_arknightsBirthCenter.content.activityTab1.gameObject:SetActive(false)
                self.m_arknightsBirthCenter.content.activityTab2.gameObject:SetActive(true)
                animWrapper_2:PlayInAnimation()
                self.m_curStagePage = 2

                self:RefreshRedDot()
            end)
        end)
        self.m_arknightsBirthCenter.content.activityTab2.rightNode.leftNode.unlocked.onClick:RemoveAllListeners()
        self.m_arknightsBirthCenter.content.activityTab2.rightNode.leftNode.unlocked.onClick:AddListener(function()

            animWrapper_2:PlayOutAnimation(function()

                self.view.content.arrowImage01.gameObject:SetActive(true)
                self.view.content.arrowImage02.gameObject:SetActive(false)

                self.m_arknightsBirthCenter.content.activityTab2.gameObject:SetActive(false)
                self.m_arknightsBirthCenter.content.activityTab1.gameObject:SetActive(true)
                animWrapper_1:PlayInAnimation()
                self.m_curStagePage = 1

                self:RefreshRedDot()
            end)
        end)
    end


        self:RefreshRedDot()


        self.view.activityCommonInfoLuaReference.gotoNode.btnDetail.onClick:RemoveAllListeners()
        self.view.activityCommonInfoLuaReference.gotoNode.btnDetail.onClick:AddListener(function()
            CS.Beyond.SDK.SDKUtils.OpenHGWebPortalSDK(self.m_jumpIdList[self.m_curStagePage],"",nil)
            ClientDataManagerInst:SetBool(self.m_activityId .. "_stage_" .. self.m_curStagePage, true, false, EClientDataTimeValidType.Permanent)

            self:RefreshRedDot()
            Notify(MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE)
        end)


        self:RefreshReward()
    end
end

ActivityArknightsBirthCtrl.RefreshAchievement = HL.Method() << function(self)




































end

ActivityArknightsBirthCtrl.RefreshReward = HL.Method() << function(self)

    table.insert(self.m_rewardBundles, UIUtils.getRewardItems(self.m_rewardItemIdList[1]) or {})
    table.insert(self.m_rewardBundles, UIUtils.getRewardItems(self.m_rewardItemIdList[2]) or {})

    self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.stamp01.onClick:RemoveAllListeners()
    self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.stamp01.onClick:AddListener(function()
        local selectedBG1 = self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.selectedBG1
        local selectedBG2 = self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.selectedBG2
        self.m_stampClickGen = self.m_stampClickGen + 1
        local curGen = self.m_stampClickGen
        if not DeviceInfo.usingController then
            selectedBG1.gameObject:SetActive(true)
            selectedBG2.gameObject:SetActive(false)
        end
        local keyHint = self.m_arknightsBirthCenter.content.activityTab1.leftNode.keyHint
        keyHint.gameObject:SetActive(false)
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = self.m_rewardBundles[1][1].id,
            transform = self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.stamp01.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            isSideTips = true,
            onClose = function()
                if NotNull(selectedBG1) and curGen == self.m_stampClickGen then
                    selectedBG1.gameObject:SetActive(false)
                end
                if NotNull(keyHint) and curGen == self.m_stampClickGen then
                    keyHint.gameObject:SetActive(true)
                end
            end,
        })
    end)
    self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.stamp02.onClick:RemoveAllListeners()
    self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.stamp02.onClick:AddListener(function()
        local selectedBG1 = self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.selectedBG1
        local selectedBG2 = self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.selectedBG2
        self.m_stampClickGen = self.m_stampClickGen + 1
        local curGen = self.m_stampClickGen
        if not DeviceInfo.usingController then
            selectedBG2.gameObject:SetActive(true)
            selectedBG1.gameObject:SetActive(false)
        end
        local keyHint = self.m_arknightsBirthCenter.content.activityTab1.leftNode.keyHint
        keyHint.gameObject:SetActive(false)
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = self.m_rewardBundles[1][2].id,
            transform = self.m_arknightsBirthCenter.content.activityTab1.leftNode.detailNode.stamp01.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            isSideTips = true,
            onClose = function()
                if NotNull(selectedBG2) and curGen == self.m_stampClickGen then
                    selectedBG2.gameObject:SetActive(false)
                end
                if NotNull(keyHint) and curGen == self.m_stampClickGen then
                    keyHint.gameObject:SetActive(true)
                end
            end,
        })
    end)
    if self.m_curStage == 2 then
        self.m_arknightsBirthCenter.content.activityTab2.rightNode.detailNode.stamp01.onClick:RemoveAllListeners()
        self.m_arknightsBirthCenter.content.activityTab2.rightNode.detailNode.stamp01.onClick:AddListener(function()
            local selectedBG = self.m_arknightsBirthCenter.content.activityTab2.rightNode.detailNode.selectedBG
            self.m_stampClickGen = self.m_stampClickGen + 1
            local curGen = self.m_stampClickGen
            if not DeviceInfo.usingController then
                selectedBG.gameObject:SetActive(true)
            end
            local keyHint = self.m_arknightsBirthCenter.content.activityTab2.rightNode.keyHint
            keyHint.gameObject:SetActive(false)
            Notify(MessageConst.SHOW_ITEM_TIPS, {
                itemId = self.m_rewardBundles[2][1].id,
                transform = self.m_arknightsBirthCenter.content.activityTab2.rightNode.detailNode.stamp01.transform,
                posType = UIConst.UI_TIPS_POS_TYPE.RightTop,
                isSideTips = true,
                onClose = function()
                    if NotNull(selectedBG) and curGen == self.m_stampClickGen then
                        selectedBG.gameObject:SetActive(false)
                    end
                    if NotNull(keyHint) and curGen == self.m_stampClickGen then
                        keyHint.gameObject:SetActive(true)
                    end
                end,
            })
        end)
    end
end

ActivityArknightsBirthCtrl.RefreshRedDot = HL.Method() << function(self)
    self.view.activityCommonInfoLuaReference.gotoNode.btnDetailRedDot:InitRedDot("ActivityArknightsBirthStageButton", {
        activityId = self.m_activityId,
        curStagePage = self.m_curStagePage,
    })
    if self.m_curStage == 2 then
        self.m_arknightsBirthCenter.content.activityTab1.leftNode.rightNode.redDot:InitRedDot("ActivityArknightsBirthStage2", {
            activityId = self.m_activityId,
        })
        self.m_arknightsBirthCenter.content.activityTab2.rightNode.leftNode.redDot:InitRedDot("ActivityArknightsBirthStage1", {
            activityId = self.m_activityId,
        })
    end
end

ActivityArknightsBirthCtrl.OnStageChange = HL.Method(HL.Any) << function(self, args)
    local id = unpack(args)
    if id ~= self.m_activityId then
        return
    end
    self:RefreshInfo()
    self:RefreshAchievement()
end

HL.Commit(ActivityArknightsBirthCtrl)
