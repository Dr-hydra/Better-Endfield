local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainMain
local PHASE_ID = PhaseId.DomainMain
DomainMainCtrl = HL.Class('DomainMainCtrl', uiCtrl.UICtrl)


local domainDevelopmentSystem = GameInstance.player.domainDevelopmentSystem

local settlementSystem = GameInstance.player.settlementSystem

local inventorySystem = GameInstance.player.inventory

local bulletinRequireTimeInterval = 5
local bulletinDayCount = 3
local dateStateNameMap = {
    [0] = "Now",
    [1] = "Yesterday",
    [2] = "Earlier",
}





DomainMainCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SYNC_DAILY_SOURCE_MONEY_RECORD] = '_OnSyncBulletinData',

    [MessageConst.ON_SETTLEMENT_MODIFY] = '_OnSettlementModify',

    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = '_OnActivityStageUpdate',
}



DomainMainCtrl.m_curDomainId = HL.Field(HL.String) << ""

DomainMainCtrl.m_unlockDomainIds = HL.Field(HL.Table)

DomainMainCtrl.m_curDomainInfo = HL.Field(HL.Table)

DomainMainCtrl.m_bulletinInfoList = HL.Field(HL.Table)

DomainMainCtrl.m_genPoiCells = HL.Field(HL.Forward("UIListCache"))

DomainMainCtrl.m_genDateCells = HL.Field(HL.Forward("UIListCache"))

DomainMainCtrl.m_genIncomeDetailCells = HL.Field(HL.Forward("UIListCache"))

DomainMainCtrl.m_genExpendDetailCells = HL.Field(HL.Forward("UIListCache"))

DomainMainCtrl.m_curSelectBulletinIndex = HL.Field(HL.Number) << 0

DomainMainCtrl.s_lastBulletinSyncTimestamp = HL.StaticField(HL.Number) << 0

DomainMainCtrl.m_waitShowBulletin = HL.Field(HL.Boolean) << false

DomainMainCtrl.m_waitSyncBulletinData = HL.Field(HL.Boolean) << false

DomainMainCtrl.m_bindIdPreDate = HL.Field(HL.Number) << 0

DomainMainCtrl.m_bindIdNextDate = HL.Field(HL.Number) << 0

DomainMainCtrl.m_interactiveLock = HL.Field(HL.Boolean) << false


DomainMainCtrl.m_stlActivityInfo = HL.Field(HL.Table)


DomainMainCtrl.m_resumeArg = HL.Field(HL.Table)






DomainMainCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    LuaSystemManager.mainHudActionQueue:RemoveActionsOfType("DomainUpgrade")
    self:InitUI()
    self:InitData(arg)
    self:UpdateData()
    self:RefreshAllUI()
    if arg and arg.resumeState then
        self.m_resumeArg = arg.resumeState
    end
end

DomainMainCtrl.OnShow = HL.Override() << function(self)
    if self.m_resumeArg then
        return
    end
    self:_TryShowDomainVersionDiff()
end

DomainMainCtrl.OnAnimationInFinished = HL.Override() << function(self)
    self:SetNavi(true)
    if self.m_resumeArg then
        self:_ApplyResumeState(self.m_resumeArg)
        if not (self.m_resumeArg and self.m_resumeArg.isBulletinShown == true) then
            self:_RequireBulletinData()
        end
        self.m_resumeArg = nil
    end
end

DomainMainCtrl.OnHide = HL.Override() << function(self)
    self:_ShowBulletin(false)
end

DomainMainCtrl.OnPhaseRefresh = HL.Override(HL.Opt(HL.Any)) << function(self, arg)
    self:_ShowBulletin(false)
end



DomainMainCtrl.InitData = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    if arg and not string.isEmpty(arg.domainId) then
        self.m_curDomainId = arg.domainId
    else
        self.m_curDomainId = Utils.getCurDomainId()
    end
    self.m_genPoiCells = UIUtils.genCellCache(self.view.poiCell)
    self.m_genDateCells = UIUtils.genCellCache(self.view.bulletinNode.dateCell)
    self.m_genIncomeDetailCells = UIUtils.genCellCache(self.view.bulletinNode.incomeDetailCell)
    self.m_genExpendDetailCells = UIUtils.genCellCache(self.view.bulletinNode.expendBulletinCell)
    self.m_unlockDomainIds = {}
    self.m_bulletinInfoList = {}
    local curDomainIsLock = true
    for domainId, _ in cs_pairs(domainDevelopmentSystem.domainDevDataDic) do
        if domainId == self.m_curDomainId then
            curDomainIsLock = false
        end
        table.insert(self.m_unlockDomainIds, domainId)
    end
    if curDomainIsLock then
        if #self.m_unlockDomainIds > 0 then
            self.m_curDomainId = self.m_unlockDomainIds[1]
        else
            logger.error("所有地区的地区发展都没解锁，但界面被打开了！")
        end
    end
end

DomainMainCtrl.UpdateData = HL.Method() << function(self)


    local _, domainData = domainDevelopmentSystem.domainDevDataDic:TryGetValue(self.m_curDomainId)
    local _, domainCfg = Tables.domainDataTable:TryGetValue(self.m_curDomainId)
    local moneyId = domainCfg.domainGoldItemId
    local moneyItemCfg = Utils.tryGetTableCfg(Tables.itemTable, moneyId)
    local moneyIcon = ""
    if moneyItemCfg then
        moneyIcon = moneyItemCfg.iconId
    end
    local domainCurLvData = domainData.curLevelData
    local poiTypeList = domainCfg.domainPoiTypeGroup
    self.m_curDomainInfo = {
        name = domainCfg.domainName,
        icon = domainCfg.domainIcon,
        color = UIUtils.getColorByString(domainCfg.domainColor),
        bgDeco = domainCfg.domainDevelopmentDeco,
        moneyId = moneyId,
        moneyIcon = moneyIcon,
        maxMoneyCount = domainCurLvData.moneyLimit,

        curExp = domainData.exp,
        curLv = domainData.lv,
        levelUpExp = domainCurLvData.levelUpExp,
        isStlUnlocked = false,
        hasStlCanUpgrade = false,

        poiTypeList = poiTypeList,
        poiInfoList = {},
    }
    for _, poiType in pairs(poiTypeList) do
        local _, poiCfg = Tables.domainPoiTable:TryGetValue(poiType)
        if not string.isEmpty(poiCfg.phaseId) then
            local poiInfo = {
                poiType = poiType,
                icon = poiCfg.icon,
                smallIcon = poiCfg.smallIcon,
                title = poiCfg.name,
                unlockSystemType = poiCfg.unlockSystemType,
                openPhaseId = poiCfg.phaseId
            }
            table.insert(self.m_curDomainInfo.poiInfoList, poiInfo)
        end
    end
    self:_UpdateSettlementInfo()


    self.m_stlActivityInfo = DomainPOIUtils.getSettlementTradeActivityInfo()

end

DomainMainCtrl._UpdateSettlementInfo = HL.Method() << function(self)

    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Settlement) then
        return
    end
    local _, domainCfg = Tables.domainDataTable:TryGetValue(self.m_curDomainId)
    for _, stlId in pairs(domainCfg.settlementGroup) do
        local stlData = settlementSystem:GetUnlockSettlementData(stlId)
        if stlData then
            self.m_curDomainInfo.isStlUnlocked = true
        end
        local canUpgrade = RedDotUtils.hasSettlementCanUpgradeRedDot(stlId)
        if canUpgrade then
            self.m_curDomainInfo.hasStlCanUpgrade = canUpgrade
            break
        end
    end
end

DomainMainCtrl._UpdateBulletinInfo = HL.Method() << function(self)
    self.m_bulletinInfoList = {}
    local moneyId = self.m_curDomainInfo.moneyId
    local dailySourceData = inventorySystem:GetRecentDaysDailySourceData(moneyId, bulletinDayCount)
    if dailySourceData == nil then
        return
    end
    local dataCount = dailySourceData.Count
    local todayIndex = dataCount - 1
    for i = 0, dataCount - 1 do
        local group = dailySourceData[i]
        local info = self:_CreateDailyBulletinInfo(group)
        info.dayOffset = todayIndex - i
        table.insert(self.m_bulletinInfoList, info)
    end
    self.m_curSelectBulletinIndex = LuaIndex(todayIndex)
end

DomainMainCtrl._CreateDailyBulletinInfo = HL.Method(HL.Any).Return(HL.Any) << function(self, dailySourceGroup)
    local info = {
        month = 0,
        day = 0,
        dayOffset = 0,

        incomeInfos = {},
        expendInfos = {},

        totalIncome = 0,
        totalExpend = 0,
        netIncome = 0,
    }
    local date = DateTimeUtils.TimeStamp2ServerTime(dailySourceGroup.timestamp)
    info.month = date.Month
    info.day = date.Day
    for _, record in pairs(dailySourceGroup.recordList) do
        local recordInfo = {
            value = record.recordValue,
            name = record:GetRecordName()
        }
        if record.recordValue >= 0 then
            info.totalIncome = info.totalIncome + record.recordValue
            table.insert(info.incomeInfos, recordInfo)
        else
            info.totalExpend = info.totalExpend + record.recordValue
            table.insert(info.expendInfos, recordInfo)
        end
    end
    info.netIncome = info.totalIncome + info.totalExpend

    return info
end



DomainMainCtrl.InitUI = HL.Method() << function(self)
    self.view.domainTopMoneyTitle.view.closeBtn.onClick:AddListener(function()
        if self.m_interactiveLock then
            return
        end
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self:BindInputPlayerAction("mainhud_open_domain", function()
        if self.m_interactiveLock then
            return
        end
        PhaseManager:PopPhase(PHASE_ID)
    end, self.view.domainTopMoneyTitle.view.closeBtn.groupId)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.changeDomainBtn.onClick:AddListener(function()
        if self.m_interactiveLock or self.view.bulletinNode.gameObject.activeSelf then
            return
        end
        self:_OpenSwitchRegionPopup()
    end)
    self.view.bulletinBtn.onClick:AddListener(function()
        if self.m_interactiveLock then
            return
        end
        self:_ShowBulletin(true)
    end)
    self.view.bulletinNode.closeBulletinBtn.onClick:AddListener(function()
        self:_ShowBulletin(false)
    end)
    self.view.bulletinNode.fullScreenCloseBulletinBtn.onClick:AddListener(function()
        self:_ShowBulletin(false)
    end)
    self.view.domainGradeBtn.onClick:AddListener(function()
        if self.m_interactiveLock then
            return
        end
        self.m_phase.hasJumpedToOtherPhase = true
        self:_ShowBulletin(false)
        self.view.animationWrapper:ClearTween(false)
        self:SetNavi(false)
        AudioManager.PostEvent("Au_UI_Menu_RegionDevelopPanel_BlackShadow_Open")
        self.view.domainTopMoneyTitle.view.walletBarPlaceholder.view.gameObject:SetActive(false)
        self.view.decoflyAniWrapper:Play("domainmainfly_in", function()
            PhaseManager:OpenPhase(PhaseId.DomainGrade, self.m_curDomainId)
        end)
    end)
    self.view.settlementPOICell.btn.onClick:AddListener(function()
        if self.m_interactiveLock then
            return
        end
        if self.m_curDomainInfo.isStlUnlocked then
            self.m_phase.hasJumpedToOtherPhase = true
            self.view.animationWrapper:ClearTween(false)
            self:SetNavi(false)
            AudioManager.PostEvent("Au_UI_Menu_RegionDevelopPanel_BlackShadow_Open")
            self.view.domainTopMoneyTitle.view.walletBarPlaceholder.view.gameObject:SetActive(false)
            self.view.decoflyAniWrapper:Play("domainmainfly_in", function()
                PhaseManager:OpenPhase(PhaseId.SettlementMain, self.m_curDomainId)
            end)
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_DOMAIN_DEVELOPMENT_POI_UNLOCK_CLICK_TOAST)
        end
    end)
    self.view.poiOverviewBtn.onClick:AddListener(function()
        if self.m_interactiveLock or self.view.bulletinNode.gameObject.activeSelf then
            return
        end
        self.m_phase.hasJumpedToOtherPhase = true
        self.view.animationWrapper:ClearTween(false)
        self:SetNavi(false)
        AudioManager.PostEvent("Au_UI_Menu_RegionDevelopPanel_BlackShadow_Open")
        self.view.domainTopMoneyTitle.view.walletBarPlaceholder.view.gameObject:SetActive(false)
        self.view.decoflyAniWrapper:Play("domainmainfly_in", function()
            PhaseManager:OpenPhase(PhaseId.DomainPOIOverview, {
                domainId = self.m_curDomainId,
            })
        end)
    end)

    local preActionId = self.view.bulletinNode.keyHintPre.actionId
    local nextActionId = self.view.bulletinNode.keyHintNext.actionId
    self.m_bindIdPreDate = self:BindInputPlayerAction(preActionId, function()
        local count = #self.m_bulletinInfoList
        if count <= 0 then
            return
        end
        local newIndex = (self.m_curSelectBulletinIndex + count - 2) % count + 1
        if newIndex ~= self.m_curSelectBulletinIndex then
            self:_OnChangeSelectBulletinDate(newIndex)
        end
    end)
    self.m_bindIdNextDate = self:BindInputPlayerAction(nextActionId, function()
        local count = #self.m_bulletinInfoList
        if count <= 0 then
            return
        end
        local newIndex = self.m_curSelectBulletinIndex % count + 1
        if newIndex ~= self.m_curSelectBulletinIndex then
            self:_OnChangeSelectBulletinDate(newIndex)
        end
    end)
end

DomainMainCtrl._OpenSwitchRegionPopup = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if UIManager:IsOpen(PanelId.SettlementSwitchRegionPopup) then
        return
    end
    self:_ShowBulletin(false)
    UIManager:Open(PanelId.SettlementSwitchRegionPopup, {
        curDomainId = self.m_curDomainId,
        unlockedDomainIds = self.m_unlockDomainIds,
        regionRedDot = "DomainSingleMap",
        resumeState = resumeState,
        onConfirm = function(newDomainId)
            self:_OnChangeDomainConfirm(newDomainId)
        end
    })
end

DomainMainCtrl._OnChangeDomainConfirm = HL.Method(HL.String) << function(self, newDomainId)
    if self.m_curDomainId ~= newDomainId then
        self.view.domainTopMoneyTitle.view.walletBarPlaceholder.view.gameObject:SetActive(false)
        self.m_curDomainId = newDomainId
        self:UpdateData()
        self:RefreshAllUI()
        self:_RequireBulletinData()
        self:_TryShowDomainVersionDiff()

        self:SetNavi(false)
        self.animationWrapper:SampleClipAtPercent("domainmain_in_part_0", 1)
        self.animationWrapper:PlayInAnimation(function()
            self.m_interactiveLock = false
            self:SetNaviTarget(self.view.settlementPOICell.btn)
        end)
    end
end

DomainMainCtrl.RefreshAllUI = HL.Method() << function(self)
    local domainInfo = self.m_curDomainInfo
    self:_RefreshTitleMoneyUI()
    self.view.domainTitleTxt.text = domainInfo.name
    self.view.domainIconImg:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT_ICON_BIG, domainInfo.icon)
    self.view.colorImg1.color = domainInfo.color
    self.view.colorImg2.color = domainInfo.color
    self.view.colorImg3.color = domainInfo.color
    self.view.colorImg4.color = domainInfo.color
    self.view.bgDecoImg:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT, domainInfo.bgDeco)
    self.view.levelTxt.text = domainInfo.curLv
    self.view.expSlider.value = domainInfo.levelUpExp > 0 and domainInfo.curExp / domainInfo.levelUpExp or 1
    self.view.expSliderFillImg.color = self.m_curDomainInfo.color
    self.view.changeDomainBtn.gameObject:SetActive(#self.m_unlockDomainIds > 1)

    self:_RefreshSettlementCell()

    self.m_genPoiCells:Refresh(#self.m_curDomainInfo.poiInfoList, function(cell, luaIndex)
        self:_OnRefreshPoiCell(cell, luaIndex)
    end)

    self:_ShowBulletin(false)

    self.view.domainGradeRedDot:InitRedDot("DomainGradeReward", self.m_curDomainId)
    self.view.changeDomainRedDot:InitRedDot("DomainOtherMap", self.m_curDomainId)
    self.view.poiOverviewRedDot:InitRedDot("DomainPOIOverview", self.m_curDomainId)
end

DomainMainCtrl._RefreshTitleMoneyUI = HL.Method() << function(self)
    self.view.domainTopMoneyTitle:InitDomainTopMoneyTitle(self.m_curDomainId)
end

DomainMainCtrl._RefreshSettlementCell = HL.Method() << function(self)
    local stlPOICell = self.view.settlementPOICell
    stlPOICell.lockState:SetState(self.m_curDomainInfo.isStlUnlocked and "UnlockState" or "LockState")
    stlPOICell.upgradeState:SetState(self.m_curDomainInfo.hasStlCanUpgrade and "CanUpgrade" or "NoneUpgrade")

    local hasActivity = self.m_stlActivityInfo.hasActivity and self.m_stlActivityInfo.domainActivityInfos[self.m_curDomainId] ~= nil
    if hasActivity then
        stlPOICell.activityNode.gameObject:SetActive(true)
        local color = self.m_stlActivityInfo.activityColor
        stlPOICell.activityNode.activityBgImg.color = color
        stlPOICell.activityNode.activityIconImg.color = color
    else
        stlPOICell.activityNode.gameObject:SetActive(false)
    end

end

DomainMainCtrl._OnRefreshPoiCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local poiInfo = self.m_curDomainInfo.poiInfoList[luaIndex]
    cell.gameObject.name = "PoiCell_" .. luaIndex
    cell.redDot.gameObject:SetActive(false)
    local isLocked = not Utils.isSystemUnlocked(poiInfo.unlockSystemType) or string.isEmpty(poiInfo.openPhaseId)
    local checkCanOpenFuncName = DomainPOIUtils.CheckCanOpenPOIFunc[poiInfo.poiType]
    local isNotCanOpen = DomainPOIUtils[checkCanOpenFuncName] == nil or not DomainPOIUtils[checkCanOpenFuncName](self.m_curDomainId)

    cell.btn.onClick:RemoveAllListeners()
    cell.btn.onClick:AddListener(function()
        if self.m_interactiveLock then
            return
        end
        if isLocked or isNotCanOpen then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_DOMAIN_DEVELOPMENT_POI_UNLOCK_CLICK_TOAST)
            return
        end
        self.m_phase.hasJumpedToOtherPhase = true
        self.view.animationWrapper:ClearTween(false)
        self:SetNavi(false)
        AudioManager.PostEvent("Au_UI_Menu_RegionDevelopPanel_BlackShadow_Open")
        self.view.domainTopMoneyTitle.view.walletBarPlaceholder.view.gameObject:SetActive(false)
        self.view.decoflyAniWrapper:Play("domainmainfly_in", function()
            PhaseManager:OpenPhase(PhaseId[poiInfo.openPhaseId], {domainId = self.m_curDomainId, onCloseCB = function()
                self.view.decoflyAniWrapper:SampleClipAtPercent("domainmainfly_in", 1)
            end})
        end)
    end)

    if isLocked then
        cell.lockState:SetState("Locked")
        return
    end
    if isNotCanOpen then
        cell.lockState:SetState("Locked")
        return
    end
    cell.lockState:SetState("Unlocked")

    cell.titleTxt.text = poiInfo.title
    cell.iconImg:LoadSprite(UIConst.UI_SPRITE_DOMAIN, poiInfo.icon)
    cell.smallIconImg:LoadSprite(UIConst.UI_SPRITE_DOMAIN, poiInfo.smallIcon)

    if isNotCanOpen then
        cell.redDot:Stop()
        cell.redDot.gameObject:SetActive(false)
    else
        local getRedDotInfoFuncName = DomainPOIUtils.GetRedDotInfoFunc[poiInfo.poiType]
        if getRedDotInfoFuncName and DomainPOIUtils[getRedDotInfoFuncName] then
            local redDotInfo = DomainPOIUtils[getRedDotInfoFuncName](self.m_curDomainId)
            cell.redDot:InitRedDot(redDotInfo.redDotName, redDotInfo.redDotArgs)
            cell.redDot.gameObject:SetActive(true)
        end
    end
end

DomainMainCtrl._RefreshBulletinUI = HL.Method() << function(self)

    self.m_genDateCells:Refresh(bulletinDayCount, function(cell, luaIndex)
        self:_OnRefreshDateCell(cell, luaIndex)
    end)

    self:_RefreshBulletinDetailUI(self.m_curSelectBulletinIndex)
end

DomainMainCtrl._RefreshBulletinDetailUI = HL.Method(HL.Number) << function(self, luaIndex)
    local bulletinInfo = self.m_bulletinInfoList[luaIndex]
    if bulletinInfo == nil then
        logger.error("[DomainMainCtrl] current select bulletinInfo == nil")
        return
    end
    local bulletinNode = self.view.bulletinNode

    local moneyIcon = self.m_curDomainInfo.moneyIcon
    bulletinNode.incomeMoneyImg:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyIcon)
    bulletinNode.expendMoneyImg:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyIcon)
    bulletinNode.netIncomeMoneyIconImg:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyIcon)


    local absTotal = bulletinInfo.totalIncome - bulletinInfo.totalExpend
    bulletinNode.incomeNumTxt.text = bulletinInfo.totalIncome
    bulletinNode.expendNumTxt.text = bulletinInfo.totalExpend
    bulletinNode.incomeBar.fillAmount = bulletinInfo.totalExpend ~= 0 and bulletinInfo.totalIncome / absTotal or 1
    bulletinNode.expendBar.fillAmount = -bulletinInfo.totalExpend / absTotal

    local incomeInfoCount = #bulletinInfo.incomeInfos
    local expendInfoCount = #bulletinInfo.expendInfos
    if incomeInfoCount == 0 and expendInfoCount == 0 then
        bulletinNode.infoState:SetState("NonInfo")
    else
        bulletinNode.infoState:SetState("HasInfo")
        if incomeInfoCount > 0 then
            bulletinNode.incomeBulletinList.gameObject:SetActive(true)
            self.m_genIncomeDetailCells:Refresh(incomeInfoCount, function(cell, iLuaIndex)
                local record = bulletinInfo.incomeInfos[iLuaIndex]
                cell.nameTxt.text = record.name
                cell.numTxt.text = record.value
            end)
        else
            bulletinNode.incomeBulletinList.gameObject:SetActive(false)
        end
        if expendInfoCount > 0 then
            bulletinNode.expendBulletinList.gameObject:SetActive(true)
            self.m_genExpendDetailCells:Refresh(expendInfoCount, function(cell, eLuaIndex)
                local record = bulletinInfo.expendInfos[eLuaIndex]
                cell.nameTxt.text = record.name
                cell.numTxt.text = record.value
            end)
        else
            bulletinNode.expendBulletinList.gameObject:SetActive(false)
        end
    end

    bulletinNode.netIncomeTxt.text = bulletinInfo.netIncome
    if bulletinInfo.netIncome > 0 then
        bulletinNode.netIncomeState:SetState("Positive")
    elseif bulletinInfo.netIncome < 0 then
        bulletinNode.netIncomeState:SetState("Negative")
    else
        bulletinNode.netIncomeState:SetState("Zero")
    end
end

DomainMainCtrl._OnRefreshDateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    cell.normalBtn.onClick:RemoveAllListeners()
    if luaIndex > #self.m_bulletinInfoList then
        cell.emptyState:SetState("Empty")
        return
    end
    cell.emptyState:SetState("Normal")

    local bulletinInfo = self.m_bulletinInfoList[luaIndex]
    cell.monthTxt.text = bulletinInfo.month
    cell.dayTxt.text = "/" .. bulletinInfo.day
    cell.selectState:SetState(self.m_curSelectBulletinIndex == luaIndex and "Select" or "Unselect")
    cell.dateState:SetState(dateStateNameMap[bulletinInfo.dayOffset])

    cell.normalBtn.onClick:AddListener(function()
        if self.m_curSelectBulletinIndex ~= luaIndex then
            self:_OnChangeSelectBulletinDate(luaIndex)
        end
    end)
end

DomainMainCtrl._OnChangeSelectBulletinDate = HL.Method(HL.Number) << function(self, newLuaIndex)
    local oldCell = self.m_genDateCells:Get(self.m_curSelectBulletinIndex)
    if oldCell then
        oldCell.selectState:SetState("Unselect")
        AudioManager.PostEvent("Au_UI_Toggle_Common_Off")
    end
    local cell = self.m_genDateCells:Get(newLuaIndex)
    if cell then
        cell.selectState:SetState("Select")
        AudioManager.PostEvent("Au_UI_Toggle_Common_On")
    end
    self.m_curSelectBulletinIndex = newLuaIndex
    self:_RefreshBulletinDetailUI(newLuaIndex)

    local enablePre = newLuaIndex ~= 1
    local enableNext = newLuaIndex ~= #self.m_bulletinInfoList
    self.view.bulletinNode.keyHintPre.enabled = enablePre
    self.view.bulletinNode.keyHintNext.enabled = enableNext
end

DomainMainCtrl._TryShowDomainVersionDiff = HL.Method() << function(self)
    logger.info("尝试显示版本差异信息：", self.m_curDomainId)
    if UIManager:IsOpen(PanelId.DomainVersionInfoPopup) then
        return
    end
    local startVersionData, endVersionData = DomainPOIUtils.DomainMaxLevelHasVersionDiff(self.m_curDomainId)
    if not startVersionData then
        if PhaseManager:GetTopPhaseId() == PHASE_ID then
            CS.Beyond.Gameplay.Conditions.CheckIsOpenDomainMain.Trigger(self.m_curDomainId)
        end
    else
        UIManager:Open(PanelId.DomainVersionInfoPopup, {
            domainId = self.m_curDomainId,
            startVersionData = startVersionData,
            endVersionData = endVersionData,
            onClose = function()
                CS.Beyond.Gameplay.Conditions.CheckIsOpenDomainMain.Trigger(self.m_curDomainId)
            end
        })
        domainDevelopmentSystem:SendRecordCurVersionInfo(self.m_curDomainId)
    end
end



DomainMainCtrl._ShowBulletinDirect = HL.Method() << function(self)
    self.m_waitShowBulletin = false
    self.view.bulletinNode.gameObject:SetActive(true)
    InputManagerInst:ToggleBinding(self.m_bindIdPreDate, true)
    InputManagerInst:ToggleBinding(self.m_bindIdNextDate, true)
    if DeviceInfo.usingController then
        self.view.domainGradeBtn.enabled = false
        self.view.poiOverviewBtn.enabled = false
        self.view.bulletinBtn.enabled = false
        self.view.changeDomainBtn.interactable = false
        self.view.domainGradeKeyHint.gameObject:SetActive(false)
        self.view.changeDomainKeyHint.gameObject:SetActive(false)
        self.view.poiOverviewKeyHint.gameObject:SetActive(false)


        self:SetNaviTarget(self.view.bulletinNode.dateCell.normalBtn)
    end
end

DomainMainCtrl._ShowBulletin = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isShow, forceSkipRequire)

    if isShow then
        if forceSkipRequire == true then
            self:_ShowBulletinDirect()
            return
        end
        if self.m_waitSyncBulletinData then
            return
        end
        local lastTime = DomainMainCtrl.s_lastBulletinSyncTimestamp
        local nowTime = DateTimeUtils.GetCurrentTimestampBySeconds()
        if lastTime + bulletinRequireTimeInterval >= nowTime then

            self:_UpdateBulletinInfo()
            self:_RefreshBulletinUI()
            self:_ShowBulletinDirect()
        else

            self:_RequireBulletinData()
            self.m_waitShowBulletin = true
        end
    elseif self.view.bulletinNode.gameObject.activeSelf then
        self.m_waitShowBulletin = false
        self.view.bulletinNode.aniWrapper:ClearTween(true)
        self.view.bulletinNode.aniWrapper:PlayOutAnimation(function()
            self.view.bulletinNode.gameObject:SetActive(false)
            InputManagerInst:ToggleBinding(self.m_bindIdPreDate, false)
            InputManagerInst:ToggleBinding(self.m_bindIdNextDate, false)
            if DeviceInfo.usingController then
                self.view.domainGradeBtn.enabled = true
                self.view.poiOverviewBtn.enabled = true
                self.view.bulletinBtn.enabled = true
                self.view.changeDomainBtn.interactable = true
                self.view.domainGradeKeyHint.gameObject:SetActive(true)
                self.view.changeDomainKeyHint.gameObject:SetActive(true)
                self.view.poiOverviewKeyHint.gameObject:SetActive(true)
            end
        end)
    end
end

DomainMainCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState then
        return
    end
    if resumeState.isBulletinShown == true then
        self:_UpdateBulletinInfo()
        self.m_curSelectBulletinIndex = resumeState.selectedBulletinIndex or self.m_curSelectBulletinIndex
        self:_RefreshBulletinUI()
        self:_ShowBulletin(true, true)
    end
    if resumeState.switchRegionPopupState then
        self:_OpenSwitchRegionPopup(resumeState.switchRegionPopupState)
    end
end

DomainMainCtrl._RequireBulletinData = HL.Method() << function(self)
    self.m_waitSyncBulletinData = true
    DomainMainCtrl.s_lastBulletinSyncTimestamp = DateTimeUtils.GetCurrentTimestampBySeconds()
    inventorySystem:SendWalletRecordRequire(self.m_curDomainInfo.moneyId)
end

DomainMainCtrl._OnSyncBulletinData = HL.Method(HL.Any) << function(self, arg)
    self.m_waitSyncBulletinData = false
    local moneyId = unpack(arg)
    logger.info("[OnSyncBulletinData] money id", moneyId)
    if not string.isEmpty(moneyId) then
        if moneyId == self.m_curDomainInfo.moneyId then
            self:_UpdateBulletinInfo()
            self:_RefreshBulletinUI()
        end
    end
    if self.m_waitShowBulletin then
        self:_ShowBulletin(true)
    end
end

DomainMainCtrl._OnSettlementModify = HL.Method(HL.Any) << function(self, arg)
    self:_UpdateSettlementInfo()
    self:_RefreshSettlementCell()
end

DomainMainCtrl.SetNavi = HL.Method(HL.Boolean) << function(self, enable)
    self.m_interactiveLock = not enable
    if enable then
        self:SetNaviTarget(self.view.settlementPOICell.btn)
    else
        self:SetNaviTarget(nil)
    end
end

DomainMainCtrl._OnActivityStageUpdate = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if self.m_stlActivityInfo == nil or self.m_stlActivityInfo.activityId ~= activityId then
        return
    end
    self:UpdateData()
    self:RefreshAllUI()
end

DomainMainCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = {}
    arg.domainId = self.m_curDomainId
    arg.resumeState = {
        isBulletinShown = self.view.bulletinNode.gameObject.activeSelf,
        selectedBulletinIndex = self.m_curSelectBulletinIndex,
    }
    local isSwitchPopupOpen, switchPopupCtrl = UIManager:IsOpen(PanelId.SettlementSwitchRegionPopup)
    if isSwitchPopupOpen then
        arg.resumeState.switchRegionPopupState = switchPopupCtrl:GetCurPhaseStateArg()
    end
    return arg
end


HL.Commit(DomainMainCtrl)
