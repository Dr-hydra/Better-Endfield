local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityWEBRecall

ActivityWEBRecallCtrl = HL.Class('ActivityWEBRecallCtrl', uiCtrl.UICtrl)

ActivityWEBRecallCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_PLATFORM_INFO_INVITE_BACK_CODE] = '_OnInviteBackCodeReceived',
    [MessageConst.ON_CLOSE_WEB_APPLICATION] = '_OnWebClosed',
}

ActivityWEBRecallCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityWEBRecallCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    local redDotName = ActivityUtils.getActivityRedDotName(self.m_activityId) or "ActivityWEB"
    self.view.activityCommonInfoLuaReference.gotoNode.btnDetailRedDot:InitRedDot(redDotName, args.activityId)

    ActivityUtils.queryActivityWebPortalState(self.m_activityId)

    local _, activityCfg = Tables.activityTable:TryGetValue(args.activityId)
    if activityCfg and self.view.bgImg and not string.isEmpty(activityCfg.bgImg) then
        self.view.bgImg:LoadSprite(UIConst.UI_SPRITE_ACTIVITY, activityCfg.bgImg)
    end

    self.view.copyBtn.onClick:RemoveAllListeners()
    self.view.copyBtn.onClick:AddListener(function()
        local code = self.view.copyTxt.text
        if string.isEmpty(code) then
            return
        end
        Unity.GUIUtility.systemCopyBuffer = code
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SHARE_COPY_TIP)
    end)


    local cachedCode = GameInstance.player.activitySystem.inviteBackCode
    self.view.copyTxt.text = cachedCode or ""
    if not cachedCode or string.isEmpty(cachedCode) then

        GameInstance.player.activitySystem:SendQueryInviteBackCode(self.m_activityId)
    end
    self.view.activityCommonInfo.view.gotoNode.btnDetail.onClick:AddListener(function()
        ActivityUtils.setWebActivityFirstVisitRead(self.m_activityId)
    end)
end

ActivityWEBRecallCtrl._OnWebClosed = HL.Method(HL.Any) << function(self, _)
    ActivityUtils.queryActivityWebPortalState(self.m_activityId)
end

ActivityWEBRecallCtrl._OnInviteBackCodeReceived = HL.Method(HL.Any) << function(self, args)
    local code = unpack(args)
    if code then
        self.view.copyTxt.text = code
    end
end


HL.Commit(ActivityWEBRecallCtrl)
