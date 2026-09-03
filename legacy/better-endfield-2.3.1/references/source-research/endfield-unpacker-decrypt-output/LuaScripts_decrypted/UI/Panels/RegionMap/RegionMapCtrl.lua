local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local MapSpaceshipNode = require_ex('UI/Widgets/MapSpaceshipNode')
local PANEL_ID = PanelId.RegionMap
RegionMapCtrl = HL.Class('RegionMapCtrl', uiCtrl.UICtrl)

local MINI_POWER_HOVER_TEXT_ID = "ui_mappanel_collection_electricity"

local RED_DOT_DOMAIN_ID = "domain_2"
local MAX_DOMAIN_COUNT = 2







RegionMapCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CLICK_REGIONMAP_LOCK] = '_OnClickLevelBtn',

    [MessageConst.SWITCH_TO_LEVEL_MAP] = '_SwitchToLevelMap',
    [MessageConst.ON_SYSTEM_UNLOCK_CHANGED] = '_OnSystemUnlock',
}

RegionMapCtrl.m_mapManager = HL.Field(HL.Userdata)



RegionMapCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    local args = arg
    self.domainId = args.domainId

    self.m_mapManager = GameInstance.player.mapManager

    self.view.btnClose.onClick:AddListener(function()

        AudioAdapter.PostEvent("Au_UI_Menu_RegionMapPanel_Close")
        MapSpaceshipNode.ClearStaticFromData()
        self:Notify(MessageConst.ON_COMMON_BACK_CLICKED)
    end)


    self.view.walletBarPlaceholder:InitWalletBarPlaceholder(UIConst.REGION_MAP_STAMINA_IDS)
    self:_RefreshWalletNodeVisibleState()

    self:_InitDomainSwitchCells()
    self:_InitMapRemindTip()
    self:_InitRegionMapController()
end





RegionMapCtrl._OnClickLevelBtn = HL.Method() << function(self)
    self:ChangePanelCfg("gyroscopeEffect", Types.EPanelGyroscopeEffect.Disable)
    self:PlayAnimationOutWithCallback()
end


RegionMapCtrl._SwitchToLevelMap = HL.Method(HL.Table) << function(self, args)
    local levelId, insId = unpack(args)
    MapUtils.switchFromRegionMapToLevelMap(insId, levelId)
end





RegionMapCtrl.domainId = HL.Field(HL.String) << ""

RegionMapCtrl.SwitchDomain = HL.Method(HL.String) << function(self, domainId)
    local lastDomainId = self.domainId
    self.domainId = domainId
    self:_RefreshBasicInfo()
    Notify(MessageConst.SWITCH_DOMAIN_MAP, { domainId = domainId, lastDomainId = lastDomainId })
end





RegionMapCtrl.m_domainDataList = HL.Field(HL.Table)

RegionMapCtrl.m_trackIconCellCache = HL.Field(HL.Table)

RegionMapCtrl.m_selectIndex = HL.Field(HL.Number) << -1

RegionMapCtrl.m_switchBtnCells = HL.Field(HL.Forward("UIListCache"))

RegionMapCtrl.m_secondDomainFirstLevelId = HL.Field(HL.String) << ""



RegionMapCtrl._InitDomainSwitchCells = HL.Method() << function(self)
    self.m_domainDataList = {}
    local secondDomainFirstLevelId = ""
    local i, selectedIndex = 0, 0
    for _, domainData in pairs(Tables.domainDataTable) do
        local isDomainUnlocked = false
        for _, levelId in pairs(domainData.levelGroup) do
            if self.m_mapManager:IsLevelUnlocked(levelId) then
                isDomainUnlocked = true
                break
            end
        end
        if isDomainUnlocked then
            i = i + 1
            table.insert(self.m_domainDataList, domainData)
            if domainData.domainId == self.domainId then
                selectedIndex = i
            end
            if domainData.domainId == RED_DOT_DOMAIN_ID then
                secondDomainFirstLevelId = domainData.levelGroup[0]
            end
        end
    end

    self.m_switchBtnCells = UIUtils.genCellCache(self.view.domainBtnCell)
    self.m_switchBtnCells:Refresh(MAX_DOMAIN_COUNT, function(cell, index)
        if index > #self.m_domainDataList then
            cell.stateController:SetState("Lock")
            cell.button.enabled = false
        else
            local domainCfg = self.m_domainDataList[index]
            cell.domainTxt.text = domainCfg.domainName
            cell.iconImg:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT, domainCfg.domainIcon)
            local isCurrDomain = self.domainId == domainCfg.domainId
            cell.stateController:SetState(isCurrDomain and "Select" or "Unselect")
            if isCurrDomain then
                self:SwitchDomain(domainCfg.domainId)
                if domainCfg.domainId == RED_DOT_DOMAIN_ID then
                    GameInstance.player.mapManager:SendLevelReadMessage(secondDomainFirstLevelId)
                end
                self.m_selectIndex = index
            end

            cell.button.onClick:AddListener(function()
                self:_OnDomainSwitchCellClick(index)
            end)
            cell.button.enabled = true
        end
    end)

    if not string.isEmpty(self.m_secondDomainFirstLevelId) then
        self.view.domainSelectionRedDot:InitRedDot("MapUnreadLevel", secondDomainFirstLevelId)
        self.view.domainSelectionRedDot.gameObject:SetActive(true)
    else
        self.view.domainSelectionRedDot.gameObject:SetActive(false)
    end
    self.m_secondDomainFirstLevelId = secondDomainFirstLevelId



































































end

RegionMapCtrl._OnDomainSwitchCellClick = HL.Method(HL.Number) << function(self, index)
    if index == self.m_selectIndex or index > #self.m_domainDataList or index <= 0 then
        return
    end

    local domainData = self.m_domainDataList[index]
    local lastCell = self.m_switchBtnCells:GetItem(self.m_selectIndex)
    if lastCell then
        lastCell.stateController:SetState("Unselect")
    end
    local cell = self.m_switchBtnCells:GetItem(index)
    cell.stateController:SetState("Select")
    self:SwitchDomain(domainData.domainId)
    self.m_selectIndex = index

    if domainData.domainId == RED_DOT_DOMAIN_ID then
        GameInstance.player.mapManager:SendLevelReadMessage(self.m_secondDomainFirstLevelId)
    end
end



RegionMapCtrl._InitMapRemindTip = HL.Method() << function(self)

















end

RegionMapCtrl._RefreshBasicInfo = HL.Method() << function(self)
    local hasValue, _



    local domainData
    hasValue, domainData = Tables.domainDataTable:TryGetValue(self.domainId)
    local domainName = hasValue and domainData.domainName or ""
    self.view.txtTitle.text = domainName


    local prosperity = 0
    prosperity, _ = GameInstance.player.domainDevelopmentSystem:GetDomainDevelopmentLv(self.domainId)
    self.view.txtGrade.text = tostring(prosperity)


    local chapterId = ScopeUtil.ChapterIdStr2Int(self.domainId)
    self.view.facMiniPowerContent:InitFacMiniPowerContent(chapterId)
    self.view.facMiniPowerContent.view.hoverButton.onHoverChange:RemoveAllListeners()
    self.view.facMiniPowerContent.view.hoverButton.onHoverChange:AddListener(function(isHover)
        if isHover then
            Notify(MessageConst.SHOW_COMMON_HOVER_TIP, {
                mainText = Language[MINI_POWER_HOVER_TEXT_ID],
                delay = self.view.config.MINI_POWER_HOVER_DELAY,
            })
        else
            Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
        end
    end)


    self.view.mapSpaceshipNode:InitMapSpaceshipNode({ domainId = self.domainId })


    self.view.mapTrackingInfo:InitMapTrackingInfo({ domainId = self.domainId})
end

RegionMapCtrl._OnClickRegionMapLevelBtn = HL.Method(HL.Userdata) << function(self, markData)
    if markData == nil then
        return
    end

    if markData.levelId == nil or markData.insId == nil then
        return
    end

    local data = {}
    data.levelId = markData.levelId
    data.insId = markData.insId

    Notify(MessageConst.ON_CLICK_REGIONMAP_LEVEL_BTN, data)
end

RegionMapCtrl._OnSystemUnlock = HL.Method(HL.Table) << function(self, args)
    local systemIndex = unpack(args)
    if systemIndex == GEnums.UnlockSystemType.Dungeon:GetHashCode() then
        self:_RefreshWalletNodeVisibleState()
    end
end

RegionMapCtrl._RefreshWalletNodeVisibleState = HL.Method() << function(self)
    self.view.walletBarPlaceholder.gameObject:SetActive(Utils.isSystemUnlocked(GEnums.UnlockSystemType.Dungeon))
end




RegionMapCtrl._InitRegionMapController = HL.Method() << function(self)
    local prevBindingId = self:BindInputPlayerAction("map_region_switch_domain_prev", function()
        self:_OnControllerSwitchDomain(false)
    end)
    local nextBindingId = self:BindInputPlayerAction("map_region_switch_domain_next", function()
        self:_OnControllerSwitchDomain(true)
    end)
    if #self.m_domainDataList <= 1 then
        InputManagerInst:ToggleBinding(prevBindingId, false)
        InputManagerInst:ToggleBinding(nextBindingId, false)
    end
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

RegionMapCtrl._OnControllerSwitchDomain = HL.Method(HL.Boolean) << function(self, next)
    local count = #self.m_domainDataList
    if count <= 1 then
        return
    end
    local index = next and self.m_selectIndex - 1 or self.m_selectIndex + 1
    if index < 1 then
        index = count
    elseif index > count then
        index = 1
    end
    if index == self.m_selectIndex then
        return
    end
    self:_OnDomainSwitchCellClick(index)
    AudioAdapter.PostEvent("Au_UI_Toggle_Common_On")
end



HL.Commit(RegionMapCtrl)
