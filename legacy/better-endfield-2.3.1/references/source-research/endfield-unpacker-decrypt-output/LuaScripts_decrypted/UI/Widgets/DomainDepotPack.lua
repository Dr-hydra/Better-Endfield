local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local itemTypeConfig = {
    [GEnums.DomainDepotDeliverItemType.Industry] = {
        'icon_goods_fragile01',
        'icon_goods_fragile02',
        'icon_goods_fragile03',
    },
    [GEnums.DomainDepotDeliverItemType.NaturalResource] = {
        'icon_goods_normal01',
        'icon_goods_normal02',
        'icon_goods_normal03',
    },
    [GEnums.DomainDepotDeliverItemType.Misc] = {
        'icon_goods_sturdy01',
        'icon_goods_sturdy02',
        'icon_goods_sturdy03',
    },
}

DomainDepotPack = HL.Class('DomainDepotPack', UIWidgetBase)

DomainDepotPack.m_spriteNameList = HL.Field(HL.Table)

DomainDepotPack.m_currentAnim = HL.Field(HL.Table)

DomainDepotPack.m_randomItemIndex = HL.Field(HL.Number) << 1

DomainDepotPack.m_hasChangeItemType = HL.Field(HL.Boolean) << false








DomainDepotPack.m_animInfo = HL.Field(HL.Table)

DomainDepotPack.m_randomAnimInfo = HL.Field(HL.Table)

DomainDepotPack.m_packOutAnimInfo = HL.Field(HL.Table)

DomainDepotPack.m_packInAnimInfo = HL.Field(HL.Table)

DomainDepotPack.m_itemInfo = HL.Field(HL.Table)


DomainDepotPack.m_itemCount = HL.Field(HL.Number) << 0

DomainDepotPack.m_targetCount = HL.Field(HL.Number) << 0

DomainDepotPack.m_changeValue = HL.Field(HL.Number) << 0

DomainDepotPack.m_itemType = HL.Field(GEnums.DomainDepotDeliverItemType)

DomainDepotPack.m_currentShowItemType = HL.Field(GEnums.DomainDepotDeliverItemType)

DomainDepotPack.m_packSize = HL.Field(GEnums.DeliverPackType)

DomainDepotPack.m_usedDropDownIndexTable = HL.Field(HL.Table)

DomainDepotPack.m_inRandomAnim = HL.Field(HL.Boolean) << false

DomainDepotPack.m_isUpCountAnim = HL.Field(HL.Boolean) << false

DomainDepotPack.m_isInCountChangeAnim = HL.Field(HL.Boolean) << false



DomainDepotPack._OnFirstTimeInit = HL.Override() << function(self)



    self:HideAllBlocks()

    self.m_animInfo = {}
    self.m_usedDropDownIndexTable = {}

    self.m_randomAnimInfo = {
        isLocal = true,
        name = 'domainDepot_goods_in',
        getCtrlName = function(this)
            return 'goods0' .. this.index
        end,
        beforeFunc = function(this)
            if not self.m_inRandomAnim then
                return false
            end

            if self.m_usedDropDownIndexTable[this.index] == true then

                return false
            end


            self.m_usedDropDownIndexTable[this.index] = true
            self.view['Image0' .. this.index].enabled = true
            self.view['Image0' .. this.index].gameObject:SetActiveIfNecessary(true)
            self.view['Image0' .. this.index]:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, lume.randomchoice(itemTypeConfig[self.m_itemType]))
            return true
        end,
        afterFunc = function(this)

            self.m_usedDropDownIndexTable[this.index] = false
            self.view['Image0' .. this.index].enabled = false
            self:_StartCoroutine(function()
                coroutine.wait(lume.random(self.config.RANDOM_ITEM_INTERVAL))
                if self.m_inRandomAnim then
                    table.insert(self.m_animInfo, this)
                end
                self:_PlayAnimImp()
            end)


















        end,
    }

    self.m_packOutAnimInfo = {
        name = 'domainDepot_pack_out',
        groupName = 'animationWrapper',
        audioEvent = 'Au_UI_Event_RegionWareBox_Out',
    }
    self.m_packInAnimInfo = {
        name = 'domainDepot_pack_in',
        groupName = 'animationWrapper',
        audioEvent = 'Au_UI_Event_RegionWareBox_In',
        beforeFunc = function()
            self.view.boxImage:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_%s_%s", string.lower(self.m_packSize:ToString()), string.lower(self.m_itemType:ToString())))
            self.view.frameImg:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_mask_%s", string.lower(self.m_itemType:ToString())))
            self.view.layerStateController:SetState(self.m_packSize:ToString())


            for i = 1, 9 do
                self.view['goods0' .. i].gameObject:SetActiveIfNecessary(false)
            end
        end
    }
end



DomainDepotPack.InitDomainDepotPack = HL.Method() << function(self)
    self:_FirstTimeInit()
end


DomainDepotPack.ChangePackSize = HL.Method(GEnums.DeliverPackType) << function(self, packSize)
    self.m_packSize = packSize
    self.view.layerStateController:SetState(self.m_packSize:ToString())
    self.view.boxImage:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_%s_%s", string.lower(self.m_packSize:ToString()), string.lower(self.m_itemType:ToString())))
    self.view.frameImg:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_mask_%s", string.lower(self.m_itemType:ToString())))



end

DomainDepotPack.ClearPackItemCount = HL.Method() << function(self)
    self.m_isInCountChangeAnim = false
    self.m_itemCount = 0
    self.m_targetCount = 0
    for i = 1, 9 do
        self.view['block0' .. i].gameObject:SetActiveIfNecessary(false)
    end
end


DomainDepotPack.ChangePackItemCount = HL.Method(HL.Number) << function(self, count)
    if not self.m_isInCountChangeAnim then
        self.m_isInCountChangeAnim = true
        self.m_inRandomAnim = false;
        self.view.goodsRoot.gameObject:SetActiveIfNecessary(false)
        self.view.blockRoot.gameObject:SetActiveIfNecessary(true)
        self:_StartCoroutine(function()

            while self.m_isInCountChangeAnim do
                if self.m_currentAnim ~= nil then
                    logger.warn("DomainDepotPack.ChangePackItemCount: isInCountChangeAnim but has Anther anim")
                end
                coroutine.wait(self.config.CHANGE_ITEM_COUNT_INTERVAL)
                if self.m_targetCount ~= self.m_itemCount and self.m_currentAnim == nil then
                    local changeValue = self.m_targetCount > self.m_itemCount and 1 or -1
                    local i = self.m_itemCount + (changeValue > 0 and changeValue or 0)
                    table.insert(self.m_animInfo, {
                        isLocal = true,
                        getAnimName = function()
                            if changeValue > 0 then
                                return 'domainDepot_block_in'
                            end
                            if changeValue < 0 then
                                return 'domainDepot_block_out'
                            end
                            return 'domainDepot_block_idle'
                        end,
                        getCtrlName = function()
                            return 'block0' .. i
                        end,
                        beforeFunc = function()
                            if self.m_isInCountChangeAnim ~= true then

                                return false
                            end

                            self.m_itemCount = i + (changeValue > 0 and 0 or -1)
                            self.view['block0' .. i].gameObject:SetActiveIfNecessary(true)
                        end,
                    })
                    self:_PlayAnimImp()
                end
            end
        end)
    end

    count = count > 9 and 9 or count < 0 and 0 or count
    if self.m_targetCount == count then
        return
    else
        self.m_targetCount = count
    end
end

DomainDepotPack.InitPackageSellBgNode = HL.Method(HL.Any) << function(self, deliverInfo)
    self.m_packSize = deliverInfo.deliverPackType
    self.m_itemType = deliverInfo.itemType

    self.view.layerStateController:SetState(self.m_packSize:ToString())
    self.view.boxImage:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_%s_%s", string.lower(self.m_packSize:ToString()), string.lower(self.m_itemType:ToString())))
    self.view.frameImg:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_mask_%s", string.lower(self.m_itemType:ToString())))

    self.view.goodsRoot.gameObject:SetActiveIfNecessary(false)
    self.view.blockRoot.gameObject:SetActiveIfNecessary(true)

    local valueLimitCfg = DomainDepotUtils.GetDepotPackValueLimitCfg(deliverInfo.domainDepotId)
    local limitCfg = valueLimitCfg[self.m_itemType][self.m_packSize]
    local max = limitCfg.maxLimitValue

    self.m_targetCount = math.ceil(deliverInfo.originalPrice / max * 9)


    self.m_targetCount = self.m_targetCount > 9 and 9 or self.m_targetCount < 0 and 0 or self.m_targetCount

    for i = 1, self.m_targetCount do
        self.view['block0' .. i].gameObject:SetActiveIfNecessary(true)
    end
end

DomainDepotPack.ChangePackItemType = HL.Method(GEnums.DomainDepotDeliverItemType) << function(self, itemType)






    if self.m_itemType == itemType then
        return
    end
    self.m_itemType = itemType
    if self.m_currentAnim ~= nil and self.m_currentAnim.name == 'domainDepot_pack_in' then


        self.view.boxImage:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_%s_%s", string.lower(self.m_packSize:ToString()), string.lower(self.m_itemType:ToString())))
        self.view.frameImg:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_mask_%s", string.lower(self.m_itemType:ToString())))
        return
    end

    if self.m_currentAnim ~= nil and self.m_currentAnim.name == 'domainDepot_pack_out' then

        return
    end

    self.m_inRandomAnim = false
    self.view.goodsRoot.gameObject:SetActiveIfNecessary(false)
    self:_ChangePack()
    self.m_isInCountChangeAnim = false;
    table.insert(self.m_animInfo, {
        name = 'NOT PLAY change index for start',
        beforeFunc = function()
            if self.m_isInCountChangeAnim then

                return false
            end







            self:PlayRandomItemDropAnim(self.m_itemType)
        end
    })





















end


DomainDepotPack.PlayRandomItemDropAnim = HL.Method(GEnums.DomainDepotDeliverItemType) << function(self, itemType)
    self.m_itemType = itemType
    self.m_inRandomAnim = true
    self.m_isInCountChangeAnim = false;
    self.view.goodsRoot.gameObject:SetActiveIfNecessary(true)
    self.view.blockRoot.gameObject:SetActiveIfNecessary(false)
    for i = 1, 9 do
        self.view['goods0' .. i].gameObject:SetActiveIfNecessary(false)
    end
    self.m_randomItemIndex = 1
    for i = 1, 9 do
        self:_StartCoroutine(function()
            if self.m_inRandomAnim == false then
                return
            end
            coroutine.wait(lume.random(self.config.RANDOM_ITEM_INTERVAL))
            local randomAnim = lume.deepCopy(self.m_randomAnimInfo)
            randomAnim.index = i
            table.insert(self.m_animInfo, randomAnim)
            self:_PlayAnimImp()
        end)
    end
end

DomainDepotPack.GetUniqueRandomIndex = HL.Method().Return(HL.Number) << function(self)
    local idx = math.floor(lume.random(1, 9.999))
    if self.m_usedDropDownIndexTable[idx] ~= true then
        self.m_usedDropDownIndexTable[idx] = true
        return idx
    end

    for offset = 1, 9 do
        local nextIdx = (idx + offset) % 9
        if self.m_usedDropDownIndexTable[nextIdx] ~= true then
            self.m_usedDropDownIndexTable[nextIdx] = true
            return nextIdx
        end
    end
    return 0
end

DomainDepotPack.GotoSellAnim = HL.Method(GEnums.DeliverPackType, GEnums.DomainDepotDeliverItemType) << function(self, packType, itemType)
    self.m_packSize = packType
    self.m_itemType = itemType
    self.view.layerStateController:SetState(packType:ToString())


    self.m_isInCountChangeAnim = false

    if self.m_targetCount ~= self.m_itemCount then
        local changeValue = self.m_targetCount > self.m_itemCount and 1 or -1
        for i = self.m_itemCount + (changeValue > 0 and 1 or 0), self.m_targetCount + (changeValue > 0 and 0 or 1), changeValue do

            table.insert(self.m_animInfo, {
                isLocal = true,
                getAnimName = function()
                    if changeValue > 0 then
                        return 'domainDepot_block_in'
                    end
                    if changeValue < 0 then
                        return 'domainDepot_block_out'
                    end
                    return 'domainDepot_block_idle'
                end,
                getCtrlName = function()
                    return 'block0' .. i
                end,
                beforeFunc = function()
                    self.view['block0' .. i].gameObject:SetActiveIfNecessary(true)
                end,
                afterFunc = function()

                    if i == self.m_targetCount + (changeValue > 0 and -1 or 1) then
                        self.view.boxCoverImg:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_cover_%s", string.lower(itemType:ToString())))
                        self.view.boxImage:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_%s_%s", string.lower(packType:ToString()), string.lower(itemType:ToString())))
                        self.view.frameImg:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_mask_%s", string.lower(self.m_itemType:ToString())))
                    end
                end,
            })
            self:_PlayAnimImp()
        end
        self.m_itemCount = self.m_targetCount
    end



end

DomainDepotPack.CloseBoxCover = HL.Method(HL.Opt(HL.Number)) << function(self, insId)

    if insId ~= nil then
        local deliverInfo = GameInstance.player.domainDepotSystem:GetDomainDepotDeliverInfoByInstId(insId)
        self.m_packSize = deliverInfo.deliverPackType
        self.m_itemType = deliverInfo.itemType
    end

    self.view.layerStateController:SetState(self.m_packSize:ToString())
    self.view.boxCoverImg:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_cover_%s", string.lower(self.m_itemType:ToString())))
    self.view.boxImage:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_%s_%s", string.lower(self.m_packSize:ToString()), string.lower(self.m_itemType:ToString())))
    self.view.frameImg:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, string.format("bg_domain_depot_box_mask_%s", string.lower(self.m_itemType:ToString())))
    table.insert(self.m_animInfo, {
        name = 'domainDepot_pack_change',
        groupName = 'animationWrapper',
        beforeFunc = function()
            self.view.goodsRoot.gameObject:SetActiveIfNecessary(true)
            for i = 1, self.m_targetCount do
                self.view['Image0' .. i].enabled = true
                self.view['Image0' .. i].gameObject:SetActiveIfNecessary(true)
                self.view['Image0' .. i]:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT, lume.randomchoice(itemTypeConfig[self.m_itemType]))
            end
        end,
    })
    table.insert(self.m_animInfo, {
        name = 'domainDepot_pack_cover_down',
        groupName = 'animationWrapper',
    })
    self:_PlayAnimImp()
end

DomainDepotPack.HideAllBlocks = HL.Method() << function(self)

    for i = 1, 9 do
        self.view['block0' .. i].gameObject:SetActiveIfNecessary(false)
    end
end



DomainDepotPack._PlayAnimImp = HL.Method() << function(self)
    local animInfoString = ''
    if #self.m_animInfo > 0 then

        local map = lume.map(self.m_animInfo, function(a)
            return a.name or (a.getAnimName and a.getAnimName())
        end)
        for i = 1, #map do
            if i > 1 then
                animInfoString = animInfoString .. ', '
            end
            animInfoString = animInfoString .. map[i]
        end
    else
        animInfoString = 'no more anims'
    end

    if #self.m_animInfo == 0 or self.m_currentAnim ~= nil then

        logger.info(string.format("DomainDepotPack._PlayAnimImp: CurrentAnim : %s AnimList : %s", self.m_currentAnim ~= nil and self.m_currentAnim.name or 'no current anim', animInfoString))
        return
    end


    local anim = table.remove(self.m_animInfo, 1)
    local isLocal = anim.isLocal or false
    if not isLocal then
        self.m_currentAnim = anim
    end

    logger.info(string.format("DomainDepotPack._PlayAnimImp: Playing anim: %s AnimList : %s", anim.name, animInfoString))
    if anim.beforeFunc then
        local success = anim.beforeFunc(anim)
        success = success == nil or success

        if not success then

            anim = nil
            self.m_currentAnim = nil
            self:_PlayAnimImp()
            return
        end
    end

    local groupName = anim.groupName or (anim.getCtrlName ~= nil and anim.getCtrlName(anim) or nil)

    if groupName == nil then
        anim = nil
        self.m_currentAnim = nil
        self:_PlayAnimImp()
        return
    end
    if self.view[groupName].gameObject.activeInHierarchy == false then

        logger.error(string.format("DomainDepotPack._PlayAnimImp: Group %s is not active, skipping anim: %s", groupName, anim.name))
        anim = nil
        self.m_currentAnim = nil
        self:_PlayAnimImp()
        return
    end
    self.view[groupName]:Play(anim.name or anim.getAnimName(), function()
        if anim.afterFunc then
            self.m_currentAnim = nil
            anim.afterFunc(anim)
            anim = nil
        else

            anim = nil
            self.m_currentAnim = nil
            self:_PlayAnimImp()
        end
    end)
    if anim.audioEvent ~= nil then
        AudioAdapter.PostEvent(anim.audioEvent)
    end


    if isLocal then
        self:_PlayAnimImp()
    end
end

DomainDepotPack._ChangePack = HL.Method() << function(self)
    table.insert(self.m_animInfo, 1, self.m_packOutAnimInfo)
    table.insert(self.m_animInfo, 2, self.m_packInAnimInfo)
end

HL.Commit(DomainDepotPack)
return DomainDepotPack

