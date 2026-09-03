local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharHeadCell = HL.Class('CharHeadCell', UIWidgetBase)


CharHeadCell.info = HL.Field(HL.Table)

CharHeadCell.data = HL.Field(HL.Userdata)


CharHeadCell._OnFirstTimeInit = HL.Override() << function(self)
end

CharHeadCell.InitCharHeadCell = HL.Method(HL.Any, HL.Opt(HL.Function)) << function(self, info, onClick)
    self:_FirstTimeInit()
    self.info = info
    local characterTable = Tables.characterTable
    local data = characterTable:GetValue(self.info.charId)
    local instId = info.instId
    local charInfo = nil
    if instId and instId > 0 then
        charInfo = CharInfoUtils.getPlayerCharInfoByInstId(instId)
    end
    self:RefreshCharInfo(charInfo)
    self.data = data
    self:SetForbid(self.info.forbid == true)
    self:SetSelect(self.info.selectIndex, true)
    local spriteName = UIConst.UI_CHAR_HEAD_PREFIX .. self.info.charId
    self.view.imageChar:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, spriteName)
    self.view.imageNum.gameObject:SetActive(not self.info.single)
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        if onClick then
            onClick()
        end
    end)
end

CharHeadCell.SetForbid = HL.Method(HL.Boolean) << function(self, forbid)
    self.view.buttonForbid.gameObject:SetActive(forbid)
end

CharHeadCell.RefreshCharInfo = HL.Method(HL.Userdata) << function(self, charInfo)
    if charInfo then
        self.view.textLv.text = string.format("%d", charInfo.level)
        self.view.textLv.gameObject:SetActive(true)
    else
        self.view.textLv.gameObject:SetActive(false)
    end
end

CharHeadCell.SetImageSelect = HL.Method(HL.Boolean) << function(self, active)
    self.view.imageSelectBg.gameObject:SetActive(active)
end

CharHeadCell.SetSelect = HL.Method(HL.Any, HL.Opt(HL.Boolean)) << function(self, num, noAnimation)
    self.info.selectIndex = num
    if num ~= nil and num > 0 then
        if noAnimation then

            self.view.charHeadCellAnimation:SeekToPercent("charhead_select", 1)
        else
            self.view.charHeadCellAnimation:PlayAnimation("charhead_select")
        end
        self.view.textNum.text = string.format("%d", num)
    else
        if noAnimation then
            self.view.charHeadCellAnimation:SeekToPercent("charhead_unselect", 1)
        else
            self.view.charHeadCellAnimation:PlayAnimation("charhead_unselect")
        end
    end
end

CharHeadCell._OnLevelChanged = HL.Method() << function(self, arg)
    if not self.info or not self.info.instId or self.info.instId <= 0 then
        return
    end

    if not arg or not arg.result then
        return
    end

    if arg.charInstId == self.info.instId then
        local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(arg.charInstId)
        self:RefreshCharInfo(charInfo)
    end
end

HL.Commit(CharHeadCell)
return CharHeadCell
