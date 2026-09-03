local skillBgPrefix = "decal_skillline_0%d"
local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharInfoSkillButton = HL.Class('FormationSkillButton', UIWidgetBase)

CharInfoSkillButton.skillData = HL.Field(HL.Table)

CharInfoSkillButton.m_charInfo = HL.Field(HL.Any)

CharInfoSkillButton.m_skillGroupCfg = HL.Field(HL.Userdata)


CharInfoSkillButton._OnFirstTimeInit = HL.Override() << function(self)
end

CharInfoSkillButton.InitCharInfoSkillButton = HL.Method(HL.Table, HL.Opt(HL.Function)) << function(self, skillData, onClick)

    self:_OnFirstTimeInit()
    self.skillData = skillData
    local patchData = skillData.patchData
    local inUse = skillData.inUse
    local showInUse = not not skillData.showInUse
    self.view.textSkill.text = patchData.skillName
    if self.view.inUseNode then
        self.view.inUseNode.gameObject:SetActive(inUse and showInUse)
        self.view.imageSelect.gameObject:SetActive(false)
    else
        self.view.imageSelect.gameObject:SetActive(inUse and showInUse)
    end

    local skillType = skillData.bundleData.skillType
    local skillTypeInt = LuaIndex(skillType:ToInt())
    if skillTypeInt > 0 and self.view.bgSkill then
        self.view.bgSkill:LoadSprite(UIConst.UI_SPRITE_SKILL_ICON, string.format(skillBgPrefix, skillTypeInt))
    end

    local damageType = patchData.iconBgType:GetHashCode()
    if damageType then
        local color
        local colorParamName = string.format("BG_SKILL_COLOR_%d", damageType)
        if self.view.config:HasValue(colorParamName) then
            color = self.view.config[colorParamName]
        end

        for i = 1, Const.CHAR_SKILL_NUM do
            local bgSkillColor = self.view[string.format("bgSkillColor%d", i)]
            if bgSkillColor then
                local active = i == skillTypeInt
                bgSkillColor.gameObject:SetActive(active)
                if active then
                    bgSkillColor.color = color
                end
            end
        end
    end

    self.view.skillIcon:LoadSprite(UIConst.UI_SPRITE_SKILL_ICON, patchData.iconId)
    self.view.button.onClick:RemoveAllListeners()
    self.view.lockedNode.gameObject:SetActive(not skillData.unlock)
    self.view.rankText.text = string.format(Language.LUA_CHAR_INFO_TALENT_SKILL_LEVEL_PREFIX, patchData.level)
    if onClick then
        self.view.button.interactable = true
        self.view.button.onClick:AddListener(function()
            onClick()
        end)
    else
        self.view.button.interactable = false
    end
end

CharInfoSkillButton.InitCharInfoSkillButtonNew = HL.Method(HL.Any, HL.Any, HL.Opt(HL.Function)) << function(self, charInfo, skillGroupType, onClick)
    self:_OnFirstTimeInit()
    local charTemplateId = charInfo.templateId
    local skillGroupCfg = CharInfoUtils.getCharSkillGroupCfgByType(charTemplateId, skillGroupType)
    if not skillGroupCfg then
        logger.error("InitCharInfoSkillButtonNew: skillGroupCfg is nil", charTemplateId, skillGroupType)
        return
    end
    local levelInfo = CharInfoUtils.getCharSkillLevelInfoByType(charInfo, skillGroupType)
    if not levelInfo then
        logger.error("InitCharInfoSkillButtonNew: levelInfo is nil", charTemplateId, skillGroupType)
        return
    end
    self.m_charInfo = charInfo
    self.m_skillGroupCfg = skillGroupCfg

    local isUltimateSkill = skillGroupType == GEnums.SkillGroupType.UltimateSkill
    self.view.bgSkillColor2.gameObject:SetActive(not isUltimateSkill)
    self.view.bgSkillColor3.gameObject:SetActive(isUltimateSkill)

    local bgColor = CharInfoUtils.getCharInfoSkillGroupBgColor(skillGroupCfg)
    self.view.bgSkillColor2.color = bgColor
    self.view.bgSkillColor3.color = bgColor

    local skillLv = levelInfo.level
    local isElite = skillLv >= UIConst.CHAR_MAX_SKILL_NORMAL_LV
    self.view.rank.gameObject:SetActive(not isElite)
    self.view.eliteNode.gameObject:SetActive(isElite)
    self.view.elitepolygon:InitElitePolygon(skillLv - UIConst.CHAR_MAX_SKILL_NORMAL_LV)
    self.view.textSkill.text = skillGroupCfg.name
    self.view.rankText.text = string.format(Language.LUA_CHAR_INFO_TALENT_SKILL_LEVEL_PREFIX, skillLv)
    self:RefreshSkillIcon()
    self.view.button.onClick:RemoveAllListeners()
    if onClick then
        self.view.button.interactable = true
        self.view.button.onClick:AddListener(function()
            onClick()
        end)
    else
        self.view.button.interactable = false
    end
end

CharInfoSkillButton.RefreshSkillIcon = HL.Method() << function(self)
    if not self.m_charInfo or not self.m_skillGroupCfg then
        return
    end
    local icon
    if CharInfoUtils.hasBothSkillGroupConditions(self.m_skillGroupCfg) then
        local activeIdx = CharInfoUtils.getActiveSkillGroupConditionIdx(self.m_charInfo.instId, self.m_skillGroupCfg)
        icon = CharInfoUtils.generateSkillGroupConditionIcon(self.m_charInfo.instId, self.m_skillGroupCfg, activeIdx)
    end
    if icon == nil or string.isEmpty(icon) then
        icon = self.m_skillGroupCfg.icon
    end
    self.view.skillIcon:LoadSprite(UIConst.UI_SPRITE_SKILL_ICON, icon)
end

CharInfoSkillButton.RefreshRedDot = HL.Method() << function(self)
    if not self.m_charInfo or not self.m_skillGroupCfg then
        return
    end
    self.view.redDot:InitRedDot("CharSkillNode", { self.m_charInfo.instId, self.m_skillGroupCfg.skillGroupId })
end

CharInfoSkillButton.SetSelect = HL.Method(HL.Boolean) << function(self, select)
    self.view.imageSelect.gameObject:SetActive(select)
end

HL.Commit(CharInfoSkillButton)
return CharInfoSkillButton
