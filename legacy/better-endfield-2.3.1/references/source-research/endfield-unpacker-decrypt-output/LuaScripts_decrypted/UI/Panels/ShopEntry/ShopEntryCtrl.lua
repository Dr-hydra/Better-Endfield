
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopEntry
local PHASE_ID = PhaseId.ShopEntry
ShopEntryCtrl = HL.Class('ShopEntryCtrl', uiCtrl.UICtrl)


ShopEntryCtrl.m_top = HL.Field(HL.Any)

ShopEntryCtrl.m_recommendList = HL.Field(HL.Table)

ShopEntryCtrl.m_yellowPanel = HL.Field(HL.Any)

ShopEntryCtrl.m_greenPanel = HL.Field(HL.Any)
ShopEntryCtrl.m_cor = HL.Field(HL.Any)

ShopEntryCtrl.m_arg = HL.Field(HL.Any)




ShopEntryCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ShopEntryCtrl.m_getCellFunc = HL.Field(HL.Function)


ShopEntryCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local t = {ctrl = self, view = {main = self.view.main, gameObject = self.view.gameObject}}
    local topNode = UIManager:Open(PanelId.ShopTopNode, t)
    self.m_top = topNode.view.shopTopNode
    self:Init()
    self.m_top:RefreshTab()

    if arg and not string.isEmpty(arg) then
        self:ProcessJumpArg(arg)
        self.m_arg = arg
    end

    UIManager:ToggleBlockObtainWaysJump("shop_entry", true)
    self.view.main.onScrollEnd:AddListener(function()
        self:CheckTab()
    end)
end

ShopEntryCtrl.ProcessJumpArg = HL.Method(HL.Any) << function(self, arg)
    local tab = arg.tab
    local goods = arg.goods
    local sourceId = arg.exchangeSourceMoney
    local targetId = arg.exchangeTargetMoney
    self.m_top.m_tabIndex = 0
    if tab == "entry" then
        if sourceId and targetId then
            PhaseManager:OpenPhase(PhaseId.CommonMoneyExchange, {sourceId = arg.sourceId, targetId = arg.targetId})
        end
    elseif tab == "weapon" then
        self.m_top.m_tabClickFuncs[2]()
        self.m_top:RefreshTab()
    elseif tab == "yellow" then
        self.m_top.m_tabClickFuncs[3]()
        self.m_top:RefreshTab()
    elseif tab == "green" then
        self.m_top.m_tabClickFuncs[4]()
        self.m_top:RefreshTab()
    end
    Notify(MessageConst.ON_SHOP_JUMP_EVENT,{goods = goods, source = sourceId, targetId = targetId})
end

ShopEntryCtrl.CheckTab = HL.Method() << function(self)
    local index = self.view.main.centerIndex
    local data = self.m_recommendList[LuaIndex(index)]
    if data.recommonendType == CS.Beyond.GEnums.GachaEntrRecommonendType.BeginnerPool then
        self.m_top:SwitchTabBlackWhite(false)
    else
        self.m_top:SwitchTabBlackWhite(true)
    end
end

ShopEntryCtrl.Init = HL.Method() << function(self)

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.main)
    self.m_recommendList = {}
    if self:CheckHaveBeginPool() then
        table.insert(self.m_recommendList, {recommonendType = CS.Beyond.GEnums.GachaEntrRecommonendType.BeginnerPool})
    end

    local nowPool = self:GetPoolData(function(csInfo)
        return csInfo.isChar and csInfo.type ~= CS.Beyond.GEnums.CharacterGachaPoolType.Beginner
    end)
    if nowPool then
        table.insert(self.m_recommendList, {recommonendType = CS.Beyond.GEnums.GachaEntrRecommonendType.UpPool})
    end
    nowPool = self:GetPoolData(function(csInfo)
        return not csInfo.isChar and csInfo.openTime ~= 0
    end)
    if nowPool then
        table.insert(self.m_recommendList, {recommonendType = CS.Beyond.GEnums.GachaEntrRecommonendType.UpWeaponPool})
    end
    table.insert(self.m_recommendList, {recommonendType = CS.Beyond.GEnums.GachaEntrRecommonendType.WeaponShop})
    self.view.main.onCenterIndexChanged:AddListener(function(old, new)
        local oldCell = self.m_getCellFunc(LuaIndex(old))
        local newCell = self.m_getCellFunc(LuaIndex(new))
        if oldCell then
            self:PlayOutAnimation(oldCell)
        end
        if newCell then
            self:PlayInAnimation(newCell)
        end
    end)

    local initPanelByType = {
        [CS.Beyond.GEnums.GachaEntrRecommonendType.BeginnerPool] = self.InitBeginPool,
        [CS.Beyond.GEnums.GachaEntrRecommonendType.UpPool] = self.InitUpPool,
        [CS.Beyond.GEnums.GachaEntrRecommonendType.UpWeaponPool] = self.InitUpWeaponPool,
        [CS.Beyond.GEnums.GachaEntrRecommonendType.WeaponShop] = self.InitWeaponDealPool,
    }

    self.view.main.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getCellFunc(obj)
        local data = self.m_recommendList[LuaIndex(csIndex)]
        local initFunc = initPanelByType[data.recommonendType]
        initFunc(self, cell)
    end)
    self.view.main:UpdateCount(#self.m_recommendList)











end

ShopEntryCtrl.PlayInAnimation = HL.Method(HL.Any) << function(self, cell)
    cell.rolePoolNode.animationWrapper:Play("shopentry_rolepool_in")
    cell.limitPoolNode.animationWrapper:Play("shopentry_limitpool_in")
    cell.limitWeaponNode.animationWrapper:Play("shopentry_limitweapon_in")
    cell.weaponDealNode.animationWrapper:Play("shopentry_weapondeal_in")
end

ShopEntryCtrl.PlayOutAnimation = HL.Method(HL.Any) << function(self, cell)
    cell.rolePoolNode.animationWrapper:Play("shopentry_rolepool_out")
    cell.limitPoolNode.animationWrapper:Play("shopentry_limitpool_out")
    cell.limitWeaponNode.animationWrapper:Play("shopentry_limitweapon_out")
    cell.weaponDealNode.animationWrapper:Play("shopentry_weapondeal_out")
end


ShopEntryCtrl.CheckHaveBeginPool = HL.Method().Return(HL.Boolean) << function(self)
    local csGacha = GameInstance.player.gacha
    for id, csInfo in pairs(csGacha.poolInfos) do
        if csInfo.isChar and not csInfo.isClosed and csInfo.type == CS.Beyond.GEnums.CharacterGachaPoolType.Beginner then
            return true
        end
    end
    return false
end

ShopEntryCtrl.InitBeginPool = HL.Method(HL.Any) << function(self, go)
    go.rolePoolNode.gameObject:SetActive(true)
    go.limitPoolNode.gameObject:SetActive(false)
    go.limitWeaponNode.gameObject:SetActive(false)
    go.weaponDealNode.gameObject:SetActive(false)

    local panel = go.rolePoolNode
    local csGacha = GameInstance.player.gacha
    for id, csInfo in pairs(csGacha.poolInfos) do
        if csInfo.isChar and csInfo.isOpened and not csInfo.isClosed and csInfo.type == CS.Beyond.GEnums.CharacterGachaPoolType.Beginner then
            local poolId = csInfo.id
            local poolData = Tables.gachaCharPoolTable[poolId]
            panel.starterTitleText.text = poolData.name
            panel.timeTitleText.text = string.format("%s ~ %s", Utils.timestampToDateYMDHM(csInfo.openTime), Utils.timestampToDateYMDHM(csInfo.closeTime))
            panel.descText.text = poolData.desc
            panel.timeTitleText.gameObject:SetActiveIfNecessary(false)
            panel.beginPoolBtnJumpTo.onClick:RemoveAllListeners()
            panel.beginPoolBtnJumpTo.onClick:AddListener(function()
                PhaseManager:GoToPhase(PhaseId.GachaPool)
            end)
        end
    end

    local indexToId = {
        [1] = 1,
        [2] = 2,
        [3] = 3,
    }
    if not self.m_cor then

        local curIndex = 1
        local time = 0
        for j = 1, 3 do
            go.rolePoolNode["showName0" .. j].gameObject:SetActive(j == 1)
        end
        go.rolePoolNode["decoLine".. 2].color = self.view.config["PageLineColor" .. indexToId[2]]
        go.rolePoolNode["decoLine".. 3].color = self.view.config["PageLineColor" .. indexToId[3]]
        go.rolePoolNode["roleText0".. 1].text = indexToId[1]
        go.rolePoolNode["roleText0".. 2].text = indexToId[2]
        go.rolePoolNode["roleText0".. 3].text = indexToId[3]
        local func = function(index)
            local id = indexToId[index]
            for j = 1, 3 do
                go.rolePoolNode["showName0" .. j].gameObject:SetActive(j == id)
            end
            go.rolePoolNode.pageLine.color = self.view.config["PageLineColor" .. id]
            go.rolePoolNode.roleImage1:LoadSprite(UIConst.UI_SPRITE_SHOP_ROLE_IMAGE, self.view.config.RoleBigImage .. id)
            if id == 1 then
                go.rolePoolNode.roleImage2:LoadSprite(UIConst.UI_SPRITE_SHOP_ROLE_IMAGE, self.view.config.RoleBigImage .. 2)
                go.rolePoolNode.roleImage3:LoadSprite(UIConst.UI_SPRITE_SHOP_ROLE_IMAGE, self.view.config.RoleBigImage .. 3)
                indexToId[1] = 1
                indexToId[2] = 2
                indexToId[3] = 3
            end

            if id == 2 then
                go.rolePoolNode.roleImage2:LoadSprite(UIConst.UI_SPRITE_SHOP_ROLE_IMAGE, self.view.config.RoleBigImage .. 3)
                go.rolePoolNode.roleImage3:LoadSprite(UIConst.UI_SPRITE_SHOP_ROLE_IMAGE, self.view.config.RoleBigImage .. 1)
                indexToId[1] = 2
                indexToId[2] = 3
                indexToId[3] = 1
            end

            if id == 3 then
                go.rolePoolNode.roleImage2:LoadSprite(UIConst.UI_SPRITE_SHOP_ROLE_IMAGE, self.view.config.RoleBigImage .. 1)
                go.rolePoolNode.roleImage3:LoadSprite(UIConst.UI_SPRITE_SHOP_ROLE_IMAGE, self.view.config.RoleBigImage .. 2)
                indexToId[1] = 3
                indexToId[2] = 1
                indexToId[3] = 2
            end
            go.rolePoolNode.animationWrapper:Play("shopentry_rolepool_change")
            go.rolePoolNode["decoLine".. 2].color = self.view.config["PageLineColor" .. indexToId[2]]
            go.rolePoolNode["decoLine".. 3].color = self.view.config["PageLineColor" .. indexToId[3]]
            go.rolePoolNode["roleText0".. 1].text = indexToId[1]
            go.rolePoolNode["roleText0".. 2].text = indexToId[2]
            go.rolePoolNode["roleText0".. 3].text = indexToId[3]
        end

        for i = 2, 3 do
            local button = go.rolePoolNode["role0" .. i]
            button.onClick:RemoveAllListeners()
            local index = i
            button.onClick:AddListener(function()
                func(index)
                time = 0
            end)
        end

        self.m_cor = self:_StartCoroutine(function()
            while true do
                coroutine.wait(1)
                time = time + 1
                if time >= 5 then
                    time = 0
                    func(2)
                end
            end
        end)
    end

end


ShopEntryCtrl.InitUpPool = HL.Method(HL.Any) << function(self, go)
    go.rolePoolNode.gameObject:SetActive(false)
    go.limitPoolNode.gameObject:SetActive(true)
    go.limitWeaponNode.gameObject:SetActive(false)
    go.weaponDealNode.gameObject:SetActive(false)


    local nowPool ,index = self:GetPoolData(function(csInfo)
        return csInfo.isChar and csInfo.type ~= CS.Beyond.GEnums.CharacterGachaPoolType.Beginner
    end)

    local endTime = nowPool.closeTime
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    go.limitPoolNode.startTxt01.text = string.format(Language.LUA_GACHA_REMAINING_TIME, math.ceil((endTime - curServerTime) / (3600 * 24))) .. "  " .. Language.LUA_SHOP_END_AT .. Utils.appendUTC(Utils.timestampToDateYMDHM(endTime))
    go.limitPoolNode.startTxt02.text = string.format(Language.LUA_GACHA_REMAINING_TIME, math.ceil((endTime - curServerTime) / (3600 * 24))) .. "  " .. Language.LUA_SHOP_END_AT .. Utils.appendUTC(Utils.timestampToDateYMDHM(endTime))
    local poolData = Tables.gachaCharPoolTable[nowPool.id]

    go.limitPoolNode.titleImg01:LoadSprite(UIConst.UI_SPRITE_GACHA_POOL, poolData.nameImage)
    go.limitPoolNode.titleImg02:LoadSprite(UIConst.UI_SPRITE_GACHA_POOL, poolData.nameImage)


    if index > 2 then
        index = index - 2
    end
    if index == 2 then
        go.limitPoolNode.infoNode1.gameObject:SetActive(false)
        go.limitPoolNode.infoNode2.gameObject:SetActive(true)
        go.limitPoolNode.upCharImage:LoadSprite(UIConst.UI_SPRITE_SHOP_ROLE_IMAGE, self.view.config.upBG2)
        go.limitPoolNode.charPic:LoadSprite(UIConst.UI_SPRITE_SHOP_ROLE_IMAGE, self.view.config.charPic2)
    end

    go.limitPoolNode.upCharShopBtn1.onClick:RemoveAllListeners()
    go.limitPoolNode.upCharShopBtn1.onClick:AddListener(function()
        PhaseManager:GoToPhase(PhaseId.GachaPool, {poolId = nowPool.id})
    end)
    go.limitPoolNode.upCharShopBtn2.onClick:RemoveAllListeners()
    go.limitPoolNode.upCharShopBtn2.onClick:AddListener(function()
        PhaseManager:GoToPhase(PhaseId.GachaPool, {poolId = nowPool.id})
    end)
end

ShopEntryCtrl.GetPoolData = HL.Method(HL.Any).Return(HL.Opt(HL.Any, HL.Number)) << function(self, checkFunc)
    local allPool = {}
    local csGacha = GameInstance.player.gacha
    for id, csInfo in pairs(csGacha.poolInfos) do
        if checkFunc(csInfo) then
            table.insert(allPool, csInfo)
        end
    end

    table.sort(allPool, function(a, b)
        return a.openTime < b.openTime
    end)
    local nowPool = nil
    local index = 1
    for i, csInfo in ipairs(allPool) do
        if csInfo.isOpened and not csInfo.isClosed and csInfo.openTime ~= 0 then
            nowPool = csInfo
            index = i
            break
        end
    end
    return nowPool, index
end

ShopEntryCtrl.InitUpWeaponPool = HL.Method(HL.Any) << function(self, go)
    go.rolePoolNode.gameObject:SetActive(false)
    go.limitPoolNode.gameObject:SetActive(false)
    go.limitWeaponNode.gameObject:SetActive(true)
    go.weaponDealNode.gameObject:SetActive(false)


    local nowPool ,index = self:GetPoolData(function(csInfo)
        return not csInfo.isChar and csInfo.openTime ~= 0
    end)

    local endTime = nowPool.closeTime
    go.limitWeaponNode.startTxt01.text = os.date("!%m/%d", nowPool.openTime + Utils.getServerTimeZoneOffsetSeconds())
    go.limitWeaponNode.startTxt02.text = Utils.appendUTC(Utils.timestampToDateYMDHM(endTime))
    go.limitWeaponNode.endTxt01.text = os.date("!%m/%d", endTime + Utils.getServerTimeZoneOffsetSeconds())
    go.limitWeaponNode.endTxt02.text = Utils.appendUTC(Utils.timestampToDateYMDHM(endTime))
    go.limitWeaponNode.titleText.text = nowPool.data.name
    if index > 2 then
        index = index - 2
    end
    if index == 2 then
        go.limitWeaponNode.upWeaponPoolImage:LoadSprite(UIConst.UI_SPRITE_SHOP_ROLE_IMAGE, self.view.config.upWeaponBG2)
    end
    go.limitWeaponNode.subTitleText.text = nowPool.data.desc
    go.limitWeaponNode.upWeaponShopBtn.onClick:RemoveAllListeners()

    local _, box, goods = GameInstance.player.shopSystem:GetNowUpWeaponData()
    local nowBox
    for i = 0, box.Count - 1 do
        local boxData = Tables.shopGoodsTable[box[i].goodsTemplateId]
        if boxData.weaponGachaPoolId == nowPool.id then
            nowBox = box[i]
            break
        end
    end
    go.limitWeaponNode.upWeaponShopBtn.onClick:AddListener(function()
        local _, box, goods = GameInstance.player.shopSystem:GetNowUpWeaponData()
        PhaseManager:OpenPhase(PhaseId.GachaWeaponPool, {goodsData = nowBox})
    end)

end

ShopEntryCtrl.InitWeaponDealPool = HL.Method(HL.Any) << function(self, go)
    go.rolePoolNode.gameObject:SetActive(false)
    go.limitPoolNode.gameObject:SetActive(false)
    go.limitWeaponNode.gameObject:SetActive(false)
    go.weaponDealNode.gameObject:SetActive(true)


    go.weaponDealNode.weaponDealShopBtn.onClick:RemoveAllListeners()
    go.weaponDealNode.weaponDealShopBtn.onClick:AddListener(function()
        self.m_top.m_tabClickFuncs[2]()
        self.m_top:RefreshTab()
    end)
end

ShopEntryCtrl.SwitchPage = HL.Method(HL.Any, HL.Any) << function(self, oldPage, newPage)
    oldPage.gameObject:SetActive(false)
    newPage.gameObject:SetActive(true)
end


ShopEntryCtrl.OnShow = HL.Override() << function(self)
    local cell = self.m_getCellFunc(LuaIndex(self.view.main.centerIndex))
    self:PlayInAnimation(cell)
    self:CheckTab()
end


ShopEntryCtrl.OnClose = HL.Override() << function(self)
    LuaSystemManager.gachaSystem:UpdateGachaWeaponSettingState()
    UIManager:ToggleBlockObtainWaysJump("shop_entry", false)
    UIManager:Close(PanelId.ShopTopNode)
    self.view.main.onScrollEnd:RemoveAllListeners()
end

ShopEntryCtrl.OnPhaseRefresh = HL.Override(HL.Opt(HL.Any)) << function(self, arg)
    if not self.m_arg and not arg then
        return
    end
    if arg then
        self.m_arg = arg
    end
    self:ProcessJumpArg(self.m_arg)
end




HL.Commit(ShopEntryCtrl)
