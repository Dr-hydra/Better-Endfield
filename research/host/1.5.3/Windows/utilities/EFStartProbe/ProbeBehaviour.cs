using System;
using UnityEngine;

namespace EFStartProbe;

public sealed class ProbeBehaviour : MonoBehaviour
{
    private ProbeRuntime? runtime;

#if EFSTART_STUB
    public ProbeBehaviour()
    {
    }
#else
    public ProbeBehaviour(IntPtr pointer)
        : base(pointer)
    {
    }
#endif

    internal void Initialize(ProbeRuntime value)
    {
        runtime = value;
    }

    private void Update()
    {
        runtime?.Update();
    }

    private void OnDestroy()
    {
        runtime?.Dispose();
        runtime = null;
    }
}
