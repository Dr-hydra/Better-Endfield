local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

TipsGemAttributeNode = HL.Class('TipsGemAttributeNode', UIWidgetBase)

TipsGemAttributeNode.m_termGroupList = HL.Field(HL.Table)

TipsGemAttributeNode.m_termGroupCells = HL.Field(HL.Forward("UIListCache"))


TipsGemAttributeNode._OnFirstTimeInit = HL.Override() << function(self)

    self.m_termGroupCells = UIUtils.genCellCache(self.view.tipsGemAttributeCell)
end

TipsGemAttributeNode.InitTipsGemAttributeNode = HL.Method() << function(self)
    self:_FirstTimeInit()
end

TipsGemAttributeNode.RefreshView = HL.Method(HL.String) << function(self, itemId)

    local gemBoxData = Tables.GemCustomizationBox[itemId]
    local itemData = Tables.itemTable[itemId]
    local termGroupList = {}
    local termPoolIdData = Tables.GemItemId2TermPoolIdDataTable[gemBoxData.gemItemId]
    local poolId1 = termPoolIdData.termPoolId1;
    local poolId2 = termPoolIdData.termPoolId2;
    local poolId3 = termPoolIdData.termPoolId3;
    local poolIdList = {poolId1, poolId2, poolId3}
    local termGroupNum = 0
    for index, poolId in ipairs(poolIdList) do
        local gemTermIdListData = Tables.TermPoolId2TermPoolIdDataTable[poolId]
        if gemTermIdListData == nil then
            termGroupList[index] = {}
        else
            local termIdList = {}
            for j = 0, #gemTermIdListData.gemTermIdList - 1 do
                local termId = gemTermIdListData.gemTermIdList[j]
                table.insert(termIdList, termId)
            end
            termGroupList[index] = termIdList
            termGroupNum = termGroupNum + 1
        end
    end
    self.m_termGroupList = termGroupList

    self.view.detailTitleTxt.text = itemData.desc
    self.view.detailContentTxt.text = itemData.decoDesc
    self.view.entryNumberTxt.text = gemBoxData.lockedTermCount


    self.m_termGroupCells:Refresh(3, function(cell, index)
        cell:InitTipsGemAttributeCell()
        cell:RefreshUI(index, self.m_termGroupList[index])
    end)
end

HL.Commit(TipsGemAttributeNode)
return TipsGemAttributeNode

