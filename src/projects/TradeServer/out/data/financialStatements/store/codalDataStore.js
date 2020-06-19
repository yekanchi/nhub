"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CodalDataStore = void 0;
const database_1 = require("../store/database");
const index_1 = require("../index");
const n10ReportDataProvider_1 = require("../provider/n10ReportDataProvider");
const provider_1 = require("../provider");
class CodalDataStore {
    async retrieveN30Report(letter) {
        let report = await database_1.default.n30Reports.where('tracingNumber').equals(letter.tracingNumber).first();
        if (report === undefined) {
            const provider = new provider_1.DefaultCodalN30ReportDataProvider(letter);
            report = await provider.parseReport();
            await database_1.default.n30Reports.add(report);
            if (report.type === index_1.N30Type.PRODUCT) {
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
        reports = new DataGrouper(reports)
            .groupBy(r => AppUtils.periodHash(r.rpYear, r.rpMonth))
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
                if (AppUtils.periodHash(sac.rpYear, sac.rpMonth) > AppUtils.periodHash(report.rpYear, report.rpMonth)) {
                    // ignore forecasts
                    return;
                }
                data.push({
                    ...sac,
                    _report: report
                });
            });
        }
        return new DataGrouper(data)
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
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiY29kYWxEYXRhU3RvcmUuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi8uLi8uLi9zcmMvZGF0YS9maW5hbmNpYWxTdGF0ZW1lbnRzL3N0b3JlL2NvZGFsRGF0YVN0b3JlLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztBQUFBLGdEQUFxRDtBQUNyRCxvQ0FTa0I7QUFDbEIsNkVBQTZEO0FBRTdELDBDQUlvQjtBQUVwQixNQUFhLGNBQWM7SUFFdkIsS0FBSyxDQUFDLGlCQUFpQixDQUFDLE1BQWtCO1FBQ3RDLElBQUksTUFBTSxHQUFlLE1BQU0sa0JBQUUsQ0FBQyxVQUFVLENBQUMsS0FBSyxDQUFDLGVBQWUsQ0FBQyxDQUFDLE1BQU0sQ0FBQyxNQUFNLENBQUMsYUFBYSxDQUFDLENBQUMsS0FBSyxFQUFFLENBQUM7UUFDekcsSUFBSSxNQUFNLEtBQUssU0FBUyxFQUFFO1lBQ3RCLE1BQU0sUUFBUSxHQUFHLElBQUksNENBQWlDLENBQUMsTUFBTSxDQUFDLENBQUM7WUFDL0QsTUFBTSxHQUFHLE1BQU0sUUFBUSxDQUFDLFdBQVcsRUFBRSxDQUFDO1lBQ3RDLE1BQU0sa0JBQUUsQ0FBQyxVQUFVLENBQUMsR0FBRyxDQUFDLE1BQU0sQ0FBQyxDQUFDO1lBQ2hDLElBQUksTUFBTSxDQUFDLElBQUksS0FBSyxlQUFPLENBQUMsT0FBTyxFQUFFO2dCQUNqQyxNQUFNLEtBQUssR0FBRyxNQUFNLFFBQVEsQ0FBQyxVQUFVLEVBQXVCLENBQUM7Z0JBQy9ELE1BQU0saUJBQWlCLEdBQXdDLEtBQUssQ0FBQyxHQUFHLENBQUMsQ0FBQyxDQUFDLEVBQUUsQ0FBQyxDQUFDLEVBQUMsUUFBUSxFQUFFLE1BQU0sQ0FBQyxhQUFhLEVBQUUsR0FBRyxDQUFDLEVBQUMsQ0FBQyxDQUFDLENBQUM7Z0JBQ3hILE1BQU0sa0JBQUUsQ0FBQyxlQUFlLENBQUMsT0FBTyxDQUFDLGlCQUFpQixDQUFDLENBQUM7YUFDdkQ7U0FDSjtRQUNELE9BQU8sTUFBTSxDQUFD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