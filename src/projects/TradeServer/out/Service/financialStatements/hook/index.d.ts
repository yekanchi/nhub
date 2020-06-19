import { IBalanceSheet, IIncomeStatement, IN10Letter } from "data/financialStatements/index";
export interface IAsyncTaskResult {
    readonly loading: boolean;
    readonly failed: boolean;
    readonly failedMsg: string;
    readonly retry: () => void;
}
export declare const useIncomeStatement: (letter: IN10Letter) => [IIncomeStatement[], IAsyncTaskResult];
export declare const useIncomeStatements: (letters: IN10Letter[]) => [IIncomeStatement[][], IAsyncTaskResult];
export declare const useBalanceSheet: (letter: IN10Letter) => [IBalanceSheet[], IAsyncTaskResult];
export declare const useBalanceSheets: (letters: IN10Letter[]) => [IBalanceSheet[][], IAsyncTaskResult];
//# sourceMappingURL=index.d.ts.map