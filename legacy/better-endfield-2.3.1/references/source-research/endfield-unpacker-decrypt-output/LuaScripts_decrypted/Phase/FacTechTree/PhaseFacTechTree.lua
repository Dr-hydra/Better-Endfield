local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.FacTechTree

local DEFAULT_FAC_TECH_PACKAGE_ID = "tech_group_tundra"

PhaseFacTechTree = HL.Class('PhaseFacTechTree', phaseBase.PhaseBase)

PhaseFacTechTree.m_currentPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseFacTechTree.m_subPanelItem = HL.Field(HL.Forward("PhasePanelItem"))





PhaseFacTechTree.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.FAC_TECH_TREE_OPEN_PACKAGE_PANEL] = { 'OpenPackagePanel', true },
    [MessageConst.FAC_TECH_TREE_OPEN_TREE_PANEL] = { 'OpenTreePanel', true },
}



PhaseFacTechTree._OnInit = HL.Override() << function(self)
    PhaseFacTechTree.Super._OnInit(self)
end




PhaseFacTechTree._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    local facTechTreeSystem = GameInstance.player.facTechTreeSystem

    if self.arg and next(self.arg) then
        if self.arg.inPackage then

            self.arg.inPackage = false
            self.m_currentPanelItem = self:CreatePhasePanelItem(PanelId.FacTechPackage)
            return
        end


        local techId = self.arg.techId
        local packageId = self.arg.packageId
        local layerId = self.arg.layerId

        if not string.isEmpty(techId) then

            local techCfg = Tables.facSTTNodeTable[techId]
            self.m_currentPanelItem = self:CreatePhasePanelItem(PanelId.FacTechTree, {
                techId = techId,
                packageId = techCfg.groupId,
                recoverRelativeBlackboxNode = self.arg.recoverRelativeBlackboxNode,
            })
            return
        end

        if not string.isEmpty(layerId) then
            local layerCfg = Tables.facSTTLayerTable[layerId]
            self.m_currentPanelItem = self:CreatePhasePanelItem(PanelId.FacTechTree, {
                layerId = layerId,
                packageId = layerCfg.groupId,
                recoverRelativeBlackboxNode = self.arg.recoverRelativeBlackboxNode,
            })
            return
        end

        if not string.isEmpty(packageId) then
            self.m_currentPanelItem = self:CreatePhasePanelItem(PanelId.FacTechTree, {
                packageId = packageId,
                recoverRelativeBlackboxNode = self.arg.recoverRelativeBlackboxNode,
            })
            return
        end
    else

        local success, levelBasicInfo = DataManager.levelBasicInfoTable:TryGetValue(GameWorld.worldInfo.curLevelId)
        if not success then
            self.m_currentPanelItem = self:CreatePhasePanelItem(PanelId.FacTechPackage)
            return
        end

        local isInSpaceShip = Utils.isInSpaceShip()
        local domainId = isInSpaceShip and GameInstance.player.inventory.spaceshipDomainId or levelBasicInfo.domainName
        local hasDomain = not string.isEmpty(domainId)
        local facTechPackageId = hasDomain and Tables.domainDataTable[domainId].facTechPackageId or DEFAULT_FAC_TECH_PACKAGE_ID

        if facTechTreeSystem:PackageIsLocked(facTechPackageId) or facTechTreeSystem:PackageIsHidden(facTechPackageId) then
            facTechPackageId = DEFAULT_FAC_TECH_PACKAGE_ID
        end

        if facTechPackageId ~= DEFAULT_FAC_TECH_PACKAGE_ID then
            self.m_currentPanelItem = self:CreatePhasePanelItem(PanelId.FacTechTree, { packageId = facTechPackageId })
        else
            if facTechTreeSystem:PackageIsLocked(DEFAULT_FAC_TECH_PACKAGE_ID) then
                self.m_currentPanelItem = self:CreatePhasePanelItem(PanelId.FacTechPackage)
            else
                self.m_currentPanelItem = self:CreatePhasePanelItem(PanelId.FacTechTree, { packageId = DEFAULT_FAC_TECH_PACKAGE_ID })
            end
        end
    end
end

PhaseFacTechTree._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseFacTechTree._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseFacTechTree._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode,
                                                                                                     args)
end






PhaseFacTechTree._OnActivated = HL.Override() << function(self)
end

PhaseFacTechTree._OnDeActivated = HL.Override() << function(self)
end

PhaseFacTechTree._OnDestroy = HL.Override() << function(self)
    PhaseFacTechTree.Super._OnDestroy(self)
end



PhaseFacTechTree._OnRefresh = HL.Override() << function(self)
    if self.arg == nil or string.isEmpty(self.arg.techId) or self.m_currentPanelItem.uiCtrl.AutoSelect == nil then
        return
    end

    self.m_currentPanelItem.uiCtrl:AutoSelect(self.arg.techId)
end

PhaseFacTechTree.OpenTreePanel = HL.Method(HL.Any) << function(self, args)
    local arg = unpack(args)

    self:RemovePhasePanelItem(self.m_subPanelItem)
    local subItem = self.m_currentPanelItem
    self.m_currentPanelItem = self:CreatePhasePanelItem(PanelId.FacTechTree, { packageId = arg })
    self.m_subPanelItem = subItem

    subItem.uiCtrl:Hide()
end

PhaseFacTechTree.OpenPackagePanel = HL.Method(HL.Table) << function(self, args)
    self:RemovePhasePanelItem(self.m_subPanelItem)
    local subItem = self.m_currentPanelItem
    self.m_currentPanelItem = self:CreatePhasePanelItem(PanelId.FacTechPackage, args)
    self.m_subPanelItem = subItem

    subItem.uiCtrl:Hide()
end

PhaseFacTechTree.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}

    if self.m_currentPanelItem.uiCtrl.panelId == PanelId.FacTechPackage then
        arg.inPackage = true
        arg.recoverRelativeBlackboxNode = nil
    elseif self.m_currentPanelItem.uiCtrl.panelId == PanelId.FacTechTree then
        local curSelectNode = self.m_currentPanelItem.uiCtrl:GetCurSelectNode()
        if curSelectNode then
            arg.techId = curSelectNode.techId
            if self.m_currentPanelItem.uiCtrl.GetIsRelativeBlackboxNodeOpened then
                arg.recoverRelativeBlackboxNode = self.m_currentPanelItem.uiCtrl:GetIsRelativeBlackboxNodeOpened()
            else
                arg.recoverRelativeBlackboxNode = nil
            end
        else
            arg.techId = nil
            arg.recoverRelativeBlackboxNode = nil
        end
        arg.packageId = self.m_currentPanelItem.uiCtrl:GetCurPackageId()

        local isUnlockPopupOpen, popupCtrl = UIManager:IsOpen(PanelId.FacTechTreeUnlockTierPopup)
        if isUnlockPopupOpen then
            local curState = popupCtrl:GetCurState()
            arg.layerId = curState.layerId
        end
    end

    return arg
end

HL.Commit(PhaseFacTechTree)

