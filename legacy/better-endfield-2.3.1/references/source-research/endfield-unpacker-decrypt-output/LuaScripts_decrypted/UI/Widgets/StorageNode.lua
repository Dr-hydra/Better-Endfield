local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

StorageNode = HL.Class('StorageNode', UIWidgetBase)


StorageNode._OnFirstTimeInit = HL.Override() << function(self)

end

StorageNode.InitStorageNode = HL.Method(HL.Number,HL.Opt(HL.Number, HL.Boolean, HL.Boolean)) << function(self, count, needCount, ignoreInSafeZone, itemBagOnly)
    self:_FirstTimeInit()
    local prefix = ""
    if itemBagOnly then
        prefix = Language.LUA_NOT_SAFE_AREA_ITEM_COUNT_LABEL
    elseif ignoreInSafeZone then
        prefix = Language.LUA_SAFE_AREA_ITEM_COUNT_LABEL
    else
        prefix = Utils.isInSafeZone() and Language.LUA_SAFE_AREA_ITEM_COUNT_LABEL or Language.LUA_NOT_SAFE_AREA_ITEM_COUNT_LABEL
    end
    self.view.storageText.text = prefix
    self.view.storageCount.text = UIUtils.setCountColor(UIUtils.getNumString(count), needCount and count < needCount)
end

HL.Commit(StorageNode)
return StorageNode
