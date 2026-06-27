<script lang="ts">
  import type { Location } from '$lib/types'

  let { locations }: { locations: Location[] } = $props()
</script>

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
