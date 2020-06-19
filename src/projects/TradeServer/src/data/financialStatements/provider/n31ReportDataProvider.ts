import {
    IN31BuyInvestmentItem, IN31DividentIncomeItem,
    IN31IndustryGroupInvestmentItem,
    IN31PeriodicPortfolioInItem, IN31PeriodicPortfolioOutItem,
    IN31Report, IN31SellInvestmentItem,
    IN3xLetter
} from "..";
import {fetchReportPageContent} from "./utils";
import {ICodalN31ReportDataProvider} from "./index";

export enum N31SheetId {
    // صورت خلاصه سرمایه گذاریها به تفکیک گروه صنعت
    IndustryGroups = 3,
    // صورت وضعیت پورتفوی شرکتهای پذیرفته شده در بورس
    PeriodicPortfolioIn = 4,
    // صورت وضعیت پورتفوی شرکتهای خارج از بورس
    PeriodicPortfolioOut = 5,
    // صورت ريزمعاملات سهام - تحصیل شده
    BuyInvestments = 6,
    // صورت ريزمعاملات سهام - واگذار شده
    SellInvestments = 7,
    // درآمد حاصل از سود سهام محقق شده
    DividentsIncome = 17,
}

export class CodalN31ReportDataProvider implements ICodalN31ReportDataProvider {
    private readonly letter: IN3xLetter;
    private readonly defaultSheetId: N31SheetId;
    private readonly contentMap: Map<N31SheetId, string>;

    constructor(letter: IN3xLetter, defaultSheetId: N31SheetId) {
        this.letter = letter;
        this.defaultSheetId = defaultSheetId;
        this.contentMap = new Map<N31SheetId, string>();
    }

    parseReport(): Promise<IN31Report> {
        return Promise.resolve(undefined);
    }

    parseIndustryGroupInvestmentItems(): Promise<IN31IndustryGroupInvestmentItem[]> {
        return Promise.resolve([]);
    }

    parsePeriodicPortfolioInItems(): Promise<IN31PeriodicPortfolioInItem[]> {
        return Promise.resolve([]);
    }

    parsePeriodicPortfolioOutItems(): Promise<IN31PeriodicPortfolioOutItem[]> {
        return Promise.resolve([]);
    }

    parseBuyInvestmentItems(): Promise<IN31BuyInvestmentItem[]> {
        return Promise.resolve([]);
    }

    parseSellInvestmentItems(): Promise<IN31SellInvestmentItem[]> {
        return Promise.resolve([]);
    }

    parseDividentIncomeItems(): Promise<IN31DividentIncomeItem[]> {
        return Promise.resolve([]);
    }

    private async getContent(sheetId: N31SheetId): Promise<string> {
        if (this.contentMap.has(sheetId)) {
            return this.contentMap.get(sheetId);
        } else {
            const content = await fetchReportPageContent(this.letter.url, sheetId);
            this.contentMap.set(sheetId, content);
            return content;
        }
    }
}