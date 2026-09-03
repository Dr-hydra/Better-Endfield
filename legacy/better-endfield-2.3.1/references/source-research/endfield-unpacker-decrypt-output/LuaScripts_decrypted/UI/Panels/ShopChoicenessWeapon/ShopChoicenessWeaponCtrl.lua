
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopChoicenessWeapon

ShopChoicenessWeaponCtrl = HL.Class('ShopChoicenessWeaponCtrl', uiCtrl.UICtrl)






ShopChoicenessWeaponCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ShopChoicenessWeaponCtrl.m_boxData = HL.Field(HL.Any)

ShopChoicenessWeaponCtrl.m_info = HL.Field(HL.Table)




ShopChoicenessWeaponCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_info = arg
    local _, box, goods = GameInstance.player.shopSystem:GetNowUpWeaponData()
    if box == nil or box.Count == 0 then
        logger.error("[商城武器推荐页] 武器box数据缺失")
        return
    end
    if #self.m_info.cashGoodsIds <= 0 then
        logger.error("[商城武器推荐页] 武器推荐页对应goodsId数据缺失！请检查配置表中【cashGoodsIdList】字段")
        return
    end
    local goodsId = self.m_info.cashGoodsIds[1]
    for _, singleBox in pairs(box) do
        if singleBox.goodsTemplateId == goodsId then
            self.m_boxData = singleBox
            break
        end
    end
    if not self.m_boxData then
        logger.error("[商城武器推荐页] 武器推荐页对应goodsData数据缺失！GoodsId为：" .. goodsId)
        return
    end

    self:_InitUI()
    self:_RefreshAllUI()
    self.view.cashShopItemTag:InitCashShopItemTag({
        isShop = true,
        goodsData = self.m_boxData,
        hideRemainCount = true,
        hideNew = true,
    })
end

ShopChoicenessWeaponCtrl.OnShow = HL.Override() << function(self)
    local goodsId = self.m_info.cashGoodsIds[1]
    GameInstance.player.shopSystem:RecordSeeGoodsId(goodsId)
    GameInstance.player.shopSystem:SetGoodsIdSee()
end




ShopChoicenessWeaponCtrl._InitUI = HL.Method() << function(self)
    self.view.gotoBtn.onClick:AddListener(function()
        self.m_phase:OpenWeaponCategoryAndOpenDetailPanel(self.m_boxData, self.m_info.id)
    end)
end

ShopChoicenessWeaponCtrl._RefreshAllUI = HL.Method() << function(self)

    local csGachaSys = GameInstance.player.gacha
    local _, box, goods = GameInstance.player.shopSystem:GetNowUpWeaponData()

    local boxData = self.m_boxData
    local goodsCfg = Tables.shopGoodsTable[boxData.goodsTemplateId]
    local poolId = goodsCfg.weaponGachaPoolId

    local _, poolInfo = csGachaSys.poolInfos:TryGetValue(poolId)


    local poolTimeNode = self.view.poolTimeNode
    local isRealTime, closeTimeDesc = CashShopUtils.GetGachaWeaponPoolCloseTimeShowDesc(poolId)
    poolTimeNode.endTimeTxt.text = closeTimeDesc
    poolTimeNode.endingTxt.gameObject:SetActive(isRealTime)


    local weaponPoolCfg = Tables.gachaWeaponPoolTable[poolId]
    local gachaTypeCfg = Tables.gachaWeaponPoolTypeTable[weaponPoolCfg.type]
    self.view.poolNameTxt.text = weaponPoolCfg.name
    local view = self.view
    local uiPrefabName = weaponPoolCfg.poolNodeUIPrefab
    if view.uiPrefabName ~= uiPrefabName then
        if view.node then
            GameObject.Destroy(view.node)
        end
        local path = string.format("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/WeaponPoolNode/%s.prefab", uiPrefabName)
        local prefab = self.loader:LoadGameObject(path)
        local obj = CSUtils.CreateObject(prefab, view.weaponPoolNodeRoot)

        obj.name = weaponPoolCfg.id
        view.uiPrefabName = uiPrefabName
        view.node = obj
    end


    local showHardGuarantee = poolInfo.upGotCount <= 0
    local guaranteeNode = self.view.guaranteeNode
    if showHardGuarantee then
        local upWeaponId = weaponPoolCfg.upWeaponIds[0]
        local weaponItemCfg = Tables.itemTable[upWeaponId]
        local weaponCfg = Tables.weaponBasicTable[upWeaponId]
        local weaponTypeIconName = UIConst.WEAPON_EXHIBIT_WEAPON_TYPE_ICON_PREFIX .. weaponCfg.weaponType:ToInt()
        guaranteeNode.stateController:SetState("HardGuarantee")
        guaranteeNode.itemIcon:InitItemIcon(upWeaponId)
        guaranteeNode.rewardNameTxt.text = weaponItemCfg.name
        guaranteeNode.weaponTypeIcon:LoadSprite(UIConst.UI_SPRITE_WEAPON_EXHIBIT, weaponTypeIconName)
        local remainNum = math.ceil((gachaTypeCfg.hardGuarantee - poolInfo.hardGuaranteeProgress) / 10)
        guaranteeNode.guaranteeTxt.text = string.format("<color=#FEF000>%d</color> ", remainNum) .. Language.ui_shop_entry_weapon_shop_numbers_1
        guaranteeNode.btn.onClick:RemoveAllListeners()
        guaranteeNode.btn.onClick:AddListener(function()
            CashShopUtils.ShowWikiWeaponPreview(poolId, upWeaponId)
        end)
    else

        local loopRewardInfos = CashShopUtils.GetGachaWeaponLoopRewardInfo(poolId)
        if not loopRewardInfos then
            return
        end
        table.sort(loopRewardInfos, function(a, b)
            return a.remainNeedPullCount < b.remainNeedPullCount
        end)

        local info = loopRewardInfos[1]
        guaranteeNode.itemIcon:InitItemIcon(info.itemId)
        guaranteeNode.rewardNameTxt.text = info.name
        guaranteeNode.stateController:SetState("LoopReward")
        guaranteeNode.guaranteeTxt.text = string.format("%d ", info.remainNeedPullCount) .. Language.ui_shop_entry_weapon_shop_numbers_2
        guaranteeNode.btn.onClick:RemoveAllListeners()
        guaranteeNode.btn.onClick:AddListener(function()

            logger.info("武器卡池，up武器预览或宝箱预览")
            if info.isWeaponItemCase then
                UIManager:Open(PanelId.BattlePassWeaponCase, { itemId = info.itemId, isPreview = true })
            else
                CashShopUtils.ShowWikiWeaponPreview(poolId, info.itemId)
            end
        end)
    end
end





HL.Commit(ShopChoicenessWeaponCtrl)
