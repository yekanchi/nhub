"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.useBalanceSheetsImpl = exports.useBalanceSheetImpl = exports.useIncomeStatementsImpl = exports.useIncomeStatementImpl = void 0;
const React = require("react");
const store_1 = require("../store");
class AsyncTaskResult {
    constructor(failed, failedMsg, loading, retryCallback) {
        this.failed = failed;
        this.failedMsg = failedMsg;
        this.loading = loading;
        this.retryCallback = retryCallback;
    }
    retry() {
        if (this.retryCallback) {
            this.retryCallback();
        }
        else {
            console.error('retry callback is not defined.');
        }
    }
}
function useIncomeStatementImpl(letter) {
    const letters = React.useMemo(() => [letter], [letter]);
    const [iss, result] = useIncomeStatementsImpl(letters);
    return [iss.length > 0 ? iss[0] : [], result];
}
exports.useIncomeStatementImpl = useIncomeStatementImpl;
function useIncomeStatementsImpl(letters) {
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
                const iss = await store_1.codalDataStore.retrieveIncomeStatements(letter);
                if (iss.length >= 2) {
                    incomeStatements.push(iss);
                }
                else {
                    throw 'Too few income statements!';
                }
            }
            return incomeStatements;
        }
        loadData()
            .then(iss => {
            if (!mounted)
                return;
            setState({
                incomeStatements: iss,
                loading: false,
                failed: false,
                failedMsg: null
            });
        })
            .catch(reason => {
            if (!mounted)
                return;
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
exports.useIncomeStatementsImpl = useIncomeStatementsImpl;
function useBalanceSheetImpl(letter) {
    const letters = React.useMemo(() => [letter], [letter]);
    const [bss, result] = useBalanceSheetsImpl(letters);
    return [bss.length > 0 ? bss[0] : [], result];
}
exports.useBalanceSheetImpl = useBalanceSheetImpl;
function useBalanceSheetsImpl(letters) {
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
                const bss = await store_1.codalDataStore.retrieveBalanceSheets(letter);
                if (bss.length >= 2) {
                    balanceSheets.push(bss);
                }
                else {
                    throw 'Too few balance sheets!';
                }
            }
            return balanceSheets;
        }
        loadData()
            .then(bss => {
            if (!mounted)
                return;
            setState({
                balanceSheets: bss,
                loading: false,
                failed: false,
                failedMsg: null
            });
        })
            .catch(reason => {
            if (!mounted)
                return;
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
exports.useBalanceSheetsImpl = useBalanceSheetsImpl;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiaG9va3MuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi8uLi8uLi9zcmMvZGF0YS9maW5hbmNpYWxTdGF0ZW1lbnRzL2hvb2svaG9va3MudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7O0FBQUEsK0JBQStCO0FBRy9CLG9DQUF3QztBQUV4QyxNQUFNLGVBQWU7SUFNakIsWUFBWSxNQUFlLEVBQUUsU0FBaUIsRUFBRSxPQUFnQixFQUFFLGFBQXlCO1FBQ3ZGLElBQUksQ0FBQyxNQUFNLEdBQUcsTUFBTSxDQUFDO1FBQ3JCLElBQUksQ0FBQyxTQUFTLEdBQUcsU0FBUyxDQUFDO1FBQzNCLElBQUksQ0FBQyxPQUFPLEdBQUcsT0FBTyxDQUFDO1FBQ3ZCLElBQUksQ0FBQyxhQUFhLEdBQUcsYUFBYSxDQUFDO0lBQ3ZDLENBQUM7SUFFRCxLQUFLO1FBQ0QsSUFBSSxJQUFJLENBQUMsYUFBYSxFQUFFO1lBQ3BCLElBQUksQ0FBQyxhQUFhLEVBQUUsQ0FBQztTQUN4QjthQUFNO1lBQ0gsT0FBTyxDQUFDLEtBQUssQ0FBQyxnQ0FBZ0MsQ0FBQyxDQUFDO1NBQ25EO0lBQ0wsQ0FBQztDQUNKO0FBRUQsU0FBZ0Isc0JBQXNCLENBQUMsTUFBa0I7SUFDckQsTUFBTSxPQUFPLEdBQUcsS0FBSyxDQUFDLE9BQU8sQ0FBQyxHQUFHLEVBQUUsQ0FBQyxDQUFDLE1BQU0sQ0FBQyxFQUFFLENBQUMsTUFBTSxDQUFDLENBQUMsQ0FBQztJQUN4RCxNQUFNLENBQUMsR0FBRyxFQUFFLE1BQU0sQ0FBQyxHQUFHLHVCQUF1QixDQUFDLE9BQU8sQ0FBQyxDQUFDO0lBQ3ZELE9BQU8sQ0FBQyxHQUFHLENBQUMsTUFBTSxHQUFHLENBQUMsQ0FBQyxDQUFDLENBQUMsR0FBRyxDQUFDLENBQUMsQ0FBQyxDQUFDLENBQUMsQ0FBQyxFQUFFLEVBQUUsTUFBTSxDQUFDLENBQUM7QUFDbEQsQ0FBQztBQUpELHdEQUlDO0FBRUQsU0FBZ0IsdUJBQXVCLENBQUMsT0FBcUI7SUFD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