<script>
  import '../app.css'
</script>

<nav class="bg-orange-500 text-white px-8 py-5 flex items-center justify-between shadow-md">
  <a href="/" class="text-2xl font-extrabold tracking-tight">PDX Run & Chug</a>
  <div class="flex gap-6 text-sm font-medium">
    <a href="https://www.strava.com" target="_blank" rel="noopener noreferrer" class="hover:text-orange-200 transition-colors">Strava</a>
    <a href="https://www.meetup.com" target="_blank" rel="noopener noreferrer" class="hover:text-orange-200 transition-colors">Meetup</a>
    <a href="https://www.instagram.com" target="_blank" rel="noopener noreferrer" class="hover:text-orange-200 transition-colors">Instagram</a>
  </div>
</nav>

<slot />
