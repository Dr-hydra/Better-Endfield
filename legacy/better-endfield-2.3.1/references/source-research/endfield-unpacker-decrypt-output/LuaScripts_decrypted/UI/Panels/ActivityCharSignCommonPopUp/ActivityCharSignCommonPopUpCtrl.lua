

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityCharSignCommonPopUp

ActivityCharSignCommonPopUpCtrl = HL.Class('ActivityCharSignCommonPopUpCtrl', uiCtrl.UICtrl)

ActivityCharSignCommonPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_UPDATED] = 'OnActivityUpdate',
}

ActivityCharSignCommonPopUpCtrl.m_checkInPrefab = HL.Field(HL.Any)

ActivityCharSignCommonPopUpCtrl.m_checkInWidget = HL.Field(HL.Any)

ActivityCharSignCommonPopUpCtrl.m_closeCallback = HL.Field(HL.Function)

ActivityCharSignCommonPopUpCtrl.m_activityId = HL.Field(HL.String) << ""

ActivityCharSignCommonPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_closeCallback = args.closeCallback or function()
        self:Close()
    end
    self.view.btnClose.onClick:AddListener(function()
        self:_Close()
    end)
    self.view.autoCloseButtonUp.onClick:AddListener(function()
        self:_Close()
    end)
    self.view.autoCloseButtonDown.onClick:AddListener(function()
        self:_Close()
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder( { self.view.inputGroup.groupId } )

    local suc,info = Tables.checkInInfoTable:TryGetValue(args.activityId)
    if suc then
        local path = string.format(UIConst.UI_ACTIVITY_CHECK_IN_PREFAB_PATH, info.popupPanelWidgetName)
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
        isPopup = true,
    }
    self.m_activityId = args.activityId
    self.m_checkInWidget.view.info:Init(initArg)
end

ActivityCharSignCommonPopUpCtrl.OnActivityUpdate = HL.Method(HL.Table) << function(self, args)
    local id = unpack(args)
    if id == self.m_activityId and not GameInstance.player.activitySystem:GetActivity(id) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
        self:_Close()
    end
end

ActivityCharSignCommonPopUpCtrl.m_waitingForClose = HL.Field(HL.Boolean) << false

ActivityCharSignCommonPopUpCtrl._Close = HL.Method() << function(self)
    if self.m_checkInWidget.view.info.view.animationWrapper.curState == UIConst.UI_ANIMATION_WRAPPER_STATE.Out then
        return
    end
    if not self:IsShow() then
        self.m_waitingForClose = true
        return
    end
    if UIManager:IsOpen(PanelId.RewardsPopUpForSystem) then
        UIManager:Close(PanelId.RewardsPopUpForSystem)
    end
    Notify(MessageConst.HIDE_ITEM_TIPS)
    self.view.btnClose.gameObject:SetActive(false)
    self.m_checkInWidget.view.info.view.animationWrapper:PlayOutAnimation(function()
        self.m_closeCallback()
    end)
end

ActivityCharSignCommonPopUpCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)

    if self.m_checkInWidget and self.m_checkInWidget.view and self.m_checkInWidget.view.info then
        self.m_checkInWidget.view.info:OnPanelInputBlocked(active)
    end
end

ActivityCharSignCommonPopUpCtrl.OnShow = HL.Override() << function(self)
    if self.m_waitingForClose then
        self.m_waitingForClose = false
        self:_Close()
    end
end

HL.Commit(ActivityCharSignCommonPopUpCtrl)
