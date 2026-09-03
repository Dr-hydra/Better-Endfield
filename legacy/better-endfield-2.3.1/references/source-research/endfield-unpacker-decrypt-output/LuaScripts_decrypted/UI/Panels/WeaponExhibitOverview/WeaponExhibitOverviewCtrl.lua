
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeaponExhibitOverview
WeaponExhibitOverviewCtrl = HL.Class('WeaponExhibitOverviewCtrl', uiCtrl.UICtrl)





WeaponExhibitOverviewCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ITEM_LOCKED_STATE_CHANGED] = 'OnItemLockedStateChanged',
}

WeaponExhibitOverviewCtrl.m_arg = HL.Field(HL.Table)

WeaponExhibitOverviewCtrl.m_weaponInfo = HL.Field(HL.Table)

WeaponExhibitOverviewCtrl.m_rotateTickKey = HL.Field(HL.Number) << -1

WeaponExhibitOverviewCtrl.OnItemLockedStateChanged = HL.Method(HL.Table) << function (self, arg)
    local weaponInfo = self.m_weaponInfo

    self.view.weaponIntroduction:InitWeaponIntroduction(weaponInfo.weaponTemplateId, weaponInfo.weaponInstId)
end

WeaponExhibitOverviewCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    local weaponInfo = arg.weaponInfo
    local phase = arg.phase

    self.m_weaponInfo = weaponInfo

    self.view.noUINode.gameObject:SetActive(false)
    self:_InitActionEvent()
    self:_InitController()
end

WeaponExhibitOverviewCtrl.OnShow = HL.Override() << function(self)
    local weaponInfo = self.m_weaponInfo

    self:_RefreshOverviewPanel(weaponInfo.weaponInstId, weaponInfo.weaponTemplateId)

    if self.m_arg.stateArg and self.m_arg.stateArg.isBtnFullSkillClicked then
        self.view.btnFullSkill.onClick:Invoke()
    end
end

WeaponExhibitOverviewCtrl.OnClose = HL.Override() << function(self)
    LuaUpdate:Remove(self.m_rotateTickKey)
end

WeaponExhibitOverviewCtrl._InitActionEvent = HL.Method() << function(self)
    self.view.btnBack.onClick:AddListener(function()
        if self.view.touchPanel.gameObject.activeSelf then
            self:_ToggleUI(true)
        else

            Notify(MessageConst.WEAPON_EXHIBIT_BLEND_EXIT, {
                finishCallback = function()
                    PhaseManager:ExitPhaseFast(PhaseId.WeaponInfo)
                end
            })

            self:PlayAnimationOut()
        end
    end)

    self.view.btnBackNoUI.onClick:AddListener(function()
        if self.view.touchPanel.gameObject.activeSelf then
            self:_ToggleUI(true)
        else

            Notify(MessageConst.WEAPON_EXHIBIT_BLEND_EXIT, {
                finishCallback = function()
                    PhaseManager:ExitPhaseFast(PhaseId.WeaponInfo)
                end
            })

            self:PlayAnimationOut()
        end
    end)
    self.view.btnUpgrade.onClick:AddListener(function()
        self:_OnClickUpgradeButton()
    end)

    self.view.touchPanel.onDrag:AddListener(function(eventData)
        local delta = eventData.delta
        local phase = self.m_phase
        phase:RotateWeapon(delta.x)
    end)

    self.view.btnDocument.onClick:AddListener(function()
        self:Notify(MessageConst.WEAPON_EXHIBIT_PAGE_CHANGE, {
            pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.DOCUMENT,
        })
    end)

    self.view.btnHideUI.onClick:AddListener(function()
        self.view.leftBottomNaviGroup:ManuallyStopFocus()
        self:_ToggleUI(false)
    end)

    self.view.btnPotential.onClick:AddListener(function()
        self:Notify(MessageConst.WEAPON_EXHIBIT_PAGE_CHANGE, {
            pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.POTENTIAL,
        })
    end)

    self.view.btnFullSkill.onClick:AddListener(function()
        UIManager:Open(PanelId.WeaponSkillDetail, {
            weaponInstId = self.m_weaponInfo.weaponInstId,
        })
    end)
end

WeaponExhibitOverviewCtrl._PlayCustomAnimationOut = HL.Method() << function(self)
    UIUtils.PlayAnimationAndToggleActive(self.view.leftNode, false)
    UIUtils.PlayAnimationAndToggleActive(self.view.rightNode, false, function()
        PhaseManager:ExitPhaseFast(PhaseId.WeaponInfo)
    end)
end

WeaponExhibitOverviewCtrl._OnClickUpgradeButton = HL.Method() << function(self)
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(self.m_weaponInfo.weaponTemplateId, self.m_weaponInfo.weaponInstId)
    local isShowUpgrade = weaponExhibitInfo.curLv < weaponExhibitInfo.maxLv
    if not isShowUpgrade then
        return
    end

    local isInFight = Utils.isInFight()
    if isInFight then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_UPGRADE_IN_FIGHT_FORBID_INTERACT_TOAST)
        return
    end

    local isTrailCard = not CharInfoUtils.isCharDevAvailable(weaponExhibitInfo.weaponInst.equippedCharServerId)
    if isTrailCard then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_WEAPON_UPGRADE_TRAIL_FORBID)
        return
    end

    self:Notify(MessageConst.WEAPON_EXHIBIT_PAGE_CHANGE, {
        pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.UPGRADE,
    })
end

WeaponExhibitOverviewCtrl._GetOverviewHintId = HL.Method().Return(HL.String) << function(self)
    local weaponTemplateId = self.m_weaponInfo.weaponTemplateId
    local weaponInstId = self.m_weaponInfo.weaponInstId
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponTemplateId, weaponInstId)


    local isShowUpgrade = weaponExhibitInfo.curLv < weaponExhibitInfo.maxLv
    if not isShowUpgrade then
        return ""
    end

    local isBreak = weaponExhibitInfo.curLv >= weaponExhibitInfo.stageLv and weaponExhibitInfo.curLv <= weaponExhibitInfo.maxLv
    return isBreak and "LUA_CHAR_BREAK" or "ui_wpn_exhibit_overview_upgrade"
end

WeaponExhibitOverviewCtrl._GetGemHintId = HL.Method().Return(HL.String) << function(self)
    return "ui_wpn_exhibit_gem_add"
end

WeaponExhibitOverviewCtrl._OnClickOverviewTab = HL.Method() << function(self)
    self:Notify(MessageConst.WEAPON_EXHIBIT_PAGE_CHANGE, {
        pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.OVERVIEW,
    })
end

WeaponExhibitOverviewCtrl._OnClickGemTab = HL.Method() << function(self)

    self:Notify(MessageConst.WEAPON_EXHIBIT_PAGE_CHANGE, {
        pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.GEM,
    })
end

WeaponExhibitOverviewCtrl._OnClickTalentTab = HL.Method() << function(self)
    self:Notify(MessageConst.SHOW_TOAST, Language.LUA_FEATURE_NOT_AVAILABLE)
end

WeaponExhibitOverviewCtrl._OnClickDocumentTab = HL.Method() << function(self)
    self:Notify(MessageConst.WEAPON_EXHIBIT_PAGE_CHANGE, {
        pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.DOCUMENT,
    })
end

WeaponExhibitOverviewCtrl._RefreshOverviewPanel = HL.Method(HL.Number, HL.String) << function(self, weaponInstId, weaponTemplateId)
    local _, weaponItemCfg = Tables.itemTable:TryGetValue(weaponTemplateId)
    if not weaponItemCfg then
        logger.error("WeaponExhibitOverview->Can't find weaponItem ID: " .. weaponTemplateId)
        return
    end
    local _, weaponCfg = Tables.weaponBasicTable:TryGetValue(weaponTemplateId)
    if not weaponCfg then
        logger.error("WeaponExhibitOverview->Can't get weapon basic info, templateId: " .. weaponTemplateId)
        return
    end
    local weaponInfo = CharInfoUtils.getWeaponInstInfo(weaponInstId)
    local weaponExhibitInfo = weaponInfo.weaponExhibitInfo
    local curGemId = weaponExhibitInfo.gemInst and weaponExhibitInfo.gemInst.instId or -1

    self.view.weaponInfo:InitInWeaponExhibit(weaponInfo)
    self.view.potentialStar:InitWeaponPotentialStar(weaponExhibitInfo.weaponInst.refineLv, {
        showMaxPotentialHint = true,
    })
    self.view.weaponGemNode:InitWeaponGemNode(curGemId, true, function()
        self:Notify(MessageConst.WEAPON_EXHIBIT_PAGE_CHANGE, {
            pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.GEM,
        })
    end)
    self.view.weaponIntroduction:InitWeaponIntroduction(weaponTemplateId, weaponInstId)

    self.view.title.text = string.format(Language.LUA_WEAPON_EXHIBIT_OVERVIEW_TITLE, weaponItemCfg.name)

    local isShowUpgrade = weaponExhibitInfo.curLv < weaponExhibitInfo.maxLv
    self.view.btnUpgrade.gameObject:SetActive(isShowUpgrade)
    self.view.redDot.gameObject:SetActive(false)
    if isShowUpgrade then
        local isBreak = weaponExhibitInfo.curLv >= weaponExhibitInfo.stageLv and weaponExhibitInfo.curLv <= weaponExhibitInfo.maxLv
        self.view.btnUpgrade.text = isBreak and Language.LUA_WEAPON_EXHIBIT_BREAKTHROUGH or Language.LUA_WEAPON_EXHIBIT_UPGRADE
    end

    self:_RefreshTrailWeapon(weaponInstId)
end


WeaponExhibitOverviewCtrl._RefreshTrailWeapon = HL.Method(HL.Number) << function(self, weaponInstId)
    local isTrailWeapon = CharInfoUtils.checkIsWeaponInTrail(weaponInstId)
    if isTrailWeapon then
        self.view.btnUpgrade.gameObject:SetActive(false)
        self.view.btnPotential.gameObject:SetActive(false)
        self.view.weaponGemNode.gameObject:SetActive(false)
        self.view.btnDocument.gameObject:SetActive(false)
    end
end

WeaponExhibitOverviewCtrl._RefreshBasicNode = HL.Method(HL.Number, HL.String) << function(self, weaponInstId, weaponTemplateId)
    local basicNode = self.view.basicNode
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponTemplateId, weaponInstId)

    basicNode.curLv.text = weaponExhibitInfo.curLv
    basicNode.shadowCurLv.text = weaponExhibitInfo.curLv
    basicNode.maxLv.text = "/ " .. weaponExhibitInfo.stageLv
    basicNode.shadowMaxLv.text = "/ " .. weaponExhibitInfo.stageLv

    basicNode.levelBreakNode:InitLevelBreakNode(weaponExhibitInfo.curBreakthroughLv, false, weaponExhibitInfo.breakthroughInfoList)
    basicNode.btnBreakthroughPreview.gameObject:SetActive(weaponExhibitInfo.curBreakthroughLv < weaponExhibitInfo.maxBreakthroughLv)
end

WeaponExhibitOverviewCtrl._RefreshGemNode = HL.Method(HL.Number, HL.String) << function(self, weaponInstId, weaponTemplateId)
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponTemplateId, weaponInstId)

    self.view.weaponGemNode:InitWeaponGemNode(weaponExhibitInfo.gemInst and weaponExhibitInfo.gemInst.instId or 0)
end

WeaponExhibitOverviewCtrl._RefreshAttributeNode = HL.Method(HL.Number, HL.String) << function(self, weaponInstId, weaponTemplateId)
    self.view.weaponAttributeNode:InitWeaponAttributeNode(weaponInstId)
end

WeaponExhibitOverviewCtrl._RefreshSkillNode = HL.Method(HL.Number, HL.String) << function(self, weaponInstId, weaponTemplateId)
    self.view.weaponSkillNode:InitWeaponSkillNode(weaponInstId)
end

WeaponExhibitOverviewCtrl._RefreshAttributeCell = HL.Method(HL.Table, HL.Table) << function(self, cell, attributeInfo)
    local attributeKey = Const.ATTRIBUTE_TYPE_2_ATTRIBUTE_DATA_KEY[attributeInfo.attributeType]

    cell.attribute.mainText.text = attributeInfo.showName
    cell.attribute.numText.text = "+" .. attributeInfo.showValue
    cell.attribute.attributeIcon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, attributeInfo.iconName)
end


WeaponExhibitOverviewCtrl._ToggleUI = HL.Method(HL.Boolean) << function(self, isOn)
    local phase = self.m_phase

    self.view.touchPanel.gameObject:SetActive(not isOn)

    local wrapper = self.animationWrapper
    if isOn then
        wrapper:PlayInAnimation()
    else
        wrapper:PlayOutAnimation()
    end
    UIUtils.PlayAnimationAndToggleActive(self.view.noUINode, not isOn)

    if DeviceInfo.usingController then
        if isOn then
            self.m_rotateTickKey = LuaUpdate:Remove(self.m_rotateTickKey)
        else
            self.m_rotateTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
                local stickValue = InputManagerInst:GetGamepadStickValue(false)
                if stickValue.x ~= 0 then
                    phase:RotateWeapon(stickValue.x * self.view.config.CONTROLLER_ROTATE_SENSITIVITY * deltaTime)
                end
            end)
        end
    end

    phase:ResetWeaponRotation()
    phase:_ToggleWeaponEquippedMarker(isOn)

    if DeviceInfo.usingController then
        self.view.mainInputGroup.enabled = isOn
        self.view.noUINodeInputGroup.enabled = not isOn
    end
end

WeaponExhibitOverviewCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.leftTopNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        self.view.controllerFocusHintNode.gameObject:SetActive(not isFocused)
    end)
    UIUtils.bindHyperlinkPopup(self, "WeaponSkill", self.view.mainInputGroup.groupId)
end

WeaponExhibitOverviewCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    local arg = {
        isBtnFullSkillClicked = UIManager:IsShow(PanelId.WeaponSkillDetail),
    }
    return arg
end

HL.Commit(WeaponExhibitOverviewCtrl)
