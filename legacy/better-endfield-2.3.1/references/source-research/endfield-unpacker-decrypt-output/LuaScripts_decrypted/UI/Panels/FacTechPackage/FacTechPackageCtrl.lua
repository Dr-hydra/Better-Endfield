
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacTechPackage
FacTechPackageCtrl = HL.Class('FacTechPackageCtrl', uiCtrl.UICtrl)

FacTechPackageCtrl.m_packageName2Cell = HL.Field(HL.Table)






FacTechPackageCtrl.s_messages = HL.StaticField(HL.Table) << {

}


FacTechPackageCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.FacTechTree)
    end)

    self.m_packageName2Cell = {}

    self:_InitData(self.view.left, "tech_group_tundra")
    self:_InitData(self.view.center, "tech_group_jinlong")
    self:_InitData(self.view.right, "")

    self:_InitController(arg)
end

FacTechPackageCtrl._InitData = HL.Method(HL.Any, HL.String) << function(self, packageRef, packageName)
    local facTechTreeSystem = GameInstance.player.facTechTreeSystem
    if string.isEmpty(packageName) or facTechTreeSystem:PackageIsHidden(packageName) then
        packageRef.gameObject:SetActive(false)
        return
    end

    self.m_packageName2Cell[packageName] = packageRef

    local packageData = Tables.facSTTGroupTable[packageName]
    local isLocked = facTechTreeSystem:PackageIsLocked(packageName)


    packageRef.unlocked.gameObject:SetActive(not isLocked)
    packageRef.locked.gameObject:SetActive(isLocked)

    if isLocked and packageData.conditions.Count > 0 then
        packageRef.redText:SetAndResolveTextStyle(packageData.conditions[0].desc)
    end

    packageRef.btn.onClick:AddListener(function()
        if not isLocked then
            self:Notify(MessageConst.FAC_TECH_TREE_OPEN_TREE_PANEL, { packageName })
        end
    end)


    local suffix = isLocked and "Locked" or ""
    packageRef["packageText"..suffix].text = packageData.groupName
    packageRef["descText"..suffix].text = packageData.desc






    local currentProgress, totalProgress = FactoryUtils.getPackageInvestigateProgress(packageName)
    packageRef["numberText"..suffix].text = string.format("%d/%d", currentProgress, totalProgress)
    packageRef["numberTextShadow"..suffix].text = string.format("%d/%d", currentProgress, totalProgress)

    if not isLocked then
        local height = currentProgress/totalProgress
        packageRef.bgFlow.material:SetFloat("_LiquidHeight", height)
        packageRef.bgFlow2.material:SetFloat("_LiquidHeight", height)
    end
end



FacTechPackageCtrl._InitController = HL.Method(HL.Table) << function(self, args)
    if not DeviceInfo.usingController then
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    local packageId = unpack(args or {})
    local cell = string.isEmpty(packageId) and self.view.left or self.m_packageName2Cell[packageId]
    self:SetNaviTarget(cell.btn)
end



HL.Commit(FacTechPackageCtrl)
