import Dexie from 'dexie';
import {
    IBalanceSheet,
    ICostOfGoods,
    IIncomeStatement,
    IN10Report,
    IN30ProductItem,
    IN30Report,
    ISaleAndCost
} from "..";

export interface IReportLinked {
    reportId: number,
}

interface IAutoIncrementID {
    id?: number,
}

class FinancialStatementsDatabase extends Dexie {
    // N10
    n10Reports: Dexie.Table<IN10Report, number>;
    incomeStatements: Dexie.Table<IIncomeStatement & IReportLinked & IAutoIncrementID, number>;
    balanceSheets: Dexie.Table<IBalanceSheet & IReportLinked & IAutoIncrementID, number>;
    salesAndCostsTrend: Dexie.Table<ISaleAndCost & IReportLinked & IAutoIncrementID, number>;
    costOfGoods: Dexie.Table<ICostOfGoods & IReportLinked & IAutoIncrementID, number>;

    // N30
    n30Reports: Dexie.Table<IN30Report, number>;
    n30ProductItems: Dexie.Table<IN30ProductItem & IReportLinked & IAutoIncrementID, number>;

    constructor() {
        super('FinancialStatementsDatabase');
        this.version(1).stores({
            // N10
            n10Reports: 'tracingNumber, symbol, period, rpYear, rpMonth, smYear, smMonth, sendTime, publishTime, audited, amend, amendedTracingNumber',
            incomeStatements: '++id, reportId',
            balanceSheets: '++id, reportId',
            salesAndCostsTrend: '++id, reportId',
            costOfGoods: '++id, reportId',

            // N30
            n30Reports: 'tracingNumber, symbol, rpYear, rpMonth, smYear, smMonth, sendTime, publishTime, amend, amendedTracingNumber',
            n30ProductItems: '++id, reportId, name',
        });

        // N10
        this.n10Reports = this.table('n10Reports');
        this.incomeStatements = this.table('incomeStatements');
        this.balanceSheets = this.table('balanceSheets');
        this.salesAndCostsTrend = this.table('salesAndCostsTrend');
        this.costOfGoods = this.table('costOfGoods');

        // N30
        this.n30Reports = this.table('n30Reports');
        this.n30ProductItems = this.table('n30ProductItems');
    }
}

export default new FinancialStatementsDatabase();