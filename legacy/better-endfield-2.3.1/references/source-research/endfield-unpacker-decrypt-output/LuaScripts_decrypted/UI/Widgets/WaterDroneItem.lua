local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WaterDroneItem = HL.Class('WaterDroneItem', UIWidgetBase)


WaterDroneItem._OnFirstTimeInit = HL.Override() << function(self)

end

WaterDroneItem.InitWaterDroneItem = HL.Method(HL.Opt(HL.String, HL.Number, HL.String, HL.String, HL.Any)) <<
function(self, itemId, itemCount, emptyBottleId, liquidId, onClick)
    self:_FirstTimeInit()

    self.view.item:InitItem({ id = itemId, count = itemCount}, onClick)

    local success1, emptyBottleItem = Tables.itemTable:TryGetValue(emptyBottleId)
    local success2, liquidItem = Tables.itemTable:TryGetValue(liquidId)

    if success1 and success2 then
        self.view.item.view.name.text = string.format(Language.LUA_WATER_DRONE_ITEM_NAME_FORMAT, emptyBottleItem.name, liquidItem.name)
    end
end

HL.Commit(WaterDroneItem)
return WaterDroneItem

