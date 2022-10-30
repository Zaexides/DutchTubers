<script setup lang="ts">
    import GitHubIcon from "../icons/GitHubIcon.vue";
</script>

<template>
    <header class="flex justify-between items-center h-10 bg-gradient-to-b from-gray-900 to-gray-800 border-b border-gray-900 px-4">
        <div class="flex items-center">
            <svg height="40" width="112" class="w-28">
                <line x1="4" y1="-8" x2="60" y2="48" style="stroke:rgb(234 88 12);stroke-width:16" />
                <line x1="44" y1="-8" x2="100" y2="48" style="stroke:rgb(234 88 12);stroke-width:16" />
            </svg>
            <div class="flex items-end space-x-2 ml-10">
                <span class="text-xl">DutchTubers</span>
                <span class="text-gray-700 hidden sm:block">-</span>
                <span class="text-gray-400 hidden sm:block">The list of Dutch VTubers!</span>
            </div>
        </div>
        <div class="flex items-center">
            <a href="https://github.com/Zaexides/DutchTubers" target="_blank" class="transition-colors duration-200 hover:text-orange-700"><GitHubIcon /></a>
        </div>
    </header>
</template>