local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WorldLevelPopup

WorldLevelPopupCtrl = HL.Class('WorldLevelPopupCtrl', uiCtrl.UICtrl)

WorldLevelPopupCtrl.m_targetWorldLevel = HL.Field(HL.Number) << 0

WorldLevelPopupCtrl.m_isUp = HL.Field(HL.Boolean) << false

WorldLevelPopupCtrl.m_genTipCells = HL.Field(HL.Forward("UIListCache"))


WorldLevelPopupCtrl.m_textKeyTable = HL.Field(HL.Userdata)





WorldLevelPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


WorldLevelPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    arg = arg or {}

    self.view.closeBtn.onClick:RemoveAllListeners()
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self.m_phase:BackToTips()
        end)
    end)

    self.view.bg.onClick:RemoveAllListeners()
    self.view.bg.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self.m_phase:BackToTips()
        end)
    end)

    self.m_isUp = arg.isUp or false
    local currentWorldLevel = GameInstance.player.adventure.currentWorldLevel
    local maxWorldLevel = GameInstance.player.adventure.currentMaxWorldLevel

    if self.m_isUp then
        self.m_targetWorldLevel = maxWorldLevel
    else
        local defaultTargetWorldLevel = currentWorldLevel - 1
        local restoreTargetWorldLevel = tonumber(arg.targetWorldLevel)
        if restoreTargetWorldLevel then
            self.m_targetWorldLevel = math.max(1, math.min(restoreTargetWorldLevel, defaultTargetWorldLevel))
        else
            self.m_targetWorldLevel = defaultTargetWorldLevel
        end
    end

    self.view.exploreLvTxt.text = string.format("%02d", currentWorldLevel)

    self.view.effectTipsTxt:SetAndResolveTextStyle(string.format(self.m_isUp and Language.LUA_WORLD_LEVEL_UP_TIP or Language.LUA_WORLD_LEVEL_DOWN_TIP, self.m_targetWorldLevel))

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.m_textKeyTable = GameInstance.player.adventure:GetTextIdList(self.m_targetWorldLevel, currentWorldLevel)

    self.m_genTipCells = UIUtils.genCellCache(self.view.effectTxtCell)
    self.m_genTipCells:Refresh(self.m_textKeyTable.Count, function(cell, luaIndex)
        local textId = self.m_textKeyTable[CSIndex(luaIndex)]
        cell.text.text = Language[textId]
    end)


    local setWorldLevelIntervalTime = Tables.globalConst.setWorldLevelIntervalTime
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local time = setWorldLevelIntervalTime - (curServerTime - GameInstance.player.adventure.lastSetWorldLevelTS)
    if (not self.m_isUp) and GameInstance.player.adventure.lastSetWorldLevelTS > 0 and time > 0 then
        self.view.buttonOperateNode:SetState("Tips")
        self.view.timeTxt.text = string.format(Language.LUA_WORLD_LEVEL_TIME_TIP, math.floor(time / 3600), math.floor((time - math.floor(time / 3600) * 3600) / 60))
    else
        self.view.buttonOperateNode:SetState(self.m_isUp and "Up" or "Down")
    end

    self.view.confirmBtn.onClick:RemoveAllListeners()
    self.view.confirmBtn.onClick:AddListener(function()
        GameInstance.player.adventure:SetWorldLevel(self.m_targetWorldLevel)
        self.m_phase:CloseSelf()

    end)

    self.view.restoreBtn.onClick:RemoveAllListeners()
    self.view.restoreBtn.onClick:AddListener(function()
        GameInstance.player.adventure:SetWorldLevel(self.m_targetWorldLevel)
        self.m_phase:CloseSelf()

    end)

    self.view.exploreLvNode.onClosestCellChanged:RemoveAllListeners()
    self.view.exploreLvNode.onClosestCellChanged:AddListener(function(csIndex)
        self.m_targetWorldLevel = self.m_isUp and (currentWorldLevel + LuaIndex(csIndex)) or (LuaIndex(csIndex))
        self.m_textKeyTable = GameInstance.player.adventure:GetTextIdList(self.m_targetWorldLevel , currentWorldLevel)
        self.m_genTipCells:Refresh(self.m_textKeyTable.Count, function(cell, luaIndex)
            local textId = self.m_textKeyTable[CSIndex(luaIndex)]
            cell.text.text = Language[textId]
        end)
        self.view.effectTipsTxt:SetAndResolveTextStyle(string.format(self.m_isUp and Language.LUA_WORLD_LEVEL_UP_TIP or Language.LUA_WORLD_LEVEL_DOWN_TIP, self.m_targetWorldLevel))
    end)

    self.view.exploreLvNode.onCellShow:RemoveAllListeners()
    self.view.exploreLvNode.onCellShow:AddListener(function(gameObject, csIndex)
        local text = gameObject:GetComponent("UIText")



        text.text = string.format("%02d", self.m_isUp and maxWorldLevel or (LuaIndex(csIndex)))
        if csIndex == 0 and gameObject.transform.childCount > 0 then

            gameObject.transform:GetChild(0).gameObject:SetActiveIfNecessary(false)
        end
    end)

    local count
    local initialIndex
    if self.m_isUp then
        count = 1
        initialIndex = 0
    else
        count = currentWorldLevel - 1
        initialIndex = math.max(0, self.m_targetWorldLevel - 1)
    end

    self.view.exploreLvNode:RefreshLayout(count, initialIndex)
end

WorldLevelPopupCtrl.GetPopupState = HL.Method().Return(HL.Table) << function(self)
    return {
        isUp = self.m_isUp,
        targetWorldLevel = self.m_targetWorldLevel,
    }
end

WorldLevelPopupCtrl._UpdateTipText = HL.Method() << function(self)

    local changeTipInfos = {}
    local count = 0
    self.m_textKeyTable = {}
    local currentWorldLevel = GameInstance.player.adventure.currentWorldLevel
    local maxWorldLevel = GameInstance.player.adventure.currentMaxWorldLevel
    if self.m_isUp then
        for i = currentWorldLevel, maxWorldLevel do
            local success, cfg = Tables.adventureWorldLevelTable:TryGetValue(i)
            if success and cfg then
                for _, textId in pairs(cfg.levelUpTipTextIds) do
                    if not changeTipInfos[textId] then
                        changeTipInfos[textId] = true
                        count = count + 1
                        self.m_textKeyTable[count] = textId
                    end
                end
            else
                logger.error("WorldLevelPopupCtrl.OnCreate: Failed to get adventure world level config for level " .. i)
            end
        end
    else
        for i = self.m_targetWorldLevel, currentWorldLevel do
            local success, cfg = Tables.adventureWorldLevelTable:TryGetValue(i)
            if success and cfg then
                for _, textId in pairs(cfg.levelDownTipTextIds) do
                    if not changeTipInfos[textId] then
                        changeTipInfos[textId] = true
                        count = count + 1
                        self.m_textKeyTable[count] = textId
                    end
                end
            else
                logger.error("WorldLevelPopupCtrl.OnCreate: Failed to get adventure world level config for level " .. i)
            end
        end
    end
end











HL.Commit(WorldLevelPopupCtrl)
