package dev.betterendfield.next;

public final class OverlayReconnectPolicyTest {
    private static int checks;
    private static void check(boolean value, String reason) {
        ++checks; if (!value) throw new AssertionError(reason);
    }
    public static void main(String[] args) {
        for (String operation : new String[] {"edit_models", "disable_models", "edit_fov", "unknown", "read_unknown"}) {
            for (String reason : new String[] {"caller_rejected", "transport_IllegalArgumentException", "transport_DeadObjectException"}) {
                check(!new OverlayReconnectPolicy().begin(operation, reason, true, 100), "mutation/unknown operation triggered recovery: " + operation);
            }
        }
        OverlayReconnectPolicy policy = new OverlayReconnectPolicy();
        check(!policy.begin("read_models", "transport_IllegalArgumentException", false, 100), "background poll woke owner");
        check(policy.begin("read_models", "transport_IllegalArgumentException", true, 100), "foreground first read did not recover");
        check(!policy.begin("read_models", "caller_rejected", true, 100), "same poll started duplicate bootstrap");
        check(!policy.begin("read_fov", "caller_rejected", true, 29_999 + 100), "another tab bypassed cooldown");
        check(!policy.begin("read_models", "caller_rejected", true, 50), "backward clock bypassed cooldown");
        check(policy.begin("read_fov", "caller_rejected", true, 30_000 + 100), "new read could not recover after cooldown");
        for (String reason : new String[] {"provider_IllegalStateException", "provider_IOException", "provider_failed", "", "ok"}) {
            OverlayReconnectPolicy damaged = new OverlayReconnectPolicy();
            check(!damaged.begin("read_models", reason, true, 100), "provider/data failure launched owner: " + reason);
            check(damaged.begin("read_models", "caller_rejected", true, 100), "noneligible failure consumed wake quota");
        }
        check(!new OverlayReconnectPolicy().begin("read_models", null, true, 100), "unknown reply triggered recovery");
        check(!new OverlayReconnectPolicy().begin(null, "caller_rejected", true, 100), "missing method triggered recovery");
        check(new OverlayReconnectPolicy().begin("read_fov", "transport_DeadObjectException", true, 0), "initial zero monotonic time rejected");
        check(OverlayReconnectPolicy.READ_RETRIES > 0 && OverlayReconnectPolicy.RETRY_DELAY_MS > 0
                && OverlayReconnectPolicy.READ_RETRIES * OverlayReconnectPolicy.RETRY_DELAY_MS <= 2_000, "read recovery exceeds worker wait budget");
        System.out.println("PASS overlay reconnect policy: " + checks + " checks; reads only, foreground only, shared cooldown, data errors and bounded wait");
    }
}
