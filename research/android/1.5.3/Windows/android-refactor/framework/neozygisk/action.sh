printf "Status of NeoZygisk\n\n"

cat /data/adb/neozygisk/module.prop

if [[ -z "$MMRL" ]] && ([[ -n "$KSU" ]] || [[ -n "$APATCH" ]]); then
	# Avoid instant exit on KernelSU or APatch
	sleep 5
fi
