"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CodalN30ReportDataProvider = void 0;
const jsdom_1 = require("jsdom");
const __1 = require("..");
const utils_1 = require("./utils");
const dataSource_1 = require("./dataSource");
const utils_2 = require("./utils");
function parseVal(str) {
    return parseInt(utils_1.AppUtils.removeNonDigits(utils_1.AppUtils.faDigits2En(str)));
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
        const content = await utils_2.fetchReportPageContent(this.letter.url);
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
            text = utils_1.AppUtils.faDigits2En(elem.textContent.trim());
            capital = parseInt(utils_1.AppUtils.removeNonDigits(text));
        }
        // دوره
        let rpMonth = 0, rpYear = 0;
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
                    monthDesc = utils_1.AppUtils.ar2fa(c1.getStringValue());
                    yearDesc = utils_1.AppUtils.ar2fa(c2.getStringValue());
                    return [monthDesc, yearDesc];
                }
            }
        }
        // OLD REPORTS
        let elem1 = document.getElementById('txbDescMonth'), elem2 = document.getElementById('txbDescYear');
        if (elem1 && elem2) {
            monthDesc = utils_1.AppUtils.ar2fa(elem1.textContent.trim());
            yearDesc = utils_1.AppUtils.ar2fa(elem2.textContent.trim());
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
                        name: utils_1.AppUtils.ar2fa(name),
                        unit: utils_1.AppUtils.ar2fa(unit),
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
                    name: utils_1.AppUtils.ar2fa(name),
                    unit: utils_1.AppUtils.ar2fa(unit),
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
                    name: utils_1.AppUtils.ar2fa(name),
                    unit: utils_1.AppUtils.ar2fa(unit),
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
                    name: utils_1.AppUtils.ar2fa(name),
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
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibjMwUmVwb3J0RGF0YVByb3ZpZGVyLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vLi4vLi4vc3JjL2RhdGEvZmluYW5jaWFsU3RhdGVtZW50cy9wcm92aWRlci9uMzBSZXBvcnREYXRhUHJvdmlkZXIudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7O0FBQUEsaUNBQTRCO0FBRTVCLDBCQVFZO0FBQ1osbUNBQWlDO0FBQ2pDLDZDQUF3QztBQUN4QyxtQ0FBK0M7QUFHL0MsU0FBUyxRQUFRLENBQUMsR0FBVztJQUN6QixPQUFPLFFBQVEsQ0FBQyxnQkFBUSxDQUFDLGVBQWUsQ0FBQyxnQkFBUSxDQUFDLFdBQVcsQ0FBQyxHQUFHLENBQUMsQ0FBQyxDQUFDLENBQUM7QUFDekUsQ0FBQztBQUVELE1BQWEsMEJBQTBCO0lBS25DLFlBQVksTUFBa0I7UUFIdEIsV0FBTSxHQUFlLElBQUksQ0FBQztRQUMxQixVQUFLLEdBQWUsSUFBSSxDQUFDO1FBRzdCLElBQUksQ0FBQyxNQUFNLEdBQUcsTUFBTSxDQUFDO0lBQ3pCLENBQUM7SUFFRCxLQUFLLENBQUMsVUFBVTtRQUNaLElBQUksSUFBSSxDQUFDLEtBQUssS0FBSyxJQUFJLEVBQUU7WUFDckIsTUFBTSxJQUFJLENBQUMsYUFBYSxFQUFFLENBQUM7U0FDOUI7UUFDRCxPQUFPLElBQUksQ0FBQyxLQUFLLENBQUM7SUFDdEIsQ0FBQztJQUVELEtBQUssQ0FBQyxXQUFXO1FBQ2IsSUFBSSxJQUFJLENBQUMsTUFBTSxLQUFLLElBQUksRUFBRTtZQUN0QixNQUFNLElBQUksQ0FBQyxhQUFhLEVBQUUsQ0FBQztTQUM5QjtRQUNELE9BQU8sSUFBSSxDQUFDLE1BQU0sQ0FBQztJQUN2QixDQUFDO0lBRU8sS0FBSyxDQUFDLGFBQWE7UUFDdkIsTUFBTSxPQUFPLEdBQUcsTUFBTSw4QkFBc0IsQ0FBQyxJQUFJLENBQUMsTUFBTSxDQUFDLEdBQUcsQ0FBQyxDQUFDO1FBQzlELE1BQU0sUUFBUSxHQUFHLElB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