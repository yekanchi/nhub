import {IBalanceSheet, IIncomeStatement, IN10Letter} from "../index";
import {
    useBalanceSheetImpl, useBalanceSheetsImpl,
    useIncomeStatementImpl,
    useIncomeStatementsImpl
} from "./hooks";

export interface IAsyncTaskResult {
    readonly loading: boolean,
    readonly failed: boolean,
    readonly failedMsg: string,
    readonly retry: () => void,
}

export const useIncomeStatement: (letter: IN10Letter) => [IIncomeStatement[], IAsyncTaskResult] = useIncomeStatementImpl;

export const useIncomeStatements: (letters: IN10Letter[]) => [IIncomeStatement[][], IAsyncTaskResult] = useIncomeStatementsImpl;

export const useBalanceSheet: (letter: IN10Letter) => [IBalanceSheet[], IAsyncTaskResult] = useBalanceSheetImpl;

export const useBalanceSheets: (letters: IN10Letter[]) => [IBalanceSheet[][], IAsyncTaskResult] = useBalanceSheetsImpl;