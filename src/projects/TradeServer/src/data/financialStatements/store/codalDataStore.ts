import db, {IReportLinked} from  "../store/database";
import {
    ICostOfGoods,
    IIncomeStatement,
    IN10Letter,
    IN30ProductItem,
    IN30Report,
    IN3xLetter,
    ISaleAndCost,
    N30Type
} from "../index";
import {N10SheetId} from "../provider/n10ReportDataProvider";
import {ICodalDataStore} from "./index";
import {
    DefaultCodalN10ReportDataProvider,
    DefaultCodalN30ReportDataProvider,
    ICodalN10ReportDataProvider
} from '../provider'

export class CodalDataStore implements ICodalDataStore {

    async retrieveN30Report(letter: IN3xLetter): Promise<IN30Report> {
        let report: IN30Report = await db.n30Reports.where('tracingNumber').equals(letter.tracingNumber).first();
        if (report === undefined) {
            const provider = new DefaultCodalN30ReportDataProvider(letter);
            report = await provider.parseReport();
            await db.n30Reports.add(report);
            if (report.type === N30Type.PRODUCT) {
                const items = await provider.parseItems() as IN30ProductItem[];
                const reportLinkedItems: (IN30ProductItem & IReportLinked)[] = items.map(i => ({reportId: letter.tracingNumber, ...i}));
                await db.n30ProductItems.bulkAdd(reportLinkedItems);
            }
        }
        return report;
    }

    async loadN30ProductItems(tracingNumber: number): Promise<IN30ProductItem[]> {
        return await db.n30ProductItems.where('reportId').equals(tracingNumber).toArray();
    }

    async loadN30ProductItemHistory(letter: IN3xLetter, productName: string): Promise<[IN30Report[], IN30ProductItem[]]> {
        let reports = await db.n30Reports.where('symbol').equals(letter.symbol).toArray();
        reports = new DataGrouper<IN30Report>(reports)
            .groupBy(r => AppUtils.periodHash(r.rpYear, r.rpMonth))
            .sort((a, b) => b.publishTime - a.publishTime)
            .get();
        //reports = reports.slice(0, 12);
        const items = [];
        for (const report of reports) {
            const item = await db.n30ProductItems.where({reportId: report.tracingNumber, name: productName}).first();
            if (item) {
                items.push(item);
            }
        }
        return [reports, items];
    }

    async retrieveIncomeStatements(letter: IN10Letter): Promise<IIncomeStatement[]> {
        let incomeStatements = await db.incomeStatements.where('reportId').equals(letter.tracingNumber).toArray();
        if (incomeStatements.length === 0) {
            let provider = await this.retrieveN10Report(letter, N10SheetId.IncomeStatement);
            if (provider === null) {
                provider = new DefaultCodalN10ReportDataProvider(letter, N10SheetId.IncomeStatement);
            }
            let data = await provider.parseIncomeStatements();
            incomeStatements = data.map(d => ({
                reportId: letter.tracingNumber,
                ...d,
            }));
            await db.incomeStatements.bulkAdd(incomeStatements);
        }
        return incomeStatements;
    }

    async retrieveBalanceSheets(letter: IN10Letter) {
        let balanceSheets = await db.balanceSheets.where('reportId').equals(letter.tracingNumber).toArray();
        if (balanceSheets.length === 0) {
            let provider = await this.retrieveN10Report(letter, N10SheetId.BalanceSheet);
            if (provider === null) {
                provider = new DefaultCodalN10ReportDataProvider(letter, N10SheetId.BalanceSheet);
            }
            let data = await provider.parseBalanceSheets();
            balanceSheets = data.map(d => ({
                reportId: letter.tracingNumber,
                ...d,
            }));
            await db.balanceSheets.bulkAdd(balanceSheets);
        }
        return balanceSheets;
    }

    async retrieveSalesAndCostsTrend(letter: IN10Letter): Promise<ISaleAndCost[]> {
        let salesAndCosts = await db.salesAndCostsTrend.where('reportId').equals(letter.tracingNumber).toArray();
        if (salesAndCosts.length === 0) {
            let provider = await this.retrieveN10Report(letter, N10SheetId.InterpretativeReportPage1);
            if (provider === null) {
                provider = new DefaultCodalN10ReportDataProvider(letter, N10SheetId.InterpretativeReportPage1);
            }
            let data = await provider.parseSalesAndCostsTrend();
            salesAndCosts = data.map(d => ({
                reportId: letter.tracingNumber,
                ...d,
            }));
            await db.salesAndCostsTrend.bulkAdd(salesAndCosts);
        }
        return salesAndCosts;
    }

    async loadSalesAndCostsTrend(symbol: string): Promise<ISaleAndCost[]> {
        const reports = await db.n10Reports.where('symbol').equals(symbol).toArray();
        const data = [];
        for (const report of reports) {
            const salesAndCosts = await db.salesAndCostsTrend.where('reportId').equals(report.tracingNumber).toArray();
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

    async retrieveCostOfGoods(letter: IN10Letter): Promise<ICostOfGoods[]> {
        let costOfGoods = await db.costOfGoods.where('reportId').equals(letter.tracingNumber).toArray();
        if (costOfGoods.length === 0) {
            let provider = await this.retrieveN10Report(letter, N10SheetId.InterpretativeReportPage1);
            if (provider === null) {
                provider = new DefaultCodalN10ReportDataProvider(letter, N10SheetId.InterpretativeReportPage1);
            }
            let data = await provider.parseCostOfGoods();
            costOfGoods = data.map(d => ({
                reportId: letter.tracingNumber,
                ...d,
            }));
            await db.costOfGoods.bulkAdd(costOfGoods);
        }
        return costOfGoods;
    }

    private async retrieveN10Report(letter: IN10Letter, sheetId: N10SheetId): Promise<ICodalN10ReportDataProvider> {
        let report = await db.n10Reports.where('tracingNumber').equals(letter.tracingNumber).first();
        let provider: ICodalN10ReportDataProvider = null;
        if (report === undefined) {
            provider = new DefaultCodalN10ReportDataProvider(letter, sheetId);
            report = await provider.parseReport();
            await db.n10Reports.add(report);
        }
        return provider;
    }
}
