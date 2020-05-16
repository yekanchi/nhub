"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const axios_1 = require("axios");
class apiClient {
    constructor(baseUrl) {
        this.baseUrl = baseUrl;
    }
    async get(address, queryParams) {
        //implements AxiosRequestConfig
        let config = {
            method: "GET",
            params: queryParams,
            url: this.baseUrl + address
        };
        let response = await axios_1.default(config);
        let rawData = response.data;
        return rawData;
    }
}
exports.default = apiClient;
//# sourceMappingURL=apiClient.js.map