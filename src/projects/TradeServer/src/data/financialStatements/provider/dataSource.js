"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AppUtils_1 = require("../../../utils/AppUtils");
function parseDataSourceFromPageContent(content) {
    var matched = content.match(/var rawdatasource = (.*);/);
    if (matched) {
        return new DataSource(JSON.parse(matched[1]));
    }
    return null;
}
exports.parseDataSourceFromPageContent = parseDataSourceFromPageContent;
var DataSource = /** @class */ (function () {
    function DataSource(dataSource) {
        this.dataSource = dataSource;
    }
    DataSource.prototype.getTable = function (id) {
        var table = this.dataSource.sheets[0].tables.find(function (t) { return t.metaTableId === id; });
        if (table) {
            return new DataSourceTable(this.dataSource, table);
        }
        return null;
    };
    DataSource.prototype.getRawDataSource = function () {
        return this.dataSource;
    };
    return DataSource;
}());
exports.DataSource = DataSource;
var DataSourceTable = /** @class */ (function () {
    function DataSourceTable(dataSource, table) {
        this.dataSource = dataSource;
        this.table = table;
        this.baseRow = Math.min.apply(Math, this.table.cells.map(function (c) { return c.rowSequence; }));
    }
    DataSourceTable.prototype.getBaseRow = function () {
        return this.baseRow;
    };
    DataSourceTable.prototype.getCell = function (row, column) {
        row = this.baseRow + row;
        column = column + 1;
        var cell = this.table.cells.find(function (c) { return c.rowSequence === row && c.columnSequence === column; });
        if (cell) {
            return new DataSourceCell(this.dataSource, cell);
        }
        return null;
    };
    DataSourceTable.prototype.getRawCells = function () {
        return this.table.cells;
    };
    return DataSourceTable;
}());
exports.DataSourceTable = DataSourceTable;
var DataSourceCell = /** @class */ (function () {
    function DataSourceCell(dataSource, cell) {
        this.dataSource = dataSource;
        this.cell = cell;
    }
    DataSourceCell.prototype.getNumberValue = function () {
        return parseInt(this.cell.value);
    };
    DataSourceCell.prototype.getStringValue = function () {
        return this.cell.value;
    };
    DataSourceCell.prototype.getPeriodEndTo = function () {
        var _a, _b, _c, _d, _e;
        var _f = AppUtils_1.AppUtils.parseJalaliDate(this.cell.periodEndToDate), rpYear = _f[0], rpMonth = _f[1];
        var smYear, smMonth;
        if (this.cell.yearEndToDate) {
            _a = AppUtils_1.AppUtils.parseJalaliDate(this.cell.yearEndToDate), smYear = _a[0], smMonth = _a[1];
        }
        else {
            var _g = AppUtils_1.AppUtils.parseJalaliDate(this.dataSource.periodEndToDate), crpYear = _g[0], crpMonth = _g[1];
            var _h = AppUtils_1.AppUtils.parseJalaliDate(this.dataSource.yearEndToDate), csmYear = _h[0], csmMonth = _h[1];
            if (rpYear === crpYear && rpMonth === crpMonth) {
                _b = [csmYear, csmMonth], smYear = _b[0], smMonth = _b[1];
            }
            else if ((rpYear * 100 + rpMonth) < (crpYear * 100 + crpMonth)) {
                _c = [rpYear, rpMonth], smYear = _c[0], smMonth = _c[1];
            }
            else if (rpYear === csmYear && rpMonth === csmMonth) {
                _d = [csmYear, csmMonth], smYear = _d[0], smMonth = _d[1];
            }
            else {
                _e = [csmYear + 1, csmMonth], smYear = _e[0], smMonth = _e[1];
            }
        }
        return { rpMonth: rpMonth, rpYear: rpYear, smMonth: smMonth, smYear: smYear };
    };
    return DataSourceCell;
}());
exports.DataSourceCell = DataSourceCell;
//# sourceMappingURL=dataSource.js.map