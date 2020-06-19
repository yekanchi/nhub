import { IBalanceSheet, IIncomeStatement, IN10Letter } from "data/financialStatements";
import { IAsyncTaskResult } from "data/financialStatements/hook";
export declare function useIncomeStatementImpl(letter: IN10Letter): [IIncomeStatement[], IAsyncTaskResult];
export declare function useIncomeStatementsImpl(letters: IN10Letter[]): [IIncomeStatement[][], IAsyncTaskResult];
export declare function useBalanceSheetImpl(letter: IN10Letter): [IBalanceSheet[], IAsyncTaskResult];
export declare function useBalanceSheetsImpl(letters: IN10Letter[]): [IBalanceSheet[][], IAsyncTaskResult];
//# sourceMappingURL=hooks.d.ts.map