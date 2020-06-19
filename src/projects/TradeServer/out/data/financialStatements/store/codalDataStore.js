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
        //todo: fix this commented by m.talebi
        /*reports = new DataGrouper<IN30Report>(reports)
            .groupBy(r => AppUtils.periodHash(r.rpYear, r.rpMonth))
            .sort((a, b) => b.publishTime - a.publishTime)
            .get();*/ //
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
        //todo: fixed commented by m.talebi
        // return balanceSheets
        return null;
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
                //todo: fixed commented by m.telebi
                // if (AppUtils.periodHash(sac.rpYear, sac.rpMonth) > AppUtils.periodHash(report.rpYear, report.rpMonth)) {
                //     // ignore forecasts
                //     return;
                // }
                data.push({
                    ...sac,
                    _report: report
                });
            });
        }
        //todo: fixed commented by m.talebi
        // return new DataGrouper(data)
        //     .groupBy(r => r.smYear)
        //     .sort((a, b) => b._report.publishTime - a._report.publishTime)
        //     .get();
        return null;
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