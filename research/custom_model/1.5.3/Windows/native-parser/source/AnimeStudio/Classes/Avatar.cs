using System.Collections.Generic;

namespace AnimeStudio
{
    public class Node
    {
        public int m_ParentId;
        public int m_AxesId;

        public Node(ObjectReader reader)
        {
            m_ParentId = reader.ReadInt32();
            m_AxesId = reader.ReadInt32();
        }
    }

    public class Limit
    {
        public object m_Min;
        public object m_Max;

        public Limit(ObjectReader reader)
        {
            var version = reader.version;
            if (version[0] > 5 || (version[0] == 5 && version[1] >= 4))//5.4 and up
            {
                m_Min = reader.ReadVector3();
                m_Max = reader.ReadVector3();
            }
            else
            {
                m_Min = reader.ReadVector4();
                m_Max = reader.ReadVector4();
            }
        }
    }

    public class Axes
    {
        public Vector4 m_PreQ;
        public Vector4 m_PostQ;
        public object m_Sgn;
        public Limit m_Limit;
        public float m_Length;
        public uint m_Type;

        public Axes(ObjectReader reader)
        {
            var version = reader.version;
            m_PreQ = reader.ReadVector4();
            m_PostQ = reader.ReadVector4();
            if (version[0] > 5 || (version[0] == 5 && version[1] >= 4)) //5.4 and up
            {
                m_Sgn = reader.ReadVector3();
            }
            else
            {
                m_Sgn = reader.ReadVector4();
            }
            m_Limit = new Limit(reader);
            m_Length = reader.ReadSingle();
            m_Type = reader.ReadUInt32();
        }
    }

    public class Skeleton
    {
        public List<Node> m_Node;
        public uint[] m_ID;
        public List<Axes> m_AxesArray;

        public Skeleton(ObjectReader reader, bool isZZZ = false)
        {
            int numNodes = reader.ReadInt32();
            m_Node = new List<Node>();
            for (int i = 0; i < numNodes; i++)
            {
                m_Node.Add(new Node(reader));
            }

            m_ID = reader.ReadUInt32Array();


            int numAxes = reader.ReadInt32Clamped(1_000_000);
            m_AxesArray = new List<Axes>();
            for (int i = 0; i < numAxes; i++)
            {
                m_AxesArray.Add(new Axes(reader));
            }
        }
    }

    public class SkeletonPose
    {
        public XForm[] m_X;

        public SkeletonPose(ObjectReader reader)
        {
            m_X = reader.ReadXFormArray();
        }
    }

    public class Hand
    {
        public int[] m_HandBoneIndex;

        public Hand(ObjectReader reader)
        {
            m_HandBoneIndex = reader.ReadInt32Array();
        }
    }

    public class Handle
    {
        public XForm m_X;
        public uint m_ParentHumanIndex;
        public uint m_ID;

        public Handle(ObjectReader reader)
        {
            m_X = reader.ReadXForm();
            m_ParentHumanIndex = reader.ReadUInt32();
            m_ID = reader.ReadUInt32();
        }
    }

    public class Collider
    {
        public XForm m_X;
        public uint m_Type;
        public uint m_XMotionType;
        public uint m_YMotionType;
        public uint m_ZMotionType;
        public float m_MinLimitX;
        public float m_MaxLimitX;
        public float m_MaxLimitY;
        public float m_MaxLimitZ;

        public Collider(ObjectReader reader)
        {
            m_X = reader.ReadXForm();
            m_Type = reader.ReadUInt32();
            m_XMotionType = reader.ReadUInt32();
            m_YMotionType = reader.ReadUInt32();
            m_ZMotionType = reader.ReadUInt32();
            m_MinLimitX = reader.ReadSingle();
            m_MaxLimitX = reader.ReadSingle();
            m_MaxLimitY = reader.ReadSingle();
            m_MaxLimitZ = reader.ReadSingle();
        }
    }

    public class Human
    {
        public XForm m_RootX;
        public Skeleton m_Skeleton;
        public SkeletonPose m_SkeletonPose;
        public Hand m_LeftHand;
        public Hand m_RightHand;
        public List<Handle> m_Handles;
        public List<Collider> m_ColliderArray;
        public int[] m_HumanBoneIndex;
        public float[] m_HumanBoneMass;
        public int[] m_ColliderIndex;
        public float m_Scale;
        public float m_ArmTwist;
        public float m_ForeArmTwist;
        public float m_UpperLegTwist;
        public float m_LegTwist;
        public float m_ArmStretch;
        public float m_LegStretch;
        public float m_FeetSpacing;
        public bool m_HasLeftHand;
        public bool m_HasRightHand;
        public bool m_HasTDoF;

        // ZZZ-specific fields
        public float m_GlobalScale;
        public string[] m_RootMotionBoneName;
        public bool m_HasTranslationDoF;
        public bool m_HasExtraRoot;
        public bool m_SkeletonHasParents;

        public Human(ObjectReader reader, bool isZZZ = false)
        {
            var version = reader.version;
            m_RootX = reader.ReadXForm();
            m_Skeleton = new Skeleton(reader);
            m_SkeletonPose = new SkeletonPose(reader);
            m_LeftHand = new Hand(reader);
            m_RightHand = new Hand(reader);

            if (version[0] < 2018 || (version[0] == 2018 && version[1] < 2)) //2018.2 down
            {
                int numHandles = reader.ReadInt32();
                m_Handles = new List<Handle>();
                for (int i = 0; i < numHandles; i++)
                {
                    m_Handles.Add(new Handle(reader));
                }

                int numColliders = reader.ReadInt32();
                m_ColliderArray = new List<Collider>(numColliders);
                for (int i = 0; i < numColliders; i++)
                {
                    m_ColliderArray.Add(new Collider(reader));
                }
            }

            m_HumanBoneIndex = reader.ReadInt32Array();
            m_HumanBoneMass = reader.ReadSingleArray();

            if (version[0] < 2018 || (version[0] == 2018 && version[1] < 2)) //2018.2 down
            {
                m_ColliderIndex = reader.ReadInt32Array();
            }

            m_Scale = reader.ReadSingle();
            m_ArmTwist = reader.ReadSingle();
            m_ForeArmTwist = reader.ReadSingle();
            m_UpperLegTwist = reader.ReadSingle();
            m_LegTwist = reader.ReadSingle();
            m_ArmStretch = reader.ReadSingle();
            m_LegStretch = reader.ReadSingle();
            m_FeetSpacing = reader.ReadSingle();
            m_HasLeftHand = reader.ReadBoolean();
            m_HasRightHand = reader.ReadBoolean();
            if (version[0] > 5 || (version[0] == 5 && version[1] >= 2)) //5.2 and up
            {
                m_HasTDoF = reader.ReadBoolean();
            }
            
            reader.AlignStream();

        }
    }

    public class AvatarConstant
    {
        public Skeleton m_AvatarSkeleton;
        public SkeletonPose m_AvatarSkeletonPose;
        public SkeletonPose m_DefaultPose;
        public uint[] m_SkeletonNameIDArray;
        public Human m_Human;
        public int[] m_HumanSkeletonIndexArray;
        public int[] m_HumanSkeletonReverseIndexArray;
        public int m_RootMotionBoneIndex;
        public XForm m_RootMotionBoneX;
        public Skeleton m_RootMotionSkeleton;
        public SkeletonPose m_RootMotionSkeletonPose;
        public int[] m_RootMotionSkeletonIndexArray;
        public bool m_UseNextLevelForRootMotionSkeleton;

        public AvatarConstant(ObjectReader reader, bool isZZZ = false)
        {
            var version = reader.version;
            m_AvatarSkeleton = new Skeleton(reader);
            m_AvatarSkeletonPose = new SkeletonPose(reader);

            if (version[0] > 4 || (version[0] == 4 && version[1] >= 3)) //4.3 and up
            {
                m_DefaultPose = new SkeletonPose(reader);
                m_SkeletonNameIDArray = reader.ReadUInt32Array();
            }

            m_Human = new Human(reader, isZZZ);

            m_HumanSkeletonIndexArray = reader.ReadInt32Array();

            if (version[0] > 4 || (version[0] == 4 && version[1] >= 3)) //4.3 and up
            {
                m_HumanSkeletonReverseIndexArray = reader.ReadInt32Array();
            }

            m_RootMotionBoneIndex = reader.ReadInt32();
            m_RootMotionBoneX = reader.ReadXForm();

            if (version[0] > 4 || (version[0] == 4 && version[1] >= 3)) //4.3 and up
            {
                m_RootMotionSkeleton = new Skeleton(reader, isZZZ);
                m_RootMotionSkeletonPose = new SkeletonPose(reader);
                m_RootMotionSkeletonIndexArray = reader.ReadInt32Array();
            }

            if (isZZZ)
            {
                m_UseNextLevelForRootMotionSkeleton = reader.ReadBoolean();
            }
        }
    }

    public class SkeletonBoneLimit
    {
        public Vector3 m_Min;
        public Vector3 m_Max;
        public Vector3 m_Value;
        public float m_Length;
        public bool m_Modified;

        public SkeletonBoneLimit(ObjectReader reader)
        {
            m_Min = reader.ReadVector3();
            m_Max = reader.ReadVector3();
            m_Value = reader.ReadVector3();
            m_Length = reader.ReadSingle();
            m_Modified = reader.ReadBoolean();
            reader.AlignStream();
        }
    }

    public class HumanBone
    {
        public string m_BoneName;
        public string m_HumanName;
        public SkeletonBoneLimit m_Limit;
        public HumanBone(ObjectReader reader)
        {
            m_BoneName = reader.ReadAlignedString();
            m_HumanName = reader.ReadAlignedString();
            m_Limit = new SkeletonBoneLimit(reader);

        }
    }

    public class SkeletonBone
    {
        public string m_Name;
        public string m_ParentName;
        public Vector3 m_Position;
        public Quaternion m_Rotation;
        public Vector3 m_Scale;

        public SkeletonBone(ObjectReader reader)
        {
            m_Name = reader.ReadAlignedString();
            m_ParentName = reader.ReadAlignedString();
            m_Position = reader.ReadVector3();
            m_Rotation = reader.ReadQuaternion();
            m_Scale = reader.ReadVector3();
        }
    }

    public class HumanDescription
    {
        public List<HumanBone> m_Human;
        public List<SkeletonBone> m_Skeleton;
        public float m_ArmTwist;
        public float m_ForeArmTwist;
        public float m_UpperLegTwist;
        public float m_LegTwist;
        public float m_ArmStretch;
        public float m_LegStretch;
        public float m_FeetSpacing;
        public float m_GlobalScale;
        public string m_RootMotionBoneName;
        public bool m_HasTranslationDoF;
        public bool m_HasExtraRoot;
        public bool m_SkeletonHasParents;
        public HumanDescription(ObjectReader reader)
        {
            int numHumans = reader.ReadInt32();
            m_Human = new List<HumanBone>(numHumans);
            for (int i = 0; i < numHumans; i++)
            {
                m_Human.Add(new HumanBone(reader));
            }
            int numSkeleton = reader.ReadInt32();
            m_Skeleton = new List<SkeletonBone>(numSkeleton);
            for (int i = 0; i < numSkeleton; i++)
            {
                m_Skeleton.Add(new SkeletonBone(reader));
            }


            m_ArmTwist = reader.ReadSingle();
            m_ForeArmTwist = reader.ReadSingle();
            m_UpperLegTwist = reader.ReadSingle();
            m_LegTwist = reader.ReadSingle();
            m_ArmStretch = reader.ReadSingle();
            m_LegStretch = reader.ReadSingle();
            m_FeetSpacing = reader.ReadSingle();
            m_GlobalScale = reader.ReadSingle();
            m_RootMotionBoneName = reader.ReadAlignedString();
            m_HasTranslationDoF = reader.ReadBoolean();
            m_HasExtraRoot = reader.ReadBoolean();
            m_SkeletonHasParents = reader.ReadBoolean();
            reader.AlignStream();
        }
    }

    public sealed class Avatar : NamedObject
    {
        public uint m_AvatarSize;
        public AvatarConstant m_Avatar;
        public Dictionary<uint, string> m_TOS;

        public HumanDescription m_HumanDescription;

        public bool IsZZZ { get; }

        public Avatar(ObjectReader reader) : base(reader)
        {

            IsZZZ = reader.Game.Type.IsZZZ();

            m_AvatarSize = reader.ReadUInt32();
            m_Avatar = new AvatarConstant(reader, IsZZZ);

            // this is what was messing zzz up
            // tested with multiple games, both hoyo and base unity and this align doesnt mess anything up....
            reader.AlignStream();

            int numTOS = reader.ReadInt32Clamped(10_000_000);
            m_TOS = new Dictionary<uint, string>();
            for (int i = 0; i < numTOS; i++)
            {
                m_TOS.Add(reader.ReadUInt32(), reader.ReadAlignedString());
            }

            // finally implemented the humandescription, not particularly useful but hey one step closer to being able to export to unity ready files
            if (reader.version[0] >= 2019)
            {
                m_HumanDescription = new HumanDescription(reader);
            }
        }

        public string FindBonePath(uint hash)
        {

            m_TOS.TryGetValue(hash, out string path);
            return path;
        }
    }
}
