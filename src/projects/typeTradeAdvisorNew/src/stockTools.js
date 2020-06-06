const momentj = require("moment-jalali")



class Stock {
  constructor() {
    this.symbol = '';
    this.isin = 'IR000000'
    this.closinPrice = ""

  }
}
module.exports = {


  processMofidList(item) {
    item.buyTargetDiff = Number((((item.lastPrice - item.bestBuyPrice) / item.bestBuyPrice) * 100).toFixed(2));
    item.totalProfitMargin = Number((((item.bestSellPrice - item.bestBuyPrice) / item.bestBuyPrice) * 100).toFixed(2));
    item.currentProfitMarginPercent = Number((((item.bestSellPrice - item.lastPrice) / item.lastPrice) * 100).toFixed(2));
    item.currentProfitMargin = Number((((item.bestSellPrice - item.lastPrice))).toFixed(2));
    if (this.currentTable == undefined) {
      this.currentTable = [];
    }

    this.currentTable.push(item);
    if (this.currentTable.length == 17) {
      console.clear();
      console.log("Last Update: " + (new momentj).format("HH:mm:ss").toString());
      this.currentTable.sort((a, b) => (a.currentProfitMarginPercent > b.currentProfitMarginPercent) ? 1 : -1)

      console.table(this.currentTable,
        [
          "name",
          "lastPrice",
          "bestBuyPrice",
          "bestSellPrice",
          "buyTargetDiff",
          "totalProfitMargin",
          "currentProfitMarginPercent",
          "currentProfitMargin"
        ]);
      this.currentTable = undefined;
    }
  },



  getAllValidSymbols(data) {
    i = 0;
    data.forEach(d => {


      console.log(d);
      i++;
    })
  },

  parseTadbirItem(item) {
    var stock = new Stock();

    // stock.symbol = item.
  },

  async parseSymbolHistory(rawData) {
    var paredData = [];

    for (let index = 0; index < rawData.c.length; index++) {
      var element = {};
      element.OpeningPrice = rawData.o[index];
      element.closingPrice = rawData.c[index];
      element.lowest = rawData.l[index];
      element.highest = rawData.h[index];
      element.volume = rawData.v[index];
      element.timeStamp = momentj.unix(rawData.t[index]).format("jYYYY/jM/jD HH:MM:ss").toString();

      paredData.push(element);
    }

    return paredData;
  },

  async processSymbolHistory(parsedData) {
    var volumes = parsedData.map(d => d.volume);
    var max = Math.max.apply(null, volumes);
    var x = parsedData.reduce(function (prev, curr) {
      return prev.volume > curr.volume ? prev : curr;
    });
    
    while (true) {
      
    }
    console.table(x);
  }
}

