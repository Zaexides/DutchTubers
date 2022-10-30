<script setup lang="ts">
    import { computed } from "vue";
    import ContentPanel from "./ContentPanel.vue";
    import type VTuber from "../data/models/VTuber";
    import type StreamInfo from "../data/models/StreamInfo";

    const props = defineProps<{
        vtuber: VTuber
    }>();

    const twitchURL = computed<string>(() => {
        const vtuber = props.vtuber;
        return `https://twitch.tv/${vtuber.username}`;
    });

    const isLive = computed<boolean>(() => {
        const vtuber = props.vtuber;
        return !!vtuber.streamInfo;
    });

    const streamInfo = computed<StreamInfo>(() => {
        const vtuber = props.vtuber;
        return vtuber.streamInfo ?? { game: undefined, title: "" };
    });
</script>

<template>
    <ContentPanel>
        <template #header>
            <div class="flex p-2 pr-4 space-x-2 text-gray-300">
                <div class="flex-[0_0_auto] rounded-full w-12 h-12 border-2 border-stone-700 overflow-hidden motion-safe:transition-colors motion-safe:duration-200 hover:border-orange-700 md:w-20 md:h-20">
                    <a :href="twitchURL" target="_blank">
                        <img :src="vtuber.profileImg" :alt="`Profile image of ${vtuber.username}`" />
                    </a>
                </div>
                <div class="flex-1 flex flex-col overflow-x-hidden">
                    <div class="flex justify-between items-center">
                        <a :href="twitchURL" target="_blank" class="motion-safe:transition-colors motion-safe:duration-200 hover:text-orange-700 hover:underline">
                            <h3 class="sm:text-lg">{{vtuber.username}}</h3>
                        </a>
                        <div v-if="isLive" class="relative font-semibold text-xs tracking-wider text-red-700 w-min h-min px-1 py-0.5 border-red-700 rounded-md border-2 motion-safe:animate-pulse">LIVE</div>
                    </div>
                    <div class="flex flex-col" v-if="isLive">
                        <div class="truncate" :title="streamInfo.title">
                            {{streamInfo.title}}
                        </div>
                        <div v-if="streamInfo.game" class="space-x-2">
                            <span>Playing:</span>
                            <span>{{streamInfo.game}}</span>
                        </div>
                    </div>
                </div>
            </div>
        </template>
        <template #default>
            {{vtuber.description}}
        </template>
    </ContentPanel>
</template>