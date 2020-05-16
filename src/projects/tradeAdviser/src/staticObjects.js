const momentj = require("moment-jalali")

module.exports = {
  mofidWatchList: [
    {
      name: "Tipiko",
      isin: 'IRO1DTIP0001',
      initialBestBuyPrice: 15000,
      initialBestSellPrice: 2000,
      bestBuyPrice: 17000,
      bestSellPrice: 23000,
      introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))      
    },
    {
      name: "Fulad",
      isin: 'IRO1FOLD0001',
      initialBestBuyPrice:  4000,
      initialBestSellPrice: 5000,
      bestBuyPrice: 4000,
      bestSellPrice: 5000,
          introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))},
    {
      name: "Famelli",
      isin: 'IRO1MSMI0001',
      initialBestBuyPrice:  4200,
      initialBestSellPrice: 5000,
      bestBuyPrice: 4700,
      bestSellPrice:6000,
          introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))},
    {
      name: "VSandoogh",
      isin: 'IRO1SAND0001',
      initialBestBuyPrice:  3200,
      initialBestSellPrice: 3800,
      bestBuyPrice: 3200,
      bestSellPrice: 4000,
          introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))},
    {
      name: "Tayera",
      isin: 'IRO1TRIR0001',
      initialBestBuyPrice: 10000,
      initialBestSellPrice: 1350,
      bestBuyPrice: 12000,
      bestSellPrice: 15000,
      introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))      
    },
    {
      name: "Shuyande",
      isin: 'IRO1SHOY0001',
      initialBestBuyPrice: 11000,
      initialBestSellPrice: 1350,
      bestBuyPrice: 11000,
      bestSellPrice: 13500,
      introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))      
    },
    {
      name: "Knoor",
      isin: 'IRO1KNRZ0001',
      initialBestBuyPrice:  6300,
      initialBestSellPrice: 7500,
      bestBuyPrice: 6300,
      bestSellPrice: 7500,
          introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))},
    {
      name: "HKeshti",
      isin: 'IRO1KSHJ0001',
      initialBestBuyPrice:  5800,
      initialBestSellPrice: 7000,
      bestBuyPrice: 5800,
      bestSellPrice: 8000,
          introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))},,
    {
      name: "Vghadir",
      isin: 'IRO1GDIR0001',
      initialBestBuyPrice:  2200,
      initialBestSellPrice: 3000,
      bestBuyPrice: 2600,
      bestSellPrice: 3000,
          introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))},
    {
      name: "VKhavar",
      isin: 'IRO1BKHZ0001',
      initialBestBuyPrice:  3200,
      initialBestSellPrice: 3700,
      bestBuyPrice: 3600,
      bestSellPrice: 4500,
          introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))},
    {
      name: "Fajr",
      isin: 'IRO1FAJR0001',
      initialBestBuyPrice: 12500,
      initialBestSellPrice: 1600,
      bestBuyPrice: 12500,
      bestSellPrice: 16000,
      introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))      
    },
    {
      name: "Tanvin",
      isin: 'IRO1TNOV0001',
      initialBestBuyPrice:  2100,
      initialBestSellPrice: 3300,
      bestBuyPrice: 2800,
      bestSellPrice: 3500,
          introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))
	},
    {
      name: "Shiraz",
      isin: 'IRO1PSHZ0001',
      initialBestBuyPrice:  9500,
      initialBestSellPrice: 1250,
      bestBuyPrice: 9500,
      bestSellPrice: 12500,
      introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))      
    },
    {
      name: "VOmid",
      isin: 'IRO1PSHZ0001',
      initialBestBuyPrice:  9500,
      initialBestSellPrice: 1250,
      bestBuyPrice: 9500,
      bestSellPrice: 12500,
      introduceDate: (new momentj("1398/06/20", "jYYYY,jM,jD"))      
    }
  ],

  HistoryResolution: {
    Daily: "D",
    OneMinute: "1"
  },
}

