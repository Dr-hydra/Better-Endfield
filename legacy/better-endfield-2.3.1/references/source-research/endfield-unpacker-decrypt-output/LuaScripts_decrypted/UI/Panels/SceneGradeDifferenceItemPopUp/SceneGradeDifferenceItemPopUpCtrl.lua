
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SceneGradeDifferenceItemPopUp
local PHASE_ID = PhaseId.SceneGradeDifferenceItemPopUp
SceneGradeDifferenceItemPopUpCtrl = HL.Class('SceneGradeDifferenceItemPopUpCtrl', uiCtrl.UICtrl)






SceneGradeDifferenceItemPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {

}

SceneGradeDifferenceItemPopUpCtrl.m_sceneGradeCellList = HL.Field(HL.Forward('UIListCache'))


SceneGradeDifferenceItemPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.SceneGradeDifferenceItemPopUp)
    end)
    self.view.closeButton.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.SceneGradeDifferenceItemPopUp)
    end)

    local curLevelGrade = args.sceneGrade
    self.view.titleText.text = args.titleText
    local itemLists = args.itemLists

    self.m_sceneGradeCellList = UIUtils.genCellCache(self.view.singleSceneGrade)
    self.m_sceneGradeCellList:Refresh(#itemLists, function(sceneGradeCell, sceneGrade)
        sceneGradeCell.normalRoot.gameObject:SetActive(sceneGrade ~= curLevelGrade)
        sceneGradeCell.nowRoot.gameObject:SetActive(sceneGrade == curLevelGrade)
        sceneGradeCell.normalTitleText.text = Language[string.format("ui_maplevel_level%d", sceneGrade)]
        sceneGradeCell.nowTitleText.text = Language[string.format("ui_maplevel_level%d", sceneGrade)]
        self:_FillSceneGradeCellItem(sceneGradeCell, itemLists[sceneGrade])
    end)

    local targetScrollToCell = self.m_sceneGradeCellList:Get(curLevelGrade)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.scrollRect.content)
    self.view.scrollRect:ScrollToRectTransform(targetScrollToCell.gameObject.transform, true)
end

SceneGradeDifferenceItemPopUpCtrl._FillSceneGradeCellItem = HL.Method(HL.Any, HL.Any)
        << function(self, sceneGradeCell, displayList)
    local itemCount = #displayList
    local rowNeedCount = itemCount / self.view.config.maxItemPerRow
    if (itemCount % self.view.config.maxItemPerRow) >= 1 then
        rowNeedCount = rowNeedCount + 1
    end
    sceneGradeCell.rowList = UIUtils.genCellCache(sceneGradeCell.singleItemRow)
    sceneGradeCell.rowList:Refresh(rowNeedCount, function(row, rowIndex)
        local itemIndexLowNotIncluded = (rowIndex - 1) * self.view.config.maxItemPerRow
        local itemIndexHighIncluded = rowIndex * self.view.config.maxItemPerRow
        itemIndexHighIncluded = math.min(itemIndexHighIncluded, itemCount)
        row.itemListCache = UIUtils.genCellCache(row.item)
        row.itemListCache:Refresh(itemIndexHighIncluded - itemIndexLowNotIncluded, function(item, indexOffset)
            local itemInfoPack = displayList[indexOffset + itemIndexLowNotIncluded]
            item:InitItem(itemInfoPack, true)
        end)
    end)
end

HL.Commit(SceneGradeDifferenceItemPopUpCtrl)
