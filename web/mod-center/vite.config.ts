import { defineConfig } from "vite";
import preact from "@preact/preset-vite";
import { fileURLToPath } from "node:url";

export default defineConfig({
  root: fileURLToPath(new URL(".", import.meta.url)),
  base: "/endfield/",
  publicDir: fileURLToPath(new URL("public", import.meta.url)),
  plugins: [preact()],
  build: { outDir: "dist", target: "es2022", sourcemap: false },
  server: {
    host: "127.0.0.1", port: 5174,
    proxy: { "/endfield/api": "http://127.0.0.1:9017", "/endfield/auth": "http://127.0.0.1:9017" },
  },
});
