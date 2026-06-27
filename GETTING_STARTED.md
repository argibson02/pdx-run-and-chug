# Getting Started

## Prerequisites

- [Node.js 22+](https://nodejs.org/)
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- A Google Sheets API key ([setup guide](https://console.cloud.google.com/apis/library/sheets.googleapis.com))

## Install Dependencies

```powershell
npm install
cd backend; dotnet restore; cd ..
```

## Configure API Key

Create `backend/appsettings.Local.json` (this file is gitignored):

```json
{
  "GoogleSheets": {
    "ApiKey": "your_google_api_key_here"
  }
}
```

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
