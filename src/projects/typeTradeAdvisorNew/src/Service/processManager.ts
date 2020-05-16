import { processManagerContext } from "../domain/processManagerContext";
import { bpaCliendResponse } from "../domain/response/bpaCliendResponse";
import { ProcessDto } from "../domain/types";
import apiClient from "./apiClient";


export class processManager {
    processManagerContext: processManagerContext;
    apiClient: apiClient;

    constructor(processManagerContext: processManagerContext) {
        this.processManagerContext = processManagerContext;
    }

    getProcesses(): bpaCliendResponse<Array<ProcessDto>> {
        var queryParams: Array<[string, string]>;
        queryParams.push(["systemId", this.processManagerContext.systemId.toString()])

        var resultStr = this.apiClient.get("processDefinition",queryParams);

        return null;
    }
}