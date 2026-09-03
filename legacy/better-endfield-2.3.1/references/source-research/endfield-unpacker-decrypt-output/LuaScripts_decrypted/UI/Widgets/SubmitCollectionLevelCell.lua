local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local LEVEL_CELL_GOT_HINT_STATE = "GotHint"
local LEVEL_CELL_PRESENT_STATE = "Present"
local LEVEL_CELL_LOCKED_STATE = "Locked"

SubmitCollectionLevelCell = HL.Class('SubmitCollectionLevelCell', UIWidgetBase)

SubmitCollectionLevelCell.m_rewardItemCacheCell = HL.Field(HL.Forward("UIListCache"))

SubmitCollectionLevelCell.m_tween = HL.Field(HL.Any)



SubmitCollectionLevelCell.m_submitCollectionView = HL.Field(HL.Table)

SubmitCollectionLevelCell.m_controllerSelectCellIndex = HL.Field(HL.Number) << -1



SubmitCollectionLevelCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_rewardItemCacheCell = UIUtils.genCellCache(self.view.rewardItem)
    self.view.rewardNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

SubmitCollectionLevelCell._OnDestroy = HL.Override() << function(self)
    if self.m_tween ~= nil then
        self.m_tween:Kill()
    end
end

SubmitCollectionLevelCell._OnRewardItemClick = HL.Method(HL.Number) << function(self, rewardCellLuaIndex)
    local rewardCell = self.m_rewardItemCacheCell:Get(rewardCellLuaIndex)
    local posInfo
    if DeviceInfo.usingController then
        posInfo = {
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            isSideTips = true,
        }
    end

    rewardCell:ShowTips(posInfo)
end

SubmitCollectionLevelCell.InitSubmitCollectionLevelCell = HL.Method(HL.Table, HL.Number)
        << function(self, view, csIndex)
    self:_FirstTimeInit()

    local curLv = GameInstance.player.inventory:CurEtherLevel()
    local isLocked = csIndex > curLv
    local haveGotReward = csIndex < curLv
    local isCurLv = csIndex == curLv
    self.view.lvTxt.text = csIndex + 1

    local state
    if isLocked then
        state = LEVEL_CELL_LOCKED_STATE
    else
        if isCurLv then
            state = LEVEL_CELL_PRESENT_STATE
        else
            state = LEVEL_CELL_GOT_HINT_STATE
        end
    end
    self.view.stateController:SetState(state)

    local needCount = GameInstance.player.inventory:CurSubmitEtherNeedCount(csIndex+1)
    local curCount = 0
    if haveGotReward then
        curCount = needCount
    elseif isCurLv then
        curCount = GameInstance.player.inventory:CurSubmitEtherCount()
    end
    self.view.progressTxt.text = string.format("%d/%d", curCount, needCount)

    local curValue = curCount / needCount
    self.view.progressSlider.value = curValue

    local rewardData = Tables.rewardTable[GameInstance.player.inventory:CurSubmitEtherRewardID(csIndex+1)]
    local itemBundles = rewardData.itemBundles
    local rewardCount = itemBundles.Count
    self.m_rewardItemCacheCell:Refresh(rewardCount, function(rewardCell, luaIndex)
        rewardCell.view.maxMaskNode.gameObject:SetActiveIfNecessary(false)
        local itemBundle = itemBundles[CSIndex(luaIndex)]
        rewardCell:InitItem(itemBundle, function()
            self:_OnRewardItemClick(luaIndex)
        end)
        rewardCell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        rewardCell.view.rewardedCover.gameObject:SetActiveIfNecessary(haveGotReward)
        rewardCell.view.button.clickHintTextId = "virtual_mouse_hint_view"
        if not haveGotReward and itemBundle.id == "item_add_endurance" then
            local current = GameInstance.playerController.maxDashCount
            local maxDashCount = Tables.globalConst.maxDashEnergyLimit / Tables.globalConst.dashCostEnergyValue
            if current >= maxDashCount then
                rewardCell.view.rewardedCover.gameObject:SetActiveIfNecessary(false)
                rewardCell.view.maxMaskNode.gameObject:SetActiveIfNecessary(true)
            end
        end
    end)



    self.m_submitCollectionView = view


end

SubmitCollectionLevelCell.SetMaxLevel = HL.Method() << function(self)

end

SubmitCollectionLevelCell.DoProgressSliderTween = HL.Method(HL.Number) << function(self, startValue)
    local curLv = GameInstance.player.inventory:CurEtherLevel()
    local totalLv = GameInstance.player.inventory:CurSubmitEtherMaxLv()

    if curLv >= totalLv then
        return
    end

    local needCount = GameInstance.player.inventory:CurSubmitEtherNeedCount(curLv+1)
    local curCount = GameInstance.player.inventory:CurSubmitEtherCount()

    local targetValue = curCount / needCount
    self.view.progressSlider.value = startValue

    local config = self.view.config
    self.m_tween = DOTween.To(function()
        return self.view.progressSlider.value
    end, function(value)
        self.view.progressSlider.value = value
    end, targetValue, config.SLIDER_TWEEN_TIME):SetEase(config.SLIDER_TWEEN_CURV)
    AudioAdapter.PostEvent("Au_UI_Event_EtherCount")
end

SubmitCollectionLevelCell.GetProgressSlider = HL.Method().Return(CS.Beyond.UI.UISlider) << function(self)
    return self.view.progressSlider
end

HL.Commit(SubmitCollectionLevelCell)
return SubmitCollectionLevelCell

