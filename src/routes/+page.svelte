<script lang="ts">
  import type { RunEvent, Location, OtherEvent, SiteConfig } from '$lib/types'
  import { api } from '$lib/api'
  import LocationMap from '$lib/components/LocationMap.svelte'
  import RunClubTable from '$lib/components/RunClubTable.svelte'
  import LocationsTable from '$lib/components/LocationsTable.svelte'
  import OtherEventsTable from '$lib/components/OtherEventsTable.svelte'

  let schedule: RunEvent[] = $state([])
  let locations: Location[] = $state([])
  let otherEvents: OtherEvent[] = $state([])
  let config: SiteConfig = $state({ showEvents: false, showMap: false })
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
    if (!nextEvent || !nextEvent.location) {
      console.log('[Page] No next event or location — location TBD')
      return null
    }
    const name = nextEvent.location.toLowerCase()
    const match = locations.find((l) => l.name.toLowerCase().includes(name) || name.includes(l.name.toLowerCase())) ?? null
    if (match) {
      console.log(`[Page] Next event "${nextEvent.location}" on ${nextEvent.date} matched location "${match.name}" (${match.latitude}, ${match.longitude})${nextEvent.notes ? ` — ${nextEvent.notes}` : ''}`)
    } else {
      console.log(`[Page] Next event "${nextEvent.location}" — no matching location found in directory`)
    }
    return match
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
      api.get<OtherEvent[]>('/runclub/other-events'),
      api.get<SiteConfig>('/runclub/config'),
    ])
      .then(([s, b, oe, cfg]) => {
        schedule = s
        locations = b
        otherEvents = oe
        config = cfg
        console.log(`[Page] Loaded ${s.length} schedule events, ${b.length} locations, ${oe.length} other events, showEvents=${cfg.showEvents}`)
      })
      .catch(() => {
        error = 'Failed to load data from the backend.'
      })
      .finally(() => {
        loading = false
      })
  })
</script>

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
        <RunClubTable events={[nextEvent]} />
        {#if config.showMap}
          <div class="mt-4">
            <LocationMap location={nextLocation()} label={nextEvent.location} />
          </div>
        {/if}
      {:else}
        <div class="bg-white rounded-lg shadow-sm p-6 text-center">
          <p class="text-xl font-semibold text-orange-500">Location TBD</p>
          <p class="text-gray-400 mt-1">Check back soon!</p>
        </div>
        {#if config.showMap}
          <div class="mt-4">
            <LocationMap location={null} label="" />
          </div>
        {/if}
      {/if}
    </section>

    {#if upcomingRest.length > 0}
      <section class="mb-10">
        <h2 class="text-2xl font-bold mb-4">Upcoming Locations</h2>
        <RunClubTable events={upcomingRest} />
      </section>
    {/if}

    <section class="mb-10">
      <h2 class="text-2xl font-bold mb-4">Past Locations</h2>
      {#if past.length === 0}
        <p class="text-gray-400">No previous runs.</p>
      {:else}
        <RunClubTable events={past} />
      {/if}
    </section>

    {#if config.showEvents && otherEvents.length > 0}
      <section class="mb-10">
        <h2 class="text-2xl font-bold mb-4">Other Events</h2>
        <OtherEventsTable events={otherEvents} />
      </section>
    {/if}

    <section class="mb-10">
      <h2 class="text-2xl font-bold mb-4">Locations we go to</h2>
      <LocationsTable {locations} />
    </section>
  {/if}
</main>
