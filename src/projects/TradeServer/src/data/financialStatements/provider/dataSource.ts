import {AppUtils} from "utils/AppUtils";
import {IPeriodic} from "../index";

export interface IDataSourceCell {
    category: number,
    rowTypeName: string,
    rowSequence: number,
    columnSequence: number,
    value: string,
    periodEndToDate: string,
    yearEndToDate?: string,
    isVisible: boolean,
}

interface IDataSourceTable {
    metaTableId: number,
    cells: IDataSourceCell[]
}

interface IDataSourceSheet {
    tables: IDataSourceTable[]
}

export interface IDataSource {
    periodEndToDate: string,
    yearEndToDate: string,
    sheets: IDataSourceSheet[]
}

export class DataSource {
    private readonly dataSource: IDataSource;

    constructor(dataSource: IDataSource) {
        this.dataSource = dataSource;
    }

    getTable(id: number): DataSourceTable {
        const table = this.dataSource.sheets[0].tables.find(t => t.metaTableId === id);
        if (table) {
            return new DataSourceTable(this.dataSource, table);
        }
        return null;
    }

    getRawDataSource(): IDataSource {
        return this.dataSource;
    }

    static parseFromPageContent(content: string): DataSource {
        let matches = content.match(/var rawdatasource = (.*);/);
        if (!matches) {
            matches = content.match(/var datasource = (.*);/);
        }
        if (matches) {
            return new DataSource(JSON.parse(matches[1]));
        }
        return null;
    }
}

export class DataSourceTable {
    private readonly dataSource: IDataSource;
    private readonly table: IDataSourceTable;
    private readonly baseRow: number;

    constructor(dataSource: IDataSource, table: IDataSourceTable) {
        this.dataSource = dataSource;
        this.table = table;
        this.baseRow = Math.min(...this.table.cells.map(c => c.rowSequence));
    }

    getBaseRow(): number {
        return this.baseRow;
    }

    getRowsCount(): number {
        let max = Math.max(...this.table.cells.map(c => c.rowSequence));
        return max - this.baseRow + 1;
    }

    getColumnsCount(): number {
        return Math.max(...this.table.cells.map(c => c.columnSequence));
    }

    /**
     * In this method 'row' and 'column' are 0-indexed.
     * @param row
     * @param column
     */
    getCell(row: number, column: number): DataSourceCell {
        row = this.baseRow + row;
        column = column + 1;
        const cell = this.table.cells.find(c => c.rowSequence === row && c.columnSequence === column);
        if (cell) {
            return new DataSourceCell(this.dataSource, cell);
        }
        return null;
    }

    getRawCells(): IDataSourceCell[] {
        return this.table.cells;
    }
}

export class DataSourceCell {
    private readonly dataSource: IDataSource;
    private readonly cell: IDataSourceCell;

    constructor(dataSource: IDataSource, cell: IDataSourceCell) {
        this.dataSource = dataSource;
        this.cell = cell;
    }

    getNumberValue(): number {
        return parseInt(this.cell.value);
    }

    getStringValue(): string {
        return this.cell.value;
    }

    getPeriodEndTo(): IPeriodic {
        const [rpYear, rpMonth] = AppUtils.parseJalaliDate(this.cell.periodEndToDate);
        let smYear, smMonth;
        if (this.cell.yearEndToDate) {
            [smYear, smMonth] = AppUtils.parseJalaliDate(this.cell.yearEndToDate);
        } else {
            const [crpYear, crpMonth] = AppUtils.parseJalaliDate(this.dataSource.periodEndToDate);
            const [csmYear, csmMonth] = AppUtils.parseJalaliDate(this.dataSource.yearEndToDate);
            if (rpYear === crpYear && rpMonth === crpMonth) {
                [smYear, smMonth] = [csmYear, csmMonth];
            } else if ((rpYear * 100 + rpMonth) < (crpYear * 100 + crpMonth)) {
                [smYear, smMonth] = [rpYear, rpMonth];
            } else if (rpYear === csmYear && rpMonth === csmMonth) {
                [smYear, smMonth] = [csmYear, csmMonth];
            } else {
                [smYear, smMonth] = [csmYear + 1, csmMonth];
            }
        }
        const period = AppUtils.calculatePeriod(rpYear, rpMonth, smYear, smMonth);
        return {rpMonth, rpYear, period};
    }

    getReportDate(): [number, number] {
        // TODO: day=1
        const [rpYear, rpMonth] = AppUtils.parseJalaliDate(this.cell.periodEndToDate);
        return [rpYear, rpMonth];
    }
}