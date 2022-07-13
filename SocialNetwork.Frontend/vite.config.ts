import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// https://vitejs.dev/config/
export default defineConfig({
  base: "/SNetwork/",
  plugins: [react()],
  server: {
    https: false,
    port: 1234,
  },
});
