<script setup lang="ts">
    import ContentPanel from "./ContentPanel.vue";
</script>

<template>
    <div class="w-full h-full flex items-center justify-center">
        <ContentPanel>
            <div class="w-40 h-48 flex flex-col items-center justify-center">
                <div>
                    <svg width="28" height="28" class="animate-spin">
                        <circle cx="14" cy="14" r="10" stroke="rgb(28 25 23)" stroke-width="6" fill="none" />
                        <circle cx="14" cy="14" r="10" stroke="rgb(41 37 36)" stroke-width="2" fill="none" />
                        <path d="M4,14c0,10,13,11,13,8.5" stroke="rgb(234 88 12)" stroke-width="2" fill="none" />
                    </svg>
                </div>
                <div>
                    Loading...
                </div>
            </div>
        </ContentPanel>
    </div>
</template>