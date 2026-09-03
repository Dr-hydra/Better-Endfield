local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CommonMedalNode = HL.Class('CommonMedalNode', UIWidgetBase)

CommonMedalNode.m_achievementData = HL.Field(HL.Any)

CommonMedalNode.m_level = HL.Field(HL.Number) << 0

CommonMedalNode.m_obtained = HL.Field(HL.Boolean) << false

CommonMedalNode.m_plated = HL.Field(HL.Boolean) << false

CommonMedalNode.m_maxLevel = HL.Field(HL.Number) << 0

CommonMedalNode.m_state = HL.Field(HL.String) << ''


CommonMedalNode._OnFirstTimeInit = HL.Override() << function(self)
    self:_InitViews()
end

CommonMedalNode.InitCommonMedalNode = HL.Method(HL.String) << function(self, achievementId)
    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Achievement) then
        self.view.stateCtrl:SetState("NotUnlocked")
        return
    end
    self:_FirstTimeInit()
    local succ, achievementData = Tables.achievementTable:TryGetValue(achievementId)
    if not succ then
        logger.error(string.format("%s not found in Tables.achievementTable, 请检查配置", achievementId))
    end
    self.m_achievementData = achievementData
    local isEmpty = string.isEmpty(achievementId) or not succ
    if isEmpty then
        self.view.stateCtrl:SetState("NotObtained")
        return
    end
    self:_LoadData(achievementId)
    self:_RenderViews()
end

CommonMedalNode._InitViews = HL.Method() << function(self)
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        self:_OnClick()
    end)

    self:RegisterMessage(MessageConst.ON_ACHIEVEMENT_UPDATE, function(args)
        if self.m_achievementData ~= nil and not string.isEmpty(self.m_achievementData.achieveId) then
            self:_LoadData(self.m_achievementData.achieveId)
            self:_RenderViews()
        end
    end)
end

CommonMedalNode._LoadData = HL.Method(HL.String) << function(self, achievementId)
    local achievementSystem = GameInstance.player.achievementSystem
    local succ, playerInfo = achievementSystem.achievementData.achievementInfos:TryGetValue(achievementId)
    self.m_level = succ and playerInfo.level or 0
    self.m_obtained = succ and playerInfo.level >= self.m_achievementData.initLevel
    self.m_plated = succ and playerInfo.isPlated
    self.m_maxLevel = self.m_achievementData.initLevel
    for i, levelInfo in pairs(self.m_achievementData.levelInfos) do
        self.m_maxLevel = math.max(self.m_maxLevel, levelInfo.achieveLevel)
    end
    self.m_state = "Obtained"
    if not self.m_obtained then
        self.m_state = "NotObtained"
    elseif self.m_achievementData.canBePlated and not self.m_plated then
        self.m_state = "CanPlated"
    elseif self.m_achievementData.canBePlated and self.m_plated then
        self.m_state = "Plated"
    elseif self.m_achievementData.canBeUpgraded and self.m_level < self.m_maxLevel then
        self.m_state = "CanUpgrade"
    elseif self.m_achievementData.canBeUpgraded and self.m_level >= self.m_maxLevel then
        self.m_state = "Upgraded"
    end
end

CommonMedalNode._RenderViews = HL.Method() << function(self)
    if self.m_obtained then
        local medalBundle = {
            achievementId = self.m_achievementData.achieveId,
            level = self.m_level,
            isPlated = self.m_plated,
            isRare = self.m_achievementData.applyRareEffect,
        }
        self.view.medal:InitMedal(medalBundle)
    end
    self.view.stateCtrl:SetState(self.m_state)
end

CommonMedalNode._OnClick = HL.Method() << function(self)
    if self.m_achievementData == nil then
        return
    end
    local achievementId = self.m_achievementData.achieveId
    if not string.isEmpty(achievementId) then
        Notify(MessageConst.SHOW_ACHIEVEMENT, achievementId)
    end
end

HL.Commit(CommonMedalNode)
return CommonMedalNode