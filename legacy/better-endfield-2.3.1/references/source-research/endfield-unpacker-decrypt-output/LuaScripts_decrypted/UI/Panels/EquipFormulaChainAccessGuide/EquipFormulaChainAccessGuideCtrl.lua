local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.EquipFormulaChainAccessGuide

EquipFormulaChainAccessGuideCtrl = HL.Class('EquipFormulaChainAccessGuideCtrl', uiCtrl.UICtrl)

EquipFormulaChainAccessGuideCtrl.m_materialInfo = HL.Field(HL.Any)

EquipFormulaChainAccessGuideCtrl.m_missionId = HL.Field(HL.String) << ""

EquipFormulaChainAccessGuideCtrl.m_fold = HL.Field(HL.Boolean) << true

EquipFormulaChainAccessGuideCtrl.m_techConditionCellCache = HL.Field(HL.Forward("UIListCache"))






EquipFormulaChainAccessGuideCtrl.s_messages = HL.StaticField(HL.Table) << {

}


EquipFormulaChainAccessGuideCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local chainId = arg.chainId or 0

    local hasValue, materialInfo = EquipTechUtils.GetMaterialInfoByChainId(chainId)
    if not hasValue then
        logger.error("[EquipFormulaChainAccessGuide] materialInfo not found for chainId: " .. tostring(chainId))
        UIManager:Close(PANEL_ID)
        return
    end
    self.m_materialInfo = materialInfo
    self.m_missionId = materialInfo.unlockScriptMission or ""

    self:_InitBtnBinding()

    local isMissionComplete = EquipTechUtils.IsMissionCompletedByChainId(chainId)
    local isTechConditionSatisfied = EquipTechUtils.IsAllObtainWaysCompletedByChainId(chainId)
    self.view.reminderItemCell.stateController:SetState(isMissionComplete and "Complete" or "Goto")
    self.m_fold = not isMissionComplete
    if DeviceInfo.usingController then
        self.m_fold = false
    end

    self:_SetMission(isMissionComplete)
    self:_SetTechCondition(isTechConditionSatisfied)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end











EquipFormulaChainAccessGuideCtrl._SetMission = HL.Method(HL.Boolean) << function(self, isMissionComplete    )
    if isMissionComplete then
        self.view.missionRect.gameObject:SetActive(false)
        return
    end

    self.view.missionRect.gameObject:SetActive(true)
    self:_BindGotoMission(self.m_missionId)
    self:_SetMissionDesc()
end

EquipFormulaChainAccessGuideCtrl._SetTechCondition = HL.Method(HL.Boolean) << function(self, isTechConditionSatisfied)
    if isTechConditionSatisfied then
        self.view.facTechRect.gameObject:SetActive(false)
        return
    end

    self.view.facTechRect.gameObject:SetActive(true)
    self:_UpdateFoldState()
    self:_SetTechConditionDesc()
end

EquipFormulaChainAccessGuideCtrl._SetMissionDesc = HL.Method() << function(self)
    local szTarget, szCur = self:_GetMissionConditionDesc(self.m_missionId)
    self.view.reminderItemCell.titleTxt.text = szTarget
    self.view.reminderItemCell.contentTxt.text = szCur
end

EquipFormulaChainAccessGuideCtrl._InitBtnBinding = HL.Method() << function(self)
    self.view.btnClose.onClick:AddListener(function()
        self:ClosePanel()
    end)

    self.view.btnExpend.onClick:AddListener(function()
        self.m_fold = not self.m_fold
        self:_UpdateFoldState()
    end)

    self.view.maskBtn.onClick:AddListener(function()
        self:ClosePanel()
    end)
end

EquipFormulaChainAccessGuideCtrl.ClosePanel = HL.Method() << function(self)
    self:PlayAnimationOutAndClose()
end

EquipFormulaChainAccessGuideCtrl._UpdateFoldState = HL.Method() << function(self)
    self.view.reminderFormulaSubNode.gameObject:SetActive(not self.m_fold)
    self.view.arrowImage.transform.localRotation = Quaternion.Euler(0, 0, self.m_fold and 180 or 0)
end

EquipFormulaChainAccessGuideCtrl._GetMissionConditionDesc = HL.Method(HL.String).Return(HL.String, HL.String) << function(self, missionId)
    local missionSystem = GameInstance.player.mission
    local chapterId = missionSystem:GetChapterIdByMissionId(missionId)
    local chapterInfo = missionSystem:GetChapterInfo(chapterId)
    local missionName = missionSystem:GetMissionInfo(missionId).missionName:GetText()
    local targetMission = string.format(Language.LUA_EQUIP_FORMULA_CHAIN_MISSION_DESC, chapterInfo.chapterNum:GetText(), chapterInfo.episodeNum:GetText(), missionName)
    local _, curMissionDesc = Utils.getCurMissionIdAndDesc("activity")
    local curProgress = string.format(Language.LUA_EQUIP_FORMULA_CHAIN_CUR_MISSION_PROGRESS, curMissionDesc)
    return targetMission, curProgress
end

EquipFormulaChainAccessGuideCtrl._BindGotoMission = HL.Method(HL.String) << function(self, missionId)
    local jumpId = self.m_materialInfo.missionJumpId or ""
    self.view.btnGoto.onClick:RemoveAllListeners()
    self.view.btnGoto.onClick:AddListener(function()
        if not string.isEmpty(jumpId) then
            UIManager:Close(PANEL_ID)
            Utils.jumpToSystem(jumpId)
        end
    end)
end

EquipFormulaChainAccessGuideCtrl._SetTechConditionDesc = HL.Method() << function(self, conditionId)
    local itemId = self.m_materialInfo.scriptItemId
    local texts = EquipTechUtils.GetScriptUnlockObtainWayTexts(itemId, true)

    if not self.m_techConditionCellCache then
        self.m_techConditionCellCache = UIUtils.genCellCache(self.view.reminderFormulaSubCell)
    end

    self.m_techConditionCellCache:Refresh(#texts, function(cell, luaIdx)
        local txt = texts[luaIdx]
        cell.descTxt:SetAndResolveTextStyle(txt)
    end)
end

HL.Commit(EquipFormulaChainAccessGuideCtrl)
