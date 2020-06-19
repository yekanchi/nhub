export interface ICodalDataGrabber {
    getPage(pageNo: number): any;
}
export declare class CodaDataGrabber implements ICodalDataGrabber {
    getPage(pageNo: number): Promise<any>;
}
//# sourceMappingURL=DataGrabber.d.ts.map