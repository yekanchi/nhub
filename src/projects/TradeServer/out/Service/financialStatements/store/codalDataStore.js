"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CodalDataStore = void 0;
const database_1 = require("data/financialStatements/store/database");
const AppUtils_1 = require("utils/AppUtils");
const DataGrouper_1 = require("utils/DataGrouper");
const financialStatements_1 = require("data/financialStatements");
const n10ReportDataProvider_1 = require("data/financialStatements/provider/n10ReportDataProvider");
const provider_1 = require("data/financialStatements/provider");
class CodalDataStore {
    async retrieveN30Report(letter) {
        let report = await database_1.default.n30Reports.where('tracingNumber').equals(letter.tracingNumber).first();
        if (report === undefined) {
            const provider = new provider_1.DefaultCodalN30ReportDataProvider(letter);
            report = await provider.parseReport();
            await database_1.default.n30Reports.add(report);
            if (report.type === financialStatements_1.N30Type.PRODUCT) {
                const items = await provider.parseItems();
                const reportLinkedItems = items.map(i => ({ reportId: letter.tracingNumber, ...i }));
                await database_1.default.n30ProductItems.bulkAdd(reportLinkedItems);
            }
        }
        return report;
    }
    async loadN30ProductItems(tracingNumber) {
        return await database_1.default.n30ProductItems.where('reportId').equals(tracingNumber).toArray();
    }
    async loadN30ProductItemHistory(letter, productName) {
        let reports = await database_1.default.n30Reports.where('symbol').equals(letter.symbol).toArray();
        reports = new DataGrouper_1.DataGrouper(reports)
            .groupBy(r => AppUtils_1.AppUtils.periodHash(r.rpYear, r.rpMonth))
            .sort((a, b) => b.publishTime - a.publishTime)
            .get();
        //reports = reports.slice(0, 12);
        const items = [];
        for (const report of reports) {
            const item = await database_1.default.n30ProductItems.where({ reportId: report.tracingNumber, name: productName }).first();
            if (item) {
                items.push(item);
            }
        }
        return [reports, items];
    }
    async retrieveIncomeStatements(letter) {
        let incomeStatements = await database_1.default.incomeStatements.where('reportId').equals(letter.tracingNumber).toArray();
        if (incomeStatements.length === 0) {
            let provider = await this.retrieveN10Report(letter, n10ReportDataProvider_1.N10SheetId.IncomeStatement);
            if (provider === null) {
                provider = new provider_1.DefaultCodalN10ReportDataProvider(letter, n10ReportDataProvider_1.N10SheetId.IncomeStatement);
            }
            let data = await provider.parseIncomeStatements();
            incomeStatements = data.map(d => ({
                reportId: letter.tracingNumber,
                ...d,
            }));
            await database_1.default.incomeStatements.bulkAdd(incomeStatements);
        }
        return incomeStatements;
    }
    async retrieveBalanceSheets(letter) {
        let balanceSheets = await database_1.default.balanceSheets.where('reportId').equals(letter.tracingNumber).toArray();
        if (balanceSheets.length === 0) {
            let provider = await this.retrieveN10Report(letter, n10ReportDataProvider_1.N10SheetId.BalanceSheet);
            if (provider === null) {
                provider = new provider_1.DefaultCodalN10ReportDataProvider(letter, n10ReportDataProvider_1.N10SheetId.BalanceSheet);
            }
            let data = await provider.parseBalanceSheets();
            balanceSheets = data.map(d => ({
                reportId: letter.tracingNumber,
                ...d,
            }));
            await database_1.default.balanceSheets.bulkAdd(balanceSheets);
        }
        return balanceSheets;
    }
    async retrieveSalesAndCostsTrend(letter) {
        let salesAndCosts = await database_1.default.salesAndCostsTrend.where('reportId').equals(letter.tracingNumber).toArray();
        if (salesAndCosts.length === 0) {
            let provider = await this.retrieveN10Report(letter, n10ReportDataProvider_1.N10SheetId.InterpretativeReportPage1);
            if (provider === null) {
                provider = new provider_1.DefaultCodalN10ReportDataProvider(letter, n10ReportDataProvider_1.N10SheetId.InterpretativeReportPage1);
            }
            let data = await provider.parseSalesAndCostsTrend();
            salesAndCosts = data.map(d => ({
                reportId: letter.tracingNumber,
                ...d,
            }));
            await database_1.default.salesAndCostsTrend.bulkAdd(salesAndCosts);
        }
        return salesAndCosts;
    }
    async loadSalesAndCostsTrend(symbol) {
        const reports = await database_1.default.n10Reports.where('symbol').equals(symbol).toArray();
        const data = [];
        for (const report of reports) {
            const salesAndCosts = await database_1.default.salesAndCostsTrend.where('reportId').equals(report.tracingNumber).toArray();
            salesAndCosts.forEach(sac => {
                if (AppUtils_1.AppUtils.periodHash(sac.rpYear, sac.rpMonth) > AppUtils_1.AppUtils.periodHash(report.rpYear, report.rpMonth)) {
                    // ignore forecasts
                    return;
                }
                data.push({
                    ...sac,
                    _report: report
                });
            });
        }
        return new DataGrouper_1.DataGrouper(data)
            .groupBy(r => r.smYear)
            .sort((a, b) => b._report.publishTime - a._report.publishTime)
            .get();
    }
    async retrieveCostOfGoods(letter) {
        let costOfGoods = await database_1.default.costOfGoods.where('reportId').equals(letter.tracingNumber).toArray();
        if (costOfGoods.length === 0) {
            let provider = await this.retrieveN10Report(letter, n10ReportDataProvider_1.N10SheetId.InterpretativeReportPage1);
            if (provider === null) {
                provider = new provider_1.DefaultCodalN10ReportDataProvider(letter, n10ReportDataProvider_1.N10SheetId.InterpretativeReportPage1);
            }
            let data = await provider.parseCostOfGoods();
            costOfGoods = data.map(d => ({
                reportId: letter.tracingNumber,
                ...d,
            }));
            await database_1.default.costOfGoods.bulkAdd(costOfGoods);
        }
        return costOfGoods;
    }
    async retrieveN10Report(letter, sheetId) {
        let report = await database_1.default.n10Reports.where('tracingNumber').equals(letter.tracingNumber).first();
        let provider = null;
        if (report === undefined) {
            provider = new provider_1.DefaultCodalN10ReportDataProvider(letter, sheetId);
            report = await provider.parseReport();
            await database_1.default.n10Reports.add(report);
        }
        return provider;
    }
}
exports.CodalDataStore = CodalDataStore;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiY29kYWxEYXRhU3RvcmUuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi8uLi8uLi9zcmMvU2VydmljZS9maW5hbmNpYWxTdGF0ZW1lbnRzL3N0b3JlL2NvZGFsRGF0YVN0b3JlLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztBQUFBLHNFQUEwRTtBQUMxRSw2Q0FBd0M7QUFDeEMsbURBQThDO0FBQzlDLGtFQVNrQztBQUNsQyxtR0FBbUY7QUFFbkYsZ0VBSTBDO0FBRTFDLE1BQWEsY0FBYztJQUV2QixLQUFLLENBQUMsaUJBQWlCLENBQUMsTUFBa0I7UUFDdEMsSUFBSSxNQUFNLEdBQWUsTUFBTSxrQkFBRSxDQUFDLFVBQVUsQ0FBQyxLQUFLLENBQUMsZUFBZSxDQUFDLENBQUMsTUFBTSxDQUFDLE1BQU0sQ0FBQyxhQUFhLENBQUMsQ0FBQyxLQUFLLEVBQUUsQ0FBQztRQUN6RyxJQUFJLE1BQU0sS0FBSyxTQUFTLEVBQUU7WUFDdEIsTUFBTSxRQUFRLEdBQUcsSUFBSSw0Q0FBaUMsQ0FBQyxNQUFNLENBQUMsQ0FBQztZQUMvRCxNQUFNLEdBQUcsTUFBTSxRQUFRLENBQUMsV0FBVyxFQUFFLENBQUM7WUFDdEMsTUFBTSxrQkFBRSxDQUFDLFVBQVUsQ0FBQyxHQUFHLENBQUMsTUFBTSxDQUFDLENBQUM7WUFDaEMsSUFBSSxNQUFNLENBQUMsSUFBSSxLQUFLLDZCQUFPLENBQUMsT0FBTyxFQUFFO2dCQUNqQyxNQUFNLEtBQUssR0FBRyxNQUFNLFFBQVEsQ0FBQyxVQUFVLEVBQXVCLENBQUM7Z0JBQy9ELE1BQU0saUJBQWlCLEdBQXdDLEtBQUssQ0FBQyxHQUFHLENBQUMsQ0FBQyxDQUFDLEVBQUUsQ0FBQyxDQUFDLEVBQUMsUUFBUSxFQUFFLE1BQU0sQ0FBQyxhQUFhLEVBQUUsR0FBRyxDQUFDLEVBQUMsQ0FBQyxDQUFDLENBQUM7Z0JBQ3hILE1BQU0sa0JBQUUsQ0FBQyxlQUFlLENBQUMsT0FBTyxDQUFDLGlCQUFpQixDQUFDLENBQUM7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