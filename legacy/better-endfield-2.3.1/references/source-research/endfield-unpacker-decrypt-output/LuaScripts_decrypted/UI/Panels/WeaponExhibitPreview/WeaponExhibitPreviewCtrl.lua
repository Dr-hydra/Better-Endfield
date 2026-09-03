
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeaponExhibitPreview
WeaponExhibitPreviewCtrl = HL.Class('WeaponExhibitPreviewCtrl', uiCtrl.UICtrl)







WeaponExhibitPreviewCtrl.s_messages = HL.StaticField(HL.Table) << {

}


WeaponExhibitPreviewCtrl.m_curBreakthroughLv = HL.Field(HL.Number) << 0

WeaponExhibitPreviewCtrl.m_weaponExhibitInfo = HL.Field(HL.Table)

WeaponExhibitPreviewCtrl.m_requireItemCelLCache = HL.Field(HL.Forward("UIListCache"))

WeaponExhibitPreviewCtrl.m_effectCor = HL.Field(HL.Thread)


WeaponExhibitPreviewCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local weaponTemplateId = arg.weaponTemplateId
    local weaponInstId = arg.weaponInstId
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponTemplateId, weaponInstId)
    local previewBreakthroughLv = weaponExhibitInfo.curBreakthroughLv
    local maxBreak, breakLv2StageLv = CharInfoUtils.getWeaponBreakLv2StageLv(weaponExhibitInfo.weaponInst.templateId)

    previewBreakthroughLv = lume.clamp(previewBreakthroughLv, 0, maxBreak - 1)

    self:_InitActionEvent()

    self.m_weaponExhibitInfo = weaponExhibitInfo
    self.m_curBreakthroughLv = previewBreakthroughLv
    self.m_requireItemCelLCache = UIUtils.genCellCache(self.view.itemSmall)

    self:_PreviewWeaponBreakthrough(weaponExhibitInfo, previewBreakthroughLv)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

WeaponExhibitPreviewCtrl._InitActionEvent = HL.Method() << function(self)
    self.view.btnLeftArrow.onClick:AddListener(function()
        self:_StartPageTransitionTween(function()
            local weaponExhibitInfo = self.m_weaponExhibitInfo
            local breakthroughLv = self.m_curBreakthroughLv - 1

            self.m_curBreakthroughLv = breakthroughLv

            self:_PreviewWeaponBreakthrough(weaponExhibitInfo, breakthroughLv)
        end)
    end)

    self.view.btnRightArrow.onClick:AddListener(function()
        self:_StartPageTransitionTween(function()
            local weaponExhibitInfo = self.m_weaponExhibitInfo
            local breakthroughLv = self.m_curBreakthroughLv + 1

            self.m_curBreakthroughLv = breakthroughLv

            self:_PreviewWeaponBreakthrough(weaponExhibitInfo, breakthroughLv)
        end)
    end)

    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close)
    end)
end

WeaponExhibitPreviewCtrl._StartPageTransitionTween = HL.Method(HL.Function) << function(self, action)
    self:_ClearCoroutine(self.m_effectCor)
    self.m_effectCor = self:_StartCoroutine(function()
        self.view.animation:Play("weapon_view_nextpage_out")
        coroutine.wait(self.view.config.SWITCH_PAGE_DURATION)

        if action then
            action(self)
        end
        self.view.animation:Play("weapon_view_nextpage_in")
    end)
end

WeaponExhibitPreviewCtrl._PreviewWeaponBreakthrough = HL.Method(HL.Table, HL.Number) << function(self, weaponExhibitInfo, breakthroughLv)



    local maxBreak, breakLv2StageLv = CharInfoUtils.getWeaponBreakLv2StageLv(weaponExhibitInfo.weaponInst.templateId)
    local fromBreakthroughLv = breakthroughLv
    local breakthroughTemplate = weaponExhibitInfo.breakthroughTemplateCfg

    local toBreakthroughLv = fromBreakthroughLv + 1
    local toBreakthroughCfg = breakthroughTemplate.list[toBreakthroughLv]

    self.view.levelBreakNode:InitLevelBreakNode(fromBreakthroughLv, true, weaponExhibitInfo.breakthroughInfoList)

    local breakItemList = toBreakthroughCfg.breakItemList
    self.m_requireItemCelLCache:Refresh(breakItemList.Count, function(cell, index)
        local itemInfo = breakItemList[CSIndex(index)]
        cell:InitItem({
            id = itemInfo.id,
            count = itemInfo.count,
        }, true)
        cell:SetExtraInfo({
            isSideTips = DeviceInfo.usingController,
        })
    end)

    self.view.fromWeaponSkillNode:InitWeaponSkillNode(weaponExhibitInfo.weaponInst.instId, {
        tryGemInstId = 0,
        tryRefineLv = weaponExhibitInfo.weaponInst.refineLv,
        tryBreakthroughLv = fromBreakthroughLv,
        fromBreakthroughLv = fromBreakthroughLv,
        fromRefineLv = weaponExhibitInfo.weaponInst.refineLv,
    })

    self.view.toWeaponSkillNode:InitWeaponSkillNode(weaponExhibitInfo.weaponInst.instId, {
        tryGemInstId = 0,
        tryRefineLv = weaponExhibitInfo.weaponInst.refineLv,
        tryBreakthroughLv = toBreakthroughLv,
        fromBreakthroughLv = fromBreakthroughLv,
        fromRefineLv = weaponExhibitInfo.weaponInst.refineLv,
    })
    self.view.btnLeftArrow.gameObject:SetActive(fromBreakthroughLv > 0)
    self.view.btnRightArrow.gameObject:SetActive(toBreakthroughLv < weaponExhibitInfo.maxBreakthroughLv)


    local fromStageLv = breakLv2StageLv[fromBreakthroughLv]
    local toStageLv = breakLv2StageLv[toBreakthroughLv]
    self.view.fromLv.text = fromStageLv
    self.view.fromStageLv.text = fromStageLv
    self.view.toLv.text = fromStageLv
    self.view.toStageLv.text = toStageLv
end

HL.Commit(WeaponExhibitPreviewCtrl)
