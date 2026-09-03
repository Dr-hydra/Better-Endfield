
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WikiWeaponPreview

WikiWeaponPreviewCtrl = HL.Class('WikiWeaponPreviewCtrl', uiCtrl.UICtrl)

local WeaponState = {
    Init = 1,
    Max = 2,
    Gem = 3,
}





WikiWeaponPreviewCtrl.s_messages = HL.StaticField(HL.Table) << {

}

WikiWeaponPreviewCtrl.m_wikiEntryShowData = HL.Field(HL.Table)

WikiWeaponPreviewCtrl.m_arg = HL.Field(HL.Table)

WikiWeaponPreviewCtrl.m_wikiGroupShowDataList = HL.Field(HL.Table)

WikiWeaponPreviewCtrl.m_curWeaponState = HL.Field(HL.Number) << 0


WikiWeaponPreviewCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg or {}
    self:_ProcessArg(arg)
    self:_InitAction()
    self:_InitController()
    self:_RefreshLeft()
    self:_RefreshCenter()
    self:_ApplyResumeState(self.m_arg and self.m_arg.resumeState or nil)
    if self.m_arg then
        self.m_arg.resumeState = nil
    end

    self:_RefreshModel(true)
    self.m_phase:PlayBgAnim("wiki_plane_toweapon_in")
    self:_PlayDecoAnim()
end

WikiWeaponPreviewCtrl.OnShow = HL.Override() << function(self)
    self:_RefreshModel()
    self:_PlayDecoAnim()
end

WikiWeaponPreviewCtrl.OnHide = HL.Override() << function(self)
    if self.m_phase then
        self.m_phase:ActiveModelRotateRoot(false)
    end
end

WikiWeaponPreviewCtrl.OnClose = HL.Override() << function (self)
    if self.m_phase then
        self.m_phase:DestroyModel()
    end
end

WikiWeaponPreviewCtrl._OnPlayAnimationOut = HL.Override() << function(self)
    WikiWeaponPreviewCtrl.Super._OnPlayAnimationOut(self)
    self.m_phase:PlayBgAnim("wiki_plane_toweapon_out")
end

WikiWeaponPreviewCtrl._ProcessArg = HL.Method(HL.Table) << function(self, arg)

    local detailArgs = {
        categoryType = WikiConst.EWikiCategoryType.Weapon,
        wikiEntryShowData = WikiUtils.getWikiEntryShowData(arg.weaponId, WikiConst.EWikiCategoryType.Weapon),
        wikiGroupShowDataList = {},
    }
    if arg.weaponGroups == nil or #arg.weaponGroups == 0 then
        self.view.expandListBtn.gameObject:SetActive(false)
    end

    if arg.weaponGroups then
        for _, weaponGroupData in pairs(arg.weaponGroups) do

            local wikiGroupShowData = {
                wikiCategoryType = WikiConst.EWikiCategoryType.Weapon,
                customTitle = weaponGroupData.title,
                wikiEntryShowDataList = {},
            }
            for _, weaponId in ipairs(weaponGroupData.weaponIds) do
                local wikiEntryShowData = WikiUtils.getWikiEntryShowData(weaponId, WikiConst.EWikiCategoryType.Weapon)
                if wikiEntryShowData then
                    local _, itemData = Tables.itemTable:TryGetValue(weaponId)
                    if itemData then
                        wikiEntryShowData.itemData = itemData
                        wikiEntryShowData.rarity = itemData.rarity
                    end
                    table.insert(wikiGroupShowData.wikiEntryShowDataList, wikiEntryShowData)
                    table.sort(wikiGroupShowData.wikiEntryShowDataList, Utils.genSortFunction({ "rarity" }, false))
                end
            end
            table.insert(detailArgs.wikiGroupShowDataList, wikiGroupShowData)
        end
    end

    self.m_wikiEntryShowData = detailArgs.wikiEntryShowData
    self.m_wikiGroupShowDataList = detailArgs.wikiGroupShowDataList
end

WikiWeaponPreviewCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_arg and lume.deepCopy(self.m_arg) or {}
    arg.weaponId = self.m_wikiEntryShowData and self.m_wikiEntryShowData.wikiEntryData.refItemId or arg.weaponId
    arg.resumeState = {
        weaponState = self.m_curWeaponState,
        groupItemList = self.view.left and self.view.left.GetResumeState and self.view.left:GetResumeState() or nil,
    }
    return arg
end

WikiWeaponPreviewCtrl._InitAction = HL.Method() << function(self)
    local defaultWeaponStateIndex = self.m_arg.resumeState and self.m_arg.resumeState.weaponState or WeaponState.Gem
    self.view.topNode.btnClose.onClick:AddListener(function()
        local fadeTimeBoth = UIConst.CHAR_INFO_TRANSITION_BLACK_SCREEN_DURATION
        local dynamicFadeData = UIUtils.genDynamicBlackScreenMaskData("closeWeaponPreview", fadeTimeBoth, fadeTimeBoth, function()
            PhaseManager:ExitPhaseFast(PhaseId.Wiki)
        end)
        GameAction.ShowBlackScreen(dynamicFadeData)
    end)
    self.view.topNode.stateToggle:InitCommonToggleGroup({
        toggleDataList = {
            { name = Language.ui_CharInfo_init },
            { name = Language.ui_CharInfo_max_level },
            { name = Language.ui_WikiWeapon_gem_equiped },
        },
        onToggleIsOn = function(index)
            self:_RefreshWeaponState(index, true)
        end,
        defaultIndex = defaultWeaponStateIndex,
        defaultNotCall = true,
    })
    self:_RefreshWeaponState(defaultWeaponStateIndex, false)
end

WikiWeaponPreviewCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState then
        return
    end
    if self.view.left and self.view.left.ApplyResumeState then
        self.view.left:ApplyResumeState(resumeState.groupItemList)
    end
end

WikiWeaponPreviewCtrl._RefreshWeaponState = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, weaponState, playAnim)
    if self.m_curWeaponState == weaponState then
        return
    end
    self.m_curWeaponState = weaponState
    if playAnim then
        self.view.right.recipeNodeAnimWrapper:PlayOutAnimation(function()
            self:_RefreshModel()
            self:_RefreshRight()
            self.view.right.recipeNodeAnimWrapper:PlayInAnimation()
        end)
    else
        self:_RefreshModel()
        self:_RefreshRight()
    end
end

WikiWeaponPreviewCtrl._RefreshModel = HL.Method(HL.Opt(HL.Boolean)) << function(self, playInAnim)
    if self.m_phase then
        self.m_phase:ShowModel(self.m_wikiEntryShowData, {
            isWeaponRefinedMax = self.m_curWeaponState ~= WeaponState.Init,
            isWeaponGemMax = self.m_curWeaponState == WeaponState.Gem,
            playInAnim = playInAnim,
        })
        self.m_phase:ActiveEntryVirtualCamera(true)
    end
end

WikiWeaponPreviewCtrl._RefreshLeft = HL.Method() << function(self)

    local wikiGroupItemListArgs = {
        isPreviewMode = true,
        isInitHidden = true,
        wikiGroupShowDataList = self.m_wikiGroupShowDataList,
        onItemClicked = function(wikiEntryShowData)
            self.m_wikiEntryShowData = wikiEntryShowData
            self:_RefreshCenter()
            self:_RefreshRight(true)
        end,
        onGetSelectedEntryShowData = function()
            return self.m_wikiEntryShowData
        end,
        btnExpandList = self.view.expandListBtn,
        btnClose = self.view.btnEmpty,
        wikiItemInfo = self.view.wikiItemInfo,
    }
    self.view.left:InitWikiGroupItemList(wikiGroupItemListArgs)
end

WikiWeaponPreviewCtrl._RefreshCenter = HL.Method() << function(self)

    local args = {
        wikiEntryShowData = self.m_wikiEntryShowData,
        onDetailBtnClick = function()
            self:PlayAnimationOutWithCallback(function()
                self.m_phase:CreatePhasePanelItem(PanelId.WikiModelShow, self.m_wikiEntryShowData)
            end)
        end
    }
    self.view.wikiItemInfo:InitWikiItemInfo(args)
    self.view.wikiItemInfo.view.animationWrapper:PlayInAnimation()
    self:_RefreshModel()
end

WikiWeaponPreviewCtrl._RefreshRight = HL.Method(HL.Opt(HL.Boolean)) << function(self, playAnim)
    if playAnim then
        self.view.right.recipeNodeAnimWrapper:PlayOutAnimation(function()
            self:_RefreshWeaponShowInfo(self:_GetWeaponShowData(self.m_curWeaponState))
            self.view.right.recipeNodeAnimWrapper:PlayInAnimation()
        end)
    else
        self:_RefreshWeaponShowInfo(self:_GetWeaponShowData(self.m_curWeaponState))
    end
end

WikiWeaponPreviewCtrl._GetWeaponShowData = HL.Method(HL.Number).Return(HL.Table) << function(self, weaponState)
    local templateId = self.m_wikiEntryShowData.wikiEntryData.refItemId
    local maxLevel, initMaxLevel, breakThroughCount, maxBreakthroughLevel, maxRefineLevel = 0, 0, 0, 0, 0

    local _, weaponBasicData = Tables.weaponBasicTable:TryGetValue(templateId)

    if weaponBasicData then
        maxLevel = weaponBasicData.maxLv
        local _, weaponBreakThroughDetailList = Tables.weaponBreakThroughTemplateTable:TryGetValue(weaponBasicData.breakthroughTemplateId)
        if weaponBreakThroughDetailList then
            breakThroughCount = #weaponBreakThroughDetailList.list
            if breakThroughCount > 1 then
                initMaxLevel = weaponBreakThroughDetailList.list[1].breakthroughLv
                maxBreakthroughLevel = breakThroughCount - 1
            end
        end
        local _, weaponTalentDetailList = Tables.weaponTalentTemplateTable:TryGetValue(weaponBasicData.talentTemplateId)
        if weaponTalentDetailList then
            maxRefineLevel = #weaponTalentDetailList.list
        end
    end

    local isMaxLevel = weaponState ~= WeaponState.Init

    local weaponShowData = {
        templateId = templateId,
        level = isMaxLevel and maxLevel or 1,
        maxLevel = isMaxLevel and maxLevel or initMaxLevel,
        breakthroughLevel = isMaxLevel and maxBreakthroughLevel or 0,
        maxBreakthroughLevel = maxBreakthroughLevel,
        refineLevel = isMaxLevel and maxRefineLevel or 0,
    }
    return weaponShowData
end

WikiWeaponPreviewCtrl._RefreshWeaponShowInfo = HL.Method(HL.Table) << function(self, weaponShowData)
    local view = self.view.right
    local isMax = weaponShowData.level == weaponShowData.maxLevel
    local isGemMax = self.m_curWeaponState == WeaponState.Gem
    view.levelText.text = tostring(weaponShowData.level)
    view.maxText.text = string.format("/%d", weaponShowData.maxLevel)
    view.levelBreakNode:InitLevelBreakNodeSimple(weaponShowData.breakthroughLevel, weaponShowData.maxBreakthroughLevel, false)
    view.weaponAttributeNode:InitWeaponAttributeNodeByTemplateId(weaponShowData.templateId, isMax)
    view.weaponSkillNode:InitWeaponSkillNodeByTemplateId(weaponShowData.templateId, weaponShowData.breakthroughLevel,
        weaponShowData.refineLevel, isGemMax)
    view.potentialStar:InitWeaponPotentialStar(weaponShowData.refineLevel)
    view.potentialStar.view.breakthroughBg.gameObject:SetActive(not isMax)

    local gemInst
    if self.m_curWeaponState == WeaponState.Gem then
        gemInst = CS.Beyond.Gameplay.InventorySystem.CreateWeaponPerfectGemInst(weaponShowData.templateId)
    end
    view.weaponGemSlimNode:InitWeaponGemSlimeNodeByInst(gemInst)
end

WikiWeaponPreviewCtrl._InitController = HL.Virtual() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    UIUtils.bindHyperlinkPopup(self, "wiki_detail_right", self.view.inputGroup.groupId)
end

WikiWeaponPreviewCtrl._PlayDecoAnim = HL.Method() << function(self)
    if self.m_phase then
        self.m_phase:PlayDecoAnim("wiki_uideco_grouptoweaponpanel")
    end
end

HL.Commit(WikiWeaponPreviewCtrl)
