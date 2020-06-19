"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.DataSourceCell = exports.DataSourceTable = exports.DataSource = void 0;
const AppUtils_1 = require("utils/AppUtils");
class DataSource {
    constructor(dataSource) {
        this.dataSource = dataSource;
    }
    getTable(id) {
        const table = this.dataSource.sheets[0].tables.find(t => t.metaTableId === id);
        if (table) {
            return new DataSourceTable(this.dataSource, table);
        }
        return null;
    }
    getRawDataSource() {
        return this.dataSource;
    }
    static parseFromPageContent(content) {
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
exports.DataSource = DataSource;
class DataSourceTable {
    constructor(dataSource, table) {
        this.dataSource = dataSource;
        this.table = table;
        this.baseRow = Math.min(...this.table.cells.map(c => c.rowSequence));
    }
    getBaseRow() {
        return this.baseRow;
    }
    getRowsCount() {
        let max = Math.max(...this.table.cells.map(c => c.rowSequence));
        return max - this.baseRow + 1;
    }
    getColumnsCount() {
        return Math.max(...this.table.cells.map(c => c.columnSequence));
    }
    /**
     * In this method 'row' and 'column' are 0-indexed.
     * @param row
     * @param column
     */
    getCell(row, column) {
        row = this.baseRow + row;
        column = column + 1;
        const cell = this.table.cells.find(c => c.rowSequence === row && c.columnSequence === column);
        if (cell) {
            return new DataSourceCell(this.dataSource, cell);
        }
        return null;
    }
    getRawCells() {
        return this.table.cells;
    }
}
exports.DataSourceTable = DataSourceTable;
class DataSourceCell {
    constructor(dataSource, cell) {
        this.dataSource = dataSource;
        this.cell = cell;
    }
    getNumberValue() {
        return parseInt(this.cell.value);
    }
    getStringValue() {
        return this.cell.value;
    }
    getPeriodEndTo() {
        const [rpYear, rpMonth] = AppUtils_1.AppUtils.parseJalaliDate(this.cell.periodEndToDate);
        let smYear, smMonth;
        if (this.cell.yearEndToDate) {
            [smYear, smMonth] = AppUtils_1.AppUtils.parseJalaliDate(this.cell.yearEndToDate);
        }
        else {
            const [crpYear, crpMonth] = AppUtils_1.AppUtils.parseJalaliDate(this.dataSource.periodEndToDate);
            const [csmYear, csmMonth] = AppUtils_1.AppUtils.parseJalaliDate(this.dataSource.yearEndToDate);
            if (rpYear === crpYear && rpMonth === crpMonth) {
                [smYear, smMonth] = [csmYear, csmMonth];
            }
            else if ((rpYear * 100 + rpMonth) < (crpYear * 100 + crpMonth)) {
                [smYear, smMonth] = [rpYear, rpMonth];
            }
            else if (rpYear === csmYear && rpMonth === csmMonth) {
                [smYear, smMonth] = [csmYear, csmMonth];
            }
            else {
                [smYear, smMonth] = [csmYear + 1, csmMonth];
            }
        }
        const period = AppUtils_1.AppUtils.calculatePeriod(rpYear, rpMonth, smYear, smMonth);
        return { rpMonth, rpYear, period };
    }
    getReportDate() {
        // TODO: day=1
        const [rpYear, rpMonth] = AppUtils_1.AppUtils.parseJalaliDate(this.cell.periodEndToDate);
        return [rpYear, rpMonth];
    }
}
exports.DataSourceCell = DataSourceCell;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiZGF0YVNvdXJjZS5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbIi4uLy4uLy4uLy4uL3NyYy9kYXRhL2ZpbmFuY2lhbFN0YXRlbWVudHMvcHJvdmlkZXIvZGF0YVNvdXJjZS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7QUFBQSw2Q0FBd0M7QUE2QnhDLE1BQWEsVUFBVTtJQUduQixZQUFZLFVBQXVCO1FBQy9CLElBQUksQ0FBQyxVQUFVLEdBQUcsVUFBVSxDQUFDO0lBQ2pDLENBQUM7SUFFRCxRQUFRLENBQUMsRUFBVTtRQUNmLE1BQU0sS0FBSyxHQUFHLElBQUksQ0FBQyxVQUFVLENBQUMsTUFBTSxDQUFDLENBQUMsQ0FBQyxDQUFDLE1BQU0sQ0FBQyxJQUFJLENBQUMsQ0FBQyxDQUFDLEVBQUUsQ0FBQyxDQUFDLENBQUMsV0FBVyxLQUFLLEVBQUUsQ0FBQyxDQUFDO1FBQy9FLElBQUksS0FBSyxFQUFFO1lBQ1AsT0FBTyxJQUFJLGVBQWUsQ0FBQyxJQUFJLENBQUMsVUFBVSxFQUFFLEtBQUssQ0FBQyxDQUFDO1NBQ3REO1FBQ0QsT0FBTyxJQUFJLENBQUM7SUFDaEIsQ0FBQztJQUVELGdCQUFnQjtRQUNaLE9BQU8sSUFBSSxDQUFDLFVBQVUsQ0FBQztJQUMzQixDQUFDO0lBRUQsTUFBTSxDQUFDLG9CQUFvQixDQUFDLE9BQWU7UUFDdkMsSUFBSSxPQUFPLEdBQUcsT0FBTyxDQUFDLEtBQUssQ0FBQywyQkFBMkIsQ0FBQyxDQUFDO1FBQ3pELElBQUksQ0FBQyxPQUFPLEVBQUU7WUFDVixPQUFPLEdBQUcsT0FBTyxDQUFDLEtBQUssQ0FBQyx3QkFBd0IsQ0FBQyxDQUFDO1NBQ3JEO1FBQ0QsSUFBSSxPQUFPLEVBQUU7WUFDVCxPQUFPLElBQUksVUFBVSxDQUFDLElBQUksQ0FBQyxLQUFLLENBQUMsT0FBTyxDQUFDLENBQUMsQ0FBQyxDQUFDLENBQUMsQ0FBQztTQUNqRDtRQUNELE9BQU8sSUFBSSxDQUFDO0lBQ2hCLENBQUM7Q0FDSjtBQTdC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