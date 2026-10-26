#!/system/bin/sh
# Deploy the BEM Tools CLI somewhere Android will actually let you execute it.
#
#   sh install.sh                          # -> /data/local/tmp/bem-cli
#   sh install.sh /data/local/tmp/bem-cli  # explicit target
#   sh install.sh --with-deps              # also pip install zstandard if missing
#
# /sdcard is mounted noexec, and since Android 10 an app's private directory cannot
# exec either, so the default target is /data/local/tmp - writable and executable
# for the shell and for anything an agent runs under root.
set -u

src=$(cd -P "$(dirname "$0")" 2>/dev/null && pwd) || src=$(dirname "$0")
target=/data/local/tmp/bem-cli
with_deps=0

for arg in "$@"; do
    case $arg in
        --with-deps) with_deps=1 ;;
        -*) printf 'install.sh: unknown option %s\n' "$arg" >&2; exit 2 ;;
        *) target=$arg ;;
    esac
done

python_bin=${BEM_PYTHON:-}
if [ -z "$python_bin" ] && command -v python3 >/dev/null 2>&1; then
    python_bin=python3
fi
if [ -z "$python_bin" ] && command -v python >/dev/null 2>&1; then
    python_bin=python
fi
if [ -z "$python_bin" ]; then
    printf '%s\n' 'install.sh: no python3 found on PATH.' >&2
    printf '%s\n' '  Termux: pkg install python' >&2
    printf '%s\n' '  or set BEM_PYTHON=/path/to/python3' >&2
    exit 1
fi

version=$("$python_bin" -c 'import sys; print("%d.%d" % sys.version_info[:2])' 2>/dev/null || printf '0.0')
major=${version%%.*}
minor=${version#*.}
if [ "$major" -lt 3 ] || { [ "$major" -eq 3 ] && [ "$minor" -lt 10 ]; }; then
    printf 'install.sh: Python %s is too old, BEM Tools needs 3.10+\n' "$version" >&2
    exit 1
fi

PYTHONPATH=$src/app${PYTHONPATH:+:$PYTHONPATH}
export PYTHONPATH

# Two backends satisfy the converter: the zstandard extension, or the standard
# library's compression.zstd that Python 3.14+ (and therefore current Termux)
# ships. The second one means a fresh device needs no wheels at all.
zstd_backend=$("$python_bin" -c 'import zstd_compat; print(zstd_compat.backend or "")' 2>/dev/null | tr -d '\r')
if [ -z "$zstd_backend" ]; then
    if [ -d "$src/wheels" ]; then
        printf '%s\n' 'install.sh: installing zstandard from the bundled wheel ...'
        if ! "$python_bin" "$src/app/bem_android.py" install-wheel "$src/wheels"; then
            printf '%s\n' 'install.sh: bundled wheel did not fit; falling back to pip ...' >&2
            "$python_bin" -m pip install --no-index --find-links "$src/wheels" zstandard || true
        fi
    elif [ "$with_deps" = 1 ]; then
        printf '%s\n' 'install.sh: installing zstandard from PyPI (may compile on Termux) ...'
        "$python_bin" -m pip install zstandard || true
    else
        printf '%s\n' 'install.sh: warning - no zstd backend; conversion will fail.' >&2
        printf '%s\n' '  rerun with --with-deps, or unpack a build that ships wheels/.' >&2
    fi
else
    printf 'install.sh: zstd backend %s, nothing to install\n' "$zstd_backend"
fi

if [ "$src" = "$target" ]; then
    printf 'install.sh: already in place at %s\n' "$target"
else
    mkdir -p "$target" || exit 1
    cp -R "$src/app" "$target/" || exit 1
    cp "$src/bem-tools" "$target/" || exit 1
    if [ -f "$src/README.md" ]; then
        cp "$src/README.md" "$target/" || true
    fi
    if [ -d "$src/wheels" ]; then
        cp -R "$src/wheels" "$target/" || true
    fi
fi

chmod 755 "$target/bem-tools" || exit 1
mkdir -p "$target/tmp" 2>/dev/null || true

printf 'installed: %s\n' "$target"
printf '%s\n' '--- doctor ---'
TMPDIR=${TMPDIR:-$target/tmp} BEM_PYTHON=$python_bin sh "$target/bem-tools" doctor
status=$?
printf '%s\n' '-------------'
if [ "$status" -ne 0 ]; then
    printf '%s\n' 'doctor reported issues; fix them before converting.' >&2
fi
printf 'run it as: %s/bem-tools <command> [args...]\n' "$target"
exit 0
