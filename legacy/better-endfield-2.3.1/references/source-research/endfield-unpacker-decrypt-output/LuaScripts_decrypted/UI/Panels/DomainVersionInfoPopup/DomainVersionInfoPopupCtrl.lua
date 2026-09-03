local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainVersionInfoPopup

local domainSystem = GameInstance.player.domainDevelopmentSystem

DomainVersionInfoPopupCtrl = HL.Class('DomainVersionInfoPopupCtrl', uiCtrl.UICtrl)






DomainVersionInfoPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


DomainVersionInfoPopupCtrl.m_info = HL.Field(HL.Table)

DomainVersionInfoPopupCtrl.m_focusItemKeyHintHasInit = HL.Field(HL.Boolean) << false

DomainVersionInfoPopupCtrl.m_closeCallback = HL.Field(HL.Function)




DomainVersionInfoPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()

    local domainId = arg.domainId
    self.m_closeCallback = arg.onClose
    if string.isEmpty(domainId) then
        logger.error("参数错误！domainId为空")
        return
    end
    self.m_info = DomainPOIUtils.tryGetDomainNewVersionInfo(domainId, arg.startVersionData, arg.endVersionData)
    if self.m_info == nil then
        logger.error("info为空!!")
        return
    end

    self:_RefreshAllUI()
end

DomainVersionInfoPopupCtrl.OnClose = HL.Override() << function(self)
    if self.m_closeCallback then
        self.m_closeCallback()
        self.m_closeCallback = nil
    end
end



DomainVersionInfoPopupCtrl._InitUI = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.baseplateCloseBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    local contentParent = self.view.content
    contentParent.versionLevelTitleCell.gameObject:SetActive(false)
    contentParent.versionPOITitleCell.gameObject:SetActive(false)
    contentParent.versionTextCell.gameObject:SetActive(false)
    contentParent.versionRewardListCell.gameObject:SetActive(false)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

DomainVersionInfoPopupCtrl._RefreshAllUI = HL.Method() << function(self)
    self:_RefreshBasicUI()
    self:_RefreshVersionContentUI()
end

DomainVersionInfoPopupCtrl._RefreshBasicUI = HL.Method() << function(self)
    local info = self.m_info
    self.view.subTitleDomainIcon:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT, info.domainIcon)
    self.view.subTitleTxt.text = string.format(Language.LUA_DOMAIN_VERSION_DEVELOPMENT_MAX_LEVEL_DIFF, info.domainName, info.domainCurMaxLv)
end

DomainVersionInfoPopupCtrl._RefreshVersionContentUI = HL.Method() << function(self)

    for _, poiVersionInfo in pairs(self.m_info.poiVersionInfoList) do
        self:_RefreshVersionPOITitleCell(poiVersionInfo)
        for _, levelVersionInfo in pairs(poiVersionInfo.levelVersionInfoList) do
            self:_RefreshVersionLevelTitleCell(levelVersionInfo)
            self:_RefreshVersionTextCell(levelVersionInfo)
            if levelVersionInfo.rewardList then
                self:_RefreshVersionRewardListCell(levelVersionInfo)
            end
        end
    end

    InputManagerInst:ToggleBinding(self.view.poiVersionList.FocusBindingId, self.m_focusItemKeyHintHasInit)
end


DomainVersionInfoPopupCtrl._RefreshVersionPOITitleCell = HL.Method(HL.Table) << function(self, info)
    local contentParent = self.view.content

    local cell = DomainVersionInfoPopupCtrl._GenCacheContent(contentParent.versionPOITitleCell.gameObject, contentParent.gameObject)
    cell.titleIcon:LoadSprite(UIConst.UI_SPRITE_DOMAIN, info.poiIcon)
    cell.titleTxt.text = info.poiName
end

DomainVersionInfoPopupCtrl._RefreshVersionLevelTitleCell = HL.Method(HL.Table) << function(self, info)
    local contentParent = self.view.content

    local cell = DomainVersionInfoPopupCtrl._GenCacheContent(contentParent.versionLevelTitleCell.gameObject, contentParent.gameObject)
    cell.poiNameTxt.text = info.levelPoiName
    cell.levelNameTxt.text = info.levelName
end

DomainVersionInfoPopupCtrl._RefreshVersionTextCell = HL.Method(HL.Table) << function(self, info)
    if info.poiCurVersionMaxLv < 0 then
        return
    end
    local contentParent = self.view.content

    local cell = DomainVersionInfoPopupCtrl._GenCacheContent(contentParent.versionTextCell.gameObject, contentParent.gameObject)
    cell.descTxt.text = string.format(Language.LUA_DOMAIN_VERSION_POI_MAX_LEVEL_DIFF, info.poiCurVersionMaxLv)
end

DomainVersionInfoPopupCtrl._RefreshVersionRewardListCell = HL.Method(HL.Table) << function(self, info)
    if #info.rewardList <= 0 then
        return
    end
    local contentParent = self.view.content

    local cell = DomainVersionInfoPopupCtrl._GenCacheContent(contentParent.versionRewardListCell.gameObject, contentParent.gameObject)
    cell.descTxt.text = Language.LUA_DOMAIN_VERSION_SHOP_NEW_TRADE_ITEM
    info.rewardCellCached = UIUtils.genCellCache(cell.rewardItemCell)

    info.rewardCellCached:Refresh(#info.rewardList, function(rewardCell, luaIndex)
        local itemBundle = info.rewardList[luaIndex]
        rewardCell:InitItem(itemBundle, function()
            UIUtils.showItemSideTips(rewardCell)
        end)
        rewardCell.view.count.gameObject:SetActive(false)
    end)
    local focusItemKeyHintTrans = cell.focusItemKeyHint
    if self.m_focusItemKeyHintHasInit or #info.rewardList <= 0 then
        focusItemKeyHintTrans.gameObject:SetActive(false)
    else
        self.m_focusItemKeyHintHasInit = true
        focusItemKeyHintTrans.gameObject:SetActive(true)
        local firstItemCell = info.rewardCellCached:Get(1)
        if firstItemCell then
            focusItemKeyHintTrans:SetParent(firstItemCell.transform)

            local keyHintPos = Vector3(-cell.focusItemKeyHint.transform.rect.width, 0, 0)
            focusItemKeyHintTrans.transform.localPosition = keyHintPos
        end
    end
end





DomainVersionInfoPopupCtrl._GenCacheContent = HL.StaticMethod(GameObject, GameObject).Return(HL.Table) << function(templateObj, parent)
    local child = UIUtils.addChild(parent, templateObj, true)
    child.gameObject:SetActive(true)
    return Utils.wrapLuaNode(child)
end


HL.Commit(DomainVersionInfoPopupCtrl)
