

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityCharSignCommon

ActivityCharSignCommonCtrl = HL.Class('ActivityCharSignCommonCtrl', uiCtrl.UICtrl)

ActivityCharSignCommonCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivityCharSignCommonCtrl.m_checkInPrefab = HL.Field(HL.Any)

ActivityCharSignCommonCtrl.m_checkInWidget = HL.Field(HL.Any)

ActivityCharSignCommonCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local suc,info = Tables.checkInInfoTable:TryGetValue(args.activityId)
    if suc then
        local path = string.format(UIConst.UI_ACTIVITY_CHECK_IN_PREFAB_PATH, info.commonPanelWidgetName)
        local prefab = self:LoadGameObject(path)
        if self.m_checkInPrefab then
            CSUtils.ClearUIComponents(self.m_checkInPrefab)
            GameObject.DestroyImmediate(self.m_checkInPrefab)
        end
        self.m_checkInPrefab = CSUtils.CreateObject(prefab, self.view.main)
        self.m_checkInWidget = Utils.wrapLuaNode(self.m_checkInPrefab)
    end


    local initArg = {
        activityId = args.activityId,
        isPopup = args.hasPopupSpec or false,
        replayScrollAnimOnShow = args.replayScrollAnimOnShow or false
    }
    self.m_checkInWidget:Init(initArg)
end

ActivityCharSignCommonCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)

    if self.m_checkInWidget then
        self.m_checkInWidget:OnPanelInputBlocked(active)
    end
end

ActivityCharSignCommonCtrl.PlayAnimationOut = HL.Override(HL.Opt(HL.Number)) << function(self, outCompleteActionType)
    outCompleteActionType = outCompleteActionType or UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close
    self.m_checkInWidget.view.animationWrapper:PlayOutAnimation(function()
        if outCompleteActionType == UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close then
            self:Close()
        elseif outCompleteActionType == UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Hide then
            self:Hide()
        end
    end)
end

ActivityCharSignCommonCtrl.OnActivityCenterNaviFailed = HL.Method() << function(self)
    self.m_checkInWidget:OnActivityCenterNaviFailed()
end

HL.Commit(ActivityCharSignCommonCtrl)
