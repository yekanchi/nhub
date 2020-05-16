"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const jmoment = require("jalali-moment");
const apiService_1 = require("./Service/apiService");
const stcokTools_1 = require("./stcokTools");
const fs = require("fs");
const fastcsv = require("fast-csv");
function showMofidListStatus() {
    let todayJalali = jmoment().locale('fa').format('YYYY/M/D');
    console.log("Hello Wolrd");
}
exports.showMofidListStatus = showMofidListStatus;
async function getSymbolHistory(symbol, historyResolution, fromJalaliData, toJalaliDate) {
    var queryParams = {
        symbol: symbol,
        resolution: historyResolution,
        from: jmoment(fromJalaliData, "jYYYY/jM/jD HH:mm:ss").unix(),
        to: jmoment(toJalaliDate, "jYYYY/jM/jD HH:mm:ss").unix(),
    };
    var baseURL = "https://rlcwebapi.tadbirrlc.com/ChartData/history?";
    let chartData;
    chartData = await apiService_1.default.get(baseURL, queryParams, stcokTools_1.default.parseSymbolHistory, "");
    let count = 0;
    let totalCount = 0;
    let sum = 0;
    var first = chartData[0].highest;
    chartData.forEach(element => {
        var disp = (element.highest - element.lowest) / element.OpeningPrice;
        totalCount++;
        console.log(disp);
        // if (disp < .0125) {
        var seperator = "\t";
        var endLine = "\r\n";
        fs.appendFile("./log.xls", element.highest - first + seperator + (100 * disp).toFixed(2) + endLine, (err) => {
            // throws an error, you could also catch it here
            if (err)
                throw err;
            // success case, the file was saved
            console.log('Lyric saved!');
        });
        count = count + 1;
        sum = sum + disp;
        // }
    });
    const ws = fs.createWriteStream("out.csv");
    fastcsv
        .write(chartData, { headers: true })
        .pipe(ws);
    console.log(count / totalCount);
    console.log("count: " + count);
    console.log("average: " + sum / count);
}
exports.getSymbolHistory = getSymbolHistory;
async function processdata() {
    // var data = await this.getSymbolHistory("IRO1MSMI0001", HistoryResolution.OneMinute, "1398/1/1", "1398/5/5");
}
exports.processdata = processdata;
//# sourceMappingURL=main.js.map