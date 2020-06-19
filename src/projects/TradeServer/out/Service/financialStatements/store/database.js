"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const dexie_1 = require("dexie");
class FinancialStatementsDatabase extends dexie_1.default {
    constructor() {
        super('FinancialStatementsDatabase');
        this.version(1).stores({
            // N10
            n10Reports: 'tracingNumber, symbol, period, rpYear, rpMonth, smYear, smMonth, sendTime, publishTime, audited, amend, amendedTracingNumber',
            incomeStatements: '++id, reportId',
            balanceSheets: '++id, reportId',
            salesAndCostsTrend: '++id, reportId',
            costOfGoods: '++id, reportId',
            // N30
            n30Reports: 'tracingNumber, symbol, rpYear, rpMonth, smYear, smMonth, sendTime, publishTime, amend, amendedTracingNumber',
            n30ProductItems: '++id, reportId, name',
        });
        // N10
        this.n10Reports = this.table('n10Reports');
        this.incomeStatements = this.table('incomeStatements');
        this.balanceSheets = this.table('balanceSheets');
        this.salesAndCostsTrend = this.table('salesAndCostsTrend');
        this.costOfGoods = this.table('costOfGoods');
        // N30
        this.n30Reports = this.table('n30Reports');
        this.n30ProductItems = this.table('n30ProductItems');
    }
}
exports.default = new FinancialStatementsDatabase();
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiZGF0YWJhc2UuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi8uLi8uLi9zcmMvU2VydmljZS9maW5hbmNpYWxTdGF0ZW1lbnRzL3N0b3JlL2RhdGFiYXNlLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7O0FBQUEsaUNBQTBCO0FBbUIxQixNQUFNLDJCQUE0QixTQUFRLGVBQUs7SUFZM0M7UUFDSSxLQUFLLENBQUMsNkJBQTZCLENBQUMsQ0FBQztRQUNyQyxJQUFJLENBQUMsT0FBTyxDQUFDLENBQUMsQ0FBQyxDQUFDLE1BQU0sQ0FBQztZQUNuQixNQUFNO1lBQ04sVUFBVSxFQUFFLDhIQUE4SDtZQUMxSSxnQkFBZ0IsRUFBRSxnQkFBZ0I7WUFDbEMsYUFBYSxFQUFFLGdCQUFnQjtZQUMvQixrQkFBa0IsRUFBRSxnQkFBZ0I7WUFDcEMsV0FBVyxFQUFFLGdCQUFnQjtZQUU3QixNQUFNO1lBQ04sVUFBVSxFQUFFLDZHQUE2RztZQUN6SCxlQUFlLEVBQUUsc0JBQXNCO1NBQzFDLENBQUMsQ0FBQztRQUVILE1BQU07UUFDTixJQUFJLENBQUMsVUFBVSxHQUFHLElBQUksQ0FBQyxLQUFLLENBQUMsWUFBWSxDQUFDLENBQUM7UUFDM0MsSUFBSSxDQUFDLGdCQUFnQixHQUFHLElBQUksQ0FBQyxLQUFLLENBQUMsa0JBQWtCLENBQUMsQ0FBQztRQUN2RCxJQUFJLENBQUMsYUFBYSxHQUFHLElBQUksQ0FBQyxLQUFLLENBQUMsZUFBZSxDQUFDLENBQUM7UUFDakQsSUFBSSxDQUFDLGtCQUFrQixHQUFHLElBQUksQ0FBQyxLQUFLLENBQUMsb0JBQW9CLENBQUMsQ0FBQztRQUMzRCxJQUFJLENBQUMsV0FBVyxHQUFHLElBQUksQ0FBQyxLQUFLLENBQUMsYUFBYSxDQUFDLENBQUM7UUFFN0MsTUFBTTtRQUNOLElBQUksQ0FBQyxVQUFVLEdBQUcsSUFBSSxDQUFDLEtBQUssQ0FBQyxZQUFZLENBQUMsQ0FBQztRQUMzQyxJQUFJLENBQUMsZUFBZSxHQUFHLElBQUksQ0FBQyxLQUFLLENBQUMsaUJBQWlCLENBQUMsQ0FBQztJQUN6RCxDQUFDO0NBQ0o7QUFFRCxrQkFBZSxJQUFJLDJCQUEyQixFQUFFLENBQUMifQ==