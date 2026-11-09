#!/bin/bash
# Mirror the Endfield resource center to the standby host.
#
# Order matters: uploads first, then the database snapshot. If the database were
# captured first it could reference image/file rows whose bytes have not been
# transferred yet, and the standby would serve 404s for them after a failover.
#
# Deletions and overwrites are parked under trash/<date>/ for 30 days.
set -euo pipefail

SRC=/var/lib/endfield-resource-center
DEST=endfield-sync@134.185.93.243
SSH_CMD="ssh -i /root/.ssh/endfield-sync -o BatchMode=yes -o IdentitiesOnly=yes"
STAMP=$(date +%F)

echo "[$(date -Is)] sync start"

rsync -a --numeric-ids --delete \
      --backup --backup-dir="/srv/backup/endfield/trash/$STAMP" \
      -e "$SSH_CMD" \
      "$SRC/uploads/" "$DEST:uploads/"

SNAP=$(mktemp /tmp/endfield-db-XXXXXX.sqlite)
trap 'rm -f "$SNAP"' EXIT

# python3 is used because the host has no sqlite3 CLI, and the backup API is the
# only way to get a consistent copy while the service keeps writing.
python3 - "$SRC/catalog.sqlite" "$SNAP" <<'PY'
import sqlite3, sys
src = sqlite3.connect(sys.argv[1])
dst = sqlite3.connect(sys.argv[2])
src.backup(dst)
dst.close()
src.close()
PY

rsync -a -e "$SSH_CMD" "$SNAP" "$DEST:catalog.sqlite"

echo "[$(date -Is)] sync done"
