"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.fetchReportPageContent = void 0;
const query_string_1 = require("query-string");
async function fetchReportPageContent(url, sheetId) {
    if (sheetId !== undefined) {
        const qs = query_string_1.stringify({ sheetId });
        url += '&' + qs;
    }
    const res = await fetch(url);
    return await res.text();
}
exports.fetchReportPageContent = fetchReportPageContent;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoidXRpbHMuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi8uLi8uLi9zcmMvU2VydmljZS9maW5hbmNpYWxTdGF0ZW1lbnRzL3Byb3ZpZGVyL3V0aWxzLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztBQUFBLCtDQUF1QztBQUVoQyxLQUFLLFVBQVUsc0JBQXNCLENBQUMsR0FBVyxFQUFFLE9BQWdCO0lBQ3RFLElBQUksT0FBTyxLQUFLLFNBQVMsRUFBRTtRQUN2QixNQUFNLEVBQUUsR0FBRyx3QkFBUyxDQUFDLEVBQUMsT0FBTyxFQUFDLENBQUMsQ0FBQztRQUNoQyxHQUFHLElBQUksR0FBRyxHQUFHLEVBQUUsQ0FBQztLQUNuQjtJQUNELE1BQU0sR0FBRyxHQUFHLE1BQU0sS0FBSyxDQUFDLEdBQUcsQ0FBQyxDQUFDO0lBQzdCLE9BQU8sTUFBTSxHQUFHLENBQUMsSUFBSSxFQUFFLENBQUM7QUFDNUIsQ0FBQztBQVBELHdEQU9DIn0=