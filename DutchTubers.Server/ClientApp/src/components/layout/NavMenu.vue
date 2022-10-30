<script setup lang="ts">
    import NavLink from "./NavLink.vue";
</script>

<template>
    <nav class="bg-zinc-900 border-b border-stone-700 sm:px-6">
        <ul class="flex space-x-2">
            <li><NavLink name="Index" href="/" exactMatchHrefForActive="true" /></li>
            <li><NavLink name="About" href="/about" /></li>
        </ul>
    </nav>
</template>