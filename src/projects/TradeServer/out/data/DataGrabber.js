"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CodaDataGrabber = void 0;
const query_string_1 = require("query-string");
const fetch = require("node-fetch");
class CodaDataGrabber {
    async getPage(pageNumber) {
        let codalPageJsonData;
        const queryString = query_string_1.stringify({
            PageNumber: pageNumber,
        });
        await fetch('https://search.codal.ir/api/search/v2/q?' + queryString)
            .then(res => {
            codalPageJsonData = res.json();
        });
        return codalPageJsonData;
    }
}
exports.CodaDataGrabber = CodaDataGrabber;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiRGF0YUdyYWJiZXIuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi9zcmMvZGF0YS9EYXRhR3JhYmJlci50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7QUFBQSwrQ0FBdUM7QUFDdkMsTUFBTSxLQUFLLEdBQUcsT0FBTyxDQUFDLFlBQVksQ0FBQyxDQUFDO0FBTXBDLE1BQWEsZUFBZTtJQUV4QixLQUFLLENBQUMsT0FBTyxDQUFDLFVBQWtCO1FBQzVCLElBQUksaUJBQWlCLENBQUM7UUFFdEIsTUFBTSxXQUFXLEdBQUcsd0JBQVMsQ0FBQztZQUMxQixVQUFVLEVBQUUsVUFBVTtTQUN6QixDQUFDLENBQUM7UUFFSCxNQUFNLEtBQUssQ0FBQywwQ0FBMEMsR0FBRyxXQUFXLENBQUM7YUFDaEUsSUFBSSxDQUFDLEdBQUcsQ0FBQyxFQUFFO1lBQ1IsaUJBQWlCLEdBQUcsR0FBRyxDQUFDLElBQUksRUFBRSxDQUFDO1FBQ25DLENBQUMsQ0FBQyxDQUFDO1FBQ1AsT0FBUSxpQkFBaUIsQ0FBQztJQUM5QixDQUFDO0NBQ0o7QUFmRCwwQ0FlQyJ9