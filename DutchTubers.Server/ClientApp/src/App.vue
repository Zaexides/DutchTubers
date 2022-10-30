<script setup lang="ts">
    import { RouterView } from "vue-router";
    import TheHeader from "./components/layout/TheHeader.vue";
    import NavMenu from "./components/layout/NavMenu.vue";
</script>

<template>
    <div class="h-screen flex flex-col">
        <TheHeader />
        <NavMenu />
        <main class="overflow-x-hidden overflow-y-auto py-2 sm:py-6 sm:px-6">
            <RouterView />
        </main>
    </div>
</template>