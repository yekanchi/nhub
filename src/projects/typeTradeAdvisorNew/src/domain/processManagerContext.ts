import { Guid } from "guid-typescript";
export class processManagerContext {
    apiAddress: String;
    systemId: Guid;
    constructor(apiAddress: String, systemId: Guid) {
        this.apiAddress = apiAddress;
        this.systemId = systemId;
    }
}
