const axios = require('axios');
const momentj = require('moment-jalali')

module.exports = {
  async getTicketItems(callback, origin, destination, strDate) {
    var date = momentj(strDate, "jYYYY/jM/jD");
    var dateURI = encodeURIComponent(date.format("jYYYY/jM/jD"));

    reqURI = `https://safar724.com/Bus/GetServices?origin=${origin}&destination=${destination}&date=${dateURI}`;
    const config = {
      method: 'get',
      url: reqURI
    }
    let res = await axios(config);
    ticketItems = res.data.Items;
    callback(ticketItems)
  },

}

