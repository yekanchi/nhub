declare class codalPage {
    Total: number;
    Page: number;
    Letters: Array<any>;
}
export declare class JobScheduler {
    private _dataGrabber;
    constructor();
    storeCodaDump(codalPageJsonData: codalPage): void;
    RunGrabber(): Promise<void>;
}
export {};
//# sourceMappingURL=JobScheduler.d.ts.map