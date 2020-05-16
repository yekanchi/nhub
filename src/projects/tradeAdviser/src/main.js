const stockTools = require("./stockTools")
const apiService = require("./apiService")
const { mofidWatchList } = require("./staticObjects")
const momentj = require('moment-jalali')
const { HistoryResolution } = require("./staticObjects")

module.exports = {
    async showMofidListStatus() {
        setInterval(function () {
            mofidWatchList.forEach(
                s => {
                    apiService.getSymbolInfo(s, stockTools.processMofidList)
                }
            );
        }, 5000)
    },

    async getFullMarketData() {
        apiService.tadbirGetAll(stockTools.getAllValidSymbols);
    },

    async getSymbolHistory(symbol, historyResolution, fromJalaliData, toJalaliDate) {
        const Enum = {};
        var queryParams = {
            symbol: symbol,
            resolution: historyResolution,
            from: momentj(fromJalaliData, "jYYYY/jM/jD HH:mm:ss").unix(),
            to: momentj(toJalaliDate, "jYYYY/jM/jD HH:mm:ss").unix(),
        }
        var baseURL = "https://rlcwebapi.tadbirrlc.com/ChartData/history?";
        d = {}
        var d = await apiService.genericAPICall(baseURL, queryParams, stockTools.parseSymbolHistory, stockTools.processSymbolHistory);
        console.log(d);
        return d;
    },


    async processdata() {
        var data = await this.getSymbolHistory("IRO1MSMI0001", HistoryResolution.OneMinute, "1398/1/1", "1398/5/5");
    }
}