local ShopTradeGoodsCell = require_ex('UI/Widgets/ShopTradeGoodsCell')

ShopStarGoodsCell = HL.Class('ShopStarGoodsCell', ShopTradeGoodsCell)

ShopStarGoodsCell.InitShopStarGoodsCell = HL.Method(HL.Table) << function(self, info)
    self:InitCommonShopGoodsCellCommonMode(info)
    self:_RefreshCommonHeadIcon(info)
end

ShopStarGoodsCell._RefreshCommonHeadIcon = HL.Method(HL.Table) << function(self, info)
    local commonHeadIcon = self.view.commonHeadIcon
    if not commonHeadIcon then
        return
    end
    commonHeadIcon.gameObject:SetActive(false)
    local itemCfg = Utils.tryGetTableCfg(Tables.itemTable, info.itemId)
    if not itemCfg or itemCfg.type ~= GEnums.ItemType.PhotoAnim then
        return
    end
    commonHeadIcon.gameObject:SetActive(true)
    local characterId = DomainShopUtils.GetPhotoAnimCharacterIdByItemId(info.itemId)
    local hasCharacter = not string.isEmpty(characterId)
    commonHeadIcon.headIconImg.gameObject:SetActive(hasCharacter)
    commonHeadIcon.emptyNode.gameObject:SetActive(not hasCharacter)
    if hasCharacter then
        commonHeadIcon.headIconImg:LoadSprite(UIConst.UI_SPRITE_ROUND_CHAR_HEAD, UIConst.UI_ROUND_CHAR_HEAD_PREFIX .. characterId)
    end
end

HL.Commit(ShopStarGoodsCell)
return ShopStarGoodsCell
