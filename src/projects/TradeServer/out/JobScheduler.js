"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.JobScheduler = void 0;
const DataGrabber_1 = require("./data/DataGrabber");
const Domain_1 = require("./domain/Domain");
const DataAccess_1 = require("./dal/DataAccess");
class JobScheduler {
    constructor() {
        this._dataGrabber = new DataGrabber_1.CodaDataGrabber();
        this._DumpRepository = new DataAccess_1.DumpRepository();
    }
    async storeCodaDump(codalPageJsonData) {
        codalPageJsonData.Letters.forEach(item => {
            if (item.symbol == 'ونیکی') {
                this._DumpRepository.insert(item);
            }
            return true;
        });
    }
    async RunGrabber() {
        // const codaPageJsonData = await this._dataGrabber.getPage(1);
        // this.storeCodaDump(codaPageJsonData);
        let name = new Domain_1.Letter();
        name.code = 233;
        name.symbol = "some symbol";
        name.attachmentUrl = "some url";
        name.publishTime = (new Date()).getTime();
        name.sendTime = (new Date()).getTime();
        name.tracingNumber = 32323;
        name.url = "some url for you";
        await this._DumpRepository.insert(name);
    }
}
exports.JobScheduler = JobScheduler;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiSm9iU2NoZWR1bGVyLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vc3JjL0pvYlNjaGVkdWxlci50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7QUFBQSxvREFBc0U7QUFDdEUsNENBQWtEO0FBRWxELGlEQUFnRDtBQUdoRCxNQUFhLFlBQVk7SUFJckI7UUFDSSxJQUFJLENBQUMsWUFBWSxHQUFHLElBQUksNkJBQWUsRUFBRSxDQUFDO1FBQzFDLElBQUksQ0FBQyxlQUFlLEdBQUcsSUFBSSwyQkFBYyxFQUFFLENBQUM7SUFDaEQsQ0FBQztJQUVELEtBQUssQ0FBQyxhQUFhLENBQUMsaUJBQTRCO1FBQzVDLGlCQUFpQixDQUFDLE9BQU8sQ0FBQyxPQUFPLENBQUMsSUFBSSxDQUFBLEVBQUU7WUFDcEMsSUFBRyxJQUFJLENBQUMsTUFBTSxJQUFJLE9BQU8sRUFBQztnQkFDdEIsSUFBSSxDQUFDLGVBQWUsQ0FBQyxNQUFNLENBQUMsSUFBSSxDQUFDLENBQUM7YUFDckM7WUFDRCxPQUFPLElBQUksQ0FBQztRQUVoQixDQUFDLENBQUMsQ0FBQTtJQUNOLENBQUM7SUFFTSxLQUFLLENBQUMsVUFBVTtRQUNuQiwrREFBK0Q7UUFDL0Qsd0NBQXdDO1FBQ3hDLElBQUksSUFBSSxHQUFHLElBQUksZUFBTSxFQUFFLENBQUM7UUFDeEIsSUFBSSxDQUFDLElBQUksR0FBRyxHQUFHLENBQUM7UUFDaEIsSUFBSSxDQUFDLE1BQU0sR0FBRyxhQUFhLENBQUM7UUFDNUIsSUFBSSxDQUFDLGFBQWEsR0FBRyxVQUFVLENBQUM7UUFDaEMsSUFBSSxDQUFDLFdBQVcsR0FBRyxDQUFDLElBQUksSUFBSSxFQUFFLENBQUMsQ0FBQyxPQUFPLEVBQUUsQ0FBQztRQUMxQyxJQUFJLENBQUMsUUFBUSxHQUFHLENBQUMsSUFBSSxJQUFJLEVBQUUsQ0FBQyxDQUFDLE9BQU8sRUFBRSxDQUFDO1FBQ3ZDLElBQUksQ0FBQyxhQUFhLEdBQUcsS0FBSyxDQUFDO1FBQzNCLElBQUksQ0FBQyxHQUFHLEdBQUcsa0JBQWtCLENBQUM7UUFFOUIsTUFBTSxJQUFJLENBQUMsZUFBZSxDQUFDLE1BQU0sQ0FBQyxJQUFJLENBQUMsQ0FBQztJQUM1QyxDQUFDO0NBQ0o7QUFqQ0Qsb0NBaUNDIn0=