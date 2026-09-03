local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SSBacklogNode = HL.Class('SSBacklogNode', UIWidgetBase)


SSBacklogNode.m_backlogSortFunc = HL.Field(HL.Function)

SSBacklogNode.m_backlogData = HL.Field(HL.Table)

SSBacklogNode.m_nowBacklogData = HL.Field(HL.Table)

SSBacklogNode.m_backlogItems = HL.Field(HL.Table)

SSBacklogNode.m_genBacklogCells = HL.Field(HL.Userdata)

SSBacklogNode.m_isFocus = HL.Field(HL.Boolean) << false

SSBacklogNode.m_isInit = HL.Field(HL.Boolean) << false

SSBacklogNode.m_nowClickIndex = HL.Field(HL.Number) << 1



SSBacklogNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_backlogSortFunc = Utils.genSortFunction({ "sortId" })

    self.m_genBacklogCells = UIUtils.genCellCache(self.view.cell)

    self.view.selectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if isFocused then
            if #self.m_backlogItems > 0 then
                self:UpdateFocus()
            end
        end
        self.m_isFocus = isFocused
    end)
    self:RegisterMessage(MessageConst.ON_SPACESHIP_ONE_KEY_HARVEST_FINISH, function()
        if not self.m_isInit then
            return
        end
        self:RefreshData()
    end,true)

    self:RegisterMessage(MessageConst.ON_SPACESHIP_CLUE_INFO_CHANGE, function()
        self:RefreshData()
    end, true)
end

SSBacklogNode.InitSSBacklogNode = HL.Method() << function(self)
    self:_FirstTimeInit()
    if not self.m_isInit then
        InputManagerInst:ToggleBinding(self.view.selectableNaviGroup.FocusBindingId, false)
        local succ, roomInfo = GameInstance.player.spaceship:TryGetRoom(Tables.spaceshipConst.guestRoomClueExtensionId)
        if succ then

            GameInstance.player.spaceship:GetClueInfo()
        else
            self:RefreshData()
        end
    else
        self:RefreshData(true)
    end
end


SSBacklogNode.RefreshData = HL.Method(HL.Opt(HL.Boolean)) << function(self, noAnim)
    local spaceship = GameInstance.player.spaceship
    self.m_backlogData = self.m_backlogData or {}
    self.m_nowBacklogData = {}
    local needInit = #self.m_backlogData == 0

    for _, v in pairs(Tables.SpaceshipBacklogConfigDataTable) do
        local needShow = v.type and spaceship:GetBacklogCount(v.type) > 0
        if needInit then
            table.insert(self.m_backlogData, v)
        end
        table.insert(self.m_nowBacklogData,
            {
                needShow = needShow,
                sortId = v.sortId
            })
    end
    table.sort(self.m_backlogData, self.m_backlogSortFunc)
    table.sort(self.m_nowBacklogData, self.m_backlogSortFunc)
    self:UpdateView(noAnim)
    InputManagerInst:ToggleBinding(self.view.selectableNaviGroup.FocusBindingId, self:GetNeedShowState())
    if not self:GetNeedShowState() then
        self.view.selectableNaviGroup:ManuallyStopFocus()
    end
end

SSBacklogNode.UpdateView = HL.Method(HL.Opt(HL.Boolean)) << function(self, noAnim)
    if not self:GetNeedShowState() then
        if self.view.gameObject.activeSelf then
            self.view.animationWrapper:PlayOutAnimation(function()
                self.view.gameObject:SetActive(false)
            end)
        end
        return
    end
    self.view.gameObject:SetActive(true)
    if self.m_isInit then
        for i, v in ipairs(self.m_backlogItems) do
            self:UpdateCell(i, noAnim)
        end
        self:UpdateFocus()
        return
    end
    self.m_backlogItems = {}
    self.m_isInit = true


    self.m_genBacklogCells:Refresh(#self.m_backlogData, function(cell, luaIndex)
        self:InitCell(cell, luaIndex)
    end)
    self:UpdateFocus()
end
SSBacklogNode.GetNeedShowState = HL.Method().Return(HL.Boolean) << function(self)
    if GameInstance.player.spaceship.isViewingFriend or not self.m_nowBacklogData then
        return false
    end
    for i, v in ipairs(self.m_nowBacklogData) do
        if v.needShow then
            return true
        end
    end
    return false
end


SSBacklogNode.UpdateFocus = HL.Method() << function(self)
    if self.m_isFocus then
        local targetIndex = 1
        local startIndex = self.m_nowClickIndex
        if self.m_nowClickIndex == 1 then
            for index = startIndex, #self.m_nowBacklogData do
                if self.m_nowBacklogData[index] and self.m_nowBacklogData[index].needShow then
                    targetIndex = index
                    break
                end
            end
        else
            for index = startIndex, 1, -1 do
                if self.m_nowBacklogData[index] and self.m_nowBacklogData[index].needShow then
                    targetIndex = index
                    break
                end
            end
        end
        self:SetNaviTarget(self.m_backlogItems[targetIndex].backlogBtn)
    end
end


SSBacklogNode.InitCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local spaceship = GameInstance.player.spaceship
    local data = self.m_backlogData[index]
    self.m_backlogItems[index] = cell
    local needShow = data.type and spaceship:GetBacklogCount(data.type) > 0
    self.m_nowBacklogData[index].needShow = needShow
    cell.gameObject:SetActive(needShow)
    cell.titleTxt.text = data.title
    cell.backlogInfoTxt.text = data.subTitle
    cell.icon:LoadSprite(UIConst.UI_SPRITE_SS_COMMON, data.icon)
    cell.bgNum.color = UIUtils.getColorByString(data.color)
    cell.numTxt.text = spaceship:GetBacklogCount(data.type)
    cell.backlogBtn.onClick:RemoveAllListeners()
    cell.backlogBtn.onClick:AddListener(function()
        spaceship:OneKeyHarvest(data.type)
        self.m_nowClickIndex = index
    end)
end

SSBacklogNode.UpdateCell = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, index, noAnim)
    local data = self.m_backlogData[index]
    local nowData = self.m_nowBacklogData[index]

    local cell = self.m_backlogItems[index]
    local spaceship = GameInstance.player.spaceship
    if not data or not cell or not nowData then
        cell.gameObject:SetActive(false)
        return
    end
    cell.numTxt.text = spaceship:GetBacklogCount(data.type)
    local needShow = data.type and spaceship:GetBacklogCount(data.type) > 0
    nowData.needShow = needShow
    if cell.gameObject.activeSelf == needShow then
        return
    end
    if not needShow then
        if noAnim then
            cell.gameObject:SetActive(false)
        else
            cell.animationWrapper:PlayOutAnimation(function()
                cell.gameObject:SetActive(false)
            end)
        end
    else
        cell.animationWrapper:ClearTween()
        cell.gameObject:SetActive(true)
    end
end


SSBacklogNode.PlayOutAnimation = HL.Method() << function(self)
    self.view.animationWrapper:PlayOutAnimation()
end

HL.Commit(SSBacklogNode)
return SSBacklogNode

