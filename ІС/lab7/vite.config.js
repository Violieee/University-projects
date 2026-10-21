import { defineConfig } from 'vite'
import pugPlugin from 'vite-plugin-pug'
import { resolve } from 'node:path'

export default defineConfig({
  plugins: [
    pugPlugin({ localImports: true })
  ],
  build: {
    rollupOptions: {
      input: {
        main: resolve(process.cwd(), 'index.html'),
        project: resolve(process.cwd(), 'project.html'),
        create: resolve(process.cwd(), 'create.html'),
        edit: resolve(process.cwd(), 'edit.html'),
        evaluation: resolve(process.cwd(), 'evaluation.html'),
        scenarios: resolve(process.cwd(), 'scenarios.html'),
        employee: resolve(process.cwd(), 'employee.html')
      }
    }
  }
})
