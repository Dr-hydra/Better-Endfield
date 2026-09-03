local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainPOIOverview
local PHASE_ID = PhaseId.DomainPOIOverview

DomainPOIOverviewCtrl = HL.Class('DomainPOIOverviewCtrl', uiCtrl.UICtrl)






DomainPOIOverviewCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_UI_PHASE_EXITED] = '_OnUIPhaseExited',
}




local NOT_HAS_COMPLETE_STATE_POI_TYPE = {
    [GEnums.DomainPoiType.Settlement] = true,
    [GEnums.DomainPoiType.DomainShop] = true,
    [GEnums.DomainPoiType.SewageTreatPlant] = true,
}

DomainPOIOverviewCtrl.m_info = HL.Field(HL.Table)

DomainPOIOverviewCtrl.m_POICellCache = HL.Field(HL.Forward('UIListCache'))

DomainPOIOverviewCtrl.m_closeCallback = HL.Field(HL.Function)


DomainPOIOverviewCtrl.m_lastNaviContext = HL.Field(HL.Table)




DomainPOIOverviewCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData(arg)
    self:_UpdateData()
    self:_RefreshAllUI()
    self:_FocusFirstContentCell()
end

DomainPOIOverviewCtrl.OnClose = HL.Override() << function(self)
    if self.m_closeCallback then
        self.m_closeCallback()
        self.m_closeCallback = nil
    end
end

DomainPOIOverviewCtrl._OnUIPhaseExited = HL.Method(HL.String) << function(self, _)
    self:_TryRefreshOnReturn()
end



DomainPOIOverviewCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    local domainId = arg.domainId
    self.m_closeCallback = arg.closeCallback
    local _, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
    local _, itemCfg = Tables.itemTable:TryGetValue(domainCfg.domainGoldItemId)
    local poiTypeList = { GEnums.DomainPoiType.Settlement }
    for _, poiType in pairs(domainCfg.domainPoiTypeGroup) do
        table.insert(poiTypeList, poiType)
    end
    self.m_info = {
        domainId = domainId,
        domainName = domainCfg.domainName,
        domainMoneyId = domainCfg.domainGoldItemId,
        domainMoneyIcon = itemCfg.iconId,
        domainMoneyCount = Utils.getItemCount(domainCfg.domainGoldItemId),
        domainColor = UIUtils.getColorByString(domainCfg.domainColor),
        poiTypeList = poiTypeList,
        domainLevelList = domainCfg.levelGroup,

        poiInfos = {},
    }
    self.m_lastNaviContext = {}
end

DomainPOIOverviewCtrl._UpdateData = HL.Method() << function(self)

    self.m_info.domainMoneyCount = Utils.getItemCount(self.m_info.domainMoneyId)
    self.m_info.poiInfos = {}
    for i, poiType in pairs(self.m_info.poiTypeList) do
        local _, domainPOICfg = Tables.domainPoiTable:TryGetValue(poiType)
        local getPOIRemindInfoFuncName = DomainPOIUtils.GetPOIRemindInfoFunc[poiType]
        if getPOIRemindInfoFuncName then
            if Utils.isSystemUnlocked(domainPOICfg.unlockSystemType) then
                local levelContentInfos = self:_CollectPOILevelContentInfos(poiType)
                if #levelContentInfos > 0 then
                    local poiInfo = self:_GetSinglePOIInfo(poiType)
                    poiInfo.levelContentInfos = levelContentInfos
                    self:_UpdateSinglePOIInfo(poiInfo)
                    poiInfo.sortId = domainPOICfg.overviewSortId
                    poiInfo.hasRemindSort = poiInfo.hasRemind and 1 or 0

                    local preHasRemind = DomainPOIUtils.GetPOIOverviewCellPreHasRemind(self.m_info.domainId, poiInfo.poiType)
                    if preHasRemind == nil then
                        preHasRemind = poiInfo.hasRemind
                        DomainPOIUtils.SetPOIOverviewCellPreHasRemind(self.m_info.domainId, poiInfo.poiType, preHasRemind)
                    end
                    local isNewHasRemind = false
                    if preHasRemind ~= poiInfo.hasRemind then
                        isNewHasRemind = poiInfo.hasRemind and not preHasRemind
                        DomainPOIUtils.SetPOIOverviewCellPreHasRemind(self.m_info.domainId, poiInfo.poiType, poiInfo.hasRemind)
                    end
                    if DeviceInfo.usingController then
                        poiInfo.isFold = false
                    else

                        if poiInfo.hasRemind and isNewHasRemind and poiInfo.isFold then
                            poiInfo.isFold = false
                            DomainPOIUtils.SetPOIOverviewCellIsFold(self.m_info.domainId, poiInfo.poiType, false)
                        else
                            poiInfo.isFold = poiInfo.isFold or not poiInfo.hasRemind
                        end
                    end
                    table.insert(self.m_info.poiInfos, poiInfo)
                end
            end
        else
            logger.error(string.format("【系统POI总览：%s】未实现DomainPOIUtils.GetPOIRemindInfoFunc!", domainPOICfg.name))
        end
    end

    table.sort(self.m_info.poiInfos, Utils.genSortFunction({ "hasRemindSort", "sortId" }, false))
end

DomainPOIOverviewCtrl._CollectPOILevelContentInfos = HL.Method(GEnums.DomainPoiType).Return(HL.Table) << function(self, poiType)
    local _, domainPOICfg = Tables.domainPoiTable:TryGetValue(poiType)
    local getPOIRemindInfoFuncName = DomainPOIUtils.GetPOIRemindInfoFunc[poiType]
    if not getPOIRemindInfoFuncName then
        logger.error(string.format("【系统POI总览：%s】未实现DomainPOIUtils.GetPOIRemindInfoFunc!", domainPOICfg.name))
        return {}
    end
    local levelContentInfos = {}
    for _, levelId in pairs(self.m_info.domainLevelList) do
        local contentInfos = DomainPOIUtils[getPOIRemindInfoFuncName](levelId)
        if contentInfos then
            for _, contentInfo in pairs(contentInfos) do
                local hasValue, markInstId = DomainPOIUtils.GetPOIMapMarkInstId(contentInfo.mapMarkType, contentInfo.poiId)
                if hasValue and MapUtils.isMarkVisible(markInstId) then
                    contentInfo.levelId = levelId
                    table.insert(levelContentInfos, contentInfo)
                end
            end
        end
    end
    return levelContentInfos
end


DomainPOIOverviewCtrl._RefreshAllExistingPOIInfos = HL.Method() << function(self)
    self.m_info.domainMoneyCount = Utils.getItemCount(self.m_info.domainMoneyId)
    for _, poiInfo in ipairs(self.m_info.poiInfos) do
        poiInfo.levelContentInfos = self:_CollectPOILevelContentInfos(poiInfo.poiType)
        self:_UpdateSinglePOIInfo(poiInfo)
        poiInfo.hasRemindSort = poiInfo.hasRemind and 1 or 0
        self:_UpdatePOIPreHasRemind(poiInfo)
    end
end





DomainPOIOverviewCtrl._UpdatePOIPreHasRemind = HL.Method(HL.Table) << function(self, poiInfo)
    local preHasRemind = DomainPOIUtils.GetPOIOverviewCellPreHasRemind(self.m_info.domainId, poiInfo.poiType)
    if preHasRemind == nil or preHasRemind ~= poiInfo.hasRemind then
        DomainPOIUtils.SetPOIOverviewCellPreHasRemind(self.m_info.domainId, poiInfo.poiType, poiInfo.hasRemind)
    end
end




DomainPOIOverviewCtrl._GetSinglePOIInfo = HL.Method(GEnums.DomainPoiType).Return(HL.Table) << function(self, poiType)
    local _, domainPOICfg = Tables.domainPoiTable:TryGetValue(poiType)
    local info = {
        poiType = poiType,
        poiName = domainPOICfg.name,
        poiIcon = domainPOICfg.overviewIcon,

        isFold = DomainPOIUtils.GetPOIOverviewCellIsFold(self.m_info.domainId, poiType),
        contentCellCache = nil,

        hasRemind = false,
        levelContentInfos = nil,
    }
    return info
end




DomainPOIOverviewCtrl._GetPOIIndexByType = HL.Method(GEnums.DomainPoiType).Return(HL.Number) << function(self, poiType)
    for poiIndex, poiInfo in ipairs(self.m_info.poiInfos) do
        if poiInfo.poiType == poiType then
            return poiIndex
        end
    end
    return 0
end

DomainPOIOverviewCtrl._UpdateSinglePOIInfo = HL.Method(HL.Table) << function(self, poiInfo)
    local poiHasRemind = false
    for _, contentInfo in pairs(poiInfo.levelContentInfos) do
        DomainPOIUtils.CalculateSinglePOIOverviewContentInfo(self.m_info.domainId, contentInfo, false)
        if contentInfo.hasRemind then
            poiHasRemind = true
        end
    end
    poiInfo.hasRemind = poiHasRemind

    table.sort(poiInfo.levelContentInfos, function(a, b)
        if a.hasRemind ~= b.hasRemind then
            return a.hasRemind
        end
        if a.levelId ~= b.levelId then
            return a.levelId > b.levelId
        end
        return a.poiSerial < b.poiSerial
    end)
end



DomainPOIOverviewCtrl._InitUI = HL.Method() << function(self)
    self.view.domainTopMoneyTitle.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.m_POICellCache = UIUtils.genCellCache(self.view.poiCell)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.redDotScrollRect.getRedDotStateAt = function(csIndex)
        local luaIndex = LuaIndex(csIndex)
        local poiInfo = self.m_info.poiInfos[luaIndex]
        if poiInfo.hasRemind then
            return UIConst.RED_DOT_TYPE.Normal
        end
        return 0
    end
end

DomainPOIOverviewCtrl._RefreshAllUI = HL.Method() << function(self)
    local infoCount = #self.m_info.poiInfos
    self.view.main:SetState(infoCount > 0 and "Normal" or "Empty")
    self.m_POICellCache:Refresh(infoCount, function(cell, luaIndex)
        self:_OnRefreshPOICell(cell, luaIndex)
    end)
    self.view.domainTopMoneyTitle.view.titleTxt.text = string.format(Language.LUA_DOMAIN_POI_OVERVIEW_TITLE, self.m_info.domainName)
    self.m_info.domainColor.a = self.view.colorNode.color.a
    self.view.colorNode.color = self.m_info.domainColor

    self.view.domainTopMoneyTitle:InitDomainTopMoneyTitle(self.m_info.domainId)
end

DomainPOIOverviewCtrl._OnRefreshPOICell = HL.Method(HL.Any, HL.Number) << function(self, inCell, luaIndex)

    local cell = inCell
    local poiInfo = self.m_info.poiInfos[luaIndex]
    if poiInfo.contentCellCache == nil then
        poiInfo.contentCellCache = UIUtils.genCellCache(cell.contentCell)
    end

    cell.poiNameTxt.text = poiInfo.poiName
    cell.poiIcon:LoadSprite(UIConst.UI_SPRITE_DOMAIN, poiInfo.poiIcon)
    cell.foldBtnStateController:SetState(poiInfo.hasRemind and "HasRemind" or "NoRemind")

    self.m_info.domainColor.a = cell.colorCanvas.color.a
    cell.colorCanvas.color = self.m_info.domainColor
    self.m_info.domainColor.a = cell.colorImgBar.color.a
    cell.colorImgBar.color = self.m_info.domainColor
    self.m_info.domainColor.a = cell.colorImgDeco.color.a
    cell.colorImgDeco.color = self.m_info.domainColor

    cell.foldBtnStateController:SetState(poiInfo.isFold and "Close" or "Open")
    cell.foldBtn.onClick:RemoveAllListeners()
    cell.foldBtn.onClick:AddListener(function()
        if DeviceInfo.usingController then
            return
        end
        cell.foldBtnStateController:SetState(poiInfo.isFold and "Open" or "Close")
        poiInfo.isFold = not poiInfo.isFold
        if poiInfo.hasRemind then

            DomainPOIUtils.SetPOIOverviewCellIsFold(self.m_info.domainId, poiInfo.poiType, poiInfo.isFold)
        end
    end)

    cell.redDot:InitRedDot("DomainPOIOverviewSingle", {
        domainId = self.m_info.domainId,
        poiType = poiInfo.poiType
    }, nil, self.view.redDotScrollRect)

    poiInfo.contentCellCache:Refresh(#poiInfo.levelContentInfos, function(contentCell, contentIndex)
        self:_OnRefreshContentCell(contentCell, luaIndex, contentIndex)
    end)
end

DomainPOIOverviewCtrl._OnRefreshContentCell = HL.Method(HL.Any, HL.Number, HL.Number) << function(self, inCell, poiIndex, contentIndex)

    local cell = inCell
    local poiInfo = self.m_info.poiInfos[poiIndex]
    local contentInfo = poiInfo.levelContentInfos[contentIndex]
    cell.naviDeco.onIsNaviTargetChanged = function(isNaviTarget)
        if isNaviTarget then
            self.m_lastNaviContext.poiType = poiInfo.poiType
            self.m_lastNaviContext.poiId = contentInfo.poiId
        end
    end


    cell.stateController:SetState(contentInfo.hasRemind and "HasRemind" or "NoRemind")


    local nameNode = cell.nameNode
    nameNode.levelNameTxt.text = contentInfo.levelName
    local poiName = poiInfo.poiName

    if not string.isEmpty(contentInfo.poiName) then
        poiName = contentInfo.poiName
    elseif contentInfo.mapMarkType then
        local hasValue, markInstId = DomainPOIUtils.GetPOIMapMarkInstId(contentInfo.mapMarkType, contentInfo.poiId)
        if hasValue then
            local runtimeSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
            if runtimeSuccess then
                local templateId = markRuntimeData.templateId
                local hasCfg, mapMarkTempCfg = Tables.mapMarkTempTable:TryGetValue(templateId)
                if hasCfg then
                    if mapMarkTempCfg.markType == GEnums.MarkType.CustomMark then
                        poiName = markRuntimeData.note
                    else
                        poiName = mapMarkTempCfg.name
                    end
                end
            end
        end
    end

    if contentInfo.poiSerial > 0 then
        poiName = poiName .. "#" .. contentInfo.poiSerial
    end
    nameNode.poiNameTxt.text = poiName

    self.m_info.domainColor.a = cell.nameNode.colorImg1.color.a
    cell.nameNode.colorImg1.color = self.m_info.domainColor
    self.m_info.domainColor.a = cell.nameNode.colorImg2.color.a
    cell.nameNode.colorImg2.color = self.m_info.domainColor


    local levelNode = cell.levelNode
    levelNode.lvTxt.text = "Lv." .. contentInfo.curLevel
    levelNode.moneyIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, self.m_info.domainMoneyIcon)
    if contentInfo.upgradeCostMoney then
        levelNode.costMoneyNumTxt.text = contentInfo.upgradeCostMoney
    end

    levelNode.stateController:SetState(contentInfo.curLevel <= 0 and "Lock" or "Unlock")

    if contentInfo.canUpgrade then
        levelNode.stateController:SetState(contentInfo.remindUpgrade and "CanUpgradeAndEnough" or "CanUpgradeNotEnough")
    else
        levelNode.stateController:SetState("CanNotUpgrade")
    end

    levelNode.gotoTaskBtn.onClick:RemoveAllListeners()
    if contentInfo.isLvMax then

        levelNode.stateController:SetState("LevelDesc")
        if contentInfo.isFinalMaxLv then
            levelNode.levelDescTxt.text = Language.LUA_DOMAIN_POI_OVERVIEW_MAX
        else
            levelNode.levelDescTxt.text = Language.LUA_DOMAIN_POI_OVERVIEW_VERSION_MAX
        end
    elseif contentInfo.isBlockUpgrade then

        levelNode.stateController:SetState("LevelDesc")
        levelNode.levelDescTxt.text = contentInfo.blockUpgradeDesc
    else

        if not string.isEmpty(contentInfo.upgradeMissionId) then
            levelNode.stateController:SetState("Quest")
            levelNode.gotoTaskBtn.onClick:AddListener(function()
                PhaseManager:OpenPhase(PhaseId.Mission, {
                    autoSelect = contentInfo.upgradeMissionId
                })
            end)
        elseif contentInfo.upgradeCostMoney > 0 then
            levelNode.stateController:SetState("CostMoney")
            if contentInfo.upgradeCostMoney > self.m_info.domainMoneyCount then
                levelNode.costMoneyNumTxt.color = self.view.config.MONEY_NOT_ENOUGH_COLOR
            end
        else

            levelNode.stateController:SetState("LevelDesc")
            levelNode.levelDescTxt.text = ""
        end
    end


    local stateNode = cell.stateNode
    stateNode.stateDescTxt.text = contentInfo.stateDesc
    if contentInfo.hasRemind then
        if contentInfo.curLevel == 0 then
            stateNode.stateController:SetState("NeedUnlock")
            stateNode.stateDescTxt.text = Language.LUA_DOMAIN_POI_COMMON_LOCK
        elseif contentInfo.needWarning then
            stateNode.stateController:SetState("Warning")
        else
            stateNode.stateController:SetState("Normal")
        end
    else
        if contentInfo.curLevel == 0 then
            stateNode.stateController:SetState("NeedUnlock")
            stateNode.stateDescTxt.text = Language.LUA_DOMAIN_POI_COMMON_LOCK
        else
            stateNode.stateController:SetState(NOT_HAS_COMPLETE_STATE_POI_TYPE[poiInfo.poiType] and "Normal" or "Complete")
        end
    end
    local canJumpMainPage = contentInfo.jumpPhaseId ~= nil and contentInfo.curLevel > 0
    if stateNode.jumpBtn then
        stateNode.jumpBtn.gameObject:SetActive(canJumpMainPage)
        stateNode.jumpBtn.onClick:RemoveAllListeners()
        if canJumpMainPage then
            stateNode.jumpBtn.onClick:AddListener(function()
                self:_TryOpenPOIMainPage(contentInfo)
            end)
        end
    end


    cell.gotoMapBtn.onClick:RemoveAllListeners()
    cell.gotoMapBtn.onClick:AddListener(function()
        if contentInfo.mapMarkType then
            local hasValue, markInstId = DomainPOIUtils.GetPOIMapMarkInstId(contentInfo.mapMarkType, contentInfo.poiId)
            if hasValue then
                MapUtils.openMap(markInstId, contentInfo.levelId)
            else
                logger.error(string.format("【系统POI总览】关卡id：%s，POI名：%s，GetMapMarkInstId失败！无法跳转", contentInfo.levelId, poiInfo.poiName))
            end
        else
            logger.error(string.format("【系统POI总览】关卡id：%s，POI名：%s，数据缺少mapMarkType！", contentInfo.levelId, poiInfo.poiName))
        end
    end)
end



DomainPOIOverviewCtrl._FocusFirstContentCell = HL.Method() << function(self)
    local infoCount = #self.m_info.poiInfos
    if infoCount <= 0 then
        return
    end
    local poiInfo = self.m_info.poiInfos[1]

    local cell = poiInfo.contentCellCache:Get(1)
    if cell then
        self:SetNaviTarget(cell.naviDeco)
    end
end

DomainPOIOverviewCtrl._TryRefreshOnReturn = HL.Method() << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    self:_RefreshOnReturn()
end

DomainPOIOverviewCtrl._RefreshOnReturn = HL.Method() << function(self)
    self:_RefreshAllExistingPOIInfos()
    self:_RefreshAllUI()
    self:_TryRestoreLastNaviContent()
end




DomainPOIOverviewCtrl._TryRestoreLastNaviContent = HL.Method() << function(self)
    local lastNaviContext = self.m_lastNaviContext
    if lastNaviContext == nil then
        self:_FocusFirstContentCell()
        return
    end
    local poiIndex = self:_GetPOIIndexByType(lastNaviContext.poiType)
    if poiIndex <= 0 then
        self:_FocusFirstContentCell()
        return
    end
    self:_FocusContentCellByPoiId(poiIndex, lastNaviContext.poiId)
end






DomainPOIOverviewCtrl._FocusContentCellByPoiId = HL.Method(HL.Number, HL.String) << function(self, poiIndex, poiId)
    local poiInfo = self.m_info.poiInfos[poiIndex]
    if poiInfo == nil or poiInfo.contentCellCache == nil then
        self:_FocusFirstContentCell()
        return
    end
    local targetContentIndex = 0
    for contentIndex, contentInfo in ipairs(poiInfo.levelContentInfos) do
        if contentInfo.poiId == poiId then
            targetContentIndex = contentIndex
            break
        end
    end
    if targetContentIndex <= 0 then
        self:_FocusFirstContentCell()
        return
    end
    local cell = poiInfo.contentCellCache:Get(targetContentIndex)
    if cell then
        self:SetNaviTarget(cell.naviDeco)
    else
        self:_FocusFirstContentCell()
    end
end





DomainPOIOverviewCtrl._TryOpenPOIMainPage = HL.Method(HL.Table) << function(self, contentInfo)
    if contentInfo.jumpPhaseId == nil then
        return
    end
    PhaseManager:OpenPhase(contentInfo.jumpPhaseId, {
        domainId = self.m_info.domainId,
        poiId = contentInfo.poiId,
    })
end


HL.Commit(DomainPOIOverviewCtrl)
