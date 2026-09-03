
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityArknightsBirthPopup

ActivityArknightsBirthPopupCtrl = HL.Class('ActivityArknightsBirthPopupCtrl', uiCtrl.UICtrl)






ActivityArknightsBirthPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_UPDATED] = 'OnActivityUpdate',
}

ActivityArknightsBirthPopupCtrl.m_closeCallback = HL.Field(HL.Function)

ActivityArknightsBirthPopupCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityArknightsBirthPopupCtrl.m_activityData = HL.Field(CS.Beyond.Gameplay.ActivityArknightsBirth)

ActivityArknightsBirthPopupCtrl.m_stageInfoList = HL.Field(HL.Table)

ActivityArknightsBirthPopupCtrl.m_prefabNode = HL.Field(HL.Any)

ActivityArknightsBirthPopupCtrl.m_prefabName = HL.Field(HL.String) << ''

ActivityArknightsBirthPopupCtrl.m_arknightsBirthPopup = HL.Field(HL.Any)

ActivityArknightsBirthPopupCtrl.m_timeOffset = HL.Field(HL.Int) << 0

ActivityArknightsBirthPopupCtrl.m_maskStageId = HL.Field(HL.String) << ''


ActivityArknightsBirthPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_closeCallback = arg.closeCallback or function()
        self:Close()
    end
    self.view.autoCloseButtonUp.onClick:AddListener(function()
        self:_Close()
    end)
    self.view.autoCloseButtonDown.onClick:AddListener(function()
        self:_Close()
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder( { self.view.inputGroup.groupId } )

    self.m_activityId = arg.activityId
    self.m_activityData = GameInstance.player.activitySystem:GetActivity(arg.activityId)

    local suc, multiStageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(arg.activityId)
    if suc then

        local curStage = 0
        local curSortId = 0
        for stageId, stageCfg in pairs(multiStageCfg.stageList) do

            if not Tables.activityArknightsBirthMultiStageTable[stageId].isVisible then
                self.m_maskStageId = stageId
                goto continue
            end

            local csConditionalStageInfo = self.m_activityData:GetStageData(stageId)
            if csConditionalStageInfo ~= nil then

                if stageCfg.timeOffset > self.m_timeOffset then
                    self.m_timeOffset = stageCfg.timeOffset
                end
                local status = GEnums.ActivityConditionalStageState.__CastFrom(csConditionalStageInfo.Status)

                if status ~= GEnums.ActivityConditionalStageState.Locked then
                    curStage = curStage + 1
                    local popupPanelId = Tables.activityArknightsBirthMultiStageTable[stageId].popupPanelId
                    if self.m_prefabName == nil or self.m_prefabName == '' then
                        self.m_prefabName = popupPanelId
                        curSortId = stageCfg.sortId
                    elseif stageCfg.sortId > curSortId then
                        self.m_prefabName = popupPanelId
                        curSortId = stageCfg.sortId
                    end
                end
            end
            :: continue ::
        end


        local path = string.format(UIConst.UI_ACTIVITY_ARKNIGHTS_BIRTH_PREFAB_PATH, self.m_prefabName)
        local prefab = self:LoadGameObject(path)
        if self.m_prefabNode then
            CSUtils.ClearUIComponents(self.m_prefabNode)
            GameObject.DestroyImmediate(self.m_prefabNode)
        end
        self.m_prefabNode = CSUtils.CreateObject(prefab, self.view.main)
        self.m_arknightsBirthPopup = Utils.wrapLuaNode(self.m_prefabNode)


        self.m_arknightsBirthPopup.rightNode.btnClose.onClick:AddListener(function()
            self:_Close()
        end)
        self.m_arknightsBirthPopup.rightNode.activityCommonInfo:InitActivityCommonInfo(arg)
        self.m_arknightsBirthPopup.rightNode.activityCommonInfo:UpdateGoToBtnDetailCallBack(function()
            self:_Close()
        end)
        self.m_arknightsBirthPopup.rightNode.activityCommonInfoLuaReference.gotoNode.btnDetailRedDot.gameObject:SetActive(false)









        if curStage == 1 then
            local leftTime = (self.m_activityData.startTime + self.m_timeOffset * 3600 - DateTimeUtils.GetCurrentTimestampBySeconds())
            local timeTxt = UIUtils.getLeftTime(leftTime)
            self.m_arknightsBirthPopup.leftNode.xiangyanNode.tipsNode.timeTxt:SetAndResolveTextStyle(timeTxt)
        end
    end
end

ActivityArknightsBirthPopupCtrl._Close = HL.Method() << function(self)












    self:PlayAnimationOutWithCallback(function()
        self:Close()
        self.m_closeCallback()
    end)
end

ActivityArknightsBirthPopupCtrl.OnActivityUpdate = HL.Method(HL.Table) << function(self, args)
    local id = unpack(args)
    if id == self.view.config.ACTIVITY_ID and not GameInstance.player.activitySystem:GetActivity(id) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
        self:_Close()
    end
end

HL.Commit(ActivityArknightsBirthPopupCtrl)
