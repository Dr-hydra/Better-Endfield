local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WeaponInfo = HL.Class('WeaponInfo', UIWidgetBase)

WeaponInfo.m_weaponStarCell = HL.Field(HL.Forward("UIListCache"))

WeaponInfo.m_gemStarCell = HL.Field(HL.Forward("UIListCache"))

WeaponInfo.m_weaponInfo = HL.Field(HL.Table)

WeaponInfo.m_equipCallback = HL.Field(HL.Function)

WeaponInfo.m_expandCallback = HL.Field(HL.Function)

WeaponInfo.m_lastTryGemInstId = HL.Field(HL.Any)

WeaponInfo.m_isTrailChar = HL.Field(HL.Boolean) << false

WeaponInfo.m_isBtnUpgradeClicked = HL.Field(HL.Boolean) << false


WeaponInfo._OnFirstTimeInit = HL.Override() << function(self)
    self.m_weaponStarCell = UIUtils.genCellCache(self.view.titleNode.starCell)
    self.m_gemStarCell = UIUtils.genCellCache(self.view.gemCompareNode.starCell)

    local function onBtnEquipClicked()
        local isInRpgDungeon = Utils.isInRpgDungeon()
        if isInRpgDungeon then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_RPG_DUNGEON_FORBID_CHAR_WEAPON)
            return
        end

        if self.m_equipCallback then
            self.m_equipCallback(self.m_weaponInfo)
        end
    end
    self.view.buttonNode.buttonEquip.onClick:RemoveAllListeners()
    self.view.buttonNode.buttonEquip.onClick:AddListener(onBtnEquipClicked)

    if self.view.buttonNode.controllerConfirmBtn then
        self.view.buttonNode.controllerConfirmBtn.onClick:RemoveAllListeners()
        self.view.buttonNode.controllerConfirmBtn.onClick:AddListener(onBtnEquipClicked)
    end

    self.view.buttonNode.buttonExpand.onClick:RemoveAllListeners()
    self.view.buttonNode.buttonExpand.onClick:AddListener(function()
        local isInRpgDungeon = Utils.isInRpgDungeon()
        if isInRpgDungeon then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_RPG_DUNGEON_FORBID_CHAR_WEAPON)
            return
        end

        if self.m_expandCallback then
            self.m_expandCallback(self.m_weaponInfo)
        end
    end)

    self.view.charWeaponBasicNode.btnUpgrade.onClick:RemoveAllListeners()
    self.view.charWeaponBasicNode.btnUpgrade.onClick:AddListener(function()
        local isInRpgDungeon = Utils.isInRpgDungeon()
        if isInRpgDungeon then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_RPG_DUNGEON_FORBID_CHAR_WEAPON)
            return
        end

        local exhibitInfo = self.m_weaponInfo.weaponExhibitInfo
        if self.m_isTrailChar then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_WEAPON_UPGRADE_TRAIL_FORBID)
            return
        end

        local isMaxLv = exhibitInfo.curLv >= exhibitInfo.maxLv
        if isMaxLv then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_LEVEL_MAX_TOAST)
            return
        end

        if self.m_isBtnUpgradeClicked then
            return
        end
        self.m_isBtnUpgradeClicked = true
        Notify(MessageConst.CHAR_INFO_BLEND_EXIT, {
            finishCallback = function()
                self.m_isBtnUpgradeClicked = false
                CharInfoUtils.openWeaponInfoBestWay({
                    weaponTemplateId = exhibitInfo.weaponInst.templateId,
                    weaponInstId = exhibitInfo.weaponInst.instId,
                    pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.UPGRADE,
                    isFocusJump = true,
                })
            end
        })
    end)

    self.view.charWeaponBasicNode.btnPotential.onClick:RemoveAllListeners()
    self.view.charWeaponBasicNode.btnPotential.onClick:AddListener(function()
        local isInRpgDungeon = Utils.isInRpgDungeon()
        if isInRpgDungeon then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_RPG_DUNGEON_FORBID_CHAR_WEAPON)
            return
        end

        local exhibitInfo = self.m_weaponInfo.weaponExhibitInfo
        if self.m_isTrailChar then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_WEAPON_POTENTIAL_TRAIL_FORBID)
            return
        end

        Notify(MessageConst.CHAR_INFO_BLEND_EXIT, {
            finishCallback = function()
                CharInfoUtils.openWeaponInfoBestWay({
                    weaponTemplateId = exhibitInfo.weaponInst.templateId,
                    weaponInstId = exhibitInfo.weaponInst.instId,
                    pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.POTENTIAL,
                    isFocusJump = true,
                })
            end
        })
    end)
end

WeaponInfo._InitWeaponInfo = HL.Method(HL.Table, HL.Opt(HL.Number, HL.String)) << function(self, weaponInfo, tryGemInstId, tryCharId)
    self:_FirstTimeInit()
    self.m_weaponInfo = weaponInfo
    self.m_equipCallback = weaponInfo.equipCallback
    self.m_expandCallback = weaponInfo.expandCallback
    self.m_lastTryGemInstId = tryGemInstId ~= nil and tryGemInstId or -1
    self.m_isTrailChar = not CharInfoUtils.isCharDevAvailable(weaponInfo.weaponExhibitInfo.weaponInst.equippedCharServerId)
    self:_RefreshButtonState(weaponInfo)

    local weaponExhibitInfo = weaponInfo.weaponExhibitInfo
    local weaponInstId = weaponExhibitInfo.weaponInst.instId

    self:_RefreshGemCompareNode(weaponInfo.weaponExhibitInfo, tryGemInstId)
    self:_RefreshNameNode(weaponInfo.weaponExhibitInfo)

    self:_RefreshTitleNode(weaponInfo.weaponExhibitInfo)
    self:_RefreshAttributes(weaponInstId, tryGemInstId)
    self:_RefreshSkill(weaponInstId, tryGemInstId, tryCharId)
    self:_RefreshBasicNode(weaponInfo)
    self:_RefreshWeaponTrail(weaponInfo)
end


WeaponInfo.InitInCharInfo = HL.Method(HL.Table) << function(self, weaponInfo)
    self.view.charWeaponBasicNode.gameObject:SetActive(true)
    self.view.weaponBasicNode.gameObject:SetActive(false)
    self.view.gemBasicNode.gameObject:SetActive(false)
    self.view.gemCompareNode.gameObject:SetActive(false)

    self:_InitWeaponInfo(weaponInfo, weaponInfo.weaponExhibitInfo.weaponInst.attachedGemInstId, weaponInfo.charId)
end

WeaponInfo.InitInWeaponExhibit = HL.Method(HL.Table) << function(self, weaponInfo)
    self.view.charWeaponBasicNode.gameObject:SetActive(false)
    self.view.weaponBasicNode.gameObject:SetActive(true)
    self.view.gemBasicNode.gameObject:SetActive(false)
    self.view.gemCompareNode.gameObject:SetActive(false)

    self:_InitWeaponInfo(weaponInfo, weaponInfo.weaponExhibitInfo.weaponInst.attachedGemInstId)
end

WeaponInfo.InitInWeaponExhibitGem = HL.Method(HL.Table, HL.Opt(HL.Number)) << function(self, weaponInfo, tryGemInstId)
    self.view.charWeaponBasicNode.gameObject:SetActive(false)
    self.view.weaponBasicNode.gameObject:SetActive(false)
    self.view.gemBasicNode.gameObject:SetActive(true)
    self.view.gemCompareNode.gameObject:SetActive(false)

    self:_InitWeaponInfo(weaponInfo, tryGemInstId)
end

WeaponInfo.InitInWeaponExhibitGemCompare = HL.Method(HL.Table, HL.Opt(HL.Number)) << function(self, weaponInfo, tryGemInstId)
    self.view.charWeaponBasicNode.gameObject:SetActive(false)
    self.view.weaponBasicNode.gameObject:SetActive(false)
    self.view.gemBasicNode.gameObject:SetActive(false)
    self.view.gemCompareNode.gameObject:SetActive(true)

    self:_InitWeaponInfo(weaponInfo, tryGemInstId)
end

WeaponInfo._RefreshTitleNode = HL.Method(HL.Table) << function(self, weaponExhibitInfo)
    local itemCfg = weaponExhibitInfo.itemCfg
    local templateId = weaponExhibitInfo.weaponInst.templateId
    local instId = weaponExhibitInfo.weaponInst.instId

    self.m_weaponStarCell:Refresh(itemCfg.rarity)
    self.view.titleNode.lockToggle:InitLockToggle(templateId, instId)
    self.view.titleNode.lockToggle.gameObject:SetActive(not self.m_isTrailChar)
end

WeaponInfo._RefreshNameNode = HL.Method(HL.Table) << function(self, weaponExhibitInfo)
    local itemCfg = weaponExhibitInfo.itemCfg
    local weaponCfg = weaponExhibitInfo.weaponCfg
    local weaponTypeInt = weaponCfg.weaponType:ToInt()

    self.view.nameNode.weaponName.text = itemCfg.name
    self.view.nameNode.typeIcon:LoadSprite(UIConst.UI_SPRITE_WEAPON_EXHIBIT, UIConst.WEAPON_EXHIBIT_WEAPON_TYPE_ICON_PREFIX .. weaponTypeInt)
    self.view.nameNode.typeText.text = Language[string.format("LUA_WEAPON_TYPE_%d", weaponTypeInt)]
    UIUtils.setItemRarityImage(self.view.nameNode.rarityColor, weaponExhibitInfo.itemCfg.rarity)
end

WeaponInfo._RefreshBasicNode = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    local weaponExhibitInfo = self.m_weaponInfo.weaponExhibitInfo
    local itemCfg = weaponExhibitInfo.itemCfg

    local weaponBasicNode = self.view.weaponBasicNode
    local charWeaponBasicNode = self.view.charWeaponBasicNode
    local isMaxLv = weaponExhibitInfo.curLv >= weaponExhibitInfo.stageLv

    weaponBasicNode.level.text = weaponExhibitInfo.curLv
    weaponBasicNode.maxLevel.text = weaponExhibitInfo.stageLv
    weaponBasicNode.experienceBar.fillAmount = isMaxLv and 1 or weaponExhibitInfo.curExp / weaponExhibitInfo.nextLvExp
    weaponBasicNode.levelBreakNode:InitLevelBreakNode(weaponExhibitInfo.curBreakthroughLv, false, weaponExhibitInfo.breakthroughInfoList)
    weaponBasicNode.btnPreview.onClick:RemoveAllListeners()
    weaponBasicNode.btnPreview.onClick:AddListener(function()
        UIManager:Open(PanelId.WeaponExhibitPreview, {
            weaponTemplateId = weaponExhibitInfo.weaponInst.templateId,
            weaponInstId = weaponExhibitInfo.weaponInst.instId,
        })
    end)

    local attachedGemInstId = weaponExhibitInfo.weaponInst.attachedGemInstId
    local canEditGem = arg and arg.canEditGem
    charWeaponBasicNode.level.text = weaponExhibitInfo.curLv
    charWeaponBasicNode.maxLevel.text = weaponExhibitInfo.stageLv
    charWeaponBasicNode.weaponGemNode:InitWeaponGemNode(attachedGemInstId, canEditGem == true, function()
        local isInRpgDungeon = Utils.isInRpgDungeon()
        if isInRpgDungeon then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_RPG_DUNGEON_FORBID_CHAR_WEAPON)
            return
        end

        local exhibitInfo = self.m_weaponInfo.weaponExhibitInfo
        local isTrailCard = not CharInfoUtils.isCharDevAvailable(exhibitInfo.weaponInst.equippedCharServerId)
        if isTrailCard then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_WEAPON_GEM_TRAIL_FORBID)
            return
        end

        Notify(MessageConst.CHAR_INFO_BLEND_EXIT, {
            finishCallback = function()
                CharInfoUtils.openWeaponInfoBestWay({
                    weaponTemplateId = weaponExhibitInfo.weaponInst.templateId,
                    weaponInstId = weaponExhibitInfo.weaponInst.instId,
                    pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.GEM,
                    isFocusJump = true,
                })
            end
        })
    end)
    charWeaponBasicNode.levelBreakNode:InitLevelBreakNode(weaponExhibitInfo.curBreakthroughLv, false, weaponExhibitInfo.breakthroughInfoList)
    charWeaponBasicNode.potentialStar:InitWeaponPotentialStar(weaponExhibitInfo.weaponInst.refineLv)

    local canUpgrade = arg.canUpgrade == true
    local canUpgradePotential = arg.canUpgradePotential == true
    charWeaponBasicNode.addImage.gameObject:SetActive(canUpgrade)
    charWeaponBasicNode.dotNode.gameObject:SetActive(canUpgradePotential)

    charWeaponBasicNode.btnUpgrade.interactable = canUpgrade
    charWeaponBasicNode.btnPotential.interactable = canUpgradePotential

    self.m_weaponStarCell:Refresh(itemCfg.rarity)
end

WeaponInfo._RefreshWeaponTrail = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    local weaponExhibitInfo = arg.weaponExhibitInfo
    local isTrailWeapon = CharInfoUtils.checkIsWeaponInTrail(weaponExhibitInfo.weaponInst.instId)
    self.view.weaponBasicNode.btnPreview.gameObject:SetActive(not isTrailWeapon)
end

WeaponInfo._RefreshGemCompareNode = HL.Method(HL.Table, HL.Opt(HL.Number)) << function(self, weaponExhibitInfo, tryGemInstId)
    if tryGemInstId == nil then
        tryGemInstId = weaponExhibitInfo.weaponInst.attachedGemInstId
    end
    if tryGemInstId and tryGemInstId <= 0 then
        return
    end

    local gemInst = CharInfoUtils.getGemByInstId(tryGemInstId)
    local gemTemplateId = gemInst.templateId
    local gemItemCfg = Tables.itemTable:GetValue(gemTemplateId)
    local gemRarity = gemItemCfg.rarity

    self.m_gemStarCell:Refresh(gemRarity)

    self.view.gemCompareNode.gemName.text = gemItemCfg.name
    self.view.gemCompareNode.itemBlack:InitItem({
        id = gemTemplateId,
        instId = tryGemInstId,
    }, true)
end

WeaponInfo._RefreshAttributes = HL.Method(HL.Number, HL.Opt(HL.Number)) << function(self, weaponInstId, tryGemInstId)
    self.view.weaponAttributeNode:InitWeaponAttributeNode(weaponInstId, tryGemInstId)
end

WeaponInfo._RefreshSkill = HL.Method(HL.Number, HL.Opt(HL.Number, HL.String)) << function(self, weaponInstId, tryGemInstId, tryCharId)
    local weaponInst = CharInfoUtils.getWeaponByInstId(weaponInstId)
    self.view.weaponSkillNode:InitWeaponSkillNode(weaponInstId, {
        tryRefineLv = weaponInst.refineLv,
        tryBreakthroughLv = weaponInst.breakthroughLv,
        tryGemInstId = tryGemInstId,
        tryCharId = tryCharId,
    })
end

WeaponInfo._RefreshButtonState = HL.Method(HL.Table) << function(self, weaponInfo)
    if self.m_isTrailChar then
        self.view.buttonNode.gameObject:SetActive(false)
    end
    if self.view.contentStateCtrl then
        self.view.contentStateCtrl:SetState(self.m_isTrailChar and "Preview" or "Normal")
    end
    local useControllerEquipBtn = DeviceInfo.usingController and weaponInfo.isInDetail == true
                                    and self.view.buttonNode.controllerConfirmBtn ~= nil
    self.view.buttonNode.buttonEquip.gameObject:SetActive(weaponInfo.showEquip == true and not useControllerEquipBtn)
    if self.view.buttonNode.controllerConfirmBtn ~= nil then
        self.view.buttonNode.controllerConfirmBtn.gameObject:SetActive(weaponInfo.showEquip == true and useControllerEquipBtn)
    end
    self.view.buttonNode.buttonExpand.gameObject:SetActive(weaponInfo.showExpand == true)

    local showAnyButton = weaponInfo.showEquip == true or weaponInfo.showEnhance == true
    self.view.buttonNode.equippedNodeSmall.gameObject:SetActive(showAnyButton and weaponInfo.showEquipped == true)
    self.view.buttonNode.equippedNodeBig.gameObject:SetActive((not showAnyButton) and weaponInfo.showEquipped == true)
end

HL.Commit(WeaponInfo)
return WeaponInfo
