# MmdAudio host JVM regression tests

The runner compiles the current production `android/app/src/main/java/dev/betterendfield/next/MmdAudio.java`
with minimal `android.media.MediaPlayer` and `android.os` stubs. It requires a JDK supporting Java 17,
and does not run Gradle, JNI, an emulator, or the overlay geometry suite.

From the repository root:

```powershell
./native/tests/android_mmd_audio/Run.ps1
./native/tests/android_mmd_audio/Run.ps1 -Cases seek_burst_last_wins,background_close_queue
./native/tests/android_mmd_audio/Run.ps1 -Cases prepared_generation_snapshot_race,poll_error_generation_snapshot_race,play_generation_snapshot_race
```

Each case runs in a fresh JVM. The runner copies the production source unchanged into a temporary
directory and prints its SHA256, so concurrent production edits cannot change a running suite.
An assertion failure or an uncaught worker exception makes the runner exit nonzero. The compiled
classes and source snapshot remain in the printed temporary directory for inspection.

## Model and coverage

The worker uses a manual clock and FIFO ordering for tasks with the same due time. Neither `post`
nor `prepareAsync` runs inline. Tests explicitly deliver prepared, seek, error, and completion callbacks.
Every MediaPlayer API call must run on its owning worker. The player stub checks ready/playing states,
rejects overlapping native seeks, records starts/releases/volume, and supports injected failures.
This validates Java state and queue behavior, rather than codecs or device audio output.

The cases cover asynchronous preparation, loading gain updates, play/pause/resume, the 50 ms position
poll, seek clamping and precision, last-wins seek coalescing, pause/resume while seeking, stale token
operations and callbacks, queued open/close/reopen, background close with pending transport, completion
and replay, data-source failure, media errors, polling errors, and callback exceptions.

Three generation-race cases start a real producer thread from a MediaPlayer fault hook. That producer
opens a new track after the old task has checked ownership but before the old task publishes its result.
The new track must retain its Loading snapshot with zero position/duration and no old failure. If a
production fix serializes the producer with the callback, the harness allows that producer to finish
after the callback returns; it does not require any particular locking implementation.

## Current verified result

The parent agent ran the full `Run.ps1` suite after the production fixes: **23/23 cases passed**.
The tested production source SHA256 was:

```text
F1ECE2A210F0CDF6306C0654AF3E49D7277DA472AFAEB946329D4F83A8457D0C
```

The fixes serialize `open`, worker operations, and guarded callback state publication with the same
class monitor, and capture RuntimeException from prepared/seek callbacks as Error snapshots.
All five regression cases below passed in this run; they do not describe outstanding defects in
the verified source. This result was supplied from the parent agent's actual run; no duplicate run
was performed for this documentation update.

## Historical findings, fixed and verified

Before the fixes, the initial production source passed 18 normal cases and failed these five additional
cases. This table records the original failures; each case now passes in the verified run above:

| Case | Observed failure | Required behavior |
| --- | --- | --- |
| `prepared_generation_snapshot_race` | Old prepared callback changes the new token to Ready (2). | Keep the new token Loading (1), without the old duration. |
| `play_generation_snapshot_race` | Old play task changes the new token to Playing (3). | Do not publish old playback state to the new token. |
| `poll_error_generation_snapshot_race` | Old position-read exception changes the new token to Error (6). | Do not publish the old error to the new token. |
| `prepared_volume_failure` | `setVolume` exception escapes the prepared callback. | Publish an Error snapshot and retain a usable worker for close/reopen. |
| `seek_resume_start_failure` | `start` exception escapes the seek callback. | Publish an Error snapshot and retain a usable worker for close/reopen. |

The production implementation is not changed by these tests. All regression assertions remain enabled
to detect a future recurrence of the publication race or callback error handling defects.
