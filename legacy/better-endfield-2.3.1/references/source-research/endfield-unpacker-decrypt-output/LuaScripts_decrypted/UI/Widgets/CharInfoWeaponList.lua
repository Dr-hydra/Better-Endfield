local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharInfoWeaponList = HL.Class('CharInfoWeaponList', UIWidgetBase)

CharInfoWeaponList.m_charInfo = HL.Field(HL.Table)

CharInfoWeaponList.m_curWeaponInstId = HL.Field(HL.Int) << 0

CharInfoWeaponList.m_curSelectIndex = HL.Field(HL.Number) << 0

CharInfoWeaponList.m_getWeaponCell = HL.Field(HL.Function)

CharInfoWeaponList.m_onClickWeaponItem = HL.Field(HL.Function)

CharInfoWeaponList.m_weaponList = HL.Field(HL.Table)

CharInfoWeaponList.SetCurSelect = HL.Method(HL.Int, HL.Opt(HL.Number)) << function(self, curWeaponInstId, luaIndex)
    self.m_curWeaponInstId = curWeaponInstId
    if luaIndex == nil then
        return
    end

    if self.m_curSelectIndex ~= luaIndex then
        local curGo = self.view.weaponList:Get(CSIndex(self.m_curSelectIndex))
        if curGo then
            local curCell = self.m_getWeaponCell(curGo)
            if curCell then
                curCell.itemBig.view.selectedBG.gameObject:SetActive(false)
            end
        end

        local nextGo = self.view.weaponList:Get(CSIndex(luaIndex))
        if nextGo then
            local nextCell = self.m_getWeaponCell(nextGo)
            if nextCell then
                nextCell.itemBig.view.selectedBG.gameObject:SetActive(true)
            end
        end
    end
    self.m_curSelectIndex = luaIndex
end


CharInfoWeaponList._OnFirstTimeInit = HL.Override() << function(self)
    self.m_getWeaponCell = UIUtils.genCachedCellFunction(self.view.weaponList)
    self.view.weaponList.onUpdateCell:AddListener(function(object, csIndex)
        self:_RefreshWeaponCell(object, LuaIndex(csIndex))
    end)
end

CharInfoWeaponList.InitCharInfoWeaponList = HL.Method(HL.Table, HL.Function, HL.Opt(HL.Userdata, HL.Boolean)) << function
(self,
 charInfo,
 onClickCallback, weaponType, skipGraduallyShow)
    self:_FirstTimeInit()

    weaponType = weaponType or GEnums.WeaponType.All
    local weaponList = CharInfoUtils.getAllWeaponList(weaponType)
    local weaponCount = #weaponList
    local empty = weaponCount == 0

    self.m_charInfo = charInfo
    self.m_weaponList = weaponList
    self.m_onClickWeaponItem = onClickCallback

    self.view.emptyNode.gameObject:SetActive(empty)
    if skipGraduallyShow == nil then
        skipGraduallyShow = false
    end
    self.view.sortNode:InitSortNode(UIConst.WEAPON_SORT_OPTION, function(optData, isIncremental)
        self:_OnSortChanged(optData, isIncremental)
    end)
    self:_OnSortChanged(self.view.sortNode:GetCurSortData(), self.view.sortNode.isIncremental)
    self:_RefreshWeaponList()
end

CharInfoWeaponList._RefreshWeaponList = HL.Method() << function(self)
    if not self.m_weaponList then
        return
    end

    local weaponCount = #self.m_weaponList
    self.view.weaponList:UpdateCount(weaponCount, false, false, false, true)
end

CharInfoWeaponList._RefreshWeaponCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, index)
    local cell = self.m_getWeaponCell(object)
    local weaponInfo = self.m_weaponList[index]
    local weaponInstData = weaponInfo.instInfo.weaponInst
    local weaponInstId = weaponInstData.instId
    local weaponTemplateId = weaponInstData.templateId


    cell.itemBig:InitItem({
        id = weaponTemplateId,
        instId = weaponInstId
    }, function()
        if self.m_onClickWeaponItem then
            self.m_onClickWeaponItem(weaponInfo.instInfo, index)
        end
    end)

    cell.itemBig.view.lvNumTxt.text = weaponInstData.weaponLv


    local isCurSelectWeapon = weaponInstId == self.m_curWeaponInstId
    cell.itemBig.view.selectedBG.gameObject:SetActive(isCurSelectWeapon)
    if isCurSelectWeapon then
        self.m_curSelectIndex = index
    end


    local equippedCardInstId = weaponInstData.equippedCharServerId
    local isEquipped = equippedCardInstId and equippedCardInstId > 0
    cell.imageCharMask.gameObject:SetActive(isEquipped)
    if isEquipped then
        local charEntityInfo = CharInfoUtils.getPlayerCharInfoByInstId(equippedCardInstId)
        local charTemplateId = charEntityInfo.templateId
        local spriteName = UIConst.UI_CHAR_HEAD_PREFIX .. charTemplateId
        cell.imageChar:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, spriteName)
    end
end

CharInfoWeaponList._OnSortChanged = HL.Method(HL.Table, HL.Boolean) << function(self, optData, isIncremental)
    local sortKeys = optData.keys
    if self.m_weaponList then
        table.sort(self.m_weaponList, Utils.genSortFunction(sortKeys, isIncremental))
    end
    self:_RefreshWeaponList()
end

HL.Commit(CharInfoWeaponList)
return CharInfoWeaponList

