local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WeaponGemNode = HL.Class('WeaponGemNode', UIWidgetBase)

WeaponGemNode._OnFirstTimeInit = HL.Override() << function(self)

end

WeaponGemNode.InitWeaponGemNode = HL.Method(HL.Number, HL.Opt(HL.Boolean, HL.Function)) << function(self, gemInstId, showModifyBg, actionOnClick)
    self:_FirstTimeInit()

    local hasGem = gemInstId > 0
    self.view.itemBlackShadow.gameObject:SetActive(hasGem)
    self.view.itemBlack.gameObject:SetActive(hasGem)
    self.view.emptyNode.gameObject:SetActive(not hasGem)

    self.view.gemBg.gameObject:SetActive(showModifyBg == true)
    self.view.addIcon.gameObject:SetActive(not hasGem)
    self.view.replaceIcon.gameObject:SetActive(hasGem)

    self.view.button.onClick:RemoveAllListeners()
    if (not actionOnClick) or (not showModifyBg) then
        self.view.button.enabled = false
    else
        self.view.button.onClick:AddListener(function()
            actionOnClick()
        end)
    end

    if not hasGem then
        return
    end

    local gemInst = CharInfoUtils.getGemByInstId(gemInstId)
    local templateId = gemInst.templateId
    local itemCfg = Tables.itemTable:GetValue(templateId)
    self.view.itemBlack:InitItem({
        id = itemCfg.id,
        instId = gemInstId
    })
end

HL.Commit(WeaponGemNode)
return WeaponGemNode

