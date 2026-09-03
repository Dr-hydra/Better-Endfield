local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SeasonTowerAchieve
local PHASE_ID = PhaseId.SeasonTowerAchieve

SeasonTowerAchieveCtrl = HL.Class('SeasonTowerAchieveCtrl', uiCtrl.UICtrl)

SeasonTowerAchieveCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SEASON_TOWER_ACHIEVE_UPDATE] = '_OnAchieveUpdate',
}

SeasonTowerAchieveCtrl.m_system = HL.Field(HL.Userdata)

SeasonTowerAchieveCtrl.m_allGameGroupIds = HL.Field(HL.Table)

SeasonTowerAchieveCtrl.m_selectedIndex = HL.Field(HL.Number) << 0

SeasonTowerAchieveCtrl.m_getCell = HL.Field(HL.Any)

SeasonTowerAchieveCtrl.m_challengeCellCache = HL.Field(HL.Forward("UIListCache"))

SeasonTowerAchieveCtrl.m_claimBindingId = HL.Field(HL.Number) << 0

local MEDAL_STATES = { [1] = "BronzeMedal", [2] = "SilverMedal", [3] = "GoldMedal" }
local function _GetMedalState(starNum)
    return MEDAL_STATES[starNum] or MEDAL_STATES[3]
end


SeasonTowerAchieveCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_system = GameInstance.player.seasonTowerSystem
    self.m_allGameGroupIds = {}

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)


    self.m_claimBindingId = InputManagerInst:CreateBindingByActionId("seasontower_achieve_claim", function()
        self:_OnClickClaimReward()
    end, self.view.inputGroup.groupId)

    self.m_challengeCellCache = UIUtils.genCellCache(self.view.levelChallengeCell)

    self:_BuildGameGroupList()

    self.m_getCell = UIUtils.genCachedCellFunction(self.view.levelScrollList)
    self.view.levelScrollList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateCell(self.m_getCell(object), LuaIndex(csIndex))
    end)
    self.view.levelScrollList.onSelectedCell:AddListener(function(object, csIndex)
        local cell = self.m_getCell(object)
        if cell then
            self:_OnSelectCell(cell, LuaIndex(csIndex))
        end
    end)

    self.view.levelScrollList:UpdateCount(#self.m_allGameGroupIds, false, false, false, true)

    local restoreIndex = 0
    if arg and arg.selectedGameGroupId then
        for i, id in ipairs(self.m_allGameGroupIds) do
            if id == arg.selectedGameGroupId then
                restoreIndex = i - 1
                break
            end
        end
    end

    if #self.m_allGameGroupIds > 0 then
        self.view.levelScrollList:SetSelectedIndex(restoreIndex, true, true)
        if DeviceInfo.usingController then
            self:SetNaviTarget(self.m_getCell(restoreIndex+1).button)
        end
    end

    if DeviceInfo.usingController then
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    end
end

SeasonTowerAchieveCtrl.OnShow = HL.Override() << function(self)
end

SeasonTowerAchieveCtrl._BuildGameGroupList = HL.Method() << function(self)
    self.m_allGameGroupIds = {}
    local sortValues = {}

    for gameGroupId, _ in pairs(Tables.seasonTowerGameGroupTable) do
        table.insert(self.m_allGameGroupIds, gameGroupId)
        local res, achieveInfo = self.m_system.levelAchieve:TryGetValue(gameGroupId)
        if not res then
            sortValues[gameGroupId] = 2
        elseif achieveInfo:HasReward() then
            sortValues[gameGroupId] = 0
        else
            sortValues[gameGroupId] = 1
        end
    end

    table.sort(self.m_allGameGroupIds, function(a, b)
        if sortValues[a] ~= sortValues[b] then
            return sortValues[a] < sortValues[b]
        end
        return a < b
    end)
end

SeasonTowerAchieveCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local gameGroupId = self.m_allGameGroupIds[luaIndex]
    if not gameGroupId then return end

    local groupCfg = Tables.gameMechanicGroupTable[gameGroupId]
    local levelCfg = Tables.seasonTowerGameGroupTable[gameGroupId]
    local res, achieveInfo = self.m_system.levelAchieve:TryGetValue(gameGroupId)

    cell.txtLevel.text = groupCfg.gameGroupName
    cell.redDot:InitRedDot("SeasonTowerAchieveSingle", gameGroupId)
    cell.iconImg:LoadSprite(UIConst.UI_SPRITE_SEASONTOWER, levelCfg.icon)

    if res and achieveInfo.bestStarNum > 0 then
        cell.stateController:SetState(_GetMedalState(achieveInfo.bestStarNum))
        cell.stateController:SetState(luaIndex == self.m_selectedIndex and "Select" or "Normal")
        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            self.view.levelScrollList:SetSelectedIndex(CSIndex(luaIndex), true)
        end)
        cell.button.interactable = true
    else
        cell.stateController:SetState("EmptyState")
        cell.button.interactable = false
    end
end

SeasonTowerAchieveCtrl._OnSelectCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local oldCell = self.m_getCell(self.m_selectedIndex)
    if oldCell and oldCell.stateController then
        oldCell.stateController:SetState("Normal")
    end

    self.m_selectedIndex = luaIndex

    if cell.stateController then
        cell.stateController:SetState("Select")
    end

    self:_RefreshRightPanel()
end

SeasonTowerAchieveCtrl._RefreshRightPanel = HL.Method() << function(self)
    if self.m_selectedIndex <= 0 or self.m_selectedIndex > #self.m_allGameGroupIds then
        return
    end

    local gameGroupId = self.m_allGameGroupIds[self.m_selectedIndex]
    local groupCfg = Tables.gameMechanicGroupTable[gameGroupId]
    local res, achieveInfo = self.m_system.levelAchieve:TryGetValue(gameGroupId)

    self.view.titleTxt.text = groupCfg.gameGroupName
    self.view.descText.text = Tables.seasonTowerGameGroupTable[gameGroupId].desc

    local starCount = #MEDAL_STATES
    self.m_challengeCellCache:Refresh(starCount, function(cell, starIndex)
        self:_OnUpdateChallengeCell(cell, starIndex, gameGroupId, res and achieveInfo or nil)
    end)

    InputManagerInst:ToggleBinding(self.m_claimBindingId, res and achieveInfo:HasReward() or false)

    if res and achieveInfo.completeTime > 0 then
        self.view.completeTimeNode.gameObject:SetActive(true)
        self.view.completeTimeText.text = string.format(Language.LUA_SEASON_TOWER_ACHIEVE_COMPLETE_TIME, Utils.timestampToDateYMD(achieveInfo.completeTime))
    else
        self.view.completeTimeNode.gameObject:SetActive(false)
    end
end

SeasonTowerAchieveCtrl._OnUpdateChallengeCell = HL.Method(HL.Any, HL.Number, HL.String, HL.Opt(HL.Userdata)) << function(self, cell, starIndex, gameGroupId, achieveInfo)
    local achieveCfgRes, achieveCfg = Tables.seasonTowerGameGroupTable:TryGetValue(gameGroupId)
    local starData = nil
    if achieveCfgRes then
        local starRes, data = achieveCfg.stars:TryGetValue(starIndex)
        if starRes then
            starData = data
        end
    end

    cell.txtTitle.text = string.format(Language.LUA_SEASON_TOWER_ACHIEVE_REWARD_DESC, starIndex)
    cell.stateController:SetState(_GetMedalState(starIndex))

    local hasReward = achieveInfo and starIndex <= achieveInfo.bestStarNum and achieveInfo.rewardedStarNum < starIndex
    local rewardItems = { cell.itemReward01, cell.itemReward02 }
    if starData then
        local rewardBundles = UIUtils.getRewardItems(starData.rewardId)
        for i, itemView in ipairs(rewardItems) do
            if i <= #rewardBundles then
                if hasReward and not DeviceInfo.usingController then
                    itemView:InitItem(rewardBundles[i], function()
                        self:_OnClickClaimReward()
                    end)
                else
                    itemView:InitItem(rewardBundles[i], true)
                end
                itemView:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
                if DeviceInfo.usingController then
                    itemView:SetEnableHoverTips(false)
                end
            else
                itemView:InitItem(nil)
                itemView.gameObject:SetActive(false)
            end
        end
    else
        for _, itemView in ipairs(rewardItems) do
            itemView:InitItem(nil)
            itemView.gameObject:SetActive(false)
        end
    end

    if achieveInfo and starIndex <= achieveInfo.bestStarNum then
        if achieveInfo.rewardedStarNum < starIndex then
            cell.stateController:SetState("Completed")
            cell.redDot.gameObject:SetActive(true)
        else
            cell.stateController:SetState("Claimed")
            cell.redDot.gameObject:SetActive(false)
        end
    else
        cell.stateController:SetState("Normal")
        cell.redDot.gameObject:SetActive(false)
    end

    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_OnClickClaimReward()
    end)
end

SeasonTowerAchieveCtrl._OnClickClaimReward = HL.Method() << function(self)
    if self.m_selectedIndex <= 0 or self.m_selectedIndex > #self.m_allGameGroupIds then
        return
    end

    local gameGroupId = self.m_allGameGroupIds[self.m_selectedIndex]
    local res, achieveInfo = self.m_system.levelAchieve:TryGetValue(gameGroupId)
    if not res or not achieveInfo:HasReward() then
        return
    end

    self.m_system:GetAchieveReward(gameGroupId)
end

SeasonTowerAchieveCtrl._OnAchieveUpdate = HL.Method(HL.Any) << function(self, arg)
    local updatedGameGroupId = unpack(arg)

    local prevSelectedId = nil
    if self.m_selectedIndex > 0 and self.m_selectedIndex <= #self.m_allGameGroupIds then
        prevSelectedId = self.m_allGameGroupIds[self.m_selectedIndex]
    end


    self.view.levelScrollList:UpdateCount(#self.m_allGameGroupIds, false, false, false, true)

    if prevSelectedId then
        for i, id in ipairs(self.m_allGameGroupIds) do
            if id == prevSelectedId then
                self.view.levelScrollList:SetSelectedIndex(i - 1, true, true)
                return
            end
        end
    end

    if #self.m_allGameGroupIds > 0 then
        self.view.levelScrollList:SetSelectedIndex(0, true, true)
    end
end

SeasonTowerAchieveCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if self.m_selectedIndex > 0 and self.m_selectedIndex <= #self.m_allGameGroupIds then
        return { selectedGameGroupId = self.m_allGameGroupIds[self.m_selectedIndex] }
    end
end

HL.Commit(SeasonTowerAchieveCtrl)
