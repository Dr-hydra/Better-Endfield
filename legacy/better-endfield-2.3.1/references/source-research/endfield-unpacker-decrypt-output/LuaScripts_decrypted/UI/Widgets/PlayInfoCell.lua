local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

PlayInfoCell = HL.Class('PlayInfoCell', UIWidgetBase)

PlayInfoCell.m_birthdayTipsThread = HL.Field(HL.Thread)

local HAS_BIRTHDAY_TIPS_SHOWED = "HasBirthdayTipsShowed"


PlayInfoCell._OnFirstTimeInit = HL.Override() << function(self)

end

PlayInfoCell.InitPlayInfoCell = HL.Method() << function(self)
    self.view.redDot:InitRedDot("PlayInfoCell")

    self:RegisterMessage(MessageConst.ON_FRIEND_BUSINESS_INFO_CHANGE, function()
        self:RefreshAdventureInfo()
    end)


    self.view.adventureRewardEntryBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.AdventureReward)
    end)
    self:RefreshAdventureInfo()


    self.view.rightBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.AdventureReward)
    end)

    self:RefreshBirthdayTips()
end

PlayInfoCell.RefreshAdventureInfo = HL.Method() << function(self)

    self.view.commonPlayerHead:UpdateHideLevelTxt(true)
    self.view.commonPlayerHead:InitCommonPlayerHeadByRoleId(GameInstance.player.roleId, function()
        PhaseManager:OpenPhase(PhaseId.Friend)
    end)
    self.view.button.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.Friend)
    end)
    self.view.commonPlayerHead.view.descText.gameObject:SetActive(false)
    self.view.commonPlayerHead.view.levelTxt.gameObject:SetActive(false)


    local adventureData = GameInstance.player.adventure.adventureLevelData
    local fillAmount = adventureData.reachMaxLv and 1 or adventureData.relativeExp / adventureData.relativeLevelUpExp
    local progressTxt = adventureData.reachMaxLv and Language.LUA_ADVENTURE_MAX_LEVEL_DESC
        or string.format("%d/%d", adventureData.relativeExp,
        adventureData.relativeLevelUpExp)


    self.view.managerLevel.text = string.format(Language.LUA_ADVENTURE_LEVEL_FORMAT, adventureData.lv)
    self.view.managerName.text = GameInstance.player.playerInfoSystem.playerName
    self.view.managerNumber.text = string.format("UID:%s", GameInstance.player.playerInfoSystem.roleId)
    self.view.levelSlider.fillAmount = fillAmount
    self.view.progressTxt.text = progressTxt
end

PlayInfoCell.RefreshBirthdayTips = HL.Method() << function(self)
    if not RedDotManager:GetRedDotState("SetBirthday") then
        return
    end
    local _, hasShowed = ClientDataManagerInst:GetBool(HAS_BIRTHDAY_TIPS_SHOWED, false, false)
    if hasShowed then
        return
    end
    self.m_birthdayTipsThread = self:_ClearCoroutine(self.m_birthdayTipsThread)
    self.view.birthdayTipsNode.gameObject:SetActiveIfNecessary(true)
    self.m_birthdayTipsThread = self:_StartCoroutine(function()
        coroutine.wait(Tables.birthdayConst.setBirthdayTipsDuration)
        self.view.birthdayTipsNode.gameObject:SetActiveIfNecessary(false)
    end)
end

PlayInfoCell.OnHide = HL.Method() << function(self)
    self:_CleanBirthdayTips();
end

PlayInfoCell.OnDestroy = HL.Method() << function(self)
    self:_CleanBirthdayTips();
end

PlayInfoCell._CleanBirthdayTips = HL.Method() << function(self)
    if self.m_birthdayTipsThread ~= nil then
        ClientDataManagerInst:SetBool(HAS_BIRTHDAY_TIPS_SHOWED, true, false)
    end
    self.m_birthdayTipsThread = self:_ClearCoroutine(self.m_birthdayTipsThread)
end

HL.Commit(PlayInfoCell)
return PlayInfoCell

