import { IN30Item, IN30Report, IN3xLetter } from "data/financialStatements";
import { ICodalN30ReportDataProvider } from "data/financialStatements/provider";
export declare class CodalN30ReportDataProvider implements ICodalN30ReportDataProvider {
    private readonly letter;
    private report;
    private items;
    constructor(letter: IN3xLetter);
    parseItems(): Promise<IN30Item[]>;
    parseReport(): Promise<IN30Report>;
    private fetchAndParse;
    private static parseDescFields;
    private static parseIncomeFields;
    private static parseItems;
}
//# sourceMappingURL=n30ReportDataProvider.d.ts.map