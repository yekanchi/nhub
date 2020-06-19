"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CodalN30ReportDataProvider = void 0;
const jsdom_1 = require("jsdom");
const __1 = require("..");
// import {AppUtils} from "./utils/AppUtils";
const dataSource_1 = require("./dataSource");
const utils_1 = require("./utils");
function parseVal(str) {
    return parseInt(AppUtils.removeNonDigits(AppUtils.faDigits2En(str)));
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
            text = AppUtils.faDigits2En(elem.textContent.trim());
            capital = parseInt(AppUtils.removeNonDigits(text));
        }
        // دوره
        let rpMonth = 0, rpYear = 0;
        elem = document.getElementById('ctl00_lblPeriodEndToDate');
        if (elem) {
            text = AppUtils.faDigits2En(elem.textContent.trim());
            const [year, month] = AppUtils.parseJalaliDate(text);
            rpMonth = month;
            rpYear = year;
        }
        // سال مالی
        let smMonth = 0, smYear = 1;
        elem = document.getElementById('ctl00_lblYearEndToDate');
        if (elem) {
            text = AppUtils.faDigits2En(elem.textContent.trim());
            const [year, month] = AppUtils.parseJalaliDate(text);
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
                    monthDesc = AppUtils.ar2fa(c1.getStringValue());
                    yearDesc = AppUtils.ar2fa(c2.getStringValue());
                    return [monthDesc, yearDesc];
                }
            }
        }
        // OLD REPORTS
        let elem1 = document.getElementById('txbDescMonth'), elem2 = document.getElementById('txbDescYear');
        if (elem1 && elem2) {
            monthDesc = AppUtils.ar2fa(elem1.textContent.trim());
            yearDesc = AppUtils.ar2fa(elem2.textContent.trim());
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
                        name: AppUtils.ar2fa(name),
                        unit: AppUtils.ar2fa(unit),
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
                addMarketTypeItems(items, 1, __1.N30MarketType.INTERNAL);
                addMarketTypeItems(items, 2, __1.N30MarketType.EXTERNAL);
                return [__1.N30Type.PRODUCT, items];
            }
            table = ds.getTable(1197);
            if (table && table.getColumnsCount() === 21) {
                const items = [];
                addMarketTypeItems(items, 1, __1.N30MarketType.UNKNOWN);
                return [__1.N30Type.PRODUCT, items];
            }
            console.error('cannot parse items.');
            return [__1.N30Type.UNKNOWN];
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
                    return [__1.N30Type.UNKNOWN];
                }
                items.push({
                    name: AppUtils.ar2fa(name),
                    unit: AppUtils.ar2fa(unit),
                    mQuantityProd: parseVal(mQuantityProd),
                    mQuantitySell: parseVal(mQuantitySell),
                    mFee: parseVal(mFee),
                    mValue: parseVal(mValue),
                    tQuantityProd: parseVal(tQuantityProd),
                    tQuantitySell: parseVal(tQuantitySell),
                    tFee: parseVal(tFee),
                    tValue: parseVal(tValue),
                    marketType: __1.N30MarketType.UNKNOWN,
                });
            }
            return [__1.N30Type.PRODUCT, items];
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
                    return [__1.N30Type.UNKNOWN];
                }
                items.push({
                    name: AppUtils.ar2fa(name),
                    unit: AppUtils.ar2fa(unit),
                    mQuantityProd: parseVal(mQuantityProd),
                    mQuantitySell: parseVal(mQuantitySell),
                    mFee: parseVal(mFee),
                    mValue: parseVal(mValue),
                    tQuantityProd: parseVal(tQuantityProd),
                    tQuantitySell: parseVal(tQuantitySell),
                    tFee: parseVal(tFee),
                    tValue: parseVal(tValue),
                    marketType: __1.N30MarketType.UNKNOWN,
                });
            }
            return [__1.N30Type.PRODUCT, items];
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
                    return [__1.N30Type.UNKNOWN];
                }
                items.push({
                    name: AppUtils.ar2fa(name),
                    conDate,
                    conDuration: parseVal(conDuration),
                    mValue: parseVal(monthIncome),
                    tValue: parseVal(totalIncome),
                });
            }
            return [__1.N30Type.CONTRACT, items];
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
                    return [__1.N30Type.UNKNOWN];
                }
            }
            return [__1.N30Type.BANK, items];
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
                    return [__1.N30Type.UNKNOWN];
                }
            }
            return [__1.N30Type.BANK, items];
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
                    return [__1.N30Type.UNKNOWN];
                }
            }
            return [__1.N30Type.LEASING, items];
        }
        console.error('cannot parse items.');
        return [__1.N30Type.UNKNOWN];
    }
}
exports.CodalN30ReportDataProvider = CodalN30ReportDataProvider;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibjMwUmVwb3J0RGF0YVByb3ZpZGVyLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vLi4vLi4vc3JjL2RhdGEvZmluYW5jaWFsU3RhdGVtZW50cy9wcm92aWRlci9uMzBSZXBvcnREYXRhUHJvdmlkZXIudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7O0FBQUEsaUNBQTRCO0FBRTVCLDBCQVFZO0FBQ1osNkNBQTZDO0FBQzdDLDZDQUF3QztBQUN4QyxtQ0FBK0M7QUFHL0MsU0FBUyxRQUFRLENBQUMsR0FBVztJQUN6QixPQUFPLFFBQVEsQ0FBQyxRQUFRLENBQUMsZUFBZSxDQUFDLFFBQVEsQ0FBQyxXQUFXLENBQUMsR0FBRyxDQUFDLENBQUMsQ0FBQyxDQUFDO0FBQ3pFLENBQUM7QUFFRCxNQUFhLDBCQUEwQjtJQUtuQyxZQUFZLE1BQWtCO1FBSHRCLFdBQU0sR0FBZSxJQUFJLENBQUM7UUFDMUIsVUFBSyxHQUFlLElBQUksQ0FBQztRQUc3QixJQUFJLENBQUMsTUFBTSxHQUFHLE1BQU0sQ0FBQztJQUN6QixDQUFDO0lBRUQsS0FBSyxDQUFDLFVBQVU7UUFDWixJQUFJLElBQUksQ0FBQyxLQUFLLEtBQUssSUFBSSxFQUFFO1lBQ3JCLE1BQU0sSUFBSSxDQUFDLGFBQWEsRUFBRSxDQUFDO1NBQzlCO1FBQ0QsT0FBTyxJQUFJLENBQUMsS0FBSyxDQUFDO0lBQ3RCLENBQUM7SUFFRCxLQUFLLENBQUMsV0FBVztRQUNiLElBQUksSUFBSSxDQUFDLE1BQU0sS0FBSyxJQUFJLEVBQUU7WUFDdEIsTUFBTSxJQUFJLENBQUMsYUFBYSxFQUFFLENBQUM7U0FDOUI7UUFDRCxPQUFPLElBQUksQ0FBQyxNQUFNLENBQUM7SUFDdkIsQ0FBQztJQUVPLEtBQUssQ0FBQyxhQUFhO1FBQ3ZCLE1BQU0sT0FBTyxHQUFHLE1BQU0sOEJBQXNCLENBQUMsSUFBSSxDQUFDLE1BQU0sQ0FBQyxHQUFHLENBQUMsQ0FBQztRQUM5RCxNQUFNLFFBQVEsR0FBRyxJQUFJ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