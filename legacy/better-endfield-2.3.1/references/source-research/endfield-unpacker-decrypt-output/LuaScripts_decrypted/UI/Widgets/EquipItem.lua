local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

EquipItem = HL.Class('EquipItem', UIWidgetBase)


EquipItem._OnFirstTimeInit = HL.Override() << function(self)

end






EquipItem.InitEquipItem = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()

    local equipInstData = EquipTechUtils.getEquipInstData(args.equipInstId)

    local isEmpty = equipInstData == nil
    self.view.imageCharMask.gameObject:SetActiveIfNecessary(false)

    if not isEmpty then
        local isEquipByChar = equipInstData.equippedCharServerId > 0
        self.view.imageCharMask.gameObject:SetActiveIfNecessary(isEquipByChar)
        if isEquipByChar then
            local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(equipInstData.equippedCharServerId)
            local charTemplateId = charInfo.templateId
            local spriteName = UIConst.UI_CHAR_HEAD_PREFIX .. charTemplateId
            self.view.imageChar:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, spriteName)
        end
    end

    if not args.noInitItem then
        local itemInfo = nil
        if not isEmpty then
            itemInfo = {
                id = equipInstData.templateId,
                instId = args.equipInstId,
            }
        end
        self.view.item:InitItem(itemInfo, args.itemInteractable)
    end

    self.view.item.view.button.enabled = args.itemInteractable
end

HL.Commit(EquipItem)
return EquipItem

