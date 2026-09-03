local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WikiTop = HL.Class('WikiTop', UIWidgetBase)




local OPEN_FOLDER_NORMAL_FUNC = function(folder, args)
    args.phase:CreateOrShowPhasePanelItem(folder.panelId)
end


local OPEN_FOLDER_GROUP_FUNC = function(folder, args)
    local panelCfg = args.phase:GetCategoryPanelCfg(folder.categoryType)

    local wikiGroupArgs = {
        categoryType = folder.categoryType,
        detailPanelId = panelCfg.detailPanelId,
        wikiEntryShowData = args.wikiEntryShowData,
        includeLocked = panelCfg.includeLocked,
    }
    args.phase.m_currentWikiGroupArgs = wikiGroupArgs
    logger.info("[Wiki] Open group panel: " .. wikiGroupArgs.wikiEntryShowData.wikiGroupData.groupId)
    if UIManager:IsShow(panelCfg.groupPanelId) then
        args.phase:_GetPanelPhaseItem(panelCfg.groupPanelId).uiCtrl:Refresh(wikiGroupArgs)
    else
        args.phase:CreateOrShowPhasePanelItem(panelCfg.groupPanelId, wikiGroupArgs)
    end
end


local OPEN_FOLDER_DETAIL_FUNC = function(folder, args)
    local wikiGroupShowDataList = WikiUtils.getWikiGroupShowDataList(args.wikiEntryShowData.wikiCategoryType)

    local wikiDetailArgs = {
        categoryType = args.wikiEntryShowData.wikiCategoryType,
        wikiGroupShowDataList = wikiGroupShowDataList,
        wikiEntryShowData = args.wikiEntryShowData,
    }
    args.phase.m_currentWikiDetailArgs = wikiDetailArgs
    if UIManager:IsShow(folder.panelId) then
        args.phase:_GetPanelPhaseItem(folder.panelId).uiCtrl:Refresh(wikiDetailArgs)
    else
        args.phase:CreateOrShowPhasePanelItem(folder.panelId, wikiDetailArgs)
    end
end









local WIKI_ROOT = {

    nameKey = "LUA_WIKI_NAME",
    panelId = PanelId.Wiki,
    openFunc = OPEN_FOLDER_NORMAL_FUNC,

    children = {

        {
            nameKey = "ui_wiki_common_tut",
            panelId = PanelId.WikiGuide,
            categoryType = WikiConst.EWikiCategoryType.Tutorial,
            openFunc = OPEN_FOLDER_GROUP_FUNC,
        },

        {
            nameKey = "ui_wiki_common_eny",
            panelId = PanelId.WikiGroup,
            categoryType = WikiConst.EWikiCategoryType.Monster,
            openFunc = OPEN_FOLDER_GROUP_FUNC,

            children = {
                {
                    nameKey = nil,
                    panelId = PanelId.WikiMonster,
                    openFunc = OPEN_FOLDER_DETAIL_FUNC,
                }
            }
        },

        {
            nameKey = "ui_wiki_common_wpn",
            panelId = PanelId.WikiGroup,
            categoryType = WikiConst.EWikiCategoryType.Weapon,
            openFunc = OPEN_FOLDER_GROUP_FUNC,

            children = {
                {
                    nameKey = nil,
                    panelId = PanelId.WikiWeapon,
                    openFunc = OPEN_FOLDER_DETAIL_FUNC,
                    children = {
                        {
                            nameKey = "LUA_WIKI_WEAPON_SKILL_NAME",
                            panelId = PanelId.WikiWeaponSkill,
                        }
                    }
                }
            }
        },

        {
            nameKey = "ui_wiki_common_equip",
            panelId = PanelId.WikiEquipSuit,
            categoryType = WikiConst.EWikiCategoryType.Equip,
            openFunc = OPEN_FOLDER_GROUP_FUNC,

            children = {
                {
                    nameKey = nil,
                    panelId = PanelId.WikiEquip,
                    openFunc = OPEN_FOLDER_DETAIL_FUNC,
                }
            }
        },

        {
            nameKey = "ui_wiki_common_mac",
            panelId = PanelId.WikiGroup,
            categoryType = WikiConst.EWikiCategoryType.Building,
            openFunc = OPEN_FOLDER_GROUP_FUNC,

            children = {
                {
                    nameKey = nil,
                    panelId = PanelId.WikiBuilding,
                    openFunc = OPEN_FOLDER_DETAIL_FUNC,
                    children = {
                        {
                            nameKey = "ui_wiki_item_fac_tree",
                            categoryType = WikiConst.EWikiCategoryType.Building,
                            panelId = PanelId.WikiCraftingTree,
                        }
                    }
                }
            }
        },

        {
            nameKey = "ui_wiki_common_mat",
            panelId = PanelId.WikiGroup,
            categoryType = WikiConst.EWikiCategoryType.Item,
            openFunc = OPEN_FOLDER_GROUP_FUNC,

            children = {
                {
                    nameKey = nil,
                    panelId = PanelId.WikiItem,
                    openFunc = OPEN_FOLDER_DETAIL_FUNC,
                    children = {
                        {
                            nameKey = "ui_wiki_item_fac_tree",
                            panelId = PanelId.WikiCraftingTree,
                            categoryType = WikiConst.EWikiCategoryType.Item,
                        }
                    }
                }
            }
        },
    }
}



WikiTop.m_folderCellCache = HL.Field(HL.Forward("UIListCache"))


WikiTop.m_folderPath = HL.Field(HL.Table)

WikiTop.m_args = HL.Field(HL.Table)


WikiTop._OnFirstTimeInit = HL.Override() << function(self)
    local isShowBackBtn = not self.m_args.forceShowCloseBtn and (self.m_args.forceShowBackBtn or self.m_args.phase.m_isShowBackBtn)
    self.view.closeBtn.gameObject:SetActive(not isShowBackBtn)
    self.view.backBtn.gameObject:SetActive(isShowBackBtn)
    if isShowBackBtn then
        self.view.backBtn.onClick:AddListener(function()
            self:_FolderBack()
        end)
    else
        self.view.closeBtn.onClick:AddListener(function()
            self.m_args.phase:RemovePhasePanelItemById(PanelId.WikiEmpty)
            PhaseManager:PopPhase(PhaseId.Wiki)
        end)
    end
    self.view.searchBtn.onClick:AddListener(function()
        self.m_args.phase:CreateOrShowPhasePanelItem(PanelId.WikiSearch)
        UIManager:SetTopOrder(PanelId.WikiSearch)
    end)

    self.m_folderCellCache = UIUtils.genCellCache(self.view.folderCell)
    self:RegisterMessage(MessageConst.ON_WIKI_SEARCH_KEYWORD_CHANGED, function(keyword)
        self:_RefreshSearch()
    end)
end

WikiTop._OnEnable = HL.Override() << function (self)
    self.view.naviGroup:ManuallyStopFocus()
end

WikiTop._OnDisable = HL.Override() << function(self)
    self.view.naviGroup:ManuallyStopFocus()
end









WikiTop.InitWikiTop = HL.Method(HL.Table) << function(self, args)
    self.m_args = args

    self:_FirstTimeInit()
    self:_RefreshSearch()

    self.m_folderPath = {}
    local folderFound = self:_FindFolderPath(WIKI_ROOT, args, self.m_folderPath)
    if not folderFound then
        self.m_folderCellCache:Refresh(0)
        logger.error("WikiTop->Can't find folder", args)
        return
    end
    local folderCount = #self.m_folderPath
    self.m_folderCellCache:Refresh(folderCount, function(folderCell, index)
        local folderIndex = folderCount - index + 1
        local folder = self.m_folderPath[folderIndex]
        local isCurrent = index == folderCount
        folderCell.bgImg.gameObject:SetActive(not isCurrent)
        folderCell.arrowImg.gameObject:SetActive(not isCurrent)
        folderCell.stateController:SetState(isCurrent and "current" or "normal")
        local folderName
        if folder.nameKey ~= nil then
            folderName = Language[folder.nameKey]
        end
        if folderName == nil and args.wikiEntryShowData then
            if args.wikiEntryShowData.wikiEntryData.refItemId and not string.isEmpty(args.wikiEntryShowData.wikiEntryData.refItemId) then
                folderName = Tables.itemTable[args.wikiEntryShowData.wikiEntryData.refItemId].name
            elseif args.wikiEntryShowData.wikiEntryData.refMonsterTemplateId and not string.isEmpty(args.wikiEntryShowData.wikiEntryData.refMonsterTemplateId) then
                folderName = Tables.enemyTemplateDisplayInfoTable[args.wikiEntryShowData.wikiEntryData.refMonsterTemplateId].name
            end
        end
        folderCell.nameTxt.text = folderName
        folderCell.nameTxt.color = isCurrent and folderCell.config.COLOR_CURRENT or folderCell.config.COLOR_PARENT
        folderCell.btn.onClick:RemoveAllListeners()
        folderCell.btn.gameObject:GetComponent(typeof(CS.Beyond.UI.NonDrawingGraphic)).raycastTarget = not isCurrent
        if not isCurrent then
            folderCell.btn.onClick:AddListener(function()
                if folder.openFunc then
                    self.view.naviGroup:ManuallyStopFocus()
                    self:_RemoveUnusedPanel()
                    for i = 1, folderIndex - 1 do
                        args.phase:RemovePhasePanelItemById(self.m_folderPath[i].panelId)
                    end
                    folder.openFunc(folder, args)
                end
            end)
        end
        if DeviceInfo.usingController then
            local action = isCurrent and CS.Beyond.Input.ActionOnSetNaviTarget.None or
                CS.Beyond.Input.ActionOnSetNaviTarget.PressConfirmTriggerOnClick
            folderCell.btn:ChangeActionOnSetNaviTarget(action)
        end
    end)
end

WikiTop._FindFolderPath = HL.Method(HL.Table, HL.Table, HL.Table).Return(HL.Boolean) << function(
    self, folder, folderArgs, folderPath)
    if folder.panelId == folderArgs.panelId and (folder.categoryType == nil or folder.categoryType == folderArgs.categoryType) then
        table.insert(folderPath, folder)
        return true
    elseif folder.children then
        for _, child in pairs(folder.children) do
            local result = self:_FindFolderPath(child, folderArgs, folderPath)
            if result then
                table.insert(folderPath, folder)
                return true
            end
        end
    end
    return false
end

WikiTop._FolderBack = HL.Method() << function(self)
    if self.m_folderPath and #self.m_folderPath > 1 then
        local preFolder = self.m_folderPath[2]
        if preFolder.openFunc then
            self:_RemoveUnusedPanel()

            local curPanelId = self.m_folderPath[1].panelId
            local phaseItem = self.m_args.phase:_GetPanelPhaseItem(curPanelId)
            if phaseItem then
                self.m_args.phase:ActiveModelRotateRoot(false, curPanelId == PanelId.WikiBuilding)
                phaseItem.uiCtrl:PlayAnimationOutWithCallback(function()
                    self.m_args.phase:RemovePhasePanelItemById(curPanelId)
                    preFolder.openFunc(preFolder, self.m_args)
                end)
            else
                preFolder.openFunc(preFolder, self.m_args)
            end
        end
    end
end

WikiTop._RemoveUnusedPanel = HL.Method() << function(self)
    local panelIdToRemove = {}
    for panelId, _ in pairs(self.m_args.phase.m_panel2Item) do
        if panelId ~= PanelId.WikiEmpty then
            local found = false
            for _, folder in pairs(self.m_folderPath) do
                if folder.panelId == panelId then
                    found = true
                    break
                end
            end
            if not found then
                table.insert(panelIdToRemove, panelId)
            end
        end
    end
    for _, panelId in pairs(panelIdToRemove) do
        local phaseItem = self.m_args.phase:_GetPanelPhaseItem(panelId)
        if phaseItem then
            if not phaseItem.uiCtrl:IsPlayingAnimationOut() then
                self.m_args.phase:RemovePhasePanelItemById(panelId)
            end
        end
    end
end

WikiTop._RefreshSearch = HL.Method() << function(self)
    self.view.inputField.text = self.m_args.phase.curSearchKeyword
end

HL.Commit(WikiTop)
return WikiTop

