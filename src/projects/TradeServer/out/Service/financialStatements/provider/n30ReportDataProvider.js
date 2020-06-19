"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CodalN30ReportDataProvider = void 0;
const jsdom_1 = require("jsdom");
const financialStatements_1 = require("data/financialStatements");
const AppUtils_1 = require("utils/AppUtils");
const dataSource_1 = require("data/financialStatements/provider/dataSource");
const utils_1 = require("data/financialStatements/provider/utils");
function parseVal(str) {
    return parseInt(AppUtils_1.AppUtils.removeNonDigits(AppUtils_1.AppUtils.faDigits2En(str)));
}
class CodalN30ReportDataProvider {
    constructor(letter) {
        this.report = null;
        this.items = null;
        this.letter = letter;
    }
    async parseItems() {
        if (this.items === null) {
            await this.fetchAndParse();
        }
        return this.items;
    }
    async parseReport() {
        if (this.report === null) {
            await this.fetchAndParse();
        }
        return this.report;
    }
    async fetchAndParse() {
        const content = await utils_1.fetchReportPageContent(this.letter.url);
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
            amendmentDesc = elem.textContent;
        }
        // سرمایه
        let capital = 0;
        elem = document.getElementById('ctl00_lblListedCapital');
        if (elem) {
            text = AppUtils_1.AppUtils.faDigits2En(elem.textContent.trim());
            capital = parseInt(AppUtils_1.AppUtils.removeNonDigits(text));
        }
        // دوره
        let rpMonth = 0, rpYear = 0;
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
        const dataSource = dataSource_1.DataSource.parseFromPageContent(content);
        // فیلدهای توضیحات
        let [monthDesc, yearDesc] = CodalN30ReportDataProvider.parseDescFields(document, dataSource);
        // فیلدهای جمع مبالغ
        let [monthIncome, yearIncome] = CodalN30ReportDataProvider.parseIncomeFields(document, dataSource);
        const [type, items] = CodalN30ReportDataProvider.parseItems(document, dataSource);
        this.report = {
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
            capital,
            rpMonth,
            rpYear,
            smMonth,
            smYear,
            period: 1,
            monthIncome,
            yearIncome,
            monthDesc,
            yearDesc,
            type,
        };
        this.items = items;
    }
    static parseDescFields(document, ds) {
        let monthDesc = null, yearDesc = null;
        // NEW REPORTS
        if (ds) {
            let table = ds.getTable(1357) || ds.getTable(1199);
            if (table) {
                let c1 = table.getCell(3, 0), c2 = table.getCell(5, 0);
                if (c1 && c2) {
                    monthDesc = AppUtils_1.AppUtils.ar2fa(c1.getStringValue());
                    yearDesc = AppUtils_1.AppUtils.ar2fa(c2.getStringValue());
                    return [monthDesc, yearDesc];
                }
            }
        }
        // OLD REPORTS
        let elem1 = document.getElementById('txbDescMonth'), elem2 = document.getElementById('txbDescYear');
        if (elem1 && elem2) {
            monthDesc = AppUtils_1.AppUtils.ar2fa(elem1.textContent.trim());
            yearDesc = AppUtils_1.AppUtils.ar2fa(elem2.textContent.trim());
            return [monthDesc, yearDesc];
        }
        console.error('cannot parse desc fields.');
        return [null, null];
    }
    static parseIncomeFields(document, ds) {
        // NEW REPORTS
        if (ds) {
            let table = ds.getTable(1356);
            if (table) {
                let rowsCount = table.getRowsCount(), columnsCount = table.getColumnsCount();
                if (columnsCount === 26) {
                    let vm = table.getCell(rowsCount - 1, 16).getNumberValue(), vy = table.getCell(rowsCount - 1, 20).getNumberValue();
                    return [vm, vy];
                }
                else {
                    console.error('cannot parse income fields.');
                    return [null, null];
                }
            }
            table = ds.getTable(1197);
            if (table) {
                let rowsCount = table.getRowsCount(), columnsCount = table.getColumnsCount();
                if (columnsCount === 21) {
                    let vm = table.getCell(rowsCount - 1, 16).getNumberValue(), vy = table.getCell(rowsCount - 1, 20).getNumberValue();
                    return [vm, vy];
                }
                else {
                    console.error('cannot parse income fields.');
                    return [null, null];
                }
            }
            console.error('cannot parse income fields.');
            return [null, null];
        }
        // OLD REPORTS
        let row = document.querySelector('#ctl00_cphBody_ucProduct1_dgProduction tr.lightBlueBg') ||
            document.querySelector('#ctl00_cphBody_ucProductionAndSales1_dgProduction tr.lightBlueBg');
        if (row) {
            let vm, vy;
            if (row.childElementCount === 10) {
                vm = row.querySelector('td:nth-child(6)').textContent;
                vy = row.querySelector('td:nth-child(10)').textContent;
            }
            else {
                console.error('cannot parse income fields.');
                return [null, null];
            }
            return [parseVal(vm), parseVal(vy)];
        }
        row = document.querySelector('#ctl00_cphBody_ucProduct2_dgProduction tr.lightBlueBg');
        if (row) {
            let vm, vy;
            if (row.childElementCount === 21) {
                vm = row.querySelector('td:nth-child(17)').textContent;
                vy = row.querySelector('td:nth-child(21)').textContent;
            }
            else if (row.childElementCount === 10) {
                vm = row.querySelector('td:nth-child(6)').textContent;
                vy = row.querySelector('td:nth-child(10)').textContent;
            }
            else {
                console.error('cannot parse income fields.');
                return [null, null];
            }
            return [parseVal(vm), parseVal(vy)];
        }
        row = document.querySelector('#ctl00_cphBody_ucService1_dgContract tr.lightBlueBg');
        if (row) {
            let vm, vy;
            if (row.childElementCount === 13) {
                vm = row.querySelector('td:nth-child(10)').textContent;
                vy = row.querySelector('td:nth-child(11)').textContent;
            }
            else if (row.childElementCount === 9) {
                vm = row.querySelector('td:nth-child(6)').textContent;
                vy = row.querySelector('td:nth-child(7)').textContent;
            }
            else {
                console.error('cannot parse income fields.');
                return [null, null];
            }
            return [parseVal(vm), parseVal(vy)];
        }
        row = document.querySelector('#ctl00_cphBody_ucBank1_dgFacility tr.lightBlueBg');
        if (row) {
            let vm, vy;
            if (row.childElementCount === 7) {
                vm = row.querySelector('td:nth-child(6)').textContent;
                vy = row.querySelector('td:nth-child(7)').textContent;
            }
            else {
                console.error('cannot parse income fields.');
                return [null, null];
            }
            return [parseVal(vm), parseVal(vy)];
        }
        row = document.querySelector('#ctl00_cphBody_ucBank2_dgFacility tr.lightBlueBg');
        if (row) {
            let vm, vy;
            if (row.childElementCount === 12) {
                vm = row.querySelector('td:nth-child(11)').textContent;
                vy = row.querySelector('td:nth-child(12)').textContent;
            }
            else if (row.childElementCount === 10) {
                vm = row.querySelector('td:nth-child(9)').textContent;
                vy = row.querySelector('td:nth-child(10)').textContent;
            }
            else {
                console.error('cannot parse income fields.');
                return [null, null];
            }
            return [parseVal(vm), parseVal(vy)];
        }
        row = document.querySelector('#ctl00_cphBody_ucLeasing1_dgAchievedRevenue tr.lightBlueBg');
        if (row) {
            let vm, vy;
            if (row.childElementCount === 3) {
                vm = row.querySelector('td:nth-child(2)').textContent;
                vy = row.querySelector('td:nth-child(3)').textContent;
            }
            else {
                console.error('cannot parse income fields.');
                return [null, null];
            }
            return [parseVal(vm), parseVal(vy)];
        }
        console.error('cannot parse income fields.');
        return [null, null];
    }
    static parseItems(document, ds) {
        // NEW REPORTS
        if (ds) {
            const addMarketTypeItems = (items, category, type) => {
                const rowSeqs = table.getRawCells()
                    .filter(c => c.category === category && c.rowTypeName === "CustomRow").map(c => c.rowSequence);
                const rowStart = Math.min(...rowSeqs) - table.getBaseRow();
                const rowEnd = Math.max(...rowSeqs) - table.getBaseRow();
                for (let r = rowStart; r <= rowEnd; r++) {
                    const name = table.getCell(r, 0).getStringValue();
                    const unit = table.getCell(r, 1).getStringValue();
                    const mQuantityProd = table.getCell(r, 13).getNumberValue();
                    const mQuantitySell = table.getCell(r, 14).getNumberValue();
                    const mFee = table.getCell(r, 15).getNumberValue();
                    const mValue = table.getCell(r, 16).getNumberValue();
                    const tQuantityProd = table.getCell(r, 17).getNumberValue();
                    const tQuantitySell = table.getCell(r, 18).getNumberValue();
                    const tFee = table.getCell(r, 19).getNumberValue();
                    const tValue = table.getCell(r, 20).getNumberValue();
                    items.push({
                        name: AppUtils_1.AppUtils.ar2fa(name),
                        unit: AppUtils_1.AppUtils.ar2fa(unit),
                        mQuantityProd,
                        mQuantitySell,
                        mFee,
                        mValue,
                        tQuantityProd,
                        tQuantitySell,
                        tFee,
                        tValue,
                        marketType: type,
                    });
                }
            };
            let table = ds.getTable(1356);
            if (table && table.getColumnsCount() === 26) {
                const items = [];
                addMarketTypeItems(items, 1, financialStatements_1.N30MarketType.INTERNAL);
                addMarketTypeItems(items, 2, financialStatements_1.N30MarketType.EXTERNAL);
                return [financialStatements_1.N30Type.PRODUCT, items];
            }
            table = ds.getTable(1197);
            if (table && table.getColumnsCount() === 21) {
                const items = [];
                addMarketTypeItems(items, 1, financialStatements_1.N30MarketType.UNKNOWN);
                return [financialStatements_1.N30Type.PRODUCT, items];
            }
            console.error('cannot parse items.');
            return [financialStatements_1.N30Type.UNKNOWN];
        }
        // OLD REPORTS
        let rows = document.querySelectorAll('#ctl00_cphBody_ucProduct1_dgProduction tr:not(.lightBlueBg)');
        if (rows.length === 0) {
            rows = document.querySelectorAll('#ctl00_cphBody_ucProductionAndSales1_dgProduction tr:not(.lightBlueBg)');
        }
        if (rows.length > 0) {
            const items = [];
            for (let i = 0; i < rows.length; i++) {
                const row = rows[i];
                let name, unit, mQuantityProd, mQuantitySell, mFee, mValue, tQuantityProd, tQuantitySell, tFee, tValue;
                if (row.childElementCount === 10) {
                    name = row.querySelector('td:nth-child(1)').textContent;
                    unit = row.querySelector('td:nth-child(2)').textContent;
                    mQuantityProd = row.querySelector('td:nth-child(3)').textContent;
                    mQuantitySell = row.querySelector('td:nth-child(4)').textContent;
                    mFee = row.querySelector('td:nth-child(5)').textContent;
                    mValue = row.querySelector('td:nth-child(6)').textContent;
                    tQuantityProd = row.querySelector('td:nth-child(7)').textContent;
                    tQuantitySell = row.querySelector('td:nth-child(8)').textContent;
                    tFee = row.querySelector('td:nth-child(9)').textContent;
                    tValue = row.querySelector('td:nth-child(10)').textContent;
                }
                else {
                    console.error('cannot parse items.');
                    return [financialStatements_1.N30Type.UNKNOWN];
                }
                items.push({
                    name: AppUtils_1.AppUtils.ar2fa(name),
                    unit: AppUtils_1.AppUtils.ar2fa(unit),
                    mQuantityProd: parseVal(mQuantityProd),
                    mQuantitySell: parseVal(mQuantitySell),
                    mFee: parseVal(mFee),
                    mValue: parseVal(mValue),
                    tQuantityProd: parseVal(tQuantityProd),
                    tQuantitySell: parseVal(tQuantitySell),
                    tFee: parseVal(tFee),
                    tValue: parseVal(tValue),
                    marketType: financialStatements_1.N30MarketType.UNKNOWN,
                });
            }
            return [financialStatements_1.N30Type.PRODUCT, items];
        }
        rows = document.querySelectorAll('#ctl00_cphBody_ucProduct2_dgProduction tr:not(.lightBlueBg)');
        if (rows.length > 0) {
            const items = [];
            for (let i = 0; i < rows.length; i++) {
                const row = rows[i];
                let name, unit, mQuantityProd, mQuantitySell, mFee, mValue, tQuantityProd, tQuantitySell, tFee, tValue;
                if (row.childElementCount === 21) {
                    name = row.querySelector('td:nth-child(1)').textContent;
                    unit = row.querySelector('td:nth-child(2)').textContent;
                    mQuantityProd = row.querySelector('td:nth-child(14)').textContent;
                    mQuantitySell = row.querySelector('td:nth-child(15)').textContent;
                    mFee = row.querySelector('td:nth-child(16)').textContent;
                    mValue = row.querySelector('td:nth-child(17)').textContent;
                    tQuantityProd = row.querySelector('td:nth-child(18)').textContent;
                    tQuantitySell = row.querySelector('td:nth-child(19)').textContent;
                    tFee = row.querySelector('td:nth-child(20)').textContent;
                    tValue = row.querySelector('td:nth-child(21)').textContent;
                }
                else if (row.childElementCount === 10) {
                    name = row.querySelector('td:nth-child(1)').textContent;
                    unit = row.querySelector('td:nth-child(2)').textContent;
                    mQuantityProd = row.querySelector('td:nth-child(3)').textContent;
                    mQuantitySell = row.querySelector('td:nth-child(4)').textContent;
                    mFee = row.querySelector('td:nth-child(5)').textContent;
                    mValue = row.querySelector('td:nth-child(6)').textContent;
                    tQuantityProd = row.querySelector('td:nth-child(7)').textContent;
                    tQuantitySell = row.querySelector('td:nth-child(8)').textContent;
                    tFee = row.querySelector('td:nth-child(9)').textContent;
                    tValue = row.querySelector('td:nth-child(10)').textContent;
                }
                else {
                    console.error('cannot parse items.');
                    return [financialStatements_1.N30Type.UNKNOWN];
                }
                items.push({
                    name: AppUtils_1.AppUtils.ar2fa(name),
                    unit: AppUtils_1.AppUtils.ar2fa(unit),
                    mQuantityProd: parseVal(mQuantityProd),
                    mQuantitySell: parseVal(mQuantitySell),
                    mFee: parseVal(mFee),
                    mValue: parseVal(mValue),
                    tQuantityProd: parseVal(tQuantityProd),
                    tQuantitySell: parseVal(tQuantitySell),
                    tFee: parseVal(tFee),
                    tValue: parseVal(tValue),
                    marketType: financialStatements_1.N30MarketType.UNKNOWN,
                });
            }
            return [financialStatements_1.N30Type.PRODUCT, items];
        }
        rows = document.querySelectorAll('#ctl00_cphBody_ucService1_dgContract tr:not(.lightBlueBg)');
        if (rows.length > 0) {
            const items = [];
            for (let i = 0; i < rows.length; i++) {
                const row = rows[i];
                let name, conDate, conDuration, monthIncome, totalIncome, desc;
                if (row.childElementCount === 13) {
                    name = row.querySelector('td:nth-child(2)').textContent;
                    conDate = row.querySelector('td:nth-child(3)').textContent;
                    conDuration = row.querySelector('td:nth-child(4)').textContent;
                    monthIncome = row.querySelector('td:nth-child(10)').textContent;
                    totalIncome = row.querySelector('td:nth-child(11)').textContent;
                    desc = row.querySelector('td:nth-child(13)').textContent;
                }
                else if (row.childElementCount === 9) {
                    name = row.querySelector('td:nth-child(1)').textContent;
                    conDate = row.querySelector('td:nth-child(2)').textContent;
                    conDuration = row.querySelector('td:nth-child(3)').textContent;
                    monthIncome = row.querySelector('td:nth-child(6)').textContent;
                    totalIncome = row.querySelector('td:nth-child(7)').textContent;
                    desc = row.querySelector('td:nth-child(9)').textContent;
                }
                else {
                    console.error('cannot parse items.');
                    return [financialStatements_1.N30Type.UNKNOWN];
                }
                items.push({
                    name: AppUtils_1.AppUtils.ar2fa(name),
                    conDate,
                    conDuration: parseVal(conDuration),
                    mValue: parseVal(monthIncome),
                    tValue: parseVal(totalIncome),
                });
            }
            return [financialStatements_1.N30Type.CONTRACT, items];
        }
        rows = document.querySelectorAll('#ctl00_cphBody_ucBank1_dgFacility tr:not(.lightBlueBg)');
        if (rows.length > 0) {
            const items = [];
            for (let i = 0; i < rows.length; i++) {
                const row = rows[i];
                if (row.childElementCount === 7) {
                }
                else {
                    console.error('cannot parse items.');
                    return [financialStatements_1.N30Type.UNKNOWN];
                }
            }
            return [financialStatements_1.N30Type.BANK, items];
        }
        rows = document.querySelectorAll('#ctl00_cphBody_ucBank2_dgFacility tr:not(.lightBlueBg)');
        if (rows.length > 0) {
            const items = [];
            for (let i = 0; i < rows.length; i++) {
                const row = rows[i];
                if (row.childElementCount === 12) {
                }
                else if (row.childElementCount === 10) {
                }
                else {
                    console.error('cannot parse items.');
                    return [financialStatements_1.N30Type.UNKNOWN];
                }
            }
            return [financialStatements_1.N30Type.BANK, items];
        }
        rows = document.querySelectorAll('#ctl00_cphBody_ucLeasing1_dgAchievedRevenue tr:not(.lightBlueBg)');
        if (rows.length > 0) {
            const items = [];
            for (let i = 0; i < rows.length; i++) {
                const row = rows[i];
                if (row.childElementCount === 3) {
                }
                else {
                    console.error('cannot parse items.');
                    return [financialStatements_1.N30Type.UNKNOWN];
                }
            }
            return [financialStatements_1.N30Type.LEASING, items];
        }
        console.error('cannot parse items.');
        return [financialStatements_1.N30Type.UNKNOWN];
    }
}
exports.CodalN30ReportDataProvider = CodalN30ReportDataProvider;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibjMwUmVwb3J0RGF0YVByb3ZpZGVyLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vLi4vLi4vc3JjL1NlcnZpY2UvZmluYW5jaWFsU3RhdGVtZW50cy9wcm92aWRlci9uMzBSZXBvcnREYXRhUHJvdmlkZXIudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7O0FBQUEsaUNBQTRCO0FBRTVCLGtFQVFrQztBQUNsQyw2Q0FBd0M7QUFDeEMsNkVBQXdFO0FBQ3hFLG1FQUErRTtBQUcvRSxTQUFTLFFBQVEsQ0FBQyxHQUFXO0lBQ3pCLE9BQU8sUUFBUSxDQUFDLG1CQUFRLENBQUMsZUFBZSxDQUFDLG1CQUFRLENBQUMsV0FBVyxDQUFDLEdBQUcsQ0FBQyxDQUFDLENBQUMsQ0FBQztBQUN6RSxDQUFDO0FBRUQsTUFBYSwwQkFBMEI7SUFLbkMsWUFBWSxNQUFrQjtRQUh0QixXQUFNLEdBQWUsSUFBSSxDQUFDO1FBQzFCLFVBQUssR0FBZSxJQUFJLENBQUM7UUFHN0IsSUFBSSxDQUFDLE1BQU0sR0FBRyxNQUFNLENBQUM7SUFDekIsQ0FBQztJQUVELEtBQUssQ0FBQyxVQUFVO1FBQ1osSUFBSSxJQUFJLENBQUMsS0FBSyxLQUFLLElBQUksRUFBRTtZQUNyQixNQUFNLElBQUksQ0FBQyxhQUFhLEVBQUUsQ0FBQztTQUM5QjtRQUNELE9BQU8sSUFBSSxDQUFDLEtBQUssQ0FBQztJQUN0QixDQUFDO0lBRUQsS0FBSyxDQUFDLFdBQVc7UUFDYixJQUFJLElBQUksQ0FBQyxNQUFNLEtBQUssSUFBSSxFQUFFO1lBQ3RCLE1BQU0sSUFBSSxDQUFDLGFBQWEsRUFBRSxDQUFDO1NBQzlCO1FBQ0QsT0FBTyxJQUFJLENBQUMsTUFBTSxDQUFDO0lBQ3ZCLENBQUM7SUFFTyxLQUFLLENBQUMsYUFBYTtRQUN2QixNQUFNLE9BQU8sR0FBRyxNQUFNLDhCQUFzQixDQUFDLElBQUksQ0FBQyxNQUFNLENBQUMsR0FBRyxDQUFDLENBQUM7UUFDOUQsTUFBTSxRQUFRLEdB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