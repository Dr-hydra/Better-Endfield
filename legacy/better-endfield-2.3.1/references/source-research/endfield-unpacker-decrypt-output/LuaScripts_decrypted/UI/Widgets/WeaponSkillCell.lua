local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WeaponSkillCell = HL.Class('WeaponSkillCell', UIWidgetBase)

WeaponSkillCell.m_skillNotchCellCache = HL.Field(HL.Forward("UIListCache"))

WeaponSkillCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_skillNotchCellCache = UIUtils.genCellCache(self.view.weaponSkillNotchCell)
end

WeaponSkillCell.InitWeaponSkillCell = HL.Method(HL.Userdata, HL.Userdata, HL.String, HL.Opt(HL.Boolean)) << function(self, toLevelInfo, fromLevelInfo, skillDesc, onlyShowDiff)
    self:_FirstTimeInit()

    local skillCfg = CharInfoUtils.getSkillCfg(toLevelInfo.skillId, toLevelInfo.level)
    if not skillCfg then
        logger.error("WeaponExhibitOverview->Can't get skillCfg for skillId: " .. toLevelInfo.skillId)
        self.view.gameObject:SetActive(false)
        return
    end

    self.view.name.text = skillCfg.skillName
    self.view.desc:SetAndResolveTextStyle(skillDesc)
    self.view.progressText.text = string.format(Language.LUA_CHAR_INFO_WEAPON_SKILL_PROGRESS_FORMAT, toLevelInfo.level, toLevelInfo.maxLevel)


    local curLevel = toLevelInfo.level
    local levelDefault = fromLevelInfo.level
    self.view.gameObject:SetActive(true)

    if onlyShowDiff == true then
        if fromLevelInfo.level == toLevelInfo.level and fromLevelInfo.maxLevel == toLevelInfo.maxLevel then
            self.view.gameObject:SetActive(false)
            return
        end
    end

    self.m_skillNotchCellCache:Refresh(Tables.CharacterConst.maxWeaponSkillLevel, function(notchCell, index)
        local isFill = index <= levelDefault
        notchCell.progress.gameObject:SetActive(false)
        notchCell.progressGem.gameObject:SetActive(false)
        notchCell.available.gameObject:SetActive(false)
        notchCell.notAvailable.gameObject:SetActive(false)
        notchCell.maxAdd.gameObject:SetActive(false)
        if isFill then
            notchCell.progress.gameObject:SetActive(true)
            return
        end

        local isFillByGem = (not isFill) and (index <= curLevel)
        if isFillByGem then
            notchCell.progressGem.gameObject:SetActive(true)
            return
        end

        local isAvailable = index <= fromLevelInfo.maxLevel
        if isAvailable then
            notchCell.available.gameObject:SetActive(true)
            return
        end

        local isMaxAdd = (index > fromLevelInfo.maxLevel) and index <= toLevelInfo.maxLevel
        if isMaxAdd then
            notchCell.maxAdd.gameObject:SetActive(true)
            return
        end

        notchCell.notAvailable.gameObject:SetActive(true)
    end)
    local isMax = curLevel == Tables.characterConst.maxWeaponSkillLevel
    self.view.stateController:SetState(isMax and "max" or "normal")
    self.view.maxNode:SetAsLastSibling()
end

HL.Commit(WeaponSkillCell)
return WeaponSkillCell

