local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WeaponIntroduction = HL.Class('WeaponIntroduction', UIWidgetBase)

WeaponIntroduction.m_starCellCache = HL.Field(HL.Forward("UIListCache"))


WeaponIntroduction._OnFirstTimeInit = HL.Override() << function(self)
    self.m_starCellCache = UIUtils.genCellCache(self.view.starCell)


end

WeaponIntroduction.InitWeaponIntroduction = HL.Method(HL.String, HL.Number) << function(self, weaponTemplateId, weaponInstId)
    self:_FirstTimeInit()

    local _, weaponItemCfg = Tables.itemTable:TryGetValue(weaponTemplateId)
    if not weaponItemCfg then
        logger.error("WeaponExhibitOverview->Can't find weaponItem ID: " .. weaponTemplateId)
        return
    end

    self.m_starCellCache:Refresh(weaponItemCfg.rarity)
    self.view.weaponName.text = weaponItemCfg.name


    local isTrailWeapon = CharInfoUtils.checkIsWeaponInTrail(weaponInstId)
    self.view.lockToggle.gameObject:SetActive(not isTrailWeapon)
    self.view.deco05.gameObject:SetActive(not isTrailWeapon)

    if not isTrailWeapon then
        self.view.lockToggle:InitLockToggle(weaponTemplateId, weaponInstId)
    end

    UIUtils.setItemRarityImage(self.view.qualityColor, weaponItemCfg.rarity)
end

HL.Commit(WeaponIntroduction)
return WeaponIntroduction
