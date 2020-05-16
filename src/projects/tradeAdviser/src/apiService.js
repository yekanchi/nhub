
const axios = require('axios');

module.exports = {
  async getSymbolInfo(item, callBack) {
    reqURI = `https://core.tadbirrlc.com//StockFutureInfoHandler.ashx?{"Type":"getLightSymbolInfoAndQueue","la":"fa","nscCode":"${item.isin}"}`;
    const config = {
      method: 'get',
      url: reqURI
    }
    let res = await axios(config);
    data = res.data;

    newItem = new Object;
    newItem.isin = item.isin;
    newItem.bestBuyPrice = item.bestBuyPrice;
    newItem.bestSellPrice = item.bestSellPrice;
    newItem.name = item.name;

    newItem.persianSymbol = data.symbolinfo.est.toString().replace(/[0-9]/g, '');
    newItem.closingPrice = data.symbolinfo.cp;
    newItem.lastPrice = data.symbolinfo.ltp;
    callBack(newItem)
  },

  async tadbirGetAll(callBack) {
    reqURI = `https://core.tadbirrlc.com//StocksHandler.ashx?{"Type":"ALL21","Lan":"Fa"}`;
    const config = {
      method: 'get',
      url: reqURI
    }
    let res = await axios(config);
    data = res.data;
    callBack(data)
  },
  async genericAPICall(baseURL, queryParams, dataParser, dataProcessor) {
    //qs = queryString.stringify(queryStrings);
    reqURI = baseURL;
    const config = {
      method: 'get',
      url: reqURI,
      params: queryParams,
    }
    let res = await axios(config);
    data = res.data;

    var parsedData = await dataParser(data);
    dataProcessor(parsedData);
    return data;
  },
}

