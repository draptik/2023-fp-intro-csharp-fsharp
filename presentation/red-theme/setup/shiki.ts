import { defineShikiSetup } from '@slidev/types'
import { readFileSync } from 'fs'

const COMMENT_COLORS = ['#4d7c0f', '#a8cc8c']

export default defineShikiSetup(() => {
  const light = JSON.parse(readFileSync(require.resolve('theme-vitesse/themes/vitesse-light.json'), 'utf-8'))
  const dark = JSON.parse(readFileSync(require.resolve('theme-vitesse/themes/vitesse-dark.json'), 'utf-8'))

  return {
    theme: { light, dark },
    colorReplacements: {
      '#a0ada0': '#4d7c0f',
      '#758575dd': '#a8cc8c',
      '#758575': '#a8cc8c',
    },
    transformers: [
      {
        span(node) {
          const style = (node.properties.style as string) ?? ''
          if (COMMENT_COLORS.some(c => style.includes(c))) {
            this.addClassToHast(node, 'shiki-comment')
          }
        },
      },
    ],
  }
})
