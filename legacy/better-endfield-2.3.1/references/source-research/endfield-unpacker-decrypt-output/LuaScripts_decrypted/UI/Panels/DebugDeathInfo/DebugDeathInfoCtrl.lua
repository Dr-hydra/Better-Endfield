local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DebugDeathInfo
local DisplayMode = CS.Beyond.Gameplay.Core.DeathPerformance.CommonDeathPanelDisplayMode










local DISPLAY_MODE_OPTIONS = {
    { mode = DisplayMode.WorldDeath, label = "WorldDeath 世界死亡" },
    { mode = DisplayMode.DungeonFail, label = "DungeonFail 副本失败" },
    { mode = DisplayMode.Miasma, label = "Miasma 迷雾死亡" },
}

DebugDeathInfoCtrl = HL.Class('DebugDeathInfoCtrl', uiCtrl.UICtrl)


DebugDeathInfoCtrl.s_messages = HL.StaticField(HL.Table) << {
}

DebugDeathInfoCtrl.m_selectedModeCsIndex = HL.Field(HL.Number) << 0



local function trimOrNil(s)
    if not s or string.isEmpty(string.trim(s)) then
        return nil
    end
    return string.trim(s)
end



local function buildDefaultDeathInfoTable()
    local d = {
        dungeonId = nil,
        enemyId = nil,
        enemyLv = -1,
        squadLvSum = 0,
        squadWeaponLvSum = 0,
        squadSkillLvSum = 0,
        squadEquipLvSum = 0,
    }

    pcall(function()
        if not GameInstance or not GameInstance.player then
            return
        end

        if GameInstance.dungeonManager and GameInstance.dungeonManager.inDungeon then
            d.dungeonId = GameInstance.dungeonManager.curDungeonId
        end

        local squadManager = GameInstance.player.squadManager
        local charBag = GameInstance.player.charBag
        if squadManager and squadManager.curSquad and charBag then
            local currentSquad = squadManager.curSquad
            local count = currentSquad.slots.Count
            for i = 1, count do
                local slot = currentSquad.slots[CSIndex(i - 1)]
                local charInstId = slot.charInstId
                local charInfo = charBag:GetCharInfo(charInstId)
                if charInfo then
                    d.squadLvSum = d.squadLvSum + charInfo.level
                    local weaponInfo = CharInfoUtils.getWeaponInstInfo(charInfo.weaponInstId)
                    if weaponInfo and weaponInfo.weaponInst then
                        d.squadWeaponLvSum = d.squadWeaponLvSum + weaponInfo.weaponInst.weaponLv
                    end
                    for _, skillInfo in pairs(charInfo.skillGroupLevelInfoList) do
                        d.squadSkillLvSum = d.squadSkillLvSum + skillInfo.level
                    end
                    for _, equipInstId in pairs(charInfo.equipCol) do
                        local equipInstanceData = CharInfoUtils.getEquipByInstId(equipInstId)
                        if equipInstanceData then
                            local equipTemplateId = equipInstanceData.templateId
                            local equipData = Tables.equipTable[equipTemplateId]
                            if equipData then
                                d.squadEquipLvSum = d.squadEquipLvSum + equipData.minWearLv
                            end
                        end
                    end
                end
            end
        end

        local squadManager2 = GameInstance.player.squadManager
        local isKilled = squadManager2 and squadManager2:IsAllDead()
        if isKilled and GameWorld and GameWorld.battle then
            local battleDeathInfo = GameWorld.battle:GetDieSnapShot()
            if battleDeathInfo and battleDeathInfo.killer then
                d.enemyId = battleDeathInfo.killer.enemyId
                d.enemyLv = battleDeathInfo.killer.level
            end
        end
    end)

    return d
end

DebugDeathInfoCtrl._FillFieldsFromDeathInfo = HL.Method(HL.Table) << function(self, d)
    self.view.deathDungeonId.text = d.dungeonId or ""
    self.view.deathEnemyId.text = d.enemyId or ""
    self.view.deathEnemyLv.text = tostring(d.enemyLv)
    self.view.deathSquadLvSum.text = tostring(d.squadLvSum)
    self.view.deathSquadWeaponLvSum.text = tostring(d.squadWeaponLvSum)
    self.view.deathSquadSkillLvSum.text = tostring(d.squadSkillLvSum)
    self.view.deathSquadEquipLvSum.text = tostring(d.squadEquipLvSum)
end

DebugDeathInfoCtrl._ReadDeathInfoFromFields = HL.Method().Return(HL.Table) << function(self)
    return {
        dungeonId = trimOrNil(self.view.deathDungeonId.text),
        enemyId = trimOrNil(self.view.deathEnemyId.text),
        enemyLv = tonumber(self.view.deathEnemyLv.text) or -1,
        squadLvSum = tonumber(self.view.deathSquadLvSum.text) or 0,
        squadWeaponLvSum = tonumber(self.view.deathSquadWeaponLvSum.text) or 0,
        squadSkillLvSum = tonumber(self.view.deathSquadSkillLvSum.text) or 0,
        squadEquipLvSum = tonumber(self.view.deathSquadEquipLvSum.text) or 0,
    }
end



DebugDeathInfoCtrl._GetTipGroupAndMode = HL.Method().Return(HL.String, HL.Userdata, HL.Number) << function(self)
    local deathInfo = self:_ReadDeathInfoFromFields()
    local displayMode = DISPLAY_MODE_OPTIONS[self.m_selectedModeCsIndex + 1].mode

    if displayMode == DisplayMode.Miasma then
        if Tables.miasmaDeathTips and #Tables.miasmaDeathTips > 0 then
            return "miasma", Tables.miasmaDeathTips, 0
        end
        return "miasma->common", Tables.commonDeathTips, 0
    end

    if displayMode == DisplayMode.DungeonFail then
        if deathInfo.dungeonId then
            local _, tipGroupBean = Tables.dungeonDeathTips:TryGetValue(deathInfo.dungeonId)
            if tipGroupBean and tipGroupBean.tipContents and #tipGroupBean.tipContents > 0 then
                return "dungeon", tipGroupBean.tipContents, -1
            end
        end
        if deathInfo.enemyId and deathInfo.enemyLv >= 0 then
            local _, tipGroupBean = Tables.enemyRelatedDeathTips:TryGetValue(deathInfo.enemyId)
            if tipGroupBean and tipGroupBean.tipContents and #tipGroupBean.tipContents > 0 then
                return "enemy", tipGroupBean.tipContents, -1
            end
        end
        return "common", Tables.commonDeathTips, 0
    end


    if deathInfo.enemyId and deathInfo.enemyLv >= 0 then
        local _, tipGroupBean = Tables.enemyRelatedDeathTips:TryGetValue(deathInfo.enemyId)
        if tipGroupBean and tipGroupBean.tipContents and #tipGroupBean.tipContents > 0 then
            return "enemy", tipGroupBean.tipContents, -1
        end
    end
    return "common", Tables.commonDeathTips, 0
end


DebugDeathInfoCtrl._AppendTrainingPreview = HL.Method(HL.Table, HL.Table) << function(self, deathInfo, lines)
    lines[#lines + 1] = "=== Training Tips（与 DeathInfoCtrl 同序，仅第一条会显示）==="
    local _, trainingStd = Tables.recommendTraining:TryGetValue(deathInfo.enemyLv)
    if not trainingStd then
        lines[#lines + 1] = "[Training] enemyLv=" .. tostring(deathInfo.enemyLv) .. " 无 recommendTraining 配置"
        return
    end

    local checkTypeInOrder = {}
    for _, trainingTypeInfo in pairs(Cfg.Tables.trainingTypeInfoTable) do
        checkTypeInOrder[trainingTypeInfo.priority] = trainingTypeInfo
    end

    local shown = false
    for priority = 1, #checkTypeInOrder do
        local trainingTypeInfo = checkTypeInOrder[priority]
        local trainingType = trainingTypeInfo.trainingType
        local stdVal = trainingStd[trainingType]
        if stdVal and stdVal > 0 then
            local actual = deathInfo[trainingType]
            local degree = actual / stdVal
            local tipGroupWrapper = Tables.trainingDeathTips[trainingType]
            local tipCount = 0
            if tipGroupWrapper and tipGroupWrapper.tipContents then
                tipCount = #tipGroupWrapper.tipContents
            end
            local wouldShow = degree < trainingTypeInfo.trainingThresholdFactor and tipCount > 0
            lines[#lines + 1] = string.format(
                "[p%d] %s  actual=%.2f std=%.2f degree=%.3f threshold=%.3f tips=%d => %s",
                priority,
                trainingType,
                actual,
                stdVal,
                degree,
                trainingTypeInfo.trainingThresholdFactor,
                tipCount,
                wouldShow and "可显示" or "不显示"
            )
            if wouldShow and not shown then
                lines[#lines + 1] = "  >> 实际面板将优先显示此类型的随机练度文案（共 " .. tipCount .. " 条候选）"
                shown = true
            end
        end
    end
end

DebugDeathInfoCtrl._RefreshTipsDisplay = HL.Method() << function(self)
    local deathInfo = self:_ReadDeathInfoFromFields()
    local lines = {}

    self:_AppendTrainingPreview(deathInfo, lines)

    local modeLabel = DISPLAY_MODE_OPTIONS[self.m_selectedModeCsIndex + 1].label
    local mode, tipGroup, indexOffset = self:_GetTipGroupAndMode()
    lines[#lines + 1] = "=== 死亡提示候选（displayMode=" .. modeLabel .. " | 实际命中=" .. mode .. "）==="
    if not tipGroup or #tipGroup == 0 then
        lines[#lines + 1] = "当前 DeathInfo 未找到对应 tips (mode=" .. mode .. ")"
    else
        for i = 1, #tipGroup do
            lines[#lines + 1] = tostring(i) .. ": " .. tostring(tipGroup[i + indexOffset])
        end
    end

    self.view.tipsPreviewText.text = table.concat(lines, "\n")
end

DebugDeathInfoCtrl._OnConfirm = HL.Method() << function(self)
    local deathInfo = self:_ReadDeathInfoFromFields()
    local index1 = tonumber(self.view.index1.text)
    local index2 = tonumber(self.view.index2.text)

    UIManager:Close(PANEL_ID)
    UIManager:Open(PanelId.DeathInfoFake, {
        deathInfo = deathInfo,
        index1 = index1,
        index2 = index2,
        displayMode = DISPLAY_MODE_OPTIONS[self.m_selectedModeCsIndex + 1].mode,
    })
end

DebugDeathInfoCtrl._InitDisplayModeDropdown = HL.Method() << function(self)
    local dropdown = self.view.displayModeDropdown
    dropdown:ClearComponent()
    dropdown:Init(
        function(csIndex, option, _)
            option:SetText(DISPLAY_MODE_OPTIONS[csIndex + 1].label)
        end,
        function(csIndex, isOn)
            if not isOn then
                return
            end
            self.m_selectedModeCsIndex = csIndex

            for i = 0, #DISPLAY_MODE_OPTIONS - 1 do
                if i ~= csIndex then
                    dropdown:SetSelected(i, false, false, false, false)
                end
            end
            self:_RefreshTipsDisplay()
        end
    )
    dropdown:Refresh(#DISPLAY_MODE_OPTIONS)

    dropdown:SetSelected(self.m_selectedModeCsIndex, true, false, false, false)
    dropdown.onIsNaviTargetChanged = function(isTarget)
        InputManagerInst:ToggleGroup(dropdown.groupId, isTarget)
    end
end


DebugDeathInfoCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.text1.text = "副本 Id"
    self.view.text2.text = "敌人 Id"
    self.view.text3.text = "敌人等级"
    self.view.text4.text = "小队等级和"
    self.view.text5.text = "小队武器等级和"
    self.view.text6.text = "小队技能等级和"
    self.view.text7.text = "小队装备穿戴等级和"
    self.view.text8.text = "指定index"

    local defaultD = buildDefaultDeathInfoTable()
    self:_FillFieldsFromDeathInfo(defaultD)

    self.view.closeButton.onClick:AddListener(function()
        UIManager:Close(PANEL_ID)
    end)

    self.view.confirmButton.onClick:AddListener(function()
        self:_OnConfirm()
    end)

    local function onFieldChanged()
        self:_RefreshTipsDisplay()
    end

    self.view.deathDungeonId.onValueChanged:AddListener(onFieldChanged)
    self.view.deathEnemyId.onValueChanged:AddListener(onFieldChanged)
    self.view.deathEnemyLv.onValueChanged:AddListener(onFieldChanged)
    self.view.deathSquadLvSum.onValueChanged:AddListener(onFieldChanged)
    self.view.deathSquadWeaponLvSum.onValueChanged:AddListener(onFieldChanged)
    self.view.deathSquadSkillLvSum.onValueChanged:AddListener(onFieldChanged)
    self.view.deathSquadEquipLvSum.onValueChanged:AddListener(onFieldChanged)

    self:_InitDisplayModeDropdown()

    self:_RefreshTipsDisplay()
end

HL.Commit(DebugDeathInfoCtrl)
