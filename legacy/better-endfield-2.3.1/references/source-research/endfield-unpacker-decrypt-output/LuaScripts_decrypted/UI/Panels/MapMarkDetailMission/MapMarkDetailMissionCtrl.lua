
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailMission
local ChapterType = CS.Beyond.Gameplay.ChapterType
local QuestType = GEnums.QuestType

MapMarkDetailMissionCtrl = HL.Class('MapMarkDetailMissionCtrl', uiCtrl.UICtrl)

local CHAPTER_ICON_PATH = "Mission/ChapterIconNew"
local OPTIONAL_TEXT_COLOR = "C7EC59"

MapMarkDetailMissionCtrl.m_questList = HL.Field(HL.Forward('UIListCache'))

MapMarkDetailMissionCtrl.m_rewardList = HL.Field(HL.Forward('UIListCache'))





MapMarkDetailMissionCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailMissionCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)

    self.m_questList = UIUtils.genCellCache(self.view.singleQuest)
    self.m_rewardList = UIUtils.genCellCache(self.view.rewardItem)

    args = args or {}
    local markInstId = args.markInstId
    local missionSystem = GameInstance.player.mission

    local getRuntimeDataSuccess, markDetailData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)

    if getRuntimeDataSuccess == false then
        logger.LogError("地图详情页获取实例数据失败" .. self.m_instId)
        return
    end

    local missionId = markDetailData.missionInfo.missionId
    local missionRuntimeAsset = missionSystem:GetMissionInfo(missionId)
    local missionData = missionSystem.missions:get_Item(missionId)
    local chapterId = markDetailData.missionInfo.chapterId

    self:_FillQuestInfo({
        missionRuntimeAsset = missionRuntimeAsset,
        missionData = missionData,
    })

    self:_FillRewardsInfo({
        missionRuntimeAsset = missionRuntimeAsset,
    })

    local chapterInfo = GameInstance.player.mission:GetChapterInfo(chapterId)
    if string.isEmpty(chapterId) then
        self.view.icon.gameObject:SetActive(false)
        self.view.icon.sprite = nil
    else
        local chapterConfig = UIConst.CHAPTER_ICON_CONFIGS[chapterInfo.type]

        if not string.isEmpty(chapterInfo.icon) then
            self.view.icon.gameObject:SetActive(true)
            if chapterInfo.type == ChapterType.Main then
                self.view.icon:LoadSprite(CHAPTER_ICON_PATH, chapterInfo.icon)
            else
                self.view.icon:LoadSprite("CharInfo", chapterInfo.icon)
            end
        elseif not string.isEmpty(chapterConfig.icon) then
            self.view.icon.gameObject:SetActive(true)
            if chapterInfo.type == ChapterType.Main then
                self.view.icon:LoadSprite(CHAPTER_ICON_PATH, chapterConfig.icon)
            else
                self.view.icon:LoadSprite("CharInfo", chapterConfig.icon)
            end
        else
            self.view.icon.gameObject:SetActive(false)
            self.view.icon.sprite = nil
        end
    end

    self.view.detailCommon.gameObject:SetActive(true)
    self.view.detailCommon:InitMapMarkDetailCommon( {
        bigBtnActive = true,

        bigBtnCallback = function()
            self:_RemoveTrace({})
        end,

        bigBtnText = Language["ui_map_common_tracer_cancel"],
        bigBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.REMOVE_TRACE,

        titleText = missionRuntimeAsset.missionName:GetText(),
        descText = missionRuntimeAsset:GetMissionDesc():GetText(),

        markInstId = markInstId
    })

    if DeviceInfo.usingController then
        self.view.naviGroup.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end
end

MapMarkDetailMissionCtrl._FillQuestInfo = HL.Method(HL.Table) << function(self, args)
    local missionRuntimeAsset = args.missionRuntimeAsset
    local missionData = args.missionData

    local questList = missionData:GetDisplayQuestIds()
    local questCount = questList.Count
    self.m_questList:Refresh(questCount, function(quest, index)
        local questName = questList[CSIndex(index)]
        local questInfo = missionRuntimeAsset.questDic:get_Item(questName)
        local optional = questInfo.questType == QuestType.Optional

        if not quest.m_objectiveList then
            quest.m_objectiveList = UIUtils.genCellCache(quest.objective)
        end

        local objectiveCount = questInfo.objectiveList.Count

        quest.m_objectiveList:Refresh(objectiveCount, function(objective, objIndex)
            local objectiveData = questInfo.objectiveList[CSIndex(objIndex)]
            local descTxt
            if not objectiveData.useStrDesc then
                if optional then
                    descTxt = string.format("<color=#%s>%s</color> %s", OPTIONAL_TEXT_COLOR,
                        Language.ui_optional_quest, objectiveData.runtimeDescription:GetText())
                else
                    descTxt = objectiveData.runtimeDescription:GetText()
                end
            else
                descTxt = objectiveData.descStr
            end

            objective.descText:SetAndResolveTextStyle(descTxt)
            objective.slc.gameObject:SetActive(questInfo.optional)
            objective.slcPadding.gameObject:SetActive(questInfo.optional == false)
            objective.number.gameObject:SetActive(objectiveData.isShowProgress)
            if objectiveData.isShowProgress then
                local numberBefore = objectiveData.progressForShow
                local numberAfter = objectiveData.progressToCompareForShow
                objective.number.text = string.format("%d/%d", numberBefore, numberAfter)
            end
            if objectiveData.isCompleted then
                objective.animationWrapper:Play("map_detail_mission_objective_finish")
            end
        end)


        LayoutRebuilder.ForceRebuildLayoutImmediate(quest.rectTransform)

        quest.m_objectiveList:Update(function(objective, objIndex)
            LayoutRebuilder.ForceRebuildLayoutImmediate(objective.textTransform)
            objective.textHolderSizeFollower:SyncSize()
        end)

        LayoutRebuilder.ForceRebuildLayoutImmediate(quest.rectTransform)
    end)
end

MapMarkDetailMissionCtrl._FillRewardsInfo = HL.Method(HL.Table) << function(self, args)
    local missionRuntimeAsset = args.missionRuntimeAsset
    local rewardItemBundles = {}
    local findReward, rewardData = Tables.rewardTable:TryGetValue(missionRuntimeAsset.rewardId or "")

    if findReward then
        for _, itemBundle in pairs(rewardData.itemBundles) do
            local itemCfg = Tables.itemTable[itemBundle.id]
            table.insert(rewardItemBundles, {
                id = itemBundle.id,
                count = itemBundle.count,
                sortId1 = itemCfg.sortId1,
                sortId2 = itemCfg.sortId2,
                rarity = itemCfg.rarity,
                type = itemCfg.type:ToInt(),
            })
        end
    end

    local rewardItemCount = #rewardItemBundles
    if rewardItemCount == 0 then
        self.view.reward.gameObject:SetActive(false)
        return
    end

    table.sort(rewardItemBundles, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
    self.m_rewardList:Refresh(rewardItemCount, function(item, index)
        self.view.detailCommon:InitDetailItem(item, rewardItemBundles[index], {
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
            tipsPosTransform = self.view.scrollView,
        })
    end)
end

MapMarkDetailMissionCtrl._RemoveTrace = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    GameInstance.player.mission:StopTrackMission()
    Notify(MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL)
end

HL.Commit(MapMarkDetailMissionCtrl)
