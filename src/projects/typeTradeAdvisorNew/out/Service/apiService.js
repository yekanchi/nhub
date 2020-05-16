"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const axios_1 = require("axios");
class apiService {
    static async get(baseURL, queryParams, dataParser, dataProcessor) {
        let reqURI = baseURL;
        //implements AxiosRequestConfig
        let config = {
            method: "GET",
            params: queryParams,
            url: baseURL
        };
        let response = await axios_1.default(config);
        let rawData = response.data;
        var parsedData = dataParser(rawData);
        return parsedData;
    }
}
exports.default = apiService;
//# sourceMappingURL=apiService.js.map