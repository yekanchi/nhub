import { IBalanceSheet, ICostOfGoods, IIncomeStatement, ILetter, IN10Letter, IN10Report, IN30Item, IN30Report, IN31BuyInvestmentItem, IN31DividentIncomeItem, IN31IndustryGroupInvestmentItem, IN31PeriodicPortfolioInItem, IN31PeriodicPortfolioOutItem, IN31Report, IN31SellInvestmentItem, IN3xLetter, ISaleAndCost } from "data/financialStatements";
import { N31SheetId } from "data/financialStatements/provider/n31ReportDataProvider";
import { N10SheetId } from "data/financialStatements/provider/n10ReportDataProvider";
export interface ICodalIndexDataProvider {
    fetchN10Letters(symbol: string): Promise<IN10Letter[]>;
    fetchN3xLetters(symbol: string): Promise<IN3xLetter[]>;
}
export interface ICodalN30ReportDataProvider {
    parseReport(): Promise<IN30Report>;
    parseItems(): Promise<IN30Item[]>;
}
export interface ICodalN10ReportDataProvider {
    parseReport(): Promise<IN10Report>;
    parseIncomeStatements(): Promise<IIncomeStatement[]>;
    parseBalanceSheets(): Promise<IBalanceSheet[]>;
    parseSalesAndCostsTrend(): Promise<ISaleAndCost[]>;
    parseCostOfGoods(): Promise<ICostOfGoods[]>;
    parseFutureManagementGoalsAndStrategies(): Promise<string>;
}
export interface ICodalN31ReportDataProvider {
    parseReport(): Promise<IN31Report>;
    parseIndustryGroupInvestmentItems(): Promise<IN31IndustryGroupInvestmentItem[]>;
    parsePeriodicPortfolioInItems(): Promise<IN31PeriodicPortfolioInItem[]>;
    parsePeriodicPortfolioOutItems(): Promise<IN31PeriodicPortfolioOutItem[]>;
    parseBuyInvestmentItems(): Promise<IN31BuyInvestmentItem[]>;
    parseSellInvestmentItems(): Promise<IN31SellInvestmentItem[]>;
    parseDividentIncomeItems(): Promise<IN31DividentIncomeItem[]>;
}
export interface CodalIndexDataProviderClass {
    new (): ICodalIndexDataProvider;
}
export interface CodalReportDataProviderClass<LetterType extends ILetter, ProviderType, SheetIdType = number> {
    new (letter: LetterType, defaultSheetId?: SheetIdType): ProviderType;
}
export declare const DefaultCodalIndexDataProvider: CodalIndexDataProviderClass;
export declare const DefaultCodalN30ReportDataProvider: CodalReportDataProviderClass<IN3xLetter, ICodalN30ReportDataProvider>;
export declare const DefaultCodalN31ReportDataProvider: CodalReportDataProviderClass<IN3xLetter, ICodalN31ReportDataProvider, N31SheetId>;
export declare const DefaultCodalN10ReportDataProvider: CodalReportDataProviderClass<IN10Letter, ICodalN10ReportDataProvider, N10SheetId>;
//# sourceMappingURL=index.d.ts.map