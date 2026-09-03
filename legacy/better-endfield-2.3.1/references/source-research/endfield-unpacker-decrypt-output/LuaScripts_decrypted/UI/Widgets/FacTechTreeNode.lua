local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local State = {
    None = 0,
    Locked = 1,
    CanUnlock = 2,
    Unlocked = 3,
}

FacTechTreeNode = HL.Class('FacTechTreeNode', UIWidgetBase)

FacTechTreeNode.techId = HL.Field(HL.String) << ""

FacTechTreeNode.x = HL.Field(HL.Number) << 0

FacTechTreeNode.y = HL.Field(HL.Number) << 0

FacTechTreeNode.m_state = HL.Field(HL.Number) << State.None

FacTechTreeNode.m_onClickFunc = HL.Field(HL.Function)

FacTechTreeNode.m_onIsNaviTargetChanged = HL.Field(HL.Function)


FacTechTreeNode._OnFirstTimeInit = HL.Override() << function(self)
    self.view.itemBtn.onClick:AddListener(function()
        if self.m_onClickFunc then
            self.m_onClickFunc()
        end
    end)


    self.view.itemBtn.onIsNaviTargetChanged = function(isTarget)
        if self.m_onIsNaviTargetChanged then
            self.m_onIsNaviTargetChanged(isTarget)
        end
    end
end

FacTechTreeNode.InitFacTechTreeNode = HL.Method(HL.Table, HL.Boolean, HL.Function, HL.Function)
        << function(self, techInfo, recommend, onClickFun, onIsNaviTargetChanged)
    self:_FirstTimeInit()

    self.techId = techInfo.techId
    self.x = techInfo.x
    self.y = techInfo.y

    self.gameObject.name = "Node-" .. techInfo.techId

    self.m_onClickFunc = onClickFun
    self.m_onIsNaviTargetChanged = onIsNaviTargetChanged


    self:OnShowNameStateChange(false)
    self:Refresh(recommend)
    self.view.redDot:InitRedDot("TechTreeNode", techInfo.techId)
end

FacTechTreeNode.Refresh = HL.Method(HL.Boolean) << function(self, recommend)
    local techTreeSystem = GameInstance.player.facTechTreeSystem
    local techId = self.techId
    local nodeData = Tables.facSTTNodeTable:GetValue(techId)
    self.view.nameTxt.text = nodeData.name

    local layerIsLocked = techTreeSystem:LayerIsLocked(nodeData.layer)
    local isLocked = techTreeSystem:NodeIsLocked(nodeData.techId)
    self.view.lockNode.gameObject:SetActiveIfNecessary(layerIsLocked)
    self.view.completeNode.gameObject:SetActiveIfNecessary(not isLocked)
    self.view.normalNode.gameObject:SetActiveIfNecessary(not layerIsLocked and isLocked)

    local hasCornerIcon = not string.isEmpty(nodeData.cornerIcon)
    self.view.cornerIconNodeN.gameObject:SetActiveIfNecessary(hasCornerIcon)
    self.view.cornerIconNodeL.gameObject:SetActiveIfNecessary(hasCornerIcon)

    if hasCornerIcon then
        self.view.cornerIconN:LoadSprite(UIConst.UI_SPRITE_FAC_HUB_ICON, nodeData.cornerIcon)
        self.view.cornerIconL:LoadSprite(UIConst.UI_SPRITE_FAC_HUB_ICON, nodeData.cornerIcon)
    end

    self.view.iconN:LoadSprite(UIConst.UI_SPRITE_FAC_TECH_ICON, nodeData.icon)
    self.view.iconC:LoadSprite(UIConst.UI_SPRITE_FAC_TECH_ICON, nodeData.icon)
    self.view.iconL:LoadSprite(UIConst.UI_SPRITE_FAC_TECH_ICON, nodeData.icon)

    self.transform.localPosition = Vector3(self.x, self.y)

    self.view.recommend.gameObject:SetActiveIfNecessary(recommend)

    local preState = self.m_state
    if not isLocked then
        self.m_state = State.Unlocked
    elseif not layerIsLocked then
        self.m_state = State.CanUnlock
    else
        self.m_state = State.Locked
    end

    if preState == State.CanUnlock and self.m_state == State.Unlocked then
        self.view.animationWrapper:Play("factechtree_treenode_unlock")
    elseif preState == State.Locked and self.m_state == State.CanUnlock then
        self.view.animationWrapper:Play("factechtree_treenodenormal_unlock")
    end
end

FacTechTreeNode.OnSelect = HL.Method(HL.Boolean) << function(self, isSelect)
    self.view.selected.gameObject:SetActiveIfNecessary(isSelect)
end

FacTechTreeNode.OnShowNameStateChange = HL.Method(HL.Boolean) << function(self, show)
    self.view.nameNode.gameObject:SetActiveIfNecessary(show)
end

HL.Commit(FacTechTreeNode)
return FacTechTreeNode
