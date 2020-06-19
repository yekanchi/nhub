"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.JobScheduler = void 0;
const DataGrabber_1 = require("./data/DataGrabber");
class codalPage {
}
class JobScheduler {
    constructor() {
        this._dataGrabber = new DataGrabber_1.CodaDataGrabber();
    }
    storeCodaDump(codalPageJsonData) {
        codalPageJsonData.Letters.forEach(item => {
            if (item.Symbol == 'ونیکی') {
                console.table(item);
            }
        });
    }
    async RunGrabber() {
        const codaPageJsonData = await this._dataGrabber.getPage(1);
        this.storeCodaDump(codaPageJsonData);
    }
}
exports.JobScheduler = JobScheduler;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiSm9iU2NoZWR1bGVyLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vc3JjL0pvYlNjaGVkdWxlci50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7QUFBQSxvREFBc0U7QUFJdEUsTUFBTSxTQUFTO0NBSWQ7QUFFRCxNQUFhLFlBQVk7SUFHckI7UUFDSSxJQUFJLENBQUMsWUFBWSxHQUFHLElBQUksNkJBQWUsRUFBRSxDQUFDO0lBQzlDLENBQUM7SUFFRCxhQUFhLENBQUMsaUJBQTRCO1FBQ3RDLGlCQUFpQixDQUFDLE9BQU8sQ0FBQyxPQUFPLENBQUMsSUFBSSxDQUFBLEVBQUU7WUFDcEMsSUFBRyxJQUFJLENBQUMsTUFBTSxJQUFJLE9BQU8sRUFBQztnQkFDdEIsT0FBTyxDQUFDLEtBQUssQ0FBQyxJQUFJLENBQUMsQ0FBQzthQUN2QjtRQUVMLENBQUMsQ0FBQyxDQUFBO0lBQ04sQ0FBQztJQUVNLEtBQUssQ0FBQyxVQUFVO1FBQ25CLE1BQU0sZ0JBQWdCLEdBQUcsTUFBTSxJQUFJLENBQUMsWUFBWSxDQUFDLE9BQU8sQ0FBQyxDQUFDLENBQUMsQ0FBQztRQUU1RCxJQUFJLENBQUMsYUFBYSxDQUFDLGdCQUFnQixDQUFDLENBQUM7SUFFekMsQ0FBQztDQUNKO0FBdEJELG9DQXNCQyJ9