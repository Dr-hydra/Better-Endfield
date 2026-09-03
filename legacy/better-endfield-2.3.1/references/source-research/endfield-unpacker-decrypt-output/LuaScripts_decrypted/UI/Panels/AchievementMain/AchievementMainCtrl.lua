
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AchievementMain
local PHASE_ID = PhaseId.AchievementMain

AchievementMainCtrl = HL.Class('AchievementMainCtrl', uiCtrl.UICtrl)






AchievementMainCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACHIEVEMENT_DISPLAY_UPDATE] = '_OnDisplayUpdate',
}

AchievementMainCtrl.OpenAchievementMainPanel = HL.StaticMethod() << function()
    PhaseManager:OpenPhase(PHASE_ID)
end

AchievementMainCtrl.m_achievementCountInfo = HL.Field(HL.Any) << nil

AchievementMainCtrl.m_displayBundles = HL.Field(HL.Any) << nil

AchievementMainCtrl.m_editSwitch = HL.Field(HL.Any) << nil

local IRON_LEVEL = 1
local SILVER_LEVEL = 2
local GOLD_LEVEL = 3


AchievementMainCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local achievementListState = arg and arg.achievementListState or nil
    local achievementEditState = arg and arg.achievementEditState or nil
    local commonIntroState = arg and arg.commonIntroState or nil
    self:_InitViews()
    self:_LoadData()
    self:_RenderView()
    self:_TryRecoverAchievementList(achievementListState)
    self:_TryRecoverAchievementEdit(achievementEditState)
    self:_TryRecoverCommonIntro(commonIntroState)
end










AchievementMainCtrl._InitViews = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.helpBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_INTRO, "achievement")
    end)

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.redDot:InitRedDot("AchievementMain")

    self.view.detailBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.AchievementList)
    end)

    local switchBuilder = CS.Beyond.UI.UIAnimationSwitchTween.Builder()
    switchBuilder.animWrapper = self.view.mainAnimationWrapper
    switchBuilder.dontDisableGameObject = true
    self.m_editSwitch = switchBuilder:Build()
    self.m_editSwitch:Reset(false)

    self.view.editBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.AchievementEdit, {
            onClose = function()
                self.m_editSwitch.isShow = false
            end
        })
        self.m_editSwitch.isShow = true
    end)
end

AchievementMainCtrl._LoadData = HL.Method() << function(self)
    self.m_achievementCountInfo = {}
    self.m_displayBundles = {}
    local playerAchievements = GameInstance.player.achievementSystem.achievementData;
    local achievementTable = Tables.achievementTable
    for i, info in pairs(playerAchievements.achievementInfos) do
        local level = info.level
        if self.m_achievementCountInfo[level] == nil then
            self.m_achievementCountInfo[level] = 0
        end
        self.m_achievementCountInfo[level] = self.m_achievementCountInfo[level] + 1
    end
    for slotIndex, achievementId in pairs(playerAchievements.displayInfo) do
        if not string.isEmpty(achievementId) then
            local hasPlayer, playerAchievement = playerAchievements.achievementInfos:TryGetValue(achievementId)
            local hasData, achievementData = achievementTable:TryGetValue(achievementId)
            self.m_displayBundles[slotIndex] = {
                achievementId = achievementId,
                level = hasPlayer and playerAchievement.level or false,
                isPlated = hasPlayer and playerAchievement.isPlated or false,
                isRare = hasData and achievementData.applyRareEffect or false,
            }
        end
    end
end

AchievementMainCtrl._RenderView = HL.Method() << function(self)
    local ironCount = self.m_achievementCountInfo[IRON_LEVEL] == nil and 0 or self.m_achievementCountInfo[IRON_LEVEL]
    local silverCount = self.m_achievementCountInfo[SILVER_LEVEL] == nil and 0 or self.m_achievementCountInfo[SILVER_LEVEL]
    local goldCount = self.m_achievementCountInfo[GOLD_LEVEL] == nil and 0 or self.m_achievementCountInfo[GOLD_LEVEL]
    self.view.sumAllTxt.text = ironCount + silverCount + goldCount
    self.view.sumIronTxt.text = ironCount
    self.view.sumSilverTxt.text = silverCount
    self.view.sumGoldTxt.text = goldCount
    self.view.medalGroup:InitMedalGroup(self.m_displayBundles, self.view.config.MEDAL_DISPLAY_SLOT_COUNT)
end

AchievementMainCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return {
        achievementListState = self:_GetAchievementListRecoverState(),
        achievementEditState = self:_GetAchievementEditRecoverState(),
        commonIntroState = self:_GetCommonIntroRecoverState(),
    }
end

AchievementMainCtrl._GetAchievementListRecoverState = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local isOpen, ctrl = UIManager:IsOpen(PanelId.AchievementList)
    if not isOpen or not ctrl:IsShow() then
        return
    end
    return ctrl:GetRecoverStateArg()
end

AchievementMainCtrl._GetAchievementEditRecoverState = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local isOpen, ctrl = UIManager:IsOpen(PanelId.AchievementEdit)
    if not isOpen or not ctrl:IsShow() then
        return
    end
    return ctrl:GetRecoverStateArg()
end

AchievementMainCtrl._GetCommonIntroRecoverState = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local isOpen, ctrl = UIManager:IsOpen(PanelId.CommonIntro)
    if not isOpen or not ctrl:IsShow() then
        return
    end
    local recoverState = ctrl:GetRecoverStateArg()
    if recoverState == nil or recoverState.introId ~= "achievement" then
        return
    end
    return recoverState
end

AchievementMainCtrl._TryRecoverAchievementList = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    local isOpen = UIManager:IsOpen(PanelId.AchievementList)
    if isOpen then
        return
    end
    UIManager:Open(PanelId.AchievementList, recoverState)
end

AchievementMainCtrl._TryRecoverAchievementEdit = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    local isOpen = UIManager:IsOpen(PanelId.AchievementEdit)
    if isOpen then
        return
    end
    UIManager:Open(PanelId.AchievementEdit, {
        recoverState = recoverState,
        onClose = function()
            self.m_editSwitch.isShow = false
        end
    })
    self.m_editSwitch.isShow = true
end

AchievementMainCtrl._TryRecoverCommonIntro = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    local isOpen = UIManager:IsOpen(PanelId.CommonIntro)
    if isOpen then
        return
    end
    UIManager:Open(PanelId.CommonIntro, recoverState)
end

AchievementMainCtrl._OnDisplayUpdate = HL.Method() << function(self)
    self:_LoadData()
    self:_RenderView()
end

HL.Commit(AchievementMainCtrl)
