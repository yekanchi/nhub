import {JSDOM} from 'jsdom';

import {
    IN30ContractItem,
    IN30Item,
    IN30ProductItem,
    IN30Report,
    IN3xLetter,
    N30MarketType,
    N30Type
} from "..";
// import {AppUtils} from "./utils/AppUtils";
import {DataSource} from "./dataSource";
import {fetchReportPageContent} from "./utils";
import {ICodalN30ReportDataProvider} from "./index";

function parseVal(str: string): number {
    return parseInt(AppUtils.removeNonDigits(AppUtils.faDigits2En(str)));
}

export class CodalN30ReportDataProvider implements ICodalN30ReportDataProvider {
    private readonly letter: IN3xLetter;
    private report: IN30Report = null;
    private items: IN30Item[] = null;

    constructor(letter: IN3xLetter) {
        this.letter = letter;
    }

    async parseItems(): Promise<IN30Item[]> {
        if (this.items === null) {
            await this.fetchAndParse();
        }
        return this.items;
    }

    async parseReport(): Promise<IN30Report> {
        if (this.report === null) {
            await this.fetchAndParse();
        }
        return this.report;
    }

    private async fetchAndParse() {
        const content = await fetchReportPageContent(this.letter.url);
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
        let rpMonth = 0,
            rpYear = 0;
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

        const dataSource = DataSource.parseFromPageContent(content);

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

    private static parseDescFields(document, ds: DataSource): [string | null, string | null] {
        let monthDesc = null,
            yearDesc = null;

        // NEW REPORTS
        if (ds) {
            let table = ds.getTable(1357) || ds.getTable(1199);
            if (table) {
                let c1 = table.getCell(3, 0),
                    c2 = table.getCell(5, 0);
                if (c1 && c2) {
                    monthDesc = AppUtils.ar2fa(c1.getStringValue());
                    yearDesc = AppUtils.ar2fa(c2.getStringValue());
                    return [monthDesc, yearDesc];
                }
            }
        }

        // OLD REPORTS
        let elem1 = document.getElementById('txbDescMonth'),
            elem2 = document.getElementById('txbDescYear');
        if (elem1 && elem2) {
            monthDesc = AppUtils.ar2fa(elem1.textContent.trim());
            yearDesc = AppUtils.ar2fa(elem2.textContent.trim());
            return [monthDesc, yearDesc];
        }

        console.error('cannot parse desc fields.');
        return [null, null];
    }

    private static parseIncomeFields(document, ds: DataSource): [number | null, number | null] {
        // NEW REPORTS
        if (ds) {
            let table = ds.getTable(1356);
            if (table) {
                let rowsCount = table.getRowsCount(),
                    columnsCount = table.getColumnsCount();
                if (columnsCount === 26) {
                    let vm = table.getCell(rowsCount - 1, 16).getNumberValue(),
                        vy = table.getCell(rowsCount - 1, 20).getNumberValue();
                    return [vm, vy];
                } else {
                    console.error('cannot parse income fields.');
                    return [null, null];
                }
            }

            table = ds.getTable(1197);
            if (table) {
                let rowsCount = table.getRowsCount(),
                    columnsCount = table.getColumnsCount();
                if (columnsCount === 21) {
                    let vm = table.getCell(rowsCount - 1, 16).getNumberValue(),
                        vy = table.getCell(rowsCount - 1, 20).getNumberValue();
                    return [vm, vy];
                } else {
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
            } else {
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
            } else if (row.childElementCount === 10) {
                vm = row.querySelector('td:nth-child(6)').textContent;
                vy = row.querySelector('td:nth-child(10)').textContent;
            } else {
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
            } else if (row.childElementCount === 9) {
                vm = row.querySelector('td:nth-child(6)').textContent;
                vy = row.querySelector('td:nth-child(7)').textContent;
            } else {
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
            } else {
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
            } else if (row.childElementCount === 10) {
                vm = row.querySelector('td:nth-child(9)').textContent;
                vy = row.querySelector('td:nth-child(10)').textContent;
            } else {
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
            } else {
                console.error('cannot parse income fields.');
                return [null, null];
            }
            return [parseVal(vm), parseVal(vy)];
        }

        console.error('cannot parse income fields.');
        return [null, null];
    }

    private static parseItems(document, ds: DataSource): [N30Type, IN30Item[]?] {
        // NEW REPORTS
        if (ds) {
            const addMarketTypeItems = (items: IN30ProductItem[], category: number, type: N30MarketType) => {
                const rowSeqs = table.getRawCells()
                    .filter(c => c.category === category && c.rowTypeName === "CustomRow").map(c => c.rowSequence);
                const rowStart = Math.min(...rowSeqs) - table.getBaseRow();
                const rowEnd = Math.max(...rowSeqs) - table.getBaseRow();
                for (let r = rowStart; r <= rowEnd; r++) {
                    const name = table.getCell(r, 0).getStringValue();
                    const unit = table.getCell(r, 1).getStringValue();
                    const mQuantityProd = table.getCell(r, 13).getNumberValue()
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
            }

            let table = ds.getTable(1356);
            if (table && table.getColumnsCount() === 26) {
                const items: IN30ProductItem[] = [];
                addMarketTypeItems(items, 1, N30MarketType.INTERNAL);
                addMarketTypeItems(items, 2, N30MarketType.EXTERNAL);
                return [N30Type.PRODUCT, items];
            }

            table = ds.getTable(1197);
            if (table && table.getColumnsCount() === 21) {
                const items: IN30ProductItem[] = [];
                addMarketTypeItems(items, 1, N30MarketType.UNKNOWN);
                return [N30Type.PRODUCT, items];
            }

            console.error('cannot parse items.');
            return [N30Type.UNKNOWN];
        }

        // OLD REPORTS
        let rows = document.querySelectorAll('#ctl00_cphBody_ucProduct1_dgProduction tr:not(.lightBlueBg)');
        if (rows.length === 0) {
            rows = document.querySelectorAll('#ctl00_cphBody_ucProductionAndSales1_dgProduction tr:not(.lightBlueBg)');
        }
        if (rows.length > 0) {
            const items: IN30ProductItem[] = [];
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
                } else {
                    console.error('cannot parse items.');
                    return [N30Type.UNKNOWN];
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
                    marketType: N30MarketType.UNKNOWN,
                });
            }
            return [N30Type.PRODUCT, items];
        }
        rows = document.querySelectorAll('#ctl00_cphBody_ucProduct2_dgProduction tr:not(.lightBlueBg)');
        if (rows.length > 0) {
            const items: IN30ProductItem[] = [];
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
                } else if (row.childElementCount === 10) {
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
                } else {
                    console.error('cannot parse items.');
                    return [N30Type.UNKNOWN];
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
                    marketType: N30MarketType.UNKNOWN,
                });
            }
            return [N30Type.PRODUCT, items];
        }
        rows = document.querySelectorAll('#ctl00_cphBody_ucService1_dgContract tr:not(.lightBlueBg)');
        if (rows.length > 0) {
            const items: IN30ContractItem[] = [];
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
                } else if (row.childElementCount === 9) {
                    name = row.querySelector('td:nth-child(1)').textContent;
                    conDate = row.querySelector('td:nth-child(2)').textContent;
                    conDuration = row.querySelector('td:nth-child(3)').textContent;
                    monthIncome = row.querySelector('td:nth-child(6)').textContent;
                    totalIncome = row.querySelector('td:nth-child(7)').textContent;
                    desc = row.querySelector('td:nth-child(9)').textContent;
                } else {
                    console.error('cannot parse items.');
                    return [N30Type.UNKNOWN];
                }
                items.push({
                    name: AppUtils.ar2fa(name),
                    conDate,
                    conDuration: parseVal(conDuration),
                    mValue: parseVal(monthIncome),
                    tValue: parseVal(totalIncome),
                });
            }
            return [N30Type.CONTRACT, items];
        }
        rows = document.querySelectorAll('#ctl00_cphBody_ucBank1_dgFacility tr:not(.lightBlueBg)');
        if (rows.length > 0) {
            const items = [];
            for (let i = 0; i < rows.length; i++) {
                const row = rows[i];
                if (row.childElementCount === 7) {

                } else {
                    console.error('cannot parse items.');
                    return [N30Type.UNKNOWN];
                }
            }
            return [N30Type.BANK, items];
        }
        rows = document.querySelectorAll('#ctl00_cphBody_ucBank2_dgFacility tr:not(.lightBlueBg)');
        if (rows.length > 0) {
            const items = [];
            for (let i = 0; i < rows.length; i++) {
                const row = rows[i];
                if (row.childElementCount === 12) {

                } else if (row.childElementCount === 10) {

                } else {
                    console.error('cannot parse items.');
                    return [N30Type.UNKNOWN];
                }
            }
            return [N30Type.BANK, items];
        }
        rows = document.querySelectorAll('#ctl00_cphBody_ucLeasing1_dgAchievedRevenue tr:not(.lightBlueBg)');
        if (rows.length > 0) {
            const items = [];
            for (let i = 0; i < rows.length; i++) {
                const row = rows[i];
                if (row.childElementCount === 3) {

                } else {
                    console.error('cannot parse items.');
                    return [N30Type.UNKNOWN];
                }
            }
            return [N30Type.LEASING, items];
        }

        console.error('cannot parse items.');
        return [N30Type.UNKNOWN];
    }
}


