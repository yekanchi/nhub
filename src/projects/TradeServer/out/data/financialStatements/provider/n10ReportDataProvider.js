"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CodalN10ReportDataProvider = exports.N10SheetId = void 0;
const jsdom_1 = require("jsdom");
const AppUtils_1 = require("utils/AppUtils");
const FormatUtils_1 = require("utils/FormatUtils");
const dataSource_1 = require("./dataSource");
const utils_1 = require("./utils");
var N10SheetId;
(function (N10SheetId) {
    N10SheetId[N10SheetId["BalanceSheet"] = 0] = "BalanceSheet";
    N10SheetId[N10SheetId["IncomeStatement"] = 1] = "IncomeStatement";
    N10SheetId[N10SheetId["InterpretativeReportPage1"] = 20] = "InterpretativeReportPage1";
})(N10SheetId = exports.N10SheetId || (exports.N10SheetId = {}));
class CodalN10ReportDataProvider {
    constructor(letter, defaultSheetId) {
        this.letter = letter;
        this.defaultSheetId = defaultSheetId;
        this.contentMap = new Map();
    }
    async parseReport() {
        const content = await this.getContent(this.defaultSheetId);
        const document = new jsdom_1.JSDOM(content).window.document;
        let elem, text;
        // اصلاحیه
        let amend = false, amendedTracingNumber = 0, amendmentDesc = '';
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
            text = AppUtils_1.AppUtils.faDigits2En(elem.textContent.trim());
            capital = parseInt(AppUtils_1.AppUtils.removeNonDigits(text));
        }
        // دوره
        let period = 0, rpMonth = 0, rpYear = 0;
        elem = document.getElementById('ctl00_lblPeriod');
        if (elem) {
            text = AppUtils_1.AppUtils.faDigits2En(elem.textContent.trim());
            period = parseInt(AppUtils_1.AppUtils.removeNonDigits(text));
        }
        elem = document.getElementById('ctl00_lblPeriodEndToDate');
        if (elem) {
            text = AppUtils_1.AppUtils.faDigits2En(elem.textContent.trim());
            const [year, month] = AppUtils_1.AppUtils.parseJalaliDate(text);
            rpMonth = month;
            rpYear = year;
        }
        // سال مالی
        let smMonth = 0, smYear = 1;
        elem = document.getElementById('ctl00_lblYearEndToDate');
        if (elem) {
            text = AppUtils_1.AppUtils.faDigits2En(elem.textContent.trim());
            const [year, month] = AppUtils_1.AppUtils.parseJalaliDate(text);
            smMonth = month;
            smYear = year;
        }
        // تلفیقی
        let consolidated = false;
        elem = document.getElementById('ctl00_lblReportName');
        if (elem) {
            text = AppUtils_1.AppUtils.ar2fa(elem.textContent);
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
        };
    }
    async parseBalanceSheets() {
        const content = await this.getContent(N10SheetId.BalanceSheet);
        const period = [], rpYear = [], rpMonth = [], audited = [], cashAndCachEquivalents = [], shortTermInvestments = [], tradeAndNonTradeReceivables = [], tradeReceivables = [], nonTradeReceivables = [], receivablesFromAffiliatedCompanies = [], inventories = [], prepayments = [], advancesToSupliers = [], nonCurrentAssetsForSale = [], totalCurrentAssets = [], longTermNotesAndAccountsReceivable = [], longTermInvestments = [], investmentProperty = [], intangibleAssets = [], plantAssetsNetOfAccumulatedDepreciation = [], capitalAdvances = [], otherNonCurrentAssets = [], totalNonCurrentAssets = [], totalAssets = [], tradeAndNonTradePayables = [], tradePayables = [], nonTradePayables = [], debtToAffiliatedComponies = [], deferredRevenue = [], currentTaxLiabilities = [], dividentsPayable = [], loanPayableCurrent = [], provisions = [], liabilitiesOfDisposalGroupsForSale = [], totalCurrentLiabilities = [], longTermPayables = [], nonCurrentDeferredRevenue = [], loanPayable = [], allowanceForPostRetirement = [], totalNonCurrentLiabilities = [], totalLiabilities = [], commonStock = [], inprocessCapitalIncrease = [], sharePremium = [], treasuryStock = [], treasuryStockConsume = [], receivesForCapitalAdvance = [], legalReserve = [], expansionReserve = [], retainedEarnings = [], revaluationSurplus = [], revaluationSurplusOfNonCurrentAssetsForSale = [], exchangeDifferences = [], exchangeReserveOfGovernmentalCorporations = [], totalStockHoldersEquity = [], totalLiabilitiesAndStockHoldersEquity = [];
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
        const ds = dataSource_1.DataSource.parseFromPageContent(content);
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
        const doc = new jsdom_1.JSDOM(content).window.document;
        // دوره منتهی به
        let v0 = doc.querySelector('#ctl00_cphBody_ucSFinancialPosition_grdSFinancialPosition tr.GridHeader th:nth-child(3)');
        let v1 = doc.querySelector('#ctl00_cphBody_ucSFinancialPosition_grdSFinancialPosition tr.GridHeader th:nth-child(4)');
        v0 = AppUtils_1.AppUtils.faDigits2En(v0 ? v0.textContent : '');
        v1 = AppUtils_1.AppUtils.faDigits2En(v1 ? v1.textContent : '');
        [rpYear[0], rpMonth[0]] = AppUtils_1.AppUtils.findJalaliDate(v0);
        [rpYear[1], rpMonth[1]] = AppUtils_1.AppUtils.findJalaliDate(v1);
        // حسابرسی شده یا نشده
        audited[0] = v0.indexOf('حسابرسی شده') !== -1;
        audited[1] = v1.indexOf('حسابرسی شده') !== -1;
        let rows = doc.querySelectorAll('#ctl00_cphBody_ucSFinancialPosition_grdSFinancialPosition tr');
        const readRow = (index, arr1, arr2) => {
            let idx = FormatUtils_1.FormatUtils.padNumber(index, 2);
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
    async parseIncomeStatements() {
        const content = await this.getContent(N10SheetId.IncomeStatement);
        const period = [], rpYear = [], rpMonth = [], audited = [], dividendIncome = [], interestIncome = [], salesOfInvestmentInSecuritiesPL = [], changesInFairValueOfInvestmentInSecuritiesPL = [], revenue = [], costOfSales = [], grossProfit = [], sgaExpenses = [], hazineKaheshArzeshDaryaftaniha = [], otherRevenues = [], otherExpenses = [], otherRevenuesAndExpenses = [], operatingProfit = [], financeCosts = [], nonOperatingPL_investments = [], nonOperatingPL_others = [], nonOperatingPL = [], continuingOperationsProfitBeforeTax = [], incomeTaxCurrentYear = [], incomeTaxPastYears = [], incomeTaxExpense = [], continuingOperationsProfit = [], discontinuedOperationsProfit = [], profit = [], operatingBasicEPS = [], nonOperatingBasicEPS = [], continuingOperationsBasicEPS = [], discontinuedOperationsBasicEPS = [], basicEPS = [], eps = [], capital = [];
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
        const ds = dataSource_1.DataSource.parseFromPageContent(content);
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
                }
                else {
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
                }
                else {
                    return [build(0), build(1), build(2)];
                }
            }
            console.error('cannot parse income statement.');
            return [];
        }
        // OLD REPORTS
        const doc = new jsdom_1.JSDOM(content).window.document;
        // دوره منتهی به
        let v0 = doc.getElementById('ctl00_cphBody_ucInterimStatement_lblYear0');
        let v1 = doc.getElementById('ctl00_cphBody_ucInterimStatement_lblYear1');
        let v2 = doc.getElementById('ctl00_cphBody_ucInterimStatement_lblYear2');
        v0 = AppUtils_1.AppUtils.faDigits2En(v0 ? v0.textContent : '');
        v1 = AppUtils_1.AppUtils.faDigits2En(v1 ? v1.textContent : '');
        v2 = AppUtils_1.AppUtils.faDigits2En(v2 ? v2.textContent : '');
        [rpYear[0], rpMonth[0]] = AppUtils_1.AppUtils.findJalaliDate(v0);
        [rpYear[1], rpMonth[1]] = AppUtils_1.AppUtils.findJalaliDate(v1);
        [rpYear[2], rpMonth[2]] = AppUtils_1.AppUtils.findJalaliDate(v2);
        // حسابرسی شده یا نشده
        audited[0] = v0.indexOf('حسابرسی شده') !== -1;
        audited[1] = v1.indexOf('حسابرسی شده') !== -1;
        audited[2] = v2.indexOf('حسابرسی شده') !== -1;
        let rows = doc.querySelectorAll('#ctl00_cphBody_ucInterimStatement_grdInterimStatement tr');
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
            }
            else {
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
            }
            else {
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
            }
            else {
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
            }
            else {
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
            }
            else {
                return [build(0), build(1), build(2)];
            }
        }
        console.error('cannot parse income statement.');
        return [];
    }
    async parseCostOfGoods() {
        const content = await this.getContent(N10SheetId.InterpretativeReportPage1);
        const table = dataSource_1.DataSource.parseFromPageContent(content).getTable(223);
        const costs1 = {}, costs2 = {};
        let cell;
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
    async parseSalesAndCostsTrend() {
        const content = await this.getContent(N10SheetId.InterpretativeReportPage1);
        const dataSource = dataSource_1.DataSource.parseFromPageContent(content);
        const table = dataSource.getTable(221);
        const baseRow = table.getBaseRow();
        return table.getRawCells()
            .filter(c => c.rowSequence === baseRow
            && c.columnSequence > 1
            && c.isVisible
            && c.periodEndToDate
            && c.periodEndToDate.length > 0)
            .map(c => {
            const period = new dataSource_1.DataSourceCell(dataSource.getRawDataSource(), c).getPeriodEndTo();
            const sale = table.getCell(1, c.columnSequence - 1).getNumberValue();
            const cost = -table.getCell(2, c.columnSequence - 1).getNumberValue();
            return { ...period, sale, cost };
        });
    }
    async parseFutureManagementGoalsAndStrategies() {
        const content = await this.getContent(N10SheetId.InterpretativeReportPage1);
        const dataSource = dataSource_1.DataSource.parseFromPageContent(content);
        return dataSource.getTable(222).getCell(0, 0).getStringValue();
    }
    async getContent(sheetId) {
        if (this.contentMap.has(sheetId)) {
            return this.contentMap.get(sheetId);
        }
        else {
            const content = await utils_1.fetchReportPageContent(this.letter.url, sheetId);
            this.contentMap.set(sheetId, content);
            return content;
        }
    }
}
exports.CodalN10ReportDataProvider = CodalN10ReportDataProvider;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibjEwUmVwb3J0RGF0YVByb3ZpZGVyLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vLi4vLi4vc3JjL2RhdGEvZmluYW5jaWFsU3RhdGVtZW50cy9wcm92aWRlci9uMTBSZXBvcnREYXRhUHJvdmlkZXIudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7O0FBUUEsaUNBQTRCO0FBQzVCLDZDQUF3QztBQUN4QyxtREFBOEM7QUFDOUMsNkNBQXdEO0FBQ3hELG1DQUErQztBQUcvQyxJQUFZLFVBSVg7QUFKRCxXQUFZLFVBQVU7SUFDbEIsMkRBQWdCLENBQUE7SUFDaEIsaUVBQW1CLENBQUE7SUFDbkIsc0ZBQThCLENBQUE7QUFDbEMsQ0FBQyxFQUpXLFVBQVUsR0FBVixrQkFBVSxLQUFWLGtCQUFVLFFBSXJCO0FBRUQsTUFBYSwwQkFBMEI7SUFLbkMsWUFBWSxNQUFrQixFQUFFLGNBQTBCO1FBQ3RELElBQUksQ0FBQyxNQUFNLEdBQUcsTUFBTSxDQUFDO1FBQ3JCLElBQUksQ0FBQyxjQUFjLEdBQUcsY0FBYyxDQUFDO1FBQ3JDLElBQUksQ0FBQyxVQUFVLEdBQUcsSUFBSSxHQUFHLEVBQXNCLENBQUM7SUFDcEQsQ0FBQztJQUVELEtBQUssQ0FBQyxXQUFXO1FBQ2IsTUFBTSxPQUFPLEdBQUcsTUFBTSxJQUFJLENBQUMsVUFBVSxDQUFDLElBQUksQ0FBQyxjQUFjLENBQUMsQ0FBQztRQUMzRCxNQUFNLFFBQVEsR0FBRyxJQUFJLGFBQUssQ0FBQyxPQUFPLENBQUMsQ0FBQyxNQUFNLENBQUMsUUFBUSxDQUFDO1FBQ3BELElBQUksSUFBSSxFQUFFLElBQUksQ0FBQztRQUVmLFVBQVU7UUFDVixJQUFJLEtBQUssR0FBRyxLQUFLLEVBQ2Isb0JBQW9CLEdBQUcsQ0FBQyxFQUN4QixhQUFhLEdBQUcsRUFBRSxDQUFDO1FBQ3ZCLElBQUksR0FBRyxRQUFRLENBQUMsY0FBYyxDQUFDLHdEQUF3RCxDQUFDLENBQUM7UUFDekYsSUFBSSxJQUFJLEVBQUU7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