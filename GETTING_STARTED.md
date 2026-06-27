# Getting Started

## Prerequisites

- [Node.js 22+](https://nodejs.org/)
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- A Google Sheets API key (only if using the `google` data source — see [Google Sheets Setup](#google-sheets-setup))

## Install Dependencies

```powershell
npm install
cd backend; dotnet restore; cd ..
```

## Data Sources

The backend supports three data sources, controlled by the `DataSource` setting:

- **`"google"`** (default) — pulls live data from Google Sheets. This is the production data source.
- **`"local"`** — reads from a local JSON file at `backend/Data/seed.json`. This is for local development of the application without an internet connection or API key.
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

No setup required. Uses the seed data file at `backend/Data/seed.json`. Just set the data source:

```json
{
  "DataSource": "local"
}
```

### MariaDB Setup

If using the `mariadb` data source, you need [Podman](https://podman.io/) installed.

1. From the project root, run the environment script to start a MariaDB container and seed the database:
   ```bash
   ./env/envUp.sh
   ```
2. Set `DataSource` to `mariadb` in `backend/appsettings.Local.json`:
   ```json
   {
     "DataSource": "mariadb"
   }
   ```

The default connection string in `appsettings.json` matches the container (`root` / `password` on port 3306).

## Run Locally

Run each in a separate terminal:

**Backend** (terminal 1):

```powershell
npm run dev:backend
```

**Frontend** (terminal 2):

```powershell
npm run dev:frontend
```

- Frontend: http://localhost:5173
- Backend: http://localhost:5000
- OpenAPI spec: http://localhost:5000/openapi/v1.json

The Vite dev server proxies `/api` requests to the backend automatically.

## Other Commands

| Command | Description |
| --- | --- |
| `npm run build` | Production build |
| `npm run preview` | Preview production build |
| `npm run check` | TypeScript type checking |
| `npm run lint` | ESLint + Prettier |
| `npm run format` | Auto-format code |
| `npm run test:unit` | Unit tests (Vitest) |
| `npm run test:e2e` | E2E tests (Playwright) |
