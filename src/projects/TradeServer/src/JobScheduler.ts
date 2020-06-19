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
                this._DumpRepository.create(item);
            }
            return true;

        })
    }

    public async RunGrabber(): Promise<void>{
        // const codaPageJsonData = await this._dataGrabber.getPage(1);
        // this.storeCodaDump(codaPageJsonData);
        let name = new Letter();
        name.id = 1;
        name.code = 233;
        name.symbol = "some symbol";
        await this._DumpRepository.create(name);
    }
}