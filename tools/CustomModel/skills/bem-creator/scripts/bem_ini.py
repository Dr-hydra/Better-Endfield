"""Zero-dependency reader for 3Dmigoto-style source INI files.

Only the Python standard library is used on purpose: these helpers ship inside the
BEM skill package, where the tool's own modules (bem_v1, component_auto, ...) are
not guaranteed to be importable.

The parser is deliberately literal. It never "fixes" a source statement: a source
that writes ``ski`` instead of ``skip`` must stay ``ski``, because that is what the
game actually executes.
"""
from __future__ import annotations

import re
from pathlib import Path

__all__ = [
    "Section", "read_ini", "command_lines", "is_active_ini", "iter_active_inis",
    "parse_constants", "Condition", "Scenario",
]

# The loader takes everything up to the *first* ``]`` as the name and treats
# whatever follows on that line as non-command text, so the pattern must not be
# anchored at the end.
HEADER = re.compile(r"^\s*\[([^\]\n]+)\]")
ASSIGN = re.compile(
    r"^(?P<scope>global\s+persist|global|post|persist)?\s*(?P<name>\$[^\s=]+)\s*=\s*(?P<value>.*)$",
    re.I,
)

# Values that may legitimately follow a variable name in a condition.
_BINARY = ("||", "&&", "==", "!=", "<=", ">=", "<", ">", "+", "-", "*", "/", "%")
_TOKEN = re.compile(
    r"\s*(?:(?P<num>-?\d+(?:\.\d+)?)|(?P<var>\$[A-Za-z_][\w.]*)|"
    r"(?P<bare>[A-Za-z_]\w*)|"
    r"(?P<op>\|\||&&|==|!=|<=|>=|[<>+\-*/%!()]))"
)


class Section:
    """One ``[name]`` block with its raw body text."""

    __slots__ = ("name", "line", "body")

    def __init__(self, name: str, line: int, body: str):
        self.name = name
        self.line = line
        self.body = body

    @property
    def lines(self) -> list[str]:
        return command_lines(self.body)

    def value(self, key: str) -> str | None:
        """First ``key = value`` in the block, case-insensitive."""
        for line in self.lines:
            if "=" not in line:
                continue
            name, _, value = line.partition("=")
            if name.strip().lower() == key.lower():
                return value.strip()
        return None

    def __repr__(self) -> str:
        return f"<Section {self.name} line={self.line}>"


def command_lines(body: str) -> list[str]:
    """Strip ``;`` comments and blank lines, keep original case and spacing."""
    out = []
    for raw in body.splitlines():
        statement = raw.split(";", 1)[0].strip()
        if statement:
            out.append(statement)
    return out


def read_ini(path: str | Path) -> dict[str, Section]:
    """Parse an INI into ``{section name: Section}``, preserving file order.

    A repeated section name keeps its **first** occurrence, because that is what
    the loader does: ``ini_sections.emplace`` fails on the second one and the
    rest of that block is parsed and thrown away. Keeping the last copy instead
    silently reports the dead variant's contents. Use :mod:`bem_merge` when the
    duplicates themselves matter.
    """
    text = Path(path).read_text(encoding="utf-8-sig", errors="replace")
    text = text.replace("\r\n", "\n").replace("\r", "")
    sections: dict[str, Section] = {}
    current: Section | None = None
    for number, line in enumerate(text.split("\n"), 1):
        match = HEADER.match(line)
        if match:
            name = match.group(1).strip()
            if name in sections:
                # Dead duplicate: swallow its body without recording it.
                current = None
                continue
            current = Section(name, number, "")
            sections[current.name] = current
            continue
        if current is not None:
            current.body += line + "\n"
    return sections


def is_active_ini(name: str) -> bool:
    """A source INI is active unless a path segment marks it disabled/backup."""
    parts = Path(name.replace("\\", "/")).parts
    for part in parts:
        lowered = part.lower()
        if lowered.startswith("disabled") or lowered in {"backup", "backups"}:
            return False
    return name.lower().endswith(".ini")


def iter_active_inis(root: str | Path):
    """Yield ``(relative posix path, sections)`` for every non-disabled INI."""
    root = Path(root)
    for path in sorted(root.rglob("*.ini")):
        rel = path.relative_to(root).as_posix()
        if is_active_ini(rel):
            try:
                yield rel, read_ini(path)
            except OSError:
                continue


def parse_constants(sections: dict[str, Section]) -> tuple[dict[str, str], dict[str, str]]:
    """Split declared variables into ``(runtime, persisted)``.

    ``persist`` survives a game restart and is the reason a "default" state can be
    outside the key cycle. ``post`` is re-evaluated every frame and is not persisted.
    """
    runtime: dict[str, str] = {}
    persisted: dict[str, str] = {}
    for section in sections.values():
        if section.name.lower() != "constants":
            continue
        for line in section.lines:
            match = ASSIGN.match(line)
            if not match:
                continue
            scope = (match.group("scope") or "").lower()
            name, value = match.group("name"), match.group("value").strip()
            if "persist" in scope:
                persisted[name] = value
            else:
                runtime[name] = value
    return runtime, persisted


class Condition:
    """Static evaluator for the small expression subset 3Dmigoto conditions use.

    Supports numeric literals, ``$variables``, ``+ - * / %``, comparisons, ``&&``,
    ``||`` and ``!``. Anything unresolvable evaluates to ``None`` (unknown) instead
    of raising, so callers can distinguish "false" from "cannot tell".
    """

    def __init__(self, text: str):
        self.text = text.strip()
        self._tokens = self._tokenize(self.text)
        self._pos = 0

    @staticmethod
    def _tokenize(text: str) -> list[tuple[str, str]]:
        tokens, pos = [], 0
        while pos < len(text):
            match = _TOKEN.match(text, pos)
            if not match or match.end() == pos:
                # Unknown character: skip it rather than abandoning the whole
                # expression. Sources contain typos and odd punctuation, and a probe
                # that dies on one of them is useless.
                pos += 1
                continue
            pos = match.end()
            for kind in ("num", "var", "bare", "op"):
                if match.group(kind) is not None:
                    tokens.append((kind, match.group(kind)))
                    break
        return tokens

    # --- recursive descent -------------------------------------------------
    def _peek(self) -> tuple[str, str] | None:
        return self._tokens[self._pos] if self._pos < len(self._tokens) else None

    def _take(self) -> tuple[str, str]:
        token = self._tokens[self._pos]
        self._pos += 1
        return token

    def _accept(self, op: str) -> bool:
        token = self._peek()
        if token and token[0] == "op" and token[1] == op:
            self._pos += 1
            return True
        return False

    def evaluate(self, env: dict[str, float]) -> bool | None:
        try:
            value = self._or(env)
        except (ValueError, IndexError, ZeroDivisionError):
            return None
        return None if value is None else bool(value)

    def _or(self, env):
        left = self._and(env)
        while self._accept("||"):
            right = self._and(env)
            if left is True or right is True:
                left = True
            elif left is None or right is None:
                left = None
            else:
                left = False
        return left

    def _and(self, env):
        left = self._cmp(env)
        while self._accept("&&"):
            right = self._cmp(env)
            if left is False or right is False:
                left = False
            elif left is None or right is None:
                left = None
            else:
                left = True
        return left

    def _cmp(self, env):
        left = self._add(env)
        token = self._peek()
        if not token or token[0] != "op" or token[1] not in {"==", "!=", "<", ">", "<=", ">="}:
            return left
        op = self._take()[1]
        right = self._add(env)
        if left is None or right is None:
            return None
        return {
            "==": left == right, "!=": left != right, "<": left < right,
            ">": left > right, "<=": left <= right, ">=": left >= right,
        }[op]

    def _add(self, env):
        left = self._mul(env)
        while True:
            token = self._peek()
            if not token or token[0] != "op" or token[1] not in {"+", "-"}:
                return left
            op = self._take()[1]
            right = self._mul(env)
            if left is None or right is None:
                return None
            left = left + right if op == "+" else left - right

    def _mul(self, env):
        left = self._unary(env)
        while True:
            token = self._peek()
            if not token or token[0] != "op" or token[1] not in {"*", "/", "%"}:
                return left
            op = self._take()[1]
            right = self._unary(env)
            if left is None or right is None:
                return None
            if op == "*":
                left = left * right
            elif right == 0:
                return None
            else:
                left = left / right if op == "/" else left % right

    def _unary(self, env):
        if self._accept("!"):
            value = self._unary(env)
            return None if value is None else (not value)
        if self._accept("-"):
            value = self._unary(env)
            return None if value is None else -value
        return self._primary(env)

    def _primary(self, env):
        token = self._peek()
        if token is None:
            raise ValueError("Unexpected end of condition")
        kind, value = self._take()
        if kind == "num":
            return float(value)
        if kind in ("var", "bare"):
            # 3Dmigoto exposes pseudo-variables such as `vs`, `ps` and `DRAW_TYPE`
            # without a `$` prefix. Anything the environment does not define is
            # unknown, not zero: reporting "unknown" keeps the caller honest.
            if value not in env:
                return None
            return float(env[value])
        if value == "(":
            inner = self._or(env)
            if not self._accept(")"):
                raise ValueError("Missing closing parenthesis")
            return inner
        raise ValueError(f"Unexpected operator {value!r}")


class Scenario:
    """A fixed assignment of every variable, used to statically walk one state.

    Real source INIs branch on game-provided values that are not in the file
    (``$object_detected``, ``$lod_level``, instance counts). Those are supplied here
    as explicit assumptions, so a probe result always states what it assumed.
    """

    def __init__(self, values: dict[str, float], *, assume: dict[str, float] | None = None):
        self.values = dict(values)
        self.assumed = dict(assume or {})

    def resolve(self, expression: str) -> bool | None:
        try:
            return Condition(expression).evaluate(self.values)
        except (ValueError, IndexError, RecursionError):
            return None

    def fork(self, **overrides: float) -> "Scenario":
        child = Scenario(self.values, assume=self.assumed)
        child.values.update(overrides)
        return child
