import { defineStore } from "pinia";
import type VTuber from "../models/VTuber";

export type VTuberState = {
    vtubers?: VTuber[],
    cacheId?: string
};

export const useVTuberStore = defineStore("vtuber", {
    state: () => ({
        vtubers: undefined,
        cacheId: undefined
    } as VTuberState),
    actions: {
        store(vtubers: VTuber[], cacheId: string) {
            this.vtubers = vtubers;
            this.cacheId = cacheId;
        }
    }
});