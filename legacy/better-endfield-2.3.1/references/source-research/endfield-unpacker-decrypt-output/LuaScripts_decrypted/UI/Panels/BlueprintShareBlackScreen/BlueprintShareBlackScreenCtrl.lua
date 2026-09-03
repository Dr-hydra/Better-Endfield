local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BlueprintShareBlackScreen
local PHASE_ID = PhaseId.BlueprintShareBlackScreen
BlueprintShareBlackScreenCtrl = HL.Class('BlueprintShareBlackScreenCtrl', uiCtrl.UICtrl)





BlueprintShareBlackScreenCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.FAC_ON_SHARE_BLUEPRINT] = 'FacOnShareBlueprint',
}

BlueprintShareBlackScreenCtrl.m_blueprintID = HL.Field(HL.Any) << 0

BlueprintShareBlackScreenCtrl.m_csBPInst = HL.Field(CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintInstance)

BlueprintShareBlackScreenCtrl.m_isReporting = HL.Field(HL.Boolean) << false

BlueprintShareBlackScreenCtrl.m_sharingOutside = HL.Field(HL.Boolean) << false

BlueprintShareBlackScreenCtrl.m_gettingShareCode = HL.Field(HL.Boolean) << false

BlueprintShareBlackScreenCtrl.m_onClose = HL.Field(HL.Function)

BlueprintShareBlackScreenCtrl.m_deviceInfo = HL.Field(HL.Table)


BlueprintShareBlackScreenCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_blueprintID = arg.id
    self.m_csBPInst = arg.csBPInst
    self.m_isReporting = arg.isReporting
    self.m_deviceInfo = arg.deviceInfo
    local tipsTransform = arg.tipsTransform
    if arg.onClose then
        self.m_onClose = arg.onClose
    end
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.selfGroup.groupId })
    self:_InitNodes(tipsTransform)
    InputManagerInst:ToggleGroup(self.view.bindingGroup.groupId, true)
    if self.m_isReporting then
        self:SetNaviTarget(self.view.infoBtn)
    else
        self:SetNaviTarget(self.view.shareToFriendsBtn)
    end
end


BlueprintShareBlackScreenCtrl._InitNodes = HL.Method(HL.Any) << function(self,tipsTransform)
    if not self.m_isReporting then
        self.view.stateController:SetState("share")
        self.view.shareNode.transform.position = tipsTransform:TransformPoint(CS.UnityEngine.Vector3(-tipsTransform.rect.width, -tipsTransform.rect.height*1/2, 0))
        self.view.shareOutsideBtn.onClick:AddListener(function()
            self:_ShareOutside()
        end)
        self.view.shareToFriendsBtn.onClick:AddListener(function()
            self.view.shareNode.gameObject:SetActive(false)
            self:_ShareToFriends()
        end)
        self.view.clickBtn.onClick:AddListener(function()
            self:PlayAnimationOutAndClose()
        end)
    else
        self.view.stateController:SetState("more")
        self.view.moreNode.transform.position = tipsTransform:TransformPoint(CS.UnityEngine.Vector3(-tipsTransform.rect.width, -tipsTransform.rect.height*1/2, 0))
        self.view.reportBtn.onClick:AddListener(function()
            self:_ReportBlueprint()
        end)
        self.view.infoBtn.onClick:AddListener(function()
            self:PlayAnimationOutAndClose()
            self:_SeeCreatorInfo()
        end)
        self.view.clickBtn.onClick:AddListener(function()
            self:PlayAnimationOutAndClose()
        end)
    end
end

BlueprintShareBlackScreenCtrl._ReportBlueprint = HL.Method() << function(self)
    local roleId = self.m_csBPInst.creatorRoleId
    GameInstance.player.friendSystem:SyncFriendInfoById(roleId, function()
        UIManager:Open(PanelId.ReportPlayer,{
            reportType = FriendUtils.ReportGroupType.Blueprint,
            roleId = roleId,
            blueprintParam = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintUtil.ParamBlueprintToProto(self.m_csBPInst.param),
        })
        self:PlayAnimationOutAndClose()
    end)
end

BlueprintShareBlackScreenCtrl.OnClose = HL.Override() << function(self)
    if self.m_onClose then
        self.m_onClose()
    end
end

BlueprintShareBlackScreenCtrl._ShareOutside = HL.Method() << function(self)
    self.m_sharingOutside = true
    self:_GetShareCode()
end

BlueprintShareBlackScreenCtrl.FacOnShareBlueprint = HL.Method(HL.Table) << function(self, args)
    if self.m_gettingShareCode then
        self.m_gettingShareCode = false
        local shareCode = unpack(args)
        local devices = {}
        for _,device in ipairs(self.m_deviceInfo) do
            devices[device.buildingId] = device.count
        end
        local activityId = FactoryUtils.getActivityIdByBluePrintId(self.m_csBPInst.param.bpGiftUid)
        EventLogManagerInst:GameEvent_ShareBlueprint(self.m_sharingOutside and "Outside" or "Friend",
            shareCode,
            self.m_csBPInst.param.bpGiftUid,
            self.m_csBPInst.creatorUserId,
            self.m_csBPInst.param.shareIdx,
            self.m_csBPInst.info.name,
            self.m_csBPInst.info.bp.sourceRect.width,
            self.m_csBPInst.info.bp.sourceRect.height,
            self.m_csBPInst.info.tags,
            self.m_csBPInst.info.desc,
            devices,
            activityId)
        if self.m_sharingOutside then
            UIManager:Open(PanelId.FacSaveBlueprint, {
                bpInst = self.m_csBPInst,
                isSharing = true,
                isEditing = false,
                id = self.m_blueprintID,
            })
            UIManager:Hide(PanelId.CommonToast)
            Notify(MessageConst.SHOW_COMMON_SHARE_PANEL,{
                type = "Blueprint",
                codeId = shareCode,
                copyStr = string.format(Language.LUA_BLUEPRINT_SHARE_COPY_TIPS, self.m_csBPInst.info.name, shareCode),
                showPlayerInfo = true,
                showPlayerInfoToggle = false,
                onCaptureEnd = function()
                    UIManager:Close(PanelId.FacSaveBlueprint)
                    self:PlayAnimationOutAndClose()
                end,
                title = Language['ui_blueprint_mainpanel_info_shared_button_outside_code'],
            })
        else
            UIManager:Open(PanelId.FriendRequest, {
                onShareClick = function(roleId)
                    UIManager:Close(PanelId.FriendRequest)
                    GameInstance.player.friendChatSystem:SendChatBluePrint(roleId, shareCode, function()
                        PhaseManager:GoToPhase(PhaseId.SNS, { roleId = roleId })
                    end)
                end
            })
            self:PlayAnimationOutAndClose()
        end
    end
end

BlueprintShareBlackScreenCtrl._ShareToFriends = HL.Method() << function(self)
    self.m_sharingOutside = false
    self:_GetShareCode()
end

BlueprintShareBlackScreenCtrl._GetShareCode = HL.Method() << function(self)
    self.m_gettingShareCode = true
    if self.m_csBPInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Mine then
        GameInstance.player.remoteFactory.blueprint:SendShareBlueprint(self.m_blueprintID)
    elseif self.m_csBPInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Sys then
        GameInstance.player.remoteFactory.blueprint:SendShareSysBlueprint(self.m_blueprintID)
    else
        GameInstance.player.remoteFactory.blueprint:SendShareGiftBlueprint(self.m_blueprintID)
    end
end


BlueprintShareBlackScreenCtrl._SeeCreatorInfo = HL.Method() << function(self)
    if GameInstance.player.friendSystem:PlayerInBlackList(self.m_csBPInst.creatorRoleId) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_BLUEPRINT_BLACK_LIST_VISIT)
        return
    end
    Notify(MessageConst.ON_OPEN_BUSINESS_CARD_PREVIEW, { roleId = self.m_csBPInst.creatorRoleId, isPhase = true })
end

HL.Commit(BlueprintShareBlackScreenCtrl)
