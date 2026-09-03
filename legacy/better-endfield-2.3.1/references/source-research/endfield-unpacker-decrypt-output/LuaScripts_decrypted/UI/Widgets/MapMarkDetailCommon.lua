local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MapMarkDetailCommon = HL.Class('MapMarkDetailCommon', UIWidgetBase)


MapMarkDetailCommon.m_onLeftBtnClickFromOuterSide = HL.Field(HL.Function)


MapMarkDetailCommon.m_onRightBtnClickFromOuterSide = HL.Field(HL.Function)


MapMarkDetailCommon.m_onBigBtnClickFromOuterSide = HL.Field(HL.Function)

MapMarkDetailCommon.m_markDetailData = HL.Field(HL.Any)

MapMarkDetailCommon.m_template = HL.Field(HL.Any)

MapMarkDetailCommon.m_instId = HL.Field(HL.String) << ""

MapMarkDetailCommon.s_forbidAllBtn = HL.StaticField(HL.Boolean) << false


MapMarkDetailCommon._OnFirstTimeInit = HL.Override() << function(self)
    self.view.fullScreenBtn.onClick:AddListener(function()
        self:_Close()
    end)

    self.view.closeBtn.onClick:AddListener(function()
        self:_Close()
    end)

    self.view.leftBtn.onClick:AddListener(function()
        self:_OnLeftBtnClick()
    end)

    self.view.rightBtn.onClick:AddListener(function()
        self:_OnRightBtnClick()
    end)

    self.view.bigBtn.onClick:AddListener(function()
        self:_OnBigBtnClick()
    end)
end

MapMarkDetailCommon.InitMapMarkDetailCommon = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()

    self.m_instId = args.markInstId

    self:ToggleEmptyMaskVisibleState(false)

    local getRuntimeDataSuccess
    getRuntimeDataSuccess, self.m_markDetailData = GameInstance.player.mapManager:GetMarkInstRuntimeData(self.m_instId)

    if getRuntimeDataSuccess == false then
        logger.error("地图详情页获取实例数据失败" .. self.m_instId)
        return
    end

    local getTemplateSuccess, template = Tables.mapMarkTempTable:TryGetValue(self.m_markDetailData.templateId)
    if getTemplateSuccess == false then
        logger.error("地图详情页模板失败" .. self.m_instId)
        return
    end

    if args.descText ~= nil then
        self.view.desc:SetAndResolveTextStyle(args.descText)
    else
        self.view.desc:SetAndResolveTextStyle(template.desc)
    end

    if args.titleText ~= nil then
        self.view.common.title.text.text = args.titleText
    else

        local overrideTitle = self.m_markDetailData:GetTitleOverrideText()
        if not string.isEmpty(overrideTitle) then
            self.view.common.title.text.text = overrideTitle
        else
            self.view.common.title.text.text = template.name
        end
    end

    local levelId = self.m_markDetailData.levelId
    local getLevelNameSuccess, levelDescInfo = Tables.levelDescTable:TryGetValue(levelId)

    if getLevelNameSuccess == false then
        logger.error("关卡名称获取失败" .. self.m_instId)
        return
    end

    local needRefreshWithTier = false
    if self.m_markDetailData.tierId ~= MapConst.BASE_TIER_ID then
        local tierIndex = GameWorld.mapRegionManager:GetTierIndex(self.m_markDetailData.tierId)
        needRefreshWithTier = tierIndex ~= MapConst.BASE_TIER_INDEX
    end
    if needRefreshWithTier then
        local _, loaderData = GameInstance.player.mapManager:GetLoaderLevelDataByLevelId(self.m_markDetailData.levelId)
        local tierNames = loaderData.loadConfig.tierNames
        local nameText = Language[tierNames[self.m_markDetailData.tierId]]
        self.view.common.subTitle.text.text = string.format(
            Language.LUA_MAP_MARK_DETAIL_COMMON_SUB_TITLE_TIER_TEXT,
            levelDescInfo.showName,
            nameText
        )
    else
        self.view.common.subTitle.text.text = levelDescInfo.showName
    end

    self.m_template = template
    local leftBtnActive = (args.leftBtnActive == true)
    self.view.leftBtn.gameObject:SetActive(leftBtnActive)
    if leftBtnActive == true then
        local leftBtnCallback = args.leftBtnCallback
        if leftBtnCallback ~= nil then
            self.m_onLeftBtnClickFromOuterSide = leftBtnCallback
        end
        local leftBtnText = args.leftBtnText
        self.view.leftBtnText.text = leftBtnText
        self.view.leftBtnIcon:LoadSprite(UIConst.UI_SPRITE_MAP_DETAIL_BTN_ICON, args.leftBtnIconName)
    end

    local rightBtnActive = (args.rightBtnActive == true)
    self.view.rightBtn.gameObject:SetActive(rightBtnActive)
    if rightBtnActive == true then
        local rightBtnCallback = args.rightBtnCallback
        if rightBtnCallback ~= nil then
            self.m_onRightBtnClickFromOuterSide = rightBtnCallback
            local rightBtnText = args.rightBtnText
            self.view.rightBtnText.text = rightBtnText
            self.view.rightBtnIcon:LoadSprite(UIConst.UI_SPRITE_MAP_DETAIL_BTN_ICON, args.rightBtnIconName)
        else
            self.view.rightBtn.hintTextId = ""
            self:_SetTracerBtn()
        end
    end

    local bigBtnActive = (args.bigBtnActive == true)
    self.view.bigBtn.gameObject:SetActive(bigBtnActive)
    if bigBtnActive == true then
        local bigBtnCallback = args.bigBtnCallback
        if bigBtnCallback ~= nil then
            self.m_onBigBtnClickFromOuterSide = bigBtnCallback
            local bigBtnText = args.bigBtnText
            self.view.bigBtnText.text = bigBtnText
            self.view.bigBtnIcon:LoadSprite(UIConst.UI_SPRITE_MAP_DETAIL_BTN_ICON, args.bigBtnIconName)
        else
            self.view.bigBtn.hintTextId = ""
            self:_SetTracerBtn()
        end
    end

    self:_RefreshHintAndJumpInfo(args)

    if MapMarkDetailCommon.s_forbidAllBtn then
        self.view.btnGroup.gameObject:SetActive(false)
    end

    self:_RefreshHeadIconSprite()

    self:_RefreshJumpToTpBtnVisibleState()

    self:_InitDetailCommonController()
end

MapMarkDetailCommon._SetTracerBtn = HL.Method() << function(self)
    local tracking = self.m_instId == GameInstance.player.mapManager.trackingMarkInstId
    if tracking == false then
        self.view.rightBtnIcon:LoadSprite(UIConst.UI_SPRITE_MAP_DETAIL_BTN_ICON, UIConst.MAP_DETAIL_BTN_ICON_NAME.TRACE)
        self.view.rightBtnText.text = Language["ui_map_common_tracer"]
        self.view.bigBtnIcon:LoadSprite(UIConst.UI_SPRITE_MAP_DETAIL_BTN_ICON, UIConst.MAP_DETAIL_BTN_ICON_NAME.TRACE)
        self.view.bigBtnText.text = Language["ui_map_common_tracer"]
    else
        self.view.rightBtnIcon:LoadSprite(UIConst.UI_SPRITE_MAP_DETAIL_BTN_ICON, UIConst.MAP_DETAIL_BTN_ICON_NAME.REMOVE_TRACE)
        self.view.rightBtnText.text = Language["ui_map_common_tracer_cancel"]
        self.view.bigBtnIcon:LoadSprite(UIConst.UI_SPRITE_MAP_DETAIL_BTN_ICON, UIConst.MAP_DETAIL_BTN_ICON_NAME.REMOVE_TRACE)
        self.view.bigBtnText.text = Language["ui_map_common_tracer_cancel"]
    end
end

MapMarkDetailCommon._RefreshHeadIconSprite = HL.Method() << function(self)
    local active = self.m_markDetailData.isActive
    if active == true then
        self.view.common.title.icon:LoadSprite(UIConst.UI_SPRITE_MAP_MARK_ICON_SMALL, self.m_template.activeIcon)
    else
        self.view.common.title.icon:LoadSprite(UIConst.UI_SPRITE_MAP_MARK_ICON_SMALL, self.m_template.inActiveIcon)
    end
end

MapMarkDetailCommon._SwitchTracerState = HL.Method() << function(self)
    local tracking = self.m_instId == GameInstance.player.mapManager.trackingMarkInstId
    GameInstance.player.mapManager:TrackMark(self.m_instId, not tracking)
end

MapMarkDetailCommon._Close = HL.Method() << function(self)
    Notify(MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL)
end

MapMarkDetailCommon._OnLeftBtnClick = HL.Method() << function(self)
    if self.m_onLeftBtnClickFromOuterSide ~= nil then
        self:m_onLeftBtnClickFromOuterSide(self.m_instId)
    end
end

MapMarkDetailCommon._OnRightBtnClick = HL.Method() << function(self)
    if self.m_onRightBtnClickFromOuterSide ~= nil then
        self:m_onRightBtnClickFromOuterSide(self.m_instId)
    else
        self:_SwitchTracerState()
        self:_SetTracerBtn()
        self:_RefreshHeadIconSprite()
    end
end

MapMarkDetailCommon._OnBigBtnClick = HL.Method() << function(self)
    if self.m_onBigBtnClickFromOuterSide ~= nil then
        self:m_onBigBtnClickFromOuterSide(self.m_instId)
    else
        self:_SwitchTracerState()
        self:_SetTracerBtn()
        self:_RefreshHeadIconSprite()
    end
end

MapMarkDetailCommon._OnJumpToTpBtnClick = HL.Method() << function(self)
    local tpMarkInstId = GameInstance.player.mapManager:GetTargetMarkMostNearbyTpRelatedMark(self.m_instId)
    if string.isEmpty(tpMarkInstId) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_MAP_MARK_DETAIL_COMMON_JUMP_TO_TP_MARK_FAILED)
        return
    end
    Notify(MessageConst.ON_JUMP_TO_TP_RELATED_MARK, tpMarkInstId)
end

MapMarkDetailCommon._RefreshHintAndJumpInfo = HL.Method(HL.Table) << function(self, args)
    if args.hintInfo then
        self.view.hintNode.stateController:SetState(args.hintInfo.importantHint and "Important" or "Normal")
        self.view.hintNode.hintTxt.text = args.hintInfo.hintText
        self.view.hintNode.gameObject:SetActive(true)
    else
        self.view.hintNode.gameObject:SetActive(false)
    end

    if args.jumpInfo then
        self.view.jumpBtn.button.onClick:AddListener(function()
            args.jumpInfo.onJump()
        end)
        self.view.jumpBtn.jumpTxt.text = args.jumpInfo.jumpText
        self.view.jumpBtn.gameObject:SetActive(true)
    else
        self.view.jumpBtn.gameObject:SetActive(false)
    end

    if args.hintInfo == nil and args.jumpInfo == nil then
        local lockedInfo
        if self.m_markDetailData.GetFunctionMissionLockedState ~= nil then
            lockedInfo = {}
            lockedInfo.isLocked, lockedInfo.missionId = self.m_markDetailData:GetFunctionMissionLockedState()
            if not string.isEmpty(lockedInfo.missionId) then
                local missionState = GameInstance.player.mission:GetMissionState(lockedInfo.missionId)
                lockedInfo.missionProcessing = missionState == CS.Beyond.Gameplay.MissionSystem.MissionState.Processing
            end
        end

        if lockedInfo ~= nil and lockedInfo.isLocked then
            local missionName = ""
            if not string.isEmpty(lockedInfo.missionId) then
                local missionInfo = GameInstance.player.mission:GetMissionInfo(lockedInfo.missionId)
                missionName = missionInfo.missionName:GetText()
            end
            if lockedInfo.missionProcessing then
                self.view.jumpBtn.button.onClick:AddListener(function()
                    PhaseManager:OpenPhase(PhaseId.Mission, {autoSelect = lockedInfo.missionId, useBlackMask = true})
                end)
                self.view.jumpBtn.jumpTxt.text = string.format(Language.LUA_DETAIL_COMMON_LOCKED_JUMP_TEXT, missionName)
                self.view.jumpBtn.gameObject:SetActive(true)
                self.view.hintNode.gameObject:SetActive(false)
            else
                self.view.hintNode.stateController:SetState("Important")
                self.view.hintNode.hintTxt.text = string.format(Language.LUA_DETAIL_COMMON_LOCKED_HINT_TEXT, missionName)
                self.view.jumpBtn.gameObject:SetActive(false)
                self.view.hintNode.gameObject:SetActive(true)
            end
        else
            self.view.jumpBtn.gameObject:SetActive(false)
            self.view.hintNode.gameObject:SetActive(false)
        end
    end

    if self.view.hintNode.gameObject.activeSelf or self.view.jumpBtn.gameObject.activeSelf then
        self.view.leftBtn.gameObject:SetActive(false)
        self.view.rightBtn.gameObject:SetActive(false)
        self.view.bigBtn.gameObject:SetActive(false)
    end
end

MapMarkDetailCommon._RefreshJumpToTpBtnVisibleState = HL.Method() << function(self)
    if not self.m_template.showJumpTpBtn then
        return
    end

    if self.view.jumpBtn.gameObject.activeSelf or self.view.hintNode.gameObject.activeSelf then
        return
    end

    if self.view.leftBtn.gameObject.activeSelf and self.view.rightBtn.gameObject.activeSelf then
        logger.warn("当前图标按钮过多，不能使用跳转至附近传送点功能，请确认相关设计")
        return
    end

    if self.view.bigBtn.gameObject.activeSelf then

        self.view.leftBtn.hintTextId = ""
        self.view.leftBtn.text = Language.LUA_MAP_MARK_DETAIL_COMMON_JUMP_TO_TP_BTN_TEXT
        self.view.leftBtnIcon:LoadSprite(UIConst.UI_SPRITE_MAP_ICON, "icon_jump_to_tp_mark")
        self.view.leftBtnIcon:SetNativeSize()
        self.view.leftBtn.onClick:RemoveAllListeners()
        self.view.leftBtn.onClick:AddListener(function()
            self:_OnJumpToTpBtnClick()
        end)

        self.view.rightBtn.hintTextId = ""
        self.view.rightBtn.text = self.view.bigBtnText.text
        self.view.rightBtnIcon.sprite = self.view.bigBtnIcon.sprite
        self.view.rightBtnIcon:SetNativeSize()
        self.view.rightBtn.onClick:RemoveAllListeners()
        self.view.rightBtn.onClick:AddListener(function()
            self:_OnBigBtnClick()
        end)

        self.view.leftBtn.gameObject:SetActive(true)
        self.view.rightBtn.gameObject:SetActive(true)
        self.view.bigBtn.gameObject:SetActive(false)
    else
        self.view.bigBtnText.hintTextId = ""
        self.view.bigBtnText.text = Language.LUA_MAP_MARK_DETAIL_COMMON_JUMP_TO_TP_BTN_TEXT
        self.view.bigBtnIcon:LoadSprite(UIConst.UI_SPRITE_MAP_ICON, "icon_jump_to_tp_mark")
        self.view.bigBtnIcon:SetNativeSize()
        self.view.bigBtn.onClick:RemoveAllListeners()
        self.view.bigBtn.onClick:AddListener(function()
            self:_OnJumpToTpBtnClick()
        end)
        self.view.bigBtn.gameObject:SetActive(true)
    end
end

MapMarkDetailCommon.CloseDetail = HL.Method() << function(self)
    self:_Close()
end



MapMarkDetailCommon._InitDetailCommonController = HL.Method() << function(self)
    local ctrl = self:GetUICtrl()

    ctrl.view.controllerHintPlaceholder = self.view.controllerHintPlaceholder
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ctrl.view.inputGroup.groupId})
end






MapMarkDetailCommon.InitDetailItem = HL.Method(HL.Userdata, HL.Any, HL.Opt(HL.Table)) << function(self, itemCell, itemInfo, tipsExtraInfo)
    if tipsExtraInfo ~= nil then
        tipsExtraInfo.isSideTips = DeviceInfo.usingController
    else
        tipsExtraInfo = { isSideTips = DeviceInfo.usingController }
    end
    itemCell:InitItem(itemInfo, function()
        itemCell:ShowTips(tipsExtraInfo, function()
            local ctrl = self:GetUICtrl()
            if ctrl == nil or ctrl.m_isClosed then
                return
            end
            self:ToggleEmptyMaskVisibleState(false)
        end)
        self:ToggleEmptyMaskVisibleState(true)
    end)
    if DeviceInfo.usingController then
        itemCell:SetEnableHoverTips(false)
    end
end

MapMarkDetailCommon.ToggleEmptyMaskVisibleState = HL.Method(HL.Boolean) << function(self, visible)
    self.view.emptyMask.gameObject:SetActive(visible)
end




HL.Commit(MapMarkDetailCommon)
return MapMarkDetailCommon

