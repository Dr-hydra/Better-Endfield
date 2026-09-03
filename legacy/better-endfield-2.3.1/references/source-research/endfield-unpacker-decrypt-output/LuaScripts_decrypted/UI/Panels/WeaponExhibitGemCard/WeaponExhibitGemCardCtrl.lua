
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeaponExhibitGemCard
WeaponExhibitGemCardCtrl = HL.Class('WeaponExhibitGemCardCtrl', uiCtrl.UICtrl)







WeaponExhibitGemCardCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.WEAPON_EXHIBIT_REFRESH_GEM_CARD] = "RefreshGemCard",
    [MessageConst.CLOSE_WEAPON_EXHIBIT_GEM_CARD] = "PlayAnimationOut",
}

WeaponExhibitGemCardCtrl.m_gemInstIdLeft = HL.Field(HL.Number) << -1

WeaponExhibitGemCardCtrl.m_gemInstIdRight = HL.Field(HL.Number) << -1


WeaponExhibitGemCardCtrl.m_weaponInfo = HL.Field(HL.Table)

WeaponExhibitGemCardCtrl.m_effectCor = HL.Field(HL.Thread)


WeaponExhibitGemCardCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local weaponInfo = arg.weaponInfo
    self.m_weaponInfo = weaponInfo

    self.view.gemCardLeft.gameObject:SetActive(false)
    self.view.gemCardRight.gameObject:SetActive(false)
    self:_InitController()
end

WeaponExhibitGemCardCtrl.RefreshGemCard = HL.Method(HL.Any) << function(self, arg)
    local hasGem = arg.hasGem
    local equippedGemInstId = arg.equippedGemInstId
    local selectGemInstId = arg.selectGemInstId
    local tryWeaponInstId = self.m_weaponInfo.weaponInstId

    local hasSelectedGem = selectGemInstId and selectGemInstId > 0
    local hasEquippedGem = equippedGemInstId and equippedGemInstId > 0

    local gemInstIdLeft = hasEquippedGem and equippedGemInstId or selectGemInstId
    local hasLeftGem = gemInstIdLeft and gemInstIdLeft > 0

    self.view.gemCardNode.gameObject:SetActive(hasGem)
    self.view.gemCardLeft.gameObject:SetActive(hasLeftGem)


    local isDifferentGem = hasSelectedGem and hasEquippedGem and selectGemInstId ~= equippedGemInstId
    if self.view.gemCardRight.gameObject.activeSelf ~= isDifferentGem or
        self.view.gemCardRight.view.animationWrapper.curState ~= CS.Beyond.UI.UIConst.AnimationState.Stop then
        UIUtils.PlayAnimationAndToggleActive(self.view.gemCardRight.view.animationWrapper, isDifferentGem)
    end



    self.view.gemCardLeft.view.weaponInlayNode.gameObject:SetActive(hasEquippedGem)
    if DeviceInfo.usingController then
        self.view.gemCardRight:ActiveToggleGroup(isDifferentGem)
        self.view.gemCardLeft:ActiveToggleGroup(not isDifferentGem)
    end

    if isDifferentGem then
        if self.m_gemInstIdRight ~= selectGemInstId then
            self.m_gemInstIdRight = selectGemInstId
            self.view.gemCardRight:InitGemCard(selectGemInstId, tryWeaponInstId)
            self:_RefreshGemPerfectIcon(self.view.gemCardRight, selectGemInstId)
            self.view.gemCardRight.view.animationWrapper:ClearTween()
            self.view.gemCardRight.view.animationWrapper:PlayInAnimation()
        end


    else
        if self.m_gemInstIdLeft ~= gemInstIdLeft then
            if self.m_gemInstIdLeft > 0 then

                self.m_effectCor = self:_ClearCoroutine(self.m_effectCor)
                self.m_effectCor = self:_StartCoroutine(function()
                    self.view.gemCardLeft.view.animationWrapper:ClearTween()
                    self.view.gemCardLeft.view.animationWrapper:PlayOutAnimation()
                    coroutine.wait(0.1)
                    self.view.gemCardLeft:InitGemCard(gemInstIdLeft, tryWeaponInstId)
                    self:_RefreshGemPerfectIcon(self.view.gemCardLeft, gemInstIdLeft)
                    self.view.gemCardLeft.view.animationWrapper:PlayInAnimation()
                end)


            else
                self.view.gemCardLeft.view.animationWrapper:ClearTween()
                self.view.gemCardLeft.view.animationWrapper:PlayInAnimation()
                self.view.gemCardLeft:InitGemCard(gemInstIdLeft, tryWeaponInstId)
                self:_RefreshGemPerfectIcon(self.view.gemCardLeft, gemInstIdLeft)
            end

            self.m_gemInstIdLeft = gemInstIdLeft
        else


        end
    end

    if hasLeftGem then
        self:_RefreshGemPerfectIcon(self.view.gemCardLeft, gemInstIdLeft)
    else
        self:_RefreshGemPerfectIcon(self.view.gemCardLeft, -1)
    end
    if isDifferentGem then
        self:_RefreshGemPerfectIcon(self.view.gemCardRight, selectGemInstId)
    else
        self:_RefreshGemPerfectIcon(self.view.gemCardRight, -1)
    end
end

WeaponExhibitGemCardCtrl._RefreshGemPerfectIcon = HL.Method(HL.Any, HL.Any) << function(self, gemCard, gemInstId)
    if not gemCard.view.gemNode then
        return
    end

    local matchesCurWeapon = false
    if gemInstId and gemInstId > 0 then
        local isPerfectMatch, matchList = UIUtils.getGemWishListPerfectMatch(gemInstId)
        if isPerfectMatch and matchList then
            local curWeaponTemplateId = self.m_weaponInfo.weaponTemplateId
            for _, weaponTemplateId in ipairs(matchList) do
                if weaponTemplateId == curWeaponTemplateId then
                    matchesCurWeapon = true
                    break
                end
            end
        end
    end

    gemCard.view.gemNode.gameObject:SetActive(matchesCurWeapon == true)
    gemCard.view.leftDeco01.gameObject:SetActive(matchesCurWeapon ~= true)
end

WeaponExhibitGemCardCtrl._InitController = HL.Method() << function(self)
    local toggleFocusInputGroup = function(active)
        local weaponGemPhaseItem = self.m_phase:_GetPanelPhaseItem(PanelId.WeaponExhibitGem)
        if weaponGemPhaseItem then
            weaponGemPhaseItem.uiCtrl:ToggleFocusInputGroup(active)
        end
    end
    self.view.gemCardLeft.view.naviGroup.onIsFocusedChange:AddListener(function(isFocused)
        toggleFocusInputGroup(not isFocused)
    end)
    self.view.gemCardRight.view.naviGroup.onIsFocusedChange:AddListener(function(isFocused)
        toggleFocusInputGroup(not isFocused)
    end)
end

HL.Commit(WeaponExhibitGemCardCtrl)
