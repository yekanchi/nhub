"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CodalN10ReportDataProvider = exports.N10SheetId = void 0;
const jsdom_1 = require("jsdom");
const utils_1 = require("./utils");
// import {FormatUtils} from "./utils";
const dataSource_1 = require("./dataSource");
const utils_2 = require("./utils");
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
            text = utils_1.AppUtils.faDigits2En(elem.textContent.trim());
            capital = parseInt(utils_1.AppUtils.removeNonDigits(text));
        }
        // دوره
        let period = 0, rpMonth = 0, rpYear = 0;
        elem = document.getElementById('ctl00_lblPeriod');
        if (elem) {
            text = utils_1.AppUtils.faDigits2En(elem.textContent.trim());
            period = parseInt(utils_1.AppUtils.removeNonDigits(text));
        }
        elem = document.getElementById('ctl00_lblPeriodEndToDate');
        if (elem) {
            text = utils_1.AppUtils.faDigits2En(elem.textContent.trim());
            const [year, month] = utils_1.AppUtils.parseJalaliDate(text);
            rpMonth = month;
            rpYear = year;
        }
        // سال مالی
        let smMonth = 0, smYear = 1;
        elem = document.getElementById('ctl00_lblYearEndToDate');
        if (elem) {
            text = utils_1.AppUtils.faDigits2En(elem.textContent.trim());
            const [year, month] = utils_1.AppUtils.parseJalaliDate(text);
            smMonth = month;
            smYear = year;
        }
        // تلفیقی
        let consolidated = false;
        elem = document.getElementById('ctl00_lblReportName');
        if (elem) {
            text = utils_1.AppUtils.ar2fa(elem.textContent);
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
        v0 = utils_1.AppUtils.faDigits2En(v0 ? v0.textContent : '');
        v1 = utils_1.AppUtils.faDigits2En(v1 ? v1.textContent : '');
        [rpYear[0], rpMonth[0]] = utils_1.AppUtils.findJalaliDate(v0);
        [rpYear[1], rpMonth[1]] = utils_1.AppUtils.findJalaliDate(v1);
        // حسابرسی شده یا نشده
        audited[0] = v0.indexOf('حسابرسی شده') !== -1;
        audited[1] = v1.indexOf('حسابرسی شده') !== -1;
        let rows = doc.querySelectorAll('#ctl00_cphBody_ucSFinancialPosition_grdSFinancialPosition tr');
        const readRow = (index, arr1, arr2) => {
            let idx; // = FormatUtils.padNumber(index, 2);
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
        v0 = utils_1.AppUtils.faDigits2En(v0 ? v0.textContent : '');
        v1 = utils_1.AppUtils.faDigits2En(v1 ? v1.textContent : '');
        v2 = utils_1.AppUtils.faDigits2En(v2 ? v2.textContent : '');
        [rpYear[0], rpMonth[0]] = utils_1.AppUtils.findJalaliDate(v0);
        [rpYear[1], rpMonth[1]] = utils_1.AppUtils.findJalaliDate(v1);
        [rpYear[2], rpMonth[2]] = utils_1.AppUtils.findJalaliDate(v2);
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
            const content = await utils_2.fetchReportPageContent(this.letter.url, sheetId);
            this.contentMap.set(sheetId, content);
            return content;
        }
    }
}
exports.CodalN10ReportDataProvider = CodalN10ReportDataProvider;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibjEwUmVwb3J0RGF0YVByb3ZpZGVyLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vLi4vLi4vc3JjL2RhdGEvZmluYW5jaWFsU3RhdGVtZW50cy9wcm92aWRlci9uMTBSZXBvcnREYXRhUHJvdmlkZXIudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7O0FBUUEsaUNBQTRCO0FBQzVCLG1DQUFpQztBQUNqQyx1Q0FBdUM7QUFDdkMsNkNBQXdEO0FBQ3hELG1DQUErQztBQUcvQyxJQUFZLFVBSVg7QUFKRCxXQUFZLFVBQVU7SUFDbEIsMkRBQWdCLENBQUE7SUFDaEIsaUVBQW1CLENBQUE7SUFDbkIsc0ZBQThCLENBQUE7QUFDbEMsQ0FBQyxFQUpXLFVBQVUsR0FBVixrQkFBVSxLQUFWLGtCQUFVLFFBSXJCO0FBRUQsTUFBYSwwQkFBMEI7SUFLbkMsWUFBWSxNQUFrQixFQUFFLGNBQTBCO1FBQ3RELElBQUksQ0FBQyxNQUFNLEdBQUcsTUFBTSxDQUFDO1FBQ3JCLElBQUksQ0FBQyxjQUFjLEdBQUcsY0FBYyxDQUFDO1FBQ3JDLElBQUksQ0FBQyxVQUFVLEdBQUcsSUFBSSxHQUFHLEVBQXNCLENBQUM7SUFDcEQsQ0FBQztJQUVELEtBQUssQ0FBQyxXQUFXO1FBQ2IsTUFBTSxPQUFPLEdBQUcsTUFBTSxJQUFJLENBQUMsVUFBVSxDQUFDLElBQUksQ0FBQyxjQUFjLENBQUMsQ0FBQztRQUMzRCxNQUFNLFFBQVEsR0FBRyxJQUFJLGFBQUssQ0FBQyxPQUFPLENBQUMsQ0FBQyxNQUFNLENBQUMsUUFBUSxDQUFDO1FBQ3BELElBQUksSUFBSSxFQUFFLElBQUksQ0FBQztRQUVmLFVBQVU7UUFDVixJQUFJLEtBQUssR0FBRyxLQUFLLEVBQ2Isb0JBQW9CLEdBQUcsQ0FBQyxFQUN4QixhQUFhLEdBQUcsRUFBRSxDQUFDO1FBQ3ZCLElBQUksR0FBRyxRQUFRLENBQUMsY0FBYyxDQUFDLHdEQUF3RCxDQUFDLENBQUM7UUFDekYsSUFBSSxJQUFJLEVBQUU7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