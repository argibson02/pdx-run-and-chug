<script lang="ts">
  import type { RunEvent } from '$lib/types'

  let { events }: { events: RunEvent[] } = $props()
</script>

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
        <td class="p-3 px-6 font-medium">{event.location || 'TBD'}</td>
        <td class="p-3 px-6">
          <span class="inline-block text-xs font-semibold px-2 py-0.5 rounded-full
            {(event.status || 'In-Progress') === 'Canceled' ? 'bg-red-100 text-red-800' : (event.status || 'In-Progress') === 'In-Progress' ? 'bg-blue-100 text-blue-800' : 'bg-green-100 text-green-800'}">{event.status || 'In-Progress'}</span>
        </td>
        <td class="p-3 px-6 text-gray-500 italic">{event.notes ?? ''}</td>
      </tr>
    {/each}
  </tbody>
</table>
