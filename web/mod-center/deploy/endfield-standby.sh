#!/bin/bash
# Reached only through the restricted authorized_keys entry used by the standby
# host. The client calls this over SSH with an operation name, which arrives in
# SSH_ORIGINAL_COMMAND.
#
#   standby-on   stop the primary tunnel connector and the mirror timer so the
#                standby can take over without the two fighting over data
#   standby-off  restore both after failing back
#   status       report the current state
set -euo pipefail

case "${SSH_ORIGINAL_COMMAND:-}" in
  standby-on)
    sudo -n systemctl stop endfield-sync.timer
    sudo -n systemctl stop endfield-sync.service 2>/dev/null || true
    sudo -n systemctl stop hydra-cloudflared
    sudo -n systemctl is-active hydra-cloudflared endfield-sync.timer 2>/dev/null | tr '\n' ' ' || true
    echo "primary stopped"
    ;;
  standby-off)
    sudo -n systemctl start endfield-sync.timer
    sudo -n systemctl start hydra-cloudflared
    sudo -n systemctl is-active hydra-cloudflared endfield-sync.timer 2>/dev/null | tr '\n' ' ' || true
    echo "primary restored"
    ;;
  status)
    sudo -n systemctl is-active hydra-cloudflared endfield-sync.timer 2>/dev/null | tr '\n' ' ' || true
    echo
    ;;
  *)
    echo "usage: standby-on | standby-off | status" >&2
    exit 2
    ;;
esac
