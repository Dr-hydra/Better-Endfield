
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WikiGuideTips

WikiGuideTipsCtrl = HL.Class('WikiGuideTipsCtrl', uiCtrl.UICtrl)

local WIKI_TIP_TYPE_NAMES = {
    MONSTER = "ui_wiki_common_eny",
    TUTORIAL = "ui_wiki_common_tut"
}





WikiGuideTipsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.HIDE_WIKI_REF_TIPS] = 'CloseTips',
    [MessageConst.ON_SYSTEM_DISPLAY_SIZE_CHANGED] = '_OnSystemDisplaySizeChanged',
}

WikiGuideTipsCtrl.m_wikiEntryId = HL.Field(HL.String) << ""

WikiGuideTipsCtrl.m_onClose = HL.Field(HL.Function)

WikiGuideTipsCtrl.m_key = HL.Field(HL.Any)

WikiGuideTipsCtrl.m_tipsTransform = HL.Field(HL.Userdata)


WikiGuideTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.view.wikiBtn.onClick:AddListener(function()
        self:ShowWiki()
    end)

    self.view.autoCloseArea.onTriggerAutoClose:AddListener(function()
        self:ClearOnClose()
    end)
end







WikiGuideTipsCtrl.ShowTips = HL.StaticMethod(HL.Table) << function(args)
    local self = UIManager:AutoOpen(PANEL_ID)
    self:_ShowTips(args)
end

WikiGuideTipsCtrl._ShowTips = HL.Method(HL.Table) << function(self, args)

    if not args.key then
        args.key = args.transform
    end
    if args.key == self.m_key then

        if not DeviceInfo.usingController then
            self.view.autoCloseArea:CloseSelf()
        end
        return
    end


    self:ClearOnClose()
    local wikiEntryId = args.wikiEntryId
    self.m_wikiEntryId = wikiEntryId
    self.m_onClose = args.onClose
    self.m_key = args.key
    self.m_tipsTransform = args.transform


    self.view.autoCloseArea.tmpSafeArea = args.transform



    local hasValue, wikiEntryData = Tables.wikiEntryDataTable:TryGetValue(wikiEntryId)
    if wikiEntryData.refMonsterTemplateId and not string.isEmpty(wikiEntryData.refMonsterTemplateId) then

        local monsterDisplayData = Tables.enemyTemplateDisplayInfoTable[wikiEntryData.refMonsterTemplateId]
        self:_ConfigMonsterDisplay(wikiEntryData, monsterDisplayData)
    else


        local refPage = nil

        local _, pages = Tables.wikiTutorialPageByEntryTable:TryGetValue(wikiEntryId)
        for _, pageId in pairs(pages.pageIds) do
            local _, pageData = Tables.wikiTutorialPageTable:TryGetValue(pageId)
            if pageData.order == 1 then
                refPage = pageData
                break
            end
        end
        self:_ConfigTutorialDisplay(wikiEntryData, refPage)
    end


    self.view.autoCloseArea:OpenSelf()
    self:_UpdateTipsPosition()
end

WikiGuideTipsCtrl._UpdateTipsPosition = HL.Method() << function(self)
    local padding = { bottom = 50 + (self:_IsFullScreen() and 50 or 0 ) }
    local notchSize = CS.Beyond.DeviceInfoManager.NotchPaddingInCanvas(self.view.transform).x
    padding.left = (padding.left or 0) + notchSize
    padding.right = (padding.right or 0) + notchSize

    UIUtils.updateTipsPosition(self.view.content, self.m_tipsTransform, self.view.rectTransform, self.uiCamera, nil, padding)
end

WikiGuideTipsCtrl._OnSystemDisplaySizeChanged = HL.Method() << function(self)
    self:_StartCoroutine(function()
        coroutine.step()
        coroutine.step()
        coroutine.step()
        self:_UpdateTipsPosition()
    end)
end

WikiGuideTipsCtrl._ConfigTutorialDisplay = HL.Method(HL.Userdata, HL.Userdata) << function(self, entryShowData, pageData)
    self.view.nameTxt.text = entryShowData.desc
    self.view.decoTxt:SetAndResolveTextStyle(InputManager.ParseTextActionId(pageData.content))
    self.view.itemTypeTxt.text = I18nUtils.GetText(WIKI_TIP_TYPE_NAMES.TUTORIAL)
end

WikiGuideTipsCtrl._ConfigMonsterDisplay = HL.Method(HL.Userdata, HL.Userdata) << function(self, entryShowData, monsterData)
    self.view.nameTxt.text = monsterData.name
    self.view.decoTxt:SetAndResolveTextStyle(monsterData.description)
    self.view.itemTypeTxt.text = I18nUtils.GetText(WIKI_TIP_TYPE_NAMES.MONSTER)
end


WikiGuideTipsCtrl.CloseTips = HL.Method() << function(self)
    if self.view.autoCloseArea.toggleObj.activeSelf then
        self.view.autoCloseArea:CloseSelf()
    end
end

WikiGuideTipsCtrl.ShowWiki = HL.Method() << function(self)
    Notify(MessageConst.SHOW_WIKI_ENTRY, { wikiEntryId = self.m_wikiEntryId })
    self.view.autoCloseArea:CloseSelf()
end


WikiGuideTipsCtrl.ClearOnClose = HL.Method() << function(self)
    if self.m_onClose then
        self.m_onClose()
    end
    self.m_onClose = nil
    self.m_key = nil
    self.m_wikiEntryId = ""
end

WikiGuideTipsCtrl._IsFullScreen = HL.Method().Return(HL.Boolean) << function(self)
    return InputManagerInst.usingController
end

WikiGuideTipsCtrl.OnHide = HL.Override() << function(self)
    self:ClearOnClose()
end
WikiGuideTipsCtrl.OnClose = HL.Override() << function(self)
    self:ClearOnClose()
end

HL.Commit(WikiGuideTipsCtrl)