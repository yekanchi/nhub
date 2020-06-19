import { IN31BuyInvestmentItem, IN31DividentIncomeItem, IN31IndustryGroupInvestmentItem, IN31PeriodicPortfolioInItem, IN31PeriodicPortfolioOutItem, IN31Report, IN31SellInvestmentItem, IN3xLetter } from "..";
import { ICodalN31ReportDataProvider } from "./index";
export declare enum N31SheetId {
    IndustryGroups = 3,
    PeriodicPortfolioIn = 4,
    PeriodicPortfolioOut = 5,
    BuyInvestments = 6,
    SellInvestments = 7,
    DividentsIncome = 17
}
export declare class CodalN31ReportDataProvider implements ICodalN31ReportDataProvider {
    private readonly letter;
    private readonly defaultSheetId;
    private readonly contentMap;
    constructor(letter: IN3xLetter, defaultSheetId: N31SheetId);
    parseReport(): Promise<IN31Report>;
    parseIndustryGroupInvestmentItems(): Promise<IN31IndustryGroupInvestmentItem[]>;
    parsePeriodicPortfolioInItems(): Promise<IN31PeriodicPortfolioInItem[]>;
    parsePeriodicPortfolioOutItems(): Promise<IN31PeriodicPortfolioOutItem[]>;
    parseBuyInvestmentItems(): Promise<IN31BuyInvestmentItem[]>;
    parseSellInvestmentItems(): Promise<IN31SellInvestmentItem[]>;
    parseDividentIncomeItems(): Promise<IN31DividentIncomeItem[]>;
    private getContent;
}
//# sourceMappingURL=n31ReportDataProvider.d.ts.map