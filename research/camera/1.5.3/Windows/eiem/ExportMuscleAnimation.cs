// Unity Editor Script: Export VMD animation as muscle data + arm/finger bone rotations + blend shapes
// Usage:
// 1. Import MMD model (Humanoid), convert VMD to AnimationClip via MMD4Mecanim
// 2. Create an Animator Controller, add the clip as a state
// 3. Assign the Animator Controller to the model in scene
// 4. Select the model, menu: Tools > Export Muscle Animation
// 5. Output: Assets/muscle_anim.bin (binary muscle + bone + blendshape data per frame)
//
// Binary format v4 (MUS4):
//   Header: "MUS4" (4 bytes)
//   float fps (4 bytes)
//   int frameCount (4 bytes)
//   int muscleCount (4 bytes) = 95
//   int armBoneCount (4 bytes) = 6
//   int fingerBoneCount (4 bytes) = 30
//   int blendShapeCount (4 bytes)
//   Arm rest rotations: armBoneCount × quaternion(x,y,z,w) (96 bytes)
//   Finger rest rotations: fingerBoneCount × quaternion(x,y,z,w) (480 bytes)
//   Blend shape names: for each blend shape:
//     int nameLen (4 bytes)
//     byte[] nameUtf8 (nameLen bytes)
//   Per frame:
//     float bodyPosX, bodyPosY, bodyPosZ (12 bytes)
//     float bodyRotX, bodyRotY, bodyRotZ, bodyRotW (16 bytes)
//     float muscles[95] (380 bytes)
//     float armBones[6 * 4] (96 bytes)
//     float fingerBones[30 * 4] (480 bytes)
//     float blendShapes[blendShapeCount] (variable)

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public class ExportMuscleAnimation : MonoBehaviour
{
    // Arm bones to export as direct rotations
    static readonly HumanBodyBones[] armBones = {
        HumanBodyBones.LeftUpperArm,   // 0
        HumanBodyBones.LeftLowerArm,   // 1
        HumanBodyBones.LeftHand,       // 2
        HumanBodyBones.RightUpperArm,  // 3
        HumanBodyBones.RightLowerArm,  // 4
        HumanBodyBones.RightHand,      // 5
    };

    // Finger bones: 5 fingers × 3 segments × 2 hands = 30 bones
    // Order: L.Thumb(3), L.Index(3), L.Middle(3), L.Ring(3), L.Little(3),
    //        R.Thumb(3), R.Index(3), R.Middle(3), R.Ring(3), R.Little(3)
    static readonly HumanBodyBones[] fingerBones = {
        // Left hand
        HumanBodyBones.LeftThumbProximal, HumanBodyBones.LeftThumbIntermediate, HumanBodyBones.LeftThumbDistal,
        HumanBodyBones.LeftIndexProximal, HumanBodyBones.LeftIndexIntermediate, HumanBodyBones.LeftIndexDistal,
        HumanBodyBones.LeftMiddleProximal, HumanBodyBones.LeftMiddleIntermediate, HumanBodyBones.LeftMiddleDistal,
        HumanBodyBones.LeftRingProximal, HumanBodyBones.LeftRingIntermediate, HumanBodyBones.LeftRingDistal,
        HumanBodyBones.LeftLittleProximal, HumanBodyBones.LeftLittleIntermediate, HumanBodyBones.LeftLittleDistal,
        // Right hand
        HumanBodyBones.RightThumbProximal, HumanBodyBones.RightThumbIntermediate, HumanBodyBones.RightThumbDistal,
        HumanBodyBones.RightIndexProximal, HumanBodyBones.RightIndexIntermediate, HumanBodyBones.RightIndexDistal,
        HumanBodyBones.RightMiddleProximal, HumanBodyBones.RightMiddleIntermediate, HumanBodyBones.RightMiddleDistal,
        HumanBodyBones.RightRingProximal, HumanBodyBones.RightRingIntermediate, HumanBodyBones.RightRingDistal,
        HumanBodyBones.RightLittleProximal, HumanBodyBones.RightLittleIntermediate, HumanBodyBones.RightLittleDistal,
    };

    [MenuItem("Tools/Export Muscle Animation")]
    static void Export()
    {
        GameObject go = Selection.activeGameObject;
        if (go == null)
        {
            Debug.LogError("Select a GameObject with Humanoid Animator!");
            return;
        }

        Animator animator = go.GetComponent<Animator>();
        if (animator == null || !animator.isHuman || animator.runtimeAnimatorController == null)
        {
            Debug.LogError("Need Humanoid Animator with a controller + clip!");
            return;
        }

        // Get clip duration
        AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
        if (clips.Length == 0)
        {
            animator.Play(0, 0, 0);
            animator.Update(0);
            clips = animator.GetCurrentAnimatorClipInfo(0);
        }
        if (clips.Length == 0)
        {
            Debug.LogError("No animation clip playing! Make sure Animator has a clip.");
            return;
        }

        AnimationClip clip = clips[0].clip;
        float duration = clip.length;
        float fps = 30f;
        int frameCount = Mathf.CeilToInt(duration * fps) + 1;

        Debug.Log($"Exporting: {clip.name}, {duration}s, {frameCount} frames at {fps}fps");

        // Diagnostic: dump all curve bindings to find blend shape curves
        var bindings = UnityEditor.AnimationUtility.GetCurveBindings(clip);
        int bsCurves = 0;
        Debug.Log($"[DIAG] Total curve bindings in clip: {bindings.Length}");
        foreach (var b in bindings)
        {
            if (b.propertyName.StartsWith("blendShape."))
            {
                bsCurves++;
                if (bsCurves <= 20) // log first 20
                    Debug.Log($"[DIAG] BS curve: path='{b.path}' prop='{b.propertyName}' type={b.type.Name}");
            }
        }
        Debug.Log($"[DIAG] Found {bsCurves} blend shape curves out of {bindings.Length} total");

        HumanPoseHandler handler = new HumanPoseHandler(animator.avatar, animator.transform);
        HumanPose pose = new HumanPose();

        // Cache arm bone transforms
        Transform[] armTransforms = new Transform[armBones.Length];
        for (int i = 0; i < armBones.Length; i++)
        {
            armTransforms[i] = animator.GetBoneTransform(armBones[i]);
            Debug.Log($"Arm bone [{i}] {armBones[i]} = {(armTransforms[i] != null ? armTransforms[i].name : "NULL")}");
        }

        // Cache finger bone transforms
        Transform[] fingerTransforms = new Transform[fingerBones.Length];
        int fingerFound = 0;
        for (int i = 0; i < fingerBones.Length; i++)
        {
            fingerTransforms[i] = animator.GetBoneTransform(fingerBones[i]);
            if (fingerTransforms[i] != null) fingerFound++;
        }
        Debug.Log($"Finger bones: {fingerFound}/{fingerBones.Length} found");

        // Discover blend shapes on the model → write to file
        SkinnedMeshRenderer[] smrs = go.GetComponentsInChildren<SkinnedMeshRenderer>();
        string bsDumpPath = "Assets/blendshapes.txt";
        using (StreamWriter bsw = new StreamWriter(bsDumpPath))
        {
            bsw.WriteLine($"=== Blend Shape Dump ===");
            bsw.WriteLine($"Model: {go.name}");
            bsw.WriteLine($"SkinnedMeshRenderers: {smrs.Length}");
            bsw.WriteLine();
            foreach (var smr in smrs)
            {
                if (smr.sharedMesh == null) continue;
                int bsCount = smr.sharedMesh.blendShapeCount;
                bsw.WriteLine($"[{smr.name}] {bsCount} blend shapes:");
                for (int i = 0; i < bsCount; i++)
                {
                    string bsName = smr.sharedMesh.GetBlendShapeName(i);
                    float w = smr.GetBlendShapeWeight(i);
                    bsw.WriteLine($"  [{i,3}] {bsName,-40} (w={w:F1})");
                }
                bsw.WriteLine();
            }
        }
        Debug.Log($"Blend shape dump: {bsDumpPath} ({smrs.Length} SMRs)");
        // Save complete bone hierarchy state FIRST (before anything modifies transforms)
        bool origApplyRootMotion = animator.applyRootMotion;
        Vector3 origRootPos = animator.transform.localPosition;
        Quaternion origRootRot = animator.transform.localRotation;
        Transform[] allBoneTransforms = go.GetComponentsInChildren<Transform>();
        Vector3[] allBoneLocalPos = new Vector3[allBoneTransforms.Length];
        Quaternion[] allBoneLocalRot = new Quaternion[allBoneTransforms.Length];
        for (int i = 0; i < allBoneTransforms.Length; i++)
        {
            allBoneLocalPos[i] = allBoneTransforms[i].localPosition;
            allBoneLocalRot[i] = allBoneTransforms[i].localRotation;
        }
        Debug.Log($"Saved {allBoneTransforms.Length} bone transforms for restoration");

        // Capture rest pose (with Animator disabled)
        animator.enabled = false;
        Quaternion[] armRestRots = new Quaternion[armBones.Length];
        for (int i = 0; i < armBones.Length; i++)
            armRestRots[i] = armTransforms[i] != null ? armTransforms[i].localRotation : Quaternion.identity;

        Quaternion[] fingerRestRots = new Quaternion[fingerBones.Length];
        for (int i = 0; i < fingerBones.Length; i++)
            fingerRestRots[i] = fingerTransforms[i] != null ? fingerTransforms[i].localRotation : Quaternion.identity;
        animator.enabled = true;

        // Disable root motion for export sampling
        animator.applyRootMotion = false;

        // Zero out root transform so HumanPose.bodyRotation is relative to
        // identity (world Z+). Without this, the model's scene rotation gets
        // baked into bodyRotation, causing unpredictable character yaw in-game
        // every time the model is exported facing a different direction.
        animator.transform.localPosition = Vector3.zero;
        animator.transform.localRotation = Quaternion.identity;

        // Find the face SMR with most blend shapes
        SkinnedMeshRenderer faceSMR = null;
        int faceBsCount = 0;
        foreach (var smr in smrs)
        {
            if (smr.sharedMesh == null) continue;
            int c = smr.sharedMesh.blendShapeCount;
            if (c > faceBsCount) { faceBsCount = c; faceSMR = smr; }
        }
        
        // Collect blend shape names
        string[] bsNames = new string[faceBsCount];
        for (int i = 0; i < faceBsCount; i++)
            bsNames[i] = faceSMR.sharedMesh.GetBlendShapeName(i);
        Debug.Log($"Face SMR: {(faceSMR ? faceSMR.name : "none")}, {faceBsCount} blend shapes for export");

        string path = "Assets/muscle_anim.bin";
        using (BinaryWriter bw = new BinaryWriter(File.Create(path)))
        {
            // Header v4
            bw.Write(new char[] { 'M', 'U', 'S', '4' });
            bw.Write(fps);
            bw.Write(frameCount);
            bw.Write(95); // muscle count
            bw.Write(armBones.Length); // arm bone count
            bw.Write(fingerBones.Length); // finger bone count
            bw.Write(faceBsCount); // blend shape count

            // Write arm rest rotations
            for (int i = 0; i < armBones.Length; i++)
            {
                bw.Write(armRestRots[i].x);
                bw.Write(armRestRots[i].y);
                bw.Write(armRestRots[i].z);
                bw.Write(armRestRots[i].w);
            }

            // Write finger rest rotations
            for (int i = 0; i < fingerBones.Length; i++)
            {
                bw.Write(fingerRestRots[i].x);
                bw.Write(fingerRestRots[i].y);
                bw.Write(fingerRestRots[i].z);
                bw.Write(fingerRestRots[i].w);
            }

            // Write blend shape names
            for (int i = 0; i < faceBsCount; i++)
            {
                byte[] nameBytes = System.Text.Encoding.UTF8.GetBytes(bsNames[i]);
                bw.Write(nameBytes.Length);
                bw.Write(nameBytes);
            }


            for (int f = 0; f < frameCount; f++)
            {
                float t = Mathf.Min((float)f / fps, duration);
                clip.SampleAnimation(go, t);
                handler.GetHumanPose(ref pose);

                // Write body transform
                bw.Write(pose.bodyPosition.x);
                bw.Write(pose.bodyPosition.y);
                bw.Write(pose.bodyPosition.z);
                bw.Write(pose.bodyRotation.x);
                bw.Write(pose.bodyRotation.y);
                bw.Write(pose.bodyRotation.z);
                bw.Write(pose.bodyRotation.w);

                // Write muscles
                for (int m = 0; m < 95; m++)
                    bw.Write(m < pose.muscles.Length ? pose.muscles[m] : 0f);

                // Write arm bone rotations
                for (int i = 0; i < armBones.Length; i++)
                {
                    Quaternion rot = armTransforms[i] != null ? armTransforms[i].localRotation : Quaternion.identity;
                    bw.Write(rot.x); bw.Write(rot.y); bw.Write(rot.z); bw.Write(rot.w);
                }

                // Write finger bone rotations
                for (int i = 0; i < fingerBones.Length; i++)
                {
                    Quaternion rot = fingerTransforms[i] != null ? fingerTransforms[i].localRotation : Quaternion.identity;
                    bw.Write(rot.x); bw.Write(rot.y); bw.Write(rot.z); bw.Write(rot.w);
                }

                // Write blend shape weights
                for (int i = 0; i < faceBsCount; i++)
                    bw.Write(faceSMR != null ? faceSMR.GetBlendShapeWeight(i) : 0f);

                if (f % 100 == 0)
                    Debug.Log($"Frame {f}/{frameCount}...");
            }

            // Restore root transform
            animator.applyRootMotion = origApplyRootMotion;
            animator.transform.localPosition = origRootPos;
            animator.transform.localRotation = origRootRot;

            // Restore all bone transforms
            for (int i = 0; i < allBoneTransforms.Length; i++)
            {
                if (allBoneTransforms[i] != null)
                {
                    allBoneTransforms[i].localPosition = allBoneLocalPos[i];
                    allBoneTransforms[i].localRotation = allBoneLocalRot[i];
                }
            }
        }

        // Text dump of frame 0 (use saved data, don't re-sample)
        {
            // Re-sample frame 0 temporarily for text dump
            Vector3 savedPos2 = animator.transform.localPosition;
            Quaternion savedRot2 = animator.transform.localRotation;
            
            clip.SampleAnimation(go, 0);
            handler.GetHumanPose(ref pose);

            string txtPath = "Assets/muscle_anim_frame0.txt";
            using (StreamWriter sw = new StreamWriter(txtPath))
            {
                sw.WriteLine($"Clip: {clip.name}");
                sw.WriteLine($"Duration: {duration}s, Frames: {frameCount}");
                sw.WriteLine($"Format: MUS4 v4 (muscles + arm + finger bone rotations + {faceBsCount} blend shapes)");
                sw.WriteLine($"BodyPos: ({pose.bodyPosition.x:F6}, {pose.bodyPosition.y:F6}, {pose.bodyPosition.z:F6})");
                sw.WriteLine($"BodyRot: ({pose.bodyRotation.x:F6}, {pose.bodyRotation.y:F6}, {pose.bodyRotation.z:F6}, {pose.bodyRotation.w:F6})");
                sw.WriteLine();
                sw.WriteLine("=== Muscles ===");
                string[] names = HumanTrait.MuscleName;
                for (int m = 0; m < pose.muscles.Length && m < names.Length; m++)
                    sw.WriteLine($"[{m,2}] {names[m],-40} = {pose.muscles[m],10:F6}");
                sw.WriteLine();
                sw.WriteLine("=== Arm Bone Rotations ===");
                for (int i = 0; i < armBones.Length; i++)
                {
                    Quaternion rot = armTransforms[i] != null ? armTransforms[i].localRotation : Quaternion.identity;
                    sw.WriteLine($"[{i}] {armBones[i],-25} rot=({rot.x:F4},{rot.y:F4},{rot.z:F4},{rot.w:F4})");
                }
                sw.WriteLine();
                sw.WriteLine("=== Finger Bone Rotations ===");
                for (int i = 0; i < fingerBones.Length; i++)
                {
                    Quaternion rot = fingerTransforms[i] != null ? fingerTransforms[i].localRotation : Quaternion.identity;
                    Quaternion rest = fingerRestRots[i];
                    string status = fingerTransforms[i] != null ? "OK" : "MISSING";
                    sw.WriteLine($"[{i,2}] {fingerBones[i],-35} rot=({rot.x:F4},{rot.y:F4},{rot.z:F4},{rot.w:F4}) rest=({rest.x:F4},{rest.y:F4},{rest.z:F4},{rest.w:F4}) [{status}]");
                }
            }
            
            // Restore everything again after text dump
            animator.transform.localPosition = savedPos2;
            animator.transform.localRotation = savedRot2;
            for (int i = 0; i < allBoneTransforms.Length; i++)
            {
                if (allBoneTransforms[i] != null)
                {
                    allBoneTransforms[i].localPosition = allBoneLocalPos[i];
                    allBoneTransforms[i].localRotation = allBoneLocalRot[i];
                }
            }
        }

        // Reset Animator to original state
        animator.Rebind();
        animator.Update(0);

        long fileSize = new FileInfo(path).Length;
        Debug.Log($"Exported {frameCount} frames to {path} ({fileSize} bytes)");
        Debug.Log($"Format: MUS4 — muscles + {armBones.Length} arm + {fingerBones.Length} finger bones ({fingerFound} found)");
        Debug.Log($"Frame 0 text dump: Assets/muscle_anim_frame0.txt");
        AssetDatabase.Refresh();
    }
}
#endif
