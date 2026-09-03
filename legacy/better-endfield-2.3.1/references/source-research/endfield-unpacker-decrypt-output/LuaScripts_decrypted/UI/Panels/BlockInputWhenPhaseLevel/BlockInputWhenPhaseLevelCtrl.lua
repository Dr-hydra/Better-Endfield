
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BlockInputWhenPhaseLevel

BlockInputWhenPhaseLevelCtrl = HL.Class('BlockInputWhenPhaseLevelCtrl', uiCtrl.UICtrl)






BlockInputWhenPhaseLevelCtrl.s_messages = HL.StaticField(HL.Table) << {

}

BlockInputWhenPhaseLevelCtrl.s_blockInfos = HL.StaticField(HL.Table)

BlockInputWhenPhaseLevelCtrl.ToggleBlockInputWhenPhaseLevel = HL.StaticMethod(HL.Table) << function(args)
    local key, isBlock = unpack(args)
    if not BlockInputWhenPhaseLevelCtrl.s_blockInfos then
        BlockInputWhenPhaseLevelCtrl.s_blockInfos = {}
    end
    if isBlock then
        BlockInputWhenPhaseLevelCtrl.s_blockInfos[key] = true
    else
        BlockInputWhenPhaseLevelCtrl.s_blockInfos[key] = nil
    end
    BlockInputWhenPhaseLevelCtrl.UpdateState()
end

BlockInputWhenPhaseLevelCtrl.UpdateState = HL.StaticMethod() << function()
    local shouldBlock = PhaseManager:GetTopPhaseId() == PhaseId.Level and BlockInputWhenPhaseLevelCtrl.s_blockInfos and next(BlockInputWhenPhaseLevelCtrl.s_blockInfos)
    logger.info("BlockInputWhenPhaseLevelCtrl.UpdateState", shouldBlock)
    local isOpen = UIManager:IsOpen(PANEL_ID)
    if shouldBlock then
        if isOpen then
            UIManager:Show(PANEL_ID)
        else
            UIManager:Open(PANEL_ID)
        end
    else
        if isOpen then
            UIManager:Hide(PANEL_ID)
        end
    end
end


HL.Commit(BlockInputWhenPhaseLevelCtrl)
