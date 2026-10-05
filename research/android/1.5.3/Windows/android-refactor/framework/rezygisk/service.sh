#!/system/bin/sh

set -e

DEBUG=false

MODDIR=${0%/*}

if [ "$ZYGISK_ENABLED" ]; then
  sed -i "s|^description=|description=[❌ Disable Magisk's built-in Zygisk] |" "$MODDIR/module.prop"

  exit 0
fi

cd "$MODDIR"

if [ "$(which magisk)" ]; then
  for file in ../*; do
    if [ -d "$file" ] && [ -d "$file/zygisk" ] && ! [ -f "$file/disable" ]; then
      if [ -f "$file/service.sh" ]; then
        cd "$file"
        log -p i -t "zygisk-sh" "Manually trigger service.sh for $file"
        sh "$(realpath ./service.sh)" &
        cd "$MODDIR"
      fi
    fi
  done
fi

exit 0
