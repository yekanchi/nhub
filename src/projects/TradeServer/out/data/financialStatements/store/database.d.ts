import Dexie from 'dexie';
import { IBalanceSheet, ICostOfGoods, IIncomeStatement, IN10Report, IN30ProductItem, IN30Report, ISaleAndCost } from "..";
export interface IReportLinked {
    reportId: number;
}
interface IAutoIncrementID {
    id?: number;
}
declare class FinancialStatementsDatabase extends Dexie {
    n10Reports: Dexie.Table<IN10Report, number>;
    incomeStatements: Dexie.Table<IIncomeStatement & IReportLinked & IAutoIncrementID, number>;
    balanceSheets: Dexie.Table<IBalanceSheet & IReportLinked & IAutoIncrementID, number>;
    salesAndCostsTrend: Dexie.Table<ISaleAndCost & IReportLinked & IAutoIncrementID, number>;
    costOfGoods: Dexie.Table<ICostOfGoods & IReportLinked & IAutoIncrementID, number>;
    n30Reports: Dexie.Table<IN30Report, number>;
    n30ProductItems: Dexie.Table<IN30ProductItem & IReportLinked & IAutoIncrementID, number>;
    constructor();
}
declare const _default: FinancialStatementsDatabase;
export default _default;
//# sourceMappingURL=database.d.ts.map