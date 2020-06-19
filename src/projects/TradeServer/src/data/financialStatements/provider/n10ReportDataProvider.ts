import {
    IBalanceSheet,
    ICostOfGoods,
    IIncomeStatement,
    IN10Letter,
    IN10Report,
    ISaleAndCost
} from "..";
import {JSDOM} from 'jsdom';
import {AppUtils} from "utils/AppUtils";
import {FormatUtils} from "utils/FormatUtils";
import {DataSource, DataSourceCell} from "./dataSource";
import {fetchReportPageContent} from "./utils";
import {ICodalN10ReportDataProvider} from "./index";

export enum N10SheetId {
    BalanceSheet = 0,
    IncomeStatement = 1,
    InterpretativeReportPage1 = 20,
}

export class CodalN10ReportDataProvider implements ICodalN10ReportDataProvider {
    private readonly letter: IN10Letter;
    private readonly defaultSheetId: N10SheetId;
    private readonly contentMap: Map<N10SheetId, string>;

    constructor(letter: IN10Letter, defaultSheetId: N10SheetId) {
        this.letter = letter;
        this.defaultSheetId = defaultSheetId;
        this.contentMap = new Map<N10SheetId, string>();
    }

    async parseReport(): Promise<IN10Report> {
        const content = await this.getContent(this.defaultSheetId);
        const document = new JSDOM(content).window.document;
        let elem, text;

        // اصلاحیه
        let amend = false,
            amendedTracingNumber = 0,
            amendmentDesc = '';
        elem = document.getElementById('ctl00_cphBody_ucNavigateToNextPrevLetter_hlPrevVersion');
        if (elem) {
            amend = true;
            const matches = elem.textContent.match(/\( (\d+) \)/);
            if (matches) {
                amendedTracingNumber = parseInt(matches[1]);
            }
            elem = document.getElementById('ctl00_cphBody_ucNavigateToNextPrevLetter_lblCorrectionDesc');
            if (elem) {
                amendmentDesc = elem.textContent;
            }
        }

        // وضعیت حسابرسی
        let audited = false;
        elem = document.getElementById('ctl00_lblIsAudited');
        if (elem) {
            text = elem.textContent.trim();
            if (text.length > 0) {
                audited = elem.textContent.indexOf('ن') === -1;
            }
        }

        // سرمایه
        let capital = 0;
        elem = document.getElementById('ctl00_lblListedCapital');
        if (elem) {
            text = AppUtils.faDigits2En(elem.textContent.trim());
            capital = parseInt(AppUtils.removeNonDigits(text));
        }

        // دوره
        let period = 0,
            rpMonth = 0,
            rpYear = 0;
        elem = document.getElementById('ctl00_lblPeriod');
        if (elem) {
            text = AppUtils.faDigits2En(elem.textContent.trim());
            period = parseInt(AppUtils.removeNonDigits(text));
        }
        elem = document.getElementById('ctl00_lblPeriodEndToDate');
        if (elem) {
            text = AppUtils.faDigits2En(elem.textContent.trim());
            const [year, month] = AppUtils.parseJalaliDate(text);
            rpMonth = month;
            rpYear = year;
        }

        // سال مالی
        let smMonth = 0,
            smYear = 1;
        elem = document.getElementById('ctl00_lblYearEndToDate');
        if (elem) {
            text = AppUtils.faDigits2En(elem.textContent.trim());
            const [year, month] = AppUtils.parseJalaliDate(text);
            smMonth = month;
            smYear = year;
        }

        // تلفیقی
        let consolidated = false;
        elem = document.getElementById('ctl00_lblReportName');
        if (elem) {
            text = AppUtils.ar2fa(elem.textContent);
            consolidated = text.indexOf('تلفیقی') !== -1;
        }

        return {
            tracingNumber: this.letter.tracingNumber,
            symbol: this.letter.symbol,
            companyName: this.letter.companyName,
            sendTime: this.letter.sendTime,
            publishTime: this.letter.publishTime,
            url: this.letter.url,
            attachmentUrl: this.letter.attachmentUrl,
            code: this.letter.code,
            amend,
            amendedTracingNumber,
            amendmentDesc,
            audited,
            capital,
            period,
            rpMonth,
            rpYear,
            smMonth,
            smYear,
            consolidated,
        }
    }

    async parseBalanceSheets(): Promise<IBalanceSheet[]> {
        const content = await this.getContent(N10SheetId.BalanceSheet);
        const period = [],
            rpYear = [],
            rpMonth = [],
            audited = [],
            cashAndCachEquivalents = [],
            shortTermInvestments = [],
            tradeAndNonTradeReceivables = [],
            tradeReceivables = [],
            nonTradeReceivables = [],
            receivablesFromAffiliatedCompanies = [],
            inventories = [],
            prepayments = [],
            advancesToSupliers = [],
            nonCurrentAssetsForSale = [],
            totalCurrentAssets = [],
            longTermNotesAndAccountsReceivable = [],
            longTermInvestments = [],
            investmentProperty = [],
            intangibleAssets = [],
            plantAssetsNetOfAccumulatedDepreciation = [],
            capitalAdvances = [],
            otherNonCurrentAssets = [],
            totalNonCurrentAssets = [],
            totalAssets = [],
            tradeAndNonTradePayables = [],
            tradePayables = [],
            nonTradePayables = [],
            debtToAffiliatedComponies = [],
            deferredRevenue = [],
            currentTaxLiabilities = [],
            dividentsPayable = [],
            loanPayableCurrent = [],
            provisions = [],
            liabilitiesOfDisposalGroupsForSale = [],
            totalCurrentLiabilities = [],
            longTermPayables = [],
            nonCurrentDeferredRevenue = [],
            loanPayable = [],
            allowanceForPostRetirement = [],
            totalNonCurrentLiabilities = [],
            totalLiabilities = [],
            commonStock = [],
            inprocessCapitalIncrease = [],
            sharePremium = [],
            treasuryStock = [],
            treasuryStockConsume = [],
            receivesForCapitalAdvance = [],
            legalReserve = [],
            expansionReserve = [],
            retainedEarnings = [],
            revaluationSurplus = [],
            revaluationSurplusOfNonCurrentAssetsForSale = [],
            exchangeDifferences = [],
            exchangeReserveOfGovernmentalCorporations = [],
            totalStockHoldersEquity = [],
            totalLiabilitiesAndStockHoldersEquity = [];

        const build = i => ({
            period: period[i],
            rpYear: rpYear[i],
            rpMonth: rpMonth[i],
            audited: audited[i],
            cashAndCachEquivalents: cashAndCachEquivalents[i],
            shortTermInvestments: shortTermInvestments[i],
            tradeAndNonTradeReceivables: tradeAndNonTradeReceivables[i],
            tradeReceivables: tradeReceivables[i],
            nonTradeReceivables: nonTradeReceivables[i],
            receivablesFromAffiliatedCompanies: receivablesFromAffiliatedCompanies[i],
            inventories: inventories[i],
            prepayments: prepayments[i],
            advancesToSupliers: advancesToSupliers[i],
            nonCurrentAssetsForSale: nonCurrentAssetsForSale[i],
            totalCurrentAssets: totalCurrentAssets[i],
            longTermNotesAndAccountsReceivable: longTermNotesAndAccountsReceivable[i],
            longTermInvestments: longTermInvestments[i],
            investmentProperty: investmentProperty[i],
            intangibleAssets: intangibleAssets[i],
            plantAssetsNetOfAccumulatedDepreciation: plantAssetsNetOfAccumulatedDepreciation[i],
            capitalAdvances: capitalAdvances[i],
            otherNonCurrentAssets: otherNonCurrentAssets[i],
            totalNonCurrentAssets: totalNonCurrentAssets[i],
            totalAssets: totalAssets[i],
            tradeAndNonTradePayables: tradeAndNonTradePayables[i],
            tradePayables: tradePayables[i],
            nonTradePayables: nonTradePayables[i],
            debtToAffiliatedComponies: debtToAffiliatedComponies[i],
            deferredRevenue: deferredRevenue[i],
            currentTaxLiabilities: currentTaxLiabilities[i],
            dividentsPayable: dividentsPayable[i],
            loanPayableCurrent: loanPayableCurrent[i],
            provisions: provisions[i],
            liabilitiesOfDisposalGroupsForSale: liabilitiesOfDisposalGroupsForSale[i],
            totalCurrentLiabilities: totalCurrentLiabilities[i],
            longTermPayables: longTermPayables[i],
            nonCurrentDeferredRevenue: nonCurrentDeferredRevenue[i],
            loanPayable: loanPayable[i],
            allowanceForPostRetirement: allowanceForPostRetirement[i],
            totalNonCurrentLiabilities: totalNonCurrentLiabilities[i],
            totalLiabilities: totalLiabilities[i],
            commonStock: commonStock[i],
            inprocessCapitalIncrease: inprocessCapitalIncrease[i],
            sharePremium: sharePremium[i],
            treasuryStock: treasuryStock[i],
            treasuryStockConsume: treasuryStockConsume[i],
            receivesForCapitalAdvance: receivesForCapitalAdvance[i],
            legalReserve: legalReserve[i],
            expansionReserve: expansionReserve[i],
            retainedEarnings: retainedEarnings[i],
            revaluationSurplus: revaluationSurplus[i],
            revaluationSurplusOfNonCurrentAssetsForSale: revaluationSurplusOfNonCurrentAssetsForSale[i],
            exchangeDifferences: exchangeDifferences[i],
            exchangeReserveOfGovernmentalCorporations: exchangeReserveOfGovernmentalCorporations[i],
            totalStockHoldersEquity: totalStockHoldersEquity[i],
            totalLiabilitiesAndStockHoldersEquity: totalLiabilitiesAndStockHoldersEquity[i],
        });

        // دوره
        period[0] = this.letter.period;
        period[1] = 12;
        period[2] = 12;

        // NEW REPORTS
        const ds = DataSource.parseFromPageContent(content);
        if (ds) {
            let table = ds.getTable(1087) || ds.getTable(1155) || ds.getTable(1288);
            if (table && table.getColumnsCount() === 6 && table.getRowsCount() === 52) {
                const readRow = (arr, index) => {
                    arr[0] = table.getCell(index, 1).getNumberValue();
                    arr[1] = table.getCell(index, 2).getNumberValue();
                    arr[2] = table.getCell(index, 3).getNumberValue();
                };
                // دوره منتهی به
                [rpYear[0], rpMonth[0]] = table.getCell(0, 1).getReportDate();
                [rpYear[1], rpMonth[1]] = table.getCell(0, 2).getReportDate();
                [rpYear[2], rpMonth[2]] = table.getCell(0, 3).getReportDate();
                // حسابرسی شده یا نشده
                audited[0] = table.getCell(0, 1).getStringValue().indexOf('حسابرسی شده') !== -1;
                audited[1] = table.getCell(1, 2).getStringValue().indexOf('حسابرسی شده') !== -1;
                audited[2] = table.getCell(1, 3).getStringValue().indexOf('حسابرسی شده') !== -1;
                // دارايی‌های غیرجاری
                // دارایی‌های ثابت مشهود
                readRow(plantAssetsNetOfAccumulatedDepreciation, 4);
                // سرمایه‌گذاری در املاک
                readRow(investmentProperty, 5);
                // دارایی‌های نامشهود
                readRow(intangibleAssets, 6);
                // سرمایه‌گذاری‌های بلندمدت
                readRow(longTermInvestments, 7);
                // دریافتنی‌های بلندمدت
                readRow(longTermNotesAndAccountsReceivable, 8);
                // سایر دارایی‌ها
                readRow(otherNonCurrentAssets, 9);
                // جمع دارایی‌های غیرجاری
                readRow(totalNonCurrentAssets, 10);
                // دارایی‌های جاری
                // سفارشات و پیش‌پرداخت‌ها
                readRow(prepayments, 12);
                // موجودی مواد و کالا
                readRow(inventories, 13);
                // دریافتنی‌های تجاری و سایر دریافتنی‌ها
                readRow(tradeAndNonTradeReceivables, 14);
                //سرمایه‌گذاری‌های کوتاه‌مدت
                readRow(shortTermInvestments, 15);
                // موجودی نقد
                readRow(cashAndCachEquivalents, 16);
                // دارایی‌های نگهداری شده برای فروش
                readRow(nonCurrentAssetsForSale, 18);
                // جمع دارایی‌های جاری
                readRow(totalCurrentAssets, 19);
                // جمع دارایی‌ها
                readRow(totalAssets, 20);
                // حقوق مالکانه
                // سرمايه
                readRow(commonStock, 23);
                // افزایش سرمایه در جریان
                readRow(inprocessCapitalIncrease, 24);
                // صرف سهام
                readRow(sharePremium, 25);
                // صرف سهام خزانه
                readRow(treasuryStockConsume, 26);
                // اندوخته قانونی
                readRow(legalReserve, 27);
                // ساير اندوخته‌ها
                readRow(expansionReserve, 28);
                // مازاد تجدیدارزيابی دارایی‌ها
                readRow(revaluationSurplus, 29);
                // تفاوت تسعیر ارز عملیات خارجی
                readRow(exchangeDifferences, 30);
                // سود (زيان) انباشته
                readRow(retainedEarnings, 31);
                // سهام خزانه
                readRow(treasuryStock, 32);
                // جمع حقوق مالکانه
                readRow(totalStockHoldersEquity, 33);
                // بدهی‌های غیرجاری
                // پرداختنی‌های بلندمدت
                readRow(longTermPayables, 36);
                // تسهیلات مالی بلندمدت
                readRow(loanPayable, 37);
                // ذخیره مزایای پایان خدمت کارکنان
                readRow(allowanceForPostRetirement, 38);
                // جمع بدهی‌های غیرجاری
                readRow(totalNonCurrentLiabilities, 39);
                // بدهی‌های جاری
                // پرداختنی‌های تجاری و سایر پرداختنی‌ها
                readRow(tradeAndNonTradePayables, 41);
                // مالیات پرداختنی
                readRow(currentTaxLiabilities, 42);
                // سود سهام پرداختنی
                readRow(dividentsPayable, 43);
                // تسهیلات مالی
                readRow(loanPayableCurrent, 44);
                // ذخایر
                readRow(provisions, 45);
                // پیش‌دریافت‌ها
                readRow(deferredRevenue, 46);
                // بدهی‌های ‌مرتبط ‌با دارایی‌های نگهداری‌‌شده برای ‌فروش
                readRow(liabilitiesOfDisposalGroupsForSale, 48);
                // جمع بدهی‌های جاری
                readRow(totalCurrentLiabilities, 49);
                // جمع بدهی‌ها
                readRow(totalLiabilities, 50);
                // جمع حقوق مالکانه و بدهی‌ها
                readRow(totalLiabilitiesAndStockHoldersEquity, 51);

                return [build(0), build(1), build(2)];
            }

            table = ds.getTable(899) || ds.getTable(917);
            if (table && table.getColumnsCount() === 8 && table.getRowsCount() === 33) {
                const readRow = (index, arr1, arr2) => {
                    if (arr1) {
                        arr1[0] = table.getCell(index, 1).getNumberValue();
                        arr1[1] = table.getCell(index, 2).getNumberValue();
                    }
                    if (arr2) {
                        arr2[0] = table.getCell(index, 5).getNumberValue();
                        arr2[1] = table.getCell(index, 6).getNumberValue();
                    }
                };
                // دوره منتهی به
                [rpYear[0], rpMonth[0]] = table.getCell(0, 1).getReportDate();
                [rpYear[1], rpMonth[1]] = table.getCell(0, 2).getReportDate();
                // حسابرسی شده یا نشده
                audited[0] = table.getCell(0, 1).getStringValue().indexOf('حسابرسی شده') !== -1;
                audited[1] = table.getCell(0, 2).getStringValue().indexOf('حسابرسی شده') !== -1;
                // موجودی نقد / پرداختنی‌های تجاری
                readRow(3, cashAndCachEquivalents, tradePayables);
                // سرمایه‌گذاری‌‌های کوتاه مدت / پرداختنی‌های غیرتجاری
                readRow(4, shortTermInvestments, nonTradePayables);
                // دریافتنی‌‌های تجاری / مالیات پرداختنی
                readRow(5, tradeReceivables, currentTaxLiabilities);
                // دریافتنی‌‌های غیرتجاری / سود سهام پرداختنی
                readRow(6, nonTradeReceivables, dividentsPayable);
                // موجودی مواد و کالا / تسهیلات مالی
                readRow(7, inventories, loanPayableCurrent);
                // پیش پرداخت‌ها و سفارشات / ذخایر
                readRow(8, prepayments, provisions);
                // دارایی‌های نگهداری شده برای فروش / پیش‌دریافت‌های جاری
                readRow(9, nonCurrentAssetsForSale, deferredRevenue);
                // جمع دارایی‌های جاری / بدهی‌های مرتبط با دارایی‌های نگهداری شده برای فروش
                readRow(10, totalCurrentAssets, liabilitiesOfDisposalGroupsForSale);
                // جمع بدهی‌های جاری
                readRow(11, null, totalCurrentLiabilities);
                // دریافتنی‌‌های بلندمدت
                readRow(12, longTermNotesAndAccountsReceivable, null);
                // سرمایه‌گذاری‌های بلندمدت / پرداختنی‌های بلندمدت
                readRow(13, longTermInvestments, longTermPayables);
                // سرمایه‌گذاری در املاک / پیش‌دریافت‌های غیرجاری
                readRow(14, investmentProperty, nonCurrentDeferredRevenue);
                // دارایی‌های نامشهود / تسهیلات مالی بلندمدت
                readRow(15, intangibleAssets, loanPayable);
                // دارایی‌های ثابت مشهود / ذخیره مزایای پایان خدمت کارکنان
                readRow(16, plantAssetsNetOfAccumulatedDepreciation, allowanceForPostRetirement);
                // سایر دارایی‌ها / جمع بدهی‌های غیرجاری
                readRow(17, otherNonCurrentAssets, totalNonCurrentLiabilities);
                // جمع دارایی‌های غیرجاری / جمع بدهی‌ها
                readRow(18, totalNonCurrentAssets, totalLiabilities);
                // سرمایه
                readRow(20, null, commonStock);
                // افزایش (کاهش) سرمایه در جریان
                readRow(21, null, inprocessCapitalIncrease);
                // صرف (کسر) سهام
                readRow(22, null, sharePremium);
                // سهام خزانه
                readRow(23, null, treasuryStock);
                // اندوخته قانونی
                readRow(24, null, legalReserve);
                // سایر اندوخته‌ها
                readRow(25, null, expansionReserve);
                // مازاد تجدید ارزیابی دارایی‌های نگهداری شده برای فروش
                readRow(26, null, revaluationSurplusOfNonCurrentAssetsForSale);
                // مازاد تجدید ارزیابی دارایی‌ها
                readRow(27, null, revaluationSurplus);
                // تفاوت تسعیر ناشی از تبدیل به واحد پول گزارشگری
                readRow(28, null, exchangeDifferences);
                // اندوخته تسعیر ارز دارایی‌ها و بدهی‌های شرکت‌های دولتی
                readRow(29, null, exchangeReserveOfGovernmentalCorporations);
                // سود (زیان) انباشته
                readRow(30, null, retainedEarnings);
                // جمع حقوق صاحبان سهام
                readRow(31, null, totalStockHoldersEquity);
                // جمع دارایی‌ها / جمع بدهی‌ها و حقوق صاحبان سهام
                readRow(32, totalAssets, totalLiabilitiesAndStockHoldersEquity);

                return [build(0), build(1)];
            }

            table = ds.getTable(1016);
            if (table && table.getColumnsCount() === 8 && table.getRowsCount() === 35) {
                const readRow = (index, arr1, arr2) => {
                    if (arr1) {
                        arr1[0] = table.getCell(index, 1).getNumberValue();
                        arr1[1] = table.getCell(index, 2).getNumberValue();
                    }
                    if (arr2) {
                        arr2[0] = table.getCell(index, 5).getNumberValue();
                        arr2[1] = table.getCell(index, 6).getNumberValue();
                    }
                };
                // دوره منتهی به
                [rpYear[0], rpMonth[0]] = table.getCell(2, 1).getReportDate();
                [rpYear[1], rpMonth[1]] = table.getCell(2, 2).getReportDate();
                // حسابرسی شده یا نشده
                audited[0] = table.getCell(0, 1).getStringValue().indexOf('حسابرسی شده') !== -1;
                audited[1] = table.getCell(0, 2).getStringValue().indexOf('حسابرسی شده') !== -1;
                // موجودی نقد / پرداختنی‌های تجاری
                readRow(5, cashAndCachEquivalents, tradePayables);
                // سرمایه‌گذاری‌‌های کوتاه مدت / پرداختنی‌های غیرتجاری
                readRow(6, shortTermInvestments, nonTradePayables);
                // دریافتنی‌‌های تجاری / مالیات پرداختنی
                readRow(7, tradeReceivables, currentTaxLiabilities);
                // دریافتنی‌‌های غیرتجاری / سود سهام پرداختنی
                readRow(8, nonTradeReceivables, dividentsPayable);
                // موجودی مواد و کالا / تسهیلات مالی
                readRow(9, inventories, loanPayableCurrent);
                // پیش پرداخت‌ها و سفارشات / ذخایر
                readRow(10, prepayments, provisions);
                // دارایی‌های نگهداری شده برای فروش / پیش‌دریافت‌های جاری
                readRow(11, nonCurrentAssetsForSale, deferredRevenue);
                // جمع دارایی‌های جاری / بدهی‌های مرتبط با دارایی‌های نگهداری شده برای فروش
                readRow(12, totalCurrentAssets, liabilitiesOfDisposalGroupsForSale);
                // جمع بدهی‌های جاری
                readRow(13, null, totalCurrentLiabilities);
                // دریافتنی‌‌های بلندمدت
                readRow(14, longTermNotesAndAccountsReceivable, null);
                // سرمایه‌گذاری‌های بلندمدت / پرداختنی‌های بلندمدت
                readRow(15, longTermInvestments, longTermPayables);
                // سرمایه‌گذاری در املاک / پیش‌دریافت‌های غیرجاری
                readRow(16, investmentProperty, nonCurrentDeferredRevenue);
                // دارایی‌های نامشهود / تسهیلات مالی بلندمدت
                readRow(17, intangibleAssets, loanPayable);
                // دارایی‌های ثابت مشهود / ذخیره مزایای پایان خدمت کارکنان
                readRow(18, plantAssetsNetOfAccumulatedDepreciation, allowanceForPostRetirement);
                // سایر دارایی‌ها / جمع بدهی‌های غیرجاری
                readRow(19, otherNonCurrentAssets, totalNonCurrentLiabilities);
                // جمع دارایی‌های غیرجاری / جمع بدهی‌ها
                readRow(20, totalNonCurrentAssets, totalLiabilities);
                // سرمایه
                readRow(22, null, commonStock);
                // افزایش (کاهش) سرمایه در جریان
                readRow(23, null, inprocessCapitalIncrease);
                // صرف (کسر) سهام
                readRow(24, null, sharePremium);
                // سهام خزانه
                readRow(25, null, treasuryStock);
                // اندوخته قانونی
                readRow(26, null, legalReserve);
                // سایر اندوخته‌ها
                readRow(27, null, expansionReserve);
                // مازاد تجدید ارزیابی دارایی‌های نگهداری شده برای فروش
                readRow(28, null, revaluationSurplusOfNonCurrentAssetsForSale);
                // مازاد تجدید ارزیابی دارایی‌ها
                readRow(29, null, revaluationSurplus);
                // تفاوت تسعیر ناشی از تبدیل به واحد پول گزارشگری
                readRow(30, null, exchangeDifferences);
                // اندوخته تسعیر ارز دارایی‌ها و بدهی‌های شرکت‌های دولتی
                readRow(31, null, exchangeReserveOfGovernmentalCorporations);
                // سود (زیان) انباشته
                readRow(32, null, retainedEarnings);
                // جمع حقوق صاحبان سهام
                readRow(33, null, totalStockHoldersEquity);
                // جمع دارایی‌ها / جمع بدهی‌ها و حقوق صاحبان سهام
                readRow(34, totalAssets, totalLiabilitiesAndStockHoldersEquity);

                return [build(0), build(1)];
            }

            console.error('cannot parse balance sheet.');
            return [];
        }

        // OLD REPORTS
        const doc = new JSDOM(content).window.document;
        // دوره منتهی به
        let v0 = doc.querySelector('#ctl00_cphBody_ucSFinancialPosition_grdSFinancialPosition tr.GridHeader th:nth-child(3)');
        let v1 = doc.querySelector('#ctl00_cphBody_ucSFinancialPosition_grdSFinancialPosition tr.GridHeader th:nth-child(4)');
        v0 = AppUtils.faDigits2En(v0 ? v0.textContent : '');
        v1 = AppUtils.faDigits2En(v1 ? v1.textContent : '');
        [rpYear[0], rpMonth[0]] = AppUtils.findJalaliDate(v0);
        [rpYear[1], rpMonth[1]] = AppUtils.findJalaliDate(v1);
        // حسابرسی شده یا نشده
        audited[0] = v0.indexOf('حسابرسی شده') !== -1;
        audited[1] = v1.indexOf('حسابرسی شده') !== -1;

        let rows = doc.querySelectorAll('#ctl00_cphBody_ucSFinancialPosition_grdSFinancialPosition tr')
        const readRow = (index: number, arr1, arr2) => {
            let idx = FormatUtils.padNumber(index, 2);
            if (arr1) {
                let v0 = doc.getElementById(`ctl00_cphBody_ucSFinancialPosition_grdSFinancialPosition_ctl${idx}_txbAssetYear0`);
                let v1 = doc.getElementById(`ctl00_cphBody_ucSFinancialPosition_grdSFinancialPosition_ctl${idx}_txbAssetYear1`);
                arr1[0] = v0 ? parseInt(v0.value) : undefined;
                arr1[1] = v1 ? parseInt(v1.value) : undefined;
            }
            if (arr2) {
                let v0 = doc.getElementById(`ctl00_cphBody_ucSFinancialPosition_grdSFinancialPosition_ctl${idx}_txbLiabilityYear0`);
                let v1 = doc.getElementById(`ctl00_cphBody_ucSFinancialPosition_grdSFinancialPosition_ctl${idx}_txbLiabilityYear1`);
                arr2[0] = v0 ? parseInt(v0.value) : undefined;
                arr2[1] = v1 ? parseInt(v1.value) : undefined;
            }
        };

        if (rows.length === 33) {
            // موجودی نقد / پرداختنی‌های تجاری
            readRow(4, cashAndCachEquivalents, tradePayables);
            // سرمایه‌گذاری‌‌های کوتاه مدت / پرداختنی‌های غیرتجاری
            readRow(5, shortTermInvestments, nonTradePayables);
            // دریافتنی‌‌های تجاری / مالیات پرداختنی
            readRow(6, tradeReceivables, currentTaxLiabilities);
            // دریافتنی‌‌های غیرتجاری / سود سهام پرداختنی
            readRow(7, nonTradeReceivables, dividentsPayable);
            // موجودی مواد و کالا / تسهیلات مالی
            readRow(8, inventories, loanPayableCurrent);
            // پیش پرداخت‌ها و سفارشات / ذخایر
            readRow(9, prepayments, provisions);
            // دارایی‌های نگهداری شده برای فروش / پیش‌دریافت‌های جاری
            readRow(10, nonCurrentAssetsForSale, deferredRevenue);
            // جمع دارایی‌های جاری / بدهی‌های مرتبط با دارایی‌های نگهداری شده برای فروش
            readRow(11, totalCurrentAssets, liabilitiesOfDisposalGroupsForSale);
            // جمع بدهی‌های جاری
            readRow(12, null, totalCurrentLiabilities);
            // دریافتنی‌‌های بلندمدت
            readRow(13, longTermNotesAndAccountsReceivable, null);
            // سرمایه‌گذاری‌های بلندمدت / پرداختنی‌های بلندمدت
            readRow(14, longTermInvestments, longTermPayables);
            // سرمایه‌گذاری در املاک / پیش‌دریافت‌های غیرجاری
            readRow(15, investmentProperty, nonCurrentDeferredRevenue);
            // دارایی‌های نامشهود / تسهیلات مالی بلندمدت
            readRow(16, intangibleAssets, loanPayable);
            // دارایی‌های ثابت مشهود / ذخیره مزایای پایان خدمت کارکنان
            readRow(17, plantAssetsNetOfAccumulatedDepreciation, allowanceForPostRetirement);
            // سایر دارایی‌ها / جمع بدهی‌های غیرجاری
            readRow(18, otherNonCurrentAssets, totalNonCurrentLiabilities);
            // جمع دارایی‌های غیرجاری / جمع بدهی‌ها
            readRow(19, totalNonCurrentAssets, totalLiabilities);
            // سرمایه
            readRow(21, null, commonStock);
            // افزایش (کاهش) سرمایه در جریان
            readRow(22, null, inprocessCapitalIncrease);
            // صرف (کسر) سهام
            readRow(23, null, sharePremium);
            // سهام خزانه
            readRow(24, null, treasuryStock);
            // اندوخته قانونی
            readRow(25, null, legalReserve);
            // سایر اندوخته‌ها
            readRow(26, null, expansionReserve);
            // مازاد تجدید ارزیابی دارایی‌های نگهداری شده برای فروش
            readRow(27, null, revaluationSurplusOfNonCurrentAssetsForSale);
            // مازاد تجدید ارزیابی دارایی‌ها
            readRow(28, null, revaluationSurplus);
            // تفاوت تسعیر ناشی از تبدیل به واحد پول گزارشگری
            readRow(29, null, exchangeDifferences);
            // اندوخته تسعیر ارز دارایی‌ها و بدهی‌های شرکت‌های دولتی
            readRow(30, null, exchangeReserveOfGovernmentalCorporations);
            // سود (زیان) انباشته
            readRow(31, null, retainedEarnings);
            // جمع حقوق صاحبان سهام
            readRow(32, null, totalStockHoldersEquity);
            // جمع دارایی‌ها / جمع بدهی‌ها و حقوق صاحبان سهام
            readRow(33, totalAssets, totalLiabilitiesAndStockHoldersEquity);

            return [build(0), build(1)];
        }

        if (rows.length === 30) {
            // موجودی نقد
            readRow(3, cashAndCachEquivalents, null);
            // سرمایه گذاریهای کوتاه مدت / حسابها و اسناد پرداختنی تجاری
            readRow(4, shortTermInvestments, tradeAndNonTradePayables);
            // حسابها و اسناد دریافتنی تجاری / بدهی به شرکتهای گروه و وابسته
            readRow(5, tradeReceivables, debtToAffiliatedComponies);
            // طلب از شرکتهای گروه و شرکتهای وابسته / سایر حسابها و اسناد پرداختنی
            readRow(6, receivablesFromAffiliatedCompanies, nonTradePayables);
            // سایر حسابها و اسناد دریافتنی / پیش دریافتها
            readRow(7, nonTradeReceivables, deferredRevenue);
            // موجودی مواد و کالا / ذخیره مالیات
            readRow(8, inventories, currentTaxLiabilities);
            // سفارشات / سود سهام پرداختنی
            readRow(9, advancesToSupliers, dividentsPayable);
            // پیش پرداختها / تسهیلات مالی دریافتی
            readRow(10, prepayments, loanPayableCurrent);
            // دارایی های غیر جاری نگهداری شده برای فروش / بدهی های مرتبط با دارایی های غیر جاری نگهداری شده برای فروش
            readRow(11, nonCurrentAssetsForSale, liabilitiesOfDisposalGroupsForSale);
            // جمع داراییهای جاری / جمع بدهیهای جاری
            readRow(12, totalCurrentAssets, totalCurrentLiabilities);
            // حسابها و اسناد دریافتنی بلند مدت / حسابها و اسناد پرداختنی بلند مدت
            readRow(14, longTermNotesAndAccountsReceivable, longTermPayables);
            // سرمایه گذاریهای بلند مدت / تسهیلات مالی دریافتی بلند مدت
            readRow(15, longTermInvestments, loanPayable);
            // داراییهای ثابت مشهود / ذخیره مزایای پایان خدمت کارکنان
            readRow(16, plantAssetsNetOfAccumulatedDepreciation, allowanceForPostRetirement);
            // داراییهای نامشهود / جمع بدهیهای غیر جاری
            readRow(17, intangibleAssets, totalNonCurrentLiabilities);
            // سایر داراییها / جمع بدهی ها
            readRow(18, otherNonCurrentAssets, totalLiabilities);
            // جمع داراییهای غیر جاری
            readRow(19, totalNonCurrentAssets, null);
            // سرمایه
            readRow(20, null, commonStock);
            // صرف سهام
            readRow(21, null, sharePremium);
            // دریافتی بابت افزایش سرمایه
            readRow(22, null, receivesForCapitalAdvance);
            // اندوخته قانونی
            readRow(23, null, legalReserve);
            // سایر اندوخته ها
            readRow(24, null, expansionReserve);
            // مازاد تجدید ارزیابی دارایی‌های غیرجاری نگهداری شده برای فروش
            readRow(25, null, revaluationSurplusOfNonCurrentAssetsForSale);
            // مازاد تجدید ارزیابی داراییها
            readRow(26, null, revaluationSurplus);
            // اندوخته تسعیر ارز داراییها و بدهیهای شرکتهای دولتی
            readRow(27, null, exchangeReserveOfGovernmentalCorporations);
            // سود (زیان) انباشته
            readRow(28, null, retainedEarnings);
            // جمع حقوق صاحبان سهام
            readRow(29, null, totalStockHoldersEquity);
            // جمع داراییها / جمع بدهیها و حقوق صاحبان سهام
            readRow(30, totalAssets, totalLiabilitiesAndStockHoldersEquity);

            return [build(0), build(1)];
        }

        if (rows.length === 24) {
            // موجودی نقد / حسابها و اسناد پرداختنی تجاری
            readRow(3, cashAndCachEquivalents, tradePayables);
            // سرمایه گذاریهای کوتاه مدت / سایر حسابها و اسناد پرداختنی
            readRow(4, shortTermInvestments, nonTradePayables);
            // طلب از شرکتهای گروه و شرکتهای وابسته / بدهی به شرکتهای گروه و وابسته
            readRow(5, receivablesFromAffiliatedCompanies, debtToAffiliatedComponies);
            // حصه جاری حسابها و اسناد دریافتنی تجاری / پیش دریافتها
            readRow(6, tradeReceivables, deferredRevenue);
            // سایر حسابها و اسناد دریافتنی / ذخیره مالیات بر درآمد
            readRow(7, nonTradeReceivables, currentTaxLiabilities);
            // موجودی مواد و کالا / حصه جاری تسهیلات مالی دریافتی
            readRow(8, inventories, loanPayableCurrent);
            // پیش پرداختها / سود سهام پیشنهادی و پرداختنی
            readRow(9, prepayments, dividentsPayable);
            // جمع داراییهای جاری / جمع بدهیهای جاری
            readRow(10, totalCurrentAssets, totalCurrentLiabilities);
            // سرمایه گذاریهای بلند مدت / حسابها و اسناد پرداختنی بلند مدت
            readRow(11, longTermInvestments, longTermPayables);
            // داراییهای ثابت پس از کسر استهلاک / تسهیلات مالی دریافتی بلند مدت
            readRow(12, plantAssetsNetOfAccumulatedDepreciation, loanPayable);
            // داراییهای نامشهود / ذخیره مزایای پایان خدمت
            readRow(13, intangibleAssets, allowanceForPostRetirement);
            // حسابها و اسناد دریافتنی تجاری(بلند مدت) / جمع بدهیهای غیر جاری
            readRow(14, longTermNotesAndAccountsReceivable, totalNonCurrentLiabilities);
            // پیش پرداختهای سرمایه ای / جمع بدهیهای جاری و غیر جاری
            readRow(15, capitalAdvances, totalLiabilities);
            // سایر داراییها
            readRow(16, otherNonCurrentAssets, null);
            // سرمایه
            readRow(17, null, commonStock);
            // صرف سهام
            readRow(18, null, sharePremium);
            // وجوه دریافتی بابت افزایش سرمایه
            readRow(19, null, receivesForCapitalAdvance);
            // اندوخته قانونی
            readRow(20, null, legalReserve);
            // اندوخته طرح و توسعه
            readRow(21, null, expansionReserve);
            // سود (زیان) انباشته
            readRow(22, null, retainedEarnings);
            // جمع داراییهای غیر جاری / جمع حقوق صاحبان سهام
            readRow(23, totalNonCurrentAssets, totalStockHoldersEquity);
            // جمع داراییها / جمع بدهیها و حقوق صاحبان سهام
            readRow(24, totalAssets, totalLiabilitiesAndStockHoldersEquity);

            return [build(0), build(1)];
        }

        console.error('cannot parse balance sheet.');
        return [];
    }

    async parseIncomeStatements(): Promise<IIncomeStatement[]> {
        const content = await this.getContent(N10SheetId.IncomeStatement);
        const period = [],
            rpYear = [],
            rpMonth = [],
            audited = [],
            dividendIncome = [],
            interestIncome = [],
            salesOfInvestmentInSecuritiesPL = [],
            changesInFairValueOfInvestmentInSecuritiesPL = [],
            revenue = [],
            costOfSales = [],
            grossProfit = [],
            sgaExpenses = [],
            hazineKaheshArzeshDaryaftaniha = [],
            otherRevenues = [],
            otherExpenses = [],
            otherRevenuesAndExpenses = [],
            operatingProfit = [],
            financeCosts = [],
            nonOperatingPL_investments = [],
            nonOperatingPL_others = [],
            nonOperatingPL = [],
            continuingOperationsProfitBeforeTax = [],
            incomeTaxCurrentYear = [],
            incomeTaxPastYears = [],
            incomeTaxExpense = [],
            continuingOperationsProfit = [],
            discontinuedOperationsProfit = [],
            profit = [],
            operatingBasicEPS = [],
            nonOperatingBasicEPS = [],
            continuingOperationsBasicEPS = [],
            discontinuedOperationsBasicEPS = [],
            basicEPS = [],
            eps = [],
            capital = [];

        const build = i => ({
            dividendIncome: dividendIncome[i],
            interestIncome: interestIncome[i],
            salesOfInvestmentInSecuritiesPL: salesOfInvestmentInSecuritiesPL[i],
            hazineKaheshArzeshDaryaftaniha: hazineKaheshArzeshDaryaftaniha[i],
            changesInFairValueOfInvestmentInSecuritiesPL: changesInFairValueOfInvestmentInSecuritiesPL[i],
            audited: audited[i],
            basicEPS: basicEPS[i],
            capital: capital[i],
            continuingOperationsBasicEPS: continuingOperationsBasicEPS[i],
            continuingOperationsProfit: continuingOperationsProfit[i],
            continuingOperationsProfitBeforeTax: continuingOperationsProfitBeforeTax[i],
            costOfSales: costOfSales[i],
            discontinuedOperationsBasicEPS: discontinuedOperationsBasicEPS[i],
            discontinuedOperationsProfit: discontinuedOperationsProfit[i],
            eps: eps[i],
            financeCosts: financeCosts[i],
            grossProfit: grossProfit[i],
            incomeTaxCurrentYear: incomeTaxCurrentYear[i],
            incomeTaxPastYears: incomeTaxPastYears[i],
            incomeTaxExpense: incomeTaxExpense[i],
            nonOperatingBasicEPS: nonOperatingBasicEPS[i],
            nonOperatingPL_investments: nonOperatingPL_investments[i],
            nonOperatingPL_others: nonOperatingPL_others[i],
            nonOperatingPL: nonOperatingPL[i],
            operatingBasicEPS: operatingBasicEPS[i],
            operatingProfit: operatingProfit[i],
            otherExpenses: otherExpenses[i],
            otherRevenues: otherRevenues[i],
            otherRevenuesAndExpenses: otherRevenuesAndExpenses[i],
            profit: profit[i],
            revenue: revenue[i],
            rpMonth: rpMonth[i],
            rpYear: rpYear[i],
            sgaExpenses: sgaExpenses[i],
            period: period[i]
        });

        // دوره
        period[0] = this.letter.period;
        period[1] = this.letter.period;
        period[2] = 12;

        // NEW REPORTS
        const ds = DataSource.parseFromPageContent(content);
        if (ds) {
            let table = ds.getTable(1084) || ds.getTable(1285) || ds.getTable(1152);
            if (table && table.getColumnsCount() === 6 && table.getRowsCount() === 31) {
                const readRow = (arr, index) => {
                    arr[0] = table.getCell(index, 1).getNumberValue();
                    arr[1] = table.getCell(index, 2).getNumberValue();
                    arr[2] = table.getCell(index, 3).getNumberValue();
                };
                // دوره منتهی به
                [rpYear[0], rpMonth[0]] = table.getCell(0, 1).getReportDate();
                [rpYear[1], rpMonth[1]] = table.getCell(0, 2).getReportDate();
                [rpYear[2], rpMonth[2]] = table.getCell(0, 3).getReportDate();
                // حسابرسی شده یا نشده
                audited[0] = table.getCell(0, 1).getStringValue().indexOf('حسابرسی شده') !== -1;
                audited[1] = table.getCell(1, 2).getStringValue().indexOf('حسابرسی شده') !== -1;
                audited[2] = table.getCell(1, 3).getStringValue().indexOf('حسابرسی شده') !== -1;
                // درآمدهای عملیاتی
                readRow(revenue, 3);
                // بهای تمام شده درآمدهای عملیاتی
                readRow(costOfSales, 4);
                // سود (زیان) ناخالص
                readRow(grossProfit, 5);
                // هزینه‌های فروش، اداری و عمومی
                readRow(sgaExpenses, 6);
                // هزینه کاهش ارزش دریافتنی‌ها (هزینه استثنایی)
                readRow(hazineKaheshArzeshDaryaftaniha, 7);
                // سایر درآمدها
                readRow(otherRevenues, 8);
                // سایر هزینه‌ها
                readRow(otherExpenses, 9);
                // سود (زیان) عملیاتی
                readRow(operatingProfit, 10);
                // هزینه‌های مالی
                readRow(financeCosts, 11);
                // سایر درآمدها و هزینه‌های غیرعملیاتی - درآمد سرمایه‌گذاری‌ها
                readRow(nonOperatingPL_investments, 12);
                // سایر درآمدها و هزینه‌های غیرعملیاتی - اقلام متفرقه
                readRow(nonOperatingPL_others, 13);
                // سود (زیان) عملیات در حال تداوم قبل از مالیات
                readRow(continuingOperationsProfitBeforeTax, 14);
                // هزینه مالیات بر درآمد سال جاری
                readRow(incomeTaxCurrentYear, 16);
                // هزینه مالیات بر درآمد سال‌های قبل
                readRow(incomeTaxPastYears, 17);
                // سود (زیان) خالص عملیات در حال تداوم
                readRow(continuingOperationsProfit, 18);
                // سود (زیان) خالص عملیات متوقف شده
                readRow(discontinuedOperationsProfit, 20);
                // سود (زیان) خالص
                readRow(profit, 21);
                // سود عملیاتی پایه هر سهم
                readRow(operatingBasicEPS, 23);
                // سود غیر عملیاتی پابه هر سهم
                readRow(nonOperatingBasicEPS, 24);
                // سود پایه هر سهم ناشی از عملیات در حال تداوم
                readRow(continuingOperationsBasicEPS, 25);
                // سود پایه هر سهم ناشی از عملیات متوقف شده
                readRow(discontinuedOperationsBasicEPS, 26);
                // سود (زیان) پایه هر سهم
                readRow(basicEPS, 27);
                // سود (زیان) خالص هر سهم
                readRow(eps, 28);
                // سرمایه
                readRow(capital, 29);

                if (this.letter.period === 12) {
                    return [build(0), build(2)];
                } else {
                    return [build(0), build(1), build(2)];
                }
            }

            table = ds.getTable(904) || ds.getTable(923) || ds.getTable(1020);
            if (table && table.getColumnsCount() === 6 && table.getRowsCount() === 45) {
                const readRow = (arr, index) => {
                    arr[0] = table.getCell(index, 1).getNumberValue();
                    arr[1] = table.getCell(index, 2).getNumberValue();
                    arr[2] = table.getCell(index, 4).getNumberValue();
                };
                // دوره منتهی به
                [rpYear[0], rpMonth[0]] = table.getCell(0, 1).getReportDate();
                [rpYear[1], rpMonth[1]] = table.getCell(0, 2).getReportDate();
                [rpYear[2], rpMonth[2]] = table.getCell(0, 4).getReportDate();
                // حسابرسی شده یا نشده
                audited[0] = table.getCell(2, 1).getStringValue().indexOf('حسابرسی شده') !== -1;
                audited[1] = table.getCell(2, 2).getStringValue().indexOf('حسابرسی شده') !== -1;
                audited[2] = table.getCell(2, 4).getStringValue().indexOf('حسابرسی شده') !== -1;
                // درآمدهای عملیاتی
                readRow(revenue, 4);
                // بهای تمام شده درآمدهای عملیاتی
                readRow(costOfSales, 5);
                // سود (زیان) ناخالص
                readRow(grossProfit, 6);
                // هزینه‌های فروش، اداری و عمومی
                readRow(sgaExpenses, 7);
                // سایر درآمدها
                readRow(otherRevenues, 8);
                // سایر هزینه‌ها
                readRow(otherExpenses, 9);
                // سود (زیان) عملیاتی
                readRow(operatingProfit, 10);
                // هزینه‌های مالی
                readRow(financeCosts, 11);
                // سایر درآمدها و هزینه‌های غیرعملیاتی - درآمد سرمایه‌گذاری‌ها
                readRow(nonOperatingPL_investments, 12);
                // سایر درآمدها و هزینه‌های غیرعملیاتی - اقلام متفرقه
                readRow(nonOperatingPL_others, 13);
                // سود (زیان) عملیات در حال تداوم قبل از مالیات
                readRow(continuingOperationsProfitBeforeTax, 14);
                // مالیات بر درآمد
                readRow(incomeTaxExpense, 15);
                // سود (زیان) خالص عملیات در حال تداوم
                readRow(continuingOperationsProfit, 16);
                // سود (زیان) خالص عملیات متوقف شده
                readRow(discontinuedOperationsProfit, 17);
                // سود (زیان) خالص
                readRow(profit, 18);
                // سود عملیاتی پایه هر سهم
                readRow(operatingBasicEPS, 20);
                // سود غیر عملیاتی پابه هر سهم
                readRow(nonOperatingBasicEPS, 21);
                // سود پایه هر سهم ناشی از عملیات متوقف شده
                readRow(discontinuedOperationsBasicEPS, 22);
                // سود (زیان) پایه هر سهم
                readRow(basicEPS, 23);
                // سود (زیان) خالص هر سهم
                readRow(eps, 42);
                // سرمایه
                readRow(capital, 43);

                if (this.letter.period === 12) {
                    return [build(0), build(2)];
                } else {
                    return [build(0), build(1), build(2)];
                }
            }

            console.error('cannot parse income statement.');
            return [];
        }

        // OLD REPORTS
        const doc = new JSDOM(content).window.document;
        // دوره منتهی به
        let v0 = doc.getElementById('ctl00_cphBody_ucInterimStatement_lblYear0');
        let v1 = doc.getElementById('ctl00_cphBody_ucInterimStatement_lblYear1');
        let v2 = doc.getElementById('ctl00_cphBody_ucInterimStatement_lblYear2');
        v0 = AppUtils.faDigits2En(v0 ? v0.textContent : '');
        v1 = AppUtils.faDigits2En(v1 ? v1.textContent : '');
        v2 = AppUtils.faDigits2En(v2 ? v2.textContent : '');
        [rpYear[0], rpMonth[0]] = AppUtils.findJalaliDate(v0);
        [rpYear[1], rpMonth[1]] = AppUtils.findJalaliDate(v1);
        [rpYear[2], rpMonth[2]] = AppUtils.findJalaliDate(v2);
        // حسابرسی شده یا نشده
        audited[0] = v0.indexOf('حسابرسی شده') !== -1;
        audited[1] = v1.indexOf('حسابرسی شده') !== -1;
        audited[2] = v2.indexOf('حسابرسی شده') !== -1;

        let rows = doc.querySelectorAll('#ctl00_cphBody_ucInterimStatement_grdInterimStatement tr')
        const readRow = (arr, index) => {
            let v0 = doc.getElementById(`ctl00_cphBody_ucInterimStatement_grdInterimStatement_ctl${index}_txbYear0`);
            let v1 = doc.getElementById(`ctl00_cphBody_ucInterimStatement_grdInterimStatement_ctl${index}_txbYear1`);
            let v2 = doc.getElementById(`ctl00_cphBody_ucInterimStatement_grdInterimStatement_ctl${index}_txbYear2`);
            arr[0] = v0 ? parseInt(v0.value) : undefined;
            arr[1] = v1 ? parseInt(v1.value) : undefined;
            arr[2] = v2 ? parseInt(v2.value) : undefined;
        };
        if (rows.length === 58) {
            // درآمدهای عملیاتی
            readRow(revenue, 20);
            // بهای تمام شده درآمدهای عملیاتی
            readRow(costOfSales, 21);
            // سود (زیان) ناخالص
            readRow(grossProfit, 22);
            // هزینه‌های فروش، اداری و عمومی
            readRow(sgaExpenses, 23);
            // سایر درآمدهای عملیاتی
            readRow(otherRevenues, 24);
            // سایر هزینه‌های عملیاتی
            readRow(otherExpenses, 25);
            // سود (زیان) عملیاتی
            readRow(operatingProfit, 26);
            // هزینه‌های مالی
            readRow(financeCosts, 27);
            // سایر درآمدها و هزینه‌های غیرعملیاتی - درآمد سرمایه‌گذاری‌ها
            readRow(nonOperatingPL_investments, 28);
            // سایر درآمدها و هزینه‌های غیرعملیاتی - اقلام متفرقه
            readRow(nonOperatingPL_others, 29);
            // سود (زیان) عملیات در حال تداوم قبل از مالیات
            readRow(continuingOperationsProfitBeforeTax, 30);
            // هزینه مالیات بر درآمد
            readRow(incomeTaxExpense, 31);
            // سود (زیان) خالص عملیات در حال تداوم
            readRow(continuingOperationsProfit, 32);
            // سود (زیان) عملیات متوقف ‌شده پس از اثر مالیاتی
            readRow(discontinuedOperationsProfit, 33);
            // سود (زیان) خالص
            readRow(profit, 34);
            // سود (زیان) پایه هر سهم ناشی از عملیات در حال تداوم- عملیاتی
            readRow(operatingBasicEPS, 36);
            // سود (زیان) پایه هر سهم ناشی از عملیات در حال تداوم- غیرعملیاتی
            readRow(nonOperatingBasicEPS, 37);
            // سود (زیان) پایه هر سهم ناشی از عملیات متوقف‌ شده
            readRow(discontinuedOperationsBasicEPS, 38);
            // سود (زیان) پایه هر سهم
            readRow(basicEPS, 39);
            // سود (زیان) خالص هر سهم
            readRow(eps, 58);
            // سرمایه
            readRow(capital, 59);

            if (this.letter.period === 12) {
                return [build(0), build(1)];
            } else {
                return [build(0), build(1), build(2)];
            }
        }

        if (rows.length === 57) {
            // فروش خالص
            readRow(revenue, 20);
            // بهای تمام شده فروش
            readRow(costOfSales, 21);
            // سود (زیان) ناخالص
            readRow(grossProfit, 22);
            // هزینه های اداری, عمومی و فروش
            readRow(sgaExpenses, 23);
            // سایر درآمدها (هزینه های) عملیاتی
            readRow(otherRevenuesAndExpenses, 24);
            // سود (زیان) عملیاتی
            readRow(operatingProfit, 26);
            // هزینه‌های مالی
            readRow(financeCosts, 27);
            // خالص سایر درآمدها و هزینه های غیرعملیاتی
            readRow(nonOperatingPL, 28);
            // سود (زیان) عملیات در حال تداوم قبل از مالیات
            readRow(continuingOperationsProfitBeforeTax, 29);
            // هزینه مالیات بر درآمد
            readRow(incomeTaxExpense, 30);
            // سود (زیان) خالص عملیات در حال تداوم
            readRow(continuingOperationsProfit, 31);
            // سود (زیان) عملیات متوقف ‌شده پس از اثر مالیاتی
            readRow(discontinuedOperationsProfit, 32);
            // سود (زیان) خالص
            readRow(profit, 33);
            // سود (زیان) پایه هر سهم ناشی از عملیات در حال تداوم- عملیاتی
            readRow(operatingBasicEPS, 35);
            // سود (زیان) پایه هر سهم ناشی از عملیات در حال تداوم- غیرعملیاتی
            readRow(nonOperatingBasicEPS, 36);
            // سود (زیان) پایه هر سهم ناشی از عملیات متوقف‌ شده
            readRow(discontinuedOperationsBasicEPS, 37);
            // سود (زیان) پایه هر سهم
            readRow(basicEPS, 38);
            // سود (زیان) خالص هر سهم
            readRow(eps, 57);
            // سرمایه
            readRow(capital, 58);

            if (this.letter.period === 12) {
                return [build(0), build(1)];
            } else {
                return [build(0), build(1), build(2)];
            }
        }

        if (rows.length === 44) {
            // فروش
            readRow(revenue, 19);
            // بهای تمام شده کالای فروش رفته
            readRow(costOfSales, 20);
            // سود (زیان) ناخالص
            readRow(grossProfit, 21);
            // هزینه های عمومی, اداری و تشکیلاتی
            readRow(sgaExpenses, 22);
            // خالص سایر درآمدها (هزینه ها) ی عملیاتی
            readRow(otherRevenuesAndExpenses, 23);
            // سود (زیان) عملیاتی
            readRow(operatingProfit, 24);
            // هزینه‌های مالی
            readRow(financeCosts, 25);
            // درآمد حاصل از سرمایه گذاریها
            readRow(nonOperatingPL_investments, 26);
            // خالص درآمد (هزینه) های متفرقه
            readRow(nonOperatingPL_others, 27);
            // سود (زیان) قبل از کسر مالیات
            readRow(continuingOperationsProfitBeforeTax, 30);
            // مالیات
            readRow(incomeTaxExpense, 31);
            // سود (زیان) خالص پس از کسر مالیات
            readRow(profit, 32);
            // سود هر سهم پس از کسر مالیات
            readRow(eps, 44);
            // سرمایه
            readRow(capital, 45);

            if (this.letter.period === 12) {
                return [build(0), build(1)];
            } else {
                return [build(0), build(1), build(2)];
            }
        }

        if (rows.length === 43) {
            // فروش
            readRow(revenue, 19);
            // بهای تمام شده کالای فروش رفته
            readRow(costOfSales, 20);
            // سود (زیان) ناخالص
            readRow(grossProfit, 21);
            // هزینه های عمومی, اداری و تشکیلاتی
            readRow(sgaExpenses, 22);
            // خالص سایر درآمدها (هزینه ها) ی عملیاتی
            readRow(otherRevenuesAndExpenses, 23);
            // سود (زیان) عملیاتی
            readRow(operatingProfit, 24);
            // هزینه‌های مالی
            readRow(financeCosts, 25);
            // درآمد حاصل از سرمایه گذاریها
            readRow(nonOperatingPL_investments, 26);
            // خالص درآمد (هزینه) های متفرقه
            readRow(nonOperatingPL_others, 27);
            // سود (زیان) قبل از کسر مالیات
            readRow(continuingOperationsProfitBeforeTax, 30);
            // مالیات
            readRow(incomeTaxExpense, 31);
            // سود (زیان) خالص پس از کسر مالیات
            readRow(profit, 32);
            // سود هر سهم پس از کسر مالیات
            readRow(eps, 43);
            // سرمایه
            readRow(capital, 44);

            if (this.letter.period === 12) {
                return [build(0), build(1)];
            } else {
                return [build(0), build(1), build(2)];
            }
        }

        if (rows.length === 62) {
            // درآمد سود سهام
            readRow(dividendIncome, 21);
            // درآمد سود تضمین شده
            readRow(interestIncome, 22);
            // سود (زیان) فروش سرمایه گذاری ها
            readRow(salesOfInvestmentInSecuritiesPL, 23);
            // سود (زیان) تغییر ارزش سرمایه گذاری در اوراق بهادار
            readRow(salesOfInvestmentInSecuritiesPL, 24);
            // سایر درآمدهای عملیاتی
            readRow(otherRevenues, 25);
            // هزینه‌های فروش، اداری و عمومی
            readRow(sgaExpenses, 28);
            // سایر هزینه‌های عملیاتی
            readRow(otherExpenses, 29);
            // سود (زیان) عملیاتی
            readRow(operatingProfit, 31);
            // هزینه‌های مالی
            readRow(financeCosts, 32);
            // سایر درآمدها و هزینه‌های غیرعملیاتی
            readRow(nonOperatingPL, 33);
            // سود (زیان) عملیات در حال تداوم قبل از مالیات
            readRow(continuingOperationsProfitBeforeTax, 34);
            // مالیات بر درآمد
            readRow(incomeTaxExpense, 35);
            // سود (زیان) خالص عملیات در حال تداوم
            readRow(profit, 36);
            // سود (زیان) عملیات متوقف ‌شده پس از اثر مالیاتی
            readRow(discontinuedOperationsProfit, 37);
            // سود (زیان) خالص
            readRow(profit, 38);
            // سود (زیان) پایه هر سهم ناشی از عملیات در حال تداوم- عملیاتی
            readRow(operatingBasicEPS, 40);
            // سود (زیان) پایه هر سهم ناشی از عملیات در حال تداوم- غیرعملیاتی
            readRow(nonOperatingBasicEPS, 41);
            // سود (زیان) پایه هر سهم ناشی از عملیات متوقف‌ شده
            readRow(discontinuedOperationsBasicEPS, 42);
            // سود (زیان) پایه هر سهم
            readRow(basicEPS, 43);
            // سود (زیان) خالص هر سهم- ریال
            readRow(eps, 62);
            // سرمایه
            readRow(capital, 63);

            if (this.letter.period === 12) {
                return [build(0), build(1)];
            } else {
                return [build(0), build(1), build(2)];
            }
        }

        console.error('cannot parse income statement.');
        return [];
    }

    async parseCostOfGoods(): Promise<ICostOfGoods[]> {
        const content = await this.getContent(N10SheetId.InterpretativeReportPage1);
        const table = DataSource.parseFromPageContent(content).getTable(223);

        const costs1 = <ICostOfGoods>{}, costs2 = <ICostOfGoods>{};
        let cell: DataSourceCell;

        // period & year end
        let period;

        cell = table.getCell(0, 1);
        period = cell.getPeriodEndTo();
        costs1.rpYear = period.rpYear;
        costs1.rpMonth = period.rpMonth;
        //costs1.smYear = period.smYear;
        //costs1.smMonth = period.smMonth;

        cell = table.getCell(0, 2);
        period = cell.getPeriodEndTo();
        costs2.rpYear = period.rpYear;
        costs2.rpMonth = period.rpMonth;
        //costs2.smYear = period.smYear;
        //costs2.smMonth = period.smMonth;

        // Mavad Mostaghim Masrafi
        costs1.mavadMostaghimMasrafi = table.getCell(1, 1).getNumberValue();
        costs2.mavadMostaghimMasrafi = table.getCell(1, 2).getNumberValue();

        // Dastmozd Mostaghim Tolid
        costs1.dastmozdMostaghimTolid = table.getCell(2, 1).getNumberValue();
        costs2.dastmozdMostaghimTolid = table.getCell(2, 2).getNumberValue();

        // Sarbar Tolid
        costs1.sarbarTolid = table.getCell(3, 1).getNumberValue();
        costs2.sarbarTolid = table.getCell(3, 2).getNumberValue();

        // Hazineh Jazb Nashodeh Dar Tolid
        costs1.hazinehJazbNashodehDarTolid = table.getCell(5, 1).getNumberValue();
        costs2.hazinehJazbNashodehDarTolid = table.getCell(5, 2).getNumberValue();

        // Mojodi Kala Dar Jaryan Sakht Aval Doreh
        costs1.mojodiKalaDarJaryanSakhtAvalDoreh = table.getCell(7, 1).getNumberValue();
        costs2.mojodiKalaDarJaryanSakhtAvalDoreh = table.getCell(7, 2).getNumberValue();

        // Mojodi Kala Dar Jaryan Sakht Payan Doreh
        costs1.mojodiKalaDarJaryanSakhtPayanDoreh = table.getCell(8, 1).getNumberValue();
        costs2.mojodiKalaDarJaryanSakhtPayanDoreh = table.getCell(8, 2).getNumberValue();

        // Zayeat Ghire Adi
        costs1.zayeatGhireAdi = table.getCell(9, 1).getNumberValue();
        costs2.zayeatGhireAdi = table.getCell(9, 2).getNumberValue();

        // Mojodi Kala Sakhteh Shode Aval Doreh
        costs1.mojodiKalaSakhtehShodeAvalDoreh = table.getCell(11, 1).getNumberValue();
        costs2.mojodiKalaSakhtehShodeAvalDoreh = table.getCell(11, 2).getNumberValue();

        // Mojodi Kala Sakhteh Shode Payan Doreh
        costs1.mojodiKalaSakhtehShodePayanDoreh = table.getCell(12, 1).getNumberValue();
        costs2.mojodiKalaSakhtehShodePayanDoreh = table.getCell(12, 2).getNumberValue();

        // Khadamat
        costs1.khadamat = table.getCell(14, 1).getNumberValue();
        costs2.khadamat = table.getCell(14, 2).getNumberValue();

        return [costs1, costs2];
    }

    async parseSalesAndCostsTrend(): Promise<ISaleAndCost[]> {
        const content = await this.getContent(N10SheetId.InterpretativeReportPage1);
        const dataSource = DataSource.parseFromPageContent(content);
        const table = dataSource.getTable(221);
        const baseRow = table.getBaseRow();
        return table.getRawCells()
            .filter(c => c.rowSequence === baseRow
                && c.columnSequence > 1
                && c.isVisible
                && c.periodEndToDate
                && c.periodEndToDate.length > 0)
            .map(c => {
                const period = new DataSourceCell(dataSource.getRawDataSource(), c).getPeriodEndTo();
                const sale = table.getCell(1, c.columnSequence - 1).getNumberValue();
                const cost = -table.getCell(2, c.columnSequence - 1).getNumberValue();
                return <ISaleAndCost>{...period, sale, cost};
            });
    }

    async parseFutureManagementGoalsAndStrategies(): Promise<string> {
        const content = await this.getContent(N10SheetId.InterpretativeReportPage1);
        const dataSource = DataSource.parseFromPageContent(content);
        return dataSource.getTable(222).getCell(0, 0).getStringValue();
    }

    private async getContent(sheetId: N10SheetId): Promise<string> {
        if (this.contentMap.has(sheetId)) {
            return this.contentMap.get(sheetId);
        } else {
            const content = await fetchReportPageContent(this.letter.url, sheetId);
            this.contentMap.set(sheetId, content);
            return content;
        }
    }
}