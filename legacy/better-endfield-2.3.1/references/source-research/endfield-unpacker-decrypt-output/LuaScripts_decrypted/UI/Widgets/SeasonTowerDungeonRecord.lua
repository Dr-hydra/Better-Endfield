local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local ROLE_CELL_COUNT = 4

SeasonTowerDungeonRecord = HL.Class('SeasonTowerDungeonRecord', UIWidgetBase)

SeasonTowerDungeonRecord.m_levelId = HL.Field(HL.String) << ""

SeasonTowerDungeonRecord.m_levelRecord = HL.Field(HL.Any)

SeasonTowerDungeonRecord.m_roleCellCache = HL.Field(HL.Any)


SeasonTowerDungeonRecord.InitSeasonTowerDungeonRecord = HL.Method(HL.String, HL.Any) << function(self, levelId, levelRecord)
    self:_FirstTimeInit()

    self.m_levelId = levelId
    self.m_levelRecord = levelRecord
    self.m_roleCellCache = self.m_roleCellCache or UIUtils.genCellCache(self.view.roleCell)
    self:_RefreshDungeonRecord()
end

SeasonTowerDungeonRecord._RefreshDungeonRecord = HL.Method() << function(self)
    local _, cfg = Tables.gameMechanicGroupTable:TryGetValue(self.m_levelId)
    if cfg then
        self.view.nameTxt.text = cfg.gameGroupName
    end


    if not self.m_levelRecord then
        self.m_roleCellCache:Refresh(ROLE_CELL_COUNT, function(cell, luaIndex)
            cell.stateController:SetState("Empty")
        end)
        self.view.starList:InitStarGroupWithState(3,0,
            "02", "01")
    else
        self.m_roleCellCache:Refresh(ROLE_CELL_COUNT, function(cell, luaIndex)
            local formation = self.m_levelRecord.bestPassFormation
            local hasChar = formation and luaIndex <= formation.Count
            cell.stateController:SetState(hasChar and "Nrl" or "Empty")
            if hasChar then
                local roleInfo = formation[CSIndex(luaIndex)]
                local templateId = CSCharUtils.GetCharTemplateId(roleInfo.templateId)
                cell.charHeadCell:InitCharFormationHeadCell({
                    templateId = templateId,
                    level = roleInfo.level,
                    potentialLevel = roleInfo.potential,
                })
            end
        end)
        self.view.starList:InitStarGroupWithState(3, self.m_levelRecord.starNum,
            self.m_levelRecord.completeTask and "03" or "02", "01")
    end
end

HL.Commit(SeasonTowerDungeonRecord)
return SeasonTowerDungeonRecord

