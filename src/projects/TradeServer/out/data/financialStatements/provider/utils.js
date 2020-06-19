"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppUtils = exports.fetchReportPageContent = void 0;
const query_string_1 = require("query-string");
const persianJs = require("persianjs");
async function fetchReportPageContent(url, sheetId) {
    if (sheetId !== undefined) {
        const qs = query_string_1.stringify({ sheetId });
        url += '&' + qs;
    }
    const res = await fetch(url);
    return await res.text();
}
exports.fetchReportPageContent = fetchReportPageContent;
var AppUtils;
(function (AppUtils) {
    function ar2fa(str) {
        if (str == null || str.length === 0) {
            return str;
        }
        return persianJs(str).arabicChar().toString();
    }
    AppUtils.ar2fa = ar2fa;
    function faDigits2En(str) {
        if (str == null || str.length === 0) {
            return str;
        }
        return persianJs(str).toEnglishNumber().toString();
    }
    AppUtils.faDigits2En = faDigits2En;
    function removeNonDigits(str) {
        return str.replace(/[^0-9]/g, '');
    }
    AppUtils.removeNonDigits = removeNonDigits;
    // example: "1398/11/15" => [1398, 11, 15]
    function parseJalaliDate(str) {
        return str.split('/').map(Number);
    }
    AppUtils.parseJalaliDate = parseJalaliDate;
    function findJalaliDate(str) {
        const matches = str.match(/(\d{4})\/(\d{2})\/(\d{2})/);
        if (matches) {
            return [parseInt(matches[1]), parseInt(matches[2]), parseInt(matches[3])];
        }
        else {
            return [];
        }
    }
    AppUtils.findJalaliDate = findJalaliDate;
    function calculateSalMaliFromReportDateAndPeriod(rpYear, rpMonth, period) {
        const smMonth = rpMonth + (12 - period);
        const smYear = rpYear;
        if (smMonth > 12) {
            return [smYear + 1, smMonth - 12];
        }
        else {
            return [smYear, smMonth];
        }
    }
    AppUtils.calculateSalMaliFromReportDateAndPeriod = calculateSalMaliFromReportDateAndPeriod;
    function calculatePeriod(rpYear, rpMonth, smYear, smMonth) {
        return 12 - ((smYear - rpYear) * 12 + smMonth - rpMonth);
    }
    AppUtils.calculatePeriod = calculatePeriod;
    function calculateSm(value) {
        const remaining = 12 - value.period;
        let smYear = value.rpYear, smMonth = value.rpMonth + remaining;
        if (smMonth > 12) {
            smMonth -= 12;
            smYear += 1;
        }
        return [smYear, smMonth];
    }
    AppUtils.calculateSm = calculateSm;
    function calculateProfitMargin(sale, cost) {
        return (sale - cost) / sale;
    }
    AppUtils.calculateProfitMargin = calculateProfitMargin;
    // a + p% = b
    function calculatePercent(a, b, places = 0) {
        return parseFloat(((b / a - 1) * 100).toFixed(places));
    }
    AppUtils.calculatePercent = calculatePercent;
    function periodHash(year, month) {
        return year * 100 + month;
    }
    AppUtils.periodHash = periodHash;
    function isSameDay(d1, d2) {
        return d1.getFullYear() === d2.getFullYear() &&
            d1.getMonth() === d2.getMonth() &&
            d1.getDate() === d2.getDate();
    }
    AppUtils.isSameDay = isSameDay;
    function isHoliday(d) {
        return d.getDay() === 4 || d.getDay() === 5;
    }
    AppUtils.isHoliday = isHoliday;
})(AppUtils = exports.AppUtils || (exports.AppUtils = {}));
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoidXRpbHMuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi8uLi8uLi9zcmMvZGF0YS9maW5hbmNpYWxTdGF0ZW1lbnRzL3Byb3ZpZGVyL3V0aWxzLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztBQUFBLCtDQUF1QztBQUN2Qyx1Q0FBdUM7QUFHaEMsS0FBSyxVQUFVLHNCQUFzQixDQUFDLEdBQVcsRUFBRSxPQUFnQjtJQUN0RSxJQUFJLE9BQU8sS0FBSyxTQUFTLEVBQUU7UUFDdkIsTUFBTSxFQUFFLEdBQUcsd0JBQVMsQ0FBQyxFQUFDLE9BQU8sRUFBQyxDQUFDLENBQUM7UUFDaEMsR0FBRyxJQUFJLEdBQUcsR0FBRyxFQUFFLENBQUM7S0FDbkI7SUFDRCxNQUFNLEdBQUcsR0FBRyxNQUFNLEtBQUssQ0FBQyxHQUFHLENBQUMsQ0FBQztJQUM3QixPQUFPLE1BQU0sR0FBRyxDQUFDLElBQUksRUFBRSxDQUFDO0FBQzVCLENBQUM7QUFQRCx3REFPQztBQUlELElBQWlCLFFBQVEsQ0ErRXhCO0FBL0VELFdBQWlCLFFBQVE7SUFDckIsU0FBZ0IsS0FBSyxDQUFDLEdBQVc7UUFDN0IsSUFBSSxHQUFHLElBQUksSUFBSSxJQUFJLEdBQUcsQ0FBQyxNQUFNLEtBQUssQ0FBQyxFQUFFO1lBQ2pDLE9BQU8sR0FBRyxDQUFDO1NBQ2Q7UUFDRCxPQUFPLFNBQVMsQ0FBQyxHQUFHLENBQUMsQ0FBQyxVQUFVLEVBQUUsQ0FBQyxRQUFRLEVBQUUsQ0FBQztJQUNsRCxDQUFDO0lBTGUsY0FBSyxRQUtwQixDQUFBO0lBRUQsU0FBZ0IsV0FBVyxDQUFDLEdBQVc7UUFDbkMsSUFBSSxHQUFHLElBQUksSUFBSSxJQUFJLEdBQUcsQ0FBQyxNQUFNLEtBQUssQ0FBQyxFQUFFO1lBQ2pDLE9BQU8sR0FBRyxDQUFDO1NBQ2Q7UUFDRCxPQUFPLFNBQVMsQ0FBQyxHQUFHLENBQUMsQ0FBQyxlQUFlLEVBQUUsQ0FBQyxRQUFRLEVBQUUsQ0FBQztJQUN2RCxDQUFDO0lB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