import * as React from "react";
import {IBalanceSheet, IIncomeStatement, IN10Letter} from "../index";
import {IAsyncTaskResult} from "./index";
import {codalDataStore} from "../store";

class AsyncTaskResult implements IAsyncTaskResult {
    public readonly failed: boolean;
    public readonly failedMsg: string;
    public readonly loading: boolean;
    private readonly retryCallback: () => void;

    constructor(failed: boolean, failedMsg: string, loading: boolean, retryCallback: () => void) {
        this.failed = failed;
        this.failedMsg = failedMsg;
        this.loading = loading;
        this.retryCallback = retryCallback;
    }

    retry(): void {
        if (this.retryCallback) {
            this.retryCallback();
        } else {
            console.error('retry callback is not defined.');
        }
    }
}

export function useIncomeStatementImpl(letter: IN10Letter): [IIncomeStatement[], IAsyncTaskResult] {
    const letters = React.useMemo(() => [letter], [letter]);
    const [iss, result] = useIncomeStatementsImpl(letters);
    return [iss.length > 0 ? iss[0] : [], result];
}

export function useIncomeStatementsImpl(letters: IN10Letter[]): [IIncomeStatement[][], IAsyncTaskResult] {
    const [state, setState] = React.useState({
        incomeStatements: [],
        loading: false,
        failed: false,
        failedMsg: null
    });

    React.useEffect(() => {
        let mounted = true;
        setState({
            incomeStatements: [],
            loading: true,
            failed: false,
            failedMsg: null
        });

        async function loadData() {
            const incomeStatements = [];
            for (const letter of letters) {
                const iss = await codalDataStore.retrieveIncomeStatements(letter);
                if (iss.length >= 2) {
                    incomeStatements.push(iss);
                } else {
                    throw 'Too few income statements!';
                }
            }
            return incomeStatements;
        }

        loadData()
            .then(iss => {
                if (!mounted) return;
                setState({
                    incomeStatements: iss,
                    loading: false,
                    failed: false,
                    failedMsg: null
                });
            })
            .catch(reason => {
                if (!mounted) return;
                setState({
                    incomeStatements: [],
                    loading: false,
                    failed: true,
                    failedMsg: reason.toString(),
                });
            });
        return () => mounted = false;
    }, [letters]);

    return [
        state.incomeStatements,
        new AsyncTaskResult(state.failed, state.failedMsg, state.loading, null)
    ];
}

export function useBalanceSheetImpl(letter: IN10Letter): [IBalanceSheet[], IAsyncTaskResult] {
    const letters = React.useMemo(() => [letter], [letter]);
    const [bss, result] = useBalanceSheetsImpl(letters);
    return [bss.length > 0 ? bss[0] : [], result];
}

export function useBalanceSheetsImpl(letters: IN10Letter[]): [IBalanceSheet[][], IAsyncTaskResult] {
    const [state, setState] = React.useState({
        balanceSheets: [],
        loading: false,
        failed: false,
        failedMsg: null
    });

    React.useEffect(() => {
        let mounted = true;
        setState({
            balanceSheets: [],
            loading: true,
            failed: false,
            failedMsg: null
        });

        async function loadData() {
            const balanceSheets = [];
            for (const letter of letters) {
                const bss = await codalDataStore.retrieveBalanceSheets(letter);
                if (bss.length >= 2) {
                    balanceSheets.push(bss);
                } else {
                    throw 'Too few balance sheets!';
                }
            }
            return balanceSheets;
        }

        loadData()
            .then(bss => {
                if (!mounted) return;
                setState({
                    balanceSheets: bss,
                    loading: false,
                    failed: false,
                    failedMsg: null
                });
            })
            .catch(reason => {
                if (!mounted) return;
                setState({
                    balanceSheets: [],
                    loading: false,
                    failed: true,
                    failedMsg: reason.toString(),
                });
            });
        return () => mounted = false;
    }, [letters]);

    return [
        state.balanceSheets,
        new AsyncTaskResult(state.failed, state.failedMsg, state.loading, null)
    ];
}