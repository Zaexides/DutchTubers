<script setup lang="ts">
    import { computed, useSlots } from "vue";

    defineProps<{
        title?: string,
        id?: string,
        cssClass?: string
    }>();

    const hasTitleSlot = computed<boolean>(() => {
        const slots = useSlots();
        return !!slots.header;
    });
</script>

<template>
    <div :class="`bg-stone-800 border border-stone-700 shadow-md sm:rounded ${cssClass}`" :id="id">
        <div v-if="title || hasTitleSlot" class="border-b border-stone-700">
            <slot name="header">
                <h2 class="px-4 py-2 text-lg text-gray-300">{{title}}</h2>
            </slot>
        </div>
        <div class="px-4 pt-2 pb-3">
            <slot />
        </div>
    </div>
</template>