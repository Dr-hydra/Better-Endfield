
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.PRTSInvestigateReport
local PHASE_ID = PhaseId.PRTSInvestigateReport
PRTSInvestigateReportCtrl = HL.Class('PRTSInvestigateReportCtrl', uiCtrl.UICtrl)






PRTSInvestigateReportCtrl.s_messages = HL.StaticField(HL.Table) << {

}



local ContentTypeEnum = {
    RichContent = 0,
    Radio = 1,
}

PRTSInvestigateReportCtrl.m_showSubmitAni = HL.Field(HL.Boolean) << false

PRTSInvestigateReportCtrl.m_storyCollId = HL.Field(HL.String) << ""

PRTSInvestigateReportCtrl.m_belongsInvestId = HL.Field(HL.String) << ""

PRTSInvestigateReportCtrl.m_info = HL.Field(HL.Table)

PRTSInvestigateReportCtrl.m_aniUpdateKey = HL.Field(HL.Number) << -1

PRTSInvestigateReportCtrl.m_aniCurPlayTime = HL.Field(HL.Number) << 0

PRTSInvestigateReportCtrl.m_isNewReport = HL.Field(HL.Boolean) << false




PRTSInvestigateReportCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitData(arg)
    self:_UpdateData()
    self:_InitUI()
    self:_ApplyResumeState(arg and arg.resumeState or nil)
    self:_RefreshAllUI()
end

PRTSInvestigateReportCtrl.OnClose = HL.Override() << function(self)
    self.m_aniUpdateKey = LuaUpdate:Remove(self.m_aniUpdateKey)
end

PRTSInvestigateReportCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return {
        investId = self.m_belongsInvestId,
        storyCollId = self.m_storyCollId,
        showSubmitAni = self.m_showSubmitAni,
        isNewReport = self.m_isNewReport,
        resumeState = {
            aniCurPlayTime = self.m_aniCurPlayTime,
        },
    }
end

PRTSInvestigateReportCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState then
        return
    end
    self.m_aniCurPlayTime = resumeState.aniCurPlayTime or self.m_aniCurPlayTime
end




PRTSInvestigateReportCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_belongsInvestId = arg.investId
    self.m_storyCollId = arg.storyCollId
    self.m_isNewReport = arg.isNewReport or false
    self.m_showSubmitAni = arg.isNewReport or self.m_isNewReport
end
PRTSInvestigateReportCtrl._UpdateData = HL.Method() << function(self)
    local itemCfg = Utils.tryGetTableCfg(Tables.prtsAllItem, self.m_storyCollId)
    self.m_info = {
        contentId = itemCfg.contentId,
        contentType = itemCfg.type == "multi_media" and ContentTypeEnum.Radio or ContentTypeEnum.RichContent,
        name = itemCfg.name
    }
end






PRTSInvestigateReportCtrl._InitUI = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        self:_OnClickCloseBtn()
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

PRTSInvestigateReportCtrl._RefreshAllUI = HL.Method() << function(self)
    local info = self.m_info
    if info.contentType == ContentTypeEnum.RichContent then
        self.view.prtsRichContent.gameObject:SetActiveIfNecessary(true)
        self.view.prtsRadio.gameObject:SetActiveIfNecessary(false)

        self.view.prtsRichContent:InitPRTSRichContent(info.contentId)
    else
        self.view.prtsRadio.gameObject:SetActiveIfNecessary(true)
        self.view.prtsRichContent.gameObject:SetActiveIfNecessary(false)

        self.view.prtsRadio:InitPRTSRadio(info.contentId, info.name)
    end

    local aniWrapper = self.animationWrapper
    if self.m_showSubmitAni then
        self:_PlaySubmitAni()
        aniWrapper:PlayWithTween("prtsinvestigaterport_in_part_0", function()
            aniWrapper:PlayWithTween("prtsinvestigaterport_in_part_1")
        end)
    else
        self:_ShowContent()
        aniWrapper:PlayWithTween("prtsstorycolldetailinvesnode_in_part_0", function()
            aniWrapper:PlayWithTween("prtsstorycolldetailinvesnode_in_part_1")
        end)
    end
end

PRTSInvestigateReportCtrl._PlaySubmitAni = HL.Method() << function(self)
    if self.m_aniUpdateKey > 0 then
        return
    end
    self.view.contentNode.gameObject:SetActiveIfNecessary(false)
    local aniTimeProgress = self.view.config.ANI_TIME_PROGRESS
    self.m_aniCurPlayTime = lume.clamp(self.m_aniCurPlayTime, 0, aniTimeProgress)
    self.view.progressBar.fillAmount = aniTimeProgress > 0 and self.m_aniCurPlayTime / aniTimeProgress or 1
    AudioManager.PostEvent("Au_UI_Event_PRTS_Processing")
    self.m_aniUpdateKey = LuaUpdate:Add("Tick", function(deltaTime)
        self:_OnTickSubmitAni(deltaTime)
    end)
end

PRTSInvestigateReportCtrl._ShowContent = HL.Method() << function(self)
    self.view.contentNode.gameObject:SetActiveIfNecessary(true)
    if self.m_info.contentType == ContentTypeEnum.Radio then
        self.view.prtsRadio:SetPlayRadio(true)
    end
end

PRTSInvestigateReportCtrl._OnTickSubmitAni = HL.Method(HL.Number) << function(self, deltaTime)
    local curTime = self.m_aniCurPlayTime + deltaTime
    self.m_aniCurPlayTime = curTime
    if curTime >= self.view.config.ANI_TIME_PROGRESS then
        self.m_aniUpdateKey = LuaUpdate:Remove(self.m_aniUpdateKey)
        self.view.progressBar.fillAmount = 1
        self.m_showSubmitAni = false
        local aniWrapper = self.animationWrapper
        self:_ShowContent()
        aniWrapper:PlayWithTween("prtsstorycolldetailinves_in")
        return
    end

    self.view.progressBar.fillAmount = curTime / self.view.config.ANI_TIME_PROGRESS
end

PRTSInvestigateReportCtrl._OnClickCloseBtn = HL.Method() << function(self)
    PhaseManager:PopPhase(PhaseId.PRTSInvestigateReport)

    if not self.m_isNewReport then
        return
    end

    local investCfg = Utils.tryGetTableCfg(Tables.prtsInvestigate, self.m_belongsInvestId)
    if investCfg then
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
            items = UIUtils.getRewardItems(investCfg.rewardId)
        })
    end
end



HL.Commit(PRTSInvestigateReportCtrl)
