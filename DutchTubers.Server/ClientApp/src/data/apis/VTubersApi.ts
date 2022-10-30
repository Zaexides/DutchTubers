import type VTuber from "../models/VTuber";
import type CacheMeta from "../models/CacheMeta";

export default abstract class VTubersApi {
    static async Get(): Promise<VTuber[]> {
        const response = await fetch("/api/vtubers");
        return await response.json();
    }

    static async GetCacheMeta(): Promise<CacheMeta> {
        const response = await fetch("/api/vtubers/cache");
        return await response.json();
    }
};