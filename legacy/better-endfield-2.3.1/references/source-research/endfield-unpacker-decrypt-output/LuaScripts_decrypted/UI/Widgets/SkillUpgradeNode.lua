local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SkillUpgradeNode = HL.Class('SkillUpgradeNode', UIWidgetBase)

SkillUpgradeNode.m_itemCells = HL.Field(HL.Forward("UIListCache"))

SkillUpgradeNode.m_skillLevelUpData = HL.Field(HL.Any)


SkillUpgradeNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_itemCells = UIUtils.genCellCache(self.view.itemCell)
end

SkillUpgradeNode.InitSkillUpgradeNode = HL.Method(HL.Userdata, HL.Opt(HL.Function)) << function(self,
                                                                                                skillLevelUpData,
                                                                                                callback)
    self:_FirstTimeInit()
    self.m_skillLevelUpData = skillLevelUpData

    local itemBundle = skillLevelUpData.itemBundle
    self.m_itemCells:Refresh(itemBundle.Count, function(cell, luaIndex)
        local item = itemBundle[CSIndex(luaIndex)]
        local needCount = item.count

        cell:InitItem(item, true)
        UIUtils.setItemStorageCountText(cell.view.storageNode, item.id, needCount)
    end)
    self.view.textMoney.text = tostring(skillLevelUpData.goldCost)

    self.view.btnCommon.onClick:RemoveAllListeners()
    self.view.btnCommon.onClick:AddListener(function()
        local isRPGDungeon = Utils.isInRpgDungeon()
        if isRPGDungeon then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_RPG_DUNGEON_FORBID_CHAR_SKILL)
            return
        end

        local curGold = Utils.getItemCount(UIConst.INVENTORY_MONEY_IDS[1])
        if skillLevelUpData.goldCost > curGold then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_NOT_ENOUGH_GOLD)
            return
        end

        for _, item in pairs(itemBundle) do
            local count = Utils.getItemCount(item.id)
            local needCount = item.count

            if count < needCount then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_NOT_ENOUGH_SKILL_UPGRADE_ITEM)
                return
            end
        end

        if callback then
            callback()
        end
    end)
end

HL.Commit(SkillUpgradeNode)
return SkillUpgradeNode

