local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

TipWeaponLevelNode = HL.Class('TipWeaponLevelNode', UIWidgetBase)


TipWeaponLevelNode._OnFirstTimeInit = HL.Override() << function(self)

end

TipWeaponLevelNode.InitTipWeaponLevelNodeNoInst = HL.Method(HL.Number, HL.Number, HL.Number, HL.Number) << function(self, curLv, stageLv, curBreakthroughLv, maxBreakthroughLv)
    self.view.curLvText.text = curLv
    self.view.stageLvText.text = "/" .. stageLv
    self.view.levelBreakNode:InitLevelBreakNodeSimple(curBreakthroughLv, maxBreakthroughLv, false)

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.levelNode)
end

TipWeaponLevelNode.InitTipWeaponLevelNode = HL.Method(HL.String, HL.Number) << function(self, templateId, instId)
    self:_FirstTimeInit()

    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(templateId, instId)

    if not weaponExhibitInfo.stageLv then
        return
    end
    self:InitTipWeaponLevelNodeNoInst(weaponExhibitInfo.curLv, weaponExhibitInfo.stageLv, weaponExhibitInfo.curBreakthroughLv, weaponExhibitInfo.maxBreakthroughLv)
end

HL.Commit(TipWeaponLevelNode)
return TipWeaponLevelNode

