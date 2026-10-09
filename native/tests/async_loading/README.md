# Task C handoff (2026-10-03)

Owned files: `native/modules/custom_model/async_loading.h`, `bem.h`, `bem.cpp`,
and this independent test directory. No module/cache/matcher/camera or main
CMake changes; no APK build, device operation, branch, commit or push.

Production interface, namespace `BetterEndfieldNext::CustomModel`:

```cpp
AsyncBemLoader loader;        // one instance for all roles/world/UI
FrameBudget budget;          // one main-thread instance, real verified frame ID
BemRequest request;
request.key = selectionKey;
request.revision = immutableRevision;
request.path = package;
request.generation = selectionGeneration;
request.priority = LoadPriority::Foreground; // Visible / Prewarm also supported
request.appearance = appearanceOrOptions;
request.parameters = canonicalParameters;
request.loading_optimization = loadingOptimization;
job.ticket = loader.Request(std::move(request));

auto completion = loader.Poll(job.ticket); // never waits
if (completion.status == AsyncLoadStatus::Ready)
    job.cpu = completion.result;          // immutable BemResult; data is BemPocData

// Each ctor/raw/Apply or indivisible mesh/commit step obtains a permit.
auto permit = budget.TryBegin(realFrameId, {stepBytes, 1, true, true});
if (permit) ExecuteOneTextureStep();      // RAII records elapsed time
```

`Request` returns an empty ticket on shutdown/queue saturation. Poll exposes
Pending/Ready/Failed/Cancelled and an internal error. Key/revision must be
nonempty and identify immutable content; an optional CPU-only `file_lease`
can retain that file generation. No hash or immutable file lease is invented
from a mutable pathname. `CancelGeneration` affects existing subscribers only;
runtime must recheck its current generation and target before Unity publication.
Request a replacement subscription before cancelling an old one to reuse
unchanged content. Finished failed content remains failed while subscribed;
there is no automatic retry loop.

Runtime owns Unity roots/tracked handles/assets and all Unity work. Release
the job's ticket and CPU result when its last raw-data consumer is finished.
An aliasing `shared_ptr<const BemPocData>(result, &result->data)` also preserves
the decoded lease. Never copy/move backing out and drop the result to bypass
accounting. Cancelling a subscription does not revoke external result owners.
Call `Shutdown` on teardown, never in a synchronous Finish hook/frame pump.

Two worker limit, 3:1 foreground/background opportunities, oldest background
rotation, key+revision+selection coalescing, cancellation and stale results are
implemented in the header. BEM metadata planning uses selected payloads, with a
conservative cache-plus-consumer decoded reservation. The 256 MiB budget covers
both active reservations and results still held outside the queue. A validated
monolithic package larger than this budget gets one exclusive lease: it waits
for existing backing/workers to drain and pauses other CPU work until its last
holder releases. This is a first-version large-package exception, not payload
streaming or a 256 MiB whole-process memory cap. Compressed input, Zstd workspace,
metadata, Unity allocations and GPU allocations are outside that decoded ledger.
Bounded `LoadBem` rechecks the plan in its opened file before reading payloads.

Frame defaults: 32 MiB, 8 steps, soft 2 ms, at most one heavy step/texture per
observed frame. 64 MiB gets a clean exclusive frame. Two observed upload-free
cooldown frames and byte debt follow; debt can add another idle upload frame.
Same/old frame IDs, reentrant permits and soft-time overruns cannot reset the
ledger. These are submission controls, not GPU fences or performance results.

No additional production source is needed for this header-only helper. Both
PC and Android targets already compile `bem.cpp`; rebuild their existing core.
Standalone targets: `async_loading_tests`, `BetterEndfieldNext.BemV11CapacityTests`,
`BetterEndfieldNext.BemV13MorphTests` in `native/tests/async_loading/CMakeLists.txt`.
Local build directory: `artifacts/async-loading-tests-20261003` (MSVC Release).
Run `ctest --test-dir artifacts/async-loading-tests-20261003 -C Release --output-on-failure`.

Local validation: all three CTest targets passed; new scheduler/ownership/frame/
real BEM tests passed 55 checks. Large concurrency fixtures use declared sizes
and gated CPU backends, not four real 400 MiB allocations or a performance run.
No device/GPU measurements or claim that GPU peak has been resolved.
