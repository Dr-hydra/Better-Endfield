
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SubmitCollection

local TOP_NODE_NORMAL_STATE = "Normal"
local TOP_NODE_EMPTY_STATE = "Empty"
local TOP_NODE_COMPLETE_STATE = "Complete"
local ICON_FOLDER = "ItemIcon"
local ICON_MAP_FOLDER = "Inventory"
local ICON_ETHER_FOLDER = "ItemIconBig"

SubmitCollectionCtrl = HL.Class('SubmitCollectionCtrl', uiCtrl.UICtrl)





SubmitCollectionCtrl.s_messages = HL.StaticField(HL.Table) << {
     [MessageConst.ON_SUBMIT_ETHER_SUCC] = 'OnSubmitEtherSucc',
}

SubmitCollectionCtrl.m_curLevelCellIndex = HL.Field(HL.Number) << -1

SubmitCollectionCtrl.m_mapId = HL.Field(HL.String) << ""

SubmitCollectionCtrl.m_maxLv = HL.Field(HL.Number) << 0

SubmitCollectionCtrl.m_getLvCellFunc = HL.Field(HL.Function)

SubmitCollectionCtrl.m_oldLv = HL.Field(HL.Number) << -1

SubmitCollectionCtrl.m_redDotOldLv = HL.Field(HL.Number) << -1

SubmitCollectionCtrl.m_redDotNewLv = HL.Field(HL.Number) << -1

SubmitCollectionCtrl.m_buffCellCache = HL.Field(HL.Forward("UIListCache"))

SubmitCollectionCtrl.m_maxDashCount = HL.Field(HL.Number) << -1



SubmitCollectionCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_maxDashCount = GameInstance.playerController.maxDashCount
    self:UpdateDomainInfo()
    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.SubmitCollection)
    end)
    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "collection_submit")
    end)
    self.view.submitBtn.onClick:AddListener(function()
        self:_OnClickSubmit()
    end)

    self.m_buffCellCache = UIUtils.genCellCache(self.view.buffCell)
    self.m_getLvCellFunc = UIUtils.genCachedCellFunction(self.view.lvScrollList)
    self.view.lvScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getLvCellFunc(obj)
        self:_OnRefreshLvCell(cell, csIndex)
    end)

    self.view.lvScrollListScrollRect.enabled = false
    self.view.lvScrollList.onGraduallyShowFinish:AddListener(function()
        self.view.lvScrollListScrollRect.enabled = true
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.m_maxLv = GameInstance.player.inventory:CurSubmitEtherMaxLv()
    self.m_curLevelCellIndex = math.min(GameInstance.player.inventory:CurEtherLevel(), self.m_maxLv - 1)
    self:_RefreshLvInfo(true)
    self:_StartCoroutine(function()
        if GameInstance.player.inventory:CurEtherLevel() == self.m_maxLv then
            return
        end

        while not self:_GetCurLevelCell() do
            coroutine.step()
        end
        self:_DoCurLevelCellProgressSliderTween(0)
    end)

    self.view.detailsBtn.onClick:AddListener(function()
        self:OnClickDetailsBtn()
    end)
    self.m_redDotOldLv = GameInstance.player.inventory:CurEtherLevel()
    self.m_redDotNewLv = GameInstance.player.inventory:CurEtherLevel()
    self.view.detailsBtnRedDot.gameObject:SetActive(false)
    self:UpdateBuffShow()
    if GameInstance.player.inventory:CurEtherCount() > 0 then
        AudioAdapter.PostEvent("Au_UI_Menu_SubmitEtherPanel_Open")
    else
        AudioAdapter.PostEvent("Au_UI_Menu_SubmitCollectionPanel_Open")
    end
end

SubmitCollectionCtrl.UpdateBuffShow = HL.Method() << function(self)
    local curLevelBuffList = self:GetEffectBuffList(self.m_mapId, self.m_redDotNewLv)
    local maxLevelBuffList = self:GetEffectBuffList(self.m_mapId, self.m_maxLv)

    if maxLevelBuffList == nil or maxLevelBuffList.Count == 0 then
        self.view.areaBuffNode.gameObject:SetActive(false)
        return
    end
    self.view.areaBuffNode.gameObject:SetActive(true)

    self.m_buffCellCache:Refresh(maxLevelBuffList.Count, function(cell, luaIndex)
        if curLevelBuffList == nil or luaIndex > curLevelBuffList.Count then
            cell.buffEmptyImg.gameObject:SetActiveIfNecessary(true)
            cell.buffIconImg.gameObject:SetActiveIfNecessary(false)
            cell.buffLevelText.gameObject:SetActiveIfNecessary(false)
            cell.buffLevelLayout.gameObject:SetActiveIfNecessary(false)
        else
            local buffName = curLevelBuffList[CSIndex(luaIndex)]
            local data = Tables.etherSubmitBuffShowTable[buffName]
            cell.buffEmptyImg.gameObject:SetActiveIfNecessary(false)
            cell.buffIconImg.gameObject:SetActiveIfNecessary(true)
            cell.buffLevelLayout.gameObject:SetActiveIfNecessary(true)
            cell.buffLevelText.gameObject:SetActiveIfNecessary(true)
            if data then
                cell.buffIconImg:LoadSprite(ICON_FOLDER, data.effectSubIcon)
                cell.buffLevelText.text = data.effectFlagLevel
            end
        end
    end)
end

SubmitCollectionCtrl.OnClickDetailsBtn = HL.Method() << function(self)
    PhaseManager:OpenPhase(PhaseId.AreaBuffPopup,{
        domainId = self.m_mapId,
        lastLevel = self.m_redDotOldLv,
        curLevel = self.m_redDotNewLv,
        lastLevelBuffList = self:GetEffectBuffList(self.m_mapId, self.m_redDotOldLv),
        curLevelBuffList = self:GetEffectBuffList(self.m_mapId, self.m_redDotNewLv),
    })

    self.m_redDotOldLv = self.m_redDotNewLv
    self:UpdateRedDot()
end

SubmitCollectionCtrl.UpdateDomainInfo = HL.Method() << function(self)
    self.m_mapId = GameUtil.GetSystemMapIdByLevelId(GameWorld.worldInfo.curLevelId)

    local domainSuccess, mapData = Tables.MapIdTable:TryGetValue(self.m_mapId)
    if mapData then
        self.view.areaNameTxt.text = mapData.showName
    end

    local succ, domainUiData = Tables.etherSubmitDomainShowTable:TryGetValue(self.m_mapId)
    if domainUiData then
        self.view.areaIcon:LoadSprite(ICON_MAP_FOLDER, domainUiData.domainIcon)
        self.view.icon:LoadSprite(ICON_ETHER_FOLDER, domainUiData.itemIcon)
        self.view.areaIconImg:LoadSprite(ICON_MAP_FOLDER, domainUiData.domainIcon)
    end
end

SubmitCollectionCtrl.OnAnimationInFinished = HL.Override() << function(self)
    if DeviceInfo.usingController then
        local csIndex = self.m_curLevelCellIndex
        local obj = self.view.lvScrollList:Get(csIndex)
        local cell = self.m_getLvCellFunc(obj)
        if cell ~= nil then
            self:SetNaviTarget(cell.view.submitCollectionLevelCell)
        end
    end
end

SubmitCollectionCtrl.ShowSubmitEther = HL.StaticMethod(HL.Opt(HL.Table)) << function(args)
    PhaseManager:OpenPhase(PhaseId.SubmitCollection)
end

SubmitCollectionCtrl._RefreshLvInfo = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    self:_RefreshContent(isInit)

    local csIndex = self.m_curLevelCellIndex
    self.view.lvScrollList:UpdateCount(
        self.m_maxLv, csIndex, false, false, not isInit, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)

    if not isInit and DeviceInfo.usingController then
        local obj = self.view.lvScrollList:Get(csIndex)
        local cell = self.m_getLvCellFunc(obj)
        self:SetNaviTarget(cell.view.submitCollectionLevelCell)
    end
end

SubmitCollectionCtrl._OnRefreshLvCell = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)
    cell:InitSubmitCollectionLevelCell(self.view, csIndex)
    if csIndex == self.m_maxLv - 1 and GameInstance.player.inventory:CurEtherLevel() == self.m_maxLv then

        cell:SetMaxLevel()
    end
end

SubmitCollectionCtrl._RefreshContent = HL.Method(HL.Boolean) << function(self, isInit)
    local curLv = GameInstance.player.inventory:CurEtherLevel()
    local isMax = curLv == self.m_maxLv
    local curEtherCount = GameInstance.player.inventory:CurEtherCount()
    local isNullEther = curEtherCount == 0

    self.view.curCountTxt2.text = curEtherCount

    local state
    if isMax then
        state = TOP_NODE_COMPLETE_STATE
    else
        state = isNullEther and TOP_NODE_EMPTY_STATE or TOP_NODE_NORMAL_STATE
    end
    self.view.topNode:SetState(state)
end


SubmitCollectionCtrl._OnClickSubmit = HL.Method() << function(self)
    self.m_oldLv = GameInstance.player.inventory:CurEtherLevel()
    GameInstance.player.inventory:SubmitEther(self.m_mapId)
end

SubmitCollectionCtrl._DoCurLevelCellProgressSliderTween = HL.Method(HL.Number) << function(self, startValue)

    local cell = self:_GetCurLevelCell()
    cell:DoProgressSliderTween(startValue)
end

SubmitCollectionCtrl._GetCurLevelCell = HL.Method().Return(HL.Forward("SubmitCollectionLevelCell")) << function(self)
    local csIndex = self.m_curLevelCellIndex
    local obj = self.view.lvScrollList:Get(csIndex)

    local cell = self.m_getLvCellFunc(obj)
    return cell
end

SubmitCollectionCtrl.OnSubmitEtherSucc = HL.Method() << function(self)
    self.m_redDotOldLv = self.m_redDotNewLv
    self.m_redDotNewLv = GameInstance.player.inventory:CurEtherLevel()
    self:UpdateRedDot()
    self:UpdateBuffShow()

    self.m_curLevelCellIndex = math.min(GameInstance.player.inventory:CurEtherLevel(), self.m_maxLv - 1)
    local currentDashCount = GameInstance.playerController.maxDashCount
    local maxDashCount = Tables.globalConst.maxDashEnergyLimit / Tables.globalConst.dashCostEnergyValue
    local maxShowCount = maxDashCount - self.m_maxDashCount

    local items = {}
    local isEmpty = true
    for i = self.m_oldLv + 1, GameInstance.player.inventory:CurEtherLevel() do
        local rewardID = GameInstance.player.inventory:CurSubmitEtherRewardID(i)
        local rewardData = Tables.rewardTable[rewardID]
        for _, itemBundle in pairs(rewardData.itemBundles) do
            if itemBundle.id == "item_add_endurance" then
                if self.m_maxDashCount < maxDashCount and maxShowCount > 0 then
                    maxShowCount = maxShowCount - 1
                    table.insert(items, itemBundle)
                end
            else
                table.insert(items, itemBundle)
            end
            isEmpty = false
        end
    end

    local cell = self:_GetCurLevelCell()
    if cell then
        local levelCellSlider = cell:GetProgressSlider()
        local preSliderValue = levelCellSlider.value

        levelCellSlider.value = 0
        if not isEmpty then
            Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
                title = Language.LUA_SUBMIT_COLLECTION_REWARD_TITLE,
                items = items,
                onComplete = function()
                    self:_DoCurLevelCellProgressSliderTween(0)
                end
            })
        else
            self:_DoCurLevelCellProgressSliderTween(preSliderValue)
        end
    else
        if not isEmpty then
            Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
                title = Language.LUA_SUBMIT_COLLECTION_REWARD_TITLE,
                items = items,
                onComplete = function()
                    self.view.lvScrollList:ScrollToIndex(self.m_curLevelCellIndex)
                end
            })
        else
            self.view.lvScrollList:ScrollToIndex(self.m_curLevelCellIndex)
        end
    end
    self:_RefreshLvInfo(false)

    if self.m_maxDashCount < maxDashCount and currentDashCount >= maxDashCount then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SUBMIT_ETHER_STAMINA_MAX_TOAST)
    end
    self.m_maxDashCount = currentDashCount
end

SubmitCollectionCtrl.UpdateRedDot = HL.Method() << function(self)
    local lastLevelBuffList = self:GetEffectBuffList(self.m_mapId, self.m_redDotOldLv)
    local curLevelBuffList = self:GetEffectBuffList(self.m_mapId, self.m_redDotNewLv)
    local haveNewBuff = false
    if curLevelBuffList and self.m_redDotNewLv > self.m_redDotOldLv then
        for curKey, curBuff in pairs(curLevelBuffList) do
            local isHave = false
            local curBuffInfo = Tables.etherSubmitBuffShowTable[curBuff]
            if curBuffInfo ~= nil then
                if lastLevelBuffList then
                    for lastKey, lastBuff in pairs(lastLevelBuffList) do
                        local lastBuffInfo = Tables.etherSubmitBuffShowTable[lastBuff]
                        if lastBuffInfo ~= nil then
                            if lastBuffInfo.effectFlagType == curBuffInfo.effectFlagType then
                                isHave = true
                                if curBuffInfo.effectFlagLevel > lastBuffInfo.effectFlagLevel then
                                    haveNewBuff = true
                                end
                            end
                        end
                    end
                end
                if not isHave then
                    haveNewBuff = true
                end
            end
        end
    end

    if haveNewBuff then
        self.view.detailsBtnRedDot.gameObject:SetActive(true)
    else
        self.view.detailsBtnRedDot.gameObject:SetActive(false)
    end
end

SubmitCollectionCtrl.GetEffectBuffList = HL.Method(HL.String, HL.Number).Return(HL.Any) << function(self, domainId, level)
    for key, value in pairs(Tables.etherSubmitInfoTable) do
        if value.domainId == domainId and value.level == level then
            return value.effectList
        end
    end
    return nil
end


HL.Commit(SubmitCollectionCtrl)
