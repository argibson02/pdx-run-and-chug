<script lang="ts">
  import type { WeatherForecast } from '$lib/types'
  import { api } from '$lib/api'

  let forecasts: WeatherForecast[] = $state([])
  let loading = $state(true)
  let error = $state('')

  $effect(() => {
    api.get<WeatherForecast[]>('/weatherforecast')
      .then(data => { forecasts = data })
      .catch(() => { error = 'Failed to load data from the backend.' })
      .finally(() => { loading = false })
  })
</script>

<main class="max-w-3xl mx-auto mt-8 px-4">
  <h1 class="text-2xl font-bold mb-4">Svelte + .NET Boilerplate</h1>

  {#if loading}
    <p class="text-gray-500">Loading from C# backend...</p>
  {:else if error}
    <p class="text-red-500">{error}</p>
  {:else}
    <table class="w-full border-collapse">
      <thead>
        <tr>
          <th class="p-2 px-4 border border-gray-200 text-left bg-gray-100">Date</th>
          <th class="p-2 px-4 border border-gray-200 text-left bg-gray-100">Temp (°C)</th>
          <th class="p-2 px-4 border border-gray-200 text-left bg-gray-100">Summary</th>
        </tr>
      </thead>
      <tbody>
        {#each forecasts as forecast}
          <tr>
            <td class="p-2 px-4 border border-gray-200">{forecast.date}</td>
            <td class="p-2 px-4 border border-gray-200">{forecast.temperatureC}</td>
            <td class="p-2 px-4 border border-gray-200">{forecast.summary}</td>
          </tr>
        {/each}
      </tbody>
    </table>
  {/if}
</main>
