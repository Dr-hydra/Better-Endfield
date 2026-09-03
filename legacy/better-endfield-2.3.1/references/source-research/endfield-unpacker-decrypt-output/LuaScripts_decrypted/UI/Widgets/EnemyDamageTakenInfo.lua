local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

EnemyDamageTakenInfo = HL.Class('EnemyDamageTakenInfo', UIWidgetBase)

local ELEMENT_RESISTANCE_DATA = {
    { "physicalResistance", "Physical"},
    { "fireResistance", "Fire"},
    { "pulseResistance", "Pulse"},
    { "crystResistance", "Cryst"},
    { "naturalResistance", "Natural"},
}

EnemyDamageTakenInfo.m_levelCellCache = HL.Field(HL.Forward("UIListCache"))


EnemyDamageTakenInfo._OnFirstTimeInit = HL.Override() << function(self)
    self.m_levelCellCache = UIUtils.genCellCache(self.view.entry)
end

EnemyDamageTakenInfo.InitEnemyDamageTakenInfo = HL.Method(HL.String) << function(self, enemyTemplateId)
    self:_FirstTimeInit()

    local _, enemyData = Tables.enemyAttributeTemplateTable:TryGetValue(enemyTemplateId)
    if not enemyData then
        self.m_levelCellCache:Refresh(0)
    end
    self.m_levelCellCache:Refresh(#ELEMENT_RESISTANCE_DATA, function(cell, index)
        local data = ELEMENT_RESISTANCE_DATA[index]
        local value = enemyData[data[1]]
        local scalar = 1- value / 100
        cell.stateController:SetState(data[2])
        cell.gradeTxt.text = self:_GetDamageTakenLevelText(scalar)
    end)
end

EnemyDamageTakenInfo._GetDamageTakenLevelText = HL.Method(HL.Number).Return(HL.String) << function(self, damageTakenScalar)
    for _, data in ipairs(Tables.enemyDamageTakenLevelTable) do
        if damageTakenScalar <= data.damageTakenScalar then
            return data.name
        end
    end
    return ''
end

HL.Commit(EnemyDamageTakenInfo)
return EnemyDamageTakenInfo

