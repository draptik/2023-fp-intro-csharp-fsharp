import { defineShikiSetup } from '@slidev/types'
import { readFileSync } from 'fs'

const COMMENT_COLORS = ['#4d7c0f', '#a8cc8c']

export default defineShikiSetup(() => {
  const light = JSON.parse(readFileSync(require.resolve('theme-vitesse/themes/vitesse-light.json'), 'utf-8'))
  const dark = JSON.parse(readFileSync(require.resolve('theme-vitesse/themes/vitesse-dark.json'), 'utf-8'))

  return {
    theme: { light, dark },
    colorReplacements: {
      // vitesse-light comment (#a0ada0) -> darker green for better contrast on light bg
      '#a0ada0': '#4d7c0f',
      // vitesse-dark comment (#758575, with and without alpha) -> lighter green for dark bg
      '#758575dd': '#a8cc8c',
      '#758575': '#a8cc8c',
    },
    transformers: [
      {
        // We use the `line` hook (not `span`) because we need to insert sibling spans.
        // The Vitesse grammar includes indented whitespace in the comment token, so a
        // line like "    // foo" arrives as a single span containing "    // foo".
        // We split it: a plain span for the leading whitespace (no background) and a
        // shiki-comment span for the "// foo" part (gets the green background via CSS).
        line(node) {
          const newChildren: typeof node.children = []
          for (const child of node.children) {
            if (child.type !== 'element' || child.tagName !== 'span') {
              newChildren.push(child)
              continue
            }
            const style = (child.properties.style as string) ?? ''
            if (!COMMENT_COLORS.some(c => style.includes(c))) {
              newChildren.push(child)
              continue
            }
            const textNode = child.children[0]
            if (textNode?.type === 'text') {
              const wsMatch = textNode.value.match(/^(\s+)/)
              if (wsMatch) {
                const ws = wsMatch[1]
                const rest = textNode.value.slice(ws.length)
                // Plain span: whitespace only, no shiki-comment class → no background
                newChildren.push({
                  type: 'element',
                  tagName: 'span',
                  properties: {},
                  children: [{ type: 'text', value: ws }],
                })
                // Comment span: starts at // → background applies from here
                if (rest) {
                  const commentSpan = {
                    type: 'element' as const,
                    tagName: 'span',
                    properties: { ...child.properties },
                    children: [{ type: 'text' as const, value: rest }],
                  }
                  this.addClassToHast(commentSpan, 'shiki-comment')
                  newChildren.push(commentSpan)
                }
                continue
              }
            }
            // No leading whitespace — mark the span directly
            this.addClassToHast(child, 'shiki-comment')
            newChildren.push(child)
          }
          node.children = newChildren
        },
      },
    ],
  }
})
