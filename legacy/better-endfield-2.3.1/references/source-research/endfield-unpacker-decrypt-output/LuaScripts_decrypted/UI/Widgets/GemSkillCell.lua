local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

GemSkillCell = HL.Class('GemSkillCell', UIWidgetBase)

GemSkillCell.m_notchCellCache = HL.Field(HL.Forward("UIListCache"))

GemSkillCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_notchCellCache = UIUtils.genCellCache(self.view.notchCell)
end

GemSkillCell.InitGemSkillCell = HL.Method(HL.String, HL.Number, HL.Opt(HL.Boolean)) << function(self, termId, skillLevel, isActive)
    self:_FirstTimeInit()

    local _, termCfg = Tables.gemTable:TryGetValue(termId)
    if not termCfg then
        logger.LogError("GemSkillCell.InitGemSkillCell: termCfg is nil, termId = " .. termId)
        return
    end
    if isActive == nil then
        isActive = true
    end

    local skillNameFormat = isActive and Language.LUA_GEM_CARD_SKILL_ACTIVE or Language.LUA_GEM_CARD_SKILL_INACTIVE
    self.view.skillName:SetAndResolveTextStyle(string.format(skillNameFormat, termCfg.tagName))
    self.view.skillDefault.gameObject:SetActive(not isActive)
    self.view.skillActive.gameObject:SetActive(isActive)
    self.view.rankValue.text = string.format(Language.LUA_WEAPON_EXHIBIT_UPGRADE_ADD_FORMAT, skillLevel)

    self.m_notchCellCache:Refresh(skillLevel, function(notchCell, index)
        notchCell.progress.gameObject:SetActive(false)
        notchCell.progressGem.gameObject:SetActive(false)
        notchCell.available.gameObject:SetActive(false)
        notchCell.notAvailable.gameObject:SetActive(false)
        notchCell.maxAdd.gameObject:SetActive(false)

        notchCell.progressGem.gameObject:SetActive(isActive)
        notchCell.available.gameObject:SetActive(not isActive)
    end)
end

HL.Commit(GemSkillCell)
return GemSkillCell

