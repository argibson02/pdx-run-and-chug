<script lang="ts">
  import type { RunEvent, Location } from '$lib/types'
  import { api } from '$lib/api'
  import LocationMap from '$lib/components/LocationMap.svelte'

  let schedule: RunEvent[] = $state([])
  let locations: Location[] = $state([])
  let loading = $state(true)
  let error = $state('')

  const today = new Date().toISOString().split('T')[0]

  let sorted = $derived(
    schedule
      .filter((e) => e.date >= today)
      .sort((a, b) => a.date.localeCompare(b.date)),
  )

  let nextEvent = $derived(sorted[0] ?? null)
  let upcomingRest = $derived(sorted.slice(1, 21))

  let nextLocation = $derived(() => {
    if (!nextEvent) return null
    const name = nextEvent.location.toLowerCase()
    return locations.find((l) => l.name.toLowerCase().includes(name) || name.includes(l.name.toLowerCase())) ?? null
  })

  let past = $derived(
    schedule
      .filter((e) => e.date < today)
      .sort((a, b) => b.date.localeCompare(a.date))
      .slice(0, 20),
  )

  $effect(() => {
    Promise.all([
      api.get<RunEvent[]>('/runclub/schedule'),
      api.get<Location[]>('/runclub/locations'),
    ])
      .then(([s, b]) => {
        schedule = s
        locations = b
      })
      .catch(() => {
        error = 'Failed to load data from the backend.'
      })
      .finally(() => {
        loading = false
      })
  })
</script>

{#snippet scheduleTable(events: RunEvent[])}
  <table class="w-full rounded-lg overflow-hidden shadow-sm">
    <thead>
      <tr class="bg-orange-100 text-orange-900">
        <th class="p-3 px-6 text-left font-semibold">Date</th>
        <th class="p-3 px-6 text-left font-semibold">Location</th>
        <th class="p-3 px-6 text-left font-semibold">Status</th>
        <th class="p-3 px-6 text-left font-semibold">Notes</th>
      </tr>
    </thead>
    <tbody>
      {#each events as event, i}
        <tr class="{i % 2 === 0 ? 'bg-white' : 'bg-orange-50'} hover:bg-orange-100 transition-colors">
          <td class="p-3 px-6">{event.date}</td>
          <td class="p-3 px-6 font-medium">{event.location}</td>
          <td class="p-3 px-6">
            <span class="inline-block bg-green-100 text-green-800 text-xs font-semibold px-2 py-0.5 rounded-full">{event.status}</span>
          </td>
          <td class="p-3 px-6 text-gray-500 italic">{event.notes ?? ''}</td>
        </tr>
      {/each}
    </tbody>
  </table>
{/snippet}

<main class="max-w-5xl mx-auto mt-10 px-6 pb-12">
  {#if loading}
    <p class="text-gray-400 text-lg animate-pulse">Loading schedule...</p>
  {:else if error}
    <p class="bg-red-100 text-red-700 px-4 py-3 rounded-lg">{error}</p>
  {:else}
    <section class="mb-10">
      <h2 class="text-2xl font-bold mb-4 flex items-center gap-2">
        Next Location
      </h2>
      {#if nextEvent}
        {@render scheduleTable([nextEvent])}
        <div class="mt-4">
          <LocationMap location={nextLocation()} label={nextEvent.location} />
        </div>
      {:else}
        <div class="bg-white rounded-lg shadow-sm p-6 text-center">
          <p class="text-xl font-semibold text-orange-500">Location TBD</p>
          <p class="text-gray-400 mt-1">Check back soon!</p>
        </div>
        <div class="mt-4">
          <LocationMap location={null} label="" />
        </div>
      {/if}
    </section>

    {#if upcomingRest.length > 0}
      <section class="mb-10">
        <h2 class="text-2xl font-bold mb-4">Upcoming Locations</h2>
        {@render scheduleTable(upcomingRest)}
      </section>
    {/if}

    <section class="mb-10">
      <h2 class="text-2xl font-bold mb-4">Past Locations</h2>
      {#if past.length === 0}
        <p class="text-gray-400">No previous runs.</p>
      {:else}
        {@render scheduleTable(past)}
      {/if}
    </section>

    <section class="mb-10">
      <h2 class="text-2xl font-bold mb-4">Locations we go to</h2>
      <table class="w-full rounded-lg overflow-hidden shadow-sm">
        <thead>
          <tr class="bg-orange-100 text-orange-900">
            <th class="p-3 px-6 text-left font-semibold">Name</th>
            <th class="p-3 px-6 text-left font-semibold">Address</th>
            <th class="p-3 px-6 text-left font-semibold">City</th>
          </tr>
        </thead>
        <tbody>
          {#each locations as location, i}
            <tr class="{i % 2 === 0 ? 'bg-white' : 'bg-orange-50'} hover:bg-orange-100 transition-colors">
              <td class="p-3 px-6 font-medium">{location.name}</td>
              <td class="p-3 px-6">{location.address}</td>
              <td class="p-3 px-6">{location.city}, {location.state} {location.zip}</td>
            </tr>
          {/each}
        </tbody>
      </table>
    </section>
  {/if}
</main>
