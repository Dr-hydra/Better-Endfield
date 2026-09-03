local WeaponInfo = require_ex('UI/Widgets/WeaponInfo')

UpgradeWeaponInfo = HL.Class('UpgradeWeaponInfo', WeaponInfo)

UpgradeWeaponInfo.m_sliderTween = HL.Field(HL.Any)

UpgradeWeaponInfo._OnFirstTimeInit = HL.Override() << function(self)
    self.view.upgradeLevelNode.addExp.text = 0
    self.view.upgradeLevelNode.addLevel.text = 0

    self.view.upgradeLevelNode.addExpBar.fillAmount = 0
end

UpgradeWeaponInfo.InitUpgradeWeaponInfo = HL.Method(HL.Table) << function(self, arg)
    self:_FirstTimeInit()

    local weaponExhibitInfo = arg.weaponExhibitInfo

    local curLevel = arg.curLevel
    local targetLevel = arg.targetLevel

    local curExp = arg.curExp
    local nextLvExp = arg.nextLvExp
    local addExp = arg.addExp

    local addLevel = targetLevel - curLevel

    self:_RefreshNameNode(weaponExhibitInfo)

    self.view.levelBreakNode.gameObject:SetActive(false)
    self.view.weaponAttributeNode.gameObject:SetActive(true)
    self.view.weaponSkillNode.gameObject:SetActive(false)

    if arg.inUpgradeTransition then

        self.view.upgradeLevelNode.curExp.text = curExp
        self.view.upgradeLevelNode.addLevel.text = addLevel
        self.view.upgradeLevelNode.addExp.text = addExp
        self.view.upgradeLevelNode.curLevel.tweenToText = curLevel
    else

        self.view.upgradeLevelNode.curExp.text = curExp
        self.view.upgradeLevelNode.addLevel.tweenToText = addLevel
        self.view.upgradeLevelNode.addExp.text = addExp
        self.view.upgradeLevelNode.curLevel.text = curLevel
    end

    self.view.upgradeLevelNode.stageLevel.text = weaponExhibitInfo.stageLv

    if self.m_sliderTween then
        self.m_sliderTween:Kill()
    end
    self.view.upgradeLevelNode.currentExpBar.fillAmount = (targetLevel > curLevel) and 0 or (curExp / nextLvExp)
    local newValue = (curExp + addExp) / nextLvExp
    self.m_sliderTween = DOTween.To(function()
        return self.view.upgradeLevelNode.addExpBar.fillAmount
    end, function(value)
        self.view.upgradeLevelNode.addExpBar.fillAmount = value
    end, newValue, self.view.config.SLIDER_TWEEN_TIME)

    self.view.upgradeLevelNode.nextLvExp.text = nextLvExp

    self.view.weaponAttributeNode:InitWeaponUpgradeAttributeNode({
        fromLv = weaponExhibitInfo.curLv,
        fromBreakthroughLv = weaponExhibitInfo.curBreakthroughLv,
        toLv = targetLevel,
        toBreakthroughLv = weaponExhibitInfo.curBreakthroughLv,
        weaponInstId = weaponExhibitInfo.weaponInst.instId,
    },{
        showAttrTransition = arg.inUpgradeTransition
    })
end

UpgradeWeaponInfo.InitBreakWeaponInfo = HL.Method(HL.Table) << function(self, arg)
    self:_FirstTimeInit()

    local weaponExhibitInfo = arg.weaponExhibitInfo
    local weaponInstId = weaponExhibitInfo.weaponInst.instId

    local weaponInst = CharInfoUtils.getWeaponByInstId(weaponInstId)

    local fromBreakthroughCfg = CharInfoUtils.getWeaponExpInfo(weaponInstId, weaponInst.breakthroughLv)
    local toBreakthroughCfg = CharInfoUtils.getWeaponExpInfo(weaponInstId, weaponInst.breakthroughLv + 1)

    self.view.weaponAttributeNode.gameObject:SetActive(false)
    self.view.weaponSkillNode.gameObject:SetActive(true)

    self.view.breakLevelNode.currentLevel.text = fromBreakthroughCfg.stageLv
    self.view.breakLevelNode.fromLevel.text = fromBreakthroughCfg.stageLv
    self.view.breakLevelNode.toLevel.text = toBreakthroughCfg.stageLv

    self.view.levelBreakNode.gameObject:SetActive(true)
    self.view.levelBreakNode:InitLevelBreakNode(weaponExhibitInfo.curBreakthroughLv, true, weaponExhibitInfo.breakthroughInfoList)

    self.view.weaponAttributeNode:InitWeaponUpgradeAttributeNode({
        fromLv = weaponExhibitInfo.curLv,
        toLv = weaponExhibitInfo.curLv,
        fromBreakthroughLv = weaponInst.breakthroughLv,
        toBreakthroughLv = weaponInst.breakthroughLv + 1,
        weaponInstId = weaponExhibitInfo.weaponInst.instId,
    })
    self.view.weaponSkillNode:InitWeaponSkillNode(weaponInstId, {
        tryBreakthroughLv = weaponInst.breakthroughLv + 1,
        tryRefineLv = weaponInst.refineLv,
    })
end

UpgradeWeaponInfo._OnDestroy = HL.Override() << function(self)
    if self.m_sliderTween then
        self.m_sliderTween:Kill()
    end
end

HL.Commit(UpgradeWeaponInfo)
return UpgradeWeaponInfo

