local RichContent = require_ex('UI/Widgets/RichContent')

PRTSRichContent = HL.Class('PRTSRichContent', RichContent)



PRTSRichContent.m_genGotoBtnCells = HL.Field(HL.Forward("UIListCache"))

PRTSRichContent.m_gotoBtnCallback = HL.Field(HL.Function)

PRTSRichContent.m_gotoBtnNameList = HL.Field(HL.Table)




PRTSRichContent._OnFirstTimeInit = HL.Override() << function(self)
    self.m_genGotoBtnCells = UIUtils.genCellCache(self.view.gotoBtnCell)
end

PRTSRichContent.InitPRTSRichContent = HL.Method(HL.String) << function(self, contentId)
    self:_FirstTimeInit()
    self:SetContentById(contentId)
    self.view.gotoBtnListState:SetState("Hide")
    InputManagerInst:ToggleGroup(self.view.gotoBtnListInputGroup.groupId, false)
    self.view.scrollList:ScrollTo(Vector2.up, true)
end

PRTSRichContent.SetGotoBtn = HL.Method(HL.Table, HL.Function) << function(self, btnNameList, gotoBtnCallback)
    local count = #btnNameList
    InputManagerInst:ToggleGroup(self.view.gotoBtnListInputGroup.groupId, count > 0)
    if count <= 0 then
        self.view.gotoBtnListState:SetState("Hide")
        return
    end
    self.m_gotoBtnCallback = gotoBtnCallback
    self.m_gotoBtnNameList = btnNameList
    self.view.gotoBtnListState:SetState("Show")

    self.m_genGotoBtnCells:Refresh(count, function(cell, luaIndex)
        cell.nameTxt.text = self.m_gotoBtnNameList[luaIndex]
        cell.gotoBtn.onClick:RemoveAllListeners()
        cell.gotoBtn.onClick:AddListener(function()
            self:_OnClickGotoBtn(luaIndex)
        end)
    end)
end

PRTSRichContent._OnClickGotoBtn = HL.Method(HL.Number) << function(self, luaIndex)
    if self.m_gotoBtnCallback then
        self.m_gotoBtnCallback(luaIndex)
    end
end

HL.Commit(PRTSRichContent)
return PRTSRichContent

