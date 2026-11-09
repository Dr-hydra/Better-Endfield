#!/bin/bash
# Take over on the standby host. Run this ON THE ARM HOST.
#
# It stops the primary's tunnel connector and mirror timer first — if the
# primary is still alive but its backend is dead, its connector keeps forwarding
# to the dead backend and the domain stays 502 forever. Stopping the mirror timer
# also prevents it from overwriting what the standby starts writing.
#
# Usage: endfield-failover.sh [--yes]
set -euo pipefail

PRIMARY=opc@146.235.16.65
KEY=/opt/endfield/failover-key
SSH_CMD="ssh -i $KEY -o BatchMode=yes -o IdentitiesOnly=yes -o StrictHostKeyChecking=yes"
HEALTH=https://146.235.16.65:8443/endfield/api/health
COMPOSE="docker compose -f /opt/endfield/docker-compose.yml"
ASSUME=no
[ "${1:-}" = "--yes" ] && ASSUME=yes

echo "== 1. primary state"
if curl -sk -m 5 -o /dev/null "$HEALTH"; then
  echo "   primary backend is STILL SERVING."
  echo "   Starting the standby now would leave two connectors on the same tunnel."
  if [ "$ASSUME" != "yes" ]; then
    read -r -p "   stop primary connector and mirror timer now? [yes/NO] " ans
    [ "${ans:-}" = "yes" ] || { echo "aborted"; exit 1; }
  fi
  $SSH_CMD "$PRIMARY" standby-on
else
  echo "   primary backend unreachable; asking it to stand down anyway"
  $SSH_CMD "$PRIMARY" standby-on || echo "   primary host unreachable, assuming total failure"
fi

echo "== 2. start standby stack"
cd /opt/endfield
$COMPOSE up -d

echo "== 3. local health"
ok=no
for _ in $(seq 1 20); do
  if curl -s -m 3 -o /dev/null http://127.0.0.1:9017/endfield/api/health; then ok=yes; break; fi
  sleep 2
done
[ "$ok" = yes ] || { echo "   backend did not come up"; docker compose -f /opt/endfield/docker-compose.yml logs --tail 20 app; exit 1; }
echo "   backend ok"

echo "== 4. cloudflare routing (needs a moment to register the connector)"
sleep 15
curl -s -m 20 -o /dev/null -w "   domain -> %{http_code}\n" https://endfield.03250799.xyz/endfield/api/health \
  || echo "   domain check failed (may be blocked from this network)"

echo "== done. verify in a browser, then run endfield-standby-stop.sh before failing back."
