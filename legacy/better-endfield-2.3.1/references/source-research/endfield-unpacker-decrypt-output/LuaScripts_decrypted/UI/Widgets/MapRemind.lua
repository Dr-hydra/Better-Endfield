local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MapRemind = HL.Class('MapRemind', UIWidgetBase)

MapRemind.m_tabInfos = HL.Field(HL.Table)

MapRemind.m_mapRemindInfos = HL.Field(HL.Table)

MapRemind.m_curTabIndex = HL.Field(HL.Number) << -1

MapRemind.m_onClose = HL.Field(HL.Function)

MapRemind.m_genTabCells = HL.Field(HL.Forward("UIListCache"))

MapRemind.m_getCell = HL.Field(HL.Function)

MapRemind.m_levelId = HL.Field(HL.String) << ""


MapRemind._OnFirstTimeInit = HL.Override() << function(self)
    self.view.closeBtn.onClick:RemoveAllListeners()
    self.view.closeBtn.onClick:AddListener(function()
        self.m_onClose()
    end)

    self.view.closeBGButton.onClick:RemoveAllListeners()
    self.view.closeBGButton.onClick:AddListener(function()
        self.m_onClose()
    end)

    self.view.closeBG.onTriggerAutoClose:RemoveAllListeners()
    self.view.closeBG.onTriggerAutoClose:AddListener(function()
        self.m_onClose()
        AudioAdapter.PostEvent("Au_UI_Popup_Common_Small_Close")
    end)

    self.m_genTabCells = UIUtils.genCellCache(self.view.tabs.tabCell)
    self.m_getCell = UIUtils.genCachedCellFunction(self.view.infoScrollList)

    self.view.infoScrollList.onUpdateCell:RemoveAllListeners()
    self.view.infoScrollList.onUpdateCell:AddListener(function(object, index)
        self:_OnUpdateCell(object, LuaIndex(index))
    end)

    self.m_tabInfos = {
        {
            tabType = GEnums.MapRemindTabType.ImportantMatters,
            redDot = "MapImportantMatters",
            text = Language["ui_map_important_reminder_important"]
        },
        {
            tabType = GEnums.MapRemindTabType.CollectionTips,
            redDot = "MapCollectionTips",
            text = Language["ui_map_important_reminder_collection"]
        },
    }
end

MapRemind._OnUpdateCell = HL.Method(HL.Userdata, HL.Number, HL.Opt(HL.Function)) << function(self, object, index)
    local tabType = GEnums.MapRemindTabType.__CastFrom(self.m_curTabIndex - 1)
    local info = self.m_mapRemindInfos[tabType][index]
    local cell = self:_GetCellByIndex(index)
    cell.gameObject.name = "MapRemindInfo_" .. index
    local cfg = Tables.mapRemindTable:GetValue(info.key)
    local instId = info.value.insId

    local success, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(instId)

    local desc = cfg.desc
    if success then
        local cfgSuccess, mapMarkTempTableCfg = Tables.mapMarkTempTable:TryGetValue(markRuntimeData.templateId)
        if cfgSuccess then
            desc = string.format(cfg.desc, mapMarkTempTableCfg.name)
        end
    end

    cell.text.text = desc
    local icon = cfg.icon
    if info.value.useMarkIcon then
        local succ, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(instId)
        if succ then
            local tempSucc, templateData = Tables.mapMarkTempTable:TryGetValue(markRuntimeData.templateId)
            if tempSucc then
                icon = templateData.activeIcon
            end
        end
    end

    cell.mattersIconImg:LoadSprite(UIConst.UI_SPRITE_MAP_MARK_ICON_SMALL, icon)
    cell.btn.onClick:RemoveAllListeners()
    cell.btn.onClick:AddListener(function()
        if info.value.onClick then
            info.value.onClick(self.m_levelId)
        else
            MapUtils.openMap(instId)
        end
        self.m_onClose()
    end)
    cell.btn.customBindingViewLabelText = Language.LUA_MAP_REMIND_CELL_CONFIRM

    if info.value.state then
        cell.animRoot:SetState(info.value.state)
    else
        cell.animRoot:SetState("Normal")
    end
end

MapRemind._GetCellByIndex = HL.Method(HL.Number).Return(HL.Any) << function(self, cellIndex)
    local go = self.view.infoScrollList:Get(CSIndex(cellIndex))
    local cell = nil
    if go then
        cell = self.m_getCell(go)
    end

    return cell
end

MapRemind.InitMapRemind = HL.Method(HL.String, HL.Table, HL.Function, HL.Opt(HL.Number)) << function(self, levelId, infos, onClose, initIndex)
    self:_FirstTimeInit()
    self.m_curTabIndex = -1
    self.m_levelId = levelId
    self.m_onClose = onClose
    self.m_mapRemindInfos = {
        [GEnums.MapRemindTabType.ImportantMatters] = {},
        [GEnums.MapRemindTabType.CollectionTips] = {},
    }
    for k, v in pairs(infos) do

        local cfg = Tables.mapRemindTable:GetValue(k)
        for _, insId in pairs(v.insIdList) do
            if MapUtils.isMarkVisible(insId) or k == GEnums.MapRemindType.InvalidBuilding then
                table.insert(self.m_mapRemindInfos[cfg.tabType], { key = k, value =
                {
                    insId = insId,
                    redDotName = v.redDotName,
                    useMarkIcon = v.useMarkIcon,
                    state = v.state,
                    onClick = v.onClick,
                }})
            end
        end
    end

    for tabType, infoList in pairs(self.m_mapRemindInfos) do
        table.sort(infoList, function(a, b)
            local cfgA = Tables.mapRemindTable:GetValue(a.key)
            local cfgB = Tables.mapRemindTable:GetValue(b.key)
            if cfgA.sort ~= cfgB.sort then
                return cfgA.sort < cfgB.sort
            end
            return a.value.insId < b.value.insId
        end)
    end

    local index = 1
    if initIndex then
        index = initIndex
    else


        if #self.m_mapRemindInfos[GEnums.MapRemindTabType.ImportantMatters] == 0 and #self.m_mapRemindInfos[GEnums.MapRemindTabType.CollectionTips] ~= 0 then
            index = 2
        end
    end
    self:_InitTabInfos(index)
end

MapRemind.UpdateMapRemindInfo = HL.Method() << function(self)


    local tabType = GEnums.MapRemindTabType.__CastFrom(self.m_curTabIndex - 1)
    self.view.emptyNode.gameObject:SetActiveIfNecessary(#self.m_mapRemindInfos[tabType] == 0)
    self.view.infoScrollList:UpdateCount(#self.m_mapRemindInfos[tabType], true)
    local cell = self:_GetCellByIndex(1)
    if cell == nil then
        return
    end
    self:SetNaviTarget(cell.btn)
end

MapRemind.GetCurTabIndex = HL.Method().Return(HL.Number) << function(self)
   return self.m_curTabIndex
end

MapRemind._InitTabInfos = HL.Method(HL.Number) << function(self, index)
    self.m_genTabCells:Refresh(#self.m_tabInfos, function(cell, luaIndex)
        local info = self.m_tabInfos[luaIndex]
        cell.gameObject.name = "MapRemindTab_" .. luaIndex
        cell.mattersText.text = info.text
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            cell.stateController:SetState(isOn and "on" or "off")
            if isOn then
                self:_OnTabClick(luaIndex)
            end
        end)
        if luaIndex == index then
            if cell.toggle.isOn then
                cell.toggle.isOn = false
            end
            cell.toggle.isOn = true
        end
    end)
end

MapRemind._OnTabClick = HL.Method(HL.Number) << function(self, luaIndex)
    if self.m_curTabIndex == luaIndex then
        return
    end
    self.m_curTabIndex = luaIndex
    self:UpdateMapRemindInfo()
end

HL.Commit(MapRemind)
return MapRemind

