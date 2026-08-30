import { defineConfig } from "vite";
import preact from "@preact/preset-vite";

export default defineConfig({
  base: "./",
  plugins: [preact()],
  build: {
    target: "es2022",
    sourcemap: true,
    assetsInlineLimit: 4096
  },
  worker: { format: "es" }
});
