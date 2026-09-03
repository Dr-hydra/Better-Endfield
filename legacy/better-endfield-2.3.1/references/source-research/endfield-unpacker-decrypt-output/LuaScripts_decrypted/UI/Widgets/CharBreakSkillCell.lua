local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharBreakSkillCell = HL.Class('CharBreakSkillCell', UIWidgetBase)

CharBreakSkillCell.m_callback = HL.Field(HL.Function)


CharBreakSkillCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.buttonSkill.onClick:RemoveAllListeners()
    self.view.buttonSkill.onClick:AddListener(function()
        if self.m_callback then
            self.m_callback()
        end
    end)

    self.view.buttonTalent.onClick:RemoveAllListeners()
    self.view.buttonTalent.onClick:AddListener(function()
        if self.m_callback then
            self.m_callback()
        end
    end)

    self.view.buttonFacSkill.onClick:RemoveAllListeners()
    self.view.buttonFacSkill.onClick:AddListener(function()
        if self.m_callback then
            self.m_callback()
        end
    end)
end


CharBreakSkillCell.InitCharBreakSkillCell = HL.Method(HL.Table) << function(self, data)
    self:_FirstTimeInit()

    local skillData = data.skillData
    local talentData = data.talentData
    local facSkillData = data.facSkillData
    local isUnlock = data.isUnlock

    local isSkill = skillData ~= nil
    local isTalent = talentData ~= nil
    local isFacSkill = facSkillData ~= nil
    self.m_callback = data.callback


    self.view.skillNode.gameObject:SetActive(isSkill)
    self.view.talentNode.gameObject:SetActive(talentData)
    self.view.facSkillNode.gameObject:SetActive(isFacSkill)

    if isSkill then
        self.view.buttonSkillNew:InitCharInfoSkillButton(skillData)
        self.view.textSkillName.text = skillData.patchData.skillName
    elseif isTalent then
        if isUnlock then
            self.view.describeText.text = Language.LUA_TALENT_UNLOCK
        else
            self.view.describeText.text = Language.LUA_TALENT_ENHANCE
        end
        self.view.talentNameText.text = talentData.talentName
        self.view.talentUnlockIcon.gameObject:SetActive(isUnlock)
        self.view.talentArrowIcon.gameObject:SetActive(not isUnlock)
    else
        if isUnlock then
            self.view.facSkillNode.describeText.text = Language.LUA_FAC_SKILL_UNLOCK
        else
            self.view.facSkillNode.describeText.text = Language.LUA_FAC_SKILL_ENHANCE
        end

        local facSkillNode = self.view.facSkillNode
        local cell = facSkillNode.charInfoFacSkillCell
        cell.text.text = facSkillData.name
        cell.textShadow.text = facSkillData.name
        cell.icon:LoadSprite(UIConst.UI_SPRITE_FAC_SKILL_ICON, facSkillData.icon)
        cell.iconShadow:LoadSprite(UIConst.UI_SPRITE_FAC_SKILL_ICON, facSkillData.icon)

        facSkillNode.unlockIcon.gameObject:SetActive(isUnlock)
        facSkillNode.arrowIcon.gameObject:SetActive(not isUnlock)
    end
end

HL.Commit(CharBreakSkillCell)
return CharBreakSkillCell

