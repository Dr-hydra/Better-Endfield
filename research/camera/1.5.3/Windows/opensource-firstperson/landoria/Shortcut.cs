using BepInEx.Configuration;
using UnityEngine;

namespace Landoria.FirstPerson
{
    // Toggles first person without changing the current camera distance.
    internal static class Shortcut
    {
        private static bool thirdPersonAutoActive;
        private static bool actionReturnCancelled;
        private static float actionThirdPersonEndTime;
        private static float expectedCameraDistance;

        internal static void Update(ref float cameraDistance)
        {
            UpdateActionCamera(ref cameraDistance);
            if (!CanToggle())
            {
                return;
            }

            if (IsShortcutDown(Preference.ToggleThirdPersonAutoShortcut))
            {
                ToggleThirdPersonAuto(ref cameraDistance);
                return;
            }

            if (IsShortcutDown(Preference.ToggleShortcut))
            {
                ToggleShortcut();
            }
        }

        // Records the distance before Valheim handles manual camera zoom.
        internal static void PrepareDistanceObservation(float cameraDistance)
        {
            expectedCameraDistance = cameraDistance;
        }

        // Applies manual zoom behavior while an automatic return is pending.
        internal static void ObserveCameraDistance(ref float cameraDistance)
        {
            if (!thirdPersonAutoActive)
            {
                return;
            }

            bool zoomedOut = cameraDistance >
                             expectedCameraDistance + Mathf.Epsilon;
            bool zoomedIn = cameraDistance <
                            expectedCameraDistance - Mathf.Epsilon;
            if (zoomedIn)
            {
                cameraDistance = 0f;
            }

            if (zoomedOut || zoomedIn)
            {
                thirdPersonAutoActive = false;
                actionReturnCancelled = true;
                actionThirdPersonEndTime = 0f;
            }
        }

        // Toggles whether first-person zoom is available.
        private static void ToggleShortcut()
        {
            thirdPersonAutoActive = false;
            actionReturnCancelled = false;
            actionThirdPersonEndTime = 0f;
            bool enabled = !Mode.Enabled;
            Mode.SetEnabled(enabled);
            Preference.SetEnabled(enabled);
            ShowState(enabled);
        }

        // Toggles and saves third person during combat actions.
        private static void ToggleThirdPersonAuto(ref float cameraDistance)
        {
            bool enabled = !Preference.ThirdPersonAuto;
            Preference.SetThirdPersonAuto(enabled);
            UpdateActionCamera(ref cameraDistance);
            Player.m_localPlayer?.Message(
                MessageHud.MessageType.TopLeft,
                enabled
                    ? "Third person auto enabled"
                    : "Third person auto disabled");
        }

        // Keeps combat actions in third person for the configured return delay.
        private static void UpdateActionCamera(ref float cameraDistance)
        {
            if (!Preference.ThirdPersonAuto || !Mode.Enabled)
            {
                if (thirdPersonAutoActive)
                {
                    cameraDistance = 0f;
                    thirdPersonAutoActive = false;
                    actionThirdPersonEndTime = 0f;
                }
                return;
            }

            Player player = Player.m_localPlayer;
            bool actionActive = player && (player.InAttack() || player.IsBlocking());
            if (!actionActive)
            {
                actionReturnCancelled = false;
            }

            if (actionActive && !actionReturnCancelled &&
                (thirdPersonAutoActive ||
                 Mode.IsFirstPersonDistance(cameraDistance)))
            {
                if (!thirdPersonAutoActive)
                {
                    cameraDistance = Preference.ThirdPersonAutoDistance;
                }
                thirdPersonAutoActive = true;
                actionThirdPersonEndTime = Time.unscaledTime +
                                           Preference.AutomaticReturnDelay;
            }
            else if (thirdPersonAutoActive &&
                     Time.unscaledTime >= actionThirdPersonEndTime)
            {
                cameraDistance = 0f;
                thirdPersonAutoActive = false;
                actionThirdPersonEndTime = 0f;
            }
        }

        internal static void Reset()
        {
            thirdPersonAutoActive = false;
            actionReturnCancelled = false;
            actionThirdPersonEndTime = 0f;
            expectedCameraDistance = 0f;
        }

        private static bool CanToggle()
        {
            Player player = Player.m_localPlayer;
            return player && !player.IsDead() && !player.InCutscene() &&
                   !GameCamera.InFreeFly() && !Console.IsVisible() &&
                   !Menu.IsVisible() && !InventoryGui.IsVisible() &&
                   !StoreGui.IsVisible() && !Minimap.IsOpen() &&
                   !Hud.IsPieceSelectionVisible() && !Hud.InRadial() &&
                   (Chat.instance == null || !Chat.instance.HasFocus());
        }

        // Checks whether a configured keyboard shortcut was pressed.
        private static bool IsShortcutDown(KeyboardShortcut shortcut)
        {
            if (shortcut.MainKey == KeyCode.None ||
                !ZInput.GetKeyDown(shortcut.MainKey))
            {
                return false;
            }

            foreach (KeyCode modifier in shortcut.Modifiers)
            {
                if (!ZInput.GetKey(modifier))
                {
                    return false;
                }
            }

            return true;
        }

        private static void ShowState(bool enabled)
        {
            Player.m_localPlayer?.Message(
                MessageHud.MessageType.TopLeft,
                enabled ? "First person enabled" : "First person disabled");
        }
    }
}
