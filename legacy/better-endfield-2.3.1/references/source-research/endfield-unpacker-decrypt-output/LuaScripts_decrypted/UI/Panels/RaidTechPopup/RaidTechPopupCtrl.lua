
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RaidTechPopup

RaidTechPopupCtrl = HL.Class('RaidTechPopupCtrl', uiCtrl.UICtrl)






RaidTechPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}

RaidTechPopupCtrl.m_getCellFunc = HL.Field(HL.Function)


RaidTechPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.emptyState.gameObject:SetActive(GameInstance.player.weekRaidSystem.unlockedTechIds.Count == 0)

    self.view.btnClose.onClick:RemoveAllListeners()
    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    local techTypeData = {}

    for i = 0, GameInstance.player.weekRaidSystem.unlockedTechIds.Count - 1 do
        local techId = GameInstance.player.weekRaidSystem.unlockedTechIds[i]

        local _,cfg = Tables.weekRaidTechTable:TryGetValue(techId)

        if cfg then

            if WeeklyRaidUtils.TechUseStrValue(cfg) then
                if techTypeData[cfg.techType] == nil then
                    techTypeData[cfg.techType] = {}
                end
                table.insert(techTypeData[cfg.techType], cfg)
            else

                if techTypeData[cfg.techType] == nil then
                    techTypeData[cfg.techType] = cfg.numValue
                else
                    techTypeData[cfg.techType] = cfg.numValue + techTypeData[cfg.techType]
                end
            end
        end
    end

    local techData = {}
    for type,data in pairs(techTypeData) do
        if WeeklyRaidUtils.TechUseStrValue(type) then
            for _, item in ipairs(data) do
                table.insert(techData, {
                    type = type,
                    data = item,
                })
            end
        else
            table.insert(techData, {
                type = type,
                data = data,
            })
        end
    end


    table.sort(techData, function(a, b)

        local aSort = 0
        local bSort = 0
        if WeeklyRaidUtils.TechUseStrValue(a.type) then
            aSort = Tables.weekRaidBufTechTypeTable[a.data.strValue].sort
        else
            aSort = Tables.weekRaidTechTypeTable[a.type].sort
        end
        if WeeklyRaidUtils.TechUseStrValue(b.type) then
            bSort = Tables.weekRaidBufTechTypeTable[b.data.strValue].sort
        else
            bSort = Tables.weekRaidTechTypeTable[b.type].sort
        end
        return aSort < bSort
    end)

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.verticalList)
    self.view.verticalList.onUpdateCell:RemoveAllListeners()
    self.view.verticalList.onUpdateCell:AddListener(function(object, csIndex)
        local cell = self.m_getCellFunc(object)
        local data = techData[LuaIndex(csIndex)]
        local techTypeCfg = nil

        if WeeklyRaidUtils.TechUseStrValue(data.type) then
            techTypeCfg = Tables.weekRaidBufTechTypeTable[data.data.strValue]
        else
            techTypeCfg = Tables.weekRaidTechTypeTable[data.type]
        end

        cell.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, techTypeCfg.icon)
        cell.name.text = techTypeCfg.name
        cell.desc.text = WeeklyRaidUtils.GetTechShowString(techTypeCfg, data.data)
    end)
    self.view.verticalList:UpdateCount(#techData)

end











HL.Commit(RaidTechPopupCtrl)
