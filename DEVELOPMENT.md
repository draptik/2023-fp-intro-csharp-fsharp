# Development Setup

## Prerequisites

This project uses [mise](https://mise.jdx.dev) to manage tool versions.

### Installing mise

**curl (Linux/macOS):**

```sh
curl https://mise.run | sh
```

**apt (Ubuntu/Debian):**

```sh
sudo add-apt-repository -y ppa:jdxcode/mise
sudo apt update -y
sudo apt install -y mise
```

**pacman (Arch Linux):**

```sh
sudo pacman -S mise
```

**Homebrew (macOS/Linux):**

```sh
brew install mise
```

**winget (Windows):**

```sh
winget install jdx.mise
```

**Scoop (Windows):**

```sh
scoop install mise
```

For all installation options see the [mise installation docs](https://mise.jdx.dev/installing-mise.html).

## Installing tools

From the repo root, run:

```sh
mise trust
mise install
```

This installs the versions defined in `mise.toml`:

| Tool               | Version  | Used for              |
|--------------------|----------|-----------------------|
| .NET               | 10.0.104 | C# and F# code        |
| Node               | lts      | Slidev presentation   |
| markdownlint-cli2  | 0.22     | Markdown linting      |

## Using nvm instead of mise (Node only)

A `.nvmrc` is provided in the `presentation/` folder for developers who prefer nvm:

```sh
cd presentation
nvm install
nvm use
```

Note: you will still need to install .NET manually if you don't use mise.

## Markdown linting

Run from the `presentation/` folder (where `.markdownlint-cli2.yaml` is located):

```sh
cd presentation
npm run lint
```

## Presentation (Slidev)

```sh
cd presentation
npm install
./start-presentation.sh
```
