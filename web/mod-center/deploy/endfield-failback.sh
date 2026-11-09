#!/bin/bash
# Fail back to the primary host. Run this ON THE PRIMARY, after the standby has
# been stopped with endfield-standby-stop.sh.
#
# Pulls the standby's data back (it may contain works and uploads created during
# the outage), then restores the tunnel connector and mirror timer. Files are
# pulled before the database, same ordering rule as the forward sync.
#
# Usage: endfield-failback.sh [--yes]
set -euo pipefail

REMOTE=endfield-sync@134.185.93.243
SSH_CMD="ssh -i /root/.ssh/endfield-sync -o BatchMode=yes -o IdentitiesOnly=yes"
SRC=/var/lib/endfield-resource-center
ASSUME=no
[ "${1:-}" = "--yes" ] && ASSUME=yes

if [ "$ASSUME" != "yes" ]; then
  echo "This will overwrite $SRC with the standby's copy."
  read -r -p "Confirm the standby is stopped and you want to continue? [yes/NO] " ans
  [ "${ans:-}" = "yes" ] || { echo "aborted"; exit 1; }
fi

echo "== 1. stop local service so nothing writes during the pull"
systemctl stop endfield-resource-center 2>/dev/null || true

echo "== 2. pull uploads"
rsync -a --numeric-ids -e "$SSH_CMD" "$REMOTE:uploads/" "$SRC/uploads/"

echo "== 3. pull database snapshot"
rsync -a -e "$SSH_CMD" "$REMOTE:catalog.sqlite" "$SRC/catalog.sqlite"
rm -f "$SRC/catalog.sqlite-wal" "$SRC/catalog.sqlite-shm"

chown -R endfield-mods:endfield-mods "$SRC"

echo "== 4. restore service and tunnel"
systemctl start endfield-resource-center
sleep 3
systemctl start endfield-sync.timer
systemctl start hydra-cloudflared
sleep 2
systemctl is-active endfield-resource-center hydra-cloudflared endfield-sync.timer | tr '\n' ' '
echo
curl -s -m 5 -o /dev/null -w "backend -> %{http_code}\n" http://127.0.0.1:9017/endfield/api/health
echo "== done"
