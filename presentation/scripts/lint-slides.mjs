#!/usr/bin/env node
import { readFileSync } from 'fs';
import { spawnSync } from 'child_process';

const slides = readFileSync('./slides.md', 'utf8');
const files = [...slides.matchAll(/src: \.\/(.+\.md)/g)].map(m => m[1]);

const result = spawnSync('markdownlint-cli2', files, { stdio: 'inherit' });
process.exit(result.status ?? 0);
