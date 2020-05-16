import * as jmoment from "jalali-moment"
import apiService from "./Service/apiService"
import stockTools from "./stcokTools"
import chartTick from "./domain/chartTick"
import * as fs from "fs"
import * as fastcsv from "fast-csv"

export function showMofidListStatus() {
    let todayJalali = jmoment().locale('fa').format('YYYY/M/D');

    console.log("Hello Wolrd");
}

export async function getSymbolHistory(symbol, historyResolution, fromJalaliData, toJalaliDate) {
    var queryParams = {
        symbol: symbol,
        resolution: historyResolution,
        from: jmoment(fromJalaliData, "jYYYY/jM/jD HH:mm:ss").unix(),
        to: jmoment(toJalaliDate, "jYYYY/jM/jD HH:mm:ss").unix(),
    }
    var baseURL = "https://rlcwebapi.tadbirrlc.com/ChartData/history?";
    let chartData: chartTick[];
    chartData = await apiService.get(baseURL, queryParams, stockTools.parseSymbolHistory, "");

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
            if (err) throw err;

            // success case, the file was saved
            console.log('Lyric saved!');
        })
        count = count + 1;
        sum = sum + disp;
        // }
    });
    const ws = fs.createWriteStream("out.csv");
    fastcsv
        .write(chartData, { headers: true })
        .pipe(ws);
    console.log(count / totalCount)
    console.log("count: " + count);
    console.log("average: " + sum / count);

}


export async function processdata() {
    //var data = await this.getSymbolHistory("IRO1MSMI0001", HistoryResolution.OneMinute, "1398/1/1", "1398/5/5");
}