const axios = require('axios');
const ticketTools = require("./ticketTools")
const { cities } = require("./ticketTools")
const apiService = require("./apiService")

console.log("hi")
console.table(cities);


origin = Object.values(cities)[0];
date = "1399/02/01";

var destination = Object.values(cities).filter(function (item) {
  if (item == origin) {
    return false;
  }
  return true;
});
function doSomething() {
  destination.map(destCity =>
    apiService.getTicketItems(ticketTools.ProcessItems, cities.Tehran, destCity, date)
  );
  console.clear();
}

setInterval(doSomething, 5000);