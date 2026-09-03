local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

AdventureDungeonCell = HL.Class('AdventureDungeonCell', UIWidgetBase)



AdventureDungeonCell.m_genRewardCells = HL.Field(HL.Forward("UIListCache"))

AdventureDungeonCell.m_rewardInfos = HL.Field(HL.Table)

AdventureDungeonCell.m_info = HL.Field(HL.Table)

AdventureDungeonCell.m_subGameIds = HL.Field(HL.Table)




AdventureDungeonCell._OnFirstTimeInit = HL.Override() << function(self)

    self.view.goToBtn.onClick:RemoveAllListeners()
    self.view.goToBtn.onClick:AddListener(function()
        self:_OnClickGoToBtn()
    end)

    self.view.tracerBtn.onClick:RemoveAllListeners()
    self.view.tracerBtn.onClick:AddListener(function()
        self:_OnClickTracerBtn()
    end)

    self.view.lockBtn.onClick:RemoveAllListeners()
    self.view.lockBtn.onClick:AddListener(function()
        self:_OnClickLockBtn()
    end)

    self.m_genRewardCells = UIUtils.genCellCache(self.view.rewardCell)
    self.view.rewardListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

AdventureDungeonCell._ResetReusableState = HL.Method() << function(self)
    local stateCtrl = self.view.contentState

    stateCtrl:SetState("BossNormal")
    stateCtrl:SetState("PropsNormal")
    stateCtrl:SetState("Normal")
    self.view.staminaState:SetState("HideStamina")
    self.view.expandState:SetState("Hide")
    self.view.staminaCostTxt.gameObject:SetActive(false)
    self.view.dungeonBgImg.gameObject:SetActiveIfNecessary(false)
end



AdventureDungeonCell.InitAdventureDungeonCell = HL.Method(HL.Any, HL.Opt(HL.Boolean)) << function(self, info, isScrollRect)
    self:_FirstTimeInit()
    self:_ResetReusableState()

    self.m_info = info
    self.m_subGameIds = info.subGameIds or {}

    if info.isEmpty then
        self.view.contentState:SetState("Empty")
        return
    end


    local hasRoleImg = not string.isEmpty(info.dungeonRoleImg)
    local hasDungeonImg = not string.isEmpty(info.dungeonImg)

    local stateCtrl = self.view.contentState
    if info.dungeonCategory == GEnums.DungeonCategoryType.BossRush or
       info.dungeonCategory == GEnums.DungeonCategoryType.MiniBossRush then
        self.view.imgState:SetState("ShowBoss")
        if info.isHunterMode then
            stateCtrl:SetState("BossHunt")

        else
            stateCtrl:SetState("BossNormal")

        end
        self.view.bossImg:LoadSprite(UIConst.UI_SPRITE_MONSTER_ICON_BIG, info.dungeonRoleImg)
    elseif info.dungeonCategory == GEnums.DungeonCategoryType.SpecialResource then
        self.view.imgState:SetState("ShowBoss")
        if info.isHunterMode then
            stateCtrl:SetState("PropsHunt")

        else
            stateCtrl:SetState("PropsNormal")

        end
        self.view.dungeonImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, info.dungeonImg)
    else

        if hasRoleImg then
            self.view.imgState:SetState("ShowRoleIcon")
            self.view.roleImg:LoadSprite(UIConst.UI_SPRITE_MONSTER_ICON_BIG, info.dungeonRoleImg)
            if hasDungeonImg then
                self.view.dungeonBgImg.gameObject:SetActiveIfNecessary(true)
                self.view.dungeonBgImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, info.dungeonImg)
            else
                self.view.dungeonBgImg.gameObject:SetActiveIfNecessary(false)
            end
        elseif hasDungeonImg then
            self.view.imgState:SetState("ShowDungeonIcon")
            self.view.dungeonBgImg.gameObject:SetActiveIfNecessary(true)
            self.view.dungeonBgImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, info.dungeonImg)
            self.view.dungeonImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, info.dungeonImg)
        end
    end

    if info.mapMarkType == GEnums.MarkType.EnemySpawner then
        local isFull = GameInstance.player.worldEnergyPointSystem.isFull
        if isFull then
            stateCtrl:SetState("SiltationPointLock")
        else
            stateCtrl:SetState("SiltationPoint")
        end
        local domainId = info.domainId
        local domainStateCtrl = self.view.siltationPointState
        if domainId ~= nil and domainId == "domain_1" then
            domainStateCtrl:SetState("Tundra")
        elseif domainId ~= nil and domainId == "domain_2" then
            domainStateCtrl:SetState("Hongshan")
        end
    end

    self.view.dungeonNameTxt.text = info.dungeonName


    if info.dungeonCategory == GEnums.DungeonCategoryType.SpecialResource then
        if not info.isActive then
            self.view.staminaState:SetState("HideStamina")
            self.view.expandState:SetState("NotActivated")
        else
            if string.isEmpty(info.staminaTxt) then
                self.view.expandState:SetState("Hide")
                self.view.staminaCostTxt.gameObject:SetActive(false)
            else
                self.view.staminaState:SetState("ShowStamina")
                self.view.staminaCostTxt.text = info.staminaTxt
                self.view.staminaCostTxt.text = info.staminaTxt
                if ActivityUtils.hasStaminaReduceCount() then
                    self.view.staminaCostTxt.gameObject:SetActive(false)
                    self.view.expandState:SetState("Relief")
                    self.view.expandState.gameObject:GetComponent("UIAnimationWrapper"):PlayInAnimation()
                else
                    self.view.expandState:SetState("Normal")
                end
            end
        end
    else
        if string.isEmpty(info.staminaTxt) or not info.isActive then
            self.view.staminaState:SetState("HideStamina")
            if info.dungeonCategory == GEnums.DungeonCategoryType.BasicResource or
                info.dungeonCategory == GEnums.DungeonCategoryType.CharResource then
                self.view.expandState:SetState("NotActivated")
            else
                self.view.expandState:SetState("Hide")
                self.view.staminaCostTxt.gameObject:SetActive(false)
            end
        else
            self.view.staminaState:SetState("ShowStamina")
            self.view.staminaCostTxt.text = info.staminaTxt
            self.view.staminaCostTxt.text = info.staminaTxt
            if ActivityUtils.hasStaminaReduceCount() then
                self.view.staminaCostTxt.gameObject:SetActive(false)
                self.view.expandState:SetState("Relief")
                self.view.expandState.gameObject:GetComponent("UIAnimationWrapper"):PlayInAnimation()
            else
                self.view.expandState:SetState("Normal")
            end
        end
    end


    self.m_rewardInfos = info.rewardInfos
    self.m_genRewardCells:Refresh(#self.m_rewardInfos, function(cell, luaIndex)
        local rewardInfo = self.m_rewardInfos[luaIndex]
        cell:InitItemAdventureReward(rewardInfo)
    end)



    if not info.isActive or info.mapMarkType == GEnums.MarkType.EnemySpawner then
        self.view.btnNodeState:SetState("ShowTracerBtn")
    else
        self.view.btnNodeState:SetState("ShowGoToBtn")
    end

    if string.isEmpty(self.m_info.redDotName) then
        self.view.redDot:InitRedDot(
            "AdventureDungeonCell",
            self.m_subGameIds,
            nil,
            isScrollRect and
                self:GetUICtrl().view.dungeonCategoryListReddot or
                self:GetUICtrl().view.singleCategoryListReddot)
    else
        self.view.redDot:InitRedDot(
            self.m_info.redDotName,
            self.m_info.redDotArg,
            nil,
            isScrollRect and
                self:GetUICtrl().view.dungeonCategoryListReddot or
                self:GetUICtrl().view.singleCategoryListReddot)
    end

end

AdventureDungeonCell._OnClickGoToBtn = HL.Method() << function(self)
    local id = self.m_info.seriesId
    if string.isEmpty(id) then
        return
    end
    if self.m_info.onGotoDungeon then
        self.m_info.onGotoDungeon()
    end
    Notify(MessageConst.ON_OPEN_DUNGEON_ENTRY_PANEL, { id })
    GameInstance.player.subGameSys:SendSubGameListRead(self.m_subGameIds)
end

AdventureDungeonCell._OnClickTracerBtn = HL.Method() << function(self)
    local hasData, instId = GameInstance.player.mapManager:GetMapMarkInstId(self.m_info.mapMarkType, self.m_info.seriesId)
    if not hasData then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ADVENTURE_DUNGEON_TRACE_NO_MAPMARKINST)
        logger.error("[MapManager.GetMapMarkInstId] missing, id = " .. self.m_info.seriesId .. " type = " .. self.m_info.mapMarkType:ToString())
        return
    end
    if self.m_info.onGotoDungeon then
        self.m_info.onGotoDungeon()
    end
    MapUtils.openMap(instId)
    GameInstance.player.subGameSys:SendSubGameListRead(self.m_subGameIds)
end

AdventureDungeonCell._OnClickLockBtn = HL.Method() << function(self)
    local gameGroupId = self.m_info.gameGroupId
    UIManager:Open(PanelId.GemTermOverviewPopup, gameGroupId)
end

HL.Commit(AdventureDungeonCell)
return AdventureDungeonCell

