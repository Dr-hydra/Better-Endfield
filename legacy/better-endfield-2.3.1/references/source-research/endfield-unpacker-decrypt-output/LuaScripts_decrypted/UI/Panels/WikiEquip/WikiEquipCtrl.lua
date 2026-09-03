local wikiDetailBaseCtrl = require_ex('UI/Panels/WikiDetailBase/WikiDetailBaseCtrl')
local PANEL_ID = PanelId.WikiEquip
WikiEquipCtrl = HL.Class('WikiEquipCtrl', wikiDetailBaseCtrl.WikiDetailBaseCtrl)



WikiEquipCtrl.OnShow = HL.Override() << function(self)
    WikiEquipCtrl.Super.OnShow(self)
    self:_PlayBgDecoAnim()
end

WikiEquipCtrl.GetPanelId = HL.Override().Return(HL.Number) << function(self)
    return PANEL_ID
end

WikiEquipCtrl._RefreshCenter = HL.Override() << function(self)
    WikiEquipCtrl.Super._RefreshCenter(self)
    local hasValue

    local itemData
    hasValue, itemData = Tables.itemTable:TryGetValue(self.m_wikiEntryShowData.wikiEntryData.refItemId)
    self.view.wikiItemImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
    if self.m_phase then
        self.m_phase:ActiveCommonSceneItem(true)
    end
end

WikiEquipCtrl._RefreshRight = HL.Override() << function(self)
    local view = self.view.right
    local itemId = self.m_wikiEntryShowData.wikiEntryData.refItemId
    view.itemObtainWaysForWiki:InitItemObtainWays(itemId)
    view.equipDetails.weaponAttributeNode:InitEquipAttributeNodeByTemplateId(itemId)
    view.equipDetails.equipSuitNode:InitEquipSuitNode(itemId)
    EquipTechUtils.setEquipBaseInfo(view.equipInfo, self.loader, itemId)
end



WikiEquipCtrl._PlayBgDecoAnim = HL.Method() << function(self)
    if self.m_phase then
        self.m_phase:ActiveCommonSceneItem(true)
        self.m_phase:PlayDecoAnim("wiki_uideco_grouptocommonpanel")
    end
end

HL.Commit(WikiEquipCtrl)