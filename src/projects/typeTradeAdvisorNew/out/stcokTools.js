"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const jmoment = require("jalali-moment");
class apiService {
    static async parseSymbolHistory(rawData) {
        var paredData = [];
        for (let index = 0; index < rawData.c.length; index++) {
            let element = Object();
            element.OpeningPrice = rawData.o[index];
            element.closingPrice = rawData.c[index];
            element.lowest = rawData.l[index];
            element.highest = rawData.h[index];
            element.volume = rawData.v[index];
            element.timeStamp = jmoment.unix(rawData.t[index]).format("jYYYY/jM/jD HH:MM:ss").toString();
            paredData.push(element);
        }
        ;
        return paredData;
    }
}
exports.default = apiService;
//# sourceMappingURL=stcokTools.js.map