
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.SNS
PhaseSNS = HL.Class('PhaseSNS', phaseBase.PhaseBase)

local NeedClosePanelIds = {
    PanelId.FacMarkerManagePopup,
}

local SNS_BASIC_PANEL_ID = PanelId.SNSBasic
local SNS_BARKER_PANEL_ID = PanelId.SNSBarker
local SNS_MISSION_PANEL_ID = PanelId.SNSMission
local SNS_FRIEND_PANEL_ID = PanelId.SNSFriend

PhaseSNS.m_basicPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseSNS.m_curPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseSNS.m_panelId2Item = HL.Field(HL.Table)





PhaseSNS.s_messages = HL.StaticField(HL.Table) << {
}

PhaseSNS.s_prePanelId = HL.StaticField(HL.Number) << -1


PhaseSNS._OnInit = HL.Override() << function(self)
   PhaseSNS.Super._OnInit(self)
end



PhaseSNS._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
   self.m_panelId2Item = {}

   local defaultPanelId
   if self.arg and self.arg.currentPanelId then
      defaultPanelId = self.arg.currentPanelId
   elseif not self.arg then
      defaultPanelId = PhaseSNS.s_prePanelId > 0 and PhaseSNS.s_prePanelId or SNS_BARKER_PANEL_ID
   elseif not string.isEmpty(self.arg.dialogId) then
      local dialogId = self.arg.dialogId
      local succ, dialogCfg = Tables.sNSDialogTable:TryGetValue(dialogId)
      local isMissionRelated = succ and not string.isEmpty(dialogCfg.relatedMissionId)
      if isMissionRelated then
         defaultPanelId = SNS_MISSION_PANEL_ID
      else
         defaultPanelId = SNS_BARKER_PANEL_ID
      end

      if not succ then
         logger.error("[sns] openPhaseSNS with invalid param snsDialogId:", self.arg.dialogId)
         self.arg.dialogId = nil
      end
   elseif not string.isEmpty(self.arg.roleId) then
       defaultPanelId = SNS_FRIEND_PANEL_ID
   else
      logger.error("invalid open phase params")
   end

   self.m_basicPanelItem = self:CreatePhasePanelItem(SNS_BASIC_PANEL_ID, {
      defaultPanelId = defaultPanelId,
      tabIndex = self.arg and self.arg.tabIndex or nil,
   })
   local defaultPanelArg = self.arg and self.arg.currentPanelArg ~= nil and self.arg.currentPanelArg or self.arg
   local defaultPanelItem = self:CreatePhasePanelItem(defaultPanelId, defaultPanelArg)
   self.m_panelId2Item[defaultPanelId] = defaultPanelItem
   self.m_curPanelItem = defaultPanelItem
   local uiCtrl = defaultPanelItem.uiCtrl
   if HL.TryGet(uiCtrl, "OnSwitchOn") then
      uiCtrl:OnSwitchOn(true)
   end

    PhaseSNS.s_prePanelId = defaultPanelId
   self:_BindControllerHintPlaceHolder()
end

PhaseSNS._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    for _, panelId in pairs(NeedClosePanelIds) do
        if UIManager:IsOpen(panelId) then
            UIManager:Close(panelId)
        end
    end
end

PhaseSNS._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseSNS._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseSNS._OnActivated = HL.Override() << function(self)
end

PhaseSNS._OnDeActivated = HL.Override() << function(self)
end

PhaseSNS._OnDestroy = HL.Override() << function(self)
   PhaseSNS.Super._OnDestroy(self)
end

PhaseSNS._OnRefresh = HL.Override() << function(self)
    local isFriendSChat = false
    for panelId, item in pairs(self.m_panel2Item) do
        if self.m_curPanelItem == item and panelId == SNS_FRIEND_PANEL_ID then
            isFriendSChat = true
        end
    end

    if isFriendSChat then
        self.m_curPanelItem.uiCtrl:OnClickOpenFriendChat({self.arg.roleId})
    end









   logger.warn("PhaseSNS._OnRefresh fail")
end

PhaseSNS.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
   local arg = self.arg and lume.deepCopy(self.arg) or {}

   if self.m_basicPanelItem and HL.TryGet(self.m_basicPanelItem.uiCtrl, "GetRecoverStateArg") then
      local basicPanelArg = self.m_basicPanelItem.uiCtrl:GetRecoverStateArg()
      if basicPanelArg then
         arg.tabIndex = basicPanelArg.tabIndex
         arg.currentPanelId = basicPanelArg.panelId
      end
   end

   if self.m_curPanelItem and self.m_curPanelItem.uiCtrl then
      arg.currentPanelId = self.m_curPanelItem.uiCtrl.panelId
      if HL.TryGet(self.m_curPanelItem.uiCtrl, "GetRecoverStateArg") then
         local currentPanelArg = self.m_curPanelItem.uiCtrl:GetRecoverStateArg()
         arg.currentPanelArg = currentPanelArg and lume.deepCopy(currentPanelArg) or nil
      else
         arg.currentPanelArg = nil
      end
   end

   return arg
end



PhaseSNS.OnTabChange = HL.Method(HL.Table) << function(self, args)
   local panelId = args.panelId
   if not panelId then
      return
   end

   local prePanelItem = self.m_curPanelItem
   if prePanelItem then
      prePanelItem.uiCtrl:Hide()
      local preUICtrl = prePanelItem.uiCtrl
      if HL.TryGet(preUICtrl, "OnSwitchOn") then
         preUICtrl:OnSwitchOn(false)
      end
   end

   local panelItem
    local needContinue = self.m_panel2Item[panelId] ~= nil
   if self.m_panelId2Item[panelId] then
      panelItem = self.m_panelId2Item[panelId]
      panelItem.uiCtrl:Show()
   else
      panelItem = self:CreatePhasePanelItem(panelId)
      self.m_panelId2Item[panelId] = panelItem
   end

   local uiCtrl = panelItem.uiCtrl
   if needContinue and HL.TryGet(uiCtrl, "TryContinueDialog") then
      uiCtrl:TryContinueDialog()
   end

   if HL.TryGet(uiCtrl, "OnSwitchOn") then
      uiCtrl:OnSwitchOn(true)
   end

   self.m_curPanelItem = panelItem
   PhaseSNS.s_prePanelId = panelId

   self:_BindControllerHintPlaceHolder()
end

PhaseSNS._BindControllerHintPlaceHolder = HL.Method() << function(self)
   if not DeviceInfo.usingController then
      return
   end

   if not self.m_basicPanelItem or not self.m_curPanelItem then
      return
   end


   local basicCtrl = self.m_basicPanelItem.uiCtrl
   local curCtrl = self.m_curPanelItem.uiCtrl
   if curCtrl then
      curCtrl.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ basicCtrl.view.inputGroup.groupId,
                                                                             curCtrl.view.inputGroup.groupId, })
   end
end



PhaseSNS.OnSNSBarkerContentCoreFocus = HL.Method(HL.Boolean) << function(self, isOn)

   local ctrl = self.m_basicPanelItem.uiCtrl
   ctrl:ToggleTitleBindGroup(not isOn)
end

PhaseSNS.ToggleBasicPanelCloseBtn = HL.Method(HL.Boolean) << function(self, isOn)

   local ctrl = self.m_basicPanelItem.uiCtrl
   ctrl:ToggleCloseBtn(isOn)
end



HL.Commit(PhaseSNS)

