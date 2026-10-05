using HarmonyLib;
using UnityEngine;

namespace Landoria.FirstPerson
{
    // Saves and applies camera distance settings when the camera starts.
    [HarmonyPatch(typeof(GameCamera), "Awake")]
    internal static class CameraAwakePatch
    {
        private static void Prefix(GameCamera __instance)
        {
            Mode.CaptureVanillaDistance(__instance);
        }

        private static void Postfix(GameCamera __instance)
        {
            Mode.Apply(__instance);
        }
    }

    // Updates first-person state after Valheim positions the camera.
    [HarmonyPatch(typeof(GameCamera), "UpdateCamera")]
    internal static class CameraUpdatePatch
    {
        private static void Prefix(ref float ___m_distance)
        {
            Shortcut.Update(ref ___m_distance);
            Shortcut.PrepareDistanceObservation(___m_distance);
        }

        private static void Postfix(
            GameCamera __instance, Camera ___m_camera, ref float ___m_distance)
        {
            Shortcut.ObserveCameraDistance(ref ___m_distance);
            Player player = Player.m_localPlayer;
            bool shouldApply = Mode.ShouldActivate(
                player, GameCamera.InFreeFly(), ___m_distance);
            Mode.SetActive(shouldApply);
            Mode.ApplyConfiguredFieldOfView(__instance);
            Mode.ApplyNearClipPlane(___m_camera);
            VisibilityController.SetHidden(player, shouldApply);
            if (shouldApply)
            {
                ViewController.Apply(__instance, player);
            }
            if (shouldApply)
            {
                HeadBobController.Apply(__instance, player);
                HelmetLightController.Apply(__instance, player);
            }
            else
            {
                HeadBobController.Reset();
                HelmetLightController.Restore();
            }
        }
    }

    // Applies Valheim's first-person camera offset.
    [HarmonyPatch(typeof(GameCamera), "GetCameraOffset")]
    internal static class CameraOffsetPatch
    {
        private static void Postfix(
            GameCamera __instance, Player player, ref Vector3 __result)
        {
            if (!Mode.Active || !player) return;

            Vector3 firstPersonOffset = player.m_eye.transform.TransformVector(
                __instance.m_fpsOffset);
            __result = firstPersonOffset;
        }
    }

    // Keeps movement relative to the camera without turning the body toward strafing.
    [HarmonyPatch(typeof(Player), "AlwaysRotateCamera")]
    internal static class PlayerRotationPatch
    {
        private static void Postfix(Player __instance, ref bool __result)
        {
            if (Mode.Active && __instance == Player.m_localPlayer)
            {
                __result = true;
            }
        }
    }

    // Updates vegetation materials on objects loaded after first person activates.
    [HarmonyPatch(typeof(ZNetView), "Awake")]
    internal static class LoadedObjectPatch
    {
        private static void Postfix(ZNetView __instance)
        {
            VegetationController.Apply(__instance.gameObject);
        }
    }

    // Keeps the local character active so its animations still run while hidden.
    [HarmonyPatch(typeof(Character), "SetVisible")]
    internal static class PlayerVisibilityPatch
    {
        private static void Prefix(Character __instance, ref bool visible)
        {
            if (Mode.Active && __instance == Player.m_localPlayer)
            {
                visible = true;
            }
        }
    }

    // Refreshes hidden visuals and held items after equipment changes.
    [HarmonyPatch(typeof(VisEquipment), "UpdateVisuals")]
    internal static class VisualVisibilityPatch
    {
        private static void Postfix(
            VisEquipment __instance, GameObject ___m_leftItemInstance,
            GameObject ___m_rightItemInstance)
        {
            Player player = __instance.GetComponentInParent<Player>();
            if (player == Player.m_localPlayer)
            {
                VisibilityController.TrackHeldItems(
                    player, ___m_leftItemInstance, ___m_rightItemInstance);
            }
            if (Mode.Active && player == Player.m_localPlayer)
            {
                HelmetLightController.Refresh(player);
            }
        }
    }

    // Repositions helmet lights after Valheim finishes its frame updates.
    [HarmonyPatch(typeof(MonoUpdaters), "LateUpdate")]
    internal static class HelmetLightLateUpdatePatch
    {
        private static void Postfix()
        {
            HelmetLightController.Apply(
                GameCamera.instance, Player.m_localPlayer);
        }
    }

    // Validates and saves values handled by Valheim's FOV command.
    [HarmonyPatch(typeof(Terminal.ConsoleCommand), nameof(Terminal.ConsoleCommand.RunAction))]
    internal static class FieldOfViewCommandPatch
    {
        private static bool Prefix(
            Terminal.ConsoleCommand __instance, Terminal.ConsoleEventArgs args)
        {
            if (__instance.Command == "fov" && args.Length == 1)
            {
                args.Context?.AddString($"FOV: {Preference.FieldOfView:0.#}");
                return false;
            }

            string value = args.Length > 1 ? args[1] : null;
            bool shouldReset = __instance.Command == "fov" && args.Length == 2 &&
                               string.Equals(value, "reset",
                                   System.StringComparison.OrdinalIgnoreCase);
            if (!shouldReset)
            {
                bool parsed = args.TryParameterFloat(1, out float requestedFieldOfView);
                bool outsideSupportedRange = __instance.Command == "fov" &&
                                             args.Length > 1 && parsed &&
                                             (requestedFieldOfView <
                                              Preference.MinimumFieldOfView ||
                                              requestedFieldOfView >
                                              Preference.MaximumFieldOfView);
                if (!outsideSupportedRange)
                {
                    return true;
                }

                args.Context?.AddString(
                    $"FOV must be between {Preference.MinimumFieldOfView} " +
                    $"and {Preference.MaximumFieldOfView}. " +
                    "The current FOV was not changed.");
                return false;
            }

            float fieldOfView = Preference.DefaultFieldOfView;
            Preference.SetFieldOfView(fieldOfView);
            Mode.ApplyConfiguredFieldOfView(GameCamera.instance);
            return false;
        }

        private static void Postfix(
            Terminal.ConsoleCommand __instance, Terminal.ConsoleEventArgs args)
        {
            bool parsed = args.TryParameterFloat(1, out float fieldOfView);
            bool shouldSave = __instance.Command == "fov" && args.Length > 1 &&
                              parsed && fieldOfView >=
                              Preference.MinimumFieldOfView &&
                              fieldOfView <=
                              Preference.MaximumFieldOfView;
            if (shouldSave)
            {
                Preference.SetFieldOfView(fieldOfView);
                Mode.ApplyConfiguredFieldOfView(GameCamera.instance);
            }
        }
    }

    // Restores saved first-person settings when the local player spawns.
    [HarmonyPatch(typeof(Player), nameof(Player.OnSpawned))]
    internal static class PlayerSpawnPatch
    {
        private static void Postfix(Player __instance)
        {
            if (__instance == Player.m_localPlayer)
            {
                Mode.SetEnabled(Preference.Enabled);
                Mode.ApplyConfiguredFieldOfView(GameCamera.instance);
            }
        }
    }

    // Restores changed state when the player leaves a game session.
    [HarmonyPatch(typeof(ZNet), "OnDestroy")]
    internal static class DisconnectPatch
    {
        private static void Prefix()
        {
            Mode.ResetSession();
        }
    }
}
