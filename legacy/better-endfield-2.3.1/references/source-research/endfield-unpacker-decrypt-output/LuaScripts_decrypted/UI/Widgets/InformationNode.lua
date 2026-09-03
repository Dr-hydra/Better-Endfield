local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

InformationNode = HL.Class('InformationNode', UIWidgetBase)


InformationNode._OnFirstTimeInit = HL.Override() << function(self)

    self.view.rightBtn.onClick:RemoveAllListeners()
    self.view.rightBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.AdventureReward)
    end)

    self.view.playerInfoBtn.onClick:RemoveAllListeners()
    self.view.playerInfoBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.Friend)
    end)
end

InformationNode.InitInformationNode = HL.Method() << function(self)
    self:_FirstTimeInit()

    self.view.avatorMale.gameObject:SetActiveIfNecessary(Utils.getPlayerGender() == CS.Proto.GENDER.GenMale)
    self.view.avatorFemale.gameObject:SetActiveIfNecessary(Utils.getPlayerGender() == CS.Proto.GENDER.GenFemale)


    self.view.managerName.text = GameInstance.player.playerInfoSystem.playerName
    self.view.managerLevel.text = GameInstance.player.adventure.adventureLevelData.lv
    self.view.managerNumber.text = string.format("UID:%s", GameInstance.player.playerInfoSystem.roleId)
    self.view.progressTxt.text = string.format("%d/%d", GameInstance.player.adventure.adventureLevelData.exp, GameInstance.player.adventure.adventureLevelData.exp + GameInstance.player.adventure.adventureLevelData.relativeExp)

    self.view.levelSlider.fillAmount = GameInstance.player.adventure.adventureLevelData.exp / (GameInstance.player.adventure.adventureLevelData.exp + GameInstance.player.adventure.adventureLevelData.relativeExp)

end

HL.Commit(InformationNode)
return InformationNode

