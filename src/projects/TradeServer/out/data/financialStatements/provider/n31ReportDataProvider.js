"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CodalN31ReportDataProvider = exports.N31SheetId = void 0;
const utils_1 = require("./utils");
var N31SheetId;
(function (N31SheetId) {
    // صورت خلاصه سرمایه گذاریها به تفکیک گروه صنعت
    N31SheetId[N31SheetId["IndustryGroups"] = 3] = "IndustryGroups";
    // صورت وضعیت پورتفوی شرکتهای پذیرفته شده در بورس
    N31SheetId[N31SheetId["PeriodicPortfolioIn"] = 4] = "PeriodicPortfolioIn";
    // صورت وضعیت پورتفوی شرکتهای خارج از بورس
    N31SheetId[N31SheetId["PeriodicPortfolioOut"] = 5] = "PeriodicPortfolioOut";
    // صورت ريزمعاملات سهام - تحصیل شده
    N31SheetId[N31SheetId["BuyInvestments"] = 6] = "BuyInvestments";
    // صورت ريزمعاملات سهام - واگذار شده
    N31SheetId[N31SheetId["SellInvestments"] = 7] = "SellInvestments";
    // درآمد حاصل از سود سهام محقق شده
    N31SheetId[N31SheetId["DividentsIncome"] = 17] = "DividentsIncome";
})(N31SheetId = exports.N31SheetId || (exports.N31SheetId = {}));
class CodalN31ReportDataProvider {
    constructor(letter, defaultSheetId) {
        this.letter = letter;
        this.defaultSheetId = defaultSheetId;
        this.contentMap = new Map();
    }
    parseReport() {
        return Promise.resolve(undefined);
    }
    parseIndustryGroupInvestmentItems() {
        return Promise.resolve([]);
    }
    parsePeriodicPortfolioInItems() {
        return Promise.resolve([]);
    }
    parsePeriodicPortfolioOutItems() {
        return Promise.resolve([]);
    }
    parseBuyInvestmentItems() {
        return Promise.resolve([]);
    }
    parseSellInvestmentItems() {
        return Promise.resolve([]);
    }
    parseDividentIncomeItems() {
        return Promise.resolve([]);
    }
    async getContent(sheetId) {
        if (this.contentMap.has(sheetId)) {
            return this.contentMap.get(sheetId);
        }
        else {
            const content = await utils_1.fetchReportPageContent(this.letter.url, sheetId);
            this.contentMap.set(sheetId, content);
            return content;
        }
    }
}
exports.CodalN31ReportDataProvider = CodalN31ReportDataProvider;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibjMxUmVwb3J0RGF0YVByb3ZpZGVyLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vLi4vLi4vc3JjL2RhdGEvZmluYW5jaWFsU3RhdGVtZW50cy9wcm92aWRlci9uMzFSZXBvcnREYXRhUHJvdmlkZXIudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7O0FBT0EsbUNBQStDO0FBRy9DLElBQVksVUFhWDtBQWJELFdBQVksVUFBVTtJQUNsQiwrQ0FBK0M7SUFDL0MsK0RBQWtCLENBQUE7SUFDbEIsaURBQWlEO0lBQ2pELHlFQUF1QixDQUFBO0lBQ3ZCLDBDQUEwQztJQUMxQywyRUFBd0IsQ0FBQTtJQUN4QixtQ0FBbUM7SUFDbkMsK0RBQWtCLENBQUE7SUFDbEIsb0NBQW9DO0lBQ3BDLGlFQUFtQixDQUFBO0lBQ25CLGtDQUFrQztJQUNsQyxrRUFBb0IsQ0FBQTtBQUN4QixDQUFDLEVBYlcsVUFBVSxHQUFWLGtCQUFVLEtBQVYsa0JBQVUsUUFhckI7QUFFRCxNQUFhLDBCQUEwQjtJQUtuQyxZQUFZLE1BQWtCLEVBQUUsY0FBMEI7UUFDdEQsSUFBSSxDQUFDLE1BQU0sR0FBRyxNQUFNLENBQUM7UUFDckIsSUFBSSxDQUFDLGNBQWMsR0FBRyxjQUFjLENBQUM7UUFDckMsSUFBSSxDQUFDLFVBQVUsR0FBRyxJQUFJLEdBQUcsRUFBc0IsQ0FBQztJQUNwRCxDQUFDO0lBRUQsV0FBVztRQUNQLE9BQU8sT0FBTyxDQUFDLE9BQU8sQ0FBQyxTQUFTLENBQUMsQ0FBQztJQUN0QyxDQUFDO0lBRUQsaUNBQWlDO1FBQzdCLE9BQU8sT0FBTyxDQUFDLE9BQU8sQ0FBQyxFQUFFLENBQUMsQ0FBQztJQUMvQixDQUFDO0lBRUQsNkJBQTZCO1FBQ3pCLE9BQU8sT0FBTyxDQUFDLE9BQU8sQ0FBQyxFQUFFLENBQUMsQ0FBQztJQUMvQixDQUFDO0lBRUQsOEJBQThCO1FBQzFCLE9BQU8sT0FBTyxDQUFDLE9BQU8sQ0FBQyxFQUFFLENBQUMsQ0FBQztJQUMvQixDQUFDO0lBRUQsdUJBQXVCO1FBQ25CLE9BQU8sT0FBTyxDQUFDLE9BQU8sQ0FBQyxFQUFFLENBQUMsQ0FBQztJQUMvQixDQUFDO0lBRUQsd0JBQXdCO1FBQ3BCLE9BQU8sT0FBTyxDQUFDLE9BQU8sQ0FBQyxFQUFFLENBQUMsQ0FBQztJQUMvQixDQUFDO0lBRUQsd0JBQXdCO1FBQ3BCLE9BQU8sT0FBTyxDQUFDLE9BQU8sQ0FBQyxFQUFFLENBQUMsQ0FBQztJQUMvQixDQUFDO0lBRU8sS0FBSyxDQUFDLFVBQVUsQ0FBQyxPQUFtQjtRQUN4QyxJQUFJLElBQUksQ0FBQyxVQUFVLENBQUMsR0FBRyxDQUFDLE9BQU8sQ0FBQyxFQUFFO1lBQzlCLE9BQU8sSUFBSSxDQUFDLFVBQVUsQ0FBQyxHQUFHLENBQUMsT0FBTyxDQUFDLENBQUM7U0FDdkM7YUFBTTtZQUNILE1BQU0sT0FBTyxHQUFHLE1BQU0sOEJBQXNCLENBQUMsSUFBSSxDQUFDLE1BQU0sQ0FBQyxHQUFHLEVBQUUsT0FBTyxDQUFDLENBQUM7WUFDdkUsSUFBSSxDQUFDLFVBQVUsQ0FBQyxHQUFHLENBQUMsT0FBTyxFQUFFLE9BQU8sQ0FBQyxDQUFDO1lBQ3RDLE9BQU8sT0FBTyxDQUFDO1NBQ2xCO0lBQ0wsQ0FBQztDQUNKO0FBaERELGdFQWdEQyJ9