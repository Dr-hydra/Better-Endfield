local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SortPopOut
SortPopOutCtrl = HL.Class('SortPopOutCtrl', uiCtrl.UICtrl)


SortPopOutCtrl.m_canSort = HL.Field(HL.Boolean) << false

SortPopOutCtrl.m_sortCurIndex = HL.Field(HL.Number) << -1

SortPopOutCtrl.m_sortOptions = HL.Field(HL.Table)

SortPopOutCtrl.m_onSortConfirm = HL.Field(HL.Function)

SortPopOutCtrl.m_sortCellCache = HL.Field(HL.Forward("UIListCache"))



SortPopOutCtrl.m_canSelect = HL.Field(HL.Boolean) << false

SortPopOutCtrl.m_selectCurIndexes = HL.Field(HL.Table)

SortPopOutCtrl.m_selectOptions = HL.Field(HL.Table)

SortPopOutCtrl.m_selectStates = HL.Field(HL.Table)

SortPopOutCtrl.m_onSelectToggle = HL.Field(HL.Function)

SortPopOutCtrl.m_onSelectConfirm = HL.Field(HL.Function)

SortPopOutCtrl.m_selectCellCache = HL.Field(HL.Forward("UIListCache"))






SortPopOutCtrl.s_messages = HL.StaticField(HL.Table) << {

}


SortPopOutCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        UIManager:Close(PANEL_ID)
    end)

    self.view.btnConfirm.onClick:AddListener(function()
        if self.m_onSortConfirm then
            local optionIndex = math.ceil(self.m_sortCurIndex / 2)
            local isAscending = self.m_sortCurIndex % 2 ~= 0
            self.m_onSortConfirm(optionIndex, isAscending)
        end
        if self.m_onSelectConfirm then
            self.m_onSelectConfirm(self.m_selectStates)
        end
        UIManager:Close(PANEL_ID)
    end)

    self.m_sortCellCache = UIUtils.genCellCache(self.view.sortOptionTemplate)
    self.m_selectCellCache = UIUtils.genCellCache(self.view.selectOptionTemplate)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputBindingGroup.groupId })
end












SortPopOutCtrl._ShowSelf = HL.StaticMethod(HL.Table) << function(args)
    if args == nil then
        return
    end


    local self = SortPopOutCtrl.AutoOpen(PANEL_ID, nil, false)
    if args.sortOptions then
        self:InitSortOption(args.sortOptions, args.onSortConfirm, args.curIndex, args.curIsAscending)
    end

    if args.selectOptions then
        self:InitSelectOption(args.selectOptions, args.onSelectToggle, args.onSelectConfirm)
    end
end

SortPopOutCtrl.InitSortOption = HL.Method(HL.Table, HL.Function, HL.Opt(HL.Number, HL.Boolean)) <<
function(self, sortOptions, onSortConfirm, curIndex, curIsAscending)
    self.m_sortOptions = sortOptions
    self.m_onSortConfirm = onSortConfirm
    self.m_canSort = true
    self:RefreshPanelState()

    if curIndex ~= nil and curIsAscending ~= nil then
        if curIsAscending then
            self.m_sortCurIndex = curIndex * 2 + 1
        else
            self.m_sortCurIndex = curIndex * 2 + 2
        end
    else
        self.m_sortCurIndex = -1
    end

    self.m_sortCellCache:Refresh(#sortOptions * 2, function(cell, index)
        local optionIndex = math.ceil(index / 2)
        if index % 2 == 0 then
            cell:InitSortOptionTemplate(string.format(Language.LUA_COMMON_SORT_DESCENDING, self.m_sortOptions[optionIndex].name), index == self.m_sortCurIndex)
        else
            cell:InitSortOptionTemplate(string.format(Language.LUA_COMMON_SORT_ASCENDING, self.m_sortOptions[optionIndex].name), index == self.m_sortCurIndex)
        end
        cell.view.btn.onClick:RemoveAllListeners()
        cell.view.btn.onClick:AddListener(function()
            if self.m_sortCurIndex == index then


                return
            end

            if self.m_sortCurIndex ~= -1 then
                self.m_sortCellCache:GetItem(self.m_sortCurIndex):SetSelectState(false)
            end
            self.m_sortCurIndex = index
            cell:SetSelectState(true)
        end)
    end)

    self.view.sortNaviGroup:NaviToThisGroup()
end

SortPopOutCtrl.InitSelectOption = HL.Method(HL.Table, HL.Function, HL.Function) <<
function(self, selectOptions, onSelectToggle, onSelectConfirm)
    self.m_selectOptions = selectOptions
    self.m_onSelectToggle = onSelectToggle
    self.m_onSelectConfirm = onSelectConfirm
    self.m_canSelect = true
    self:RefreshPanelState()

    self.m_selectStates = {}
    self.m_selectCellCache:Refresh(#selectOptions, function(cell, index)
        if self.m_selectOptions[index].isOn == nil then
            self.m_selectStates[index] = self.m_selectOptions[index].defaultIsOn
        else
            self.m_selectStates[index] = self.m_selectOptions[index].isOn
        end
        cell:InitSortOptionTemplate(self.m_selectOptions[index].name, self.m_selectStates[index])

        cell.view.btn.onClick:RemoveAllListeners()
        cell.view.btn.onClick:AddListener(function()
            local isOn = not self.m_selectStates[index]
            cell:SetSelectState(isOn)
            self.m_selectStates[index] = isOn

            if self.m_onSelectToggle ~= nil then
                local selectResultCount = self.m_onSelectToggle(index, isOn, self.m_selectStates)
                if selectResultCount ~= nil then

                end
            end
        end)
    end)
    self.view.selectNaviGroup:NaviToThisGroup()
end

SortPopOutCtrl.RefreshPanelState = HL.Method() << function(self)

    if(self.m_canSort) then
        self.view.sortNode.gameObject:SetActiveIfNecessary(true)
        self.view.selectNode.gameObject:SetActiveIfNecessary(false)
        return
    end

    if(self.m_canSelect) then
        self.view.sortNode.gameObject:SetActiveIfNecessary(false)
        self.view.selectNode.gameObject:SetActiveIfNecessary(true)
        return
    end
end











HL.Commit(SortPopOutCtrl)
