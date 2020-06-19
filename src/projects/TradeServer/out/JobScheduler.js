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
                this._DumpRepository.create(item);
            }
            return true;
        });
    }
    async RunGrabber() {
        // const codaPageJsonData = await this._dataGrabber.getPage(1);
        // this.storeCodaDump(codaPageJsonData);
        let name = new Domain_1.Letter();
        name.id = 1;
        name.code = 233;
        name.symbol = "some symbol";
        await this._DumpRepository.create(name);
    }
}
exports.JobScheduler = JobScheduler;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiSm9iU2NoZWR1bGVyLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vc3JjL0pvYlNjaGVkdWxlci50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7QUFBQSxvREFBc0U7QUFDdEUsNENBQWtEO0FBRWxELGlEQUFnRDtBQUdoRCxNQUFhLFlBQVk7SUFJckI7UUFDSSxJQUFJLENBQUMsWUFBWSxHQUFHLElBQUksNkJBQWUsRUFBRSxDQUFDO1FBQzFDLElBQUksQ0FBQyxlQUFlLEdBQUcsSUFBSSwyQkFBYyxFQUFFLENBQUM7SUFDaEQsQ0FBQztJQUVELEtBQUssQ0FBQyxhQUFhLENBQUMsaUJBQTRCO1FBQzVDLGlCQUFpQixDQUFDLE9BQU8sQ0FBQyxPQUFPLENBQUMsSUFBSSxDQUFBLEVBQUU7WUFDcEMsSUFBRyxJQUFJLENBQUMsTUFBTSxJQUFJLE9BQU8sRUFBQztnQkFDdEIsSUFBSSxDQUFDLGVBQWUsQ0FBQyxNQUFNLENBQUMsSUFBSSxDQUFDLENBQUM7YUFDckM7WUFDRCxPQUFPLElBQUksQ0FBQztRQUVoQixDQUFDLENBQUMsQ0FBQTtJQUNOLENBQUM7SUFFTSxLQUFLLENBQUMsVUFBVTtRQUNuQiwrREFBK0Q7UUFDL0Qsd0NBQXdDO1FBQ3hDLElBQUksSUFBSSxHQUFHLElBQUksZUFBTSxFQUFFLENBQUM7UUFDeEIsSUFBSSxDQUFDLEVBQUUsR0FBRyxDQUFDLENBQUM7UUFDWixJQUFJLENBQUMsSUFBSSxHQUFHLEdBQUcsQ0FBQztRQUNoQixJQUFJLENBQUMsTUFBTSxHQUFHLGFBQWEsQ0FBQztRQUM1QixNQUFNLElBQUksQ0FBQyxlQUFlLENBQUMsTUFBTSxDQUFDLElBQUksQ0FBQyxDQUFDO0lBQzVDLENBQUM7Q0FDSjtBQTVCRCxvQ0E0QkMifQ==