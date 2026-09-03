



local phaseGameObjectItem = require_ex('Phase/PhaseItem/PhaseGameObjectItem')
PhaseCharItem = HL.Class("PhaseCharItem", phaseGameObjectItem.PhaseGameObjectItem)














PhaseCharItem.m_tmpViewData = HL.Field(HL.Table)

PhaseCharItem.uiModelMono = HL.Field(HL.Userdata)

PhaseCharItem.charId = HL.Field(HL.String) << ""

PhaseCharItem.charInstId = HL.Field(HL.Int) << -1

PhaseCharItem.animator = HL.Field(HL.Userdata)

do
    PhaseCharItem._OnInit = HL.Override() << function(self)
        PhaseCharItem.Super._OnInit(self)
        self.charId = self.arg.charId
        self.charInstId = self.arg.charInstId or -1
    end

    PhaseCharItem._OnDestroy = HL.Override() << function(self)
        if self.go then
            self.phase.modelLoader:UnloadModel(self.go)
            self.go = nil
            self.uiModelMono = nil
            self.animator = nil
        end
    end

    PhaseCharItem._GameObjectInit = HL.Override() << function(self)
        self.m_tmpViewData = {}
        PhaseCharItem.Super._GameObjectInit(self)

        self.animator = self.go:GetComponent("Animator")
        self.animator:Update(0)

        local layer = self.arg.layer
        local hide = self.arg.hide
        local pos = self.arg.pos
        if layer ~= nil then
            self:SetLayer(layer)
        end

        if pos ~= nil then
            self:SetPos(pos)
        end

        self.uiModelMono = self.go:GetComponent("CharUIModelMono")

        self:SetVisible(not hide)
        self:_RefreshView()
    end

    PhaseCharItem.LoadPotentialEffects = HL.Method() << function(self)
        if NotNull(self.uiModelMono) then
            self.uiModelMono:LoadPotentialEffects()
        end
    end

    PhaseCharItem.UnloadPotentialEffects = HL.Method() << function(self)
        if NotNull(self.uiModelMono) then
            self.uiModelMono:UnloadPotentialEffects()
        end
    end

    PhaseCharItem.LoadTargetWeapon = HL.Method(HL.Number) << function(self, instId)
        if not instId then
            return
        end

        if self.uiModelMono and NotNull(self.uiModelMono) then
            self.uiModelMono:LoadTargetWeapon(instId)
        end
    end

    PhaseCharItem.ReloadWeapon = HL.Method(HL.Opt(HL.Boolean)) << function(self, forceReload)
        if self.uiModelMono then
            if forceReload == nil then
                forceReload = true
            end
            self.uiModelMono:ReloadWeapon(self.charInstId, forceReload)
        end
    end

    PhaseCharItem.ReloadWeaponDecoEffect = HL.Method(HL.Number) << function(self, weaponInstId)
        if self.uiModelMono then
            self.uiModelMono:ReloadWeaponDecoEffect(weaponInstId)
        end
    end

    PhaseCharItem.GetAnimator = HL.Method().Return(HL.Userdata) << function(self)
        return self.animator
    end

    PhaseCharItem.PlayAnimByState = HL.Method(HL.String).Return(HL.Boolean) << function(self, state)
        if not self.go then
            return false
        end

        if not self.animator then
            return false
        end

        self.animator:Play(state)
        return true
    end

    PhaseCharItem.SetTrigger = HL.Method(HL.String) << function(self, trigger)
        self.m_tmpViewData.trigger = trigger
        self:_RefreshView()
    end

    PhaseCharItem._DoSetTrigger = HL.Method(HL.String) << function(self, trigger)
        self.animator:SetTrigger(trigger)
        if NotNull(self.uiModelMono) then
            self.uiModelMono:DecoItemSetTrigger(trigger)
        end
    end

    PhaseCharItem.SetInteger = HL.Method(HL.String, HL.Number) << function(self, name, num)
        self.m_tmpViewData.integer = num
        self.m_tmpViewData.integerName = name
        self:_RefreshView()
    end

    PhaseCharItem._DoSetInteger = HL.Method(HL.String, HL.Number) << function(self, name, num)
        self.animator:SetInteger(name, math.floor(num))
        if NotNull(self.uiModelMono) then
            self.uiModelMono:DecoItemSetInteger(name, math.floor(num))
        end
    end

    PhaseCharItem.SetBool = HL.Method(HL.String, HL.Boolean) << function(self, name, active)
        self.m_tmpViewData.bool = active
        self.m_tmpViewData.boolName = name
        self:_RefreshView()
    end

    PhaseCharItem._DoSetBool = HL.Method(HL.String, HL.Boolean) << function(self, name, active)
        self.animator:SetBool(name, active)
        if NotNull(self.uiModelMono) then
            self.uiModelMono:DecoItemSetBool(name, active)
        end
    end

    PhaseCharItem.SwitchWeaponState = HL.Method(HL.Userdata, HL.Opt(HL.Boolean)) << function(self, state, ignoreStatic)
        self.m_tmpViewData.weaponState = state
        self.m_tmpViewData.ignoreStatic = ignoreStatic

        self:_RefreshView()
    end

    PhaseCharItem._DoSwitchWeaponState = HL.Method(HL.Userdata, HL.Opt(HL.Boolean)) << function(self, state, ignoreStatic)
        if self.go and self.uiModelMono then
            local uiModelMono = self.uiModelMono

            uiModelMono:SwitchWeaponState(state, ignoreStatic)
            uiModelMono:ToggleWeaponTick(true)
        end
    end

    PhaseCharItem.SetParent = HL.Method(HL.Userdata) << function(self, parent)
        self.m_tmpViewData.parent = parent
        self:_RefreshView()
    end

    PhaseCharItem._DoSetParent = HL.Method(HL.Userdata) << function(self, parent)
        local pos = self.go.transform.localPosition
        local rot = self.go.transform.localRotation
        self.go.transform:SetParent(parent)
        self.go.transform.localPosition = pos
        self.go.transform.localRotation = rot
    end

    PhaseCharItem.SetLayer = HL.Method(HL.Number) << function(self, layer)
        self.m_tmpViewData.layer = layer
        self:_RefreshView()
    end

    PhaseCharItem._DoSetLayer = HL.Method(HL.Number) << function(self, layer)
        self.go.transform:SetLayerOnChildren(layer, true, true)
    end

    PhaseCharItem.SetPos = HL.Method(Vector3) << function(self, pos)
        self.m_tmpViewData.pos = pos
        self:_RefreshView()
    end

    PhaseCharItem._DoSetPos = HL.Method(Vector3) << function(self, pos)
        self.go.transform.localPosition = pos
    end

    PhaseCharItem.SetVisible = HL.Method(HL.Boolean) << function(self, visible)
        self.m_tmpViewData.visible = visible
        self:_RefreshView()
    end

    PhaseCharItem.IsVisible = HL.Method().Return(HL.Boolean) << function(self)
        if self.go then
            return self.go.gameObject.activeSelf
        elseif self.m_tmpViewData and self.m_tmpViewData.visible then
            return self.m_tmpViewData.visible
        else
            return true
        end
    end

    PhaseCharItem._DoSetVisible = HL.Method(HL.Boolean) << function(self, visible)
        self.uiModelMono:SetVisible(visible)
    end


    PhaseCharItem._RefreshView = HL.Method() << function(self)
        if not self.m_tmpViewData then
            return
        end

        if not next(self.m_tmpViewData) then
            return
        end

        if not self.go then
            return
        end

        local data = self.m_tmpViewData

        if data.visible ~= nil then
            self:_DoSetVisible(data.visible)
        end
        if data.pos then
            self:_DoSetPos(data.pos)
        end
        if data.parent then
            self:_DoSetParent(data.parent)
        end
        if data.layer then
            self:_DoSetLayer(data.layer)
        end
        if data.trigger then
            self:_DoSetTrigger(data.trigger)
        end
        if data.integer and not string.isEmpty(data.integerName) then
            self:_DoSetInteger(data.integerName, data.integer)
        end
        if data.bool ~= nil and not string.isEmpty(data.boolName) then
            self:_DoSetBool(data.boolName, data.bool)
        end

        if data.weaponState then
            self:_DoSwitchWeaponState(data.weaponState, data.ignoreStatic)
        end

        self.m_tmpViewData = {}
    end

    PhaseCharItem.RotateChar = HL.Method(HL.Number) << function(self, deltaAngle)
        if self.go then
            local localAngle = self.go.transform.localEulerAngles
            self.go.transform.localEulerAngles = Vector3(localAngle.x, localAngle.y + deltaAngle, localAngle.z)
        end
    end

    PhaseCharItem.ResetChar = HL.Method() << function(self)

        if self.go then
            self.go.transform.localEulerAngles = Vector3.zero
        end
    end

    PhaseCharItem.GetName = HL.Method().Return(HL.String) << function(self)
        local name
        if self.go then
            name = self.go.name
        end
        return name
    end

    PhaseCharItem.SetName = HL.Method(HL.String) << function(self, name)
        if self.go then
            self.go.name = name
        end
    end

end

do

    PhaseCharItem._DoTransitionInCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    end


    PhaseCharItem._DoTransitionBehindCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    end


    PhaseCharItem._DoTransitionOutCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    end

    PhaseCharItem._CheckAllTransitionDone = HL.Override().Return(HL.Boolean) << function(self)
        return true
    end
end

HL.Commit(PhaseCharItem)
