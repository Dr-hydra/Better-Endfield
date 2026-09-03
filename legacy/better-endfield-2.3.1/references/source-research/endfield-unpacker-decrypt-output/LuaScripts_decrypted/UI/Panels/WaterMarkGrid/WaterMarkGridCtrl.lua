
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WaterMarkGrid

WaterMarkGridCtrl = HL.Class('WaterMarkGridCtrl', uiCtrl.UICtrl)

WaterMarkGridCtrl.m_waterMarkCellCache = HL.Field(HL.Forward("UIListCache"))

WaterMarkGridCtrl.m_uid = HL.Field(HL.String) << ""





WaterMarkGridCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHANGE_WATER_MARK_GRID] = 'ChangeWaterMarkGrid',
    [MessageConst.ON_COMMON_BLEND_IN] = 'ChangeWaterMarkGrid',
}

WaterMarkGridCtrl.OnEnterMainGame = HL.StaticMethod() << function()
    local enableWaterMark = CS.Beyond.Cfg.RemoteGameCfg.instance.data.enableMobileFullScreenWaterMark
    if not enableWaterMark then
        return
    end

    UIManager:Open(PANEL_ID)
end


WaterMarkGridCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_waterMarkCellCache = UIUtils.genCellCache(self.view.waterMarkCell)
    self.m_uid = GameInstance.player.playerInfoSystem.roleId

    self:_InitWaterMarkCell()
end

WaterMarkGridCtrl.ChangeWaterMarkGrid = HL.Method() << function(self)
    self:UpdateWaterMarkCell()
end

WaterMarkGridCtrl.UpdateWaterMarkCell = HL.Method() << function(self)
    local positionX, positionY, rotationZ = self:_GetRandomTrans()
    self.m_waterMarkCellCache:Update(function(cell, luaIndex)
        self:_UpdateMarkCellTrans(cell, {
            hasUid = false,
            positionX = positionX,
            positionY = positionY,
            rotationZ = rotationZ,
        })
    end)
end

WaterMarkGridCtrl._InitWaterMarkCell = HL.Method() << function(self)
    local gridLayout = self.view.gridWaterMarkRootGridLayoutGroup
    local spacingX = gridLayout.spacing.x
    local spacingY = gridLayout.spacing.y
    local cellX = gridLayout.cellSize.x
    local cellY = gridLayout.cellSize.y

    local screenX = self.view.rectTransform.rect.width
    local screenY = self.view.rectTransform.rect.height

    local horizontalCount = math.ceil((screenX - cellX) / (cellX + spacingX))
    local verticalCount = math.ceil((screenY - cellY) / (cellY + spacingY))

    local reviseWidth = cellX + (cellX + spacingX) * horizontalCount
    local reviseHeight = cellY + (cellY + spacingY) * verticalCount
    self.view.gridWaterMarkRoot.sizeDelta = Vector2(reviseWidth, reviseHeight)

    local positionX, positionY, rotationZ = self:_GetRandomTrans()
    self.m_waterMarkCellCache:Refresh((horizontalCount + 1) * (verticalCount + 1), function(cell, luaIndex)
        self:_UpdateMarkCellTrans(cell, {
            hasUid = true,
            positionX = positionX,
            positionY = positionY,
            rotationZ = rotationZ,
        })
    end)

end

WaterMarkGridCtrl._GetRandomTrans = HL.Method().Return(HL.Number, HL.Number, HL.Number) << function(self)
    local posRangeX = self.view.config.POSITIONX_RANDOM_RANGE
    local posRangeY = self.view.config.POSITIONY_RANDOM_RANGE
    local rotRange = self.view.config.ROTATION_RANDOM_RANGE
    local positionX = math.random(posRangeX.x, posRangeX.y)
    local positionY = math.random(posRangeY.x, posRangeY.y)
    local rotationZ = math.random(rotRange.x, rotRange.y)

    return positionX, positionY, rotationZ
end

WaterMarkGridCtrl._UpdateMarkCellTrans = HL.Method(HL.Any, HL.Table) << function(self, cell, info)
    if info.hasUid then
        cell.text.text = self.m_uid
    end

    cell.root.anchoredPosition = Vector2(info.positionX, info.positionY)
    cell.root.rotation = Quaternion.Euler(0, 0, info.rotationZ)
end

HL.Commit(WaterMarkGridCtrl)
