# Getting Started

## Prerequisites

- [Node.js 22+](https://nodejs.org/)
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- A Google Sheets API key (only if using the `google` data source — see [Google Sheets Setup](#google-sheets-setup))

## Install Dependencies

```powershell
pnpm install
cd backend; dotnet restore; cd ..
```

## Data Sources

The backend supports three data sources, controlled by the `DataSource` setting:

- **`"google"`** (default) — pulls live data from Google Sheets. This is the production data source.
- **`"local"`** — reads from a local JSON file at `backend/Data/Seeds/localJsonSeed.json`. This is for local development of the application without an internet connection or API key.
- **`"mariadb"`** — reads from a local MariaDB database. This is for local development of the application without an internet connection or API key, and allows testing with a real database.

To switch, set `DataSource` in `backend/appsettings.json` or `backend/appsettings.Local.json`:

```json
{
  "DataSource": "local"
}
```

Or pass it as an environment variable:

```bash
DataSource=local dotnet run
```

### Google Sheets Setup

This is the default and production data source. Requires an API key and internet connection.

1. Create a Google Sheets API key in the [Google Cloud Console](https://console.cloud.google.com/apis/library/sheets.googleapis.com)
2. Make sure the Google Sheet is shared as "Anyone with the link"
3. Add the API key to `backend/appsettings.Local.json` (this file is gitignored):
   ```json
   {
     "GoogleSheets": {
       "ApiKey": "your_google_api_key_here"
     }
   }
   ```

### Local JSON Setup

No setup required. Uses the seed data file at `backend/Data/Seeds/localJsonSeed.json`. Just set the data source:

```json
{
  "DataSource": "local"
}
```

### MariaDB Setup

If using the `mariadb` data source, you need [Podman](https://podman.io/) installed.

1. From the project root, run the environment script to start a MariaDB container and seed the database:
   ```bash
   ./env/mariaDbUp.sh
   ```
2. Set `DataSource` to `mariadb` in `backend/appsettings.Local.json`:
   ```json
   {
     "DataSource": "mariadb"
   }
   ```

The default connection string in `appsettings.json` matches the container (`root` / `password` on port 3306).

## Adding a New Config Option

The app reads feature flags from the **Config** tab in Google Sheets. Each row has an `Option` name and a `Status` value (`TRUE` / `FALSE`). To add a new toggle:

1. **Google Sheet** — add a new row to the `Config` tab (e.g. `Show Foo? | TRUE`)
2. **Backend model** — add a `bool` property to `backend/Models/SiteConfig.cs`:
   ```csharp
   public bool ShowFoo { get; set; }
   ```
3. **Google Sheets parser** — add an `else if` in `GoogleSheetsService.GetConfigAsync()`:
   ```csharp
   else if (option.Equals("Show Foo?", StringComparison.OrdinalIgnoreCase))
       config.ShowFoo = status.Equals("TRUE", StringComparison.OrdinalIgnoreCase);
   ```
4. **Frontend type** — add the field to `SiteConfig` in `src/lib/types.ts`:
   ```ts
   export interface SiteConfig {
     showEvents: boolean
     showMap: boolean
     showFoo: boolean
   }
   ```
5. **Frontend default** — update the initial state in `src/routes/+page.svelte`:
   ```ts
   let config: SiteConfig = $state({ showEvents: false, showMap: false, showFoo: false })
   ```
6. **Frontend usage** — wrap the relevant section with `{#if config.showFoo}`.

All config values default to `false` if the row is missing or the Config tab can't be read.

## Run Locally

Run each in a separate terminal:

**Backend** (terminal 1):

```powershell
pnpm run dev:backend
```

**Frontend** (terminal 2):

```powershell
pnpm run dev:frontend
```

- Frontend: http://localhost:5173
- Backend: http://localhost:5000
- OpenAPI spec: http://localhost:5000/openapi/v1.json

The Vite dev server proxies `/api` requests to the backend automatically.

## Other Commands

| Command | Description |
| --- | --- |
| `pnpm run build` | Production build |
| `pnpm run preview` | Preview production build |
| `pnpm run check` | TypeScript type checking |
| `pnpm run lint` | ESLint + Prettier |
| `pnpm run format` | Auto-format code |
| `pnpm run test:unit` | Unit tests (Vitest) |
| `pnpm run test:e2e` | E2E tests (Playwright) |
