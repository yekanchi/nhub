import { IPeriodic } from "../index";
export interface IDataSourceCell {
    category: number;
    rowTypeName: string;
    rowSequence: number;
    columnSequence: number;
    value: string;
    periodEndToDate: string;
    yearEndToDate?: string;
    isVisible: boolean;
}
interface IDataSourceTable {
    metaTableId: number;
    cells: IDataSourceCell[];
}
interface IDataSourceSheet {
    tables: IDataSourceTable[];
}
export interface IDataSource {
    periodEndToDate: string;
    yearEndToDate: string;
    sheets: IDataSourceSheet[];
}
export declare class DataSource {
    private readonly dataSource;
    constructor(dataSource: IDataSource);
    getTable(id: number): DataSourceTable;
    getRawDataSource(): IDataSource;
    static parseFromPageContent(content: string): DataSource;
}
export declare class DataSourceTable {
    private readonly dataSource;
    private readonly table;
    private readonly baseRow;
    constructor(dataSource: IDataSource, table: IDataSourceTable);
    getBaseRow(): number;
    getRowsCount(): number;
    getColumnsCount(): number;
    /**
     * In this method 'row' and 'column' are 0-indexed.
     * @param row
     * @param column
     */
    getCell(row: number, column: number): DataSourceCell;
    getRawCells(): IDataSourceCell[];
}
export declare class DataSourceCell {
    private readonly dataSource;
    private readonly cell;
    constructor(dataSource: IDataSource, cell: IDataSourceCell);
    getNumberValue(): number;
    getStringValue(): string;
    getPeriodEndTo(): IPeriodic;
    getReportDate(): [number, number];
}
export {};
//# sourceMappingURL=dataSource.d.ts.map