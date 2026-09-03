local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ActivatorPointer = HL.Class('ActivatorPointer', UIWidgetBase)


local POINTER_ROTATION_Z_MAX = 165

local POINTER_ROTATION_Z_MIN = 15

local POINTER_SPRING_STIFFNESS = 130

local POINTER_SPRING_DAMPING = 16

local POINTER_MAX_ANGULAR_SPEED = 360

local POINTER_SNAP_ANGLE_EPSILON = 0.05

local POINTER_SNAP_VELOCITY_EPSILON = 2

local POINTER_IDLE_START_DELAY = 0.2

local POINTER_IDLE_BLEND_IN_TIME = 0.45

local POINTER_IDLE_WOBBLE_AMPLITUDE_MAIN = 0.8

local POINTER_IDLE_WOBBLE_AMPLITUDE_SUB = 0.6

local POINTER_IDLE_WOBBLE_FREQUENCY_MAIN = 6

local POINTER_IDLE_WOBBLE_FREQUENCY_SUB = 2

local function _CalcPointerTargetAngle(consumeCount, maxCount)
    if maxCount <= 0 then
        return -POINTER_ROTATION_Z_MIN, 0
    end
    local ratio = lume.clamp(consumeCount / maxCount, 0, 1)
    return -lume.lerp(POINTER_ROTATION_Z_MIN, POINTER_ROTATION_Z_MAX, ratio), ratio
end

ActivatorPointer.m_activatorMaxCount = HL.Field(HL.Number) << 0

ActivatorPointer.m_pointerTargetAngle = HL.Field(HL.Number) << 0

ActivatorPointer.m_pointerDisplayAngle = HL.Field(HL.Number) << 0

ActivatorPointer.m_pointerAngularVelocity = HL.Field(HL.Number) << 0

ActivatorPointer.m_pointerStableTime = HL.Field(HL.Number) << 0


ActivatorPointer._OnFirstTimeInit = HL.Override() << function(self)

end

ActivatorPointer.InitActivatorPointer = HL.Method(HL.Number, HL.Number) << function(self, maxCount, curCount)
    self:_FirstTimeInit()

    self.m_activatorMaxCount = maxCount

    local pointerAngle = _CalcPointerTargetAngle(curCount, self.m_activatorMaxCount)
    self.m_pointerTargetAngle = pointerAngle
    self.m_pointerDisplayAngle = pointerAngle
    self.m_pointerAngularVelocity = 0
    self.m_pointerStableTime = 0
    self.view.consumePointer.rotation = Quaternion.Euler(0, 0, pointerAngle)
end

ActivatorPointer.RefreshConsumePointer = HL.Method(HL.Number) << function(self, curConsumeCount)
    local targetAngle = _CalcPointerTargetAngle(curConsumeCount, self.m_activatorMaxCount)
    local deltaTime = math.max(Time.deltaTime, 0.001)
    if math.abs(targetAngle - self.m_pointerTargetAngle) > 0.01 then
        self.m_pointerStableTime = 0
    else
        self.m_pointerStableTime = self.m_pointerStableTime + deltaTime
    end
    self.m_pointerTargetAngle = targetAngle

    local angleDelta = targetAngle - self.m_pointerDisplayAngle
    local angularAcceleration = angleDelta * POINTER_SPRING_STIFFNESS - self.m_pointerAngularVelocity * POINTER_SPRING_DAMPING
    self.m_pointerAngularVelocity = self.m_pointerAngularVelocity + angularAcceleration * deltaTime
    self.m_pointerAngularVelocity = lume.clamp(self.m_pointerAngularVelocity, -POINTER_MAX_ANGULAR_SPEED, POINTER_MAX_ANGULAR_SPEED)
    self.m_pointerDisplayAngle = self.m_pointerDisplayAngle + self.m_pointerAngularVelocity * deltaTime

    if math.abs(targetAngle - self.m_pointerDisplayAngle) <= POINTER_SNAP_ANGLE_EPSILON and math.abs(self.m_pointerAngularVelocity) <= POINTER_SNAP_VELOCITY_EPSILON then
        self.m_pointerDisplayAngle = targetAngle
        self.m_pointerAngularVelocity = 0
    end

    local wobbleAngle = 0
    if curConsumeCount > 0 and self.m_pointerStableTime > POINTER_IDLE_START_DELAY and math.abs(self.m_pointerAngularVelocity) < 10 and math.abs(targetAngle - self.m_pointerDisplayAngle) < 1 then
        local wobbleWeight = lume.clamp((self.m_pointerStableTime - POINTER_IDLE_START_DELAY) / POINTER_IDLE_BLEND_IN_TIME, 0, 1)
        local wobbleTime = CS.UnityEngine.Time.unscaledTime
        wobbleAngle = (math.sin(wobbleTime * POINTER_IDLE_WOBBLE_FREQUENCY_MAIN * math.pi * 2) * POINTER_IDLE_WOBBLE_AMPLITUDE_MAIN
            + math.sin(wobbleTime * POINTER_IDLE_WOBBLE_FREQUENCY_SUB * math.pi * 2 + 0.7) * POINTER_IDLE_WOBBLE_AMPLITUDE_SUB)
            * wobbleWeight
    end

    local finalAngle = lume.clamp(self.m_pointerDisplayAngle + wobbleAngle, -POINTER_ROTATION_Z_MAX, -POINTER_ROTATION_Z_MIN)
    self.view.consumePointer.rotation = Quaternion.Euler(0, 0, finalAngle)
end

HL.Commit(ActivatorPointer)
return ActivatorPointer

