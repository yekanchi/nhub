const moment = require("moment-jalali")

module.exports = {
  cities: {
    Tehran: "11320000",
    Khoy: "57350000",
    Tabriz: "26310000",
    Urmia: "57310000",
    Shabestar: "26450000",
    Marand: "26370000",
    HadiShahr: "26381025",
    Maku: "57410000",
  },


  ProcessItems(ticketItems) {

    var availableItems = [];

    ticketItems.forEach(item => {
      desiredDateTime = new moment(item.DepartureDate + ' ' + item.DepartureTime, 'jYYYY/jM/jD HH:mm');
      var des = desiredDateTime.set({ hour: 20, minute: 0 });
      if (item.AvailableSeatCount > 0) {

        departureDateTime = moment(item.DepartureDate + ' ' + item.DepartureTime, 'jYYYY/jM/jD HH:mm');
        item.departureDateTime = departureDateTime.format("jYYYY/jM/jD HH:mm");
        availableItems.push(item);
      }
    });

    if (availableItems.length > 0) {
      console.log("count: " + availableItems.length);
      console.table(availableItems, [
        // "BusType",
        "OriginTerminalName",
        "DestinationTerminalName",
        "CompanyName",
        "AvailableSeatCount",
        "departureDateTime",
        "Price"]);
    }
  },
}

