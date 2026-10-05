using System.Collections.Generic;
using UnityEngine;

namespace Landoria.FirstPerson
{
    // Hides the local body while keeping held items visible in first person.
    internal static class VisibilityController
    {
        private static readonly HashSet<Renderer> HiddenRenderers =
            new HashSet<Renderer>();
        private static readonly Dictionary<Animator, AnimatorCullingMode> AnimatorModes =
            new Dictionary<Animator, AnimatorCullingMode>();
        private static Player hiddenPlayer;
        private static Player equippedPlayer;
        private static GameObject leftHandItem;
        private static GameObject rightHandItem;

        // Remembers the items currently shown in the player's hands.
        internal static void TrackHeldItems(
            Player player, GameObject leftItem, GameObject rightItem)
        {
            equippedPlayer = player;
            leftHandItem = leftItem;
            rightHandItem = rightItem;
            if (player && player == hiddenPlayer)
            {
                HideCurrentVisuals();
            }
        }

        // Hides or restores the local player's first-person visuals.
        internal static void SetHidden(Player player, bool hidden)
        {
            if (!hidden || !player)
            {
                Restore();
                return;
            }

            if (hiddenPlayer != player)
            {
                Restore();
                hiddenPlayer = player;
                HideCurrentVisuals();
            }
        }

        // Restores all renderers and animator settings changed by the mod.
        internal static void Restore()
        {
            foreach (Renderer renderer in HiddenRenderers)
            {
                if (renderer)
                {
                    renderer.forceRenderingOff = false;
                }
            }

            foreach (KeyValuePair<Animator, AnimatorCullingMode> animator in AnimatorModes)
            {
                if (animator.Key)
                {
                    animator.Key.cullingMode = animator.Value;
                }
            }

            HiddenRenderers.Clear();
            AnimatorModes.Clear();
            hiddenPlayer = null;
        }

        private static void HideCurrentVisuals()
        {
            foreach (Renderer renderer in hiddenPlayer.GetComponentsInChildren<Renderer>(true))
            {
                if (renderer && !renderer.forceRenderingOff)
                {
                    renderer.forceRenderingOff = true;
                    HiddenRenderers.Add(renderer);
                }
            }

            foreach (Animator animator in hiddenPlayer.GetComponentsInChildren<Animator>(true))
            {
                if (animator && !AnimatorModes.ContainsKey(animator))
                {
                    AnimatorModes.Add(animator, animator.cullingMode);
                    animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
                }
            }

            if (hiddenPlayer == equippedPlayer)
            {
                ShowHeldItem(leftHandItem);
                ShowHeldItem(rightHandItem);
            }
        }

        private static void ShowHeldItem(GameObject item)
        {
            if (!item)
            {
                return;
            }

            foreach (Renderer renderer in item.GetComponentsInChildren<Renderer>(true))
            {
                if (renderer && HiddenRenderers.Remove(renderer))
                {
                    renderer.forceRenderingOff = false;
                }
            }
        }
    }
}
