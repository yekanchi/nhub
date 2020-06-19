import {CodaDataGrabber , ICodalDataGrabber} from "./data/DataGrabber"



class codalPage {
    Total: number;
    Page: number;
    Letters: Array<any>;
}

export class JobScheduler {

    private _dataGrabber: ICodalDataGrabber
    constructor() {
        this._dataGrabber = new CodaDataGrabber();
    }

    storeCodaDump(codalPageJsonData: codalPage) {
        codalPageJsonData.Letters.forEach(item=> {
            if(item.Symbol == 'ونیکی'){
                console.table(item);
            }

        })
    }

    public async RunGrabber(): Promise<void>{
        const codaPageJsonData = await this._dataGrabber.getPage(1);

        this.storeCodaDump(codaPageJsonData);

    }
}