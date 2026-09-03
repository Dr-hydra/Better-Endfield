
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WikiModelShow
WikiModelShowCtrl = HL.Class('WikiModelShowCtrl', uiCtrl.UICtrl)





WikiModelShowCtrl.s_messages = HL.StaticField(HL.Table) << {

}

WikiModelShowCtrl.m_rotateTickKey = HL.Field(HL.Number) << -1

WikiModelShowCtrl.m_starListCache = HL.Field(HL.Forward("UIListCache"))

WikiModelShowCtrl.m_wikiEntryShowData = HL.Field(HL.Table)




WikiModelShowCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})


    local wikiEntryShowData = arg
    self.m_wikiEntryShowData = wikiEntryShowData
    self.view.backBtn.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self.m_phase:RemovePhasePanelItemById(PANEL_ID)
        end)
    end)


    local hasValue

    local itemData
    hasValue, itemData = Tables.itemTable:TryGetValue(wikiEntryShowData.wikiEntryData.refItemId)
    if hasValue then
        self.view.nameTxt.text = itemData.name
        UIUtils.setItemRarityImage(self.view.circleLightImg, itemData.rarity)
        UIUtils.setItemRarityImage(self.view.circleImg, itemData.rarity)
    end
    local monsterData = nil
    hasValue, monsterData = Tables.enemyTemplateDisplayInfoTable:TryGetValue(wikiEntryShowData.wikiEntryData.refMonsterTemplateId)
    if hasValue then
        self.view.nameTxt.text = monsterData.name
    end
    self.view.circleLightImg.gameObject:SetActive(not hasValue)
    self.view.typeTxt.text = wikiEntryShowData.wikiGroupData.groupName

    local isShowStar = wikiEntryShowData.wikiCategoryType == WikiConst.EWikiCategoryType.Weapon
    self.view.star.gameObject:SetActive(isShowStar)
    if isShowStar then
        self.m_starListCache = UIUtils.genCellCache(self.view.starCell)
        self.m_starListCache:Refresh(itemData.rarity)
    end


    self.view.touchPanel.onDrag:AddListener(function(eventData)
        local delta = eventData.delta
        self.m_phase:RotateModel(delta.x * self.view.config.ROTATE_SENSITIVITY)
    end)
    self:_StartCoroutine(function()
        self.m_rotateTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
            local stickValue = InputManagerInst:GetGamepadStickValue(false)
            if stickValue.x ~= 0 then
                self.m_phase:RotateModel(stickValue.x * self.view.config.ROTATE_SENSITIVITY_CONTROLLER * deltaTime)
            else
                self.m_phase:RotateModel(deltaTime * self.view.config.ROTATE_SPEED)
            end
        end)
    end)

    self.m_phase:ActiveModelRotateRoot(true)
    self.m_phase:ActiveShowVirtualCamera(true)
    self:_SampleBgAnim()
    self:_PlayBgAnim(true)
    self:_PlayDecoAnim()

    if self.m_phase.m_sceneRoot.view.buildingECSModel.gameObject.activeSelf then
        self.m_phase.m_sceneRoot.view.buildingECSModel:Cutoff(true, 0, 1)
    end
end

WikiModelShowCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return self.m_wikiEntryShowData
end

WikiModelShowCtrl.OnClose = HL.Override() << function(self)
    LuaUpdate:Remove(self.m_rotateTickKey)
    self.m_phase:ActiveShowVirtualCamera(false)
    self.m_phase:ResetModelRotation()
end

WikiModelShowCtrl._OnPlayAnimationOut = HL.Override() << function(self)
    WikiModelShowCtrl.Super._OnPlayAnimationOut(self)
    self:_PlayBgAnim(false)
end



WikiModelShowCtrl._SampleBgAnim = HL.Method() << function(self)
    local animName = nil
    if self.m_wikiEntryShowData.wikiCategoryType == WikiConst.EWikiCategoryType.Weapon then
        animName = "wiki_plane_toweapon_in"
    elseif self.m_wikiEntryShowData.wikiCategoryType == WikiConst.EWikiCategoryType.Building then
        animName = "wiki_plane_tobuilding_in"
    end
    if self.m_phase and animName then
        self.m_phase.m_sceneRoot.view.bgAnim:SampleClipAtPercent(animName, 1)
    end
end

WikiModelShowCtrl._PlayBgAnim = HL.Method(HL.Boolean) << function(self, isIn)
    local animName = isIn and "wiki_modeltoshow_in" or "wiki_modeltoshow_out"
    if self.m_wikiEntryShowData.wikiCategoryType == WikiConst.EWikiCategoryType.Monster then
        animName = isIn and "wiki_modeltoshow_monster_in" or "wiki_modeltoshow_monster_out"
    elseif self.m_wikiEntryShowData.wikiCategoryType == WikiConst.EWikiCategoryType.Building then
        animName = isIn and "wiki_modeltoshow_building_in" or "wiki_modeltoshow_building_out"
    end
    if self.m_phase then
        self.m_phase:PlayBgAnim(animName)
    end
end

WikiModelShowCtrl._PlayDecoAnim = HL.Method() << function(self)
    local animName = "wiki_uideco_modelshow_common"
    if self.m_wikiEntryShowData.wikiCategoryType == WikiConst.EWikiCategoryType.Weapon then
        animName = "wiki_uideco_modelshow_weapon"
    elseif self.m_wikiEntryShowData.wikiCategoryType == WikiConst.EWikiCategoryType.Building then
        animName = "wiki_uideco_modelshow_building"
    end
    if self.m_phase then
        self.m_phase:PlayDecoAnim(animName)
    end
end

HL.Commit(WikiModelShowCtrl)
