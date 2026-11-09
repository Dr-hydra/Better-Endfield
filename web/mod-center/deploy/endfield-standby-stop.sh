#!/bin/bash
# Stop the standby stack on the ARM host. Run this BEFORE failing back, so the
# standby connector leaves the tunnel and stops writing to the shared data while
# the primary pulls it back.
#
# Usage: endfield-standby-stop.sh
set -euo pipefail

COMPOSE="docker compose -f /opt/endfield/docker-compose.yml"

echo "== stopping standby (tunnel first, then app)"
$COMPOSE stop tunnel 2>/dev/null || true
$COMPOSE stop app
$COMPOSE ps
echo "== done. Now run endfield-failback.sh on the primary host."
