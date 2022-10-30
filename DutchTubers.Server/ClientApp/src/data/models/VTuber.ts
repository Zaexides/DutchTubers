import type IStreamInfo from "./StreamInfo";

export default interface IVTuber {
    username: string,
    description: string,
    profileImg: string,
    streamInfo?: IStreamInfo
};