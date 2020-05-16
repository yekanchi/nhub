"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class processManager {
    constructor(processManagerContext) {
        this.processManagerContext = processManagerContext;
    }
    getProcesses() {
        var queryParams;
        queryParams.push(["systemId", this.processManagerContext.systemId.toString()]);
        var resultStr = this.apiClient.get("processDefinition", queryParams);
        return null;
    }
}
exports.processManager = processManager;
//# sourceMappingURL=processManager.js.map