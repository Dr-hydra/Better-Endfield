local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharSkillNode = HL.Class('CharSkillNode', UIWidgetBase)

CharSkillNode.m_skillCells = HL.Field(HL.Forward("UIListCache"))

CharSkillNode.m_isSkillSelectable = HL.Field(HL.Boolean) << false

CharSkillNode.m_showMode = HL.Field(HL.Number) << 1


CharSkillNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_skillCells = UIUtils.genCellCache(self.view.buttonSkill)
end





CharSkillNode.InitCharSkillNode = HL.Method() << function(self)

    self:_FirstTimeInit()
end

CharSkillNode.RefreshSkills = HL.Method(HL.Number, HL.String, HL.Opt(HL.Number, HL.Table)) << function(self, instId, templateId, teamIndex, selectCache)
    local skillDataDict = CharInfoUtils.getPlayerCharCurSkills(instId, templateId, teamIndex)
    local skills = {}
    for _, skillDatas in pairs(skillDataDict) do
        for _, skillData in pairs(skillDatas) do
            skillData.showChange = skillData.bundleData.skillType == Const.SkillTypeEnum.NormalSkill
            skillData.showInUse = skillData.bundleData.skillType == Const.SkillTypeEnum.NormalSkill
            if selectCache and selectCache[instId] then
                skillData.inUse = skillData.skillId == selectCache[instId]
            end

            table.insert(skills, {
                skillType = skillData.bundleData.skillType,
                breakStage = skillData.bundleData.breakStage,
                skillData = skillData,
                charId = templateId,
                charInstId = instId,
                unlock = skillData.unlock,
            })
        end
    end

    skills = lume.sort(skills, function(a, b)
        if a.skillType:ToInt() ~= b.skillType:ToInt() then
            return a.skillType:ToInt() < b.skillType:ToInt()
        end

        return a.breakStage < b.breakStage
    end)
    local extraSkill = #skills > Const.CHAR_SKILL_NUM
    self.m_skillCells:Refresh(#skills, function(cell, luaIndex)
        local info = skills[luaIndex]
        info.transform = cell.gameObject.transform
        info.skillData.showInUse = extraSkill and info.skillData.showInUse
        info.skillData.showChange = extraSkill and info.skillData.showChange
        cell:InitCharInfoSkillButton(info.skillData, function()
            self:OnSkillClick(info, luaIndex)
        end)
        cell.gameObject:SetActive(true)
    end)

    self.view.mainSkillBg.gameObject:SetActive(extraSkill)
end

CharSkillNode.SetShowMode = HL.Method(HL.Number) << function(self, mode)
    self.m_showMode = mode
    if mode == UIConst.CHAR_SKILL_MODE.ShowSkillTypeName then
        self.view.skillType.gameObject:SetActive(true)
    else
        self.view.skillType.gameObject:SetActive(false)
    end
end

CharSkillNode.RefreshSkillSelect = HL.Method(HL.Opt(HL.Number)) << function(self, selectIndex)
    local count = self.m_skillCells:GetCount()
    if self.m_showMode == UIConst.CHAR_SKILL_MODE.ShowSkillTypeName then
        if selectIndex and count == Const.CHAR_SKILL_NUM + 1 and selectIndex >= Const.CHAR_SKILL_NUM then
            selectIndex = selectIndex - 1
        end

        for i = 1, Const.CHAR_SKILL_NUM do
            local imageSelect = self.view[string.format("imageSelect%d", i)]
            imageSelect.gameObject:SetActive(selectIndex == i)
        end

    else
        for i = 1, count do
            local cell = self.m_skillCells:GetItem(i)
            cell:SetSelect(selectIndex == i)
        end
    end
end

CharSkillNode.OnSkillClick = HL.Method(HL.Table, HL.Number) << function(self, skillInfo, index)
    self:_ShowSkillTips(skillInfo)
    self:RefreshSkillSelect(index)
end


CharSkillNode._ShowSkillTips = HL.Method(HL.Table) << function(self, skillInfo)
    Notify(MessageConst.SHOW_CHAR_SKILL_TIP, {skillInfo, true})
end

HL.Commit(CharSkillNode)
return CharSkillNode

