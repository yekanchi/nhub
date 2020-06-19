import { IBalanceSheet, ICostOfGoods, IIncomeStatement, IN10Letter, IN10Report, ISaleAndCost } from "data/financialStatements";
import { ICodalN10ReportDataProvider } from "data/financialStatements/provider";
export declare enum N10SheetId {
    BalanceSheet = 0,
    IncomeStatement = 1,
    InterpretativeReportPage1 = 20
}
export declare class CodalN10ReportDataProvider implements ICodalN10ReportDataProvider {
    private readonly letter;
    private readonly defaultSheetId;
    private readonly contentMap;
    constructor(letter: IN10Letter, defaultSheetId: N10SheetId);
    parseReport(): Promise<IN10Report>;
    parseBalanceSheets(): Promise<IBalanceSheet[]>;
    parseIncomeStatements(): Promise<IIncomeStatement[]>;
    parseCostOfGoods(): Promise<ICostOfGoods[]>;
    parseSalesAndCostsTrend(): Promise<ISaleAndCost[]>;
    parseFutureManagementGoalsAndStrategies(): Promise<string>;
    private getContent;
}
//# sourceMappingURL=n10ReportDataProvider.d.ts.map