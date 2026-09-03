local WeaponUtils = {}

WeaponUtils.WEAPON_EXP_ITEM_LIST = {
    "item_weapon_upgrade_low",
    "item_weapon_upgrade_mid",
    "item_weapon_upgrade_high",
}

function WeaponUtils.canWeaponUpgrade(weaponInstId)
    local weaponInst = CharInfoUtils.getWeaponByInstId(weaponInstId)
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponInst.templateId, weaponInstId)
    local curLv = weaponExhibitInfo.curLv
    local stageLv = weaponExhibitInfo.stageLv
    if curLv >= stageLv then
        return false
    end

    local curExp = weaponExhibitInfo.curExp
    local nextLvExp = weaponExhibitInfo.nextLvExp
    local expNeedToUpgrade = nextLvExp - curExp
    if curExp >= nextLvExp then
        return false
    end

    local nextLvGold = weaponExhibitInfo.nextLvGold
    local curGold = Utils.getItemCount(UIConst.INVENTORY_MONEY_IDS[1])
    if curGold < nextLvGold then
        return false
    end

    local expSum = 0
    for i = 1, Tables.characterConst.weaponExpItem.Count do
        local itemId = Tables.characterConst.weaponExpItem[CSIndex(i)]
        local _, itemCfg = Tables.itemTable:TryGetValue(itemId)

        local inventoryCount = Utils.getItemCount(itemId)
        local itemExp = WeaponUtils.CalcItemExp(itemCfg)
        expSum = expSum + itemExp * inventoryCount

        if expSum >= expNeedToUpgrade then
            return true
        end
    end

    local weaponDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.Weapon]:GetOrFallback(Utils.getCurrentScope())
    local weaponInstDict = weaponDepot.instItems
    for _, itemBundle in pairs(weaponInstDict) do
        local weaponInst = itemBundle.instData
        local weaponTemplateId = weaponInst.templateId
        local _, itemCfg = Tables.itemTable:TryGetValue(weaponTemplateId)
        local isEquipped = weaponInst.equippedCharServerId and weaponInst.equippedCharServerId > 0
        local isSameEquip = weaponInst.instId == weaponInstId
        local isLocked = weaponInst.isLocked
        local hasGem = weaponInst.attachedGemInstId > 0
        if (not isEquipped) and (not isSameEquip) and (not isLocked) and (not hasGem) then
            local itemExp = WeaponUtils.CalcItemExp(itemCfg, weaponInst)
            expSum = expSum + itemExp
        end

        if expSum >= expNeedToUpgrade then
            return true
        end
    end

    return false
end

function WeaponUtils.canWeaponBreakthrough(weaponInstId)
    local weaponInst = CharInfoUtils.getWeaponByInstId(weaponInstId)
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponInst.templateId, weaponInstId)
    local curBreakthroughLv = weaponExhibitInfo.curBreakthroughLv
    local maxBreakthroughLv = weaponExhibitInfo.maxBreakthroughLv

    if weaponExhibitInfo.curLv >= weaponExhibitInfo.maxLv then
        return false
    end

    if weaponExhibitInfo.curLv < weaponExhibitInfo.stageLv then
        return false
    end

    if curBreakthroughLv >= maxBreakthroughLv then
        return false
    end

    local breakthroughTemplateCfg = weaponExhibitInfo.breakthroughTemplateCfg
    local toBreakthroughCfg = breakthroughTemplateCfg.list[curBreakthroughLv]
    if not toBreakthroughCfg then
        return false
    end

    local breakthroughGold = weaponExhibitInfo.breakthroughGold
    local curGold = Utils.getItemCount(UIConst.INVENTORY_MONEY_IDS[1])
    if curGold < breakthroughGold then
        return false
    end

    local breakItemList = toBreakthroughCfg.breakItemList
    for _, itemInfo in pairs(breakItemList) do
        local id = itemInfo.id
        local needCount = itemInfo.count

        local inventoryCount = Utils.getItemCount(id)
        if inventoryCount < needCount then
            return false
        end
    end

    return true
end


function WeaponUtils.CalcItemExp(itemCfg, weaponInst)
    local rarity = itemCfg.rarity
    local _, rarityCfg = Tables.weaponExpItemTable:TryGetValue(rarity)
    if not rarityCfg then
        logger.error("WeaponExhibitUpgradeCtrl->Can't find weapon exp item info, rarity: " .. rarity)
        return 0
    end

    local baseExp = weaponInst ~= nil and rarityCfg.weaponExp or rarityCfg.itemExp
    local compensateExp = weaponInst and WeaponUtils._CalcWeaponCompensateExp(weaponInst, rarity) or 0

    return baseExp + compensateExp
end

function WeaponUtils._CalcWeaponCompensateExp(weaponInst, rarity)
    local templateId = weaponInst.templateId
    local _, weaponCfg = Tables.weaponBasicTable:TryGetValue(templateId)
    if not weaponCfg then
        logger.error("WeaponExhibitUpgradeCtrl->Can't find weapon basic info, templateId: " .. templateId)
        return 0
    end
    local levelTemplateId = weaponCfg.levelTemplateId
    local _, levelCfg = Tables.weaponUpgradeTemplateSumTable:TryGetValue(levelTemplateId)
    if not levelCfg then
        logger.error("WeaponExhibitUpgradeCtrl->Can't find weapon level info, levelTemplateId: " .. levelTemplateId)
        return 0
    end
    local _, expItemData = Tables.weaponExpItemTable:TryGetValue(rarity)
    if not expItemData then
        logger.error("WeaponExhibitUpgradeCtrl->Can't find weapon exp item info, rarity: " .. rarity)
        return 0
    end
    local convertRatio = expItemData.weaponExpConvertRatio
    local curExp = weaponInst.exp
    local weaponLv = weaponInst.weaponLv
    local expSum = levelCfg.list[CSIndex(weaponLv)].lvUpExpSum

    return math.floor((curExp + expSum) * convertRatio)
end

function WeaponUtils.upgradeItemSortComp(a, b)
    if a.isWeapon ~= b.isWeapon then
        return a.isWeapon == false
    end

    if a.itemCfg.rarity ~= b.itemCfg.rarity then
        return a.itemCfg.rarity < b.itemCfg.rarity
    end

    if a.isWeapon and b.isWeapon then
        local aWeaponLv = a.itemInst.weaponLv
        local bWeaponLv = b.itemInst.weaponLv
        if aWeaponLv ~= bWeaponLv then
            return aWeaponLv < bWeaponLv
        end
    end

    return a.itemCfg.id > b.itemCfg.id
end

function WeaponUtils.ingredientItemSortComp(a, b)

    if a.isWeapon ~= b.isWeapon then
        return a.isWeapon == true
    end


    if a.itemCfg.rarity ~= b.itemCfg.rarity then
        return a.itemCfg.rarity > b.itemCfg.rarity
    end


    if a.isWeapon and b.isWeapon then
        local aWeaponLv = a.itemInst.weaponLv
        local bWeaponLv = b.itemInst.weaponLv
        if aWeaponLv ~= bWeaponLv then
            return aWeaponLv > bWeaponLv
        end
    end
end

function WeaponUtils.expItemSortComp(a, b)
    return a.generateExp > b.generateExp
end

function WeaponUtils.refreshListCellWeaponAddOn(cell, itemInfo)
    local instId = itemInfo.instId
    local weaponInst = CharInfoUtils.getWeaponByInstId(instId)
    if not weaponInst then
        logger.error("weaponInst is nil, itemInfo", itemInfo)
        return
    end
    local equippedCharInstId = weaponInst.equippedCharServerId

    local equipped = equippedCharInstId and equippedCharInstId > 0

    cell.imageCharMask.gameObject:SetActive(equipped)
    cell.currentSelected.gameObject:SetActive(false)
    if equipped then
        local charEntityInfo = CharInfoUtils.getPlayerCharInfoByInstId(equippedCharInstId)
        local charTemplateId = charEntityInfo.templateId
        local spriteName = UIConst.UI_CHAR_HEAD_PREFIX .. charTemplateId
        cell.imageChar:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, spriteName)
    end
end


_G.WeaponUtils = WeaponUtils
return WeaponUtils