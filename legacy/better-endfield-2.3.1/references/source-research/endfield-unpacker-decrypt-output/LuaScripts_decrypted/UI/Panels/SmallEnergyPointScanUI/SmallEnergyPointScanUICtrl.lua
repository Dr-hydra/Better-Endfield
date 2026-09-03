
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SmallEnergyPointScanUI

local MAX_SHOW_ENEMY_COUNT = 10

local DISPLAY_ENEMY_TYPE = GEnums.DisplayEnemyType
SmallEnergyPointScanUICtrl = HL.Class('SmallEnergyPointScanUICtrl', uiCtrl.UICtrl)






SmallEnergyPointScanUICtrl.s_messages = HL.StaticField(HL.Table) << {

}

SmallEnergyPointScanUICtrl.OnAddSmallEnergyPointUI = HL.StaticMethod(HL.Any) << function(args)

    local ctrl = SmallEnergyPointScanUICtrl.AutoOpen(PANEL_ID, args)
    local entity, enemyLv, enemyId2Count = unpack(args)
    ctrl:AddSmallEnergyPoint(entity, enemyLv, enemyId2Count)
end

SmallEnergyPointScanUICtrl.OnRemoveSmallEnergyPointUI = HL.StaticMethod(HL.Any) << function(args)
    local opened, ctrl = UIManager:IsOpen(PANEL_ID)
    if not opened then
        return
    end
    local entity = unpack(args)
    if ctrl.m_smallEnergyPointObjDict[entity] ~= nil then
        local cell = ctrl.m_smallEnergyPointObjDict[entity]
        cell.smallEnergy:Clear()
        cell.rectTransform.gameObject:SetActive(false)
        table.insert(ctrl.m_smallEnergyPointObjPool, cell)
        ctrl.m_smallEnergyPointObjDict[entity] = nil
    end
end

SmallEnergyPointScanUICtrl.m_smallEnergyPointObjDict = HL.Field(HL.Table)

SmallEnergyPointScanUICtrl.m_smallEnergyPointObjPool = HL.Field(HL.Table)

SmallEnergyPointScanUICtrl.m_smallEnergyPointLogicIdDict = HL.Field(HL.Table)


SmallEnergyPointScanUICtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_smallEnergyPointObjDict = {}
    self.m_smallEnergyPointObjPool = {}
    self.m_smallEnergyPointLogicIdDict = {}
end

SmallEnergyPointScanUICtrl.OnClose = HL.Override() << function(self)
    if self.m_smallEnergyPointObjDict ~= nil then
        for _, v in pairs(self.m_smallEnergyPointObjDict) do
            if v.smallEnergyPoint then
                v.smallEnergyPoint:Clear()
                GameObject.Destroy(v.smallEnergyPoint.gameObject)
            end
        end
        self.m_smallEnergyPointObjDict = nil
    end

    if self.m_smallEnergyPointObjPool ~= nil then
        for _, v in ipairs(self.m_smallEnergyPointObjPool) do
            if v.smallEnergyPoint then
                GameObject.Destroy(v.smallEnergyPoint.gameObject)
            end
        end
        self.m_smallEnergyPointObjPool = nil
    end
end

SmallEnergyPointScanUICtrl.AddSmallEnergyPoint = HL.Method(HL.Any, HL.Any, HL.Any)
        << function(self, targetObject, enemyLv, enemyId2Count)
    if self.m_smallEnergyPointObjDict[targetObject] == nil then
        self.m_smallEnergyPointObjDict[targetObject] = self:_CreateSmallEnergyPoint()
    end

    local smallEnergyPointScanUI = self.m_smallEnergyPointObjDict[targetObject].smallEnergy
    smallEnergyPointScanUI:SetTarget(targetObject)
    self.m_smallEnergyPointObjDict[targetObject].gameObject:SetActive(true)
    self.m_smallEnergyPointLogicIdDict[smallEnergyPointScanUI.entityLogicId] = smallEnergyPointScanUI

    self:_InitSmallEnergyPoint(self.m_smallEnergyPointObjDict[targetObject], targetObject, enemyLv, enemyId2Count)
end

SmallEnergyPointScanUICtrl._InitSmallEnergyPoint = HL.Method(HL.Any, HL.Any, HL.Any, HL.Any)
        << function(self, smallEnergyPoint, target, enemyLv, enemyId2Count)
    if not target or not smallEnergyPoint then
        return
    end

    local enemyVOs = {}
    for enemyId, count in cs_pairs(enemyId2Count) do
        local succ, enemyCfgData = Tables.enemyTable:TryGetValue(enemyId)
        if succ then
            local enemyVO = {}
            local templateId = enemyCfgData.templateId
            local templateCfgData = Tables.enemyTemplateDisplayInfoTable[templateId]

            enemyVO.displayType = templateCfgData.displayType
            enemyVO.sortId = templateCfgData.displayType:GetHashCode()

            for _ = 1, count do
                table.insert(enemyVOs, enemyVO)
            end
        end
    end

    table.sort(enemyVOs, Utils.genSortFunction({ "sortId"}))

    if not smallEnergyPoint.enemyTypeItemCellCache then
        smallEnergyPoint.enemyTypeItemCellCache = UIUtils.genCellCache(smallEnergyPoint.enemyTypeItemCell)
    end


    local cellCache = smallEnergyPoint.enemyTypeItemCellCache
    cellCache:Refresh(math.min(#enemyVOs, MAX_SHOW_ENEMY_COUNT), function(cell, luaIndex)
        local enemyVO = enemyVOs[luaIndex]
        cell.typeImgLevel1.gameObject:SetActive(DISPLAY_ENEMY_TYPE.Normal == enemyVO.displayType)
        cell.typeImgLevel2.gameObject:SetActive(DISPLAY_ENEMY_TYPE.Elite == enemyVO.displayType)
        cell.typeImgLevel3.gameObject:SetActive(DISPLAY_ENEMY_TYPE.HighLevel == enemyVO.displayType)
    end)

    local dangerous = CS.Beyond.Lua.UtilsForLua.CheckExtremeDangerousByLevel(enemyLv)
    if dangerous then
        smallEnergyPoint.nameNode:SetState("Warn")
        smallEnergyPoint.lvText.text = enemyLv
    else
        smallEnergyPoint.nameNode:SetState("Normal")
        smallEnergyPoint.levelTxt:SetText(string.format(Language.LUA_SMALL_ENERGY_POINT_LEVEL, enemyLv))
    end
end

SmallEnergyPointScanUICtrl._CreateSmallEnergyPoint = HL.Method().Return(HL.Table) << function(self)
    if self.m_smallEnergyPointObjPool ~= nil and #self.m_smallEnergyPointObjPool > 0 then
        local result = self.m_smallEnergyPointObjPool[#self.m_smallEnergyPointObjPool]
        table.remove(self.m_smallEnergyPointObjPool, #self.m_smallEnergyPointObjPool)
        return result
    else
        local obj = self:_CreateWorldGameObject(self.view.config.SMALL_ENERGY_POINT_ENEMY_INFO_NODE)
        local result = Utils.wrapLuaNode(obj)
        return result
    end
end

HL.Commit(SmallEnergyPointScanUICtrl)
