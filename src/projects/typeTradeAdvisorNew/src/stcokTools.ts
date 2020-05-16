import * as jmoment from "jalali-moment"

export default class apiService {
  public static async parseSymbolHistory(rawData: any) {
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
    };

    return paredData;
  }
}

