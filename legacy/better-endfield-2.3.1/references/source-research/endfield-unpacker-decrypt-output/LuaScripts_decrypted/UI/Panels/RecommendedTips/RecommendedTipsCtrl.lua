
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RecommendedTips

local GROUP_NAME_TEXT_ID_FORMAT = "LUA_RECOMMENDED_TIPS_PANEL_GROUP_NAME_%s"

RecommendedTipsCtrl = HL.Class('RecommendedTipsCtrl', uiCtrl.UICtrl)

RecommendedTipsCtrl.m_arg = HL.Field(HL.Any)

RecommendedTipsCtrl.m_infos = HL.Field(HL.Table)

RecommendedTipsCtrl.m_groupCells = HL.Field(HL.Forward("UIListCache"))


RecommendedTipsCtrl.m_weaponCellsDict = HL.Field(HL.Table)

RecommendedTipsCtrl.m_haveInitTarget = HL.Field(HL.Boolean) << false


RecommendedTipsCtrl.m_curWeaponInfo = HL.Field(HL.Any)





RecommendedTipsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SHOW_WIKI_ENTRY] = '_OnShowWikiEntry',
    [MessageConst.CHAR_INFO_WEAPON_LIST_SELECT_ITEM] = '_OnWeaponListSelectItem',
}


RecommendedTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:ChangeCurPanelBlockSetting(DeviceInfo.usingController)
    self.m_arg = arg
    self:_InitData()
    self:_BindUI()
    self:_RefreshUI()
end

RecommendedTipsCtrl.OnShow = HL.Override() << function(self)
end

RecommendedTipsCtrl.OnClose = HL.Override() << function(self)
end

RecommendedTipsCtrl.OnHide = HL.Override() << function(self)
end




RecommendedTipsCtrl._InitData = HL.Method() << function(self)
    self.m_infos = {}
    local charId = self.m_arg.charId
    local curWeaponInstId = CharInfoUtils.getCharCurWeapon(self.m_arg.charInstId).weaponInstId
    self.m_curWeaponInfo = CharInfoUtils.getWeaponInstInfo(curWeaponInstId)


    local succ, cfg = Tables.CharWpnRecommendTable:TryGetValue(charId)
    if not succ then
        logger.error("养成武器推荐缺少配置：" .. charId)
        return
    end

    if cfg.weaponIds1.Count > 0 then
        table.insert(self.m_infos, {
            weaponIds = self:_ConvertListToTable(cfg.weaponIds1),
        })
    end
    if cfg.weaponIds2.Count > 0 then
        table.insert(self.m_infos, {
            weaponIds = self:_ConvertListToTable(cfg.weaponIds2),
        })
    end
    if cfg.weaponIds3.Count > 0 then
        table.insert(self.m_infos, {
            weaponIds = self:_ConvertListToTable(cfg.weaponIds3),
        })
    end
end

RecommendedTipsCtrl._ConvertListToTable = HL.Method(HL.Any).Return(HL.Table) << function(self, list)
    if list == nil then
        return {}
    end

    local ret = {}
    for _, id in pairs(list) do
        table.insert(ret, id)
    end
    return ret
end

RecommendedTipsCtrl._InitTestData = HL.Method() << function(self)
    self.m_infos = {}

    table.insert(self.m_infos, {
        groupName = "测试标题1",
        weaponIds = {
            "wpn_lance_0009",
            "wpn_funnel_0002",
        },
    })

    table.insert(self.m_infos, {
        groupName = "测试标题2",
        weaponIds = {
            "wpn_funnel_0001",
            "wpn_sword_0006",
        },
    })

    table.insert(self.m_infos, {
        groupName = "测试标题3",
        weaponIds = {
            "wpn_sword_0021",
            "wpn_funnel_0002",
        },
    })
end

RecommendedTipsCtrl._BindUI = HL.Method() << function(self)
    self.view.btnClose.onClick:AddListener(function()
        self:_TryClose()
    end)

    self.m_groupCells = UIUtils.genCellCache(self.view.weaponsListCell)
    self.m_weaponCellsDict = {}

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.autoCloseArea.onTriggerAutoClose:AddListener(function()
        self:_TryClose()
    end)
end

RecommendedTipsCtrl._RefreshUI = HL.Method() << function(self)
    self.view.titleText.text = Language.LUA_RECOMMENDED_TIPS_PANEL_TITLE

    self.m_groupCells:Refresh(#self.m_infos, function(cell, index)
        self:_SetupGroupCell(cell, index)
    end)

    if self.m_haveInitTarget == false and #self.m_infos > 0 then
        self.m_haveInitTarget = true
        self:_SetFirstNavi()
    end
end

RecommendedTipsCtrl._SetFirstNavi = HL.Method() << function(self)
    local firstWeaponCells = self.m_weaponCellsDict[1]
    if firstWeaponCells:GetCount() > 0 then
        local firstWeaponCell = firstWeaponCells:Get(1)
        self:SetNaviTarget(firstWeaponCell.inputBindingGroupNaviDecorator)
    end
end

RecommendedTipsCtrl._SetupGroupCell = HL.Method(HL.Any, HL.Number) << function(self, groupCell, groupIndex)
    local nameTextId = string.format(GROUP_NAME_TEXT_ID_FORMAT, groupIndex)
    groupCell.titleText.text = Language[nameTextId]

    local info = self.m_infos[groupIndex]

    local cells = self.m_weaponCellsDict[groupIndex]
    if cells == nil then
        cells = UIUtils.genCellCache(groupCell.recommendedWeaponCell)
        self.m_weaponCellsDict[groupIndex] = cells
    end
    cells:Refresh(#info.weaponIds, function(cell, index)
        local weaponId = info.weaponIds[index]
        self:_SetupWeaponCell(cell, weaponId)
    end)
end

RecommendedTipsCtrl._SetupWeaponCell = HL.Method(HL.Any, HL.String) << function(self, cell, weaponId)
    local item = cell.itemBigBlack
    local stateCtrl = cell.state
    local btn = cell.checkBtn
    local itemText = cell.weaponTxt
    local buttonText = cell.text

    if not DeviceInfo.usingController then
        InputManagerInst:ToggleGroup(cell.inputBindingGroupMonoTarget.groupId, true)
    end

    local _, itemCfg = Tables.itemTable:TryGetValue(weaponId)

    item:InitItem({ id = weaponId }, function()
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = weaponId,
            posType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            transform = item.transform,
            onBeforeJump = function()

                self:Close()
            end,
            isSideTips = false,
        })
    end)

    if itemCfg then
        itemText.text = itemCfg.name
    end

    local isEquip = self.m_curWeaponInfo.weaponTemplateId == weaponId
    local isInBag, bagInst = GameInstance.player.inventory:TryGetFirstWeaponInst(Utils.getCurrentScope(), weaponId)
    local isInShop, jumpToShopFunc = CashShopUtils.TryGetWeaponByWeaponId(weaponId)

    btn.onClick:RemoveAllListeners()
    if isEquip then
        stateCtrl:SetState("Worn")
        btn.onClick:AddListener(function()
            Notify(MessageConst.CHAR_INFO_WEAPON_SELECT_WEAPON, {
                instId = self.m_curWeaponInfo.weaponInstId,
                audioEventName = "Au_UI_Event_WeaponBuild",
            })
            EventLogManagerInst:GameEvent_WeaponRecClick(
                self.m_arg.charId,
                weaponId,
                self.m_curWeaponInfo.weaponInstId,
                3
            )
            self:_TryClose()
        end)
    elseif isInBag then
        stateCtrl:SetState("Owned")
        btn.onClick:AddListener(function()
            Notify(MessageConst.CHAR_INFO_WEAPON_SELECT_WEAPON, {
                id = weaponId,
                audioEventName = "Au_UI_Event_WeaponBuild",
            })
            EventLogManagerInst:GameEvent_WeaponRecClick(
                self.m_arg.charId,
                weaponId,
                self.m_curWeaponInfo.weaponInstId,
                1
            )
            self:_TryClose()
        end)
    elseif isInShop then
        stateCtrl:SetState("NotOwnedBtn")
        btn.onClick:AddListener(function()
            self:Close()
            jumpToShopFunc()
            EventLogManagerInst:GameEvent_WeaponRecClick(
                self.m_arg.charId,
                weaponId,
                self.m_curWeaponInfo.weaponInstId,
                2
            )
        end)
    else
        stateCtrl:SetState("NotOwnedActivity")
    end
end

RecommendedTipsCtrl._OnShowWikiEntry = HL.Method() << function(self)

    self:Close()
end

RecommendedTipsCtrl._OnWeaponListSelectItem = HL.Method(HL.Any) << function(self, arg)

    self:_TryClose()
end

RecommendedTipsCtrl._TryClose = HL.Method() << function(self)
    if not self:IsPlayingAnimationOut() then
        self:PlayAnimationOut()
    end
end

HL.Commit(RecommendedTipsCtrl)
