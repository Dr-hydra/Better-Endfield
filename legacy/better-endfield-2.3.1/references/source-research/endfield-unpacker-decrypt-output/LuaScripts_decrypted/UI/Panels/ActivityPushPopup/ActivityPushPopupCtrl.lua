local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityPushPopup

ActivityPushPopupCtrl = HL.Class('ActivityPushPopupCtrl', uiCtrl.UICtrl)






ActivityPushPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SYSTEM_DISPLAY_SIZE_CHANGED] = '_OnScreenSizeChanged',
    [MessageConst.ON_SYSTEM_SCREEN_SIZE_CHANGED] = '_OnScreenSizeChanged',
}

ActivityPushPopupCtrl.m_pushIdList = HL.Field(HL.Table)
ActivityPushPopupCtrl.m_genCellCache = HL.Field(HL.Function)


ActivityPushPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitData(arg)
    self:_InitUI()
    self:_RefreshAllUIs()
    self:_UpdateBgm()
end

ActivityPushPopupCtrl._InitData = HL.Method(HL.Any) << function(self,arg)
    self.m_pushIdList = arg.pushIdList
end

ActivityPushPopupCtrl._InitUI = HL.Method() << function(self)
    self.view.btnClose.onClick:AddListener(function()
         self:PlayAnimationOutAndClose()
    end)
    self.view.activityScrollList.onUpdateCell:AddListener(function(go, csIndex)
        self:_OnUpdateActivityCell(go, csIndex)
    end)
    self.m_genCellCache = UIUtils.genCachedCellFunction(self.view.activityScrollList)

    UIManager:ToggleBlockObtainWaysJump("ActivityPushPopup", true, {})
end

ActivityPushPopupCtrl._RefreshAllUIs = HL.Method() << function(self)
     self.view.activityScrollList:UpdateCount(#self.m_pushIdList)
     self.view.activityScrollList.onScrollEnd:AddListener(function()
         self:_UpdateBgm()
     end)
     self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

ActivityPushPopupCtrl._OnUpdateActivityCell = HL.Method(HL.Any, HL.Number) << function(self, go, csIndex)
    local cell = self.m_genCellCache(go, csIndex)
    local index = LuaIndex(csIndex)
    local pushId = self.m_pushIdList[index]
    cell:InitActivityPushPopupInfo({pushId = pushId})
end

ActivityPushPopupCtrl._UpdateBgm = HL.Method() << function(self)
    local csIndex = math.floor(self.view.activityScrollList:GetCenterIndex())
    local index = LuaIndex(csIndex)
    local pushId = self.m_pushIdList[index]
    local _, pushCfg = Tables.activityPushPopupTable:TryGetValue(pushId)
    AudioManager.PostEvent(pushCfg.bgm)
end

ActivityPushPopupCtrl._OnScreenSizeChanged = HL.Method(HL.Opt(HL.Any, HL.Any)) << function(self, _, _)
    if not self.m_pushIdList or #self.m_pushIdList <= 0 then
        return
    end
    local csIndex = math.floor(self.view.activityScrollList:GetCenterIndex())
    if csIndex < 0 then
        csIndex = 0
    end
    self.view.activityScrollList:TryRecalculateSize()
    self.view.activityScrollList:UpdateCount(#self.m_pushIdList, csIndex, true, false, true)
end

ActivityPushPopupCtrl.OnClose = HL.Override() << function(self)
    if self.m_phase then
        self.m_phase:RemovePhasePanelItemById(PANEL_ID)
    end

    UIManager:ToggleBlockObtainWaysJump("ActivityPushPopup", false, {})
end

HL.Commit(ActivityPushPopupCtrl)
