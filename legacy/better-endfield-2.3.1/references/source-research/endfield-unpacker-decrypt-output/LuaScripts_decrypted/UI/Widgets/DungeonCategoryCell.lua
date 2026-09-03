local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

DungeonCategoryCell = HL.Class('DungeonCategoryCell', UIWidgetBase)



DungeonCategoryCell.m_dungeonInfos = HL.Field(HL.Table)




DungeonCategoryCell._OnFirstTimeInit = HL.Override() << function(self)
end


DungeonCategoryCell.InitDungeonCategoryCell = HL.Method(HL.Table) << function(self, infosBundle)
    self:_FirstTimeInit()

    infosBundle.hasRead = true
    self.m_dungeonInfos = infosBundle.infos

    for _, v in ipairs(self.m_dungeonInfos) do
        v.hasRead = true
    end
    local cellInfo = self:UnionDungeonInfo(self.m_dungeonInfos)
    self.view.dungeonCell:InitAdventureDungeonCell(cellInfo, true)
end

DungeonCategoryCell.GetFirstSubDungeonCellInCategory = HL.Method().Return(HL.Userdata) << function(self)
    return self.m_genDungeonCells:GetItem(1)
end

DungeonCategoryCell.UnionDungeonInfo = HL.Method(HL.Table).Return(HL.Table) << function(self, dungeonInfos)
    if not dungeonInfos or #dungeonInfos == 0 then
        return {}
    end

    local res = dungeonInfos[1]
    for i=2, #dungeonInfos do
        local curr = dungeonInfos[i]
        if curr.isActive then
            res.seriesId = curr.seriesId
        end

        if curr.staminaMin then
            if not res.staminaMin or (curr.staminaMin < res.staminaMin) then
                res.staminaMin = curr.staminaMin
            end
        end
        if curr.staminaMax then
            if not res.staminaMax or (curr.staminaMax > res.staminaMax) then
                res.staminaMax = curr.staminaMax
            end
        end

        if curr.rewardInfos then
            for _, currV in ipairs(curr.rewardInfos) do
                local exist = false
                for _, resV in ipairs(res.rewardInfos) do
                    if currV.id == resV.id then
                        exist = true
                    end
                end
                if not exist then
                    table.insert(res.rewardInfos, currV)
                end
            end
        end
    end
    if res.staminaMin and res.staminaMax and res.staminaMin ~= res.staminaMax then
        res.staminaTxt = res.staminaMin .. "~" .. res.staminaMax
    end

    if #dungeonInfos > 1 then
        table.sort(res.rewardInfos, Utils.genSortFunction({ "gainedSortId", "rewardTypeSortId", "rarity", "type" }))
    end
    return res
end

HL.Commit(DungeonCategoryCell)
return DungeonCategoryCell

