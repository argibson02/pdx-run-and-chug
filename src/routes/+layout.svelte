<script lang="ts">
  import '../app.css'
  import type { SocialLink } from '$lib/types'
  import type { Snippet } from 'svelte'
  import { api } from '$lib/api'

  let { children }: { children: Snippet } = $props()

  let links: SocialLink[] = $state([])

  $effect(() => {
    api.get<SocialLink[]>('/runclub/links').then((data) => {
      links = data.filter((l) => l.url && l.url !== 'none')
    })
  })
</script>

<nav class="bg-orange-500 text-white px-8 py-5 flex items-center justify-between shadow-md">
  <a href="/" class="text-2xl font-extrabold tracking-tight">PDX Run & Chug</a>
  <div class="flex gap-6 text-base font-medium">
    {#each links as link}
      <a href={link.url} target="_blank" rel="noopener noreferrer" class="hover:text-orange-200 transition-colors">{link.name}</a>
    {/each}
  </div>
</nav>

{@render children()}
