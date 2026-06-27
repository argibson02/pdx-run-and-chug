<script lang="ts">
  import type { Location } from '$lib/types'
  import { browser } from '$app/environment'

  let { location, label }: { location: Location | null; label: string } = $props()

  let mapEl: HTMLDivElement | undefined = $state()

  // Default to Portland, OR
  const PORTLAND: [number, number] = [45.5152, -122.6784]

  $effect(() => {
    if (!browser || !mapEl) return

    let map: any

    import('leaflet').then(async (L) => {
      import('leaflet/dist/leaflet.css')

      L.default.Icon.Default.mergeOptions({
        iconUrl: '/marker-icon.png',
        iconRetinaUrl: '/marker-icon-2x.png',
        shadowUrl: '/marker-shadow.png',
        imagePath: '',
      })

      if (location) {
        console.log(`[Map] Showing: ${label} at ${location.latitude}, ${location.longitude} (${location.address})`)
      } else {
        console.log('[Map] No location found — showing Portland placeholder')
      }

      const center: [number, number] =
        location && location.latitude && location.longitude
          ? [location.latitude, location.longitude]
          : PORTLAND

      const zoom = location ? 15 : 12

      map = L.default.map(mapEl!).setView(center, zoom)

      L.default
        .tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
          attribution:
            '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>',
        })
        .addTo(map)

      if (location) {
        const marker = L.default.marker(center)
        marker.addTo(map)
        if (label) {
          marker.bindPopup(`<strong>${label}</strong><br>${location.address}`)
        }
      }
    })

    return () => {
      map?.remove()
    }
  })
</script>

<div bind:this={mapEl} class="w-full h-64 rounded-lg shadow-sm z-0"></div>
