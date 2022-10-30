<script setup lang="ts">
    import { ref, onMounted } from "vue";
    import type { Ref } from "vue";
    import LoadingPanel from "../components/LoadingPanel.vue";
    import VTuberProfile from "../components/VTuberProfile.vue";
    import VTubersApi from "../data/apis/VTubersApi";
    import type VTuber from "../data/models/VTuber";
    import { useVTuberStore } from "../data/stores/VTuberStore";

    const vtubers: Ref<VTuber[] | undefined> = ref(undefined);

    onMounted(async () => {
        const cacheMeta = await VTubersApi.GetCacheMeta();
        const store = useVTuberStore();

        if (cacheMeta.isOutdated || cacheMeta.id !== store.cacheId) {
            vtubers.value = await VTubersApi.Get();
            const cacheMeta = await VTubersApi.GetCacheMeta();
            store.store(vtubers.value, cacheMeta.id);
        } else {
            vtubers.value = store.vtubers;
        }
    });
</script>

<template>
    <div class="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 2xl:grid-cols-4 gap-4" v-if="vtubers">
        <VTuberProfile v-for="vtuber in vtubers" :vtuber="vtuber"></VTuberProfile>
    </div>
    <LoadingPanel v-else />
</template>
