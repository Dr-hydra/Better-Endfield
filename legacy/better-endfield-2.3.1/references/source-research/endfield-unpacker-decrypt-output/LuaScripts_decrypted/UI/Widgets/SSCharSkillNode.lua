local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SSCharSkillNode = HL.Class('SSCharSkillNode', UIWidgetBase)


SSCharSkillNode.m_charId = HL.Field(HL.String) << ''

SSCharSkillNode.m_skillCells = HL.Field(HL.Forward('UIListCache'))

SSCharSkillNode.m_levelUpSkills = HL.Field(HL.Table)

SSCharSkillNode.m_canLevelUpState = HL.Field(HL.Boolean) << false



SSCharSkillNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_skillCells = UIUtils.genCellCache(self.view.skillCell)
end


SSCharSkillNode.InitSSCharSkillNode = HL.Method(HL.String, HL.Opt(HL.String, HL.Boolean)) << function(self, charId, targetRoomId, ignoreControllerCelleHint)
    self:_FirstTimeInit()
    self.m_levelUpSkills = {}
    self.m_charId = charId
    self.m_canLevelUpState = false
    local spaceship = GameInstance.player.spaceship

    local char = spaceship.characters:get_Item(charId)

    local roomType
    if targetRoomId then
        roomType = SpaceshipUtils.getRoomTypeByRoomId(targetRoomId)
    end

    local ssCharSkillInfoList = Tables.spaceshipCharSkillTable[charId]
    local skillIndexList = {}
    for skIndex = 0, ssCharSkillInfoList.maxSkillCount - 1 do
        local isValid = true
        if self.view.config.HIDE_NON_VALID_SKILL then
            local unlocked, skillId = char.skills:TryGetValue(skIndex)
            if unlocked then
                local skillData = Tables.spaceshipSkillTable[skillId]
                isValid = skillData.roomType == roomType
            else
                isValid = false
            end
        end
        if isValid then
            table.insert(skillIndexList, skIndex)
        end
    end
    local skillCount = #skillIndexList

    self.m_skillCells:Refresh(skillCount, function(cell, index)
        local skIndex = skillIndexList[index]
        local unlocked, skillId = char.skills:TryGetValue(skIndex)
        local nextSkillId, unlockHint = char:GetNextSkillId(skIndex)
        cell.upgradeNode.gameObject:SetActive(false)
        if unlocked then
            local canLevelUp = not string.isEmpty(nextSkillId)
            cell.gameObject.name = "SkillCell-" .. skillId
            cell.upgradeNode.gameObject:SetActive(canLevelUp)
            cell.upgradeHint.gameObject:SetActive(false)
            cell.upgradeNode:SetState("NotClicked")
            cell.upgradeHintAutoCloseArea.onTriggerAutoClose:AddListener(function()
                cell.upgradeNode:SetState("NotClicked")
            end)
            cell.upgradeBtn.onClick:RemoveAllListeners()
            if canLevelUp then
                self.m_canLevelUpState = true
                cell.upgradeBtn.onClick:AddListener(function()
                    cell.upgradeHint.gameObject:SetActive(not cell.upgradeHint.gameObject.activeSelf)
                    cell.upgradeNode:SetState(cell.upgradeHint.gameObject.activeSelf and "Click" or "NotClicked")
                    AudioManager.PostEvent(cell.upgradeHint.gameObject.activeSelf and "Au_UI_Toggle_Common_On" or "Au_UI_Toggle_Common_Off")
                end)
                table.insert(self.m_levelUpSkills, cell)
                cell.upgradeHintTxt.text = unlockHint
            end
            local skillData = Tables.spaceshipSkillTable[skillId]
            cell.icon:LoadSprite(UIConst.UI_SPRITE_SS_SKILL_ICON, skillData.icon)
            cell.nameTxt.text = skillData.name
            local isValid = skillData.roomType == roomType
            if self.view.config.SHOW_NOT_MATCH_SKILL_DESC and not isValid then
                cell.stateController:SetState("Mismatching")
            else
                cell.stateController:SetState("Normal")
            end
            cell.desc.text = skillData.desc
            local isActive = true
            if roomType then
                isActive = skillData.roomType == roomType
            end
            cell.animationWrapper:PlayWithTween(isActive and "ss_char_skill_cell_normal" or "ss_char_skill_cell_inactive")
        else
            cell.gameObject.name = "SkillCell-" .. nextSkillId
            local skillData = Tables.spaceshipSkillTable[nextSkillId]
            cell.icon:LoadSprite(UIConst.UI_SPRITE_SS_SKILL_ICON, skillData.icon)
            cell.nameTxt.text = skillData.name
            if self.view.config.SHOW_LOCKED_SKILL_DESC then
                cell.stateController:SetState("NotUnlocked")
                cell.desc.text = skillData.desc
                cell.status.text = unlockHint
            else
                cell.stateController:SetState("Normal")
                cell.desc.text = unlockHint
            end
            cell.upgradeHint.gameObject:SetActive(false)
            cell.upgradeNode:SetState("NotClicked")
            cell.upgradeNode.gameObject:SetActive(false)
            cell.animationWrapper:PlayWithTween("ss_char_skill_cell_locked")
        end
    end)

    if not ignoreControllerCelleHint then
        self:BindInputPlayerAction("ss_char_skill_detail",function()
            self:TriggerSkillHint()
        end)
    end
end

SSCharSkillNode.TriggerSkillHint = HL.Method() << function(self)
    for i, cell in ipairs(self.m_levelUpSkills) do
        cell.upgradeHint.gameObject:SetActive(not cell.upgradeHint.gameObject.activeSelf)
        cell.upgradeNode:SetState(cell.upgradeHint.gameObject.activeSelf and "Click" or "NotClicked")
        AudioManager.PostEvent(cell.upgradeHint.gameObject.activeSelf and "Au_UI_Toggle_Common_On" or "Au_UI_Toggle_Common_Off")
    end
end

SSCharSkillNode.GetCanLevelUpState = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_canLevelUpState
end


HL.Commit(SSCharSkillNode)
return SSCharSkillNode
