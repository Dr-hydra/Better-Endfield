local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

PortableDeviceTagNode = HL.Class('PortableDeviceTagNode', UIWidgetBase)


PortableDeviceTagNode._OnFirstTimeInit = HL.Override() << function(self)
end

PortableDeviceTagNode.InitPortableDeviceTagNode = HL.Method(HL.String) << function(self, itemId)
    self:_FirstTimeInit()

    local succ, data = Tables.itemPortableDeviceTable:TryGetValue(itemId)
    if not succ then
        self.gameObject:SetActive(false)
        return
    end
    local tData = Tables.itemPortableDeviceTypeTable[data.type]
    self.view.portableDeviceTxt.text = tData.name
    self.view.lvTxt.text = Language["LUA_PORTABLE_DEVICE_LEVEL_TEXT_" .. data.lv]
    self.gameObject:SetActive(true)
end

HL.Commit(PortableDeviceTagNode)
return PortableDeviceTagNode
