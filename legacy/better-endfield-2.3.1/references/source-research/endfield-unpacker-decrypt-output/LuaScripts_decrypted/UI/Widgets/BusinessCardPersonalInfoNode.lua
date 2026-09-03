local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

BusinessCardPersonalInfoNode = HL.Class('BusinessCardPersonalInfoNode', UIWidgetBase)

BusinessCardPersonalInfoNode.m_id = HL.Field(HL.Number) << 0

BusinessCardPersonalInfoNode.m_preview = HL.Field(HL.Boolean) << false


BusinessCardPersonalInfoNode._OnFirstTimeInit = HL.Override() << function(self)
    self.view.btnOther.onClick:RemoveAllListeners()
    self.view.btnOther.onClick:AddListener(function()
        local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(self.m_id)
        local postfix = (success and not string.isEmpty(playerInfo.remakeName)) and "Remark" or "Name"
        local seasonTag = self.view["seasonTag" .. postfix]
        local levelTag = self.view["levelTag" .. postfix]
        local recordTipsTransform = seasonTag.gameObject.activeInHierarchy and seasonTag.transform
            or levelTag.gameObject.activeInHierarchy and levelTag.transform
            or self.view.tipRect
        if self.m_id == GameInstance.player.roleId then
            FriendUtils.FRIEND_CELL_INIT_FUNC.onSelfClick(self.view.tipRect, self.m_id, recordTipsTransform)
        elseif GameInstance.player.friendSystem.friendInfoDic:ContainsKey(self.m_id) then
            FriendUtils.FRIEND_CELL_INIT_FUNC.onBusinessCardFriendPlayerClick(self.view.tipRect, self.m_id, recordTipsTransform)
        else
            FriendUtils.FRIEND_CELL_INIT_FUNC.onBusinessCardStrangerPlayerClick(self.view.tipRect, self.m_id, recordTipsTransform)
        end
    end)

    self.view.playerUidTxtButton.onClick:RemoveAllListeners()
    self.view.playerUidTxtButton.onClick:AddListener(function()
        Unity.GUIUtility.systemCopyBuffer = self.view.playerUidTxt.text
        Notify(MessageConst.SHOW_TOAST, Language.LUA_COPY_UID_SUCCESS)
    end)

    local openRecordTips = function(transform)
        if self.m_id ~= 0 then
            UIManager:Open(PanelId.FriendBusinessRecordTips, { roleId = self.m_id, transform = transform, posType = UIConst.UI_TIPS_POS_TYPE.LeftAlignBottom })
        end
    end
    self.view.levelTagRemark.onClick:RemoveAllListeners()
    self.view.levelTagRemark.onClick:AddListener(function() openRecordTips(self.view.levelTagRemark.transform) end)
    self.view.levelTagName.onClick:RemoveAllListeners()
    self.view.levelTagName.onClick:AddListener(function() openRecordTips(self.view.levelTagName.transform) end)
    self.view.seasonTagRemark.onClick:RemoveAllListeners()
    self.view.seasonTagRemark.onClick:AddListener(function() openRecordTips(self.view.seasonTagRemark.transform) end)
    self.view.seasonTagName.onClick:RemoveAllListeners()
    self.view.seasonTagName.onClick:AddListener(function() openRecordTips(self.view.seasonTagName.transform) end)
end

BusinessCardPersonalInfoNode.InitBusinessCardPersonalInfoNodeByRoleId = HL.Method(HL.Number, HL.Boolean) << function(self, roleId, preview)
    self:_FirstTimeInit()

    self.m_preview = preview
    self.m_id = roleId
    if self.m_id == GameInstance.player.roleId then
        self.view.redDot:InitRedDot("NewAvatarInfo")
        self.view.moreInfoRedDot:InitRedDot("SelfBusinessCard")
    else
        self.view.redDot.gameObject:SetActiveIfNecessary(false)
        self.view.moreInfoRedDot.gameObject:SetActiveIfNecessary(false)
    end

    local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
    if not success then
        logger.error("获取玩家信息失败，roleId: " .. roleId)
        return
    end

    local click = not preview and function()

        PhaseManager:OpenPhase(PhaseId.FriendHeadSelectedPopUp)
    end or false

    self.view.commonPlayerHead:UpdateHideLevelTxt(true)
    self.view.commonPlayerHead:InitCommonPlayerHeadByRoleId(roleId, click)
    local postfix = string.isEmpty(playerInfo.remakeName) and "Name" or "Remark"
    local oppositePostfix = string.isEmpty(playerInfo.remakeName) and "Remark" or "Name"
    self:UpdateContingencyContractActivityState(postfix)
    self.view["levelTag" .. oppositePostfix].gameObject:SetActiveIfNecessary(false)
    self:UpdateSeasonTowerActivityState(postfix)
    self.view["seasonTag" .. oppositePostfix].gameObject:SetActiveIfNecessary(false)
    local showLineImage = self.view["levelTag" .. postfix].gameObject.activeSelf
        and self.view["seasonTag" .. postfix].gameObject.activeSelf
    self.view["lineImage" .. postfix].gameObject:SetActiveIfNecessary(showLineImage)
    self.view["lineImage" .. oppositePostfix].gameObject:SetActiveIfNecessary(false)


    local stateName = string.isEmpty(playerInfo.remakeName) and "NoRemarks" or "Remarks"
    local name = string.format(Language.LUA_FRIEND_NAME, playerInfo.name, playerInfo.shortId)
    self.view.nameTxt.text = name
    self.view.remarkTxt.text = playerInfo.remakeName

    if FriendUtils.isPsnPlatform() then
        stateName = stateName .. (string.isEmpty(playerInfo.psName) and "NoPsAccount" or "Ps")
        self.view.layoutName:SetState(stateName)
        self.view.psNameTxt.text = playerInfo.psName
    else
        self.view.psNameRoot.gameObject:SetActiveIfNecessary(false)
        self.view.psNameTxt.gameObject:SetActiveIfNecessary(false)
        if not string.isEmpty(playerInfo.remakeName) then
            self.view.layoutName:SetState(stateName)
        else
            self.view.layoutName:SetState(stateName)
        end
    end

    self.view.playerUidTxt.text = playerInfo.platformRoleId
    self:_RefreshBirthdayInfo(playerInfo)
end

BusinessCardPersonalInfoNode.UpdateContingencyContractActivityState = HL.Method(HL.String) << function(self, postfix)
    if self.m_id == 0 then
        self.view["levelTag" .. postfix].gameObject:SetActiveIfNecessary(false)
        return
    end

    local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(self.m_id)
    if success and playerInfo and playerInfo.contingencyContractBestRecord.Item1 ~= nil and playerInfo.contingencyContractBestRecord.Item2 > 0 then
        local activityData = GameInstance.player.activitySystem:GetActivity(playerInfo.contingencyContractBestRecord.Item1)
        if activityData == nil then
            self.view["levelTag" .. postfix].gameObject:SetActiveIfNecessary(false)
            return
        end
        local currentTime = DateTimeUtils.GetCurrentTimestampBySeconds()
        local isOpen = activityData.gameplayEndTime - currentTime > 0
        self.view["levelTag" .. postfix].gameObject:SetActiveIfNecessary(isOpen)
        self.view["activityLevelTxt" .. postfix].text = tostring(playerInfo.contingencyContractBestRecord.Item2)
        local rangeArray = Tables.activityContingencyContractTable:GetValue(playerInfo.contingencyContractBestRecord.Item1).rangeArray


        self.view["levelTagNode" .. postfix]:SetState(tostring(1))
        for i = 1, #rangeArray do
            local range = rangeArray[CSIndex(i)]
            if playerInfo.contingencyContractBestRecord.Item2 >= range then
                self.view["levelTagNode" .. postfix]:SetState(tostring(i + 1))
            end
        end
    else
        self.view["levelTag" .. postfix].gameObject:SetActiveIfNecessary(false)
    end
end

BusinessCardPersonalInfoNode.UpdateSeasonTowerActivityState = HL.Method(HL.String) << function(self, postfix)
    if self.m_id == 0 then
        self.view["seasonTag" .. postfix].gameObject:SetActiveIfNecessary(false)
        return
    end

    local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(self.m_id)
    local displayRecord = success and FriendUtils.getSeasonTowerDisplayRecord(playerInfo)
    local rank = displayRecord and displayRecord.rank or 0
    if displayRecord and FriendUtils.SEASON_TOWER_RANK_NAMES[rank] then
        self.view["seasonTag" .. postfix].gameObject:SetActiveIfNecessary(true)
        self.view["seasonTowerTagSmall" .. postfix]:SetState(FriendUtils.SEASON_TOWER_RANK_NAMES[rank])
    else
        self.view["seasonTag" .. postfix].gameObject:SetActiveIfNecessary(false)
    end
end

BusinessCardPersonalInfoNode.RefreshBirthdayInfo = HL.Method() << function(self)
    local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(self.m_id)
    if success then
        self:_RefreshBirthdayInfo(playerInfo)
    end
end

BusinessCardPersonalInfoNode._RefreshBirthdayInfo = HL.Method(HL.Any) << function(self, playerInfo)
    if GameInstance.player.friendSystem:IsBirthdayVisible(self.m_id) then
        self.view.timeText.text = string.format(Language.LUA_FRIEND_BIRTHDAY_FORMAT, playerInfo.monthOfBirthday, playerInfo.dayOfBirthday)
        self.view.stateController:SetState("Birthday")
    else
        self.view.timeText.text = os.date(Language.LUA_BUSINESS_CARD_TIME, playerInfo.createTime)
        self.view.stateController:SetState("JoiningDay")
    end

    if not self.m_preview and self.m_id == GameInstance.player.roleId and GameInstance.player.friendSystem:IsBirthdayAlreadySet() then
        self.view.switchBtn.interactable = true
        self.view.switchBtn.onClick:RemoveAllListeners()
        self.view.switchBtn.onClick:AddListener(function()
            GameInstance.player.friendSystem:SetBirthdayVisible(not GameInstance.player.friendSystem:IsBirthdayVisible(GameInstance.player.roleId))
        end)

        self.view.switchNode.gameObject:SetActiveIfNecessary(not DeviceInfo.usingController)
    else
        self.view.switchBtn.interactable = false
        self.view.switchNode.gameObject:SetActiveIfNecessary(false)
    end
end

HL.Commit(BusinessCardPersonalInfoNode)
return BusinessCardPersonalInfoNode
