#!/bin/sh

# Invoke from this folder, otherwise change target location (`-o "..."`)
# Also adopt URL (last parameter)...

qrencode -s 6 -l H -o "./public/images/qr-code-magdeburger-devdays-2026-draptik.png" "https://draptik.github.io/2026-05-md-dev-days-fp-intro-csharp-fsharp"
