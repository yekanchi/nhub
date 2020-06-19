"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CodaDataGrabber = void 0;
const fetch = require('node-fetch');
class CodaDataGrabber {
    async getPage(pageNo) {
        let codalPageJsonData;
        await fetch('https://search.codal.ir/api/search/v2/q?&PageNumber=1')
            .then(res => {
            codalPageJsonData = res.json();
        });
        return codalPageJsonData;
    }
}
exports.CodaDataGrabber = CodaDataGrabber;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiRGF0YUdyYWJiZXIuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi9zcmMvZGF0YS9EYXRhR3JhYmJlci50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7QUFBQSxNQUFNLEtBQUssR0FBRyxPQUFPLENBQUMsWUFBWSxDQUFDLENBQUM7QUFTcEMsTUFBYSxlQUFlO0lBRXhCLEtBQUssQ0FBQyxPQUFPLENBQUMsTUFBYztRQUN4QixJQUFJLGlCQUFpQixDQUFDO1FBQ3RCLE1BQU0sS0FBSyxDQUFDLHVEQUF1RCxDQUFDO2FBQy9ELElBQUksQ0FBQyxHQUFHLENBQUMsRUFBRTtZQUNSLGlCQUFpQixHQUFHLEdBQUcsQ0FBQyxJQUFJLEVBQUUsQ0FBQztRQUNuQyxDQUFDLENBQUMsQ0FBQztRQUVQLE9BQVEsaUJBQWlCLENBQUM7SUFDOUIsQ0FBQztDQUNKO0FBWEQsMENBV0MifQ==