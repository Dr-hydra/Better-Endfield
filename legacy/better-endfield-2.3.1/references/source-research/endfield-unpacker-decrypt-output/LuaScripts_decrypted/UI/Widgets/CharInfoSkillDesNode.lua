local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharInfoSkillDesNode = HL.Class('CharInfoSkillDesNode', UIWidgetBase)


CharInfoSkillDesNode._OnFirstTimeInit = HL.Override() << function(self)

end

CharInfoSkillDesNode.InitCharInfoSkillDesNode = HL.Method(HL.Table) << function(self, skillInfo)
    self:_FirstTimeInit()
    local skillData = skillInfo.skillData
    local skillId = skillData.skillId
    local bundleData = skillData.bundleData
    local patchData = skillData.patchData
    local skillType = bundleData.skillType
    local unlock = skillData.unlock
    local realMaxLevel = skillData.realMaxLevel

    local level = patchData.level
    local skillTypeText = CharInfoUtils.getSkillTypeName(skillType)


    self.view.textRank.gameObject:SetActive(unlock)

    if self.view.textLocked then
        self.view.textLocked.gameObject:SetActive(not unlock)
    end

    if unlock then
        local rankText = string.format("RANK %d", level)
        if skillType == Const.SkillTypeEnum.NormalAttack then
            rankText = "RANK MAX"
        end
        self.view.textRank.text = rankText
    else
        if self.view.textLocked then
            local textNum = Language[string.format("LUA_NUM_%d", bundleData.breakStage)]
            self.view.textLocked.text = string.format(Language.LUA_SKILL_UNLOCK_HINT, textNum)
        end
    end

    self.view.textName.text = patchData.skillName

    local description = Utils.SkillUtil.GetSkillDescription(skillId, level)
    self.view.textDes:SetAndResolveTextStyle(description)

    if self.view.textTitle then
        self.view.textTitle.text = skillTypeText
    end

    if self.view.buttonSkill then
        self.view.buttonSkill:InitCharInfoSkillButton(skillData)
    end
end

HL.Commit(CharInfoSkillDesNode)
return CharInfoSkillDesNode

