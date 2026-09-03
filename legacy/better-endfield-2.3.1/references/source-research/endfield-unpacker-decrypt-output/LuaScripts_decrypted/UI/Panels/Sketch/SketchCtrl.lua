
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Sketch
SketchCtrl = HL.Class('SketchCtrl', uiCtrl.UICtrl)







SketchCtrl.s_messages = HL.StaticField(HL.Table) << {
}


SketchCtrl.m_callback = HL.Field(HL.Userdata)

SketchCtrl.TryOpenSketch = HL.StaticMethod(HL.Table) << function(arg)
    if BEYOND_DEBUG then
        local ctrl = SketchCtrl.AutoOpen(PANEL_ID, nil, true)
        if ctrl == nil then
            return
        end

        local sketchData = unpack(arg)
        ctrl:ShowSketch(sketchData)
    end
end


SketchCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        local callback = self.m_callback
        self:Close()
        if callback then
            callback()
        end
    end)
end








SketchCtrl.ShowSketch = HL.Method(HL.Any) << function(self, sketchData)
    self.m_callback = sketchData.callback
    local sprite = not string.isEmpty(sketchData.imageBG) and self:LoadSprite(UIConst.UI_SPRITE_SKETCH, sketchData.imageBG)
    self.view.bg.gameObject:SetActive(not not sprite)
    self.view.bg.sprite = sprite
    local leftActive = not string.isEmpty(sketchData.imageLeft)
    local rightActive = not string.isEmpty(sketchData.imageRight)
    self.view.leftImage.gameObject:SetActive(leftActive)
    self.view.rightImage.gameObject:SetActive(rightActive)
    if leftActive then
        self.view.leftImage:LoadSprite(UIConst.UI_SPRITE_SKETCH, sketchData.imageLeft)
        self.view.leftImage:SetNativeSize()
    end

    if rightActive then
        self.view.rightImage:LoadSprite(UIConst.UI_SPRITE_SKETCH, sketchData.imageRight)
        self.view.rightImage:SetNativeSize()
    end
    self.view.text.text = sketchData.text
    UIUtils.changeAlpha(self.view.mask, sketchData.maskAlpha)
    if sketchData.textBlack then
        self.view.text.color = Color.black
    else
        self.view.text.color = Color.white
    end
end

HL.Commit(SketchCtrl)
