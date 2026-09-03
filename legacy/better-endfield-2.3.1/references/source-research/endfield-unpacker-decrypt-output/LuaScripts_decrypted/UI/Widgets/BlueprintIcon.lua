local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

BlueprintIcon = HL.Class('BlueprintIcon', UIWidgetBase)


BlueprintIcon._OnFirstTimeInit = HL.Override() << function(self)
end

BlueprintIcon.InitBlueprintIcon = HL.Method(HL.String, HL.Number) << function(self, iconId, colorId)
    self:_FirstTimeInit()

    if iconId == FacConst.FAC_BLUEPRINT_DEFAULT_ICON then


        self.view.icon:InitItemIcon("item_gold")
        self.view.icon.view.icon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, iconId)
    elseif FactoryUtils.isBlueprintProductIconGasEnv(iconId) then
        local gasSprite = FactoryUtils.blueprintProductIconGasEnvToSpriteName(iconId)
        self.view.icon:InitItemIcon("item_gold", true)
        if gasSprite then
            self.view.icon.view.icon:LoadSprite(UIConst.UI_SPRITE_FAC_GAS, gasSprite)
            self.view.icon.view.icon.transform.localScale = Vector3.one * 0.7
        end
    else
        self.view.icon:InitItemIcon(iconId, true)
    end

    colorId = colorId > 0 and colorId or FacConst.BLUEPRINT_DEFAULT_ICON_BG_COLOR_ID
    local colorData = Tables.factoryBlueprintIconBGColorTable[colorId]
    self.view.image:LoadSprite(UIConst.UI_SPRITE_BLUEPRINT, colorData.imgName)
end

HL.Commit(BlueprintIcon)
return BlueprintIcon
