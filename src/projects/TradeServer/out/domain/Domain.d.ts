import { ILetter } from "../data/financialStatements";
export declare class CodalPage {
    Total: number;
    Page: number;
    Letters: Array<Letter>;
}
export declare class User {
    id: number;
    userName: string;
    name: string;
    family: string;
    email: string;
    password: string;
    constructor(intial?: Partial<User>);
}
export declare class Letter implements ILetter {
    id: number;
    attachmentUrl: string;
    code: number;
    companyName: string;
    publishTime: number;
    sendTime: number;
    symbol: string;
    tracingNumber: number;
    url: string;
    constructor(intial?: Partial<User>);
}
//# sourceMappingURL=Domain.d.ts.map