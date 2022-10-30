<script setup lang="ts">
    import { RouterLink, useRoute } from "vue-router";
    import { computed, watch, reactive } from "vue";

    const props = defineProps<{
        name: string,
        href: string,
        exactMatchHrefForActive?: boolean
    }>();

    const route = useRoute();

    const state = reactive<{
        isActive: boolean
    }>({
        isActive: isActive()
    });

    watch(() => route.path, () => {
        state.isActive = isActive();
    });

    function isActive() {
        const location = window.location;
        const relativePath = location.pathname;
        if (props.exactMatchHrefForActive) {
            return relativePath === props.href;
        } else {
            return relativePath.startsWith(props.href);
        }
    }

    const anchorClasses = computed<string>(() => {
        return state.isActive
            ? "text-orange-700 border-b-orange-700"
            : "border-b-transparent";
    });
</script>

<template>
    <RouterLink :to="href" :class="`block px-4 py-2 text-sm font-semibold border-b-2 border-t border-t-transparent motion-safe:transition-colors motion-safe:duration-200 hover:text-orange-700 hover:border-b-orange-700 ${anchorClasses}`">
        {{name}}
    </RouterLink>
</template>