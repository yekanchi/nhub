import {CodaDataGrabber , ICodalDataGrabber} from "./data/DataGrabber"
import {CodalPage, Letter} from "./domain/Domain";
import {IDumpRepository} from "./dal/DataAccessContract";
import {DumpRepository} from "./dal/DataAccess";


export class JobScheduler {
    private _dataGrabber: ICodalDataGrabber
    private _DumpRepository: IDumpRepository;

    constructor() {
        this._dataGrabber = new CodaDataGrabber();
        this._DumpRepository = new DumpRepository();
    }

    async storeCodaDump(codalPageJsonData: CodalPage): Promise<any> {
        codalPageJsonData.Letters.forEach(item=> {
            if(item.symbol == 'ونیکی'){
                this._DumpRepository.insert(item);
            }
            return true;

        })
    }

    public async RunGrabber(): Promise<void>{
        // const codaPageJsonData = await this._dataGrabber.getPage(1);
        // this.storeCodaDump(codaPageJsonData);
        let name = new Letter();
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