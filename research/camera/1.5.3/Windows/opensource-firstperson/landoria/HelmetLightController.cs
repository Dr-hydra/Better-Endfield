using System.Collections.Generic;
using UnityEngine;

namespace Landoria.FirstPerson
{
    // Keeps helmet lights stable and restores their original settings afterward.
    internal static class HelmetLightController
    {
        private struct LightState
        {
            internal Vector3 LocalPosition;
            internal Quaternion LocalRotation;
        }

        private struct FlickerState
        {
            internal float Intensity;
            internal float Movement;
        }

        private static readonly Dictionary<Light, LightState> Lights =
            new Dictionary<Light, LightState>();
        private static readonly Dictionary<LightFlicker, FlickerState> Flickers =
            new Dictionary<LightFlicker, FlickerState>();
        private static Player trackedPlayer;

        // Places tracked helmet lights on the first-person camera.
        internal static void Apply(GameCamera camera, Player player)
        {
            if (!Mode.Active || !camera || !player)
            {
                Restore();
                return;
            }

            if (trackedPlayer != player)
            {
                Refresh(player);
            }
            foreach (Light light in Lights.Keys)
            {
                if (light)
                {
                    light.transform.SetPositionAndRotation(
                        camera.transform.position, camera.transform.rotation);
                }
            }
        }

        // Finds and remembers the current player's helmet lights.
        internal static void Refresh(Player player)
        {
            if (!player)
            {
                Restore();
                return;
            }

            if (trackedPlayer != player)
            {
                Restore();
                trackedPlayer = player;
            }

            VisEquipment equipment = player.GetComponent<VisEquipment>();
            if (!equipment || !equipment.m_helmet)
            {
                return;
            }

            CaptureLights(equipment.m_helmet);
            CaptureFlickers(equipment.m_helmet);
        }

        // Restores every tracked light to its original state.
        internal static void Restore()
        {
            foreach (KeyValuePair<Light, LightState> light in Lights)
            {
                if (light.Key)
                {
                    light.Key.transform.localPosition = light.Value.LocalPosition;
                    light.Key.transform.localRotation = light.Value.LocalRotation;
                }
            }

            foreach (KeyValuePair<LightFlicker, FlickerState> flicker in Flickers)
            {
                if (flicker.Key)
                {
                    flicker.Key.m_flickerIntensity = flicker.Value.Intensity;
                    flicker.Key.m_movement = flicker.Value.Movement;
                }
            }

            Lights.Clear();
            Flickers.Clear();
            trackedPlayer = null;
        }

        private static void CaptureLights(Transform helmet)
        {
            foreach (Light light in helmet.GetComponentsInChildren<Light>(true))
            {
                if (light && !Lights.ContainsKey(light))
                {
                    Lights.Add(light, new LightState
                    {
                        LocalPosition = light.transform.localPosition,
                        LocalRotation = light.transform.localRotation
                    });
                }
            }
        }

        private static void CaptureFlickers(Transform helmet)
        {
            foreach (LightFlicker flicker in
                helmet.GetComponentsInChildren<LightFlicker>(true))
            {
                if (flicker && !Flickers.ContainsKey(flicker))
                {
                    Flickers.Add(flicker, new FlickerState
                    {
                        Intensity = flicker.m_flickerIntensity,
                        Movement = flicker.m_movement
                    });
                }

                if (flicker)
                {
                    flicker.m_flickerIntensity = 0f;
                    flicker.m_movement = 0f;
                }
            }
        }
    }
}
