using System.Collections.Generic;
using UnityEngine;

namespace Landoria.FirstPerson
{
    // Keeps vegetation materials visible when they are very close to the camera.
    internal static class VegetationController
    {
        private const string CameraCullProperty = "_CamCull";

        private static readonly Dictionary<Material, float> CameraCullValues =
            new Dictionary<Material, float>();

        internal static void Apply()
        {
            foreach (Material material in Resources.FindObjectsOfTypeAll<Material>())
            {
                Apply(material);
            }
        }

        internal static void Apply(GameObject root)
        {
            if (!Mode.Active || !root)
            {
                return;
            }

            foreach (Renderer renderer in root.GetComponentsInChildren<Renderer>(true))
            {
                foreach (Material material in renderer.sharedMaterials)
                {
                    Apply(material);
                }
            }
        }

        internal static void Restore()
        {
            foreach (KeyValuePair<Material, float> material in CameraCullValues)
            {
                if (material.Key)
                {
                    material.Key.SetFloat(CameraCullProperty, material.Value);
                }
            }

            CameraCullValues.Clear();
        }

        private static void Apply(Material material)
        {
            if (!material || !material.HasProperty(CameraCullProperty) ||
                CameraCullValues.ContainsKey(material))
            {
                return;
            }

            float value = material.GetFloat(CameraCullProperty);
            CameraCullValues.Add(material, value);
            material.SetFloat(CameraCullProperty, 0f);
        }
    }
}
