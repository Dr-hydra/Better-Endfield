
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacTechPointGainedToast

local SHOWING_EFFECT_CLIP = "factechpoint_gained"

FacTechPointGainedToastCtrl = HL.Class('FacTechPointGainedToastCtrl', uiCtrl.UICtrl)

FacTechPointGainedToastCtrl.m_showingCor = HL.Field(HL.Thread)





FacTechPointGainedToastCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE] = 'InterruptMainHudActionQueue',
}

FacTechPointGainedToastCtrl.OnFacTechPointGained = HL.StaticMethod(HL.Any) << function(arg)
    LuaSystemManager.mainHudActionQueue:AddRequest("FacTechPointGained", function()

        local ctrl = UIManager:AutoOpen(PANEL_ID)
        ctrl:StartToast(arg, function()
            Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "FacTechPointGained")
        end)
    end)
end


FacTechPointGainedToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

end

FacTechPointGainedToastCtrl.StartToast = HL.Method(HL.Table, HL.Function) << function(self, arg, finishCb)
    local itemBundleList = unpack(arg)


    local facTechPointId = itemBundleList[0].id
    local gainedCount = 0
    for i = 0, itemBundleList.Count - 1 do
        gainedCount = gainedCount + itemBundleList[i].count
    end

    local costItemCfg = Tables.itemTable[facTechPointId]
    local pointName = costItemCfg.name
    local ownCostPoint = Utils.getItemCount(facTechPointId)

    self.view.pointNameTxt.text = pointName
    self.view.increasePointsTxt.text = string.format(Language.LUA_FAC_TECH_POINT_GAINED_NUMBER_FORMAT, gainedCount)
    self.view.previewPointTxt.text = ownCostPoint - gainedCount
    self.view.curPointTxt.text = ownCostPoint

    local wrapper = self.animationWrapper
    wrapper:Play(SHOWING_EFFECT_CLIP)
    self.m_showingCor = self:_StartCoroutine(function()
        local showingTime = wrapper:GetClipLength(SHOWING_EFFECT_CLIP)
        coroutine.wait(showingTime)
        if finishCb then
            finishCb()
        end
        self:Close()
    end)
    AudioAdapter.PostEvent("Au_UI_Toast_FacTechPointGainedToastPanel_Open")
end

FacTechPointGainedToastCtrl.InterruptMainHudActionQueue = HL.Method() << function(self)
    self.m_showingCor = self:_ClearCoroutine(self.m_showingCor)
    self:Close()
end


HL.Commit(FacTechPointGainedToastCtrl)
