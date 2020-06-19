import { CodalPage } from "./domain/Domain";
export declare class JobScheduler {
    private _dataGrabber;
    private _DumpRepository;
    constructor();
    storeCodaDump(codalPageJsonData: CodalPage): Promise<any>;
    RunGrabber(): Promise<void>;
}
//# sourceMappingURL=JobScheduler.d.ts.map