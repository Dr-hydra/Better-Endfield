local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AreaBuffPopup
local ICON_FOLDER = "ItemIcon"

AreaBuffPopupCtrl = HL.Class('AreaBuffPopupCtrl', uiCtrl.UICtrl)






AreaBuffPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
}

AreaBuffPopupCtrl.m_tabCellCache = HL.Field(HL.Forward("UIListCache"))

AreaBuffPopupCtrl.m_redDotTable = HL.Field(HL.Table)

AreaBuffPopupCtrl.m_showBuffList = HL.Field(HL.Table)

AreaBuffPopupCtrl.m_animIndex = HL.Field(HL.Number) << 0

AreaBuffPopupCtrl.m_animTime = HL.Field(HL.Number) << 0

AreaBuffPopupCtrl.m_animTickHandle = HL.Field(HL.Number) << -1

AreaBuffPopupCtrl.m_isClose = HL.Field(HL.Boolean) << false



AreaBuffPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_showBuffList = {}
    self.m_redDotTable = {}
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    local lastLevel = arg.lastLevel
    local curLevel = arg.curLevel
    local curLevelBuffList = arg.curLevelBuffList
    local lastLevelBuffList = arg.lastLevelBuffList

    self.view.btnClose.onClick:AddListener(function()
        self.m_isClose = true
        PhaseManager:PopPhase(PhaseId.AreaBuffPopup)
    end)

    self.view.mask.onClick:AddListener(function()
        self.m_isClose = true
        PhaseManager:PopPhase(PhaseId.AreaBuffPopup)
    end)

    if curLevelBuffList == nil or curLevelBuffList.Count == 0 then
        self.view.buffDetailsListNode.gameObject:SetActive(false)
        self.view.buffEmptyNode.gameObject:SetActive(true)
        return
    end

    if curLevel > lastLevel then
        self:UpdateRedDotInfo(curLevelBuffList, lastLevelBuffList)
    end

    self.m_tabCellCache = UIUtils.genCellCache(self.view.buffDetailsCell)
    self.m_tabCellCache:Refresh(curLevelBuffList.Count, function(cell, luaIndex)
        local buffName = curLevelBuffList[CSIndex(luaIndex)]
        local data = Tables.etherSubmitBuffShowTable[buffName]
        if self.m_redDotTable[buffName] then
            cell.redDot.gameObject:SetActiveIfNecessary(true)
        else
            cell.redDot.gameObject:SetActiveIfNecessary(false)
        end

        cell.itemIcon:InitItemIcon(data.effectIcon)
        cell.titleTxt.text = data.effectTitle
        cell.buffLevelTxt.text = data.effectLevelText
        local globalData = Tables.etherSubmitGlobalEffectTable[buffName]
        if globalData.effectType == GEnums.GlobalEffectType.AddBuff or globalData.effectType == GEnums.GlobalEffectType.AddGlobalBuff then
            cell.contentTxt.text = GameInstance.player.inventory:GetBuffDesc(buffName)
        elseif globalData.effectType == GEnums.GlobalEffectType.AddDropRate then
            cell.contentTxt.text = string.format(data.effectContentText, globalData.dp1)
        else
            cell.contentTxt.text = ""
        end

        cell.animationWrapper.gameObject:SetActive(false)
        table.insert(self.m_showBuffList, cell)
    end)

    self.m_animIndex = 1
    self.m_animTime = 999
    self.m_animTickHandle = LuaUpdate:Add("Tick", function(deltaTime)
        self:_AnimTick(deltaTime)
    end)
end

AreaBuffPopupCtrl._AnimTick = HL.Method(HL.Number) << function(self, deltaTime)
    if self.m_isClose then
        return
    end
    if self.m_animIndex > #self.m_showBuffList then
        return
    end

    self.m_animTime = self.m_animTime + deltaTime
    if self.m_animTime < self.view.config.CELL_PLAY_IN_INTERVAL then
        return
    end
    self.m_animTime = 0
    local cell = self.m_showBuffList[self.m_animIndex]
    if cell then
        cell.animationWrapper.gameObject:SetActive(true)
        cell.animationWrapper:PlayInAnimation()
    end

    self.m_animIndex = self.m_animIndex + 1
end

AreaBuffPopupCtrl.UpdateRedDotInfo = HL.Method(HL.Any, HL.Any) << function(self, curLevelBuffList, lastLevelBuffList)
    self.m_redDotTable = {}
    for curKey, curBuff in pairs(curLevelBuffList) do
        local isNew = false
        local isHave = false
        local curBuffInfo = Tables.etherSubmitBuffShowTable[curBuff]
        if curBuffInfo ~= nil then
            if lastLevelBuffList ~= nil then
                for lastKey, lastBuff in pairs(lastLevelBuffList) do
                    local lastBuffInfo = Tables.etherSubmitBuffShowTable[lastBuff]
                    if lastBuffInfo ~= nil then
                        if lastBuffInfo.effectFlagType == curBuffInfo.effectFlagType then
                            isHave = true
                            if curBuffInfo.effectFlagLevel > lastBuffInfo.effectFlagLevel then
                                isNew = true
                            end
                        end
                    end
                end
            end
            if not isHave then
                isNew = true
            end
        end
        self.m_redDotTable[curBuff] = isNew
    end
end


AreaBuffPopupCtrl.OnClose = HL.Override() << function(self)
    if self.m_animTickHandle ~= -1 then
        LuaUpdate:Remove(self.m_animTickHandle)
        self.m_animTickHandle = -1
    end
end


HL.Commit(AreaBuffPopupCtrl)
