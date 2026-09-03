local CommonPopUpCtrl = require_ex('UI/Panels/CommonPopUp/CommonPopUpCtrl')
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Mission
local PHASE_ID = PhaseId.Mission

local CHAPTER_ICON_PATH = "Mission/ChapterIconNew"
local CHAPTER_BG_ICON_PATH = "Mission/ChapterBgIconNew"
local CHAPTER_CHAR_ICON_PATH = "Mission/ChaptCharIconNew"

local OPTIONAL_TEXT_COLOR = "C7EC59"

local QuestState = CS.Beyond.Gameplay.MissionSystem.QuestState
local MissionState = CS.Beyond.Gameplay.MissionSystem.MissionState
local TrackAction = CS.Beyond.Gameplay.MissionSystem.TrackAction
local TrackJumpType = CS.Beyond.Gameplay.MissionSystem.TrackJumpType
local MissionImportance = GEnums.MissionImportance
local ChapterType = CS.Beyond.Gameplay.ChapterType
local MissionExternalSystemType = GEnums.MissionExternalSystemType
local MissionType = CS.Beyond.Gameplay.MissionSystem.MissionType
local MissionViewType = GEnums.MissionViewType
local DomainDepotPackageProgress = GEnums.DomainDepotPackageProgress
local QuestType = GEnums.QuestType

local MissionListCellType_Importance = 0
local MissionListCellType_Chapter = 1
local MissionListCellType_Mission = 2

local MissionSystem = CS.Beyond.Gameplay.MissionSystem

local MissionFilterType_All = -1

local SKIP_CHAPTER_ACTIVITY_ID = "activity_skipchapter_1"
local SKIP_CHAPTER_JUMP_ID = "jump_center_activity_skipchapter_1"
local SKIP_CHAPTER_GLOBAL_VAR_KEY = "mission_panel_skip_bubble_off"
local ActivityStatus = GEnums.ActivityStatus

local MissionImportanceColorMap = {
    [MissionImportance.High] = "Yellow",
    [MissionImportance.Mid] = "Green",
    [MissionImportance.Low] = "Blue",
}


local MISSION_BADGE_PRIORITY_EXPIRE = 1
local MissionBadgePriorityMap = {
    [GEnums.MissionExtraInfoType.Unlock] = 4,
    [GEnums.MissionExtraInfoType.UnlockRegion] = 3,
    [GEnums.MissionExtraInfoType.NarrativeImportant] = 2,
}

local BlockConditionListType = CS.System.Collections.Generic.List(CS.Beyond.Gameplay.MissionRuntimeAsset.BlockCondition)

local function getHighestBit(n)
    if n <= 0 then return 0 end
    local bit = 0
    while n > 1 do
        n = math.floor(n / 2)
        bit = bit + 1
    end
    return bit
end

local function getMissionBadgePriority(missionInfo, missionId)
    local p = MissionBadgePriorityMap[missionInfo.extraInfoType]
    if p then
        return p
    end
    if MissionSystem.GetMissionExpireInfo(missionId) then
        return MISSION_BADGE_PRIORITY_EXPIRE
    end
    return 0
end

local MissionFilterCellConfig = {
    [1] = {
        missionFilterType = MissionFilterType_All,
        icon = "all_mission_icon_gray",
        typeText = Language.ui_mis_panel_tab_all,
    },
    [2] = {
        missionFilterType = MissionViewType.MissionViewMain,
        icon = "main_mission_icon_gray",
        typeText = Language.ui_mis_panel_tab_main_new,
    },
    [3] = {
        missionFilterType = MissionViewType.MissionViewDiscovery,
        icon = "fac_mission_icon_gray",
        typeText = Language.ui_mis_panel_tab_discovery,
    },
    [4] = {
        missionFilterType = MissionViewType.MissionViewSide,
        icon = "char_mission_icon_gray",
        typeText = Language.ui_mis_panel_tab_side,
    },
    [5] = {
        missionFilterType = MissionViewType.MissionViewActivity,
        icon = "activity_mission_icon_gray",
        typeText = Language.ui_mis_panel_tab_activity,
    },
    [6] = {
        missionFilterType = MissionViewType.MissionViewOther,
        icon = "misc_mission_icon_gray",
        typeText = Language.ui_mis_panel_tab_other,
    },
}



























































MissionCtrl = HL.Class('MissionCtrl', uiCtrl.UICtrl)








MissionCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_TRACK_MISSION_CHANGE] = 'OnTrackMissionChange',
    [MessageConst.ON_TRACK_MISSION_JUMP_READY] = '_OnTrackMissionJumpReady',
    [MessageConst.ON_QUEST_OBJECTIVE_UPDATE] = 'OnObjectiveUpdate',

    [MessageConst.ON_SYNC_ALL_MISSION] = 'OnSyncAllMission',
    [MessageConst.ON_MISSION_STATE_CHANGE] = 'OnMissionStateChange',
    [MessageConst.ON_QUEST_STATE_CHANGE] = 'OnQuestStateChange',
    [MessageConst.ON_BLOCK_QUEST_STATE_CHANGE] = "OnBlockQuestStateChange",

}




MissionCtrl.m_missionSystem = HL.Field(HL.Any)


MissionCtrl.m_selectedMissionId = HL.Field(HL.Any) << nil


MissionCtrl.m_lastSelectedMissionId = HL.Field(HL.Any) << nil


MissionCtrl.m_missionViewInfo = HL.Field(HL.Table)


MissionCtrl.m_getMissionCellsFunc = HL.Field(HL.Function)


MissionCtrl.m_questCellCache = HL.Field(HL.Forward("UIListCache"))


MissionCtrl.m_missionTypeCells = HL.Field(HL.Forward("UIListCache"))


MissionCtrl.m_missionFilterType = HL.Field(HL.Any)


MissionCtrl.m_getRewardItemCellsFunc = HL.Field(HL.Function)


MissionCtrl.m_isClosing = HL.Field(HL.Boolean) << false


MissionCtrl.m_willExpire = HL.Field(HL.Boolean) << false


MissionCtrl.m_timeStamp = HL.Field(HL.Any)


MissionCtrl.m_packageNode = HL.Field(HL.Table)


MissionCtrl.m_openMissionFilter = HL.Field(HL.Any) << MissionFilterType_All


MissionCtrl.m_doNotPostAudio = HL.Field(HL.Boolean) << false


MissionCtrl.m_pendingTrackJumpMissionId = HL.Field(HL.Any) << nil





MissionCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_missionSystem = GameInstance.player.mission
    self:_RefreshMissionFilterCellConfig()

    self.m_packageNode = {
        ["Hurt"] = self.view.missionInfoNode.packageList.hurtNode,
        ["Jump"] = self.view.missionInfoNode.packageList.jumpNode,
        ["Teleport"] = self.view.missionInfoNode.packageList.tpNode,
    }

    self.m_questCellCache = UIUtils.genCellCache(self.view.missionInfoNode.questCell)


    self.view.btnClose.onClick:RemoveAllListeners()
    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    if arg and arg.missionFilter then
        if arg.missionFilter == "MissionViewMain" then
            self.m_openMissionFilter = MissionViewType.MissionViewMain
        elseif arg.missionFilter == "MissionViewDiscovery" then
            self.m_openMissionFilter = MissionViewType.MissionViewDiscovery
        elseif arg.missionFilter == "MissionViewSide" then
            self.m_openMissionFilter = MissionViewType.MissionViewSide
        elseif arg.missionFilter == "MissionViewActivity" then
            self.m_openMissionFilter = MissionViewType.MissionViewActivity
        elseif arg.missionFilter == "MissionViewOther" then
            self.m_openMissionFilter = MissionViewType.MissionViewOther
        else
            self.m_openMissionFilter = MissionFilterType_All
        end
    end


    self:_InitMissionFilter()

    self.m_getMissionCellsFunc = UIUtils.genCachedCellFunction(self.view.missionScrollView)

    if arg and arg.autoSelect then
        self.m_selectedMissionId = arg.autoSelect
    else
        self.m_selectedMissionId = self.m_missionSystem.trackMissionId
    end

    if arg and arg.autoSelectList then
        local list = arg.autoSelectList
        for key, missionId in pairs(list) do

            if self.m_missionSystem:GetMissionState(missionId) == MissionState.Processing and
                self.m_missionSystem:GetMissionInfo(missionId).isVisible then

                self.m_selectedMissionId = missionId
                break
            end
        end
    end

    self.view.blackMask.gameObject:SetActive(true)



    self:_RefreshSkipChapterBubble()
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.skipChapterBubble.transform.parent)

    self:_RefreshMissionList()

    if arg and arg.selectChapter then
        local success, data = Tables.missionSelectChapterTable:TryGetValue(arg.selectChapter)
        if not success or data == nil then
            logger.error("MissionCtrl._GetMissionChapterBitmaskBySelectChapter: missing config, selectChapter = " .. tostring(arg.selectChapter))
            return 0
        end
        local targetBitmask = data.missionChapter:ToInt()
        if targetBitmask > 0 then
            self.m_selectedMissionId = self:_FindMissionByChapterBitmask(targetBitmask)
        end
    end

    self:_AutoSelectMission(true)
    self:_RefreshMissionInfo()

    self.view.skipChapterBubble.onClick:AddListener(function()
        self:_OnClickSkipChapterBubble()
    end)

    self:_ChangeSelectedMission(0)
    self:_RefreshNaviSelected()

    if DeviceInfo.usingController then
        self.view.rewardNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_LARGER_UPDATE_INTERVAL)
            self:_UpdateShow()
        end
    end)
end



MissionCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Any) << function(self)
    local missionFilter
    if type(self.m_missionFilterType) == "number" then
        if self.m_missionFilterType >= 0 then
            missionFilter = Utils.intToEnum(typeof(GEnums.MissionViewType), self.m_missionFilterType)
            missionFilter = missionFilter:ToString()
        end
    elseif type(self.m_missionFilterType) == "string" then
        missionFilter = self.m_missionFilterType
    else
        missionFilter = self.m_missionFilterType:ToString()
    end

    return {
        autoSelect = self.m_selectedMissionId,
        missionFilter = missionFilter,
    }
end



MissionCtrl._InitMissionFilter = HL.Method() << function(self)
    self.view.m_missionTypeCells = UIUtils.genCellCache(self.view.missionTypeTab.tabCell)
    self.m_missionFilterType = self.m_openMissionFilter
    local currentSelectTab
    local currentSelectConfig
    self.view.m_missionTypeCells:Refresh(#MissionFilterCellConfig, function(cell, index)
        local config = MissionFilterCellConfig[index]
        if config.missionFilterType == self.m_openMissionFilter then
            currentSelectTab = cell
            currentSelectConfig = config
        end
        if config then
            cell.toggle.onValueChanged:AddListener(function(isOn)
                if isOn then
                    self:_SetMissionFilterType(config.missionFilterType)
                end
            end)
            UIUtils.setTabIcons(cell,UIConst.UI_SPRITE_MISSION_TYPE_ICON,config.icon)
        end
    end)
    if currentSelectTab then
        currentSelectTab.toggle.isOn = true
        self.view.titleTxt.text = currentSelectConfig.typeText
    end
end



MissionCtrl._RefreshMissionFilterCellConfig = HL.Method() << function(self)
    MissionFilterCellConfig = {
        [1] = {
            missionFilterType = MissionFilterType_All,
            icon = "all_mission_icon_gray",
            typeText = Language.ui_mis_panel_tab_all,
        },
        [2] = {
            missionFilterType = MissionViewType.MissionViewMain,
            icon = "main_mission_icon_gray",
            typeText = Language.ui_mis_panel_tab_main_new,
        },
        [3] = {
            missionFilterType = MissionViewType.MissionViewDiscovery,
            icon = "fac_mission_icon_gray",
            typeText = Language.ui_mis_panel_tab_discovery,
        },
        [4] = {
            missionFilterType = MissionViewType.MissionViewSide,
            icon = "char_mission_icon_gray",
            typeText = Language.ui_mis_panel_tab_side,
        },
        [5] = {
            missionFilterType = MissionViewType.MissionViewActivity,
            icon = "activity_mission_icon_gray",
            typeText = Language.ui_mis_panel_tab_activity,
        },
        [6] = {
            missionFilterType = MissionViewType.MissionViewOther,
            icon = "misc_mission_icon_gray",
            typeText = Language.ui_mis_panel_tab_other,
        },
    }
end





MissionCtrl._SetMissionFilterType = HL.Method(HL.Any) << function(self, filterType)
    if self.m_missionFilterType == filterType then
        return
    end
    self.m_missionFilterType = filterType

    for _, missionTypeConfig in pairs(MissionFilterCellConfig) do
        if filterType == missionTypeConfig.missionFilterType then
            self.view.titleTxt.text = missionTypeConfig.typeText
            break
        end
    end





    self:_RefreshSkipChapterBubble()
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.skipChapterBubble.transform.parent)
    self:_RefreshMissionList()

    local trackId = self.m_missionSystem.trackMissionId
    if not string.isEmpty(trackId) then
        self.m_selectedMissionId = trackId
    end

    self:_AutoSelectMission(false)
    self:_ChangeSelectedMission(0)
    self:_RefreshNaviSelected()

    if not string.isEmpty(self.m_selectedMissionId) then
        local missionInfo = self.m_missionSystem:GetMissionInfo(self.m_selectedMissionId)
        if self.m_missionFilterType == MissionFilterType_All or self.m_missionFilterType == missionInfo.viewType then
            self.view.missionInfoNode.gameObject:SetActive(true)
        else
            self.view.missionInfoNode.gameObject:SetActive(false)
        end
    end
end




MissionCtrl._AutoSelectMission = HL.Method(HL.Boolean) << function(self, isFirst)





    local trackMissionId = self.m_missionSystem.trackMissionId
    local priority = 0
    local toBeSelectedMissionId = ""
    for _, viewInfo in pairs(self.m_missionViewInfo) do
        if viewInfo.type == MissionListCellType_Chapter then
            for _, missionViewInfo in pairs(viewInfo.missionList) do
                local missionId = missionViewInfo.id
                if (not DeviceInfo.usingController or isFirst) and self.m_selectedMissionId and missionId == self.m_selectedMissionId and priority < 3 then
                    toBeSelectedMissionId = missionId
                    priority = 3
                elseif missionId == trackMissionId and priority < 2 then
                    toBeSelectedMissionId = missionId
                    priority = 2
                elseif missionId and string.isEmpty(toBeSelectedMissionId) and priority < 1 then
                    toBeSelectedMissionId = missionId
                    priority = 1
                end
            end
        else
            local missionId = viewInfo.id
            if (not DeviceInfo.usingController or isFirst) and self.m_selectedMissionId and missionId == self.m_selectedMissionId and priority < 3 then
                toBeSelectedMissionId = missionId
                priority = 3
            elseif missionId == trackMissionId and priority < 2 then
                toBeSelectedMissionId = missionId
                priority = 2
            elseif missionId and string.isEmpty(toBeSelectedMissionId) and priority < 1 then
                toBeSelectedMissionId = missionId
                priority = 1
            end
        end
    end
    if not isFirst then
        self.m_lastSelectedMissionId = self.m_selectedMissionId
    end
    if self.m_selectedMissionId ~= toBeSelectedMissionId then
        self.m_selectedMissionId = toBeSelectedMissionId

        self.m_doNotPostAudio = true
        self:_RefreshSelectedMission()
        self.m_doNotPostAudio = false

        self:_RefreshMissionInfo()
    end
end




MissionCtrl._TraverseAllMissionCell = HL.Method(HL.Any).Return(HL.Boolean) << function(self, callback)
    local hasLastSelectMission = false
    for i, viewInfo in ipairs(self.m_missionViewInfo) do
        local t = viewInfo.type
        if t == MissionListCellType_Mission then
            local missionId = viewInfo.id
            if missionId == self.m_lastSelectedMissionId then
                hasLastSelectMission = true
            end
            local csIdx = CSIndex(i)
            local gameObject = self.view.missionScrollView:Get(csIdx)
            if gameObject then
                local contentCell = self.m_getMissionCellsFunc(gameObject)
                callback(missionId, contentCell.missionCell)
            end
        elseif t == MissionListCellType_Chapter then
            local csIdx = CSIndex(i)
            local gameObject = self.view.missionScrollView:Get(csIdx)
            if gameObject then
                local contentCell = self.m_getMissionCellsFunc(gameObject)
                local missionList = viewInfo.missionList
                for missionIdx = 1, #missionList do
                    local missionId = missionList[missionIdx].id
                    if missionId == self.m_lastSelectedMissionId then
                        hasLastSelectMission = true
                    end
                    local missionCell = contentCell.missionCellCache:GetItem(missionIdx)
                    callback(missionId, missionCell)
                end
            end
        end
    end
    return hasLastSelectMission
end



MissionCtrl._RefreshEmptyNode = HL.Method() << function(self)
    local noMission = #self.m_missionViewInfo <= 0
    if noMission then
        self.view.missionListNodeState:SetState("None")
        self.view.missionInfoNodeState:SetState("None")
    else
        self.view.missionListNodeState:SetState("Any")
        self.view.missionInfoNodeState:SetState("Any")
    end
    self.view.emptyNode.gameObject:SetActive(noMission)


    local filterCellConfig = nil
    for _, c in pairs(MissionFilterCellConfig) do
        if self.m_missionFilterType == c.missionFilterType then
            filterCellConfig = c
            break
        end
    end
    if filterCellConfig then
        self.view.emptyIcon.gameObject:SetActive(true)
        self.view.emptyIcon:LoadSprite(UIConst.UI_SPRITE_MISSION_TYPE_ICON, filterCellConfig.icon)
    else
        self.view.emptyIcon.gameObject:SetActive(false)
    end
end



MissionCtrl._RefreshMissionList = HL.Method() << function(self)
    local missionFilterType = self.m_missionFilterType
    if missionFilterType ~= MissionFilterType_All then
        missionFilterType = missionFilterType:ToInt()
    end

    local trackMissionId = self.m_missionSystem.trackMissionId

    self.m_missionViewInfo = {}
    local viewInfoByImportance = {}
    local missionLayout = self.m_missionSystem:GetMissionListLayout_CBT3(missionFilterType)
    for importance, subLayout in pairs(missionLayout.importance) do
        local subViewInfo = {}
        table.insert(viewInfoByImportance, {importance = importance, info = subViewInfo})
        table.insert(subViewInfo, {type = MissionListCellType_Importance, importance = importance})
        for _, chapter in pairs(subLayout.chapters) do
            local missionList = {}

            table.insert(subViewInfo,
                { type = MissionListCellType_Chapter,
                  id = chapter.chapterId, missionList = missionList}
            )

            for _, missionId in pairs(chapter.missionList) do
                table.insert(missionList, {type = MissionListCellType_Mission, id = missionId, importance = importance})
            end

            table.sort(missionList, function(a, b)
                return self:_SortMissions(a, b)
            end)
        end
        for _, missionId in pairs(subLayout.standaloneMissions) do
            table.insert(subViewInfo, {type = MissionListCellType_Mission, id = missionId, importance = importance})
        end

        table.sort(subViewInfo, function(a, b)
            if a.type == MissionListCellType_Importance or b.type == MissionListCellType_Importance then
                return a.type < b.type
            end

            local aRepMission = self:_GetRepresentativeMissionInfo(a)
            local bRepMission = self:_GetRepresentativeMissionInfo(b)
            if aRepMission == nil or bRepMission == nil then
                return false
            end

            local priorityResult = self:_CompareMissionPriority(aRepMission, aRepMission.missionId, bRepMission, bRepMission.missionId)
            if priorityResult ~= nil then
                return priorityResult
            end

            if a.type ~= b.type then
                return a.type < b.type
            end

            if a.type == MissionListCellType_Chapter then
                local chapterA = self.m_missionSystem:GetChapterInfo(a.id)
                local chapterB = self.m_missionSystem:GetChapterInfo(b.id)
                local aType = chapterA.type:ToInt()
                local bType = chapterB.type:ToInt()
                if aType == bType then
                    local aMission = a.missionList[1]
                    local bMission = b.missionList[1]
                    if aMission ~= nil and bMission ~= nil then
                        return self:_SortMissions(aMission, bMission)
                    else
                        return false
                    end
                else
                    return aType < bType
                end
            elseif a.type == MissionListCellType_Mission then
                return self:_SortMissions(a, b)
            else
                return false
            end
        end)
    end

    table.sort(viewInfoByImportance, function(a, b)
        return a.importance:ToInt() <= b.importance:ToInt()
    end)

    for _, infoList in ipairs(viewInfoByImportance) do
        for _, info in ipairs(infoList.info) do
            table.insert(self.m_missionViewInfo, info)
        end
    end

    self:_RefreshEmptyNode()

    self.view.missionScrollView.getCellSize = function(index)
        local luaIdx = LuaIndex(index)
        local viewInfo = self.m_missionViewInfo[luaIdx]
        if viewInfo.type == MissionListCellType_Chapter then
            return 219 + 112 * #viewInfo.missionList
        elseif viewInfo.type == MissionListCellType_Mission then
            return 112
        elseif viewInfo.type == MissionListCellType_Importance then
            return 87
        end
    end

    local firstImportanceFound = false
    for i = 1, #self.m_missionViewInfo do
        local viewInfo = self.m_missionViewInfo[i]
        if viewInfo.type == MissionListCellType_Importance and (not firstImportanceFound) then
            viewInfo.isFirstImportance = true
            firstImportanceFound = true
        end
    end

    self.view.missionScrollView.onUpdateCell:RemoveAllListeners()
    self.view.missionScrollView.onUpdateCell:AddListener(function(gameObject, index)
        local luaIndex = LuaIndex(index)
        local content = self.m_getMissionCellsFunc(gameObject)
        local viewInfo = self.m_missionViewInfo[luaIndex]
        if viewInfo.type == MissionListCellType_Chapter then

            content.chapterCell.gameObject:SetActiveIfNecessary(true)
            content.missionCell.gameObject:SetActiveIfNecessary(false)
            content.importanceNode.gameObject:SetActiveIfNecessary(false)
            local chapterCell = content.chapterCell
            local chapterId = self.m_missionViewInfo[luaIndex].id
            local chapterInfo = self.m_missionSystem:GetChapterInfo(chapterId)
            if chapterInfo then
                if chapterInfo.type == ChapterType.Main then
                    chapterCell.episodeName:SetAndResolveTextStyle(chapterInfo.episodeName:GetText())
                    local chapterNumTxt = chapterInfo.chapterNum:GetText()
                    local episodeNumTxt = chapterInfo.episodeNum:GetText()
                    local separator = ""
                    if not string.isEmpty(chapterNumTxt) and not string.isEmpty(episodeNumTxt) then
                        separator = " - "
                    end
                    chapterCell.chapterNumAndEpisodeNum:SetAndResolveTextStyle(chapterNumTxt .. separator .. episodeNumTxt)

                    local chapterConfig = UIConst.CHAPTER_ICON_CONFIGS[chapterInfo.type]

                    if not string.isEmpty(chapterInfo.icon) then
                        chapterCell.icon.gameObject:SetActive(true)
                        chapterCell.icon:LoadSprite(CHAPTER_ICON_PATH, chapterInfo.icon)
                    elseif not string.isEmpty(chapterConfig.icon) then
                        chapterCell.icon.gameObject:SetActive(true)
                        chapterCell.icon:LoadSprite(CHAPTER_ICON_PATH, chapterConfig.icon)
                    else
                        chapterCell.icon.gameObject:SetActive(false)
                        chapterCell.icon.sprite = nil
                    end

                    if not string.isEmpty(chapterInfo.bgIcon) then
                        chapterCell.bgIcon.gameObject:SetActive(true)
                        chapterCell.bgIcon:LoadSprite(CHAPTER_BG_ICON_PATH, chapterInfo.bgIcon)
                    elseif not string.isEmpty(chapterConfig.bgIcon) then
                        chapterCell.bgIcon.gameObject:SetActive(true)
                        chapterCell.bgIcon:LoadSprite(CHAPTER_BG_ICON_PATH, chapterConfig.bgIcon)
                    else
                        chapterCell.bgIcon.gameObject:SetActive(false)
                        chapterCell.bgIcon.sprite = nil
                    end
                    chapterCell.titleNode1.gameObject:SetActiveIfNecessary(true)
                    chapterCell.titleNode2.gameObject:SetActiveIfNecessary(false)
                else
                    chapterCell.titleTxt:SetAndResolveTextStyle(chapterInfo.episodeName:GetText())
                    chapterCell.titleNode1.gameObject:SetActiveIfNecessary(false)
                    chapterCell.titleNode2.gameObject:SetActiveIfNecessary(true)
                end


                local missionList = self.m_missionViewInfo[luaIndex].missionList
                content.missionCellCache = content.missionCellCache or UIUtils.genCellCache(chapterCell.missionCell)
                content.missionCellCache:Refresh(#missionList, function(missionCell, luaIndex)
                    local mission = missionList[luaIndex]
                    local missionId = mission.id
                    local missionInfo = self.m_missionSystem:GetMissionInfo(missionId)
                    local importance = mission.importance
                    if missionInfo then
                        self:_SetMissionCellContent(missionCell, missionInfo, importance)
                    end
                end)
            end
        elseif viewInfo.type == MissionListCellType_Mission then

            content.chapterCell.gameObject:SetActiveIfNecessary(false)
            content.missionCell.gameObject:SetActiveIfNecessary(true)
            content.importanceNode.gameObject:SetActiveIfNecessary(false)
            local missionCell = content.missionCell
            local missionId = self.m_missionViewInfo[luaIndex].id
            local missionInfo = self.m_missionSystem:GetMissionInfo(missionId)
            local importance = viewInfo.importance
            if missionInfo then
                self:_SetMissionCellContent(missionCell, missionInfo, importance)
            end
        elseif viewInfo.type == MissionListCellType_Importance then
            content.chapterCell.gameObject:SetActiveIfNecessary(false)
            content.missionCell.gameObject:SetActiveIfNecessary(false)
            content.importanceNode.gameObject:SetActiveIfNecessary(true)
            content.importanceLayout.gameObject:SetActiveIfNecessary(not viewInfo.isFirstImportance)

            local importance = viewInfo.importance
            local targetState = "Vital"
            if importance == MissionImportance.High then
                targetState = "Vital"
            elseif importance == MissionImportance.Mid then
                targetState = "Secondary"
            elseif importance == MissionImportance.Low then
                targetState = "Normal"
            end
            content.importanceNodeStateController:SetState(targetState)
        end
        LayoutRebuilder.ForceRebuildLayoutImmediate(content.transform)
    end)
    self.view.missionScrollView:UpdateCount(#self.m_missionViewInfo)
end




MissionCtrl._GetRepresentativeMissionInfo = HL.Method(HL.Any).Return(HL.Any) << function(self, viewInfo)
    if viewInfo.type == MissionListCellType_Chapter then
        local firstMission = viewInfo.missionList[1]
        if firstMission then
            return self.m_missionSystem:GetMissionInfo(firstMission.id)
        end
        return nil
    elseif viewInfo.type == MissionListCellType_Mission then
        return self.m_missionSystem:GetMissionInfo(viewInfo.id)
    end
    return nil
end







MissionCtrl._CompareMissionPriority = HL.Method(HL.Any, HL.String, HL.Any, HL.String).Return(HL.Any) << function(self, missionInfoA, missionIdA, missionInfoB, missionIdB)
    local aIsMain = (missionInfoA.viewType == MissionViewType.MissionViewMain)
    local bIsMain = (missionInfoB.viewType == MissionViewType.MissionViewMain)
    if aIsMain ~= bIsMain then
        return aIsMain
    end

    local aRec = missionInfoA.isCurrentlyRecommended
    local bRec = missionInfoB.isCurrentlyRecommended
    if aRec ~= bRec then
        return aRec
    end

    local aBadgePriority = getMissionBadgePriority(missionInfoA, missionIdA)
    local bBadgePriority = getMissionBadgePriority(missionInfoB, missionIdB)
    if aBadgePriority ~= bBadgePriority then
        return aBadgePriority > bBadgePriority
    end

    return nil
end





MissionCtrl._SortMissions = HL.Method(HL.Any, HL.Any).Return(HL.Boolean) << function(self, a, b)
    if a.type ~= MissionListCellType_Mission or b.type ~= MissionListCellType_Mission then
        return false
    end
    local missionA = self.m_missionSystem:GetMissionInfo(a.id)
    local missionB = self.m_missionSystem:GetMissionInfo(b.id)
    local missionDataA = self.m_missionSystem:GetMissionData(a.id)
    local missionDataB = self.m_missionSystem:GetMissionData(b.id)
    if missionA == nil or missionDataA == nil or missionB == nil or missionDataB == nil then
        return false
    end

    local priorityResult = self:_CompareMissionPriority(missionA, a.id, missionB, b.id)
    if priorityResult ~= nil then
        return priorityResult
    end

    if missionA.sortId ~= missionB.sortId then
        return missionA.sortId > missionB.sortId
    end

    local _, aTypeInfo = Tables.missionTypeInfoTable:TryGetValue(missionA.missionType)
    local _, bTypeInfo = Tables.missionTypeInfoTable:TryGetValue(missionB.missionType)
    if aTypeInfo ~= nil and bTypeInfo ~= nil then
        local missionTypeOrderA = aTypeInfo.typePriority
        local missionTypeOrderB = bTypeInfo.typePriority
        if missionTypeOrderA == missionTypeOrderB then
            local aBitmask = missionA.missionChapterBitmaskInt
            local bBitmask = missionB.missionChapterBitmaskInt
            local aHighBit = getHighestBit(aBitmask)
            local bHighBit = getHighestBit(bBitmask)
            if aHighBit ~= bHighBit then
                return aHighBit > bHighBit
            end
            if missionDataA.acceptTime == missionDataB.acceptTime then
                return missionA.missionId > missionB.missionId
            else
                return missionDataA.acceptTime > missionDataB.acceptTime
            end
        else
            return missionTypeOrderA > missionTypeOrderB
        end
    else
        return aTypeInfo == nil
    end
end




MissionCtrl._FindMissionByChapterBitmask = HL.Method(HL.Number).Return(HL.String) << function(self, targetBitmask)
    local fallbackMainMissionId = ""
    for _, viewInfo in pairs(self.m_missionViewInfo) do
        local missionIds = {}
        if viewInfo.type == MissionListCellType_Chapter then
            for _, m in ipairs(viewInfo.missionList) do
                table.insert(missionIds, m.id)
            end
        elseif viewInfo.type == MissionListCellType_Mission then
            table.insert(missionIds, viewInfo.id)
        end
        for _, missionId in ipairs(missionIds) do
            local info = self.m_missionSystem:GetMissionInfo(missionId)
            if info and info.viewType == MissionViewType.MissionViewMain then
                local bitmask = info.missionChapterBitmaskInt
                if bitmask == targetBitmask then
                    return missionId
                end
                if string.isEmpty(fallbackMainMissionId) then
                    fallbackMainMissionId = missionId
                end
            end
        end
    end
    return fallbackMainMissionId
end



MissionCtrl._RefreshSelectedMission = HL.Method() << function(self)
    self:_TraverseAllMissionCell(function(missionId, missionCell)
        local selected = missionId == self.m_selectedMissionId


        if missionId ~= self.m_lastSelectedMissionId and selected and (not self.m_doNotPostAudio) then
            AudioAdapter.PostEvent("Au_UI_Toggle_Common_On")
        end


        self:_SetMissionCellSelected(missionCell, selected)
    end)
end



MissionCtrl._RefreshNaviSelected = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    self:_TraverseAllMissionCell(function(missionId, missionCell)
        local isSelected = missionId == self.m_selectedMissionId
        if isSelected then
            self:SetNaviTarget(missionCell.selectBtn)
        end
    end)
end





MissionCtrl._SetMissionCellSelected = HL.Method(HL.Any, HL.Boolean) << function(self, missionCell, selected)

    missionCell.stateController:SetState(selected and "SelectedBg" or "NormalBg")
end





MissionCtrl._SetMissionCellTrack = HL.Method(HL.Any, HL.Any) << function(self, missionCell, missionInfo)
    local missionId = missionInfo.missionId
    local track = (missionInfo.missionId == self.m_missionSystem.trackMissionId)

    if missionInfo.isConflicted or missionInfo.isBlocked then
        missionCell.missionLevelName.color = self.view.config.MISSION_LEVEL_BLOCK_FONT_COLOR
        local txt = missionInfo.isConflicted and Language.ui_mis_panel_conflict_list_notice
            or Language.ui_mis_panel_block_quest_list_notice
        missionCell.missionLevelName:SetAndResolveTextStyle(txt)
    else
        missionCell.missionLevelName.color = self.view.config.MISSION_LEVEL_NORMAL_FONT_COLOR
        if track then
            missionCell.missionTrackTip.gameObject:SetActive(true)
            local distance = self.m_missionSystem:GetMissionTrackDistance(missionId)
            if distance > 0 then
                missionCell.missionLevelName.text = tostring(math.floor(distance + 0.5)) .. " m"
            else
                local levelId = self:_GetLevelId(missionInfo)
                local _, levelInfo = Tables.levelDescTable:TryGetValue(levelId)
                if levelInfo then
                    missionCell.missionLevelName:SetAndResolveTextStyle(levelInfo.showName)
                else
                    missionCell.missionLevelName:SetAndResolveTextStyle(Language[levelId])
                end
            end
        else
            missionCell.missionTrackTip.gameObject:SetActive(false)
            local levelId = self:_GetLevelId(missionInfo)
            local _, levelInfo = Tables.levelDescTable:TryGetValue(levelId)
            if levelInfo then
                missionCell.missionLevelName:SetAndResolveTextStyle(levelInfo.showName)
            else
                missionCell.missionLevelName:SetAndResolveTextStyle(Language[levelId])
            end
        end
    end
end






MissionCtrl._SetMissionCellContent = HL.Method(HL.Any, HL.Any, HL.Any) << function(self, missionCell, missionInfo, importance)
    local missionId = missionInfo.missionId
    missionCell.missionNameTxt:SetAndResolveTextStyle(missionInfo.missionName:GetText())
    missionCell.selectBtn.onClick:RemoveAllListeners()
    missionCell.selectBtn.onClick:AddListener(function()
        if self.m_selectedMissionId ~= missionId then
            self.m_lastSelectedMissionId = self.m_selectedMissionId
            self.m_selectedMissionId = missionId
            self:_RefreshSelectedMission()
            self.view.missionInfoNode.animation:SkipInAnimation()
            self.view.missionInfoNode.animation:PlayInAnimation()
            self:_RefreshMissionInfo()
        end
    end)
    local missionViewType = missionInfo.viewType
    local icon = UIConst.MISSION_VIEW_TYPE_CONFIG[missionViewType].missionIcon
    missionCell.missionIcon:LoadSprite(UIConst.UI_SPRITE_MISSION_TYPE_ICON, icon)


    local willExpire, timeStamp = MissionSystem.GetMissionExpireInfo(missionId)
    missionCell.redDot.gameObject:SetActive(willExpire)

    if willExpire then
        missionCell.unlockIcon.gameObject:SetActiveIfNecessary(false)
        missionCell.narrativeIcon.gameObject:SetActiveIfNecessary(false)
        missionCell.newRegionIcon.gameObject:SetActiveIfNecessary(false)
    else
        local unlock = missionInfo.extraInfoType == GEnums.MissionExtraInfoType.Unlock
        local narrative = missionInfo.extraInfoType == GEnums.MissionExtraInfoType.NarrativeImportant
        local unlockRegion = missionInfo.extraInfoType == GEnums.MissionExtraInfoType.UnlockRegion

        missionCell.unlockIcon.gameObject:SetActiveIfNecessary(unlock)
        missionCell.narrativeIcon.gameObject:SetActiveIfNecessary(narrative)
        missionCell.newRegionIcon.gameObject:SetActiveIfNecessary(unlockRegion)
    end

    local insideSimpleChapter = false
    missionCell.charaIconImage.gameObject:SetActiveIfNecessary(false)
    local chapterId = self.m_missionSystem:GetChapterIdByMissionId(missionId)
    if chapterId and chapterId ~= nil then
        local chapterInfo = self.m_missionSystem:GetChapterInfo(chapterId)
        if chapterInfo and chapterInfo.type == ChapterType.Other then
            insideSimpleChapter = true
            missionCell.charaIconImage.gameObject:SetActiveIfNecessary(true)
            local characterId = missionInfo.charId
            local _, characterImgInfo = Tables.actorImageTable:TryGetValue(characterId)
            if characterImgInfo then
                missionCell.charaIconImage:LoadSprite(CHAPTER_CHAR_ICON_PATH, characterImgInfo.missionPanelChrAvatarPath)
            end
        end
    end

    local isMissionSelected = self.m_selectedMissionId == missionId
    self:_SetMissionCellTrack(missionCell, missionInfo)
    self:_SetMissionCellSelected(missionCell, isMissionSelected)

    local importanceState = MissionImportanceColorMap[importance]
    missionCell.stateController:SetState(importanceState)

    local rightState = "Rightempty"
    if missionId == self.m_missionSystem:GetTrackMissionId() then
        rightState = "MissionTrack"
    elseif missionInfo.isBlocked then
        rightState = "Lock"
    elseif missionInfo.isConflicted then
        rightState = "Forbidder"
    end
    missionCell.stateController:SetState(rightState)
    if missionId == self.m_selectedMissionId then
        missionCell.stateController:SetState("SelectedBg")
    else
        missionCell.stateController:SetState("NormalBg")
    end

    local isRecommend = missionInfo.isCurrentlyRecommended
    if insideSimpleChapter then
        if isRecommend then
            missionCell.stateController:SetState("CurrentRecommendTag")
        else
            missionCell.stateController:SetState("NoCurrentRecommendTag")
        end
    else
        missionCell.stateController:SetState("NormalHeight")
    end
end



MissionCtrl._RefreshMissionInfo = HL.Method() << function(self)
    local bSelectedMissionValid = false
    if not string.isEmpty(self.m_selectedMissionId) then
        local missionData = self.m_missionSystem:GetMissionData(self.m_selectedMissionId)
        local missionInfo = self.m_missionSystem:GetMissionInfo(self.m_selectedMissionId)
        if missionData and missionInfo then
            local missionInfoNode = self.view.missionInfoNode
            bSelectedMissionValid = true
            self.view.missionInfoNodeState:SetState("Any")


            local missionNameText = missionInfo.missionName:GetText()
            missionInfoNode.missionName:SetAndResolveTextStyle(missionNameText)

            local missionDescText = missionInfo:GetMissionDesc():GetText()


            local actualText = UIUtils.resolveTextCinematic(missionDescText)
            missionInfoNode.missionDesc:SetAndResolveTextStyle(actualText)
            local levelId = self:_GetLevelId(missionInfo)
            local _, sceneInfo = Tables.levelDescTable:TryGetValue(levelId)
            if sceneInfo then
                missionInfoNode.missionLevelName:SetAndResolveTextStyle(sceneInfo.showName)
            else
                missionInfoNode.missionLevelName:SetAndResolveTextStyle(Language[levelId])
            end


            self.m_willExpire, self.m_timeStamp = MissionSystem.GetMissionExpireInfo(self.m_selectedMissionId)
            missionInfoNode.timerNode.gameObject:SetActiveIfNecessary(self.m_willExpire)
            self:_UpdateExpireTime()

            missionInfoNode.unlockNode.gameObject:SetActiveIfNecessary(false)
            missionInfoNode.narrativeNode.gameObject:SetActiveIfNecessary(false)
            local extraInfoType = missionInfo.extraInfoType
            local extraText = missionInfo.missionExtraInfoDesc

            if extraInfoType == GEnums.MissionExtraInfoType.Unlock then
                missionInfoNode.unlockNode.gameObject:SetActiveIfNecessary(true)
                missionInfoNode.unlockText:SetAndResolveTextStyle(extraText)
            end

            if extraInfoType == GEnums.MissionExtraInfoType.NarrativeImportant then
                missionInfoNode.narrativeNode.gameObject:SetActiveIfNecessary(true)
                missionInfoNode.narrativeText:SetAndResolveTextStyle(extraText)
            end


            if extraInfoType == GEnums.MissionExtraInfoType.UnlockRegion then
                missionInfoNode.unlockNode.gameObject:SetActiveIfNecessary(true)
                missionInfoNode.unlockText:SetAndResolveTextStyle(extraText)
            end

            missionInfoNode.packageList.gameObject:SetActiveIfNecessary(false)
            missionInfoNode.trustBtn.gameObject:SetActiveIfNecessary(false)
            missionInfoNode.trustBtn.onClick:RemoveAllListeners()
            local externalSysType = missionInfo.externalSystemType
            local externalId = missionInfo.externalSystemId
            local isPackage = externalSysType == MissionExternalSystemType.DomainDepot
            if isPackage then
                local packageList = missionInfoNode.packageList

                local deliverInfo = GameInstance.player.domainDepotSystem:GetDomainDepotDeliverInfoByInstId(tonumber(externalId))
                local inDeliver = deliverInfo.packageProgress == DomainDepotPackageProgress.WaitingRecvPackage or
                    deliverInfo.packageProgress == DomainDepotPackageProgress.WaitingSendPackage
                if deliverInfo ~= nil and inDeliver then
                    if not deliverInfo.delegateFromOther then
                        missionInfoNode.trustBtn.gameObject:SetActiveIfNecessary(true)
                        missionInfoNode.trustBtn.onClick:AddListener(function()
                            self:Notify(MessageConst.SHOW_POP_UP, {
                                content = Language.ui_mis_deliver_title,
                                warningContent = Language.ui_mis_deliver_text_1,
                                secondWarningContent = Language.ui_mis_deliver_text_2,
                                onConfirm = function()
                                    DomainDepotUtils.DelegateCurrentDeliver()
                                    PhaseManager:PopPhase(PHASE_ID)
                                end,
                            })
                        end)
                    end

                    if deliverInfo.packageProgress == DomainDepotPackageProgress.WaitingSendPackage then
                        local progress = deliverInfo.cargoIntegrity
                        local packageTitleState
                        if progress > 70 then
                            packageTitleState = "GreaterThan70"
                        elseif progress > 30 then
                            packageTitleState = "GreaterThan30"
                        else
                            packageTitleState = "GreaterThan0"
                        end
                        packageList.titleNode:SetState(packageTitleState)
                        packageList.packageNumTxt:SetAndResolveTextStyle(progress .. "%")
                        DomainDepotUtils.UpdateReduceView(self.m_packageNode, deliverInfo.itemType)
                        packageList.gameObject:SetActiveIfNecessary(true)
                    end
                end
            end

            self:_RefreshTrackBtn()

            missionInfoNode.infoBtn.onClick:RemoveAllListeners()
            if missionInfo.isBlocked then
                missionInfoNode.lockTipsNode.gameObject:SetActiveIfNecessary(true)
                missionInfoNode.leftTipsNode.gameObject:SetActiveIfNecessary(false)
                missionInfoNode.mapBtn.gameObject:SetActiveIfNecessary(false)
                local cList = BlockConditionListType()
                missionInfo:AddBlockConditionDescToList(cList)


                local blockCnt = 0
                local blockIdx = -1
                local conditions = {}
                for i = 0, cList.Count - 1 do
                    local condition = cList[i]
                    table.insert(conditions, {
                        jumpId = nil,
                        desc = condition.desc,
                        tips = "",
                        isComplete = condition.isComplete
                    })
                    if not condition.isComplete then
                        blockCnt = blockCnt + 1
                        blockIdx = i
                    end
                end

                if blockCnt == 1 then
                    missionInfoNode.infoBtn.gameObject:SetActiveIfNecessary(false)
                    missionInfoNode.tipsTxt:SetAndResolveTextStyle(cList[blockIdx].desc)
                elseif blockCnt > 1 then
                    missionInfoNode.infoBtn.gameObject:SetActiveIfNecessary(true)
                    missionInfoNode.tipsTxt:SetAndResolveTextStyle(Language.ui_mis_panel_block_quest_notice)
                    missionInfoNode.infoBtn.onClick:AddListener(function()

                        UIManager:Open(PanelId.ActivityStartReminderPopup,{
                            title = Language.ui_mis_block_quest_subtitle,
                            conditions = conditions,
                            mainTitle = Language.ui_mis_block_quest_title,
                            drawMode = ActivityConst.ACTIVITY_REMINDER_DRAW_MODE.NoComplete
                        })
                    end)
                end
            else
                missionInfoNode.lockTipsNode.gameObject:SetActiveIfNecessary(false)
                missionInfoNode.leftTipsNode.gameObject:SetActiveIfNecessary(missionInfo.isConflicted)


                local missionId = missionInfo.missionId
                if missionId == self.m_missionSystem.trackMissionId and self.m_missionSystem:HasTrackDataForMap() then
                    missionInfoNode.mapBtn.gameObject:SetActive(true)
                    missionInfoNode.mapBtn.onClick:RemoveAllListeners()
                    missionInfoNode.mapBtn.onClick:AddListener(function()
                        if not string.isEmpty(self.m_missionSystem.trackMissionId) then
                            local haveTracker, trackId = self.m_missionSystem:HasShowedTrackDataForMap()
                            if haveTracker then
                                MapUtils.openMapByMissionId(trackId)
                            else
                                Notify(MessageConst.SHOW_TOAST, Language.ui_mis_toast_map_in_challenge)
                            end
                        end
                    end)
                else
                    missionInfoNode.mapBtn.gameObject:SetActive(false)
                    missionInfoNode.mapBtn.onClick:RemoveAllListeners()
                end
            end


            self:_RefreshObjectiveProgress(self.m_selectedMissionId)


            local rewardItemBundles = {}


            if not (missionInfo.isWrapperMission and missionInfo.useRewardWrapper) then
                local findReward, rewardData = Tables.rewardTable:TryGetValue(missionInfo.rewardId or "")
                if findReward then
                    for _,itemBundle in pairs(rewardData.itemBundles) do
                        local itemData = Tables.itemTable[itemBundle.id]
                        table.insert(rewardItemBundles, {
                            id = itemBundle.id,
                            count = itemBundle.count,
                            sortId1 = itemData.sortId1,
                            sortId2 = itemData.sortId2,
                            rarity = itemData.rarity,
                        })
                    end
                end
            else

                local externalType = missionInfo.externalSystemType
                local externalId = missionInfo.externalSystemId

                local rewardInfoGot, rewardItemIds, rewardItemNums = CS.Beyond.Gameplay.MissionSystem.TryGetWrapperMissionReward(externalType, externalId)
                if rewardInfoGot then
                    for i = 0, rewardItemIds.Count - 1 do
                        local itemData = Tables.itemTable[rewardItemIds[i]]
                        table.insert(rewardItemBundles, {
                            id = rewardItemIds[i],
                            count = rewardItemNums[i],
                            sortId1 = itemData.sortId1,
                            sortId2 = itemData.sortId2,
                            rarity = itemData.rarity,
                        })
                    end
                end
            end
            table.sort(rewardItemBundles, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
            local hasReward = #rewardItemBundles > 0
            self.view.rewardFocusKeyHint.gameObject:SetActive(hasReward)
            if hasReward then
                self.view.rewardsNode.gameObject:SetActive(true)
                self.m_getRewardItemCellsFunc = self.m_getRewardItemCellsFunc or UIUtils.genCachedCellFunction(self.view.rewardScrollList)
                self.view.rewardScrollList.onUpdateCell:RemoveAllListeners();
                self.view.rewardScrollList.onUpdateCell:AddListener(function(gameObject, index)
                    local itemCell = self.m_getRewardItemCellsFunc(gameObject)
                    local luaIdx = LuaIndex(index)
                    itemCell:InitItem(rewardItemBundles[luaIdx], true)
                    if DeviceInfo.usingController then
                        itemCell:SetExtraInfo({
                            isSideTips = true,
                        })
                    end
                end)
                self.view.rewardScrollList:UpdateCount(#rewardItemBundles)
                local firstItemGo = self.view.rewardScrollList:Get(0)
                if firstItemGo then
                    self.view.rewardFocusKeyHint.transform.position = firstItemGo.transform.position
                    local keyHintPos = self.view.rewardFocusKeyHint.transform.localPosition
                    keyHintPos.x = keyHintPos.x - 50
                    keyHintPos.y = keyHintPos.y - 30
                    self.view.rewardFocusKeyHint.transform.localPosition = keyHintPos
                end
            else
                self.view.rewardsNode.gameObject:SetActive(false)
            end
        end
    end
    if not bSelectedMissionValid then

        self.view.missionInfoNodeState:SetState("None")
    end
end






MissionCtrl._RefreshObjectiveProgress = HL.Method(HL.String) << function(self, missionId)
    if string.isEmpty(missionId) then
        return
    end

    local displayQuestIds = self.m_missionSystem:GetDisplayQuestIdsByMissionId(missionId)
    local missionInfo = self.m_missionSystem:GetMissionInfo(missionId)

    if displayQuestIds and missionInfo then
        local displayQuestCount = displayQuestIds.Count

        self.m_questCellCache:Refresh(displayQuestCount, function(questCell, luaIdx)
            local csIdx = CSIndex(luaIdx)
            local questId = displayQuestIds[csIdx]
            local questInfo = self.m_missionSystem:GetQuestInfo(questId)
            local questData = self.m_missionSystem:GetQuestData(questId)
            local objectiveCountInQuest = questInfo.objectiveList.Count

            if not missionInfo.isBlocked and objectiveCountInQuest > 0 then
                questCell.gameObject:SetActive(true)
                local allObjectiveComplete = true
                for _, objective in pairs(questInfo.objectiveList) do
                    if not objective.isCompleted then
                        allObjectiveComplete = false
                        break
                    end
                end

                questCell.normalIcon.gameObject:SetActive(not allObjectiveComplete)
                questCell.completeIcon.gameObject:SetActive(allObjectiveComplete)
                questCell.multiObjectiveDeco.gameObject:SetActive(objectiveCountInQuest > 1)
                local optional = questInfo.questType == QuestType.Optional

                questCell.objectiveCache = questCell.objectiveCache or UIUtils.genCellCache(questCell.objectiveCell)
                questCell.objectiveCache:Refresh(objectiveCountInQuest, function(objectiveCell, objectiveLuaIdx)
                    local objectiveCSIdx = CSIndex(objectiveLuaIdx)
                    local objective = questInfo.objectiveList[objectiveCSIdx]

                    local descTxt = nil
                    if not objective.useStrDesc then
                        if optional then
                            descTxt = string.format("<color=#%s>%s</color> %s", OPTIONAL_TEXT_COLOR, Language.ui_optional_quest, objective.runtimeDescription:GetText())
                        else
                            descTxt = objective.runtimeDescription:GetText()
                        end
                    else
                        descTxt = objective.descStr
                    end

                    if objective.isCompleted then
                        descTxt = descTxt:gsub("<@qu%.key>", ""):gsub("</>", "")
                    end
                    objectiveCell.desc:SetAndResolveTextStyle(descTxt)

                    if not missionInfo.isConflicted and objective.isShowProgress then
                        objectiveCell.progress.gameObject:SetActive(true)
                        if objective.isCompleted then
                            objectiveCell.progress.text = string.format("%d/%d", objective.progressToCompareForShow, objective.progressToCompareForShow)
                        else
                            objectiveCell.progress.text = string.format("%d/%d", objective.progressForShow, objective.progressToCompareForShow)
                        end
                    else
                        objectiveCell.progress.gameObject:SetActive(false)
                    end

                    if objective.isCompleted then
                        objectiveCell.desc.color = self.view.config.OBJECTIVE_COMPLETE_FONT_COLOR
                        objectiveCell.progress.color = self.view.config.OBJECTIVE_COMPLETE_FONT_COLOR
                    else
                        objectiveCell.desc.color = self.view.config.OBJECTIVE_NORMAL_FONT_COLOR
                        objectiveCell.progress.color = self.view.config.OBJECTIVE_NORMAL_FONT_COLOR
                    end
                end)
            else
                questCell.gameObject:SetActive(false)
            end
        end)
    end
end



MissionCtrl._RefreshTrackBtn = HL.Method() << function(self)
    local missionInfoNode = self.view.missionInfoNode
    local trackMissionId = self.m_missionSystem:GetTrackMissionId()

    missionInfoNode.trackBtn.onClick:RemoveAllListeners()
    missionInfoNode.stopBtn.onClick:RemoveAllListeners()

    local missionInfo = self.m_missionSystem:GetMissionInfo(self.m_selectedMissionId)
    if missionInfo.isBlocked then
        missionInfoNode.stopBtn.gameObject:SetActive(false)
        missionInfoNode.trackBtn.gameObject:SetActive(false)
    else
        if trackMissionId == self.m_selectedMissionId then
            missionInfoNode.trackBtn.gameObject:SetActive(false)
            missionInfoNode.stopBtn.gameObject:SetActive(true)
        else
            missionInfoNode.trackBtn.gameObject:SetActive(true)
            missionInfoNode.stopBtn.gameObject:SetActive(false)
        end
        missionInfoNode.trackBtn.onClick:AddListener(function()

            if self.m_isClosing then
                return
            end
            local id = self.m_selectedMissionId
            local sys = self.m_missionSystem
            local alreadyTracking = (sys:GetTrackMissionId() == id)
            sys:PushSkipNextTrackInOrOut(id)
            sys:TrackMission(id)
            if alreadyTracking then
                self:_DoTrackJumpAfterServerConfirm(id)
            else
                self.m_pendingTrackJumpMissionId = id
            end
        end)
        missionInfoNode.stopBtn.onClick:AddListener(function()

            if self.m_isClosing then
                return
            end
            local sys = self.m_missionSystem
            sys:PushSkipNextTrackInOrOut(self.m_selectedMissionId)
            self.m_missionSystem:StopTrackMission()
        end)
    end
end



MissionCtrl._RefreshMissionTrackTip = HL.Method() << function(self)
    self:_TraverseAllMissionCell(function(missionId, missionCell)
        local missionInfo = self.m_missionSystem:GetMissionInfo(missionId)
        self:_SetMissionCellTrack(missionCell, missionInfo)
    end)
end




MissionCtrl.OnObjectiveUpdate = HL.Method(HL.Any) << function(self, arg)
    local questId = unpack(arg)
    local missionId = self.m_missionSystem:GetMissionIdByQuestId(questId)
    if not string.isEmpty(self.m_selectedMissionId) and self.m_selectedMissionId == missionId then
        self:_RefreshObjectiveProgress(missionId)
    end
end



MissionCtrl.OnTrackMissionChange = HL.Method() << function(self)
    if self.m_isClosing then
        return
    end

    self:_RefreshTrackBtn()
    self:_RefreshMissionTrackTip()
    self:_UpdateCurrentDisplayTrackBtn()
end



MissionCtrl._OnTrackMissionJumpReady = HL.Method() << function(self)
    if not self.m_pendingTrackJumpMissionId then
        return
    end

    local id = self.m_pendingTrackJumpMissionId
    self.m_pendingTrackJumpMissionId = nil

    local trackMissionId = self.m_missionSystem:GetTrackMissionId()
    if trackMissionId ~= id then
        return
    end

    self:_DoTrackJumpAfterServerConfirm(id)
end



MissionCtrl._UpdateCurrentDisplayTrackBtn = HL.Method() << function(self)
    local missionInfo = self.m_missionSystem:GetMissionInfo(self.m_selectedMissionId)

    if not missionInfo then
        return
    end

    local missionId = missionInfo.missionId
    local missionInfoNode = self.view.missionInfoNode
    if missionId == self.m_missionSystem.trackMissionId and self.m_missionSystem:HasTrackDataForMap() then
        missionInfoNode.mapBtn.gameObject:SetActive(true)
        missionInfoNode.mapBtn.onClick:RemoveAllListeners()
        missionInfoNode.mapBtn.onClick:AddListener(function()
            if not string.isEmpty(self.m_missionSystem.trackMissionId) then
                local haveTracker, trackId = self.m_missionSystem:HasShowedTrackDataForMap()
                if haveTracker then
                    MapUtils.openMapByMissionId(trackId)
                else
                    Notify(MessageConst.SHOW_TOAST, Language.ui_mis_toast_map_in_challenge)
                end
            end
        end)
    else
        missionInfoNode.mapBtn.gameObject:SetActive(false)
        missionInfoNode.mapBtn.onClick:RemoveAllListeners()
    end
end




MissionCtrl.OnMissionStateChange = HL.Method(HL.Any) << function(self, arg)
    if self.m_selectedMissionId ~= "" then
        local missionState = self.m_missionSystem:GetMissionState(self.m_selectedMissionId)
        if missionState == MissionState.None or missionState == MissionState.Failed or missionState == MissionState.Disabled then
            self.m_selectedMissionId = ""
        end
    end

    self:_RefreshMissionList()
    self:_RefreshSelectedMission()
    self:_RefreshMissionInfo()
    self:_RefreshEmptyNode()
    self:_AutoSelectMission(false)
    self:_RefreshNaviSelected()
end



MissionCtrl.OnBlockQuestStateChange = HL.Method() << function(self)
    self:_RefreshMissionList()
    self:_RefreshSelectedMission()
    self:_RefreshMissionInfo()
    self:_RefreshEmptyNode()
    self:_AutoSelectMission(false)
    self:_RefreshNaviSelected()
end




MissionCtrl.OnQuestStateChange = HL.Method(HL.Any) << function(self, arg)
    local questId, questState = unpack(arg)
    local missionId = self.m_missionSystem:GetMissionIdByQuestId(questId)
    local missionState = self.m_missionSystem:GetMissionState(missionId)

    if missionState == MissionState.None or missionState == MissionState.Failed or missionState == MissionState.Disabled then
        return
    end

    self:_RefreshMissionTrackTip()

    if missionId == self.m_selectedMissionId then
        self:_RefreshMissionInfo()
    end
end




MissionCtrl.OnSyncAllMission = HL.Method(HL.Any) << function(self, arg)
    self:_RefreshMissionList()
    self:_RefreshSelectedMission()
    self:_RefreshMissionInfo()
    self:_RefreshNaviSelected()
end



MissionCtrl.OnShow = HL.Override() << function(self)
    self:_RefreshSkipChapterBubble()
end






MissionCtrl.OnAnimationInFinished = HL.Override() << function(self)

end




MissionCtrl._ChangeSelectedMission = HL.Method(HL.Number) << function(self, offset)
    local selectedIndex
    local missionIdInfoList = {}
    for k, v in ipairs(self.m_missionViewInfo) do
        if v.type == MissionListCellType_Chapter then
            for kk, missionViewInfo in pairs(v.missionList) do
                table.insert(missionIdInfoList, { cellIndex = k, subIndex = kk, id = missionViewInfo.id, })
                if missionViewInfo.id == self.m_selectedMissionId then
                    selectedIndex = #missionIdInfoList
                end
            end
        else
            table.insert(missionIdInfoList, { cellIndex = k, id = v.id, })
            if v.id == self.m_selectedMissionId then
                selectedIndex = #missionIdInfoList
            end
        end
    end
    if not selectedIndex then
        return
    end
    local newInfo = missionIdInfoList[selectedIndex + offset]
    if not newInfo then
        return
    end

    self.m_selectedMissionId = newInfo.id

    self.m_doNotPostAudio = true
    self:_RefreshSelectedMission()
    self.m_doNotPostAudio = false

    self:_RefreshMissionInfo()

    if DeviceInfo.usingController and offset == 0 and newInfo.cellIndex > 1 then
        local scrollTarget = math.max(1, newInfo.cellIndex - 2)
        self.view.missionScrollView:ScrollToIndex(CSIndex(scrollTarget), true)
    else
        self.view.missionScrollView:ScrollToIndex(CSIndex(newInfo.cellIndex), true)
    end
    self:_RefreshNaviSelected()
end




MissionCtrl._GetLevelId = HL.Method(HL.Any).Return(HL.String) << function(self, missionInfo)
    if (not missionInfo.isWrapperMission) or (not missionInfo.useLevelIdWrapper) then
        return missionInfo:GetLevelId() or ""
    end
    local externalType = missionInfo.externalSystemType
    local externalId = missionInfo.externalSystemId
    return CS.Beyond.Gameplay.MissionSystem.GetWrapperMissionLevelId(externalType, externalId)
end



MissionCtrl._UpdateShow = HL.Method() << function(self)
    self:_UpdateExpireTime()
end



MissionCtrl._UpdateExpireTime = HL.Method() << function(self)
    if self.m_willExpire then
        local leftTime = (self.m_timeStamp - DateTimeUtils.GetCurrentTimestampByMilliseconds()) / 1000
        local timeTxt = UIUtils.getLeftTime(leftTime)
        self.view.missionInfoNode.timerTxt:SetAndResolveTextStyle(timeTxt)
    end
end




MissionCtrl._ExitAllPhasesExcept = HL.Method(HL.Table) << function(self, keepPhaseIds)
    local keepSet = {}
    for _, pid in ipairs(keepPhaseIds) do keepSet[pid] = true end
    local exitList = {}
    for k = PhaseManager.m_phaseStack:TopIndex(), PhaseManager.m_phaseStack:BottomIndex(), -1 do
        local p = PhaseManager.m_phaseStack:Get(k)
        if not keepSet[p.phaseId] then
            table.insert(exitList, p.phaseId)
        end
    end
    for _, v in ipairs(exitList) do
        if PhaseManager:IsOpen(v) then
            PhaseManager:ExitPhaseFast(v)
        end
    end
end




MissionCtrl._DoTrackJumpAfterServerConfirm = HL.Method(HL.String) << function(self, missionId)



    if self.m_isClosing then
        return
    end

    local jumpInfo = self.m_missionSystem:GetTrackJumpInfoForMission(missionId)
    self.m_isClosing = true
    self:PlayAnimationOutWithCallback(function()
        if jumpInfo.jumpType == TrackJumpType.MapTracker then
            local instId = GameInstance.player.mapManager:GetMissionTrackingMarkInstIdByTrackId(jumpInfo.mapTrackerId)
            local levelId = GameWorld.worldInfo.curLevelId
            if not string.isEmpty(instId) then
                local _, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(instId)
                levelId = markRuntimeData.levelId
            end


            local canOpen, toast = MapUtils.checkCanOpenMapAndParseArgs({instId = instId, levelId = levelId})
            if canOpen then



                PhaseManager:GoToPhase(PhaseId.Map, {instId = instId, levelId = levelId}, function()
                    self:_ExitAllPhasesExcept({PhaseId.Level, PhaseId.Map, PhaseId.Dialog})
                end, true)
            else
                Notify(MessageConst.SHOW_TOAST, toast)
                self:_ExitAllPhasesExcept({PhaseId.Level, PhaseId.Dialog})
            end
            return
        elseif jumpInfo.jumpType == TrackJumpType.SNS then
            if PhaseManager:CheckCanOpenPhaseAndToast(PhaseId.SNS, {dialogId = jumpInfo.snsDialogId}) then

                PhaseManager:GoToPhase(PhaseId.SNS, {dialogId = jumpInfo.snsDialogId}, function()
                    self:_ExitAllPhasesExcept({PhaseId.Level, PhaseId.SNS, PhaseId.Dialog})
                end, true)
                return
            end
        end
        self:_ExitAllPhasesExcept({PhaseId.Level, PhaseId.Dialog})
    end)
end



MissionCtrl._RefreshSkipChapterBubble = HL.Method() << function(self)
    local show = false
    if self.m_missionFilterType == MissionFilterType_All
        or self.m_missionFilterType == MissionViewType.MissionViewMain then
        local status = GameInstance.player.activitySystem:GetActivityStatus(SKIP_CHAPTER_ACTIVITY_ID)
        if status == ActivityStatus.InProgress then
            local hasVar, value = GameInstance.player.globalVar:TryGetClientVar(SKIP_CHAPTER_GLOBAL_VAR_KEY)
            if not hasVar or value ~= 1 then
                show = true
            end
        end
    end
    self.view.skipChapterBubble.gameObject:SetActiveIfNecessary(show)
end



MissionCtrl._OnClickSkipChapterBubble = HL.Method() << function(self)
    local dontShowAgain = false
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_MISSION_SKIP_CHAPTER_POPUP_CONTENT,
        toggle = {
            toggleText = Language.LUA_MISSION_SKIP_CHAPTER_POPUP_TOGGLE,
            isOn = false,
            onValueChanged = function(isOn)
                dontShowAgain = isOn
            end,
            styleType = CommonPopUpCtrl.EToggleStyle.Square,
            onHintTextId = "key_hint_mission_skipchapter_popup_toggle",
            offHintTextId = "key_hint_mission_skipchapter_popup_toggle"
        },
        onConfirm = function()
            if dontShowAgain then
                GameInstance.player.globalVar:SetClientVar(SKIP_CHAPTER_GLOBAL_VAR_KEY, 1)
            end
            Utils.jumpToSystem(SKIP_CHAPTER_JUMP_ID)
        end,
    })
end

HL.Commit(MissionCtrl)
