using UnityEngine;

namespace Landoria.FirstPerson
{
    // Controls the local player's view while first person is active.
    internal static class ViewController
    {
        private const float BackwardOffset = 0.4f; // Meters.
        // Aligns the player with the camera while keeping Valheim's camera position.
        internal static void Apply(GameCamera camera, Player player)
        {
            if (!camera || !player || player.IsAttached() || player.InCutscene())
            {
                return;
            }

            Quaternion cameraRotation = camera.transform.rotation;
            Vector3 lookDirection = camera.transform.forward;
            Vector3 bodyDirection = Vector3.ProjectOnPlane(lookDirection, Vector3.up);
            if (!Menu.IsVisible() &&
                bodyDirection.sqrMagnitude > Mathf.Epsilon)
            {
                player.SetLookDir(lookDirection);
                player.transform.rotation = Quaternion.LookRotation(bodyDirection, Vector3.up);
            }

            // Preserve Valheim's smoothed base position, then apply view offsets.
            camera.transform.position -= lookDirection * BackwardOffset;
            camera.transform.rotation = cameraRotation;
        }
    }
}
