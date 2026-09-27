# Android BEM file opening / sharing — 2026-09-27

## Delivery baseline

The single delivery patch is relative to `main` commit
`9b1e89599b1d3fbdbf1bcccd2d1b3c48340125c0`, not to the old Android repair branch
or to the rebuild branch. It contains the preceding 38-file Android runtime and
hotkey-overlay rebuild plus this file-import change. No CI/workflow is added.

The earlier runtime changes retain their own scope and limitations in
`ANDROID_REBUILD_20260927.md`. This change does not claim to fix every shared
native Hook target or to establish physical-device acceptance of those changes.

## User flow

Open one local BEM file in a document/file manager, choose **Better Endfield ·
导入 BEM**, and the existing package-management screen starts importing. A
single-file **Share** action is also supported. The in-app document picker remains
available. Import preserves original textures; conversion remains a separate,
explicit action. Installed selections take effect on the next game startup.
The existing LSPosed/module-service publication requirement is unchanged.

The Activity accepts ACTION_VIEW with a content URI, and ACTION_SEND with
EXTRA_STREAM or one URI-bearing ClipData item. No path extra, file:// URI,
http(s) URL, text-as-URI coercion, or multi-document request is accepted. No
storage, all-files, Internet or overlay permission was added. The sender must
grant read access or expose an otherwise readable document. The input is opened
on the existing installation worker with ContentResolver; no "real path" lookup
and no provider display-name query are performed on the UI thread.

## Resolver compatibility (intentional tradeoff)

The registered BEM MIME identifiers are application/x-bem and
application/vnd.betterendfield.bem; these are application-defined identifiers,
not a claim of IANA registration. Generic application/octet-stream and
application/x-binary are supported because document URIs can contain only opaque
IDs, without the original filename. A separate VIEW filter supports providers
that supply no MIME type at all. Filename case and dot count are not used as a
content-validation rule.

Consequently the app may also appear as an option for other generic/untyped
binary documents. These are rejected by byte validation. The manifest does not
claim */* or application/*, and does not take over image/PDF/text associations.
A file manager assigning an incompatible MIME type, not offering a chooser, or
not granting access may still require using **Share** or the app's document picker.
This is Android intent routing, not a universal Windows-style extension registry.

## Validation and lifecycle

BemImportRequest parses a single URI and rejects conflicting data/stream/ClipData
fields. BemImportStream checks the actual eight-byte `BEM\0PKG\0` signature before
copying the rest, with the pre-existing 2 GiB cap and cancellation checkpoints.
This is only an early rejection check: the existing native parser still validates
the complete BEM structure/version/selection before publication. Renaming a file
to .bem does not make it pass. Input is copied, not overwritten or deleted.

BemInstaller.start now reports whether it accepted the request. Busy operations
retain their original status and are not cancelled by an external open. The UI
holds at most one pending URI, explains the busy state, and offers explicit retry
or dismissal. Another external open replaces that pending request (the UI explains
this policy); there is no unbounded queue or silently scheduled later import.

singleTop/onNewIntent handle new documents when the screen is already on top.
Saved state prevents configuration changes/task recreation from replaying an
already accepted Intent. Only a pending request is restored for manual retry.
The screen stays in the task while the worker consumes the temporary URI grant;
there is no trampoline Activity that finishes immediately and revokes the grant.
No durable background job or process-death auto-resume is claimed. After process
termination, reopen the document if necessary. Permission failures have a specific
retry message and do not substitute filesystem access.

## Local checks and remaining acceptance

Executed locally:

- `bash native/tests/android_rebuild/run.sh`: preceding input/Hook/runtime/JNI/
  camera regression fixtures and Android-conditional shared-source syntax checks.
- `bash native/tests/bem_import/run.sh`: 31 assertions against the production
  streaming guard (exact bytes, short/zero reads, malformed/truncated header,
  limit, cancellation, failure propagation and caller-owned streams), plus XML/
  manifest structural assertions. These do not simulate the Android resolver.
- `python3 native/tests/bem_import/check_java.py --android-jar <SDK android.jar>`:
  import/Activity/installer sources and device-test source compile against API 37,
  using actual resource names plus compile-only FrameworkSettings signatures.
  This is not Gradle dependency resolution, resource linking, dexing or an APK.

Added but NOT executed here: device tests in BemInstallerTest for VIEW/SEND/
ClipData parsing, malformed/conflicting inputs, busy result, and actual package
manager matching. They use the repository's existing instrumentation runner.

Before release, build/install the APK and test different document managers on
Android: cold/warm open, opaque URI, generic MIME, uppercase file extension, Share,
missing read permission, invalid/oversized package, open while busy, rotate while
importing, process termination, successful framework publication and restart-game
activation. A successful host test is not an ART/LSPosed or ARM64 acceptance result.

## Platform references

- https://developer.android.com/guide/topics/manifest/data-element
- https://developer.android.com/training/sharing/receive
- https://developer.android.com/reference/android/content/Intent#FLAG_GRANT_READ_URI_PERMISSION
