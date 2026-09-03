
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonEnemyPopup

CommonEnemyPopupCtrl = HL.Class('CommonEnemyPopupCtrl', uiCtrl.UICtrl)


CommonEnemyPopupCtrl.m_enemyInfos = HL.Field(HL.Table)

CommonEnemyPopupCtrl.m_genEnemyCellFunc = HL.Field(HL.Function)

CommonEnemyPopupCtrl.m_enemyAbilityCellCache = HL.Field(HL.Forward("UIListCache"))

CommonEnemyPopupCtrl.m_curSelectEnemyCell = HL.Field(HL.Any)

CommonEnemyPopupCtrl.m_curSelectEnemyIndex = HL.Field(HL.Number) << -1

CommonEnemyPopupCtrl.m_hideDamageTakenInfo = HL.Field(HL.Boolean) << false





CommonEnemyPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}












CommonEnemyPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if arg.isBackBtnStyle then
        self.view.backBtn.gameObject:SetActive(true)
        self.view.mask.gameObject:SetActive(false)
        self.view.btnClose.gameObject:SetActive(false)
        self.view.backBtn.onClick:AddListener(function()
            self:_OnBtnCloseClick()
        end)
    else
        self.view.btnClose.gameObject:SetActive(true)
        self.view.mask.gameObject:SetActive(true)
        self.view.backBtn.gameObject:SetActive(false)
        self.view.btnClose.onClick:AddListener(function()
            self:_OnBtnCloseClick()
        end)
    end

    self.view.mask.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    self.view.titleTxt.text = arg.title
    self.view.enemyListTitleTxt.text = arg.enemyListTitle or Language["ui_common_enemy_popup_info_list"]
    self.view.enemyInfoTitleTxt.text = arg.enemyInfoTitle or Language["ui_common_enemy_popup_info_desc"]
    if arg.hideLevelTextNode then
        self.view.levelTextNode.gameObject:SetActive(false)
    end
    self.m_genEnemyCellFunc = UIUtils.genCachedCellFunction(self.view.dungeonEnemyScrollList)
    self.m_enemyAbilityCellCache = UIUtils.genCellCache(self.view.enemyAbilityCell)
    self.view.dungeonEnemyScrollList.onUpdateCell:AddListener(function(gameObject, csIndex)
        self:_OnUpdateEnemyCell(gameObject, csIndex)
    end)

    self.m_enemyInfos = {}
    local enemyIds = arg.enemyIds
    local enemyLevels = arg.enemyLevels
    if type(enemyIds) == "table" then
        for i = 1, #enemyIds do
            local id = enemyIds[i]
            local level = #enemyLevels >= #enemyIds and enemyLevels[i] or 1
            local enemyInfo = UIUtils.getEnemyInfoByIdAndLevel(id, level)
            if enemyInfo then
                table.insert(self.m_enemyInfos, enemyInfo)
            end
        end
    else
        for csIndex = 0, enemyIds.Count - 1 do
            local id = enemyIds[csIndex]
            local level = enemyLevels.Count >= enemyIds.Count and enemyLevels[csIndex] or 1
            local enemyInfo = UIUtils.getEnemyInfoByIdAndLevel(id, level)
            if enemyInfo then
                table.insert(self.m_enemyInfos, enemyInfo)
            end
        end
    end

    if #self.m_enemyInfos == 0 then
        logger.error("敌人相关信息有效的数量为0，请检查配置")
        return
    end
    self:_Refresh(arg)
    self:_InitController()
end

CommonEnemyPopupCtrl._OnBtnCloseClick = HL.Method() << function(self)
    self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close)
end


CommonEnemyPopupCtrl._OnUpdateEnemyCell = HL.Method(GameObject, HL.Number) << function(self, gameObject, csIndex)
    local cell = self.m_genEnemyCellFunc(gameObject)
    local luaIndex = LuaIndex(csIndex)
    local info = self.m_enemyInfos[luaIndex]

    local selected = self.m_curSelectEnemyIndex == luaIndex
    if selected then
        self.m_curSelectEnemyCell = cell
        if cell.view.button ~= InputManagerInst.controllerNaviManager.curTarget then
            self:SetNaviTarget(cell.view.button)
        end
    end

    cell:InitEnemyCell(info, function()
        self:_OnEnemyCellClick(cell, luaIndex)
    end)
    cell:SetSelected(selected)
end

CommonEnemyPopupCtrl._Refresh = HL.Method(HL.Any) << function(self, arg)
    if arg.initSelectEnemyLuaIndex ~= nil then
        self.m_curSelectEnemyIndex = arg.initSelectEnemyLuaIndex
    else
        self.m_curSelectEnemyIndex = 1
    end
    self.m_hideDamageTakenInfo = arg.hideDamageTakenInfo or false

    self:_RefreshContent()
    self.view.dungeonEnemyScrollList:UpdateCount(#self.m_enemyInfos)
end

CommonEnemyPopupCtrl._OnEnemyCellClick = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    if self.m_curSelectEnemyIndex == luaIndex then
        return
    end

    self.m_curSelectEnemyIndex = luaIndex
    self.m_curSelectEnemyCell:SetSelected(false)
    self.m_curSelectEnemyCell = cell
    cell:SetSelected(true)

    self:_RefreshContent()

    self.view.rightNode:PlayInAnimation()
end

CommonEnemyPopupCtrl._RefreshContent = HL.Method() << function(self)
    local info = self.m_enemyInfos[self.m_curSelectEnemyIndex]

    self.view.nameTxt.text = info.name
    self.view.levelTxt.text = info.level or "-"
    self.view.enemyIcon:LoadSprite(UIConst.UI_SPRITE_MONSTER_ICON_BIG, info.templateId)
    self.view.descTxt:SetAndResolveTextStyle(info.desc)
    if self.m_hideDamageTakenInfo then
        self.view.enemyDamageTakenInfo.gameObject:SetActive(false)
    else
        self.view.enemyDamageTakenInfo:InitEnemyDamageTakenInfo(info.templateId)
    end

    local ability = info.ability
    self.m_enemyAbilityCellCache:Refresh(#ability, function(cell, luaIndex)
        cell.skillTxt:SetAndResolveTextStyle(ability[luaIndex].description)
    end)
    self.view.enemyAbilityNode.gameObject:SetActiveIfNecessary(#ability > 0)
end



CommonEnemyPopupCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    if self.m_curSelectEnemyCell ~= nil then
        self:SetNaviTarget(self.m_curSelectEnemyCell.view.button)
    end
end

CommonEnemyPopupCtrl.GetCurSelectEnemyLuaIndex = HL.Method().Return(HL.Number) << function(self)
    return self.m_curSelectEnemyIndex
end



HL.Commit(CommonEnemyPopupCtrl)
