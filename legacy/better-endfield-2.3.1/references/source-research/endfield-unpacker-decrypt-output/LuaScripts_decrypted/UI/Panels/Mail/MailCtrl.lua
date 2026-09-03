local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Mail
MailCtrl = HL.Class('MailCtrl', uiCtrl.UICtrl)

local ASYNC_TEXTINFO_LINKINFO = 0.1

local MAIL_CELL_PREFAB_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Mail/%s_MailCellBg.prefab"
local MAIL_TITLE_PNG_PATH = "Mail/DynamicBg/%s_mailtitlebg"
local MAIL_CONTENT_PNG_PATH = "Mail/DynamicBg/%s_mailcontentbg"





MailCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ALL_MAIL_INITED] = 'OnAllMailInited',
    [MessageConst.ON_READ_MAIL] = 'OnReadMail',
    [MessageConst.ON_GET_MAIL_ATTACHMENT] = 'OnGetMailAttachment',
    [MessageConst.ON_DEL_MAILS] = 'OnDelMails',
    [MessageConst.ON_GET_NEW_MAILS] = 'OnGetNewMails',
    [MessageConst.ON_MARK_STAR_MAIL] = 'OnStarMail',

    [MessageConst.ON_GET_LOST_AND_FOUND] = 'OnGetLostAndFound',
    [MessageConst.ON_ADD_LOST_AND_FOUND] = 'OnAddLostAndFound'
}




MailCtrl.m_inLoading = HL.Field(HL.Boolean) << false

MailCtrl.m_getMailCell = HL.Field(HL.Function)

MailCtrl.m_curMails = HL.Field(HL.Table)

MailCtrl.m_curMailIndex = HL.Field(HL.Number) << 1

MailCtrl.m_curMailId = HL.Field(HL.Number) << -1

MailCtrl.m_naviToHyperLinkBindingId = HL.Field(HL.Number) << -1

MailCtrl.m_skipClickOnce = HL.Field(HL.Boolean) << false

MailCtrl.m_waitingForNavi = HL.Field(HL.Boolean) << true





MailCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:_OnClickClose()
    end)
    self:BindInputPlayerAction("common_open_mail", function()
        self:_OnClickClose()
    end)
    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "mail")
    end)
    self.view.getBtn.onClick:AddListener(function()
        self:_OnClickGet()
    end)
    self.view.delBtn.onClick:AddListener(function()
        self:_OnClickDel()
    end)
    self.view.getAllBtn.onClick:AddListener(function()
        self:_OnClickGetAll()
    end)
    self.view.delReadBtn.onClick:AddListener(function()
        self:_OnClickDelAll()
    end)
    self.view.lostAndFoundBtn.onClick:AddListener(function()
        self:_OnClickLostAndFound()
    end)

    self.view.contentTxt.onClickLink:AddListener(function(value)
        self:_OnClickLink(value)
    end)

    self.m_getMailCell = UIUtils.genCachedCellFunction(self.view.mailList)
    self.view.mailList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getMailCell(obj), LuaIndex(csIndex))
    end)

    self.view.filterBtn.view.normalBtn.onClick:AddListener(function()
        self:_OnClickStar()
    end)
    self.view.filterBtn.view.selectedBtn.onClick:AddListener(function()
        self:_OnClickStar()
    end)

    self.view.monthlyPassBtnNode.buyBtn.onClick:AddListener(function()
        self:_OnClickMonthlyPassBtn()
    end)

    self.view.lostAndFoundRedDot:InitRedDot("LostAndFoundBtn")
    self.view.getAllBtnRedDot:InitRedDot("MailTabGetAllBtn")

    local mailSys = GameInstance.player.mail
    if mailSys:IsAllMailInited() then
        self:_OnLoadingFinished()
    else
        self:_ToggleLoading(true)
        mailSys:GetAllMails()
    end

    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_CheckExpiredMail()
        end
    end)

    self.m_naviToHyperLinkBindingId = InputManagerInst:CreateBindingByActionId("mail_focus_hyperlink", function()
        self:_NaviToHyperLink()
    end, self.view.inputGroup.groupId)
    InputManagerInst:ToggleBinding(self.m_naviToHyperLinkBindingId, false)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self:_RecoverByArgs(arg)
end

MailCtrl.OnShow = HL.Override() << function(self)
    self:RefreshLostAndFoundBtn()
    self:_RefreshMailListSelectedAnim()
end

MailCtrl.OnGetLostAndFound = HL.Method(HL.Table) << function(self, args)
    self:RefreshLostAndFoundBtn()
end

MailCtrl.OnAddLostAndFound = HL.Method() << function(self)
    self:RefreshLostAndFoundBtn()
end

MailCtrl.RefreshLostAndFoundBtn = HL.Method() << function(self)
    local lostAndFound = GameInstance.player.inventory.lostAndFound
    local isEmpty = lostAndFound:IsEmpty()
    if isEmpty then
        self.view.lostAndFoundBtn.gameObject:SetActive(false)
    else
        self.view.lostAndFoundBtn.gameObject:SetActive(true)
        self.view.lostAndFoundNum.text = tostring(lostAndFound:GetUsedGridCount())
        self.view.lostAndFoundMax.text = tostring(Const.MAX_LOST_AND_FOUND_COUNT)
    end
end

MailCtrl._OnClickClose = HL.Method() << function(self)
    self.view.mailContentNode:ClearTween()
    PhaseManager:PopPhase(PhaseId.Mail)
end

MailCtrl.OnAnimationInFinished = HL.Override() << function(self)
end




MailCtrl._OnLoadingFinished = HL.Method() << function(self)
    self:_ToggleLoading(false)
    self:_OnClickTab(nil, nil)
end

MailCtrl._OnClickTab = HL.Method(HL.Opt(HL.Number, HL.Any)) << function(self, mailIndex, mailId)
    self.m_curMails = {}
    local mailSys = GameInstance.player.mail
    for _, mail in pairs(mailSys.mails) do
        if mail.inited and not mail.isExpired then
            local info = { mail = mail, }
            setmetatable(info, { __index = mail })
            info.starOrder = mail.isStar and 0 or 1
            info.readOrder = (mail.isRead and mail.collected) and 1 or 0
            info.sendTimeOrder = -mail.sendTime

            if info.itemList then
                info.items = {}
                local originiumItem
                for _, v in pairs(info.itemList) do
                    local data = Tables.itemTable[v.id]
                    if data.id == Tables.globalConst.originiumItemId then

                        if not originiumItem then
                            originiumItem = {
                                id = v.id,
                                count = v.count,
                                sortId1 = data.sortId1,
                                sortId2 = data.sortId2,
                                rarity = data.rarity,
                            }
                            table.insert(info.items, originiumItem)
                        else
                            originiumItem.count = originiumItem.count + v.count
                        end
                    else
                        table.insert(info.items, {
                            id = v.id,
                            count = v.count,
                            sortId1 = data.sortId1,
                            sortId2 = data.sortId2,
                            rarity = data.rarity,
                        })
                    end

                end
                table.sort(info.items, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
            end
            table.insert(self.m_curMails, info)
        end
    end

    table.sort(self.m_curMails, Utils.genSortFunction({ "starOrder", "readOrder", "sendTimeOrder", "title" }, true))

    local count = #self.m_curMails
    local isEmpty = count == 0
    local isFull = count >= Const.MAX_MAIL_COUNT
    local countTxt = string.format("<color=%s>%d</color>/%d", isFull and "red" or "white", count, Const.MAX_MAIL_COUNT)
    self.view.listCountText.text = countTxt
    if isEmpty then
        self.view.mailContentNode.gameObject:SetActive(false)
        self.view.mailListNode.gameObject:SetActive(false)
        self.view.emptyNode.gameObject:SetActive(true)
        self.m_curMailId = -1
    else
        self.view.mailContentNode.gameObject:SetActive(true)
        self.view.mailListNode.gameObject:SetActive(true)
        self.view.emptyNode.gameObject:SetActive(false)
        self.view.listTitleTxt.text = Language.LUA_MAIL_TAB_TITLE

        if mailId then
            for k, v in ipairs(self.m_curMails) do
                if v.id == mailId then
                    mailIndex = k
                    break
                end
            end
        else
            if mailIndex then
                mailIndex = lume.clamp(mailIndex, 1, count)
            else
                mailIndex = 1
            end
        end
        self.m_curMailId = self.m_curMails[mailIndex].id

        if self.view.rewardItems.view.rewardListNaviGroup.IsTopLayer then
            InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.rewardItems.view.rewardListNaviGroup)
        end

        self.m_waitingForNavi = true
        self.view.mailList:UpdateCount(count)
        self:_OnClickMail(mailIndex)
    end
end





MailCtrl.m_cacheMailCellBg = HL.Field(HL.Table)

MailCtrl._GetMailSkinPrefix = HL.Method(HL.Table, HL.Boolean).Return(HL.String) << function(self, info, upper)
    local prefix = ""
    if info.mail.subType == GEnums.MailSubType.Birthday then
        prefix = "birthday"
    else
        prefix = "default"
    end
    if upper then
        prefix = string.upperFirst(prefix)
    end
    return prefix
end

MailCtrl._GetMailCellBgPrefab = HL.Method(HL.Table).Return(CS.UnityEngine.GameObject) << function(self, info)
    if self.m_cacheMailCellBg == nil then
        self.m_cacheMailCellBg = {}
    end

    local prefix = self:_GetMailSkinPrefix(info, true)
    if self.m_cacheMailCellBg[prefix] == nil then
        self.m_cacheMailCellBg[prefix] = self:LoadGameObject(string.format(MAIL_CELL_PREFAB_PATH, prefix))
    end
    return self.m_cacheMailCellBg[prefix]
end






MailCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local info = self.m_curMails[index]

    cell.title.text = info.mail.title
    cell.senderIcon:LoadSprite(info.senderIcon)

    do
        if cell.dynamicBgAnimMap == nil then
            cell.dynamicBgAnimMap = {}
        end
        local bgName = self:_GetMailSkinPrefix(info, true)
        if cell.dynamicBgAnimMap[bgName] == nil then
            local prefab = self:_GetMailCellBgPrefab(info)
            local obj = CSUtils.CreateObject(prefab, cell.dynamicBg)
            cell.dynamicBgAnimMap[bgName] = obj:GetComponent("UIAnimationWrapper")
        end

        if cell.curDynamicBgName ~= bgName then
            if cell.curDynamicBgName ~= nil and cell.dynamicBgAnimMap[cell.curDynamicBgName] ~= nil then
                cell.dynamicBgAnimMap[cell.curDynamicBgName].gameObject:SetActive(false)
            end
            cell.curDynamicBgName = bgName
            cell.dynamicBgAnimMap[cell.curDynamicBgName].gameObject:SetActive(true)
        end
    end

    if DeviceInfo.usingController then
        cell.button.onIsNaviTargetChanged = function(isTarget, isGroupChanged)
            if isTarget and not isGroupChanged then
                if self.m_skipClickOnce then
                    self.m_skipClickOnce = false
                else
                    self:_OnClickMail(index)
                end
            end
        end
        if self.m_waitingForNavi and info.id == self.m_curMailId then
            self.m_waitingForNavi = false
            self:SetNaviTarget(cell.button)
        end
    else
        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            self:_OnClickMail(index)
        end)
    end

    if info.items then
        cell.item:InitItem(info.items[1])
        cell.item.view.rewardedCover.gameObject:SetActive(info.collected)
        cell.item.gameObject:SetActive(true)
    else
        cell.item.gameObject:SetActive(false)
    end

    if info.expireTime > 0 then
        cell.expireTime.gameObject:SetActive(true)
        cell.expireTime.text = string.format(Language.LUA_MAIL_CELL_EXPIRED_TIME_FORMAT,
            UIUtils.getLeftTime(info.expireTime - DateTimeUtils.GetCurrentTimestampBySeconds()))
    else
        cell.expireTime.gameObject:SetActive(false)
    end

    if info.isStar then
        cell.star.gameObject:SetActive(true)
    else
        cell.star.gameObject:SetActive(false)
    end

    if info.id == self.m_curMailId then
        cell.dynamicBgAnimMap[cell.curDynamicBgName]:PlayInAnimation()
    else
        cell.dynamicBgAnimMap[cell.curDynamicBgName]:PlayOutAnimation()
    end
    cell.animator:SetBool("IsSelected", info.id == self.m_curMailId)
    cell.senderAvatar:SetState(info.id == self.m_curMailId and "Select" or "UnSelect")
    cell.readMask.gameObject:SetActive(info.isRead and info.collected)

    cell.redDot:InitRedDot("SingleMail", info.id)

    cell.gameObject.name = "MailCell-" .. index
end

MailCtrl._RefreshMailListSelectedAnim = HL.Method() << function(self)
    self.m_waitingForNavi = true
    self.view.mailList:UpdateShowingCells(function(index, obj)
        local cell = self.m_getMailCell(obj)
        local info = self.m_curMails[LuaIndex(index)]
        if cell ~= nil and info ~= nil then
            if info.id == self.m_curMailId then
                cell.dynamicBgAnimMap[cell.curDynamicBgName]:PlayInAnimation()
            else
                cell.dynamicBgAnimMap[cell.curDynamicBgName]:PlayOutAnimation()
            end
            cell.animator:SetBool("IsSelected", info.id == self.m_curMailId)
            cell.senderAvatar:SetState(info.id == self.m_curMailId and "Select" or "UnSelect")
            if DeviceInfo.usingController then
                if self.m_waitingForNavi and info.id == self.m_curMailId then
                    self.m_waitingForNavi = false
                    self:SetNaviTarget(cell.button)
                end
            end
        end
    end)
end

MailCtrl._OnClickMail = HL.Method(HL.Number) << function(self, index)
    if self.m_curMailIndex ~= index then
        local oldCell = self.m_getMailCell(self.m_curMailIndex)
        if oldCell then
            oldCell.dynamicBgAnimMap[oldCell.curDynamicBgName]:PlayOutAnimation()
            oldCell.animator:SetBool("IsSelected", false)
            oldCell.senderAvatar:SetState("UnSelect")
        end
    end

    self.m_curMailIndex = index
    self.m_curMailId = self.m_curMails[index].id
    local cell = self.m_getMailCell(index)
    if cell then
        cell.dynamicBgAnimMap[cell.curDynamicBgName]:PlayInAnimation()
        cell.animator:SetBool("IsSelected", true)
        cell.senderAvatar:SetState("Select")
    end
    self:_ShowContent(index)
end

MailCtrl._ShowContent = HL.Method(HL.Number) << function(self, index)
    local info = self.m_curMails[index]

    self.view.mailName.text = info.mail.title
    self.view.sendTimeTxt.text = os.date("!" .. Language.LUA_MAIL_SEND_TIME_FORMAT, info.sendTime + Utils.getClientTimeZoneOffsetSeconds())
    self.view.senderNameTxt.text = string.format(Language.LUA_MAIL_SENDER_FORMAT, info.senderName)

    do
        local prefix = self:_GetMailSkinPrefix(info, false)
        self.view.contentNodeState:SetState(prefix)
    end

    local analyzedContentInfo = MailUtils.AnalyzeMailContent(info.mail)
    self.view.contentTxt:SetAndResolveTextStyle(analyzedContentInfo.content)
    self.view.contentTxt:ShrinkLinkTags()
    if DeviceInfo.usingController then
        coroutine.start(function()
            coroutine.wait(ASYNC_TEXTINFO_LINKINFO)
            InputManagerInst:ToggleBinding(self.m_naviToHyperLinkBindingId, self.view.contentTxt.textInfo.linkCount > 0)
        end)
    end

    if info.expireTime > 0 then
        self.view.expireTxt.gameObject:SetActive(true)
        self.view.expireTxt.text = string.format(Language.LUA_MAIL_CELL_EXPIRED_TIME_FORMAT,
            UIUtils.getLeftTime(info.expireTime - DateTimeUtils.GetCurrentTimestampByMilliseconds() / 1000))
    else
        self.view.expireTxt.gameObject:SetActive(false)
    end

    if info.items then
        self.view.rewardItems:InitRewardItems(info.items, info.collected, {
            enableItemHoverTips = false
        })
        self.view.rewardsNode.gameObject:SetActive(true)
        self.view.getBtn.gameObject:SetActive(not info.collected)
        self.view.delBtn.gameObject:SetActive(info.collected)
    else
        self.view.rewardsNode.gameObject:SetActive(false)
        self.view.getBtn.gameObject:SetActive(false)
        self.view.delBtn.gameObject:SetActive(true)
    end

    if info.isStar then
        self.view.filterBtn.view.normalBtn.gameObject:SetActive(false)
        self.view.filterBtn.view.selectedBtn.gameObject:SetActive(true)
    else
        self.view.filterBtn.view.normalBtn.gameObject:SetActive(true)
        self.view.filterBtn.view.selectedBtn.gameObject:SetActive(false)
    end


    self.view.monthlyPassBtnNode.gameObject:SetActive(info.mail.subType == GEnums.MailSubType.MonthlyPassExpired)
    self:_RefreshContentGachaPoolNode(info.mail, analyzedContentInfo.specialParamTable)
    self:_RefreshContentGachaWeaponPoolNode(info.mail, analyzedContentInfo.specialParamTable)

    if not info.isRead then
        GameInstance.player.mail:ReadMail(info.id)
    end

    self.view.mailContentNode:ClearTween()
    self.view.mailContentNode:PlayInAnimation()
end

MailCtrl._RefreshContentGachaPoolNode = HL.Method(CS.Beyond.Gameplay.MailSystem.Mail, HL.Any) << function(self, mail, specialParamTable)
    local gachaPoolNode = self.view.gachaPoolNode
    gachaPoolNode.jumpGachaPoolBtn.onClick:RemoveAllListeners()
    if not specialParamTable or not specialParamTable[MailUtils.specialParamKey.GachaLTTicket] then
        gachaPoolNode.gameObject:SetActive(false)
        return
    end
    local isGachaLTTicket = mail.subType == GEnums.MailSubType.GachaLTTicket
    if not isGachaLTTicket then
        gachaPoolNode.gameObject:SetActive(false)
        return
    end

    local info = specialParamTable[MailUtils.specialParamKey.GachaLTTicket][1]
    local ticketId = info.itemId
    local hasCfg, cfg = Tables.gachaLtTicket2PoolTable:TryGetValue(ticketId)
    if not hasCfg then
        logger.error(string.format("邮件类型为【%s】但卡池表中找不到itemId为【%s】的限时抽卡券", GEnums.MailSubType.GachaLTTicket, ticketId))
        gachaPoolNode.gameObject:SetActive(false)
        return
    end

    gachaPoolNode.gameObject:SetActive(true)
    gachaPoolNode.itemIcon:InitItemIcon(ticketId)
    local openedPoolId
    for i, poolId in pairs(cfg.poolIdList) do
        local hasInfo, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(poolId)
        if hasInfo and poolInfo.isOpenValid then
            openedPoolId = poolId
            break
        end
    end

    local targetPoolId
    if openedPoolId then
        targetPoolId = openedPoolId
    else

        targetPoolId = cfg.poolIdList[0]
    end
    local _, poolCfg = Tables.gachaCharPoolTable:TryGetValue(targetPoolId)
    gachaPoolNode.bannerBgImg:LoadSprite(UIConst.UI_SPRITE_GACHA_POOL, poolCfg.mailBannerImage)
    gachaPoolNode.decoColorNode.color = UIUtils.getColorByString(poolCfg.tabGradientColor, gachaPoolNode.decoColorNode.color.a * 255)

    gachaPoolNode.jumpGachaPoolBtn.onClick:AddListener(function()
        local hasInfo, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(targetPoolId)
        if hasInfo and poolInfo.isOpenValid then
            PhaseManager:OpenPhase(PhaseId.GachaPool, { poolId = targetPoolId })
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_GACHA_MAIL_JUMP_FAIL)
        end
    end)
end

MailCtrl._RefreshContentGachaWeaponPoolNode = HL.Method(CS.Beyond.Gameplay.MailSystem.Mail, HL.Any) << function(self, mail, specialParamTable)
    local gachaPoolNode = self.view.gachaWeaponPoolNode
    gachaPoolNode.jumpGachaPoolBtn.onClick:RemoveAllListeners()
    if not specialParamTable then
        gachaPoolNode.gameObject:SetActive(false)
        return
    end
    local isGachaLTTicket = mail.subType == GEnums.MailSubType.GachaWeaponLTTicket
    if not isGachaLTTicket then
        gachaPoolNode.gameObject:SetActive(false)
        return
    end

    local info = specialParamTable[MailUtils.specialParamKey.GachaWeaponLTTicket][1]
    local ticketId = info.itemId
    local hasCfg, cfg = Tables.gachaWeaponLtTicket2PoolTable:TryGetValue(ticketId)
    if not hasCfg then
        logger.error(string.format("邮件类型为【%s】但卡池表中找不到itemId为【%s】的限时抽卡券", GEnums.MailSubType.GachaWeaponLTTicket, ticketId))
        gachaPoolNode.gameObject:SetActive(false)
        return
    end

    gachaPoolNode.gameObject:SetActive(true)
    gachaPoolNode.itemIcon:InitItemIcon(ticketId)
    local targetPoolId
    local targetJumpId
    for i, singlePoolCfg in pairs(cfg.poolDataList) do
        local hasInfo, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(singlePoolCfg.gachaPoolId)
        if hasInfo and poolInfo.isOpenValid then
            targetPoolId = singlePoolCfg.gachaPoolId
            targetJumpId = singlePoolCfg.mailJumpId
            break
        end
    end

    gachaPoolNode.jumpGachaPoolBtn.onClick:AddListener(function()
        local hasInfo, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(targetPoolId)
        if hasInfo and poolInfo.isOpenValid then
            Utils.jumpToSystem(targetJumpId)
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_GACHA_MAIL_JUMP_FAIL)
        end
    end)
end






MailCtrl._OnClickGet = HL.Method() << function(self)
    local canPutInItemBagAndFactoryDepot = true
    local canPutInValuableDepot = true

    local info = self.m_curMails[self.m_curMailIndex]
    local scope = Utils.getCurrentScope()
    local inventory = GameInstance.player.inventory
    for i, item in ipairs(info.items) do
        local storageSpace = inventory:TryGetItemStorageSpace(item.id)
        if storageSpace == GEnums.ItemStorageSpace.BagAndFactoryDepot then
            if not inventory:CanItemBagAndFactoryDepotPutInItem(scope, item.id, item.count) then
                canPutInItemBagAndFactoryDepot = false
            end
        elseif storageSpace == GEnums.ItemStorageSpace.ValuableDepot then
            if not inventory:CanValuableDepotPutInItem(scope, item.id, item.count) then
                canPutInValuableDepot = false
                break
            end
        end
    end

    if canPutInValuableDepot then

        if canPutInItemBagAndFactoryDepot then

            GameInstance.player.mail:GetMailAttachment(info.id)
            AudioAdapter.PostEvent("au_ui_g_confirm_button_get_mail")
        else

            Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_MAIL_HINT_GET_ITEM_FACTORY_DEPOT_FULL,
                onConfirm = function()

                    GameInstance.player.mail:GetMailAttachment(info.id)
                    AudioAdapter.PostEvent("au_ui_g_confirm_button_get_mail")
                end
            })
        end
    else

        Notify(MessageConst.SHOW_TOAST, Language.LUA_MAIL_HINT_GET_ITEM_VALUABLE_DEPOT_FULL)
    end
end

MailCtrl._OnClickGetAll = HL.Method() << function(self)
    for _, v in pairs(self.m_curMails) do
        if not v.collected then
            GameInstance.player.mail:GetAllMailAttachments()
            return
        end
    end
    Notify(MessageConst.SHOW_TOAST, Language.LUA_MAIL_NO_ATTACHMENT)
    AudioAdapter.PostEvent("au_ui_g_confirm_button_get_all_mail")
end

MailCtrl._OnClickDel = HL.Method() << function(self)
    local info = self.m_curMails[self.m_curMailIndex]
    if info.items and not info.collected then

        return
    end
    local hint
    if info.isStar then
        hint = Language.LUA_MAIL_HINT_DEL_STAR
    else
        hint = Language.LUA_MAIL_HINT_DEL_NORMAL
    end
    Notify(MessageConst.SHOW_POP_UP, {
        content = hint,
        ignoreClickWhenPlayingAnimation = true,
        onConfirm = function()
            GameInstance.player.mail:DelMail(info.id)
            AudioAdapter.PostEvent("au_ui_mail_delete")
        end
    })
    self.m_skipClickOnce = true
end

MailCtrl._OnClickDelAll = HL.Method() << function(self)
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_MAIL_HINT_DEL_ALL,
        onConfirm = function()
            local canDelAll = false
            for i, mail in ipairs(self.m_curMails) do
                if mail.isRead and not mail.isStar and not (mail.items and not mail.collected) then

                    canDelAll = true
                    break
                end
            end
            if not canDelAll then
                return
            end
            GameInstance.player.mail:DelAllMails()
            AudioAdapter.PostEvent("au_ui_mail_delete")
        end
    })
end

MailCtrl._OnClickLostAndFound = HL.Method() << function(self)
    PhaseManager:OpenPhase(PhaseId.LostAndFound)
    self.m_skipClickOnce = true
end

MailCtrl._OnClickStar = HL.Method() << function(self)
    local info = self.m_curMails[self.m_curMailIndex]
    GameInstance.player.mail:StarMail(info.id, not info.isStar)
end

MailCtrl._OnClickMonthlyPassBtn = HL.Method() << function(self)
    PhaseManager:OpenPhase(PhaseId.CashShop, {
        shopGroupId = CashShopConst.CashShopCategoryType.Pack,
        cashShopId = "MCard",
    })
end






MailCtrl.OnAllMailInited = HL.Method() << function(self)
    if self.m_inLoading then
        self:_OnLoadingFinished()
    else
        local curId
        if self.m_curMailId ~= -1 then
            curId = self.m_curMailId
        end
        self:_OnClickTab(nil, curId)
    end
end

MailCtrl.OnReadMail = HL.Method(HL.Table) << function(self, args)
    local id = unpack(args)
    for k, v in ipairs(self.m_curMails) do
        if v.id == id then
            local cell = self.m_getMailCell(k)
            if cell then
                self:_OnUpdateCell(cell, k)
            end
            return
        end
    end
end

MailCtrl.OnGetMailAttachment = HL.Method(HL.Table) << function(self, args)

    local hasSuccess, hasFailed, rewardPack = unpack(args)
    if hasSuccess then

        self:_OnClickTab(self.m_curMailIndex, nil)
    end
    if hasFailed then

        Notify(MessageConst.SHOW_TOAST, Language.LUA_MAIL_HINT_GET_ITEM_PARTIALLY_FAILED_INVENTORY_FULL)
    end
    if rewardPack then
        local items = rewardPack.itemBundleList
        if items.Count > 0 then

            Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
                title = Language.LUA_MAIL_GET_ITEM_SUCC_TITLE,
                icon = "icon_mail_obtain",
                items = items,
                chars = rewardPack.chars,
            })
        end
    end
end

MailCtrl.OnStarMail = HL.Method(HL.Table) << function(self, args)
    local id = unpack(args)
    for k, v in ipairs(self.m_curMails) do
        if v.id == id then
            local cell = self.m_getMailCell(k)
            if cell then
                self:_OnUpdateCell(cell, k)
            end

            if k == self.m_curMailIndex then
                if v.isStar then
                    self.view.filterBtn.view.normalBtn.gameObject:SetActive(false)
                    self.view.filterBtn.view.selectedBtn.gameObject:SetActive(true)
                else
                    self.view.filterBtn.view.normalBtn.gameObject:SetActive(true)
                    self.view.filterBtn.view.selectedBtn.gameObject:SetActive(false)
                end
            end
            return
        end
    end
end

MailCtrl.OnDelMails = HL.Method() << function(self)
    self:_OnClickTab(self.m_curMailIndex, nil)


    if self.m_curMails ~= nil and #self.m_curMails == 0 then
        AudioAdapter.PostEvent("Au_UI_Event_MailEmpty")
    end
end

MailCtrl.OnGetNewMails = HL.Method() << function(self)
    GameInstance.player.mail:GetAllMails()
end





MailCtrl._ToggleLoading = HL.Method(HL.Boolean) << function(self, active)
    self.m_inLoading = active
    self.view.contentNode.gameObject:SetActive(not active)
    self.view.loadingNode.gameObject:SetActive(active)
end

MailCtrl._OnClickLink = HL.Method(HL.String) << function(self, value)
    logger.info("_OnClickLink", value)
    CS.Beyond.UI.WebApplication.StartHGBrowser(value)
end

MailCtrl._CheckExpiredMail = HL.Method() << function(self)
    if not self.m_curMails then
        return
    end

    for _, v in ipairs(self.m_curMails) do
        if v.isExpired then
            self:_OnClickTab(nil, nil)
            return
        end
    end
end

MailCtrl._NaviToHyperLink = HL.Method() << function(self)
    local textInfo = self.view.contentTxt.textInfo
    local linkLength = textInfo.linkCount
    if linkLength > 0 then
        local actionMenuInfos = {}
        for luaIndex = 1, linkLength do
            local csIndex = CSIndex(luaIndex)
            local actionText = textInfo.linkInfo[csIndex]:GetLinkText()
            local _, linkId = self.view.contentTxt:TryGetLinkId(csIndex)
            table.insert(actionMenuInfos, {
                text = actionText,
                action = function()
                    self:_OnClickLink(linkId)
                end,
            })
        end
        self.m_skipClickOnce = true
        self.view.actionMenuNode.gameObject:SetActiveIfNecessary(true)
        Notify(MessageConst.SHOW_NAVI_TARGET_ACTION_MENU, {
            transform = self.view.actionMenuNode,
            actions = actionMenuInfos,
            onClose = function()
                self.view.actionMenuNode.gameObject:SetActiveIfNecessary(false)
            end,
            posType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
        })
    end
end

MailCtrl._RecoverByArgs = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    if not args then
        return
    end

    if args.curMailIndex and self.m_curMails and self.m_curMails[args.curMailIndex] then
        self.view.mailList:ScrollToIndex(CSIndex(args.curMailIndex), true)
        if DeviceInfo.usingController then
            local cell = self.m_getMailCell(args.curMailIndex)
            if cell then
                self:SetNaviTarget(cell.button)
            end
        else
            self:_OnClickMail(args.curMailIndex)
        end
    end

    if args.isMailInstructionShown then
        UIManager:Open(PanelId.InstructionBook, "mail")
    end
end

MailCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    local isMailInstructionShown = false
    if isOpen and instructionCtrl:IsShow() and instructionCtrl.id == "mail" then
        isMailInstructionShown = true
    end
    return {
        curMailIndex = self.m_curMailIndex,
        isMailInstructionShown = isMailInstructionShown,
    }
end



HL.Commit(MailCtrl)
