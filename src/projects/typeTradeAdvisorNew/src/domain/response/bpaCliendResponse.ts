import { Guid } from "guid-typescript";

export class bpaCliendResponse<T> {
    hasError: Boolean;
    userErrorMessage: String;
    DevErrorMessage: String;
    Messages: Array<bpaCientMessage>;
    Data: T;
    statusCode: Number;
    exception: String;
}

export class bpaCientMessage{
    id: Guid;
    bpaClientMessageType: BpaClientMessageType;
    userMessage: String;
    devMessage: String;
}

export enum BpaClientMessageType{
    error = 0,
    warning = 1
}


