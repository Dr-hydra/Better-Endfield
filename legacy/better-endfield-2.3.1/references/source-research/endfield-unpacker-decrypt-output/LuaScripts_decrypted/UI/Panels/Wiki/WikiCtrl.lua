local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Wiki
WikiCtrl = HL.Class('WikiCtrl', uiCtrl.UICtrl)








WikiCtrl.s_messages = HL.StaticField(HL.Table) << {
}



local WIKI_CATEGORY_TO_Node_NAME = {
    [WikiConst.EWikiCategoryType.Weapon] = "btnWeapon",
    [WikiConst.EWikiCategoryType.Equip] = "btnEquip",
    [WikiConst.EWikiCategoryType.Item] = "btnItem",
    [WikiConst.EWikiCategoryType.Monster] = "btnMonster",
    [WikiConst.EWikiCategoryType.Building] = "btnBuilding",
    [WikiConst.EWikiCategoryType.Tutorial] = "btnTutorial",
}



WikiCtrl.m_selectedNodeAnim = HL.Field(HL.Userdata)

WikiCtrl.m_arg = HL.Field(HL.Table)



WikiCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg or {}
    local spriteNumberTable = {}
    for i = 1, 6 do
        spriteNumberTable[i] = self.view["imgNumber0" .. i].sprite
    end

    for categoryId, categoryData in pairs(Tables.wikiCategoryTable) do
        local nodeName = WIKI_CATEGORY_TO_Node_NAME[categoryId]
        if nodeName ~= nil then
            local node = self.view[nodeName]
            if node then
                node.btn.transform:SetSiblingIndex(categoryData.categoryPriority - 1)
                node.imgNumber.sprite = spriteNumberTable[categoryData.categoryPriority]
                node.btn.onClick:AddListener(function()
                    if WikiUtils.isWikiCategoryUnlocked(categoryId) then
                        self.m_phase:OpenCategory(categoryId)
                    else
                        Notify(MessageConst.SHOW_TOAST, Language.LUA_WIKI_CATEGORY_LOCKED)
                    end
                end)
                node.btn.onHoverChange:AddListener(function(isHover)
                    if self.m_selectedNodeAnim then
                        UIUtils.PlayAnimationAndToggleActive(self.m_selectedNodeAnim, false)
                    end
                    if isHover then
                        self.m_selectedNodeAnim = node.selectNodeAnim
                        if self.m_selectedNodeAnim then
                            UIUtils.PlayAnimationAndToggleActive(self.m_selectedNodeAnim, true)
                        end
                    end
                end)
                node.redDot:InitRedDot("WikiCategory", categoryId)

                if categoryData.categoryPriority == 1 then
                    self.m_firstCategoryBtn = node.btn
                end
            end
        end
    end

    self:_InitController()

    self.view.topNode:InitWikiTop({
        phase = self.m_phase,
        panelId = PANEL_ID,
        forceShowCloseBtn = true,
    })
    self.m_phase:ActiveMainSceneItem(true)
    self.m_phase:PlayDecoAnim("wiki_uideco_in")

    AudioManager.PostEvent("au_ui_menu_wiki_open")
end

WikiCtrl.OnClose = HL.Override() << function(self)
    AudioManager.PostEvent("au_ui_menu_wiki_close")
end

WikiCtrl.OnShow = HL.Override() << function(self)
    if self.m_phase then
        self.m_phase:ActiveMainSceneItem(true)
        self.m_phase:PlayDecoAnim("wiki_uideco_in")
    end


    self:_StartCoroutine(function()
        coroutine.step()
        if self:IsShow() then
            AudioAdapter.PostEvent("Au_UI_Menu_WikiPanel_Open")
        end
    end)
    if DeviceInfo.usingController then
        local categoryType = self.m_arg and self.m_arg.resumeState and self.m_arg.resumeState.categoryType or nil
        if string.isEmpty(categoryType) then
            categoryType = self.m_phase and
                (self.m_phase.m_currentWikiDetailArgs and self.m_phase.m_currentWikiDetailArgs.categoryType or
                    (self.m_phase.m_currentWikiGroupArgs and self.m_phase.m_currentWikiGroupArgs.categoryType or nil)) or nil
        end
        local targetBtn = self:_GetCategoryBtn(categoryType) or self.m_firstCategoryBtn
        if targetBtn then
            self:SetNaviTarget(targetBtn)
        end
    end
end

WikiCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_arg and lume.deepCopy(self.m_arg) or {}
    local categoryType = nil
    if self.m_phase then
        categoryType = self.m_phase.m_currentWikiDetailArgs and self.m_phase.m_currentWikiDetailArgs.categoryType or
            (self.m_phase.m_currentWikiGroupArgs and self.m_phase.m_currentWikiGroupArgs.categoryType or nil)
    end
    arg.resumeState = {
        categoryType = categoryType,
    }
    return arg
end

WikiCtrl._GetCategoryBtn = HL.Method(HL.Opt(HL.String)).Return(HL.Opt(HL.Userdata)) << function(self, categoryType)
    if string.isEmpty(categoryType) then
        return nil
    end
    local nodeName = WIKI_CATEGORY_TO_Node_NAME[categoryType]
    local node = nodeName and self.view[nodeName] or nil
    return node and node.btn or nil
end





WikiCtrl.m_firstCategoryBtn = HL.Field(HL.Userdata)

WikiCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    if self.m_firstCategoryBtn then
        self:SetNaviTarget(self.m_firstCategoryBtn)
    end
end



HL.Commit(WikiCtrl)
