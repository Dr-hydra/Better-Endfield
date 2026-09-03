local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

GuestroomCluesCell = HL.Class('GuestroomCluesCell', UIWidgetBase)

GuestroomCluesCell.m_onClick = HL.Field(HL.Function)

GuestroomCluesCell.m_genCharCells = HL.Field(HL.Forward("UIListCache"))

GuestroomCluesCell.m_hasBeenPlaced = HL.Field(HL.Boolean) << false


GuestroomCluesCell.m_clueCellData = HL.Field(HL.Userdata)


GuestroomCluesCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.clueCell.onClick:RemoveAllListeners()
    self.view.clueCell.onClick:AddListener(function()
        if self.m_onClick then
            self.m_onClick(self.m_hasBeenPlaced)
        end
    end)

    self.view.unpackBtn.onClick:RemoveAllListeners()
    self.view.unpackBtn.onClick:AddListener(function()
        if self.m_onClick then
            self.m_onClick(self.m_hasBeenPlaced)
        end
    end)

    self.view.delBtn.onClick:RemoveAllListeners()
    self.view.delBtn.onClick:AddListener(function()

        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_RECYCLE_CLUE_POPUP_TITLE,
            subContent = string.format(Language.LUA_RECYCLE_CLUE_POPUP_SUB_TITLE, Tables.spaceshipConst.recycleClueAddCreditCnt),
            onConfirm = function()
                GameInstance.player.spaceship:DeleteSelfClue(self.m_clueCellData.instId)
            end
        })
    end)

    self.view.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:RemoveAllListeners()
    self.view.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:AddListener(function(select)
        if select then
            Notify(MessageConst.ON_SPACESHIP_HEAD_NAVI_TARGET_CHANGE, self)
        end
        self.view.hintText.gameObject:SetActive(select)
    end)

    self.m_genCharCells = UIUtils.genCellCache(self.view.ssCharHeadCellRound)

    self:RegisterMessage(MessageConst.ON_SPACESHIP_CLUE_PLACE_CHANGE, function()
        if not self.m_clueCellData then
            return
        end
        self.m_hasBeenPlaced = GameInstance.player.spaceship:ClueDataHasBeenPlaced(self.m_clueCellData.instId)
        self.view.alreadyAssembledNode.gameObject:SetActive(self.m_hasBeenPlaced)
        local hintText = DeviceInfo.usingController and "ui_spaceshipguestroom_inventory_guestroomcluescell_hinttext_ps" or "ui_spaceshipguestroom_inventory_guestroomcluescell_hinttext"
        self.view.hintText:SetAndResolveTextStyle(Language[hintText]);
        self:RefreshDelState()
    end)
end

GuestroomCluesCell.InitGuestroomCluesCell = HL.Method(HL.Any, HL.Function) << function(self, data, onClick)
    self:_FirstTimeInit()
    self.m_onClick = onClick


    local success, clueData = Tables.spaceshipClueDataTable:TryGetValue(data.clueId)

    if not success then
        logger.error("未找到线索数据，检查线索数据表" .. data.id)
        return
    end
    self.m_clueCellData = data
    self.m_hasBeenPlaced = GameInstance.player.spaceship:ClueDataHasBeenPlaced(data.instId)
    self.view.alreadyAssembledNode.gameObject:SetActive(self.m_hasBeenPlaced)
    self.view.clueNameTxt.text = clueData.name
    local hintText = DeviceInfo.usingController and "ui_spaceshipguestroom_inventory_guestroomcluescell_hinttext_ps" or "ui_spaceshipguestroom_inventory_guestroomcluescell_hinttext"
    self.view.hintText:SetAndResolveTextStyle(Language[hintText]);
    self.view.colorImg.color = UIUtils.getColorByString(clueData.color)
    self.view.iconImg:LoadSprite(UIConst.UI_SPRITE_SS_CLUE_ICON, clueData.icon)
    self.view.numberTxt.text = string.format("%02d", clueData.clueType)
    local stateName = clueData.clueType .. "thNode"
    self.view.stateController:SetState(stateName)

    self.view.redDot.gameObject:SetActive(false)

    if data.expireTs > 0 then
        self.view.tagTime.gameObject:SetActive(true)
        local WARNING_TIME = 3600 * 24
        self.view.timeNode:StartTickLimitTime(data.expireTs, WARNING_TIME, function()
            Notify(MessageConst.ON_CLUE_EXPIRE, data.instId)
        end)
    else
        self.view.tagTime.gameObject:SetActive(false)
    end



    local charTable = {}
    for key,value in cs_pairs(data.charIdToProbAcc) do
        table.insert(charTable, {
            charId = key,
            upTag = value,
        })
    end

    table.sort(charTable, function(a, b)
        return a.charId < b.charId
    end)

    self.m_genCharCells:Refresh(#charTable, function(cell, luaIndex)
        local charData = charTable[luaIndex]
        cell:InitSSCharHeadCell({
            charId = charData.charId,
            showBufTag = charData.upTag,
            hideStaminaNode = true,
        })
    end)

    if data.fromRoleId ~= 0 then
        local success , friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(data.fromRoleId)
        if success then
            if GameInstance.player.roleId == data.fromRoleId then
                self.view.nameTxt.text = FriendUtils.getFriendInfoByRoleId(data.fromRoleId) .. Language.LUA_SPACESHIP_CLUE_MINE_MARK
            else
                self.view.nameTxt.text = FriendUtils.getFriendInfoByRoleId(data.fromRoleId)
            end
        else
            self.view.nameTxt.text = Language.LUA_FRIEND_NOT_EXIST
        end
        self.view.nameTxt.gameObject:SetActive(true)
    else
        self.view.nameTxt.gameObject:SetActive(false)
    end

    self:RefreshDelState()
end


GuestroomCluesCell.RefreshDelState = HL.Method() << function(self)
    local selfDict = GameInstance.player.spaceship:GetCluesByIndex(0, CS.Beyond.Gameplay.GuestRoomClueType.Self)
    if selfDict and selfDict:ContainsKey(self.m_clueCellData.instId) then
        self.view.delBtn.gameObject:SetActive(not self.m_hasBeenPlaced)
        self.view.disable.gameObject:SetActive(self.m_hasBeenPlaced)
    else
        self.view.delBtn.gameObject:SetActive(false)
        self.view.disable.gameObject:SetActive(false)
    end
end


HL.Commit(GuestroomCluesCell)
return GuestroomCluesCell

