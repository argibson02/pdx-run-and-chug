<script lang="ts">
  import type { OtherEvent } from '$lib/types'

  let { events }: { events: OtherEvent[] } = $props()
</script>

<table class="w-full rounded-lg overflow-hidden shadow-sm">
  <thead>
    <tr class="bg-orange-100 text-orange-900">
      <th class="p-3 px-6 text-left font-semibold">Date</th>
      <th class="p-3 px-6 text-left font-semibold">Location</th>
      <th class="p-3 px-6 text-left font-semibold">Notes</th>
    </tr>
  </thead>
  <tbody>
    {#each events as event, i}
      <tr class="{i % 2 === 0 ? 'bg-white' : 'bg-orange-50'} hover:bg-orange-100 transition-colors">
        <td class="p-3 px-6">{event.date}</td>
        <td class="p-3 px-6 font-medium">{event.location || 'TBD'}</td>
        <td class="p-3 px-6 text-gray-500 italic">{event.notes ?? ''}</td>
      </tr>
    {/each}
  </tbody>
</table>
