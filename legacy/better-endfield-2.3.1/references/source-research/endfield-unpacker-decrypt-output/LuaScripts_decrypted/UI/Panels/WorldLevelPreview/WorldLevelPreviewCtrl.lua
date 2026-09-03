local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WorldLevelPreview

WorldLevelPreviewCtrl = HL.Class('WorldLevelPreviewCtrl', uiCtrl.UICtrl)

WorldLevelPreviewCtrl.m_fromMainHudActionQueue = HL.Field(HL.Boolean) << false





WorldLevelPreviewCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE] = "OnToastInterrupted",
}

WorldLevelPreviewCtrl.ShowPreview = HL.StaticMethod(HL.Any) << function(args)

    local lastLevel, currentWorldLevel, isActiveChange = unpack(args)
    local openArg = {
        isUp = currentWorldLevel > lastLevel,
        lastLevel = lastLevel,
        currentWorldLevel = currentWorldLevel,
    }

    local actionDirect = function()
        openArg.fromMainHudActionQueue = false
        UIManager:Open(PANEL_ID, openArg)
    end

    local actionFromQueue = function()
        openArg.fromMainHudActionQueue = true
        UIManager:Open(PANEL_ID, openArg)
    end
    if isActiveChange then
        PhaseManager:PopPhase(PhaseId.Watch)
        actionDirect()
    else

        if LuaSystemManager.mainHudActionQueue ~= nil and (not LuaSystemManager.mainHudActionQueue:HasRequest('WorldLevelPreview')) and GameInstance.player.adventure.needShowWorldLevelUpToast then
            LuaSystemManager.mainHudActionQueue:AddRequest('WorldLevelPreview', actionFromQueue)
        end
    end

end


WorldLevelPreviewCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    self.m_fromMainHudActionQueue = arg and arg.fromMainHudActionQueue == true
    self.view.closeBtn.onClick:RemoveAllListeners()
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
            if self.m_fromMainHudActionQueue then
                Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "WorldLevelPreview")
            end
        end)
    end)

    self:BindInputPlayerAction("common_cancel_no_hint", function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
            if self.m_fromMainHudActionQueue then
                Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "WorldLevelPreview")
            end
        end)
    end)

    if arg then
        if arg.isUp == nil or arg.lastLevel == nil or arg.currentWorldLevel == nil then
            logger.error('WorldLevelPreviewCtrl.OnCreate: arg is invalid' .. arg)
            return
        end
    else
        logger.error('WorldLevelPreviewCtrl.OnCreate: arg is nil')
        return
    end

    GameInstance.player.adventure:SendReadMaxWorldLevel()
    local isUp = arg.isUp
    local lastLevel = arg.lastLevel
    local currentWorldLevel = arg.currentWorldLevel
    local maxWorldLevel = GameInstance.player.adventure.currentMaxWorldLevel
    self.view.titleText.text = isUp and Language.LUA_WORLD_LEVEL_UP_TITLE or Language.LUA_WORLD_LEVEL_DOWN_TITLE

    self.view.decoArrowImage.transform.localScale = isUp and Vector3(1, -1, 1) or Vector3.one

    self.animationWrapper:Play(isUp and 'worldlevelpreview_upin' or 'worldlevelpreview_downin')


    local genUpCells = UIUtils.genCellCache(self.view.content.upLayoutGroup.cell)
    genUpCells:Refresh(maxWorldLevel, function(cell, luaIndex)
        cell.gameObject:GetComponent('UIText').text = string.format("%02d", maxWorldLevel - luaIndex + 1)
    end)
    self.view.content:UpdateUpPos(lastLevel)


    local genDownCells = UIUtils.genCellCache(self.view.content.downLayoutGroup.cell)
    genDownCells:Refresh(maxWorldLevel, function(cell, luaIndex)
        cell.gameObject:GetComponent('UIText').text = string.format("%02d", maxWorldLevel - luaIndex + 1)
    end)
    self.view.content:UpdateDownPos(maxWorldLevel - lastLevel + 1)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })


    local genMidCells = UIUtils.genCellCache(self.view.content.middleLayoutGroup.cell)
    genMidCells:Refresh(maxWorldLevel, function(cell, luaIndex)
        cell.gameObject:GetComponent('UIText').text = string.format("%02d", maxWorldLevel - luaIndex + 1)
    end)
    self.view.content:UpdateMidPos(lastLevel - 1)

    self.view.content:UpdateWorldLevelScrollList(isUp, math.abs(lastLevel - currentWorldLevel))

    local charIndex = 0
    local monsterIndex = 1
    local textKeyTable = GameInstance.player.adventure:GetTextIdList(currentWorldLevel, lastLevel)
    for _, textId in pairs(Tables.globalConst.worldLevelNotShowTextIds) do
        textKeyTable:Remove(textId)
    end


    textKeyTable:Insert(charIndex, "LUA_WORLD_LEVEL_CHAR_TIP")
    textKeyTable:Insert(monsterIndex, "LUA_WORLD_LEVEL_MONSTER_TIP")

    local genTextCells = UIUtils.genCellCache(self.view.cell)
    genTextCells:Refresh(textKeyTable.Count, function(cell, luaIndex)
        if CSIndex(luaIndex) == charIndex then
            cell.num01Txt.text = Tables.adventureWorldLevelTable:GetValue(lastLevel).charMaxLv
            cell.num02Txt.text = Tables.adventureWorldLevelTable:GetValue(currentWorldLevel).charMaxLv
            cell.num02Txt.color = isUp and self.view.config.UP_COLOR or self.view.config.DOWN_COLOR
            cell.num01Txt.gameObject:SetActiveIfNecessary(true)
            cell.num02Txt.gameObject:SetActiveIfNecessary(true)
            cell.decoArrow.gameObject:SetActiveIfNecessary(true)
        elseif CSIndex(luaIndex) == monsterIndex then
            cell.num01Txt.text = Tables.adventureWorldLevelTable:GetValue(lastLevel).monsterBaseLv
            cell.num02Txt.text = Tables.adventureWorldLevelTable:GetValue(currentWorldLevel).monsterBaseLv
            cell.num02Txt.color = isUp and self.view.config.UP_COLOR or self.view.config.DOWN_COLOR
            cell.num01Txt.gameObject:SetActiveIfNecessary(true)
            cell.num02Txt.gameObject:SetActiveIfNecessary(true)
            cell.decoArrow.gameObject:SetActiveIfNecessary(true)
        else
            cell.num01Txt.gameObject:SetActiveIfNecessary(false)
            cell.num02Txt.gameObject:SetActiveIfNecessary(false)
            cell.decoArrow.gameObject:SetActiveIfNecessary(false)
        end
        local textId = textKeyTable[CSIndex(luaIndex)]
        cell.infoTxt.text = Language[textId]
    end)

end

WorldLevelPreviewCtrl.OnToastInterrupted = HL.Method() << function(self)

    self.animationWrapper:ClearTween(false)
    self:Close()
end











HL.Commit(WorldLevelPreviewCtrl)
