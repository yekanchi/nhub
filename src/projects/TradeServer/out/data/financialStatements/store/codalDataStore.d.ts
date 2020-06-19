import { ICostOfGoods, IIncomeStatement, IN10Letter, IN30ProductItem, IN30Report, IN3xLetter, ISaleAndCost } from "../index";
import { ICodalDataStore } from "./index";
export declare class CodalDataStore implements ICodalDataStore {
    retrieveN30Report(letter: IN3xLetter): Promise<IN30Report>;
    loadN30ProductItems(tracingNumber: number): Promise<IN30ProductItem[]>;
    loadN30ProductItemHistory(letter: IN3xLetter, productName: string): Promise<[IN30Report[], IN30ProductItem[]]>;
    retrieveIncomeStatements(letter: IN10Letter): Promise<IIncomeStatement[]>;
    retrieveBalanceSheets(letter: IN10Letter): Promise<any>;
    retrieveSalesAndCostsTrend(letter: IN10Letter): Promise<ISaleAndCost[]>;
    loadSalesAndCostsTrend(symbol: string): Promise<ISaleAndCost[]>;
    retrieveCostOfGoods(letter: IN10Letter): Promise<ICostOfGoods[]>;
    private retrieveN10Report;
}
//# sourceMappingURL=codalDataStore.d.ts.map