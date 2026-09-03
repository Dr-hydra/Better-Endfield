local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SeasonTowerEnemyBuffPopup

SeasonTowerEnemyBuffPopupCtrl = HL.Class('SeasonTowerEnemyBuffPopupCtrl', uiCtrl.UICtrl)


SeasonTowerEnemyBuffPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BOUNTY_ENEMY_INFO_UPDATE] = '_OnBountyEnemyUpdate',
}

SeasonTowerEnemyBuffPopupCtrl.m_getCell = HL.Field(HL.Function)
SeasonTowerEnemyBuffPopupCtrl.m_closeCb = HL.Field(HL.Function)
SeasonTowerEnemyBuffPopupCtrl.m_dungeonId = HL.Field(HL.String) << ""
SeasonTowerEnemyBuffPopupCtrl.m_keys = HL.Field(HL.Table)

SeasonTowerEnemyBuffPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_dungeonId = arg and arg.dungeonId or ""
    self.m_closeCb = arg and arg.closeCb or nil

    self.view.closeButton.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    self.m_getCell = UIUtils.genCachedCellFunction(self.view.buffScrollList)
    self.view.buffScrollList.onUpdateCell:AddListener(function(go, csIndex)
        self:_OnUpdateCell(self.m_getCell(go), LuaIndex(csIndex))
    end)

    if DeviceInfo.usingController then
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    end

    self:_Refresh()
end

SeasonTowerEnemyBuffPopupCtrl._Refresh = HL.Method() << function(self)
    self.m_keys = {}
    local orderedKeys = GameWorld.battle.bountyEnemyRegisterOrder
    for csIndex = 0, orderedKeys.Count - 1 do
        table.insert(self.m_keys, orderedKeys[csIndex])
    end
    self.view.buffScrollList:UpdateCount(#self.m_keys)
end

SeasonTowerEnemyBuffPopupCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    if not cell then
        return
    end

    if luaIndex < 1 or luaIndex > #self.m_keys then
        return
    end

    local key = self.m_keys[luaIndex]
    local bountyEnemies = GameWorld.battle.bountyEnemies

    local _,info = bountyEnemies:TryGetValue(key)


    cell.stateController:SetState(info.isDead and "Lock" or "UnLock")


    local enemyInfo = string.isEmpty(info.enemyId) and nil or UIUtils.getEnemyInfoByIdAndLevel(info.enemyId, nil)
    if enemyInfo then
        cell.titleTxt.text = enemyInfo.name or ""
        if not string.isEmpty(enemyInfo.templateId) then
            cell.monsterIcon:LoadSprite(UIConst.UI_SPRITE_MONSTER_ICON, enemyInfo.templateId)
        end
    else
        cell.titleTxt.text = ""
    end


    cell.buffTxt:SetAndResolveTextStyle(info.descText)



    if not string.isEmpty(info.buffIcon) then
        cell.buffIcon:LoadSprite(UIConst.UI_SPRITE_SEASONTOWER, info.buffIcon)
    end
end

SeasonTowerEnemyBuffPopupCtrl._OnBountyEnemyUpdate = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    self:_Refresh()
end



SeasonTowerEnemyBuffPopupCtrl._OnBtnCloseClick = HL.Method() << function(self)
    self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close)
    if self.m_closeCb then
        self.m_closeCb()
        self.m_closeCb = nil
    end
end

HL.Commit(SeasonTowerEnemyBuffPopupCtrl)
