/**
 * Minimal stand-in for a Cordis 4 context, faithful in the one behaviour that
 * already broke this bundle in a real install.
 *
 * A Cordis context is a proxy over the service table. Reading a service name
 * that the current fiber did not declare in `inject` throws
 *
 *   cannot get property "webServer" without inject
 *
 * rather than yielding `undefined`. A plain object stub cannot catch that class
 * of bug: an undeclared read looks like "service absent, degrade gracefully",
 * which is precisely the wrong conclusion. `test/host.test.mjs` asserts that this
 * harness still throws, so the fidelity cannot rot silently.
 *
 * One intentional deviation: a real Cordis *root* context (no fiber runtime)
 * returns `undefined` for an undeclared read, while this harness throws at every
 * level. Plugin code only ever runs inside a fiber, where the real proxy throws
 * as well, so the stricter bias catches mistakes earlier without ever reporting
 * a failure a real host would not have.
 *
 * Everything else is deliberately thin — only what the bundle touches:
 * `effect`, `get`, `inject`, `logger`, plus the three service tables.
 */

const SPECIAL = new Set(['then', 'prototype', 'toJSON'])

function isSpecial(prop) {
  return typeof prop === 'symbol' || SPECIAL.has(prop) || prop.startsWith('_')
}

export function createHarness({ inject: declared = [], services = {}, logger } = {}) {
  const registry = { ...services }
  const pending = []
  const log = []
  const rootState = { disposers: [], children: [] }

  function makeContext(injectNames, state) {
    const api = {
      /**
       * Record a disposer on the current fiber. Returned callable so a caller can
       * release early; matches the real signature closely enough.
       */
      effect(body) {
        const disposer = body()
        if (typeof disposer !== 'function') return () => {}
        state.disposers.push(disposer)
        return () => {
          const at = state.disposers.indexOf(disposer)
          if (at >= 0) state.disposers.splice(at, 1)
          disposer()
        }
      },

      /** Non-strict service lookup: never throws, returns undefined when absent. */
      get(name) {
        return registry[name]
      },

      /**
       * Start a child fiber that declares `deps`.
       *
       * Matching Cordis, the callback runs immediately when every dependency is
       * already provided, and stays pending otherwise until `provide` satisfies
       * it. The child's inject set is the union, which is what makes a service
       * readable inside the callback.
       */
      inject(deps, callback) {
        const names = [...new Set([...injectNames, ...deps])]
        const childState = { disposers: [], children: [] }
        const child = makeContext(names, childState)
        const fiber = {
          inject: [...deps],
          state: childState,
          done: false,
          run() {
            if (fiber.done) return
            fiber.done = true
            callback(child)
          },
        }
        state.children.push(fiber)
        if (deps.every((dep) => registry[dep] !== undefined)) fiber.run()
        else pending.push(fiber)
        return fiber
      },

      logger: {
        info(message) { log.push(['info', String(message)]) },
        warn(message) { log.push(['warn', String(message)]) },
        error(message) { log.push(['error', String(message)]) },
        ...(logger || {}),
      },
    }

    return new Proxy(api, {
      get(target, prop, receiver) {
        if (isSpecial(prop)) return Reflect.get(target, prop, receiver)
        if (Reflect.has(target, prop)) return Reflect.get(target, prop, receiver)
        if (injectNames.includes(prop)) return registry[prop]
        throw new Error(`cannot get property "${prop}" without inject`)
      },
      has(target, prop) {
        if (Reflect.has(target, prop)) return true
        return injectNames.includes(prop)
      },
    })
  }

  const ctx = makeContext(declared, rootState)

  return {
    ctx,
    log,
    /** Make a service available, releasing any fiber that was waiting for it. */
    provide(name, value) {
      registry[name] = value
      for (const fiber of [...pending]) {
        if (!fiber.inject.every((dep) => registry[dep] !== undefined)) continue
        pending.splice(pending.indexOf(fiber), 1)
        fiber.run()
      }
      return value
    },
    /** Withdraw a service and run the disposers of the fibers that owned it. */
    withdraw(name) {
      delete registry[name]
    },
    /** Tear the tree down, children before parents, newest disposer first. */
    dispose() {
      const walk = (state) => {
        for (const child of [...state.children].reverse()) walk(child.state)
        while (state.disposers.length) state.disposers.pop()()
      }
      walk(rootState)
    },
  }
}
